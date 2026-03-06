using SAES_DBO.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAES_Services
{
    public class CalificacionPorComponentes : Methods
    {
        public DataTable obtenCalificacionesNivel(string P_Nivel)
        {
            ModelTCaliRequest request = new ModelTCaliRequest() { Nivel=P_Nivel };
            List<ModelTCaliResponse> response = DB.CallSPListResult<ModelTCaliResponse, ModelTCaliRequest>(request);
            return ToDataTableForDropDownList(response);
        }

        public DataTable obtenCalificacionesNivelGrid(string P_Nivel)
        {
            ModelTCaliRequest request = new ModelTCaliRequest() { Nivel = P_Nivel };
            List<ModelTCaliResponse> response = DB.CallSPListResult<ModelTCaliResponse, ModelTCaliRequest>(request);
            return ToDataTable(response);
        }

        public ModelInsertaTCaliResponse InsertarTCali(string p_tcali_tnive_clave, string p_tcali_clave, string p_tcali_puntos, string p_tcali_ind_aprob,
    string p_tcali_ind_prom, string p_tcali_estatus, string p_tcali_user)
        {
            ModelInsertaTCali Insert = new ModelInsertaTCali()
            {
                tcali_tnive_clave = p_tcali_tnive_clave,
                tcali_clave = p_tcali_clave,
                tcali_puntos = p_tcali_puntos,
                tcali_ind_aprob = p_tcali_ind_aprob,
                tcali_ind_prom = p_tcali_ind_prom,
                tcali_estatus = p_tcali_estatus,
                tcali_user = p_tcali_user
            };
            ModelInsertaTCaliResponse response = DB.CallSPResult<ModelInsertaTCaliResponse, ModelInsertaTCali>(Insert);
            return response;
        }

        public string EditarTCali(string p_tcali_tnive_clave, string p_tcali_clave, string p_tcali_puntos, string p_tcali_ind_aprob,
            string p_tcali_ind_prom, string p_tcali_estatus, string p_tcali_user)
        {
            ModelEditarTCali Editar = new ModelEditarTCali()
            {
                tcali_tnive_clave = p_tcali_tnive_clave,
                tcali_clave = p_tcali_clave,
                tcali_puntos = p_tcali_puntos,
                tcali_ind_aprob = p_tcali_ind_aprob,
                tcali_ind_prom = p_tcali_ind_prom,
                tcali_estatus = p_tcali_estatus,
                tcali_user = p_tcali_user
            };
            return DB.CallSPForInsertUpdate(Editar);
        }

        public CalificacionPorComponentes() : base() { }

        public DataTable obtenCalificacionPorComponentes(string periodo, string campus, string materia, string grupo, string componente)
        {
            ModelObtenAlumnosRequest request = new ModelObtenAlumnosRequest() { Periodo = periodo, Campus = campus, Materia = materia, Grupo = grupo, Componente = componente };
            List<ModelObtenAlumnosResponse> response = DB.CallSPListResult<ModelObtenAlumnosResponse, ModelObtenAlumnosRequest>(request);
            return ToDataTable(response);
        }

        public string InsertCalificacionPorComponentes(string periodo, string campus, string materia, string grupo, string componente,string matricula, string clavePrograma, string claveCalificacion, string usuario)
        {
            ModelCalificacionPorComponentesForInsertRequest req = new ModelCalificacionPorComponentesForInsertRequest();
            req.Periodo = periodo;
            req.Campus = campus;
            req.Materia = materia;
            req.Grupo = grupo;
            req.Componente = componente;
            req.Matricula = matricula;
            req.Programa = clavePrograma;
            req.Calificacion = claveCalificacion;
            req.Usuario = usuario;
            return DB.CallSPForInsertUpdate(req);
        }
    }
}
