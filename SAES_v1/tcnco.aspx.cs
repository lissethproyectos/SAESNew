using MySql.Data.MySqlClient;
using SAES_DBO.Models;
using SAES_Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using static SAES_DBO.Models.ModelAlumno;
using static SAES_DBO.Models.ModelMenu;

namespace SAES_v1
{
    public partial class tcnco : System.Web.UI.Page
    {
        #region <Variables>
        //Utilidades utils = new Utilidades();
        AlumnoService serviceAlumno = new AlumnoService();
        CobranzaService serviceCobranza = new CobranzaService();
        ContactoService serviceContacto = new ContactoService();
        List<ModelObtenPaisesResponse> lstPaises = new List<ModelObtenPaisesResponse>();
        Catalogos serviceCatalogo = new Catalogos();
        public string id_num = null;
        MenuService servicePermiso = new MenuService();
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
                    if (txt_matricula.Text != null)
                    {
                        combo_contacto();
                    }
                    LlenaPagina();
                    combo_estatus();
                    combo_tipo_correo();
                    grid_correo_bind(txt_matricula.Text);
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_Alumnos", "load_datatable_Alumnos();", true);
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

        //private void LlenaPagina()
        //{
        //    string QerySelect = "select tusme_update, tusme_select from tuser, tusme, tmede " +
        //                      " where tuser_clave = '" + Session["usuario"].ToString() + "'" +
        //                      " and tusme_trole_clave = tuser_trole_clave and tusme_tmenu_clave = 3 and tusme_tmenu_clave = tmede_tmenu_clave " +
        //                      " and tusme_tmede_clave = tmede_clave and tmede_forma='tcnco' ";

        //    MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
        //    conexion.Open();
        //    try
        //    {
        //        MySqlDataAdapter sqladapter = new MySqlDataAdapter();

        //        DataSet dssql1 = new DataSet();

        //        MySqlCommand commandsql1 = new MySqlCommand(QerySelect, conexion);
        //        sqladapter.SelectCommand = commandsql1;
        //        sqladapter.Fill(dssql1);
        //        sqladapter.Dispose();
        //        commandsql1.Dispose();
        //        if (dssql1.Tables[0].Rows.Count == 0 || dssql1.Tables[0].Rows[0][1].ToString() == "0")
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
        //        }
        //        if (dssql1.Tables[0].Rows[0][0].ToString() == "0")
        //        {
        //            btn_tcnco.Visible = false;
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        string mensaje_error = ex.Message.Replace("'", "-");
        //        Global.inserta_log(mensaje_error, "tcnco", Session["usuario"].ToString());
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
        //    }
        //    conexion.Close();
        //}

        private void LlenaPagina()
        {
            ModelObtenerPermisoFormResponse objPermiso = new ModelObtenerPermisoFormResponse();
            objPermiso.usme_update = "0";
            objPermiso.usme_select = "0";
            System.Threading.Thread.Sleep(50);
            try
            {
                objPermiso = servicePermiso.ObtenerPermisoFormulario(Session["usuario"].ToString(), "tcnco");
                if (objPermiso != null)
                {
                    if (objPermiso.usme_update == "0" || objPermiso.usme_select == "0")
                    {
                        btn_tcnco.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                    }
                    //else
                    //    grid_telefono_bind(txt_matricula);
                }
                else
                {
                    btn_tcnco.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                }


            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcnco", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);

            }
        }


