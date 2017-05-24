using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Collections.Generic;
using System.ServiceModel;
using System.Reflection;
using JCodes.Framework.Entity;
using JCodes.Framework.Common.Files;
using JCodes.Framework.Common.Network;

namespace JCodes.Framework.Common.Framework.Facade
{
    /// <summary>
    /// 基于WCF数据访问方式的基础API包装类
    /// </summary>
    /// <typeparam name="T">Facade接口</typeparam>
    public class BaseWCFService<T> : IBaseService<T> where T : BaseEntity, new()
    {
        private AppConfig config = new AppConfig();
        /// <summary>
        /// WCF配置文件, 默认为"WcfConfig.config"
        /// </summary>
        protected string configurationPath = "WcfConfig.config"; 

        /// <summary>
        /// 服务配置节点,在子类中配置
        /// </summary>
        protected string endpointConfigurationName = "";

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public BaseWCFService()
        {         
        }
               
        /// <summary>
        /// 使用自定义终结点配置
        /// </summary>
        /// <param name="endpointConfigurationName">终结点配置项名称</param>
        /// <param name="configurationPath">配置路径</param>
        public BaseWCFService(string endpointConfigurationName, string configurationPath)
        {
            this.endpointConfigurationName = endpointConfigurationName;
            this.configurationPath = configurationPath;
        }

        /// <summary>
        /// 子类构造一个ChannelFactory，方便给基类进行调用通用的API
        /// </summary>
        /// <returns></returns>
        protected virtual IBaseService<T> CreateClient()
        {
            return null;
        }

        #region 对象添加、修改、查询接口

        /// <summary>
        /// 插入指定对象到数据库中
        /// </summary>
        /// <param name="obj">指定的对象</param>
        /// <returns>执行成功返回新增记录的自增长ID。</returns>
        public virtual bool Insert(T obj)
        {
            bool result = false;

            IBaseService<T> service = CreateClient();
            ICommunicationObject comm = service as ICommunicationObject;
            comm.Using(client =>
            {
                result = service.Insert(obj);
            });
            return result;
        }

        /// <summary>
        /// 插入指定对象到数据库中
        /// </summary>
        /// <param name="obj">指定的对象</param>
        /// <returns>执行成功返回新增记录的自增长ID。</returns>
        public virtual int Insert2(T obj)
        {
            int result = -1;

            IBaseService<T> service = CreateClient();
            ICommunicationObject comm = service as ICommunicationObject;
            comm.Using(client =>
            {
                result = service.Insert2(obj);
            });

            return result;
        }

        /// <summary>
        /// 插入或更新对象属性到数据库中
        /// </summary>
        /// <param name="obj">指定的对象</param>
        /// <param name="primaryKeyValue">主键的值</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        public virtual bool InsertUpdate(T obj, object primaryKeyValue)
        {
            bool result = false;

            IBaseService<T> service = CreateClient();
            ICommunicationObject comm = service as ICommunicationObject;
            comm.Using(client =>
            {
                result = service.InsertUpdate(obj, primaryKeyValue);
            });

            return result;
        }

        /// <summary>
        /// 如果不存在记录，则插入对象属性到数据库中
        /// </summary>
        /// <param name="obj">指定的对象</param>
        /// <param name="primaryKeyValue">主键的值</param>
        /// <returns>执行插入成功返回<c>true</c>，否则为<c>false</c>。</returns>
        public virtual bool InsertIfNew(T obj, object primaryKeyValue)
        {
            bool result = false;

            IBaseService<T> service = CreateClient();
            ICommunicationObject comm = service as ICommunicationObject;
            comm.Using(client =>
            {
                result = service.InsertIfNew(obj, primaryKeyValue);
            });

            return result;
        }

        /// <summary>
        /// 更新对象属性到数据库中
        /// </summary>
        /// <param name="obj">指定的对象</param>
        /// <param name="primaryKeyValue">主键的值</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        public virtual bool Update(T obj, object primaryKeyValue)
        {
            bool result = false;

            IBaseService<T> service = CreateClient();
            ICommunicationObject comm = service as ICommunicationObject;
            comm.Using(client =>
            {
                result = service.Update(obj, primaryKeyValue);
            });

            return result;
        }

