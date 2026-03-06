using MySql.Data.MySqlClient;
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
    public partial class ttama : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Response.Redirect(FormsAuthentication.DefaultUrl);
                Response.End();
            }
            else
            {

                //c_prog_campus.Attributes.Add("onblur", "validarclavePrograma('ContentPlaceHolder1_c_prog_campus')");
                //c_prog_campus.Attributes.Add("oninput", "validarclavePrograma('ContentPlaceHolder1_c_prog_campus')");
                LlenaPagina();
                if (!IsPostBack)
                {
                    combo_estatus();
                    combo_tasa();
                }

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
                              " and tusme_trole_clave = tuser_trole_clave and tusme_tmenu_clave = 6 and tusme_tmenu_clave = tmede_tmenu_clave " +
                              " and tusme_tmede_clave = tmede_clave and tmede_forma='ttama' ";

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
                    btn_tasa_conceptos.Visible = false;
                }

            }
            catch (Exception ex)
            {
                ///logs
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "ttama", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }
        protected void combo_estatus()
        {
            ddl_estatus.Items.Clear();
            ddl_estatus.Items.Add(new ListItem("Activo", "A"));
            ddl_estatus.Items.Add(new ListItem("Inactivo", "B"));

            ddl_tipo.Items.Clear();
            ddl_tipo.Items.Add(new ListItem("-----", "0"));
            ddl_tipo.Items.Add(new ListItem("Curricular", "C"));
            ddl_tipo.Items.Add(new ListItem("Idioma", "I"));

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
        protected void combo_tasa()
        {
            search_tasa.Items.Clear();
            string Query = "SELECT DISTINCT ttasa_clave Clave, ttasa_desc Nombre FROM ttasa " +
                            "UNION " +
                            "SELECT DISTINCT '0','--------' Nombre  " +
                            "ORDER BY 1";
            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            ConexionMySql.Open();
            DataTable Tablatasa = new DataTable();
            MySqlCommand ConsultaMySql = new MySqlCommand();
            MySqlDataReader DatosMySql;
            try
            {
                ConsultaMySql.Connection = ConexionMySql;
                ConsultaMySql.CommandType = CommandType.Text;
                ConsultaMySql.CommandText = Query;
                DatosMySql = ConsultaMySql.ExecuteReader();
                Tablatasa.Load(DatosMySql, LoadOption.OverwriteChanges);
                search_tasa.DataSource = Tablatasa;
                search_tasa.DataValueField = "Clave";
                search_tasa.DataTextField = "Nombre";
                search_tasa.DataBind();

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "ttama", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
            finally
            {
                ConexionMySql.Close();
            }
        }


        protected void grid_conceptos_bind(string tasa)
        {
            string Query = "";
            Query = "SELECT ttama_ttasa_clave tasa, ttasa_desc Nombre, " +
                     " ttama_materias Numero,ttama_estatus Estatus_code, ttama_ttima_clave TTIMA," +
                    " CASE WHEN ttama_ttima_clave = 'C' THEN 'Curricular' WHEN ttama_ttima_clave = 'I' THEN 'Idioma' END Tipo, " +
                    " CASE WHEN ttama_estatus = 'A' THEN 'ACTIVO' ELSE 'INACTIVO' END Estatus, DATE_FORMAT(ttama_date, '%d/%m/%Y') Fecha " +
                    " FROM ttama " +
                    " INNER JOIN ttasa on ttama_ttasa_clave=ttasa_clave " +
                    "WHERE ttama_ttasa_clave = '" + search_tasa.SelectedValue + "' " +
                    "ORDER BY 1";
            try
            {
                MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                ConexionMySql.Open();
                MySqlDataAdapter dataadapter = new MySqlDataAdapter(Query, ConexionMySql);
                DataSet ds = new DataSet();
                dataadapter.Fill(ds, "Materias");
                GridConceptos.DataSource = ds;
                //GridConceptos.EditIndex = -1;
                GridConceptos.DataBind();
                GridConceptos.DataMember = "Materias";
                if (GridConceptos.Rows.Count > 0)
                {
                    GridConceptos.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridConceptos.UseAccessibleHeader = true;
                    GridConceptos.Visible = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);
                }
            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "ttama", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void search_tasa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (search_tasa.SelectedValue != "0")
            {
                grid_conceptos_bind(search_tasa.SelectedValue);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                guardar_tasa.Visible = true;

            }
            else
            {
                btn_tasa_conceptos.Visible = false;
            }

        }

        protected void GridConceptos_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridConceptos.SelectedRow;
            //search_tasa.SelectedValue = row.Cells[9].Text;
            txt_numero.Text = row.Cells[3].Text;
            combo_estatus();
            ddl_tipo.SelectedValue = row.Cells[4].Text;
            ddl_estatus.SelectedValue = row.Cells[6].Text;

            guardar_tasa.Visible = false;
            update_tasa.Visible = true;
            grid_conceptos_bind(search_tasa.SelectedValue);
        }

        protected void cancelar_tasa_Click(object sender, EventArgs e)
        {
            combo_tasa();
            combo_estatus();
            txt_numero.Text = "";
            guardar_tasa.Visible = true;
            update_tasa.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
            GridConceptos.Visible = false;

        }

        protected void guardar_tasa_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txt_numero.Text) && search_tasa.SelectedValue != "0" && ddl_tipo.SelectedValue != "0")
            {
                if (valida_tipo_materia(search_tasa.SelectedValue, ddl_tipo.SelectedValue))
                {

                    MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                    ConexionMySql.Open();
                    string Query = "INSERT INTO ttama Values ('" + search_tasa.SelectedValue + "','" + txt_numero.Text + "','" + ddl_tipo.SelectedValue + "','" + Session["usuario"].ToString() + "',  current_timestamp() ,'" + ddl_estatus.SelectedValue + "')";
                    MySqlCommand mysqlcmd = new MySqlCommand(Query, ConexionMySql);
                    mysqlcmd.CommandType = CommandType.Text;
                    try
                    {
                        mysqlcmd.ExecuteNonQuery();
                        //combo_tasa();
                        txt_numero.Text = null;
                        combo_estatus();
                        //search_tasa.SelectedValue = tasa;
                        grid_conceptos_bind(search_tasa.SelectedValue);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                    }
                    catch (Exception ex)
                    {
                        string test = ex.Message;
                        string mensaje_error = ex.Message.Replace("'", "-");
                        Global.inserta_log(mensaje_error, "ttama", Session["usuario"].ToString());
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                    }
                    finally
                    {

                    }

                    ConexionMySql.Close();
                }
                else
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validarClave('ContentPlaceHolder1_ddl_tipo',1);", true);
                    grid_conceptos_bind(search_tasa.SelectedValue);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "validar_campos_tasa", "validar_campos_tasa();", true);
            }
        }

        protected void update_tasa_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txt_numero.Text) && search_tasa.SelectedValue != "0" && ddl_tipo.SelectedValue != "0")
            {

                MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                ConexionMySql.Open();
                string Query = "UPDATE ttama SET ttama_materias=" + txt_numero.Text + ",ttama_estatus='" + ddl_estatus.SelectedValue + "',ttama_date= current_timestamp(),ttama_tuser_clave='" + Session["usuario"].ToString() + "' WHERE ttama_ttasa_clave='" + search_tasa.SelectedValue + "' AND ttama_ttima_clave='" + ddl_tipo.SelectedValue + "'";
                MySqlCommand mysqlcmd = new MySqlCommand(Query, ConexionMySql);
                mysqlcmd.CommandType = CommandType.Text;
                try
                {
                    mysqlcmd.ExecuteNonQuery();
                    //combo_campus();
                    txt_numero.Text = null;
                    combo_estatus();
                    grid_conceptos_bind(search_tasa.SelectedValue);
                    guardar_tasa.Visible = true;
                    update_tasa.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "update_p", "update();", true);
                }
                catch (Exception ex)
                {
                    string test = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "ttama", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                }
                finally
                {
                    ConexionMySql.Close();
                }
                ConexionMySql.Close();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "validar_campos_tasa", "validar_campos_tasa();", true);
            }
        }

        protected bool valida_tipo_materia(string ttasa, string ttima)
        {
            string Query = "";
            Query = "SELECT COUNT(*) Indicador FROM ttama WHERE ttama_ttasa_clave='" + ttasa + "' and ttama_ttima_clave='" + ttima + "'";
            MySqlCommand cmd = new MySqlCommand(Query);
            DataTable dt = GetData(cmd);
            if (dt.Rows[0][0].ToString() != "0")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}