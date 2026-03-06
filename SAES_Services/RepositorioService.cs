using SAES_DBO.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SAES_DBO.Models.ModelCobranza;
using static SAES_DBO.Models.ModelRepositorio;

namespace SAES_Services
{
    public class RepositorioService : Methods
    {
        public List<ModelStatusRepositorioResponse> ObtenerStatusDocumentos(string P_Rol, string P_Status, string P_User)
        {
            ModelStatusRepositorioRequest request = new ModelStatusRepositorioRequest() { Rol = P_Rol, Status = P_Status, User= P_User };
            List<ModelStatusRepositorioResponse> response = DB.CallSPListResult<ModelStatusRepositorioResponse, ModelStatusRepositorioRequest>(request);
            return response;
        }

        public List<ModelDocumentosAlumnoResponse> ObtenerDocumentosAlumno(string P_IDAlumno_in, string P_Rol)
        {
            ModelDocumentosAlumnoRequest request = new ModelDocumentosAlumnoRequest() { IDAlumno_in = P_IDAlumno_in, Rol = P_Rol };
            List<ModelDocumentosAlumnoResponse> response = DB.CallSPListResult<ModelDocumentosAlumnoResponse, ModelDocumentosAlumnoRequest>(request);
            return response;
        }

        public ModelObtenerDoctosEntregadosResponse DocumentosEntregados(string P_IDAlumno)
        {
            ModelObtenerDoctosEntregadosRequest request = new ModelObtenerDoctosEntregadosRequest() { IDAlumno = P_IDAlumno };
            ModelObtenerDoctosEntregadosResponse response = DB.CallSPResult<ModelObtenerDoctosEntregadosResponse, ModelObtenerDoctosEntregadosRequest>(request);
            return response;
        }

        public ModelObtenerAvanceAlumnoResponse DocumentosEntregadosAlumno(string P_Matricula)
        {
            ModelObtenerAvanceAlumnoRequest request = new ModelObtenerAvanceAlumnoRequest() { Matricula = P_Matricula };
            ModelObtenerAvanceAlumnoResponse response = DB.CallSPResult<ModelObtenerAvanceAlumnoResponse, ModelObtenerAvanceAlumnoRequest>(request);
            return response;
        }

        public ModelCambiaStatusResponse ModelCambiaStatus(string p_matricula)
        {
            ModelCambiaStatusRequest request = new ModelCambiaStatusRequest() {Matricula = p_matricula };
            ModelCambiaStatusResponse response = DB.CallSPResult<ModelCambiaStatusResponse, ModelCambiaStatusRequest>(request);
            return response;
        }

        public ModelObtenerTipoDoctosResponse ObtenerTipoDoctos(string P_Tipo)
        {
            ModelObtenerTipoDoctosRequest request = new ModelObtenerTipoDoctosRequest() { Tipo = P_Tipo };
            ModelObtenerTipoDoctosResponse response = DB.CallSPResult<ModelObtenerTipoDoctosResponse, ModelObtenerTipoDoctosRequest>(request);
            return response;
        }

        public string EditarDoctosAlumnoCobranza(string P_ID_DOCTO)
        {

            ModelEditarDoctosAlumnoRequest Update = new ModelEditarDoctosAlumnoRequest()
            {
                ID_DOCTO = P_ID_DOCTO
            };
            return DB.CallSPForInsertUpdate(Update);
        }
    }
}
