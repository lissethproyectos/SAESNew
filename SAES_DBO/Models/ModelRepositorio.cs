using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAES_DBO.Models
{

    public class ModelRepositorio
    {
        [SPName("Obtener_Listado_Alumnos")]
        public class ModelStatusRepositorioRequest : BaseModelRequest
        {
            [Required]
            [SPParameterName("Rol", 0)]
            public string Rol { get; set; }

            [SPParameterName("Status", 1)]
            public string Status { get; set; }

            [SPParameterName("User", 2)]
            public string User { get; set; }
        }
        public class ModelStatusRepositorioResponse : BaseModelResponse
        {
            [SPResponseColumnName("IDAlumno")]
            public string IDAlumno { get; set; }

            [SPResponseColumnName("Nombre")]
            public string Nombre { get; set; }

            [SPResponseColumnName("Campus")]
            public string Campus { get; set; }

            [SPResponseColumnName("Nivel")]
            public string Nivel { get; set; }

            [SPResponseColumnName("Programa")]
            public string Programa { get; set; }

            [SPResponseColumnName("Estatus_Alumno")]
            public string Estatus_Alumno { get; set; }

            [SPResponseColumnName("FechaRegistro")]
            public string FechaRegistro { get; set; }

            [SPResponseColumnName("FechaUltNotificacion")]
            public string FechaUltNotificacion { get; set; }

            [SPResponseColumnName("NoAlerta")]
            public string NoAlerta { get; set; }

            [SPResponseColumnName("Modalidad")]
            public string Modalidad { get; set; }
        }


        [SPName("Obtener_Listado_Documentos_Alumno")]
        public class ModelDocumentosAlumnoRequest : BaseModelRequest
        {
            [Required]
            [SPParameterName("IDAlumno_in", 0)]
            public string IDAlumno_in { get; set; }

            [SPParameterName("Rol", 1)]
            public string Rol { get; set; }

        }
        public class ModelDocumentosAlumnoResponse : BaseModelResponse
        {
            [SPResponseColumnName("IDTipoDocumento")]
            public string IDTipoDocumento { get; set; }

            [SPResponseColumnName("IDDocumento")]
            public string IDDocumento { get; set; }

            [SPResponseColumnName("IDAlumno")]
            public string IDAlumno { get; set; }

            [SPResponseColumnName("Documento")]
            public string Documento { get; set; }

            [SPResponseColumnName("estatus")]
            public string estatus { get; set; }

            [SPResponseColumnName("limite")]
            public string limite { get; set; }

            [SPResponseColumnName("Comentarios")]
            public string Comentarios { get; set; }

            [SPResponseColumnName("Minimo")]
            public string Minimo { get; set; }

            [SPResponseColumnName("Maximo")]
            public string Maximo { get; set; }

            [SPResponseColumnName("Formato")]
            public string Formato { get; set; }
        }


        [SPName("P_QRY_AVANCE_DOCUMENTOS")]
        public class ModelObtenerDoctosEntregadosRequest : BaseModelRequest
        {
            [SPParameterName("P_IDAlumno", 0)]
            public string IDAlumno { get; set; }
        }
        public class ModelObtenerDoctosEntregadosResponse : BaseModelResponse
        {
            [SPResponseColumnName("TotDoctos")]
            public string TotDoctos { get; set; }

            [SPResponseColumnName("TotEntregados")]
            public string TotEntregados { get; set; }
        }


        [SPName("P_QRY_AVANCE_DOCUMENTOS_ALUMNO")]
        public class ModelObtenerAvanceAlumnoRequest : BaseModelRequest
        {
            [SPParameterName("P_Matricula", 0)]
            public string Matricula { get; set; }
        }
        public class ModelObtenerAvanceAlumnoResponse : BaseModelResponse
        {
            [SPResponseColumnName("Documentos")]
            public string Documentos { get; set; }

            [SPResponseColumnName("Entregados")]
            public string Entregados { get; set; }
        }



        [SPName("P_QRY_TIPO_DOCTO")]
        public class ModelObtenerTipoDoctosRequest : BaseModelRequest
        {
            [SPParameterName("P_Tipo", 0)]
            public string Tipo { get; set; }
        }
        public class ModelObtenerTipoDoctosResponse : BaseModelResponse
        {
            [SPResponseColumnName("Formato")]
            public string Formato { get; set; }

            [SPResponseColumnName("TamanoMinimo")]
            public string TamanoMinimo { get; set; }

            [SPResponseColumnName("TamanoMaximo")]
            public string TamanoMaximo { get; set; }
        }

        [SPName("P_UPD_CAMBIA_STATUS_DOCTO")]
        public class ModelCambiaStatusRequest : BaseModelRequest
        {
            [SPParameterName("P_Matricula", 0)]
            public string Matricula { get; set; }
        }
        public class ModelCambiaStatusResponse : BaseModelResponse
        {
            [SPResponseColumnName("IDDocumento")]
            public string IDDocumento { get; set; }

            [SPResponseColumnName("IDEstatusDocumento")]
            public string IDEstatusDocumento { get; set; }

            [SPResponseColumnName("Documento")]
            public string Documento { get; set; }

            [SPResponseColumnName("FechaUltModif")]
            public string FechaUltModif { get; set; }
        }

        [SPName("P_UPD_DOCTOS_ALUMNO")]
        public class ModelEditarDoctosAlumnoRequest : BaseModelRequest
        {
            [SPParameterName("P_ID_DOCTO", 0)]
            public string ID_DOCTO { get; set; }
        }
        
    }
}
