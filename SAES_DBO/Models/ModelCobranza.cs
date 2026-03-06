using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAES_DBO.Models
{
    public class ModelCobranza
    {
        [SPName("P_QRY_TPACO")]
        public class ModelObtenerDatosCobranzaRequest : BaseModelRequest
        {
            [SPParameterName("P_PERIODO", 0)]
            public string P_Periodo { get; set; }

            [SPParameterName("P_CAMPUS", 1)]
            public string P_Campus { get; set; }

            [SPParameterName("P_NIVEL", 2)]
            public string P_Nivel { get; set; }

            [SPParameterName("P_CLAVE_TPACO", 3)]
            public string P_Clave_TPaco { get; set; }
        }
        public class ModelObtenerDatosCobranzaResponse : BaseModelResponse
        {
            [SPResponseColumnName("C_CAMPUS")]
            public string C_Campus { get; set; }

            [SPResponseColumnName("CAMPUS")]
            public string Campus { get; set; }

            [SPResponseColumnName("C_NIVEL")]
            public string C_Nivel { get; set; }

            [SPResponseColumnName("NIVEL")]
            public string Nivel { get; set; }

            [SPResponseColumnName("CLAVE")]
            public string Clave { get; set; }

            [SPResponseColumnName("TIPO_PERIODO")]
            public string Tipo_Periodo { get; set; }

            [SPResponseColumnName("DESC_INSC")]
            public string Desc_Insc { get; set; }

            [SPResponseColumnName("DESC_COL")]
            public string Desc_Col { get; set; }

            [SPResponseColumnName("C_CONCE_CAL")]
            public string C_Conce_Cal { get; set; }

            [SPResponseColumnName("CONCE_CALENDARIO")]
            public string Conce_Calendario { get; set; }

            [SPResponseColumnName("C_CONCE_COB")]
            public string C_Conce_Cob { get; set; }

            [SPResponseColumnName("CONCE_COBRANZA")]
            public string Conce_Cobranza { get; set; }

            [SPResponseColumnName("PERIODO")]
            public string Periodo { get; set; }
        }

        [SPName("P_QRY_TGECA")]
        public class ModelObtenerDatosCobranzaGralRequest : BaseModelRequest
        {
            [SPParameterName("P_Perido_Destino", 0)]
            public string P_Perido_Destino { get; set; }

            [SPParameterName("P_Campus", 1)]
            public string P_Campus { get; set; }

            [SPParameterName("P_Nivel", 2)]
            public string P_Nivel { get; set; }

            [SPParameterName("P_Programa", 3)]
            public string P_Programa { get; set; }

            [SPParameterName("P_Tasa", 4)]
            public string P_Tasa { get; set; }

            [SPParameterName("P_Matricula", 5)]
            public string P_Matricula { get; set; }

            [SPParameterName("P_Tipo", 6)]
            public string P_Tipo { get; set; }
        }
        public class ModelObtenerDatosCobranzaGralResponse : BaseModelResponse
        {
            [SPResponseColumnName("matricula")]
            public string matricula { get; set; }

            [SPResponseColumnName("nombre")]
            public string nombre { get; set; }

            [SPResponseColumnName("campus")]
            public string campus { get; set; }

            [SPResponseColumnName("nivel")]
            public string nivel { get; set; }

            [SPResponseColumnName("programa")]
            public string programa { get; set; }

            [SPResponseColumnName("tasa")]
            public string tasa { get; set; }
        }


        [SPName("P_INS_TPACO")]
        public class ModelInsertarDatosCobranzaRequest : BaseModelRequest
        {

            [SPParameterName("P_PERIODO", 0)]
            public string periodo { get; set; }

            [SPParameterName("P_CAMPUS", 1)]
            public string campus { get; set; }

            [SPParameterName("P_NIVEL", 2)]
            public string nivel { get; set; }

            [SPParameterName("P_CLAVE_TPACO", 3)]
            public string clave_tpaco { get; set; }

            [SPParameterName("P_DESC_INS", 4)]
            public string desc_ins { get; set; }

            [SPParameterName("P_DESC_PARC", 5)]
            public string desc_parc { get; set; }

            [SPParameterName("P_CLAVE_TCOCA", 6)]
            public string clave_tcoca { get; set; }

            [SPParameterName("P_CLAVE_TCOCO", 7)]
            public string clave_tcoco { get; set; }

            [SPParameterName("P_USUARIO", 7)]
            public string usuario { get; set; }

        }
        public class ModelInsertarDatosCobranzaResponse : BaseModelResponse
        {
            [SPResponseColumnName("Existe")]
            public string Existe { get; set; }
        }

        [SPName("P_VAL_CUOTAS_ALUMNO")]
        public class ModelValidaCuotasAlumnoRequest : BaseModelRequest
        {

            [SPParameterName("P_Matricula", 0)]
            public string matricula { get; set; }

            [SPParameterName("P_Periodo", 1)]
            public string periodo { get; set; }

           
        }
        public class ModelValidaCuotasAlumnoResponse : BaseModelResponse
        {
            [SPResponseColumnName("Existe")]
            public string Existe { get; set; }
        }

        [SPName("P_VAL_CUOTAS_PERIODO")]
        public class ModelValidaCuotasPeriodoRequest : BaseModelRequest
        {

            [SPParameterName("P_Periodo", 1)]
            public string periodo { get; set; }


        }
        public class ModelValidaCuotasPeriodoResponse : BaseModelResponse
        {
            [SPResponseColumnName("Existe")]
            public string Existe { get; set; }
        }


        [SPName("P_DEL_PAG1")]
        public class ModelEliminarConsecCobranzaRequest : BaseModelRequest
        {
            [SPParameterName("P_Matricula", 0)]
            public string Matricula { get; set; }

            [SPParameterName("P_Programa", 1)]
            public string Programa { get; set; }
        }

        [SPName("P_UPD_TPACO")]
        public class ModelEditarDatosCobranzaRequest : BaseModelRequest
        {

            [SPParameterName("P_PERIODO", 0)]
            public string periodo { get; set; }

            [SPParameterName("P_CAMPUS", 1)]
            public string campus { get; set; }

            [SPParameterName("P_NIVEL", 2)]
            public string nivel { get; set; }

            [SPParameterName("P_CLAVE_TPACO", 3)]
            public string clave_tpaco { get; set; }

            [SPParameterName("P_DESC_INS", 4)]
            public string desc_ins { get; set; }

            [SPParameterName("P_DESC_PARC", 5)]
            public string desc_parc { get; set; }

            [SPParameterName("P_CLAVE_TCOCA", 6)]
            public string clave_tcoca { get; set; }

            [SPParameterName("P_CLAVE_TCOCO", 7)]
            public string clave_tcoco { get; set; }
        }
        public class ModelEditarDatosCobranzaResponse : BaseModelResponse
        {
            [SPResponseColumnName("Existe")]
            public string Existe { get; set; }
        }

        [SPName("P_QRY_TEDCU")]
        public class ModelObtenerDatosCobroRequest : BaseModelRequest
        {
            [SPParameterName("P_Matricula", 0)]
            public string Matricula { get; set; }

            [SPParameterName("P_Programa", 1)]
            public string Programa { get; set; }

            [SPParameterName("P_Periodo", 2)]
            public string Periodo { get; set; }            
        }
        public class ModelObtenerDatosCobroResponse : BaseModelResponse
        {
            [SPResponseColumnName("transa")]
            public string transa { get; set; }

            [SPResponseColumnName("periodo")]
            public string periodo { get; set; }

            [SPResponseColumnName("parcia")]
            public string parcia { get; set; }

            [SPResponseColumnName("clave")]
            public string clave { get; set; }

            [SPResponseColumnName("concepto")]
            public string concepto { get; set; }

            [SPResponseColumnName("saldo")]
            public string saldo { get; set; }

            [SPResponseColumnName("vencimiento")]
            public string vencimiento { get; set; }

            [SPResponseColumnName("importe")]
            public string importe { get; set; }

            [SPResponseColumnName("inhabil")]
            public bool inhabil { get; set; }

            [SPResponseColumnName("importe_total")]
            public string importe_total { get; set; }
        }
        public class Pago
        {
            [SPResponseColumnName("saldo")]
            public string saldo { get; set; }
        }

        [SPName("P_QRY_TCEDC")]
        public class ModelObtenerDatosTcedcRequest : BaseModelRequest
        {
            [SPParameterName("P_Matricula", 0)]
            public string Matricula { get; set; }

            [SPParameterName("P_Campus", 1)]
            public string Campus { get; set; }

            [SPParameterName("P_Programa", 2)]
            public string Programa { get; set; }

            [SPParameterName("P_Periodo", 3)]
            public string Periodo { get; set; }
        }
        public class ModelObtenerDatosTcedcResponse : BaseModelResponse
        {
            [SPResponseColumnName("tedcu_consec")]
            public string tedcu_consec { get; set; }

            [SPResponseColumnName("tedcu_tpees_clave")]
            public string tedcu_tpees_clave { get; set; }

            [SPResponseColumnName("tcoco_desc")]
            public string tcoco_desc { get; set; }

            [SPResponseColumnName("tedcu_importe")]
            public string tedcu_importe { get; set; }

            [SPResponseColumnName("pago")]
            public string pago { get; set; }

            [SPResponseColumnName("saldo")]
            public string saldo { get; set; }

            [SPResponseColumnName("vencimiento_cargo")]
            public string vencimiento_cargo { get; set; }

            [SPResponseColumnName("saldo_pendiente")]
            public string saldo_pendiente { get; set; }

            [SPResponseColumnName("tipo")]
            public string tipo { get; set; }

            [SPResponseColumnName("tpers_id")]
            public string tpers_id { get; set; }

            [SPResponseColumnName("nombre")]
            public string nombre { get; set; }

            [SPResponseColumnName("factura")]
            public string factura { get; set; }

            [SPResponseColumnName("desc_concepto")]
            public string desc_concepto { get; set; }

            [SPResponseColumnName("vencimiento")]
            public string vencimiento { get; set; }
        }

        [SPName("P_QRY_COMPROBANTES")]
        public class ModelObtenerDatosTpagoRequest : BaseModelRequest
        {
            [SPParameterName("P_Matricula", 0)]
            public string Matricula { get; set; }

            [SPParameterName("P_Programa", 1)]
            public string Programa { get; set; }
        }
        public class ModelObtenerDatosTpagoResponse : BaseModelResponse
        {
            [SPResponseColumnName("fecha")]
            public string fecha { get; set; }

            [SPResponseColumnName("tipo")]
            public string tipo { get; set; }

            [SPResponseColumnName("numero")]
            public string numero { get; set; }

            [SPResponseColumnName("total")]
            public string total { get; set; }

            [SPResponseColumnName("recibo")]
            public string recibo { get; set; }
        }

        [SPName("P_QRY_TCEDC_DET")]
        public class ModelObtenerDatosTcedcDetRequest : BaseModelRequest
        {
            [SPParameterName("P_Matricula", 0)]
            public string Matricula { get; set; }

            [SPParameterName("P_Campus", 1)]
            public string Campus { get; set; }

            [SPParameterName("P_Programa", 2)]
            public string Programa { get; set; }

            [SPParameterName("P_Consecutivo", 3)]
            public int Consecutivo { get; set; }
        }
        public class ModelObtenerDatosTcedcDetResponse : BaseModelResponse
        {
            [SPResponseColumnName("tedcu_consec")]
            public string tedcu_consec { get; set; }

            [SPResponseColumnName("tedcu_tpees_clave")]
            public string tedcu_tpees_clave { get; set; }

            [SPResponseColumnName("tcoco_desc")]
            public string tcoco_desc { get; set; }

            [SPResponseColumnName("tedcu_importe")]
            public string tedcu_importe { get; set; }

            [SPResponseColumnName("pago")]
            public string pago { get; set; }

            [SPResponseColumnName("saldo")]
            public string saldo { get; set; }

            [SPResponseColumnName("vencimiento_cargo")]
            public string vencimiento_cargo { get; set; }

            [SPResponseColumnName("saldo_pendiente")]
            public string saldo_pendiente { get; set; }

            [SPResponseColumnName("tipo")]
            public string tipo { get; set; }

            [SPResponseColumnName("tpers_id")]
            public string tpers_id { get; set; }

            [SPResponseColumnName("nombre")]
            public string nombre { get; set; }

            [SPResponseColumnName("factura")]
            public string factura { get; set; }

            [SPResponseColumnName("desc_concepto")]
            public string desc_concepto { get; set; }

            [SPResponseColumnName("vencimiento")]
            public string vencimiento { get; set; }

            [SPResponseColumnName("fecha_pago")]
            public string fecha_pago { get; set; }

            [SPResponseColumnName("tpago_factura_cons")]
            public string tpago_factura_cons { get; set; }

            [SPResponseColumnName("recibo")]

            public string recibo { get; set; }

            [SPResponseColumnName("tiene_recibo")]

            public bool tiene_recibo { get; set; }



        }

        [SPName("P_QRY_SALDO_ALUMNO")]
        public class ModelObtenerSaldoAlumnoRequest : BaseModelRequest
        {
            [SPParameterName("P_Matricula", 0)]
            public string Matricula { get; set; }

            [SPParameterName("P_Campus", 1)]
            public string Campus { get; set; }

            [SPParameterName("P_Programa", 2)]
            public string Programa { get; set; }

            [SPParameterName("P_Periodo", 3)]
            public string Periodo { get; set; }
        }
        public class ModelObtenerSaldoAlumnoResponse : BaseModelResponse
        {
            [SPResponseColumnName("matricula")]
            public string matricula { get; set; }

            [SPResponseColumnName("nombre")]
            public string nombre { get; set; }

            [SPResponseColumnName("cargo")]
            public string cargo { get; set; }

            [SPResponseColumnName("abono")]
            public string abono { get; set; }

            [SPResponseColumnName("saldo")]
            public string saldo { get; set; }

            [SPResponseColumnName("beca")]
            public string beca { get; set; }

            [SPResponseColumnName("desc_prog")]
            public string desc_prog { get; set; }

            [SPResponseColumnName("cancelacion")]
            public string cancelacion { get; set; }
        }


        [SPName("P_QRY_TCPCB")]
        public class ModelObtenerTcpbcRequest : BaseModelRequest
        {
            [SPParameterName("p_periodo", 0)]
            public string Periodo { get; set; }

            [SPParameterName("p_campus", 1)]
            public string Campus { get; set; }

            [SPParameterName("p_programa_nivel", 2)]
            public string Programa_Nivel { get; set; }

            [SPParameterName("p_programa", 3)]
            public string Programa { get; set; }

            [SPParameterName("p_tipo_plan", 4)]
            public string Tipo_Plan { get; set; }

            [SPParameterName("p_estatus", 5)]
            public string Estatus { get; set; }
        }
        public class ModelObtenerTcpbcResponse : BaseModelResponse
        {
            [SPResponseColumnName("campus")]
            public string campus { get; set; }

            [SPResponseColumnName("nivel")]
            public string nivel { get; set; }

            [SPResponseColumnName("programa")]
            public string programa { get; set; }

            [SPResponseColumnName("tipo")]
            public string tipo { get; set; }

            [SPResponseColumnName("matricula")]
            public string matricula { get; set; }

            [SPResponseColumnName("nombre")]
            public string nombre { get; set; }

            [SPResponseColumnName("periodo")]
            public string periodo { get; set; }

            [SPResponseColumnName("clave")]
            public string clave { get; set; }

            [SPResponseColumnName("descrip")]
            public string descrip { get; set; }

            [SPResponseColumnName("porc")]
            public string porc { get; set; }

            [SPResponseColumnName("monto")]
            public string monto { get; set; }

            [SPResponseColumnName("estatus")]
            public string estatus { get; set; }

            [SPResponseColumnName("fecha")]
            public string fecha { get; set; }

            [SPResponseColumnName("usuario")]
            public string usuario { get; set; }
        }

        [SPName("P_QRY_CONCEPTOS")]
        public class ModelObtenerConceptosRequest : BaseModelRequest
        {
            [SPParameterName("P_Campus", 0)]
            public string Campus { get; set; }

            [SPParameterName("P_Programa", 1)]
            public string Programa { get; set; }
        }
        public class ModelObtenerConceptosResponse : BaseModelResponse
        {
            [SPResponseColumnName("clave")]
            public string clave { get; set; }

            [SPResponseColumnName("descripcion")]
            public string descripcion { get; set; }

            [SPResponseColumnName("importe")]
            public string importe { get; set; }
        }

        [SPName("P_INS_TEDCU")]
        public class ModelInsTedcu : BaseModelRequest
        {
            [SPParameterName("p_tedcu_tpers_id", 0)]
            public string tedcu_tpers_id { get; set; }

            [SPParameterName("p_tedcu_tprog_clave", 1)]
            public string tedcu_tprog_clave { get; set; }

            [SPParameterName("p_tedcu_tpees_clave", 2)]
            public string tedcu_tpees_clave { get; set; }

            [SPParameterName("p_tedcu_consec", 3)]
            public int tedcu_consec { get; set; }

            [SPParameterName("p_tedcu_tcoco_clave", 4)]
            public string tedcu_tcoco_clave { get; set; }

            [SPParameterName("p_tedcu_numero", 5)]
            public int tedcu_numero { get; set; }

            [SPParameterName("p_tedcu_importe", 6)]
            public decimal tedcu_importe { get; set; }

          

            [SPParameterName("p_tedcu_fecha_venc", 7)]
            public string tedcu_fecha_venc { get; set; }

            [SPParameterName("p_tedcu_user", 8)]
            public string tedcu_user { get; set; }
        }

        [SPName("P_INS_TPAG1")]
        public class ModelInsertarTpag1 : BaseModelRequest
        {

            [SPParameterName("p_tpag1_tpers_id", 0)]
            public string tpag1_tpers_id { get; set; }

            [SPParameterName("p_tpag1_tprog_clave", 1)]
            public string tpag1_tprog_clave { get; set; }

            [SPParameterName("p_tpag1_tcoco_clave", 2)]
            public string tpag1_tcoco_clave { get; set; }

            [SPParameterName("p_tpag1_importe", 3)]
            public decimal tpag1_importe { get; set; }

            [SPParameterName("p_tpag1_tedcu_consec", 4)]
            public string tpag1_tedcu_consec { get; set; }
        }

        [SPName("P_QRY_SFV")]
        public class ModelObtenerSFVAlumnoRequest : BaseModelRequest
        {
            [SPParameterName("P_Matricula", 0)]
            public string Matricula { get; set; }

            [SPParameterName("P_Programa", 1)]
            public string Programa { get; set; }
        }
        public class ModelObtenerSFVAlumnoResponse : BaseModelResponse
        {
            [SPResponseColumnName("saldo")]
            public string saldo { get; set; }
        }

        [SPName("P_UPD_TSAFV")]
        public class ModelEditarSaldoSFVRequest : BaseModelRequest
        {

            [SPParameterName("p_tpers_id", 0)]
            public string tpers_id { get; set; }

            [SPParameterName("p_tsafv_tprog_clave", 1)]
            public string tsafv_tprog_clave { get; set; }

            [SPParameterName("p_tsafv_tcoco_clave", 2)]
            public string tsafv_tcoco_clave { get; set; }

            [SPParameterName("p_tsafv_saldo", 3)]
            public decimal tsafv_saldo { get; set; }

            [SPParameterName("p_tsafv_tpees_clave", 4)]
            public string tsafv_tpees_clave { get; set; }
        }

        [SPName("P_INS_TEPCB")]
        public class ModelInsertarBecaAlumno : BaseModelRequest
        {
            [SPParameterName("p_tpers_id", 0)]
            public string tpers_id { get; set; }

            [SPParameterName("p_tepcb_tpees_clave", 1)]
            public string tepcb_tpees_clave { get; set; }

            [SPParameterName("p_tepcb_tprog_clave", 2)]
            public string tepcb_tprog_clave { get; set; }

            [SPParameterName("p_tepcb_tpcbe_clave", 3)] 
            public string tepcb_tpcbe_clave { get; set; }

            [SPParameterName("p_tepcb_prioridad", 4)]
            public string tepcb_prioridad { get; set; }

            [SPParameterName("p_tepcb_estatus", 5)]
            public string tepcb_estatus { get; set; }

            [SPParameterName("p_tepcb_user", 6)]
            public string tepcb_user { get; set; }
        }


        [SPName("P_UPD_TEPCB")]
        public class ModelEditarBecaAlumno : BaseModelRequest
        {
            [SPParameterName("p_tpers_id", 0)]
            public string tpers_id { get; set; }

            [SPParameterName("p_tepcb_tpees_clave", 1)]
            public string tepcb_tpees_clave { get; set; }

            [SPParameterName("p_tepcb_tprog_clave", 2)]
            public string tepcb_tprog_clave { get; set; }

            [SPParameterName("p_tepcb_tpcbe_clave", 3)]
            public string tepcb_tpcbe_clave { get; set; }           

            [SPParameterName("p_tepcb_estatus", 4)]
            public string tepcb_estatus { get; set; }

            [SPParameterName("p_tepcb_user", 5)]
            public string tepcb_user { get; set; }

            [SPParameterName("p_existe_cambio", 6)]
            public string existe_cambio { get; set; }
        }

        [SPName("P_APLICA_BECA")]
        public class ModelAplicarBecasRequest : BaseModelRequest
        {
            [SPParameterName("p_periodo", 0)]
            public string periodo { get; set; }

            [SPParameterName("p_id", 1)]
            public string id { get; set; }

            [SPParameterName("p_programa", 2)]
            public string programa { get; set; }

            [SPParameterName("p_beca", 3)]
            public string beca { get; set; }

            [SPParameterName("p_user", 4)]
            public string user { get; set; }
        }

        [SPName("P_GENERA_CARTERA")]
        public class ModelGenerarEdoCtaRequest : BaseModelRequest
        {
            [SPParameterName("p_periodo", 0)]
            public string periodo { get; set; }

            [SPParameterName("p_id", 1)]
            public string id { get; set; }

            [SPParameterName("p_programa", 2)]
            public string programa { get; set; }

            [SPParameterName("p_user", 3)]
            public string user { get; set; }
        }
    }
}
