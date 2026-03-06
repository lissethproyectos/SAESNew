using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAES_v1;
using static SAES_DBO.Models.ModelMenu;
using SAES_Services;

namespace SAES_v1
{
    public partial class tcodo : System.Web.UI.Page
    {
        #region <Variables>
        AlumnoService serviceAlumno = new AlumnoService();
        CobranzaService serviceCobro = new CobranzaService();
        Catalogos serviceCatalogo = new Catalogos();
        MenuService servicePermiso = new MenuService();
        DocumentoService serviceDocto = new DocumentoService();
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
                    combo_campus();
                    combo_nivel();
                    combo_estatus();
                    combo_tipo();
                    combo_colegios();
                    combo_modalidad();
                    combo_programas();

                    LlenaPagina();

                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_tcodo", "load_datatable_tcodo();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_tdocu", "load_datatable_tdocu();", true);


            }
        }
        private void combo_tipo()
        {
            try
            {
                ddl_tipo.DataSource = serviceCatalogo.obtenTtiinActivos();
                ddl_tipo.DataValueField = "clave";
                ddl_tipo.DataTextField = "descripcion";
                ddl_tipo.DataBind();
                if (ddl_tipo.Items[0].Value == "")
                {
                    ddl_tipo.Items.RemoveAt(0);
                    ddl_tipo.Items.Insert(0, new ListItem("-------", "000"));
                }
            }
            catch (Exception ex)
            {
                //Logs
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcodo", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }
        private void combo_programas()
        {
            try
            {
                //ddl_programa.DataSource = serviceCatalogo.obtenProgramas();
                ddl_programa.DataSource = serviceCatalogo.obtenTcaprPrograma(ddl_campus.SelectedValue, ddl_nivel.SelectedValue, ddl_colegio.SelectedValue, ddl_modalidad.SelectedValue);

                
                ddl_programa.DataValueField = "Clave";
                ddl_programa.DataTextField = "Nombre";
                ddl_programa.DataBind();
                //if (ddl_programa.Items[0].Value == "")
                //{
                //    ddl_programa.Items.RemoveAt(0);
                //    ddl_programa.Items.Insert(0, new ListItem("-------", "0000000000"));
                //}
                //else
                //    ddl_programa.Items.Insert(0, new ListItem("-------", "0000000000"));

            }
            catch (Exception ex)
            {
                //Logs
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcodo", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }
        private void combo_modalidad()
        {
            try
            {
                //ddl_modalidad.DataSource = serviceCatalogo.obtenModalidadActivos();
                ddl_modalidad.DataSource = serviceCatalogo.obtenModalidadporPrograma(ddl_campus.SelectedValue, ddl_nivel.SelectedValue, ddl_colegio.SelectedValue);

                ddl_modalidad.DataValueField = "clave";
                ddl_modalidad.DataTextField = "nombre";
                ddl_modalidad.DataBind();
               

            }
            catch (Exception ex)
            {
                //Logs
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcodo", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }
        private void combo_campus()
        {
            try
            {
                ddl_campus.DataSource = serviceCatalogo.ObtenerCampusVigentes();
                ddl_campus.DataValueField = "clave";
                ddl_campus.DataTextField = "campus";
                ddl_campus.DataBind();
                if (ddl_campus.Items[0].Value == "")
                {
                    ddl_campus.Items.RemoveAt(0);
                    ddl_campus.Items.Insert(0, new ListItem("-------", "000"));
                }
            }
            catch (Exception ex)
            {
                //Logs
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcodo", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }
        private void combo_colegios()
        {
            try
            {
                //ddl_colegio.DataSource = serviceCatalogo.obtenColegiosActivos();
                ddl_colegio.DataSource = serviceCatalogo.obtenColegiosporPrograma(ddl_campus.SelectedValue, ddl_nivel.SelectedValue);

                ddl_colegio.DataValueField = "Clave";
                ddl_colegio.DataTextField = "Nombre";
                ddl_colegio.DataBind();
                //if (ddl_colegio.Items[0].Value == "")
                //{
                //    ddl_colegio.Items.RemoveAt(0);
                //    ddl_colegio.Items.Insert(0, new ListItem("-------", "000"));
                //}
            }
            catch (Exception ex)
            {
                //Logs
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcodo", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }
        private void combo_nivel()
        {
            try
            {
                //ddl_nivel.DataSource = serviceCatalogo.obtenNivelesActivos();
                //ddl_nivel.DataSource = serviceCatalogo.obtenNivelesporCampus(ddl_campus.SelectedValue.ToString());
                ddl_nivel.DataSource = serviceCatalogo.obtenNivel(ddl_campus.SelectedValue.ToString());

                ddl_nivel.DataValueField = "clave";
                ddl_nivel.DataTextField = "descripcion";
                ddl_nivel.DataBind();
                if (ddl_nivel.Items[0].Value == "" || ddl_nivel.Items.Count==0)
                {
                    ddl_nivel.Items.RemoveAt(0);
                    ddl_nivel.Items.Insert(0, new ListItem("-------", "000"));
                }
            }
            catch (Exception ex)
            {
                //Logs
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcodo", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
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
                objPermiso = servicePermiso.ObtenerPermisoFormulario(Session["usuario"].ToString(), "tcodo");
                if (objPermiso != null)
                {
                    if (objPermiso.usme_update == "0" || objPermiso.usme_select == "0")
                    {
                        btn_tcodo.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                    }
                    else
                        grid_tcodo_bind();
                }
                else
                {
                    btn_tcodo.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                }


            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcodo", Session["usuario"].ToString());
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

        protected void ddl_campus_SelectedIndexChanged(object sender, EventArgs e)
        {
            
                combo_nivel();

                combo_colegios();

                combo_modalidad();

                combo_programas();
        }

        protected void ddl_nivel_SelectedIndexChanged(object sender, EventArgs e)
        {
            string QueryColegio = "";
            QueryColegio = "select distinct tprog_tcole_clave clave, tcole_desc colegio from tcapr, tcole, tprog " +
                                " where tcapr_estatus='A' " +
                                " and tcapr_tprog_clave=tprog_clave and tprog_tcole_clave=tcole_clave ";
          

            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            ConexionMySql.Open();
            try
            {

                combo_colegios();

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcodo", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }

            string QueryModalidad = "";
            QueryModalidad = "select distinct tprog_tmoda_clave clave, tmoda_desc modalidad from tcapr, tmoda, tprog " +
                                " where tcapr_estatus='A' " +
                                " and tcapr_tprog_clave=tprog_clave and tprog_tmoda_clave=tmoda_clave ";
            if (ddl_campus.SelectedValue != "000")
            {
                QueryModalidad = QueryModalidad + " and tcapr_tcamp_clave = '" + ddl_campus.SelectedValue.ToString() + "'";
            }
            if (ddl_nivel.SelectedValue != "")
            {
                QueryModalidad = QueryModalidad + " and tprog_tnive_clave = '" + ddl_nivel.SelectedValue.ToString() + "'";
            }
            if (ddl_colegio.SelectedValue != "")
            {
                QueryModalidad = QueryModalidad + " and tprog_tcole_clave = '" + ddl_colegio.SelectedValue.ToString() + "'";
            }
            QueryModalidad = QueryModalidad + " union " +
                " select '000' clave, '---------' modalidad from dual ";

            QueryModalidad = QueryModalidad + " order by clave "; ;

            try
            {

                DataTable TablaModalidad = new DataTable();
                MySqlCommand ConsultaMySqlModa = new MySqlCommand();
                MySqlDataReader DatosMySqlModa;
                ConsultaMySqlModa.Connection = ConexionMySql;
                ConsultaMySqlModa.CommandType = CommandType.Text;
                ConsultaMySqlModa.CommandText = QueryModalidad;
                DatosMySqlModa = ConsultaMySqlModa.ExecuteReader();
                TablaModalidad.Load(DatosMySqlModa, LoadOption.OverwriteChanges);

                ddl_modalidad.DataSource = TablaModalidad;
                ddl_modalidad.DataValueField = "clave";
                ddl_modalidad.DataTextField = "modalidad";
                ddl_modalidad.DataBind();

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcodo", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }

            string QueryPrograma = "";
            QueryPrograma = "select distinct tprog_clave clave, tprog_desc programa from tcapr, tprog " +
                                " where tcapr_estatus='A' " +
                                " and tcapr_tprog_clave=tprog_clave ";
            if (ddl_campus.SelectedValue != "000")
            {
                QueryPrograma = QueryPrograma + " and tcapr_tcamp_clave = '" + ddl_campus.SelectedValue.ToString() + "'";
            }
            if (ddl_nivel.SelectedValue != "")
            {
                QueryPrograma = QueryPrograma + " and tprog_tnive_clave = '" + ddl_nivel.SelectedValue.ToString() + "'";
            }
            if (ddl_colegio.SelectedValue != "")
            {
                QueryPrograma = QueryPrograma + " and tprog_tcole_clave = '" + ddl_colegio.SelectedValue.ToString() + "'";
            }
            if (ddl_modalidad.SelectedValue != "")
            {
                QueryPrograma = QueryPrograma + " and tprog_tmoda_clave = '" + ddl_modalidad.SelectedValue.ToString() + "'";
            }
            QueryPrograma = QueryPrograma + " union " +
                " select '000' clave, '---------' programa from dual ";

            QueryPrograma = QueryPrograma + " order by clave "; ;

            try
            {

                DataTable TablaPrograma = new DataTable();
                MySqlCommand ConsultaMySqlProg = new MySqlCommand();
                MySqlDataReader DatosMySqlProg;
                ConsultaMySqlProg.Connection = ConexionMySql;
                ConsultaMySqlProg.CommandType = CommandType.Text;
                ConsultaMySqlProg.CommandText = QueryPrograma;
                DatosMySqlProg = ConsultaMySqlProg.ExecuteReader();
                TablaPrograma.Load(DatosMySqlProg, LoadOption.OverwriteChanges);

                ddl_programa.DataSource = TablaPrograma;
                ddl_programa.DataValueField = "clave";
                ddl_programa.DataTextField = "programa";
                ddl_programa.DataBind();

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcodo", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
            ConexionMySql.Close();

        }

        protected void ddl_colegio_SelectedIndexChanged(object sender, EventArgs e)
        {
            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            ConexionMySql.Open();


            string QueryModalidad = "";
            QueryModalidad = "select distinct tprog_tmoda_clave clave, tmoda_desc modalidad from tcapr, tmoda, tprog " +
                                " where tcapr_estatus='A' " +
                                " and tcapr_tprog_clave=tprog_clave and tprog_tmoda_clave=tmoda_clave ";
            if (ddl_campus.SelectedValue != "000")
            {
                QueryModalidad = QueryModalidad + " and tcapr_tcamp_clave = '" + ddl_campus.SelectedValue.ToString() + "'";
            }
            if (ddl_nivel.SelectedValue != "")
            {
                QueryModalidad = QueryModalidad + " and tprog_tnive_clave = '" + ddl_nivel.SelectedValue.ToString() + "'";
            }
            if (ddl_colegio.SelectedValue != "")
            {
                QueryModalidad = QueryModalidad + " and tprog_tcole_clave = '" + ddl_colegio.SelectedValue.ToString() + "'";
            }
            QueryModalidad = QueryModalidad + " union " +
                " select '000' clave, '---------' modalidad from dual ";

            QueryModalidad = QueryModalidad + " order by clave "; ;

            try
            {

                DataTable TablaModalidad = new DataTable();
                MySqlCommand ConsultaMySqlModa = new MySqlCommand();
                MySqlDataReader DatosMySqlModa;
                ConsultaMySqlModa.Connection = ConexionMySql;
                ConsultaMySqlModa.CommandType = CommandType.Text;
                ConsultaMySqlModa.CommandText = QueryModalidad;
                DatosMySqlModa = ConsultaMySqlModa.ExecuteReader();
                TablaModalidad.Load(DatosMySqlModa, LoadOption.OverwriteChanges);

                ddl_modalidad.DataSource = TablaModalidad;
                ddl_modalidad.DataValueField = "clave";
                ddl_modalidad.DataTextField = "modalidad";
                ddl_modalidad.DataBind();

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcodo", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }

            string QueryPrograma = "";
            QueryPrograma = "select distinct tprog_clave clave, tprog_desc programa from tcapr, tprog " +
                                " where tcapr_estatus='A' " +
                                " and tcapr_tprog_clave=tprog_clave ";
            if (ddl_campus.SelectedValue != "000")
            {
                QueryPrograma = QueryPrograma + " and tcapr_tcamp_clave = '" + ddl_campus.SelectedValue.ToString() + "'";
            }
            if (ddl_nivel.SelectedValue != "")
            {
                QueryPrograma = QueryPrograma + " and tprog_tnive_clave = '" + ddl_nivel.SelectedValue.ToString() + "'";
            }
            if (ddl_colegio.SelectedValue != "")
            {
                QueryPrograma = QueryPrograma + " and tprog_tcole_clave = '" + ddl_colegio.SelectedValue.ToString() + "'";
            }
            if (ddl_modalidad.SelectedValue != "")
            {
                QueryPrograma = QueryPrograma + " and tprog_tmoda_clave = '" + ddl_modalidad.SelectedValue.ToString() + "'";
            }
            QueryPrograma = QueryPrograma + " union " +
                " select '000' clave, '---------' programa from dual ";

            QueryPrograma = QueryPrograma + " order by clave "; ;

            try
            {

                DataTable TablaPrograma = new DataTable();
                MySqlCommand ConsultaMySqlProg = new MySqlCommand();
                MySqlDataReader DatosMySqlProg;
                ConsultaMySqlProg.Connection = ConexionMySql;
                ConsultaMySqlProg.CommandType = CommandType.Text;
                ConsultaMySqlProg.CommandText = QueryPrograma;
                DatosMySqlProg = ConsultaMySqlProg.ExecuteReader();
                TablaPrograma.Load(DatosMySqlProg, LoadOption.OverwriteChanges);

                ddl_programa.DataSource = TablaPrograma;
                ddl_programa.DataValueField = "clave";
                ddl_programa.DataTextField = "programa";
                ddl_programa.DataBind();

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcodo", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
            ConexionMySql.Close();

        }

        protected void ddl_modalidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            ConexionMySql.Open();

            string QueryPrograma = "";
            QueryPrograma = "select distinct tprog_clave clave, tprog_desc programa from tcapr, tprog " +
                                " where tcapr_estatus='A' " +
                                " and tcapr_tprog_clave=tprog_clave ";
            if (ddl_campus.SelectedValue != "000")
            {
                QueryPrograma = QueryPrograma + " and tcapr_tcamp_clave = '" + ddl_campus.SelectedValue.ToString() + "'";
            }
            if (ddl_nivel.SelectedValue != "")
            {
                QueryPrograma = QueryPrograma + " and tprog_tnive_clave = '" + ddl_nivel.SelectedValue.ToString() + "'";
            }
            if (ddl_colegio.SelectedValue != "")
            {
                QueryPrograma = QueryPrograma + " and tprog_tcole_clave = '" + ddl_colegio.SelectedValue.ToString() + "'";
            }
            if (ddl_modalidad.SelectedValue != "")
            {
                QueryPrograma = QueryPrograma + " and tprog_tmoda_clave = '" + ddl_modalidad.SelectedValue.ToString() + "'";
            }
            QueryPrograma = QueryPrograma + " union " +
                " select '000' clave, '---------' programa from dual ";

            QueryPrograma = QueryPrograma + " order by clave "; ;

            try
            {

                DataTable TablaPrograma = new DataTable();
                MySqlCommand ConsultaMySqlProg = new MySqlCommand();
                MySqlDataReader DatosMySqlProg;
                ConsultaMySqlProg.Connection = ConexionMySql;
                ConsultaMySqlProg.CommandType = CommandType.Text;
                ConsultaMySqlProg.CommandText = QueryPrograma;
                DatosMySqlProg = ConsultaMySqlProg.ExecuteReader();
                TablaPrograma.Load(DatosMySqlProg, LoadOption.OverwriteChanges);

                ddl_programa.DataSource = TablaPrograma;
                ddl_programa.DataValueField = "clave";
                ddl_programa.DataTextField = "programa";
                ddl_programa.DataBind();

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcodo", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
            ConexionMySql.Close();

        }



        //protected void grid_tcodo_bind()
        //{
        //    string strQuery = "";
        //    strQuery = " select tcodo_tdocu_clave clave, tdocu_desc nombre,  " +
        //      " tcodo_tcamp_clave Campus, tcodo_tnive_clave Nivel, tcodo_tcole_clave Colegio," +
        //      " tcodo_tmoda_clave Modalidad, tcodo_tprog_clave Programa, tcodo_ttiin_clave Tipo, " +
        //      " tcodo_estatus c_estatus,CASE WHEN tcodo_estatus = 'A' THEN 'ACTIVO' ELSE 'INACTIVO' END Estatus, fecha(date_format(tcodo_date,'%Y-%m-%d')) fecha " +
        //      " from tcodo, tdocu " +
        //      " where tcodo_tdocu_clave=tdocu_clave ";

        //    strQuery = strQuery + " order by clave ";

        //    MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
        //    conexion.Open();
        //    try
        //    {
        //        MySqlDataAdapter dataadapter = new MySqlDataAdapter(strQuery, conexion);
        //        DataSet ds = new DataSet();
        //        dataadapter.Fill(ds, "Tcodo");
        //        Gridtcodo.DataSource = ds;
        //        Gridtcodo.DataBind();
        //        Gridtcodo.DataMember = "Tcodo";
        //        Gridtcodo.HeaderRow.TableSection = TableRowSection.TableHeader;
        //        Gridtcodo.UseAccessibleHeader = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        //resultado.Text = ex.Message;
        //        string mensaje_error = ex.Message.Replace("'", "-");
        //        Global.inserta_log(mensaje_error, "tcodo", Session["usuario"].ToString());
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
        //    }
        //    conexion.Close();
        //}

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            txt_tcodo.Text = null;
            txtNombre.Text = null;
            txt_tcodo.ReadOnly = false;
            //LlenaPagina();
            //combo_estatus();
            btn_save.Visible = true;
            btn_update.Visible = false;
            btn_cancel.Visible = false;
            btn_search.Visible = true;

            ddl_campus.SelectedIndex= 0;
            ddl_campus_SelectedIndexChanged(null, null);
            ddl_tipo.SelectedIndex = -1;

            Gridtcodo.SelectedIndex = -1;
            //grid_tstdo_bind();
            //grid_tcodo_bind();

            //Gridtcodo.DataSource = null;
            //Gridtcodo.DataBind();
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txt_tcodo.Text) && !String.IsNullOrEmpty(txtNombre.Text))
            {
                if (valida_tcodo(txt_tcodo.Text, ddl_campus.SelectedValue, ddl_nivel.SelectedValue, ddl_colegio.SelectedValue,
                    ddl_modalidad.SelectedValue, ddl_programa.SelectedValue, ddl_tipo.SelectedValue))
                {
                    string strCadSQL = "INSERT INTO tcodo Values ('" + txt_tcodo.Text + "','" + ddl_campus.SelectedValue + "','" +
                    ddl_nivel.SelectedValue + "','" + ddl_colegio.SelectedValue + "','" + ddl_programa.SelectedValue + "','" +
                    ddl_modalidad.SelectedValue + "','" + ddl_tipo.SelectedValue + "','" +
                    Session["usuario"].ToString() + "',current_timestamp(),'" + ddl_estatus.SelectedValue + "')";
                    MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                    conexion.Open();
                    MySqlCommand mysqlcmd = new MySqlCommand(strCadSQL, conexion);
                    mysqlcmd.CommandType = CommandType.Text;
                    try
                    {
                        mysqlcmd.ExecuteNonQuery();
                        txt_tcodo.Text = null;
                        txtNombre.Text = null;
                        LlenaPagina();
                        combo_estatus();
                        //grid_tstdo_bind();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                    }
                    catch (Exception ex)
                    {
                        string test = ex.Message;
                        string mensaje_error = ex.Message.Replace("'", "-");
                        Global.inserta_log(mensaje_error, "tcodo", Session["usuario"].ToString());
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                    }
                    finally
                    {
                        conexion.Close();
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validarClave('ContentPlaceHolder1_txt_tcodo',1);", true);
                    //grid_tstdo_bind();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_tcodo();", true);
                //grid_tstdo_bind();
            }


        }

        protected void btn_update_Click(object sender, EventArgs e)
        {
            string test = ddl_campus.SelectedValue;
            if (!String.IsNullOrEmpty(txt_tcodo.Text) && !String.IsNullOrEmpty(txtNombre.Text))
            {
                string strCadSQL = "UPDATE tcodo SET tcodo_tcamp_clave='" + ddl_campus.SelectedValue + "',tcodo_tnive_clave='" +
                    ddl_nivel.SelectedValue + "', tcodo_tcole_clave='" + ddl_colegio.SelectedValue + "', tcodo_tmoda_clave='" +
                    ddl_modalidad.SelectedValue + "', tcodo_tprog_clave='" + ddl_programa.SelectedValue + "', tcodo_ttiin_clave='" +
                    ddl_tipo.SelectedValue + "', tcodo_estatus='" + ddl_estatus.SelectedValue + "', tcodo_user='" + Session["usuario"].ToString() + "', tcodo_date=CURRENT_TIMESTAMP() " +
                    " WHERE tcodo_tdocu_clave='" + txt_tcodo.Text + "' and tcodo_tcamp_clave='" + Global.campus + "'" +
                    " and tcodo_tnive_clave='" + Global.nivel + "' and tcodo_tcole_clave='" + Global.colegio + "'" +
                    " and tcodo_tmoda_clave='" + Global.modalidad + "' and tcodo_tprog_clave='" + Global.programa + "'" +
                    " and tcodo_ttiin_clave='" + Global.tipo_ing + "' and tcodo_estatus='" + Global.estatus + "'";
                MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                conexion.Open();
                MySqlCommand mysqlcmd = new MySqlCommand(strCadSQL, conexion);
                mysqlcmd.CommandType = CommandType.Text;
                try
                {
                    mysqlcmd.ExecuteNonQuery();
                    //serviceDocumento
                    //grid_tstdo_bind();
                    LlenaPagina();
                    combo_estatus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "update_p", "update();", true);
                    //Gridtcodo.Visible = false;
                    btn_update.Visible = false;
                    btn_save.Visible = true;
                    //LlenaPagina();
                }
                catch (Exception ex)
                {
                    string test1 = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tcodo", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                }
                finally
                {
                    conexion.Close();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_tcodo();", true);
            }
        }

        protected void grid_tcodo_bind()
        {
            Gridtcodo.DataSource = null;
            Gridtcodo.DataBind();

            try
            {
                Gridtcodo.DataSource = serviceDocto.obtenConfiguracionDoctos(txt_tcodo.Text, ddl_modalidad.SelectedValue,
                    ddl_programa.SelectedValue,
                    ddl_tipo.SelectedValue, ddl_nivel.SelectedValue, ddl_campus.SelectedValue, ddl_colegio.SelectedValue);
                Gridtcodo.DataBind();
                //        Gridtcodo.DataMember = "Tcodo";
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcodo", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }
        

        protected bool valida_tcodo(string tcodo, string campus, string nivel, string colegio, string modalidad, string programa, string tipo)
        {
            string Query = "";
            Query = "SELECT COUNT(*) Indicador FROM tcodo WHERE tcodo_tdocu_clave='" + tcodo + "' and tcodo_tcamp_clave='" + campus + "' " +
                " and tcodo_tnive_clave='" + nivel + "' and tcodo_tcole_clave='" + colegio + "' and tcodo_tmoda_clave='" + modalidad + "' " +
                " and tcodo_tprog_clave='" + programa + "' and tcodo_ttiin_clave='" + tipo + "'";
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

        protected void Gridtdocu_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = Gridtdocu.SelectedRow;
            txt_tcodo.Text = row.Cells[1].Text;
            txtNombre.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);
            divTipoDoctos.Visible = false;
            //Gridtdocu.Visible = false;
            grid_tcodo_bind();
            //btn_search_Click(null, null);
            //combo_estatus();
        }

        protected void Gridtcodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = Gridtcodo.SelectedRow;
            txt_tcodo.ReadOnly = true;
            txt_tcodo.Text = row.Cells[1].Text;
            txtNombre.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);

            combo_estatus();
            ddl_estatus.SelectedValue = row.Cells[9].Text;
            try
            {

                ddl_campus.SelectedValue = row.Cells[3].Text.ToString();
                combo_nivel();
                ddl_nivel.SelectedValue = row.Cells[4].Text.ToString();
                combo_colegios();
                ddl_colegio.SelectedValue = row.Cells[5].Text.ToString();
                combo_modalidad();
                if(ddl_modalidad.Items.Count>0) 
                    ddl_modalidad.SelectedValue = row.Cells[6].Text.ToString();

                combo_programas();

                if (ddl_programa.Items.Count > 0)
                    ddl_programa.SelectedValue = row.Cells[7].Text.ToString();

                combo_tipo();

                if (ddl_tipo.Items.Count > 0)
                    ddl_tipo.SelectedValue = row.Cells[8].Text.ToString();

                Global.campus = row.Cells[3].Text.ToString();
                Global.nivel = row.Cells[4].Text.ToString();
                Global.colegio = row.Cells[5].Text.ToString();
                Global.modalidad = row.Cells[6].Text.ToString();
                Global.programa = row.Cells[7].Text.ToString();
                Global.tipo_ing = row.Cells[8].Text.ToString();
                Global.estatus = row.Cells[9].Text.ToString();

                btn_update.Visible = true;
                btn_save.Visible = false;
                btn_search.Visible = false;
                btn_cancel.Visible = true;
                txt_tcodo.Attributes.Add("readonly", "");
                //grid_tcodo_bind();
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                ddl_campus.SelectedIndex = 0;
                ddl_campus_SelectedIndexChanged(null, null);
                ddl_tipo.SelectedIndex = -1;

                Gridtcodo.SelectedIndex = -1;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcodo", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void linkBttnBusca_Click(object sender, EventArgs e)
        {

            //string strQuery = "";
            //strQuery = " select tdocu_clave clave, tdocu_desc nombre " +
            //           " from tdocu ";


            try
            {
                if (divTipoDoctos.Visible == false)
                {
                    Gridtdocu.DataSource = serviceCatalogo.obtenCatDocumentos();
                    Gridtdocu.DataBind();
                    Gridtdocu.DataMember = "Documentos";
                    //Gridtdocu.Visible = true;
                    divTipoDoctos.Visible = true;

                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "showModalCatDoctos", "$('#modalCatDoctos').modal('show')", true);

                }
                else
                    divTipoDoctos.Visible = false;
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcodo", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
            //Gridtdocu.Visible = true;

        }

        protected void txt_tcodo_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btn_search_Click(object sender, EventArgs e)
        {
            grid_tcodo_bind();
        }
    }
}