using SAES_DBO.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SAES_DBO.Models.ModelDocente;
using static SAES_DBO.Models.ModelFinanzas;

namespace SAES_Services
{
    public class FinanzasService : Methods
    {

        public DataTable obtenDescuentosAlumno(string P_Periodo, string P_Campus, string P_Programa, string P_Nivel, string P_Concepto,
            string P_Beca, string P_Estatus)
        {
            ModelDescuentosAlumnoRequest request = new ModelDescuentosAlumnoRequest() { Periodo= P_Periodo, Campus = P_Campus, 
                Programa = P_Programa, 
                Nivel = P_Nivel, 
                Concepto= P_Concepto,
                Beca= P_Beca,
                Estatus= P_Estatus
            };
            List<ModelDescuentosAlumnoResponse> response = DB.CallSPListResult<ModelDescuentosAlumnoResponse, ModelDescuentosAlumnoRequest>(request);
            return ToDataTable(response);
        }

        public DataTable obtenConceptosPagados(string P_Matricula, string P_Programa)
        {
            ModelConceptosPagadosRequest request = new ModelConceptosPagadosRequest() { Matricula= P_Matricula,  Programa = P_Programa };
            List <ModelConceptosPagadosResponse> response = DB.CallSPListResult<ModelConceptosPagadosResponse, ModelConceptosPagadosRequest>(request);
            return ToDataTable(response);
        }

        public DataTable obtenConceptosCartera(string P_Matricula, string P_Programa, string P_Periodo)
        {
            ModelConceptosCarteraRequest request = new ModelConceptosCarteraRequest() { Matricula = P_Matricula, Programa = P_Programa, Periodo= P_Periodo };
            List<ModelConceptosCarteraResponse> response = DB.CallSPListResult<ModelConceptosCarteraResponse, ModelConceptosCarteraRequest>(request);
            return ToDataTable(response);
        }
        public string DesaplicarPagos(string P_Matricula, string P_Programa, string P_Periodo, string P_Factura,
           string P_Factura_Cons, string P_Cartera_Cons, string P_Importe, string P_Usuario)
        {
            ModelUpdatePagoAlu Update = new ModelUpdatePagoAlu()
            {
                Matricula = P_Matricula,
                Programa = P_Programa,
                Periodo = P_Periodo,
                Factura = P_Factura,
                Factura_Cons = P_Factura_Cons,
                Cartera_Cons = P_Cartera_Cons,
                Importe= P_Importe,
                Usuario= P_Usuario
            };
            return DB.CallSPForInsertUpdate(Update);

        }

        public string CancelarPagos(string P_Matricula, string P_Programa, string P_Periodo, string P_Usuario,
    string P_Consecutivo, string P_Balance, string P_Concepto_Cargo)
        {
            ModelCancelarPagoAlu Update = new ModelCancelarPagoAlu()
            {
                Matricula = P_Matricula,
                Programa = P_Programa,
                Periodo = P_Periodo,
                Usuario = P_Usuario,
                Consecutivo = P_Consecutivo,
                Balance = P_Balance,
                Concepto_Cargo = P_Concepto_Cargo
            };
            return DB.CallSPForInsertUpdate(Update);

        }
    }
}
