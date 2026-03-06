using MySql.Data.MySqlClient;
using SAES_DBO.Models;
using SAES_Services;
using SAES_v1.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using static SAES_DBO.Models.ModelMenu;

namespace SAES_v1
{
    public partial class tpdoc : System.Web.UI.Page
    {
        #region <Variables>
        Utilidades utils = new Utilidades();
        Catalogos serviceCatalogo = new Catalogos();
        Catalogos_grales_Service serviceCatalogoGrals = new Catalogos_grales_Service();
        List<ModeltpaisResponse> lstPaises = new List<ModeltpaisResponse>();
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

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "ctrl_f_nac", "ctrl_f_nac();", true);
                //txt_matricula.Attributes.Add("readonly", "");

                if (!IsPostBack)
                {
                    LlenaPagina();
                    combo_genero();
                    combo_estado_civil();
                    txt_matricula.Text = Global.clave_docente;
                    txt_nombre.Text = Global.nombre_docente;
                    txt_apellido_p.Text = Global.ap_paterno_docente;
                    txt_apellido_m.Text = Global.ap_materno_docente;
                    Alumno();
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ctrl_fecha_nac", "ctrl_fecha_nac();", true);

            }
        }

        private void LlenaPagina()
        {
            ModelObtenerPermisoFormResponse objPermiso = new ModelObtenerPermisoFormResponse();
            objPermiso.usme_update = "0";
            objPermiso.usme_select = "0";
            //update_pais.Visible = false;

            System.Threading.Thread.Sleep(50);
            try
            {
                objPermiso = servicePermiso.ObtenerPermisoFormulario(Session["usuario"].ToString(), "tpdoc");
                if (objPermiso != null)
                {
                    if (objPermiso.usme_update == "0" || objPermiso.usme_select == "0")
                    {
                        btn_tpdoc.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                    }
                }
                else
                {
                    btn_tpdoc.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                }


            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tpdoc", Session["usuario"].ToString());
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

        protected void combo_genero()
        {
            ddl_genero.Items.Clear();
            ddl_genero.Items.Add(new ListItem("------", "0"));
            ddl_genero.Items.Add(new ListItem("Masculino", "M"));
            ddl_genero.Items.Add(new ListItem("Femenino", "F"));
            ddl_genero.Items.Add(new ListItem("No Aplica", "N"));
        }
        protected void combo_estado_civil()
        {
            ddl_estado_c.Items.Clear();
            ddl_estado_c.Items.Add(new ListItem("-----", "0"));
            ddl_estado_c.Items.Add(new ListItem("Casado", "C"));
            ddl_estado_c.Items.Add(new ListItem("Soltero", "S"));
            ddl_estado_c.Items.Add(new ListItem("Viudo", "V"));
            ddl_estado_c.Items.Add(new ListItem("Union Libre", "U"));
            ddl_estado_c.Items.Add(new ListItem("Divorciado", "D"));
            ddl_estado_c.Items.Add(new ListItem("No Aplica", "N"));

            //CboYear.Items.Clear();
            //DateTime hoy = DateTime.Today;
            //CboYear.Items.Add("---");
            //for (int i = 1950; i < hoy.Year; i++)
            //{
            //    CboYear.Items.Add(i.ToString());
            //}
            //CboMonth.Items.Clear();
            //CboDay.Items.Clear();
        }

        protected void grid_solicitudes_bind(object sender, EventArgs e)
        {
            string QueryEstudiantes = "select distinct tpers_id clave, tpers_nombre nombre, tpers_paterno paterno, tpers_materno materno, tpers_genero c_genero, CASE WHEN tpers_genero = 'F' THEN 'Femenino' WHEN tpers_genero = 'M' THEN 'Masculino' ELSE 'No Aplica' END genero, " +
                                       "tpers_edo_civ c_civil, CASE WHEN tpers_edo_civ = 'C' THEN 'Casado' WHEN tpers_edo_civ = 'S' THEN 'Soltero' WHEN tpers_edo_civ = 'V' THEN 'Viudo' WHEN tpers_edo_civ = 'U' THEN 'Union Libre' WHEN tpers_edo_civ = 'D' THEN 'Divorciado' ELSE 'No Aplica' END e_civil, tpers_curp curp, date_format(tpers_fecha_nac, ' %d/%m/%Y') fecha, tpers_usuario usuario, fecha(date_format(tpers_date, '%Y-%m-%d')) fecha_reg " +
                                        "from tpers " +
                                         "where tpers_tipo = 'D'";
            MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            conexion.Open();
            try
            {
                MySqlDataAdapter dataadapter = new MySqlDataAdapter(QueryEstudiantes, conexion);
                DataSet ds = new DataSet();
                dataadapter.Fill(ds, "Solicitudes");
                GridSolicitudes.DataSource = ds;
                GridSolicitudes.DataBind();
                GridSolicitudes.DataMember = "Solicitudes";
                GridSolicitudes.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridSolicitudes.UseAccessibleHeader = true;

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);
                //GridSolicitudes.Visible = true;
            }
            catch (Exception ex)
            {
                //logs
            }
            conexion.Close();
        }

        protected void Alumno()
        {
            string QueryEstudiantes = "select distinct tpers_id clave, tpers_nombre nombre, tpers_paterno paterno, tpers_materno materno, tpers_genero c_genero, CASE WHEN tpers_genero = 'F' THEN 'Femenino' WHEN tpers_genero = 'M' THEN 'Masculino' ELSE 'No Aplica' END genero, " +
                                       "tpers_edo_civ c_civil, CASE WHEN tpers_edo_civ = 'C' THEN 'Casado' WHEN tpers_edo_civ = 'S' THEN 'Soltero' WHEN tpers_edo_civ = 'V' THEN 'Viudo' WHEN tpers_edo_civ = 'U' THEN 'Union Libre' WHEN tpers_edo_civ = 'D' THEN 'Divorciado' ELSE 'No Aplica' END e_civil, tpers_curp curp, date_format(tpers_fecha_nac, ' %d/%m/%Y') fecha, tpers_usuario usuario, fecha(date_format(tpers_date, '%Y-%m-%d')) fecha_reg " +
                                        "from tpers " +
                                         "where tpers_tipo = 'D' and tpers_id='" + txt_matricula.Text + "'";
            MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            conexion.Open();
            try
            {
                MySqlDataAdapter dataadapter = new MySqlDataAdapter(QueryEstudiantes, conexion);
                DataSet ds = new DataSet();
                dataadapter.Fill(ds, "Solicitudes");
                GridSolicitudes.DataSource = ds;
                GridSolicitudes.DataBind();
                GridSolicitudes.DataMember = "Solicitudes";
                //GridSolicitudes.HeaderRow.TableSection = TableRowSection.TableHeader;
                //GridSolicitudes.UseAccessibleHeader = true;

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);
                //GridSolicitudes.Visible = true;
            }
            catch (Exception ex)
            {
                //logs
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tpdoc", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
            conexion.Close();
        }
        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            txt_matricula.Text = null;
            txt_nombre.Text = null;
            txt_apellido_p.Text = null;
            txt_apellido_m.Text = null;
            combo_genero();
            combo_estado_civil();
            txt_curp.Text = null;
            txt_f_nac.Text = null;
            btn_save.Visible = true;
            btn_update.Visible = false;
            txt_matricula.Attributes.Remove("readonly");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {

            if (ddl_genero.SelectedValue != "0" && ddl_estado_c.SelectedValue != "0" && !String.IsNullOrEmpty(txt_nombre.Text)
                && !String.IsNullOrEmpty(txt_apellido_p.Text) && !String.IsNullOrEmpty(txt_apellido_m.Text)
                && !String.IsNullOrEmpty(txt_curp.Text.Trim()))
            {
                //if (valida_curp_format(txt_curp.Text))
                //{
                string strCadSQL = "";
                double pidm;
                MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                conexion.Open();
                MySqlDataAdapter sqladapter = new MySqlDataAdapter();
                string clave = "";
                string strSeqnpidm = " update tseqn set tseqn_numero=tseqn_numero+1 where tseqn_clave='001'";
                MySqlCommand myCommandupd1 = new MySqlCommand(strSeqnpidm, conexion);
                myCommandupd1.ExecuteNonQuery();


                string strSeqnpidm1 = " select tseqn_numero from tseqn where tseqn_clave='001' ";
                DataSet dssql1 = new DataSet();
                MySqlCommand commandsql1 = new MySqlCommand(strSeqnpidm1, conexion);
                sqladapter.SelectCommand = commandsql1;
                sqladapter.Fill(dssql1);
                sqladapter.Dispose();
                commandsql1.Dispose();

                pidm = Convert.ToDouble(dssql1.Tables[0].Rows[0][0].ToString());
                Global.interno = pidm;

                string strSeqnId = " update tseqn set tseqn_numero=tseqn_numero+1 where tseqn_clave='005'";
                MySqlCommand myCommandupd2 = new MySqlCommand(strSeqnId, conexion);
                myCommandupd2.ExecuteNonQuery();

                string strSeqnId1 = " select lpad(tseqn_numero,tseqn_longitud,'0') from tseqn where tseqn_clave='005' ";

                DataSet dssql2 = new DataSet();
                MySqlCommand commandsql2 = new MySqlCommand(strSeqnId1, conexion);
                sqladapter.SelectCommand = commandsql2;
                sqladapter.Fill(dssql2);
                sqladapter.Dispose();
                commandsql2.Dispose();

                clave = dssql2.Tables[0].Rows[0][0].ToString();
                Global.clave_docente = "D" + clave;

                strCadSQL = "INSERT INTO tpers " +
                " Values (" + pidm + ",'" + Global.clave_docente + "','" + txt_apellido_p.Text + "','" + txt_apellido_m.Text + "','" + txt_nombre.Text + "','" +
                ddl_genero.SelectedValue + "', STR_TO_DATE('" + txt_f_nac.Text + "', ' %d/%m/%Y'),'" + ddl_estado_c.SelectedValue + "','" + txt_curp.Text + "','D', null, current_timestamp(), '" +
                Session["usuario"].ToString() + "')";

                try
                {
                    MySqlCommand myCommandinserta = new MySqlCommand(strCadSQL, conexion);
                    myCommandinserta.ExecuteNonQuery();
                    /*txt_nombre.Text = null;
                    txt_apellido_p.Text = null;
                    txt_apellido_m.Text = null;
                    combo_genero();
                    combo_estado_civil();
                    txt_curp.Text = null;
                    txt_f_nac.Text = null;*/
                    txt_matricula.Text = Global.clave_docente;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                    txt_matricula.Attributes.Add("readonly", "");
                    btn_save.Visible = false;
                    btn_update.Visible = true;
                    Global.clave_docente = txt_matricula.Text;
                    Global.nombre_docente = txt_nombre.Text;
                    Global.ap_paterno_docente = txt_apellido_p.Text;
                    Global.ap_materno_docente = txt_apellido_m.Text;
                    Alumno();

                }
                catch (Exception ex)
                {
                    ///logs
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tpdoc", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                }

                conexion.Close();
                /* }
                 else
                 {
                     ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_solicitud();", true);
                 }*/
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_solicitud();", true);
            }
        }

        protected void btn_update_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txt_nombre.Text) && !String.IsNullOrEmpty(txt_apellido_p.Text) && !String.IsNullOrEmpty(txt_apellido_m.Text) && !String.IsNullOrEmpty(txt_curp.Text.Trim()))
            {
                // if (valida_curp_format(txt_curp.Text))
                // {
                string strCadSQL = "";
                string genero = ddl_genero.SelectedValue.ToString();
                string e_civil = ddl_estado_c.SelectedValue.ToString();
                //if (ddl_genero.SelectedValue == "0") { genero = "N"; } else { genero = ddl_genero.SelectedValue; }
                //if (ddl_estado_c.SelectedValue == "0") { e_civil = "N"; } else { e_civil = ddl_estado_c.SelectedValue; }
                strCadSQL = " update tpers set tpers_nombre='" + txt_nombre.Text + "'," +
                        " tpers_paterno='" + txt_apellido_p.Text + "'," +
                        " tpers_materno='" + txt_apellido_m.Text + "'," +
                        " tpers_genero='" + genero + "'," +
                        " tpers_fecha_nac= STR_TO_DATE('" + txt_f_nac.Text + "', ' %d/%m/%Y'), " +
                        " tpers_edo_civ='" + e_civil + "'," +
                        " tpers_curp='" + txt_curp.Text + "'," +
                        " tpers_date=curdate()," +
                        " tpers_usuario='" + Session["usuario"].ToString() + "'" +
                        " where tpers_id='" + txt_matricula.Text + "'";
                MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                conexion.Open();
                try
                {
                    MySqlCommand myCommandinserta = new MySqlCommand(strCadSQL, conexion);
                    myCommandinserta.ExecuteNonQuery();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "update_p", "update();", true);
                    Alumno();

                }
                catch (Exception ex)
                {
                    ///logs
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tpdoc", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                }
                /* }
                 else
                 {
                     ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validarCurp_format('0');", true);
                 }*/
            }
            else
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_solicitud();", true);
            }

        }

        protected void GridSolicitudes_SelectedIndexChanged(object sender, EventArgs e)
        {

            GridViewRow row = GridSolicitudes.SelectedRow;

            DateTime fecha = DateTime.Parse(row.Cells[10].Text);
            txt_matricula.Text = row.Cells[1].Text;
            txt_nombre.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);
            txt_apellido_p.Text = HttpUtility.HtmlDecode(row.Cells[3].Text);
            txt_apellido_m.Text = HttpUtility.HtmlDecode(row.Cells[4].Text);
            combo_genero();
            ddl_genero.SelectedValue = row.Cells[5].Text;
            combo_estado_civil();
            ddl_estado_c.SelectedValue = row.Cells[7].Text;
            txt_curp.Text = HttpUtility.HtmlDecode(row.Cells[9].Text);
            //txt_f_nac.Text = row.Cells[10].Text;

            txt_f_nac.Text = fecha.ToString("dd/MM/yyyy");

            btn_save.Visible = false;
            btn_update.Visible = true;
            //GridSolicitudes.Visible = false;
            txt_matricula.Attributes.Add("readonly", "");
            btn_save.Visible = false;
            btn_update.Visible = true;
            Global.clave_docente = txt_matricula.Text;
            Global.nombre_docente = txt_nombre.Text;
            Global.ap_paterno_docente = txt_apellido_p.Text;
            Global.ap_materno_docente = txt_apellido_m.Text;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "collapse", "$('#collapseGrid').collapse('toggle')", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ctrl_fecha_nac", "ctrl_fecha_nac();", true);



        }

        protected bool valida_curp_format(string curp_form)
        {
            if (txt_curp.Text.Length == 18)
            {
                double indicador = 0;
                string regex_curp_of =
                "[A-Z]{1}[AEIOU]{1}[A-Z]{2}[0-9]{2}" +
                "(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1])" +
                "[HM]{1}" +
                "(AS|BC|BS|CC|CS|CH|CL|CM|DF|DG|GT|GR|HG|JC|MC|MN|MS|NT|NL|OC|PL|QT|QR|SP|SL|SR|TC|TS|TL|VZ|YN|ZS|NE)" +
                "[B-DF-HJ-NP-TV-Z]{3}" +
                "[0-9A-Z]{1}[0-9]{1}$";
                Regex curp = new Regex(regex_curp_of);
                string dv = curp_form.Substring(17, 1);
                if (!curp.IsMatch(curp_form))
                {
                    return false;
                }
                else
                {

                    string diccionario = "0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZ";
                    double lngSuma = 0.0, lngDigito = 0.0;
                    for (var i = 0; i < 17; i++)
                        lngSuma = lngSuma + diccionario.IndexOf(curp_form[i]) * (18 - i);
                    lngDigito = 10 - lngSuma % 10;
                    if (lngDigito == 10)
                    {
                        indicador = 0;

                    }
                    else
                    {
                        indicador = lngDigito;

                    }
                }
                if (dv == indicador.ToString())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        

       
        protected void Carga_Estudiante(object sender, EventArgs e)
        {
            Carga_Estudiante();
        }

        protected void Carga_Estudiante()
        {
            if (txt_matricula.Text != "")
            {
                string strQuery = "";
                strQuery = " select tpers_id clave, tpers_nombre nombre, tpers_paterno paterno, tpers_materno materno, tpers_genero genero, tpers_edo_civ civil, " +
                    "  tpers_curp curp , date_format(tpers_fecha_nac, ' %d/%m/%Y') fecha, tpers_usuario usuario from tpers ";
                strQuery = strQuery + " where tpers_id='" + txt_matricula.Text + "'";


                //resultado.Text = strQuery;
                MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                conexion.Open();

                MySqlDataAdapter ad1 = new MySqlDataAdapter();

                DataSet ds = new DataSet();
                try
                {
                    MySqlCommand comm = new MySqlCommand(strQuery, conexion);
                    ad1.SelectCommand = comm;
                    ad1.Fill(ds);
                    ad1.Dispose();
                    comm.Dispose();

                    MySqlDataAdapter dataadapter = new MySqlDataAdapter(strQuery, conexion);
                    dataadapter.Fill(ds, "Codigos");

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txt_nombre.Text = ds.Tables[0].Rows[0][1].ToString();
                        txt_apellido_p.Text = ds.Tables[0].Rows[0][2].ToString();
                        txt_apellido_m.Text = ds.Tables[0].Rows[0][3].ToString();
                        txt_f_nac.Text = ds.Tables[0].Rows[0][7].ToString();
                        txt_curp.Text = ds.Tables[0].Rows[0][6].ToString();
                        string genero = "";
                        if (ds.Tables[0].Rows[0][4].ToString() == "M")
                        { genero = "Masculino"; }
                        if (ds.Tables[0].Rows[0][4].ToString() == "F")
                        { genero = "Femenino"; }
                        if (ds.Tables[0].Rows[0][4].ToString() == "N")
                        { genero = "No Aplica"; }

                        string civil = "";
                        if (ds.Tables[0].Rows[0][5].ToString() == "C")
                        { civil = "Casado"; }
                        if (ds.Tables[0].Rows[0][5].ToString() == "S")
                        { civil = "Soltero"; }
                        if (ds.Tables[0].Rows[0][5].ToString() == "V")
                        { civil = "Viudo"; }
                        if (ds.Tables[0].Rows[0][5].ToString() == "U")
                        { civil = "Union Libre"; }
                        if (ds.Tables[0].Rows[0][5].ToString() == "D")
                        { civil = "Divorciado"; }
                        combo_genero();
                        combo_estado_civil();

                        ddl_genero.Items.FindByValue(ds.Tables[0].Rows[0][4].ToString()).Selected = true;
                        ddl_estado_c.Items.FindByValue(ds.Tables[0].Rows[0][5].ToString()).Selected = true;

                        conexion.Close();
                        Global.clave_docente = txt_matricula.Text;
                        Global.nombre_docente = txt_nombre.Text;
                        Global.ap_paterno_docente = txt_apellido_p.Text;
                        Global.ap_materno_docente = txt_apellido_m.Text;
                        txt_matricula.Attributes.Add("readonly", "");
                        btn_save.Visible = false;
                        btn_update.Visible = true;
                        //GridSolicitudes.Visible = true;
                        Alumno();
                    }
                    else
                    {
                        if (txt_matricula.Text != "")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "Noexiste();", true);

                        }
                    }
                }
                catch (Exception ex)
                {
                    //resultado.Text = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tpdoc", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                }

                //Estudiantes.Visible = true;
                txt_matricula.Focus();
            }
            else
            {
                txt_nombre.Text = "";
                txt_apellido_p.Text = "";
                txt_apellido_m.Text = "";
                txt_curp.Text = "";
                txt_f_nac.Text = "";
                // Estudiantes.Visible = false;
                txt_nombre.Focus();
            }
        }

        protected void consulta_docs_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Tdper.aspx");
        }

        protected void linkBttnBusca_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "collapse", "$('#collapseGrid').collapse('toggle')", true);

            string QueryEstudiantes = "select distinct tpers_id clave, tpers_nombre nombre, tpers_paterno paterno, tpers_materno materno, tpers_genero c_genero, CASE WHEN tpers_genero = 'F' THEN 'Femenino' WHEN tpers_genero = 'M' THEN 'Masculino' ELSE 'No Aplica' END genero, " +
                                      "tpers_edo_civ c_civil, CASE WHEN tpers_edo_civ = 'C' THEN 'Casado' WHEN tpers_edo_civ = 'S' THEN 'Soltero' WHEN tpers_edo_civ = 'V' THEN 'Viudo' WHEN tpers_edo_civ = 'U' THEN 'Union Libre' WHEN tpers_edo_civ = 'D' THEN 'Divorciado' ELSE 'No Aplica' END e_civil, tpers_curp curp, date_format(tpers_fecha_nac, ' %d/%m/%Y') fecha, tpers_usuario usuario, fecha(date_format(tpers_date, '%Y-%m-%d')) fecha_reg " +
                                       "from tpers " +
                                        "where tpers_tipo = 'D'";
            MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            conexion.Open();
            try
            {
                MySqlDataAdapter dataadapter = new MySqlDataAdapter(QueryEstudiantes, conexion);
                DataSet ds = new DataSet();
                dataadapter.Fill(ds, "Solicitudes");
                GridSolicitudes.DataSource = ds;
                GridSolicitudes.DataBind();
                GridSolicitudes.DataMember = "Solicitudes";
                GridSolicitudes.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridSolicitudes.UseAccessibleHeader = true;

                //GridSolicitudes.Visible = true;
            }
            catch (Exception ex)
            {
                //logs
            }
            conexion.Close();
        }

        protected void linkBttnBuscar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Tdper.aspx");
        }
    }
}