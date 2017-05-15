using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;

using JCodes.Framework.Entity;

namespace JCodes.Framework.Common
{
    /// <summary>
    /// 业务基类对象
    /// </summary>
    /// <typeparam name="T">业务对象类型</typeparam>
    public class BaseBLL<T> where T : BaseEntity, new()
    {
        #region 构造函数
        /// <summary>
        /// DAL层名字
        /// </summary>
        private string dalName = "";   

        /// <summary>
        /// BLL业务类的全名（子类必须实现），可使用this.GetType().FullName
        /// </summary>
        protected string bllFullName;

        /// <summary>
        /// 数据访问层程序集的清单文件的文件名，不包括其扩展名，可使用Assembly.GetExecutingAssembly().GetName().Name
        /// </summary>
        protected string dalAssemblyName;

        /// <summary>
        /// BLL命名空间的前缀（BLL.)
        /// </summary>
        protected string bllPrefix = "BLL.";

        /// <summary>
        /// 基础数据访问层接口对象
        /// </summary>
        protected IBaseDAL<T> baseDal = null;

        /// <summary>
        /// 默认构造函数，调用后需手动调用一次 Init() 方法进行对象初始化
        /// </summary>
        public BaseBLL() { }
              
        /// <summary>
        /// 参数赋值后，初始化相关对象
        /// </summary>
        /// <param name="bllFullName">BLL业务类的全名（子类必须实现）,子类构造函数传入this.GetType().FullName</param>
        /// <param name="dalAssemblyName">数据访问层程序集的清单文件的文件名，不包括其扩展名。设置为NULL或默认为Assembly.GetExecutingAssembly().GetName().Name</param>
        /// <param name="bllPrefix">BLL命名空间的前缀（BLL.)</param>
        /// <param name="dbConfigName">数据库配置项名称</param>
        protected void Init(string bllFullName, string dalAssemblyName = null, string dbConfigName = null, string bllPrefix = "BLL.")
        {
            if (string.IsNullOrEmpty(bllFullName))
                throw new ArgumentNullException("子类未设置bllFullName业务类全名！");

            if (string.IsNullOrEmpty(dalAssemblyName))
            {
                dalAssemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            }

            //赋值，准备构建对象
            this.bllFullName = bllFullName;
            //this.dalAssemblyName = dalAssemblyName;
            this.bllPrefix = bllPrefix;

            #region 根据不同的数据库类型，构造相应的DAL层
            AppConfig config = new AppConfig();
            string dbType = config.AppConfigGet("ComponentDbType");
            if (string.IsNullOrEmpty(dbType))
            {
                dbType = "sqlserver";
            }
            dbType = dbType.ToLower();

            string DALPrefix = "";
            if (dbType == "sqlserver")
            {
                DALPrefix = "SQLServerDAL.";
            }
            else if (dbType == "access")
            {
                DALPrefix = "AccessDAL.";
            }
            else if (dbType == "oracle")
            {
                DALPrefix = "OracleDAL.";
            }
            else if (dbType == "sqlite")
            {
                DALPrefix = "SQLiteDAL.";
            }
            else if (dbType == "mysql")
            {
                DALPrefix = "MySqlDAL.";
            }
            #endregion

            this.dalName = bllFullName.Replace(bllPrefix, DALPrefix);//替换中级的BLL.为DAL.，就是DAL类的全名
            this.dalAssemblyName = dalAssemblyName.Replace(bllPrefix.Replace(".", ""), DALPrefix.Replace(".", ""));
            //baseDal = Reflect<IBaseDAL<T>>.Create(this.dalName, dalAssemblyName);//构造对应的DAL数据访问层的对象类
            // CREATE 命名空间， 程序集名（JCodes.Framework.SQLServerDAL.DictType， JCodes.Framework.SQLServerDAL）
            baseDal = Reflect<IBaseDAL<T>>.Create(this.dalName, this.dalAssemblyName);//构造对应的DAL数据访问层的对象类

            if (!string.IsNullOrEmpty(dbConfigName))
            {
                baseDal.SetDbConfigName(dbConfigName); //设置数据库配置项名称
            }
        }

