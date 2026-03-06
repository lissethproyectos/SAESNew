using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SAES_v1
{
    public partial class tfeve : System.Web.UI.Page
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ctrl_fecha", "ctrl_fecha();", true);
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
                              " and tusme_tmede_clave = tmede_clave and tmede_forma='tfeve' ";

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
                    btn_tfeve.Visible = false;
                }

            }
            catch (Exception ex)
            {
                ///logs
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tfeve", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }
        protected void combo_estatus()
        {
            ddl_estatus.Items.Clear();
            ddl_estatus.Items.Add(new ListItem("Activo", "A"));
            ddl_estatus.Items.Add(new ListItem("Inactivo", "B"));
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

        protected void grid_tfeve_bind()
        {
            string Query = "";
            Query = "SELECT tfeve_tpees_clave Periodo, tpees_desc Nom_periodo, " +
                    " tfeve_ttasa_clave tasa, ttasa_desc Nombre, " +
                     " tfeve_numero Numero,DATE_FORMAT(tfeve_vencimiento, '%d/%m/%Y') fecha, tfeve_estatus Estatus_code, " +
                    " CASE WHEN tfeve_estatus = 'A' THEN 'ACTIVO' ELSE 'INACTIVO' END Estatus, DATE_FORMAT(tfeve_date, '%d/%m/%Y') Fecha_reg " +
                    " FROM tfeve " +
                    " INNER JOIN tpees on tfeve_tpees_clave=tpees_clave " +
                    " INNER JOIN ttasa on tfeve_ttasa_clave=ttasa_clave " +
                    "WHERE tfeve_tpees_clave='" + txt_periodo.Text + "' and tfeve_ttasa_clave = '" + search_tasa.SelectedValue + "' " +
                    "ORDER BY 5";
            try
            {
                MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                ConexionMySql.Open();
                MySqlDataAdapter dataadapter = new MySqlDataAdapter(Query, ConexionMySql);
                DataSet ds = new DataSet();
                dataadapter.Fill(ds, "Fechas");
                GridFechas.DataSource = ds;
                //GridConceptos.EditIndex = -1;
                GridFechas.DataBind();
                GridFechas.DataMember = "Fechas";
                if (GridFechas.Rows.Count > 0)
                {
                    GridFechas.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridFechas.UseAccessibleHeader = true;
                    GridFechas.Visible = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);
                }
            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tfeve", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void search_tasa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (search_tasa.SelectedValue != "0")
            {
                grid_tfeve_bind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                guardar_tfeve.Visible = true;

            }
            else
            {
                btn_tfeve.Visible = false;
            }

        }

        protected void GridFechas_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridFechas.SelectedRow;
            //search_tasa.SelectedValue = row.Cells[9].Text;
            txt_numero.Text = row.Cells[5].Text;
            txt_fecha_i.Text = row.Cells[6].Text;
            combo_estatus();
            ddl_estatus.SelectedValue = row.Cells[7].Text;

            guardar_tfeve.Visible = false;
            update_tfeve.Visible = true;
            grid_tfeve_bind();
        }

        protected void cancelar_tfeve_Click(object sender, EventArgs e)
        {
            combo_tasa();
            combo_estatus();
            txt_periodo.Text = "";
            txt_nombre_periodo.Text = "";
            txt_numero.Text = "";
            txt_fecha_i.Text = "";
            guardar_tfeve.Visible = true;
            update_tfeve.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
            GridFechas.Visible = false;

        }

        protected void guardar_tfeve_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txt_periodo.Text) && !String.IsNullOrEmpty(txt_numero.Text) && !String.IsNullOrEmpty(txt_fecha_i.Text) && search_tasa.SelectedValue != "0")
            {
                if (valida_parcialidad(search_tasa.SelectedValue, txt_periodo.Text))
                {

                    string fecha_i_string = txt_fecha_i.Text;
                    string format = "dd/MM/yyyy";
                    DateTime fecha_inicio = DateTime.ParseExact(fecha_i_string, format, CultureInfo.InvariantCulture);
                    MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                    ConexionMySql.Open();
                    string Query = "INSERT INTO tfeve Values ('" + txt_periodo.Text + "','" + search_tasa.SelectedValue + "','" + txt_numero.Text + "', STR_TO_DATE('" + string.Format(txt_fecha_i.Text, "dd/MM/yyyy") + "','%d/%m/%Y') , '" + Session["usuario"].ToString() + "',  current_timestamp() ,'" + ddl_estatus.SelectedValue + "')";
                    MySqlCommand mysqlcmd = new MySqlCommand(Query, ConexionMySql);
                    mysqlcmd.CommandType = CommandType.Text;
                    try
                    {
                        mysqlcmd.ExecuteNonQuery();
                        grid_tfeve_bind();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                    }
                    catch (Exception ex)
                    {
                        string test = ex.Message;
                        string mensaje_error = ex.Message.Replace("'", "-");
                        Global.inserta_log(mensaje_error, "tfeve", Session["usuario"].ToString());
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
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validarClave('ContentPlaceHolder1_txt_numero',1);", true);
                    grid_tfeve_bind();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "validar_campos_tfeve", "validar_campos_tfeve();", true);
            }
        }

        protected void update_tfeve_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txt_periodo.Text) && !String.IsNullOrEmpty(txt_numero.Text) && !String.IsNullOrEmpty(txt_fecha_i.Text) && search_tasa.SelectedValue != "0")
            {
                string fecha_i_string = txt_fecha_i.Text;
                string format = "dd/MM/yyyy";
                DateTime fecha_inicio = DateTime.ParseExact(fecha_i_string, format, CultureInfo.InvariantCulture);
                MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                ConexionMySql.Open();
                string Query = "UPDATE tfeve SET tfeve_numero=" + txt_numero.Text + ",tfeve_vencimiento=STR_TO_DATE('" + string.Format(txt_fecha_i.Text, "dd/MM/yyyy") + "','%d/%m/%Y'), tfeve_estatus='" + ddl_estatus.SelectedValue + "',tfeve_date= current_timestamp(),tfeve_tuser_clave='" + Session["usuario"].ToString() + "' WHERE tfeve_tpees_clave='" + txt_periodo.Text + "' and tfeve_ttasa_clave='" + search_tasa.SelectedValue + "' AND tfeve_numero=" + txt_numero.Text + "";
                MySqlCommand mysqlcmd = new MySqlCommand(Query, ConexionMySql);
                mysqlcmd.CommandType = CommandType.Text;
                try
                {
                    mysqlcmd.ExecuteNonQuery();
                    grid_tfeve_bind();
                    guardar_tfeve.Visible = true;
                    update_tfeve.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "update_p", "update();", true);
                }
                catch (Exception ex)
                {
                    string test = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tfeve", Session["usuario"].ToString());
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "validar_campos_tfeve", "validar_campos_tfeve();", true);
            }
        }

        protected bool valida_parcialidad(string ttasa, string tpees)
        {
            string Query = "";
            Query = "SELECT COUNT(*) Indicador FROM tfeve WHERE tfeve_ttasa_clave='" + ttasa + "' and tfeve_tpees_clave='" + tpees + "' and tfeve_numero=" + txt_numero.Text;
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

        protected void grid_periodos_bind(object sender, EventArgs e)
        {
            string Query = "";
            Query = " select tpees_clave Periodo, tpees_desc Descripcion, date_format(tpees_inicio,'%d/%m/%Y') Fecha_ini, date_format(tpees_fin,'%d/%m/%Y') Fecha_fin from tpees where tpees_estatus = 'A' and tpees_fin >= curdate() ";

            try
            {
                MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                ConexionMySql.Open();
                MySqlDataAdapter dataadapter = new MySqlDataAdapter(Query, ConexionMySql);
                DataSet ds = new DataSet();
                dataadapter.Fill(ds, "Periodos");
                Gridtpees.DataSource = ds;
                //GridConceptos.EditIndex = -1;
                Gridtpees.DataBind();
                Gridtpees.DataMember = "Periodos";
                if (Gridtpees.Rows.Count > 0)
                {
                    Gridtpees.HeaderRow.TableSection = TableRowSection.TableHeader;
                    Gridtpees.UseAccessibleHeader = true;
                    Gridtpees.Visible = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_tpees", "load_datatable_tpees();", true);
                }
            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tfeve", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void Gridtpees_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = Gridtpees.SelectedRow;
            //search_tasa.SelectedValue = row.Cells[9].Text;
            txt_periodo.Text = row.Cells[1].Text;
            txt_nombre_periodo.Text = row.Cells[2].Text;
            Gridtpees.Visible = false;
            grid_tfeve_bind();
        }

        protected void Busca_Periodo(object sender, EventArgs e)
        {
            string QerySelect = "select tpees_desc from tpees " +
                              " where tpees_clave = '" + txt_periodo.Text + "'";

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

                if (dssql1.Tables[0].Rows.Count != 0)
                {
                    txt_nombre_periodo.Text = dssql1.Tables[0].Rows[0][0].ToString();
                }
                else
                {
                    txt_nombre_periodo.Text = "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "No_Exists", "No_Exists();", true);
                }

            }
            catch (Exception ex)
            {
                ///logs
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tfeve", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void linkBttnBusca_Click(object sender, EventArgs e)
        {
            string Query = "";
            Query = " select tpees_clave Periodo, tpees_desc Descripcion, date_format(tpees_inicio,'%d/%m/%Y') Fecha_ini, date_format(tpees_fin,'%d/%m/%Y') Fecha_fin from tpees where tpees_estatus = 'A' and tpees_fin >= curdate() ";

            try
            {
                MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                ConexionMySql.Open();
                MySqlDataAdapter dataadapter = new MySqlDataAdapter(Query, ConexionMySql);
                DataSet ds = new DataSet();
                dataadapter.Fill(ds, "Periodos");
                Gridtpees.DataSource = ds;
                //GridConceptos.EditIndex = -1;
                Gridtpees.DataBind();
                Gridtpees.DataMember = "Periodos";
                if (Gridtpees.Rows.Count > 0)
                {
                    Gridtpees.HeaderRow.TableSection = TableRowSection.TableHeader;
                    Gridtpees.UseAccessibleHeader = true;
                    Gridtpees.Visible = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_tpees", "load_datatable_tpees();", true);
                }
            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tfeve", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }
    }
}