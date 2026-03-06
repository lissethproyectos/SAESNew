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
    public partial class tdoco : System.Web.UI.Page
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
                    txt_matricula.Text = Global.clave_docente;
                    txt_nombre.Text = Global.nombre_docente + " " + Global.ap_paterno_docente + " " + Global.ap_materno_docente;
                    LlenaPagina();
                    combo_estatus();
                    combo_tipo_correo();
                    grid_correo_bind(txt_matricula.Text);
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_Docentes", "load_datatable_Docentes();", true);
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
                objPermiso = servicePermiso.ObtenerPermisoFormulario(Session["usuario"].ToString(), "tdoco");
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
                Global.inserta_log(mensaje_error, "tdoco", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }


        protected void combo_tipo_correo()
        {
           
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
           
            try
            {
                GridCorreo.DataSource = serviceDocente.ObtenerCorreo(txt_matricula.Text);
                GridCorreo.DataBind();
            }
            catch (Exception ex)
            {
                string test = ex.Message;
            }
        }
        protected void txt_matricula_TextChanged(object sender, EventArgs e)
        {
            linkBttnBusca_Click(null, null);
        }

       
        protected void GridCorreo_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            GridViewRow row = GridCorreo.SelectedRow;
            txt_matricula.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);
            txt_nombre.Text = HttpUtility.HtmlDecode(row.Cells[3].Text);
            combo_tipo_correo();
            ddl_tipo_correo.SelectedValue = row.Cells[4].Text;
            lbl_consecutivo.Text = row.Cells[6].Text;
            txt_correo.Text = row.Cells[7].Text;
            if (row.Cells[8].Text == "S")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "activar_check", "activar_check();", true);
            }
            combo_estatus();
            ddl_estatus.SelectedValue = row.Cells[9].Text;
            //ddl_tipo_correo.Attributes.Add("disabled", "");
            btn_save.Visible = false;
            btn_update.Visible = true;
            btn_cancel.Visible = true;
            ddl_tipo_correo.Enabled = false;
            //grid_correo_bind(txt_matricula.Text);
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {

            btn_save.Visible = true;
            btn_update.Visible = false;
            txt_matricula.Text = null;
            txt_nombre.Text = null;
            combo_tipo_correo();
            combo_estatus();
            txt_correo.Text = null;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "desactivar_check", "desactivar_check();", true);
            ddl_tipo_correo.Enabled = true; //.Attributes.Remove("disabled");
            GridCorreo.DataSource = null;
            GridCorreo.DataBind();
            GridCorreo.SelectedIndex = -1;
            txt_matricula.ReadOnly = false;
            GridCorreo.Visible = false;
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
                    serviceDocente.InsertarTalco(txt_matricula.Text, ddl_tipo_correo.SelectedValue, txt_correo.Text,
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
                    //resultado.Text = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tdoco", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                }
            }
            else
            {
                
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_correo();", true);
            }
        }

        protected void btn_update_Click(object sender, EventArgs e)
        {
            string pattern_mail = @"/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/";
            Regex mail = new Regex(pattern_mail);
            if (!String.IsNullOrEmpty(txt_matricula.Text) && !String.IsNullOrEmpty(txt_nombre.Text) && !String.IsNullOrEmpty(txt_correo.Text) && ddl_tipo_correo.SelectedValue != "0")
            {
                string preferido = "";
                string selected = Request.Form["customSwitches"];
                if (selected == "on") { preferido = "S"; } else { preferido = "N"; }
                string Query = "UPDATE talco SET talco_correo='" + txt_correo.Text + "'," +
                    "talco_preferido='" + preferido + "',talco_estatus='" + ddl_estatus.SelectedValue + "'," +
                    "talco_date=current_timestamp(),talco_user='" + Session["usuario"].ToString() + "' " +
                    "WHERE talco_tpers_num in (select tpers_num from tpers where tpers_id='" + txt_matricula.Text + "') " +
                    "AND talco_consec='" + lbl_consecutivo.Text + "' AND talco_tmail_clave='" + ddl_tipo_correo.SelectedValue + "'";
                
                try
                {
                    serviceDocente.EditarTalco(txt_matricula.Text, lbl_consecutivo.Text, ddl_tipo_correo.SelectedValue, txt_correo.Text,
                      preferido, ddl_estatus.SelectedValue, Session["usuario"].ToString());
                    txt_correo.Text = null;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "desactivar_check", "desactivar_check();", true);
                    ddl_tipo_correo.SelectedIndex = 0;
                    ddl_estatus.SelectedIndex = 0;
                    //combo_tipo_correo();
                    //combo_estatus();
                    grid_correo_bind(txt_matricula.Text);
                    GridCorreo.SelectedIndex = -1;
                    btn_update.Visible = false;
                    btn_save.Visible = true;
                    btn_cancel.Visible=false;
                    ddl_tipo_correo.Enabled = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "update_p", "update();", true);
                }
                catch (Exception ex)
                {
                    //resultado.Text = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tdoco", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
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


        protected void GridDocentes_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridDocentes.SelectedRow;
            txt_matricula.Text = row.Cells[1].Text;
            txt_nombre.Text = HttpUtility.HtmlDecode(row.Cells[2].Text) + " " + HttpUtility.HtmlDecode(row.Cells[3].Text) + " " +
             HttpUtility.HtmlDecode(row.Cells[4].Text);
            //btn_save.Visible = false;
            //btn_update.Visible = true;
            //GridDocentes.Visible = false;
            txt_matricula.ReadOnly = true;
            btn_save.Visible = true;
            btn_cancel.Visible = true;
            //btn_update.Visible = true;
            Global.clave_docente = txt_matricula.Text;
            Global.nombre_docente = HttpUtility.HtmlDecode(row.Cells[2].Text);
            Global.ap_paterno_docente = HttpUtility.HtmlDecode(row.Cells[3].Text);
            Global.ap_materno_docente = HttpUtility.HtmlDecode(row.Cells[4].Text);
            grid_correo_bind(txt_matricula.Text);
            GridDocentes.Visible = false;
            GridCorreo.Visible = true;
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "collapse", "$('#collapseGrid').collapse('toggle')", true);
        }

        protected void consulta_docs_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Tccor.aspx");
        }

        protected void linkBttnBusca_Click(object sender, EventArgs e)
        {
            List<ModelObtenerDocentesResponse> lst = new List<ModelObtenerDocentesResponse>();
            GridCorreo.Visible = false;
            try
            {
                lst = serviceDocente.ListaDocentes(txt_matricula.Text);
                if (lst.Count() == 1)
                {
                    txt_nombre.Text = lst[0].nombre + " " + lst[0].paterno + "" + lst[0].materno;
                    txt_matricula.ReadOnly = true;
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
    }
}