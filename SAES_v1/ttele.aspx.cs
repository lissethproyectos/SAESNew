using MySql.Data.MySqlClient;
using SAES_DBO.Models;
using SAES_Services;
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
    public partial class ttele : System.Web.UI.Page
    {
        Catalogos serviceCatalogo = new Catalogos();
        MenuService servicePermiso = new MenuService();

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
                objPermiso = servicePermiso.ObtenerPermisoFormulario(Session["usuario"].ToString(), "ttele");
                if (objPermiso != null)
                {
                    if (objPermiso.usme_update == "0" || objPermiso.usme_select == "0")
                    {
                        btn_ttele.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                    }

                    grid_ttele_bind();

                }
                else
                {
                    btn_ttele.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                }


            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "ttele", Session["usuario"].ToString());
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
        protected void grid_ttele_bind()
        {
            try
            {
                Gridttele.DataSource = serviceCatalogo.obtenTelefonos();
                Gridttele.DataBind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "ttele", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            txt_ttele.Text = null;
            txt_nombre.Text = null;
            combo_estatus();
            btn_save.Visible = true;
            btn_update.Visible = false;
            btn_cancel.Visible = false;
            txt_ttele.Enabled = true;
            //txt_ttele.Attributes.Remove("readonly");
            grid_ttele_bind();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            ModelInsertarTelefonoResponse objExiste = new ModelInsertarTelefonoResponse();

            if (!String.IsNullOrEmpty(txt_ttele.Text) && !String.IsNullOrEmpty(txt_nombre.Text))
            {
                string strCadSQL = "INSERT INTO ttele Values ('" + txt_ttele.Text + "','" + txt_nombre.Text + "','" +
                Session["usuario"].ToString() + "',current_timestamp(),'" + ddl_estatus.SelectedValue + "')";
                try
                {
                    objExiste=serviceCatalogo.InsertarTelefono(txt_ttele.Text, txt_nombre.Text, Session["usuario"].ToString(), ddl_estatus.SelectedValue);

                    if (objExiste.Existe != null)
                    {
                        if (objExiste.Existe == "0")
                        {
                            txt_ttele.Text = string.Empty;
                            txt_nombre.Text = string.Empty;
                            ddl_estatus.SelectedIndex = 0;
                            grid_ttele_bind();
                            Gridttele.SelectedIndex = -1;
                            btn_update.Visible = false;
                            btn_save.Visible = true;
                            btn_cancel.Visible = false;

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validarClave('ContentPlaceHolder1_txt_ttele',1);", true);
                            grid_ttele_bind();
                        }
                    }
                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);

                }
                catch (Exception ex)
                {
                    string test = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "ttele", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                }


            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_ttele();", true);
                grid_ttele_bind();
            }


        }

        protected void btn_update_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txt_ttele.Text) && !String.IsNullOrEmpty(txt_nombre.Text))
            {
                //string strCadSQL = "UPDATE ttele SET ttele_desc='" + txt_nombre.Text + "', " +
                //    "ttele_estatus='" + ddl_estatus.SelectedValue + "', ttele_user='" + Session["usuario"].ToString() + "', ttele_date=CURRENT_TIMESTAMP() WHERE ttele_clave='" + txt_ttele.Text + "'";
                //MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                //conexion.Open();
                //MySqlCommand mysqlcmd = new MySqlCommand(strCadSQL, conexion);
                //mysqlcmd.CommandType = CommandType.Text;
                try
                {

                    serviceCatalogo.EditarTelefono(txt_ttele.Text, txt_nombre.Text, Session["usuario"].ToString(), ddl_estatus.SelectedValue);
                    txt_ttele.Enabled = true;
                    txt_nombre.Text = string.Empty;
                    txt_ttele.Text = string.Empty;
                    ddl_estatus.SelectedIndex = 0;
                    grid_ttele_bind();
                    Gridttele.SelectedIndex = -1;
                    btn_update.Visible = false;
                    btn_save.Visible = true;
                    btn_cancel.Visible = false;


                    ScriptManager.RegisterStartupScript(this, this.GetType(), "update_p", "update();", true);
                }
                catch (Exception ex)
                {
                    string test = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "ttele", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_ttele();", true);
            }
        }

        protected bool valida_ttele(string ttele)
        {
            string Query = "";
            Query = "SELECT COUNT(*) Indicador FROM ttele WHERE ttele_clave='" + ttele + "'";
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

        protected void Gridttele_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = Gridttele.SelectedRow;
            txt_ttele.Text = row.Cells[1].Text;
            btn_cancel.Visible = true;
            txt_nombre.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);
            combo_estatus();
            ddl_estatus.SelectedValue = row.Cells[3].Text;
            btn_update.Visible = true;
            btn_save.Visible = false;
            txt_ttele.Enabled = false;
            //txt_ttele.Attributes.AddAttributes("readonly");

            ///grid_ttele_bind();
        }
    }
}