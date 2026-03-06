using MySql.Data.MySqlClient;
using SAES_DBO.Models;
using SAES_Services;
using SAES_v1.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using static SAES_DBO.Models.ModelMenu;

namespace SAES_v1
{
    public partial class tnive : System.Web.UI.Page
    {
        #region <Variables>
        Utilidades utils = new Utilidades();
        Catalogos serviceCatalogo = new Catalogos();
        DocenteService serviceDocente = new DocenteService();
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

                //c_campus.Attributes.Add("onblur", "validarclaveCampus('ContentPlaceHolder1_c_campus',0)");
                //c_campus.Attributes.Add("oninput", "validarclaveCampus('ContentPlaceHolder1_c_campus',0)");
                //n_campus.Attributes.Add("onblur", "validarNombreCampus('ContentPlaceHolder1_n_campus')");
                //n_campus.Attributes.Add("oninput", "validarNombreCampus('ContentPlaceHolder1_n_campus')");

                if (!IsPostBack)
                {
                    LlenaPagina();
                    combo_estatus();
                }

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
                objPermiso = servicePermiso.ObtenerPermisoFormulario(Session["usuario"].ToString(), "tnive");
                if (objPermiso != null)
                {
                    if (objPermiso.usme_update == "0" || objPermiso.usme_select == "0")
                    {
                        btn_tnive.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                    }
                    else
                        grid_tnive_bind();
                }
                else
                {
                    btn_tnive.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                }


            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tnive", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);

            }
        }


        protected void combo_estatus()
        {
            ddl_estatus.Items.Clear();
            ddl_estatus.Items.Add(new ListItem("Activo", "A"));
            ddl_estatus.Items.Add(new ListItem("Inactivo", "B"));
        }
        protected void grid_tnive_bind()
        {

            try
            {
                Gridtnive.DataSource = serviceCatalogo.obtenGridTNive();
                Gridtnive.DataBind();
                Gridtnive.DataMember = "Tnive";
                Gridtnive.HeaderRow.TableSection = TableRowSection.TableHeader;
                Gridtnive.UseAccessibleHeader = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tnive", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            txt_tnive.Text = null;
            txt_nombre.Text = null;
            combo_estatus();
            btn_save.Visible = true;
            btn_update.Visible = false;
            txt_tnive.Attributes.Remove("readonly");
            grid_tnive_bind();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            ModelInsertarNivelResponse objExiste = new ModelInsertarNivelResponse();
            try
            {
                objExiste = serviceCatalogo.InsertarNivel(txt_tnive.Text, txt_nombre.Text, Session["usuario"].ToString(), ddl_estatus.SelectedValue);

                if (!String.IsNullOrEmpty(txt_tnive.Text) && !String.IsNullOrEmpty(txt_nombre.Text))
                {
                    if (objExiste.Existe == "0")
                    {
                        txt_tnive.Text = null;
                        txt_nombre.Text = null;
                        combo_estatus();
                        grid_tnive_bind();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validarClave('ContentPlaceHolder1_txt_tnive',1);", true);
                        grid_tnive_bind();
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_tnive();", true);
                    grid_tnive_bind();
                }
            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tnive", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
            }
        }



        protected void btn_update_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txt_tnive.Text) && !String.IsNullOrEmpty(txt_nombre.Text))
            {
                //string strCadSQL = "UPDATE tnive SET tnive_desc='" + txt_nombre.Text + "', tnive_estatus='" + ddl_estatus.SelectedValue + "', tnive_user='" + Session["usuario"].ToString() + "', tnive_date=CURRENT_TIMESTAMP() WHERE tnive_clave='" + txt_tnive.Text + "'";
                
                try
                {
                    serviceCatalogo.EditarNivel(txt_tnive.Text, txt_nombre.Text, Session["usuario"].ToString(), ddl_estatus.SelectedValue);
                    grid_tnive_bind();
                    Gridtnive.SelectedIndex = -1;
                    txt_tnive.Text = null;
                    txt_nombre.Text = null;
                    txt_tnive.ReadOnly = false;
                    btn_save.Visible = true;
                    btn_update.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "update_p", "update();", true);
                }
                catch (Exception ex)
                {
                    string test = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tnive", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_tnive();", true);
            }
        }


        protected void Gridtnive_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = Gridtnive.SelectedRow;
            txt_tnive.Text = row.Cells[1].Text;
            txt_nombre.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);
            combo_estatus();
            ddl_estatus.SelectedValue = row.Cells[3].Text;
            btn_update.Visible = true;
            btn_save.Visible = false;
            txt_tnive.Attributes.Add("readonly", "");
            grid_tnive_bind();
        }
    }
}