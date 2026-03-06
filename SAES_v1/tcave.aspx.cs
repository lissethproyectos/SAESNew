using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SAES_v1
{
    public partial class tcave : System.Web.UI.Page
    {
        public string labels_dashboard_1;
        public string data_dashboard_1;
        public string label_dashboard_1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                combo_periodo(ddl_periodo, ddl_campus, ddl_nivel);
                combo_campus(ddl_campus);
            }
        }


        protected void combo_periodo(DropDownList ddl_periodo, DropDownList ddl_campus, DropDownList ddl_nivel)
        {
            ddl_periodo.Items.Clear();
            ddl_campus.Items.Clear();
            ddl_campus.Items.Add(new ListItem("----Campus----", "0"));
            ddl_nivel.Items.Clear();
            ddl_nivel.Items.Add(new ListItem("----Nivel----", "0"));


            string Query = "SELECT tpees_clave Clave,concat(tpees_clave , ' -- ' , tpees_desc) Nombre FROM tpees " +
                          "UNION " +
                          "SELECT '0' Clave,'----Periodo----' Nombre " +
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

                ddl_periodo.DataSource = TablaEstado;
                ddl_periodo.DataValueField = "Clave";
                ddl_periodo.DataTextField = "Nombre";
                ddl_periodo.DataBind();

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

        protected void combo_campus(DropDownList ddl_campus)
        {
            string Query = "SELECT tcamp_clave Clave, tcamp_desc Nombre FROM tcamp " +
                          "UNION " +
                          "SELECT '0' Clave,'----Campus----' Nombre " +
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

                ddl_campus.DataSource = TablaEstado;
                ddl_campus.DataValueField = "Clave";
                ddl_campus.DataTextField = "Nombre";
                ddl_campus.DataBind();

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

        protected void combo_nivel(string campus, DropDownList ddl_nivel)
        {
            string Query = "SELECT DISTINCT tnive_clave Clave, tnive_desc Nombre FROM tnive INNER JOIN tprog ON tprog_tnive_clave=tnive_clave INNER JOIN tcapr ON tcapr_tprog_clave=tprog_clave WHERE tcapr_tcamp_clave='" + campus + "'" +
                          "UNION " +
                          "SELECT '0' Clave,'----Nivel----' Nombre " +
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

                ddl_nivel.DataSource = TablaEstado;
                ddl_nivel.DataValueField = "Clave";
                ddl_nivel.DataTextField = "Nombre";
                ddl_nivel.DataBind();

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

        #region M+etodos para Dashboard 1
        protected void ddl_periodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Gridtcv.Visible = false;
        }

        protected void ddl_campus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_campus.SelectedValue != "0")
            {
                combo_nivel(ddl_campus.SelectedValue, ddl_nivel);

            }
            Gridtcv.Visible = false;
        }

        protected void ddl_nivel_SelectedIndexChanged(object sender, EventArgs e)
        {
            Gridtcv.Visible = false;
        }

        protected void CV_Click(object sender, EventArgs e)
        {

            string Query = "";

                Query = " select Campus, Nivel, format(Xcobrar_Par,2) Xcobrar_Par, fecha(date_format(FV1,'%Y-%m-%d')) FV1, format(Vencido1,2) Vencido1, round(Vencido1/ Xcobrar_Par*100,2) Porc1, " +
                 " fecha(date_format(FV2, '%Y-%m-%d')) FV2, format(Vencido2, 2) Vencido2, round(Vencido2 / Xcobrar_Par * 100, 2) Porc2, " +
                 " fecha(date_format(FV3, '%Y-%m-%d')) FV3, format(Vencido3, 2) Vencido3, round(Vencido3 / Xcobrar_Par * 100, 2) Porc3, " +
                 " fecha(date_format(FV4, '%Y-%m-%d')) FV4, format(Vencido4, 2) Vencido4, round(Vencido4 / Xcobrar_Par * 100, 2) Porc4, " +
                 " format(Xcobrar_Par * 4, 2) Xcobrar_Total, format(Vencido1 + Vencido2 + Vencido3 + Vencido4, 2) Total_Vencido, " +
                 " round((Vencido1 + Vencido2 + Vencido3 + Vencido4) / (Xcobrar_Par * 4) * 100, 2) Porc_Vencido_Total " +
                 " from( " +
                " select tcamp_desc Campus, tnive_desc Nivel, sum(tedcu_importe) Xcobrar_Par, " +
                " (select tfeve_vencimiento from tfeve where tfeve_tpees_clave = x.tedcu_tpees_clave " +
                "  and   tfeve_ttasa_clave = '01' and tfeve_numero = 1) FV1, " +
                " (select sum(tedcu_balance) from tedcu xx, tcoco, testu e, tprog " +
                " where x.tedcu_tpees_clave = xx.tedcu_tpees_clave and xx.tedcu_tcoco_clave = tcoco_clave " +
                " and tcoco_categ = 'CO' and tedcu_fecha_venc<curdate() and xx.tedcu_numero = 1 " +
                " and testu_tpers_num = tedcu_tpers_num and testu_tpees_clave in " +
                " (select max(testu_tpees_clave) from testu ee  where e.testu_tpers_num = ee.testu_tpers_num) " +
                " and testu_tprog_clave = tprog_clave and e.testu_tcamp_clave = z.testu_tcamp_clave) Vencido1, " +
                " (select tfeve_vencimiento from tfeve where tfeve_tpees_clave = x.tedcu_tpees_clave " +
                "  and tfeve_ttasa_clave = '01' and tfeve_numero = 2) FV2, " +
                " (select sum(tedcu_balance) from tedcu xx, tcoco, testu e, tprog " +
                " where x.tedcu_tpees_clave = xx.tedcu_tpees_clave and xx.tedcu_tcoco_clave = tcoco_clave " +
                " and tcoco_categ = 'CO' and tedcu_fecha_venc<curdate() and xx.tedcu_numero = 2 " +
                " and testu_tpers_num = tedcu_tpers_num and testu_tpees_clave in " +
                " (select max(testu_tpees_clave) from testu ee  where e.testu_tpers_num = ee.testu_tpers_num) " +
                " and testu_tprog_clave = tprog_clave and e.testu_tcamp_clave = z.testu_tcamp_clave) Vencido2, " +
                " (select tfeve_vencimiento from tfeve where tfeve_tpees_clave = x.tedcu_tpees_clave " +
                "  and tfeve_ttasa_clave = '01' and tfeve_numero = 3) FV3, " +
                " (select sum(tedcu_balance) from tedcu xx, tcoco, testu e, tprog " +
                " where x.tedcu_tpees_clave = xx.tedcu_tpees_clave and xx.tedcu_tcoco_clave = tcoco_clave " +
                " and tcoco_categ = 'CO' and tedcu_fecha_venc<curdate() and xx.tedcu_numero = 3 " +
                " and testu_tpers_num = tedcu_tpers_num and testu_tpees_clave in " +
                " (select max(testu_tpees_clave) from testu ee  where e.testu_tpers_num = ee.testu_tpers_num) " +
                " and testu_tprog_clave = tprog_clave and e.testu_tcamp_clave = z.testu_tcamp_clave) Vencido3, " +
                " (select tfeve_vencimiento from tfeve where tfeve_tpees_clave = x.tedcu_tpees_clave " +
                " and tfeve_ttasa_clave = '01' and tfeve_numero = 4) FV4, " +
                " (select sum(tedcu_balance) from tedcu xx, tcoco, testu e, tprog " +
                " where x.tedcu_tpees_clave = xx.tedcu_tpees_clave and xx.tedcu_tcoco_clave = tcoco_clave " +
                " and tcoco_categ = 'CO' and tedcu_fecha_venc<curdate() and xx.tedcu_numero = 4 " +
                " and testu_tpers_num = tedcu_tpers_num and testu_tpees_clave in " +
                " (select max(testu_tpees_clave) from testu ee  where e.testu_tpers_num = ee.testu_tpers_num) " +
                " and testu_tprog_clave = tprog_clave and e.testu_tcamp_clave = z.testu_tcamp_clave) Vencido4 " +
                " from tedcu x, tcoco, testu z, tnive, tprog, tcamp " +
                " where tedcu_tcoco_clave = tcoco_clave and tcoco_categ = 'CO' " +
                " and testu_tpers_num = tedcu_tpers_num and testu_tpees_clave in " +
                " (select max(testu_tpees_clave) from testu zz " +
                " where z.testu_tpers_num = zz.testu_tpers_num) " +
                " and testu_tprog_clave = tprog_clave " +
                " and tprog_tnive_clave = tnive_clave " +
                " and tedcu_numero = 1 and testu_tcamp_clave = tcamp_clave ";

                    Query = Query + " and tedcu_tpees_clave = '" + ddl_periodo.SelectedValue + "'";

                if (ddl_campus.SelectedValue != "0")
                {
                    Query = Query + " and testu_tcamp_clave = '" + ddl_campus.SelectedValue + "'";
                }
                if (ddl_nivel.SelectedValue != "0")
                {
                    Query = Query + " and tprog_tnive_clave = '" + ddl_nivel.SelectedValue + "'";
                }
            Query = Query + " group by Campus, Nivel) x ";
            Query = Query + " union " +
                 " select Campus, '--Total' Nivel,  format(sum(Xcobrar_Par), 2), null FV1, format(sum(Vencido1), 2) Vencido1, round(sum(Vencido1) / sum(Xcobrar_Par) * 100, 2) Porc1, " +
                 " null FV2, format(sum(Vencido2), 2) Vencido2, round(sum(Vencido2) / sum(Xcobrar_Par) * 100, 2) Porc2, " +
                 " null FV3, format(sum(Vencido3), 2) Vencido3, round(sum(Vencido3) / sum(Xcobrar_Par) * 100, 2) Porc3, " +
                 " null FV4, format(sum(Vencido4), 2) Vencido4, round(sum(Vencido4) / sum(Xcobrar_Par) * 100, 2) Porc4, " +
                 " format(sum(Xcobrar_Par * 4), 2) Xcobrar_Total, format(sum(Vencido1) + sum(Vencido2) + sum(Vencido3) + sum(Vencido4), 2) Total_Vencido, " +
                 " round((sum(Vencido1) + sum(Vencido2) + sum(Vencido3) + sum(Vencido4)) / (sum(Xcobrar_Par * 4)) * 100, 2) Porc_Vencido_Total " +
                 " from( " +
                " select tcamp_desc Campus, null Nivel, sum(tedcu_importe) Xcobrar_Par, " +
                " (select tfeve_vencimiento from tfeve where tfeve_tpees_clave = x.tedcu_tpees_clave " +
                "  and   tfeve_ttasa_clave = '01' and tfeve_numero = 1) FV1, " +
                " (select sum(tedcu_balance) from tedcu xx, tcoco, testu e, tprog " +
                " where x.tedcu_tpees_clave = xx.tedcu_tpees_clave and xx.tedcu_tcoco_clave = tcoco_clave " +
                " and tcoco_categ = 'CO' and tedcu_fecha_venc<curdate() and xx.tedcu_numero = 1 " +
                " and testu_tpers_num = tedcu_tpers_num and testu_tpees_clave in " +
                " (select max(testu_tpees_clave) from testu ee  where e.testu_tpers_num = ee.testu_tpers_num) " +
                " and testu_tprog_clave = tprog_clave and e.testu_tcamp_clave = z.testu_tcamp_clave) Vencido1, " +
                " (select tfeve_vencimiento from tfeve where tfeve_tpees_clave = x.tedcu_tpees_clave " +
                "  and tfeve_ttasa_clave = '01' and tfeve_numero = 2) FV2, " +
                " (select sum(tedcu_balance) from tedcu xx, tcoco, testu e, tprog " +
                " where x.tedcu_tpees_clave = xx.tedcu_tpees_clave and xx.tedcu_tcoco_clave = tcoco_clave " +
                " and tcoco_categ = 'CO' and tedcu_fecha_venc<curdate() and xx.tedcu_numero = 2 " +
                " and testu_tpers_num = tedcu_tpers_num and testu_tpees_clave in " +
                " (select max(testu_tpees_clave) from testu ee  where e.testu_tpers_num = ee.testu_tpers_num) " +
                " and testu_tprog_clave = tprog_clave and e.testu_tcamp_clave = z.testu_tcamp_clave) Vencido2, " +
                " (select tfeve_vencimiento from tfeve where tfeve_tpees_clave = x.tedcu_tpees_clave " +
                "  and tfeve_ttasa_clave = '01' and tfeve_numero = 3) FV3, " +
                " (select sum(tedcu_balance) from tedcu xx, tcoco, testu e, tprog " +
                " where x.tedcu_tpees_clave = xx.tedcu_tpees_clave and xx.tedcu_tcoco_clave = tcoco_clave " +
                " and tcoco_categ = 'CO' and tedcu_fecha_venc<curdate() and xx.tedcu_numero = 3 " +
                " and testu_tpers_num = tedcu_tpers_num and testu_tpees_clave in " +
                " (select max(testu_tpees_clave) from testu ee  where e.testu_tpers_num = ee.testu_tpers_num) " +
                " and testu_tprog_clave = tprog_clave and e.testu_tcamp_clave = z.testu_tcamp_clave) Vencido3, " +
                " (select tfeve_vencimiento from tfeve where tfeve_tpees_clave = x.tedcu_tpees_clave " +
                " and tfeve_ttasa_clave = '01' and tfeve_numero = 4) FV4, " +
                " (select sum(tedcu_balance) from tedcu xx, tcoco, testu e, tprog " +
                " where x.tedcu_tpees_clave = xx.tedcu_tpees_clave and xx.tedcu_tcoco_clave = tcoco_clave " +
                " and tcoco_categ = 'CO' and tedcu_fecha_venc<curdate() and xx.tedcu_numero = 4 " +
                " and testu_tpers_num = tedcu_tpers_num and testu_tpees_clave in " +
                " (select max(testu_tpees_clave) from testu ee  where e.testu_tpers_num = ee.testu_tpers_num) " +
                " and testu_tprog_clave = tprog_clave and e.testu_tcamp_clave = z.testu_tcamp_clave) Vencido4 " +
                " from tedcu x, tcoco, testu z, tnive, tprog, tcamp " +
                " where tedcu_tcoco_clave = tcoco_clave and tcoco_categ = 'CO' " +
                " and testu_tpers_num = tedcu_tpers_num and testu_tpees_clave in " +
                " (select max(testu_tpees_clave) from testu zz " +
                " where z.testu_tpers_num = zz.testu_tpers_num) " +
                " and testu_tprog_clave = tprog_clave " +
                " and tprog_tnive_clave = tnive_clave " +
                " and tedcu_numero = 1 and testu_tcamp_clave = tcamp_clave ";

            Query = Query + " and tedcu_tpees_clave = '" + ddl_periodo.SelectedValue + "'";

            if (ddl_campus.SelectedValue != "0")
            {
                Query = Query + " and testu_tcamp_clave = '" + ddl_campus.SelectedValue + "'";
            }
            if (ddl_nivel.SelectedValue != "0")
            {
                Query = Query + " and tprog_tnive_clave = '" + ddl_nivel.SelectedValue + "'";
            }

            Query = Query + " group by Campus, Nivel) x " +
                " group by Campus,Nivel order by Xcobrar_Par";

            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            MySqlCommand myCommand = new MySqlCommand(Query, ConexionMySql);
            ConexionMySql.Open();
            MySqlDataAdapter dataadapter = new MySqlDataAdapter(Query, ConexionMySql);
            DataSet ds = new DataSet();
            dataadapter.Fill(ds, "Tcv");
            Gridtcv.DataSource = ds;
            Gridtcv.DataBind();
            if (Gridtcv.Rows.Count > 0)
            {
                Gridtcv.DataMember = "Tcv";
                Gridtcv.HeaderRow.TableSection = TableRowSection.TableHeader;
                Gridtcv.UseAccessibleHeader = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);
                Gridtcv.Visible = true;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Nodatos", "Nodatos();", true);
            }
            ConexionMySql.Close();


        }
        #endregion
    }
}