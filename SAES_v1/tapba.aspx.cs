using SAES_DBO.Models;
using SAES_Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aspose.Cells;
using System.IO;
using applyWeb.Data;
using static SAES_DBO.Models.ModelBanco;
using Org.BouncyCastle.Asn1.X509;
using System.Collections;

namespace SAES_v1
{
    public partial class tapba : System.Web.UI.Page
    {
        #region <Variables>
        //Utilidades utils = new Utilidades();
        AlumnoService serviceAlumno = new AlumnoService();
        List<ModelObtenPaisesResponse> lstPaises = new List<ModelObtenPaisesResponse>();
        Catalogos serviceCatalogo = new Catalogos();
        BancoService serviceBanco = new BancoService();

        string dato = string.Empty;
        int consecutivo_ini = 0;
        int consecutivo_fin = 0;
        int consecutivo_length = 0;

        int tpers_num_ini = 0;
        int tpers_num_fin = 0;
        int tpers_num_length = 0;

        int referencia_ini = 0;
        int referencia_fin = 0;
        int referencia_length = 0;

        int importe_ini = 0;
        int importe_fin = 0;
        int importe_length = 0;

        int fecha_ini = 0;
        int fecha_fin = 0;
        int fecha_length = 0;

        decimal importe = 0;
        string fecha_carga = string.Empty;
        string fecha_pago = string.Empty;

        int matricula = 0;
        string referencia = string.Empty;
        int consecutivo = 0;
        int longitud = 0;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Gridtpers.DataSource = null;
                //Gridtpers.DataBind();
                Gridtapba.DataSource = null;
                Gridtapba.DataBind();

