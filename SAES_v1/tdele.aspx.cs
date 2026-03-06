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
using static SAES_DBO.Models.ModelMenu;

namespace SAES_v1
{
    public partial class tdele : System.Web.UI.Page
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

                c_deleg.Attributes.Add("onblur", "validarclaveDelegacion('ContentPlaceHolder1_c_deleg', 0));");
                c_deleg.Attributes.Add("oninput", "validarclaveDelegacion('ContentPlaceHolder1_c_deleg', 0));");
                n_deleg.Attributes.Add("onblur", "validarNombreDelegacion('ContentPlaceHolder1_e_deleg')");
                n_deleg.Attributes.Add("oninput", "validarNombreDelegacion('ContentPlaceHolder1_e_deleg')");
                LlenaPagina();
                if (!IsPostBack)
                {
                    combo_estatus();
                    combo_paises_deleg();
                    combo_estados_deleg();
                    grid_bind_delegacion();
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);

        }

        protected void combo_estatus()
        {
            e_deleg.Items.Clear();
            e_deleg.Items.Add(new ListItem("Activo", "A"));
            e_deleg.Items.Add(new ListItem("Inactivo", "B"));
        }
        protected void combo_paises_deleg()
        {
            //cbop_deleg.Items.Clear();
            //cboe_deleg.Items.Clear();
            //cboe_deleg.Items.Add(new ListItem("----Selecciona un estado----", "0"));


            try
            {

                cbop_deleg.DataSource = serviceCatalogo.ObtenerPaisesActivos(); ;
                cbop_deleg.DataValueField = "Clave";
                cbop_deleg.DataTextField = "Nombre";
                cbop_deleg.DataBind();
                //cbop_deleg_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                string test = ex.Message;
            }


        }
        protected void cbop_deleg_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbop_deleg.SelectedValue == "")
            {
                combo_paises_deleg();

            }
            else
            {
                cboe_deleg.Items.Clear();
                combo_estados_deleg();
            }
            //c_deleg.Attributes.Remove("readonly");
            //GridDelegacion.Visible = false;
        }
        protected void combo_estados_deleg()
        {
            try
            {

                cboe_deleg.DataSource = serviceCatalogo.ObtenerEstados(cbop_deleg.SelectedValue);
                cboe_deleg.DataValueField = "Clave";
                cboe_deleg.DataTextField = "Descripcion";
                cboe_deleg.DataBind();
                cboe_deleg_SelectedIndexChanged(null, null);

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
                objPermiso = servicePermiso.ObtenerPermisoFormulario(Session["usuario"].ToString(), "tdele");
                if (objPermiso != null)
                {
                    if (objPermiso.usme_update == "0" || objPermiso.usme_select == "0")
                    {
                        btn_delegacion.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                    }

                }
                else
                {
                    btn_delegacion.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                }


            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tdele", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);

            }
        }

        protected void grid_bind_delegacion()
        {
            List<ModelObtenDelegacionesResponse> lst = new List<ModelObtenDelegacionesResponse>();
            try
            {
                GridDelegacion.DataSource = lst;
                GridDelegacion.DataBind();

                GridDelegacion.DataSource = serviceCatalogo.ObtenerDelegaciones(cbop_deleg.SelectedValue, cboe_deleg.SelectedValue, "");
                GridDelegacion.DataBind();
                //GridDelegacion.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridDelegacion.UseAccessibleHeader = true;
                //GridDelegacion.Visible = true;
            }
            catch (Exception ex)
            {
                ///logs
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tdele", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }

        }
        protected void GridDelegacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridDelegacion.SelectedRow;
            try
            {
                c_deleg.Text = row.Cells[1].Text;
                n_deleg.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);
                combo_paises_deleg();
                cbop_deleg.SelectedValue = row.Cells[3].Text;
                combo_estados_deleg();
                cboe_deleg.SelectedValue = row.Cells[5].Text;
                combo_estatus();
                e_deleg.SelectedValue = row.Cells[7].Text;
                c_deleg.ReadOnly = true;
                save_deleg.Visible = false;
                update_deleg.Visible = true;
                //grid_bind_delegacion();
            }
            catch (Exception ex)
            {
                ///logs
            }


        }

        protected void cboe_deleg_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cboe_deleg.SelectedValue != "")
            //{
            c_deleg.Text = null;
            n_deleg.Text = null;
            //c_deleg.Attributes.Remove("readonly");
            //combo_estatus();
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
            grid_bind_delegacion();
            //}
        }

        protected void cancel_deleg_Click(object sender, EventArgs e)
        {
            save_deleg.Visible = true;
            update_deleg.Visible = false;
            combo_paises_deleg();
            cboe_deleg.SelectedIndex = 0;
            combo_estatus();
            c_deleg.Text = null;
            c_deleg.ReadOnly = false;
            n_deleg.ReadOnly = false;
            n_deleg.Text = null;
            //GridDelegacion.Visible = false;
        }

        protected void save_deleg_Click(object sender, EventArgs e)
        {
            ModelInsertarTdeleResponse objExisteRegistro = new ModelInsertarTdeleResponse();

            if (cbop_deleg.SelectedValue != "0" && cboe_deleg.SelectedValue != "0" && !String.IsNullOrEmpty(c_deleg.Text) && !String.IsNullOrEmpty(n_deleg.Text))
            {


                try
                {
                    objExisteRegistro = serviceCatalogo.InsertarDelegaciones(c_deleg.Text, n_deleg.Text, cboe_deleg.SelectedValue, cbop_deleg.SelectedValue,
                        Session["usuario"].ToString(), e_deleg.SelectedValue);
                    if (objExisteRegistro != null)
                    {
                        if (objExisteRegistro.Existe == "0")
                        {

                            c_deleg.Text = null;
                            n_deleg.Text = null;
                            combo_estatus();
                            grid_bind_delegacion();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar_d", "save();", true);
                        }
                        else
                        {

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validarclaveDelegacion('ContentPlaceHolder1_c_deleg', 1);", true);

                        }
                    }
                    else
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validarclaveDelegacion('ContentPlaceHolder1_c_deleg', 1);", true);

                    }
                }
                catch (Exception ex)
                {
                    string test = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tdele", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                }

            }
            else
            {
                grid_bind_delegacion();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_delegacion();", true);
            }
        }

        protected void update_deleg_Click(object sender, EventArgs e)
        {
            if (cbop_deleg.SelectedValue != "0" && cboe_deleg.SelectedValue != "0" && !String.IsNullOrEmpty(c_deleg.Text) && !String.IsNullOrEmpty(n_deleg.Text))
            {
                //string Query = "UPDATE tdele SET tdele_desc='" + n_deleg.Text + "',tdele_user='" + Session["usuario"].ToString() + "',tdele_date=CURRENT_TIMESTAMP(),tdele_estatus='" + e_deleg.SelectedValue + "' WHERE tdele_clave='" + c_deleg.Text + "' AND tdele_testa_clave='" + cboe_deleg.SelectedValue + "' AND tdele_tpais_clave='" + cbop_deleg.SelectedValue + "'";

                serviceCatalogo.EditarrDelegaciones(c_deleg.Text, n_deleg.Text, cboe_deleg.SelectedValue, cbop_deleg.SelectedValue, Session["usuario"].ToString(), e_deleg.SelectedValue);
                try
                {
                    c_deleg.ReadOnly = false;
                    n_deleg.Text = string.Empty;
                    e_deleg.SelectedIndex = 0;
                    c_deleg.Text = null;
                    grid_bind_delegacion();
                    GridDelegacion.SelectedIndex = -1;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "update_d", "update();", true);
                }
                catch (Exception ex)
                {
                    string test = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tdele", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                }

            }
            else
            {
                grid_bind_delegacion();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_delegacion();", true);
            }
        }


    }
}