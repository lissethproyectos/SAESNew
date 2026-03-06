using SAES_DBO.Models;
using SAES_Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using static SAES_DBO.Models.ModelMenu;
using static SAES_DBO.Models.ModelServicioSocial;

namespace SAES_v1
{
    public partial class tprss : System.Web.UI.Page
    {
        #region <Variables>
        //Utilidades utils = new Utilidades();
        AlumnoService serviceAlumno = new AlumnoService();
        List<ModelObtenPaisesResponse> lstPaises = new List<ModelObtenPaisesResponse>();
        Catalogos serviceCatalogo = new Catalogos();
        ServicioSocialService serviceServicioSocial = new ServicioSocialService();
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
                    //txtClave.Text = Global.cuenta;
                    //txtDescripcion.Text = Global.cuenta;
                    combo_estatus();
                    txtEmpresa.Text = null;
                    txtCreditos.Text = null;
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
                objPermiso = servicePermiso.ObtenerPermisoFormulario(Session["usuario"].ToString(), "tprss");
                if (objPermiso != null)
                {
                    if (objPermiso.usme_update == "0" || objPermiso.usme_select == "0")
                    {
                        btn_tprss.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                    }
                    else
                    {
                        btn_tprss.Visible = true;
                        grid_tprss_bind();
                        //grid_bind_treti();
                    }
                }
                else
                {
                    btn_tprss.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                }


            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tprss", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);

            }
        }


        private void combo_estatus()
        {

        }

        protected void Gridtprss_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = Gridtprss.SelectedRow;
            try
            {
                txtClave.ReadOnly = true;
                txtClave.Text = row.Cells[1].Text;
                txtDescripcion.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);
                ddlEstatus.SelectedValue = row.Cells[3].Text;
                txtEmpresa.Text = HttpUtility.HtmlDecode(row.Cells[4].Text);
                txtCreditos.Text = row.Cells[5].Text;
                btn_save.Visible = false;
                btn_update.Visible = true;
                btn_cancel.Visible = true;
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tprss", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            txtClave.ReadOnly = false;
            txtClave.Text = null;
            txtDescripcion.Text = null;
            ddlEstatus.SelectedIndex = 0;
            txtEmpresa.Text = null;
            txtCreditos.Text = null;
            txtClave.Focus();
            btn_cancel.Visible = false;
            btn_save.Visible = true;
            btn_update.Visible = false;
            Gridtprss.SelectedIndex = -1;
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            ModelInsertarTprssResponse objExiste = new ModelInsertarTprssResponse();
            if (Page.IsValid == true)
            {
                try
                {
                    objExiste = serviceServicioSocial.InsertarTprss(txtClave.Text, txtDescripcion.Text, txtEmpresa.Text,
                     txtCreditos.Text, ddlEstatus.SelectedValue, Session["usuario"].ToString());
                    if (objExiste != null)
                    {
                        if (objExiste.Existe == "0")
                        {
                            grid_tprss_bind();
                            txtClave.Focus();
                            txtClave.Text = null;
                            txtDescripcion.Text = null;
                            ddlEstatus.SelectedIndex = 0;
                            txtEmpresa.Text = null;
                            txtCreditos.Text = null;
                            txtClave.Focus();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "registro_duplicado();", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                    }
                }
                catch (Exception ex)
                {
                    string test = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tprss", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                }
            }
            else
            {
                this.Page.Validate();
            }
        }

        protected void btn_update_Click(object sender, EventArgs e)
        {
            ModelInsertarTprssResponse objExiste = new ModelInsertarTprssResponse();
            if (Page.IsValid == true)
            {
                try
                {
                    serviceServicioSocial.EditarTprss(txtClave.Text, txtDescripcion.Text, txtEmpresa.Text,
                        txtCreditos.Text, ddlEstatus.SelectedValue, Session["usuario"].ToString());

                    grid_tprss_bind();
                    Gridtprss.SelectedIndex = -1;
                    txtClave.ReadOnly = false;
                    txtClave.Text = null;
                    txtDescripcion.Text = null;
                    ddlEstatus.SelectedIndex = 0;
                    txtEmpresa.Text = null;
                    txtCreditos.Text = null;
                    btn_save.Visible = true;
                    btn_update.Visible = false;
                    btn_cancel.Visible = false;
                    txtClave.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                }
                catch (Exception ex)
                {
                    string test = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tprss", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                }
            }
            else
            {
                this.Page.Validate();
            }
        }

        private void grid_tprss_bind()
        {
            try
            {
                Gridtprss.DataSource = serviceServicioSocial.obtenGridQryTprss();
                Gridtprss.DataBind();
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tprss", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

    }
}