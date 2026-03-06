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
    public partial class tcomb : System.Web.UI.Page
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
                              " and tusme_tmede_clave = tmede_clave and tmede_forma='tcomb' ";

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
                Global.inserta_log(mensaje_error, "tcomb", Session["usuario"].ToString());
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
                Global.inserta_log(mensaje_error, "tcomb", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
            finally
            {
                ConexionMySql.Close();
            }
        }

        protected void txt_concepto_TextChanged(object sender, EventArgs e)
        {

            string Query = "SELECT DISTINCT tcoco_desc Concepto FROM tcoco WHERE tcoco_clave='" + txt_concepto.Text + "'";
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
            if (dsmysql.Tables[0].Rows.Count > 0)
            {
                txt_nombre.Text = dsmysql.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "No_Exists", "No_Exists();", true);
            }
        }

        protected void grid_conceptos_bind(string tasa)
        {
            string Query = "";
            Query = "SELECT tcomb_ttasa_clave tasa, ttasa_desc Nombre_tasa, " +
                     " tcomb_tcoco_clave Clave,tcoco_desc Nombre,tcomb_estatus Estatus_code, " +
                    " CASE WHEN tcoco_tipo = 'C' THEN 'CARGO' ELSE 'PAGO' END Tipo, " +
                    " CASE WHEN tcomb_estatus = 'A' THEN 'ACTIVO' ELSE 'INACTIVO' END Estatus, DATE_FORMAT(tcomb_date, '%d/%m/%Y') Fecha " +
                    " FROM tcomb " +
                    " INNER JOIN tcoco ON tcoco_clave = tcomb_tcoco_clave and tcoco_tipo='C' " +
                    " INNER JOIN ttasa on tcomb_ttasa_clave=ttasa_clave " +
                    "WHERE tcomb_ttasa_clave = '" + search_tasa.SelectedValue + "' " +
                    "ORDER BY 1";
            try
            {
                MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                ConexionMySql.Open();
                MySqlDataAdapter dataadapter = new MySqlDataAdapter(Query, ConexionMySql);
                DataSet ds = new DataSet();
                dataadapter.Fill(ds, "Conceptos");
                GridConceptos.DataSource = ds;
                GridConceptos.EditIndex = -1;
                GridConceptos.DataBind();
                GridConceptos.DataMember = "Conceptos";
                GridConceptos.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridConceptos.UseAccessibleHeader = true;
                GridConceptos.Visible = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcomb", Session["usuario"].ToString());
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
                Gridttasa.Visible = false;
            }

        }

        protected void GridConceptos_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridConceptos.SelectedRow;
            //search_tasa.SelectedValue = row.Cells[9].Text;
            txt_concepto.Text = row.Cells[3].Text;
            txt_nombre.Text = HttpUtility.HtmlDecode(row.Cells[4].Text);
            combo_estatus();
            ddl_estatus.SelectedValue = row.Cells[6].Text;

            guardar_tasa.Visible = false;
            update_tasa.Visible = true;
            grid_conceptos_bind(search_tasa.SelectedValue);
        }

        protected void cancelar_tasa_Click(object sender, EventArgs e)
        {
            combo_tasa();
            combo_estatus();
            txt_concepto.Text = "";
            txt_nombre.Text = "";
            guardar_tasa.Visible = true;
            update_tasa.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
            GridConceptos.Visible = false;
            Gridttasa.Visible = false;


        }

        protected void guardar_tasa_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txt_concepto.Text) && search_tasa.SelectedValue != "0")
            {
                if (valida_concepto(search_tasa.SelectedValue, txt_concepto.Text))
                {

                    string Querytcoco = "SELECT DISTINCT tcoco_desc Concepto FROM tcoco WHERE tcoco_clave='" + txt_concepto.Text + "'";
                    MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);

                    ConexionMySql.Open();
                    MySqlDataAdapter mysqladapter = new MySqlDataAdapter();
                    DataSet dsmysql = new DataSet();
                    MySqlCommand cmdmysql = new MySqlCommand(Querytcoco, ConexionMySql);
                    mysqladapter.SelectCommand = cmdmysql;
                    mysqladapter.Fill(dsmysql);
                    mysqladapter.Dispose();
                    cmdmysql.Dispose();

                    if (dsmysql.Tables[0].Rows.Count == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "No_Exists", "No_Exists();", true);
                    }
                    else
                    {

                        string Query = "INSERT INTO tcomb Values ('" + search_tasa.SelectedValue + "','" + txt_concepto.Text + "','" + Session["usuario"].ToString() + "',  current_timestamp() ,'" + ddl_estatus.SelectedValue + "')";
                        MySqlCommand mysqlcmd = new MySqlCommand(Query, ConexionMySql);
                        mysqlcmd.CommandType = CommandType.Text;
                        try
                        {
                            mysqlcmd.ExecuteNonQuery();
                            //combo_tasa();
                            txt_concepto.Text = null;
                            txt_nombre.Text = null;
                            //search_tasa.SelectedValue = tasa;
                            grid_conceptos_bind(search_tasa.SelectedValue);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                            ConexionMySql.Close();
                        }
                        catch (Exception ex)
                        {
                            string test = ex.Message;
                            string mensaje_error = ex.Message.Replace("'", "-");
                            Global.inserta_log(mensaje_error, "tcomb", Session["usuario"].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                        }
                        finally
                        {

                        }
                    }

                }

                else
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validarClave('ContentPlaceHolder1_txt_concepto',1);", true);
                    grid_conceptos_bind(search_tasa.SelectedValue);
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "validar_campos_tasa", "validar_campos_tasa();", true);
            }
        }

        protected void Busqueda_tasa(object sender, EventArgs e)
        {
            if (Gridttasa.Visible == true)
            {

                Gridttasa.Visible = false;
            }
            else
            {
                if (search_tasa.SelectedValue != "0")
                {
                    string strQuery = "";
                    strQuery = " select tcoco_clave clave, tcoco_desc nombre " +
                               " from tcoco " +
                               " where tcoco_clave not in (select tcomb_tcoco_clave from tcomb " +
                               "      where tcomb_ttasa_clave ='" + search_tasa.SelectedValue + "')" +
                               "  and tcoco_tipo='C' ";

                    strQuery = strQuery + " order by clave ";

                    //resultado.Text = strQuery;
                    MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                    conexion.Open();
                    try
                    {
                        MySqlDataAdapter dataadapter = new MySqlDataAdapter(strQuery, conexion);
                        DataSet ds = new DataSet();
                        dataadapter.Fill(ds, "Programa");
                        Gridttasa.DataSource = ds;
                        Gridttasa.DataBind();
                        Gridttasa.DataMember = "Programa";
                        if (Gridttasa.Rows.Count == 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "no_existe", "noexist();", true);
                        }
                        else
                        {
                            Gridttasa.HeaderRow.TableSection = TableRowSection.TableHeader;
                            Gridttasa.UseAccessibleHeader = true;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_ttasa", "load_datatable_ttasa();", true);
                        }
                    }
                    catch (Exception ex)
                    {
                        //resultado.Text = ex.Message;
                        string mensaje_error = ex.Message.Replace("'", "-");
                        Global.inserta_log(mensaje_error, "tcomb", Session["usuario"].ToString());
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                    }
                    Gridttasa.Visible = true;
                    conexion.Close();
                }
            }
        }

        protected void GridConc_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = Gridttasa.SelectedRow;
            txt_concepto.Text = row.Cells[1].Text;
            txt_nombre.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);
            Gridttasa.Visible = false;
            //combo_estatus();
        }

        protected void update_tasa_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txt_concepto.Text) && search_tasa.SelectedValue != "0")
            {
                string Querytcoco = "SELECT DISTINCT tcoco_desc Concepto FROM tcoco WHERE tcoco_clave='" + txt_concepto.Text + "'";
                MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);

                ConexionMySql.Open();
                MySqlDataAdapter mysqladapter = new MySqlDataAdapter();
                DataSet dsmysql = new DataSet();
                MySqlCommand cmdmysql = new MySqlCommand(Querytcoco, ConexionMySql);
                mysqladapter.SelectCommand = cmdmysql;
                mysqladapter.Fill(dsmysql);
                mysqladapter.Dispose();
                cmdmysql.Dispose();

                if (dsmysql.Tables[0].Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "No_Exists", "No_Exists();", true);
                }
                else
                {
                    string Query = "UPDATE tcomb SET tcomb_estatus='" + ddl_estatus.SelectedValue + "',tcomb_date= current_timestamp(),tcomb_tuser_clave='" + Session["usuario"].ToString() + "' WHERE tcomb_ttasa_clave='" + search_tasa.SelectedValue + "' AND tcomb_tcoco_clave='" + txt_concepto.Text + "'";
                    MySqlCommand mysqlcmd = new MySqlCommand(Query, ConexionMySql);
                    mysqlcmd.CommandType = CommandType.Text;
                    try
                    {
                        mysqlcmd.ExecuteNonQuery();
                        //combo_campus();
                        txt_concepto.Text = null;
                        txt_nombre.Text = null;
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
                        Global.inserta_log(mensaje_error, "tcomb", Session["usuario"].ToString());
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                    }
                    finally
                    {
                        ConexionMySql.Close();
                    }
                }
                ConexionMySql.Close();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "validar_campos_tasa", "validar_campos_tasa();", true);
            }
        }
        protected bool valida_concepto(string ttasa, string tcoco)
        {
            string Query = "";
            Query = "SELECT COUNT(*) Indicador FROM tcomb WHERE tcomb_ttasa_clave='" + ttasa + "' and tcomb_tcoco_clave='" + tcoco + "'";
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