using MySql.Data.MySqlClient;
using SAES_DBO.Models;
using SAES_Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using static SAES_DBO.Models.ModelCobranza;
using static SAES_DBO.Models.ModelMenu;

namespace SAES_v1
{
    public partial class tpaco : System.Web.UI.Page
    {
        Catalogos serviceCatalogo = new Catalogos();
        CobranzaService serviceCobranza = new CobranzaService();
        UsuarioService serviceUsuario = new UsuarioService();
        MenuService servicePermiso = new MenuService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Response.Redirect(FormsAuthentication.DefaultUrl);
                Response.End();
            }
            else
            {
                LlenaPagina();
                if (!IsPostBack)
                {
                    combo_campus_cobranza();
                    combo_tipo_periodo();
                    combo_concepto_calendario();
                    combo_concepto_cobranza();
                    combo_periodos();

                }

            }
        }

        private void LlenaPagina()
        {
            ModelObtenerPermisoFormResponse objPermiso = new ModelObtenerPermisoFormResponse();
            try
            {
                objPermiso = servicePermiso.ObtenerPermisoFormulario(Session["usuario"].ToString(), "tpaco");
                if (objPermiso != null)
                {
                    if (objPermiso.usme_update == "0" || objPermiso.usme_select == "0")
                    {
                        cobranza_btn.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                    }
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);

            }
            catch (Exception ex)
            {
                ///logs
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tpaco", Session["usuario"].ToString());
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
        protected void combo_campus_cobranza()
        {

            try
            {
                cobranza_c.DataSource = serviceCatalogo.ObtenerCampus();
                cobranza_c.DataValueField = "Clave";
                cobranza_c.DataTextField = "Descripcion";
                cobranza_c.DataBind();
                cobranza_c_SelectedIndexChanged(null, null);

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tpaco", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }
        protected void combo_periodos()
        {
            try
            {
                dd_periodo.DataSource = serviceCatalogo.ObtenerPeriodosEscolares();
                dd_periodo.DataValueField = "clave";
                dd_periodo.DataTextField = "nombre";
                dd_periodo.DataBind();
            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tpaco", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }

        }

        protected void combo_tipo_periodo()
        {

            try
            {
                cobranza_tipo_p.DataSource = serviceCatalogo.obtenTipoPeriodo();
                cobranza_tipo_p.DataValueField = "Clave";
                cobranza_tipo_p.DataTextField = "Descripcion";
                cobranza_tipo_p.DataBind();
            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tpaco", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void combo_concepto_calendario()
        {
            try
            {
                cobranza_conc_cal.DataSource = serviceCatalogo.ObtenerComboComun("Periodos_Escolares", "");
                cobranza_conc_cal.DataValueField = "Clave";
                cobranza_conc_cal.DataTextField = "Descripcion";
                cobranza_conc_cal.DataBind();
            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tpaco", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void combo_concepto_cobranza()
        {
            try
            {
                //cobranza_concepto.DataSource = serviceCatalogo.ObtenerPeriodosEscolares("Conceptos_Cobranza", "");
                cobranza_concepto.DataSource = serviceCatalogo.ObtenerComboComun("Conceptos_Cobranza", "");
                cobranza_concepto.DataValueField = "Clave";
                cobranza_concepto.DataTextField = "Descripcion";
                cobranza_concepto.DataBind();
            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tpaco", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void grid_cobranza_bind()
        {
            try
            {
                GridCobranza.DataSource = serviceCobranza.ObtenerDatosCobranza(dd_periodo.SelectedValue, cobranza_c.SelectedValue, cobranza_n.SelectedValue, cobranza_tipo_p.SelectedValue);
                GridCobranza.DataBind();
            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tpaco", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }
        

        protected void cobranza_p_TextChanged(object sender, EventArgs e)
        {
            if (cobranza_p.Text != string.Empty)
            {
                if (!valida_periodo(cobranza_p.Text))
                {
                    grid_cobranza_bind();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_cobranza", "load_datatable_cobranza();", true);
                }
                else
                {
                    dd_term.Visible = true;

                    term_text.Visible = false;
                    combo_periodos();
                }
            }
        }

        protected void cobranza_c_SelectedIndexChanged(object sender, EventArgs e)
        {
            //combo_nivel_cobranza(cobranza_c.SelectedValue);
            cobranza_n.DataSource = serviceCatalogo.obtenNivel(cobranza_c.SelectedValue);
            cobranza_n.DataValueField = "Clave";
            cobranza_n.DataTextField = "Descripcion";
            cobranza_n.DataBind();
            ListItem itemToRemove = cobranza_n.Items.FindByValue("");
            if (itemToRemove != null)
            {
                cobranza_n.Items.Remove(itemToRemove);
            }

            if (!String.IsNullOrEmpty(cobranza_p.Text))
            {
                grid_cobranza_bind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_cobranza", "load_datatable_cobranza();", true);
            }

        }
        protected void cobranza_tipo_p_SelectedIndexChanged(object sender, EventArgs e)
        {           
           
            if (!String.IsNullOrEmpty(cobranza_p.Text))
            {
                grid_cobranza_bind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_cobranza", "load_datatable_cobranza();", true);
            }

        }


        protected void dd_periodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //dd_term.Visible = false;
            //term_text.Visible = true;
            cobranza_p.Text = dd_periodo.SelectedValue;
            if (dd_periodo.SelectedValue != "0")
            {
                grid_cobranza_bind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable_cobranza", "load_datatable_cobranza();", true);
            }

        }

        //protected void search_term_Click(object sender, ImageClickEventArgs e)
        //{
        //    dd_term.Visible = true;
        //    term_text.Visible = false;
        //    combo_periodos();
        //}

        protected bool valida_periodo(string periodo)
        {
            string Query = "";
            Query = "SELECT COUNT(*) Indicador FROM tpees WHERE tpees_clave='" + periodo + "'";
            MySqlCommand cmd = new MySqlCommand(Query);
            DataTable dt = GetData(cmd);
            if (dt.Rows[0]["Indicador"].ToString() != "0")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        protected void GridCobranza_SelectedIndexChanged(object sender, EventArgs e)
        {
            //combo_campus_cobranza();
            GridViewRow row = GridCobranza.SelectedRow;
            cobranza_c.SelectedValue = row.Cells[1].Text;
            //combo_nivel_cobranza(cobranza_c.SelectedValue);
            cobranza_n.SelectedValue = row.Cells[3].Text;
            //combo_tipo_periodo();
            dd_periodo.SelectedValue = row.Cells[13].Text;
            cobranza_tipo_p.SelectedValue = row.Cells[5].Text;

            //cobranza_p.Text = row.Cells[13].Text;
            //combo_concepto_calendario();
            cobranza_conc_cal.SelectedValue = row.Cells[9].Text;
            //combo_concepto_cobranza();
            cobranza_concepto.SelectedValue = row.Cells[11].Text;
            descuento_col.Text = row.Cells[8].Text;
            descuento_ins.Text = row.Cells[7].Text;
            guardar_cob.Visible = false;
            actualizar_cob.Visible = true;
            cancelar_cob.Visible = true;
            //grid_cobranza_bind();
        }

        protected void cancelar_cob_Click(object sender, EventArgs e)
        {
            combo_campus_cobranza();
            combo_concepto_cobranza();
            combo_concepto_calendario();
            combo_tipo_periodo();
            cobranza_p.Text = null;
            descuento_col.Text = null;
            descuento_ins.Text = null;
            guardar_cob.Visible = true;
            actualizar_cob.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
            //GridCobranza.Visible = false;
            GridCobranza.DataSource = null;
            GridCobranza.DataBind();
        }

        protected void guardar_cob_Click(object sender, EventArgs e)
        {
            ModelInsertarDatosCobranzaResponse objExisteRegistro = new ModelInsertarDatosCobranzaResponse();

            if (cobranza_c.SelectedValue != "0" && cobranza_n.SelectedValue != "0" && cobranza_tipo_p.SelectedValue != "0" && !String.IsNullOrEmpty(cobranza_p.Text) && cobranza_concepto.SelectedValue != "0" && cobranza_conc_cal.SelectedValue != "0" && !String.IsNullOrEmpty(descuento_col.Text) && !String.IsNullOrEmpty(descuento_ins.Text))
            {
                //if (valida_tipo_peri())
                //{
                double vale = 0;
                decimal resultado1 = 0; decimal resultado2 = 0;

                bool insc = Decimal.TryParse(descuento_ins.Text, out resultado1);
                bool cole = Decimal.TryParse(descuento_col.Text, out resultado2);


                string campus = cobranza_c.SelectedValue;
                string nivel = cobranza_n.SelectedValue;
                string periodo = cobranza_p.Text;
                string t_periodo = cobranza_tipo_p.SelectedValue;

                try
                {
                    //mysqlcmd.ExecuteNonQuery();
                    objExisteRegistro = serviceCobranza.InsertarDatosCobranza(dd_periodo.SelectedValue, cobranza_c.SelectedValue, cobranza_n.SelectedValue, cobranza_tipo_p.SelectedValue, descuento_ins.Text, descuento_col.Text, cobranza_conc_cal.SelectedValue, cobranza_concepto.SelectedValue, Session["usuario"].ToString());
                    if (objExisteRegistro != null)
                    {
                        if (objExisteRegistro.Existe == "0")
                        {

                            //combo_campus_cobranza();
                            //cobranza_p.Text = periodo;
                            //descuento_col.Text = null;
                            //descuento_ins.Text = null;
                            //combo_concepto_cobranza();
                            //combo_concepto_calendario();
                            //combo_tipo_periodo();
                            actualizar_cob.Visible = false;
                            guardar_cob.Visible = true;
                            ////combo_nivel_cobranza(campus);
                            //cobranza_c.SelectedValue = campus;
                            //cobranza_n.SelectedValue = nivel;
                            //cobranza_tipo_p.SelectedValue = t_periodo;
                            cobranza_tipo_p.SelectedIndex = 0;
                            cobranza_conc_cal.SelectedIndex = 0;
                            cobranza_concepto.SelectedIndex = 0;
                            descuento_ins.Text = null;
                            descuento_col.Text = null;


                            grid_cobranza_bind();
                            GridCobranza.SelectedIndex = -1;

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                        }
                        else
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "validartperiodo_cob", "validartperiodo_cob('ContentPlaceHolder1_cobranza_tipo_p',1);", true);

                    }
                }
                catch (Exception ex)
                {
                    string test = ex.Message;
                    string mensaje_error = ex.Message.Replace("'", "-");
                    Global.inserta_log(mensaje_error, "tpaco", Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                }

            }

            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "validar_campos_cob", "validar_campos_cobranza();", true);


        }

        //protected void actualizar_cob_Click(object sender, EventArgs e)
        //{
        //    if (cobranza_c.SelectedValue != "0" && cobranza_n.SelectedValue != "0" && cobranza_tipo_p.SelectedValue != "0" && !String.IsNullOrEmpty(cobranza_p.Text) && cobranza_concepto.SelectedValue != "0" && cobranza_conc_cal.SelectedValue != "0" && !String.IsNullOrEmpty(descuento_col.Text) && !String.IsNullOrEmpty(descuento_ins.Text))
        //    {
        //        double vale = 0;
        //        decimal resultado1 = 0; decimal resultado2 = 0;

        //        bool insc = Decimal.TryParse(descuento_ins.Text, out resultado1);
        //        bool cole = Decimal.TryParse(descuento_col.Text, out resultado2);

        //        if (insc && cole)
        //        {
        //            vale = 1;
        //            // Si llega hasta aquí, resultado es numérico.
        //        }
        //        else
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
        //            if (insc)
        //            {
        //                //
        //            }
        //            else
        //            {

        //                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "valida_desc_insc();", true);
        //                grid_cobranza_bind();
        //            }
        //            if (cole)
        //            {
        //                //
        //            }
        //            else
        //            {
        //                //ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
        //                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "valida_desc_cole();", true);
        //                grid_cobranza_bind();
        //            }
        //        }
        //        if (vale == 1)
        //        {
        //            string campus = cobranza_c.SelectedValue;
        //            string nivel = cobranza_n.SelectedValue;
        //            string periodo = cobranza_p.Text;
        //            string t_periodo = cobranza_tipo_p.SelectedValue;
        //            string Query = "UPDATE tpaco SET tpaco_pdesc_insc='" + descuento_ins.Text + "',tpaco_pdesc_parc='" + descuento_col.Text + "',tpaco_tcoca_clave='" + cobranza_conc_cal.Text + "',tpaco_tcoco_clave='" + cobranza_concepto.SelectedValue + "',tpaco_user='" + Session["usuario"].ToString() + "',tpaco_date= current_timestamp() WHERE tpaco_tpees_clave='" + cobranza_p.Text + "' AND tpaco_tcamp_clave='" + cobranza_c.SelectedValue + "' AND tpaco_tnive_clave='" + cobranza_n.Text + "' AND tpaco_clave='" + cobranza_tipo_p.SelectedValue + "'";
        //            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
        //            ConexionMySql.Open();
        //            MySqlCommand mysqlcmd = new MySqlCommand(Query, ConexionMySql);
        //            mysqlcmd.CommandType = CommandType.Text;
        //            try
        //            {
        //                mysqlcmd.ExecuteNonQuery();
        //                combo_campus_cobranza();
        //                cobranza_p.Text = periodo;
        //                descuento_col.Text = null;
        //                descuento_ins.Text = null;
        //                combo_concepto_cobranza();
        //                combo_concepto_calendario();
        //                combo_tipo_periodo();
        //                actualizar_cob.Visible = false;
        //                guardar_cob.Visible = true;
        //                combo_nivel_cobranza(campus);
        //                cobranza_c.SelectedValue = campus;
        //                cobranza_n.SelectedValue = nivel;
        //                cobranza_tipo_p.SelectedValue = t_periodo;
        //                grid_cobranza_bind();
        //                ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
        //                ScriptManager.RegisterStartupScript(this, this.GetType(), "update_p", "update();", true);
        //            }
        //            catch (Exception ex)
        //            {
        //                string test = ex.Message;
        //                string mensaje_error = ex.Message.Replace("'", "-");
        //                Global.inserta_log(mensaje_error, "tpaco", Session["usuario"].ToString());
        //                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
        //            }
        //            finally
        //            {
        //                ConexionMySql.Close();
        //            }
        //        }
        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "validar_campos_cob", "validar_campos_cobranza();", true);
        //    }
        //}
        protected void actualizar_cob_Click(object sender, EventArgs e)
        {
            ModelEditarDatosCobranzaResponse objExisteRegistro = new ModelEditarDatosCobranzaResponse();

            if (cobranza_c.SelectedValue != "0" && cobranza_n.SelectedValue != "0" && cobranza_tipo_p.SelectedValue != "0" && !String.IsNullOrEmpty(cobranza_p.Text) && cobranza_concepto.SelectedValue != "0" && cobranza_conc_cal.SelectedValue != "0" && !String.IsNullOrEmpty(descuento_col.Text) && !String.IsNullOrEmpty(descuento_ins.Text))
            {
                string campus = cobranza_c.SelectedValue;
                string nivel = cobranza_n.SelectedValue;
                string periodo = cobranza_p.Text;
                string t_periodo = cobranza_tipo_p.SelectedValue;

                objExisteRegistro = serviceCobranza.EditarDatosCobranza(dd_periodo.SelectedValue, cobranza_c.SelectedValue, cobranza_n.SelectedValue, cobranza_tipo_p.SelectedValue, descuento_ins.Text, descuento_col.Text, cobranza_conc_cal.SelectedValue, cobranza_concepto.SelectedValue);
                if (objExisteRegistro != null)
                {
                    if (objExisteRegistro.Existe == "0")
                    {

                        //combo_campus_cobranza();
                        //cobranza_p.Text = periodo;
                        //descuento_col.Text = null;
                        //descuento_ins.Text = null;
                        //combo_concepto_cobranza();
                        //combo_concepto_calendario();
                        //combo_tipo_periodo();
                        //actualizar_cob.Visible = false;
                        //guardar_cob.Visible = true;
                        //cancelar_cob.Visible = false;
                        ////combo_nivel_cobranza(campus);
                        //cobranza_c.SelectedValue = campus;
                        //cobranza_n.SelectedValue = nivel;
                        //cobranza_tipo_p.SelectedValue = t_periodo;
                        //grid_cobranza_bind();


                        actualizar_cob.Visible = false;
                        guardar_cob.Visible = true;
                        cobranza_tipo_p.SelectedIndex = 0;
                        cobranza_conc_cal.SelectedIndex = 0;
                        cobranza_concepto.SelectedIndex = 0;
                        descuento_ins.Text = null;
                        descuento_col.Text = null;
                        grid_cobranza_bind();
                        GridCobranza.SelectedIndex = -1;



                        ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "update_p", "update();", true);

                    }

                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "validar_campos_cob", "validar_campos_cobranza();", true);
                    }
                }
            }
        }

        protected void search_term_Click(object sender, EventArgs e)
        {
            dd_term.Visible = true;
            term_text.Visible = false;
            combo_periodos();
        }
    }
}