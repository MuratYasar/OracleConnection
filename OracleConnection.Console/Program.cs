// See https://aka.ms/new-console-template for more information
using Oracle.ManagedDataAccess.Client;
using OracleConnection.Core.Models;

using (OracleConnection.Core.ContextEntity contextCore = new OracleConnection.Core.ContextEntity(OracleConnection.Core.Enums.ConnectionCode.A, OracleConnection.Core.Enums.DBEnvironment.DEVELOPMENT))
{
    var res = contextCore.DBLog.ToList();
}

using (OracleConnection.Core.Context contextTest = new OracleConnection.Core.Context(OracleConnection.Core.Enums.ConnectionCode.A, OracleConnection.Core.Enums.DBEnvironment.DEVELOPMENT))
{
    List<OracleParameter> list = new List<OracleParameter>()
    {
        new OracleParameter() {Direction = System.Data.ParameterDirection.Input,    OracleDbType = OracleDbType.NVarchar2,  ParameterName = "P_LEVEL",   Value = "INFO"},
        new OracleParameter() {Direction = System.Data.ParameterDirection.Output,   OracleDbType = OracleDbType.RefCursor,  ParameterName = "CURSOR"}
    };

    var resultReader = contextTest.GetResultSetFromStoredProcedureUsingDataReader("TEST.SP_GETLOG", list);

    var resultAdapter = contextTest.GetResultSetFromStoredProcedureUsingDataReader("TEST.SP_GETLOG", list);

    var resultReaderOfTypeT = contextTest.GetResultSetFromStoredProcedureUsingDataReader<DBLog>("TEST.SP_GETLOG", list);

    var resultAdapterOfTypeT = contextTest.GetResultSetFromStoredProcedureUsingDataAdapter<DBLog>("TEST.SP_GETLOG", list);
}