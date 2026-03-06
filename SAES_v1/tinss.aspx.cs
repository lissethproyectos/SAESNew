using SAES_Services;
using SAES_v1.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Configuration;


namespace SAES_v1
{
    public partial class tinss : System.Web.UI.Page
    {
        Utilidades utils = new Utilidades();
        Catalogos catalogos = new Catalogos();
        RegistroInasistencias registroInasistencias = new RegistroInasistencias();
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
                              " and tusme_tmede_clave = tmede_clave and tmede_forma='tinss' ";

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
                            }
                            else
                            {
                                btn_save.Visible = false;
                            }
                            CargaInicial();
                        }

                    }
                    catch (Exception ex)
                    {
                        //resultado.Text = ex.Message;
                        string mensaje_error = ex.Message.Replace("'", "-");
                        Global.inserta_log(mensaje_error, "tinss", Session["usuario"].ToString());
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                    }
                    finally
                    {
                        conexion.Close();
                    }
                    
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "SetDateControl", "SetDateControl('ContentPlaceHolder1_txt_fecha');", true);
            }
        }

        public void CargaInicial()
        {
            btn_save.Visible = false;

            ddl_periodo.Items.Clear();
            ddl_campus.Items.Clear();
            ddl_materia.Items.Clear();
            ddl_grupo.Items.Clear();

            ddl_periodo = utils.BeginDropdownList(ddl_periodo, catalogos.obtenPeriodo());
            ddl_campus = utils.BeginDropdownList(ddl_campus);
            ddl_materia = utils.BeginDropdownList(ddl_materia);
            ddl_grupo = utils.BeginDropdownList(ddl_grupo);
            Gridtinss = utils.ClearGridView(Gridtinss);
            txt_fecha.Text = string.Empty;
        }

        protected void ddl_periodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Obtiene la fecha fin del periodo
                string QerySelect = "select date_format(tpees_fin,'%d/%m/%Y') from tpees " +
                              " where tpees_clave = '" + ddl_periodo.SelectedValue + "'";

                MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                conexion.Open();

                MySqlDataAdapter sqladapter = new MySqlDataAdapter();

                DataSet dssql1 = new DataSet();

                MySqlCommand commandsql1 = new MySqlCommand(QerySelect, conexion);
                sqladapter.SelectCommand = commandsql1;
                sqladapter.Fill(dssql1);
                sqladapter.Dispose();
                commandsql1.Dispose();
                Global.fecha_fin = Convert.ToDateTime(dssql1.Tables[0].Rows[0][0].ToString());

                string usuario = Session["usuario"].ToString();
                string periodo = (ddl_periodo.SelectedValue == "") ? null : ddl_periodo.SelectedValue;
                if (!string.IsNullOrEmpty(periodo))
                {
                    ddl_campus = utils.BeginDropdownList(ddl_campus, catalogos.obtenCampusDocente(usuario, periodo));
                    ddl_materia = utils.BeginDropdownList(ddl_materia, catalogos.obtenMateriaDocente(usuario, null, periodo));
                    ddl_grupo = utils.BeginDropdownList(ddl_grupo, catalogos.obtenGrupoDocente(usuario, null, null, periodo));
                    if (Global.opcion == 1)
                    {
                        btn_save.Visible = true;
                    }
                }
                else
                {
                    Gridtinss = utils.ClearGridView(Gridtinss);
                }
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tinss", Session["usuario"].ToString(), ex.StackTrace);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
            }
        }

        protected void ddl_campus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string usuario = Session["usuario"].ToString();
                string campus = (ddl_campus.SelectedValue == "") ? null : ddl_campus.SelectedValue;
                string periodo = (ddl_periodo.SelectedValue == "") ? null : ddl_periodo.SelectedValue;
                if (!string.IsNullOrEmpty(periodo))
                {
                    ddl_materia = utils.BeginDropdownList(ddl_materia, catalogos.obtenMateriaDocente(usuario, campus, periodo));
                }
                else
                {
                    Gridtinss = utils.ClearGridView(Gridtinss);
                }
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tinss", Session["usuario"].ToString(), ex.StackTrace);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
            }
        }

        protected void ddl_materia_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string usuario = Session["usuario"].ToString();
                string campus = (ddl_campus.SelectedValue == "") ? null : ddl_campus.SelectedValue;
                string materia = (ddl_materia.SelectedValue == "") ? null : ddl_materia.SelectedValue;
                string periodo = (ddl_periodo.SelectedValue == "") ? null : ddl_periodo.SelectedValue;
                if (!string.IsNullOrEmpty(periodo))
                {
                    ddl_grupo = utils.BeginDropdownList(ddl_grupo, catalogos.obtenGrupoDocente(usuario, campus, materia, periodo));
                }
                else
                {
                    Gridtinss = utils.ClearGridView(Gridtinss);
                }
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tinss", Session["usuario"].ToString(), ex.StackTrace);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
            }
        }

        protected void ddl_grupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string usuario = Session["usuario"].ToString();
                string campus = (ddl_campus.SelectedValue == "") ? null : ddl_campus.SelectedValue;
                string materia = (ddl_materia.SelectedValue == "") ? null : ddl_materia.SelectedValue;
                string periodo = (ddl_periodo.SelectedValue == "") ? null : ddl_periodo.SelectedValue;
                string grupo = (ddl_materia.SelectedValue == "") ? null : ddl_materia.SelectedValue;
                if (!string.IsNullOrEmpty(periodo))
                {

                }
                else
                {
                    Gridtinss = utils.ClearGridView(Gridtinss);
                }
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tinss", Session["usuario"].ToString(), ex.StackTrace);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
            }
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                string periodo = (ddl_periodo.SelectedValue == "") ? null : ddl_periodo.SelectedValue;
                string campus = (ddl_campus.SelectedValue == "") ? null : ddl_campus.SelectedValue;
                string materia = (ddl_materia.SelectedValue == "") ? null : ddl_materia.SelectedValue;
                string grupo = (ddl_grupo.SelectedValue == "") ? null : ddl_grupo.SelectedValue;
                string fecha = txt_fecha.Text;
                string usuario = Session["usuario"].ToString();

                registroInasistencias.DeleteRegistroInasistencias(periodo, campus, materia, grupo, fecha);
                foreach (GridViewRow row in Gridtinss.Rows)
                //for (int i = 0; i < Gridtinss.Rows.Count; i++)
                {
                    var check = (CheckBox)row.FindControl("CHBX_Inasistencia");
                    if (check.Checked == true) 
                    {
                        string matricula = row.Cells[0].Text;
                        string programa = row.Cells[2].Text;                    
                        registroInasistencias.InsertRegistroInasistencias(periodo, campus, materia, grupo, fecha, matricula, usuario, programa);
                    }
                }
                CargaInicial();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_success", "alert_success('Se guardaron los datos exitosamente');", true);


            }
            catch (Exception ex)
            {
                Gridtinss = utils.ClearGridView(Gridtinss);
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tinss", Session["usuario"].ToString(), ex.StackTrace);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
            }
        }

        protected void txt_fecha_TextChange(object sender, EventArgs e)
        {
            string periodo = (ddl_periodo.SelectedValue == "") ? null : ddl_periodo.SelectedValue;
            string campus = (ddl_campus.SelectedValue == "") ? null : ddl_campus.SelectedValue;
            string materia = (ddl_materia.SelectedValue == "") ? null : ddl_materia.SelectedValue;
            string grupo = (ddl_grupo.SelectedValue == "") ? null : ddl_grupo.SelectedValue;
            string fecha = txt_fecha.Text;

            DateTime fecha1 = Convert.ToDateTime(txt_fecha.Text);
            DateTime fecha2 = Convert.ToDateTime(DateTime.Now.ToString());
            TimeSpan dif = fecha2.Subtract(fecha1);

            bool valida = true;
            if (fecha1 > Global.fecha_fin || fecha1 > fecha2)
            {
                valida = false;
            }
            else
            {
                valida = true;
            }

            if (valida == true)
            {

                double dias = Convert.ToDouble(dif.Days);

                if (dias <= 3)
                {

                    try
                    {
                        if (!string.IsNullOrEmpty(periodo) && !string.IsNullOrEmpty(fecha))
                        {
                            if (registroInasistencias.validaFecha(periodo, campus, materia, grupo, fecha))
                            {
                                DataTable dt = registroInasistencias.obtenAlumnosInscritos(periodo, campus, materia, grupo, fecha);
                                //Gridtinss = utils.BeginGrid(Gridtinss, dt);
                                Gridtinss = utils.BeginGrid2(Gridtinss, dt);
                                Gridtinss = validaInasistencias(Gridtinss, dt);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "generic_datatable_load", "generic_datatable_load('Gridtinss');", true);
                                Gridtinss.Visible = true;
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_fecha", "error_fecha();", true);
                                Gridtinss.Visible = false;
                            }
                        }
                        else
                        {
                            Gridtinss = utils.ClearGridView(Gridtinss);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_warning", "alert_warning('No se encontraron datos con la fecha: '" + fecha + ");", true);
                        }
                    }
                    catch (Exception ex)
                    {
                        Gridtinss = utils.ClearGridView(Gridtinss);
                        string mensaje_error = ex.Message.Replace("'", "-");
                        Global.inserta_log(mensaje_error, "tinss", Session["usuario"].ToString(), ex.StackTrace);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "fecha_fuera_rango", "fecha_fuera_rango();", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "fecha_fuera_periodo", "fecha_fuera_periodo();", true);
            }
        }

        private GridView validaInasistencias(GridView grid, DataTable dataTable)
        {
            foreach (GridViewRow row in grid.Rows)
            {
                var rowDT = dataTable.Rows.Cast<DataRow>().Where(x => x["Matricula"].ToString() == row.Cells[0].Text && x["Clave"].ToString() == row.Cells[2].Text && x["Inasistencia"].ToString() == "I").FirstOrDefault();
                if (rowDT != null)
                {
                    var check = (CheckBox)row.FindControl("CHBX_Inasistencia");
                    check.Checked = true;
                    check.DataBind();
                }                
            }
            return grid;
        }

    }
}