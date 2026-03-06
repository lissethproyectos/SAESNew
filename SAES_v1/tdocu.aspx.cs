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
    public partial class tdocu : System.Web.UI.Page
    {
        #region <Variables>
        Utilidades utils = new Utilidades();
        Catalogos serviceCatalogo = new Catalogos();
        MenuService servicePermiso = new MenuService();
        List<ModelObtenPaisesResponse> lstPaises = new List<ModelObtenPaisesResponse>();
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
                objPermiso = servicePermiso.ObtenerPermisoFormulario(Session["usuario"].ToString(), "tdocu");
                if (objPermiso != null)
                {
                    if (objPermiso.usme_update == "0" || objPermiso.usme_select == "0")
                    {
                        btn_tdocu.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                    }
                    else
                        grid_tdocu_bind();
                }
                else
                {
                    btn_tdocu.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                }
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tdocu", Session["usuario"].ToString());
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

            ddl_tipo.Items.Clear();
            ddl_tipo.Items.Add(new ListItem("Original", "O"));
            ddl_tipo.Items.Add(new ListItem("Copia", "C"));

            ddl_formato.Items.Clear();
            ddl_formato.Items.Add(new ListItem("Físico", "F"));
            ddl_formato.Items.Add(new ListItem("Digital", "D"));

        }
        protected void grid_tdocu_bind()
        {          
            try
            {
                Gridtdocu.DataSource = serviceCatalogo.obtenCatDocumentos();
                Gridtdocu.DataBind();
                Gridtdocu.DataMember = "Tdocu";
                Gridtdocu.HeaderRow.TableSection = TableRowSection.TableHeader;
                Gridtdocu.UseAccessibleHeader = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tdocu", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            txt_tdocu.Text = null;
            txt_nombre.Text = null;
            btn_cancel.Visible = false;
            combo_estatus();
            btn_save.Visible = true;
            btn_update.Visible = false;
            txt_tdocu.Attributes.Remove("readonly");
            grid_tdocu_bind();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txt_tdocu.Text) && !String.IsNullOrEmpty(txt_nombre.Text))
            {
                if (valida_tdocu(txt_tdocu.Text))
                {
                    MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                    conexion.Open();
                    string strCadSQL = "INSERT INTO tdocu Values ('" + txt_tdocu.Text + "','" + txt_nombre.Text + "','" +
                    ddl_tipo.SelectedValue + "','" + ddl_formato.SelectedValue + "','" + ddl_estatus.SelectedValue + "','" +
                    Session["usuario"].ToString() + "',current_timestamp())";

                    string Consecutivo = "select max(IDTipoDocumento)+1 from TipoDocumento ";


                    //serviceCatalogo.InsertarCatCampus(txt_tdocu.Text, txt_nombre.Text,  ;


                    MySqlDataAdapter sqladapter = new MySqlDataAdapter();

                    DataSet dssql1 = new DataSet();

                    MySqlCommand commandsql1 = new MySqlCommand(Consecutivo, conexion);
                    sqladapter.SelectCommand = commandsql1;
                    sqladapter.Fill(dssql1);
                    sqladapter.Dispose();
                    commandsql1.Dispose();
                    double cons = 0;
                    if (dssql1.Tables[0].Rows.Count == 0)
                    {
                        cons = 1;
                    }
                    else
                    {
                        cons = Convert.ToDouble(dssql1.Tables[0].Rows[0][0].ToString());
                    }

                    string strTipoDocumento = "INSERT INTO TipoDocumento Values ("+ cons + ",'" + txt_tdocu.Text + "','" + txt_nombre.Text + "','.',0,0,'.',1, curdate())";

                    
                    MySqlCommand mysqlcmd = new MySqlCommand(strCadSQL, conexion);
                    mysqlcmd.CommandType = CommandType.Text;

                    MySqlCommand mysqlTipoDoc = new MySqlCommand(strTipoDocumento, conexion);
                    mysqlTipoDoc.CommandType = CommandType.Text;

                    try
                    {
                        mysqlcmd.ExecuteNonQuery();
                        mysqlTipoDoc.ExecuteNonQuery();
                        txt_tdocu.Text = null;
                        txt_nombre.Text = null;
                        //combo_estatus();
                        grid_tdocu_bind();

                        //Gridtdocu.SelectedIndex = -1;
                        ddl_estatus.SelectedIndex = 0;
                        ddl_tipo.SelectedIndex = 0;
                        ddl_formato.SelectedIndex = 0;
                        //btn_update.Visible = false;
                        //btn_save.Visible = true;

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                    }
                    catch (Exception ex)
                    {
                        string test = ex.Message;
                        string mensaje_error = ex.Message.Replace("'", "-");
                        Global.inserta_log(mensaje_error, "tdocu", Session["usuario"].ToString());
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                    }
                    finally
                    {
                        conexion.Close();
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validarClave('ContentPlaceHolder1_txt_tdocu',1);", true);
                    grid_tdocu_bind();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_tdocu();", true);
                grid_tdocu_bind();
            }


        }

        protected void btn_update_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txt_tdocu.Text) && !String.IsNullOrEmpty(txt_nombre.Text))
            {
                string strCadSQL = "UPDATE tdocu SET tdocu_desc='" + txt_nombre.Text + "', " +
                    " tdocu_tipo='" + ddl_tipo.SelectedValue + "', tdocu_fis_dig='" + ddl_formato.SelectedValue + "'," +
                    " tdocu_estatus='" + ddl_estatus.SelectedValue + "', tdocu_tuser_clave='" + Session["usuario"].ToString() + "', tdocu_date=CURRENT_TIMESTAMP() WHERE tdocu_clave='" + txt_tdocu.Text + "'";
                MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                conexion.Open();
                MySqlCommand mysqlcmd = new MySqlCommand(strCadSQL, conexion);
                mysqlcmd.CommandType = CommandType.Text;
                try
                {
                    mysqlcmd.ExecuteNonQuery();
                    grid_tdocu_bind();
                    Gridtdocu.SelectedIndex = -1;
                    ddl_estatus.SelectedIndex = 0;
                    ddl_tipo.SelectedIndex= 0;
                    ddl_formato.SelectedIndex = 0;
                    btn_update.Visible = false;
                    btn_cancel.Visible = false;
                    btn_save.Visible = true;
                    txt_tdocu.Text = null;
                    txt_nombre.Text = null;
                    txt_tdocu.ReadOnly = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "update_p", "update();", true);
                }
                catch (Exception ex)
                {
                    string test = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tdocu", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                }
                finally
                {
                    conexion.Close();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_tdocu();", true);
            }
        }

        protected bool valida_tdocu(string tdocu)
        {
            string Query = "";
            Query = "SELECT COUNT(*) Indicador FROM tdocu WHERE tdocu_clave='" + tdocu + "'";
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

        protected void Gridtdocu_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = Gridtdocu.SelectedRow;
            txt_tdocu.Text = row.Cells[1].Text;
            txt_nombre.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);
            combo_estatus();
            ddl_estatus.SelectedValue = row.Cells[7].Text;
            ddl_tipo.SelectedValue = row.Cells[3].Text;
            ddl_formato.SelectedValue = row.Cells[5].Text;
            btn_update.Visible = true;
            btn_save.Visible = false;
            btn_cancel.Visible = true;
            txt_tdocu.ReadOnly = true;
            grid_tdocu_bind();
        }
    }
}