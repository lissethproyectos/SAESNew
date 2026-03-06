using Microsoft.SqlServer.Server;
using SAES_DBO.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using static SAES_DBO.Models.ModelAlumno;
using static SAES_DBO.Models.ModelCobranza;
using static SAES_DBO.Models.ModelMenu;

namespace SAES_Services
{
    public class CobranzaService : Methods
    {
        public DataTable ObtenerDatosCobranza(string periodo, string campus, string nivel, string clave_tpaco)
        {
            ModelObtenerDatosCobranzaRequest request = new ModelObtenerDatosCobranzaRequest() { P_Periodo = periodo, P_Campus= campus, P_Nivel=nivel, P_Clave_TPaco= clave_tpaco };
            List<ModelObtenerDatosCobranzaResponse> response = DB.CallSPListResult<ModelObtenerDatosCobranzaResponse, ModelObtenerDatosCobranzaRequest>(request);
            return ToDataTable(response);
        }

        public DataTable ObtenerDatosCobranzaGral(string Perido_Destino, string campus, string nivel, string programa, string tasa, string matricula, string tipo)
        {
            ModelObtenerDatosCobranzaGralRequest request = new ModelObtenerDatosCobranzaGralRequest() { P_Perido_Destino = Perido_Destino, 
                P_Campus = campus, P_Nivel = nivel, P_Programa=programa, P_Tasa=tasa, P_Matricula=matricula, P_Tipo=tipo};
            List<ModelObtenerDatosCobranzaGralResponse> response = DB.CallSPListResult<ModelObtenerDatosCobranzaGralResponse, ModelObtenerDatosCobranzaGralRequest>(request);
            return ToDataTable(response);
        }

        public ModelInsertarDatosCobranzaResponse InsertarDatosCobranza(string p_periodo, string p_campus, string p_nivel, string p_clave_tpaco, string p_desc_ins, string p_desc_parc, string p_clave_tcoca, string p_clave_tcoco, string p_usuario)
        {
            ModelInsertarDatosCobranzaRequest Insert = new ModelInsertarDatosCobranzaRequest()
            {
                periodo = p_periodo,
                campus = p_campus,
                nivel = p_nivel,
                clave_tpaco = p_clave_tpaco,
                desc_ins = p_desc_ins,
                desc_parc = p_desc_parc,
                clave_tcoca = p_clave_tcoca,
                clave_tcoco= p_clave_tcoco,
                usuario=p_usuario
            };
            ModelInsertarDatosCobranzaResponse response = DB.CallSPResult<ModelInsertarDatosCobranzaResponse, ModelInsertarDatosCobranzaRequest>(Insert);
            return response;
        }

        public string EliminarConsecCobranza(string p_matricula, string p_programa)
        {
            ModelEliminarConsecCobranzaRequest Eliminar = new ModelEliminarConsecCobranzaRequest()
            {
                Matricula = p_matricula,
                Programa = p_programa
            };
            return DB.CallSPForInsertUpdate(Eliminar);
        }
        public ModelEditarDatosCobranzaResponse EditarDatosCobranza(string p_periodo, string p_campus, string p_nivel, string p_clave_tpaco, string p_desc_ins, string p_desc_parc, string p_clave_tcoca, string p_clave_tcoco)
        {
            ModelEditarDatosCobranzaRequest Update = new ModelEditarDatosCobranzaRequest()
            {
                periodo = p_periodo,
                campus = p_campus,
                nivel = p_nivel,
                clave_tpaco = p_clave_tpaco,
                desc_ins = p_desc_ins,
                desc_parc = p_desc_parc,
                clave_tcoca = p_clave_tcoca,
                clave_tcoco = p_clave_tcoco

            };
            ModelEditarDatosCobranzaResponse response = DB.CallSPResult<ModelEditarDatosCobranzaResponse, ModelEditarDatosCobranzaRequest>(Update);
            return response;
        }

        public List<ModelObtenerDatosCobroResponse> ObtenerDatosTedcu(string p_matricula, string p_programa, string p_periodo)
        {
            ModelObtenerDatosCobroRequest request = new ModelObtenerDatosCobroRequest() { Matricula = p_matricula, Programa= p_programa, Periodo= p_periodo };
            List<ModelObtenerDatosCobroResponse> response = DB.CallSPListResult<ModelObtenerDatosCobroResponse, ModelObtenerDatosCobroRequest>(request);
            return response;
        }

        public DataTable ObtenerDatosTcedcEnc(string p_matricula, string p_campus, string p_programa, string p_periodo)
        {
            ModelObtenerDatosTcedcRequest request = new ModelObtenerDatosTcedcRequest() { Matricula = p_matricula, Programa = p_programa, Periodo=p_periodo };
            List<ModelObtenerDatosTcedcResponse> response = DB.CallSPListResult<ModelObtenerDatosTcedcResponse, ModelObtenerDatosTcedcRequest>(request);
            return ToDataTable(response);
        }

