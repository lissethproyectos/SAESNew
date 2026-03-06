using iTextSharp.text;
using MySqlX.XDevAPI.Common;
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
    public partial class treti : System.Web.UI.Page
    {
        #region <Variables>
        //Utilidades utils = new Utilidades();
        AlumnoService serviceAlumno = new AlumnoService();
        List<ModelObtenPaisesResponse> lstPaises = new List<ModelObtenPaisesResponse>();
        Catalogos serviceCatalogo = new Catalogos();
        CatOpcionesTitulacion serviceTitulacion = new CatOpcionesTitulacion();
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

                    txt_matricula.Text = Global.cuenta;
                    txt_matricula.Text = Global.cuenta;
                    txt_nombre.Text = Global.nombre + " " + Global.ap_paterno + " " + Global.ap_materno;
                    LlenaPagina();
                    //if (txt_matricula.Text != null)
                    //{
                    //    combo_contacto();
                    //}

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
                objPermiso = servicePermiso.ObtenerPermisoFormulario(Session["usuario"].ToString(), "treti");
                if (objPermiso != null)
                {
                    if (objPermiso.usme_update == "0" || objPermiso.usme_select == "0")
                    {
                        btn_treti.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                    }
                    else
                    {
                        combo_niveles();
                        Carga_Programa();
                        Carga_Periodo();
                        //grid_bind_treti();
                    }
                }
                else
                {
                    btn_treti.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                }


            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "treti", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);

            }
        }

        protected void grid_treti_bind()
        {
            List<ModelRegTitulacionResponse> lst = new List<ModelRegTitulacionResponse>();
            List<ModelComun> lstStatus = new List<ModelComun>();
            try
            {
                if (txt_matricula.Text != null)
                {
                    lstStatus = serviceTitulacion.ObtenStatusTreti();


                    lst = serviceTitulacion.ObtenRegistroTitulacion(txt_matricula.Text, ddl_programa.SelectedValue);
                    Gridtreti.DataSource = lst;
                    Gridtreti.DataBind();

                    var result = lstStatus.Where(p => lst.All(p2 => p2.treti_status != p.IdStr));

                    ddl_estatus.DataSource = result;
                    ddl_estatus.DataValueField = "IdStr";
                    ddl_estatus.DataTextField = "Descripcion";
                    ddl_estatus.DataBind();

                    //var lstStatus = new[] { "I", "R", "B", "T" };




                    if (Gridtreti.Rows.Count == 0)
                    {
                        ddl_estatus.SelectedValue = "I";
                        ddl_estatus.Enabled = false;
                    }
                    else
                    {
                        //ddl_estatus.SelectedValue = "I";
                        ddl_estatus.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "treti", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void combo_estatus()
        {
            ddl_estatus.DataSource = serviceCatalogo.obtenNivelesActivos();
            ddl_estatus.DataValueField = "clave";
            ddl_estatus.DataTextField = "nombre";
            ddl_estatus.DataBind();
        }

        protected void combo_niveles()
        {
            ddl_nivel.DataSource = serviceCatalogo.obtenNivelesActivos();
            ddl_nivel.DataValueField = "clave";
            ddl_nivel.DataTextField = "nombre";
            ddl_nivel.DataBind();
        }
        protected void combo_opciones_titulacion()
        {
            if (ddl_nivel.SelectedValue != null)
            {
                ddl_opc_titulacion.DataSource = serviceCatalogo.obtenOpcTitulacionNivel(ddl_nivel.SelectedValue);
                ddl_opc_titulacion.DataValueField = "Clave";
                ddl_opc_titulacion.DataTextField = "Descripcion";
                ddl_opc_titulacion.DataBind();
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
                Global.inserta_log(mensaje_error, "treti", Session["usuario"].ToString());
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
                    Carga_Programa();
                    Carga_Periodo();
                }
                else if (dtAlumno.Rows.Count > 1)
                {
                    GridAlumnos.Visible = true;
                    GridAlumnos.DataSource = dtAlumno;
                    GridAlumnos.DataBind();
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
                Global.inserta_log(mensaje_error, "treti", Session["usuario"].ToString());
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
            btn_save.Visible = true;
            btn_update.Visible = false;
            btn_cancel_save.Visible = true;
            btn_cancel_update.Visible = false;

            Global.cuenta = txt_matricula.Text;
            Global.nombre = HttpUtility.HtmlDecode(row.Cells[2].Text);
            Global.ap_paterno = HttpUtility.HtmlDecode(row.Cells[3].Text);
            Global.ap_materno = HttpUtility.HtmlDecode(row.Cells[4].Text);
            Carga_Programa();
            Carga_Periodo();
            //Carga_Estudiante();
        }

        protected void Carga_Programa()
        {
            List<ModelObtenerProgsAlumnoResponse> lst = new List<ModelObtenerProgsAlumnoResponse>();
            DataTable dt = new DataTable();
            hddnNivel.Value = string.Empty;

            Session["listaNiveles"] = null;
            try
            {
                if (txt_matricula.Text != "")
                {
                    lst = serviceAlumno.ObtenerProgramaAlumno2(txt_matricula.Text);

                    ddl_programa.DataSource = lst; // serviceAlumno.ObtenerProgramaAlumno2(txt_matricula.Text);
                    ddl_programa.DataValueField = "Clave";
                    ddl_programa.DataTextField = "Descripcion";
                    ddl_programa.DataBind();

                    Session["listaNiveles"] = lst;
                    //lstProductosAgregados = (List<Bien>)Session["listaProductosAgregados"];

                    ddl_programa_SelectedIndexChanged(null, null);


                    ddl_campus.DataSource = serviceCatalogo.ObtenerCampusVigentes();
                    ddl_campus.DataValueField = "clave";
                    ddl_campus.DataTextField = "campus";
                    ddl_campus.DataBind();

                    ddl_campus.SelectedValue = lst[0].testu_tcamp_clave;
                }
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "treti", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                //Response.Redirect("Inicio.aspx");
            }
        }

        protected void grid_bind_treti()
        {
            try
            {
                Gridtreti.DataSource = null;
                Gridtreti.DataBind();
                Gridtreti.DataSource = serviceTitulacion.ObtenRegistroTitulacion(txt_matricula.Text, ddl_programa.SelectedValue);
                Gridtreti.DataBind();
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "treti", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                //Response.Redirect("Inicio.aspx");
            }
        }

        protected void btn_cancel_save_Click(object sender, EventArgs e)
        {
            txt_matricula.Text = null;
            txt_matricula.ReadOnly = false;
            txt_nombre.Text = string.Empty;
            hddnNivel.Value = string.Empty;

            ddl_programa.Items.Clear();
            ddl_nivel.Items.Clear();
            ddl_opc_titulacion.SelectedIndex = 0;
            ddl_estatus.SelectedIndex = 0;
            txt_foja.Text = null;
            txt_cedula.Text = null;
            txt_libro.Text = null;

            btn_cancel_update.Visible = false;
            btn_cancel_save.Visible = true;
            btn_save.Visible = true;

        }

        protected void btn_cancel_update_Click(object sender, EventArgs e)
        {
            //txt_matricula.ReadOnly = false;
            //ddl_programa.Enabled = true;
            ddl_opc_titulacion.SelectedIndex = 0;
            ddl_estatus.SelectedIndex = 0;
            ddl_estatus_SelectedIndexChanged(null, null);


            ddl_programa.Items.Clear();
            ddl_programa.Items.Insert(0, "-------");

            ddl_nivel.Items.Clear();
            ddl_nivel.Items.Insert(0, "-------");


            ddl_periodo.Items.Clear();
            ddl_periodo.Items.Insert(0, "-------");

            txt_foja.Text = null;
            txt_cedula.Text = null;
            txt_libro.Text = null;
            Gridtreti.SelectedIndex = -1;

            btn_cancel_update.Visible = false;
            btn_cancel_save.Visible = true;
            btn_save.Visible = true;
            btn_update.Visible = false;
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {

            ModelInsTretiResponse objExiste = new ModelInsTretiResponse();
            if (Page.IsValid == true)
            {
                try
                {
                    objExiste = serviceTitulacion.Ins_treti(txt_matricula.Text, ddl_programa.SelectedValue, ddl_opc_titulacion.SelectedValue, ddl_estatus.SelectedValue,
                        Session["usuario"].ToString(), txt_foja.Text, txt_libro.Text, txt_cedula.Text, ddl_nivel.SelectedValue, ddl_periodo.SelectedValue, ddl_campus.SelectedValue);
                    if (objExiste != null)
                    {
                        if (objExiste.Existe == "0")
                        {
                            grid_treti_bind();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                        }
                        else if (objExiste.Existe == "1")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validarClave('ContentPlaceHolder1_ddl_programa',1);", true);
                        }
                        else if (objExiste.Existe == "4")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "valida_creditos", "valida_creditos('" + objExiste.TotCreditos + "','" + objExiste.TotCreditosCubiertos + "');", true);
                        }
                        else if (objExiste.Existe == "5")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "valida_promedio", "valida_promedio('" + objExiste.PromedioRequerido + "','" + objExiste.Promedio + "');", true);
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
                    Global.inserta_log(mensaje_error, "treti", Session["usuario"].ToString());
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
                    serviceTitulacion.Upd_treti(txt_matricula.Text, ddl_programa.SelectedValue, ddl_opc_titulacion.SelectedValue, ddl_estatus.SelectedValue,
                        Session["usuario"].ToString(), txt_foja.Text, txt_libro.Text, txt_cedula.Text, ddl_periodo.SelectedValue);
                    ddl_opc_titulacion.SelectedIndex = 0;
                    ddl_estatus.SelectedIndex = 0;
                    txt_foja.Text = null;
                    txt_cedula.Text = null;
                    txt_libro.Text = null;
                    grid_treti_bind();
                    Gridtreti.SelectedIndex = -1;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                }
                catch (Exception ex)
                {
                    string test = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "treti", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                }
            }
            else
            {
                this.Page.Validate();
            }
        }

        protected void ddl_programa_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<ModelObtenerProgsAlumnoResponse> lst = new List<ModelObtenerProgsAlumnoResponse>();
            try
            {
                if (Session["listaNiveles"] != null)
                {

                    lst = (List<ModelObtenerProgsAlumnoResponse>)Session["listaNiveles"];
                    if (lst.Count > 0)
                    {
                        ddl_nivel.SelectedValue = lst[ddl_programa.SelectedIndex].tprog_tnive_clave;
                        combo_opciones_titulacion();
                    }
                }
                grid_treti_bind();
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "treti", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                //Response.Redirect("Inicio.aspx");
            }
        }

        protected void Gridtreti_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<ModelComun> lstStatus = new List<ModelComun>();
            GridViewRow row = Gridtreti.SelectedRow;
            try
            {
                txt_matricula.ReadOnly = true;
                ddl_programa.Enabled = true;
                ddl_opc_titulacion.SelectedValue = row.Cells[1].Text;
                lstStatus = serviceTitulacion.ObtenStatusTreti();
                ddl_estatus.DataSource = lstStatus;
                ddl_estatus.DataValueField = "IdStr";
                ddl_estatus.DataTextField = "Descripcion";
                ddl_estatus.DataBind();


                ddl_estatus.SelectedValue = row.Cells[6].Text;
                ddl_estatus_SelectedIndexChanged(null, null);
                txt_foja.Text = HttpUtility.HtmlDecode(row.Cells[7].Text); //row.Cells[7].Text;
                txt_cedula.Text = HttpUtility.HtmlDecode(row.Cells[8].Text); //row.Cells[8].Text;
                txt_libro.Text = HttpUtility.HtmlDecode(row.Cells[9].Text); //row.Cells[9].Text;
                ddl_periodo.SelectedValue = HttpUtility.HtmlDecode(row.Cells[10].Text); //row.Cells[9].Text;

                btn_cancel_update.Visible = true;
                btn_cancel_save.Visible = false;
                btn_save.Visible = false;
                btn_update.Visible = true;
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "treti", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void Carga_Periodo()
        {
            ddl_periodo.DataSource = serviceAlumno.ObtenerPeriodoAlumno(txt_matricula.Text); // serviceCatalogo.ObtenerPeriodosEscolaresVigentes();
            ddl_periodo.DataValueField = "Clave";
            ddl_periodo.DataTextField = "Descripcion";
            ddl_periodo.DataBind();
        }

        protected void ddl_estatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_estatus.SelectedValue == "T")
                rowInicial.Visible = true;
            else
                rowInicial.Visible = false;
        }
    }
}