        /// <summary>
        /// 调用前检查baseDal是否为空引用
        /// </summary>
        protected void CheckDAL()
        {
            if (baseDal == null)
            {
                throw new ArgumentNullException("baseDal", "未能成功创建对应的DAL对象，请在BLL业务类构造函数中调用base.Init(**,**)方法，如base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);");
            }
        }

        #endregion

        #region 对象添加、修改、查询接口

        /// <summary>
        /// 插入指定对象到数据库中
        /// </summary>
        /// <param name="obj">指定的对象</param>
        /// <param name="trans">事务对象</param>
        /// <returns>执行操作是否成功。</returns>
        public virtual bool Insert(T obj, DbTransaction trans = null)
        {
            CheckDAL();
            return baseDal.Insert(obj, trans);
        }

        /// <summary>
        /// 插入指定对象到数据库中
        /// </summary>
        /// <param name="obj">指定的对象</param>
        /// <param name="trans">事务对象</param>
        /// <returns>执行成功返回新增记录的自增长ID。</returns>
        public virtual int Insert2(T obj, DbTransaction trans = null)
        {
            CheckDAL();
            return baseDal.Insert2(obj, trans);
        }

        /// <summary>
        /// 更新对象属性到数据库中
        /// </summary>
        /// <param name="obj">指定的对象</param>
        /// <param name="primaryKeyValue">主键的值</param>
        /// <param name="trans">事务对象</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        public virtual bool Update(T obj, object primaryKeyValue, DbTransaction trans = null)
        {
            CheckDAL();
            return baseDal.Update(obj, primaryKeyValue, trans);
        }

        /// <summary>
        /// 更新数据到数据表中
        /// </summary>
        /// <param name="commandType">SQL类型</param>
        /// <param name="sql">sql或者存储过程</param>
        /// <param name="trans">事务对象</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        public virtual bool Update(CommandType commandType, string sql, DbTransaction trans = null)
        {
            CheckDAL();
            return baseDal.Update(commandType, sql, trans);
        }

        /// <summary>
        /// 更新某个表一条记录(只适用于用单键,用string类型作键值的表)
        /// </summary>
        /// <param name="id">ID号</param>
        /// <param name="recordField">Hashtable:键[key]为字段名;值[value]为字段对应的值</param>
        /// <param name="targetTable">需要操作的目标表名称</param>
        /// <param name="trans">事务对象,如果使用事务,传入事务对象,否则为Null不使用事务</param>
        public virtual bool Update(object id, Hashtable recordField, string targetTable, DbTransaction trans)
        {
            CheckDAL();
            return baseDal.Update(id, recordField, targetTable, trans);
        }

        /// <summary>
        /// 插入或更新对象属性到数据库中
        /// </summary>
        /// <param name="obj">指定的对象</param>
        /// <param name="primaryKeyValue">主键的值</param>
        /// <param name="trans">事务对象</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        public virtual bool InsertUpdate(T obj, object primaryKeyValue, DbTransaction trans = null)
        {
            CheckDAL();
            return baseDal.InsertUpdate(obj, primaryKeyValue, trans);
        }

        /// <summary>
        /// 如果不存在记录，则插入对象属性到数据库中
        /// </summary>
        /// <param name="obj">指定的对象</param>
        /// <param name="primaryKeyValue">主键的值</param>
        /// <param name="trans">事务对象</param>
        /// <returns>执行插入成功返回<c>true</c>，否则为<c>false</c>。</returns>
        public virtual bool InsertIfNew(T obj, object primaryKeyValue, DbTransaction trans = null)
        {
            CheckDAL();
            return baseDal.InsertIfNew(obj, primaryKeyValue, trans);
        }

        /// <summary>
        /// 执行SQL查询语句，返回查询结果的所有记录的第一个字段,用逗号分隔。
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="trans">事务对象</param>
        /// <returns>
        /// 返回查询结果的所有记录的第一个字段,用逗号分隔。
        /// </returns>
        public virtual string SqlValueList(string sql, DbTransaction trans = null)
        {
            CheckDAL();
            return baseDal.SqlValueList(sql, trans);
        }

        /// <summary>
        /// 执行SQL查询语句，返回所有记录的DataTable集合。
        /// </summary>
        /// <param name="sql">SQL查询语句</param>
        /// <param name="trans">事务对象</param>
        /// <returns></returns>
        public virtual DataTable SqlTable(string sql, DbTransaction trans = null)
        {
            CheckDAL();
            return baseDal.SqlTable(sql, trans);
        }
        
