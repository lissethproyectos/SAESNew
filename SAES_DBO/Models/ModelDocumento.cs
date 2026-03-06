using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAES_DBO.Models
{
    public class ModelDocumento
    {
        [SPName("P_UPD_TCODO")]
        public class ModelEditarTcodo : BaseModelRequest
        {

            [SPParameterName("p_testu_tpers_id", 0)]
            public string testu_tpers_id { get; set; }

            [SPParameterName("p_testu_tpees_clave", 1)]
            public string testu_tpees_clave { get; set; }

            [SPParameterName("p_testu_tcamp_clave", 2)]
            public string testu_tcamp_clave { get; set; }

            [SPParameterName("p_testu_tprog_clave", 3)]
            public string testu_tprog_clave { get; set; }

            [SPParameterName("p_testu_ttasa_clave", 4)]
            public string testu_ttasa_clave { get; set; }

            [SPParameterName("p_testu_tstal_clave", 5)]
            public string testu_tstal_clave { get; set; }

            [SPParameterName("p_testu_periodo", 6)]
            public int testu_periodo { get; set; }

            [SPParameterName("p_testu_tespe_clave", 7)]
            public string testu_tespe_clave { get; set; }

            [SPParameterName("p_testu_turno", 8)]
            public string testu_turno { get; set; }

            [SPParameterName("p_testu_user", 9)]
            public string testu_user { get; set; }

            [SPParameterName("p_calculo_cuotas", 10)]
            public string calculo_cuotas { get; set; }

        }

        [SPName("P_UPD_TREDO")]
        public class ModelEditarTredo : BaseModelRequest
        {

            [SPParameterName("p_matricula", 0)]
            public string matricula { get; set; }

            [SPParameterName("p_tredo_tpees_clave", 1)]
            public string tredo_tpees_clave { get; set; }

            [SPParameterName("p_tredo_consecutivo", 2)]
            public string tredo_consecutivo { get; set; }

            [SPParameterName("p_tredo_tdocu_clave", 3)]
            public string tredo_tdocu_clave { get; set; }

            [SPParameterName("p_tredo_tstdo_clave", 4)]
            public string tredo_tstdo_clave { get; set; }

            [SPParameterName("p_tredo_fecha_limite", 5)]
            public string tredo_fecha_limite { get; set; }

            [SPParameterName("p_tredo_fecha_entrega", 6)]
            public string tredo_fecha_entrega { get; set; }

            [SPParameterName("p_tredo_user", 7)]
            public string tredo_user { get; set; }

            [SPParameterName("p_tredo_date", 8)]
            public string tredo_date { get; set; }

            [SPParameterName("p_tredo_estatus", 9)]
            public string tredo_estatus { get; set; }
        }


        [SPName("P_QRY_TSTDO")]

        public class ModelEstatusDoctoRequest : BaseModelRequest
        {
           
        }
        public class ModelEstatusDoctoResponse : BaseModelResponse
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


        [SPName("P_QRY_TCODO")]

        public class ModelTcodoRequest : BaseModelRequest
        {
            [SPParameterName("p_tcodo_tdocu_clave", 0)]
            public string tcodo_tdocu_clave { get; set; }

            [SPParameterName("p_tcodo_tmoda_clave", 1)]
            public string tcodo_tmoda_clave { get; set; }

            [SPParameterName("p_tcodo_tprog_clave", 2)]
            public string tcodo_tprog_clave { get; set; }

            [SPParameterName("p_tcodo_ttiin_clave", 3)]
            public string tcodo_ttiin_clave { get; set; }

            [SPParameterName("p_tcodo_tnive_clave", 4)]
            public string tcodo_tnive_clave { get; set; }

            [SPParameterName("p_tcodo_tcamp_clave", 5)]
            public string tcodo_tcamp_clave { get; set; }

            [SPParameterName("p_tcodo_tcole_clave", 6)]
            public string tcodo_tcole_clave { get; set; }
        }
        public class ModelTcodoResponse : BaseModelResponse
        {
            [SPResponseColumnName("tcodo_tprog_clave")]
            public string tcodo_tprog_clave { get; set; }

            [SPResponseColumnName("tcodo_ttiin_clave")]
            public string tcodo_ttiin_clave { get; set; }

            [SPResponseColumnName("clave")]
            public string clave { get; set; }

            [SPResponseColumnName("nombre")]
            public string nombre { get; set; }

            [SPResponseColumnName("campus")]
            public string campus { get; set; }

            [SPResponseColumnName("nivel")]
            public string nivel { get; set; }

            [SPResponseColumnName("colegio")]
            public string colegio { get; set; }

            [SPResponseColumnName("modalidad")]
            public string modalidad { get; set; }

            [SPResponseColumnName("programa")]
            public string programa { get; set; }

            [SPResponseColumnName("tipo")]
            public string tipo { get; set; }

            [SPResponseColumnName("c_estatus")]
            public string c_estatus { get; set; }

            [SPResponseColumnName("estatus")]
            public string estatus { get; set; }

            [SPResponseColumnName("fecha")]
            public string fecha { get; set; }
        }


        [SPName("P_INS_TSTDO")]
        public class ModelInsertarTstdo : BaseModelRequest
        {
            [SPParameterName("p_tstdo_clave", 0)]
            public string tstdo_clave { get; set; }

            [SPParameterName("p_tstdo_desc", 1)]
            public string tstdo_desc { get; set; }

            [SPParameterName("p_tstdo_coment", 2)]
            public string tstdo_coment { get; set; }

            [SPParameterName("p_tstdo_user", 3)]
            public string tstdo_user { get; set; }

            [SPParameterName("p_tstdo_estatus", 4)]
            public string tstdo_estatus { get; set; }
        }

        public class ModelInsertarTstdoResponse : BaseModelResponse
        {
            [SPResponseColumnName("Existe")]
            public string Existe { get; set; }
        }

        [SPName("P_UPD_TSTDO")]
        public class ModelUpdTstdo : BaseModelRequest
        {
            [SPParameterName("p_tstdo_clave", 0)]
            public string tstdo_clave { get; set; }

            [SPParameterName("p_tstdo_desc", 1)]
            public string tstdo_desc { get; set; }

            [SPParameterName("p_tstdo_coment", 2)]
            public string tstdo_coment { get; set; }

            [SPParameterName("p_tstdo_user", 3)]
            public string tstdo_user { get; set; }

            [SPParameterName("p_tstdo_estatus", 4)]
            public string tstdo_estatus { get; set; }
        }

    }
}
