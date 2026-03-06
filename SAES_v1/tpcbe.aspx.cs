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
    public partial class tpcbe : System.Web.UI.Page
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
                    combo_campus();
                    combo_nivel("0");
                    combo_cargo();
                    combo_abono();
                }

            }
        }

        private void LlenaPagina()
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "menu", "carga_menu();", true);
            string QerySelect = "select tusme_update, tusme_select from tuser, tusme, tmede " +
                              " where tuser_clave = '" + Session["usuario"].ToString() + "'" +
                              " and tusme_trole_clave = tuser_trole_clave and tusme_tmenu_clave = 6 and tusme_tmenu_clave = tmede_tmenu_clave " +
                              " and tusme_tmede_clave = tmede_clave and tmede_forma='tpcbe' ";

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
                    descuento_btn.Visible = false;
                }
                //grid_cobranza_bind();
                conexion.Close();
            }
            catch (Exception ex)
            {
                ///logs
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcbe", Session["usuario"].ToString());
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
        protected void combo_campus()
        {

            ddl_estatus.Items.Clear();
            ddl_estatus.Items.Add(new ListItem("Activo", "A"));
            ddl_estatus.Items.Add(new ListItem("Inactivo", "B"));

            ddl_tipo.Items.Clear();
            ddl_tipo.Items.Add(new ListItem("------", "0"));
            ddl_tipo.Items.Add(new ListItem("Plan de cobro", "P"));
            ddl_tipo.Items.Add(new ListItem("Beca", "B"));

            ddl_campus.Items.Clear();

            string Query = "SELECT DISTINCT tcamp_clave Clave, tcamp_desc Campus FROM tcamp " +
                            "UNION " +
                            "SELECT DISTINCT '0','--------' Clave  " +
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
                ddl_campus.DataSource = TablaCampus;
                ddl_campus.DataValueField = "Clave";
                ddl_campus.DataTextField = "Campus";
                ddl_campus.DataBind();

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tpcbe", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
            finally
            {
                ConexionMySql.Close();
            }
        }

        protected void combo_nivel(string campus)
        {
            ddl_nivel.Items.Clear();
            string Query = "SELECT DISTINCT tprog_tnive_clave clave, tnive_desc nivel FROM tcapr, tnive, tprog " +
                " WHERE tcapr_estatus='A' ";
            if (campus != "0")
            {
                Query = Query + " AND tcapr_tcamp_clave = '" + campus + "'";
            }
            Query = Query + " AND tcapr_tprog_clave=tprog_clave AND tprog_tnive_clave=tnive_clave  " +
                             "UNION " +
                             "SELECT DISTINCT '0','--------' clave  " +
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
                ddl_nivel.DataSource = TablaCampus;
                ddl_nivel.DataValueField = "Clave";
                ddl_nivel.DataTextField = "Nivel";
                ddl_nivel.DataBind();

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tpcbe", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
            finally
            {
                ConexionMySql.Close();
            }
        }

        protected void combo_cargo()
        {
            ddl_cargo.Items.Clear();
            string Query = " select tcoco_clave clave, tcoco_desc nombre from tcoco where tcoco_estatus='A' and tcoco_tipo='C' and tcoco_categ='CO' " +
                " union " +
                " select '0' clave ,'------' nombre from dual " +
                " order by clave ";
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
                ddl_cargo.DataSource = TablaCampus;
                ddl_cargo.DataValueField = "Clave";
                ddl_cargo.DataTextField = "nombre";
                ddl_cargo.DataBind();

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tpaco", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
            finally
            {
                ConexionMySql.Close();
            }
        }

        protected void combo_abono()
        {
            string Query = "select tcoco_clave clave, tcoco_desc nombre from tcoco where tcoco_estatus='A' and tcoco_tipo='P' and tcoco_categ='BE' " +
                " union " +
                " select '0' clave ,'------' nombre from dual " +
                " order by clave ";
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
                ddl_abono.DataSource = TablaCampus;
                ddl_abono.DataValueField = "Clave";
                ddl_abono.DataTextField = "nombre";
                ddl_abono.DataBind();

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tpcbe", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
            finally
            {
                ConexionMySql.Close();
            }
        }

        private void Carga_Pcbe()
        {
            string strQueryPcbe = " select  tpcbe_tcamp_clave c_campus, tcamp_desc campus, tpcbe_tnive_clave c_nivel, " +
                " tnive_desc nivel, tpcbe_clave descuento, tpcbe_desc nom_descuento, " +
                " tpcbe_tcoco_cargo cargo, tpcbe_tcoco_abono abono, " +
                    " tpcbe_tipo tipo, tpcbe_porcentaje porcentaje, tpcbe_monto monto, tpcbe_estatus estatus, " +
                               " fecha(date_format(tpcbe_date,'%Y-%m-%d')) fecha " +
                              " from tpcbe, tcamp , tnive " +
                              " where tpcbe_tcamp_clave=tcamp_clave and  tpcbe_tnive_clave=tnive_clave ";
            if (ddl_campus.SelectedValue != "0")
            {
                strQueryPcbe = strQueryPcbe + " and tpcbe_tcamp_clave='" + ddl_campus.SelectedValue.ToString() + "' ";
            }
            if (ddl_nivel.SelectedValue != "0")
            {
                strQueryPcbe = strQueryPcbe + " and tpcbe_tnive_clave='" + ddl_nivel.SelectedValue.ToString() + "' ";
            }

            strQueryPcbe = strQueryPcbe + " order by c_campus, c_nivel,descuento  ";

            //resultado.Text = "1--" + strQueryPcbe;

            //Label1.Text = strQueryEsc;
            MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            conexion.Open();
            try
            {
                MySqlDataAdapter dataadapter = new MySqlDataAdapter(strQueryPcbe, conexion);
                DataSet ds = new DataSet();
                dataadapter.Fill(ds, "Beca");
                GridDescuentos.DataSource = ds;
                GridDescuentos.DataBind();
                GridDescuentos.DataMember = "Beca";

                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridDescuentos.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridDescuentos.UseAccessibleHeader = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);
                    GridDescuentos.Visible = true;
                }

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tpcbe", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
            conexion.Close();
        }

        protected void Carga_Pcbe(object sender, EventArgs e)
        {

            Carga_Pcbe();

        }



        protected void GridDescuentos_Click(object sender, EventArgs e)
        {
            GridViewRow row = GridDescuentos.SelectedRow;

            //combo_campus();
            //combo_nivel("0");
            combo_cargo();
            combo_abono();
            txt_desc.Text = row.Cells[5].Text;
            txt_descripcion.Text = HttpUtility.HtmlDecode(row.Cells[6].Text);
            ddl_cargo.SelectedValue = row.Cells[10].Text;
            ddl_abono.SelectedValue = row.Cells[11].Text;
            txt_porcentaje.Text = row.Cells[7].Text;
            txt_monto.Text = row.Cells[8].Text;
            ddl_estatus.SelectedValue = row.Cells[12].Text;
            ddl_tipo.SelectedValue = row.Cells[9].Text;
            guardar_desc.Visible = false;
            actualizar_desc.Visible = true;
            txt_desc.Attributes.Add("readonly", "");
            ddl_campus.Attributes.Add("readonly", "");
            ddl_nivel.Attributes.Add("readonly", "");
        }

        protected bool valida_desc(string tcamp, string tnive, string tcoco)
        {
            string Query = "";
            Query = "SELECT COUNT(*) Indicador FROM tpcbe WHERE tpcbe_tcamp_clave='" + tcamp + "' and tpcbe_tnive_clave='" + tnive + "' and tpcbe_clave='" + txt_desc.Text + "'";
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

        protected void guardar_desc_Click(object sender, EventArgs e)
        {
            int vale = 0;
            if (ddl_campus.SelectedValue != "0" && ddl_tipo.SelectedValue != "0" && ddl_cargo.SelectedValue != "0" && ddl_abono.SelectedValue != "0" && !String.IsNullOrEmpty(txt_desc.Text) && !String.IsNullOrEmpty(txt_descripcion.Text) && !String.IsNullOrEmpty(txt_porcentaje.Text) && !String.IsNullOrEmpty(txt_monto.Text))
            {
                if (valida_desc(ddl_campus.SelectedValue, ddl_nivel.SelectedValue, txt_desc.Text))
                {
                    decimal resultado1 = 0;
                    decimal resultado2 = 0;

                    bool porcen = Decimal.TryParse(txt_porcentaje.Text, out resultado1);
                    bool fijo = Decimal.TryParse(txt_monto.Text, out resultado2);

                    if (porcen && fijo)
                    {
                        vale = 1;
                        // Si llega hasta aquí, resultado es numérico.
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                        if (porcen)
                        {
                            //
                        }
                        else
                        {

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "valida_descuento();", true);
                        }
                        if (fijo)
                        {
                            //
                        }
                        else
                        {
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "valida_monto();", true);
                        }
                    }

                    if (vale == 1)
                    {

                        string strCadSQL = "INSERT INTO tpcbe Values ('" + txt_desc.Text + "','" + txt_descripcion.Text + "','" + ddl_campus.SelectedValue.ToString() + "','" +
                        ddl_nivel.SelectedValue.ToString() + "','" + ddl_tipo.SelectedValue +
                        "'," + txt_porcentaje.Text + "," + txt_monto.Text + ",'" + ddl_cargo.SelectedValue + "','" + ddl_abono.SelectedValue + "','" +
                        Session["usuario"].ToString() + "',  current_timestamp ,'" + ddl_estatus.SelectedValue + "')";

                        MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                        conexion.Open();
                        try
                        {
                            MySqlCommand myCommandinserta = new MySqlCommand(strCadSQL, conexion);
                            //Ejecucion del comando en el servidor de BD
                            myCommandinserta.ExecuteNonQuery();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "save", "save();", true);
                        }
                        catch (Exception ex)
                        {
                            string test = ex.Message;
                            string mensaje_error = ex.Message.Replace("'", "-");
                            Global.inserta_log(mensaje_error, "tpcbe", Session["usuario"].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                        }
                        conexion.Close();
                        //combo_campus();
                        //combo_nivel("0");
                        combo_cargo();
                        combo_abono();
                        txt_desc.Text = "";
                        txt_descripcion.Text = "";
                        txt_porcentaje.Text = "";
                        txt_monto.Text = "";
                        Carga_Pcbe();
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "validardesc", "validardesc('ContentPlaceHolder1_txt_desc',1);", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "validar_campos_descuento", "validar_campos_descuento();", true);
            }

        }

        protected void actualizar_desc_Click(object sender, EventArgs e)
        {
            int vale = 0;
            if (ddl_campus.SelectedValue != "0" && ddl_tipo.SelectedValue != "0" && ddl_cargo.SelectedValue != "0" && ddl_abono.SelectedValue != "0" && !String.IsNullOrEmpty(txt_desc.Text) && !String.IsNullOrEmpty(txt_descripcion.Text) && !String.IsNullOrEmpty(txt_porcentaje.Text) && !String.IsNullOrEmpty(txt_monto.Text))
            {
                decimal resultado1 = 0;
                decimal resultado2 = 0;

                bool porcen = Decimal.TryParse(txt_porcentaje.Text, out resultado1);
                bool fijo = Decimal.TryParse(txt_monto.Text, out resultado2);

                if (porcen && fijo)
                {
                    vale = 1;
                    // Si llega hasta aquí, resultado es numérico.
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    if (porcen)
                    {
                        //
                    }
                    else
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "valida_descuento();", true);
                    }
                    if (fijo)
                    {
                        //
                    }
                    else
                    {
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "valida_monto();", true);
                    }
                }
                if (vale == 1)
                {
                    string Query = "UPDATE tpcbe SET tpcbe_desc='" + txt_descripcion.Text + "', tpcbe_tipo='" + ddl_tipo.SelectedValue + "', " +
                        " tpcbe_porcentaje='" + txt_porcentaje.Text + "',tpcbe_monto='" + txt_monto.Text + "', tpcbe_tcoco_cargo='" + ddl_cargo.SelectedValue + "', " +
                        " tpcbe_tcoco_abono='" + ddl_abono.SelectedValue + "', tpcbe_user_clave='" + Session["usuario"].ToString() + "',tpcbe_date= current_timestamp(), tpcbe_estatus='" + ddl_estatus.SelectedValue + "'" +
                        " WHERE tpcbe_clave='" + txt_desc.Text + "' AND tpcbe_tcamp_clave='" + ddl_campus.SelectedValue + "' AND tpcbe_tnive_clave='" + ddl_nivel.SelectedValue + "' ";
                    MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                    ConexionMySql.Open();
                    MySqlCommand mysqlcmd = new MySqlCommand(Query, ConexionMySql);
                    mysqlcmd.CommandType = CommandType.Text;
                    try
                    {
                        mysqlcmd.ExecuteNonQuery();
                        //combo_campus();
                        //combo_nivel("0");
                        combo_cargo();
                        combo_abono();
                        txt_desc.Text = "";
                        txt_descripcion.Text = "";
                        txt_porcentaje.Text = "";
                        txt_monto.Text = "";
                        Carga_Pcbe();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "update_p", "update();", true);
                        guardar_desc.Visible = true;
                        actualizar_desc.Visible = false;
                        txt_desc.Attributes.Remove("readonly");
                        ddl_campus.Attributes.Remove("readonly");
                        ddl_nivel.Attributes.Remove("readonly");
                    }
                    catch (Exception ex)
                    {
                        string test = ex.Message;
                        string mensaje_error = ex.Message.Replace("'", "-");
                        Global.inserta_log(mensaje_error, "tpcbe", Session["usuario"].ToString());
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                    }
                    finally
                    {
                        ConexionMySql.Close();
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "validar_campos_descuento", "validar_campos_descuento();", true);
            }
        }

        protected void cancelar_desc_Click(object sender, EventArgs e)
        {
            combo_campus();
            combo_nivel("0");
            combo_cargo();
            combo_abono();
            txt_desc.Text = "";
            txt_descripcion.Text = "";
            txt_porcentaje.Text = "";
            txt_monto.Text = "";
            GridDescuentos.Visible = false;
        }
    }
}