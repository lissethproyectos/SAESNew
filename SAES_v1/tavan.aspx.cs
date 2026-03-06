using SAES_DBO.Models;
using SAES_Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static SAES_DBO.Models.ModelAlumno;

namespace SAES_v1
{
    public partial class tavan : System.Web.UI.Page
    {
        #region <Variables>
        //Utilidades utils = new Utilidades();
        AlumnoService serviceAlumno = new AlumnoService();
        List<ModelObtenPaisesResponse> lstPaises = new List<ModelObtenPaisesResponse>();
        Catalogos serviceCatalogo = new Catalogos();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddl_programa.Items.Insert(0, new ListItem("------", ""));
                txt_matricula.Focus();
            }


            ScriptManager.RegisterStartupScript(this, this.GetType(), "GridAlumnos", "load_datatable_alumnos();", true);

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
                Global.inserta_log(mensaje_error, "tavan", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void Carga_Estudiante()
        {
            ModelObtenerAlumnosResponse datosAlumno = new ModelObtenerAlumnosResponse();
            txt_nombre.Text = string.Empty;
            DataTable dtAlumno = new DataTable();
            Gridtpers.Visible = false;


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
                }
                else if (dtAlumno.Rows.Count > 1)
                {
                    Gridtpers.Visible = true;
                    Gridtpers.DataSource = dtAlumno;
                    Gridtpers.DataBind();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "NoexisteAlumno", "NoexisteAlumno();", true);
                    txt_matricula.Text = null;
                    txt_matricula.Focus();
                }
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tavan", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }

        }


        protected void Gridtpers_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txt_matricula.Text = Gridtpers.SelectedRow.Cells[2].Text;
                txt_nombre.Text = Gridtpers.SelectedRow.Cells[3].Text + " " + Gridtpers.SelectedRow.Cells[4].Text + " " + Gridtpers.SelectedRow.Cells[5].Text;

                ddl_programa.DataSource = serviceAlumno.ObtenerProgramaAlumno2(txt_matricula.Text);
                ddl_programa.DataValueField = "Clave";
                ddl_programa.DataTextField = "Descripcion";
                ddl_programa.DataBind();



                //ddl_periodo.DataSource = serviceCatalogo.ObtenerPeriodosEscolaresVigentes();
                //ddl_periodo.DataValueField = "clave";
                //ddl_periodo.DataTextField = "nombre";
                //ddl_periodo.DataBind();


                //linkBttnCancelar.Visible = true;
                //linkBttnGuardar.Visible = true;
                //linkBttnCancelarModificar.Visible = false;
                //linkBttnModificar.Visible = false;



            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tavan", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void ddl_programa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_programa.SelectedValue != "")
            {
                containerGridAlumnos.Visible = false;
                containerHistorialAcad.Visible = true;

                //containerDetalle.Visible = true;
                //ddl_programa.Enabled = false;
                //grid_bind_testu();
                linkBttnCancelar.Visible = true;
                //linkBttnGuardar.Visible = true;
                //linkBttnCancelarModificar.Visible = false;
                //linkBttnModificar.Visible = false;
                //linkBttnBusca.Visible = false;

                string ruta = "../Reports/VisualizadorCrystal.aspx?Tipo=RepAvanceAcad&Valor1=" + txt_matricula.Text + "&Valor2=" + ddl_programa.SelectedValue + "&Valor3=" + Gridtpers.SelectedRow.Cells[0].Text + "&enExcel=N";
                string _open = "window.open('" + ruta + "', 'miniContenedor');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), _open, true);
            }
        }
        protected void linkBttnCancelar_Click(object sender, EventArgs e)
        {
            containerGridAlumnos.Visible = true;
            containerHistorialAcad.Visible = false;
        }
    }
}