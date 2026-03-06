using ApisSysweb.Interface;
using ApisSysweb.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApisSysweb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngresosController : ControllerBase
    {
        ICoinService reciboService;

        public IngresosController(ICoinService _reciboService)
        {
            reciboService = _reciboService;
        }
        // GET: IngresosController
        [HttpGet("PagosBanco", Name = "PagosBanco")]

        public IActionResult PagosBanco()
        {
            string Verificador = string.Empty;
            List<Grafica> lstDatos = new List<Grafica>();          
            Resultado_Grafica objResultado = new Resultado_Grafica();
            try
            {
                lstDatos=reciboService.ListPagosBanco(ref Verificador);
                objResultado.Error = false;
                objResultado.Mensaje_Error = string.Empty;
                objResultado.Resultado = lstDatos;
                //return Json(objResultado, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                objResultado.Error = true;
                objResultado.Mensaje_Error = ex.Message;
                objResultado.Resultado = null;
                //return Json(objResultado, JsonRequestBehavior.AllowGet);
            }



            return new JsonResult(objResultado);
        }


        [HttpGet("PagosporEjercicio", Name = "PagosporEjercicio")]
        public IActionResult PagosporEjercicio()
        { 
            string Verificador = string.Empty;
            List<Grafica> lstDatos = new List<Grafica>();
            Resultado_Grafica objResultado = new Resultado_Grafica();
            try
            {
                lstDatos = reciboService.ListPagosporEjercicio(ref Verificador);
                objResultado.Error = false;
                objResultado.Mensaje_Error = string.Empty;
                objResultado.Resultado = lstDatos;
            }
            catch (Exception ex)
            {
                objResultado.Error = true;
                objResultado.Mensaje_Error = ex.Message;
                objResultado.Resultado = null;
            }
            return new JsonResult(objResultado);
        }

    }
}
