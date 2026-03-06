using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;

namespace SAES_DBA
{
    //[DebuggerStepThrough]
    public class DBA
    {
        #region Variables
        private MySqlCommand Command = null;
        private string ErrorResponse = string.Empty;
        private DataSet Results = new DataSet();
        private MySqlTransaction transaction = null;

        //private string strConnection;//= "Server=pprd.cwdwfcqf0dbx.us-east-2.rds.amazonaws.com;Database=db_a8b622_saesppr;Uid=admin;Pwd=JxxDzVeDimt4d3s;Min Pool Size=3;Max Pool Size=90;Pooling=True";
        //private string strConnection = "Server=MYSQL8002.site4now.net;Database=db_a8b622_saesd;Uid=a8b622_saesd;Pwd=saesdev01;Min Pool Size=3;Max Pool Size=90;Pooling=True";
        private string strConnection = "Server=MYSQL8002.site4now.net;Database=localhost;Uid=saes;Pwd=;Min Pool Size=3;Max Pool Size=90;Pooling=True";

        //private string strConnection = "Server=   ;Database=saesdev;Uid=root;Pwd=;Charset=utf8mb4;";

        #endregion
        #region Constructores
        public DBA()
        {
            Results = new DataSet();
            strConnection = ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString;

        }
        #endregion
        #region Funciones

        #region Transacciones
        //public void BeginTransaction()
        //{
        //    if (transaction == null)
        //        transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);
        //    else
        //        throw new Exception("Ya existe una transacción");
        //}
        //public void CommitTransaction()
        //{
        //    if (transaction != null)
        //    {
        //        transaction.Commit();
        //        transaction.Dispose();
        //        transaction = null;
        //        connection.Close();
        //    }
        //    else
        //        throw new Exception("No existe una transacción Iniciada");
        //}
        //public void RollBackTransaction()
        //{
        //    if (transaction != null)
        //    {
        //        transaction.Rollback();
        //        transaction = null;
        //        transaction.Dispose();
        //        connection.Close();
        //    }
        //    else
        //        throw new Exception("No existe una transacción Iniciada");
        //}
        #endregion

        private void BeginCommand(string SPName, Array SPParams, MySqlConnection connection)
        {
            Command = new MySqlCommand(SPName, connection);
            Command.CommandTimeout = 120;
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.AddRange(SPParams);
            if (transaction != null)
                Command.Transaction = transaction;
        }
        private void BeginCommand(string Query, MySqlConnection connection)
        {
            Command = new MySqlCommand(Query, connection);
            Command.CommandTimeout = 120;
            Command.CommandType = CommandType.Text;
            if (transaction != null)
                Command.Transaction = transaction;
        }
        private void GetResults()
        {
            Results = new DataSet();
            MySqlDataAdapter mySQLDataAdapter = new MySqlDataAdapter(Command);
            mySQLDataAdapter.Fill(Results);
        }
        public DataSet ExcecSP(string SPName, List<MySqlParameter> SPParams)
        {
            using (MySqlConnection miConexion = new MySqlConnection(strConnection))
            {
                miConexion.Open();
                BeginCommand(SPName, SPParams.ToArray(), miConexion);
                GetResults();
                miConexion.Close();
            }
            return Results;
        }
        public DataSet ExcecSP(string SPName)
        {
            using (MySqlConnection miConexion = new MySqlConnection(strConnection))
            {
                miConexion.Open();
                BeginCommand(SPName, miConexion);
                GetResults();
                miConexion.Close();
            }
            return Results;
        }


        public DataSet ExcecQuery(string Query)
        {
            using (MySqlConnection miConexion = new MySqlConnection(strConnection))
            {
                miConexion.Open();
                BeginCommand(Query, miConexion);
                GetResults();
                miConexion.Close();
            }
            return Results;
        }


        #endregion
    }
}