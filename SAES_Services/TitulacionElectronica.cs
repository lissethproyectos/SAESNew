using SAES_DBO.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAES_Services
{
    public class TitulacionElectronica : Methods
    {
        public TitulacionElectronica() : base() { }
        public DataTable obtenDatosAlumnoTCEEL(string Matricula, string Programa)
        {
            ModelObtenDatosAlumnoTTIELRequest request = new ModelObtenDatosAlumnoTTIELRequest() { Matricula = Matricula, Programa = Programa };
            List<ModelObtenDatosAlumnoTTIELResponse> response = DB.CallSPListResult<ModelObtenDatosAlumnoTTIELResponse, ModelObtenDatosAlumnoTTIELRequest>(request);
            return ToDataTable(response);
        }
        public DataTable obtenEscuelaProcedenciaTCEEL(string Matricula, string Programa)
        {
            ModelObtenEscuelaProcTTIELRequest request = new ModelObtenEscuelaProcTTIELRequest() { Matricula = Matricula, Programa = Programa };
            List<ModelObtenEscuelaProcTTIELResponse> response = DB.CallSPListResult<ModelObtenEscuelaProcTTIELResponse, ModelObtenEscuelaProcTTIELRequest>(request);
            return ToDataTable(response);
        }
        public string obtenFolioTCEEL(string Matricula, string Programa)
        {
            ModelObtenFolioTTIELRequest request = new ModelObtenFolioTTIELRequest() { Matricula = Matricula, Programa = Programa };
            List<ModelObtenFolioTTIELResponse> response = DB.CallSPListResult<ModelObtenFolioTTIELResponse, ModelObtenFolioTTIELRequest>(request);
            return response[0].Folio;
        }

        public string InsertTTIEL(string NumeroCertificacion, string Matricula, string Programa, string Campus, string XML, string usuario)
        {
            ModelInsertaTTIELResponse Insert = new ModelInsertaTTIELResponse()
            {
                NumeroCertificacion = NumeroCertificacion,
                Matricula = Matricula,
                Programa = Programa,
                Campus = Campus,
                XML = XML,
                Usuario = usuario
            };
            return DB.CallSPForInsertUpdate(Insert);
        }
    }
}
