using System;
using System.Collections.Generic;
using System.Text;

namespace H31SQLLibrary
{
    /// <summary>
    /// 数据库操作基类(只包含分页方法,供其它数据库操作基类继承)
    /// </summary>   
    internal class H31DBPage
    {
        /// <summary>
        /// 获取分页SQL
        /// </summary>
        /// <param name="strCondition">条件</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">第几页</param>
        /// <param name="fldSort">排序字段（最后一个不需要填写正序还是倒序，例如：id asc, name）</param>
        /// <param name="tblName">表名</param>
        /// <param name="sort">最后一个排序字段的正序或倒序（true为倒序，false为正序）</param>
        /// <returns>返回用于分页的SQL语句</returns>
        protected string GetPagerSQL(string condition, int pageSize, int pageIndex, string fldSort,string tblName, bool sort)
        {
            string strSort = sort ? " DESC" : " ASC";

            //if (pageIndex == 1)
            //{
            //    return "select top " + pageSize.ToString() + " * from " + tblName.ToString()
            //        + ((string.IsNullOrEmpty(condition)) ? string.Empty : (" where " + condition))
            //        + " order by " + fldSort.ToString() + strSort;
            //}
            //else
            {
                StringBuilder strSql = new StringBuilder();
                strSql.AppendFormat("select top {0} * from", pageSize);
                strSql.AppendFormat(" (select top {0} {1},* from {2} ", pageSize * (pageIndex),
                    (fldSort.Substring(fldSort.LastIndexOf(',') + 1, fldSort.Length - fldSort.LastIndexOf(',') - 1)), tblName);
                if (!string.IsNullOrEmpty(condition))
                {
                    strSql.AppendFormat(" where {0} order by {1}{2})", condition, fldSort, strSort);
                }
                else
                {
                    strSql.AppendFormat(" order by {0}{1}) ", fldSort, strSort);
                }
                strSql.AppendFormat(" order by {0} ASC", fldSort);
                return strSql.ToString();
            }
        }
    }
}
