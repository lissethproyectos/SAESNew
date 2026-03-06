using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MySql.Data.MySqlClient;
using System.Text;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace SAES_v1
{
    public partial class Tcedc : System.Web.UI.Page
    {
        public static double reporte = 0;
        public static double transaccion;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
            Label2.Text = "--";
            Label3.Text = "--";
            Label4.Text = "--";
            Label5.Text = "--";
            Consulta_Click();

        }

        private void Consulta_Click()
        {
            string userId = Global.cuenta;  //"U99201091"; // "U99204302"; //
            string nombre = Global.nombre_alumno;
            string programa = Global.programa;
            string nombre_programa = Global.nombre_programa;
            string strQuery = "";
            string strQueryCargos = "";
            string strQueryPagos = "";
            string strQueryBecas = "";
            string strQueryCancelaciones = "";
            Lblerror.Visible = false;
            LblNombre.Text = "";
            Label6.Text = "";
            Lblerror.Text = "";
            GridViewConceptos.Visible = false;
            UpdatePanel1.Visible = true;
            TxtMatricula.Text = userId;
            LblNombre.Text = nombre;
            Label6.Text = nombre_programa;

            strQuery =
                 @"select tedcu_consec TRANS_CARGO,tedcu_tpees_clave PERIODO_CARGO, " +
                 " tcoco_desc DESC_CONCEPTO_CARGO, tedcu_importe IMPORTE,  " +
                 " null PAGO, " +
                 " null FECHA_VENCIMIENTO_CARGO, tedcu_balance SALDO_PENDIENTE ,'C' TIPO , tpers_id IDEN, concat(tpers_nombre, ' ', tpers_paterno, ' ', tpers_materno) NOMBRE, 0 factura, '' desc_concepto, " +
                 "  date_format(tedcu_fecha_venc, '%d/%m/%Y') vencimiento " +
                 " from tedcu z, tcoco, tpers " +
                 " where tpers_id='" + userId + "'" +
                 " and z.tedcu_tpers_num=tpers_num and z.tedcu_tprog_clave='" + programa + "'" +
                 " and  tcoco_clave=z.tedcu_tcoco_clave and tcoco_tipo='C' " +
                 " union " +
                 " select tedcu_consec TRANS_CARGO, tedcu_tpees_clave PERIODO_CARGO, " +
                 " (select distinct tpcbe_desc from tpcbe where tpcbe_clave=tpago_tpcbe_clave " +
                 " and (tpcbe_tcamp_clave='" + Global.campus.ToString() + "' or tpcbe_tcamp_clave='000') " +
                 " and (tpcbe_tnive_clave=tprog_tnive_clave or tpcbe_tnive_clave='000')) DESC_CONCEPTO_CARGO, " +
                 //tcoco_desc DESC_CONCEPTO_CARGO, " +
                 " 0 IMPORTE, tpago_importe PAGO, date_format(tpago_date, '%d/%m/%Y') FECHA_VENCIMIENTO_CARGO, 0 SALDO_PENDIENTE, 'P' TIPO, tpers_id IDEN, " +
                 " concat(tpers_nombre, ' ', tpers_paterno, ' ', tpers_materno) NOMBRE, tpago_factura factura, tcoco_desc desc_concepto, '' vencimiento " +
                 " from tpers, tedcu, tpago, tcoco, tpcbe, tprog " +
                 " where tpers_id = '" + userId + "' and tedcu_tpers_num = tpers_num and tedcu_tprog_clave='" + programa + "' and tpago_tpers_num =tedcu_tpers_num " +
                 " and tpago_tedcu_consec = tedcu_consec and tpago_estatus = 'A' and tpago_tcoco_clave = tcoco_clave " +
                 " and  tprog_clave=tedcu_tprog_clave " +
                 " order by  TRANS_CARGO desc, TIPO, factura ";

            strQueryCargos =
           @" select sum(tedcu_importe) from tedcu z, tpers , tcoco " +
            " where tpers_id='" + userId + "'  and z.tedcu_tpers_num=tpers_num and  tcoco_clave=z.tedcu_tcoco_clave and tcoco_tipo='C' " +
            " and tedcu_tprog_clave='" + programa + "'";

            strQueryPagos =
            @" select sum(tpago_importe) from tedcu z, tpago, tpers, tcoco " +
             " where tpers_id='" + userId + "'  and z.tedcu_tpers_num=tpers_num and tpago_tpers_num=tedcu_tpers_num " +
             " and   tpago_tedcu_consec=tedcu_consec and tpago_estatus='A' and tedcu_tprog_clave='" + programa + "'" +
             " and   tpago_tcoco_clave = tcoco_clave and tcoco_categ in ('PC','PB', 'SS') ";

            strQueryBecas =
              @" select sum(tpago_importe) from tedcu z, tpago, tpers, tcoco " +
              " where tpers_id='" + userId + "'  and z.tedcu_tpers_num=tpers_num and tpago_tpers_num=tedcu_tpers_num " +
              " and   tpago_tedcu_consec=tedcu_consec and tpago_estatus='A' and tedcu_tprog_clave='" + programa + "'" +
              " and   tpago_tcoco_clave = tcoco_clave and tcoco_categ in ('DE','BE')";

            strQueryCancelaciones =
            @" select sum(tpago_importe) from tedcu z, tpago, tpers, tcoco " +
             " where tpers_id='" + userId + "'  and z.tedcu_tpers_num=tpers_num and tpago_tpers_num=tedcu_tpers_num " +
             " and   tpago_tedcu_consec=tedcu_consec and tpago_estatus='A' and tedcu_tprog_clave='" + programa + "'" +
             " and   tpago_tcoco_clave = tcoco_clave and tcoco_categ in ('IN','CO') ";

            Lblerror.Visible = true;
            //Lblerror.Text = strQuery; // + "--" + strQueryCargos + "--" + strQueryPagos + "--" + strQueryBecas;
            try
            {
                MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                conexion.Open();
                MySqlDataReader DatosBanner;
                DataTable ds = new DataTable();
                MySqlCommand ConsultaMySql = new MySqlCommand();

                ConsultaMySql.Connection = conexion;
                ConsultaMySql.CommandType = CommandType.Text;
                ConsultaMySql.CommandText = strQuery;
                DatosBanner = ConsultaMySql.ExecuteReader();
                ds.Load(DatosBanner, LoadOption.OverwriteChanges);

                MySqlDataAdapter adapter = new MySqlDataAdapter();
                DataSet ds1 = new DataSet();
                MySqlCommand command = new MySqlCommand(strQuery, conexion);
                adapter.SelectCommand = command;
                adapter.Fill(ds1);
                adapter.Dispose();
                command.Dispose();

                if (ds1.Tables[0].Rows.Count > 0)
                {
                    DataSet ds2 = new DataSet();
                    MySqlCommand command2 = new MySqlCommand(strQueryCargos, conexion);
                    adapter.SelectCommand = command2;
                    adapter.Fill(ds2);
                    adapter.Dispose();
                    command.Dispose();
                    Label2.Text = Convert.ToDouble(ds2.Tables[0].Rows[0][0].ToString()).ToString("#,##.00");

                    DataSet ds3 = new DataSet();
                    MySqlCommand command3 = new MySqlCommand(strQueryPagos, conexion);
                    adapter.SelectCommand = command3;
                    adapter.Fill(ds3);
                    adapter.Dispose();
                    command.Dispose();
                    if (ds3.Tables[0].Rows[0][0].ToString() != "")
                    {
                        Label3.Text = Convert.ToDouble(ds3.Tables[0].Rows[0][0].ToString()).ToString("#,##.00");
                    }
                    else
                    {
                        Label3.Text = "0.00";
                    }

                    DataSet ds4 = new DataSet();
                    MySqlCommand command4 = new MySqlCommand(strQueryBecas, conexion);
                    adapter.SelectCommand = command4;
                    adapter.Fill(ds4);
                    adapter.Dispose();
                    command.Dispose();
                    if (ds4.Tables[0].Rows[0][0].ToString() != "")
                    {
                        Label4.Text = Convert.ToDouble(ds4.Tables[0].Rows[0][0].ToString()).ToString("#,##.00");
                    }
                    else
                    {
                        Label4.Text = "0.00";
                    }

                    DataSet ds5 = new DataSet();
                    MySqlCommand command5 = new MySqlCommand(strQueryCancelaciones, conexion);
                    adapter.SelectCommand = command5;
                    adapter.Fill(ds5);
                    adapter.Dispose();
                    command.Dispose();
                    if (ds5.Tables[0].Rows[0][0].ToString() != "")
                    {
                        Label16.Text = Convert.ToDouble(ds5.Tables[0].Rows[0][0].ToString()).ToString("#,##.00");
                    }
                    else
                    {
                        Label16.Text = "0.00";
                    }

                    double total_cargos = 0, total_pagos = 0, total_becas = 0, total_cancelaciones = 0;

                    total_cargos = Convert.ToDouble(ds2.Tables[0].Rows[0][0].ToString());
                    if (ds3.Tables[0].Rows[0][0].ToString() != "")
                    {
                        total_pagos = Convert.ToDouble(ds3.Tables[0].Rows[0][0].ToString());
                    }
                    else
                    {
                        total_pagos = 0;
                    }
                    if (ds4.Tables[0].Rows[0][0].ToString() != "")
                    {
                        total_becas = Convert.ToDouble(ds4.Tables[0].Rows[0][0].ToString());
                    }
                    else
                    {
                        total_becas = 0;
                    }
                    if (ds5.Tables[0].Rows[0][0].ToString() != "")
                    {
                        total_cancelaciones = Convert.ToDouble(ds5.Tables[0].Rows[0][0].ToString());
                    }
                    else
                    {
                        total_cancelaciones = 0;
                    }
                    Label5.Text = (total_cargos - (total_pagos + total_becas + total_cancelaciones)).ToString("#,##.00");

                    Label6.Text = Global.nombre_programa;

                    DateTime dt = DateTime.Now;

                    conexion.Close();

                    double saldo = 0;
                    double importe = 0;
                    double amount = 0, beca = 0, suma_pagos = 0;
                    if (ds.Rows.Count > 0)
                    {
                        string concepto = "";
                        for (int z = 0; z < ds.Rows.Count; z++)
                        //foreach (var key in grouped1.Distinct())
                        {
                            StringBuilder html = new StringBuilder();
                            if (ds1.Tables[0].Rows[z][7].ToString() == "C")
                            {
                                amount = Convert.ToDouble(ds1.Tables[0].Rows[z][3].ToString());
                                saldo = amount;
                                if (suma_pagos > 0)
                                {
                                    html.Append("<table border='0'  align='center' >");
                                    html.Append("<tr>" +
                                            "<th width='120'></th>" +
                                            "<th width='300'></th>" +
                                            "<th width='90'>Suma Pagos</th>" +
                                            "<th width='82' align='right' >" + suma_pagos.ToString("#,##.00") + "</th>" +
                                            "<th width='98'></th>" +
                                            "<th width='90'></th>" +
                                            "</tr>");
                                    suma_pagos = 0;
                                }
                                if (amount >= 0)
                                {
                                    html.Append("</br>");
                                    html.Append("<table border='0'   align='center' >");
                                    html.Append("<tr style='background-color:#3c9ab6; color: White;'>" +

                                        "<th width='120'>" + ds1.Tables[0].Rows[z][1].ToString() + "</th>" +
                                        "<th width='300'  align='left'>" + ds1.Tables[0].Rows[z][2].ToString() + "</th>" +
                                        "<th width='90' align='right'>" + "$ " + Convert.ToDouble(ds1.Tables[0].Rows[z][3].ToString()).ToString("#,##.00") + "</th>" +
                                        "<th width='82'></th>" +
                                        "<th width='98' align='right'>" + ds1.Tables[0].Rows[z][12].ToString() + "</th>" +
                                        "<th width='90' align='right'>" + "$ " + Convert.ToDouble(ds1.Tables[0].Rows[z][6].ToString()).ToString("#,##.00") + "</th>" +
                                        "</tr>");
                                }
                            }
                            if (ds1.Tables[0].Rows[z][7].ToString() == "P")
                            {
                                if (ds1.Tables[0].Rows[z][2].ToString() == "" || ds1.Tables[0].Rows[z][2].ToString() == null)
                                {
                                    concepto = ds1.Tables[0].Rows[z][11].ToString();
                                }
                                else
                                {
                                    concepto = ds1.Tables[0].Rows[z][2].ToString();
                                }
                                importe = Convert.ToDouble(ds1.Tables[0].Rows[z][4].ToString());
                                saldo = saldo - importe;
                                html.Append("<table border='0' align='center' >");
                                html.Append("<tr>" +
                                        "<th width='120'></th>" +
                                        "<th width='300' align='left'>" + concepto + "</th>" +
                                        "<th width='90'></th>" +
                                        "<th width='82' align='right' >" + Convert.ToDouble(ds1.Tables[0].Rows[z][4]).ToString("#,##.00") + "</th>" +
                                        "<th width='98' align='right'>" + ds1.Tables[0].Rows[z][5].ToString() + "</th>" +
                                        "<th width='90' align='right'>" + saldo.ToString("#,##.00") + "</th>" +
                                        "</tr>");
                                suma_pagos = suma_pagos + importe;
                            }
                            if (ds1.Tables[0].Rows[z][8].ToString() == "D")
                                if (z == (ds.Rows.Count) - 1)
                                {
                                    html.Append("<table border='0'  align='center'>");
                                    html.Append("<tr>" +
                                            "<th width='120'></th>" +
                                            "<th width='240'></th>" +
                                            "<th width='90'></th>" +
                                            "<th width='82'></th>" +
                                            "<th width='98' align='right' >" + suma_pagos.ToString("#,##.00") + "</th>" +
                                            "<th width='90'></th>" +
                                            "</tr>");
                                }
                            LblNombre.Text = Global.nombre_alumno;
                            TxtMatricula.Text = userId; // columnValue["MATRICULA"].ToString();


                            html.Append("</table>");
                            html.Append("</body>");
                            html.Append("</html>");

                            PlaceHolder1.Controls.Add(new Literal { Text = html.ToString() });
                            reporte = 1;
                        }
                        StringBuilder html1 = new StringBuilder();
                        if (suma_pagos > 0)
                        {
                            html1.Append("<table border='0'  align='center' >");
                            html1.Append("<tr>" +
                                    "<th width='120'></th>" +
                                    "<th width='300'></th>" +
                                    "<th width='90'>Suma Pagos</th>" +
                                    "<th width='82' align='right' >" + suma_pagos.ToString("#,##.00") + "</th>" +
                                    "<th width='98'></th>" +
                                    "<th width='90'></th>" +
                                    "</tr>");
                            html1.Append("</table>");
                            html1.Append("</body>");
                            html1.Append("</html>");
                            PlaceHolder1.Controls.Add(new Literal { Text = html1.ToString() });
                            suma_pagos = 0;

                        }
                        // Lblerror.Text = "suma pagos:" + suma_pagos;

                    }
                }
                else
                {
                    Lblerror.Visible = true;
                    Lblerror.Text = "No existen datos para mostrar !"; // "No existe Matrícula";
                }
            }
            catch (Exception ex)
            {
                Lblerror.Text = ex.Message;
            }

        }

        protected void Referencia_Click(object sender, EventArgs e)
        {

            string userId = Global.cuenta;  //"U99201091"; // "U99204302"; //
            string nombre = Global.nombre_alumno;
            string programa = Global.programa;
            string nombre_programa = Global.nombre_programa;
            string strQuery = "";
            string strQueryCargos = "";
            string strQueryPagos = "";
            string strQueryBecas = "";
            transaccion = 0;
            UpdatePanel1.Visible = false;
            LblNombre.Text = "";
            Label6.Text = "";
            Lblerror.Visible = false;


            strQuery =
                @" select tedcu_consec transaccion , tedcu_tcoco_clave codigo, tcoco_desc descrip, tedcu_balance balance, date_format(tedcu_fecha_venc,'%d %b %Y') vencim " +
                 " from tedcu, tcoco" +
                 " where tedcu_tpers_num in (select tpers_num from tpers where tpers_id='" + userId + "')" +
                 " and tcoco_clave=tedcu_tcoco_clave and tcoco_tipo='C' and tedcu_balance > 0 " +
                 " union" +
                 " select 0 transaccion , '' codigo, 'Todos Adeudos' descrip, sum(tedcu_balance) balance, " +
                 " null vencim " +
                 " from tedcu, tcoco " +
                 " where tedcu_tpers_num in (select tpers_num from tpers where tpers_id = '" + userId + "')" +
                 " and tcoco_clave = tedcu_tcoco_clave and tcoco_tipo = 'C' and tedcu_balance > 0 " +
                 " order by transaccion ";
            //Lblerror.Text = strQuery;

            strQueryCargos =
          @" select sum(tedcu_importe) from tedcu z, tpers , tcoco " +
           " where tpers_id='" + userId + "'  and z.tedcu_tpers_num=tpers_num and  tcoco_clave=z.tedcu_tcoco_clave and tcoco_tipo='C' " +
           " and tedcu_tprog_clave='" + programa + "'";

            strQueryPagos =
            @" select sum(tpago_importe) from tedcu z, tpago, tpers, tcoco " +
             " where tpers_id='" + userId + "'  and z.tedcu_tpers_num=tpers_num and tpago_tpers_num=tedcu_tpers_num " +
             " and   tpago_tedcu_consec=tedcu_consec and tpago_estatus='A' and tedcu_tprog_clave='" + programa + "'" +
             " and   tpago_tcoco_clave=tcoco_clave and tcoco_categ in ('PC','PB') ";

            strQueryBecas =
             @" select sum(tpago_importe) from tedcu z, tpago, tpers, tcoco " +
             " where tpers_id='" + userId + "'  and z.tedcu_tpers_num=tpers_num and tpago_tpers_num=tedcu_tpers_num " +
             " and   tpago_tedcu_consec=tedcu_consec and tpago_estatus='A' and tedcu_tprog_clave='" + programa + "'" +
             " and   tpago_tcoco_clave=tcoco_clave and tcoco_categ in ('DE','BE')";

            try
            {
                MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQL"].ConnectionString);
                conexion.Open();
                MySqlDataReader DatosMysql;
                DataTable ds = new DataTable();
                MySqlCommand ConsultaMySql = new MySqlCommand();
                MySqlDataAdapter adapter = new MySqlDataAdapter();

                ConsultaMySql.Connection = conexion;
                ConsultaMySql.CommandType = CommandType.Text;
                ConsultaMySql.CommandText = strQuery;
                DatosMysql = ConsultaMySql.ExecuteReader();
                ds.Load(DatosMysql, LoadOption.OverwriteChanges);


                DataSet ds1 = new DataSet();
                MySqlCommand command = new MySqlCommand(strQuery, conexion);
                adapter.SelectCommand = command;
                adapter.Fill(ds1);
                adapter.Dispose();
                command.Dispose();



                /* DataSet ds2 = new DataSet();
                 MySqlCommand command2 = new MySqlCommand(strQueryCargos, conexion);
                 adapter.SelectCommand = command2;
                 adapter.Fill(ds2);
                 adapter.Dispose();
                 command.Dispose();

                 double total_cargos = 0, total_pagos = 0, total_becas = 0;

                 if (ds2.Tables[0].Rows.Count > 0)
                 {
                     Label2.Text = Convert.ToDouble(ds2.Tables[0].Rows[0][0].ToString()).ToString("#,##.00");
                     total_cargos = Convert.ToDouble(ds2.Tables[0].Rows[0][0].ToString());
                 }

                 DataSet ds3 = new DataSet();
                 MySqlCommand command3 = new MySqlCommand(strQueryPagos, conexion);
                 adapter.SelectCommand = command3;
                 adapter.Fill(ds3);
                 adapter.Dispose();
                 command.Dispose();
                 if (ds3.Tables[0].Rows.Count > 0)
                 {
                     Label3.Text = Convert.ToDouble(ds3.Tables[0].Rows[0][0].ToString()).ToString("#,##.00");
                     total_pagos = Convert.ToDouble(ds3.Tables[0].Rows[0][0].ToString());
                 }
                 DataSet ds4 = new DataSet();
                 MySqlCommand command4 = new MySqlCommand(strQueryBecas, conexion);
                 adapter.SelectCommand = command4;
                 adapter.Fill(ds4);
                 adapter.Dispose();
                 command.Dispose();
                 if (ds4.Tables[0].Rows.Count > 0)
                 {
                     Label4.Text = Convert.ToDouble(ds4.Tables[0].Rows[0][0].ToString()).ToString("#,##.00");
                     total_becas = Convert.ToDouble(ds4.Tables[0].Rows[0][0].ToString());
                 }
                 */
                //Label5.Text = (total_cargos - (total_pagos + total_becas)).ToString("#,##.00");

                Label6.Text = Global.programa.ToString();

                DateTime dt = DateTime.Now;

                LblNombre.Text = Global.nombre_programa.ToString();
                MySqlDataAdapter adapter10 = new MySqlDataAdapter(strQuery, conexion);
                DataSet dsgrupos = new DataSet();
                adapter10.Fill(dsgrupos, "Conceptos");
                GridViewConceptos.DataSource = dsgrupos;
                GridViewConceptos.DataBind();
                GridViewConceptos.DataMember = "Conceptos";
                GridViewConceptos.Visible = true;


                if (ds1.Tables[0].Rows.Count > 0)
                {
                    reporte = 2;
                    Lblerror.Text = "--- Selecciona un concepto a la vez para descargar la referencia bancaria ---";
                    Lblerror.Visible = true;
                }
                else
                {
                    Lblerror.Text = "No existen saldos pendientes por pagar";
                    Lblerror.Visible = true;
                    ClientScript.RegisterStartupScript(this.GetType(), "", "datos();", true);
                    //ClientScript.RegisterStartupScript(this.GetType(), "alerta", "<script>swal('No existen saldos pendientes por pagar', 'warning')</script>");
                    //LblNombre.Text = "No existe Matrícula  -- <Enter> Para continuar";
                    //Console.Read();
                    //Response.Redirect("Estado-CuentaADM.aspx");
                }
            }
            catch (Exception ex)

            {
                Lblerror.Visible = true;
                Lblerror.Text = Lblerror.Text + "---" + ex.Message;
            }


            // Lblerror.Text = "Funcionalidad NO disponible ... se encuentra en mantenimiento";
            // Lblerror.Visible = true;
        }

        [WebMethod(), ScriptMethod()]
        public static string GetDynamicContent(string contextKey)
        {
            return default(string);
        }

        protected void Busqueda_Click(object sender, EventArgs e)
        {

        }

        protected void Alumno(object sender, EventArgs e)
        {

        }

        protected void CmdLogin_Click1(object sender, EventArgs e)
        {
            if (reporte == 1)
            {
                if (TxtMatricula.Text != "")
                {
                    string userId = Global.cuenta;  //"U99201091"; // "U99204302"; //
                    string nombre = Global.nombre_alumno;
                    string programa = Global.programa;
                    string nombre_programa = Global.nombre_programa;
                    Label2.Text = "--";
                    Label3.Text = "--";
                    Label4.Text = "--";
                    Label5.Text = "--";
                    if (IsPostBack == true)
                    {

                        string _open = "window.open('TedcuPDF.aspx', '_self');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), _open, true);

                    }
                }
            }
            if (reporte == 2)
            {
                string strQueryTransaccion = "";

                //for (int i=0; i<GridViewConceptos.Rows.Count;i++)
                //{

                //CheckBox ch3 = (CheckBox)GridViewConceptos.Rows[i].FindControl("chkCtrl");
                //Lblerror.Text = Lblerror.Text + "buscando :" + ch3.Checked + "registro:" + i;
                if (transaccion != 0)
                {
                    //double tr= Convert.ToDouble(GridViewConceptos.Rows[i].Cells[1].Text.ToString());
                    strQueryTransaccion =
                    @"SELECT tedcu_tcoco_clave codigo, concat(tpers_id,tedcu_tpees_clave,LPAD(tedcu_consec, 3, '0')) banorte, " +
                     " concat(tpers_id,tedcu_tpees_clave,LPAD(tedcu_consec, 3, '0')) bancomer, " +
                     " concat(tpers_id,tedcu_tpees_clave,LPAD(tedcu_consec, 3, '0')) santander, tedcu_tpers_num pidm , tedcu_tpees_clave periodo  " +
                     " FROM tedcu, tpers " +
                     " WHERE tedcu_tpers_num = tpers_num  AND tedcu_tpers_num in (select tpers_num from tpers where tpers_id='" + TxtMatricula.Text + "')" +
                     " AND tedcu_consec =" + transaccion;

                    MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQL"].ConnectionString);
                    conexion.Open();
                    MySqlDataAdapter adapter10 = new MySqlDataAdapter(strQueryTransaccion, conexion);
                    DataSet dsgrupos = new DataSet();
                    adapter10.Fill(dsgrupos, "Conceptos");
                    GridViewRef.DataSource = dsgrupos;
                    GridViewRef.DataBind();
                    GridViewRef.DataMember = "Conceptos";


                    string banorte = GridViewRef.Rows[0].Cells[1].Text.ToString();
                    string bancomer = GridViewRef.Rows[0].Cells[2].Text.ToString();
                    string santander = GridViewRef.Rows[0].Cells[3].Text.ToString();
                    string pidm = GridViewRef.Rows[0].Cells[4].Text.ToString();
                    string periodo = GridViewRef.Rows[0].Cells[5].Text.ToString();
                    conexion.Close();

                    Response.Redirect("Conekta/Ref_bancarias.ASPX?matricula=" + TxtMatricula.Text + "&numero=" + transaccion + "&banorte=" + banorte + "&bancomer=" + bancomer + "&santander=" + santander);
                    //string _open = "window.open('Estado-CuentaPDFADM.aspx', '_self');";
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), _open, true);                    //Lblerror.Text = Lblerror.Text +  strQueryTransaccion;
                    //Label4.Text = Label4.Text + GridView2.Rows[i].Cells[1].Text.ToString() + " " + GridView2.Rows[i].Cells[2].Text.ToString();

                }
                //}
            }

        }


        protected void Cerrar_Click(object sender, EventArgs e)
        {
            Session.Clear();
            if (Global.opcion == 1)
            {
                Response.Redirect("Tedcu.aspx");
            }
            if (Global.opcion == 2)
            {
                Response.Redirect("Tepcb.aspx");
            }
        }

        protected void GridConceptos_Click(object sender, EventArgs e)
        {
            GridViewRow row = GridViewConceptos.SelectedRow;

            transaccion = Convert.ToDouble(row.Cells[1].Text.ToString());
            string strQueryTransaccion = "";

            //double tr= Convert.ToDouble(GridViewConceptos.Rows[i].Cells[1].Text.ToString());
            if (transaccion > 0)
            {
                strQueryTransaccion =
                @"SELECT tedcu_tcoco_clave codigo, concat(tpers_id,tedcu_tpees_clave,LPAD(tedcu_consec, 3, '0')) banorte, " +
                 " concat(tpers_id,tedcu_tpees_clave,LPAD(tedcu_consec, 3, '0')) bancomer, " +
                 " concat(tpers_id,tedcu_tpees_clave,LPAD(tedcu_consec, 3, '0')) santander, tedcu_tpers_num pidm , tedcu_tpees_clave periodo  " +
                 " FROM tedcu, tpers " +
                 " WHERE tedcu_tpers_num = tpers_num  AND tedcu_tpers_num in (select tpers_num from tpers where tpers_id='" + TxtMatricula.Text + "')" +
                 " AND tedcu_consec =" + transaccion;
            }
            else
            {
                strQueryTransaccion =
                @"SELECT '' codigo, concat(tpers_id,tedcu_tpees_clave,LPAD(tedcu_consec, 3, '0')) banorte, " +
                 " concat(tpers_id,tedcu_tpees_clave,LPAD(tedcu_consec, 3, '0')) bancomer, " +
                 " concat(tpers_id,tedcu_tpees_clave,LPAD(tedcu_consec, 3, '0')) santander, tedcu_tpers_num pidm , '' periodo  " +
                 " FROM tedcu, tpers " +
                 " WHERE tedcu_tpers_num = tpers_num  AND tedcu_tpers_num in (select tpers_num from tpers where tpers_id='" + TxtMatricula.Text + "')" +
                 " AND tedcu_balance > 0 ";
            }

            MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQL"].ConnectionString);
            conexion.Open();
            try
            {
                MySqlDataAdapter adapter10 = new MySqlDataAdapter(strQueryTransaccion, conexion);
                DataSet dsgrupos = new DataSet();
                adapter10.Fill(dsgrupos, "Conceptos");
                GridViewRef.DataSource = dsgrupos;
                GridViewRef.DataBind();
                GridViewRef.DataMember = "Conceptos";


                string banorte = GridViewRef.Rows[0].Cells[1].Text.ToString();
                string bancomer = GridViewRef.Rows[0].Cells[2].Text.ToString();
                string santander = GridViewRef.Rows[0].Cells[3].Text.ToString();
                string pidm = GridViewRef.Rows[0].Cells[4].Text.ToString();
                string periodo = GridViewRef.Rows[0].Cells[5].Text.ToString();
                conexion.Close();

                Response.Redirect("Trefe.ASPX?matricula=" + TxtMatricula.Text + "&numero=" + transaccion + "&banorte=" + banorte + "&bancomer=" + bancomer + "&santander=" + santander);

            }
            catch (Exception ex)
            {
                Lblerror.Visible = true;
                Lblerror.Text = ex.Message;
            }
        }
    }
}
