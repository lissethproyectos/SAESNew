using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApisSysweb.Model
{
    public class ExeProcedimiento
    {
        OracleCommand cmd = default(OracleCommand);
        private OracleConnection cn;
        private OracleTransaction trans;

        public ExeProcedimiento(string cadenaConexion)
        {
            cn = new OracleConnection(cadenaConexion);
        }
        public OracleCommand GenerarOracleCommandCursor(string SP, ref OracleDataReader dr)
        {

            cmd = new OracleCommand(SP, cn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            if (trans != null) cmd.Transaction = trans;
            if (trans == null) cn.Open();
           

            cmd.Parameters.Add("p_registros", OracleDbType.RefCursor).Direction = System.Data.ParameterDirection.Output;
            dr = cmd.ExecuteReader();
            return cmd;

        }
        public OracleCommand GenerarOracleCommandCursor(string SP, ref OracleDataReader dr, string[] Parametros, object[] Valores)
        {

            cmd = new OracleCommand(SP, cn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            if (trans != null) cmd.Transaction = trans;
            if (trans == null) cn.Open();
            if (Parametros != null)
                for (int i = 0; i <= Parametros.Length - 1; i++)
                    cmd.Parameters.Add(Parametros[i], OracleDbType.Varchar2).Value = Valores[i];

            cmd.Parameters.Add("p_registros", OracleDbType.RefCursor).Direction = System.Data.ParameterDirection.Output;
            dr = cmd.ExecuteReader();
            return cmd;

        }
        #region Limpiar
        public void LimpiarOracleCommand(ref OracleCommand Cmm)
        {
            try
            {
                if (Cmm != null)
                {
                    Cmm.Connection.Close();
                    Cmm.Connection.Dispose();
                    Cmm.Dispose();
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }
            finally
            {
                if (cn.State != System.Data.ConnectionState.Closed)
                {
                    cn.Close();
                }
            }
        }
        #endregion

    }
}
