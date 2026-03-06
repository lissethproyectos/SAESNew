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
    public partial class C_Demograficos : System.Web.UI.Page
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
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "show_tab", "show_pais();", true);
                    form_pais.Attributes.Add("style", "display:none");
                    form_estado.Attributes.Add("style", "display:none");
                    form_delegacion.Attributes.Add("style", "display:none");
                    form_zip.Attributes.Add("style", "display:none");
                    btn_pais.Attributes.Add("style", "display:none");
                    btn_estado.Attributes.Add("style", "display:none");
                    btn_delegacion.Attributes.Add("style", "display:none");
                    btn_zip.Attributes.Add("style", "display:none");
                    combo_estatus();
                    combo_pais();
                    combo_paises_deleg();
                    combo_paises_zip();
                    
                }
                
                grid_paises_bind();
                grid_estados_bind();
                grid_delegaciones_bind();
                grid_zip_bind();
            }
        }

        protected void combo_estatus()
        {
            estatus_pais.Items.Clear();
            estatus_pais.Items.Add(new ListItem("Activo", "A"));
            estatus_pais.Items.Add(new ListItem("Inactivo", "B"));
            estatus_estado.Items.Clear();
            estatus_estado.Items.Add(new ListItem("Activo", "A"));
            estatus_estado.Items.Add(new ListItem("Inactivo", "B"));
            e_deleg.Items.Clear();
            e_deleg.Items.Add(new ListItem("Activo", "A"));
            e_deleg.Items.Add(new ListItem("Inactivo", "B"));
            e_zip.Items.Clear();
            e_zip.Items.Add(new ListItem("Activo", "A"));
            e_zip.Items.Add(new ListItem("Inactivo", "B"));
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

        #region Metodos para la pestaña de paises

        protected void agregar_pais_Click(object sender, EventArgs e)
        {
            c_pais.CssClass = "form-control";
            n_pais.CssClass = "form-control";
            c_pais.Attributes.Remove("disabled");
            c_pais.Text = null;
            n_pais.Text = null;
            g_pais.Text = null;
            estatus_pais.SelectedValue = "A";
            update_pais.Attributes.Add("style", "display:none");
            save_pais.Attributes.Add("style", "display:Initial");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "p", "loader_stop();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Nuevo_Pais", "nuevo_pais();", true);
        }

        protected void grid_paises_bind()
        {
            string Query = "";
            Query = "SELECT TPAIS_CLAVE CLAVE, TPAIS_DESC NOMBRE, TPAIS_GENTIL GENTIL, TPAIS_ESTATUS ESTATUS_CODE, CASE WHEN TPAIS_ESTATUS='A' THEN 'Activo' ELSE 'Inactivo' END ESTATUS, DATE_FORMAT(TPAIS_DATE,'%d/%m/%Y') FECHA FROM TPAIS ORDER BY CLAVE";
            try
            {
                MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                ConexionMySql.Open();
                MySqlDataAdapter dataadapter = new MySqlDataAdapter(Query, ConexionMySql);
                DataSet ds = new DataSet();
                dataadapter.Fill(ds, "Paises");
                GridPaises.DataSource = ds;
                GridPaises.EditIndex = -1;
                GridPaises.DataBind();
                GridPaises.DataMember = "Paises";
                GridPaises.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridPaises.UseAccessibleHeader = true;


            }
            catch (Exception ex)
            {

            }

        }

        protected void insertar_pais()
        {
            if (valida_clave(c_pais.Text))
            {
                string Query = "INSERT INTO TPAIS VALUES ('" + c_pais.Text + "','" + n_pais.Text + "','" + g_pais.Text + "','" + Session["usuario"].ToString() + "',CURRENT_TIMESTAMP(),'" + estatus_pais.SelectedValue + "')";
                MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                ConexionMySql.Open();
                MySqlCommand mysqlcmd = new MySqlCommand(Query, ConexionMySql);
                mysqlcmd.CommandType = CommandType.Text;
                try
                {
                    mysqlcmd.ExecuteNonQuery();
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Valida_clave", "valida('pais_n');", true);
            }

        }

        protected void actualiza_pais()
        {

            string Query = "UPDATE tpais SET tpais_desc='" + n_pais.Text + "',tpais_gentil='" + g_pais.Text + "',tpais_tuser_clave='" + Session["usuario"].ToString() + "',tpais_date=CURRENT_TIMESTAMP(),tpais_estatus='" + estatus_pais.SelectedValue + "' WHERE tpais_clave='" + c_pais.Text + "'";
            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            ConexionMySql.Open();
            MySqlCommand mysqlcmd = new MySqlCommand(Query, ConexionMySql);
            mysqlcmd.CommandType = CommandType.Text;
            try
            {
                mysqlcmd.ExecuteNonQuery();
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

        protected bool valida_clave(string clave)
        {
            string Query = "";
            Query = "SELECT COUNT(*) Indicador FROM TPAIS WHERE TPAIS_CLAVE='" + clave + "'";
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
        protected void save_pais_Click(object sender, EventArgs e)
        {
            c_pais.CssClass = "form-control";
            n_pais.CssClass = "form-control";
            
            if (!String.IsNullOrEmpty(c_pais.Text.Trim()) && !String.IsNullOrEmpty(n_pais.Text.Trim()))
            {
                insertar_pais();
            }
            else
            {
                if (String.IsNullOrEmpty(c_pais.Text.Trim()) && String.IsNullOrEmpty(n_pais.Text.Trim()))
                {
                    c_pais.CssClass = "form-control focus";
                    n_pais.CssClass = "form-control focus";
                }
                else if(String.IsNullOrEmpty(c_pais.Text.Trim()))
                {
                    c_pais.CssClass = "form-control focus";                    
                }
                else
                {
                    n_pais.CssClass = "form-control focus";
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "incompletos_n", "incompletos('pais_n');", true);
            }

        }

        protected void update_pais_Click(object sender, EventArgs e)
        {
            n_pais.CssClass = "form-control";

            if (!String.IsNullOrEmpty(c_pais.Text.Trim()) && !String.IsNullOrEmpty(n_pais.Text.Trim()))
            {

                actualiza_pais();
            }
            else
            {
                n_pais.CssClass = "form-control focus";
                c_pais.Attributes.Add("disabled", "");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "incompletos_u", "incompletos('pais_u');", true);
            }
        }



        #endregion


        #region Métodos para la pestaña estados

        protected void grid_estados_bind()
        {
            string Query = "";
            Query = "SELECT TESTA_CLAVE CLAVE, TESTA_DESC NOMBRE,TESTA_TPAIS_CLAVE C_PAIS,TPAIS_DESC PAIS, TESTA_ESTATUS ESTATUS_CODE,CASE WHEN TESTA_ESTATUS='A' THEN 'Activo' ELSE 'Inactivo' END ESTATUS, DATE_FORMAT(TESTA_DATE,'%d/%m/%Y') FECHA " +
                    "FROM TESTA " +
                    "INNER JOIN TPAIS ON TPAIS_CLAVE = TESTA_TPAIS_CLAVE " +
                    "ORDER BY 1";
            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            ConexionMySql.Open();
            try
            {

                MySqlDataAdapter dataadapter = new MySqlDataAdapter(Query, ConexionMySql);
                DataSet ds = new DataSet();
                dataadapter.Fill(ds, "Estados");
                GridEstados.DataSource = ds;
                GridEstados.DataBind();
                GridEstados.DataMember = "Estados";
                GridEstados.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridEstados.UseAccessibleHeader = true;

            }
            catch (Exception ex)
            {

            }
            finally
            {
                ConexionMySql.Close();
            }

        }

        protected void combo_pais()
        {
            string Query = "SELECT TPAIS_CLAVE Clave,TPAIS_DESC Nombre FROM TPAIS WHERE TPAIS_ESTATUS='A'  " +
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

                cbo_pais.DataSource = TablaEstado;
                cbo_pais.DataValueField = "Clave";
                cbo_pais.DataTextField = "Nombre";
                cbo_pais.DataBind();

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
        protected void insertar_estado()
        {
            if (valida_clave_edo(c_estado.Text))
            {
                string Query = "INSERT INTO TESTA VALUES ('" + c_estado.Text + "','" + cbo_pais.SelectedValue+ "','" + n_estado.Text + "','" + Session["usuario"].ToString() + "',CURRENT_TIMESTAMP(),'" + estatus_estado.SelectedValue + "')";

                MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                ConexionMySql.Open();
                MySqlCommand mysqlcmd = new MySqlCommand(Query, ConexionMySql);
                mysqlcmd.CommandType = CommandType.Text;
                try
                {
                    mysqlcmd.ExecuteNonQuery();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar_e", "save();", true);
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Valida_clave_e", "valida('estado_n');", true);
            }
        }

        protected void actualizar_estado()
        {
            string Query = "UPDATE testa SET testa_desc='" + n_estado.Text + "',testa_tuser_clave='" + Session["usuario"].ToString() + "',testa_date=CURRENT_TIMESTAMP(),testa_estatus='" + estatus_estado.SelectedValue + "' WHERE testa_clave='" + c_estado.Text + "' AND testa_tpais_clave='" + cbo_pais.SelectedValue + "'";
            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            ConexionMySql.Open();
            MySqlCommand mysqlcmd = new MySqlCommand(Query, ConexionMySql);
            mysqlcmd.CommandType = CommandType.Text;
            try
            {
                mysqlcmd.ExecuteNonQuery();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "update_e", "update();", true);
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

        protected bool valida_clave_edo(string clave)
        {
            string Query = "";
            Query = "SELECT DISTINCT COUNT(*) Indicador " +
                    "FROM TESTA " +
                    "INNER JOIN TPAIS ON tpais_clave = testa_tpais_clave " +
                    "WHERE testa_tpais_clave = '" + cbo_pais.SelectedValue + "' " +
                    "AND testa_clave = '" + clave + "' ";

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

        protected void save_estado_Click(object sender, EventArgs e)
        {
            cbo_pais.CssClass = "form-control";
            c_estado.CssClass = "form-control";
            n_estado.CssClass = "form-control";

            if (cbo_pais.SelectedValue != "0" && !String.IsNullOrEmpty(c_estado.Text) && !String.IsNullOrEmpty(n_estado.Text))
            {
                insertar_estado();
            }
            else
            {
                if(cbo_pais.SelectedValue == "0" && String.IsNullOrEmpty(c_estado.Text) && String.IsNullOrEmpty(n_estado.Text))
                {
                    cbo_pais.CssClass = "form-control focus";
                    c_estado.CssClass = "form-control focus";
                    n_estado.CssClass = "form-control focus";
                }
                else if (cbo_pais.SelectedValue == "0")
                {
                    cbo_pais.CssClass = "form-control focus";
                    
                }
                else if (String.IsNullOrEmpty(c_estado.Text))
                {
                    c_estado.CssClass = "form-control focus";
                    
                }
                else
                {
                    n_estado.CssClass = "form-control focus";
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "incompletos_n_e", "incompletos('estado_n');", true);
            }
        }

        protected void update_estado_Click(object sender, EventArgs e)
        {
            n_estado.CssClass = "form-control";

            if (cbo_pais.SelectedValue != "0" && !String.IsNullOrEmpty(c_estado.Text) && !String.IsNullOrEmpty(n_estado.Text))
            {
                actualizar_estado();
            }
            else
            {
                n_estado.CssClass= "form-control focus";
                cbo_pais.Attributes.Add("disabled", "");
                c_estado.Attributes.Add("disabled", "");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "incompletos_u_e", "incompletos('estado_u');", true);
            }
        }

        protected void agregar_estado_Click(object sender, EventArgs e)
        {
            cbo_pais.CssClass= "form-control";
            c_estado.CssClass = "form-control";
            n_estado.CssClass = "form-control";
            c_estado.Attributes.Remove("disabled");
            cbo_pais.Attributes.Remove("disabled");
            c_estado.Text = null;
            n_estado.Text = null;
            cbo_pais.SelectedValue = "0";
            update_estado.Attributes.Add("style", "display:none");
            save_estado.Attributes.Add("style", "display:initial");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "e", "loader_stop();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Nuevo_Estado", "nuevo_estado();", true);
        }
        #endregion


        #region Métodos para la pestaña delegación

        protected void combo_paises_deleg()
        {
            cbop_deleg.Items.Clear();
            cboe_deleg.Items.Clear();
            cboe_deleg.Items.Add(new ListItem("----Selecciona un estado----", "0"));

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

                cbop_deleg.DataSource = TablaEstado;
                cbop_deleg.DataValueField = "Clave";
                cbop_deleg.DataTextField = "Nombre";
                cbop_deleg.DataBind();

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

        protected void cbop_deleg_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbop_deleg.SelectedValue == "0")
            {
                combo_paises_deleg();
            }
            else
            {
                cboe_deleg.Items.Clear();
                combo_estados_deleg();
            }

            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Nuevo_Delegacion", "nuevo_delegacion();", true);
            add_delegacion.Attributes.Add("style", "display:none");
            form_delegacion.Attributes.Add("style", "display:Block");
            btn_delegacion.Attributes.Remove("style");
            update_deleg.Attributes.Add("style", "display:none");
            save_deleg.Attributes.Add("style", "display:initial");
            
        }

        protected void combo_estados_deleg()
        {
            string Query = "SELECT TESTA_CLAVE Clave,TESTA_DESC Nombre FROM TESTA WHERE TESTA_ESTATUS='A' AND TESTA_TPAIS_CLAVE= " + cbop_deleg.SelectedValue +
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

                cboe_deleg.DataSource = TablaEstado;
                cboe_deleg.DataValueField = "Clave";
                cboe_deleg.DataTextField = "Nombre";
                cboe_deleg.DataBind();

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

        protected void grid_delegaciones_bind()
        {
            string Query = "";
            Query = "SELECT tdele_clave CLAVE,tdele_desc NOMBRE,tpais_clave C_PAIS,tpais_desc PAIS,testa_clave C_ESTADO,testa_desc ESTADO,tdele_estatus ESTATUS_CODE,CASE WHEN tdele_estatus='A' THEN 'Activo' ELSE 'Inactivo' END ESTATUS, DATE_FORMAT(tdele_date,'%d/%m/%Y') FECHA " +
                    "FROM TDELE " +
                    "INNER JOIN TESTA ON testa_clave = tdele_testa_clave AND testa_tpais_clave = tdele_tpais_clave " +
                    "INNER JOIN TPAIS ON tpais_clave = testa_tpais_clave " +
                    "ORDER BY 3,1";
            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            ConexionMySql.Open();
            try
            {

                MySqlDataAdapter dataadapter = new MySqlDataAdapter(Query, ConexionMySql);
                DataSet ds = new DataSet();
                dataadapter.Fill(ds, "Delegaciones");
                GridDelegacion.DataSource = ds;
                GridDelegacion.DataBind();
                GridDelegacion.DataMember = "Delegaciones";
                GridDelegacion.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridDelegacion.UseAccessibleHeader = true;

            }
            catch (Exception ex)
            {

            }
            finally
            {
                ConexionMySql.Close();
            }
        }

        protected void insertar_delegacion()
        {
            if (valida_clave_dele(c_deleg.Text))
            {
                string Query = "INSERT INTO TDELE VALUES ('" + c_deleg.Text + "','" + n_deleg.Text + "','" + cboe_deleg.SelectedValue + "','" + cbop_deleg.SelectedValue + "','" + Session["usuario"].ToString() + "',CURRENT_TIMESTAMP(),'" + e_deleg.SelectedValue + "')";
                
                MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                ConexionMySql.Open();
                MySqlCommand mysqlcmd = new MySqlCommand(Query, ConexionMySql);
                mysqlcmd.CommandType = CommandType.Text;
                try
                {
                    mysqlcmd.ExecuteNonQuery();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar_d", "save();", true);
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Valida_clave_d", "valida('delegacion_n');", true);
            }
        }

        protected void actualizar_delegacion()
        {
            string Query = "UPDATE tdele SET tdele_desc='" + n_deleg.Text + "',tdele_user='" + Session["usuario"].ToString() + "',tdele_date=CURRENT_TIMESTAMP(),tdele_estatus='" + e_deleg.SelectedValue + "' WHERE tdele_clave='" + c_deleg.Text + "' AND tdele_testa_clave='" + state_deleg.Value + "' AND tdele_tpais_clave='" + cbop_deleg.SelectedValue + "'";
            

            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            ConexionMySql.Open();
            MySqlCommand mysqlcmd = new MySqlCommand(Query, ConexionMySql);
            mysqlcmd.CommandType = CommandType.Text;
            try
            {
                mysqlcmd.ExecuteNonQuery();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "update_d", "update();", true);
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

        protected bool valida_clave_dele(string clave)
        {
            string Query = "";
            Query = "SELECT COUNT(*) Indicador FROM TDELE WHERE tdele_tpais_clave='"+cbop_deleg.SelectedValue+"' AND tdele_testa_clave='"+cboe_deleg.SelectedValue+"' AND tdele_clave='"+clave+"'";

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

        protected void save_deleg_Click(object sender, EventArgs e)
        {
            cbop_deleg.CssClass = "form-control";
            cboe_deleg.CssClass = "form-control";
            c_deleg.CssClass = "form-control";
            n_deleg.CssClass = "form-control";

            if (cbop_deleg.SelectedValue!="0"  && cboe_deleg.SelectedValue != "0" && !String.IsNullOrEmpty(c_deleg.Text) && !String.IsNullOrEmpty(n_deleg.Text))
            {
                insertar_delegacion();
            }
            else
            {
                if (cbop_deleg.SelectedValue == "0")
                {
                    cbop_deleg.CssClass = "form-control focus";
                }
                if (cboe_deleg.SelectedValue == "0")
                {
                    cboe_deleg.CssClass = "form-control focus";
                }
                if (String.IsNullOrEmpty(c_deleg.Text))
                {
                    c_deleg.CssClass = "form-control focus";
                }
                if (String.IsNullOrEmpty(n_deleg.Text))
                {
                    n_deleg.CssClass = "form-control focus";
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "incompletos_n_d", "incompletos('delegacion_n');", true);
            }
        }

        protected void update_deleg_Click(object sender, EventArgs e)
        {
            n_deleg.CssClass = "form-control";
            if (cbop_deleg.SelectedValue != "0" && state_deleg.Value != "0" && !String.IsNullOrEmpty(c_deleg.Text) && !String.IsNullOrEmpty(n_deleg.Text))
            {
                actualizar_delegacion();
            }
            else
            {
                combo_estados_deleg();
                cboe_deleg.SelectedValue = state_deleg.Value;
                n_deleg.CssClass = "form-control focus";
                cbop_deleg.Attributes.Add("disabled", "");
                cboe_deleg.Attributes.Add("disabled", "");
                c_deleg.Attributes.Add("disabled", "");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "incompletos_u_d", "incompletos('delegacion_u');", true);
            }

        }

        protected void agregar_delegacion_Click(object sender, EventArgs e)
        {
            cbop_deleg.CssClass = "form-control";
            cboe_deleg.CssClass = "form-control";
            c_deleg.CssClass = "form-control";
            n_deleg.CssClass = "form-control";
            combo_paises_deleg();
            c_deleg.Attributes.Remove("disabled");
            cbop_deleg.Attributes.Remove("disabled");
            cboe_deleg.Attributes.Remove("disabled");
            c_deleg.Text = null;
            n_deleg.Text = null;
            update_deleg.Attributes.Add("style", "display:none");
            save_deleg.Attributes.Add("style", "display:initial");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "d", "loader_stop();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Nuevo_Delegacion", "nuevo_delegacion();", true);
        }

        #endregion


        #region Metodos para la pestaña de Codigo Postal

        protected void combo_paises_zip()
        {
            cbop_zip.Items.Clear();
            cboe_zip.Items.Clear();
            cboe_zip.Items.Add(new ListItem("----Selecciona un estado----", "0"));
            cbod_zip.Items.Clear();
            cbod_zip.Items.Add(new ListItem("----Selecciona una delegación---", "0"));

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

                cbop_zip.DataSource = TablaEstado;
                cbop_zip.DataValueField = "Clave";
                cbop_zip.DataTextField = "Nombre";
                cbop_zip.DataBind();

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
        protected void combo_estados_zip()
        {
            string Query = "SELECT TESTA_CLAVE Clave,TESTA_DESC Nombre FROM TESTA WHERE TESTA_ESTATUS='A' AND TESTA_TPAIS_CLAVE= " + cbop_zip.SelectedValue +
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

                cboe_zip.DataSource = TablaEstado;
                cboe_zip.DataValueField = "Clave";
                cboe_zip.DataTextField = "Nombre";
                cboe_zip.DataBind();

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

        protected void combo_delegacion_zip()
        {
            string Query = "SELECT tdele_clave CLAVE,tdele_desc NOMBRE FROM TDELE WHERE tdele_tpais_clave='"+cbop_zip.SelectedValue+"' AND tdele_testa_clave='" + cboe_zip.SelectedValue + "' " +
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

                cbod_zip.DataSource = TablaEstado;
                cbod_zip.DataValueField = "Clave";
                cbod_zip.DataTextField = "Nombre";
                cbod_zip.DataBind();

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

        protected void grid_zip_bind()
        {
            string Query = "";
            Query = "SELECT DISTINCT tcopo_clave CLAVE, tcopo_desc NOMBRE,tpais_clave C_PAIS,tpais_desc PAIS,tcopo_testa_clave C_ESTADO,testa_desc ESTADO,tcopo_tdele_clave C_DELEGACION,tdele_desc DELEGACION,tcopo_estatus ESTATUS_CODE,CASE WHEN tcopo_estatus='A' THEN 'Activo' ELSE 'Inactivo' END ESTATUS, DATE_FORMAT(tcopo_date,'%d/%m/%Y') FECHA " +
                    "FROM TCOPO " +
                    "INNER JOIN TESTA ON testa_clave = tcopo_testa_clave and testa_tpais_clave = tcopo_tpais_clave " +
                    "INNER JOIN TDELE ON tdele_clave = tcopo_tdele_clave and tdele_testa_clave = testa_clave and tdele_tpais_clave = testa_tpais_clave " +
                    "INNER JOIN TPAIS ON tpais_clave = tcopo_tpais_clave " +
                    "ORDER BY 3,5,1";
            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            ConexionMySql.Open();
            try
            {

                MySqlDataAdapter dataadapter = new MySqlDataAdapter(Query, ConexionMySql);
                DataSet ds = new DataSet();
                dataadapter.Fill(ds, "Zip");
                GridZip.DataSource = ds;
                GridZip.DataBind();
                GridZip.DataMember = "Zip";
                GridZip.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridZip.UseAccessibleHeader = true;

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

        protected void agregar_zip_Click(object sender, EventArgs e)
        {

            cbop_zip.CssClass = "form-control";
            cboe_zip.CssClass = "form-control";
            cbod_zip.CssClass = "form-control";
            c_zip.CssClass = "form-control";
            n_zip.CssClass = "form-control";
            combo_paises_zip();
            c_zip.Text = null;
            n_zip.Text = null;
            cbop_zip.Attributes.Remove("disabled");
            cboe_zip.Attributes.Remove("disabled");
            cbod_zip.Attributes.Remove("disabled");
            c_zip.Attributes.Remove("disabled");
            update_zip.Attributes.Add("style", "display:none");
            save_zip.Attributes.Add("style", "display:initial");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "z", "loader_stop();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Nuevo_Zip", "nuevo_zip();", true);
        }

        protected void cbop_zip_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbop_zip.SelectedValue == "0")
            {
                combo_paises_zip();
            }
            else
            {
                
                cboe_zip.Items.Clear();
                combo_estados_zip();
                
            }
            add_zip.Attributes.Add("style", "display:none; margin-top:15px;");
            form_zip.Attributes.Add("style", "display:Block");
            btn_zip.Attributes.Remove("style");
            update_zip.Attributes.Add("style", "display:none");
            save_zip.Attributes.Add("style", "display:initial");

        }

        protected void cboe_zip_SelectedIndexChanged(object sender, EventArgs e)
        {
            combo_delegacion_zip();
            add_zip.Attributes.Add("style", "display:none; margin-top:15px;");
            form_zip.Attributes.Add("style", "display:Block");
            btn_zip.Attributes.Remove("style");
            update_zip.Attributes.Add("style", "display:none");
            save_zip.Attributes.Add("style", "display:initial");


        }

        protected void insertar_zip()
        {
            if (valida_clave_zip(c_zip.Text))
            {
                string Query = "INSERT INTO TCOPO VALUES ('" + c_zip.Text + "','" + n_zip.Text + "','" + cbop_zip.SelectedValue + "','" + cboe_zip.SelectedValue + "','" + cbod_zip.SelectedValue +"','" + Session["usuario"].ToString() + "',CURRENT_TIMESTAMP(),'" + e_zip.SelectedValue + "', NULL)";

                MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                ConexionMySql.Open();
                MySqlCommand mysqlcmd = new MySqlCommand(Query, ConexionMySql);
                mysqlcmd.CommandType = CommandType.Text;
                try
                {
                    mysqlcmd.ExecuteNonQuery();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar_z", "save();", true);
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Valida_clave_z", "valida('zip_n');", true);
            }

        }

        protected void actualizar_zip()
        {
            string Query = "UPDATE tcopo SET tcopo_desc='" + n_zip.Text + "',tcopo_user='" + Session["usuario"].ToString() + "',tcopo_date=CURRENT_TIMESTAMP(),tcopo_estatus='" + e_zip.SelectedValue + "' WHERE tcopo_clave='" + c_zip.Text + "' AND tcopo_tpais_clave='" + cbop_zip.SelectedValue + "' AND tcopo_testa_clave='" + state_zip.Value + "' AND tcopo_tdele_clave='"+ country_zip.Value+"'";


            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            ConexionMySql.Open();
            MySqlCommand mysqlcmd = new MySqlCommand(Query, ConexionMySql);
            mysqlcmd.CommandType = CommandType.Text;
            try
            {
                mysqlcmd.ExecuteNonQuery();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "update_Z", "update();", true);
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
        protected bool valida_clave_zip(string clave)
        {
            string Query = "";
            Query = "SELECT COUNT(*) Indicador FROM TCOPO WHERE tcopo_tpais_clave='" + cbop_zip.SelectedValue + "' AND tcopo_testa_clave='" + cboe_zip.SelectedValue + "'AND tcopo_tdele_clave='"+cbod_zip.SelectedValue+"' AND tcopo_clave='" + clave + "'";

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
        protected void save_zip_Click(object sender, EventArgs e)
        {
            cbop_zip.CssClass = "form-control";
            cboe_zip.CssClass = "form-control";
            cbod_zip.CssClass = "form-control";
            c_zip.CssClass = "form-control";
            n_zip.CssClass = "form-control";

            if (cbop_zip.SelectedValue !="0" && cboe_zip.SelectedValue != "0" && cbod_zip.SelectedValue != "0" && !String.IsNullOrEmpty(c_zip.Text) && !String.IsNullOrEmpty(n_zip.Text))
            {
                insertar_zip();
            }
            else
            {
                if (cbop_zip.SelectedValue == "0") { cbop_zip.CssClass = "form-control focus"; }
                if (cboe_zip.SelectedValue == "0") { cboe_zip.CssClass = "form-control focus"; }
                if (cbod_zip.SelectedValue == "0") { cbod_zip.CssClass = "form-control focus"; }
                if (String.IsNullOrEmpty(c_zip.Text)) { c_zip.CssClass = "form-control focus"; }
                if (String.IsNullOrEmpty(n_zip.Text)) { n_zip.CssClass = "form-control focus"; }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "incompletos_n_z", "incompletos('zip_n');", true);
            }
            

        }

        protected void update_zip_Click(object sender, EventArgs e)
        {
            n_zip.CssClass = "form-control";
            if (cbop_zip.SelectedValue != "0" && state_zip.Value != "0" && country_zip.Value!= "0" && !String.IsNullOrEmpty(c_zip.Text) && !String.IsNullOrEmpty(n_zip.Text))
            {
                actualizar_zip();
            }
            else
            {
                combo_estados_zip();
                cboe_zip.SelectedValue = state_zip.Value;
                combo_delegacion_zip();
                cbod_zip.SelectedValue = country_zip.Value;
                n_zip.CssClass = "form-control focus";
                cbop_zip.Attributes.Add("disabled", "");
                cboe_zip.Attributes.Add("disabled", "");
                cbod_zip.Attributes.Add("disabled", "");
                c_zip.Attributes.Add("disabled", "");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "incompletos_u_z", "incompletos('zip_u');", true);
            }
        }
        #endregion


    }
}