using SAES_Services;
using SAES_v1.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static SAES_DBO.Models.ModelGrafica;

namespace SAES_v1
{
    public partial class tacxc : System.Web.UI.Page
    {
        #region <Variables>
        Utilidades utils = new Utilidades();
        Catalogos serviceCatalogo = new Catalogos();
        GraficaService serviceGrafica = new GraficaService();
        public string labels_dashboard_1;
        public string data_dashboard_1;
        public string label_dashboard_1;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                Inicializar();

        }

        private void Inicializar()
        {
            ddl_periodo.DataSource = serviceCatalogo.ObtenerPeriodosEscolares();
            ddl_periodo.DataValueField = "clave";
            ddl_periodo.DataTextField = "nombre";
            ddl_periodo.DataBind();
            //ddl_periodo.SelectedValue = "202065";
            ddl_periodo_SelectedIndexChanged(null, null);
        }
        protected void ddl_periodo_SelectedIndexChanged(object sender, EventArgs e)
        {

            ddl_campus.DataSource = serviceCatalogo.ObtenerCampus();
            ddl_campus.DataValueField = "Clave";
            ddl_campus.DataTextField = "Descripcion";
            ddl_campus.DataBind();
            ddl_campus.Items.Insert(0, new ListItem("--Seleccione--", "0"));
            ddl_campus_SelectedIndexChanged(null, null);
            dashboard_1();
        }

        protected void ddl_campus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddl_nivel.DataSource = serviceCatalogo.obtenNivel(ddl_campus.SelectedValue);
            ddl_nivel.DataValueField = "Clave";
            ddl_nivel.DataTextField = "Descripcion";
            ddl_nivel.DataBind();
            //ddl_nivel.Items.Insert(0, new ListItem("--Seleccione--", "0"));
            dashboard_1();
        }

        private void dashboard_1()
        {
            grvDatosGrafica.Columns[1].Visible = true;

            List<ModelObtenGraficaCtasporCobrarResponse> lstDatosGrafica = new List<ModelObtenGraficaCtasporCobrarResponse>();
            try
            {
                
                if (ddl_campus.SelectedValue == "0" && ddl_nivel.SelectedValue == "")
                {
                    grvDatosGrafica.DataBind();
                    lstDatosGrafica = serviceGrafica.obtenerDatosGraficaCuentasporCobrar("GRAFICA_1", ddl_tipo.SelectedValue, ddl_periodo.SelectedValue, "", "", "");
                    grvDatosGrafica.DataSource = lstDatosGrafica;
                    grvDatosGrafica.DataBind();
                    grvDatosGrafica.Columns[1].Visible = false;
                    ScriptManager.RegisterStartupScript(this, GetType(), "CuentaporCobrar", "GraficaCXC('GRAFICA_1', '" + ddl_tipo.SelectedValue + "','" + ddl_periodo.SelectedValue + "', '" + ddl_campus.SelectedValue + "','','');", true);
                }

                else if (ddl_campus.SelectedValue != "0" && ddl_nivel.SelectedValue == "")
                {
                    grvDatosGrafica.DataBind();
                    lstDatosGrafica = serviceGrafica.obtenerDatosGraficaCuentasporCobrar("GRAFICA_2", ddl_tipo.SelectedValue, ddl_periodo.SelectedValue, ddl_campus.SelectedValue, "", "");
                    grvDatosGrafica.DataSource = lstDatosGrafica;
                    grvDatosGrafica.DataBind();
                    grvDatosGrafica.Columns[1].Visible = false;
                    ScriptManager.RegisterStartupScript(this, GetType(), "CuentaporCobrar", "GraficaCXC('GRAFICA_2', '" + ddl_tipo.SelectedValue + "','" + ddl_periodo.SelectedValue + "', '" + ddl_campus.SelectedValue + "','','');", true);
                }
                else
                {
                    grvDatosGrafica.DataBind();
                    lstDatosGrafica = serviceGrafica.obtenerDatosGraficaCuentasporCobrar("GRAFICA_3", ddl_tipo.SelectedValue, ddl_periodo.SelectedValue, ddl_campus.SelectedValue, ddl_nivel.SelectedValue, "");
                    grvDatosGrafica.DataSource = lstDatosGrafica;
                    grvDatosGrafica.DataBind();
                    grvDatosGrafica.Columns[1].Visible = true;
                    ScriptManager.RegisterStartupScript(this, GetType(), "CuentaporCobrar", "GraficaCXC('GRAFICA_3', '" + ddl_tipo.SelectedValue + "','" + ddl_periodo.SelectedValue + "', '" + ddl_campus.SelectedValue + "','" + ddl_nivel.SelectedValue + "','');", true);

                }

            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tpees", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);

            }
        }
        protected void ddl_nivel_SelectedIndexChanged(object sender, EventArgs e)
        {
            dashboard_1();
        }
        protected void ddl_tipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            dashboard_1();
        }
    }
}