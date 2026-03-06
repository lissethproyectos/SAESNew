using ApisSysweb.Interface;
using ApisSysweb.Model;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApisSysweb.Service
{
    public class CoinService:ICoinService
    {
        private readonly string _connectionString;
        public CoinService(IConfiguration _configuratio)
        {
            _connectionString = _configuratio.GetConnectionString("CnnFElectr");
        }
        public List<Grafica> ListPagosBanco(ref string Verificador)
        {
            OracleCommand cmd = null;
            ExeProcedimiento exeProc = new ExeProcedimiento(_connectionString);
            List<Grafica> lstDatos = new List<Grafica>();


            try
            {
                Verificador = "0";
                OracleDataReader dr = null;
                cmd = exeProc.GenerarOracleCommandCursor("PKG_FELECTRONICA_2016.Obt_List_Pagos_Banco", ref dr);
                while (dr.Read())
                {
                    Grafica objGrafica = new Grafica();
                    objGrafica.Dato1 = Convert.ToString(dr[0]);
                    objGrafica.Dato2 = Convert.ToString(dr[1]);
                    objGrafica.Dato3 = Convert.ToString(dr[2]);
                    lstDatos.Add(objGrafica);
                }
            }
            catch(Exception ex)
            {
                Verificador = ex.Message;
                    
            }
            finally
            {
                exeProc.LimpiarOracleCommand(ref cmd);
            }
            return lstDatos;
        }
        public List<Grafica> ListPagosporEjercicio(ref string Verificador)
        {
            OracleCommand cmd = null;
            ExeProcedimiento exeProc = new ExeProcedimiento(_connectionString);
            List<Grafica> lstDatos = new List<Grafica>();

            try
            {
                Verificador = "0";
                OracleDataReader dr = null;
                cmd = exeProc.GenerarOracleCommandCursor("PKG_FELECTRONICA_2016.Obt_List_Pagos_por_Anio", ref dr);
                while (dr.Read())
                {
                    Grafica objGrafica = new Grafica();
                    objGrafica.Dato1 = Convert.ToString(dr[0]);
                    objGrafica.Dato2 = Convert.ToString(dr[1]);
                    objGrafica.Dato3 = Convert.ToString(dr[2]);
                    lstDatos.Add(objGrafica);
                }
            }
            catch (Exception ex)
            {
                Verificador = ex.Message;

            }
            finally
            {
                exeProc.LimpiarOracleCommand(ref cmd);
            }
            return lstDatos;
        }
    }
}
