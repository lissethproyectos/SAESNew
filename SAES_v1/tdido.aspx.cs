using MySql.Data.MySqlClient;
using SAES_DBO.Models;
using SAES_Services;
using SAES_v1.Utils;
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
using static SAES_DBO.Models.ModelDocente;
using static SAES_DBO.Models.ModelMenu;

namespace SAES_v1
{
    public partial class tdido : System.Web.UI.Page
    {
        #region <Variables>
        Utilidades utils = new Utilidades();
        Catalogos serviceCatalogo = new Catalogos();
        List<ModelObtenPaisesResponse> lstPaises = new List<ModelObtenPaisesResponse>();
        MenuService servicePermiso = new MenuService();
        DocenteService serviceDocente = new DocenteService();
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
                    txt_matricula.Text = Global.clave_docente;
                    txt_nombre.Text = Global.nombre_docente + " " + Global.ap_paterno_docente + " " + Global.ap_materno_docente;
                    LlenaPagina();
                    combo_estatus();
                    combo_dia();
                    if (txt_matricula.Text != "")
                    {
                        Carga_Disponibilidad();
                    }
                    //grid_correo_bind(txt_matricula.Text);
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_Docentes", "load_datatable_Docentes();", true);
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


        private void LlenaPagina()
        {
            ModelObtenerPermisoFormResponse objPermiso = new ModelObtenerPermisoFormResponse();
            objPermiso.usme_update = "0";
            objPermiso.usme_select = "0";
            System.Threading.Thread.Sleep(50);
            try
            {
                objPermiso = servicePermiso.ObtenerPermisoFormulario(Session["usuario"].ToString(), "tpais");
                if (objPermiso != null)
                {
                    if (objPermiso.usme_update == "0" || objPermiso.usme_select == "0")
                    {
                        btn_tdido.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                    }
                    else
                    {
                        combo_dia();
                        combo_estatus();
                    }
                }
                else
                {
                    btn_tdido.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                }


            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tpais", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);

            }
        }

        protected void combo_estatus()
        {
            ddl_estatus.Items.Clear();
            ddl_estatus.Items.Add(new ListItem("-----", "0"));
            ddl_estatus.Items.Add(new ListItem("Activo", "A"));
            ddl_estatus.Items.Add(new ListItem("Inactivo", "B"));
        }

