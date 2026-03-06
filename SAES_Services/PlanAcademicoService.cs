using SAES_DBO.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SAES_DBO.Models.ModelCobranza;
using static SAES_DBO.Models.ModelPlan;

namespace SAES_Services
{
    public class PlanAcademicoService : Methods
    {
        #region <Variables>
        Catalogos serviceCatalogo = new Catalogos();
        Catalogos_grales_Service serviceCatalogoGrals = new Catalogos_grales_Service();
        List<ModeltpaisResponse> lstPaises = new List<ModeltpaisResponse>();
        #endregion
        public DataTable ObtenerPredictamenAlumno(string matricula, string programa)
        {
            ModelObtenerPredictamenAlumnoRequest request = new ModelObtenerPredictamenAlumnoRequest() { P_Matricula = matricula, P_Programa = programa };
            List<ModelObtenerPredictamenAlumnoResponse> response = DB.CallSPListResult<ModelObtenerPredictamenAlumnoResponse, ModelObtenerPredictamenAlumnoRequest>(request);
            return ToDataTable(response);
        }

        public DataTable ObtenerPlanAlumno(string programa, string matricula, string escuela_procedencia)
        {
            ModelObtenerPlanAlumnoRequest request = new ModelObtenerPlanAlumnoRequest() { P_Programa = programa, P_Matricula = matricula, P_Escuela_Procedencia = escuela_procedencia };
            List<ModelObtenerPlanAlumnoResponse> response = DB.CallSPListResult<ModelObtenerPlanAlumnoResponse, ModelObtenerPlanAlumnoRequest>(request);
            return ToDataTable(response);
        }

        public DataTable ObtenerCatCalificaciones(string programa)
        {
            ModelObtenerCatCalificacionesRequest request = new ModelObtenerCatCalificacionesRequest() { P_Programa = programa };
            List<ModelObtenerCatCalificacionesResponse> response = DB.CallSPListResult<ModelObtenerCatCalificacionesResponse, ModelObtenerCatCalificacionesRequest>(request);
            return ToDataTableForDropDownList(response);
        }

        public string InsertarCalificacionRevalida(string p_tpred_tpers_num, string p_tpred_tespr_clave, string p_tpred_tprog_clave,
            string p_tpred_tmate_clave, string p_tpred_mate_origen, string p_tpred_tcali_clave, string p_tpred_consecutivo,
            string p_tpred_user, string p_tpred_estatus
            )
        {
            ModelInsTpredRequest request = new ModelInsTpredRequest()
            {
                tpred_tpers_id = p_tpred_tpers_num,
                tpred_tespr_clave = p_tpred_tespr_clave,
                tpred_tprog_clave = p_tpred_tprog_clave,
                tpred_tmate_clave = p_tpred_tmate_clave,
                tpred_mate_origen = p_tpred_mate_origen,
                tpred_tcali_clave = p_tpred_tcali_clave,
                tpred_consecutivo = p_tpred_consecutivo,
                tpred_user= p_tpred_user,
                tpred_estatus= p_tpred_estatus
            };
            return DB.CallSPForInsertUpdate(request);
        }

        public DataTable ObtenerProgramasPorCampus(string campus)
        {
            ModelObtenerProgramasporCampusRequest request = new ModelObtenerProgramasporCampusRequest() { P_Campus = campus };
            List<ModelObtenerProgramasporCampusResponse> response = DB.CallSPListResult<ModelObtenerProgramasporCampusResponse, ModelObtenerProgramasporCampusRequest>(request);
            return ToDataTable(response);
        }

        public string InsertarPrograma(string p_tprog_clave, string p_tprog_desc, string p_tprog_tnive_clave,
           string p_tprog_tcole_clave, string p_tprog_tmoda_clave, string p_tprog_creditos, string p_tprog_cursos,
           string p_tprog_periodos, string p_tprog_incorporante, string p_tprog_rvoe, string p_tprog_fecha_rvoe,
           string p_tprog_clave_plan, string p_tprog_periodicidad, string p_tprog_cal_minima, string p_tprog_cal_maxima, 
           string p_tprog_min_aprob, string p_tprog_estatus, string p_tprog_user
           )
        {
            ModelInsTprogRequest request = new ModelInsTprogRequest()
            {
                tprog_clave = p_tprog_clave,
                tprog_desc = p_tprog_desc,
                tprog_tnive_clave = p_tprog_tnive_clave,
                tprog_tcole_clave = p_tprog_tcole_clave,
                tprog_tmoda_clave = p_tprog_tmoda_clave,
                tprog_creditos = p_tprog_creditos,
                tprog_cursos = p_tprog_cursos,
                tprog_periodos = p_tprog_periodos,
                tprog_incorporante = p_tprog_incorporante,
                tprog_rvoe= p_tprog_rvoe,
                tprog_fecha_rvoe= p_tprog_fecha_rvoe,
                tprog_clave_plan= p_tprog_clave_plan,
                tprog_periodicidad= p_tprog_periodicidad,
                tprog_cal_minima= p_tprog_cal_minima,
                tprog_cal_maxima= p_tprog_cal_maxima,
                tprog_min_aprob= p_tprog_min_aprob,
                tprog_estatus= p_tprog_estatus,
                tprog_user= p_tprog_user

            };
            return DB.CallSPForInsertUpdate(request);
        }

    }
}
