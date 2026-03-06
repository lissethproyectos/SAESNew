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

namespace SAES_v1
{
    public partial class tcapr : System.Web.UI.Page
    {
        #region <Variables>
        Utilidades utils = new Utilidades();
        Catalogos serviceCatalogo = new Catalogos();
        PlanAcademicoService servicePlanAcademico = new PlanAcademicoService();
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

                c_prog_campus.Attributes.Add("onblur", "validarclavePrograma('ContentPlaceHolder1_c_prog_campus')");
                c_prog_campus.Attributes.Add("oninput", "validarclavePrograma('ContentPlaceHolder1_c_prog_campus')");
                LlenaPagina();
                if (!IsPostBack)
                {
                    combo_estatus();
                    combo_campus();
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);

            }
        }

        protected void LlenaPagina()
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "menu", "carga_menu();", true);
            if (Session["usuario"].ToString() == "")
            {
                Response.Redirect("Default.aspx");
            }
            string QerySelect = "select tusme_update, tusme_select from tuser, tusme, tmede " +
                              " where tuser_clave = '" + Session["usuario"].ToString() + "'" +
                              " and tusme_trole_clave = tuser_trole_clave and tusme_tmenu_clave = 1 and tusme_tmenu_clave = tmede_tmenu_clave " +
                              " and tusme_tmede_clave = tmede_clave and tmede_forma='tcapr' ";

            MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            conexion.Open();
            try
            {
                MySqlDataAdapter sqladapter = new MySqlDataAdapter();

                DataSet dssql1 = new DataSet();

                MySqlCommand commandsql1 = new MySqlCommand(QerySelect, conexion);
                sqladapter.SelectCommand = commandsql1;
                sqladapter.Fill(dssql1);
                sqladapter.Dispose();
                commandsql1.Dispose();

                if (dssql1.Tables[0].Rows[0][0].ToString() == "0")
                {
                    btn_programa.Visible = false;
                }
                
            }
            catch (Exception ex)
            {
                ///logs
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcapr", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }
        protected void combo_estatus()
        {
            e_prog_campus.Items.Clear();
            e_prog_campus.Items.Add(new ListItem("Activo", "A"));
            e_prog_campus.Items.Add(new ListItem("Inactivo", "B"));
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
        protected void combo_campus()
        {
            search_campus.Items.Clear();
            string Query = "SELECT DISTINCT tcamp_clave Clave, tcamp_desc Campus FROM tcamp " +
                            "UNION " +
                            "SELECT DISTINCT '0','--------' Campus  " +
                            "ORDER BY 1";
            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            ConexionMySql.Open();
            DataTable TablaCampus = new DataTable();
            MySqlCommand ConsultaMySql = new MySqlCommand();
            MySqlDataReader DatosMySql;
            try
            {
                ConsultaMySql.Connection = ConexionMySql;
                ConsultaMySql.CommandType = CommandType.Text;
                ConsultaMySql.CommandText = Query;
                DatosMySql = ConsultaMySql.ExecuteReader();
                TablaCampus.Load(DatosMySql, LoadOption.OverwriteChanges);
                search_campus.DataSource = TablaCampus;
                search_campus.DataValueField = "Clave";
                search_campus.DataTextField = "Campus";
                search_campus.DataBind();

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcapr", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
            finally
            {
                ConexionMySql.Close();
            }
        }
        protected void grid_programas_bind(string campus)
        {
            
            try
            {
               
                GridProgramas.DataSource = servicePlanAcademico.ObtenerProgramasPorCampus(campus);
                GridProgramas.DataBind();
                GridProgramas.EditIndex = -1;

                //GridProgramas.DataMember = "Programas";
                //GridProgramas.HeaderRow.TableSection = TableRowSection.TableHeader;
                //GridProgramas.UseAccessibleHeader = true;
                GridProgramas.Visible = true;
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcapr", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }
        protected bool validar_clave_programa(string clave)
        {
            string Query = "";
            Query = "SELECT COUNT(*) Indicador FROM tprog WHERE tprog_clave='" + clave + "'";
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
        protected void search_campus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (search_campus.SelectedValue != "0")
            {
                c_prog_campus.Text = null;
                n_prog_campus.Text = null;
                grid_programas_bind(search_campus.SelectedValue);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                guardar_prog.Visible = true;
                update_prog.Visible = false;

            }
            else
            {
                btn_programa.Visible = false;
                GridProgramas.Visible = false;
            }

        }
        protected void c_prog_campus_TextChanged(object sender, EventArgs e)
        {
            if (!validar_clave_programa(c_prog_campus.Text))
            {
                string Query = "SELECT DISTINCT tprog_desc Programa FROM tprog WHERE tprog_clave='" + c_prog_campus.Text + "'";
                MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);

                ConexionMySql.Open();
                MySqlDataAdapter mysqladapter = new MySqlDataAdapter();
                DataSet dsmysql = new DataSet();
                MySqlCommand cmdmysql = new MySqlCommand(Query, ConexionMySql);
                mysqladapter.SelectCommand = cmdmysql;
                mysqladapter.Fill(dsmysql);
                mysqladapter.Dispose();
                cmdmysql.Dispose();
                ConexionMySql.Close();
                n_prog_campus.Text = dsmysql.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                n_prog_campus.Text = null;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "validar_clave", "validarclavePrograma_N('ContentPlaceHolder1_c_prog_campus',1);", true);
            }


        }
        protected void GridProgramas_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridProgramas.SelectedRow;
            search_campus.SelectedValue = row.Cells[9].Text;
            c_prog_campus.Text = row.Cells[1].Text;
            n_prog_campus.Text = HttpUtility.HtmlDecode(row.Cells[2].Text); ;
            combo_estatus();
            e_prog_campus.SelectedValue = row.Cells[6].Text;
            if (row.Cells[5].Text=="S")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "activar_check", "activar_check();", true);
            }
            guardar_prog.Visible = false;
            update_prog.Visible = true;
            grid_programas_bind(search_campus.SelectedValue);
        }

        protected void cancelar_prog_Click(object sender, EventArgs e)
        {
            combo_campus();
            c_prog_campus.Text = null;
            n_prog_campus.Text = null;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "desactivar_check", "desactivar_check();", true);
            combo_estatus();
            guardar_prog.Visible = true;
            update_prog.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
            GridProgramas.Visible = false;
            Gridtprog.Visible = false;


        }

        protected void guardar_prog_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(c_prog_campus.Text))
            {
                string admision = "";
                string selected = Request.Form["customSwitches"];
                string campus = search_campus.SelectedValue;
                if (selected == "on") { admision = "S"; } else { admision = "N"; }
                string Query = "INSERT INTO tcapr Values ('" + search_campus.SelectedValue + "','" + c_prog_campus.Text + "','" + admision + "','" + Session["usuario"].ToString() + "',  current_timestamp() ,'" + e_prog_campus.SelectedValue + "')";
                MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                ConexionMySql.Open();
                MySqlCommand mysqlcmd = new MySqlCommand(Query, ConexionMySql);
                mysqlcmd.CommandType = CommandType.Text;
                try
                {
                    mysqlcmd.ExecuteNonQuery();
                    combo_campus();
                    c_prog_campus.Text = null;
                    n_prog_campus.Text = null;
                    search_campus.SelectedValue = campus;
                    grid_programas_bind(campus);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                }
                catch (Exception ex)
                {
                    string test = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tcapr", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                }
                finally
                {
                    ConexionMySql.Close();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "validar_campos_campus", "validar_campos_campus();", true);
            }
        }

        protected void update_prog_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(c_prog_campus.Text))
            {
                string admision = "";
                string selected = Request.Form["customSwitches"];
                string campus = search_campus.SelectedValue;
                if (selected == "on") { admision = "S"; } else { admision = "N"; }
                string Query = "UPDATE tcapr SET tcapr_ind_admi='" + admision + "',tcapr_estatus='" + e_prog_campus.SelectedValue + "',tcapr_date= current_timestamp(),tcapr_tuser_clave='" + Session["usuario"].ToString() + "' WHERE tcapr_tcamp_clave='" + search_campus.SelectedValue + "' AND tcapr_tprog_clave='" + c_prog_campus.Text + "'";
                MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                ConexionMySql.Open();
                MySqlCommand mysqlcmd = new MySqlCommand(Query, ConexionMySql);
                mysqlcmd.CommandType = CommandType.Text;
                try
                {
                    mysqlcmd.ExecuteNonQuery();
                    combo_campus();
                    c_prog_campus.Text = null;
                    n_prog_campus.Text = null;
                    search_campus.SelectedValue = campus;
                    grid_programas_bind(campus);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "update_p", "update();", true);
                }
                catch (Exception ex)
                {
                    string test = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tcapr", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                }
                finally
                {
                    ConexionMySql.Close();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "validar_campos_campus", "validar_campos_campus();", true);
            }
        }

        

        protected void Gridtprog_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = Gridtprog.SelectedRow;
            c_prog_campus.Text = row.Cells[1].Text;
            n_prog_campus.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);
            Gridtprog.Visible = false;
            //combo_estatus();
        }

        protected void linkBttnBusca_Click(object sender, EventArgs e)
        {
            try
            {

                Gridtprog.DataSource = serviceCatalogo.obtenPrograma();
                Gridtprog.DataBind();
                if (Gridtprog.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "no_existe", "noexist();", true);
                }
                else
                {
                    Gridtprog.HeaderRow.TableSection = TableRowSection.TableHeader;
                    Gridtprog.UseAccessibleHeader = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_tprog", "load_datatable_tprog();", true);
                }
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcapr", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
            Gridtprog.Visible = true;
        }
    }
}