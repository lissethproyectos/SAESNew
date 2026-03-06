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
    public partial class trole : System.Web.UI.Page
    {
        #region <Variables>
        Utilidades utils = new Utilidades();
        Catalogos serviceRol = new Catalogos();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                Inicializar();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "GridRoles", "load_datatableRoles();", true);
        }
        private void Inicializar()
        {
            //Cargarcombos();
            grvCatRoles.DataSource = null;
            grvCatRoles.DataBind();
            DataTable dt = serviceRol.obtenRoles();
            grvCatRoles = utils.BeginGrid(grvCatRoles, dt);
        }
        private void LimpiarCampos()
        {
            txtClave.Text = string.Empty;
            txtRole.Text = string.Empty;

        }
        protected void grvCatRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            LimpiarCampos();
            linkBttnGuardar.Visible = false;
            linkBttnModificar.Visible = true;
            linkBttnCancelar.Visible = true;
            try
            {
                txtClave.Text = grvCatRoles.SelectedRow.Cells[1].Text;
                txtRole.Text = grvCatRoles.SelectedRow.Cells[2].Text;
                DDLEstatus.SelectedValue = grvCatRoles.SelectedRow.Cells[3].Text;
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
            txtClave.Text = string.Empty;
            txtRole.Text = string.Empty;
            DDLEstatus.SelectedIndex = 0;
            linkBttnGuardar.Visible = true;
            linkBttnModificar.Visible = false;
            linkBttnCancelar.Visible = false;
        }

        protected void linkBttnGuardar_Click(object sender, EventArgs e)
        {
            List<ModelObtenRolesResponse> lstUsuarios = new List<ModelObtenRolesResponse>();
            UsuarioService serviceUsuario = new UsuarioService();
            ModelUsuario objUsuario = new ModelUsuario();
            try
            {
                serviceRol.InsertarRol(txtClave.Text, txtRole.Text, Session["usuario"].ToString(), DDLEstatus.SelectedValue);
                grvCatRoles.DataSource = null;
                grvCatRoles.DataBind();
                DataTable dt = serviceRol.obtenRoles();
                grvCatRoles = utils.BeginGrid(grvCatRoles, dt);                
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_success", "alert_success('Se registraron los datos exitosamente');", true);
                txtClave.Text = string.Empty;
                txtRole.Text = string.Empty;
                DDLEstatus.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "ttiop", Session["usuario"].ToString(), ex.StackTrace);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
            }
        }

        protected void linkBttnModificar_Click(object sender, EventArgs e)
        {
            List<ModelObtenRolesResponse> lstUsuarios = new List<ModelObtenRolesResponse>();
            UsuarioService serviceUsuario = new UsuarioService();
            ModelUsuario objUsuario = new ModelUsuario();
            try
            {
                serviceRol.EditarRol(txtClave.Text, txtRole.Text, DDLEstatus.SelectedValue);
                grvCatRoles.DataSource = null;
                grvCatRoles.DataBind();
                DataTable dt = serviceRol.obtenRoles();
                grvCatRoles = utils.BeginGrid(grvCatRoles, dt);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_success", "alert_success('Se actualizaron los datos exitosamente');", true);
                txtClave.Text = string.Empty;
                txtRole.Text = string.Empty;
                DDLEstatus.SelectedIndex = 0;
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