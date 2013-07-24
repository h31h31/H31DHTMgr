using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Windows.Forms;
using H31DHTMgr;

namespace H31SQLLibrary
{
    public class H31SQL
    {
        protected static H31DBSQL dbsql = GetSQL("NorthWind");
        private static DbConnection conn = dbsql.CreateConnection();

        /// <summary>
        /// 从Web.config从读取数据库的连接以及数据库类型
        /// </summary>
        private static H31DBSQL GetSQL(string connectionStringName)
        {
            H31DBSQL h31dbsql = new H31DBSQL();

            // 从Web.config中读取数据库类型
            //string providerName = System.Configuration.ConfigurationManager.ConnectionStrings[connectionStringName].ProviderName;
            string providerName = "Access";
            switch (providerName)
            {
                case "Oracle":
                    h31dbsql.DatabaseType = H31DBSQL.DatabaseTypes.Oracle;
                    break;
                case "MySql":
                    h31dbsql.DatabaseType = H31DBSQL.DatabaseTypes.MySql;
                    break;
                case "Access":
                    h31dbsql.DatabaseType = H31DBSQL.DatabaseTypes.Access;
                    break;
                case "SqlClient":
                default:
                    h31dbsql.DatabaseType = H31DBSQL.DatabaseTypes.Sql;
                    break;
            }

            //string connstr=System.Configuration.ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
            string connstr = "H31DHT.mdb;Jet OLEDB:Database Password=";
            // 从Web.config中读取数据库连接
            switch (h31dbsql.DatabaseType)
            {
                case H31DBSQL.DatabaseTypes.Access:
                    h31dbsql.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Persist Security Info=true;Data Source="
                        //+ HttpContext.Current.Request.PhysicalApplicationPath
                        + connstr;
                    break;
                default:
                    h31dbsql.ConnectionString = connstr;
                    break;
            }

            return h31dbsql;
        }

        /// <summary>
        /// 判断连接数据库   2009-11-20
        /// </summary>
        public static bool ConnectToDBServer()
        {
            try
            {
                conn.Open();
            }
            catch (Exception e)
            {
                MessageBox.Show("如果是数据库连接配置不对,请修改!\r\n如果是数据库没有启动,请先启动!\r\n" + e.Message, "连接错误提示");
                H31Debug.PrintLn(e.Message);
                conn.Close();
                return false;
            }
            finally
            {
            }
            return true;
        }

        /// <summary>
        /// 关闭数据库   2009-11-20
        /// </summary>
        public static void CloseTheDB()
        {
            try
            {
                if (conn.State.ToString() == "Open") conn.Close();
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message, "连接错误提示");
                H31Debug.PrintLn(e.Message);
            }
        }

