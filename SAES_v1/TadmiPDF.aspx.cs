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
using System.Web.UI.WebControls;
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
    public partial class TadmiPDF : System.Web.UI.Page
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
                    getData();
                }

            }
        }

        private void getData()
        {
            string strQuery = "";
            strQuery = " select '--' titulo, 0 parcia, tsimu_tcoco_clave concepto, tcoco_desc nombre, tsimu_importe importe, " +
                " tpcbe_porcentaje porc_beca, tpcbe_monto  monto_beca,  " +
                " (select tpaco_pdesc_insc from tpaco, tcaes , tprog where tpaco_clave = '01' " +
                "    and tprog_clave ='" + Global.programa.ToString() + "' and tpaco_tpees_clave ='" + Global.periodo.ToString() + "' " +
                " and tpaco_tcamp_clave ='" + Global.campus.ToString() + "'" +
                " and tpaco_tnive_clave = tprog_tnive_clave and tcaes_tpees_clave = tpaco_tpees_clave " +
                " and tcaes_tcoca_clave = tpaco_tcoca_clave and tcaes_tcamp_clave = tpaco_tcamp_clave " +
                "  and tcaes_tnive_clave = tpaco_tnive_clave  and curdate() between tcaes_inicio and tcaes_fin)  Descuento, " +
                " date_format(curdate(), '%d %b %Y') vencimiento " +
                " from tsimu inner join tcoco on tsimu_tcoco_clave = tcoco_clave and tcoco_ind_parc = 'N' " +
                " inner join tprog on tprog_clave ='" + Global.programa.ToString() + "'" +
                " left outer join tpcbe on tsimu_tcoco_clave = tpcbe_tcoco_cargo and tpcbe_clave ='" + Global.descuento.ToString() + "' " +
                " and tpcbe_tcamp_clave ='" + Global.campus.ToString() + "' and(tpcbe_tnive_clave = tprog_tnive_clave or tpcbe_tnive_clave = '000') " +
                " and tpcbe_estatus='A' " +
                " where tsimu_id ='" + Global.cuenta.ToString() + "'" +
                " union " +
                " select '--' titulo, tfeve_numero parcia, tsimu_tcoco_clave concepto, tcoco_desc nombre, tsimu_importe importe,  " +
                " tpcbe_porcentaje porc_beca, tpcbe_monto monto_beca, " +
                " (select tpaco_pdesc_parc from tpaco, tcaes , tprog where tpaco_clave = '01' and tprog_clave = '" + Global.programa.ToString() + "'" +
                " and tpaco_tpees_clave ='" + Global.periodo.ToString() + "' and tpaco_tcamp_clave ='" + Global.campus.ToString() + "'" +
                " and tpaco_tnive_clave = tprog_tnive_clave and tcaes_tpees_clave = tpaco_tpees_clave and tcaes_tcoca_clave = tpaco_tcoca_clave " +
                " and tcaes_tcamp_clave = tpaco_tcamp_clave and tcaes_tnive_clave = tpaco_tnive_clave and curdate() between tcaes_inicio and tcaes_fin) Descuento, " +
                " date_format(tfeve_vencimiento, '%d %b %Y') vencimiento " +
                " from tsimu inner join tcoco on tsimu_tcoco_clave = tcoco_clave and tcoco_ind_parc = 'S' " +
                " inner join tprog on tprog_clave ='" + Global.programa.ToString() + "'" +
                " left outer join tpcbe on tsimu_tcoco_clave = tpcbe_tcoco_cargo and tpcbe_clave ='" + Global.descuento.ToString() + "'" +
                " and tpcbe_tcamp_clave ='" + Global.campus.ToString() + "'   and(tpcbe_tnive_clave = tprog_tnive_clave or tpcbe_tnive_clave = '000') " +
                " and tpcbe_estatus='A' " +
                "  inner join tfeve on tfeve_tpees_clave ='" + Global.periodo.ToString() + "' and tfeve_ttasa_clave = tsimu_ttasa_clave " +
                "  where tsimu_id ='" + Global.cuenta.ToString() + "' order by parcia, concepto ";

            MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            conexion.Open();
            MySqlDataReader DatosMySql;
            DataTable TablaSimula = new DataTable();

            MySqlCommand ConsultaMySql1 = new MySqlCommand();
            ConsultaMySql1.Connection = conexion;
            ConsultaMySql1.CommandType = CommandType.Text;
            ConsultaMySql1.CommandText = strQuery;
            DatosMySql = ConsultaMySql1.ExecuteReader();
            TablaSimula.Load(DatosMySql, LoadOption.OverwriteChanges);

            MySqlDataAdapter sqladapter = new MySqlDataAdapter();

            DataSet ds = new DataSet();
            //DataTable ds = new DataTable();

            MySqlCommand commandsql1 = new MySqlCommand(strQuery, conexion);
            sqladapter.SelectCommand = commandsql1;
            sqladapter.Fill(ds);
            sqladapter.Dispose();
            commandsql1.Dispose();

            conexion.Close();

            string path = Server.MapPath("PDF");
            string strLogoPath = Server.MapPath("Images/Sitemaster") + "//Logo_1.png";
            //string strLogoPath = Server.MapPath("Images") + "//logo.png";
            iTextSharp.text.Document doc = new iTextSharp.text.Document(PageSize.A4, 72f, 72f, 40f, 100f);
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(path + "\\" + "Solicitud_" + Global.cuenta + ".pdf", FileMode.Create));
            doc.AddTitle("Solicitud Admisión");
            doc.AddAuthor("Universidad Latinoamericana");

            doc.Open();

            // writer.PageEvent = new Footer();
            for (int i = 1; i <= 5; i++)
            {

                //Logo
                Rectangle page = doc.PageSize;
                PdfPTable table = new PdfPTable(2);
                table.WidthPercentage = 100;
                PdfPCell cell = new PdfPCell();
                cell.Colspan = 4;
                cell.Border = 0;
                iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(strLogoPath);
                AddImageInCell(cell, image, 200f, 85f, 0);
                table.AddCell(cell);

                string TITULO = "";
                string PARCIA = "";
                string CONCEPTO = "";
                string NOMBRE = "";
                string IMPORTE = "";
                string BECA = "";
                string DESCUENTO = "";
                string SALDO = "";
                string VENCIMIENTO = "";

                PdfPTable tblTitulo = new PdfPTable(1);

                tblTitulo.DefaultCell.Border = Rectangle.NO_BORDER;
                tblTitulo.SpacingBefore = 15f;
                tblTitulo.TotalWidth = 500;
                tblTitulo.WidthPercentage = 100;
                tblTitulo.LockedWidth = true;

                Font fonttblTitulo = new Font(Font.FontFamily.TIMES_ROMAN, 12, Font.NORMAL);
                Font fonttCampus = new Font(Font.FontFamily.COURIER, 10, Font.NORMAL);
                Font fonttNivel = new Font(Font.FontFamily.COURIER, 8, Font.NORMAL);
                Font fonttHistoria = new Font(Font.FontFamily.HELVETICA, 11, Font.UNDERLINE);

                PdfPCell cellHistoria = new PdfPCell(new Paragraph("''" + "SOLICITUD ADMISION" + "''\n\n", fonttHistoria));
                cellHistoria.Border = 0;
                cellHistoria.BackgroundColor = (BaseColor.WHITE);
                cellHistoria.HorizontalAlignment = Element.ALIGN_CENTER;
                tblTitulo.AddCell(cellHistoria);

                PdfPTable tblalumno = new PdfPTable(2);

                tblalumno.DefaultCell.Border = Rectangle.NO_BORDER;
                tblalumno.SpacingBefore = 0f;
                tblalumno.TotalWidth = 500;
                tblalumno.WidthPercentage = 100;
                tblalumno.LockedWidth = true;

                Font fonttblalumno = new Font(Font.FontFamily.COURIER, 8, Font.NORMAL);
                Font fonttblalumnoDB = new Font(Font.FontFamily.COURIER, 8, Font.BOLD);

                PdfPCell celltblalumno3 = new PdfPCell(new Paragraph("", fonttblalumno));
                celltblalumno3.Border = 0;
                celltblalumno3.BackgroundColor = (BaseColor.WHITE);
                celltblalumno3.HorizontalAlignment = Element.ALIGN_RIGHT;
                tblalumno.AddCell(celltblalumno3);

                PdfPCell celltblalumnoBD3 = new PdfPCell(new Paragraph("Fecha:" + System.DateTime.Now.ToShortDateString(), fonttblalumnoDB));
                celltblalumnoBD3.Border = 0;
                celltblalumnoBD3.BackgroundColor = (BaseColor.WHITE);
                celltblalumnoBD3.HorizontalAlignment = Element.ALIGN_RIGHT;
                tblalumno.AddCell(celltblalumnoBD3);

                PdfPCell celltblalumno2 = new PdfPCell(new Paragraph("MATRICULA:", fonttblalumno));
                celltblalumno2.Border = 0;
                celltblalumno2.BackgroundColor = (BaseColor.WHITE);
                celltblalumno2.HorizontalAlignment = Element.ALIGN_RIGHT;
                tblalumno.AddCell(celltblalumno2);

                PdfPCell celltblalumnoBD2 = new PdfPCell(new Paragraph(Global.cuenta, fonttblalumnoDB));
                celltblalumnoBD2.Border = 0;
                celltblalumnoBD2.BackgroundColor = (BaseColor.WHITE);
                celltblalumnoBD2.HorizontalAlignment = Element.ALIGN_LEFT;
                tblalumno.AddCell(celltblalumnoBD2);

                PdfPCell celltblalumno = new PdfPCell(new Paragraph("NOMBRE DEL ALUMNO:", fonttblalumno));
                celltblalumno.Border = 0;
                celltblalumno.BackgroundColor = (BaseColor.WHITE);
                celltblalumno.HorizontalAlignment = Element.ALIGN_RIGHT;
                tblalumno.AddCell(celltblalumno);

                PdfPCell celltblalumnoBD = new PdfPCell(new Paragraph(Global.nombre_alumno, fonttblalumnoDB));
                celltblalumnoBD.Border = 0;
                celltblalumnoBD.BackgroundColor = (BaseColor.WHITE);
                celltblalumnoBD.HorizontalAlignment = Element.ALIGN_LEFT;
                tblalumno.AddCell(celltblalumnoBD);

                PdfPCell celltblalumno1 = new PdfPCell(new Paragraph("PROGRAMA EDUCATIVO:", fonttblalumno));
                celltblalumno1.Border = 0;
                celltblalumno1.BackgroundColor = (BaseColor.WHITE);
                celltblalumno1.HorizontalAlignment = Element.ALIGN_RIGHT;
                tblalumno.AddCell(celltblalumno1);

                PdfPCell celltblalumnoBD1 = new PdfPCell(new Paragraph(Global.nombre_programa, fonttblalumnoDB));
                celltblalumnoBD1.Border = 0;
                celltblalumnoBD1.BackgroundColor = (BaseColor.WHITE);
                celltblalumnoBD1.HorizontalAlignment = Element.ALIGN_LEFT;
                tblalumno.AddCell(celltblalumnoBD1);


                PdfPTable tblalumno1 = new PdfPTable(4);

                tblalumno1.DefaultCell.Border = Rectangle.NO_BORDER;
                tblalumno1.SpacingBefore = 15f;
                tblalumno1.TotalWidth = 500;
                tblalumno1.WidthPercentage = 100;
                tblalumno1.LockedWidth = true;

                Font fonttblalumno1 = new Font(Font.FontFamily.COURIER, 8, Font.NORMAL);
                Font fonttblalumno1DB = new Font(Font.FontFamily.COURIER, 8, Font.BOLD);

                PdfPCell cellcamp = new PdfPCell(new Paragraph("CAMPUS:", fonttblalumno1));
                cellcamp.Border = 0;
                cellcamp.BackgroundColor = (BaseColor.WHITE);
                cellcamp.HorizontalAlignment = Element.ALIGN_RIGHT;
                tblalumno1.AddCell(cellcamp);

                PdfPCell cellcampus = new PdfPCell(new Paragraph(Global.nombre_campus, fonttblalumno1DB));
                cellcampus.Border = 0;
                cellcampus.BackgroundColor = (BaseColor.WHITE);
                cellcampus.HorizontalAlignment = Element.ALIGN_LEFT;
                tblalumno1.AddCell(cellcampus);

                PdfPCell cellper = new PdfPCell(new Paragraph("PERIODO:", fonttblalumno1));
                cellper.Border = 0;
                cellper.BackgroundColor = (BaseColor.WHITE);
                cellper.HorizontalAlignment = Element.ALIGN_RIGHT;
                tblalumno1.AddCell(cellper);

                PdfPCell cellperiodo = new PdfPCell(new Paragraph(Global.nombre_periodo, fonttblalumno1DB));
                cellperiodo.Border = 0;
                cellperiodo.BackgroundColor = (BaseColor.WHITE);
                cellperiodo.HorizontalAlignment = Element.ALIGN_LEFT;
                tblalumno1.AddCell(cellperiodo);

                PdfPCell cellespr = new PdfPCell(new Paragraph("ESCUELA PROCEDENCIA:", fonttblalumno1));
                cellespr.Border = 0;
                cellespr.BackgroundColor = (BaseColor.WHITE);
                cellespr.HorizontalAlignment = Element.ALIGN_RIGHT;
                tblalumno1.AddCell(cellespr);

                PdfPCell cellescuela = new PdfPCell(new Paragraph(Global.procedencia, fonttblalumno1DB));
                cellescuela.Border = 0;
                cellescuela.BackgroundColor = (BaseColor.WHITE);
                cellescuela.HorizontalAlignment = Element.ALIGN_LEFT;
                tblalumno1.AddCell(cellescuela);

                PdfPCell cellprom = new PdfPCell(new Paragraph("PROMEDIO:", fonttblalumno1));
                cellprom.Border = 0;
                cellprom.BackgroundColor = (BaseColor.WHITE);
                cellprom.HorizontalAlignment = Element.ALIGN_RIGHT;
                tblalumno1.AddCell(cellprom);

                PdfPCell cellpromedio = new PdfPCell(new Paragraph(Global.promedio, fonttblalumno1DB));
                cellpromedio.Border = 0;
                cellpromedio.BackgroundColor = (BaseColor.WHITE);
                cellpromedio.HorizontalAlignment = Element.ALIGN_LEFT;
                tblalumno1.AddCell(cellpromedio);

                PdfPCell celltiin = new PdfPCell(new Paragraph("TIPO INGRESO:", fonttblalumno1));
                celltiin.Border = 0;
                celltiin.BackgroundColor = (BaseColor.WHITE);
                celltiin.HorizontalAlignment = Element.ALIGN_RIGHT;
                tblalumno1.AddCell(celltiin);

                PdfPCell celltiingreso = new PdfPCell(new Paragraph(Global.nombre_tipoingreso, fonttblalumno1DB));
                celltiingreso.Border = 0;
                celltiingreso.BackgroundColor = (BaseColor.WHITE);
                celltiingreso.HorizontalAlignment = Element.ALIGN_LEFT;
                tblalumno1.AddCell(celltiingreso);

                PdfPCell celldesc = new PdfPCell(new Paragraph("PLAN COBRO/BECA:", fonttblalumno1));
                celldesc.Border = 0;
                celldesc.BackgroundColor = (BaseColor.WHITE);
                celldesc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tblalumno1.AddCell(celldesc);

                PdfPCell celldescuento = new PdfPCell(new Paragraph(Global.nombre_descuento, fonttblalumno1DB));
                celldescuento.Border = 0;
                celldescuento.BackgroundColor = (BaseColor.WHITE);
                celldescuento.HorizontalAlignment = Element.ALIGN_LEFT;
                tblalumno1.AddCell(celldescuento);

                Font fonttitulo = new Font(Font.FontFamily.HELVETICA, 11, Font.NORMAL);
                PdfPTable tblTitulo1 = new PdfPTable(1);

                tblTitulo1.DefaultCell.Border = Rectangle.NO_BORDER;
                tblTitulo1.SpacingBefore = 15f;
                tblTitulo1.TotalWidth = 500;
                tblTitulo1.WidthPercentage = 100;
                tblTitulo1.LockedWidth = true;

                PdfPCell celltitulo1 = new PdfPCell(new Paragraph("ESTADO DE CUENTA", fonttitulo));
                celltitulo1.Border = 0;
                celltitulo1.BackgroundColor = (BaseColor.WHITE);
                celltitulo1.HorizontalAlignment = Element.ALIGN_CENTER;
                tblTitulo1.AddCell(celltitulo1);

                PdfPTable tablaedocuenta = new PdfPTable(8);

                tablaedocuenta.DefaultCell.Border = Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;
                tablaedocuenta.SpacingBefore = 0f;
                tablaedocuenta.TotalWidth = 500;
                tablaedocuenta.WidthPercentage = 100;
                tablaedocuenta.LockedWidth = true;
                float[] widths = new float[] { 50f, 50f, 180f, 50f, 50f, 50f, 50f, 60f };
                tablaedocuenta.SetWidths(widths);

                Font fonttblacreditosasignaturas = new Font(Font.FontFamily.COURIER, 7, Font.BOLD);

                PdfPCell cellparc = new PdfPCell(new Paragraph("PARC.", fonttblacreditosasignaturas));
                cellparc.Border = 1;
                cellparc.BorderColorLeft = BaseColor.BLACK;
                cellparc.BorderColorTop = BaseColor.BLACK;
                cellparc.BorderColorBottom = BaseColor.BLACK;
                cellparc.BorderWidthLeft = 1f;
                cellparc.BorderWidthTop = 1f;
                cellparc.BorderWidthBottom = 1f;

                cellparc.BackgroundColor = (BaseColor.LIGHT_GRAY);
                cellparc.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaedocuenta.AddCell(cellparc);

                PdfPCell cellclave = new PdfPCell(new Paragraph("CLAVE", fonttblacreditosasignaturas));
                cellclave.Border = 1;
                cellclave.BorderColorLeft = BaseColor.BLACK;
                cellclave.BorderColorRight = BaseColor.BLACK;
                cellclave.BorderColorTop = BaseColor.BLACK;
                cellclave.BorderColorBottom = BaseColor.BLACK;
                cellclave.BorderWidthTop = 1f;
                cellclave.BorderWidthBottom = 1f;

                cellclave.BackgroundColor = (BaseColor.LIGHT_GRAY);
                cellclave.HorizontalAlignment = Element.ALIGN_LEFT;
                tablaedocuenta.AddCell(cellclave);

                PdfPCell cellconcepto = new PdfPCell(new Paragraph("CONCEPTO", fonttblacreditosasignaturas));
                cellconcepto.Border = 1;
                cellconcepto.BorderColorLeft = BaseColor.BLACK;
                cellconcepto.BorderColorRight = BaseColor.BLACK;
                cellconcepto.BorderColorTop = BaseColor.BLACK;
                cellconcepto.BorderColorBottom = BaseColor.BLACK;
                cellconcepto.BorderWidthTop = 1f;
                cellconcepto.BorderWidthBottom = 1f;
                cellconcepto.BackgroundColor = (BaseColor.LIGHT_GRAY);
                cellconcepto.HorizontalAlignment = Element.ALIGN_LEFT;
                tablaedocuenta.AddCell(cellconcepto);

                PdfPCell cellimporte = new PdfPCell(new Paragraph("IMPORTE", fonttblacreditosasignaturas));
                cellimporte.Border = 1;
                cellimporte.BorderColorLeft = BaseColor.BLACK;
                cellimporte.BorderColorRight = BaseColor.BLACK;
                cellimporte.BorderColorTop = BaseColor.BLACK;
                cellimporte.BorderColorBottom = BaseColor.BLACK;
                cellimporte.BorderWidthTop = 1f;
                cellimporte.BorderWidthBottom = 1f;
                cellimporte.BackgroundColor = (BaseColor.LIGHT_GRAY);
                cellimporte.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaedocuenta.AddCell(cellimporte);

                PdfPCell cellbeca = new PdfPCell(new Paragraph("DESCUENTO", fonttblacreditosasignaturas));
                cellbeca.Border = 1;
                cellbeca.BorderColorLeft = BaseColor.BLACK;
                cellbeca.BorderColorRight = BaseColor.BLACK;
                cellbeca.BorderColorTop = BaseColor.BLACK;
                cellbeca.BorderColorBottom = BaseColor.BLACK;
                cellbeca.BorderWidthTop = 1f;
                cellbeca.BorderWidthBottom = 1f;
                cellbeca.BackgroundColor = (BaseColor.LIGHT_GRAY);
                cellbeca.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaedocuenta.AddCell(cellbeca);

                PdfPCell celldescu = new PdfPCell(new Paragraph("BECA", fonttblacreditosasignaturas));
                celldescu.Border = 1;
                celldescu.BorderColorLeft = BaseColor.BLACK;
                celldescu.BorderColorRight = BaseColor.BLACK;
                celldescu.BorderColorTop = BaseColor.BLACK;
                celldescu.BorderColorBottom = BaseColor.BLACK;
                celldescu.BorderWidthTop = 1f;
                celldescu.BorderWidthBottom = 1f;
                celldescu.BackgroundColor = (BaseColor.LIGHT_GRAY);
                celldescu.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaedocuenta.AddCell(celldescu);

                PdfPCell cellsaldo = new PdfPCell(new Paragraph("SALDO", fonttblacreditosasignaturas));
                cellsaldo.Border = 1;
                cellsaldo.BorderColorLeft = BaseColor.BLACK;
                cellsaldo.BorderColorRight = BaseColor.BLACK;
                cellsaldo.BorderColorTop = BaseColor.BLACK;
                cellsaldo.BorderColorBottom = BaseColor.BLACK;
                cellsaldo.BorderWidthTop = 1f;
                cellsaldo.BorderWidthBottom = 1f;
                cellsaldo.BackgroundColor = (BaseColor.LIGHT_GRAY);
                cellsaldo.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaedocuenta.AddCell(cellsaldo);

                PdfPCell cellvenc = new PdfPCell(new Paragraph("VENCIMIENTO", fonttblacreditosasignaturas));
                cellvenc.Border = 1;
                cellvenc.BorderColorLeft = BaseColor.BLACK;
                cellvenc.BorderColorRight = BaseColor.BLACK;
                cellvenc.BorderColorTop = BaseColor.BLACK;
                cellvenc.BorderColorBottom = BaseColor.BLACK;
                cellvenc.BorderWidthRight = 1f;
                cellvenc.BorderWidthTop = 1f;
                cellvenc.BorderWidthBottom = 1f;
                cellvenc.BackgroundColor = (BaseColor.LIGHT_GRAY);
                cellvenc.HorizontalAlignment = Element.ALIGN_LEFT;
                tablaedocuenta.AddCell(cellvenc);

                PdfPTable tabladetalle = new PdfPTable(8);
                tabladetalle.DefaultCell.Border = Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;
                tabladetalle.SpacingBefore = 15f;
                tabladetalle.TotalWidth = 500;
                tabladetalle.WidthPercentage = 100;
                tabladetalle.LockedWidth = true;
                float[] widths1 = new float[] { 50f, 50f, 180f, 50f, 50f, 50f, 50f, 60f };
                tabladetalle.SetWidths(widths1);
                Font fontdatatable = new Font(Font.FontFamily.COURIER, 7, Font.BOLD);

                decimal suma_importe = 0; decimal suma_descuento = 0; decimal suma_saldo = 0; decimal suma_beca = 0;
                decimal dsco = 0; decimal be = 0; decimal amount = 0;

                for (int w = 0; w < ds.Tables[0].Rows.Count; w++)
                {
                    PARCIA = ds.Tables[0].Rows[w][1].ToString();
                    CONCEPTO = ds.Tables[0].Rows[w][2].ToString();
                    NOMBRE = ds.Tables[0].Rows[w][3].ToString();
                    IMPORTE = ds.Tables[0].Rows[w][4].ToString();
                    amount = Convert.ToDecimal(ds.Tables[0].Rows[w][4].ToString());
                    dsco = 0; be = 0;
                    if (ds.Tables[0].Rows[w][7].ToString() != "" && ds.Tables[0].Rows[w][7].ToString() != null)
                    {
                        dsco = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[w][7].ToString()) / 100 * Convert.ToDecimal(ds.Tables[0].Rows[w][4].ToString()), 2);
                    }
                    DESCUENTO = dsco.ToString("#.00");
                    if (ds.Tables[0].Rows[w][5].ToString() != "" && ds.Tables[0].Rows[w][5].ToString() != null)
                    {
                        if (Convert.ToDecimal(ds.Tables[0].Rows[w][5].ToString()) > 0)
                        {
                            be = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[w][5].ToString()) / 100 * (Convert.ToDecimal(ds.Tables[0].Rows[w][4].ToString()) - dsco), 2);
                        }
                    }
                    if (ds.Tables[0].Rows[w][6].ToString() != "" && ds.Tables[0].Rows[w][6].ToString() != null)
                    {
                        if (Convert.ToDecimal(ds.Tables[0].Rows[w][6].ToString()) > 0)
                        {
                            be = Convert.ToDecimal(ds.Tables[0].Rows[w][6].ToString());
                        }
                    }
                    IMPORTE = amount.ToString("#.00");
                    BECA = be.ToString("#.00");
                    decimal balance = 0;
                    balance = amount - (dsco + be);
                    SALDO = balance.ToString("#.00");
                    VENCIMIENTO = ds.Tables[0].Rows[w][8].ToString();


                    PdfPCell celdaparcia = new PdfPCell(new Paragraph(PARCIA, fontdatatable));
                    celdaparcia.Border = 0;
                    celdaparcia.BackgroundColor = (BaseColor.WHITE);
                    celdaparcia.HorizontalAlignment = Element.ALIGN_CENTER;
                    tabladetalle.AddCell(celdaparcia);

                    PdfPCell celdaconcepto = new PdfPCell(new Paragraph(CONCEPTO, fontdatatable));
                    celdaconcepto.Border = 0;
                    celdaconcepto.BackgroundColor = (BaseColor.WHITE);
                    celdaconcepto.HorizontalAlignment = Element.ALIGN_LEFT;
                    tabladetalle.AddCell(celdaconcepto);

                    PdfPCell celdanombre = new PdfPCell(new Paragraph(NOMBRE, fontdatatable));
                    celdanombre.Border = 0;
                    celdanombre.BackgroundColor = (BaseColor.WHITE);
                    celdanombre.HorizontalAlignment = Element.ALIGN_LEFT;
                    tabladetalle.AddCell(celdanombre);

                    PdfPCell celdaimporte = new PdfPCell(new Paragraph(IMPORTE, fontdatatable));
                    celdaimporte.Border = 0;
                    celdaimporte.BackgroundColor = (BaseColor.WHITE);
                    celdaimporte.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tabladetalle.AddCell(celdaimporte);

                    PdfPCell celdabeca = new PdfPCell(new Paragraph(DESCUENTO, fontdatatable));
                    celdabeca.Border = 0;
                    celdabeca.BackgroundColor = (BaseColor.WHITE);
                    celdabeca.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tabladetalle.AddCell(celdabeca);

                    PdfPCell celdadescuento = new PdfPCell(new Paragraph(BECA, fontdatatable));
                    celdadescuento.Border = 0;
                    celdadescuento.BackgroundColor = (BaseColor.WHITE);
                    celdadescuento.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tabladetalle.AddCell(celdadescuento);

                    PdfPCell celdasaldo = new PdfPCell(new Paragraph(SALDO, fontdatatable));
                    celdasaldo.Border = 0;
                    celdasaldo.BackgroundColor = (BaseColor.WHITE);
                    celdasaldo.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tabladetalle.AddCell(celdasaldo);

                    PdfPCell celdavenc = new PdfPCell(new Paragraph(VENCIMIENTO, fontdatatable));
                    celdavenc.Border = 0;
                    celdavenc.BackgroundColor = (BaseColor.WHITE);
                    celdavenc.HorizontalAlignment = Element.ALIGN_LEFT;
                    tabladetalle.AddCell(celdavenc);

                    suma_importe = suma_importe + amount;
                    suma_beca = suma_beca + be;
                    suma_descuento = suma_descuento + dsco;
                    suma_saldo = suma_saldo + balance;
                }

                PdfPTable linea = new PdfPTable(1);

                linea.DefaultCell.Border = Rectangle.NO_BORDER;
                linea.SpacingBefore = 0f;
                linea.TotalWidth = 500;
                linea.WidthPercentage = 100;
                linea.LockedWidth = true;
                Font fontlinea = new Font(Font.FontFamily.HELVETICA, 11, Font.NORMAL);

                PdfPCell celllinea = new PdfPCell(new Paragraph("---------------------------------------------------------------------------------------------------------------------------", fontlinea));
                celllinea.Border = 0;
                celllinea.BackgroundColor = (BaseColor.WHITE);
                celllinea.HorizontalAlignment = Element.ALIGN_CENTER;
                linea.AddCell(celllinea);

                PdfPTable tablasuma = new PdfPTable(8);
                tablasuma.DefaultCell.Border = Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;
                tablasuma.SpacingBefore = 0f;
                tablasuma.TotalWidth = 500;
                tablasuma.WidthPercentage = 100;
                tablasuma.LockedWidth = true;
                float[] widths2 = new float[] { 50f, 50f, 180f, 50f, 50f, 50f, 50f, 60f };
                tablasuma.SetWidths(widths2);

                PdfPCell celdaparciasuma = new PdfPCell(new Paragraph("", fontdatatable));
                celdaparciasuma.Border = 0;
                celdaparciasuma.BackgroundColor = (BaseColor.WHITE);
                celdaparciasuma.HorizontalAlignment = Element.ALIGN_LEFT;
                tablasuma.AddCell(celdaparciasuma);

                PdfPCell celdaconceptosuma = new PdfPCell(new Paragraph("", fontdatatable));
                celdaconceptosuma.Border = 0;
                celdaconceptosuma.BackgroundColor = (BaseColor.WHITE);
                celdaconceptosuma.HorizontalAlignment = Element.ALIGN_LEFT;
                tablasuma.AddCell(celdaconceptosuma);

                PdfPCell celdanombresuma = new PdfPCell(new Paragraph("", fontdatatable));
                celdanombresuma.Border = 0;
                celdanombresuma.BackgroundColor = (BaseColor.WHITE);
                celdanombresuma.HorizontalAlignment = Element.ALIGN_LEFT;
                tablasuma.AddCell(celdanombresuma);

                PdfPCell celdaimportesuma = new PdfPCell(new Paragraph(suma_importe.ToString("#.00"), fontdatatable));
                celdaimportesuma.Border = 0;
                celdaimportesuma.BackgroundColor = (BaseColor.WHITE);
                celdaimportesuma.HorizontalAlignment = Element.ALIGN_RIGHT;
                tablasuma.AddCell(celdaimportesuma);

                PdfPCell celdabecasuma = new PdfPCell(new Paragraph(suma_beca.ToString("#.00"), fontdatatable));
                celdabecasuma.Border = 0;
                celdabecasuma.BackgroundColor = (BaseColor.WHITE);
                celdabecasuma.HorizontalAlignment = Element.ALIGN_RIGHT;
                tablasuma.AddCell(celdabecasuma);

                PdfPCell celdadescuentosuma = new PdfPCell(new Paragraph(suma_descuento.ToString("#.00"), fontdatatable));
                celdadescuentosuma.Border = 0;
                celdadescuentosuma.BackgroundColor = (BaseColor.WHITE);
                celdadescuentosuma.HorizontalAlignment = Element.ALIGN_RIGHT;
                tablasuma.AddCell(celdadescuentosuma);

                PdfPCell celdasaldosuma = new PdfPCell(new Paragraph(suma_saldo.ToString("#.00"), fontdatatable));
                celdasaldosuma.Border = 0;
                celdasaldosuma.BackgroundColor = (BaseColor.WHITE);
                celdasaldosuma.HorizontalAlignment = Element.ALIGN_RIGHT;
                tablasuma.AddCell(celdasaldosuma);

                PdfPCell celdavencsuma = new PdfPCell(new Paragraph("", fontdatatable));
                celdavencsuma.Border = 0;
                celdavencsuma.BackgroundColor = (BaseColor.WHITE);
                celdavencsuma.HorizontalAlignment = Element.ALIGN_LEFT;
                tablasuma.AddCell(celdavencsuma);

                doc.NewPage();


                doc.Add(table);
                doc.Add(tblTitulo);
                doc.Add(tblalumno);
                doc.Add(tblalumno1);
                doc.Add(tblTitulo1);
                doc.Add(tablaedocuenta);
                doc.Add(tabladetalle);
                doc.Add(linea);
                doc.Add(tablasuma);

                /*  doc.Add(tblacreditos);
                  doc.Add(tblacreditosllenar);
                  doc.Add(tblacreditosllenar2);
                  doc.Add(tblacreditosasignaturas);
                  doc.Add(datatable);*/
                // }

                //---------------------------------------------------MargenPage-----------------------------------------
                PdfContentByte content = writer.DirectContent;
                Rectangle rectangle = new Rectangle(doc.PageSize);
                rectangle.Left += 580;
                rectangle.Right -= 582;
                rectangle.Top -= 830;
                rectangle.Bottom += 820;
                content.SetColorStroke(BaseColor.BLACK);
                content.Rectangle(rectangle.Left, rectangle.Bottom, rectangle.Width, rectangle.Height);
                content.Stroke();
                doc.Close();

                byte[] bytes = File.ReadAllBytes(path + "/" + "Solicitud_" + Global.cuenta + ".pdf");
                Font blackFont = FontFactory.GetFont("Arial", 4, Font.NORMAL, BaseColor.BLACK);
                using (MemoryStream stream = new MemoryStream())
                {

                    PdfReader reader = new PdfReader(bytes);
                    using (PdfStamper stamper = new PdfStamper(reader, stream))
                    {
                        iTextSharp.text.Font fntTableFontHdr = FontFactory.GetFont("Times New Roman", 4, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                        iTextSharp.text.Font fntTableFont = FontFactory.GetFont("Times New Roman", 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

                        int PageCount = reader.NumberOfPages;
                        int pages = reader.NumberOfPages;
                        for (int x = 1; x <= PageCount; x++)
                        {

                            //ColumnText.ShowTextAligned(stamper.GetOverContent(x), Element.ALIGN_LEFT, new Phrase(String.Format("Hoja {0} de {1}", x, PageCount)), 500f, 15f, 0);

                        }
                    }

                    bytes = stream.ToArray();
                    File.WriteAllBytes(path + "/" + "Solicitud_" + Global.cuenta + ".pdf", bytes);
                    string path1 = Server.MapPath("PDF") + "\\" + "Solicitud_" + Global.cuenta + ".pdf";
                    WebClient client = new WebClient();
                    Byte[] buffer = client.DownloadData(path1);
                    //  File.Delete(path + "\\" + userId + "1.pdf");
                    //  File.Delete(path + "\\" + userId + "2.pdf");
                    if (buffer != null)
                    {
                        Response.Clear();
                        Response.ContentType = "application/pdf";
                        Response.AddHeader("Content-Disposition", "attachment; filename=Solicitud.pdf");
                        Response.ContentType = "application/pdf";
                        Response.Buffer = true;
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.BinaryWrite(buffer);
                        Response.End();
                        Response.Close();

                        // Response.Redirect("http://www3.ula.edu.mx/Servicios_Web_ULA/Historia_Academica/Historia-Academica.aspx");
                    }
                }


            }

        }

        private static void AddImageInCell(PdfPCell cell, iTextSharp.text.Image image, float fitWidth, float fitHight, int Alignment)
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
                //string watermarkText1 = "SIN VALIDEZ OFICIAL";
                //float fontSize1 = 40;
                //float xPosition1 = 300;
                //float yPosition1 = 400;
                //float angle1 = 45;
                //PdfContentByte under1 = writer.DirectContent;
                //BaseFont baseFont1 = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.WINANSI, BaseFont.EMBEDDED);
                //under1.BeginText();
                //under1.SetColorFill(iTextSharp.text.pdf.CMYKColor.LIGHT_GRAY);
                //under1.SetFontAndSize(baseFont1, fontSize1);
                //under1.ShowTextAligned(PdfContentByte.ALIGN_CENTER, watermarkText1, xPosition1, yPosition1, angle1);
                //under1.EndText();

                //PdfContentByte content = writer.DirectContentUnder;
                //Rectangle rectangle = new Rectangle(doc.PageSize);
                //rectangle.Left += 580;
                //rectangle.Right -= 582;
                //rectangle.Top -= 830;
                //rectangle.Bottom += 820;
                //content.SetColorStroke(BaseColor.BLACK);
                //content.Rectangle(rectangle.Left, rectangle.Bottom, rectangle.Width, rectangle.Height);
                //content.Stroke();

                //Paragraph headerfooter = new Paragraph(new Phrase("** DOCUMENTOS SIN VALOR OFICIAL",
                //    //"Date: " + DateTime.Now.ToString("dd-MMM-yyyy") + " " + "Page: " + writer.PageNumber,
                // new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                //headerfooter.Alignment = Element.ALIGN_CENTER;
                //PdfPTable footerTbl = new iTextSharp.text.pdf.PdfPTable(1);

                //footerTbl.DefaultCell.Border = Rectangle.NO_BORDER;
                //footerTbl.SpacingBefore = 15f;
                //footerTbl.TotalWidth = 500;
                //footerTbl.WidthPercentage = 100;
                //footerTbl.LockedWidth = true;
                //footerTbl.HorizontalAlignment = Element.ALIGN_RIGHT;

                //PdfPCell cell = new PdfPCell(headerfooter);
                //cell.Border = 0;
                //cell.PaddingLeft = -400;
                //footerTbl.AddCell(cell);
                //footerTbl.WriteSelectedRows(0, -1, 415, 30, writer.DirectContent);

            }
            public override void OnStartPage(PdfWriter writer, Document document)
            {


                PdfContentByte content = writer.DirectContent;
                Rectangle rectangle = new Rectangle(document.PageSize);
                rectangle.Left += 580;
                rectangle.Right -= 582;
                rectangle.Top -= 830;
                rectangle.Bottom += 820;
                content.SetColorStroke(BaseColor.BLACK);
                content.Rectangle(rectangle.Left, rectangle.Bottom, rectangle.Width, rectangle.Height);
                content.Stroke();

                PdfPTable tabledatos = new PdfPTable(6);

                tabledatos.DefaultCell.Border = Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;
                tabledatos.SpacingBefore = 15f;
                tabledatos.TotalWidth = 500;
                tabledatos.WidthPercentage = 100;
                tabledatos.LockedWidth = true;
                float[] widths = new float[] { 50f, 50f, 200f, 82f, 40f, 80f };
                tabledatos.SetWidths(widths);

                Font fonttabledatos = new Font(Font.FontFamily.COURIER, 7, Font.BOLD);


                PdfPCell cellclave = new PdfPCell(new Paragraph("CLAVE", fonttabledatos));
                cellclave.Border = 1;
                // cellclave = new PdfPCell(new Phrase("Source Review", fonttblacreditosasignaturas));

                cellclave.BorderColorLeft = BaseColor.BLACK;
                // cellclave.BorderColorRight = BaseColor.BLACK;
                cellclave.BorderColorTop = BaseColor.BLACK;
                cellclave.BorderColorBottom = BaseColor.BLACK;
                cellclave.BorderWidthLeft = 1f;
                //cellclave.BorderWidthRight = 1f;
                cellclave.BorderWidthTop = 1f;
                cellclave.BorderWidthBottom = 1f;

                cellclave.BackgroundColor = (BaseColor.LIGHT_GRAY);
                cellclave.HorizontalAlignment = Element.ALIGN_LEFT;
                tabledatos.AddCell(cellclave);

                PdfPCell cellclaveBD = new PdfPCell(new Paragraph("CREDITOS", fonttabledatos));
                cellclaveBD.Border = 1;
                cellclaveBD.BorderColorLeft = BaseColor.BLACK;
                cellclaveBD.BorderColorRight = BaseColor.BLACK;
                cellclaveBD.BorderColorTop = BaseColor.BLACK;
                cellclaveBD.BorderColorBottom = BaseColor.BLACK;
                //cellclaveBD.BorderWidthLeft = 1f;
                // cellclaveBD.BorderWidthRight = 1f;
                cellclaveBD.BorderWidthTop = 1f;
                cellclaveBD.BorderWidthBottom = 1f;

                cellclaveBD.BackgroundColor = (BaseColor.LIGHT_GRAY);
                cellclaveBD.HorizontalAlignment = Element.ALIGN_LEFT;
                tabledatos.AddCell(cellclaveBD);

                PdfPCell cellcreditos = new PdfPCell(new Paragraph("                    ASIGNATURA", fonttabledatos));
                cellcreditos.Border = 1;
                cellcreditos.BorderColorLeft = BaseColor.BLACK;
                cellcreditos.BorderColorRight = BaseColor.BLACK;
                cellcreditos.BorderColorTop = BaseColor.BLACK;
                cellcreditos.BorderColorBottom = BaseColor.BLACK;
                //cellclaveBD.BorderWidthLeft = 1f;
                // cellclaveBD.BorderWidthRight = 1f;
                cellcreditos.BorderWidthTop = 1f;
                cellcreditos.BorderWidthBottom = 1f;

                cellcreditos.BackgroundColor = (BaseColor.LIGHT_GRAY);

                cellcreditos.HorizontalAlignment = Element.ALIGN_LEFT;
                tabledatos.AddCell(cellcreditos);

                PdfPCell cellcreditosBD = new PdfPCell(new Paragraph("  CALIFICACION", fonttabledatos));
                cellcreditosBD.Border = 1;
                cellcreditosBD.BorderColorLeft = BaseColor.BLACK;
                cellcreditosBD.BorderColorRight = BaseColor.BLACK;
                cellcreditosBD.BorderColorTop = BaseColor.BLACK;
                cellcreditosBD.BorderColorBottom = BaseColor.BLACK;
                //cellclaveBD.BorderWidthLeft = 1f;
                // cellclaveBD.BorderWidthRight = 1f;
                cellcreditosBD.BorderWidthTop = 1f;
                cellcreditosBD.BorderWidthBottom = 1f;

                cellcreditosBD.BackgroundColor = (BaseColor.LIGHT_GRAY);

                cellcreditosBD.HorizontalAlignment = Element.ALIGN_LEFT;
                tabledatos.AddCell(cellcreditosBD);

                PdfPCell cellcreditos1 = new PdfPCell(new Paragraph("CICLO", fonttabledatos));
                cellcreditos1.Border = 1;
                cellcreditos1.BorderColorLeft = BaseColor.BLACK;
                cellcreditos1.BorderColorRight = BaseColor.BLACK;
                cellcreditos1.BorderColorTop = BaseColor.BLACK;
                cellcreditos1.BorderColorBottom = BaseColor.BLACK;
                //cellclaveBD.BorderWidthLeft = 1f;
                // cellclaveBD.BorderWidthRight = 1f;
                cellcreditos1.BorderWidthTop = 1f;
                cellcreditos1.BorderWidthBottom = 1f;

                cellcreditos1.BackgroundColor = (BaseColor.LIGHT_GRAY);

                cellcreditos1.HorizontalAlignment = Element.ALIGN_LEFT;
                tabledatos.AddCell(cellcreditos1);

                PdfPCell cellcreditosBD1 = new PdfPCell(new Paragraph("T.EXAMEN", fonttabledatos));
                cellcreditosBD1.Border = 1;

                cellcreditosBD1.BorderColorLeft = BaseColor.BLACK;
                cellcreditosBD1.BorderColorRight = BaseColor.BLACK;
                cellcreditosBD1.BorderColorTop = BaseColor.BLACK;
                cellcreditosBD1.BorderColorBottom = BaseColor.BLACK;
                //cellcreditosBD1.BorderWidthLeft = 1f;
                cellcreditosBD1.BorderWidthRight = 1f;
                cellcreditosBD1.BorderWidthTop = 1f;
                cellcreditosBD1.BorderWidthBottom = 1f;

                cellcreditosBD1.BackgroundColor = (BaseColor.LIGHT_GRAY);

                cellcreditosBD1.HorizontalAlignment = Element.ALIGN_LEFT;
                tabledatos.AddCell(cellcreditosBD1);



                Rectangle page = document.PageSize;
                PdfPTable table = new PdfPTable(2);
                table.WidthPercentage = 100;
                PdfPCell cell = new PdfPCell();
                cell.Colspan = 4;
                cell.Border = 0;
                string path = @"D:\inetpub\wwwroot\\Servicios_Web_ULA\Historia_Academica\Images";
                iTextSharp.text.Image imgLogo = iTextSharp.text.Image.GetInstance(path + "//logo.png");

                AddImageInCell(cell, imgLogo, 200f, 85f, 0);
                table.AddCell(cell);
                document.Add(table);
                document.Add(tabledatos);


            }

        }
    }
}