        /// <summary>
        /// 执行SQL查询语句，返回查询结果的所有记录的第一个字段,用逗号分隔。
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns>
        /// 返回查询结果的所有记录的第一个字段,用逗号分隔。
        /// </returns>
        public virtual string SqlValueList(string sql)
        {
            string result = "";

            IBaseService<T> service = CreateClient();
            ICommunicationObject comm = service as ICommunicationObject;
            comm.Using(client =>
            {
                result = service.SqlValueList(sql);
            });
            return result;
        }

        /// <summary>
        /// 执行SQL查询语句，返回所有记录的DataTable集合。
        /// </summary>
        /// <param name="sql">SQL查询语句</param>
        /// <returns></returns>
        public virtual DataTable SqlTable(string sql)
        {
            DataTable result = new DataTable();

            IBaseService<T> service = CreateClient();
            ICommunicationObject comm = service as ICommunicationObject;
            comm.Using(client =>
            {
                result = service.SqlTable(sql);
            });

            return result;
        }

        /// <summary>
        /// 查询数据库,检查是否存在指定ID的对象(用于字符型主键)
        /// </summary>
        /// <param name="key">对象的ID值</param>
        /// <returns>存在则返回指定的对象,否则返回Null</returns>
        public virtual T FindByID(string key)
        {
            T result = null;

            IBaseService<T> service = CreateClient();
            ICommunicationObject comm = service as ICommunicationObject;
            comm.Using(client =>
            {
                result = service.FindByID(key);
            });

            return result;
        }

        /// <summary>
        /// 查询数据库,检查是否存在指定ID的对象(用于整型主键)
        /// </summary>
        /// <param name="key">对象的ID值</param>
        /// <returns>存在则返回指定的对象,否则返回Null</returns>
        public virtual T FindByID2(int key)
        {
            T result = null;
            IBaseService<T> service = CreateClient();
            ICommunicationObject comm = service as ICommunicationObject;
            comm.Using(client =>
            {
                result = service.FindByID2(key);
            });
            return result;
        }

        /// <summary>
        /// 根据条件查询数据库,如果存在返回第一个对象
        /// </summary>
        /// <param name="condition">查询的条件</param>
        /// <returns>指定的对象</returns>
        public virtual T FindSingle(string condition)
        {
            T result = null;

            IBaseService<T> service = CreateClient();
            ICommunicationObject comm = service as ICommunicationObject;
            comm.Using(client =>
            {
                result = service.FindSingle(condition);
            });

            return result;
        }

        /// <summary>
        /// 根据条件查询数据库,如果存在返回第一个对象
        /// </summary>
        /// <param name="condition">查询的条件</param>
        /// <param name="orderBy">排序条件</param>
        /// <returns>指定的对象</returns>
        public virtual T FindSingle2(string condition, string orderBy)
        {
            T result = null;

            IBaseService<T> service = CreateClient();
            ICommunicationObject comm = service as ICommunicationObject;
            comm.Using(client =>
            {
                result = service.FindSingle2(condition, orderBy);
            });

            return result;
        }

        /// <summary>
        /// 查找记录表中最旧的一条记录
        /// </summary>
        /// <returns></returns>
        public virtual T FindFirst()
        {
            T result = null;

            IBaseService<T> service = CreateClient();
            ICommunicationObject comm = service as ICommunicationObject;
            comm.Using(client =>
            {
                result = service.FindFirst();
            });

            return result;
        }

        /// <summary>
        /// 查找记录表中最新的一条记录
        /// </summary>
        /// <returns></returns>
        public virtual T FindLast()
        {
            T result = null;
            IBaseService<T> service = CreateClient();
            ICommunicationObject comm = service as ICommunicationObject;
            comm.Using(client =>
            {
                result = service.FindLast();
            });

            return result;
        }

        #endregion

        #region 返回集合的接口

        /// <summary>
        /// 根据ID字符串(逗号分隔)获取对象列表
        /// </summary>
        /// <param name="idString">ID字符串(逗号分隔)</param>
        /// <returns>符合条件的对象列表</returns>
        public virtual List<T> FindByIDs(string idString)
        {
            List<T> result = new List<T>();

            IBaseService<T> service = CreateClient();
            ICommunicationObject comm = service as ICommunicationObject;
            comm.Using(client =>
            {
                result = service.FindByIDs(idString);
            });

            return result;
        }

        /// <summary>
        /// 根据条件查询数据库,并返回对象集合
        /// </summary>
        /// <param name="condition">查询的条件</param>
        /// <returns>指定对象的集合</returns>
        public virtual List<T> Find(string condition)
        {
            List<T> result = new List<T>();
            IBaseService<T> service = CreateClient();
            ICommunicationObject comm = service as ICommunicationObject;
            comm.Using(client =>
            {
                result = service.Find(condition);
            });

            return result;
        }

