using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAES_DBO.Models
{
    public class ModelPlan
    {
        [SPName("P_QRY_PREDICTAMEN")]
        public class ModelObtenerPredictamenAlumnoRequest : BaseModelRequest
        {
            [SPParameterName("P_Matricula", 0)]
            public string P_Matricula { get; set; }

            [SPParameterName("P_Programa", 1)]
            public string P_Programa { get; set; }
        }
        public class ModelObtenerPredictamenAlumnoResponse : BaseModelResponse
        {
            [SPResponseColumnName("tpreq_tprog_clave")]
            public string tpreq_tprog_clave { get; set; }

            [SPResponseColumnName("clave")]
            public string clave { get; set; }

            [SPResponseColumnName("Escuela")]
            public string Escuela { get; set; }

            [SPResponseColumnName("carrera")]
            public string carrera { get; set; }

            [SPResponseColumnName("folio")]
            public string folio { get; set; }

            [SPResponseColumnName("fecha_dict")]
            public string fecha_dict { get; set; }

            [SPResponseColumnName("estatus")]
            public string estatus { get; set; }

            [SPResponseColumnName("fecha")]
            public string fecha { get; set; }
        }

        [SPName("P_QRY_PLAN_ALUMNO")]
        public class ModelObtenerPlanAlumnoRequest : BaseModelRequest
        {
            [SPParameterName("P_Programa", 0)]
            public string P_Programa { get; set; }

            [SPParameterName("P_Matricula", 1)]
            public string P_Matricula { get; set; }

            [SPParameterName("P_Escuela_Procedencia", 2)]
            public string P_Escuela_Procedencia { get; set; }
        }
        public class ModelObtenerPlanAlumnoResponse : BaseModelResponse
        {
            [SPResponseColumnName("consec")]
            public string consec { get; set; }

            [SPResponseColumnName("area")]
            public string area { get; set; }

            [SPResponseColumnName("clave")]
            public string clave { get; set; }

            [SPResponseColumnName("materia")]
            public string materia { get; set; }


            [SPResponseColumnName("origen")]
            public string origen { get; set; }


            [SPResponseColumnName("cali")]
            public string cali { get; set; }


            [SPResponseColumnName("sugerida")]
            public string sugerida { get; set; }


            [SPResponseColumnName("tpred_estatus")]
            public string tpred_estatus { get; set; }
        }

        [SPName("P_QRY_CALIFICACIONES")]
        public class ModelObtenerCatCalificacionesRequest : BaseModelRequest
        {
            [SPParameterName("P_Programa", 0)]
            public string P_Programa { get; set; }         
        }
        public class ModelObtenerCatCalificacionesResponse : BaseModelResponse
        {
            [SPResponseColumnName("clave")]
            public string clave { get; set; }

            [SPResponseColumnName("descripcion")]
            public string descripcion { get; set; }
        }

        [SPName("P_INS_TPRED")]
        public class ModelInsTpredRequest : BaseModelRequest
        {
            [SPParameterName("p_tpred_tpers_id", 0)]
            public string tpred_tpers_id { get; set; }

            [SPParameterName("p_tpred_tespr_clave", 1)]
            public string tpred_tespr_clave { get; set; }

            [SPParameterName("p_tpred_tprog_clave", 2)]
            public string tpred_tprog_clave { get; set; }

            [SPParameterName("p_tpred_tmate_clave", 3)]
            public string tpred_tmate_clave { get; set; }

            [SPParameterName("p_tpred_mate_origen", 4)]
            public string tpred_mate_origen { get; set; }

            [SPParameterName("p_tpred_tcali_clave", 5)]
            public string tpred_tcali_clave { get; set; }

            [SPParameterName("p_tpred_consecutivo", 6)]
            public string tpred_consecutivo { get; set; }
           
            [SPParameterName("p_tpred_user", 7)]
            public string tpred_user { get; set; }

            [SPParameterName("p_tpred_estatus", 8)]
            public string tpred_estatus { get; set; }
        }


        [SPName("P_QRY_PROGRAMAS_POR_CAMPUS")]
        public class ModelObtenerProgramasporCampusRequest : BaseModelRequest
        {
            [SPParameterName("P_Campus", 0)]
            public string P_Campus { get; set; }
        }
        public class ModelObtenerProgramasporCampusResponse : BaseModelResponse
        {
            [SPResponseColumnName("Clave")]
            public string Clave { get; set; }

            [SPResponseColumnName("Nombre")]
            public string Nombre { get; set; }

            [SPResponseColumnName("Nivel")]
            public string Nivel { get; set; }

            [SPResponseColumnName("Modalidad")]
            public string Modalidad { get; set; }

            [SPResponseColumnName("Admision")]
            public string Admision { get; set; }

            [SPResponseColumnName("Estatus_code")]
            public string Estatus_code { get; set; }

            [SPResponseColumnName("Estatus")]
            public string Estatus { get; set; }

            [SPResponseColumnName("Fecha")]
            public string Fecha { get; set; }

            [SPResponseColumnName("c_campus")]
            public string c_campus { get; set; }
        }


        [SPName("P_INS_TPROG")]
        public class ModelInsTprogRequest : BaseModelRequest
        {
            [SPParameterName("p_tprog_clave", 0)]
            public string tprog_clave { get; set; }

            [SPParameterName("p_tprog_desc", 1)]
            public string tprog_desc { get; set; }

            [SPParameterName("p_tprog_tnive_clave", 2)]
            public string tprog_tnive_clave { get; set; }

            [SPParameterName("p_tprog_tcole_clave", 3)]
            public string tprog_tcole_clave { get; set; }

            [SPParameterName("p_tprog_tmoda_clave", 4)]
            public string tprog_tmoda_clave { get; set; }

            [SPParameterName("p_tprog_creditos", 5)]
            public string tprog_creditos { get; set; }

            [SPParameterName("p_tprog_cursos", 6)]
            public string tprog_cursos { get; set; }

            [SPParameterName("p_tprog_periodos", 7)]
            public string tprog_periodos { get; set; }

            [SPParameterName("p_tprog_incorporante", 8)]
            public string tprog_incorporante { get; set; }

            [SPParameterName("p_tprog_rvoe", 9)]
            public string tprog_rvoe { get; set; }

            [SPParameterName("p_tprog_fecha_rvoe", 10)]
            public string tprog_fecha_rvoe { get; set; }

            [SPParameterName("p_tprog_clave_plan", 11)]
            public string tprog_clave_plan { get; set; }

            [SPParameterName("p_tprog_periodicidad", 12)]
            public string tprog_periodicidad { get; set; }

            [SPParameterName("p_tprog_cal_minima", 13)]
            public string tprog_cal_minima { get; set; }

            [SPParameterName("p_tprog_cal_maxima", 14)]
            public string tprog_cal_maxima { get; set; }

            [SPParameterName("p_tprog_min_aprob", 15)]
            public string tprog_min_aprob { get; set; }

            [SPParameterName("p_tprog_estatus", 16)]
            public string tprog_estatus { get; set; }

            [SPParameterName("p_tprog_user", 17)]
            public string tprog_user { get; set; }

        }
    }
}
