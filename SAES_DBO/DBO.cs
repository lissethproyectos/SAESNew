using SAES_DBA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using MySql.Data.MySqlClient;
using SAES_DBO.Models;
using System.ComponentModel;

namespace SAES_DBO
{
    //[DebuggerStepThrough]
    public class DBO : IDisposable
    {
        #region Variables
        private DBA dba = null;
        private string Error = string.Empty;

        #endregion
        #region Constructores
        public DBO() { }
        #endregion
        #region Functions
        public bool init()
        {
            try
            {
                if (dba == null)
                    dba = new DBA();

                return true;
            }
            catch (Exception e)
            {
                Error = e.Message;
                return false;
            }
        }
        #region Transactions
        //public void BeginTransaction()
        //=> dba.BeginTransaction();
        //public void CommitTransaction()
        //=> dba.CommitTransaction();
        //public void RollBackTransaction()
        //=> dba.RollBackTransaction();
        #endregion
        public List<T> CallSPResult<T>(string StoreProcedureName, List<MySqlParameter> StoreProcedureParams) where T : BaseModelResponse, new()
        {
            try
            {
                init();
                DataSet Cursor = new DataSet();
                Cursor = dba.ExcecSP(StoreProcedureName, StoreProcedureParams);

                return ConvertDtc<T>(Cursor.Tables);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private string GetSPNameFromObject<T, T2>() where T : BaseModelRequest, new() where T2 : BaseModelResponse, new()
        {
            try
            {
                string response = GetSPNameFromObjectReq<T>();

                if (string.IsNullOrEmpty(response))
                    response = GetSPNameFromObjectResp<T2>();

                if (string.IsNullOrEmpty(response))
                    throw new Exception("Nombre del SP no econtrado");

                return response;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private string GetQueryStringFromObject<T, T2>() where T : BaseModelRequest, new() where T2 : BaseModelResponse, new()
        {
            try
            {
                init();

                string response = GetQueryStringFromObjectReq<T>();

                if (string.IsNullOrEmpty(response))
                    response = GetQueryStringFromObjectResp<T2>();

                if (string.IsNullOrEmpty(response))
                    throw new Exception("Nombre del SP no econtrado");

                return response;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        private string GetSPNameFromObjectResp<T>() where T : BaseModelResponse, new()
        {
            try
            {
                init();

                string result = "";
                Type tipo = typeof(T);
                List<CustomAttributeData> ca = tipo.CustomAttributes.Where(x => x.AttributeType == typeof(SPName)).ToList();
                if (ca.Count > 0)
                    result = ca.FirstOrDefault().ConstructorArguments[0].Value.ToString();

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private string GetSPNameFromObjectReq<T2>() where T2 : BaseModelRequest, new()
        {
            try
            {
                init();

                string result = "";
                Type tipo = typeof(T2);
                List<CustomAttributeData> ca = tipo.CustomAttributes.Where(x => x.AttributeType == typeof(SPName)).ToList();
                if (ca.Count > 0)
                    result = ca.FirstOrDefault().ConstructorArguments[0].Value.ToString();

                return result;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        private string GetQueryStringFromObjectResp<T>() where T : BaseModelResponse, new()
        {
            try
            {
                init();

                string result = "";
                Type tipo = typeof(T);
                List<CustomAttributeData> ca = tipo.CustomAttributes.Where(x => x.AttributeType == typeof(QueryString)).ToList();
                if (ca.Count > 0)
                    result = ca.FirstOrDefault().ConstructorArguments[0].Value.ToString();

                return result;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        private string GetQueryStringFromObjectReq<T>() where T : BaseModelRequest, new()
        {
            try
            {
                init();

                string result = "";
                Type tipo = typeof(T);
                List<CustomAttributeData> ca = tipo.CustomAttributes.Where(x => x.AttributeType == typeof(QueryString)).ToList();
                if (ca.Count > 0)
                    result = ca.FirstOrDefault().ConstructorArguments[0].Value.ToString();

                return result;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public DataTable GetDataTable(string Query)
        {
            try
            {
                init();
                DataSet DS = GetDataSet(Query);
                DataTable DT = new DataTable();
                if (DS.Tables.Count > 0)
                    DT = DS.Tables[0];

                return DT;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public DataSet GetDataSet(string Query)
        {
            try
            {
                init();
                DataSet Cursor = new DataSet();
                Cursor = dba.ExcecQuery(Query);
                return Cursor;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public List<T> ExcecQuery<T>(string Query) where T : BaseModelResponse, new()
        {
            try
            {
                init();
                DataSet Cursor = new DataSet();

                Cursor = dba.ExcecQuery(Query);

                return ConvertDtc<T>(Cursor.Tables);
            }
            catch (Exception e)
            {

                throw e;
            }

        }
        public List<T> CallSPResult<T>(List<MySqlParameter> StoreProcedureParams) where T : BaseModelResponse, new()
        {
            try
            {
                init();

                DataSet Cursor = new DataSet();
                Cursor = dba.ExcecSP(GetSPNameFromObjectResp<T>(), StoreProcedureParams);

                return ConvertDtc<T>(Cursor.Tables);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public List<T> CallSPResult<T, T2>(string StoreProcedureName, T2 RequestObject) where T : BaseModelResponse, new() where T2 : BaseModelRequest, new()
        {
            try
            {
                init();

                DataSet Cursor = new DataSet();

                Cursor = dba.ExcecSP(StoreProcedureName, ObjToParams(RequestObject));

                return ConvertDtc<T>(Cursor.Tables);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        #region Genericos
        public DataTable CallSPDataTableResult<T2>(T2 RequestObject) where T2 : BaseModelRequest, new()
        {
            DataSet Cursor = new DataSet();
            init();

            string spName = GetSPNameFromObjectReq<T2>();
            List<MySqlParameter> mySqlParameters = ObjToParams(RequestObject);
            Cursor = new DBA().ExcecSP(spName, mySqlParameters);

            if (Cursor != null)
                return Cursor?.Tables[0];
            else
                return new DataTable();

        }
        public List<T> CallSPListResult<T, T2>(T2 RequestObject) where T : BaseModelResponse, new() where T2 : BaseModelRequest, new()
        {
            DataSet Cursor = new DataSet();
            init();

            string spName = GetSPNameFromObject<T2, T>();
            List<MySqlParameter> mySqlParameters = ObjToParams(RequestObject);
            Cursor = new DBA().ExcecSP(spName, mySqlParameters);

            if (Cursor != null)
                return ConvertDtc<T>(Cursor?.Tables);
            else
                return new List<T>() { };

        }
        public T CallSPResult<T, T2>(T2 RequestObject) where T : BaseModelResponse, new() where T2 : BaseModelRequest, new()
        {
            try
            {
                init();

                DataSet Cursor = new DataSet();

                Cursor = dba.ExcecSP(GetSPNameFromObject<T2, T>(), ObjToParams(RequestObject));

                return ConvertDtc<T>(Cursor.Tables)?.FirstOrDefault();
            }
            catch (Exception e)
            {

                throw e;
            }

        }
        public string CallSPForInsertUpdate<T2>(T2 RequestObject) where T2 : BaseModelRequest, new()
        {
            init();
            DataSet Cursor = new DataSet();

            Cursor = dba.ExcecSP(GetSPNameFromObjectReq<T2>(), ObjToParams(RequestObject));

            if (Cursor?.Tables.Count == 0)
                return "";            
            else            
                return Cursor?.Tables[0]?.Rows[0]?[0]?.ToString();            
        }
        public T ExcecQuery<T, T2>(T2 RequestObject) where T : BaseModelResponse, new() where T2 : BaseModelRequest, new()
        {
            try
            {
                init();

                DataSet Cursor = new DataSet();

                Cursor = GetDataSet(GetQueryStringFromObject<T2, T>());

                return ConvertDtc<T>(Cursor.Tables)?.FirstOrDefault();
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public List<TResponse> GetListData<TResponse, TRequest>(TRequest request) where TResponse : BaseModelResponse, new() where TRequest : BaseModelRequest, new()
        => CallSPListResult<TResponse, TRequest>(request);
        public TResponse GetData<TResponse, TRequest>(TRequest request) where TResponse : BaseModelResponse, new() where TRequest : BaseModelRequest, new()
        => CallSPResult<TResponse, TRequest>(request);
        public TResponse GetDataQuery<TResponse, TRequest>(TRequest request) where TResponse : BaseModelResponse, new() where TRequest : BaseModelRequest, new()
        => ExcecQuery<TResponse, TRequest>(request);
        #endregion
        private object parseValue<TRequest>(object val)
        {
            object response = null;
            if (typeof(TRequest) == typeof(List<int>))
            {
                List<int> test = (List<int>)val;
                response = String.Join(",", test.Select(i => i.ToString()).ToList());
            }
            return response;
        }
        private List<MySqlParameter> ObjToParams<T>(T _object) where T : BaseModelRequest, new()
        {
            List<MySqlParameter> result = new List<MySqlParameter>();
            Type tipo = typeof(T);
            string PropName = string.Empty;
            foreach (PropertyInfo item
                in tipo.GetProperties()
                .OrderBy(x => x.CustomAttributes.Where(y => y.AttributeType == typeof(SPParameterName)).Select(z => z.ConstructorArguments).ToList()[0][1].Value))
            {
                MySqlParameter param = null;
                PropName = item.Name;
                List<CustomAttributeData> CA = item.CustomAttributes.Where(x => x.AttributeType == typeof(SPParameterName)).ToList();
                if (CA.Count() > 0)
                    PropName = CA[0].ConstructorArguments[0].Value.ToString();
                if (item.PropertyType == typeof(int) || item.PropertyType == typeof(int?))
                {
                    param = new MySqlParameter((CA.Count() == 0) ? $"ps{PropName}" : PropName, MySqlDbType.Int32);
                    param.Direction = ParameterDirection.Input;
                    param.Value = item.GetValue(_object);
                    result.Add(param);
                }
                if (item.PropertyType == typeof(decimal) || item.PropertyType == typeof(decimal?))
                {
                    param = new MySqlParameter((CA.Count() == 0) ? $"ps{PropName}" : PropName, MySqlDbType.Decimal);
                    param.Direction = ParameterDirection.Input;
                    param.Value = item.GetValue(_object);
                    result.Add(param);
                }
                if (item.PropertyType == typeof(DateTime) || item.PropertyType == typeof(DateTime?))
                {
                    param = new MySqlParameter((CA.Count() == 0) ? $"ps{PropName}" : PropName, MySqlDbType.Date);
                    param.Direction = ParameterDirection.Input;
                    param.Value = item.GetValue(_object);
                    result.Add(param);
                }
                else if (item.PropertyType == typeof(string))
                {
                    param = new MySqlParameter((CA.Count() == 0) ? $"ps{PropName}" : PropName, MySqlDbType.VarChar);
                    param.Direction = ParameterDirection.Input;
                    param.Value = item.GetValue(_object);
                    result.Add(param);
                }
                else if (item.PropertyType == typeof(List<int>))
                {
                    param = new MySqlParameter((CA.Count() == 0) ? $"ps{PropName}" : PropName, MySqlDbType.VarChar);
                    param.Direction = ParameterDirection.Input;
                    param.Value = parseValue<List<int>>(item.GetValue(_object));
                    result.Add(param);
                }
                else if (item.PropertyType == typeof(object))
                {
                    param = new MySqlParameter((CA.Count() == 0) ? $"ps{PropName}" : PropName, MySqlDbType.JSON);
                    param.Direction = ParameterDirection.Output;
                    param.Value = null;
                    result.Add(param);
                }
            }
            return result;
        }
        #endregion
        #region Tools
        sealed class Tuple<T1, T2>
        {
            public Tuple() { }
            public Tuple(T1 value1, T2 value2) { Value1 = value1; Value2 = value2; }
            public T1 Value1 { get; set; }
            public T2 Value2 { get; set; }
        }
        private List<T> ConvertDtc<T>(DataTableCollection tables) where T : BaseModelResponse, new()
        {
            if (tables == null || tables.Count == 0)
                return new List<T>() { };

            if (tables.Count > 1)
            {
                string PropName = string.Empty;
                string PropOracleName = string.Empty;
                T response = ConvertDt<T>(tables["cuDatos"]).FirstOrDefault();
                Type tipo = typeof(T);
                foreach (PropertyInfo pi in typeof(T).GetProperties().Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(SPResponseCursorColumnName))))
                {
                    PropName = pi.Name;

                    List<CustomAttributeData> CA = pi.CustomAttributes.Where(x => x.AttributeType == typeof(SPResponseCursorColumnName)).ToList();
                    if (CA.Count() > 0)
                        PropOracleName = CA[0].ConstructorArguments[0].Value.ToString();

                    List<dynamic> LD = ToDynamic(tables[PropOracleName]);
                    FillObject(pi.PropertyType, LD, pi, response);
                }
                return new List<T>() { response };
            }

            return ConvertDt<T>(tables[0]);
        }
        private List<T> ConvertDt<T>(DataTable table) where T : BaseModelResponse, new()
        {

            if (table.Rows.Count == 0)
                return new List<T>();

            List<Tuple<DataColumn, PropertyInfo>> map =
            new List<Tuple<DataColumn, PropertyInfo>>();
            string PropName = string.Empty;
            foreach (PropertyInfo pi in typeof(T).GetProperties())
            {
                PropName = pi.Name;
                List<CustomAttributeData> CA = pi.CustomAttributes.Where(x => x.AttributeType == typeof(SPResponseColumnName)).ToList();
                if (CA.Count() > 0)
                    PropName = CA[0].ConstructorArguments[0].Value.ToString();

                if (table.Columns.Contains(PropName))
                {
                    map.Add(new Tuple<DataColumn, PropertyInfo>(
                    table.Columns[PropName], pi));
                }
            }

            List<T> list = new List<T>(table.Rows.Count);
            foreach (DataRow row in table.Rows)
            {
                if (row == null)
                {
                    list.Add(null);
                    continue;
                }
                T item = new T();
                foreach (Tuple<DataColumn, PropertyInfo> pair in map)
                {
                    string val = "";
                    object value = row[pair.Value1];
                    if (value is DBNull) value = null;
                    else
                    {
                        byte[] utf = System.Text.Encoding.UTF8.GetBytes(value.ToString());
                        val = System.Text.Encoding.UTF8.GetString(utf);
                    }                    
                    pair.Value2.SetValue(item, val, null);
                }
                list.Add(item);
            }
            return list;
        }
        public List<dynamic> ToDynamic(DataTable dt)
        {
            var dynamicDt = new List<dynamic>();
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    dynamic dyn = new ExpandoObject();
                    dynamicDt.Add(dyn);
                    foreach (DataColumn column in dt.Columns)
                    {
                        //column.DataType = typeof(string);
                        //column.DefaultValue = String.Empty;
                        var dic = (IDictionary<string, object>)dyn;
                        dic[column.ColumnName] = row[column];
                    }
                }
            }
            return dynamicDt;
        }
        private T GenerateErrorObject<T>(string Error) where T : BaseModelResponse, new()
        {
            T o = (T)Activator.CreateInstance(typeof(T));
            return o;
        }
        private List<T> GenerateErrorObjectList<T>(string Error) where T : BaseModelResponse, new()
        {
            T o = (T)Activator.CreateInstance(typeof(T));
            List<T> response = new List<T>(1);
            response.Add(o);
            return response;
        }
        public void FillObject<T>(Type type, List<dynamic> genList, PropertyInfo property, T main) where T : BaseModelResponse, new()
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
                FillDynamicList(type, genList, property, main);
            else if (genList.Count == 1)
                FillDynamicObject(genList, property, main);
            else if (genList.Count > 0)
                throw new Exception("[Error] Mas de un resultado encontrado en el foreignObject durante el FillDetails");
        }
        private void FillDynamicList<T>(Type type, List<dynamic> genList, PropertyInfo property, T main) where T : BaseModelResponse, new()
        {
            Type ListType = type.GetGenericArguments()[0];
            System.Collections.IList lista = CreateList(ListType);
            foreach (dynamic objD in genList)
            {
                var values = (IDictionary<string, object>)objD;
                Object obj = Activator.CreateInstance(ListType);

                lista.Add(FillDynaimcProperty(obj, values));
            }
            property.SetValue(main, lista);
        }
        private void FillDynamicObject<T>(List<dynamic> genList, PropertyInfo property, T main) where T : BaseModelResponse, new()
        {
            var values = (IDictionary<string, object>)genList[0];
            Object obj = Activator.CreateInstance(property.PropertyType);
            property.SetValue(main, FillDynaimcProperty(obj, values));
        }
        private Object FillDynaimcProperty(Object obj, IDictionary<string, object> values)
        {
            Type t = obj.GetType();
            List<PropertyInfo> props = t.GetProperties().ToList();
            string PropName = string.Empty;
            foreach (PropertyInfo pI in props)
            {
                PropName = pI.Name;
                List<CustomAttributeData> CA = pI.CustomAttributes.Where(x => x.AttributeType == typeof(SPResponseColumnName)).ToList();
                if (CA.Count() > 0)
                {
                    PropName = CA[0].ConstructorArguments[0].Value.ToString();

                    Type type = values.Where(x => x.Key == PropName).Select(x => x.Value).SingleOrDefault().GetType();

                    object value = values.Where(x => x.Key == PropName).Select(x => x.Value).SingleOrDefault();

                    if (pI.PropertyType.Name == "String" && type.Name == "DBNull")
                        value = "";
                    else if (pI.PropertyType.Name == "Int" && type.Name == "DBNull")
                        value = 0;
                    else if (pI.PropertyType.Name == "Decimal" && type.Name == "DBNull")
                        value = Convert.ToDecimal(0);
                    else if (pI.PropertyType.Name == "Decimal" && type.Name == "Int64")
                        value = Convert.ToDecimal(value);
                    else if (pI.PropertyType.Name == "Decimal" && type.Name == "Single")
                        value = Convert.ToDecimal(value.ToString());
                    else if (pI.PropertyType.Name == "String" && type.Name == "DateTime")
                    {
                        CultureInfo ci = new CultureInfo("es-MX");
                        ci = new CultureInfo("es-MX");
                        DateTime dt = Convert.ToDateTime(value);
                        value = dt.ToString("dd/MM/yyyy", ci);
                    }

                    pI.SetValue(obj, value);
                }
            }
            return obj;
        }
        private System.Collections.IList CreateList(Type myType)
        {
            Type genericListType = typeof(List<>).MakeGenericType(myType);
            return (System.Collections.IList)Activator.CreateInstance(genericListType);
        }
        #endregion
        #region DisponseMethods
        // Pointer to an external unmanaged resource.
        private IntPtr handle;
        // Other managed resource this class uses.
        private Component component = new Component();
        // Track whether Dispose has been called.
        private bool disposed = false;

        // Implement IDisposable.
        // Do not make this method virtual.
        // A derived class should not be able to override this method.
        public void Dispose()
        {
            Dispose(disposing: true);
            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SuppressFinalize to
            // take this object off the finalization queue
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }

        // Dispose(bool disposing) executes in two distinct scenarios.
        // If disposing equals true, the method has been called directly
        // or indirectly by a user's code. Managed and unmanaged resources
        // can be disposed.
        // If disposing equals false, the method has been called by the
        // runtime from inside the finalizer and you should not reference
        // other objects. Only unmanaged resources can be disposed.
        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this.disposed)
            {
                // If disposing equals true, dispose all managed
                // and unmanaged resources.
                if (disposing)
                {
                    // Dispose managed resources.
                    component.Dispose();
                }

                // Note disposing has been done.
                disposed = true;
            }
        }

        // Use interop to call the method necessary
        // to clean up the unmanaged resource.
        [System.Runtime.InteropServices.DllImport("Kernel32")]
        private extern static Boolean CloseHandle(IntPtr handle);

        // Use C# finalizer syntax for finalization code.
        // This finalizer will run only if the Dispose method
        // does not get called.
        // It gives your base class the opportunity to finalize.
        // Do not provide finalizer in types derived from this class.
        ~DBO()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(disposing: false) is optimal in terms of
            // readability and maintainability.
            Dispose(disposing: false);
        }
        #endregion
    }
}