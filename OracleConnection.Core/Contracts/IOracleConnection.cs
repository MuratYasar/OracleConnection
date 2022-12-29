using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OracleConnection.Core.Enums;

namespace OracleConnection.Core.Contracts
{
    internal interface IOracleConnection
    {
        Oracle.ManagedDataAccess.Client.OracleConnection GetOracleConnection(ConnectionCode connectionCode, DBEnvironment dBEnvironment);
    }
}
