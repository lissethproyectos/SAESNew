using SAES_DBO.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls.WebParts;

namespace SAES_Services
{
    public class CatOpcionesTitulacion : Methods
    {
        public CatOpcionesTitulacion() : base() { }

        public DataTable ObtenOpcionesTitulacion()
        {
            ModelOpcionTitulacionRequest request = new ModelOpcionTitulacionRequest() { Nada = "" };
            List<ModelOpcionTitulacionResponse> response = DB.CallSPListResult<ModelOpcionTitulacionResponse, ModelOpcionTitulacionRequest>(request);
            return ToDataTable(response);
        }

        public DataTable ObtenerCampusTitulacion()
        {
            ModelObtenCampusTitulacionRequest request = new ModelObtenCampusTitulacionRequest() { };
            List<ModelObtenCampusTitulacionResponse> response = DB.CallSPListResult<ModelObtenCampusTitulacionResponse, ModelObtenCampusTitulacionRequest>(request);
            return ToDataTableForDropDownList(response);
        }

        public DataTable ObtenerPeriodosTitulacion()
        {
            ModelObtenPeriodosTitulacionRequest request = new ModelObtenPeriodosTitulacionRequest() { };
            List<ModelObtenPeriodosTitulacionResponse> response = DB.CallSPListResult<ModelObtenPeriodosTitulacionResponse, ModelObtenPeriodosTitulacionRequest>(request);
            return ToDataTableForDropDownList(response);
        }

        public ModelInsTretiResponse Ins_treti(string p_matricula, string p_treti_tprog_clave, string p_treti_ttiop_clave, string p_treti_status,
    string p_treti_tuser_clave, string p_treti_foja, string p_treti_libro, string p_treti_cedula, string p_nivel, string p_periodo, string p_campus)
        {
            ModelInsTreti Insert = new ModelInsTreti()
            {
                matricula = p_matricula,
                treti_tprog_clave = p_treti_tprog_clave,
                treti_ttiop_clave = p_treti_ttiop_clave,
                treti_status = p_treti_status,
                treti_tuser_clave = p_treti_tuser_clave,
                treti_foja= p_treti_foja,
                treti_libro= p_treti_libro,
                treti_cedula= p_treti_cedula,
                nivel= p_nivel,
                periodo= p_periodo,
                campus=p_campus
            };
            ModelInsTretiResponse response = DB.CallSPResult<ModelInsTretiResponse, ModelInsTreti>(Insert);
            return response;
        }

        public DataTable obtenGridTitulacion(string p_ttini_ttiop_clave)
        {
            ModelTtiniRequest request = new ModelTtiniRequest() { ttini_ttiop_clave = p_ttini_ttiop_clave };
            List<ModelTtiniResponse> response = DB.CallSPListResult<ModelTtiniResponse, ModelTtiniRequest>(request);
            return ToDataTable(response);
        }

        public string Upd_treti(string p_matricula, string p_treti_tprog_clave, string p_treti_ttiop_clave, string p_treti_status,
    string p_treti_tuser_clave, string p_treti_foja, string p_treti_libro, string p_treti_cedula, string p_treti_tpees_clave)
        {
            ModelUpdTreti Update = new ModelUpdTreti()
            {
                matricula = p_matricula,
                treti_tprog_clave = p_treti_tprog_clave,
                treti_ttiop_clave = p_treti_ttiop_clave,
                treti_status = p_treti_status,
                treti_tuser_clave = p_treti_tuser_clave,
                treti_foja = p_treti_foja,
                treti_libro = p_treti_libro,
                treti_cedula = p_treti_cedula,
                treti_tpees_clave= p_treti_tpees_clave
            };
            return DB.CallSPForInsertUpdate(Update);
        }

        public List<ModelRegTitulacionResponse> ObtenRegistroTitulacion(string P_Matricula, string P_Programa)
        {
            ModelRegTitulacionRequest request = new ModelRegTitulacionRequest() { Matricula = P_Matricula, Programa= P_Programa };
            List<ModelRegTitulacionResponse> response = DB.CallSPListResult<ModelRegTitulacionResponse, ModelRegTitulacionRequest>(request);
            return response;
        }

