using SAES_Services;
using SAES_v1.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SAES_v1
{
    public partial class tconc : System.Web.UI.Page
    {
        Utilidades utils = new Utilidades();
        ConcentracionCalificaciones concentracionCalificaciones = new ConcentracionCalificaciones();
        Catalogos catalogos = new Catalogos();
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
                    CargaInicial();
                }
            }
        }

        public void CargaInicial()
        {
            btn_save.Visible = false;

            ddl_campus.Items.Clear();
            ddl_nivel.Items.Clear();
            ddl_periodo.Items.Clear();

            ddl_periodo = utils.BeginDropdownList(ddl_periodo, catalogos.obtenPeriodo());
            ddl_campus = utils.BeginDropdownList(ddl_campus, catalogos.obtenCampus());
            ddl_nivel = utils.BeginDropdownList(ddl_nivel, catalogos.obtenNivel(""));
            ddl_programa = utils.BeginDropdownList(ddl_programa, catalogos.obtenPrograma("", ""));
            ddl_materia = utils.BeginDropdownList(ddl_materia, catalogos.obtenMateria(null, null, null));
            ddl_grupo = utils.BeginDropdownList(ddl_grupo);

            ddl_campus.Enabled = false;
            ddl_nivel.Enabled = false;
            ddl_programa.Enabled = false;
            ddl_materia.Enabled = false;
            ddl_grupo.Enabled = false;
        }

        protected void ddl_periodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string periodo = ddl_periodo.SelectedValue;
                if (!string.IsNullOrEmpty(periodo))
                {
                    btn_save.Visible = true;
                    ddl_campus.Enabled = true;
                    ddl_nivel.Enabled = true;
                    ddl_programa.Enabled = true;
                    ddl_materia.Enabled = true;
                    ddl_grupo.Enabled = true;
                }
                else
                    CargaInicial();                           
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, this.GetType().Name, Session["usuario"].ToString(), ex.StackTrace);
                if (ex.Message.Contains("Object reference not set to an instance of an object"))
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion('" + ex.StackTrace + "');", true);                    
                }
                else if (ex.Message.Contains("NO SE ENCUENTRA GRUPO"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "warning_consulta", "warning_consulta('" + mensaje_error + "');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion('" + mensaje_error + "');", true);
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "begin_chosen", "begin_chosen();", true);
        }

        protected void ddl_campus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string periodo = (ddl_periodo.SelectedValue == "") ? null : ddl_periodo.SelectedValue;
                string campus = (ddl_campus.SelectedValue == "") ? null : ddl_campus.SelectedValue;
                ddl_nivel = utils.BeginDropdownList(ddl_nivel, catalogos.obtenNivel(campus));                
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, this.GetType().Name, Session["usuario"].ToString(), ex.StackTrace);
                if (ex.Message.Contains("Object reference not set to an instance of an object"))
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion('" + ex.StackTrace + "');", true);                    
                }
                else if (ex.Message.Contains("NO SE ENCUENTRA GRUPO"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "warning_consulta", "warning_consulta('" + mensaje_error + "');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion('" + mensaje_error + "');", true);
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "begin_chosen", "begin_chosen();", true);
        }

        protected void ddl_nivel_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string periodo = (ddl_periodo.SelectedValue == "") ? null : ddl_periodo.SelectedValue;
                string campus = (ddl_campus.SelectedValue == "") ? null : ddl_campus.SelectedValue;
                string nivel = (ddl_nivel.SelectedValue == "") ? null : ddl_nivel.SelectedValue;
                ddl_programa = utils.BeginDropdownList(ddl_programa, catalogos.obtenPrograma(campus, nivel));   
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, this.GetType().Name, Session["usuario"].ToString(), ex.StackTrace);
                if (ex.Message.Contains("Object reference not set to an instance of an object"))
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion('" + ex.StackTrace + "');", true);                    
                }
                else if (ex.Message.Contains("NO SE ENCUENTRA GRUPO"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "warning_consulta", "warning_consulta('" + mensaje_error + "');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion('" + mensaje_error + "');", true);
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "begin_chosen", "begin_chosen();", true);
        }
        protected void ddl_programa_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string periodo = ddl_periodo.SelectedValue;
                string campus = (ddl_campus.SelectedValue == "") ? null : ddl_campus.SelectedValue;
                string nivel = (ddl_nivel.SelectedValue == "") ? null : ddl_nivel.SelectedValue;
                string programa = (ddl_programa.SelectedValue == "") ? null : ddl_programa.SelectedValue;

                ddl_materia = utils.BeginDropdownList(ddl_materia, catalogos.obtenMateria(campus, nivel, programa));
                ScriptManager.RegisterStartupScript(this, this.GetType(), "begin_chosen", "begin_chosen();", true);                                  
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, this.GetType().Name, Session["usuario"].ToString(), ex.StackTrace);
                if (ex.Message.Contains("Object reference not set to an instance of an object"))
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion('" + ex.StackTrace + "');", true);                    
                }
                else if (ex.Message.Contains("NO SE ENCUENTRA GRUPO"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "warning_consulta", "warning_consulta('" + mensaje_error + "');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion('" + mensaje_error + "');", true);
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "begin_chosen", "begin_chosen();", true);
        }

        protected void ddl_materia_SelectedIndexChanged(object sender, EventArgs e)
        {
            string periodo = (ddl_periodo.SelectedValue == "") ? null : ddl_periodo.SelectedValue;
            string campus = (ddl_campus.SelectedValue == "") ? null : ddl_campus.SelectedValue;
            string nivel = (ddl_nivel.SelectedValue == "") ? null : ddl_nivel.SelectedValue;
            string programa = (ddl_programa.SelectedValue == "") ? null : ddl_programa.SelectedValue;
            string materia = (ddl_materia.SelectedValue == "") ? null : ddl_materia.SelectedValue;
            try
            {
                if (!string.IsNullOrEmpty(materia))                
                    ddl_grupo = utils.BeginDropdownList(ddl_grupo, catalogos.obtenGrupo(campus, nivel, programa, materia, periodo));  
                else
                    ddl_grupo = utils.BeginDropdownList(ddl_grupo);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "begin_chosen", "begin_chosen();", true);
            }
            catch (Exception ex)
            {
                ddl_grupo = utils.BeginDropdownList(ddl_grupo);
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error + "periodo:" + periodo + " campus:"+ campus+ " nivel:"+ nivel+ " programa:"+ programa+ " materia:"+ materia, this.GetType().Name, Session["usuario"].ToString(), ex.StackTrace);
                if (ex.Message.Contains("Object reference not set to an instance of an object"))
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion('" + ex.StackTrace + "');", true);                    
                }
                else if(ex.Message.Contains("NO SE ENCUENTRA GRUPO"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "warning_consulta", "warning_consulta('" + mensaje_error + "');", true);                    
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion('" + mensaje_error + "');", true);
                }
                
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "begin_chosen", "begin_chosen();", true);
        }       

        protected void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                string periodo = (ddl_periodo.SelectedValue == "")? null: ddl_periodo.SelectedValue;
                string campus = (ddl_campus.SelectedValue == "") ? null : ddl_campus.SelectedValue;
                string nivel = (ddl_nivel.SelectedValue == "") ? null : ddl_nivel.SelectedValue;
                string programa = (ddl_programa.SelectedValue == "") ? null : ddl_programa.SelectedValue;
                string materia = (ddl_materia.SelectedValue == "") ? null : ddl_materia.SelectedValue;
                string grupo = (ddl_grupo.SelectedValue == "") ? null : ddl_grupo.SelectedValue;
                if (!string.IsNullOrEmpty(periodo))
                {
                    concentracionCalificaciones.InsertConcentracionCalificacion(periodo, campus, nivel, programa, materia, grupo, Session["usuario"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "save", "save();", true);
                    CargaInicial();
                }
            }
            catch (Exception ex)
            {
                string mensaje_error = ex.Message.Replace("'", "-");
                Global.inserta_log(mensaje_error, this.GetType().Name, Session["usuario"].ToString(), ex.StackTrace);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_transaccion", "error_transaccion('" + mensaje_error + "');", true);
            }
        }          
    }
}