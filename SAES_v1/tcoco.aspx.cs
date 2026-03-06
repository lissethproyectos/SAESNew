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

namespace SAES_v1
{
    public partial class tcoco : System.Web.UI.Page
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

        private void LlenaPagina()
        {
            System.Threading.Thread.Sleep(50);

            string QerySelect = "select tusme_update, tusme_select from tuser, tusme, tmede " +
                              " where tuser_clave = '" + Session["usuario"].ToString() + "'" +
                              " and tusme_trole_clave = tuser_trole_clave and tusme_tmenu_clave = 6 and tusme_tmenu_clave = tmede_tmenu_clave " +
                              " and tusme_tmede_clave = tmede_clave and tmede_forma='tcoco' ";

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
                    if (dssql1.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        btn_tcoco.Visible = false;
                    }
                    grid_tcoco_bind();
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

        protected void combo_estatus()
        {
            ddl_estatus.Items.Clear();
            ddl_estatus.Items.Add(new ListItem("-----", "0"));
            ddl_estatus.Items.Add(new ListItem("Activo", "A"));
            ddl_estatus.Items.Add(new ListItem("Inactivo", "B"));

            ddl_naturaleza.Items.Clear();
            ddl_naturaleza.Items.Add(new ListItem("-----", "0"));
            ddl_naturaleza.Items.Add(new ListItem("Cargo", "C"));
            ddl_naturaleza.Items.Add(new ListItem("Pago", "P"));

            ddl_categoria.Items.Clear();
            ddl_categoria.Items.Add(new ListItem("-----", "0"));
            ddl_categoria.Items.Add(new ListItem("Colegiatura", "CO"));
            ddl_categoria.Items.Add(new ListItem("Servicios Varios", "SV"));
            ddl_categoria.Items.Add(new ListItem("Pagos Caja", "PC"));
            ddl_categoria.Items.Add(new ListItem("Pagos Bancos", "PB"));
            ddl_categoria.Items.Add(new ListItem("Becas", "BE"));
            ddl_categoria.Items.Add(new ListItem("Descuentos", "DE"));
            ddl_categoria.Items.Add(new ListItem("Recargos", "RE"));
            ddl_categoria.Items.Add(new ListItem("Baja", "BA"));

        }
        protected void grid_tcoco_bind()
        {
            string strQueryGrid = "";
            strQueryGrid = " select tcoco_clave clave, tcoco_desc nombre, tcoco_ind_parc parcialidad, tcoco_ind_reemb reembolso, tcoco_iva iva," +
                " tcoco_cta_contable contable, tcoco_tipo c_naturaleza ,CASE WHEN tcoco_tipo = 'C' THEN 'CARGO' when tcoco_tipo = 'P' then 'PAGO' END naturaleza, " +
                " tcoco_categ c_categoria,CASE WHEN tcoco_categ = 'CO' THEN 'COLEGIATURA' when tcoco_categ = 'SV' then 'SERVICIOS VARIOS' " +
                " WHEN tcoco_categ = 'PC' THEN 'PAGOS CAJA' WHEN tcoco_categ = 'PB' THEN 'PAGOS BANCOS' WHEN tcoco_categ = 'BE' THEN 'BECAS' " +
                " WHEN tcoco_categ = 'DE' THEN 'DESCUENTO' WHEN tcoco_categ = 'RE' THEN 'RECARGOS' end categoria, " +
              " tcoco_estatus c_estatus,CASE WHEN tcoco_estatus = 'A' THEN 'ACTIVO' ELSE 'INACTIVO' END Estatus, fecha(date_format(tcoco_date,'%Y-%m-%d')) fecha " +
              " from tcoco order by clave ";
            MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            conexion.Open();
            try
            {
                MySqlDataAdapter dataadapter = new MySqlDataAdapter(strQueryGrid, conexion);
                DataSet ds = new DataSet();
                dataadapter.Fill(ds, "Tcoco");
                Gridtcoco.DataSource = ds;
                Gridtcoco.DataBind();
                Gridtcoco.DataMember = "Tcoco";
                Gridtcoco.HeaderRow.TableSection = TableRowSection.TableHeader;
                Gridtcoco.UseAccessibleHeader = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcoco", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
            conexion.Close();
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            txt_tcoco.Text = null;
            txt_nombre.Text = null;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "desactivar_check1", "desactivar_check1();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "desactivar_check2", "desactivar_check2();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "desactivar_check3", "desactivar_check3();", true);
            txt_contable.Text = null;
            combo_estatus();
            btn_save.Visible = true;
            btn_update.Visible = false;
            txt_tcoco.Attributes.Remove("readonly");
            grid_tcoco_bind();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txt_tcoco.Text) && !String.IsNullOrEmpty(txt_nombre.Text) && !String.IsNullOrEmpty(txt_contable.Text) &&
                ddl_naturaleza.SelectedValue != "0" && ddl_categoria.SelectedValue != "0" && ddl_estatus.SelectedValue != "0")
            {
                if (valida_tcoco(txt_tcoco.Text))
                {
                    string parcialidad = "";
                    string selected = Request.Form["customSwitches1"];
                    if (selected == "on") { parcialidad = "S"; } else { parcialidad = "N"; }
                    string reembolso = "";
                    string selected1 = Request.Form["customSwitches2"];
                    if (selected1 == "on") { reembolso = "S"; } else { reembolso = "N"; }
                    string iva = "";
                    string selected2 = Request.Form["customSwitches3"];
                    if (selected2 == "on") { reembolso = "S"; } else { reembolso = "N"; }

                    string strCadSQL = "INSERT INTO tcoco Values ('" + txt_tcoco.Text + "','" + txt_nombre.Text + "','" +
                        parcialidad + "','" + reembolso + "','" + txt_contable.Text + "','" + ddl_naturaleza.SelectedValue + "','" +
                        ddl_categoria.SelectedValue + "','" + ddl_estatus.SelectedValue + "',current_timestamp(),'" + Session["usuario"].ToString() + "','" + iva + "')";
                    MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                    conexion.Open();
                    MySqlCommand mysqlcmd = new MySqlCommand(strCadSQL, conexion);
                    mysqlcmd.CommandType = CommandType.Text;
                    try
                    {
                        mysqlcmd.ExecuteNonQuery();
                        txt_tcoco.Text = null;
                        txt_nombre.Text = null;
                        txt_contable.Text = null;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "desactivar_check1", "desactivar_check1();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "desactivar_check2", "desactivar_check2();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "desactivar_check3", "desactivar_check3();", true);
                        combo_estatus();
                        grid_tcoco_bind();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                    }
                    catch (Exception ex)
                    {
                        string test = ex.Message;
                        string mensaje_error = ex.Message.Replace("'", "-");
                        Global.inserta_log(mensaje_error, "tcoco", Session["usuario"].ToString());
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
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validarClave('ContentPlaceHolder1_txt_tcoco',1);", true);
                    grid_tcoco_bind();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_tcoco();", true);
                grid_tcoco_bind();
            }


        }

