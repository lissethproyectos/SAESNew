using MySql.Data.MySqlClient;
using SAES_Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using static SAES_DBO.Models.ModelCobranza;

namespace SAES_v1
{
    public partial class thora : System.Web.UI.Page
    {
        #region <Variables>
        //Utilidades utils = new UtilidadesHorarioService
        HorarioService serviceHorario = new HorarioService();
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
                    combo_periodo();
                    combo_campus();
                    combo_turno();
                    combo_estatus();
                    grid_salones_bind();
                    grid_materias_bind();
                    Gridthora.DataSource = null;
                    Gridthora.DataBind();
                    if (Global.campus != "")
                    {
                        ddl_campus.SelectedValue = Global.campus;
                        ddl_turno.SelectedValue = Global.turno;
                        ddl_periodo.SelectedValue = Global.periodo;
                        txt_materia.Text = Global.materia;
                        txt_nombre_materia.Text = Global.nombre_materia;
                        txt_grupo.Text = Global.grupo;
                        txt_salon.Text = Global.salon;
                        txt_nombre_salon.Text = Global.nombre_salon;
                        //Carga_Disponibilidad();
                    }
                    combo_dia();
                    Global.hinicio_docente = "";
                    Global.hfin_docente = "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_materias", "load_datatable_materias();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_salones", "load_datatable_salones();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_hora1", "load_datatable_hora1();", true);

                }
            }
        }
        private void LlenaPagina()
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "menu", "carga_menu();", true);

            string QerySelect = "select tusme_update, tusme_select from tuser, tusme, tmede " +
                              " where tuser_clave = '" + Session["usuario"].ToString() + "'" +
                              " and tusme_trole_clave = tuser_trole_clave and tusme_tmenu_clave = 5 and tusme_tmenu_clave = tmede_tmenu_clave " +
                              " and tusme_tmede_clave = tmede_clave and tmede_forma='thora' ";

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
                    btn_thora.Visible = false;
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
            ddl_estatus.Items.Add(new ListItem("-----", "0"));
            ddl_estatus.Items.Add(new ListItem("Activo", "A"));
            ddl_estatus.Items.Add(new ListItem("Inactivo", "B"));


        }

        protected void combo_turno()
        {
            ddl_turno.Items.Clear();
            ddl_turno.Items.Add(new ListItem("-----", "0"));
            ddl_turno.Items.Add(new ListItem("Matutino", "M"));
            ddl_turno.Items.Add(new ListItem("Vespertino", "V"));
            ddl_turno.Items.Add(new ListItem("Nocturno", "N"));
        }

        protected void grid_grupos_bind(object sender, EventArgs e)
        {
            grid_grupos_bind();
        }

        protected void grid_grupos_bind()
        {
            if (ddl_periodo.SelectedValue != "" && ddl_turno.SelectedValue != "" && ddl_campus.SelectedValue != "")
            {
                string strQueryGrup = "";
                strQueryGrup = "select tgrup_tmate_clave clave, tmate_desc materia, tgrup_clave grupo, tgrup_tsalo_clave salon, tsalo_desc nombre_salon, " +
                               " tgrup_capacidad capacidad, tgrup_enroll inscritos, tgrup_capacidad-tgrup_enroll disponible,  " +
                               " tgrup_estatus c_estatus, case when tgrup_estatus='A' then 'ACTIVO' else 'INACTIVO' end estatus, fecha(date_format(tgrup_date,'%Y-%m-%d')) fecha " +
                               " from tgrup " +
                               " inner join tsalo on tgrup_tsalo_clave=tsalo_clave ";

                strQueryGrup = strQueryGrup +
                " inner join tmate on tmate_clave = tgrup_tmate_clave " +
                " where tgrup_tcamp_clave = '" + ddl_campus.SelectedValue.ToString() + "'" +
                " and   tgrup_tpees_clave = '" + ddl_periodo.SelectedValue.ToString() + "'" +
                " and   tgrup_turno = '" + ddl_turno.SelectedValue + "'" +
                " and   tgrup_tsalo_clave is not null ";
                if (txt_materia.Text != "")
                {
                    strQueryGrup = strQueryGrup + " and tgrup_tmate_clave='" + txt_materia.Text + "'";
                }

                strQueryGrup = strQueryGrup + " order by clave, grupo ";
                MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                ConexionMySql.Open();
                try
                {

                    MySqlDataAdapter dataadapter = new MySqlDataAdapter(strQueryGrup, ConexionMySql);
                    DataSet ds = new DataSet();
                    dataadapter.Fill(ds, "thora");
                    Gridthora.DataSource = ds;
                    Gridthora.DataBind();
                    //Gridthora.DataMember = "thora";
                    //Gridthora.HeaderRow.TableSection = TableRowSection.TableHeader;
                    //Gridthora.UseAccessibleHeader = true;
                    //Gridthora.Visible = true;
                    //Gridthora1.Visible = false;


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
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_thora();", true);
            }
        }

        protected void Gridthora_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = Gridthora.SelectedRow;
                txt_materia.Text = HttpUtility.HtmlDecode(row.Cells[1].Text);
                txt_nombre_materia.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);
                txt_grupo.Text = HttpUtility.HtmlDecode(row.Cells[3].Text);
                txt_salon.Text = HttpUtility.HtmlDecode(row.Cells[4].Text);
                txt_nombre_salon.Text = HttpUtility.HtmlDecode(row.Cells[5].Text);

                txt_materia.Attributes.Add("readonly", "");
                txt_grupo.Attributes.Add("readonly", "");

                //save_thora.Visible = false;
                //update_thora.Visible = true;
                //Gridthora.Visible = false;
                save_thora.Visible = true;
                update_thora.Visible = false;
                Global.campus = ddl_campus.SelectedValue;
                Global.turno = ddl_turno.SelectedValue;
                Global.periodo = ddl_periodo.SelectedValue;
                Global.materia = txt_materia.Text;
                Global.nombre_materia = txt_nombre_materia.Text;
                Global.grupo = txt_grupo.Text;
                Global.salon = txt_salon.Text;
                Global.nombre_salon = txt_nombre_salon.Text;
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "tabHorarios", "$('#nav-horario-tab').tab('show')", true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "collapse", "$('#collapseGrid').collapse('toggle')", true);

                Carga_Disponibilidad();
            }
            catch (Exception ex)
            {
                ///logs
            }

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

        protected void save_thora_Click(object sender, EventArgs e)
        {
            if (ddl_periodo.SelectedValue != "" && ddl_turno.SelectedValue != "" && ddl_campus.SelectedValue != "" && !String.IsNullOrEmpty(txt_materia.Text) && !String.IsNullOrEmpty(txt_grupo.Text) && !String.IsNullOrEmpty(txt_salon.Text) && ddl_estatus.SelectedValue != "0")
            {
                if (ddl_estatus.SelectedValue == "B")
                {
                    string strCborraSQL = " delete from thora where thora_tpees_clave='" + ddl_periodo.SelectedValue + " ' and thora_tcamp_clave='" +
                            ddl_campus.SelectedValue + "' and thora_tmate_clave='" + txt_materia.Text + "' and thora_tgrup_clave='" + txt_grupo.Text + "' " +
                            " and thora_tsalo_clave='" + txt_salon.Text + " ' and thora_tdias_clave='" +
                            ddl_dia.SelectedValue.ToString() + "' and thora_thocl_inicio='" + Global.hinicio_docente.ToString() + "' " +
                            " and thora_thocl_fin='" + Global.hfin_docente.ToString() + "'";
                    MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                    ConexionMySql.Open();
                    MySqlCommand myCommandborra = new MySqlCommand(strCborraSQL, ConexionMySql);
                    //Ejecucion del comando en el servidor de BD 
                    myCommandborra.ExecuteNonQuery();
                    LlenaPagina();
                }
                else
                {


                    if (Global.hinicio_docente != "") // Selecciona un horario previamente cargado
                    {
                        string strCborraSQL = " delete from thora where thora_tpees_clave='" + ddl_periodo.SelectedValue + " ' and thora_tcamp_clave='" +
                            ddl_campus.SelectedValue + "' and thora_tmate_clave='" + txt_materia.Text + "' and thora_tgrup_clave='" + txt_grupo.Text + "' " +
                            " and thora_tsalo_clave='" + txt_salon.Text + " ' and thora_tdias_clave='" +
                            ddl_dia.SelectedValue.ToString() + "' and thora_thocl_inicio='" + Global.hinicio_docente.ToString() + "' " +
                            " and thora_thocl_fin='" + Global.hfin_docente.ToString() + "'";
                        MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                        ConexionMySql.Open();
                        MySqlCommand myCommandborra = new MySqlCommand(strCborraSQL, ConexionMySql);
                        //Ejecucion del comando en el servidor de BD 
                        myCommandborra.ExecuteNonQuery();
                        //Verifica que no exista traslape en el nuevo registro de horario
                        string stQuerytraslape = " select count(*) from thora " +
                          " where thora_tpees_clave='" + ddl_periodo.SelectedValue + " ' and thora_tcamp_clave='" +
                          ddl_campus.SelectedValue + "' and thora_tsalo_clave='" + txt_salon.Text + " '" +
                          " and thora_tdias_clave =" + ddl_dia.SelectedValue.ToString() + "" +
                          " and((" + ddl_hini.SelectedValue.ToString() + " >= thora_thocl_inicio and " + ddl_hini.SelectedValue.ToString() + " <= thora_thocl_fin) or " +
                          " (" + ddl_hfin.SelectedValue.ToString() + " >= thora_thocl_inicio and " + ddl_hfin.SelectedValue.ToString() + " <= thora_thocl_fin) or" +
                          " ( " + ddl_hini.SelectedValue.ToString() + " <= thora_thocl_inicio and " + ddl_hfin.SelectedValue.ToString() + " >= thora_thocl_fin ) ) ";
                        DataSet dstras = new DataSet();
                        MySqlCommand commandtras = new MySqlCommand(stQuerytraslape, ConexionMySql);
                        MySqlDataAdapter sqladapter = new MySqlDataAdapter();
                        sqladapter.SelectCommand = commandtras;
                        sqladapter.Fill(dstras);
                        sqladapter.Dispose();
                        commandtras.Dispose();
                        //resultado.Text = "1-" + stQuerytraslape;
                        string conteo = dstras.Tables[0].Rows[0][0].ToString();
                        if (dstras.Tables[0].Rows[0][0].ToString() != "0") // Si existe traslapa
                        {
                            //resultado.Text = strCborraSQL;
                            string strCadSQL = "INSERT INTO thora Values ('" + ddl_periodo.SelectedValue + "','" + ddl_campus.SelectedValue + "','" +
                                txt_materia.Text + "','" + txt_grupo.Text + "','" + txt_salon.Text + "'," +
                                ddl_dia.SelectedValue.ToString() + "," + Global.hinicio_docente.ToString() + "," +
                               Global.hfin_docente.ToString() + ",'" + Global.usuario.ToString() + "',current_timestamp())";
                            //resultado.Text = strCadSQL;
                            MySqlCommand myCommandinserta = new MySqlCommand(strCadSQL, ConexionMySql);
                            //Ejecucion del comando en el servidor de BD 
                            myCommandinserta.ExecuteNonQuery();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "traslape", "traslape();", true);
                            Carga_Disponibilidad();
                        }
                        else // Se inserta registro en tabla thora
                        {
                            //resultado.Text = strCborraSQL;
                            string strCadSQL = "INSERT INTO thora Values ('" + ddl_periodo.SelectedValue + "','" + ddl_campus.SelectedValue + "','" +
                                txt_materia.Text + "','" + txt_grupo.Text + "','" + txt_salon.Text + "'," +
                                ddl_dia.SelectedValue.ToString() + "," + ddl_hini.SelectedValue.ToString() + "," +
                                ddl_hfin.SelectedValue.ToString() + ",'" + Session["usuario"].ToString() + "',current_timestamp())";
                            //resultado.Text = strCadSQL;
                            MySqlCommand myCommandinserta = new MySqlCommand(strCadSQL, ConexionMySql);
                            //Ejecucion del comando en el servidor de BD 
                            myCommandinserta.ExecuteNonQuery();
                            Global.hinicio_docente = "";
                            Global.hfin_docente = "";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "save", "save();", true);
                            txt_materia.Text = null; txt_nombre_materia.Text = null; txt_grupo.Text = null;
                            txt_salon.Text = null; txt_nombre_salon.Text = null;
                            combo_estatus();
                            Carga_Disponibilidad();
                            //Gridthora1.SelectedIndex = -1;
                        }
                        combo_dia();
                        ddl_hfin.Items.Clear();
                        Global.hinicio_docente = ""; Global.hfin_docente = "";
                        LlenaPagina();

                    }
                    else // Es un registro nuevo
                    {
                        MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                        ConexionMySql.Open();

                        //Valida traslape de horarios

                        string stQuerytraslape = " select count(*) from thora " +
                          " where thora_tpees_clave='" + ddl_periodo.SelectedValue + " ' and thora_tcamp_clave='" +
                          ddl_campus.SelectedValue + "' and thora_tsalo_clave='" + txt_salon.Text + " '" +
                          " and thora_tdias_clave =" + ddl_dia.SelectedValue.ToString() + "" +
                          " and((" + ddl_hini.SelectedValue.ToString() + " >= thora_thocl_inicio and " + ddl_hini.SelectedValue.ToString() + " <= thora_thocl_fin) or " +
                          " (" + ddl_hfin.SelectedValue.ToString() + " >= thora_thocl_inicio and " + ddl_hfin.SelectedValue.ToString() + " <= thora_thocl_fin) or" +
                          " ( " + ddl_hini.SelectedValue.ToString() + " <= thora_thocl_inicio and " + ddl_hfin.SelectedValue.ToString() + " >= thora_thocl_fin ) ) ";
                        MySqlDataAdapter sqladapter = new MySqlDataAdapter();
                        DataSet dstras = new DataSet();

                        MySqlCommand commandtras = new MySqlCommand(stQuerytraslape, ConexionMySql);
                        sqladapter.SelectCommand = commandtras;
                        sqladapter.Fill(dstras);
                        sqladapter.Dispose();
                        commandtras.Dispose();
                        if (dstras.Tables[0].Rows[0][0].ToString() != "0") //Existe traslape
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "traslape", "traslape();", true);
                            //Carga_Disponibilidad();
                        }
                        else // Se graba registro en thora
                        {
                            string strCadSQL = "INSERT INTO thora Values ('" + ddl_periodo.SelectedValue + "','" + ddl_campus.SelectedValue + "','" +
                                txt_materia.Text + "','" + txt_grupo.Text + "','" + txt_salon.Text + "'," +
                                ddl_dia.SelectedValue.ToString() + "," + ddl_hini.SelectedValue.ToString() + "," +
                                ddl_hfin.SelectedValue.ToString() + ",'" + Session["usuario"].ToString() + "',current_timestamp())";
                            //resultado.Text = strCadSQL;
                            MySqlCommand myCommandinserta = new MySqlCommand(strCadSQL, ConexionMySql);
                            //Ejecucion del comando en el servidor de BD 
                            myCommandinserta.ExecuteNonQuery();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "save", "save();", true);
                            // txt_materia.Text = null; txt_nombre_materia.Text = null; txt_grupo.Text = null;
                            // txt_salon.Text = null; txt_nombre_salon.Text = null;
                            combo_estatus();
                            Carga_Disponibilidad();
                        }
                        combo_dia();
                        ddl_hfin.Items.Clear();
                        LlenaPagina();
                    }
                }

                //Carga_Disponibilidad();
            }
            else
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "mivalidacion();", true);
                grid_grupos_bind();
            }

        }

        protected void update_thora_Click(object sender, EventArgs e)
        {
            if (ddl_periodo.SelectedValue != "0" && ddl_turno.SelectedValue != "0" && ddl_campus.SelectedValue != "0" && !String.IsNullOrEmpty(txt_materia.Text) && !String.IsNullOrEmpty(txt_grupo.Text) && !String.IsNullOrEmpty(txt_salon.Text) && ddl_estatus.SelectedValue != "0")
            {
                string Query = "UPDATE ";


                MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                ConexionMySql.Open();
                MySqlCommand mysqlcmd = new MySqlCommand(Query, ConexionMySql);
                mysqlcmd.CommandType = CommandType.Text;
                try
                {
                    mysqlcmd.ExecuteNonQuery();
                    txt_materia.Text = null; txt_nombre_materia.Text = null; txt_grupo.Text = null;
                    txt_salon.Text = null; txt_nombre_salon.Text = null;
                    combo_estatus();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "update_Z", "update();", true);
                    txt_materia.Attributes.Remove("readonly");
                    txt_grupo.Attributes.Remove("readonly");
                    Global.campus = ddl_campus.SelectedValue;
                    Global.turno = ddl_turno.SelectedValue;
                    Global.periodo = ddl_periodo.SelectedValue;
                    Global.materia = txt_materia.Text;
                    Global.nombre_materia = txt_nombre_materia.Text;
                    Global.grupo = txt_grupo.Text;
                    Global.salon = txt_salon.Text;
                    Global.nombre_salon = txt_nombre_salon.Text;

                    //grid_grupos_bind();
                    Carga_Disponibilidad();
                }
                catch (Exception ex)
                {
                    //string test = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "thora", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                }
                finally
                {
                    ConexionMySql.Close();
                }
            }
            else
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "mivalidacion();", true);
                grid_grupos_bind();
            }
        }

        protected void cancel_thora_Click(object sender, EventArgs e)
        {
            combo_periodo();
            combo_campus();
            combo_turno();
            combo_estatus();
            txt_materia.Text = null;
            txt_nombre_materia.Text = null;
            txt_grupo.Text = null;
            txt_salon.Text = null;
            txt_nombre_salon.Text = null;

            //Gridthora.Visible = false;
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


        protected void grid_materias_bind()
        {
            //if (Gridtmate.Visible == true)
            //{
            //    Gridtmate.Visible = false;
            //}
            //else
            //{
            string strQueryMate = "";
            strQueryMate = " select tmate_clave clave, tmate_desc materia, tmate_creditos cred from tmate where tmate_estatus='A' ";

            strQueryMate = strQueryMate + " order by clave ";

            //resultado.Text = strQueryProg;

            //Label1.Text = strQueryEsc;
            MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            conexion.Open();
            try
            {
                MySqlDataAdapter dataadapter = new MySqlDataAdapter(strQueryMate, conexion);
                DataSet ds = new DataSet();
                dataadapter.Fill(ds, "Materia");
                Gridtmate.DataSource = ds;
                Gridtmate.DataBind();
                Gridtmate.DataMember = "Materia";
                Gridtmate.HeaderRow.TableSection = TableRowSection.TableHeader;
                Gridtmate.UseAccessibleHeader = true;
                //Gridtmate.Visible = true;
                conexion.Close();
                grid_grupos_bind();
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
            }
            //}
        }

        protected void Gridtmate_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = Gridtmate.SelectedRow;
                txt_materia.Text = HttpUtility.HtmlDecode(row.Cells[1].Text);
                txt_nombre_materia.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);
                //Gridtmate.Visible = false;
                grid_grupos_bind();
            }
            catch (Exception ex)
            {
                ///logs
            }

        }

        protected void Busca_Materia(object sender, EventArgs e)
        {
            string strQuery = "";
            strQuery = "  select tmate_desc from tmate where tmate_clave='" + txt_materia.Text + "' and tmate_estatus='A' ";

            MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            conexion.Open();
            try
            {
                MySqlDataAdapter sqladapter = new MySqlDataAdapter();

                DataSet dssql1 = new DataSet();

                MySqlCommand commandsql1 = new MySqlCommand(strQuery, conexion);
                sqladapter.SelectCommand = commandsql1;
                sqladapter.Fill(dssql1);
                sqladapter.Dispose();
                commandsql1.Dispose();

                if (dssql1.Tables[0].Rows.Count == 0)
                {
                    txt_materia.Text = null; txt_nombre_materia.Text = "NO EXISTE MATERIA";
                    grid_grupos_bind();
                }
                else
                {
                    txt_nombre_materia.Text = dssql1.Tables[0].Rows[0][0].ToString();
                    grid_grupos_bind();

                    //Txthorao.Text = ""; TxtClaveSalon.Text = ""; TxtSalon.Text = "";
                    //TxtCapacidad.Text = ""; TxtInscritos.Text = ""; TxtDisponibles.Text = "";

                    //Carga_Grup();

                }
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
            }
        }

        protected void grid_salones_bind()
        {
            string strQuerySalon = "";
            strQuerySalon = " select tsalo_clave clave, tsalo_desc salon, tsalo_minimo minimo, tsalo_maximo maximo, " +
                " tsalo_tipo c_tipo, case when tsalo_tipo='T' then 'TEORICO' when tsalo_tipo='P' then 'PRACTICO' when tsalo_tipo='M' then 'MIXTO' end tipo " +
                " from tsalo where tsalo_estatus='A' ";

            strQuerySalon = strQuerySalon + " order by clave ";

            //resultado.Text = strQueryProg;

            //Label1.Text = strQueryEsc;
            MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            conexion.Open();
            try
            {
                MySqlDataAdapter dataadapter = new MySqlDataAdapter(strQuerySalon, conexion);
                DataSet ds = new DataSet();
                dataadapter.Fill(ds, "Materia");
                Gridtsalo.DataSource = ds;
                Gridtsalo.DataBind();
                conexion.Close();
                grid_grupos_bind();
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "thora", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
            }

        }

        protected void Gridtsalo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = Gridtsalo.SelectedRow;
                txt_salon.Text = HttpUtility.HtmlDecode(row.Cells[1].Text);
                txt_nombre_salon.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);

                grid_grupos_bind();
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "thora", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
            }

        }

        protected void Busca_Salon(object sender, EventArgs e)
        {
            string strQuery = "";
            strQuery = "  select tsalo_desc, tsalo_maximo from tsalo where tsalo_clave='" + txt_salon.Text + "' and tsalo_estatus='A' ";

            MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            conexion.Open();
            try
            {
                MySqlDataAdapter sqladapter = new MySqlDataAdapter();

                DataSet dssql1 = new DataSet();

                MySqlCommand commandsql1 = new MySqlCommand(strQuery, conexion);
                sqladapter.SelectCommand = commandsql1;
                sqladapter.Fill(dssql1);
                sqladapter.Dispose();
                commandsql1.Dispose();

                if (dssql1.Tables[0].Rows.Count == 0)
                {
                    txt_salon.Text = null; txt_nombre_salon.Text = "NO EXISTE SALÓN";

                    // Txthorao.Text = ""; TxtClaveSalon.Text = ""; TxtSalon.Text = "";
                    // TxtCapacidad.Text = ""; TxtInscritos.Text = ""; TxtDisponibles.Text = "";
                    grid_grupos_bind();
                }
                else
                {
                    txt_nombre_salon.Text = dssql1.Tables[0].Rows[0][0].ToString();
                    grid_grupos_bind();

                    //Txthorao.Text = ""; TxtClaveSalon.Text = ""; TxtSalon.Text = "";
                    //TxtCapacidad.Text = ""; TxtInscritos.Text = ""; TxtDisponibles.Text = "";

                    //Carga_Grup();

                }
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
            }
        }

        protected void combo_dia()
        {
            string strQuerydias = "";
            strQuerydias = "select tdias_clave clave, tdias_desc dias from tdias " +
                             " union " +
                             " select '0' clave, '---------' dias from dual " +
                             " order by clave ";

            MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            conexion.Open();
            try
            {

                DataTable TablaDias = new DataTable();
                MySqlCommand ConsultaMySql = new MySqlCommand();
                MySqlDataReader DatosMySql;
                ConsultaMySql.Connection = conexion;
                ConsultaMySql.CommandType = CommandType.Text;
                ConsultaMySql.CommandText = strQuerydias;
                DatosMySql = ConsultaMySql.ExecuteReader();
                TablaDias.Load(DatosMySql, LoadOption.OverwriteChanges);

                ddl_dia.DataSource = TablaDias;
                ddl_dia.DataValueField = "clave";
                ddl_dia.DataTextField = "dias";
                ddl_dia.DataBind();

                string strQueryhinicio = "";
                strQueryhinicio = "select thocl_clave clave, thocl_inicia inicia from thocl " +
                                 " union " +
                                 " select 0 clave, '---------' inicia from dual " +
                                 " order by clave ";

                DataTable TablaHinicia = new DataTable();
                MySqlCommand ConsultaMySqlh = new MySqlCommand();
                MySqlDataReader DatosMySqlh;
                ConsultaMySqlh.Connection = conexion;
                ConsultaMySqlh.CommandType = CommandType.Text;
                ConsultaMySqlh.CommandText = strQueryhinicio;
                DatosMySqlh = ConsultaMySqlh.ExecuteReader();
                TablaHinicia.Load(DatosMySqlh, LoadOption.OverwriteChanges);

                ddl_hini.DataSource = TablaHinicia;
                ddl_hini.DataValueField = "clave";
                ddl_hini.DataTextField = "inicia";
                ddl_hini.DataBind();
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
            }

            conexion.Close();
        }

        protected void Carga_Hora_fin(object sender, EventArgs e)
        {
            Carga_Hora_fin();
        }

        protected void Carga_Hora_fin()
        {
            MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            conexion.Open();
            string strQueryhfin = "";
            strQueryhfin = "select thocl_clave clave, thocl_fin fin from thocl " +
                           " where thocl_clave >= '" + ddl_hini.SelectedValue.ToString() + "'" +
                             " union " +
                             " select 0 clave, '---------' fin from dual " +
                             " order by clave ";
            try
            {
                DataTable TablaHfin = new DataTable();
                MySqlCommand ConsultaMySqlh = new MySqlCommand();
                MySqlDataReader DatosMySqlh;
                ConsultaMySqlh.Connection = conexion;
                ConsultaMySqlh.CommandType = CommandType.Text;
                ConsultaMySqlh.CommandText = strQueryhfin;
                DatosMySqlh = ConsultaMySqlh.ExecuteReader();
                TablaHfin.Load(DatosMySqlh, LoadOption.OverwriteChanges);

                ddl_hfin.DataSource = TablaHfin;
                ddl_hfin.DataValueField = "clave";
                ddl_hfin.DataTextField = "fin";
                ddl_hfin.DataBind();
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
            }
        }

        private void Carga_Disponibilidad()
        {

            MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            conexion.Open();
            try
            {

                MySqlDataAdapter sqladapter = new MySqlDataAdapter();

                string StrQueryDias = " select  tdias_desc dia, a.thocl_inicia inicio, b.thocl_fin fin, a.thocl_clave, b.thocl_clave  " +
                    " from thora, tdias, thocl a, thocl b where thora_tpees_clave='" + ddl_periodo.SelectedValue + " ' and thora_tcamp_clave='" +
                    ddl_campus.SelectedValue + "' and thora_tmate_clave='" + Gridthora.SelectedRow.Cells[1].Text + "' and thora_tgrup_clave='" + Gridthora.SelectedRow.Cells[3].Text + "' " +
                    " and thora_tsalo_clave='" + Gridthora.SelectedRow.Cells[4].Text + "'" +
                    " and thora_tdias_clave=tdias_clave and thora_thocl_inicio=a.thocl_clave and thora_thocl_fin=b.thocl_clave " +
                    " order by dia, inicio,fin ";

                DataSet ds = new DataSet();
                MySqlDataAdapter dataadapter1 = new MySqlDataAdapter(StrQueryDias, conexion);
                dataadapter1.Fill(ds, "Disponibilidad");
                //Gridthora1.DataSource = ds;
                //Gridthora1.DataBind();
                //Gridthora1.DataMember = "Disponibilidad";

                if (ds.Tables[0].Rows.Count == 0)
                {
                    StrQueryDias = " select '' dia, '' inicio, '' fin  " +
                    " from dual ";

                    //  resultado.Text = "1--" + strQueryTelefono + "----" + strQueryTelefono;
                    DataSet ds1 = new DataSet();
                    MySqlDataAdapter dataadapter = new MySqlDataAdapter(StrQueryDias, conexion);
                    dataadapter.Fill(ds1, "Disponibilidad");
                    //Gridthora1.DataSource = ds1;
                    //Gridthora1.DataBind();
                }

                //Gridthora1.Visible = true;
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
            }
            conexion.Close();
            ddl_dia.Focus();

        }

        protected void GridCatalogo_Click(object sender, EventArgs e)
        {
            //GridViewRow row = Gridthora1.SelectedRow;
            GridView row = sender as GridView;



            string strQuery = "";
            strQuery = " select distinct tdias_clave, a.thocl_clave, b.thocl_clave " +
                       " from thora, tdias, thocl a, thocl b " +
                       " where thora_tpees_clave='" + ddl_periodo.SelectedValue.ToString() + "' and thora_tcamp_clave='" + ddl_campus.SelectedValue.ToString() + "'" +
                       " and   thora_tmate_clave='" + txt_materia.Text + "' and thora_tsalo_clave='" + txt_salon.Text + "' and tdias_desc='" + row.SelectedRow.Cells[1].Text.ToString() + "' " +
                       " and a.thocl_inicia='" + row.SelectedRow.Cells[2].Text.ToString() + "' and b.thocl_fin='" + row.SelectedRow.Cells[3].Text.ToString() + "'";

            // resultado.Text = strQuery;
            MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            conexion.Open();
            try
            {
                MySqlDataAdapter sqladapter = new MySqlDataAdapter();

                DataSet dssql1 = new DataSet();

                MySqlCommand commandsql1 = new MySqlCommand(strQuery, conexion);
                sqladapter.SelectCommand = commandsql1;
                sqladapter.Fill(dssql1);
                sqladapter.Dispose();
                commandsql1.Dispose();

                combo_dia();

                ddl_dia.Items.FindByText(row.SelectedRow.Cells[1].Text.ToString()).Selected = true;
                ddl_hini.Items.FindByText(row.SelectedRow.Cells[2].Text.ToString()).Selected = true;

                string strQueryhfin = "";
                strQueryhfin = "select thocl_clave clave, thocl_fin fin from thocl " +
                                  " where thocl_inicia >= '" + row.SelectedRow.Cells[2].Text.ToString() + "'" +
                                 " union " +
                                 " select 0 clave, '---------' fin from dual " +
                                 " order by clave ";

                DataTable Tablahfin = new DataTable();
                MySqlCommand ConsultaMySqlf = new MySqlCommand();
                MySqlDataReader DatosMySqlf;
                ConsultaMySqlf.Connection = conexion;
                ConsultaMySqlf.CommandType = CommandType.Text;
                ConsultaMySqlf.CommandText = strQueryhfin;
                DatosMySqlf = ConsultaMySqlf.ExecuteReader();
                Tablahfin.Load(DatosMySqlf, LoadOption.OverwriteChanges);

                ddl_hfin.DataSource = Tablahfin;
                ddl_hfin.DataValueField = "clave";
                ddl_hfin.DataTextField = "fin";
                ddl_hfin.DataBind();


                ddl_hfin.Items.FindByText(row.SelectedRow.Cells[3].Text.ToString()).Selected = true;

                Global.hinicio_docente = dssql1.Tables[0].Rows[0][1].ToString();
                Global.hfin_docente = dssql1.Tables[0].Rows[0][2].ToString();

                save_thora.Visible = false;
                update_thora.Visible = true;
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
            }
            conexion.Close();

        }

        protected void ddl_campus_SelectedIndexChanged(object sender, EventArgs e)
        {
            grid_materias_bind();
        }

        protected void Gridthora_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    GridView GridDet = e.Row.FindControl("Gridthora1") as GridView;
                    DataTable dt = new DataTable();
                    GridDet.DataSource = serviceHorario.ObtenerHorarioMateria(ddl_periodo.SelectedValue, ddl_campus.SelectedValue, e.Row.Cells[1].Text, e.Row.Cells[3].Text, e.Row.Cells[4].Text);
                    GridDet.DataBind();
                }
            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcedc", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }

        }
    }
}