        #region  成员方法
        /// <summary>
        /// 检查有没有这条HASH纪录，如果有，返回日志表号
        /// </summary>
        public static int CheckHashItemExist(string hashname,ref int keytype,ref int tableid,ref int isHanzi)
        {
            try
            {
                string strSql = string.Format("select ID,Detail,type from H31_DHT_AllKey where hashKey='{0}'", hashname);
                DataSet ds = dbsql.ExecuteQuery(CommandType.Text, strSql.ToString(), null);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    tableid = Convert.ToInt32(ds.Tables[0].Rows[0]["Detail"].ToString());
                    int typeid = Convert.ToInt32(ds.Tables[0].Rows[0]["type"].ToString());
                    keytype = typeid / 1000;
                    isHanzi = typeid % 2;
                    return 1;
                }
            }
            catch (System.Exception ex)
            {
                H31Debug.PrintLn(ex.StackTrace);
            }
            //if (keytype > 0 && keytype < 7)
            //{
            //    tableid = CheckHashItemExistTable(keytype,isHanzi, hashname);
            //    if (tableid > 0)
            //    {
            //        return 1;
            //    }
            //}
            //if (tableid <= 0)
            //{
            //    for (int i = 1; i < 7; i++)
            //    {
            //        tableid = CheckHashItemExistTable(i,1, hashname);
            //        if (tableid > 0)
            //        {
            //            keytype = i;
            //            isHanzi=1;
            //            return 1;
            //        }
            //        tableid = CheckHashItemExistTable(i, 0, hashname);
            //        if (tableid > 0)
            //        {
            //            keytype = i;
            //            isHanzi = 0;
            //            return 1;
            //        }
            //    }
            //}
            return 0;
        }

        /// <summary>
        /// 检查有没有这条HASH纪录，如果有，返回日志表号
        /// </summary>
        private static int CheckHashItemExistTable(int hashtype, int isHanzhi, string hashname)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                string tempstr1 = string.Format("select Detail from H31_DHT_TYPE_{0}_{1} where hashKey='{1}'", hashtype*100+1,isHanzhi, hashname);
                object countid = dbsql.ExecuteScalar(CommandType.Text, tempstr1.ToString(), null);
                if (countid == null)
                    return 0;
                if (dbsql.GetInt(countid) > 0)
                    return dbsql.GetInt(countid);
            }
            catch (System.Exception ex)
            {
                H31Debug.PrintLn(ex.StackTrace);
            }
            return -1;
        }

        /// <summary>
        /// 更新HASH纪录次
        /// </summary>
        public static int UpdateHashCount(int thetype, HASHITEM thehash, int thedetailID, int isHanzhi)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                string tempstr = string.Format("update H31_DHT_TYPE_{0}_{1}  set recvTimes=recvTimes+1 where hashKey='{1}'", thetype*100+1,isHanzhi, thehash.hashKey);
                strSql.Append(tempstr);
                return dbsql.ExecuteNonQuery(CommandType.Text, strSql.ToString(), null);
            }
            catch (System.Exception ex)
            {
                H31Debug.PrintLn(ex.StackTrace);
            }
            return -1;
        }
        /// <summary>
        /// 增加一条HASH纪录
        /// </summary>
        public static int AddNewHash(HASHTYPE thetype,HASHITEM thehash,int thedetailID,int isHanzhi)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                {
                    //添加工作计划
                    string tempstr = string.Format("insert into H31_DHT_TYPE_{0}_{1} (", (int)thetype * 100 + 1, isHanzhi);
                    strSql.Append(tempstr);
                    strSql.Append("hashKey,recvTime,updateTime,keyContent,KeyWords,keyType,recvTimes,fileCnt,filetotalSize,Detail)");
                    strSql.Append(" values (");
                    strSql.Append("@hashKey,@recvTime,@updateTime,@keyContent,@KeyWords,@keyType,@recvTimes,@fileCnt,@filetotalSize,@Detail)");
                    DbParameter[] cmdParms = {
                    dbsql.CreateInDbParameter("@hashKey",DbType.String,40,thehash.hashKey),
                    dbsql.CreateInDbParameter("@recvTime", DbType.DateTime,thehash.recvTime),
                    dbsql.CreateInDbParameter("@updateTime", DbType.DateTime,thehash.recvTime),
                    dbsql.CreateInDbParameter("@keyContent", DbType.String,thehash.keyContent),
                    dbsql.CreateInDbParameter("@KeyWords", DbType.String,thehash.keyWords),
                    dbsql.CreateInDbParameter("@keyType", DbType.Int32,thehash.keyType),
                    dbsql.CreateInDbParameter("@recvTimes", DbType.Int32,thehash.recvTimes),
                    dbsql.CreateInDbParameter("@fileCnt", DbType.Int32,thehash.fileCnt),
                    dbsql.CreateInDbParameter("@filetotalSize", DbType.Double,Convert.ToDouble(thehash.filetotalSize)),
                    dbsql.CreateInDbParameter("@Detail", DbType.Int32,thedetailID)
                    };
                    int res=dbsql.ExecuteNonQuery(CommandType.Text, strSql.ToString(), cmdParms);
                    if (res == 1)
                    {
                        string tempstr2 = string.Format("select top 1 id from H31_DHT_TYPE_{0}_{1} where hashKey='{2}' order by id desc", (int)thetype * 100 + 1, isHanzhi, thehash.hashKey);
                        object countid = dbsql.ExecuteScalar(CommandType.Text, tempstr2.ToString(), null);
                        if (countid == null)
                            return 0;
                        return (int)countid;
                    }
                }
            }
            catch (System.Exception ex)
            {
                H31Debug.PrintLn(ex.StackTrace);
            }
            return -1;
        }
        /// <summary>
        /// 增加一条HASH纪录的文件列表
        /// </summary>
        public static int AddNewHashDetail(int thetype, HASHFILEITEM thehash, int thedetailID)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                {
                    //添加工作计划
                    string tempstr = string.Format("insert into H31_DHT_DETAIL_{0} (", thedetailID);
                    strSql.Append(tempstr);
                    strSql.Append("hashID,hashType,recvTime,filename,filesize)");
                    strSql.Append(" values (");
                    strSql.Append("@hashID,@hashType,@recvTime,@filename,@filesize)");
                    DbParameter[] cmdParms = {
                    dbsql.CreateInDbParameter("@hashID",DbType.Int32,thehash.hashID),
                    dbsql.CreateInDbParameter("@hashType",DbType.Int32,thetype),
                    dbsql.CreateInDbParameter("@recvTime", DbType.DateTime,thehash.recvTime),
                    dbsql.CreateInDbParameter("@filename", DbType.String,thehash.filename),
                    dbsql.CreateInDbParameter("@filesize", DbType.Double,thehash.filesize)
                    };
                    return dbsql.ExecuteNonQuery(CommandType.Text, strSql.ToString(), cmdParms);
                }
            }
            catch (System.Exception ex)
            {
                H31Debug.PrintLn(ex.StackTrace);
            }
            return -1;
        }
        /// <summary>
        /// 增加一条HASH纪录的日志文件
        /// </summary>
        public static int AddNewHashLOG(HASHITEM thehash, int thedetailID)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                {
                    ////添加工作计划
                    //string tempstr = string.Format("insert into H31_DHT_TYPE{0} (", thedetailID);
                    //strSql.Append(tempstr);
                    //strSql.Append("hashKey,recvTime,updateTime,keyContent,KeyWords,keyType,recvTimes,fileCnt,filetotalSize,Detail)");
                    //strSql.Append(" values (");
                    //strSql.Append("@hashKey,@recvTime,@updateTime,@keyContent,@KeyWords,@keyType,@recvTimes,@fileCnt,@filetotalSize,@Detail)");
                    //DbParameter[] cmdParms = {
                    //dbsql.CreateInDbParameter("@hashKey",DbType.String,40,thehash.hashKey),
                    //dbsql.CreateInDbParameter("@recvTime", DbType.DateTime,thehash.recvTime),
                    //dbsql.CreateInDbParameter("@updateTime", DbType.DateTime,thehash.recvTime),
                    //dbsql.CreateInDbParameter("@keyContent", DbType.String,thehash.keyContent),
                    //dbsql.CreateInDbParameter("@KeyWords", DbType.String,thehash.keyWords),
                    //dbsql.CreateInDbParameter("@keyType", DbType.Int32,thehash.keyType),
                    //dbsql.CreateInDbParameter("@recvTimes", DbType.Int32,thehash.recvTimes),
                    //dbsql.CreateInDbParameter("@fileCnt", DbType.Int32,thehash.fileCnt),
                    //dbsql.CreateInDbParameter("@filetotalSize", DbType.Int32,thehash.filetotalSize),
                    //dbsql.CreateInDbParameter("@Detail", DbType.Int32,thedetailID)
                    //};
                    //return dbsql.ExecuteNonQuery(CommandType.Text, strSql.ToString(), cmdParms);
                }
            }
            catch (System.Exception ex)
            {
                H31Debug.PrintLn(ex.StackTrace);
            }
            return -1;
        }

        /// <summary>
        /// 取得分页表项目
        /// </summary>
        public static DataSet GetHashPageListFromDB(int thetype,int isHanzhi, string keywords,int orderbytype,int pageindex,int pageCount)
        {
            string tempstr1 = string.Format("H31_DHT_TYPE_{0}_{1}", (int)thetype * 100 + 1, isHanzhi);
            string tempstr2 = "";
            if(keywords.Length>0)
                tempstr2 = string.Format(" keyContent like '%{0}%'", keywords);

            string tempstr3 = "ID";
            if (orderbytype == 2)
                tempstr3 = "recvTimes";
            else if (orderbytype == 2)
                tempstr3 = "updateTime";

            using (DataSet dr = dbsql.PageList(tempstr1, pageCount, pageindex, tempstr3, true, tempstr2))
            {
                return dr;
            }
        }

         ///<summary>
         ///取得种子文件里面的信息
         ///</summary>
        public static DataSet GetHashFileDetail(int tableid,int hashid)
        {
            try
            {
                string strSql = string.Format("select * from H31_DHT_DETAIL_{0} where hashID={1} order by id", tableid, hashid);
                DataSet ds = dbsql.ExecuteQuery(CommandType.Text, strSql.ToString(), null);
                return ds;
            }
            catch (System.Exception ex)
            {
                H31Debug.PrintLn(ex.StackTrace);
            }
            return null;
        }

        ///<summary>
        ///取得种子文表的总条数目
        ///</summary>
        public static int GetHashFileTableNumberCount(int tableid)
        {
            try
            {
                string strSql = string.Format("select count(ID) from H31_DHT_DETAIL_{0}", tableid);
                object totalNum = dbsql.ExecuteScalar(CommandType.Text, strSql.ToString(), null);
                if (totalNum == null)
                    return 0;
                if (dbsql.GetInt(totalNum) > 0)
                    return dbsql.GetInt(totalNum);
                return 0;
            }
            catch (System.Exception ex)
            {
                H31Debug.PrintLn(ex.StackTrace);
            }
            return 0;
        }

        #endregion

    }
}
