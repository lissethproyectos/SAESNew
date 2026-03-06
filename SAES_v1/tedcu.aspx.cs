using iTextSharp.text;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using Renci.SshNet;
using SAES_DBO.Models;
using SAES_Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using static SAES_DBO.Models.ModelAlumno;
using static SAES_DBO.Models.ModelCobranza;
using static SAES_DBO.Models.ModelMenu;
using static System.Net.Mime.MediaTypeNames;

namespace SAES_v1
{
    public partial class tedcu : System.Web.UI.Page
    {
        #region <Variables>
        AlumnoService serviceAlumno = new AlumnoService();
        CobranzaService serviceCobro = new CobranzaService();
        Catalogos serviceCatalogo = new Catalogos();
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

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "ctrl_f_nac", "ctrl_f_nac();", true);
                //txt_matricula.Attributes.Add("readonly", "");

                if (!IsPostBack)
                {
                    Session["lstConceptos"] = null;
                    LlenaPagina2();
                    ddl_forma_pago.DataSource = serviceCatalogo.ObtenerComboComun("Conceptos_Cobranza_Ef", "");
                    ddl_forma_pago.DataValueField = "Clave";
                    ddl_forma_pago.DataTextField = "Descripcion";
                    ddl_forma_pago.DataBind();



                    txt_matricula.Text = Global.cuenta;
                    txt_matricula.Focus();
                    if (Global.cuenta != string.Empty)
                        linkBttnBusca_Click(null, null);

                    //if (txt_matricula.Text != "")                    
                    //    Programa();
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "GridALumnos", "load_datatable_alumnos();", true);


            }
        }

        private void LlenaPagina2()
        {
            try
            {
                string QerySelect = "select tusme_update, tusme_select from tuser, tusme, tmede " +
                              " where tuser_clave = '" + Session["usuario"].ToString() + "'" +
                              " and tusme_trole_clave = tuser_trole_clave and tusme_tmenu_clave = 6 and tusme_tmenu_clave = tmede_tmenu_clave " +
                              " and tusme_tmede_clave = tmede_clave and tmede_forma='tedcu' ";

                MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                conexion.Open();

                MySqlDataAdapter sqladapter = new MySqlDataAdapter();

                DataSet dssql1 = new DataSet();

                MySqlCommand commandsql1 = new MySqlCommand(QerySelect, conexion);
                sqladapter.SelectCommand = commandsql1;
                sqladapter.Fill(dssql1);
                sqladapter.Dispose();
                commandsql1.Dispose();
                if (dssql1.Tables[0].Rows.Count == 0 || dssql1.Tables[0].Rows[0][1].ToString() == "0")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                }
                //if (dssql1.Tables[0].Rows[0][0].ToString() == "0")
                //{
                //    btn_tedcu.Visible = false;
                //}
                conexion.Close();

            }
            catch (Exception ex)
            {
                //logs
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tedcu", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        private void LlenaPagina()
        {
            ModelObtenerPermisoFormResponse objPermiso = new ModelObtenerPermisoFormResponse();

            try
            {
                objPermiso = servicePermiso.ObtenerPermisoFormulario(Session["usuario"].ToString(), "tedcu");
                if (objPermiso != null)
                {
                    if (objPermiso.usme_update == "0" || objPermiso.usme_select == "0")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                    else
                    {
                        //if (objPermiso.usme_update == "0")
                        //    btn_tedcu.Visible = false;

                        //grid_bind_estados();
                    }
                }
                else
                {
                    //btn_tedcu.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                }
                //grid_bind_estados();

            }
            catch (Exception ex)
            {
                //Logs
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tedcu", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
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

        protected void linkBttnBusca_Click(object sender, EventArgs e)
        {
            try
            {
                Carga_Estudiante();
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tedcu", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void Carga_Estudiante()
        {
            ModelObtenerAlumnosResponse datosAlumno = new ModelObtenerAlumnosResponse();
            txt_alumno.Text = string.Empty;
            DataTable dtAlumno = new DataTable();
            GridAlumnos.Visible = false;


            try
            {
                dtAlumno = serviceAlumno.ObtenerAlumnos(txt_matricula.Text);
                if (dtAlumno.Rows.Count == 1)
                {
                    txt_alumno.Text = dtAlumno.Rows[0][1].ToString() + " " + dtAlumno.Rows[0][2].ToString() + " " + dtAlumno.Rows[0][3].ToString();
                    Global.cuenta = txt_matricula.Text;
                    Global.nombre = dtAlumno.Rows[0][1].ToString();
                    Global.ap_paterno = dtAlumno.Rows[0][2].ToString();
                    Global.ap_materno = dtAlumno.Rows[0][3].ToString();


                    Carga_Programa();
                    Carga_Periodos();

                    Gridtedcu.DataSource = null;
                    Gridtedcu.DataBind();

                    //Global.campus = dtAlumno[0].testu_tcamp_clave;
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
                Global.inserta_log(mensaje_error, "tedcu", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }

        }

        protected void linkBttnBusca2_Click(object sender, EventArgs e)
        {

            Session["lstSuma"] = null;
            Session["lstTedcu"] = null;
            Session["lstTPagos"] = null;
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

                        Carga_Periodos();

                        Gridtedcu.DataSource = null;
                        Gridtedcu.DataBind();

                        Global.campus = lstAlumno[0].testu_tcamp_clave;

                    }
                    ddl_programa_SelectedIndexChanged(null, null);
                }
                else
                {
                    rowAlumnos.Visible = true;
                    GridAlumnos.DataSource = serviceAlumno.ObtenerAlumnos(txt_matricula.Text);
                    //    Gridtedcu.DataSource = lst;
                    GridAlumnos.DataBind();
                }
            }
            catch (Exception ex)
            {
                //Logs
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tedcu", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void Carga_Periodos()
        {
            ddl_periodo_concep.DataSource = serviceAlumno.ObtenerPeriodoAlumno(txt_matricula.Text);
            ddl_periodo_concep.DataValueField = "Clave";
            ddl_periodo_concep.DataTextField = "Descripcion";
            ddl_periodo_concep.DataBind();


            ddl_periodo.DataSource = serviceAlumno.ObtenerPeriodoAlumno(txt_matricula.Text);
            ddl_periodo.DataValueField = "Clave";
            ddl_periodo.DataTextField = "Descripcion";
            ddl_periodo.DataBind();

            ddl_periodo_SelectedIndexChanged(null, null);
        }
        protected void Carga_Programa()
        {
            try
            {

                ddl_programa.DataSource = serviceAlumno.ObtenerProgramaAlumnoMatricula(txt_matricula.Text);
                ddl_programa.DataValueField = "Clave";
                ddl_programa.DataTextField = "Descripcion";
                ddl_programa.DataBind();

            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tedcu", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                //Response.Redirect("Inicio.aspx");
            }
        }

        protected void btn_pagos_Click(object sender, EventArgs e)
        {
            //if (TxtSuma.Text == "" || TxtSuma.Text == "0")
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "ImportePagar", "ImportePagar();", true);
            //}
            //else
            //{
            //    btn_pagos_ON();
            //}
            btn_pagos_ON();

        }

        private void btn_pagos_OFF()
        {
            ImgConsulta1.Visible = false;
            Conc_pago.Visible = false;
            txt_concepto.Visible = false;
            txt_nom_concepto.Visible = false;
            Importe.Visible = false;
            TxtCantidad.Visible = false;
            LblPago.Visible = false;
            TxtPago.Visible = false;
            ImgGo.Visible = false;

            Conc_pago.Visible = false;
            txt_concepto.Text = "";
            txt_nom_concepto.Text = "";
            TxtCantidad.Text = "";
            //TxtSuma.Text = "";
            TxtPago.Text = "";
            //GridPagos.Visible = false;
            LblAplica.Visible = false;
            ImgAplica.Visible = false;

        }

        private void btn_pagos_ON()
        {
            //Gridtedcu.Visible = false;
            //ImgConsulta1.Visible = true;
            //Conc_pago.Visible = true;
            //txt_concepto.Visible = true;
            //txt_nom_concepto.Visible = true;
            //Importe.Visible = true;
            //TxtCantidad.Visible = true;
            //LblPago.Visible = true;
            //TxtPago.Visible = true;
            //ImgGo.Visible = true;


            //lblTotalFinal.Text = "0";
            //hddnGranTotal.Value= "0";


            LblAplica.Visible = true;
            ImgAplica.Visible = true;

            string strQueryPago = "";

            strQueryPago = " select '----' tpag1_tprog_clave, '----------' tpag1_tcoco_clave,0 tpag1_importe, 0 tpag1_tedcu_consec " +
                " from dual ";
            MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            conexion.Open();
            DataSet ds1 = new DataSet();
            MySqlDataAdapter dataadapter2 = new MySqlDataAdapter(strQueryPago, conexion);
            dataadapter2.Fill(ds1, "Pagos");
            GridPagos.DataSource = ds1;
            GridPagos.DataBind();
            GridPagos.DataMember = "Pagos";

            //GridPagos.Visible = true;
            paso2.Visible = true;
            paso2_enabled.Visible = false;
        }

        //protected void GridSolicitudes_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    GridViewRow row = GridSolicitudes.SelectedRow;
        //    txt_matricula.Text = row.Cells[1].Text;
        //    txt_alumno.Text = HttpUtility.HtmlDecode(row.Cells[2].Text) + " " + HttpUtility.HtmlDecode(row.Cells[3].Text) +
        //        " " + HttpUtility.HtmlDecode(row.Cells[4].Text);
        //    GridSolicitudes.Visible = false;
        //    txt_matricula.Attributes.Add("readonly", "");
        //    txt_alumno.Attributes.Add("readonly", "");
        //    Global.cuenta = txt_matricula.Text;
        //    Global.nombre_alumno = txt_alumno.Text;
        //    GridSolicitudes.Visible = false;
        //    Programa();
        //}

        protected void Programa(object sender, EventArgs e)
        {
            Programa();
        }

        private void Programa()
        {
            btn_pagos_OFF();

            if (txt_alumno.Text == "" || txt_alumno.Text != Global.nombre_alumno.ToString())
            {
                string QerySelect = "select concat(tpers_nombre,' ',tpers_paterno,' ',tpers_materno) from tpers " +
                              " where tpers_id = '" + txt_matricula.Text + "'";

                MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                conexion.Open();
                try
                {
                    MySqlDataAdapter sqladapter = new MySqlDataAdapter();

                    DataSet dssql1 = new DataSet();

                    MySqlCommand commandsql1 = new MySqlCommand(QerySelect, conexion);
                    sqladapter.SelectCommand = commandsql1;
                    sqladapter.Fill(dssql1);
                    sqladapter.Dispose();
                    commandsql1.Dispose();
                    if (dssql1.Tables[0].Rows.Count == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "NoExist", "NoExist();", true);
                    }
                    else
                    {
                        txt_alumno.Text = dssql1.Tables[0].Rows[0][0].ToString();
                        Global.cuenta = txt_matricula.Text;
                        Global.nombre_alumno = txt_alumno.Text;
                    }
                }
                catch (Exception ex)
                {
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tedcu", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                }
            }


            string strQueryestu = ""; string strQueryadmi = "";
            strQueryestu = "select distinct testu_tprog_clave clave, concat(tcamp_desc,' || ',tprog_desc) programa" +
                 " from testu, tprog, tcamp " +
                 " where testu_tpers_num in (select tpers_num from tpers where tpers_id ='" + txt_matricula.Text + "') " +
                 " and testu_tprog_clave = tprog_clave and testu_tcamp_clave = tcamp_clave " +
                            " union " +
                            " select '0' clave, '-------' programa " +
                            " order by 1";

            strQueryadmi = "select distinct tadmi_tprog_clave clave, concat(tcamp_desc,' || ',tprog_desc) programa" +
                 " from tadmi, tprog, tcamp " +
                 " where tadmi_tpers_num in (select tpers_num from tpers where tpers_id ='" + txt_matricula.Text + "') " +
                 " and tadmi_tprog_clave = tprog_clave and tadmi_tcamp_clave = tcamp_clave " +
                            " union " +
                            " select '0' clave, '-------' programa " +
                            " order by 1";

            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            ConexionMySql.Open();
            DataTable TablaEstado = new DataTable();
            DataTable TablaAdmi = new DataTable();
            MySqlCommand ConsultaMySql = new MySqlCommand();
            MySqlDataReader DatosMySql;
            MySqlCommand ConsultaMySql1 = new MySqlCommand();
            MySqlDataReader DatosMySql1;
            try
            {
                ConsultaMySql.Connection = ConexionMySql;
                ConsultaMySql.CommandType = CommandType.Text;
                ConsultaMySql.CommandText = strQueryestu;
                DatosMySql = ConsultaMySql.ExecuteReader();
                TablaEstado.Load(DatosMySql, LoadOption.OverwriteChanges);

                if (TablaEstado.Rows.Count > 1)
                {
                    ddl_programa.DataSource = TablaEstado;
                    ddl_programa.DataValueField = "Clave";
                    ddl_programa.DataTextField = "Programa";
                    ddl_programa.DataBind();
                    //btn_tedcu.Visible = true;
                }
                else
                {
                    try
                    {
                        ConsultaMySql1.Connection = ConexionMySql;
                        ConsultaMySql1.CommandType = CommandType.Text;
                        ConsultaMySql1.CommandText = strQueryadmi;
                        DatosMySql1 = ConsultaMySql1.ExecuteReader();
                        TablaAdmi.Load(DatosMySql1, LoadOption.OverwriteChanges);
                        ddl_programa.DataSource = TablaAdmi;
                        ddl_programa.DataValueField = "Clave";
                        ddl_programa.DataTextField = "Programa";
                        ddl_programa.DataBind();
                        //btn_tedcu.Visible = true;
                    }
                    catch (Exception ex)
                    {
                        string test = ex.Message;
                        string mensaje_error = ex.Message.Replace("'", "-");
                        Global.inserta_log(mensaje_error, "tedcu", Session["usuario"].ToString());
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                    }
                    finally
                    {
                        ConexionMySql.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tedcu", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
            finally
            {
                ConexionMySql.Close();
            }
        }

        private void Carga_tedcu()
        {
            try
            {


                serviceCobro.EliminarConsecCobranza(txt_matricula.Text, ddl_programa.SelectedValue);

                //string strBorra = " delete from tpag1 " +
                //       " where tpag1_tpers_num = (select tpers_num from tpers where tpers_id = '" + txt_matricula.Text + "')" +
                //       " and   tpag1_tprog_clave='" + ddl_programa.SelectedValue + "'" +
                //       " and   tpag1_tedcu_consec between 1 and 100 ";
                // resultado.Text = resultado.Text + strBorra;

                grid_bind_tedcu();
                //Gridtedcu.Visible = true;
                linkBttnPagosAplicados.Visible = true;

            }
            catch (Exception ex)
            {
                //logs
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tedcu", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }

        }

        protected void Carga_tedcu(object sender, EventArgs e)
        {
            Carga_tedcu();
        }

        protected void suma_click(object sender, EventArgs e)
        {
            List<ModelObtenerDatosCobroResponse> lst = new List<ModelObtenerDatosCobroResponse>();

            List<Pago> lst2 = new List<Pago>();
            Pago objTesu = new Pago();

            TextBox cbi = (TextBox)(sender);
            GridViewRow row = (GridViewRow)cbi.NamingContainer;
            Gridtedcu.SelectedIndex = row.RowIndex;
            int indice_ant = 0;
            int indice_post = 0;

            try
            {
                Label lblTot = (Label)Gridtedcu.FooterRow.FindControl("lblGranTotal");
                LinkButton linkBttnPagar = (LinkButton)Gridtedcu.FooterRow.FindControl("linkBttnPagar");

                decimal total = 0;

                lst = (List<ModelObtenerDatosCobroResponse>)Session["lstTedcu"]; //Session["lstTedcu"];

                if (row.RowIndex > 0)
                {
                    if (cbi.Text != string.Empty)
                    {
                        indice_ant = row.RowIndex - 1;
                        indice_post = row.RowIndex + 1;

                        TextBox txt = Gridtedcu.Rows[indice_ant].Cells[7].FindControl("suma") as TextBox;

                        if (Convert.ToDecimal(cbi.Text) > Convert.ToDecimal(Gridtedcu.Rows[row.RowIndex].Cells[5].Text))
                        {
                            cbi.Text = "0"; //Convert.ToString(Gridtedcu.Rows[row.RowIndex].Cells[5].Text);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "error_saldo", "error_saldo();", true);
                            cbi.Focus();
                        }
                        else
                        {
                            if (Convert.ToDecimal(cbi.Text) == Convert.ToDecimal(Gridtedcu.Rows[row.RowIndex].Cells[5].Text))
                            {
                                txt.Enabled = true;
                                txt.Focus();


                                lst[row.RowIndex].importe = cbi.Text;
                                Session["lstTedcu"] = lst;


                                total = lst.Sum(x => Convert.ToDecimal(x.importe));
                                lblTot.Text = Convert.ToString(total);
                                lblTotalFinal.Text = total.ToString("C");
                                hddnGranTotal.Value = Convert.ToString(total);
                                linkBttnPagar.Visible = true;
                            }
                            else if (row.RowIndex > 0 && (Convert.ToDecimal(cbi.Text) < Convert.ToDecimal(Gridtedcu.Rows[row.RowIndex].Cells[5].Text)
                            && txt.Text != string.Empty))
                            {

                                for (var i = 0; i < lst.Count; i++)
                                {
                                    TextBox txt2 = Gridtedcu.Rows[i].Cells[8].FindControl("suma") as TextBox;
                                    if (Gridtedcu.Rows[i].Cells[3].Text == "ILI" || Gridtedcu.Rows[i].Cells[3].Text == "PLI")
                                    {
                                        if (i < row.RowIndex)
                                        {
                                            string valor = txt2.Text;
                                            txt2.Text = string.Empty;
                                            txt2.Enabled = false;
                                            lst[i].importe = "0";
                                        }
                                        else if (i == row.RowIndex)
                                            lst[i].importe = txt2.Text; //cbi.Text;
                                    }
                                    else
                                        lst[i].importe = txt2.Text;
                                }

                                Session["lstTedcu"] = lst;


                                total = lst.Sum(x => Convert.ToDecimal(x.importe));
                                lblTot.Text = Convert.ToString(total);
                                lblTotalFinal.Text = total.ToString("C");
                                hddnGranTotal.Value = Convert.ToString(total);
                                linkBttnPagar.Visible = true;
                                //linkBttnPagar.Visible = false;
                                //ScriptManager.RegisterStartupScript(this, this.GetType(), "error_saldo_menor", "error_saldo_menor();", true);
                            }
                            else
                            {
                                lst[row.RowIndex].importe = cbi.Text;
                                Session["lstTedcu"] = lst;


                                total = lst.Sum(x => Convert.ToDecimal(x.importe));
                                lblTot.Text = Convert.ToString(total);
                                lblTotalFinal.Text = total.ToString("C");
                                hddnGranTotal.Value = Convert.ToString(total);
                                linkBttnPagar.Visible = true;
                                if (lst[row.RowIndex].clave != "PLI")
                                    if (indice_ant >= 0)
                                        txt.Enabled = true;

                            }


                        }
                    }
                    else
                    {
                        //total = Convert.ToDecimal(lblTot.Text) + 0;
                        //lblTot.Text = Convert.ToString(total);
                        objTesu.saldo = "0";
                        lst[row.RowIndex].importe = "0";
                        Session["lstTedcu"] = lst;

                        total = lst.Sum(x => Convert.ToDecimal(x.importe));
                        lblTot.Text = Convert.ToString(total);
                    }

                }
                else
                {
                    indice_ant = row.RowIndex - 1;
                    indice_post = row.RowIndex + 1;
                    TextBox txt_post = Gridtedcu.Rows[indice_post].Cells[7].FindControl("suma") as TextBox;

                    if (cbi.Text != string.Empty)
                    {
                        if (Convert.ToDecimal(cbi.Text) > Convert.ToDecimal(Gridtedcu.Rows[row.RowIndex].Cells[5].Text))
                        {
                            cbi.Text = "0"; //Convert.ToString(Gridtedcu.Rows[row.RowIndex].Cells[5].Text);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "error_saldo", "error_saldo();", true);
                            cbi.Focus();
                        }
                        else
                        {

                            //cbi.Text = Convert.ToString(Gridtedcu.Rows[row.RowIndex].Cells[5].Text);
                            lst[0].importe = cbi.Text;
                            Session["lstTedcu"] = lst;


                            total = lst.Sum(x => Convert.ToDecimal(x.importe));
                            lblTot.Text = Convert.ToString(total);
                            lblTotalFinal.Text = total.ToString("C");
                            hddnGranTotal.Value = Convert.ToString(total);
                            linkBttnPagar.Visible = true;

                            if (lst[row.RowIndex].clave != "PLI")
                                if (indice_post >= 0)
                                    txt_post.Enabled = true;
                        }
                    }
                    else
                    {
                        lblTot.Text = "0";
                        linkBttnPagar.Visible = false;
                    }
                }
            }

            catch (Exception ex)
            {
                //logs
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tedcu", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void suma2_click(object sender, EventArgs e)
        {
            TextBox cbi = (TextBox)(sender);
            GridViewRow row = (GridViewRow)cbi.NamingContainer;
            Gridtedcu.SelectedIndex = row.RowIndex;
            int indice_ant = 0;
            Label lbl = Gridtedcu.Rows[row.RowIndex].Cells[7].FindControl("lblReq") as Label;

            try
            {
                if (row.RowIndex > 0)
                {
                    if (cbi.Text != string.Empty)
                    {
                        indice_ant = row.RowIndex - 1;
                        TextBox txt = Gridtedcu.Rows[indice_ant].Cells[7].FindControl("suma") as TextBox;

                        if (cbi.Text != string.Empty || cbi.Text != "")
                        {
                            lbl.Visible = false;
                            txt.Enabled = true;
                            txt.Focus();
                            Global.cons_pago = row.RowIndex;

                        }
                    }
                    else
                    {
                        indice_ant = row.RowIndex - 1;
                        TextBox txt = Gridtedcu.Rows[indice_ant].Cells[7].FindControl("suma") as TextBox;
                        if (txt.Text != string.Empty)
                        {
                            lbl.Visible = true;
                            cbi.Focus();
                        }
                    }
                }
                if (cbi.Text != "")
                {

                    if (Convert.ToDecimal(cbi.Text) > Convert.ToDecimal(Gridtedcu.Rows[row.RowIndex].Cells[5].Text.ToString()))
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error_saldo", "error_saldo();", true);

                }
                //else
                //{
                //    indice_ant=Convert.ToInt32(Global.cons_pago);
                //    TextBox txt = Gridtedcu.Rows[indice_ant].Cells[7].FindControl("suma") as TextBox;
                //    txt.Enabled = false;
                //    txt.Text = string.Empty;
                //}
            }
            catch (Exception ex)
            {
                //logs
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tedcu", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }
        protected void Gridtcoco_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = Gridtcoco.SelectedRow;
            txt_concepto.Text = row.Cells[1].Text;
            txt_nom_concepto.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);
            Gridtcoco.Visible = false;
            txt_concepto.Attributes.Add("readonly", "");
            txt_nom_concepto.Attributes.Add("readonly", "");
        }

        protected void Gridtpago_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridPagos.SelectedRow;
            ddl_forma_pago.SelectedValue = row.Cells[2].Text;
            txt_importe_efect.Text = row.Cells[3].Text;
            //TxtCantidad.Text = row.Cells[3].Text;
            //txt_concepto.Attributes.Add("readonly", "");
            //txt_nom_concepto.Attributes.Add("readonly", "");
            //Global.consecutivo = Convert.ToDouble(row.Cells[4].Text);
            if (row.Cells[3].Text != string.Empty)
                Global.importe_pago = Convert.ToDecimal(row.Cells[3].Text);




            //txt_concepto.Text = row.Cells[1].Text;
            //txt_nom_concepto.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);
            //TxtCantidad.Text = row.Cells[3].Text;
            //txt_concepto.Attributes.Add("readonly", "");
            //txt_nom_concepto.Attributes.Add("readonly", "");
            //Global.consecutivo = Convert.ToDouble(row.Cells[4].Text);
            //Global.importe_pago = Convert.ToDecimal(row.Cells[3].Text);
        }

        protected void grid_conceptos_bind(object sender, EventArgs e)
        {
            if (Gridtcoco.Visible == true)
            {
                Gridtcoco.Visible = false;
            }
            else
            {
                string QueryEstudiantes = "select tcoco_clave codigo, tcoco_desc concepto from tcoco where tcoco_estatus='A' and tcoco_tipo='P' and tcoco_categ='PC' ";
                MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                conexion.Open();
                try
                {
                    MySqlDataAdapter dataadapter = new MySqlDataAdapter(QueryEstudiantes, conexion);
                    DataSet ds = new DataSet();
                    dataadapter.Fill(ds, "Conceptos");
                    Gridtcoco.DataSource = ds;
                    Gridtcoco.DataBind();
                    Gridtcoco.DataMember = "Conceptos";
                    Gridtcoco.HeaderRow.TableSection = TableRowSection.TableHeader;
                    Gridtcoco.UseAccessibleHeader = true;

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_tcoco", "load_datatable_tcoco();", true);
                    Gridtcoco.Visible = true;
                }
                catch (Exception ex)
                {
                    //logs
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tedcu", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                }
                conexion.Close();
            }
        }

        protected void Agregar_Click(object sender, EventArgs e)
        {
            int vale = 0;
            if (!String.IsNullOrEmpty(txt_concepto.Text) && !String.IsNullOrEmpty(TxtCantidad.Text))
            {
                decimal resultado1 = 0;

                bool cantidad = Decimal.TryParse(TxtCantidad.Text, out resultado1);

                if (cantidad)
                {
                    vale = 1;
                    // Si llega hasta aquí, resultado es numérico.
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    if (cantidad)
                    {
                        //
                    }
                    else
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "valida_cantidad();", true);
                    }
                }

                if (vale == 1)
                {
                    MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                    conexion.Open();
                    if (Global.consecutivo != 0)
                    {
                        string strCupSQL = "update tpag1 set tpag1_importe=" + Convert.ToDecimal(TxtCantidad.Text) +
                               " where tpag1_tpers_num = (select tpers_num from tpers where tpers_id = '" + txt_matricula.Text + "')" +
                               " and tpag1_tedcu_consec = " + Global.consecutivo;
                        //resultado.Text = resultado.Text + strCupSQL;

                        MySqlCommand myCommand = new MySqlCommand(strCupSQL, conexion);
                        //Ejecucion del comando en el servidor de BD
                        myCommand.ExecuteNonQuery();
                        Global.consecutivo = 0;
                    }
                    else
                    {
                        Global.cons_pago = Global.cons_pago + 1;
                        string strCadSQL = "insert into tpag1 values((select tpers_num from tpers where tpers_id='" + txt_matricula.Text + "'),'" +
                          ddl_programa.SelectedValue + "','" + txt_concepto.Text + "'," + Convert.ToDecimal(TxtCantidad.Text) + "," +
                            Global.cons_pago + ")";
                        MySqlCommand myCommandinserta = new MySqlCommand(strCadSQL, conexion);
                        //Ejecucion del comando en el servidor de BD
                        myCommandinserta.ExecuteNonQuery();
                    }
                    txt_concepto.Text = "";
                    txt_nom_concepto.Text = "";
                    TxtCantidad.Text = "";

                    string strQueryPagos = "";
                    strQueryPagos = " select tpag1_tcoco_clave clave, tcoco_desc concepto,tpag1_importe saldo, tpag1_tedcu_consec consec " +
                        " from tpers, tpag1, tcoco " +
                        " where  tpers_id = '" + txt_matricula.Text + "' and tpag1_tpers_num = tpers_num " +
                        " and tpag1_tprog_clave = '" + ddl_programa.SelectedValue + "'" +
                        " and tcoco_clave = tpag1_tcoco_clave " +
                        " order by tpag1_tedcu_consec ";

                    //resultado.Text = "1--" + strQueryPagos;
                    DataSet ds = new DataSet();
                    MySqlDataAdapter dataadapter1 = new MySqlDataAdapter(strQueryPagos, conexion);
                    dataadapter1.Fill(ds, "Pagos");
                    GridPagos.DataSource = ds;
                    GridPagos.DataBind();
                    GridPagos.DataMember = "Pagos";
                    conexion.Close();
                    decimal suma = 0;
                    decimal importe = 0;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        importe = Convert.ToDecimal(ds.Tables[0].Rows[i][2].ToString());
                        suma = suma + importe;
                        //resultado.Text = resultado.Text + "-" + TxtPago.Text + "-" + ds.Tables[0].Rows[i][2].ToString();
                    }
                    TxtPago.Text = suma.ToString();

                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "validar_campos_pago", "validar_campos_pago();", true);
            }
        }

        protected void consulta_docs_Click(object sender, ImageClickEventArgs e)
        {
            //Response.Redirect("Tcper.aspx");
        }

        protected void txt_concepto_TextChanged(object sender, EventArgs e)
        {
            string QerySelect = "select tcoco_desc from tcoco " +
                              " where tcoco_clave = '" + txt_concepto.Text + "'";

            MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            conexion.Open();
            try
            {
                MySqlDataAdapter sqladapter = new MySqlDataAdapter();

                DataSet dssql1 = new DataSet();

                MySqlCommand commandsql1 = new MySqlCommand(QerySelect, conexion);
                sqladapter.SelectCommand = commandsql1;
                sqladapter.Fill(dssql1);
                sqladapter.Dispose();
                commandsql1.Dispose();
                if (dssql1.Tables[0].Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "NoExistConcepto", "NoExistConcepto();", true);
                }
                else
                {
                    txt_nom_concepto.Text = dssql1.Tables[0].Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tedcu", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void PDF_Click(object sender, EventArgs e)
        {
            Global.cuenta = txt_matricula.Text;
            Global.nombre_alumno = txt_alumno.Text;
            Global.programa = ddl_programa.SelectedValue;
            Global.periodo = ddl_periodo.SelectedValue;
            Global.nombre_programa = ddl_programa.SelectedItem.ToString();
            //ImgPdf.Visible = false;
            Global.opcion = 1;
            Response.Redirect("Tcedc2.aspx");
        }

        protected void Guardar_Pago(object sender, EventArgs e)
        {
            try
            {
                Aplicar_Pago();
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tedcu", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void Aplicar_Pago()
        {
            if (GridPagos.Rows.Count > 0)
            {
                Label lblTot = (Label)GridPagos.FooterRow.FindControl("lblGranTotalFin");


                if (Convert.ToDecimal(hddnGranTotal.Value) != Convert.ToDecimal(lblTot.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ImporteDiferente", "ImporteDiferente();", true);
                }
                else
                {
                    decimal cantidad = 0;
                    string strCupSQL;
                    MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                    conexion.Open();
                    for (int i = 0; i < Gridtedcu.Rows.Count; i++)
                    {
                        TextBox importe = (TextBox)Gridtedcu.Rows[i].FindControl("suma");

                        if (importe.Text != "")
                        {
                            cantidad = Convert.ToDecimal(importe.Text);
                            //cantidad = Convert.ToDecimal(lblTot.Text);

                            if (cantidad > 0)
                            {
                                // Actualiza el estado de cuenta
                                strCupSQL = "update tedcu set tedcu_balance=tedcu_balance-" + cantidad +
                                   " where tedcu_tpers_num = (select tpers_num from tpers where tpers_id = '" + txt_matricula.Text + "')" +
                                   " and tedcu_consec = " + Gridtedcu.Rows[i].Cells[0].Text.ToString();
                                //resultado.Text = resultado.Text + strCupSQL;

                                MySqlCommand myCommandinserta = new MySqlCommand(strCupSQL, conexion);
                                //Ejecucion del comando en el servidor de BD
                                myCommandinserta.ExecuteNonQuery();

                                // Verifica el último registro cargado en testu
                                string strQueryEstu = "";
                                strQueryEstu = "  select testu_tpees_clave, testu_tcamp_clave, testu_tprog_clave, testu_ttasa_clave, testu_tespe_clave, testu_tpers_num " +
                                    " from testu a, tpers " +
                                    " where tpers_id='" + txt_matricula.Text + "' and testu_tpers_num=tpers_num and testu_tpees_clave in (select max(testu_tpees_clave) " +
                                    " from testu b where a.testu_tpers_num=b.testu_tpers_num) ";

                                MySqlDataAdapter sqladapter = new MySqlDataAdapter();

                                DataSet dssql1 = new DataSet();

                                MySqlCommand commandsql1 = new MySqlCommand(strQueryEstu, conexion);
                                sqladapter.SelectCommand = commandsql1;
                                sqladapter.Fill(dssql1);
                                sqladapter.Dispose();
                                commandsql1.Dispose();

                                if (dssql1.Tables[0].Rows.Count > 0) // Existe registro en testu
                                {
                                    // verifica si el pago es sobre un concepto de colegiatura
                                    Global.campus = dssql1.Tables[0].Rows[0][1].ToString();

                                    string strQueryComb = "";
                                    strQueryComb = " select count(*) from tcomb where tcomb_ttasa_clave='" + dssql1.Tables[0].Rows[0][3].ToString() + "' " +
                                        " and tcomb_tcoco_clave='" + Gridtedcu.Rows[i].Cells[3].Text.ToString() + "'";

                                    DataSet dscomb = new DataSet();

                                    MySqlCommand commandcomb = new MySqlCommand(strQueryComb, conexion);
                                    sqladapter.SelectCommand = commandcomb;
                                    sqladapter.Fill(dscomb);
                                    sqladapter.Dispose();
                                    commandcomb.Dispose();
                                    if (dscomb.Tables[0].Rows[0][0].ToString() != "0")
                                    {
                                        try
                                        {

                                            if (dssql1.Tables[0].Rows[0][0].ToString() == Gridtedcu.Rows[i].Cells[1].Text.ToString().Trim()) // Existe el periodo en testu
                                            {
                                                //Actualiza el estatus AC=Activo
                                                string strAcEstu = "";
                                                strAcEstu = " update testu set testu_tstal_clave='AC' where testu_tpers_num = " + dssql1.Tables[0].Rows[0][5].ToString() +
                                                    " and testu_tcamp_clave='" + dssql1.Tables[0].Rows[0][1].ToString() + "'  and testu_tprog_clave='" +
                                                    ddl_programa.SelectedValue + "' and testu_tpees_clave='" + Gridtedcu.Rows[i].Cells[1].Text.ToString().Trim() + "'";
                                                MySqlCommand strUpdate = new MySqlCommand(strAcEstu, conexion);
                                                strUpdate.ExecuteNonQuery();


                                            }
                                            else
                                            {
                                                //Se debe calcular el periodo programa --
                                                //Inserta registro en testu
                                                string strInEstu = "";
                                                strInEstu = " insert into testu values(" + dssql1.Tables[0].Rows[0][5].ToString() + ",'" + Gridtedcu.Rows[i].Cells[1].Text.ToString().Trim() + "','" +
                                                    dssql1.Tables[0].Rows[0][1].ToString() + "','" + ddl_programa.SelectedValue + "','" + dssql1.Tables[0].Rows[0][3].ToString() + "','AC',1," +
                                                    dssql1.Tables[0].Rows[0][4].ToString() + "','" + Session["usuario"].ToString() + "',current_timestamp())";
                                                MySqlCommand strInsert = new MySqlCommand(strInEstu, conexion);
                                                strInsert.ExecuteNonQuery();
                                            }
                                            //Actualiza el estatus de la solicitud a TE=Terminada
                                            string strAcADmi = " update tadmi set tadmi_tstso_clave='TE' where tadmi_tpers_num = " + dssql1.Tables[0].Rows[0][5].ToString() +
                                            " and tadmi_tcamp_clave='" + dssql1.Tables[0].Rows[0][1].ToString() + "'  and tadmi_tprog_clave='" +
                                            ddl_programa.SelectedValue + "' and tadmi_tpees_clave='" + Gridtedcu.Rows[i].Cells[1].Text.ToString().Trim() + "'";
                                            MySqlCommand strUpAdmi = new MySqlCommand(strAcADmi, conexion);
                                            strUpAdmi.ExecuteNonQuery();

                                        }
                                        catch (Exception ex)
                                        {
                                            string mensaje_error = ex.Message.Replace("'", "-");
                                            Global.inserta_log(mensaje_error, "tedcu", Session["usuario"].ToString());
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                                        }
                                    }
                                }
                                else
                                {
                                    // Toma valores de tadmi
                                    string strQueryAdmi = "";
                                    strQueryAdmi = "  select tadmi_tpees_clave, tadmi_tcamp_clave, tadmi_tprog_clave, tadmi_ttasa_clave, tadmi_tpers_num, tadmi_turno, tprog_tnive_clave, " +
                                        " (select case when ttama_materias is null then 0 else ttama_materias end  from ttama where ttama_ttasa_clave=tadmi_ttasa_clave and ttama_ttima_clave='C') curr, " +
                                        " (select case when ttama_materias is null then 0 else ttama_materias end  from ttama where ttama_ttasa_clave = tadmi_ttasa_clave and ttama_ttima_clave = 'I') idio " +
                                        " from tadmi a, tpers, tprog " +
                                        " where tpers_id='" + txt_matricula.Text + "' and tadmi_tpers_num=tpers_num and tadmi_tprog_clave='" + ddl_programa.SelectedValue + " '" +
                                        " and tadmi_consecutivo in (select max(tadmi_consecutivo) from tadmi b where a.tadmi_tpers_num=b.tadmi_tpers_num and a.tadmi_tprog_clave=b.tadmi_tprog_clave) " +
                                        " and tprog_clave=tadmi_tprog_clave ";
                                    //resultado.Text =  "<<" + strQueryAdmi;
                                    DataSet dsadmi = new DataSet();
                                    try
                                    {
                                        MySqlCommand commandadmi = new MySqlCommand(strQueryAdmi, conexion);
                                        sqladapter.SelectCommand = commandadmi;
                                        sqladapter.Fill(dsadmi);
                                        sqladapter.Dispose();
                                        commandadmi.Dispose();
                                        string strInEstu = "";
                                        strInEstu = " insert into testu values(" + dsadmi.Tables[0].Rows[0][4].ToString() + ",'" + Gridtedcu.Rows[i].Cells[1].Text.ToString().Trim() + "','" +
                                            dsadmi.Tables[0].Rows[0][1].ToString() + "','" + ddl_programa.SelectedValue + "','" + dsadmi.Tables[0].Rows[0][3].ToString() + "','AC',1,''," +
                                             "'" + dsadmi.Tables[0].Rows[0][5].ToString() + "','" + Session["usuario"].ToString() + "',current_timestamp())";
                                        //resultado.Text = resultado.Text + "Ins:" + strInEstu;
                                        MySqlCommand strInsert = new MySqlCommand(strInEstu, conexion);
                                        strInsert.ExecuteNonQuery();
                                        Global.campus = dsadmi.Tables[0].Rows[0][1].ToString();
                                        //Actualiza el estatus de la solicitud a TE=Terminada
                                        string strAcADmi = " update tadmi set tadmi_tstso_clave='TE' where tadmi_tpers_num = " + dsadmi.Tables[0].Rows[0][4].ToString() +
                                        " and tadmi_tcamp_clave='" + dsadmi.Tables[0].Rows[0][1].ToString() + "'  and tadmi_tprog_clave='" +
                                        ddl_programa.SelectedValue + "' and tadmi_tpees_clave='" + Gridtedcu.Rows[i].Cells[1].Text.ToString().Trim() + "'";
                                        MySqlCommand strUpAdmi = new MySqlCommand(strAcADmi, conexion);
                                        strUpAdmi.ExecuteNonQuery();
                                        string strBorra = " delete from ttir1 where ttir1_tpees_clave='" + Gridtedcu.Rows[i].Cells[1].Text.ToString().Trim() + "' " +
                                            " and ttir1_tpers_num=" + dsadmi.Tables[0].Rows[0][4].ToString();
                                        MySqlCommand myCommandborra = new MySqlCommand(strBorra, conexion);
                                        myCommandborra.ExecuteNonQuery();
                                    }
                                    catch (Exception ex)
                                    {
                                        string mensaje_error = ex.Message.Replace("'", "-");
                                        Global.inserta_log(mensaje_error, "tedcu", Session["usuario"].ToString());
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                                    }

                                    double tpers_num, curr, idio;
                                    string prog, campus, nivel;

                                    tpers_num = Convert.ToDouble(dsadmi.Tables[0].Rows[0][4].ToString());
                                    prog = ddl_programa.SelectedValue;
                                    campus = dsadmi.Tables[0].Rows[0][1].ToString();
                                    nivel = dsadmi.Tables[0].Rows[0][6].ToString();
                                    curr = Convert.ToDouble(dsadmi.Tables[0].Rows[0][7].ToString());
                                    idio = Convert.ToDouble(dsadmi.Tables[0].Rows[0][8].ToString());

                                    /*resultado.Text = resultado.Text + "-->" + "tpers_num =" + Convert.ToDouble(dsadmi.Tables[0].Rows[0][4].ToString());
                                    resultado.Text = resultado.Text + "-->" + "prog =" + CboProg.SelectedValue.ToString();
                                    resultado.Text = resultado.Text + "-->" + "campus =" + dsadmi.Tables[0].Rows[0][1].ToString();
                                    resultado.Text = resultado.Text + "-->" + "nivel =" + dsadmi.Tables[0].Rows[0][6].ToString();
                                    resultado.Text = resultado.Text + "-->" + "curr =" + Convert.ToDouble(dsadmi.Tables[0].Rows[0][7].ToString());
                                    resultado.Text = resultado.Text + "-->" + "idio =" + Convert.ToDouble(dsadmi.Tables[0].Rows[0][8].ToString());*/

                                    // resultado.Text = resultado.Text + "-->" + GridInscritos.Rows[i].Cells[0].Text.ToString() + "curr:" + curr;

                                    if (curr > 0)
                                    {
                                        string StrMaterias = "select tplan_periodo, tplan_consecutivo,tplan_tmate_clave,tplan_ttima_clave from tplan " +
                                          " where tplan_tprog_clave = '" + prog + "'" +
                                          " and tplan_tmate_clave not in (select tkard_tmate_clave from tkard , tcali " +
                                          "               where tkard_tpers_num =" + tpers_num + " and tkard_tprog_clave = tplan_tprog_clave " +
                                          "               and tkard_tmate_clave = tplan_tmate_clave and tcali_clave = tkard_tcali_clave and tcali_ind_aprob = 'S') " +
                                          " and tplan_tmate_clave not in (select ttira_tmate_clave from ttira " +
                                          "         where ttira_tpers_num = " + tpers_num + " and ttira_tprog_clave = '" + prog + "' and ttira_tpees_clave = '" +
                                                    Gridtedcu.Rows[i].Cells[1].Text.ToString().Trim() + "') " +
                                          " and tplan_tmate_clave not in (select tpred_tmate_clave from tpred " +
                                          "         where tpred_tpers_num = " + tpers_num + " and tpred_tprog_clave = '" + prog + "' and tpred_estatus = 'A') " +
                                          " and tplan_ttima_clave = 'C' " +
                                          " order by tplan_periodo, tplan_consecutivo ";



                                        DataSet dsttira = new DataSet();

                                        MySqlCommand commandttira = new MySqlCommand(StrMaterias, conexion);
                                        sqladapter.SelectCommand = commandttira;
                                        sqladapter.Fill(dsttira);
                                        sqladapter.Dispose();   
                                        commandttira.Dispose();

                                        if (dsttira.Tables[0].Rows.Count > 0)
                                        {
                                            double mate = 0;
                                            if (dsttira.Tables[0].Rows.Count >= curr)
                                            {
                                                mate = curr;
                                            }
                                            else
                                            {
                                                mate = dsttira.Tables[0].Rows.Count;
                                            }
                                            //resultado.Text = resultado.Text + "--->" + StrMaterias + "reg:" + dsttira.Tables[0].Rows.Count + "Mate:" + mate; ;
                                            for (int w = 0; w < mate; w++)
                                            {
                                                //resultado.Text = resultado.Text + "C=" + dssql1.Tables[0].Rows[w][2].ToString();
                                                string strCadSQL = "INSERT INTO ttir1 Values (" + tpers_num + ",'" + Gridtedcu.Rows[i].Cells[1].Text.ToString().Trim() + "','" + campus + "','" +
                                                    nivel + "','" + prog + "','" + dsttira.Tables[0].Rows[w][2].ToString() + "','" +
                                                    Session["usuario"].ToString() + "',current_timestamp())";
                                                //resultado.Text = resultado.Text + "---" + strCadSQL;
                                                try
                                                {


                                                    MySqlCommand myCommandins = new MySqlCommand(strCadSQL, conexion);
                                                    //Ejecucion del comando en el servidor de BD
                                                    myCommandins.ExecuteNonQuery();
                                                }
                                                catch (Exception ex)
                                                {
                                                    string mensaje_error = ex.Message.Replace("'", "-");
                                                    Global.inserta_log(mensaje_error, "tedcu", Session["usuario"].ToString());
                                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                                                }

                                            }

                                        }
                                    }

                                    if (idio > 0)
                                    {
                                        string StrMaterias = "select tplan_periodo, tplan_consecutivo,tplan_tmate_clave,tplan_ttima_clave from tplan " +
                                            " where tplan_tprog_clave = '" + prog + "'" +
                                            " and tplan_tmate_clave not in (select tkard_tmate_clave from tkard , tcali " +
                                            "               where tkard_tpers_num =" + tpers_num + " and tkard_tprog_clave = tplan_tprog_clave " +
                                            "               and tkard_tmate_clave = tplan_tmate_clave and tcali_clave = tkard_tcali_clave and tcali_ind_aprob = 'S') " +
                                            " and tplan_tmate_clave not in (select ttira_tmate_clave from ttira " +
                                            "         where ttira_tpers_num = " + tpers_num + " and ttira_tprog_clave = '" + prog + "' and ttira_tpees_clave = '" +
                                                    Gridtedcu.Rows[i].Cells[1].Text.ToString().Trim() + "') " +
                                            " and tplan_tmate_clave not in (select tpred_tmate_clave from tpred " +
                                            "         where tpred_tpers_num = " + tpers_num + " and tpred_tprog_clave = '" + prog + "' and tpred_estatus = 'A') " +
                                            " and tplan_ttima_clave = 'I' " +
                                            " order by tplan_periodo, tplan_consecutivo ";

                                        DataSet dsidio = new DataSet();

                                        MySqlCommand commandidio = new MySqlCommand(StrMaterias, conexion);
                                        sqladapter.SelectCommand = commandidio;
                                        sqladapter.Fill(dsidio);
                                        sqladapter.Dispose();
                                        commandidio.Dispose();

                                        if (dsidio.Tables[0].Rows.Count > 0)
                                        {
                                            double mate = 0;
                                            if (dsidio.Tables[0].Rows.Count >= idio)
                                            {
                                                mate = idio;
                                            }
                                            else
                                            {
                                                mate = dsidio.Tables[0].Rows.Count;
                                            }

                                            for (int w = 0; w < mate; w++)
                                            {
                                                //resultado.Text = resultado.Text + "I=" + dssql1.Tables[0].Rows[w][2].ToString();
                                                string strCadSQL = "INSERT INTO ttir1 Values (" + tpers_num + ",'" + Gridtedcu.Rows[i].Cells[1].Text.ToString() + "','" + campus + "','" +
                                                    nivel + "','" + prog + "','" + dsidio.Tables[0].Rows[w][2].ToString() + "','" +
                                                    Session["usuario"].ToString() + "',current_timestamp())";
                                                //resultado.Text = strBorra + "---" + strCadSQL;
                                                try
                                                {


                                                    MySqlCommand myCommandinsidio = new MySqlCommand(strCadSQL, conexion);
                                                    //Ejecucion del comando en el servidor de BD
                                                    myCommandinsidio.ExecuteNonQuery();
                                                }
                                                catch (Exception ex)
                                                {
                                                    string mensaje_error = ex.Message.Replace("'", "-");
                                                    Global.inserta_log(mensaje_error, "tedcu", Session["usuario"].ToString());
                                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                                                }

                                            }

                                        }

                                    }

                                }
                            }
                        }

                    }
                    string StrActCartera = "";
                    decimal pago = 0;
                    string factura = "";
                    double fact_cons = 0;

                    try
                    {
                        for (int i = 0; i < GridPagos.Rows.Count; i++)
                        {
                            // resultado.Text = resultado.Text + "i:->" + Pagos.Rows[i].Cells[2].Text.ToString();
                            Label lblCvePago = GridPagos.Rows[i].Cells[2].FindControl("lblCvePago") as Label;
                            Label lblCveConcepto = GridPagos.Rows[i].Cells[2].FindControl("lblCvePago") as Label;
                            Label lblPag1 = GridPagos.Rows[i].Cells[4].FindControl("lblImportePag1") as Label;

                            for (int w = 0; w < Gridtedcu.Rows.Count; w++)
                            {
                                TextBox importe = (TextBox)Gridtedcu.Rows[w].FindControl("suma");
                                if (importe.Text != "")
                                {
                                    cantidad = Convert.ToDecimal(importe.Text);


                                    //if (Convert.ToDecimal(GridPagos.Rows[i].Cells[3].Text.ToString()) > 0)
                                    if (Convert.ToDecimal(lblPag1.Text) > 0)
                                    //resultado.Text = resultado.Text + "w" + "<>" + cantidad.ToString();
                                    {
                                        if (factura == "")
                                        {
                                            string strSeqnFactura = " update tcseq set tcseq_numero=tcseq_numero+1 where tcseq_tseqn_clave='004' and tcseq_tcamp_clave='" +
                                                Global.campus.ToString() + "'";
                                            MySqlCommand myCommandupd1 = new MySqlCommand(strSeqnFactura, conexion);
                                            myCommandupd1.ExecuteNonQuery();

                                            //select lpad(tseqn_numero, tseqn_longitud,'0')
                                            string strSeqnfact1 = " select lpad(tcseq_numero, tcseq_longitud,'0') from tcseq where tcseq_tseqn_clave='004' and tcseq_tcamp_clave='" +
                                                Global.campus.ToString() + "'";
                                            DataSet dsfact = new DataSet();
                                            MySqlCommand commanfact = new MySqlCommand(strSeqnfact1, conexion);
                                            MySqlDataAdapter sqladapter = new MySqlDataAdapter();
                                            sqladapter.SelectCommand = commanfact;
                                            sqladapter.Fill(dsfact);
                                            sqladapter.Dispose();
                                            commanfact.Dispose();

                                            factura = "F" + dsfact.Tables[0].Rows[0][0].ToString();
                                            lblNumFact.Text = "Pago realizado, con folio " + factura;
                                        }
                                        if (cantidad > 0)
                                        {

                                            //resultado.Text = resultado.Text = "-->" + Pagos.Rows[i].Cells[2].Text.ToString() + "--" + Edcu.Rows[w].Cells[6].Text.ToString();
                                            fact_cons = fact_cons + 1;

                                            //if (Convert.ToDecimal(GridPagos.Rows[i].Cells[3].Text.ToString()) >= cantidad)
                                            if (Convert.ToDecimal(lblPag1.Text) >= cantidad)
                                            {
                                                StrActCartera = " insert into tpago values((select tpers_num from tpers where tpers_id='" + txt_matricula.Text + "'),'" +
                                                ddl_programa.SelectedValue + "','" + lblCvePago.Text + "'," +
                                                cantidad + "," + Convert.ToDouble(Gridtedcu.Rows[w].Cells[0].Text.ToString()) +
                                                "," + fact_cons + ",'A','" + factura + "','000','" + Session["usuario"].ToString() + "',current_timestamp())";
                                                //  StrActCartera = " insert into tpago values((select tpers_num from tpers where tpers_id='" + txt_matricula.Text + "'),'" +
                                                //ddl_programa.SelectedValue + "','" + GridPagos.Rows[i].Cells[2].Text.ToString() + "'," +
                                                //cantidad + "," + Convert.ToDouble(Gridtedcu.Rows[w].Cells[0].Text.ToString()) +
                                                //"," + fact_cons + ",'A','" + factura + "','000','" + Session["usuario"].ToString() + "',current_timestamp())";
                                                Gridtedcu.Rows[w].Cells[6].Text = "0";
                                                //resultado.Text = " >= 0 --" + StrActCartera;
                                            }
                                            else
                                            {
                                                StrActCartera = " insert into tpago values((select tpers_num from tpers where tpers_id='" + txt_matricula.Text + "'),'" +
                                                   ddl_programa.SelectedValue + "','" + lblCvePago.Text + "'," +
                                                   Convert.ToDecimal(GridPagos.Rows[i].Cells[3].Text.ToString()) + "," + Convert.ToDouble(Gridtedcu.Rows[w].Cells[0].Text.ToString()) +
                                                   "," + fact_cons + ",'A','" + factura + "','000','" + Session["usuario"].ToString() + "',current_timestamp())";
                                                //StrActCartera = " insert into tpago values((select tpers_num from tpers where tpers_id='" + txt_matricula.Text + "'),'" +
                                                //    ddl_programa.SelectedValue + "','" + GridPagos.Rows[i].Cells[1].Text.ToString() + "'," +
                                                //    Convert.ToDecimal(GridPagos.Rows[i].Cells[3].Text.ToString()) + "," + Convert.ToDouble(Gridtedcu.Rows[w].Cells[0].Text.ToString()) +
                                                //    "," + fact_cons + ",'A','" + factura + "','000','" + Session["usuario"].ToString() + "',current_timestamp())";
                                                pago = cantidad - Convert.ToDecimal(GridPagos.Rows[i].Cells[3].Text.ToString());
                                                importe.Text = pago.ToString(); //Edcu.Rows[w].Cells[6].Text 
                                                GridPagos.Rows[i].Cells[3].Text = "0";
                                                //resultado.Text = resultado.Text + "--" + StrActCartera + "--sobra w=" + pago;

                                            }




                                            MySqlCommand myCommandinserta = new MySqlCommand(StrActCartera, conexion);
                                            //Ejecucion del comando en el servidor de BD
                                            myCommandinserta.ExecuteNonQuery();
                                            //resultado.Text = resultado.Text + "inserta i -- w:" + i + "--" + w + ".. " + StrActCartera;
                                        }
                                    }
                                }

                            }


                            if (lblCveConcepto.Text == "SAFV")
                            {
                                //string pruebas = GridPagos.Rows[i].Cells[3].Text.ToString();

                                serviceCobro.EditarSaldoSFV(txt_matricula.Text, ddl_programa.SelectedValue, "SAFV", Convert.ToDecimal(lblPag1.Text), ddl_periodo.SelectedValue);
                                //serviceCobro.EditarSaldoSFV(txt_matricula.Text, ddl_programa.SelectedValue, "SAFV", Convert.ToDecimal(GridPagos.Rows[i].Cells[3].Text.ToString()));

                            }

                        }
                        btn_pagos_OFF();
                        btn_pagos_ON();
                        Carga_tedcu();

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "save", "save();", true);
                    }
                    catch (Exception ex)
                    {

                        string mensaje_error = ex.Message.Replace("'", "-");
                        Global.inserta_log(mensaje_error, "tedcu", Session["usuario"].ToString());
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                    }
                    conexion.Close();
                    rowPagoExitoso.Visible = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "paso3", "Paso3()", true);
                    //VerRecibo(Global.campus, txt_matricula.Text, factura);
                }
                //VerRecibo();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_gral('Debes agregar una forma de pago');", true);

            }
        }
        protected void linkRegresar_Click(object sender, EventArgs e)
        {
            lblTotalFinal.Text = "0";
            hddnGranTotal.Value = "0";
            rowPagoExitoso.Visible = false;
            btn_pagos_ON();
            btn_pagos_ON();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "paso1", "Paso1()", true);
        }
        protected void VerRecibo(string campus, string matricula, string consecutivo)
        {
            try
            {
                //string ruta = "../Reports/VisualizadorCrystal.aspx?Tipo=RepRecibo&Valor1=" + campus + "&Valor2=" + matricula + "&Valor3="+consecutivo;
                //string _open = "window.open('" + ruta + "', 'miniContenedor');";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), _open, true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "VerReciboPdf('" + Global.campus + "','" + txt_matricula.Text + "','" + consecutivo + "')", true);

            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tedcu", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                //Response.Redirect("Inicio.aspx");
            }
        }

        protected void ddl_programa_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            List<ModelObtenerConceptosResponse> lista = new List<ModelObtenerConceptosResponse>();
            //Session["lstSuma"] = null;
            //Session["lstTedcu"] = null;
            Session["lstTPagos"] = null;
            Session["lstTedcu"] = null;
            hddnSFV.Value = "";
            hddnGranTotal.Value = "";
            try
            {
                if (ddl_programa.SelectedValue != "")
                {
                    divBotones.Visible = true;
                    //dt = serviceCobro.ObtenerConceptos(Global.campus, ddl_programa.SelectedValue);
                    //lista = (from DataRow dr in dt.Rows
                    //         select new ModelObtenerConceptosResponse()
                    //         {
                    //             clave = dr["clave"].ToString(),
                    //             descripcion = dr["descripcion"].ToString(),
                    //             importe = dr["importe"].ToString()
                    //         }).ToList();



                    //Session["lstConceptos"] = lista;
                    ddl_conceptos.DataSource = serviceCobro.ObtenerConceptos(Global.campus, ddl_programa.SelectedValue);
                    ddl_conceptos.DataValueField = "Clave";
                    ddl_conceptos.DataTextField = "Descripcion";
                    ddl_conceptos.DataBind();

                    grid_bind_tedcu();

                }
                else
                {
                    divBotones.Visible = false;
                }
            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tedcu", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
            }
        }

        protected void ddl_periodo_concep_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["lstSuma"] = null;
            Session["lstTedcu"] = null;
            Session["lstTPagos"] = null;
            try
            {
                if (ddl_periodo_concep.SelectedValue != "")
                {
                    grid_bind_tedcu();
                    divBotones.Visible = true;
                }
                else
                    divBotones.Visible = false;


            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tedcu", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                //Response.Redirect("Inicio.aspx");
            }
        }

        protected void ddl_conceptos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ModelConceptoResponse objConcepto = new ModelConceptoResponse();

                objConcepto = serviceCatalogo.obtenImporteConcepto(ddl_programa.SelectedValue, Global.campus, ddl_conceptos.SelectedValue);
                //if (Session["lstConceptos"] != null)
                //{
                //    lst = (List<ModelObtenerConceptosResponse>)Session["lstConceptos"];
                //    txt_importe.Text = lst[ddl_conceptos.SelectedIndex].importe;
                //}
                txt_importe.Text = objConcepto.Importe;
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tedcu", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                //Response.Redirect("Inicio.aspx");
            }
        }

        protected void grid_bind_tedcu()
        {
            List<ModelObtenerDatosCobroResponse> lst = new List<ModelObtenerDatosCobroResponse>();
            List<ModelObtenerDatosCobroResponse> lst2 = new List<ModelObtenerDatosCobroResponse>();
            Session["lstTedcu"] = null;
            int tot;
            try
            {
                //Gridtedcu.Visible = true;
                Gridtedcu.DataSource = null;
                Gridtedcu.DataBind();
                lst = serviceCobro.ObtenerDatosTedcu(txt_matricula.Text, ddl_programa.SelectedValue, ddl_periodo.SelectedValue);
                Session["lstTedcu"] = lst;

                //lst2 = (List<ModelObtenerDatosCobroResponse>)lst.OrderBy(pagos => pagos.parcia);
                if (ddl_programa.SelectedValue != "")
                {
                    //btn_tedcu.Visible = true;
                    lst2 = lst.OrderBy(x => x.parcia).ToList();
                    tot = lst2.Count;
                    for (int i = 0; i < lst2.Count; i++)
                    {
                        if (i == (tot - 1)) //lst2[i].clave == "ILI")
                        {
                            if (lst2[i].clave == "PLI")
                                lst2[i].inhabil = false;
                            else
                                lst2[i].inhabil = true;
                        }

                        else if (i == 0 && lst2[i].clave == "PLI" && tot > 0)
                        {
                            lst2[i].inhabil = true;

                        }

                        else
                        {
                            if (lst2[i].importe == "0")
                            {
                                if (i > 0 && lst2[i].clave == "PLI")
                                {
                                    if (lst2[i - 1].importe == "0")
                                    {
                                        lst2[i].inhabil = false;
                                    }
                                    else
                                    {
                                        lst2[i].importe = string.Empty;
                                        lst2[i].inhabil = true;
                                    }
                                }
                                else
                                {
                                    lst2[i].importe = "0";
                                    lst2[i].inhabil = true;
                                }
                            }
                            else
                                lst2[i].inhabil = false;

                        }
                    }
                    Gridtedcu.DataSource = lst;
                    Gridtedcu.DataBind();
                    //btn_tedcu.Visible = true;
                }
                else
                {
                    //btn_tedcu.Visible = false;
                    Gridtedcu.DataSource = lst;
                    Gridtedcu.DataBind();
                    //Gridtedcu.ShowHeader = true;
                    //Gridtedcu.ShowFooter=true;
                }
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tedcu", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                //Response.Redirect("Inicio.aspx");
            }
        }



        protected void Gridtedcu_SelectedIndexChanged(object sender, EventArgs e)

        {

            string referencia = txt_matricula.Text + Gridtedcu.SelectedRow.Cells[0].Text;
            TextBox txtImporte = Gridtedcu.SelectedRow.Cells[7].FindControl("suma") as TextBox;
            try
            {
                string server = Server.MapPath("");
                string ruta = "../Reports/VisualizadorCrystal.aspx?Tipo=RepFichaBancaria&Valor1=" + txt_matricula.Text + "&Valor2=" + ddl_programa.SelectedValue + "&Valor3=" + referencia + "&Valor4=" + txtImporte.Text;
                string _open = "window.open('" + ruta + "', '_black');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), _open, true);
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tedcu", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                //Response.Redirect("Inicio.aspx");
            }
        }

        protected void Gridtedcu_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            List<ModelObtenerDatosCobroResponse> lst = new List<ModelObtenerDatosCobroResponse>();
            int indice = 0;
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    TextBox txt = e.Row.FindControl("suma") as TextBox;

                    if (Session["lstTedcu"] != null)
                        lst = (List<ModelObtenerDatosCobroResponse>)Session["lstTedcu"];


                    //indice = e.Row.RowIndex;
                    indice = Convert.ToInt32(e.Row.Cells[2].Text) + e.Row.RowIndex;
                    if (indice != 0)
                    {
                        indice = indice - 1;
                        if (indice != 0)
                        {
                            if (lst[indice].importe == string.Empty || lst[indice].importe == null)
                                txt.Enabled = false;
                            else
                                txt.Enabled = true;
                        }
                    }
                    else
                    {
                        txt.Enabled = true;

                    }


                    //int parcialidad=Convert.ToInt32(e.Row.Cells[2].Text);


                    //if (parcialidad == 0)
                    //else txt.Enabled=false;

                    //decimal total = 0;
                }
            }

            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tedcu", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void suma_Unload(object sender, EventArgs e)
        {
            try
            {
                TextBox cbi = (TextBox)(sender);
                GridViewRow row = (GridViewRow)cbi.NamingContainer;
                Gridtedcu.SelectedIndex = row.RowIndex;

                if (row.RowIndex > 1)
                {
                    //TextBox txt = Gridtedcu.Rows[row.RowIndex].Cells[7].FindControl("suma") as TextBox;
                    Label lbl = Gridtedcu.Rows[row.RowIndex].Cells[7].FindControl("lblReq") as Label;

                    if (cbi.Text == string.Empty)
                        lbl.Visible = true;

                }

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tedcu", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }

        }
        protected void linkBttnPagar_Click(object sender, EventArgs e)
        {
            hddnSFV.Value = "";
            rowPagoExitoso.Visible = false;
            List<ModelObtenerSFVAlumnoResponse> lstSFV = new List<ModelObtenerSFVAlumnoResponse>();
            try
            {
                lstSFV = serviceCobro.ObtenerSFVAlumno(txt_matricula.Text, ddl_programa.SelectedValue);
                if (lstSFV.Count > 0)
                {
                    if (lstSFV[0].saldo != string.Empty)
                        if (lstSFV[0].saldo != "0")
                            if (lstSFV[0].saldo != "")
                            {
                                hddnSFV.Value = lstSFV[0].saldo;
                                lblMsjSFV.Text = "El alumno tiene un saldo a favor de $" + lstSFV[0].saldo + ", ¿desea utilizarlo?";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "showConceptos", "$('#modalSFV').modal('show')", true);
                            }
                }
                else
                {
                    btn_pagos_ON();
                }
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tedcu", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
            }
        }
        protected void linkBttnPagosAplicados_Click(object sender, EventArgs e)
        {
            Global.cuenta = txt_matricula.Text;
            Global.nombre_alumno = txt_alumno.Text;
            Global.programa = ddl_programa.SelectedValue;
            Global.nombre_programa = ddl_programa.SelectedItem.ToString();
            Global.periodo = ddl_periodo.SelectedValue;

            //ImgPdf.Visible = false;
            Global.opcion = 1;
            Response.Redirect("Tcedc2.aspx");
        }

        protected void linkBttnGuardar_Click(object sender, EventArgs e)
        {
            ModelInstbaapResponse objExisteRegistro = new ModelInstbaapResponse();
            try
            {
                serviceCobro.InsertarTedcu(txt_matricula.Text, ddl_programa.SelectedValue, ddl_periodo_concep.SelectedValue, 0,
                    ddl_conceptos.SelectedValue, 0, Convert.ToDecimal(txt_importe.Text), "", Session["usuario"].ToString());

                ScriptManager.RegisterStartupScript(this, this.GetType(), "showConceptos", "$('#modalConceptos').modal('hide')", true);

                Carga_tedcu();
                //Gridtbapa = utils.BeginGrid(Gridtbapa, devoluciones.obtenParametrosDevolucion(ddl_periodo.SelectedValue, ddl_campus.SelectedValue, ddl_nivel.SelectedValue));
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "save", "save();", true);

            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tedcu", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
            }
        }

        protected void linkBttnAgregarCargo_Click(object sender, EventArgs e)
        {
            ddl_conceptos.SelectedIndex = 0;
            txt_importe.Text = string.Empty;
        }
        protected void linkBttnAgFormaPago_Click(object sender, EventArgs e)
        {
            List<ModelInsertarTpag1> lstPag1 = new List<ModelInsertarTpag1>();
            ModelInsertarTpag1 objPag1 = new ModelInsertarTpag1();

            try
            {
                if (Session["lstTPagos"] != null)
                    lstPag1 = (List<ModelInsertarTpag1>)Session["lstTPagos"];

                objPag1.tpag1_tpers_id = txt_matricula.Text;
                objPag1.tpag1_tprog_clave = ddl_programa.SelectedValue;
                objPag1.tpag1_tcoco_clave = ddl_forma_pago.SelectedValue;
                objPag1.tpag1_importe = Convert.ToDecimal(txt_importe_efect.Text);
                lstPag1.Add(objPag1);
                GridPagos.DataSource = lstPag1;
                GridPagos.DataBind();
                Session["lstTPagos"] = lstPag1;

                Label lblTot = (Label)GridPagos.FooterRow.FindControl("lblGranTotalFin");
                lblTot.Text = Convert.ToString(lstPag1.Sum(x => Convert.ToDecimal(x.tpag1_importe)));

                txt_importe_efect.Text = string.Empty;

                //serviceCobro.InsertarTpago1(txt_matricula.Text,ddl_programa.SelectedValue);
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tedcu", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
            }
        }

        protected void linkBttnAplicarSFV_Click(object sender, EventArgs e)
        {
            List<ModelInsertarTpag1> lstPag1 = new List<ModelInsertarTpag1>();
            ModelInsertarTpag1 objPag1 = new ModelInsertarTpag1();

            try
            {
                if (Session["lstTPagos"] != null)
                    lstPag1 = (List<ModelInsertarTpag1>)Session["lstTPagos"];

                objPag1.tpag1_tpers_id = txt_matricula.Text;
                objPag1.tpag1_tprog_clave = ddl_programa.SelectedValue;
                objPag1.tpag1_tcoco_clave = "SAFV";
                if (Convert.ToDecimal(hddnSFV.Value) > Convert.ToDecimal(hddnGranTotal.Value))
                    objPag1.tpag1_importe = Convert.ToDecimal(hddnGranTotal.Value);
                else
                    objPag1.tpag1_importe = Convert.ToDecimal(hddnSFV.Value);

                lstPag1.Add(objPag1);
                GridPagos.DataSource = lstPag1;
                GridPagos.DataBind();
                Session["lstTPagos"] = lstPag1;

                Label lblTot = (Label)GridPagos.FooterRow.FindControl("lblGranTotalFin");
                lblTot.Text = Convert.ToString(lstPag1.Sum(x => Convert.ToDecimal(x.tpag1_importe)));

                txt_importe_efect.Text = string.Empty;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showConceptos", "$('#modalSFV').modal('hide')", true);

                //serviceCobro.InsertarTpago1(txt_matricula.Text,ddl_programa.SelectedValue);
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tedcu", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
            }


            //ScriptManager.RegisterStartupScript(this, this.GetType(), "showConceptos", "$('#modalSFV').modal('hide')", true);
            //Aplicar_Pago();
        }

        protected void linkBttnCancelarSFV_Click(object sender, EventArgs e)
        {
            hddnSFV.Value = "";
        }

        protected void GridPagos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Label lblCvePago = (Label)GridPagos.SelectedRow.Cells[2].FindControl("lblCvePago");
                ddl_forma_pago.SelectedValue = lblCvePago.Text;
                Label lblImportePag1 = (Label)GridPagos.SelectedRow.Cells[4].FindControl("lblImportePag1");
                txt_importe_efect.Text = lblImportePag1.Text; // row.Cells[3].Text;           
                if (lblImportePag1.Text != string.Empty)
                    Global.importe_pago = Convert.ToDecimal(lblImportePag1.Text);

                linkBttnCancelarModificar.Visible = true;
                linkBttnModificar.Visible = true;
                linkBttnAgFormaPago.Visible = false;
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tedcu", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
            }
        }

        protected void linkBttnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                linkBttnCancelarModificar.Visible = false;
                linkBttnModificar.Visible = false;
                linkBttnAgFormaPago.Visible = true;
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tedcu", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
            }
        }

        protected void linkBttnCancelarModificar_Click(object sender, EventArgs e)
        {
            linkBttnCancelarModificar.Visible = false;
            linkBttnModificar.Visible = false;
            linkBttnAgFormaPago.Visible = true;
            GridPagos.SelectedIndex = -1;
        }

        protected void linkBttnComprobante_Click(object sender, EventArgs e)
        {
            try
            {
                Global.cuenta = txt_matricula.Text;
                Global.nombre_alumno = txt_alumno.Text;
                Global.programa = ddl_programa.SelectedValue;
                Global.nombre_programa = ddl_programa.SelectedItem.ToString();
                //ImgPdf.Visible = false;
                Global.opcion = 1;
                Response.Redirect("tpago.aspx");
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tedcu", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
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
            Global.nombre = HttpUtility.HtmlDecode(row.Cells[2].Text);
            Global.ap_paterno = HttpUtility.HtmlDecode(row.Cells[3].Text);
            Global.ap_materno = HttpUtility.HtmlDecode(row.Cells[4].Text);


            //Global.nombre_alumno = txt_alumno.Text;
            GridAlumnos.Visible = false;



            Carga_Programas();
            Carga_Periodos();

        }

        protected void Carga_Programas()
        {
            List<ModelObtenerProgsAlumnoResponse> lstdatosAlu = new List<ModelObtenerProgsAlumnoResponse>();
            try
            {
                lstdatosAlu = serviceAlumno.ObtenerProgramaAlumno2(txt_matricula.Text);
               // lstdatosAlu.Insert("0", "-------");
                //ddl_programa.Items.Add(new ListItem("-------", ""));

                ddl_programa.DataSource = lstdatosAlu;
                ddl_programa.DataValueField = "Clave";
                ddl_programa.DataTextField = "Descripcion";
                ddl_programa.DataBind();
                //ddl_programa.Items.Insert(0, new ListItem("Add New", ""));


                if (lstdatosAlu.Count > 0)
                {
                    Global.campus = lstdatosAlu[0].testu_tcamp_clave;
                    //ddl_programa_SelectedIndexChanged(null, null);
                }
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tedcu", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
            }
        }



        protected void linkBttnCancelar_Click(object sender, EventArgs e)
        {
            rowAlumnos.Visible = false;
            divBotones.Visible = false;
            //Gridtedcu.Visible=false;
            Gridtedcu.DataSource = null;
            Gridtedcu.DataBind();


            txt_matricula.Text = string.Empty;
            txt_matricula.ReadOnly = false;



            Session["lstSuma"] = null;
            Session["lstTedcu"] = null;
            Session["lstTPagos"] = null;
            txt_alumno.Text = string.Empty;
            ddl_programa.Items.Clear();
            ddl_periodo.Items.Clear();

            GridPagos.Visible = false;
        }

        protected void ddl_periodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            List<ModelObtenerConceptosResponse> lista = new List<ModelObtenerConceptosResponse>();
            Session["lstTPagos"] = null;
            Session["lstTedcu"] = null;
            hddnSFV.Value = "";
            hddnGranTotal.Value = "";
            try
            {
                if (ddl_programa.SelectedValue != "")
                {
                    divBotones.Visible = true;
                    ddl_conceptos.DataSource = serviceCobro.ObtenerConceptos(Global.campus, ddl_programa.SelectedValue);
                    ddl_conceptos.DataValueField = "Clave";
                    ddl_conceptos.DataTextField = "Descripcion";
                    ddl_conceptos.DataBind();

                    grid_bind_tedcu();

                }
                else
                {
                    divBotones.Visible = false;
                }
            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tedcu", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
            }
        }

        protected void GridPagos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int fila = e.RowIndex;

            List<ModelInsertarTpag1> lstPag1 = new List<ModelInsertarTpag1>();
            ModelInsertarTpag1 objPag1 = new ModelInsertarTpag1();

            try
            {
                if (Session["lstTPagos"] != null)
                    lstPag1 = (List<ModelInsertarTpag1>)Session["lstTPagos"];

                lstPag1.RemoveAt(fila);
                GridPagos.DataSource = lstPag1;
                GridPagos.DataBind();
                Session["lstTPagos"] = lstPag1;

                Label lblTot = (Label)GridPagos.FooterRow.FindControl("lblGranTotalFin");
                lblTot.Text = Convert.ToString(lstPag1.Sum(x => Convert.ToDecimal(x.tpag1_importe)));

                txt_importe_efect.Text = string.Empty;
            }
             catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tedcu", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
            }
        }
    }
}
