using SAES_DBO.Models;
using SAES_Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using static SAES_DBO.Models.ModelAlumno;
using static SAES_DBO.Models.ModelMenu;

namespace SAES_v1
{
    public partial class tcanc : System.Web.UI.Page
    {
        #region <Variables>
        //Utilidades utils = new Utilidades();
        AlumnoService serviceAlumno = new AlumnoService();
        List<ModelObtenPaisesResponse> lstPaises = new List<ModelObtenPaisesResponse>();
        Catalogos serviceCatalogo = new Catalogos();
        ServicioSocialService serviceServicioSocial = new ServicioSocialService();
        MenuService servicePermiso = new MenuService();
        FinanzasService serviceFinanzas = new FinanzasService();
        #endregion

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
                    txt_matricula.Text = Global.cuenta;
                    txt_matricula.Text = Global.cuenta;
                    txt_nombre.Text = Global.nombre + " " + Global.ap_paterno + " " + Global.ap_materno;
                    LlenaPagina();
                    txt_matricula.Focus();

                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_alumnos", "load_datatable_alumnos();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);
            }
        }

        private void LlenaPagina()
        {
            ModelObtenerPermisoFormResponse objPermiso = new ModelObtenerPermisoFormResponse();
            objPermiso.usme_update = "0";
            objPermiso.usme_select = "0";
            System.Threading.Thread.Sleep(50);
            try
            {
                objPermiso = servicePermiso.ObtenerPermisoFormulario(Session["usuario"].ToString(), "tdsap");
                if (objPermiso != null)
                {
                    if (objPermiso.usme_update == "0" || objPermiso.usme_select == "0")
                    {
                        btn_tcanc.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                    }
                    else
                    {
                        btn_tcanc.Visible = true;
                        Carga_Programas();
                        Carga_Periodos();
                        ddl_periodo_SelectedIndexChanged(null, null);
                    }
                }
                else
                {
                    btn_tcanc.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                }


            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcanc", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);

            }
        }


        protected void linkBttnBusca_Click(object sender, EventArgs e)
        {
            try
            {
                Carga_Estudiante();
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcanc", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void Carga_Estudiante()
        {
            ModelObtenerAlumnosResponse datosAlumno = new ModelObtenerAlumnosResponse();
            txt_nombre.Text = string.Empty;
            DataTable dtAlumno = new DataTable();
            GridAlumnos.Visible = false;


            try
            {
                dtAlumno = serviceAlumno.ObtenerAlumnos(txt_matricula.Text);
                if (dtAlumno.Rows.Count == 1)
                {
                    txt_nombre.Text = dtAlumno.Rows[0][1].ToString() + " " + dtAlumno.Rows[0][2].ToString() + " " + dtAlumno.Rows[0][3].ToString();
                    Global.cuenta = txt_matricula.Text;
                    Global.nombre = dtAlumno.Rows[0][1].ToString();
                    Global.ap_paterno = dtAlumno.Rows[0][2].ToString();
                    Global.ap_materno = dtAlumno.Rows[0][3].ToString();
                    Carga_Programas();
                    //Carga_Periodos();
                    grid_bind_tcanc();
                }
                else if (dtAlumno.Rows.Count > 1)
                {
                    GridAlumnos.Visible = true;
                    GridAlumnos.DataSource = dtAlumno;
                    GridAlumnos.DataBind();
                }
                else
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "NoexisteAlumno", "NoexisteAlumno();", true);
                    txt_matricula.Text = null;
                    txt_nombre.Text = null;
                    ddl_programa.Items.Clear();
                    ddl_programa.Items.Add(new ListItem("-------", ""));
                    ddl_programa.DataSource = null;
                    ddl_programa.DataBind();
                    Gridtcanc.DataSource = null;
                    Gridtcanc.DataBind();
                    txt_matricula.Focus();
                }
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcanc", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }

        }

        protected void grid_bind_tcanc()
        {
            try
            {
                Gridtcanc.DataSource = null;
                Gridtcanc.DataBind();
                Gridtcanc.DataSource = serviceFinanzas.obtenConceptosCartera(txt_matricula.Text, ddl_programa.SelectedValue, ddl_periodo.SelectedValue);
                Gridtcanc.DataBind();
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tdsap", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                //Response.Redirect("Inicio.aspx");
            }
        }


        protected void Carga_Programas()
        {
            List<ModelObtenerProgsAlumnoResponse> lst = new List<ModelObtenerProgsAlumnoResponse>();
            DataTable dt = new DataTable();
            hddnNivel.Value = string.Empty;

            Session["listaNiveles"] = null;
            try
            {
                //dt = serviceAlumno.ObtenerProgramaAlumno2(txt_matricula.Text);
                lst = serviceAlumno.ObtenerProgramaAlumno2(txt_matricula.Text);

                ddl_programa.DataSource = lst; //
                serviceAlumno.ObtenerProgramaAlumno2(txt_matricula.Text);
                ddl_programa.DataValueField = "Clave";
                ddl_programa.DataTextField = "Descripcion";
                ddl_programa.DataBind();

                Session["listaNiveles"] = lst;
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcanc", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                //Response.Redirect("Inicio.aspx");
            }
        }

        protected void ddl_periodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            grid_bind_tcanc();
        }

        protected void GridAlumnos_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridAlumnos.SelectedRow;
            txt_matricula.Text = row.Cells[1].Text;
            txt_nombre.Text = HttpUtility.HtmlDecode(row.Cells[2].Text) + " " + HttpUtility.HtmlDecode(row.Cells[3].Text) + " " +
             HttpUtility.HtmlDecode(row.Cells[4].Text);
            GridAlumnos.Visible = false;
            //txt_matricula.ReadOnly = true;
            btn_cancelar_conc.Visible = true;
            btn_cancel_save.Visible = true;
            btn_cancel_update.Visible = false;

            Global.cuenta = txt_matricula.Text;
            Global.nombre = HttpUtility.HtmlDecode(row.Cells[2].Text);
            Global.ap_paterno = HttpUtility.HtmlDecode(row.Cells[3].Text);
            Global.ap_materno = HttpUtility.HtmlDecode(row.Cells[4].Text);
            Carga_Programas();
            Carga_Periodos();
            ddl_periodo_SelectedIndexChanged(null, null);
        }

        protected void Carga_Periodos()
        {
            ddl_periodo.DataSource = serviceAlumno.ObtenerPeriodoAlumno(txt_matricula.Text); // serviceCatalogo.ObtenerPeriodosEscolaresVigentes();
            ddl_periodo.DataValueField = "Clave";
            ddl_periodo.DataTextField = "Descripcion";
            ddl_periodo.DataBind();
        }

        protected void btn_cancel_update_Click(object sender, EventArgs e)
        {

        }

        protected void btn_cancel_save_Click(object sender, EventArgs e)
        {

        }

        protected void btn_save_Click(object sender, EventArgs e)
        {

        }

        protected void btn_cancelar_conc_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "modalCancelarConceptos", "$('#modalCancelarConceptos').modal('show')", true);

        }

        protected void linkBttnActualizar_Click(object sender, EventArgs e)
        { 
        bool existe = false;
            string valida;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "modalCancelarConceptos", "$('#modalCancelarConceptos').modal('hide')", true);
            try
            {
                foreach (GridViewRow row in Gridtcanc.Rows)
                {

                    ModelObtenerPermisosFormsResponse objValPermiso = new ModelObtenerPermisosFormsResponse();
                    CheckBox cbAct = (CheckBox)(row.Cells[0].FindControl("chkValida"));
                    valida = (cbAct.Checked == true) ? "1" : "0";
                    if (valida == "1")
                    {
                        existe = true;
                        serviceFinanzas.CancelarPagos(txt_matricula.Text, ddl_programa.SelectedValue, ddl_periodo.SelectedValue,
                            Session["usuario"].ToString(), row.Cells[2].Text,
                            row.Cells[7].Text, row.Cells[3].Text);
                    }

                }

                if (existe == false)
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_sel", "error_sel();", true);
                else
                {
                    grid_bind_tcanc();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                }

            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcanc", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                //Response.Redirect("Inicio.aspx");
            }
        }

    }
}