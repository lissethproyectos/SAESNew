using SAES_DBO.Models;
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
    public partial class torca : System.Web.UI.Page
    {
        #region <Variables>
        Utilidades utils = new Utilidades();
        Catalogos serviceCatalogo = new Catalogos();
        List<ModelObtenPaisesResponse> lstPaises = new List<ModelObtenPaisesResponse>();
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
                    combo_estatus();
                    grid_torca_bind();
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
                objPermiso = servicePermiso.ObtenerPermisoFormulario(Session["usuario"].ToString(), "torca");
                if (objPermiso != null)
                {
                    if (objPermiso.usme_update == "0" || objPermiso.usme_select == "0")
                    {
                        btn_torca.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                    }
                    else
                    {
                        combo_estatus();
                        grid_torca_bind();
                    }
                }
                else
                {
                    btn_torca.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                }


            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "torca", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);

            }
        }

        protected void combo_estatus()
        {
            ddl_estatus.Items.Clear();
            ddl_estatus.Items.Add(new ListItem("Activo", "A"));
            ddl_estatus.Items.Add(new ListItem("Inactivo", "B"));
        }

        private void grid_torca_bind()
        {
            try
            {
                Gridtorca.DataSource = serviceCatalogo.obtenGridFuncionarios();
                Gridtorca.DataBind();
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "torca", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {

        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            ModelInstorcaResponse objExiste = new ModelInstorcaResponse();
            if (!String.IsNullOrEmpty(txt_clave.Text) && !String.IsNullOrEmpty(txt_nombre.Text))
            {

                try
                {
                    objExiste = serviceCatalogo.Ins_torca(txt_clave.Text, txt_nombre.Text, Session["usuario"].ToString(), ddl_estatus.SelectedValue);
                    if (objExiste != null)
                    {
                        if (objExiste.Existe == "0")
                        {
                            txt_clave.Text = null;
                            txt_nombre.Text = null;
                            grid_torca_bind();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validarClave('ContentPlaceHolder1_txt_clave',1);", true);
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validarClave('ContentPlaceHolder1_txt_tdire',1);", true);

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
                    Global.inserta_log(mensaje_error, "torca", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                }



            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_tdire();", true);
                grid_torca_bind();
            }
        }

        protected void btn_update_Click(object sender, EventArgs e)
        {

            try
            {
                //mysqlcmd.ExecuteNonQuery();
                serviceCatalogo.Upd_torca(txt_clave.Text, txt_nombre.Text, Session["usuario"].ToString(), ddl_estatus.SelectedValue);
                txt_nombre.Text = null;
                ddl_estatus.SelectedIndex = 0;
                txt_clave.ReadOnly = false;
                grid_torca_bind();
                Gridtorca.SelectedIndex = -1;
                btn_update.Visible = false;
                btn_save.Visible = true;
                txt_clave.Text = null;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "update_p", "update();", true);
            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tdire", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
            }

        }

        protected void Gridtorca_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = Gridtorca.SelectedRow;
            txt_clave.Text = row.Cells[1].Text;
            txt_nombre.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);
            combo_estatus();
            string st = row.Cells[3].Text;
            string st_nom = row.Cells[4].Text;
            ddl_estatus.SelectedValue = row.Cells[3].Text;
            btn_update.Visible = true;
            btn_save.Visible = false;
            txt_clave.ReadOnly = true;
        }
    }
}