        /// <summary>
        /// 执行SQL查询语句，返回所有记录的DataTable集合。
        /// </summary>
        /// <param name="sql">SQL查询语句</param>
        /// <param name="parameters">参数集合</param>
        /// <param name="trans">事务对象</param>
        /// <returns></returns>
        public virtual DataTable SqlTable(string sql, DbParameter[] parameters, DbTransaction trans = null)
        {
            CheckDAL();
            return baseDal.SqlTable(sql, parameters, trans);
        }

        /// <summary>
		/// 查询数据库,检查是否存在指定ID的对象
		/// </summary>
		/// <param name="key">对象的ID值</param>
        /// <param name="trans">事务对象</param>
		/// <returns>存在则返回指定的对象,否则返回Null</returns>
        public virtual T FindByID(object key, DbTransaction trans = null)
        {
            CheckDAL();
            return baseDal.FindByID(key, trans);
        }
                       
        /// <summary>
        /// 根据条件查询数据库,如果存在返回第一个对象
        /// </summary>
        /// <param name="condition">查询的条件</param>
        /// <param name="trans">事务对象</param>
        /// <returns>指定的对象</returns>
        public virtual T FindSingle(string condition, DbTransaction trans = null)
        {
            CheckDAL();
            return baseDal.FindSingle(condition, trans);
        }

        /// <summary>
        /// 根据条件查询数据库,如果存在返回第一个对象
        /// </summary>
        /// <param name="condition">查询的条件</param>
        /// <param name="orderBy">排序条件</param>
        /// <param name="trans">事务对象</param>
        /// <returns>指定的对象</returns>
        public virtual T FindSingle(string condition, string orderBy, DbTransaction trans = null)
        {
            CheckDAL();
            return baseDal.FindSingle(condition, orderBy, trans);
        }   
        
        /// <summary>
        /// 查找记录表中最旧的一条记录
        /// </summary>
        /// <param name="trans">事务对象</param>
        /// <returns></returns>
        public virtual T FindFirst(DbTransaction trans = null)
        {
            CheckDAL();
            return baseDal.FindFirst(trans);
        }

        /// <summary>
        /// 查找记录表中最新的一条记录
        /// </summary>
        /// <param name="trans">事务对象</param>
        /// <returns></returns>
        public virtual T FindLast(DbTransaction trans = null)
        {
            CheckDAL();
            return baseDal.FindLast(trans);
        }

        #endregion

        #region 返回集合的接口

        /// <summary>
        /// 根据ID字符串(逗号分隔)获取对象列表
        /// </summary>
        /// <param name="idString">ID字符串(逗号分隔)</param>
         /// <param name="trans">事务对象</param>
       /// <returns>符合条件的对象列表</returns>
        public virtual List<T> FindByIDs(string idString, DbTransaction trans = null)
        {
            CheckDAL();
            return baseDal.FindByIDs(idString, trans);
        }

        /// <summary>
        /// 根据条件查询数据库,并返回对象集合
        /// </summary>
        /// <param name="condition">查询的条件</param>
         /// <param name="trans">事务对象</param>
        /// <returns>指定对象的集合</returns>
        public virtual List<T> Find(string condition, DbTransaction trans = null)
        {
            CheckDAL();
            return baseDal.Find(condition, trans);
        }
        
        /// <summary>
        /// 根据条件查询数据库,并返回对象集合
        /// </summary>
        /// <param name="condition">查询的条件</param>
        /// <param name="orderBy">排序条件</param>
         /// <param name="trans">事务对象</param>
        /// <returns>指定对象的集合</returns>
        public virtual List<T> Find(string condition, string orderBy, DbTransaction trans = null)
        {
            CheckDAL();
            return baseDal.Find(condition, orderBy, trans);
        }

        /// <summary>
        /// 根据条件查询数据库,并返回对象集合(用于分页数据显示)
        /// </summary>
        /// <param name="condition">查询的条件</param>
        /// <param name="info">分页实体</param>
        /// <param name="trans">事务对象</param>
        /// <returns>指定对象的集合</returns>
        public virtual List<T> FindWithPager(string condition, PagerInfo info, DbTransaction trans = null)
        {
            CheckDAL();
            return baseDal.FindWithPager(condition, info,trans);
        }

