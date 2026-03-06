using SAES_Services;
using SAES_v1.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static SAES_DBO.Models.ModelCobranza;

namespace SAES_v1
{
    public partial class tcpcb : System.Web.UI.Page
    {
        #region <Variables>
        Utilidades utils = new Utilidades();
        Catalogos serviceCatalogo = new Catalogos();
        GraficaService serviceGrafica = new GraficaService();
        CobranzaService serviceCobro = new CobranzaService();
        public string labels_dashboard_1;
        public string data_dashboard_1;
        public string label_dashboard_1;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                Inicializar();


            //ScriptManager.RegisterStartupScript(this, this.GetType(), "GridTcp", "load_datatable();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Grid", "load_datatable2();", true);

        }
        private void Inicializar()
        {
            List<ModelObtenerTcpbcResponse> lstTcpcb = new List<ModelObtenerTcpbcResponse>();
            ddl_periodo.DataSource = null;
            ddl_periodo.DataBind();

            ddl_campus.DataSource = null;
            ddl_campus.DataBind();


            ddl_nivel.DataSource = null;
            ddl_nivel.DataBind();

            ddl_periodo_new.DataSource = serviceCatalogo.ObtenerPeriodosEscolaresVigentes(); // ObtenerPeriodosEscolares();
            ddl_periodo_new.DataValueField = "clave";
            ddl_periodo_new.DataTextField = "nombre";
            ddl_periodo_new.DataBind();

            ddl_periodo.DataSource = serviceCatalogo.ObtenerPeriodosEscolaresVigentes(); // ObtenerPeriodosEscolares();
            ddl_periodo.DataValueField = "clave";
            ddl_periodo.DataTextField = "nombre";
            ddl_periodo.DataBind();
            ddl_periodo_SelectedIndexChanged(null, null);

            GridTcpcb.DataSource = lstTcpcb;
            GridTcpcb.DataBind();
        }

        protected void ddl_periodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddl_campus.DataSource = serviceCatalogo.ObtenerCampus();
                ddl_campus.DataValueField = "Clave";
                ddl_campus.DataTextField = "Descripcion";
                ddl_campus.DataBind();
                ddl_campus_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                //Logs
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcpcb", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void ddl_campus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddl_nivel.DataSource = serviceCatalogo.obtenNivel(ddl_campus.SelectedValue);
            ddl_nivel.DataValueField = "Clave";
            ddl_nivel.DataTextField = "Descripcion";
            ddl_nivel.DataBind();
            ddl_nivel_SelectedIndexChanged(null, null);
        }

        protected void ddl_nivel_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddl_nivel.SelectedValue != "")
                {
                    ddl_programa.DataSource = serviceCatalogo.obtenProgramaVigente(ddl_campus.SelectedValue, ddl_nivel.SelectedValue);
                    ddl_programa.DataValueField = "Clave";
                    ddl_programa.DataTextField = "Descripcion";
                    ddl_programa.DataBind();
                    //grid_bind_tcpcb();

                }
                //else
                //{
                //    GridTcpcb.DataSource = null;
                //    GridTcpcb.DataBind();
                //    //grid_bind_tcpcb();

                //}

                //ddl_programa.DataSource = serviceCatalogo.obtenProgramaVigente(ddl_campus.SelectedValue, ddl_nivel.SelectedValue);
                //ddl_programa.DataValueField = "Clave";
                //ddl_programa.DataTextField = "Descripcion";
                //ddl_programa.DataBind();

                //ddl_programa_SelectedIndexChanged(null, null);

            }
            catch (Exception ex)
            {
                //Logs
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcpcb", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }



        protected void buscar(object sender, EventArgs e)
        {
            grid_bind_tcpcb();
        }



        protected void grid_bind_tcpcb()
        {
            try
            {
                GridTcpcb.DataSource = serviceCobro.ObtenerDatosTcpcb(ddl_periodo.SelectedValue, ddl_campus.SelectedValue, ddl_nivel.SelectedValue,
                    ddl_programa.SelectedValue, ddl_tipo_plan.SelectedValue, ddl_estatus.SelectedValue);// ("1", "202065")  
                GridTcpcb.DataBind();

            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcpcb", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void linkBttnRolarDescuentos_Click(object sender, EventArgs e)
        {
            try
            {
                var checkedRows = from GridViewRow msgRow in GridTcpcb.Rows
                                  where ((CheckBox)msgRow.Cells[0].FindControl("chkRolDescuento")).Checked
                                  select (int)GridTcpcb.DataKeys[msgRow.RowIndex].Value;

                foreach (GridViewRow row in GridTcpcb.Rows)
                {
                    var check = (CheckBox)row.Cells[0].FindControl("chkRolDescuento");
                    if (check.Checked == true)
                    {
                        serviceCobro.AplicarBecas(ddl_periodo.SelectedValue, row.Cells[5].Text, row.Cells[3].Text, row.Cells[8].Text, Session["usuario"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcpcb", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void linkBttnAplicar_Click(object sender, EventArgs e)
        {
            int total = 0;
            try
            {
                var checkedRows = from GridViewRow msgRow in GridTcpcb.Rows
                                  where ((CheckBox)msgRow.Cells[0].FindControl("chkRolDescuento")).Checked
                                  select (int)GridTcpcb.DataKeys[msgRow.RowIndex].Value;

                foreach (GridViewRow row in GridTcpcb.Rows)
                {
                    var check = (CheckBox)row.Cells[0].FindControl("chkRolDescuento");
                    if (check.Checked == true)
                    {
                        total = total + 1;
                        serviceCobro.AplicarBecas(ddl_periodo_new.SelectedValue, row.Cells[5].Text, row.Cells[3].Text, row.Cells[8].Text, Session["usuario"].ToString());
                    }
                }

                if (total > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPeriodo", "$('#modalPeriodoNuevo').modal('hide')", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Aplicar", "aplicar();", true);
                    grid_bind_tcpcb();
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_becas_seleccionadas", "error_becas_seleccionadas();", true);


            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcpcb", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }
    }
}