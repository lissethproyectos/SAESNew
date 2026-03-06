using SAES_DBO.Models;
using SAES_Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SAES_v1
{
    public partial class tpago : System.Web.UI.Page
    {
        #region <Variables>
        //Utilidades utils = new Utilidades();
        CobranzaService serviceCobranza = new CobranzaService();
        List<ModelObtenPaisesResponse> lstPaises = new List<ModelObtenPaisesResponse>();
        Catalogos_grales_Service serviceCatalogo = new Catalogos_grales_Service();
        Catalogos serviceCatalogo2 = new Catalogos();
        BancoService serviceBanco = new BancoService();
        public static double reporte = 0;

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txt_matricula.Text = Global.cuenta;
                txt_alumno.Text = Global.nombre_alumno;
                //txt_matricula.Focus();
                Gridtpago.DataSource = null;
                Gridtpago.DataBind();
                grid_pagos();
            }

            //ScriptManager.RegisterStartupScript(this, this.GetType(), "GridALumnos", "load_datatable_alumnos();", true);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "ctrl_fecha_b", "ctrl_fecha_b();", true);


        }
        protected void grid_pagos()
        {
            Gridtpago.DataSource = serviceCobranza.ObtenerTPago(Global.cuenta,Global.programa);// ("1", "202065")  
            Gridtpago.DataBind();
        }

        protected void linkRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("tedcu.aspx");
        }

        protected void Gridtpago_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            LinkButton linkBttn = e.Row.FindControl("linkBttnRecibo") as LinkButton;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[5].Text=="0")
                    linkBttn.Visible = false;
                //Label lblTot = (Label)GridTcedc_Det.FooterRow.FindControl("lblTotNumPago");
            }
        }

        protected void Gridtpago_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string server = Server.MapPath("");
                string ruta = "../Reports/VisualizadorCrystal.aspx?Tipo=RepRecibo&Valor1=" + Global.campus + "&Valor2=" + Global.cuenta + "&Valor3=" + Gridtpago.SelectedRow.Cells[2].Text;
                string _open = "window.open('" + ruta + "', '_black');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), _open, true);

            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tedcu", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                //Response.Redirect("Inicio.aspx");
            }
        }
    }
}