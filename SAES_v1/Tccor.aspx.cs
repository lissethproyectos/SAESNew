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
    public partial class Tccor : System.Web.UI.Page
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
                    combo_periodos();
                    combo_campus();
                    combo_nivel();
                    combo_programa();
                    combo_estatus_sol();

                }
            }
        }

        protected void combo_periodos()
        {
            string strQuerydire = "";
            strQuerydire = "select tpees_clave clave, concat(tpees_clave,'||',tpees_desc) periodo from tpees where tpees_estatus='A'  " +
                            "union " +
                            "select '999999' clave, '-------' periodo " +
                            "order by 1 desc";
            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            ConexionMySql.Open();
            DataTable TablaEstado = new DataTable();
            MySqlCommand ConsultaMySql = new MySqlCommand();
            MySqlDataReader DatosMySql;
            try
            {
                ConsultaMySql.Connection = ConexionMySql;
                ConsultaMySql.CommandType = CommandType.Text;
                ConsultaMySql.CommandText = strQuerydire;
                DatosMySql = ConsultaMySql.ExecuteReader();
                TablaEstado.Load(DatosMySql, LoadOption.OverwriteChanges);

                ddl_periodo.DataSource = TablaEstado;
                ddl_periodo.DataValueField = "Clave";
                ddl_periodo.DataTextField = "Periodo";
                ddl_periodo.DataBind();

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tccor", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
            finally
            {
                ConexionMySql.Close();
            }
        }
        protected void combo_campus()
        {
            ddl_programa.Items.Clear();
            ddl_programa.Items.Add(new ListItem("-------", "0"));
            string strQuerydire = "";
            strQuerydire = "select tcamp_clave clave, tcamp_desc campus from tcamp where tcamp_estatus='A' " +
                            "union " +
                            "select '0' clave, '--------' campus " +
                            "order by 1";
            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            ConexionMySql.Open();
            DataTable TablaEstado = new DataTable();
            MySqlCommand ConsultaMySql = new MySqlCommand();
            MySqlDataReader DatosMySql;
            try
            {
                ConsultaMySql.Connection = ConexionMySql;
                ConsultaMySql.CommandType = CommandType.Text;
                ConsultaMySql.CommandText = strQuerydire;
                DatosMySql = ConsultaMySql.ExecuteReader();
                TablaEstado.Load(DatosMySql, LoadOption.OverwriteChanges);

                ddl_campus.DataSource = TablaEstado;
                ddl_campus.DataValueField = "Clave";
                ddl_campus.DataTextField = "Campus";
                ddl_campus.DataBind();

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tccor", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
            finally
            {
                ConexionMySql.Close();
            }
        }
        protected void combo_nivel()
        {
            string strQueryNivel = "";
            strQueryNivel = "select distinct tnive_clave clave, tnive_desc nivel from tcapr, tprog, tnive " +
                            " where tcapr_estatus='A' " +
                            " and tcapr_tprog_clave=tprog_clave and tprog_ind_admi='X' and tprog_tnive_clave=tnive_clave ";
            if (ddl_campus.SelectedValue != "0")
            {
                strQueryNivel = strQueryNivel + " and tcapr_tcamp_clave = '" + ddl_campus.SelectedValue + "'";
            }
            strQueryNivel = strQueryNivel + "union " +
                            "select '0' clave, '-------' nivel " +
                            "order by 1";
            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            ConexionMySql.Open();
            DataTable TablaEstado = new DataTable();
            MySqlCommand ConsultaMySql = new MySqlCommand();
            MySqlDataReader DatosMySql;
            try
            {
                ConsultaMySql.Connection = ConexionMySql;
                ConsultaMySql.CommandType = CommandType.Text;
                ConsultaMySql.CommandText = strQueryNivel;
                DatosMySql = ConsultaMySql.ExecuteReader();
                TablaEstado.Load(DatosMySql, LoadOption.OverwriteChanges);

                ddl_nivel.DataSource = TablaEstado;
                ddl_nivel.DataValueField = "clave";
                ddl_nivel.DataTextField = "nivel";
                ddl_nivel.DataBind();

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tccor", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
            finally
            {
                ConexionMySql.Close();
            }

        }
        protected void combo_programa()
        {
            string strQuerydire = "";
            strQuerydire = "select distinct tprog_clave clave, tprog_desc programa from tcapr, tprog " +
                            " where tcapr_estatus='A' " +
                            " and tcapr_tprog_clave=tprog_clave and tprog_ind_admi='X'  ";
            if (ddl_campus.SelectedValue != "0")
            {
                strQuerydire = strQuerydire + " and tcapr_tcamp_clave = '" + ddl_campus.SelectedValue + "'";
            }
            if (ddl_nivel.SelectedValue != "0")
            {
                strQuerydire = strQuerydire + " and tprog_tnive_clave = '" + ddl_nivel.SelectedValue + "'";
            }
            strQuerydire = strQuerydire + "union " +
                            "select '0' clave, '-------' programa " +
                            "order by 1";
            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            ConexionMySql.Open();
            DataTable TablaEstado = new DataTable();
            MySqlCommand ConsultaMySql = new MySqlCommand();
            MySqlDataReader DatosMySql;
            try
            {
                ConsultaMySql.Connection = ConexionMySql;
                ConsultaMySql.CommandType = CommandType.Text;
                ConsultaMySql.CommandText = strQuerydire;
                DatosMySql = ConsultaMySql.ExecuteReader();
                TablaEstado.Load(DatosMySql, LoadOption.OverwriteChanges);

                ddl_programa.DataSource = TablaEstado;
                ddl_programa.DataValueField = "Clave";
                ddl_programa.DataTextField = "Programa";
                ddl_programa.DataBind();

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tccor", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
            finally
            {
                ConexionMySql.Close();
            }
        }

        protected void combo_estatus_sol()
        {
            string strQuerydire = "";
            strQuerydire = "select tstso_clave clave, tstso_desc estatus_sol from tstso  where tstso_estatus='A' " +
                            "union " +
                            "select '0' clave, '--------' estatus_sol " +
                            "order by 1";
            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            ConexionMySql.Open();
            DataTable TablaEstado = new DataTable();
            MySqlCommand ConsultaMySql = new MySqlCommand();
            MySqlDataReader DatosMySql;
            try
            {
                ConsultaMySql.Connection = ConexionMySql;
                ConsultaMySql.CommandType = CommandType.Text;
                ConsultaMySql.CommandText = strQuerydire;
                DatosMySql = ConsultaMySql.ExecuteReader();
                TablaEstado.Load(DatosMySql, LoadOption.OverwriteChanges);

                ddl_estatus_sol.DataSource = TablaEstado;
                ddl_estatus_sol.DataValueField = "Clave";
                ddl_estatus_sol.DataTextField = "estatus_sol";
                ddl_estatus_sol.DataBind();

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tccor", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
            finally
            {
                ConexionMySql.Close();
            }
        }

        protected void ddl_campus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_campus.SelectedValue != "0")
            {
                combo_nivel();
                combo_programa();
            }
        }

        protected void ddl_nivel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_campus.SelectedValue != "0")
            {
                combo_programa();
            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            combo_periodos();
            combo_campus();
            combo_estatus_sol();
            GridCorreos.Visible = false;
        }

        protected void btn_consultar_Click(object sender, EventArgs e)
        {
            grid_c_alumnos();
        }
        protected void grid_c_alumnos()
        {
            string Query = "  select tadmi_tpees_clave periodo, tcamp_desc campus, tnive_desc nivel, tprog_desc programa," +
              " tpers_id iden, concat(tpers_nombre,' ',tpers_paterno,' ',tpers_materno) alumno, " +
              " tmail_desc tipo_correo,talco_correo correo, talco_preferido prefer, tstso_desc estatus" +
              " from talco a, tpers, tmail, tadmi, tcamp, tnive, tprog, tstso " +
              " where talco_tpers_num=tpers_num and talco_consec in (select max(talco_consec) from talco b " +
              "   where a.talco_tpers_num = b.talco_tpers_num and a.talco_tmail_clave = b.talco_tmail_clave) " +
              " and talco_estatus = 'A' and talco_tmail_clave = tmail_clave " +
              " and tadmi_tpers_num = talco_tpers_num and talco_tpers_num = tpers_num  " +
              " and tadmi_tcamp_clave = tcamp_clave " +
              " and tadmi_tprog_clave = tprog_clave and tprog_tnive_clave = tnive_clave " +
              " and tadmi_tstso_clave = tstso_clave ";

            if (ddl_periodo.SelectedValue != "999999")
            {
                Query = Query + " and tadmi_tpees_clave='" + ddl_periodo.SelectedValue + "' ";
            }
            if (ddl_campus.SelectedValue != "0")
            {
                Query = Query + " and tadmi_tcamp_clave='" + ddl_campus.SelectedValue + "' ";
            }
            if (ddl_programa.SelectedValue != "0")
            {
                Query = Query + " and tprog_clave='" + ddl_programa.SelectedValue + "' ";
            }

            if (ddl_estatus_sol.SelectedValue != "0")
            {
                Query = Query + " and tstso_clave='" + ddl_estatus_sol.SelectedValue + "' ";
            }

            Query = Query + " order by 1,2,3,4,7 ";

            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            ConexionMySql.Open();
            try
            {

                MySqlDataAdapter dataadapter = new MySqlDataAdapter(Query, ConexionMySql);
                DataSet ds = new DataSet();
                dataadapter.Fill(ds, "Solicitud");
                GridCorreos.DataSource = ds;
                GridCorreos.DataBind();
                GridCorreos.DataMember = "Solicitud";
                GridCorreos.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridCorreos.UseAccessibleHeader = true;
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                GridCorreos.Visible = true;

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tccor", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
            finally
            {

            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);
            ConexionMySql.Close();
        }

        protected void back_Click(object sender, EventArgs e)
        {
            Response.Redirect("Talco.aspx");
        }
    }
}