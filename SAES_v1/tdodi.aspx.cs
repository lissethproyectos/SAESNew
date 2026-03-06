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
using static SAES_DBO.Models.ModelDocente;
using static SAES_DBO.Models.ModelMenu;

namespace SAES_v1
{
    public partial class tdodi : System.Web.UI.Page
    {
        #region <Variables>
        Utilidades utils = new Utilidades();
        Catalogos serviceCatalogo = new Catalogos();
        DocenteService serviceDocente = new DocenteService();
        List<ModelObtenPaisesResponse> lstPaises = new List<ModelObtenPaisesResponse>();
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
                    txt_matricula.Text = Global.clave_docente;
                    txt_nombre.Text = Global.nombre_docente + " " + Global.ap_paterno_docente + " " + Global.ap_materno_docente;
                    grid_direccion_bind(txt_matricula.Text);
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_Docente", "load_datatable_Docente();", true);

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
        //                      " and tusme_trole_clave = tuser_trole_clave and tusme_tmenu_clave = 5 and tusme_tmenu_clave = tmede_tmenu_clave " +
        //                      " and tusme_tmede_clave = tmede_clave and tmede_forma='tdodi' ";

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
        //            btn_tdodi.Visible = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //resultado.Text = ex.Message;

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
                objPermiso = servicePermiso.ObtenerPermisoFormulario(Session["usuario"].ToString(), "tdodi");
                if (objPermiso != null)
                {
                    if (objPermiso.usme_update == "0" || objPermiso.usme_select == "0")
                    {
                        btn_tdodi.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                    }
                }
                else
                {
                    btn_tdodi.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                }


            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tdodi", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);

            }
        }


        protected void combo_tipo_direccion()
        {           
            try
            {              

                ddl_tipo_direccion.DataSource = serviceCatalogo.obtenTDireActivos();
                ddl_tipo_direccion.DataValueField = "Clave";
                ddl_tipo_direccion.DataTextField = "Nombre";
                ddl_tipo_direccion.DataBind();

            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tdodi", Session["usuario"].ToString());
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
                combo_estado("139");

            }
            catch (Exception ex)
            {
                string test = ex.Message;
            }
        }

        protected void combo_estado(string c_pais)
        {
            ddl_estado.Items.Clear();
            ddl_delegacion.Items.Clear();
            ddl_delegacion.Items.Add(new ListItem("--------", "0"));
          
            try
            {
                ddl_estado.DataSource = serviceCatalogo.ObtenerEstados(c_pais);
                ddl_estado.DataValueField = "clave";
                ddl_estado.DataTextField = "descripcion";
                ddl_estado.DataBind();

            }
            catch (Exception ex)
            {
                string test = ex.Message;
            }
        }


        


        protected void Carga_Docente()
        {
            List<ModelObtenerDocentesResponse> lst = new List<ModelObtenerDocentesResponse>();

            try
            {
                lst = serviceDocente.ListaDocentes(txt_matricula.Text);
                if (lst.Count() == 1)
                {
                    txt_nombre.Text = lst[0].nombre + " " + lst[0].paterno + " " + lst[0].materno;
                    Global.cuenta = txt_matricula.Text;
                    Global.nombre = lst[0].nombre.ToString();
                    Global.ap_paterno = lst[0].paterno.ToString();
                    Global.ap_materno = lst[0].materno.ToString();
                }
                else if (lst.Count() > 1)
                {
                    GridDocente.DataSource = lst;
                    GridDocente.DataBind();
                    GridDocente.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridDocente.UseAccessibleHeader = true;
                    GridDocente.Visible = true;
                }
                else
                {
                    GridDocente.DataSource = lst;
                    GridDocente.DataBind();
                    GridDocente.Visible = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "NoexisteDocente", "NoexisteDocente();", true);
                    txt_matricula.Text = null;
                    txt_matricula.Focus();
                }

                btn_cancel.Visible = true;

            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tdodi", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        //protected void Carga_Docente()
        //{
        //    if (txt_matricula.Text != "")
        //    {
        //        string strQuery = "";
        //        strQuery = " select  tpers_nombre , tpers_paterno , tpers_materno " +
        //            "  from tpers ";
        //        strQuery = strQuery + " where tpers_id='" + txt_matricula.Text + "'";


        //        //resultado.Text = strQuery;
        //        MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
        //        conexion.Open();

        //        MySqlDataAdapter ad1 = new MySqlDataAdapter();

        //        DataSet ds = new DataSet();
        //        try
        //        {
        //            MySqlCommand comm = new MySqlCommand(strQuery, conexion);
        //            ad1.SelectCommand = comm;
        //            ad1.Fill(ds);
        //            ad1.Dispose();
        //            comm.Dispose();

        //            MySqlDataAdapter dataadapter = new MySqlDataAdapter(strQuery, conexion);
        //            dataadapter.Fill(ds, "Codigos");

        //            //GridSolicitudes.DataSource = ds;
        //            //GridSolicitudes.DataBind();
        //            //GridSolicitudes.DataMember = "Codigos";

        //            if (ds.Tables[0].Rows.Count > 0)
        //            {
        //                Global.clave_docente = txt_matricula.Text;
        //                Global.nombre_docente = ds.Tables[0].Rows[0][0].ToString();
        //                Global.ap_paterno_docente = ds.Tables[0].Rows[0][1].ToString();
        //                Global.ap_materno_docente = ds.Tables[0].Rows[0][2].ToString();
        //                txt_nombre.Text = ds.Tables[0].Rows[0][0].ToString() + " " + ds.Tables[0].Rows[0][1].ToString() +
        //                    " " + ds.Tables[0].Rows[0][2].ToString();
        //                grid_direccion_bind(txt_matricula.Text);
        //            }
        //            else
        //            {
        //                if (txt_matricula.Text != "")
        //                {
        //                    txt_matricula.Text = "";
        //                    txt_nombre.Text = "";
        //                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
        //                    ScriptManager.RegisterStartupScript(this, this.GetType(), "NoexisteAlumno", "NoexisteAlumno();", true);

        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            //resultado.Text = ex.Message;
        //        }

        //        //Estudiantes.Visible = true;
        //        txt_matricula.Focus();
        //    }
        //}

        //protected void Busca_Docente(object sender, EventArgs e)
        //{
        //    Carga_Docente();
        //}

        protected void txt_matricula_TextChanged(object sender, EventArgs e)
        {
            Carga_Docente();
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
            if (ddl_pais.SelectedValue != "0")
            {
                combo_estado(ddl_pais.SelectedValue);
            }
            else
            {
                combo_pais();
            }

            /*if (valida_direccion(txt_matricula.Text))
            {
                grid_direccion_bind(txt_matricula.Text);
            }*/
        }

        protected void ddl_estado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_estado.SelectedValue != "0")
            {
                combo_delegacion(ddl_pais.SelectedValue, ddl_estado.SelectedValue);
            }
            else
            {
                combo_estado(ddl_pais.SelectedValue);
            }
            /*if (valida_direccion(txt_matricula.Text))
            {
                grid_direccion_bind(txt_matricula.Text);
            }*/
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


        protected void ddl_delegacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            Busqueda_CP();
        }

        protected void txt_zip_TextChanged(object sender, EventArgs e)
        {

            BuscaCP();

            //if (!String.IsNullOrEmpty(txt_zip.Text))
            //{
            //    if (valida_zip(txt_zip.Text))
            //    {
            //        string QerySelect = " select distinct tcopo_testa_clave, tcopo_tdele_clave from tcopo " +
            //            " where tcopo_clave='" + txt_zip.Text + "'";

            //        MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            //        conexion.Open();
            //        try
            //        {
            //            MySqlDataAdapter sqladapter = new MySqlDataAdapter();

            //            DataSet dssql1 = new DataSet();

            //            MySqlCommand commandsql1 = new MySqlCommand(QerySelect, conexion);
            //            sqladapter.SelectCommand = commandsql1;
            //            sqladapter.Fill(dssql1);
            //            sqladapter.Dispose();
            //            commandsql1.Dispose();
            //            if (dssql1.Tables[0].Rows.Count > 0)
            //            {
            //                combo_estado(ddl_pais.SelectedValue);
            //                ddl_estado.SelectedValue = dssql1.Tables[0].Rows[0][0].ToString();
            //                combo_delegacion(ddl_pais.SelectedValue, ddl_estado.SelectedValue);
            //                ddl_delegacion.SelectedValue = dssql1.Tables[0].Rows[0][1].ToString();
            //                combo_colonia(ddl_pais.SelectedValue, ddl_estado.SelectedValue, ddl_delegacion.SelectedValue, txt_zip.Text);
            //            }
            //            else
            //            {
            //                txt_zip.Text = "";
            //                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
            //                ScriptManager.RegisterStartupScript(this, this.GetType(), "NoExiste", "NoExiste();", true);
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            string test = ex.Message;
            //        }
            //        finally
            //        {
            //            conexion.Close();
            //        }

            //    }
            //    else
            //    {
            //        //Validación ZIP
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "NoExiste", "NoExiste();", true);
            //    }
            //}
        }

        protected bool valida_zip(string zip)
        {
            string Query = "";
            Query = "SELECT COUNT(*) Indicador FROM tcopo WHERE tcopo_clave='" + zip + "'";
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
            Query = "SELECT COUNT(*) Indicador FROM aldi WHERE taldi_tpers_num = (SELECT DISTINCT tpers_num FROM tpers WHERE tpers_id='" + matricula + "')";
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

        protected void Busqueda_CP()
        {

            string strQueryCP = "";
            strQueryCP = " select tcopo_clave Clave, tcopo_desc Nombre, tcopo_testa_clave TESTA , tcopo_tdele_clave TDELE " +
                " from tcopo where 1=1";

            strQueryCP = strQueryCP + " and tcopo_testa_clave='" + ddl_estado.SelectedValue + "'";

            strQueryCP = strQueryCP + " and tcopo_tdele_clave='" + ddl_delegacion.SelectedValue + "'";
            strQueryCP = strQueryCP + " order  by Clave, Nombre ";
            try
            {
                MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                conexion.Open();
                MySqlDataAdapter dataadapter = new MySqlDataAdapter(strQueryCP, conexion);
                DataSet ds = new DataSet();
                dataadapter.Fill(ds, "Codigos");
                GridCP.DataSource = ds;
                GridCP.DataBind();
                GridCP.DataMember = "Codigos";
                //GridCP.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridCP.UseAccessibleHeader = true;
                GridCP.Visible = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_CP", "load_datatable_CP();", true);
                conexion.Close();
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
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
            ddl_colonia.SelectedItem.Text = row.Cells[2].Text;
            GridCP.Visible = false;

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
                Global.inserta_log(mensaje_error, "tdodi", Session["usuario"].ToString());
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
                Global.inserta_log(mensaje_error, "tdodi", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }


        protected void GridDocente_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridDocente.SelectedRow;
            txt_matricula.Text = row.Cells[1].Text;
            txt_nombre.Text = HttpUtility.HtmlDecode(row.Cells[2].Text) + " " + HttpUtility.HtmlDecode(row.Cells[3].Text) + " " +
             HttpUtility.HtmlDecode(row.Cells[4].Text);
            //btn_update.Visible = true;
            //GridDocente.Visible = false;
            txt_matricula.Attributes.Add("readonly", "");
            Global.clave_docente = txt_matricula.Text;
            Global.nombre_docente = HttpUtility.HtmlDecode(row.Cells[2].Text);
            Global.ap_paterno_docente = HttpUtility.HtmlDecode(row.Cells[3].Text);
            Global.ap_materno_docente = HttpUtility.HtmlDecode(row.Cells[4].Text);
            Carga_Docente();
            GridDocente.Visible= false;
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_Docente", "load_datatable_Docente();", true);

            //ScriptManager.RegisterStartupScript(this, this.GetType(), "collapse", "$('#collapseGrid').collapse('toggle')", true);

        }

        protected void grid_direccion_bind(string matricula)
        {
            try
            {
                GridDireccion.DataSource = serviceDocente.ObtenerDireccion(matricula);
                GridDireccion.DataBind();
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tdodi", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);

            }
        }

        protected void GridDireccion_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                btn_save.Visible = false;
                btn_update.Visible = true;
                btn_cancel.Visible = true;
                GridViewRow row = GridDireccion.SelectedRow;
                txt_matricula.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);
                txt_nombre.Text = HttpUtility.HtmlDecode(row.Cells[3].Text);
                combo_tipo_direccion();
                ddl_tipo_direccion.SelectedValue = row.Cells[4].Text;
                txt_direccion.Text = HttpUtility.HtmlDecode(row.Cells[7].Text);
                //combo_estatus();
                ddl_estatus.SelectedValue = row.Cells[8].Text;
                //combo_pais();
                ddl_pais.SelectedValue = row.Cells[11].Text;
                ddl_pais_SelectedIndexChanged(null, null);
                //combo_estado(ddl_pais.SelectedValue);
                ddl_estado.SelectedValue = row.Cells[12].Text;
                ddl_estado_SelectedIndexChanged(null, null);
                //combo_delegacion(ddl_pais.SelectedValue, ddl_estado.SelectedValue);
                ddl_delegacion.SelectedValue = row.Cells[13].Text;
                txt_zip.Text = row.Cells[14].Text;
                combo_colonia(ddl_pais.SelectedValue, ddl_estado.SelectedValue, ddl_delegacion.SelectedValue, txt_zip.Text);
                ListItem colonia = new ListItem();
                colonia = ddl_colonia.Items.FindByText(HttpUtility.HtmlDecode(row.Cells[15].Text));
                ddl_colonia.SelectedValue = row.Cells[15].Text; // colonia.Value;
                txt_ciudad.Text = HttpUtility.HtmlDecode(row.Cells[16].Text);
                lbl_consecutivo.Text = row.Cells[6].Text;
                ddl_tipo_direccion.Attributes.Add("disabled", "");
                // grid_direccion_bind(txt_matricula.Text);
            }
            catch (Exception ex)
            {
                string test = ex.Message;
                //Logs
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tdodi", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
            }
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
        protected void linkBttnBuscaCP_Click(object sender, EventArgs e)
        {
            if (GridCP.Visible == false)
                BuscaCP();
            else
            {
                GridCP.Visible = false;
            }

        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            //if (!String.IsNullOrEmpty(txt_ciudad.Text) && !String.IsNullOrEmpty(txt_direccion.Text) && !String.IsNullOrEmpty(txt_matricula.Text) && !String.IsNullOrEmpty(txt_zip.Text) && ddl_tipo_direccion.SelectedValue != "0" && ddl_pais.SelectedValue != "0" && ddl_estado.SelectedValue != "0" && ddl_delegacion.SelectedValue != "0" && ddl_colonia.SelectedValue != "0")
            //{

                //string Query = "INSERT INTO taldi (taldi_tpers_num,taldi_consec,taldi_tdire_clave,taldi_calle,taldi_colonia,taldi_testa_clave,taldi_tdele_clave,taldi_tpais_clave,taldi_tcopo_clave,taldi_ciudad,taldi_estatus,taldi_date,taldi_user) VALUES ( " +
                //              "( select tpers_num from tpers where tpers_id='" + txt_matricula.Text + "')," + consecutivo(lbl_id_pers.Text) + ",'" + 
                //              ddl_tipo_direccion.SelectedValue + "','" + txt_direccion.Text + "','" + ddl_colonia.SelectedItem.Text + "','" +
                //              ddl_estado.SelectedValue + "','" + ddl_delegacion.SelectedValue + "','" + ddl_pais.SelectedValue + "','" + 
                //              txt_zip.Text + "','" + txt_ciudad.Text + "','" + ddl_estatus.SelectedValue + "',CURRENT_TIMESTAMP(),'" + Session["usuario"].ToString() + "')";
               
                try
                {
                    serviceDocente.InsertarTaldi(txt_matricula.Text, ddl_tipo_direccion.SelectedValue, txt_direccion.Text, ddl_colonia.SelectedItem.Text,
                        ddl_estado.SelectedValue, ddl_delegacion.SelectedValue, ddl_pais.SelectedValue, txt_zip.Text,
                        txt_ciudad.Text, ddl_estatus.SelectedValue, Session["usuario"].ToString());
                    txt_zip.Text = null;
                    txt_ciudad.Text = null;
                    txt_direccion.Text = null;
                    combo_pais();
                    combo_estatus();
                    combo_tipo_direccion();
                    grid_direccion_bind(txt_matricula.Text);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                }
                catch (Exception ex)
                {
                    string test = ex.Message;
                }
            //}
            //else
            //{
            //    //if (!String.IsNullOrEmpty(txt_matricula.Text))
                //{
                //    grid_direccion_bind(txt_matricula.Text);
                //}
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_direccion();", true);
            //}
        }

        protected void btn_update_Click(object sender, EventArgs e)
        {
            //if (!String.IsNullOrEmpty(txt_ciudad.Text) && !String.IsNullOrEmpty(txt_direccion.Text) && !String.IsNullOrEmpty(txt_matricula.Text) && !String.IsNullOrEmpty(txt_zip.Text) && ddl_tipo_direccion.SelectedValue != "0" && ddl_pais.SelectedValue != "0" && ddl_estado.SelectedValue != "0" && ddl_delegacion.SelectedValue != "0" && ddl_colonia.SelectedValue != "0")
            //{
            //    string Query = "UPDATE taldi SET taldi_calle='" + txt_direccion.Text + "',taldi_colonia='" + ddl_colonia.SelectedItem.Text + "',taldi_testa_clave='" + ddl_estado.SelectedValue + "',taldi_tdele_clave='" + ddl_delegacion.SelectedValue + "',taldi_tpais_clave='" + ddl_pais.SelectedValue + "',taldi_tcopo_clave='" + txt_zip.Text + "',taldi_ciudad='" + txt_ciudad.Text + "',taldi_estatus='" + ddl_estatus.SelectedValue + "',taldi_date=current_timestamp(),taldi_user='" + Session["usuario"].ToString() + "' WHERE taldi_tpers_num=(select tpers_num from tpers where tpers_id='" + txt_matricula.Text + "') AND taldi_consec='" + lbl_consecutivo.Text + "' AND taldi_tdire_clave='" + ddl_tipo_direccion.SelectedValue + "'";
               
                try
                {
                    serviceDocente.EditarTaldi(txt_matricula.Text, ddl_tipo_direccion.SelectedValue, txt_direccion.Text, ddl_colonia.SelectedItem.Text,
                        ddl_estado.SelectedValue, ddl_delegacion.SelectedValue, ddl_pais.SelectedValue, txt_zip.Text,
                        txt_ciudad.Text, ddl_estatus.SelectedValue, Session["usuario"].ToString(), GridDireccion.SelectedRow.Cells[6].Text);

                    txt_zip.Text = null;
                    txt_ciudad.Text = null;
                    txt_direccion.Text = null;
                    ddl_estatus.SelectedIndex = 0;
                    ddl_pais.SelectedIndex = 0;
                    ddl_pais_SelectedIndexChanged(null, null);
                    //combo_pais();
                    //combo_estatus();
                    //combo_tipo_direccion();
                    ddl_tipo_direccion.SelectedIndex = 0;
                    grid_direccion_bind(txt_matricula.Text);
                    GridDireccion.SelectedIndex = -1;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "update_p", "update();", true);
                    btn_save.Visible = true;
                    btn_update.Visible = false;
                }
                catch (Exception ex)
                {
                    string test = ex.Message;
                }
            //}
            //else
            //{
            //    if (!String.IsNullOrEmpty(txt_matricula.Text))
            //    {
            //        grid_direccion_bind(txt_matricula.Text);
            //    }
            //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_direccion();", true);
            //}
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
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

        protected void Busca_Docente()
        {
            List<ModelObtenerDocentesResponse> lst = new List<ModelObtenerDocentesResponse>();

            try
            {
                lst = serviceDocente.ListaDocentes(txt_matricula.Text);
                if (lst.Count() == 1)
                    txt_nombre.Text = lst[0].nombre + " " + lst[0].paterno + " " + lst[0].materno;
                else if (lst.Count() > 1)
                {
                    GridDocente.DataSource = lst;
                    GridDocente.DataBind();
                    GridDocente.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridDocente.UseAccessibleHeader = true;
                    GridDocente.Visible = true;
                }
                else
                {
                    GridDocente.DataSource = lst;
                    GridDocente.DataBind();
                    GridDocente.Visible = true;
                }

                btn_cancel.Visible = true;

            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tdodi", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void consulta_docs_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Tcdir.aspx");
        }

        //protected void linkBttnBusca_Click(object sender, EventArgs e)
        //{

        //    string QueryEstudiantes = "select distinct tpers_id clave, tpers_nombre nombre, tpers_paterno paterno, tpers_materno materno, tpers_num pidm,  tpers_genero c_genero, CASE WHEN tpers_genero = 'F' THEN 'Femenino' WHEN tpers_genero = 'M' THEN 'Masculino' ELSE 'No Aplica' END genero, " +
        //                               "tpers_edo_civ c_civil, CASE WHEN tpers_edo_civ = 'C' THEN 'Casado' WHEN tpers_edo_civ = 'S' THEN 'Soltero' WHEN tpers_edo_civ = 'V' THEN 'Viudo' WHEN tpers_edo_civ = 'U' THEN 'Union Libre' WHEN tpers_edo_civ = 'D' THEN 'Divorciado' ELSE 'No Aplica' END e_civil, tpers_curp curp, date_format(tpers_fecha_nac, ' %d/%m/%Y') fecha, tpers_usuario usuario, fecha(date_format(tpers_date, '%Y-%m-%d')) fecha_reg " +
        //                                "from tpers " +
        //                                 "where tpers_tipo = 'D'";
        //    MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
        //    conexion.Open();
        //    try
        //    {
        //        MySqlDataAdapter dataadapter = new MySqlDataAdapter(QueryEstudiantes, conexion);
        //        DataSet ds = new DataSet();
        //        dataadapter.Fill(ds, "Solicitudes");
        //        GridDocente.DataSource = ds;
        //        GridDocente.DataBind();
        //        GridDocente.DataMember = "Solicitudes";
        //        GridDocente.HeaderRow.TableSection = TableRowSection.TableHeader;
        //        GridDocente.UseAccessibleHeader = true;
        //        GridDocente.Visible = true;
        //        //ScriptManager.RegisterStartupScript(this, this.GetType(), "collapse", "$('#collapseGrid').collapse('toggle')", true);

        //    }
        //    catch (Exception ex)
        //    {
        //        //logs
        //    }
        //    conexion.Close();

        //}

        protected void btn_consultar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Tcdir.aspx");
        }

        protected void linkBttnBusca_Click(object sender, EventArgs e)
        {
            Carga_Docente();
        }

    }
}