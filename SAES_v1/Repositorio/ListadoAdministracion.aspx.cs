using MySql.Data.MySqlClient;
using SAES_DBO.Models;
using SAES_Services;
using SAES_v1.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using static SAES_DBO.Models.ModelMenu;

namespace SAES_v1.Repositorio
{
    public partial class ListadoAdministracion : System.Web.UI.Page
    {
        string id_export = null;
        string id_page = null;
        #region <Variables>
        Utilidades utils = new Utilidades();
        Catalogos serviceCatalogo = new Catalogos();

        UsuarioService serviceUsuario = new UsuarioService();
        RepositorioService serviceRepositorio=new RepositorioService();
        List<ModeltpaisResponse> lstPaises = new List<ModeltpaisResponse>();
        MenuService servicePermiso = new MenuService();
        #endregion


        //applyWeb.Data.Data objAdministracion = new applyWeb.Data.Data(System.Configuration.ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);


        protected void Page_Load(object sender, EventArgs e)
        {

            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Response.Redirect(FormsAuthentication.DefaultUrl);
                Response.End();
            }
            if (Session["rol"] == null || (Session["rol"].ToString().Equals("Alumno")))
            {
                Response.Redirect("../Default.aspx");
            }
            else
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                if (!IsPostBack)
                {                    
                    permisos();

                    if (chkSoloCompletos.Checked == true)
                    {
                        string strStatus = "COMPLETO";
                        CargaListaAlumnos(Session["Rol"].ToString(), Session["usuario"].ToString(), strStatus);
                    }
                    else
                    {
                        string strStatus = "PENDIENTE";
                        CargaListaAlumnos(Session["Rol"].ToString(), Session["usuario"].ToString(), strStatus);
                    }

                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "load_datatable", "load_datatable();", true);

            }
        }

        protected void CargaListaAlumnos(string pRol, string pIDusuario, string pStatus)
        {

            //ArrayList arrParametros = new ArrayList();
            //arrParametros.Add(new applyWeb.Data.Parametro("@Rol", pRol));
            //arrParametros.Add(new applyWeb.Data.Parametro("@User", pIDusuario));
            //arrParametros.Add(new applyWeb.Data.Parametro("@Status", pStatus));
            //DataSet dsAlumnos = objAdministracion.ExecuteSP("Obtener_Listado_Alumnos", arrParametros);
            //gvAlumnos.DataSource = dsAlumnos;
            //gvAlumnos.DataBind();
            //gvAlumnos.HeaderRow.TableSection = TableRowSection.TableHeader;
            //gvAlumnos.UseAccessibleHeader = true;
            gvAlumnos.DataSource = serviceRepositorio.ObtenerStatusDocumentos(pRol, pStatus, pIDusuario);
            gvAlumnos.DataBind();



        }

        protected void chkSoloCompletos_CheckedChanged(object sender, EventArgs e)
        {
            string strStatus = "PENDIENTE";

            if (chkSoloCompletos.Checked == true)
                strStatus = "COMPLETO";

            CargaListaAlumnos(Session["Rol"].ToString(), Session["usuario"].ToString(), strStatus);
        }

        protected void permisos()
        {
            ModelObtenerPermisoRepositorioResponse objPrivilegio = new ModelObtenerPermisoRepositorioResponse();

            try
            {
                objPrivilegio=serviceUsuario.UsuarioRepositorio(Session["Rol"].ToString());
                if (objPrivilegio != null)
                {
                    if (objPrivilegio.IDPrivilegio == "18")
                    {
                        chkSoloCompletos.Enabled = true; chkSoloCompletos.Visible = true;
                    }
                    else
                    {
                        //exportar.Visible = false;
                        chkSoloCompletos.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                //resultado.Text = ex.Message;
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, "ListadoAdministracion", Session["usuario"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_consulta", "error_consulta();", true);

            }

            //MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            //ConexionMySql.Open();
            //string strQuery = "SELECT DISTINCT A.IDPrivilegio,b.Permiso FROM Permisos_App_Rol A INNER JOIN Permisos_App B ON A.IDPrivilegio=B.IDPrivilegio INNER JOIN Rol C ON A.IDRol=C.IDRol WHERE B.IDMenu=3 AND B.IDSubMenu=1 AND C.Nombre='" + Session["Rol"].ToString() + "'";
            //MySqlCommand cmd = new MySqlCommand(strQuery, ConexionMySql);
            //MySqlDataReader dr = cmd.ExecuteReader();
            //while (dr.Read())
            //{
            //    int IDprivilegio = dr.GetInt32(0);

            //    //if (IDprivilegio == 17) { exportar.Visible = true; } //Permiso para Exportar
            //    //else 
            //    if (IDprivilegio == 18) { chkSoloCompletos.Enabled = true; chkSoloCompletos.Visible = true; } //Permiso para Expedientes completos
            //    else
            //    {
            //        //exportar.Visible = false;
            //        chkSoloCompletos.Visible = false;
            //    }


            //}
            //ConexionMySql.Close();
        }


        protected void gvAlumnos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Expediente")
            {
                int pagina = gvAlumnos.PageIndex;
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvAlumnos.Rows[index];
                string Matricula = row.Cells[0].Text;
                if (TextBox1.Text == null || TextBox1.Text == "")
                {
                    Response.Redirect("ListadoExpediente.aspx?IDAlumno=" + Matricula);
                }
                else
                {
                    Response.Redirect("ListadoExpediente.aspx?IDAlumno=" + Matricula);
                }

            }
        }

        protected void gvAlumnos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            string strStatus = "PENDIENTE";

            if (chkSoloCompletos.Checked == true)
                strStatus = "COMPLETO";
            //GridView gv = (GridView)sender;
            gvAlumnos.PageIndex = e.NewPageIndex;
            CargaListaAlumnos(Session["Rol"].ToString(), Session["usuario"].ToString(), strStatus);
            id_page = gvAlumnos.PageIndex.ToString();
            TextBox1.Text = id_page;

        }
    }
}