        /// <summary>
        /// 根据条件查询数据库,并返回对象集合
        /// </summary>
        /// <param name="condition">查询的条件</param>
        /// <param name="orderBy">排序条件</param>
        /// <returns>指定对象的集合</returns>
        public virtual List<T> Find2(string condition, string orderBy)
        {
            List<T> result = new List<T>();

            IBaseService<T> service = CreateClient();
            ICommunicationObject comm = service as ICommunicationObject;
            comm.Using(client =>
            {
                result = service.Find2(condition, orderBy);
            });

            return result;
        }

        /// <summary>
        /// 根据条件查询数据库,并返回对象集合(用于分页数据显示)
        /// </summary>
        /// <param name="condition">查询的条件</param>
        /// <param name="info">分页实体</param>
        /// <returns>指定对象的集合</returns>
        public virtual List<T> FindWithPager(string condition, ref PagerInfo info)
        {
            List<T> result = new List<T>();
            IBaseService<T> service = CreateClient();
            ICommunicationObject comm = service as ICommunicationObject;
            PagerInfo pagerInfo = info;
            comm.Using(client =>
            {
                result = service.FindWithPager(condition, ref pagerInfo);
            });
            info.RecordCount = pagerInfo.RecordCount;

            return result;
        }

        /// <summary>
        /// 根据条件查询数据库,并返回对象集合(用于分页数据显示)
        /// </summary>
        /// <param name="condition">查询的条件</param>
        /// <param name="info">分页实体</param>
        /// <param name="fieldToSort">排序字段</param>
        /// <returns>指定对象的集合</returns>
        public virtual List<T> FindWithPager2(string condition, ref PagerInfo info, string fieldToSort)
        {
            List<T> result = new List<T>();

            IBaseService<T> service = CreateClient();
            ICommunicationObject comm = service as ICommunicationObject;
            PagerInfo pagerInfo = info;
            comm.Using(client =>
            {
                result = service.FindWithPager2(condition, ref pagerInfo, fieldToSort);
            });
            info.RecordCount = pagerInfo.RecordCount;

            return result;
        }

        /// <summary>
        /// 根据条件查询数据库,并返回对象集合(用于分页数据显示)
        /// </summary>
        /// <param name="condition">查询的条件</param>
        /// <param name="info">分页实体</param>
        /// <param name="fieldToSort">排序字段</param>
        /// <param name="desc">是否降序</param>
        /// <returns>指定对象的集合</returns>
        public virtual List<T> FindWithPager3(string condition, ref PagerInfo info, string fieldToSort, bool desc)
        {
            List<T> result = new List<T>();

            IBaseService<T> service = CreateClient();
            ICommunicationObject comm = service as ICommunicationObject;
            PagerInfo pagerInfo = info;
            comm.Using(client =>
            {
                result = service.FindWithPager3(condition, ref pagerInfo, fieldToSort, desc);
            });
            info.RecordCount = pagerInfo.RecordCount;

            return result;
        }

        /// <summary>
        /// 返回数据库所有的对象集合
        /// </summary>
        /// <returns>指定对象的集合</returns>
        public virtual List<T> GetAll()
        {
            List<T> result = new List<T>();

            IBaseService<T> service = CreateClient();
            ICommunicationObject comm = service as ICommunicationObject;
            comm.Using(client =>
            {
                result = service.GetAll();
            });

            return result;
        }

        /// <summary>
        /// 返回数据库所有的对象集合
        /// </summary>
        /// <param name="orderBy">自定义排序语句，如Order By Name Desc；如不指定，则使用默认排序</param>
        /// <returns>指定对象的集合</returns>
        public virtual List<T> GetAll2(string orderBy)
        {
            List<T> result = new List<T>();

            IBaseService<T> service = CreateClient();
            ICommunicationObject comm = service as ICommunicationObject;
            comm.Using(client =>
            {
                result = service.GetAll2(orderBy);
            });

            return result;
        }