                combo_bancos();
                txt_fecha_c.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                //txt_fecha_c.Text = DateTime.ParseExact(fecha_prev, "yyyy-MM-dd", null).ToString("dd/MM/yyyy");

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "ctrl_fecha_c", "ctrl_fecha_c();", true);
                //Gridtapba.DataSource = serviceBanco.ObtenerDatosTapba(ddl_banco.SelectedValue, ddl_config.SelectedValue);// ("1", "202065")  
                //Gridtapba.DataBind();
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Grid", "load_datatable();", true);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "ctrl_fecha_c", "ctrl_fecha_c();", true);
        }

        protected void combo_bancos()
        {
            try
            {

                ddl_banco.DataSource = serviceCatalogo.ObtenerComboComun("Bancos", "");
                ddl_banco.DataValueField = "Clave";
                ddl_banco.DataTextField = "Descripcion";
                ddl_banco.DataBind();
            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tapba", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void ddl_config_SelectedIndexChanged(object sender, EventArgs e)
        {


            try
            {
                if (ddl_config.SelectedValue == "C") { 
                    valArchivo.ValidationExpression = "(.*?)\\.(xls|XLS|Xls|xlsx|Xlsx|XLSX)$";
                    valArchivo.ErrorMessage = "Archivo incorrecto, debe ser un archivo XLS";
                }
                else { 
                    valArchivo.ValidationExpression = "(.*?)\\.(txt|TXT|Txt|csv|CSV|Csv)$";
                    valArchivo.ErrorMessage = "Archivo incorrecto, debe ser un archivo TXT";
                }
                grid_bind_tapba();

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tapba", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void ddl_banco_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                grid_bind_tapba();
            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tapba", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }


        protected void grid_bind_tapba()
        {
            try
            {
                string fecha = DateTime.ParseExact(txt_fecha_c.Text, "yyyy-MM-dd", null).ToString("dd/MM/yyyy");

                //string fecha2 = txt_fecha_c.Text.ToString("dd/MM/yyyy");
                Gridtapba.DataSource = serviceBanco.ObtenerDatosTapba(ddl_banco.SelectedValue, ddl_config.SelectedValue, fecha);// ("1", "202065")  
                Gridtapba.DataBind();
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tedcu", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                //Response.Redirect("Inicio.aspx");
            }
        }

        protected void linkBttnUpload_Click(object sender, EventArgs e)
        {
            ModelObtenerLengthMaxLayoutResponse result = new ModelObtenerLengthMaxLayoutResponse();
            ModelValidaLayoutResponse resultLayout = new ModelValidaLayoutResponse();
            int longitud = 0;
            string fullPath = string.Empty;
            lblMsjError.Text = string.Empty;
            linkBttnEliminar.Visible = true;
            linkBttnCancelar.Text = "NO";
            try
            {
                string fecha = DateTime.ParseExact(txt_fecha_c.Text, "yyyy-MM-dd", null).ToString("dd/MM/yyyy");

                resultLayout = serviceBanco.ValidaLayout(ddl_banco.SelectedValue, fecha, ddl_config.SelectedValue);
                if (resultLayout.Valido == "0")
                {
                    if (fileUpload.HasFile)
                    {
                        fullPath = Path.Combine(Server.MapPath("~/UploadedFiles"), fileUpload.FileName);
                        hddnFile.Value=fullPath; 
                        fileUpload.SaveAs(fullPath);
                        HttpPostedFile archivo = fileUpload.PostedFile;

                        if (ddl_config.SelectedValue == "C")
                        {                           
                            cargar_excel(fullPath);
                        }
                        else
                        {
                            //result = serviceBanco.ObtenerLongitudMaximaLayout(ddl_banco.SelectedValue, ddl_config.SelectedValue);
                            //longitud = Convert.ToInt32(result.fila_max);
                            cargar_csv(archivo);
                            grid_bind_tapba();

                        }
                    }
                }
                else if (resultLayout.Valido == "1")
                {

                    if (fileUpload.HasFile)
                    {
                        fullPath = Path.Combine(Server.MapPath("~/UploadedFiles"), fileUpload.FileName);
                        hddnFile.Value = fullPath;
                        fileUpload.SaveAs(fullPath);


                        if (ddl_config.SelectedValue == "T")
                        {
                            HttpPostedFile archivo = fileUpload.PostedFile;

                            ArrayList arr = new ArrayList();
                            arr.Add(fileUpload.PostedFile);
                            Session["UploadedFiles"] = arr;
                        }
                        else
                        {
                            Session["rutaExcel"] = fullPath;
                        }
                    }

                        lblMsjError.Text = "Ya existen registros del banco " + ddl_banco.SelectedItem.ToString() + " con fecha de carga " + txt_fecha_c.Text + " ¿desea volver a cargar?";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "showModal", "$('#modalEliminar').modal('show')", true);
                }
                else if (resultLayout.Valido == "2")
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "error_layout", "error_layout();", true);
                    linkBttnEliminar.Visible = false;
                    linkBttnCancelar.Text = "SALIR";
                    lblMsjError.Text = "Los pagos ya han sido aplicados, no se puede realizar la carga nuevamente.";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "showModal", "$('#modalEliminar').modal('show')", true);
                }
            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tapba", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void cargar_excel(string fullPath)
        {
            ModelObtenerLayout datosLayout = new ModelObtenerLayout();
            List<ModelObtenerConfTcobaResponse> lstConfiguracion = new List<ModelObtenerConfTcobaResponse>();
            try
            {
                lstConfiguracion = serviceBanco.ObtenerConfiguracionTcoba(ddl_banco.SelectedValue, ddl_config.SelectedValue);
                consecutivo_ini = Convert.ToInt32(lstConfiguracion[0].tcoba_tran_inicio);
                consecutivo_fin = Convert.ToInt32(lstConfiguracion[0].tcoba_tran_fin);
                tpers_num_ini = Convert.ToInt32(lstConfiguracion[0].tcoba_tpers_inicio);
                tpers_num_fin = Convert.ToInt32(lstConfiguracion[0].tcoba_tpers_fin);
                referencia_ini = Convert.ToInt32(lstConfiguracion[0].tcoba_referencia);
                importe_ini = Convert.ToInt32(lstConfiguracion[0].tcoba_imp_inicio);
                fecha_ini = Convert.ToInt32(lstConfiguracion[0].tcoba_fecha_ini);
                string monto = string.Empty;
                Workbook wb = new Workbook(fullPath);

                // Obtener todas las hojas de trabajo
                WorksheetCollection collection = wb.Worksheets;

                // Recorra todas las hojas de trabajo
                for (int worksheetIndex = 0; worksheetIndex < collection.Count; worksheetIndex++)
                {
                    datosLayout.tapba_tbanc_clave = string.Empty;
                    datosLayout.tapba_carga_date = string.Empty;
                    datosLayout.tapba_consecutivo = string.Empty; // dato.Substring(16, 3).Trim();
                    datosLayout.tapba_tpers_num = string.Empty; //dato.Substring(7, 9).Trim();
                    datosLayout.tapba_referencia = string.Empty;  //dato.Substring(7, 12).Trim();
                    datosLayout.tapba_importe = 0;  //dato.Substring(73, 18).Trim();
                    datosLayout.tapba_fecha_pago = string.Empty;
                    datosLayout.tapba_tuser_clave = string.Empty;
                    importe = 0;
                    datosLayout.tapba_observaciones = string.Empty;
                    datosLayout.tapba_estatus = "A";
                    datosLayout.tapba_tbanc_clave = ddl_banco.SelectedValue;
                    monto = string.Empty;

                    //datosLayout.tapba_carga_date = txt_fecha_c.Text;



                    // Obtener hoja de trabajo usando su índice
                    Worksheet worksheet = collection[worksheetIndex];

                    // Imprimir el nombre de la hoja de trabajo
                    Console.WriteLine("Worksheet: " + worksheet.Name);

                    // Obtener el número de filas y columnas


                    // Imprimir el nombre de la hoja de trabajo
                    Console.WriteLine("Worksheet: " + worksheet.Name);

                    // Obtener el número de filas y columnas
                    int rows = worksheet.Cells.MaxDataRow;
                    int cols = worksheet.Cells.MaxDataColumn;


                    for (int i = 1; i <= rows; i++)
                    {
                        datosLayout.tapba_observaciones = string.Empty;
                        if (worksheet == null)
                            break;
                        else
                        {
                            try
                            {
                                tpers_num_length = tpers_num_fin - tpers_num_ini;
                                referencia = Convert.ToString(worksheet.Cells[i, referencia_ini].Value).Replace(" ", "");
                                longitud = consecutivo_fin - consecutivo_ini;
                                datosLayout.tapba_consecutivo = referencia.Substring(consecutivo_ini, longitud).Trim();
                            }
                            catch
                            {
                                datosLayout.tapba_consecutivo = string.Empty;
                                datosLayout.tapba_observaciones = "consecutivo invalido, ";
                                datosLayout.tapba_estatus = "E";
                            }
                            try
                            {
                                referencia = Convert.ToString(worksheet.Cells[i, referencia_ini].Value).Replace(" ", "");
                                /*if (tpers_num_ini == 0)
                                    tpers_num_fin = tpers_num_fin + 1;
                                */
                                longitud = tpers_num_fin - tpers_num_ini;
                                if (tpers_num_ini == 0)
                                    longitud = longitud + 1;

                                datosLayout.tapba_tpers_num = referencia.Substring(tpers_num_ini, longitud).Trim();

                                //datosLayout.tapba_tpers_num = Convert.ToString(worksheet.Cells[i, tpers_num_ini].Value).Replace(" ", "");
                            }
                            catch
                            {
                                datosLayout.tapba_tpers_num = string.Empty;
                                //datosLayout.tapba_observaciones = "matricula invalida, ";
                                datosLayout.tapba_estatus = "E";
                            }
                            try
                            {
                                datosLayout.tapba_referencia = Convert.ToString(worksheet.Cells[i, referencia_ini].Value).Replace(" ", "");
                            }
                            catch
                            {
                                datosLayout.tapba_referencia = string.Empty;
                                datosLayout.tapba_observaciones = datosLayout.tapba_observaciones + "referencia invalida, ";
                                datosLayout.tapba_estatus = "E";
                            }
                            try
                            {
                                monto = Convert.ToString(worksheet.Cells[i, importe_ini].Value);
                                
                                importe = Convert.ToDecimal(monto);
                                datosLayout.tapba_importe = importe;  //dato.Substring(73, 18).Trim();
                                //importe = Convert.ToDecimal(datosLayout.tapba_importe);
                            }
                            catch
                            {
                                datosLayout.tapba_importe = 0; //string.Empty;
                                datosLayout.tapba_observaciones = datosLayout.tapba_observaciones + "importe invalido, ";
                                importe = 0;
                                datosLayout.tapba_estatus = "E";
                            }
                            try
                            {
                                fecha_pago = Convert.ToString(worksheet.Cells[i, fecha_ini].Value).Replace(" ", "");
                                //datosLayout.tapba_carga_date = DateTime.ParseExact(fecha_pago, "yyyy-MM-dd", null).ToString("dd/MM/yyyy");
                                fecha_pago = DateTime.ParseExact(fecha_pago.Substring(0, 10), "yyyy-MM-dd", null).ToString("dd/MM/yyyy");
                                datosLayout.tapba_fecha_pago = fecha_pago;  //dato.Substring(100, 10).Trim();
                                                                            //datosLayout.tapba_fecha_pago_old = datosLayout.tapba_fecha_pago;
                            }
                            catch
                            {

                                datosLayout.tapba_fecha_pago = string.Empty;
                                datosLayout.tapba_observaciones = datosLayout.tapba_observaciones + "fecha invalida, ";
                                datosLayout.tapba_estatus = "E";
                            }

                            datosLayout.tapba_carga_date = DateTime.ParseExact(txt_fecha_c.Text, "yyyy-MM-dd", null).ToString("dd/MM/yyyy");
                            datosLayout.tapba_tuser_clave = Session["usuario"].ToString();
                            datosLayout.tapba_tcoba_ind = ddl_config.SelectedValue;
                            serviceBanco.InsertarTapba(datosLayout);

                        }


                    }
                    grid_bind_tapba();

                    //int totFilas = Convert.ToInt32(txtTot.Text);

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);


                    // Bucle a través de filas
                    //for (int i = 0; i < totFilas; i++)
                    //{
                    //}

                }

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tapba", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }
        protected void cargar_csv(HttpPostedFile archivo)
        {
            ModelObtenerLayout datosLayout = new ModelObtenerLayout();
            int consecutivo = 0;

            List<ModelObtenerConfTcobaResponse> lstConfiguracion = new List<ModelObtenerConfTcobaResponse>();
            try
            {
                lstConfiguracion = serviceBanco.ObtenerConfiguracionTcoba(ddl_banco.SelectedValue, ddl_config.SelectedValue);


                ///-- Valor consecutivo --///
                consecutivo_ini = Convert.ToInt32(lstConfiguracion[0].tcoba_tran_inicio);
                consecutivo_fin = Convert.ToInt32(lstConfiguracion[0].tcoba_tran_fin);
                consecutivo_length = consecutivo_fin - consecutivo_ini;


                ///-- Matricula --///
                tpers_num_ini = Convert.ToInt32(lstConfiguracion[0].tcoba_tpers_inicio);
                tpers_num_fin = Convert.ToInt32(lstConfiguracion[0].tcoba_tpers_fin);
                tpers_num_length = tpers_num_fin - tpers_num_ini;


                ///-- Referencia --///
                referencia_ini = Convert.ToInt32(lstConfiguracion[0].tcoba_referencia);
                referencia_length = tpers_num_ini + tpers_num_fin;

                ///-- Importe --///
                importe_ini = Convert.ToInt32(lstConfiguracion[0].tcoba_imp_inicio);
                importe_fin = Convert.ToInt32(lstConfiguracion[0].tcoba_imp_fin);
                importe_length = importe_fin - importe_ini;

                ///-- Fecha pago --///
                fecha_ini = Convert.ToInt32(lstConfiguracion[0].tcoba_fecha_ini);
                fecha_fin = Convert.ToInt32(lstConfiguracion[0].tcoba_fecha_fin);
                fecha_length = fecha_fin - fecha_ini;



                System.IO.StreamReader archivo_ap = new System.IO.StreamReader(archivo.InputStream);
                while ((dato = archivo_ap.ReadLine()) != null)
                {

                    datosLayout.tapba_tbanc_clave = string.Empty;
                    datosLayout.tapba_carga_date = string.Empty;
                    datosLayout.tapba_consecutivo = string.Empty; // dato.Substring(16, 3).Trim();
                    datosLayout.tapba_tpers_num = string.Empty; //dato.Substring(7, 9).Trim();
                    datosLayout.tapba_referencia = string.Empty;  //dato.Substring(7, 12).Trim();
                    datosLayout.tapba_importe = 0;  //dato.Substring(73, 18).Trim();
                    datosLayout.tapba_fecha_pago = string.Empty;
                    datosLayout.tapba_tuser_clave = string.Empty;
                    importe = 0;
                    datosLayout.tapba_observaciones = string.Empty;
                    datosLayout.tapba_estatus = "A";
                    datosLayout.tapba_tbanc_clave = ddl_banco.SelectedValue;
                    datosLayout.tapba_carga_date = txt_fecha_c.Text;

                    try
                    {
                        datosLayout.tapba_consecutivo = dato.Substring(consecutivo_ini, consecutivo_length).Trim(); // dato.Substring(16, 3).Trim();
                    }
                    catch
                    {
                        consecutivo = consecutivo + 1;
                        datosLayout.tapba_consecutivo = null; //Convert.ToString(consecutivo);
                        datosLayout.tapba_observaciones = "consecutivo invalido, ";
                        datosLayout.tapba_estatus = "E";
                    }
                    try
                    {
                        datosLayout.tapba_tpers_num = dato.Substring(tpers_num_ini, tpers_num_length).Trim(); //dato.Substring(7, 9).Trim();
                    }
                    catch
                    {
                        datosLayout.tapba_tpers_num = string.Empty;
                        datosLayout.tapba_observaciones = datosLayout.tapba_observaciones + "matricula invalida, ";
                        datosLayout.tapba_estatus = "E";
                    }
                    try
                    {
                        datosLayout.tapba_referencia = dato.Substring(referencia_ini, referencia_length).Trim();  //dato.Substring(7, 12).Trim();
                    }
                    catch
                    {
                        datosLayout.tapba_referencia = string.Empty;
                        datosLayout.tapba_observaciones = datosLayout.tapba_observaciones + "referencia invalida, ";
                        datosLayout.tapba_estatus = "E";
                    }
                    try
                    {
                        importe = Convert.ToDecimal(dato.Substring(importe_ini, importe_length).Trim());
                        datosLayout.tapba_importe = importe;  //dato.Substring(73, 18).Trim();
                        importe = Convert.ToDecimal(datosLayout.tapba_importe);
                    }
                    catch
                    {
                        datosLayout.tapba_importe = 0;

                        datosLayout.tapba_observaciones = datosLayout.tapba_observaciones + "importe invalido, ";
                        //importe = 0;
                        datosLayout.tapba_estatus = "E";
                    }
                    try
                    {
                        fecha_pago = dato.Substring(fecha_ini, fecha_length).Trim();
                        datosLayout.tapba_fecha_pago = DateTime.ParseExact(fecha_pago, "yyyy-MM-dd", null).ToString("dd/MM/yyyy");
                    }
                    catch
                    {
                        datosLayout.tapba_fecha_pago = string.Empty;
                        datosLayout.tapba_observaciones = datosLayout.tapba_observaciones + "fecha invalida, ";
                        datosLayout.tapba_estatus = "E";
                    }


                    datosLayout.tapba_carga_date = DateTime.ParseExact(txt_fecha_c.Text, "yyyy-MM-dd", null).ToString("dd/MM/yyyy");
                    datosLayout.tapba_tuser_clave = Session["usuario"].ToString();
                    datosLayout.tapba_tcoba_ind = ddl_config.SelectedValue;
                    //serviceBanco.InsertarTapba(tapba_tbanc_clave, tapba_carga_date, tapba_consecutivo, tapba_tpers_num, tapba_referencia, importe, tapba_fecha_pago, tapba_estatus, tapba_observaciones, tapba_tuser_clave, ddl_config.SelectedValue);
                    serviceBanco.InsertarTapba(datosLayout);

                    //}
                }
            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tapba", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void linkBttnEliminar_Click(object sender, EventArgs e)
        {
            string ruta = string.Empty;
            try
            {
                fecha_carga = DateTime.ParseExact(txt_fecha_c.Text, "yyyy-MM-dd", null).ToString("dd/MM/yyyy");
                serviceBanco.EliminarTapba(ddl_banco.SelectedValue, fecha_carga, ddl_config.SelectedValue);

                if (ddl_config.SelectedValue == "T")
                {

                    ArrayList arr = (ArrayList)Session["UploadedFiles"];
                    //To get back the first file
                    HttpPostedFile postedfile = (HttpPostedFile)arr[0];
                    cargar_csv(postedfile);
                }
                else
                {
                    if(Session["rutaExcel"]!=null)
                    {
                       ruta= Session["rutaExcel"].ToString();
                        cargar_excel(ruta);
                    }                  
                }

                grid_bind_tapba();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showModal", "$('#modalEliminar').modal('hide')", true);
            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tapba", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void txt_fecha_c_TextChanged(object sender, EventArgs e)
        {
            grid_bind_tapba();
        }

        protected void linkBttnVer_Click(object sender, EventArgs e)
        {
            grid_bind_tapba();
        }

        protected void txt_fecha_c_Unload(object sender, EventArgs e)
        {
            grid_bind_tapba();

        }

        protected void linkBtnnAplica_Click(object sender, EventArgs e)
        {
            try
            {
                if (Gridtapba.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "showAplicar", "$('#modalAplicar').modal('show')", true);

                }
            }

            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tapba", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void linkBttnAplicar_Click(object sender, EventArgs e)
        {
            try
            {
                fecha_carga = DateTime.ParseExact(txt_fecha_c.Text, "yyyy-MM-dd", null).ToString("dd/MM/yyyy");
                serviceBanco.AplicarPagos(ddl_banco.SelectedValue, fecha_carga, ddl_config.SelectedValue, Session["usuario"].ToString());

                ScriptManager.RegisterStartupScript(this, this.GetType(), "showAplicar", "$('#modalAplicar').modal('hide')", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pago_aplicado", "pago_aplicado();", true);

                grid_bind_tapba();
            }

            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tapba", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showAplicar", "$('#modalAplicar').modal('hide')", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void txt_fecha_c_TextChanged1(object sender, EventArgs e)
        {

        }
    }
}