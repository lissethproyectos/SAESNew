using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAES_DBO.Models
{






    [SPName("P_QRY_TSTSO")]
    public class ModelTstsoRequest : BaseModelRequest
    {
        [SPParameterName("P_Pais", 0)]
        public string pais { get; set; }

    }
    public class ModelTstsoResponse : BaseModelResponse
    {
        [SPParameterName("clave", 0)]
        public string clave { get; set; }

        [SPParameterName("nombre", 1)]
        public string nombre { get; set; }

        [SPParameterName("c_estatus", 2)]
        public string c_estatus { get; set; }

        [SPParameterName("estatus", 3)]
        public string estatus { get; set; }

        [SPParameterName("fecha", 4)]
        public string fecha { get; set; }
    }

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

    [SPName("P_QRY_TBANC")]
    public class ModeltbancRequest : BaseModelRequest
    {

    }

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

}

