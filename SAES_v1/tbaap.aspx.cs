using iTextSharp.text;
using MySqlX.XDevAPI.Relational;
using SAES_DBO.Models;
using SAES_Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static SAES_DBO.Models.ModelAlumno;

namespace SAES_v1
{
    public partial class tbaap : System.Web.UI.Page
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
                txt_matricula.Text = Global.cuenta;
                txt_nombre.Text = Global.nombre_alumno;
                txt_matricula.Focus();
                Gridtpers.DataSource = null;
                Gridtpers.DataBind();
                combo_tipo_baja();
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "GridALumnos", "load_datatable_alumnos();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ctrl_fecha_b", "ctrl_fecha_b();", true);


        }
        protected void combo_tipo_baja()
        {
            try
            {

                ddl_tipo_baja.DataSource = serviceCatalogo.ObtenerComboComun("Tipo_Baja", "");
                ddl_tipo_baja.DataValueField = "Clave";
                ddl_tipo_baja.DataTextField = "Descripcion";
                ddl_tipo_baja.DataBind();
            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tpaco", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void linkBttnBusca_Click(object sender, EventArgs e)
        {
            ModelObtenerAlumnosResponse datosAlumno = new ModelObtenerAlumnosResponse();

            txt_nombre.Text = string.Empty;
            ddl_programa.Items.Clear();
            DataTable dtAlumno = new DataTable();
            try
            {

                dtAlumno = serviceAlumno.ObtenerAlumnos(txt_matricula.Text);
                if (dtAlumno.Rows.Count == 1)
                {
                    txt_nombre.Text = dtAlumno.Rows[0][1].ToString() + " " + dtAlumno.Rows[0][2].ToString() + " " + dtAlumno.Rows[0][3].ToString();
                    hddnIdAlumno.Value = dtAlumno.Rows[0][12].ToString();


                    ddl_periodo.DataSource = serviceAlumno.ObtenerPeriodoAlumno(txt_matricula.Text);
                    ddl_periodo.DataValueField = "Clave";
                    ddl_periodo.DataTextField = "Descripcion";
                    ddl_periodo.DataBind();

                    ddl_programa.DataSource = serviceAlumno.ObtenerProgramaAlumnoMatricula(txt_matricula.Text);
                    ddl_programa.DataValueField = "Clave";
                    ddl_programa.DataTextField = "Descripcion";
                    ddl_programa.DataBind();
                    ddl_programa_SelectedIndexChanged(null, null);
                }
                else
                {
                    containerGridAlumnos.Visible = true;
                    Gridtpers.DataSource = serviceAlumno.ObtenerAlumnos(txt_matricula.Text);// ("1", "202065")  
                    Gridtpers.DataBind();
                }

            }
            catch (Exception ex)
            {
                //Logs
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tbaap", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
            //ModelObtenerAlumnosResponse datosAlumno = new ModelObtenerAlumnosResponse();
            //try
            //{
            //    Gridtpers.DataSource = serviceAlumno.ObtenerAlumnos(txt_matricula.Text);// ("1", "202065")  
            //    Gridtpers.DataBind();
            //}
            //catch (Exception ex)
            //{
            //    //Logs
            //    string mensaje_error = ex.Message.Replace("'", "-");
            //    Global.inserta_log(mensaje_error, "testa", Session["usuario"].ToString());
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            //}
        }

        protected void Gridtbaap_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txt_fecha_b.Text = HttpUtility.HtmlDecode(Gridtbaap.SelectedRow.Cells[4].Text);
                ddl_periodo.SelectedValue = Gridtbaap.SelectedRow.Cells[2].Text;
                ddl_tipo_baja.SelectedValue = Gridtbaap.SelectedRow.Cells[6].Text;
                if (Gridtbaap.SelectedRow.Cells[5].Text == "S")
                {
                    //customSwitches2.Checked = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "habilitar_calcular_devolucion", "habilitar_calcular_devolucion('S');", true);
                    checked_input2.Value = "S";

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "habilitar_calcular_devolucion", "habilitar_calcular_devolucion('N');", true);

                    checked_input2.Value = "N";
                }
                linkBttnCancelar.Visible = false;
                linkBttnGuardar.Visible = false;
                linkBttnCancelarModificar.Visible = true;
                linkBttnModificar.Visible = true;
            }
            catch (Exception ex)
            {
                //Logs
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "testa", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void linkBttnGuardar_Click(object sender, EventArgs e)
        {
            ModelInstbaapResponse objExisteRegistro = new ModelInstbaapResponse();
            try
            {
                DateTime fechaBaja = DateTime.ParseExact(txt_fecha_b.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                if (fechaBaja <= DateTime.Now)
                {
                    objExisteRegistro = serviceAlumno.InsertarBaja(hddnIdAlumno.Value, ddl_programa.SelectedValue, ddl_periodo.SelectedValue,
                        ddl_tipo_baja.SelectedValue, txt_fecha_b.Text, checked_input2.Value, "", Session["usuario"].ToString(), checked_input2.Value);
                    if (objExisteRegistro != null)
                    {
                        if (objExisteRegistro.Existe == "0")
                        {
                            if (checked_input2.Value == "S")
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Aplicar", "save_aplica();", true);
                            else
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);

                            grid_bind_tbaap();
                            ddl_periodo.SelectedIndex = 0;
                            ddl_tipo_baja.SelectedIndex = 0;
                            txt_fecha_b.Text = string.Empty;
                            checked_input2.Value = "N";
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "error_clave", "error_clave();", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validarclave('ContentPlaceHolder1_c_zip', 1);", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_fecha_baja", "error_fecha_baja();", true);

                }
            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tbaap", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
            }
        }
        protected void linkBttnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Gridtbaap.SelectedRow.Cells[5].Text == "S")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_modifica();", true);
                else
                {
                    DateTime fechaBaja = DateTime.ParseExact(txt_fecha_b.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    if (fechaBaja <= DateTime.Now)
                    {

                        serviceAlumno.ModificarBaja(hddnIdAlumno.Value, Gridtbaap.SelectedRow.Cells[1].Text, ddl_periodo.SelectedValue, ddl_tipo_baja.SelectedValue, txt_fecha_b.Text, checked_input2.Value, "", Session["usuario"].ToString());
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                        ddl_periodo.SelectedIndex = 0;
                        ddl_tipo_baja.SelectedIndex = 0;
                        txt_fecha_b.Text = string.Empty;
                        linkBttnCancelarModificar.Visible = false;
                        linkBttnModificar.Visible = false;
                        linkBttnCancelar.Visible = true;
                        linkBttnGuardar.Visible = true;
                        checked_input2.Value = "N";
                        Gridtbaap.SelectedIndex = -1;
                        grid_bind_tbaap();
                    }
                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error_fecha_baja", "error_fecha_baja();", true);
                }
            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tpais", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
            }
        }

        protected void linkBttnCancelar_Click(object sender, EventArgs e)
        {
            if (ddl_programa.SelectedValue != "")
            {
                containerGridAlumnos.Visible = true;
                containerDetalleBaja.Visible = false;
                ddl_programa.SelectedIndex = 0;
                ddl_programa.Enabled = true;
                linkBttnBusca.Visible = true;
                txt_nombre.Text = string.Empty;
                txt_matricula.Text = string.Empty;
                ddl_periodo.SelectedIndex = 0;
                ddl_tipo_baja.SelectedIndex = 0;
                txt_fecha_b.Text = string.Empty;
                Gridtpers.SelectedIndex = -1;
                //txt_matricula.Enabled = true;
                txt_matricula.Focus();
            }


        }
        protected void grid_bind_tbaap()
        {
            try
            {
                Gridtbaap.DataSource = null;
                Gridtbaap.DataBind();
                Gridtbaap.DataSource = serviceCatalogo.ObtenerBajas(txt_matricula.Text, ddl_programa.SelectedValue);
                Gridtbaap.DataBind();
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tbaap", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                //Response.Redirect("Inicio.aspx");
            }
        }

        protected void ddl_programa_SelectedIndexChanged(object sender, EventArgs e)
        {
            containerGridAlumnos.Visible = false;
            containerDetalleBaja.Visible = true;
            if (ddl_programa.SelectedValue == "")
                ddl_programa.Enabled = true;
            else
            {
                ddl_programa.Enabled = false;
                grid_bind_tbaap();
            }
            //txt_matricula.Enabled = false;
            linkBttnCancelar.Visible = true;
            linkBttnGuardar.Visible = true;
            linkBttnCancelarModificar.Visible = false;
            linkBttnModificar.Visible = false;
            linkBttnBusca.Visible = false;
        }

        protected void ddl_periodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            grid_bind_tbaap();
        }

        protected void ddl_tipo_baja_SelectedIndexChanged(object sender, EventArgs e)
        {
            grid_bind_tbaap();
        }

        protected void linkBttnBuscar_Click(object sender, EventArgs e)
        {
            //grid_bind_tbaap();
            ddl_programa.Enabled = true;
            Gridtbaap.DataSource = null;
            Gridtbaap.DataBind();
        }

        protected void Gridtpers_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txt_matricula.Text = Gridtpers.SelectedRow.Cells[2].Text;
                txt_nombre.Text = HttpUtility.HtmlDecode(Gridtpers.SelectedRow.Cells[3].Text + " " + Gridtpers.SelectedRow.Cells[4].Text + " " + Gridtpers.SelectedRow.Cells[5].Text);
                Gridtpers.Visible = false;
                containerDetalleBaja.Visible = true;
                ddl_programa.DataSource = serviceAlumno.ObtenerProgramaAlumno2(txt_matricula.Text);
                ddl_programa.DataValueField = "Clave";
                ddl_programa.DataTextField = "Descripcion";
                ddl_programa.DataBind();

                ddl_periodo.DataSource = serviceAlumno.ObtenerPeriodoAlumno(txt_matricula.Text);
                ddl_periodo.DataValueField = "Clave";
                ddl_periodo.DataTextField = "Descripcion";
                ddl_periodo.DataBind();

                //if (customSwitches2.Checked == true)
                //    txt_nombre.Text = "pruebas";

                linkBttnCancelar.Visible = true;
                linkBttnGuardar.Visible = true;
                linkBttnCancelarModificar.Visible = false;
                linkBttnModificar.Visible = false;
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tbaap", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void linkBttnCancelarModificar_Click(object sender, EventArgs e)
        {
            linkBttnCancelar.Visible = true;
            linkBttnGuardar.Visible = true;
            linkBttnCancelarModificar.Visible = false;
            linkBttnModificar.Visible = false;

            ddl_periodo.SelectedIndex = 0;
            ddl_tipo_baja.SelectedIndex = 0;
            txt_fecha_b.Text = string.Empty;
            Gridtbaap.SelectedIndex = -1;

            checked_input2.Value = "N";
            //chkEstimacion.Checked = false;
        }

        protected void customSwitches2_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}