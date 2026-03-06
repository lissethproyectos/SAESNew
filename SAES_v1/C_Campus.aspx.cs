using MySql.Data.MySqlClient;
using Newtonsoft.Json;
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
    public partial class C_Campus : System.Web.UI.Page
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
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "show_tab", "show_pais();", true);
                    form_Campus.Attributes.Add("style", "display:none");
                    btn_campus.Attributes.Add("style", "display:none");
                    combo_estatus();
                    ///Llamado a combos pestaña campus///
                    combo_pais();

                    ///Llamado a combos pestaña Programa///
                    combo_campus();
                    c_prog_campus.Attributes.Add("onblur", "validarclavePrograma('ContentPlaceHolder1_c_prog_campus')");
                    c_prog_campus.Attributes.Add("oninput", "validarclavePrograma('ContentPlaceHolder1_c_prog_campus')");
                    update_prog.Attributes.Add("style", "display:none");
                    btn_programa.Attributes.Add("style", "display:none");

                    ///Llamado a combos pestaña cobranza///
                    actualizar_cob.Attributes.Add("style", "display:none");
                    dd_term.Attributes.Add("style", "display:none");
                    combo_campus_cobranza();
                    combo_tipo_periodo();
                    combo_concepto_calendario();
                    combo_concepto_cobranza();

                }
                grid_campus_bind();
            }
        }


        protected void combo_estatus()
        {
            estatus_campus.Items.Clear();
            estatus_campus.Items.Add(new ListItem("Activo", "A"));
            estatus_campus.Items.Add(new ListItem("Inactivo", "B"));
            e_prog_campus.Items.Clear();
            e_prog_campus.Items.Add(new ListItem("Activo", "A"));
            e_prog_campus.Items.Add(new ListItem("Inactivo", "B"));
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

        #region Métodos para pestañana de Campus

        protected void combo_pais()
        {

            dde_campus.Items.Clear();
            dde_campus.Items.Add(new ListItem("----Selecciona un estado----", "0"));
            ddd_campus.Items.Clear();
            ddd_campus.Items.Add(new ListItem("----Selecciona una delegación---", "0"));
            ddz_campus.Items.Clear();
            ddz_campus.Items.Add(new ListItem("----Selecciona una código postal---", "0"));

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

        protected void combo_estado( string clave_pais)
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

        protected void combo_delegacion(string clave_pais,string clave_edo)
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

        protected void combo_zip(string clave_pais, string clave_edo,string clave_deleg)
        {
            string Query = "SELECT DISTINCT ROW_NUMBER() OVER (PARTITION BY tcopo_clave,tcopo_tpais_clave,tcopo_testa_clave,tcopo_tdele_clave ORDER BY tcopo_clave) Clave, tcopo_desc Nombre FROM tcopo WHERE tcopo_tpais_clave='" + clave_pais+"' AND tcopo_testa_clave='"+clave_edo+"' AND tcopo_tdele_clave='"+clave_deleg+"' AND tcopo_clave='"+zip_campus.Text+"' "+
                            "UNION " +
                            "SELECT '0' CLAVE,'----Selecciona una código postal----' NOMBRE " +
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
            Query = "SELECT DISTINCT  tcamp_clave Clave, tcamp_desc Nombre, tcamp_abr Abreviatura,tcamp_rfc RFC,tcamp_estatus Estatus_Code, "+
                    "CASE WHEN tcamp_estatus = 'A' THEN 'ACTIVO' ELSE 'INACTIVO' END Estatus, DATE_FORMAT(tcamp_date, '%d/%m/%Y') Fecha, " +
                    "tcamp_tpais_clave C_Pais, tcamp_testa_clave C_Estado,testa_desc N_Estado, tcamp_tdele_clave C_Dele,tdele_desc N_Dele, tcamp_tcopo_clave ZIP,tcamp_colonia Colonia, tcamp_calle Direccion " +
                        "FROM tcamp " +
                        "INNER JOIN testa ON testa_clave = tcamp_testa_clave AND testa_tpais_clave = tcamp_tpais_clave " +
                    "INNER JOIN tdele ON tdele_clave = tcamp_tdele_clave AND tdele_testa_clave = tcamp_testa_clave AND tdele_tpais_clave = tcamp_tpais_clave " +
                    "ORDER BY 1";
            try
            {
                MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
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


            }
            catch (Exception ex)
            {
                string test = ex.Message;
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

        protected void insertar_campus()
        {

            if (valida_clave(c_campus.Value))
            {
                string Query = "INSERT INTO tcamp Values ('" + c_campus.Value + "','" + n_campus.Value+ "','" +
                                 direc_campus.Value + "','" + ddz_campus.SelectedItem.Text + "','" + ddp_campus.SelectedValue + "','" +
                                 dde_campus.SelectedValue + "','" + ddd_campus.SelectedValue + "','" +
                                 zip_campus.Text + "','" + Session["usuario"].ToString() + "',current_timestamp(),'" + estatus_campus.SelectedValue + "','" + a_campus.Value + "','" + RFC_campus.Value + "')";
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
                
            }
        }
        protected bool valida_clave(string clave)
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
        protected void actualizar_campus_db()
        {
            string colonia = "";
            string estado = "";
            string delegacion = "";
            if (ddz_campus.SelectedValue == "0") { colonia = hd_ddz_campus.Value; } else { colonia = ddz_campus.SelectedItem.Text; }
            if (dde_campus.SelectedValue == "0") { estado = hd_dde_campus.Value; } else { estado = dde_campus.SelectedValue; }
            if (ddd_campus.SelectedValue == "0") { delegacion = hd_ddd_campus.Value; } else { delegacion = ddd_campus.SelectedValue; }
            string Query = "UPDATE tcamp SET tcamp_desc='"+n_campus.Value+"',tcamp_calle='"+direc_campus.Value+"',tcamp_colonia='"+colonia+"',tcamp_tpais_clave='"+ddp_campus.SelectedValue+"',tcamp_testa_clave='"+estado+"',tcamp_tdele_clave='"+delegacion+"',tcamp_tcopo_clave='"+zip_campus.Text+"',tcamp_user='"+ Session["usuario"].ToString() + "',tcamp_date=current_timestamp(),tcamp_estatus='"+estatus_campus.SelectedValue+"',tcamp_abr='"+a_campus.Value+"',tcamp_rfc='"+RFC_campus.Value+"' WHERE tcamp_clave='"+c_campus.Value+"'";
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

        protected void ddp_campus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddp_campus.SelectedValue != "0")
            {
                combo_estado(ddp_campus.SelectedValue);
            }
        }

        protected void dde_campus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dde_campus.SelectedValue != "0")
            {
                combo_delegacion(ddp_campus.SelectedValue, dde_campus.SelectedValue);
            }
        }

        protected void zip_campus_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(zip_campus.Text))
            {
                combo_zip(ddp_campus.SelectedValue, dde_campus.SelectedValue, ddd_campus.SelectedValue);
            }
        }

        protected void agregar_Campus_Click(object sender, EventArgs e)
        {
            c_campus.Value = null;
            n_campus.Value = null;
            a_campus.Value = null;
            RFC_campus.Value = null;
            zip_campus.Text = null;
            direc_campus.Value = null;
            estatus_campus.SelectedValue = "A";
            upd_btn_dir.Attributes.Add("style", "display:none");
            combo_pais();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "p_c", "loader_stop();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Nuevo_Campus", "nuevo_campus();", true);
        }

        protected void upd_btn_dir_Click(object sender, EventArgs e)
        {
            string test = hd_ddp_campus.Value;
            combo_estado(hd_ddp_campus.Value);
            dde_campus.SelectedValue = hd_dde_campus.Value;
            combo_delegacion(hd_ddp_campus.Value, hd_dde_campus.Value);
            ddd_campus.SelectedValue = hd_ddd_campus.Value;
            combo_zip(hd_ddp_campus.Value, hd_dde_campus.Value, hd_ddd_campus.Value);
            upd_btn_dir.Attributes.Add("style", "display:none");
        }

        protected void guardar_campus_Click(object sender, EventArgs e)
        {
            if(!String.IsNullOrEmpty(c_campus.Value) && !String.IsNullOrEmpty(n_campus.Value))
            {
                insertar_campus();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_campus();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Nuevo_Campus_Save", "nuevo_campus();", true);
            }
            
        }

        protected void actualizar_campus_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(c_campus.Value) && !String.IsNullOrEmpty(n_campus.Value))
            {
                actualizar_campus_db();
            }
            else
            {
                combo_estado(hd_ddp_campus.Value);
                dde_campus.SelectedValue = hd_dde_campus.Value;
                combo_delegacion(hd_ddp_campus.Value, hd_dde_campus.Value);
                ddd_campus.SelectedValue = hd_ddd_campus.Value;
                combo_zip(hd_ddp_campus.Value, hd_dde_campus.Value, hd_ddd_campus.Value);
                ddz_campus.SelectedItem.Text = hd_ddz_campus.Value;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_campus();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "update_campus", "update_campus();", true);
            }
            
        }
        #endregion

        #region Métodos para la pestaña de Programas

        protected void combo_campus()
        {
            string Query = "SELECT DISTINCT tcamp_clave Clave, tcamp_desc Campus FROM tcamp "+
                            "UNION " +
                            "SELECT DISTINCT '0','----Selecciona un Campus----' Campus  " +
                            "ORDER BY 1";
            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            ConexionMySql.Open();
            DataTable TablaCampus = new DataTable();
            MySqlCommand ConsultaMySql = new MySqlCommand();
            MySqlDataReader DatosMySql;
            try
            {
                ConsultaMySql.Connection = ConexionMySql;
                ConsultaMySql.CommandType = CommandType.Text;
                ConsultaMySql.CommandText = Query;
                DatosMySql = ConsultaMySql.ExecuteReader();
                TablaCampus.Load(DatosMySql, LoadOption.OverwriteChanges);
                search_campus.DataSource = TablaCampus;
                search_campus.DataValueField = "Clave";
                search_campus.DataTextField = "Campus";
                search_campus.DataBind();

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

        protected void grid_programas_bind(string campus)
        {
            string Query = "";
            Query = "SELECT tcapr_tprog_clave Clave,tprog_desc Nombre,tnive_desc Nivel, tmoda_desc Modalidad, tcapr_ind_admi Admision,tcapr_estatus Estatus_code, " +
                    "CASE WHEN tcapr_estatus = 'A' THEN 'ACTIVO' ELSE 'INACTIVO' END Estatus, DATE_FORMAT(tcapr_date, '%d/%m/%Y') Fecha " +
                    "FROM tcapr " +
                    "INNER JOIN tprog ON tprog_clave = tcapr_tprog_clave " +
                    "INNER JOIN tnive ON tnive_clave = tprog_tnive_clave " +
                    "INNER JOIN tmoda ON tmoda_clave = tprog_tmoda_clave " +
                    "WHERE tcapr_tcamp_clave = '"+campus+"' " +
                    "ORDER BY 1";
            try
            {
                MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                ConexionMySql.Open();
                MySqlDataAdapter dataadapter = new MySqlDataAdapter(Query, ConexionMySql);
                DataSet ds = new DataSet();
                dataadapter.Fill(ds, "Programas");
                GridProgramas.DataSource = ds;
                GridProgramas.EditIndex = -1;
                GridProgramas.DataBind();
                GridProgramas.DataMember = "Programas";
                GridProgramas.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridProgramas.UseAccessibleHeader = true;
                GridProgramas.Visible = true;

            }
            catch (Exception ex)
            {
                string test = ex.Message;
            }
        }

        protected bool validar_clave_programa(string clave)
        {
            string Query = "";
            Query = "SELECT COUNT(*) Indicador FROM tprog WHERE tprog_clave='" + clave + "'";
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

        protected void insertar_programa_c()
        {
            string admision = "";
            if (Page.Request.Form["customSwitches"].ToString() == "on") { admision = "S"; } else { admision = "N"; }
            string Query = "INSERT INTO tcapr Values ('" + search_campus.SelectedValue + "','" + c_prog_campus.Text + "','" + admision + "','" + Session["usuario"].ToString() + "',  current_timestamp() ,'" + e_prog_campus.SelectedValue + "')";
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

        protected void actualizar_programa_c()
        {
            
            string admision = "";
            if (Page.Request.Form["customSwitches"].ToString() == "on") { admision = "S"; } else { admision = "N"; }
            string Query = "UPDATE tcapr SET tcapr_ind_admi='" + admision + "',tcapr_estatus='" + e_prog_campus.SelectedValue + "',tcapr_date= current_timestamp(),tcapr_tuser_clave='" + Session["usuario"].ToString() + "' WHERE tcapr_tcamp_clave='" + search_campus.SelectedValue + "' AND tcapr_tprog_clave='" + c_prog_campus.Text + "'";
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

        protected void search_campus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (search_campus.SelectedValue != "0")
            {
                c_prog_campus.Text = null;
                n_prog_campus.Text = null;
                grid_programas_bind(search_campus.SelectedValue);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "btn_programa();", true);
            }
            else
            {                
                ScriptManager.RegisterStartupScript(this, this.GetType(), "btn_ocultar", "btn_programa_ocultar();", true);
            }
            
        }
        protected void c_prog_campus_TextChanged(object sender, EventArgs e)
        {
            if (!validar_clave_programa(c_prog_campus.Text))
            {
                string Query = "SELECT DISTINCT tprog_desc Programa FROM tprog WHERE tprog_clave='" + c_prog_campus.Text + "'";
                MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);

                ConexionMySql.Open();
                MySqlDataAdapter mysqladapter = new MySqlDataAdapter();
                DataSet dsmysql = new DataSet();
                MySqlCommand cmdmysql = new MySqlCommand(Query, ConexionMySql);
                mysqladapter.SelectCommand = cmdmysql;
                mysqladapter.Fill(dsmysql);
                mysqladapter.Dispose();
                cmdmysql.Dispose();
                ConexionMySql.Close();
                n_prog_campus.Text = dsmysql.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                n_prog_campus.Text = null;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "validar_clave", "validarclavePrograma_N('ContentPlaceHolder1_c_prog_campus',1);", true);
            }


        }

        protected void guardar_prog_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(c_prog_campus.Text))
            {
                insertar_programa_c();
            }
            else
            {
                grid_programas_bind(search_campus.SelectedValue);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "btn_programa();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "validar_clave", "validarclavePrograma('ContentPlaceHolder1_c_prog_campus');", true);
            }
            
        }

        protected void update_prog_Click(object sender, EventArgs e)
        {
            
            actualizar_programa_c();
        }

        protected void cancelar_prog_Click(object sender, EventArgs e)
        {
            c_prog_campus.Text = null;
            n_prog_campus.Text = null;
            grid_programas_bind(search_campus.SelectedValue);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "btn_programa();", true);
        }
        #endregion

        #region Métodos para la pestaña de cobranza


        protected void combo_campus_cobranza()
        {
            cobranza_n.Items.Clear();
            cobranza_n.Items.Add(new ListItem("----Selecciona un Nivel----", "0"));

            string Query = "SELECT DISTINCT tcamp_clave Clave, tcamp_desc Campus FROM tcamp " +
                            "UNION " +
                            "SELECT DISTINCT '0','----Selecciona un Campus----' Campus  " +
                            "ORDER BY 1";
            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            ConexionMySql.Open();
            DataTable TablaCampus = new DataTable();
            MySqlCommand ConsultaMySql = new MySqlCommand();
            MySqlDataReader DatosMySql;
            try
            {
                ConsultaMySql.Connection = ConexionMySql;
                ConsultaMySql.CommandType = CommandType.Text;
                ConsultaMySql.CommandText = Query;
                DatosMySql = ConsultaMySql.ExecuteReader();
                TablaCampus.Load(DatosMySql, LoadOption.OverwriteChanges);
                cobranza_c.DataSource = TablaCampus;
                cobranza_c.DataValueField = "Clave";
                cobranza_c.DataTextField = "Campus";
                cobranza_c.DataBind();

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
        
        protected void combo_nivel_cobranza(string campus)
        {
            cobranza_n.Items.Clear();
            string Query = "SELECT DISTINCT tprog_tnive_clave clave, tnive_desc nivel FROM tcapr, tnive, tprog WHERE tcapr_estatus='A' AND tcapr_tcamp_clave='"+campus+"' AND tcapr_tprog_clave=tprog_clave AND tprog_tnive_clave=tnive_clave  " +
                           "UNION " +
                           "SELECT DISTINCT '0','----Selecciona un Nivel----' Campus  " +
                           "ORDER BY 1";
            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            ConexionMySql.Open();
            DataTable TablaCampus = new DataTable();
            MySqlCommand ConsultaMySql = new MySqlCommand();
            MySqlDataReader DatosMySql;
            try
            {
                ConsultaMySql.Connection = ConexionMySql;
                ConsultaMySql.CommandType = CommandType.Text;
                ConsultaMySql.CommandText = Query;
                DatosMySql = ConsultaMySql.ExecuteReader();
                TablaCampus.Load(DatosMySql, LoadOption.OverwriteChanges);
                cobranza_n.DataSource = TablaCampus;
                cobranza_n.DataValueField = "Clave";
                cobranza_n.DataTextField = "Nivel";
                cobranza_n.DataBind();

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

        protected void combo_tipo_periodo()
        {
            cobranza_tipo_p.Items.Clear();
            string Query = "SELECT '01' clave, 'ANTICIPADO' tipo_per "+
                            "UNION " +
                            "SELECT '02' clave, 'REGULAR' tipo_per " +
                            "UNION " +
                            "SELECT '03' clave, 'EXTEMPORANEO' tipo_per " +
                           "UNION " +
                           "SELECT DISTINCT '0' clave,'----Selecciona un Tipo de Periodo----' tipo_per  " +
                           "ORDER BY 1";
            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            ConexionMySql.Open();
            DataTable TablaCampus = new DataTable();
            MySqlCommand ConsultaMySql = new MySqlCommand();
            MySqlDataReader DatosMySql;
            try
            {
                ConsultaMySql.Connection = ConexionMySql;
                ConsultaMySql.CommandType = CommandType.Text;
                ConsultaMySql.CommandText = Query;
                DatosMySql = ConsultaMySql.ExecuteReader();
                TablaCampus.Load(DatosMySql, LoadOption.OverwriteChanges);
                cobranza_tipo_p.DataSource = TablaCampus;
                cobranza_tipo_p.DataValueField = "Clave";
                cobranza_tipo_p.DataTextField = "tipo_per";
                cobranza_tipo_p.DataBind();

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
        
        protected void combo_periodos()
        {
            string Query = "SELECT DISTINCT tpees_clave Clave,CONCAT(tpees_clave,'-',tpees_desc) Periodo FROM tpees WHERE tpees_clave like '" + cobranza_p.Text + "%' " +
                           "UNION " +
                           "SELECT DISTINCT '0','' Periodo  " +
                           "ORDER BY 1";
            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            ConexionMySql.Open();
            DataTable TablaCampus = new DataTable();
            MySqlCommand ConsultaMySql = new MySqlCommand();
            MySqlDataReader DatosMySql;
            try
            {
                ConsultaMySql.Connection = ConexionMySql;
                ConsultaMySql.CommandType = CommandType.Text;
                ConsultaMySql.CommandText = Query;
                DatosMySql = ConsultaMySql.ExecuteReader();
                TablaCampus.Load(DatosMySql, LoadOption.OverwriteChanges);
                dd_periodo.DataSource = TablaCampus;
                dd_periodo.DataValueField = "Clave";
                dd_periodo.DataTextField = "Periodo";
                dd_periodo.DataBind();

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
        protected void combo_concepto_calendario()
        {
            string Query = "SELECT DISTINCT tcoca_clave Clave, tcoca_desc Concepto_Cal FROM tcoca " +
                           "UNION " +
                           "SELECT DISTINCT '0','----Selecciona un Concepto----' Concepto_Cal  " +
                           "ORDER BY 1";
            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            ConexionMySql.Open();
            DataTable TablaCampus = new DataTable();
            MySqlCommand ConsultaMySql = new MySqlCommand();
            MySqlDataReader DatosMySql;
            try
            {
                ConsultaMySql.Connection = ConexionMySql;
                ConsultaMySql.CommandType = CommandType.Text;
                ConsultaMySql.CommandText = Query;
                DatosMySql = ConsultaMySql.ExecuteReader();
                TablaCampus.Load(DatosMySql, LoadOption.OverwriteChanges);
                cobranza_conc_cal.DataSource = TablaCampus;
                cobranza_conc_cal.DataValueField = "Clave";
                cobranza_conc_cal.DataTextField = "Concepto_Cal";
                cobranza_conc_cal.DataBind();

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
        
        protected void combo_concepto_cobranza()
        {
            string Query = "SELECT DISTINCT tcoco_clave Clave, tcoco_desc Concepto_Cob FROM tcoco " +
                           "UNION " +
                           "SELECT DISTINCT '0','----Selecciona un Concepto----' Concepto_Cob  " +
                           "ORDER BY 1";
            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            ConexionMySql.Open();
            DataTable TablaCampus = new DataTable();
            MySqlCommand ConsultaMySql = new MySqlCommand();
            MySqlDataReader DatosMySql;
            try
            {
                ConsultaMySql.Connection = ConexionMySql;
                ConsultaMySql.CommandType = CommandType.Text;
                ConsultaMySql.CommandText = Query;
                DatosMySql = ConsultaMySql.ExecuteReader();
                TablaCampus.Load(DatosMySql, LoadOption.OverwriteChanges);
                cobranza_concepto.DataSource = TablaCampus;
                cobranza_concepto.DataValueField = "Clave";
                cobranza_concepto.DataTextField = "Concepto_Cob";
                cobranza_concepto.DataBind();

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

        protected void insertar_parametros_c()
        {
            string Query = "INSERT INTO tpaco VALUES('" + cobranza_p.Text + "','" + cobranza_c.SelectedValue + "','" + cobranza_n.SelectedValue + "','" + cobranza_tipo_p.SelectedValue + "'," +descuento_ins.Text + "," + descuento_col.Text + ",'" +cobranza_conc_cal.SelectedValue + "','" + cobranza_concepto.SelectedValue + "','" + Session["usuario"].ToString() + "',current_timestamp())";
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

        protected void actualizar_parametros_c()
        {
            string Query = "UPDATE tpaco SET tpaco_pdesc_insc='" + descuento_ins.Text + "',tpaco_pdesc_parc='" + descuento_col.Text + "',tpaco_tcoca_clave='" + cobranza_conc_cal.Text + "',tpaco_tcoco_clave='" + cobranza_concepto.SelectedValue + "',tpaco_user='" + Session["usuario"].ToString() + "',tpaco_date= current_timestamp() WHERE tpaco_tpees_clave='" + cobranza_p.Text + "' AND tpaco_tcamp_clave='"+cobranza_c.SelectedValue+"' AND tpaco_tnive_clave='"+cobranza_n.Text+"' AND tpaco_clave='"+cobranza_tipo_p.SelectedValue+"'";
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

        protected void grid_cobranza_bind()
        {
            string Query = "";
            Query = "SELECT tpaco_tcamp_clave C_Campus,tcamp_desc Campus, tpaco_tnive_clave C_Nivel,tnive_desc Nivel,tpaco_clave Clave,CASE WHEN tpaco_clave='01' THEN 'ANTICIPADO' WHEN tpaco_clave='02' THEN 'REGULAR' WHEN tpaco_clave='03' THEN 'EXTEMPORANEO' END Tipo_Periodo,tpaco_pdesc_insc Desc_Insc,tpaco_pdesc_parc Desc_Col,tcoca_clave C_Conce_Cal,tcoca_desc Conce_Calendario,tcoco_clave C_Conce_Cob,tcoco_desc Conce_Cobranza,tpaco_tpees_clave Periodo " +
                    "FROM tpaco " +
                    "INNER JOIN tcamp ON tcamp_clave = tpaco_tcamp_clave " +
                    "INNER JOIN tnive ON tnive_clave = tpaco_tnive_clave " +
                    "INNER JOIN tcoco ON tcoco_clave = tpaco_tcoco_clave " +
                    "INNER JOIN tcoca ON tcoca_clave = tpaco_tcoca_clave " +
                    "WHERE tpaco_tpees_clave='"+cobranza_p.Text+"' ";

            if (cobranza_c.SelectedValue != "0")
            {
                Query += "AND tpaco_tcamp_clave='" + cobranza_c.SelectedValue + "' ";
            }
            if (cobranza_n.SelectedValue != "0")
            {
                Query += "AND tpaco_tnive_clave='" + cobranza_n.SelectedValue + "' ";
            }
            if (cobranza_tipo_p.SelectedValue != "0")
            {
                Query += "AND tpaco_clave='" + cobranza_tipo_p.SelectedValue + "' ";
            }
            Query += " ORDER BY 1,3,5";
            try
            {
                MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                ConexionMySql.Open();
                MySqlDataAdapter dataadapter = new MySqlDataAdapter(Query, ConexionMySql);
                DataSet ds = new DataSet();
                dataadapter.Fill(ds, "cobranza");
                GridCobranza.DataSource = ds;
                GridCobranza.EditIndex = -1;
                GridCobranza.DataBind();
                GridCobranza.DataMember = "cobranza";
                GridCobranza.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridCobranza.UseAccessibleHeader = true;


            }
            catch (Exception ex)
            {
                string test = ex.Message;
            }
        }
        protected void cobranza_p_TextChanged(object sender, EventArgs e)
        {
            if (!valida_periodo(cobranza_p.Text))
            {
                grid_cobranza_bind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_cobranza", "load_datatable_cobranza();", true);
            }
            else
            {
                dd_term.Attributes.Add("style", "display:initial");
                term_text.Attributes.Add("style", "display:none");
                combo_periodos();
            }
            
        }

        protected void cobranza_c_SelectedIndexChanged(object sender, EventArgs e)
        {
            combo_nivel_cobranza(cobranza_c.SelectedValue);
            if (!String.IsNullOrEmpty(cobranza_p.Text))
            {
                grid_cobranza_bind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_cobranza", "load_datatable_cobranza();", true);
            }
            actualizar_cob.Attributes.Add("style", "display:none");
            guardar_cob.Attributes.Add("style", "display:initial");
        }

        protected void dd_periodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            dd_term.Attributes.Add("style", "display:none");
            term_text.Attributes.Add("style", "display:initial");
            cobranza_p.Text = dd_periodo.SelectedValue;
            if (dd_periodo.SelectedValue != "0")
            {
                grid_cobranza_bind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_cobranza", "load_datatable_cobranza();", true);
            }
            actualizar_cob.Attributes.Add("style", "display:none");
            guardar_cob.Attributes.Add("style", "display:initial");
        }

        protected void search_term_Click(object sender, ImageClickEventArgs e)
        {
            dd_term.Attributes.Add("style", "display:initial");
            term_text.Attributes.Add("style", "display:none");
            combo_periodos();
        }

        protected bool valida_periodo(string periodo)
        {
            string Query = "";
            Query = "SELECT COUNT(*) Indicador FROM tpees WHERE tpees_clave='" + periodo + "'";
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

        protected bool valida_tipo_peri()
        {
            string Query = "";
            Query = "SELECT COUNT(*) Indicador FROM tpaco WHERE tpaco_tpees_clave='"+cobranza_p.Text+"' AND tpaco_tcamp_clave='"+cobranza_c.SelectedValue+"' AND tpaco_tnive_clave='"+cobranza_n.SelectedValue+"' AND tpaco_clave='"+cobranza_tipo_p.SelectedValue+"'";
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

        protected void guardar_cob_Click(object sender, EventArgs e)
        {
            if (valida_tipo_peri())
            {
                if (!String.IsNullOrEmpty(cobranza_p.Text) && cobranza_c.SelectedValue != "0" && cobranza_n.SelectedValue != "0" && cobranza_tipo_p.SelectedValue != "0")
                {
                    insertar_parametros_c();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "validar_campos_cob", "validar_campos_cobranza();", true);
                }
            }
            else
            {
                lbl_error.Visible = Visible;
            }

        }

        protected void btn_oculto_Click(object sender, EventArgs e)
        {
            cobranza_c.SelectedValue = campus.Text;
            combo_nivel_cobranza(cobranza_c.SelectedValue);
            cobranza_n.SelectedValue = nivel.Text;
            cobranza_tipo_p.SelectedValue = t_periodo.Text;
            cobranza_p.Text = periodo_txt.Text;
            cobranza_conc_cal.SelectedValue = conce_cal_txt.Text;
            cobranza_concepto.SelectedValue = conce_cob_txt.Text;
            descuento_ins.Text = desc_ins.Text;
            descuento_col.Text = desc_col.Text;
            grid_cobranza_bind();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_cobranza", "load_datatable_cobranza();", true);
            actualizar_cob.Attributes.Add("style", "display:initial");
            guardar_cob.Attributes.Add("style", "display:none");
        }

        protected void actualizar_cob_Click(object sender, EventArgs e)
        {
            actualizar_parametros_c();
        }
        #endregion

    }
}