        protected void combo_dia()
        {

            try
            {


                ddl_dia.DataSource = serviceCatalogo.ObtenerDias();
                ddl_dia.DataValueField = "Clave";
                ddl_dia.DataTextField = "Descripcion";
                ddl_dia.DataBind();



                ddl_hini.DataSource = serviceCatalogo.ObtenerHoraInicial();
                ddl_hini.DataValueField = "Clave";
                ddl_hini.DataTextField = "Descripcion";
                ddl_hini.DataBind();
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tdido", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }

        }

       



        protected void txt_matricula_TextChanged(object sender, EventArgs e)
        {
            Carga_Docente();
        }

        protected void GridDocentes_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridDocentes.SelectedRow;
            txt_matricula.Text = row.Cells[1].Text;
            txt_nombre.Text = HttpUtility.HtmlDecode(row.Cells[2].Text) + " " + HttpUtility.HtmlDecode(row.Cells[3].Text) + " " +
             HttpUtility.HtmlDecode(row.Cells[4].Text);
            //GridDocentes.Visible = false;
            txt_matricula.Attributes.Add("readonly", "");
            Global.clave_docente = txt_matricula.Text;
            Global.nombre_docente = HttpUtility.HtmlDecode(row.Cells[2].Text);
            Global.ap_paterno_docente = HttpUtility.HtmlDecode(row.Cells[3].Text);
            Global.ap_materno_docente = HttpUtility.HtmlDecode(row.Cells[4].Text);
            //Carga_Docente();
            GridDocentes.Visible = false;
            GridDisponibilidad.Visible = true;
            Carga_Disponibilidad();
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "collapse", "$('#collapseGrid').collapse('toggle')", true);
        }


        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            txt_matricula.ReadOnly = false;
            txt_matricula.Text = "";
            txt_nombre.Text = "";
            combo_dia();
            combo_estatus();
        }

        protected void Agregar_Click(object sender, EventArgs e)
        {
            ModelInsertarDisponibilidadResponse objExiste = new ModelInsertarDisponibilidadResponse();

            try
            {
                objExiste = serviceDocente.InsertarDisponibilidad(txt_matricula.Text, ddl_dia.SelectedValue, ddl_hini.SelectedValue, ddl_hfin.SelectedValue, Session["usuario"].ToString(), ddl_estatus.SelectedValue);
                if (objExiste.Existe != "0")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Traslape", "Traslape();", true);
                    //Carga_Disponibilidad();
                }
                else
                {
                    Global.hinicio_docente = "";
                    Global.hfin_docente = "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                    combo_estatus();
                    combo_dia();
                    ddl_hfin.Items.Clear();
                    Carga_Disponibilidad();
                }

            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tdido", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }

            Global.clave_docente = txt_matricula.Text;
        }

        protected void Carga_Disponibilidad(object sender, EventArgs e)
        {
            Carga_Disponibilidad();
        }

        private void Carga_Disponibilidad()
        {

            try
            {

                GridDisponibilidad.Visible = true;
                GridDisponibilidad.DataSource = serviceDocente.ObtenerDisponibilidad(txt_matricula.Text);
                GridDisponibilidad.DataBind();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_Disponibilidad", "load_datatable_Disponibilidad();", true);

            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tdido", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
            txt_matricula.Focus();
        }

        protected void GridDisponibilidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridDisponibilidad.SelectedRow;

            combo_dia();
            ddl_dia.SelectedValue = row.Cells[7].Text.ToString();
            ddl_hini.SelectedValue = row.Cells[5].Text.ToString();
            ddl_hini_SelectedIndexChanged(null, null);
            //Carga_Hora_fin();
            ddl_hfin.SelectedValue = row.Cells[6].Text.ToString();
            ddl_estatus.SelectedValue = row.Cells[4].Text.ToString();
            Global.hinicio_docente = row.Cells[5].Text.ToString();
            Global.hfin_docente = row.Cells[6].Text.ToString();
            Global.clave_docente = txt_matricula.Text;
            //Carga_Disponibilidad();
            btn_save.Visible = false;
            btn_update.Visible = true;
            btn_cancel.Visible = false;
            btn_cancel_update.Visible = true;
        }

        //protected void Carga_Hora_fin()
        //{
        //    ddl_hfin.DataSource = serviceCatalogo.ObtenerHoraFinal(ddl_hini.SelectedValue);
        //    ddl_hfin.DataBind();
        //}

        protected void linkBttnBusca_Click(object sender, EventArgs e)
        {
            //string QueryEstudiantes = "select distinct tpers_id clave, tpers_nombre nombre, tpers_paterno paterno, tpers_materno materno, tpers_num pidm,  tpers_genero c_genero, CASE WHEN tpers_genero = 'F' THEN 'Femenino' WHEN tpers_genero = 'M' THEN 'Masculino' ELSE 'No Aplica' END genero, " +
            //                              "tpers_edo_civ c_civil, CASE WHEN tpers_edo_civ = 'C' THEN 'Casado' WHEN tpers_edo_civ = 'S' THEN 'Soltero' WHEN tpers_edo_civ = 'V' THEN 'Viudo' WHEN tpers_edo_civ = 'U' THEN 'Union Libre' WHEN tpers_edo_civ = 'D' THEN 'Divorciado' ELSE 'No Aplica' END e_civil, tpers_curp curp, date_format(tpers_fecha_nac, ' %d/%m/%Y') fecha, tpers_usuario usuario, fecha(date_format(tpers_date, '%Y-%m-%d')) fecha_reg " +
            //                               "from tpers " +
            //                                "where tpers_tipo = 'D'";

            try
            {
                Carga_Docente();
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tdido", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void Carga_Docente()
        {
            List<ModelObtenerDocentesResponse> lst = new List<ModelObtenerDocentesResponse>();

            try
            {
                lst = serviceDocente.ListaDocentes(txt_matricula.Text);
                if (lst.Count() == 1)
                    txt_nombre.Text = lst[0].nombre + " " + lst[0].paterno + "" + lst[0].materno;
                else if (lst.Count() > 1)
                {
                    GridDocentes.DataSource = lst;
                    GridDocentes.DataBind();
                    GridDocentes.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridDocentes.UseAccessibleHeader = true;
                    GridDocentes.Visible = true;
                }
                else
                {
                    GridDocentes.DataSource = lst;
                    GridDocentes.DataBind();
                    GridDocentes.Visible = true;
                }

                //Carga_Disponibilidad();
                btn_cancel.Visible = true;

            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tdido", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void btn_cancel_update_Click(object sender, EventArgs e)
        {
            btn_cancel.Visible = true;
            btn_cancel_update.Visible = false;
            btn_update.Visible = false;
            btn_save.Visible = true;
            ddl_dia.SelectedIndex = 0;
            ddl_hini.SelectedIndex = 0;
            ddl_hfin.SelectedIndex = 0;
            ddl_estatus.SelectedIndex = 0;
            GridDisponibilidad.SelectedIndex = -1;
        }

        protected void btn_update_Click(object sender, EventArgs e)
        {
            //string strCborraSQL = " delete from tdido where tdido_tpers_num=" + pidm + " and tdido_tdias_clave='" +
            //           ddl_dia.SelectedValue.ToString() + "' and tdido_inicio='" + Global.hinicio_docente.ToString() + "' " +
            //           " and tdido_fin='" + Global.hfin_docente.ToString() + "'";

            //string stQuerytraslape = " select count(*) from tdido " +
            //         " where tdido_tpers_num =" + pidm +
            //         " and tdido_tdias_clave =" + ddl_dia.SelectedValue.ToString() + "" +
            //         " and((" + ddl_hini.SelectedValue.ToString() + " >= tdido_inicio and " +
            //         ddl_hini.SelectedValue.ToString() + " <= tdido_fin) or " +
            //         " (" + ddl_hfin.SelectedValue.ToString() + " >= tdido_inicio and
            //         " + ddl_hfin.SelectedValue.ToString() + " <= tdido_fin)) ";

            ModelInsertarDisponibilidadResponse objExiste = new ModelInsertarDisponibilidadResponse();
            try
            {
              
                serviceDocente.EliminarDisponibilidad(txt_matricula.Text, ddl_dia.SelectedValue.ToString(),
                        GridDisponibilidad.SelectedRow.Cells[5].Text, GridDisponibilidad.SelectedRow.Cells[6].Text, 
                        Session["usuario"].ToString(), GridDisponibilidad.SelectedRow.Cells[4].Text);

                    objExiste = serviceDocente.InsertarDisponibilidad(txt_matricula.Text, ddl_dia.SelectedValue, ddl_hini.SelectedValue, ddl_hfin.SelectedValue, Session["usuario"].ToString(), ddl_estatus.SelectedValue);
                    if (objExiste.Existe != "0")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Traslape", "Traslape();", true);
                        //Carga_Disponibilidad();
                    }
                    else
                    {
                        Global.hinicio_docente = "";
                        Global.hfin_docente = "";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                        ddl_dia.SelectedIndex = 0;
                        ddl_estatus.SelectedIndex = 0;
                        ddl_hini.SelectedIndex = 0;
                        ddl_hfin.Items.Clear();
                        Carga_Disponibilidad();
                    }


                    //if (dstras.Tables[0].Rows[0][0].ToString() != "0")
                    //{
                    //    string strCadSQL = "INSERT INTO tdido Values (" + pidm + "," + ddl_dia.SelectedValue.ToString() + "," + Global.hinicio_docente.ToString() + "," +
                    //       Global.hfin_docente.ToString() + ",current_timestamp(),'" + Session["usuario"].ToString() + "','" + ddl_estatus.SelectedValue + "')";
                    //    MySqlCommand myCommandinserta = new MySqlCommand(strCadSQL, conexion);
                    //    myCommandinserta.ExecuteNonQuery();
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Traslape", "Traslape();", true);
                    //    Carga_Disponibilidad();
                    //}
                    //else
                    //{
                    //    string strCadSQL = "INSERT INTO tdido Values (" + pidm + "," + ddl_dia.SelectedValue.ToString() + "," + ddl_hini.SelectedValue.ToString() + "," +
                    //       ddl_hfin.SelectedValue.ToString() + ",current_timestamp(),'" + Session["usuario"].ToString() + "','" + ddl_estatus.SelectedValue + "')";
                    //    myCommandinserta.ExecuteNonQuery();
                    //    Global.hinicio_docente = "";
                    //    Global.hfin_docente = "";
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                    //    Carga_Disponibilidad();
                    //    combo_estatus();
                    //    combo_dia();
                    //    ddl_hfin.Items.Clear();

                    //}


                
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tdido", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void ddl_hini_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddl_hfin.DataSource = serviceCatalogo.ObtenerHoraFinal(ddl_hini.SelectedValue);
            ddl_hfin.DataValueField = "Clave";
            ddl_hfin.DataTextField = "Descripcion";
            ddl_hfin.DataBind();
        }

    }
}