using SAES_Services;
using SAES_v1.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static SAES_DBO.Models.ModelGrafica;

namespace SAES_v1
{
    public partial class Dashboard2 : System.Web.UI.Page
    {
        #region <Variables>
        Utilidades utils = new Utilidades();
        Catalogos serviceCatalogo = new Catalogos();
        GraficaService serviceGrafica= new GraficaService();
        public string labels_dashboard_1;
        public string data_dashboard_1;
        public string label_dashboard_1;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                Inicializar();


            ScriptManager.RegisterStartupScript(this, this.GetType(), "GridDashboard", "load_datatable();", true);

        }
        private void Inicializar()
        {
            ddl_periodo.DataSource = null;
            ddl_periodo.DataBind();

            ddl_campus.DataSource = null;
            ddl_campus.DataBind();


            ddl_nivel.DataSource = null;
            ddl_nivel.DataBind();



            ddl_periodo.DataSource = serviceCatalogo.ObtenerPeriodosEscolares();
            ddl_periodo.DataValueField = "clave";
            ddl_periodo.DataTextField = "nombre";
            ddl_periodo.DataBind();
            ddl_periodo_SelectedIndexChanged(null, null);
        }

        protected void ddl_periodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Gridtcv.Visible = false;
            ddl_campus.DataSource = serviceCatalogo.ObtenerCampus();
            ddl_campus.DataValueField = "Clave";
            ddl_campus.DataTextField = "Descripcion";
            ddl_campus.DataBind();
            //ddl_campus.Items.Insert(0, new ListItem("--Seleccione--", "0"));
            ddl_campus_SelectedIndexChanged(null, null);
            dashboard_1();
        }

        protected void ddl_campus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddl_nivel.DataSource = serviceCatalogo.obtenNivel(ddl_campus.SelectedValue);
            ddl_nivel.DataValueField = "Clave";
            ddl_nivel.DataTextField = "Descripcion";
            ddl_nivel.DataBind();
            //ddl_nivel.Items.Insert(0, new ListItem("--Seleccione--", "0"));
            dashboard_1();
        }

        protected void ddl_nivel_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Gridtcv.Visible = false;
            dashboard_1();
        }

        //pruebas

        private void dashboard_1()
        {
            List<ModelObtenGraficaAdeudoResponse> lstDatosGrafica = new List<ModelObtenGraficaAdeudoResponse>();
            try
            {
                if (ddl_periodo.SelectedValue != "0" && ddl_campus.SelectedValue == "" && ddl_nivel.SelectedValue == "")
                {
                    grvDatosGrafica.DataSource = null;
                    grvDatosGrafica.DataBind();
                    lstDatosGrafica = serviceGrafica.obtenerDatosGraficaAdeudo("1", ddl_periodo.SelectedValue, "", "");
                    grvDatosGrafica.DataSource = lstDatosGrafica;
                    grvDatosGrafica.DataBind();
                    ScriptManager.RegisterStartupScript(this, GetType(), "Grafica_Adeudos", "GraficaAdeudos('1','"+ddl_periodo.SelectedValue+"', '', '');", true);

                    //grvDatosGrafica = utils.BeginGrid(grvDatosGrafica, dt);
                }
                else if (ddl_periodo.SelectedValue != "0" && ddl_campus.SelectedValue != "" && ddl_nivel.SelectedValue == "")
                {
                    grvDatosGrafica.DataSource = null;
                    grvDatosGrafica.DataBind();
                    lstDatosGrafica = serviceGrafica.obtenerDatosGraficaAdeudo("2", ddl_periodo.SelectedValue, ddl_campus.SelectedValue, "");
                    grvDatosGrafica.DataSource = lstDatosGrafica;
                    grvDatosGrafica.DataBind();
                    //DataTable dt = serviceGrafica.obtenerDatosGrafica1("5");
                    //grvDatosGrafica = utils.BeginGrid(grvDatosGrafica, dt);
                    ScriptManager.RegisterStartupScript(this, GetType(), "Grafica_Adeudos", "GraficaAdeudos(2," + ddl_periodo.SelectedValue+",'"+ddl_campus.SelectedValue+"', '');", true);
                }
               

                else
                {
                    grvDatosGrafica.DataSource = null;
                    grvDatosGrafica.DataBind();
                    lstDatosGrafica = serviceGrafica.obtenerDatosGraficaAdeudo("3", ddl_periodo.SelectedValue, ddl_campus.SelectedValue, ddl_nivel.SelectedValue);
                    grvDatosGrafica.DataSource = lstDatosGrafica;
                    grvDatosGrafica.DataBind();
                    ScriptManager.RegisterStartupScript(this, GetType(), "Grafica_Adeudos", "GraficaAdeudos(3," + ddl_periodo.SelectedValue + ",'" + ddl_campus.SelectedValue + "', '"+ddl_nivel.SelectedValue+"');", true);
                    //grvDatosGrafica.DataSource = null;
                    //grvDatosGrafica.DataBind();
                    //DataTable dt = serviceGrafica.obtenerDatosGrafica1("6");
                    //grvDatosGrafica = utils.BeginGrid(grvDatosGrafica, dt);
                }
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tpees", Session["usuario"].ToString());
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);

            }
        }

        protected void grvDatosGrafica_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ruta = string.Empty;
            try
            {
                if (ddl_periodo.SelectedValue != "0" && ddl_campus.SelectedValue == "" && ddl_nivel.SelectedValue == "0")
                    ruta = "../Reports/VisualizadorCrystal.aspx?Tipo=RepDeudores1&Valor1=" + ddl_periodo.SelectedValue + "&Valor2=" + grvDatosGrafica.SelectedRow.Cells[5].Text + "&enExcel=N";
                else if (ddl_periodo.SelectedValue != "0" && ddl_campus.SelectedValue != "" && ddl_nivel.SelectedValue == "0")
                    ruta = "../Reports/VisualizadorCrystal.aspx?Tipo=RepDeudores2&Valor1=" + ddl_periodo.SelectedValue + "&Valor2=" + ddl_campus.SelectedValue + "&Valor3=" + grvDatosGrafica.SelectedRow.Cells[5].Text + "&enExcel=N";
                else
                    ruta = "../Reports/VisualizadorCrystal.aspx?Tipo=RepDeudores3&Valor1=" + ddl_periodo.SelectedValue + "&Valor2=" + ddl_campus.SelectedValue + "&Valor3=" + ddl_nivel.SelectedValue + "&Valor4=" + grvDatosGrafica.SelectedRow.Cells[5].Text + "&enExcel=N";

                string _open = "window.open('" + ruta + "', '_newtab');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), _open, true);
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tpees", Session["usuario"].ToString());
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);

            }
        }
    }
}