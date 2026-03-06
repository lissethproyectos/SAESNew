using Aspose.Cells.Drawing;
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
using static SAES_DBO.Models.ModelCobranza;
using static SAES_DBO.Models.ModelMenu;

namespace SAES_v1
{
    public partial class tgeca : System.Web.UI.Page
    {
        #region <Variables>
        //Utilidades utils = new Utilidades();
        AlumnoService serviceAlumno = new AlumnoService();
        CobranzaService serviceCobranza = new CobranzaService();
        List<ModelObtenPaisesResponse> lstPaises = new List<ModelObtenPaisesResponse>();
        Catalogos serviceCatalogo = new Catalogos();
        public string id_num = null;
        MenuService servicePermiso = new MenuService();
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

                    if (Global.nombre != "")
                        txt_nombre.Text = Global.nombre + " " + Global.ap_paterno + " " + Global.ap_materno;

                    if (Global.cuenta != "")
                    {
                        txt_matricula.Text = Global.cuenta;
                        LlenaPagina();
                    }


                    Inicio();
                    ScriptManager.RegisterStartupScript(this, GetType(), "Periodos", "FiltPeriodos();", true);



                    //grid_telefono_bind(txt_matricula.Text);
                }
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "GridALumnos", "load_datatable_alumnos();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "GridALumnosCuotas", "load_datatable_cuotas_periodo();", true);

        }

        protected void grid_cuotas_alumnos_bind()
        {
            try
            {
                GridAlumnos.Visible = false;
                GridAlumnosCuotas.DataSource = null;
                GridAlumnosCuotas.DataBind();
                if (ddl_tipo.SelectedValue == "I")
                {
                    GridAlumnosCuotas.DataSource = serviceCobranza.ObtenerDatosCobranzaGral(ddl_periodo_destino_ind.SelectedValue,
                        ddl_campus.SelectedValue, ddl_nivel.SelectedValue, ddl_programa_ind.SelectedValue, ddl_tasa.SelectedValue, txt_matricula.Text, ddl_tipo.SelectedValue);
                }
                else
                {
                    GridAlumnosCuotas.DataSource = serviceCobranza.ObtenerDatosCobranzaGral(ddl_periodo_origen.SelectedValue,
                        ddl_campus.SelectedValue, ddl_nivel.SelectedValue, ddl_programa.SelectedValue, ddl_tasa.SelectedValue,
                        "", ddl_tipo.SelectedValue);

                }
                GridAlumnosCuotas.DataBind();
            }
            catch (Exception ex)
            {
                //Logs
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tgeca", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void linkBttnBusca_Click(object sender, EventArgs e)
        {
            //GridTelefono.Visible = false;
            try
            {
                Carga_Estudiante();
            }
            catch (Exception ex)
            {
                //Logs
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tgeca", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
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
                        btn_tgeca.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                    }
                    else
                    {
                        btn_tgeca.Visible = true;
                        Carga_Inf_Estudiante();
                    }
                }
                else
                {
                    btn_tgeca.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                }


            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tgeca", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);

            }
        }


        protected void GridAlumnos_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridAlumnos.SelectedRow;
            txt_matricula.Text = row.Cells[1].Text;
            txt_nombre.Text = HttpUtility.HtmlDecode(row.Cells[2].Text) + " " + HttpUtility.HtmlDecode(row.Cells[3].Text) + " " +
            HttpUtility.HtmlDecode(row.Cells[4].Text);
            GridAlumnos.Visible = false;
            txt_matricula.ReadOnly = true;
            Global.cuenta = txt_matricula.Text;
            Global.nombre = HttpUtility.HtmlDecode(row.Cells[2].Text);
            Global.ap_paterno = HttpUtility.HtmlDecode(row.Cells[3].Text);
            Global.ap_materno = HttpUtility.HtmlDecode(row.Cells[4].Text);
            id_num = row.Cells[5].Text;
            Carga_Inf_Estudiante();
        }
        protected void Carga_Estudiante()
        {
            ModelObtenerAlumnosResponse datosAlumno = new ModelObtenerAlumnosResponse();
            txt_nombre.Text = string.Empty;
            DataTable dtAlumno = new DataTable();


            try
            {
                dtAlumno = serviceAlumno.ObtenerAlumnos(txt_matricula.Text);
                if (dtAlumno.Rows.Count == 1)
                {
                    txt_nombre.Text = dtAlumno.Rows[0][1].ToString() + " " + dtAlumno.Rows[0][2].ToString() + " " + dtAlumno.Rows[0][3].ToString();
                    Global.cuenta = txt_matricula.Text;
                    Global.nombre = dtAlumno.Rows[0][0].ToString();
                    Global.ap_paterno = dtAlumno.Rows[0][1].ToString();
                    Global.ap_materno = dtAlumno.Rows[0][2].ToString();
                    Carga_Inf_Estudiante();
                    //grid_telefono_bind(txt_matricula.Text);
                }
                else
                {
                    GridAlumnos.Visible = true;
                    GridAlumnos.DataSource = dtAlumno;
                    GridAlumnos.DataBind();
                    GridAlumnosCuotas.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "GridALumnos", "load_datatable_alumnos();", true);

                }
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tsimu", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }

            //Estudiantes.Visible = true;
            txt_matricula.Focus();

        }

        protected void Carga_Inf_Estudiante()
        {


            ddl_programa_ind.DataSource = serviceAlumno.ObtenerProgramaAlumnoMatricula(txt_matricula.Text);
            ddl_programa_ind.DataValueField = "Clave";
            ddl_programa_ind.DataTextField = "Descripcion";
            ddl_programa_ind.DataBind();

            ddl_periodo_destino_ind.DataSource = serviceCatalogo.ObtenerPeriodosEscolaresVigentes(); //serviceAlumno.ObtenerPeriodoAlumno(txt_matricula.Text);
            ddl_periodo_destino_ind.DataValueField = "clave";
            ddl_periodo_destino_ind.DataTextField = "nombre";
            ddl_periodo_destino_ind.DataBind();


            grid_cuotas_alumnos_bind();
        }

        protected void Inicio()
        {
            ddl_periodo_origen.DataSource = serviceCatalogo.ObtenerPeriodosEscolaresVigentes();
            ddl_periodo_origen.DataValueField = "clave";
            ddl_periodo_origen.DataTextField = "nombre";
            ddl_periodo_origen.DataBind();


            ddl_periodo_destino.DataSource = serviceCatalogo.ObtenerPeriodosEscolaresVigentes();
            ddl_periodo_destino.DataValueField = "clave";
            ddl_periodo_destino.DataTextField = "nombre";
            ddl_periodo_destino.DataBind();



            ddl_periodo_destino_ind.DataSource = serviceCatalogo.ObtenerPeriodosEscolaresVigentes();
            ddl_periodo_destino_ind.DataValueField = "clave";
            ddl_periodo_destino_ind.DataTextField = "nombre";
            ddl_periodo_destino_ind.DataBind();


            ddl_campus.DataSource = serviceCatalogo.ObtenerCampus();
            ddl_campus.DataValueField = "Clave";
            ddl_campus.DataTextField = "Descripcion";
            ddl_campus.DataBind();

            ddl_tasa.DataSource = serviceCatalogo.ObtenerComboComun("Tasas", "");
            ddl_tasa.DataValueField = "Clave";
            ddl_tasa.DataTextField = "Descripcion";
            ddl_tasa.DataBind();

            ddl_campus_SelectedIndexChanged(null, null);

        }

        protected void ddl_campus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_campus.SelectedValue != "")
            {
                ddl_nivel.DataSource = serviceCatalogo.obtenNivel(ddl_campus.SelectedValue);
                ddl_nivel.DataValueField = "Clave";
                ddl_nivel.DataTextField = "Descripcion";
                ddl_nivel.DataBind();

            }
            else
            {
                ddl_nivel.Items.Clear();
                ddl_nivel.Items.Insert(0, new ListItem("-------", ""));
            }
            ddl_nivel_SelectedIndexChanged(null, null);
            //ddl_nivel.Items.Insert(0, new ListItem("--Seleccione--", "0"));
        }

        protected void ddl_programa_SelectedIndexChanged(object sender, EventArgs e)
        {
            grid_cuotas_alumnos_bind();
        }

        protected void linkBttnCancelar_Click(object sender, EventArgs e)
        {
            txt_matricula.ReadOnly = false;
            txt_matricula.Text = string.Empty;
            txt_nombre.Text = string.Empty;
            ddl_programa_ind.Items.Clear();
            ddl_programa_ind.Items.Insert(0, new ListItem("-------", ""));

            ddl_periodo_destino_ind.Items.Clear();
            ddl_periodo_destino_ind.Items.Insert(0, new ListItem("-------", ""));


            ddl_periodo_origen.SelectedIndex = 0;
            ddl_periodo_destino.SelectedIndex = 0;
            ddl_campus.SelectedIndex = 0;
            ddl_nivel.SelectedIndex = 0;
            ddl_programa.SelectedIndex = 0;
            ddl_tasa.SelectedIndex = 0;

            GridAlumnosCuotas.DataSource = null;
            GridAlumnosCuotas.DataBind();

        }

        protected void linkBttnGenerar_Click(object sender, EventArgs e)
        {
            ModelValidaCuotasAlumnoResponse objExisteCuotasAlu = new ModelValidaCuotasAlumnoResponse();
            ModelValidaCuotasPeriodoResponse objExisteCuotasPeriodo = new ModelValidaCuotasPeriodoResponse();

            try
            {
                if (ddl_tipo.SelectedValue == "I")
                {
                    objExisteCuotasAlu = serviceCobranza.ValidaCuotasAlumno(txt_matricula.Text, ddl_periodo_destino_ind.SelectedValue);
                    if (objExisteCuotasAlu != null)
                    {
                        if (objExisteCuotasAlu.Existe == "0")
                        {
                            serviceCobranza.GenerarEdoCta(ddl_periodo_destino_ind.SelectedValue, txt_matricula.Text,
                                ddl_programa_ind.SelectedValue, Session["usuario"].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "generar", "generar();", true);
                            grid_cuotas_alumnos_bind();

                        }
                        else
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "valida_cuotas_alumno", "valida_cuotas();", true);

                    }
                }
                else
                {
                    int contador = 0;
                    //objExisteCuotasPeriodo = serviceCobranza.ValidaCuotasMasivo(ddl_periodo_destino.SelectedValue);

                    //Falta Procedimiento
                    foreach (GridViewRow row in GridAlumnosCuotas.Rows)
                    {
                        objExisteCuotasAlu = serviceCobranza.ValidaCuotasAlumno(row.Cells[1].Text, ddl_periodo_destino.SelectedValue);
                        if (objExisteCuotasAlu != null)
                        {
                            if (objExisteCuotasAlu.Existe == "0")
                            {
                                serviceCobranza.GenerarEdoCta(ddl_periodo_destino.SelectedValue, row.Cells[1].Text,
                            ddl_programa.SelectedValue, Session["usuario"].ToString());
                                contador = contador + 1;
                            }
                        }
                    }

                    if (contador > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "generar", "generar();", true);
                        grid_cuotas_alumnos_bind();
                    }

                }
            }
            catch (Exception ex)
            {
                //Logs
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tgeca", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void ddl_tipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            rowMasivo.Visible = false;
            rowIndividual.Visible = false;
            ddl_tasa.SelectedIndex = 0;
            ddl_campus.SelectedIndex = 0;
            ddl_campus_SelectedIndexChanged(null, null);


            if (ddl_tipo.SelectedValue == "I")
                rowIndividual.Visible = true;
            else
            {

                rowMasivo.Visible = true;

            }
            grid_cuotas_alumnos_bind();
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
                }
                else
                {
                    ddl_programa.Items.Clear();
                    ddl_programa.Items.Insert(0, new ListItem("-------", ""));
                }
                grid_cuotas_alumnos_bind();
            }
            catch (Exception ex)
            {
                //Logs
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tgeca", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void ddl_periodo_destino_SelectedIndexChanged(object sender, EventArgs e)
        {
            grid_cuotas_alumnos_bind();
        }

        protected void ddl_periodo_destino_ind_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridAlumnosCuotas.Visible = true;
            grid_cuotas_alumnos_bind();
        }

        protected void ddl_tasa_SelectedIndexChanged(object sender, EventArgs e)
        {
            grid_cuotas_alumnos_bind();
        }
    }
}