using System;

namespace H31SQLLibrary
{
    interface IH31DBSQL
    {
        int ExecuteNonQuery(string connectionString, System.Data.CommandType cmdType, string cmdText, params System.Data.Common.DbParameter[] cmdParms);
        int ExecuteNonQuery(System.Data.Common.DbTransaction trans, System.Data.CommandType cmdType, string cmdText, params System.Data.Common.DbParameter[] cmdParms);
        System.Data.DataSet ExecuteQuery(System.Data.Common.DbTransaction trans, System.Data.CommandType cmdType, string cmdText, params System.Data.Common.DbParameter[] cmdParms);
        System.Data.DataSet ExecuteQuery(string connectionString, System.Data.CommandType cmdType, string cmdText, params System.Data.Common.DbParameter[] cmdParms);
        System.Data.DataSet ExecuteReader(System.Data.Common.DbTransaction trans, System.Data.CommandType cmdType, string cmdText, params System.Data.Common.DbParameter[] cmdParms);
        System.Data.DataSet ExecuteReader(string connectionString, System.Data.CommandType cmdType, string cmdText, params System.Data.Common.DbParameter[] cmdParms);
        object ExecuteScalar(System.Data.Common.DbTransaction trans, System.Data.CommandType cmdType, string cmdText, params System.Data.Common.DbParameter[] cmdParms);
        object ExecuteScalar(string connectionString, System.Data.CommandType cmdType, string cmdText, params System.Data.Common.DbParameter[] cmdParms);
        System.Data.DataSet PageList(string connectionString, string tblName, int pageSize, int pageIndex, string fldSort, bool sort, string condition);
    }
}