        public List<ModelObtenerDatosTcedcDetResponse> ObtenerDatosTcedcDet(string p_matricula, string p_campus, string p_programa, int p_consecutivo)
        {
            ModelObtenerDatosTcedcDetRequest request = new ModelObtenerDatosTcedcDetRequest() { Matricula = p_matricula, Campus=p_campus,  Programa = p_programa, Consecutivo=p_consecutivo };
            List<ModelObtenerDatosTcedcDetResponse> response = DB.CallSPListResult<ModelObtenerDatosTcedcDetResponse, ModelObtenerDatosTcedcDetRequest>(request);
            return response;
        }

        public ModelObtenerSaldoAlumnoResponse ObtenerSaldoAlumno(string p_matricula, string p_campus, string p_programa, string p_periodo)
        {
            ModelObtenerSaldoAlumnoRequest request = new ModelObtenerSaldoAlumnoRequest() { Matricula = p_matricula, Programa = p_programa, Periodo=p_periodo };
            ModelObtenerSaldoAlumnoResponse response = DB.CallSPResult<ModelObtenerSaldoAlumnoResponse, ModelObtenerSaldoAlumnoRequest>(request);
            return response;
        }

        public DataTable ObtenerDatosTcpcb(string p_periodo, string p_campus, string p_programa_nivel,
            string p_programa, string p_tipo_plan, string p_estatus
            )
        {
            ModelObtenerTcpbcRequest request = new ModelObtenerTcpbcRequest() { Periodo=p_periodo, Campus=p_campus, Programa_Nivel=p_programa_nivel,
            Programa= p_programa, Tipo_Plan= p_tipo_plan, Estatus= p_estatus
            };
            List<ModelObtenerTcpbcResponse> response = DB.CallSPListResult<ModelObtenerTcpbcResponse, ModelObtenerTcpbcRequest>(request);
            return ToDataTable(response);
        }

        public string InsertarBecaAlumno(string p_matricula, string p_tepcb_tpees_clave, string p_tepcb_tprog_clave,
            string p_tepcb_tpcbe_clave, int p_tepcb_prioridad, string p_tepcb_estatus, string p_tepcb_user)
        {
            ModelInsertarBecaAlumno request = new ModelInsertarBecaAlumno() { tpers_id = p_matricula, 
                tepcb_tpees_clave = p_tepcb_tpees_clave,
                tepcb_tprog_clave= p_tepcb_tprog_clave,
                tepcb_tpcbe_clave= p_tepcb_tpcbe_clave,
                tepcb_estatus= p_tepcb_estatus,
                tepcb_user= p_tepcb_user
            };
            return DB.CallSPForInsertUpdate(request);
        }

                public string EditarBecaAlumno(string p_matricula, string p_tepcb_tpees_clave, string p_tepcb_tprog_clave,
            string p_tepcb_tpcbe_clave, int p_tepcb_prioridad, string p_tepcb_estatus, string p_tepcb_user, string p_existe_cambio)
        {
            ModelEditarBecaAlumno request = new ModelEditarBecaAlumno() { tpers_id = p_matricula, 
                tepcb_tpees_clave = p_tepcb_tpees_clave,
                tepcb_tprog_clave= p_tepcb_tprog_clave,
                tepcb_tpcbe_clave= p_tepcb_tpcbe_clave,
                tepcb_estatus= p_tepcb_estatus,
                tepcb_user= p_tepcb_user,
                existe_cambio= p_existe_cambio
            };
            return DB.CallSPForInsertUpdate(request);
        }


        public List<ModelObtenerSFVAlumnoResponse> ObtenerSFVAlumno(string p_matricula, string p_programa)
        {
            ModelObtenerSFVAlumnoRequest request = new ModelObtenerSFVAlumnoRequest() { Matricula = p_matricula, Programa = p_programa };
            List<ModelObtenerSFVAlumnoResponse> response = DB.CallSPListResult<ModelObtenerSFVAlumnoResponse, ModelObtenerSFVAlumnoRequest>(request);
            return response;
        }

        public DataTable ObtenerConceptos(string p_campus, string p_programa)
        {
            ModelObtenerConceptosRequest request = new ModelObtenerConceptosRequest() { Campus = p_campus, Programa = p_programa };
            List<ModelObtenerConceptosResponse> response = DB.CallSPListResult<ModelObtenerConceptosResponse, ModelObtenerConceptosRequest>(request);
            return ToDataTableForDropDownList(response);
            //return response;
        }

