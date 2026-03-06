using Microsoft.SqlServer.Server;
using SAES_DBO.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using static SAES_DBO.Models.ModelCobranza;
using static SAES_DBO.Models.ModelServicioSocial;

namespace SAES_Services
{
    public class ServicioSocialService : Methods
    {

        public DataTable ObtenerPeriodosServicioSocial()
        {
            ModelObtenPeriodosServicioSocialRequest request = new ModelObtenPeriodosServicioSocialRequest() { };
            List<ModelObtenPeriodosServicioSocialResponse> response = DB.CallSPListResult<ModelObtenPeriodosServicioSocialResponse, ModelObtenPeriodosServicioSocialRequest>(request);
            return ToDataTableForDropDownList(response);
        }

        public DataTable ObtenerProgramasServicioSocial()
        {
            ModelObtenPeriodosServicioSocialRequest request = new ModelObtenPeriodosServicioSocialRequest() { };
            List<ModelObtenPeriodosServicioSocialResponse> response = DB.CallSPListResult<ModelObtenPeriodosServicioSocialResponse, ModelObtenPeriodosServicioSocialRequest>(request);
            return ToDataTableForDropDownList(response);
        }

        public DataTable Qry_Tprss()
        {
            ModelInsertarTprss request = new ModelInsertarTprss() { };
            List<ModelInsertarTprssResponse> response = DB.CallSPListResult<ModelInsertarTprssResponse, ModelInsertarTprss>(request);
            return ToDataTable(response);
        }

        public DataTable ObtenRegistroServicioSocial(string P_Campus, string P_Nivel, string P_Programa, string P_Opcion, string P_Estatus,
          string P_Moda)
        {
            ModelObtenDatosServicioSocialRequest request = new ModelObtenDatosServicioSocialRequest()
            {
                Campus = P_Campus,
                Nivel = P_Nivel,
                Programa = P_Programa,
                Opcion = P_Opcion,
                Estatus = P_Estatus,
                Moda = P_Moda
            };
            List<ModelObtenDatosServicioSocialResponse> response = DB.CallSPListResult<ModelObtenDatosServicioSocialResponse, ModelObtenDatosServicioSocialRequest>(request);
            return ToDataTable(response);
        }


        public DataTable obtenGridQryTprss()
        {
            ModelTprssRequest request = new ModelTprssRequest() { };
            List<ModelTprssResponse> response = DB.CallSPListResult<ModelTprssResponse, ModelTprssRequest>(request);
            return ToDataTable(response);
        }

        public DataTable obtenGridQryTress(string p_matricula, string p_tress_tprog_clave)
        {
            ModelTressRequest request = new ModelTressRequest() { matricula = p_matricula, tress_tprog_clave= p_tress_tprog_clave };
            List<ModelTressResponse> response = DB.CallSPListResult<ModelTressResponse, ModelTressRequest>(request);
            return ToDataTable(response);
        }

        public DataTable obtenPeriodosDisponibles(string p_matricula, string p_programa)
        {
            ModelTprssDisponiblesRequest request = new ModelTprssDisponiblesRequest() { matricula = p_matricula, programa= p_programa };
            List<ModelTprssDisponiblesResponse> response = DB.CallSPListResult<ModelTprssDisponiblesResponse, ModelTprssDisponiblesRequest>(request);
            return ToDataTableForDropDownList(response);
        }

        public ModelInsertarTprssResponse InsertarTprss(string p_tprss_clave, string p_tprss_desc, string p_tprss_empresa, 
            string p_tprss_creditos, string p_tprss_estatus, string p_tprss_tuser_clave)
        {
            ModelInsertarTprss Insert = new ModelInsertarTprss()
            {
                tprss_clave = p_tprss_clave,
                tprss_desc = p_tprss_desc,
                tprss_empresa = p_tprss_empresa,
                tprss_creditos=p_tprss_creditos,
                tprss_estatus = p_tprss_estatus,
                tprss_tuser_clave = p_tprss_tuser_clave
            };
            ModelInsertarTprssResponse response = DB.CallSPResult<ModelInsertarTprssResponse, ModelInsertarTprss>(Insert);
            return response;
        }

