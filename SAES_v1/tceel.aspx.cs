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
    public partial class tceel : System.Web.UI.Page
    {
        Utilidades utils = new Utilidades();
        Catalogos catalogos = new Catalogos();
        CertificacionElectronica model = new CertificacionElectronica();
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
            lb_claveGenero.Text = string.Empty;
            lb_genero.Text = string.Empty;
            lb_fechaNacimiento.Text = string.Empty;

            ddl_programa = utils.BeginDropdownList(ddl_programa);
            lb_campus.Text              = string.Empty;
            lb_abrebCampus.Text         = string.Empty;
            lb_campusClave.Text         = string.Empty;
            lb_claveIncorporante.Text   = string.Empty;
            lb_nombreLegal.Text         = string.Empty;
            lb_RVOE.Text                = string.Empty;
            lb_fechaExpedicion.Text     = string.Empty;
            lb_clavePeriodicidad.Text   = string.Empty;
            lb_periodicidad.Text        = string.Empty;
            lb_plan.Text                = string.Empty;
            lb_claveNivelEstudios.Text  = string.Empty;
            lb_nivelEstudios.Text       = string.Empty;
            lb_creditos.Text            = string.Empty;
            lb_califMinima.Text         = string.Empty;
            lb_califMaxima.Text         = string.Empty;
            lb_califMinAprobatoria.Text = string.Empty;

            lb_TipoCertificado.Text = "CERTIFICADO [  ]";

            txb_fechaExpedicion.Text = "";

            lb_materias.Text = string.Empty;
            lb_promedio.Text = string.Empty;
            lb_totalCreditos.Text = string.Empty;
            lb_noCiclos.Text = string.Empty;

            Gridtceel = utils.BeginGrid(Gridtceel);

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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ctrl_fecha_expedicion", "ctrl_fecha_expedicion();", true);
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
                lb_claveGenero.Text = Catalogo.Rows[0]["IdGenero"].ToString();
                lb_genero.Text = Catalogo.Rows[0]["Genero"].ToString();
                lb_fechaNacimiento.Text = Catalogo.Rows[0]["FechaNacimiento"].ToString();
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
        protected void ddl_programa_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                btn_cancel.Visible = true;                
                btn_generarConfirmarCertificado.Visible = true;
                string matricula = txb_matricula.Text;
                string programa = ddl_programa.SelectedValue;


                string cuentafolio = model.obtenValidaFolioTCEEL(matricula, programa);
                string folio = model.obtenFolioTCEEL(matricula, programa);
                if (int.Parse(cuentafolio) >= int.Parse(folio))
                {
                    folio = model.obtenFolioTCEEL(matricula, programa);
                    hdf_Folio.Value = folio;
                }
                hdf_Folio.Value = folio;
                DataTable dt = catalogos.obtenAlumnoProgramaTCEEL(matricula, programa);
                if (dt != null && dt.Rows.Count > 0)
                {
                    lb_campus.Text = dt.Rows[0]["IdCampus"].ToString();
                    lb_abrebCampus.Text = dt.Rows[0]["ClaveCampus"].ToString();
                    lb_campusClave.Text = dt.Rows[0]["DescripcionCampus"].ToString();
                    lb_claveIncorporante.Text = dt.Rows[0]["IdCarrera"].ToString();
                    lb_nombreLegal.Text = dt.Rows[0]["NombreLegal"].ToString();
                    lb_RVOE.Text = dt.Rows[0]["RVOE"].ToString();
                    lb_fechaExpedicion.Text = dt.Rows[0]["FechaExpedicion"].ToString().Trim();
                    lb_clavePeriodicidad.Text = dt.Rows[0]["IdPeriodo"].ToString();
                    lb_periodicidad.Text = dt.Rows[0]["Periodicidad"].ToString();
                    lb_plan.Text = dt.Rows[0]["ClavePlanEstudios"].ToString();
                    lb_claveNivelEstudios.Text = dt.Rows[0]["IdNivelEstudios"].ToString();
                    lb_nivelEstudios.Text = dt.Rows[0]["DescripcionNivelEstudios"].ToString();
                    lb_creditos.Text = dt.Rows[0]["Creditos"].ToString();
                    lb_califMinima.Text = dt.Rows[0]["CalificacionMinima"].ToString();
                    lb_califMaxima.Text = dt.Rows[0]["CalificacionMaxima"].ToString();
                    lb_califMinAprobatoria.Text = dt.Rows[0]["CalificacionMinimaAprobatoria"].ToString();
                }
                DataTable dtTipoCert = catalogos.obtenTipoCertificadoTCEEL(matricula, programa);

                if (dtTipoCert != null && dtTipoCert.Rows.Count > 0)
                {
                    string tipocert = "CERTIFICADO [ "+ dtTipoCert.Rows[0]["IdTipoCertificacion"].ToString() + " ]";
                    lb_TipoCertificado.Text = tipocert;
                }

                DataTable TotalMaterias = catalogos.obtenDatosTotalesTCEEL(matricula, programa);

                if (TotalMaterias != null && TotalMaterias.Rows.Count > 0)
                {
                    lb_materias.Text = TotalMaterias.Rows[0]["TotalMaterias"].ToString();
                    lb_promedio.Text = TotalMaterias.Rows[0]["TotalPromedio"].ToString();
                    lb_totalCreditos.Text = TotalMaterias.Rows[0]["TotalCreditos"].ToString();
                    lb_noCiclos.Text = TotalMaterias.Rows[0]["NumeroCiclos"].ToString();
                }     

                DataTable DatosMateria = catalogos.obtenDatosMateriaTCEEL(matricula, programa);

                if (DatosMateria != null && DatosMateria.Rows.Count > 0)
                {
                    Gridtceel = utils.BeginGrid(Gridtceel, DatosMateria);
                }

                DataTable DatosFuncionario = catalogos.obtenDatosFuncionarioTCEEL(matricula, programa);

                if (DatosFuncionario != null && DatosFuncionario.Rows.Count > 0)
                {
                    lb_nombreFuncionario.Text = DatosFuncionario.Rows[0]["Nombre"].ToString();
                    lb_apellidoPFuncionario.Text = DatosFuncionario.Rows[0]["ApellidoP"].ToString();
                    lb_apellidoMFuncionario.Text = DatosFuncionario.Rows[0]["ApellidoM"].ToString();
                    lb_CURPFuncionario.Text = DatosFuncionario.Rows[0]["CURP"].ToString();
                    lb_CargoFuncionario.Text = DatosFuncionario.Rows[0]["IdCargo"].ToString();
                }
                reloadComponents();
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tceel", Session["usuario"].ToString(), ex.StackTrace);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_error", "alert_error('" + mensaje_error + "');", true);
            }
        }

        private void reloadComponents()
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "generic_datatable_load", "generic_datatable_load('Gridtceel');", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ctrl_fecha_expedicion", "ctrl_fecha_expedicion();", true);
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
            string folioActual = model.obtenValidaFolioTCEEL(matricula, programa);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_warning_Confirm", "alert_warning_Confirm(" + folioActual + ");", true);
        }

        protected void btn_generate_certificado_Click(object sender, EventArgs e)
        {
            try
            {
                if (txb_fechaExpedicion.Text != String.Empty)
                {
                    string xml = MakeXML();
                    txb_Respuesta.Visible = true;
                    txb_Respuesta.Text = xml;
                    btn_generarConfirmarCertificado.Visible = true;
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_warning", "alert_warning('Ingresa una fecha de expedición');", true);                

                reloadComponents();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "downloaddoc", "downloaddoc()", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_success", "alert_success('Certificado generado correctamente');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "generic_datatable_load", "generic_datatable_load('ContentPlaceHolder1_Gridtceel');", true);
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
                string cuentafolio = model.obtenValidaFolioTCEEL(matricula, programa);
                string folio = hdf_Folio.Value;
                model.InsertaTCEEL(folio, matricula, programa, Campus, xml, Session["usuario"].ToString());
                CargaInicial();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert_success", "alert_success('Certificado enviado correctamente con el folio " + folio + "');", true);

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

            response = "<?xml version=\"1.0\" encoding =\"utf-8\" standalone =\"yes\" ?>";
            buildDec(writer);
            buildServicioFirmante(writer);
            buildIpes(writer);
            buildRvoe(writer);
            buildCarrera(writer);
            buildAlumno(writer);
            buildExpedicion(writer);
            buildAsignaturas(writer);

            writer.WriteEndElement();//close Dec
            xmlDoc.LoadXml(sb.ToString());
            return response + xmlDoc.InnerXml.ToString();
        }

        private void buildDec(System.Xml.XmlTextWriter writer)
        {
            writer.WriteStartElement("Dec");
            writer.WriteAttributeString("version", "3.0");
            writer.WriteAttributeString("tipoCertificado", "5");
            writer.WriteAttributeString("folioControl", hdf_Folio.Value);
            writer.WriteAttributeString("sello", "LvWJCy2eW3mqS2dX7Dv/8oQqqajQa2fJoiC9rTe9ei53MmdpWbogMS4tTg8jdZYKWdeoPiARufBbvri/Sxe5tlW13E4xZ+12TUuLPOduqfip1mn3G4ylNsLat+3Wz+QejB9rJ3OIvdWhDv6/imxdbcXg5ZxoRL/wibBSJOE0oU8xMaNH2POqAbXdJ9eHa9o2p9Uz6gvgTA5rKe5tl9vwucy+n5lcSfm4NnUhfbP1q7oo2Gl2pvyIlVhpvPU2gb9Y2edYZPGh5AMZbVBdaxryelMxkYBX//ktboxrcRP8vkoBblUfP6hGKkUJLVzZv79ygkZM09mMgu5OmfiyeePS5Q==");
            writer.WriteAttributeString("certificadoResponsable", "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");
            writer.WriteAttributeString("noCertificadoResponsable", "00001000000502044780");
            writer.WriteAttributeString("xmlns", "https://www.siged.sep.gob.mx/certificados/");
        }
        private void buildServicioFirmante(System.Xml.XmlTextWriter writer)
        {
            writer.WriteStartElement("ServicioFirmante");
            writer.WriteAttributeString("idEntidad", "20499");
            writer.WriteEndElement();
        }
        private void buildIpes(System.Xml.XmlTextWriter writer)
        {
            writer.WriteStartElement("Ipes");
            writer.WriteAttributeString("idNombreInstitucion", "20499");//20499
            string matricula = txb_matricula.Text;
            string programa = ddl_programa.SelectedValue;
            DataTable Campus = catalogos.obtenCampusTCEEL(matricula, programa);

            if (Campus != null && Campus.Rows.Count > 0)
            {
                hdf_IdCampus.Value = Campus.Rows[0]["IdCampus"].ToString();
                writer.WriteAttributeString("idCampus", Campus.Rows[0]["IdCampus"].ToString());
                writer.WriteAttributeString("idEntidadFederativa", Campus.Rows[0]["IdEntidadFederativa"].ToString());
            }
            
            writer.WriteStartElement("Responsable");
            writer.WriteAttributeString("curp", lb_CURPFuncionario.Text);
            writer.WriteAttributeString("nombre", lb_nombreFuncionario.Text);
            writer.WriteAttributeString("primerApellido", lb_apellidoPFuncionario.Text);
            writer.WriteAttributeString("segundoApellido", lb_apellidoMFuncionario.Text);
            writer.WriteAttributeString("idCargo", lb_CargoFuncionario.Text);
            writer.WriteEndElement(); //Responsable
            writer.WriteEndElement(); //Ipes
        }
        private void buildRvoe(System.Xml.XmlTextWriter writer)
        {
            writer.WriteStartElement("Rvoe");
            writer.WriteAttributeString("numero", lb_RVOE.Text);//20180409
            DateTime d = DateTime.ParseExact(lb_fechaExpedicion.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            writer.WriteAttributeString("fechaExpedicion", d.ToString("yyyy-MM-dd'T'hh:mm:ss"));
            writer.WriteEndElement(); //Rvoe
        }
        private void buildCarrera(System.Xml.XmlTextWriter writer)
        {
            writer.WriteStartElement("Carrera");
            writer.WriteAttributeString("idCarrera", lb_claveIncorporante.Text);//"14"
            writer.WriteAttributeString("idTipoPeriodo", lb_clavePeriodicidad.Text); //"93"
            writer.WriteAttributeString("clavePlan", lb_plan.Text);//"2017"
            writer.WriteAttributeString("idNivelEstudios", lb_claveNivelEstudios.Text);//"81"
            writer.WriteAttributeString("calificacionMinima", lb_califMinima.Text);//"5"
            writer.WriteAttributeString("calificacionMaxima", lb_califMaxima.Text);//"10"
            writer.WriteAttributeString("calificacionMinimaAprobatoria", lb_califMinAprobatoria.Text);//"6.00"
            writer.WriteEndElement(); //Carrera
        }
        private void buildAlumno(System.Xml.XmlTextWriter writer)
        {
            writer.WriteStartElement("Alumno");
            writer.WriteAttributeString("numeroControl", hfd_numero_persona.Value);
            writer.WriteAttributeString("curp", lb_CURP.Text);
            writer.WriteAttributeString("nombre", lb_nombre.Text);
            writer.WriteAttributeString("primerApellido", lb_apellidoPaterno.Text);
            writer.WriteAttributeString("segundoApellido", lb_apellidoMaterno.Text);
            writer.WriteAttributeString("idGenero", lb_claveGenero.Text);
            DateTime d = DateTime.ParseExact(lb_fechaNacimiento.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            writer.WriteAttributeString("fechaNacimiento", d.ToString("yyyy-MM-dd'T'hh:mm:ss"));
            writer.WriteEndElement(); //Alumno
        }
        private void buildExpedicion(System.Xml.XmlTextWriter writer)
        {
            writer.WriteStartElement("Expedicion");
            decimal creditos = Convert.ToDecimal(lb_creditos.Text);
            decimal totalCreditos = Convert.ToDecimal(lb_totalCreditos.Text);
            writer.WriteAttributeString("idTipoCertificacion", (creditos >= totalCreditos) ? "80" : "79" );//"79"
            //DateTime dt = DateTime.Parse(txb_fechaExpedicion.Text.Trim());
            //string fechaExpedicion = dt.ToString("yyyy-MM-ddThh:mm:ss");
            //writer.WriteAttributeString("fecha", fechaExpedicion);
            DateTime d = DateTime.ParseExact(txb_fechaExpedicion.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            writer.WriteAttributeString("fecha", d.ToString("yyyy-MM-dd'T'hh:mm:ss"));
            //writer.WriteAttributeString("fecha", txb_fechaExpedicion.Text.Trim());
            string matricula = txb_matricula.Text;
            string programa = ddl_programa.SelectedValue;
            DataTable Campus = catalogos.obtenCampusTCEEL(matricula, programa);

            if (Campus != null && Campus.Rows.Count > 0)
            {
                writer.WriteAttributeString("idLugarExpedicion", Campus.Rows[0]["IdEntidadFederativa"].ToString());//2022-07-10T00:00:00
            }
            writer.WriteEndElement(); //Expedicion
        }
        private void buildAsignaturas(System.Xml.XmlTextWriter writer)
        {
            writer.WriteStartElement("Asignaturas");
            writer.WriteAttributeString("total", "45");
            writer.WriteAttributeString("asignadas", lb_materias.Text);
            writer.WriteAttributeString("promedio", lb_promedio.Text);
            writer.WriteAttributeString("totalCreditos", lb_totalCreditos.Text);
            writer.WriteAttributeString("creditosObtenidos", lb_creditos.Text);
            writer.WriteAttributeString("numeroCiclos", lb_noCiclos.Text);

            foreach (GridViewRow row in Gridtceel.Rows)
            {
                writer.WriteStartElement("Asignaturas");
                writer.WriteAttributeString("IdAsignatura", row.Cells[1].Text);//"144"
                writer.WriteAttributeString("ciclo", row.Cells[3].Text);//"2018-1"
                writer.WriteAttributeString("calificacion", row.Cells[5].Text);//"8.00"
                writer.WriteAttributeString("idObservaciones", row.Cells[8].Text);
                writer.WriteAttributeString("idTipoAsignatura", row.Cells[9].Text);//"263"
                writer.WriteAttributeString("creditos", row.Cells[10].Text);//"7.00"
                writer.WriteEndElement();//Asignaturas
            }

            writer.WriteEndElement(); //Asignaturas
        }
    }
}