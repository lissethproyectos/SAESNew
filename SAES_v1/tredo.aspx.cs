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
using static SAES_DBO.Models.ModelAlumno;
using static SAES_DBO.Models.ModelMenu;

namespace SAES_v1
{
    public partial class tredo : System.Web.UI.Page
    {
        #region <Variables>
        Utilidades utils = new Utilidades();
        Catalogos serviceCatalogo = new Catalogos();
        MenuService servicePermiso = new MenuService();
        AlumnoService serviceAlumno = new AlumnoService();
        DocumentoService serviceDocto= new DocumentoService();
        List<ModelObtenPaisesResponse> lstPaises = new List<ModelObtenPaisesResponse>();
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
                    txt_matricula.Text = Global.cuenta;
                    txt_nombre.Text = Global.nombre + " " + Global.ap_paterno + " " + Global.ap_materno;
                    LlenaPagina();
                    grid_documentos_bind(txt_matricula.Text);
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_Alumnos", "load_datatable_Alumnos();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_Doctos", "load_datatable_Doctos();", true);

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
        //protected void llena_pagina()
        //{
        //    string QerySelect = "select tusme_update, tusme_select from tuser, tusme, tmede " +
        //                      " where tuser_clave = '" + Session["usuario"].ToString() + "'" +
        //                      " and tusme_trole_clave = tuser_trole_clave and tusme_tmenu_clave = 3 and tusme_tmenu_clave = tmede_tmenu_clave " +
        //                      " and tusme_tmede_clave = tmede_clave and tmede_forma='tredo' ";

        //    MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
        //    ConexionMySql.Open();
        //    MySqlDataAdapter sqladapter = new MySqlDataAdapter();
        //    try
        //    {


        //        DataSet dssql1 = new DataSet();

        //        MySqlCommand commandsql1 = new MySqlCommand(QerySelect, ConexionMySql);
        //        sqladapter.SelectCommand = commandsql1;
        //        sqladapter.Fill(dssql1);
        //        sqladapter.Dispose();
        //        commandsql1.Dispose();
        //        if (dssql1.Tables[0].Rows.Count == 0 || dssql1.Tables[0].Rows[0][1].ToString() == "0")
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
        //        }
        //        if (dssql1.Tables[0].Rows[0][0].ToString() == "0")
        //        {
        //            btn_documentos.Visible = false;
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        string mensaje_error = ex.Message.Replace("'", "-");
        //        Global.inserta_log(mensaje_error, "tredo", Session["usuario"].ToString());
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
        //    }

        //    //Verifica si existen registros guardados en relación de documentos para el el alumno
        //    string Querytredo = "select count(*) from tredo where tredo_tpers_num in (select tpers_num from tpers where tpers_id='" + txt_matricula.Text + "')";

        //    DataSet dssql2 = new DataSet();

        //    MySqlCommand commandsql2 = new MySqlCommand(Querytredo, ConexionMySql);
        //    sqladapter.SelectCommand = commandsql2;
        //    sqladapter.Fill(dssql2);
        //    sqladapter.Dispose();
        //    commandsql2.Dispose();
        //    if (dssql2.Tables[0].Rows[0][0].ToString() == "0")
        //    {
        //        //Inserta registros en tredo de acuerdo a la configuración de tcodo
        //        string Queryinserttredo = " select distinct tpers_num Id_Num,tadmi_tpees_clave periodo, " +
        //         " tadmi_consecutivo consecutivo, tcodo_tdocu_clave clave_docto, tdocu_desc descripcion, 'PE' c_estatus " +
        //         " from tpers inner join tadmi a on tadmi_tpers_num = tpers_num  " +
        //         " inner join  tcodo on tcodo_tdocu_clave = tcodo_tdocu_clave " +
        //         " inner join tdocu on tcodo_tdocu_clave = tdocu_clave and tdocu_estatus = 'A' " +
        //         " inner join tprog on tprog_clave = tadmi_tprog_clave " +
        //         " where tpers_id = '" + txt_matricula.Text + "'" +
        //         " and(tcodo_tcamp_clave = tadmi_tcamp_clave or tcodo_tcamp_clave = '000') " +
        //         " and(tcodo_tnive_clave = tprog_tnive_clave or tcodo_tnive_clave = '000') " +
        //         " and(tcodo_tcole_clave = tprog_tcole_clave or tcodo_tcole_clave = '000') " +
        //         " and(tcodo_tmoda_clave = tprog_tmoda_clave or tcodo_tmoda_clave = '000') " +
        //         " and(tcodo_tprog_clave = tprog_clave or tcodo_tprog_clave = '0000000000') " +
        //         " and(tcodo_ttiin_clave = tadmi_ttiin_clave or tcodo_ttiin_clave = '000') " +
        //         " and tcodo_estatus = 'A' order by tadmi_consecutivo desc, tadmi_tpees_clave, tcodo_tdocu_clave ";

