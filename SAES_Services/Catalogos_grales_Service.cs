using SAES_DBO.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SAES_DBO.Models.ModelCobranza;
using static SAES_DBO.Models.ModelAlumno;

namespace SAES_Services
{
    public class Catalogos_grales_Service : Methods
    {
      
        public DataTable QRY_TSTSO()
        {
            ModelTstsoRequest request = new ModelTstsoRequest() {  };
            List<ModelTstsoResponse> response = DB.CallSPListResult<ModelTstsoResponse, ModelTstsoRequest>(request);
            return ToDataTable(response);
        }

        public DataTable QRY_TBANC()
        {
            ModeltbancRequest request = new ModeltbancRequest() { };
            List<ModeltbancResponse> response = DB.CallSPListResult<ModeltbancResponse, ModeltbancRequest>(request);
            return ToDataTable(response);
        }

        public DataTable Qry_tbanc_Combo()
        {
            ModeltbancRequest request = new ModeltbancRequest() {};
            List<ModeltbancResponse> response = DB.CallSPListResult<ModeltbancResponse, ModeltbancRequest>(request);
            return ToDataTableForDropDownList(response);
        }

        public string Ins_tbanc(string clave_banc, string codigo, string descripcion, string usu_alta, string estatus, string ruta_logo, string convenio)
        {
            ModelInstbanc Insert = new ModelInstbanc()
            {
                tbanc_clave = clave_banc,
                tbanc_tcoco_clave = codigo,
                tbanc_desc = descripcion,
                tbanc_tuser_clave = usu_alta,
                tbanc_estatus = estatus,
                tbanc_ruta_logo=ruta_logo,
                tbanc_convenio=convenio
            };
            return DB.CallSPForInsertUpdate(Insert);
        }

        public ModelInsTstscResponse Ins_tstsc(string p_tstso_desc, string p_tstso_estatus, string p_tstso_user, string p_tstso_clave)
        {
            ModelInsTstsc Insert = new ModelInsTstsc()
            {
                tstso_desc = p_tstso_desc,
                tstso_estatus = p_tstso_estatus,
                tstso_user = p_tstso_user,
                tstso_clave = p_tstso_clave
            };
            //return DB.CallSPForInsertUpdate(Insert);
            ModelInsTstscResponse response = DB.CallSPResult<ModelInsTstscResponse, ModelInsTstsc>(Insert);
            return response;
        }

        public string Upd_tstsc(string p_tstso_desc, string p_tstso_estatus, string p_tstso_user, string p_tstso_clave)
        {
            ModelUpdTstsc Update = new ModelUpdTstsc()
            {
                tstso_desc = p_tstso_desc,
                tstso_estatus = p_tstso_estatus,
                tstso_user = p_tstso_user,
                tstso_clave = p_tstso_clave
            };
            return DB.CallSPForInsertUpdate(Update);
        }

        public string Upd_tbanc(string clave_banc, string descripcion, string codigo, string usu_alta, string estatus, string ruta_logo, string convenio)
        {
            ModelUpdtbanc Update = new ModelUpdtbanc()
            {
                tbanc_clave = clave_banc,
                tbanc_tcoco_clave = codigo,
                tbanc_desc = descripcion,
                tbanc_tuser_clave = usu_alta,
                tbanc_estatus = estatus,
                tbanc_ruta_logo= ruta_logo,
                tbanc_convenio= convenio
            };
            return DB.CallSPForInsertUpdate(Update);
        }

        public DataTable QRY_TPAIS()
        {
            ModelTpaisRequest request = new ModelTpaisRequest() { };
            List<ModelTpaisResponse> response = DB.CallSPListResult<ModelTpaisResponse, ModelTpaisRequest>(request);
            return ToDataTable(response);
        }

        public string Ins_Tpais(string clave, string desc, string gentil, string user, string estatus)
        {
            ModelInsTpaisRequest Insert = new ModelInsTpaisRequest()
            {
                clave = clave,
                desc = desc,
                gentil = gentil,
                user = user,
                estatus = estatus
            };
            return DB.CallSPForInsertUpdate(Insert);
        }

