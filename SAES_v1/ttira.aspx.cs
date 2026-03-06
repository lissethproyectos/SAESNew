using applyWeb.Data;
using SAES_DBO.Models;
using SAES_Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using static SAES_DBO.Models.ModelAlumno;
using static SAES_DBO.Models.ModelCobranza;
using static SAES_DBO.Models.ModelMenu;

namespace SAES_v1
{
    public partial class ttira : System.Web.UI.Page
    {
        #region <Variables>
        //Utilidades utils = new Utilidades();
        AlumnoService serviceAlumno = new AlumnoService();
        List<ModelObtenPaisesResponse> lstPaises = new List<ModelObtenPaisesResponse>();
        Catalogos serviceCatalogo = new Catalogos();
        KardexAlumno serviceKardex = new KardexAlumno();
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
                    if (Global.cuenta != "")
                    {
                        txt_matricula.Text = Global.cuenta;
                        txt_nombre.Text = Global.nombre + " " + Global.ap_paterno + " " + Global.ap_materno;
                        Carga_Programas();
                        Carga_Periodos();
                        Carga_Campus();
                        Gridttira.DataSource = null;
                        Gridttira.DataBind();
                        Carga_Hora_Inicial();
                        Carga_Hora_Final();
                    }
                    //LlenaPagina();
                }



                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_alumnos", "load_datatable_alumnos();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_materias_disp", "load_datatable_materias_disp();", true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "ctrl_fecha_inicio", "ctrl_fecha_inicio();", true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "ctrl_fecha_final", "ctrl_fecha_final();", true);
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
                Global.inserta_log(mensaje_error, "ttira", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void Carga_Hora_Inicial()
        {
            ddl_hora_inicial.DataSource = serviceCatalogo.ObtenerHoras();
            ddl_hora_inicial.DataValueField = "clave";
            ddl_hora_inicial.DataTextField = "hora_ini";
            ddl_hora_inicial.DataBind();
        }

        protected void Carga_Hora_Final()
        {
            ddl_hora_final.DataSource = serviceCatalogo.ObtenerHoras();
            ddl_hora_final.DataValueField = "clave";
            ddl_hora_final.DataTextField = "hora_fin";
            ddl_hora_final.DataBind();
        }
        protected void Carga_Estudiante()
        {
            ModelObtenerAlumnosResponse datosAlumno = new ModelObtenerAlumnosResponse();
            txt_nombre.Text = string.Empty;
            ddl_programa.Items.Clear();

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
                    //grid_bind_tress();
                }
                else if (dtAlumno.Rows.Count > 1)
                {
                    GridAlumnos.Visible = true;
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "collapseAlumnos", "collapseAlumnos();", true);
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "collapseAlumnos", "$('#collapseAlumnos').collapse('toggle')", true);

