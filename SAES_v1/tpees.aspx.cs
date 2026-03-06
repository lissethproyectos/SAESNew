using MySql.Data.MySqlClient;
using SAES_Services;
using SAES_v1.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Management;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using static SAES_DBO.Models.ModelMenu;

namespace SAES_v1
{
    public partial class tpees : System.Web.UI.Page
    {
        #region <Variables>
        Utilidades utils = new Utilidades();
        Catalogos serviceCatalogo = new Catalogos();
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
                
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ctrl_fecha_i", "ctrl_fecha_i();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ctrl_fecha_f", "ctrl_fecha_f();", true);

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
                objPermiso = servicePermiso.ObtenerPermisoFormulario(Session["usuario"].ToString(), "tpees");
                if (objPermiso != null)
                {
                    if (objPermiso.usme_update == "0" || objPermiso.usme_select == "0")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                    else
                    {
                        if (objPermiso.usme_update == "0")
                            btn_periodo.Visible = false;

                        grid_periodo_bind();
                    }
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);


            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tpees", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);

            }
        }

        protected void combo_estatus()
        {
            ddl_estatus.Items.Clear();
            ddl_estatus.Items.Add(new ListItem("Activo", "A"));
            ddl_estatus.Items.Add(new ListItem("Inactivo", "B"));
        }
        protected void grid_periodo_bind()
        {
            try
            {
                GridPeriodo.DataSource = null;
                GridPeriodo.DataBind();
                DataTable dt = serviceCatalogo.ObtenerPeriodosEscolares();
                GridPeriodo = utils.BeginGrid(GridPeriodo, dt);
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tpees", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);

            }
          
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            txt_periodo.Text = null;
            txt_nombre.Text = null;
            txt_oficial.Text = null;
            txt_fecha_i.Text = null;
            txt_fecha_f.Text = null;
            combo_estatus();
            btn_save.Visible = true;
            btn_update.Visible = false;
            btn_cancel.Visible = false;
            txt_periodo.ReadOnly=false;
            //txt_periodo.Attributes.Remove("readonly");
            grid_periodo_bind();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            

            if (!String.IsNullOrEmpty(txt_periodo.Text) && !String.IsNullOrEmpty(txt_nombre.Text) && !String.IsNullOrEmpty(txt_fecha_i.Text) && !String.IsNullOrEmpty(txt_fecha_f.Text))
            {
                if (valida_periodo(txt_periodo.Text))
                {
                    string fecha_i_string = txt_fecha_i.Text;
                    string fecha_f_string = txt_fecha_f.Text;
                    string format = "dd/MM/yyyy";

                    DateTime fecha_inicio = DateTime.ParseExact(fecha_i_string, format, CultureInfo.InvariantCulture);
                    DateTime fecha_fin = DateTime.ParseExact(fecha_f_string, format, CultureInfo.InvariantCulture);

                    if (fecha_inicio < fecha_fin)
                    {
                        try
                        {
                            serviceCatalogo.InsertarPeriodoEscolar(Convert.ToInt32(txt_periodo.Text), txt_nombre.Text, txt_oficial.Text, string.Format(txt_fecha_i.Text, "dd/MM/yyyy"),
                                string.Format(txt_fecha_f.Text, "dd/MM/yyyy"), Session["usuario"].ToString(), ddl_estatus.SelectedValue);

                            txt_periodo.Text = null;
                            txt_nombre.Text = null;
                            txt_oficial.Text = null;
                            txt_fecha_i.Text = null;
                            txt_fecha_f.Text = null;
                            combo_estatus();
                            grid_periodo_bind();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                        }
                        catch (Exception ex)
                        {
                            string test = ex.Message;
                            string mensaje_error = ex.Message.Replace("'", "-");
                            Global.inserta_log(mensaje_error, "tpees", Session["usuario"].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
                        }
                    }
                    else
                    {
                        grid_periodo_bind();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validarFechas('ContentPlaceHolder1_txt_fecha_i','ContentPlaceHolder1_txt_fecha_f');", true);
                    }
                    
                }
                else
                {
                    grid_periodo_bind();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validarClavePeriodo('ContentPlaceHolder1_txt_periodo',1);", true);
                }
            }
            else
            {
                grid_periodo_bind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_periodo();", true);
            }


        }

        protected void btn_update_Click(object sender, EventArgs e)
        {

            

            if (!String.IsNullOrEmpty(txt_periodo.Text) && !String.IsNullOrEmpty(txt_nombre.Text) && !String.IsNullOrEmpty(txt_fecha_i.Text) && !String.IsNullOrEmpty(txt_fecha_f.Text))
            {
                string fecha_i_string = txt_fecha_i.Text;
                string fecha_f_string = txt_fecha_f.Text;
                string format = "dd/MM/yyyy";

                DateTime fecha_inicio = DateTime.ParseExact(fecha_i_string, format, CultureInfo.InvariantCulture);
                DateTime fecha_fin = DateTime.ParseExact(fecha_f_string, format, CultureInfo.InvariantCulture);

                if (fecha_inicio < fecha_fin)
                {
                    
                    try
                    {
                        //mysqlcmd.ExecuteNonQuery();
                        serviceCatalogo.EditarPeriodoEscolar(Convert.ToInt32(txt_periodo.Text), txt_nombre.Text, txt_oficial.Text, string.Format(txt_fecha_i.Text, "dd/MM/yyyy"),
                            string.Format(txt_fecha_f.Text, "dd/MM/yyyy"), Session["usuario"].ToString(), ddl_estatus.SelectedValue);
                        grid_periodo_bind();
                        GridPeriodo.SelectedIndex = -1;
                        btn_update.Visible = false;
                        btn_save.Visible = true;
                        //txt_periodo.Enabled= true;
                        txt_periodo.Text = null;
                        txt_nombre.Text = null;
                        txt_oficial.Text = null;
                        txt_fecha_i.Text = null;
                        txt_fecha_f.Text = null;

                        txt_periodo.ReadOnly = false;
                        btn_update.Visible = false;
                        btn_cancel.Visible = false;

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "update_p", "update();", true);
                    }
                    catch (Exception ex)
                    {
                        string test = ex.Message;
                        string mensaje_error = ex.Message.Replace("'", "-");
                        Global.inserta_log(mensaje_error, "tpees", Session["usuario"].ToString());
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                    }
                }
                else
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validarFechas('','ContentPlaceHolder1_txt_fecha_f');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "validarFechas", "validarFechas();", true);

                    //grid_periodo_bind();

                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validarFechas('ContentPlaceHolder1_txt_fecha_i','ContentPlaceHolder1_txt_fecha_f');", true);
                }
            }
            else
            {
                
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_periodo();", true);
            }
        }

        protected bool valida_periodo(string periodo)
        {
            string Query = "";
            //Query = "SELECT COUNT(*) Indicador FROM tpees WHERE tpees_clave='" + periodo + "'";
            //MySqlCommand cmd = new MySqlCommand(Query);
            //DataTable dt = GetData(cmd);
            //if (dt.Rows[0]["Indicador"].ToString() != "0")
            //{
            //    return false;
            //}
            //else
            //{
            //    return true;
            //}
            return true;

        }

        protected void GridPeriodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridPeriodo.SelectedRow;
            txt_periodo.Text = row.Cells[1].Text;
            txt_nombre.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);
            txt_oficial.Text = HttpUtility.HtmlDecode(row.Cells[3].Text);
            combo_estatus();
            ddl_estatus.SelectedValue = row.Cells[6].Text;
            txt_fecha_i.Text = row.Cells[4].Text;
            txt_fecha_f.Text = row.Cells[5].Text;
            btn_update.Visible = true;
            btn_save.Visible = false;
            btn_cancel.Visible = true;
            txt_periodo.ReadOnly = false;//.Attributes.Add("readonly", "");
            grid_periodo_bind();
        }
    }
}