using iTextSharp.text;
using MySql.Data.MySqlClient;
using SAES_DBO.Models;
using SAES_Services;
using SAES_v1.Utils;
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
using static SAES_DBO.Models.ModelDocente;
using static SAES_DBO.Models.ModelMenu;

namespace SAES_v1
{
    public partial class tdoce : System.Web.UI.Page
    {
        #region <Variables>
        Utilidades utils = new Utilidades();
        Catalogos serviceCatalogo = new Catalogos();
        DocenteService serviceDocente = new DocenteService();
        List<ModelObtenPaisesResponse> lstPaises = new List<ModelObtenPaisesResponse>();
        public string id_num = null;
        MenuService servicePermiso = new MenuService();
        AlumnoService serviceAlumno = new AlumnoService();
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
                    txt_matricula.Text = Global.clave_docente;
                    txt_nombre.Text = Global.nombre_docente + " " + Global.ap_paterno_docente + " " + Global.ap_materno_docente;
                    LlenaPagina();
                    combo_categoria();
                    if (txt_matricula.Text != "")
                    {
                        //Carga_Carreras();
                        categorias_docente();
                        grid_idiomas_bind();
                        grid_carreras_bind();
                    }
                    //grid_correo_bind(txt_matricula.Text);
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_Docentes", "load_datatable_Docentes();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_Carreras", "load_datatable_Carreras();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_Idiomas", "load_datatable_Idiomas();", true);

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
                objPermiso = servicePermiso.ObtenerPermisoFormulario(Session["usuario"].ToString(), "tdoce");
                if (objPermiso != null)
                {
                    if (objPermiso.usme_update == "0" || objPermiso.usme_select == "0")
                    {
                        linkBttnAddCarreras.Visible = false;
                        linkBttnAddIdiomas.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                    }
                }
                else
                {
                    combo_categoria();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                }


            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tdoce", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }


