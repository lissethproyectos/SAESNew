using MySql.Data.MySqlClient;
using SAES_Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SAES_v1
{
    public partial class tseri : System.Web.UI.Page
    {
        #region <Variables>
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_Programas", "load_datatable_Programas();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_Materias", "load_datatable_Materias();", true);

            }
        }

        private void LlenaPagina()
        {
            System.Threading.Thread.Sleep(50);

            string QerySelect = "select tusme_update, tusme_select from tuser, tusme " +
                              " where tuser_clave = '" + Session["usuario"].ToString() + "'" +
                              " and tusme_trole_clave = tuser_trole_clave and tusme_tmenu_clave = 1 and tusme_tmede_clave = 6 ";

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
                        btn_tstal.Visible = true;
                    }
                }

            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;

            }
            conexion.Close();

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



        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            txt_tprog.Text = null;
            txt_nombre_prog.Text = null;
            txt_tmate.Text = null;
            txt_nombre_mate.Text = null;
            txt_seri1.Text = null;
            txt_seri2.Text = null;
            txt_seri3.Text = null;
            btn_save.Visible = true;
            btn_update.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
        }

        protected void grid_tseri_bind()
        {
            string strQueryGrid = "";
            strQueryGrid = " select tseri_tmate_clave clave, a.tmate_desc materia, tseri_mat1 cl_seriacion, b.tmate_desc desc_seriacion, " +
             " tseri_mat2 clave_or2,(select tmate_desc from tmate where tmate_clave = tseri_mat2) or2, " +
             " tseri_mat3 clave_or3,(select tmate_desc from tmate where tmate_clave = tseri_mat3) or3 " +
             "  from tseri, tmate a, tmate b " +
             "  where tseri_tprog_clave = '" + txt_tprog.Text + "' and tseri_tmate_clave = a.tmate_clave " +
             " and tseri_mat1 = b.tmate_clave ";
            MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            conexion.Open();
            try
            {
                MySqlDataAdapter dataadapter = new MySqlDataAdapter(strQueryGrid, conexion);
                DataSet ds = new DataSet();
                dataadapter.Fill(ds, "tseri");
                Gridtseri.DataSource = ds;
                Gridtseri.DataBind();
                Gridtseri.DataMember = "tseri";
                Gridtseri.HeaderRow.TableSection = TableRowSection.TableHeader;
                Gridtseri.UseAccessibleHeader = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_tseri", "load_datatable_tseri();", true);
                Gridtseri.Visible = true;
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;

            }
            conexion.Close();
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txt_seri1.Text))
            {

                string strBorra = "DELETE from tseri where tseri_tprog_clave='" + txt_tprog.Text + "' " +
                    " and tseri_tmate_clave='" + txt_tmate.Text + "'";
                MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                conexion.Open();
                MySqlCommand myCommandborra = new MySqlCommand(strBorra, conexion);
                myCommandborra.ExecuteNonQuery();

                string strCadSQL = "INSERT INTO tseri Values ('" + txt_tprog.Text + "','" + txt_tmate.Text + "','" +
                txt_seri1.Text + "','" + txt_seri2.Text + "','" + txt_seri3.Text + "','" + Session["usuario"].ToString() + "',current_timestamp()) ";

                MySqlCommand mysqlcmd = new MySqlCommand(strCadSQL, conexion);
                mysqlcmd.CommandType = CommandType.Text;
                try
                {
                    mysqlcmd.ExecuteNonQuery();
                    txt_tmate.Text = "";
                    txt_nombre_mate.Text = "";
                    txt_seri1.Text = "";
                    txt_seri2.Text = "";
                    txt_seri3.Text = "";

                    grid_tseri_bind();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                }
                catch (Exception ex)
                {
                    string test = ex.Message;
                }
                finally
                {
                    conexion.Close();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_tseri();", true);
                grid_tseri_bind();
            }
        }

        protected void btn_update_Click(object sender, EventArgs e)
        {

        }

        protected void Carga_Materia(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txt_tprog.Text))
            {

                string strQueryPlan = "";
                strQueryPlan = " select count(*) from tplan where tplan_tprog_clave='" + txt_tprog.Text + "'" +
                    " and tplan_tmate_clave='" + txt_tmate.Text + "'";

                //resultado.Text = strQuery;
                //try
                //{
                MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                conexion.Open();

                MySqlDataAdapter sqladapter = new MySqlDataAdapter();

                DataSet dssql1 = new DataSet();

                MySqlCommand commandsql1 = new MySqlCommand(strQueryPlan, conexion);
                sqladapter.SelectCommand = commandsql1;
                sqladapter.Fill(dssql1);
                sqladapter.Dispose();
                commandsql1.Dispose();
                if (dssql1.Tables[0].Rows[0][0].ToString() == "0")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validarClaveMateria('ContentPlaceHolder1_txt_tmate',2);", true);
                }
                else
                {
                    string strQuery = "";
                    strQuery = " select tmate_desc from tmate where tmate_clave='" + txt_tmate.Text + "'";

                    //resultado.Text = strQuery;
                    try
                    {


                        DataSet dssql2 = new DataSet();

                        MySqlCommand commandsql2 = new MySqlCommand(strQuery, conexion);
                        sqladapter.SelectCommand = commandsql2;
                        sqladapter.Fill(dssql2);
                        sqladapter.Dispose();
                        commandsql1.Dispose();
                        if (dssql2.Tables[0].Rows.Count > 0)
                        {
                            txt_nombre_mate.Text = dssql2.Tables[0].Rows[0][0].ToString();

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validarClaveMateria('ContentPlaceHolder1_txt_tmate',1);", true);
                        }
                        //Plan.Visible = false;
                        //Preq.Visible = false;
                    }
                    catch (Exception ex)
                    {
                        //resultado.Text = ex.Message;
                    }
                }

                /* }
                 catch (Exception ex)
                 {
                     //resultado.Text = ex.Message;
                 }*/


            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validarClave('ContentPlaceHolder1_txt_tprog',0);", true);
            }
        }

        protected void Busqueda_Programas(object sender, EventArgs e)
        {

        }

        protected void Busqueda_Materias(object sender, EventArgs e)
        {

        }

        protected void Busqueda_Seri1(object sender, EventArgs e)
        {
            Busca_Seriacion();
            Global.consecutivo = 1;
        }

        protected void Busqueda_Seri2(object sender, EventArgs e)
        {
            Busca_Seriacion();
            Global.consecutivo = 2;
        }

        protected void Busqueda_Seri3(object sender, EventArgs e)
        {
            Busca_Seriacion();
            Global.consecutivo = 3;
        }

        private void Busca_Seriacion()
        {
            //if (Gridmat.Visible == true)
            //{
            //    Gridmat.Visible = false;
            //}
            //else
            //{
            //    Gridmat.Visible = true;
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopupAreas", "$('#modalAreas').modal('show')", true);
            Gridmat.Visible = true;
            string strQueryMaterias = "";
            strQueryMaterias = " select tplan_tarea_clave area, tplan_consecutivo cons, tplan_tmate_clave Clave, tmate_desc Nombre " +
                " from tplan, tmate where tplan_tprog_clave='" + txt_tprog.Text + "'" +
                " and tplan_tmate_clave=tmate_clave " +
                " and tplan_tmate_clave not in ('" + txt_tmate.Text + "','" + txt_seri1.Text + "','" + txt_seri2.Text + "','" + txt_seri3.Text + "')";
            strQueryMaterias = strQueryMaterias + " order  by Clave ";
            try
            {
                MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                conexion.Open();
                MySqlDataAdapter dataadapter = new MySqlDataAdapter(strQueryMaterias, conexion);
                DataSet ds = new DataSet();
                dataadapter.Fill(ds, "Programas");
                Gridmat.DataSource = ds;
                Gridmat.DataBind();
                Gridmat.DataMember = "Programas";
                //Gridmat.HeaderRow.TableSection = TableRowSection.TableHeader;
                Gridmat.UseAccessibleHeader = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_Mate", "load_datatable_Mate();", true);
                conexion.Close();
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
            }
            //}
        }

        protected void Gridtprog_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = Gridtprog.SelectedRow;
            txt_tprog.Text = row.Cells[1].Text;
            txt_nombre_prog.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopupProgramas", "$('#modalProgramas').modal('hide')", true);
            Gridtprog.Visible = false;
            grid_tseri_bind();
            //combo_estatus();
        }

        protected void Gridtmate_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = Gridtmate.SelectedRow;
            txt_tmate.Text = row.Cells[3].Text;
            txt_nombre_mate.Text = HttpUtility.HtmlDecode(row.Cells[4].Text);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopupMaterias", "$('#modalMaterias').modal('hide')", true);

            Gridtmate.Visible = false;
            //combo_estatus();
        }

        protected void Gridtseri_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = Gridtseri.SelectedRow;
            txt_tmate.Text = row.Cells[1].Text;
            txt_nombre_mate.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);
            txt_seri1.Text = HttpUtility.HtmlDecode(row.Cells[3].Text);
            txt_seri2.Text = HttpUtility.HtmlDecode(row.Cells[5].Text);
            txt_seri3.Text = HttpUtility.HtmlDecode(row.Cells[7].Text);
            btn_update.Visible = true;
            btn_save.Visible = false;

        }

        protected void Gridmat_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = Gridmat.SelectedRow;
            if (Global.consecutivo == 1)
            {
                txt_seri1.Text = row.Cells[3].Text;
            }
            if (Global.consecutivo == 2)
            {
                txt_seri2.Text = row.Cells[3].Text;
            }
            if (Global.consecutivo == 3)
            {
                txt_seri3.Text = row.Cells[3].Text;
            }
            Gridmat.Visible = false;
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopupAreas", "$('#modalAreas').modal('hide')", true);

        }


        protected void linkBttnBuscaMateria_Click(object sender, EventArgs e)
        {
            //if (Gridtmate.Visible == true)
            //{
            //    Gridtmate.Visible = false;
            //}
            //else
            //{

            if (!String.IsNullOrEmpty(txt_tprog.Text))
            {
                Gridtmate.Visible = true;
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopupMaterias", "$('#modalMaterias').modal('show')", true);

                string strQueryMaterias = "";
                strQueryMaterias = " select tplan_tarea_clave area, tplan_consecutivo cons, tplan_tmate_clave Clave, tmate_desc Nombre " +
                    " from tplan, tmate where tplan_tprog_clave='" + txt_tprog.Text + "'" +
                    " and tplan_tmate_clave=tmate_clave ";
                strQueryMaterias = strQueryMaterias + " order  by Clave ";
                try
                {
                    MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                    conexion.Open();
                    MySqlDataAdapter dataadapter = new MySqlDataAdapter(strQueryMaterias, conexion);
                    DataSet ds = new DataSet();
                    dataadapter.Fill(ds, "Programas");
                    Gridtmate.DataSource = ds;
                    Gridtmate.DataBind();
                    Gridtmate.DataMember = "Programas";
                    Gridtmate.HeaderRow.TableSection = TableRowSection.TableHeader;
                    Gridtmate.UseAccessibleHeader = true;
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_Materias", "load_datatable_Materias();", true);
                    conexion.Close();
                }
                catch (Exception ex)
                {
                    //resultado.Text = ex.Message;
                }
            }
            else
            {
                //txt_tprog.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validarClave('ContentPlaceHolder1_txt_tprog',0);", true);
            }

            //}
        }

        protected void linkBttnBuscaProg_Click(object sender, EventArgs e)
        {
            //if (Gridtprog.Visible == true)
            //{
            //    Gridtprog.Visible = false;
            //}
            //else
            //{
            //    Gridtprog.Visible = true;
            //}
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopupProgramas", "$('#modalProgramas').modal('show')", true);
            Gridtprog.Visible = true;

            //string strQueryProgramas = "";
            //strQueryProgramas = " select tprog_clave Clave, tprog_desc Nombre, tnive_desc Nivel " +
            //    " from tprog, tnive where tprog_tnive_clave=tnive_clave  ";
            //strQueryProgramas = strQueryProgramas + " order  by Clave ";
            try
            {

                Gridtprog.DataSource = serviceCatalogo.obtenProgramaNivel();
                Gridtprog.DataBind();
                Gridtprog.DataMember = "Programas";
                Gridtprog.HeaderRow.TableSection = TableRowSection.TableHeader;
                Gridtprog.UseAccessibleHeader = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_Programas", "load_datatable_Programas();", true);
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
            }
            //}
        }

        protected void linkBttnBuscaSeriacion_Click(object sender, EventArgs e)
        {
            Busca_Seriacion();
            Global.consecutivo = 1;
        }

        protected void linkBttnBuscaSeri2_Click(object sender, EventArgs e)
        {
            Busca_Seriacion();
            Global.consecutivo = 2;
        }

        protected void linkBttnBuscaSeri3_Click(object sender, EventArgs e)
        {
            Busca_Seriacion();
            Global.consecutivo = 3;
        }
    }
}