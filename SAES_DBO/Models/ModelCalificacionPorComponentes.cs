using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAES_DBO.Models
{
    [SPName("P_OBTEN_ALUMNOS")]
    public class ModelObtenAlumnosRequest : BaseModelRequest
    {
        [Required]
        [SPParameterName("P_PERIODO", 0)]
        public string Periodo { get; set; }
        [Required]
        [SPParameterName("P_CAMPUS", 1)]
        public string Campus { get; set; }
        [Required]
        [SPParameterName("P_MATERIA", 2)]
        public string Materia { get; set; }
        [Required]
        [SPParameterName("P_GRUPO", 3)]
        public string Grupo { get; set; }
        [Required]
        [SPParameterName("P_COMPONENTE", 4)]
        public string Componente { get; set; }
    }
    public class ModelObtenAlumnosResponse : BaseModelResponse
    {
        [SPResponseColumnName("MATRICULA")]
        public string Matricula { get; set; }
        [SPResponseColumnName("NOMBRE")]
        public string Nombre { get; set; }
        [SPResponseColumnName("CLAVE")]
        public string Clave { get; set; }
        [SPResponseColumnName("NOM_PROGRAMA")]
        public string Programa { get; set; }
        [SPResponseColumnName("CALIFICACION")]
        public string Calificacion { get; set; }
        [SPResponseColumnName("FECHA")]
        public string Fecha { get; set; }
    }

    [SPName("P_OBTEN_BORRA_ACTUALIZA")]
    public class ModelCalificacionPorComponentesForInsertRequest : BaseModelRequest
    {
        [Required]
        [SPParameterName("P_PERIODO", 0)]
        public string Periodo { get; set; }
        [SPParameterName("P_CAMPUS", 1)]
        public string Campus { get; set; }
        [SPParameterName("P_MATERIA", 2)]
        public string Materia { get; set; }
        [SPParameterName("P_GRUPO", 3)]
        public string Grupo { get; set; }
        [SPParameterName("P_COMPONENTE", 4)]
        public string Componente { get; set; }
        [SPParameterName("P_PERS_ID", 5)]
        public string Matricula { get; set; }
        [SPParameterName("P_CLAVE_PROG", 6)]
        public string Programa { get; set; }
        [SPParameterName("P_CALI_CLAVE", 7)]
        public string Calificacion { get; set; }
        [SPParameterName("P_USER", 8)]
        public string Usuario { get; set; }
    }
}
