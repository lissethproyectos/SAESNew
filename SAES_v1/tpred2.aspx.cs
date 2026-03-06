using MySql.Data.MySqlClient;
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

namespace SAES_v1
{
    public partial class tpred2 : System.Web.UI.Page
    {
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

                if (!IsPostBack)
                {
                    TxtCuenta.Text = Global.cuenta.ToString();
                    TxtNombre.Text = Global.nombre_alumno.ToString();
                    combo_estatus();
                    if (TxtCuenta.Text != "")
                    {
                        Carga_Prog();
                    }
                }

            }
        }

        protected void combo_estatus()
        {
            ddl_estatus.Items.Clear();
            ddl_estatus.Items.Add(new ListItem("------", "XX"));
            ddl_estatus.Items.Add(new ListItem("Predictamen", "PR"));
            ddl_estatus.Items.Add(new ListItem("Dictamen Oficial", "DO"));
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

        protected void Carga_Alumno(object sender, EventArgs e)
        {
            string strQuery = "";
            strQuery = " select concat(tpers_nombre,' ',tpers_paterno,' ',tpers_materno) nombre " +
                         " from tpers where tpers_id='" + TxtCuenta.Text + "' and tpers_tipo='E' ";

            //resultado.Text = strQuery;
            try
            {
                MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                conexion.Open();

                MySqlDataAdapter sqladapter = new MySqlDataAdapter();

                DataSet dssql1 = new DataSet();

                MySqlCommand commandsql1 = new MySqlCommand(strQuery, conexion);
                sqladapter.SelectCommand = commandsql1;
                sqladapter.Fill(dssql1);
                sqladapter.Dispose();
                commandsql1.Dispose();
                if (dssql1.Tables[0].Rows.Count > 0)
                {
                    TxtNombre.Text = dssql1.Tables[0].Rows[0][0].ToString();
                    Global.cuenta = TxtCuenta.Text;
                    Global.nombre_alumno = TxtNombre.Text;
                    lbl_Campus.Text = ""; lbl_Estatus.Text = "";
                    lbl_Nivel.Text = ""; lbl_Programa.Text = ""; lbl_Turno.Text = "";
                    txt_esc_proc.Text = "";
                    txt_nom_proc.Text = "";
                    txt_origen.Text = "";
                    txt_folio.Text = "";
                    txt_fecha_i.Text = "";
                    txt_periodo.Text = "";
                    txt_nom_per.Text = "";

                    ddl_estatus.Items.Clear();
                    ddl_estatus.Items.Add("------");
                    ddl_estatus.Items.Add(new ListItem("Predictamen", "PR"));
                    ddl_estatus.Items.Add(new ListItem("Dictamen Oficial", "DO"));
                    GridTpreq.Visible = false;
                    Carga_Prog();
                }
                else
                {
                    //TxtNombre.Text = "No existe Matrícula";
                }
                //Plan.Visible = false;
                //Preq.Visible = false;
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tpred", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        private void Carga_Alumno()
        {
            string strQuery = "";
            strQuery = " select concat(tpers_nombre,' ',tpers_paterno,' ',tpers_materno) nombre " +
                         " from tpers where tpers_id='" + TxtCuenta.Text + "' and tpers_tipo='E' ";

            //resultado.Text = strQuery;
            try
            {
                MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                conexion.Open();

                MySqlDataAdapter sqladapter = new MySqlDataAdapter();

                DataSet dssql1 = new DataSet();

                MySqlCommand commandsql1 = new MySqlCommand(strQuery, conexion);
                sqladapter.SelectCommand = commandsql1;
                sqladapter.Fill(dssql1);
                sqladapter.Dispose();
                commandsql1.Dispose();
                if (dssql1.Tables[0].Rows.Count > 0)
                {
                    TxtNombre.Text = dssql1.Tables[0].Rows[0][0].ToString();
                    Global.cuenta = TxtCuenta.Text;
                    Global.nombre_alumno = TxtNombre.Text;
                    lbl_Campus.Text = ""; lbl_Estatus.Text = "";
                    lbl_Nivel.Text = ""; lbl_Programa.Text = ""; lbl_Turno.Text = "";
                    txt_esc_proc.Text = "";
                    txt_nom_proc.Text = "";
                    txt_origen.Text = "";
                    txt_folio.Text = "";
                    txt_fecha_i.Text = "";
                    txt_periodo.Text = "";
                    txt_nom_per.Text = "";

                    ddl_estatus.Items.Clear();
                    ddl_estatus.Items.Add("------");
                    ddl_estatus.Items.Add(new ListItem("Predictamen", "PR"));
                    ddl_estatus.Items.Add(new ListItem("Dictamen Oficial", "DO"));
                    GridTpreq.Visible = false;
                    Carga_Prog();
                }
                else
                {
                    //TxtNombre.Text = "No existe Matrícula";
                }
                //Plan.Visible = false;
                //Preq.Visible = false;
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tpred", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void Busqueda_Alumnos(object sender, EventArgs e)
        {
            if (GridAlumnos.Visible == true)
            {
                GridAlumnos.Visible = false;
            }
            else
            {
                GridAlumnos.Visible = true;
                string strQueryAlumnos = "";
                strQueryAlumnos = " select Matricula, Alumno " +
                    " from (SELECT tpers_id matricula, concat(tpers_nombre,' ',tpers_paterno,' ',tpers_materno) alumno  from tpers where tpers_tipo='E') datos";
                strQueryAlumnos = strQueryAlumnos + " order  by matricula ";
                try
                {
                    MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                    conexion.Open();
                    MySqlDataAdapter dataadapter = new MySqlDataAdapter(strQueryAlumnos, conexion);
                    DataSet ds = new DataSet();
                    dataadapter.Fill(ds, "Alumnos");
                    GridAlumnos.DataSource = ds;
                    GridAlumnos.DataBind();
                    GridAlumnos.DataMember = "Alumnos";
                    GridAlumnos.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridAlumnos.UseAccessibleHeader = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_Alumnos", "load_datatable_Alumnos();", true);
                    conexion.Close();
                }
                catch (Exception ex)
                {
                    //resultado.Text = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tpred", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                }
            }
        }

        protected void Busqueda_Escuelas(object sender, EventArgs e)
        {
            if (GridEscuelas.Visible == true)
            {
                GridEscuelas.Visible = false;
            }
            else
            {
                GridEscuelas.Visible = true;
                string strQueryEscuelas = "";
                strQueryEscuelas = " SELECT tespr_clave Clave, tespr_desc Escuela  from tespr " +
                    " order  by Clave ";
                try
                {
                    MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                    conexion.Open();
                    MySqlDataAdapter dataadapter = new MySqlDataAdapter(strQueryEscuelas, conexion);
                    DataSet ds = new DataSet();
                    dataadapter.Fill(ds, "Escuelas");
                    GridEscuelas.DataSource = ds;
                    GridEscuelas.DataBind();
                    GridEscuelas.DataMember = "Escuelas";
                    GridEscuelas.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridEscuelas.UseAccessibleHeader = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_Escuelas", "load_datatable_Escuelas();", true);
                    conexion.Close();
                }
                catch (Exception ex)
                {
                    //resultado.Text = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tpred", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                }
            }
        }

        protected void Busqueda_Periodos(object sender, EventArgs e)
        {
            if (GridPeriodos.Visible == true)
            {
                GridPeriodos.Visible = false;
            }
            else
            {
                GridPeriodos.Visible = true;
                string strQueryPeriodos = "";
                strQueryPeriodos = " SELECT tpees_clave Clave, tpees_desc Periodo  from tpees " +
                    " order  by Clave desc";
                try
                {
                    MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                    conexion.Open();
                    MySqlDataAdapter dataadapter = new MySqlDataAdapter(strQueryPeriodos, conexion);
                    DataSet ds = new DataSet();
                    dataadapter.Fill(ds, "Periodos");
                    GridPeriodos.DataSource = ds;
                    GridPeriodos.DataBind();
                    GridPeriodos.DataMember = "Periodos";
                    GridPeriodos.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridPeriodos.UseAccessibleHeader = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_Periodos", "load_datatable_Periodos();", true);
                    conexion.Close();
                }
                catch (Exception ex)
                {
                    //resultado.Text = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tpred", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                }
            }
        }

        protected void GridAlumnos_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridAlumnos.SelectedRow;
            TxtCuenta.Text = row.Cells[1].Text;
            TxtNombre.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);
            GridAlumnos.Visible = false;
            Global.cuenta = TxtCuenta.Text;
            Global.nombre_alumno = TxtNombre.Text;
            lbl_Campus.Text = ""; lbl_Estatus.Text = "";
            lbl_Nivel.Text = ""; lbl_Programa.Text = ""; lbl_Turno.Text = "";
            txt_esc_proc.Text = "";
            txt_nom_proc.Text = "";
            txt_origen.Text = "";
            txt_folio.Text = "";
            txt_fecha_i.Text = "";
            txt_periodo.Text = "";
            txt_nom_per.Text = "";

            ddl_estatus.Items.Clear();
            ddl_estatus.Items.Add("------");
            ddl_estatus.Items.Add(new ListItem("Predictamen", "PR"));
            ddl_estatus.Items.Add(new ListItem("Dictamen Oficial", "DO"));
            GridTpreq.Visible = false;
            Carga_Prog();
        }


        private void Carga_Prog()
        {
            MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            conexion.Open();

            lbl_Campus.Text = "";
            lbl_Nivel.Text = "";
            lbl_Programa.Text = "";
            lbl_Estatus.Text = "";
            lbl_Turno.Text = "";
            GridProgramas.Visible = true;
            string strQueryProgramas = "";
            strQueryProgramas = " select a.testu_tcamp_clave campus, x.tprog_tnive_clave nivel ,a.testu_tprog_clave clave, x.tprog_desc programa , " +
                 " a.testu_tstal_clave estatus, a.testu_turno turno, a.testu_tpees_clave periodo " +
                 " from testu a, tpers, tprog x " +
                 " where tpers_id = '" + TxtCuenta.Text + "' and a.testu_tpers_num = tpers_num and x.tprog_clave = a.testu_tprog_clave " +
                 " and a.testu_tpees_clave in (select max(testu_tpees_clave) from testu b, tprog z " +
                 "           where a.testu_tpers_num = b.testu_tpers_num and a.testu_tcamp_clave = b.testu_tcamp_clave " +
                 "           and a.testu_tprog_clave = b.testu_tprog_clave and x.tprog_clave = z.tprog_clave " +
                 "           and x.tprog_tnive_clave = z.tprog_tnive_clave)  order by periodo";


            //resultado.Text = "1--"+ strQueryAlumnos;
            try
            {
                MySqlDataAdapter sqladapter = new MySqlDataAdapter();

                DataSet dssql1 = new DataSet();

                MySqlCommand commandsql1 = new MySqlCommand(strQueryProgramas, conexion);
                sqladapter.SelectCommand = commandsql1;
                sqladapter.Fill(dssql1);
                sqladapter.Dispose();
                commandsql1.Dispose();

                //MySqlDataAdapter dataadapter = new MySqlDataAdapter(strQueryProgramas, conexion);
                //DataSet dsprog = new DataSet();

                if (dssql1.Tables[0].Rows.Count == 0)
                {
                    strQueryProgramas = " select a.tadmi_tcamp_clave campus, x.tprog_tnive_clave nivel ,a.tadmi_tprog_clave clave, x.tprog_desc programa , " +
                     " 'PR' estatus, a.tadmi_turno turno, a.tadmi_tpees_clave periodo " +
                     " from tadmi a, tpers, tprog x " +
                     " where tpers_id = '" + TxtCuenta.Text + "' and a.tadmi_tpers_num = tpers_num and x.tprog_clave = a.tadmi_tprog_clave " +
                     " and a.tadmi_tpees_clave in (select max(tadmi_tpees_clave) from tadmi b, tprog z " +
                     "           where a.tadmi_tpers_num = b.tadmi_tpers_num and a.tadmi_tcamp_clave = b.tadmi_tcamp_clave " +
                     "           and a.tadmi_tprog_clave = b.tadmi_tprog_clave and x.tprog_clave = z.tprog_clave " +
                     "           and x.tprog_tnive_clave = z.tprog_tnive_clave)  order by periodo";
                    DataSet dsadmi = new DataSet();
                    MySqlDataAdapter dataadapteradmi = new MySqlDataAdapter(strQueryProgramas, conexion);
                    dataadapteradmi.Fill(dsadmi, "Cuenta");
                    GridProgramas.DataSource = dsadmi;
                    GridProgramas.DataBind();
                    GridProgramas.DataMember = "Cuenta";
                    GridAlumnos.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridAlumnos.UseAccessibleHeader = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_Programas", "load_datatable_Programas();", true);
                }
                else
                {
                    strQueryProgramas = " select a.testu_tcamp_clave campus, x.tprog_tnive_clave nivel ,a.testu_tprog_clave clave, x.tprog_desc programa , " +
                  " a.testu_tstal_clave estatus, a.testu_turno turno, a.testu_tpees_clave periodo " +
                  " from testu a, tpers, tprog x " +
                  " where tpers_id = '" + TxtCuenta.Text + "' and a.testu_tpers_num = tpers_num and x.tprog_clave = a.testu_tprog_clave " +
                  " and a.testu_tpees_clave in (select max(testu_tpees_clave) from testu b, tprog z " +
                  "           where a.testu_tpers_num = b.testu_tpers_num and a.testu_tcamp_clave = b.testu_tcamp_clave " +
                  "           and a.testu_tprog_clave = b.testu_tprog_clave and x.tprog_clave = z.tprog_clave " +
                  "           and x.tprog_tnive_clave = z.tprog_tnive_clave)  order by periodo";
                    DataSet dsadmi = new DataSet();
                    MySqlDataAdapter dataadapteradmi = new MySqlDataAdapter(strQueryProgramas, conexion);
                    dataadapteradmi.Fill(dsadmi, "Cuenta");
                    GridProgramas.DataSource = dsadmi;
                    GridProgramas.DataBind();
                    GridProgramas.DataMember = "Cuenta";
                    //   GridAlumnos.HeaderRow.TableSection = TableRowSection.TableHeader;
                    //   GridAlumnos.UseAccessibleHeader = true;
                    //   ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_Programas", "load_datatable_Programas();", true);
                }
                GridProgramas.Visible = true;
                btn_save.Visible = true;
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message + "---" + strQueryProgramas;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tpred", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
            conexion.Close();

        }

        protected void GridProgramas_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridProgramas.SelectedRow;
            string Query = " select tcamp_desc, tnive_desc, " +
                " tstal_desc estatus, " +
                " case when '" + row.Cells[6].Text + "'='M' then 'MATUTINO' else 'VESPERTINO' end turno " +
            " from tcamp, tnive, tstal " +
            " where tcamp_clave='" + row.Cells[1].Text + "' and tnive_clave='" + row.Cells[2].Text + "' " +
            " and   tstal_clave='" + row.Cells[5].Text + "'";

            MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            conexion.Open();
            try
            {
                MySqlDataAdapter sqladapter = new MySqlDataAdapter();

                DataSet dssql1 = new DataSet();

                MySqlCommand commandsql1 = new MySqlCommand(Query, conexion);
                sqladapter.SelectCommand = commandsql1;
                sqladapter.Fill(dssql1);
                sqladapter.Dispose();
                commandsql1.Dispose();

                lbl_Campus.Text = dssql1.Tables[0].Rows[0][0].ToString();
                lbl_Nivel.Text = dssql1.Tables[0].Rows[0][1].ToString();
                lbl_Programa.Text = HttpUtility.HtmlDecode(row.Cells[4].Text);
                Global.programa = row.Cells[3].Text;
                lbl_Estatus.Text = dssql1.Tables[0].Rows[0][2].ToString();
                lbl_Turno.Text = dssql1.Tables[0].Rows[0][3].ToString();
                GridProgramas.Visible = false;
                conexion.Close();
                Carga_Tpreq();
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tpred", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void GridEscuelas_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridEscuelas.SelectedRow;
            txt_esc_proc.Text = row.Cells[1].Text;
            txt_nom_proc.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);
            GridEscuelas.Visible = false;

        }

        protected void GridPeriodos_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridPeriodos.SelectedRow;
            txt_periodo.Text = row.Cells[1].Text;
            txt_nom_per.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);
            GridPeriodos.Visible = false;

        }

        private void Carga_Tpreq()
        {
            try
            {
                MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                conexion.Open();
                MySqlDataAdapter sqladapter = new MySqlDataAdapter();

                string QerySelect = "select tusme_update from tuser, tusme " +
                              " where tuser_clave = '" + Session["usuario"].ToString() + "'" +
                              " and tusme_trole_clave = tuser_trole_clave and tusme_tmenu_clave = 4 and tusme_tmede_clave = 4 ";

                DataSet dssql1 = new DataSet();

                MySqlCommand commandsql1 = new MySqlCommand(QerySelect, conexion);
                sqladapter.SelectCommand = commandsql1;
                sqladapter.Fill(dssql1);
                sqladapter.Dispose();
                commandsql1.Dispose();

                if (dssql1.Tables[0].Rows[0][0].ToString() == "1")
                {
                    //CmdGuardar.Visible = true;
                }

                string QeryTpreq = "select tpreq_tespr_clave clave, tespr_desc Escuela, tpreq_carrera carrera, tpreq_folio folio, " +
                   " fecha(date_format(tpreq_fecha_dict,'%Y-%m-%d')) fecha_dict , tpreq_estatus estatus ,fecha(date_format(tpreq_date,'%Y-%m-%d')) fecha " +
                   " from tpreq, tespr " +
                   " where tpreq_tpers_num=(select tpers_num from tpers where tpers_id='" + TxtCuenta.Text + "')" +
                   " and tpreq_tprog_clave='" + Global.programa.ToString() + "' and tespr_clave=tpreq_tespr_clave ";

                //resultado.Text = "1--" + QeryTpreq; // + "----" + strQueryAdmision;
                DataSet ds = new DataSet();
                MySqlDataAdapter dataadapter1 = new MySqlDataAdapter(QeryTpreq, conexion);
                dataadapter1.Fill(ds, "Preq");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridTpreq.DataSource = ds;
                    GridTpreq.DataBind();
                    GridTpreq.DataMember = "Preq";
                    GridTpreq.Visible = true;
                    Global.cuenta = TxtCuenta.Text;
                    Global.nombre_alumno = TxtNombre.Text;
                }
                conexion.Close();
                TxtCuenta.Focus();
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tpred", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }

        }

        protected void GridTpreq_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*try
            {*/
            GridViewRow row = GridTpreq.SelectedRow;

            string strQuery = "";
            strQuery = "select  tespr_desc nombre, tpreq_carrera carrera, tpreq_folio folio, " +
               " date_format(tpreq_fecha_dict,'%d/%m/%Y') fecha_dict , tpreq_tpees_clave, tpees_desc, tpreq_estatus " +
               " from tpreq, tespr, tpees " +
               " where tpreq_tpers_num=(select tpers_num from tpers where tpers_id='" + TxtCuenta.Text + "')" +
               " and tpreq_tprog_clave='" + Global.programa.ToString() + "' and tespr_clave=tpreq_tespr_clave " +
               " and tpreq_tespr_clave='" + row.Cells[1].Text + "' and tpees_clave=tpreq_tpees_clave ";

            //resultado.Text = strQuery;
            MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            conexion.Open();
            try
            {

                MySqlDataAdapter sqladapter = new MySqlDataAdapter();

                DataSet dssql1 = new DataSet();

                MySqlCommand commandsql1 = new MySqlCommand(strQuery, conexion);
                sqladapter.SelectCommand = commandsql1;
                sqladapter.Fill(dssql1);
                sqladapter.Dispose();
                commandsql1.Dispose();


                txt_esc_proc.Text = row.Cells[1].Text;
                txt_nom_proc.Text = dssql1.Tables[0].Rows[0][0].ToString();
                txt_origen.Text = dssql1.Tables[0].Rows[0][1].ToString();
                txt_folio.Text = dssql1.Tables[0].Rows[0][2].ToString();
                txt_fecha_i.Text = dssql1.Tables[0].Rows[0][3].ToString();
                txt_periodo.Text = dssql1.Tables[0].Rows[0][4].ToString();
                txt_nom_per.Text = dssql1.Tables[0].Rows[0][5].ToString();

                ddl_estatus.Items.Clear();
                ddl_estatus.Items.Add("------");
                ddl_estatus.Items.Add(new ListItem("Predictamen", "PR"));
                ddl_estatus.Items.Add(new ListItem("Dictamen Oficial", "DO"));


                ddl_estatus.Items.FindByValue(dssql1.Tables[0].Rows[0][6].ToString()).Selected = true;
                Global.escuela = row.Cells[1].Text;


                GridTpreq.Visible = false;
                Carga_Tpred();
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tpred", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }

            /*
            string strPred = "";
            strPred = "select  count(*) from tpred" +
               " where tpred_tpers_num=(select tpers_num from tpers where tpers_id='" + TxtCuenta.Text + "')" +
               " and tpred_tprog_clave='" + BaseDatos.programa.ToString() + "'";

            //resultado.Text = strQuery;

            DataSet ds = new DataSet();

            MySqlCommand command = new MySqlCommand(strPred, conexion);
            sqladapter.SelectCommand = command;
            sqladapter.Fill(ds);
            sqladapter.Dispose();
            command.Dispose();


            TxtEscuelas.Text = dssql1.Tables[0].Rows[0][0].ToString();

            string strQueryCuenta = "";

            strQueryCuenta = " select tplan_consecutivo consec, tplan_tarea_clave area, tplan_tmate_clave clave, tmate_desc materia , " +
                " tpred_mate_origen origen, tpred_tcali_clave cali, " +
                "(select tpred_mate_origen from tpred a where tpred_tprog_clave = '" + BaseDatos.programa.ToString() + "'" +
                "  and tpred_tmate_clave = tplan_tmate_clave and tpred_tespr_clave='" + TxtEspr.Text + "'" +
                " and tpred_consecutivo in (select max(tpred_consecutivo) from tpred b " +
                " where a.tpred_tprog_clave = b.tpred_tprog_clave and a.tpred_tmate_clave = b.tpred_tmate_clave " +
                " and   a.tpred_tespr_clave=b.tpred_tespr_clave)) sugerida, tpred_estatus estatus " +
                " from tpers " +
                " inner join tplan on tplan_tprog_clave='" + BaseDatos.programa.ToString() + "' " +
                " inner join tmate on tmate_clave = tplan_tmate_clave " +
                " left outer join tpred on tpred_tpers_num = tpers_num and tpred_tprog_clave = '" + BaseDatos.programa.ToString() + "' " +
                " and tplan_tmate_clave=tpred_tmate_clave " +
                " where  tpers_id = '" + TxtCuenta.Text + "' " +
                " order by area, consec ";
            //resultado.Text = strQueryCuenta;
            DataSet ds1 = new DataSet();
            MySqlDataAdapter dataadapter2 = new MySqlDataAdapter(strQueryCuenta, conexion);
            dataadapter2.Fill(ds1, "Plan");
            Plan.DataSource = ds1;
            Plan.DataBind();
            Plan.DataMember = "Plan";

            for (int i = 0; i < Plan.Rows.Count; i++)
            {

                TextBox orig = (TextBox)Plan.Rows[i].FindControl("mat_origen");
                if (ds1.Tables[0].Rows[i][4].ToString() == "")
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        orig.Text = ds1.Tables[0].Rows[i][6].ToString();
                    }
                }
                else
                {
                    orig.Text = ds1.Tables[0].Rows[i][4].ToString();
                }
                DropDownList calif = (DropDownList)Plan.Rows[i].FindControl("CboCalif");
                DropDownList St = (DropDownList)Plan.Rows[i].FindControl("CboSt");

                string strQuerycali = "";
                strQuerycali = " select tcali_clave cali, tcali_puntos puntos from tprog, tcali " +
                    " where tprog_clave='" + BaseDatos.programa.ToString() + "' and tcali_tnive_clave=tprog_tnive_clave " +
                                 " union " +
                                 " select '---' cali, 0 puntos from dual " +
                                 " order by puntos ";


                DataTable TablaPrograma = new DataTable();
                MySqlCommand ConsultaMySql2 = new MySqlCommand();
                MySqlDataReader DatosMySql2;
                ConsultaMySql2.Connection = conexion;
                ConsultaMySql2.CommandType = CommandType.Text;
                ConsultaMySql2.CommandText = strQuerycali;
                DatosMySql2 = ConsultaMySql2.ExecuteReader();
                TablaPrograma.Load(DatosMySql2, LoadOption.OverwriteChanges);

                calif.DataSource = TablaPrograma;
                calif.DataValueField = "cali";
                calif.DataBind();
                if (ds1.Tables[0].Rows[i][5].ToString() != "")
                {
                    calif.Items.FindByValue(ds1.Tables[0].Rows[i][5].ToString()).Selected = true;
                }

                St.Items.Clear();
                St.Items.Add("----");
                St.Items.Add("Activo");
                St.Items.Add("Baja");

                if (ds1.Tables[0].Rows[i][7].ToString() == "A")
                {
                    St.Items.FindByValue("Activo").Selected = true;
                }
                if (ds1.Tables[0].Rows[i][7].ToString() == "B")
                {
                    St.Items.FindByValue("Baja").Selected = true;
                }

                // TextBox calificacion = (TextBox)Plan.Rows[i].FindControl("calif");
                // calificacion.Text = ds1.Tables[0].Rows[i][5].ToString();

            }
            //resultado.Text = resultado.Text + "-" + importe.Text;
            Plan.Visible = true;

            conexion.Close();

        }
        catch (Exception ex)
        {
            //resultado.Text = ex.Message;
        }
        */
        }

        private void Carga_Tpred()
        {
            string strQueryCuenta = "";
            /*  if (CmdGuardar.Visible == true)
              {
                  Imgagregar.Visible = true;
              }
              */
            string strPred = "";
            strPred = "select  count(*) from tpred" +
               " where tpred_tpers_num=(select tpers_num from tpers where tpers_id='" + TxtCuenta.Text + "')" +
               " and tpred_tprog_clave='" + Global.programa.ToString() + "'";

            //resultado.Text = strQuery;
            MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            conexion.Open();
            //resultado.Text = strQueryCuenta;
            MySqlDataAdapter sqladapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(strPred, conexion);
            DataSet ds = new DataSet();
            sqladapter.SelectCommand = command;
            sqladapter.Fill(ds);
            sqladapter.Dispose();
            command.Dispose();

            strQueryCuenta = " select tplan_consecutivo consec, tplan_tarea_clave area, tplan_tmate_clave clave, tmate_desc materia , " +
                   " tpred_mate_origen origen, tpred_tcali_clave cali, " +
                   "(select tpred_mate_origen from tpred a where tpred_tprog_clave = '" + Global.programa.ToString() + "'" +
                   "  and tpred_tmate_clave = tplan_tmate_clave and tpred_tespr_clave='" + txt_esc_proc.Text + "'" +
                   " and tpred_consecutivo in (select max(tpred_consecutivo) from tpred b " +
                   " where a.tpred_tprog_clave = b.tpred_tprog_clave and a.tpred_tmate_clave = b.tpred_tmate_clave " +
                   " and   a.tpred_tespr_clave=b.tpred_tespr_clave)) sugerida, tpred_estatus " +
                   " from tpers " +
                   " inner join tplan on tplan_tprog_clave='" + Global.programa.ToString() + "' " +
                   " inner join tmate on tmate_clave = tplan_tmate_clave " +
                   " left outer join tpred on tpred_tpers_num = tpers_num and tpred_tprog_clave = '" + Global.programa.ToString() + "' " +
                   " and tplan_tmate_clave=tpred_tmate_clave " +
                   " where  tpers_id = '" + TxtCuenta.Text + "' " +
                   " order by area, consec ";
            try
            {
                //resultado.Text = strQueryCuenta;
                DataSet ds1 = new DataSet();
                MySqlDataAdapter dataadapter2 = new MySqlDataAdapter(strQueryCuenta, conexion);
                dataadapter2.Fill(ds1, "Plan");
                Plan.DataSource = ds1;
                Plan.DataBind();
                Plan.DataMember = "Plan";
                Plan.HeaderRow.TableSection = TableRowSection.TableHeader;
                Plan.UseAccessibleHeader = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_Plan", "load_datatable_Plan();", true);
                string cal = "";
                for (int i = 0; i < Plan.Rows.Count; i++)
                {
                    TextBox orig = (TextBox)Plan.Rows[i].FindControl("mat_origen");
                    if (ds1.Tables[0].Rows[i][4].ToString() == "")
                    {
                        if (ds.Tables[0].Rows[0][0].ToString() == "0")
                        {
                            orig.Text = ds1.Tables[0].Rows[i][6].ToString();
                        }
                    }
                    else
                    {
                        orig.Text = ds1.Tables[0].Rows[i][4].ToString();
                    }
                    DropDownList calif = (DropDownList)Plan.Rows[i].FindControl("CboCalif");
                    DropDownList St = (DropDownList)Plan.Rows[i].FindControl("CboSt");

                    string strQuerycali = "";
                    strQuerycali = " select tcali_clave cali, tcali_puntos puntos from tprog, tcali " +
                        " where tprog_clave='" + Global.programa.ToString() + "' and tcali_tnive_clave=tprog_tnive_clave " +
                                     " union " +
                                     " select '---' cali, 0 puntos from dual " +
                                     " order by puntos, cali ";


                    DataTable TablaPrograma = new DataTable();
                    MySqlCommand ConsultaMySql2 = new MySqlCommand();
                    MySqlDataReader DatosMySql2;
                    ConsultaMySql2.Connection = conexion;
                    ConsultaMySql2.CommandType = CommandType.Text;
                    ConsultaMySql2.CommandText = strQuerycali;
                    DatosMySql2 = ConsultaMySql2.ExecuteReader();
                    TablaPrograma.Load(DatosMySql2, LoadOption.OverwriteChanges);

                    calif.DataSource = TablaPrograma;
                    calif.DataValueField = "cali";
                    calif.DataBind();
                    cal = ds1.Tables[0].Rows[i][5].ToString();
                    if (ds1.Tables[0].Rows[i][5].ToString() != "")
                    {
                        calif.Items.FindByValue(ds1.Tables[0].Rows[i][5].ToString()).Selected = true;
                    }
                    St.Items.Clear();
                    St.Items.Add("----");
                    St.Items.Add("Activo");
                    St.Items.Add("Baja");

                    if (ds1.Tables[0].Rows[i][7].ToString() == "A")
                    {
                        St.Items.FindByValue("Activo").Selected = true;
                    }
                    if (ds1.Tables[0].Rows[i][7].ToString() == "B")
                    {
                        St.Items.FindByValue("Baja").Selected = true;
                    }
                    // TextBox calificacion = (TextBox)Plan.Rows[i].FindControl("calif");
                    // calificacion.Text = ds1.Tables[0].Rows[i][5].ToString();

                }
                //resultado.Text = resultado.Text + "-" + importe.Text;
                Plan.Visible = true;
                btn_tpred.Visible = true;
                btn_pdf.Visible = true;


                conexion.Close();
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tpred", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }


        protected void btn_save_Click(object sender, EventArgs e)
        {


            if (!String.IsNullOrEmpty(TxtCuenta.Text) && !String.IsNullOrEmpty(txt_esc_proc.Text) && !String.IsNullOrEmpty(txt_fecha_i.Text) && !String.IsNullOrEmpty(txt_origen.Text) && !String.IsNullOrEmpty(txt_folio.Text) && !String.IsNullOrEmpty(txt_periodo.Text))
            {

                string fecha_i_string = txt_fecha_i.Text;
                string format = "dd/MM/yyyy";

                DateTime fecha_inicio = DateTime.ParseExact(fecha_i_string, format, CultureInfo.InvariantCulture);

                /*string strCadSQL = "INSERT INTO tpees Values ('" + txt_periodo.Text + "','" + txt_nombre.Text + "','" +
            txt_oficial.Text + "', STR_TO_DATE('" + string.Format(txt_fecha_i.Text, "dd/MM/yyyy") + "','%d/%m/%Y'), STR_TO_DATE('" + string.Format(txt_fecha_f.Text, "dd/MM/yyyy") + "','%d/%m/%Y'),'" +
            Session["usuario"].ToString() + "',current_timestamp(),'" + ddl_estatus.SelectedValue + "')";*/

                string strBorra = "DELETE from tpreq where tpreq_tpers_num=(select tpers_num from tpers where tpers_id='" + TxtCuenta.Text + "') " +
                " and tpreq_tprog_clave='" + Global.programa.ToString() + "' and tpreq_tespr_clave='" + Global.escuela.ToString() + "' ";

                string strCadSQL =
                    "insert into tpreq values((select tpers_num from tpers where tpers_id='" + TxtCuenta.Text + "'),'" +
                    Global.programa.ToString() + "','" + txt_esc_proc.Text + "','" + txt_origen.Text + "','" + txt_periodo.Text + "','" +
                    txt_folio.Text + "', STR_TO_DATE('" + string.Format(txt_fecha_i.Text, "dd/MM/yyyy") + "','%d/%m/%Y'), '" + ddl_estatus.SelectedValue + "',current_timestamp(), '" +
                    Session["usuario"].ToString() + "')";
                MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                conexion.Open();
                MySqlCommand mysqlcmd = new MySqlCommand(strCadSQL, conexion);
                mysqlcmd.CommandType = CommandType.Text;
                try
                {
                    MySqlCommand myCommandborra = new MySqlCommand(strBorra, conexion);
                    //Ejecucion del comando en el servidor de BD
                    myCommandborra.ExecuteNonQuery();
                    mysqlcmd.ExecuteNonQuery();
                    Carga_Tpreq();
                    txt_esc_proc.Text = "";
                    txt_nom_proc.Text = "";
                    txt_origen.Text = "";
                    txt_folio.Text = "";
                    txt_fecha_i.Text = "";
                    txt_periodo.Text = "";
                    txt_nom_per.Text = "";

                    ddl_estatus.Items.Clear();
                    ddl_estatus.Items.Add("------");
                    ddl_estatus.Items.Add(new ListItem("Predictamen", "PR"));
                    ddl_estatus.Items.Add(new ListItem("Dictamen Oficial", "DO"));
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                    btn_save.Visible = false;
                    Carga_Tpreq();
                }
                catch (Exception ex)
                {
                    string test = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tpred", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                }
                finally
                {
                    conexion.Close();
                }
            }
            else
            {
                //grid_periodo_bind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_tpreq();", true);
            }


        }

        protected void Agregar_Materias(object sender, EventArgs e)
        {

            try
            {
                double err = 0;
                for (int i = 0; i < Plan.Rows.Count; i++)
                {
                    TextBox materia = (TextBox)Plan.Rows[i].FindControl("mat_origen");
                    DropDownList calif = (DropDownList)Plan.Rows[i].FindControl("CboCalif");
                    if (materia.Text != "" && calif.SelectedValue.ToString() == "---")
                    {
                        /*ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "calif_materia();", true);
                        Carga_Tpred();*/
                        resultado.Visible = true;
                        resultado.Text = "Falta Calificación / Estatus en Materia";
                        Carga_Tpred();
                        err = 1;
                    }
                }
                if (err == 0)
                {
                    MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                    conexion.Open();

                    // resultado.Text = strBorra;

                    for (int i = 0; i < Plan.Rows.Count; i++)
                    {
                        TextBox materia = (TextBox)Plan.Rows[i].FindControl("mat_origen");
                        DropDownList calif = (DropDownList)Plan.Rows[i].FindControl("CboCalif");
                        DropDownList St = (DropDownList)Plan.Rows[i].FindControl("CboSt");

                        if (materia.Text != "" && calif.SelectedValue.ToString() != "---" && St.SelectedValue.ToString() != "----")
                        {

                            string strBorra = "DELETE from tpred where tpred_tpers_num=(select tpers_num from tpers where tpers_id='" + TxtCuenta.Text + "') " +
                            " and tpred_tprog_clave='" + Global.programa.ToString() + "' and tpred_tmate_clave='" + Plan.Rows[i].Cells[2].Text.ToString() + "'" +
                            " and tpred_tespr_clave='" + txt_esc_proc.Text + "'";
                            MySqlCommand myCommandborra = new MySqlCommand(strBorra, conexion);
                            //Ejecucion del comando en el servidor de BD
                            myCommandborra.ExecuteNonQuery();

                            string consecutivo = " select count(*) from tpred " +
                                " where tpred_tespr_clave='" + txt_esc_proc.Text + "' and tpred_tprog_clave='" + Global.programa.ToString() + "'" +
                                " and tpred_tmate_clave='" + Plan.Rows[i].Cells[2].Text.ToString() + "'";
                            MySqlDataAdapter sqladapter = new MySqlDataAdapter();

                            DataSet dssql1 = new DataSet();

                            MySqlCommand commandsql1 = new MySqlCommand(consecutivo, conexion);
                            sqladapter.SelectCommand = commandsql1;
                            sqladapter.Fill(dssql1);
                            sqladapter.Dispose();
                            commandsql1.Dispose();
                            double conse = Convert.ToDouble(dssql1.Tables[0].Rows[0][0].ToString()) + 1;
                            string estatus = "";
                            if (St.SelectedValue.ToString() == "Activo")
                            {
                                estatus = "A";
                            }
                            if (St.SelectedValue.ToString() == "Baja")
                            {
                                estatus = "B";
                            }


                            string strCadSQL = "insert into tpred values((select tpers_num from tpers where tpers_id='" + TxtCuenta.Text + "'),'" +
                            txt_esc_proc.Text + "','" + Global.programa.ToString() + "','" + Plan.Rows[i].Cells[2].Text.ToString() + "','" + materia.Text + "','" + calif.SelectedValue.ToString() + "'," + conse +
                            " , current_timestamp(),'" + Session["usuario"].ToString() + "','" + estatus + "')";
                            // resultado.Text = resultado.Text + "-->" + strCadSQL;

                            MySqlCommand myCommandinserta = new MySqlCommand(strCadSQL, conexion);
                            //Ejecucion del comando en el servidor de BD
                            myCommandinserta.ExecuteNonQuery();
                            //resultado.Text = resultado.Text + "<-->" + strBorra + ":" + conse + "--" + strCadSQL;
                        }
                        if (materia.Text != "" && (calif.SelectedValue.ToString() == "---" || St.SelectedValue.ToString() == "----"))
                        {
                            /*ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "datos_materia();", true);
                            Carga_Tpred();*/
                            resultado.Visible = true;
                            resultado.Text = "Falta Calificación / Estatus en Materia";
                            Carga_Tpred();
                            err = 1;

                        }
                        if ((materia.Text == "" || calif.SelectedValue.ToString() == "---") && St.SelectedValue.ToString() != "----")
                        {
                            /*ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "datos_materia();", true);
                            Carga_Tpred();*/
                            resultado.Visible = true;
                            resultado.Text = "Falta Materia / Calificación en Materia";
                            Carga_Tpred();
                            err = 1;
                        }
                        if (calif.SelectedValue.ToString() != "---" && (materia.Text == "" || St.SelectedValue.ToString() == "----"))
                        {
                            /*ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "datos_materia();", true);
                            Carga_Tpred();*/
                            resultado.Visible = true;
                            resultado.Text = "Falta Materia / Estatus en Materia";
                            Carga_Tpred();
                            err = 1;
                        }


                    }
                    if (err == 0)
                    {
                        txt_esc_proc.Text = "";
                        txt_nom_proc.Text = "";
                        txt_origen.Text = "";
                        txt_folio.Text = "";
                        txt_fecha_i.Text = "";
                        txt_periodo.Text = "";
                        txt_nom_per.Text = "";

                        ddl_estatus.Items.Clear();
                        ddl_estatus.Items.Add("------");
                        ddl_estatus.Items.Add(new ListItem("Predictamen", "PR"));
                        ddl_estatus.Items.Add(new ListItem("Dictamen Oficial", "DO"));
                        /*CboEstatus.Items.Clear();
                        CboEstatus.Items.Add("------");
                        CboEstatus.Items.Add("Predictamen");
                        CboEstatus.Items.Add("Dictamen Oficial");*/
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                        Plan.Visible = false;
                        btn_tpred.Visible = false;
                        btn_pdf.Visible = false;
                        btn_save.Visible = true;
                        resultado.Visible = false;
                        Carga_Tpreq();
                    }

                }
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tpred", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
            }

        }

        protected void PDF_Click(object sender, EventArgs e)
        {

            Response.Redirect("TpredPDF.aspx");
        }
    }
}