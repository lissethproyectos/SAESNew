using MySql.Data.MySqlClient;
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

namespace SAES_v1
{
    public partial class tdoci : System.Web.UI.Page
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

                if (!IsPostBack)
                {
                    txt_matricula.Text = Global.clave_docente;
                    txt_nombre.Text = Global.nombre_docente + " " + Global.ap_paterno_docente + " " + Global.ap_materno_docente;
                    LlenaPagina();
                    combo_estatus();
                    combo_categoria();
                    combo_estatus_carrera();
                    if (txt_matricula.Text != "")
                    {
                        Carga_Carreras();
                    }
                }

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

        private void LlenaPagina()
        {
            string QerySelect = "select tusme_update, tusme_select from tuser, tusme, tmede " +
                              " where tuser_clave = '" + Session["usuario"].ToString() + "'" +
                              " and tusme_trole_clave = tuser_trole_clave and tusme_tmenu_clave = 5 and tusme_tmenu_clave = tmede_tmenu_clave " +
                              " and tusme_tmede_clave = tmede_clave and tmede_forma='tdoce' ";

            MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            conexion.Open();
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
            if (dssql1.Tables[0].Rows[0][0].ToString() == "0")
            {
                Img1.Visible = false;
                Img2.Visible = false;
                Img3.Visible = false;
            }
            combo_categoria();
            //combo_estatus_carrera();
            combo_estatus();

            conexion.Close();
        }

        protected void combo_estatus()
        {
            ddl_estatus.Items.Clear();
            ddl_estatus.Items.Add(new ListItem("Activo", "A"));
            ddl_estatus.Items.Add(new ListItem("Inactivo", "B"));
        }

        protected void combo_categoria()
        {
            string strQuerycado = "";
            strQuerycado = "select tcado_clave clave, tcado_desc categoria from tcado " +
                            " where tcado_estatus='A' " +
                             " union " +
                             " select '000' clave, '---------' categoria from dual " +
                             " order by clave ";

            MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            conexion.Open();
            MySqlDataAdapter sqladapter = new MySqlDataAdapter();
            DataTable TablaCado = new DataTable();
            MySqlCommand ConsultaMySql = new MySqlCommand();
            MySqlDataReader DatosMySql;
            ConsultaMySql.Connection = conexion;
            ConsultaMySql.CommandType = CommandType.Text;
            ConsultaMySql.CommandText = strQuerycado;
            DatosMySql = ConsultaMySql.ExecuteReader();
            TablaCado.Load(DatosMySql, LoadOption.OverwriteChanges);

            ddl_categoria.DataSource = TablaCado;
            ddl_categoria.DataValueField = "clave";
            ddl_categoria.DataTextField = "categoria";
            ddl_categoria.DataBind();
        }

        protected void combo_estatus_carrera()
        {
            ddl_estatus_carrera.Items.Clear();
            ddl_estatus_carrera.Items.Add(new ListItem("----", "0"));
            ddl_estatus_carrera.Items.Add(new ListItem("Titulado", "T"));
            ddl_estatus_carrera.Items.Add(new ListItem("Egresado", "E"));
            ddl_estatus_carrera.Items.Add(new ListItem("Carrera Trunca", "C"));
        }

        protected void grid_docentes_bind(object sender, EventArgs e)
        {
            if (GridDocentes.Visible == true)
            {
                GridDocentes.Visible = false;
            }
            else
            {
                string QueryEstudiantes = "select distinct tpers_id clave, tpers_nombre nombre, tpers_paterno paterno, tpers_materno materno, tpers_num pidm,  tpers_genero c_genero, CASE WHEN tpers_genero = 'F' THEN 'Femenino' WHEN tpers_genero = 'M' THEN 'Masculino' ELSE 'No Aplica' END genero, " +
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
                    GridDocentes.DataSource = ds;
                    GridDocentes.DataBind();
                    GridDocentes.DataMember = "Solicitudes";
                    GridDocentes.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridDocentes.UseAccessibleHeader = true;

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_Docentes", "load_datatable_Docentes();", true);
                    GridDocentes.Visible = true;
                }
                catch (Exception ex)
                {
                    //logs
                }
                conexion.Close();
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
                 " tpers_nombre, tpers_paterno, tpers_materno FROM tpers WHERE tpers_id = '" + matricula + "'";
            MySqlCommand cmd = new MySqlCommand(Query);
            DataTable dt = GetData(cmd);
            nombre = dt.Rows[0]["nombre"].ToString();
            Global.clave_docente = txt_matricula.Text;
            Global.nombre_docente = dt.Rows[0]["tpers_nombre"].ToString();
            Global.ap_paterno_docente = dt.Rows[0]["tpers_paterno"].ToString();
            Global.ap_materno_docente = dt.Rows[0]["tpers_materno"].ToString();
            return nombre;
        }

