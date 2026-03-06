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
    public partial class tdsap : System.Web.UI.Page
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
                        btn_tress.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                    }
                    else
                    {
                        btn_tress.Visible = true;
                        Carga_Programas();
                        Carga_Periodos();
                    }
                }
                else
                {
                    btn_tress.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                }


            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tdsap", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);

            }
        }

        protected void Carga_Periodos()
        {
            ddl_periodo.DataSource = serviceAlumno.ObtenerPeriodoAlumno(txt_matricula.Text); // serviceCatalogo.ObtenerPeriodosEscolaresVigentes();
            ddl_periodo.DataValueField = "Clave";
            ddl_periodo.DataTextField = "Descripcion";
            ddl_periodo.DataBind();
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
                //lstProductosAgregados = (List<Bien>)Session["listaProductosAgregados"];

                //ddl_programa_SelectedIndexChanged(null, null);
                //if (dt.Rows.Count > 1)
                //{
                //    ddl_nivel.SelectedValue = dt.Rows[1][2].ToString();
                //    hddnNivel.Value = dt.Rows[1][2].ToString();
                //}
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tdsap", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                //Response.Redirect("Inicio.aspx");
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
                Global.inserta_log(mensaje_error, "tdsap", Session["usuario"].ToString());
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
                    Carga_Periodos();
                    //Carga_Programas_Disponibles("-1");
                    grid_bind_tdsap();
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
                    Gridtdsap.DataSource = null;
                    Gridtdsap.DataBind();
                    txt_matricula.Focus();
                }
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tdsap", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }

        }

        protected void grid_bind_tdsap()
        {
            try
            {
                Gridtdsap.DataSource = null;
                Gridtdsap.DataBind();
                Gridtdsap.DataSource = serviceFinanzas.obtenConceptosPagados(txt_matricula.Text, ddl_programa.SelectedValue);
                Gridtdsap.DataBind();
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tdsap", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                //Response.Redirect("Inicio.aspx");
            }
        }

        protected void ddl_programa_SelectedIndexChanged(object sender, EventArgs e)
        {
            grid_bind_tdsap();
        }

        protected void GridAlumnos_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridAlumnos.SelectedRow;
            txt_matricula.Text = row.Cells[1].Text;
            txt_nombre.Text = HttpUtility.HtmlDecode(row.Cells[2].Text) + " " + HttpUtility.HtmlDecode(row.Cells[3].Text) + " " +
             HttpUtility.HtmlDecode(row.Cells[4].Text);
            GridAlumnos.Visible = false;
            //txt_matricula.ReadOnly = true;
            btn_save.Visible = true;
            btn_update.Visible = false;
            btn_cancel_save.Visible = true;
            btn_cancel_update.Visible = false;

            Global.cuenta = txt_matricula.Text;
            Global.nombre = HttpUtility.HtmlDecode(row.Cells[2].Text);
            Global.ap_paterno = HttpUtility.HtmlDecode(row.Cells[3].Text);
            Global.ap_materno = HttpUtility.HtmlDecode(row.Cells[4].Text);
            Carga_Programas();
            Carga_Periodos();
            //Carga_Programas_Disponibles("-1");
            //grid_bind_tress();
        }

        protected void btn_cancel_update_Click(object sender, EventArgs e)
        {

        }

        protected void btn_cancel_save_Click(object sender, EventArgs e)
        {
            txt_matricula.Text = null;
            txt_nombre.Text = null;
            ddl_programa.Items.Clear();
            ddl_periodo.Items.Clear();
            Gridtdsap.DataSource = null;
            Gridtdsap.DataBind();
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
           

            //if (checkedIDs!=null)
                ScriptManager.RegisterStartupScript(this, this.GetType(), "modalDesaplicar", "$('#modalDesaplicar').modal('show')", true);
            


            //foreach (GridViewRow row in Gridtdsap.Rows)
            //{

            //    ModelObtenerPermisosFormsResponse objValPermiso = new ModelObtenerPermisosFormsResponse();

            //      CheckBox cbAct = (CheckBox)(row.Cells[0].FindControl("chkValida"));
            //    string usme_update = (cbAct.Checked == true) ? "1" : "0";
            //    //serviceFinanzas.obtenConceptosPagados();// serviceMenu.Ins_Tusme(ddlRol.SelectedValue, ddlMenu.SelectedValue, row.Cells[0].Text, usme_select, usme_update, Session["usuario"].ToString());


            //}

            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_success", "alert_success('Se guardaron los datos exitosamente');", true);


        }

        protected void btn_update_Click(object sender, EventArgs e)
        {

        }

        protected void btn_save_Click1(object sender, EventArgs e)
        {

        }

        protected void linkBttnDesaplicar_Click(object sender, EventArgs e)
        {
            bool existe = false;
            string valida;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "modalDesaplicar", "$('#modalDesaplicar').modal('hide')", true);
            try
            {
                foreach (GridViewRow row in Gridtdsap.Rows)
                {

                    ModelObtenerPermisosFormsResponse objValPermiso = new ModelObtenerPermisosFormsResponse();
                    CheckBox cbAct = (CheckBox)(row.Cells[0].FindControl("chkValida"));
                    valida = (cbAct.Checked == true) ? "1" : "0";
                    if (valida == "1")
                    {
                        existe = true;
                        serviceFinanzas.DesaplicarPagos(txt_matricula.Text, ddl_programa.SelectedValue, ddl_periodo.SelectedValue,
                            row.Cells[9].Text, row.Cells[10].Text, row.Cells[2].Text, row.Cells[7].Text, Session["usuario"].ToString());
                    }

                }

                if (existe == false)
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_sel", "error_sel();", true);
                else
                {
                    grid_bind_tdsap();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                }

            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tdsap", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                //Response.Redirect("Inicio.aspx");
            }
        }

        protected void ddl_periodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            grid_bind_tdsap();
        }
    }
}