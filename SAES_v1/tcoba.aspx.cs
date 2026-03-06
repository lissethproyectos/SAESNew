using SAES_DBO.Models;
using SAES_Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static SAES_DBO.Models.ModelAlumno;
using static SAES_DBO.Models.ModelBanco;

namespace SAES_v1
{
    public partial class tcoba : System.Web.UI.Page
    {
        #region <Variables>
        //Utilidades utils = new Utilidades();
        AlumnoService serviceAlumno = new AlumnoService();
        List<ModelObtenPaisesResponse> lstPaises = new List<ModelObtenPaisesResponse>();
        Catalogos_grales_Service serviceCatalogo = new Catalogos_grales_Service();
        Catalogos serviceCatalogo2 = new Catalogos();
        BancoService serviceBanco = new BancoService();

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                grid_tcoba();

                ddl_banco.DataSource = serviceCatalogo.Qry_tbanc_Combo();
                ddl_banco.DataValueField = "clave";
                ddl_banco.DataTextField = "Nombre";
                ddl_banco.DataBind();



                ddl_cobranza.DataSource = serviceCatalogo2.ObtenerComboComun("Concepto_Cobranza", "");
                ddl_cobranza.DataValueField = "clave";
                ddl_cobranza.DataTextField = "Descripcion";
                ddl_cobranza.DataBind();
            }

        }

        protected void ddl_config_SelectedIndexChanged(object sender, EventArgs e)
        {
            datos.Visible = false;

            try
            {
                if (ddl_config.SelectedValue == "C")
                {
                    datos.Visible = false;
                    lblTipoCobranza.Text = "Especificar valores por número de columna.";
                    txtMatriculaFin.Text = string.Empty;
                    //txtMatriculaFin.Visible = false;
                    txtTransaccionFin.Text = string.Empty;
                    //txtTransaccionFin.Visible = false;
                    txtFechaFin.Text = string.Empty;
                    txtFechaFin.Visible = false;
                    txtImporteFin.Text = string.Empty;
                    txtImporteFin.Visible = false;
                    //txtPosicionMatricula.Visible = true;
                    //txtPosicionTrans.Visible = true;
                    //txtPosicionMatricula.Text = string.Empty;
                    //txtPosicionTrans.Text = string.Empty;
                    //txtReferenciaFin.Text = string.Empty;
                    //txtReferenciaFin.Visible = false;
                    lblValorIni.Text = "NÚMERO COLUMNA";
                    lblValorFin.Visible = false;
                    //lblPoscTrans.Visible = true;
                    //reqMatFin.Visible = false;
                    lblMatricula.Visible = true;
                    lblTransaccion.Visible = true;

                    rowRef.Visible = true;
                    //colLeyMatricula.Visible=true;
                }
                else
                {
                    datos.Visible = true;
                    lblTipoCobranza.Text = "Especificar valores por posición de texto.";
                    txtMatriculaFin.Visible = true;
                    txtMatriculaFin.Text = string.Empty;
                    lblMatricula.Visible = false;
                    lblTransaccion.Visible = false;

                    txtTransaccionFin.Visible = true;
                    txtTransaccionFin.Text = string.Empty;
                    txtFechaFin.Visible = true;
                    txtFechaFin.Text = string.Empty;
                    txtImporteFin.Visible = true;
                    txtImporteFin.Text = string.Empty;
                    //txtPosicionMatricula.Visible = false;
                    //txtPosicionTrans.Visible = false;
                    //txtPosicionMatricula.Text = string.Empty;
                    //txtPosicionTrans.Text = string.Empty;
                    //txtReferenciaFin.Visible = true;
                    //txtReferenciaFin.Text = string.Empty;
                    lblValorIni.Text = "VALOR INICIO";
                    lblValorFin.Visible = true;
                    //rowRef.Visible=false;
                    reqMatFin.Visible = true;
                    //lblPoscTrans.Visible = false;
                    rowRef.Visible = false;
                    //colLeyMatricula.Visible = false;

                }

                //if (ddl_banco.SelectedValue != "" && ddl_config.SelectedValue != "" && ddl_cobranza.SelectedValue != "")
                if (ddl_config.SelectedValue != "")
                {
                    grid_tcoba();
                    datos.Visible = true;
                }
                else
                    datos.Visible = false;

            }
            catch (Exception ex)
            {
                //Logs
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcoba", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }
        protected void ddl_banco_SelectedIndexChanged(object sender, EventArgs e)
        {
            datos.Visible = false;
            try
            {
                if (ddl_banco.SelectedValue != "" && ddl_config.SelectedValue != "" && ddl_cobranza.SelectedValue != "")
                {
                    grid_tcoba();
                    datos.Visible = true;
                }
            }
            catch (Exception ex)
            {
                //Logs
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcoba", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void ddl_cobranza_SelectedIndexChanged(object sender, EventArgs e)
        {
            datos.Visible = false;
            try
            {
                if (ddl_banco.SelectedValue != "" && ddl_config.SelectedValue != "" && ddl_cobranza.SelectedValue != "")
                {
                    grid_tcoba();
                    datos.Visible = true;
                }
            }
            catch (Exception ex)
            {
                //Logs
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcoba", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void linkBttnGuardar_Click(object sender, EventArgs e)
        {
            ModelInsertarTcobaResponse objExisteRegistro = new ModelInsertarTcobaResponse();
            try
            {

                objExisteRegistro = serviceBanco.InsertarTcoba(ddl_banco.SelectedValue, ddl_config.SelectedValue, ddl_cobranza.SelectedValue,
                    txtMatriculaIni.Text, txtMatriculaFin.Text, txtTransaccionIni.Text, txtTransaccionFin.Text, txtFechaIni.Text, txtFechaFin.Text,
                    txtImporteIni.Text, txtImporteFin.Text, Session["usuario"].ToString(), ddl_estatus.SelectedValue, txtReferencia.Text);
                if (objExisteRegistro != null)
                {
                    if (objExisteRegistro.Existe == "0")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                        ddl_banco.SelectedIndex = 0;
                        ddl_config.SelectedIndex = 0;
                        ddl_cobranza.SelectedIndex = 0;
                        ddl_estatus.SelectedIndex = 0;
                        txtMatriculaIni.Text = string.Empty;
                        txtMatriculaFin.Text = string.Empty;
                        txtTransaccionIni.Text = string.Empty;
                        txtTransaccionFin.Text = string.Empty;
                        txtFechaIni.Text = string.Empty;
                        txtFechaFin.Text = string.Empty;
                        txtImporteIni.Text = string.Empty;
                        txtImporteFin.Text = string.Empty;
                        txtReferencia.Text = string.Empty;
                        datos.Visible = false;
                        grid_tcoba();
                        GridTcoba.SelectedIndex = -1;
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
                Global.inserta_log(mensaje_error, "tcoba", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
            }
        }

        protected void linkBttnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                serviceBanco.EditarTcoba(ddl_banco.SelectedValue, ddl_config.SelectedValue, ddl_cobranza.SelectedValue,
                    txtMatriculaIni.Text, txtMatriculaFin.Text, txtTransaccionIni.Text, txtTransaccionFin.Text, txtFechaIni.Text, txtFechaFin.Text,
                    txtImporteIni.Text, txtImporteFin.Text, Session["usuario"].ToString(), ddl_estatus.SelectedValue, txtReferencia.Text);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "update_p", "update();", true);
                ddl_banco.SelectedIndex = 0;
                ddl_config.SelectedIndex = 0;
                ddl_cobranza.SelectedIndex = 0;
                ddl_estatus.SelectedIndex = 0;



                txtMatriculaIni.Text = string.Empty;
                txtMatriculaFin.Text = string.Empty;
                txtTransaccionIni.Text = string.Empty;
                txtTransaccionFin.Text = string.Empty;
                txtFechaIni.Text = string.Empty;
                txtFechaFin.Text = string.Empty;
                txtImporteIni.Text = string.Empty;
                txtImporteFin.Text = string.Empty;
                txtReferencia.Text = string.Empty;
                //txtReferenciaIni.Text = string.Empty;
                //txtReferenciaFin.Text = string.Empty;
                //txtPosicionMatricula.Text = string.Empty;
                //txtPosicionTrans.Text= string.Empty;
                linkBttnGuardar.Visible = true;
                linkBttnCancelar.Visible = true;
                linkBttnCancelarModificar.Visible = false;
                linkBttnModificar.Visible = false;
                datos.Visible = false;
                grid_tcoba();
                GridTcoba.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcoba", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
            }
        }

        protected void linkBttnCancelar_Click(object sender, EventArgs e)
        {
            ddl_banco.SelectedIndex = 0;
            ddl_config.SelectedIndex = 0;
            ddl_cobranza.SelectedIndex = 0;
            ddl_estatus.SelectedIndex = 0;
            txtMatriculaIni.Text = string.Empty;
            txtMatriculaFin.Text = string.Empty;
            txtTransaccionIni.Text = string.Empty;
            txtTransaccionFin.Text = string.Empty;
            txtFechaIni.Text = string.Empty;
            txtFechaFin.Text = string.Empty;
            txtImporteIni.Text = string.Empty;
            txtImporteFin.Text = string.Empty;

        }

        protected void linkBttnCancelarModificar_Click(object sender, EventArgs e)
        {
            ddl_banco.SelectedIndex = 0;
            ddl_config.SelectedIndex = 0;
            ddl_cobranza.SelectedIndex = 0;
            ddl_estatus.SelectedIndex = 0;

            ddl_banco.Enabled = true;
            ddl_config.Enabled = true;
            ddl_cobranza.Enabled = true;
            ddl_estatus.Enabled = true;

            datos.Visible = false;

            txtMatriculaIni.Text = string.Empty;
            txtMatriculaFin.Text = string.Empty;
            txtTransaccionIni.Text = string.Empty;
            txtTransaccionFin.Text = string.Empty;
            txtFechaIni.Text = string.Empty;
            txtFechaFin.Text = string.Empty;
            txtImporteIni.Text = string.Empty;
            txtImporteFin.Text = string.Empty;
            txtReferencia.Text = string.Empty;

            linkBttnGuardar.Visible = true;
            linkBttnCancelar.Visible = true;
            linkBttnCancelarModificar.Visible = false;
            linkBttnModificar.Visible = false;
        }
        protected void grid_tcoba()
        {
            try
            {
                GridTcoba.DataSource = serviceBanco.ObtenerDatosTcoba();
                GridTcoba.DataBind();
            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcoba", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void GridTcoba_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddl_banco.SelectedValue = GridTcoba.SelectedRow.Cells[5].Text;
                ddl_config.SelectedValue = GridTcoba.SelectedRow.Cells[6].Text;
                ddl_config_SelectedIndexChanged(null, null);
                ddl_cobranza.SelectedValue = GridTcoba.SelectedRow.Cells[8].Text;
                ddl_estatus.SelectedValue = GridTcoba.SelectedRow.Cells[7].Text;
                txtMatriculaIni.Text = GridTcoba.SelectedRow.Cells[9].Text;
                txtMatriculaFin.Text = GridTcoba.SelectedRow.Cells[10].Text;
                txtTransaccionIni.Text = GridTcoba.SelectedRow.Cells[11].Text;
                txtTransaccionFin.Text = GridTcoba.SelectedRow.Cells[12].Text;
                txtFechaIni.Text = GridTcoba.SelectedRow.Cells[13].Text;
                txtFechaFin.Text = GridTcoba.SelectedRow.Cells[14].Text;
                txtImporteIni.Text = GridTcoba.SelectedRow.Cells[15].Text;
                txtImporteFin.Text = GridTcoba.SelectedRow.Cells[16].Text;
                txtReferencia.Text = HttpUtility.HtmlDecode(GridTcoba.SelectedRow.Cells[17].Text);
                //txtReferenciaFin.Text = HttpUtility.HtmlDecode(GridTcoba.SelectedRow.Cells[18].Text);

                //ddl_banco.Enabled = false;
                //ddl_config.Enabled = false;
                //ddl_cobranza.Enabled = false;
                //ddl_estatus.Enabled = false;

                linkBttnGuardar.Visible = false;
                linkBttnCancelar.Visible = false;
                linkBttnCancelarModificar.Visible = true;
                linkBttnModificar.Visible = true;

                datos.Visible = true;

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcoba", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }
    }
}