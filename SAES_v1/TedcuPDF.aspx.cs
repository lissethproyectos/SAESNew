using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;

using SAES_v1;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Xml.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Net;

namespace SAES_v1
{

    public partial class TedcuPDF : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            getData();

        }


        private void getData()
        {

            string userId = Global.cuenta;  //"U99201091"; // "U99204302"; //
            string nombre = Global.nombre_alumno;
            string programa = Global.programa;
            string nombre_programa = Global.nombre_programa;



            string strQuery = "";
            strQuery =
                 @"select tedcu_consec TRANS_CARGO,tedcu_tpees_clave PERIODO_CARGO, " +
                 " tcoco_desc DESC_CONCEPTO_CARGO, tedcu_importe IMPORTE,  " +
                 " null PAGO, " +
                 " null FECHA_VENCIMIENTO_CARGO, tedcu_balance SALDO_PENDIENTE ,'C' TIPO , tpers_id IDEN, concat(tpers_nombre, ' ', tpers_paterno, ' ', tpers_materno) NOMBRE, 0 factura, '' desc_concepto, " +
                 " date_format(tedcu_fecha_venc, '%d/%m/%Y') vencimiento " +
                 " from tedcu z, tcoco, tpers " +
                 " where tpers_id='" + userId + "'" +
                 " and z.tedcu_tpers_num=tpers_num and z.tedcu_tprog_clave='" + programa + "'" +
                 " and  tcoco_clave=z.tedcu_tcoco_clave and tcoco_tipo='C' " +
                 " union " +
                 " select tedcu_consec TRANS_CARGO, tedcu_tpees_clave PERIODO_CARGO, " +
                 " (select distinct tpcbe_desc from tpcbe where tpcbe_clave=tpago_tpcbe_clave " +
                 " and (tpcbe_tcamp_clave='" + Global.campus.ToString() + "' or tpcbe_tcamp_clave='000') " +
                 " and (tpcbe_tnive_clave=tprog_tnive_clave or tpcbe_tnive_clave='000')) DESC_CONCEPTO_CARGO, " +
                 // tcoco_desc DESC_CONCEPTO_CARGO, " +
                 " 0 IMPORTE, tpago_importe PAGO, date_format(tpago_date, '%d/%m/%Y') FECHA_VENCIMIENTO_CARGO, 0 SALDO_PENDIENTE, 'P' TIPO, tpers_id IDEN, " +
                 " concat(tpers_nombre, ' ', tpers_paterno, ' ', tpers_materno) NOMBRE, tpago_factura factura, tcoco_desc desc_concepto, '' vencimiento " +
                 " from tpers, tedcu, tpago, tcoco, tprog " +
                 " where tpers_id = '" + userId + "' and tedcu_tpers_num = tpers_num and tedcu_tprog_clave='" + programa + "' and tpago_tpers_num =tedcu_tpers_num " +
                 " and tpago_tedcu_consec = tedcu_consec and tpago_estatus = 'A' and tpago_tcoco_clave = tcoco_clave " +
                 " and tprog_clave= tedcu_tprog_clave " +
                 " order by  TRANS_CARGO desc, TIPO, factura ";

            string strQueryCargos =
           @" select sum(tedcu_importe) from tedcu z, tpers , tcoco " +
            " where tpers_id='" + userId + "'  and z.tedcu_tpers_num=tpers_num and  tcoco_clave=z.tedcu_tcoco_clave and tcoco_tipo='C' " +
            " and tedcu_tprog_clave='" + programa + "'";

            string strQueryPagos =
            @" select sum(tpago_importe) from tedcu z, tpago, tpers, tcoco " +
             " where tpers_id='" + userId + "'  and z.tedcu_tpers_num=tpers_num and tpago_tpers_num=tedcu_tpers_num " +
             " and   tpago_tedcu_consec=tedcu_consec and tpago_estatus='A' and tedcu_tprog_clave='" + programa + "'" +
             " and   tpago_tcoco_clave=tcoco_clave and tcoco_categ in ('PC','PB', 'SS')";

            string strQueryBecas =
             @" select sum(tpago_importe) from tedcu z, tpago, tpers, tcoco " +
             " where tpers_id='" + userId + "'  and z.tedcu_tpers_num=tpers_num and tpago_tpers_num=tedcu_tpers_num " +
             " and   tpago_tedcu_consec=tedcu_consec and tpago_estatus='A' and tedcu_tprog_clave='" + programa + "'" +
             " and   tpago_tcoco_clave=tcoco_clave and tcoco_categ in ('DE','BE') ";

            string strQueryPrograma =
            @"";

            MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            conexion.Open();
            DataTable ds = new DataTable();
            MySqlCommand oracleMySql = new MySqlCommand();

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds1 = new DataSet();
            MySqlCommand command = new MySqlCommand(strQuery, conexion);
            adapter.SelectCommand = command;
            adapter.Fill(ds1);
            adapter.Dispose();
            command.Dispose();



            DataSet ds2 = new DataSet();
            MySqlCommand command2 = new MySqlCommand(strQueryCargos, conexion);
            adapter.SelectCommand = command2;
            adapter.Fill(ds2);
            adapter.Dispose();
            command2.Dispose();

            DataSet ds3 = new DataSet();
            MySqlCommand command3 = new MySqlCommand(strQueryPagos, conexion);
            adapter.SelectCommand = command3;
            adapter.Fill(ds3);
            adapter.Dispose();
            command3.Dispose();

            DataSet ds4 = new DataSet();
            MySqlCommand command4 = new MySqlCommand(strQueryBecas, conexion);
            adapter.SelectCommand = command4;
            adapter.Fill(ds4);
            adapter.Dispose();
            command4.Dispose();


            Document doc = new Document(PageSize.A4.Rotate(), 0f, 0f, 25f, 25f);

            string path = Server.MapPath("PDF");
            string strLogoPath = Server.MapPath("Images/Sitemaster") + "//Logo_3.png";
            //string strLogoPath = Server.MapPath("Images") + "//logo.png";

            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(path + "\\Edo-Cuenta-" + userId + ".pdf", FileMode.Create));
            doc.AddTitle("Estado-Cuenta");
            doc.AddAuthor("Universidad Latinoamericana");

            doc.Open();
            writer.PageEvent = new Footer();


            //Tabla de logo table

            Rectangle page = doc.PageSize.Rotate();
            PdfPTable table = new PdfPTable(2);
            table.WidthPercentage = 100;
            table.HorizontalAlignment = Element.ALIGN_RIGHT;
            PdfPCell cell = new PdfPCell();
            cell.Colspan = 4;
            cell.Border = 0;
            iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(strLogoPath);
            AddImageInCell(cell, image, 200f, 85f, 0);
            table.AddCell(cell);



            // TABLA TITULO------------------------------------tblTitulo---------------------

            conexion.Close();

            try
            {

                conexion.Open();
                MySqlCommand MySql = new MySqlCommand();
                MySql.Connection = conexion;
                MySql.CommandType = CommandType.Text;
                MySql.CommandText = strQuery;
                MySqlDataReader DataReader = MySql.ExecuteReader();
                ds.Load((IDataReader)DataReader, LoadOption.OverwriteChanges);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexion.Close();
            }

            //Tabla de titulo tblTitulo
            PdfPTable tblTitulo = new PdfPTable(1);

            tblTitulo.DefaultCell.Border = Rectangle.NO_BORDER;
            tblTitulo.SpacingBefore = 0f;
            tblTitulo.TotalWidth = 800;
            tblTitulo.WidthPercentage = 100;
            tblTitulo.LockedWidth = true;

            Font fonttblTitulo = new Font(Font.FontFamily.TIMES_ROMAN, 12, Font.BOLD);
            Font fonttCampus = new Font(Font.FontFamily.COURIER, 10, Font.BOLD);
            Font fonttNivel = new Font(Font.FontFamily.COURIER, 9, Font.NORMAL);
            Font fonttHistoria = new Font(Font.FontFamily.HELVETICA, 11, Font.UNDERLINE);

            PdfPCell celltblTitulo = new PdfPCell(new Paragraph("UNIVERSIDAD LATINOAMERICANA", fonttblTitulo));
            celltblTitulo.Border = 0;
            celltblTitulo.BackgroundColor = (BaseColor.WHITE);
            celltblTitulo.HorizontalAlignment = Element.ALIGN_CENTER;
            tblTitulo.AddCell(celltblTitulo);

            PdfPCell cellCampus = new PdfPCell(new Paragraph("ESTADO DE CUENTA", fonttCampus));
            cellCampus.Border = 0;
            cellCampus.BackgroundColor = (BaseColor.WHITE);
            cellCampus.HorizontalAlignment = Element.ALIGN_CENTER;
            tblTitulo.AddCell(cellCampus);


            // TABLA TITULO------------------------------------tblalumno---------------------


            PdfPTable tblalumno = new PdfPTable(4);

            tblalumno.DefaultCell.Border = Rectangle.NO_BORDER;
            tblalumno.SpacingBefore = 0f;
            tblalumno.TotalWidth = 800;
            tblalumno.WidthPercentage = 100;
            tblalumno.LockedWidth = true;

            Font fonttblalumno = new Font(Font.FontFamily.HELVETICA, 9, Font.NORMAL);
            Font fonttblalumnoDB = new Font(Font.FontFamily.HELVETICA, 9, Font.BOLD);

            PdfPCell celltblalumno = new PdfPCell(new Paragraph("NOMBRE DEL ALUMNO:", fonttblalumno));
            celltblalumno.Border = 0;
            celltblalumno.BackgroundColor = (BaseColor.WHITE);
            celltblalumno.HorizontalAlignment = Element.ALIGN_MIDDLE;
            tblalumno.AddCell(celltblalumno);

            PdfPCell celltblalumnoBD = new PdfPCell(new Paragraph(nombre, fonttblalumnoDB));
            celltblalumnoBD.Border = 0;
            celltblalumnoBD.BackgroundColor = (BaseColor.WHITE);
            celltblalumnoBD.HorizontalAlignment = Element.ALIGN_MIDDLE;
            tblalumno.AddCell(celltblalumnoBD);

            PdfPCell celltblalumno1 = new PdfPCell(new Paragraph("", fonttblalumno));
            celltblalumno1.Border = 0;
            celltblalumno1.BackgroundColor = (BaseColor.WHITE);
            celltblalumno1.HorizontalAlignment = Element.ALIGN_LEFT;
            tblalumno.AddCell(celltblalumno1);

            PdfPCell celltblalumnoBD1 = new PdfPCell(new Paragraph("", fonttblalumnoDB));
            celltblalumnoBD1.Border = 0;
            celltblalumnoBD1.BackgroundColor = (BaseColor.WHITE);
            celltblalumnoBD1.HorizontalAlignment = Element.ALIGN_MIDDLE;
            tblalumno.AddCell(celltblalumnoBD1);


            PdfPTable tblalumno1 = new PdfPTable(4);

            tblalumno1.DefaultCell.Border = Rectangle.NO_BORDER;
            tblalumno1.SpacingBefore = 0f;
            tblalumno1.TotalWidth = 800;
            tblalumno1.WidthPercentage = 100;
            tblalumno1.LockedWidth = true;

            Font fonttblalumno1 = new Font(Font.FontFamily.HELVETICA, 9, Font.NORMAL);
            Font fonttblalumno1DB = new Font(Font.FontFamily.HELVETICA, 9, Font.BOLD);

            PdfPCell celltblalumno2 = new PdfPCell(new Paragraph("MATRICULA:", fonttblalumno1));
            celltblalumno2.Border = 0;
            celltblalumno2.BackgroundColor = (BaseColor.WHITE);
            celltblalumno2.HorizontalAlignment = Element.ALIGN_LEFT;
            tblalumno1.AddCell(celltblalumno2);

            PdfPCell celltblalumnoBD2 = new PdfPCell(new Paragraph(userId, fonttblalumno1DB));
            celltblalumnoBD2.Border = 0;
            celltblalumnoBD2.BackgroundColor = (BaseColor.WHITE);
            celltblalumnoBD2.HorizontalAlignment = Element.ALIGN_LEFT;
            tblalumno1.AddCell(celltblalumnoBD2);

            PdfPCell celltblalumno3 = new PdfPCell(new Paragraph("FECHA DE CONSULTA:", fonttblalumno1));
            celltblalumno3.Border = 0;
            celltblalumno3.BackgroundColor = (BaseColor.WHITE);
            celltblalumno3.HorizontalAlignment = Element.ALIGN_RIGHT;
            tblalumno1.AddCell(celltblalumno3);

            DateTime dt = DateTime.Now;
            LblFecha.Text = dt.ToString("dd/MM/yyyy");

            PdfPCell celltblalumnoBD3 = new PdfPCell(new Paragraph(LblFecha.Text, fonttblalumno1DB));
            celltblalumnoBD3.Border = 0;
            celltblalumnoBD3.BackgroundColor = (BaseColor.WHITE);
            celltblalumnoBD3.HorizontalAlignment = Element.ALIGN_LEFT;
            tblalumno1.AddCell(celltblalumnoBD3);

            PdfPTable tblalumno2 = new PdfPTable(4);

            tblalumno2.DefaultCell.Border = Rectangle.NO_BORDER;
            tblalumno2.SpacingBefore = 0f;
            tblalumno2.TotalWidth = 800;
            tblalumno2.WidthPercentage = 100;
            tblalumno2.LockedWidth = true;

            PdfPCell celltblalumno20 = new PdfPCell(new Paragraph("PROGRAMA ACADÉMICO:", fonttblalumno));
            celltblalumno20.Border = 0;
            celltblalumno20.BackgroundColor = (BaseColor.WHITE);
            celltblalumno20.HorizontalAlignment = Element.ALIGN_MIDDLE;
            tblalumno2.AddCell(celltblalumno20);

            PdfPCell celltblalumnoBD20 = new PdfPCell(new Paragraph(programa + " " + nombre_programa, fonttblalumnoDB));
            celltblalumnoBD20.Border = 0;
            celltblalumnoBD20.BackgroundColor = (BaseColor.WHITE);
            celltblalumnoBD20.HorizontalAlignment = Element.ALIGN_MIDDLE;
            tblalumno2.AddCell(celltblalumnoBD20);

            PdfPCell celltblalumno10 = new PdfPCell(new Paragraph("", fonttblalumno));
            celltblalumno10.Border = 0;
            celltblalumno10.BackgroundColor = (BaseColor.WHITE);
            celltblalumno10.HorizontalAlignment = Element.ALIGN_LEFT;
            tblalumno2.AddCell(celltblalumno10);

            PdfPCell celltblalumnoBD10 = new PdfPCell(new Paragraph("", fonttblalumnoDB));
            celltblalumnoBD10.Border = 0;
            celltblalumnoBD10.BackgroundColor = (BaseColor.WHITE);
            celltblalumnoBD10.HorizontalAlignment = Element.ALIGN_MIDDLE;
            tblalumno2.AddCell(celltblalumnoBD10);


            //tabla encabezado 1

            Font fontttblaencabezadouno = new Font(Font.FontFamily.COURIER, 9, Font.BOLD, BaseColor.WHITE);
            PdfPTable tblaencabezadocero = new PdfPTable(1);

            tblaencabezadocero.DefaultCell.Border = Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;
            tblaencabezadocero.SpacingBefore = 0f;
            tblaencabezadocero.TotalWidth = 700;
            tblaencabezadocero.WidthPercentage = 100;
            tblaencabezadocero.LockedWidth = true;
            float[] widthscero = new float[] { 400f };
            tblaencabezadocero.SetWidths(widthscero);

            //0
            PdfPCell cellcero = new PdfPCell(new Paragraph("DETALLE DE MOVIMIENTOS", fontttblaencabezadouno));
            cellcero.Border = 0;
            cellcero.BackgroundColor = (BaseColor.DARK_GRAY);
            cellcero.HorizontalAlignment = Element.ALIGN_CENTER;
            tblaencabezadocero.AddCell(cellcero);

            PdfPTable tblaencabezadouno = new PdfPTable(6);

            tblaencabezadouno.DefaultCell.Border = Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;
            tblaencabezadouno.SpacingBefore = 0f;
            tblaencabezadouno.TotalWidth = 700;
            tblaencabezadouno.WidthPercentage = 100;
            tblaencabezadouno.LockedWidth = true;
            float[] widths = new float[] { 80f, 150f, 80f, 80f, 90f, 80f };
            tblaencabezadouno.SetWidths(widths);


            //1
            PdfPCell cellclave = new PdfPCell(new Paragraph("Periodo", fontttblaencabezadouno));
            cellclave.Border = 0;
            cellclave.BackgroundColor = (BaseColor.DARK_GRAY);
            cellclave.HorizontalAlignment = Element.ALIGN_LEFT;
            tblaencabezadouno.AddCell(cellclave);

            //2
            PdfPCell cellperiodo = new PdfPCell(new Paragraph("Concepto", fontttblaencabezadouno));
            cellperiodo.Border = 0;
            cellperiodo.BackgroundColor = (BaseColor.DARK_GRAY);
            cellperiodo.HorizontalAlignment = Element.ALIGN_LEFT;
            tblaencabezadouno.AddCell(cellperiodo);

            //3
            PdfPCell cellcargo = new PdfPCell(new Paragraph("Costo", fontttblaencabezadouno));
            cellcargo.Border = 0;
            cellcargo.BackgroundColor = (BaseColor.DARK_GRAY);
            cellcargo.HorizontalAlignment = Element.ALIGN_LEFT;
            tblaencabezadouno.AddCell(cellcargo);

            //4
            PdfPCell cellxpagar = new PdfPCell(new Paragraph("Pagos Realizados", fontttblaencabezadouno));
            cellxpagar.Border = 0;
            cellxpagar.BackgroundColor = (BaseColor.DARK_GRAY);
            cellxpagar.HorizontalAlignment = Element.ALIGN_LEFT;
            tblaencabezadouno.AddCell(cellxpagar);

            //5
            PdfPCell cellsaldo = new PdfPCell(new Paragraph("Fecha Venc./Pago", fontttblaencabezadouno));
            cellsaldo.Border = 0;
            cellsaldo.BackgroundColor = (BaseColor.DARK_GRAY);
            cellsaldo.HorizontalAlignment = Element.ALIGN_LEFT;
            tblaencabezadouno.AddCell(cellsaldo);

            //6
            PdfPCell cellimporte = new PdfPCell(new Paragraph("Saldo", fontttblaencabezadouno));
            cellimporte.Border = 0;
            cellimporte.BackgroundColor = (BaseColor.DARK_GRAY);
            cellimporte.HorizontalAlignment = Element.ALIGN_LEFT;
            tblaencabezadouno.AddCell(cellimporte);

            //-----------------------------------datatable------------------------------------------------------------------------


            PdfPTable datatable = new PdfPTable(6);

            datatable.DefaultCell.Border = Rectangle.NO_BORDER;
            datatable.SpacingBefore = 15f;
            datatable.TotalWidth = 700;
            datatable.WidthPercentage = 100;
            datatable.LockedWidth = true;
            float[] widths1 = new float[] { 80f, 150f, 80f, 80f, 90f, 80f };
            datatable.SetWidths(widths1);

            Font fonttblacreditos = new Font(Font.FontFamily.HELVETICA, 9, Font.BOLDITALIC, BaseColor.BLACK);
            Font fonttblacreditos1 = new Font(Font.FontFamily.HELVETICA, 9, Font.NORMAL, BaseColor.BLACK);




            if (ds.Rows.Count > 0)
            {
                double saldo = 0;
                double importe = 0;
                double amount = 0;
                double imp = 0, pago = 0, suma_pagos = 0;
                for (int z = 0; z < ds.Rows.Count; z++)
                //foreach (var key in grouped1.Distinct())
                {

                    if (ds1.Tables[0].Rows[z][7].ToString() == "C")
                    {
                        if (suma_pagos > 0)
                        {
                            importe = Convert.ToDouble(ds1.Tables[0].Rows[z][3].ToString());
                            //1
                            PdfPCell PERIODO =
                                    new PdfPCell(new Paragraph(" ", fonttblacreditos1));
                            PERIODO.Border = 0;
                            PERIODO.BackgroundColor = (BaseColor.WHITE);
                            PERIODO.HorizontalAlignment = Element.ALIGN_LEFT;
                            datatable.AddCell(PERIODO);

                            //2
                            PdfPCell DESC_CONCEPTO_CARGO = new PdfPCell(new Paragraph(" ", fonttblacreditos1));
                            DESC_CONCEPTO_CARGO.Border = 0;
                            DESC_CONCEPTO_CARGO.BackgroundColor = (BaseColor.WHITE);
                            DESC_CONCEPTO_CARGO.HorizontalAlignment = Element.ALIGN_LEFT;
                            datatable.AddCell(DESC_CONCEPTO_CARGO);

                            //3
                            PdfPCell XPAGAR =
                                new PdfPCell(new Paragraph("Suma Pagos", fonttblacreditos1));
                            XPAGAR.Border = 0;
                            XPAGAR.BackgroundColor = (BaseColor.WHITE);
                            XPAGAR.HorizontalAlignment = Element.ALIGN_RIGHT;
                            datatable.AddCell(XPAGAR);

                            //4
                            //pago = Convert.ToDouble(ds1.Tables[0].Rows[z][3].ToString());
                            PdfPCell PAGOS = new PdfPCell(new Paragraph("$ " + suma_pagos.ToString("#,##.00"), fonttblacreditos1));
                            PAGOS.Border = 0;
                            PAGOS.BackgroundColor = (BaseColor.WHITE);
                            PAGOS.HorizontalAlignment = Element.ALIGN_RIGHT;
                            datatable.AddCell(PAGOS);

                            //5
                            PdfPCell FECHA_PAGO = new PdfPCell(new Paragraph(" ", fonttblacreditos1));
                            FECHA_PAGO.Border = 0;
                            FECHA_PAGO.BackgroundColor = (BaseColor.WHITE);
                            FECHA_PAGO.HorizontalAlignment = Element.ALIGN_RIGHT;
                            datatable.AddCell(FECHA_PAGO);

                            //6
                            PdfPCell SALDO =
                                new PdfPCell(new Paragraph(" ", fonttblacreditos1));
                            SALDO.Border = 0;
                            SALDO.BackgroundColor = (BaseColor.WHITE);
                            SALDO.HorizontalAlignment = Element.ALIGN_RIGHT;
                            datatable.AddCell(SALDO);

                            suma_pagos = 0;
                        }

                        amount = Convert.ToDouble(ds1.Tables[0].Rows[z][3].ToString());
                        if (amount >= 0)
                        {
                            //1
                            PdfPCell PERIODO =
                                new PdfPCell(new Paragraph(ds1.Tables[0].Rows[z][1].ToString(), fonttblacreditos));
                            PERIODO.Border = 0;
                            PERIODO.BackgroundColor = (BaseColor.LIGHT_GRAY);
                            PERIODO.HorizontalAlignment = Element.ALIGN_LEFT;
                            datatable.AddCell(PERIODO);


                            //2
                            PdfPCell DESC_CONCEPTO_CARGO = new PdfPCell(new Paragraph(ds1.Tables[0].Rows[z][2].ToString(), fonttblacreditos));
                            DESC_CONCEPTO_CARGO.Border = 0;
                            DESC_CONCEPTO_CARGO.BackgroundColor = (BaseColor.LIGHT_GRAY);
                            DESC_CONCEPTO_CARGO.HorizontalAlignment = Element.ALIGN_LEFT;
                            datatable.AddCell(DESC_CONCEPTO_CARGO);

                            //3
                            imp = Convert.ToDouble(ds1.Tables[0].Rows[z][3].ToString());
                            saldo = imp;
                            PdfPCell IMPORTE =
                                new PdfPCell(new Paragraph("$ " + imp.ToString("#,##.00"),
                                    fonttblacreditos));
                            IMPORTE.Border = 0;
                            IMPORTE.BackgroundColor = (BaseColor.LIGHT_GRAY);
                            IMPORTE.HorizontalAlignment = Element.ALIGN_RIGHT;
                            datatable.AddCell(IMPORTE);

                            //4
                            PdfPCell PAGO =
                                new PdfPCell(new Paragraph("", fonttblacreditos));
                            PAGO.Border = 0;
                            PAGO.BackgroundColor = (BaseColor.LIGHT_GRAY);
                            PAGO.HorizontalAlignment = Element.ALIGN_RIGHT;
                            datatable.AddCell(PAGO);

                            //5
                            PdfPCell FECHA_PAGO = new PdfPCell(new Paragraph(ds1.Tables[0].Rows[z][12].ToString(), fonttblacreditos));
                            FECHA_PAGO.Border = 0;
                            FECHA_PAGO.BackgroundColor = (BaseColor.LIGHT_GRAY);
                            FECHA_PAGO.HorizontalAlignment = Element.ALIGN_RIGHT;
                            datatable.AddCell(FECHA_PAGO);

                            //6
                            PdfPCell XPAGAR =
                                new PdfPCell(new Paragraph(ds1.Tables[0].Rows[z][6].ToString(), fonttblacreditos));
                            XPAGAR.Border = 0;
                            XPAGAR.BackgroundColor = (BaseColor.LIGHT_GRAY);
                            XPAGAR.HorizontalAlignment = Element.ALIGN_RIGHT;
                            datatable.AddCell(XPAGAR);

                        }
                    }
                    if (ds1.Tables[0].Rows[z][7].ToString() == "P")
                    {
                        importe = Convert.ToDouble(ds1.Tables[0].Rows[z][3].ToString());

                        PdfPCell PERIODO =
                                new PdfPCell(new Paragraph(" ", fonttblacreditos1));
                        PERIODO.Border = 0;
                        PERIODO.BackgroundColor = (BaseColor.WHITE);
                        PERIODO.HorizontalAlignment = Element.ALIGN_LEFT;
                        datatable.AddCell(PERIODO);
                        string concepto;
                        if (ds1.Tables[0].Rows[z][2].ToString() == "" || ds1.Tables[0].Rows[z][2].ToString() == null)
                        {
                            concepto = ds1.Tables[0].Rows[z][11].ToString();
                        }
                        else
                        {
                            concepto = ds1.Tables[0].Rows[z][2].ToString();
                        }

                        //2
                        PdfPCell DESC_CONCEPTO_CARGO = new PdfPCell(new Paragraph(concepto, fonttblacreditos1));
                        DESC_CONCEPTO_CARGO.Border = 0;
                        DESC_CONCEPTO_CARGO.BackgroundColor = (BaseColor.WHITE);
                        DESC_CONCEPTO_CARGO.HorizontalAlignment = Element.ALIGN_LEFT;
                        datatable.AddCell(DESC_CONCEPTO_CARGO);

                        //3
                        PdfPCell IMPORTE =
                            new PdfPCell(new Paragraph(" ", fonttblacreditos1));
                        IMPORTE.Border = 0;
                        IMPORTE.BackgroundColor = (BaseColor.WHITE);
                        IMPORTE.HorizontalAlignment = Element.ALIGN_LEFT;
                        datatable.AddCell(IMPORTE);

                        //4
                        PdfPCell PAGO =
                            new PdfPCell(new Paragraph(ds1.Tables[0].Rows[z][4].ToString(), fonttblacreditos1));
                        PAGO.Border = 0;
                        PAGO.BackgroundColor = (BaseColor.WHITE);
                        PAGO.HorizontalAlignment = Element.ALIGN_RIGHT;
                        datatable.AddCell(PAGO);
                        pago = Convert.ToDouble(ds1.Tables[0].Rows[z][4].ToString());

                        //5
                        PdfPCell FECHA_PAGO = new PdfPCell(new Paragraph(ds1.Tables[0].Rows[z][5].ToString(), fonttblacreditos1));
                        FECHA_PAGO.Border = 0;
                        FECHA_PAGO.BackgroundColor = (BaseColor.WHITE);
                        FECHA_PAGO.HorizontalAlignment = Element.ALIGN_RIGHT;
                        datatable.AddCell(FECHA_PAGO);

                        //6
                        saldo = saldo - pago;
                        suma_pagos = suma_pagos + pago;
                        PdfPCell SALDO =
                            new PdfPCell(new Paragraph("$ " + saldo.ToString("#,##.00"),
                                fonttblacreditos1));
                        SALDO.Border = 0;
                        SALDO.BackgroundColor = (BaseColor.WHITE);
                        SALDO.HorizontalAlignment = Element.ALIGN_RIGHT;
                        datatable.AddCell(SALDO);

                    }
                    if (ds1.Tables[0].Rows[z][7].ToString() == "D")
                    {
                        importe = Convert.ToDouble(ds1.Tables[0].Rows[z][3].ToString());
                        //1
                        PdfPCell PERIODO =
                                new PdfPCell(new Paragraph(" ", fonttblacreditos1));
                        PERIODO.Border = 0;
                        PERIODO.BackgroundColor = (BaseColor.WHITE);
                        PERIODO.HorizontalAlignment = Element.ALIGN_LEFT;
                        datatable.AddCell(PERIODO);

                        //2
                        PdfPCell DESC_CONCEPTO_CARGO = new PdfPCell(new Paragraph(ds1.Tables[0].Rows[z][2].ToString(), fonttblacreditos1));
                        DESC_CONCEPTO_CARGO.Border = 0;
                        DESC_CONCEPTO_CARGO.BackgroundColor = (BaseColor.WHITE);
                        DESC_CONCEPTO_CARGO.HorizontalAlignment = Element.ALIGN_LEFT;
                        datatable.AddCell(DESC_CONCEPTO_CARGO);

                        //3
                        pago = Convert.ToDouble(ds1.Tables[0].Rows[z][3].ToString());
                        PdfPCell IMPORTE = new PdfPCell(new Paragraph("$ " + pago.ToString("#,##.00"), fonttblacreditos1));
                        IMPORTE.Border = 0;
                        IMPORTE.BackgroundColor = (BaseColor.WHITE);
                        IMPORTE.HorizontalAlignment = Element.ALIGN_RIGHT;
                        datatable.AddCell(IMPORTE);

                        //4
                        PdfPCell PAGO =
                            new PdfPCell(new Paragraph(" ", fonttblacreditos1));
                        PAGO.Border = 0;
                        PAGO.BackgroundColor = (BaseColor.WHITE);
                        PAGO.HorizontalAlignment = Element.ALIGN_LEFT;
                        datatable.AddCell(PAGO);


                        //5
                        PdfPCell FECHA_PAGO = new PdfPCell(new Paragraph(ds1.Tables[0].Rows[z][5].ToString(), fonttblacreditos1));
                        FECHA_PAGO.Border = 0;
                        FECHA_PAGO.BackgroundColor = (BaseColor.WHITE);
                        FECHA_PAGO.HorizontalAlignment = Element.ALIGN_RIGHT;
                        datatable.AddCell(FECHA_PAGO);

                        //6
                        saldo = saldo - pago;
                        PdfPCell SALDO =
                            new PdfPCell(new Paragraph("$ " + ds1.Tables[0].Rows[z][6].ToString(),
                                fonttblacreditos1));
                        SALDO.Border = 0;
                        SALDO.BackgroundColor = (BaseColor.WHITE);
                        SALDO.HorizontalAlignment = Element.ALIGN_RIGHT;
                        datatable.AddCell(SALDO);

                    }

                }
                PdfPTable datatablefin = new PdfPTable(6);

                datatablefin.DefaultCell.Border = Rectangle.NO_BORDER;
                datatablefin.SpacingBefore = 0f;
                datatablefin.TotalWidth = 700;
                datatablefin.WidthPercentage = 100;
                datatablefin.LockedWidth = true;
                float[] widthsfin = new float[] { 80f, 150f, 80f, 80f, 90f, 80f };
                datatablefin.SetWidths(widthsfin);
                if (suma_pagos > 0)
                {

                    //1
                    PdfPCell PERIODO =
                            new PdfPCell(new Paragraph(" ", fonttblacreditos1));
                    PERIODO.Border = 0;
                    PERIODO.BackgroundColor = (BaseColor.WHITE);
                    PERIODO.HorizontalAlignment = Element.ALIGN_LEFT;
                    datatablefin.AddCell(PERIODO);

                    //2
                    PdfPCell DESC_CONCEPTO_CARGO = new PdfPCell(new Paragraph(" ", fonttblacreditos1));
                    DESC_CONCEPTO_CARGO.Border = 0;
                    DESC_CONCEPTO_CARGO.BackgroundColor = (BaseColor.WHITE);
                    DESC_CONCEPTO_CARGO.HorizontalAlignment = Element.ALIGN_LEFT;
                    datatablefin.AddCell(DESC_CONCEPTO_CARGO);

                    //3
                    PdfPCell XPAGAR =
                        new PdfPCell(new Paragraph("Suma Pagos", fonttblacreditos1));
                    XPAGAR.Border = 0;
                    XPAGAR.BackgroundColor = (BaseColor.WHITE);
                    XPAGAR.HorizontalAlignment = Element.ALIGN_RIGHT;
                    datatablefin.AddCell(XPAGAR);

                    //4
                    //pago = Convert.ToDouble(ds1.Tables[0].Rows[z][3].ToString());
                    PdfPCell PAGOS = new PdfPCell(new Paragraph("$ " + suma_pagos.ToString("#,##.00"), fonttblacreditos1));
                    PAGOS.Border = 0;
                    PAGOS.BackgroundColor = (BaseColor.WHITE);
                    PAGOS.HorizontalAlignment = Element.ALIGN_RIGHT;
                    datatablefin.AddCell(PAGOS);

                    //5
                    PdfPCell FECHA_PAGO = new PdfPCell(new Paragraph(" ", fonttblacreditos1));
                    FECHA_PAGO.Border = 0;
                    FECHA_PAGO.BackgroundColor = (BaseColor.WHITE);
                    FECHA_PAGO.HorizontalAlignment = Element.ALIGN_RIGHT;
                    datatablefin.AddCell(FECHA_PAGO);

                    //6
                    PdfPCell SALDO =
                        new PdfPCell(new Paragraph(" ", fonttblacreditos1));
                    SALDO.Border = 0;
                    SALDO.BackgroundColor = (BaseColor.WHITE);
                    SALDO.HorizontalAlignment = Element.ALIGN_RIGHT;
                    datatablefin.AddCell(SALDO);

                    suma_pagos = 0;
                }


                PdfPTable datatable2 = new PdfPTable(6);

                datatable2.DefaultCell.Border = Rectangle.NO_BORDER;
                datatable2.SpacingBefore = 0f;
                datatable2.TotalWidth = 700;
                datatable2.WidthPercentage = 100;
                datatable2.LockedWidth = true;
                float[] widths2 = new float[] { 80f, 150f, 80f, 80f, 90f, 80f };
                datatable2.SetWidths(widths2);

                PdfPCell PER =
                                    new PdfPCell(new Paragraph(" ", fonttblacreditos1));
                PER.Border = 0;
                PER.BackgroundColor = (BaseColor.WHITE);
                PER.HorizontalAlignment = Element.ALIGN_LEFT;
                datatable2.AddCell(PER);

                //2
                PdfPCell CONCEPTO = new PdfPCell(new Paragraph(" ", fonttblacreditos1));
                CONCEPTO.Border = 0;
                CONCEPTO.BackgroundColor = (BaseColor.WHITE);
                CONCEPTO.HorizontalAlignment = Element.ALIGN_LEFT;
                datatable2.AddCell(CONCEPTO);

                //3
                PdfPCell IMP =
                    new PdfPCell(new Paragraph(" ", fonttblacreditos1));
                IMP.Border = 0;
                IMP.BackgroundColor = (BaseColor.WHITE);
                IMP.HorizontalAlignment = Element.ALIGN_LEFT;
                datatable2.AddCell(IMP);

                //4
                PdfPCell BECAS =
                    new PdfPCell(new Paragraph(" ", fonttblacreditos1));
                BECAS.Border = 0;
                BECAS.BackgroundColor = (BaseColor.WHITE);
                BECAS.HorizontalAlignment = Element.ALIGN_LEFT;
                datatable2.AddCell(BECAS);

                //7
                PdfPCell FECHA = new PdfPCell(new Paragraph(" ", fonttblacreditos1));
                FECHA.Border = 0;
                FECHA.BackgroundColor = (BaseColor.WHITE);
                FECHA.HorizontalAlignment = Element.ALIGN_RIGHT;
                datatable2.AddCell(FECHA);

                //7
                PdfPCell SALDO1 =
                    new PdfPCell(new Paragraph(" ", fonttblacreditos1));
                SALDO1.Border = 0;
                SALDO1.BackgroundColor = (BaseColor.WHITE);
                SALDO1.HorizontalAlignment = Element.ALIGN_RIGHT;
                datatable2.AddCell(SALDO1);

                PdfPTable tablefin = new PdfPTable(4);

                tablefin.DefaultCell.Border = Rectangle.NO_BORDER;
                tablefin.SpacingBefore = 0f;
                tablefin.TotalWidth = 700;
                tablefin.WidthPercentage = 100;
                tablefin.LockedWidth = true;
                float[] widthsfin1 = new float[] { 250f, 80f, 40f, 50f };
                tablefin.SetWidths(widthsfin1);

                double total_cargos = 0, total_pagos = 0, total_becas = 0;

                if (ds2.Tables[0].Rows[0][0].ToString() != "")
                {
                    total_cargos = Convert.ToDouble(ds2.Tables[0].Rows[0][0].ToString());
                }
                else
                {
                    total_cargos = 0;
                }
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

                /*var label2 = Convert.ToDouble(ds2.Tables[0].Rows[0][0].ToString()).ToString("#,##.00");

                var label3 = Convert.ToDouble(ds3.Tables[0].Rows[0][0].ToString()).ToString("#,##.00");

                var label4 = Convert.ToDouble(ds4.Tables[0].Rows[0][0].ToString()).ToString("#,##.00");*/
                var label2 = total_cargos.ToString("#,##.00");
                var label3 = total_pagos.ToString("#,##.00");
                var label4 = total_becas.ToString("#,##.00");

                var label5 = (total_cargos - (total_pagos + total_becas)).ToString("#,##.00");

                PdfPCell cellfin = new PdfPCell();
                cellfin.Colspan = 3;
                cellfin.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                                                 //tablefin.AddCell(cellfin);
                tablefin.AddCell(" ");

                PdfPCell Encabezado = new PdfPCell(new Paragraph("Resumen Estado de cuenta", fonttblacreditos));
                Encabezado.Colspan = 2;
                Encabezado.Border = 0;
                Encabezado.BackgroundColor = (BaseColor.LIGHT_GRAY);
                Encabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablefin.AddCell(Encabezado);
                tablefin.AddCell(" ");
                tablefin.AddCell(" ");

                PdfPCell ETotalCargos = new PdfPCell(new Paragraph("Total de Cargos: " + "       $ ", fonttblacreditos));
                ETotalCargos.Border = 1;
                ETotalCargos.BackgroundColor = (BaseColor.WHITE);
                ETotalCargos.HorizontalAlignment = Element.ALIGN_RIGHT;
                tablefin.AddCell(ETotalCargos);

                PdfPCell TotalCargos = new PdfPCell(new Paragraph(Convert.ToDouble(label2).ToString("#,##.00"), fonttblacreditos));
                TotalCargos.Border = 1;
                TotalCargos.BackgroundColor = (BaseColor.WHITE);
                TotalCargos.HorizontalAlignment = Element.ALIGN_RIGHT;
                tablefin.AddCell(TotalCargos);


                //PdfPCell TotalCargos1 = new PdfPCell(new Paragraph("$ " + Convert.ToDouble(label2).ToString("#,##.00"), fonttblacreditos));
                //TotalCargos1.Border = 0;
                //TotalCargos1.BackgroundColor = (BaseColor.WHITE);
                //TotalCargos1.HorizontalAlignment = Element.ALIGN_LEFT;
                //tablefin.AddCell(TotalCargos1);


                tablefin.AddCell(" ");
                tablefin.AddCell(" ");


                PdfPCell ETotalPagos = new PdfPCell(new Paragraph("Total de Pagos: " + "        $ ", fonttblacreditos));
                ETotalPagos.Border = 1;
                ETotalPagos.BackgroundColor = (BaseColor.WHITE);
                ETotalPagos.HorizontalAlignment = Element.ALIGN_RIGHT;
                tablefin.AddCell(ETotalPagos);

                PdfPCell TotalPagos = new PdfPCell(new Paragraph(Convert.ToDouble(label3).ToString("#,##.00"), fonttblacreditos));
                TotalPagos.Border = 1;
                TotalPagos.BackgroundColor = (BaseColor.WHITE);
                TotalPagos.HorizontalAlignment = Element.ALIGN_RIGHT;
                tablefin.AddCell(TotalPagos);

                //PdfPCell TotalPagos1 = new PdfPCell(new Paragraph("$ " + Convert.ToDouble(label3).ToString("#,##.00"), fonttblacreditos));
                //TotalPagos1.Border = 0;
                //TotalPagos1.BackgroundColor = (BaseColor.WHITE);
                //TotalPagos1.HorizontalAlignment = Element.ALIGN_LEFT;
                //tablefin.AddCell(TotalPagos1);

                tablefin.AddCell(" ");
                tablefin.AddCell(" ");


                PdfPCell ETotalDescuentos = new PdfPCell(new Paragraph("Total de Descuentos:" + " $ ", fonttblacreditos));
                ETotalDescuentos.Border = 1;
                ETotalDescuentos.BackgroundColor = (BaseColor.WHITE);
                ETotalDescuentos.HorizontalAlignment = Element.ALIGN_RIGHT;
                tablefin.AddCell(ETotalDescuentos);

                PdfPCell TotalDescuentos = new PdfPCell(new Paragraph(Convert.ToDouble(label4).ToString("#,##.00"), fonttblacreditos));
                TotalDescuentos.Border = 1;
                TotalDescuentos.BackgroundColor = (BaseColor.WHITE);
                TotalDescuentos.HorizontalAlignment = Element.ALIGN_RIGHT;
                tablefin.AddCell(TotalDescuentos);

                //PdfPCell TotalDescuentos1 = new PdfPCell(new Paragraph("$ " + Convert.ToDouble(label4).ToString("#,##.00"), fonttblacreditos));
                //TotalDescuentos1.Border = 0;
                //TotalDescuentos1.BackgroundColor = (BaseColor.WHITE);
                //TotalDescuentos1.HorizontalAlignment = Element.ALIGN_LEFT;
                //tablefin.AddCell(TotalDescuentos1);


                tablefin.AddCell(" ");
                tablefin.AddCell(" ");



                PdfPCell ETotalSaldoFinal = new PdfPCell(new Paragraph("Saldo Final:" + "  $ ", fonttblacreditos));
                ETotalSaldoFinal.Border = 1;
                ETotalSaldoFinal.BackgroundColor = (BaseColor.WHITE);
                ETotalSaldoFinal.HorizontalAlignment = Element.ALIGN_RIGHT;
                tablefin.AddCell(ETotalSaldoFinal);

                PdfPCell TotalSaldoFinal = new PdfPCell(new Paragraph(Convert.ToDouble(label5).ToString("#,##.00"), fonttblacreditos));
                TotalSaldoFinal.Border = 1;
                TotalSaldoFinal.BackgroundColor = (BaseColor.WHITE);
                TotalSaldoFinal.HorizontalAlignment = Element.ALIGN_RIGHT;
                tablefin.AddCell(TotalSaldoFinal);

                //PdfPCell TotalSaldoFinal1 = new PdfPCell(new Paragraph("$ " + Convert.ToDouble(label5).ToString("#,##.00"), fonttblacreditos));
                //TotalSaldoFinal1.Border = 0;
                //TotalSaldoFinal1.BackgroundColor = (BaseColor.WHITE);
                //TotalSaldoFinal1.HorizontalAlignment = Element.ALIGN_LEFT;
                //tablefin.AddCell(TotalSaldoFinal1);

                tablefin.AddCell(" ");
                tablefin.AddCell(" ");

                PdfPTable tbdefiniciones = new PdfPTable(1);

                tbdefiniciones.DefaultCell.Border = Rectangle.NO_BORDER;
                tbdefiniciones.SpacingBefore = 15f;
                tbdefiniciones.TotalWidth = 800;
                tbdefiniciones.WidthPercentage = 100;
                tbdefiniciones.LockedWidth = true;


                PdfPCell def0 = new PdfPCell(new Paragraph("DEFINICIONES:", fonttblacreditos1));
                def0.Border = 0;
                def0.BackgroundColor = (BaseColor.WHITE);
                def0.HorizontalAlignment = Element.ALIGN_LEFT;
                tbdefiniciones.AddCell(def0);

                PdfPCell def1 = new PdfPCell(new Paragraph("Total de cargos: cantidad total a cobrar por servicios (colegiatura y/o trámites escolares).", fonttblacreditos1));
                def1.Border = 0;
                def1.BackgroundColor = (BaseColor.WHITE);
                def1.HorizontalAlignment = Element.ALIGN_LEFT;
                tbdefiniciones.AddCell(def1);

                PdfPCell def2 = new PdfPCell(new Paragraph("Total de pagos realizados: cantidad total de pagos realizados por el alumno", fonttblacreditos1));
                def2.Border = 0;
                def2.BackgroundColor = (BaseColor.WHITE);
                def2.HorizontalAlignment = Element.ALIGN_LEFT;
                tbdefiniciones.AddCell(def2);

                PdfPCell def3 = new PdfPCell(new Paragraph("Total de descuentos: cantidad total por concepto de descuentos y/o becas.", fonttblacreditos1));
                def3.Border = 0;
                def3.BackgroundColor = (BaseColor.WHITE);
                def3.HorizontalAlignment = Element.ALIGN_LEFT;
                tbdefiniciones.AddCell(def3);

                PdfPCell def4 = new PdfPCell(new Paragraph("Saldo total: cantidad en contra o a favor(-) que resulta de los movimientos realizados a la cuenta del alumno.", fonttblacreditos1));
                def4.Border = 0;
                def4.BackgroundColor = (BaseColor.WHITE);
                def4.HorizontalAlignment = Element.ALIGN_LEFT;
                tbdefiniciones.AddCell(def4);

                PdfPCell def5 = new PdfPCell(new Paragraph("Periodo: código administrativo que identifica un intervalo de tiempo.", fonttblacreditos1));
                def5.Border = 0;
                def5.BackgroundColor = (BaseColor.WHITE);
                def5.HorizontalAlignment = Element.ALIGN_LEFT;
                tbdefiniciones.AddCell(def5);

                PdfPCell def7 = new PdfPCell(new Paragraph("Concepto: descripción del servicio y/o forma de pago.", fonttblacreditos1));
                def7.Border = 0;
                def7.BackgroundColor = (BaseColor.WHITE);
                def7.HorizontalAlignment = Element.ALIGN_LEFT;
                tbdefiniciones.AddCell(def7);

                PdfPCell def8 = new PdfPCell(new Paragraph("Costo: valor del servicio.", fonttblacreditos1));
                def8.Border = 0;
                def8.BackgroundColor = (BaseColor.WHITE);
                def8.HorizontalAlignment = Element.ALIGN_LEFT;
                tbdefiniciones.AddCell(def8);


                PdfPCell def11 = new PdfPCell(new Paragraph("Pagos realizados: cantidad pagada por el alumno sobre el servicio.", fonttblacreditos1));
                def11.Border = 0;
                def11.BackgroundColor = (BaseColor.WHITE);
                def11.HorizontalAlignment = Element.ALIGN_LEFT;
                tbdefiniciones.AddCell(def11);

                PdfPCell def12 = new PdfPCell(new Paragraph("Fecha de pago: momento en el que el pago realizado se aplica en el sistema.", fonttblacreditos1));
                def12.Border = 0;
                def12.BackgroundColor = (BaseColor.WHITE);
                def12.HorizontalAlignment = Element.ALIGN_LEFT;
                tbdefiniciones.AddCell(def12);

                PdfPCell def13 = new PdfPCell(new Paragraph("Saldo: en contra o a favor(-) que resulta de los movimientos realizados al servicio.", fonttblacreditos1));
                def13.Border = 0;
                def13.BackgroundColor = (BaseColor.WHITE);
                def13.HorizontalAlignment = Element.ALIGN_LEFT;
                tbdefiniciones.AddCell(def13);

                PdfPCell def14 = new PdfPCell(new Paragraph("NOTA: Las cuotas de colegiatura y servicios están sujetos a un incremento anual", fonttblacreditos));
                def14.Border = 0;
                def14.BackgroundColor = (BaseColor.WHITE);
                def14.HorizontalAlignment = Element.ALIGN_LEFT;
                tbdefiniciones.AddCell(def14);


                if (datatable.Rows.Count >= 0)
                {
                    doc.NewPage();

                }

                int cuantos = Convert.ToInt32(datatable.Rows.Count.ToString());

                if (cuantos >= 1)
                {
                    //Tabla de logo table
                    doc.Add(table);


                    doc.Add(tblTitulo);
                    doc.Add(tblalumno);
                    doc.Add(tblalumno1);
                    doc.Add(tblalumno2);
                    doc.Add(tablefin);
                    doc.Add(tblaencabezadocero);
                    doc.Add(tblaencabezadouno);
                    doc.Add(datatable);
                    doc.Add(datatablefin);
                    doc.Add(datatable2);
                    doc.Add(tbdefiniciones);

                }
                else
                {
                    writer.PageEvent = new Footer();
                    //Tabla de logo table
                    doc.Add(table);

                    doc.Add(tblTitulo);
                    doc.Add(tblalumno);
                    doc.Add(tblalumno1);
                    doc.Add(tblalumno2);
                    doc.Add(tablefin);
                    doc.Add(tblaencabezadocero);
                    doc.Add(tblaencabezadouno);
                    doc.Add(datatable);
                    doc.Add(datatablefin);
                    doc.Add(datatable2);
                    doc.Add(tbdefiniciones);


                }

                //---------------------------------------------------MargenPage-----------------------------------------
                //PdfContentByte content = writer.DirectContent;
                //Rectangle rectangle = new Rectangle(doc.PageSize.Rotate());
                //rectangle.Left += 580;
                //rectangle.Right -= 582;
                //rectangle.Top -= 830;
                //rectangle.Bottom += 820;
                //content.SetColorStroke(BaseColor.BLACK);
                //content.Rectangle(rectangle.Left, rectangle.Bottom, rectangle.Width, rectangle.Height);
                //content.Stroke();

                doc.Close();

                byte[] bytes = File.ReadAllBytes(path + "/Edo-Cuenta-" + userId + ".pdf");
                Font blackFont = FontFactory.GetFont("Arial", 8, Font.NORMAL, BaseColor.BLACK);
                using (MemoryStream stream = new MemoryStream())
                {

                    PdfReader reader = new PdfReader(bytes);
                    using (PdfStamper stamper = new PdfStamper(reader, stream))
                    {
                        Font fntTableFontHdr = FontFactory.GetFont("Times New Roman", 4, Font.BOLD,
                            BaseColor.BLACK);
                        Font fntTableFont = FontFactory.GetFont("Times New Roman", 8, Font.NORMAL,
                            BaseColor.BLACK);

                        int PageCount = reader.NumberOfPages;
                        int pages = reader.NumberOfPages;
                        for (int x = 1; x <= PageCount; x++)
                        {

                            //  ColumnText.ShowTextAligned(stamper.GetOverContent(x), Element.ALIGN_LEFT, new Phrase(String.Format("Hoja {0} de {1}", x, PageCount)), 500f, 15f, 0);

                        }
                    }

                    bytes = stream.ToArray();
                    File.WriteAllBytes(path + "/Edo-Cuenta-" + userId + ".pdf", bytes);
                    string path1 = Server.MapPath("PDF") + "\\Edo-Cuenta-" + userId + ".pdf";
                    WebClient client = new WebClient();
                    Byte[] buffer = client.DownloadData(path1);
                    File.Delete(path + "\\" + userId + ".pdf");
                    if (buffer != null)
                    {

                        Response.Clear();
                        Response.ContentType = "application/pdf";
                        Response.AddHeader("Content-Disposition", "attachment; filename=Estado-Cuenta.pdf");
                        Response.ContentType = "application/pdf";
                        Response.Buffer = true;
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.BinaryWrite(buffer);
                        Response.End();
                        Response.Close();

                    }
                }



            }
        }










        private static
            void AddImageInCell(PdfPCell cell, Image image, float fitWidth, float fitHight, int Alignment)
        {
            image.ScaleToFit(fitWidth, fitHight);
            image.Alignment = Alignment;
            cell.AddElement(image);
        }
        private void AddtextCell(PdfPTable table, PdfPCell cell)
        {
            cell.Colspan = 3;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.Border = 0;
            table.AddCell(cell);
        }
        private void AddtextCell(PdfPTable table, PdfPCell cell, float paddingLeft, float paddingRight)
        {
            cell.Colspan = 3;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            cell.PaddingLeft = paddingLeft;
            cell.PaddingRight = paddingRight;
            cell.Border = 0;
            table.AddCell(cell);
        }
        private void AddtextCell(PdfPTable table, PdfPCell cell, float paddingLeft, float paddingRight, int hAlign)
        {
            cell.Colspan = 3;
            cell.HorizontalAlignment = hAlign; //0=Left, 1=Centre, 2=Right
            cell.PaddingLeft = paddingLeft;
            cell.PaddingRight = paddingRight;
            cell.Border = 0;
            table.AddCell(cell);
        }
        private static void AddtextCell(PdfPTable table, PdfPCell cell, int Colspan, int HorizontalAlignment, int Border)
        {
            cell.Colspan = Colspan;
            cell.HorizontalAlignment = HorizontalAlignment; //0=Left, 1=Centre, 2=Right
            cell.Border = Border;
            table.AddCell(cell);
        }


        public partial class Footer : PdfPageEventHelper
        {
            public override void OnEndPage(PdfWriter writer, Document doc)
            {

                //PdfContentByte content = writer.DirectContent;
                //Rectangle rectangle = new Rectangle(doc.PageSize.Rotate());
                //rectangle.Left += 580;
                //rectangle.Right -= 582;
                //rectangle.Top -= 830;
                //rectangle.Bottom += 820;
                //content.SetColorStroke(BaseColor.BLACK);
                //content.Rectangle(rectangle.Left, rectangle.Bottom, rectangle.Width, rectangle.Height);
                //content.Stroke();

                Paragraph headerfooter = new Paragraph(new Phrase("** Documento sin Valor Oficial",
                 //"Date: " + DateTime.Now.ToString("dd-MMM-yyyy") + " " + "Page: " + writer.PageNumber,
                 new Font(Font.FontFamily.HELVETICA, 6, Font.NORMAL, BaseColor.BLACK)));
                headerfooter.Alignment = Element.ALIGN_CENTER;
                PdfPTable footerTbl = new PdfPTable(1);

                footerTbl.DefaultCell.Border = Rectangle.NO_BORDER;
                footerTbl.SpacingBefore = 15f;
                footerTbl.TotalWidth = 800;
                footerTbl.WidthPercentage = 100;
                footerTbl.LockedWidth = true;
                footerTbl.HorizontalAlignment = Element.ALIGN_RIGHT;

                PdfPCell cell = new PdfPCell(headerfooter);
                cell.Border = 0;
                cell.PaddingLeft = -400;
                footerTbl.AddCell(cell);
                footerTbl.WriteSelectedRows(0, -1, 415, 30, writer.DirectContent);

            }
            public override void OnStartPage(PdfWriter writer, Document document)
            {
                Font fontttblaencabezadouno = new Font(Font.FontFamily.COURIER, 9, Font.BOLD, BaseColor.WHITE);
                PdfPTable tblaencabezadocero = new PdfPTable(1);

                tblaencabezadocero.DefaultCell.Border = Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;
                tblaencabezadocero.SpacingBefore = 0f;
                tblaencabezadocero.TotalWidth = 700;
                tblaencabezadocero.WidthPercentage = 100;
                tblaencabezadocero.LockedWidth = true;
                float[] widthscero = new float[] { 400f };
                tblaencabezadocero.SetWidths(widthscero);

                //0
                PdfPCell cellcero = new PdfPCell(new Paragraph("DETALLE DE MOVIMIENTOS", fontttblaencabezadouno));
                cellcero.Border = 0;
                cellcero.BackgroundColor = (BaseColor.DARK_GRAY);
                cellcero.HorizontalAlignment = Element.ALIGN_CENTER;
                tblaencabezadocero.AddCell(cellcero);

                PdfPTable tblaencabezadouno = new PdfPTable(6);

                tblaencabezadouno.DefaultCell.Border = Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;
                tblaencabezadouno.SpacingBefore = 15f;
                tblaencabezadouno.TotalWidth = 700;
                tblaencabezadouno.WidthPercentage = 100;
                tblaencabezadouno.LockedWidth = true;
                float[] widths = new float[] { 80f, 150f, 80f, 80f, 90f, 80f };
                tblaencabezadouno.SetWidths(widths);


                //0
                PdfPCell cellclave = new PdfPCell(new Paragraph("Periodo", fontttblaencabezadouno));
                cellclave.Border = 0;
                cellclave.BackgroundColor = (BaseColor.DARK_GRAY);
                cellclave.HorizontalAlignment = Element.ALIGN_LEFT;
                tblaencabezadouno.AddCell(cellclave);

                //2
                PdfPCell cellperiodo = new PdfPCell(new Paragraph("Concepto", fontttblaencabezadouno));
                cellperiodo.Border = 0;
                cellperiodo.BackgroundColor = (BaseColor.DARK_GRAY);
                cellperiodo.HorizontalAlignment = Element.ALIGN_LEFT;
                tblaencabezadouno.AddCell(cellperiodo);

                //3
                PdfPCell cellcargo = new PdfPCell(new Paragraph("Costo", fontttblaencabezadouno));
                cellcargo.Border = 0;
                cellcargo.BackgroundColor = (BaseColor.DARK_GRAY);
                cellcargo.HorizontalAlignment = Element.ALIGN_LEFT;
                tblaencabezadouno.AddCell(cellcargo);

                //6
                PdfPCell celltipo = new PdfPCell(new Paragraph("Pagos Realizados", fontttblaencabezadouno));
                celltipo.Border = 0;
                celltipo.BackgroundColor = (BaseColor.DARK_GRAY);
                celltipo.HorizontalAlignment = Element.ALIGN_LEFT;
                tblaencabezadouno.AddCell(celltipo);

                //7
                PdfPCell cellsaldo = new PdfPCell(new Paragraph("Fecha de Pago", fontttblaencabezadouno));
                cellsaldo.Border = 0;
                cellsaldo.BackgroundColor = (BaseColor.DARK_GRAY);
                cellsaldo.HorizontalAlignment = Element.ALIGN_LEFT;
                tblaencabezadouno.AddCell(cellsaldo);

                //8
                PdfPCell cellimporte = new PdfPCell(new Paragraph("Saldo", fontttblaencabezadouno));
                cellimporte.Border = 0;
                cellimporte.BackgroundColor = (BaseColor.DARK_GRAY);
                cellimporte.HorizontalAlignment = Element.ALIGN_LEFT;
                tblaencabezadouno.AddCell(cellimporte);



                /* Rectangle page = document.PageSize.Rotate();
                 PdfPTable table = new PdfPTable(3);
                 table.TotalWidth = 300;
                 table.WidthPercentage = 100;
                 table.HorizontalAlignment = Element.ALIGN_RIGHT;
                 float[] widths111 = new float[] { 300f, 300f, 200f };
                 table.SetWidths(widths111);

                 PdfPCell cell = new PdfPCell();
                 cell.Colspan = 4;
                 cell.Border = 0;
                 string path = @"D:\inetpub\wwwroot\Servicios_Web_ULA\Estado_de_Cuenta\Images";
                 Image imgLogo = Image.GetInstance(path + "//logo_ula_vanta.jpg");
                 AddImageInCell(cell, imgLogo, 200f, 85f, 1);*/

                Rectangle page = document.PageSize.Rotate();
                PdfPTable table = new PdfPTable(3);
                table.TotalWidth = 300;
                table.WidthPercentage = 100;
                table.HorizontalAlignment = Element.ALIGN_RIGHT;
                //string path = @"D:\inetpub\wwwroot\Servicios_Web_ULA\Estado_de_Cuenta\Images";
                //string strLogoPath = Server.MapPath("Images") + "//logo_ula_vanta.jpg";

                PdfPCell cell = new PdfPCell();
                cell.Colspan = 4;
                cell.Border = 0;
                table.LockedWidth = true;
                //float[] widths11 = new float[] { 300f, 200f, 100f};
                float[] widths11 = new float[] { 300f, 300f, 200f };
                table.SetWidths(widths11);
                //table.AddCell(cell);
                //AddImageInCell(cell, image, 200f, 85f, 0);
                // AddImageInCell(cell, imgLogo, 200f, 85f, 1);
                table.AddCell(cell);
                document.Add(table);
                document.Add(tblaencabezadouno);

            }

        }
    }
}
