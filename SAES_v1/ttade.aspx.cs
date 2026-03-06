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
    public partial class ttade : System.Web.UI.Page
    {
        #region <Variables>
        Utilidades utils = new Utilidades();
        Catalogos serviceCatalogo = new Catalogos();
        GraficaService serviceGrafica = new GraficaService();
        // ModelObtenGraficaBecasResponse lstDatosGrafica = new ModelObtenGraficaBecasResponse();
        public string labels_dashboard_1;
        public string data_dashboard_1;
        public string label_dashboard_1;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                Inicializar();


                ScriptManager.RegisterStartupScript(this, this.GetType(), "Inicializar", "Inicializar();", true);
            }
        }
        private void Inicializar()
        {

            ddl_periodo.DataSource = serviceCatalogo.ObtenerPeriodosEscolares();
            ddl_periodo.DataValueField = "Clave";
            ddl_periodo.DataTextField = "Nombre";
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
            List<ModelObtenGraficaBecasResponse> lstDatosGrafica = new List<ModelObtenGraficaBecasResponse>();
            grvDatosGrafica.Columns[1].Visible = false;
            if (ddl_periodo.SelectedValue != "0" && ddl_campus.SelectedValue != "0" && ddl_nivel.SelectedValue != string.Empty)
            {
                grvDatosGrafica.Columns[1].Visible = true;
                lstDatosGrafica = serviceGrafica.obtenerDatosGraficaBecas("GRAFICA_3", ddl_tipo.SelectedValue, ddl_periodo.SelectedValue, ddl_campus.SelectedValue, ddl_nivel.SelectedValue, "");
                grvDatosGrafica.DataSource = lstDatosGrafica;
                grvDatosGrafica.DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "GraficaBecas", "GraficaBecas('GRAFICA_3', '" + ddl_tipo.SelectedValue + "','" + ddl_periodo.SelectedValue + "', '" + ddl_campus.SelectedValue + "','" + ddl_nivel.SelectedValue + "','');", true);
            }

            else if (ddl_periodo.SelectedValue!= "0" && ddl_campus.SelectedValue == "0")
            {
                lstDatosGrafica = serviceGrafica.obtenerDatosGraficaBecas("GRAFICA_1", ddl_tipo.SelectedValue, ddl_periodo.SelectedValue, ddl_campus.SelectedValue, ddl_nivel.SelectedValue, "");
                grvDatosGrafica.DataSource = lstDatosGrafica;
                grvDatosGrafica.DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "GraficaBecas", "GraficaBecas('GRAFICA_1', '" + ddl_tipo.SelectedValue + "','" + ddl_periodo.SelectedValue + "', '" + ddl_campus.SelectedValue + "','"+ddl_nivel.SelectedValue+"','');", true);
            }
            else
            {
                lstDatosGrafica = serviceGrafica.obtenerDatosGraficaBecas("GRAFICA_2", ddl_tipo.SelectedValue, ddl_periodo.SelectedValue, ddl_campus.SelectedValue, "", "");
                grvDatosGrafica.DataSource = lstDatosGrafica;
                grvDatosGrafica.DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "GraficaBecas", "GraficaBecas('GRAFICA_2', '" + ddl_tipo.SelectedValue + "','" + ddl_periodo.SelectedValue + "', '" + ddl_campus.SelectedValue + "','','');", true);
            }
        }

        protected void ddl_nivel_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Gridtcv.Visible = false;
            dashboard_1();
        }
    }
}