using SAES_DBO.Models;
using SAES_Services;
using SAES_v1.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SAES_v1
{

    public partial class tbole : System.Web.UI.Page
    {
        #region <Variables>
        Utilidades utils = new Utilidades();
        Catalogos serviceCatalogo = new Catalogos();
        GraficaService serviceGrafica = new GraficaService();
        KardexAlumno serviceAlumno = new KardexAlumno();

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                Inicializar();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "GridAlumnos", "load_datatable();", true);

        }
        private void Inicializar()
        {



            ddl_periodo.DataSource = serviceCatalogo.ObtenerPeriodosEscolares();
            ddl_periodo.DataValueField = "clave";
            ddl_periodo.DataTextField = "nombre";
            ddl_periodo.DataBind();
            //ddl_periodo.SelectedValue = "202065";
            ddl_periodo_SelectedIndexChanged(null, null);
            ddl_programa.Items.Insert(0, new ListItem("--Todos--", "0"));

        }

        protected void linkBttnGenBol_Click(object sender, EventArgs e)
        {
            string ruta = string.Empty;
            try
            {
                ruta = "../Reports/VisualizadorCrystal.aspx?Tipo=RepBoleta&Valor1=" + ddl_periodo.SelectedValue + "&Valor2=" + ddl_campus.SelectedValue + "&Valor3=" + ddl_nivel.SelectedValue + "&Valor4=" + ddl_programa.SelectedValue + "&Valor5=&enExcel=N";


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

        protected void ddl_periodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddl_campus.DataSource = serviceCatalogo.ObtenerCampus();
            ddl_campus.DataValueField = "Clave";
            ddl_campus.DataTextField = "Descripcion";
            ddl_campus.DataBind();
            //ddl_campus.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
            //ddl_campus.SelectedValue = "LV";
            ddl_campus_SelectedIndexChanged(null, null);
        }

        protected void ddl_nivel_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<ModelProgramaRequest> lst = new List<ModelProgramaRequest>();
            try
            {
                ddl_programa.Items.Clear();

                if (ddl_nivel.SelectedValue != "" && ddl_campus.SelectedValue != "")
                {
                    ddl_programa.DataSource = serviceCatalogo.obtenPrograma(ddl_campus.SelectedValue, ddl_nivel.SelectedValue);
                    ddl_programa.DataValueField = "Clave";
                    ddl_programa.DataTextField = "Descripcion";
                    ddl_programa.DataBind();
                }
               else
                {
                    ddl_programa.Items.Insert(0, new ListItem("--Seleccionar--", "0"));

                }

                ddl_programa_SelectedIndexChanged(null, null);
            }

            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "ttiop", Session["usuario"].ToString(), ex.StackTrace);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
            }

        }

        protected void ddl_campus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddl_nivel.DataSource = serviceCatalogo.obtenNivel(ddl_campus.SelectedValue);
            ddl_nivel.DataValueField = "Clave";
            ddl_nivel.DataTextField = "Descripcion";
            ddl_nivel.DataBind();
            //ddl_nivel.Items.Insert(0, new ListItem("--Seleccionar--", "0"));
            ddl_nivel_SelectedIndexChanged(null, null);
        }

        protected void ddl_programa_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridAlumnos.DataSource = null;
                GridAlumnos.DataBind();
                DataTable dt = serviceAlumno.ObtenerAlumnosProg(ddl_periodo.SelectedValue, ddl_campus.SelectedValue, ddl_nivel.SelectedValue, ddl_programa.SelectedValue);
                GridAlumnos = utils.BeginGrid(GridAlumnos, dt);

            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "ttiop", Session["usuario"].ToString(), ex.StackTrace);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
            }
        }

        protected void GridAlumnos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ruta = string.Empty;
            try
            {
                ruta = "../Reports/VisualizadorCrystal.aspx?Tipo=RepBoleta&Valor1=" + ddl_periodo.SelectedValue + "&Valor2=" + ddl_campus.SelectedValue + "&Valor3=" + ddl_nivel.SelectedValue + "&Valor4=" + ddl_programa.SelectedValue + "&Valor5=" + GridAlumnos.SelectedRow.Cells[3].Text
                +"&enExcel=N";

                string _open = "window.open('" + ruta + "', '_newtab');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), _open, true);
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tbole", Session["usuario"].ToString());
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);

            }
        }
    }
}