        public string Upd_Tpais(string clave, string desc, string gentil, string user, string estatus)
        {
            ModelUpdTpaisRequest Update = new ModelUpdTpaisRequest()
            {
                clave = clave,
                desc = desc,
                gentil = gentil,
                user = user,
                estatus = estatus
            };
            return DB.CallSPForInsertUpdate(Update);
        }

        public bool ValidaClavePais(string clave)
        {
            ModelValTpaisRequest request = new ModelValTpaisRequest() { clave = clave };
            List<ModelValTpaisResponse> response = DB.CallSPListResult<ModelValTpaisResponse, ModelValTpaisRequest>(request);

            if (response != null && response.Count > 0)
            {
                return response[0].indicador == "0";
            }
            
            return false; 
        }

        public DataTable QRY_TESTA(string tpais_clave = null)
        {
            ModelTestaRequest request = new ModelTestaRequest() { tpais_clave = tpais_clave };
            List<ModelTestaResponse> response = DB.CallSPListResult<ModelTestaResponse, ModelTestaRequest>(request);
            return ToDataTable(response);
        }

        public DataTable QRY_TPAIS_COMBO()
        {
            ModelTpaisComboRequest request = new ModelTpaisComboRequest();
            var response = DB.CallSPListResult<ModelTpaisComboResponse, ModelTpaisComboRequest>(request);
            return ToDataTable(response);
        }

        public string Ins_Testa(string clave, string pais, string desc, string user, string estatus)
        {
            ModelInsTestaRequest request = new ModelInsTestaRequest()
            {
                clave = clave,
                pais_clave = pais,
                desc = desc,
                user = user,
                estatus = estatus
            };
            return DB.CallSPForInsertUpdate(request);
        }

        public string Upd_Testa(string clave, string pais, string desc, string user, string estatus)
        {
            ModelUpdTestaRequest request = new ModelUpdTestaRequest()
            {
                clave = clave,
                pais_clave = pais,
                desc = desc,
                user = user,
                estatus = estatus
            };
            return DB.CallSPForInsertUpdate(request);
        }

        public bool ValidaClaveEstado(string clave, string pais)
        {
            ModelValTestaRequest request = new ModelValTestaRequest() { clave = clave, pais = pais };
            List<ModelValTestaResponse> response = DB.CallSPListResult<ModelValTestaResponse, ModelValTestaRequest>(request);

            if (response != null && response.Count > 0)
            {
                return response[0].indicador == "0";
            }
            return false; 
        }

        public DataTable QRY_TESTA_POR_PAIS(string tpais_clave)
        {
            ModelTestaComboRequest request = new ModelTestaComboRequest() { tpais_clave = tpais_clave };
            var response = DB.CallSPListResult<ModelTestaComboResponse, ModelTestaComboRequest>(request);
            return ToDataTable(response);
        }

        public DataTable QRY_TDELE(string tpais_clave = null, string testa_clave = null)
        {
            ModelTdeleRequest request = new ModelTdeleRequest() 
            { 
                tpais_clave = tpais_clave, 
                testa_clave = testa_clave 
            };
            var response = DB.CallSPListResult<ModelTdeleResponse, ModelTdeleRequest>(request);
            return ToDataTable(response);
        }

        public string Ins_Tdele(string clave, string desc, string estado, string pais, string user, string estatus)
        {
            ModelInsTdeleRequest request = new ModelInsTdeleRequest()
            {
                clave = clave,
                desc = desc,
                estado_clave = estado,
                pais_clave = pais,
                user = user,
                estatus = estatus
            };
            return DB.CallSPForInsertUpdate(request);
        }
    
        public string Upd_Tdele(string clave, string desc, string estado, string pais, string user, string estatus)
        {
            ModelUpdTdeleRequest request = new ModelUpdTdeleRequest()
            {
                clave = clave,
                desc = desc,
                estado_clave = estado,
                pais_clave = pais,
                user = user,
                estatus = estatus
            };
            return DB.CallSPForInsertUpdate(request);
        }
        
