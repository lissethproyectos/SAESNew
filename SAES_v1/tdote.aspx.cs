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
    public partial class tdote : System.Web.UI.Page
    {
        #region <Variables>
        Utilidades utils = new Utilidades();
        Catalogos serviceCatalogo = new Catalogos();
        DocenteService serviceDocente = new DocenteService();
        List<ModelObtenPaisesResponse> lstPaises = new List<ModelObtenPaisesResponse>();
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
                    txt_matricula.Text = Global.clave_docente;
                    txt_nombre.Text = Global.nombre_docente + " " + Global.ap_paterno_docente + " " + Global.ap_materno_docente;
                    combo_estatus();
                    combo_tipo_telefono();
                    grid_telefono_bind(txt_matricula.Text);
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_Docentes", "load_datatable_Docentes();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);

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
                objPermiso = servicePermiso.ObtenerPermisoFormulario(Session["usuario"].ToString(), "tdote");
                if (objPermiso != null)
                {
                    if (objPermiso.usme_update == "0" || objPermiso.usme_select == "0")
                    {
                        btn_talte.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                    }
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
                Global.inserta_log(mensaje_error, "tdote", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }


        protected void combo_tipo_telefono()
        {           
            try
            {   
                ddl_tipo_telefono.DataSource = serviceCatalogo.obtenTelefonos();
                ddl_tipo_telefono.DataValueField = "clave";
                ddl_tipo_telefono.DataTextField = "nombre";
                ddl_tipo_telefono.DataBind();

            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tdote", Session["usuario"].ToString());
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
                GridTelefono.DataSource = serviceDocente.ObtenerTelefono(matricula);
                GridTelefono.DataBind();
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tdote", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            btn_save.Visible = true;
            btn_update.Visible = false;
            txt_matricula.Text = null;
            txt_nombre.Text = null;
            combo_tipo_telefono();
            combo_estatus();
            txt_lada.Text = null;
            txt_telefono.Text = null;
            txt_extension.Text = null;
            ddl_tipo_telefono.Enabled = true;
            ////ddl_tipo_telefono.Attributes.Remove("disabled");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
            //GridTelefono.Visible = false;
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            ModelInsertarTelefonoDocenteResponse objExiste = new ModelInsertarTelefonoDocenteResponse();
            if (!String.IsNullOrEmpty(txt_matricula.Text) && !String.IsNullOrEmpty(txt_nombre.Text) && !String.IsNullOrEmpty(txt_lada.Text) && !String.IsNullOrEmpty(txt_telefono.Text) && ddl_tipo_telefono.SelectedValue != "0")
            {
                try
                {
                    objExiste=serviceDocente.InsertarTelefono(txt_matricula.Text, lbl_consecutivo.Text, ddl_tipo_telefono.SelectedValue, txt_lada.Text, txt_telefono.Text, txt_extension.Text, ddl_estatus.SelectedValue, Session["usuario"].ToString());
                    if (objExiste != null)
                    {
                        if (objExiste.Existe == "0")
                        {
                            txt_lada.Text = null;
                            txt_telefono.Text = null;
                            txt_extension.Text = null;
                            combo_tipo_telefono();
                            combo_estatus();
                            grid_telefono_bind(txt_matricula.Text);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                        }
                        else
                        {
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validarClave('ContentPlaceHolder1_txt_tnive',1);", true);
                        }
                    }
                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);

                }
                catch (Exception ex)
                {
                    //resultado.Text = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tdote", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
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
                    serviceDocente.EditarTelefono(txt_matricula.Text, lbl_consecutivo.Text, ddl_tipo_telefono.SelectedValue, txt_lada.Text , txt_telefono.Text, txt_extension.Text, ddl_estatus.SelectedValue, Session["usuario"].ToString());
                    txt_lada.Text = null;
                    txt_telefono.Text = null;
                    txt_extension.Text = null;
                    combo_tipo_telefono();
                    combo_estatus();
                    grid_telefono_bind(txt_matricula.Text);
                    GridTelefono.SelectedIndex = -1;
                    ddl_tipo_telefono.Enabled = true;
                    btn_save.Visible = true;
                    btn_update.Visible = false;
                    btn_cancel.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "update_p", "update();", true);
                }
                catch (Exception ex)
                {
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tdote", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
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


      

       
        protected void GridTelefono_SelectedIndexChanged(object sender, EventArgs e)
        {
            btn_save.Visible = false;
            btn_update.Visible = true;
            btn_cancel.Visible = true;
            GridViewRow row = GridTelefono.SelectedRow;
            txt_matricula.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);
            txt_nombre.Text = HttpUtility.HtmlDecode(row.Cells[3].Text);
            combo_tipo_telefono();
            ddl_tipo_telefono.SelectedValue = row.Cells[4].Text;
            ddl_tipo_telefono.Enabled = false;
            lbl_consecutivo.Text = row.Cells[6].Text;
            txt_lada.Text = row.Cells[7].Text;
            txt_telefono.Text = row.Cells[8].Text;
            txt_extension.Text = HttpUtility.HtmlDecode(row.Cells[9].Text.Trim());
            combo_estatus();
            ddl_estatus.SelectedValue = row.Cells[10].Text;
            //grid_telefono_bind(txt_matricula.Text);
        }


        protected void GridDocentes_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridDocentes.SelectedRow;
            txt_matricula.Text = row.Cells[1].Text;
            txt_nombre.Text = HttpUtility.HtmlDecode(row.Cells[2].Text) + " " + HttpUtility.HtmlDecode(row.Cells[3].Text) + " " +
             HttpUtility.HtmlDecode(row.Cells[4].Text);
            btn_save.Visible = true;
            btn_update.Visible = false;
            //GridDocentes.Visible = false;
            txt_matricula.Attributes.Add("readonly", "");
           
            Global.clave_docente = txt_matricula.Text;
            Global.nombre_docente = HttpUtility.HtmlDecode(row.Cells[2].Text);
            Global.ap_paterno_docente = HttpUtility.HtmlDecode(row.Cells[3].Text);
            Global.ap_materno_docente = HttpUtility.HtmlDecode(row.Cells[4].Text);
            id_num = row.Cells[5].Text;
            GridDocentes.Visible = false;
            grid_telefono_bind(txt_matricula.Text);
            //GridTelefono.SelectedIndex = -1;

        }

        protected void consulta_docs_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Tctel.aspx");
        }

        protected void linkBttnBusca_Click(object sender, EventArgs e)
        {
            List<ModelObtenerDocentesResponse> lst = new List<ModelObtenerDocentesResponse>();
            
            try
            {
                lst = serviceDocente.ListaDocentes(txt_matricula.Text);
                if (lst.Count() == 1)
                    txt_nombre.Text = lst[0].nombre + " " + lst[0].paterno + " " + lst[0].materno;
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

                btn_cancel.Visible = true;

            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tdote", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }




        protected void bttnConsultar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Tctel.aspx");
        }

        protected void ddl_tipo_telefono_SelectedIndexChanged(object sender, EventArgs e)
        {
            grid_telefono_bind(txt_matricula.Text);
        }
    }
}