        protected void txt_matricula_TextChanged(object sender, EventArgs e)
        {
            if (valida_matricula(txt_matricula.Text))
            {
                txt_nombre.Text = nombre_alumno(txt_matricula.Text);
                Carga_Carreras();
            }
            else
            {
                ///Matricula no existe
            }
        }

        protected void GridDocentes_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridDocentes.SelectedRow;
            txt_matricula.Text = row.Cells[1].Text;
            txt_nombre.Text = HttpUtility.HtmlDecode(row.Cells[2].Text) + " " + HttpUtility.HtmlDecode(row.Cells[3].Text) + " " +
             HttpUtility.HtmlDecode(row.Cells[4].Text);
            GridDocentes.Visible = false;
            txt_matricula.Attributes.Add("readonly", "");
            Global.clave_docente = txt_matricula.Text;
            Global.nombre_docente = HttpUtility.HtmlDecode(row.Cells[2].Text);
            Global.ap_paterno_docente = HttpUtility.HtmlDecode(row.Cells[3].Text);
            Global.ap_materno_docente = HttpUtility.HtmlDecode(row.Cells[4].Text);
            Carga_Carreras();
        }

        protected void Agregar_Click(object sender, EventArgs e)
        {
            string strQueryInterno = " select tpers_num from tpers where tpers_id='" + txt_matricula.Text + "' ";
            MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            conexion.Open();
            MySqlDataAdapter sqladapter = new MySqlDataAdapter();
            DataSet dssql = new DataSet();
            MySqlCommand commandsql = new MySqlCommand(strQueryInterno, conexion);
            sqladapter.SelectCommand = commandsql;
            sqladapter.Fill(dssql);
            sqladapter.Dispose();
            commandsql.Dispose();
            double pidm;
            pidm = Convert.ToDouble(dssql.Tables[0].Rows[0][0].ToString());
            //resultado.Text = strQueryInterno + "===" + dssql.Tables[0].Rows[0][0].ToString();

            string strCborraSql = " delete from tdoce where tdoce_tpers_num=" + pidm;
            MySqlCommand myCommandborra = new MySqlCommand(strCborraSql, conexion);
            //Ejecucion del comando en el servidor de BD 
            myCommandborra.ExecuteNonQuery();

            string strCadSQL = "INSERT INTO tdoce Values (" + pidm + ",'" + ddl_categoria.SelectedValue.ToString() + "','" +
                ddl_estatus.SelectedValue + "', current_timestamp(),'" + Session["usuario"].ToString() + "')";
            //resultado.Text = strCadSQL;
            MySqlCommand myCommandinserta = new MySqlCommand(strCadSQL, conexion);
            //Ejecucion del comando en el servidor de BD 
            myCommandinserta.ExecuteNonQuery();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
            conexion.Close();
            Carga_Carreras();

        }

        protected void Agregar_Carrera(object sender, EventArgs e)
        {
            string strQueryInterno = " select tpers_num from tpers where tpers_id='" + txt_matricula.Text + "' ";
            MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            conexion.Open();
            MySqlDataAdapter sqladapter = new MySqlDataAdapter();
            DataSet dssql = new DataSet();
            MySqlCommand commandsql = new MySqlCommand(strQueryInterno, conexion);
            sqladapter.SelectCommand = commandsql;
            sqladapter.Fill(dssql);
            sqladapter.Dispose();
            commandsql.Dispose();
            double pidm;
            pidm = Convert.ToDouble(dssql.Tables[0].Rows[0][0].ToString());
            //resultado.Text = strQueryInterno + "===" + dssql.Tables[0].Rows[0][0].ToString();

            if (Global.carrera_docente != null)
            {
                string strCborraSQL = " delete from tdoca where tdoca_tpers_num=" + pidm + " and tdoca_carrera='" +
                    Global.carrera_docente.ToString() + "'";
                MySqlCommand myCommandborra = new MySqlCommand(strCborraSQL, conexion);
                //Ejecucion del comando en el servidor de BD 
                myCommandborra.ExecuteNonQuery();

            }

            string strCadSQL = "INSERT INTO tdoca Values (" + pidm + ",'" + txt_carrera.Text + "','" + ddl_estatus_carrera.SelectedValue + "','" + txt_cedula.Text +
                "',current_timestamp(),'" + Session["usuario"].ToString() + "')";
            //resultado.Text = strCadSQL;
            MySqlCommand myCommandinserta = new MySqlCommand(strCadSQL, conexion);
            //Ejecucion del comando en el servidor de BD 
            myCommandinserta.ExecuteNonQuery();

            conexion.Close();
            Global.clave_docente = txt_matricula.Text;
            txt_carrera.Text = "";
            txt_cedula.Text = "";
            combo_estatus_carrera();
            Carga_Carreras();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
        }

        protected void Agregar_Idioma(object sender, EventArgs e)
        {

            string strQueryInterno = " select tpers_num from tpers where tpers_id='" + txt_matricula.Text + "' ";
            MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            conexion.Open();
            MySqlDataAdapter sqladapter = new MySqlDataAdapter();
            DataSet dssql = new DataSet();
            MySqlCommand commandsql = new MySqlCommand(strQueryInterno, conexion);
            sqladapter.SelectCommand = commandsql;
            sqladapter.Fill(dssql);
            sqladapter.Dispose();
            commandsql.Dispose();
            double pidm;
            pidm = Convert.ToDouble(dssql.Tables[0].Rows[0][0].ToString());
            //resultado.Text = strQueryInterno + "===" + dssql.Tables[0].Rows[0][0].ToString();

            if (Global.idioma_docente != null)
            {
                string strCborraSQL = " delete from tdoid where tdoid_tpers_num=" + pidm + " and tdoid_idioma='" +
                    Global.idioma_docente.ToString() + "'";
                MySqlCommand myCommandborra = new MySqlCommand(strCborraSQL, conexion);
                //Ejecucion del comando en el servidor de BD 
                myCommandborra.ExecuteNonQuery();

            }

            string strCadSQL = "INSERT INTO tdoid Values (" + pidm + ",'" + txt_idioma.Text + "','" + txt_porcentaje.Text +
                "',current_timestamp(),'" + Session["usuario"].ToString() + "')";
            //resultado.Text = strCadSQL;
            MySqlCommand myCommandinserta = new MySqlCommand(strCadSQL, conexion);
            //Ejecucion del comando en el servidor de BD 
            myCommandinserta.ExecuteNonQuery();
            conexion.Close();
            Global.clave_docente = txt_matricula.Text;
            txt_idioma.Text = "";
            txt_porcentaje.Text = "";
            Carga_Carreras();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
        }
        


        private void Carga_Carreras()
        {

            MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            conexion.Open();
            //if (BaseDatos.docente != TxtClave.Text)
            if (txt_matricula.Text != "")
            {
                combo_categoria();

                combo_estatus();

                combo_estatus_carrera();
            }

            string strQueryNombre = "";
            strQueryNombre = " select concat(tpers_nombre,' ',tpers_paterno,' ',tpers_materno) nombre " +
                         " from tpers where tpers_id='" + txt_matricula.Text + "' and tpers_tipo='D' ";

            //resultado.Text = "1--" + strQueryNombre;
            
            try
            {

                MySqlDataAdapter sqladapter = new MySqlDataAdapter();

                DataSet dssql1 = new DataSet();

                MySqlCommand commandsql1 = new MySqlCommand(strQueryNombre, conexion);
                sqladapter.SelectCommand = commandsql1;
                sqladapter.Fill(dssql1);
                sqladapter.Dispose();
                commandsql1.Dispose();


                if (dssql1.Tables[0].Rows.Count != 0)
                {
                    txt_nombre.Text = dssql1.Tables[0].Rows[0][0].ToString();

                }
                else
                {
                    //
                }

                string strQueryDoce = " select tdoce_tcado_clave categoria, tdoce_estatus estatus from tdoce " +
                       " where tdoce_tpers_num in (select tpers_num from tpers where tpers_id='" + txt_matricula.Text + "')";
                DataSet dsdoce = new DataSet();

                MySqlCommand commandoce = new MySqlCommand(strQueryDoce, conexion);
                sqladapter.SelectCommand = commandoce;
                sqladapter.Fill(dsdoce);
                sqladapter.Dispose();
                commandoce.Dispose();

                if (dsdoce.Tables[0].Rows.Count > 0)
                {
                    combo_categoria();
                    ddl_categoria.SelectedValue = dsdoce.Tables[0].Rows[0][0].ToString();

                    combo_estatus();
                    ddl_estatus.SelectedValue = dsdoce.Tables[0].Rows[0][1].ToString();

                }

              string StrQueryCarr = " select tdoca_carrera carrera, tdoca_st_carrera c_estatus," +
                    " case when tdoca_st_carrera='T' then 'Titulado' " +
                    "      when tdoca_st_carrera='E' then 'Egresado' " +
                    "      when tdoca_st_carrera='C' then 'Carrera Trunca' " +
                    " end estatus, tdoca_ced_carrera cedula,  " +
                    " fecha(date_format(tdoca_date,'%Y-%m-%d')) fecha " +
                    " from tdoca where tdoca_tpers_num in (select tpers_num from tpers where tpers_id='" + txt_matricula.Text + "')";

                DataSet ds = new DataSet();
                MySqlDataAdapter dataadapter1 = new MySqlDataAdapter(StrQueryCarr, conexion);
                dataadapter1.Fill(ds, "Carreras");
                GridCarreras.DataSource = ds;
                GridCarreras.DataBind();
                GridCarreras.DataMember = "Carreras";
                GridCarreras.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridCarreras.UseAccessibleHeader = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_Carreras", "load_datatable_Carreras();", true);
                GridCarreras.Visible = true;
                Global.clave_docente = txt_matricula.Text;

                if (ds.Tables[0].Rows.Count == 0)
                {
                    StrQueryCarr = " select '' carrera,'' c_estatus, '' estatus, '' cedula,  " +
                    " '' fecha " +
                    " from dual ";

                    //  resultado.Text = "1--" + strQueryTelefono + "----" + strQueryTelefono;
                    DataSet ds1 = new DataSet();
                    MySqlDataAdapter dataadapter = new MySqlDataAdapter(StrQueryCarr, conexion);
                    dataadapter.Fill(ds1, "Carreras");
                    GridCarreras.DataSource = ds1;
                    GridCarreras.DataBind();
                    GridCarreras.DataMember = "Carreras";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_Carreras", "load_datatable_Carreras();", true);
                    GridCarreras.Visible = true;
                }
                
                string StrQueryIdioma = " select tdoid_idioma idioma, tdoid_porc_idioma porcentaje,  " +
                     " fecha(date_format(tdoid_date,'%Y-%m-%d')) fecha " +
                     " from tdoid where tdoid_tpers_num in (select tpers_num from tpers where tpers_id='" + txt_matricula.Text + "')";

                //  resultado.Text = "1--" + strQueryTelefono + "----" + strQueryTelefono;
                DataSet dsidiomas = new DataSet();
                MySqlDataAdapter dataadapter10 = new MySqlDataAdapter(StrQueryIdioma, conexion);
                dataadapter10.Fill(dsidiomas, "Idiomas");
                GridIdiomas.DataSource = dsidiomas;
                GridIdiomas.DataBind();
                GridIdiomas.DataMember = "Idiomas";
                GridIdiomas.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridIdiomas.UseAccessibleHeader = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_Idiomas", "load_datatable_Idiomas();", true);
                GridIdiomas.Visible = true;

                if (dsidiomas.Tables[0].Rows.Count == 0)
                {
                    StrQueryIdioma = " select '' idioma, '' porcentaje, " +
                    " '' fecha " +
                    " from dual ";

                    //  resultado.Text = "1--" + strQueryTelefono + "----" + strQueryTelefono;
                    DataSet ds1 = new DataSet();
                    MySqlDataAdapter dataadapter = new MySqlDataAdapter(StrQueryIdioma, conexion);
                    dataadapter.Fill(ds1, "Idiomas");
                    GridIdiomas.DataSource = ds1;
                    GridIdiomas.DataBind();
                    GridIdiomas.DataMember = "Idiomas";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_Idiomas", "load_datatable_Idiomas();", true);
                    GridIdiomas.Visible = true;
                }


            conexion.Close();
            }
            catch (Exception ex)
            {
                //
            }
            txt_matricula.Focus();
        }
        
        protected void GridCatalogo_Carreras(object sender, EventArgs e)
        {
            GridViewRow row = GridCarreras.SelectedRow;
            txt_carrera.Text = HttpUtility.HtmlDecode(row.Cells[1].Text);
            combo_estatus_carrera();
            ddl_estatus_carrera.SelectedValue = row.Cells[2].Text;
            txt_cedula.Text = HttpUtility.HtmlDecode(row.Cells[4].Text);
            Global.clave_docente = txt_matricula.Text;
            Global.carrera_docente = row.Cells[1].Text.ToString();

        }

        protected void GridCatalogo_Idiomas(object sender, EventArgs e)
        {
            GridViewRow row = GridIdiomas.SelectedRow;
            txt_idioma.Text = HttpUtility.HtmlDecode(row.Cells[1].Text);
            txt_porcentaje.Text = row.Cells[2].Text;
            Global.docente = txt_matricula.Text;
            Global.idioma_docente = txt_idioma.Text;
            txt_matricula.Focus();
        }
        
        protected void txt_porcentaje_TextChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_valor_entero();", true);
        }

    }
}