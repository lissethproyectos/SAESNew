using MySql.Data.MySqlClient;
using SAES_Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using static SAES_DBO.Models.ModelRepositorio;

namespace SAES_v1.Repositorio
{
    public partial class InputFile : System.Web.UI.Page
    {
        #region <Variables>
        public string formato;
        public string tamano_min;
        public string tamano_max;
        public string IDTipoDocumento;
        public string IDDocumento;
        public string IDAlumno;
        RepositorioService serviceRepositorio = new RepositorioService();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            ModelObtenerTipoDoctosResponse objTipoDoctso=new ModelObtenerTipoDoctosResponse();

            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Response.Redirect(FormsAuthentication.DefaultUrl);
                Response.End();
            }
            if (Session["Rol"] == null)
            {
                Response.Redirect("../Default.aspx");
            }

            IDTipoDocumento = Convert.ToString(Request.QueryString["IDTipoDocumento"]);
            IDDocumento = Convert.ToString(Request.QueryString["IDDocumento"]);
            IDAlumno = Convert.ToString(Request.QueryString["IDAlumno"]);

            if (IDTipoDocumento == null || IDDocumento == null || IDAlumno == null)
            {
                Response.Redirect("Inicio.aspx");
            }
            else
            {
                try
                {
                    objTipoDoctso = serviceRepositorio.ObtenerTipoDoctos("2");
                    if (objTipoDoctso != null)
                    {
                        formato = objTipoDoctso.Formato;
                        tamano_min = objTipoDoctso.TamanoMinimo;
                        tamano_max = objTipoDoctso.TamanoMaximo;
                    }
                }
                catch (Exception ex)
                {
                    DirectoryInfo virtualDirPath = new DirectoryInfo(Server.MapPath("~/Logs/"));
                    StreamWriter sw = new StreamWriter(virtualDirPath + "error_input_file" + DateTime.Now + ".txt", true);
                    sw.WriteLine(IDAlumno);
                    sw.WriteLine(IDTipoDocumento);
                    sw.WriteLine(IDDocumento);
                    sw.WriteLine(ex.ToString());
                    sw.Close();
                }
            }
        }
    }
}