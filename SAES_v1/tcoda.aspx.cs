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
    public partial class tcoda : System.Web.UI.Page
    {
        #region <Variables>
        //Utilidades utils = new Utilidades();
        AlumnoService serviceAlumno = new AlumnoService();
        CobranzaService serviceCobranza = new CobranzaService();
        List<ModelObtenPaisesResponse> lstPaises = new List<ModelObtenPaisesResponse>();
        Catalogos serviceCatalogo = new Catalogos();
        DocumentoService serviceDocumento = new DocumentoService();
        MenuService servicePermiso = new MenuService();

        public string id_num = null;
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

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "ctrl_f_nac", "ctrl_f_nac();", true);
                //txt_matricula.Attributes.Add("readonly", "");

                if (!IsPostBack)
                {
                    LlenaPagina();
                    //combo_fecha();
                    //combio_anio();
                    combo_contacto();
                    txt_matricula.Text = Global.cuenta;
                    txt_nombre.Text = Global.nombre + " " + Global.ap_paterno + " " + Global.ap_materno;
                    Alumno();
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ctrl_fecha_nac", "ctrl_fecha_nac();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_Alumnos", "load_datatableAlumno();", true);

            }

        }

        private void LlenaPagina()
        {
            ModelObtenerPermisoFormResponse objPermiso = new ModelObtenerPermisoFormResponse();
            objPermiso.usme_update = "0";
            objPermiso.usme_select = "0";
            System.Threading.Thread.Sleep(50);
            try
            {
                objPermiso = servicePermiso.ObtenerPermisoFormulario(Session["usuario"].ToString(), "tcoda");
                if (objPermiso != null)
                {
                    if (objPermiso.usme_update == "0" || objPermiso.usme_select == "0")
                    {
                        btn_tpers.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                    }
                    //else
                    //    grid_tespr_bind();
                }
                else
                {
                    btn_tpers.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                }


            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcoda", Session["usuario"].ToString());
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

        protected void combo_contacto()
        {
            string strQuerytcont = "";
            strQuerytcont = "select tcont_clave clave, tcont_desc contacto from tcont where tcont_estatus='A'  " +
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
                Global.inserta_log(mensaje_error, "tcoda", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
            finally
            {
                ConexionMySql.Close();
            }
        }

        //protected void combo_fecha()
        //{

        //    CboYear.Items.Clear();
        //    DateTime hoy = DateTime.Today;
        //    CboYear.Items.Add("---");
        //    for (int i = 1950; i < hoy.Year; i++)
        //    {
        //        CboYear.Items.Add(i.ToString());
        //    }
        //    CboMonth.Items.Clear();
        //    //CboDay.Items.Clear();
        //}


        protected void Alumno(object sender, EventArgs e)
        {
            Alumno();
        }

        protected void Alumno()
        {
            //string QueryEstudiantes = " select tcoda_consecutivo consecutivo, tcoda_tipo tipo, tcont_desc contacto, tcoda_nombre nombre, " +
            //    "  tcoda_rfc curp , date_format(tcoda_fecha_nac, '%d/%m/%Y') fecha, tcoda_usuario usuario, fecha(date_format(tcoda_date, '%Y-%m-%d')) fecha_reg " +
            //    " from tcoda, tcont ";
            //QueryEstudiantes = QueryEstudiantes + " where tcoda_tpers_num in (select tpers_num from tpers where tpers_id='" + txt_matricula.Text + "') and tcoda_tipo=tcont_clave order by tcoda_consecutivo";
            try
            {
                GridSolicitudes.DataSource = serviceAlumno.ObtenerContactosGrid(txt_matricula.Text);
                GridSolicitudes.DataBind();
                GridSolicitudes.DataMember = "Solicitudes";
                GridSolicitudes.Visible = true;
            }
            catch (Exception ex)
            {
                //logs
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcoda", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }
        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            //txt_matricula.Text = null;
            //txt_nombre.Text = null;
            txt_nombre_cont.Text = null;
            //combo_fecha();
            txt_curp.Text = null;
            txt_f_nac.Text = null;
            btn_save.Visible = true;
            btn_cancel.Visible = false;
            btn_update.Visible = false;
            ddl_tcont.SelectedIndex = 0;
            //btn_cancel_update.Visible = false;
            //txt_matricula.Attributes.Remove("readonly");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {

            if (ddl_tcont.SelectedValue != "0" && !String.IsNullOrEmpty(txt_nombre_cont.Text)
                && !String.IsNullOrEmpty(txt_curp.Text.Trim()))
            {
                //if (valida_curp_format(txt_curp.Text))
                //{
                string strCadSQL = "";


                double consecutivo = 0;
                //if (Global.consecutivo == 0)
                //{
                //    if (dssql1.Tables[0].Rows[0][0].ToString() == "" || dssql1.Tables[0].Rows[0][0].ToString() == null)
                //    {
                //        consecutivo = 1;
                //    }
                //    else
                //    {
                //        consecutivo = Convert.ToDouble(dssql1.Tables[0].Rows[0][0].ToString()) + 1;
                //    }
                //}
                //else
                //{
                //    consecutivo = Global.consecutivo;
                //}
                Global.cuenta = txt_matricula.Text;

                //strCadSQL = "INSERT INTO tcoda " +
                //" Values ((select tpers_num from tpers where tpers_id='" + txt_matricula.Text + "'),'" + consecutivo
                //+ "','" + ddl_tcont.SelectedValue + "','" + txt_nombre_cont.Text + "'," +
                //" STR_TO_DATE('" + txt_f_nac.Text + "', '%d/%m/%Y'),'" + txt_curp.Text + "',current_timestamp(),'" +
                //Session["usuario"].ToString() + "')";

                try
                {
                    serviceAlumno.InsertarContacto(txt_matricula.Text, ddl_tcont.SelectedValue, txt_nombre_cont.Text,
                        txt_f_nac.Text, txt_curp.Text, Session["usuario"].ToString());

                    //txt_nombre.Text = null;
                    txt_nombre_cont.Text = null;
                    combo_contacto();
                    txt_curp.Text = null;
                    txt_f_nac.Text = null;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                    txt_matricula.Attributes.Add("readonly", "");
                    btn_save.Visible = true;
                    btn_cancel.Visible = false;
                    btn_update.Visible = false;
                    //btn_cancel_update.Visible = true;

                    Alumno();
                    

                }
                catch (Exception ex)
                {
                    ///logs
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tcoda", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_solicitud();", true);
            }
        }

        protected void btn_update_Click(object sender, EventArgs e)
        {
            if (ddl_tcont.SelectedValue != "0" && !String.IsNullOrEmpty(txt_nombre_cont.Text)
                && !String.IsNullOrEmpty(txt_curp.Text.Trim()))
            {
                try
                {
                    serviceAlumno.EditarTcoda(txt_matricula.Text, ddl_tcont.SelectedValue, Convert.ToString(Global.consecutivo), txt_nombre_cont.Text, txt_f_nac.Text, txt_curp.Text, Session["usuario"].ToString());

                    //combo_contacto();
                    txt_curp.Text = null;
                    txt_nombre_cont.Text = null;
                    txt_f_nac.Text = null;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "update_p", "update();", true);
                    Alumno();
                    GridSolicitudes.SelectedIndex = -1;
                }
                catch (Exception ex)
                {
                    ///logs
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tcoda", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                }
            }
            else
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_solicitud();", true);
            }

        }

        protected void GridAlumnos_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridAlumnos.SelectedRow;



            txt_matricula.Text = HttpUtility.HtmlDecode(row.Cells[1].Text);
            txt_nombre.Text = HttpUtility.HtmlDecode(row.Cells[2].Text) + " " + HttpUtility.HtmlDecode(row.Cells[3].Text) + " " + HttpUtility.HtmlDecode(row.Cells[4].Text);


            Global.cuenta = txt_matricula.Text;
            Global.nombre = HttpUtility.HtmlDecode(row.Cells[2].Text);
            Global.ap_paterno = HttpUtility.HtmlDecode(row.Cells[3].Text);
            Global.ap_materno = HttpUtility.HtmlDecode(row.Cells[4].Text);
            GridAlumnos.Visible = false;
            Alumno();
        }

        protected void GridSolicitudes_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DateTime fechaNacimiento = new DateTime();

            GridViewRow row = GridSolicitudes.SelectedRow;

            txt_nombre_cont.Text = HttpUtility.HtmlDecode(row.Cells[4].Text);
            //combo_contacto();

            ////string cont = row.Cells[2].Text;
            ////ddl_tcont.SelectedValue = row.Cells[2].Text;
            ////txt_nombre_cont.Text = row.Cells[4].Text;
            ////txt_curp.Text = row.Cells[5].Text;
            ////if (row.Cells[6].Text != "")
            ////{
            ////    fechaNacimiento = Convert.ToDateTime(row.Cells[6].Text);
            ////    CboYear.SelectedValue = fechaNacimiento.ToString("yyyy");
            ////    CboMonth.SelectedValue = fechaNacimiento.ToString("MM");
            ////    CboDay.SelectedValue = fechaNacimiento.ToString("dd");
            ////}

            ddl_tcont.SelectedValue = row.Cells[2].Text;
            txt_curp.Text = HttpUtility.HtmlDecode(row.Cells[5].Text);
            if (row.Cells[6].Text != "")
            {
                //fechaNacimiento = Convert.ToDateTime(row.Cells[6].Text);
                //CboYear.SelectedValue = fechaNacimiento.ToString("yyyy");
                //CboMonth.SelectedValue = fechaNacimiento.ToString("MM");
                //CboDay.SelectedValue = fechaNacimiento.ToString("dd");
                txt_f_nac.Text = row.Cells[6].Text;
            }
            //txt_f_nac.Text = row.Cells[5].Text;
            btn_cancel.Visible = true;
            btn_save.Visible = false;
            btn_update.Visible = true;
            //btn_cancel_update.Visible = true;
            Global.cuenta = txt_matricula.Text;
            Global.consecutivo = Convert.ToDouble(HttpUtility.HtmlDecode(row.Cells[1].Text));
        }

        protected bool valida_curp_format(string curp_form)
        {
            if (txt_curp.Text.Length == 18)
            {
                double indicador = 0;
                string regex_curp_of =
                "[A-Z]{1}[AEIOU]{1}[A-Z]{2}[0-9]{2}" +
                "(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1])" +
                "[HM]{1}" +
                "(AS|BC|BS|CC|CS|CH|CL|CM|DF|DG|GT|GR|HG|JC|MC|MN|MS|NT|NL|OC|PL|QT|QR|SP|SL|SR|TC|TS|TL|VZ|YN|ZS|NE)" +
                "[B-DF-HJ-NP-TV-Z]{3}" +
                "[0-9A-Z]{1}[0-9]{1}$";
                Regex curp = new Regex(regex_curp_of);
                string dv = curp_form.Substring(17, 1);
                if (!curp.IsMatch(curp_form))
                {
                    return false;
                }
                else
                {

                    string diccionario = "0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZ";
                    double lngSuma = 0.0, lngDigito = 0.0;
                    for (var i = 0; i < 17; i++)
                        lngSuma = lngSuma + diccionario.IndexOf(curp_form[i]) * (18 - i);
                    lngDigito = 10 - lngSuma % 10;
                    if (lngDigito == 10)
                    {
                        indicador = 0;

                    }
                    else
                    {
                        indicador = lngDigito;

                    }
                }
                if (dv == indicador.ToString())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        //protected void Carga_Day(object sender, EventArgs e)
        //{
        //    int fin = 0;
        //    if (CboMonth.SelectedValue.ToString() == "Enero" || CboMonth.SelectedValue.ToString() == "Marzo" ||
        //        CboMonth.SelectedValue.ToString() == "Mayo" || CboMonth.SelectedValue.ToString() == "Julio" ||
        //        CboMonth.SelectedValue.ToString() == "Agosto" || CboMonth.SelectedValue.ToString() == "Octubre" ||
        //        CboMonth.SelectedValue.ToString() == "Diciembre")
        //    { fin = 31; }

        //    if (CboMonth.SelectedValue.ToString() == "Abril" || CboMonth.SelectedValue.ToString() == "Junio" ||
        //        CboMonth.SelectedValue.ToString() == "Septiembre" || CboMonth.SelectedValue.ToString() == "Noviembre")
        //    { fin = 30; }

        //    if (CboMonth.SelectedValue.ToString() == "Febrero")
        //    { fin = 29; }

        //    //CboDay.Items.Clear();
        //    //CboDay.Items.Add("---");
        //    //for (int i = 1; i <= fin; i++)
        //    //{
        //    //    CboDay.Items.Add(i.ToString());
        //    //}

        //}


        protected void Carga_Estudiante(object sender, EventArgs e)
        {
            Carga_Estudiante();
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
                    Global.nombre = dtAlumno.Rows[0][0].ToString();
                    Global.ap_paterno = dtAlumno.Rows[0][1].ToString();
                    Global.ap_materno = dtAlumno.Rows[0][2].ToString();
                    Alumno();
                    //Carga_Inf_Estudiante();
                    //grid_telefono_bind(txt_matricula.Text);
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
                    GridSolicitudes.DataSource = null;
                    GridSolicitudes.DataBind(); 
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "NoexisteAlumno", "NoexisteAlumno();", true);
                }
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcoda", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }

            //Estudiantes.Visible = true;
            txt_matricula.Focus();

        }
        protected void linkBttnBusca_Click(object sender, EventArgs e)
        {
            //GridTelefono.Visible = false;
            try
            {
                Carga_Estudiante();
            }
            catch (Exception ex)
            {
                //Logs
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcoda", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void btn_cancel_update_Click(object sender, EventArgs e)
        {
            btn_cancel.Visible = true;
            btn_cancel_update.Visible = false;
            btn_save.Visible = true;
            btn_update.Visible = false;
            GridSolicitudes.SelectedIndex = -1;
            txt_matricula.Text = string.Empty;
            txt_nombre.Text = string.Empty;
            txt_matricula.ReadOnly = false;
            ddl_tcont.SelectedIndex = 0;
            txt_nombre_cont.Text = string.Empty;
            txt_curp.Text = string.Empty;
            txt_f_nac.Text = string.Empty;
            GridSolicitudes.DataSource = null;
            GridSolicitudes.DataBind();
        }
    }
}