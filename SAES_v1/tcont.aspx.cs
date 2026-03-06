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
    public partial class tcont : System.Web.UI.Page
    {
        #region <Variables>
        Utilidades utils = new Utilidades();
        Catalogos serviceCatalogo = new Catalogos();
        Catalogos_grales_Service serviceCatalogoGrals = new Catalogos_grales_Service();
        List<ModeltpaisResponse> lstPaises = new List<ModeltpaisResponse>();
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
                objPermiso = servicePermiso.ObtenerPermisoFormulario(Session["usuario"].ToString(), "tcont");
                if (objPermiso != null)
                {
                    if (objPermiso.usme_update == "0" || objPermiso.usme_select == "0")
                    {
                        btn_tcont.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                    }

                    grid_tcont_bind();

                }
                else
                {
                    btn_tcont.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                }


            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcont", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);

            }
        }


        private DataTable GetData(MySqlCommand cmd)
        {
            DataTable dt = new DataTable();
            String strConnString = System.Configuration.ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strConnString);
            MySqlDataAdapter sda = new MySqlDataAdapter();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            try
            {
                con.Open();
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                return dt;
            }
            catch
            {
                return null;
            }
            finally
            {
                con.Close();
                sda.Dispose();
                con.Dispose();
            }
        }

        protected void combo_estatus()
        {
            ddl_estatus.Items.Clear();
            ddl_estatus.Items.Add(new ListItem("Activo", "A"));
            ddl_estatus.Items.Add(new ListItem("Inactivo", "B"));
        }
        protected void grid_tcont_bind()
        {
            try
            {
                Gridtcont.DataSource = serviceCatalogo.obtenContactos();
                Gridtcont.DataBind();
                Gridtcont.DataMember = "Tcont";
                Gridtcont.HeaderRow.TableSection = TableRowSection.TableHeader;
                Gridtcont.UseAccessibleHeader = true;
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcont", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            txt_tcont.Text = null;
            txt_nombre.Text = null;
            combo_estatus();
            btn_save.Visible = true;
            btn_update.Visible = false;
            txt_tcont.ReadOnly = false;
            txt_tcont.Text = null;
            btn_cancel.Visible = false;
            //txt_tcont.Attributes.Remove("readonly");
            grid_tcont_bind();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            ModelInsertarContactoResponse objExiste = new ModelInsertarContactoResponse();

            if (!String.IsNullOrEmpty(txt_tcont.Text) && !String.IsNullOrEmpty(txt_nombre.Text))
            {
                try
                {
                    objExiste = serviceCatalogo.InsertarContacto(txt_tcont.Text, txt_nombre.Text,
                        Session["usuario"].ToString(), ddl_estatus.SelectedValue);

                    if (objExiste != null)
                    {
                        if (objExiste.Existe == "0")
                        {
                            txt_tcont.Text = null;
                            txt_nombre.Text = null;
                            ddl_estatus.SelectedIndex=0;
                            grid_tcont_bind();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validarClave('ContentPlaceHolder1_txt_tcont',1);", true);
                            grid_tcont_bind();
                        }
                    }
                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);

                }
                catch (Exception ex)
                {
                    string test = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tcont", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_tcont();", true);
                grid_tcont_bind();
            }


        }

        protected void btn_update_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txt_tcont.Text) && !String.IsNullOrEmpty(txt_nombre.Text))
            {              
                try
                {
                    serviceCatalogo.EditarContacto(txt_tcont.Text, txt_nombre.Text, Session["usuario"].ToString(), ddl_estatus.SelectedValue);
                    txt_tcont.Text = null;
                    txt_tcont.ReadOnly = false;
                    txt_nombre.Text = null;
                    ddl_estatus.SelectedIndex = 0;
                    btn_save.Visible = true;
                    btn_cancel.Visible = false;
                    btn_update.Visible = false;

                    //txt_tcont.ReadOnly = true;
                    btn_cancel.Visible = false;
                    grid_tcont_bind();
                    Gridtcont.SelectedIndex = -1;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "update_p", "update();", true);
                }
                catch (Exception ex)
                {
                    string test = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tcont", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_tcont();", true);
            }
        }

        protected bool valida_tcont(string tcont)
        {
            string Query = "";
            Query = "SELECT COUNT(*) Indicador FROM tcont WHERE tcont_clave='" + tcont + "'";
            MySqlCommand cmd = new MySqlCommand(Query);
            DataTable dt = GetData(cmd);
            if (dt.Rows[0]["Indicador"].ToString() != "0")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        protected void Gridtcont_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = Gridtcont.SelectedRow;
            txt_tcont.Text = row.Cells[1].Text;
            txt_nombre.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);
            combo_estatus();
            ddl_estatus.SelectedValue = row.Cells[3].Text;
            btn_update.Visible = true;
            btn_save.Visible = false;
            txt_tcont.ReadOnly = true;
            btn_cancel.Visible = true;
            grid_tcont_bind();
        }
    }
}