using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAES_DBO.Models
{
    [SPName("P_OPCION_TITULACION")]
    public class ModelOpcionTitulacionRequest : BaseModelRequest
    {
        [Required]

        [SPParameterName("P_NADA", 0)]
        public string Nada { get; set; }
    }
    public class ModelOpcionTitulacionResponse : BaseModelResponse
    {
        [SPResponseColumnName("CLAVE")]
        public string Clave { get; set; }
        [SPResponseColumnName("DESCRIPCION")]
        public string Descripcion { get; set; }
        [SPResponseColumnName("ESTATUS")]
        public string Estatus { get; set; }
        [SPResponseColumnName("USUARIO")]
        public string Usuario { get; set; }
        [SPResponseColumnName("FECHAREGISTRO")]
        public string FechaRegistro
        {
            get;set;
        }
    }


    [SPName("P_QRY_TTINI")]
    public class ModelTtiniRequest : BaseModelRequest
    {
        [SPParameterName("p_ttini_ttiop_clave", 0)]
        public string ttini_ttiop_clave { get; set; }
    }
    public class ModelTtiniResponse : BaseModelResponse
    {
        [SPResponseColumnName("clave")]
        public string Clave { get; set; }

        [SPResponseColumnName("ttini_tnive_clave")]
        public string ttini_tnive_clave { get; set; }

        [SPResponseColumnName("ttini_creditos")]
        public string ttini_creditos { get; set; }

        [SPResponseColumnName("ttini_promedio")]
        public string ttini_promedio { get; set; }

        [SPResponseColumnName("ttini_tcoco_clave")]
        public string ttini_tcoco_clave { get; set; }

        [SPResponseColumnName("ttini_tuser_clave")]
        public string ttini_tuser_clave { get; set; }

        [SPResponseColumnName("ttini_date")]
        public string ttini_date { get; set; }

        [SPResponseColumnName("descripcion")]
        public string descripcion { get; set; }

        [SPResponseColumnName("nivel")]
        public string nivel { get; set; }

        [SPResponseColumnName("tipo")]
        public string tipo { get; set; }

        [SPResponseColumnName("ttini_tseso")]
        public string ttini_tseso { get; set; }
    }


    [SPName("P_QRY_TPEES_TRETI")]
    public class ModelObtenPeriodosTitulacionRequest : BaseModelRequest
    {

    }
    public class ModelObtenPeriodosTitulacionResponse : BaseModelResponse
    {
        [SPResponseColumnName("clave")]
        public string clave { get; set; }

        [SPResponseColumnName("nombre")]
        public string nombre { get; set; }
    }



    [SPName("P_QRY_TCAMP_TRETI")]
    public class ModelObtenCampusTitulacionRequest : BaseModelRequest
    {

    }
    public class ModelObtenCampusTitulacionResponse : BaseModelResponse
    {
        [SPResponseColumnName("Clave")]
        public string Clave { get; set; }

        [SPResponseColumnName("Nombre")]
        public string Descripcion { get; set; }
    }



    [SPName("P_QRY_TRETI")]
    public class ModelRegTitulacionRequest : BaseModelRequest
    {
        [Required]

        [SPParameterName("P_Matricula", 0)]
        public string Matricula { get; set; }

        [SPParameterName("P_Programa", 0)]
        public string Programa { get; set; }
    }
    public class ModelRegTitulacionResponse : BaseModelResponse
    {
        [SPResponseColumnName("treti_ttiop_clave")]
        public string treti_ttiop_clave { get; set; }

        [SPResponseColumnName("ttiop_desc")]
        public string ttiop_desc { get; set; }

        [SPResponseColumnName("treti_status")]
        public string treti_status { get; set; }

        [SPResponseColumnName("treti_status_desc")]
        public string treti_status_desc { get; set; }

        [SPResponseColumnName("fecha")]
        public string fecha { get; set; }

        [SPResponseColumnName("treti_tuser_clave")]
        public string treti_tuser_clave { get; set; }

        [SPResponseColumnName("treti_foja")]
        public string treti_foja { get; set; }

        [SPResponseColumnName("treti_libro")]
        public string treti_libro { get; set; }

        [SPResponseColumnName("treti_cedula")]
        public string treti_cedula { get; set; }

        [SPResponseColumnName("treti_tpees_clave")]
        public string treti_tpees_clave { get; set; }
    }


    [SPName("P_QRY_TRTIT")]
    public class ModelRegTitulacionDetRequest : BaseModelRequest
    {
        [Required]

        [SPParameterName("P_Periodo", 0)]
        public string Periodo { get; set; }

        [SPParameterName("P_Campus", 1)]
        public string Campus { get; set; }

        [SPParameterName("P_Programa", 2)]
        public string Programa { get; set; }

        [SPParameterName("P_Nivel", 3)]
        public string Nivel { get; set; }

        [SPParameterName("P_Opc_Titulacion", 4)]
        public string Opc_Titulacion { get; set; }

        [SPParameterName("P_Estatus", 5)]
        public string Estatus { get; set; }

    }
    public class ModelRegTitulacionDetResponse : BaseModelResponse
    {
        [SPResponseColumnName("treti_tpers_num")]
        public string treti_tpers_num { get; set; }

        [SPResponseColumnName("tpers_id")]
        public string tpers_id { get; set; }

        [SPResponseColumnName("nombre")]
        public string nombre { get; set; }

        [SPResponseColumnName("treti_tpees_clave")]
        public string treti_tpees_clave { get; set; }

        [SPResponseColumnName("tcamp_desc")]
        public string tcamp_desc { get; set; }

        [SPResponseColumnName("tnive_desc")]
        public string tnive_desc { get; set; }

        [SPResponseColumnName("tprog_clave")]
        public string tprog_clave { get; set; }

        [SPResponseColumnName("ttiop_desc")]
        public string ttiop_desc { get; set; }

        [SPResponseColumnName("treti_status")]
        public string treti_status { get; set; }

        [SPResponseColumnName("treti_foja")]
        public string treti_foja { get; set; }

        [SPResponseColumnName("treti_libro")]
        public string treti_libro { get; set; }

        [SPResponseColumnName("treti_cedula")]
        public string treti_cedula { get; set; }

        [SPResponseColumnName("treti_fecha_estatus")]
        public string treti_fecha_estatus { get; set; }
    }


    [SPName("P_INS_TRETI")]
    public class ModelInsTreti : BaseModelRequest
    {
        [SPParameterName("P_Matricula", 0)]
        public string matricula { get; set; }

        [SPParameterName("p_treti_tprog_clave", 1)]
        public string treti_tprog_clave { get; set; }

        [SPParameterName("p_treti_ttiop_clave", 2)]
        public string treti_ttiop_clave { get; set; }

        [SPParameterName("p_treti_status", 3)]
        public string treti_status { get; set; }        

        [SPParameterName("p_treti_tuser_clave", 4)]
        public string treti_tuser_clave { get; set; }       

        [SPParameterName("p_treti_foja", 5)]
        public string treti_foja { get; set; }

        [SPParameterName("p_treti_libro", 6)]
        public string treti_libro { get; set; }

        [SPParameterName("p_treti_cedula", 7)]
        public string treti_cedula { get; set; }

        [SPParameterName("p_nivel", 8)]
        public string nivel { get; set; }

        [SPParameterName("p_periodo", 9)]
        public string periodo { get; set; }

        [SPParameterName("p_campus", 10)]
        public string campus { get; set; }
    }
    public class ModelInsTretiResponse : BaseModelResponse
    {
        [SPResponseColumnName("Existe")]
        public string Existe { get; set; }

        [SPResponseColumnName("TotCreditos")]
        public string TotCreditos { get; set; }


        [SPResponseColumnName("Promedio")]
        public string Promedio { get; set; }


        [SPResponseColumnName("PromedioRequerido")]
        public string PromedioRequerido { get; set; }


        [SPResponseColumnName("TotCreditosCubiertos")]
        public string TotCreditosCubiertos { get; set; }

    }


    [SPName("P_UPD_TRETI")]
    public class ModelUpdTreti : BaseModelRequest
    {
        [SPParameterName("P_Matricula", 0)]
        public string matricula { get; set; }

        [SPParameterName("p_treti_tprog_clave", 1)]
        public string treti_tprog_clave { get; set; }

        [SPParameterName("p_treti_ttiop_clave", 2)]
        public string treti_ttiop_clave { get; set; }

        [SPParameterName("p_treti_status", 3)]
        public string treti_status { get; set; }

        [SPParameterName("p_treti_tuser_clave", 4)]
        public string treti_tuser_clave { get; set; }

        [SPParameterName("p_treti_foja", 5)]
        public string treti_foja { get; set; }

        [SPParameterName("p_treti_libro", 6)]
        public string treti_libro { get; set; }

        [SPParameterName("p_treti_cedula", 7)]
        public string treti_cedula { get; set; }

        [SPParameterName("p_treti_tpees_clave", 8)]
        public string treti_tpees_clave { get; set; }
    }

    [SPName("P_TITULACION_NIVE")]
    public class ModelOpcionTitulacionDetRequest : BaseModelRequest
    {
        [Required]
        [SPParameterName("P_OPTITU", 0)]
        public string Clave { get; set; }
    }
    public class ModelOpcionTitulacionDetResponse : BaseModelResponse
    {
        [SPResponseColumnName("NIVEL")]
        public string Nivel { get; set; }
        [SPResponseColumnName("CREDITOS")]
        public string Creditos { get; set; }
        [SPResponseColumnName("PROMEDIO")]
        public string Promedio { get; set; }
        [SPResponseColumnName("CODIGO")]
        public string Codigo { get; set; }
    }

    [SPName("P_NIVEL_GENERAL")]
    public class ModelOpcionTitulacionNivelRequest : BaseModelRequest
    {
        [Required]
        [SPParameterName("P_NADA", 0)]
        public string Nada { get; set; }
    }
    public class ModelOpcionTitulacionNivelResponse : BaseModelResponse
    {
        [SPResponseColumnName("CLAVE")]
        public string Clave { get; set; }
        [SPResponseColumnName("DESCRIPCION")]
        public string Descripcion { get; set; }
    }

    [SPName("P_CODIGO_COCO")]
    public class ModelOpcionTitulacionCodigoRequest : BaseModelRequest
    {
        [Required]
        [SPParameterName("P_NADA", 0)]
        public string Nada { get; set; }
    }
    public class ModelOpcionTitulacionCodigoResponse : BaseModelResponse
    {
        [SPResponseColumnName("CLAVE")]
        public string Clave { get; set; }
        [SPResponseColumnName("DESCRIPCION")]
        public string Descripcion { get; set; }
    }

    [SPName("P_BORRAR_OPTI")]
    public class ModelBorraOpcionTitulacionResponse : BaseModelRequest
    {
        [Required]
        [SPParameterName("P_CLAVE_OPTI", 0)]
        public string Clave { get; set; }
    }

    [SPName("P_INSERTA_OPTI")]
    public class ModelInsertaOpcionTitulacionResponse : BaseModelRequest
    {
        [Required]
        [SPParameterName("P_CLAVE_OPTI", 0)]
        public string Clave { get; set; }
        [SPParameterName("P_DESC", 1)]
        public string Descripcion { get; set; }
        [SPParameterName("P_ESTATUS", 2)]
        public string Estatus { get; set; }
        [SPParameterName("P_USER", 3)]
        public string Usuario { get; set; }
        [SPParameterName("P_NIVEL", 4)]
        public string Nivel { get; set; }
        [SPParameterName("P_CREDITOS", 5)]
        public string Creditos { get; set; }
        [SPParameterName("P_PROMEDIO", 6)]
        public string Promedio { get; set; }
        [SPParameterName("P_CLAVE_COCO", 7)]
        public string ClaveCodigo { get; set; }
    }

    [SPName("P_INSERTA_TTINI")]
    public class ModelInsertaDetalleOpcionTitulacionResponse : BaseModelRequest
    {
        [Required]
        [SPParameterName("P_CLAVE_OPTI", 0)]
        public string Clave { get; set; }        
        [SPParameterName("P_USER", 1)]
        public string Usuario { get; set; }
        [SPParameterName("P_NIVEL", 2)]
        public string Nivel { get; set; }
        [SPParameterName("P_CREDITOS", 3)]
        public string Creditos { get; set; }
        [SPParameterName("P_PROMEDIO", 4)]
        public string Promedio { get; set; }
        [SPParameterName("P_CLAVE_COCO", 5)]
        public string ClaveCodigo { get; set; }
    }
}
