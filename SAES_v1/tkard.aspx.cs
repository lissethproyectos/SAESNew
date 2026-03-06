using SAES_Services;
using SAES_v1.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Drawing;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace SAES_v1
{
    public partial class tkard : System.Web.UI.Page
    {
        Utilidades utils = new Utilidades();
        Catalogos catalogos = new Catalogos();
        KardexAlumno kardexAlumno = new KardexAlumno();
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
                    CargaInicial();
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_alumnos", "load_datatable_alumnos();", true);
            }
        }

        public void CargaInicial(bool isupdate = false)
        {
            tbx_Comentario.Visible = false;
            btn_update.Visible = false;
            btn_save.Visible = false;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "cleartxt_matricula", "cleartxt_matricula('');", true);
            edit_periodo.Value = string.Empty;
            edit_materia.Value = string.Empty;
            edit_grupo.Value = string.Empty;

            ddl_programa = utils.BeginDropdownList(ddl_programa);

            Gridtkard_Captura.DataSource = null;
            Gridtkard_Captura.DataBind();

            Gridtkard.DataSource = null;
            Gridtkard.DataBind();

            gridtkarddet.DataSource = null;
            gridtkarddet.DataBind();

            GridSolicitudes.DataSource = null;
            GridSolicitudes.DataBind();

            htbx_ClaveAcredita.Value = "";
            htbx_ClaveCalificacion.Value = "";

            tbx_Comentario.Visible = false;
            lb_Comentario.Visible = false;

            txb_matricula.Text = Global.cuenta;
            txb_nombre.Text = Global.nombre;
            txb_apellido_p.Text = Global.ap_paterno;
            txb_apellido_m.Text = Global.ap_materno;

            if (!string.IsNullOrEmpty(txb_matricula.Text))
                findPersona();

            if (!isupdate)
                ScriptManager.RegisterStartupScript(this, this.GetType(), "generic_datatable_load", "generic_datatable_load('Gridtkard_Captura');", true);
        }

        public void PrepareGridCaptura()
        {
            int numeroPersona = Convert.ToInt32(hfd_numero_persona.Value);
            string programa = (ddl_programa.SelectedValue == "") ? null : ddl_programa.SelectedValue;
            Gridtkard_Captura = utils.BeginGridForInsert(Gridtkard_Captura, kardexAlumno.ObtenGridStructureForInsert(), 1);
            foreach (GridViewRow row in Gridtkard_Captura.Rows)
            {                
                var dropdownPeriodo = (DropDownList)row.FindControl("DDL_Periodo_Insert");
                dropdownPeriodo = utils.BeginDropdownList(dropdownPeriodo, catalogos.obtenTodoPeriodos());
                dropdownPeriodo.Enabled = true;

                var dropdownMateria = (DropDownList)row.FindControl("DDL_Materia_Insert");
                dropdownMateria.Enabled = true;
                var textBoxClaveMateria = (TextBox)row.FindControl("txb_Clave_Insert");
                DataTable dtMateria = catalogos.obtenMateriasNoAcreditadas(programa);
                if (dtMateria.Rows.Count > 0)
                {
                    dropdownMateria = utils.BeginDropdownList(dropdownMateria, dtMateria);
                    string descripcion = dtMateria.Rows[0]["Descripcion"].ToString();
                    textBoxClaveMateria.Text = (descripcion == "-- Seleccione --")?"":descripcion;
                }

                var dropdownCalificacion = (DropDownList)row.FindControl("DDL_Calificacion_Insert");
                dropdownCalificacion = utils.BeginDropdownList(dropdownCalificacion, catalogos.obtenCalificaciones(programa));

                var dropdownAcreditacion = (DropDownList)row.FindControl("DDL_Acreditacion_Insert");
                dropdownAcreditacion = utils.BeginDropdownList(dropdownAcreditacion, catalogos.obtenAcreditaciones());
            }
        }
        protected void grid_solicitudes_bind()
        {
            string QueryEstudiantes = "select distinct tpers_id clave, tpers_nombre nombre, tpers_paterno paterno, tpers_materno materno, tpers_genero c_genero, CASE WHEN tpers_genero = 'F' THEN 'Femenino' WHEN tpers_genero = 'M' THEN 'Masculino' ELSE 'No Aplica' END genero, " +
                                       "tpers_edo_civ c_civil, CASE WHEN tpers_edo_civ = 'C' THEN 'Casado' WHEN tpers_edo_civ = 'S' THEN 'Soltero' WHEN tpers_edo_civ = 'V' THEN 'Viudo' WHEN tpers_edo_civ = 'U' THEN 'Union Libre' WHEN tpers_edo_civ = 'D' THEN 'Divorciado' ELSE 'No Aplica' END e_civil, tpers_curp curp, date_format(tpers_fecha_nac, ' %d/%m/%Y') fecha, tpers_usuario usuario, fecha(date_format(tpers_date, '%Y-%m-%d')) fecha_reg " +
                                        "from tpers " +
                                         "where tpers_tipo = 'E'";
            MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            conexion.Open();
            try
            {
                MySqlDataAdapter dataadapter = new MySqlDataAdapter(QueryEstudiantes, conexion);
                DataSet ds = new DataSet();
                dataadapter.Fill(ds, "Solicitudes");
                GridSolicitudes.DataSource = ds;
                GridSolicitudes.DataBind();
                GridSolicitudes.DataMember = "Solicitudes";
                GridSolicitudes.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridSolicitudes.UseAccessibleHeader = true;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);
                GridSolicitudes.Visible = true;
            }
            catch (Exception ex)
            {
                //logs
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tpers", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
            conexion.Close();
        }
        protected void txb_matricula_OnTextChanged(object sender, EventArgs e)
        {
            try
            {
                grid_solicitudes_bind();
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tkard", Session["usuario"].ToString(), ex.StackTrace);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
            }
        }

        protected void GridSolicitudes_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridSolicitudes.SelectedRow;
            txb_matricula.Text = row.Cells[1].Text;
            txb_nombre.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);
            txb_apellido_p.Text = HttpUtility.HtmlDecode(row.Cells[3].Text);
            txb_apellido_m.Text = HttpUtility.HtmlDecode(row.Cells[4].Text);
            GridSolicitudes.Visible = false;
            btn_save.Visible = false;
            btn_update.Visible = true;
            findPersona();
            CargaInicial(true);
        }

        private void findPersona()
        {
            if (!string.IsNullOrEmpty(txb_matricula.Text.Trim()))
            {
                string matricula = txb_matricula.Text;
                if (!string.IsNullOrEmpty(matricula))
                {
                    DataTable dt = catalogos.obtenMatricula(matricula);
                    if (dt.Rows.Count == 1)
                    {
                        string numero_persona = dt.Rows[0]["NUMERO"].ToString();
                        hfd_numero_persona.Value = numero_persona;
                        string nombre = dt.Rows[0]["NOMBRE"].ToString();
                        string apellidop = dt.Rows[0]["ApellidoPaterno"].ToString();
                        string apellidom = dt.Rows[0]["ApellidoMaterno"].ToString();
                        Global.cuenta = matricula;
                        Global.nombre = nombre;
                        Global.ap_paterno = apellidop;
                        Global.ap_materno = apellidom;
                        txb_nombre.Text = nombre;
                        txb_apellido_p.Text = apellidop;
                        txb_apellido_m.Text = apellidom;
                        ddl_programa = utils.BeginDropdownList(ddl_programa, catalogos.obtenAlumnoPrograma(numero_persona));
                    }
                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_warning", "alert_warning('No se encontraron resultados: '" + matricula + ");", true);
                }
            }
        }

        protected void ddl_programa_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int numeroPersona = Convert.ToInt32(hfd_numero_persona.Value);
                string programa = (ddl_programa.SelectedValue == "") ? null : ddl_programa.SelectedValue;
                if (!string.IsNullOrEmpty(programa))
                {
                    PrepareGridCaptura();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "generic_datatable_load", "generic_datatable_load('Gridtkard_Captura');", true);
                    btn_save.Visible = true;
                    DataTable dt = kardexAlumno.ObtenKardexAlumno(numeroPersona, programa);
                    Gridtkard = utils.BeginGrid(Gridtkard, dt);
                    int i = 0;
                    foreach (GridViewRow row in Gridtkard.Rows)
                    {
                        if (dt.Rows[i]["VALOR_CONTAR"].ToString() == "0")
                        {
                            var imgb_ShowDetalle = (ImageButton)row.FindControl("Imgb_ShowDetalle");
                            //imgb_ShowDetalle.Visible = false;
                        }
                        i++;
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_Gridtkard", "load_datatable_Gridtkard()", true);
                }
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tkard", Session["usuario"].ToString(), ex.StackTrace);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
            }
        }

        protected void ddl_clave_insert_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddl = (DropDownList)sender;
                GridViewRow row = (GridViewRow)ddl.Parent.Parent;
                if (!string.IsNullOrEmpty(ddl.SelectedValue))
                {
                    var textBoxMateria = (TextBox)row.FindControl("txb_Clave_Insert");
                    textBoxMateria.Text = ddl.SelectedValue;
                }
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tkard", Session["usuario"].ToString(), ex.StackTrace);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
            }
        }

        protected void GetDetailAuditoria_click(object sender, EventArgs e)
        {            
            try
            {
                ImageButton ddl = (ImageButton)sender;
                GridViewRow row = (GridViewRow)ddl.Parent.Parent;
                                
                int NumroPersona = Convert.ToInt32(hfd_numero_persona.Value);
                string ClavePrograma = (ddl_programa.SelectedValue == "") ? null : ddl_programa.SelectedValue;
                string ClavePeriodo = row.Cells[1].Text;
                string ClaveMateria = row.Cells[2].Text;

                foreach (GridViewRow rowg in Gridtkard.Rows)                
                    rowg.BackColor = Color.White;                

                if ((htbx_ClavePeriodo.Value == "" && htbx_ClaveMateria.Value == "") || (ClavePeriodo != htbx_ClavePeriodo.Value && ClaveMateria != htbx_ClaveMateria.Value))
                {
                    htbx_ClavePeriodo.Value = ClavePeriodo;
                    htbx_ClaveMateria.Value = ClaveMateria;

                    DataTable dt = kardexAlumno.ObtenBitacoraKardex(NumroPersona, ClavePrograma, ClavePeriodo, ClaveMateria);
                    if (dt.Rows.Count > 0)
                        gridtkarddet = utils.BeginGrid(gridtkarddet, dt);
                    else
                    {
                        gridtkarddet.DataSource = null;
                        gridtkarddet.DataBind();
                    }
                    row.BackColor = Color.LightGreen;
                    gridtkarddet.HeaderStyle.BackColor = Color.Red;
                }
                else
                {
                    htbx_ClavePeriodo.Value = "";
                    htbx_ClaveMateria.Value = "";
                    gridtkarddet.DataSource = null;
                    gridtkarddet.DataBind();
                    row.BackColor = Color.White;
                }     

                ScriptManager.RegisterStartupScript(this, this.GetType(), "generic_datatable_load", "generic_datatable_load('gridtkarddet');", true);
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tkard", Session["usuario"].ToString(), ex.StackTrace);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            try
            {
                CargaInicial(true);
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tkard", Session["usuario"].ToString(), ex.StackTrace);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
            }
        }
        protected void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                string usuario = Session["usuario"].ToString();
                int numeroPersona = Convert.ToInt32(hfd_numero_persona.Value);
                string programa = (ddl_programa.SelectedValue == "") ? null : ddl_programa.SelectedValue;
                GridViewRow rowGrid = Gridtkard_Captura.Rows[0];
                var dropdownPeriodo = (DropDownList)rowGrid.FindControl("DDL_Periodo_Insert");
                string periodo = dropdownPeriodo.SelectedValue;
                var dropdownClaveMateria = (DropDownList)rowGrid.FindControl("DDL_Materia_Insert");
                string materia = dropdownClaveMateria.SelectedValue;
                var textBoxGrupo = (TextBox)rowGrid.FindControl("txb_Grupo_Insert");
                string grupo = textBoxGrupo.Text;
                var dropdownAcreditacion = (DropDownList)rowGrid.FindControl("DDL_Acreditacion_Insert");
                string acreditacion = dropdownAcreditacion.SelectedValue;
                var dropdownCalificacion = (DropDownList)rowGrid.FindControl("DDL_Calificacion_Insert");
                string calificacion = dropdownCalificacion.SelectedValue;
                kardexAlumno.InsertKardexAlumno(numeroPersona, programa, periodo, materia, grupo, acreditacion, calificacion, usuario);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_success", "alert_success('Se guardaron los datos exitosamente');", true);
                CargaInicial(true);
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tkard", Session["usuario"].ToString(), ex.StackTrace);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
            }
        }
        protected void btn_update_Click(object sender, EventArgs e)
        {
            try
            {
                tbx_Comentario.Visible = true;
                btn_save.Visible = false;
                string usuario = Session["usuario"].ToString();
                int numeroPersona = Convert.ToInt32(hfd_numero_persona.Value);
                string programa = (ddl_programa.SelectedValue == "") ? null : ddl_programa.SelectedValue;
                GridViewRow rowGrid = Gridtkard_Captura.Rows[0];
                var dropdownPeriodo = (DropDownList)rowGrid.FindControl("DDL_Periodo_Insert");
                string periodo = dropdownPeriodo.SelectedValue;
                var dropdownClaveMateria = (DropDownList)rowGrid.FindControl("DDL_Materia_Insert");
                string materia = dropdownClaveMateria.SelectedValue;
                var textBoxGrupo = (TextBox)rowGrid.FindControl("txb_Grupo_Insert");
                string grupo = textBoxGrupo.Text;

                var dropdownAcreditacion = (DropDownList)rowGrid.FindControl("DDL_Acreditacion_Insert");
                string acreditacion_NEW = dropdownAcreditacion.SelectedValue;
                string acreditacion_OLD = htbx_ClaveAcredita.Value;

                var dropdownCalificacion = (DropDownList)rowGrid.FindControl("DDL_Calificacion_Insert");
                string calificacion_NEW = dropdownCalificacion.SelectedValue;
                string calificacion_OLD = htbx_ClaveCalificacion.Value;

                string comentario = tbx_Comentario.Text;

                string periodo_original = edit_periodo.Value;
                string materia_original = edit_materia.Value;
                string grupo_original = edit_grupo.Value;

                kardexAlumno.UpdateKardexAlumno(numeroPersona, programa, periodo_original, periodo, materia_original, materia, grupo_original, grupo, acreditacion_OLD, acreditacion_NEW, calificacion_OLD , calificacion_NEW, comentario, usuario);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_success", "alert_success('Se actualizaron los datos exitosamente');", true);
                CargaInicial(true);
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tkard", Session["usuario"].ToString(), ex.StackTrace);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
            }
        }

        protected void Gridtkard_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                GridViewRow row = Gridtkard.SelectedRow;
                string periodo = row.Cells[1].Text;
                string claveMateria = row.Cells[2].Text;
                string grupo = row.Cells[4].Text;
                string calificacion = row.Cells[5].Text;
                string Acreditacion = row.Cells[6].Text;

                btn_update.Visible = true;
                btn_save.Visible = false;


                GridViewRow rowGrid = Gridtkard_Captura.Rows[0];

                //Problema con el periodo no lo encuentra en los items del ddl

                var dropdownPeriodo = (DropDownList)rowGrid.FindControl("DDL_Periodo_Insert");
                dropdownPeriodo.SelectedValue = periodo;
                dropdownPeriodo.Enabled = false;

                edit_periodo.Value = periodo;

                var dropdownClaveMateria = (DropDownList)rowGrid.FindControl("DDL_Materia_Insert");
                dropdownClaveMateria.SelectedValue = claveMateria;
                dropdownClaveMateria.Enabled = false;
                var textBoxMateria = (TextBox)rowGrid.FindControl("txb_Clave_Insert");
                textBoxMateria.Text = claveMateria;

                edit_materia.Value = claveMateria;

                var textBoxGrupo = (TextBox)rowGrid.FindControl("txb_Grupo_Insert");
                textBoxGrupo.Text = grupo;

                edit_grupo.Value = grupo;

                var dropdownCalificacion = (DropDownList)rowGrid.FindControl("DDL_Calificacion_Insert");
                string califformat = string.Format("{0:0.00}", Convert.ToDecimal(calificacion));
                dropdownCalificacion.SelectedValue = califformat;
                htbx_ClaveCalificacion.Value = califformat;

                var dropdownAcreditacion = (DropDownList)rowGrid.FindControl("DDL_Acreditacion_Insert");
                dropdownAcreditacion.SelectedValue = Acreditacion;
                htbx_ClaveAcredita.Value = Acreditacion;

                lb_Comentario.Visible = true;
                tbx_Comentario.Visible = true;                

            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tbapa", Session["usuario"].ToString(), ex.StackTrace);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static List<Alumno> GetCompanyName(string search)
        {
            List<Alumno> allAlumns = new List<Alumno>();
            Catalogos catalogos = new Catalogos();
            DataTable dt = catalogos.obtenMatricula(search);
            foreach (DataRow item in dt.Rows)
            {
                Alumno alumno = new Alumno() { Nombre = item["Nombre"].ToString(), Matricula = item["Matricula"].ToString(), Numero = item["Numero"].ToString() };
                allAlumns.Add(alumno);
            }
            return allAlumns;
        }

        protected void linkBttnBusca_Click(object sender, EventArgs e)
        {
            try
            {
                grid_solicitudes_bind();
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tkard", Session["usuario"].ToString(), ex.StackTrace);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
            }
        }

        protected void linkBttnVer_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton ddl = (LinkButton)sender;
                GridViewRow row = (GridViewRow)ddl.Parent.Parent;

                int NumroPersona = Convert.ToInt32(hfd_numero_persona.Value);
                string ClavePrograma = (ddl_programa.SelectedValue == "") ? null : ddl_programa.SelectedValue;
                string ClavePeriodo = row.Cells[1].Text;
                string ClaveMateria = row.Cells[2].Text;

                foreach (GridViewRow rowg in Gridtkard.Rows)
                    rowg.BackColor = Color.White;

                if ((htbx_ClavePeriodo.Value == "" && htbx_ClaveMateria.Value == "") || (ClavePeriodo != htbx_ClavePeriodo.Value && ClaveMateria != htbx_ClaveMateria.Value))
                {
                    htbx_ClavePeriodo.Value = ClavePeriodo;
                    htbx_ClaveMateria.Value = ClaveMateria;

                    DataTable dt = kardexAlumno.ObtenBitacoraKardex(NumroPersona, ClavePrograma, ClavePeriodo, ClaveMateria);
                    if (dt.Rows.Count > 0)
                        gridtkarddet = utils.BeginGrid(gridtkarddet, dt);
                    else
                    {
                        gridtkarddet.DataSource = null;
                        gridtkarddet.DataBind();
                    }
                    row.BackColor = Color.LightGreen;
                    gridtkarddet.HeaderStyle.BackColor = Color.Red;
                }
                else
                {
                    htbx_ClavePeriodo.Value = "";
                    htbx_ClaveMateria.Value = "";
                    gridtkarddet.DataSource = null;
                    gridtkarddet.DataBind();
                    row.BackColor = Color.White;
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "generic_datatable_load", "generic_datatable_load('gridtkarddet');", true);
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tkard", Session["usuario"].ToString(), ex.StackTrace);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
            }
        }
    }

    public class Alumno
    {
        public string Matricula { get; set; }
        public string Nombre { get; set; }
        public string Numero { get; set; }
    }
}