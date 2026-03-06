using SAES_Services;
using SAES_v1.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SAES_v1
{
        
    public partial class ttiop : System.Web.UI.Page
    {
        Utilidades utils = new Utilidades();
        Catalogos catalogos = new Catalogos();
        CatOpcionesTitulacion Model = new CatOpcionesTitulacion();                       

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
                    CargaInicial();
                }
            }
        }

        public void CargaInicial()
        {
            txb_claveTitulacion.Text = "";
            txb_descripcion.Text = "";
            ddl_estatus = utils.BeginDropdownList(ddl_estatus, catalogos.obtenEstatusCatOpcionesTitulacion());

            Gridttiop.Visible = true;
            Gridttiop = utils.BeginGrid(Gridttiop, Model.ObtenOpcionesTitulacion());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "generic_datatable_load", "generic_datatable_load('Gridttiop');", true);

            GridttiopDet.DataSource = null;
            GridttiopDet.DataBind();
        }               

        protected void Gridttiop_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = Gridttiop.SelectedRow;
                string claveTitulacion = row.Cells[1].Text;
                string descripcion = row.Cells[2].Text;
                string estatus = row.Cells[3].Text;

                txb_claveTitulacion.Text = claveTitulacion;
                txb_descripcion.Text = descripcion;
                ddl_estatus.SelectedValue = estatus;

                DataTable dtDet = Model.ObtenOpcionesTitulacionDetalle(claveTitulacion);
                GridttiopDet = utils.BeginGrid(GridttiopDet, dtDet);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "generic_datatable_load", "generic_datatable_load('GridttiopDet');", true);

                DataTable dtNiveles = new DataTable();
                dtNiveles = Model.ObtenOpcionesTitulacionNiveles();

                DataTable dtCodigos = new DataTable();
                dtCodigos = Model.ObtenOpcionesTitulacionCodigos();

                Gridttiop.Visible = false;

                int i = 0;
                foreach (GridViewRow rowg in GridttiopDet.Rows)
                {
                    string Nivel = dtDet.Rows[i]["Nivel"].ToString();
                    var dropdownNivel = (DropDownList)rowg.FindControl("DDL_Nivel");
                    dropdownNivel.DataSource = dtNiveles;
                    dropdownNivel.DataValueField = "Clave";
                    dropdownNivel.DataTextField = "Descripcion";
                    dropdownNivel.SelectedValue = Nivel;
                    dropdownNivel.DataBind();

                    string creditos = dtDet.Rows[i]["Creditos"].ToString();
                    var txbCreditos = (TextBox)rowg.FindControl("txb_Creditos");
                    txbCreditos.Text = creditos;

                    string promedio = dtDet.Rows[i]["Promedio"].ToString();
                    var txbPromedio = (TextBox)rowg.FindControl("txb_Promedio");
                    txbPromedio.Text = promedio;

                    string Codigo = dtDet.Rows[i]["Codigo"].ToString();
                    var dropdownCodigos = (DropDownList)rowg.FindControl("DDL_Codigo");
                    dropdownCodigos.DataSource = dtCodigos;
                    dropdownCodigos.DataValueField = "Clave";
                    dropdownCodigos.DataTextField = "Descripcion";
                    dropdownCodigos.SelectedValue = Codigo;
                    try
                    {
                        dropdownCodigos.DataBind();
                    }
                    catch (Exception)
                    {
                    }
                    
                    i++;
                }                                
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "ttiop", Session["usuario"].ToString(), ex.StackTrace);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            try
            {
                CargaInicial();
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "ttiop", Session["usuario"].ToString(), ex.StackTrace);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
            }
        }

        protected void btn_update_Click(object sender, EventArgs e)
        {
            try
            {
                bool isFirst = true;
                foreach (GridViewRow row in GridttiopDet.Rows)
                {
                    string claveTitulacion = txb_claveTitulacion.Text;
                    DropDownList dropdownCodigos = (DropDownList)row.FindControl("DDL_Codigo");
                    string codigos = dropdownCodigos.SelectedValue;
                    var txbCreditos = (TextBox)row.FindControl("txb_Creditos");
                    string creditos = txbCreditos.Text;
                    string descripcion = txb_descripcion.Text;
                    string estatus = ddl_estatus.SelectedValue;
                    DropDownList nivelddl = (DropDownList)row.FindControl("DDL_Nivel");
                    string nivel = nivelddl.SelectedValue;
                    TextBox txbPromedio = (TextBox)row.FindControl("txb_Promedio");
                    string promedio = txbPromedio.Text;
                    Model.BorraOpcionesTitulacion(claveTitulacion);

                    if (isFirst)
                        Model.ActualizaEncabezadoOpcionesTitulacion(claveTitulacion, codigos, creditos, descripcion, estatus, nivel, promedio, Session["usuario"].ToString());

                    isFirst = false;
                    Model.ActualizaDetalleOpcionesTitulacion(claveTitulacion, codigos, creditos, nivel, promedio, Session["usuario"].ToString());
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_success", "alert_success('Se actualizaron los datos exitosamente');", true);
                CargaInicial();
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "ttiop", Session["usuario"].ToString(), ex.StackTrace);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
            }
        }

        protected void btn_addrow_Click(object sender, EventArgs e)
        {
            try
            {
                Session["oldData"] = GridttiopDet.DataSource;

                DataTable tbl = (DataTable)Session["oldData"];

                if (tbl.Columns.Count == 0)
                {
                    tbl.Columns.Add("PayScale", typeof(string));
                    tbl.Columns.Add("IncrementAmt", typeof(string));
                    tbl.Columns.Add("Period", typeof(string));
                }

                DataRow NewRow = tbl.NewRow();
                tbl.Rows.Add(NewRow);
                GridttiopDet.DataSource = tbl;
                GridttiopDet.DataBind();
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "ttiop", Session["usuario"].ToString(), ex.StackTrace);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
            }
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {

        }
    }
}