using SAES_DBO.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SAES_DBO.Models.ModelCargaAcademica;
using static SAES_DBO.Models.ModelCobranza;
using static SAES_DBO.Models.ModelDocente;

namespace SAES_Services
{
    public class CargaAcademicaService : Methods
    {
        public DataTable ObtenerDatosGruposMateria(string programa, string periodo_programa, string campus, string periodo,
            string grupo_turno, string materia)
        {
            ModelObtenerDatosGruposMateriaRequest request = new ModelObtenerDatosGruposMateriaRequest() 
            { 
                P_Programa = programa,
                P_Periodo_Programa = periodo_programa,
                P_Campus = campus,
                P_Periodo = periodo,
                P_Grupo_Turno=grupo_turno,
                P_Materia=materia
            };
            List<ModelObtenerDatoGruposMateriaResponse> response = DB.CallSPListResult<ModelObtenerDatoGruposMateriaResponse, ModelObtenerDatosGruposMateriaRequest>(request);
            return ToDataTable(response);
        }

        public DataTable ObtenerEscenariosAcademicos()
        {
            ModelObtenerEscenariosAcadRequest request = new ModelObtenerEscenariosAcadRequest() { };
            List<ModelEscenariosAcadResponse> response = DB.CallSPListResult<ModelEscenariosAcadResponse, ModelObtenerEscenariosAcadRequest>(request);
            return ToDataTable(response);
        }


        public ModelInsertarEscenariosAcademicosResponse InsertarDisponibilidad(string p_tsalo_clave, string p_tsalo_desc, string p_tsalo_minimo, 
            string p_tsalo_maximo, string p_tsalo_tipo, string p_tsalo_tuser_clave, string p_tsalo_estatus)
        {
            ModelEscenariosAcademicosRequest Insert = new ModelEscenariosAcademicosRequest()
            {
                tsalo_clave = p_tsalo_clave,
                tsalo_desc = p_tsalo_desc,
                tsalo_minimo = p_tsalo_minimo,
                tsalo_maximo = p_tsalo_maximo,
                tsalo_tipo = p_tsalo_tipo,
                tsalo_tuser_clave = p_tsalo_tuser_clave,
                tsalo_estatus= p_tsalo_estatus
            };
            ModelInsertarEscenariosAcademicosResponse response = DB.CallSPResult<ModelInsertarEscenariosAcademicosResponse, ModelEscenariosAcademicosRequest>(Insert);
            return response;
        }

        public ModelObtenerEscuelaProcedenciaResponse ObtenerEscuelaProcedencia(string p_tpreq_tprog_clave, string p_tpreq_tespr_clave, string p_matricula)
        {
            ModelObtenerEscuelaProcedenciaRequest request = new ModelObtenerEscuelaProcedenciaRequest()
            {
                tpreq_tprog_clave = p_tpreq_tprog_clave,
                tpreq_tespr_clave = p_tpreq_tespr_clave,
                matricula = p_matricula
            };
            ModelObtenerEscuelaProcedenciaResponse response = DB.CallSPResult<ModelObtenerEscuelaProcedenciaResponse, ModelObtenerEscuelaProcedenciaRequest>(request);
            return response;
        }

        public string InsertarTPreq(string p_tpreq_tpers_num, string p_tpreq_tprog_clave, string p_tpreq_tespr_clave, string p_tpreq_carrera,
string p_tpreq_tpees_clave, string p_tpreq_folio, string p_tpreq_fecha_dict, string p_tpreq_estatus, string p_tpreq_user)
        {
            ModelInsertaTPreq Insert = new ModelInsertaTPreq()
            {
                tpreq_tpers_num = p_tpreq_tpers_num,
                tpreq_tprog_clave = p_tpreq_tprog_clave,
                tpreq_tespr_clave = p_tpreq_tespr_clave,
                tpreq_carrera = p_tpreq_carrera,
                tpreq_tpees_clave = p_tpreq_tpees_clave,
                tpreq_folio = p_tpreq_folio,
                tpreq_fecha_dict = p_tpreq_fecha_dict,
                tpreq_estatus = p_tpreq_estatus,
                tpreq_user= p_tpreq_user

            };
            return DB.CallSPForInsertUpdate(Insert);
        }

        public string EditarTPreq(string p_tpreq_tpers_num, string p_tpreq_tprog_clave, string p_tpreq_tespr_clave, string p_tpreq_carrera,
string p_tpreq_tpees_clave, string p_tpreq_folio, string p_tpreq_fecha_dict, string p_tpreq_estatus, string p_tpreq_user)
        {
            ModelEditaTPreq Update = new ModelEditaTPreq()
            {
                tpreq_tpers_num = p_tpreq_tpers_num,
                tpreq_tprog_clave = p_tpreq_tprog_clave,
                tpreq_tespr_clave = p_tpreq_tespr_clave,
                tpreq_carrera = p_tpreq_carrera,
                tpreq_tpees_clave = p_tpreq_tpees_clave,
                tpreq_folio = p_tpreq_folio,
                tpreq_fecha_dict = p_tpreq_fecha_dict,
                tpreq_estatus = p_tpreq_estatus,
                tpreq_user = p_tpreq_user

            };
            return DB.CallSPForInsertUpdate(Update);
        }

    }
}
