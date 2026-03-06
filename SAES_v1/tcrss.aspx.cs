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
    public partial class tcrss : System.Web.UI.Page
    {
        #region <Variables>
        Utilidades utils = new Utilidades();
        Catalogos serviceCatalogo = new Catalogos();
        GraficaService serviceGrafica = new GraficaService();
        FinanzasService serviceFinanzas = new FinanzasService();
        public string labels_dashboard_1;
        public string data_dashboard_1;
        public string label_dashboard_1;
        MenuService servicePermiso = new MenuService();
        CatOpcionesTitulacion serviceTitulacion = new CatOpcionesTitulacion();
        ServicioSocialService serviceServicioSocial = new ServicioSocialService();
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
                    btn_tcrss.Visible = true;
                    Carga_Periodos();
                    Gridtcrss.DataSource = null;
                    Gridtcrss.DataBind();
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);
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
                objPermiso = servicePermiso.ObtenerPermisoFormulario(Session["usuario"].ToString(), "tcrss");
                if (objPermiso != null)
                {
                    if (objPermiso.usme_update == "0" || objPermiso.usme_select == "0")
                    {
                        btn_tcrss.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                    }
                    else
                    {
                        btn_tcrss.Visible = true;
                        Carga_Periodos();
                    }
                }
                else
                {
                    btn_tcrss.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                }


            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "trtit", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void Carga_Periodos()
        {
            try
            {
                ddl_periodo.DataSource = serviceServicioSocial.ObtenerPeriodosServicioSocial();
                ddl_periodo.DataValueField = "clave";
                ddl_periodo.DataTextField = "nombre";
                ddl_periodo.DataBind();
                //ddl_periodo.Items.Add(new ListItem("-------", ""));
                ddl_periodo_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "trtit", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void Carga_Programas()
        {
            ddl_programa.Items.Clear();
            try
            {
                if (ddl_campus.SelectedValue == "" || ddl_nivel.SelectedValue == "")
                    ddl_programa.DataSource = serviceCatalogo.obtenPrograma(ddl_campus.SelectedValue, ddl_nivel.SelectedValue);
                else
                    ddl_programa.DataSource = serviceCatalogo.obtenProgramaVigente(ddl_campus.SelectedValue, ddl_nivel.SelectedValue);

                ddl_programa.DataValueField = "Clave";
                ddl_programa.DataTextField = "Descripcion";
                ddl_programa.DataBind();


                ddl_programa_ss.DataSource = serviceServicioSocial.obtenGridQryTprss();
                ddl_programa_ss.DataValueField = "tprss_clave";
                ddl_programa_ss.DataTextField = "tprss_desc";
                ddl_programa_ss.DataBind();
                ddl_programa_ss.Items.Insert(0, new ListItem("-------", ""));
            }

            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcrss", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);

            }

        }

        protected void ddl_periodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddl_campus.DataSource = serviceTitulacion.ObtenerCampusTitulacion();
                ddl_campus.DataValueField = "Clave";
                ddl_campus.DataTextField = "Descripcion";
                ddl_campus.DataBind();
                ddl_campus_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcrss", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }

        }

        protected void ddl_campus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddl_campus.SelectedValue == "")
                {
                    ddl_nivel.DataSource = serviceCatalogo.obtenNivelesActivos();
                    ddl_nivel.DataValueField = "clave";
                    ddl_nivel.DataTextField = "nombre";
                }
                else
                {
                    ddl_nivel.DataSource = serviceCatalogo.obtenNivel(ddl_campus.SelectedValue);
                    ddl_nivel.DataValueField = "clave";
                    ddl_nivel.DataTextField = "descripcion";
                }
                ddl_nivel.DataBind();
                ddl_nivel_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcrss", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void ddl_nivel_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                Carga_Programas();
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcrss", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void ddl_programa_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btn_consultar_Click(object sender, EventArgs e)
        {
            Gridtcrss.DataSource = null;
            Gridtcrss.DataBind();
            //serviceServicioSocial
            try
            {
                Gridtcrss.DataSource = serviceServicioSocial.ObtenRegistroServicioSocial(ddl_campus.SelectedValue, 
                    ddl_nivel.SelectedValue, ddl_programa.SelectedValue, ddl_programa_ss.SelectedValue, ddl_estatus.SelectedValue, ddl_modalidad.SelectedValue);
                Gridtcrss.DataBind();
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcrss", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);

            }
        }
    }
}