                    //data - toggle = "collapse" href = "#collapseAlumnos" role = "button" aria - expanded = "false"

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
                    //Gridtress.DataSource = null;
                    //Gridtress.DataBind();
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
        protected void Carga_Campus()
        {
            try
            {
                ddl_campus.DataSource = serviceCatalogo.ObtenerCampusVigentes();
                ddl_campus.DataValueField = "clave";
                ddl_campus.DataTextField = "campus";
                ddl_campus.DataBind();
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
                //ddl_periodo.DataSource = serviceCatalogo.ObtenerPeriodosEscolaresVigentes();
                //ddl_periodo.DataValueField = "clave";
                //ddl_periodo.DataTextField = "nombre";.
                //ddl_periodo.DataBind();

                ddl_periodo.DataSource = serviceCatalogo.ObtenerPeriodosEscolaresVigentes(); // serviceAlumno.ObtenerPeriodoAlumno(txt_matricula.Text); // serviceCatalogo.ObtenerPeriodosEscolaresVigentes();
                ddl_periodo.DataValueField = "Clave";
                ddl_periodo.DataTextField = "nombre";
                ddl_periodo.DataBind();

                ddl_periodo_SelectedIndexChanged(null, null);

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
                if (lst.Count() >= 1)
                {
                    // ddl_programa.DataSource = lst; //
                    ddl_programa.DataSource = lst; // serviceAlumno.ObtenerProgramaAlumno(txt_matricula.Text);
                    ddl_programa.DataValueField = "Clave";
                    ddl_programa.DataTextField = "Descripcion";
                    ddl_programa.DataBind();
                    ddl_programa.SelectedValue = lst[0].Clave;

                    //lstProductosAgregados = (List<Bien>)Session["listaProductosAgregados"];



                    ddl_campus.DataSource = serviceCatalogo.ObtenerCampusVigentes();
                    ddl_campus.DataValueField = "clave";
                    ddl_campus.DataTextField = "campus";
                    ddl_campus.DataBind();
                    ddl_campus.SelectedValue = lst[0].testu_tcamp_clave;

                    ddl_nivel.DataSource = serviceCatalogo.obtenNivel(ddl_campus.SelectedValue);
                    ddl_nivel.DataValueField = "clave";
                    ddl_nivel.DataTextField = "descripcion";
                    ddl_nivel.DataBind();
                    ddl_nivel.SelectedValue = lst[0].tprog_tnive_clave;

                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta_programas", "error_consulta_programas();", true);

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
            Global.programa = ddl_programa.SelectedValue;
        }


        protected void GridAlumnos_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridAlumnos.SelectedRow;
            txt_matricula.Text = row.Cells[1].Text;
            txt_nombre.Text = HttpUtility.HtmlDecode(row.Cells[2].Text) + " " + HttpUtility.HtmlDecode(row.Cells[3].Text) + " " +
             HttpUtility.HtmlDecode(row.Cells[4].Text);
            GridAlumnos.Visible = false;
            //btn_save.Visible = true;
            //btn_update.Visible = false;
            //btn_cancel_save.Visible = true;
            //btn_cancel_update.Visible = false;

            Global.cuenta = txt_matricula.Text;
            Global.nombre = HttpUtility.HtmlDecode(row.Cells[2].Text);
            Global.ap_paterno = HttpUtility.HtmlDecode(row.Cells[3].Text);
            Global.ap_materno = HttpUtility.HtmlDecode(row.Cells[4].Text);
            Carga_Programas();
            Carga_Periodos();
            //grid_bind_tress();
        }

        protected void grid_bind_ttira()
        {
            try
            {
                Gridttira.DataSource = null;
                Gridttira.DataBind();
                if (ddl_programa.SelectedValue != "" && ddl_periodo.SelectedValue != "")
                {
                    Gridttira.DataSource = serviceKardex.ObtenerMateriasAlumno(txt_matricula.Text, ddl_programa.SelectedValue, ddl_periodo.SelectedValue);
                    Gridttira.DataBind();
                }
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "ttira", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                //Response.Redirect("Inicio.aspx");
            }
        }

        protected void grid_bind_tmaterias_disp()
        {
            try
            {
                Gridttira_Disp.DataSource = null;
                Gridttira_Disp.DataBind();
                Gridttira_Disp.DataSource = serviceKardex.ObtenerMateriasDisp(txt_matricula.Text, ddl_programa.SelectedValue, ddl_campus.SelectedValue, ddl_periodo.SelectedValue);
                Gridttira_Disp.DataBind();
            }


            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "ttira", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                //Response.Redirect("Inicio.aspx");
            }
        }
        protected void grid_bind_thora()
        {
            try
            {
                Gridthora.DataSource = null;
                Gridthora.DataBind();
                Gridthora.DataSource = serviceKardex.ObtenerHorarioAlumno(txt_matricula.Text, ddl_periodo.SelectedValue, ddl_campus.SelectedValue, "");
                Gridthora.DataBind();
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "ttira", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                //Response.Redirect("Inicio.aspx");
            }
        }

        protected void ddl_periodo_SelectedIndexChanged(object sender, EventArgs e)
        {

            grid_bind_ttira();
            grid_bind_thora();

        }

