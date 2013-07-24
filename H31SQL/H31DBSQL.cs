using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;
//using MySql.Data.MySqlClient;
//using System.Data.OracleClient;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace H31SQLLibrary
{
    /// <summary>
    /// 数据库
    /// </summary>
    public class H31DBSQL
    {
        /// <summary>
        /// 枚举：数据库类型
        /// </summary>
        public enum DatabaseTypes
        {
            Sql, MySql, Oracle, Access
        }

        private DatabaseTypes databaseType;
        private string connectionString;

        public H31DBSQL()
        { }

        public H31DBSQL(DatabaseTypes type, string connectionString)
        {
            this.databaseType = type;
            this.connectionString = connectionString;
        }

        /// <summary>
        /// 数据库类型
        /// </summary>
        public DatabaseTypes DatabaseType
        {
            get { return databaseType; }
            set { databaseType = value; }
        }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionString
        {
            get { return connectionString; }
            set { connectionString = value; }
        }

        private IH31DBSQL h31DBSql
        {
            get
            {
                switch (databaseType)
                {
                    case DatabaseTypes.Access:
                        return new H31Access();
                    //case DatabaseTypes.MySql:
                    //    return new MySqlHelper();
                    //case DatabaseTypes.Oracle:
                    //    return new MySqlHelper();
                    case DatabaseTypes.Sql:
                    default:
                        return new H31SQL2000();
                }
            }
        }

        public DbConnection CreateConnection()
        {
            switch (databaseType)
            {
                //case DatabaseTypes.MySql:
                //    return new MySqlConnection(connectionString);
                //case DatabaseTypes.Oracle:
                //    return new OracleConnection(connectionString);
                case DatabaseTypes.Access:
                    return new OleDbConnection(connectionString);
                case DatabaseTypes.Sql:
                default:
                    return new SqlConnection(connectionString);
            }
        }

        #region === 创造DbParameter的实例 ===

        /// <summary>
        /// 创造输入DbParameter的实例
        /// </summary>
        public DbParameter CreateInDbParameter(string paraName, DbType dbType, int size, object value)
        {
            return CreateDbParameter(paraName, dbType, size, value, ParameterDirection.Input);
        }

        /// <summary>
        /// 创造输入DbParameter的实例
        /// </summary>
        public DbParameter CreateInDbParameter(string paraName, DbType dbType, object value)
        {
            return CreateDbParameter(paraName, dbType, 0, value, ParameterDirection.Input);
        }

        /// <summary>
        /// 创造输出DbParameter的实例
        /// </summary>        
        public DbParameter CreateOutDbParameter(string paraName, DbType dbType, int size, object value)
        {
            return CreateDbParameter(paraName, dbType, size, value, ParameterDirection.Output);
        }

        /// <summary>
        /// 创造输出DbParameter的实例
        /// </summary>        
        public DbParameter CreateOutDbParameter(string paraName, DbType dbType, object value)
        {
            return CreateDbParameter(paraName, dbType, 0, value, ParameterDirection.Output);
        }

        /// <summary>
        /// 创造DbParameter的实例
        /// </summary>
        public DbParameter CreateDbParameter(string paraName, DbType dbType, int size, object value, ParameterDirection direction)
        {
            DbParameter para;
            switch (databaseType)
            {
                //case DatabaseTypes.MySql:
                //    para = new MySqlParameter();
                //    break;
                //case DatabaseTypes.Oracle:
                //    para = new OracleParameter();
                //    break;
                case DatabaseTypes.Access:
                    para = new OleDbParameter();
                    break;
                case DatabaseTypes.Sql:
                default:
                    para = new SqlParameter();
                    break;
            }
            para.ParameterName = paraName;
            if (size != 0)
                para.Size = size;
            para.DbType = dbType;
            para.Value = value;
            para.Direction = direction;

            return para;
        }

        #endregion

        #region === 数据库执行方法 ===

        public int ExecuteNonQuery(CommandType cmdType, string cmdText, params DbParameter[] cmdParms)
        {
            return h31DBSql.ExecuteNonQuery(connectionString, cmdType, cmdText, cmdParms);
        }

        public int ExecuteNonQuery(DbTransaction trans, CommandType cmdType, string cmdText, params DbParameter[] cmdParms)
        {
            return h31DBSql.ExecuteNonQuery(trans, cmdType, cmdText, cmdParms);
        }

        public DataSet ExecuteQuery(DbTransaction trans, CommandType cmdType, string cmdText, params DbParameter[] cmdParms)
        {
            return h31DBSql.ExecuteQuery(trans, cmdType, cmdText, cmdParms);
        }

        public DataSet ExecuteQuery(CommandType cmdType, string cmdText, params DbParameter[] cmdParms)
        {
            return h31DBSql.ExecuteQuery(connectionString, cmdType, cmdText, cmdParms);
        }

        public DataSet ExecuteReader(DbTransaction trans, CommandType cmdType, string cmdText, params DbParameter[] cmdParms)
        {
            return h31DBSql.ExecuteReader(trans, cmdType, cmdText, cmdParms);
        }

        public DataSet ExecuteReader(CommandType cmdType, string cmdText, params DbParameter[] cmdParms)
        {
            return h31DBSql.ExecuteReader(connectionString, cmdType, cmdText, cmdParms);
        }

        public object ExecuteScalar(DbTransaction trans, CommandType cmdType, string cmdText, params DbParameter[] cmdParms)
        {
            return h31DBSql.ExecuteScalar(trans, cmdType, cmdText, cmdParms);
        }

        public object ExecuteScalar(CommandType cmdType, string cmdText, params DbParameter[] cmdParms)
        {
            return h31DBSql.ExecuteScalar(connectionString, cmdType, cmdText, cmdParms);
        }

        public DataSet PageList(string tblName, int pageSize, int pageIndex, string fldSort, bool sort, string condition)
        {
            return h31DBSql.PageList(connectionString, tblName, pageSize, pageIndex, fldSort, sort, condition);
        }

        #endregion

        #region === 由Object取值 ===

        /// <summary>
        /// 取得Int值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetInt(object obj)
        {
            if (obj.ToString() != "")
                return int.Parse(obj.ToString());
            else
                return 0;
        }

        /// <summary>
        /// 取得byte值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public byte Getbyte(object obj)
        {
            if (obj.ToString() != "")
                return byte.Parse(obj.ToString());
            else
                return 0;
        }

        /// <summary>
        /// 获得Long值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public long GetLong(object obj)
        {
            if (obj.ToString() != "")
                return long.Parse(obj.ToString());
            else
                return 0;
        }

        /// <summary>
        /// 取得Decimal值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public decimal GetDecimal(object obj)
        {
            if (obj.ToString() != "")
                return decimal.Parse(obj.ToString());
            else
                return 0;
        }

        /// <summary>
        /// 取得Guid值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Guid GetGuid(object obj)
        {
            if (obj.ToString() != "")
                return new Guid(obj.ToString());
            else
                return Guid.Empty;
        }

        /// <summary>
        /// 取得DateTime值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public DateTime GetDateTime(object obj)
        {
            if (obj.ToString() != "" && obj.ToString() != "0000-0-0 0:00:00")
                return DateTime.Parse(obj.ToString());
            else
                return DateTime.MinValue;
        }

        /// <summary>
        /// 取得bool值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool GetBool(object obj)
        {
            if (obj.ToString() == "1" || obj.ToString().ToLower() == "true")
                return true;
            else
                return false;
        }

        /// <summary>
        /// 取得byte[]
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Byte[] GetByte(object obj)
        {
            if (obj.ToString() != "")
            {
                return (Byte[])obj;
            }
            else
                return null;
        }

        /// <summary>
        /// 取得string值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string GetString(object obj)
        {
            return obj.ToString();
        }

        #endregion
    }
}
