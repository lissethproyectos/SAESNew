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


namespace SAES_v1
{
    public partial class tbapa2 : System.Web.UI.Page
    {
        Utilidades utils = new Utilidades();
        Catalogos catalogos = new Catalogos();
        Devoluciones devoluciones = new Devoluciones();
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
                    string QerySelect = "select tusme_update, tusme_select from tuser, tusme, tmede " +
                              " where tuser_clave = '" + Session["usuario"].ToString() + "'" +
                              " and tusme_trole_clave = tuser_trole_clave and tusme_tmenu_clave = 4 and tusme_tmenu_clave = tmede_tmenu_clave " +
                              " and tusme_tmede_clave = tmede_clave and tmede_forma='tbapa' ";

                    MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                    conexion.Open();
                    try
                    {
                        MySqlDataAdapter sqladapter = new MySqlDataAdapter();

                        DataSet dssql1 = new DataSet();

                        MySqlCommand commandsql1 = new MySqlCommand(QerySelect, conexion);
                        sqladapter.SelectCommand = commandsql1;
                        sqladapter.Fill(dssql1);
                        sqladapter.Dispose();
                        commandsql1.Dispose();
                        if (dssql1.Tables[0].Rows.Count == 0 || dssql1.Tables[0].Rows[0][1].ToString() == "0")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                        }
                        else
                        {
                            if (dssql1.Tables[0].Rows[0][0].ToString() == "1")
                            {
                                //btn_tcoca.Visible = true;
                                Global.opcion = 1;
                                btn_save.Visible = true;
                            }
                            else
                            {
                                btn_save.Visible = false;
                            }
                            CargaInicial();
                            Gridtbapa = utils.ClearGridView(Gridtbapa);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);
                        }

                    }
                    catch (Exception ex)
                    {
                        //resultado.Text = ex.Message;
                        string mensaje_error = ex.Message.Replace("'", "-");
                        Global.inserta_log(mensaje_error, "tbapa", Session["usuario"].ToString());
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                    }
                    finally
                    {
                        conexion.Close();
                    }
                    
                }
                    

                ScriptManager.RegisterStartupScript(this, this.GetType(), "ctrl_fecha_i", "ctrl_fecha_i();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ctrl_fecha_f", "ctrl_fecha_f();", true);
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
            //applyWeb.Data.Data objAreas = new applyWeb.Data.Data(System.Configuration.ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            //ArrayList arrParametros = new ArrayList();
            //arrParametros.Add(new applyWeb.Data.Parametro("@Nada", ""));
            //ddl_campus.DataSource = objAreas.ExecuteSP("p_obten_campus", arrParametros);

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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion('"+ex.Message+"');", true);
            }
        }

        protected void btn_update_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = Gridtbapa.SelectedRow;
                if (!string.IsNullOrEmpty(ddl_periodo.SelectedValue) && validaDecimal(txt_porcentaje.Text) && validaFecha(txt_fecha_l.Text) && validaFecha(txt_fecha_f.Text))
                {
                    Decimal Porc_anterior = Convert.ToDecimal(row.Cells[1].Text);
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
                                //devoluciones.UpdateParametroDevolucion(Global.consecutivo, Convert.ToDecimal(txt_porcentaje.Text), txt_fecha_l.Text, txt_fecha_f.Text, Session["usuario"].ToString());
                                applyWeb.Data.Data objAreas = new applyWeb.Data.Data(System.Configuration.ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                                
                                ArrayList arrParametros = new ArrayList();
                                arrParametros.Add(new applyWeb.Data.Parametro("@Consecutivo", Global.consecutivo));
                                arrParametros.Add(new applyWeb.Data.Parametro("@Porc", Convert.ToDecimal(txt_porcentaje.Text)));
                                arrParametros.Add(new applyWeb.Data.Parametro("@FechaIni", txt_fecha_l.Text));
                                arrParametros.Add(new applyWeb.Data.Parametro("@FechaFin", txt_fecha_f.Text));
                                arrParametros.Add(new applyWeb.Data.Parametro("@Usuario", Session["usuario"].ToString()));
                                //DataSet dsAlumnos = objAdministracion.ExecuteSP("p_update_tbapa", arrParametros);
                                objAreas.ExecuteInsertSP("p_update_tbapa", arrParametros);
                                Gridtbapa = utils.BeginGrid(Gridtbapa, devoluciones.obtenParametrosDevolucion(ddl_periodo.SelectedValue, ddl_campus.SelectedValue, ddl_nivel.SelectedValue));
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "update", "update();", true);
                                //CargaInicial();
                                //Gridtbapa = utils.ClearGridView(Gridtbapa);
                            }
                            catch (Exception ex)
                            {
                                string mensaje_error = ex.Message.Replace("'", "-");
                                Global.inserta_log(mensaje_error, "tbapa", Session["usuario"].ToString(), ex.StackTrace);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta('" + ex.Message + "');", true);
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
        
        protected void btn_search_Click(object sender, EventArgs e)
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
              
                    if (Gridtbapa.Rows.Count > 0)
                    {
                        Gridtbapa.Visible = true;
                    }
                    else
                    {
                        Gridtbapa.Visible = false;
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_tbapa_consulta();", true);
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tbapa", Session["usuario"].ToString(), ex.StackTrace);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion('"+ mensaje_error + "');", true);
                Gridtbapa = utils.ClearGridView(Gridtbapa);
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
    }
}