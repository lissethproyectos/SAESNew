using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SAES_v1.Repositorio
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Response.Redirect(FormsAuthentication.DefaultUrl);
                Response.End();
            }
            else
            {
                try
                {

                    if (Session["rol"].ToString() == "Alumno")
                    {
                        ///Menus///
                        operacion.Visible = false;
                        prospectos.Visible = false;
                        admision.Visible = false;
                        escolares.Visible = false;
                        planeacion.Visible = false;
                        Finanzas.Visible = false;
                        Seguridad.Visible = false;
                        ///SubMenus
                        tdocumentos.Visible = false;
                        permisos_repo.Visible = false;
                        expedientes.Visible = false;
                    }
                    else
                    {
                        ///SubMenu///
                        carga_alumno.Visible = false;
                    }
                }
                catch
                {
                    Response.Redirect("../Default.aspx");
                }
                try
                {
                    nombre.Text = Session["nombre"].ToString();
                    perfil.Text = Session["rol"].ToString();
                    nombre_1.Text = Session["nombre"].ToString();
                    perfil_1.Text = Session["rol"].ToString();
                }
                catch
                {
                    Response.Redirect("../Default.aspx");
                }
            }
        }

        protected void logout_btn_Click(object sender, EventArgs e)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Response.Redirect(FormsAuthentication.DefaultUrl);
                Response.End();
            }
            else
            {
                FormsAuthentication.SignOut();
                HttpContext.Current.Session.Abandon();
                Session.Clear();
                Response.Redirect("../Default.aspx");
            }
        }
    }
}