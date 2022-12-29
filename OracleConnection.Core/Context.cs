using Oracle.ManagedDataAccess.Client;
using OracleConnection.Core.Dtos;
using OracleConnection.Core.Implementations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OracleConnection.Core.Enums;

namespace OracleConnection.Core
{
    public class Context : IDisposable
    {
        private bool disposed = false;

        private readonly ConnectionCode _connectionCode;
        private readonly DBEnvironment _dBEnvironment;
        private readonly Oracle.ManagedDataAccess.Client.OracleConnection _oracleConnection;

        public Context() { }

        public Context(ConnectionCode connectionCode = ConnectionCode.A, DBEnvironment dBEnvironment = DBEnvironment.DEVELOPMENT)
        {
            _connectionCode = connectionCode;
            _dBEnvironment = dBEnvironment;
            _oracleConnection = new CreateOracleConnection().GetOracleConnection(_connectionCode, _dBEnvironment);
        }

        public ResponseOfT<T> GetResultSetFromStoredProcedureUsingDataReader<T>(string storedProcedureName, List<OracleParameter> oracleParameters)
        {
            using (DataTable dt = new DataTable())
            {
                using (CreateOracleCommand cmd = new CreateOracleCommand())
                {
                    var command = cmd.GetOracleCommandForStoredProcedure(_oracleConnection, storedProcedureName, oracleParameters);

                    try
                    {
                        OracleDataReader objReader = command.ExecuteReader();

                        if (objReader.HasRows)
                        {
                            dt.Load(objReader);
                        }

                        objReader.Close();
                        objReader.Dispose();

                        var res = Helper.ConvertDataTable<T>(dt);

                        return (new ResponseOfT<T>() { ReturnObject = res, Error = string.Empty });
                    }
                    catch (Exception ex)
                    {
                        return (new ResponseOfT<T>() { ReturnObject = null, Error = ex.ToString() });
                    }
                }
            }
        }

        public Response GetResultSetFromStoredProcedureUsingDataReader(string storedProcedureName, List<OracleParameter> oracleParameters)
        {
            using (DataTable dt = new DataTable())
            {
                using (CreateOracleCommand cmd = new CreateOracleCommand())
                {
                    var command = cmd.GetOracleCommandForStoredProcedure(_oracleConnection, storedProcedureName, oracleParameters);

                    try
                    {
                        OracleDataReader objReader = command.ExecuteReader();

                        if (objReader.HasRows)
                        {
                            dt.Load(objReader);
                        }

                        objReader.Close();
                        objReader.Dispose();

                        return (new Response() { ReturnObject = dt, Error = string.Empty });
                    }
                    catch (Exception ex)
                    {
                        return new Response() { ReturnObject = null, Error = ex.ToString() };
                    }
                }
            }
        }

        public ResponseOfT<T> GetResultSetFromStoredProcedureUsingDataAdapter<T>(string storedProcedureName, List<OracleParameter> oracleParameters)
        {
            using (DataTable dataTable = new DataTable())
            {
                using (CreateOracleCommand cmd = new CreateOracleCommand())
                {
                    var command = cmd.GetOracleCommandForStoredProcedure(_oracleConnection, storedProcedureName, oracleParameters);

                    try
                    {
                        OracleDataAdapter oracleDataAdapter = new OracleDataAdapter(command);
                        oracleDataAdapter.Fill(dataTable);

                        var res = Helper.ConvertDataTable<T>(dataTable);

                        return (new ResponseOfT<T>() { ReturnObject = res, Error = string.Empty });
                    }
                    catch (Exception ex)
                    {
                        return (new ResponseOfT<T>() { ReturnObject = null, Error = ex.ToString() });
                    }
                }
            }
        }

        public Response GetResultSetFromStoredProcedureUsingDataAdapter(string storedProcedureName, List<OracleParameter> oracleParameters)
        {
            using (DataTable dataTable = new DataTable())
            {
                using (CreateOracleCommand cmd = new CreateOracleCommand())
                {
                    var command = cmd.GetOracleCommandForStoredProcedure(_oracleConnection, storedProcedureName, oracleParameters);

                    try
                    {
                        OracleDataAdapter oracleDataAdapter = new OracleDataAdapter(command);
                        oracleDataAdapter.Fill(dataTable);

                        return (new Response() { ReturnObject = dataTable, Error = string.Empty });
                    }
                    catch (Exception ex)
                    {
                        return (new Response() { ReturnObject = null, Error = ex.ToString() });
                    }
                }
            }
        }

        public ResponseExec ModifyDataUsingStoredProcedure(string storedProcedureName, List<OracleParameter> oracleParameters)
        {
            using (CreateOracleCommand cmd = new CreateOracleCommand())
            {
                var command = cmd.GetOracleCommandForStoredProcedure(_oracleConnection, storedProcedureName, oracleParameters);

                try
                {
                    var result = command.ExecuteNonQuery();

                    return (new ResponseExec() { ReturnObject = true, Error = string.Empty });
                }
                catch (Exception ex)
                {
                    return (new ResponseExec() { ReturnObject = false, Error = ex.ToString() });
                }

            }
        }

        #region Dispose

        public void Dispose()
        {
            Cleanup(false);
            GC.SuppressFinalize(this);
        }

        private void Cleanup(bool calledFromFinalizer)
        {
            if (this.disposed)
                return;

            if (!calledFromFinalizer)
            {
                //Dispose Managed Resources
                if (_oracleConnection != null)
                {
                    _oracleConnection.Dispose();
                }
            }

            //Dispose Unmanaged resources

            this.disposed = true;
        }

        ~Context()
        {
            Cleanup(true);
        }

        #endregion
    }
}