        /// <summary>
        /// 返回数据库所有的对象集合(用于分页数据显示)
        /// </summary>
        /// <param name="info">分页实体信息</param>
        /// <returns>指定对象的集合</returns>
        public virtual List<T> GetAllWithPager(ref PagerInfo info)
        {
            List<T> result = new List<T>();
            IBaseService<T> service = CreateClient();
            ICommunicationObject comm = service as ICommunicationObject;
            PagerInfo pagerInfo = info;
            comm.Using(client =>
            {
                result = service.GetAllWithPager(ref pagerInfo);
            });
            info.RecordCount = pagerInfo.RecordCount;

            return result;
        }

        /// <summary>
        /// 返回数据库所有的对象集合(用于分页数据显示)
        /// </summary>
        /// <param name="info">分页实体信息</param>
        /// <param name="fieldToSort">排序字段</param>
        /// <param name="desc">是否降序</param>
        /// <returns>指定对象的集合</returns>
        public virtual List<T> GetAllWithPager2(ref PagerInfo info, string fieldToSort, bool desc)
        {
            List<T> result = new List<T>();

            IBaseService<T> service = CreateClient();
            ICommunicationObject comm = service as ICommunicationObject;
            PagerInfo pagerInfo = info;
            comm.Using(client =>
            {
                result = service.GetAllWithPager2(ref pagerInfo, fieldToSort, desc);
            });
            info.RecordCount = pagerInfo.RecordCount;

            return result;
        }

        /// <summary>
        /// 返回所有记录到DataTable集合中
        /// </summary>
        /// <returns></returns>
        public virtual DataTable GetAllToDataTable()
        {
            DataTable result = new DataTable();
            result.TableName = "tableName";

            IBaseService<T> service = CreateClient();
            ICommunicationObject comm = service as ICommunicationObject;
            comm.Using(client =>
            {
                result = service.GetAllToDataTable();
            });

            return result;
        }

        /// <summary>
        /// 返回所有记录到DataTable集合中
        /// </summary>
        /// <param name="orderBy">自定义排序语句，如Order By Name Desc；如不指定，则使用默认排序</param>
        /// <returns></returns>
        public DataTable GetAllToDataTable2(string orderBy)
        {
            DataTable result = new DataTable();
            result.TableName = "tableName";

            IBaseService<T> service = CreateClient();
            ICommunicationObject comm = service as ICommunicationObject;
            comm.Using(client =>
            {
                result = service.GetAllToDataTable2(orderBy);
            });

            return result;
        }

        /// <summary>
        /// 根据分页条件，返回DataSet对象
        /// </summary>
        /// <param name="info">分页条件</param>
        /// <returns></returns>
        public virtual DataTable GetAllToDataTableWithPager(ref PagerInfo info)
        {
            DataTable result = new DataTable();
            result.TableName = "tableName";

            IBaseService<T> service = CreateClient();
            ICommunicationObject comm = service as ICommunicationObject;
            PagerInfo pagerInfo = info;
            comm.Using(client =>
            {
                result = service.GetAllToDataTableWithPager(ref pagerInfo);
            });
            info.RecordCount = pagerInfo.RecordCount;

            return result;
        }

        /// <summary>
        /// 根据分页条件，返回DataSet对象
        /// </summary>
        /// <param name="info">分页条件</param>
        /// <param name="fieldToSort">排序字段</param>
        /// <param name="desc">是否降序</param>
        /// <returns></returns>
        public virtual DataTable GetAllToDataTableWithPager2(ref PagerInfo info, string fieldToSort, bool desc)
        {
            DataTable result = new DataTable();
            result.TableName = "tableName";

            IBaseService<T> service = CreateClient();
            ICommunicationObject comm = service as ICommunicationObject;
            PagerInfo pagerInfo = info;
            comm.Using(client =>
            {
                result = service.GetAllToDataTableWithPager2(ref pagerInfo, fieldToSort, desc);
            });
            info.RecordCount = pagerInfo.RecordCount;

            return result;
        }
        
        /// <summary>
        /// 根据查询条件，返回记录到DataTable集合中
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        public virtual DataTable FindToDataTable(string condition)
        {
            DataTable result = new DataTable();
            result.TableName = "tableName";

            IBaseService<T> service = CreateClient();
            ICommunicationObject comm = service as ICommunicationObject;
            comm.Using(client =>
            {
                result = service.FindToDataTable(condition);
            });

            return result;
        }

