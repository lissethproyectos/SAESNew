using MySql.Data.MySqlClient;
using SAES_DBO.Models;
using SAES_Services;
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
    public partial class tstal : System.Web.UI.Page
    {
        Catalogos serviceCatalogo = new Catalogos();
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

                //c_campus.Attributes.Add("onblur", "validarclaveCampus('ContentPlaceHolder1_c_campus',0)");
                //c_campus.Attributes.Add("oninput", "validarclaveCampus('ContentPlaceHolder1_c_campus',0)");
                //n_campus.Attributes.Add("onblur", "validarNombreCampus('ContentPlaceHolder1_n_campus')");
                //n_campus.Attributes.Add("oninput", "validarNombreCampus('ContentPlaceHolder1_n_campus')");

                if (!IsPostBack)
                {
                    LlenaPagina();
                    combo_estatus();
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
                objPermiso = servicePermiso.ObtenerPermisoFormulario(Session["usuario"].ToString(), "tpais");
                if (objPermiso != null)
                {
                    if (objPermiso.usme_update == "0" || objPermiso.usme_select == "0")
                    {
                        btn_tstal.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                    }
                    else
                        grid_tstal_bind();
                }
                else
                {
                    btn_tstal.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                }


            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tstal", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);

            }
        }

        protected void combo_estatus()
        {
            ddl_estatus.Items.Clear();
            ddl_estatus.Items.Add(new ListItem("Activo", "A"));
            ddl_estatus.Items.Add(new ListItem("Inactivo", "B"));

            ddl_tipo.Items.Clear();
            ddl_tipo.Items.Add(new ListItem("Vigente", "V"));
            ddl_tipo.Items.Add(new ListItem("Baja", "B"));
            ddl_tipo.Items.Add(new ListItem("Egresado", "E"));
            ddl_tipo.Items.Add(new ListItem("Titulado", "T"));

        }
        protected void grid_tstal_bind()



        {
            //strQueryGrid = " select tstal_clave clave, tstal_desc nombre,  " +
            //  " tstal_tipo c_tipo,CASE WHEN tstal_tipo = 'V' THEN 'VIGENTE' WHEN tstal_tipo ='B' THEN 'BAJA' WHEN tstal_tipo ='E' THEN 'EGRESADO' WHEN tstal_tipo ='T' THEN 'TITULADO' END Tipo, " +
            //  " tstal_estatus c_estatus,CASE WHEN tstal_estatus = 'A' THEN 'ACTIVO' ELSE 'INACTIVO' END Estatus, fecha(date_format(tstal_date,'%Y-%m-%d')) fecha " +
            //  " from tstal order by clave ";
            try
            {
                Gridtstal.DataSource = serviceCatalogo.QRY_TSTAL();
                Gridtstal.DataBind();
                Gridtstal.DataMember = "tstal";
                Gridtstal.HeaderRow.TableSection = TableRowSection.TableHeader;
                Gridtstal.UseAccessibleHeader = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tstal", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            txt_tstal.Text = null;
            txt_nombre.Text = null;
            combo_estatus();
            btn_save.Visible = true;
            btn_update.Visible = false;
            txt_tstal.Attributes.Remove("readonly");
            grid_tstal_bind();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            ModelInsertarTstalResponse objExiste = new ModelInsertarTstalResponse();
            try
            {
                objExiste = serviceCatalogo.InsertarTstal(txt_tstal.Text, txt_nombre.Text, Session["usuario"].ToString(), ddl_estatus.SelectedValue, ddl_tipo.SelectedValue);
                if (objExiste.Existe != null)
                {
                    if (objExiste.Existe == "0")
                    {
                        txt_tstal.Text = null;
                        txt_nombre.Text = null;
                        ddl_estatus.SelectedIndex = 0;
                        //combo_estatus();
                        grid_tstal_bind();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validarClave('ContentPlaceHolder1_txt_tstal',1);", true);
                        grid_tstal_bind();
                    }
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tstal", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
            }
        }

        protected void btn_update_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txt_tstal.Text) && !String.IsNullOrEmpty(txt_nombre.Text))
            {                
                try
                {

                    serviceCatalogo.EditarTstal(txt_tstal.Text, txt_nombre.Text, Session["usuario"].ToString(), ddl_estatus.SelectedValue, ddl_tipo.SelectedValue);
                    txt_tstal.ReadOnly = false;
                    txt_tstal.Text = null;
                    txt_nombre.Text = null;
                    ddl_tipo.SelectedIndex = 0;
                    ddl_estatus.SelectedIndex = 0;
                    grid_tstal_bind();
                    Gridtstal.SelectedIndex = -1;
                    btn_save.Visible = true;
                    btn_update.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "update_p", "update();", true);
                }
                catch (Exception ex)
                {
                    string test = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tstal", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_tstal();", true);
            }
        }



        protected void Gridtstal_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = Gridtstal.SelectedRow;
            txt_tstal.Text = row.Cells[1].Text;
            txt_nombre.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);
            combo_estatus();
            ddl_tipo.SelectedValue = row.Cells[3].Text;
            ddl_estatus.SelectedValue = row.Cells[5].Text;
            btn_update.Visible = true;
            btn_save.Visible = false;
            txt_tstal.ReadOnly = true;
            grid_tstal_bind();
        }
    }
}