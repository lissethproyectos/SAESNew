using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAES_DBO.Models
{
    public class ModelInstdireResponse : BaseModelResponse
    {
        [SPResponseColumnName("Existe")]
        public string Existe { get; set; }
    }

    [SPName("P_INS_TORCA")]
    public class ModelInstorca : BaseModelRequest
    {
        [SPParameterName("p_torca_clave", 0)]
        public string torca_clave { get; set; }

        [SPParameterName("p_torca_desc", 1)]
        public string torca_desc { get; set; }

        [SPParameterName("p_torca_user", 2)]
        public string torca_user { get; set; }

        [SPParameterName("p_torca_estatus", 3)]
        public string torca_estatus { get; set; }
    }
    public class ModelInstorcaResponse : BaseModelResponse
    {
        [SPResponseColumnName("Existe")]
        public string Existe { get; set; }
    }

    [SPName("P_UPD_TDIRE")]
    public class ModelUpdtdire : BaseModelRequest
    {

        [SPParameterName("Clave_tdire", 0)]
        public string tdire_clave { get; set; }

        [SPParameterName("Desc_tdire", 2)]
        public string tdire_desc { get; set; }

        [SPParameterName("Usuario", 3)]
        public string tdire_user { get; set; }

        [SPParameterName("Estatus", 4)]
        public string tdire_estatus { get; set; }

    }

    [SPName("P_INS_TFUCA")]
    public class ModelInstfuca : BaseModelRequest
    {
        [SPParameterName("p_tfuca_tcamp_clave", 0)]
        public string tfuca_tcamp_clave { get; set; }

        [SPParameterName("p_tfuca_clave", 1)]
        public string tfuca_clave { get; set; }

        [SPParameterName("p_tfuca_desc", 2)]
        public string tfuca_desc { get; set; }

        [SPParameterName("p_tfuca_nombre", 3)]
        public string tfuca_nombre { get; set; }

        [SPParameterName("p_tfuca_paterno", 4)]
        public string tfuca_paterno { get; set; }

        [SPParameterName("p_tfuca_materno", 5)]
        public string tfuca_materno { get; set; }

        [SPParameterName("p_tfuca_curp", 6)]
        public string tfuca_curp { get; set; }

        [SPParameterName("p_tfuca_tuser_clave", 7)]
        public string tfuca_tuser_clave { get; set; }

        [SPParameterName("p_tfuca_estatus", 8)]
        public string tfuca_estatus { get; set; }
    }
    public class ModelInstfucaResponse : BaseModelResponse
    {
        [SPResponseColumnName("Existe")]
        public string Existe { get; set; }
    }


    [SPName("P_INS_TTINI")]
    public class ModelInsTtini : BaseModelRequest
    {
        [SPParameterName("p_ttini_ttiop_clave", 0)]
        public string ttini_ttiop_clave { get; set; }

        [SPParameterName("p_ttini_tnive_clave", 1)]
        public string ttini_tnive_clave { get; set; }

        [SPParameterName("p_ttini_creditos", 2)]
        public string ttini_creditos { get; set; }

        [SPParameterName("p_ttini_promedio", 3)]
        public string ttini_promedio { get; set; }

        [SPParameterName("p_ttini_tcoco_clave", 4)]
        public string ttini_tcoco_clave { get; set; }

        [SPParameterName("p_ttini_tuser_clave", 5)]
        public string ttini_tuser_clave { get; set; }

        [SPParameterName("p_ttini_tseso", 6)]
        public string ttini_tseso { get; set; }        
    }
    public class ModelInsTtiniResponse : BaseModelResponse
    {
        [SPResponseColumnName("Existe")]
        public string Existe { get; set; }
    }

    [SPName("P_UPD_TTINI")]
    public class ModelUpdttini : BaseModelRequest
    {
        [SPParameterName("p_ttini_ttiop_clave", 0)]
        public string ttini_ttiop_clave { get; set; }

        [SPParameterName("p_ttini_tnive_clave", 1)]
        public string ttini_tnive_clave { get; set; }

        [SPParameterName("p_ttini_creditos", 2)]
        public string ttini_creditos { get; set; }

        [SPParameterName("p_ttini_promedio", 3)]
        public string ttini_promedio { get; set; }

        [SPParameterName("p_ttini_tcoco_clave", 4)]
        public string ttini_tcoco_clave { get; set; }

        [SPParameterName("p_ttini_tuser_clave", 5)]
        public string ttini_tuser_clave { get; set; }

        [SPParameterName("p_ttini_tseso", 6)]
        public string ttini_tseso { get; set; }
    }


    [SPName("P_UPD_TFUCA")]
    public class ModelUpdtfuca : BaseModelRequest
    {
        [SPParameterName("p_tfuca_tcamp_clave", 0)]
        public string tfuca_tcamp_clave { get; set; }

        [SPParameterName("p_tfuca_clave", 1)]
        public string tfuca_clave { get; set; }

        [SPParameterName("p_tfuca_desc", 2)]
        public string tfuca_desc { get; set; }

        [SPParameterName("p_tfuca_nombre", 3)]
        public string tfuca_nombre { get; set; }

        [SPParameterName("p_tfuca_paterno", 4)]
        public string tfuca_paterno { get; set; }

        [SPParameterName("p_tfuca_materno", 5)]
        public string tfuca_materno { get; set; }

        [SPParameterName("p_tfuca_curp", 6)]
        public string tfuca_curp { get; set; }

        [SPParameterName("p_tfuca_tuser_clave", 7)]
        public string tfuca_tuser_clave { get; set; }

        [SPParameterName("p_tfuca_estatus", 8)]
        public string tfuca_estatus { get; set; }
    }

    [SPName("P_UPD_TORCA")]
    public class ModelUpdtorca : BaseModelRequest
    {

        [SPParameterName("p_torca_clave", 0)]
        public string torca_clave { get; set; }

        [SPParameterName("p_torca_desc", 1)]
        public string torca_desc { get; set; }

        [SPParameterName("p_torca_user", 2)]
        public string torca_user { get; set; }

        [SPParameterName("p_torca_estatus", 3)]
        public string torca_estatus { get; set; }

    }

    [SPName("P_INS_TBANC")]
    public class ModelInstbanc : BaseModelRequest
    {

        [SPParameterName("Clave_tbanc", 0)]
        public string tbanc_clave { get; set; }

        [SPParameterName("Codigo", 2)]
        public string tbanc_tcoco_clave  { get; set; }

        [SPParameterName("Desc_tbanc", 3)]
        public string tbanc_desc { get; set; }

        [SPParameterName("Usuario", 4)]
        public string tbanc_tuser_clave { get; set; }

        [SPParameterName("Estatus", 5)]
        public string tbanc_estatus { get; set; }

        [SPParameterName("Ruta_Logo", 6)]
        public string tbanc_ruta_logo { get; set; }

        [SPParameterName("Convenio", 7)]
        public string tbanc_convenio { get; set; }

    }

    [SPName("P_INS_TSTSO")]
    public class ModelInsTstsc : BaseModelRequest
    {

        [SPParameterName("p_tstso_desc", 0)]
        public string tstso_desc { get; set; }

        [SPParameterName("p_tstso_estatus", 2)]
        public string tstso_estatus { get; set; }

        [SPParameterName("p_tstso_user", 3)]
        public string tstso_user { get; set; }

        [SPParameterName("p_tstso_clave", 4)]
        public string tstso_clave { get; set; }

    }

    public class ModelInsTstscResponse : BaseModelResponse
    {
        [SPResponseColumnName("Existe")]
        public string Existe { get; set; }
    }

    [SPName("P_UPD_TSTSO")]
    public class ModelUpdTstsc : BaseModelRequest
    {

        [SPParameterName("p_tstso_desc", 0)]
        public string tstso_desc { get; set; }

        [SPParameterName("p_tstso_estatus", 2)]
        public string tstso_estatus { get; set; }

        [SPParameterName("p_tstso_user", 3)]
        public string tstso_user { get; set; }

        [SPParameterName("p_tstso_clave", 4)]
        public string tstso_clave { get; set; }

    }

    [SPName("P_UPD_TBANC")]
    public class ModelUpdtbanc : BaseModelRequest
    {

        [SPParameterName("Clave_tbanc", 0)]
        public string tbanc_clave { get; set; }

        [SPParameterName("Desc_tbanc", 1)]
        public string tbanc_desc { get; set; }

        [SPParameterName("Codigo", 2)]
        public string tbanc_tcoco_clave { get; set; }

        [SPParameterName("Usuario", 3)]
        public string tbanc_tuser_clave { get; set; }

        [SPParameterName("Estatus", 4)]
        public string tbanc_estatus { get; set; }

        [SPParameterName("Ruta_Logo", 5)]
        public string tbanc_ruta_logo { get; set; }

        [SPParameterName("Convenio", 6)]
        public string tbanc_convenio { get; set; }


    }

    [SPName("P_QRY_TPAIS")]
    public class ModelTpaisRequest : BaseModelRequest
    {
    }

    public class ModelTpaisResponse : BaseModelResponse
    {
        [SPResponseColumnName("CLAVE")]
        public string clave { get; set; }

        [SPResponseColumnName("NOMBRE")]
        public string nombre { get; set; }

        [SPResponseColumnName("GENTIL")]
        public string gentil { get; set; }

        [SPResponseColumnName("ESTATUS_CODE")]
        public string c_estatus { get; set; }

        [SPResponseColumnName("ESTATUS")]
        public string estatus { get; set; }

        [SPResponseColumnName("FECHA")]
        public string fecha { get; set; }
    }
    
    [SPName("P_INS_TPAIS")]
    public class ModelInsTpaisRequest : BaseModelRequest
    {
        [SPParameterName("p_clave", 0)]
        public string clave { get; set; }
        [SPParameterName("p_desc", 1)]
        public string desc { get; set; }
        [SPParameterName("p_gentil", 2)]
        public string gentil { get; set; }
        [SPParameterName("p_user", 3)]
        public string user { get; set; }
        [SPParameterName("p_estatus", 4)]
        public string estatus { get; set; }
    }

    public class ModelInsTpaisResponse : BaseModelResponse
    {
        
    }

    [SPName("P_UPD_TPAIS")]
    public class ModelUpdTpaisRequest : BaseModelRequest
    {
        [SPParameterName("p_clave", 0)]
        public string clave { get; set; }
        [SPParameterName("p_desc", 1)]
        public string desc { get; set; }
        [SPParameterName("p_gentil", 2)]
        public string gentil { get; set; }
        [SPParameterName("p_user", 3)]
        public string user { get; set; }
        [SPParameterName("p_estatus", 4)]
        public string estatus { get; set; }
    }

    
    [SPName("P_VAL_TPAIS")]
    public class ModelValTpaisRequest : BaseModelRequest
    {
        [SPParameterName("p_clave", 0)]
        public string clave { get; set; }
    }

    public class ModelValTpaisResponse : BaseModelResponse
    {
        [SPResponseColumnName("INDICADOR")]
        public string indicador { get; set; }
    }
    
    #region TESTA (Estados)
    [SPName("P_QRY_TESTA")]
    public class ModelTestaRequest : BaseModelRequest
    {
        [SPParameterName("p_tpais_clave", 0)]
        public string tpais_clave { get; set; }
    }

    public class ModelTestaResponse : BaseModelResponse
    {
        [SPResponseColumnName("CLAVE")]
        public string clave { get; set; }
        [SPResponseColumnName("NOMBRE")]
        public string nombre { get; set; }
        [SPResponseColumnName("ESTATUS")]
        public string estatus { get; set; }
        [SPResponseColumnName("FECHA")]
        public string fecha { get; set; }
    }
    #endregion

    #region TSTSO (Estatus Socioeconómicos / Otros)
    [SPName("P_QRY_TSTSO")]
    public class ModelTstsoRequest : BaseModelRequest
    {
        [SPParameterName("P_Pais", 0)]
        public string pais { get; set; }
    }

    public class ModelTstsoResponse : BaseModelResponse
    {
        // CORREGIDO: Usamos SPResponseColumnName para las respuestas
        [SPResponseColumnName("CLAVE")]
        public string clave { get; set; }
        [SPResponseColumnName("NOMBRE")]
        public string nombre { get; set; }
        [SPResponseColumnName("C_ESTATUS")]
        public string c_estatus { get; set; }
        [SPResponseColumnName("ESTATUS")]
        public string estatus { get; set; }
        [SPResponseColumnName("FECHA")]
        public string fecha { get; set; }
    }
    #endregion

    #region Otros Modelos (Bancos, Direcciones, etc.)
    [SPName("P_INS_TDIRE")]
    public class ModelInstdire : BaseModelRequest
    {
        [SPParameterName("Clave_tdire", 0)]
        public string tdire_clave { get; set; }
        [SPParameterName("Desc_tdire", 2)]
        public string tdire_desc { get; set; }
        [SPParameterName("Usuario", 3)]
        public string tdire_user { get; set; }
        [SPParameterName("Estatus", 4)]
        public string tdire_estatus { get; set; }
    }

    [SPName("P_QRY_TBANC")]
    public class ModeltbancRequest : BaseModelRequest { }

    public class ModeltbancResponse : BaseModelResponse
    {
        [SPResponseColumnName("CLAVE")]
        public string clave { get; set; }
        [SPResponseColumnName("NOMBRE")]
        public string Nombre { get; set; }
        [SPResponseColumnName("CODIGO")]
        public string Codigo { get; set; }
        [SPResponseColumnName("C_ESTATUS")]
        public string C_ESTATUS { get; set; }
        [SPResponseColumnName("ESTATUS")]
        public string ESTATUS { get; set; }
        [SPResponseColumnName("FECHA")]
        public string Fecha { get; set; }
        [SPResponseColumnName("tbanc_ruta_logo")]
        public string ruta_logo { get; set; }
        [SPResponseColumnName("tbanc_convenio")]
        public string convenio { get; set; }
    }

    [SPName("P_INS_TESTA")]
    public class ModelInsTestaRequest : BaseModelRequest
    {
        [SPParameterName("p_clave", 0)]
        public string clave { get; set; }
        
        [SPParameterName("p_pais_clave", 1)]
        public string pais_clave { get; set; }
        
        [SPParameterName("p_desc", 2)]
        public string desc { get; set; }
        
        [SPParameterName("p_user", 3)]
        public string user { get; set; }
        
        [SPParameterName("p_estatus", 4)]
        public string estatus { get; set; }
    }
    
    [SPName("P_UPD_TESTA")]
    public class ModelUpdTestaRequest : BaseModelRequest
    {
        [SPParameterName("p_clave", 0)]
        public string clave { get; set; }
        
        [SPParameterName("p_pais_clave", 1)]
        public string pais_clave { get; set; }
        
        [SPParameterName("p_desc", 2)]
        public string desc { get; set; }
        
        [SPParameterName("p_user", 3)]
        public string user { get; set; }
        
        [SPParameterName("p_estatus", 4)]
        public string estatus { get; set; }
    }

    [SPName("P_VAL_TESTA")]
    public class ModelValTestaRequest : BaseModelRequest
    {
        [SPParameterName("p_clave", 0)]
        public string clave { get; set; }

        [SPParameterName("p_pais", 1)]
        public string pais { get; set; }
    }

    public class ModelValTestaResponse : BaseModelResponse
    {
        [SPResponseColumnName("INDICADOR")]
        public string indicador { get; set; }
    }

    [SPName("P_QRY_TESTA_POR_PAIS")]
    public class ModelTestaComboRequest : BaseModelRequest
    {
        [SPParameterName("p_tpais_clave", 0)]
        public string tpais_clave { get; set; }
    }

    public class ModelTestaComboResponse : BaseModelResponse
    {
        [SPResponseColumnName("CLAVE")]
        public string clave { get; set; }
        [SPResponseColumnName("NOMBRE")]
        public string nombre { get; set; }
    }

    [SPName("P_QRY_TDELE")]
    public class ModelTdeleRequest : BaseModelRequest
    {
        [SPParameterName("p_tpais_clave", 0)]
        public string tpais_clave { get; set; }

        [SPParameterName("p_testa_clave", 1)]
        public string testa_clave { get; set; }
    }

    public class ModelTdeleResponse : BaseModelResponse
    {
        [SPResponseColumnName("CLAVE")]
        public string clave { get; set; }
        [SPResponseColumnName("NOMBRE")]
        public string nombre { get; set; }
        [SPResponseColumnName("ESTATUS")]
        public string estatus { get; set; }
        [SPResponseColumnName("FECHA")]
        public string fecha { get; set; }
    }

    [SPName("P_INS_TDELE")]
    public class ModelInsTdeleRequest : BaseModelRequest
    {
        [SPParameterName("p_clave", 0)]
        public string clave { get; set; }
        
        [SPParameterName("p_desc", 1)]
        public string desc { get; set; }
        
        [SPParameterName("p_estado_clave", 2)]
        public string estado_clave { get; set; }
        
        [SPParameterName("p_pais_clave", 3)]
        public string pais_clave { get; set; }
        
        [SPParameterName("p_user", 4)]
        public string user { get; set; }
        
        [SPParameterName("p_estatus", 5)]
        public string estatus { get; set; }
    }

    [SPName("P_UPD_TDELE")]
    public class ModelUpdTdeleRequest : BaseModelRequest
    {
        [SPParameterName("p_clave", 0)]
        public string clave { get; set; }
        
        [SPParameterName("p_desc", 1)]
        public string desc { get; set; }
        
        [SPParameterName("p_estado_clave", 2)]
        public string estado_clave { get; set; }
        
        [SPParameterName("p_pais_clave", 3)]
        public string pais_clave { get; set; }
        
        [SPParameterName("p_user", 4)]
        public string user { get; set; }
        
        [SPParameterName("p_estatus", 5)]
        public string estatus { get; set; }
    }

    [SPName("P_VAL_TDELE")]
    public class ModelValTdeleRequest : BaseModelRequest
    {
        [SPParameterName("p_clave", 0)]
        public string clave { get; set; }

        [SPParameterName("p_estado", 1)]
        public string estado { get; set; }

        [SPParameterName("p_pais", 2)]
        public string pais { get; set; }
    }

    public class ModelValTdeleResponse : BaseModelResponse
    {
        [SPResponseColumnName("INDICADOR")]
        public string indicador { get; set; }
    }

    [SPName("P_QRY_TDELE_COMBO")]
    public class ModelTdeleComboRequest : BaseModelRequest
    {
        [SPParameterName("p_pais", 0)]
        public string pais { get; set; }
        [SPParameterName("p_estado", 1)]
        public string estado { get; set; }
    }

    public class ModelTdeleComboResponse : BaseModelResponse
    {
        [SPResponseColumnName("CLAVE")]
        public string clave { get; set; }
        [SPResponseColumnName("NOMBRE")]
        public string nombre { get; set; }
    }

    [SPName("P_QRY_TZIPS")]
    public class ModelTzipRequest : BaseModelRequest
    {
        [SPParameterName("p_tpais", 0)]
        public string tpais { get; set; }
        [SPParameterName("p_testa", 1)]
        public string testa { get; set; }
        [SPParameterName("p_tdele", 2)]
        public string tdele { get; set; }
    }

    public class ModelTzipResponse : BaseModelResponse
    {
        [SPResponseColumnName("CLAVE")] public string clave { get; set; }
        [SPResponseColumnName("NOMBRE")] public string nombre { get; set; }
        [SPResponseColumnName("C_PAIS")] public string c_pais { get; set; }
        [SPResponseColumnName("PAIS")] public string pais { get; set; }
        [SPResponseColumnName("C_ESTADO")] public string c_estado { get; set; }
        [SPResponseColumnName("ESTADO")] public string estado { get; set; }
        [SPResponseColumnName("C_DELEGACION")] public string c_delegacion { get; set; }
        [SPResponseColumnName("DELEGACION")] public string delegacion { get; set; }
        [SPResponseColumnName("ESTATUS_CODE")] public string estatus_code { get; set; }
        [SPResponseColumnName("ESTATUS")] public string estatus { get; set; }
        [SPResponseColumnName("FECHA")] public string fecha { get; set; }
    }

    [SPName("P_INS_TZIP")]
    public class ModelInsTzipRequest : BaseModelRequest
    {
        [SPParameterName("p_clave", 0)] public string clave { get; set; }
        [SPParameterName("p_desc", 1)] public string desc { get; set; }
        [SPParameterName("p_pais", 2)] public string pais { get; set; }
        [SPParameterName("p_estado", 3)] public string estado { get; set; }
        [SPParameterName("p_dele", 4)] public string dele { get; set; }
        [SPParameterName("p_user", 5)] public string user { get; set; }
        [SPParameterName("p_estatus", 6)] public string estatus { get; set; }
    }

    [SPName("P_UPD_TZIP")]
    public class ModelUpdTzipRequest : BaseModelRequest
    {
        [SPParameterName("p_clave", 0)] public string clave { get; set; }
        [SPParameterName("p_desc", 1)] public string desc { get; set; }
        [SPParameterName("p_pais", 2)] public string pais { get; set; }
        [SPParameterName("p_estado", 3)] public string estado { get; set; }
        [SPParameterName("p_dele", 4)] public string dele { get; set; }
        [SPParameterName("p_user", 5)] public string user { get; set; }
        [SPParameterName("p_estatus", 6)] public string estatus { get; set; }
    }

    [SPName("P_VAL_TZIP")]
    public class ModelValTzipRequest : BaseModelRequest
    {
        [SPParameterName("p_clave", 0)] public string clave { get; set; }
        [SPParameterName("p_pais", 1)] public string pais { get; set; }
        [SPParameterName("p_estado", 2)] public string estado { get; set; }
        [SPParameterName("p_dele", 3)] public string dele { get; set; }
    }

    public class ModelValTzipResponse : BaseModelResponse
    {
        [SPResponseColumnName("INDICADOR")]
        public string indicador { get; set; }
    }

    public class ModelValCampusRequest
    {
        public string clave { get; set; }
    }

    public class ModelValCampusResponse
    {
        public string indicador { get; set; }
    }


    public class ModelInsCampusRequest
    {
        public string p_clave { get; set; }
        public string p_nombre { get; set; }
        public string p_direccion { get; set; }
        public string p_colonia { get; set; }
        public string p_pais { get; set; }
        public string p_estado { get; set; }
        public string p_dele { get; set; }
        public string p_zip { get; set; }
        public string p_user { get; set; }
        public string p_estatus { get; set; }
        public string p_abr { get; set; }
        public string p_rfc { get; set; }
    }
        
    public class ModelInsProgCampusRequest
    {
        public string p_campus { get; set; }
        public string p_programa { get; set; }
        public string p_admision { get; set; }
        public string p_user { get; set; }
        public string p_estatus { get; set; }
    }
        
    public class ModelInsCobranzaRequest
    {
        public string p_periodo { get; set; }
        public string p_campus { get; set; }
        public string p_nivel { get; set; }
        public string p_tipo_per { get; set; }
        public decimal p_desc_ins { get; set; }
        public decimal p_desc_col { get; set; }
        public string p_conc_cal { get; set; }
        public string p_concepto { get; set; }
        public string p_user { get; set; }
    }
    
    #endregion

}