        /// <summary>
        /// 根据查询条件，返回记录到DataTable集合中
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="info">分页条件</param>
        /// <returns></returns>
        public virtual DataTable FindToDataTableWithPager(string condition, ref PagerInfo info)
        {
            DataTable result = new DataTable();
            result.TableName = "tableName";

            IBaseService<T> service = CreateClient();
            ICommunicationObject comm = service as ICommunicationObject;
            PagerInfo pagerInfo = info;
            comm.Using(client =>
            {
                result = service.FindToDataTableWithPager(condition, ref pagerInfo);
            });
            info.RecordCount = pagerInfo.RecordCount;

            return result;
        }

        /// <summary>
        /// 根据条件查询数据库,并返回DataTable集合(用于分页数据显示)
        /// </summary>
        /// <param name="condition">查询的条件</param>
        /// <param name="info">分页实体</param>
        /// <param name="fieldToSort">排序字段</param>
        /// <param name="desc">是否降序</param>
        /// <returns>指定DataTable的集合</returns>
        public virtual DataTable FindToDataTableWithPager2(string condition, ref PagerInfo info, string fieldToSort, bool desc)
        {
            DataTable result = new DataTable();
            result.TableName = "tableName";

            IBaseService<T> service = CreateClient();
            ICommunicationObject comm = service as ICommunicationObject;
            PagerInfo pagerInfo = info;
            comm.Using(client =>
            {
                result = service.FindToDataTableWithPager2(condition, ref pagerInfo, fieldToSort, desc);                
            });
            info.RecordCount = pagerInfo.RecordCount;

            return result;
        }

        /// <summary>
        /// 根据条件，从视图里面获取记录
        /// </summary>
        /// <param name="viewName">视图名称</param>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        public virtual DataTable FindByView(string viewName, string condition)
        {
            DataTable result = new DataTable();
            result.TableName = "tableName";

            IBaseService<T> service = CreateClient();
            ICommunicationObject comm = service as ICommunicationObject;
            comm.Using(client =>
            {
                result = service.FindByView(viewName, condition);
            });

            return result;
        }


        /// <summary>
        /// 根据条件，从视图里面获取记录
        /// </summary>
        /// <param name="viewName">视图名称</param>
        /// <param name="condition">查询条件</param>
        /// <param name="sortField">排序字段</param>
        /// <param name="isDescending">是否为降序</param>
        /// <returns></returns>
        public DataTable FindByView2(string viewName, string condition, string sortField, bool isDescending)
        {
            DataTable result = new DataTable();
            result.TableName = "tableName";

            IBaseService<T> service = CreateClient();
            ICommunicationObject comm = service as ICommunicationObject;
            comm.Using(client =>
            {
                result = service.FindByView2(viewName, condition, sortField, isDescending);
            });

            return result;
        }

        /// <summary>
        /// 根据条件，从视图里面获取记录
        /// </summary>
        /// <param name="viewName">视图名称</param>
        /// <param name="condition">查询条件</param>
        /// <param name="sortField">排序字段</param>
        /// <param name="isDescending">是否为降序</param>
        /// <param name="info">分页条件</param>
        /// <returns></returns>
        public DataTable FindByViewWithPager(string viewName, string condition, string sortField, bool isDescending, PagerInfo info)
        {
            DataTable result = new DataTable();
            result.TableName = "tableName";

            IBaseService<T> service = CreateClient();
            ICommunicationObject comm = service as ICommunicationObject;
            comm.Using(client =>
            {
                result = service.FindByViewWithPager(viewName, condition, sortField, isDescending, info);
            });

            return result;
        }

        #endregion

        #region 基础接口

        /// <summary>
        /// 获取表的所有记录数量
        /// </summary>
        /// <returns></returns>
        public virtual int GetRecordCount2(string condition)
        {
            int result = 0;

            IBaseService<T> service = CreateClient();
            ICommunicationObject comm = service as ICommunicationObject;
            comm.Using(client =>
            {
                result = service.GetRecordCount2(condition);
            });

            return result;
        }

        /// <summary>
        /// 获取表的所有记录数量
        /// </summary>
        /// <returns></returns>
        public virtual int GetRecordCount()
        {
            int result = 0;

            IBaseService<T> service = CreateClient();
            ICommunicationObject comm = service as ICommunicationObject;
            comm.Using(client =>
            {
                result = service.GetRecordCount();
            });

            return result;
        }

        /// <summary>
        /// 查询数据库,检查是否存在指定键值的对象
        /// </summary>
        /// <param name="fieldName">指定的属性名</param>
        /// <param name="key">指定的值</param>
        /// <returns>存在则返回<c>true</c>，否则为<c>false</c>。</returns>
        public virtual bool IsExistKey(string fieldName, object key)
        {
            bool result = false;
            IBaseService<T> service = CreateClient();
            ICommunicationObject comm = service as ICommunicationObject;
            comm.Using(client =>
            {
                result = service.IsExistKey(fieldName, key);
            });

            return result;
        }

