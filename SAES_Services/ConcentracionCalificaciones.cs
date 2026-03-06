using SAES_DBO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAES_Services
{
    public class ConcentracionCalificaciones : Methods
    {
        public ConcentracionCalificaciones() : base() { }
        public string InsertConcentracionCalificacion(string periodo, string campus, string nivel, string programa, string materia, string grupo, string usuario)
        {
            ModelConcentracionCalificacionesForInsertRequest req = new ModelConcentracionCalificacionesForInsertRequest();
            req.Periodo = periodo;
            req.Campus = campus;
            req.Nivel = nivel;
            req.Programa = programa;
            req.Materia = materia;
            req.Grupo = grupo;
            req.User = usuario;
            return DB.CallSPForInsertUpdate(req);
        }
    }
}