        protected void combo_contacto()
        {
            //strQuerytcont = "select tcoda_consecutivo clave, tcont_desc contacto from tcoda, tcont where tcont_estatus='A'  " +
            //                " and tcoda_tpers_num in (select tpers_num from tpers where tpers_id='" + txt_matricula.Text + "') and tcoda_tipo=tcont_clave " +
            //                "union " +
            //                "select '0' clave, '-------' contacto " +
            //                "order by 1";           
            try
            {
               
                ddl_tcont.DataSource = serviceAlumno.ObtenerEMailContactos(txt_matricula.Text);
                ddl_tcont.DataValueField = "Clave";
                ddl_tcont.DataTextField = "contacto";
                ddl_tcont.DataBind();

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcnco", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void combo_tipo_correo()
        {
            //strQuerydire = "select tmail_clave clave, tmail_desc correo from tmail where tmail_estatus='A' " +
            //                "union " +
            //                "select '0' clave, '-------' correo " +
            //                "order by 1";
           
            try
            {
                ddl_tipo_correo.DataSource =serviceCatalogo.obtenEMailActivos();
                ddl_tipo_correo.DataValueField = "clave";
                ddl_tipo_correo.DataTextField = "nombre";
                ddl_tipo_correo.DataBind();
            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcnco", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
          
        }
        protected void combo_estatus()
        {
            ddl_estatus.Items.Clear();
            ddl_estatus.Items.Add(new ListItem("Activo", "A"));
            ddl_estatus.Items.Add(new ListItem("Inactivo", "B"));
        }

        protected void grid_correo_bind(string matricula)
        {
            string strQueryDir = "";
            strQueryDir = "  select tpers_num id_num, " +
                            "tcnco_tmail_clave tipo_mail, tmail_desc descripcion, tcnco_consec consecutivo, tcnco_correo correo, tcnco_preferido preferido, " +
                            "tcnco_estatus c_estatus, case when tcnco_estatus = 'A' then 'Activo' else 'Inactivo' end estatus, fecha(date_format(tcnco_date, '%Y-%m-%d')) fecha, " +
                            " tcnco_tcoda_consec cl_contacto, tcont_desc contacto " +
                            "from(SELECT tpers_id matricula, concat(tpers_nombre, ' ', tpers_paterno, ' ', tpers_materno) alumno  from tpers where tpers_tipo = 'E') estu, tcnco, tpers,tmail, tcoda, tcont " +
                            "where tpers_id = estu.matricula and tpers_num = tcnco_tpers_num and tmail_clave = tcnco_tmail_clave and tpers_id like '" + matricula + "' and tcoda_tpers_num=tcnco_tpers_num " +
                            " and tcoda_consecutivo=tcnco_tcoda_consec and tcoda_tipo=tcont_clave ";

            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            ConexionMySql.Open();
            try
            {

                MySqlDataAdapter dataadapter = new MySqlDataAdapter(strQueryDir, ConexionMySql);
                DataSet ds = new DataSet();
                dataadapter.Fill(ds, "Correo");
                GridCorreo.DataSource = ds;
                GridCorreo.DataBind();
                GridCorreo.DataMember = "Correo";
                GridCorreo.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridCorreo.UseAccessibleHeader = true;
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                GridCorreo.Visible = true;

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcnco", Session["usuario"].ToString());
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
            Carga_Estudiante();

            //if (valida_matricula(txt_matricula.Text))
            //{

            //    if (valida_telefono(txt_matricula.Text))
            //    {
            //        txt_nombre.Text = nombre_alumno(txt_matricula.Text);
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
            //        grid_correo_bind(txt_matricula.Text);
            //    }
            //    else if (txt_matricula.Text.Contains("%"))
            //    {
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
            //        grid_correo_bind(txt_matricula.Text);
            //    }
            //    else
            //    {
            //        txt_nombre.Text = nombre_alumno(txt_matricula.Text);
            //    }

            //}
            //else
            //{
            //    ///Matricula no existe
            //}
        }

        protected bool valida_matricula(string matricula)
        {
            string Query = "";
            Query = "SELECT COUNT(*) Indicador FROM tpers WHERE tpers_id Like '" + matricula + "'";
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

        protected bool valida_telefono(string matricula)
        {
            string Query = "";
            Query = "SELECT COUNT(*) Indicador FROM tcnco WHERE tcnco_tpers_num = (SELECT DISTINCT tpers_num FROM tpers WHERE tpers_id='" + matricula + "')";
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
                 " tpers_nombre, tpers_paterno, tpers_materno FROM tpers WHERE tpers_id = '" + matricula + "'";
            try
            {
                MySqlCommand cmd = new MySqlCommand(Query);
                DataTable dt = GetData(cmd);
                nombre = dt.Rows[0]["nombre"].ToString();
                lbl_id_pers.Text = dt.Rows[0]["tpers_num"].ToString();
                Global.cuenta = txt_matricula.Text;
                Global.nombre = dt.Rows[0]["tpers_nombre"].ToString();
                Global.ap_paterno = dt.Rows[0]["tpers_paterno"].ToString();
                Global.ap_materno = dt.Rows[0]["tpers_materno"].ToString();
            }
            catch(Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcnco", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
            return nombre;
        }

        protected string consecutivo(string id_num)
        {
            string consecutivo = "";
            string Query = "";
            Query = "select IFNULL(max(tcnco_consec),0)+1 consecutivo from tcnco where tcnco_tpers_num in (select tpers_num from tpers where tpers_id='" + txt_matricula.Text + "') and tcnco_tmail_clave='" + ddl_tipo_correo.SelectedValue + "'";
            try
            {
                MySqlCommand cmd = new MySqlCommand(Query);
                DataTable dt = GetData(cmd);
                consecutivo = dt.Rows[0]["consecutivo"].ToString();
            }
            catch(Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcnco", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
            
            return consecutivo;
        }

        protected void GridCorreo_SelectedIndexChanged(object sender, EventArgs e)
        {
            btn_save.Visible = false;
            btn_update.Visible = true;
            btn_cancel.Visible = true;
            GridViewRow row = GridCorreo.SelectedRow;
            combo_tipo_correo();
            ddl_tipo_correo.SelectedValue = row.Cells[2].Text;
            combo_contacto();
            if(ddl_tcont.Items.Count> 0)
                ddl_tcont.SelectedValue = row.Cells[10].Text;

            lbl_consecutivo.Text = row.Cells[4].Text;
            txt_correo.Text = row.Cells[5].Text;
            if (row.Cells[6].Text == "S")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "activar_check", "activar_check();", true);
            }
            combo_estatus();
            ddl_estatus.SelectedValue = row.Cells[7].Text;
            ddl_tipo_correo.Enabled = false;
            //ddl_tipo_correo.Attributes.Add("disabled", "");
            grid_correo_bind(txt_matricula.Text);
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {

            btn_save.Visible = true;
            btn_update.Visible = false;
            btn_cancel.Visible = false;
            //txt_matricula.Text = null;
            //txt_nombre.Text = null;
            //txt_matricula.ReadOnly = false;
            combo_tipo_correo();
            combo_estatus();
            combo_contacto();
            txt_correo.Text = null;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "desactivar_check", "desactivar_check();", true);
            ddl_tipo_correo.Enabled = true;

            //ddl_tipo_correo.Attributes.Remove("disabled");
            GridCorreo.SelectedIndex = -1;
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            string pattern_mail = @"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$";
            Regex mail = new Regex(pattern_mail);
            //var test_1 = mail.IsMatch(txt_correo.Text);
            if (!String.IsNullOrEmpty(txt_matricula.Text) && !String.IsNullOrEmpty(txt_nombre.Text) && ddl_tipo_correo.SelectedValue != "0" && ddl_tcont.SelectedValue != "0"  && mail.IsMatch(txt_correo.Text))
            {
                string preferido = "";
                string selected = Request.Form["customSwitches"];
                if (selected == "on") { preferido = "S"; } else { preferido = "N"; }
                string Query = "INSERT INTO tcnco  VALUES ((select tpers_num from tpers where tpers_id='" + txt_matricula.Text + "')" +
                               "," + ddl_tcont.SelectedValue + "," +  consecutivo(lbl_id_pers.Text) + ",'" + ddl_tipo_correo.SelectedValue + "','" + txt_correo.Text + "','" + preferido + "','" + ddl_estatus.SelectedValue + "',CURRENT_TIMESTAMP(),'" + Session["usuario"].ToString() + "')";
                MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                conexion.Open();
                MySqlCommand mysqlcmd = new MySqlCommand(Query, conexion);
                mysqlcmd.CommandType = CommandType.Text;
                try
                {
                    mysqlcmd.ExecuteNonQuery();
                    txt_correo.Text = null;
                    ddl_tipo_correo.Enabled = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "desactivar_check", "desactivar_check();", true);
                    combo_tipo_correo();
                    combo_estatus();
                    combo_contacto();
                    grid_correo_bind(txt_matricula.Text);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                }
                catch (Exception ex)
                {
                    string test = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tcnco", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                }
            }
            else
            {
                if (!String.IsNullOrEmpty(txt_matricula.Text))
                {
                    grid_correo_bind(txt_matricula.Text);
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_correo();", true);
            }
        }

        protected void btn_update_Click(object sender, EventArgs e)
        {
            string pattern_mail = @"/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/";
            Regex mail = new Regex(pattern_mail);
            if (!String.IsNullOrEmpty(txt_matricula.Text) && !String.IsNullOrEmpty(txt_nombre.Text) && !String.IsNullOrEmpty(txt_correo.Text) && ddl_tipo_correo.SelectedValue != "0" && ddl_tcont.SelectedValue != "0")
            {
                string preferido = "";
                string selected = Request.Form["customSwitches"];
                if (selected == "on") { preferido = "S"; } else { preferido = "N"; }
                string Query = "UPDATE tcnco SET tcnco_correo='" + txt_correo.Text + "',tcnco_preferido='" + preferido + "'," +
                    "tcnco_estatus='" + ddl_estatus.SelectedValue + "',tcnco_date=current_timestamp()," +
                    "tcnco_user='" + Session["usuario"].ToString() + "' " +
                    "WHERE tcnco_tpers_num in (select tpers_num from tpers where tpers_id='" + txt_matricula.Text + "') " +
                    "AND tcnco_consec='" + lbl_consecutivo.Text + "' AND tcnco_tmail_clave='" + ddl_tipo_correo.SelectedValue + "' " +
                    "and tcnco_tcoda_consec=" + ddl_tcont.SelectedValue;
                //MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                //conexion.Open();
                //MySqlCommand mysqlcmd = new MySqlCommand(Query, conexion);
                //mysqlcmd.CommandType = CommandType.Text;
                try
                {
                    //mysqlcmd.ExecuteNonQuery();
                    serviceAlumno.EditarCorreoContacto(txt_matricula.Text, ddl_tcont.SelectedValue, lbl_consecutivo.Text, ddl_tipo_correo.SelectedValue,
                        txt_correo.Text, preferido, ddl_estatus.SelectedValue, Session["usuario"].ToString());
                    ddl_tcont.SelectedIndex = 0;
                    ddl_tipo_correo.SelectedIndex = 0;
                    ddl_tipo_correo.Enabled = true;
                    txt_correo.Text = null;
                    ddl_estatus.SelectedIndex = 0;
                    btn_cancel.Visible = false;
                    btn_update.Visible = false;
                    btn_save.Visible = true;
                    ddl_tipo_correo.Enabled = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "desactivar_check", "desactivar_check();", true);
                    //combo_tipo_correo();
                    //combo_estatus();
                    //combo_contacto();
                    grid_correo_bind(txt_matricula.Text);
                    GridCorreo.SelectedIndex = -1;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "update_p", "update();", true);
                }
                catch (Exception ex)
                {
                    string test = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tcnco", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                }
            }
            else
            {
                if (!String.IsNullOrEmpty(txt_matricula.Text))
                {
                    grid_correo_bind(txt_matricula.Text);
                }
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_correo();", true);
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
                    combo_contacto();

                }
                catch (Exception ex)
                {
                    //logs
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tcnco", Session["usuario"].ToString());
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
            btn_save.Visible = true;
            btn_update.Visible = false;
            GridAlumnos.Visible = false;
            Global.cuenta = txt_matricula.Text;
            Global.nombre = HttpUtility.HtmlDecode(row.Cells[2].Text);
            Global.ap_paterno = HttpUtility.HtmlDecode(row.Cells[3].Text);
            Global.ap_materno = HttpUtility.HtmlDecode(row.Cells[4].Text);
            grid_correo_bind(txt_matricula.Text);
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
                Global.inserta_log(mensaje_error, "tcnco", Session["usuario"].ToString());
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
                    Global.nombre = dtAlumno.Rows[0][1].ToString();
                    Global.ap_paterno = dtAlumno.Rows[0][2].ToString();
                    Global.ap_materno = dtAlumno.Rows[0][3].ToString();
                    combo_contacto();
                    grid_correo_bind(txt_matricula.Text);
                }
                else if (dtAlumno.Rows.Count > 1)
                {
                    GridAlumnos.Visible = true;
                    GridAlumnos.DataSource = dtAlumno;
                    GridAlumnos.DataBind();
                    GridCorreo.Visible = false;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "NoexisteAlumno", "NoexisteAlumno();", true);
                    txt_matricula.Text = null;
                    txt_nombre.Text = null;
                    GridCorreo.DataSource = null;
                    GridCorreo.DataBind();

                }
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcote", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }

            //Estudiantes.Visible = true;
            txt_matricula.Focus();

        }


    }
}