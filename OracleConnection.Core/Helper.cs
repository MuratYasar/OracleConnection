using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static OracleConnection.Core.Enums;

namespace OracleConnection.Core
{
    internal static class Helper
    {
        public static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                    {
                        var value = dr[column.ColumnName];

                        if (value == DBNull.Value)
                        {
                            value = null;
                        }

                        pro.SetValue(obj, value, null);
                    }
                    else
                        continue;
                }
            }
            return obj;
        }

        public static void LogDB(LOGLEVEL logLevel, string methodName, List<OracleParameter> parameters, string message)
        {
            var paramsValue = parameters != null && parameters.Count > 0 ? JsonSerializer.Serialize(parameters) : string.Empty;

            using (Context contextClassic = new Context(ConnectionCode.A, DBEnvironment.PRODUCTION))
            {
                List<OracleParameter> list = new List<OracleParameter>()
                {
                    new OracleParameter() {Direction = System.Data.ParameterDirection.Input,OracleDbType = OracleDbType.TimeStamp, ParameterName = "p_date",Value = DateTime.Now},
                    new OracleParameter() {Direction = System.Data.ParameterDirection.Input,OracleDbType = OracleDbType.NVarchar2, ParameterName = "p_methodname",Value = methodName},
                    new OracleParameter() {Direction = System.Data.ParameterDirection.Input,OracleDbType = OracleDbType.NVarchar2, ParameterName = "p_parameter",Value = paramsValue},
                    new OracleParameter() {Direction = System.Data.ParameterDirection.Input,OracleDbType = OracleDbType.Varchar2, ParameterName = "p_level",Value = logLevel.ToString()},
                    new OracleParameter() {Direction = System.Data.ParameterDirection.Input,OracleDbType = OracleDbType.NVarchar2, ParameterName = "p_message",Value = message},
                    
                };

                var insertResult = contextClassic.ModifyDataUsingStoredProcedure("TEST.SP_DBLOG_INSERT", list);
            }
        }

        public static void LogFile(string logMessage)
        {
            string _logPath = @"C:\Log\OracleConnection\Core\";

            try
            {
                if (!Directory.Exists(@"C:\Log"))
                {
                    Directory.CreateDirectory(@"C:\Log");
                }
                if (!Directory.Exists(@"C:\Log\OracleConnection"))
                {
                    Directory.CreateDirectory(@"C:\Log\OracleConnection");
                }
                if (!Directory.Exists(@"C:\Log\GetOracleConnection\Core"))
                {
                    Directory.CreateDirectory(@"C:\Log\GetOracleConnection\Core");
                }

                using (StreamWriter w = File.AppendText(_logPath + DateTime.Now.ToString("yyyyMMdd") + ".txt"))
                {
                    w.Write("\r\nLog Entry : ");
                    w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
                    w.WriteLine("  :{0}", logMessage);
                    w.WriteLine("-------------------------------");
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
