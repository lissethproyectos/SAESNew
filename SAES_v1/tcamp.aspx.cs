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
    public partial class tcamp : System.Web.UI.Page
    {
        #region <Variables>
        Utilidades utils = new Utilidades();
        Catalogos serviceCatalogo = new Catalogos();
        Catalogos_grales_Service serviceCatalogoGrals = new Catalogos_grales_Service();
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

                c_campus.Attributes.Add("onblur", "validarclaveCampus('ContentPlaceHolder1_c_campus',0)");
                c_campus.Attributes.Add("oninput", "validarclaveCampus('ContentPlaceHolder1_c_campus',0)");
                n_campus.Attributes.Add("onblur", "validarNombreCampus('ContentPlaceHolder1_n_campus')");
                n_campus.Attributes.Add("oninput", "validarNombreCampus('ContentPlaceHolder1_n_campus')");

                if (!IsPostBack)
                {
                    LlenaPagina();
                    combo_estatus();
                    combo_pais();
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);

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
                objPermiso = servicePermiso.ObtenerPermisoFormulario(Session["usuario"].ToString(), "tcamp");
                if (objPermiso != null)
                {
                    if (objPermiso.usme_update == "0" || objPermiso.usme_select == "0")
                    {
                        btn_campus.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                    }

                    grid_campus_bind();

                }
                else
                {
                    btn_campus.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                }


            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcamp", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);

            }
        }

        protected void combo_estatus()
        {
            estatus_campus.Items.Clear();
            estatus_campus.Items.Add(new ListItem("Activo", "A"));
            estatus_campus.Items.Add(new ListItem("Inactivo", "B"));
        }
        protected void combo_pais()
        {

            dde_campus.Items.Clear();
            dde_campus.Items.Add(new ListItem("--------", "0"));
            ddd_campus.Items.Clear();
            ddd_campus.Items.Add(new ListItem("-------", "0"));
            ddz_campus.Items.Clear();
            ddz_campus.Items.Add(new ListItem("-------", "0"));


            try
            {

                ddp_campus.DataSource = serviceCatalogo.ObtenerPaisesActivos();
                ddp_campus.DataValueField = "Clave";
                ddp_campus.DataTextField = "Nombre";
                ddp_campus.DataBind();

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcamp", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }

        }
        protected void combo_estado(string clave_pais)
        {

            try
            {
                dde_campus.DataSource = serviceCatalogo.ObtenerEstados(ddp_campus.SelectedValue); ;
                dde_campus.DataValueField = "Clave";
                dde_campus.DataTextField = "Descripcion";
                dde_campus.DataBind();

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcamp", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }

        }
        protected void combo_delegacion(string clave_pais, string clave_edo)
        {
            try
            {
                ddd_campus.DataSource = serviceCatalogo.ObtenerDelegaciones(clave_pais, clave_edo, "A");
                ddd_campus.DataValueField = "Clave";
                ddd_campus.DataTextField = "Nombre";
                ddd_campus.DataBind();
            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcamp", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }
        protected void combo_zip(string clave_pais, string clave_edo, string clave_deleg)
        {

            try
            {
                ddz_campus.DataSource = serviceCatalogo.QRY_TCOPO(clave_pais, clave_edo, clave_deleg); ;
                ddz_campus.DataValueField = "Clave";
                ddz_campus.DataTextField = "Nombre";
                ddz_campus.DataBind();

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcamp", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }

        }
        protected void grid_campus_bind()
        {

            try
            {

                GridCampus.DataSource = serviceCatalogo.ObtenerCatCampus();
                GridCampus.DataBind();

                GridCampus.EditIndex = -1;

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcamp", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }


        }

        protected void GridCampus_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridCampus.SelectedRow;
            c_campus.Text = row.Cells[1].Text;
            c_campus.Attributes.Add("readonly", "");
            n_campus.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);
            a_campus.Text = HttpUtility.HtmlDecode(row.Cells[3].Text);
            RFC_campus.Text = HttpUtility.HtmlDecode(row.Cells[4].Text);
            combo_estatus();
            estatus_campus.SelectedValue = row.Cells[5].Text;
            combo_pais();
            ddp_campus.SelectedValue = row.Cells[8].Text;
            combo_estado(ddp_campus.SelectedValue);
            dde_campus.SelectedValue = row.Cells[9].Text;
            combo_delegacion(ddp_campus.SelectedValue, dde_campus.SelectedValue);
            ddd_campus.SelectedValue = row.Cells[11].Text;
            zip_campus.Text = HttpUtility.HtmlDecode(row.Cells[13].Text);
            combo_zip(ddp_campus.SelectedValue, dde_campus.SelectedValue, ddd_campus.SelectedValue);
            string col_text = HttpUtility.HtmlDecode(row.Cells[14].Text);
            if (col_text == null || col_text.Trim() == "") { ddz_campus.SelectedValue = "0"; } else { ddz_campus.SelectedValue = ddz_campus.Items.FindByText(col_text).Value; ; }
            direc_campus.Text = HttpUtility.HtmlDecode(row.Cells[15].Text);
            guardar_campus.Visible = false;
            actualizar_campus.Visible = true;
            grid_campus_bind();
        }

        protected void cancelar_campus_Click(object sender, EventArgs e)
        {
            c_campus.Text = null;
            c_campus.Attributes.Remove("readonly");
            n_campus.Text = null;
            a_campus.Text = null;
            RFC_campus.Text = null;
            zip_campus.Text = null;
            direc_campus.Text = null;
            combo_estatus();
            combo_pais();
            guardar_campus.Visible = true;
            actualizar_campus.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
            grid_campus_bind();
        }

        protected void guardar_campus_Click(object sender, EventArgs e)
        {
            ModelInsertarCampusResponse objExiste = new ModelInsertarCampusResponse();
            if (!String.IsNullOrEmpty(c_campus.Text) && !String.IsNullOrEmpty(n_campus.Text))
            {

                string colonia = null;
                if (ddz_campus.SelectedValue != "0") { colonia = ddz_campus.SelectedItem.Text; }
                try
                {
                    objExiste = serviceCatalogo.InsertarCampus(c_campus.Text, n_campus.Text, direc_campus.Text, colonia,
                        ddp_campus.SelectedValue, dde_campus.SelectedValue, ddd_campus.SelectedValue, zip_campus.Text,
                        Session["usuario"].ToString(), estatus_campus.SelectedValue, a_campus.Text, RFC_campus.Text, "");


                    if (objExiste != null)
                    {
                        if (objExiste.Existe == "0")
                        {
                            c_campus.Text = null;
                            n_campus.Text = null;
                            zip_campus.Text = null;
                            a_campus.Text = null;
                            RFC_campus.Text = null;
                            direc_campus.Text = null;
                            combo_estatus();
                            combo_pais();
                            grid_campus_bind();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                        }
                        else
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "error_clave", "error_clave();", true);
                    }
                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);

                }
                catch (Exception ex)
                {
                    string test = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tcamp", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                }

            }
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_campus();", true);
            //}
        }

        protected void actualizar_campus_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(c_campus.Text) && !String.IsNullOrEmpty(n_campus.Text))
            {
                string colonia = null;
                if (ddz_campus.SelectedValue != "0") { colonia = ddz_campus.SelectedItem.Text; }

                try
                {
                    serviceCatalogo.EditarCampus(c_campus.Text, n_campus.Text, direc_campus.Text, colonia,
                        ddp_campus.SelectedValue, dde_campus.SelectedValue, ddd_campus.SelectedValue, zip_campus.Text,
                        Session["usuario"].ToString(), estatus_campus.SelectedValue, a_campus.Text, RFC_campus.Text, "");

                    c_campus.Text = null;
                    n_campus.Text = null;
                    zip_campus.Text = null;
                    a_campus.Text = null;
                    RFC_campus.Text = null;
                    direc_campus.Text = null;
                    combo_estatus();
                    combo_pais();
                    grid_campus_bind();
                    GridCampus.SelectedIndex = -1;
                    //string Query = "UPDATE tcamp SET tcamp_desc='" + n_campus.Text + "',tcamp_calle='" + direc_campus.Text + "'," +
                    //    "tcamp_colonia='" + colonia + "',tcamp_tpais_clave='" + ddp_campus.SelectedValue + "'," +
                    //    "tcamp_testa_clave='" + dde_campus.SelectedValue + "'" +
                    //    ",tcamp_tdele_clave='" + ddd_campus.SelectedValue + "',tcamp_tcopo_clave='" + zip_campus.Text + "'" +
                    //    ",tcamp_user='" + Session["usuario"].ToString() + "',tcamp_date=current_timestamp()," +
                    //    "tcamp_estatus='" + estatus_campus.SelectedValue + "',tcamp_abr='" + a_campus.Text + "'," +
                    //    "tcamp_rfc='" + RFC_campus.Text + "' WHERE tcamp_clave='" + c_campus.Text + "'";


                    ScriptManager.RegisterStartupScript(this, this.GetType(), "update_p", "update();", true);
                }
                catch (Exception ex)
                {
                    string test = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tcamp", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_campus();", true);
            }
        }

        protected void ddp_campus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddp_campus.SelectedValue != "0")
            {
                combo_estado(ddp_campus.SelectedValue);
                grid_campus_bind();
            }
            else
            {
                combo_pais();
                zip_campus.Text = null;
                grid_campus_bind();
            }
        }

        protected void dde_campus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dde_campus.SelectedValue != "0")
            {
                combo_delegacion(ddp_campus.SelectedValue, dde_campus.SelectedValue);
            }
            else
            {
                //ddd_campus.Items.Clear();
                //ddd_campus.Items.Add(new ListItem("-------", "0"));
                ddd_campus_SelectedIndexChanged(null, null);
                ddz_campus.Items.Clear();
                ddz_campus.Items.Add(new ListItem("-------", "0"));
                zip_campus.Text = null;
            }
        }
        protected void zip_campus_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(zip_campus.Text))
            {
                combo_zip(ddp_campus.SelectedValue, dde_campus.SelectedValue, ddd_campus.SelectedValue);
            }
        }

        protected void ddd_campus_SelectedIndexChanged(object sender, EventArgs e)
        {
            combo_zip(ddp_campus.SelectedValue, dde_campus.SelectedValue, ddd_campus.SelectedValue);
        }
    }
}