        /// <summary>
		/// 根据条件查询数据库,并返回对象集合(用于分页数据显示)
		/// </summary>
		/// <param name="condition">查询的条件</param>
		/// <param name="info">分页实体</param>
        /// <param name="fieldToSort">排序字段</param>
        /// <param name="trans">事务对象</param>
        /// <returns>指定对象的集合</returns>
        public virtual List<T> FindWithPager(string condition, PagerInfo info, string fieldToSort, DbTransaction trans = null)
        {
            CheckDAL();
            return baseDal.FindWithPager(condition, info, fieldToSort, trans);
        }

        /// <summary>
		/// 根据条件查询数据库,并返回对象集合(用于分页数据显示)
		/// </summary>
		/// <param name="condition">查询的条件</param>
		/// <param name="info">分页实体</param>
        /// <param name="fieldToSort">排序字段</param>
        /// <param name="desc">是否降序</param>
        /// <param name="trans">事务对象</param>
        /// <returns>指定对象的集合</returns>
        public virtual List<T> FindWithPager(string condition, PagerInfo info, string fieldToSort, bool desc, DbTransaction trans = null)
        {
            CheckDAL();
            return baseDal.FindWithPager(condition, info, fieldToSort, desc, trans);
        }
        
        /// <summary>
        /// 返回数据库所有的对象集合
        /// </summary>
        /// <param name="trans">事务对象</param>
        /// <returns>指定对象的集合</returns>
        public virtual List<T> GetAll(DbTransaction trans = null)
        {
            CheckDAL();
            return baseDal.GetAll(trans);
        }

        /// <summary>
        /// 返回数据库所有的对象集合
        /// </summary>
        /// <param name="orderBy">自定义排序语句，如Order By Name Desc；如不指定，则使用默认排序</param>
        /// <param name="trans">事务对象</param>
        /// <returns>指定对象的集合</returns>
        public virtual List<T> GetAll(string orderBy, DbTransaction trans = null)
        {
            CheckDAL();
            return baseDal.GetAll(orderBy, trans);
        }
        
        /// <summary>
		/// 返回数据库所有的对象集合(用于分页数据显示)
		/// </summary>
        /// <param name="info">分页实体信息</param>
        /// <param name="trans">事务对象</param>
        /// <returns>指定对象的集合</returns>
        public virtual List<T> GetAll(PagerInfo info, DbTransaction trans = null)
        {
            CheckDAL();
            return baseDal.GetAll(info, trans);
        }

        /// <summary>
        /// 返回数据库所有的对象集合(用于分页数据显示)
        /// </summary>
        /// <param name="info">分页实体信息</param>
        /// <param name="fieldToSort">排序字段</param>
        /// <param name="desc">是否降序</param>
        /// <param name="trans">事务对象</param>
        /// <returns>指定对象的集合</returns>
        public virtual List<T> GetAll(PagerInfo info, string fieldToSort, bool desc, DbTransaction trans = null)
        {
            CheckDAL();
            return baseDal.GetAll(info, fieldToSort, desc, trans);
        }

        /// <summary>
        /// 返回所有记录到DataTable集合中
        /// </summary>
        /// <param name="trans">事务对象</param>
        /// <returns></returns>
        public virtual DataTable GetAllToDataTable(DbTransaction trans = null)
        {
            CheckDAL();
            return baseDal.GetAllToDataTable(trans);
        }

        /// <summary>
        /// 返回所有记录到DataTable集合中
        /// </summary>
        /// <param name="orderBy">自定义排序语句，如Order By Name Desc；如不指定，则使用默认排序</param>
        /// <param name="trans">事务对象</param>
        /// <returns></returns>
        public virtual DataTable GetAllToDataTable(string orderBy, DbTransaction trans = null)
        {
            CheckDAL();
            return baseDal.GetAllToDataTable(orderBy, trans);
        }

         /// <summary>
        /// 根据分页条件，返回DataSet对象
        /// </summary>
        /// <param name="info">分页条件</param>
        /// <param name="trans">事务对象</param>
        /// <returns></returns>
        public virtual DataTable GetAllToDataTable(PagerInfo info, DbTransaction trans = null)
        {
            CheckDAL();
            return baseDal.GetAllToDataTable(info, trans);
        }

