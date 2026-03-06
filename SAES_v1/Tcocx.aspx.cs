using MySql.Data.MySqlClient;
using SAES_Services;
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
using static SAES_DBO.Models.ModelMenu;

namespace SAES_v1
{
    public partial class tcocx : System.Web.UI.Page
    {
        #region <Variables>
        public string labels_dashboard_1;
        public string data_dashboard_1;
        public string label_dashboard_1;
        Catalogos serviceCatalogo = new Catalogos();
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

                if (!IsPostBack)
                {
                    LlenaPagina();
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
                objPermiso = servicePermiso.ObtenerPermisoFormulario(Session["usuario"].ToString(), "tcocx");
                if (objPermiso != null)
                {
                    if (objPermiso.usme_update == "0" || objPermiso.usme_select == "0")
                    {
                        Global.opcion = 1;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                    }
                    else
                    {
                        combo_campus();
                        combo_nivel();
                        combo_programa();
                    }
                }
                else
                {
                    Global.opcion = 1;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                }
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcocx", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void combo_campus()
        {
            
            try
            {               
                ddl_campus.DataSource = serviceCatalogo.obtenCampus();
                ddl_campus.DataValueField = "Clave";
                ddl_campus.DataTextField = "Descripcion";
                ddl_campus.DataBind();
                Global.campus = "0";

            }
            catch (Exception ex)
            {
                string test = ex.Message;
            }
        }

        protected void combo_nivel()
        {                       
            try
            {           
                ddl_nivel.DataSource = serviceCatalogo.obtenNivel(ddl_campus.SelectedValue);
                ddl_nivel.DataValueField = "Clave";
                ddl_nivel.DataTextField = "Descripcion";
                ddl_nivel.DataBind();
                Global.nivel = "0";
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcocx", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

       

            protected void combo_programa()
        {
            string Query = "";
            if (ddl_campus.SelectedValue == "0")
            {
                Query = "SELECT tprog_clave Clave, tprog_desc Nombre FROM tprog ";
                        if (ddl_nivel.SelectedValue != "0")
                        {
                            Query = Query + " where tprog_tnive_clave ='" + ddl_nivel.SelectedValue + "'";
                        }
                Query = Query +  "UNION " +
                             "SELECT '0' Clave,'--- Programa ---' Nombre " +
                             "ORDER BY 1";
            }
            else
            {
                Query = " select distinct tprog_clave Clave, tprog_desc Nombre from tcapr, tprog " +
                  " where tcapr_tcamp_clave = '" + ddl_campus.SelectedValue + "' and tcapr_tprog_clave = tprog_clave ";
                 
                if (ddl_nivel.SelectedValue != "0")
                {
                    Query = Query + " and tprog_tnive_clave ='" + ddl_nivel.SelectedValue + "'";
                }
                Query = Query + " UNION " +
                             " SELECT '0' Clave,'--- Programa ---' Nombre " +
                             " ORDER BY 1 ";
            }
            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            ConexionMySql.Open();
            DataTable TablaPrograma = new DataTable();
            MySqlCommand ConsultaMySql = new MySqlCommand();
            MySqlDataReader DatosMySql;
            try
            {
                ConsultaMySql.Connection = ConexionMySql;
                ConsultaMySql.CommandType = CommandType.Text;
                ConsultaMySql.CommandText = Query;
                DatosMySql = ConsultaMySql.ExecuteReader();
                TablaPrograma.Load(DatosMySql, LoadOption.OverwriteChanges);

                ddl_programa.DataSource = TablaPrograma;
                ddl_programa.DataValueField = "Clave";
                ddl_programa.DataTextField = "Nombre";
                ddl_programa.DataBind();
                Global.programa = "0";
            }
            catch (Exception ex)
            {
                string test = ex.Message;
            }
            finally
            {
                ConexionMySql.Close();
            }
        }

        protected void ver_conf()
        {
            if (Global.campus != ddl_campus.SelectedValue)
            {
                btn_setup.Visible = true;
                btn_save.Visible = false;
                Gridtcocx.Visible = false;
            }
            if (Global.nivel != ddl_nivel.SelectedValue)
            {
                btn_setup.Visible = true;
                btn_save.Visible = false;
                Gridtcocx.Visible = false;
            }
            if (Global.programa != ddl_programa.SelectedValue)
            {
                btn_setup.Visible = true;
                btn_save.Visible = false;
                Gridtcocx.Visible = false;
            }
            if (Global.materia != txt_materia.Text)
            {
                btn_setup.Visible = true;
                btn_save.Visible = false;
                Gridtcocx.Visible = false;
            }
        }

        protected void ddl_campus_SelectedIndexChanged(object sender, EventArgs e)
        {
            combo_nivel();
            ver_conf();
        }

        protected void ddl_nivel_SelectedIndexChanged(object sender, EventArgs e)
        {
            combo_programa();
            ver_conf();
        }



        protected void btn_cancel_Click(object sender, EventArgs e)
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
            combo_campus();
            combo_nivel();
            combo_programa();
            txt_materia.Text = "";
            txt_nombre_mat.Text = "";
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            double error = 0;
            decimal suma = 0;
            for (int i = 0; i < Gridtcocx.Rows.Count; i++)
            {
                
                TextBox percent = (TextBox)Gridtcocx.Rows[i].FindControl("porc");
                if (percent.Text != "")
                {
                    bool valida;
                    try
                    {
                        Decimal.Parse(percent.Text);
                        valida = true;
                    }
                    catch
                    {
                        valida = false;
                    }
                    if (valida == false)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Error1", "Error1();", true);
                        error = 1;
                    }
                    else
                    {
                        suma = suma + Convert.ToDecimal(percent.Text);
                    }
                }
            }
            if ( suma != 100)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Error2", "Error2();", true);
                error = 1;
            }
            if (error == 0)
            {

                string config = " select distinct tcocx_no_config from tcocx " +
                    " where 1=1 ";
                if ( ddl_campus.SelectedValue != "0")
                {
                    config = config + " and tcocx_tcamp_clave='" + ddl_campus.SelectedValue + "'";
                }
                if (ddl_nivel.SelectedValue != "0")
                {
                    config = config + " and tcocx_tnive_clave='" + ddl_nivel.SelectedValue + "'";
                }
                if (ddl_programa.SelectedValue != "0")
                {
                    config = config + " and tcocx_tprog_clave='" + ddl_programa.SelectedValue + "'";
                }
                if (txt_materia.Text != "")
                {
                    config = config + " and tcocx_tmate_clave='" + txt_materia.Text + "'";
                }

                MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                conexion.Open();
                try
                {
                    MySqlDataAdapter sqladapter = new MySqlDataAdapter();

                    DataSet dssql1 = new DataSet();

                    MySqlCommand commandsql1 = new MySqlCommand(config, conexion);
                    sqladapter.SelectCommand = commandsql1;
                    sqladapter.Fill(dssql1);
                    sqladapter.Dispose();
                    commandsql1.Dispose();
                    if (dssql1.Tables[0].Rows.Count > 0)
                    {
                        Global.no_config = Convert.ToDouble(dssql1.Tables[0].Rows[0][0].ToString());
                    }
                    else
                    {
                        string config_maximo = " select max(tcocx_no_config) from tcocx ";
                        DataSet dssql2 = new DataSet();

                        MySqlCommand commandsql2 = new MySqlCommand(config_maximo, conexion);
                        sqladapter.SelectCommand = commandsql2;
                        sqladapter.Fill(dssql2);
                        sqladapter.Dispose();
                        commandsql2.Dispose();
                        if (dssql2.Tables[0].Rows[0][0].ToString() == "")
                        {
                            Global.no_config = 1;
                        }
                        else
                        {
                            Global.no_config = Convert.ToDouble(dssql2.Tables[0].Rows[0][0].ToString()) + 1;
                        }

                    }
                }
                catch (Exception ex)
                {
                    //resultado.Text = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tcocx", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);

                }


                string strBorra = "DELETE from tcocx where tcocx_no_config =" + Global.no_config;
 //             " and tcocx_tnive_clave ='" + ddl_nivel.SelectedValue + "'" +
 //             " and tcocx_tprog_clave ='" + ddl_programa.SelectedValue + "'";
 //             if (txt_materia.Text != "")
 //             {
 //               strBorra = strBorra + " and tcocx_tmate_clave ='" + txt_materia.Text + "'";
//              }


                MySqlCommand myCommandborra = new MySqlCommand(strBorra, conexion);
                myCommandborra.ExecuteNonQuery();


                for (int i = 0; i < Gridtcocx.Rows.Count; i++)
                {
                    TextBox percent = (TextBox)Gridtcocx.Rows[i].FindControl("porc");
                    if (percent.Text != "")
                    {
                        string strCadSQL = "INSERT INTO tcocx Values ('" +
                             ddl_campus.SelectedValue + "','" + ddl_nivel.SelectedValue + "','" + ddl_programa.SelectedValue + "','" +
                             txt_materia.Text + "','" + Gridtcocx.Rows[i].Cells[0].Text.ToString() + "','" + percent.Text + "','" +
                             Session["usuario"].ToString() + "',current_timestamp()," + Global.no_config + " )";
                        try
                        {
                            MySqlCommand myCommandinserta = new MySqlCommand(strCadSQL, conexion);
                            //Ejecucion del comando en el servidor de BD
                            myCommandinserta.ExecuteNonQuery();

                        }
                        catch (Exception ex)
                        {
                            string mensaje_error = ex.Message.Replace("'", "-");
                            Global.inserta_log(mensaje_error, "tcocx", Session["usuario"].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);//resultado.Text = ex.Message;
                        }
                    }
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "save", "save();", true);
                Gridtcocx.Visible = false;
                btn_save.Visible = false;
                btn_setup.Visible = true;
                combo_campus();
                combo_nivel();
                combo_programa();
                txt_materia.Text = "";
                txt_nombre_mat.Text = "";
                Global.no_config = 0;
            }
        }

