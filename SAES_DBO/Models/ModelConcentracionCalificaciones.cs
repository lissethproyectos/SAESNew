using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAES_DBO.Models
{
    [SPName("P_INSERTA_CALI")]
    public class ModelConcentracionCalificacionesForInsertRequest : BaseModelRequest
    {
        [Required]
        [SPParameterName("P_PERIODO", 0)]
        public string Periodo { get; set; }
        [SPParameterName("P_CAMPUS", 1)]
        public string Campus { get; set; }
        [SPParameterName("P_NIVEL", 2)]
        public string Nivel { get; set; }
        [SPParameterName("P_PROGRAMA", 3)]
        public string Programa { get; set; }
        [SPParameterName("P_MATERIA", 4)]
        public string Materia { get; set; }
        [SPParameterName("P_GRUPO", 5)]
        public string Grupo { get; set; }
        [SPParameterName("p_user", 6)]
        public string User { get; set; }
    }
}
