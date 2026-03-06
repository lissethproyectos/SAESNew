using SAES_DBO.Models;
using SAES_Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static SAES_DBO.Models.ModelGrafica;

namespace SAES_Apis.Controllers
{
    public class Dashboard1Controller : Controller
    {
        // GET: DashboardAdeudos
        #region <Variables>
        //Utilidades utils = new Utilidades();
        GraficaService serviceGrafica = new GraficaService();
        List<ModelObtenPaisesResponse> lstPaises = new List<ModelObtenPaisesResponse>();
        #endregion
        public JsonResult Adeudos(string Tipo_Grafica, string Periodo, string Campus, string Nivel)
        {
            List<ModelObtenGraficaAdeudoResponse> lst = new List<ModelObtenGraficaAdeudoResponse>();
            Resultado_Comun resultado = new Resultado_Comun();
            string Verificador = string.Empty;
            try
            {
                //lst = nominaService.ListTiposPersonal(ref Verificador);
                lst = serviceGrafica.obtenerDatosGraficaAdeudo(Tipo_Grafica, Periodo, Campus, Nivel);// ("1", "202065");
                if (lst.Count()>0)
                {
                    resultado.error = false;
                    resultado.resultado = lst;
                    resultado.mensaje_error = string.Empty;
                }
                else
                {
                    resultado.error = true;
                    resultado.resultado = null;
                    resultado.mensaje_error = Verificador;
                }
                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                resultado.error = false;
                resultado.resultado = null;
                resultado.mensaje_error = ex.Message;
                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult PronosticoIngreso(string Tipo, string Turno, string Periodo, string Campus, string Nivel, string Programa)
        {
            List<ModelObtenGraficaPronosticoIngresoResponse> lst = new List<ModelObtenGraficaPronosticoIngresoResponse>();
            Resultado_Comun_Pronostico resultado = new Resultado_Comun_Pronostico();
            string Verificador = string.Empty;
            try
            {
                lst = serviceGrafica.obtenerDatosGraficaPronosticoIngreso(Tipo, Turno, Periodo, Campus, Nivel, Programa);// ("1", "202065");
                //if (lst.Count() > 0)
                //{
                    resultado.error = false;
                    resultado.resultado = lst;
                    resultado.mensaje_error = string.Empty;
                //}
                //else
                //{
                //    resultado.error = true;
                //    resultado.resultado = null;
                //    resultado.mensaje_error = Verificador;
                //}
                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                resultado.error = true;
                resultado.resultado = null;
                resultado.mensaje_error = ex.Message;
                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult PronosticoReIngreso(string Tipo, string Turno, string Periodo, string Campus, string Nivel, string Programa)
        {
            List<ModelObtenGraficaPronosticoReingresoResponse> lst = new List<ModelObtenGraficaPronosticoReingresoResponse>();
            Resultado_Comun_Pronostico_RI resultado = new Resultado_Comun_Pronostico_RI();
            string Verificador = string.Empty;
            try
            {
                lst = serviceGrafica.obtenerDatosGraficaPronosticoReIngreso(Tipo, Turno, Periodo, Campus, Nivel, Programa);// ("1", "202065");
                                                                                                                         //if (lst.Count() > 0)
                                                                                                                         //{
                resultado.error = false;
                resultado.resultado = lst;
                resultado.mensaje_error = string.Empty;             
                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                resultado.error = true;
                resultado.resultado = null;
                resultado.mensaje_error = ex.Message;
                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult Becas(string Tipo, string Clasificacion, string Periodo, string Campus, string Nivel, string Programa)
        {
            List<ModelObtenGraficaBecasResponse> lst = new List<ModelObtenGraficaBecasResponse>();
            Resultado_Comun_Becas resultado = new Resultado_Comun_Becas();
            string Verificador = string.Empty;
            try
            {
                lst = serviceGrafica.obtenerDatosGraficaBecas(Tipo, Clasificacion, Periodo, Campus, Nivel, Programa);// ("1", "202065");
                                                                                                                           //if (lst.Count() > 0)
                                                                                                                           //{
                resultado.error = false;
                resultado.resultado = lst;
                resultado.mensaje_error = string.Empty;
                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                resultado.error = true;
                resultado.resultado = null;
                resultado.mensaje_error = ex.Message;
                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult CuentasPorCobrar(string Tipo, string Tipo_Pago, string Periodo, string Campus, string Nivel, string Programa)
        {
            List<ModelObtenGraficaCtasporCobrarResponse> lst = new List<ModelObtenGraficaCtasporCobrarResponse>();
            Resultado_Comun_CXC resultado = new Resultado_Comun_CXC();
            string Verificador = string.Empty;
            try
            {
                lst = serviceGrafica.obtenerDatosGraficaCuentasporCobrar(Tipo, Tipo_Pago, Periodo, Campus, Nivel, Programa);// ("1", "202065")                                                                                                                    //{
                resultado.error = false;
                resultado.resultado = lst;
                resultado.mensaje_error = string.Empty;
                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                resultado.error = true;
                resultado.resultado = null;
                resultado.mensaje_error = ex.Message;
                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
        }

    }
}