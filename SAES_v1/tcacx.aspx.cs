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
    public partial class tcacx : System.Web.UI.Page
    {
        Utilidades utils = new Utilidades();
        Catalogos catalogos = new Catalogos();
        CalificacionPorComponentes calificacionPorComponentes = new CalificacionPorComponentes();
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
                    Gridtcacx = utils.ClearGridView(Gridtcacx);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "generic_datatable_load", "generic_datatable_load('Gridtcacx');", true);
                }
            }
        }

        public void CargaInicial()
        {
            btn_update.Visible = false;

            ddl_periodo.Items.Clear();
            ddl_campus.Items.Clear();
            ddl_materia.Items.Clear();
            ddl_grupo.Items.Clear();
            ddl_componente.Items.Clear();

            ddl_periodo = utils.BeginDropdownList(ddl_periodo, catalogos.obtenPeriodo());
            ddl_campus = utils.BeginDropdownList(ddl_campus);
            ddl_materia = utils.BeginDropdownList(ddl_materia);
            ddl_grupo = utils.BeginDropdownList(ddl_grupo);
            ddl_componente = utils.BeginDropdownList(ddl_componente);
        }

        protected void ddl_periodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string usuario = Session["usuario"].ToString();
                string periodo = (ddl_periodo.SelectedValue == "") ? null : ddl_periodo.SelectedValue;
                if (!string.IsNullOrEmpty(periodo))
                {
                    ddl_campus = utils.BeginDropdownList(ddl_campus, catalogos.obtenCampusDocente(usuario, periodo));
                    ddl_materia = utils.BeginDropdownList(ddl_materia, catalogos.obtenMateriaDocente(usuario, null, periodo));
                    ddl_grupo = utils.BeginDropdownList(ddl_grupo, catalogos.obtenGrupoDocente(usuario, null, null, periodo));
                    btn_update.Visible = true;
                }
                else
                {
                    Gridtcacx = utils.ClearGridView(Gridtcacx);
                }
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcdoc", Session["usuario"].ToString(), ex.StackTrace);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('"+ mensaje_error + "');", true);
            }
        }

        protected void ddl_campus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string usuario = Session["usuario"].ToString();
                string campus = (ddl_campus.SelectedValue == "") ? null : ddl_campus.SelectedValue;
                string periodo = (ddl_periodo.SelectedValue == "") ? null : ddl_periodo.SelectedValue;
                if (!string.IsNullOrEmpty(periodo))
                {
                    ddl_materia = utils.BeginDropdownList(ddl_materia, catalogos.obtenMateriaDocente(usuario, campus, periodo));
                }
                else
                {
                    Gridtcacx = utils.ClearGridView(Gridtcacx);
                }
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcdoc", Session["usuario"].ToString(), ex.StackTrace);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
            }
        }

        protected void ddl_materia_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string usuario = Session["usuario"].ToString();
                string campus = (ddl_campus.SelectedValue == "") ? null : ddl_campus.SelectedValue;
                string materia = (ddl_materia.SelectedValue == "") ? null : ddl_materia.SelectedValue;
                string periodo = (ddl_periodo.SelectedValue == "") ? null : ddl_periodo.SelectedValue;
                if (!string.IsNullOrEmpty(periodo))
                {
                    ddl_grupo = utils.BeginDropdownList(ddl_grupo, catalogos.obtenGrupoDocente(usuario, campus, materia, periodo));
                    ddl_componente = utils.BeginDropdownList(ddl_componente, catalogos.obtenComponente(materia));
                }
                else
                {
                    Gridtcacx = utils.ClearGridView(Gridtcacx);
                }
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcdoc", Session["usuario"].ToString(), ex.StackTrace);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
            }
        }

        protected void ddl_grupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string usuario = Session["usuario"].ToString();
                string campus = (ddl_campus.SelectedValue == "") ? null : ddl_campus.SelectedValue;
                string materia = (ddl_materia.SelectedValue == "") ? null : ddl_materia.SelectedValue;
                string periodo = (ddl_periodo.SelectedValue == "") ? null : ddl_periodo.SelectedValue;
                string grupo = (ddl_grupo.SelectedValue == "") ? null : ddl_grupo.SelectedValue;
                if (!string.IsNullOrEmpty(periodo))
                {
                    
                }
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcdoc", Session["usuario"].ToString(), ex.StackTrace);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
            }
        }

        protected void ddl_componente_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string usuario = Session["usuario"].ToString();
                string campus = (ddl_campus.SelectedValue == "") ? null : ddl_campus.SelectedValue;
                string materia = (ddl_materia.SelectedValue == "") ? null : ddl_materia.SelectedValue;
                string periodo = (ddl_periodo.SelectedValue == "") ? null : ddl_periodo.SelectedValue;
                string grupo = (ddl_grupo.SelectedValue == "") ? null : ddl_grupo.SelectedValue;
                string componente = (ddl_componente.SelectedValue == "") ? null : ddl_componente.SelectedValue;
                if (!string.IsNullOrEmpty(periodo) && !string.IsNullOrEmpty(componente))
                {
                    DataTable dt = calificacionPorComponentes.obtenCalificacionPorComponentes(periodo, campus, materia, grupo, componente);
                    List<Calificaciones> liCalificaciones = getCatalogsCalifications(dt);
                    Gridtcacx = utils.BeginGrid2(Gridtcacx, dt);
                    prepareDLLsCalificaciones(Gridtcacx, dt, liCalificaciones);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "generic_datatable_load", "generic_datatable_load('Gridtcacx');", true);
                }
                else
                {
                    Gridtcacx = utils.ClearGridView(Gridtcacx);
                }
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcdoc", Session["usuario"].ToString(), ex.StackTrace);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
            }
        }
        private List<Calificaciones> getCatalogsCalifications(DataTable grid)
        {
            List<Calificaciones> liCalificaciones = new List<Calificaciones>();
            if (grid.Rows.Count > 0)
            {
                List<string> Programas = grid.Rows.Cast<DataRow>().Select(row => row["Clave"].ToString()).Distinct().ToList();
                foreach (string programa in Programas)
                {
                    DataTable cat = catalogos.obtenCalificaciones(programa);
                    liCalificaciones.Add(new Calificaciones() { programa = programa, soruce = cat });
                }
            }
            return liCalificaciones;
        }
        private GridView prepareDLLsCalificaciones(GridView grid, DataTable dataTable, List<Calificaciones> liCalificaciones)
        {
            foreach (GridViewRow row in grid.Rows)
            {
                string CalificaionDT = dataTable.Rows.Cast<DataRow>().Where(x => x["Matricula"].ToString() == row.Cells[0].Text).Select(rowd => rowd["Calificacion"].ToString()).FirstOrDefault();
                CalificaionDT = (!CalificaionDT.Contains(".00")) ? CalificaionDT.Replace(".0",".00") : CalificaionDT;
                string programaRow = row.Cells[2].Text;
                Calificaciones calificaciones = liCalificaciones.Where(x => x.programa == programaRow).FirstOrDefault();
                var dropdown = (DropDownList)row.FindControl("DDL_Calificacion");
                dropdown.DataSource = calificaciones.soruce;
                dropdown.DataValueField = "Clave";
                dropdown.DataTextField = "Descripcion";
               // dropdown.SelectedValue = (CalificaionDT =="")? "0.00" : CalificaionDT;
                dropdown.DataBind();
            }
            
            return grid;
        }

        protected void btn_update_Click(object sender, EventArgs e)
        {
            try
            {                
                string periodo = (ddl_periodo.SelectedValue == "") ? null : ddl_periodo.SelectedValue;
                string campus = (ddl_campus.SelectedValue == "") ? null : ddl_campus.SelectedValue;
                string materia = (ddl_materia.SelectedValue == "") ? null : ddl_materia.SelectedValue;               
                string grupo = (ddl_grupo.SelectedValue == "") ? null : ddl_grupo.SelectedValue;
                string componente = (ddl_componente.SelectedValue == "") ? null : ddl_componente.SelectedValue;
                string usuario = Session["usuario"].ToString();

                foreach (GridViewRow row in Gridtcacx.Rows)
                {
                    var dropdown = (DropDownList)row.FindControl("DDL_Calificacion");
                    calificacionPorComponentes.InsertCalificacionPorComponentes(periodo, campus, materia, grupo, componente, row.Cells[0].Text, row.Cells[2].Text, dropdown.SelectedValue, usuario);
                    
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_success", "alert_success('Se guardaron los datos exitosamente');", true);
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcacx", Session["usuario"].ToString(), ex.StackTrace);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
            }
        }



        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            CargaInicial();
            Gridtcacx = utils.ClearGridView(Gridtcacx);
        }

    }

    class Calificaciones
    {
        public string programa { get; set; }
        public DataTable soruce { get; set; }
    }
}