using SAES_Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static SAES_DBO.Models.ModelAlumno;
using static SAES_DBO.Models.ModelCobranza;

namespace SAES_v1
{
    public partial class tepcb : System.Web.UI.Page
    {
        #region <Variables>
        AlumnoService serviceAlumno = new AlumnoService();
        CobranzaService serviceCobro = new CobranzaService();
        Catalogos serviceCatalogo = new Catalogos();
        MenuService servicePermiso = new MenuService();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Global.cuenta != string.Empty)
                {
                    txt_matricula.Text = Global.cuenta;
                    linkBttnBusca_Click(null, null);

                    if (Global.programa != string.Empty)
                    {
                        try
                        {
                            ddl_programa.SelectedValue = Global.programa;
                            ddl_programa_SelectedIndexChanged(null, null);
                        }
                        catch
                        {
                            ddl_programa.SelectedIndex = 0;
                        }
                    }
                }

                Inicial();
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "GridALumnos", "load_datatable_alumnos();", true);
        }

        protected void Inicial()
        {
            ddl_periodo.DataSource = serviceCatalogo.ObtenerPeriodosEscolaresVigentes();
            ddl_periodo.DataValueField = "clave";
            ddl_periodo.DataTextField = "nombre";
            ddl_periodo.DataBind();
            //rowGridTepcb.Visible = true;
            //grid_bind_tepcb();
        }
        protected void linkBttnBusca_Click(object sender, EventArgs e)
        {
            txt_alumno.Text = string.Empty;
            ddl_programa.Items.Clear();
            DataTable dtAlumno = new DataTable();
            List<ModelObtenerProgActAlumnoResponse> lstAlumno = new List<ModelObtenerProgActAlumnoResponse>();
            List<ModelObtenerDatosCobroResponse> lst = new List<ModelObtenerDatosCobroResponse>();
            try
            {
                if (txt_matricula.Text != string.Empty)
                {
                    rowAlumnos.Visible = false;
                    lstAlumno = serviceAlumno.ObtenerProgActAlumno(txt_matricula.Text);
                    if (lstAlumno.Count >= 1)
                    {
                        txt_alumno.Text = lstAlumno[0].nombre + " " + lstAlumno[0].paterno + " " + lstAlumno[0].materno;
                        ddl_programa.DataSource = serviceAlumno.ObtenerProgramaAlumnoMatricula(txt_matricula.Text);
                        ddl_programa.DataValueField = "Clave";
                        ddl_programa.DataTextField = "Descripcion";
                        ddl_programa.DataBind();

                        //ddl_periodo.DataSource = serviceAlumno.ObtenerPeriodoAlumno(txt_matricula.Text);
                        //ddl_periodo.DataValueField = "Clave";
                        //ddl_periodo.DataTextField = "Descripcion";
                        //ddl_periodo.DataBind();

                        GridTepcb.DataSource = null;
                        GridTepcb.DataBind();

                        Global.campus = lstAlumno[0].testu_tcamp_clave;

                    }
                    ddl_programa_SelectedIndexChanged(null, null);
                }
                else
                {
                    rowAlumnos.Visible = true;
                    GridAlumnos.DataSource = serviceAlumno.ObtenerAlumnos(txt_matricula.Text);
                    GridAlumnos.DataBind();
                }
            }
            catch (Exception ex)
            {
                //Logs
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tepcb", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void GridAlumnos_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridAlumnos.SelectedRow;
            txt_matricula.Text = row.Cells[1].Text;
            txt_alumno.Text = HttpUtility.HtmlDecode(row.Cells[2].Text) + " " + HttpUtility.HtmlDecode(row.Cells[3].Text) +
                " " + HttpUtility.HtmlDecode(row.Cells[4].Text);
            GridAlumnos.Visible = false;
            txt_matricula.Attributes.Add("readonly", "");
            txt_alumno.Attributes.Add("readonly", "");
            Global.cuenta = txt_matricula.Text;
            Global.nombre_alumno = txt_alumno.Text;
            GridAlumnos.Visible = false;
            linkBttnBusca_Click(null, null);
            //Programa();
        }

        protected void ddl_programa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_programa.SelectedValue == "")
                rowGridTepcb.Visible = false;
            else
            {
                linkBttnCancelar.Visible = true;
                rowAlumnos.Visible = false;
                rowGridTepcb.Visible = true;
                grid_bind_tepcb();

            }
        }

        protected void GridTepcb_SelectedIndexChanged(object sender, EventArgs e)
        {
            linkBttnGuardar.Visible = false;
            linkBttnCancelar.Visible = false;
            linkBttnCancelarModificar.Visible = true;
            linkBttnModificar.Visible = true;
            ddl_periodo.Enabled = false;
            ddl_becas.Enabled = false;
            ddl_becas.DataSource = serviceAlumno.ObtenerBecas(txt_matricula.Text, Global.campus, ddl_programa.SelectedValue, GridTepcb.SelectedRow.Cells[2].Text);
            ddl_becas.DataValueField = "Clave";
            ddl_becas.DataTextField = "Descripcion";
            ddl_becas.DataBind();
            try
            {
                ddl_periodo.SelectedValue = GridTepcb.SelectedRow.Cells[1].Text;
            }
            catch
            {
                ddl_periodo.SelectedIndex = 0;
            }
            try
            {
                ddl_becas.SelectedValue = GridTepcb.SelectedRow.Cells[2].Text;
            }
            catch
            {
                ddl_becas.SelectedIndex = 0;
            }
            try
            {
                ddl_estatus.SelectedValue = GridTepcb.SelectedRow.Cells[6].Text;
            }
            catch
            {
                ddl_estatus.SelectedIndex = 0;
            }

        }

        protected void ddl_periodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddl_becas.DataSource = serviceAlumno.ObtenerBecas(txt_matricula.Text, Global.campus, ddl_programa.SelectedValue, ddl_periodo.SelectedValue);
                ddl_becas.DataValueField = "Clave";
                ddl_becas.DataTextField = "Descripcion";
                ddl_becas.DataBind();

                //grid_bind_tepcb();


            }
            catch (Exception ex)
            {
                //Logs
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tepcb", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void linkBttnCancelar_Click(object sender, EventArgs e)
        {
            rowGridTepcb.Visible = false;
            ddl_periodo.SelectedIndex = 0;
            ddl_becas.SelectedIndex = 0;
            ddl_estatus.SelectedIndex = 0;
            txt_matricula.ReadOnly = false;
            txt_matricula.Text = string.Empty;
            txt_alumno.Text = string.Empty;
            ddl_programa.Items.Clear();
            ddl_programa.DataSource = null;
            ddl_programa.DataBind();
        }

        protected void linkBttnCancelarModificar_Click(object sender, EventArgs e)
        {
            linkBttnCancelar.Visible = true;
            linkBttnGuardar.Visible = true;
            linkBttnCancelarModificar.Visible = false;
            linkBttnModificar.Visible = false;
            ddl_becas.Enabled = true;
            ddl_periodo.Enabled = true;
            ddl_periodo.SelectedIndex = 0;
            ddl_becas.SelectedIndex = 0;
            ddl_estatus.SelectedIndex = 0;
            GridTepcb.SelectedIndex = -1;
        }

        protected void linkBttnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                serviceCobro.InsertarBecaAlumno(txt_matricula.Text, ddl_periodo.SelectedValue,
              ddl_programa.SelectedValue, ddl_becas.SelectedValue, 1, ddl_estatus.SelectedValue, Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                grid_bind_tepcb();
            }
            catch (Exception ex)
            {
                //Logs
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tepcb", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void linkBttnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (GridTepcb.SelectedRow.Cells[6].Text != ddl_estatus.SelectedValue)
                {
                    if (ddl_estatus.SelectedValue == "A")
                    {
                        lblMsjEstatus.Text = "¿Esta seguro de reactivar la beca?";

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "showEstatus", "$('#modalActualizaEstatus').modal('show')", true);
                    }
                    else
                    {
                        lblMsjEstatus.Text = "¿Esta seguro de cancelar la beca?";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "showEstatus", "$('#modalActualizaEstatus').modal('show')", true);
                    }
                }
                else
                    EditarBeca();

            }
            catch (Exception ex)
            {
                //Logs
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tepcb", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void linkBttnAplicar_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showEstatus", "$('#modalActualizaEstatus').modal('hide')", true);

                serviceCobro.EditarBecaAlumno(txt_matricula.Text, ddl_periodo.SelectedValue,
                ddl_programa.SelectedValue, ddl_becas.SelectedValue, 1, ddl_estatus.SelectedValue, Session["usuario"].ToString(), "S");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Actualizar", "update();", true);
                linkBttnCancelarModificar_Click(null, null);
                grid_bind_tepcb();
            }
            catch (Exception ex)
            {
                //Logs
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tepcb", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void EditarBeca()
        {
            try
            {

                serviceCobro.EditarBecaAlumno(txt_matricula.Text, ddl_periodo.SelectedValue,
                ddl_programa.SelectedValue, ddl_becas.SelectedValue, 1, ddl_estatus.SelectedValue, Session["usuario"].ToString(), "N");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Actualizar", "update();", true);
                linkBttnCancelarModificar_Click(null, null);
                grid_bind_tepcb();
            }
            catch (Exception ex)
            {
                //Logs
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tepcb", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }


        protected void grid_bind_tepcb()
        {
            try
            {
                GridTepcb.DataSource = null;
                GridTepcb.DataBind();
                GridTepcb.DataSource = serviceAlumno.ObtenerBecasAlumno(txt_matricula.Text, ddl_programa.SelectedValue);
                GridTepcb.DataBind();
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tepcb", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                //Response.Redirect("Inicio.aspx");
            }
        }

        protected void linkBttnPagosAplicados_Click(object sender, EventArgs e)
        {

            Global.cuenta = txt_matricula.Text;
            Global.nombre_alumno = txt_alumno.Text;
            Global.programa = ddl_programa.SelectedValue;
            Global.nombre_programa = ddl_programa.SelectedItem.ToString();
            //ImgPdf.Visible = false;
            Global.opcion = 1;
            Global.ruta_origen = "tepcb.aspx";
            Response.Redirect("Tcedc2.aspx");
        }

        protected void linkBttnPlanesCobro_Click(object sender, EventArgs e)
        {
            Response.Redirect("tcpcb.aspx");

        }
    }
}