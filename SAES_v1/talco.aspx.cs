using MySql.Data.MySqlClient;
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
using static SAES_DBO.Models.ModelMenu;

namespace SAES_v1
{
    public partial class talco : System.Web.UI.Page
    {
        #region <Variables>
        AlumnoService serviceAlumno = new AlumnoService();
        MenuService servicePermiso = new MenuService();
        Catalogos_grales_Service serviceCatalogo = new Catalogos_grales_Service();
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
                    if(Global.cuenta.ToString()!="")
                        txt_matricula.Text = Global.cuenta;
                    if (Global.nombre.ToString() != "")
                        txt_nombre.Text = Global.nombre + " " + Global.ap_paterno + " " + Global.ap_materno;

                    LlenaPagina();
                    combo_estatus();
                    combo_tipo_correo();
                    grid_correo_bind(txt_matricula.Text);
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_Alumnos", "load_datatable_Alumnos();", true);


            }

            //ScriptManager.RegisterStartupScript(this, this.GetType(), "GridALumnos", "load_datatable_alumnos();", true);

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
            ModelObtenerPermisoFormResponse objPermiso = new ModelObtenerPermisoFormResponse();

            try
            {
                objPermiso = servicePermiso.ObtenerPermisoFormulario(Session["usuario"].ToString(), "talco");
                if (objPermiso != null)
                {
                    if (objPermiso.usme_update == "0" || objPermiso.usme_select == "0")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                    else
                    {
                        //if (objPermiso.usme_update == "0")
                        //    btn_tedcu.Visible = false;

                        //grid_bind_estados();
                    }
                }
                else
                {
                    //btn_tedcu.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                }

            }
            catch (Exception ex)
            {
                //Logs
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "talco", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }


        protected void combo_tipo_correo()
        {
            //string strQuerydire = "";
            //strQuerydire = "select tmail_clave clave, tmail_desc correo from tmail where tmail_estatus='A' " +
            //                "union " +
            //                "select '0' clave, '-------' correo " +
            //                "order by 1";
           
            try
            {                
                ddl_tipo_correo.DataSource = serviceCatalogo.obtenEMailActivos();
                ddl_tipo_correo.DataValueField = "clave";
                ddl_tipo_correo.DataTextField = "nombre";
                ddl_tipo_correo.DataBind();

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "talco", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
            //finally
            //{
            //    ConexionMySql.Close();
            //}
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
            //strQueryDir = "  select tpers_num id_num,tpers_id clave, concat(tpers_nombre,' ',tpers_paterno,' ',tpers_materno) nombre, " +
            //                "talco_tmail_clave tipo_mail, tmail_desc descripcion, talco_consec consecutivo, talco_correo correo, talco_preferido preferido, " +
            //                "talco_estatus c_estatus, case when talco_estatus = 'A' then 'Activo' else 'Inactivo' end estatus, fecha(date_format(talco_date, '%Y-%m-%d')) fecha " +
            //                "from(SELECT tpers_id matricula, concat(tpers_nombre, ' ', tpers_paterno, ' ', tpers_materno) alumno  from tpers where tpers_tipo = 'E') estu, talco, tpers,tmail " +
            //                "where tpers_id = estu.matricula and tpers_num = talco_tpers_num and tmail_clave = talco_tmail_clave and tpers_id like '" + matricula + "'";

            try
            {
                GridCorreo.DataSource = serviceAlumno.ObtenerCorreos(txt_matricula.Text);
                GridCorreo.DataBind();
                GridCorreo.DataMember = "Correo";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                GridCorreo.Visible = true;

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "talco", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
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
                    grid_correo_bind(txt_matricula.Text);
                }
                else if (txt_matricula.Text.Contains("%"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    grid_correo_bind(txt_matricula.Text);
                }
                else
                {
                    txt_nombre.Text = nombre_alumno(txt_matricula.Text);
                }

            }
            else
            {
                //txt_matricula.Text = Global.cuenta;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "NoexisteAlumno", "NoexisteAlumno();", true);

                ///Matricula no existe
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
            try 
            {
                return serviceCatalogo.ValidarExistenciaTelefono(matricula);
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "valida_telefono", Session["usuario"]?.ToString() ?? "Sistema");
                return false;
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
                Global.inserta_log(mensaje_error, "talco", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
            return nombre;
        }

        protected string consecutivo(string id_num)
        {
            string consecutivo = "";
            string Query = "";
            Query = "select IFNULL(max(talco_consec),0)+1 consecutivo from talco where talco_tpers_num in (select tpers_num from tpers where tpers_id='" + txt_matricula.Text + "') and talco_tmail_clave='" + ddl_tipo_correo.SelectedValue + "'";
            try
            {
                MySqlCommand cmd = new MySqlCommand(Query);
                DataTable dt = GetData(cmd);
                consecutivo = dt.Rows[0]["consecutivo"].ToString();
            }
            catch(Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "talco", Session["usuario"].ToString());
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
            txt_matricula.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);
            txt_nombre.Text = HttpUtility.HtmlDecode(row.Cells[3].Text);
            ddl_tipo_correo.SelectedValue = row.Cells[4].Text;
            lbl_consecutivo.Text = row.Cells[6].Text;
            txt_correo.Text = row.Cells[7].Text;
            if (row.Cells[8].Text == "S")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "activar_check", "activar_check();", true);
            }
            ddl_estatus.SelectedValue = row.Cells[9].Text;
            ddl_tipo_correo.Enabled = false;
            grid_correo_bind(txt_matricula.Text);
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {

            btn_save.Visible = true;
            btn_update.Visible = false;
            btn_cancel.Visible = false;
            //txt_matricula.Text = null;
            //txt_nombre.Text = null;
            //txt_matricula.ReadOnly=false;
            txt_nombre.ReadOnly = false;
            combo_tipo_correo();
            combo_estatus();
            txt_correo.Text = null;
            GridCorreo.SelectedIndex = -1;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "desactivar_check", "desactivar_check();", true);
            ddl_tipo_correo.Attributes.Remove("disabled");
            //GridCorreo.Visible = false;
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            string pattern_mail = @"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$";
            Regex mail = new Regex(pattern_mail);
            //var test_1 = mail.IsMatch(txt_correo.Text);
            if (!String.IsNullOrEmpty(txt_matricula.Text) && !String.IsNullOrEmpty(txt_nombre.Text) && ddl_tipo_correo.SelectedValue != "0" && mail.IsMatch(txt_correo.Text))
            {
                string preferido = "";
                string selected = Request.Form["customSwitches"];
                if (selected == "on") { preferido = "S"; } else { preferido = "N"; }
                try
                {
                    serviceAlumno.InsertarCorreo(txt_matricula.Text, ddl_tipo_correo.SelectedValue, txt_correo.Text,
                        preferido, ddl_estatus.SelectedValue, Session["usuario"].ToString());

                    txt_correo.Text = null;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "desactivar_check", "desactivar_check();", true);
                    combo_tipo_correo();
                    combo_estatus();
                    grid_correo_bind(txt_matricula.Text);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                }
                catch (Exception ex)
                {
                    string test = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "talco", Session["usuario"].ToString());
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
            if (!String.IsNullOrEmpty(txt_matricula.Text) && !String.IsNullOrEmpty(txt_nombre.Text) && !String.IsNullOrEmpty(txt_correo.Text) && ddl_tipo_correo.SelectedValue != "0" )
            {
                string preferido = "";
                string selected = Request.Form["customSwitches"];
                if (selected == "on") { preferido = "S"; } else { preferido = "N"; }
                try
                {
                    serviceAlumno.EditarCorreo(txt_matricula.Text, ddl_tipo_correo.SelectedValue, txt_correo.Text,
                                           preferido, ddl_estatus.SelectedValue, Session["usuario"].ToString());

                    grid_correo_bind(txt_matricula.Text);
                    GridCorreo.SelectedIndex = -1;
                    txt_matricula.Enabled = true;
                    ddl_tipo_correo.Enabled = true;
                    ddl_tipo_correo.SelectedIndex = 0;
                    txt_correo.Text = string.Empty;
                    ddl_estatus.Enabled = true;
                    ddl_estatus.SelectedIndex = 0;
                    btn_update.Visible=false;
                    btn_save.Visible = true;
                    btn_cancel.Visible = false;                    
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "update_p", "update();", true);
                }
                catch (Exception ex)
                {
                    string test = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "talco", Session["usuario"].ToString());
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


        protected void GridAlumnos_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridAlumnos.SelectedRow;
            txt_matricula.Text = row.Cells[1].Text;
            txt_nombre.Text = HttpUtility.HtmlDecode(row.Cells[2].Text) + " " + HttpUtility.HtmlDecode(row.Cells[3].Text) + " " +
             HttpUtility.HtmlDecode(row.Cells[4].Text);
            btn_save.Visible = false;
            btn_update.Visible = true;
            GridAlumnos.Visible = false;
            txt_matricula.ReadOnly = true;

            //txt_matricula.Attributes.Add("readonly", "");
            txt_nombre.ReadOnly = true;

            btn_save.Visible = true;
            btn_update.Visible = false;
            Global.cuenta = txt_matricula.Text;
            Global.nombre = HttpUtility.HtmlDecode(row.Cells[2].Text);
            Global.ap_paterno = HttpUtility.HtmlDecode(row.Cells[3].Text);
            Global.ap_materno = HttpUtility.HtmlDecode(row.Cells[4].Text);
            grid_correo_bind(txt_matricula.Text);
        }

        protected void consulta_docs_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Tccor.aspx");
        }

