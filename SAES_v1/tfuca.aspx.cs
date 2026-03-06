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

namespace SAES_v1
{
    public partial class tfuca : System.Web.UI.Page
    {
        #region <Variables>
        Utilidades utils = new Utilidades();
        Catalogos serviceCatalogo = new Catalogos();
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
                try
                {
                    if (!IsPostBack)
                    {
                        //LlenaPagina();
                        combo_campus();
                        combo_estatus();
                        combo_funcionarios();
                        grid_tfuca_bind();
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);
                }
                catch (Exception ex)
                {
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tfuca", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                }
            }
        }

        private void grid_tfuca_bind()
        {
            try
            {
                Gridtfuca.DataSource = serviceCatalogo.obtenFuncionariosporCampus(ddl_campus.SelectedValue);
                Gridtfuca.DataBind();
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tfuca", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }
        protected void combo_estatus()
        {
            ddl_estatus.Items.Clear();
            ddl_estatus.Items.Add(new ListItem("Activo", "A"));
            ddl_estatus.Items.Add(new ListItem("Inactivo", "B"));
        }
        protected void combo_campus()
        {
            ddl_campus.DataSource = serviceCatalogo.obtenCampus();
            ddl_campus.DataValueField = "Clave";
            ddl_campus.DataTextField = "Descripcion";
            ddl_campus.DataBind();
        }


        protected void combo_funcionarios()
        {
            ddl_funcionarios.DataSource = serviceCatalogo.obtenFuncionariosActivos();
            ddl_funcionarios.DataValueField = "clave";
            ddl_funcionarios.DataTextField = "nombre";
            ddl_funcionarios.DataBind();
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            ddl_campus.Enabled = true;
            ddl_campus.SelectedIndex = 0;
            ddl_funcionarios.SelectedIndex = 0;
            txt_nombre.Text = null;
            txt_paterno.Text = null;
            txt_materno.Text = null;
            txt_curp.Text = null;
            ddl_estatus.SelectedIndex = 0;
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            if (Page.IsValid == true)
            {
                ModelInstfucaResponse objExiste = new ModelInstfucaResponse();
                try
                {
                    objExiste = serviceCatalogo.Ins_tfuca(ddl_campus.SelectedValue, ddl_funcionarios.SelectedValue,
                        "", txt_nombre.Text, txt_paterno.Text, txt_materno.Text, txt_curp.Text, Session["usuario"].ToString(),
                        ddl_estatus.SelectedValue);
                    if (objExiste != null)
                    {
                        if (objExiste.Existe == "0")
                        {
                            ddl_campus.SelectedIndex = 0;
                            ddl_funcionarios.SelectedIndex = 0;
                            txt_nombre.Text = null;
                            txt_paterno.Text = null;
                            txt_materno.Text = null;
                            txt_curp.Text = null;
                            ddl_estatus.SelectedIndex = 0;
                            grid_tfuca_bind();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validarClave('ContentPlaceHolder1_ddl_funcionarios',1);", true);
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
                    Global.inserta_log(mensaje_error, "tfuca", Session["usuario"].ToString());
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
            if (Page.IsValid == true)
            {
                try
                {
                    serviceCatalogo.Upd_tfuca(Gridtfuca.SelectedRow.Cells[10].Text, Gridtfuca.SelectedRow.Cells[1].Text, // ddl_funcionarios.SelectedValue,
                        "", txt_nombre.Text, txt_paterno.Text, txt_materno.Text, txt_curp.Text, Session["usuario"].ToString(),
                        ddl_estatus.SelectedValue);
                    ddl_campus.Enabled = true;
                    ddl_campus.SelectedIndex = 0;
                    ddl_funcionarios.Enabled = true;
                    ddl_funcionarios.SelectedIndex = 0;
                    txt_nombre.Text = null;
                    txt_paterno.Text = null;
                    txt_materno.Text = null;
                    txt_curp.Text = null;
                    ddl_estatus.SelectedIndex = 0;
                    grid_tfuca_bind();
                    Gridtfuca.SelectedIndex = -1;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                }
                catch (Exception ex)
                {
                    string test = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tfuca", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                }
            }
            else
            {
                this.Page.Validate();
            }
        }

        protected void Gridtfuca_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = Gridtfuca.SelectedRow;
            try
            {
                ddl_campus.SelectedValue = row.Cells[10].Text;
            }
            catch
            {
                ddl_campus.SelectedIndex = 0;
            }
            try
            {
                ddl_funcionarios.SelectedValue = row.Cells[1].Text;

            }
            catch
            {
                ddl_funcionarios.SelectedIndex = 0;
            }
            ddl_campus.Enabled = false;
            txt_nombre.Text = HttpUtility.HtmlDecode(row.Cells[3].Text);
            txt_paterno.Text = HttpUtility.HtmlDecode(row.Cells[4].Text);
            txt_materno.Text = HttpUtility.HtmlDecode(row.Cells[5].Text);
            txt_curp.Text = HttpUtility.HtmlDecode(row.Cells[6].Text);
            //ddl_estatus.SelectedValue = row.Cells[9].Text;
            btn_update.Visible = true;
            btn_save.Visible = false;
        }

        protected void ddl_campus_SelectedIndexChanged(object sender, EventArgs e)
        {
            grid_tfuca_bind();
        }
    }
}