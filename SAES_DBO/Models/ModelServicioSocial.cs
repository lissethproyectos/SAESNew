using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SAES_DBO.Models.ModelServicioSocial;

namespace SAES_DBO.Models
{
    public class ModelServicioSocial
    {
        [SPName("P_INS_TPRSS")]//Insertar Tcoca
        public class ModelInsertarTprss : BaseModelRequest
        {

            [SPParameterName("p_tprss_clave", 0)]
            public string tprss_clave { get; set; }

            [SPParameterName("p_tprss_desc", 1)]
            public string tprss_desc { get; set; }

            [SPParameterName("p_tprss_empresa", 2)]
            public string tprss_empresa { get; set; }

            [SPParameterName("p_tprss_creditos", 3)]
            public string tprss_creditos { get; set; }

            [SPParameterName("p_tprss_estatus", 4)]
            public string tprss_estatus { get; set; }

            [SPParameterName("p_tprss_tuser_clave", 5)]
            public string tprss_tuser_clave { get; set; }
        }
        public class ModelInsertarTprssResponse : BaseModelResponse
        {
            [SPResponseColumnName("Existe")]
            public string Existe { get; set; }
        }



        [SPName("P_QRY_TRESS_REPS")]
        public class ModelObtenDatosServicioSocialRequest : BaseModelRequest
        {
            [SPParameterName("P_Campus", 0)]
            public string Campus { get; set; }

            [SPParameterName("P_Nivel", 1)]
            public string Nivel { get; set; }

            [SPParameterName("P_Programa", 2)]
            public string Programa { get; set; }

            [SPParameterName("P_Opcion", 3)]
            public string Opcion { get; set; }

            [SPParameterName("P_Estatus", 4)]
            public string Estatus { get; set; }

            [SPParameterName("P_Moda", 5)]
            public string Moda { get; set; }
        }
        public class ModelObtenDatosServicioSocialResponse : BaseModelResponse
        {
            [SPResponseColumnName("tress_tpers_num")]
            public string tress_tpers_num { get; set; }

            [SPResponseColumnName("tpers_id")]
            public string tpers_id { get; set; }

            [SPResponseColumnName("Nombre")]
            public string Nombre { get; set; }

            [SPResponseColumnName("tress_tpees_clave")]
            public string tress_tpees_clave { get; set; }

            [SPResponseColumnName("tcamp_desc")]
            public string tcamp_desc { get; set; }

            [SPResponseColumnName("tnive_desc")]
            public string tnive_desc { get; set; }

            [SPResponseColumnName("tprog_clave")]
            public string tprog_clave { get; set; }

            [SPResponseColumnName("tprss_desc")]
            public string tprss_desc { get; set; }

            [SPResponseColumnName("trees_estatus")]
            public string trees_estatus { get; set; }

            [SPResponseColumnName("Modalidad")]
            public string Modalidad { get; set; }

            [SPResponseColumnName("trees_horas")]
            public string trees_horas { get; set; }

            [SPResponseColumnName("trees_horas_cumplidas")]
            public string trees_horas_cumplidas { get; set; }

            [SPResponseColumnName("trees_date")]
            public string trees_date { get; set; }
        }

        [SPName("P_QRY_TPEES_TRESS")]
        public class ModelObtenPeriodosServicioSocialRequest : BaseModelRequest
        {

        }
        public class ModelObtenPeriodosServicioSocialResponse : BaseModelResponse
        {
            [SPResponseColumnName("clave")]
            public string clave { get; set; }

            [SPResponseColumnName("nombre")]
            public string nombre { get; set; }
        }





        [SPName("P_UPD_TPRSS")]//Insertar Tcoca
        public class ModelEditarTprss : BaseModelRequest
        {

            [SPParameterName("p_tprss_clave", 0)]
            public string tprss_clave { get; set; }

            [SPParameterName("p_tprss_desc", 1)]
            public string tprss_desc { get; set; }

            [SPParameterName("p_tprss_empresa", 2)]
            public string tprss_empresa { get; set; }

            [SPParameterName("p_tprss_creditos", 3)]
            public string tprss_creditos { get; set; }

            [SPParameterName("p_tprss_estatus", 4)]
            public string tprss_estatus { get; set; }

            [SPParameterName("p_tprss_tuser_clave", 5)]
            public string tprss_tuser_clave { get; set; }
        }

        [SPName("P_QRY_TPRSS")]
        public class ModelTprssRequest : BaseModelRequest
        {

        }
        public class ModelTprssResponse : BaseModelResponse
        {
            [SPResponseColumnName("tprss_clave")]
            public string tprss_clave { get; set; }

            [SPResponseColumnName("tprss_desc")]
            public string tprss_desc { get; set; }

            [SPResponseColumnName("tprss_empresa")]
            public string tprss_empresa { get; set; }

            [SPResponseColumnName("tprss_creditos")]
            public string tprss_creditos { get; set; }

            [SPResponseColumnName("tprss_estatus")]
            public string tprss_estatus { get; set; }

            [SPResponseColumnName("tprss_date")]
            public string tprss_date { get; set; }

            [SPResponseColumnName("tprss_tuser_clave")]
            public string tprss_tuser_clave { get; set; }
        }

