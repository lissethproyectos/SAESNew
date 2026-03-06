using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Web;
using CrystalDecisions.ReportSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SAES_v1.Reports
{
    public partial class VisualizadorCrystal : System.Web.UI.Page
    {
        private ReportDocument report = new ReportDocument();
        private System.Web.UI.Page p;
        ConnectionInfo connectionInfo = new ConnectionInfo();
        string Tipo;
        String Reporte = "";
        object[] campos;
        protected void Page_Load(object sender, EventArgs e)
        {
            rptVisor();
        }
        private void rptVisor()
        {
            try
            {

                string Tipo = Convert.ToString(Request.QueryString["Tipo"]);
                string Valor1 = Convert.ToString(Request.QueryString["Valor1"]);
                string Valor2 = Convert.ToString(Request.QueryString["Valor2"]);
                string Valor3 = Convert.ToString(Request.QueryString["Valor3"]);
                string Valor4 = Convert.ToString(Request.QueryString["Valor4"]);
                string Valor5 = Convert.ToString(Request.QueryString["Valor5"]);
                string Valor6 = Convert.ToString(Request.QueryString["Valor6"]);
                string Valor7 = Convert.ToString(Request.QueryString["Valor7"]);

                string caseSwitch = Tipo;
                switch (caseSwitch)
                {
                    case "RepCatUsuarios":
                        Reporte = "Reports\\RepCatUsuarios.rpt";
                        reportes_dir();                        
                        reporte_PDF(Reporte, campos, "Catalogo de usuarios");
                        break;
                    case "RepCatUsuariosExc":
                        Reporte = "Reports\\RepCatUsuarios.rpt";
                        reportes_dir();
                        reporte_Excel(Reporte, campos, "Catalogo de usuarios");
                        break;
                    case "RepDeudores1":
                        Reporte = "Reports\\RepDeudores1.rpt";
                        reportes_dir();
                        campos = new[] { Valor1, Valor2 };
                        reporte_PDF(Reporte, campos, "Deudores");
                        break;
                    case "RepDeudores2":
                        Reporte = "Reports\\RepDeudores2.rpt";
                        reportes_dir();
                        campos = new[] { Valor1, Valor2, Valor3 };
                        reporte_PDF(Reporte, campos, "Deudores");
                        break;
                    case "RepDeudores3":
                        Reporte = "Reports\\RepDeudores3.rpt";
                        reportes_dir();
                        campos = new []{ Valor1, Valor2, Valor3, Valor4 };
                        reporte_PDF(Reporte, campos, "Deudores");
                        break;
                    case "RepBoleta":
                        Reporte = "Reports\\RepBoleta.rpt";
                        reportes_dir();
                        campos = new[] { Valor1, Valor2, Valor3, Valor4, Valor5 };
                        reporte_PDF(Reporte, campos, "Boleta");
                        break;
                    case "RepHistAcad":
                        Reporte = "Reports\\RepHistorialAcademico.rpt";
                        reportes_dir();
                        campos = new[] { Valor1, Valor2 };
                        reporte_PDF(Reporte, campos, "Historial Académico");
                        break;
                    case "RepAvanceAcad":
                        Reporte = "Reports\\RepAvanceAcad.rpt";
                        reportes_dir();
                        campos = new[] { Valor1, Valor2 };
                        reporte_PDF(Reporte, campos, "Avance Académico");
                        break;
                    case "RepFichaBancaria":
                        Reporte = "Reports\\RepFichaBancaria.rpt";
                        reportes_dir();
                        string ruta = Server.MapPath("~/");

                        campos = new[] { Valor1, Valor2, Valor3, ruta, Valor4 };
                        reporte_PDF(Reporte, campos, "Ficha Bancaria");
                        break;
                    case "RepRecibo":
                        Reporte = "Reports\\RepRecibo.rpt";
                        reportes_dir();
                        campos = new[] { Valor1, Valor2, Valor3 };
                        reporte_PDF(Reporte, campos, "Recibo");
                        break;
                    case "RepProgAlumnos":
                        Reporte = "Reports\\RepProgAlumnos.rpt";
                        reportes_dir();
                        campos = new[] { Valor1, Valor2, Valor3 };
                        reporte_PDF(Reporte, campos, "Programa Alumnos");
                        break;
                    case "RepProgAlumnos_Excel":
                        Reporte = "Reports\\RepProgAlumnos_Excel.rpt";
                        reportes_dir();
                        campos = new[] { Valor1, Valor2, Valor3 };
                        reporte_Excel(Reporte, campos, "Programa Alumnos");
                        break;
                    case "RepBecas_Descuentos":
                        Reporte = "Reports\\RepDescuentos.rpt";
                        reportes_dir();
                        campos = new[] { Valor1, Valor2, Valor3, Valor4, Valor5, Valor6, Valor7 };
                        reporte_PDF(Reporte, campos, "Becas/Descuentos Alumnos");
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "loader_false();", true);
                        break;
                    case "RepRegistroTitulacion":
                        Reporte = "Reports\\RepRegistroTitulacion.rpt";
                        reportes_dir();
                        campos = new[] { Valor1, Valor2, Valor3, Valor4, Valor5, Valor6 };
                        reporte_PDF(Reporte, campos, "Registro de Titulacion");
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "loader_false();", true);
                        break;
                    case "RepRegistroSS":
                        Reporte = "Reports\\RepRegistrosSS.rpt";
                        reportes_dir();
                        campos = new[] { Valor1, Valor2, Valor3, Valor4, Valor5, Valor6 };
                        reporte_PDF(Reporte, campos, "Registro de Servicio Social");
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "loader_false();", true);
                        break;
                        
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                report.Close();
                report.Dispose();
                CR_Reportes.Dispose();
                GC.Collect();
            }
        }
        private void reporte_PDF(String Reporte, object[] Parametros, string NombreReporte)
        {
            ConnectionInfo connectionInfo = new ConnectionInfo();
            p = new System.Web.UI.Page();
            report.Load(p.Server.MapPath("~") + "\\" + Reporte);

            if (Parametros!=null)
                for (int i = 0; i <= Parametros.Length - 1; i++)
                    report.SetParameterValue(i, Parametros[i]);

            report.PrintOptions.PaperSize = PaperSize.PaperLetter;
            connectionInfo.ServerName = "localhost";             // No debe decir "BaseSAES_local" si el servidor es localhost
            connectionInfo.DatabaseName = "db_a8b622_saesd"; 
            connectionInfo.UserID = "a8b622_saesd";
            connectionInfo.Password = "saesdev01";
            SetDBLogonForReport(connectionInfo, report);

            report.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, Tipo);
            CR_Reportes.ReportSource = report;

        }

        private void reporte_Excel(String Reporte, object[] Parametros, string NombreReporte)
        {
            try
            {

                ConnectionInfo connectionInfo = new ConnectionInfo();
                p = new System.Web.UI.Page();
                report.Load(p.Server.MapPath("~") + "\\" + Reporte);

                if (Parametros != null)
                    for (int i = 0; i <= Parametros.Length - 1; i++)
                        report.SetParameterValue(i, Parametros[i]);

                report.PrintOptions.PaperSize = PaperSize.PaperLetter;
                connectionInfo.ServerName = "BaseSAES_local";
                connectionInfo.UserID = "a8b622_saesd";
                connectionInfo.Password = "saesdev01";
                SetDBLogonForReport(connectionInfo, report);
                report.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, false, NombreReporte);


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CR_Reportes.ReportSource = report;
                report.Close();
                report.Dispose();
                CR_Reportes.Dispose();
            }
        }

        private void reportes_dir()
        {
            p = new System.Web.UI.Page();
            string Ruta = p.Server.MapPath("~") + "\\" + Reporte;
            report.Load(p.Server.MapPath("~") + "\\" + Reporte);

        }
        private void SetDBLogonForReport(ConnectionInfo connectionInfo, ReportDocument reportDocument)
        {
            try
            {
                Tables tables = reportDocument.Database.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table table in tables)
                {
                    TableLogOnInfo tableLogonInfo = table.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    table.ApplyLogOnInfo(tableLogonInfo);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}