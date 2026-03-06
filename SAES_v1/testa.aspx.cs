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
using static SAES_DBO.Models.ModelCobranza;
using static SAES_DBO.Models.ModelMenu;

namespace SAES_v1
{
    public partial class testa : System.Web.UI.Page
    {
        #region <Variables>
        Utilidades utils = new Utilidades();
        Catalogos serviceCatalogo = new Catalogos();
        List<ModeltpaisResponse> lstPaises = new List<ModeltpaisResponse>();
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

                c_estado.Attributes.Add("onblur", "validarclaveEstado('ContentPlaceHolder1_c_estado',0)");
                c_estado.Attributes.Add("oninput", "validarclaveEstado('ContentPlaceHolder1_c_estado',0)");
                n_estado.Attributes.Add("onblur", "validarNombreEstado('ContentPlaceHolder1_n_estado')");
                n_estado.Attributes.Add("oninput", "validarNombreEstado('ContentPlaceHolder1_n_estado')");
                if (!IsPostBack)
                {
                    combo_estatus();
                    combo_pais();
                    LlenaPagina();
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);

        }

        protected void combo_estatus()
        {
            estatus_estado.Items.Clear();
            estatus_estado.Items.Add(new ListItem("Activo", "A"));
            estatus_estado.Items.Add(new ListItem("Inactivo", "B"));
        }
        protected void combo_pais()
        {

            try
            {

                lstPaises = serviceCatalogo.QRY_TPAIS();
                cbo_pais.DataSource = lstPaises.OrderBy(p => p.clave);
                cbo_pais.DataValueField = "Clave";
                cbo_pais.DataTextField = "Nombre";
                cbo_pais.DataBind();
                cbo_pais.SelectedValue = "139";

            }
            catch (Exception ex)
            {
                string test = ex.Message;
            }

        }

        private void LlenaPagina()
        {
            ModelObtenerPermisoFormResponse objPermiso = new ModelObtenerPermisoFormResponse();
            objPermiso.usme_update = "0";
            objPermiso.usme_select = "0";
            //update_pais.Visible = false;

            System.Threading.Thread.Sleep(50);
            try
            {
                objPermiso = servicePermiso.ObtenerPermisoFormulario(Session["usuario"].ToString(), "testa");
                if (objPermiso != null)
                {
                    if (objPermiso.usme_update == "0" || objPermiso.usme_select == "0")
                    {
                        btn_estado.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                    }
                    else
                        grid_bind_estados();

                }
                else
                {
                    btn_estado.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                }


            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tpees", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);

            }
        }

        protected void grid_bind_estados()
        {
            try
            {
                GridEstados.DataSource = serviceCatalogo.QRY_TESTA();
                GridEstados.DataBind();
                //DataTable dt = serviceCatalogo.QRY_TESTA();
                //GridEstados = utils.BeginGrid(GridEstados, dt);
            }
            catch (Exception ex)
            {
                //Logs
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "testa", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }
        protected void GridEstados_SelectedIndexChanged(object sender, EventArgs e)
        {

            GridViewRow row = GridEstados.SelectedRow;

            c_estado.Text = row.Cells[1].Text;
            n_estado.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);
            combo_estatus();
            combo_pais();
            cbo_pais.SelectedValue = row.Cells[3].Text;
            estatus_estado.SelectedValue = row.Cells[5].Text;
            save_estado.Visible = false;
            update_estado.Visible = true;
            cancel_estado.Visible = true;
            c_estado.ReadOnly = true;
            //grid_bind_estados();

        }
        //protected bool valida_clave_edo(string clave)
        //{
        //    string Query = "";
        //    Query = "SELECT DISTINCT COUNT(*) Indicador " +
        //            "FROM TESTA " +
        //            "INNER JOIN TPAIS ON tpais_clave = testa_tpais_clave " +
        //            "WHERE testa_tpais_clave = '" + cbo_pais.SelectedValue + "' " +
        //            "AND testa_clave = '" + clave + "' ";

        //    MySqlCommand cmd = new MySqlCommand(Query);
        //    DataTable dt = GetData(cmd);
        //    if (dt.Rows[0]["Indicador"].ToString() != "0")
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}
        protected void save_estado_Click(object sender, EventArgs e)
        {
            ModelInstestaResponse objExisteRegistro = new ModelInstestaResponse();

            if (cbo_pais.SelectedValue != "0" && !String.IsNullOrEmpty(c_estado.Text) && !String.IsNullOrEmpty(n_estado.Text))
            {                            
                try
                {
                    objExisteRegistro=serviceCatalogo.Ins_testa(c_estado.Text, cbo_pais.SelectedValue, n_estado.Text, Session["usuario"].ToString(), estatus_estado.SelectedValue);
                    if (objExisteRegistro != null)
                    {
                        if (objExisteRegistro.Existe == "0")
                        {
                            c_estado.Text = null;
                            n_estado.Text = null;
                            combo_estatus();
                            combo_pais();
                            grid_bind_estados();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar_e", "save();", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validarclaveEstado('ContentPlaceHolder1_c_estado',1);", true);
                        }
                    }
                }
                catch (Exception ex)
                {
                    string test = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "testa", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                }               
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_estado();", true);
            }
        }

        protected void update_estado_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(c_estado.Text) && !String.IsNullOrEmpty(n_estado.Text) && cbo_pais.SelectedValue != "0")
            {                
                try
                {
                    serviceCatalogo.Upd_testa(c_estado.Text, cbo_pais.SelectedValue, n_estado.Text, Session["usuario"].ToString(), estatus_estado.SelectedValue);
                    n_estado.Text = string.Empty;
                    estatus_estado.SelectedIndex = 0;
                    c_estado.ReadOnly = false;
                    c_estado.Text = string.Empty;
                    update_estado.Visible=false;
                    save_estado.Visible = true;
                    cancel_estado.Visible = false;
                    grid_bind_estados();
                    GridEstados.SelectedIndex = -1;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "update_e", "update();", true);
                }
                catch (Exception ex)
                {
                    string test = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "testa", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_estado();", true);
            }
        }

        protected void cancel_estado_Click(object sender, EventArgs e)
        {
            combo_pais();
            combo_estatus();
            c_estado.Text = null;
            c_estado.ReadOnly=false;
            n_estado.Text = null;
            save_estado.Visible = true;
            update_estado.Visible = false;
            cancel_estado.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
            grid_bind_estados();
        }
    }
}