        //        DataSet dssql3 = new DataSet();
        //        try
        //        {
        //            MySqlCommand commandsql3 = new MySqlCommand(Queryinserttredo, ConexionMySql);
        //            sqladapter.SelectCommand = commandsql3;
        //            sqladapter.Fill(dssql3);
        //            sqladapter.Dispose();
        //            commandsql3.Dispose();
        //            String QueryInsert = "";
        //            for (int i = 0; i < dssql3.Tables[0].Rows.Count; i++)
        //            {
        //                QueryInsert = "INSERT INTO tredo Values (" + dssql3.Tables[0].Rows[i][0].ToString() + ",'" + dssql3.Tables[0].Rows[i][1].ToString() + "'," +
        //                   dssql3.Tables[0].Rows[i][2].ToString() + ",'" + dssql3.Tables[0].Rows[i][3].ToString() + "','PE',null, null,'" + Session["usuario"].ToString() + "',current_timestamp(),'A') ";
        //                MySqlCommand mysqlcmd = new MySqlCommand(QueryInsert, ConexionMySql);
        //                mysqlcmd.CommandType = CommandType.Text;
        //                try
        //                {
        //                    mysqlcmd.ExecuteNonQuery();
        //                }
        //                catch (Exception ex)
        //                {
        //                    string test = ex.Message;
        //                }
        //            }
        //        }
        //        catch(Exception ex)
        //        {
        //            string mensaje_error = ex.Message.Replace("'", "-");
        //            Global.inserta_log(mensaje_error, "tredo", Session["usuario"].ToString());
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
        //        }
        //    }
        //    ConexionMySql.Close();

        //    ddl_estatus.Items.Clear();
        //    ddl_estatus.Items.Add(new ListItem("Activo", "A"));
        //    ddl_estatus.Items.Add(new ListItem("Inactivo", "B"));

