using MySql.Data.MySqlClient;
using SAES_Services;
using SAES_v1.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SAES_v1
{
    public partial class ttiel : System.Web.UI.Page
    {
        Utilidades utils = new Utilidades();
        Catalogos catalogos = new Catalogos();
        TitulacionElectronica model = new TitulacionElectronica();
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
            }
        }

        private void CargaInicial()
        {
            txb_matricula.Text = string.Empty;
            lb_nombre.Text = string.Empty;
            lb_apellidoPaterno.Text = string.Empty;
            lb_apellidoMaterno.Text = string.Empty;
            lb_CURP.Text = string.Empty;

            ddl_programa = utils.BeginDropdownList(ddl_programa);
            ddl_reconocimiento = utils.BeginDropdownList(ddl_reconocimiento, catalogos.obtenAutorizacion_ttiel());
            ddl_modalidadTitulacion = utils.BeginDropdownList(ddl_modalidadTitulacion, catalogos.obtenModalidadTitulacion_ttiel());
            ddl_servicioSocial = utils.BeginDropdownList(ddl_servicioSocial, catalogos.obtenServicioSocial_ttiel());
            ddl_estudioAntecedente = utils.BeginDropdownList(ddl_estudioAntecedente, catalogos.obtenEstudiosAntecedentes_ttiel());
            lb_campus.Text = string.Empty;
            lb_abrebCampus.Text = string.Empty;
            lb_campusClave.Text = string.Empty;
            lb_claveIncorporante.Text = string.Empty;
            lb_nombreLegal.Text = string.Empty;
            lb_RVOE.Text = string.Empty;

            lb_nombreFuncionario.Text = string.Empty;
            lb_apellidoPFuncionario.Text = string.Empty;
            lb_apellidoMFuncionario.Text = string.Empty;
            lb_CURPFuncionario.Text = string.Empty;
            lb_CargoFuncionario.Text = string.Empty;

            hfd_numero_persona.Value = "";
            btn_cancel.Visible = false;
            btn_enviarCertificado.Visible = false;
            btn_generarConfirmarCertificado.Visible = false;
            //btn_generarCertificado.Visible = false;
            txb_Respuesta.Visible = false;
            txb_Respuesta.Text = "";
            hdf_IdCampus.Value = "";
            lb_fechaInicio.Text = "";
            lb_fechaTermino.Text = "";
            hdf_idCargoFuncionario.Value = "";
            hdf_correo.Value = "";
            hdf_IdEntidadFederativaCampus.Value = "";
            hdf_EntidadFederativaCampus.Value = "";
            relaodComponents();
        }

        private void relaodComponents()
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ctrl_fecha_expedicion", "ctrl_fecha_expedicion();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ctrl_fecha_examen_profecional", "ctrl_fecha_examen_profecional();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ctrl_fecha_terminacion", "ctrl_fecha_terminacion();", true);
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
                Global.inserta_log(mensaje_error, "tceel", Session["usuario"].ToString(), ex.StackTrace);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
            }
        }

        protected void ddl_programa_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                relaodComponents();
                btn_cancel.Visible = true;
                btn_generarConfirmarCertificado.Visible = true;
                string matricula = txb_matricula.Text;
                string programa = ddl_programa.SelectedValue;

                DataTable dt = model.obtenDatosAlumnoTCEEL(matricula, programa);
                if (dt != null && dt.Rows.Count > 0)
                {
                    hdf_correo.Value = dt.Rows[0]["CorreoPersona"].ToString();
                    lb_campusClave.Text = dt.Rows[0]["ClaveInstitucion"].ToString();
                    lb_abrebCampus.Text = dt.Rows[0]["AbrebInstitucion"].ToString();
                    lb_campus.Text = dt.Rows[0]["NombreInstitucionReal"].ToString();
                    lb_claveIncorporante.Text = dt.Rows[0]["ClaveCarrera"].ToString();
                    lb_nombreLegal.Text = dt.Rows[0]["NombreCarrera"].ToString();
                    lb_RVOE.Text = dt.Rows[0]["RVOE"].ToString();
                    lb_fechaInicio.Text = dt.Rows[0]["FechaInicio"].ToString();
                    lb_fechaTermino.Text = dt.Rows[0]["FechaTerminacion"].ToString();

                    lb_nombreFuncionario.Text = dt.Rows[0]["NombreFuncionario"].ToString();
                    lb_apellidoPFuncionario.Text = dt.Rows[0]["PrimerApellidoFuncionario"].ToString();
                    lb_apellidoMFuncionario.Text = dt.Rows[0]["SegundoApellidoFuncionario"].ToString();
                    lb_CURPFuncionario.Text = dt.Rows[0]["CurpFuncionario"].ToString();
                    lb_CargoFuncionario.Text = dt.Rows[0]["DescripcionCargo"].ToString();
                    hdf_idCargoFuncionario.Value = dt.Rows[0]["IdCargo"].ToString();
                    hdf_IdEntidadFederativaCampus.Value = dt.Rows[0]["IdEntidadFederativa"].ToString();
                    hdf_EntidadFederativaCampus.Value = dt.Rows[0]["EntidadFederativa"].ToString();
                }

                DataTable dtEscuelaProcedencia = model.obtenEscuelaProcedenciaTCEEL(matricula, programa);
                if (dtEscuelaProcedencia != null && dtEscuelaProcedencia.Rows.Count > 0)
                {
                    lb_institutoProcedencia.Text = dtEscuelaProcedencia.Rows[0]["NombreEscuelaProc"].ToString();
                    hdf_IdEntidadFederativa.Value = dtEscuelaProcedencia.Rows[0]["IdEntidadFederativaEscuelaProc"].ToString();
                    hdf_EntidadFederativa.Value = dtEscuelaProcedencia.Rows[0]["DescripcionEntidadFederativaEscuelaProc"].ToString();
                }
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tceel", Session["usuario"].ToString(), ex.StackTrace);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
            }
        }

        protected void GridSolicitudes_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridSolicitudes.SelectedRow;
            DataTable Catalogo = catalogos.obtenAlumnoTCEEL(row.Cells[1].Text);
            if (Catalogo != null && Catalogo.Rows.Count > 0)
            {
                txb_matricula.Text = row.Cells[1].Text;
                hfd_numero_persona.Value = row.Cells[1].Text;
                lb_nombre.Text = Catalogo.Rows[0]["Nombre"].ToString();
                lb_apellidoPaterno.Text = Catalogo.Rows[0]["PrimerApellido"].ToString();
                lb_apellidoMaterno.Text = Catalogo.Rows[0]["SegundoApellido"].ToString();
                lb_CURP.Text = Catalogo.Rows[0]["CURP"].ToString();
            }
            GridSolicitudes.Visible = false;
            findPersona();
        }

        private void findPersona()
        {
            if (!string.IsNullOrEmpty(txb_matricula.Text.Trim()))
            {
                string matricula = txb_matricula.Text;
                DataTable dt = catalogos.obtenMatricula(matricula);
                if (dt.Rows.Count == 1)
                {
                    string numero_persona = dt.Rows[0]["NUMERO"].ToString();
                    string nombre = dt.Rows[0]["NOMBRE"].ToString();
                    string apellidop = dt.Rows[0]["ApellidoPaterno"].ToString();
                    string apellidom = dt.Rows[0]["ApellidoMaterno"].ToString();
                    Global.cuenta = matricula;
                    Global.nombre = nombre;
                    Global.ap_paterno = apellidop;
                    Global.ap_materno = apellidom;
                    ddl_programa = utils.BeginDropdownList(ddl_programa, catalogos.obtenAlumnoPrograma(numero_persona));
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_warning", "alert_warning('No se encontraron resultados: '" + matricula + ");", true);
            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            try
            {
                CargaInicial();
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tceel", Session["usuario"].ToString(), ex.StackTrace);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
            }
        }
        protected void btn_confirm_generate_certificado_Click(object sender, EventArgs e)
        {
            string matricula = txb_matricula.Text;
            string programa = ddl_programa.SelectedValue;
            string folio = model.obtenFolioTCEEL(matricula, programa);
            hdf_Folio.Value = folio;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_warning_Confirm", "alert_warning_Confirm(" + folio + ");", true);
        }

        protected void btn_generate_certificado_Click(object sender, EventArgs e)
        {
            try
            {
                if (txb_fechaExpedicion.Text != String.Empty || txb_fechaExamenProfecional.Text != String.Empty || txb_fechaTerminacion.Text != String.Empty)
                {
                    string xml = MakeXML();
                    txb_Respuesta.Visible = true;
                    txb_Respuesta.Text = xml;
                    btn_generarConfirmarCertificado.Visible = true;
                    string matricula = txb_matricula.Text;
                    string programa = ddl_programa.SelectedValue;
                    string Campus = lb_abrebCampus.Text;
                    string folio = hdf_Folio.Value;
                    model.InsertTTIEL(folio, matricula, programa, Campus, xml, Session["usuario"].ToString());
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_warning", "alert_warning('Ingresa los datos requeridos');", true);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "downloaddoc", "downloaddoc()", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_success", "alert_success('Certificado generado y enviado correctamente');", true);
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tceel", Session["usuario"].ToString(), ex.StackTrace);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
            }
        }
        protected void btn_obtenCertificadoActual_Click(object sender, EventArgs e)
        {
        }

        protected void btn_send_certificado_Click(object sender, EventArgs e)
        {
            try
            {
                string xml = MakeXML();
                string matricula = txb_matricula.Text;
                string programa = ddl_programa.SelectedValue;
                string Campus = lb_abrebCampus.Text;
                string folio = hdf_Folio.Value;
                
                CargaInicial();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_success", "alert_success('Certificado enviado correctamente con el folio " + "" + "');", true);

            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tceel", Session["usuario"].ToString(), ex.StackTrace);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
            }
        }

        private string MakeXML()
        {
            System.Xml.XmlTextWriter writer;
            System.Text.StringBuilder sb;
            sb = new StringBuilder();
            System.Xml.XmlDocument xmlDoc;
            xmlDoc = new System.Xml.XmlDocument();
            writer = new System.Xml.XmlTextWriter(new StringWriter(sb));
            string response = string.Empty;           
            response = "<?xml version=\"1.0\" encoding =\"utf-8\" ?>";
            writer.WriteStartElement("TituloElectronico");
            writer.WriteAttributeString("xmlns", "https://www.siged.sep.gob.mx/titulos/");
            writer.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
            writer.WriteAttributeString("xsi:schemaLocation", "https://www.siged.sep.gob.mx/titulos/ schema.xsd");
            writer.WriteAttributeString("version", "1.0");
            writer.WriteAttributeString("folioControl", hdf_Folio.Value);
            buildFirmaResponsables(writer);
            buildInstitucion(writer);
            buildCarrera(writer);
            buildProfesionista(writer);
            buildExpedicion(writer);
            buildAntecedente(writer);
            writer.WriteEndElement();//close TituloElectronico
            xmlDoc.LoadXml(sb.ToString());
            return response + xmlDoc.InnerXml.ToString();
        }
        private void buildFirmaResponsables(System.Xml.XmlTextWriter writer)
        {
            writer.WriteStartElement("FirmaResponsables");
            writer.WriteStartElement("FirmaResponsable");
            writer.WriteAttributeString("nombre", lb_nombreFuncionario.Text);
            writer.WriteAttributeString("primerApellido", lb_apellidoPFuncionario.Text);
            writer.WriteAttributeString("segundoApellido", lb_apellidoMFuncionario.Text);
            writer.WriteAttributeString("curp", lb_CURPFuncionario.Text);
            writer.WriteAttributeString("idCargo", hdf_idCargoFuncionario.Value);
            writer.WriteAttributeString("cargo", lb_CargoFuncionario.Text);
            writer.WriteAttributeString("abrTitulo", "");
            writer.WriteAttributeString("sello", "");
            writer.WriteAttributeString("certificadoResponsable", "");
            writer.WriteAttributeString("noCertificadoResponsable", "");
            writer.WriteEndElement();
            writer.WriteEndElement();
        }
        private void buildInstitucion(System.Xml.XmlTextWriter writer)
        {
            writer.WriteStartElement("Institucion");
            writer.WriteAttributeString("cveInstitucion", lb_campusClave.Text);
            writer.WriteAttributeString("nombreInstitucion", lb_campus.Text);
            writer.WriteEndElement();
        }
        private void buildCarrera(System.Xml.XmlTextWriter writer)
        {
            writer.WriteStartElement("Carrera");
            writer.WriteAttributeString("cveCarrera", lb_claveIncorporante.Text);
            writer.WriteAttributeString("nombreCarrera", lb_nombreLegal.Text);            
            writer.WriteAttributeString("fechaInicio", lb_fechaInicio.Text);
            writer.WriteAttributeString("fechaTerminacion", lb_fechaTermino.Text);
            writer.WriteAttributeString("idAutorizacionReconocimiento", ddl_reconocimiento.SelectedValue);
            writer.WriteAttributeString("autorizacionReconocimiento", ddl_reconocimiento.SelectedItem.Text);
            writer.WriteAttributeString("numeroRvoe", lb_RVOE.Text);
            writer.WriteEndElement();
        }
        private void buildProfesionista(System.Xml.XmlTextWriter writer)
        {
            writer.WriteStartElement("Profesionista");
            writer.WriteAttributeString("curp", lb_CURP.Text);
            writer.WriteAttributeString("nombre", lb_nombre.Text);
            writer.WriteAttributeString("primerApellido", lb_apellidoPaterno.Text);
            writer.WriteAttributeString("segundoApellido", lb_apellidoMaterno.Text);
            writer.WriteAttributeString("correoElectronico", "titulos@universidad-coremo.edu.mx");
            writer.WriteEndElement();
        }
        private void buildExpedicion(System.Xml.XmlTextWriter writer)
        {
            writer.WriteStartElement("Expedicion");
            DateTime d = DateTime.ParseExact(txb_fechaExpedicion.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            writer.WriteAttributeString("fechaExpedicion", d.ToString("yyyy-MM-dd"));
            writer.WriteAttributeString("idModalidadTitulacion", ddl_modalidadTitulacion.SelectedValue);
            writer.WriteAttributeString("modalidadTitulacion", ddl_modalidadTitulacion.SelectedItem.Text);
            DateTime de = DateTime.ParseExact(txb_fechaExamenProfecional.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            writer.WriteAttributeString("fechaExencionExamenProfesional", de.ToString("yyyy-MM-dd"));
            writer.WriteAttributeString("cumplioServicioSocial", "1");
            writer.WriteAttributeString("idFundamentoLegalServicioSocial", ddl_servicioSocial.SelectedValue);
            writer.WriteAttributeString("fundamentoLegalServicioSocial", ddl_servicioSocial.SelectedItem.Text);
            writer.WriteAttributeString("idEntidadFederativa", hdf_IdEntidadFederativaCampus.Value);
            writer.WriteAttributeString("entidadFederativa", hdf_EntidadFederativaCampus.Value);
            writer.WriteEndElement();
        }
        private void buildAntecedente(System.Xml.XmlTextWriter writer)
        {
            writer.WriteStartElement("Antecedente");
            writer.WriteAttributeString("institucionProcedencia", lb_institutoProcedencia.Text);
            writer.WriteAttributeString("idTipoEstudioAntecedente", ddl_estudioAntecedente.SelectedValue);
            writer.WriteAttributeString("tipoEstudioAntecedente", ddl_estudioAntecedente.SelectedItem.Text);
            writer.WriteAttributeString("idEntidadFederativa", hdf_IdEntidadFederativa.Value);
            writer.WriteAttributeString("entidadFederativa", hdf_EntidadFederativa.Value);
            DateTime de = DateTime.ParseExact(txb_fechaTerminacion.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            writer.WriteAttributeString("fechaTerminacion", de.ToString("yyyy-MM-dd"));
            writer.WriteEndElement();
        }
    }
}