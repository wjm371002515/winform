using System;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Reflection;

using MySql.Data.MySqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using JCodes.Framework.Entity;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.Common.Databases;
using JCodes.Framework.Common.Files;
using System.Configuration;

namespace JCodes.Framework.Common.Framework.BaseDAL
{
    /// <summary>
    /// MySql数据访问层的基类
    /// </summary>
    public abstract class BaseDALMySql<T> : AbstractBaseDAL<T>, IBaseDAL<T> where T : BaseEntity, new()
    {
        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public BaseDALMySql() { }

        /// <summary>
        /// 指定表名以及主键,对基类进构造
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="primaryKey">表主键</param>
        public BaseDALMySql(string tableName, string primaryKey)
            : base(tableName, primaryKey)
        {
            this.parameterPrefix = "?";// 数据库参数化访问的占位符
            this.safeFieldFormat = "{0}"; //防止和保留字、关键字同名的字段格式(尽量避免）
        }

        #endregion

        #region 通用操作方法

        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="recordField">Hashtable:键[key]为字段名;值[value]为字段对应的值</param>
        /// <param name="targetTable">需要操作的目标表名称</param>
        /// <param name="trans">事务对象,如果使用事务,传入事务对象,否则为Null不使用事务</param>
        public override int Insert2(Hashtable recordField, string targetTable, DbTransaction trans)
        {
            int result = -1;
            if (recordField == null || recordField.Count < 1)
            {
                return result;
            }

            string fields = ""; // 字段名
            string vals = ""; // 字段值
            foreach (string field in recordField.Keys)
            {
                fields += string.Format("[{0}],", field);//加[]为了去除别名引起的错误
                vals += string.Format("{0}{1},", parameterPrefix, field);
            }
            fields = fields.Trim(',');//除去前后的逗号
            vals = vals.Trim(',');//除去前后的逗号
            string sql = string.Format("INSERT INTO {0} ({1}) VALUES ({2});Select LAST_INSERT_ID()", targetTable, fields, vals);

            Database db = CreateDatabase();
            DbCommand command = db.GetSqlStringCommand(sql);
            foreach (string field in recordField.Keys)
            {
                object val = recordField[field];
                val = val ?? DBNull.Value;
                if (val is DateTime)
                {
                    if (Convert.ToDateTime(val) <= Convert.ToDateTime("1753-1-1"))
                    {
                        val = DBNull.Value;
                    }
                }

                db.AddInParameter(command, field, TypeToDbType(val.GetType()), val);
            }

            if (trans != null)
            {
                result = Convert.ToInt32(db.ExecuteScalar(command, trans).ToString());
            }
            else
            {
                result = Convert.ToInt32(db.ExecuteScalar(command).ToString());
            }

            return result;
        }

        /// <summary>
        /// 测试数据库是否正常连接
        /// </summary>
        public override bool TestConnection(string connectionString)
        {
            bool result = false;

            using (DbConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    result = true;
                }
            }

            return result;
        }

        public override bool TestConnection()
        {
            return TestConnection(ConfigurationManager.ConnectionStrings["mysql"].ToString());
        }  

        #endregion

        #region 对象添加、修改、查询接口

        /// <summary>
        /// 查找记录表中最旧的一条记录
        /// </summary>
        /// <param name="trans">事务对象</param>
        /// <returns></returns>
        public override T FindFirst(DbTransaction trans = null)
        {
            string sql = string.Format("Select {0} From {1} Order by {2} ASC LIMIT 1", selectedFields, tableName, GetSafeFileName(sortField));

            Database db = CreateDatabase();
            DbCommand command = db.GetSqlStringCommand(sql);

            T entity = GetEntity(db, command, trans);
            return entity;
        }

        /// <summary>
        /// 查找记录表中最新的一条记录
        /// </summary>
        /// <param name="trans">事务对象</param>
        /// <returns></returns>
        public override T FindLast(DbTransaction trans = null)
        {
            string sql = string.Format("Select {0} From {1} Order by {2} DESC LIMIT 1", selectedFields, tableName, GetSafeFileName(sortField));

            Database db = CreateDatabase();
            DbCommand command = db.GetSqlStringCommand(sql);

            T entity = GetEntity(db, command, trans);
            return entity;
        }