        [SPName("P_QRY_TRESS")]
        public class ModelTressRequest : BaseModelRequest
        {
            [SPParameterName("p_matricula", 0)]
            public string matricula { get; set; }

            [SPParameterName("p_tress_tprog_clave", 1)]
            public string tress_tprog_clave { get; set; }
        }
        public class ModelTressResponse : BaseModelResponse
        {
            [SPResponseColumnName("tress_tpees_clave")]
            public string tress_tpees_clave { get; set; }

            [SPResponseColumnName("tress_tpers_num")]
            public string tress_tpers_num { get; set; }

            [SPResponseColumnName("tress_tprog_clave")]
            public string tress_tprog_clave { get; set; }

            [SPResponseColumnName("tress_tprss_clave")]
            public string tress_tprss_clave { get; set; }

            [SPResponseColumnName("tress_fecha_inicio")]
            public string tress_fecha_inicio { get; set; }

            [SPResponseColumnName("tress_modalidad")]
            public string tress_modalidad { get; set; }

            [SPResponseColumnName("trees_horas")]
            public string trees_horas { get; set; }

            [SPResponseColumnName("trees_horas_cumplidas")]
            public string trees_horas_cumplidas { get; set; }

            [SPResponseColumnName("trees_estatus")]
            public string trees_estatus { get; set; }

            [SPResponseColumnName("trees_tuser_clave")]
            public string trees_tuser_clave { get; set; }

            [SPResponseColumnName("tress_desc_modalidad")]
            public string tress_desc_modalidad { get; set; }

            [SPResponseColumnName("trees_fecha_fin")]
            public string trees_fecha_fin { get; set; }
        }

        [SPName("P_INS_TRESS")]//Insertar Tcoca
        public class ModelInsertarTress : BaseModelRequest
        {

            [SPParameterName("p_tress_tpees_clave", 0)]
            public string tress_tpees_clave { get; set; }

            [SPParameterName("p_tress_tpers_num",1)]
            public string tress_tpers_num { get; set; }

            [SPParameterName("p_tress_tprog_clave",2)]
            public string tress_tprog_clave { get; set; }

            [SPParameterName("p_tress_tprss_clave",3)]
            public string tress_tprss_clave { get; set; }

            [SPParameterName("p_tress_fecha_inicio",4)]
            public string tress_fecha_inicio { get; set; }

            [SPParameterName("p_tress_fecha_final", 5)]
            public string tress_fecha_final { get; set; }

            [SPParameterName("p_tress_modalidad",6)]
            public string tress_modalidad { get; set; }

            [SPParameterName("p_trees_horas",7)]
            public string trees_horas { get; set; }

            [SPParameterName("p_trees_horas_cumplidas",8)]
            public string trees_horas_cumplidas { get; set; }

            [SPParameterName("p_trees_estatus",9)]
            public string trees_estatus { get; set; }

            [SPParameterName("p_trees_tuser_clave",10)]
            public string trees_tuser_clave { get; set; }
        }
        public class ModelInsertarTressResponse : BaseModelResponse
        {
            [SPResponseColumnName("Existe")]
            public string Existe { get; set; }

            [SPResponseColumnName("CreditosValidados")]
            public string CreditosValidados { get; set; }

            [SPResponseColumnName("CreditosAlum")]
            public string CreditosAlum { get; set; }

            [SPResponseColumnName("CreditosRequeridos")]
            public string CreditosRequeridos { get; set; }
        }


        [SPName("P_UPD_TRESS")]//Insertar Tcoca
        public class ModelEditarTress : BaseModelRequest
        {

            [SPParameterName("p_tress_tpees_clave", 0)]
            public string tress_tpees_clave { get; set; }

            [SPParameterName("p_tress_tpers_num", 1)]
            public string tress_tpers_num { get; set; }

            [SPParameterName("p_tress_tprog_clave", 2)]
            public string tress_tprog_clave { get; set; }

            [SPParameterName("p_tress_tprss_clave", 3)]
            public string tress_tprss_clave { get; set; }

            [SPParameterName("p_tress_fecha_inicio", 4)]
            public string tress_fecha_inicio { get; set; }

            [SPParameterName("p_tress_fecha_final", 5)]
            public string tress_fecha_final { get; set; }

            [SPParameterName("p_tress_modalidad", 6)]
            public string tress_modalidad { get; set; }

            [SPParameterName("p_trees_horas", 7)]
            public string trees_horas { get; set; }

            [SPParameterName("p_trees_horas_cumplidas", 8)]
            public string trees_horas_cumplidas { get; set; }

            [SPParameterName("p_trees_estatus", 9)]
            public string trees_estatus { get; set; }

            [SPParameterName("p_trees_tuser_clave", 10)]
            public string trees_tuser_clave { get; set; }
        }

        [SPName("P_QRY_TPRSS_DISPONIBLES")]
        public class ModelTprssDisponiblesRequest : BaseModelRequest
        {
            [SPParameterName("p_matricula", 0)]
            public string matricula { get; set; }

            [SPParameterName("p_programa", 1)]
            public string programa { get; set; }
        }

        public class ModelTprssDisponiblesResponse : BaseModelResponse
        {
            [SPResponseColumnName("tprss_clave")]
            public string clave { get; set; }

            [SPResponseColumnName("tprss_desc")]
            public string descripcion { get; set; }
        }

    }
}
