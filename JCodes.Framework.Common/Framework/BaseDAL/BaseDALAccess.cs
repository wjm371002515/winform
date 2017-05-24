using System;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Data.Common;
using System.Collections.Generic;
using System.Reflection;
using System.Configuration;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Configuration;
using JCodes.Framework.Entity;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.Common.Encrypt;
using JCodes.Framework.Common.Databases;
using JCodes.Framework.Common.Format;

namespace JCodes.Framework.Common.Framework.BaseDAL
{
    /// <summary>
    /// 数据访问层的基类
    /// </summary>
    public abstract class BaseDALAccess<T> : AbstractBaseDAL<T>, IBaseDAL<T> where T : BaseEntity, new()
    {
        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public BaseDALAccess() { }

        /// <summary>
        /// 指定表名以及主键,对基类进构造
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="primaryKey">表主键</param>
        public BaseDALAccess(string tableName, string primaryKey)
            : base(tableName, primaryKey)
        {
            parameterPrefix = "@";     //数据库参数化访问的占位符
            safeFieldFormat = "[{0}]"; //防止和保留字、关键字同名的字段格式，如[value]
        } 
        #endregion

        #region 通用操作方法

        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="recordField">Hashtable:键[key]为字段名;值[value]为字段对应的值</param>
        /// <param name="targetTable">需要操作的目标表名称</param>
        /// <param name="trans">事务对象,如果使用事务,传入事务对象,否则为Null不使用事务</param>
        public override bool Insert(Hashtable recordField, string targetTable, DbTransaction trans)
        {
            bool result = false;
            string fields = ""; // 字段名
            string vals = ""; // 字段值
            if (recordField == null || recordField.Count < 1)
            {
                return result;
            }

            OleDbParameter[] param = new OleDbParameter[recordField.Count];
            IEnumerator eKeys = recordField.Keys.GetEnumerator();

            int i = 0;
            while (eKeys.MoveNext())
            {
                string field = eKeys.Current.ToString();
                fields += string.Format("[{0}],", field);//加[]为了去除别名引起的错误
                vals += string.Format("@{0},", field);

                object val = recordField[eKeys.Current.ToString()];
                val = val ?? DBNull.Value;
                if (val is DateTime)
                {
                    if (Convert.ToDateTime(val) <= Convert.ToDateTime("1753-1-1"))
                    {
                        val = DBNull.Value;
                    }
                }

                param[i] = new OleDbParameter("@" + field, val);
                if (val is DateTime)
                {
                    param[i].OleDbType = OleDbType.Date;//日期类型特别处理，否则Access数据库访问出错
                }

                i++;
            }

            fields = fields.Trim(',');//除去前后的逗号
            vals = vals.Trim(',');//除去前后的逗号
            string sql = string.Format("INSERT INTO {0} ({1}) VALUES ({2})", targetTable, fields, vals);

            Database db = CreateDatabase();
            DbCommand command = db.GetSqlStringCommand(sql);
            command.Parameters.AddRange(param);

            if (trans != null)
            {
                result = db.ExecuteNonQuery(command, trans) > 0;
            }
            else
            {
                result = db.ExecuteNonQuery(command) > 0;
            }

            return result;
        }

        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="recordField">Hashtable:键[key]为字段名;值[value]为字段对应的值</param>
        /// <param name="targetTable">需要操作的目标表名称</param>
        /// <param name="trans">事务对象,如果使用事务,传入事务对象,否则为Null不使用事务</param>
        public override int Insert2(Hashtable recordField, string targetTable, DbTransaction trans)
        {
            int result = -1;
            string fields = ""; // 字段名
            string vals = ""; // 字段值
            if (recordField == null || recordField.Count < 1)
            {
                return result;
            }

            OleDbParameter[] param = new OleDbParameter[recordField.Count];
            IEnumerator eKeys = recordField.Keys.GetEnumerator();

            int i = 0;
            while (eKeys.MoveNext())
            {
                string field = eKeys.Current.ToString();
                fields += string.Format("[{0}],", field);//加[]为了去除别名引起的错误
                vals += string.Format("@{0},", field);

                object val = recordField[eKeys.Current.ToString()];
                val = val ?? DBNull.Value;
                if (val is DateTime)
                {
                    if (Convert.ToDateTime(val) <= Convert.ToDateTime("1753-1-1"))
                    {
                        val = DBNull.Value;
                    }
                }

                param[i] = new OleDbParameter("@" + field, val);
                if (val is DateTime)
                {
                    param[i].OleDbType = OleDbType.Date;//日期类型特别处理，否则Access数据库访问出错
                }

                i++;
            }

            fields = fields.Trim(',');//除去前后的逗号
            vals = vals.Trim(',');//除去前后的逗号
            string sql = string.Format("INSERT INTO {0} ({1}) VALUES ({2});SELECT @@IDENTITY", targetTable, fields, vals);//SCOPE_IDENTITY()

            Database db = CreateDatabase();
            DbCommand command = db.GetSqlStringCommand(sql);
            command.Parameters.AddRange(param);

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
        /// 更新某个表一条记录
        /// </summary>
        /// <param name="id">ID值</param>
        /// <param name="recordField">Hashtable:键[key]为字段名;值[value]为字段对应的值</param>
        /// <param name="targetTable">需要操作的目标表名称</param>
        /// <param name="trans">事务对象,如果使用事务,传入事务对象,否则为Null不使用事务</param>
        public override bool PrivateUpdate(object id, Hashtable recordField, string targetTable, DbTransaction trans)
        {
            try
            {
                if (recordField == null || recordField.Count < 1)
                {
                    return false;
                }

                string setValue = "";
                foreach (string field in recordField.Keys)
                {
                    setValue += string.Format("{0} = {1}{2},", GetSafeFileName(field), parameterPrefix, field);
                }
                string sql = string.Format("UPDATE {0} SET {1} WHERE {2} = {3}{2} ",
                    targetTable, setValue.Substring(0, setValue.Length - 1), primaryKey, parameterPrefix);
                Database db = CreateDatabase();
                DbCommand command = db.GetSqlStringCommand(sql);

                bool foundID = false;
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
                        db.AddInParameter(command, field, DbType.Date, val);//Access日期必须使用DbType.Date，如果使用 DbType.DateTime会出现类型不匹配错误
                    }
                    else
                    {
                        db.AddInParameter(command, field, TypeToDbType(val.GetType()), val);
                    }

                    if (field.Equals(primaryKey, StringComparison.OrdinalIgnoreCase))
                    {
                        foundID = true;
                    }
                }

                if (!foundID)
                {
                    db.AddInParameter(command, primaryKey, TypeToDbType(id.GetType()), id);
                }

                bool result = false;
                if (trans != null)
                {
                    result = db.ExecuteNonQuery(command, trans) > 0;
                }
                else
                {
                    result = db.ExecuteNonQuery(command) > 0;
                }

                return result;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(BaseDALAccess<T>));
                throw;
            }
        }

        /// <summary>
        /// 更新某个表一条记录
        /// </summary>
        /// <param name="id">ID值</param>
        /// <param name="recordField">Hashtable:键[key]为字段名;值[value]为字段对应的值</param>
        /// <param name="targetTable">需要操作的目标表名称</param>
        /// <param name="trans">事务对象,如果使用事务,传入事务对象,否则为Null不使用事务</param>
        public bool PrivateUpdate2(object id, Hashtable recordField, string targetTable, DbTransaction trans)
        {

            string field = ""; // 字段名
            object val = null; // 值
            string setValue = ""; // 更新Set () 中的语句

            if (recordField == null || recordField.Count < 1)
            {
                return false;
            }

            OleDbParameter[] param = new OleDbParameter[recordField.Count];
            int i = 0;

            IEnumerator eKeys = recordField.Keys.GetEnumerator();
            while (eKeys.MoveNext())
            {
                field = eKeys.Current.ToString();
                val = recordField[eKeys.Current.ToString()];
                val = val ?? DBNull.Value;
                if (val is DateTime)
                {
                    if (Convert.ToDateTime(val) <= Convert.ToDateTime("1753-1-1"))
                    {
                        val = DBNull.Value;
                    }
                }

                setValue += string.Format("[{0}] = @{0},", field);//加[ ]用来避免关键字错误
                param[i] = new OleDbParameter(string.Format("@{0}", field), val);

                if (val is DateTime)
                {
                    param[i].OleDbType = OleDbType.Date;//日期类型特别处理，否则Access数据库访问出错
                }

                i++;
            }

            string sql = "";
            //为了避免整形ID在更新语句Where ID ='1'出现“标准表达式中数据类型不匹配”错误
            //因此设置不同类型，更新语句不同的条件语句。
            if (id.GetType() == typeof(int) || ValidateUtil.IsNumber(id.ToString()))
            {
                sql = string.Format("UPDATE {0} SET {1} WHERE {2} = {3} ", targetTable, setValue.Substring(0, setValue.Length - 1), primaryKey, id);
            }
            else
                sql = string.Format("UPDATE {0} SET {1} WHERE {2} = '{3}' ", targetTable, setValue.Substring(0, setValue.Length - 1), primaryKey, id);
            {
            }

            Database db = CreateDatabase();
            DbCommand command = db.GetSqlStringCommand(sql);
            command.Parameters.AddRange(param);

            bool result = false;
            if (trans != null)
            {
                result = db.ExecuteNonQuery(command, trans) > 0;
            }
            else
            {
                result = db.ExecuteNonQuery(command) > 0;
            }

            return result;
        }

        /// <summary>
        /// 测试数据库是否正常连接
        /// </summary>
        public override bool TestConnection(string connectionString)
        {
            bool result = false;

            using (DbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    result = true;
                }
            }

            return result;
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
            string sql = string.Format("Select top 1 {0} From {1} Order by {2} ASC", selectedFields, tableName, GetSafeFileName(sortField));

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
            string sql = string.Format("Select top 1 {0} From {1} Order by {2} DESC", selectedFields, tableName, GetSafeFileName(sortField));

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
            if (HasInjectionData(condition))
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_ERR, string.Format("检测出SQL注入的恶意数据, {0}", condition), typeof(BaseDALAccess<T>));
                throw new Exception("检测出SQL注入的恶意数据");
            }


            PagerHelper helper = new PagerHelper(tableName, this.selectedFields, fieldToSort,
                info.PageSize, info.CurrenetPageIndex, desc, condition);

            string countSql = helper.GetPagingSql(DatabaseType.Access, true);
            string strCount = SqlValueList(countSql, trans);
            info.RecordCount = Convert.ToInt32(strCount);

            string dataSql = helper.GetPagingSql(DatabaseType.Access, false);
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
            if (HasInjectionData(condition))
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_ERR, string.Format("检测出SQL注入的恶意数据, {0}", condition), typeof(BaseDALAccess<T>));
                throw new Exception("检测出SQL注入的恶意数据");
            }

            PagerHelper helper = new PagerHelper(tableName, this.selectedFields, fieldToSort,
                info.PageSize, info.CurrenetPageIndex, desc, condition);

            string countSql = helper.GetPagingSql(DatabaseType.Access, true);
            string strCount = SqlValueList(countSql, trans);
            info.RecordCount = Convert.ToInt32(strCount);

            string dataSql = helper.GetPagingSql(DatabaseType.Access, false);
            return GetDataTableBySql(dataSql, trans);
        }


        #endregion

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
            string resultSql = string.Format("select top {0} * from ({1} {2}) ", count, sql, orderBy);
            return SqlTable(resultSql, trans);
        }

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
            Database db = CreateDatabase();
            List<string> list = new List<string>();

            using (DbConnection connection = db.CreateConnection())
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                DataTable schemaTable = connection.GetSchema("TABLES");
                for (int i = 0; i < schemaTable.Rows.Count; i++)
                {
                    string tablename = schemaTable.Rows[i]["TABLE_NAME"].ToString();
                    list.Add(tablename);
                }
            }
            return list;
        }

        #endregion

        #region 设置数据库的密码

        /// <summary>
        /// 根据配置数据库配置名称生成Database对象
        /// </summary>
        /// <returns></returns>
        protected override Database CreateDatabase()
        {
            Database db = null;
            if (string.IsNullOrEmpty(dbConfigName))
            {
                db = DatabaseFactory.CreateDatabase();
            }
            else
            {
                db = DatabaseFactory.CreateDatabase(dbConfigName);
            }

            DbConnectionStringBuilder sb = db.DbProviderFactory.CreateConnectionStringBuilder();
            sb.ConnectionString = GetConnectionString(dbConfigName);
            GenericDatabase newDb = new GenericDatabase(sb.ToString(), db.DbProviderFactory);
            db = newDb;

            return db;
        }

        /// <summary>
        /// 动态改变或者连接字符串
        /// </summary>
        /// <returns></returns>
        protected virtual string GetConnectionString(string dbConfigName)
        {
            string connectionString = "";
            DatabaseSettings setting = ConfigurationManager.GetSection("dataConfiguration") as DatabaseSettings;
            if (setting != null)
            {
                string connection = string.IsNullOrEmpty(dbConfigName) ? setting.DefaultDatabase : dbConfigName;
                connectionString = ConfigurationManager.ConnectionStrings[connection].ConnectionString;

                #region 加密解密操作

                //使用自定义加密
                //if (!connectionString.EndsWith(";"))
                //{
                //    connectionString += ";";
                //}
                //connectionString += string.Format("Jet OLEDB:Database Password=wuhuacong2013;");

                string passwordKey = "Jet OLEDB:Database Password";
                string password = GetSubValue(connectionString, passwordKey);
                if (!string.IsNullOrEmpty(password))
                {
                    //尝试使用AES解密
                    string decryptStr = password;
                    try
                    {
                        decryptStr = EncodeHelper.AES_Decrypt(password);
                    }
                    catch (Exception ex)
                    {
                        decryptStr = password;
                        LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(BaseDALAccess<T>));
                    }

                    connectionString += string.Format(";{0}={1};", passwordKey, decryptStr);
                }
                #endregion
            }

            return connectionString;
        }

        #endregion 

    }
}