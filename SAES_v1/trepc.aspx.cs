using SAES_Services;
using SAES_v1.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using static SAES_DBO.Models.ModelMenu;

namespace SAES_v1
{
    public partial class trepc : System.Web.UI.Page
    {
        #region <Variables>
        Utilidades utils = new Utilidades();
        Catalogos serviceCatalogo = new Catalogos();
        GraficaService serviceGrafica = new GraficaService();
        FinanzasService serviceFinanzas=new FinanzasService();
        public string labels_dashboard_1;
        public string data_dashboard_1;
        public string label_dashboard_1;
        MenuService servicePermiso = new MenuService();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Response.Redirect(FormsAuthentication.DefaultUrl);
                Response.End();
            }
            else
            {

                if (!IsPostBack)
                {
                    LlenaPagina();
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);
            }
        }

        protected void Carga_Periodos()
        {
            try
            {
                ddl_periodo.DataSource = serviceCatalogo.ObtenerPeriodosPrograma();
                ddl_periodo.DataValueField = "clave";
                ddl_periodo.DataTextField = "nombre";
                ddl_periodo.DataBind();
                ddl_periodo_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "trepc", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void Carga_Conceptos()
        {
            ddl_concepto.DataSource = serviceCatalogo.ObtenerConceptosDescuento();
            ddl_concepto.DataValueField = "clave";
            ddl_concepto.DataTextField = "nombre";
            ddl_concepto.DataBind();
        }

        protected void Carga_Tipos()
        {
            ddl_tipo.DataSource = serviceCatalogo.ObtenerConceptosDescuento();
            ddl_tipo.DataValueField = "clave";
            ddl_tipo.DataTextField = "nombre";
            ddl_tipo.DataBind();
        }

        protected void Carga_Programas()
        {
            ddl_programa.Items.Clear();
            try
            {
                if (ddl_campus.SelectedValue != "" && ddl_nivel.SelectedValue != "")
                {
                    ddl_programa.DataSource = serviceCatalogo.obtenProgramaVigente(ddl_campus.SelectedValue, ddl_nivel.SelectedValue);
                    ddl_programa.DataValueField = "Clave";
                    ddl_programa.DataTextField = "Descripcion";
                    ddl_programa.DataBind();
                }
                else
                    ddl_programa.DataSource = serviceCatalogo.obtenPrograma(ddl_campus.SelectedValue, ddl_nivel.SelectedValue);
            }

            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "trepc", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);

            }

        }

        protected void ddl_periodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddl_campus.DataSource = serviceCatalogo.ObtenerCampus();
                ddl_campus.DataValueField = "Clave";
                ddl_campus.DataTextField = "Descripcion";
                ddl_campus.DataBind();
                ddl_campus_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "trepc", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void ddl_nivel_SelectedIndexChanged(object sender, EventArgs e)
        {
            Carga_Programas();
        }

        protected void ddl_campus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //ddl_nivel.DataSource = serviceCatalogo.obtenNivelesporCampus(ddl_campus.SelectedValue);
                ddl_nivel.DataSource = serviceCatalogo.obtenNivel(ddl_campus.SelectedValue);

                ddl_nivel.DataValueField = "Clave";
                ddl_nivel.DataTextField = "Descripcion";
                ddl_nivel.DataBind();
                ddl_nivel_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "trepc", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        private void LlenaPagina()
        {
            ModelObtenerPermisoFormResponse objPermiso = new ModelObtenerPermisoFormResponse();
            objPermiso.usme_update = "0";
            objPermiso.usme_select = "0";
            System.Threading.Thread.Sleep(50);
            try
            {
                objPermiso = servicePermiso.ObtenerPermisoFormulario(Session["usuario"].ToString(), "trepc");
                if (objPermiso != null)
                {
                    if (objPermiso.usme_update == "0" || objPermiso.usme_select == "0")
                    {
                        btn_trepc.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                    }
                    else
                    {
                        btn_trepc.Visible = true;
                        Carga_Conceptos();
                        Carga_Periodos();
                        GridTrepc.DataSource = null;
                        GridTrepc.DataBind();

                        //ddl_periodo_SelectedIndexChanged(null, null);
                    }
                }
                else
                {
                    btn_trepc.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                }


            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "trepc", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {

        }

        protected void btn_consultar_Click(object sender, EventArgs e)
        {
            GridTrepc.DataSource = null;
            GridTrepc.DataBind();

            try
            {
                GridTrepc.DataSource = serviceFinanzas.obtenDescuentosAlumno(ddl_periodo.SelectedValue, ddl_campus.SelectedValue, ddl_programa.SelectedValue, 
                    ddl_nivel.SelectedValue, ddl_concepto.SelectedValue, ddl_tipo.SelectedValue, ddl_estatus.SelectedValue
                   );
                GridTrepc.DataBind();
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "trepc", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);

            }
        }

        protected void btn_generar_Click(object sender, EventArgs e)
        {
            //string ruta = "../Reportes/VisualizadorCrystal.aspx?Tipo=REP052&dependencia=" + ddlDependencia.SelectedValue + "&Evento=" + CveEvento + "&TipoDesc=" + DescEvento;
            string ruta = "../Reports/VisualizadorCrystal.aspx?Tipo=RepBecas_Descuentos&Valor1=" + ddl_campus.SelectedValue + "&Valor2=" + ddl_nivel.SelectedValue + "&Valor3=" + ddl_programa.SelectedValue + "&Valor4=" + ddl_concepto.SelectedValue + "&Valor5=" + ddl_tipo.SelectedValue + "&Valor6=" + ddl_estatus.SelectedValue + "&Valor7=" + ddl_periodo.SelectedValue;

            string _open = "window.open('" + ruta + "', '_newtab');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), _open, true);

            //ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "ver_reporte_becas('" + ddl_campus.SelectedValue + "','"+ddl_nivel.SelectedValue+"','" +
            //ddl_programa.SelectedValue + "','" + ddl_concepto.SelectedValue + "','" + ddl_tipo.SelectedValue + "','" + ddl_estatus.SelectedValue + "','"+ddl_periodo.SelectedValue+"');", true);

        }
    }
}