        public string EditarTprss(string p_tprss_clave, string p_tprss_desc, string p_tprss_empresa,
           string p_tprss_creditos, string p_tprss_estatus, string p_tprss_tuser_clave)
        {
            ModelEditarTprss Update = new ModelEditarTprss()
            {
                tprss_clave = p_tprss_clave,
                tprss_desc = p_tprss_desc,
                tprss_empresa = p_tprss_empresa,
                tprss_creditos = p_tprss_creditos,
                tprss_estatus = p_tprss_estatus,
                tprss_tuser_clave = p_tprss_tuser_clave
            };
            return DB.CallSPForInsertUpdate(Update);
        }

        public ModelInsertarTressResponse InsertarTress(string p_tress_tpees_clave, string p_tress_tpers_num, string p_tress_tprog_clave,
            string p_tress_tprss_clave, string p_tress_fecha_inicio, string p_tress_fecha_final, string p_tress_modalidad, string p_trees_horas,
            string p_trees_horas_cumplidas, string p_trees_estatus, string p_trees_tuser_clave)
        {
            ModelInsertarTress Insert = new ModelInsertarTress()
            {
                tress_tpees_clave = p_tress_tpees_clave,
                tress_tpers_num = p_tress_tpers_num,
                tress_tprog_clave = p_tress_tprog_clave,
                tress_tprss_clave = p_tress_tprss_clave,
                tress_fecha_inicio = p_tress_fecha_inicio,
                tress_fecha_final = p_tress_fecha_final,
                tress_modalidad = p_tress_modalidad,
                trees_horas= p_trees_horas,
                trees_horas_cumplidas= p_trees_horas_cumplidas,
                trees_estatus= p_trees_estatus,
                trees_tuser_clave= p_trees_tuser_clave
            };
            ModelInsertarTressResponse response = DB.CallSPResult<ModelInsertarTressResponse, ModelInsertarTress>(Insert);
            return response;
        }

        public string EditarTress(string p_tress_tpees_clave, string p_tress_tpers_num, string p_tress_tprog_clave,
    string p_tress_tprss_clave, string p_tress_fecha_inicio, string p_tress_fecha_final, string p_tress_modalidad, string p_trees_horas,
    string p_trees_horas_cumplidas, string p_trees_estatus, string p_trees_tuser_clave)
        {
            ModelEditarTress Update = new ModelEditarTress()
            {
                tress_tpees_clave = p_tress_tpees_clave,
                tress_tpers_num = p_tress_tpers_num,
                tress_tprog_clave = p_tress_tprog_clave,
                tress_tprss_clave = p_tress_tprss_clave,
                tress_fecha_inicio = p_tress_fecha_inicio,
                tress_fecha_final = p_tress_fecha_final,
                tress_modalidad = p_tress_modalidad,
                trees_horas = p_trees_horas,
                trees_horas_cumplidas = p_trees_horas_cumplidas,
                trees_estatus = p_trees_estatus,
                trees_tuser_clave = p_trees_tuser_clave
            };
            return DB.CallSPForInsertUpdate(Update);
        }

        //public string EditarTprss(string p_tprss_clave, string p_tprss_desc, string p_tprss_empresa,
        //   string p_tprss_creditos, string p_tprss_estatus, string p_tprss_tuser_clave)
        //{
        //    ModelEditarTprss Update = new ModelEditarTprss()
        //    {
        //        tprss_clave = p_tprss_clave,
        //        tprss_desc = p_tprss_desc,
        //        tprss_empresa = p_tprss_empresa,
        //        tprss_creditos = p_tprss_creditos,
        //        tprss_estatus = p_tprss_estatus,
        //        tprss_tuser_clave = p_tprss_tuser_clave
        //    };
        //    return DB.CallSPForInsertUpdate(Update);
        //}
    }
}
