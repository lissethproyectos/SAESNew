using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SAES_DBO.Models.ModelAlumno;
using static SAES_DBO.Models.ModelContacto;

namespace SAES_Services
{
    public class ContactoService : Methods
    {
        public DataTable ObtenerDirecciones(string P_Matricula)
        {
            ModelObtenerDireccionesRequest request = new ModelObtenerDireccionesRequest() { Matricula = P_Matricula };
            List<ModelObtenerDireccionesResponse> response = DB.CallSPListResult<ModelObtenerDireccionesResponse, ModelObtenerDireccionesRequest>(request);
            return ToDataTable(response);
        }
    }
}
