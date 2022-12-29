using OracleConnection.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OracleConnection.Core.Enums;

namespace OracleConnection.Core.Implementations
{
    internal class CreateOracleConnection : IOracleConnection, IDisposable
    {
        private bool disposed = false;
        private Oracle.ManagedDataAccess.Client.OracleConnection _connection;

        public CreateOracleConnection()
        {
        }

        public Oracle.ManagedDataAccess.Client.OracleConnection GetOracleConnection(ConnectionCode connectionCode, DBEnvironment dBEnvironment)
        {
            _connection = new Oracle.ManagedDataAccess.Client.OracleConnection(AppConfig.GetConnectionString(connectionCode, dBEnvironment));

            if (_connection.State == System.Data.ConnectionState.Closed)
            {
                _connection.Open();
            }

            return _connection;
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
                if (_connection != null)
                {
                    if (_connection.State == System.Data.ConnectionState.Open)
                    {
                        _connection.Close();
                    }

                    _connection.Dispose();
                }
            }

            //Dispose Unmanaged resources

            this.disposed = true;
        }

        ~CreateOracleConnection()
        {
            Cleanup(true);
        }

        #endregion
    }
}
