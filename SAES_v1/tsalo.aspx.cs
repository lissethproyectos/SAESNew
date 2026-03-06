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
using static SAES_DBO.Models.ModelCargaAcademica;
using static SAES_DBO.Models.ModelMenu;

namespace SAES_v1
{
    public partial class tsalo : System.Web.UI.Page
    {

        #region <Variables>
        Utilidades utils = new Utilidades();
        Catalogos serviceCatalogo = new Catalogos();
        List<ModelObtenPaisesResponse> lstPaises = new List<ModelObtenPaisesResponse>();
        MenuService servicePermiso = new MenuService();
        DocenteService serviceDocente = new DocenteService();
        CargaAcademicaService servicePlanAcad = new CargaAcademicaService();
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

                //c_campus.Attributes.Add("onblur", "validarclaveCampus('ContentPlaceHolder1_c_campus',0)");
                //c_campus.Attributes.Add("oninput", "validarclaveCampus('ContentPlaceHolder1_c_campus',0)");
                //n_campus.Attributes.Add("onblur", "validarNombreCampus('ContentPlaceHolder1_n_campus')");
                //n_campus.Attributes.Add("oninput", "validarNombreCampus('ContentPlaceHolder1_n_campus')");

                if (!IsPostBack)
                {
                    LlenaPagina();
                    combo_estatus();
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);
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
                        btn_tsalo.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                    }
                    else
                    {
                        grid_tsalo_bind();
                    }
                }
                else
                {
                    btn_tsalo.Visible = false;
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

        private DataTable GetData(MySqlCommand cmd)
        {
            DataTable dt = new DataTable();
            String strConnString = System.Configuration.ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strConnString);
            MySqlDataAdapter sda = new MySqlDataAdapter();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            try
            {
                con.Open();
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                return dt;
            }
            catch
            {
                return null;
            }
            finally
            {
                con.Close();
                sda.Dispose();
                con.Dispose();
            }
        }

        protected void combo_estatus()
        {
            ddl_estatus.Items.Clear();
            ddl_estatus.Items.Add(new ListItem("Activo", "A"));
            ddl_estatus.Items.Add(new ListItem("Inactivo", "B"));

            ddl_tipo.Items.Clear();
            ddl_tipo.Items.Add(new ListItem("Teórico", "T"));
            ddl_tipo.Items.Add(new ListItem("Práctico", "P"));
            ddl_tipo.Items.Add(new ListItem("Mixto", "M"));
        }
        protected void grid_tsalo_bind()
        {

            try
            {
                Gridtsalo.DataSource = servicePlanAcad.ObtenerEscenariosAcademicos();
                Gridtsalo.DataBind();
                Gridtsalo.UseAccessibleHeader = true;
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tsalo", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            txt_tsalo.Text = null;
            txt_nombre.Text = null;
            txt_minimo.Text = null;
            txt_maximo.Text = null;
            combo_estatus();
            btn_save.Visible = true;
            btn_update.Visible = false;
            txt_tsalo.Attributes.Remove("readonly");
            grid_tsalo_bind();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            ModelInsertarEscenariosAcademicosResponse objExiste = new ModelInsertarEscenariosAcademicosResponse();
            try
            {
                objExiste = servicePlanAcad.InsertarDisponibilidad(txt_tsalo.Text, txt_nombre.Text, txt_minimo.Text, txt_maximo.Text,
                ddl_tipo.SelectedValue, Session["usuario"].ToString(), ddl_estatus.SelectedValue);
                if (objExiste.Existe == "0")
                {


                    txt_tsalo.Text = null;
                    txt_nombre.Text = null;
                    txt_minimo.Text = null;
                    txt_maximo.Text = null;
                    combo_estatus();
                    grid_tsalo_bind();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);


                }
                else
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validarClave('ContentPlaceHolder1_txt_tsalo',1);", true);
                    grid_tsalo_bind();
                }
            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tsalo", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
            }

        }

        protected void btn_update_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txt_tsalo.Text) && !String.IsNullOrEmpty(txt_nombre.Text))
            {
                string strCadSQL = "UPDATE tsalo SET tsalo_desc='" + txt_nombre.Text + "', tsalo_estatus='" + ddl_estatus.SelectedValue + "', " +
                    " tsalo_minimo='" + txt_minimo.Text + "', tsalo_maximo='" + txt_maximo.Text + "', tsalo_tipo='" + ddl_tipo.SelectedValue + "', " +
                    " tsalo_tuser_clave='" + Session["usuario"].ToString() + "', tsalo_date=CURRENT_TIMESTAMP() WHERE tsalo_clave='" + txt_tsalo.Text + "'";
                MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                conexion.Open();
                MySqlCommand mysqlcmd = new MySqlCommand(strCadSQL, conexion);
                mysqlcmd.CommandType = CommandType.Text;
                try
                {
                    mysqlcmd.ExecuteNonQuery();
                    grid_tsalo_bind();
                    txt_tsalo.Text = null;
                    txt_nombre.Text = null;
                    txt_minimo.Text = null;
                    txt_maximo.Text = null;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "update_p", "update();", true);
                }
                catch (Exception ex)
                {
                    string test = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tsalo", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                }
                finally
                {
                    conexion.Close();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_tsalo();", true);
            }
        }



        protected void Gridtsalo_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = Gridtsalo.SelectedRow;
            txt_tsalo.Text = row.Cells[1].Text;
            txt_nombre.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);
            txt_minimo.Text = HttpUtility.HtmlDecode(row.Cells[3].Text);
            txt_maximo.Text = HttpUtility.HtmlDecode(row.Cells[4].Text);
            combo_estatus();
            ddl_tipo.SelectedValue = row.Cells[5].Text;
            ddl_estatus.SelectedValue = row.Cells[7].Text;
            btn_update.Visible = true;
            btn_save.Visible = false;
            txt_tsalo.Attributes.Add("readonly", "");
            grid_tsalo_bind();
        }
    }
}