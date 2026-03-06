using SAES_DBO.Models;
using SAES_Services;
using SAES_v1.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using static SAES_DBO.Models.ModelMenu;

namespace SAES_v1
{
    public partial class ttini : System.Web.UI.Page
    {
        #region <Variables>
        Utilidades utils = new Utilidades();
        Catalogos serviceCatalogo = new Catalogos();
        List<ModelObtenPaisesResponse> lstPaises = new List<ModelObtenPaisesResponse>();
        MenuService servicePermiso = new MenuService();
        CatOpcionesTitulacion serviceTitulacion=new CatOpcionesTitulacion();
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
                try
                {
                    if (!IsPostBack)
                    {
                        LlenaPagina();
                       
                        //combo_estatus();
                        //combo_funcionarios();
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);
                }
                catch (Exception ex)
                {
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tinni", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                }
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
                objPermiso = servicePermiso.ObtenerPermisoFormulario(Session["usuario"].ToString(), "ttini");
                if (objPermiso != null)
                {
                    if (objPermiso.usme_update == "0" || objPermiso.usme_select == "0")
                    {
                        btn_ttini.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                    }
                    else
                    {
                        btn_ttini.Visible = true;
                        combo_opciones_titulacion();
                        combo_nivel();
                        combo_codigos();
                        Gridtinni.DataSource = null;
                        Gridtinni.DataBind();
                    }
                }
                else
                {
                    btn_ttini.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                }


            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "ttini", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        private void grid_tinni_bind()
        {
            try
            {
                Gridtinni.DataSource = serviceTitulacion.obtenGridTitulacion(ddl_opcTit.SelectedValue);
                Gridtinni.DataBind();
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tfuca", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void combo_opciones_titulacion()
        {
            ddl_opcTit.DataSource = serviceCatalogo.obtenOpcTitulacion();
            ddl_opcTit.DataValueField = "Clave";
            ddl_opcTit.DataTextField = "Descripcion";
            ddl_opcTit.DataBind();
        }

        protected void combo_nivel()
        {
            ddl_nivel.DataSource = serviceCatalogo.obtenNivelesActivos();
            ddl_nivel.DataValueField = "clave";
            ddl_nivel.DataTextField = "nombre";
            ddl_nivel.DataBind();
        }

        protected void combo_codigos()
        {
            ddl_codigo.DataSource = serviceCatalogo.ObtenerTcoco("TCOCO", "TI");
            ddl_codigo.DataValueField = "Clave";
            ddl_codigo.DataTextField = "Descripcion";
            ddl_codigo.DataBind();
        }

        protected void Gridtinni_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = Gridtinni.SelectedRow;
            string preferido = "";
            ddl_opcTit.Enabled = false;
            ddl_nivel.Enabled = false;
            try
            {
                ddl_opcTit.SelectedValue = row.Cells[7].Text;
            }
            catch
            {
                ddl_opcTit.SelectedIndex = 0;
            }
            try
            {
                ddl_nivel.SelectedValue = row.Cells[8].Text;

            }
            catch
            {
                ddl_nivel.SelectedIndex = 0;
            }
            try
            {
                ddl_codigo.SelectedValue = row.Cells[9].Text;
            }
            catch
            {
                ddl_codigo.SelectedIndex = 0;
            }

            //string selected = Request.Form["customSwitches"];
            //if (selected == "on") { preferido = "S"; } else { preferido = "N"; }

            if(row.Cells[2].Text=="S")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "activar_check", "activar_check();", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "desactivar_check", "desactivar_check();", true);

            if (row.Cells[10].Text == "S")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "activar_check_ss", "activar_check_ss();", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "desactivar_check_ss", "desactivar_check_ss();", true);


            //txt_creditos.Text= row.Cells[2].Text;

            txt_promedio.Text = row.Cells[4].Text;            
            btn_update.Visible = true;
            btn_save.Visible = false;
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            //ddl_opcTit.SelectedIndex = 0;
            ddl_nivel.SelectedIndex = 0;
            //txt_creditos.Text = null;
            txt_promedio.Text = null;
            ddl_codigo.SelectedIndex = 0;
            Gridtinni.SelectedIndex = -1;
            btn_save.Visible = true;
            btn_update.Visible = false;
            ddl_opcTit.Enabled = true;
            ddl_nivel.Enabled = true;
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            ModelInsTtiniResponse objExiste = new ModelInsTtiniResponse();
            if (Page.IsValid == true)
            {
                try
                {
                    string preferido = "";
                    string preferido_ss = "";


                    string selected = Request.Form["customSwitches"];
                    if (selected == "on") { preferido = "S"; } else { preferido = "N"; }

                    string selected_ss = Request.Form["customSwitchesSS"];
                    if (selected_ss == "on") { preferido_ss = "S"; } else { preferido_ss = "N"; }

                    objExiste = serviceCatalogo.Ins_ttini(ddl_opcTit.SelectedValue, ddl_nivel.SelectedValue,
                        preferido, txt_promedio.Text, ddl_codigo.SelectedValue, Session["usuario"].ToString(), preferido_ss);
                    if (objExiste != null)
                    {
                        if (objExiste.Existe == "0")
                        {
                            grid_tinni_bind();
                            ddl_opcTit.SelectedIndex = 0;
                            ddl_nivel.SelectedIndex = 0;
                            //txt_creditos.Text = null;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "desactivar_check", "desactivar_check();", true);
                            txt_promedio.Text = null;
                            ddl_codigo.SelectedIndex = 0;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validarClave('ContentPlaceHolder1_ddl_opcTit',1);", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                    }
                }
                catch (Exception ex)
                {
                    string test = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "ttini", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                }
            }
            else
            {
                this.Page.Validate();
            }
        }

        protected void btn_update_Click(object sender, EventArgs e)
        {
            try
            {
                string preferido = null;
                string preferido_ss = null;


                string selected = Request.Form["customSwitches"];
                if (selected == "on") { preferido = "S"; } else { preferido = "N"; }

                string selected_ss = Request.Form["customSwitchesSS"];
                if (selected_ss == "on") { preferido_ss = "S"; } else { preferido_ss = "N"; }

                serviceCatalogo.Upd_ttini(ddl_opcTit.SelectedValue, ddl_nivel.SelectedValue,
                    preferido, txt_promedio.Text, ddl_codigo.SelectedValue, Session["usuario"].ToString(), preferido_ss);
                grid_tinni_bind();
                ddl_opcTit.SelectedIndex = 0;
                ddl_nivel.SelectedIndex = 0;
                //txt_creditos.Text = null;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "desactivar_check", "desactivar_check();", true);
                txt_promedio.Text = null;
                ddl_codigo.SelectedIndex = 0;
                Gridtinni.SelectedIndex = -1;
                btn_save.Visible = true;
                btn_update.Visible = false;
                ddl_opcTit.Enabled = true;
                ddl_nivel.Enabled = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "ttini", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
            }
        }

        protected void ddl_opcTit_SelectedIndexChanged(object sender, EventArgs e)
        {
            grid_tinni_bind();
        }
    }
}