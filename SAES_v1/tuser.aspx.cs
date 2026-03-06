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
    public partial class tuser : System.Web.UI.Page
    {
        #region <Variables>
        Utilidades utils = new Utilidades();
        UsuarioService usuService = new UsuarioService();
        Catalogos serviceRol = new Catalogos();
        ComunService comun = new ComunService();
        List<ModelComun> lstComun = new List<ModelComun>();
        UsuarioService serviceUsuario = new UsuarioService();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                Inicializar();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatableUsuarios", "load_datatableUsuarios();", true);
        }
        private void Inicializar()
        {
            ddlRol.SelectedIndex = 0;
            txtUsuario.Text = string.Empty;
            DDLStatus.SelectedIndex = 0;
            txtNombre.Text = string.Empty;
            Cargarcombos();
            grvUsuarios.DataSource = null;
            grvUsuarios.DataBind();
            DataTable dt = serviceUsuario.ObtenerUsuarios(ddlRol.SelectedValue);
            grvUsuarios = utils.BeginGrid(grvUsuarios, dt);


        }

        //protected bool Acepta_Privacidad1()
        //{
        //    ArrayList arrParam = new ArrayList();
        //    arrParam.Add(new applyWeb.Data.Parametro("@IDAlumno_in", Session["usuario"].ToString()));
        //    DataSet objDS = objExpediente.ExecuteSP("Revisar_ContratoAceptado", arrParam);
        //    if (objDS.Tables[0].Rows.Count > 0)
        //    {
        //        if (objDS.Tables[0].Rows[0]["ContratoAceptado"].ToString().ToLower().Equals("true"))
        //            return true;
        //        else
        //            return false;
        //    }
        //    return false;
        //}
        private void Cargarcombos()
        {
            //lblError.Text = string.Empty;

            try
            {
                ddlRol.DataSource = serviceRol.obtenListRoles();
                ddlRol.DataValueField = "trole_clave";
                ddlRol.DataTextField = "trole_desc";
                ddlRol.DataBind();
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
            }
        }
        protected void linkBttnGuardar_Click(object sender, EventArgs e)
        {
            //Verificador = string.Empty;
            List<ModelUsuario> lstUsuarios = new List<ModelUsuario>();
            UsuarioService serviceUsuario = new UsuarioService();
            ModelUsuario objUsuario = new ModelUsuario();
            try
            {
                serviceUsuario.InsertarUsuario( txtUsuario.Text, txtNombre.Text, DDLStatus.SelectedValue, Session["usuario"].ToString(), ddlRol.SelectedValue);
                DataTable dt = serviceUsuario.ObtenerUsuarios(ddlRol.SelectedValue);
                grvUsuarios = utils.BeginGrid(grvUsuarios, dt);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_success", "alert_success('Se registraron los datos exitosamente');", true);
                txtNombre.Text = string.Empty;
                txtUsuario.Text = string.Empty;
                txtUsuario.Focus();
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "ttiop", Session["usuario"].ToString(), ex.StackTrace);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
            }
        }
       
        protected void linkBttnEditar_Click(object sender, EventArgs e)
        {
            LinkButton cbi = (LinkButton)(sender);
            GridViewRow row = (GridViewRow)cbi.NamingContainer;
            grvUsuarios.SelectedIndex = row.RowIndex;           
        }
        protected void linkBttnModificar_Click(object sender, EventArgs e)
        {
            ModelUsuario objUsuario = new ModelUsuario();
            try
            {
                serviceUsuario.EditarUsuario(grvUsuarios.SelectedRow.Cells[1].Text, txtNombre.Text, DDLStatus.SelectedValue);
                DataTable dt = serviceUsuario.ObtenerUsuarios(ddlRol.SelectedValue);
                grvUsuarios = utils.BeginGrid(grvUsuarios, dt);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_success", "alert_success('Se actualizaron los datos exitosamente');", true);
                txtUsuario.Text = string.Empty;
                txtNombre.Text = string.Empty;
                //ddlRol.SelectedIndex = 0;
                //DDLStatus.SelectedIndex = 0;
                linkBttnGuardar.Visible = true;
                linkBttnModificar.Visible = false;
                txtUsuario.Enabled = true;
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "ttiop", Session["usuario"].ToString(), ex.StackTrace);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
            }
        }
        private void LimpiarCampos()
        {
            txtUsuario.Text = string.Empty;
            txtNombre.Text = string.Empty;
            ddlRol.SelectedIndex = 0;
            DDLStatus.SelectedIndex = 0;
            linkBttnGuardar.Visible = true;
            linkBttnModificar.Visible = false;
        }
        protected void linkBttnNuevo_Click(object sender, EventArgs e)
        {
           
            try
            {
               
                //txtUsuario.Text = grvUsuarios.SelectedRow.Cells[0].Text;
                //txtNombre.Text = grvUsuarios.SelectedRow.Cells[1].Text;
                //DDLStatus.SelectedValue = grvUsuarios.SelectedRow.Cells[2].Text;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "modalUsuarios", "$('#exampleModal').modal('show')", true);
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "ttiop", Session["usuario"].ToString(), ex.StackTrace);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
            }
        }

        protected void grvUsuarios_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int fila = e.RowIndex;
                serviceUsuario.EliminarUsuario(grvUsuarios.Rows[fila].Cells[1].Text);
                DataTable dt = serviceUsuario.ObtenerUsuarios(ddlRol.SelectedValue);
                grvUsuarios = utils.BeginGrid(grvUsuarios, dt);
            }

            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "ttiop", Session["usuario"].ToString(), ex.StackTrace);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
            }
        }

        protected void ddlRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                grvUsuarios.DataSource = null;
                grvUsuarios.DataBind();
                DataTable dt = serviceUsuario.ObtenerUsuarios(ddlRol.SelectedValue);
                grvUsuarios = utils.BeginGrid(grvUsuarios, dt);
            }
            catch(Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "ttiop", Session["usuario"].ToString(), ex.StackTrace);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
            }
        }

        protected void grvUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            linkBttnGuardar.Visible = false;
            linkBttnModificar.Visible = true;
            string Nombre = string.Empty;

            try
            {
                txtUsuario.Enabled = false;
                linkBttnCancelar.Visible = true;
                Nombre=grvUsuarios.SelectedRow.Cells[2].Text.Replace("&#193;","Á");
                Nombre = Nombre.Replace("&#201;", "É");
                Nombre = Nombre.Replace("&#205;", "É");
                Nombre = Nombre.Replace("&#211;", "É");
                Nombre = Nombre.Replace("&#218;", "É");
                txtNombre.Text = Nombre;
                txtUsuario.Text = grvUsuarios.SelectedRow.Cells[1].Text;
                DDLStatus.SelectedValue = grvUsuarios.SelectedRow.Cells[3].Text;
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
                ddlRol.SelectedIndex = 0;
                txtUsuario.Enabled = true;
                txtUsuario.Text = string.Empty;
                DDLStatus.SelectedIndex = 0;
                txtNombre.Text = string.Empty;
                linkBttnGuardar.Visible = true;
                linkBttnModificar.Visible = false;
                linkBttnCancelar.Visible = false;
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


















