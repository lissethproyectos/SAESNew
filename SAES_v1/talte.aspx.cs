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
    public partial class talte : System.Web.UI.Page
    {
        #region <Variables>
        //Utilidades utils = new Utilidades();
        AlumnoService serviceAlumno = new AlumnoService();
        List<ModelObtenPaisesResponse> lstPaises = new List<ModelObtenPaisesResponse>();
        Catalogos serviceCatalogo = new Catalogos();
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

                if (!IsPostBack)
                {
                    //txt_matricula.ReadOnly= true;
                    txt_matricula.Text = Global.cuenta;
                    txt_nombre.Text = Global.nombre + " " + Global.ap_paterno + " " + Global.ap_materno;
                    LlenaPagina();
                    combo_estatus();
                    combo_tipo_telefono();
                    grid_telefono_bind(txt_matricula.Text);
                    GridTelefono.SelectedIndex = -1;
                }
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "GridALumnos", "load_datatable_alumnos();", true);

        }

        protected void Carga_Estudiante()
        {
            ModelObtenerAlumnosResponse datosAlumno = new ModelObtenerAlumnosResponse();
            txt_nombre.Text = string.Empty;
            DataTable dtAlumno = new DataTable();
            GridAlumnos.Visible = false;


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
                    txt_nombre.Text = dtAlumno.Rows[0][0].ToString() + " " + dtAlumno.Rows[0][1].ToString() +
                        " " + dtAlumno.Rows[0][2].ToString();
                    grid_telefono_bind(txt_matricula.Text);
                }
                else
                {
                    //txt_matricula.Text = Global.cuenta;
                    txt_matricula.Text = "";
                    txt_nombre.Text = "";
                    txt_matricula.Focus();
                    GridAlumnos.Visible = true;
                    GridAlumnos.DataSource = dtAlumno;
                    GridAlumnos.DataBind();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "GridALumnos", "load_datatable_alumnos();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "NoexisteAlumno", "NoexisteAlumno();", true);


                }
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tsimu", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }

            //Estudiantes.Visible = true;
            txt_matricula.Focus();

        }
        private void LlenaPagina()
        {
            ModelObtenerPermisoFormResponse objPermiso = new ModelObtenerPermisoFormResponse();
            objPermiso.usme_update = "0";
            objPermiso.usme_select = "0";

            System.Threading.Thread.Sleep(50);
            try
            {
                objPermiso = servicePermiso.ObtenerPermisoFormulario(Session["usuario"].ToString(), "talte");
                if (objPermiso != null)
                {
                    if (objPermiso.usme_update == "0" || objPermiso.usme_select == "0")
                    {
                        btn_talte.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                    }
                    //else
                    //    grid_bind_pais();
                }
                else
                {
                    btn_talte.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                }


            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "talte", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
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
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "talte", Session["usuario"].ToString());
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
            try
            {

                GridTelefono.DataSource = serviceAlumno.ObtenerTelefonos(txt_matricula.Text);
                GridTelefono.DataBind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                GridTelefono.Visible = true;

            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "talte", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                string test = ex.Message;
            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            btn_save.Visible = true;
            btn_update.Visible = false;
            btn_cancel.Visible = false;
            //txt_matricula.Text = null;
            //txt_nombre.Text = null;
            //txt_matricula.ReadOnly = false;
            combo_tipo_telefono();
            combo_estatus();
            txt_lada.Text = null;
            txt_telefono.Text = null;
            txt_extension.Text = null;
            txt_telefono.ReadOnly = false;
            ddl_tipo_telefono.Enabled= true;
            GridTelefono.SelectedIndex = -1;
            //ddl_tipo_telefono.Attributes.Remove("disabled");
           // ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
            //GridTelefono.Visible = false;
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txt_matricula.Text) && !String.IsNullOrEmpty(txt_nombre.Text) && !String.IsNullOrEmpty(txt_lada.Text) && !String.IsNullOrEmpty(txt_telefono.Text) && ddl_tipo_telefono.SelectedValue != "0")
            {
                try
                {
                    serviceAlumno.InsertarTelefono(txt_matricula.Text, ddl_tipo_telefono.SelectedValue,
                        txt_lada.Text, txt_telefono.Text, txt_extension.Text, ddl_estatus.SelectedValue, Session["usuario"].ToString());
                    txt_lada.Text = null;
                    txt_telefono.Text = null;
                    txt_extension.Text = null;
                    //combo_tipo_telefono();
                    //combo_estatus();
                    grid_telefono_bind(txt_matricula.Text);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                }
                catch (Exception ex)
                {
                    string test = ex.Message;
                    //resultado.Text = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "talte", Session["usuario"].ToString());
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
            if (!String.IsNullOrEmpty(txt_matricula.Text) && !String.IsNullOrEmpty(txt_nombre.Text) && !String.IsNullOrEmpty(txt_lada.Text) && !String.IsNullOrEmpty(txt_telefono.Text) && ddl_tipo_telefono.SelectedValue != "0")
            {
                try
                {
                    serviceAlumno.EditarTelefono(txt_matricula.Text, GridTelefono.SelectedRow.Cells[6].Text, ddl_tipo_telefono.SelectedValue,
                                           txt_lada.Text, txt_telefono.Text, txt_extension.Text, ddl_estatus.SelectedValue, Session["usuario"].ToString()); grid_telefono_bind(txt_matricula.Text);
                    GridTelefono.SelectedIndex = -1;
                    ddl_tipo_telefono.Enabled = true;
                    txt_lada.Text = string.Empty;
                    txt_telefono.Text=string.Empty;
                    txt_extension.Text = string.Empty;
                    ddl_estatus.SelectedIndex = 0;

                    btn_update.Visible = false;
                    btn_save.Visible = true;
                    btn_cancel.Visible = false;
                    //txt_matricula.ReadOnly = false;
                    txt_telefono.ReadOnly = false;
                    GridTelefono.SelectedIndex = -1;

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "update_p", "update();", true);
                }
                catch (Exception ex)
                {
                    string test = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "talte", Session["usuario"].ToString());
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
            Carga_Estudiante();
            //if (valida_matricula(txt_matricula.Text))
            //{

            //    if (valida_telefono(txt_matricula.Text))
            //    {
            //        txt_nombre.Text = nombre_alumno(txt_matricula.Text);
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
            //        grid_telefono_bind(txt_matricula.Text);
            //    }
            //    else if (txt_matricula.Text.Contains("%"))
            //    {
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
            //        grid_telefono_bind(txt_matricula.Text);
            //    }
            //    else
            //    {
            //        txt_nombre.Text = nombre_alumno(txt_matricula.Text);
            //    }

            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "NoexisteAlumno", "NoexisteAlumno();", true);
            //    txt_matricula.Text = ""; txt_nombre.Text = "";
            //    GridTelefono.Visible = false;
            //}
        }

        //protected bool valida_matricula(string matricula)
        //{
        //    string Query = "";
        //    Query = "SELECT COUNT(*) Indicador FROM tpers WHERE tpers_id Like '" + matricula + "'";
        //    MySqlCommand cmd = new MySqlCommand(Query);
        //    DataTable dt = GetData(cmd);
        //    if (dt.Rows[0]["Indicador"].ToString() == "0")
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}

        //protected bool valida_telefono(string matricula)
        //{
        //    string Query = "";
        //    Query = "SELECT COUNT(*) Indicador FROM talte WHERE talte_tpers_num = (SELECT DISTINCT tpers_num FROM tpers WHERE tpers_id='" + matricula + "')";
        //    MySqlCommand cmd = new MySqlCommand(Query);
        //    DataTable dt = GetData(cmd);
        //    if (dt.Rows[0]["Indicador"].ToString() == "0")
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}

        //protected string nombre_alumno(string matricula)
        //{
        //    string nombre = "";
        //    string Query = "";
        //    Query = "SELECT tpers_num, CONCAT(tpers_nombre,' ',tpers_materno,' ',tpers_paterno) nombre, " +
        //        " tpers_nombre, tpers_paterno, tpers_materno FROM tpers WHERE tpers_id = '" + matricula + "'";
        //    try
        //    {
        //        MySqlCommand cmd = new MySqlCommand(Query);
        //        DataTable dt = GetData(cmd);
        //        nombre = dt.Rows[0]["nombre"].ToString();
        //        lbl_id_pers.Text = dt.Rows[0]["tpers_num"].ToString();
        //        Global.cuenta = txt_matricula.Text;
        //        Global.nombre = dt.Rows[0]["tpers_nombre"].ToString();
        //        Global.ap_paterno = dt.Rows[0]["tpers_paterno"].ToString();
        //        Global.ap_materno = dt.Rows[0]["tpers_materno"].ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        string mensaje_error = ex.Message.Replace("'", "-");
        //        Global.inserta_log(mensaje_error, "talte", Session["usuario"].ToString());
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
        //    }
        //    return nombre;
        //}

        //protected string consecutivo(string id_num)
        //{
        //    string consecutivo = "";
        //    string Query = "";
        //    Query = "select IFNULL(max(talte_consec),0)+1 consecutivo from talte where talte_tpers_num in (select tpers_num from tpers where tpers_id='" + txt_matricula.Text + "') and talte_ttele_clave='" + ddl_tipo_telefono.SelectedValue + "'";
        //    try
        //    {
        //        MySqlCommand cmd = new MySqlCommand(Query);
        //        DataTable dt = GetData(cmd);
        //        consecutivo = dt.Rows[0]["consecutivo"].ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        string mensaje_error = ex.Message.Replace("'", "-");
        //        Global.inserta_log(mensaje_error, "talte", Session["usuario"].ToString());
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
        //    }

        //    return consecutivo;
        //}
        protected void GridTelefono_SelectedIndexChanged(object sender, EventArgs e)
        {
            btn_save.Visible = false;
            btn_update.Visible = true;
            GridViewRow row = GridTelefono.SelectedRow;
            txt_matricula.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);
            txt_nombre.Text = HttpUtility.HtmlDecode(row.Cells[3].Text);
            combo_tipo_telefono();
            ddl_tipo_telefono.SelectedValue = row.Cells[4].Text;
            lbl_consecutivo.Text = row.Cells[6].Text;
            txt_lada.Text = row.Cells[7].Text;
            txt_telefono.Text = row.Cells[8].Text;
            txt_extension.Text = HttpUtility.HtmlDecode(row.Cells[9].Text.Trim());
            //txt_telefono.ReadOnly = true;
            btn_cancel.Visible = true;
            combo_estatus();
            ddl_estatus.SelectedValue = row.Cells[10].Text;
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
                Global.inserta_log(mensaje_error, "talte", Session["usuario"].ToString());
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
            btn_save.Visible = true;
            btn_update.Visible = false;
            GridAlumnos.Visible = false;
            //txt_matricula.ReadOnly = true;
            btn_cancel.Visible = true;
            //txt_matricula.Attributes.Add("readonly", "");
            Global.cuenta = txt_matricula.Text;
            Global.nombre = HttpUtility.HtmlDecode(row.Cells[2].Text);
            Global.ap_paterno = HttpUtility.HtmlDecode(row.Cells[3].Text);
            Global.ap_materno = HttpUtility.HtmlDecode(row.Cells[4].Text);
            id_num = row.Cells[5].Text;
            grid_telefono_bind(txt_matricula.Text);
        }

        protected void consulta_docs_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Tctel.aspx");
        }

        protected void linkBttnBusca_Click(object sender, EventArgs e)
        {
            GridTelefono.Visible = false;
            try
            {
                Carga_Estudiante();

            }
            catch (Exception ex)
            {
                //Logs
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "talte", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }


        protected void btn_consulta_tel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Tctel.aspx");
        }
    }
}