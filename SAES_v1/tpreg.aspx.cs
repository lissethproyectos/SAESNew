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
    public partial class tpreg : System.Web.UI.Page
    {
        #region <Variables>
        Utilidades utils = new Utilidades();
        Catalogos serviceCatalogo = new Catalogos();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                Inicializar();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "GridOpcPreguntas", "load_datatablePreguntas();", true);

        }
        private void Inicializar()
        {
            //Cargarcombos();
            grdPreguntas.DataSource = null;
            grdPreguntas.DataBind();
            DataTable dt = serviceCatalogo.ObtenerPreguntas();
            grdPreguntas = utils.BeginGrid(grdPreguntas, dt);
        }
        protected void linkBttnCancelar_Click(object sender, EventArgs e)
        {
            linkBttnGuardar.Visible = true;
            linkBttnModificar.Visible = false;
            txtClave.Enabled = true;
            txtClave.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            ddlEstatus.SelectedIndex = 0;
        }

        protected void linkBttnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                serviceCatalogo.InsertarPregunta(Convert.ToInt32(txtClave.Text), txtDescripcion.Text, ddlEstatus.SelectedValue);
                grdPreguntas.DataSource = null;
                grdPreguntas.DataBind();
                DataTable dt = serviceCatalogo.ObtenerPreguntas();
                grdPreguntas = utils.BeginGrid(grdPreguntas, dt);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_success", "alert_success('Se guardaron los datos exitosamente');", true);
                txtClave.Text = string.Empty;
                txtDescripcion.Text = string.Empty;
                ddlEstatus.SelectedIndex = 0;
                linkBttnGuardar.Visible = true;
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
            ModelMenu objMenu = new ModelMenu();
            try
            {
                serviceCatalogo.EditarPregunta(Convert.ToInt32(txtClave.Text), txtDescripcion.Text, ddlEstatus.SelectedValue);
                grdPreguntas.DataSource = null;
                grdPreguntas.DataBind();
                DataTable dt = serviceCatalogo.ObtenerPreguntas();
                grdPreguntas = utils.BeginGrid(grdPreguntas, dt);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_success", "alert_success('Se actualizaron los datos exitosamente');", true);
                txtClave.Enabled = true;
                txtClave.Text = string.Empty;
                txtDescripcion.Text = string.Empty;
                ddlEstatus.SelectedIndex = 0;
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

        protected void grdMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtClave.Text = grdPreguntas.SelectedRow.Cells[1].Text;
                txtDescripcion.Text = grdPreguntas.SelectedRow.Cells[2].Text;
                ddlEstatus.SelectedValue = grdPreguntas.SelectedRow.Cells[3].Text;
                linkBttnModificar.Visible = true;
                linkBttnGuardar.Visible = false;
                txtClave.Enabled = false;



            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
            }
        }
    }
}