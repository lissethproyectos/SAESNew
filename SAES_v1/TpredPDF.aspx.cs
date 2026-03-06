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
    public partial class TpredPDF : System.Web.UI.Page
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
            string Datos = " select concat(tpers_nombre,' ',tpers_paterno,' ',tpers_materno) Alumno, tprog_desc, " +
            " tcamp_desc, tespr_desc, ttiin_desc, tpees_desc, tadmi_promedio, tadmi_tpees_clave, tadmi_tcamp_clave,  " +
            " tpreq_tespr_clave " +
            " from tpers, tprog, tadmi s, tcamp, tespr, ttiin, tpees, tpreq " +
            " where tpers_id = '" + Global.cuenta + "' and tprog_clave = '" + Global.programa + "'" +
            " and tadmi_tpers_num = tpers_num and tadmi_tprog_clave = tprog_clave " +
            " and tadmi_tpees_clave in (select max(tadmi_tpees_clave) from tadmi ss " +
            "     where s.tadmi_tpers_num = ss.tadmi_tpers_num and s.tadmi_tprog_clave = ss.tadmi_tprog_clave) " +
            " and tpreq_tpers_num=tpers_num and tpreq_tprog_clave=tprog_clave " +
            " and tcamp_clave = tadmi_tcamp_clave and tespr_clave = tpreq_tespr_clave " +
            " and ttiin_clave = tadmi_ttiin_clave and tpees_clave = tadmi_tpees_clave ";

            MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            conexion.Open();
            MySqlDataAdapter sqladapter = new MySqlDataAdapter();

            DataSet dsdatos = new DataSet();
            try
            {
                MySqlCommand commandvalida = new MySqlCommand(Datos, conexion);
                sqladapter.SelectCommand = commandvalida;
                sqladapter.Fill(dsdatos);
                sqladapter.Dispose();
                commandvalida.Dispose();
            }
            catch (Exception ex)
            {
                //resultado.Text = "1--" + ex.Message;
            }

            string programa = Global.programa;
            string periodo = dsdatos.Tables[0].Rows[0][7].ToString();
            string cuenta = Global.cuenta;
            string campus = dsdatos.Tables[0].Rows[0][8].ToString(); ;
            string descuento = "040";
            string nombre_alumno = dsdatos.Tables[0].Rows[0][0].ToString();
            string nombre_programa = dsdatos.Tables[0].Rows[0][1].ToString();
            string nombre_campus = dsdatos.Tables[0].Rows[0][2].ToString();
            string nombre_periodo = dsdatos.Tables[0].Rows[0][5].ToString(); ;
            string procedencia = dsdatos.Tables[0].Rows[0][3].ToString();
            string promedio = dsdatos.Tables[0].Rows[0][6].ToString();
            string nombre_tipoingreso = dsdatos.Tables[0].Rows[0][4].ToString();
            string nombre_descuento = "DESCUENTO 40%";
            string clave_esc_proc = dsdatos.Tables[0].Rows[0][9].ToString();
            string strQuery = "";
            strQuery = " select tplan_consecutivo consec, tplan_tarea_clave area, tplan_tmate_clave clave, " +
                    " tmate_desc materia , tpred_mate_origen origen, tpred_tcali_clave cali, " +
                    " (select tpred_mate_origen from tpred a where tpred_tprog_clave = '" + Global.programa + "'" +
                    "   and tpred_tmate_clave = tplan_tmate_clave and tpred_tespr_clave = '" + clave_esc_proc + "'" +
                    "   and tpred_consecutivo in (select max(tpred_consecutivo) from tpred b " +
                    "   where a.tpred_tprog_clave = b.tpred_tprog_clave and a.tpred_tmate_clave = b.tpred_tmate_clave " +
                    "   and a.tpred_tespr_clave = b.tpred_tespr_clave)) sugerida, tpred_estatus " +
                    " from tpers " +
                    " inner join tplan on tplan_tprog_clave = '" + Global.programa + "'" +
                    " inner join tmate on tmate_clave = tplan_tmate_clave " +
                    " left outer join tpred on tpred_tpers_num = tpers_num and tpred_tprog_clave = '" + Global.programa + "'" +
                    " and tplan_tmate_clave = tpred_tmate_clave " +
                    " where tpers_id = '" + Global.cuenta + "'" +
                    " order by area, consec  ";
            // resultado.Text = strQuery;

            MySqlDataReader DatosMySql;
            DataTable TablaSimula = new DataTable();

            MySqlCommand ConsultaMySql1 = new MySqlCommand();
            ConsultaMySql1.Connection = conexion;
            ConsultaMySql1.CommandType = CommandType.Text;
            ConsultaMySql1.CommandText = strQuery;
            DatosMySql = ConsultaMySql1.ExecuteReader();
            TablaSimula.Load(DatosMySql, LoadOption.OverwriteChanges);


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
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(path + "\\" + "Predictamen_" + cuenta + ".pdf", FileMode.Create));
            doc.AddTitle("Predictamen");
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


                string CONSECUTIVO = "";
                string AREA = "";
                string CLAVE = "";
                string MATERIA_I = "";
                string MATERIA_E = "";
                string CALIFICACION = "";


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

                PdfPCell cellHistoria = new PdfPCell(new Paragraph("''" + "PROPUESTA PREDICTAMEN REVALIDACION/EQUIVALENCIA" + "''\n\n", fonttHistoria));
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

                PdfPCell celltblalumnoBD2 = new PdfPCell(new Paragraph(cuenta, fonttblalumnoDB));
                celltblalumnoBD2.Border = 0;
                celltblalumnoBD2.BackgroundColor = (BaseColor.WHITE);
                celltblalumnoBD2.HorizontalAlignment = Element.ALIGN_LEFT;
                tblalumno.AddCell(celltblalumnoBD2);

                PdfPCell celltblalumno = new PdfPCell(new Paragraph("NOMBRE DEL ALUMNO:", fonttblalumno));
                celltblalumno.Border = 0;
                celltblalumno.BackgroundColor = (BaseColor.WHITE);
                celltblalumno.HorizontalAlignment = Element.ALIGN_RIGHT;
                tblalumno.AddCell(celltblalumno);

                PdfPCell celltblalumnoBD = new PdfPCell(new Paragraph(nombre_alumno, fonttblalumnoDB));
                celltblalumnoBD.Border = 0;
                celltblalumnoBD.BackgroundColor = (BaseColor.WHITE);
                celltblalumnoBD.HorizontalAlignment = Element.ALIGN_LEFT;
                tblalumno.AddCell(celltblalumnoBD);

                PdfPCell celltblalumno1 = new PdfPCell(new Paragraph("PROGRAMA EDUCATIVO:", fonttblalumno));
                celltblalumno1.Border = 0;
                celltblalumno1.BackgroundColor = (BaseColor.WHITE);
                celltblalumno1.HorizontalAlignment = Element.ALIGN_RIGHT;
                tblalumno.AddCell(celltblalumno1);

                PdfPCell celltblalumnoBD1 = new PdfPCell(new Paragraph(nombre_programa, fonttblalumnoDB));
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

                PdfPCell cellcampus = new PdfPCell(new Paragraph(nombre_campus, fonttblalumno1DB));
                cellcampus.Border = 0;
                cellcampus.BackgroundColor = (BaseColor.WHITE);
                cellcampus.HorizontalAlignment = Element.ALIGN_LEFT;
                tblalumno1.AddCell(cellcampus);

                PdfPCell cellper = new PdfPCell(new Paragraph("PERIODO:", fonttblalumno1));
                cellper.Border = 0;
                cellper.BackgroundColor = (BaseColor.WHITE);
                cellper.HorizontalAlignment = Element.ALIGN_RIGHT;
                tblalumno1.AddCell(cellper);

                PdfPCell cellperiodo = new PdfPCell(new Paragraph(nombre_periodo, fonttblalumno1DB));
                cellperiodo.Border = 0;
                cellperiodo.BackgroundColor = (BaseColor.WHITE);
                cellperiodo.HorizontalAlignment = Element.ALIGN_LEFT;
                tblalumno1.AddCell(cellperiodo);

                PdfPCell cellespr = new PdfPCell(new Paragraph("ESCUELA PROCEDENCIA:", fonttblalumno1));
                cellespr.Border = 0;
                cellespr.BackgroundColor = (BaseColor.WHITE);
                cellespr.HorizontalAlignment = Element.ALIGN_RIGHT;
                tblalumno1.AddCell(cellespr);

                PdfPCell cellescuela = new PdfPCell(new Paragraph(procedencia, fonttblalumno1DB));
                cellescuela.Border = 0;
                cellescuela.BackgroundColor = (BaseColor.WHITE);
                cellescuela.HorizontalAlignment = Element.ALIGN_LEFT;
                tblalumno1.AddCell(cellescuela);

                PdfPCell cellprom = new PdfPCell(new Paragraph("PROMEDIO:", fonttblalumno1));
                cellprom.Border = 0;
                cellprom.BackgroundColor = (BaseColor.WHITE);
                cellprom.HorizontalAlignment = Element.ALIGN_RIGHT;
                tblalumno1.AddCell(cellprom);

                PdfPCell cellpromedio = new PdfPCell(new Paragraph(promedio, fonttblalumno1DB));
                cellpromedio.Border = 0;
                cellpromedio.BackgroundColor = (BaseColor.WHITE);
                cellpromedio.HorizontalAlignment = Element.ALIGN_LEFT;
                tblalumno1.AddCell(cellpromedio);

                PdfPCell celltiin = new PdfPCell(new Paragraph("TIPO INGRESO:", fonttblalumno1));
                celltiin.Border = 0;
                celltiin.BackgroundColor = (BaseColor.WHITE);
                celltiin.HorizontalAlignment = Element.ALIGN_RIGHT;
                tblalumno1.AddCell(celltiin);

                PdfPCell celltiingreso = new PdfPCell(new Paragraph(nombre_tipoingreso, fonttblalumno1DB));
                celltiingreso.Border = 0;
                celltiingreso.BackgroundColor = (BaseColor.WHITE);
                celltiingreso.HorizontalAlignment = Element.ALIGN_LEFT;
                tblalumno1.AddCell(celltiingreso);

                PdfPCell celldesc = new PdfPCell(new Paragraph("", fonttblalumno1));
                celldesc.Border = 0;
                celldesc.BackgroundColor = (BaseColor.WHITE);
                celldesc.HorizontalAlignment = Element.ALIGN_RIGHT;
                tblalumno1.AddCell(celldesc);

                PdfPCell celldescuento = new PdfPCell(new Paragraph("", fonttblalumno1DB));
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

                PdfPCell celltitulo1 = new PdfPCell(new Paragraph("PREDICTAMEN", fonttitulo));
                celltitulo1.Border = 0;
                celltitulo1.BackgroundColor = (BaseColor.WHITE);
                celltitulo1.HorizontalAlignment = Element.ALIGN_CENTER;
                tblTitulo1.AddCell(celltitulo1);

                PdfPTable tablaedocuenta = new PdfPTable(6);

                tablaedocuenta.DefaultCell.Border = Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;
                tablaedocuenta.SpacingBefore = 0f;
                tablaedocuenta.TotalWidth = 500;
                tablaedocuenta.WidthPercentage = 100;
                tablaedocuenta.LockedWidth = true;
                float[] widths = new float[] { 60f, 60f, 50F, 200f, 70f, 200f };
                tablaedocuenta.SetWidths(widths);

                Font fonttblacreditosasignaturas = new Font(Font.FontFamily.COURIER, 7, Font.BOLD);

                PdfPCell cellparc = new PdfPCell(new Paragraph("CONS.", fonttblacreditosasignaturas));
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

                PdfPCell cellclave = new PdfPCell(new Paragraph("AREA", fonttblacreditosasignaturas));
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

                PdfPCell cellconcepto = new PdfPCell(new Paragraph("CLAVE", fonttblacreditosasignaturas));
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

                PdfPCell cellimporte = new PdfPCell(new Paragraph("MATERIA INTERNA", fonttblacreditosasignaturas));
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

                PdfPCell cellbeca = new PdfPCell(new Paragraph("CALIFICACIÓN", fonttblacreditosasignaturas));
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

                PdfPCell celldescu = new PdfPCell(new Paragraph("MATERIA EXTERNA", fonttblacreditosasignaturas));
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

                PdfPTable tabladetalle = new PdfPTable(6);
                tabladetalle.DefaultCell.Border = Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;
                tabladetalle.SpacingBefore = 15f;
                tabladetalle.TotalWidth = 500;
                tabladetalle.WidthPercentage = 100;
                tabladetalle.LockedWidth = true;
                float[] widths1 = new float[] { 70f, 70f, 50F, 200f, 50f, 200f };
                tabladetalle.SetWidths(widths1);
                Font fontdatatable = new Font(Font.FontFamily.COURIER, 7, Font.BOLD);

                for (int w = 0; w < ds.Tables[0].Rows.Count; w++)
                {
                    CONSECUTIVO = ds.Tables[0].Rows[w][0].ToString();
                    AREA = ds.Tables[0].Rows[w][1].ToString();
                    CLAVE = ds.Tables[0].Rows[w][2].ToString();
                    MATERIA_I = ds.Tables[0].Rows[w][3].ToString();
                    MATERIA_E = ds.Tables[0].Rows[w][4].ToString();
                    CALIFICACION = ds.Tables[0].Rows[w][5].ToString();

                    PdfPCell celdaparcia = new PdfPCell(new Paragraph(CONSECUTIVO, fontdatatable));
                    celdaparcia.Border = 0;
                    celdaparcia.BackgroundColor = (BaseColor.WHITE);
                    celdaparcia.HorizontalAlignment = Element.ALIGN_CENTER;
                    tabladetalle.AddCell(celdaparcia);

                    PdfPCell celdaconcepto = new PdfPCell(new Paragraph(AREA, fontdatatable));
                    celdaconcepto.Border = 0;
                    celdaconcepto.BackgroundColor = (BaseColor.WHITE);
                    celdaconcepto.HorizontalAlignment = Element.ALIGN_LEFT;
                    tabladetalle.AddCell(celdaconcepto);

                    PdfPCell celdanombre = new PdfPCell(new Paragraph(CLAVE, fontdatatable));
                    celdanombre.Border = 0;
                    celdanombre.BackgroundColor = (BaseColor.WHITE);
                    celdanombre.HorizontalAlignment = Element.ALIGN_LEFT;
                    tabladetalle.AddCell(celdanombre);

                    PdfPCell celdaimporte = new PdfPCell(new Paragraph(MATERIA_I, fontdatatable));
                    celdaimporte.Border = 0;
                    celdaimporte.BackgroundColor = (BaseColor.WHITE);
                    celdaimporte.HorizontalAlignment = Element.ALIGN_LEFT;
                    tabladetalle.AddCell(celdaimporte);

                    PdfPCell celdabeca = new PdfPCell(new Paragraph(CALIFICACION, fontdatatable));
                    celdabeca.Border = 0;
                    celdabeca.BackgroundColor = (BaseColor.WHITE);
                    celdabeca.HorizontalAlignment = Element.ALIGN_CENTER;
                    tabladetalle.AddCell(celdabeca);

                    PdfPCell celdadescuento = new PdfPCell(new Paragraph(MATERIA_E, fontdatatable));
                    celdadescuento.Border = 0;
                    celdadescuento.BackgroundColor = (BaseColor.WHITE);
                    celdadescuento.HorizontalAlignment = Element.ALIGN_LEFT;
                    tabladetalle.AddCell(celdadescuento);

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



                doc.NewPage();


                doc.Add(table);
                doc.Add(tblTitulo);
                doc.Add(tblalumno);
                doc.Add(tblalumno1);
                doc.Add(tblTitulo1);
                doc.Add(tablaedocuenta);
                doc.Add(tabladetalle);
                doc.Add(linea);


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

                byte[] bytes = File.ReadAllBytes(path + "/" + "Predictamen_" + cuenta + ".pdf");
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
                    File.WriteAllBytes(path + "/" + "Predictamen_" + cuenta + ".pdf", bytes);
                    string path1 = Server.MapPath("PDF") + "\\" + "Predictamen_" + cuenta + ".pdf";
                    WebClient client = new WebClient();
                    Byte[] buffer = client.DownloadData(path1);
                    //  File.Delete(path + "\\" + userId + "1.pdf");
                    //  File.Delete(path + "\\" + userId + "2.pdf");
                    if (buffer != null)
                    {
                        Response.Clear();
                        Response.ContentType = "application/pdf";
                        Response.AddHeader("Content-Disposition", "attachment; filename=Predictamen.pdf");
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