        protected void btn_update_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txt_tcoco.Text) && !String.IsNullOrEmpty(txt_nombre.Text) && !String.IsNullOrEmpty(txt_contable.Text) &&
                ddl_naturaleza.SelectedValue != "0" && ddl_categoria.SelectedValue != "0" && ddl_estatus.SelectedValue != "0")
            {
                string parcialidad = "";
                string selected = Request.Form["customSwitches1"];
                if (selected == "on") { parcialidad = "S"; } else { parcialidad = "N"; }
                string reembolso = "";
                string selected1 = Request.Form["customSwitches2"];
                if (selected1 == "on") { reembolso = "S"; } else { reembolso = "N"; }
                string iva = "";
                string selected2 = Request.Form["customSwitches3"];
                if (selected2 == "on") { iva = "S"; } else { iva = "N"; }

                string strCadSQL = "UPDATE tcoco SET tcoco_desc='" + txt_nombre.Text + "', tcoco_estatus='" + ddl_estatus.SelectedValue + "', " +
                    " tcoco_cta_contable='" + txt_contable.Text + "', tcoco_ind_parc='" + parcialidad + "', tcoco_ind_reemb='" + reembolso + "', " +
                    " tcoco_tipo='" + ddl_naturaleza.SelectedValue + "', tcoco_categ='" + ddl_categoria.SelectedValue + "'," +
                    " tcoco_usuario='" + Session["usuario"].ToString() + "', tcoco_date=CURRENT_TIMESTAMP(), tcoco_iva='" + iva + "'" +
                    " WHERE tcoco_clave='" + txt_tcoco.Text + "'";
                MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                conexion.Open();
                MySqlCommand mysqlcmd = new MySqlCommand(strCadSQL, conexion);
                mysqlcmd.CommandType = CommandType.Text;
                try
                {
                    mysqlcmd.ExecuteNonQuery();
                    grid_tcoco_bind();
                    txt_tcoco.Text = null;
                    txt_nombre.Text = null;
                    txt_contable.Text = null;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "desactivar_check1", "desactivar_check1();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "desactivar_check2", "desactivar_check2();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "desactivar_check3", "desactivar_check3();", true);
                    combo_estatus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "update_p", "update();", true);
                }
                catch (Exception ex)
                {
                    string test = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tcoco", Session["usuario"].ToString());
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_tcoco();", true);
            }
        }

        protected bool valida_tcoco(string tcoco)
        {
            string Query = "";
            Query = "SELECT COUNT(*) Indicador FROM tcoco WHERE tcoco_clave='" + tcoco + "'";
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

        protected void Gridtcoco_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = Gridtcoco.SelectedRow;
            txt_tcoco.Text = row.Cells[1].Text;
            txt_nombre.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);

            if (row.Cells[3].Text == "S")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "activar_check1", "activar_check1();", true);
            }
            if (row.Cells[4].Text == "S")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "activar_check2", "activar_check2();", true);
            }
            if (row.Cells[5].Text == "S")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "activar_check3", "activar_check3();", true);
            }

            txt_contable.Text = HttpUtility.HtmlDecode(row.Cells[6].Text);
            combo_estatus();
            ddl_naturaleza.SelectedValue = row.Cells[7].Text;
            ddl_categoria.SelectedValue = row.Cells[8].Text;
            ddl_estatus.SelectedValue = row.Cells[9].Text;
            btn_update.Visible = true;
            btn_save.Visible = false;
            txt_tcoco.Attributes.Add("readonly", "");
            grid_tcoco_bind();
        }
    }
}