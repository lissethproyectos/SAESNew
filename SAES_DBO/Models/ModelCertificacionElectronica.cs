using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAES_DBO.Models
{
    [SPName("P_INSERTA_DOC_CERTIFICADO")]
    public class ModelInsertaDocumentoCertificacionElectronicaResponse : BaseModelRequest
    {
        [Required]
        [SPParameterName("Documento", 0)]
        public string Documento { get; set; }
        [SPParameterName("pNombreDoc", 1)]
        public string NombreDoc { get; set; }
        [SPParameterName("pExtensionDoc", 2)]
        public string ExtensionDoc { get; set; }
    }

    [SPName("P_INSERTA_TCEEL")]
    public class ModelInsertaTCEELResponse : BaseModelRequest
    {
        [Required]
        [SPParameterName("P_NO_CERTIF", 0)]
        public string NumeroCertificacion { get; set; }
        [SPParameterName("P_MATRICULA", 1)]
        public string Matricula { get; set; }
        [SPParameterName("P_PROGRAMA", 2)]
        public string Programa { get; set; }
        [SPParameterName("P_CAMPUS", 3)]
        public string Campus { get; set; }
        [SPParameterName("P_XML", 4)]
        public string XML { get; set; }
        [SPParameterName("P_USER", 5)]
        public string Usuario { get; set; }
    }
}
