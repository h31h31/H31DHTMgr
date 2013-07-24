using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Data.OleDb;

namespace H31SQLLibrary
{
    /// <summary>
    /// 数据库操作基类(for Access)
    /// </summary>
    internal class H31Access: H31DBPage, IH31DBSQL
    {
        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="tblName">表名</param>
        /// <param name="fldName">字段名</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageIndex">第几页</param>
        /// <param name="fldSort">排序字段</param>
        /// <param name="sort">升序{False}/降序(True)</param>
        /// <param name="strCondition">条件(不需要where)</param>
        public DataSet PageList(string connectionString, string tblName, int pageSize,
            int pageIndex, string fldSort, bool sort, string condition)
        {
            string sql = GetPagerSQL(condition, pageSize, pageIndex, fldSort, tblName, sort);
            return ExecuteReader(connectionString, CommandType.Text, sql, null);
        }

        /// <summary>
        /// 执行查询，返回DataSet
        /// </summary>
        public DataSet ExecuteQuery(string connectionString, CommandType cmdType, string cmdText,
            params DbParameter[] cmdParms)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                using (OleDbCommand cmd = new OleDbCommand())
                {
                    PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                    using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                        return ds;
                    }
                }
            }
        }

        /// <summary>
        /// 在事务中执行查询，返回DataSet
        /// </summary>
        public DataSet ExecuteQuery(DbTransaction trans, CommandType cmdType, string cmdText,
            params DbParameter[] cmdParms)
        {
            OleDbCommand cmd = new OleDbCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, cmdParms);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "ds");
            cmd.Parameters.Clear();
            return ds;
        }

        /// <summary>
        /// 执行 Transact-SQL 语句并返回受影响的行数。
        /// </summary>
        public int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText,
            params DbParameter[] cmdParms)
        {
            OleDbCommand cmd = new OleDbCommand();

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// 在事务中执行 Transact-SQL 语句并返回受影响的行数。
        /// </summary>
        public int ExecuteNonQuery(DbTransaction trans, CommandType cmdType, string cmdText,
            params DbParameter[] cmdParms)
        {
            OleDbCommand cmd = new OleDbCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, cmdParms);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }


        /// <summary>
        /// 执行查询，返回DataReader
        /// </summary>
        public DataSet ExecuteReader(string connectionString, CommandType cmdType, string cmdText,
            params DbParameter[] cmdParms)
        {
            OleDbCommand cmd = new OleDbCommand();
            OleDbConnection conn = new OleDbConnection(connectionString);

            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "ds");
                cmd.Parameters.Clear();
                return ds;
            }
            catch
            {
                conn.Close();
                throw;
            }
            return null;
        }

        /// <summary>
        /// 在事务中执行查询，返回DataReader
        /// </summary>
        public DataSet ExecuteReader(DbTransaction trans, CommandType cmdType, string cmdText,
            params DbParameter[] cmdParms)
        {
            OleDbCommand cmd = new OleDbCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, cmdParms);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "ds");
            cmd.Parameters.Clear();
            return ds;
        }

        /// <summary>
        /// 执行查询，并返回查询所返回的结果集中第一行的第一列。忽略其他列或行。
        /// </summary>
        public object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText,
            params DbParameter[] cmdParms)
        {
            OleDbCommand cmd = new OleDbCommand();

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, cmdParms);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// 在事务中执行查询，并返回查询所返回的结果集中第一行的第一列。忽略其他列或行。
        /// </summary>
        public object ExecuteScalar(DbTransaction trans, CommandType cmdType, string cmdText,
            params DbParameter[] cmdParms)
        {
            OleDbCommand cmd = new OleDbCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, cmdParms);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// 生成要执行的命令
        /// </summary>
        private static void PrepareCommand(DbCommand cmd, DbConnection conn, DbTransaction trans, CommandType cmdType,
            string cmdText, DbParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (OleDbParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }
    }
}
