using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAES_DBO.Models
{
    public class ModelBanco
    {
        [SPName("P_INS_TCOBA")]//Insertar en tabla tcoba
        public class ModelInsertarTcoba : BaseModelRequest
        {

            [SPParameterName("p_tcoba_tbanc_clave", 0)]
            public string tcoba_tbanc_clave { get; set; }

            [SPParameterName("p_tcoba_ind", 1)]
            public string tcoba_ind { get; set; }

            [SPParameterName("p_tcoba_tcoco_clave", 2)]
            public string tcoba_tcoco_clave { get; set; }

            [SPParameterName("p_tcoba_tpers_inicio", 3)]
            public string tcoba_tpers_inicio { get; set; }

            [SPParameterName("p_tcoba_tpers_fin", 4)]
            public string tcoba_tpers_fin { get; set; }

            [SPParameterName("p_tcoba_tran_inicio", 5)]
            public string tcoba_tran_inicio { get; set; }

            [SPParameterName("p_tcoba_tran_fin", 6)]
            public string tcoba_tran_fin { get; set; }

            [SPParameterName("p_tcoba_fecha_ini", 7)]
            public string tcoba_fecha_ini { get; set; }

            [SPParameterName("p_tcoba_fecha_fin", 8)]
            public string tcoba_fecha_fin { get; set; }

            [SPParameterName("p_tcoba_imp_inicio", 9)]
            public string tcoba_imp_inicio { get; set; }

            [SPParameterName("p_tcoba_imp_fin", 10)]
            public string tcoba_imp_fin { get; set; }

            [SPParameterName("p_tcoba_tuser_clave", 11)]
            public string tcoba_tuser_clave { get; set; }

            [SPParameterName("p_tcoba_estatus", 12)]
            public string tcoba_estatus { get; set; }

            [SPParameterName("p_tcoba_referencia", 13)]
            public string tcoba_referencia { get; set; }
        }

        [SPName("P_UPD_TCOBA")]//Actualizar tabla tcoba
        public class ModelEditarTcoba : BaseModelRequest
        {

            [SPParameterName("p_tcoba_tbanc_clave", 0)]
            public string tcoba_tbanc_clave { get; set; }

            [SPParameterName("p_tcoba_ind", 1)]
            public string tcoba_ind { get; set; }

            [SPParameterName("p_tcoba_tcoco_clave", 2)]
            public string tcoba_tcoco_clave { get; set; }

            [SPParameterName("p_tcoba_tpers_inicio", 3)]
            public string tcoba_tpers_inicio { get; set; }

            [SPParameterName("p_tcoba_tpers_fin", 4)]
            public string tcoba_tpers_fin { get; set; }

            [SPParameterName("p_tcoba_tran_inicio", 5)]
            public string tcoba_tran_inicio { get; set; }

            [SPParameterName("p_tcoba_tran_fin", 6)]
            public string tcoba_tran_fin { get; set; }

            [SPParameterName("p_tcoba_fecha_ini", 7)]
            public string tcoba_fecha_ini { get; set; }

            [SPParameterName("p_tcoba_fecha_fin", 8)]
            public string tcoba_fecha_fin { get; set; }

            [SPParameterName("p_tcoba_imp_inicio", 9)]
            public string tcoba_imp_inicio { get; set; }

            [SPParameterName("p_tcoba_imp_fin", 10)]
            public string tcoba_imp_fin { get; set; }

            [SPParameterName("p_tcoba_tuser_clave", 11)]
            public string tcoba_tuser_clave { get; set; }

            [SPParameterName("p_tcoba_estatus", 12)]
            public string tcoba_estatus { get; set; }

            [SPParameterName("p_tcoba_referencia", 13)]
            public string tcoba_referencia { get; set; }

        }     

        public class ModelInsertarTcobaResponse : BaseModelResponse
        {
            [SPResponseColumnName("Existe")]
            public string Existe { get; set; }
        }

        [SPName("P_QRY_TCOBA")]
        public class ModelObtenerTcobaRequest : BaseModelRequest
        {

        }
        public class ModelObtenerTcobaResponse : BaseModelResponse
        {
            [SPResponseColumnName("tbanc_desc")]
            public string tbanc_desc { get; set; }

            [SPResponseColumnName("tcoba_tbanc_clave")]
            public string tcoba_tbanc_clave { get; set; }

            [SPResponseColumnName("tcoba_ind_desc")]
            public string tcoba_ind_desc { get; set; }

            [SPResponseColumnName("tcoba_estatus_desc")]
            public string tcoba_estatus_desc { get; set; }

            [SPResponseColumnName("tcoba_ind")]
            public string tcoba_ind { get; set; }

            [SPResponseColumnName("tcoba_estatus")]
            public string tcoba_estatus { get; set; }

            [SPResponseColumnName("tcoba_date")]
            public string tcoba_date { get; set; }

            [SPResponseColumnName("tcoba_tcoco_clave")]
            public string tcoba_tcoco_clave { get; set; }

            [SPResponseColumnName("tcoba_tpers_inicio")]
            public string tcoba_tpers_inicio { get; set; }

            [SPResponseColumnName("tcoba_tpers_fin")]
            public string tcoba_tpers_fin { get; set; }

            [SPResponseColumnName("tcoba_tran_inicio")]
            public string tcoba_tran_inicio { get; set; }

            [SPResponseColumnName("tcoba_tran_fin")]
            public string tcoba_tran_fin { get; set; }

            [SPResponseColumnName("tcoba_fecha_ini")]
            public string tcoba_fecha_ini { get; set; }

            [SPResponseColumnName("tcoba_fecha_fin")]
            public string tcoba_fecha_fin { get; set; }

            [SPResponseColumnName("tbanc_clave")]
            public string tbanc_clave { get; set; }

            [SPResponseColumnName("tcoba_imp_inicio")]
            public string tcoba_imp_inicio { get; set; }

            [SPResponseColumnName("tcoba_imp_fin")]
            public string tcoba_imp_fin { get; set; }

            [SPResponseColumnName("tcoba_referencia")]
            public string tcoba_referencia { get; set; }
        }

        [SPName("P_QRY_CONFIGURACION_TCOBA")]
        public class ModelObtenerConfTcobaRequest : BaseModelRequest
        {
            [SPParameterName("p_tcoba_tbanc_clave", 0)]
            public string tcoba_tbanc_clave { get; set; }

            [SPParameterName("p_tcoba_ind", 1)]
            public string tcoba_ind { get; set; }
        }
        public class ModelObtenerConfTcobaResponse : BaseModelResponse
        {
            [SPResponseColumnName("tcoba_tpers_inicio")]
            public string tcoba_tpers_inicio { get; set; }

            [SPResponseColumnName("tcoba_tpers_fin")]
            public string tcoba_tpers_fin { get; set; }

            [SPResponseColumnName("tcoba_tran_inicio")]
            public string tcoba_tran_inicio { get; set; }

            [SPResponseColumnName("tcoba_tran_fin")]
            public string tcoba_tran_fin { get; set; }

            [SPResponseColumnName("tcoba_fecha_ini")]
            public string tcoba_fecha_ini { get; set; }

            [SPResponseColumnName("tcoba_fecha_fin")]
            public string tcoba_fecha_fin { get; set; }

            [SPResponseColumnName("tcoba_imp_inicio")]
            public string tcoba_imp_inicio { get; set; }

            [SPResponseColumnName("tcoba_imp_fin")]
            public string tcoba_imp_fin { get; set; }

            [SPResponseColumnName("tcoba_referencia")]
            public string tcoba_referencia { get; set; }
        }


        [SPName("P_QRY_TAPBA")]
        public class ModelObtenerTapbaRequest : BaseModelRequest
        {
            [SPParameterName("p_tapba_tbanc_clave", 0)]
            public string tapba_tbanc_clave { get; set; }

            [SPParameterName("p_tapba_tcoba_ind", 1)]
            public string tapba_tcoba_ind { get; set; }

            [SPParameterName("p_tapba_carga_date", 2)]
            public string tapba_carga_date { get; set; }
        }
        public class ModelObtenerTapbaResponse : BaseModelResponse
        {
            [SPResponseColumnName("tapba_tpers_num")]
            public string tapba_tpers_num { get; set; }

            [SPResponseColumnName("tpers_nombre")]
            public string tpers_nombre { get; set; }

            [SPResponseColumnName("tapba_consecutivo")]
            public string tapba_consecutivo { get; set; }

            [SPResponseColumnName("tapba_fecha_pago")]
            public string tapba_fecha_pago { get; set; }

            [SPResponseColumnName("tapba_importe")]
            public string tapba_importe { get; set; }

            [SPResponseColumnName("tapba_comentario")]
            public string tapba_comentario { get; set; }
        }

        [SPName("P_QRY_LENGTH_MAX_LAYOUT")]
        public class ModelObtenerLengthMaxLayoutRequest : BaseModelRequest
        {
            [SPParameterName("p_tcoba_tbanc_clave", 0)]
            public string tcoba_tbanc_clave { get; set; }

            [SPParameterName("p_tcoba_ind", 1)]
            public string tcoba_ind { get; set; }
        }
        public class ModelObtenerLengthMaxLayoutResponse : BaseModelResponse
        {
            [SPResponseColumnName("fila_max")]
            public string fila_max { get; set; }
        }


        [SPName("P_QRY_VAL_TAPBA")]
        public class ModelValidaLayoutRequest : BaseModelRequest
        {
            [SPParameterName("p_tapba_tbanc_clave", 0)]
            public string tapba_tbanc_clave { get; set; }

            [SPParameterName("p_tapba_carga_date", 1)]
            public string tapba_carga_date { get; set; }

            [SPParameterName("p_tapba_tcoba_ind", 2)]
            public string tapba_tcoba_ind { get; set; }
        }
        public class ModelValidaLayoutResponse : BaseModelResponse
        {
            [SPResponseColumnName("Valido")]
            public string Valido { get; set; }

            //[SPResponseColumnName("Fecha_Pago")]
            //public string Fecha_Pago { get; set; }
        }

        [SPName("P_INS_TAPBA")]//Insertar tapba
        public class ModelInstapba : BaseModelRequest
        {

            [SPParameterName("p_tapba_tbanc_clave", 0)]
            public string tapba_tbanc_clave { get; set; }         

            [SPParameterName("p_tapba_carga_date", 1)]
            public string tapba_carga_date { get; set; }

            [SPParameterName("p_tapba_consecutivo", 2)]
            public string tapba_consecutivo { get; set; }

            [SPParameterName("p_tapba_tpers_num", 3)]
            public string tapba_tpers_num { get; set; }

            [SPParameterName("p_tapba_referencia", 4)]
            public string tapba_referencia { get; set; }

            [SPParameterName("p_tapba_importe", 5)]
            public string tapba_importe { get; set; }

            [SPParameterName("p_tapba_fecha_pago", 6)]
            public string tapba_fecha_pago { get; set; }

            [SPParameterName("p_tapba_estatus", 7)]
            public string tapba_estatus { get; set; }

            [SPParameterName("p_tapba_comentario", 8)]
            public string tapba_comentario { get; set; }

            [SPParameterName("p_tapba_tuser_clave", 9)]
            public string tapba_tuser_clave { get; set; }

            [SPParameterName("p_tapba_tcoba_ind", 10)]
            public string tapba_tcoba_ind { get; set; }
        }

        [SPName("P_DEL_TAPBA")]//Insertar tapba
        public class ModelDeltapba : BaseModelRequest
        {

            [SPParameterName("p_tapba_tbanc_clave", 0)]
            public string tapba_tbanc_clave { get; set; }

            [SPParameterName("p_tapba_carga_date", 1)]
            public string tapba_carga_date { get; set; }

            [SPParameterName("p_tapba_tcoba_ind", 2)]
            public string tapba_tcoba_ind { get; set; }
        }


        public class ModelObtenerLayout
        {
            public string tapba_tbanc_clave { get; set; }
            public string tapba_carga_date { get; set; }
            public string tapba_consecutivo { get; set; }
            public string tapba_tpers_num { get; set; }
            public string tapba_referencia { get; set; }
            public decimal tapba_importe { get; set; }
            public string tapba_fecha_pago { get; set; }
            public string tapba_estatus { get; set; }
            public string tapba_comentario { get; set; }
            public string tapba_tuser_clave { get; set; }
            public string tapba_date { get; set; }
            public string tapba_observaciones { get; set; }
            public string tapba_tcoba_ind { get; set; }
        }


        [SPName("P_APLICA_PAGOS")]//Actualizar tabla tcoba
        public class ModelAplicarPagos : BaseModelRequest
        {

            [SPParameterName("p_tapba_tbanc_clave", 0)]
            public string tapba_tbanc_clave { get; set; }

            [SPParameterName("p_tapba_carga_date", 1)]
            public string tapba_carga_date { get; set; }

            [SPParameterName("p_tapba_tcoba_ind", 2)]
            public string tapba_tcoba_ind { get; set; }

            [SPParameterName("p_usuario", 3)]
            public string usuario { get; set; }
        }
    }
}
