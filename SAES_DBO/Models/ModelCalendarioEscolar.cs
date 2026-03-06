using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAES_DBO.Models
{
    public class ModelCalendarioEscolar
    {

        [SPName("P_QRY_TCOCA")]//Tipo Bajas
        public class ModelObtenTcocaRequest : BaseModelRequest
        {
            
        }
        public class ModelObtenTcocaResponse : BaseModelResponse
        {
            [SPResponseColumnName("clave")]
            public string clave { get; set; }

            [SPResponseColumnName("nombre")]
            public string nombre { get; set; }

            [SPResponseColumnName("c_estatus")]
            public string c_estatus { get; set; }

            [SPResponseColumnName("Estatus")]
            public string Estatus { get; set; }

            [SPResponseColumnName("fecha")]
            public string fecha { get; set; }
        }

        [SPName("P_QRY_CALENDARIO_ESCOLAR")]//Tipo Bajas
        public class ModelObtenCalendarioRequest : BaseModelRequest
        {
            [SPParameterName("p_tpees_clave", 0)]
            public string tpees_clave { get; set; }

            [SPParameterName("p_tcaes_tcamp_clave", 1)]
            public string tcaes_tcamp_clave { get; set; }

            [SPParameterName("p_tcaes_tnive_clave", 2)]
            public string tcaes_tnive_clave { get; set; }
        }
        public class ModelObtenCalendarioResponse : BaseModelResponse
        {
            [SPResponseColumnName("clave")]
            public string clave { get; set; }

            [SPResponseColumnName("nombre")]
            public string nombre { get; set; }

            [SPResponseColumnName("Fecha_I")]
            public string Fecha_I { get; set; }

            [SPResponseColumnName("Fecha_F")]
            public string Fecha_F { get; set; }

            [SPResponseColumnName("c_estatus")]
            public string c_estatus { get; set; }

            [SPResponseColumnName("Estatus")]
            public string Estatus { get; set; }

            [SPResponseColumnName("fecha")]
            public string fecha { get; set; }

        }

        [SPName("P_INS_TCOCA")]//Insertar Tcoca
        public class ModelInsertarTcoca : BaseModelRequest
        {

            [SPParameterName("p_tcoca_clave", 0)]
            public string tcoca_clave { get; set; }

            [SPParameterName("p_tcoca_desc", 1)]
            public string tcoca_desc { get; set; }

            [SPParameterName("p_tcoca_tuser_clave", 2)]
            public string tcoca_tuser_clave { get; set; }

            [SPParameterName("p_tcoca_estatus", 3)]
            public string tespr_estatus { get; set; }
        }
        public class ModelInsertarTcocaResponse : BaseModelResponse
        {
            [SPResponseColumnName("Existe")]
            public string Existe { get; set; }
        }

        [SPName("P_INS_TCAES")]//Insertar Tcoca
        public class ModelInsertarTcaes : BaseModelRequest
        {

            [SPParameterName("p_tcaes_tpees_clave", 0)]
            public string tcaes_tpees_clave { get; set; }

            [SPParameterName("p_tcaes_tcamp_clave", 1)]
            public string tcaes_tcamp_clave { get; set; }

            [SPParameterName("p_tcaes_tnive_clave", 2)]
            public string tcaes_tnive_clave { get; set; }

            [SPParameterName("p_tcaes_tcoca_clave", 3)]
            public string tcaes_tcoca_clave { get; set; }

            [SPParameterName("p_tcaes_inicio", 4)]
            public string tcaes_inicio { get; set; }

            [SPParameterName("p_tcaes_fin", 5)]
            public string tcaes_fin { get; set; }

            [SPParameterName("p_tcaes_tuser_clave", 6)]
            public string tcaes_tuser_clave { get; set; }

            [SPParameterName("p_tcaes_estatus", 7)]
            public string tcaes_estatus { get; set; }

        }


        [SPName("P_UPD_TCAES")]//Insertar Tcoca
        public class ModelEditarTcaes : BaseModelRequest
        {

            [SPParameterName("p_tcaes_tpees_clave", 0)]
            public string tcaes_tpees_clave { get; set; }

            [SPParameterName("p_tcaes_tcamp_clave", 1)]
            public string tcaes_tcamp_clave { get; set; }

            [SPParameterName("p_tcaes_tnive_clave", 2)]
            public string tcaes_tnive_clave { get; set; }

            [SPParameterName("p_tcaes_tcoca_clave", 3)]
            public string tcaes_tcoca_clave { get; set; }

            [SPParameterName("p_tcaes_inicio", 4)]
            public string tcaes_inicio { get; set; }

            [SPParameterName("p_tcaes_fin", 5)]
            public string tcaes_fin { get; set; }

            [SPParameterName("p_tcaes_tuser_clave", 6)]
            public string tcaes_tuser_clave { get; set; }

            [SPParameterName("p_tcaes_estatus", 7)]
            public string tcaes_estatus { get; set; }

        }

        [SPName("P_UPD_TCOCA")]//Editar Tcoca
        public class ModelEditarTcoca : BaseModelRequest
        {

            [SPParameterName("p_tcoca_clave", 0)]
            public string tcoca_clave { get; set; }

            [SPParameterName("p_tcoca_desc", 1)]
            public string tcoca_desc { get; set; }

            [SPParameterName("p_tcoca_tuser_clave", 2)]
            public string tcoca_tuser_clave { get; set; }

            [SPParameterName("p_tcoca_estatus", 3)]
            public string tespr_estatus { get; set; }
        }
    }
}
