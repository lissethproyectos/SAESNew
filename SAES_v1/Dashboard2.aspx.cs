using SAES_Services;
using SAES_v1.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static SAES_DBO.Models.ModelGrafica;

namespace SAES_v1
{
    public partial class Dashboard2 : System.Web.UI.Page
    {
        #region <Variables>
        Utilidades utils = new Utilidades();
        // Usar los nombres de servicios estandarizados
        Catalogos_grales_Service serviceCatalogo = new Catalogos_grales_Service();
        GraficaService serviceGrafica = new GraficaService();
        
        public string labels_dashboard_1, data_dashboard_1, label_dashboard_1;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                Inicializar();

            // Esto mantiene el diseño de la tabla tras cada postback
            ScriptManager.RegisterStartupScript(this, this.GetType(), "GridDashboard", "if(typeof load_datatable === 'function') { load_datatable(); }", true);
        }

        private void Inicializar()
        {
            try {
                // Carga inicial de periodos usando el servicio de 4 capas
                ddl_periodo.DataSource = serviceCatalogo.QRY_TPEES_PERIODOS(""); 
                ddl_periodo.DataValueField = "Clave";
                ddl_periodo.DataTextField = "Periodo";
                ddl_periodo.DataBind();
                
                // Forzar la primera carga
                ddl_periodo_SelectedIndexChanged(null, null);
            }
            catch (Exception ex) {
                Global.inserta_log(ex.Message.Replace("'", "-"), "Dashboard2_Init", Session["usuario"]?.ToString());
            }
        }

        protected void ddl_periodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cargar campus basado en el periodo (o general)
            ddl_campus.DataSource = serviceCatalogo.QRY_TCAMP_COMBO();
            ddl_campus.DataValueField = "Clave";
            ddl_campus.DataTextField = "Campus";
            ddl_campus.DataBind();
            
            ddl_campus_SelectedIndexChanged(null, null);
        }

        protected void ddl_campus_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cargar niveles filtrados por campus
            string campusSel = ddl_campus.SelectedValue;
            ddl_nivel.DataSource = serviceCatalogo.QRY_TNIVE_COBRANZA(campusSel);
            ddl_nivel.DataValueField = "clave";
            ddl_nivel.DataTextField = "nivel";
            ddl_nivel.DataBind();
            
            dashboard_1();
        }

        private void dashboard_1()
        {
            try
            {
                string periodo = ddl_periodo.SelectedValue;
                string campus = ddl_campus.SelectedValue == "0" ? "" : ddl_campus.SelectedValue;
                string nivel = ddl_nivel.SelectedValue == "0" ? "" : ddl_nivel.SelectedValue;
                
                // Determinar el "tipo" de consulta para el SP
                string tipo = "1"; // Por defecto Periodo
                if (!string.IsNullOrEmpty(campus)) tipo = "2";
                if (!string.IsNullOrEmpty(nivel)) tipo = "3";

                var lstDatosGrafica = serviceGrafica.obtenerDatosGraficaAdeudo(tipo, periodo, campus, nivel);
                
                grvDatosGrafica.DataSource = lstDatosGrafica;
                grvDatosGrafica.DataBind();

                // Llamada al script de JS para renderizar la gráfica
                // Nota: Asegúrate de que GraficaAdeudos en JS maneje estos 4 parámetros
                string script = $"GraficaAdeudos('{tipo}', '{periodo}', '{campus}', '{nivel}');";
                ScriptManager.RegisterStartupScript(this, GetType(), "Grafica_Adeudos", script, true);
            }
            catch (Exception ex)
            {
                Global.inserta_log(ex.Message.Replace("'", "-"), "Dashboard2_Grafica", Session["usuario"]?.ToString());
            }
        }
        
        protected void grvDatosGrafica_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ruta = string.Empty;
            try
            {
                if (ddl_periodo.SelectedValue != "0" && ddl_campus.SelectedValue == "" && ddl_nivel.SelectedValue == "0")
                    ruta = "../Reports/VisualizadorCrystal.aspx?Tipo=RepDeudores1&Valor1=" + ddl_periodo.SelectedValue + "&Valor2=" + grvDatosGrafica.SelectedRow.Cells[5].Text + "&enExcel=N";
                else if (ddl_periodo.SelectedValue != "0" && ddl_campus.SelectedValue != "" && ddl_nivel.SelectedValue == "0")
                    ruta = "../Reports/VisualizadorCrystal.aspx?Tipo=RepDeudores2&Valor1=" + ddl_periodo.SelectedValue + "&Valor2=" + ddl_campus.SelectedValue + "&Valor3=" + grvDatosGrafica.SelectedRow.Cells[5].Text + "&enExcel=N";
                else
                    ruta = "../Reports/VisualizadorCrystal.aspx?Tipo=RepDeudores3&Valor1=" + ddl_periodo.SelectedValue + "&Valor2=" + ddl_campus.SelectedValue + "&Valor3=" + ddl_nivel.SelectedValue + "&Valor4=" + grvDatosGrafica.SelectedRow.Cells[5].Text + "&enExcel=N";

                string _open = "window.open('" + ruta + "', '_newtab');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), _open, true);
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tpees", Session["usuario"].ToString());
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);

            }
        }
    }
}