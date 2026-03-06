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
    public partial class ttiba : System.Web.UI.Page
    {
        #region <Variables>
        //Utilidades utils = new Utilidades();
        AlumnoService serviceAlumno = new AlumnoService();
        CobranzaService serviceCobranza = new CobranzaService();
        ContactoService serviceContacto = new ContactoService();
        List<ModelObtenPaisesResponse> lstPaises = new List<ModelObtenPaisesResponse>();
        Catalogos serviceCatalogo = new Catalogos();
        public string id_num = null;
        MenuService servicePermiso = new MenuService();
        DocumentoService serviceDocumento = new DocumentoService();
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
                objPermiso = servicePermiso.ObtenerPermisoFormulario(Session["usuario"].ToString(), "ttiba");
                if (objPermiso != null)
                {
                    if (objPermiso.usme_update == "0" || objPermiso.usme_select == "0")
                    {
                        btn_ttiba.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                    }
                    else
                        grid_ttiba_bind();
                }
                else
                {
                    btn_ttiba.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                }


            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "ttiba", Session["usuario"].ToString());
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
        }
        protected void grid_ttiba_bind()
        {
            //string strQueryGrid = "";
            //strQueryGrid = " select ttiba_clave clave, ttiba_desc nombre,  " +
            //  " ttiba_estatus c_estatus,CASE WHEN ttiba_estatus = 'A' THEN 'ACTIVO' ELSE 'INACTIVO' END Estatus, fecha(date_format(ttiba_date,'%Y-%m-%d')) fecha " +
            //  " from ttiba order by clave ";
            try
            {
                Gridttiba.DataSource = serviceCatalogo.ObtenerTipoBajas();
                Gridttiba.DataBind();
                Gridttiba.DataMember = "Ttiba";
                Gridttiba.HeaderRow.TableSection = TableRowSection.TableHeader;
                Gridttiba.UseAccessibleHeader = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "ttiba", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            txt_ttiba.Text = null;
            txt_nombre.Text = null;
            combo_estatus();
            btn_save.Visible = true;
            btn_update.Visible = false;
            txt_ttiba.ReadOnly = false;
            grid_ttiba_bind();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            ModelInsertarTipoBajaResponse objExiste = new ModelInsertarTipoBajaResponse();

            if (!String.IsNullOrEmpty(txt_ttiba.Text) && !String.IsNullOrEmpty(txt_nombre.Text))
            {
                //string strCadSQL = "INSERT INTO ttiba Values ('" + txt_ttiba.Text + "','" + txt_nombre.Text + "','" +
                //Session["usuario"].ToString() + "',current_timestamp(),'" + ddl_estatus.SelectedValue + "')";
               
                try
                {
                    objExiste = serviceCatalogo.InsertarTipoBaja(txt_ttiba.Text, txt_nombre.Text, Session["usuario"].ToString(), ddl_estatus.SelectedValue);
                    if (objExiste != null)
                    {
                        if (objExiste.Existe == "0")
                        {
                            txt_ttiba.Text = null;
                            txt_nombre.Text = null;
                            ddl_estatus.SelectedIndex = 0;
                            grid_ttiba_bind();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validarClave('ContentPlaceHolder1_txt_ttiba',1);", true);
                            grid_ttiba_bind();
                        }
                    }
                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);

                }
                catch (Exception ex)
                {
                    string test = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "ttiba", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_ttiba();", true);
                grid_ttiba_bind();
            }
        }

        protected void btn_update_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txt_ttiba.Text) && !String.IsNullOrEmpty(txt_nombre.Text))
            {
                //string strCadSQL = "UPDATE ttiba SET ttiba_desc='" + txt_nombre.Text + "', ttiba_estatus='" +
                //ddl_estatus.SelectedValue + "', ttiba_user='" + Session["usuario"].ToString() + "',
                //ttiba_date=CURRENT_TIMESTAMP() WHERE ttiba_clave='" + txt_ttiba.Text + "'";
              
                try
                {
                    serviceCatalogo.EditarTipoBaja(txt_ttiba.Text, txt_nombre.Text, Session["usuario"].ToString(), ddl_estatus.SelectedValue);
                    grid_ttiba_bind();
                    Gridttiba.SelectedIndex = -1;
                    btn_save.Visible = true;
                    btn_update.Visible = false;
                    txt_ttiba.ReadOnly = false;
                    txt_nombre.Text = string.Empty;
                    ddl_estatus.SelectedIndex = 0;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "update_p", "update();", true);
                }
                catch (Exception ex)
                {
                    string test = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "ttiba", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                }
               
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_ttiba();", true);
            }
        }

        protected void Gridttiba_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = Gridttiba.SelectedRow;
            txt_ttiba.Text = row.Cells[1].Text;
            txt_nombre.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);
            combo_estatus();
            ddl_estatus.SelectedValue = row.Cells[3].Text;
            btn_update.Visible = true;
            btn_save.Visible = false;
            txt_ttiba.ReadOnly = true;
            grid_ttiba_bind();
        }
    }
}