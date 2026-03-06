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
    public partial class tcopo : System.Web.UI.Page
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

                c_zip.Attributes.Add("onblur", "validarclaveZip('ContentPlaceHolder1_c_zip', 0);");
                c_zip.Attributes.Add("oninput", "validarclaveZip('ContentPlaceHolder1_c_zip', 0);");
                n_zip.Attributes.Add("onblur", "validarNombreZip('ContentPlaceHolder1_n_zip');");
                n_zip.Attributes.Add("oninput", "validarNombreZip('ContentPlaceHolder1_n_zip');");

                LlenaPagina();
                if (!IsPostBack)
                {
                    combo_estatus();
                    combo_paises_zip();
                }
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);

        }
        private void LlenaPagina()
        {
            ModelObtenerPermisoFormResponse objPermiso = new ModelObtenerPermisoFormResponse();

            try
            {
                objPermiso = servicePermiso.ObtenerPermisoFormulario(Session["usuario"].ToString(), "tcopo");
                if (objPermiso != null)
                {
                    if (objPermiso.usme_update == "0" || objPermiso.usme_select == "0")
                    {

                        btn_zip.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);

                        //grid_bind_estados();
                    }
                }
                else
                {
                    btn_zip.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                }
                grid_bind_zip();
            }
            catch (Exception ex)
            {
                //Logs
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcopo", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        //private void LlenaPagina()
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "menu", "carga_menu();", true);

        //    string QerySelect = "select tusme_update, tusme_select from tuser, tusme, tmede " +
        //                      " where tuser_clave = '" + Session["usuario"].ToString() + "'" +
        //                      " and tusme_trole_clave = tuser_trole_clave and tusme_tmenu_clave = 1 and tusme_tmenu_clave = tmede_tmenu_clave " +
        //                      " and tusme_tmede_clave = tmede_clave and tmede_forma='tcopo' ";

        //    MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
        //    ConexionMySql.Open();
        //    try
        //    {
        //        MySqlDataAdapter sqladapter = new MySqlDataAdapter();

        //        DataSet dssql1 = new DataSet();

        //        MySqlCommand commandsql1 = new MySqlCommand(QerySelect, ConexionMySql);
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
        //            btn_zip.Visible = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ///logs
        //        string mensaje_error = ex.Message.Replace("'", "-");
        //        Global.inserta_log(mensaje_error, "tcopo", Session["usuario"].ToString());
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
        //    }

        //}
        protected void combo_estatus()
        {
            e_zip.Items.Clear();
            e_zip.Items.Add(new ListItem("Activo", "A"));
            e_zip.Items.Add(new ListItem("Inactivo", "B"));
        }
        protected void cbop_zip_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbop_zip.SelectedValue != "0")
            {
                combo_estados_zip();
            }
        }

        protected void cboe_zip_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboe_zip.SelectedValue != "0")
            {
                combo_delegacion_zip();
            }
        }

        protected void cbod_zip_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbod_zip.SelectedValue != "0")
            {
                grid_bind_zip();
            }
        }
        protected void grid_bind_zip()
        {
            try
            {
                GridZip.DataSource = serviceCatalogo.QRY_TCOPO(cbop_zip.SelectedValue, cboe_zip.SelectedValue, cbod_zip.SelectedValue);
                GridZip.DataBind();

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcopo", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }

        }


        protected void GridZip_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = GridZip.SelectedRow;
                combo_paises_zip();
                cbop_zip.SelectedValue = row.Cells[3].Text;
                combo_estados_zip();
                cboe_zip.SelectedValue = row.Cells[5].Text;
                combo_delegacion_zip();
                cbod_zip.SelectedValue = row.Cells[7].Text;
                c_zip.Text = row.Cells[1].Text;
                c_zip.ReadOnly = true;
                n_zip.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);
                //n_zip.Attributes.Add("readonly", "");
                combo_estatus();
                e_zip.SelectedValue = row.Cells[9].Text;
                save_zip.Visible = false;
                update_zip.Visible = true;
                cancel_zip.Visible = true;
                grid_bind_zip();
            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcopo", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void combo_paises_zip()
        {
            cbop_zip.Items.Clear();
            cboe_zip.Items.Clear();
            cboe_zip.Items.Add(new ListItem("-------", "0"));
            cbod_zip.Items.Clear();
            cbod_zip.Items.Add(new ListItem("------", "0"));

            try
            {

                cbop_zip.DataSource = serviceCatalogo.QRY_TPAIS();
                cbop_zip.DataValueField = "Clave";
                cbop_zip.DataTextField = "Nombre";
                cbop_zip.DataBind();

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcopo", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }
        protected void combo_estados_zip()
        {
            try
            {
                cboe_zip.DataSource = serviceCatalogo.QRY_ESTADOS(cbop_zip.SelectedValue);
                cboe_zip.DataValueField = "Clave";
                cboe_zip.DataTextField = "Descripcion";
                cboe_zip.DataBind();
                combo_delegacion_zip();
            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcopo", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }

        }
        protected void combo_delegacion_zip()
        {
            try
            {

                cbod_zip.DataSource = serviceCatalogo.QRY_MUNICIPIOS(cbop_zip.SelectedValue, cboe_zip.SelectedValue);
                cbod_zip.DataValueField = "Clave";
                cbod_zip.DataTextField = "Descripcion";
                cbod_zip.DataBind();

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcopo", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }
        //protected bool valida_clave_zip(string clave, string nombre)
        //{
        //    string Query = "";
        //    Query = "SELECT COUNT(*) Indicador FROM TCOPO WHERE tcopo_tpais_clave='" + cbop_zip.SelectedValue + "' AND tcopo_testa_clave='" + cboe_zip.SelectedValue + "'AND tcopo_tdele_clave='" + cbod_zip.SelectedValue + "' AND tcopo_clave='" + clave + "' AND tcopo_desc='" + nombre + "'";

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
        protected void save_zip_Click(object sender, EventArgs e)
        {
            ModelInstcopoResponse objExisteRegistro = new ModelInstcopoResponse();

            if (cbop_zip.SelectedValue != "0" && cboe_zip.SelectedValue != "0" && cbod_zip.SelectedValue != "0" && !String.IsNullOrEmpty(c_zip.Text) && !String.IsNullOrEmpty(n_zip.Text))
            {
                objExisteRegistro = serviceCatalogo.Ins_tcopo(c_zip.Text, n_zip.Text, e_zip.Text, cbop_zip.SelectedValue, cboe_zip.SelectedValue, cbod_zip.SelectedValue, Session["usuario"].ToString());
                if (objExisteRegistro != null)
                {
                    if (objExisteRegistro.Existe == "0")
                    {
                        try
                        {
                            c_zip.Text = null;
                            n_zip.Text = null;
                            combo_estatus();
                            grid_bind_zip();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar_z", "save();", true);

                        }
                        catch (Exception ex)
                        {
                            string test = ex.Message;
                            string mensaje_error = ex.Message.Replace("'", "-");
                            Global.inserta_log(mensaje_error, "tcopo", Session["usuario"].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                        }
                    }
                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validarclaveZip('ContentPlaceHolder1_c_zip', 1);", true);

                }
                else
                {
                    grid_bind_zip();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validarclaveZip('ContentPlaceHolder1_c_zip', 1);", true);
                }
            }
            else
            {
                grid_bind_zip();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_zip();", true);
            }
        }

        protected void update_zip_Click(object sender, EventArgs e)
        {
            if (cbop_zip.SelectedValue != "0" && cboe_zip.SelectedValue != "0" && cbod_zip.SelectedValue != "0" && !String.IsNullOrEmpty(c_zip.Text) && !String.IsNullOrEmpty(n_zip.Text))
            {
                try
                {
                    serviceCatalogo.UPD_TCOPO(Session["usuario"].ToString(), c_zip.Text, e_zip.SelectedValue, cbop_zip.SelectedValue, cboe_zip.SelectedValue, cbod_zip.SelectedValue, n_zip.Text);
                    c_zip.ReadOnly = false;
                    n_zip.Text = string.Empty;
                    e_zip.SelectedIndex = 0;
                    cancel_zip.Visible = false;
                    update_zip.Visible = false;
                    save_zip.Visible = true;
                    grid_bind_zip();
                }
                catch (Exception ex)
                {
                    string test = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tcopo", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                }
            }
            else
            {
                grid_bind_zip();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_zip();", true);
            }
        }

        protected void cancel_zip_Click(object sender, EventArgs e)
        {
            combo_paises_zip();
            combo_estatus();
            c_zip.Text = null;
            c_zip.ReadOnly = false;
            n_zip.Text = null;
            GridZip.Visible = false;
            cancel_zip.Visible = false;
        }
    }
}