using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OracleConnection.Core.Contracts
{
    internal interface IOracleCommand
    {
        OracleCommand GetOracleCommand(Oracle.ManagedDataAccess.Client.OracleConnection oracleConnection);
    }
}
