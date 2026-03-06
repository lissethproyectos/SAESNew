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
    public class Global
    {
        public static string cuenta="";
        public static string ap_paterno = "";
        public static string ap_materno = "";
        public static string nombre = "";
        public static string docente;
        public static double interno;
        public static double consecutivo_admi;
        public static string usuario = "";
        public static string ruta_origen = "";


        public static string periodo = "";
        public static string campus = "";
        public static string nivel = "";
        public static string colegio = "";
        public static string modalidad = "";
        public static string tipo_ing = "";

        public static string nombre_periodo = "";
        public static string nombre_campus = "";
        public static string nombre_nivel = "";
        public static string turno = "";
        public static string materia = "";
        public static string nombre_materia = "";
        public static string grupo = "";

        public static string salon = "";
        public static string nombre_salon = "";

        public static string capacidad = "";
        public static string inscritos = "";
        public static string disponibles = "";
        public static string estatus_hora = "";

        public static string admi_descuento;
        public static string descuento;
        public static string nombre_descuento;
        public static string procedencia;
        public static string promedio;

        public static string tipo_ingreso;
        public static string tasa;

        public static string nombre_alumno = "";
        public static double consecutivo = 0;
        public static double cons_pasa = 0;
        public static string tipo_contacto = "";
        public static string nombre_contacto = "";

        public static string nombre_programa;
        public static string programa;
        public static string nombre_tipoingreso;
        public static string nombre_tasa;

        public static string tipo;
        public static string estatus;

        public static string clave_docente;
        public static string nombre_docente;
        public static string ap_paterno_docente;
        public static string ap_materno_docente;
        public static string password;
        public static string respuesta;

        public static string decision;
        public static string factura;

        public static string escuela;
        public static string nombre_escuela;

        public static string carrera_docente;
        public static string idioma_docente;

        public static string hinicio_docente = "";
        public static string hfin_docente = "";

        public static double opcion = 0;
        public static DateTime fecha_fin;
        public static double cons_pago = 0;
        public static decimal importe_pago = 0;

        public static double no_config = 0;


        internal static void inserta_log(string error, string forma, string usuario)
        {
            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
            ConexionMySql.Open();
           
            string QueryLogs = "INSERT INTO TLOGS VALUES ('" + forma + "','" + error + "',CURRENT_TIMESTAMP(),'" + usuario + "', NULL)";
            MySqlCommand mysqlcmd1 = new MySqlCommand(QueryLogs, ConexionMySql);
            mysqlcmd1.CommandType = CommandType.Text;
            mysqlcmd1.ExecuteNonQuery();
        }

        internal static void inserta_log(string error, string forma, string usuario, string stackTrace)
        {
            try
            {
                MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
                ConexionMySql.Open();

                forma = forma.Replace("_aspx", "");
                string QueryLogs = "INSERT INTO TLOGS VALUES ('" + forma + "','" + error + "',CURRENT_TIMESTAMP(),'" + usuario + "','" + stackTrace.Trim().Replace(" ","") + "')";
                MySqlCommand mysqlcmd1 = new MySqlCommand(QueryLogs, ConexionMySql);
                mysqlcmd1.CommandType = CommandType.Text;
                mysqlcmd1.ExecuteNonQuery();
            }
            catch (Exception e)
            {
            }
            
        }
    }
}