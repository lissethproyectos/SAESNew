using iTextSharp.text.pdf;
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
    public partial class tcodi : System.Web.UI.Page
    {
        #region <Variables>
        //Utilidades utils = new Utilidades();
        AlumnoService serviceAlumno = new AlumnoService();
        CobranzaService serviceCobranza = new CobranzaService();
        ContactoService serviceContacto = new ContactoService();
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
                    LlenaPagina();
                    combo_tipo_direccion();
                    combo_estatus();
                    combo_pais();
                    txt_matricula.Text = Global.cuenta;
                    txt_nombre.Text = Global.nombre + " " + Global.ap_paterno + " " + Global.ap_materno;
                    if (txt_matricula.Text != null)
                    {
                        combo_contacto();
                    }
                    grid_direccion_bind(txt_matricula.Text);

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

        private void LlenaPagina()
        {
            ModelObtenerPermisoFormResponse objPermiso = new ModelObtenerPermisoFormResponse();
            objPermiso.usme_update = "0";
            objPermiso.usme_select = "0";
            System.Threading.Thread.Sleep(50);
            try
            {
                objPermiso = servicePermiso.ObtenerPermisoFormulario(Session["usuario"].ToString(), "tcodi");
                if (objPermiso != null)
                {
                    if (objPermiso.usme_update == "0" || objPermiso.usme_select == "0")
                    {
                        btn_tcodi.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                    }
                    else
                        grid_direccion_bind(txt_matricula.Text);
                }
                else
                {
                    btn_tcodi.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                }


            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcodi", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);

            }
        }


        protected void combo_contacto()
        {
            //string strQuerytcont = "";
            //strQuerytcont = "select tcoda_consecutivo clave, tcont_desc contacto from tcoda, tcont where tcont_estatus='A'  " +
            //                " and tcoda_tpers_num in (select tpers_num from tpers where tpers_id='" + txt_matricula.Text + "') and tcoda_tipo=tcont_clave " +
            //                "union " +
            //                "select '0' clave, '-------' contacto " +
            //                "order by 1";
            try
            {

                ddl_tcont.DataSource = serviceAlumno.ObtenerContactos(txt_matricula.Text);
                ddl_tcont.DataValueField = "clave";
                ddl_tcont.DataTextField = "descripcion";
                ddl_tcont.DataBind();

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcodi", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void combo_tipo_direccion()
        {
            
            try
            {
                ddl_tipo_direccion.DataSource = serviceCatalogo.QRY_TDIRE();
                ddl_tipo_direccion.DataValueField = "clave";
                ddl_tipo_direccion.DataTextField = "nombre";
                ddl_tipo_direccion.DataBind();

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcodi", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }
        protected void combo_estatus()
        {
            ddl_estatus.Items.Clear();
            ddl_estatus.Items.Add(new ListItem("Activo", "A"));
            ddl_estatus.Items.Add(new ListItem("Inactivo", "B"));
        }

        protected void combo_pais()
        {
            ddl_estado.Items.Clear();
            ddl_estado.Items.Add(new ListItem("--------", "0"));
            ddl_delegacion.Items.Clear();
            ddl_delegacion.Items.Add(new ListItem("-------", "0"));
            ddl_colonia.Items.Clear();
            ddl_colonia.Items.Add(new ListItem("--------", "0"));


            try
            {

                ddl_pais.DataSource = serviceCatalogo.ObtenerPaises();
                ddl_pais.DataValueField = "clave";
                ddl_pais.DataTextField = "nombre";
                ddl_pais.DataBind();
                ddl_pais.SelectedValue = "139";
                combo_estado();

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcodi", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }

        }

        protected void combo_estado()
        {
            ddl_estado.Items.Clear();
            ddl_delegacion.Items.Clear();
            ddl_delegacion.Items.Add(new ListItem("--------", "0"));

            try
            {
                ddl_estado.DataSource = serviceCatalogo.ObtenerEstados(ddl_pais.SelectedValue);
                ddl_estado.DataValueField = "clave";
                ddl_estado.DataTextField = "descripcion";
                ddl_estado.DataBind();

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcodi", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }


        protected void combo_delegacion(string c_pais, string c_estado)
        {

            try
            {
                ddl_delegacion.DataSource = serviceCatalogo.ObtenerDelegaciones(c_pais, c_estado, "A");
                ddl_delegacion.DataValueField = "Clave";
                ddl_delegacion.DataTextField = "Nombre";
                ddl_delegacion.DataBind();

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcodi", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void combo_colonia(string c_pais, string c_estado, string c_delegacion, string zip)
        {
            ddl_colonia.Items.Clear();
            string Query = "SELECT DISTINCT ROW_NUMBER() OVER (PARTITION BY tcopo_clave,tcopo_tpais_clave,tcopo_testa_clave,tcopo_tdele_clave ORDER BY tcopo_clave) Clave, tcopo_desc Nombre FROM tcopo WHERE tcopo_clave='" + txt_zip.Text + "' " +
                            "UNION " +
                            "SELECT '0' CLAVE,'--------' NOMBRE " +
                            "ORDER BY 1";        
            try
            {
                ddl_colonia.DataSource = serviceCatalogo.QRY_TCOPO(ddl_pais.SelectedValue, ddl_estado.SelectedValue, ddl_delegacion.SelectedValue);
                ddl_colonia.DataValueField = "Clave";
                ddl_colonia.DataTextField = "Nombre";
                ddl_colonia.DataBind();
            }
            catch (Exception ex)
            {
                string test = ex.Message;
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
            GridAlumnos.Visible = false;


            try
            {
                dtAlumno = serviceAlumno.ObtenerAlumnos(txt_matricula.Text);
                if (dtAlumno.Rows.Count == 1)
                {
                    datosContacto.Visible = true;
                    txt_nombre.Text = dtAlumno.Rows[0][1].ToString() + " " + dtAlumno.Rows[0][2].ToString() + " " + dtAlumno.Rows[0][3].ToString();
                    Global.cuenta = txt_matricula.Text;
                    Global.nombre = dtAlumno.Rows[0][1].ToString();
                    Global.ap_paterno = dtAlumno.Rows[0][2].ToString();
                    Global.ap_materno = dtAlumno.Rows[0][3].ToString();
                    combo_contacto();
                    grid_direccion_bind(txt_matricula.Text);
                }
                else if (dtAlumno.Rows.Count > 1)
                {
                    GridAlumnos.Visible = true;
                    GridAlumnos.DataSource = dtAlumno;
                    GridAlumnos.DataBind();
                    GridDireccion.Visible = false;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "NoexisteAlumno", "NoexisteAlumno();", true);
                    txt_matricula.Text = null;
                    txt_matricula.Focus();
                    GridDireccion.DataSource = null;
                    GridDireccion.DataBind();
                }
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcodi", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }

        }

        protected void Carga_Estudiante(object sender, EventArgs e)
        {
            Carga_Estudiante();
        }

        protected void txt_matricula_TextChanged(object sender, EventArgs e)
        {
            /*if (valida_matricula(txt_matricula.Text))
            {

                if (valida_direccion(txt_matricula.Text))
                {
                    txt_nombre.Text = nombre_alumno(txt_matricula.Text);
                    grid_direccion_bind(txt_matricula.Text);
                }
                else if (txt_matricula.Text.Contains("%"))
                {
                    grid_direccion_bind(txt_matricula.Text);
                }
                else
                {
                    txt_nombre.Text = nombre_alumno(txt_matricula.Text);
                }

            }
            else
            {
                ///Matricula no existe
            }*/
        }

        protected void ddl_pais_SelectedIndexChanged(object sender, EventArgs e)
        {
            combo_estado();

        }

        protected void ddl_estado_SelectedIndexChanged(object sender, EventArgs e)
        {
            combo_delegacion(ddl_pais.SelectedValue, ddl_estado.SelectedValue);

        }

        protected void ddl_delegacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_zip.Text = string.Empty;
            ddl_colonia.Items.Clear();
            ddl_colonia.Items.Add(new ListItem("--------", ""));
            txt_ciudad.Text = string.Empty;
            txt_direccion.Text = string.Empty;
            GridCP.Visible = false;
            //BuscaCP();
        }


        protected void txt_zip_TextChanged(object sender, EventArgs e)
        {
            BuscaCP();
        }

        protected void BuscaCP()
        {
            List<ModelObtenDatosCPResponse> datos = new List<ModelObtenDatosCPResponse>();

            GridCP.Visible = false;
            GridCP.DataSource = null;
            GridCP.DataBind();
            try
            {
                datos = serviceCatalogo.obtenDatosCP(txt_zip.Text, ddl_estado.SelectedValue, ddl_delegacion.SelectedValue);
                if (datos.Count == 1)
                {
                    if (datos[0].testa != null && datos[0].tdele != null)
                    {
                        combo_estado();
                        ddl_estado.SelectedValue = datos[0].testa;
                        combo_delegacion(ddl_pais.SelectedValue, ddl_estado.SelectedValue);
                        ddl_delegacion.SelectedValue = datos[0].tdele;
                        combo_colonia(ddl_pais.SelectedValue, ddl_estado.SelectedValue, ddl_delegacion.SelectedValue, txt_zip.Text);
                 
                    }
                }
                else if (datos.Count > 1)
                {
                    GridCP.DataSource = datos;
                    GridCP.DataBind();
                    GridCP.Visible = true;
                }
                else if (datos.Count == 0 && txt_zip.Text == string.Empty)
                {
                    GridCP.DataSource = null;
                    GridCP.DataBind();
                    GridCP.Visible = true;
                }
                else
                {
                    txt_zip.Text = "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "NoExiste", "NoExiste();", true);
                }
            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "taldi", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
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

        protected bool valida_direccion(string matricula)
        {
            string Query = "";
            Query = "SELECT COUNT(*) Indicador FROM tcodi WHERE tcodi_tpers_num = (SELECT DISTINCT tpers_num FROM tpers WHERE tpers_id='" + matricula + "')";
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


        protected void GridCP_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridCP.SelectedRow;
            txt_zip.Text = row.Cells[1].Text;

            combo_estado();
            ddl_estado.SelectedValue = row.Cells[3].Text;

            combo_delegacion(ddl_pais.SelectedValue, ddl_estado.SelectedValue);
            ddl_delegacion.SelectedValue = row.Cells[4].Text;

            combo_colonia(ddl_pais.SelectedValue, ddl_estado.SelectedValue, ddl_delegacion.SelectedValue, txt_zip.Text);
            ListItem colonia = new ListItem();
            colonia = ddl_colonia.Items.FindByText(HttpUtility.HtmlDecode(row.Cells[2].Text));
            ddl_colonia.SelectedValue = colonia.Value;
            GridCP.Visible = false;

        }

        protected void GridAlumnos_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridAlumnos.SelectedRow;
            txt_matricula.Text = row.Cells[1].Text;
            txt_nombre.Text = HttpUtility.HtmlDecode(row.Cells[2].Text) + " " + HttpUtility.HtmlDecode(row.Cells[3].Text) + " " +
             HttpUtility.HtmlDecode(row.Cells[4].Text);
            GridAlumnos.Visible = false;
            txt_matricula.Attributes.Add("readonly", "");
            btn_save.Visible = true;
            btn_update.Visible = false;
            //btn_cancel_save.Visible = true;
            btn_cancel_update.Visible = false;

            Global.cuenta = txt_matricula.Text;
            Global.nombre = HttpUtility.HtmlDecode(row.Cells[2].Text);
            Global.ap_paterno = HttpUtility.HtmlDecode(row.Cells[3].Text);
            Global.ap_materno = HttpUtility.HtmlDecode(row.Cells[4].Text);
            datosContacto.Visible = true;
            combo_contacto();
            grid_direccion_bind(txt_matricula.Text);

            //Carga_Estudiante();
        }

        protected void grid_direccion_bind(string matricula)
        {
            //string strQueryDir = "";
            //strQueryDir = " select tpers_num id_num,tpers_id clave, concat(tpers_nombre,' ',tpers_paterno,' ',tpers_materno) nombre, " +
            //               "  tcodi_tdire_clave tipo_dir, tdire_desc descripcion, tcodi_consec consecutivo, tcodi_calle direccion, " +
            //               " tcodi_estatus c_estatus, case when tcodi_estatus='A' then 'Activo' else 'Inactivo' end estatus,fecha(date_format(tcodi_date,'%Y-%m-%d')) fecha,tcodi_tpais_clave,tcodi_testa_clave,tcodi_tdele_clave,tcodi_tcopo_clave,tcodi_colonia,tcodi_ciudad, " +
            //               " tcoda_consecutivo cl_contacto, tcont_desc contacto " +
            //               " from ( SELECT tpers_id matricula, concat(tpers_nombre,' ',tpers_paterno,' ',tpers_materno) alumno  from tpers where tpers_tipo='E') estu, tcodi, tpers, tdire, tcoda, tcont " +
            //               " where tpers_id=estu.matricula and tpers_num=tcodi_tpers_num and   tdire_clave=tcodi_tdire_clave and tpers_id like '" + matricula + "'" +
            //               " and   tcoda_tpers_num=tcodi_tpers_num and tcoda_consecutivo=tcodi_tcoda_consec and tcoda_tipo=tcont_clave ";
            //MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            //ConexionMySql.Open();
            try
            {


                GridDireccion.DataSource = serviceContacto.ObtenerDirecciones(txt_matricula.Text);
                GridDireccion.DataBind();
                GridDireccion.DataMember = "Direccion";
                GridDireccion.Visible = true;
                if (txt_matricula.Text != string.Empty && txt_nombre.Text != string.Empty)
                    datosContacto.Visible = true;



            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcodi", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }

        }

        protected void GridDireccion_SelectedIndexChanged(object sender, EventArgs e)
        {
            //btn_save.Visible = false;
            //btn_update.Visible = true;


            btn_save.Visible = false;
            btn_update.Visible = true;
            //btn_cancel_save.Visible = false;
            btn_cancel_update.Visible = true;

            GridViewRow row = GridDireccion.SelectedRow;
            combo_tipo_direccion();
            ddl_tipo_direccion.SelectedValue = row.Cells[2].Text;
            txt_direccion.Text = HttpUtility.HtmlDecode(row.Cells[5].Text);
            combo_estatus();
            ddl_estatus.SelectedValue = row.Cells[6].Text;
            combo_pais();
            ddl_pais.SelectedValue = row.Cells[9].Text;
            combo_estado();
            ddl_estado.SelectedValue = row.Cells[10].Text;
            combo_delegacion(ddl_pais.SelectedValue, ddl_estado.SelectedValue);
            ddl_delegacion.SelectedValue = row.Cells[11].Text;
            txt_zip.Text = row.Cells[12].Text;
            combo_colonia(ddl_pais.SelectedValue, ddl_estado.SelectedValue, ddl_delegacion.SelectedValue, txt_zip.Text);
            ListItem colonia = new ListItem();
            colonia = ddl_colonia.Items.FindByText(HttpUtility.HtmlDecode(row.Cells[13].Text));
            try
            {
                ddl_colonia.SelectedItem.Text = row.Cells[13].Text;
            }
            catch
            {
                ddl_colonia.SelectedIndex = 0;
            }
            txt_ciudad.Text = HttpUtility.HtmlDecode(row.Cells[14].Text);
            combo_contacto();
            string pruebas = row.Cells[16].Text;
            ddl_tcont.SelectedValue = row.Cells[16].Text;
            lbl_consecutivo.Text = row.Cells[4].Text;
            ddl_tipo_direccion.Enabled = false;
            // grid_direccion_bind(txt_matricula.Text);
        }

        protected string nombre_alumno(string matricula)
        {
            string nombre = "";
            string Query = "";
            Query = "SELECT tpers_num, CONCAT(tpers_nombre,' ',tpers_materno,' ',tpers_paterno) nombre FROM tpers WHERE tpers_id = '" + matricula + "'";
            MySqlCommand cmd = new MySqlCommand(Query);
            DataTable dt = GetData(cmd);
            nombre = dt.Rows[0]["nombre"].ToString();
            lbl_id_pers.Text = dt.Rows[0]["tpers_num"].ToString();
            return nombre;
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            string cons = ddl_tcont.SelectedValue;
            if (!String.IsNullOrEmpty(txt_ciudad.Text) && !String.IsNullOrEmpty(txt_direccion.Text) && !String.IsNullOrEmpty(txt_matricula.Text) && !String.IsNullOrEmpty(txt_zip.Text) && ddl_tipo_direccion.SelectedValue != "0" && ddl_pais.SelectedValue != "0" && ddl_estado.SelectedValue != "0" && ddl_delegacion.SelectedValue != "0" && ddl_colonia.SelectedValue != "0" && ddl_tcont.SelectedValue != "0")
            {

                string Query = "INSERT INTO tcodi  VALUES ( " +
                              "( select tpers_num from tpers where tpers_id='" + txt_matricula.Text + "')," + ddl_tcont.SelectedValue + "," + consecutivo(lbl_id_pers.Text) + ",'" + ddl_tipo_direccion.SelectedValue + "','" + txt_direccion.Text + "','" + ddl_colonia.SelectedItem.Text + "','" + ddl_estado.SelectedValue + "','" + ddl_delegacion.SelectedValue + "','" + ddl_pais.SelectedValue + "','" + txt_zip.Text + "','" + txt_ciudad.Text + "','" + ddl_estatus.SelectedValue + "',CURRENT_TIMESTAMP(),'" + Session["usuario"].ToString() + "')";
                MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                conexion.Open();
                MySqlCommand mysqlcmd = new MySqlCommand(Query, conexion);
                mysqlcmd.CommandType = CommandType.Text;
                try
                {
                    mysqlcmd.ExecuteNonQuery();
                    txt_zip.Text = null;
                    txt_ciudad.Text = null;
                    txt_direccion.Text = null;
                    combo_pais();
                    combo_estatus();
                    combo_tipo_direccion();
                    combo_contacto();
                    grid_direccion_bind(txt_matricula.Text);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                }
                catch (Exception ex)
                {
                    string test = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tcodi", Session["usuario"].ToString());
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
                    grid_direccion_bind(txt_matricula.Text);
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_direccion();", true);
            }
        }

        protected void btn_update_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txt_ciudad.Text) && !String.IsNullOrEmpty(txt_direccion.Text) && !String.IsNullOrEmpty(txt_matricula.Text) && !String.IsNullOrEmpty(txt_zip.Text) && ddl_tipo_direccion.SelectedValue != "0" && ddl_pais.SelectedValue != "0" && ddl_estado.SelectedValue != "0" && ddl_delegacion.SelectedValue != "0" && ddl_colonia.SelectedValue != "0")
            {
                string Query = "UPDATE tcodi SET tcodi_calle='" + txt_direccion.Text + "',tcodi_colonia='" + ddl_colonia.SelectedItem.Text + "',tcodi_testa_clave='" + ddl_estado.SelectedValue + "',tcodi_tdele_clave='" + ddl_delegacion.SelectedValue + "',tcodi_tpais_clave='" + ddl_pais.SelectedValue + "',tcodi_tcopo_clave='" + txt_zip.Text + "',tcodi_ciudad='" + txt_ciudad.Text + "',tcodi_estatus='" + ddl_estatus.SelectedValue + "',tcodi_date=current_timestamp(),tcodi_user='" + Session["usuario"].ToString() + "' WHERE tcodi_tpers_num=(select tpers_num from tpers where tpers_id='" + txt_matricula.Text + "') AND tcodi_consec='" + lbl_consecutivo.Text + "' AND tcodi_tdire_clave='" + ddl_tipo_direccion.SelectedValue + "' and tcodi_tcoda_consec=" + ddl_tcont.SelectedValue;
                MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                conexion.Open();
                MySqlCommand mysqlcmd = new MySqlCommand(Query, conexion);
                mysqlcmd.CommandType = CommandType.Text;
                try
                {
                    mysqlcmd.ExecuteNonQuery();
                    txt_zip.Text = null;
                    txt_ciudad.Text = null;
                    txt_direccion.Text = null;
                    ddl_tipo_direccion.Enabled = true;
                    combo_pais();
                    combo_estatus();
                    combo_tipo_direccion();
                    combo_contacto();
                    grid_direccion_bind(txt_matricula.Text);
                    btn_update.Visible = false;
                    btn_save.Visible = true;
                    GridDireccion.SelectedIndex = -1;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "update_p", "update();", true);
                }
                catch (Exception ex)
                {
                    string test = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tcodi", Session["usuario"].ToString());
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
                    grid_direccion_bind(txt_matricula.Text);
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_direccion();", true);
            }
        }

        protected string consecutivo(string id_num)
        {
            string consecutivo = "";
            string Query = "";
            Query = "select IFNULL(max(tcodi_consec),0)+1 consecutivo from tcodi where tcodi_tpers_num in " +
                " (select tpers_num from tpers where tpers_id='" + txt_matricula.Text + "') and tcodi_tdire_clave='" + ddl_tipo_direccion.SelectedValue + "'";
            MySqlCommand cmd = new MySqlCommand(Query);
            DataTable dt = GetData(cmd);
            consecutivo = dt.Rows[0]["consecutivo"].ToString();
            lbl_consecutivo.Text = consecutivo;
            return consecutivo;
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            txt_matricula.Text = null;
            txt_nombre.Text = null;
            ddl_tipo_direccion.Enabled = true;
            combo_tipo_direccion();
            combo_pais();
            combo_estatus();
            txt_direccion.Text = null;
            txt_ciudad.Text = null;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
            GridDireccion.Visible = false;
        }

        protected void txt_ciudad_TextChanged(object sender, EventArgs e)
        {
            txt_direccion.Focus();
        }

        protected void txt_direccion_TextChanged(object sender, EventArgs e)
        {
            txt_ciudad.Focus();
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

        protected void btn_cancel_save_Click(object sender, EventArgs e)
        {

            txt_matricula.Text = null;
            txt_nombre.Text = null;
            txt_matricula.ReadOnly = false;
            ddl_tcont.SelectedIndex = 0;
            ddl_tipo_direccion.SelectedIndex = 0;
            ddl_estatus.SelectedIndex = 0;
            ddl_pais.SelectedIndex = 0;
            ddl_pais_SelectedIndexChanged(null, null);
            txt_zip.Text = null;
            txt_ciudad.Text = null;
            txt_ciudad.Text = null;
            datosContacto.Visible = true;
        }

        protected void btn_cancel_update_Click(object sender, EventArgs e)
        {
            ddl_tcont.SelectedIndex = 0;
            ddl_tipo_direccion.SelectedIndex = 0;
            ddl_estatus.SelectedIndex = 0;
            ddl_pais.SelectedIndex = 0;
            ddl_pais_SelectedIndexChanged(null, null);
            txt_zip.Text = null;
            txt_ciudad.Text = null;
            txt_ciudad.Text = null;
            GridDireccion.SelectedIndex = -1;
            btn_save.Visible = true;
            btn_update.Visible = false;
            //btn_cancel_save.Visible = true;
            btn_cancel_update.Visible = false;


        }

        protected void linkBttnBuscarCP_Click(object sender, EventArgs e)
        {
            BuscaCP();
        }
    }
}