        /// <summary>
        /// 根据分页条件，返回DataSet对象
        /// </summary>
        /// <param name="info">分页条件</param>
        /// <param name="fieldToSort">排序字段</param>
        /// <param name="trans">事务对象</param>
        /// <param name="desc">是否降序</param>
        /// <returns></returns>
        public virtual DataTable GetAllToDataTable(PagerInfo info, string fieldToSort, bool desc, DbTransaction trans = null)
        {
            CheckDAL();
            return baseDal.GetAllToDataTable(info, fieldToSort, desc, trans);
        }

        /// <summary>
        /// 根据查询条件，返回记录到DataTable集合中
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="trans">事务对象</param>
        /// <returns></returns>
        public virtual DataTable FindToDataTable(string condition, DbTransaction trans = null)
        {
            CheckDAL();
            return baseDal.FindToDataTable(condition, trans);
        }

        /// <summary>
        /// 根据查询条件，返回记录到DataTable集合中
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="pagerInfo">分页条件</param>
        /// <param name="trans">事务对象</param>
        /// <returns></returns>
        public virtual DataTable FindToDataTable(string condition, PagerInfo pagerInfo, DbTransaction trans = null)
        {
            CheckDAL();
            return baseDal.FindToDataTable(condition, pagerInfo, trans);
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
        public virtual DataTable FindToDataTable(string condition, PagerInfo info, string fieldToSort, bool desc, DbTransaction trans = null)
        {
            CheckDAL();
            return baseDal.FindToDataTable(condition, info, fieldToSort, desc, trans);
        }

        /// <summary>
        /// 获取字段列表
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        /// <param name="trans">事务对象</param>
        /// <returns></returns>
        public virtual List<string> GetFieldList(string fieldName, DbTransaction trans = null)
        {
            CheckDAL();
            return baseDal.GetFieldList(fieldName, trans);
        }

        /// <summary>
        /// 根据条件，获取某字段数据字典列表
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        /// <param name="trans">事务对象</param>
        /// <returns></returns>
        public List<string> GetFieldListByCondition(string fieldName, string condition, DbTransaction trans = null)
        {
            CheckDAL();
            return baseDal.GetFieldListByCondition(fieldName, condition, trans);
        }

        /// <summary>
        /// 根据条件，从视图里面获取记录
        /// </summary>
        /// <param name="viewName">视图名称</param>
        /// <param name="condition">查询条件</param>
        /// <param name="trans">事务对象</param>
        /// <returns></returns>
        public virtual DataTable FindByView(string viewName, string condition, DbTransaction trans = null)
        {
            CheckDAL();
            return baseDal.FindByView(viewName, condition, trans);
        }
               
        /// <summary>
        /// 根据条件，从视图里面获取记录
        /// </summary>
        /// <param name="viewName">视图名称</param>
        /// <param name="condition">查询条件</param>
        /// <param name="sortField">排序字段</param>
        /// <param name="isDescending">是否为降序</param>
        /// <param name="trans">事务对象</param>
        /// <returns></returns>
        public virtual DataTable FindByView(string viewName, string condition, string sortField, bool isDescending, DbTransaction trans = null)
        {
            CheckDAL();
            return baseDal.FindByView(viewName, condition, sortField, isDescending, trans);
        }
                       
        /// <summary>
        /// 根据条件，从视图里面获取记录
        /// </summary>
        /// <param name="viewName">视图名称</param>
        /// <param name="condition">查询条件</param>
        /// <param name="sortField">排序字段</param>
        /// <param name="isDescending">是否为降序</param>
        /// <param name="info">分页条件</param>
        /// <param name="trans">事务对象</param>
        /// <returns></returns>
        public virtual DataTable FindByViewWithPager(string viewName, string condition, string sortField, bool isDescending, PagerInfo info, DbTransaction trans = null)
        {
            CheckDAL();
            return baseDal.FindByViewWithPager(viewName, condition, sortField, isDescending, info, trans);
        }

        #endregion

        #region 基础接口
                       
        /// <summary>
        /// 获取表的所有记录数量
        /// </summary>
        /// <param name="trans">事务对象</param>
        /// <returns></returns>
        public virtual int GetRecordCount(DbTransaction trans = null)
        {
            CheckDAL();
            return baseDal.GetRecordCount(trans);
        }

        /// <summary>
        /// 获取表的指定条件记录数量
        /// </summary>
        /// <param name="trans">事务对象</param>
        /// <returns></returns>
        public virtual int GetRecordCount(string condition, DbTransaction trans = null)
        {
            CheckDAL();
            return baseDal.GetRecordCount(condition, trans);
        }

        /// <summary>
        /// 根据condition条件，判断是否存在记录
        /// </summary>
        /// <param name="condition">查询的条件</param>
        /// <param name="trans">事务对象</param>
        /// <returns>如果存在返回True，否则False</returns>
        public virtual bool IsExistRecord(string condition, DbTransaction trans = null)
        {
            CheckDAL();
            return baseDal.IsExistRecord(condition, trans);
        }

        /// <summary>
        /// 查询数据库,检查是否存在指定键值的对象
        /// </summary>
        /// <param name="fieldName">指定的属性名</param>
        /// <param name="key">指定的值</param>
        /// <param name="trans">事务对象</param>
        /// <returns>存在则返回<c>true</c>，否则为<c>false</c>。</returns>
        public virtual bool IsExistKey(string fieldName, object key, DbTransaction trans = null)
        {
            CheckDAL();
            return baseDal.IsExistKey(fieldName, key, trans);
        }
                        
        /// <summary>
        /// 根据主键和字段名称，获取对应字段的内容
        /// </summary>
        /// <param name="key">指定对象的ID</param>
        /// <param name="fieldName">字段名称</param>
        /// <param name="trans">事务对象</param>
        /// <returns></returns>
        public virtual string GetFieldValue(object key, string fieldName, DbTransaction trans = null)
        {
            CheckDAL();
            return baseDal.GetFieldValue(key, fieldName, trans);
        }

        /// <summary>
        /// 根据指定对象的ID,从数据库中删除指定对象
        /// </summary>
        /// <param name="key">指定对象的ID</param>
        /// <param name="trans">事务对象</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        public virtual bool Delete(object key, DbTransaction trans = null)
        {
            CheckDAL();
            return baseDal.Delete(key, trans);
        }  

        /// <summary>
        /// 根据指定条件,从数据库中删除指定对象
        /// </summary>
        /// <param name="condition">删除记录的条件语句</param>
        /// <param name="trans">事务对象</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        public virtual bool DeleteByCondition(string condition, DbTransaction trans = null)
        {
            CheckDAL();
            return baseDal.DeleteByCondition(condition, trans);
        }
     
        /// <summary>
        /// 打开数据库连接，并创建事务对象
        /// </summary>
        public virtual DbTransaction CreateTransaction()
        {
            CheckDAL();
            return baseDal.CreateTransaction();
        }
                
        /// <summary>
        /// 打开数据库连接，并创建事务对象
        /// </summary>
        /// <param name="level">事务级别</param>
        public virtual DbTransaction CreateTransaction(IsolationLevel level)
        {
            CheckDAL();
            return baseDal.CreateTransaction(level);
        }

        #endregion

        #region 其他接口
        /// <summary>
        /// 初始化数据库表名
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        public virtual void InitTableName(string tableName)
        {
            CheckDAL();
            baseDal.InitTableName(tableName);
        }

        /// <summary>
        /// 获取表的字段名称和数据类型列表
        /// </summary>
        /// <returns></returns>
        public virtual DataTable GetFieldTypeList()
        {
            CheckDAL();
            return baseDal.GetFieldTypeList();
        }

        /// <summary>
        /// 获取字段中文别名（用于界面显示）的字典集合
        /// </summary>
        /// <returns></returns>
        public virtual Dictionary<string, string> GetColumnNameAlias()
        {
            CheckDAL();
            return baseDal.GetColumnNameAlias();
        }

        /// <summary>
        /// 获取指定字段的报表数据
        /// </summary>
        /// <param name="fieldName">表字段</param>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        public virtual DataTable GetReportData(string fieldName, string condition)
        {
            CheckDAL();
            return baseDal.GetReportData(fieldName, condition);
        } 
        #endregion
    }
}
