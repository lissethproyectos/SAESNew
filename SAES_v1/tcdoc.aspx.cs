using MySql.Data.MySqlClient;
using SAES_Services;
using SAES_v1.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SAES_v1
{
    public partial class tcdoc : System.Web.UI.Page
    {
        #region <Variables>
        Utilidades utils = new Utilidades();
        Catalogos serviceCatalogo = new Catalogos();
        GraficaService serviceGrafica = new GraficaService();
        KardexAlumno serviceAlumno = new KardexAlumno();
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ctrl_fecha_i", "ctrl_fecha_i();", true);
                if (!IsPostBack)
                {
                    combo_periodos();
                    combo_campus();
                    combo_documentos();
                    combo_estatus_Doc();
                    combo_estatus_sol();

                }
            }
        }

        protected void combo_periodos()
        {
            string strQuerydire = "";
            strQuerydire = "select tpees_clave clave, concat(tpees_clave,'||',tpees_desc) periodo from tpees where tpees_estatus='A'  " +
                            "union " +
                            "select '999999' clave, '-------' periodo " +
                            "order by 1 desc";
            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            ConexionMySql.Open();
            DataTable TablaEstado = new DataTable();
            MySqlCommand ConsultaMySql = new MySqlCommand();
            MySqlDataReader DatosMySql;
            try
            {
                ConsultaMySql.Connection = ConexionMySql;
                ConsultaMySql.CommandType = CommandType.Text;
                ConsultaMySql.CommandText = strQuerydire;
                DatosMySql = ConsultaMySql.ExecuteReader();
                TablaEstado.Load(DatosMySql, LoadOption.OverwriteChanges);

                ddl_periodo.DataSource = TablaEstado;
                ddl_periodo.DataValueField = "Clave";
                ddl_periodo.DataTextField = "Periodo";
                ddl_periodo.DataBind();

            }
            catch (Exception ex)
            {
                string test = ex.Message;
            }
            finally
            {
                ConexionMySql.Close();
            }
        }
        protected void combo_campus()
        {
            ddl_programa.Items.Clear();
            ddl_programa.Items.Add(new ListItem("-------", "0"));
            string strQuerydire = "";
            strQuerydire = "select tcamp_clave clave, tcamp_desc campus from tcamp where tcamp_estatus='A' " +
                            "union " +
                            "select '0' clave, '--------' campus " +
                            "order by 1";
            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            ConexionMySql.Open();
            DataTable TablaEstado = new DataTable();
            MySqlCommand ConsultaMySql = new MySqlCommand();
            MySqlDataReader DatosMySql;
            try
            {
                ConsultaMySql.Connection = ConexionMySql;
                ConsultaMySql.CommandType = CommandType.Text;
                ConsultaMySql.CommandText = strQuerydire;
                DatosMySql = ConsultaMySql.ExecuteReader();
                TablaEstado.Load(DatosMySql, LoadOption.OverwriteChanges);

                ddl_campus.DataSource = TablaEstado;
                ddl_campus.DataValueField = "Clave";
                ddl_campus.DataTextField = "Campus";
                ddl_campus.DataBind();

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcdoc", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
            finally
            {
                ConexionMySql.Close();
            }
        }
        protected void combo_programa()
        {            
            try
            {             
                ddl_programa.DataSource = serviceCatalogo.obtenProgramaCampus(ddl_campus.SelectedValue);
                ddl_programa.DataValueField = "Clave";
                ddl_programa.DataTextField = "Descripcion";
                ddl_programa.DataBind();
            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcdoc", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
        }
        protected void combo_documentos()
        {
            try
            {               
                ddl_documento.DataSource = serviceCatalogo.ObtenerComboComun("Tipo_Documentos", "");
                ddl_documento.DataValueField = "Clave";
                ddl_documento.DataTextField = "Descripcion";
                ddl_documento.DataBind();

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcdoc", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
            
        }
        protected void combo_estatus_Doc()
        {
            string strQuerydire = "";
            strQuerydire = "select tstdo_clave clave, tstdo_desc estatus_doc from tstdo  where tstdo_estatus='A' " +
                            "union " +
                            "select '0' clave, '-------' estatus_doc " +
                            "order by 1";
            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            ConexionMySql.Open();
            DataTable TablaEstado = new DataTable();
            MySqlCommand ConsultaMySql = new MySqlCommand();
            MySqlDataReader DatosMySql;
            try
            {
                ConsultaMySql.Connection = ConexionMySql;
                ConsultaMySql.CommandType = CommandType.Text;
                ConsultaMySql.CommandText = strQuerydire;
                DatosMySql = ConsultaMySql.ExecuteReader();
                TablaEstado.Load(DatosMySql, LoadOption.OverwriteChanges);

                ddl_estatus_doc.DataSource = TablaEstado;
                ddl_estatus_doc.DataValueField = "Clave";
                ddl_estatus_doc.DataTextField = "estatus_doc";
                ddl_estatus_doc.DataBind();

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcdoc", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
            finally
            {
                ConexionMySql.Close();
            }
        }
        protected void combo_estatus_sol()
        {
            string strQuerydire = "";
            strQuerydire = "select tstso_clave clave, tstso_desc estatus_sol from tstso  where tstso_estatus='A' " +
                            "union " +
                            "select '0' clave, '--------' estatus_sol " +
                            "order by 1";
            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            ConexionMySql.Open();
            DataTable TablaEstado = new DataTable();
            MySqlCommand ConsultaMySql = new MySqlCommand();
            MySqlDataReader DatosMySql;
            try
            {
                ConsultaMySql.Connection = ConexionMySql;
                ConsultaMySql.CommandType = CommandType.Text;
                ConsultaMySql.CommandText = strQuerydire;
                DatosMySql = ConsultaMySql.ExecuteReader();
                TablaEstado.Load(DatosMySql, LoadOption.OverwriteChanges);

                ddl_estatus_sol.DataSource = TablaEstado;
                ddl_estatus_sol.DataValueField = "Clave";
                ddl_estatus_sol.DataTextField = "estatus_sol";
                ddl_estatus_sol.DataBind();

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcdoc", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
            finally
            {
                ConexionMySql.Close();
            }
        }

        protected void ddl_campus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_campus.SelectedValue != "0")
            {
                combo_programa();
            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            combo_periodos();
            combo_campus();
            combo_documentos();
            combo_estatus_Doc();
            combo_estatus_sol();
            txt_fecha_l.Text = null;
            GridCDocumentos.Visible = false;
        }

        protected void btn_consultar_Click(object sender, EventArgs e)
        {
            grid_c_documentos();
        }
        protected void grid_c_documentos()
        {
            string Query = "select distinct  tpers_id clave, concat(tpers_nombre, ' ', tpers_paterno, ' ', tpers_materno) nombre,tadmi_tpees_clave periodo,tcamp_desc Campus,tprog_desc programa, tdocu_desc Documento,tstdo_desc Estatus_Documento,tstso_desc Estatus_Solicitud,fecha(date_format(tredo_fecha_limite, '%Y-%m-%d')) fecha_limite " +
                            "from tpers " +
                            "inner join tadmi on tadmi_tpers_num = tpers_num " +
                            "inner join tcamp on tcamp_clave = tadmi_tcamp_clave " +
                            "inner join tprog on tprog_clave = tadmi_tprog_clave " +
                            "inner join tredo on tredo_tpers_num = tpers_num and tadmi_tpees_clave = tredo_tpees_clave " +
                            "inner join tdocu on tdocu_clave = tredo_tdocu_clave " +
                            "left join tstdo on tstdo_clave = tredo_tstdo_clave " +
                            "inner join tstso on tstso_clave = tadmi_tstso_clave " +
                            "where 1=1 ";
            if (ddl_periodo.SelectedValue != "999999")
            {
                Query = Query + "and tadmi_tpees_clave='" + ddl_periodo.SelectedValue + "' ";
            }
            if (ddl_campus.SelectedValue != "0")
            {
                Query = Query + "and tadmi_tcamp_clave='" + ddl_campus.SelectedValue + "' ";
            }
            if (ddl_programa.SelectedValue != "0")
            {
                Query = Query + "and tprog_clave='" + ddl_programa.SelectedValue + "' ";
            }
            if (ddl_documento.SelectedValue != "0")
            {
                Query = Query + "and tredo_tdocu_clave='" + ddl_documento.SelectedValue + "' ";
            }
            if (ddl_estatus_doc.SelectedValue != "0")
            {
                Query = Query + " and tstdo_clave='" + ddl_estatus_doc.SelectedValue + "' ";
            }
            if (ddl_estatus_sol.SelectedValue != "0")
            {
                Query = Query + " and tstso_clave='" + ddl_estatus_sol.SelectedValue + "' ";
            }
            if (!String.IsNullOrEmpty(txt_fecha_l.Text))
            {
                Query = Query + " and date_format(tredo_fecha_limite,'%d/%m/%Y')='" + txt_fecha_l.Text + "' ";
            }
            Query = Query + "order by 1";
            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            ConexionMySql.Open();
            try
            {

                MySqlDataAdapter dataadapter = new MySqlDataAdapter(Query, ConexionMySql);
                DataSet ds = new DataSet();
                dataadapter.Fill(ds, "Solicitud");
                GridCDocumentos.DataSource = ds;
                GridCDocumentos.DataBind();
                GridCDocumentos.DataMember = "Solicitud";
                GridCDocumentos.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridCDocumentos.UseAccessibleHeader = true;
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "remove_class", "remove_class();", true);
                GridCDocumentos.Visible = true;

            }
            catch (Exception ex)
            {
                string test = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "tcdoc", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);
            }
            finally
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);
                ConexionMySql.Close();
            }
        }

        protected void back_Click(object sender, EventArgs e)
        {
            Response.Redirect("Tredo.aspx");
        }

        protected void linkRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Tredo.aspx");
        }
    }
}