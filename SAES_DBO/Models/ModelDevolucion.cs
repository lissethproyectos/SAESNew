using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAES_DBO.Models
{
    [SPName("p_obten_tbapa")]
    public class ModelParametrosDevolucionFiltersRequest : BaseModelRequest
    {
        [Required]
        [SPParameterName("p_term", 0)]
        public string Periodo { get; set; }
        [Required]
        [SPParameterName("p_campus", 1)]
        public string Campus { get; set; }
        [Required]
        [SPParameterName("p_nivel", 2)]
        public string Nivel { get; set; }
    }
    public class ModelParametrosDevolucionFiltersResponse : BaseModelResponse
    {
        string _fechaInicio = string.Empty;
        string _fechaFin = string.Empty;
        [SPResponseColumnName("Consecutivo")]
        public string Consecutivo { get; set; }
        [SPResponseColumnName("Porcentaje")]
        public string Porcentaje { get; set; }
        [SPResponseColumnName("fecha_ini")]
        public string FechaInicio { get {
                DateTime date = Convert.ToDateTime(_fechaInicio);
                string result = date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                return result; 
            } set { 
                _fechaInicio = value; 
            } 
        }
        [SPResponseColumnName("fecha_fin")]
        public string FechaFin { get 
            {
                DateTime date = Convert.ToDateTime(_fechaFin);
                string result = date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                return result;
            } set { _fechaFin = value; } }
    }

    [SPName("p_dml_porcentaje")]
    public class ModelParametrosDevolucionForInsertRequest : BaseModelRequest
    {
        [Required]
        [SPParameterName("p_periodo", 0)]
        public string Periodo { get; set; }
        [Required]
        [SPParameterName("p_campus", 1)]
        public string Campus { get; set; }
        [Required]
        [SPParameterName("p_nivel", 2)]
        public string Nivel { get; set; }
        [Required]
        [SPParameterName("p_porcentaje", 3)]
        public decimal Porcentaje { get; set; }
        [Required]
        [SPParameterName("p_fecha_ini", 4)]
        public string FechaInicio { get; set; }
        [Required]
        [SPParameterName("p_fecha_fin", 5)]
        public string FechaFin { get; set; }
        [Required]
        [SPParameterName("p_user", 6)]
        public string User { get; set; }
    }

    [SPName("p_update_tbapa")]
    public class ModelParametrosDevolucionForUpdateRequest : BaseModelRequest
    {
        [Required]
        [SPParameterName("p_consecutivo", 0)]
        public int Consecutivo { get; set; }
        [Required]
        [SPParameterName("p_porcentaje", 1)]
        public decimal Porcentaje { get; set; }
        [Required]
        [SPParameterName("p_fecha_ini", 2)]
        public string FechaInicio { get; set; }
        [Required]
        [SPParameterName("p_fecha_fin", 3)]
        public string FechaFin { get; set; }
        [Required]
        [SPParameterName("p_user", 4)]
        public string User { get; set; }
    }

}
