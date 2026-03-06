using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
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
using static iTextSharp.text.pdf.PdfStructTreeController;
using static SAES_DBO.Models.ModelMenu;

namespace SAES_v1
{
    public partial class tpais : System.Web.UI.Page
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
                if (!IsPostBack)
                {
                    LlenaPagina();
                    combo_estatus();
                }

            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "fade_datatable", "fade_datatable();", true);

        }

        private void LlenaPagina()
        {
            ModelObtenerPermisoFormResponse objPermiso = new ModelObtenerPermisoFormResponse();
            objPermiso.usme_update = "0";
            objPermiso.usme_select = "0";
            System.Threading.Thread.Sleep(50);
            try
            {
                objPermiso = servicePermiso.ObtenerPermisoFormulario(Session["usuario"].ToString(), "tpais");
                if (objPermiso != null)
                {
                    if (objPermiso.usme_update == "0" || objPermiso.usme_select == "0")
                    {
                        btn_pais.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                    }
                    else
                        grid_bind_pais();
                }
                else
                {
                    btn_pais.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                }


            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tpais", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);

            }
        }

        protected void combo_estatus()
        {
            estatus_pais.Items.Clear();
            estatus_pais.Items.Add(new ListItem("Activo", "A"));
            estatus_pais.Items.Add(new ListItem("Inactivo", "B"));
        }

        protected void grid_bind_pais()
        {

            try
            {
                GridPaises.DataSource = lstPaises;
                GridPaises.DataBind();
                lstPaises = serviceCatalogo.ObtenerPaises();
                GridPaises.DataSource = lstPaises;
                GridPaises.DataBind();
                Session["Paises"] = lstPaises;

                GridPaises.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridPaises.UseAccessibleHeader = true;
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tpais", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                //Response.Redirect("Inicio.aspx");
            }
        }

        protected void GridPaises_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridPaises.SelectedRow;

            c_pais.Text = row.Cells[1].Text;
            n_pais.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);
            g_pais.Text = HttpUtility.HtmlDecode(row.Cells[3].Text);
            combo_estatus();
            estatus_pais.SelectedValue = row.Cells[4].Text;
            save_pais.Visible = false;
            update_pais.Visible = true;
            c_pais.ReadOnly = true;
            cancel_pais.Visible = true;
        }

        protected void cancel_pais_Click(object sender, EventArgs e)
        {
            cancel_pais.Visible = false;
            c_pais.Text = null;
            n_pais.Text = null;
            g_pais.Text = null;
            combo_estatus();
            c_pais.ReadOnly = false;

            save_pais.Visible = true;
            update_pais.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
            grid_bind_pais();
        }

        protected bool valida_clave(string clave)
        {

            try
            {
                //DataTable dt = GetData(cmd);
                if (Session["Paises"] != null)
                    lstPaises = (List<ModelObtenPaisesResponse>)Session["Paises"];

                var existe = from p in lstPaises
                             where p.clave == clave
                             select p;

                if (existe.Count() > 0)
                    return false;
                else
                    return true;
                //if (dt.Rows[0]["Indicador"].ToString() != "0")
                //{
                //    return false;
                //}
                //else
                //{
                //    return true;
                //}
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tpais", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                return false;
            }

        }

        protected void save_pais_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(c_pais.Text) && !String.IsNullOrEmpty(n_pais.Text))
            {
                if (valida_clave(c_pais.Text))
                {

                    try
                    {

                        serviceCatalogo.InsertarPais(c_pais.Text, n_pais.Text, g_pais.Text, Session["usuario"].ToString(), estatus_pais.SelectedValue);

                        c_pais.Text = null;
                        n_pais.Text = null;
                        combo_estatus();
                        grid_bind_pais();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                    }
                    catch (Exception ex)
                    {
                        string test = ex.Message;
                        //Logs
                        string mensaje_error = ex.Message.Replace("'", "-");
                        Global.inserta_log(mensaje_error, "tpais", Session["usuario"].ToString());
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validarclavePais('ContentPlaceHolder1_c_pais',1);", true);
                }
            }
            else
            {
                //Validación de campos obligatorios JS
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_pais();", true);
            }
        }

        protected void update_pais_Click(object sender, EventArgs e)
        {

            if (!String.IsNullOrEmpty(c_pais.Text) && !String.IsNullOrEmpty(n_pais.Text))
            {
                try
                {
                    serviceCatalogo.EditarPais(c_pais.Text, n_pais.Text, g_pais.Text, Session["usuario"].ToString(), estatus_pais.SelectedValue);
                    save_pais.Visible = true;
                    cancel_pais.Visible = false;
                    update_pais.Visible = false;
                    c_pais.ReadOnly = false;
                    c_pais.Text = string.Empty;
                    n_pais.Text = string.Empty;
                    g_pais.Text = string.Empty;                    
                    estatus_pais.SelectedIndex = 0;
                    grid_bind_pais();
                    GridPaises.SelectedIndex = -1;

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "update_p", "update();", true);
                }
                catch (Exception ex)
                {
                    string test = ex.Message;
                    //Logs
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tpais", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_pais();", true);
            }
        }

        protected void estatus_pais_SelectedIndexChanged(object sender, EventArgs e)
        {
            grid_bind_pais();
        }
    }
}