        #region 下面两个覆盖基类函数，指定具体的数据库类型
        /// <summary>
        /// 根据条件查询数据库,并返回对象集合(用于分页数据显示)
        /// </summary>
        /// <param name="condition">查询的条件</param>
        /// <param name="info">分页实体</param>
        /// <param name="fieldToSort">排序字段</param>
        /// <param name="desc">是否降序</param>
        /// <param name="trans">事务对象</param>
        /// <returns>指定对象的集合</returns>
        public override List<T> FindWithPager(string condition, PagerInfo info, string fieldToSort, bool desc, DbTransaction trans = null)
        {

            PagerHelper helper = new PagerHelper(tableName, this.selectedFields, fieldToSort,
                info.PageSize, info.CurrenetPageIndex, desc, condition);

            string countSql = helper.GetPagingSql(DatabaseType.MySql, true);
            string strCount = SqlValueList(countSql, trans);
            info.RecordCount = Convert.ToInt32(strCount);

            string dataSql = helper.GetPagingSql(DatabaseType.MySql, false);
            List<T> list = GetList(dataSql, null, trans);
            return list;
        }

        /// <summary>
        /// 根据条件查询数据库,并返回DataTable集合(用于分页数据显示)
        /// </summary>
        /// <param name="condition">查询的条件</param>
        /// <param name="info">分页实体</param>
        /// <param name="fieldToSort">排序字段</param>
        /// <param name="desc">是否降序</param>
        /// <param name="trans">事务对象</param>
        /// <returns>指定DataTable的集合</returns>
        public override DataTable FindToDataTable(string condition, PagerInfo info, string fieldToSort, bool desc, DbTransaction trans = null)
        {
            PagerHelper helper = new PagerHelper(tableName, this.selectedFields, fieldToSort,
                info.PageSize, info.CurrenetPageIndex, desc, condition);

            string countSql = helper.GetPagingSql(DatabaseType.MySql, true);
            string strCount = SqlValueList(countSql, trans);
            info.RecordCount = Convert.ToInt32(strCount);

            string dataSql = helper.GetPagingSql(DatabaseType.MySql, false);
            return GetDataTableBySql(dataSql, trans);
        }

        /// <summary>
        /// 获取前面记录指定数量的记录
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="count">指定数量</param>
        /// <param name="orderBy">排序条件，例如order by id</param>
        /// <param name="trans">事务对象</param>
        /// <returns></returns>
        public override DataTable GetTopResult(string sql, int count, string orderBy, DbTransaction trans = null)
        {
            string resultSql = string.Format("select * from ({1} {2})  LIMIT {0} ", count, sql, orderBy);
            return SqlTable(resultSql, trans);
        }

        #endregion

        #endregion

        #region 特殊的操作

        /// <summary>
        /// 兼容Oracle的字段大写的重写函数
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="trans">事务对象</param>
        /// <returns></returns>
        public override DataTable SqlTable(string sql, DbTransaction trans = null)
        {
            //由于Sqlite字段查询不区分大小写，因此要返回大写字段，不能通过改变Sql大写方式
            //通过代码改变列的名称为大写即可
            DataTable dt = base.SqlTable(sql, trans);
            foreach (DataColumn col in dt.Columns)
            {
                col.ColumnName = col.ColumnName.ToUpper();
            }
            return dt;
        }

        /// <summary>
        /// 兼容Oracle的字段大写的重写函数
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">SQL参数集合</param>
        /// <param name="trans">事务对象</param>
        /// <returns></returns>
        public override DataTable SqlTable(string sql, DbParameter[] parameters, DbTransaction trans = null)
        {
            //由于Sqlite字段查询不区分大小写，因此要返回大写字段，不能通过改变Sql大写方式
            //通过代码改变列的名称为大写即可
            DataTable dt = base.SqlTable(sql, parameters, trans);
            foreach (DataColumn col in dt.Columns)
            {
                col.ColumnName = col.ColumnName.ToUpper();
            }
            return dt;
        }

        /// <summary>
        /// 获取数据库的全部表名称
        /// </summary>
        /// <returns></returns>
        public override List<string> GetTableNames()
        {
            string sql = string.Format("show tables;");
            Database db = CreateDatabase();
            DbCommand command = db.GetSqlStringCommand(sql);

            List<string> list = new List<string>();
            using (IDataReader dr = db.ExecuteReader(command))
            {
                while (dr.Read())
                {
                    list.Add(dr[0].ToString());
                }
            }
            return list;
        }

        #endregion
    }
}