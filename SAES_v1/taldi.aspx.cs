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
using static SAES_Services.Catalogos;
using static System.Net.Mime.MediaTypeNames;

namespace SAES_v1
{
    public partial class taldi : System.Web.UI.Page
    {
        #region <Variables>
        //Utilidades utils = new Utilidades();
        AlumnoService serviceAlumno = new AlumnoService();
        List<ModelObtenPaisesResponse> lstPaises = new List<ModelObtenPaisesResponse>();
        Catalogos serviceCatalogo = new Catalogos();
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
                    combo_tipo_direccion();
                    combo_estatus();
                    combo_pais();
                    txt_matricula.Text = Global.cuenta;
                    txt_nombre.Text = Global.nombre + " " + Global.ap_paterno + " " + Global.ap_materno;
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
        //private void Llena_pagina()
        //{
        //    System.Threading.Thread.Sleep(50);

        //    string QerySelect = "select tusme_update, tusme_select from tuser, tusme, tmede " +
        //                      " where tuser_clave = '" + Session["usuario"].ToString() + "'" +
        //                      " and tusme_trole_clave = tuser_trole_clave and tusme_tmenu_clave = 3 and tusme_tmenu_clave = tmede_tmenu_clave " +
        //                      " and tusme_tmede_clave = tmede_clave and tmede_forma='taldi' ";

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
        //            btn_taldi.Visible = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //resultado.Text = ex.Message;
        //        string mensaje_error = ex.Message.Replace("'", "-");
        //        Global.inserta_log(mensaje_error, "taldi", Session["usuario"].ToString());
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
        //    }
        //    conexion.Close();
        //}


