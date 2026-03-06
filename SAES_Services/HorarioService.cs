using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SAES_DBO.Models.ModelHorario;
using static SAES_DBO.Models.ModelPlan;

namespace SAES_Services
{
    public class HorarioService : Methods
    {
        public DataTable ObtenerHorarioMateria(string periodo, string campus, string hora, string grupo, string salon)
        {
            ModelObtenerHorarioMateriaRequest request = new ModelObtenerHorarioMateriaRequest() { 
                P_Periodo = periodo,
                P_Campus = campus,
                P_Hora= hora,
                P_Grupo= grupo,
                P_Salon= salon
            };
            List<ModelObtenerHorarioMateriaResponse> response = DB.CallSPListResult<ModelObtenerHorarioMateriaResponse, ModelObtenerHorarioMateriaRequest>(request);
            return ToDataTable(response);
        }
    }
}
