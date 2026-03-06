using MySql.Data.MySqlClient;
using SAES_DBO.Models;
using SAES_Services;
using SAES_v1.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using static SAES_DBO.Models.ModelRepositorio;

namespace SAES_v1.Repositorio
{
    public partial class ListadoExpediente : System.Web.UI.Page
    {
        //applyWeb.Data.Data objExpediente = new applyWeb.Data.Data(System.Configuration.ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
        string id_page_ex;
        string id_page;
        string id_page_reload;
        public double porcentaje;
        #region <Variables>
        Utilidades utils = new Utilidades();
        Catalogos serviceCatalogo = new Catalogos();
        UsuarioService serviceUsuario = new UsuarioService();
        RepositorioService serviceRepositorio = new RepositorioService();
        List<ModeltpaisResponse> lstPaises = new List<ModeltpaisResponse>();
        MenuService servicePermiso = new MenuService();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                permisos();
            }
            catch
            {
                Response.Redirect("../Default.aspx");
            }

            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Response.Redirect(FormsAuthentication.DefaultUrl);
                Response.End();
            }
            if (Session["Rol"] == null || (Session["Rol"].ToString().Equals("Alumno")))
            {
                Response.Redirect("../Default.aspx");
            }
            else
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                if (!IsPostBack)
                {
                    try
                    {
                        progress_bar();
                        carga_nombre_alumno();
                        CargaListaExpediente(Request.QueryString["IDAlumno"].ToString());

                    }
                    catch (Exception ex)
                    {
                        Response.Redirect("../Default.aspx");
                    }
                }

            }
            

            string id_archivo = Convert.ToString(Request.QueryString["id_archivo"]);
            if (id_archivo != null)
            {
                elimina_preview(id_archivo, Request.QueryString["IDAlumno"].ToString());
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

        protected void CargaListaExpediente(string pIDAlumno)
        {
            try
            {                
                gvExpediente.DataSource = serviceRepositorio.ObtenerDocumentosAlumno(pIDAlumno, Session["Rol"].ToString());
                gvExpediente.DataBind();
                gvExpediente.UseAccessibleHeader = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);
            }
            catch (Exception e)
            {
                string text = e.Message + e.ToString() + e.StackTrace.ToString();
            }
        }

        protected void gvExpediente_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Subir")
            {

                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvExpediente.Rows[index];
                string IDTipoDocumento = row.Cells[0].Text;
                string IDDocumento = row.Cells[1].Text;
                string IDAlumno = row.Cells[2].Text;
                string QerySelect = "select count(*) from documentos_alumno " +
                          " where IDAlumno = '" + IDAlumno + "'" +
                          " and IDTipoDocumento ='" + IDDocumento + "'";

                MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                conexion.Open();
                DataSet dssql1 = new DataSet();
                MySqlDataAdapter sqladapter = new MySqlDataAdapter();
                MySqlCommand commandsql1 = new MySqlCommand(QerySelect, conexion);
                sqladapter.SelectCommand = commandsql1;
                sqladapter.Fill(dssql1);
                sqladapter.Dispose();
                try
                {
                    commandsql1.Dispose();
                }
                catch (Exception ex)
                {
                    ///logs
                }
                if (dssql1.Tables[0].Rows[0][0].ToString() != "0")
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "cargar_doc('InputFile.aspx?IDTipoDocumento=" + IDTipoDocumento + "&IDDocumento=" + IDDocumento + "&IDAlumno=" + IDAlumno + "','ListadoExpediente.aspx?IDAlumno=" + IDAlumno + "')", true);
                }
                else
                {
                    
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "JQDialog('InputFile.aspx?IDTipoDocumento=" + IDTipoDocumento + "&IDDocumento=" + IDDocumento + "&IDAlumno=" + IDAlumno + "','ListadoExpediente.aspx?IDAlumno=" + IDAlumno + "');", true);
                }
                conexion.Close();


            }
            else if (e.CommandName == "Preview")
            {
                DirectoryInfo vPath = new DirectoryInfo(Server.MapPath("~/UploadedFiles/"));
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvExpediente.Rows[index];
                string IDTipoDocumento = row.Cells[0].Text;
                string IDDocumento = row.Cells[1].Text;
                string IDAlumno = row.Cells[2].Text;
                cargar_vista_previa(IDTipoDocumento, IDDocumento, IDAlumno);
                
            }
            else if (e.CommandName == "Comentarios")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvExpediente.Rows[index];
                string IDDocumento = HttpUtility.HtmlDecode(row.Cells[0].Text);
                string IDAlumno = HttpUtility.HtmlDecode(row.Cells[2].Text);
                if ( IDDocumento == "0")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "NoComen", "NoComen();", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "JQDialog_Comentarios('Comentarios.aspx?IDDocumento=" + IDDocumento + "','ListadoExpediente.aspx?IDAlumno=" + IDAlumno + "');", true);
                }

            }
        }

        private void cargar_vista_previa(string IdTipoDocumento, string IDDocumento, string IDAlumno)
        {
            DirectoryInfo vPath = new DirectoryInfo(Server.MapPath("~/UploadedFiles/"));
            string strQuery = "SELECT DISTINCT Documento,Formato FROM Documentos_Alumno WHERE IDTipoDocumento=@IDDocumento AND IDAlumno=@IDAlumno";
            MySqlCommand cmd = new MySqlCommand(strQuery);
            cmd.Parameters.Add("@IDDocumento", MySqlDbType.Int32).Value = Convert.ToInt32(IDDocumento);
            //cmd.Parameters.Add("@IDTipoDocumento", MySqlDbType.Int32).Value = Convert.ToInt32(IdTipoDocumento);
            cmd.Parameters.Add("@IDAlumno", MySqlDbType.VarChar).Value = IDAlumno;
            DataTable dt = GetData(cmd);
            if (dt.Rows.Count > 0)
            {
                Byte[] bytes = (Byte[])dt.Rows[0]["Documento"];
                string extension = "." + dt.Rows[0]["Formato"];
                string path = vPath.ToString() + IdTipoDocumento + "_" + IDDocumento + "_" + IDAlumno + extension;
                File.WriteAllBytes(path, bytes);
                string paso = "JQDialog_preview('Preview_Page.aspx?id_archivo=" + IdTipoDocumento + "_" + IDDocumento + "_" + IDAlumno + extension + "&formato=" + extension + "&IDDocumento=" + IDDocumento + "&IDTipoDocumento=" + IdTipoDocumento + "', 'ListadoExpediente.aspx?IDAlumno=" + IDAlumno + "&id_archivo=" + IdTipoDocumento + "_" + IDDocumento + "_" + IDAlumno + extension + "'); ";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "JQDialog_preview('Preview_Page.aspx?id_archivo=" + IdTipoDocumento + "_" + IDDocumento + "_" + IDAlumno + extension + "&formato=" + extension + "&IDDocumento=" + IDDocumento + "&IDTipoDocumento=" + IdTipoDocumento + "','ListadoExpediente.aspx?IDAlumno=" + IDAlumno + "&id_archivo=" + IdTipoDocumento + "_" + IDDocumento + "_" + IDAlumno + extension + "');", true);

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "NoExiste", "NoExiste();", true);
            }

        }

        protected void gvExpediente_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string IDDocumento = e.Row.Cells[1].Text;
                string Estatus = e.Row.Cells[4].Text;
                LinkButton preview = e.Row.FindControl("imgPreview") as LinkButton;
                LinkButton subir = e.Row.FindControl("imgExpediente") as LinkButton;
                LinkButton comentarios = e.Row.FindControl("imgComentarios") as LinkButton;
                if (IDDocumento != "0" && Estatus == "ACEPTADO")
                {
                    preview.Visible = true;
                    subir.Visible = false;
                    comentarios.Visible = true;
                }
                else if (IDDocumento != "0" && Estatus != "ACEPTADO")
                {
                    preview.Visible = true;
                    comentarios.Visible = true;
                }
                else
                {
                    preview.Visible = false;
                    comentarios.Visible = false;
                }
            }
        }

        private void elimina_preview(string id_archivo, string IDAlumno)
        {
            string id_page_reload_1 = Convert.ToString(Request.QueryString["PageEx"]);
            DirectoryInfo vPath = new DirectoryInfo(Server.MapPath("~/UploadedFiles/"));
            File.Delete(vPath.ToString() + id_archivo);
            
            Response.Redirect("ListadoExpediente.aspx?IDAlumno=" + IDAlumno );
        }

        protected void carga_nombre_alumno()
        {
            string IDAlumno = Request.QueryString["IDAlumno"].ToString();
            string strQuery = "SELECT DISTINCT Nombre FROM Alumno WHERE IDAlumno=@IDAlumno";
            MySqlCommand cmd = new MySqlCommand(strQuery);
            cmd.Parameters.Add("@IDAlumno", MySqlDbType.VarChar).Value = IDAlumno;
            DataTable dt = GetData(cmd);
            Nombre_Alumno.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dt.Rows[0]["Nombre"].ToString());

        }


        protected void permisos()
        {
            ModelObtenerPermisoRepositorioResponse objPrivilegio = new ModelObtenerPermisoRepositorioResponse();

            try
            {
                objPrivilegio = serviceUsuario.UsuarioRepositorio(Session["Rol"].ToString());
                if (objPrivilegio != null)
                {
                    if (objPrivilegio.IDPrivilegio == "19") 
                    { 
                        gvExpediente.Columns[6].Visible = true; 
                    } //Permiso para subir documentos


                    
                }
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "ListadoExpediente", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);

            }

        }

        protected void progress_bar()
        {
            string IDAlumno = Convert.ToString(Request.QueryString["IDAlumno"]);
            ModelObtenerDoctosEntregadosResponse objDoctos = new ModelObtenerDoctosEntregadosResponse();

            /*string strQuery = "SELECT DISTINCT COUNT(TDA.IDTipoDocumento)Documentos, " +
                              "(SELECT COUNT(*) FROM Documentos_Alumno WHERE IDEstatusDocumento in (17,3) AND IDAlumno='" + IDAlumno + "')Entregados " +
                              "FROM TiposDocumento_nivel TDA " +
                              "JOIN TipoDocumento TD ON TD.IDTipoDocumento = TDA.IDTipoDocumento " +
                              "JOIN Alumno AL ON AL.CodigoProcedencia = TDA.IDProcedencia AND AL.CodigoTipoIngreso = TDA.IDTipoIngreso AND AL.IDNivel=TDA.IDNivel  AND AL.IDModalidad=TDA.IDModalidad AND AL.IDAlumno = '" + IDAlumno + "' ";
            */
            //string strQuery = "select count(*) Documentos from tpers inner join tadmi a on tadmi_tpers_num = tpers_num " +
            //  " inner join tredo on tredo_tpers_num = tadmi_tpers_num and tredo_tpees_clave = tadmi_tpees_clave " +     
            //  "   and tredo_consecutivo = tadmi_consecutivo and tredo_estatus = 'A' " +
            //  " inner join tdocu on tredo_tdocu_clave = tdocu_clave and tdocu_estatus = 'A' " +
            //  " inner join TipoDocumento x on id_saes = tdocu_clave " +
            //  " LEFT JOIN Documentos_Alumno b ON IDAlumno = tpers_id and x.IDTipoDocumento = b.IDTipoDocumento " +
            //  " where tpers_id = '" + IDAlumno + "'" ;

            //string strQuery1 = "select count(*) Entregados from tpers inner join tadmi a on tadmi_tpers_num = tpers_num " +
            //  " inner join tredo on tredo_tpers_num = tadmi_tpers_num and tredo_tpees_clave = tadmi_tpees_clave " +
            //  "   and tredo_consecutivo = tadmi_consecutivo and tredo_estatus = 'A' " +
            //  " inner join tdocu on tredo_tdocu_clave = tdocu_clave and tdocu_estatus = 'A' " +
            //  " inner join TipoDocumento x on id_saes = tdocu_clave " +
            //  " LEFT JOIN Documentos_Alumno b ON IDAlumno = tpers_id and x.IDTipoDocumento = b.IDTipoDocumento " +
            //  " where tpers_id = '" + IDAlumno + "' and  IDEstatusDocumento=1 ";

            //MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);


            //MySqlCommand cmd = new MySqlCommand(strQuery);
            //DataTable dt = GetData(cmd);
            //double documentos = Convert.ToDouble(dt.Rows[0]["Documentos"]);

            //MySqlCommand cmd1 = new MySqlCommand(strQuery1);
            //DataTable dt1 = GetData(cmd1);
            //double Entregados = Convert.ToDouble(dt1.Rows[0]["Entregados"]);

            objDoctos = serviceRepositorio.DocumentosEntregados(IDAlumno);

            if (Convert.ToInt32(objDoctos.TotEntregados) > 0)
            {
                porcentaje = Convert.ToInt32(objDoctos.TotEntregados) * 100 / Convert.ToInt32(objDoctos.TotDoctos);
            }
            else
            {
                porcentaje = 1;
            }

            lbl_bar.Text = "Documentos aceptados " + objDoctos.TotEntregados + " de " + objDoctos.TotDoctos + " (" + Math.Round(porcentaje).ToString() + "%)";

            if (Math.Round(porcentaje) < 40)
            {
                html_progress_bar.Attributes.Add("style", "width:" + Math.Round(porcentaje).ToString() + "%;color:#000;");
                html_progress_bar.Attributes.Add("class", "progress-bar progress-bar-striped progress-bar-animated");
            }
            else if (Math.Round(porcentaje) > 90)
            {
                html_progress_bar.Attributes.Add("style", "width:" + Math.Round(porcentaje).ToString() + "%");
                html_progress_bar.Attributes.Add("class", "progress-bar progress-bar-striped progress-bar-animated bg-success");
            }
            else
            {
                html_progress_bar.Attributes.Add("style", "width:" + Math.Round(porcentaje).ToString() + "%");
                html_progress_bar.Attributes.Add("class", "progress-bar progress-bar-striped progress-bar-animated");
            }
        }
    }
}