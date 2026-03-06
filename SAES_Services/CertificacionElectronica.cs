using SAES_DBO.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAES_Services
{
    public class CertificacionElectronica : Methods
    {
        public CertificacionElectronica() : base() { }
        public string InsertaDocumento(string documento, string nombreDoc, string extensionDoc)
        {
            ModelInsertaDocumentoCertificacionElectronicaResponse Insert = new ModelInsertaDocumentoCertificacionElectronicaResponse()
            {
                Documento = documento,
                NombreDoc = nombreDoc,
                ExtensionDoc = extensionDoc
            };
            return DB.CallSPForInsertUpdate(Insert);
        }

        public string obtenValidaFolioTCEEL(string Matricula, string Programa)
        {
            ModelObtenValidaFolioTCEELRequest request = new ModelObtenValidaFolioTCEELRequest() { Matricula = Matricula, Programa = Programa };
            List<ModelObtenValidaFolioTCEELResponse> response = DB.CallSPListResult<ModelObtenValidaFolioTCEELResponse, ModelObtenValidaFolioTCEELRequest>(request);
            return response[0].CuentaFolio;
        }

        public string obtenFolioTCEEL(string Matricula, string Programa)
        {
            ModelObtenFolioTCEELRequest request = new ModelObtenFolioTCEELRequest() { Matricula = Matricula, Programa = Programa };
            List<ModelObtenFolioTCEELResponse> response = DB.CallSPListResult<ModelObtenFolioTCEELResponse, ModelObtenFolioTCEELRequest>(request);
            return response[0].Folio;
        }

        public string InsertaTCEEL(string NumeroCertificacion, string Matricula, string Programa, string Campus, string XML, string usuario)
        {
            ModelInsertaTCEELResponse Insert = new ModelInsertaTCEELResponse()
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
