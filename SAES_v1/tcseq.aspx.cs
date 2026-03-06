using MySql.Data.MySqlClient;
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
using static SAES_DBO.Models.ModelMenu;

namespace SAES_v1
{
    public partial class tcseq : System.Web.UI.Page
    {
        Catalogos serviceCatalogo=new Catalogos();
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

                //c_prog_campus.Attributes.Add("onblur", "validarclavePrograma('ContentPlaceHolder1_c_prog_campus')");
                //c_prog_campus.Attributes.Add("oninput", "validarclavePrograma('ContentPlaceHolder1_c_prog_campus')");
                //LlenaPagina();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "menu", "carga_menu();", true);
                if (!IsPostBack)
                {
                    combo_campus();

                    LlenaPagina();
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);

            }
        }

        //protected void LlenaPagina()
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "menu", "carga_menu();", true);
        //    if (Session["usuario"].ToString() == "")
        //    {
        //        Response.Redirect("Default.aspx");
        //    }
        //    string QerySelect = "select tusme_update, tusme_select from tuser, tusme, tmede " +
        //                      " where tuser_clave = '" + Session["usuario"].ToString() + "'" +
        //                      " and tusme_trole_clave = tuser_trole_clave and tusme_tmenu_clave = 1 and tusme_tmenu_clave = tmede_tmenu_clave " +
        //                      " and tusme_tmede_clave = tmede_clave and tmede_forma='tcseq' ";

        //    MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
        //    conexion.Open();
        //    try
        //    {
        //        MySqlDataAdapter sqladapter = new MySqlDataAdapter();

        //        DataSet dssql1 = new DataSet();

        //        MySqlCommand commandsql1 = new MySqlCommand(QerySelect, conexion);
        //        sqladapter.SelectCommand = commandsql1;
        //        sqladapter.Fill(dssql1);
        //        sqladapter.Dispose();
        //        commandsql1.Dispose();

        //        if (dssql1.Tables[0].Rows[0][0].ToString() == "0")
        //        {
        //            btn_seq.Visible = false;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        ///logs
        //        string mensaje_error = ex.Message.Replace("'", "-");
        //        Global.inserta_log(mensaje_error, "tcseq", Session["usuario"].ToString());
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
        //    }
        //}

        private void LlenaPagina()
        {
            ModelObtenerPermisoFormResponse objPermiso = new ModelObtenerPermisoFormResponse();
            objPermiso.usme_update = "0";
            objPermiso.usme_select = "0";
            //update_pais.Visible = false;
            //GridPaises.Columns[0].Visible = false;

            System.Threading.Thread.Sleep(50);
            try
            {
                objPermiso = servicePermiso.ObtenerPermisoFormulario(Session["usuario"].ToString(), "tcseq");
                if (objPermiso != null)
                {
                    if (objPermiso.usme_update == "0" || objPermiso.usme_select == "0")
                    {
                        btn_seq.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                    }
                    else
                        grid_secuencia_bind();
                }
                else
                {
                    btn_seq.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "permisos", "sin_acceso();", true);
                }


            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcseq", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);

            }
        }

        protected void combo_campus()
        {                
            try
            {                
                search_campus.DataSource = serviceCatalogo.ObtenerCampus();
                search_campus.DataValueField = "Clave";
                search_campus.DataTextField = "Descripcion";
                search_campus.DataBind();

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcseq", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void grid_secuencia_bind()
        {            
            try
            {               
                GridSequence.DataSource = serviceCatalogo.obtenSecuencias(search_campus.SelectedValue);
                GridSequence.DataBind();
                GridSequence.DataMember = "Plan";
                //GridSequence.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridSequence.UseAccessibleHeader = true;
                

                //for (int i = 0; i < GridSequence.Rows.Count; i++)
                //{
                //    TextBox numero = (TextBox)GridSequence.Rows[i].FindControl("valor");
                //    TextBox largo = (TextBox)GridSequence.Rows[i].FindControl("longitud");

                //    numero.Text = ds1.Tables[0].Rows[i][2].ToString();
                //    largo.Text = ds1.Tables[0].Rows[i][3].ToString();

                //}
                
                GridSequence.Visible = true;

            }
            catch (Exception ex)
            {
                ///logs
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcseq", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }

        protected void search_campus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (search_campus.SelectedValue != "0")
            {
                grid_secuencia_bind();
            }
            else
            {
                GridSequence.Visible = false;
            }
            
        }

        protected void cancelar_seq_Click(object sender, EventArgs e)
        {
            grid_secuencia_bind();
        }

        protected void guardar_seq_Click(object sender, EventArgs e)
        {
            string indicador = null;
            double vale = 0;

            for (int i = 0; i < GridSequence.Rows.Count; i++)
            {
                TextBox numero = (TextBox)GridSequence.Rows[i].FindControl("valor");
                TextBox largo = (TextBox)GridSequence.Rows[i].FindControl("longitud");

                decimal resultado1 = 0; decimal resultado2 = 0;

                bool valor = Decimal.TryParse(numero.Text, out resultado1);
                bool longitud = Decimal.TryParse(largo.Text, out resultado2);

                if (valor && longitud && vale == 0)
                {
                    vale = 0;
                    // Si llega hasta aquí, resultado es numérico.
                }
                else
                {
                    vale = 1;

                }
            }

            if (vale == 0)
            {
                for (int i = 0; i < GridSequence.Rows.Count; i++)
                {
                    TextBox numero = (TextBox)GridSequence.Rows[i].FindControl("valor");
                    TextBox largo = (TextBox)GridSequence.Rows[i].FindControl("longitud");
                    if (!String.IsNullOrEmpty(numero.Text) && !String.IsNullOrEmpty(largo.Text))
                    {


                        string Query = "UPDATE tcseq SET tcseq_numero = '" + numero.Text + "', tcseq_longitud = '" + largo.Text + "', tcseq_date = current_timestamp(), tcseq_user = '" + Session["usuario"].ToString() + "' WHERE tcseq_tcamp_clave = '" + search_campus.SelectedValue + "' AND tcseq_tseqn_clave = '" + GridSequence.Rows[i].Cells[0].Text + "'";
                        MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                        ConexionMySql.Open();
                        MySqlCommand mysqlcmd = new MySqlCommand(Query, ConexionMySql);
                        mysqlcmd.CommandType = CommandType.Text;
                        try
                        {
                            mysqlcmd.ExecuteNonQuery();

                        }
                        catch (Exception ex)
                        {
                            ///logs
                            string mensaje_error = ex.Message.Replace("'", "-");
                            Global.inserta_log(mensaje_error, "tcseq", Session["usuario"].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion();", true);
                        }
                        indicador += "0";

                    }
                    else
                    {
                        GridSequence.HeaderRow.TableSection = TableRowSection.TableHeader;
                        GridSequence.UseAccessibleHeader = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "validarValor", "validarValor('ContentPlaceHolder1_GridSequence_valor_" + i + "');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "validarLongitud", "validarLongitud('ContentPlaceHolder1_GridSequence_longitud_" + i + "');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);
                        indicador += "1";
                    }

                }
                if (!indicador.Contains("1"))
                {
                    grid_secuencia_bind();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Guardar", "save();", true);
                }

            }
            else
            {
                grid_secuencia_bind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "error();", true);
            }
            
        }
    }
}