using MySql.Data.MySqlClient;
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
    public partial class tgrup : System.Web.UI.Page
    {
        #region <Variables>
        Utilidades utils = new Utilidades();
        Catalogos serviceCatalogo = new Catalogos();
        MenuService servicePermiso = new MenuService();
        CargaAcademicaService serviceDatosAcad=new CargaAcademicaService();
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
                    combo_periodo();
                    combo_campus();
                    combo_turno();
                    combo_estatus();
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_materias", "load_datatable_materias();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_salones", "load_datatable_salones();", true);

            }
        }
        private void LlenaPagina()
        {
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "menu", "carga_menu();", true);

            string QerySelect = "select tusme_update, tusme_select from tuser, tusme, tmede " +
                              " where tuser_clave = '" + Session["usuario"].ToString() + "'" +
                              " and tusme_trole_clave = tuser_trole_clave and tusme_tmenu_clave = 5 and tusme_tmenu_clave = tmede_tmenu_clave " +
                              " and tusme_tmede_clave = tmede_clave and tmede_forma='tgrup' ";

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
                    btn_tgrup.Visible = false;
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
            if (ddl_periodo.SelectedValue != "0" && ddl_turno.SelectedValue != "0" && ddl_campus.SelectedValue != "0")
            {
                //string strQueryGrup = "";
                //strQueryGrup = "select tgrup_tmate_clave clave, tmate_desc materia, tgrup_clave grupo, tgrup_tsalo_clave salon, tsalo_desc nombre_salon, " +
                //                " tgrup_capacidad capacidad, tgrup_enroll inscritos, tgrup_capacidad-tgrup_enroll disponible,  " +
                //                " tgrup_estatus c_estatus, case when tgrup_estatus='A' then 'ACTIVO' else 'INACTIVO' end estatus, " +
                //                "fecha(date_format(tgrup_date,'%Y-%m-%d')) fecha " +
                //                " from tgrup " +
                //                " left outer join tsalo on tgrup_tsalo_clave=tsalo_clave ";
                //if (ddl_programa.SelectedValue.ToString() != "0" && ddl_programa.SelectedValue.ToString() != "")
                //{
                //    strQueryGrup = strQueryGrup + " inner join tplan on tplan_tprog_clave='" + ddl_programa.SelectedValue.ToString() + "' " +
                //        " and tplan_tmate_clave=tgrup_tmate_clave ";
                //    if (ddl_per_prog.SelectedValue.ToString() != "0")
                //    {
                //        strQueryGrup = strQueryGrup + " and tplan_periodo=" + ddl_per_prog.SelectedValue.ToString();
                //    }
                //}
                //strQueryGrup = strQueryGrup +
                //" inner join tmate on tmate_clave = tgrup_tmate_clave " +
                //" where tgrup_tcamp_clave = '" + ddl_campus.SelectedValue.ToString() + "'" +
                //" and   tgrup_tpees_clave = '" + ddl_periodo.SelectedValue.ToString() + "'" +
                //" and   tgrup_turno = '" + ddl_turno.SelectedValue + "'";
                //if (txt_materia.Text != "")
                //{
                //    strQueryGrup = strQueryGrup + " and tgrup_tmate_clave='" + txt_materia.Text + "'";
                //}

                //strQueryGrup = strQueryGrup + " order by clave, grupo ";
                //MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                //ConexionMySql.Open();
                try
                {

                    //MySqlDataAdapter dataadapter = new MySqlDataAdapter(strQueryGrup, ConexionMySql);
                    //DataSet ds = new DataSet();
                    //dataadapter.Fill(ds, "Tgrup");
                    Gridtgrup.DataSource = serviceDatosAcad.ObtenerDatosGruposMateria(ddl_programa.SelectedValue, ddl_per_prog.SelectedValue.ToString(),
                        ddl_campus.SelectedValue, ddl_periodo.SelectedValue, ddl_turno.SelectedValue, txt_materia.Text);
                    Gridtgrup.DataBind();
                    //Gridtgrup.DataMember = "Tgrup";
                    //Gridtgrup.HeaderRow.TableSection = TableRowSection.TableHeader;
                    //Gridtgrup.UseAccessibleHeader = true;
                }
                catch (Exception ex)
                {
                    string test = ex.Message;
                    //Logs
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tgrup", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
                }
                //finally
                //{

                //    ConexionMySql.Close();
                //}

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_tgrup();", true);
            }
        }

        protected void Gridtgrup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = Gridtgrup.SelectedRow;
                txt_materia.Text = HttpUtility.HtmlDecode(row.Cells[1].Text);
                txt_nombre_materia.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);
                txt_grupo.Text = HttpUtility.HtmlDecode(row.Cells[3].Text);
                txt_salon.Text = HttpUtility.HtmlDecode(row.Cells[4].Text);
                txt_nombre_salon.Text = HttpUtility.HtmlDecode(row.Cells[5].Text);
                txt_capacidad.Text = HttpUtility.HtmlDecode(row.Cells[6].Text);
                txt_inscritos.Text = HttpUtility.HtmlDecode(row.Cells[7].Text);
                txt_disponibles.Text = HttpUtility.HtmlDecode(row.Cells[8].Text);

                //n_zip.Attributes.Add("readonly", "");
                combo_estatus();
                ddl_estatus.SelectedValue = row.Cells[9].Text;

                txt_materia.Attributes.Add("readonly", "");
                txt_grupo.Attributes.Add("readonly", "");

                save_tgrup.Visible = false;
                update_tgrup.Visible = true;
                grid_grupos_bind();
                Global.campus = ddl_campus.SelectedValue;
                Global.turno = ddl_turno.SelectedValue;
                Global.periodo = ddl_periodo.SelectedValue;
                Global.materia = txt_materia.Text;
                Global.nombre_materia = txt_nombre_materia.Text;
                Global.grupo = txt_grupo.Text;
                Global.salon = txt_salon.Text;
                Global.nombre_salon = txt_nombre_salon.Text;
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
        
        protected void save_tgrup_Click(object sender, EventArgs e)
        {
            if (ddl_periodo.SelectedValue != "0" && ddl_turno.SelectedValue != "0" && ddl_campus.SelectedValue != "0" && !String.IsNullOrEmpty(txt_materia.Text) && !String.IsNullOrEmpty(txt_grupo.Text) && !String.IsNullOrEmpty(txt_salon.Text) && ddl_estatus.SelectedValue != "0")
            {
                MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                ConexionMySql.Open();
                string consulta = " select tgrup_origen from tgrup where tgrup_tcamp_clave='" + ddl_campus.SelectedValue.ToString() + "' " +
                        " and tgrup_tpees_clave='" + ddl_periodo.SelectedValue.ToString() + "' and tgrup_turno='" + ddl_turno.SelectedValue + "' " +
                        " and tgrup_tmate_clave='" + txt_materia.Text + "' and tgrup_clave='" + txt_grupo.Text + "'";
                MySqlDataAdapter ad1 = new MySqlDataAdapter();

                DataSet ds = new DataSet();

                MySqlCommand comm = new MySqlCommand(consulta, ConexionMySql);
                ad1.SelectCommand = comm;
                ad1.Fill(ds);
                ad1.Dispose();
                comm.Dispose();
                string origen = "M";
                if (ds.Tables[0].Rows.Count == 0)
                {
                    string Query = "insert into tgrup values('" + ddl_periodo.SelectedValue.ToString() + "','" +
                            ddl_campus.SelectedValue.ToString() + "','" + txt_materia.Text + "','" + txt_grupo.Text + "','" + ddl_turno.SelectedValue + "','"
                            + txt_salon.Text + "'," + txt_capacidad.Text + "," + txt_inscritos.Text + ",'" + ddl_estatus.SelectedValue + "','" +
                            Session["usuario"].ToString() + "',current_timestamp(),'" + origen + "')";


                    MySqlCommand mysqlcmd = new MySqlCommand(Query, ConexionMySql);
                    mysqlcmd.CommandType = CommandType.Text;
                    try
                    {
                        mysqlcmd.ExecuteNonQuery();

                        
                        
                        
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar_z", "save();", true);
                        Global.campus = ddl_campus.SelectedValue;
                        Global.turno = ddl_turno.SelectedValue;
                        Global.periodo = ddl_periodo.SelectedValue;
                        Global.materia = txt_materia.Text;
                        Global.nombre_materia = txt_nombre_materia.Text;
                        Global.grupo = txt_grupo.Text;
                        Global.salon = txt_salon.Text;
                        Global.nombre_salon = txt_nombre_salon.Text;
                        grid_grupos_bind();
                        txt_materia.Text = null; txt_nombre_materia.Text = null; txt_grupo.Text = null;
                        txt_salon.Text = null; txt_nombre_salon.Text = null;
                        txt_capacidad.Text = null; txt_inscritos.Text = null; txt_disponibles.Text = null;
                        combo_estatus();
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
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Exist", "Exist();", true);
                    grid_grupos_bind();
                }
            }
            else
            {
                
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "mivalidacion();", true);
                grid_grupos_bind();
            }
           
        }

        protected void update_tgrup_Click(object sender, EventArgs e)
        {
            if (ddl_periodo.SelectedValue != "0" && ddl_turno.SelectedValue != "0" && ddl_campus.SelectedValue != "0" && !String.IsNullOrEmpty(txt_materia.Text) && !String.IsNullOrEmpty(txt_grupo.Text) && !String.IsNullOrEmpty(txt_salon.Text) && ddl_estatus.SelectedValue != "0")
            {
                string Query = "UPDATE tgrup SET tgrup_tsalo_clave='" + txt_salon.Text + "', tgrup_capacidad=" + txt_capacidad.Text + ", tgrup_enroll=" +
                    txt_capacidad.Text + ", tgrup_estatus='" + ddl_estatus.SelectedValue + "', tgrup_tuser_clave='" + Session["usuario"].ToString() + "', tgrup_date=current_timestamp() " +
                    " where tgrup_tcamp_clave='" + ddl_campus.SelectedValue + "' and tgrup_tpees_clave='" + ddl_periodo.SelectedValue + "'" +
                    " and   tgrup_turno='" + ddl_turno.SelectedValue + "' and tgrup_tmate_clave='" + txt_materia.Text + "' and tgrup_clave='" + txt_grupo.Text + "'"; 


                MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                ConexionMySql.Open();
                MySqlCommand mysqlcmd = new MySqlCommand(Query, ConexionMySql);
                mysqlcmd.CommandType = CommandType.Text;
                try
                {
                    mysqlcmd.ExecuteNonQuery();
                   

                    Global.campus = ddl_campus.SelectedValue;
                    Global.turno = ddl_turno.SelectedValue;
                    Global.periodo = ddl_periodo.SelectedValue;
                    Global.materia = txt_materia.Text;
                    Global.nombre_materia = txt_nombre_materia.Text;
                    Global.grupo = txt_grupo.Text;
                    Global.salon = txt_salon.Text;
                    Global.nombre_salon = txt_nombre_salon.Text;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "update_Z", "update();", true);
                    grid_grupos_bind();
                    txt_materia.Attributes.Remove("readonly");
                    txt_grupo.Attributes.Remove("readonly");
                    txt_materia.Text = null; txt_nombre_materia.Text = null; txt_grupo.Text = null;
                    txt_salon.Text = null; txt_nombre_salon.Text = null;
                    txt_capacidad.Text = null; txt_inscritos.Text = null; txt_disponibles.Text = null;
                    combo_estatus();
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "mivalidacion();", true);
                grid_grupos_bind();
            }
            }

        protected void cancel_tgrup_Click(object sender, EventArgs e)
        {
            combo_periodo();
            combo_campus();
            combo_estatus();
            

            //Gridtgrup.Visible = false;
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

        protected void Carga_Programa(object sender, EventArgs e)
        {
            Carga_Programa();
        }

        private void Carga_Programa()
        {
            MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            conexion.Open();
            Global.campus = ddl_campus.SelectedValue.ToString();
            Global.nombre_campus = ddl_campus.SelectedItem.ToString();
            string strQuerypees = "";
            strQuerypees = "select tcapr_tprog_clave clave, tprog_desc programa from tcapr, tprog " +
                            " where tcapr_tcamp_clave='" + ddl_campus.SelectedValue.ToString() + "' and tcapr_estatus='A' " +
                            " and   tcapr_tprog_clave=tprog_clave " +
                             " union " +
                             " select '0' clave, '---------' programa from dual " +
                             " order by clave ";

            try
            {
                DataTable TablaPrograma = new DataTable();
                MySqlCommand ConsultaMySql = new MySqlCommand();
                MySqlDataReader DatosMySql;
                ConsultaMySql.Connection = conexion;
                ConsultaMySql.CommandType = CommandType.Text;
                ConsultaMySql.CommandText = strQuerypees;
                DatosMySql = ConsultaMySql.ExecuteReader();
                TablaPrograma.Load(DatosMySql, LoadOption.OverwriteChanges);

                ddl_programa.DataSource = TablaPrograma;
                ddl_programa.DataValueField = "clave";
                ddl_programa.DataTextField = "programa";
                ddl_programa.DataBind();
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
            }
            conexion.Close();
        }

        protected void Carga_Perprog(object sender, EventArgs e)
        {
            MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            conexion.Open();

            string strQuerypees = "";
            strQuerypees = "select distinct tplan_periodo clave, tplan_periodo periodo from tplan " +
                            " where tplan_tprog_clave='" + ddl_programa.SelectedValue.ToString() + "' and tplan_estatus='A' " +
                             " union " +
                             " select 0 clave, '---------' periodo from dual " +
                             " order by clave ";
            Global.programa = ddl_programa.SelectedValue.ToString();
            Global.nombre_programa = ddl_programa.SelectedItem.ToString();
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

                ddl_per_prog.DataSource = TablaPeriodo;
                ddl_per_prog.DataValueField = "clave";
                ddl_per_prog.DataTextField = "periodo";
                ddl_per_prog.DataBind();
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
            }

            conexion.Close();
        }

        protected void grid_materias_bind(object sender, EventArgs e)
        {
            if (Gridtmate.Visible == true )
            {
                Gridtmate.Visible = false;
            }
            else
            {
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
            }
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "collapse1", "$('#collapseGrid').collapse('toggle')", true);

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
                    //grid_grupos_bind();

                    //TxtGrupo.Text = ""; TxtClaveSalon.Text = ""; TxtSalon.Text = "";
                    //TxtCapacidad.Text = ""; TxtInscritos.Text = ""; TxtDisponibles.Text = "";

                    //Carga_Grup();

                }
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
            }
        }

        protected void grid_salones_bind(object sender, EventArgs e)
        {
            if (Gridtsalo.Visible == true)
            {
                Gridtsalo.Visible = false;
            }
            else
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
                    Gridtsalo.DataMember = "Materia";
                    Gridtsalo.HeaderRow.TableSection = TableRowSection.TableHeader;
                    Gridtsalo.UseAccessibleHeader = true;
                    //Gridtsalo.Visible = true;
                    conexion.Close();
                    grid_grupos_bind();
                }
                catch (Exception ex)
                {
                    //resultado.Text = ex.Message;
                }
            }
        }

        protected void Gridtsalo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = Gridtsalo.SelectedRow;
                txt_salon.Text = HttpUtility.HtmlDecode(row.Cells[1].Text);
                txt_nombre_salon.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);
                txt_capacidad.Text = HttpUtility.HtmlDecode(row.Cells[4].Text);
                txt_inscritos.Text = "0";
                txt_disponibles.Text = HttpUtility.HtmlDecode(row.Cells[4].Text);

                //Gridtsalo.Visible = false;
                grid_grupos_bind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "collapse2", "$('#collapseGridTsalo').collapse('toggle')", true);

            }
            catch (Exception ex)
            {
                ///logs
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

                    // TxtGrupo.Text = ""; TxtClaveSalon.Text = ""; TxtSalon.Text = "";
                    // TxtCapacidad.Text = ""; TxtInscritos.Text = ""; TxtDisponibles.Text = "";
                    grid_grupos_bind();
                }
                else
                {
                    txt_nombre_salon.Text = dssql1.Tables[0].Rows[0][0].ToString();
                    txt_capacidad.Text = dssql1.Tables[0].Rows[0][1].ToString();
                    txt_inscritos.Text = "0";
                    txt_disponibles.Text = dssql1.Tables[0].Rows[0][1].ToString();
                    grid_grupos_bind();

                    //TxtGrupo.Text = ""; TxtClaveSalon.Text = ""; TxtSalon.Text = "";
                    //TxtCapacidad.Text = ""; TxtInscritos.Text = ""; TxtDisponibles.Text = "";

                    //Carga_Grup();

                }
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
            }
        }

        protected void linkBttnBusca_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "collapse1", "$('#collapseGrid').collapse('toggle')", true);
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
        }

        protected void linkBttnBuscaSalon_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "collapse2", "$('#collapseGridTsalo').collapse('toggle')", true);

            string strQuerySalon = "";
            strQuerySalon = " select tsalo_clave clave, tsalo_desc salon, tsalo_minimo minimo, tsalo_maximo maximo, " +
                " tsalo_tipo c_tipo, case when tsalo_tipo='T' then 'TEORICO' when tsalo_tipo='P' then 'PRACTICO' when tsalo_tipo='M' then 'MIXTO' end tipo " +
                " from tsalo where tsalo_estatus='A' ";

            strQuerySalon = strQuerySalon + " order by clave ";
            MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            conexion.Open();
            try
            {
                MySqlDataAdapter dataadapter = new MySqlDataAdapter(strQuerySalon, conexion);
                DataSet ds = new DataSet();
                dataadapter.Fill(ds, "Materia");
                Gridtsalo.DataSource = ds;
                Gridtsalo.DataBind();
                Gridtsalo.DataMember = "Materia";
                Gridtsalo.HeaderRow.TableSection = TableRowSection.TableHeader;
                Gridtsalo.UseAccessibleHeader = true;
                //Gridtsalo.Visible = true;
                conexion.Close();
                grid_grupos_bind();
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
            }
        }
    }
}