        //}
        private void LlenaPagina()
        {
            ModelObtenerPermisoFormResponse objPermiso = new ModelObtenerPermisoFormResponse();
            objPermiso.usme_update = "0";
            objPermiso.usme_select = "0";
            System.Threading.Thread.Sleep(50);
            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            ConexionMySql.Open();

            MySqlDataAdapter sqladapter = new MySqlDataAdapter();

            try
            {
                objPermiso = servicePermiso.ObtenerPermisoFormulario(Session["usuario"].ToString(), "tredo");
                if (objPermiso != null)
                {
                    if (objPermiso.usme_update == "0" || objPermiso.usme_select == "0")
                    {
                        btn_documentos.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                    }
                }
                else
                {
                    btn_documentos.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                }


            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tredo", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);

            }

            //Verifica si existen registros guardados en relación de documentos para el el alumno
            string Querytredo = "select count(*) from tredo where tredo_tpers_num in (select tpers_num from tpers where tpers_id='" + txt_matricula.Text + "')";

            DataSet dssql2 = new DataSet();

            MySqlCommand commandsql2 = new MySqlCommand(Querytredo, ConexionMySql);
            sqladapter.SelectCommand = commandsql2;
            sqladapter.Fill(dssql2);
            sqladapter.Dispose();
            commandsql2.Dispose();
            if (dssql2.Tables[0].Rows[0][0].ToString() == "0")
            {
                //Inserta registros en tredo de acuerdo a la configuración de tcodo
                string Queryinserttredo = " select distinct tpers_num Id_Num,tadmi_tpees_clave periodo, " +
                 " tadmi_consecutivo consecutivo, tcodo_tdocu_clave clave_docto, tdocu_desc descripcion, 'PE' c_estatus " +
                 " from tpers inner join tadmi a on tadmi_tpers_num = tpers_num  " +
                 " inner join  tcodo on tcodo_tdocu_clave = tcodo_tdocu_clave " +
                 " inner join tdocu on tcodo_tdocu_clave = tdocu_clave and tdocu_estatus = 'A' " +
                 " inner join tprog on tprog_clave = tadmi_tprog_clave " +
                 " where tpers_id = '" + txt_matricula.Text + "'" +
                 " and(tcodo_tcamp_clave = tadmi_tcamp_clave or tcodo_tcamp_clave = '000') " +
                 " and(tcodo_tnive_clave = tprog_tnive_clave or tcodo_tnive_clave = '000') " +
                 " and(tcodo_tcole_clave = tprog_tcole_clave or tcodo_tcole_clave = '000') " +
                 " and(tcodo_tmoda_clave = tprog_tmoda_clave or tcodo_tmoda_clave = '000') " +
                 " and(tcodo_tprog_clave = tprog_clave or tcodo_tprog_clave = '0000000000') " +
                 " and(tcodo_ttiin_clave = tadmi_ttiin_clave or tcodo_ttiin_clave = '000') " +
                 " and tcodo_estatus = 'A' order by tadmi_consecutivo desc, tadmi_tpees_clave, tcodo_tdocu_clave ";

                DataSet dssql3 = new DataSet();
                try
                {
                    MySqlCommand commandsql3 = new MySqlCommand(Queryinserttredo, ConexionMySql);
                    sqladapter.SelectCommand = commandsql3;
                    sqladapter.Fill(dssql3);
                    sqladapter.Dispose();
                    commandsql3.Dispose();
                    String QueryInsert = "";
                    for (int i = 0; i < dssql3.Tables[0].Rows.Count; i++)
                    {
                        QueryInsert = "INSERT INTO tredo Values (" + dssql3.Tables[0].Rows[i][0].ToString() + ",'" + dssql3.Tables[0].Rows[i][1].ToString() + "'," +
                           dssql3.Tables[0].Rows[i][2].ToString() + ",'" + dssql3.Tables[0].Rows[i][3].ToString() + "','PE',null, null,'" + Session["usuario"].ToString() + "',current_timestamp(),'A') ";
                        MySqlCommand mysqlcmd = new MySqlCommand(QueryInsert, ConexionMySql);
                        mysqlcmd.CommandType = CommandType.Text;
                        try
                        {
                            mysqlcmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            string test = ex.Message;
                        }
                    }
                }
                catch (Exception ex)
                {
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tredo", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                }
            }
            ConexionMySql.Close();

            ddl_estatus.Items.Clear();
            ddl_estatus.Items.Add(new ListItem("Activo", "A"));
            ddl_estatus.Items.Add(new ListItem("Inactivo", "B"));
        }

        protected void grid_documentos_bind(string matricula)
        {
            try
            {
                GridDocumentos.DataSource = serviceAlumno.ObtenerDocumentos(txt_matricula.Text);
                GridDocumentos.DataBind();
                GridDocumentos.DataMember = "Solicitud";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                GridDocumentos.Visible = true;

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tredo", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }
        protected void txt_matricula_TextChanged(object sender, EventArgs e)
        {
            if (valida_matricula(txt_matricula.Text))
            {

                txt_nombre.Text = nombre_alumno(txt_matricula.Text);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                //Verifica si existen registros guardados en relación de documentos para el el alumno
                string Querytredo = "select count(*) from tredo where tredo_tpers_num in (select tpers_num from tpers where tpers_id='" + txt_matricula.Text + "')";

                DataSet dssql2 = new DataSet();
                MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                ConexionMySql.Open();
                MySqlDataAdapter sqladapter = new MySqlDataAdapter();

                MySqlCommand commandsql2 = new MySqlCommand(Querytredo, ConexionMySql);
                sqladapter.SelectCommand = commandsql2;
                sqladapter.Fill(dssql2);
                sqladapter.Dispose();
                commandsql2.Dispose();
                if (dssql2.Tables[0].Rows[0][0].ToString() == "0")
                {
                    //Inserta registros en tredo de acuerdo a la configuración de tcodo
                    string Queryinserttredo = " select distinct tpers_num Id_Num,tadmi_tpees_clave periodo, " +
                     " tadmi_consecutivo consecutivo, tcodo_tdocu_clave clave_docto, tdocu_desc descripcion, 'PE' c_estatus " +
                     " from tpers inner join tadmi a on tadmi_tpers_num = tpers_num  " +
                     " inner join  tcodo on tcodo_tdocu_clave = tcodo_tdocu_clave " +
                     " inner join tdocu on tcodo_tdocu_clave = tdocu_clave and tdocu_estatus = 'A' " +
                     " inner join tprog on tprog_clave = tadmi_tprog_clave " +
                     " where tpers_id = '" + txt_matricula.Text + "'" +
                     " and(tcodo_tcamp_clave = tadmi_tcamp_clave or tcodo_tcamp_clave = '000') " +
                     " and(tcodo_tnive_clave = tprog_tnive_clave or tcodo_tnive_clave = '000') " +
                     " and(tcodo_tcole_clave = tprog_tcole_clave or tcodo_tcole_clave = '000') " +
                     " and(tcodo_tmoda_clave = tprog_tmoda_clave or tcodo_tmoda_clave = '000') " +
                     " and(tcodo_tprog_clave = tprog_clave or tcodo_tprog_clave = '0000000000') " +
                     " and(tcodo_ttiin_clave = tadmi_ttiin_clave or tcodo_ttiin_clave = '000') " +
                     " and tcodo_estatus = 'A' order by tadmi_consecutivo desc, tadmi_tpees_clave, tcodo_tdocu_clave ";

                    DataSet dssql3 = new DataSet();
                    try
                    {
                        MySqlCommand commandsql3 = new MySqlCommand(Queryinserttredo, ConexionMySql);
                        sqladapter.SelectCommand = commandsql3;
                        sqladapter.Fill(dssql3);
                        sqladapter.Dispose();
                        commandsql3.Dispose();
                        String QueryInsert = "";
                        for (int i = 0; i < dssql3.Tables[0].Rows.Count; i++)
                        {
                            QueryInsert = "INSERT INTO tredo Values (" + dssql3.Tables[0].Rows[i][0].ToString() + ",'" + dssql3.Tables[0].Rows[i][1].ToString() + "'," +
                               dssql3.Tables[0].Rows[i][2].ToString() + ",'" + dssql3.Tables[0].Rows[i][3].ToString() + "','PE',null, null,'" + Session["usuario"].ToString() + "',current_timestamp(),'A') ";
                            MySqlCommand mysqlcmd = new MySqlCommand(QueryInsert, ConexionMySql);
                            mysqlcmd.CommandType = CommandType.Text;
                            try
                            {
                                mysqlcmd.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                string test = ex.Message;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        string mensaje_error = ex.Message.Replace("'", "-");
                        Global.inserta_log(mensaje_error, "tredo", Session["usuario"].ToString());
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                    }
                }
                grid_documentos_bind(txt_matricula.Text);

            }
            else
            {
                ///Matricula no existe
            }
        }
        protected bool valida_matricula(string matricula)
        {
            string Query = "";
            Query = "SELECT COUNT(*) Indicador FROM tpers WHERE tpers_id Like '" + matricula + "'";
            MySqlCommand cmd = new MySqlCommand(Query);
            DataTable dt = GetData(cmd);
            if (dt.Rows[0]["Indicador"].ToString() == "0")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        protected string nombre_alumno(string matricula)
        {
            string nombre = "";
            string Query = "";
            Query = "SELECT tpers_num, CONCAT(tpers_nombre,' ',tpers_materno,' ',tpers_paterno) nombre, " +
                " tpers_nombre, tpers_paterno, tpers_materno FROM tpers WHERE tpers_id = '" + txt_matricula.Text + "'";
            MySqlCommand cmd = new MySqlCommand(Query);
            DataTable dt = GetData(cmd);
            nombre = dt.Rows[0]["nombre"].ToString();
            lbl_id_pers.Text = dt.Rows[0]["tpers_num"].ToString();
            Global.cuenta = txt_matricula.Text;
            Global.nombre = dt.Rows[0]["tpers_nombre"].ToString();
            Global.ap_paterno = dt.Rows[0]["tpers_paterno"].ToString();
            Global.ap_materno = dt.Rows[0]["tpers_materno"].ToString();
            return nombre;
        }
        protected bool valida_redo(string matricula, string documento)
        {
            string Query = "";
            Query = "SELECT COUNT(*) Indicador FROM tredo WHERE tredo_tpers_num = (SELECT DISTINCT tpers_num FROM tpers WHERE tpers_id='" + matricula + "') and tredo_tdocu_clave='" + documento + "'";
            MySqlCommand cmd = new MySqlCommand(Query);
            DataTable dt = GetData(cmd);
            if (dt.Rows[0]["Indicador"].ToString() == "0")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            txt_matricula.Text = null;
            txt_matricula.ReadOnly = false;
            txt_nombre.Text = null;
            txt_clave_doc.Text = null;
            txt_documento.Text = null;
            txt_fecha_l.Text = null;
            txt_fecha_e.Text = null;
            txt_estatus.Text = null;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
            GridDocumentos.DataSource = null;
            GridDocumentos.DataBind();
            //GridDocumentos.Visible = false;
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {

            if (valida_redo(txt_matricula.Text, txt_clave_doc.Text))
            {
                //Update//
                string Query = "UPDATE tredo SET tredo_fecha_limite=STR_TO_DATE('" + txt_fecha_l.Text + "', '%d/%m/%Y'),  tredo_user='" + Session["usuario"].ToString() + "', tredo_estatus='" + ddl_estatus.SelectedValue + "' WHERE tredo_tpers_num in (select tpers_num from tpers where tpers_id='" + txt_matricula.Text + "') AND tredo_tdocu_clave='" + txt_clave_doc.Text + "' AND tredo_consecutivo='" + lbl_consecutivo.Text + "' AND tredo_tpees_clave='" + lbl_periodo.Text + "'";

                MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                conexion.Open();
                MySqlCommand mysqlcmd = new MySqlCommand(Query, conexion);
                mysqlcmd.CommandType = CommandType.Text;
                try
                {
                    mysqlcmd.ExecuteNonQuery();
                    grid_documentos_bind(txt_matricula.Text);
                    txt_clave_doc.Text = null;
                    txt_documento.Text = null;
                    txt_fecha_l.Text = null;
                    txt_fecha_e.Text = null;
                    txt_estatus.Text = null;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "update_p", "update();", true);
                }
                catch (Exception ex)
                {
                    string test = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tredo", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                }
            }
            else
            {
                //insert//
                string Query = "";
                //Obtiene los último valores de la solicitud de Periodo y consecutivo
                Query = "select tadmi_tpees_clave periodo, tadmi_consecutivo consecutivo from tadmi a " +
                  " where tadmi_tpers_num in (select tpers_num from tpers where tpers_id = '" + txt_matricula.Text + "')" +
                  " and tadmi_consecutivo in (select max(tadmi_consecutivo) from tadmi aa where a.tadmi_tpers_num = aa.tadmi_tpers_num) ";
                MySqlCommand cmd = new MySqlCommand(Query);
                DataTable dt = GetData(cmd);
                lbl_periodo.Text = dt.Rows[0]["periodo"].ToString();
                lbl_consecutivo.Text = dt.Rows[0]["consecutivo"].ToString();

                if (txt_fecha_l.Text != "")
                {
                    Query = "INSERT INTO tredo Values ((select tpers_num from tpers " +
                        "where tpers_id='" + txt_matricula.Text + "'),'" + lbl_periodo.Text + "'," + 
                        lbl_consecutivo.Text + ",'" +
                    txt_clave_doc.Text + "','PE',STR_TO_DATE('" + txt_fecha_l.Text + "', '%d/%m/%Y'),null, '" +
                     Session["usuario"].ToString() + "',current_timestamp(),'" + ddl_estatus.SelectedValue + "') ";
                }
                else
                {
                    Query = "INSERT INTO tredo Values ((select tpers_num from tpers where tpers_id='" + txt_matricula.Text + "'),'" + lbl_periodo.Text + "'," + lbl_consecutivo.Text + ",'" +
                    txt_clave_doc.Text + "','PE',null, null,'" + Session["usuario"].ToString() + "',current_timestamp(),'" + ddl_estatus.SelectedValue + "') ";
                }
                MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                conexion.Open();
                MySqlCommand mysqlcmd = new MySqlCommand(Query, conexion);
                mysqlcmd.CommandType = CommandType.Text;
                try
                {
                    mysqlcmd.ExecuteNonQuery();
                    grid_documentos_bind(txt_matricula.Text);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                }
                catch (Exception ex)
                {
                    string test = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tredo", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                }
            }
        }

        protected void GridDocumentos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = GridDocumentos.SelectedRow;
                //txt_matricula.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);
                //txt_nombre.Text = HttpUtility.HtmlDecode(row.Cells[3].Text);
                lbl_periodo.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);
                lbl_consecutivo.Text = HttpUtility.HtmlDecode(row.Cells[4].Text);
                txt_clave_doc.Text = HttpUtility.HtmlDecode(row.Cells[5].Text);
                txt_documento.Text = HttpUtility.HtmlDecode(row.Cells[6].Text);
                txt_estatus.Text = HttpUtility.HtmlDecode(row.Cells[8].Text).Trim();
                txt_fecha_l.Text = HttpUtility.HtmlDecode(row.Cells[10].Text).Trim();
                txt_fecha_e.Text = HttpUtility.HtmlDecode(row.Cells[12].Text).Trim();
                ddl_estatus.SelectedValue = row.Cells[14].Text;
                grid_documentos_bind(txt_matricula.Text);
            }
            catch (Exception EX)
            {
                string TEST = EX.Message;
            }
        }

    


        protected void Carga_Estudiante()
        {
            ModelObtenerAlumnosResponse datosAlumno = new ModelObtenerAlumnosResponse();
            txt_nombre.Text = string.Empty;
            DataTable dtAlumno = new DataTable();
            GridAlumnos.Visible = false;
            try
            {
                dtAlumno = serviceAlumno.ObtenerAlumnos(txt_matricula.Text);
                if (dtAlumno.Rows.Count == 1)
                {
                    txt_nombre.Text = dtAlumno.Rows[0][1].ToString() + " " + dtAlumno.Rows[0][2].ToString() + " " + dtAlumno.Rows[0][3].ToString();
                    Global.cuenta = txt_matricula.Text;
                    Global.nombre = dtAlumno.Rows[0][1].ToString() + " " + dtAlumno.Rows[0][2].ToString() + " " + dtAlumno.Rows[0][3].ToString();
                    grid_documentos_bind(txt_matricula.Text);
                    txt_matricula.ReadOnly = true;

                    //grid_direccion_bind(txt_matricula.Text);
                }
                else
                {
                    GridAlumnos.DataSource = serviceAlumno.ObtenerAlumnos(txt_matricula.Text);
                    GridAlumnos.DataBind();
                    GridAlumnos.Visible = true;
                }
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tredo", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }

            //Estudiantes.Visible = true;
            //txt_matricula.Focus();

        }

        protected void grid_doctos_bind(object sender, EventArgs e)
        {
            if (GridDoctos.Visible == true)
            {
                GridDoctos.Visible = false;
            }
            else
            {
                string QueryDoctos = "select tdocu_clave CLAVE, tdocu_desc DOCUMENTO from tdocu " +
                    " where tdocu_estatus='A' and tdocu_clave not in (select tredo_tdocu_clave from tredo " +
                    " where tredo_tpers_num in (select tpers_num from tpers where tpers_id='" + txt_matricula.Text + "'))";
                MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                conexion.Open();
                try
                {
                    MySqlDataAdapter dataadapter = new MySqlDataAdapter(QueryDoctos, conexion);
                    DataSet ds = new DataSet();
                    dataadapter.Fill(ds, "Doctos");
                    GridDoctos.DataSource = ds;
                    GridDoctos.DataBind();
                    GridDoctos.DataMember = "Doctos";
                    GridDoctos.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridDoctos.UseAccessibleHeader = true;

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_Doctos", "load_datatable_Doctos();", true);
                    GridDoctos.Visible = true;
                }
                catch (Exception ex)
                {
                    //logs
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tredo", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                }
                conexion.Close();
            }
        }

        protected void GridAlumnos_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridAlumnos.SelectedRow;
            txt_matricula.Text = row.Cells[1].Text;
            txt_nombre.Text = HttpUtility.HtmlDecode(row.Cells[2].Text) + " " + HttpUtility.HtmlDecode(row.Cells[3].Text) + " " +
             HttpUtility.HtmlDecode(row.Cells[4].Text);
            btn_save.Visible = false;
            //btn_update.Visible = true;
            GridAlumnos.Visible = false;
            txt_matricula.ReadOnly = true;
            btn_save.Visible = false;
            //btn_update.Visible = true;
            Global.cuenta = txt_matricula.Text;
            Global.nombre = HttpUtility.HtmlDecode(row.Cells[2].Text);
            Global.ap_paterno = HttpUtility.HtmlDecode(row.Cells[3].Text);
            Global.ap_materno = HttpUtility.HtmlDecode(row.Cells[4].Text);
            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            ConexionMySql.Open();
            MySqlDataAdapter sqladapter = new MySqlDataAdapter();

            //Verifica si existen registros guardados en relación de documentos para el el alumno
            string Querytredo = "select count(*) from tredo where tredo_tpers_num in (select tpers_num from tpers where tpers_id='" + txt_matricula.Text + "')";

            DataSet dssql2 = new DataSet();

            MySqlCommand commandsql2 = new MySqlCommand(Querytredo, ConexionMySql);
            sqladapter.SelectCommand = commandsql2;
            sqladapter.Fill(dssql2);
            sqladapter.Dispose();
            commandsql2.Dispose();
            if (dssql2.Tables[0].Rows[0][0].ToString() == "0")
            {
                //Inserta registros en tredo de acuerdo a la configuración de tcodo
                string Queryinserttredo = " select distinct tpers_num Id_Num,tadmi_tpees_clave periodo, " +
                 " tadmi_consecutivo consecutivo, tcodo_tdocu_clave clave_docto, tdocu_desc descripcion, 'PE' c_estatus " +
                 " from tpers inner join tadmi a on tadmi_tpers_num = tpers_num  " +
                 " inner join  tcodo on tcodo_tdocu_clave = tcodo_tdocu_clave " +
                 " inner join tdocu on tcodo_tdocu_clave = tdocu_clave and tdocu_estatus = 'A' " +
                 " inner join tprog on tprog_clave = tadmi_tprog_clave " +
                 " where tpers_id = '" + txt_matricula.Text + "'" +
                 " and(tcodo_tcamp_clave = tadmi_tcamp_clave or tcodo_tcamp_clave = '000') " +
                 " and(tcodo_tnive_clave = tprog_tnive_clave or tcodo_tnive_clave = '000') " +
                 " and(tcodo_tcole_clave = tprog_tcole_clave or tcodo_tcole_clave = '000') " +
                 " and(tcodo_tmoda_clave = tprog_tmoda_clave or tcodo_tmoda_clave = '000') " +
                 " and(tcodo_tprog_clave = tprog_clave or tcodo_tprog_clave = '0000000000') " +
                 " and(tcodo_ttiin_clave = tadmi_ttiin_clave or tcodo_ttiin_clave = '000') " +
                 " and tcodo_estatus = 'A' order by tadmi_consecutivo desc, tadmi_tpees_clave, tcodo_tdocu_clave ";

                DataSet dssql3 = new DataSet();

                MySqlCommand commandsql3 = new MySqlCommand(Queryinserttredo, ConexionMySql);
                sqladapter.SelectCommand = commandsql3;
                sqladapter.Fill(dssql3);
                sqladapter.Dispose();
                commandsql3.Dispose();
                String QueryInsert = "";
                for (int i = 0; i < dssql3.Tables[0].Rows.Count; i++)
                {
                    QueryInsert = "INSERT INTO tredo Values (" + dssql3.Tables[0].Rows[i][0].ToString() + ",'" + dssql3.Tables[0].Rows[i][1].ToString() + "'," +
                       dssql3.Tables[0].Rows[i][2].ToString() + ",'" + dssql3.Tables[0].Rows[i][3].ToString() + "','PE',null, null,'" + Session["usuario"].ToString() + "',current_timestamp(),'A') ";
                    MySqlCommand mysqlcmd = new MySqlCommand(QueryInsert, ConexionMySql);
                    mysqlcmd.CommandType = CommandType.Text;
                    try
                    {
                        mysqlcmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        string test = ex.Message;
                        string mensaje_error = ex.Message.Replace("'", "-");
                        Global.inserta_log(mensaje_error, "tredo", Session["usuario"].ToString());
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                    }
                }
            }
            ConexionMySql.Close();
            grid_documentos_bind(txt_matricula.Text);
        }

        protected void GridDoctos_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridDoctos.SelectedRow;
            txt_clave_doc.Text = row.Cells[1].Text;
            txt_documento.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);
            GridDoctos.Visible = false;

            txt_clave_doc.Attributes.Add("readonly", "");
            btn_save.Visible = true;
            //btn_update.Visible = true;
        }

        protected void consulta_docs_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Tcdoc.aspx");
        }

        protected void linkBttnBusca_Click(object sender, EventArgs e)
        {
            Carga_Estudiante();
        }

        protected void linkBttnCveDoc_Click(object sender, EventArgs e)
        {

            //ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopupDoctos", "$('#modalDoctos').modal('show')", true);

            //string QueryDoctos = "select tdocu_clave CLAVE, tdocu_desc DOCUMENTO from tdocu " +
            //        " where tdocu_estatus='A' and tdocu_clave not in (select tredo_tdocu_clave from tredo " +
            //        " where tredo_tpers_num in (select tpers_num from tpers where tpers_id='" + txt_matricula.Text + "'))";

            try
            {
                if (GridDoctos.Visible == false)
                {
                    GridDoctos.DataSource = serviceAlumno.ObtenerDocumentosDisponibles(txt_matricula.Text);
                    GridDoctos.DataBind();
                    GridDoctos.UseAccessibleHeader = true;

                    GridDoctos.Visible = true;
                }
                else
                {
                    GridDoctos.DataSource = null;
                    GridDoctos.DataBind();
                    GridDoctos.Visible = false;
                }
            }
            catch (Exception ex)
            {
                //logs
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tredo", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }

        }

        protected void linkBttnDoctos_Click(object sender, EventArgs e)
        {
            Response.Redirect("Tcdoc.aspx");

        }

        protected void btn_update_Click(object sender, EventArgs e)
        {
            try
            {
                //serviceDocto.EditarDoctos(txt_matricula.Text, ddl_);
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tredo", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }
    }

}