        /// <summary>
        /// 根据condition条件，判断是否存在记录
        /// </summary>
        /// <param name="condition">查询的条件</param>
        /// <returns>如果存在返回True，否则False</returns>
        public virtual bool IsExistRecord(string condition)
        {
            bool result = false;
            IBaseService<T> service = CreateClient();
            ICommunicationObject comm = service as ICommunicationObject;
            comm.Using(client =>
            {
                result = service.IsExistRecord(condition);
            });

            return result;
        }

        /// <summary>
        /// 根据指定对象的ID,从数据库中删除指定对象(用于字符型主键)
        /// </summary>
        /// <param name="key">指定对象的ID</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        public virtual bool Delete(string key)
        {
            bool result = false;
            IBaseService<T> service = CreateClient();
            ICommunicationObject comm = service as ICommunicationObject;
            comm.Using(client =>
            {
                result = service.Delete(key);
            });

            return result;
        }

        /// <summary>
        /// 根据指定对象的ID,从数据库中删除指定对象(用于整型主键)
        /// </summary>
        /// <param name="key">指定对象的ID</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        public virtual bool Delete2(int key)
        {
            bool result = false;
            IBaseService<T> service = CreateClient();
            ICommunicationObject comm = service as ICommunicationObject;
            comm.Using(client =>
            {
                result = service.Delete2(key);
            });

            return result;
        }

        /// <summary>
        /// 根据指定条件,从数据库中删除指定对象
        /// </summary>
        /// <param name="condition">删除记录的条件语句</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        public virtual bool DeleteByCondition(string condition)
        {
            bool result = false;

            IBaseService<T> service = CreateClient();
            ICommunicationObject comm = service as ICommunicationObject;
            comm.Using(client =>
            {
                result = service.DeleteByCondition(condition);
            });

            return result;
        } 
        #endregion

        #region 辅助型接口
                        
        /// <summary>
        /// 获取字段列表
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        /// <returns></returns>
        public virtual List<string> GetFieldList(string fieldName)
        {
            List<string> result = new List<string>();
            IBaseService<T> service = CreateClient();
            ICommunicationObject comm = service as ICommunicationObject;
            comm.Using(client =>
            {
                result = service.GetFieldList(fieldName);
            });

            return result;
        }
               
        /// <summary>
        /// 根据条件，获取某字段数据字典列表
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        /// <param name="condition">查询的条件</param>
        /// <returns></returns>
        public List<string> GetFieldListByCondition(string fieldName, string condition)
        {
            List<string> result = new List<string>();
            IBaseService<T> service = CreateClient();
            ICommunicationObject comm = service as ICommunicationObject;
            comm.Using(client =>
            {
                result = service.GetFieldListByCondition(fieldName, condition);
            });

            return result;
        }

        /// <summary>
        /// 获取表的字段名称和数据类型列表。
        /// </summary>
        /// <returns></returns>
        public virtual DataTable GetFieldTypeList()
        {
            DataTable result = new DataTable();
            result.TableName = "tableName";

            IBaseService<T> service = CreateClient();
            ICommunicationObject comm = service as ICommunicationObject;
            comm.Using(client =>
            {
                result = service.GetFieldTypeList();
            });

            return result;
        }

        /// <summary>
        /// 获取字段中文别名（用于界面显示）的字典集合
        /// </summary>
        /// <returns></returns>
        public virtual Dictionary<string, string> GetColumnNameAlias()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            IBaseService<T> service = CreateClient();
            ICommunicationObject comm = service as ICommunicationObject;
            comm.Using(client =>
            {
                result = service.GetColumnNameAlias();
            });

            return result;
        }

        /// <summary>
        /// 获取指定字段的报表数据
        /// </summary>
        /// <param name="fieldName">表字段</param>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        public virtual DataTable GetReportData(string fieldName, string condition)
        {
            DataTable result = new DataTable();
            result.TableName = "tableName";

            IBaseService<T> service = CreateClient();
            ICommunicationObject comm = service as ICommunicationObject;
            comm.Using(client =>
            {
                result = service.GetReportData(fieldName, condition);
            });

            return result;
        }

        #endregion

    }
}
