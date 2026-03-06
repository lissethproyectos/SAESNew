using MySql.Data.MySqlClient;
using SAES_DBO.Models;
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
using static SAES_DBO.Models.ModelAlumno;

namespace SAES_v1
{
    public partial class tadmi : System.Web.UI.Page
    {
        #region <Variables>
        //Utilidades utils = new Utilidades();
        AlumnoService serviceAlumno = new AlumnoService();
        List<ModelObtenPaisesResponse> lstPaises = new List<ModelObtenPaisesResponse>();
        Catalogos serviceCatalogo = new Catalogos();
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

                if (!IsPostBack)
                {
                    txt_matricula.Text = Global.cuenta;
                    txt_nombre.Text = Global.nombre + " " + Global.ap_paterno + " " + Global.ap_materno;
                    if (Global.cuenta != "")
                    {
                        //txt_matricula.ReadOnly = true;
                        txt_nombre.ReadOnly = true;
                    }

                    LlenaPagina();
                    combo_turno();
                    combo_periodo();
                    combo_campus();
                    combo_tipo_ingreso();
                    combo_tasa_financiera();
                    grid_solicitud_bind(txt_matricula.Text);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_Alumnos", "load_datatable_Alumnos();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_Escuelas", "load_datatable_Escuelas();", true);

                    //if (!string.IsNullOrEmpty(Session["matricula"] as string) && !string.IsNullOrEmpty(Session["nombre"] as string) && !string.IsNullOrEmpty(Session["periodo"] as string) && !string.IsNullOrEmpty(Session["campus"] as string) && !string.IsNullOrEmpty(Session["programa"] as string) && !string.IsNullOrEmpty(Session["tipo_ingreso"] as string) && !string.IsNullOrEmpty(Session["tasa"] as string))
                    //{

                    //    if (valida_matricula(txt_matricula.Text))
                    //    {

                    //        if (valida_solicitud(txt_matricula.Text))
                    //        {
                    //            txt_nombre.Text = nombre_alumno(txt_matricula.Text);
                    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    //            grid_solicitud_bind(txt_matricula.Text);
                    //        }
                    //        else if (txt_matricula.Text.Contains("%"))
                    //        {
                    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    //            grid_solicitud_bind(txt_matricula.Text);
                    //        }
                    //        else
                    //        {
                    //            txt_nombre.Text = nombre_alumno(txt_matricula.Text);
                    //        }

                    //    }
                    //    else
                    //    {
                    //        ///Matricula no existe
                    //    }
                    //}
                }

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

        private void LlenaPagina()
        {
            string QerySelect = "select tusme_update, tusme_select from tuser, tusme, tmede " +
                              " where tuser_clave = '" + Session["usuario"].ToString() + "'" +
                              " and tusme_trole_clave = tuser_trole_clave and tusme_tmenu_clave = 3 and tusme_tmenu_clave = tmede_tmenu_clave " +
                              " and tusme_tmede_clave = tmede_clave and tmede_forma='tadmi' ";

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
                if (dssql1.Tables[0].Rows[0][0].ToString() == "0")
                {
                    btn_tadmi.Visible = true;
                }
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tadmi", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }

            conexion.Close();
        }

