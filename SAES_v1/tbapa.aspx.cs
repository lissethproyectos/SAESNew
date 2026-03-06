using SAES_Services;
using SAES_v1.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using System.Collections;
using static SAES_DBO.Models.ModelMenu;

namespace SAES_v1
{
    public partial class tbapa : System.Web.UI.Page
    {
        Utilidades utils = new Utilidades();
        Catalogos catalogos = new Catalogos();
        Devoluciones devoluciones = new Devoluciones();
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
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ctrl_fecha_i", "ctrl_fecha_i();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ctrl_fecha_f", "ctrl_fecha_f();", true);
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
                        btn_save.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                    }
                    else
                    {
                        Global.opcion = 1;
                        btn_save.Visible = true;
                    }
                    CargaInicial();
                    Gridtbapa = utils.ClearGridView(Gridtbapa);

                }
                else
                {
                    btn_save.Visible = false;
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

        public void CargaInicial()
        {
            btn_update.Visible = false;
            //btn_save.Visible = true;

            ddl_campus.Items.Clear();
            ddl_nivel.Items.Clear();
            ddl_periodo.Items.Clear();

            ddl_periodo = utils.BeginDropdownList(ddl_periodo, catalogos.obtenPeriodo());

            ddl_campus = utils.BeginDropdownList(ddl_campus, catalogos.obtenCampus());            

            ddl_nivel = utils.BeginDropdownList(ddl_nivel, catalogos.obtenNivel(null));

            txt_porcentaje.Text = "0";
            txt_fecha_l.Text = string.Empty;
            txt_fecha_f.Text = string.Empty;
        }

        protected void ddl_campus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string campus = (ddl_campus.SelectedValue == "") ? null : ddl_campus.SelectedValue;
                ddl_nivel = utils.BeginDropdownList(ddl_nivel, catalogos.obtenNivel(campus));
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tbapa", Session["usuario"].ToString(), ex.StackTrace);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            try
            {
                CargaInicial();
                Gridtbapa = utils.ClearGridView(Gridtbapa);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcdoc", Session["usuario"].ToString(), ex.StackTrace);
            }
        }
        private bool validaDecimal(string valor)
        {
            if (!string.IsNullOrEmpty(valor))
            {
                decimal val = 0;
                bool result = decimal.TryParse(valor, out val);
                return result;
            }
            return false;
        }

        private bool validaFecha(string valor)
        {
            DateTime dt = DateTime.Now;
            var date = DateTime.TryParseExact(valor, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
            return date;
        }

        private bool validaTraslapa(string dti, string dtf)
        {
            string Querytraslape = "    select count(*) from TBAPA     where 1 = 1 " +
              " and TBAPA_TPEES_CLAVE = '" + ddl_periodo.SelectedValue + "'" +
              " and TBAPA_TCAMP_CLAVE = '" + ddl_campus.SelectedValue + "'" +
              " and TBAPA_TNIVE_CLAVE = '" + ddl_nivel.SelectedValue + "'" +
              " and((STR_TO_DATE('" + txt_fecha_l.Text + "', '%d/%m/%Y') between TBAPA_FECHA_INI and TBAPA_FECHA_FIN or STR_TO_DATE('" + txt_fecha_f.Text + "', '%d/%m/%Y') between TBAPA_FECHA_INI and TBAPA_FECHA_FIN) " +
              " or(STR_TO_DATE('" + txt_fecha_l.Text + "', ' %d/%m/%Y') <= TBAPA_FECHA_INI and STR_TO_DATE('" + txt_fecha_f.Text + "', ' %d/%m/%Y') >= TBAPA_FECHA_FIN) " +
              " or(STR_TO_DATE('" + txt_fecha_l.Text + "', '%d/%m/%Y') >= TBAPA_FECHA_INI and STR_TO_DATE('" + txt_fecha_f.Text + "', '%d/%m/%Y') <= TBAPA_FECHA_FIN) )";

            MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            conexion.Open();
            try
            {
                MySqlDataAdapter sqladapter = new MySqlDataAdapter();

                DataSet dssql1 = new DataSet();

                MySqlCommand commandsql1 = new MySqlCommand(Querytraslape, conexion);
                sqladapter.SelectCommand = commandsql1;
                sqladapter.Fill(dssql1);
                sqladapter.Dispose();
                commandsql1.Dispose();
                if (dssql1.Tables[0].Rows[0][0].ToString() == "0")
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tbapa", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                return false;
            }
            finally
            {
                conexion.Close();
            }
        }

        private bool validaTraslape1(string dti, string dtf, double consecutivo)
        {
            string Querytraslape = "    select count(*) from TBAPA     where 1 = 1 " +
              " and TBAPA_TPEES_CLAVE = '" + ddl_periodo.SelectedValue + "'" +
              " and TBAPA_TCAMP_CLAVE = '" + ddl_campus.SelectedValue + "'" +
              " and TBAPA_TNIVE_CLAVE = '" + ddl_nivel.SelectedValue + "'" +
              " and  TBAPA_CONSECUTIVO != " + consecutivo +
              " and((STR_TO_DATE('" + txt_fecha_l.Text + "', '%d/%m/%Y') between TBAPA_FECHA_INI and TBAPA_FECHA_FIN or STR_TO_DATE('" + txt_fecha_f.Text + "', '%d/%m/%Y') between TBAPA_FECHA_INI and TBAPA_FECHA_FIN) " +
              " or(STR_TO_DATE('" + txt_fecha_l.Text + "', ' %d/%m/%Y') <= TBAPA_FECHA_INI and STR_TO_DATE('" + txt_fecha_f.Text + "', ' %d/%m/%Y') >= TBAPA_FECHA_FIN) " +
              " or(STR_TO_DATE('" + txt_fecha_l.Text + "', '%d/%m/%Y') >= TBAPA_FECHA_INI and STR_TO_DATE('" + txt_fecha_f.Text + "', '%d/%m/%Y') <= TBAPA_FECHA_FIN) )";

            MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            conexion.Open();
            try
            {
                MySqlDataAdapter sqladapter = new MySqlDataAdapter();

                DataSet dssql1 = new DataSet();

                MySqlCommand commandsql1 = new MySqlCommand(Querytraslape, conexion);
                sqladapter.SelectCommand = commandsql1;
                sqladapter.Fill(dssql1);
                sqladapter.Dispose();
                commandsql1.Dispose();
                if (dssql1.Tables[0].Rows[0][0].ToString() == "0")
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tbapa", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                return false;
            }
            finally
            {
                conexion.Close();
            }
        }

        private bool validaRangoFechas(DateTime dti, DateTime dtf)
        {
            if (dtf < dti)
                return false;
            else
                return true;
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(ddl_periodo.SelectedValue) && validaDecimal(txt_porcentaje.Text) && validaFecha(txt_fecha_l.Text) && validaFecha(txt_fecha_f.Text))
                {
                    DateTime fechaI = DateTime.ParseExact(txt_fecha_l.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    DateTime fechaF = DateTime.ParseExact(txt_fecha_f.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    if (validaRangoFechas(fechaI, fechaF))
                    {
                        if (validaTraslapa(txt_fecha_l.Text, txt_fecha_f.Text))
                        {

             devoluciones.InsertParametroDevolucion(ddl_periodo.SelectedValue, ddl_campus.SelectedValue, ddl_nivel.SelectedValue, Convert.ToDecimal(txt_porcentaje.Text), txt_fecha_l.Text, txt_fecha_f.Text, Session["usuario"].ToString());
                            Gridtbapa = utils.BeginGrid(Gridtbapa, devoluciones.obtenParametrosDevolucion(ddl_periodo.SelectedValue, ddl_campus.SelectedValue, ddl_nivel.SelectedValue));
                            txt_porcentaje.Text = string.Empty;
                            txt_fecha_l.Text = string.Empty;
                            txt_fecha_f.Text = string.Empty;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "save", "save();", true);
                            //CargaInicial();
                            //Gridtbapa = utils.ClearGridView(Gridtbapa);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "error_traslape", "error_traslape();", true);
                        }
                    }
                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error_validacionFechas", "error_validacionFechas();", true);
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "validar_campos_tbapa_insert", "validar_campos_tbapa_insert();", true);

            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tbapa", Session["usuario"].ToString(), ex.StackTrace);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion('" + mensaje_error + "');", true);
            }
        }

        protected void btn_update_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = Gridtbapa.SelectedRow;
                if (!string.IsNullOrEmpty(ddl_periodo.SelectedValue) && validaDecimal(txt_porcentaje.Text) && validaFecha(txt_fecha_l.Text) && validaFecha(txt_fecha_f.Text))
                {
                    Decimal Porc_anterior = Convert.ToDecimal(row.Cells[2].Text);
                    DateTime fechaI_Anterior = DateTime.ParseExact(row.Cells[3].Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    DateTime fechaF_Anterior = DateTime.ParseExact(row.Cells[4].Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    DateTime fechaI = DateTime.ParseExact(txt_fecha_l.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    DateTime fechaF = DateTime.ParseExact(txt_fecha_f.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    if (validaRangoFechas(fechaI, fechaF))
                    {
                        if (validaTraslape1(txt_fecha_l.Text, txt_fecha_f.Text, Global.consecutivo))
                        {
                            try
                            {

                                devoluciones.UpdateParametroDevolucion(Convert.ToInt32(Global.consecutivo), Convert.ToDecimal(txt_porcentaje.Text), txt_fecha_l.Text, txt_fecha_f.Text, Session["usuario"].ToString());
                                Gridtbapa = utils.BeginGrid(Gridtbapa, devoluciones.obtenParametrosDevolucion(ddl_periodo.SelectedValue, ddl_campus.SelectedValue, ddl_nivel.SelectedValue));
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "update", "update();", true);
                                btn_update.Visible = false;
                                btn_save.Visible = true;
                                txt_porcentaje.Text = string.Empty;
                                txt_fecha_l.Text = string.Empty;
                                txt_fecha_f.Text = string.Empty;
                                Gridtbapa.SelectedIndex = -1;
                            }
                            catch (Exception ex)
                            {
                                string mensaje_error = ex.Message.Replace("'", "-");
                                Global.inserta_log(mensaje_error, "tbapa", Session["usuario"].ToString(), ex.StackTrace);

                                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion('" + ex.Message.Replace("'","") + "');", true);

                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "error_traslape", "error_traslape();", true);
                        }
                    }
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "validar_campos_tbapa_insert", "validar_campos_tbapa_insert();", true);
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tbapa", Session["usuario"].ToString(), ex.StackTrace);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion('" + ex.Message + "');", true);
            }
        }

   
        protected void Gridtbapa_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = Gridtbapa.SelectedRow;
                txt_porcentaje.Text = row.Cells[2].Text;
                txt_fecha_l.Text = row.Cells[3].Text;
                txt_fecha_f.Text = row.Cells[4].Text;
                Global.consecutivo = Convert.ToDouble(row.Cells[1].Text);
                if (Global.opcion == 1)
                {
                    btn_update.Visible = true;
                }

                btn_save.Visible = false;
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tbapa", Session["usuario"].ToString(), ex.StackTrace);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void ddl_nivel_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Gridtbapa.DataSource = null;
                string periodo = (ddl_periodo.SelectedValue == "") ? null : ddl_periodo.SelectedValue;
                string campus = (ddl_campus.SelectedValue == "") ? null : ddl_campus.SelectedValue;
                string nivel = (ddl_nivel.SelectedValue == "") ? null : ddl_nivel.SelectedValue;
                if (!string.IsNullOrEmpty(ddl_periodo.SelectedValue))
                {
                    double tr = 0;
                    tr = Gridtbapa.Rows.Count;
                    Gridtbapa.DataSource = "";
                    Gridtbapa = utils.BeginGrid(Gridtbapa, devoluciones.obtenParametrosDevolucion(periodo, campus, nivel));

                    //if (Gridtbapa.Rows.Count > 0)
                    //{
                    //    Gridtbapa.Visible = true;
                    //}
                    //else
                    //{
                    //    Gridtbapa.Visible = false;
                    //}
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_tbapa_consulta();", true);
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tbapa", Session["usuario"].ToString(), ex.StackTrace);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion('" + mensaje_error + "');", true);
                Gridtbapa = utils.ClearGridView(Gridtbapa);
            }
        }

        protected void ddl_periodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddl_campus.SelectedIndex = 0;
            ddl_nivel.SelectedIndex = 0;
            Gridtbapa.DataSource = null;
            Gridtbapa.DataBind();
        }
    }
}