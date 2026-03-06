using MySql.Data.MySqlClient;
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
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated || Session["rol"] == null)
            {
                Session.Clear();
                FormsAuthentication.SignOut();
                Response.Redirect(FormsAuthentication.DefaultUrl);
                Response.End();
            }
            else
            {

                /*if (Session["rol"].ToString() == "Alumno")
                {

                    ///Menus///
                    operacion.Visible = false;
                    prospectos.Visible = false;
                    admision.Visible = false;
                    escolares.Visible = false;
                    planeacion.Visible = false;
                    Finanzas.Visible = false;
                    Seguridad.Visible = false;
                    ///SubMenus///
                    tdocumentos.Visible = false;
                    permisos_repo.Visible = false;
                    expedientes.Visible = false;

                }
                else
                {
                    ///SubMenu///
                    carga_alumno.Visible = false;
                    //demograficos.Visible = false;
                }
                */
                string usu = Session["usuario"].ToString();
                carga_alumno.Visible = false;

                MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                conexion.Open();
                MySqlDataAdapter sqladapter = new MySqlDataAdapter();
                //Revisa permisos Menú Operativo
                string QueryOperacion = "select count(*) from tuser, trole, tusme, tmede " +
                    " where tuser_clave = '" + Session["usuario"].ToString() + "' " +
                    " and   trole_clave = tuser_trole_clave " +
                    " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 1 " +
                    " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave ";
                try
                {

                    DataSet dsOp = new DataSet();
                    MySqlCommand commandOp = new MySqlCommand(QueryOperacion, conexion);
                    sqladapter.SelectCommand = commandOp;
                    sqladapter.Fill(dsOp);
                    sqladapter.Dispose();
                    commandOp.Dispose();

                    if (dsOp.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        operacion.Visible = false;
                    }
                    else
                    {
                        string QueryDemograficos = "select count(*) from tuser, trole, tusme, tmede " +
                    " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                    " and tuser_trole_clave=trole_clave " +
                    " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 1 " +
                    " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                    " and tmede_rel = 'demograficos'";

                        try
                        {

                            DataSet dssql1 = new DataSet();
                            MySqlCommand commandsql1 = new MySqlCommand(QueryDemograficos, conexion);
                            sqladapter.SelectCommand = commandsql1;
                            sqladapter.Fill(dssql1);
                            sqladapter.Dispose();
                            commandsql1.Dispose();
                            string paso = dssql1.Tables[0].Rows[0][0].ToString();
                            if (dssql1.Tables[0].Rows[0][0].ToString() == "0")
                            {
                                demograficos.Visible = false;
                            }
                            else
                            {
                                //Permisos para Catálogo de País
                                string Querytpais = " select count(*) from tuser, trole, tusme, tmede " +
                                " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                " and   trole_clave=  tuser_trole_clave " +
                                " and   tusme_tmenu_clave = 1 and tmede_tmenu_clave = tusme_tmenu_clave " +
                                " and tusme_tmede_clave = tmede_clave and tmede_forma = 'tpais' and tusme_select=1 ";
                                DataSet dstpais = new DataSet();
                                MySqlCommand commandtpais = new MySqlCommand(Querytpais, conexion);
                                sqladapter.SelectCommand = commandtpais;
                                sqladapter.Fill(dstpais);
                                sqladapter.Dispose();
                                commandtpais.Dispose();
                                if (dstpais.Tables[0].Rows[0][0].ToString() == "1")
                                {
                                    tpais.Visible = true;
                                }
                                //Permisos para Catálogo de Entidad Federativa
                                string Querytesta = " select count(*) from tuser, trole, tusme, tmede " +
                                " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                " and   trole_clave=  tuser_trole_clave " +
                                " and   tusme_tmenu_clave = 1 and tmede_tmenu_clave = tusme_tmenu_clave " +
                                " and tusme_tmede_clave = tmede_clave and tmede_forma = 'testa' and tusme_select=1 ";
                                DataSet dstesta = new DataSet();
                                MySqlCommand commandtesta = new MySqlCommand(Querytesta, conexion);
                                sqladapter.SelectCommand = commandtesta;
                                sqladapter.Fill(dstesta);
                                sqladapter.Dispose();
                                commandtesta.Dispose();
                                if (dstesta.Tables[0].Rows[0][0].ToString() == "1")
                                {
                                    testa.Visible = true;
                                }

                                //Permisos para Catálogo de Alcaldía/Municipio
                                string Querytdele = " select count(*) from tuser, trole, tusme, tmede " +
                                " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                " and   trole_clave= tuser_trole_clave " +
                                " and   tusme_tmenu_clave = 1 and tmede_tmenu_clave = tusme_tmenu_clave " +
                                " and tusme_tmede_clave = tmede_clave and tmede_forma = 'tdele' and tusme_select=1 ";
                                DataSet dstdele = new DataSet();
                                MySqlCommand commandtdele = new MySqlCommand(Querytdele, conexion);
                                sqladapter.SelectCommand = commandtdele;
                                sqladapter.Fill(dstdele);
                                sqladapter.Dispose();
                                commandtdele.Dispose();
                                if (dstdele.Tables[0].Rows[0][0].ToString() == "1")
                                {
                                    tdele.Visible = true;
                                }
                                //Permisos para Catálogo Códigos Postales
                                string Querytcopo = " select count(*) from tuser, trole, tusme, tmede " +
                                " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                " and   trole_clave= tuser_trole_clave " +
                                " and   tusme_tmenu_clave = 1 and tmede_tmenu_clave = tusme_tmenu_clave " +
                                " and tusme_tmede_clave = tmede_clave and tmede_forma = 'tcopo' and tusme_select=1 ";
                                DataSet dstcopo = new DataSet();
                                MySqlCommand commandtcopo = new MySqlCommand(Querytcopo, conexion);
                                sqladapter.SelectCommand = commandtcopo;
                                sqladapter.Fill(dstcopo);
                                sqladapter.Dispose();
                                commandtcopo.Dispose();
                                if (dstcopo.Tables[0].Rows[0][0].ToString() == "1")
                                {
                                    tcopo.Visible = true;
                                }
                            }

                        }
                        catch
                        {
                            // Response.Redirect("Default.aspx");
                        }

                        string QueryTipos = "select count(*) from tuser, trole, tusme, tmede " +
                            " where tuser_clave = '" + Session["usuario"].ToString() + "' " +
                            " and tuser_trole_clave=  trole_clave " +
                            " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 1 " +
                            " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                            " and tmede_rel = 'tipos'";
                        try
                        {
                            DataSet dssql1 = new DataSet();
                            MySqlCommand commandsql1 = new MySqlCommand(QueryTipos, conexion);
                            sqladapter.SelectCommand = commandsql1;
                            sqladapter.Fill(dssql1);
                            sqladapter.Dispose();
                            commandsql1.Dispose();
                            if (dssql1.Tables[0].Rows[0][0].ToString() == "0")
                            {
                                tipos.Visible = false;
                            }
                            else
                            {
                                //Permisos para Tipo de Dirección
                                string Querytdire = " select count(*) from tuser, trole, tusme, tmede " +
                                " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                " and   trole_clave= tuser_trole_clave " +
                                " and   tusme_tmenu_clave = 1 and tmede_tmenu_clave = tusme_tmenu_clave " +
                                " and tusme_tmede_clave = tmede_clave and tmede_forma = 'tdire' and tusme_select=1 ";
                                DataSet dstdire = new DataSet();
                                MySqlCommand commandtdire = new MySqlCommand(Querytdire, conexion);
                                sqladapter.SelectCommand = commandtdire;
                                sqladapter.Fill(dstdire);
                                sqladapter.Dispose();
                                commandtdire.Dispose();
                                if (dstdire.Tables[0].Rows[0][0].ToString() == "1")
                                {
                                    tdire.Visible = true;
                                }
                                //Permisos para Tipo de Teléfono
                                string Queryttele = " select count(*) from tuser, trole, tusme, tmede " +
                                " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                " and   trole_clave= tuser_trole_clave " +
                                " and   tusme_tmenu_clave = 1 and tmede_tmenu_clave = tusme_tmenu_clave " +
                                " and tusme_tmede_clave = tmede_clave and tmede_forma = 'ttele' and tusme_select=1 ";
                                DataSet dsttele = new DataSet();
                                MySqlCommand commandttele = new MySqlCommand(Queryttele, conexion);
                                sqladapter.SelectCommand = commandttele;
                                sqladapter.Fill(dsttele);
                                sqladapter.Dispose();
                                commandttele.Dispose();
                                if (dsttele.Tables[0].Rows[0][0].ToString() == "1")
                                {
                                    ttele.Visible = true;
                                }
                                //Permisos para Tipo de Correo
                                string Querytmail = " select count(*) from tuser, trole, tusme, tmede " +
                                " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                " and   trole_clave= tuser_trole_clave " +
                                " and   tusme_tmenu_clave = 1 and tmede_tmenu_clave = tusme_tmenu_clave " +
                                " and tusme_tmede_clave = tmede_clave and tmede_forma = 'tmail' and tusme_select=1 ";
                                DataSet dstmail = new DataSet();
                                MySqlCommand commandtmail = new MySqlCommand(Querytmail, conexion);
                                sqladapter.SelectCommand = commandtmail;
                                sqladapter.Fill(dstmail);
                                sqladapter.Dispose();
                                commandtmail.Dispose();
                                if (dstmail.Tables[0].Rows[0][0].ToString() == "1")
                                {
                                    tmail.Visible = true;
                                }
                                //Permisos para Tipo de Contacto
                                string Querytcont = " select count(*) from tuser, trole, tusme, tmede " +
                                " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                " and   trole_clave= tuser_trole_clave " +
                                " and   tusme_tmenu_clave = 1 and tmede_tmenu_clave = tusme_tmenu_clave " +
                                " and tusme_tmede_clave = tmede_clave and tmede_forma = 'tcont' and tusme_select=1 ";
                                DataSet dstcont = new DataSet();
                                MySqlCommand commandtcont = new MySqlCommand(Querytcont, conexion);
                                sqladapter.SelectCommand = commandtcont;
                                sqladapter.Fill(dstcont);
                                sqladapter.Dispose();
                                commandtcont.Dispose();
                                if (dstcont.Tables[0].Rows[0][0].ToString() == "1")
                                {
                                    tcont.Visible = true;
                                }
                            }

                        }
                        catch
                        {
                            // Response.Redirect("Default.aspx");
                        }
                        //Permisos para el menú de Campus
                        string QueryCampus = "select count(*) from tuser, trole, tusme, tmede " +
                            " where tuser_clave = '" + Session["usuario"].ToString() + "' " +
                            " and tuser_trole_clave= trole_clave " +
                            " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 1 " +
                            " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                            " and tmede_rel = 'campus'";
                        try
                        {
                            DataSet dssql1 = new DataSet();
                            MySqlCommand commandsql1 = new MySqlCommand(QueryCampus, conexion);
                            sqladapter.SelectCommand = commandsql1;
                            sqladapter.Fill(dssql1);
                            sqladapter.Dispose();
                            commandsql1.Dispose();
                            if (dssql1.Tables[0].Rows[0][0].ToString() == "0")
                            {
                                campus.Visible = false;
                            }
                            else
                            {
                                //Permisos para Catálogo de Campus
                                string Querytcamp = " select count(*) from tuser, trole, tusme, tmede " +
                                " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                " and   trole_clave= tuser_trole_clave " +
                                " and   tusme_tmenu_clave = 1 and tmede_tmenu_clave = tusme_tmenu_clave " +
                                " and tusme_tmede_clave = tmede_clave and tmede_forma = 'tcamp' and tusme_select=1 ";
                                DataSet dstcamp = new DataSet();
                                MySqlCommand commandtcamp = new MySqlCommand(Querytcamp, conexion);
                                sqladapter.SelectCommand = commandtcamp;
                                sqladapter.Fill(dstcamp);
                                sqladapter.Dispose();
                                commandtcamp.Dispose();
                                if (dstcamp.Tables[0].Rows[0][0].ToString() == "1")
                                {
                                    tcamp.Visible = true;
                                }
                                //Permisos para Programas x Campus
                                string Querytcapr = " select count(*) from tuser, trole, tusme, tmede " +
                                " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                " and   trole_clave= tuser_trole_clave " +
                                " and   tusme_tmenu_clave = 1 and tmede_tmenu_clave = tusme_tmenu_clave " +
                                " and tusme_tmede_clave = tmede_clave and tmede_forma = 'tcapr' and tusme_select=1 ";
                                DataSet dstcapr = new DataSet();
                                MySqlCommand commandtcapr = new MySqlCommand(Querytcapr, conexion);
                                sqladapter.SelectCommand = commandtcapr;
                                sqladapter.Fill(dstcapr);
                                sqladapter.Dispose();
                                commandtcapr.Dispose();
                                if (dstcapr.Tables[0].Rows[0][0].ToString() == "1")
                                {
                                    tcapr.Visible = true;
                                }
                                //Permisos para Parámetros de cobranza
                                string Querytpaco = " select count(*) from tuser, trole, tusme, tmede " +
                                " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                " and   trole_clave= tuser_trole_clave " +
                                " and   tusme_tmenu_clave = 1 and tmede_tmenu_clave = tusme_tmenu_clave " +
                                " and tusme_tmede_clave = tmede_clave and tmede_forma = 'tpaco' and tusme_select=1 ";
                                DataSet dstpaco = new DataSet();
                                MySqlCommand commandtpaco = new MySqlCommand(Querytpaco, conexion);
                                sqladapter.SelectCommand = commandtpaco;
                                sqladapter.Fill(dstpaco);
                                sqladapter.Dispose();
                                commandtpaco.Dispose();
                                if (dstpaco.Tables[0].Rows[0][0].ToString() == "1")
                                {
                                    tpaco.Visible = true;
                                }
                                //Permisos para Secuencias por campus
                                string Querytcseq = " select count(*) from tuser, trole, tusme, tmede " +
                                " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                " and   trole_clave= tuser_trole_clave " +
                                " and   tusme_tmenu_clave = 1 and tmede_tmenu_clave = tusme_tmenu_clave " +
                                " and tusme_tmede_clave = tmede_clave and tmede_forma = 'tcseq' and tusme_select=1 ";
                                DataSet dstcseq = new DataSet();
                                MySqlCommand commandtcseq = new MySqlCommand(Querytcseq, conexion);
                                sqladapter.SelectCommand = commandtcseq;
                                sqladapter.Fill(dstcseq);
                                sqladapter.Dispose();
                                commandtcseq.Dispose();
                                if (dstcseq.Tables[0].Rows[0][0].ToString() == "1")
                                {
                                    tcseq.Visible = true;
                                }
                            }

                        }
                        catch
                        {
                            // Response.Redirect("Default.aspx");
                        }

                        string QueryPeriodos = "select count(*) from tuser, trole, tusme, tmede " +
                            " where tuser_clave = '" + Session["usuario"].ToString() + "' " +
                            " and   tuser_trole_clave=trole_clave " +
                            " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 1 " +
                            " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                            " and tmede_forma = 'tpees' ";
                        try
                        {
                            DataSet dssql1 = new DataSet();
                            MySqlCommand commandsql1 = new MySqlCommand(QueryPeriodos, conexion);
                            sqladapter.SelectCommand = commandsql1;
                            sqladapter.Fill(dssql1);
                            sqladapter.Dispose();
                            commandsql1.Dispose();
                            if (dssql1.Tables[0].Rows[0][0].ToString() != "0")
                            {
                                periodos.Visible = true;
                            }

                        }
                        catch
                        {
                            // Response.Redirect("Default.aspx");
                        }
                    }

                }
                catch
                {
                    // Response.Redirect("Default.aspx");
                }




                //Revisa permisos Menú Prospectos
                string QueryProspectos = "select count(*) from tuser, trole, tusme, tmede " +
                    " where tuser_clave = '" + Session["usuario"].ToString() + "' " +
                    " and   trole_clave = tuser_trole_clave " +
                    " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 2 " +
                    " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave ";
                try
                {

                    DataSet dssql1 = new DataSet();
                    MySqlCommand commandsql1 = new MySqlCommand(QueryProspectos, conexion);
                    sqladapter.SelectCommand = commandsql1;
                    sqladapter.Fill(dssql1);
                    sqladapter.Dispose();
                    commandsql1.Dispose();
                    string paso = dssql1.Tables[0].Rows[0][0].ToString();
                    if (dssql1.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        prospectos.Visible = false;
                    }
                    else
                    {
                        //Opciones para el menú de prospectos
                    }

                }
                catch
                {
                    // Response.Redirect("Default.aspx");
                }

                //Revisa permisos Menú Admisión
                string QueryAdmision = "select count(*) from tuser, trole, tusme, tmede " +
                    " where tuser_clave = '" + Session["usuario"].ToString() + "' " +
                    " and   trole_clave= tuser_trole_clave " +
                    " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 3 " +
                    " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave ";
                try
                {

                    DataSet dssql1 = new DataSet();
                    MySqlCommand commandsql1 = new MySqlCommand(QueryAdmision, conexion);
                    sqladapter.SelectCommand = commandsql1;
                    sqladapter.Fill(dssql1);
                    sqladapter.Dispose();
                    commandsql1.Dispose();
                    string paso = dssql1.Tables[0].Rows[0][0].ToString();
                    if (dssql1.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        admision.Visible = false;
                    }
                    else
                    {
                        //Permisos para el catálogo de Estatus de la solicitud
                        string Querytsto = "select count(*) from tuser, trole, tusme, tmede " +
                        " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                        " and tuser_trole_clave = trole_clave " +
                        " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 3 " +
                        " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                        " and tmede_forma = 'tstso' ";
                        try
                        {
                            DataSet dststso = new DataSet();
                            MySqlCommand commandtstso = new MySqlCommand(Querytsto, conexion);
                            sqladapter.SelectCommand = commandtstso;
                            sqladapter.Fill(dststso);
                            sqladapter.Dispose();
                            commandtstso.Dispose();
                            if (dststso.Tables[0].Rows[0][0].ToString() != "0")
                            {
                                tstso.Visible = true;
                            }

                        }
                        catch
                        {
                            // Response.Redirect("Default.aspx");
                        }
                        //Permisos para el catálogo de Tipos de Ingreso
                        //string Queryttiin = "select count(*) from tuser, trole, tusme, tmede " +
                        //" where tuser_clave='" + Session["usuario"].ToString() + "'" +
                        //" and tuser_trole_clave = trole_clave " +
                        //" and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 3 " +
                        //" and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                        //" and tmede_forma = 'ttiin' ";
                        //try
                        //{
                        //    DataSet dsttiin = new DataSet();
                        //    MySqlCommand commandttiin = new MySqlCommand(Queryttiin, conexion);
                        //    sqladapter.SelectCommand = commandttiin;
                        //    sqladapter.Fill(dsttiin);
                        //    sqladapter.Dispose();
                        //    commandttiin.Dispose();
                        //    if (dsttiin.Tables[0].Rows[0][0].ToString() != "0")
                        //    {
                        //        ttiin.Visible = true;
                        //    }

                        //}
                        //catch
                        //{
                        //    // Response.Redirect("Default.aspx");
                        //}
                        //Permisos para el submenú de Solicitud
                        string QuerySolicitud = "select count(*) from tuser, trole, tusme, tmede " +
                            " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                            " and tuser_trole_clave = trole_clave " +
                            " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 3 " +
                            " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                            " and tmede_rel = 'solicitud' ";
                        try
                        {
                            DataSet dssol = new DataSet();
                            MySqlCommand commandsol = new MySqlCommand(QuerySolicitud, conexion);
                            sqladapter.SelectCommand = commandsol;
                            sqladapter.Fill(dssol);
                            sqladapter.Dispose();
                            commandsol.Dispose();
                            if (dssol.Tables[0].Rows[0][0].ToString() == "0")
                            {
                                solicitud.Visible = false;
                            }
                            else
                            {
                                //Permisos para Datos Personales
                                string Querytpers = "select count(*) from tuser, trole, tusme, tmede " +
                                " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                " and tuser_trole_clave = trole_clave " +
                                " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 3 " +
                                " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                                " and tmede_forma = 'tpers' ";
                                DataSet dstpers = new DataSet();
                                MySqlCommand commandtpers = new MySqlCommand(Querytpers, conexion);
                                sqladapter.SelectCommand = commandtpers;
                                sqladapter.Fill(dstpers);
                                sqladapter.Dispose();
                                commandtpers.Dispose();
                                if (dstpers.Tables[0].Rows[0][0].ToString() == "1")
                                {
                                    tpers.Visible = true;
                                }
                                //Permisos para Datos Direcciones
                                string Querytaldi = "select count(*) from tuser, trole, tusme, tmede " +
                                " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                " and tuser_trole_clave = trole_clave " +
                                " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 3 " +
                                " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                                " and tmede_forma = 'taldi' ";
                                DataSet dstaldi = new DataSet();
                                MySqlCommand commandtaldi = new MySqlCommand(Querytaldi, conexion);
                                sqladapter.SelectCommand = commandtaldi;
                                sqladapter.Fill(dstaldi);
                                sqladapter.Dispose();
                                commandtaldi.Dispose();
                                if (dstaldi.Tables[0].Rows[0][0].ToString() == "1")
                                {
                                    taldi.Visible = true;
                                }
                                //Permisos para Datos Teléfonos
                                string Querytalte = "select count(*) from tuser, trole, tusme, tmede " +
                                " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                " and tuser_trole_clave = trole_clave " +
                                " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 3 " +
                                " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                                " and tmede_forma = 'talte' ";
                                DataSet dstalte = new DataSet();
                                MySqlCommand commandtalte = new MySqlCommand(Querytalte, conexion);
                                sqladapter.SelectCommand = commandtalte;
                                sqladapter.Fill(dstalte);
                                sqladapter.Dispose();
                                commandtalte.Dispose();
                                if (dstalte.Tables[0].Rows[0][0].ToString() == "1")
                                {
                                    talte.Visible = true;
                                }
                                //Permisos para Datos Correo
                                string Querytalco = "select count(*) from tuser, trole, tusme, tmede " +
                                " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                " and tuser_trole_clave = trole_clave " +
                                " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 3 " +
                                " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                                " and tmede_forma = 'talco' ";
                                DataSet dstalco = new DataSet();
                                MySqlCommand commandtalco = new MySqlCommand(Querytalco, conexion);
                                sqladapter.SelectCommand = commandtalco;
                                sqladapter.Fill(dstalco);
                                sqladapter.Dispose();
                                commandtalco.Dispose();
                                if (dstalco.Tables[0].Rows[0][0].ToString() == "1")
                                {
                                    talco.Visible = true;
                                }
                                //Permisos para Solicitud Admisión
                                string Querytadmi = "select count(*) from tuser, trole, tusme, tmede " +
                                " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                " and tuser_trole_clave = trole_clave " +
                                " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 3 " +
                                " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                                " and tmede_forma = 'tadmi' ";
                                DataSet dstadmi = new DataSet();
                                MySqlCommand commandtadmi = new MySqlCommand(Querytadmi, conexion);
                                sqladapter.SelectCommand = commandtadmi;
                                sqladapter.Fill(dstadmi);
                                sqladapter.Dispose();
                                commandtadmi.Dispose();
                                if (dstadmi.Tables[0].Rows[0][0].ToString() == "1")
                                {
                                    tadmi.Visible = true;
                                }
                                //Permisos para Relación Documentos
                                string Querytredo = "select count(*) from tuser, trole, tusme, tmede " +
                                " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                " and tuser_trole_clave = trole_clave " +
                                " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 3 " +
                                " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                                " and tmede_forma = 'tredo' ";
                                DataSet dstredo = new DataSet();
                                MySqlCommand commandtredo = new MySqlCommand(Querytredo, conexion);
                                sqladapter.SelectCommand = commandtredo;
                                sqladapter.Fill(dstredo);
                                sqladapter.Dispose();
                                commandtredo.Dispose();
                                if (dstredo.Tables[0].Rows[0][0].ToString() == "1")
                                {
                                    tredo.Visible = true;
                                }
                            }

                        }
                        catch
                        {
                            // Response.Redirect("Default.aspx");
                        }
                        //Permisos para el submenú de Documentos
                        string QueryDocumentos = "select count(*) from tuser, trole, tusme, tmede " +
                            " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                            " and tuser_trole_clave = trole_clave " +
                            " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 3 " +
                            " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                            " and tmede_rel = 'documentos' ";
                        try
                        {
                            DataSet dsdocu = new DataSet();
                            MySqlCommand commanddocu = new MySqlCommand(QueryDocumentos, conexion);
                            sqladapter.SelectCommand = commanddocu;
                            sqladapter.Fill(dsdocu);
                            sqladapter.Dispose();
                            commanddocu.Dispose();
                            if (dsdocu.Tables[0].Rows[0][0].ToString() == "0")
                            {
                                documentos.Visible = false;
                            }
                            else
                            {
                                //Permisos para Catálogo Documentos
                                string Querytdocu = "select count(*) from tuser, trole, tusme, tmede " +
                                " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                " and tuser_trole_clave = trole_clave " +
                                " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 3 " +
                                " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                                " and tmede_forma = 'tdocu' ";
                                DataSet dstdocu = new DataSet();
                                MySqlCommand commandtdocu = new MySqlCommand(Querytdocu, conexion);
                                sqladapter.SelectCommand = commandtdocu;
                                sqladapter.Fill(dstdocu);
                                sqladapter.Dispose();
                                commandtdocu.Dispose();
                                if (dstdocu.Tables[0].Rows[0][0].ToString() == "1")
                                {
                                    tdocu.Visible = true;
                                }
                                //Permisos para Configuración Documentos
                                string Querytcodo = "select count(*) from tuser, trole, tusme, tmede " +
                                " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                " and tuser_trole_clave = trole_clave " +
                                " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 3 " +
                                " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                                " and tmede_forma = 'tcodo' ";
                                DataSet dstcodo = new DataSet();
                                MySqlCommand commandtcodo = new MySqlCommand(Querytcodo, conexion);
                                sqladapter.SelectCommand = commandtcodo;
                                sqladapter.Fill(dstcodo);
                                sqladapter.Dispose();
                                commandtcodo.Dispose();
                                if (dstcodo.Tables[0].Rows[0][0].ToString() == "1")
                                {
                                    tcodo.Visible = true;
                                }
                                //Permisos para Estatus Documentos
                                string Querytstdo = "select count(*) from tuser, trole, tusme, tmede " +
                                " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                " and tuser_trole_clave = trole_clave " +
                                " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 3 " +
                                " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                                " and tmede_forma = 'tstdo' ";
                                DataSet dststdo = new DataSet();
                                MySqlCommand commandtstdo = new MySqlCommand(Querytstdo, conexion);
                                sqladapter.SelectCommand = commandtstdo;
                                sqladapter.Fill(dststdo);
                                sqladapter.Dispose();
                                commandtstdo.Dispose();
                                if (dststdo.Tables[0].Rows[0][0].ToString() == "1")
                                {
                                    tstdo.Visible = true;
                                }
                            }

                            //Permisos para Escuelas de Procedencia
                            string Querytespr = "select count(*) from tuser, trole, tusme, tmede " +
                            " where tuser_clave = '" + Session["usuario"].ToString() + "' " +
                            " and   tuser_trole_clave = trole_clave " +
                            " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 3 " +
                            " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                            " and tmede_forma = 'tespr' ";
                            try
                            {
                                DataSet dstespr = new DataSet();
                                MySqlCommand commandtespr = new MySqlCommand(Querytespr, conexion);
                                sqladapter.SelectCommand = commandtespr;
                                sqladapter.Fill(dstespr);
                                sqladapter.Dispose();
                                commandtespr.Dispose();
                                if (dstespr.Tables[0].Rows[0][0].ToString() != "0")
                                {
                                    tespr.Visible = true;
                                }

                            }
                            catch
                            {
                                // Response.Redirect("Default.aspx");
                            }
                            //Permisos para Pronóstico Nuevo ingreso
                            string Querytpron = "select count(*) from tuser, trole, tusme, tmede " +
                            " where tuser_clave = '" + Session["usuario"].ToString() + "' " +
                            " and   tuser_trole_clave = trole_clave " +
                            " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 3 " +
                            " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                            " and tmede_forma = 'tpron' ";
                            try
                            {
                                DataSet dstpron = new DataSet();
                                MySqlCommand commandtpron = new MySqlCommand(Querytpron, conexion);
                                sqladapter.SelectCommand = commandtpron;
                                sqladapter.Fill(dstpron);
                                sqladapter.Dispose();
                                commandtpron.Dispose();
                                if (dstpron.Tables[0].Rows[0][0].ToString() != "0")
                                {
                                    tpron.Visible = true;
                                }

                            }
                            catch
                            {
                            }

                            //Permisos para el submenú de Contactos
                            string QueryContacto = "select count(*) from tuser, trole, tusme, tmede " +
                                " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                " and tuser_trole_clave = trole_clave " +
                                " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 3 " +
                                " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                                " and tmede_rel = 'contacto' ";
                            try
                            {
                                DataSet dstcont = new DataSet();
                                MySqlCommand commandtcont = new MySqlCommand(QueryContacto, conexion);
                                sqladapter.SelectCommand = commandtcont;
                                sqladapter.Fill(dstcont);
                                sqladapter.Dispose();
                                commandtcont.Dispose();
                                if (dstcont.Tables[0].Rows[0][0].ToString() == "0")
                                {
                                    contacto.Visible = false;
                                }
                                else
                                {
                                    //Permisos para Datos personales contacto
                                    string Querytcoda = "select count(*) from tuser, trole, tusme, tmede " +
                                    " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                    " and tuser_trole_clave = trole_clave " +
                                    " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 3 " +
                                    " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                                    " and tmede_forma = 'tcoda' ";
                                    DataSet dstcoda = new DataSet();
                                    MySqlCommand commandtcoda = new MySqlCommand(Querytcoda, conexion);
                                    sqladapter.SelectCommand = commandtcoda;
                                    sqladapter.Fill(dstcoda);
                                    sqladapter.Dispose();
                                    commandtcoda.Dispose();
                                    if (dstcoda.Tables[0].Rows[0][0].ToString() == "1")
                                    {
                                        tcoda.Visible = true;
                                    }
                                    //Permisos para Direccion contacto
                                    string Querytcodi = "select count(*) from tuser, trole, tusme, tmede " +
                                    " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                    " and tuser_trole_clave = trole_clave " +
                                    " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 3 " +
                                    " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                                    " and tmede_forma = 'tcodi' ";
                                    DataSet dstcodi = new DataSet();
                                    MySqlCommand commandtcodi = new MySqlCommand(Querytcodi, conexion);
                                    sqladapter.SelectCommand = commandtcodi;
                                    sqladapter.Fill(dstcodi);
                                    sqladapter.Dispose();
                                    commandtcodi.Dispose();
                                    if (dstcodi.Tables[0].Rows[0][0].ToString() == "1")
                                    {
                                        tcodi.Visible = true;
                                    }
                                    //Permisos para Teléfono contacto
                                    string Querytcote = "select count(*) from tuser, trole, tusme, tmede " +
                                    " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                    " and tuser_trole_clave = trole_clave " +
                                    " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 3 " +
                                    " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                                    " and tmede_forma = 'tcote' ";
                                    DataSet dstcote = new DataSet();
                                    MySqlCommand commandtcote = new MySqlCommand(Querytcote, conexion);
                                    sqladapter.SelectCommand = commandtcote;
                                    sqladapter.Fill(dstcote);
                                    sqladapter.Dispose();
                                    commandtcote.Dispose();
                                    if (dstcote.Tables[0].Rows[0][0].ToString() == "1")
                                    {
                                        tcote.Visible = true;
                                    }

                                    //Permisos para Correo contacto
                                    string Querytcnco = "select count(*) from tuser, trole, tusme, tmede " +
                                    " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                    " and tuser_trole_clave = trole_clave " +
                                    " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 3 " +
                                    " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                                    " and tmede_forma = 'tcnco' ";
                                    DataSet dstcnco = new DataSet();
                                    MySqlCommand commandtcnco = new MySqlCommand(Querytcnco, conexion);
                                    sqladapter.SelectCommand = commandtcnco;
                                    sqladapter.Fill(dstcnco);
                                    sqladapter.Dispose();
                                    commandtcnco.Dispose();
                                    if (dstcnco.Tables[0].Rows[0][0].ToString() == "1")
                                    {
                                        tcnco.Visible = true;
                                    }
                                }
                            }
                            catch
                            {
                                //
                            }

                        }
                        catch
                        {
                            // Response.Redirect("Default.aspx");
                        }
                    }

                    string QueryRepositorio = "select count(*) from tuser, trole, tusme, tmede " +
                    " where tuser_clave = '" + Session["usuario"].ToString() + "' " +
                    " and   trole_clave= tuser_trole_clave " +
                    " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 8 " +
                    " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave ";
                    try
                    {

                        DataSet dsRepo = new DataSet();
                        MySqlCommand commandRepo = new MySqlCommand(QueryRepositorio, conexion);
                        sqladapter.SelectCommand = commandRepo;
                        sqladapter.Fill(dsRepo);
                        sqladapter.Dispose();
                        commandRepo.Dispose();
                        if (dsRepo.Tables[0].Rows[0][0].ToString() == "0")
                        {
                            repositorio.Visible = false;
                        }
                        else
                        {
                            //Permisos para Configuración de Documentos 
                            string QueryDocto = "select count(*) from tuser, trole, tusme, tmede " +
                            " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                            " and tuser_trole_clave = trole_clave " +
                            " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 8 " +
                            " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                            " and tmede_forma = 'Docto' ";
                            try
                            {
                                DataSet dsDocto = new DataSet();
                                MySqlCommand commandDocto = new MySqlCommand(QueryDocto, conexion);
                                sqladapter.SelectCommand = commandDocto;
                                sqladapter.Fill(dsDocto);
                                sqladapter.Dispose();
                                commandDocto.Dispose();
                                if (dsDocto.Tables[0].Rows[0][0].ToString() != "0")
                                {
                                    Docto.Visible = true;
                                }

                            }
                            catch
                            {
                                // Response.Redirect("Default.aspx");
                            }
                            //Permisos para Configuración de Permisos
                            string QueryPerm = "select count(*) from tuser, trole, tusme, tmede " +
                            " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                            " and tuser_trole_clave = trole_clave " +
                            " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 8 " +
                            " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                            " and tmede_forma = 'Perm' ";
                            try
                            {
                                DataSet dsPerm = new DataSet();
                                MySqlCommand commandPerm = new MySqlCommand(QueryPerm, conexion);
                                sqladapter.SelectCommand = commandPerm;
                                sqladapter.Fill(dsPerm);
                                sqladapter.Dispose();
                                commandPerm.Dispose();
                                if (dsPerm.Tables[0].Rows[0][0].ToString() != "0")
                                {
                                    Perm.Visible = true;
                                }

                            }
                            catch
                            {
                                // Response.Redirect("Default.aspx");
                            }
                            //Permisos para Revisar Expedientes
                            string QueryList = "select count(*) from tuser, trole, tusme, tmede " +
                            " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                            " and tuser_trole_clave = trole_clave " +
                            " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 8 " +
                            " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                            " and tmede_forma = 'List' ";
                            try
                            {
                                DataSet dsList = new DataSet();
                                MySqlCommand commandList = new MySqlCommand(QueryList, conexion);
                                sqladapter.SelectCommand = commandList;
                                sqladapter.Fill(dsList);
                                sqladapter.Dispose();
                                commandList.Dispose();
                                if (dsList.Tables[0].Rows[0][0].ToString() != "0")
                                {
                                    List.Visible = true;
                                }

                            }
                            catch
                            {
                                // Response.Redirect("Default.aspx");
                            }
                        }
                    }
                    catch
                    {
                        //
                    }
                    //Revisa permisos Menú Planeación académica
                    string QueryPlaneacion = "select count(*) from tuser, trole, tusme, tmede " +
                        " where tuser_clave = '" + Session["usuario"].ToString() + "' " +
                        " and   trole_clave= tuser_trole_clave " +
                        " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 5 " +
                        " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave ";
                    try
                    {

                        DataSet dsplan = new DataSet();
                        MySqlCommand commandplan = new MySqlCommand(QueryPlaneacion, conexion);
                        sqladapter.SelectCommand = commandplan;
                        sqladapter.Fill(dsplan);
                        sqladapter.Dispose();
                        commandplan.Dispose();
                        if (dsplan.Tables[0].Rows[0][0].ToString() == "0")
                        {
                            planeacion.Visible = false;
                        }
                        else
                        {
                            //Permisos para el catálogo de Niveles Educativos
                            string Querytnive = "select count(*) from tuser, trole, tusme, tmede " +
                            " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                            " and tuser_trole_clave = trole_clave " +
                            " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 5 " +
                            " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                            " and tmede_forma = 'tnive' ";
                            try
                            {
                                DataSet dstnive = new DataSet();
                                MySqlCommand commandtnive = new MySqlCommand(Querytnive, conexion);
                                sqladapter.SelectCommand = commandtnive;
                                sqladapter.Fill(dstnive);
                                sqladapter.Dispose();
                                commandtnive.Dispose();
                                if (dstnive.Tables[0].Rows[0][0].ToString() != "0")
                                {
                                    tnive.Visible = true;
                                }

                            }
                            catch
                            {
                                // Response.Redirect("Default.aspx");
                            }
                            //Permisos para el catálogo de Colegios
                            string Querytcole = "select count(*) from tuser, trole, tusme, tmede " +
                            " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                            " and tuser_trole_clave = trole_clave " +
                            " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 5 " +
                            " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                            " and tmede_forma = 'tcole' ";
                            try
                            {
                                DataSet dstcole = new DataSet();
                                MySqlCommand commandtcole = new MySqlCommand(Querytcole, conexion);
                                sqladapter.SelectCommand = commandtcole;
                                sqladapter.Fill(dstcole);
                                sqladapter.Dispose();
                                commandtcole.Dispose();
                                if (dstcole.Tables[0].Rows[0][0].ToString() != "0")
                                {
                                    tcole.Visible = true;
                                }

                            }
                            catch
                            {
                                // Response.Redirect("Default.aspx");
                            }
                            //Permisos para el catálogo de Modalidades
                            string Querytmoda = "select count(*) from tuser, trole, tusme, tmede " +
                            " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                            " and tuser_trole_clave = trole_clave " +
                            " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 5 " +
                            " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                            " and tmede_forma = 'tmoda' ";
                            try
                            {
                                DataSet dstmoda = new DataSet();
                                MySqlCommand commandtmoda = new MySqlCommand(Querytmoda, conexion);
                                sqladapter.SelectCommand = commandtmoda;
                                sqladapter.Fill(dstmoda);
                                sqladapter.Dispose();
                                commandtmoda.Dispose();
                                if (dstmoda.Tables[0].Rows[0][0].ToString() != "0")
                                {
                                    tmoda.Visible = true;
                                }

                            }
                            catch
                            {
                                // Response.Redirect("Default.aspx");
                            }
                            //Permisos para el catálogo de Programas académicos
                            string Querytprog = "select count(*) from tuser, trole, tusme, tmede " +
                            " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                            " and tuser_trole_clave = trole_clave " +
                            " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 5 " +
                            " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                            " and tmede_forma = 'tprog' ";
                            try
                            {
                                DataSet dstprog = new DataSet();
                                MySqlCommand commandtprog = new MySqlCommand(Querytprog, conexion);
                                sqladapter.SelectCommand = commandtprog;
                                sqladapter.Fill(dstprog);
                                sqladapter.Dispose();
                                commandtprog.Dispose();
                                if (dstprog.Tables[0].Rows[0][0].ToString() != "0")
                                {
                                    tprog.Visible = true;
                                }

                            }
                            catch
                            {
                                // Response.Redirect("Default.aspx");
                            }
                            //Permisos para el catálogo de Areas
                            string Querytarea = "select count(*) from tuser, trole, tusme, tmede " +
                            " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                            " and tuser_trole_clave = trole_clave " +
                            " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 5 " +
                            " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                            " and tmede_forma = 'tarea' ";
                            try
                            {
                                DataSet dstarea = new DataSet();
                                MySqlCommand commandtarea = new MySqlCommand(Querytarea, conexion);
                                sqladapter.SelectCommand = commandtarea;
                                sqladapter.Fill(dstarea);
                                sqladapter.Dispose();
                                commandtarea.Dispose();
                                if (dstarea.Tables[0].Rows[0][0].ToString() != "0")
                                {
                                    tarea.Visible = true;
                                }

                            }
                            catch
                            {
                                // Response.Redirect("Default.aspx");
                            }
                            //Permisos para el catálogo de Materias
                            string Querytmate = "select count(*) from tuser, trole, tusme, tmede " +
                            " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                            " and tuser_trole_clave = trole_clave " +
                            " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 5 " +
                            " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                            " and tmede_forma = 'tmate' ";
                            try
                            {
                                DataSet dstmate = new DataSet();
                                MySqlCommand commandtmate = new MySqlCommand(Querytmate, conexion);
                                sqladapter.SelectCommand = commandtmate;
                                sqladapter.Fill(dstmate);
                                sqladapter.Dispose();
                                commandtmate.Dispose();
                                if (dstmate.Tables[0].Rows[0][0].ToString() != "0")
                                {
                                    tmate.Visible = true;
                                }

                            }
                            catch
                            {
                                // Response.Redirect("Default.aspx");
                            }
                            //Permisos para el Plan de estudios
                            string Querytplan = "select count(*) from tuser, trole, tusme, tmede " +
                            " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                            " and tuser_trole_clave = trole_clave " +
                            " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 5 " +
                            " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                            " and tmede_forma = 'tplan' ";
                            try
                            {
                                DataSet dstplan = new DataSet();
                                MySqlCommand commandtplan = new MySqlCommand(Querytplan, conexion);
                                sqladapter.SelectCommand = commandtplan;
                                sqladapter.Fill(dstplan);
                                sqladapter.Dispose();
                                commandtplan.Dispose();
                                if (dstplan.Tables[0].Rows[0][0].ToString() != "0")
                                {
                                    tplan.Visible = true;
                                }

                            }
                            catch
                            {
                                // Response.Redirect("Default.aspx");
                            }
                            //Permisos para el submenú de Plantilla Docente
                            string QueryPlantilla = "select count(*) from tuser, trole, tusme, tmede " +
                                " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                " and tuser_trole_clave = trole_clave " +
                                " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 5 " +
                                " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                                " and tmede_rel = 'docente' ";
                            try
                            {
                                DataSet dstcont = new DataSet();
                                MySqlCommand commandtcont = new MySqlCommand(QueryPlantilla, conexion);
                                sqladapter.SelectCommand = commandtcont;
                                sqladapter.Fill(dstcont);
                                sqladapter.Dispose();
                                commandtcont.Dispose();
                                if (dstcont.Tables[0].Rows[0][0].ToString() == "0")
                                {
                                    Docente.Visible = false;
                                }
                                else
                                {
                                    //Permisos para Datos personales docente
                                    string Querytpdoc = "select count(*) from tuser, trole, tusme, tmede " +
                                    " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                    " and tuser_trole_clave = trole_clave " +
                                    " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 5 " +
                                    " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                                    " and tmede_forma = 'tpdoc' ";
                                    DataSet dstpdoc = new DataSet();
                                    MySqlCommand commandtpdoc = new MySqlCommand(Querytpdoc, conexion);
                                    sqladapter.SelectCommand = commandtpdoc;
                                    sqladapter.Fill(dstpdoc);
                                    sqladapter.Dispose();
                                    commandtpdoc.Dispose();
                                    if (dstpdoc.Tables[0].Rows[0][0].ToString() == "1")
                                    {
                                        tpdoc.Visible = true;
                                    }
                                    //Permisos para Direccion docente
                                    string Querytdodi = "select count(*) from tuser, trole, tusme, tmede " +
                                    " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                    " and tuser_trole_clave = trole_clave " +
                                    " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 5 " +
                                    " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                                    " and tmede_forma = 'tdodi' ";
                                    DataSet dstdodi = new DataSet();
                                    MySqlCommand commandtdodi = new MySqlCommand(Querytdodi, conexion);
                                    sqladapter.SelectCommand = commandtdodi;
                                    sqladapter.Fill(dstdodi);
                                    sqladapter.Dispose();
                                    commandtdodi.Dispose();
                                    if (dstdodi.Tables[0].Rows[0][0].ToString() == "1")
                                    {
                                        tdodi.Visible = true;
                                    }
                                    //Permisos para Teléfono docente
                                    string Querytdote = "select count(*) from tuser, trole, tusme, tmede " +
                                    " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                    " and tuser_trole_clave = trole_clave " +
                                    " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 5 " +
                                    " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                                    " and tmede_forma = 'tdote' ";
                                    DataSet dstdote = new DataSet();
                                    MySqlCommand commandtdote = new MySqlCommand(Querytdote, conexion);
                                    sqladapter.SelectCommand = commandtdote;
                                    sqladapter.Fill(dstdote);
                                    sqladapter.Dispose();
                                    commandtdote.Dispose();
                                    if (dstdote.Tables[0].Rows[0][0].ToString() == "1")
                                    {
                                        tdote.Visible = true;
                                    }

                                    //Permisos para Correo docente
                                    string Querytdoco = "select count(*) from tuser, trole, tusme, tmede " +
                                    " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                    " and tuser_trole_clave = trole_clave " +
                                    " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 5 " +
                                    " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                                    " and tmede_forma = 'tdoco' ";
                                    DataSet dstdoco = new DataSet();
                                    MySqlCommand commandtdoco = new MySqlCommand(Querytdoco, conexion);
                                    sqladapter.SelectCommand = commandtdoco;
                                    sqladapter.Fill(dstdoco);
                                    sqladapter.Dispose();
                                    commandtdoco.Dispose();
                                    if (dstdoco.Tables[0].Rows[0][0].ToString() == "1")
                                    {
                                        tdoco.Visible = true;
                                    }
                                    //Permisos para Datos Académicos docente
                                    string Querytdoce = "select count(*) from tuser, trole, tusme, tmede " +
                                    " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                    " and tuser_trole_clave = trole_clave " +
                                    " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 5 " +
                                    " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                                    " and tmede_forma = 'tdoce' ";
                                    DataSet dstdoce = new DataSet();
                                    MySqlCommand commandtdoce = new MySqlCommand(Querytdoce, conexion);
                                    sqladapter.SelectCommand = commandtdoce;
                                    sqladapter.Fill(dstdoce);
                                    sqladapter.Dispose();
                                    commandtdoce.Dispose();
                                    if (dstdoce.Tables[0].Rows[0][0].ToString() == "1")
                                    {
                                        tdoce.Visible = true;
                                    }
                                    //Permisos para Disponibilidad docente
                                    string Querytdido = "select count(*) from tuser, trole, tusme, tmede " +
                                    " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                    " and tuser_trole_clave = trole_clave " +
                                    " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 5 " +
                                    " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                                    " and tmede_forma = 'tdido' ";
                                    DataSet dstdido = new DataSet();
                                    MySqlCommand commandtdido = new MySqlCommand(Querytdido, conexion);
                                    sqladapter.SelectCommand = commandtdido;
                                    sqladapter.Fill(dstdido);
                                    sqladapter.Dispose();
                                    commandtdido.Dispose();
                                    if (dstdido.Tables[0].Rows[0][0].ToString() == "1")
                                    {
                                        tdido.Visible = true;
                                    }
                                }
                            }
                            catch
                            {
                                //
                            }
                            //Permisos para el catálogo de salones
                            string Querytsalo = "select count(*) from tuser, trole, tusme, tmede " +
                            " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                            " and tuser_trole_clave = trole_clave " +
                            " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 5 " +
                            " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                            " and tmede_forma = 'tsalo' ";
                            try
                            {
                                DataSet dstsalo = new DataSet();
                                MySqlCommand commandtsalo = new MySqlCommand(Querytsalo, conexion);
                                sqladapter.SelectCommand = commandtsalo;
                                sqladapter.Fill(dstsalo);
                                sqladapter.Dispose();
                                commandtsalo.Dispose();
                                if (dstsalo.Tables[0].Rows[0][0].ToString() != "0")
                                {
                                    tsalo.Visible = true;
                                }

                            }
                            catch
                            {
                                // Response.Redirect("Default.aspx");
                            }
                            //Permisos para el submenú de Grupos
                            string QueryGrupos = "select count(*) from tuser, trole, tusme, tmede " +
                                " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                " and tuser_trole_clave = trole_clave " +
                                " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 5 " +
                                " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                                " and tmede_rel = 'grupos' ";
                            try
                            {
                                DataSet dsgrupos = new DataSet();
                                MySqlCommand commandgrupos = new MySqlCommand(QueryGrupos, conexion);
                                sqladapter.SelectCommand = commandgrupos;
                                sqladapter.Fill(dsgrupos);
                                sqladapter.Dispose();
                                commandgrupos.Dispose();
                                if (dsgrupos.Tables[0].Rows[0][0].ToString() == "0")
                                {
                                    Grupos.Visible = false;
                                }
                                else
                                {
                                    //Permisos catálogo de grupos materia
                                    string Querytgrup = "select count(*) from tuser, trole, tusme, tmede " +
                                    " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                    " and tuser_trole_clave = trole_clave " +
                                    " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 5 " +
                                    " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                                    " and tmede_forma = 'tgrup' ";
                                    try
                                    {
                                        DataSet dstgrup = new DataSet();
                                        MySqlCommand commandtgrup = new MySqlCommand(Querytgrup, conexion);
                                        sqladapter.SelectCommand = commandtgrup;
                                        sqladapter.Fill(dstgrup);
                                        sqladapter.Dispose();
                                        commandtgrup.Dispose();
                                        if (dstgrup.Tables[0].Rows[0][0].ToString() != "0")
                                        {
                                            tgrup.Visible = true;
                                        }

                                    }
                                    catch
                                    {
                                        // Response.Redirect("Default.aspx");
                                    }
                                    //Permisos para asignación de horarios
                                    string Querythora = "select count(*) from tuser, trole, tusme, tmede " +
                                    " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                    " and tuser_trole_clave = trole_clave " +
                                    " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 5 " +
                                    " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                                    " and tmede_forma = 'thora' ";
                                    try
                                    {
                                        DataSet dsthora = new DataSet();
                                        MySqlCommand commandthora = new MySqlCommand(Querythora, conexion);
                                        sqladapter.SelectCommand = commandthora;
                                        sqladapter.Fill(dsthora);
                                        sqladapter.Dispose();
                                        commandthora.Dispose();
                                        if (dsthora.Tables[0].Rows[0][0].ToString() != "0")
                                        {
                                            thora.Visible = true;
                                        }

                                    }
                                    catch
                                    {
                                        // Response.Redirect("Default.aspx");
                                    }
                                    //Permisos para asignación de docente
                                    string Querythdo = "select count(*) from tuser, trole, tusme, tmede " +
                                    " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                    " and tuser_trole_clave = trole_clave " +
                                    " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 5 " +
                                    " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                                    " and tmede_forma = 'thodo' ";
                                    try
                                    {
                                        DataSet dsthodo = new DataSet();
                                        MySqlCommand commandthodo = new MySqlCommand(Querythdo, conexion);
                                        sqladapter.SelectCommand = commandthodo;
                                        sqladapter.Fill(dsthodo);
                                        sqladapter.Dispose();
                                        commandthodo.Dispose();
                                        if (dsthodo.Tables[0].Rows[0][0].ToString() != "0")
                                        {
                                            thodo.Visible = true;
                                        }

                                    }
                                    catch
                                    {
                                        // Response.Redirect("Default.aspx");
                                    }
                                    //Permisos para generación de horarios
                                    string Querytgeho = "select count(*) from tuser, trole, tusme, tmede " +
                                    " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                    " and tuser_trole_clave = trole_clave " +
                                    " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 5 " +
                                    " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                                    " and tmede_forma = 'tgeho' ";
                                    try
                                    {
                                        DataSet dstgeho = new DataSet();
                                        MySqlCommand commandtgeho = new MySqlCommand(Querytgeho, conexion);
                                        sqladapter.SelectCommand = commandtgeho;
                                        sqladapter.Fill(dstgeho);
                                        sqladapter.Dispose();
                                        commandtgeho.Dispose();
                                        if (dstgeho.Tables[0].Rows[0][0].ToString() != "0")
                                        {
                                            tgeho.Visible = true;
                                        }

                                    }
                                    catch
                                    {
                                        // Response.Redirect("Default.aspx");
                                    }
                                }
                            }
                            catch
                            {

                            }



                        }
                    }
                    catch
                    {
                        //
                    }

                    //Revisa permisos Menú Control Escolar



                    string QueryControl = "select count(*) from tuser, trole, tusme, tmede " +
                        " where tuser_clave = '" + Session["usuario"].ToString() + "' " +
                        " and   trole_clave= tuser_trole_clave " +
                        " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 4 " +
                        " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave ";
                    try
                    {

                        DataSet dscontrol = new DataSet();
                        MySqlCommand commandcontrol = new MySqlCommand(QueryControl, conexion);
                        sqladapter.SelectCommand = commandcontrol;
                        sqladapter.Fill(dscontrol);
                        sqladapter.Dispose();
                        commandcontrol.Dispose();
                        if (dscontrol.Tables[0].Rows[0][0].ToString() == "0")
                        {
                            escolares.Visible = false;
                        }
                        else
                        {
                            //Permisos para el submenú de Calendario
                            string QueryCalendario = "select count(*) from tuser, trole, tusme, tmede " +
                                " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                " and tuser_trole_clave = trole_clave " +
                                " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 4 " +
                                " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                                " and tmede_rel = 'calendario' ";
                            try
                            {
                                DataSet dscalendario = new DataSet();
                                MySqlCommand commandcalendario = new MySqlCommand(QueryCalendario, conexion);
                                sqladapter.SelectCommand = commandcalendario;
                                sqladapter.Fill(dscalendario);
                                sqladapter.Dispose();
                                commandcalendario.Dispose();
                                if (dscalendario.Tables[0].Rows[0][0].ToString() == "0")
                                {
                                    calendario.Visible = false;
                                }
                                else
                                {
                                    //Permisos catálogo conceptos calendario
                                    string Querytcoca = "select count(*) from tuser, trole, tusme, tmede " +
                                    " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                    " and tuser_trole_clave = trole_clave " +
                                    " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 4 " +
                                    " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                                    " and tmede_forma = 'tcoca' ";
                                    try
                                    {
                                        DataSet dstcoca = new DataSet();
                                        MySqlCommand commandtcoca = new MySqlCommand(Querytcoca, conexion);
                                        sqladapter.SelectCommand = commandtcoca;
                                        sqladapter.Fill(dstcoca);
                                        sqladapter.Dispose();
                                        commandtcoca.Dispose();
                                        if (dstcoca.Tables[0].Rows[0][0].ToString() != "0")
                                        {
                                            tcoca.Visible = true;
                                        }

                                    }
                                    catch
                                    {
                                        // Response.Redirect("Default.aspx");
                                    }
                                    //Permisos para Calendario Escolar
                                    string Querytcaes = "select count(*) from tuser, trole, tusme, tmede " +
                                    " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                    " and tuser_trole_clave = trole_clave " +
                                    " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 4 " +
                                    " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                                    " and tmede_forma = 'tcaes' ";
                                    try
                                    {
                                        DataSet dstcaes = new DataSet();
                                        MySqlCommand commandtcaes = new MySqlCommand(Querytcaes, conexion);
                                        sqladapter.SelectCommand = commandtcaes;
                                        sqladapter.Fill(dstcaes);
                                        sqladapter.Dispose();
                                        commandtcaes.Dispose();
                                        if (dstcaes.Tables[0].Rows[0][0].ToString() != "0")
                                        {
                                            tcaes.Visible = true;
                                        }

                                    }
                                    catch
                                    {
                                        // Response.Redirect("Default.aspx");
                                    }
                                }
                            }
                            catch
                            {

                            }

                            //Permisos para el submenú de Bajas
                            string QueryBajas = "select count(*) from tuser, trole, tusme, tmede " +
                                " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                " and tuser_trole_clave = trole_clave " +
                                " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 4 " +
                                " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                                " and tmede_rel = 'bajas' ";
                            try
                            {
                                DataSet dsbajas = new DataSet();
                                MySqlCommand commandbajas = new MySqlCommand(QueryBajas, conexion);
                                sqladapter.SelectCommand = commandbajas;
                                sqladapter.Fill(dsbajas);
                                sqladapter.Dispose();
                                commandbajas.Dispose();
                                if (dsbajas.Tables[0].Rows[0][0].ToString() == "0")
                                {
                                    bajas.Visible = false;
                                }
                                else
                                {
                                    //Permisos catálogo de tipos de baja
                                    string Queryttiba = "select count(*) from tuser, trole, tusme, tmede " +
                                    " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                    " and tuser_trole_clave = trole_clave " +
                                    " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 4 " +
                                    " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                                    " and tmede_forma = 'ttiba' ";
                                    try
                                    {
                                        DataSet dsttiba = new DataSet();
                                        MySqlCommand commandttiba = new MySqlCommand(Queryttiba, conexion);
                                        sqladapter.SelectCommand = commandttiba;
                                        sqladapter.Fill(dsttiba);
                                        sqladapter.Dispose();
                                        commandttiba.Dispose();
                                        if (dsttiba.Tables[0].Rows[0][0].ToString() != "0")
                                        {
                                            ttiba.Visible = true;
                                        }

                                    }
                                    catch
                                    {
                                        // Response.Redirect("Default.aspx");
                                    }
                                    //Permisos para Párámetros Devolución
                                    string Querytbapa = "select count(*) from tuser, trole, tusme, tmede " +
                                    " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                    " and tuser_trole_clave = trole_clave " +
                                    " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 4 " +
                                    " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                                    " and tmede_forma = 'tbapa' ";
                                    try
                                    {
                                        DataSet dstbapa = new DataSet();
                                        MySqlCommand commandtbapa = new MySqlCommand(Querytbapa, conexion);
                                        sqladapter.SelectCommand = commandtbapa;
                                        sqladapter.Fill(dstbapa);
                                        sqladapter.Dispose();
                                        commandtbapa.Dispose();
                                        if (dstbapa.Tables[0].Rows[0][0].ToString() != "0")
                                        {
                                            tbapa.Visible = true;
                                        }

                                    }
                                    catch
                                    {
                                        // Response.Redirect("Default.aspx");
                                    }
                                    //Permisos para Aplicación de Bajas
                                    string Querytbaap = "select count(*) from tuser, trole, tusme, tmede " +
                                    " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                    " and tuser_trole_clave = trole_clave " +
                                    " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 4 " +
                                    " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                                    " and tmede_forma = 'tbaap' ";
                                    try
                                    {
                                        DataSet dstbaap = new DataSet();
                                        MySqlCommand commandtbaap = new MySqlCommand(Querytbaap, conexion);
                                        sqladapter.SelectCommand = commandtbaap;
                                        sqladapter.Fill(dstbaap);
                                        sqladapter.Dispose();
                                        commandtbaap.Dispose();
                                        if (dstbaap.Tables[0].Rows[0][0].ToString() != "0")
                                        {
                                            tbaap.Visible = true;
                                        }

                                    }
                                    catch
                                    {
                                        // Response.Redirect("Default.aspx");
                                    }
                                }
                            }
                            catch
                            {

                            }

                            //Permisos para Revalidación/Equivalencias
                            string Querytpred = "select count(*) from tuser, trole, tusme, tmede " +
                            " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                            " and tuser_trole_clave = trole_clave " +
                            " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 4 " +
                            " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                            " and tmede_forma = 'tpred' ";
                            try
                            {
                                DataSet dstpred = new DataSet();
                                MySqlCommand commandtpred = new MySqlCommand(Querytpred, conexion);
                                sqladapter.SelectCommand = commandtpred;
                                sqladapter.Fill(dstpred);
                                sqladapter.Dispose();
                                commandtpred.Dispose();
                                if (dstpred.Tables[0].Rows[0][0].ToString() != "0")
                                {
                                    tpred.Visible = true;
                                }
                            }
                            catch
                            {
                                // Response.Redirect("Default.aspx");
                            }

                            //Permisos para Revalidación/Equivalencias
                            string Querytstal = "select count(*) from tuser, trole, tusme, tmede " +
                            " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                            " and tuser_trole_clave = trole_clave " +
                            " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 4 " +
                            " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                            " and tmede_forma = 'tstal' ";
                            try
                            {
                                DataSet dststal = new DataSet();
                                MySqlCommand commandtstal = new MySqlCommand(Querytstal, conexion);
                                sqladapter.SelectCommand = commandtstal;
                                sqladapter.Fill(dststal);
                                sqladapter.Dispose();
                                commandtstal.Dispose();
                                if (dststal.Tables[0].Rows[0][0].ToString() != "0")
                                {
                                    tstal.Visible = true;
                                }
                            }
                            catch
                            {
                                // Response.Redirect("Default.aspx");
                            }

                            //Permisos para Seriación Materias
                            string Querytseri = "select count(*) from tuser, trole, tusme, tmede " +
                            " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                            " and tuser_trole_clave = trole_clave " +
                            " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 4 " +
                            " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                            " and tmede_forma = 'tseri' ";
                            try
                            {
                                DataSet dstseri = new DataSet();
                                MySqlCommand commandtseri = new MySqlCommand(Querytseri, conexion);
                                sqladapter.SelectCommand = commandtseri;
                                sqladapter.Fill(dstseri);
                                sqladapter.Dispose();
                                commandtseri.Dispose();
                                if (dstseri.Tables[0].Rows[0][0].ToString() != "0")
                                {
                                    tseri.Visible = true;
                                }
                            }
                            catch
                            {
                                // Response.Redirect("Default.aspx");
                            }

                            //Permisos para el submenú de Clificaciones
                            string QueryCali = "select count(*) from tuser, trole, tusme, tmede " +
                                " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                " and tuser_trole_clave = trole_clave " +
                                " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 4 " +
                                " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                                " and tmede_rel = 'cali' ";
                            try
                            {
                                DataSet dscali = new DataSet();
                                MySqlCommand commandcali = new MySqlCommand(QueryCali, conexion);
                                sqladapter.SelectCommand = commandcali;
                                sqladapter.Fill(dscali);
                                sqladapter.Dispose();
                                commandcali.Dispose();
                                if (dscali.Tables[0].Rows[0][0].ToString() == "0")
                                {
                                    cali.Visible = false;
                                }
                                else
                                {
                                    //Permisos catálogo de calificaciones
                                    string Querytcali = "select count(*) from tuser, trole, tusme, tmede " +
                                    " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                    " and tuser_trole_clave = trole_clave " +
                                    " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 4 " +
                                    " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                                    " and tmede_forma = 'tcali' ";
                                    try
                                    {
                                        DataSet dstcali = new DataSet();
                                        MySqlCommand commandtcali = new MySqlCommand(Querytcali, conexion);
                                        sqladapter.SelectCommand = commandtcali;
                                        sqladapter.Fill(dstcali);
                                        sqladapter.Dispose();
                                        commandtcali.Dispose();
                                        if (dstcali.Tables[0].Rows[0][0].ToString() != "0")
                                        {
                                            tcali.Visible = true;
                                        }

                                    }
                                    catch
                                    {
                                        // Response.Redirect("Default.aspx");
                                    }
                                    //Permisos para Tipos de acreditación
                                    string Queryttiac = "select count(*) from tuser, trole, tusme, tmede " +
                                    " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                    " and tuser_trole_clave = trole_clave " +
                                    " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 4 " +
                                    " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                                    " and tmede_forma = 'ttiac' ";
                                    try
                                    {
                                        DataSet dsttiac = new DataSet();
                                        MySqlCommand commandttiac = new MySqlCommand(Queryttiac, conexion);
                                        sqladapter.SelectCommand = commandttiac;
                                        sqladapter.Fill(dsttiac);
                                        sqladapter.Dispose();
                                        commandttiac.Dispose();
                                        if (dsttiac.Tables[0].Rows[0][0].ToString() != "0")
                                        {
                                            ttiac.Visible = true;
                                        }

                                    }
                                    catch
                                    {
                                        // Response.Redirect("Default.aspx");
                                    }
                                    //Permisos para Componentes Calificables
                                    string Querytcomp = "select count(*) from tuser, trole, tusme, tmede " +
                                    " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                    " and tuser_trole_clave = trole_clave " +
                                    " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 4 " +
                                    " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                                    " and tmede_forma = 'tcomp' ";
                                    try
                                    {
                                        DataSet dstcomp = new DataSet();
                                        MySqlCommand commandtcomp = new MySqlCommand(Querytcomp, conexion);
                                        sqladapter.SelectCommand = commandtcomp;
                                        sqladapter.Fill(dstcomp);
                                        sqladapter.Dispose();
                                        commandtcomp.Dispose();
                                        if (dstcomp.Tables[0].Rows[0][0].ToString() != "0")
                                        {
                                            tcomp.Visible = true;
                                        }

                                    }
                                    catch
                                    {
                                        // Response.Redirect("Default.aspx");
                                    }

                                    //Permisos para configuración Componentes
                                    string Querytcocx = "select count(*) from tuser, trole, tusme, tmede " +
                                    " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                    " and tuser_trole_clave = trole_clave " +
                                    " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 4 " +
                                    " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                                    " and tmede_forma = 'tcocx' ";
                                    try
                                    {
                                        DataSet dstcocx = new DataSet();
                                        MySqlCommand commandtcocx = new MySqlCommand(Querytcocx, conexion);
                                        sqladapter.SelectCommand = commandtcocx;
                                        sqladapter.Fill(dstcocx);
                                        sqladapter.Dispose();
                                        commandtcocx.Dispose();
                                        if (dstcocx.Tables[0].Rows[0][0].ToString() != "0")
                                        {
                                            tcocx.Visible = true;
                                        }

                                    }
                                    catch
                                    {
                                        // Response.Redirect("Default.aspx");
                                    }

                                    //Permisos para calificar Componentes
                                    string Querytcacx = "select count(*) from tuser, trole, tusme, tmede " +
                                    " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                    " and tuser_trole_clave = trole_clave " +
                                    " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 4 " +
                                    " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                                    " and tmede_forma = 'tcacx' ";
                                    try
                                    {
                                        DataSet dstcacx = new DataSet();
                                        MySqlCommand commandtcacx = new MySqlCommand(Querytcacx, conexion);
                                        sqladapter.SelectCommand = commandtcacx;
                                        sqladapter.Fill(dstcacx);
                                        sqladapter.Dispose();
                                        commandtcacx.Dispose();
                                        if (dstcacx.Tables[0].Rows[0][0].ToString() != "0")
                                        {
                                            tcacx.Visible = true;
                                        }

                                    }
                                    catch
                                    {
                                        // Response.Redirect("Default.aspx");
                                    }

                                    //Permisos para Registro Inasistencias
                                    string Querytinss = "select count(*) from tuser, trole, tusme, tmede " +
                                    " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                    " and tuser_trole_clave = trole_clave " +
                                    " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 4 " +
                                    " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                                    " and tmede_forma = 'tinss' ";
                                    try
                                    {
                                        DataSet dstinss = new DataSet();
                                        MySqlCommand commandtinss = new MySqlCommand(Querytinss, conexion);
                                        sqladapter.SelectCommand = commandtinss;
                                        sqladapter.Fill(dstinss);
                                        sqladapter.Dispose();
                                        commandtinss.Dispose();
                                        if (dstinss.Tables[0].Rows[0][0].ToString() != "0")
                                        {
                                            tinss.Visible = true;
                                        }

                                    }
                                    catch
                                    {
                                        // Response.Redirect("Default.aspx");
                                    }
                                    //Permisos para Concentración calificaciones
                                    string Querytconc = "select count(*) from tuser, trole, tusme, tmede " +
                                    " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                    " and tuser_trole_clave = trole_clave " +
                                    " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 4 " +
                                    " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                                    " and tmede_forma = 'tconc' ";
                                    try
                                    {
                                        DataSet dstconc = new DataSet();
                                        MySqlCommand commandtconc = new MySqlCommand(Querytconc, conexion);
                                        sqladapter.SelectCommand = commandtconc;
                                        sqladapter.Fill(dstconc);
                                        sqladapter.Dispose();
                                        commandtconc.Dispose();
                                        if (dstconc.Tables[0].Rows[0][0].ToString() != "0")
                                        {
                                            tconc.Visible = true;
                                        }

                                    }
                                    catch
                                    {
                                        // Response.Redirect("Default.aspx");
                                    }
                                }
                            }
                            catch
                            {

                            }

                            //Permisos para Inscripción-Materias
                            string Queryttira = "select count(*) from tuser, trole, tusme, tmede " +
                            " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                            " and tuser_trole_clave = trole_clave " +
                            " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 4 " +
                            " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                            " and tmede_forma = 'ttira' ";
                            try
                            {
                                DataSet dsttira = new DataSet();
                                MySqlCommand commandttira = new MySqlCommand(Queryttira, conexion);
                                sqladapter.SelectCommand = commandttira;
                                sqladapter.Fill(dsttira);
                                sqladapter.Dispose();
                                commandttira.Dispose();
                                if (dsttira.Tables[0].Rows[0][0].ToString() != "0")
                                {
                                    ttira.Visible = true;
                                }
                            }
                            catch
                            {
                                // Response.Redirect("Default.aspx");
                            }

                            //Permisos para Kardex (Historial Académico)
                            string Querytkard = "select count(*) from tuser, trole, tusme, tmede " +
                            " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                            " and tuser_trole_clave = trole_clave " +
                            " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 4 " +
                            " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                            " and tmede_forma = 'tkard' ";
                            try
                            {
                                DataSet dstkard = new DataSet();
                                MySqlCommand commandtkard = new MySqlCommand(Querytkard, conexion);
                                sqladapter.SelectCommand = commandtkard;
                                sqladapter.Fill(dstkard);
                                sqladapter.Dispose();
                                commandtkard.Dispose();
                                if (dstkard.Tables[0].Rows[0][0].ToString() != "0")
                                {
                                    tkard.Visible = true;
                                }
                            }
                            catch
                            {
                                // Response.Redirect("Default.aspx");
                            }

                            //Permisos para Kardex (Historial Académico)
                            string Querytbole = "select count(*) from tuser, trole, tusme, tmede " +
                            " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                            " and tuser_trole_clave = trole_clave " +
                            " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 4 " +
                            " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                            " and tmede_forma = 'tbole' ";
                            try
                            {
                                DataSet dstbole = new DataSet();
                                MySqlCommand commandtbole = new MySqlCommand(Querytbole, conexion);
                                sqladapter.SelectCommand = commandtbole;
                                sqladapter.Fill(dstbole);
                                sqladapter.Dispose();
                                commandtbole.Dispose();
                                if (dstbole.Tables[0].Rows[0][0].ToString() != "0")
                                {
                                    tbole.Visible = true;
                                }
                            }
                            catch
                            {
                                // Response.Redirect("Default.aspx");
                            }


                            //Permisos para Opciones de Titulación
                            string Queryttiop = "select count(*) from tuser, trole, tusme, tmede " +
                            " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                            " and tuser_trole_clave = trole_clave " +
                            " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 4 " +
                            " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                            " and tmede_forma = 'ttiop' ";
                            try
                            {
                                DataSet dsttiop = new DataSet();
                                MySqlCommand commandttiop = new MySqlCommand(Queryttiop, conexion);
                                sqladapter.SelectCommand = commandttiop;
                                sqladapter.Fill(dsttiop);
                                sqladapter.Dispose();
                                commandttiop.Dispose();
                                if (dsttiop.Tables[0].Rows[0][0].ToString() != "0")
                                {
                                    ttiop.Visible = true;
                                }
                            }
                            catch
                            {
                                // Response.Redirect("Default.aspx");
                            }


                            //Permisos para Reporte Registro de Servicio Social	
                            string Querytcrss = "select count(*) from tuser, trole, tusme, tmede " +
                            " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                            " and tuser_trole_clave = trole_clave " +
                            " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 4 " +
                            " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                            " and tmede_forma = 'tcrss' ";
                            try
                            {
                                DataSet dstcrss = new DataSet();
                                MySqlCommand commandtcrss = new MySqlCommand(Querytcrss, conexion);
                                sqladapter.SelectCommand = commandtcrss;
                                sqladapter.Fill(dstcrss);
                                sqladapter.Dispose();
                                commandtcrss.Dispose();
                                if (dstcrss.Tables[0].Rows[0][0].ToString() != "0")
                                {
                                    tcrss.Visible = true;
                                }
                            }
                            catch
                            {
                                // Response.Redirect("Default.aspx");
                            }


                            //Permisos para Reporte Registro de Servicio Social	
                            string Querytress = "select count(*) from tuser, trole, tusme, tmede " +
                            " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                            " and tuser_trole_clave = trole_clave " +
                            " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 4 " +
                            " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                            " and tmede_forma = 'tress' ";
                            try
                            {
                                DataSet dsttress = new DataSet();
                                MySqlCommand commandtress = new MySqlCommand(Querytress, conexion);
                                sqladapter.SelectCommand = commandtress;
                                sqladapter.Fill(dsttress);
                                sqladapter.Dispose();
                                commandtress.Dispose();
                                if (dsttress.Tables[0].Rows[0][0].ToString() != "0")
                                {
                                    tress.Visible = true;
                                }
                            }
                            catch
                            {
                                // Response.Redirect("Default.aspx");
                            }


                            string Querytrtit = "select count(*) from tuser, trole, tusme, tmede " +
                         " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                         " and tuser_trole_clave = trole_clave " +
                         " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 4 " +
                         " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                         " and tmede_forma = 'trtit' ";
                            try
                            {
                                DataSet dstrtit = new DataSet();
                                MySqlCommand commandtrtit = new MySqlCommand(Querytrtit, conexion);
                                sqladapter.SelectCommand = commandtrtit;
                                sqladapter.Fill(dstrtit);
                                sqladapter.Dispose();
                                commandtrtit.Dispose();
                                if (dstrtit.Tables[0].Rows[0][0].ToString() != "0")
                                {
                                    trtit.Visible = true;
                                }
                            }
                            catch
                            {
                                // Response.Redirect("Default.aspx");
                            }


                            //Permisos para el submenú de procesos Electrónicos
                            string Queryelectronicos = "select count(*) from tuser, trole, tusme, tmede " +
                                " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                " and tuser_trole_clave = trole_clave " +
                                " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 4 " +
                                " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                                " and tmede_rel = 'electronicos' ";
                            try
                            {
                                DataSet dselectronicos = new DataSet();
                                MySqlCommand commandelectronicos = new MySqlCommand(Queryelectronicos, conexion);
                                sqladapter.SelectCommand = commandelectronicos;
                                sqladapter.Fill(dselectronicos);
                                sqladapter.Dispose();
                                commandelectronicos.Dispose();
                                if (dselectronicos.Tables[0].Rows[0][0].ToString() == "0")
                                {
                                    electronica.Visible = false;
                                }
                                else
                                {
                                    //Permisos certificación electrónica
                                    string Querytceel = "select count(*) from tuser, trole, tusme, tmede " +
                                    " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                    " and tuser_trole_clave = trole_clave " +
                                    " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 4 " +
                                    " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                                    " and tmede_forma = 'tceel' ";
                                    try
                                    {
                                        DataSet dstceel = new DataSet();
                                        MySqlCommand commandtceel = new MySqlCommand(Querytceel, conexion);
                                        sqladapter.SelectCommand = commandtceel;
                                        sqladapter.Fill(dstceel);
                                        sqladapter.Dispose();
                                        commandtceel.Dispose();
                                        if (dstceel.Tables[0].Rows[0][0].ToString() != "0")
                                        {
                                            tceel.Visible = true;
                                        }

                                    }
                                    catch
                                    {
                                        // Response.Redirect("Default.aspx");
                                    }
                                    //Permisos para titulación electrónica
                                    string Queryttiel = "select count(*) from tuser, trole, tusme, tmede " +
                                    " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                    " and tuser_trole_clave = trole_clave " +
                                    " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 4 " +
                                    " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                                    " and tmede_forma = 'ttiel' ";
                                    try
                                    {
                                        DataSet dsttiel = new DataSet();
                                        MySqlCommand commandttiel = new MySqlCommand(Queryttiel, conexion);
                                        sqladapter.SelectCommand = commandttiel;
                                        sqladapter.Fill(dsttiel);
                                        sqladapter.Dispose();
                                        commandttiel.Dispose();
                                        if (dsttiel.Tables[0].Rows[0][0].ToString() != "0")
                                        {
                                            ttiel.Visible = true;
                                        }

                                    }
                                    catch
                                    {
                                        // Response.Redirect("Default.aspx");
                                    }

                                }
                            }
                            catch
                            {

                            }

                        }
                    }
                    catch
                    {
                        //
                    }

                    //Revisa permisos Menú Finanzas
                    string QueryFinanzas = "select count(*) from tuser, trole, tusme, tmede " +
                        " where tuser_clave = '" + Session["usuario"].ToString() + "' " +
                        " and   trole_clave= tuser_trole_clave " +
                        " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 6 " +
                        " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave ";
                    try
                    {

                        DataSet dsfinanzas = new DataSet();
                        MySqlCommand commandfinanzas = new MySqlCommand(QueryFinanzas, conexion);
                        sqladapter.SelectCommand = commandfinanzas;
                        sqladapter.Fill(dsfinanzas);
                        sqladapter.Dispose();
                        commandfinanzas.Dispose();
                        if (dsfinanzas.Tables[0].Rows[0][0].ToString() == "0")
                        {
                            Finanzas.Visible = false;
                        }
                        else
                        {
                            //Permisos para el catálogo de Conceptos cobranza
                            string Querytcoco = "select count(*) from tuser, trole, tusme, tmede " +
                            " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                            " and tuser_trole_clave = trole_clave " +
                            " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 6 " +
                            " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                            " and tmede_forma = 'tcoco' ";
                            try
                            {
                                DataSet dstcoco = new DataSet();
                                MySqlCommand commandtcoco = new MySqlCommand(Querytcoco, conexion);
                                sqladapter.SelectCommand = commandtcoco;
                                sqladapter.Fill(dstcoco);
                                sqladapter.Dispose();
                                commandtcoco.Dispose();
                                if (dstcoco.Tables[0].Rows[0][0].ToString() != "0")
                                {
                                    tcoco.Visible = true;
                                }

                            }
                            catch
                            {
                                // Response.Redirect("Default.aspx");
                            }
                            //Permisos para el catálogo de Cuotas
                            string Querytcuot = "select count(*) from tuser, trole, tusme, tmede " +
                            " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                            " and tuser_trole_clave = trole_clave " +
                            " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 6 " +
                            " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                            " and tmede_forma = 'tcuot' ";
                            try
                            {
                                DataSet dstcuot = new DataSet();
                                MySqlCommand commandtcuot = new MySqlCommand(Querytcuot, conexion);
                                sqladapter.SelectCommand = commandtcuot;
                                sqladapter.Fill(dstcuot);
                                sqladapter.Dispose();
                                commandtcuot.Dispose();
                                if (dstcuot.Tables[0].Rows[0][0].ToString() != "0")
                                {
                                    tcuot.Visible = true;
                                }

                            }
                            catch
                            {
                                // Response.Redirect("Default.aspx");
                            }

                            //Permisos para el submenú de Configuración Tasa
                            string QueryTasa = "select count(*) from tuser, trole, tusme, tmede " +
                                " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                " and tuser_trole_clave = trole_clave " +
                                " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 6 " +
                                " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                                " and tmede_rel = 'tasa' ";
                            try
                            {
                                DataSet dstasa = new DataSet();
                                MySqlCommand commandtasa = new MySqlCommand(QueryTasa, conexion);
                                sqladapter.SelectCommand = commandtasa;
                                sqladapter.Fill(dstasa);
                                sqladapter.Dispose();
                                commandtasa.Dispose();
                                if (dstasa.Tables[0].Rows[0][0].ToString() == "0")
                                {
                                    tasa.Visible = false;
                                }
                                else
                                {
                                    //Permisos para Catálogo de Tasas
                                    string Queryttasa = "select count(*) from tuser, trole, tusme, tmede " +
                                    " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                    " and tuser_trole_clave = trole_clave " +
                                    " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 6 " +
                                    " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                                    " and tmede_forma = 'ttasa' ";
                                    DataSet dsttasa = new DataSet();
                                    MySqlCommand commandttasa = new MySqlCommand(Queryttasa, conexion);
                                    sqladapter.SelectCommand = commandttasa;
                                    sqladapter.Fill(dsttasa);
                                    sqladapter.Dispose();
                                    commandttasa.Dispose();
                                    if (dsttasa.Tables[0].Rows[0][0].ToString() == "1")
                                    {
                                        ttasa.Visible = true;
                                    }
                                    //Permisos para Tasa - conceptos
                                    string Querytcomb = "select count(*) from tuser, trole, tusme, tmede " +
                                    " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                    " and tuser_trole_clave = trole_clave " +
                                    " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 6 " +
                                    " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                                    " and tmede_forma = 'tcomb' ";
                                    DataSet dstcomb = new DataSet();
                                    MySqlCommand commandtcomb = new MySqlCommand(Querytcomb, conexion);
                                    sqladapter.SelectCommand = commandtcomb;
                                    sqladapter.Fill(dstcomb);
                                    sqladapter.Dispose();
                                    commandtcomb.Dispose();
                                    if (dstcomb.Tables[0].Rows[0][0].ToString() == "1")
                                    {
                                        tcomb.Visible = true;
                                    }






                                    //Permisos para Tasa - No.Materias
                                    string Queryttama = "select count(*) from tuser, trole, tusme, tmede " +
                                    " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                    " and tuser_trole_clave = trole_clave " +
                                    " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 6 " +
                                    " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                                    " and tmede_forma = 'ttama' ";
                                    DataSet dsttama = new DataSet();
                                    MySqlCommand commandttama = new MySqlCommand(Queryttama, conexion);
                                    sqladapter.SelectCommand = commandttama;
                                    sqladapter.Fill(dsttama);
                                    sqladapter.Dispose();
                                    commandttama.Dispose();
                                    if (dsttama.Tables[0].Rows[0][0].ToString() == "1")
                                    {
                                        ttama.Visible = true;
                                    }
                                }
                            }
                            catch
                            {
                                //
                            }



                            //Permisos para el submenú de Plan de Cobro Beca
                            string QueryPlanBeca = "select count(*) from tuser, trole, tusme, tmede " +
                                " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                " and tuser_trole_clave = trole_clave " +
                                " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 6 " +
                                " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                                " and tmede_rel = 'plan_beca' ";
                            try
                            {
                                DataSet dsplanbeca = new DataSet();
                                MySqlCommand commandplanbeca = new MySqlCommand(QueryPlanBeca, conexion);
                                sqladapter.SelectCommand = commandplanbeca;
                                sqladapter.Fill(dsplanbeca);
                                sqladapter.Dispose();
                                commandplanbeca.Dispose();
                                if (dsplanbeca.Tables[0].Rows[0][0].ToString() == "0")
                                {
                                    plan_beca.Visible = false;
                                }
                                else
                                {
                                    //Permisos para Catálogo de Tasas
                                    string Queryttasa = "select count(*) from tuser, trole, tusme, tmede " +
                                    " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                    " and tuser_trole_clave = trole_clave " +
                                    " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 6 " +
                                    " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                                    " and tmede_forma = 'tpcbe' ";
                                    DataSet dsttasa = new DataSet();
                                    MySqlCommand commandttasa = new MySqlCommand(Queryttasa, conexion);
                                    sqladapter.SelectCommand = commandttasa;
                                    sqladapter.Fill(dsttasa);
                                    sqladapter.Dispose();
                                    commandttasa.Dispose();
                                    if (dsttasa.Tables[0].Rows[0][0].ToString() == "1")
                                    {
                                        tpcbe.Visible = true;
                                    }
                                    
                                    //Permisos para Tasa - conceptos
                                    string Queryttepcb = "select count(*) from tuser, trole, tusme, tmede " +
                                    " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                    " and tuser_trole_clave = trole_clave " +
                                    " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 6 " +
                                    " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                                    " and tmede_forma = 'tepcb' ";
                                    DataSet dsttepcb = new DataSet();
                                    MySqlCommand commandtcomb = new MySqlCommand(Queryttepcb, conexion);
                                    sqladapter.SelectCommand = commandtcomb;
                                    sqladapter.Fill(dsttepcb);
                                    sqladapter.Dispose();
                                    commandtcomb.Dispose();
                                    if (dsttepcb.Tables[0].Rows[0][0].ToString() == "1")
                                    {
                                        tepcb.Visible = true;
                                    }


                                    //Permisos para Tasa - conceptos
                                    string Queryttrepc = "select count(*) from tuser, trole, tusme, tmede " +
                                    " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                    " and tuser_trole_clave = trole_clave " +
                                    " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 6 " +
                                    " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                                    " and tmede_forma = 'trepc' ";
                                    DataSet dsttrepc = new DataSet();
                                    MySqlCommand commandtrepc = new MySqlCommand(Queryttrepc, conexion);
                                    sqladapter.SelectCommand = commandtcomb;
                                    sqladapter.Fill(dsttrepc);
                                    sqladapter.Dispose();
                                    commandtcomb.Dispose();
                                    if (dsttrepc.Tables[0].Rows[0][0].ToString() == "1")
                                    {
                                        trepc.Visible = true;
                                    }
                                }
                            }
                            catch
                            {
                                //
                            }
                        }
                    }
                    catch
                    {
                        //
                    }

                    //Revisa permisos Menú de Tableross
                    string Querytableros = "select count(*) from tuser, trole, tusme, tmede " +
                        " where tuser_clave = '" + Session["usuario"].ToString() + "' " +
                        " and   trole_clave= tuser_trole_clave " +
                        " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 9 " +
                        " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave ";
                    try
                    {

                        DataSet dstableros = new DataSet();
                        MySqlCommand commandtableros = new MySqlCommand(Querytableros, conexion);
                        sqladapter.SelectCommand = commandtableros;
                        sqladapter.Fill(dstableros);
                        sqladapter.Dispose();
                        commandtableros.Dispose();
                        if (dstableros.Tables[0].Rows[0][0].ToString() == "0")
                        {
                            Tableros.Visible = false;
                        }
                        else
                        {
                            //Permisos para el tablero NI
                            string Querytprni = "select count(*) from tuser, trole, tusme, tmede " +
                            " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                            " and tuser_trole_clave = trole_clave " +
                            " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 9 " +
                            " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                            " and tmede_forma = 'tprni' ";
                            try
                            {
                                DataSet dstprni = new DataSet();
                                MySqlCommand commandtprni = new MySqlCommand(Querytprni, conexion);
                                sqladapter.SelectCommand = commandtprni;
                                sqladapter.Fill(dstprni);
                                sqladapter.Dispose();
                                commandtprni.Dispose();
                                if (dstprni.Tables[0].Rows[0][0].ToString() != "0")
                                {
                                    tprni.Visible = true;
                                }

                            }
                            catch
                            {
                                // Response.Redirect("Default.aspx");
                            }
                            //Permisos para el tablero RI
                            string Querytprri = "select count(*) from tuser, trole, tusme, tmede " +
                            " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                            " and tuser_trole_clave = trole_clave " +
                            " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 9 " +
                            " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                            " and tmede_forma = 'tprri' ";
                            try
                            {
                                DataSet dstprri = new DataSet();
                                MySqlCommand commandtprri = new MySqlCommand(Querytprri, conexion);
                                sqladapter.SelectCommand = commandtprri;
                                sqladapter.Fill(dstprri);
                                sqladapter.Dispose();
                                commandtprri.Dispose();
                                if (dstprri.Tables[0].Rows[0][0].ToString() != "0")
                                {
                                    tprri.Visible = true;
                                }

                            }
                            catch
                            {
                                // Response.Redirect("Default.aspx");
                            }

                            //Permisos para el Tablero de Becas/Descuentos
                            string Queryttade = "select count(*) from tuser, trole, tusme, tmede " +
                                " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                " and tuser_trole_clave = trole_clave " +
                                " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 9 " +
                                " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                                " and tmede_forma = 'ttade' ";
                            try
                            {
                                DataSet dsttade = new DataSet();
                                MySqlCommand commandttade = new MySqlCommand(Queryttade, conexion);
                                sqladapter.SelectCommand = commandttade;
                                sqladapter.Fill(dsttade);
                                sqladapter.Dispose();
                                commandttade.Dispose();
                                if (dsttade.Tables[0].Rows[0][0].ToString() == "0")
                                {
                                    ttade.Visible = false;
                                }
                                
                            }
                            catch
                            {
                                //
                            }
                            //Permisos para el Tablero de Cartera vencida
                            string Queryttave = "select count(*) from tuser, trole, tusme, tmede " +
                                " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                " and tuser_trole_clave = trole_clave " +
                                " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 9 " +
                                " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                                " and tmede_forma = 'ttave' ";
                            try
                            {
                                DataSet dsttave = new DataSet();
                                MySqlCommand commandttave = new MySqlCommand(Queryttave, conexion);
                                sqladapter.SelectCommand = commandttave;
                                sqladapter.Fill(dsttave);
                                sqladapter.Dispose();
                                commandttave.Dispose();
                                if (dsttave.Tables[0].Rows[0][0].ToString() == "0")
                                {
                                    ttave.Visible = false;
                                }

                            }
                            catch
                            {
                                //
                            }
                            //Permisos para el Tablero de Cartera vencida
                            string Queryttacxc = "select count(*) from tuser, trole, tusme, tmede " +
                                " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                " and tuser_trole_clave = trole_clave " +
                                " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 9 " +
                                " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                                " and tmede_forma = 'tacxc' ";
                            try
                            {
                                DataSet dstacxc = new DataSet();
                                MySqlCommand commandttacxc = new MySqlCommand(Queryttacxc, conexion);
                                sqladapter.SelectCommand = commandttacxc;
                                sqladapter.Fill(dstacxc);
                                sqladapter.Dispose();
                                commandttacxc.Dispose();
                                if (dstacxc.Tables[0].Rows[0][0].ToString() == "0")
                                {
                                    tacxc.Visible = false;
                                }

                            }
                            catch
                            {
                                //
                            }
                        }
                    }
                    catch
                    {
                        //
                    }

                    //Revisa permisos Menú de Seguridad
                    string Queryseguridad = "select count(*) from tuser, trole, tusme, tmede " +
                        " where tuser_clave = '" + Session["usuario"].ToString() + "' " +
                        " and   trole_clave= tuser_trole_clave " +
                        " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 7 " +
                        " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave ";
                    try
                    {

                        DataSet dsseguridad = new DataSet();
                        MySqlCommand commandseguridad = new MySqlCommand(Queryseguridad, conexion);
                        sqladapter.SelectCommand = commandseguridad;
                        sqladapter.Fill(dsseguridad);
                        sqladapter.Dispose();
                        commandseguridad.Dispose();
                        if (dsseguridad.Tables[0].Rows[0][0].ToString() == "0")
                        {
                            Seguridad.Visible = false;
                        }
                        else
                        {
                            //Permisos para el catálogo de Conceptos cobranza
                            string Querytcoco = "select count(*) from tuser, trole, tusme, tmede " +
                            " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                            " and tuser_trole_clave = trole_clave " +
                            " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 6 " +
                            " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                            " and tmede_forma = 'tcoco' ";
                            try
                            {
                                DataSet dstcoco = new DataSet();
                                MySqlCommand commandtcoco = new MySqlCommand(Querytcoco, conexion);
                                sqladapter.SelectCommand = commandtcoco;
                                sqladapter.Fill(dstcoco);
                                sqladapter.Dispose();
                                commandtcoco.Dispose();
                                if (dstcoco.Tables[0].Rows[0][0].ToString() != "0")
                                {
                                    tcoco.Visible = true;
                                }

                            }
                            catch
                            {
                                // Response.Redirect("Default.aspx");
                            }
                            //Permisos para el catálogo de Cuotas
                            string Querytcuot = "select count(*) from tuser, trole, tusme, tmede " +
                            " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                            " and tuser_trole_clave = trole_clave " +
                            " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 6 " +
                            " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                            " and tmede_forma = 'tcuot' ";
                            try
                            {
                                DataSet dstcuot = new DataSet();
                                MySqlCommand commandtcuot = new MySqlCommand(Querytcuot, conexion);
                                sqladapter.SelectCommand = commandtcuot;
                                sqladapter.Fill(dstcuot);
                                sqladapter.Dispose();
                                commandtcuot.Dispose();
                                if (dstcuot.Tables[0].Rows[0][0].ToString() != "0")
                                {
                                    tcuot.Visible = true;
                                }

                            }
                            catch
                            {
                                // Response.Redirect("Default.aspx");
                            }

                            //Permisos para el submenú de Configuración Tasa
                            string QueryTasa = "select count(*) from tuser, trole, tusme, tmede " +
                                " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                " and tuser_trole_clave = trole_clave " +
                                " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 6 " +
                                " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                                " and tmede_rel = 'tasa' ";
                            try
                            {
                                DataSet dstasa = new DataSet();
                                MySqlCommand commandtasa = new MySqlCommand(QueryTasa, conexion);
                                sqladapter.SelectCommand = commandtasa;
                                sqladapter.Fill(dstasa);
                                sqladapter.Dispose();
                                commandtasa.Dispose();
                                if (dstasa.Tables[0].Rows[0][0].ToString() == "0")
                                {
                                    tasa.Visible = false;
                                }
                                else
                                {
                                    //Permisos para Catálogo de Tasas
                                    string Queryttasa = "select count(*) from tuser, trole, tusme, tmede " +
                                    " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                    " and tuser_trole_clave = trole_clave " +
                                    " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 6 " +
                                    " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                                    " and tmede_forma = 'ttasa' ";
                                    DataSet dsttasa = new DataSet();
                                    MySqlCommand commandttasa = new MySqlCommand(Queryttasa, conexion);
                                    sqladapter.SelectCommand = commandttasa;
                                    sqladapter.Fill(dsttasa);
                                    sqladapter.Dispose();
                                    commandttasa.Dispose();
                                    if (dsttasa.Tables[0].Rows[0][0].ToString() == "1")
                                    {
                                        ttasa.Visible = true;
                                    }
                                    //Permisos para Tasa - conceptos
                                    string Querytcomb = "select count(*) from tuser, trole, tusme, tmede " +
                                    " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                    " and tuser_trole_clave = trole_clave " +
                                    " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 6 " +
                                    " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                                    " and tmede_forma = 'tcomb' ";
                                    DataSet dstcomb = new DataSet();
                                    MySqlCommand commandtcomb = new MySqlCommand(Querytcomb, conexion);
                                    sqladapter.SelectCommand = commandtcomb;
                                    sqladapter.Fill(dstcomb);
                                    sqladapter.Dispose();
                                    commandtcomb.Dispose();
                                    if (dstcomb.Tables[0].Rows[0][0].ToString() == "1")
                                    {
                                        tcomb.Visible = true;
                                    }
                                    //Permisos para Tasa - No.Materias
                                    string Queryttama = "select count(*) from tuser, trole, tusme, tmede " +
                                    " where tuser_clave='" + Session["usuario"].ToString() + "'" +
                                    " and tuser_trole_clave = trole_clave " +
                                    " and tusme_trole_clave = trole_clave and tusme_tmenu_clave = 6 " +
                                    " and tmede_tmenu_clave = tusme_tmenu_clave and tmede_clave = tusme_tmede_clave " +
                                    " and tmede_forma = 'ttama' ";
                                    DataSet dsttama = new DataSet();
                                    MySqlCommand commandttama = new MySqlCommand(Queryttama, conexion);
                                    sqladapter.SelectCommand = commandttama;
                                    sqladapter.Fill(dsttama);
                                    sqladapter.Dispose();
                                    commandttama.Dispose();
                                    if (dsttama.Tables[0].Rows[0][0].ToString() == "1")
                                    {
                                        ttama.Visible = true;
                                    }
                                }
                            }
                            catch
                            {
                                //
                            }
                        }
                    }
                    catch
                    {
                        //
                    }


                }
                catch
                {
                    // Response.Redirect("Default.aspx");
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
                    Response.Redirect("Default.aspx");
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
                Response.Redirect("Default.aspx");
            }
        }
    }
}