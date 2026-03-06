using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAES_Services;
using SAES_DBO.Models;
using SAES_v1.Utils;

namespace SAES_v1
{
    public partial class Default : System.Web.UI.Page
    {
        #region <Variables>
        Utilidades utils = new Utilidades();
        UsuarioService serviceUsuario = new UsuarioService();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                username.Attributes.Add("placeholder", "Ingresa tu usuario");
                password.Attributes.Add("placeholder", "Ingresa tu contraseña");
                username.Attributes.Add("autocomplete", "off");
                password.Attributes.Add("autocomplete", "off");
            }
            else
            {
                //Response.Redirect("Inicio.aspx");
                Response.Redirect("Inicio.aspx");
            }

            //Response.Redirect("Inicio.aspx");
        }

        protected void entrar_Click(object sender, EventArgs e)
        {
            mensaje.Visible = false;
            if (!String.IsNullOrEmpty(username.Text) || String.IsNullOrEmpty(password.Text))
            {
                ModelUsuario objUsuario = new ModelUsuario();
                objUsuario = serviceUsuario.VerificaUsuario(username.Text, password.Text);

                if (objUsuario.valido == "2") //Usuario correcto, pass incorrecto
                {

                    ////Response.Redirect("Inicio.aspx");
                    //Response.Redirect("Default2.aspx");
                    Session["tiene_pass"] = objUsuario.valido;
                    mensaje.Visible = true;
                    mensaje.Text = "La contraseña es incorrecta";

                }

                else if (objUsuario.valido == "1")
                {
                    Session["tiene_pass"] = objUsuario.valido;
                    Session["usuario"] = username.Text;
                    Session["nombre"] = objUsuario.tuser_desc;
                    Session["rol"] = objUsuario.tuser_role;

                    FormsAuthentication.Initialize();
                    FormsAuthenticationTicket fat = new FormsAuthenticationTicket(1,
                            username.Text, DateTime.Now, DateTime.Now.AddMinutes(20), false, "SAES", FormsAuthentication.FormsCookiePath);
                    Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(fat)));

                    //Response.Redirect("Inicio.aspx");
                    Response.Redirect("Inicio.aspx");
                    //Response.Redirect("Prueba_site.aspx");

                }
                else if (objUsuario.valido == "3") //Sin password
                {
                    Session["tiene_pass"] = objUsuario.valido;
                    Session["usuario"] = username.Text;
                    Response.Redirect("Default2.aspx");
                }
                //if (autenticacion_admin(username.Text, password.Text))
                //{
                //    Session["rol"] = "";
                //    Session["usuario"] = username.Text;

                //    MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);

                //    //Obtiene si el admin ya tiene registro
                //    string strQueryok = "";
                //    strQueryok = " SELECT COUNT(*) FROM tuser WHERE TUSER_CLAVE='" + username.Text + "'";

                //    string strQueryrol = "";
                //    strQueryrol = "SELECT trole_desc,tuser_desc FROM tuser INNER JOIN trole ON trole_clave=tuser_trole_clave WHERE tuser_clave='" + username.Text+"'";
                //    ConexionMySql.Open();
                //    MySqlDataAdapter mysqladapter = new MySqlDataAdapter();
                //    DataSet dsmysql = new DataSet();
                //    MySqlCommand cmdmysql = new MySqlCommand(strQueryok, ConexionMySql);
                //    mysqladapter.SelectCommand = cmdmysql;
                //    mysqladapter.Fill(dsmysql);
                //    mysqladapter.Dispose();
                //    cmdmysql.Dispose();
                //    ConexionMySql.Close();
                //    if (dsmysql.Tables[0].Rows[0][0].ToString() != "0")
                //    {
                //        ConexionMySql.Open();

                //        MySqlDataAdapter mysqladapter1 = new MySqlDataAdapter();
                //        DataSet dsmysql1 = new DataSet();
                //        MySqlCommand cmdmysql1 = new MySqlCommand(strQueryrol, ConexionMySql);
                //        mysqladapter1.SelectCommand = cmdmysql1;
                //        mysqladapter1.Fill(dsmysql1);
                //        mysqladapter1.Dispose();
                //        cmdmysql1.Dispose();
                //        ConexionMySql.Close();

                //        Session["rol"] = dsmysql1.Tables[0].Rows[0][0].ToString().Trim();
                //        Session["nombre"] = dsmysql1.Tables[0].Rows[0][1].ToString().Trim();

                //        FormsAuthentication.Initialize();
                //        FormsAuthenticationTicket fat = new FormsAuthenticationTicket(1,
                //                username.Text, DateTime.Now, DateTime.Now.AddMinutes(20), false, "SAES", FormsAuthentication.FormsCookiePath);
                //        Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(fat)));

                //        //Response.Redirect("Inicio.aspx");
                //        Response.Redirect("Inicio.aspx");
                //        //Response.Redirect("Prueba_site.aspx");
                //    }
                //    else
                //    {
                //        ///El usuario no existe
                //        mensaje.Text = "Usuario No existe";
                //    }

                //}
                //else
                //{
                //    ///Datos Incorrectos
                //    mensaje.Text = "Usuario/Password Incorrecto";
                //}
            }
            else
            {
                ///Datos vacios

            }
        }
        //protected void entrar2_Click(object sender, EventArgs e)
        //{

        //    if (!String.IsNullOrEmpty(username.Text) || String.IsNullOrEmpty(password.Text))
        //    {
        //        Thread.Sleep(2000);

        //        if (autenticacion(username.Text, password.Text))
        //        {
        //            Session["usuario"] = username.Text;
        //            Session["rol"] = "Alumno";

        //            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);

        //            //Obtiene si el admin ya tiene registro
        //            string strQueryok = "";
        //            strQueryok = " SELECT COUNT(*) FROM tuser WHERE TUSER_CLAVE='" + username.Text + "'";

        //            string strQueryrol = "";
        //            strQueryrol = "SELECT tuser_desc FROM tuser INNER JOIN trole ON trole_clave=tuser_trole_clave WHERE tuser_clave='" + username.Text + "'";
        //            ConexionMySql.Open();
        //            MySqlDataAdapter mysqladapter = new MySqlDataAdapter();
        //            DataSet dsmysql = new DataSet();
        //            MySqlCommand cmdmysql = new MySqlCommand(strQueryok, ConexionMySql);
        //            mysqladapter.SelectCommand = cmdmysql;
        //            mysqladapter.Fill(dsmysql);
        //            mysqladapter.Dispose();
        //            cmdmysql.Dispose();
        //            ConexionMySql.Close();

        //            if (dsmysql.Tables[0].Rows[0][0].ToString() != "0")
        //            {
        //                ConexionMySql.Open();

        //                MySqlDataAdapter mysqladapter1 = new MySqlDataAdapter();
        //                DataSet dsmysql1 = new DataSet();
        //                MySqlCommand cmdmysql1 = new MySqlCommand(strQueryrol, ConexionMySql);
        //                mysqladapter1.SelectCommand = cmdmysql1;
        //                mysqladapter1.Fill(dsmysql1);
        //                mysqladapter1.Dispose();
        //                cmdmysql1.Dispose();
        //                ConexionMySql.Close();


        //                Session["nombre"] = dsmysql1.Tables[0].Rows[0][0].ToString().Trim();

        //                FormsAuthentication.Initialize();
        //                FormsAuthenticationTicket fat = new FormsAuthenticationTicket(1,
        //                        username.Text, DateTime.Now, DateTime.Now.AddMinutes(20), false, "SAES", FormsAuthentication.FormsCookiePath);
        //                Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(fat)));

        //                //Response.Redirect("Inicio.aspx");
        //                Response.Redirect("Inicio.aspx");
        //                //Response.Redirect("Prueba_site.aspx");
        //            }
        //            else
        //            {
        //                ///El usuario no existe
        //                mensaje.Text = "Usuario No existe";
        //            }
        //        }
        //        else if (autenticacion_admin(username.Text, password.Text))
        //        {
        //            Session["rol"] = "";
        //            Session["usuario"] = username.Text;

        //            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);

        //            //Obtiene si el admin ya tiene registro
        //            string strQueryok = "";
        //            strQueryok = " SELECT COUNT(*) FROM tuser WHERE TUSER_CLAVE='" + username.Text + "'";

        //            string strQueryrol = "";
        //            strQueryrol = "SELECT trole_desc,tuser_desc FROM tuser INNER JOIN trole ON trole_clave=tuser_trole_clave WHERE tuser_clave='" + username.Text + "'";
        //            ConexionMySql.Open();
        //            MySqlDataAdapter mysqladapter = new MySqlDataAdapter();
        //            DataSet dsmysql = new DataSet();
        //            MySqlCommand cmdmysql = new MySqlCommand(strQueryok, ConexionMySql);
        //            mysqladapter.SelectCommand = cmdmysql;
        //            mysqladapter.Fill(dsmysql);
        //            mysqladapter.Dispose();
        //            cmdmysql.Dispose();
        //            ConexionMySql.Close();
        //            if (dsmysql.Tables[0].Rows[0][0].ToString() != "0")
        //            {
        //                ConexionMySql.Open();

        //                MySqlDataAdapter mysqladapter1 = new MySqlDataAdapter();
        //                DataSet dsmysql1 = new DataSet();
        //                MySqlCommand cmdmysql1 = new MySqlCommand(strQueryrol, ConexionMySql);
        //                mysqladapter1.SelectCommand = cmdmysql1;
        //                mysqladapter1.Fill(dsmysql1);
        //                mysqladapter1.Dispose();
        //                cmdmysql1.Dispose();
        //                ConexionMySql.Close();

        //                Session["rol"] = dsmysql1.Tables[0].Rows[0][0].ToString().Trim();
        //                Session["nombre"] = dsmysql1.Tables[0].Rows[0][1].ToString().Trim();

        //                FormsAuthentication.Initialize();
        //                FormsAuthenticationTicket fat = new FormsAuthenticationTicket(1,
        //                        username.Text, DateTime.Now, DateTime.Now.AddMinutes(20), false, "SAES", FormsAuthentication.FormsCookiePath);
        //                Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(fat)));

        //                //Response.Redirect("Inicio.aspx");
        //                Response.Redirect("Inicio.aspx");
        //                //Response.Redirect("Prueba_site.aspx");
        //            }
        //            else
        //            {
        //                ///El usuario no existe
        //                mensaje.Text = "Usuario No existe";
        //            }

        //        }
        //        else
        //        {
        //            ///Datos Incorrectos
        //            mensaje.Text = "Usuario/Password Incorrecto";
        //        }
        //    }
        //    else
        //    {
        //        ///Datos vacios

        //    }
        //}



        //private bool autenticacion(string username_text, string password_text)
        // {

        //    MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
        //    MySqlCommand cmd = new MySqlCommand("Login_Alumno", conn);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    try
        //    {
        //        conn.Open();
        //    }
        //    catch
        //    {
        //        //ScriptManager.RegisterStartupScript(this, this.GetType(), "error_bd", "error_bd();", true);
        //        mensaje.Text = ".. ERROR CONEXIÓN BD";
        //    }
        //    cmd.Parameters.AddWithValue("@username", username_text);
        //    cmd.Parameters.AddWithValue("@password_text", password_text);
        //    cmd.Parameters.Add("@Result", MySqlDbType.Int32);
        //    cmd.Parameters["@Result"].Direction = ParameterDirection.Output;
        //    try
        //    {
        //        cmd.ExecuteNonQuery();
        //        return Convert.ToBoolean((Int32)cmd.Parameters["@Result"].Value);
        //    }
        //    catch (Exception ex)
        //    {
        //        registro_log(1, MethodBase.GetCurrentMethod().Name, ex.ToString(), ex.Message.ToString(), "Username: " + username_text + "/Password: " + password_text);
        //        return false;
        //    }
        //    finally
        //    {
        //        conn.Close();

        //    }


        //}

        private bool autenticacion_admin(string username_text, string password_text)
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            MySqlCommand cmd = new MySqlCommand("Login", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error_bd", "error_bd;", true);
            }
            cmd.Parameters.AddWithValue("@username", username_text);
            cmd.Parameters.AddWithValue("@password_text", password_text);
            cmd.Parameters.Add("@Result", MySqlDbType.Int32);
            cmd.Parameters["@Result"].Direction = ParameterDirection.Output;
            try
            {
                cmd.ExecuteNonQuery();
                return Convert.ToBoolean((Int32)cmd.Parameters["@Result"].Value);
            }
            catch (Exception ex)
            {
                registro_log(1, MethodBase.GetCurrentMethod().Name, ex.ToString(), ex.Message.ToString(), "Username: " + username_text + "/Password: " + password_text);
                return true;
            }
            finally
            {
                conn.Close();

            }


        }

        protected void registro_log(int debug_mode, string metodo, string var1, string var2, string var3)
        {
            if (debug_mode == 1)
            {
                StreamWriter sw = new StreamWriter(Server.MapPath("~/logs/error/") + "Error(" + metodo + ")_" + DateTime.Now.ToString("dd_MM_yyyy") + ".txt", true);
                sw.WriteLine("---------------------------------------" + DateTime.Now.ToString() + "--------------------------------------------");
                if (var1 != "") { sw.WriteLine(var1); }
                if (var2 != "") { sw.WriteLine(var2); }
                if (var3 != "") { sw.WriteLine(var3); }
                sw.WriteLine("------------------------------------------------------------------------------------------------------");
                sw.Close();
            }
            else if (debug_mode == 2)
            {
                StreamWriter sw = new StreamWriter(Server.MapPath("~/logs/debug/") + "Debug(" + metodo + ")_" + DateTime.Now.ToString("dd_MM_yyyy") + ".txt", true);
                sw.WriteLine("---------------------------------------" + DateTime.Now.ToString() + "--------------------------------------------");
                if (var1 != "") { sw.WriteLine(var1); }
                if (var2 != "") { sw.WriteLine(var2); }
                if (var3 != "") { sw.WriteLine(var3); }
                sw.WriteLine("------------------------------------------------------------------------------------------------------");
                sw.Close();
            }


        }

        protected void reset_Click(object sender, EventArgs e)
        {
            Session["usuario"] = username.Text;
            Session["tiene_pass"] = "2";
            Response.Redirect("Default2.aspx");
        }
    }
}