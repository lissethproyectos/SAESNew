using applyWeb.Data;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.X509;
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
    public partial class tplan : System.Web.UI.Page
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


                LlenaPagina();
                if (!IsPostBack)
                {
                    Gridtplan.DataSource = null;
                    Gridtplan.DataBind();
                    combo_estatus();
                    combo_tipo();
                    programas();
                    materias();
                    areas();
                    periodos();

                   
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_Materia", "load_datatable_Materia();", true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_Materia", "setTimeout(load_datatable_Materia, 100);", true);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_Area", "load_datatable_Area();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_Periodo", "load_datatable_Periodo();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_Programa", "load_datatable_Programa();", true);


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
                              " and tusme_trole_clave = tuser_trole_clave and tusme_tmenu_clave = 5 and tusme_tmenu_clave = tmede_tmenu_clave " +
                              " and tusme_tmede_clave = tmede_clave and tmede_forma='tplan' ";

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
                    btn_tplan.Visible = false;
                }

            }
            catch (Exception ex)
            {
                ///logs
            }
        }
        protected void combo_estatus()
        {
            ddl_estatus.Items.Clear();
            ddl_estatus.Items.Add(new ListItem("Activo", "A"));
            ddl_estatus.Items.Add(new ListItem("Inactivo", "B"));
        }
        protected void combo_tipo()
        {
            ddl_tipo.Items.Clear();
            ddl_tipo.Items.Add(new ListItem("------", "0"));
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

        protected void grid_tplan_bind(string programa)
        {
            string Query = "";
            Query = "SELECT tplan_tmate_clave Clave,tmate_desc Nombre, tplan_tarea_clave c_area, tarea_desc Area, tplan_periodo Periodo, tplan_tpees_clave Ciclo, tpees_desc c_ciclo, tplan_consecutivo Consecutivo,  " +
                    " tplan_ttima_clave c_tipo, CASE WHEN tplan_ttima_clave='C' then 'Curricular' ELSE 'Idioma' end Tipo, tplan_estatus c_estatus, CASE WHEN tplan_estatus = 'A' THEN 'ACTIVO' ELSE 'INACTIVO' END estatus, " +
                    " DATE_FORMAT(tplan_date, '%d/%m/%Y') Fecha " +
                    "FROM tplan, tmate, tarea, tpees where tplan_tprog_clave='" + programa + "'" +
                    " and tplan_tmate_clave=tmate_clave and tplan_tarea_clave=tarea_clave and tplan_tpees_clave=tpees_clave " +
                    "ORDER BY Periodo, Consecutivo";
            try
            {
                MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                ConexionMySql.Open();
                MySqlDataAdapter dataadapter = new MySqlDataAdapter(Query, ConexionMySql);
                DataSet ds = new DataSet();
                dataadapter.Fill(ds, "Programas");
                Gridtplan.DataSource = ds;
                Gridtplan.EditIndex = -1;
                Gridtplan.DataBind();
                //Gridtplan.DataMember = "Programas";
                //Gridtplan.HeaderRow.TableSection = TableRowSection.TableHeader;
                //Gridtplan.UseAccessibleHeader = true;
                //Gridtplan.Visible = true;

            }
            catch (Exception ex)
            {
                string test = ex.Message;
            }
        }
        protected bool validar_clave_programa(string clave)
        {
            string Query = "";
            Query = "SELECT COUNT(*) Indicador FROM tprog WHERE tprog_clave='" + clave + "' and tprog_estatus='A' ";
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

        protected bool validar_clave_materia(string clave)
        {
            string Query = "";
            Query = "SELECT COUNT(*) Indicador FROM tmate WHERE tmate_clave='" + clave + "' and tmate_estatus='A' ";
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

        protected bool validar_clave_area(string clave)
        {
            string Query = "";
            Query = "SELECT COUNT(*) Indicador FROM tarea WHERE tarea_clave='" + clave + "' and tarea_estatus='A' ";
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

        protected bool validar_clave_periodo(string clave)
        {
            string Query = "";
            Query = "SELECT COUNT(*) Indicador FROM tpees WHERE tpees_clave='" + clave + "' and tpees_estatus='A' ";
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

        protected void tprog_TextChanged(object sender, EventArgs e)
        {
            if (!validar_clave_programa(txt_tprog.Text))
            {
                string Query = "SELECT DISTINCT tprog_desc Programa FROM tprog WHERE tprog_clave='" + txt_tprog.Text + "'";
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
                var programas = dsmysql.Tables[0].Rows[0][0].ToString();
                txt_nom_prog.Text = dsmysql.Tables[0].Rows[0][0].ToString();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "NoexistePrograma", "NoexistePrograma();", true);


                grid_tplan_bind(txt_tprog.Text);
            }
            else
            {
                txt_nom_prog.Text = null;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "validar_clave", "validarclavePrograma_N('ContentPlaceHolder1_txt_tprog',1);", true);
            }
        }

        protected void tmate_TextChanged(object sender, EventArgs e)
        {
            if (!validar_clave_materia(txt_tmate.Text))
            {
                string Query = "SELECT DISTINCT tmate_desc Programa FROM tmate WHERE tmate_clave='" + txt_tprog.Text + "'";
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
                txt_nom_mate.Text = dsmysql.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                txt_nom_mate.Text = null;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "validar_clave", "validarclavePrograma_N('ContentPlaceHolder1_c_prog_campus',1);", true);
            }
        }

        protected void tarea_TextChanged(object sender, EventArgs e)
        {
            if (!validar_clave_area(txt_tarea.Text))
            {
                string Query = "SELECT DISTINCT tarea_desc Programa FROM tarea WHERE tarea_clave='" + txt_tarea.Text + "'";
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
                txt_nom_area.Text = dsmysql.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                txt_nom_area.Text = null;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "validar_clave", "validarclavePrograma_N('ContentPlaceHolder1_c_prog_campus',1);", true);
            }
        }

        protected void tpees_TextChanged(object sender, EventArgs e)
        {
            if (!validar_clave_periodo(txt_tpees.Text))
            {
                string Query = "SELECT DISTINCT tpees_desc Programa FROM tpees WHERE tpees_clave='" + txt_tpees.Text + "'";
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
                txt_nom_per.Text = dsmysql.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                txt_nom_per.Text = null;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "validar_clave", "validarclavePrograma_N('ContentPlaceHolder1_c_prog_campus',1);", true);
            }
        }


        protected void cancelar_prog_Click(object sender, EventArgs e)
        {
            combo_estatus();
            combo_tipo();
            //txt_tprog.Text = null;
            //txt_nom_prog.Text = null;
            txt_tmate.Text = null;
            txt_nom_mate.Text = null;
            txt_tarea.Text = null;
            txt_nom_area.Text = null;
            txt_tpees.Text = null;
            txt_nom_per.Text = null;
            txt_consecutivo.Text = null;
            txt_periodo.Text = null;
            guardar_prog.Visible = true;
            update_prog.Visible = false;
            //txt_tprog.Attributes.Remove("readonly");
            txt_tmate.Attributes.Remove("readonly");
            //Programa.Visible = true;
            //Materia.Visible = true;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
            //Gridtprog.Visible = false;
        }

        protected void guardar_prog_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txt_tprog.Text) && !String.IsNullOrEmpty(txt_tmate.Text) && !String.IsNullOrEmpty(txt_tarea.Text)
                 && !String.IsNullOrEmpty(txt_tpees.Text) && !String.IsNullOrEmpty(txt_periodo.Text) && ddl_tipo.SelectedValue != "0")
            {
                double vale = 0;
                decimal resultado1 = 0; decimal resultado2 = 0;

                bool consecutivo = Decimal.TryParse(txt_consecutivo.Text, out resultado1);
                bool periodo = Decimal.TryParse(txt_periodo.Text, out resultado2);

                if (consecutivo && periodo)
                {
                    vale = 1;
                    // Si llega hasta aquí, resultado es numérico.
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    if (consecutivo)
                    {
                        //
                    }
                    else
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "valida_consecutivo();", true);
                        grid_tplan_bind(txt_tprog.Text);
                    }
                    if (periodo)
                    {
                        //
                    }
                    else
                    {
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "valida_periodo();", true);
                        grid_tplan_bind(txt_tprog.Text);
                    }
                }
                if (vale == 1)
                {
                    string selected = Request.Form["customSwitches"];
                    string Query = "INSERT INTO tplan Values ('" + txt_tprog.Text + "','" + txt_tarea.Text + "','" + txt_tmate.Text + "','" + txt_tpees.Text + "'," +
                           txt_periodo.Text + "," + txt_consecutivo.Text + ",'" + ddl_tipo.SelectedValue + "',null,'" + ddl_estatus.SelectedValue + "'," +
                           " current_timestamp(), '" + Session["usuario"].ToString() + "')";
                    MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                    ConexionMySql.Open();
                    MySqlCommand mysqlcmd = new MySqlCommand(Query, ConexionMySql);
                    mysqlcmd.CommandType = CommandType.Text;
                    try
                    {
                        mysqlcmd.ExecuteNonQuery();
                        combo_estatus();
                        combo_tipo();
                        txt_tmate.Text = null;
                        txt_nom_mate.Text = null;
                        txt_tarea.Text = null;
                        txt_nom_area.Text = null;
                        txt_tpees.Text = null;
                        txt_nom_per.Text = null;
                        txt_consecutivo.Text = null;
                        txt_periodo.Text = null;

                        grid_tplan_bind(txt_tprog.Text);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                    }
                    catch (Exception ex)
                    {
                        string mensaje_error = ex.Message.Replace("'", "-");
                        Global.inserta_log(mensaje_error, "tplan", Session["usuario"].ToString());
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                    }
                    finally
                    {
                        ConexionMySql.Close();
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "validar", "validar();", true);
            }
        }

        protected void update_prog_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txt_tprog.Text) && !String.IsNullOrEmpty(txt_tmate.Text) && !String.IsNullOrEmpty(txt_tarea.Text)
                 && !String.IsNullOrEmpty(txt_tpees.Text) && !String.IsNullOrEmpty(txt_periodo.Text) && ddl_tipo.SelectedValue != "0")
            {
                double vale = 0;
                decimal resultado1 = 0; decimal resultado2 = 0;

                bool consecutivo = Decimal.TryParse(txt_consecutivo.Text, out resultado1);
                bool periodo = Decimal.TryParse(txt_periodo.Text, out resultado2);

                if (consecutivo && periodo)
                {
                    vale = 1;
                    // Si llega hasta aquí, resultado es numérico.
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    if (consecutivo)
                    {
                        //
                    }
                    else
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "valida_consecutivo();", true);
                        grid_tplan_bind(txt_tprog.Text);
                    }
                    if (periodo)
                    {
                        //
                    }
                    else
                    {
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "valida_periodo();", true);
                        grid_tplan_bind(txt_tprog.Text);
                    }
                }
                if (vale == 1)
                {
                    string selected = Request.Form["customSwitches"];
                    string Query = "UPDATE tplan SET tplan_tarea_clave='" + txt_tarea.Text + "', " +
                        " tplan_tpees_clave='" + txt_tpees.Text + "'," +
                        " tplan_periodo=" + txt_periodo.Text + "," +
                        " tplan_consecutivo=" + txt_consecutivo.Text + "," +
                        " tplan_ttima_clave='" + ddl_tipo.SelectedValue + "'," +
                        " tplan_date= current_timestamp(), tplan_user='" + Session["usuario"].ToString() + "' " +
                        " WHERE tplan_tprog_clave='" + txt_tprog.Text + "' AND tplan_tmate_clave='" + txt_tmate.Text + "'";
                    MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                    ConexionMySql.Open();
                    MySqlCommand mysqlcmd = new MySqlCommand(Query, ConexionMySql);
                    mysqlcmd.CommandType = CommandType.Text;
                    try
                    {
                        mysqlcmd.ExecuteNonQuery();
                        combo_estatus();
                        combo_tipo();
                        txt_tmate.Text = null;
                        txt_nom_mate.Text = null;
                        txt_tarea.Text = null;
                        txt_nom_area.Text = null;
                        txt_tpees.Text = null;
                        txt_nom_per.Text = null;
                        txt_consecutivo.Text = null;
                        txt_periodo.Text = null;
                        grid_tplan_bind(txt_tprog.Text);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "update_p", "update();", true);
                        guardar_prog.Visible = true;
                        update_prog.Visible = false;
                        txt_tprog.Attributes.Remove("readonly");
                        txt_tmate.Attributes.Remove("readonly");
                        //Programa.Visible = true;
                        //Materia.Visible = true;
                    }
                    catch (Exception ex)
                    {
                        string test = ex.Message;
                    }
                    finally
                    {
                        ConexionMySql.Close();
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "validar_campos_campus", "validar_campos_campus();", true);
            }
        }

        protected void Busqueda_Programa(object sender, EventArgs e)
        {
            //if (Gridtprog.Visible == true)
            //{

            //    Gridtprog.Visible = false;
            //}
            //else
            //{
                string strQuery = "";
                strQuery = " select tprog_clave clave, tprog_desc nombre " +
                           " from tprog where tprog_estatus='A' " +
                " order by clave ";

                //resultado.Text = strQuery;
                MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                conexion.Open();
                try
                {
                    MySqlDataAdapter dataadapter = new MySqlDataAdapter(strQuery, conexion);
                    DataSet ds = new DataSet();
                    dataadapter.Fill(ds, "Programa");
                    Gridtprog.DataSource = ds;
                    Gridtprog.DataBind();
                    Gridtprog.DataMember = "Programa";
                    if (Gridtprog.Rows.Count == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "no_existe", "noexist();", true);
                    }
                    else
                    {
                        Gridtprog.HeaderRow.TableSection = TableRowSection.TableHeader;
                        Gridtprog.UseAccessibleHeader = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_Programa", "load_datatable_Programa();", true);
                    }
                }
                catch (Exception ex)
                {
                    //resultado.Text = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tplan", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                }
                //Gridtprog.Visible = true;
                conexion.Close();
            //}
        }

        protected void Busqueda_Materia(object sender, EventArgs e)
        {
            //if (Gridtmate.Visible == true)
            //{

            //    Gridtmate.Visible = false;
            //}
            //else
            //{
            string strQuery = "";
            strQuery = " select tmate_clave clave, tmate_desc nombre " +
                       " from tmate " +
                       " where tmate_clave not in (select tplan_tmate_clave from tplan " +
                       "      where tplan_tprog_clave ='" + txt_tprog.Text + "')";

            strQuery = strQuery + " order by clave ";

            //resultado.Text = strQuery;
            MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            conexion.Open();
            try
            {
                MySqlDataAdapter dataadapter = new MySqlDataAdapter(strQuery, conexion);
                DataSet ds = new DataSet();
                dataadapter.Fill(ds, "Materia");
                Gridtmate.DataSource = ds;
                Gridtmate.DataBind();
                Gridtmate.DataMember = "Materia";
                if (Gridtmate.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "no_existe", "noexist();", true);
                }
                else
                {
                    Gridtmate.HeaderRow.TableSection = TableRowSection.TableHeader;
                    Gridtmate.UseAccessibleHeader = true;
                }
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tplan", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
            }
            //Gridtmate.Visible = true;
            conexion.Close();
            //}
        }

        protected void Busqueda_Area(object sender, EventArgs e)
        {
            if (Gridtarea.Visible == true)
            {

                Gridtarea.Visible = false;
            }
            else
            {
                string strQuery = "";
                strQuery = " select tarea_clave clave, tarea_desc nombre " +
                           " from tarea where tarea_estatus='A' " +
                 " order by clave ";

                //resultado.Text = strQuery;
                MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                conexion.Open();
                try
                {
                    MySqlDataAdapter dataadapter = new MySqlDataAdapter(strQuery, conexion);
                    DataSet ds = new DataSet();
                    dataadapter.Fill(ds, "Programa");
                    Gridtarea.DataSource = ds;
                    Gridtarea.DataBind();
                    //Gridtarea.DataMember = "Programa";
                    //if (Gridtarea.Rows.Count == 0)
                    //{
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "no_existe", "noexist();", true);
                    //}
                    //else
                    //{
                    //    Gridtarea.HeaderRow.TableSection = TableRowSection.TableHeader;
                    //    Gridtarea.UseAccessibleHeader = true;
                    //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_Area", "load_datatable_Area();", true);
                    //}
                }
                catch (Exception ex)
                {
                    //resultado.Text = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tplan", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                }
                //Gridtarea.Visible = true;
                conexion.Close();
            }
        }

        protected void Busqueda_Periodo(object sender, EventArgs e)
        {
            if (Gridtpees.Visible == true)
            {

                Gridtpees.Visible = false;
            }
            else
            {
                string strQuery = "";
                strQuery = " select tpees_clave clave, tpees_desc nombre " +
                           " from tpees where tpees_estatus='A' " +
                 " order by clave ";

                //resultado.Text = strQuery;
                MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                conexion.Open();
                try
                {
                    MySqlDataAdapter dataadapter = new MySqlDataAdapter(strQuery, conexion);
                    DataSet ds = new DataSet();
                    dataadapter.Fill(ds, "Programa");
                    Gridtpees.DataSource = ds;
                    Gridtpees.DataBind();
                    Gridtpees.DataMember = "Programa";
                    if (Gridtpees.Rows.Count == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "no_existe", "noexist();", true);
                    }
                    else
                    {
                        Gridtpees.HeaderRow.TableSection = TableRowSection.TableHeader;
                        Gridtpees.UseAccessibleHeader = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_Periodo", "load_datatable_Periodo();", true);
                    }
                }
                catch (Exception ex)
                {
                    //resultado.Text = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tplan", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                }
                Gridtpees.Visible = true;
                conexion.Close();
            }
        }

        protected void Gridtprog_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = Gridtprog.SelectedRow;
            txt_tprog.Text = row.Cells[1].Text;
            txt_nom_prog.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);
            //Gridtprog.Visible = false;
            grid_tplan_bind(txt_tprog.Text);


        //    materias();
        //    areas();
        //    periodos();

         

        //ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_Materia", "load_datatable_Materia();", true);

        //ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_Area", "load_datatable_Area();", true);
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_Programa", "load_datatable_Programa();", true);

        ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowProgramas", "$('#modalProgramas').modal('hide')", true);

        }

        protected void Gridtmate_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = Gridtmate.SelectedRow;
            txt_tmate.Text = row.Cells[1].Text;
            txt_nom_mate.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);
            //Gridtmate.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowMaterias", "$('#modalMaterias').modal('hide')", true);
        }

        protected void Gridtarea_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = Gridtarea.SelectedRow;
            txt_tarea.Text = row.Cells[1].Text;
            txt_nom_area.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);
            //Gridtarea.Visible = false;
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_Area", "load_datatable_Area();", true);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAreas", "$('#modalAreas').modal('hide')", true);

        }

        protected void Gridtpees_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = Gridtpees.SelectedRow;
            txt_tpees.Text = row.Cells[1].Text;
            txt_nom_per.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);
            //Gridtpees.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPeriodos", "$('#modalPeriodos').modal('hide')", true);

        }

        protected void Gridtplan_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = Gridtplan.SelectedRow;
            txt_tmate.Text = row.Cells[2].Text;
            txt_nom_mate.Text = HttpUtility.HtmlDecode(row.Cells[3].Text);
            txt_tarea.Text = row.Cells[4].Text;
            txt_nom_area.Text = HttpUtility.HtmlDecode(row.Cells[5].Text);

            txt_tpees.Text = row.Cells[6].Text;
            txt_nom_per.Text = HttpUtility.HtmlDecode(row.Cells[7].Text);
            txt_periodo.Text = row.Cells[8].Text;
            txt_consecutivo.Text = row.Cells[1].Text;
            combo_tipo();
            ddl_tipo.SelectedValue = row.Cells[9].Text;
            combo_estatus();
            ddl_estatus.SelectedValue = row.Cells[11].Text;
            Gridtprog.Visible = false;
            txt_tprog.Attributes.Add("readonly", "");
            txt_tmate.Attributes.Add("readonly", "");
            guardar_prog.Visible = false;
            update_prog.Visible = true;
            //Programa.Visible = false;
            //Materia.Visible = false;
            grid_tplan_bind(txt_tprog.Text);
        }

        protected void linkBttnBusca_Click(object sender, EventArgs e)
        {

            try
            {
               
            
                    var programas = serviceCatalogo.obtenProgramas();
                    Gridtprog.DataSource = programas;
                    Gridtprog.DataBind();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowProgramas", "$('#modalProgramas').modal('show')", true);



            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tplan", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
            }
        }

        protected void programas()
        {
            try
            {


                var programas = serviceCatalogo.obtenProgramas();
                Gridtprog.DataSource = programas;
                Gridtprog.DataBind();




            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tplan", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
            }
        }
        protected void linkBttnBuscarMateria_Click(object sender, EventArgs e)
        {
            if (Gridtmate.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "no_existe", "noexist();", true);
            }
            //else
            //{
            //    Gridtmate.HeaderRow.TableSection = TableRowSection.TableHeader;
            //    Gridtmate.UseAccessibleHeader = true;
            //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_Materia", "load_datatable_Materia();", true);
            //}
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowProgramas", "$('#modalMaterias').modal('show')", true);

        }

        protected void materias()
        {
            string strQuery = "";
            strQuery = " select tmate_clave clave, tmate_desc nombre " +
                       " from tmate " +
                       " where tmate_clave not in (select tplan_tmate_clave from tplan " +
                       "      where tplan_tprog_clave ='" + txt_tprog.Text + "')";

            strQuery = strQuery + " order by clave ";

            //resultado.Text = strQuery;
            MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            conexion.Open();
            try
            {
                MySqlDataAdapter dataadapter = new MySqlDataAdapter(strQuery, conexion);
                DataSet ds = new DataSet();
                dataadapter.Fill(ds, "Materia");
                Gridtmate.DataSource = ds;
                Gridtmate.DataBind();
                Gridtmate.DataMember = "Materia";
               


            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tplan", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
            }
            conexion.Close();
        }
        protected void linkBttnBuscaArea_Click(object sender, EventArgs e)
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAreas", "$('#modalAreas').modal('show')", true);

        }
        protected void areas()
        {
            string strQuery = "";
            strQuery = " select tarea_clave clave, tarea_desc nombre " +
                       " from tarea where tarea_estatus='A' " +
             " order by clave ";

            //resultado.Text = strQuery;
            MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            conexion.Open();
            try
            {
                MySqlDataAdapter dataadapter = new MySqlDataAdapter(strQuery, conexion);
                DataSet ds = new DataSet();
                dataadapter.Fill(ds, "Programa");
                Gridtarea.DataSource = ds;
                Gridtarea.DataBind();
                Gridtarea.DataMember = "Programa";
                if (Gridtarea.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "no_existe", "noexist();", true);
                }
                else
                {
                    Gridtarea.HeaderRow.TableSection = TableRowSection.TableHeader;
                    Gridtarea.UseAccessibleHeader = true;
                }


            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tplan", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
            }
            conexion.Close();
        }
        protected void linkBttnBuscaClave_Click(object sender, EventArgs e)
        {

           

        }
        protected void periodos()
        {
            string strQuery = "";
            strQuery = " select tpees_clave clave, tpees_desc nombre " +
                       " from tpees where tpees_estatus='A' " +
             " order by clave ";

            //resultado.Text = strQuery;
            MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            conexion.Open();
            try
            {
                MySqlDataAdapter dataadapter = new MySqlDataAdapter(strQuery, conexion);
                DataSet ds = new DataSet();
                dataadapter.Fill(ds, "Programa");
                Gridtpees.DataSource = ds;
                Gridtpees.DataBind();
                Gridtpees.DataMember = "Programa";
                if (Gridtpees.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "no_existe", "noexist();", true);
                }

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPeriodos", "$('#modalPeriodos').modal('show')", true);

              
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tplan", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
            }
            //Gridtpees.Visible = true;
            conexion.Close();
        }
        protected void Gridtmate_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void Gridtarea_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.TableSection = TableRowSection.TableHeader;
            }
        }
    }
}