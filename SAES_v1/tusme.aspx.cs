using iTextSharp.text;
using MySqlX.XDevAPI.Relational;
using Org.BouncyCastle.Crypto.Macs;
using SAES_Services;
using SAES_v1.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Management;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static SAES_DBO.Models.ModelMenu;

namespace SAES_v1
{
    public partial class tusme : System.Web.UI.Page
    {
        #region <Variables>
        Utilidades utils = new Utilidades();
        Catalogos serviceRol = new Catalogos();
        MenuService serviceMenu = new MenuService();
        List<Catalogos> lstRoles = new List<Catalogos>();
        ModelObtenerPermisosFormsResponse objPermiso = new ModelObtenerPermisosFormsResponse();
        List<ModelObtenerPermisosFormsResponse> lstPermisos = new List<ModelObtenerPermisosFormsResponse>();

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                Inicializar();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "GridFormsPermisos", "load_datatableForms();", true);

        }
        private void Inicializar()
        {
            try
            {
                Cargarcombos();
                CargarGrid();
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
            }
        }

        private void Cargarcombos()
        {
            //lblError.Text = string.Empty;

            try
            {

                //lstRoles= serviceRol.obtenListRoles();
                ddlRol.DataSource = serviceRol.obtenListRoles();
                ddlRol.DataValueField = "trole_clave";
                ddlRol.DataTextField = "trole_desc";
                ddlRol.DataBind();

                ddlMenu.DataSource = serviceRol.obtenListMenu();
                ddlMenu.DataValueField = "menu_clave";
                ddlMenu.DataTextField = "menu_desc";
                ddlMenu.DataBind();
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
            }
        }

        protected void ddlRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarGrid();
        }

        protected void ddlMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarGrid();
        }

        private void CargarGrid()
        {
            try
            {
                grdUsuMenu.DataSource = null;
                grdUsuMenu.DataBind();
                DataTable dt = serviceMenu.ObtenerPermisosForms(Convert.ToInt32(ddlRol.SelectedValue), Convert.ToInt32(ddlMenu.SelectedValue));
                grdUsuMenu = utils.BeginGrid(grdUsuMenu, dt);
                ////GridView1 = utils.BeginGrid(GridView1, dt);



                //lstPermisos = serviceMenu.ObtenerListPermisosForms(Convert.ToInt32(ddlRol.SelectedValue), Convert.ToInt32(ddlMenu.SelectedValue));
                //Session["ListaPermisos"] = lstPermisos;
                //grdUsuMenu.DataSource = lstPermisos;
                //grdUsuMenu.DataBind();
                //if (Session["ListaPermisos"] == null)
                //{
                //    lstPermisos = new List<ModelObtenerPermisosFormsResponse>();
                //    lstPermisos.Add(ObjConcepto);
                //}
                //else
                //{
                //    lstPermisos = (List<ModelObtenerPermisosFormsResponse>)Session["ListaPermisos"];
                //    lstPermisos.Add(ObjConcepto);
                //}


            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
            }
        }

        protected void grdUsuMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
            }
        }

        protected void chkCons_CheckedChanged(object sender, EventArgs e)
        {

            CheckBox cbi = (CheckBox)(sender);
            GridViewRow row = (GridViewRow)cbi.NamingContainer;

            grdUsuMenu.SelectedIndex = row.RowIndex;

            lstPermisos = (List<ModelObtenerPermisosFormsResponse>)Session["ListaPermisos"];
            CheckBox cbCons2 = (CheckBox)(row.Cells[2].FindControl("chkCons"));
            string usme_select = (cbCons2.Checked == true) ? "1" : "0";


            int page = grdUsuMenu.PageIndex;
            lstPermisos[row.RowIndex].usme_select = (cbCons2.Checked == true) ? "true" : "false";

            Session["ListaPermisos"] = lstPermisos;


            grdUsuMenu.DataSource = lstPermisos;
            grdUsuMenu.DataBind();
        }
        protected void linkBttnGuardar_Click(object sender, EventArgs e)
        {
            try
            {

                foreach (GridViewRow row in grdUsuMenu.Rows)
                {

                    ModelObtenerPermisosFormsResponse objValPermiso = new ModelObtenerPermisosFormsResponse();

                    CheckBox cbCons = (CheckBox)(row.Cells[2].FindControl("chkCons"));
                    string usme_select = (cbCons.Checked == true) ? "1" : "0";

                    CheckBox cbAct = (CheckBox)(row.Cells[3].FindControl("chkAct"));
                    string usme_update = (cbAct.Checked == true) ? "1" : "0";


                    //if (usme_select == "0" && usme_update == "0")
                    //    serviceMenu.EliminarPermisosForms(ddlRol.SelectedValue, ddlMenu.SelectedValue, row.Cells[0].Text);
                    //else
                        serviceMenu.Ins_Tusme(ddlRol.SelectedValue, ddlMenu.SelectedValue, row.Cells[0].Text, usme_select, usme_update, Session["usuario"].ToString());


                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_success", "alert_success('Se guardaron los datos exitosamente');", true);

            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                mensaje_error = mensaje_error.Replace("\r\n", "");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
            }
        }
        //protected void linkBttnGuardar_Click(object sender, EventArgs e)
        //{
        //    string select = string.Empty;
        //    string update = string.Empty;
        //    try
        //    {
        //        if (Session["ListaPermisos"] != null)
        //        {

        //            lstPermisos = (List<ModelObtenerPermisosFormsResponse>)Session["ListaPermisos"];

        //            for (int i=0; i<lstPermisos.Count; i++) 
        //            {
        //                select=(lstPermisos[i].usme_select) == "true" ? "1" : "0";
        //                update = (lstPermisos[i].usme_update) == "true" ? "1" : "0";

        //                serviceMenu.EditarPermisosForms(ddlRol.SelectedValue, ddlMenu.SelectedValue, lstPermisos[i].mede_clave, select, update, Session["usuario"].ToString());

        //            }

        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_success", "alert_success('Se guardaron los datos exitosamente');", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string mensaje_error = ex.Message.Replace("'", "-");
        //        mensaje_error = mensaje_error.Replace("\r\n", "");
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
        //    }
        //}

        protected void chkCons_CheckedChanged1(object sender, EventArgs e)
        {

        }

        protected void grdUsuMenu_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
    }
}