        public string InsertarTedcu(string p_tedcu_tpers_id, string p_tedcu_tprog_clave, string p_tedcu_tpees_clave, int p_tedcu_consec, string p_tedcu_tcoco_clave
   ,    int p_tedcu_numero, decimal p_tedcu_importe, string p_tedcu_fecha_venc, string p_tedcu_user)
        {
            ModelInsTedcu Insert = new ModelInsTedcu()
            {
                tedcu_tpers_id = p_tedcu_tpers_id,
                tedcu_tprog_clave = p_tedcu_tprog_clave,
                tedcu_tpees_clave = p_tedcu_tpees_clave,
                tedcu_consec = p_tedcu_consec,
                tedcu_tcoco_clave = p_tedcu_tcoco_clave,
                tedcu_numero = p_tedcu_numero,
                tedcu_importe = p_tedcu_importe,
                tedcu_fecha_venc = p_tedcu_fecha_venc,
                tedcu_user = p_tedcu_user
            };
            return DB.CallSPForInsertUpdate(Insert);
            //ModelInstbaapResponse response = DB.CallSPResult<ModelInstbaapResponse, ModelInstbaap>(Insert);
            //return response;
        }

        public string InsertarTpago1(string p_tpag1_tpers_id, string p_tpag1_tprog_clave, string p_tpag1_tcoco_clave, 
            decimal p_tpag1_importe, string p_tpag1_tedcu_consec)
        {
            ModelInsertarTpag1 Insert = new ModelInsertarTpag1()
            {
                tpag1_tpers_id = p_tpag1_tpers_id,
                tpag1_tprog_clave = p_tpag1_tprog_clave,
                tpag1_tcoco_clave = p_tpag1_tcoco_clave,
                tpag1_importe = p_tpag1_importe,
                tpag1_tedcu_consec = p_tpag1_tedcu_consec
            };
            return DB.CallSPForInsertUpdate(Insert);
        }

        public string EditarSaldoSFV(string p_tpers_id, string p_tsafv_tprog_clave, string p_tsafv_tcoco_clave, 
            decimal p_tsafv_saldo, string p_tsafv_tpees_clave)
        {
            ModelEditarSaldoSFVRequest Update = new ModelEditarSaldoSFVRequest()
            {
                tpers_id = p_tpers_id,
                tsafv_tprog_clave = p_tsafv_tprog_clave,
                tsafv_tcoco_clave = p_tsafv_tcoco_clave,
                tsafv_saldo = p_tsafv_saldo,
                tsafv_tpees_clave= p_tsafv_tpees_clave
            };
            return DB.CallSPForInsertUpdate(Update);

        }
        public DataTable ObtenerTPago(string p_matricula, string p_programa)
        {
            ModelObtenerDatosTpagoRequest request = new ModelObtenerDatosTpagoRequest() { Matricula = p_matricula, Programa = p_programa };
            List<ModelObtenerDatosTpagoResponse> response = DB.CallSPListResult<ModelObtenerDatosTpagoResponse, ModelObtenerDatosTpagoRequest>(request);
            return ToDataTable(response);
        }

      

        public ModelValidaCuotasAlumnoResponse ValidaCuotasAlumno(string p_matricula, string p_periodo)
        {
            ModelValidaCuotasAlumnoRequest Insert = new ModelValidaCuotasAlumnoRequest()
            {
                matricula = p_matricula,
                periodo = p_periodo
            };
            ModelValidaCuotasAlumnoResponse response = DB.CallSPResult<ModelValidaCuotasAlumnoResponse, ModelValidaCuotasAlumnoRequest>(Insert);
            return response;
        }

        public ModelValidaCuotasPeriodoResponse ValidaCuotasMasivo(string p_periodo)
        {
            ModelValidaCuotasPeriodoRequest Insert = new ModelValidaCuotasPeriodoRequest()
            {
                periodo = p_periodo
            };
            ModelValidaCuotasPeriodoResponse response = DB.CallSPResult<ModelValidaCuotasPeriodoResponse, ModelValidaCuotasPeriodoRequest>(Insert);
            return response;
        }

        public string AplicarBecas(string p_periodo, string p_id, string p_programa, string p_beca, string p_user)
        {
            ModelAplicarBecasRequest Aplicar = new ModelAplicarBecasRequest()
            {
                periodo = p_periodo,
                id = p_id,
                programa= p_programa,
                beca=p_beca,
                user= p_user
            };
            return DB.CallSPForInsertUpdate(Aplicar);
        }

        public string GenerarEdoCta(string p_periodo, string p_id, string p_programa, string p_user)
        {
            ModelGenerarEdoCtaRequest Aplicar = new ModelGenerarEdoCtaRequest()
            {
                periodo = p_periodo,
                id = p_id,
                programa = p_programa,
                user = p_user
            };
            return DB.CallSPForInsertUpdate(Aplicar);
        }
    }
}
