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
using System.Text;
using static SAES_DBO.Models.ModelAlumno;
using static SAES_DBO.Models.ModelCobranza;
using SAES_DBO.Models;
using SAES_Services;

namespace SAES_v1
{
    public partial class tsimu : System.Web.UI.Page
    {
        #region <Variables>
        //Utilidades utils = new Utilidades();
        AlumnoService serviceAlumno = new AlumnoService();
        List<ModelObtenPaisesResponse> lstPaises = new List<ModelObtenPaisesResponse>();
        Catalogos serviceCatalogo = new Catalogos();
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
                    if (!string.IsNullOrEmpty(Global.cuenta as string) && !string.IsNullOrEmpty(Global.nombre as string) && !string.IsNullOrEmpty(Global.periodo as string) && !string.IsNullOrEmpty(Global.campus as string) && !string.IsNullOrEmpty(Global.programa as string) && !string.IsNullOrEmpty(Global.tipo_ingreso as string) && !string.IsNullOrEmpty(Global.tasa as string))
                    {

                        ddl_periodo.Attributes.Add("disabled", "");
                        ddl_Campus.Attributes.Add("disabled", "");
                        ddl_Programa.Attributes.Add("disabled", "");
                        ddl_tipo_ingreso.Attributes.Add("disabled", "");
                        ddl_tasa_f.Attributes.Add("disabled", "");

                        txt_matricula.Text = Global.cuenta.ToString();
                        txt_nombre.Text = Global.nombre.ToString();
                        combo_periodo();
                        ddl_periodo.SelectedValue = Global.periodo.ToString();
                        combo_campus();
                        ddl_Campus.SelectedValue = Global.campus.ToString();
                        combo_Programa();
                        ddl_Programa.SelectedValue = Global.programa.ToString();
                        combo_tipo_ingreso();
                        ddl_tipo_ingreso.SelectedValue = Global.tipo_ingreso.ToString();
                        combo_tasa_financiera();
                        ddl_tasa_f.SelectedValue = Global.tasa.ToString();
                        combo_plan_beca();

                    }
                }
            }


        }

        protected void combo_periodo()
        {
            string strQuerydire = "";
            strQuerydire = "select tpees_clave clave, tpees_desc periodo from tpees where tpees_estatus='A' and tpees_fin >= curdate() " +
                            "union " +
                            "select '0' clave, '----Selecciona un periodo----' periodo " +
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

                ddl_periodo.DataSource = TablaEstado;
                ddl_periodo.DataValueField = "Clave";
                ddl_periodo.DataTextField = "Periodo";
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
        protected void combo_campus()
        {
            ddl_Programa.Items.Clear();
            ddl_Programa.Items.Add(new ListItem("----Selecciona un programa----", "0"));
            string strQuerydire = "";
            strQuerydire = "select tcamp_clave clave, tcamp_desc campus from tcamp where tcamp_estatus='A' " +
                            "union " +
                            "select '0' clave, '----Selecciona un campus----' campus " +
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

                ddl_Campus.DataSource = TablaEstado;
                ddl_Campus.DataValueField = "Clave";
                ddl_Campus.DataTextField = "Campus";
                ddl_Campus.DataBind();

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


        protected void combo_Programa()
        {
            string strQuerydire = "";
            strQuerydire = "select distinct tprog_clave clave, tprog_desc programa from tcapr, tprog " +
                            " where tcapr_estatus='A' and tcapr_tcamp_clave='" + ddl_Campus.SelectedValue + "'" +
                            " and tcapr_tprog_clave=tprog_clave  " +
                            "union " +
                            "select '0' clave, '----Selecciona un programa----' programa " +
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

                ddl_Programa.DataSource = TablaEstado;
                ddl_Programa.DataValueField = "Clave";
                ddl_Programa.DataTextField = "Programa";
                ddl_Programa.DataBind();

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
        protected void combo_tipo_ingreso()
        {
            string strQuerydire = "";
            strQuerydire = "select ttiin_clave clave, ttiin_desc Tipo from ttiin where ttiin_estatus='A' " +
                            "union " +
                            "select '0' clave, '----Selecciona un tipo de ingreso----' Tipo " +
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

                ddl_tipo_ingreso.DataSource = TablaEstado;
                ddl_tipo_ingreso.DataValueField = "Clave";
                ddl_tipo_ingreso.DataTextField = "Tipo";
                ddl_tipo_ingreso.DataBind();

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
        protected void combo_tasa_financiera()
        {
            string strQuerydire = "";
            strQuerydire = "select ttasa_clave clave, ttasa_desc Tasa from ttasa where ttasa_estatus='A' " +
                            "union " +
                            "select '0' clave, '----Selecciona una tasa financiera----' Tasa " +
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

                ddl_tasa_f.DataSource = TablaEstado;
                ddl_tasa_f.DataValueField = "Clave";
                ddl_tasa_f.DataTextField = "Tasa";
                ddl_tasa_f.DataBind();

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
        protected void combo_plan_beca()
        {
            string Query = "select distinct tpcbe_clave clave, tpcbe_desc Plan from tsimu, tpcbe, tprog " +
                " where tsimu_id = '" + txt_matricula.Text + "' and tsimu_tcoco_clave = tpcbe_tcoco_cargo " +
                " and tpcbe_tcamp_clave='" + ddl_Campus.SelectedValue + "' and tprog_clave='" + ddl_Programa.SelectedValue + "' " +
                " and (tpcbe_tnive_clave=tprog_tnive_clave or tpcbe_tnive_clave='000')  " +
                " union " +
                " select '0' clave, '--- Plan Cobro / Beca ---' Plan from dual order by clave";

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

                ddl_plan_beca.DataSource = TablaEstado;
                ddl_plan_beca.DataValueField = "Clave";
                ddl_plan_beca.DataTextField = "Plan";
                ddl_plan_beca.DataBind();
                Carga_Simula(null,null);

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
        protected void grid_simulador_bind()
        {
            //Codigo que llena la tabla del simulador. 
        }

        protected void Carga_Simula(object sender, EventArgs e)
        {
            Global.admi_descuento = ddl_plan_beca.SelectedValue.ToString();
            Global.descuento = ddl_plan_beca.SelectedItem.ToString();
            Global.nombre_descuento = ddl_plan_beca.SelectedItem.ToString();

            string strQuery = "";
            strQuery = " select '--' titulo, 0 parcia, tsimu_tcoco_clave concepto, tcoco_desc nombre, tsimu_importe importe, " +
                " tpcbe_porcentaje porc_beca, tpcbe_monto  monto_beca,  " +
                " (select tpaco_pdesc_insc from tpaco, tcaes , tprog where tpaco_clave = '01' " +
                "    and tprog_clave ='" + Global.programa.ToString() + "' and tpaco_tpees_clave ='" + Global.periodo.ToString() + "' " +
                " and tpaco_tcamp_clave ='" + Global.campus.ToString() + "'" +
                " and tpaco_tnive_clave = tprog_tnive_clave and tcaes_tpees_clave = tpaco_tpees_clave " +
                " and tcaes_tcoca_clave = tpaco_tcoca_clave and tcaes_tcamp_clave = tpaco_tcamp_clave " +
                "  and tcaes_tnive_clave = tpaco_tnive_clave  and curdate() between tcaes_inicio and tcaes_fin)  Descuento, " +
                "  fecha(date_format(curdate(),'%Y-%m-%d')) vencimiento " +
                " from tsimu inner join tcoco on tsimu_tcoco_clave = tcoco_clave and tcoco_ind_parc = 'N' " +
                " inner join tprog on tprog_clave ='" + Global.programa.ToString() + "'" +
                " left outer join tpcbe on tsimu_tcoco_clave = tpcbe_tcoco_cargo and tpcbe_clave ='" + ddl_plan_beca.SelectedValue.ToString() + "' " +
                " and tpcbe_tcamp_clave ='" + Global.campus.ToString() + "' and(tpcbe_tnive_clave = tprog_tnive_clave or tpcbe_tnive_clave = '000') " +
                " and tpcbe_estatus='A' " +
                " where tsimu_id ='" + txt_matricula.Text + "'" +
                " union " +
                " select '--' titulo, tfeve_numero parcia, tsimu_tcoco_clave concepto, tcoco_desc nombre, tsimu_importe importe,  " +
                " tpcbe_porcentaje porc_beca, tpcbe_monto mnto_beca, " +
                " (select tpaco_pdesc_parc from tpaco, tcaes , tprog where tpaco_clave = '01' and tprog_clave = '" + Global.programa.ToString() + "'" +
                " and tpaco_tpees_clave ='" + Global.periodo.ToString() + "' and tpaco_tcamp_clave ='" + Global.campus.ToString() + "'" +
                " and tpaco_tnive_clave = tprog_tnive_clave and tcaes_tpees_clave = tpaco_tpees_clave and tcaes_tcoca_clave = tpaco_tcoca_clave " +
                " and tcaes_tcamp_clave = tpaco_tcamp_clave and tcaes_tnive_clave = tpaco_tnive_clave and curdate() between tcaes_inicio and tcaes_fin) Descuento, " +
                " fecha(date_format(tfeve_vencimiento,'%Y-%m-%d')) vencimiento " +
                " from tsimu inner join tcoco on tsimu_tcoco_clave = tcoco_clave and tcoco_ind_parc = 'S' " +
                " inner join tprog on tprog_clave ='" + Global.programa.ToString() + "'" +
                " left outer join tpcbe on tsimu_tcoco_clave = tpcbe_tcoco_cargo and tpcbe_clave ='" + ddl_plan_beca.SelectedValue.ToString() + "'" +
                " and tpcbe_tcamp_clave ='" + Global.campus.ToString() + "'   and(tpcbe_tnive_clave = tprog_tnive_clave or tpcbe_tnive_clave = '000') " +
                " and tpcbe_estatus='A' " +
                "  inner join tfeve on tfeve_tpees_clave ='" + Global.periodo.ToString() + "' and tfeve_ttasa_clave = tsimu_ttasa_clave " +
                "  where tsimu_id ='" + txt_matricula.Text + "' order by parcia, concepto ";

            MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            conexion.Open();
            try
            {
                MySqlDataReader DatosMySql;
                DataTable TablaSimula = new DataTable();

                DataTable copyDataTable;
                copyDataTable = TablaSimula.Copy();
                MySqlCommand ConsultaMySql1 = new MySqlCommand();
                ConsultaMySql1.Connection = conexion;
                ConsultaMySql1.CommandType = CommandType.Text;
                ConsultaMySql1.CommandText = strQuery;
                DatosMySql = ConsultaMySql1.ExecuteReader();
                TablaSimula.Load(DatosMySql, LoadOption.OverwriteChanges);

                MySqlDataAdapter sqladapter = new MySqlDataAdapter();

                DataSet dssql1 = new DataSet();

                MySqlCommand commandsql1 = new MySqlCommand(strQuery, conexion);
                sqladapter.SelectCommand = commandsql1;
                sqladapter.Fill(dssql1);
                sqladapter.Dispose();
                commandsql1.Dispose();

                conexion.Close();

                var grouped = from table in TablaSimula.AsEnumerable()

                              group table by new { placeCol = table["titulo"] } into groupby

                              select new

                              {

                                  Value = groupby.Key,
                                  Cuantos = groupby.Count(),
                                  //media = groupby.Sum(ed => ed.Field<decimal>("CALIFICACION")),
                                  //media = groupby.Sum(x => Convert.ToInt32(x.ItemArray[4].ToString())),
                                  //Average = groupby.Average(ed => ed.Field<decimal>("importe")),
                                  ColumnValues = groupby


                              };

                foreach (var key in grouped)
                {

                    StringBuilder html = new StringBuilder();
                    //html.Append("<table align='center' class='table' cellspacing='0' cellpadding='0' border='0' style='width: 920px'>");

                    /*html.Append("<tr>" +
                                           "<td width='50'>PARC.</td>" +
                                           "<td width='80'>CLAVE</td>" +
                                           "<td width='200'  align='center' >CONCEPTO</td>" +
                                           "<td width='50' align='right'>IMPORTE</td>" +
                                           "<td width='50' align='right'>DESCUENTO</td>" +
                                           "<td width='50' align='right'>BECA</td>" +
                                           "<td width='50' align='right'>SALDO</td>" +
                                           "<td width='80' align='center'>VENCIMIENTO</td>" +
                                           "</tr></table>");
                    html.Append("<table align='center' class='table - responsive' width='1000px' border='1'></table>");
                    */
                    decimal balance = 0; decimal dsco = 0; decimal be = 0; decimal amount = 0;
                    decimal importe = 0; decimal descuento = 0; decimal saldo = 0; decimal beca = 0;
                    DataTable table;
                    table = new System.Data.DataTable();
                    table.Columns.Add("PARC", typeof(System.String));
                    table.Columns.Add("CLAVE", typeof(System.String));
                    table.Columns.Add("CONCEPTO", typeof(System.String));
                    table.Columns.Add("IMPORTE", typeof(System.String));
                    table.Columns.Add("DESCUENTO", typeof(System.String));
                    table.Columns.Add("BECA", typeof(System.String));
                    table.Columns.Add("SALDO", typeof(System.String));
                    table.Columns.Add("VENCIMIENTO", typeof(System.String));
                    for (int z = 0; z < dssql1.Tables[0].Rows.Count; z++)
                    {
                        be = 0; dsco = 0;
                        if (dssql1.Tables[0].Rows[z][7].ToString() != "" && dssql1.Tables[0].Rows[z][7].ToString() != null)
                        {
                            dsco = Math.Round(Convert.ToDecimal(dssql1.Tables[0].Rows[z][7].ToString()) / 100 * Convert.ToDecimal(dssql1.Tables[0].Rows[z][4].ToString()), 2);
                        }
                        if (dssql1.Tables[0].Rows[z][5].ToString() != "" && dssql1.Tables[0].Rows[z][5].ToString() != null)
                        {
                            if (Convert.ToDecimal(dssql1.Tables[0].Rows[z][5].ToString()) > 0)
                            {
                                be = Math.Round(Convert.ToDecimal(dssql1.Tables[0].Rows[z][5].ToString()) / 100 * (Convert.ToDecimal(dssql1.Tables[0].Rows[z][4].ToString()) - dsco), 2);
                            }
                        }
                        if (dssql1.Tables[0].Rows[z][6].ToString() != "" && dssql1.Tables[0].Rows[z][6].ToString() != null)
                        {
                            if (Convert.ToDecimal(dssql1.Tables[0].Rows[z][6].ToString()) > 0)
                            {
                                be = Convert.ToDecimal(dssql1.Tables[0].Rows[z][6].ToString());
                            }
                        }
                        //balance = Convert.ToDecimal(dssql1.Tables[0].Rows[z][4].ToString()) - (Convert.ToDecimal(dssql1.Tables[0].Rows[z][5].ToString()) +
                        //    Convert.ToDecimal(dssql1.Tables[0].Rows[z][6].ToString()));
                        amount = Convert.ToDecimal(dssql1.Tables[0].Rows[z][4].ToString());
                        balance = Math.Round(Convert.ToDecimal(dssql1.Tables[0].Rows[z][4].ToString()) - (dsco + be), 2);
                        /*html.Append("<table align='center' class='table' cellspacing='0' cellpadding='0' border='0' style='width: 920px'>" +
                            "<tr>" +
                                           "<td width='50'>" + dssql1.Tables[0].Rows[z][1].ToString() + "</td>" +
                                           "<td width='80'>" + dssql1.Tables[0].Rows[z][2].ToString() + "</td>" +
                                           "<td width='120'>" + dssql1.Tables[0].Rows[z][3].ToString() + "</td>" +
                                           "<td width='50' align='right'>" + amount.ToString("#.00") + "</td>" +
                                           "<td width='50' align='right'>" + dsco.ToString("#.00") + "</td>" +
                                           "<td width='50' align='right'>" + be.ToString("#.00") + "</td>" +
                                           "<td width='50' align='right'>" + balance.ToString("#.00") + "</td>" +
                                           "<td width='80' align='center' >" + dssql1.Tables[0].Rows[z][8].ToString() + "</td>") ;
                         "</tr>");*/
                        DataRow row;
                        row = table.NewRow();
                        row["PARC"] = dssql1.Tables[0].Rows[z][1].ToString();
                        row["CLAVE"] = dssql1.Tables[0].Rows[z][2].ToString();
                        row["CONCEPTO"] = dssql1.Tables[0].Rows[z][3].ToString();
                        row["IMPORTE"] = amount.ToString("#.00");
                        row["DESCUENTO"] = dsco.ToString("#.00");
                        row["BECA"] = be.ToString("#.00");
                        row["SALDO"] = balance.ToString("#.00");
                        row["VENCIMIENTO"] = dssql1.Tables[0].Rows[z][8].ToString();
                        table.Rows.Add(row);
                        

                        importe = importe + Convert.ToDecimal(dssql1.Tables[0].Rows[z][4].ToString());
                        beca = beca + be;
                        descuento = descuento + dsco;
                        // balance = Convert.ToDecimal(dssql1.Tables[0].Rows[i][4].ToString()) - (Convert.ToDecimal(dssql1.Tables[0].Rows[i][5].ToString()) +
                        //     Convert.ToDecimal(dssql1.Tables[0].Rows[i][6].ToString()));
                        saldo = saldo + balance;

                    }
                    /*html.Append("</table>");
                    html.Append("<table align='center' class='table - responsive' width='1000px' border='1'></table>");
                    html.Append("<table align='center' class='table' cellspacing='0' cellpadding='0' border='0' style='width: 920px'>" +
                            "<tr>" +
                                           "<td width='50'></td>" +
                                           "<td width='80'></td>" +
                                           "<td width='120'></td>" +
                                           "<td width='50' align='right'>" + importe.ToString("#.00") + "</td>" +
                                            "<td width='50' align='right'>" + descuento.ToString("#.00") + "</td>" +
                                           "<td width='50' align='right'>" + beca.ToString("#.00") + "</td>" +
                                           "<td width='50' align='right'>" + saldo.ToString("#.00") + "</td>" +
                                           "<td width='80' align='center' ></td>" +
                                           "</tr>");
                    html.Append("</body>");
                    html.Append("</html>");

                    string Htmltext = html.ToString();
                    PlaceHolder1.Controls.Add(new Literal { Text = html.ToString() });
                    */
                    // ImgPdf.Visible = true;
                    // ImgEdocta.Visible = true;
                    DataRow row1;
                    row1 = table.NewRow();
                    row1["PARC"] = "";
                    row1["CLAVE"] = "";
                    row1["CONCEPTO"] ="TOTALES";
                    row1["IMPORTE"] = importe.ToString("#.00");
                    row1["DESCUENTO"] = descuento.ToString("#.00");
                    row1["BECA"] = beca.ToString("#.00");
                    row1["SALDO"] = saldo.ToString("#.00");
                    row1["VENCIMIENTO"] ="";
                    table.Rows.Add(row1);

                    GridSimulador.DataSource = table;
                    GridSimulador.DataBind();
                    GridSimulador.Visible = true;
                    linkBttnGenerarPdf.Visible = true;
                    //ImgPdf.Visible = true;
                    linkBttnGenEdoCta.Visible = true;
                }
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
            }
            
           

            

            //conexion.Close();
        }

        protected void PDF_Click(object sender, EventArgs e)
        {
            Global.descuento = ddl_plan_beca.SelectedValue.ToString();
            Global.nombre_descuento = ddl_plan_beca.SelectedItem.ToString();
            Global.cuenta = txt_matricula.Text;
            Global.nombre_alumno = txt_nombre.Text;
            Global.nombre_programa = ddl_Programa.SelectedItem.ToString();
            Global.nombre_campus = ddl_Campus.SelectedItem.ToString();
            Global.nombre_periodo = ddl_periodo.SelectedItem.ToString();
            Global.nombre_tipoingreso = ddl_tipo_ingreso.SelectedItem.ToString();

            Response.Redirect("TadmiPDF.aspx");
        }
        protected void Edo_Cta_Click(object sender, EventArgs e)
        {
            string qvalida = " select count(*) from tpers, tedcu " +
                 " where tpers_id='" + txt_matricula.Text + "' and tedcu_tpers_num = tpers_num " +
                 " and tedcu_tpees_clave = '" + Global.periodo + "'" +
                 " and tedcu_tcoco_clave in (select tcomb_tcoco_clave from tcomb, tsimu " +
                            " where tsimu_id=tpers_id and tcomb_ttasa_clave = tsimu_ttasa_clave) ";

            MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            conexion.Open();
            MySqlDataAdapter sqladapter = new MySqlDataAdapter();

            DataSet dsvalida = new DataSet();
            try
            {
                MySqlCommand commandvalida = new MySqlCommand(qvalida, conexion);
                sqladapter.SelectCommand = commandvalida;
                sqladapter.Fill(dsvalida);
                sqladapter.Dispose();
                commandvalida.Dispose();
            }
            catch (Exception ex)
            {
                //resultado.Text = "1--" + ex.Message;
            }
            if (dsvalida.Tables[0].Rows[0][0].ToString() == "0")
            {

                Global.admi_descuento = ddl_plan_beca.SelectedValue.ToString();
                Global.descuento = ddl_plan_beca.SelectedItem.ToString();

                string strQuery = "";
                strQuery = " select '--' titulo, 0 parcia, tsimu_tcoco_clave concepto, tcoco_desc nombre, tsimu_importe importe, " +
                       " case when tpcbe_porcentaje > 0 then round(tsimu_importe*(tpcbe_porcentaje/ 100),2) " +
                            " when tpcbe_monto > 0 then tpcbe_monto " +
                            " when tpcbe_porcentaje is null  then 0 " +
                        " end Beca, " +
                        " case when(select  count(*) from tpaco, tcaes, tprog where tpaco_clave = '01' and tprog_clave='" + Global.programa.ToString() + "'" +
                        " and tpaco_tpees_clave ='" + Global.periodo.ToString() + "' and tpaco_tcamp_clave ='" + Global.campus.ToString() + "'" +
                        " and tpaco_tnive_clave = tprog_tnive_clave and tcaes_tpees_clave = tpaco_tpees_clave and tcaes_tcoca_clave = tpaco_tcoca_clave " +
                        " and tcaes_tcamp_clave = tpaco_tcamp_clave and tcaes_tnive_clave = tpaco_tnive_clave " +
                        " and curdate() between tcaes_inicio and tcaes_fin) > 0 then " +
                        " round((select  tpaco_pdesc_insc from tpaco, tcaes , tprog " +
                        " where tpaco_clave = '01' and tprog_clave = '" + Global.programa.ToString() + "'" +
                        " and tpaco_tpees_clave ='" + Global.periodo.ToString() + "' and tpaco_tcamp_clave ='" + Global.campus.ToString() + "'" +
                        " and tpaco_tnive_clave = tprog_tnive_clave and tcaes_tpees_clave = tpaco_tpees_clave and tcaes_tcoca_clave = tpaco_tcoca_clave " +
                        " and tcaes_tcamp_clave = tpaco_tcamp_clave and tcaes_tnive_clave = tpaco_tnive_clave " +
                        " and curdate() between tcaes_inicio and tcaes_fin) / 100 * tsimu_importe , 2) else 0 end Descuento, " +
                       " date_format(curdate(), '%d/%m/%Y') vencimiento " +
                       " from tsimu " +
                       " inner join tcoco on tsimu_tcoco_clave = tcoco_clave and tcoco_ind_parc = 'N' " +
                       " inner join tprog on tprog_clave='" + Global.programa.ToString() + "'" +
                       " left outer join tpcbe on tsimu_tcoco_clave = tpcbe_tcoco_cargo and tpcbe_clave ='" + ddl_plan_beca.SelectedValue.ToString() + "'" +
                       " and tpcbe_tcamp_clave='" + Global.campus.ToString() + "' and (tpcbe_tnive_clave=tprog_tnive_clave or tpcbe_tnive_clave='000') " +
                       " and tpcbe_estatus='A' " +
                       " where tsimu_id = '" + txt_matricula.Text + "'" +
                       " union " +
                       " select '--' titulo, tfeve_numero parcia, tsimu_tcoco_clave concepto, tcoco_desc nombre, tsimu_importe importe, " +
                       " case when tpcbe_porcentaje > 0 then round(tsimu_importe*(tpcbe_porcentaje/ 100),2) " +
                            " when tpcbe_monto > 0 then tpcbe_monto " +
                            " when tpcbe_porcentaje is null  then 0 " +
                        " end Beca, " +
                        " case when(select  count(*) from tpaco, tcaes, tprog where tpaco_clave = '01' and tprog_clave='" + Global.programa.ToString() + "'" +
                        " and tpaco_tpees_clave ='" + Global.periodo.ToString() + "' and tpaco_tcamp_clave ='" + Global.campus.ToString() + "'" +
                        " and tpaco_tnive_clave = tprog_tnive_clave and tcaes_tpees_clave = tpaco_tpees_clave and tcaes_tcoca_clave = tpaco_tcoca_clave " +
                        " and tcaes_tcamp_clave = tpaco_tcamp_clave and tcaes_tnive_clave = tpaco_tnive_clave " +
                        " and curdate() between tcaes_inicio and tcaes_fin) > 0 then " +
                        " round((select  tpaco_pdesc_parc from tpaco, tcaes , tprog " +
                        " where tpaco_clave = '01' and tprog_clave = '" + Global.programa.ToString() + "'" +
                        " and tpaco_tpees_clave ='" + Global.periodo.ToString() + "' and tpaco_tcamp_clave ='" + Global.campus.ToString() + "'" +
                        " and tpaco_tnive_clave = tprog_tnive_clave and tcaes_tpees_clave = tpaco_tpees_clave and tcaes_tcoca_clave = tpaco_tcoca_clave " +
                        " and tcaes_tcamp_clave = tpaco_tcamp_clave and tcaes_tnive_clave = tpaco_tnive_clave " +
                        " and curdate() between tcaes_inicio and tcaes_fin) / 100 * tsimu_importe , 2) else 0 end Descuento, " +
                        " date_format(tfeve_vencimiento, '%d/%m/%Y') vencimiento " +
                        " from tsimu " +
                        " inner join tcoco on tsimu_tcoco_clave = tcoco_clave and tcoco_ind_parc = 'S' " +
                         " inner join tprog on tprog_clave='" + Global.programa.ToString() + "'" +
                        " left outer join tpcbe on tsimu_tcoco_clave = tpcbe_tcoco_cargo and tpcbe_clave ='" + ddl_plan_beca.SelectedValue.ToString() + "'" +
                        " and tpcbe_tcamp_clave='" + Global.campus.ToString() + "' and (tpcbe_tnive_clave=tprog_tnive_clave or tpcbe_tnive_clave='000') " +
                        " and tpcbe_estatus='A' " +
                        " inner join tfeve on tfeve_tpees_clave = '" + Global.periodo + "' and tfeve_ttasa_clave = tsimu_ttasa_clave " +
                        " where tsimu_id = '" + txt_matricula.Text + "'" +
                        " order by parcia, concepto ";
                //resultado.Text = strQuery;
                double tran = 0;

                string strQueryExiste = "";
                strQueryExiste = " select count(*) from tedcu, tpers " +
                    " where tpers_id='" + txt_matricula.Text + "' and tedcu_tpers_num=tpers_num ";
                try
                {
                    DataSet dsexiste = new DataSet();

                    MySqlCommand commandexiste = new MySqlCommand(strQueryExiste, conexion);
                    sqladapter.SelectCommand = commandexiste;
                    sqladapter.Fill(dsexiste);
                    sqladapter.Dispose();
                    commandexiste.Dispose();
                    if (dsexiste.Tables[0].Rows[0][0].ToString() != "0")
                    {
                        string strQueryTran = "";
                        strQueryTran = " select max(tedcu_consec) from tedcu, tpers " +
                            " where tpers_id='" + txt_matricula.Text + "' and tedcu_tpers_num=tpers_num ";
                        DataSet dssql11 = new DataSet();

                        MySqlCommand commandsql11 = new MySqlCommand(strQueryTran, conexion);
                        sqladapter.SelectCommand = commandsql11;
                        sqladapter.Fill(dssql11);
                        sqladapter.Dispose();
                        commandsql11.Dispose();
                        tran = Convert.ToDouble(dssql11.Tables[0].Rows[0][0].ToString()) + 1;
                    }
                    else
                    {
                        tran = 1;
                    }
                }
                catch (Exception ex)
                {
                    //resultado.Text = ex.Message;
                }
                double pers_num = 0;
                string strQueryPers = "";
                strQueryPers = " select tpers_num from tpers " +
                    " where tpers_id='" + txt_matricula.Text + "'";
                try
                {
                    DataSet dspers = new DataSet();

                    MySqlCommand commandpers = new MySqlCommand(strQueryPers, conexion);
                    sqladapter.SelectCommand = commandpers;
                    sqladapter.Fill(dspers);
                    sqladapter.Dispose();
                    commandpers.Dispose();
                    pers_num = Convert.ToDouble(dspers.Tables[0].Rows[0][0].ToString());
                    //TxtCuenta.Text =  "<pidm>" + pers_num;
                }
                catch (Exception ex)
                {
                    //resultado.Text = ex.Message;
                }


                string strCadSQL = "";
                string no_descuento = "";
                double factura_cons = 0;
                MySqlDataReader DatosMySql;
                DataTable TablaSimula = new DataTable();

                DataTable copyDataTable;
                copyDataTable = TablaSimula.Copy();
                MySqlCommand ConsultaMySql1 = new MySqlCommand();
                ConsultaMySql1.Connection = conexion;
                ConsultaMySql1.CommandType = CommandType.Text;
                ConsultaMySql1.CommandText = strQuery;
                DatosMySql = ConsultaMySql1.ExecuteReader();
                TablaSimula.Load(DatosMySql, LoadOption.OverwriteChanges);

                DataSet dssql1 = new DataSet();

                MySqlCommand commandsql1 = new MySqlCommand(strQuery, conexion);
                sqladapter.SelectCommand = commandsql1;
                sqladapter.Fill(dssql1);
                sqladapter.Dispose();
                commandsql1.Dispose();
                //resultado.Text = "registros insertar:" + dssql1.Tables[0].Rows.Count;
                decimal balance = 0;
                try
                {
                    for (int i = 0; i < dssql1.Tables[0].Rows.Count; i++)
                    {

                        balance = Convert.ToDecimal(dssql1.Tables[0].Rows[i][4].ToString()) - (Convert.ToDecimal(dssql1.Tables[0].Rows[i][5].ToString()) +
                        Convert.ToDecimal(dssql1.Tables[0].Rows[i][6].ToString()));
                        strCadSQL = "INSERT INTO tedcu Values (" + pers_num + ",'" + Global.programa + "','" + Global.periodo + "'," + tran + ",'" +
                            dssql1.Tables[0].Rows[i][2].ToString() + "'," + Convert.ToDouble(dssql1.Tables[0].Rows[i][1].ToString()) + "," +
                            Convert.ToDecimal(dssql1.Tables[0].Rows[i][4].ToString()) + "," + balance + "," +
                            " str_to_date('" + dssql1.Tables[0].Rows[i][7].ToString() + "','%d/%m/%Y'),'" + Session["usuario"].ToString() + "',current_timestamp())";
                        MySqlCommand myCommandinserta = new MySqlCommand(strCadSQL, conexion);
                        //resultado.Text = resultado.Text + strCadSQL;
                        myCommandinserta.ExecuteNonQuery();

                        Decimal descuento = 0, beca = 0;
                        beca = Convert.ToDecimal(dssql1.Tables[0].Rows[i][5].ToString());
                        descuento = Convert.ToDecimal(dssql1.Tables[0].Rows[i][6].ToString());
                        //resultado.Text = resultado.Text + "<>" + strCadSQL;
                        //resultado.Text = resultado.Text + "descuento:" + descuento;
                        if (beca > 0)
                        {
                            if (no_descuento == "")
                            {
                                string strSeqndescuento = " update tcseq set tcseq_numero=tcseq_numero+1 where tcseq_tseqn_clave='003' and tcseq_tcamp_clave='" +
                                    Global.campus.ToString() + "'";
                                MySqlCommand myCommandupd1 = new MySqlCommand(strSeqndescuento, conexion);
                                myCommandupd1.ExecuteNonQuery();


                                string strSeqndesc1 = " select lpad(tcseq_numero, tcseq_longitud,'0') from tcseq where tcseq_tseqn_clave='003' and tcseq_tcamp_clave='" + Global.campus.ToString() + "'";
                                DataSet dsdesc = new DataSet();
                                MySqlCommand commandesc = new MySqlCommand(strSeqndesc1, conexion);
                                sqladapter.SelectCommand = commandesc;
                                sqladapter.Fill(dsdesc);
                                sqladapter.Dispose();
                                commandesc.Dispose();

                                no_descuento = "D" + dsdesc.Tables[0].Rows[0][0].ToString();
                                //resultado.Text = resultado.Text + "-Consecutivo:" + dsdesc.Tables[0].Rows[0][0].ToString();
                            }
                            string strQueryPago = "";
                            strQueryPago = " select tpcbe_tcoco_abono from tpcbe, tprog " +
                                " where tpcbe_clave='" + ddl_plan_beca.SelectedValue.ToString() + "' and tpcbe_tcoco_cargo='" +
                                dssql1.Tables[0].Rows[i][2].ToString() + "' and tpcbe_tcamp_clave='" + Global.campus.ToString() + "' " +
                                " and tprog_clave='" + Global.programa.ToString() + "' and (tpcbe_tnive_clave=tprog_tnive_clave or tpcbe_tnive_clave='000') ";

                            DataSet dspago = new DataSet();

                            MySqlCommand commandpago = new MySqlCommand(strQueryPago, conexion);
                            sqladapter.SelectCommand = commandpago;
                            sqladapter.Fill(dspago);
                            sqladapter.Dispose();
                            commandpago.Dispose();
                            factura_cons = factura_cons + 1;
                            //resultado.Text = resultado.Text + "-tcoco-abono:" + dspago.Tables[0].Rows[0][0].ToString();
                            string strPago = "INSERT INTO tpago Values (" + pers_num + ",'" + Global.programa + "','" +
                             dspago.Tables[0].Rows[0][0].ToString() + "'," + Convert.ToDecimal(dssql1.Tables[0].Rows[i][5].ToString()) + "," +
                             tran + "," + factura_cons + ",'A','" + no_descuento + "','" + ddl_plan_beca.SelectedValue.ToString() + "','" + Global.usuario.ToString() + "',current_timestamp())";
                            //resultado.Text = resultado.Text + "---" + strPago;
                            MySqlCommand myCommandinsertapago = new MySqlCommand(strPago, conexion);
                            myCommandinsertapago.ExecuteNonQuery();
                            //resultado.Text = strPago;

                        }

                        if (descuento > 0)
                        {
                            if (no_descuento == "")
                            {
                                string strSeqndescuento = " update tcseq set tcseq_numero=tcseq_numero+1 where tcseq_tseqn_clave='003' and tcseq_tcamp_clave='" +
                                    Global.campus.ToString() + "'";
                                MySqlCommand myCommandupd1 = new MySqlCommand(strSeqndescuento, conexion);
                                myCommandupd1.ExecuteNonQuery();


                                string strSeqndesc1 = " select lpad(tcseq_numero, tcseq_longitud,'0') from tcseq where tcseq_tseqn_clave='003' and tcseq_tcamp_clave='" +
                                    Global.campus.ToString() + "'";
                                DataSet dsdesc = new DataSet();
                                MySqlCommand commandesc = new MySqlCommand(strSeqndesc1, conexion);
                                sqladapter.SelectCommand = commandesc;
                                sqladapter.Fill(dsdesc);
                                sqladapter.Dispose();
                                commandesc.Dispose();

                                no_descuento = "D" + dsdesc.Tables[0].Rows[0][0].ToString();
                                //resultado.Text = resultado.Text + "-Consecutivo:" + dsdesc.Tables[0].Rows[0][0].ToString();
                            }
                            string strQueryPago = "";
                            strQueryPago = " select tpaco_tcoco_clave from tpaco, tprog " +
                                " where tpaco_tpees_clave='" + Global.periodo.ToString() + "' and tprog_clave='" + Global.programa.ToString() + "'" +
                                " and   tpaco_tnive_clave=tprog_tnive_clave and tpaco_clave='01' ";

                            DataSet dspago = new DataSet();

                            MySqlCommand commandpago = new MySqlCommand(strQueryPago, conexion);
                            sqladapter.SelectCommand = commandpago;
                            sqladapter.Fill(dspago);
                            sqladapter.Dispose();
                            commandpago.Dispose();
                            factura_cons = factura_cons + 1;
                            //resultado.Text = resultado.Text + "-tcoco-abono:" + dspago.Tables[0].Rows[0][0].ToString();
                            string strPago = "INSERT INTO tpago Values (" + pers_num + ",'" + Global.programa + "','" +
                             dspago.Tables[0].Rows[0][0].ToString() + "'," + Convert.ToDecimal(dssql1.Tables[0].Rows[i][6].ToString()) + "," +
                             tran + "," + factura_cons + ",'A','" + no_descuento + "','" + ddl_plan_beca.SelectedValue.ToString() + "','" + Global.usuario.ToString() + "',current_timestamp())";
                            //resultado.Text = resultado.Text + "---" + strPago;
                            MySqlCommand myCommandinsertapago = new MySqlCommand(strPago, conexion);
                            myCommandinsertapago.ExecuteNonQuery();

                        }
                        tran = tran + 1;
                    }
                    string strDescuento = "INSERT INTO tepcb Values (" + pers_num + ",'" + Global.periodo + "','" + Global.programa + "','" +
                             ddl_plan_beca.SelectedValue.ToString() + "',1,'A','" + Session["usuario"].ToString() + "',current_timestamp())";
                    MySqlCommand myCommandinsertadesc = new MySqlCommand(strDescuento, conexion);
                    //resultado.Text = resultado.Text + "---" + strDescuento;
                    myCommandinsertadesc.ExecuteNonQuery();

                    string strQueryConsecutivo = " select max(tadmi_consecutivo) from tadmi where tadmi_tpers_num=" + pers_num;
                    DataSet dscons = new DataSet();
                    MySqlCommand commandsqlcons = new MySqlCommand(strQueryConsecutivo, conexion);
                    sqladapter.SelectCommand = commandsqlcons;
                    sqladapter.Fill(dscons);
                    sqladapter.Dispose();
                    commandsqlcons.Dispose();

                    string strActualizastso = " update tadmi set tadmi_tstso_clave='EN' " +
                        " where tadmi_tpers_num=" + pers_num + " and tadmi_consecutivo=" +
                        Convert.ToDouble(dscons.Tables[0].Rows[0][0].ToString());
                    MySqlCommand myCommandactualizastso = new MySqlCommand(strActualizastso, conexion);
                    myCommandactualizastso.ExecuteNonQuery();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Exitoso", "Exitoso();", true);
                    //resultado.Text = resultado.Text + "Generación de Estado de Cuenta realizada! ";
                }
                catch (Exception ex)
                {
                    //resultado.Text = "6--" + ex.Message;
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "YaExiste", "YaExiste();", true);
                //resultado.Text = "Ya existe Estado de cuenta para el periodo !!";
            }
            conexion.Close();
        }

        protected void back_Click(object sender, EventArgs e)
        {
            Response.Redirect("tadmi.aspx");
        }

        protected void linkBttnBusca_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_matricula.Text != string.Empty)
                {
                    //btn_cancelar.Visible = true;
                    //btn_update.Visible = true;
                    //btn_save.Visible = false;
                    Carga_Estudiante();
                }
                else
                {
                    //GridAlumnos.Visible = true;
                    //GridAlumnos.DataSource = serviceAlumno.ObtenerAlumnos(txt_matricula.Text);
                    //GridAlumnos.DataBind();
                }
            }
            catch (Exception ex)
            {
                //Logs
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tsimu", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void Carga_Estudiante()
        {
            ModelObtenerAlumnosResponse datosAlumno = new ModelObtenerAlumnosResponse();
            txt_nombre.Text = string.Empty;
            DataTable dtAlumno = new DataTable();

            if (txt_matricula.Text != "")
            {

                try
                {
                    dtAlumno = serviceAlumno.ObtenerAlumnos(txt_matricula.Text);
                    if (dtAlumno.Rows.Count == 1)
                    {
                        txt_nombre.Text = dtAlumno.Rows[0][1].ToString() + " " + dtAlumno.Rows[0][2].ToString() + " " + dtAlumno.Rows[0][3].ToString();

                        combo_periodo();
                        ddl_periodo.SelectedValue = Global.periodo.ToString();
                        combo_campus();
                        ddl_Campus.SelectedValue = Global.campus.ToString();
                        combo_Programa();
                        
                        ddl_Programa.SelectedValue = Global.programa.ToString();
                        combo_tipo_ingreso();
                        ddl_tipo_ingreso.SelectedValue = Global.tipo_ingreso.ToString();
                        combo_tasa_financiera();
                        ddl_tasa_f.SelectedValue = Global.tasa.ToString();
                        combo_plan_beca();

                        Global.cuenta = txt_matricula.Text;
                        //Global.nombre = ds.Tables[0].Rows[0][0].ToString();
                        //Global.ap_paterno = ds.Tables[0].Rows[0][1].ToString();
                        //Global.ap_materno = ds.Tables[0].Rows[0][2].ToString();
                        //txt_nombre.Text = ds.Tables[0].Rows[0][0].ToString() + " " + ds.Tables[0].Rows[0][1].ToString() +
                        //    " " + ds.Tables[0].Rows[0][2].ToString();
                        //grid_direccion_bind(txt_matricula.Text);
                    }
                    else
                    {
                        if (txt_matricula.Text != "")
                        {
                            txt_matricula.Text = "";
                            txt_nombre.Text = "";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "NoexisteAlumno", "NoexisteAlumno();", true);

                        }
                    }
                }
                catch (Exception ex)
                {
                    //resultado.Text = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tsimu", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                }

                //Estudiantes.Visible = true;
                txt_matricula.Focus();
            }
        }

        protected void linkBttnGenerarPdf_Click(object sender, EventArgs e)
        {
            Global.descuento = ddl_plan_beca.SelectedValue.ToString();
            Global.nombre_descuento = ddl_plan_beca.SelectedItem.ToString();
            Global.cuenta = txt_matricula.Text;
            Global.nombre_alumno = txt_nombre.Text;
            Global.nombre_programa = ddl_Programa.SelectedItem.ToString();
            Global.nombre_campus = ddl_Campus.SelectedItem.ToString();
            Global.nombre_periodo = ddl_periodo.SelectedItem.ToString();
            Global.nombre_tipoingreso = ddl_tipo_ingreso.SelectedItem.ToString();

            Response.Redirect("TadmiPDF.aspx");
        }

        protected void linkBttnGenEdoCta_Click(object sender, EventArgs e)
        {
            string qvalida = " select count(*) from tpers, tedcu " +
                             " where tpers_id='" + txt_matricula.Text + "' and tedcu_tpers_num = tpers_num " +
                             " and tedcu_tpees_clave = '" + Global.periodo + "'" +
                             " and tedcu_tcoco_clave in (select tcomb_tcoco_clave from tcomb, tsimu " +
                                        " where tsimu_id=tpers_id and tcomb_ttasa_clave = tsimu_ttasa_clave) ";

            MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            conexion.Open();
            MySqlDataAdapter sqladapter = new MySqlDataAdapter();

            DataSet dsvalida = new DataSet();
            try
            {
                MySqlCommand commandvalida = new MySqlCommand(qvalida, conexion);
                sqladapter.SelectCommand = commandvalida;
                sqladapter.Fill(dsvalida);
                sqladapter.Dispose();
                commandvalida.Dispose();
            }
            catch (Exception ex)
            {
                //resultado.Text = "1--" + ex.Message;
            }
            if (dsvalida.Tables[0].Rows[0][0].ToString() == "0")
            {

                Global.admi_descuento = ddl_plan_beca.SelectedValue.ToString();
                Global.descuento = ddl_plan_beca.SelectedItem.ToString();

                string strQuery = "";
                strQuery = " select '--' titulo, 0 parcia, tsimu_tcoco_clave concepto, tcoco_desc nombre, tsimu_importe importe, " +
                       " case when tpcbe_porcentaje > 0 then round(tsimu_importe*(tpcbe_porcentaje/ 100),2) " +
                            " when tpcbe_monto > 0 then tpcbe_monto " +
                            " when tpcbe_porcentaje is null  then 0 " +
                        " end Beca, " +
                        " case when(select  count(*) from tpaco, tcaes, tprog where tpaco_clave = '01' and tprog_clave='" + Global.programa.ToString() + "'" +
                        " and tpaco_tpees_clave ='" + Global.periodo.ToString() + "' and tpaco_tcamp_clave ='" + Global.campus.ToString() + "'" +
                        " and tpaco_tnive_clave = tprog_tnive_clave and tcaes_tpees_clave = tpaco_tpees_clave and tcaes_tcoca_clave = tpaco_tcoca_clave " +
                        " and tcaes_tcamp_clave = tpaco_tcamp_clave and tcaes_tnive_clave = tpaco_tnive_clave " +
                        " and curdate() between tcaes_inicio and tcaes_fin) > 0 then " +
                        " round((select  tpaco_pdesc_insc from tpaco, tcaes , tprog " +
                        " where tpaco_clave = '01' and tprog_clave = '" + Global.programa.ToString() + "'" +
                        " and tpaco_tpees_clave ='" + Global.periodo.ToString() + "' and tpaco_tcamp_clave ='" + Global.campus.ToString() + "'" +
                        " and tpaco_tnive_clave = tprog_tnive_clave and tcaes_tpees_clave = tpaco_tpees_clave and tcaes_tcoca_clave = tpaco_tcoca_clave " +
                        " and tcaes_tcamp_clave = tpaco_tcamp_clave and tcaes_tnive_clave = tpaco_tnive_clave " +
                        " and curdate() between tcaes_inicio and tcaes_fin) / 100 * tsimu_importe , 2) else 0 end Descuento, " +
                       " date_format(curdate(), '%d/%m/%Y') vencimiento " +
                       " from tsimu " +
                       " inner join tcoco on tsimu_tcoco_clave = tcoco_clave and tcoco_ind_parc = 'N' " +
                       " inner join tprog on tprog_clave='" + Global.programa.ToString() + "'" +
                       " left outer join tpcbe on tsimu_tcoco_clave = tpcbe_tcoco_cargo and tpcbe_clave ='" + ddl_plan_beca.SelectedValue.ToString() + "'" +
                       " and tpcbe_tcamp_clave='" + Global.campus.ToString() + "' and (tpcbe_tnive_clave=tprog_tnive_clave or tpcbe_tnive_clave='000') " +
                       " where tsimu_id = '" + txt_matricula.Text + "'" +
                       " union " +
                       " select '--' titulo, tfeve_numero parcia, tsimu_tcoco_clave concepto, tcoco_desc nombre, tsimu_importe importe, " +
                       " case when tpcbe_porcentaje > 0 then round(tsimu_importe*(tpcbe_porcentaje/ 100),2) " +
                            " when tpcbe_monto > 0 then tpcbe_monto " +
                            " when tpcbe_porcentaje is null  then 0 " +
                        " end Beca, " +
                        " case when(select  count(*) from tpaco, tcaes, tprog where tpaco_clave = '01' and tprog_clave='" + Global.programa.ToString() + "'" +
                        " and tpaco_tpees_clave ='" + Global.periodo.ToString() + "' and tpaco_tcamp_clave ='" + Global.campus.ToString() + "'" +
                        " and tpaco_tnive_clave = tprog_tnive_clave and tcaes_tpees_clave = tpaco_tpees_clave and tcaes_tcoca_clave = tpaco_tcoca_clave " +
                        " and tcaes_tcamp_clave = tpaco_tcamp_clave and tcaes_tnive_clave = tpaco_tnive_clave " +
                        " and curdate() between tcaes_inicio and tcaes_fin) > 0 then " +
                        " round((select  tpaco_pdesc_parc from tpaco, tcaes , tprog " +
                        " where tpaco_clave = '01' and tprog_clave = '" + Global.programa.ToString() + "'" +
                        " and tpaco_tpees_clave ='" + Global.periodo.ToString() + "' and tpaco_tcamp_clave ='" + Global.campus.ToString() + "'" +
                        " and tpaco_tnive_clave = tprog_tnive_clave and tcaes_tpees_clave = tpaco_tpees_clave and tcaes_tcoca_clave = tpaco_tcoca_clave " +
                        " and tcaes_tcamp_clave = tpaco_tcamp_clave and tcaes_tnive_clave = tpaco_tnive_clave " +
                        " and curdate() between tcaes_inicio and tcaes_fin) / 100 * tsimu_importe , 2) else 0 end Descuento, " +
                        " date_format(tfeve_vencimiento, '%d/%m/%Y') vencimiento " +
                        " from tsimu " +
                        " inner join tcoco on tsimu_tcoco_clave = tcoco_clave and tcoco_ind_parc = 'S' " +
                         " inner join tprog on tprog_clave='" + Global.programa.ToString() + "'" +
                        " left outer join tpcbe on tsimu_tcoco_clave = tpcbe_tcoco_cargo and tpcbe_clave ='" + ddl_plan_beca.SelectedValue.ToString() + "'" +
                        " and tpcbe_tcamp_clave='" + Global.campus.ToString() + "' and (tpcbe_tnive_clave=tprog_tnive_clave or tpcbe_tnive_clave='000') " +
                        " inner join tfeve on tfeve_tpees_clave = '" + Global.periodo + "' and tfeve_ttasa_clave = tsimu_ttasa_clave " +
                        " where tsimu_id = '" + txt_matricula.Text + "'" +
                        " order by parcia, concepto ";
                //resultado.Text = strQuery;
                double tran = 0;

                string strQueryExiste = "";
                strQueryExiste = " select count(*) from tedcu, tpers " +
                    " where tpers_id='" + txt_matricula.Text + "' and tedcu_tpers_num=tpers_num ";
                try
                {
                    DataSet dsexiste = new DataSet();

                    MySqlCommand commandexiste = new MySqlCommand(strQueryExiste, conexion);
                    sqladapter.SelectCommand = commandexiste;
                    sqladapter.Fill(dsexiste);
                    sqladapter.Dispose();
                    commandexiste.Dispose();
                    if (dsexiste.Tables[0].Rows[0][0].ToString() != "0")
                    {
                        string strQueryTran = "";
                        strQueryTran = " select max(tedcu_consec) from tedcu, tpers " +
                            " where tpers_id='" + txt_matricula.Text + "' and tedcu_tpers_num=tpers_num ";
                        DataSet dssql11 = new DataSet();

                        MySqlCommand commandsql11 = new MySqlCommand(strQueryTran, conexion);
                        sqladapter.SelectCommand = commandsql11;
                        sqladapter.Fill(dssql11);
                        sqladapter.Dispose();
                        commandsql11.Dispose();
                        tran = Convert.ToDouble(dssql11.Tables[0].Rows[0][0].ToString()) + 1;
                    }
                    else
                    {
                        tran = 1;
                    }
                }
                catch (Exception ex)
                {
                    //resultado.Text = ex.Message;
                }
                double pers_num = 0;
                string strQueryPers = "";
                strQueryPers = " select tpers_num from tpers " +
                    " where tpers_id='" + txt_matricula.Text + "'";
                try
                {
                    DataSet dspers = new DataSet();

                    MySqlCommand commandpers = new MySqlCommand(strQueryPers, conexion);
                    sqladapter.SelectCommand = commandpers;
                    sqladapter.Fill(dspers);
                    sqladapter.Dispose();
                    commandpers.Dispose();
                    pers_num = Convert.ToDouble(dspers.Tables[0].Rows[0][0].ToString());
                    //TxtCuenta.Text =  "<pidm>" + pers_num;
                }
                catch (Exception ex)
                {
                    //resultado.Text = ex.Message;
                }


                string strCadSQL = "";
                string no_descuento = "";
                double factura_cons = 0;
                MySqlDataReader DatosMySql;
                DataTable TablaSimula = new DataTable();

                DataTable copyDataTable;
                copyDataTable = TablaSimula.Copy();
                MySqlCommand ConsultaMySql1 = new MySqlCommand();
                ConsultaMySql1.Connection = conexion;
                ConsultaMySql1.CommandType = CommandType.Text;
                ConsultaMySql1.CommandText = strQuery;
                DatosMySql = ConsultaMySql1.ExecuteReader();
                TablaSimula.Load(DatosMySql, LoadOption.OverwriteChanges);

                DataSet dssql1 = new DataSet();

                MySqlCommand commandsql1 = new MySqlCommand(strQuery, conexion);
                sqladapter.SelectCommand = commandsql1;
                sqladapter.Fill(dssql1);
                sqladapter.Dispose();
                commandsql1.Dispose();
                //resultado.Text = "registros insertar:" + dssql1.Tables[0].Rows.Count;
                decimal balance = 0;
                try
                {
                    for (int i = 0; i < dssql1.Tables[0].Rows.Count; i++)
                    {

                        balance = Convert.ToDecimal(dssql1.Tables[0].Rows[i][4].ToString()) - (Convert.ToDecimal(dssql1.Tables[0].Rows[i][5].ToString()) +
                        Convert.ToDecimal(dssql1.Tables[0].Rows[i][6].ToString()));
                        strCadSQL = "INSERT INTO tedcu Values (" + pers_num + ",'" + Global.programa + "','" + Global.periodo + "'," + tran + ",'" +
                            dssql1.Tables[0].Rows[i][2].ToString() + "'," + Convert.ToDouble(dssql1.Tables[0].Rows[i][1].ToString()) + "," +
                            Convert.ToDecimal(dssql1.Tables[0].Rows[i][4].ToString()) + "," + balance + "," +
                            " str_to_date('" + dssql1.Tables[0].Rows[i][7].ToString() + "','%d/%m/%Y'),'" + Session["usuario"].ToString() + "',current_timestamp())";
                        MySqlCommand myCommandinserta = new MySqlCommand(strCadSQL, conexion);
                        //resultado.Text = resultado.Text + strCadSQL;
                        myCommandinserta.ExecuteNonQuery();

                        Decimal descuento = 0, beca = 0;
                        beca = Convert.ToDecimal(dssql1.Tables[0].Rows[i][5].ToString());
                        descuento = Convert.ToDecimal(dssql1.Tables[0].Rows[i][6].ToString());
                        //resultado.Text = resultado.Text + "<>" + strCadSQL;
                        //resultado.Text = resultado.Text + "descuento:" + descuento;
                        if (beca > 0)
                        {
                            if (no_descuento == "")
                            {
                                string strSeqndescuento = " update tcseq set tcseq_numero=tcseq_numero+1 where tcseq_tseqn_clave='003' and tcseq_tcamp_clave='" +
                                    Global.campus.ToString() + "'";
                                MySqlCommand myCommandupd1 = new MySqlCommand(strSeqndescuento, conexion);
                                myCommandupd1.ExecuteNonQuery();


                                string strSeqndesc1 = " select lpad(tcseq_numero, tcseq_longitud,'0') from tcseq where tcseq_tseqn_clave='003' and tcseq_tcamp_clave='" + Global.campus.ToString() + "'";
                                DataSet dsdesc = new DataSet();
                                MySqlCommand commandesc = new MySqlCommand(strSeqndesc1, conexion);
                                sqladapter.SelectCommand = commandesc;
                                sqladapter.Fill(dsdesc);
                                sqladapter.Dispose();
                                commandesc.Dispose();

                                no_descuento = "D" + dsdesc.Tables[0].Rows[0][0].ToString();
                                //resultado.Text = resultado.Text + "-Consecutivo:" + dsdesc.Tables[0].Rows[0][0].ToString();
                            }
                            string strQueryPago = "";
                            strQueryPago = " select tpcbe_tcoco_abono from tpcbe, tprog " +
                                " where tpcbe_clave='" + ddl_plan_beca.SelectedValue.ToString() + "' and tpcbe_tcoco_cargo='" +
                                dssql1.Tables[0].Rows[i][2].ToString() + "' and tpcbe_tcamp_clave='" + Global.campus.ToString() + "' " +
                                " and tprog_clave='" + Global.programa.ToString() + "' and (tpcbe_tnive_clave=tprog_tnive_clave or tpcbe_tnive_clave='000') ";

                            DataSet dspago = new DataSet();

                            MySqlCommand commandpago = new MySqlCommand(strQueryPago, conexion);
                            sqladapter.SelectCommand = commandpago;
                            sqladapter.Fill(dspago);
                            sqladapter.Dispose();
                            commandpago.Dispose();
                            factura_cons = factura_cons + 1;
                            //resultado.Text = resultado.Text + "-tcoco-abono:" + dspago.Tables[0].Rows[0][0].ToString();
                            string strPago = "INSERT INTO tpago Values (" + pers_num + ",'" + Global.programa + "','" +
                             dspago.Tables[0].Rows[0][0].ToString() + "'," + Convert.ToDecimal(dssql1.Tables[0].Rows[i][5].ToString()) + "," +
                             tran + "," + factura_cons + ",'A','" + no_descuento + "','" + ddl_plan_beca.SelectedValue.ToString() + "','" + Global.usuario.ToString() + "',current_timestamp())";
                            //resultado.Text = resultado.Text + "---" + strPago;
                            MySqlCommand myCommandinsertapago = new MySqlCommand(strPago, conexion);
                            myCommandinsertapago.ExecuteNonQuery();
                            //resultado.Text = strPago;

                        }

                        if (descuento > 0)
                        {
                            if (no_descuento == "")
                            {
                                string strSeqndescuento = " update tcseq set tcseq_numero=tcseq_numero+1 where tcseq_tseqn_clave='003' and tcseq_tcamp_clave='" +
                                    Global.campus.ToString() + "'";
                                MySqlCommand myCommandupd1 = new MySqlCommand(strSeqndescuento, conexion);
                                myCommandupd1.ExecuteNonQuery();


                                string strSeqndesc1 = " select lpad(tcseq_numero, tcseq_longitud,'0') from tcseq where tcseq_tseqn_clave='003' and tcseq_tcamp_clave='" +
                                    Global.campus.ToString() + "'";
                                DataSet dsdesc = new DataSet();
                                MySqlCommand commandesc = new MySqlCommand(strSeqndesc1, conexion);
                                sqladapter.SelectCommand = commandesc;
                                sqladapter.Fill(dsdesc);
                                sqladapter.Dispose();
                                commandesc.Dispose();

                                no_descuento = "D" + dsdesc.Tables[0].Rows[0][0].ToString();
                                //resultado.Text = resultado.Text + "-Consecutivo:" + dsdesc.Tables[0].Rows[0][0].ToString();
                            }
                            string strQueryPago = "";
                            strQueryPago = " select tpaco_tcoco_clave from tpaco, tprog " +
                                " where tpaco_tpees_clave='" + Global.periodo.ToString() + "' and tprog_clave='" + Global.programa.ToString() + "'" +
                                " and   tpaco_tnive_clave=tprog_tnive_clave and tpaco_clave='01' ";

                            DataSet dspago = new DataSet();

                            MySqlCommand commandpago = new MySqlCommand(strQueryPago, conexion);
                            sqladapter.SelectCommand = commandpago;
                            sqladapter.Fill(dspago);
                            sqladapter.Dispose();
                            commandpago.Dispose();
                            factura_cons = factura_cons + 1;
                            //resultado.Text = resultado.Text + "-tcoco-abono:" + dspago.Tables[0].Rows[0][0].ToString();
                            string strPago = "INSERT INTO tpago Values (" + pers_num + ",'" + Global.programa + "','" +
                             dspago.Tables[0].Rows[0][0].ToString() + "'," + Convert.ToDecimal(dssql1.Tables[0].Rows[i][6].ToString()) + "," +
                             tran + "," + factura_cons + ",'A','" + no_descuento + "','" + ddl_plan_beca.SelectedValue.ToString() + "','" + Global.usuario.ToString() + "',current_timestamp())";
                            //resultado.Text = resultado.Text + "---" + strPago;
                            MySqlCommand myCommandinsertapago = new MySqlCommand(strPago, conexion);
                            myCommandinsertapago.ExecuteNonQuery();

                        }
                        tran = tran + 1;
                    }
                    string strDescuento = "INSERT INTO tepcb Values (" + pers_num + ",'" + Global.periodo + "','" + Global.programa + "','" +
                             ddl_plan_beca.SelectedValue.ToString() + "',1,'A','" + Session["usuario"].ToString() + "',current_timestamp())";
                    MySqlCommand myCommandinsertadesc = new MySqlCommand(strDescuento, conexion);
                    //resultado.Text = resultado.Text + "---" + strDescuento;
                    myCommandinsertadesc.ExecuteNonQuery();

                    string strQueryConsecutivo = " select max(tadmi_consecutivo) from tadmi where tadmi_tpers_num=" + pers_num;
                    DataSet dscons = new DataSet();
                    MySqlCommand commandsqlcons = new MySqlCommand(strQueryConsecutivo, conexion);
                    sqladapter.SelectCommand = commandsqlcons;
                    sqladapter.Fill(dscons);
                    sqladapter.Dispose();
                    commandsqlcons.Dispose();

                    string strActualizastso = " update tadmi set tadmi_tstso_clave='EN' " +
                        " where tadmi_tpers_num=" + pers_num + " and tadmi_consecutivo=" +
                        Convert.ToDouble(dscons.Tables[0].Rows[0][0].ToString());
                    MySqlCommand myCommandactualizastso = new MySqlCommand(strActualizastso, conexion);
                    myCommandactualizastso.ExecuteNonQuery();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Exitoso", "Exitoso();", true);
                    //resultado.Text = resultado.Text + "Generación de Estado de Cuenta realizada! ";
                }
                catch (Exception ex)
                {
                    //resultado.Text = "6--" + ex.Message;
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "YaExiste", "YaExiste();", true);
                //resultado.Text = "Ya existe Estado de cuenta para el periodo !!";
            }
            conexion.Close();
        }
    }
}