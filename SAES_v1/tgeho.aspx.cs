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
    public partial class tgeho : System.Web.UI.Page
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

                LlenaPagina();
                if (!IsPostBack)
                {
                    combo_periodo();
                    combo_campus();
                    combo_turno();
                    combo_periodo_destino();

                    if (Global.campus != "")
                    {
                        ddl_campus.SelectedValue = Global.campus;
                        ddl_turno.SelectedValue = Global.turno;
                        ddl_periodo.SelectedValue = Global.periodo;

                    }
                }
            }
        }
        private void LlenaPagina()
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "menu", "carga_menu();", true);

            string QerySelect = "select tusme_update, tusme_select from tuser, tusme, tmede " +
                              " where tuser_clave = '" + Session["usuario"].ToString() + "'" +
                              " and tusme_trole_clave = tuser_trole_clave and tusme_tmenu_clave = 5 and tusme_tmenu_clave = tmede_tmenu_clave " +
                              " and tusme_tmede_clave = tmede_clave and tmede_forma='tgeho' ";

            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            ConexionMySql.Open();
            try
            {
                MySqlDataAdapter sqladapter = new MySqlDataAdapter();

                DataSet dssql1 = new DataSet();

                MySqlCommand commandsql1 = new MySqlCommand(QerySelect, ConexionMySql);
                sqladapter.SelectCommand = commandsql1;
                sqladapter.Fill(dssql1);
                sqladapter.Dispose();
                commandsql1.Dispose();
                if (dssql1.Tables[0].Rows.Count == 0 || dssql1.Tables[0].Rows[0][1].ToString() == "0")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                }
                if (dssql1.Tables[0].Rows[0][0].ToString() == "0")
                {
                    btn_tgeho.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ///logs
            }

        }

        protected void combo_turno()
        {
            ddl_turno.Items.Clear();
            ddl_turno.Items.Add(new ListItem("-----", "0"));
            ddl_turno.Items.Add(new ListItem("Matutino", "M"));
            ddl_turno.Items.Add(new ListItem("Vespertino", "V"));
            ddl_turno.Items.Add(new ListItem("Nocturno", "N"));
        }

        protected void combo_periodo()
        {
            string Query = "select tpees_clave clave, tpees_desc periodo from tpees " +
                        " where tpees_estatus='A' and tpees_fin >= curdate() " +
                         " union " +
                         " select '0' clave, '---------' periodo from dual " +
                         " order by clave ";
            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            ConexionMySql.Open();
            DataTable TablaEstado = new DataTable();
            MySqlCommand ConsultaMySql = new MySqlCommand();
            MySqlDataReader DatosMySql;
            try
            {
                ConsultaMySql.Connection = ConexionMySql;
                ConsultaMySql.CommandType = CommandType.Text;
                ConsultaMySql.CommandText = Query;
                DatosMySql = ConsultaMySql.ExecuteReader();
                TablaEstado.Load(DatosMySql, LoadOption.OverwriteChanges);

                ddl_periodo.DataSource = TablaEstado;
                ddl_periodo.DataValueField = "Clave";
                ddl_periodo.DataTextField = "periodo";
                ddl_periodo.DataBind();

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
        protected void combo_campus()
        {
            string Query = "SELECT tcamp_clave Clave, tcamp_desc Nombre FROM tcamp WHERE TCAMP_ESTATUS='A' " +
                            " UNION " +
                            "SELECT '0' Clave,'-------' Nombre " +
                            "ORDER BY 1";
            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            ConexionMySql.Open();
            DataTable TablaEstado = new DataTable();
            MySqlCommand ConsultaMySql = new MySqlCommand();
            MySqlDataReader DatosMySql;
            try
            {
                ConsultaMySql.Connection = ConexionMySql;
                ConsultaMySql.CommandType = CommandType.Text;
                ConsultaMySql.CommandText = Query;
                DatosMySql = ConsultaMySql.ExecuteReader();
                TablaEstado.Load(DatosMySql, LoadOption.OverwriteChanges);

                ddl_campus.DataSource = TablaEstado;
                ddl_campus.DataValueField = "Clave";
                ddl_campus.DataTextField = "Nombre";
                ddl_campus.DataBind();

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

        protected void Carga_Nivel(object sender, EventArgs e)
        {
            string strQuerynivel = "";
            if (ddl_campus.SelectedValue.ToString() != "0")
            {

                strQuerynivel = "select distinct tprog_tnive_clave clave, tnive_desc nivel from tcapr, tnive, tprog " +
                                " where tcapr_estatus='A' and tcapr_tcamp_clave='" + ddl_campus.SelectedValue.ToString() + "'" +
                                " and tcapr_tprog_clave=tprog_clave and tprog_tnive_clave=tnive_clave " +
                                 " union " +
                                 " select '0' clave, '---------' nivel from dual " +
                                 " order by clave ";
            }
            else
            {
                strQuerynivel = "select tnive_clave clave, tnive_desc nivel from tnive " +
                            " where tnive_estatus='A' " +
                             " union " +
                             " select '0' clave, '---------' nivel from dual " +
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

                string strQueryprog = "";

                if (ddl_campus.SelectedValue.ToString() != "0")
                {
                    strQueryprog = "select distinct tprog_clave clave, tprog_desc programa from tcapr, tprog " +
                                " where tcapr_estatus='A' and tcapr_tcamp_clave='" + ddl_campus.SelectedValue.ToString() + "'" +
                                " and tcapr_tprog_clave=tprog_clave " +
                                 " union " +
                                 " select '0' clave, '---------' programa from dual " +
                                 " order by clave ";
                }
                else
                {
                    strQueryprog = "select tprog_clave clave, tprog_desc programa from  tprog " +
                                " where tprog_estatus='A' " +
                                 " union " +
                                 " select '0' clave, '---------' programa from dual " +
                                 " order by clave ";
                }


                DataTable TablaPrograma = new DataTable();
                MySqlCommand ConsultaMySql20 = new MySqlCommand();
                MySqlDataReader DatosMySql20;
                ConsultaMySql20.CommandType = CommandType.Text;
                ConsultaMySql20.CommandText = strQueryprog;
                DatosMySql20 = ConsultaMySql20.ExecuteReader();
                TablaPrograma.Load(DatosMySql20, LoadOption.OverwriteChanges);

                ddl_programa.DataSource = TablaPrograma;
                ddl_programa.DataValueField = "clave";
                ddl_programa.DataTextField = "programa";
                ddl_programa.DataBind();

                conexion.Close();
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
            }
        }

        protected void Carga_Programa(object sender, EventArgs e)
        {
            string strQueryprog = "";
            if (ddl_nivel.SelectedValue.ToString() != "0")
            {

                strQueryprog = "select distinct tprog_clave clave, tprog_desc programa from tcapr, tnive, tprog " +
                                " where tcapr_estatus='A' and tcapr_tcamp_clave='" + ddl_campus.SelectedValue.ToString() + "'" +
                                " and   tprog_tnive_clave='" + ddl_nivel.SelectedValue + "'" +
                                " and tcapr_tprog_clave=tprog_clave and tprog_tnive_clave=tnive_clave " +
                                 " union " +
                                 " select '0' clave, '---------' programa from dual " +
                                 " order by clave ";
            }
            

            MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            conexion.Open();

            try
            {
                DataTable TablaPrograma = new DataTable();
                MySqlCommand ConsultaMySql = new MySqlCommand();
                MySqlDataReader DatosMySql;
                ConsultaMySql.Connection = conexion;
                ConsultaMySql.CommandType = CommandType.Text;
                ConsultaMySql.CommandText = strQueryprog;
                DatosMySql = ConsultaMySql.ExecuteReader();
                TablaPrograma.Load(DatosMySql, LoadOption.OverwriteChanges);

                ddl_programa.DataSource = TablaPrograma;
                ddl_programa.DataValueField = "clave";
                ddl_programa.DataTextField = "programa";
                ddl_programa.DataBind();
                conexion.Close();
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
            }
        }

        protected void combo_periodo_destino()
        {
            string Query = "select tpees_clave clave, tpees_desc periodo from tpees " +
                        " where tpees_estatus='A' and tpees_inicio >= curdate() " +
                         " union " +
                         " select '0' clave, '---------' periodo from dual " +
                         " order by clave ";
            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            ConexionMySql.Open();
            DataTable TablaEstado = new DataTable();
            MySqlCommand ConsultaMySql = new MySqlCommand();
            MySqlDataReader DatosMySql;
            try
            {
                ConsultaMySql.Connection = ConexionMySql;
                ConsultaMySql.CommandType = CommandType.Text;
                ConsultaMySql.CommandText = Query;
                DatosMySql = ConsultaMySql.ExecuteReader();
                TablaEstado.Load(DatosMySql, LoadOption.OverwriteChanges);

                ddl_periodo_destino.DataSource = TablaEstado;
                ddl_periodo_destino.DataValueField = "Clave";
                ddl_periodo_destino.DataTextField = "periodo";
                ddl_periodo_destino.DataBind();

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

        protected void save_tgeho_Click(object sender, EventArgs e)
        {
            //
        }

        protected void Procesar_Click(object sender, EventArgs e)
        {

            if (ddl_periodo.SelectedValue != "0" && ddl_turno.SelectedValue != "0" && ddl_campus.SelectedValue != "0" && ddl_periodo_destino.SelectedValue != "0")
            {

                string QerySelect = " select count(*) from tgeho, tplan where tgeho_tpees_clave='" + ddl_periodo_destino.SelectedValue + "' and tgeho_turno='" + ddl_turno.SelectedValue + "'" +
                    " and tgeho_tmate_clave=tplan_tmate_clave ";
                if (ddl_campus.SelectedValue.ToString() != "0")
                {
                    QerySelect = QerySelect + " and  tgeho_tcamp_clave='" + ddl_campus.SelectedValue.ToString() + "'";
                }
                if (ddl_nivel.SelectedValue.ToString() != "0")
                {
                    QerySelect = QerySelect + " and  tgeho_tnive_clave='" + ddl_nivel.SelectedValue.ToString() + "'";
                }
                if (ddl_programa.SelectedValue.ToString() != "0")
                {
                    QerySelect = QerySelect + " and  tplan_tprog_clave='" + ddl_programa.SelectedValue.ToString() + "'";
                }

                //resultado.Text = QerySelect;

                MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                conexion.Open();

                MySqlDataAdapter sqladapter = new MySqlDataAdapter();

                DataSet dssql2 = new DataSet();

                MySqlCommand commandsql2 = new MySqlCommand(QerySelect, conexion);
                sqladapter.SelectCommand = commandsql2;
                sqladapter.Fill(dssql2);
                sqladapter.Dispose();
                commandsql2.Dispose();

                //if (dssql2.Tables[0].Rows[0][0].ToString() == "0")
                //{
                    Global.decision = "1";
                    string strQuery = "";
                    strQuery = " select distinct ttira_tpers_num, ttira_tprog_clave, testu_tcamp_clave, tprog_tnive_clave, " +
                      " (select  ttama_materias from ttama where ttama_ttasa_clave = testu_ttasa_clave and ttama_ttima_clave = 'C') curr, " +
                      " (select  ttama_materias from ttama where ttama_ttasa_clave = testu_ttasa_clave and ttama_ttima_clave = 'I') idio " +
                      " from ttira, testu, tprog " +
                      " where ttira_tpees_clave = '" + ddl_periodo.SelectedValue + "' and ttira_tmast_clave = 'IN'   " +
                      " and testu_tpers_num = ttira_tpers_num and testu_tpees_clave = ttira_tpees_clave " +
                      " and testu_tprog_clave=tprog_clave ";

                    if (ddl_campus.SelectedValue.ToString() != "0")
                    {
                        strQuery = strQuery + " and  ttira_tcamp_clave='" + ddl_campus.SelectedValue.ToString() + "'";
                    }
                    if (ddl_nivel.SelectedValue.ToString() != "0")
                    {
                        strQuery = strQuery + " and  tprog_tnive_clave='" + ddl_nivel.SelectedValue.ToString() + "'";
                    }
                    if (ddl_programa.SelectedValue.ToString() != "0")
                    {
                        strQuery = strQuery + " and  ttira_tprog_clave='" + ddl_programa.SelectedValue.ToString() + "'";
                    }

                    try
                    {
                        MySqlDataAdapter dataadapter = new MySqlDataAdapter(strQuery, conexion);
                        DataSet ds = new DataSet();
                        dataadapter.Fill(ds, "GridInscritos");
                        GridInscritos.DataSource = ds;
                        GridInscritos.DataBind();
                        GridInscritos.DataMember = "GridInscritos";

                        //resultado.Text ="Inscritos:" + ds.Tables[0].Rows.Count;
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            string strBorra = " delete from ttir1 where ttir1_tpees_clave='" + ddl_periodo_destino.SelectedValue + "'";
                            MySqlCommand myCommandborra = new MySqlCommand(strBorra, conexion);
                            myCommandborra.ExecuteNonQuery();

                            double tpers_num, curr, idio;
                            string prog, campus, nivel;
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                tpers_num = Convert.ToDouble(GridInscritos.Rows[i].Cells[0].Text.ToString());
                                prog = GridInscritos.Rows[i].Cells[1].Text.ToString();
                                campus = GridInscritos.Rows[i].Cells[2].Text.ToString();
                                nivel = GridInscritos.Rows[i].Cells[3].Text.ToString();
                                curr = Convert.ToDouble(GridInscritos.Rows[i].Cells[4].Text.ToString());
                                idio = Convert.ToDouble(GridInscritos.Rows[i].Cells[5].Text.ToString());
                                // resultado.Text = resultado.Text + "-->" + GridInscritos.Rows[i].Cells[0].Text.ToString() + "curr:" + curr;

                                if (curr > 0)
                                {
                                    string StrMaterias = "select tplan_periodo, tplan_consecutivo,tplan_tmate_clave,tplan_ttima_clave from tplan " +
                                      " where tplan_tprog_clave = '" + prog + "'" +
                                      " and tplan_tmate_clave not in (select tkard_tmate_clave from tkard , tcali " +
                                      "               where tkard_tpers_num =" + tpers_num + " and tkard_tprog_clave = tplan_tprog_clave " +
                                      "               and tkard_tmate_clave = tplan_tmate_clave and tcali_clave = tkard_tcali_clave and tcali_ind_aprob = 'S') " +
                                      " and tplan_tmate_clave not in (select ttira_tmate_clave from ttira " +
                                      "         where ttira_tpers_num = " + tpers_num + " and ttira_tprog_clave = '" + prog + "' and ttira_tpees_clave = '" +
                                                ddl_periodo.SelectedValue + "') " +
                                      " and tplan_tmate_clave not in (select tpred_tmate_clave from tpred " +
                                      "         where tpred_tpers_num = " + tpers_num + " and tpred_tprog_clave = '" + prog + "' and tpred_estatus = 'A') " +
                                      " and tplan_ttima_clave = 'C' " +
                                      " order by tplan_periodo, tplan_consecutivo ";

                                    //resultado.Text = resultado.Text + StrMaterias;

                                    DataSet dssql1 = new DataSet();

                                    MySqlCommand commandsql1 = new MySqlCommand(StrMaterias, conexion);
                                    sqladapter.SelectCommand = commandsql1;
                                    sqladapter.Fill(dssql1);
                                    sqladapter.Dispose();
                                    commandsql1.Dispose();

                                    if (dssql1.Tables[0].Rows.Count > 0)
                                    {
                                        double mate = 0;
                                        if (dssql1.Tables[0].Rows.Count >= curr)
                                        {
                                            mate = curr;
                                        }
                                        else
                                        {
                                            mate = dssql1.Tables[0].Rows.Count;
                                        }

                                        for (int w = 0; w < mate; w++)
                                        {
                                            //resultado.Text = resultado.Text + "C=" + dssql1.Tables[0].Rows[w][2].ToString();
                                            string strCadSQL = "INSERT INTO ttir1 Values (" + tpers_num + ",'" + ddl_periodo_destino.SelectedValue + "','" + campus + "','" +
                                                nivel + "','" + prog + "','" + dssql1.Tables[0].Rows[w][2].ToString() + "','" +
                                                Session["usuario"].ToString() + "',current_timestamp())";
                                            //resultado.Text = strBorra + "---" + strCadSQL;
                                            try
                                            {


                                                MySqlCommand myCommandinserta = new MySqlCommand(strCadSQL, conexion);
                                                //Ejecucion del comando en el servidor de BD
                                                myCommandinserta.ExecuteNonQuery();
                                            }
                                            catch (Exception ex)
                                            {
                                                //resultado.Text = ex.Message;
                                            }

                                        }

                                    }
                                }

                                if (idio > 0)
                                {
                                    string StrMaterias = "select tplan_periodo, tplan_consecutivo,tplan_tmate_clave,tplan_ttima_clave from tplan " +
                                      " where tplan_tprog_clave = '" + prog + "'" +
                                      " and tplan_tmate_clave not in (select tkard_tmate_clave from tkard , tcali " +
                                      "               where tkard_tpers_num =" + tpers_num + " and tkard_tprog_clave = tplan_tprog_clave " +
                                      "               and tkard_tmate_clave = tplan_tmate_clave and tcali_clave = tkard_tcali_clave and tcali_ind_aprob = 'S') " +
                                      " and tplan_tmate_clave not in (select ttira_tmate_clave from ttira " +
                                      "         where ttira_tpers_num = " + tpers_num + " and ttira_tprog_clave = '" + prog + "' and ttira_tpees_clave = '" +
                                                ddl_periodo.SelectedValue + "') " +
                                      " and tplan_tmate_clave not in (select tpred_tmate_clave from tpred " +
                                      "         where tpred_tpers_num = " + tpers_num + " and tpred_tprog_clave = '" + prog + "' and tpred_estatus = 'A') " +
                                      " and tplan_ttima_clave = 'I' " +
                                      " order by tplan_periodo, tplan_consecutivo ";

                                    DataSet dssql1 = new DataSet();

                                    MySqlCommand commandsql1 = new MySqlCommand(StrMaterias, conexion);
                                    sqladapter.SelectCommand = commandsql1;
                                    sqladapter.Fill(dssql1);
                                    sqladapter.Dispose();
                                    commandsql1.Dispose();

                                    if (dssql1.Tables[0].Rows.Count > 0)
                                    {
                                        double mate = 0;
                                        if (dssql1.Tables[0].Rows.Count >= idio)
                                        {
                                            mate = idio;
                                        }
                                        else
                                        {
                                            mate = dssql1.Tables[0].Rows.Count;
                                        }

                                        for (int w = 0; w < mate; w++)
                                        {
                                            //resultado.Text = resultado.Text + "I=" + dssql1.Tables[0].Rows[w][2].ToString();
                                            string strCadSQL = "INSERT INTO ttir1 Values (" + tpers_num + ",'" + ddl_periodo_destino.SelectedValue + "','" + campus + "','" +
                                                nivel + "','" + prog + "','" + dssql1.Tables[0].Rows[w][2].ToString() + "','" +
                                                Session["usuario"].ToString() + "',curdate())";
                                            //resultado.Text = strBorra + "---" + strCadSQL;
                                            try
                                            {


                                                MySqlCommand myCommandinserta = new MySqlCommand(strCadSQL, conexion);
                                                //Ejecucion del comando en el servidor de BD
                                                myCommandinserta.ExecuteNonQuery();
                                            }
                                            catch (Exception ex)
                                            {
                                                //resultado.Text = ex.Message;
                                            }

                                        }

                                    }

                                }

                            }
                            DGeho();
                        }
                    }
                    catch (Exception ex)
                    {
                        //resultado.Text = ex.Message;
                    }
                /*}
                else
                {
                    Global.decision = "0";
                }*/
                conexion.Close();

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_tgeho();", true);
            }
        }

        protected void DGeho(object sender, EventArgs e)
        {
            DGeho();
        }

        private void DGeho()
        {

            string turno = "";
            string strQueryProy = "";

            if (Global.decision == "0")
            {
                strQueryProy = " select distinct tgeho_tcamp_clave campus, tgeho_tnive_clave nivel, " +
                        " tgeho_tmate_clave clave_materia, tmate_desc materia,  " +
                        " tgeho_no_alumno total, " +
                        " (SELECT group_concat( distinct ttir1_tprog_clave separator '| ') FROM ttir1 " +
                        "   where ttir1_tmate_clave = tmate_clave) Programas" +
                        " from  tgeho, tmate, tplan " +
                        " where tgeho_tmate_clave = tmate_clave  and tgeho_tpees_clave='" + ddl_periodo_destino.SelectedValue + "' and tgeho_turno='" + turno + "'" +
                        " and    tgeho_tmate_clave=tplan_tmate_clave ";

                if (ddl_campus.SelectedValue.ToString() != "0")
                {
                    strQueryProy = strQueryProy + " and  tgeho_tcamp_clave='" + ddl_campus.SelectedValue.ToString() + "'";
                }
                if (ddl_nivel.SelectedValue.ToString() != "0")
                {
                    strQueryProy = strQueryProy + " and  tgeho_tnive_clave='" + ddl_nivel.SelectedValue.ToString() + "'";
                }
                if (ddl_programa.SelectedValue.ToString() != "0")
                {
                    strQueryProy = strQueryProy + " and  tplan_tprog_clave='" + ddl_programa.SelectedValue.ToString() + "'";
                }

                strQueryProy = strQueryProy + " order by tgeho_tcamp_clave,  tgeho_tmate_clave ";
            }
            else
            {
                strQueryProy = " select ttir1_tcamp_clave campus, ttir1_tnive_clave nivel, " +
                        " ttir1_tmate_clave clave_materia, tmate_desc materia,  " +
                        " count(*) total, " +
                        " (SELECT group_concat( distinct ttir1_tprog_clave separator '| ') FROM ttir1 " +
                        "   where ttir1_tmate_clave = tmate_clave) Programas" +
                        " from  ttir1, tprog, tmate, tplan " +
                        " where ttir1_tprog_clave = tprog_clave and ttir1_tmate_clave = tmate_clave " +
                        " and tplan_tprog_clave = ttir1_tprog_clave and tplan_tmate_clave = ttir1_tmate_clave ";
                if (ddl_campus.SelectedValue.ToString() != "0")
                {
                    strQueryProy = strQueryProy + " and  ttir1_tcamp_clave='" + ddl_campus.SelectedValue.ToString() + "'";
                }
                if (ddl_nivel.SelectedValue.ToString() != "0")
                {
                    strQueryProy = strQueryProy + " and  ttir1_tnive_clave='" + ddl_nivel.SelectedValue.ToString() + "'";
                }
                if (ddl_programa.SelectedValue.ToString() != "0")
                {
                    strQueryProy = strQueryProy + " and  tplan_tprog_clave='" + ddl_programa.SelectedValue.ToString() + "'";
                }
                strQueryProy = strQueryProy + " group by ttir1_tcamp_clave, ttir1_tnive_clave, ttir1_tmate_clave, " +
                        " tmate_desc " +
                " order by ttir1_tcamp_clave, ttir1_tnive_clave, ttir1_tmate_clave ";
            }
            //resultado.Text = strQueryProy;
            MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            conexion.Open();
            try
            {

                MySqlDataAdapter adaptergeho = new MySqlDataAdapter(strQueryProy, conexion);
                DataSet dsgeho = new DataSet();
                adaptergeho.Fill(dsgeho, "Geho");
                Gridtgeho.DataSource = dsgeho;
                Gridtgeho.DataBind();
                Gridtgeho.DataMember = "Geho";
                /*Gridtgeho.HeaderRow.TableSection = TableRowSection.TableHeader;
                Gridtgeho.UseAccessibleHeader = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_tgeho", "load_datatable_tgeho();", true);
                */
                if (dsgeho.Tables[0].Rows.Count > 0)
                {
                    Gridtgeho.Visible = true;

                    // Se registran en tgeho por primera vez
                    if (Global.decision == "1")
                    {
                        /*for (int i = 0; i < dsgeho.Tables[0].Rows.Count; i++)
                        {
                            //TextBox tot = (TextBox)Geho.Rows[i].FindControl("total");

                            //tot.Text = dsgeho.Tables[0].Rows[i+indice][4].ToString();

                            string strQDel = " delete from tgeho where tgeho_tpees_clave='" + ddl_periodo_destino.SelectedValue + "' and tgeho_tcamp_clave='" +
                                 dsgeho.Tables[0].Rows[i][0].ToString() + "' and tgeho_tnive_clave='" + dsgeho.Tables[0].Rows[i][1].ToString() + "' " +
                                 " and tgeho_tmate_clave='" + dsgeho.Tables[0].Rows[i][2].ToString() + "' and tgeho_turno='" + turno + "'";
                            MySqlCommand myCommandborra = new MySqlCommand(strQDel, conexion);
                            //Ejecucion del comando en el servidor de BD
                            myCommandborra.ExecuteNonQuery();

                            string StrQIns = " insert into tgeho values ('" + ddl_periodo_destino.SelectedValue + "','" + dsgeho.Tables[0].Rows[i][0].ToString() + "','" +
                                 dsgeho.Tables[0].Rows[i][1].ToString() + "','" + dsgeho.Tables[0].Rows[i][2].ToString() + "','" + turno + "'," +
                                dsgeho.Tables[0].Rows[i][4].ToString() + ",'" + Session["usuario"].ToString() + "',current_timestamp())";
                            MySqlCommand myCommandinserta = new MySqlCommand(StrQIns, conexion);
                            //Ejecucion del comando en el servidor de BD
                            myCommandinserta.ExecuteNonQuery();

                        }*/
                        for (int i = 0; i < Gridtgeho.Rows.Count; i++)
                        {
                            TextBox alumnos = (TextBox)Gridtgeho.Rows[i].FindControl("total");

                            alumnos.Text = dsgeho.Tables[0].Rows[i][4].ToString();

                        }
                    }
                }
                conexion.Close();
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
            }

        }

        protected void Agregar_Click (object sender, EventArgs e)
        {
            if (ddl_periodo.SelectedValue != "0" && ddl_turno.SelectedValue != "0" && ddl_campus.SelectedValue != "0" && ddl_periodo_destino.SelectedValue != "0")
            {
                //
                MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                conexion.Open();
                //try
                //{
                    
                    string strInscritos = " select case when sum(tgrup_enroll) is null then 0 else sum(tgrup_enroll) end inscritos " +
                        " from tgrup, tplan, tprog " +
                        " where tgrup_tpees_clave='" + ddl_periodo_destino.SelectedValue + "' and tgrup_turno='" + ddl_turno.SelectedValue + "' and tgrup_origen='C' " +
                        " and   tplan_tmate_clave=tgrup_tmate_clave and tplan_tprog_clave=tprog_clave ";
                    if (ddl_campus.SelectedValue.ToString() != "0")
                    {
                        strInscritos = strInscritos + " and tgrup_tcamp_clave='" + ddl_campus.SelectedValue.ToString() + "' ";
                    }
                    if (ddl_nivel.SelectedValue.ToString() != "0")
                    {
                        strInscritos = strInscritos + " and tprog_tnive_clave='" + ddl_nivel.SelectedValue.ToString() + "' ";
                    }

                    MySqlDataAdapter sqladapter = new MySqlDataAdapter();
                    DataSet dsins = new DataSet();

                    MySqlCommand commandins = new MySqlCommand(strInscritos, conexion);
                    sqladapter.SelectCommand = commandins;
                    sqladapter.Fill(dsins);
                    sqladapter.Dispose();
                    commandins.Dispose();
                    //resultado.Text = strInscritos;"Inscritos:" + dsins.Tables[0].Rows[0][0].ToString();
                    //if (dsins.Tables[0].Rows[0][0].ToString() == "0")  // Realiza proceso sólo si no hay alumnos inscritos en la selección
                    //{
                        for (int i = 0; i < Gridtgeho.Rows.Count; i++)
                        {
                        //TextBox tot = (TextBox)Geho.Rows[i].FindControl("total");
                            

                            //tot.Text = dsgeho.Tables[0].Rows[i+indice][4].ToString();

                            string strQDel = " delete from tgeho where tgeho_tpees_clave='" + ddl_periodo_destino.SelectedValue + "' and tgeho_tcamp_clave='" +
                                 ddl_campus.SelectedValue + "' and tgeho_tnive_clave='" + Gridtgeho.Rows[i].Cells[1].Text.ToString() + "' " +
                                 " and tgeho_tmate_clave='" + Gridtgeho.Rows[i].Cells[2].Text.ToString() + "' and tgeho_turno='" + ddl_turno.SelectedValue + "'";
                            MySqlCommand myCommandborra = new MySqlCommand(strQDel, conexion);
                            //Ejecucion del comando en el servidor de BD
                            myCommandborra.ExecuteNonQuery();
                        //TextBox alumnos = (TextBox)Gridtgeho.Rows[i].FindControl("total");
                        TextBox alumnos = (TextBox)Gridtgeho.Rows[i].FindControl("total");
                        double pron = 0;
                        if (alumnos.Text != "")
                        {
                            pron = Convert.ToDouble(alumnos.Text);
                        }

                        string StrQIns = 
                            " insert into tgeho values ('" + ddl_periodo_destino.SelectedValue + "','" + ddl_campus.SelectedValue + "','" +
                                 Gridtgeho.Rows[i].Cells[1].Text.ToString() + "','" + Gridtgeho.Rows[i].Cells[2].Text.ToString() + "','" + ddl_turno.SelectedValue + "'," +
                                pron + ",'" + Session["usuario"].ToString() + "',current_timestamp())";
                            MySqlCommand myCommandinserta = new MySqlCommand(StrQIns, conexion);
                            //Ejecucion del comando en el servidor de BD
                            myCommandinserta.ExecuteNonQuery();

                        }
                        //Obtiene la información de la tabla tgeho
                        string StrQtgeho = " select distinct tgeho_tcamp_clave campus, tgeho_tnive_clave nivel, " +
                        " tgeho_tmate_clave clave_materia, tmate_desc materia,  " +
                        " tgeho_no_alumno total, " +
                        " (SELECT group_concat( distinct ttir1_tprog_clave separator '| ') FROM ttir1 " +
                        "   where ttir1_tmate_clave = tmate_clave) Programas" +
                        " from  tgeho, tmate, tplan " +
                        " where tgeho_tmate_clave = tmate_clave  and tgeho_tpees_clave='" + ddl_periodo_destino.SelectedValue + "' and tgeho_turno='" + ddl_turno.SelectedValue + "'" +
                        " and   tgeho_tmate_clave=tplan_tmate_clave ";

                        if (ddl_campus.SelectedValue.ToString() != "0")
                        {
                            StrQtgeho = StrQtgeho + " and  tgeho_tcamp_clave='" + ddl_campus.SelectedValue.ToString() + "'";
                        }
                        if (ddl_nivel.SelectedValue.ToString() != "0")
                        {
                            StrQtgeho = StrQtgeho + " and  tgeho_tnive_clave='" + ddl_nivel.SelectedValue.ToString() + "'";
                        }
                        if (ddl_programa.SelectedValue.ToString() != "0")
                        {
                            StrQtgeho = StrQtgeho + " and  tplan_tprog_clave='" + ddl_programa.SelectedValue.ToString() + "'";
                        }

                        DataSet dsgeho = new DataSet();

                        MySqlCommand commandgeho = new MySqlCommand(StrQtgeho, conexion);
                        sqladapter.SelectCommand = commandgeho;
                        sqladapter.Fill(dsgeho);
                        sqladapter.Dispose();
                        commandgeho.Dispose();


                        for (int i = 0; i < dsgeho.Tables[0].Rows.Count; i++)
                        {
                            string strBorraHora = " delete from tgrup " +
                            " where tgrup_tcamp_clave='" + dsgeho.Tables[0].Rows[i][0].ToString() + "' " +
                            " and   tgrup_tpees_clave='" + ddl_periodo_destino.SelectedValue + "' " +
                            " and   tgrup_turno='" + ddl_turno.SelectedValue + "' and tgrup_origen='C' and tgrup_tmate_clave='" + dsgeho.Tables[0].Rows[i][2].ToString() + "'";
                            MySqlCommand myCommandborra = new MySqlCommand(strBorraHora, conexion);

                            // resultado.Text = resultado.Text + "Procesando:" + dsgeho.Tables[0].Rows[i][2].ToString();

                            //Ejecucion del comando en el servidor de BD
                            myCommandborra.ExecuteNonQuery();
                            //TextBox tot = (TextBox)Geho.Rows[i].FindControl("total");
                            double pron = Convert.ToDouble(dsgeho.Tables[0].Rows[i][4].ToString());

                            string QerySelect = " select  teo, prac, campo, minimo, maximo, " +
                               " case when(" + pron + "/ maximo) - truncate((" + pron + "/ maximo), 0) = 0 then " +
                               "      truncate((" + pron + " / maximo), 0) else truncate((" + pron + " / maximo), 0) + 1 end grupos " +
                               " from(select  tmate_hr_teo teo, tmate_hr_prac prac, tmate_hr_campo campo, tmate_min_cupo minimo, tmate_max_cupo maximo " +
                               "   from tmate  Where tmate_clave = '" + dsgeho.Tables[0].Rows[i][2].ToString() + "') x ";
                            /*if (Geho.Rows[i].Cells[3].Text.ToString() == "COM421")
                            {
                                resultado.Text = resultado.Text + "--" + QerySelect;
                            }*/

                            DataSet dssql1 = new DataSet();

                            MySqlCommand commandsql1 = new MySqlCommand(QerySelect, conexion);
                            sqladapter.SelectCommand = commandsql1;
                            sqladapter.Fill(dssql1);
                            sqladapter.Dispose();
                            commandsql1.Dispose();
                            string materia = "";
                            decimal teo = 0, prac = 0, campo = 0;

                            materia = dsgeho.Tables[0].Rows[i][2].ToString();
                            teo = Convert.ToDecimal(dssql1.Tables[0].Rows[0][0].ToString());
                            prac = Convert.ToDecimal(dssql1.Tables[0].Rows[0][1].ToString());
                            campo = Convert.ToDecimal(dssql1.Tables[0].Rows[0][2].ToString());

                            double total, minimo, maximo, grupos;
                            total = Convert.ToDouble(dsgeho.Tables[0].Rows[i][4].ToString());
                            minimo = Convert.ToDouble(dssql1.Tables[0].Rows[0][3].ToString());
                            maximo = Convert.ToDouble(dssql1.Tables[0].Rows[0][4].ToString());
                            grupos = Convert.ToDouble(dssql1.Tables[0].Rows[0][5].ToString());
                            /*if (Geho.Rows[i].Cells[3].Text.ToString() == "COM421")
                            {
                                resultado.Text = resultado.Text + " -- materia:" + materia + " teo:" + teo + " prac:" + prac + " campo:" + campo + " total:" + total + " minimo:" + minimo + " maximo:" + maximo + " grupos:" + grupos;
                            }*/
                            if (total > 1) //minimo) // Si la suma de pronostico de la materia es mayor al mínimo para abrir un grupo
                            {
                                string seccion = "";
                                for (int w = 1; w <= grupos; w++)
                                {
                                    string QueryGrupo = " select case when substr(max(tgrup_clave),2,2) is null then 0 else substr(max(tgrup_clave),2,2) end  grupo from tgrup " +
                                           " where tgrup_tpees_clave = '" + ddl_periodo_destino.SelectedValue + "' and tgrup_tmate_clave = '" + materia + "'";
                                    DataSet dsgp = new DataSet();

                                    MySqlCommand commandsqlgp = new MySqlCommand(QueryGrupo, conexion);
                                    sqladapter.SelectCommand = commandsqlgp;
                                    sqladapter.Fill(dsgp);
                                    sqladapter.Dispose();

                                    commandsqlgp.Dispose();
                                    double consecutivo = Convert.ToDouble(dsgp.Tables[0].Rows[0][0].ToString());
                                    /*if (Geho.Rows[i].Cells[3].Text.ToString() == "COM421")
                                    {
                                        resultado.Text = resultado.Text + "--" + QueryGrupo  + ":" + consecutivo;
                                    }*/
                                    if (teo > 0)
                                    {
                                        consecutivo = consecutivo + 1;
                                        if (consecutivo < 10)
                                        {
                                            seccion = ddl_turno.SelectedValue + "0" + consecutivo + "T";
                                        }
                                        else
                                        {
                                            seccion = ddl_turno.SelectedValue + consecutivo + "T";

                                        }
                                        string StrInsertaGrupo = " insert into tgrup values('" + ddl_periodo_destino.SelectedValue + "','" +
                                              dsgeho.Tables[0].Rows[i][0].ToString() + "','" + materia + "','" + seccion + "','" + ddl_turno.SelectedValue + "',null," +
                                               maximo + ",0,'A','" + Session["usuario"].ToString() + "',current_timestamp(),'C') ";
                                        MySqlCommand myCommandinserta = new MySqlCommand(StrInsertaGrupo, conexion);
                                        /*if (Geho.Rows[i].Cells[3].Text.ToString() == "COM421")
                                        {
                                            resultado.Text = resultado.Text + "<--->" + StrInsertaGrupo;
                                        }*/
                                        //Ejecucion del comando en el servidor de BD
                                        myCommandinserta.ExecuteNonQuery();
                                    }
                                    if (prac > 0)
                                    {
                                        if (consecutivo < 10)
                                        {
                                            seccion = ddl_turno.SelectedValue + "0" + consecutivo + "P";
                                        }
                                        else
                                        {
                                            seccion = ddl_turno.SelectedValue + consecutivo + "P";

                                        }
                                        string StrInsertaGrupo = " insert into tgrup values('" + ddl_periodo_destino.SelectedValue + "','" +
                                               dsgeho.Tables[0].Rows[i][0].ToString() + "','" + materia + "','" + seccion + "','" + ddl_turno.SelectedValue + "',null," +
                                                maximo + ",0,'A','" + Session["usuario"].ToString() + "',current_timestamp(),'C') ";
                                        MySqlCommand myCommandinserta = new MySqlCommand(StrInsertaGrupo, conexion);
                                        //resultado.Text = resultado.Text + StrInsertaGrupo;
                                        //Ejecucion del comando en el servidor de BD
                                        myCommandinserta.ExecuteNonQuery();
                                    }

                                }
                            }
                            
                            Gen_Horarios();
                        }
                        conexion.Close();
                    /*}
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Nogeneracion", "Nogeneracion();", true);
                        //Gen_Horarios();
                    }*/
                /*}
                catch (Exception ex)
                {
                    //resultado.Text = resultado.Text + " ------" + ex.Message;
                }*/
            }
            else
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_tgeho();", true);
            }

        }

        private void Gen_Horarios()
        {
            System.Threading.Thread.Sleep(50);


            string strQueryProy = "";
            strQueryProy = " select distinct tgeho_tpees_clave PERIODO, tgeho_tcamp_clave CAMPUS, tgeho_tnive_clave NIVEL, " +
                  " tgeho_tmate_clave CLAVE_MATERIA, tmate_desc MATERIA, tgeho_no_alumno PRONOSTICO, " +
                  " tmate_min_cupo CUPO_MINIMO, tmate_max_cupo CUPO_MAXIMO, " +
                  " (SELECT group_concat(distinct ttir1_tprog_clave separator '| ') FROM ttir1 " +
                  "      where ttir1_tmate_clave = tmate_clave) PROGRAMAS, " +
                  " case when(SELECT group_concat(distinct tgrup_clave separator '| ') FROM tgrup " +
                  "         where tgrup_tcamp_clave = tgeho_tcamp_clave and tgrup_tpees_clave = tgeho_tpees_clave " +
                  "         and   tgrup_tmate_clave = tgeho_tmate_clave and tgrup_turno = tgeho_turno) is null then " +
                  "         'NO CUMPLE MINIMO DE ESTUDIANTES' " +
                  " ELSE " +
                  " (SELECT group_concat(distinct tgrup_clave separator '| ') FROM tgrup " +
                  "         where tgrup_tcamp_clave = tgeho_tcamp_clave and tgrup_tpees_clave = tgeho_tpees_clave " +
                  "        and   tgrup_tmate_clave = tgeho_tmate_clave and tgrup_turno = tgeho_turno) " +
                  " END GRUPOS " +
                  " from tgeho, tmate, tplan " +
                  " where tgeho_tmate_clave = tmate_clave  and tgeho_tpees_clave ='" + ddl_periodo_destino.SelectedValue + "' and tgeho_turno ='" + ddl_turno.SelectedValue + "'" +
                  " and    tgeho_tmate_clave=tplan_tmate_clave ";

            if (ddl_campus.SelectedValue.ToString() != "0")
            {
                strQueryProy = strQueryProy + " and  tgeho_tcamp_clave='" + ddl_campus.SelectedValue.ToString() + "'";
            }
            if (ddl_nivel.SelectedValue.ToString() != "0")
            {
                strQueryProy = strQueryProy + " and  tgeho_tnive_clave='" + ddl_nivel.SelectedValue.ToString() + "'";
            }
            if (ddl_programa.SelectedValue.ToString() != "0")
            {
                strQueryProy = strQueryProy + " and  tplan_tprog_clave='" + ddl_programa.SelectedValue.ToString() + "'";
            }

            strQueryProy = strQueryProy + " order by tgeho_tcamp_clave, tgeho_tnive_clave, tgeho_tmate_clave ";


            // resultado.Text = strQueryProy;
            MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            conexion.Open();
            try
            {
                Gridtgeho.Visible = false;
                GridTgrup.Visible = true;
                MySqlDataAdapter dataadapter = new MySqlDataAdapter(strQueryProy, conexion);
                DataSet dsgeho = new DataSet();
                dataadapter.Fill(dsgeho, "Gen_hor");
                GridTgrup.DataSource = dsgeho;
                GridTgrup.DataBind();
                GridTgrup.DataMember = "Gen_hor";
                GridTgrup.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridTgrup.UseAccessibleHeader = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_tgrup", "load_datatable_tgrup();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "generacion", "generacion();", true);
                conexion.Close();
                //exportarexcel(dsgeho);
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
            }
            conexion.Close();
        }

        protected void cancel_thodo_Click(object sender, EventArgs e)
        {
            combo_periodo();
            combo_campus();
            combo_turno();
            ddl_nivel.Items.Clear();
            ddl_programa.Items.Clear();
            ddl_periodo_destino.Items.Clear();
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
    }
}