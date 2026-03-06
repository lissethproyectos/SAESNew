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
    public partial class tmate : System.Web.UI.Page
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
                if (!IsPostBack)
                {
                    LlenaPagina();
                    combo_estatus();
                }

            }
        }

        //private void LlenaPagina()
        //{
        //    System.Threading.Thread.Sleep(50);

        //    string QerySelect = "select tusme_update, tusme_select from tuser, tusme, tmede " +
        //                      " where tuser_clave = '" + Session["usuario"].ToString() + "'" +
        //                      " and tusme_trole_clave = tuser_trole_clave and tusme_tmenu_clave = 5 and tusme_tmenu_clave = tmede_tmenu_clave " +
        //                      " and tusme_tmede_clave = tmede_clave and tmede_forma='tmate' ";

        //    MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
        //    conexion.Open();
        //    try
        //    {
        //        MySqlDataAdapter sqladapter = new MySqlDataAdapter();

        //        DataSet dssql1 = new DataSet();

        //        MySqlCommand commandsql1 = new MySqlCommand(QerySelect, conexion);
        //        sqladapter.SelectCommand = commandsql1;
        //        sqladapter.Fill(dssql1);
        //        sqladapter.Dispose();
        //        commandsql1.Dispose();
        //        if (dssql1.Tables[0].Rows.Count == 0 || dssql1.Tables[0].Rows[0][1].ToString() == "0")
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
        //        }
        //        else
        //        {
        //            if (dssql1.Tables[0].Rows[0][0].ToString() == "0")
        //            {
        //                btn_tmate.Visible = false;
        //            }
        //            grid_tmate_bind();
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        //resultado.Text = ex.Message;

        //    }
        //    conexion.Close();

        //}
        private void LlenaPagina()
        {
            ModelObtenerPermisoFormResponse objPermiso = new ModelObtenerPermisoFormResponse();
            objPermiso.usme_update = "0";
            objPermiso.usme_select = "0";
            System.Threading.Thread.Sleep(50);
            try
            {
                objPermiso = servicePermiso.ObtenerPermisoFormulario(Session["usuario"].ToString(), "tmate");
                if (objPermiso != null)
                {
                    if (objPermiso.usme_update == "0" || objPermiso.usme_select == "0")
                    {
                        btn_tmate.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                    }
                    else
                    {
                        grid_tmate_bind();

                    }
                }
                else
                {
                    btn_tmate.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                }


            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tmate", Session["usuario"].ToString());
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
        protected void grid_tmate_bind()
        {
            try
            {
                Gridtmate.DataSource = serviceCatalogo.ObtenerMaterias();
                Gridtmate.DataBind();
                Gridtmate.DataMember = "Tmate";
                Gridtmate.HeaderRow.TableSection = TableRowSection.TableHeader;
                Gridtmate.UseAccessibleHeader = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tmate", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            txt_tmate.Text = null;
            txt_nombre.Text = null;
            combo_estatus();
            txt_creditos.Text = null;
            txt_teoricos.Text = null;
            txt_practicos.Text = null;
            txt_campo.Text = null;
            txt_minimo.Text = null;
            txt_maximo.Text = null;
            btn_save.Visible = true;
            btn_update.Visible = false;
            txt_tmate.Attributes.Remove("readonly");
            grid_tmate_bind();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            ModelInsertarTMateResponse objExiste = new ModelInsertarTMateResponse();

            if (!String.IsNullOrEmpty(txt_tmate.Text) && !String.IsNullOrEmpty(txt_nombre.Text) && !String.IsNullOrEmpty(txt_creditos.Text) && !String.IsNullOrEmpty(txt_teoricos.Text)
                 && !String.IsNullOrEmpty(txt_practicos.Text) && !String.IsNullOrEmpty(txt_campo.Text) && !String.IsNullOrEmpty(txt_minimo.Text)
                 && !String.IsNullOrEmpty(txt_maximo.Text))
            {

                objExiste=serviceCatalogo.InsertarTMate(txt_tmate.Text, txt_nombre.Text, txt_creditos.Text, txt_teoricos.Text, txt_practicos.Text,
                         txt_campo.Text, txt_minimo.Text, txt_maximo.Text, "", "", ddl_estatus.SelectedValue, Session["usuario"].ToString()
                        );

                if (objExiste.Existe=="0")
                {
                    double vale = 0;
                    decimal resultado1 = 0; decimal resultado2 = 0;
                    decimal resultado3 = 0; decimal resultado4 = 0;
                    decimal resultado5 = 0; decimal resultado6 = 0;
                    bool cred = Decimal.TryParse(txt_creditos.Text, out resultado1);
                    bool teoria = Decimal.TryParse(txt_teoricos.Text, out resultado2);
                    bool practica = Decimal.TryParse(txt_practicos.Text, out resultado3);
                    bool campo = Decimal.TryParse(txt_campo.Text, out resultado4);
                    bool minimo = Decimal.TryParse(txt_minimo.Text, out resultado5);
                    bool maximo = Decimal.TryParse(txt_maximo.Text, out resultado6);

                    if (cred && teoria && practica && campo && minimo && maximo)
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
                            grid_tmate_bind();
                        }
                        if (teoria)
                        {
                            //
                        }
                        else
                        {
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "valida_teoria();", true);
                            grid_tmate_bind();
                        }
                        if (practica)
                        {
                            //
                        }
                        else
                        {
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "valida_practica();", true);
                            grid_tmate_bind();
                        }

                        if (campo)
                        {
                            //
                        }
                        else
                        {
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "valida_campo();", true);
                            grid_tmate_bind();
                        }

                        if (minimo)
                        {
                            //
                        }
                        else
                        {
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "valida_minimo();", true);
                            grid_tmate_bind();
                        }

                        if (maximo)
                        {
                            //
                        }
                        else
                        {
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "valida_maximo();", true);
                            grid_tmate_bind();
                        }

                    }
                    if (vale == 1)
                    {
                        //string strCadSQL = "INSERT INTO tmate Values ('" + txt_tmate.Text + "','" + txt_nombre.Text + "'," +
                        //    txt_creditos.Text + "," + txt_teoricos.Text + "," + txt_practicos.Text + "," + txt_campo.Text + "," +
                        //    txt_minimo.Text + "," + txt_maximo.Text + ",'" + ddl_estatus.SelectedValue + "',current_timestamp(),'" + Session["usuario"].ToString() + "')";
                       
                        try
                        {
                            //mysqlcmd.ExecuteNonQuery();
                            txt_tmate.Text = null;
                            txt_nombre.Text = null;
                            txt_creditos.Text = null;
                            txt_teoricos.Text = null;
                            txt_practicos.Text = null;
                            txt_campo.Text = null;
                            txt_minimo.Text = null;
                            txt_maximo.Text = null;
                            ddl_estatus.SelectedIndex = 0;
                            grid_tmate_bind();
                            Gridtmate.SelectedIndex = -1;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                        }
                        catch (Exception ex)
                        {
                            string test = ex.Message;
                            string mensaje_error = ex.Message.Replace("'", "-");
                            Global.inserta_log(mensaje_error, "tmate", Session["usuario"].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                        }
                        
                    }
                }
                else
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validarClave('ContentPlaceHolder1_txt_tmate',1);", true);
                    grid_tmate_bind();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_tmate();", true);
                grid_tmate_bind();
            }


        }

        protected void btn_update_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txt_tmate.Text) && !String.IsNullOrEmpty(txt_nombre.Text) && !String.IsNullOrEmpty(txt_creditos.Text) && !String.IsNullOrEmpty(txt_teoricos.Text)
                 && !String.IsNullOrEmpty(txt_practicos.Text) && !String.IsNullOrEmpty(txt_campo.Text) && !String.IsNullOrEmpty(txt_minimo.Text)
                 && !String.IsNullOrEmpty(txt_maximo.Text))
            {
                double vale = 0;
                decimal resultado1 = 0; decimal resultado2 = 0;
                decimal resultado3 = 0; decimal resultado4 = 0;
                decimal resultado5 = 0; decimal resultado6 = 0;
                bool cred = Decimal.TryParse(txt_creditos.Text, out resultado1);
                bool teoria = Decimal.TryParse(txt_teoricos.Text, out resultado2);
                bool practica = Decimal.TryParse(txt_practicos.Text, out resultado3);
                bool campo = Decimal.TryParse(txt_campo.Text, out resultado4);
                bool minimo = Decimal.TryParse(txt_minimo.Text, out resultado5);
                bool maximo = Decimal.TryParse(txt_maximo.Text, out resultado6);

                if (cred && teoria && practica && campo && minimo && maximo)
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
                        grid_tmate_bind();
                    }
                    if (teoria)
                    {
                        //
                    }
                    else
                    {
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "valida_teoria();", true);
                        grid_tmate_bind();
                    }
                    if (practica)
                    {
                        //
                    }
                    else
                    {
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "valida_practica();", true);
                        grid_tmate_bind();
                    }

                    if (campo)
                    {
                        //
                    }
                    else
                    {
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "valida_campo();", true);
                        grid_tmate_bind();
                    }

                    if (minimo)
                    {
                        //
                    }
                    else
                    {
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "valida_minimo();", true);
                        grid_tmate_bind();
                    }

                    if (maximo)
                    {
                        //
                    }
                    else
                    {
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "valida_maximo();", true);
                        grid_tmate_bind();
                    }

                }
                if (vale == 1)
                {
                    //string strCadSQL = "UPDATE tmate SET tmate_desc='" + txt_nombre.Text + "', tmate_estatus='" + ddl_estatus.SelectedValue + 
                    //    "', tmate_usuario='" + Session["usuario"].ToString() + "', tmate_date=CURRENT_TIMESTAMP(), " +
                    //    " tmate_creditos=" + txt_creditos.Text + ", tmate_hr_teo=" + txt_teoricos.Text + ", tmate_hr_prac=" + txt_practicos.Text + "," +
                    //    " tmate_hr_campo=" + txt_campo.Text + "," + " tmate_min_cupo=" + txt_minimo.Text + ", tmate_max_cupo=" + txt_maximo.Text +
                    //    " WHERE tmate_clave='" + txt_tmate.Text + "'";
                    serviceCatalogo.EditarTMate(txt_tmate.Text, txt_nombre.Text, txt_creditos.Text, txt_teoricos.Text, txt_practicos.Text,
                         txt_campo.Text, txt_minimo.Text, txt_maximo.Text, "", "", ddl_estatus.SelectedValue, Session["usuario"].ToString()
                        );

                    try
                    {

                        txt_tmate.Text = null;
                        txt_nombre.Text = null;
                        txt_creditos.Text = null;
                        txt_teoricos.Text = null;
                        txt_practicos.Text = null;
                        txt_campo.Text = null;
                        txt_minimo.Text = null;
                        txt_maximo.Text = null;
                        txt_tmate.ReadOnly = false;
                        combo_estatus();
                        grid_tmate_bind();
                        Gridtmate.SelectedIndex = -1;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "update_p", "update();", true);
                    }
                    catch (Exception ex)
                    {
                        string test = ex.Message;
                        string mensaje_error = ex.Message.Replace("'", "-");
                        Global.inserta_log(mensaje_error, "tmate", Session["usuario"].ToString());
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_tmate();", true);
            }
        }

        protected bool valida_tmate(string tmate)
        {
            string Query = "";
            Query = "SELECT COUNT(*) Indicador FROM tmate WHERE tmate_clave='" + tmate + "'";
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

        protected void Gridtmate_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = Gridtmate.SelectedRow;
            txt_tmate.Text = row.Cells[1].Text;
            txt_nombre.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);
            combo_estatus();
            ddl_estatus.SelectedValue = row.Cells[3].Text;
            txt_creditos.Text = HttpUtility.HtmlDecode(row.Cells[6].Text);
            txt_teoricos.Text = HttpUtility.HtmlDecode(row.Cells[7].Text);
            txt_practicos.Text = HttpUtility.HtmlDecode(row.Cells[8].Text);
            txt_campo.Text = HttpUtility.HtmlDecode(row.Cells[9].Text);
            txt_minimo.Text = HttpUtility.HtmlDecode(row.Cells[10].Text);
            txt_maximo.Text = HttpUtility.HtmlDecode(row.Cells[11].Text);
            btn_update.Visible = true;
            btn_save.Visible = false;
            txt_tmate.ReadOnly = true;
            grid_tmate_bind();
        }

        protected void txt_tmate_TextChanged(object sender, EventArgs e)
        {
            string strQueryGrid = "";
            strQueryGrid = " select tmate_clave Clave, tmate_desc nombre,  " +
              " tmate_estatus c_estatus,fecha(date_format(tmate_date,'%Y-%m-%d')) fecha, " +
              " tmate_creditos creditos, tmate_hr_teo teoricos, tmate_hr_prac practicos, " +
              " tmate_hr_campo campo, tmate_min_cupo minimo, tmate_max_cupo maximo " +
              " from tmate where tmate_clave='" + txt_tmate.Text + "' order by clave ";
            MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            conexion.Open();
            MySqlDataAdapter sqladapter = new MySqlDataAdapter();

            DataSet dssql1 = new DataSet();

            MySqlCommand commandsql1 = new MySqlCommand(strQueryGrid, conexion);
            sqladapter.SelectCommand = commandsql1;
            sqladapter.Fill(dssql1);
            sqladapter.Dispose();
            commandsql1.Dispose();
            txt_nombre.Text = dssql1.Tables[0].Rows[0][1].ToString();
            combo_estatus();
            ddl_estatus.SelectedValue = dssql1.Tables[0].Rows[0][2].ToString();
            txt_creditos.Text = dssql1.Tables[0].Rows[0][4].ToString();
            txt_teoricos.Text = dssql1.Tables[0].Rows[0][5].ToString();
            txt_practicos.Text = dssql1.Tables[0].Rows[0][6].ToString();
            txt_campo.Text = dssql1.Tables[0].Rows[0][7].ToString();
            txt_minimo.Text = dssql1.Tables[0].Rows[0][8].ToString();
            txt_maximo.Text = dssql1.Tables[0].Rows[0][9].ToString();
            conexion.Close();
        }
    }
}