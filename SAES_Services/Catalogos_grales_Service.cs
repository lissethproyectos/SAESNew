using SAES_DBO.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SAES_DBO.Models.ModelCobranza;

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

    }
}