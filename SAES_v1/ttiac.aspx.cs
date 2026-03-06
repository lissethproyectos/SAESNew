using SAES_Services;
using SAES_v1.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using static SAES_DBO.Models.ModelMenu;

namespace SAES_v1
{
    public partial class ttiac : System.Web.UI.Page
    {
        Utilidades utils = new Utilidades();
        Catalogos catalogos = new Catalogos();
        TiposAcreditacion model = new TiposAcreditacion();
        MenuService servicePermiso = new MenuService();

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
                    //CargaInicial();
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
                objPermiso = servicePermiso.ObtenerPermisoFormulario(Session["usuario"].ToString(), "ttiac");
                if (objPermiso != null)
                {
                    if (objPermiso.usme_update == "0" || objPermiso.usme_select == "0")
                    {
                        btn_tcodo.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                    }
                    else
                        CargaInicial();
                }
                else
                {
                    btn_tcodo.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                }


            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "ttiac", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);

            }
        }


        public void CargaInicial()
        {
            
            btn_update.Visible = false;
            btn_guardar.Visible = true;
            ddl_estatus = utils.BeginDropdownList(ddl_estatus, catalogos.obtenEstatusCatOpcionesTitulacion());
            ddl_estatus.SelectedValue = "A";
            /*
            txb_clave.Text = "";
            hdf_claveOld.Value = "";
            txb_descripcion.Text = "";
            txb_claveCert.Text = "";
            txb_siglasCert.Text = "";
            */
            Gridttiac = utils.BeginGrid(Gridttiac, model.obtenTiposAcreditacion());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "generic_datatable_load", "   ('Gridttiac');", true);
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                string clave = txb_clave.Text;
                string descripcion = txb_descripcion.Text;
                string estatus = ddl_estatus.SelectedValue;
                string claveCert = (txb_claveCert.Text == "") ? null : txb_claveCert.Text;
                string siglasCert = (txb_siglasCert.Text == "") ? null : txb_siglasCert.Text;
                string usuario = Session["usuario"].ToString();


                if (!String.IsNullOrEmpty(txb_clave.Text) && !String.IsNullOrEmpty(txb_descripcion.Text) && !String.IsNullOrEmpty(txb_claveCert.Text) && !String.IsNullOrEmpty(txb_siglasCert.Text))
                {

                    model.InsertTipoAcreditacion(clave, descripcion, usuario, claveCert, siglasCert, estatus);
                    CargaInicial();
                    txb_clave.Text = "";
                    hdf_claveOld.Value = "";
                    txb_descripcion.Text = "";
                    txb_claveCert.Text = "";
                    txb_siglasCert.Text = "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);

                }
                else
                {
                    CargaInicial();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_ttiac();", true);
                }

                
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "ttiac", Session["usuario"].ToString(), ex.StackTrace);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
            }
        }

        protected void btn_update_Click(object sender, EventArgs e)
        {
            try
            {
                string oldclave = hdf_claveOld.Value;
                string clave = txb_clave.Text;
                string descripcion = txb_descripcion.Text;
                string estatus = ddl_estatus.SelectedValue;
                string claveCert = (txb_claveCert.Text == "") ? null : txb_claveCert.Text;
                string siglasCert = (txb_siglasCert.Text == "") ? null : txb_siglasCert.Text;
                string usuario = Session["usuario"].ToString();

                if (!String.IsNullOrEmpty(txb_clave.Text) && !String.IsNullOrEmpty(txb_descripcion.Text) && !String.IsNullOrEmpty(txb_claveCert.Text) && !String.IsNullOrEmpty(txb_siglasCert.Text))
                {

                    model.UpdateTipoAcreditacion(oldclave, clave, descripcion, usuario, claveCert, siglasCert, estatus);
                    CargaInicial();
                    txb_clave.Text = "";
                    hdf_claveOld.Value = "";
                    txb_descripcion.Text = "";
                    txb_claveCert.Text = "";
                    txb_siglasCert.Text = "";
                    Gridttiac.SelectedIndex = -1;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "update_p", "update();", true);
                    txb_clave.Attributes.Remove("readonly");
                }
                else
                {
                    CargaInicial();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_ttiac();", true);
                }

                
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "ttiac", Session["usuario"].ToString(), ex.StackTrace);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            try
            {
                txb_clave.Text = "";
                hdf_claveOld.Value = "";
                txb_descripcion.Text = "";
                txb_claveCert.Text = "";
                txb_siglasCert.Text = "";
                txb_clave.Attributes.Remove("readonly");
                Gridttiac.SelectedIndex = -1;
                CargaInicial();

            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "ttiac", Session["usuario"].ToString(), ex.StackTrace);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
            }
        }

        protected void Gridttiac_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = Gridttiac.SelectedRow;
                txb_clave.Text = row.Cells[1].Text;
                hdf_claveOld.Value = row.Cells[1].Text;
                txb_descripcion.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);
                string status = HttpUtility.HtmlDecode(row.Cells[5].Text);
                string claveStatus = "";
                DataTable dt_estatus = catalogos.obtenEstatusCatOpcionesTitulacion();
                foreach (DataRow item in dt_estatus.Rows)
                {
                    if (item[1].ToString() == status)
                    {
                        claveStatus = item[0].ToString();
                        break;
                    }
                }
                ddl_estatus.SelectedValue = claveStatus;
                txb_claveCert.Text = HttpUtility.HtmlDecode(row.Cells[3].Text);
                txb_siglasCert.Text = HttpUtility.HtmlDecode(row.Cells[4].Text);
                btn_update.Visible = true;
                btn_guardar.Visible = false;
                txb_clave.Attributes.Add("readonly", "");
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "ttiac", Session["usuario"].ToString(), ex.StackTrace);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
            }
        }
    }
}