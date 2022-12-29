using Oracle.ManagedDataAccess.Client;
using OracleConnection.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OracleConnection.Core.Implementations
{
    internal class CreateOracleCommand : IOracleCommand, IDisposable
    {
        private bool disposed = false;
        private Oracle.ManagedDataAccess.Client.OracleCommand _oracleCommand;

        public CreateOracleCommand()
        {
        }

        public Oracle.ManagedDataAccess.Client.OracleCommand GetOracleCommand(Oracle.ManagedDataAccess.Client.OracleConnection oracleConnection)
        {
            _oracleCommand = oracleConnection.CreateCommand();

            return _oracleCommand;
        }

        public Oracle.ManagedDataAccess.Client.OracleCommand GetOracleCommandForStoredProcedure(Oracle.ManagedDataAccess.Client.OracleConnection oracleConnection, string spName, List<OracleParameter> oracleParameters)
        {
            _oracleCommand = oracleConnection.CreateCommand();
            _oracleCommand.CommandText = spName;
            _oracleCommand.CommandType = System.Data.CommandType.StoredProcedure;

            if (oracleParameters != null && oracleParameters.Count > 0)
            {
                _oracleCommand.Parameters.AddRange(oracleParameters.ToArray());
            }

            return _oracleCommand;
        }

        public Oracle.ManagedDataAccess.Client.OracleCommand GetOracleCommandForSelect(Oracle.ManagedDataAccess.Client.OracleConnection oracleConnection, string spName, List<OracleParameter> oracleParameters)
        {
            _oracleCommand = oracleConnection.CreateCommand();
            _oracleCommand.CommandText = spName;
            _oracleCommand.CommandType = System.Data.CommandType.StoredProcedure;

            if (oracleParameters != null && oracleParameters.Count > 0)
            {
                _oracleCommand.Parameters.AddRange(oracleParameters.ToArray());
            }

            return _oracleCommand;
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

                if (_oracleCommand != null && _oracleCommand.Parameters != null)
                {
                    _oracleCommand.Parameters.Clear();
                    foreach (OracleParameter item in _oracleCommand.Parameters)
                    {
                        item.Dispose();
                    }
                }

                if (_oracleCommand != null)
                {
                    _oracleCommand.Dispose();
                }
            }

            //Dispose Unmanaged resources

            this.disposed = true;
        }

        ~CreateOracleCommand()
        {
            Cleanup(true);
        }

        #endregion
    }
}