        protected void btn_setup_Click(object sender, EventArgs e)
        {

            string strCadSQL = " select tcomp_clave clave, tcomp_desc componente, " +
           " (select tcocx_porcentaje from tcocx " +
           " where 1=1 ";
           if (ddl_campus.SelectedValue != "0")
            {
                strCadSQL = strCadSQL + "  and   tcocx_tcamp_clave = '" + ddl_campus.SelectedValue + "'";
            }
            else
            {
                strCadSQL = strCadSQL + "  and   tcocx_tcamp_clave = '0' ";
            }
            if (ddl_nivel.SelectedValue != "0")
            {
                strCadSQL = strCadSQL + "  and   tcocx_tnive_clave = '" + ddl_nivel.SelectedValue + "'";
            }
            else
            {
                strCadSQL = strCadSQL + "  and   tcocx_tnive_clave = '0' ";
            }
            if (ddl_programa.SelectedValue != "0")
            {
                strCadSQL = strCadSQL + "  and   tcocx_tprog_clave = '" + ddl_programa.SelectedValue + "'";
            }
            else
            {
                strCadSQL = strCadSQL + "  and   tcocx_tprog_clave = '0' ";
            }
            if (txt_materia.Text != "")
            {
                strCadSQL = strCadSQL + "  and   tcocx_tmate_clave = '" + txt_materia.Text + "'";
            }
            else
            {
                strCadSQL = strCadSQL + "  and   tcocx_tmate_clave = '' ";
            }
            strCadSQL = strCadSQL + "  and tcocx_tcomp_clave = tcomp_clave) Porcentaje, " +
           " (select tcocx_user from tcocx " +
          " where 1=1 ";
            if (ddl_campus.SelectedValue != "0")
            {
                strCadSQL = strCadSQL + "  and   tcocx_tcamp_clave = '" + ddl_campus.SelectedValue + "'";
            }
            else
            {
                strCadSQL = strCadSQL + "  and   tcocx_tcamp_clave = '0' ";
            }
            if (ddl_nivel.SelectedValue != "0")
            {
                strCadSQL = strCadSQL + "  and   tcocx_tnive_clave = '" + ddl_nivel.SelectedValue + "'";
            }
            else
            {
                strCadSQL = strCadSQL + "  and   tcocx_tnive_clave = '0' ";
            }
            if (ddl_programa.SelectedValue != "0")
            {
                strCadSQL = strCadSQL + "  and   tcocx_tprog_clave = '" + ddl_programa.SelectedValue + "'";
            }
            else
            {
                strCadSQL = strCadSQL + "  and   tcocx_tprog_clave = '0' ";
            }
            if (txt_materia.Text != "")
            {
                strCadSQL = strCadSQL + "  and   tcocx_tmate_clave = '" + txt_materia.Text + "'";
            }
            else
            {
                strCadSQL = strCadSQL + "  and   tcocx_tmate_clave = '' ";
            }
            strCadSQL = strCadSQL + "  and tcocx_tcomp_clave = tcomp_clave) Usuario, " +
           " (select fecha(date_format(tcocx_date, '%Y-%m-%d')) from tcocx " +
           " where 1=1 ";
            if (ddl_campus.SelectedValue != "0")
            {
                strCadSQL = strCadSQL + "  and   tcocx_tcamp_clave = '" + ddl_campus.SelectedValue + "'";
            }
            else
            {
                strCadSQL = strCadSQL + "  and   tcocx_tcamp_clave = '0' ";
            }
            if (ddl_nivel.SelectedValue != "0")
            {
                strCadSQL = strCadSQL + "  and   tcocx_tnive_clave = '" + ddl_nivel.SelectedValue + "'";
            }
            else
            {
                strCadSQL = strCadSQL + "  and   tcocx_tnive_clave = '0' ";
            }
            if (ddl_programa.SelectedValue != "0")
            {
                strCadSQL = strCadSQL + "  and   tcocx_tprog_clave = '" + ddl_programa.SelectedValue + "'";
            }
            else
            {
                strCadSQL = strCadSQL + "  and   tcocx_tprog_clave = '0' ";
            }
            if (txt_materia.Text != "")
            {
                strCadSQL = strCadSQL + "  and   tcocx_tmate_clave = '" + txt_materia.Text + "'";
            }
            else
            {
                strCadSQL = strCadSQL + "  and   tcocx_tmate_clave = '' ";
            }
            strCadSQL = strCadSQL + "  and tcocx_tcomp_clave = tcomp_clave) fecha " +
           " from tcomp order by tcomp_clave ";
            MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                conexion.Open();
            MySqlDataAdapter dataadapter = new MySqlDataAdapter(strCadSQL, conexion);
            DataSet ds = new DataSet();
            dataadapter.Fill(ds, "tcocx");
            Gridtcocx.DataSource = ds;
            Gridtcocx.DataBind();
            Gridtcocx.DataMember = "tcocx";
            Gridtcocx.HeaderRow.TableSection = TableRowSection.TableHeader;
            Gridtcocx.UseAccessibleHeader = true;
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);
            for (int i = 0; i < Gridtcocx.Rows.Count; i++)
            {
                TextBox percent = (TextBox)Gridtcocx.Rows[i].FindControl("porc");

                percent.Text = ds.Tables[0].Rows[i][2].ToString();
            }

