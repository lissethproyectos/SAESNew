using System.Data;

namespace SAES_v1
{
    public partial class C_Demograficos : System.Web.UI.Page
    {
        Utilidades util = new Utilidades();
        Catalogos_grales_Service serviceCat = new Catalogos_grales_Service();

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
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "show_tab", "show_pais();", true);
                    OcultarFormularios();                    
                    combo_estatus();
                    combo_pais();
                    combo_paises_deleg();
                    combo_paises_zip();
                }
                
                grid_paises_bind();
                grid_estados_bind();
                grid_delegaciones_bind();
                grid_zip_bind();
            }
        }

        private void OcultarFormularios()
        {
            string hide = "display:none";
            form_pais.Attributes.Add("style", hide);
            form_estado.Attributes.Add("style", hide);
            form_delegacion.Attributes.Add("style", hide);
            form_zip.Attributes.Add("style", hide);
            btn_pais.Attributes.Add("style", hide);
            btn_estado.Attributes.Add("style", hide);
            btn_delegacion.Attributes.Add("style", hide);
            btn_zip.Attributes.Add("style", hide);
        }

        protected void combo_estatus()
        {
            ListItem[] items = { new ListItem("Activo", "A"), new ListItem("Inactivo", "B") };
            estatus_pais.Items.Clear();
            estatus_estado.Items.Clear();
            e_deleg.Items.Clear();
            e_zip.Items.Clear();
            estatus_pais.Items.AddRange(items);
            estatus_estado.Items.AddRange(items);
            e_deleg.Items.AddRange(items);
            e_zip.Items.AddRange(items);
        }

        #region Metodos para la pestaña de paises

        protected void grid_paises_bind()
        {
            try
            {
                GridPaises.DataSource = serviceCat.QRY_TPAIS();
                GridPaises.DataBind();
                if (GridPaises.Rows.Count > 0)
                {
                    GridPaises.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridPaises.UseAccessibleHeader = true;
                }
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tpais", Session["usuario"]?.ToString() ?? "Sistema");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void insertar_pais()
        {
            try
            {
                string respuesta = serviceCat.Ins_Tpais(
                    c_pais.Text.Trim(), 
                    n_pais.Text.Trim(), 
                    g_pais.Text.Trim(), 
                    Session["usuario"].ToString(), 
                    estatus_pais.SelectedValue
                );

                ScriptManager.RegisterStartupScript(this, this.GetType(), "save_p", "save();", true);
                grid_paises_bind(); 
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tpais_ins", Session["usuario"].ToString());
                
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }
        

        protected void actualiza_pais()
        {
            try
            {
                string respuesta = serviceCat.Upd_Tpais(
                    c_pais.Text.Trim(),
                    n_pais.Text.Trim(),
                    g_pais.Text.Trim(),
                    Session["usuario"].ToString(),
                    estatus_pais.SelectedValue
                );

                ScriptManager.RegisterStartupScript(this, this.GetType(), "update_p", "update();", true);
                grid_paises_bind();
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tpais_upd", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

       protected bool valida_clave(string clave)
        {
            try
            {
                return serviceCat.ValidaClavePais(clave.Trim());
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tpais_val", Session["usuario"].ToString());
                return false; 
            }
        }

        protected void save_pais_Click(object sender, EventArgs e)
        {
            try
            {
                c_pais.CssClass = "form-control";
                n_pais.CssClass = "form-control";
                if (!String.IsNullOrEmpty(c_pais.Text.Trim()) && !String.IsNullOrEmpty(n_pais.Text.Trim()))
                {
                    if (valida_clave(c_pais.Text.Trim()))
                    {
                        insertar_pais();
                    }
                    else
                    {
                        c_pais.CssClass = "form-control focus";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "duplicado", "duplicado();", true);
                    }
                }
                else
                {
                    if (String.IsNullOrEmpty(c_pais.Text.Trim()) && String.IsNullOrEmpty(n_pais.Text.Trim()))
                    {
                        c_pais.CssClass = "form-control focus";
                        n_pais.CssClass = "form-control focus";
                    }
                    else if (String.IsNullOrEmpty(c_pais.Text.Trim()))
                    {
                        c_pais.CssClass = "form-control focus";
                    }
                    else
                    {
                        n_pais.CssClass = "form-control focus";
                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "incompletos_n", "incompletos('pais_n');", true);
                }
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tpais_save_btn", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void update_pais_Click(object sender, EventArgs e)
        {
            try
            {
                n_pais.CssClass = "form-control";

                if (!String.IsNullOrEmpty(c_pais.Text.Trim()) && !String.IsNullOrEmpty(n_pais.Text.Trim()))
                {
                    actualiza_pais();
                }
                else
                {
                    n_pais.CssClass = "form-control focus";
                    
                    c_pais.Attributes.Add("disabled", "disabled");
                    
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "incompletos_u", "incompletos('pais_u');", true);
                }
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tpais_update_btn", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }
    
        #endregion


        #region Métodos para la pestaña estados

        protected void grid_estados_bind()
        {
            try
            {
                GridEstados.DataSource = serviceCat.QRY_TESTA(cbo_pais.SelectedValue);
                GridEstados.DataBind();
                if (GridEstados.Rows.Count > 0)
                {
                    GridEstados.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridEstados.UseAccessibleHeader = true;
                }
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "testa_bind", Session["usuario"]?.ToString() ?? "Sistema");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void combo_pais()
        {
            try
            {
                cbo_pais.Items.Clear();
                DataTable dtPaises = serviceCat.QRY_TPAIS_COMBO();
                util.BeginDropdownList(cbo_pais, dtPaises, "clave", "nombre");
                cbo_pais.Items.Insert(0, new ListItem("----Selecciona un país----", "0"));
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tpais_combo", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
            finally
            {
                ConexionMySql.Close();
            }
    }

        protected void insertar_estado()
        {
            if (valida_clave_edo(c_estado.Text.Trim()))
            {
                try
                {
                    string respuesta = serviceCat.Ins_Testa(
                        c_estado.Text.Trim(),
                        cbo_pais.SelectedValue,
                        n_estado.Text.Trim(),
                        Session["usuario"].ToString(),
                        estatus_estado.SelectedValue
                    );
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar_e", "save();", true);
                    grid_estados_bind();
                }
                catch (Exception ex)
                {
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "testa_ins", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Valida_clave_e", "valida('estado_n');", true);
            }
        }

        protected void actualizar_estado()
        {
            try
            {
                string respuesta = serviceCat.Upd_Testa(
                    c_estado.Text.Trim(),
                    cbo_pais.SelectedValue,
                    n_estado.Text.Trim(),
                    Session["usuario"].ToString(),
                    estatus_estado.SelectedValue
                );

                ScriptManager.RegisterStartupScript(this, this.GetType(), "update_e", "update();", true);
                grid_estados_bind(); 
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "testa_upd", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }
        protected bool valida_clave_edo(string clave)
        {
            try
            {
                return serviceCat.ValidaClaveEstado(clave.Trim(), cbo_pais.SelectedValue);
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "testa_val", Session["usuario"].ToString());
                return false; 
            }
        }

        protected void save_estado_Click(object sender, EventArgs e)
        {
            try
            {
                cbo_pais.CssClass = "form-control";
                c_estado.CssClass = "form-control";
                n_estado.CssClass = "form-control";
                if (cbo_pais.SelectedValue != "0" && !String.IsNullOrEmpty(c_estado.Text.Trim()) && !String.IsNullOrEmpty(n_estado.Text.Trim()))
                {
                    insertar_estado();
                }
                else
                {
                    if (cbo_pais.SelectedValue == "0" && String.IsNullOrEmpty(c_estado.Text.Trim()) && String.IsNullOrEmpty(n_estado.Text.Trim()))
                    {
                        cbo_pais.CssClass = "form-control focus";
                        c_estado.CssClass = "form-control focus";
                        n_estado.CssClass = "form-control focus";
                    }
                    else if (cbo_pais.SelectedValue == "0")
                    {
                        cbo_pais.CssClass = "form-control focus";
                    }
                    else if (String.IsNullOrEmpty(c_estado.Text.Trim()))
                    {
                        c_estado.CssClass = "form-control focus";
                    }
                    else
                    {
                        n_estado.CssClass = "form-control focus";
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "incompletos_n_e", "incompletos('estado_n');", true);
                }
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "testa_save_btn", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void update_estado_Click(object sender, EventArgs e)
        {
            try
            {
                n_estado.CssClass = "form-control";
                if (cbo_pais.SelectedValue != "0" && !String.IsNullOrEmpty(c_estado.Text.Trim()) && !String.IsNullOrEmpty(n_estado.Text.Trim()))
                {
                    actualizar_estado();
                }
                else
                {
                    n_estado.CssClass = "form-control focus";
                    cbo_pais.Attributes.Add("disabled", "disabled");
                    c_estado.Attributes.Add("disabled", "disabled");

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "incompletos_u_e", "incompletos('estado_u');", true);
                }
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "testa_update_btn", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void agregar_estado_Click(object sender, EventArgs e)
        {
            try
            {
                cbo_pais.CssClass = "form-control";
                c_estado.CssClass = "form-control";
                n_estado.CssClass = "form-control";
                c_estado.Attributes.Remove("disabled");
                cbo_pais.Attributes.Remove("disabled");
                c_estado.Text = "";
                n_estado.Text = "";
                cbo_pais.SelectedValue = "0";
                estatus_estado.SelectedValue = "A";
                update_estado.Attributes.Add("style", "display:none");
                save_estado.Attributes.Add("style", "display:initial");

                ScriptManager.RegisterStartupScript(this, this.GetType(), "e", "loader_stop();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Nuevo_Estado", "nuevo_estado();", true);
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "testa_nuevo_btn", Session["usuario"].ToString());
            }
        }

        #endregion

        #region Métodos para la pestaña delegación

        protected void combo_paises_deleg()
        {
            try
            {
                cbop_deleg.Items.Clear();
                cboe_deleg.Items.Clear();
                cboe_deleg.Items.Add(new ListItem("----Selecciona un estado----", "0"));
                DataTable dtPaises = serviceCat.QRY_TPAIS_COMBO();
                util.BeginDropdownList(cbop_deleg, dtPaises, "clave", "nombre");
                cbop_deleg.Items.Insert(0, new ListItem("----Selecciona un país----", "0"));
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tpais_deleg_combo", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void cbop_deleg_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbop_deleg.SelectedValue == "0")
                {
                    combo_paises_deleg();
                }
                else
                {
                    combo_estados_deleg();
                }
                add_delegacion.Attributes.Add("style", "display:none");
                form_delegacion.Attributes.Add("style", "display:block");
                btn_delegacion.Attributes.Remove("style"); // Muestra los botones de acción
                update_deleg.Attributes.Add("style", "display:none");
                save_deleg.Attributes.Add("style", "display:initial");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "p", "loader_stop();", true);
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "cbop_deleg_index", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void combo_estados_deleg()
        {
            try
            {
                cboe_deleg.Items.Clear();
                DataTable dtEstados = serviceCat.QRY_TESTA_POR_PAIS(cbop_deleg.SelectedValue);
                util.BeginDropdownList(cboe_deleg, dtEstados, "clave", "nombre");
                cboe_deleg.Items.Insert(0, new ListItem("----Selecciona un estado----", "0"));
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "testa_deleg_combo", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void grid_delegaciones_bind()
        {
            try
            {
                GridDelegacion.DataSource = serviceCat.QRY_TDELE(cbop_deleg.SelectedValue, cboe_deleg.SelectedValue);
                GridDelegacion.DataBind();
                if (GridDelegacion.Rows.Count > 0)
                {
                    GridDelegacion.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridDelegacion.UseAccessibleHeader = true;
                }
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tdele_bind", Session["usuario"]?.ToString() ?? "Sistema");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }
        
        protected void insertar_delegacion()
        {
            if (valida_clave_dele(c_deleg.Text.Trim()))
            {
                try
                {
                    string respuesta = serviceCat.Ins_Tdele(
                        c_deleg.Text.Trim(),
                        n_deleg.Text.Trim(),
                        cboe_deleg.SelectedValue,
                        cbop_deleg.SelectedValue,
                        Session["usuario"].ToString(),
                        e_deleg.SelectedValue
                    );
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar_d", "save();", true);
                    grid_delegaciones_bind(); 
                }
                catch (Exception ex)
                {
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tdele_ins", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Valida_clave_d", "valida('delegacion_n');", true);
            }
        }

        protected void actualizar_delegacion()
        {
            try
            {
                string respuesta = serviceCat.Upd_Tdele(
                    c_deleg.Text.Trim(),
                    n_deleg.Text.Trim(),
                    state_deleg.Value, 
                    cbop_deleg.SelectedValue,
                    Session["usuario"].ToString(),
                    e_deleg.SelectedValue
                );

                ScriptManager.RegisterStartupScript(this, this.GetType(), "update_d", "update();", true);
                grid_delegaciones_bind(); 
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tdele_upd", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected bool valida_clave_dele(string clave)
        {
            try
            {
                return serviceCat.ValidaClaveDelegacion(clave.Trim(), cboe_deleg.SelectedValue, cbop_deleg.SelectedValue);
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tdele_val", Session["usuario"].ToString());
                return false; 
            }
        }

        protected void save_deleg_Click(object sender, EventArgs e)
        {
            try
            {
                cbop_deleg.CssClass = "form-control";
                cboe_deleg.CssClass = "form-control";
                c_deleg.CssClass = "form-control";
                n_deleg.CssClass = "form-control";

                if (cbop_deleg.SelectedValue != "0" && cboe_deleg.SelectedValue != "0" && 
                    !String.IsNullOrEmpty(c_deleg.Text.Trim()) && !String.IsNullOrEmpty(n_deleg.Text.Trim()))
                {
                    insertar_delegacion();
                }
                else
                {
                    if (cbop_deleg.SelectedValue == "0")
                    {
                        cbop_deleg.CssClass = "form-control focus";
                    }
                    if (cboe_deleg.SelectedValue == "0")
                    {
                        cboe_deleg.CssClass = "form-control focus";
                    }
                    if (String.IsNullOrEmpty(c_deleg.Text.Trim()))
                    {
                        c_deleg.CssClass = "form-control focus";
                    }
                    if (String.IsNullOrEmpty(n_deleg.Text.Trim()))
                    {
                        n_deleg.CssClass = "form-control focus";
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "incompletos_n_d", "incompletos('delegacion_n');", true);
                }
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tdele_save_btn", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void update_deleg_Click(object sender, EventArgs e)
        {
            try
            {
                n_deleg.CssClass = "form-control";
                if (cbop_deleg.SelectedValue != "0" && state_deleg.Value != "0" && 
                    !String.IsNullOrEmpty(c_deleg.Text.Trim()) && !String.IsNullOrEmpty(n_deleg.Text.Trim()))
                {
                    actualizar_delegacion();
                }
                else
                {
                    combo_estados_deleg();
                    cboe_deleg.SelectedValue = state_deleg.Value;                    
                    n_deleg.CssClass = "form-control focus";
                    cbop_deleg.Attributes.Add("disabled", "disabled");
                    cboe_deleg.Attributes.Add("disabled", "disabled");
                    c_deleg.Attributes.Add("disabled", "disabled");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "incompletos_u_d", "incompletos('delegacion_u');", true);
                }
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tdele_update_btn", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void agregar_delegacion_Click(object sender, EventArgs e)
        {
            try
            {
                cbop_deleg.CssClass = "form-control";
                cboe_deleg.CssClass = "form-control";
                c_deleg.CssClass = "form-control";
                n_deleg.CssClass = "form-control";
                c_deleg.Attributes.Remove("disabled");
                cbop_deleg.Attributes.Remove("disabled");
                cboe_deleg.Attributes.Remove("disabled");
                combo_paises_deleg();
                c_deleg.Text = "";
                n_deleg.Text = "";
                e_deleg.SelectedValue = "A"; 
                update_deleg.Attributes.Add("style", "display:none");
                save_deleg.Attributes.Add("style", "display:initial");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "d", "loader_stop();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Nuevo_Delegacion", "nuevo_delegacion();", true);
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tdele_nuevo_btn", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        #endregion

        #region Metodos para la pestaña de Codigo Postal

        protected void combo_paises_zip()
        {
            try
            {
                cbop_zip.Items.Clear();
                cboe_zip.Items.Clear();
                cboe_zip.Items.Add(new ListItem("----Selecciona un estado----", "0"));
                cbod_zip.Items.Clear();
                cbod_zip.Items.Add(new ListItem("----Selecciona una delegación---", "0"));
                DataTable dtPaises = serviceCat.QRY_TPAIS_COMBO();
                util.BeginDropdownList(cbop_zip, dtPaises, "clave", "nombre");
                cbop_zip.Items.Insert(0, new ListItem("----Selecciona un país----", "0"));
            }
            catch (Exception ex)
            {
                Global.inserta_log(ex.Message.Replace("'", "-"), "tpais_zip_combo", Session["usuario"].ToString());
            }
        }

        protected void combo_estados_zip()
        {
            try
            {
                cboe_zip.Items.Clear();
                cbod_zip.Items.Clear();
                cbod_zip.Items.Add(new ListItem("----Selecciona una delegación---", "0"));

                DataTable dtEstados = serviceCat.QRY_TESTA_POR_PAIS(cbop_zip.SelectedValue);
                util.BeginDropdownList(cboe_zip, dtEstados, "clave", "nombre");
                cboe_zip.Items.Insert(0, new ListItem("----Selecciona un estado----", "0"));
            }
            catch (Exception ex)
            {
                Global.inserta_log(ex.Message.Replace("'", "-"), "testa_zip_combo", Session["usuario"].ToString());
            }
        }

        protected void combo_delegacion_zip()
        {
            try
            {
                cbod_zip.Items.Clear();
                DataTable dtDeleg = serviceCat.QRY_TDELE_COMBO(cbop_zip.SelectedValue, cboe_zip.SelectedValue);
                util.BeginDropdownList(cbod_zip, dtDeleg, "clave", "nombre");
                cbod_zip.Items.Insert(0, new ListItem("----Selecciona una delegación----", "0"));
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tdele_zip_combo", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void grid_zip_bind()
        {
            try
            {   
                GridZip.DataSource = serviceCat.QRY_TZIPS(cbop_zip.SelectedValue, cboe_zip.SelectedValue, cbod_zip.SelectedValue);
                GridZip.DataBind();
                if (GridZip.Rows.Count > 0)
                {
                    GridZip.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridZip.UseAccessibleHeader = true;
                }
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");                
                Global.inserta_log(mensaje_error, "tzip_bind", Session["usuario"]?.ToString() ?? "Sistema");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void agregar_zip_Click(object sender, EventArgs e)
        {
            try
            {
                cbop_zip.CssClass = "form-control";
                cboe_zip.CssClass = "form-control";
                cbod_zip.ZipCssClass = "form-control"; 
                c_zip.CssClass = "form-control";
                n_zip.CssClass = "form-control";
                cbop_zip.Attributes.Remove("disabled");
                cboe_zip.Attributes.Remove("disabled");
                cbod_zip.Attributes.Remove("disabled");
                c_zip.Attributes.Remove("disabled");
                combo_paises_zip(); 
                c_zip.Text = "";
                n_zip.Text = "";
                e_zip.SelectedValue = "A";
                update_zip.Attributes.Add("style", "display:none");
                save_zip.Attributes.Add("style", "display:initial");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "z", "loader_stop();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Nuevo_Zip", "nuevo_zip();", true);
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tzip_nuevo_btn", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void cbop_zip_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbop_zip.SelectedValue == "0")
                {
                    combo_paises_zip();
                }
                else
                {
                    combo_estados_zip();
                }
                add_zip.Attributes.Add("style", "display:none; margin-top:15px;");
                form_zip.Attributes.Add("style", "display:Block");
                btn_zip.Attributes.Remove("style");
                update_zip.Attributes.Add("style", "display:none");
                save_zip.Attributes.Add("style", "display:initial");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "stop_l", "loader_stop();", true);
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "cbop_zip_index", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void cboe_zip_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                combo_delegacion_zip();
                add_zip.Attributes.Add("style", "display:none; margin-top:15px;");
                form_zip.Attributes.Add("style", "display:Block");
                btn_zip.Attributes.Remove("style");
                update_zip.Attributes.Add("style", "display:none");
                save_zip.Attributes.Add("style", "display:initial");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "stop_l_zip", "loader_stop();", true);
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "cboe_zip_index", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void insertar_zip()
        {
            if (valida_clave_zip(c_zip.Text.Trim()))
            {
                try
                {
                    string respuesta = serviceCat.Ins_Tzip(
                        c_zip.Text.Trim(),
                        n_zip.Text.Trim(),
                        cbop_zip.SelectedValue,
                        cboe_zip.SelectedValue,
                        cbod_zip.SelectedValue,
                        Session["usuario"].ToString(),
                        e_zip.SelectedValue
                    );
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar_z", "save();", true);
                    grid_zip_bind(); 
                }
                catch (Exception ex)
                {
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tzip_ins", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Valida_clave_z", "valida('zip_n');", true);
            }
        }
        protected void actualizar_zip()
        {
            try
            {
                string respuesta = serviceCat.Upd_Tzip(
                    c_zip.Text.Trim(),
                    n_zip.Text.Trim(),
                    cbop_zip.SelectedValue,
                    state_zip.Value,    
                    country_zip.Value, 
                    Session["usuario"].ToString(),
                    e_zip.SelectedValue
                );
                ScriptManager.RegisterStartupScript(this, this.GetType(), "update_Z", "update();", true);
                grid_zip_bind(); 
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tzip_upd", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }
        protected bool valida_clave_zip(string clave)
        {
            try
            {
                return serviceCat.ValidaClaveZip(
                    clave.Trim(), 
                    cbop_zip.SelectedValue, 
                    cboe_zip.SelectedValue, 
                    cbod_zip.SelectedValue
                );
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tzip_val", Session["usuario"].ToString());
                return false; 
            }
        }  
        protected void save_zip_Click(object sender, EventArgs e)
        {
            try
            {
                cbop_zip.CssClass = "form-control";
                cboe_zip.CssClass = "form-control";
                cbod_zip.CssClass = "form-control";
                c_zip.CssClass = "form-control";
                n_zip.CssClass = "form-control";
                if (cbop_zip.SelectedValue != "0" && 
                    cboe_zip.SelectedValue != "0" && 
                    cbod_zip.SelectedValue != "0" && 
                    !String.IsNullOrEmpty(c_zip.Text.Trim()) && 
                    !String.IsNullOrEmpty(n_zip.Text.Trim()))
                {
                    insertar_zip();
                }
                else
                {
                    if (cbop_zip.SelectedValue == "0") { cbop_zip.CssClass = "form-control focus"; }
                    if (cboe_zip.SelectedValue == "0") { cboe_zip.CssClass = "form-control focus"; }
                    if (cbod_zip.SelectedValue == "0") { cbod_zip.CssClass = "form-control focus"; }
                    if (String.IsNullOrEmpty(c_zip.Text.Trim())) { c_zip.CssClass = "form-control focus"; }
                    if (String.IsNullOrEmpty(n_zip.Text.Trim())) { n_zip.CssClass = "form-control focus"; }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "incompletos_n_z", "incompletos('zip_n');", true);
                }
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tzip_save_btn", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void update_zip_Click(object sender, EventArgs e)
        {
            try
            {
                 n_zip.CssClass = "form-control";
                if (cbop_zip.SelectedValue != "0" && state_zip.Value != "0" && country_zip.Value != "0" && 
                    !String.IsNullOrEmpty(c_zip.Text.Trim()) && !String.IsNullOrEmpty(n_zip.Text.Trim()))
                {
                    actualizar_zip();
                }
                else
                {
                    combo_estados_zip();
                    cboe_zip.SelectedValue = state_zip.Value;
                    combo_delegacion_zip();
                    cbod_zip.SelectedValue = country_zip.Value;
                    n_zip.CssClass = "form-control focus";
                    cbop_zip.Attributes.Add("disabled", "disabled");
                    cboe_zip.Attributes.Add("disabled", "disabled");
                    cbod_zip.Attributes.Add("disabled", "disabled");
                    c_zip.Attributes.Add("disabled", "disabled");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "incompletos_u_z", "incompletos('zip_u');", true);
                }
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tzip_update_btn", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        #endregion


    }
}