        protected void combo_turno()
        {
            ddl_turno.Items.Clear();
            ddl_turno.Items.Add(new ListItem("Matutino", "M"));
            ddl_turno.Items.Add(new ListItem("Vespertino", "V"));
        }
        protected void combo_periodo()
        {
            //strQuerydire = "select tpees_clave clave, tpees_desc periodo from tpees where tpees_estatus='A' and tpees_fin >= curdate() " +
            //                "union " +
            //                "select '0' clave, '-------' periodo " +
            //                "order by 1";
          
            try
            {
                
                ddl_periodo.DataSource = serviceCatalogo.ObtenerPeriodosEscolaresVigentes();;
                ddl_periodo.DataValueField = "clave";
                ddl_periodo.DataTextField = "nombre";
                ddl_periodo.DataBind();

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tadmi", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void combo_periodo1()
        {
            string strQuerydire = "";
            strQuerydire = "select tpees_clave clave, tpees_desc periodo from tpees where tpees_estatus='A'  " +
                            "union " +
                            "select '0' clave, '-------' periodo " +
                            "order by 1";
            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            ConexionMySql.Open();
            DataTable TablaEstado = new DataTable();
            MySqlCommand ConsultaMySql = new MySqlCommand();
            MySqlDataReader DatosMySql;
            try
            {
                ConsultaMySql.Connection = ConexionMySql;
                ConsultaMySql.CommandType = CommandType.Text;
                ConsultaMySql.CommandText = strQuerydire;
                DatosMySql = ConsultaMySql.ExecuteReader();
                TablaEstado.Load(DatosMySql, LoadOption.OverwriteChanges);

                ddl_periodo.DataSource = TablaEstado;
                ddl_periodo.DataValueField = "Clave";
                ddl_periodo.DataTextField = "Periodo";
                ddl_periodo.DataBind();

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tadmi", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
            finally
            {
                ConexionMySql.Close();
            }
        }
        protected void combo_campus()
        {
            ddl_Programa.Items.Clear();
            ddl_Programa.Items.Add(new ListItem("-------", "0"));
            string strQuerydire = "";
            strQuerydire = "select tcamp_clave clave, tcamp_desc campus from tcamp where tcamp_estatus='A' " +
                            "union " +
                            "select '0' clave, '-------' campus " +
                            "order by 1";
            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            ConexionMySql.Open();
            DataTable TablaEstado = new DataTable();
            MySqlCommand ConsultaMySql = new MySqlCommand();
            MySqlDataReader DatosMySql;
            try
            {
                ConsultaMySql.Connection = ConexionMySql;
                ConsultaMySql.CommandType = CommandType.Text;
                ConsultaMySql.CommandText = strQuerydire;
                DatosMySql = ConsultaMySql.ExecuteReader();
                TablaEstado.Load(DatosMySql, LoadOption.OverwriteChanges);

                ddl_Campus.DataSource = TablaEstado;
                ddl_Campus.DataValueField = "Clave";
                ddl_Campus.DataTextField = "Campus";
                ddl_Campus.DataBind();

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tadmi", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
            finally
            {
                ConexionMySql.Close();
            }
        }
        protected void combo_Programa()
        {
            string strQuerydire = "";
            strQuerydire = "select distinct tprog_clave clave, tprog_desc programa from tcapr, tprog " +
                            " where tcapr_estatus='A' and tcapr_tcamp_clave='" + ddl_Campus.SelectedValue + "'" +
                            " and tcapr_tprog_clave=tprog_clave " +
                            "union " +
                            "select '0' clave, '-------' programa " +
                            "order by 1";
            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            ConexionMySql.Open();
            DataTable TablaEstado = new DataTable();
            MySqlCommand ConsultaMySql = new MySqlCommand();
            MySqlDataReader DatosMySql;
            try
            {
                ConsultaMySql.Connection = ConexionMySql;
                ConsultaMySql.CommandType = CommandType.Text;
                ConsultaMySql.CommandText = strQuerydire;
                DatosMySql = ConsultaMySql.ExecuteReader();
                TablaEstado.Load(DatosMySql, LoadOption.OverwriteChanges);

                ddl_Programa.DataSource = TablaEstado;
                ddl_Programa.DataValueField = "Clave";
                ddl_Programa.DataTextField = "Programa";
                ddl_Programa.DataBind();

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tadmi", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
            finally
            {
                ConexionMySql.Close();
            }
        }
        protected void combo_tipo_ingreso()
        {
            string strQuerydire = "";
            strQuerydire = "select ttiin_clave clave, ttiin_desc Tipo from ttiin where ttiin_estatus='A' " +
                            "union " +
                            "select '0' clave, '-------' Tipo " +
                            "order by 1";
            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            ConexionMySql.Open();
            DataTable TablaEstado = new DataTable();
            MySqlCommand ConsultaMySql = new MySqlCommand();
            MySqlDataReader DatosMySql;
            try
            {
                ConsultaMySql.Connection = ConexionMySql;
                ConsultaMySql.CommandType = CommandType.Text;
                ConsultaMySql.CommandText = strQuerydire;
                DatosMySql = ConsultaMySql.ExecuteReader();
                TablaEstado.Load(DatosMySql, LoadOption.OverwriteChanges);

                ddl_tipo_ingreso.DataSource = TablaEstado;
                ddl_tipo_ingreso.DataValueField = "Clave";
                ddl_tipo_ingreso.DataTextField = "Tipo";
                ddl_tipo_ingreso.DataBind();

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tadmi", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
            finally
            {
                ConexionMySql.Close();
            }
        }
        protected void combo_tasa_financiera()
        {
            string strQuerydire = "";
            strQuerydire = "select ttasa_clave clave, ttasa_desc Tasa from ttasa where ttasa_estatus='A' " +
                            "union " +
                            "select '0' clave, '-------' Tasa " +
                            "order by 1";
            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            ConexionMySql.Open();
            DataTable TablaEstado = new DataTable();
            MySqlCommand ConsultaMySql = new MySqlCommand();
            MySqlDataReader DatosMySql;
            try
            {
                ConsultaMySql.Connection = ConexionMySql;
                ConsultaMySql.CommandType = CommandType.Text;
                ConsultaMySql.CommandText = strQuerydire;
                DatosMySql = ConsultaMySql.ExecuteReader();
                TablaEstado.Load(DatosMySql, LoadOption.OverwriteChanges);

                ddl_tasa_f.DataSource = TablaEstado;
                ddl_tasa_f.DataValueField = "Clave";
                ddl_tasa_f.DataTextField = "Tasa";
                ddl_tasa_f.DataBind();

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tadmi", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
            finally
            {
                ConexionMySql.Close();
            }
        }
        protected void combo_escuelas_procedencia()
        {
            string Query = "select tespr_clave clave, tespr_desc nombre from tespr where tespr_estatus='A' ";
            if (!String.IsNullOrEmpty(txt_escuela_pro.Text))
            {
                Query = Query + "where tespr_desc like '" + txt_escuela_pro.Text + "%' or tespr_clave like '%" + txt_escuela_pro.Text + "%'";
            }
            Query = Query + " order by clave ";

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

                /*ddl_escuela_pro.DataSource = TablaEstado;
                ddl_escuela_pro.DataValueField = "clave";
                ddl_escuela_pro.DataTextField = "nombre";
                ddl_escuela_pro.DataBind();*/

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tadmi", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
            finally
            {
                ConexionMySql.Close();
            }
        }
        protected void ddl_Campus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_Campus.SelectedValue != "0")
            {
                if (valida_matricula(txt_matricula.Text))
                {

                    if (valida_solicitud(txt_matricula.Text))
                    {
                        txt_nombre.Text = nombre_alumno(txt_matricula.Text);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                        grid_solicitud_bind(txt_matricula.Text);
                    }
                    else if (txt_matricula.Text.Contains("%"))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                        grid_solicitud_bind(txt_matricula.Text);
                    }
                    else
                    {
                        txt_nombre.Text = nombre_alumno(txt_matricula.Text);
                    }

                }
                else
                {
                    ///Matricula no existe
                }
                combo_Programa();
            }

        }
        protected void grid_solicitud_bind(string matricula)
        {
            string strQueryDir = "";
            strQueryDir = "  select distinct tpers_num id_num,tpers_id clave, concat(tpers_nombre,' ',tpers_paterno,' ',tpers_materno) nombre, " +
                            "tadmi_tpees_clave periodo, tadmi_consecutivo consecutivo, tadmi_turno turno,tadmi_tpees_clave periodo, tadmi_tcamp_clave campus, " +
                            "tadmi_tprog_clave programa,tadmi_ttiin_clave tiin,tadmi_ttasa_clave tasa,tadmi_tespr_clave e_pro,tadmi_promedio promedio,tadmi_tstso_clave c_estatus,tstso_desc estatus, fecha(date_format(tadmi_date, '%Y-%m-%d')) fecha " +
                            "from(SELECT tpers_id matricula, concat(tpers_nombre, ' ', tpers_paterno, ' ', tpers_materno) alumno  from tpers where tpers_tipo = 'E') estu, tpers,tadmi " +
                            "inner join tstso on tstso_clave=tadmi_tstso_clave " +
                            "inner join tpees on tpees_clave=tadmi_tpees_clave and tpees_estatus='A' " +
                            "inner join tcamp on tcamp_clave = tadmi_tcamp_clave and tcamp_estatus = 'A' " +
                            "inner join tcapr on tcapr_tprog_clave = tadmi_tprog_clave and tcapr_estatus = 'A' " +
                            "inner join ttiin on ttiin_clave = tadmi_ttiin_clave and ttiin_estatus = 'A' " +
                            "inner join ttasa on ttasa_clave = tadmi_ttasa_clave and ttasa_estatus = 'A'" +
                            "where tpers_id = estu.matricula and tpers_num = tadmi_tpers_num and tpers_id like '" + txt_matricula.Text + "'";

            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            ConexionMySql.Open();
            try
            {

                MySqlDataAdapter dataadapter = new MySqlDataAdapter(strQueryDir, ConexionMySql);
                DataSet ds = new DataSet();
                dataadapter.Fill(ds, "Solicitud");
                GridSolicitud.DataSource = ds;
                GridSolicitud.DataBind();
                GridSolicitud.DataMember = "Solicitud";
                //GridSolicitud.HeaderRow.TableSection = TableRowSection.TableHeader;
                //GridSolicitud.UseAccessibleHeader = true;
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                GridSolicitud.Visible = true;

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tadmi", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
            finally
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);
                ConexionMySql.Close();
            }
        }
        protected void txt_matricula_TextChanged(object sender, EventArgs e)
        {
            if (valida_matricula(txt_matricula.Text))
            {

                if (valida_solicitud(txt_matricula.Text))
                {
                    txt_nombre.Text = nombre_alumno(txt_matricula.Text);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    grid_solicitud_bind(txt_matricula.Text);
                }
                else if (txt_matricula.Text.Contains("%"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    grid_solicitud_bind(txt_matricula.Text);
                }
                else
                {
                    txt_nombre.Text = nombre_alumno(txt_matricula.Text);
                }

            }
            else
            {
                ///Matricula no existe
            }
        }
        protected bool valida_matricula(string matricula)
        {
            string Query = "";
            Query = "SELECT COUNT(*) Indicador FROM tpers WHERE tpers_id Like '" + txt_matricula.Text + "'";
            MySqlCommand cmd = new MySqlCommand(Query);
            DataTable dt = GetData(cmd);
            if (dt.Rows[0]["Indicador"].ToString() == "0")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        protected string nombre_alumno(string matricula)
        {
            string nombre = "";
            string Query = "";
            Query = "SELECT tpers_num, CONCAT(tpers_nombre,' ',tpers_materno,' ',tpers_paterno) nombre, " +
                 " tpers_nombre, tpers_paterno, tpers_materno FROM tpers WHERE tpers_id = '" + txt_matricula.Text + "'";
            MySqlCommand cmd = new MySqlCommand(Query);
            DataTable dt = GetData(cmd);
            nombre = dt.Rows[0]["nombre"].ToString();
            lbl_id_pers.Text = dt.Rows[0]["tpers_num"].ToString();
            Global.cuenta = txt_matricula.Text;
            Global.nombre = dt.Rows[0]["tpers_nombre"].ToString();
            Global.ap_paterno = dt.Rows[0]["tpers_paterno"].ToString();
            Global.ap_materno = dt.Rows[0]["tpers_materno"].ToString();
            return nombre;
        }
        protected bool valida_solicitud(string matricula)
        {
            string Query = "";
            Query = "SELECT COUNT(*) Indicador FROM tadmi WHERE tadmi_tpers_num = (SELECT DISTINCT tpers_num FROM tpers WHERE tpers_id='" + txt_matricula.Text + "')";
            MySqlCommand cmd = new MySqlCommand(Query);
            DataTable dt = GetData(cmd);
            if (dt.Rows[0]["Indicador"].ToString() == "0")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        protected string consecutivo(string id_num)
        {
            string consecutivo = "";
            string Query = "";
            Query = "select IFNULL(max(tadmi_consecutivo),0)+1 consecutivo from tadmi where tadmi_tpers_num in (select tpers_num from tpers where tpers_id='" + txt_matricula.Text + "')";
            //+ " and tadmi_tprog_clave='" + ddl_Programa.SelectedValue + "' and tadmi_tpees_clave='" + ddl_periodo.SelectedValue + "' and tadmi_tcamp_clave='" + ddl_Campus.SelectedValue + "'";
            MySqlCommand cmd = new MySqlCommand(Query);
            DataTable dt = GetData(cmd);
            consecutivo = dt.Rows[0]["consecutivo"].ToString();
            return consecutivo;
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {

            btn_save.Visible = true;
            btn_update.Visible = false;
            //txt_matricula.Text = null;
            //txt_nombre.Text = null;
            //txt_matricula.ReadOnly = false;
            txt_nombre.ReadOnly = false;
            combo_turno();
            combo_periodo();
            combo_campus();
            combo_tipo_ingreso();
            combo_tasa_financiera();

            txt_escuela_pro.Text = null;
            txt_nom_esc.Text = null;
            txt_promedio.Text = null;
            txt_estatus_sol.Visible = false;
            GridSolicitud.Visible = false;
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {

            if (ddl_periodo.SelectedValue != "0" && ddl_Campus.SelectedValue != "0" && ddl_Programa.SelectedValue != "0" && ddl_tipo_ingreso.SelectedValue != "0" && ddl_tasa_f.SelectedValue != "0" && !String.IsNullOrEmpty(txt_escuela_pro.Text) && !String.IsNullOrEmpty(txt_promedio.Text) && ddl_turno.SelectedValue != "0")
            {

                string Query = "INSERT INTO tadmi Values ((select tpers_num from tpers where tpers_id='" + txt_matricula.Text + "'),'" + ddl_periodo.SelectedValue.ToString() + "'," + consecutivo(lbl_id_pers.Text) + ",'" + ddl_turno.SelectedValue + "','" +
                         ddl_Programa.SelectedValue.ToString() + "','" + ddl_tipo_ingreso.SelectedValue.ToString() + "','IN','" +
                         ddl_Campus.SelectedValue.ToString() + "','" + txt_escuela_pro.Text + "','" + txt_promedio.Text + "','" + ddl_tasa_f.SelectedValue.ToString() +
                         "',current_timestamp(), current_timestamp(),'" + Session["usuario"].ToString() + "')";



                MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                conexion.Open();
                MySqlCommand mysqlcmd = new MySqlCommand(Query, conexion);
                mysqlcmd.CommandType = CommandType.Text;



                try
                {
                    mysqlcmd.ExecuteNonQuery();

                    //Agrega la abreviatura del campus
                    string strQueryAbr = " select tcamp_abr from tcamp where tcamp_clave='" + ddl_Campus.SelectedValue.ToString() + "'";
                    DataSet dsabr = new DataSet();
                    MySqlDataAdapter sqladapter = new MySqlDataAdapter();
                    MySqlCommand commandsqlabr = new MySqlCommand(strQueryAbr, conexion);
                    sqladapter.SelectCommand = commandsqlabr;
                    sqladapter.Fill(dsabr);
                    sqladapter.Dispose();
                    commandsqlabr.Dispose();
                    if (txt_matricula.Text.Substring(0, dsabr.Tables[0].Rows[0][0].ToString().Length) != dsabr.Tables[0].Rows[0][0].ToString())
                    {
                        string strQueryInterno = " select tpers_num from tpers where tpers_id='" + txt_matricula.Text + "' ";
                        DataSet dssql = new DataSet();
                        try
                        {
                            MySqlCommand commandsql = new MySqlCommand(strQueryInterno, conexion);
                            sqladapter.SelectCommand = commandsql;
                            sqladapter.Fill(dssql);
                            sqladapter.Dispose();
                            commandsql.Dispose();
                        }
                        catch (Exception ex)
                        {
                            // resultado.Text = ex.Message;
                        }
                        double pidm;
                        pidm = Convert.ToDouble(dssql.Tables[0].Rows[0][0].ToString());
                        string strId = " update tpers set tpers_id=concat('" + dsabr.Tables[0].Rows[0][0].ToString() + "',tpers_id)" +
                            " where tpers_num =" + pidm;
                        MySqlCommand myCommandId = new MySqlCommand(strId, conexion);
                        //Ejecucion del comando en el servidor de BD
                        myCommandId.ExecuteNonQuery();
                        txt_matricula.Text = dsabr.Tables[0].Rows[0][0].ToString() + txt_matricula.Text;
                        string TablaAlumno = "INSERT INTO alumno Values ('" + txt_matricula.Text + "',(select concat(tpers_nombre,' ',tpers_paterno,' ',tpers_materno) from tpers where tpers_id='" + txt_matricula.Text + "'),'',7,10,'" +
                       ddl_Campus.SelectedValue.ToString() + "','',(select tprog_tnive_clave from tprog where tprog_clave='" + ddl_Programa.SelectedValue.ToString() + "'),'" +
                       ddl_Programa.SelectedValue.ToString() + "','','N','" + ddl_tipo_ingreso.SelectedValue.ToString() + "',(select tprog_tmoda_clave from tprog where tprog_clave='" + ddl_Programa.SelectedValue.ToString() +
                       "'),'AC','Activo','" + ddl_periodo.SelectedValue.ToString() + "',null,0,0,curdate(),1,null,curdate())";
                        MySqlCommand mysqlAlumno = new MySqlCommand(TablaAlumno, conexion);
                        mysqlAlumno.CommandType = CommandType.Text;
                        mysqlAlumno.ExecuteNonQuery();

                        conexion.Close();
                    }

                    combo_turno();
                    combo_periodo();
                    combo_campus();
                    combo_Programa();
                    combo_tipo_ingreso();
                    combo_tasa_financiera();

                    txt_escuela_pro.Text = null;
                    txt_escuela_pro.Visible = true;
                    grid_solicitud_bind(txt_matricula.Text);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                }
                catch (Exception ex)
                {
                    string test = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tadmi", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                }
            }
            else
            {
                if (!String.IsNullOrEmpty(txt_matricula.Text))
                {
                    grid_solicitud_bind(txt_matricula.Text);
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_solicitud();", true);
            }
        }

        protected void btn_update_Click(object sender, EventArgs e)
        {

            if (ddl_periodo.SelectedValue != "0" && ddl_Campus.SelectedValue != "0" && ddl_Programa.SelectedValue != "0" && ddl_tipo_ingreso.SelectedValue != "0" && ddl_tasa_f.SelectedValue != "0" && !String.IsNullOrEmpty(txt_escuela_pro.Text) && !String.IsNullOrEmpty(txt_promedio.Text) && ddl_turno.SelectedValue != "0" && txt_promedio.Text != null)
            {

                string Query = "UPDATE tadmi SET tadmi_turno='" + ddl_turno.SelectedValue + "',tadmi_tprog_clave='" + ddl_Programa.SelectedValue + "',tadmi_ttiin_clave='" + ddl_tipo_ingreso.SelectedValue + "',tadmi_tcamp_clave='" + ddl_Campus.SelectedValue + "',tadmi_tespr_clave='" + txt_escuela_pro.Text + "',tadmi_promedio='" + txt_promedio.Text + "',tadmi_ttasa_clave='" + ddl_tasa_f.SelectedValue + "',tadmi_date=current_timestamp(),tadmi_user='" + Session["usuario"].ToString() + "'  WHERE tadmi_tpers_num in (select tpers_num from tpers where tpers_id='" + txt_matricula.Text + "') AND tadmi_tpees_clave='" + ddl_periodo.SelectedValue + "' AND tadmi_consecutivo='" + lbl_consecutivo.Text + "'";

                MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                conexion.Open();
                MySqlCommand mysqlcmd = new MySqlCommand(Query, conexion);
                mysqlcmd.CommandType = CommandType.Text;
                try
                {
                    mysqlcmd.ExecuteNonQuery();
                    grid_solicitud_bind(txt_matricula.Text);
                    GridSolicitud.SelectedIndex = -1;
                    ddl_periodo.SelectedIndex = 0;
                    ddl_Campus.SelectedIndex = 0;
                    ddl_tipo_ingreso.SelectedIndex = 0;
                    ddl_tasa_f.SelectedIndex = 0;
                    ddl_Programa.SelectedIndex = 0;
                    
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "update_p", "update();", true);
                }
                catch (Exception ex)
                {
                    string test = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tadmi", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                }
                finally
                {
                    conexion.Close();
                }
            }
            else
            {
                if (!String.IsNullOrEmpty(txt_matricula.Text))
                {
                    grid_solicitud_bind(txt_matricula.Text);
                }
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_solicitud();", true);
            }
        }


        protected void buscar_Click(object sender, EventArgs e)
        {

            combo_escuelas_procedencia();
        }

        protected void cancelar_Click(object sender, EventArgs e)
        {


        }

        protected void GridSolicitud_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                btn_save.Visible = false;
                btn_update.Visible = true;
                GridViewRow row = GridSolicitud.SelectedRow;
                txt_matricula.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);
                txt_nombre.Text = HttpUtility.HtmlDecode(row.Cells[3].Text);
                combo_periodo1();
                ddl_periodo.SelectedValue = row.Cells[4].Text;
                combo_turno();
                ddl_turno.SelectedValue = row.Cells[6].Text;
                combo_campus();
                ddl_Campus.SelectedValue = row.Cells[7].Text;
                combo_Programa();
                ddl_Programa.SelectedValue = row.Cells[8].Text;
                combo_tipo_ingreso();
                ddl_tipo_ingreso.SelectedValue = row.Cells[9].Text;
                combo_tasa_financiera();
                ddl_tasa_f.SelectedValue = row.Cells[10].Text;
                txt_estatus_sol.Text = row.Cells[14].Text;
                txt_estatus_sol.Attributes.Add("readonly", "");
                txt_estatus_sol.Visible = true;
                combo_escuelas_procedencia();
                txt_escuela_pro.Text = row.Cells[11].Text;
                txt_promedio.Text = row.Cells[12].Text;
                lbl_consecutivo.Text = row.Cells[5].Text;
                string QerySelect = "select tespr_desc from tespr where tespr_clave='" + txt_escuela_pro.Text + "'";

                MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                conexion.Open();
                MySqlDataAdapter sqladapter = new MySqlDataAdapter();

                DataSet dssql1 = new DataSet();

                MySqlCommand commandsql1 = new MySqlCommand(QerySelect, conexion);
                sqladapter.SelectCommand = commandsql1;
                sqladapter.Fill(dssql1);
                sqladapter.Dispose();
                commandsql1.Dispose();
                txt_nom_esc.Text = dssql1.Tables[0].Rows[0][0].ToString();
                conexion.Close();
                grid_solicitud_bind(txt_matricula.Text);
            }
            catch (Exception EX)
            {
                string TEST = EX.Message;
            }
        }

        protected void simulador_Click(object sender, EventArgs e)
        {
            if (ddl_periodo.SelectedValue != "0" && ddl_Campus.SelectedValue != "0" && ddl_Programa.SelectedValue != "0" && ddl_tipo_ingreso.SelectedValue != "0" && ddl_tasa_f.SelectedValue != "0" && !String.IsNullOrEmpty(txt_escuela_pro.Text) && !String.IsNullOrEmpty(txt_promedio.Text) && ddl_turno.SelectedValue != "0")
            {
                Global.cuenta = txt_matricula.Text;
                Global.nombre = txt_nombre.Text;
                Global.periodo = ddl_periodo.SelectedValue;
                Global.campus = ddl_Campus.SelectedValue;
                Global.programa = ddl_Programa.SelectedValue;
                Global.tipo_ingreso = ddl_tipo_ingreso.SelectedValue;
                Global.tasa = ddl_tasa_f.SelectedValue;
                Global.procedencia = txt_nom_esc.Text;
                Global.promedio = txt_promedio.Text;
                elimina_simulador();
                inserta_simulador();
                Response.Redirect("tsimu.aspx");
            }
            else
            {
                if (!String.IsNullOrEmpty(txt_matricula.Text))
                {
                    grid_solicitud_bind(txt_matricula.Text);
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_solicitud();", true);
            }
        }

        protected void elimina_simulador()
        {
            string Query_Delete = "delete from  tsimu where tsimu_id='" + txt_matricula.Text + "'";
            MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            conexion.Open();
            MySqlCommand mysqlcmd = new MySqlCommand(Query_Delete, conexion);
            mysqlcmd.CommandType = CommandType.Text;
            try
            {
                mysqlcmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tadmi", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
            }
            finally
            {
                conexion.Close();
            }
        }

        protected void inserta_simulador()
        {
            string QueryTcomb = " select distinct tcomb_ttasa_clave, tcomb_tcoco_clave from tcomb " +
                " where tcomb_ttasa_clave='" + ddl_tasa_f.SelectedValue + "'";
            MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            conexion.Open();
            try
            {
                MySqlDataAdapter sqladapter = new MySqlDataAdapter();
                DataSet dssql1 = new DataSet();
                MySqlCommand commandsql1 = new MySqlCommand(QueryTcomb, conexion);
                sqladapter.SelectCommand = commandsql1;
                sqladapter.Fill(dssql1);
                sqladapter.Dispose();
                commandsql1.Dispose();

                for (int i = 0; i < dssql1.Tables[0].Rows.Count; i++)
                {
                    string QueryTcuot = " select tcomb_ttasa_clave, tcomb_tcoco_clave, tcuot_importe , tcoco_ind_parc, tcuot_tcamp_clave, " +
                        " tcuot_tnive_clave,tcuot_tcole_clave, tcuot_tmoda_clave, tcuot_tprog_clave, tcuot_ttiin_clave " +
                        " from tcomb,tcuot,tprog, tcoco " +
                        " where tcomb_ttasa_clave = '" + ddl_tasa_f.SelectedValue + "' and tcomb_tcoco_clave = '" + dssql1.Tables[0].Rows[i][1].ToString() + "'" +
                        " and tcomb_tcoco_clave = tcuot_tcoco_clave and tprog_clave='" + ddl_Programa.SelectedValue + "'" +
                        " and(tcuot_tcamp_clave = '" + ddl_Campus.SelectedValue + "' or tcuot_tcamp_clave = '000') " +
                        " and(tcuot_tnive_clave = tprog_tnive_clave or tcuot_tnive_clave = '000') " +
                        " and(tcuot_tcole_clave = tprog_tcole_clave or tcuot_tcole_clave = '000') " +
                        " and(tcuot_tmoda_clave = tprog_tmoda_clave or tcuot_tmoda_clave = '000') " +
                        " and(tcuot_tprog_clave = tprog_clave or tcuot_tprog_clave = '0000000000') " +
                        " and(tcuot_ttiin_clave = '" + ddl_tipo_ingreso.SelectedValue + "' or tcuot_ttiin_clave = '000') " +
                        " and tcuot_estatus = 'A' and curdate() between tcuot_inicio and tcuot_fin and tcoco_clave = tcuot_tcoco_clave " +
                        " order by tcomb_ttasa_clave,tcomb_tcoco_clave,tcuot_tcamp_clave desc, tcuot_tnive_clave desc,tcuot_tcole_clave desc, " +
                        " tcuot_tmoda_clave desc,tcuot_tprog_clave desc,  tcuot_ttiin_clave desc";
                    //resultado.Text = resultado.Text + "----" + QueryTcuot;

                    MySqlDataAdapter sqladapter1 = new MySqlDataAdapter();

                    DataSet dssql11 = new DataSet();

                    MySqlCommand commandsql11 = new MySqlCommand(QueryTcuot, conexion);
                    sqladapter1.SelectCommand = commandsql11;
                    sqladapter1.Fill(dssql11);
                    sqladapter1.Dispose();
                    commandsql11.Dispose();

                    for (int w = 0; w < dssql11.Tables[0].Rows.Count; w++)
                    {
                        if (w == 0)
                        {
                            string Queryinserta = " insert into tsimu values ('" + txt_matricula.Text + "','" + ddl_tasa_f.SelectedValue + "','" +
                             dssql1.Tables[0].Rows[i][1].ToString() + "'," + Convert.ToDouble(dssql11.Tables[0].Rows[w][2].ToString()) + ")";
                            //resultado.Text = resultado.Text + "---" + Queryinserta;
                            MySqlCommand myCommandinserta = new MySqlCommand(Queryinserta, conexion);
                            myCommandinserta.ExecuteNonQuery();
                        }
                    }
                    //resultado.Text = QueryTcuot;
                }

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tadmi", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
            }
            finally
            {
                conexion.Close();
            }

        }

        protected void grid_alumnos_bind(object sender, EventArgs e)
        {
            if (GridAlumnos.Visible == true)
            {
                GridAlumnos.Visible = false;
            }
            else
            {
                string QueryEstudiantes = "select distinct tpers_id clave, tpers_nombre nombre, tpers_paterno paterno, tpers_materno materno, tpers_num pidm,  tpers_genero c_genero, CASE WHEN tpers_genero = 'F' THEN 'Femenino' WHEN tpers_genero = 'M' THEN 'Masculino' ELSE 'No Aplica' END genero, " +
                                           "tpers_edo_civ c_civil, CASE WHEN tpers_edo_civ = 'C' THEN 'Casado' WHEN tpers_edo_civ = 'S' THEN 'Soltero' WHEN tpers_edo_civ = 'V' THEN 'Viudo' WHEN tpers_edo_civ = 'U' THEN 'Union Libre' WHEN tpers_edo_civ = 'D' THEN 'Divorciado' ELSE 'No Aplica' END e_civil, tpers_curp curp, date_format(tpers_fecha_nac, ' %d/%m/%Y') fecha, tpers_usuario usuario, fecha(date_format(tpers_date, '%Y-%m-%d')) fecha_reg " +
                                            "from tpers " +
                                             "where tpers_tipo = 'E'";
                MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                conexion.Open();
                try
                {
                    MySqlDataAdapter dataadapter = new MySqlDataAdapter(QueryEstudiantes, conexion);
                    DataSet ds = new DataSet();
                    dataadapter.Fill(ds, "Solicitudes");
                    GridAlumnos.DataSource = ds;
                    GridAlumnos.DataBind();
                    GridAlumnos.DataMember = "Solicitudes";
                    GridAlumnos.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridAlumnos.UseAccessibleHeader = true;

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_Alumnos", "load_datatable_Alumnos();", true);
                    GridAlumnos.Visible = true;
                }
                catch (Exception ex)
                {
                    //logs
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tadmi", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                }
                conexion.Close();
            }
        }

        protected void grid_escuelas_bind(object sender, EventArgs e)
        {
            if (GridEscuelas.Visible == true)
            {
                GridEscuelas.Visible = false;
            }
            else
            {
                string QueryEscuelas = "select tespr_clave clave, tespr_desc nombre from tespr " +
                                             "order by clave ";
                MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                conexion.Open();
                try
                {
                    MySqlDataAdapter dataadapter = new MySqlDataAdapter(QueryEscuelas, conexion);
                    DataSet ds = new DataSet();
                    dataadapter.Fill(ds, "Escuelas");
                    GridEscuelas.DataSource = ds;
                    GridEscuelas.DataBind();
                    GridEscuelas.DataMember = "Escuelas";
                    GridEscuelas.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridEscuelas.UseAccessibleHeader = true;

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_Escuelas", "load_datatable_Escuelas();", true);
                    GridEscuelas.Visible = true;
                }
                catch (Exception ex)
                {
                    //logs
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tadmi", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                }
                conexion.Close();
            }
        }

        protected void GridAlumnos_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridAlumnos.SelectedRow;
            txt_matricula.Text = row.Cells[1].Text;
            txt_nombre.Text = HttpUtility.HtmlDecode(row.Cells[2].Text) + " " + HttpUtility.HtmlDecode(row.Cells[3].Text) + " " +
             HttpUtility.HtmlDecode(row.Cells[4].Text);
            btn_save.Visible = false;
            btn_update.Visible = true;
            GridAlumnos.Visible = false;
            txt_matricula.Attributes.Add("readonly", "");
            btn_save.Visible = false;
            btn_update.Visible = true;
            Global.cuenta = txt_matricula.Text;
            Global.nombre = HttpUtility.HtmlDecode(row.Cells[2].Text);
            Global.ap_paterno = HttpUtility.HtmlDecode(row.Cells[3].Text);
            Global.ap_materno = HttpUtility.HtmlDecode(row.Cells[4].Text);
            grid_solicitud_bind(txt_matricula.Text);
        }

        protected void GridEscuelas_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridEscuelas.SelectedRow;
            txt_escuela_pro.Text = row.Cells[1].Text;
            txt_nom_esc.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);
            GridEscuelas.Visible = false;
        }

        protected void consulta_docs_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Tcadm.aspx");
        }

        protected void txt_promedio_TextChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_valor_entero();", true);

        }

        protected void linkBttnBuscaEscuela_Click(object sender, EventArgs e)
        {
            if (GridEscuelas.Visible == true)
            {
                GridEscuelas.Visible = false;
            }
            else
            {
                string QueryEscuelas = "select tespr_clave clave, tespr_desc nombre from tespr " +
                                             "order by clave ";
              
                try
                {
                 
                    GridEscuelas.DataSource = serviceCatalogo.ObtenerGridEscuelasProcedencia();
                    GridEscuelas.DataBind();
                    GridEscuelas.UseAccessibleHeader = true;

                    GridEscuelas.Visible = true;
                }
                catch (Exception ex)
                {
                    //logs
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tadmi", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                }
            }
        }

        protected void linkBttnBusca_Click(object sender, EventArgs e)
        {
            try
            {                
                    Carga_Estudiante();
            }
            catch (Exception ex)
            {
                //Logs
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tadmi", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }
        protected void Carga_Estudiante()
        {
            ModelObtenerAlumnosResponse datosAlumno = new ModelObtenerAlumnosResponse();
            txt_nombre.Text = string.Empty;
            DataTable dtAlumno = new DataTable();


            try
            {
                dtAlumno = serviceAlumno.ObtenerAlumnos(txt_matricula.Text);

                if (dtAlumno.Rows.Count == 1)
                {
                    txt_nombre.Text = dtAlumno.Rows[0][1].ToString() + " " + dtAlumno.Rows[0][2].ToString() + " " + dtAlumno.Rows[0][3].ToString();
                    Global.cuenta = txt_matricula.Text;
                    Global.nombre = dtAlumno.Rows[0][1].ToString() + " " + dtAlumno.Rows[0][2].ToString() + " " + dtAlumno.Rows[0][3].ToString();
                    grid_solicitud_bind(txt_matricula.Text);

                    //grid_direccion_bind(txt_matricula.Text);
                }
                else if (dtAlumno.Rows.Count > 1)
                {
                    GridAlumnos.DataSource = dtAlumno;
                    GridAlumnos.DataBind();
                    GridAlumnos.Visible = true;
                }
                else
                {

                    txt_matricula.Text = "";
                    txt_nombre.Text = "";
                    txt_matricula.Focus();

                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "NoexisteAlumno", "NoexisteAlumno();", true);
                }
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tadmi", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }

            //Estudiantes.Visible = true;
            //txt_matricula.Focus();

        }

    }
}