            conexion.Close();
            Gridtcocx.Visible = true;
            btn_setup.Visible = false;
            if (Global.opcion == 0)
            {
                btn_save.Visible = true;
            }
            Global.campus = ddl_campus.SelectedValue;
            Global.nivel =  ddl_nivel.SelectedValue;
            Global.programa = ddl_programa.SelectedValue;
            Global.materia = txt_materia.Text;
        }

        protected void Busqueda_Materias(object sender, EventArgs e)
        {
            if (Gridtmate.Visible == true)
            {
                Gridtmate.Visible = false;
            }
            else
            {
                Gridtmate.Visible = true;
                string strQueryMaterias = "";
                if (ddl_nivel.SelectedValue != "0")
                {
                   if (ddl_programa.SelectedValue == "0")
                   {
                        strQueryMaterias = " select distinct tmate_clave Clave, tmate_desc Nombre from tmate, tprog, tplan  " +
                          " where tprog_tnive_clave ='" + ddl_nivel.SelectedValue + "' " +
                          " and tplan_tprog_clave = tprog_clave and tplan_tmate_clave = tmate_clave order by Clave ";
                   }
                   else
                    {
                        strQueryMaterias = " select tmate_clave Clave, tmate_desc Nombre from tmate, tplan " +
                            " where tplan_tprog_clave='" + ddl_programa.SelectedValue + "' " +
                            " and tplan_tmate_clave=tmate_clave  order by Clave";
                    }
                }
                else
                {
                    if (ddl_programa.SelectedValue == "0")
                    {
                        strQueryMaterias = " select tmate_clave Clave, tmate_desc Nombre from tmate " +
                            " order by Clave ";
                    }
                    else
                    {
                        strQueryMaterias = " select tmate_clave Clave, tmate_desc Nombre from tmate, tplan " +
                            " where tplan_tprog_clave='" + ddl_programa.SelectedValue + "' " +
                            " and tplan_tmate_clave=tmate_clave  order by Clave";
                    }
                }
                try
                {
                    MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                    conexion.Open();
                    MySqlDataAdapter dataadapter = new MySqlDataAdapter(strQueryMaterias, conexion);
                    DataSet ds = new DataSet();
                    dataadapter.Fill(ds, "Materias");
                    Gridtmate.DataSource = ds;
                    Gridtmate.DataBind();
                    Gridtmate.DataMember = "Materias";
                    Gridtmate.HeaderRow.TableSection = TableRowSection.TableHeader;
                    Gridtmate.UseAccessibleHeader = true;
                    Gridtmate.Visible = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_Materias", "load_datatable_Materias();", true);
                    conexion.Close();
                }
                catch (Exception ex)
                {
                    //resultado.Text = ex.Message;
                }
            }
        }