        protected void btn_correos_Click(object sender, EventArgs e)
        {
            Response.Redirect("Tccor.aspx");
        }

        protected void linkBttnBusca_Click(object sender, EventArgs e)
        {
            if (txt_matricula.Text != string.Empty)
            {
                if (valida_matricula(txt_matricula.Text))
                {

                    if (valida_telefono(txt_matricula.Text))
                    {
                        txt_nombre.Text = nombre_alumno(txt_matricula.Text);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                        grid_correo_bind(txt_matricula.Text);
                    }
                    else if (txt_matricula.Text.Contains("%"))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                        grid_correo_bind(txt_matricula.Text);
                    }
                    else
                    {
                        txt_nombre.Text = nombre_alumno(txt_matricula.Text);
                    }

                }
                else
                {
                    //txt_matricula.Text = Global.cuenta;
                    txt_matricula.Text = "";
                    txt_nombre.Text = "";
                    txt_matricula.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "NoexisteAlumno", "NoexisteAlumno();", true);
                }
            }
            else
            {
                GridAlumnos.DataSource = serviceAlumno.ListaAlumnos(txt_matricula.Text);
                GridAlumnos.DataBind();
                GridAlumnos.DataMember = "Solicitudes";
                GridAlumnos.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridAlumnos.UseAccessibleHeader = true;
                GridAlumnos.Visible = true;

            }
        }
    }
}