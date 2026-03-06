using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAES_DBO.Models
{
    public class ModelHorario
    {
        [SPName("P_QRY_HORA_MATERIA")]
        public class ModelObtenerHorarioMateriaRequest : BaseModelRequest
        {
            [SPParameterName("P_Periodo", 0)]
            public string P_Periodo { get; set; }

            [SPParameterName("P_Campus", 1)]
            public string P_Campus { get; set; }

            [SPParameterName("P_Hora", 2)]
            public string P_Hora { get; set; }

            [SPParameterName("P_Grupo", 3)]
            public string P_Grupo { get; set; }

            [SPParameterName("P_Salon", 4)]
            public string P_Salon { get; set; }
        }
        public class ModelObtenerHorarioMateriaResponse : BaseModelResponse
        {
            [SPResponseColumnName("dia")]
            public string dia { get; set; }

            [SPResponseColumnName("inicio")]
            public string inicio { get; set; }

            [SPResponseColumnName("fin")]
            public string fin { get; set; }

            [SPResponseColumnName("thocl_clave")]
            public string thocl_clave { get; set; }

            [SPResponseColumnName("thocl_clave2")]
            public string thocl_clave2 { get; set; }
        }
    }
}
