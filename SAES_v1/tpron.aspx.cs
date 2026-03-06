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
    public partial class tpron : System.Web.UI.Page
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
            System.Threading.Thread.Sleep(50);

            string QerySelect = "select tusme_update, tusme_select from tuser, tusme, tmede " +
                              " where tuser_clave = '" + Session["usuario"].ToString() + "'" +
                              " and tusme_trole_clave = tuser_trole_clave and tusme_tmenu_clave = 3 and tusme_tmenu_clave = tmede_tmenu_clave " +
                              " and tusme_tmede_clave = tmede_clave and tmede_forma='tpron' ";

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
                        btn_tpron.Visible = false;
                    }
                    //grid_tespr_bind();
                }

            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tpron", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }

            string strQuerypees = "";
            strQuerypees = "select tpees_clave clave, tpees_desc periodo from tpees " +
                            " where tpees_estatus='A' and tpees_fin >= curdate() " +
                             " union " +
                             " select '000000' clave, '---------' periodo from dual " +
                             " order by clave ";

            try
            {
                DataTable TablaPeriodo = new DataTable();
                MySqlCommand ConsultaMySql = new MySqlCommand();
                MySqlDataReader DatosMySql;
                ConsultaMySql.Connection = conexion;
                ConsultaMySql.CommandType = CommandType.Text;
                ConsultaMySql.CommandText = strQuerypees;
                DatosMySql = ConsultaMySql.ExecuteReader();
                TablaPeriodo.Load(DatosMySql, LoadOption.OverwriteChanges);

                ddl_periodo.DataSource = TablaPeriodo;
                ddl_periodo.DataValueField = "clave";
                ddl_periodo.DataTextField = "periodo";
                ddl_periodo.DataBind();

                string strQuerycamp = "";
                strQuerycamp = "select tcamp_clave clave, tcamp_desc campus from tcamp " +
                               " where tcamp_estatus='A' " +
                                 " union " +
                                 " select '000' clave, '---------' campus from dual " +
                                 " order by clave ";


                DataTable TablaCamp = new DataTable();
                MySqlCommand ConsultaMySql1 = new MySqlCommand();
                MySqlDataReader DatosMySql1;
                ConsultaMySql1.Connection = conexion;
                ConsultaMySql1.CommandType = CommandType.Text;
                ConsultaMySql1.CommandText = strQuerycamp;
                DatosMySql1 = ConsultaMySql1.ExecuteReader();
                TablaCamp.Load(DatosMySql1, LoadOption.OverwriteChanges);

                ddl_campus.DataSource = TablaCamp;
                ddl_campus.DataValueField = "clave";
                ddl_campus.DataTextField = "campus";
                ddl_campus.DataBind();
            }
            catch(Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tpron", Session["usuario"].ToString());
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
            ddl_turno.Items.Clear();
            ddl_turno.Items.Add(new ListItem("-----", "X"));
            ddl_turno.Items.Add(new ListItem("Matutino", "M"));
            ddl_turno.Items.Add(new ListItem("Vespertino", "V"));
        }

        protected void grid_tpron_bind(object sender, EventArgs e)
        {
            string turno = "";
            if (ddl_turno.SelectedItem.ToString() == "Matutino")
            {
                turno = "M";
            }
            if (ddl_turno.SelectedItem.ToString() == "Vespertino")
            {
                turno = "V";
            }
            string strQueryPron = "";
            strQueryPron = "select tnive_desc nivel, tcapr_tprog_clave clave, tprog_desc programa, tpron_pronostico pron" +
                           " from tcapr " +
                           " inner join tprog on tcapr_tprog_clave = tprog_clave " +
                           " inner join tnive on tcapr_tprog_clave = tprog_clave and tprog_tnive_clave = tnive_clave " +
                           " left outer join tpron on tpron_tcamp_clave = tcapr_tcamp_clave and tpron_tprog_clave = tcapr_tprog_clave " +
                           "     and tpron_tpees_clave='" + ddl_periodo.SelectedValue.ToString() + "' and tpron_turno='" + turno + "'" +
                           " where tcapr_tcamp_clave = '" + ddl_campus.SelectedValue.ToString() + "' and tcapr_estatus='A' ";


            strQueryPron = strQueryPron + " order by nivel, clave ";

            //resultado.Text = "1--" + strQueryCapr;

            //Label1.Text = strQueryEsc;
            MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            conexion.Open();
            try
            {
                MySqlDataAdapter dataadapter = new MySqlDataAdapter(strQueryPron, conexion);
                DataSet ds = new DataSet();
                dataadapter.Fill(ds, "Pron");
                Gridtpron.DataSource = ds;
                Gridtpron.DataBind();
                Gridtpron.DataMember = "Pron";
                //Gridtpron.HeaderRow.TableSection = TableRowSection.TableHeader;
                Gridtpron.UseAccessibleHeader = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);
                Gridtpron.Visible = true;

                MySqlDataAdapter sqladapter = new MySqlDataAdapter();

                DataSet dssql1 = new DataSet();

                MySqlCommand commandsql1 = new MySqlCommand(strQueryPron, conexion);
                sqladapter.SelectCommand = commandsql1;
                sqladapter.Fill(dssql1);
                sqladapter.Dispose();
                commandsql1.Dispose();

                string QerySelect = "select tusme_update from tuser, tusme " +
                              " where tuser_clave = '" + Session["usuario"].ToString() + "'" +
                              " and tusme_trole_clave = tuser_trole_clave and tusme_tmenu_clave = 3 and tusme_tmede_clave = 8 ";
                try
                {
                    MySqlDataAdapter sqladapter0 = new MySqlDataAdapter();

                    DataSet dssql10 = new DataSet();

                    MySqlCommand commandsql10 = new MySqlCommand(QerySelect, conexion);
                    sqladapter0.SelectCommand = commandsql10;
                    sqladapter0.Fill(dssql10);
                    sqladapter0.Dispose();
                    commandsql10.Dispose();
                    if (dssql10.Tables[0].Rows[0][0].ToString() == "1" && ds.Tables[0].Rows.Count > 0)
                    {
                        btn_save.Visible = true;
                        btn_update.Visible = true;
                    }



                }
                catch (Exception ex)
                {
                    //resultado.Text = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tpron", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                }

                for (int i = 0; i < Gridtpron.Rows.Count; i++)
                {
                    TextBox pronos = (TextBox)Gridtpron.Rows[i].FindControl("pronostico");
                    pronos.Text = dssql1.Tables[0].Rows[i][3].ToString();
                }

            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tpron", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }

           
            conexion.Close();
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            LlenaPagina();
            combo_estatus();


            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                double resultado = 0;
                for (int i = 0; i < Gridtpron.Rows.Count; i++)
                {
                    TextBox pronos = (TextBox)Gridtpron.Rows[i].FindControl("pronostico");
                    
                    if (pronos.Text != "")
                    {
                       
                        bool valida;
                        try
                        {
                            Int32.Parse(pronos.Text);
                            valida = true;
                        }
                        catch
                        {
                            valida = false;
                        }
                        if (valida == false)
                        {
                            //resultado = "Pronóstico debe ser un valor numérico";
                            resultado = 1;
                            //
                        }
                    }
                }
                if (resultado == 0)
                {
                    MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                    conexion.Open();

                    // resultado.Text = strBorra;

                    for (int i = 0; i < Gridtpron.Rows.Count; i++)
                    {
                        TextBox pronos = (TextBox)Gridtpron.Rows[i].FindControl("pronostico");
                        string turno = "";
                        if (ddl_turno.SelectedItem.ToString() == "Matutino")
                        {
                            turno = "M";
                        }
                        if (ddl_turno.SelectedItem.ToString() == "Vespertino")
                        {
                            turno = "V";
                        }
                        if (pronos.Text != "")
                        {

                            string strBorra = "DELETE from tpron where tpron_tcamp_clave='" + ddl_campus.SelectedValue.ToString() + "' " +
                            " and tpron_tprog_clave='" + Gridtpron.Rows[i].Cells[1].Text.ToString() + "' and tpron_tpees_clave='" +
                            ddl_periodo.SelectedValue.ToString() + "' and tpron_turno='" + turno + "'";
                            MySqlCommand myCommandborra = new MySqlCommand(strBorra, conexion);
                            //Ejecucion del comando en el servidor de BD
                            myCommandborra.ExecuteNonQuery();

                            string strCadSQL = "insert into tpron values('" + ddl_campus.SelectedValue.ToString() + "','" +
                                ddl_periodo.SelectedValue.ToString() + "','" + Gridtpron.Rows[i].Cells[1].Text.ToString() + "','" + turno + "',"
                                + pronos.Text + ",'" + Session["usuario"].ToString() + "',current_timestamp())";

                            MySqlCommand myCommandinserta = new MySqlCommand(strCadSQL, conexion);
                            //Ejecucion del comando en el servidor de BD
                            myCommandinserta.ExecuteNonQuery();
                            Gridtpron.Visible = false;
                            //LlenaPagina();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                            //resultado.Text = resultado.Text + "-->" + strBorra + "-->" + strCadSQL;
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "valor_numerico", "valor_numerico();", true);
                }

            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tpron", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
            }


        }

        protected void btn_update_Click(object sender, EventArgs e)
        {
            double resultado = 0;
            for (int i = 0; i < Gridtpron.Rows.Count; i++)
            {
                TextBox pronos = (TextBox)Gridtpron.Rows[i].FindControl("pronostico");

                if (pronos.Text != "")
                {

                        resultado = 1;
                }
            }
            if (resultado == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "valor_numerico", "valor_numerico();", true);
            }
            else
            {
                MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                conexion.Open();
                try
                { 
                string turno = "";
                if (ddl_turno.SelectedItem.ToString() == "Matutino")
                {
                    turno = "M";
                }
                if (ddl_turno.SelectedItem.ToString() == "Vespertino")
                {
                    turno = "V";
                }
                string strInscritos = " select case when sum(tgrup_enroll) is null then 0 else sum(tgrup_enroll) end inscritos " +
                    " from tgrup " +
                    " where tgrup_tcamp_clave='" + ddl_campus.SelectedValue.ToString() + "' " +
                    " and   tgrup_tpees_clave='" + ddl_periodo.SelectedValue.ToString() + "' " +
                    " and   tgrup_turno='" + turno + "' and tgrup_origen='I' ";
                MySqlDataAdapter sqladapter = new MySqlDataAdapter();
                DataSet dsins = new DataSet();

                MySqlCommand commandins = new MySqlCommand(strInscritos, conexion);
                sqladapter.SelectCommand = commandins;
                sqladapter.Fill(dsins);
                sqladapter.Dispose();
                commandins.Dispose();
                //resultado.Text = "Inscritos:" + dsins.Tables[0].Rows[0][0].ToString();
                    if (dsins.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        //Borra generación de Horarios anterior

                        string strBorraHora = " delete from tgrup " +
                        " where tgrup_tcamp_clave='" + ddl_campus.SelectedValue.ToString() + "' " +
                        " and   tgrup_tpees_clave='" + ddl_periodo.SelectedValue.ToString() + "' " +
                        " and   tgrup_turno='" + turno + "' and tgrup_origen='I' ";
                        MySqlCommand myCommandborra = new MySqlCommand(strBorraHora, conexion);
                        //Ejecucion del comando en el servidor de BD
                        myCommandborra.ExecuteNonQuery();

                        string QerySelect = "select mate,  teo, prac, campo, sum(pron), sum(pron)/avg(maximo) funcion, minimo, maximo, " +
                          " case when(sum(pron) / avg(maximo) - truncate(sum(pron) / avg(maximo), 0)) = 0 then " +
                          "      truncate(sum(pron) / avg(maximo), 0) else truncate(sum(pron) / avg(maximo), 0) + 1 end grupos " +
                          " from(select tplan_tprog_clave prog, tplan_tmate_clave mate, tplan_consecutivo cons, " +
                          "      tmate_hr_teo teo, tmate_hr_prac prac, tmate_hr_campo campo, tmate_min_cupo minimo, tmate_max_cupo maximo, " +
                          "      tpron_pronostico pron " +
                          "      from tplan, tmate, tpron " +
                          "      Where tplan_periodo = 1 and   tplan_tmate_clave = tmate_clave " +
                          "      and   tpron_tcamp_clave = '" + ddl_campus.SelectedValue.ToString() + "' and tpron_tpees_clave = '" + ddl_periodo.SelectedValue.ToString() + "' " +
                          "      and tplan_tprog_clave = tpron_tprog_clave order by tplan_tmate_clave) x " +
                          " group by mate, teo, prac, campo";
                        //resultado.Text = resultado.Text + "--" + QerySelect;

                        DataSet dssql1 = new DataSet();

                        MySqlCommand commandsql1 = new MySqlCommand(QerySelect, conexion);
                        sqladapter.SelectCommand = commandsql1;
                        sqladapter.Fill(dssql1);
                        sqladapter.Dispose();
                        commandsql1.Dispose();
                        string materia = "";
                        decimal teo = 0, prac = 0, campo = 0;
                        if (dssql1.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < dssql1.Tables[0].Rows.Count; i++)
                            {
                                materia = dssql1.Tables[0].Rows[i][0].ToString();
                                teo = Convert.ToDecimal(dssql1.Tables[0].Rows[i][1].ToString());
                                prac = Convert.ToDecimal(dssql1.Tables[0].Rows[i][2].ToString());
                                campo = Convert.ToDecimal(dssql1.Tables[0].Rows[i][3].ToString());
                                double total, minimo, maximo, grupos;
                                total = Convert.ToDouble(dssql1.Tables[0].Rows[i][4].ToString());
                                minimo = Convert.ToDouble(dssql1.Tables[0].Rows[i][6].ToString());
                                maximo = Convert.ToDouble(dssql1.Tables[0].Rows[i][7].ToString());
                                grupos = Convert.ToDouble(dssql1.Tables[0].Rows[i][8].ToString());

                                if (total > minimo) // Si la suma de pronostico de la materia es mayor al mínimo para abrir un grupo
                                {
                                    string seccion = "";
                                    for (int w = 1; w <= grupos; w++)
                                    {

                                        if (teo > 0)
                                        {
                                            if (w < 10)
                                            {
                                                seccion = turno + "0" + w + "T";
                                            }
                                            else
                                            {
                                                seccion = turno + w + "T";

                                            }
                                            string StrInsertaGrupo = " insert into tgrup values('" + ddl_periodo.SelectedValue.ToString() + "','" +
                                                   ddl_campus.SelectedValue.ToString() + "','" + materia + "','" + seccion + "','" + turno + "',null," +
                                                   maximo + ",0,'A','" + Session["usuario"].ToString() + "',current_timestamp(),'I') ";
                                            MySqlCommand myCommandinserta = new MySqlCommand(StrInsertaGrupo, conexion);
                                            //resultado.Text = resultado.Text + StrInsertaGrupo;
                                            //Ejecucion del comando en el servidor de BD
                                            myCommandinserta.ExecuteNonQuery();
                                        }
                                        if (prac > 0)
                                        {
                                            if (w < 10)
                                            {
                                                seccion = turno + "0" + w + "P";
                                            }
                                            else
                                            {
                                                seccion = turno + w + "P";

                                            }
                                            string StrInsertaGrupo = " insert into tgrup values('" + ddl_periodo.SelectedValue.ToString() + "','" +
                                                    ddl_campus.SelectedValue.ToString() + "','" + materia + "','" + seccion + "','" + turno + "',null," +
                                                    maximo + ",0,'A','" + Session["usuario"].ToString() + "',current_timestamp(),'I') ";
                                            MySqlCommand myCommandinserta = new MySqlCommand(StrInsertaGrupo, conexion);
                                            //resultado.Text = resultado.Text + StrInsertaGrupo;
                                            //Ejecucion del comando en el servidor de BD
                                            myCommandinserta.ExecuteNonQuery();
                                        }
                                    }
                                }
                            }
                            //LlenaPagina();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                            Gridtpron.Visible = false;
                            btn_save.Visible = false;
                            btn_update.Visible = false;
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "NoProcesar", "NoProcesar();", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "NoGuardar", "unsave();", true);
                    }
                }
                catch (Exception ex)
                {
                    //resultado.Text = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tpron", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                }
                conexion.Close();
            }
        }

        protected void linkBttnBuscar_Click(object sender, EventArgs e)
        {

        }
    }
}