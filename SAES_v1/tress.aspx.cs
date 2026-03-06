using SAES_DBO.Models;
using SAES_Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using static SAES_DBO.Models.ModelAlumno;
using static SAES_DBO.Models.ModelMenu;
using static SAES_DBO.Models.ModelServicioSocial;

namespace SAES_v1
{
    public partial class tress : System.Web.UI.Page
    {
        #region <Variables>
        //Utilidades utils = new Utilidades();
        AlumnoService serviceAlumno = new AlumnoService();
        List<ModelObtenPaisesResponse> lstPaises = new List<ModelObtenPaisesResponse>();
        Catalogos serviceCatalogo = new Catalogos();
        ServicioSocialService serviceServicioSocial = new ServicioSocialService();
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
                    if (Global.cuenta != null)
                    {
                        txt_matricula.Text = Global.cuenta;
                        txt_matricula.Text = Global.cuenta;
                        txt_nombre.Text = Global.nombre + " " + Global.ap_paterno + " " + Global.ap_materno;


                    }
                    LlenaPagina();
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_alumnos", "load_datatable_alumnos();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ctrl_fecha_inicio", "ctrl_fecha_inicio();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ctrl_fecha_final", "ctrl_fecha_final();", true);
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
                objPermiso = servicePermiso.ObtenerPermisoFormulario(Session["usuario"].ToString(), "tress");
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
                        Carga_Programas_Disponibles("-1");
                        grid_bind_tress();
                        //grid_bind_treti();
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
                Global.inserta_log(mensaje_error, "tress", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);

            }
        }

        protected void grid_bind_tress()
        {
            try
            {
                Gridtress.DataSource = null;
                Gridtress.DataBind();
                Gridtress.DataSource = serviceServicioSocial.obtenGridQryTress(txt_matricula.Text, ddl_programa.SelectedValue);
                Gridtress.DataBind();
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tress", Session["usuario"].ToString());
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
                Global.inserta_log(mensaje_error, "tress", Session["usuario"].ToString());
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
                    Carga_Programas_Disponibles("-1");
                    grid_bind_tress();
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
                    Gridtress.DataSource = null;
                    Gridtress.DataBind();
                    txt_matricula.Focus();
                }
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tress", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }

        }

        public void Carga_Programas_Disponibles(string programa)
        {
            try
            {
                ddl_programa_ss.DataSource = serviceServicioSocial.obtenPeriodosDisponibles(txt_matricula.Text, programa); // serviceCatalogo.ObtenerPeriodosEscolaresVigentes();
                ddl_programa_ss.DataValueField = "clave";
                ddl_programa_ss.DataTextField = "descripcion";
                ddl_programa_ss.DataBind();
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tress", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }
        protected void Carga_Periodos()
        {
            try
            {
                ddl_periodo.DataSource = serviceCatalogo.ObtenerPeriodosEscolaresVigentes();
                ddl_periodo.DataValueField = "clave";
                ddl_periodo.DataTextField = "nombre";
                ddl_periodo.DataBind();
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tress", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
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
                //lstProductosAgregados = (List<Bien>)Session["listaProductosAgregados"];

                ddl_programa_SelectedIndexChanged(null, null);
                //if (dt.Rows.Count > 1)
                //{
                //    ddl_nivel.SelectedValue = dt.Rows[1][2].ToString();
                //    hddnNivel.Value = dt.Rows[1][2].ToString();
                //}
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "treti", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                //Response.Redirect("Inicio.aspx");
            }
        }


        protected void ddl_programa_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddl_periodo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btn_cancel_save_Click(object sender, EventArgs e)
        {
            try
            {

                txt_matricula.Text = null;
                txt_nombre.Text = null;
                ddl_programa.Items.Clear();
                ddl_programa.Items.Add(new ListItem("-------", ""));
                ddl_programa.DataSource = null;
                ddl_programa.DataBind();
                Gridtress.DataSource = null;
                Gridtress.DataBind();
                txt_matricula.Focus();




                ddl_periodo.SelectedIndex = 0;
                ddl_modalidad.SelectedIndex = 0;
                txt_horas.Text = null;
                txt_horas_cumplidas.Text = null;
                ddl_estatus.SelectedIndex = 0;
                btn_update.Visible = false;
                btn_cancel_update.Visible = false;
                //txt_fecha_inicio.Text = null;
                //txt_fecha_fin.Text = null;


            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tress", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
            }
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            ModelInsertarTressResponse objExiste = new ModelInsertarTressResponse();
            if (Page.IsValid == true)
            {
                try
                {
                            objExiste = serviceServicioSocial.InsertarTress(ddl_periodo.SelectedValue, txt_matricula.Text, ddl_programa.SelectedValue, ddl_programa_ss.SelectedValue,
                        txt_fecha_inicio.Text, txt_fecha_fin.Text, ddl_modalidad.SelectedValue, txt_horas.Text, txt_horas_cumplidas.Text, ddl_estatus.SelectedValue, Session["usuario"].ToString());
                    if (objExiste != null)
                    {
                        if (objExiste.Existe == "0")
                        {
                            if (objExiste.CreditosValidados == "0")
                            {
                                grid_bind_tress();
                                ddl_periodo.SelectedIndex = 0;
                                ddl_programa_ss.SelectedIndex = 0;
                                txt_fecha_inicio.Text = null;
                                ddl_modalidad.SelectedIndex = 0;
                                txt_horas.Text = null;
                                txt_horas_cumplidas.Text = null;
                                ddl_estatus.SelectedIndex = 0;
                                Carga_Programas_Disponibles("-1");
                                txt_fecha_inicio.Text = null;
                                //txt_fecha_fin.Text = null;
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "CreditosIncompletos('" + objExiste.CreditosAlum + "','" + objExiste.CreditosRequeridos + "');", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "registro_duplicado();", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                    }
                }
                catch (Exception ex)
                {
                    string test = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tress", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                }
            }
            else
            {
                this.Page.Validate();
            }
        }

        protected void btn_update_Click(object sender, EventArgs e)
        {
            if (Page.IsValid == true)
            {
                try
                {
                    serviceServicioSocial.EditarTress(ddl_periodo.SelectedValue, txt_matricula.Text, ddl_programa.SelectedValue, ddl_programa_ss.SelectedValue,
                        txt_fecha_inicio.Text, txt_fecha_fin.Text, ddl_modalidad.SelectedValue, txt_horas.Text, txt_horas_cumplidas.Text, ddl_estatus.SelectedValue, Session["usuario"].ToString());

                    grid_bind_tress();
                    ddl_periodo.SelectedIndex = 0;
                    ddl_programa_ss.SelectedIndex = 0;
                    txt_fecha_inicio.Text = null;
                    ddl_modalidad.SelectedIndex = 0;
                    txt_horas.Text = null;
                    txt_horas_cumplidas.Text = null;
                    ddl_estatus.SelectedIndex = 0;
                    Carga_Programas_Disponibles("-1");
                    txt_fecha_inicio.Text = null;
                    txt_fecha_fin.Text = null;
                    Gridtress.SelectedIndex = -1;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);

                }
                catch (Exception ex)
                {
                    string test = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tress", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                }
            }
            else
            {
                this.Page.Validate();
            }
        }

        protected void Gridtprss_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = Gridtress.SelectedRow;

            txt_matricula.ReadOnly = true;
            ddl_programa.Enabled = true;
            btn_cancel_update.Visible = true;
            btn_cancel_save.Visible = false;
            btn_save.Visible = false;
            btn_update.Visible = true;
        }

        protected void btn_cancel_update_Click(object sender, EventArgs e)
        {
            ddl_programa.Enabled = true;
            ddl_periodo.SelectedIndex = 0;
            ddl_programa_ss.SelectedIndex = 0;
            ddl_modalidad.SelectedIndex = 0;
            Carga_Programas_Disponibles("-1");
            txt_horas.Text = null;
            txt_horas_cumplidas.Text = null;
            ddl_estatus.SelectedIndex = 0;
            btn_save.Visible = true;
            btn_update.Visible = false;
            btn_cancel_save.Visible = true;
            btn_cancel_update.Visible = false;
            Gridtress.SelectedIndex = -1;

        }

        protected void Gridtress_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = Gridtress.SelectedRow;

            try
            {
                ddl_programa.Enabled = false;
                ddl_periodo.SelectedValue = row.Cells[1].Text;
                //ddl_programa_ss.SelectedValue = row.Cells[3].Text;
                ddl_modalidad.SelectedValue = row.Cells[8].Text;
                txt_horas.Text = row.Cells[6].Text;
                txt_horas_cumplidas.Text = row.Cells[7].Text;
                btn_save.Visible = false;
                btn_update.Visible = true;
                btn_cancel_save.Visible = false;
                btn_cancel_update.Visible = true;
                txt_fecha_inicio.Text = row.Cells[9].Text;

                txt_horas_cumplidas_TextChanged(null, null);


                ddl_estatus.SelectedValue = row.Cells[5].Text;
                //if (ddl_estatus.SelectedValue == "T")
                //    ddl_estatus_SelectedIndexChanged(null, null);


                txt_fecha_fin.Text = HttpUtility.HtmlDecode(row.Cells[10].Text);


                Carga_Programas_Disponibles(row.Cells[2].Text);
                if(ddl_programa_ss.Items.Count > 0 )
                    if (ddl_programa_ss.Items[0].Text!="")
                        ddl_programa_ss.SelectedValue = row.Cells[2].Text;


            }
            catch (Exception ex)
            {
                string test = ex.Message;
                   string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tress", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
            }
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
            Carga_Programas_Disponibles("-1");
            grid_bind_tress();
        }

        protected void txt_horas_cumplidas_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txt_horas_cumplidas.Text) >= Convert.ToInt32(txt_horas.Text))
            {
                //ddl_estatus.Items.Clear();
                ddl_estatus.Items.Add(new ListItem("Terminado", "T"));
                ddl_estatus.SelectedValue = "T";
            }
            else
            {
                ddl_estatus.Items.Remove(new ListItem("Terminado", "T"));
            }

            ddl_estatus_SelectedIndexChanged(null, null);
        }

        protected void ddl_estatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DateTime fechaFin = DateTime.ParseExact("", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime FechaActual = DateTime.Today;


            string Fecha = FechaActual.ToString("dd/MM/yyyy");


            if (ddl_estatus.SelectedValue == "T")
            {
                txt_fecha_fin.Text = null; // Convert.ToString(Fecha);
                colFechaFin.Visible = true;
            }
            else
            {

                colFechaFin.Visible = false;
            }
        }
    }
}