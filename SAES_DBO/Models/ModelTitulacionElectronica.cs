using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAES_DBO.Models
{
    [SPName("P_OBTEN_DATOS_ALUMNO_TTIEL")]
    public class ModelObtenDatosAlumnoTTIELRequest : BaseModelRequest
    {
        [Required]
        [SPParameterName("P_MATRICULA", 0)]
        public string Matricula { get; set; }
        [Required]
        [SPParameterName("P_PROGRAMA", 1)]
        public string Programa { get; set; }
    }
    public class ModelObtenDatosAlumnoTTIELResponse : BaseModelResponse
    {
        [SPResponseColumnName("func_nombre")]
        public string NombreFuncionario { get; set; }
        [SPResponseColumnName("func_primerApellido")]
        public string PrimerApellidoFuncionario { get; set; }
        [SPResponseColumnName("func_segundoApellido")]
        public string SegundoApellidoFuncionario { get; set; }
        [SPResponseColumnName("curp")]
        public string CurpFuncionario { get; set; }
        [SPResponseColumnName("tfuca_desc")]
        public string DescripcionCargo { get; set; }
        [SPResponseColumnName("idCargo")]
        public string IdCargo { get; set; }
        [SPResponseColumnName("noCertificadoResponsable")]
        public string NoCertificadoResponsable { get; set; }
        [SPResponseColumnName("abrTitulo")]
        public string AbreviacionTitulo { get; set; }
        [SPResponseColumnName("cveInstitucion")]
        public string ClaveInstitucion { get; set; }
        [SPResponseColumnName("cveInstitucionAbreb")]
        public string AbrebInstitucion { get; set; }
        [SPResponseColumnName("nombreInstitucionReal")]
        public string NombreInstitucionReal { get; set; }
        [SPResponseColumnName("nombreInstitucion")]
        public string NombreInstitucion { get; set; }
        [SPResponseColumnName("cveCarrera")]
        public string ClaveCarrera { get; set; }
        [SPResponseColumnName("nombreCarrera")]
        public string NombreCarrera { get; set; }
        [SPResponseColumnName("fechaInicio")]
        public string FechaInicio { get; set; }
        [SPResponseColumnName("fechaTerminacion")]
        public string FechaTerminacion { get; set; }
        [SPResponseColumnName("nombre")]
        public string NombrePersona { get; set; }
        [SPResponseColumnName("primerApellido")]
        public string PrimerApellidoPersona { get; set; }
        [SPResponseColumnName("segundoApellido")]
        public string SegundoApellidoPersona { get; set; }
        [SPResponseColumnName("Profesionista_curp")]
        public string CurpPersona { get; set; }
        [SPResponseColumnName("correoElectronico")]
        public string CorreoPersona { get; set; }
        [SPResponseColumnName("numeroRvoe")]
        public string RVOE { get; set; }
        [SPResponseColumnName("idEntidadFederativa")]
        public string IdEntidadFederativa { get; set; }
        [SPResponseColumnName("entidadFederativa")]
        public string EntidadFederativa { get; set; }
    }
    [SPName("P_OBTEN_ESCUELA_PROC_TTIEL")]
    public class ModelObtenEscuelaProcTTIELRequest : BaseModelRequest
    {
        [Required]
        [SPParameterName("P_MATRICULA", 0)]
        public string Matricula { get; set; }
        [Required]
        [SPParameterName("P_PROGRAMA", 1)]
        public string Programa { get; set; }
    }
    public class ModelObtenEscuelaProcTTIELResponse : BaseModelResponse
    {
        [SPResponseColumnName("institucionProcedencia")]
        public string NombreEscuelaProc { get; set; }
        [SPResponseColumnName("idEntidadFederativa_esc")]
        public string IdEntidadFederativaEscuelaProc { get; set; }
        [SPResponseColumnName("entidadFederativa_esc")]
        public string DescripcionEntidadFederativaEscuelaProc { get; set; }

    }
    [SPName("P_OBTEN_FOLIO_TTIEL")]
    public class ModelObtenFolioTTIELRequest : BaseModelRequest
    {
        [Required]
        [SPParameterName("P_MATRICULA", 0)]
        public string Matricula { get; set; }
        [Required]
        [SPParameterName("P_PROGRAMA", 1)]
        public string Programa { get; set; }
    }
    public class ModelObtenFolioTTIELResponse : BaseModelResponse
    {
        [SPResponseColumnName("folio")]
        public string Folio { get; set; }
    }

    [SPName("P_INSERTA_TTIEL")]
    public class ModelInsertaTTIELResponse : BaseModelRequest
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
        [SPParameterName("P_USER", 4)]
        public string Usuario { get; set; }
        [SPParameterName("P_XML", 5)]
        public string XML { get; set; }
        
    }
}