        public bool ValidaClaveDelegacion(string clave, string estado, string pais)
        {
            ModelValTdeleRequest request = new ModelValTdeleRequest() 
            { 
                clave = clave, 
                estado = estado, 
                pais = pais 
            };
            List<ModelValTdeleResponse> response = DB.CallSPListResult<ModelValTdeleResponse, ModelValTdeleRequest>(request);

            if (response != null && response.Count > 0)
            {
                return response[0].indicador == "0";
            }
            
            return false; 
        }

        public DataTable QRY_TDELE_COMBO(string pais, string estado)
        {
            ModelTdeleComboRequest request = new ModelTdeleComboRequest() { pais = pais, estado = estado };
            var response = DB.CallSPListResult<ModelTdeleComboResponse, ModelTdeleComboRequest>(request);
            return ToDataTable(response);
        }

        public DataTable QRY_TZIPS(string pais = null, string estado = null, string dele = null)
        {
            ModelTzipRequest request = new ModelTzipRequest() { tpais = pais, testa = estado, tdele = dele };
            var response = DB.CallSPListResult<ModelTzipResponse, ModelTzipRequest>(request);
            return ToDataTable(response);
        }

        public string Ins_Tzip(string clave, string desc, string pais, string estado, string dele, string user, string estatus)
        {
            ModelInsTzipRequest request = new ModelInsTzipRequest()
            {
                clave = clave,
                desc = desc,
                pais = pais,
                estado = estado,
                dele = dele,
                user = user,
                estatus = estatus
            };
            return DB.CallSPForInsertUpdate(request);
        }
        
        public string Upd_Tzip(string clave, string desc, string pais, string estado, string dele, string user, string estatus)
        {
            ModelUpdTzipRequest request = new ModelUpdTzipRequest()
            {
                clave = clave,
                desc = desc,
                pais = pais,
                estado = estado,
                dele = dele,
                user = user,
                estatus = estatus
            };
            return DB.CallSPForInsertUpdate(request);
        }

        public bool ValidaClaveZip(string clave, string pais, string estado, string dele)
        {
            ModelValTzipRequest request = new ModelValTzipRequest() 
            { 
                clave = clave, 
                pais = pais, 
                estado = estado, 
                dele = dele 
            };
            var response = DB.CallSPListResult<ModelValTzipResponse, ModelValTzipRequest>(request);

            if (response != null && response.Count > 0)
            {
                return response[0].indicador == "0";
            }
            
            return false; 
        }

        public DataTable QRY_TCAMP_GRID()
        {
            return DB.CallSPListResult<dynamic, dynamic>("P_QRY_TCAMP_GRID", null).ToDataTable(); 
        }

        public bool ValidarClaveCampus(string clave)
        {
            ModelValCampusRequest request = new ModelValCampusRequest { clave = clave };
            var response = DB.CallSPListResult<ModelValCampusResponse, ModelValCampusRequest>(request);
            if (response != null && response.Count > 0)
            {
                return response[0].indicador == "0";
            }
            return false;
        }

        public string InsertarCampus(ModelInsCampusRequest request)
        {
            return DB.CallSPForInsertUpdate<ModelInsCampusRequest>("P_INS_TCAMP", request);
        }

        public DataTable QRY_TCAMP_COMBO()
        {
            return DB.CallSPListResult<dynamic, dynamic>("P_QRY_TCAMP_COMBO", null).ToDataTable();
        }

        public DataTable QRY_TCAPR_GRID(string campus)
        {
            var request = new { p_campus = campus };
            
            return DB.CallSPListResult<dynamic, dynamic>("P_QRY_TCAPR_GRID", request).ToDataTable();
        }

        public bool ValidarClavePrograma(string clave)
        {
            var request = new { p_clave = clave };
            var response = DB.CallSPListResult<ModelValCampusResponse, object>("P_VAL_TPROG_CLAVE", request);

            if (response != null && response.Count > 0)
            {
                return response[0].indicador == "0";
            }

            return false;
        }
        
