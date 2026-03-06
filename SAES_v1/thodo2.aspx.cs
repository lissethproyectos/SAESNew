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
    public partial class thodo2 : System.Web.UI.Page
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
                    combo_estatus();
                    if (Global.campus != "")
                    {
                        ddl_campus.SelectedValue = Global.campus;
                        ddl_turno.SelectedValue = Global.turno;
                        ddl_periodo.SelectedValue = Global.periodo;
                        //txt_materia.Text = Global.materia;
                        //txt_nombre_materia.Text = Global.nombre_materia;
                        //txt_grupo.Text = Global.grupo;
                        //txt_salon.Text = Global.salon;
                        //txt_nombre_salon.Text = Global.nombre_salon;
                        //Carga_Disponibilidad();
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
                              " and tusme_tmede_clave = tmede_clave and tmede_forma='thodo' ";

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
            //if (Gridthora.Visible == true)
            //{
            //    Gridthora.Visible = false;
            //}
            //else
            //{
                if (ddl_periodo.SelectedValue != "0" && ddl_turno.SelectedValue != "0" && ddl_campus.SelectedValue != "0")
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
                    /* if (TxtMate.Text != "")
                     {
                         strQueryGrup = strQueryGrup + " and thora_tmate_clave='" + TxtMate.Text + "'";
                     }*/

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
                        Gridthora.DataMember = "thora";
                        Gridthora.HeaderRow.TableSection = TableRowSection.TableHeader;
                        Gridthora.UseAccessibleHeader = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);
                        Gridthora.Visible = true;
                        GridDocentes.Visible = false;


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
            //}
        }

        protected void Gridthora_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

               GridViewRow row = Gridthora.SelectedRow;
                //txt_materia.Text = HttpUtility.HtmlDecode(row.Cells[1].Text);
                //txt_nombre_materia.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);
                //txt_grupo.Text = HttpUtility.HtmlDecode(row.Cells[3].Text);
                //txt_salon.Text = HttpUtility.HtmlDecode(row.Cells[4].Text);
                //txt_nombre_salon.Text = HttpUtility.HtmlDecode(row.Cells[5].Text);
                save_thodo.Visible = false;
                update_thora.Visible = true;
                Gridthora.Visible = false;
                Global.campus = ddl_campus.SelectedValue;
                Global.turno = ddl_turno.SelectedValue;
                Global.periodo = ddl_periodo.SelectedValue;
                Global.materia = HttpUtility.HtmlDecode(row.Cells[1].Text);
                Global.nombre_materia = HttpUtility.HtmlDecode(row.Cells[2].Text);
                Global.grupo = HttpUtility.HtmlDecode(row.Cells[3].Text);
                Global.salon = HttpUtility.HtmlDecode(row.Cells[4].Text);
                Global.nombre_salon = HttpUtility.HtmlDecode(row.Cells[5].Text);
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

        protected void save_thodo_Click(object sender, EventArgs e)
        {
            if (ddl_periodo.SelectedValue != "0" && ddl_turno.SelectedValue != "0" && ddl_campus.SelectedValue != "0" && !String.IsNullOrEmpty(Gridthora.SelectedRow.Cells[1].Text) && !String.IsNullOrEmpty(Gridthora.SelectedRow.Cells[3].Text) && !String.IsNullOrEmpty(Gridthora.SelectedRow.Cells[4].Text) && ddl_estatus.SelectedValue != "0")
            {
                string strExiste = " select count(*) from thodo where thodo_tpees_clave='" + ddl_periodo.SelectedValue + " ' and thodo_tcamp_clave='" +
                           ddl_campus.SelectedValue + "' and thodo_tmate_clave='" + Gridthora.SelectedRow.Cells[1].Text + "' and thodo_tgrup_clave='" + Gridthora.SelectedRow.Cells[3].Text + "' " +
                           " and thodo_tpers_num in (select tpers_num from tpers where tpers_id='" + txt_docente.Text + "')" ;
                MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                ConexionMySql.Open();
                DataSet dsexiste = new DataSet();

                MySqlCommand commandsqlexiste = new MySqlCommand(strExiste, ConexionMySql);
                MySqlDataAdapter sqladapter = new MySqlDataAdapter();
                sqladapter.SelectCommand = commandsqlexiste;
                sqladapter.Fill(dsexiste);
                sqladapter.Dispose();
                commandsqlexiste.Dispose();
                if (dsexiste.Tables[0].Rows[0][0].ToString() != "0")
                {
                    string strCborraSQL = " update thodo set thodo_estatus='" + ddl_estatus.SelectedValue + "' where thodo_tpees_clave='" + ddl_periodo.SelectedValue + " ' and thodo_tcamp_clave='" +
                            ddl_campus.SelectedValue + "' and thodo_tmate_clave='" + Gridthora.SelectedRow.Cells[1].Text + "' and thodo_tgrup_clave='" + Gridthora.SelectedRow.Cells[3].Text + "' " +
                            " and thodo_tpers_num in (select tpers_num from tpers where tpers_id='" + txt_docente.Text + "')";

                    MySqlCommand myCommandborra = new MySqlCommand(strCborraSQL, ConexionMySql);
                    //Ejecucion del comando en el servidor de BD 
                    myCommandborra.ExecuteNonQuery();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "save", "save();", true);
                    Carga_Disponibilidad();
                    LlenaPagina();
                }
                else
                {

                    string strcuentaSQL = " select count(*) from thodo where thodo_tpees_clave='" + ddl_periodo.SelectedValue + " ' and thodo_tcamp_clave='" +
                            ddl_campus.SelectedValue + "' and thodo_tmate_clave='" + Gridthora.SelectedRow.Cells[1].Text + "' and thodo_tgrup_clave='" + Gridthora.SelectedRow.Cells[3].Text + "' " +
                            " and thodo_estatus='A' ";
                    DataSet dscuenta = new DataSet();

                    
                    MySqlCommand commandsqlcuenta = new MySqlCommand(strcuentaSQL, ConexionMySql);

                    sqladapter.SelectCommand = commandsqlcuenta;
                    sqladapter.Fill(dscuenta);
                    sqladapter.Dispose();
                    commandsqlcuenta.Dispose();
                    if (dscuenta.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        double contador = 0;

                        string strHorario = " select thora_tdias_clave, thora_thocl_inicio, thora_thocl_fin from thora " +
                          " where thora_tpees_clave='" + ddl_periodo.SelectedValue + " ' and thora_tcamp_clave='" +
                          ddl_campus.SelectedValue + "' and thora_tmate_clave='" + Gridthora.SelectedRow.Cells[1].Text + " ' and thora_tgrup_clave='" + Gridthora.SelectedRow.Cells[3].Text + "'";
                        DataSet dshorario = new DataSet();

                        MySqlCommand commandsqlhorario = new MySqlCommand(strHorario, ConexionMySql);
                        sqladapter.SelectCommand = commandsqlhorario;
                        sqladapter.Fill(dshorario);
                        sqladapter.Dispose();
                        commandsqlhorario.Dispose();
                        for (int i = 0; i <= dshorario.Tables[0].Rows.Count; i++)
                        {
                            string strTraslape = " select count(*) from thora, thodo " +
                             " where thora_tpees_clave='" + ddl_periodo.SelectedValue + " ' and thora_tcamp_clave='" +
                             ddl_campus.SelectedValue + "'" +
                             " and thodo_tpees_clave=thora_tpees_clave and thodo_tcamp_clave=thora_tcamp_clave and thodo_tmate_clave=thora_tmate_clave " +
                             " and thodo_tgrup_clave=thora_tgrup_clave and thodo_tpers_num in (select tpers_num from tpers where tpers_id='" + txt_docente.Text + "') " +
                              " and thodo_estatus='A' and thora_tdias_clave =" + dshorario.Tables[0].Rows[0][0].ToString() + "" +
                              " and((" + dshorario.Tables[0].Rows[0][1].ToString() + " >= thora_thocl_inicio and " + dshorario.Tables[0].Rows[0][1].ToString() + " <= thora_thocl_fin) or " +
                              " (" + dshorario.Tables[0].Rows[0][2].ToString() + " >= thora_thocl_inicio and " + dshorario.Tables[0].Rows[0][1].ToString() + " <= thora_thocl_fin) or" +
                              " ( " + dshorario.Tables[0].Rows[0][1].ToString() + " <= thora_thocl_inicio and " + dshorario.Tables[0].Rows[0][2].ToString() + " >= thora_thocl_fin ) ) ";
                            try
                            {
                                DataSet dstras = new DataSet();
                                MySqlCommand commandtras = new MySqlCommand(strTraslape, ConexionMySql);
                                sqladapter.SelectCommand = commandtras;
                                sqladapter.Fill(dstras);
                                sqladapter.Dispose();
                                commandtras.Dispose();
                                //resultado.Text = resultado.Text + "-->" + strTraslape + "=" + dstras.Tables[0].Rows[0][0].ToString();
                                if (dstras.Tables[0].Rows[0][0].ToString() != "0")
                                {
                                    contador = 1;
                                }
                            }
                            catch (Exception ex)
                            {
                                //resultado.Text = ex.Message;
                            }

                        }
                        //resultado.Text = "1-" + stQuerytraslape;
                        if (contador == 1)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "traslape", "traslape();", true);
                        }
                        else
                        {
                            //resultado.Text = strCborraSQL;

                            /*string strCborraSQL = "delete from thodo where thodo_tpees_clave='" + ddl_periodo.SelectedValue + "' and thodo_tcamp_clave='" +
                                ddl_campus.SelectedValue + "' and thodo_tmate_clave='" + txt_materia.Text + "' and thodo_tgrup_clave='" + txt_grupo.Text + "'";

                            MySqlCommand myCommandborra = new MySqlCommand(strCborraSQL, ConexionMySql);
                            //Ejecucion del comando en el servidor de BD 
                            myCommandborra.ExecuteNonQuery();
                            */
                            string strCadSQL = "INSERT INTO thodo Values ('" + ddl_periodo.SelectedValue + "','" + ddl_campus.SelectedValue + "','" +
                                 Gridthora.SelectedRow.Cells[1].Text + "','" + Gridthora.SelectedRow.Cells[3].Text + "',(select tpers_num from tpers where tpers_id='" + txt_docente.Text + "'),'','" +
                                Session["usuario"].ToString() + "',current_timestamp(),'" + ddl_estatus.SelectedValue + "')";
                            //resultado.Text = strCadSQL;
                            MySqlCommand myCommandinserta = new MySqlCommand(strCadSQL, ConexionMySql);
                            //Ejecucion del comando en el servidor de BD 
                            myCommandinserta.ExecuteNonQuery();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "save", "save();", true);
                            //txt_materia.Text = null; txt_nombre_materia.Text = null; txt_grupo.Text = null;
                            //txt_salon.Text = null; txt_nombre_salon.Text = null;
                            Global.docente = txt_docente.Text;
                            Global.nombre_docente = txt_nombre_docente.Text;
                            Carga_Disponibilidad();
                        }
                        LlenaPagina();
                    }
                    else
                    {
                        // Existe ya un docente activo
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "activo", "activo();", true);
                    }
                }
            }
            else
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "mivalidacion();", true);
                grid_grupos_bind();
            }

        }

        

        protected void cancel_thodo_Click(object sender, EventArgs e)
        {
            //combo_periodo();
            //combo_campus();
            //combo_turno();
            combo_estatus();
            //txt_materia.Text = null; txt_nombre_materia.Text = null; txt_grupo.Text = null;
            //txt_salon.Text = null; txt_nombre_salon.Text = null;
            txt_docente.Text = null; txt_nombre_docente.Text = null;

            Gridthora.Visible = false;
            GridDocentes.Visible = false;
            Gridthodo.Visible = false;
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

        protected void GridCatalogo_Click(object sender, EventArgs e)
        {
            GridViewRow row = GridDocentes.SelectedRow;

            txt_docente.Text = row.Cells[1].Text.ToString();
            txt_nombre_docente.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);

            GridDocentes.Visible = false;

        }

        private void Carga_Disponibilidad()
        {

            MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            conexion.Open();
            try
            {

                MySqlDataAdapter sqladapter = new MySqlDataAdapter();

                string StrQuerythodo = " select  tpers_id Clave, concat(tpers_nombre,' ',tpers_paterno,' ',tpers_materno) Docente, " +
                        " thodo_estatus estatus, fecha(date_format(thodo_date, '%Y-%m-%d')) fecha " +
                        " from thodo, tpers " +
                        " where thodo_tpees_clave = '" + ddl_periodo.SelectedValue + "' and thodo_tcamp_clave = '" + ddl_campus.SelectedValue + "'" +
                        " and thodo_tmate_clave = '" + Gridthora.SelectedRow.Cells[1].Text + "' and thodo_tgrup_clave = '" + Gridthora.SelectedRow.Cells[3].Text + "'" +
                        " and tpers_num = thodo_tpers_num ";
                    

                DataSet ds = new DataSet();
                MySqlDataAdapter dataadapter1 = new MySqlDataAdapter(StrQuerythodo, conexion);
                dataadapter1.Fill(ds, "Asignacion");
                Gridthodo.DataSource = ds;
                Gridthodo.DataBind();
                Gridthodo.DataMember = "Asignacion";

                if (ds.Tables[0].Rows.Count == 0)
                {
                    StrQuerythodo = " select '' Clave, '' Docente, '' estatus '', fecha ''  " +
                    " from dual ";

                    //  resultado.Text = "1--" + strQueryTelefono + "----" + strQueryTelefono;
                    DataSet ds1 = new DataSet();
                    MySqlDataAdapter dataadapter = new MySqlDataAdapter(StrQuerythodo, conexion);
                    dataadapter.Fill(ds1, "Asignacion");
                    Gridthodo.DataSource = ds1;
                    Gridthodo.DataBind();
                    Gridthodo.DataMember = "Asignacion";
                   // Gridthodo.HeaderRow.TableSection = TableRowSection.TableHeader;
                   // Gridthodo.UseAccessibleHeader = true;
                   // ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_hora1", "load_datatable_hora1();", true);

                }

                Gridthodo.Visible = true;
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
            }
            conexion.Close();
        }

        protected void Gridthodo_Click(object sender, EventArgs e)
        {
            GridViewRow row = Gridthodo.SelectedRow;

            txt_docente.Text = row.Cells[1].Text.ToString();
            txt_nombre_docente.Text = HttpUtility.HtmlDecode(row.Cells[2].Text.ToString());
            combo_estatus();
            ddl_estatus.SelectedValue=row.Cells[3].Text.ToString();
        }

        protected void Consulta_Docentes(object sender, EventArgs e)
        {
            if (GridDocentes.Visible == true)
            {
                GridDocentes.Visible = false;
            }
            {
                string strQueryDocentes = "";
                strQueryDocentes = " select tpers_id clave, concat(tpers_nombre,' ',tpers_paterno,' ',tpers_materno) docente, tcado_desc categoria, tcado_puntos " +
                              " from tpers, tdoce, tcado where tpers_tipo='D' " +
                              " and tpers_num=tdoce_tpers_num and tdoce_estatus='A' and tdoce_tcado_clave=tcado_clave and tpers_num not in " +
                              " (select thodo_tpers_num from thodo where thodo_tpees_clave='" + ddl_periodo.SelectedValue + "' " +
                              " and thodo_tcamp_clave='" + ddl_campus.SelectedValue + "' and thodo_tmate_clave = '" + Gridthora.SelectedRow.Cells[1].Text + "'" +
                              " and thodo_tgrup_clave = '" + Gridthora.SelectedRow.Cells[3].Text + "')" +
                              " order by tcado_puntos desc, docente ";


                MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                conexion.Open();
                try
                {
                    MySqlDataAdapter dataadapter = new MySqlDataAdapter(strQueryDocentes, conexion);
                    DataSet ds = new DataSet();
                    dataadapter.Fill(ds, "Docente");
                    GridDocentes.DataSource = ds;
                    GridDocentes.DataBind();
                    GridDocentes.DataMember = "Docente";


                    MySqlCommand commandsql10 = new MySqlCommand(strQueryDocentes, conexion);
                    DataSet ds1 = new DataSet();
                    dataadapter.SelectCommand = commandsql10;
                    dataadapter.Fill(ds1);
                    dataadapter.Dispose();
                    commandsql10.Dispose();

                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "NoExist", "NoExist();", true);
                    }
                    else
                    {
                        string strQueryHora = "";
                        strQueryHora = "select thora_tdias_clave, tdias_desc ,thora_thocl_inicio, a.thocl_inicia,  thora_thocl_fin ,b.thocl_fin " +
                           " from thora, thocl a, thocl b, tdias " +
                           " where thora_tpees_clave = '" + ddl_periodo.SelectedValue + "' and thora_tcamp_clave = '" + ddl_campus.SelectedValue + "' " +
                           " and thora_tgrup_clave = '" + Gridthora.SelectedRow.Cells[3].Text + "' and thora_tmate_clave = '" + Gridthora.SelectedRow.Cells[1].Text + "' " +
                           " and tdias_clave = thora_tdias_clave and a.thocl_clave = thora_thocl_inicio and b.thocl_clave = thora_thocl_fin  ";

                        DataSet dssql1 = new DataSet();

                        MySqlCommand commandsql1 = new MySqlCommand(strQueryHora, conexion);
                        dataadapter.SelectCommand = commandsql1;
                        dataadapter.Fill(dssql1);
                        dataadapter.Dispose();
                        commandsql1.Dispose();

                        if (dssql1.Tables[0].Rows.Count > 0)
                        {

                            for (int w = 0; w < ds1.Tables[0].Rows.Count; w++)
                            {

                                int contador = 0; int conta_mate = 0;
                                string strQuery = "";
                                for (int i = 0; i < dssql1.Tables[0].Rows.Count; i++)
                                {
                                    //Verifica si el Docente tiene disponibilidad para impartir la materia
                                    strQuery = " select count(*) from tdido where tdido_tpers_num in (select tpers_num from tpers where tpers_id='" +
                                        ds1.Tables[0].Rows[w][0].ToString() + "') and tdido_tdias_clave =" + dssql1.Tables[0].Rows[i][0].ToString() +
                                        " and " + dssql1.Tables[0].Rows[i][2].ToString() + " >= tdido_inicio and " + dssql1.Tables[0].Rows[i][4].ToString() +
                                        " <= tdido_fin";
                                    try
                                    {
                                        MySqlDataAdapter sqladapter = new MySqlDataAdapter();

                                        DataSet dshora = new DataSet();

                                        MySqlCommand commandsql11 = new MySqlCommand(strQuery, conexion);
                                        sqladapter.SelectCommand = commandsql11;
                                        sqladapter.Fill(dshora);
                                        sqladapter.Dispose();
                                        commandsql11.Dispose();

                                        if (dshora.Tables[0].Rows[0][0].ToString() != "0")
                                        {
                                            contador = contador + 1;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        //resultado.Text = ex.Message;
                                    }

                                    //Verifica si el Docente ha impartido la materia
                                    strQuery = " select count(*) from thodo where thodo_tpers_num in (select tpers_num from tpers where tpers_id='" +
                                        ds1.Tables[0].Rows[w][0].ToString() + "') and  thodo_tmate_clave='" + Gridthora.SelectedRow.Cells[1].Text + "' and thodo_tpees_clave != '" +
                                        ddl_periodo.SelectedValue + "'";
                                    try
                                    {
                                        MySqlDataAdapter sqladapter = new MySqlDataAdapter();

                                        DataSet dshora = new DataSet();

                                        MySqlCommand commandsql11 = new MySqlCommand(strQuery, conexion);
                                        sqladapter.SelectCommand = commandsql11;
                                        sqladapter.Fill(dshora);
                                        sqladapter.Dispose();
                                        commandsql11.Dispose();
                                        //resultado.Text = resultado.Text + strQuery + "tabla:" + dshora.Tables[0].Rows[0][0].ToString() ;
                                        if (dshora.Tables[0].Rows[0][0].ToString() != "0")
                                        {
                                            conta_mate = conta_mate + 1;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        //resultado.Text = ex.Message;
                                    }


                                }

                                if (contador == dssql1.Tables[0].Rows.Count)
                                {
                                    CheckBox disponible = (CheckBox)GridDocentes.Rows[w].FindControl("Disponibilidad");
                                    disponible.Checked = true;
                                }

                                if (conta_mate > 0)
                                {
                                    CheckBox imp = (CheckBox)GridDocentes.Rows[w].FindControl("Impartida");
                                    imp.Checked = true;
                                }
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje1", "Mensaje1();", true);
                        }
                    }
                    GridDocentes.Visible = true;
                    conexion.Close();
                }
                catch (Exception ex)
                {
                    //resultado.Text = ex.Message;
                }
            }
        }

        protected void linkBttnBusca_Click(object sender, EventArgs e)
        {
            grid_grupos_bind();
        }

        protected void ddl_campus_SelectedIndexChanged(object sender, EventArgs e)
        {
            grid_grupos_bind();

        }
    }
}