        protected void linkBttnAgregarMat_Click(object sender, EventArgs e)
        {
            try
            {
                grid_bind_tmaterias_disp();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "modalMaterias", "$('#modalMaterias').modal('show')", true);

            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "ttira", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                //Response.Redirect("Inicio.aspx");
            }
        }

        protected void Gridttira_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Gridttira.EditIndex = e.NewEditIndex;
            grid_bind_ttira();
        }

        protected void Gridttira_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            Gridttira.EditIndex = -1;
            grid_bind_ttira();
        }

        protected void Gridttira_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = Gridttira.Rows[e.RowIndex];
            DropDownList ddl = (DropDownList)(row.Cells[0].FindControl("ddl_estatus_edit"));
            //objBien.Id_Inventario = Gridttira.Rows[e.RowIndex].Cells[0].Text.ToString();
            try
            {
                serviceKardex.UpdEstatusMateria(txt_matricula.Text, ddl_periodo.SelectedValue,
                    ddl_programa.SelectedValue, ddl_campus.SelectedValue, row.Cells[0].Text, ddl.SelectedValue, row.Cells[2].Text, ddl_nivel.SelectedValue, Session["usuario"].ToString());
                Gridttira.EditIndex = -1;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                grid_bind_ttira();
                grid_bind_thora();

                //serviceKardex.ObtenGridStructureForInsert(txt_matricula.Text, ddl_programa.SelectedValue, ddl_opc_titulacion.SelectedValue, ddl_estatus.SelectedValue,
                //    Session["usuario"].ToString(), txt_foja.Text, txt_libro.Text, txt_cedula.Text, ddl_nivel.SelectedValue, ddl_periodo.SelectedValue, ddl_campus.SelectedValue);
                ////if (objExiste != null)
                //{
                //}

            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "ttira", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                //Response.Redirect("Inicio.aspx");
            }
        }

        protected void Gridttira_Disp_SelectedIndexChanged(object ender, EventArgs e)
        {
            GridViewRow row = Gridttira_Disp.SelectedRow;
            // txt_matricula.Text = row.Cells[1].Text;
            try
            {
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "ttira", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                //Response.Redirect("Inicio.aspx");
            }
        }
        protected void bttnAgregarMatAlum_Click(object sender, EventArgs e)
        {
            bool validados = false;
            bool totMaterias = false;
            ModelValidaTotMateriasResponse objValida = new ModelValidaTotMateriasResponse();



            try
            {
                foreach (GridViewRow row in Gridttira_Disp.Rows)
                {
                    CheckBox cbCons = (CheckBox)(row.Cells[0].FindControl("chkMatDisp"));
                    if (cbCons.Checked == true)
                    {
                        //objValida = serviceKardex.ValTotMaterias(txt_matricula.Text, ddl_campus.SelectedValue, ddl_programa.SelectedValue, ddl_periodo.SelectedValue, row.Cells[4].Text);
                        //if (Convert.ToInt32(objValida.Validado) == 0)

                        //   if (Convert.ToInt32(objValida.TotPropuestos)+1 <= Convert.ToInt32(objValida.TotMaterias))
                        //{
                            serviceKardex.InsertMateriaAlumno(txt_matricula.Text, ddl_periodo.SelectedValue, ddl_campus.SelectedValue,
                            ddl_programa.SelectedValue, row.Cells[1].Text, "SG", ddl_nivel.SelectedValue, Session["usuario"].ToString());
                            validados = true;
                            totMaterias = false;
                        //}
                        //else
                        //{
                        //    totMaterias = true;
                        //    break;
                        //}
                    }
                }

                if (totMaterias == true)
                {
                    grid_bind_ttira();
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "modalMaterias", "$('#modalMaterias').modal('hide')", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_tot_materias", "error_tot_materias('" + objValida.TotMaterias + "');", true);
                }
                else
                {
                    if (validados == true)
                    {
                        grid_bind_ttira();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "modalMaterias", "$('#modalMaterias').modal('hide')", true);
                    }
                    else
                    {
                        grid_bind_ttira();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error_sin_materias", "error_sin_materias();", true);
                    }
                }
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "ttira", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                //Response.Redirect("Inicio.aspx");
            }
        }

        //protected void bttnAgregarMatAlum_Click(object sender, EventArgs e)
        //{
        //    bool validados = false;
        //    bool totMaterias = false;
        //    ModelValidaTotMateriasResponse objValida = new ModelValidaTotMateriasResponse();



        //    try
        //    {
        //        foreach (GridViewRow row in Gridttira_Disp.Rows)
        //        {
        //            CheckBox cbCons = (CheckBox)(row.Cells[0].FindControl("chkMatDisp"));
        //            if (cbCons.Checked == true)
        //            {

        //                    serviceKardex.InsertMateriaAlumno(txt_matricula.Text, ddl_periodo.SelectedValue, ddl_campus.SelectedValue,
        //                        ddl_programa.SelectedValue, row.Cells[1].Text, "SG", ddl_nivel.SelectedValue, Session["usuario"].ToString());
        //                    validados = true;
        //                    totMaterias = false;
        //                //}
        //                //else
        //                //{
        //                //    totMaterias = true;
        //                //    break;
        //                //}
        //            }
        //        }

        //        //if (totMaterias == true)
        //        //{
        //        //    grid_bind_ttira();
        //        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "modalMaterias", "$('#modalMaterias').modal('hide')", true);
        //        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_tot_materias", "error_tot_materias('"+ objValida.Validado + "');", true);
        //        //}
        //        //else
        //        //{
        //            if (validados == true)
        //            {
        //                grid_bind_ttira();
        //                ScriptManager.RegisterStartupScript(this, this.GetType(), "modalMaterias", "$('#modalMaterias').modal('hide')", true);
        //            }
        //            else
        //            {
        //                grid_bind_ttira();
        //                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_sin_materias", "error_sin_materias();", true);
        //            }
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        string mensaje_error = ex.Message.Replace("'", "-");
        //        Global.inserta_log(mensaje_error, "ttira", Session["usuario"].ToString());
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
        //        //Response.Redirect("Inicio.aspx");
        //    }
        //}

        protected void linkBttnAsigTodas_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "modalAsigTodas", "$('#modalConfirma').modal('show')", true);

        }

        protected void Gridttira_DataBound(object sender, EventArgs e)
        {

        }

        protected void Gridttira_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string clave = Convert.ToString(Gridttira.DataKeys[e.Row.RowIndex].Value.ToString());
                    //DropDownList ddl = e.Row.FindControl("ddl_grupo_edit") as DropDownList;
                    //ddl.DataSource = serviceKardex.ObtenerGruposDisp(clave, e.Row.Cells[6].Text, ddl_periodo.SelectedValue, ddl_campus.SelectedValue);
                    //ddl.DataValueField = "Clave";
                    //ddl.DataTextField = "Descripcion";
                    //ddl.DataBind();
                    //ddl.SelectedValue = e.Row.Cells[6].Text;
                }
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "ttira", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                //Response.Redirect("Inicio.aspx");
            }
        }

        protected void Gridttira_SelectedIndexChanged(object sender, EventArgs e)
        {
            string grupo = HttpUtility.HtmlDecode(Gridttira.SelectedRow.Cells[2].Text);
            DropDownList ddl = (DropDownList)Gridttira.SelectedRow.Cells[3].FindControl("ddl_estatus");

            if (grupo == "SG" || ddl.SelectedValue == "0")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_estatus", "error_estatus();", true);
            else
            {
                Gridthora.DataSource = null;
                Gridthora.DataBind();
                Gridthora.DataSource = serviceKardex.ObtenerHorarioAlumno(txt_matricula.Text, ddl_periodo.SelectedValue, ddl_campus.SelectedValue, Gridttira.SelectedRow.Cells[0].Text);
                Gridthora.DataBind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "modalHorario", "$('#modalHorario').modal('show')", true);
            }
        }

        protected void linkBttnGuardarHora_Click(object sender, EventArgs e)
        {
            ModelInsertarHoraMateriaResponse objExiste = new ModelInsertarHoraMateriaResponse();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "modalHorario", "$('#modalHorario').modal('show')", true);

            //Label lblGrupo=(Label)(Gridttira.SelectedRow.Cells[2].FindControl("lbl_grupo"));
            //                        //CheckBox cbCons = (CheckBox)(row.Cells[0].FindControl("chkMatDisp"));

            try
            {
                objExiste = serviceKardex.InsertHoraMateria(txt_matricula.Text, ddl_hora_inicial.SelectedValue,
                    ddl_hora_final.SelectedValue, ddl_periodo.SelectedValue, Gridttira.SelectedRow.Cells[0].Text,
                    Gridttira.SelectedRow.Cells[2].Text, ddl_dia.SelectedValue, ddl_campus.SelectedValue);
                if (objExiste.Existe == "0")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "modalHorario", "$('#modalHorario').modal('hide')", true);
                    grid_bind_thora();
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_hora", "error_hora();", true);

            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "ttira", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                //Response.Redirect("Inicio.aspx");
            }
        }

        protected void linkBttnEditar_Click(object sender, EventArgs e)
        {
            string grupo;
            LinkButton cbi = (LinkButton)(sender);
            GridViewRow row = (GridViewRow)cbi.NamingContainer;
            //ddl_grupo2.SelectedIndex = 0;
            int fila = row.RowIndex;
            Gridttira.SelectedIndex = row.RowIndex;
            try
            {
                grupo = HttpUtility.HtmlDecode(Gridttira.SelectedRow.Cells[2].Text);
                ddl_grupo2.DataSource = serviceKardex.ObtenerGruposDisp(Gridttira.SelectedRow.Cells[0].Text,
                    grupo, ddl_periodo.SelectedValue, ddl_campus.SelectedValue, ddl_programa.SelectedValue, txt_matricula.Text);
                ddl_grupo2.DataValueField = "Clave";
                ddl_grupo2.DataTextField = "Descripcion";
                ddl_grupo2.DataGroupField = "Clasificacion";
                ddl_grupo2.DataBind();
                if (grupo == "SG")
                    ddl_grupo2.SelectedIndex = 0;

                ddl_estatus_editar.SelectedValue = Gridttira.SelectedRow.Cells[6].Text;
                GridHorarioGrupo.DataSource = null;
                GridHorarioGrupo.DataBind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "modalGrupos", "$('#modalGrupos').modal('show')", true);
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "ttira", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                //Response.Redirect("Inicio.aspx");
            }
        }

        protected void grid_bind_tmaterias_grupo()
        {
            try
            {
                GridHorarioGrupo.DataSource = null;
                GridHorarioGrupo.DataBind();
                GridHorarioGrupo.DataSource = serviceKardex.ObtenerMateriasGrupo(ddl_periodo.SelectedValue, ddl_campus.SelectedValue,
                    Gridttira.SelectedRow.Cells[0].Text, ddl_grupo2.SelectedValue);
                GridHorarioGrupo.DataBind();
            }


            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "ttira", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                //Response.Redirect("Inicio.aspx");
            }
        }

        protected void linBttnGuardarGrupoMat_Click(object sender, EventArgs e)
        {
            ModelInsertarHorarioAlumnoResponse objExiste = new ModelInsertarHorarioAlumnoResponse();
            ModelValidaTotMateriasResponse objValida = new ModelValidaTotMateriasResponse();
            try
            {

                //objValida = serviceKardex.ValTotMaterias(txt_matricula.Text, ddl_campus.SelectedValue, ddl_programa.SelectedValue, ddl_periodo.SelectedValue, "");
                //if (Convert.ToInt32(objValida.Validado) > 0)
                objValida = serviceKardex.ValTotMaterias(txt_matricula.Text, ddl_campus.SelectedValue, ddl_programa.SelectedValue, ddl_periodo.SelectedValue, Gridttira.SelectedRow.Cells[7].Text);
                if (Convert.ToInt32(objValida.Validado) == 0)

                {

                    objExiste = serviceKardex.InsertHorarioAlumno(txt_matricula.Text, ddl_periodo.SelectedValue, ddl_campus.SelectedValue,
                    ddl_programa.SelectedValue, Gridttira.SelectedRow.Cells[0].Text, ddl_grupo2.SelectedValue, Session["usuario"].ToString());

                    if (Convert.ToInt32(objExiste.Existe) == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "modalHorario", "$('#modalGrupos').modal('hide')", true);
                        grid_bind_ttira();
                        grid_bind_thora();
                    }
                    else if (Convert.ToInt32(objExiste.Existe) == 99999)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error_grupo", "error_grupo();", true);
                        grid_bind_ttira();
                        grid_bind_thora();
                    }
                    else if (Convert.ToInt32(objExiste.Existe) > 0)
                    {
                        grid_bind_ttira();
                        grid_bind_thora();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "modalHorario", "$('#modalGrupos').modal('hide')", true);

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error_hora", "error_hora();", true);
                    }
                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "error_transaccion();", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_tot_materias", "error_tot_materias('" + objValida.TotMaterias + "');", true);

                }
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "ttira", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                //Response.Redirect("Inicio.aspx");
            }
        }

        protected void linkBttnAsignar_Click(object sender, EventArgs e)
        {
            ModelInsertarHorarioMasivoAlumnoResponse objExiste = new ModelInsertarHorarioMasivoAlumnoResponse();
            int registrados = 0;
            try
            {
                //serviceKardex.UpdMateriasPropuestas(txt_matricula.Text, ddl_periodo.SelectedValue,
                //                   ddl_programa.SelectedValue, ddl_campus.SelectedValue, Session["usuario"].ToString());
                foreach (GridViewRow row in Gridttira.Rows)
                {
                    DropDownList ddl = (DropDownList)(row.Cells[3].FindControl("ddl_estatus"));
                    if (ddl.SelectedValue == "PR")
                    {
                        objExiste = serviceKardex.InsertHorarioMasivoAlumno(txt_matricula.Text, ddl_periodo.SelectedValue,
                                   ddl_campus.SelectedValue, ddl_programa.SelectedValue, row.Cells[0].Text, Session["usuario"].ToString());

                        registrados = Convert.ToInt32(objExiste.Existe) + 1;
                    }
                }


                //if (objExiste.Existe=="0")
                //{
                //    grid_bind_ttira();
                //    grid_bind_thora();
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "modalMaterias", "$('#modalConfirma').modal('hide')", true);
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_hora", "error_hora();", true);

                //}
                if (Convert.ToInt32(objExiste.Existe) == 0)
                {
                    grid_bind_ttira();
                    grid_bind_thora();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "modalMaterias", "$('#modalConfirma').modal('hide')", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);

                }
                else if (Convert.ToInt32(objExiste.Existe) > 0)
                {
                    grid_bind_ttira();
                    grid_bind_thora();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "modalMaterias", "$('#modalConfirma').modal('hide')", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_hora", "error_hora();", true);

                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "error_transaccion();", true);



            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "ttira", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                //Response.Redirect("Inicio.aspx");
            }
        }

        protected void ddl_grupo2_SelectedIndexChanged(object sender, EventArgs e)
        {
            grid_bind_tmaterias_grupo();
        }

        protected void linkBttnEliminar_Click(object sender, EventArgs e)
        {
            LinkButton cbi = (LinkButton)(sender);
            GridViewRow row = (GridViewRow)cbi.NamingContainer;
            int fila = row.RowIndex;
            Gridttira.SelectedIndex = row.RowIndex;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "modalEliminar", "$('#modalEliminar').modal('show')", true);


        }

        protected void linkBttnDeletemat_Click(object sender, EventArgs e)
        {
            try
            {
                serviceKardex.UpdEstatusMateria(txt_matricula.Text, ddl_periodo.SelectedValue,
                    ddl_programa.SelectedValue, ddl_campus.SelectedValue, Gridttira.SelectedRow.Cells[0].Text, "BA", Gridttira.SelectedRow.Cells[2].Text, ddl_nivel.SelectedValue, Session["usuario"].ToString());
                //Gridttira.EditIndex = -1;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                grid_bind_ttira();
                grid_bind_thora();
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "ttira", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                //Response.Redirect("Inicio.aspx");
            }
        }
    }
}