        private void LlenaPagina()
        {
            ModelObtenerPermisoFormResponse objPermiso = new ModelObtenerPermisoFormResponse();

            try
            {
                objPermiso = servicePermiso.ObtenerPermisoFormulario(Session["usuario"].ToString(), "taldi");
                if (objPermiso != null)
                {
                    if (objPermiso.usme_update == "0" || objPermiso.usme_select == "0")
                    {

                        btn_taldi.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);

                        //grid_bind_estados();
                    }
                    else
                    {
                        grid_direccion_bind(txt_matricula.Text);
                    }
                }
                else
                {
                    btn_taldi.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                }
                //grid_bind_estados();

            }
            catch (Exception ex)
            {
                //Logs
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "taldi", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void combo_tipo_direccion()
        {
            List<ModeltdireResponse> lstDireccion = new List<ModeltdireResponse>();
            List<ModeltdireResponse> lstDireccionStatus = new List<ModeltdireResponse>();

            try
            {
                lstDireccion = serviceCatalogo.QRY_TDIRE();
                lstDireccionStatus = (from lst in lstDireccion
                                      where lst.C_ESTATUS == "A"
                                      select lst).ToList();


                ddl_tipo_direccion.DataSource = lstDireccionStatus;
                ddl_tipo_direccion.DataValueField = "clave";
                ddl_tipo_direccion.DataTextField = "nombre";
                ddl_tipo_direccion.DataBind();


                ddl_tipo_direccion.Items.Add(new ListItem("--------", "0"));
            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "taldi", Session["usuario"].ToString());
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

                ddl_pais.DataSource = serviceCatalogo.ObtenerPaisesActivos();
                ddl_pais.DataValueField = "Clave";
                ddl_pais.DataTextField = "Nombre";
                ddl_pais.DataBind();
                ddl_pais.SelectedValue = "139";
                combo_estado("139");

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "taldi", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void combo_estado(string c_pais)
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
                ddl_estado_SelectedIndexChanged(null, null);

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "taldi", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void combo_delegacion(string c_pais, string c_estado)
        {
            try
            {
                //ddl_delegacion.DataSource = serviceCatalogo.ObtenerDelegaciones(ddl_pais.SelectedValue, ddl_estado.SelectedValue, "A");
                ddl_delegacion.DataSource = serviceCatalogo.ObtenerMunicipios(ddl_pais.SelectedValue, ddl_estado.SelectedValue);
                ddl_delegacion.DataValueField = "Clave";
                ddl_delegacion.DataTextField = "Nombre";
                ddl_delegacion.DataBind();
            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "taldi", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void combo_colonia(string c_pais, string c_estado, string c_delegacion, string zip)
        {
            ddl_colonia.Items.Clear();
            try
            {
                ddl_colonia.DataSource = serviceCatalogo.obtenColoniasCP(txt_zip.Text);
                ddl_colonia.DataValueField = "clave";
                ddl_colonia.DataTextField = "nombre";
                ddl_colonia.DataBind();

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "taldi", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void Carga_Estudiante()
        {
            ModelObtenerAlumnosResponse datosAlumno = new ModelObtenerAlumnosResponse();
            txt_nombre.Text = string.Empty;
            DataTable dtAlumno = new DataTable();
            btn_save.Visible = true;
            btn_update.Visible = false;
            btn_cancelar.Visible = false;

            //txt_nombre.Text = string.Empty;
            ddl_tipo_direccion.Enabled = false;
            ddl_tipo_direccion.SelectedIndex = 0;
            ddl_estatus.SelectedIndex = 0;
            ddl_pais.SelectedIndex = 0;

            ddl_estado.Items.Clear();
            ddl_estado.Items.Add(new ListItem("--------", ""));
            ddl_estado.SelectedIndex = 0;


            ddl_delegacion.Items.Clear();
            ddl_delegacion.Items.Add(new ListItem("--------", ""));
            ddl_delegacion.SelectedIndex = 0;

            txt_zip.Text = string.Empty;
            ddl_colonia.SelectedIndex = 0;
            txt_ciudad.Text = string.Empty;
            txt_direccion.Text = string.Empty;
            GridAlumnos.Visible = false;

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
                    btn_save.Visible = true;
                    btn_update.Visible = false;
                    btn_cancelar.Visible = true;
                    grid_direccion_bind(txt_matricula.Text);
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


                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "NoexisteAlumno", "NoexisteAlumno();", true);


                }
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "taldi", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }

            //Estudiantes.Visible = true;


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
            try
            {
                combo_estado(ddl_pais.SelectedValue);
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "taldi", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void ddl_estado_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridCP.Visible = false;
          
                combo_delegacion(ddl_pais.SelectedValue, ddl_estado.SelectedValue);
          
        }

        protected void ddl_delegacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddl_colonia.SelectedIndex = 0;
            txt_zip.Text = string.Empty;
            txt_ciudad.Text = string.Empty;
            txt_direccion.Text = string.Empty;
            ddl_colonia.Items.Clear();
            ddl_colonia.Items.Add(new ListItem("--------", ""));
            BuscaCP();
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
            ddl_colonia.Items.Clear();
            ddl_colonia.Items.Add(new ListItem("--------", "0"));
            txt_ciudad.Text = string.Empty;
            txt_direccion.Text = string.Empty;
            try
                {

                    datos = serviceCatalogo.obtenDatosCP(txt_zip.Text, ddl_estado.SelectedValue, ddl_delegacion.SelectedValue);
                    if (datos.Count == 1)
                    {
                        if (datos[0].testa != null && datos[0].tdele != null)
                        {
                            combo_estado(ddl_pais.SelectedValue);
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
                        ddl_estado.SelectedIndex = 0;
                        ddl_delegacion.SelectedIndex = 0;
                        ddl_colonia.SelectedIndex = 0;
                        txt_ciudad.Text = string.Empty;
                        txt_direccion.Text = string.Empty;
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

        protected bool valida_direccion(string matricula)
        {
            string Query = "";
            Query = "SELECT COUNT(*) Indicador FROM taldi WHERE taldi_tpers_num = (SELECT DISTINCT tpers_num FROM tpers WHERE tpers_id='" + matricula + "')";
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

            combo_estado(ddl_pais.SelectedValue);
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
            GridAlumnos.Visible = false;
            GridDireccion.Visible = true;
            txt_matricula.Text = row.Cells[1].Text;
            txt_nombre.Text = HttpUtility.HtmlDecode(row.Cells[2].Text) + " " + HttpUtility.HtmlDecode(row.Cells[3].Text) + " " +
             HttpUtility.HtmlDecode(row.Cells[4].Text);
            btn_save.Visible = false;
            btn_update.Visible = true;
            btn_cancelar.Visible = true;
            GridAlumnos.Visible = false;
            txt_matricula.ReadOnly = true; //.Attributes.Add("readonly", "");
            btn_save.Visible = false;
            btn_update.Visible = true;
            ddl_tipo_direccion.Enabled = true;
            Global.cuenta = txt_matricula.Text;
            Global.nombre = HttpUtility.HtmlDecode(row.Cells[2].Text);
            Global.ap_paterno = HttpUtility.HtmlDecode(row.Cells[3].Text);
            Global.ap_materno = HttpUtility.HtmlDecode(row.Cells[4].Text);
            Carga_Estudiante();
        }

        protected void grid_direccion_bind(string matricula)
        {

            try
            {
                GridDireccion.DataSource = serviceAlumno.ObtenerDirecciones(matricula);
                GridDireccion.DataBind();
                GridDireccion.Visible = true;

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "taldi", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }

        }
        protected void btn_cancelar_Click(object sender, EventArgs e)
        {
            try
            {
                btn_update.Visible = false;
                btn_cancelar.Visible = false;
                btn_save.Visible = true;
                GridAlumnos.DataSource = null;
                GridAlumnos.DataBind();
                GridAlumnos.Visible = false;
                GridAlumnos.SelectedIndex = -1;
                GridDireccion.SelectedIndex = -1;
                //txt_matricula.Enabled = true;
                txt_matricula.Text = string.Empty;
                txt_nombre.Text = string.Empty;
                ddl_tipo_direccion.Enabled = true;
                ddl_tipo_direccion.SelectedIndex = 0;
                ddl_estatus.SelectedIndex = 0;
                ddl_pais.SelectedIndex = 0;
                ddl_pais_SelectedIndexChanged(null, null);
                ddl_estado.SelectedIndex = 0;
                ddl_estado_SelectedIndexChanged(null, null);
                ddl_delegacion.SelectedIndex = 0;
                ddl_delegacion_SelectedIndexChanged(null, null);
                ddl_colonia.SelectedIndex = 0;
                txt_zip.Text = string.Empty;
                txt_ciudad.Text = string.Empty;
                txt_direccion.Text = string.Empty;
                txt_matricula.Focus();
                txt_matricula.ReadOnly = false;
                GridDireccion.DataSource = null;
                GridDireccion.DataBind();
            }
            catch (Exception ex)
            {
                //Logs
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "taldi", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }
        protected void GridDireccion_SelectedIndexChanged(object sender, EventArgs e)
        {
            btn_save.Visible = false;
            btn_update.Visible = true;
            btn_cancelar.Visible = false;
            btn_cancelar_update.Visible = true;
            txt_matricula.ReadOnly = false;
            GridViewRow row = GridDireccion.SelectedRow;
            txt_matricula.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);
            txt_nombre.Text = HttpUtility.HtmlDecode(row.Cells[3].Text);
            //combo_tipo_direccion();
            try
            {
                ddl_tipo_direccion.SelectedValue = row.Cells[4].Text;
            }
            catch
            {
                ddl_tipo_direccion.SelectedIndex = 0;
            }
            ddl_estatus.SelectedValue = row.Cells[8].Text;
            ddl_pais.SelectedValue = row.Cells[11].Text;
            txt_zip.Text = row.Cells[14].Text;
            txt_zip_TextChanged(null, null);
            //combo_pais();
            //combo_estado(ddl_pais.SelectedValue);
            ddl_estado.SelectedValue = row.Cells[12].Text;
            ddl_estado_SelectedIndexChanged(null, null);
            //combo_delegacion(ddl_pais.SelectedValue, ddl_estado.SelectedValue);
            ddl_delegacion.SelectedValue = row.Cells[13].Text;
            //ddl_delegacion_SelectedIndexChanged(null, null);


            //txt_zip.Text = row.Cells[14].Text;
            combo_colonia(ddl_pais.SelectedValue, ddl_estado.SelectedValue, ddl_delegacion.SelectedValue, txt_zip.Text);
           
              ddl_colonia.SelectedItem.Text = row.Cells[15].Text;

            ListItem colonia = new ListItem();
            colonia = ddl_colonia.Items.FindByText(HttpUtility.HtmlDecode(row.Cells[15].Text));
            ddl_colonia.SelectedValue = colonia.Value;


            txt_ciudad.Text = HttpUtility.HtmlDecode(row.Cells[16].Text);
            txt_direccion.Text = HttpUtility.HtmlDecode(row.Cells[7].Text);
            //txt_direccion.Text = row.Cells[17].Text;
            lbl_consecutivo.Text = row.Cells[6].Text;
            ddl_tipo_direccion.Attributes.Add("disabled", "");
            // grid_direccion_bind(txt_matricula.Text);
        }

        protected void linkBttnBusca_Click(object sender, EventArgs e)
        {
            GridDireccion.Visible = false;
            try
            {
                Carga_Estudiante();

                //if (txt_matricula.Text != string.Empty)
                //{



                //}
                //else
                //{


                //    GridDireccion.DataSource = null;
                //    GridDireccion.DataBind();

                //    GridAlumnos.Visible = true;
                //    GridAlumnos.DataSource = serviceAlumno.ObtenerAlumnos(txt_matricula.Text);
                //    GridAlumnos.DataBind();
                //}
            }
            catch (Exception ex)
            {
                //Logs
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "taldi", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txt_ciudad.Text) && !String.IsNullOrEmpty(txt_direccion.Text) && !String.IsNullOrEmpty(txt_matricula.Text) && !String.IsNullOrEmpty(txt_zip.Text) && ddl_tipo_direccion.SelectedValue != "0" && ddl_pais.SelectedValue != "0" && ddl_estado.SelectedValue != "0" && ddl_delegacion.SelectedValue != "0" && ddl_colonia.SelectedValue != "0")
            {

                //string Query = "INSERT INTO taldi (taldi_tpers_num,taldi_consec,taldi_tdire_clave,taldi_calle,taldi_colonia,taldi_testa_clave,taldi_tdele_clave,taldi_tpais_clave,taldi_tcopo_clave,taldi_ciudad,taldi_estatus,taldi_date,taldi_user) " +
                //    "VALUES ( " +
                //              "( select tpers_num from tpers where tpers_id='" + txt_matricula.Text + "')," + 
                //              consecutivo(lbl_id_pers.Text) + ",'" + ddl_tipo_direccion.SelectedValue + "','" + txt_direccion.Text + "','" + 
                //              ddl_colonia.SelectedItem.Text + "','" + ddl_estado.SelectedValue + "','" + ddl_delegacion.SelectedValue + "','" + 
                //              ddl_pais.SelectedValue + "','" + txt_zip.Text + "','" + txt_ciudad.Text + "','" + ddl_estatus.SelectedValue + "'," +
                //              "CURRENT_TIMESTAMP(),'" + Session["usuario"].ToString() + "')";

                try
                {
                    serviceAlumno.InsertarDireccion(txt_matricula.Text, ddl_tipo_direccion.SelectedValue, txt_direccion.Text, ddl_colonia.SelectedItem.Text, ddl_estado.SelectedValue,
                        ddl_delegacion.SelectedValue, ddl_pais.SelectedValue, txt_zip.Text, txt_ciudad.Text, ddl_estatus.Text, Session["usuario"].ToString());
                    txt_zip.Text = null;
                    txt_ciudad.Text = null;
                    txt_direccion.Text = null;
                    ddl_tipo_direccion.SelectedIndex = 0;

                    ddl_pais.SelectedIndex = 0;

                    ddl_estado.Items.Clear();
                    ddl_estado.Items.Add(new ListItem("--------", ""));
                    ddl_estado.SelectedIndex = 0;

                    ddl_delegacion.Items.Clear();
                    ddl_delegacion.Items.Add(new ListItem("--------", ""));
                    ddl_delegacion.SelectedIndex = 0;

                    ddl_colonia.Items.Clear();
                    ddl_colonia.Items.Add(new ListItem("--------", ""));
                    ddl_colonia.SelectedIndex = 0;

                    btn_cancelar.Visible = true;
                    //combo_pais();
                    //combo_estatus();
                    //combo_tipo_direccion();
                    grid_direccion_bind(txt_matricula.Text);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                }
                catch (Exception ex)
                {
                    string test = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "taldi", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
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
                //string Query = "UPDATE taldi SET taldi_calle='" + txt_direccion.Text + "'," +
                //    "taldi_colonia='" + ddl_colonia.SelectedItem.Text + "'" +
                //    ",taldi_testa_clave='" + ddl_estado.SelectedValue + "'" +
                //    ",taldi_tdele_clave='" + ddl_delegacion.SelectedValue + "'" +
                //    ",taldi_tpais_clave='" + ddl_pais.SelectedValue + "" +
                //    "',taldi_tcopo_clave='" + txt_zip.Text + "'" +
                //    ",taldi_ciudad='" + txt_ciudad.Text + "'" +
                //    ",taldi_estatus='" + ddl_estatus.SelectedValue + "'" +
                //    ",taldi_date=current_timestamp(),taldi_user='" + Session["usuario"].ToString() + "' " +
                //    "WHERE taldi_tpers_num=(select tpers_num from tpers where tpers_id='" + txt_matricula.Text + "') " +
                //    "AND taldi_consec='" + lbl_consecutivo.Text + "' AND taldi_tdire_clave='" + ddl_tipo_direccion.SelectedValue + 
                //    "'";

                try
                {

                    serviceAlumno.EditarDireccion(txt_matricula.Text, ddl_tipo_direccion.SelectedValue, txt_direccion.Text, ddl_colonia.SelectedItem.Text,
                        ddl_estado.SelectedValue, ddl_delegacion.SelectedValue, ddl_pais.SelectedValue, txt_zip.Text, txt_ciudad.Text,
                         ddl_estatus.SelectedValue, Session["usuario"].ToString(), lbl_consecutivo.Text);

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "update_p", "update();", true);
                    ddl_estatus.SelectedIndex = 0;
                    ddl_pais.SelectedIndex = 0;
                    ddl_pais_SelectedIndexChanged(null, null);
                    txt_zip.Text = string.Empty;
                    ddl_colonia.SelectedIndex = 0;
                    txt_ciudad.Text = string.Empty;
                    txt_direccion.Text = string.Empty;
                    ddl_tipo_direccion.Enabled = false;
                    btn_save.Visible = true;
                    btn_update.Visible = false;
                    btn_cancelar.Visible = true;
                    btn_cancelar_update.Visible = false;
                    ddl_colonia.Items.Clear();
                    ddl_colonia.Items.Add(new ListItem("--------", ""));
                    grid_direccion_bind(txt_matricula.Text);
                    GridDireccion.SelectedIndex = -1;
                }
                catch (Exception ex)
                {
                    string test = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "taldi", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                }

            }
            else
            {
                if (!String.IsNullOrEmpty(txt_matricula.Text))
                {
                    grid_direccion_bind(txt_matricula.Text);
                }
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_direccion();", true);
            }
        }


        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            txt_matricula.Text = null;
            txt_nombre.Text = null;
            ddl_tipo_direccion.Attributes.Remove("disabled");
            combo_tipo_direccion();
            combo_pais();
            combo_estatus();
            txt_direccion.Text = null;
            txt_ciudad.Text = null;
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
            //GridDireccion.Visible = false;
        }

        protected void txt_ciudad_TextChanged(object sender, EventArgs e)
        {
            txt_direccion.Focus();
        }

        protected void txt_direccion_TextChanged(object sender, EventArgs e)
        {
            txt_ciudad.Focus();
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
                    Global.inserta_log(mensaje_error, "taldi", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                }
                conexion.Close();
            }
        }

        protected void consulta_docs_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Tcdir.aspx");
        }

        protected void linkBttnBuscaCP_Click(object sender, EventArgs e)
        {
            if (GridCP.Visible == false)
                BuscaCP();
            else
            {
                GridCP.Visible = false;
            }

        }

        protected void btn_cancelar_update_Click(object sender, EventArgs e)
        {
            btn_cancelar.Visible = true;
            btn_cancelar_update.Visible = false;
            btn_save.Visible = true;
            btn_update.Visible = false;
            ddl_tipo_direccion.SelectedIndex = 0;
            ddl_estatus.SelectedIndex = 0;
            ddl_pais.SelectedIndex = 0;
            txt_zip.Text = string.Empty;
            ddl_estado.SelectedIndex = 0;
            ddl_delegacion.SelectedIndex = 0;
            ddl_colonia.SelectedIndex = 0;
            txt_ciudad.Text = string.Empty;
            txt_direccion.Text = string.Empty;
            ddl_colonia.SelectedIndex = 0;
            GridDireccion.SelectedIndex = -1;
        }

       
    }
}