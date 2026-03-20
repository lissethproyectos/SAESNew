using System.Data;using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace SAES_v1
{
    public partial class C_Campus : System.Web.UI.Page
    {
        Utilidades util = new Utilidades();
        Catalogos_grales_Service serviceCat = new Catalogos_grales_Service();

    protected void Page_Load(object sender, EventArgs e)
    {
        // 1. Seguridad de sesión 
        if (!HttpContext.Current.User.Identity.IsAuthenticated)
        {
            Response.Redirect(FormsAuthentication.DefaultUrl);
            Response.End();
        }
        else
        {
            if (!IsPostBack)
            {
                // 2. Configuración visual inicial 
                form_Campus.Attributes.Add("style", "display:none");
                btn_campus.Attributes.Add("style", "display:none");
                combo_estatus();
                // 3. Carga de combos iniciales 
                combo_pais(); 
                combo_campus(); 

                // 4. Atributos de validación del lado del cliente (JavaScript)
                c_prog_campus.Attributes.Add("onblur", "validarclavePrograma('ContentPlaceHolder1_c_prog_campus')");
                c_prog_campus.Attributes.Add("oninput", "validarclavePrograma('ContentPlaceHolder1_c_prog_campus')");
                
                update_prog.Attributes.Add("style", "display:none");
                btn_programa.Attributes.Add("style", "display:none");

                // 5. Configuración pestaña Cobranza
                actualizar_cob.Attributes.Add("style", "display:none");
                dd_term.Attributes.Add("style", "display:none");
                
                combo_campus_cobranza();
                combo_tipo_periodo();
                combo_concepto_calendario();
                combo_concepto_cobranza();
            }
            grid_campus_bind();
        }
    }


        protected void combo_estatus()
        {
            estatus_campus.Items.Clear();
            estatus_campus.Items.Add(new ListItem("Activo", "A"));
            estatus_campus.Items.Add(new ListItem("Inactivo", "B"));
            e_prog_campus.Items.Clear();
            e_prog_campus.Items.Add(new ListItem("Activo", "A"));
            e_prog_campus.Items.Add(new ListItem("Inactivo", "B"));
        }
            
            #region Métodos para pestañana de Campus

        protected void combo_pais()
        {
            try
            {
                // 1. Limpieza de cascada visual (Resetear combos dependientes)
                dde_campus.Items.Clear();
                dde_campus.Items.Add(new ListItem("----Selecciona un estado----", "0"));
                
                ddd_campus.Items.Clear();
                ddd_campus.Items.Add(new ListItem("----Selecciona una delegación---", "0"));
                
                ddz_campus.Items.Clear();
                ddz_campus.Items.Add(new ListItem("----Selecciona una código postal---", "0"));

                // 2. Llamada al servicio que YA EXISTE (Reutilización de código)
                // El SP 'P_QRY_TPAIS_COMBO' ya maneja el UNION con el 'Selecciona un país'
                DataTable TablaPais = serviceCat.QRY_TPAIS_COMBO();

                // 3. Vinculación al DropDownList (ddp_campus)
                ddp_campus.DataSource = TablaPais;
                ddp_campus.DataValueField = "Clave";
                ddp_campus.DataTextField = "Nombre";
                ddp_campus.DataBind();
            }
            catch (Exception ex)
            {
                // Registro de error centralizado en TLOGS
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "combo_pais_campus", Session["usuario"].ToString());
            }
        }

        protected void combo_estado(string clave_pais)
        {
            try
            {
                // 1. Limpieza de combos hijos (Cascada)
                ddd_campus.Items.Clear();
                ddd_campus.Items.Add(new ListItem("----Selecciona una delegación---", "0"));
                
                ddz_campus.Items.Clear();
                ddz_campus.Items.Add(new ListItem("----Selecciona una código postal---", "0"));

                // 2. Llamada al servicio centralizado
                // Pasamos la 'clave_pais' al método que ya creamos anteriormente
                DataTable TablaEstado = serviceCat.QRY_TESTA_COMBO(clave_pais);

                // 3. Vinculación al control dde_campus
                dde_campus.DataSource = TablaEstado;
                dde_campus.DataValueField = "Clave";
                dde_campus.DataTextField = "Nombre";
                dde_campus.DataBind();
            }
            catch (Exception ex)
            {
                // Registro de error estandarizado
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "combo_estado_campus", Session["usuario"].ToString());
            }
        }

        protected void combo_delegacion(string clave_pais, string clave_edo)
        {
            try
            {
                // 1. Limpieza del último nivel (Código Postal)
                ddz_campus.Items.Clear();
                ddz_campus.Items.Add(new ListItem("----Selecciona una código postal---", "0"));

                // 2. Llamada al servicio centralizado con doble parámetro
                // Reutilizamos el SP que ya filtra por País y Estado
                DataTable TablaDele = serviceCat.QRY_TDELE_COMBO(clave_pais, clave_edo);

                // 3. Vinculación al control ddd_campus
                ddd_campus.DataSource = TablaDele;
                ddd_campus.DataValueField = "Clave";
                ddd_campus.DataTextField = "Nombre";
                ddd_campus.DataBind();
            }
            catch (Exception ex)
            {
                // Registro de error estandarizado para auditoría
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "combo_dele_campus", Session["usuario"].ToString());
            }
        }

        protected void combo_zip(string clave_pais, string clave_edo, string clave_deleg)
        {
            try
            {
                DataTable TablaZip = serviceCat.QRY_TZIPS_COMBO(clave_pais, clave_edo, clave_deleg, zip_texto);
                ddz_campus.DataSource = TablaZip;
                ddz_campus.DataValueField = "Clave";
                ddz_campus.DataTextField = "Nombre";
                ddz_campus.DataBind();
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "combo_zip_campus", Session["usuario"].ToString());
            }
        }

        protected void grid_campus_bind()
        {
            try
            {
                GridCampus.DataSource = serviceCat.QRY_TCAMP_GRID();
                GridCampus.EditIndex = -1;
                GridCampus.DataBind();
                if (GridCampus.Rows.Count > 0)
                {
                    GridCampus.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridCampus.UseAccessibleHeader = true;
                }
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "grid_campus_bind", Session["usuario"]?.ToString() ?? "Sistema");
            }
        }

        protected bool validar_clave_campus(string clave)
        {
            try
            {
               return serviceCat.ValidarClaveCampus(clave);
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "validar_clave_campus", Session["usuario"]?.ToString() ?? "Sistema");
                return false;
            }
        }

        protected void insertar_campus()
        {
            try
            {
                if (validar_clave_campus(c_campus.Value))
                {
                    ModelInsCampusRequest request = new ModelInsCampusRequest
                    {
                        p_clave = c_campus.Value,
                        p_nombre = n_campus.Value,
                        p_direccion = direc_campus.Value,
                        p_colonia = ddz_campus.SelectedItem.Text,
                        p_pais = ddp_campus.SelectedValue,
                        p_estado = dde_campus.SelectedValue,
                        p_dele = ddd_campus.SelectedValue,
                        p_zip = zip_campus.Text,
                        p_user = Session["usuario"].ToString(),
                        p_estatus = estatus_campus.SelectedValue,
                        p_abr = a_campus.Value,
                        p_rfc = RFC_campus.Value
                    };

                    string respuesta = serviceCat.InsertarCampus(request);

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                    
                    grid_campus_bind();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "alert('La clave del campus ya existe');", true);
                }
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "insertar_campus", Session["usuario"].ToString());
            }
        }
        #endregion
       

        protected void ddp_campus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddp_campus.SelectedValue != "0")
            {
                combo_estado(ddp_campus.SelectedValue);
            }
        }

        protected void dde_campus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dde_campus.SelectedValue != "0")
            {
                combo_delegacion(ddp_campus.SelectedValue, dde_campus.SelectedValue);
            }
        }

        protected void zip_campus_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(zip_campus.Text))
            {
                combo_zip(ddp_campus.SelectedValue, dde_campus.SelectedValue, ddd_campus.SelectedValue);
            }
        }

        protected void agregar_Campus_Click(object sender, EventArgs e)
        {
            c_campus.Value = null;
            n_campus.Value = null;
            a_campus.Value = null;
            RFC_campus.Value = null;
            zip_campus.Text = null;
            direc_campus.Value = null;
            estatus_campus.SelectedValue = "A";
            upd_btn_dir.Attributes.Add("style", "display:none");
            combo_pais();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "p_c", "loader_stop();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Nuevo_Campus", "nuevo_campus();", true);
        }

        protected void upd_btn_dir_Click(object sender, EventArgs e)
        {
            string test = hd_ddp_campus.Value;
            combo_estado(hd_ddp_campus.Value);
            dde_campus.SelectedValue = hd_dde_campus.Value;
            combo_delegacion(hd_ddp_campus.Value, hd_dde_campus.Value);
            ddd_campus.SelectedValue = hd_ddd_campus.Value;
            combo_zip(hd_ddp_campus.Value, hd_dde_campus.Value, hd_ddd_campus.Value);
            upd_btn_dir.Attributes.Add("style", "display:none");
        }

        protected void guardar_campus_Click(object sender, EventArgs e)
        {
            if(!String.IsNullOrEmpty(c_campus.Value) && !String.IsNullOrEmpty(n_campus.Value))
            {
                insertar_campus();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "validar_campos_campus();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Nuevo_Campus_Save", "nuevo_campus();", true);
            }
            
        }

        protected void actualizar_campus_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(c_campus.Value) && !String.IsNullOrEmpty(n_campus.Value))
            {
                actualizar_campus_db();
            }
            else
            {
                combo_estado(hd_ddp_campus.Value);
                dde_campus.SelectedValue = hd_dde_campus.Value;
                combo_delegacion(hd_ddp_campus.Value, hd_dde_campus.Value);
                ddd_campus.SelectedValue = hd_ddd_campus.Value;

                combo_zip(hd_ddp_campus.Value, hd_dde_campus.Value, hd_ddd_campus.Value);
                if (ddz_campus.Items.FindByValue(hd_ddz_campus.Value) != null)
                {
                    ddz_campus.SelectedValue = hd_ddz_campus.Value;
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ValidaCampos", "validar_campos_campus();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "MantieneModal", "update_campus();", true);
            }
        }

        #region Métodos para la pestaña de Programas

        protected void combo_campus()
        {
            try
            {
                search_campus.DataSource = serviceCat.QRY_TCAMP_COMBO();
                search_campus.DataValueField = "Clave";
                search_campus.DataTextField = "Campus";
                search_campus.DataBind();
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "combo_campus_programas", Session["usuario"]?.ToString() ?? "Sistema");
            }
        }

        protected void grid_programas_bind(string campus)
        {
            try
            {
                GridProgramas.DataSource = serviceCat.QRY_TCAPR_GRID(campus);
                GridProgramas.EditIndex = -1;
                GridProgramas.DataBind();
                if (GridProgramas.Rows.Count > 0)
                {
                    GridProgramas.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridProgramas.UseAccessibleHeader = true;
                }
                
                GridProgramas.Visible = true;
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "grid_programas_bind", Session["usuario"]?.ToString() ?? "Sistema");
            }
        }

        protected bool validar_clave_programa(string clave)
        {
            try
            {
                return serviceCat.ValidarClavePrograma(clave);
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "validar_clave_programa", Session["usuario"]?.ToString() ?? "Sistema");
                return false;
            }
        }

        protected void insertar_programa_c()
        {
            try
            {
               string admision = (Page.Request.Form["customSwitches"] == "on") ? "S" : "N";
                ModelInsProgCampusRequest request = new ModelInsProgCampusRequest
                {
                    p_campus = search_campus.SelectedValue,
                    p_programa = c_prog_campus.Text.Trim(),
                    p_admision = admision,
                    p_user = Session["usuario"]?.ToString() ?? "Sistema",
                    p_estatus = e_prog_campus.SelectedValue
                };
                serviceCat.InsertarProgramaCampus(request);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                grid_programas_bind(search_campus.SelectedValue);
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "insertar_programa_c", Session["usuario"]?.ToString() ?? "Sistema");
            }
        }

        protected void actualizar_programa_c()
        {
            try
            {
                string admision = (Page.Request.Form["customSwitches"] == "on") ? "S" : "N";
                ModelInsProgCampusRequest request = new ModelInsProgCampusRequest
                {
                    p_campus = search_campus.SelectedValue,
                    p_programa = c_prog_campus.Text.Trim(),
                    p_admision = admision,
                    p_user = Session["usuario"]?.ToString() ?? "Sistema",
                    p_estatus = e_prog_campus.SelectedValue
                };
                serviceCat.ActualizarProgramaCampus(request);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "update_p", "update();", true);
                grid_programas_bind(search_campus.SelectedValue);
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "actualizar_programa_c", Session["usuario"]?.ToString() ?? "Sistema");
            }
        }

        protected void search_campus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (search_campus.SelectedValue != "0")
                {
                    c_prog_campus.Text = string.Empty;
                    n_prog_campus.Text = string.Empty;
                    grid_programas_bind(search_campus.SelectedValue);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "mostrar_btn", "btn_programa();", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "btn_ocultar", "btn_programa_ocultar();", true);
                    GridProgramas.Visible = false;
                }
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "search_campus_changed", Session["usuario"]?.ToString() ?? "Sistema");
            }
        }
        protected void c_prog_campus_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string clave = c_prog_campus.Text.Trim();
                if (!validar_clave_programa(clave))
                {
                    n_prog_campus.Text = serviceCat.ObtenerNombrePrograma(clave);
                }
                else
                {
                    n_prog_campus.Text = string.Empty;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "validar_clave", "validarclavePrograma_N('ContentPlaceHolder1_c_prog_campus',1);", true);
                }
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "c_prog_campus_changed", Session["usuario"]?.ToString() ?? "Sistema");
            }
        }

        protected void guardar_prog_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(c_prog_campus.Text.Trim()))
                {
                    insertar_programa_c();
                }
                else
                {
                    grid_programas_bind(search_campus.SelectedValue);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "mostrar_btn", "btn_programa();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "validar_clave", "validarclavePrograma('ContentPlaceHolder1_c_prog_campus');", true);
                }
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "guardar_prog_click", Session["usuario"]?.ToString() ?? "Sistema");
            }
        }

        protected void update_prog_Click(object sender, EventArgs e)
        {
            try
            {
                actualizar_programa_c();
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "update_prog_click", Session["usuario"]?.ToString() ?? "Sistema");
            }
        }

        protected void cancelar_prog_Click(object sender, EventArgs e)
        {
            try
            {
                c_prog_campus.Text = string.Empty;
                n_prog_campus.Text = string.Empty;
                grid_programas_bind(search_campus.SelectedValue);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "reset_btn", "btn_programa();", true);
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "cancelar_prog_click", Session["usuario"]?.ToString() ?? "Sistema");
            }
        }

        #endregion

        #region Métodos para la pestaña de cobranza


        protected void combo_campus_cobranza()
        {
            try
            {
                cobranza_n.Items.Clear();
                cobranza_n.Items.Add(new ListItem("----Selecciona un Nivel----", "0"));
                cobranza_c.DataSource = serviceCat.QRY_TCAMP_COMBO();
                cobranza_c.DataValueField = "Clave";
                cobranza_c.DataTextField = "Campus";
                cobranza_c.DataBind();
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "combo_campus_cobranza", Session["usuario"]?.ToString() ?? "Sistema");
            }
        }
        
        protected void combo_nivel_cobranza(string campus)
        {
            try
            {
                cobranza_n.Items.Clear();
                cobranza_n.DataSource = serviceCat.QRY_TNIVE_COBRANZA(campus);
                cobranza_n.DataValueField = "clave";
                cobranza_n.DataTextField = "nivel";
                cobranza_n.DataBind();
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "combo_nivel_cobranza", Session["usuario"]?.ToString() ?? "Sistema");
            }
        }

        protected void combo_tipo_periodo()
        {
            try
            {
                cobranza_tipo_p.Items.Clear();
                cobranza_tipo_p.DataSource = serviceCat.QRY_TIPO_PERIODO_COBRANZA();
                cobranza_tipo_p.DataValueField = "clave";
                cobranza_tipo_p.DataTextField = "tipo_per";
                cobranza_tipo_p.DataBind();
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "combo_tipo_periodo_cob", Session["usuario"]?.ToString() ?? "Sistema");
            }
        }
        
        protected void combo_periodos()
        {
            try
            {
                // Asignación Directa: Pasamos el texto del buscador al servicio
                dd_periodo.DataSource = serviceCat.QRY_TPEES_PERIODOS(cobranza_p.Text.Trim());
                dd_periodo.DataValueField = "Clave";
                dd_periodo.DataTextField = "Periodo";
                dd_periodo.DataBind();
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "combo_periodos_cob", Session["usuario"]?.ToString() ?? "Sistema");
            }
        }
        protected void combo_concepto_calendario()
        {
            try
            {
                cobranza_conc_cal.DataSource = serviceCat.QRY_TCOCA_COMBO();
                cobranza_conc_cal.DataValueField = "Clave";
                cobranza_conc_cal.DataTextField = "Concepto_Cal";
                cobranza_conc_cal.DataBind();
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "combo_concepto_cal", Session["usuario"]?.ToString() ?? "Sistema");
            }
        } 
        protected void combo_concepto_cobranza()
        {
            try
            {
                cobranza_concepto.DataSource = serviceCat.QRY_TCOCO_COMBO();
                cobranza_concepto.DataValueField = "Clave";
                cobranza_concepto.DataTextField = "Concepto_Cob";
                cobranza_concepto.DataBind();
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "combo_concepto_cob", Session["usuario"]?.ToString() ?? "Sistema");
            }
        }

        protected void insertar_parametros_c()
        {
            try
            {
                ModelInsCobranzaRequest request = new ModelInsCobranzaRequest
                {
                    p_periodo = cobranza_p.Text.Trim(),
                    p_campus = cobranza_c.SelectedValue,
                    p_nivel = cobranza_n.SelectedValue,
                    p_tipo_per = cobranza_tipo_p.SelectedValue,
                    p_desc_ins = string.IsNullOrEmpty(descuento_ins.Text) ? 0 : Convert.ToDecimal(descuento_ins.Text),
                    p_desc_col = string.IsNullOrEmpty(descuento_col.Text) ? 0 : Convert.ToDecimal(descuento_col.Text),
                    p_conc_cal = cobranza_conc_cal.SelectedValue,
                    p_concepto = cobranza_concepto.SelectedValue,
                    p_user = Session["usuario"]?.ToString() ?? "Sistema"
                };
                serviceCat.InsertarParametrosCobranza(request);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                grid_cobranza_bind(); 
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "insertar_parametros_cob", Session["usuario"]?.ToString() ?? "Sistema");
            }
        }

        protected void actualizar_parametros_c()
        {
            try
            {
                ModelInsCobranzaRequest request = new ModelInsCobranzaRequest
                {
                    p_periodo = cobranza_p.Text.Trim(),
                    p_campus = cobranza_c.SelectedValue,
                    p_nivel = cobranza_n.SelectedValue,
                    p_tipo_per = cobranza_tipo_p.SelectedValue,
                    p_desc_ins = string.IsNullOrEmpty(descuento_ins.Text) ? 0 : Convert.ToDecimal(descuento_ins.Text),
                    p_desc_col = string.IsNullOrEmpty(descuento_col.Text) ? 0 : Convert.ToDecimal(descuento_col.Text),
                    p_conc_cal = cobranza_conc_cal.SelectedValue,
                    p_concepto = cobranza_concepto.SelectedValue,
                    p_user = Session["usuario"]?.ToString() ?? "Sistema"
                };
                serviceCat.ActualizarParametrosCobranza(request);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "update_p", "update();", true);
                
                grid_cobranza_bind();
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "actualizar_parametros_cob", Session["usuario"]?.ToString() ?? "Sistema");
            }
        }

        protected void grid_cobranza_bind()
        {
            try
            {
                GridCobranza.DataSource = serviceCat.QRY_TPACO_GRID(
                    cobranza_p.Text.Trim(),
                    cobranza_c.SelectedValue,
                    cobranza_n.SelectedValue,
                    cobranza_tipo_p.SelectedValue
                );
                GridCobranza.EditIndex = -1;
                GridCobranza.DataBind();
                if (GridCobranza.Rows.Count > 0)
                {
                    GridCobranza.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridCobranza.UseAccessibleHeader = true;
                }
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "grid_cobranza_bind", Session["usuario"]?.ToString() ?? "Sistema");
            }
        }
        protected void cobranza_c_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                combo_nivel_cobranza(cobranza_c.SelectedValue);
                if (!String.IsNullOrEmpty(cobranza_p.Text.Trim()))
                {
                    grid_cobranza_bind();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_cobranza", "load_datatable_cobranza();", true);
                }

                actualizar_cob.Attributes.Add("style", "display:none");
                guardar_cob.Attributes.Add("style", "display:initial");
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "cobranza_c_changed", Session["usuario"]?.ToString() ?? "Sistema");
            }
        }
        protected void dd_periodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dd_term.Attributes.Add("style", "display:none");
                term_text.Attributes.Add("style", "display:initial");
                cobranza_p.Text = dd_periodo.SelectedValue;
                if (dd_periodo.SelectedValue != "0")
                {
                    grid_cobranza_bind();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_cobranza", "load_datatable_cobranza();", true);
                }
                actualizar_cob.Attributes.Add("style", "display:none");
                guardar_cob.Attributes.Add("style", "display:initial");
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "dd_periodo_changed", Session["usuario"]?.ToString() ?? "Sistema");
            }
        }

        protected void cobranza_p_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(cobranza_p.Text.Trim()))
                {
                    if (!valida_periodo(cobranza_p.Text))
                    {
                        grid_cobranza_bind();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_cobranza", "load_datatable_cobranza();", true);
                    }
                    else
                    {
                        dd_term.Attributes.Add("style", "display:initial");
                        term_text.Attributes.Add("style", "display:none");
                        combo_periodos();
                    }
                }
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "cobranza_p_TextChanged", Session["usuario"]?.ToString() ?? "Sistema");
            }
        }

        protected void search_term_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                dd_term.Attributes.Add("style", "display:initial");
                term_text.Attributes.Add("style", "display:none");
                combo_periodos();
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "search_term_click", Session["usuario"]?.ToString() ?? "Sistema");
            }
        }

        protected bool valida_periodo(string periodo)
        {
            try
            {
                return serviceCat.ValidarPeriodoExiste(periodo.Trim());
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "valida_periodo", Session["usuario"]?.ToString() ?? "Sistema");
                return false; 
            }
        }

        protected bool valida_tipo_peri()
        {
            try
            {
                return serviceCat.ValidarConfiguracionCobranzaExiste(
                    cobranza_p.Text.Trim(),
                    cobranza_c.SelectedValue,
                    cobranza_n.SelectedValue,
                    cobranza_tipo_p.SelectedValue
                );
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "valida_tipo_peri", Session["usuario"]?.ToString() ?? "Sistema");
                return false; 
            }
        }

        protected void guardar_cob_Click(object sender, EventArgs e)
        {
            try
            {
                if (valida_tipo_peri())
                {
                    if (!String.IsNullOrEmpty(cobranza_p.Text.Trim()) && 
                        cobranza_c.SelectedValue != "0" && 
                        cobranza_n.SelectedValue != "0" && 
                        cobranza_tipo_p.SelectedValue != "0")
                    {
                        insertar_parametros_c();
                        
                        lbl_error.Visible = false;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "validar_campos_cob", "validar_campos_cobranza();", true);
                    }
                }
                else
                {
                    lbl_error_text.Text = "Esta configuración de cobranza ya se encuentra registrada para el periodo seleccionado.";
                    lbl_error.Visible = true;
                }
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "guardar_cob_click", Session["usuario"]?.ToString() ?? "Sistema");
            }
        }

        protected void btn_oculto_Click(object sender, EventArgs e)
        {
            try
            {
                cobranza_c.SelectedValue = campus.Text;
                combo_nivel_cobranza(cobranza_c.SelectedValue);                
                cobranza_n.SelectedValue = nivel.Text;
                cobranza_tipo_p.SelectedValue = t_periodo.Text;
                cobranza_p.Text = periodo_txt.Text;
                cobranza_conc_cal.SelectedValue = conce_cal_txt.Text;
                cobranza_concepto.SelectedValue = conce_cob_txt.Text;
                descuento_ins.Text = desc_ins.Text;
                descuento_col.Text = desc_col.Text;
                grid_cobranza_bind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_cobranza", "load_datatable_cobranza();", true);
                actualizar_cob.Attributes.Add("style", "display:initial");
                guardar_cob.Attributes.Add("style", "display:none");
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "btn_oculto_cobranza", Session["usuario"]?.ToString() ?? "Sistema");
            }
        }

        protected void actualizar_cob_Click(object sender, EventArgs e)
        {
            try
            {
                actualizar_parametros_c();
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "actualizar_cob_click", Session["usuario"]?.ToString() ?? "Sistema");
            }
        }

        #endregion

    }
}