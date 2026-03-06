using SAES_Services;
using SAES_v1.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SAES_v1
{
    public partial class Default2 : System.Web.UI.Page
    {
        #region <Variables>
        Utilidades utils = new Utilidades();
        UsuarioService serviceUsuario = new UsuarioService();
        ComunService comun = new ComunService();
        
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            //string usuario = "";
            if (Session["usuario"] == null)
                Response.Redirect("Default.aspx");

            if (!IsPostBack)
                Inicializar();

        }

        private void Inicializar()
        {
            try
            {
                if (Session["tiene_pass"].ToString() == "2")
                {
                    linkBttnVerificaRespuesta.Visible = true;
                    linkActDatos.Visible = false;
                }
                else if (Session["tiene_pass"].ToString() == "3")
                {
                    linkBttnVerificaRespuesta.Visible = false;
                    linkActDatos.Visible = true;
                }

                multiViewUsu.ActiveViewIndex = 0;
               
                comun.LlenaCombo("ObtenerListaPreguntas", ref DDLPregunta);

            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
            }
        }

        protected void linkBttnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("Default.aspx");
            }
            catch(Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "ttiop", Session["usuario"].ToString(), ex.StackTrace);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
            }
        }

        protected void linkBttnRegresar_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("Default.aspx");
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "ttiop", Session["usuario"].ToString(), ex.StackTrace);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
            }
        }

        protected void linkBttnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["usuario"] == null)
                    Response.Redirect("Default.aspx");
                else {
                    if (txtNuevoPass.Text == txConfPass.Text)
                    {
                        serviceUsuario.EditarPassword(Session["usuario"].ToString(), txtNuevoPass.Text);
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_success", "alert_success('Se actualizaron los datos exitosamente');", true);
                        Response.Redirect("Default.aspx");
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('Las contraseñas no son iguales.');", true);
                    }
                }

            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "ttiop", Session["usuario"].ToString(), ex.StackTrace);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
            }
        }

        protected void linkBttnVerificaRespuesta_Click(object sender, EventArgs e)
        {
            try
            {
                int Valida = serviceUsuario.ValidaPregunta(Session["usuario"].ToString(), DDLPregunta.SelectedValue, txtRespuesta.Text);
                if (Valida == 0)
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('Respuesta incorrecta');", true);
                else if (Valida == 1)
                    multiViewUsu.ActiveViewIndex = 1;
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('Se actualizaron los datos exitosamente');", true);

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_success", "alert_success('Se actualizaron los datos exitosamente');", true);
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "ttiop", Session["usuario"].ToString(), ex.StackTrace);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
            }
        }

        protected void linkActDatos_Click(object sender, EventArgs e)
        {
            try
            {
                serviceUsuario.InsertarPregunta(Session["usuario"].ToString(), DDLPregunta.SelectedValue, txtRespuesta.Text);
                multiViewUsu.ActiveViewIndex = 1;

            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "ttiop", Session["usuario"].ToString(), ex.StackTrace);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
            }
        }
    }
}