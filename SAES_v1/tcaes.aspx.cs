using MySql.Data.MySqlClient;
using SAES_Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using static SAES_DBO.Models.ModelCalendarioEscolar;
using static SAES_DBO.Models.ModelMenu;

namespace SAES_v1
{
    public partial class tcaes : System.Web.UI.Page
    {
        public string labels_dashboard_1;
        public string data_dashboard_1;
        public string label_dashboard_1;
        Catalogos serviceCatalogo = new Catalogos();
        CalendarioEscolarService serviceCalendario = new CalendarioEscolarService();
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

                ScriptManager.RegisterStartupScript(this, this.GetType(), "ctrl_fecha_i", "ctrl_fecha_i();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ctrl_fecha_f", "ctrl_fecha_f();", true);

                if (!IsPostBack)
                {
                    LlenaPagina();
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
                objPermiso = servicePermiso.ObtenerPermisoFormulario(Session["usuario"].ToString(), "tcaes");
                if (objPermiso != null)
                {
                    if (objPermiso.usme_update == "0" || objPermiso.usme_select == "0")
                    {
                        btn_tcaes.Visible = false;
                        ddl_estatus.Visible = false;
                        txt_fecha_i.Visible = false;
                        txt_fecha_f.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                    }
                    else
                    {
                        btn_tcaes.Visible = true;
                        grid_tcaes_bind();
                    }

                    combo_periodo(ddl_periodo, ddl_campus, ddl_nivel);
                    combo_campus(ddl_campus);
                    //else
                    //    grid_bind_pais();
                }
                else
                {
                    btn_tcaes.Visible = false;
                    ddl_estatus.Visible = false;
                    txt_fecha_i.Visible = false;
                    txt_fecha_f.Visible = false;
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

        protected void combo_periodo(DropDownList ddl_periodo, DropDownList ddl_campus, DropDownList ddl_nivel)
        {
            ddl_periodo.Items.Clear();
            ddl_campus.Items.Clear();
            ddl_campus.Items.Add(new ListItem("-----", "0"));
            ddl_nivel.Items.Clear();
            ddl_nivel.Items.Add(new ListItem("-----", "0"));

            try
            {
                ddl_periodo.DataSource = serviceCatalogo.ObtenerPeriodosEscolares();
                ddl_periodo.DataValueField = "Clave";
                ddl_periodo.DataTextField = "Nombre";
                ddl_periodo.DataBind();

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcaes", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void combo_campus(DropDownList ddl_campus)
        {
            try
            {
                ddl_campus.DataSource = serviceCatalogo.ObtenerCampus();
                ddl_campus.DataValueField = "Clave";
                ddl_campus.DataTextField = "Descripcion";
                ddl_campus.DataBind();
            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcaes", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void combo_tcaes()
        {
            try
            {

                ddl_tcaes.DataSource = serviceCalendario.ObtenerTcoca();
                ddl_tcaes.DataValueField = "Clave";
                ddl_tcaes.DataTextField = "Nombre";
                ddl_tcaes.DataBind();

                ddl_estatus.Items.Clear();
                ddl_estatus.Items.Add(new ListItem("Activo", "A"));
                ddl_estatus.Items.Add(new ListItem("Inactivo", "B"));

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcaes", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void combo_nivel(string campus, DropDownList ddl_nivel)
        {
            try
            {
                ddl_nivel.DataSource = serviceCatalogo.obtenNivel(ddl_campus.SelectedValue);
                ddl_nivel.DataValueField = "Clave";
                ddl_nivel.DataTextField = "Descripcion";
                ddl_nivel.DataBind();
            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcaes", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }


        protected void ddl_periodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            grid_tcaes_bind();
            //Gridtcaes.Visible = false;
        }

        protected void ddl_campus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_campus.SelectedValue != "0")
            {
                combo_nivel(ddl_campus.SelectedValue, ddl_nivel);
            }
            //Gridtcaes.Visible = false;
        }

        protected void grid_tcaes_bind()
        {
            Gridtcaes.DataSource = null;
            Gridtcaes.DataBind();
            try
            {

                Gridtcaes.DataSource = serviceCalendario.ObtenerCalendario(ddl_periodo.SelectedValue, ddl_campus.SelectedValue.ToString(), ddl_nivel.SelectedValue.ToString());
                Gridtcaes.DataBind();
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcaes", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            };
        }

        protected void ddl_nivel_SelectedIndexChanged(object sender, EventArgs e)
        {
            grid_tcaes_bind();
            combo_tcaes();
        }


        protected void GridTcaes_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = Gridtcaes.SelectedRow;

            combo_tcaes();
            grid_tcaes_bind();
            ddl_estatus.SelectedValue = row.Cells[5].Text;
            ddl_tcaes.SelectedValue = row.Cells[1].Text;
            txt_fecha_i.Text = row.Cells[3].Text;
            txt_fecha_f.Text = row.Cells[4].Text;

            btn_update.Visible = true;
            btn_save.Visible = false;
            btn_cancel.Visible = true;
            ddl_periodo.Enabled = false;



        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {

            ddl_tcaes.Items.Clear();
            txt_fecha_i.Text = ""; txt_fecha_f.Text = "";
            combo_tcaes();
            btn_save.Visible = true;
            btn_update.Visible = false;
            btn_cancel.Visible = false;
            Gridtcaes.SelectedIndex = -1;
            ddl_periodo.Enabled = true;

            //Gridtcaes.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            if (ddl_periodo.SelectedValue == "0" || ddl_campus.SelectedValue == "0" || ddl_nivel.SelectedValue == "0" || ddl_tcaes.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_tcaes();", true);

            }
            else
            {
                //mysqlcmd.CommandType = CommandType.Text;
                try
                {
                    serviceCalendario.InsertarTcaes(ddl_periodo.SelectedValue, ddl_campus.SelectedValue.ToString(), ddl_nivel.SelectedValue.ToString(), ddl_tcaes.SelectedValue,
                        txt_fecha_i.Text, txt_fecha_f.Text, Session["usuario"].ToString(), ddl_estatus.SelectedValue);

                    grid_tcaes_bind();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                    txt_fecha_i.Text = ""; txt_fecha_f.Text = "";
                    combo_tcaes();
                }
                catch (Exception ex)
                {
                    string test = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tcaes", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                }
            }

        }

        protected void btn_update_Click(object sender, EventArgs e)
        {

            if (ddl_periodo.SelectedValue == "0" || ddl_campus.SelectedValue == "0" || ddl_nivel.SelectedValue == "0" || ddl_tcaes.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_tcaes();", true);
            }
            else
            {
                string strCadSQL = "UPDATE tcaes SET tcaes_inicio=STR_TO_DATE('" + txt_fecha_i.Text + "','%d/%m/%Y'), " +
                    "  tcaes_fin=STR_TO_DATE('" + txt_fecha_f.Text + "','%d/%m/%Y'), tcaes_estatus='" + ddl_estatus.SelectedValue + "', tcaes_tuser_clave='" + Session["usuario"].ToString() + "', tcaes_date=CURRENT_TIMESTAMP() " +
                    " WHERE tcaes_tpees_clave='" + ddl_periodo.SelectedValue + "'" +
                    " and   tcaes_tcamp_clave='" + ddl_campus.SelectedValue + "'" +
                    " and   tcaes_tnive_clave='" + ddl_nivel.SelectedValue + "'" +
                    " and   tcaes_tcoca_clave='" + ddl_tcaes.SelectedValue + "'";
                MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                conexion.Open();
                MySqlCommand mysqlcmd = new MySqlCommand(strCadSQL, conexion);
                mysqlcmd.CommandType = CommandType.Text;
                try
                {
                    mysqlcmd.ExecuteNonQuery();
                    serviceCalendario.EditarTcaes(ddl_periodo.SelectedValue, ddl_campus.SelectedValue.ToString(), ddl_nivel.SelectedValue.ToString(), ddl_tcaes.SelectedValue,
                    txt_fecha_i.Text, txt_fecha_f.Text, Session["usuario"].ToString(), ddl_estatus.SelectedValue);

                    txt_fecha_i.Text = "";
                    txt_fecha_f.Text = "";
                    ddl_periodo.Enabled = true;
                    btn_cancel.Visible = false;
                    btn_update.Visible=false;
                    btn_save.Visible = true;
                    //ddl_estatus.SelectedIndex = 0;
                    //ddl_periodo.SelectedIndex = 0;
                    //ddl_campus.SelectedIndex = 0;
                    //ddl_nivel.SelectedIndex = 0;
                    //ddl_tcaes.SelectedIndex = 0;
                    grid_tcaes_bind();

                    Gridtcaes.SelectedIndex = -1;


                    ScriptManager.RegisterStartupScript(this, this.GetType(), "update_p", "update();", true);
                    //combo_tcaes();
                }
                catch (Exception ex)
                {
                    string test = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tcaes", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                }
                finally
                {
                    conexion.Close();
                }

            }
        }

    }
}