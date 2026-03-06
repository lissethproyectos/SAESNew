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
    public partial class testu : System.Web.UI.Page
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
              

                txt_matricula.Focus();
                if (Global.cuenta != "")
                {
                    txt_matricula.Text = Global.cuenta;
                    containerDetalle.Visible = true;
                    ddl_periodo.DataSource = serviceCatalogo.ObtenerPeriodosEscolaresVigentes();
                    ddl_periodo.DataValueField = "clave";
                    ddl_periodo.DataTextField = "nombre";
                    ddl_periodo.DataBind();
                    containerGridAlumnos.Visible = false;
                    grid_bind_testu();
                }
                if (Global.nombre != "")                
                    txt_nombre.Text = Global.nombre + " " + Global.ap_paterno + " " + Global.ap_materno;





                //ddl_programa.Items.Insert(0, new ListItem("-------", ""));
                //ddl_programa_new.Items.Insert(0, new ListItem("-------", ""));
                //ddl_periodo.Items.Insert(0, new ListItem("-------", ""));
                ddl_campus.DataSource = serviceCatalogo.obtenCampus();


                ddl_campus.DataValueField = "Clave";
                ddl_campus.DataTextField = "Descripcion";
                ddl_campus.DataBind();
                ddl_campus_SelectedIndexChanged(null, null);

                ddl_tasa.DataSource = serviceCatalogo.ObtenerComboComun("Tasas", "");
                ddl_tasa.DataValueField = "Clave";
                ddl_tasa.DataTextField = "Descripcion";
                ddl_tasa.DataBind();

                ddl_turno.DataSource = serviceCatalogo.ObtenerTurnos();
                ddl_turno.DataValueField = "Clave";
                ddl_turno.DataTextField = "Descripcion";
                ddl_turno.DataBind();

                ddl_estatus.DataSource = serviceCatalogo.ObtenerComboComun("Estatus_Alumno", "");
                ddl_estatus.DataValueField = "Clave";
                ddl_estatus.DataTextField = "Descripcion";
                ddl_estatus.DataBind();

                ddl_especialidad.DataSource = serviceCatalogo.ObtenerComboComun("Especialidad", "");
                ddl_especialidad.DataValueField = "Clave";
                ddl_especialidad.DataTextField = "Descripcion";
                ddl_especialidad.DataBind();

                //combo_tipo_baja();
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "GridAlumnos", "load_datatable_alumnos();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "GridTestu", "load_datatable_testu();", true);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "ctrl_fecha_f", "ctrl_fecha_f();", true);

        }

        protected void linkBttnBusca_Click(object sender, EventArgs e)
        {
            //ModelObtenerAlumnosResponse datosAlumno = new ModelObtenerAlumnosResponse();
            DataTable dtAlumno = new DataTable();

            try
            {
                //Gridtpers.DataSource = serviceAlumno.ObtenerAlumnos(txt_matricula.Text);// ("1", "202065")  
                //Gridtpers.DataBind();
                containerGridAlumnos.Visible = false;
                containerDetalle.Visible = true;
                linkBttnCancelarGridTpers.Visible = true;
                txt_matricula.Attributes.Add("disabled", "");  //Enabled = false;
                txt_nombre.Attributes.Add("disabled", "");  //Enabled = false;

                linkBttnCancelar.Visible = true;
                linkBttnGuardar.Visible = true;
                linkBttnCancelarModificar.Visible = false;
                linkBttnModificar.Visible = false;
                linkBttnBusca.Visible = false;


                dtAlumno = serviceAlumno.ObtenerAlumnos(txt_matricula.Text);
                if (dtAlumno.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_busqueda();", true);

                }
                else if (dtAlumno.Rows.Count == 1)
                {
                    txt_nombre.Text = dtAlumno.Rows[0][1].ToString() + " " + dtAlumno.Rows[0][2].ToString() + " " + dtAlumno.Rows[0][3].ToString();
                    //ddl_programa.DataSource = serviceAlumno.ObtenerProgramaAlumnoMatricula(txt_matricula.Text);
                    //ddl_programa.DataValueField = "Clave";
                    //ddl_programa.DataTextField = "Descripcion";
                    //ddl_programa.DataBind();
                    ddl_periodo.DataSource = serviceCatalogo.ObtenerPeriodosEscolaresVigentes();
                    ddl_periodo.DataValueField = "clave";
                    ddl_periodo.DataTextField = "nombre";
                    ddl_periodo.DataBind();
                    containerGridAlumnos.Visible = false;
                    grid_bind_testu();
                }
                else if (dtAlumno.Rows.Count > 1)
                {
                    Gridtpers.DataSource = serviceAlumno.ObtenerAlumnos("");// ("1", "202065")  
                    Gridtpers.DataBind();
                    containerGridAlumnos.Visible = true;
                    containerDetalle.Visible = false;
                    txt_nombre.Text = string.Empty;

                }

            }
            catch (Exception ex)
            {
                //Logs
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "testa", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }



        //protected void linkBttnBusca_Click(object sender, EventArgs e)
        //{
        //    ModelObtenerDatosAlumnoResponse datosAlumno = new ModelObtenerDatosAlumnoResponse();
        //    try
        //    {
        //        datosAlumno = serviceAlumno.ObtenerDatosAlumno(txt_matricula.Text);// ("1", "202065")  
        //        if (datosAlumno != null)
        //        {
        //            txt_nombre.Text = datosAlumno.tpers_nombre;
        //            hddnIdAlumno.Value = datosAlumno.tpers_clave;
        //            ddl_programa.DataSource = serviceAlumno.ObtenerProgramaAlumno(datosAlumno.tpers_clave);
        //            ddl_programa.DataValueField = "Clave";
        //            ddl_programa.DataTextField = "Descripcion";
        //            ddl_programa.DataBind();

        //            ddl_periodo.DataSource = serviceAlumno.ObtenerPeriodoAlumno(datosAlumno.tpers_clave);
        //            ddl_periodo.DataValueField = "Clave";
        //            ddl_periodo.DataTextField = "Descripcion";
        //            ddl_periodo.DataBind();


        //        }
        //        else
        //        {
        //            hddnIdAlumno.Value = "0";
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_busqueda();", true);
        //            txt_nombre.Text = string.Empty;

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //Logs
        //        string mensaje_error = ex.Message.Replace("'", "-");
        //        Global.inserta_log(mensaje_error, "testa", Session["usuario"].ToString());
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
        //    }
        //}

        protected void ddl_programa_SelectedIndexChanged(object sender, EventArgs e)
        {
            containerGridAlumnos.Visible = false;
            containerDetalle.Visible = true;
            //ddl_programa.Attributes.Add("disabled", "");  //Enabled = false;
            txt_matricula.Attributes.Add("disabled", "");  //Enabled = false;
            txt_nombre.Attributes.Add("disabled", "");  //Enabled = false;
            ddl_periodo.DataSource = serviceAlumno.ObtenerPeriodoAlumno(txt_matricula.Text);
            ddl_periodo.DataValueField = "Clave";
            ddl_periodo.DataTextField = "Descripcion";
            ddl_periodo.DataBind();
            grid_bind_testu();
            linkBttnCancelar.Visible = true;
            linkBttnGuardar.Visible = true;
            linkBttnCancelarModificar.Visible = false;
            linkBttnModificar.Visible = false;
            linkBttnBusca.Visible = false;




        }

        protected void ddl_periodo_SelectedIndexChanged(object sender, EventArgs e)
        {



        }

        protected void ddl_campus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
               
                    ddl_programa_new.DataSource = serviceCatalogo.obtenPrograma(ddl_campus.SelectedValue, "");
                    ddl_programa_new.DataValueField = "Clave";
                    ddl_programa_new.DataTextField = "Descripcion";
                    ddl_programa_new.DataBind();
                //}
                //else
                //{
                //    ddl_programa_new.DataSource = null;
                //    ddl_programa_new.DataBind();
                //}
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "testu", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }

        }
        protected void grid_bind_testu()
        {
            try
            {
                //ddl_programa.DataSource = serviceAlumno.ObtenerProgramaAlumno(Gridtpers.SelectedRow.Cells[1].Text);
                //ddl_programa.DataValueField = "Clave";
                //ddl_programa.DataTextField = "Descripcion";
                //ddl_programa.DataBind();


                GridTestu.DataSource = serviceAlumno.ObtenerTestu(txt_matricula.Text);// ("1", "202065")  
                GridTestu.DataBind();




            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "testu", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void linkBttnCancelar_Click(object sender, EventArgs e)
        {
            containerDetalle.Visible = false;
            containerGridAlumnos.Visible = true;

            //ddl_programa.Attributes.Remove("disabled");
            txt_matricula.Attributes.Remove("disabled");
            txt_nombre.Attributes.Remove("disabled");
            linkBttnCancelarGridTpers.Visible = false;

            //ddl_programa.Items.Clear();
            //ddl_programa.Items.Insert(0, new ListItem("-------", ""));
            //ddl_programa.SelectedIndex = 0;
            txt_matricula.Text = string.Empty;
            linkBttnBusca.Visible = true;
            txt_nombre.Text = string.Empty;

            Gridtpers.DataSource = null;
            Gridtpers.DataBind();
            Gridtpers.SelectedIndex = -1;

            GridTestu.DataSource = null;
            GridTestu.DataBind();
            GridTestu.SelectedIndex = -1;

            txt_matricula.Focus();

            //GridTestu.SelectRow(-1);
            //GridTestu_SelectedIndexChanged(null, null);
        }

        protected void linkBttnGuardar_Click(object sender, EventArgs e)
        {
            ModelInsertarTestuResponse objExisteRegistro = new ModelInsertarTestuResponse();

            //if (ddl_programa.SelectedValue != "")cance
            //{
            try
            {
                objExisteRegistro = serviceAlumno.InsertarTestu(txt_matricula.Text, ddl_periodo.SelectedValue, ddl_campus.SelectedValue,
                    ddl_programa_new.SelectedValue, ddl_tasa.SelectedValue, ddl_estatus.SelectedValue, txt_generacion.Text,
                    ddl_especialidad.SelectedValue,
                    //ddl_programa_new.SelectedValue, 
                    ddl_turno.SelectedValue, Session["usuario"].ToString(),
                    checked_input2.Value);
                    //customSwitches2.SelectedValue);
                //checked_input2.Value);

                if (objExisteRegistro != null)
                {
                    if (objExisteRegistro.Existe == "0")
                    {
                        serviceAlumno.GeneraCuotas(GridTestu.SelectedRow.Cells[1].Text, txt_matricula.Text,
                            ddl_campus.SelectedValue, ddl_programa_new.SelectedValue,
                            Session["usuario"].ToString()
                            );

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                        ddl_periodo.SelectedIndex = 0;
                        ddl_campus.SelectedIndex = 0;
                        ddl_programa_new.SelectedIndex = 0;
                        ddl_tasa.SelectedIndex = 0;
                        ddl_turno.SelectedIndex = 0;
                        txt_generacion.Text = string.Empty;
                        ddl_estatus.SelectedIndex = 0;
                        grid_bind_testu();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error_insertar", "error_insertar();", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                }

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "testu", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
            }
            //}
            //else
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);

        }



        protected void Gridtpers_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txt_matricula.Text = Gridtpers.SelectedRow.Cells[2].Text;
                txt_nombre.Text = Gridtpers.SelectedRow.Cells[3].Text + " " + Gridtpers.SelectedRow.Cells[4].Text + " " + Gridtpers.SelectedRow.Cells[5].Text;

                containerGridAlumnos.Visible = false;
                containerDetalle.Visible = true;
                txt_matricula.Attributes.Add("disabled", "");  //Enabled = false;
                txt_nombre.Attributes.Add("disabled", "");  //Enabled = false;


                linkBttnCancelar.Visible = true;
                linkBttnGuardar.Visible = true;
                linkBttnCancelarModificar.Visible = false;
                linkBttnModificar.Visible = false;
                linkBttnBusca.Visible = false;

                ddl_periodo.DataSource = serviceCatalogo.ObtenerPeriodosEscolaresVigentes();
                ddl_periodo.DataValueField = "clave";
                ddl_periodo.DataTextField = "nombre";
                ddl_periodo.DataBind();

                grid_bind_testu();

                linkBttnCancelar.Visible = true;
                linkBttnGuardar.Visible = true;
                linkBttnCancelarModificar.Visible = false;
                linkBttnModificar.Visible = false;



            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "testu", Session["usuario"].ToString());
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
            ddl_campus.SelectedIndex = 0;
            ddl_programa_new.SelectedIndex = 0;
            ddl_turno.SelectedIndex = 0;
            txt_generacion.Text = string.Empty;
            //ddl_periodo.Enabled = true;
            ddl_programa_new.Enabled = true;
            ddl_estatus.SelectedIndex = 0;
            GridTestu.SelectedIndex = -1;
            ddl_periodo.DataSource = serviceCatalogo.ObtenerPeriodosEscolaresVigentes();
            ddl_periodo.DataValueField = "clave";
            ddl_periodo.DataTextField = "nombre";
            ddl_periodo.DataBind();
        }

        protected void GridTestu_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddl_periodo.DataSource = serviceCatalogo.ObtenerPeriodosEscolares();
                ddl_periodo.DataValueField = "clave";
                ddl_periodo.DataTextField = "nombre";
                ddl_periodo.DataBind();
                ddl_periodo.SelectedValue = GridTestu.SelectedRow.Cells[1].Text;
                ddl_campus.SelectedValue = GridTestu.SelectedRow.Cells[2].Text;
                ddl_campus_SelectedIndexChanged(null, null);
                ddl_programa_new.SelectedValue = GridTestu.SelectedRow.Cells[3].Text;
                ddl_turno.SelectedValue = GridTestu.SelectedRow.Cells[5].Text;
                txt_generacion.Text = HttpUtility.HtmlDecode(GridTestu.SelectedRow.Cells[6].Text); //GridTestu.SelectedRow.Cells[6].Text;
                ddl_estatus.SelectedValue = GridTestu.SelectedRow.Cells[8].Text;
                ddl_tasa.SelectedValue = GridTestu.SelectedRow.Cells[11].Text;
                //if (GridTestu.SelectedRow.Cells[5].Text == "S")
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "habilitar_calcular_devolucion", "habilitar_calcular_cuotas('S');", true);
                //    checked_input2.Value = "S";
                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "habilitar_calcular_devolucion", "habilitar_calcular_cuotas('N');", true);

                //    checked_input2.Value = "N";
                //}
                linkBttnModificar.Visible = true;
                linkBttnCancelarModificar.Visible = true;
                linkBttnCancelar.Visible = false;
                linkBttnGuardar.Visible = false;
                //ddl_periodo.Enabled = false;
                ddl_programa_new.Enabled = false;
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "testu", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void linkBttnModificar_Click(object sender, EventArgs e)
        {
            ModelEditarTestuResponse objPeriodoValido = new ModelEditarTestuResponse();
            string calculo_cuotas = "N";
            try
            {
                int generacion = Convert.ToInt32(txt_generacion.Text);
                //if (checked_input2.Value == string.Empty)
                //    calculo_cuotas = "N";

                objPeriodoValido = serviceAlumno.EditarTestu(txt_matricula.Text, GridTestu.SelectedRow.Cells[1].Text,
                    ddl_campus.SelectedValue,
                       ddl_programa_new.SelectedValue, ddl_tasa.SelectedValue, ddl_estatus.SelectedValue, generacion, // Convert.ToInt32(txt_generacion.Text),
                       ddl_especialidad.SelectedValue, ddl_turno.SelectedValue, Session["usuario"].ToString(),
                       checked_input2.Value);
                       //customSwitches2.SelectedValue);

                if (objPeriodoValido != null)
                {
                    if (objPeriodoValido.PeriodoVigente != "0")
                    {

                        serviceAlumno.CalculaCuotas(GridTestu.SelectedRow.Cells[1].Text, txt_matricula.Text,
                            ddl_tasa.SelectedValue, ddl_campus.SelectedValue, ddl_programa_new.SelectedValue,
                            Session["usuario"].ToString()
                            );

                        linkBttnModificar.Visible = false;
                        linkBttnCancelarModificar.Visible = false;
                        linkBttnCancelar.Visible = true;
                        linkBttnGuardar.Visible = true;
                        linkBttnBusca.Visible = true;

                        ddl_periodo.SelectedIndex = 0;
                        ddl_campus.SelectedIndex = 0;
                        ddl_programa_new.SelectedIndex = 0;
                        ddl_tasa.SelectedIndex = 0;
                        ddl_turno.SelectedIndex = 0;
                        txt_generacion.Text = string.Empty;
                        ddl_estatus.SelectedIndex = 0;
                        //ddl_periodo.Enabled = true;
                        ddl_programa_new.Enabled = true;


                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                        grid_bind_testu();
                        GridTestu.SelectedIndex = -1;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error_clave", "error_editar();", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                }
            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "testu", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
            }
        }
    }
}