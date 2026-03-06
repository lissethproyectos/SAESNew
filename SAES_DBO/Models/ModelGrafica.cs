using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAES_DBO.Models
{
    public class ModelGrafica
    {
        [SPName("P_QRY_DASHBOARD_CARTERA_VENCIDA")]
        //ObtenerListaDatosDashboard
        public class ModelObtenGraficaAdeudoRequest : BaseModelRequest
        {
            [Required]
            [SPParameterName("P_Tipo_Grafica", 0)]
            public string Tipo_Grafica { get; set; }

            [SPParameterName("P_Periodo", 1)]
            public string Periodo { get; set; }

            [SPParameterName("P_Campus", 2)]
            public string Campus { get; set; }

            [SPParameterName("P_Nivel", 3)]
            public string Nivel { get; set; }
        }
        public class ModelObtenGraficaAdeudoResponse : BaseModelResponse
        {
            [SPResponseColumnName("Nombre")]
            public string Nombre { get; set; }

            [SPResponseColumnName("Por_Cobrar")]
            public string Por_Cobrar { get; set; }

            [SPResponseColumnName("Vencido")]
            public string Vencido { get; set; }

            [SPResponseColumnName("Porcentaje")]
            public string Porcentaje { get; set; }

            [SPResponseColumnName("Clave")]
            public string Clave { get; set; }
        }

        [SPName("P_QRY_DASHBOARD_PRONOSTICO_NI")]
        public class ModelObtenGraficaPronosticoIngresoRequest : BaseModelRequest
        {
            [Required]
            [SPParameterName("P_Tipo", 0)]
            public string Tipo { get; set; }

            [SPParameterName("P_Turno", 1)]
            public string Turno { get; set; }

            [SPParameterName("P_Periodo", 2)]
            public string Periodo { get; set; }

            [SPParameterName("P_Campus", 3)]
            public string Campus { get; set; }

            [SPParameterName("P_Nivel", 4)]
            public string Nivel { get; set; }

            [SPParameterName("P_Programa", 5)]
            public string Programa { get; set; }

            
        }
        public class ModelObtenGraficaPronosticoIngresoResponse : BaseModelResponse
        {
            [SPResponseColumnName("Periodo")]
            public string Periodo { get; set; }

            [SPResponseColumnName("Campus")]
            public string Campus { get; set; }


            [SPResponseColumnName("Nivel")]
            public string Nivel { get; set; }

            [SPResponseColumnName("Pronostico_Total")]
            public string Pronostico_Total { get; set; }

            [SPResponseColumnName("Registrados")]
            public string Registrados { get; set; }

            [SPResponseColumnName("Pronostico_Actual")]
            public string Pronostico_Actual { get; set; }

            [SPResponseColumnName("Turno")]
            public string Turno { get; set; }

            [SPResponseColumnName("Clave")]
            public string Clave { get; set; }

            [SPResponseColumnName("Diferencia")]
            public string Diferencia { get; set; }
        }

        [SPName("P_QRY_DASHBOARD_PRONOSTICO_RI")]
        public class ModelObtenGraficaPronosticoReingresoRequest : BaseModelRequest
        {
            [Required]
            [SPParameterName("P_Tipo", 0)]
            public string Tipo { get; set; }

            [SPParameterName("P_Turno", 1)]
            public string Turno { get; set; }

            [SPParameterName("P_Periodo", 2)]
            public string Periodo { get; set; }

            [SPParameterName("P_Campus", 3)]
            public string Campus { get; set; }

            [SPParameterName("P_Nivel", 4)]
            public string Nivel { get; set; }

            [SPParameterName("P_Programa", 5)]
            public string Programa { get; set; }


        }
        public class ModelObtenGraficaPronosticoReingresoResponse : BaseModelResponse
        {
            [SPResponseColumnName("Periodo")]
            public string Periodo { get; set; }

            [SPResponseColumnName("Campus")]
            public string Campus { get; set; }


            [SPResponseColumnName("Nivel")]
            public string Nivel { get; set; }

            [SPResponseColumnName("Pronostico_Total")]
            public string Pronostico_Total { get; set; }

            [SPResponseColumnName("Registros_RI")]
            public string Registros_RI { get; set; }

            [SPResponseColumnName("Pronostico_Actual")]
            public string Pronostico_Actual { get; set; }

            [SPResponseColumnName("Turno")]
            public string Turno { get; set; }

            [SPResponseColumnName("Clave")]
            public string Clave { get; set; }

            [SPResponseColumnName("Porcentaje")]
            public string Porcentaje { get; set; }

            [SPResponseColumnName("Programa")]
            public string Programa { get; set; }
        }

        [SPName("P_QRY_DASHBOARD_DESCUENTOS")]
        public class ModelObtenGraficaBecasRequest : BaseModelRequest
        {
            [Required]
            [SPParameterName("P_Tipo", 0)]
            public string Tipo { get; set; }

            [SPParameterName("P_Clasificacion", 1)]
            public string Clasificacion { get; set; }

            [SPParameterName("P_Periodo", 2)]
            public string Periodo { get; set; }

            [SPParameterName("P_Campus", 3)]
            public string Campus { get; set; }

            [SPParameterName("P_Nivel", 4)]
            public string Nivel { get; set; }

            [SPParameterName("P_Programa", 5)]
            public string Programa { get; set; }


        }
        public class ModelObtenGraficaBecasResponse : BaseModelResponse
        {
           
            [SPResponseColumnName("Clave")]
            public string Clave { get; set; }

            [SPResponseColumnName("Desc_Clave")]
            public string Desc_Clave { get; set; }

            [SPResponseColumnName("Becas_Inscripcion")]
            public string Becas_Inscripcion { get; set; }

            [SPResponseColumnName("Becas_Colegiatura")]
            public string Becas_Colegiatura { get; set; }

            [SPResponseColumnName("Total_Becas")]
            public string Total_Becas { get; set; }


        }

        [SPName("P_QRY_DASHBOARD_CUENTAS_POR_COBRAR")]
        public class ModelObtenGraficaCtasporCobrarRequest : BaseModelRequest
        {
            [Required]
            [SPParameterName("P_Tipo", 0)]
            public string Tipo { get; set; }

            [SPParameterName("P_Tipo_Pago", 1)]
            public string Tipo_Pago { get; set; }

            [SPParameterName("P_Periodo", 2)]
            public string Periodo { get; set; }

            [SPParameterName("P_Campus", 3)]
            public string Campus { get; set; }

            [SPParameterName("P_Nivel", 4)]
            public string Nivel { get; set; }

            [SPParameterName("P_Programa", 5)]
            public string Programa { get; set; }
        }
        public class ModelObtenGraficaCtasporCobrarResponse : BaseModelResponse
        {

            [SPResponseColumnName("Clave")]
            public string Clave { get; set; }

            [SPResponseColumnName("Clave2")]
            public string Clave2 { get; set; }

            [SPResponseColumnName("Desc_Clave")]
            public string Desc_Clave { get; set; }

            [SPResponseColumnName("Falta_x_pagar")]
            public string Falta_por_Cobrar { get; set; }

            [SPResponseColumnName("Pagado")]
            public string Pagado { get; set; }

            [SPResponseColumnName("CXC_Neto")]
            public string Cta_por_Cobrar { get; set; }

            [SPResponseColumnName("Por_Pagar")]
            public string Por_Pagar { get; set; }

            [SPResponseColumnName("CXC")]
            public string CXC { get; set; }

            [SPResponseColumnName("Descuentos")]
            public string Descuentos { get; set; }
        }
        public class Resultado_Comun
        {
            public bool error { get; set; }
            public string mensaje_error { get; set; }
            public List<ModelObtenGraficaAdeudoResponse> resultado { get; set; }
        }
        public class Resultado_Comun_Pronostico
        {
            public bool error { get; set; }
            public string mensaje_error { get; set; }
            public List<ModelObtenGraficaPronosticoIngresoResponse> resultado { get; set; }
        }
        public class Resultado_Comun_Pronostico_RI
        {
            public bool error { get; set; }
            public string mensaje_error { get; set; }
            public List<ModelObtenGraficaPronosticoReingresoResponse> resultado { get; set; }
        }
        public class Resultado_Comun_Becas
        {
            public bool error { get; set; }
            public string mensaje_error { get; set; }
            public List<ModelObtenGraficaBecasResponse> resultado { get; set; }
        }
        public class Resultado_Comun_CXC
        {
            public bool error { get; set; }
            public string mensaje_error { get; set; }
            public List<ModelObtenGraficaCtasporCobrarResponse> resultado { get; set; }
        }

        [SPName("p_pronostico_ri")]
        public class ModelCrearGrafica : BaseModelRequest
        {
            [SPParameterName("p_periodo", 0)]
            public string Periodo { get; set; }
          
        }

    }
}
