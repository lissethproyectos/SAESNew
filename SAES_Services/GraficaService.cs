using SAES_DBA;
using SAES_DBO.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SAES_DBO.Models.ModelGrafica;

namespace SAES_Services
{
    public class GraficaService : Methods
    {
        public List<ModelObtenGraficaAdeudoResponse> obtenerDatosGraficaAdeudo(string P_Tipo_Grafica, string P_Periodo, string P_Campus, string P_Nivel)
        {
            //ModelObtenGraficaAdeudoRequest request = new ModelObtenGraficaAdeudoRequest() { };
            //List<ModelObtenGraficaAdeudoResponse> response = DB.CallSPListResult<ModelObtenGraficaAdeudoResponse, ModelObtenGraficaAdeudoRequest>(request);
            //return response;

            ModelObtenGraficaAdeudoRequest request = new ModelObtenGraficaAdeudoRequest() { Tipo_Grafica= P_Tipo_Grafica, Periodo= P_Periodo, Campus=P_Campus, Nivel=P_Nivel };
            List<ModelObtenGraficaAdeudoResponse> response = DB.CallSPListResult<ModelObtenGraficaAdeudoResponse, ModelObtenGraficaAdeudoRequest>(request);
            return response;
        }

        public List<ModelObtenGraficaPronosticoIngresoResponse> obtenerDatosGraficaPronosticoIngreso(string Tipo, string P_Turno, string P_Periodo, string P_Campus, string P_Nivel, string P_Programa)
        {
            ModelObtenGraficaPronosticoIngresoRequest request = new ModelObtenGraficaPronosticoIngresoRequest() { Tipo = Tipo, Turno = P_Turno, Periodo = P_Periodo, Campus = P_Campus, Nivel = P_Nivel, Programa=P_Programa };
            List<ModelObtenGraficaPronosticoIngresoResponse> response = DB.CallSPListResult<ModelObtenGraficaPronosticoIngresoResponse, ModelObtenGraficaPronosticoIngresoRequest>(request);
            return response;
        }

        public List<ModelObtenGraficaPronosticoReingresoResponse> obtenerDatosGraficaPronosticoReIngreso(string Tipo, string P_Turno, string P_Periodo, string P_Campus, string P_Nivel, string P_Programa)
        {
            ModelObtenGraficaPronosticoReingresoRequest request = new ModelObtenGraficaPronosticoReingresoRequest() { Tipo = Tipo, Turno = P_Turno, Periodo = P_Periodo, Campus = P_Campus, Nivel = P_Nivel, Programa = P_Programa };
            List<ModelObtenGraficaPronosticoReingresoResponse> response = DB.CallSPListResult<ModelObtenGraficaPronosticoReingresoResponse, ModelObtenGraficaPronosticoReingresoRequest>(request);
            return response;
        }

        public List<ModelObtenGraficaBecasResponse> obtenerDatosGraficaBecas(string P_Tipo, string P_Clasificacion, string P_Periodo, string P_Campus, string P_Nivel, string P_Programa)
        {
            ModelObtenGraficaBecasRequest request = new ModelObtenGraficaBecasRequest() { Tipo = P_Tipo, Clasificacion = P_Clasificacion, Periodo = P_Periodo, Campus = P_Campus, Nivel = P_Nivel, Programa = P_Programa };
            List<ModelObtenGraficaBecasResponse> response = DB.CallSPListResult<ModelObtenGraficaBecasResponse, ModelObtenGraficaBecasRequest>(request);
            return response;
        }

        public List<ModelObtenGraficaCtasporCobrarResponse> obtenerDatosGraficaCuentasporCobrar(string P_Tipo, string P_Tipo_Pago, string P_Periodo, string P_Campus, string P_Nivel, string P_Programa)
        {
            ModelObtenGraficaCtasporCobrarRequest request = new ModelObtenGraficaCtasporCobrarRequest() { Tipo = P_Tipo, Tipo_Pago = P_Tipo_Pago, Periodo = P_Periodo, Campus = P_Campus, Nivel = P_Nivel, Programa = P_Programa };
            List<ModelObtenGraficaCtasporCobrarResponse> response = DB.CallSPListResult<ModelObtenGraficaCtasporCobrarResponse, ModelObtenGraficaCtasporCobrarRequest>(request);
            return response;
        }
        public string CrearPronostico(string Periodo)
        {
            ModelCrearGrafica crear = new ModelCrearGrafica()
            {
                Periodo = Periodo
            };
            return DB.CallSPForInsertUpdate(crear);
        }
    }
}