        protected void combo_categoria()
        {
            try
            {
                ddl_categoria.DataSource = serviceDocente.ObtenerCategorias();
                ddl_categoria.DataValueField = "clave";
                ddl_categoria.DataTextField = "descripcion";
                ddl_categoria.DataBind();
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tdoce", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }



        protected void txt_matricula_TextChanged(object sender, EventArgs e)
        {
            linkBttnBusca_Click(null, null);
        }

        protected void GridDocentes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = GridDocentes.SelectedRow;
                txt_matricula.Text = row.Cells[1].Text;
                txt_nombre.Text = HttpUtility.HtmlDecode(row.Cells[2].Text) + " " + HttpUtility.HtmlDecode(row.Cells[3].Text) + " " +
                 HttpUtility.HtmlDecode(row.Cells[4].Text);
                GridDocentes.Visible = false;
                txt_matricula.ReadOnly = true;
                Global.clave_docente = txt_matricula.Text;
                Global.nombre_docente = HttpUtility.HtmlDecode(row.Cells[2].Text);
                Global.ap_paterno_docente = HttpUtility.HtmlDecode(row.Cells[3].Text);
                Global.ap_materno_docente = HttpUtility.HtmlDecode(row.Cells[4].Text);


                categorias_docente();
                grid_idiomas_bind();
                grid_carreras_bind();
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tdoce", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }

        }
        protected void categorias_docente()
        {
            List<ModelObtenerCategoriasDocenteResponse> lst = new List<ModelObtenerCategoriasDocenteResponse>();

            try
            {

                lst = serviceDocente.ObtenerCategoriasDocente(txt_matricula.Text);
                if (lst.Count > 0)
                {
                    ddl_categoria.SelectedValue = lst[0].categoria;
                    ddl_estatus.SelectedValue = lst[0].estatus;
                }

            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tdoce", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void grid_carreras_bind()
        {
            try
            {
                GridCarreras.DataSource = serviceDocente.ObtenerCarreras(txt_matricula.Text);
                GridCarreras.DataBind();
                GridCarreras.UseAccessibleHeader = true;
                //GridCarreras.Visible = true;
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tdoce", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void grid_idiomas_bind()
        {
            try
            {
                GridIdiomas.DataSource = serviceDocente.ObtenerIdiomas(txt_matricula.Text);
                GridIdiomas.DataBind();
                GridIdiomas.UseAccessibleHeader = true;
                //GridIdiomas.Visible = true;
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tdoce", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                serviceDocente.EditarTdoce(txt_matricula.Text, ddl_categoria.SelectedValue.ToString(), ddl_estatus.SelectedValue, Session["usuario"].ToString());

                ddl_categoria.Enabled = false;
                ddl_estatus.Enabled = false;

                btn_cancel_estatus.Visible = false;
                btn_save.Visible = false;
                btn_update.Visible = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);


            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tdoce", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_update", "error_update();", true);
            }
        }



        protected void GridCatalogo_Carreras(object sender, EventArgs e)
        {
            GridViewRow row = GridCarreras.SelectedRow;
            txt_carrera.Text = HttpUtility.HtmlDecode(row.Cells[1].Text);
            ddl_estatus_carrera.SelectedValue = row.Cells[3].Text;
            txt_cedula.Text = HttpUtility.HtmlDecode(row.Cells[4].Text);
            Global.clave_docente = txt_matricula.Text;
            Global.carrera_docente = row.Cells[1].Text.ToString();

        }

        protected void GridCatalogo_Idiomas(object sender, EventArgs e)
        {
            GridViewRow row = GridIdiomas.SelectedRow;
            txt_idioma.Text = HttpUtility.HtmlDecode(row.Cells[1].Text);
            txt_porcentaje.Text = row.Cells[2].Text;
            Global.docente = txt_matricula.Text;
            Global.idioma_docente = txt_idioma.Text;
            txt_matricula.Focus();
        }

        protected void txt_porcentaje_TextChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_valor_entero();", true);
        }


        protected void linkBttnBusca_Click(object sender, EventArgs e)
        {
            List<ModelObtenerDocentesResponse> lst = new List<ModelObtenerDocentesResponse>();
            try
            {
                lst = serviceDocente.ListaDocentes(txt_matricula.Text);
                if (lst.Count() == 1)
                {
                    GridDocentes.Visible = false;
                    txt_nombre.Text = lst[0].nombre + " " + lst[0].paterno + " " + lst[0].materno;
                    txt_matricula.ReadOnly = true;
                    categorias_docente();
                    grid_idiomas_bind();
                    grid_carreras_bind();
                }
                else if (lst.Count() > 1)
                {
                    GridDocentes.DataSource = lst;
                    GridDocentes.DataBind();
                    GridDocentes.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridDocentes.UseAccessibleHeader = true;
                    GridDocentes.Visible = true;
                }
                else
                {
                    GridDocentes.DataSource = lst;
                    GridDocentes.DataBind();
                    GridDocentes.Visible = true;
                }


            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tdoce", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void linkBttnAddIdiomas_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid == true)
                {
                    serviceDocente.InsertarTdoid(txt_matricula.Text, txt_idioma.Text, txt_porcentaje.Text, Session["usuario"].ToString());
                    Global.clave_docente = txt_matricula.Text;
                    txt_idioma.Text = "";
                    txt_porcentaje.Text = "";
                    //Carga_Carreras();
                    grid_idiomas_bind();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                }
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tdoce", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void linkBttnAddCarreras_Click(object sender, EventArgs e)
        {
            if (Page.IsValid == true)
            {
                ModelInsertarTdocaResponse objExiste = new ModelInsertarTdocaResponse();
                try
                {
                    objExiste = serviceDocente.InsertarTdoca(txt_matricula.Text, txt_carrera.Text, ddl_estatus_carrera.SelectedValue, txt_cedula.Text, Session["usuario"].ToString());
                    if (objExiste.Existe == "0")
                    {
                        Global.clave_docente = txt_matricula.Text;
                        txt_carrera.Text = "";
                        txt_cedula.Text = "";
                        ddl_estatus_carrera.SelectedIndex = 0;
                        grid_carreras_bind();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);

                    }
                    else
                        validaCarrera.IsValid = false;
                }
                catch (Exception ex)
                {
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tdoce", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                }
            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            try
            {
                txt_matricula.Text = string.Empty;
                txt_nombre.Text = string.Empty;
                txt_matricula.ReadOnly = false;
                ddl_categoria.SelectedIndex = 0;
                ddl_estatus.SelectedIndex = 0;
                GridCarreras.DataSource = null;
                GridCarreras.DataBind();
                GridIdiomas.DataSource = null;
                GridIdiomas.DataBind();
                //grid
            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tadmi", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void btn_update_Click(object sender, EventArgs e)
        {
            btn_cancel_estatus.Visible = true;
            btn_save.Visible = true;
            btn_update.Visible = false;
            ddl_categoria.Enabled = true;
            ddl_estatus.Enabled = true;
        }       

        protected void btn_cancel_estatus_Click(object sender, EventArgs e)
        {

        }
    }
}