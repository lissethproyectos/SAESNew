using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAES_v1;
using SAES_DBO.Models;
using SAES_Services;
using static SAES_DBO.Models.ModelPlan;
using static SAES_DBO.Models.ModelAlumno;
using static SAES_DBO.Models.ModelCargaAcademica;

namespace SAES_v1
{
    public partial class tpred : System.Web.UI.Page
    {
        #region <Variables>
        //Utilidades utils = new Utilidades();
        AlumnoService serviceAlumno = new AlumnoService();
        List<ModelObtenPaisesResponse> lstPaises = new List<ModelObtenPaisesResponse>();
        Catalogos serviceCatalogo = new Catalogos();
        PlanAcademicoService servicePlan = new PlanAcademicoService();
        CargaAcademicaService serviceCargaAcad = new CargaAcademicaService();
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ctrl_fecha_i", "ctrl_fecha_i();", true);

                if (!IsPostBack)
                {
                    TxtCuenta.Text = Global.cuenta.ToString();
                    TxtNombre.Text = Global.nombre_alumno.ToString();
                    combo_estatus();
                    if (TxtCuenta.Text != "")
                    {
                        Carga_Estudiante();
                    }


                    ddl_periodo.DataSource = serviceCatalogo.ObtenerPeriodosEscolares();
                    ddl_periodo.DataValueField = "clave";
                    ddl_periodo.DataTextField = "nombre";
                    ddl_periodo.DataBind();

                    ddlEscuelaProcedencia.DataSource = serviceCatalogo.ObtenerEscuelasProcedencia();
                    ddlEscuelaProcedencia.DataValueField = "clave";
                    ddlEscuelaProcedencia.DataTextField = "descripcion";
                    ddlEscuelaProcedencia.DataBind();
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_Alumnos", "load_datatable_Alumnos();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_Plan", "load_datatable_Plan();", true);

            }
        }

        protected void combo_estatus()
        {
            ddl_estatus.Items.Clear();
            ddl_estatus.Items.Add(new ListItem("------", "XX"));
            ddl_estatus.Items.Add(new ListItem("Predictamen", "PR"));
            ddl_estatus.Items.Add(new ListItem("Dictamen Oficial", "DO"));
        }








        


