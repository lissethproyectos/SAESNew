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
    public partial class tmede : System.Web.UI.Page
    {
        #region <Variables>
        Utilidades utils = new Utilidades();
        MenuService serviceMenu = new MenuService();
        Catalogos serviceCatalogo = new Catalogos();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                Inicializar();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "GridOpcMenu", "load_datatableOpcionesMenu();", true);
        
    }
        private void Inicializar()
        {
            Cargarcombos();
            grdMenu.DataSource = null;
            grdMenu.DataBind();
            DataTable dt = serviceMenu.ObtenerMenuOpciones(ddlMenu.SelectedValue);
            grdMenu = utils.BeginGrid(grdMenu, dt);
        }

        private void Cargarcombos()
        {
            //lblError.Text = string.Empty;

            try
            {
                ddlMenu.DataSource = serviceCatalogo.obtenListMenu();
                ddlMenu.DataValueField = "menu_clave";
                ddlMenu.DataTextField = "menu_desc";
                ddlMenu.DataBind();
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
            }
        }

        private void LimpiarCampos()
        {
           

        }

        protected void grdMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            string opcion = grdMenu.SelectedRow.Cells[3].Text.Replace("&#225;", "á");

            try
            {
                txtClave.Text = grdMenu.SelectedRow.Cells[2].Text;
                txtOpcion.Text = HttpUtility.HtmlDecode(grdMenu.SelectedRow.Cells[3].Text);
                ddlEstatus.SelectedValue= grdMenu.SelectedRow.Cells[4].Text;
                txtRelacion.Text = HttpUtility.HtmlDecode(grdMenu.SelectedRow.Cells[5].Text);
                txtForma.Text = grdMenu.SelectedRow.Cells[6].Text;
                linkBttnGuardar.Visible= false;
                linkBttnModificar.Visible = true;


            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
            }
        }

        protected void linkBttnGuardar_Click(object sender, EventArgs e)
        {

            try
            {
                serviceMenu.InsertarMenuOpciones(ddlMenu.SelectedValue, txtClave.Text, txtOpcion.Text,   Session["usuario"].ToString(), ddlEstatus.SelectedValue, txtRelacion.Text, txtForma.Text);
                grdMenu.DataSource = null;
                grdMenu.DataBind();
                DataTable dt = serviceMenu.ObtenerMenuOpciones(ddlMenu.SelectedValue);
                grdMenu = utils.BeginGrid(grdMenu, dt);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_success", "alert_success('Se guardaron los datos exitosamente');", true);
                txtClave.Text = string.Empty;
                txtOpcion.Text = string.Empty;
                ddlEstatus.SelectedIndex = 0;
                txtRelacion.Text = string.Empty;
                txtForma.Text = string.Empty;
                linkBttnGuardar.Visible = true;
                //serviceMenu.EditarMenu(ddlMenu.SelectedValue, txtClave.Text, txtOpcion.Text, ddlEstatus.SelectedValue, txtRelacion.Text, txtForma.Text);

            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tmede", Session["usuario"].ToString(), ex.StackTrace);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
            }
        }

        protected void linkBttnModificar_Click(object sender, EventArgs e)
        {
            ModelMenu objMenu=new ModelMenu();
            try
            {
                serviceMenu.EditarMenuOpciones(ddlMenu.SelectedValue, txtClave.Text, txtOpcion.Text, ddlEstatus.SelectedValue, txtRelacion.Text, txtForma.Text);
                grdMenu.DataSource = null;
                grdMenu.DataBind();
                DataTable dt = serviceMenu.ObtenerMenuOpciones(ddlMenu.SelectedValue);
                grdMenu = utils.BeginGrid(grdMenu, dt);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_success", "alert_success('Se actualizaron los datos exitosamente');", true);
                txtClave.Text = string.Empty;
                txtOpcion.Text = string.Empty;
                ddlEstatus.SelectedIndex = 0;
                txtRelacion.Text = string.Empty;
                txtForma.Text = string.Empty;
                linkBttnGuardar.Visible = true;
                linkBttnModificar.Visible = false;

            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "ttiop", Session["usuario"].ToString(), ex.StackTrace);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
            }
        }

        protected void ddlMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                grdMenu.DataSource = null;
                grdMenu.DataBind();
                DataTable dt = serviceMenu.ObtenerMenuOpciones(ddlMenu.SelectedValue);
                grdMenu = utils.BeginGrid(grdMenu, dt);

            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "ttiop", Session["usuario"].ToString(), ex.StackTrace);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
            }
        }

        protected void linkBttnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                ddlMenu.SelectedIndex = 0;
                txtClave.Text = string.Empty;
                txtOpcion.Text = string.Empty;
                ddlEstatus.SelectedIndex = 0;
                txtRelacion.Text = string.Empty;
                txtForma.Text = string.Empty;
                linkBttnGuardar.Visible = true;
                linkBttnModificar.Visible = false;
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