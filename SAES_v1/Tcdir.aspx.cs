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
    public partial class Tcdir : System.Web.UI.Page
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
                    combo_pais();
                    combo_estado();
                    combo_dele();

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
                Global.inserta_log(mensaje_error, "tcdir", Session["usuario"].ToString());
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
                Global.inserta_log(mensaje_error, "tcdir", Session["usuario"].ToString());
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
                Global.inserta_log(mensaje_error, "tcdir", Session["usuario"].ToString());
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
                Global.inserta_log(mensaje_error, "tcdir", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
            finally
            {
                ConexionMySql.Close();
            }
        }

        protected void combo_pais()
        {
            ddl_programa.Items.Clear();
            ddl_programa.Items.Add(new ListItem("-------", "0"));
            string strQuerydire = "";
            strQuerydire = "select tpais_clave clave, tpais_desc pais from tpais where tpais_estatus='A' " +
                            "union " +
                            "select '0' clave, '--------' pais " +
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

                ddl_pais.DataSource = TablaEstado;
                ddl_pais.DataValueField = "Clave";
                ddl_pais.DataTextField = "pais";
                ddl_pais.DataBind();

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcdir", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
            finally
            {
                ConexionMySql.Close();
            }
        }

        protected void combo_estado()
        {
            string strQueryEstado = "";
            strQueryEstado = "select distinct testa_clave clave, testa_desc estado from testa " +
                            " where testa_estatus='A' ";
            if (ddl_pais.SelectedValue != "0")
            {
                strQueryEstado = strQueryEstado + " and testa_tpais_clave = '" + ddl_pais.SelectedValue + "'";
            }
            strQueryEstado = strQueryEstado + "union " +
                            "select '0' clave, '-------' estado " +
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
                ConsultaMySql.CommandText = strQueryEstado;
                DatosMySql = ConsultaMySql.ExecuteReader();
                TablaEstado.Load(DatosMySql, LoadOption.OverwriteChanges);

                ddl_estado.DataSource = TablaEstado;
                ddl_estado.DataValueField = "clave";
                ddl_estado.DataTextField = "estado";
                ddl_estado.DataBind();

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcdir", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
            finally
            {
                ConexionMySql.Close();
            }

        }

        protected void combo_dele()
        {
            string strQuerydele = "";
            strQuerydele = "select distinct tdele_clave clave, tdele_desc dele from tdele " +
                            " where tdele_estatus='A' " ;
            if (ddl_pais.SelectedValue != "0")
            {
                strQuerydele = strQuerydele + " and tdele_tpais_clave = '" + ddl_pais.SelectedValue + "'";
            }
            if (ddl_estado.SelectedValue != "0")
            {
                strQuerydele = strQuerydele + " and tdele_testa_clave = '" + ddl_estado.SelectedValue + "'";
            }
            strQuerydele = strQuerydele + "union " +
                            "select '0' clave, '-------' dele " +
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
                ConsultaMySql.CommandText = strQuerydele;
                DatosMySql = ConsultaMySql.ExecuteReader();
                TablaEstado.Load(DatosMySql, LoadOption.OverwriteChanges);

                ddl_dele.DataSource = TablaEstado;
                ddl_dele.DataValueField = "Clave";
                ddl_dele.DataTextField = "dele";
                ddl_dele.DataBind();

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcdir", Session["usuario"].ToString());
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
                Global.inserta_log(mensaje_error, "tcdir", Session["usuario"].ToString());
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

        protected void ddl_pais_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_pais.SelectedValue != "0")
            {
                combo_estado();
            }
        }

        protected void ddl_estado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_estado.SelectedValue != "0")
            {
                combo_dele();
            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            combo_periodos();
            combo_campus();
            combo_estatus_sol();
            combo_pais();
            combo_estado();
            combo_dele();
            GridDirecciones.Visible = false;
        }

        protected void btn_consultar_Click(object sender, EventArgs e)
        {
            grid_c_direcciones();
        }
        protected void grid_c_direcciones()
        {
            string Query = "  select tadmi_tpees_clave periodo, tcamp_desc campus , tnive_desc nivel, tprog_desc programa, " +
            " tpers_id iden, concat(tpers_nombre, ' ', tpers_paterno, ' ', tpers_materno) alumno, " +
            " tdire_desc tipo_dir, taldi_calle calle, taldi_colonia colonia, testa_desc estado, tdele_desc dele, " +
            " taldi_tcopo_clave cp, taldi_ciudad ciudad, tpais_desc pais, tstso_desc estatus " +
            " from taldi a, tpers, tdire, testa, tdele, tpais, tadmi, tcamp, tnive, tprog, tstso " +
            " where taldi_tpers_num = tpers_num and taldi_consec in (select max(taldi_consec) from taldi b " +
            "   where a.taldi_tpers_num = b.taldi_tpers_num and a.taldi_tdire_clave = b.taldi_tdire_clave) " +
            " and taldi_estatus = 'A' and taldi_tdire_clave = tdire_clave and taldi_testa_clave = testa_clave " +
            " and taldi_tdele_clave = tdele_clave and tdele_testa_clave = taldi_testa_clave and taldi_tpais_clave = tpais_clave " +
            " and taldi_tpers_num = tadmi_tpers_num and tadmi_tcamp_clave = tcamp_clave " +
            " and tadmi_tprog_clave = tprog_clave and tprog_tnive_clave = tnive_clave and tadmi_tstso_clave=tstso_clave ";
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

            if (ddl_pais.SelectedValue != "0")
            {
                Query = Query + " and taldi_tpais_clave='" + ddl_pais.SelectedValue + "' ";
            }

            if (ddl_estado.SelectedValue != "0")
            {
                Query = Query + " and taldi_testa_clave='" + ddl_estado.SelectedValue + "' ";
            }

            if (ddl_dele.SelectedValue != "0")
            {
                Query = Query + " and taldi_tdele_clave='" + ddl_dele.SelectedValue + "' ";
            }

            Query = Query + " order by 1,2,3,4,7,12,11,10 ";
            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            ConexionMySql.Open();
            try
            {

            MySqlDataAdapter dataadapter = new MySqlDataAdapter(Query, ConexionMySql);
            DataSet ds = new DataSet();
            dataadapter.Fill(ds, "Solicitud");
            GridDirecciones.DataSource = ds;
            GridDirecciones.DataBind();
            GridDirecciones.DataMember = "Solicitud";
            GridDirecciones.HeaderRow.TableSection = TableRowSection.TableHeader;
            GridDirecciones.UseAccessibleHeader = true;
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
            GridDirecciones.Visible = true;

             }
             catch (Exception ex)
             {
                 string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcdir", Session["usuario"].ToString());
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
            Response.Redirect("Taldi.aspx");
        }
    }
}