        public string InsertarProgramaCampus(ModelInsProgCampusRequest request)
        {
            return DB.CallSPForInsertUpdate<ModelInsProgCampusRequest>("P_INS_TCAPR", request);
        }

        public string ActualizarProgramaCampus(ModelInsProgCampusRequest request)
        {
            return DB.CallSPForInsertUpdate<ModelInsProgCampusRequest>("P_UPD_TCAPR", request);
        }
        public string ObtenerNombrePrograma(string clave)
        {
            var request = new { p_clave = clave };
            DataTable dt = DB.CallSPListResult<dynamic, dynamic>("P_QRY_TPROG_NOMBRE", request).ToDataTable();
            
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows[0]["Programa"].ToString();
            }
            
            return string.Empty;
        }

        public DataTable QRY_TNIVE_COBRANZA(string campus)
        {
            var request = new { p_campus = campus };
            return DB.CallSPListResult<dynamic, dynamic>("P_QRY_TNIVE_COBRANZA", request).ToDataTable();
        }

        public DataTable QRY_TIPO_PERIODO_COBRANZA()
        {
            return DB.CallSPListResult<dynamic, dynamic>("P_QRY_TIPO_PERIODO_COBRANZA", null).ToDataTable();
        }

        public DataTable QRY_TPEES_PERIODOS(string filtro)
        {
            var request = new { p_filtro = filtro };
            return DB.CallSPListResult<dynamic, dynamic>("P_QRY_TPEES_PERIODOS", request).ToDataTable();
        }
        
        public DataTable QRY_TCOCA_COMBO()
        {
            return DB.CallSPListResult<dynamic, dynamic>("P_QRY_TCOCA_COMBO", null).ToDataTable();
        }

        public DataTable QRY_TCOCO_COMBO()
        {
            return DB.CallSPListResult<dynamic, dynamic>("P_QRY_TCOCO_COMBO", null).ToDataTable();
        }

        public string InsertarParametrosCobranza(ModelInsCobranzaRequest request)
        {
            return DB.CallSPForInsertUpdate<ModelInsCobranzaRequest>("P_INS_TPACO_COBRANZA", request);
        }

        public string ActualizarParametrosCobranza(ModelInsCobranzaRequest request)
        {
            return DB.CallSPForInsertUpdate<ModelInsCobranzaRequest>("P_UPD_TPACO_COBRANZA", request);
        }

        public DataTable QRY_TPACO_GRID(string periodo, string campus, string nivel, string tipo_p)
        {
            var request = new { 
                p_periodo = periodo, 
                p_campus = campus, 
                p_nivel = nivel, 
                p_tipo_p = tipo_p 
            };
            
            return DB.CallSPListResult<dynamic, dynamic>("P_QRY_TPACO_GRID", request).ToDataTable();
        }

        public bool ValidarPeriodoExiste(string periodo)
        {
            var request = new { p_periodo = periodo };
            var response = DB.CallSPListResult<ModelValCampusResponse, object>("P_VAL_TPEES_PERIODO", request);

            if (response != null && response.Count > 0)
            {
               return response[0].indicador == "0";
            }
            return false;
        }

        public bool ValidarConfiguracionCobranzaExiste(string periodo, string campus, string nivel, string tipo_per)
        {
            var request = new { 
                p_periodo = periodo, 
                p_campus = campus, 
                p_nivel = nivel, 
                p_tipo_per = tipo_per 
            };
            var response = DB.CallSPListResult<ModelValCampusResponse, object>("P_VAL_TPACO_EXISTE", request);

            if (response != null && response.Count > 0)
            {
                return response[0].indicador == "0";
            }
            return false;
        }

        public bool ValidarExistenciaTelefono(string matricula)
        {
            var request = new ModelValidaTalcoRequest { p_matricula = matricula };
            var resultado = DB.CallSPListResult<ModelValidaTalcoResponse, ModelValidaTalcoRequest>("P_VAL_TALCO_EXISTE", request);
            if (resultado != null && resultado.Count > 0)
            {
                return resultado[0].Indicador > 0;
            }            
            return false;
        }
    }
}