        public DataTable ObtenRegistroTitulacionDetalle(string P_Periodo, string P_Campus, string P_Programa, string P_Nivel, string P_Opc_Titulacion,
            string P_Estatus)
        {
            ModelRegTitulacionDetRequest request = new ModelRegTitulacionDetRequest() 
            { 
                Periodo = P_Periodo,
                Campus = P_Campus,
                Programa= P_Programa,
                Nivel= P_Nivel,
                Opc_Titulacion= P_Opc_Titulacion,
                Estatus= P_Estatus
            };
            List<ModelRegTitulacionDetResponse> response = DB.CallSPListResult<ModelRegTitulacionDetResponse, ModelRegTitulacionDetRequest>(request);
            return ToDataTable(response);
        }

        public List<ModelComun> ObtenStatusTreti()
        {
            List<ModelComun> lst=new List<ModelComun>();
            lst.Add(new ModelComun() { IdStr = "I", Descripcion = "Iniciado" });
            lst.Add(new ModelComun() { IdStr = "R", Descripcion = "Trámite" });
            lst.Add(new ModelComun() { IdStr = "B", Descripcion = "Baja" });
            lst.Add(new ModelComun() { IdStr = "T", Descripcion = "Terminado" });
            return lst;
        }

        public DataTable ObtenOpcionesTitulacionDetalle(string clave)
        {
            ModelOpcionTitulacionDetRequest request = new ModelOpcionTitulacionDetRequest() { Clave = clave };
            List<ModelOpcionTitulacionDetResponse> response = DB.CallSPListResult<ModelOpcionTitulacionDetResponse, ModelOpcionTitulacionDetRequest>(request);
            return ToDataTable(response);
        }

        public DataTable ObtenOpcionesTitulacionNiveles()
        {
            ModelOpcionTitulacionNivelRequest request = new ModelOpcionTitulacionNivelRequest() { Nada = "" };
            List<ModelOpcionTitulacionNivelResponse> response = DB.CallSPListResult<ModelOpcionTitulacionNivelResponse, ModelOpcionTitulacionNivelRequest>(request);
            return ToDataTable(response);
        }

        public DataTable ObtenOpcionesTitulacionCodigos()
        {
            ModelOpcionTitulacionCodigoRequest request = new ModelOpcionTitulacionCodigoRequest() { Nada = "" };
            List<ModelOpcionTitulacionCodigoResponse> response = DB.CallSPListResult<ModelOpcionTitulacionCodigoResponse, ModelOpcionTitulacionCodigoRequest>(request);
            return ToDataTable(response);
        }

        public string BorraOpcionesTitulacion(string ClaveTitulacion)
        {
            ModelBorraOpcionTitulacionResponse borrar = new ModelBorraOpcionTitulacionResponse() { Clave = ClaveTitulacion };            
            return DB.CallSPForInsertUpdate(borrar);
        }

        public string ActualizaEncabezadoOpcionesTitulacion(string claveTitulacion, string claveCodigo, string creditos, string descripcion, string estatus, string nivel, string promedio, string usuario)
        {
            ModelInsertaOpcionTitulacionResponse Insert = new ModelInsertaOpcionTitulacionResponse() {
                Clave = claveTitulacion,
                ClaveCodigo = claveCodigo,
                Creditos = creditos,
                Descripcion = descripcion,
                Estatus = estatus,
                Nivel = nivel,
                Promedio = promedio,
                Usuario = usuario
            };
            return DB.CallSPForInsertUpdate(Insert);
        }
        public string ActualizaDetalleOpcionesTitulacion(string claveTitulacion, string claveCodigo, string creditos, string nivel, string promedio, string usuario)
        {
            ModelInsertaDetalleOpcionTitulacionResponse Insert = new ModelInsertaDetalleOpcionTitulacionResponse()
            {
                Clave = claveTitulacion,
                ClaveCodigo = claveCodigo,
                Creditos = creditos,
                Nivel = nivel,
                Promedio = promedio,
                Usuario = usuario
            };
            return DB.CallSPForInsertUpdate(Insert);
        }
    }
}
