using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAES_DBO.Models
{
    [SPName("P_BORRA_ACTUALIZA_INSS")]
    public class ModelRegistroInasistenciasForDeleteRequest : BaseModelRequest
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
        [SPParameterName("P_FECHA", 4)]
        public string Fecha { get; set; }
    }

    [SPName("P_INSERTA_INSS")]
    public class ModelRegistroInasistenciasForInsertRequest : BaseModelRequest
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
        [SPParameterName("P_FECHA", 4)]
        public string Fecha { get; set; }
        [SPParameterName("P_PERS_ID", 5)]
        public string Matricula { get; set; }
        [SPParameterName("P_USER", 6)]
        public string Usuario { get; set; }
        [SPParameterName("P_PROGRAMA", 7)]
        public string Programa { get; set; }
    }

    [SPName("P_OBTEN_ALUMNOS_INSCRITOS")]
    public class ModelObtenAlumnosInscritosRequest : BaseModelRequest
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
        [SPParameterName("P_FECHA", 4)]
        public string Fecha { get; set; }
    }
    public class ModelObtenAlumnosInscritosResponse : BaseModelResponse
    {
        [SPResponseColumnName("MATRICULA")]
        public string Matricula { get; set; }
        [SPResponseColumnName("NOMBRE")]
        public string Nombre { get; set; }
        [SPResponseColumnName("CLAVE")]
        public string Clave { get; set; }
        [SPResponseColumnName("NOM_PROGRAMA")]
        public string Programa { get; set; }
        [SPResponseColumnName("INASISTENCIA")]
        public string Inasistencia { get; set; } //Cuando venga el valor I prender el check
    }

    [SPName("P_VALIDA_FECHA")]
    public class ModelValidaFechaRegistoInasistenciasRequest : BaseModelRequest
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
        [SPParameterName("P_FECHA", 4)]
        public string Fecha { get; set; }
    }
    public class ModelValidaFechaRegistoInasistenciasResponse : BaseModelResponse
    {
        [SPResponseColumnName("RESPUESTA")]
        public string Respuesta { get; set; }        
    }
}
