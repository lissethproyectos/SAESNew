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
using static SAES_DBO.Models.ModelMenu;

namespace SAES_v1
{
    public partial class tcote : System.Web.UI.Page
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
                    LlenaPagina();
                    txt_matricula.Text = Global.cuenta;
                    txt_nombre.Text = Global.nombre + " " + Global.ap_paterno + " " + Global.ap_materno;
                    combo_estatus();
                    combo_tipo_telefono();
                    if (txt_matricula.Text != null)
                    {
                        combo_contacto();
                    }
                    grid_telefono_bind(txt_matricula.Text);
                }
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_Alumnos", "load_datatable_Alumnos();", true);

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
        //                      " and tusme_tmede_clave = tmede_clave and tmede_forma='talte' ";

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
        //            //btn_talte.Visible = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string mensaje_error = ex.Message.Replace("'", "-");
        //        Global.inserta_log(mensaje_error, "tcote", Session["usuario"].ToString());
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
                objPermiso = servicePermiso.ObtenerPermisoFormulario(Session["usuario"].ToString(), "tcote");
                if (objPermiso != null)
                {
                    if (objPermiso.usme_update == "0" || objPermiso.usme_select == "0")
                    {
                        btn_talte.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                    }
                    //else
                    //    grid_telefono_bind(txt_matricula);
                }
                else
                {
                    btn_talte.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                }


            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcote", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);

            }
        }


        protected void combo_contacto()
        {
            string strQuerytcont = "";
            strQuerytcont = "select tcoda_consecutivo clave, tcont_desc contacto from tcoda, tcont where tcont_estatus='A'  " +
                            " and tcoda_tpers_num in (select tpers_num from tpers where tpers_id='" + txt_matricula.Text + "') and tcoda_tipo=tcont_clave " +
                            "union " +
                            "select '0' clave, '-------' contacto " +
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
                ConsultaMySql.CommandText = strQuerytcont;
                DatosMySql = ConsultaMySql.ExecuteReader();
                TablaEstado.Load(DatosMySql, LoadOption.OverwriteChanges);

                ddl_tcont.DataSource = TablaEstado;
                ddl_tcont.DataValueField = "Clave";
                ddl_tcont.DataTextField = "contacto";
                ddl_tcont.DataBind();

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcote", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
            finally
            {
                ConexionMySql.Close();
            }
        }

        protected void combo_tipo_telefono()
        {
            try
            {
                ddl_tipo_telefono.DataSource = serviceCatalogo.obtenTelefonosActivos();
                ddl_tipo_telefono.DataValueField = "clave";
                ddl_tipo_telefono.DataTextField = "nombre";
                ddl_tipo_telefono.DataBind();

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcote", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }
        protected void combo_estatus()
        {
            ddl_estatus.Items.Clear();
            ddl_estatus.Items.Add(new ListItem("Activo", "A"));
            ddl_estatus.Items.Add(new ListItem("Inactivo", "B"));
        }
        protected void grid_telefono_bind(string matricula)
        {
            GridTelefono.Visible = true;

            try
            {
                GridTelefono.DataSource = serviceAlumno.ObtenerTelContactos(txt_matricula.Text);
                GridTelefono.DataBind();
                GridTelefono.DataMember = "Telefono";
                GridTelefono.UseAccessibleHeader = true;
            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcote", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            btn_save.Visible = true;
            btn_update.Visible = false;
            txt_matricula.Text = null;
            txt_matricula.ReadOnly = false;
            txt_nombre.Text = null;
            combo_tipo_telefono();
            combo_estatus();
            combo_contacto();
            txt_lada.Text = null;
            txt_telefono.Text = null;
            txt_extension.Text = null;
            ddl_tipo_telefono.Enabled = true;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
            GridTelefono.SelectedIndex = -1;
            //GridTelefono.Visible = false;
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txt_matricula.Text) && !String.IsNullOrEmpty(txt_nombre.Text) && !String.IsNullOrEmpty(txt_lada.Text) && !String.IsNullOrEmpty(txt_telefono.Text) && ddl_tipo_telefono.SelectedValue != "0" && ddl_tcont.SelectedValue != "0")
            {
                ModelInsertarTelContactoResponse objExiste = new ModelInsertarTelContactoResponse();
                try
                {
                    objExiste=serviceAlumno.InsertarTelContacto(txt_matricula.Text, ddl_tcont.SelectedValue,
                        ddl_tipo_telefono.SelectedValue, txt_lada.Text, txt_telefono.Text, txt_extension.Text, ddl_estatus.SelectedValue,
                        Session["usuario"].ToString());
                    if (objExiste.Existe == "0")
                    {
                        txt_lada.Text = null;
                        txt_telefono.Text = null;
                        txt_extension.Text = null;
                        combo_tipo_telefono();
                        combo_estatus();
                        combo_contacto();
                        grid_telefono_bind(txt_matricula.Text);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                    }
                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "registro_duplicado", "registro_duplicado();", true);

                }
                catch (Exception ex)
                {
                    string test = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tcote", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                }
            }
            else
            {
                if (!String.IsNullOrEmpty(txt_matricula.Text))
                {
                    grid_telefono_bind(txt_matricula.Text);
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_telefono();", true);
            }
        }

        protected void btn_update_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txt_matricula.Text) && !String.IsNullOrEmpty(txt_nombre.Text) && !String.IsNullOrEmpty(txt_lada.Text) && !String.IsNullOrEmpty(txt_telefono.Text) && ddl_tipo_telefono.SelectedValue != "0" && ddl_tcont.SelectedValue != "0")
            {                
                try
                {
                    serviceAlumno.EditarTelContacto(txt_matricula.Text, ddl_tcont.SelectedValue,
                        ddl_tipo_telefono.SelectedValue, txt_lada.Text, txt_telefono.Text, txt_extension.Text, ddl_estatus.SelectedValue,
                        Session["usuario"].ToString(), GridTelefono.SelectedRow.Cells[5].Text);

                    ddl_tcont.SelectedIndex = 0;
                    ddl_tipo_telefono.SelectedIndex = 0;
                    ddl_tipo_telefono.Enabled = true;
                    txt_lada.Text = string.Empty;
                    txt_telefono.Text = string.Empty;
                    txt_extension.Text = string.Empty;
                    ddl_estatus.SelectedIndex = 0;
                    //btn_cancel.Visible = true;
                    btn_save.Visible = true;
                    btn_update.Visible = false;
                    btn_cancel_update.Visible = false;
                    grid_telefono_bind(txt_matricula.Text);
                    GridTelefono.SelectedIndex = -1;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "update_p", "update();", true);
                }
                catch (Exception ex)
                {
                    string test = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tcote", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                }
            }
            else
            {
                if (!String.IsNullOrEmpty(txt_matricula.Text))
                {
                    grid_telefono_bind(txt_matricula.Text);
                }
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_telefono();", true);
            }
        }

        protected void txt_matricula_TextChanged(object sender, EventArgs e)
        {
            if (valida_matricula(txt_matricula.Text))
            {

                if (valida_telefono(txt_matricula.Text))
                {
                    txt_nombre.Text = nombre_alumno(txt_matricula.Text);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    grid_telefono_bind(txt_matricula.Text);
                }
                else if (txt_matricula.Text.Contains("%"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    grid_telefono_bind(txt_matricula.Text);
                }
                else
                {
                    txt_nombre.Text = nombre_alumno(txt_matricula.Text);
                }

            }
            else
            {
                ///Matricula no existe
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "NoexisteAlumno", "NoexisteAlumno();", true);
                txt_matricula.Text = ""; 
                txt_nombre.Text = "";
                GridTelefono.DataSource = null;
                GridTelefono.DataBind();
            }
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
            Query = "SELECT COUNT(*) Indicador FROM tcote WHERE tcote_tpers_num = (SELECT DISTINCT tpers_num FROM tpers WHERE tpers_id='" + matricula + "')";
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
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcote", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
            return nombre;
        }

        protected string consecutivo(string id_num)
        {
            string consecutivo = "";
            string Query = "";
            Query = "select IFNULL(max(tcote_consec),0)+1 consecutivo from tcote where tcote_tpers_num in (select tpers_num from tpers where tpers_id='" + txt_matricula.Text + "') and tcote_ttele_clave='" + ddl_tipo_telefono.SelectedValue + "' ";
            try
            {
                MySqlCommand cmd = new MySqlCommand(Query);
                DataTable dt = GetData(cmd);
                consecutivo = dt.Rows[0]["consecutivo"].ToString();
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcote", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
            return consecutivo;
        }
        protected void GridTelefono_SelectedIndexChanged(object sender, EventArgs e)
        {
            btn_save.Visible = false;
            btn_update.Visible = true;

            btn_cancel_update.Visible = true;
            //btn_cancel.Visible = false;

            GridViewRow row = GridTelefono.SelectedRow;

            combo_tipo_telefono();
            ddl_tipo_telefono.SelectedValue = row.Cells[3].Text;
            lbl_consecutivo.Text = row.Cells[5].Text;
            txt_lada.Text = row.Cells[6].Text;
            txt_telefono.Text = row.Cells[7].Text;
            txt_extension.Text = HttpUtility.HtmlDecode(row.Cells[8].Text.Trim());
            combo_contacto();
            ddl_tcont.SelectedValue = row.Cells[12].Text;
            ddl_tcont.Enabled = false;
            combo_estatus();
            ddl_estatus.SelectedValue = row.Cells[9].Text;
            ddl_tipo_telefono.Enabled = false;
            grid_telefono_bind(txt_matricula.Text);
        }

        protected void grid_alumnos_bind(object sender, EventArgs e)
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
                Global.inserta_log(mensaje_error, "tcote", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
            conexion.Close();
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
            btn_save.Visible = true;
            btn_update.Visible = false;
            //btn_cancel.Visible = true;
            btn_cancel_update.Visible = false;
            Global.cuenta = txt_matricula.Text;
            Global.nombre = HttpUtility.HtmlDecode(row.Cells[2].Text);
            Global.ap_paterno = HttpUtility.HtmlDecode(row.Cells[3].Text);
            Global.ap_materno = HttpUtility.HtmlDecode(row.Cells[4].Text);
            id_num = row.Cells[5].Text;
            grid_telefono_bind(txt_matricula.Text);
            combo_contacto();
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
                Global.inserta_log(mensaje_error, "tcodi", Session["usuario"].ToString());
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
                    GridAlumnos.Visible = false;
                    GridTelefono.Visible = true;
                    grid_telefono_bind(txt_matricula.Text);
                }
                else if (dtAlumno.Rows.Count > 1)
                {
                    GridAlumnos.Visible = true;
                    GridAlumnos.DataSource = dtAlumno;
                    GridAlumnos.DataBind();
                    GridTelefono.Visible = false;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "NoexisteAlumno", "NoexisteAlumno();", true);
                    txt_matricula.Text = "";
                    txt_nombre.Text = "";
                    GridTelefono.DataSource = null;
                    GridTelefono.DataBind();
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

        protected void btn_cancel_update_Click(object sender, EventArgs e)
        {
            ddl_tcont.SelectedIndex = 0;
            ddl_tipo_telefono.SelectedIndex = 0;
            txt_lada.Text = string.Empty;
            txt_telefono.Text = string.Empty;
            txt_extension.Text = string.Empty;
            ddl_estatus.SelectedIndex = 0;
            btn_save.Visible = true;
            btn_update.Visible = false;
            //btn_cancel.Visible = true;
            btn_cancel_update.Visible = false;
            GridTelefono.SelectedIndex = -1;
            //GridTelefono.DataSource = null;
            //GridTelefono.DataBind();
        }

        protected void txt_matricula_TextChanged1(object sender, EventArgs e)
        {
            Carga_Estudiante();
        }
    }
}