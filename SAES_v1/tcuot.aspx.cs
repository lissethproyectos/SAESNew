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
    public partial class tcuot : System.Web.UI.Page
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

                ScriptManager.RegisterStartupScript(this, this.GetType(), "ctrl_fecha_i", "ctrl_fecha_i();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ctrl_fecha_f", "ctrl_fecha_f();", true);

                if (!IsPostBack)
                {
                    LlenaPagina();
                    combo_estatus();
                }

            }
        }

        private void LlenaPagina()
        {
            System.Threading.Thread.Sleep(50);

            string QerySelect = "select tusme_update, tusme_select from tuser, tusme, tmede " +
                              " where tuser_clave = '" + Session["usuario"].ToString() + "'" +
                              " and tusme_trole_clave = tuser_trole_clave and tusme_tmenu_clave = 6 and tusme_tmenu_clave = tmede_tmenu_clave " +
                              " and tusme_tmede_clave = tmede_clave and tmede_forma='tcuot' ";

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
                if (dssql1.Tables[0].Rows.Count == 0 || dssql1.Tables[0].Rows[0][1].ToString() == "0")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                }
                else
                {
                    if (dssql1.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        btn_periodo.Visible = false;
                    }
                    //grid_conceptos_bind();
                }

            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcuot", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);

            }
            conexion.Close();

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

            MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            conexion.Open();
            try
            {
                string strQuerycampus = "";
                strQuerycampus = "select tcamp_clave clave, tcamp_desc campus from tcamp " +
                                " where tcamp_estatus='A' " +
                                 " union " +
                                 " select '000' clave, '---------' campus from dual " +
                                 " order by clave ";


                DataTable TablaCampus = new DataTable();
                MySqlCommand ConsultaMySql0 = new MySqlCommand();
                MySqlDataReader DatosMySql0;
                ConsultaMySql0.Connection = conexion;
                ConsultaMySql0.CommandType = CommandType.Text;
                ConsultaMySql0.CommandText = strQuerycampus;
                DatosMySql0 = ConsultaMySql0.ExecuteReader();
                TablaCampus.Load(DatosMySql0, LoadOption.OverwriteChanges);

                ddl_campus.DataSource = TablaCampus;
                ddl_campus.DataValueField = "clave";
                ddl_campus.DataTextField = "campus";
                ddl_campus.DataBind();

                string strQuerynivel = "";
                strQuerynivel = "select tnive_clave clave, tnive_desc nivel from tnive " +
                                " where tnive_estatus='A' " +
                                 " union " +
                                 " select '000' clave, '---------' nivel from dual " +
                                 " order by clave ";

                DataTable TablaNivel = new DataTable();
                MySqlCommand ConsultaMySql = new MySqlCommand();
                MySqlDataReader DatosMySql;
                ConsultaMySql.Connection = conexion;
                ConsultaMySql.CommandType = CommandType.Text;
                ConsultaMySql.CommandText = strQuerynivel;
                DatosMySql = ConsultaMySql.ExecuteReader();
                TablaNivel.Load(DatosMySql, LoadOption.OverwriteChanges);

                ddl_nivel.DataSource = TablaNivel;
                ddl_nivel.DataValueField = "clave";
                ddl_nivel.DataTextField = "nivel";
                ddl_nivel.DataBind();

                string strQuerycole = "";
                strQuerycole = "select tcole_clave clave, tcole_desc colegio from tcole " +
                                " where tcole_estatus='A' " +
                                 " union " +
                                 " select '000' clave, '---------' colegio from dual " +
                                 " order by clave ";

                DataTable TablaCole = new DataTable();
                MySqlCommand ConsultaMySql1 = new MySqlCommand();
                MySqlDataReader DatosMySql1;
                ConsultaMySql1.Connection = conexion;
                ConsultaMySql1.CommandType = CommandType.Text;
                ConsultaMySql1.CommandText = strQuerycole;
                DatosMySql1 = ConsultaMySql1.ExecuteReader();
                TablaCole.Load(DatosMySql1, LoadOption.OverwriteChanges);

                ddl_colegio.DataSource = TablaCole;
                ddl_colegio.DataValueField = "clave";
                ddl_colegio.DataTextField = "colegio";
                ddl_colegio.DataBind();

                string strQuerymoda = "select tmoda_clave clave, tmoda_desc modalidad from tmoda " +
                                " where tmoda_estatus='A' " +
                                 " union " +
                                 " select '000' clave, '---------' modalidad from dual " +
                                 " order by clave ";

                DataTable TablaModa = new DataTable();
                MySqlCommand ConsultaMySql2 = new MySqlCommand();
                MySqlDataReader DatosMySql2;
                ConsultaMySql2.Connection = conexion;
                ConsultaMySql2.CommandType = CommandType.Text;
                ConsultaMySql2.CommandText = strQuerymoda;
                DatosMySql2 = ConsultaMySql2.ExecuteReader();
                TablaModa.Load(DatosMySql2, LoadOption.OverwriteChanges);

                ddl_modalidad.DataSource = TablaModa;
                ddl_modalidad.DataValueField = "clave";
                ddl_modalidad.DataTextField = "modalidad";
                ddl_modalidad.DataBind();

                string strQueryprog = "";
                strQueryprog = "select tprog_clave clave, tprog_desc programa from  tprog " +
                                " where tprog_estatus='A' " +
                                 " union " +
                                 " select '0000000000' clave, '---------' programa from dual " +
                                 " order by clave ";


                DataTable TablaPrograma = new DataTable();
                MySqlCommand ConsultaMySql21 = new MySqlCommand();
                MySqlDataReader DatosMySql21;
                ConsultaMySql2.Connection = conexion;
                ConsultaMySql2.CommandType = CommandType.Text;
                ConsultaMySql2.CommandText = strQueryprog;
                DatosMySql21 = ConsultaMySql2.ExecuteReader();
                TablaPrograma.Load(DatosMySql21, LoadOption.OverwriteChanges);

                ddl_programa.DataSource = TablaPrograma;
                ddl_programa.DataValueField = "clave";
                ddl_programa.DataTextField = "programa";
                ddl_programa.DataBind();

                string strQueryingreso = "";
                strQueryingreso = "select  ttiin_clave clave, ttiin_desc ingreso from ttiin " +
                                " where ttiin_estatus='A' " +
                                 " union " +
                                 " select '000' clave, '---------' ingreso from dual " +
                                 " order by clave ";


                DataTable TablaIngreso = new DataTable();
                MySqlCommand ConsultaMySql22 = new MySqlCommand();
                MySqlDataReader DatosMySql22;
                ConsultaMySql2.Connection = conexion;
                ConsultaMySql2.CommandType = CommandType.Text;
                ConsultaMySql2.CommandText = strQueryingreso;
                DatosMySql22 = ConsultaMySql2.ExecuteReader();
                TablaIngreso.Load(DatosMySql22, LoadOption.OverwriteChanges);

                ddl_tipo.DataSource = TablaIngreso;
                ddl_tipo.DataValueField = "clave";
                ddl_tipo.DataTextField = "ingreso";
                ddl_tipo.DataBind();
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcuot", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }

            conexion.Close();

        }

        protected void Carga_Nivel(object sender, EventArgs e)
        {
            string strQuerynivel = "";
            if (ddl_campus.SelectedValue.ToString() != "000")
            {

                strQuerynivel = "select distinct tprog_tnive_clave clave, tnive_desc nivel from tcapr, tnive, tprog " +
                                " where tcapr_estatus='A' and tcapr_tcamp_clave='" + ddl_campus.SelectedValue.ToString() + "'" +
                                " and tcapr_tprog_clave=tprog_clave and tprog_tnive_clave=tnive_clave " +
                                 " union " +
                                 " select '000' clave, '---------' nivel from dual " +
                                 " order by clave ";
            }
            else
            {
                strQuerynivel = "select tnive_clave clave, tnive_desc nivel from tnive " +
                            " where tnive_estatus='A' " +
                             " union " +
                             " select '000' clave, '---------' nivel from dual " +
                             " order by clave ";
            }

            MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            conexion.Open();
            try
            {
                DataTable TablaNivel = new DataTable();
                MySqlCommand ConsultaMySql = new MySqlCommand();
                MySqlDataReader DatosMySql;
                ConsultaMySql.Connection = conexion;
                ConsultaMySql.CommandType = CommandType.Text;
                ConsultaMySql.CommandText = strQuerynivel;
                DatosMySql = ConsultaMySql.ExecuteReader();
                TablaNivel.Load(DatosMySql, LoadOption.OverwriteChanges);

                ddl_nivel.DataSource = TablaNivel;
                ddl_nivel.DataValueField = "clave";
                ddl_nivel.DataTextField = "nivel";
                ddl_nivel.DataBind();

                string strQuerycole = "";
                if (ddl_campus.SelectedValue.ToString() != "000")
                {
                    strQuerycole = "select distinct tprog_tcole_clave clave, tcole_desc colegio from tcapr, tcole, tprog " +
                                    " where tcapr_estatus='A' and tcapr_tcamp_clave='" + ddl_campus.SelectedValue.ToString() + "'" +
                                    " and tcapr_tprog_clave=tprog_clave and tprog_tcole_clave=tcole_clave " +
                                     " union " +
                                     " select '000' clave, '---------' colegio from dual " +
                                     " order by clave ";
                }
                else
                {
                    strQuerycole = "select tcole_clave clave, tcole_desc colegio from tcole " +
                                    " where tcole_estatus='A' " +
                                     " union " +
                                     " select '000' clave, '---------' colegio from dual " +
                                     " order by clave ";
                }
                DataTable TablaCole = new DataTable();
                MySqlCommand ConsultaMySql1 = new MySqlCommand();
                MySqlDataReader DatosMySql1;
                ConsultaMySql1.Connection = conexion;
                ConsultaMySql1.CommandType = CommandType.Text;
                ConsultaMySql1.CommandText = strQuerycole;
                DatosMySql1 = ConsultaMySql1.ExecuteReader();
                TablaCole.Load(DatosMySql1, LoadOption.OverwriteChanges);

                ddl_colegio.DataSource = TablaCole;
                ddl_colegio.DataValueField = "clave";
                ddl_colegio.DataTextField = "colegio";
                ddl_colegio.DataBind();

                string strQuerymoda = "";
                if (ddl_campus.SelectedValue.ToString() != "000")
                {
                    strQuerymoda = "select distinct tprog_tmoda_clave clave, tmoda_desc modalidad from tcapr, tmoda, tprog " +
                                " where tcapr_estatus='A' and tcapr_tcamp_clave='" + ddl_campus.SelectedValue.ToString() + "'" +
                                " and tcapr_tprog_clave=tprog_clave and tprog_tmoda_clave=tmoda_clave " +
                                 " union " +
                                 " select '000' clave, '---------' modalidad from dual " +
                                 " order by clave ";
                }
                else
                {
                    strQuerymoda = "select tmoda_clave clave, tmoda_desc modalidad from tmoda " +
                                " where tmoda_estatus='A' " +
                                 " union " +
                                 " select '000' clave, '---------' modalidad from dual " +
                                 " order by clave ";
                }


                DataTable TablaModa = new DataTable();
                MySqlCommand ConsultaMySql2 = new MySqlCommand();
                MySqlDataReader DatosMySql2;
                ConsultaMySql2.Connection = conexion;
                ConsultaMySql2.CommandType = CommandType.Text;
                ConsultaMySql2.CommandText = strQuerymoda;
                DatosMySql2 = ConsultaMySql2.ExecuteReader();
                TablaModa.Load(DatosMySql2, LoadOption.OverwriteChanges);

                ddl_modalidad.DataSource = TablaModa;
                ddl_modalidad.DataValueField = "clave";
                ddl_modalidad.DataTextField = "modalidad";
                ddl_modalidad.DataBind();


                string strQueryprog = "";

                if (ddl_campus.SelectedValue.ToString() != "000")
                {
                    strQueryprog = "select distinct tprog_clave clave, tprog_desc programa from tcapr, tprog " +
                                " where tcapr_estatus='A' and tcapr_tcamp_clave='" + ddl_campus.SelectedValue.ToString() + "'" +
                                " and tcapr_tprog_clave=tprog_clave " +
                                 " union " +
                                 " select '0000000000' clave, '---------' programa from dual " +
                                 " order by clave ";
                }
                else
                {
                    strQueryprog = "select tprog_clave clave, tprog_desc programa from  tprog " +
                                " where tprog_estatus='A' " +
                                 " union " +
                                 " select '0000000000' clave, '---------' programa from dual " +
                                 " order by clave ";
                }


                DataTable TablaPrograma = new DataTable();
                MySqlCommand ConsultaMySql20 = new MySqlCommand();
                MySqlDataReader DatosMySql20;
                ConsultaMySql2.Connection = conexion;
                ConsultaMySql2.CommandType = CommandType.Text;
                ConsultaMySql2.CommandText = strQueryprog;
                DatosMySql20 = ConsultaMySql2.ExecuteReader();
                TablaPrograma.Load(DatosMySql20, LoadOption.OverwriteChanges);

                ddl_programa.DataSource = TablaPrograma;
                ddl_programa.DataValueField = "clave";
                ddl_programa.DataTextField = "programa";
                ddl_programa.DataBind();
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcuot", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
            conexion.Close();
        }

        protected void Query_click(object sender, EventArgs e)
        {
            grid_conceptos_bind();
        }

        protected void grid_conceptos_bind()
        {
            string strQuery = "";
            strQuery = strQuery = " select tcuot_tcoco_clave clave, tcoco_desc nombre, tcuot_importe importe, date_format(tcuot_inicio, '%d-%m-%Y')  inicio, date_format(tcuot_fin, '%d-%m-%Y') fin, tcuot_tcamp_clave campus, " +
                   " tcuot_tnive_clave nivel, tcuot_tcole_clave colegio, tcuot_tmoda_clave moda, tcuot_tprog_clave prog, tcuot_ttiin_clave tiin, " +
                   " fecha(date_format(tcuot_date, '%Y-%m-%d')) fecha, tcuot_estatus estatus from tcuot, tcoco where 1=1 ";

            //strQuery = strQuery + " where (tcuot_tcoco_clave like '%" + TxtBusqueda.Text + "%' or tcoco_desc like '%" + TxtBusqueda.Text + "%') ";
            if (txt_concepto.Text != "")
            {
                strQuery = strQuery + " and tcuot_tcoco_clave='" + txt_concepto.Text + "'";
            }
            if (ddl_campus.SelectedValue.ToString() != "" && ddl_campus.SelectedValue.ToString() != "000")
            {
                strQuery = strQuery + " and tcuot_tcamp_clave='" + ddl_campus.SelectedValue.ToString() + "'";
            }
            if (ddl_nivel.SelectedValue.ToString() != "" && ddl_nivel.SelectedValue.ToString() != "000")
            {
                strQuery = strQuery + " and tcuot_tnive_clave='" + ddl_nivel.SelectedValue.ToString() + "'";
            }
            if (ddl_colegio.SelectedValue.ToString() != "" && ddl_colegio.SelectedValue.ToString() != "000")
            {
                strQuery = strQuery + " and tcuot_tcole_clave='" + ddl_colegio.SelectedValue.ToString() + "'";
            }
            if (ddl_modalidad.SelectedValue.ToString() != "" && ddl_modalidad.SelectedValue.ToString() != "000")
            {
                strQuery = strQuery + " and tcuot_tmoda_clave='" + ddl_modalidad.SelectedValue.ToString() + "'";
            }
            if (ddl_programa.SelectedValue.ToString() != "" && ddl_programa.SelectedValue.ToString() != "0000000000")
            {
                strQuery = strQuery + " and tcuot_tprog_clave='" + ddl_programa.SelectedValue.ToString() + "'";
            }
            if (ddl_tipo.SelectedValue.ToString() != "" && ddl_tipo.SelectedValue.ToString() != "000")
            {
                strQuery = strQuery + " and tcuot_ttiin_clave='" + ddl_tipo.SelectedValue.ToString() + "'";
            }
            if (txt_fecha_i.Text != "")
            {
                strQuery = strQuery + " and tcuot_inicio=STR_TO_DATE('" + txt_fecha_i.Text + "','%d/%m/%Y') ";
            }
            if (txt_fecha_f.Text != "")
            {
                strQuery = strQuery + " and tcuot_fin=STR_TO_DATE('" + txt_fecha_f.Text + "','%d/%m/%Y') ";
            }
            if (txt_importe.Text != "")
            {
                strQuery = strQuery + " and tcuot_importe='" + txt_importe.Text + "'";
            }
            string vigente = "";
            string selected = Request.Form["customSwitches1"];
            if (selected == "on") { vigente = "S"; } else { vigente = "N"; }

            if (vigente == "S")
            {
                strQuery = strQuery + " and curdate() between tcuot_inicio and tcuot_fin ";
            }

            strQuery = strQuery + " and tcuot_estatus='" + ddl_estatus.SelectedValue + "' and tcuot_tcoco_clave=tcoco_clave order by clave ";
            MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            conexion.Open();
            try
            {
                MySqlDataAdapter dataadapter = new MySqlDataAdapter(strQuery, conexion);
                DataSet ds = new DataSet();
                dataadapter.Fill(ds, "Conceptos");
                GridConcepto.DataSource = ds;
                GridConcepto.DataBind();
                if (GridConcepto.Rows.Count > 0)
                {
                    GridConcepto.DataMember = "Conceptos";
                    GridConcepto.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridConcepto.UseAccessibleHeader = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);
                }
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tpees", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);

            }
            conexion.Close();
        }

        protected void grid_codigos_bind(object sender, EventArgs e)
        {
            if (GridCodigos.Visible == true)
            {
                GridCodigos.Visible = false;
            }
            else
            {
                string QueryEstudiantes = "select tcoco_clave clave , tcoco_desc nombre " +
                                            " from tcoco " +
                                            " where tcoco_tipo='C' and tcoco_categ in ('IN','CO','SV','TI') " +
                                            " order by tcoco_clave";
                MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                conexion.Open();
                try
                {
                    MySqlDataAdapter dataadapter = new MySqlDataAdapter(QueryEstudiantes, conexion);
                    DataSet ds = new DataSet();
                    dataadapter.Fill(ds, "Codigos");
                    GridCodigos.DataSource = ds;
                    GridCodigos.DataBind();
                    GridCodigos.DataMember = "Codigos";
                    GridCodigos.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridCodigos.UseAccessibleHeader = true;

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_Codigos", "load_datatable_Codigos();", true);
                    GridCodigos.Visible = true;
                }
                catch (Exception ex)
                {
                    //logs
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tcuot", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                }
                conexion.Close();
            }
        }

        protected void Gridcodigos_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridCodigos.SelectedRow;
            txt_concepto.Text = row.Cells[1].Text;
            txt_nombre.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);
            GridCodigos.Visible = false;
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            txt_concepto.Text = null;
            txt_nombre.Text = null;
            txt_importe.Text = null;
            txt_fecha_i.Text = null;
            txt_fecha_f.Text = null;
            combo_estatus();
            btn_save.Visible = true;
            //grid_conceptos_bind();
            Cuotas.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {


            if (!String.IsNullOrEmpty(txt_concepto.Text) && !String.IsNullOrEmpty(txt_importe.Text) && !String.IsNullOrEmpty(txt_fecha_i.Text) && !String.IsNullOrEmpty(txt_fecha_f.Text))
            {
                double vale = 0;
                decimal resultado1 = 0;
                bool importe = Decimal.TryParse(txt_importe.Text, out resultado1);

                if (importe)
                {
                    vale = 1;
                    // Si llega hasta aquí, resultado es numérico.
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    if (importe)
                    {
                        //
                    }
                    else
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "valida_creditos();", true);
                    }
                }
                if (vale == 1)
                {
                    string fecha_i_string = txt_fecha_i.Text;
                    string fecha_f_string = txt_fecha_f.Text;
                    string format = "dd/MM/yyyy";

                    DateTime fecha_inicio = DateTime.ParseExact(fecha_i_string, format, CultureInfo.InvariantCulture);
                    DateTime fecha_fin = DateTime.ParseExact(fecha_f_string, format, CultureInfo.InvariantCulture);

                    if (fecha_inicio < fecha_fin)
                    {
                        string traslape = "select tcuot_tcoco_clave clave, tcoco_desc concepto,tcuot_importe importe, fecha(date_format(tcuot_inicio, '%Y-%m-%d')) inicio, " +
                           " fecha(date_format(tcuot_fin, '%Y-%m-%d')) fin, tcuot_tcamp_clave campus, tcuot_tnive_clave nivel, tcuot_tcole_clave colegio, " +
                           " tcuot_tmoda_clave moda, tcuot_tprog_clave prog, tcuot_ttiin_clave tiin, fecha(date_format(tcuot_date, '%Y-%m-%d')) fecha, " +
                           " tcuot_estatus estatus " +
                           " from tcuot, tcoco " +
                           " where tcuot_tcoco_clave = '" + txt_concepto.Text + "'" +
                           " and tcuot_tcamp_clave = '" + ddl_campus.SelectedValue + "'" +
                           " and tcuot_tnive_clave = '" + ddl_nivel.SelectedValue + "'" +
                           " and tcuot_tcole_clave = '" + ddl_colegio.SelectedValue + "'" +
                           " and tcuot_tmoda_clave = '" + ddl_modalidad.SelectedValue + "'" +
                           " and tcuot_tprog_clave = '" + ddl_programa.SelectedValue + "'" +
                           " and tcuot_ttiin_clave = '" + ddl_tipo.SelectedValue + "'" +
                           " and(STR_TO_DATE('" + txt_fecha_i.Text + "', ' %d/%m/%Y') between tcuot_inicio and tcuot_fin or " +
                           "    STR_TO_DATE('" + txt_fecha_f.Text + "', '%d/%m/%Y') between tcuot_inicio and tcuot_fin) " +
                           " and (STR_TO_DATE('" + txt_fecha_i.Text + "', ' %d/%m/%Y') != tcuot_inicio or STR_TO_DATE('" + txt_fecha_f.Text + "', ' %d/%m/%Y') != tcuot_fin) " +
                           " and tcuot_tcoco_clave = tcoco_clave ";
                        MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                        conexion.Open();
                        try
                        {
                            MySqlDataAdapter dataadapter = new MySqlDataAdapter(traslape, conexion);
                            DataSet ds = new DataSet();
                            dataadapter.Fill(ds, "Cuotas");
                            Cuotas.DataSource = ds;
                            Cuotas.DataBind();
                            Cuotas.DataMember = "Cuotas";
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                //resultado.Text = "Existen cuota(s) vigentes";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Traslape", "Traslape();", true);
                                Cuotas.Visible = true;
                            }

                            else
                            {
                                string strBorra = "DELETE from tcuot where tcuot_tcoco_clave='" + txt_concepto.Text + "'" +
                                    " and tcuot_tcamp_clave ='" + ddl_campus.SelectedValue + "'" +
                                    " and tcuot_tnive_clave ='" + ddl_nivel.SelectedValue + "'" +
                                    " and tcuot_tcole_clave ='" + ddl_colegio.SelectedValue + "'" +
                                    " and tcuot_tmoda_clave ='" + ddl_modalidad.SelectedValue + "'" +
                                    " and tcuot_tprog_clave ='" + ddl_programa.SelectedValue + "'" +
                                    " and tcuot_ttiin_clave ='" + ddl_tipo.SelectedValue + "'" +
                                    " and tcuot_inicio=STR_TO_DATE('" + txt_fecha_i.Text + "','%d/%m/%Y') " +
                                    " and tcuot_fin=STR_TO_DATE('" + txt_fecha_f.Text + "','%d/%m/%Y') ";

                                string strCadSQL = "INSERT INTO tcuot Values ('" + txt_concepto.Text + "'," + txt_importe.Text + "," +
                                    " STR_TO_DATE('" + txt_fecha_i.Text + "', '%d/%m/%Y'), STR_TO_DATE('" + txt_fecha_f.Text + "', '%d/%m/%Y') ,'" +
                                     ddl_campus.SelectedValue + "','" + ddl_nivel.SelectedValue + "','" + ddl_colegio.SelectedValue + "','" + ddl_modalidad.SelectedValue + "','" +
                                     ddl_programa.SelectedValue + "','" + ddl_tipo.SelectedValue + "','" + Session["usuario"].ToString() + "',current_timestamp(), '" + ddl_estatus.SelectedValue + "')";

                                MySqlCommand myCommandborra = new MySqlCommand(strBorra, conexion);
                                myCommandborra.ExecuteNonQuery();

                                MySqlCommand myCommandinserta = new MySqlCommand(strCadSQL, conexion);
                                //Ejecucion del comando en el servidor de BD
                                myCommandinserta.ExecuteNonQuery();
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "save", "save();", true);
                                txt_concepto.Text = "";
                                txt_importe.Text = "";
                                txt_nombre.Text = "";
                                txt_fecha_i.Text = "";
                                txt_fecha_f.Text = "";
                                combo_estatus();

                            }


                        }
                        catch (Exception ex)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                            string mensaje_error = ex.Message.Replace("'", "-");
                            Global.inserta_log(mensaje_error, "tcuot", Session["usuario"].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                        }

                    }
                    else
                    {
                        grid_conceptos_bind();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validarFechas('ContentPlaceHolder1_txt_fecha_i','ContentPlaceHolder1_txt_fecha_f');", true);
                    }
                }

            }
            else
            {
                grid_conceptos_bind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_cuotas();", true);
            }


        }


        protected void GridConcepto_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridConcepto.SelectedRow;
            txt_concepto.Text = row.Cells[1].Text;
            txt_nombre.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);
            txt_importe.Text = row.Cells[3].Text;
            txt_fecha_i.Text = row.Cells[4].Text;
            txt_fecha_f.Text = row.Cells[5].Text;
            combo_estatus();
            ddl_campus.SelectedValue = row.Cells[6].Text;
            ddl_nivel.SelectedValue = row.Cells[7].Text;
            ddl_colegio.SelectedValue = row.Cells[8].Text;
            ddl_modalidad.SelectedValue = row.Cells[9].Text;
            ddl_programa.SelectedValue = row.Cells[10].Text;
            ddl_tipo.SelectedValue = row.Cells[11].Text;
            ddl_estatus.SelectedValue = row.Cells[12].Text;
            grid_conceptos_bind();
        }
    }
}