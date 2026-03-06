using SAES_DBO.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAES_Services
{
    public class Devoluciones : Methods
    {
        public Devoluciones() : base() {
        }
        
        public DataTable obtenParametrosDevolucion(string periodo, string campus,string nivel)
        {
            ModelParametrosDevolucionFiltersRequest request = new ModelParametrosDevolucionFiltersRequest() { Periodo = periodo, Campus = campus, Nivel = nivel };
            List<ModelParametrosDevolucionFiltersResponse> response = DB.CallSPListResult<ModelParametrosDevolucionFiltersResponse, ModelParametrosDevolucionFiltersRequest>(request);
            return ToDataTable(response);
        }

        public string InsertParametroDevolucion(string periodo, string campus, string nivel, decimal pocBaja, string FechaInicio, string FechaFin, string usuario)
        {
            ModelParametrosDevolucionForInsertRequest req = new ModelParametrosDevolucionForInsertRequest();
            req.Periodo = periodo;
            req.Campus = campus;
            req.Nivel = nivel;
            req.Porcentaje = pocBaja;
            req.FechaInicio = FechaInicio;
            req.FechaFin = FechaFin;
            req.User = usuario;
            return DB.CallSPForInsertUpdate(req);
        }

        public string UpdateParametroDevolucion(int consecutivo, decimal pocBaja, string FechaInicio, string FechaFin, string usuario)
        {
            
                ModelParametrosDevolucionForUpdateRequest req = new ModelParametrosDevolucionForUpdateRequest();
                req.Consecutivo = consecutivo;
                req.Porcentaje = pocBaja;
                req.FechaInicio = FechaInicio;
                req.FechaFin = FechaFin;
                req.User = usuario;
                return DB.CallSPForInsertUpdate(req);

        }

    }
}
