using SAES_DBO.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAES_Services
{
    public class RegistroInasistencias : Methods
    {
        public RegistroInasistencias() : base()
        {
        }
        public DataTable obtenAlumnosInscritos(string periodo, string campus, string materia, string grupo, string fecha)
        {
            ModelObtenAlumnosInscritosRequest request = new ModelObtenAlumnosInscritosRequest() { Periodo = periodo, Campus = campus, Materia = materia, Grupo = grupo, Fecha = fecha };
            List<ModelObtenAlumnosInscritosResponse> response = DB.CallSPListResult<ModelObtenAlumnosInscritosResponse, ModelObtenAlumnosInscritosRequest>(request);
            return ToDataTableForDropDownList(response, false);
        }

        public bool validaFecha(string periodo, string campus, string materia, string grupo, string fecha)
        {
            ModelValidaFechaRegistoInasistenciasRequest request = new ModelValidaFechaRegistoInasistenciasRequest() { Periodo = periodo, Campus = campus, Materia = materia, Grupo = grupo, Fecha = fecha };
            List<ModelValidaFechaRegistoInasistenciasResponse> response = DB.CallSPListResult<ModelValidaFechaRegistoInasistenciasResponse, ModelValidaFechaRegistoInasistenciasRequest>(request);
            return (response.Count > 0 && response.FirstOrDefault().Respuesta == "EXITO")? true : false;
        }

        public string DeleteRegistroInasistencias(string periodo, string campus, string materia, string grupo, string fecha)
        {
            ModelRegistroInasistenciasForDeleteRequest req = new ModelRegistroInasistenciasForDeleteRequest();
            req.Periodo = periodo;
            req.Campus = campus;
            req.Materia = materia;
            req.Grupo = grupo;
            req.Fecha = fecha;
            return DB.CallSPForInsertUpdate(req);
        }

        public string InsertRegistroInasistencias(string periodo, string campus, string materia, string grupo, string fecha, string matricula, string usuario, string programa)
        {
            ModelRegistroInasistenciasForInsertRequest req = new ModelRegistroInasistenciasForInsertRequest();
            req.Periodo = periodo;
            req.Campus = campus;
            req.Materia = materia;
            req.Grupo = grupo;
            req.Fecha = fecha;
            req.Matricula = matricula;
            req.Usuario = usuario;
            req.Programa = programa;
            return DB.CallSPForInsertUpdate(req);
        }
    }
}
