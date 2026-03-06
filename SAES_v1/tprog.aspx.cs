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
    public partial class tprog : System.Web.UI.Page
    {
        #region <Variables>
        Utilidades utils = new Utilidades();
        Catalogos serviceCatalogo = new Catalogos();
        DocenteService serviceDocente = new DocenteService();
        PlanAcademicoService servicePlanAcademico = new PlanAcademicoService();
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

                //c_campus.Attributes.Add("onblur", "validarclaveCampus('ContentPlaceHolder1_c_campus',0)");
                //c_campus.Attributes.Add("oninput", "validarclaveCampus('ContentPlaceHolder1_c_campus',0)");
                //n_campus.Attributes.Add("onblur", "validarNombreCampus('ContentPlaceHolder1_n_campus')");
                //n_campus.Attributes.Add("oninput", "validarNombreCampus('ContentPlaceHolder1_n_campus')");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ctrl_fecha_rvoe", "ctrl_fecha_rvoe();", true);
                if (!IsPostBack)
                {
                    LlenaPagina();
                    combo_estatus();
                    combo_nivel();
                    combo_colegio();
                    combo_moda();
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
                objPermiso = servicePermiso.ObtenerPermisoFormulario(Session["usuario"].ToString(), "tprog");
                if (objPermiso != null)
                {
                    if (objPermiso.usme_update == "0" || objPermiso.usme_select == "0")
                    {
                        btn_tprog.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                    }
                    else
                    {
                        combo_nivel();
                        combo_colegio();
                        combo_moda();
                        grid_tprog_bind();
                    }
                }
                else
                {
                    btn_tprog.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                }


            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tprog", Session["usuario"].ToString());
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

        protected void combo_nivel()
        {
            ddl_tnive.Items.Clear();
            string Query = "SELECT DISTINCT tnive_clave Clave, tnive_desc Nivel FROM tnive " +
                            "UNION " +
                            "SELECT DISTINCT '0' Clave,'--------' Nivel  " +
                            "ORDER BY 1";
          
            try
            {               
                ddl_tnive.DataSource = serviceCatalogo.obtenTNive();
                ddl_tnive.DataValueField = "clave";
                ddl_tnive.DataTextField = "nombre";
                ddl_tnive.DataBind();

            }
            catch (Exception ex)
            {
                string test = ex.Message;
            }
        }

        protected void combo_colegio()
        {       
            try
            {
               
                ddl_tcole.DataSource = serviceCatalogo.obtenTCole();
                ddl_tcole.DataValueField = "clave";
                ddl_tcole.DataTextField = "nombre";
                ddl_tcole.DataBind();

            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tprog", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void combo_moda()
        {
            try
            {                
                ddl_tmoda.DataSource = serviceCatalogo.obtenTModa();
                ddl_tmoda.DataValueField = "clave";
                ddl_tmoda.DataTextField = "nombre";
                ddl_tmoda.DataBind();
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tprog", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void combo_estatus()
        {
            ddl_estatus.Items.Clear();
            ddl_estatus.Items.Add(new ListItem("Activo", "A"));
            ddl_estatus.Items.Add(new ListItem("Inactivo", "B"));
        }
        protected void grid_tprog_bind()
        {
            string strQueryGrid = "";
            //strQueryGrid = " select tprog_clave clave, tprog_desc nombre,  " +
            //  " tprog_estatus c_estatus,CASE WHEN tprog_estatus = 'A' THEN 'ACTIVO' ELSE 'INACTIVO' END Estatus, fecha(date_format(tprog_date,'%Y-%m-%d')) fecha, " +
            //  " tprog_tnive_clave Nivel, tprog_tcole_clave Colegio, tprog_tmoda_clave Modalidad, " +
            //  " tprog_creditos creditos, tprog_cursos cursos, tprog_periodos periodos, " +
            //  " tprog_incorporante incorporante, tprog_rvoe rvoe, date_format(tprog_fecha_rvoe,'%d/%m/%Y') fecha_rvoe " +
            //  " from tprog order by clave ";

            try
            {                
                Gridtprog.DataSource = serviceCatalogo.obtenPrograma();
                Gridtprog.DataBind();
                Gridtprog.DataMember = "Tprog";
                Gridtprog.HeaderRow.TableSection = TableRowSection.TableHeader;
                Gridtprog.UseAccessibleHeader = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tprog", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            txt_tprog.Text = null;
            txt_nombre.Text = null;
            combo_nivel();
            combo_colegio();
            combo_moda();
            combo_estatus();
            txt_creditos.Text = null;
            txt_cursos.Text = null;
            txt_periodos.Text = null;
            txt_clave_inc.Text = null;
            txt_rvoe.Text = null;
            txt_fecha_rvoe.Text = null;
            btn_save.Visible = true;
            btn_update.Visible = false;
            txt_tprog.Attributes.Remove("readonly");
            grid_tprog_bind();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txt_tprog.Text) && !String.IsNullOrEmpty(txt_nombre.Text) && !String.IsNullOrEmpty(txt_creditos.Text) && !String.IsNullOrEmpty(txt_cursos.Text)
                 && !String.IsNullOrEmpty(txt_periodos.Text) && !String.IsNullOrEmpty(txt_clave_inc.Text) && !String.IsNullOrEmpty(txt_rvoe.Text)
                 && !String.IsNullOrEmpty(txt_fecha_rvoe.Text) && ddl_tnive.SelectedValue != "0" && ddl_tcole.SelectedValue != "0" && ddl_tmoda.SelectedValue != "0")
            {
                if (valida_tprog(txt_tprog.Text))
                {
                    double vale = 0;
                    decimal resultado1 = 0;
                    decimal resultado2 = 0;
                    decimal resultado3 = 0;
                    bool cred = Decimal.TryParse(txt_creditos.Text, out resultado1);
                    bool curso = Decimal.TryParse(txt_cursos.Text, out resultado2);
                    bool periodo = Decimal.TryParse(txt_periodos.Text, out resultado3);
                    if (cred && curso && periodo)
                    {
                        vale = 1;
                        // Si llega hasta aquí, resultado es numérico.
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                        if (cred)
                        {
                            //
                        }
                        else
                        {

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "valida_creditos();", true);
                            grid_tprog_bind();
                        }
                        if (curso)
                        {
                            //
                        }
                        else
                        {
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "valida_cursos();", true);
                            grid_tprog_bind();
                        }
                        if (periodo)
                        {
                            //
                        }
                        else
                        {
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "valida_periodos();", true);
                            grid_tprog_bind();
                        }

                    }
                    if (vale == 1)
                    {
                        servicePlanAcademico.InsertarPrograma(txt_tprog.Text, txt_nombre.Text,ddl_tnive.SelectedValue, ddl_tcole.SelectedValue, 
                            ddl_tmoda.SelectedValue, txt_creditos.Text, txt_cursos.Text,
                              txt_periodos.Text, txt_clave_inc.Text, txt_rvoe.Text, string.Format(txt_fecha_rvoe.Text, "dd/MM/yyyy"),
                              "", "", "", "", "", ddl_estatus.SelectedValue, Session["usuario"].ToString()
                              );
                        try
                        {
                            txt_tprog.Text = null;
                            txt_nombre.Text = null;
                            combo_nivel();
                            combo_colegio();
                            combo_moda();
                            combo_estatus();
                            txt_creditos.Text = null;
                            txt_cursos.Text = null;
                            txt_periodos.Text = null;
                            txt_clave_inc.Text = null;
                            txt_rvoe.Text = null;
                            txt_fecha_rvoe.Text = null;
                            combo_estatus();
                            grid_tprog_bind();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                        }
                        catch (Exception ex)
                        {
                            string test = ex.Message;
                            string mensaje_error = ex.Message.Replace("'", "-");
                            Global.inserta_log(mensaje_error, "tprog", Session["usuario"].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                        }
                    }
                }
                else
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validarClave('ContentPlaceHolder1_txt_tprog',1);", true);
                    grid_tprog_bind();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_tprog();", true);
                grid_tprog_bind();
            }


        }

        protected void btn_update_Click(object sender, EventArgs e)
        {
            int vale = 0;
            if (!String.IsNullOrEmpty(txt_tprog.Text) && !String.IsNullOrEmpty(txt_nombre.Text) && !String.IsNullOrEmpty(txt_creditos.Text) && !String.IsNullOrEmpty(txt_cursos.Text)
                 && !String.IsNullOrEmpty(txt_periodos.Text) && !String.IsNullOrEmpty(txt_clave_inc.Text) && !String.IsNullOrEmpty(txt_rvoe.Text)
                 && !String.IsNullOrEmpty(txt_fecha_rvoe.Text) && ddl_tnive.SelectedValue != "0" && ddl_tcole.SelectedValue != "0" && ddl_tmoda.SelectedValue != "0")
            {
                decimal resultado1 = 0;
                decimal resultado2 = 0;
                decimal resultado3 = 0;
                bool cred = Decimal.TryParse(txt_creditos.Text, out resultado1);
                bool curso = Decimal.TryParse(txt_cursos.Text, out resultado2);
                bool periodo = Decimal.TryParse(txt_periodos.Text, out resultado3);

                if (cred && curso && periodo)
                {
                    vale = 1;
                    // Si llega hasta aquí, resultado es numérico.
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    if (cred)
                    {
                        //
                    }
                    else
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "valida_creditos();", true);
                        grid_tprog_bind();
                    }
                    if (curso)
                    {
                        //
                    }
                    else
                    {
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "valida_cursos();", true);
                        grid_tprog_bind();
                    }
                    if (periodo)
                    {
                        //
                    }
                    else
                    {
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "valida_periodos();", true);
                        grid_tprog_bind();
                    }

                }
            }
            else
            {
                grid_tprog_bind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_tprog();", true);
            }
            if (vale == 1)
            {
                string strCadSQL = "UPDATE tprog SET tprog_desc='" + txt_nombre.Text + "', tprog_estatus='" + ddl_estatus.SelectedValue + "', tprog_user='" + Session["usuario"].ToString() + "', tprog_date=CURRENT_TIMESTAMP(), " +
                    " tprog_tnive_clave='" + ddl_tnive.SelectedValue + "', tprog_tcole_clave='" + ddl_tcole.SelectedValue + "', tprog_tmoda_clave='" + ddl_tmoda.SelectedValue + "'," +
                    " tprog_creditos=" + txt_creditos.Text + ", tprog_cursos=" + txt_cursos.Text + ", tprog_periodos=" + txt_periodos.Text + "," +
                    " tprog_incorporante='" + txt_clave_inc.Text + "'," + " tprog_rvoe='" + txt_rvoe.Text + "', tprog_fecha_rvoe=STR_TO_DATE('" + txt_fecha_rvoe.Text + "','%d/%m/%Y') " +
                    " WHERE tprog_clave='" + txt_tprog.Text + "'";
                MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                conexion.Open();
                MySqlCommand mysqlcmd = new MySqlCommand(strCadSQL, conexion);
                mysqlcmd.CommandType = CommandType.Text;
                try
                {
                    mysqlcmd.ExecuteNonQuery();
                    txt_tprog.Text = null;
                    txt_nombre.Text = null;
                    combo_nivel();
                    combo_colegio();
                    combo_moda();
                    combo_estatus();
                    txt_creditos.Text = null;
                    txt_cursos.Text = null;
                    txt_periodos.Text = null;
                    txt_clave_inc.Text = null;
                    txt_rvoe.Text = null;
                    txt_fecha_rvoe.Text = null;
                    combo_estatus();
                    grid_tprog_bind();
                    Gridtprog.SelectedIndex = -1;
                    btn_save.Visible = true;
                    btn_update.Visible = false;
                    txt_tprog.ReadOnly = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "update_p", "update();", true);
                }
                catch (Exception ex)
                {
                    string test = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tprog", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                }
                finally
                {
                    conexion.Close();
                }
            }
        }

        protected bool valida_tprog(string tprog)
        {
            string Query = "";
            Query = "SELECT COUNT(*) Indicador FROM tprog WHERE tprog_clave='" + tprog + "'";
            MySqlCommand cmd = new MySqlCommand(Query);
            DataTable dt = GetData(cmd);
            if (dt.Rows[0][0].ToString() != "0")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        protected void Gridtprog_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = Gridtprog.SelectedRow;
            txt_tprog.Text = row.Cells[1].Text;
            txt_nombre.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);
            combo_estatus();
            ddl_estatus.SelectedValue = row.Cells[3].Text;
            combo_nivel();
            ddl_tnive.SelectedValue = row.Cells[6].Text;
            combo_colegio();
            ddl_tcole.SelectedValue = row.Cells[7].Text;
            combo_moda();
            ddl_tmoda.SelectedValue = row.Cells[8].Text;
            txt_creditos.Text = HttpUtility.HtmlDecode(row.Cells[9].Text);
            txt_cursos.Text = HttpUtility.HtmlDecode(row.Cells[10].Text);
            txt_periodos.Text = HttpUtility.HtmlDecode(row.Cells[11].Text);
            txt_clave_inc.Text = HttpUtility.HtmlDecode(row.Cells[12].Text);
            txt_rvoe.Text = HttpUtility.HtmlDecode(row.Cells[13].Text);
            txt_fecha_rvoe.Text = HttpUtility.HtmlDecode(row.Cells[14].Text);
            btn_update.Visible = true;
            btn_save.Visible = false;
            txt_tprog.ReadOnly = true;
            grid_tprog_bind();
        }

        protected void txt_tprog_TextChanged(object sender, EventArgs e)
        {
            txt_nombre.Text = null;
            combo_nivel();
            combo_colegio();
            combo_moda();
            combo_estatus();
            txt_creditos.Text = null;
            txt_cursos.Text = null;
            txt_periodos.Text = null;
            txt_clave_inc.Text = null;
            txt_rvoe.Text = null;
            txt_fecha_rvoe.Text = null;
            combo_estatus();
            string strQueryGrid = "";
            strQueryGrid = " select tprog_clave Clave, tprog_desc nombre,  " +
              " tprog_estatus c_estatus,fecha(date_format(tprog_date,'%Y-%m-%d')) fecha, " +
              " tprog_tnive_clave Nivel, tprog_tcole_clave Colegio, tprog_tmoda_clave Modalidad, " +
              " tprog_creditos creditos, tprog_cursos cursos, tprog_periodos periodos, " +
              " tprog_incorporante incorporante, tprog_rvoe rvoe, date_format(tprog_fecha_rvoe,'%d/%m/%Y') fecha_rvoe " +
              " from tprog where tprog_clave='" + txt_tprog.Text + "' order by clave ";
            MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            conexion.Open();
            MySqlDataAdapter sqladapter = new MySqlDataAdapter();

            DataSet dssql1 = new DataSet();

            MySqlCommand commandsql1 = new MySqlCommand(strQueryGrid, conexion);
            sqladapter.SelectCommand = commandsql1;
            sqladapter.Fill(dssql1);
            sqladapter.Dispose();
            commandsql1.Dispose();
            if (dssql1.Tables[0].Rows.Count > 0)
            {
                txt_nombre.Text = dssql1.Tables[0].Rows[0][1].ToString();
                combo_estatus();
                ddl_estatus.SelectedValue = dssql1.Tables[0].Rows[0][2].ToString();
                combo_nivel();
                ddl_tnive.SelectedValue = dssql1.Tables[0].Rows[0][4].ToString();
                combo_colegio();
                ddl_tcole.SelectedValue = dssql1.Tables[0].Rows[0][5].ToString();
                combo_moda();
                ddl_tmoda.SelectedValue = dssql1.Tables[0].Rows[0][6].ToString();
                txt_creditos.Text = dssql1.Tables[0].Rows[0][7].ToString();
                txt_cursos.Text = dssql1.Tables[0].Rows[0][8].ToString();
                txt_periodos.Text = dssql1.Tables[0].Rows[0][9].ToString();
                txt_clave_inc.Text = dssql1.Tables[0].Rows[0][10].ToString();
                txt_rvoe.Text = dssql1.Tables[0].Rows[0][11].ToString();
                txt_fecha_rvoe.Text = dssql1.Tables[0].Rows[0][12].ToString();
                grid_tprog_bind();
            }
            else
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "", "NoClave('ContentPlaceHolder1_txt_tprog',1);", true);
                //grid_tprog_bind();
            }
            conexion.Close();
        }

    }
}