        private DataTable GetData(MySqlCommand cmd)
        {
            DataTable dt = new DataTable();
            String strConnString = System.Configuration.ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strConnString);
            MySqlDataAdapter sda = new MySqlDataAdapter();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            try
            {
                con.Open();
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                return dt;
            }
            catch
            {
                return null;
            }
            finally
            {
                con.Close();
                sda.Dispose();
                con.Dispose();
            }
        }






        protected void GridAlumnos_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridAlumnos.SelectedRow;
            TxtCuenta.Text = row.Cells[1].Text;
            TxtNombre.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);
            //GridAlumnos.Visible = false;
            Global.cuenta = TxtCuenta.Text;
            Global.nombre_alumno = TxtNombre.Text;
            //lbl_Campus.Text = ""; lbl_Estatus.Text = "";
            //lbl_Nivel.Text = ""; lbl_Programa.Text = ""; lbl_Turno.Text = "";
            //txt_esc_proc.Text = "";
            //txt_nom_proc.Text = "";
            ddlEscuelaProcedencia.SelectedIndex = 0;
            txt_origen.Text = "";
            txt_folio.Text = "";
            txt_fecha_i.Text = "";
            //txt_periodo.Text = "";
            //txt_nom_per.Text = "";

            ddl_estatus.Items.Clear();
            ddl_estatus.Items.Add("------");
            ddl_estatus.Items.Add(new ListItem("Predictamen", "PR"));
            ddl_estatus.Items.Add(new ListItem("Dictamen Oficial", "DO"));
            //GridTpreq.Visible = false;
            containerGridAlumnos.Visible = false;

            //Carga_Prog();
        }




        protected void Gridtpers_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                TxtCuenta.Text = GridAlumnos.SelectedRow.Cells[2].Text;
                TxtNombre.Text = HttpUtility.HtmlDecode(GridAlumnos.SelectedRow.Cells[3].Text + " " + GridAlumnos.SelectedRow.Cells[4].Text + " " + GridAlumnos.SelectedRow.Cells[5].Text);

                ddl_programa.DataSource = serviceAlumno.ObtenerProgramaAlumno2(TxtCuenta.Text);
                ddl_programa.DataValueField = "Clave";
                ddl_programa.DataTextField = "Descripcion";
                ddl_programa.DataBind();

                ddl_programa_SelectedIndexChanged(null, null);
                containerGridAlumnos.Visible = false;

                //ddl_periodo.DataSource = serviceAlumno.ObtenerPeriodoAlumno(GridAlumnos.SelectedRow.Cells[1].Text);
                //ddl_periodo.DataValueField = "Clave";
                //ddl_periodo.DataTextField = "Descripcion";
                //ddl_periodo.DataBind();

                //linkBttnCancelar.Visible = true;
                //linkBttnGuardar.Visible = true;
                //linkBttnCancelarModificar.Visible = false;
                //linkBttnModificar.Visible = false;
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tpred", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void GridPeriodos_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridPeriodos.SelectedRow;
            //txt_periodo.Text = row.Cells[1].Text;
            //txt_nom_per.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);
            GridPeriodos.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopupPeriodos", "$('#modalPeriodos').modal('show')", true);


        }

        private void Carga_Tpreq()
        {
            try
            {
               

                //if (dssql1.Tables[0].Rows[0][0].ToString() == "1")
                //{
                //    //CmdGuardar.Visible = true;
                //}


                GridTpreq.DataSource = servicePlan.ObtenerPredictamenAlumno(TxtCuenta.Text, ddl_programa.SelectedValue);
                GridTpreq.DataBind();


                TxtCuenta.Focus();
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tpred", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }

        }

        protected void GridTpreq_SelectedIndexChanged(object sender, EventArgs e)
        {
            ModelObtenerEscuelaProcedenciaResponse objDatos = new ModelObtenerEscuelaProcedenciaResponse();
            GridViewRow row = GridTpreq.SelectedRow;

            try
            {
                objDatos = serviceCargaAcad.ObtenerEscuelaProcedencia(Global.programa.ToString(), row.Cells[1].Text, TxtCuenta.Text);
                if (objDatos != null)
                {
                    ddlEscuelaProcedencia.Enabled = false;
                    ddlEscuelaProcedencia.SelectedValue = row.Cells[1].Text;
                    txt_origen.ReadOnly = true;
                    txt_origen.Text = objDatos.carrera; //dssql1.Tables[0].Rows[0][1].ToString();
                    txt_folio.Text = objDatos.folio; // dssql1.Tables[0].Rows[0][2].ToString();
                    txt_fecha_i.Text = objDatos.fecha_dict; // dssql1.Tables[0].Rows[0][3].ToString();
                    ddl_periodo.SelectedValue = objDatos.tpreq_tpees_clave; // dssql1.Tables[0].Rows[0][4].ToString();
                    ddl_estatus.Items.Clear();
                    ddl_estatus.Items.Add("------");
                    ddl_estatus.Items.Add(new ListItem("Predictamen", "PR"));
                    ddl_estatus.Items.Add(new ListItem("Dictamen Oficial", "DO"));
                    ddl_estatus.Items.FindByValue(objDatos.tpreq_estatus).Selected = true; // dssql1.Tables[0].Rows[0][6].ToString()).Selected = true;
                    Global.escuela = row.Cells[1].Text;
                    btn_save.Visible = false;
                    btn_update.Visible = true;
                    Carga_Tpred();
                }
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tpred", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }

            
        }

        

        private void Carga_Tpred()
        {
            try
            {
                Plan.DataSource = servicePlan.ObtenerPlanAlumno(Global.programa.ToString(), TxtCuenta.Text, ddlEscuelaProcedencia.SelectedValue);
                Plan.DataBind();
                Plan.UseAccessibleHeader = true;
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tpred", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {


            if (!String.IsNullOrEmpty(TxtCuenta.Text) && !String.IsNullOrEmpty(ddlEscuelaProcedencia.SelectedValue) && !String.IsNullOrEmpty(txt_fecha_i.Text) && !String.IsNullOrEmpty(txt_origen.Text) && !String.IsNullOrEmpty(txt_folio.Text) && !String.IsNullOrEmpty(ddl_periodo.SelectedValue))
            {

                string fecha_i_string = txt_fecha_i.Text;
                string format = "dd/MM/yyyy";

                DateTime fecha_inicio = DateTime.ParseExact(fecha_i_string, format, CultureInfo.InvariantCulture);
              //string strCadSQL =
              //      "insert into tpreq values((select tpers_num from tpers where tpers_id='" + TxtCuenta.Text + "'),'" +
              //      Global.programa.ToString() + "','" + ddlEscuelaProcedencia.SelectedValue + "','" + txt_origen.Text + "','" + ddl_periodo.SelectedValue + "','" +
              //      txt_folio.Text + "', STR_TO_DATE('" + string.Format(txt_fecha_i.Text, "dd/MM/yyyy") + "','%d/%m/%Y'), '" + ddl_estatus.SelectedValue + "',current_timestamp(), '" +
              //     + "')";
               
                try
                {
                    serviceCargaAcad.InsertarTPreq(TxtCuenta.Text, Global.programa.ToString(), ddlEscuelaProcedencia.SelectedValue, txt_origen.Text,
                         ddl_periodo.SelectedValue, txt_folio.Text, string.Format(txt_fecha_i.Text, "dd/MM/yyyy"), ddl_estatus.SelectedValue, Session["usuario"].ToString()); 
                    ddlEscuelaProcedencia.SelectedIndex = 0;
                    txt_origen.Text = "";
                    txt_folio.Text = "";
                    txt_fecha_i.Text = "";
                    ddl_estatus.Items.Clear();
                    ddl_estatus.Items.Add("------");
                    ddl_estatus.Items.Add(new ListItem("Predictamen", "PR"));
                    ddl_estatus.Items.Add(new ListItem("Dictamen Oficial", "DO"));
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                    Carga_Tpreq();
                }
                catch (Exception ex)
                {
                    string test = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tpred", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                }

            }
            else
            {
                //grid_periodo_bind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_tpreq();", true);
            }


        }

        protected void Agregar_Materias(object sender, EventArgs e)
        {

            try
            {
                double err = 0;
                for (int i = 0; i < Plan.Rows.Count; i++)
                {
                    TextBox materia = (TextBox)Plan.Rows[i].FindControl("mat_origen");
                    DropDownList calif = (DropDownList)Plan.Rows[i].FindControl("CboCalif");
                    if (materia.Text != "" && calif.SelectedValue.ToString() == "---")
                    {
                        /*ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "calif_materia();", true);
                        Carga_Tpred();*/
                        resultado.Visible = true;
                        resultado.Text = "Falta Calificación / Estatus en Materia";
                        Carga_Tpred();
                        err = 1;
                    }
                }
                if (err == 0)
                {
                    MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                    conexion.Open();

                    // resultado.Text = strBorra;

                    for (int i = 0; i < Plan.Rows.Count; i++)
                    {
                        TextBox materia = (TextBox)Plan.Rows[i].FindControl("mat_origen");
                        DropDownList calif = (DropDownList)Plan.Rows[i].FindControl("CboCalif");
                        DropDownList St = (DropDownList)Plan.Rows[i].FindControl("CboSt");

                        if (materia.Text != "" && calif.SelectedValue.ToString() != "---" && St.SelectedValue.ToString() != "----")
                        {

                            string strBorra = "DELETE from tpred where tpred_tpers_num=(select tpers_num from tpers where tpers_id='" + TxtCuenta.Text + "') " +
                            " and tpred_tprog_clave='" + Global.programa.ToString() + "' and tpred_tmate_clave='" + Plan.Rows[i].Cells[2].Text.ToString() + "'" +
                            " and tpred_tespr_clave='" + ddlEscuelaProcedencia.SelectedValue + "'";
                            MySqlCommand myCommandborra = new MySqlCommand(strBorra, conexion);
                            //Ejecucion del comando en el servidor de BD
                            myCommandborra.ExecuteNonQuery();

                            string consecutivo = " select count(*) from tpred " +
                                " where tpred_tespr_clave='" + ddlEscuelaProcedencia.SelectedValue + "' and tpred_tprog_clave='" + Global.programa.ToString() + "'" +
                                " and tpred_tmate_clave='" + Plan.Rows[i].Cells[2].Text.ToString() + "'";
                            MySqlDataAdapter sqladapter = new MySqlDataAdapter();

                            DataSet dssql1 = new DataSet();

                            MySqlCommand commandsql1 = new MySqlCommand(consecutivo, conexion);
                            sqladapter.SelectCommand = commandsql1;
                            sqladapter.Fill(dssql1);
                            sqladapter.Dispose();
                            commandsql1.Dispose();
                            double conse = Convert.ToDouble(dssql1.Tables[0].Rows[0][0].ToString()) + 1;
                            string estatus = "";
                            if (St.SelectedValue.ToString() == "Activo")
                            {
                                estatus = "A";
                            }
                            if (St.SelectedValue.ToString() == "Baja")
                            {
                                estatus = "B";
                            }


                            string strCadSQL = "insert into tpred values((select tpers_num from tpers where tpers_id='" + TxtCuenta.Text + "'),'" +
                            ddlEscuelaProcedencia.SelectedValue + "','" + Global.programa.ToString() + "','" + Plan.Rows[i].Cells[2].Text.ToString() + "','" + materia.Text + "','" + calif.SelectedValue.ToString() + "'," + conse +
                            " , current_timestamp(),'" + Session["usuario"].ToString() + "','" + estatus + "')";
                            // resultado.Text = resultado.Text + "-->" + strCadSQL;

                            MySqlCommand myCommandinserta = new MySqlCommand(strCadSQL, conexion);
                            //Ejecucion del comando en el servidor de BD
                            myCommandinserta.ExecuteNonQuery();
                            //resultado.Text = resultado.Text + "<-->" + strBorra + ":" + conse + "--" + strCadSQL;
                        }
                        if (materia.Text != "" && (calif.SelectedValue.ToString() == "---" || St.SelectedValue.ToString() == "----"))
                        {
                            /*ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "datos_materia();", true);
                            Carga_Tpred();*/
                            resultado.Visible = true;
                            resultado.Text = "Falta Calificación / Estatus en Materia";
                            Carga_Tpred();
                            err = 1;

                        }
                        if ((materia.Text == "" || calif.SelectedValue.ToString() == "---") && St.SelectedValue.ToString() != "----")
                        {
                            /*ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "datos_materia();", true);
                            Carga_Tpred();*/
                            resultado.Visible = true;
                            resultado.Text = "Falta Materia / Calificación en Materia";
                            Carga_Tpred();
                            err = 1;
                        }
                        if (calif.SelectedValue.ToString() != "---" && (materia.Text == "" || St.SelectedValue.ToString() == "----"))
                        {
                            /*ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "datos_materia();", true);
                            Carga_Tpred();*/
                            resultado.Visible = true;
                            resultado.Text = "Falta Materia / Estatus en Materia";
                            Carga_Tpred();
                            err = 1;
                        }


                    }
                    if (err == 0)
                    {
                        //txt_esc_proc.Text = "";
                        //txt_nom_proc.Text = "";
                        ddlEscuelaProcedencia.SelectedIndex = 0;
                        txt_origen.Text = "";
                        txt_folio.Text = "";
                        txt_fecha_i.Text = "";
                        //txt_periodo.Text = "";
                        //txt_nom_per.Text = "";

                        ddl_estatus.Items.Clear();
                        ddl_estatus.Items.Add("------");
                        ddl_estatus.Items.Add(new ListItem("Predictamen", "PR"));
                        ddl_estatus.Items.Add(new ListItem("Dictamen Oficial", "DO"));
                        /*CboEstatus.Items.Clear();
                        CboEstatus.Items.Add("------");
                        CboEstatus.Items.Add("Predictamen");
                        CboEstatus.Items.Add("Dictamen Oficial");*/
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                        //Plan.Visible = false;
                        //btn_tpred.Visible = false;
                        //btn_pdf.Visible = false;
                        btn_save.Visible = true;
                        btn_cancel.Visible = false;
                        resultado.Visible = false;
                        Carga_Tpreq();
                    }

                }
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tpred", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
            }

        }

        protected void PDF_Click(object sender, EventArgs e)
        {

            Response.Redirect("TpredPDF.aspx");
        }

        //protected void linkBttnBuscarEscuelaProc_Click(object sender, EventArgs e)
        //{
        //    string strQueryEscuelas = "";
        //    strQueryEscuelas = " SELECT tespr_clave Clave, tespr_desc Escuela  from tespr " +
        //        " order  by Clave ";
        //    try
        //    {
        //        MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
        //        conexion.Open();
        //        MySqlDataAdapter dataadapter = new MySqlDataAdapter(strQueryEscuelas, conexion);
        //        DataSet ds = new DataSet();
        //        dataadapter.Fill(ds, "Escuelas");
        //        GridEscuelas.DataSource = ds;
        //        GridEscuelas.DataBind();
        //        GridEscuelas.DataMember = "Escuelas";
        //        GridEscuelas.HeaderRow.TableSection = TableRowSection.TableHeader;
        //        GridEscuelas.UseAccessibleHeader = true;
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_Escuelas", "load_datatable_Escuelas();", true);
        //        conexion.Close();

        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopupEscuelas", "$('#modalEscuelas').modal('show')", true);


        //    }
        //    catch (Exception ex)
        //    {
        //        //resultado.Text = ex.Message;
        //        string mensaje_error = ex.Message.Replace("'", "-");
        //        Global.inserta_log(mensaje_error, "tpred", Session["usuario"].ToString());
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
        //    }
        //}

        protected void linkBttnBuscarPeriodo_Click(object sender, EventArgs e)
        {
            //string strQueryPeriodos = "";
            //strQueryPeriodos = " SELECT tpees_clave Clave, tpees_desc Periodo  from tpees " +
            //    " order  by Clave desc";
            //try
            //{
            //    MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            //    conexion.Open();
            //    MySqlDataAdapter dataadapter = new MySqlDataAdapter(strQueryPeriodos, conexion);
            //    DataSet ds = new DataSet();
            //    dataadapter.Fill(ds, "Periodos");
            //    GridPeriodos.DataSource = ds;
            //    GridPeriodos.DataBind();
            //    GridPeriodos.DataMember = "Periodos";
            //    GridPeriodos.HeaderRow.TableSection = TableRowSection.TableHeader;
            //    GridPeriodos.UseAccessibleHeader = true;
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_Periodos", "load_datatable_Periodos();", true);
            //    conexion.Close();
            //}
            //catch (Exception ex)
            //{
            //    //resultado.Text = ex.Message;
            //    string mensaje_error = ex.Message.Replace("'", "-");
            //    Global.inserta_log(mensaje_error, "tpred", Session["usuario"].ToString());
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            //}
            string strQueryPeriodos = "";
            strQueryPeriodos = " SELECT tpees_clave Clave, tpees_desc Periodo  from tpees " +
                " order  by Clave desc";
            try
            {
                MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                conexion.Open();
                MySqlDataAdapter dataadapter = new MySqlDataAdapter(strQueryPeriodos, conexion);
                DataSet ds = new DataSet();
                dataadapter.Fill(ds, "Periodos");
                GridPeriodos.DataSource = ds;
                GridPeriodos.DataBind();
                GridPeriodos.DataMember = "Periodos";
                GridPeriodos.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridPeriodos.UseAccessibleHeader = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopupPeriodos", "$('#modalPeriodos').modal('show')", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_Periodos", "load_datatable_Periodos();", true);
                conexion.Close();
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tpred", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }

        }

        protected void linkBttnGuardarPedictamen_Click(object sender, EventArgs e)
        {

            try
            {
                double err = 0;
                for (int i = 0; i < Plan.Rows.Count; i++)
                {
                    TextBox materia = (TextBox)Plan.Rows[i].FindControl("mat_origen");
                    DropDownList calif = (DropDownList)Plan.Rows[i].FindControl("CboCalif");
                    if (materia.Text != "" && calif.SelectedValue.ToString() == "---")
                    {
                        /*ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "calif_materia();", true);
                        Carga_Tpred();*/
                        resultado.Visible = true;
                        resultado.Text = "Falta Calificación / Estatus en Materia";
                        Carga_Tpred();
                        err = 1;
                    }
                }
                if (err == 0)
                {
                    MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                    conexion.Open();

                    // resultado.Text = strBorra;

                    for (int i = 0; i < Plan.Rows.Count; i++)
                    {
                        TextBox materia = (TextBox)Plan.Rows[i].FindControl("mat_origen");
                        DropDownList calif = (DropDownList)Plan.Rows[i].FindControl("CboCalif");
                        DropDownList St = (DropDownList)Plan.Rows[i].FindControl("CboSt");

                        if (materia.Text != "" && calif.SelectedValue.ToString() != "---" && St.SelectedValue.ToString() != "----")
                        {

                            string strBorra = "DELETE from tpred where tpred_tpers_num=(select tpers_num from tpers where tpers_id='" + TxtCuenta.Text + "') " +
                            " and tpred_tprog_clave='" + Global.programa.ToString() + "' and tpred_tmate_clave='" + Plan.Rows[i].Cells[2].Text.ToString() + "'" +
                            " and tpred_tespr_clave='" + ddlEscuelaProcedencia.SelectedValue + "'";
                            MySqlCommand myCommandborra = new MySqlCommand(strBorra, conexion);
                            //Ejecucion del comando en el servidor de BD
                            myCommandborra.ExecuteNonQuery();

                            string consecutivo = " select count(*) from tpred " +
                                " where tpred_tespr_clave='" + ddlEscuelaProcedencia.SelectedValue + "' and tpred_tprog_clave='" + Global.programa.ToString() + "'" +
                                " and tpred_tmate_clave='" + Plan.Rows[i].Cells[2].Text.ToString() + "'";
                            MySqlDataAdapter sqladapter = new MySqlDataAdapter();

                            DataSet dssql1 = new DataSet();

                            MySqlCommand commandsql1 = new MySqlCommand(consecutivo, conexion);
                            sqladapter.SelectCommand = commandsql1;
                            sqladapter.Fill(dssql1);
                            sqladapter.Dispose();
                            commandsql1.Dispose();
                            double conse = Convert.ToDouble(dssql1.Tables[0].Rows[0][0].ToString()) + 1;
                            string estatus = "";
                            if (St.SelectedValue.ToString() == "Activo")
                            {
                                estatus = "A";
                            }
                            if (St.SelectedValue.ToString() == "Baja")
                            {
                                estatus = "B";
                            }


                            string strCadSQL = "insert into tpred values((select tpers_num from tpers where tpers_id='" + TxtCuenta.Text + "'),'" +
                            ddlEscuelaProcedencia.SelectedValue + "','" + Global.programa.ToString() + "','" + Plan.Rows[i].Cells[2].Text.ToString() + "','" + materia.Text + "','" + calif.SelectedValue.ToString() + "'," + conse +
                            " , current_timestamp(),'" + Session["usuario"].ToString() + "','" + estatus + "')";
                            // resultado.Text = resultado.Text + "-->" + strCadSQL;

                            MySqlCommand myCommandinserta = new MySqlCommand(strCadSQL, conexion);
                            //Ejecucion del comando en el servidor de BD
                            myCommandinserta.ExecuteNonQuery();
                            //resultado.Text = resultado.Text + "<-->" + strBorra + ":" + conse + "--" + strCadSQL;
                        }
                        if (materia.Text != "" && (calif.SelectedValue.ToString() == "---" || St.SelectedValue.ToString() == "----"))
                        {
                            /*ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "datos_materia();", true);
                            Carga_Tpred();*/
                            resultado.Visible = true;
                            resultado.Text = "Falta Calificación / Estatus en Materia";
                            Carga_Tpred();
                            err = 1;

                        }
                        if ((materia.Text == "" || calif.SelectedValue.ToString() == "---") && St.SelectedValue.ToString() != "----")
                        {
                            /*ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "datos_materia();", true);
                            Carga_Tpred();*/
                            resultado.Visible = true;
                            resultado.Text = "Falta Materia / Calificación en Materia";
                            Carga_Tpred();
                            err = 1;
                        }
                        if (calif.SelectedValue.ToString() != "---" && (materia.Text == "" || St.SelectedValue.ToString() == "----"))
                        {
                            /*ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "datos_materia();", true);
                            Carga_Tpred();*/
                            resultado.Visible = true;
                            resultado.Text = "Falta Materia / Estatus en Materia";
                            Carga_Tpred();
                            err = 1;
                        }


                    }
                    if (err == 0)
                    {
                        //txt_esc_proc.Text = "";
                        //txt_nom_proc.Text = "";
                        ddlEscuelaProcedencia.SelectedIndex = 0;
                        txt_origen.Text = "";
                        txt_folio.Text = "";
                        txt_fecha_i.Text = "";
                        //txt_periodo.Text = "";
                        ddl_periodo.Items.Clear();
                        //txt_nom_per.Text = "";

                        ddl_estatus.Items.Clear();
                        ddl_estatus.Items.Add("------");
                        ddl_estatus.Items.Add(new ListItem("Predictamen", "PR"));
                        ddl_estatus.Items.Add(new ListItem("Dictamen Oficial", "DO"));
                        /*CboEstatus.Items.Clear();
                        CboEstatus.Items.Add("------");
                        CboEstatus.Items.Add("Predictamen");
                        CboEstatus.Items.Add("Dictamen Oficial");*/
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                        //Plan.Visible = false;
                        //btn_tpred.Visible = false;
                        //btn_pdf.Visible = false;
                        btn_save.Visible = true;
                        btn_cancel.Visible = false;
                        resultado.Visible = false;
                        Carga_Tpreq();
                    }

                }
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tpred", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
            }
        }

        protected void linkBttnImprimirPedictamen_Click(object sender, EventArgs e)
        {
            Response.Redirect("TpredPDF.aspx");
        }

        protected void Plan_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Plan.EditIndex = e.NewEditIndex;
            Carga_Tpred();
            //List<Bien> lstBien = new List<Bien>();
            //lstBien = (List<Bien>)Session["Inventario"];
            //CargarGridAgregados(lstBien);
            ((DropDownList)Plan.Rows[e.NewEditIndex].FindControl("CboCalif")).Enabled = true;
            ((DropDownList)Plan.Rows[e.NewEditIndex].FindControl("CboSt")).Enabled = true;
            ((TextBox)Plan.Rows[e.NewEditIndex].FindControl("mat_origen")).Enabled = true;
        }

        protected void Plan_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList calif = (DropDownList)e.Row.FindControl("CboCalif");
                    calif.DataSource = servicePlan.ObtenerCatCalificaciones(Global.programa.ToString());
                    calif.DataValueField = "clave";
                    calif.DataBind();
                    calif.SelectedValue = e.Row.Cells[7].Text;

                    DropDownList St = (DropDownList)e.Row.FindControl("CboSt");
                    St.SelectedValue = e.Row.Cells[6].Text;
                }
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tpred", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
            }
        }

        protected void Plan_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int fila = e.RowIndex;
            //int pagina = grdEventos.PageSize * grdEventos.PageIndex;
            //fila = pagina + fila;
            GridViewRow row = Plan.Rows[e.RowIndex];
            ModelInsTpredRequest objPlan = new ModelInsTpredRequest();


            //"insert into tpred values((select tpers_num from tpers where tpers_id='" + TxtCuenta.Text + "'),'" +
            //                txt_esc_proc.Text + "','" + Global.programa.ToString() + "','" + Plan.Rows[i].Cells[2].Text.ToString() + "',
            //                '" + materia.Text + "','" + calif.SelectedValue.ToString() + "'," + conse +
            //                " , current_timestamp(),'" + Session["usuario"].ToString() + "','" + estatus + "')";
            //// resultado.Text = resultado.Text + "-->" + strCadSQL;

            try
            {
                DropDownList calif = (DropDownList)Plan.Rows[fila].Cells[4].FindControl("CboCalif");
                DropDownList estatus = (DropDownList)Plan.Rows[fila].Cells[4].FindControl("CboSt");

                TextBox materia = (TextBox)Plan.Rows[fila].Cells[4].FindControl("mat_origen");


                objPlan.tpred_tpers_id = TxtCuenta.Text;
                objPlan.tpred_tespr_clave = ddlEscuelaProcedencia.SelectedValue; // txt_esc_proc.Text;
                objPlan.tpred_tprog_clave = Global.programa.ToString();
                objPlan.tpred_tmate_clave = row.Cells[2].Text.ToString();
                objPlan.tpred_mate_origen = materia.Text;
                objPlan.tpred_tcali_clave = calif.SelectedValue;
                objPlan.tpred_consecutivo = row.Cells[0].Text;
                //objPlan.tpred_date = p_tpred_date,
                objPlan.tpred_user = Session["usuario"].ToString();
                objPlan.tpred_estatus = estatus.SelectedValue;
                servicePlan.InsertarCalificacionRevalida(TxtCuenta.Text, ddlEscuelaProcedencia.SelectedValue, Global.programa.ToString(),
                    row.Cells[2].Text.ToString(), materia.Text, calif.SelectedValue, row.Cells[0].Text,
                    Session["usuario"].ToString(), estatus.SelectedValue
                    );

                Plan.EditIndex = -1;
                Carga_Tpred();


            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tpred", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
            }
        }

        protected void Plan_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            Plan.EditIndex = -1;
            Carga_Tpred();
        }

        protected void ddl_programa_SelectedIndexChanged(object sender, EventArgs e)
        {
            Global.cuenta = TxtCuenta.Text;
            Global.nombre_alumno = TxtNombre.Text;
            Global.programa = ddl_programa.SelectedValue;
            //lbl_Campus.Text = ""; lbl_Estatus.Text = "";
            //lbl_Nivel.Text = ""; lbl_Programa.Text = ""; lbl_Turno.Text = "";
            //txt_esc_proc.Text = "";
            //txt_nom_proc.Text = "";
            txt_origen.Text = "";
            txt_folio.Text = "";
            txt_fecha_i.Text = "";
            //txt_periodo.Text = "";
            //txt_nom_per.Text = "";

            ddl_estatus.Items.Clear();
            ddl_estatus.Items.Add("------");
            ddl_estatus.Items.Add(new ListItem("Predictamen", "PR"));


            Carga_Tpreq();
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            btn_save.Visible = true;
            btn_cancel.Visible = false;
            GridTpreq.SelectedIndex = -1;
            txt_origen.Text = "";
            txt_folio.Text = "";
            txt_fecha_i.Text = "";
            ddl_estatus.SelectedIndex = 0;

        }

        protected void TxtCuenta_TextChanged(object sender, EventArgs e)
        {
            Carga_Estudiante();
        }

        protected void linkBttnBusca_Click(object sender, EventArgs e)
        {
            Carga_Estudiante();
        }


        protected void Carga_Estudiante()
        {
            ModelObtenerAlumnosResponse datosAlumno = new ModelObtenerAlumnosResponse();
            TxtNombre.Text = string.Empty;
            DataTable dtAlumno = new DataTable();
            containerGridAlumnos.Visible = false;

            ddl_programa.Items.Clear();
            ddl_programa.Items.Add(new ListItem("-------", null));

            try
            {
                dtAlumno = serviceAlumno.ObtenerAlumnos(TxtCuenta.Text);
                if (dtAlumno.Rows.Count == 1)
                {
                    TxtNombre.Text = dtAlumno.Rows[0][1].ToString() + " " + dtAlumno.Rows[0][2].ToString() + " " + dtAlumno.Rows[0][3].ToString();
                    Global.cuenta = TxtCuenta.Text;
                    Global.nombre = dtAlumno.Rows[0][0].ToString();
                    Global.ap_paterno = dtAlumno.Rows[0][1].ToString();
                    Global.ap_materno = dtAlumno.Rows[0][2].ToString();
                    ddl_programa.DataSource = serviceAlumno.ObtenerProgramaAlumnoMatricula(TxtCuenta.Text);
                    ddl_programa.DataValueField = "Clave";
                    ddl_programa.DataTextField = "Descripcion";
                    ddl_programa.DataBind();
                }
                else if (dtAlumno.Rows.Count > 1)
                {
                    containerGridAlumnos.Visible = true;
                    GridAlumnos.DataSource = dtAlumno;
                    GridAlumnos.DataBind();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "NoexisteAlumno", "NoexisteAlumno();", true);


                }
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tpred", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }

        }

        protected void btn_update_Click(object sender, EventArgs e)
        {
            try
            {
                serviceCargaAcad.EditarTPreq(TxtCuenta.Text, Global.programa.ToString(), ddlEscuelaProcedencia.SelectedValue, txt_origen.Text,
                     ddl_periodo.SelectedValue, txt_folio.Text, string.Format(txt_fecha_i.Text, "dd/MM/yyyy"), ddl_estatus.SelectedValue, Session["usuario"].ToString());
                ddlEscuelaProcedencia.SelectedIndex = 0;
                ddlEscuelaProcedencia.Enabled = true;
                txt_origen.ReadOnly = false;
                txt_origen.Text = "";
                txt_folio.Text = "";
                txt_fecha_i.Text = "";
                ddl_estatus.Items.Clear();
                ddl_estatus.Items.Add("------");
                ddl_estatus.Items.Add(new ListItem("Predictamen", "PR"));
                btn_update.Visible = false;
                btn_save.Visible = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                Carga_Tpreq();
            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tpred", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
            }
        }
    }
}