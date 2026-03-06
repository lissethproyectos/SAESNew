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
    public partial class tcamp : System.Web.UI.Page
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

                c_campus.Attributes.Add("onblur", "validarclaveCampus('ContentPlaceHolder1_c_campus',0)");
                c_campus.Attributes.Add("oninput", "validarclaveCampus('ContentPlaceHolder1_c_campus',0)");
                n_campus.Attributes.Add("onblur", "validarNombreCampus('ContentPlaceHolder1_n_campus')");
                n_campus.Attributes.Add("oninput", "validarNombreCampus('ContentPlaceHolder1_n_campus')");
                
                if (!IsPostBack)
                {
                    LlenaPagina();
                    combo_estatus();
                    combo_pais();
                }

            }
        }

        private void LlenaPagina()
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "menu", "carga_menu();", true);
            string QerySelect = "select tusme_update from tuser, tusme " +
                          " where tuser_clave = '" + Session["usuario"].ToString() + "'" +
                          " and tusme_trole_clave = tuser_trole_clave and tusme_tmenu_clave = 1 and tusme_tmede_clave = 9 ";

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
                //if (dssql1.Tables[0].Rows.Count == 0 || dssql1.Tables[0].Rows[0][1].ToString() == "0")
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                //}
                if (dssql1.Tables[0].Rows[0][0].ToString() == "1")
                {
                    btn_campus.Visible = true;
                }
                grid_campus_bind();
            }
            catch (Exception ex)
            {
                /// Logs
            }
        }
        protected void combo_estatus()
        {
            estatus_campus.Items.Clear();
            estatus_campus.Items.Add(new ListItem("Activo", "A"));
            estatus_campus.Items.Add(new ListItem("Inactivo", "B"));
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
        protected void combo_pais()
        {

            dde_campus.Items.Clear();
            dde_campus.Items.Add(new ListItem("----Selecciona un estado----", "0"));
            ddd_campus.Items.Clear();
            ddd_campus.Items.Add(new ListItem("----Selecciona una delegación---", "0"));
            ddz_campus.Items.Clear();
            ddz_campus.Items.Add(new ListItem("----Selecciona una colonia---", "0"));

            string Query = "SELECT TPAIS_CLAVE Clave,TPAIS_DESC Nombre FROM TPAIS WHERE TPAIS_ESTATUS='A' " +
                           "UNION " +
                           "SELECT '0' Clave,'----Selecciona un país----' Nombre " +
                           "ORDER BY 1";
            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            ConexionMySql.Open();
            DataTable TablaEstado = new DataTable();
            MySqlCommand ConsultaMySql = new MySqlCommand();
            MySqlDataReader DatosMySql;
            try
            {
                ConsultaMySql.Connection = ConexionMySql;
                ConsultaMySql.CommandType = CommandType.Text;
                ConsultaMySql.CommandText = Query;
                DatosMySql = ConsultaMySql.ExecuteReader();
                TablaEstado.Load(DatosMySql, LoadOption.OverwriteChanges);

                ddp_campus.DataSource = TablaEstado;
                ddp_campus.DataValueField = "Clave";
                ddp_campus.DataTextField = "Nombre";
                ddp_campus.DataBind();

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
        protected void combo_estado(string clave_pais)
        {
            string Query = "SELECT TESTA_CLAVE Clave,TESTA_DESC Nombre FROM TESTA WHERE TESTA_ESTATUS='A' AND TESTA_TPAIS_CLAVE= " + clave_pais +
                           " UNION " +
                           "SELECT '0' Clave,'----Selecciona un estado----' Nombre " +
                           "ORDER BY 1";
            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            ConexionMySql.Open();
            DataTable TablaEstado = new DataTable();
            MySqlCommand ConsultaMySql = new MySqlCommand();
            MySqlDataReader DatosMySql;
            try
            {
                ConsultaMySql.Connection = ConexionMySql;
                ConsultaMySql.CommandType = CommandType.Text;
                ConsultaMySql.CommandText = Query;
                DatosMySql = ConsultaMySql.ExecuteReader();
                TablaEstado.Load(DatosMySql, LoadOption.OverwriteChanges);

                dde_campus.DataSource = TablaEstado;
                dde_campus.DataValueField = "Clave";
                dde_campus.DataTextField = "Nombre";
                dde_campus.DataBind();

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
        protected void combo_delegacion(string clave_pais, string clave_edo)
        {
            string Query = "SELECT tdele_clave CLAVE,tdele_desc NOMBRE FROM TDELE WHERE tdele_tpais_clave='" + clave_pais + "' AND tdele_testa_clave='" + clave_edo + "' " +
                           "UNION " +
                           "SELECT '0' CLAVE,'----Selecciona una delegación----' NOMBRE " +
                           "ORDER BY 1";
            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            ConexionMySql.Open();
            DataTable TablaEstado = new DataTable();
            MySqlCommand ConsultaMySql = new MySqlCommand();
            MySqlDataReader DatosMySql;
            try
            {
                ConsultaMySql.Connection = ConexionMySql;
                ConsultaMySql.CommandType = CommandType.Text;
                ConsultaMySql.CommandText = Query;
                DatosMySql = ConsultaMySql.ExecuteReader();
                TablaEstado.Load(DatosMySql, LoadOption.OverwriteChanges);

                ddd_campus.DataSource = TablaEstado;
                ddd_campus.DataValueField = "Clave";
                ddd_campus.DataTextField = "Nombre";
                ddd_campus.DataBind();

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
        protected void combo_zip(string clave_pais, string clave_edo, string clave_deleg)
        {
            string Query = "SELECT DISTINCT ROW_NUMBER() OVER (PARTITION BY tcopo_clave,tcopo_tpais_clave,tcopo_testa_clave,tcopo_tdele_clave ORDER BY tcopo_clave) Clave, tcopo_desc Nombre FROM tcopo WHERE tcopo_tpais_clave='" + clave_pais + "' AND tcopo_testa_clave='" + clave_edo + "' AND tcopo_tdele_clave='" + clave_deleg + "' AND tcopo_clave='" + zip_campus.Text + "' " +
                            "UNION " +
                            "SELECT '0' CLAVE,'----Selecciona una colonia----' NOMBRE " +
                            "ORDER BY 1";
            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            ConexionMySql.Open();
            DataTable TablaEstado = new DataTable();
            MySqlCommand ConsultaMySql = new MySqlCommand();
            MySqlDataReader DatosMySql;
            try
            {
                ConsultaMySql.Connection = ConexionMySql;
                ConsultaMySql.CommandType = CommandType.Text;
                ConsultaMySql.CommandText = Query;
                DatosMySql = ConsultaMySql.ExecuteReader();
                TablaEstado.Load(DatosMySql, LoadOption.OverwriteChanges);

                ddz_campus.DataSource = TablaEstado;
                ddz_campus.DataValueField = "Clave";
                ddz_campus.DataTextField = "Nombre";
                ddz_campus.DataBind();

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
        protected void grid_campus_bind()
        {
            string Query = "";
            Query = "SELECT DISTINCT  tcamp_clave Clave, tcamp_desc Nombre, tcamp_abr Abreviatura,tcamp_rfc RFC,tcamp_estatus Estatus_Code, " +
                    "CASE WHEN tcamp_estatus = 'A' THEN 'ACTIVO' ELSE 'INACTIVO' END Estatus, DATE_FORMAT(tcamp_date, '%d/%m/%Y') Fecha, " +
                    "tcamp_tpais_clave C_Pais, tcamp_testa_clave C_Estado,testa_desc N_Estado, tcamp_tdele_clave C_Dele,tdele_desc N_Dele, tcamp_tcopo_clave ZIP,tcamp_colonia Colonia, tcamp_calle Direccion " +
                        "FROM tcamp " +
                        "LEFT JOIN testa ON testa_clave = tcamp_testa_clave AND testa_tpais_clave = tcamp_tpais_clave " +
                    "LEFT JOIN tdele ON tdele_clave = tcamp_tdele_clave AND tdele_testa_clave = tcamp_testa_clave AND tdele_tpais_clave = tcamp_tpais_clave " +
                    "ORDER BY 1";
            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            try
            {
                
                ConexionMySql.Open();
                MySqlDataAdapter dataadapter = new MySqlDataAdapter(Query, ConexionMySql);
                DataSet ds = new DataSet();
                dataadapter.Fill(ds, "Campus");
                GridCampus.DataSource = ds;
                GridCampus.EditIndex = -1;
                GridCampus.DataBind();
                GridCampus.DataMember = "Campus";
                GridCampus.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridCampus.UseAccessibleHeader = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);

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
        protected bool validar_clave_campus(string clave)
        {
            string Query = "";
            Query = "SELECT COUNT(*) Indicador FROM tcamp WHERE tcamp_clave='" + clave + "'";
            MySqlCommand cmd = new MySqlCommand(Query);
            DataTable dt = GetData(cmd);
            if (dt.Rows[0]["Indicador"].ToString() != "0")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        protected void GridCampus_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridCampus.SelectedRow;
            c_campus.Text= row.Cells[1].Text;
            c_campus.Attributes.Add("readonly","");
            n_campus.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);
            a_campus.Text = HttpUtility.HtmlDecode(row.Cells[3].Text);
            RFC_campus.Text = HttpUtility.HtmlDecode(row.Cells[4].Text);
            combo_estatus();
            estatus_campus.SelectedValue = row.Cells[5].Text;
            combo_pais();
            ddp_campus.SelectedValue = row.Cells[8].Text;
            combo_estado(ddp_campus.SelectedValue);
            dde_campus.SelectedValue = row.Cells[9].Text;
            combo_delegacion(ddp_campus.SelectedValue, dde_campus.SelectedValue);
            ddd_campus.SelectedValue = row.Cells[11].Text;
            zip_campus.Text = HttpUtility.HtmlDecode(row.Cells[13].Text);
            combo_zip(ddp_campus.SelectedValue, dde_campus.SelectedValue, ddd_campus.SelectedValue);
            string col_text = HttpUtility.HtmlDecode(row.Cells[14].Text); 
            if (col_text == null || col_text.Trim() == "") { ddz_campus.SelectedValue = "0"; } else { ddz_campus.SelectedValue = ddz_campus.Items.FindByText(col_text).Value; ; }            
            direc_campus.Text = HttpUtility.HtmlDecode(row.Cells[15].Text);
            guardar_campus.Visible = false;
            actualizar_campus.Visible = true;
            grid_campus_bind();
        }

        protected void cancelar_campus_Click(object sender, EventArgs e)
        {
            c_campus.Text = null;
            c_campus.Attributes.Remove("readonly");
            n_campus.Text = null;
            a_campus.Text = null;
            RFC_campus.Text = null;
            zip_campus.Text = null;
            direc_campus.Text = null;
            combo_estatus();
            combo_pais();
            guardar_campus.Visible = true;
            actualizar_campus.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
            grid_campus_bind();
        }

        protected void guardar_campus_Click(object sender, EventArgs e)
        {
            if(!String.IsNullOrEmpty(c_campus.Text) && !String.IsNullOrEmpty(n_campus.Text))
            {
                if (validar_clave_campus(c_campus.Text))
                {
                    string colonia = null;
                    if (ddz_campus.SelectedValue != "0") { colonia = ddz_campus.SelectedItem.Text; }

                    string Query = "INSERT INTO tcamp Values ('" + c_campus.Text + "','" + n_campus.Text + "','" +
                                     direc_campus.Text + "','" + colonia + "','" + ddp_campus.SelectedValue + "','" +
                                     dde_campus.SelectedValue + "','" + ddd_campus.SelectedValue + "','" +
                                     zip_campus.Text + "','" + Session["usuario"].ToString() + "',current_timestamp(),'" + estatus_campus.SelectedValue + "','" + a_campus.Text + "','" + RFC_campus.Text + "')";
                    MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                    ConexionMySql.Open();
                    MySqlCommand mysqlcmd = new MySqlCommand(Query, ConexionMySql);
                    mysqlcmd.CommandType = CommandType.Text;
                    try
                    {
                        mysqlcmd.ExecuteNonQuery();
                        c_campus.Text = null;
                        n_campus.Text = null;
                        zip_campus.Text = null;
                        combo_estatus();
                        combo_pais();
                        grid_campus_bind();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
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
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validarclaveCampus('ContentPlaceHolder1_c_campus',1);", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_campus();", true);
            }
        }

        protected void actualizar_campus_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(c_campus.Text) && !String.IsNullOrEmpty(n_campus.Text))
            {
                string colonia = null;
                if (ddz_campus.SelectedValue != "0") { colonia = ddz_campus.SelectedItem.Text; }

                string Query = "UPDATE tcamp SET tcamp_desc='" + n_campus.Text + "',tcamp_calle='" + direc_campus.Text + "',tcamp_colonia='" + colonia+ "',tcamp_tpais_clave='" + ddp_campus.SelectedValue + "',tcamp_testa_clave='" + dde_campus.SelectedValue + "',tcamp_tdele_clave='" + ddd_campus.SelectedValue + "',tcamp_tcopo_clave='" + zip_campus.Text + "',tcamp_user='" + Session["usuario"].ToString() + "',tcamp_date=current_timestamp(),tcamp_estatus='" + estatus_campus.SelectedValue + "',tcamp_abr='" + a_campus.Text + "',tcamp_rfc='" + RFC_campus.Text + "' WHERE tcamp_clave='" + c_campus.Text + "'";
                MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                ConexionMySql.Open();
                MySqlCommand mysqlcmd = new MySqlCommand(Query, ConexionMySql);
                mysqlcmd.CommandType = CommandType.Text;
                try
                {
                    mysqlcmd.ExecuteNonQuery();
                    //grid_campus_bind();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "update_p", "update();", true);
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
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_campus();", true);
            }
        }

        protected void ddp_campus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddp_campus.SelectedValue != "0")
            {
                combo_estado(ddp_campus.SelectedValue);
                grid_campus_bind();
            }
            else
            {
                combo_pais();
                zip_campus.Text = null;
                grid_campus_bind();
            }
        }

        protected void dde_campus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dde_campus.SelectedValue != "0")
            {
                combo_delegacion(ddp_campus.SelectedValue, dde_campus.SelectedValue);
            }
            else
            {
                ddd_campus.Items.Clear();
                ddd_campus.Items.Add(new ListItem("----Selecciona una delegación---", "0"));
                ddz_campus.Items.Clear();
                ddz_campus.Items.Add(new ListItem("----Selecciona una colonia---", "0"));
                zip_campus.Text = null;
            }
        }
        protected void zip_campus_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(zip_campus.Text))
            {
                combo_zip(ddp_campus.SelectedValue, dde_campus.SelectedValue, ddd_campus.SelectedValue);
            }
        }
    }
}