        protected void Gridtmate_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = Gridtmate.SelectedRow;
            txt_materia.Text = row.Cells[1].Text;
            txt_nombre_mat.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);
            Gridtmate.Visible = false;
        }

        protected void txt_materia_TextChanged(object sender, EventArgs e)
        {
            string QerySelect = " select tmate_desc from tmate where tmate_clave='" + txt_materia.Text + "'";
            MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            conexion.Open();
            MySqlDataAdapter sqladapter = new MySqlDataAdapter();

            DataSet dssql1 = new DataSet();

            MySqlCommand commandsql1 = new MySqlCommand(QerySelect, conexion);
            sqladapter.SelectCommand = commandsql1;
            sqladapter.Fill(dssql1);
            sqladapter.Dispose();
            commandsql1.Dispose();
            if (dssql1.Tables[0].Rows.Count == 0)
            {
                txt_nombre_mat.Text = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "NoExists", "NoExists();", true);
                ver_conf();
            }
            else
            {
                txt_nombre_mat.Text = dssql1.Tables[0].Rows[0][0].ToString();
                ver_conf();
            }
        }

        protected void linkBttnBusca_Click(object sender, EventArgs e)
        {
            //if (Gridtmate.Visible == true)
            //{
            //    Gridtmate.Visible = false;
            //}
            //else
            //{
            //    Gridtmate.Visible = true;
                string strQueryMaterias = "";
                if (ddl_nivel.SelectedValue != "0")
                {
                    if (ddl_programa.SelectedValue == "0")
                    {
                        strQueryMaterias = " select distinct tmate_clave Clave, tmate_desc Nombre from tmate, tprog, tplan  " +
                          " where tprog_tnive_clave ='" + ddl_nivel.SelectedValue + "' " +
                          " and tplan_tprog_clave = tprog_clave and tplan_tmate_clave = tmate_clave order by Clave ";
                    }
                    else
                    {
                        strQueryMaterias = " select tmate_clave Clave, tmate_desc Nombre from tmate, tplan " +
                            " where tplan_tprog_clave='" + ddl_programa.SelectedValue + "' " +
                            " and tplan_tmate_clave=tmate_clave  order by Clave";
                    }
                }
                else
                {
                    if (ddl_programa.SelectedValue == "0")
                    {
                        strQueryMaterias = " select tmate_clave Clave, tmate_desc Nombre from tmate " +
                            " order by Clave ";
                    }
                    else
                    {
                        strQueryMaterias = " select tmate_clave Clave, tmate_desc Nombre from tmate, tplan " +
                            " where tplan_tprog_clave='" + ddl_programa.SelectedValue + "' " +
                            " and tplan_tmate_clave=tmate_clave  order by Clave";
                    }
                }
                try
                {
                    MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                    conexion.Open();
                    MySqlDataAdapter dataadapter = new MySqlDataAdapter(strQueryMaterias, conexion);
                    DataSet ds = new DataSet();
                    dataadapter.Fill(ds, "Materias");
                    Gridtmate.DataSource = ds;
                    Gridtmate.DataBind();
                    Gridtmate.DataMember = "Materias";
                    Gridtmate.HeaderRow.TableSection = TableRowSection.TableHeader;
                    Gridtmate.UseAccessibleHeader = true;
                    Gridtmate.Visible = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_Materias", "load_datatable_Materias();", true);
                    conexion.Close();
                }
                catch (Exception ex)
                {
                    //resultado.Text = ex.Message;
                }
            //}
        }
    }
}