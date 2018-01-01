using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;
using System.ServiceModel;
using System.Reflection;

using System.Data.Common;
using JCodes.Framework.Entity;

namespace JCodes.Framework.Common.Framework.Facade
{
    /// <summary>
    /// 基于Winform的基础API包装类
    /// </summary>
    /// <typeparam name="T">Facade接口</typeparam>
    public class BaseLocalService<T> : IBaseService<T> where T : BaseEntity, new()
    {
        /// <summary>
        /// 基础业务对象
        /// </summary>
        protected BaseBLL<T> baseBLL = null;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public BaseLocalService()
        {
        }

        /// <summary>
        /// 使用业务对象构造对象
        /// </summary>
        /// <param name="bll"></param>
        public BaseLocalService(BaseBLL<T> bll)
        {
            this.baseBLL = bll;
        }

        #region 对象添加、修改、查询接口

        /// <summary>
        /// 插入指定对象到数据库中
        /// </summary>
        /// <param name="obj">指定的对象</param>
        /// <returns>执行成功返回新增记录的自增长ID。</returns>
        public virtual bool Insert(T obj)
        {
            return baseBLL.Insert(obj);
        }

        /// <summary>
        /// 插入指定对象到数据库中
        /// </summary>
        /// <param name="obj">指定的对象</param>
        /// <returns>执行成功返回新增记录的自增长ID。</returns>
        public virtual int Insert2(T obj)
        {
            return baseBLL.Insert2(obj);
        }
                        
        /// <summary>
        /// 插入或更新对象属性到数据库中
        /// </summary>
        /// <param name="obj">指定的对象</param>
        /// <param name="primaryKeyValue">主键的值</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        public virtual bool InsertUpdate(T obj, object primaryKeyValue)
        {
            return baseBLL.InsertUpdate(obj, primaryKeyValue);
        }
                
        /// <summary>
        /// 如果不存在记录，则插入对象属性到数据库中
        /// </summary>
        /// <param name="obj">指定的对象</param>
        /// <param name="primaryKeyValue">主键的值</param>
        /// <returns>执行插入成功返回<c>true</c>，否则为<c>false</c>。</returns>
        public virtual bool InsertIfNew(T obj, object primaryKeyValue)
        {
            return baseBLL.InsertIfNew(obj, primaryKeyValue);
        }

        /// <summary>
        /// 更新对象属性到数据库中
        /// </summary>
        /// <param name="obj">指定的对象</param>
        /// <param name="primaryKeyValue">主键的值</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        public virtual bool Update(T obj, object primaryKeyValue)
        {
            return baseBLL.Update(obj, primaryKeyValue);
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
            return baseBLL.SqlValueList(sql);
        }

        /// <summary>
        /// 执行SQL查询语句，返回所有记录的DataTable集合。
        /// </summary>
        /// <param name="sql">SQL查询语句</param>
        /// <returns></returns>
        public virtual DataTable SqlTable(string sql)
        {
            return baseBLL.SqlTable(sql);
        }

        /// <summary>
        /// 查询数据库,检查是否存在指定ID的对象(用于字符型主键)
        /// </summary>
        /// <param name="key">对象的ID值</param>
        /// <returns>存在则返回指定的对象,否则返回Null</returns>
        public virtual T FindByID(string key)
        {
            return baseBLL.FindByID(key);
        }

        /// <summary>
        /// 查询数据库,检查是否存在指定ID的对象(用于整型主键)
        /// </summary>
        /// <param name="key">对象的ID值</param>
        /// <returns>存在则返回指定的对象,否则返回Null</returns>
        public virtual T FindByID2(int key)
        {
            return baseBLL.FindByID(key);
        }

        /// <summary>
        /// 根据条件查询数据库,如果存在返回第一个对象
        /// </summary>
        /// <param name="condition">查询的条件</param>
        /// <returns>指定的对象</returns>
        public virtual T FindSingle(string condition)
        {
            return baseBLL.FindSingle(condition);
        }

        /// <summary>
        /// 根据条件查询数据库,如果存在返回第一个对象
        /// </summary>
        /// <param name="condition">查询的条件</param>
        /// <param name="orderBy">排序条件</param>
        /// <returns>指定的对象</returns>
        public virtual T FindSingle2(string condition, string orderBy)
        {
            return baseBLL.FindSingle(condition, orderBy);
        }

        /// <summary>
        /// 查找记录表中最旧的一条记录
        /// </summary>
        /// <returns></returns>
        public T FindFirst()
        {
            return baseBLL.FindFirst();
        }

        /// <summary>
        /// 查找记录表中最新的一条记录
        /// </summary>
        /// <returns></returns>
        public T FindLast()
        {
            return baseBLL.FindLast();
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
            return baseBLL.FindByIDs(idString);
        }

        /// <summary>
        /// 根据条件查询数据库,并返回对象集合
        /// </summary>
        /// <param name="condition">查询的条件</param>
        /// <returns>指定对象的集合</returns>
        public virtual List<T> Find(string condition)
        {
            return baseBLL.Find(condition);
        }

        /// <summary>
        /// 根据条件查询数据库,并返回对象集合
        /// </summary>
        /// <param name="condition">查询的条件</param>
        /// <param name="orderBy">排序条件</param>
        /// <returns>指定对象的集合</returns>
        public virtual List<T> Find2(string condition, string orderBy)
        {
            return baseBLL.Find(condition, orderBy);
        }

        /// <summary>
        /// 根据条件查询数据库,并返回对象集合(用于分页数据显示)
        /// </summary>
        /// <param name="condition">查询的条件</param>
        /// <param name="info">分页实体</param>
        /// <returns>指定对象的集合</returns>
        public virtual List<T> FindWithPager(string condition, ref PagerInfo info)
        {
            return baseBLL.FindWithPager(condition, info);
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
            return baseBLL.FindWithPager(condition, info, fieldToSort);
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
            return baseBLL.FindWithPager(condition, info, fieldToSort, desc);
        }

        /// <summary>
        /// 返回数据库所有的对象集合
        /// </summary>
        /// <returns>指定对象的集合</returns>
        public virtual List<T> GetAll()
        {
            return baseBLL.GetAll();
        }

        /// <summary>
        /// 返回数据库所有的对象集合
        /// </summary>
        /// <param name="orderBy">自定义排序语句，如Order By Name Desc；如不指定，则使用默认排序</param>
        /// <returns>指定对象的集合</returns>
        public virtual List<T> GetAll2(string orderBy)
        {
            return baseBLL.GetAll(orderBy);
        }

        /// <summary>
        /// 返回数据库所有的对象集合(用于分页数据显示)
        /// </summary>
        /// <param name="info">分页实体信息</param>
        /// <returns>指定对象的集合</returns>
        public virtual List<T> GetAllWithPager(ref PagerInfo info)
        {
            return baseBLL.GetAll(info);
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
            return baseBLL.GetAll(info, fieldToSort, desc);
        }

        /// <summary>
        /// 返回所有记录到DataTable集合中
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllToDataTable()
        {
            return baseBLL.GetAllToDataTable();
        }

        /// <summary>
        /// 返回所有记录到DataTable集合中
        /// </summary>
        /// <param name="orderBy">自定义排序语句，如Order By Name Desc；如不指定，则使用默认排序</param>
        /// <returns></returns>
        public DataTable GetAllToDataTable2(string orderBy)
        {
            return baseBLL.GetAllToDataTable(orderBy);
        }

        /// <summary>
        /// 根据分页条件，返回DataSet对象
        /// </summary>
        /// <param name="info">分页条件</param>
        /// <returns></returns>
        public virtual DataTable GetAllToDataTableWithPager(ref PagerInfo info)
        {
            return baseBLL.GetAllToDataTable(info);
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
            return baseBLL.GetAllToDataTable(info, fieldToSort, desc);
        }
        
        /// <summary>
        /// 根据查询条件，返回记录到DataTable集合中
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        public DataTable FindToDataTable(string condition)
        {
            return baseBLL.FindToDataTable(condition);
        }

        /// <summary>
        /// 根据查询条件，返回记录到DataTable集合中
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="pagerInfo">分页条件</param>
        /// <returns></returns>
        public virtual DataTable FindToDataTableWithPager(string condition, ref PagerInfo pagerInfo)
        {
            return baseBLL.FindToDataTable(condition, pagerInfo);
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
            return baseBLL.FindToDataTable(condition, info, fieldToSort, desc);
        }

        /// <summary>
        /// 根据条件，从视图里面获取记录
        /// </summary>
        /// <param name="viewName">视图名称</param>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        public DataTable FindByView(string viewName, string condition)
        {
            return baseBLL.FindByView(viewName, condition);
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
            return baseBLL.FindByView(viewName, condition, sortField, isDescending);
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
            return baseBLL.FindByViewWithPager(viewName, condition, sortField, isDescending, info);
        }

        #endregion

        #region 基础接口

        /// <summary>
        /// 获取表的所有记录数量
        /// </summary>
        /// <returns></returns>
        public int GetRecordCount2(string condition)
        {
            return baseBLL.GetRecordCount(condition);
        }

        /// <summary>
        /// 获取表的所有记录数量
        /// </summary>
        /// <returns></returns>
        public int GetRecordCount()
        {
            return baseBLL.GetRecordCount();
        }

        /// <summary>
        /// 查询数据库,检查是否存在指定键值的对象
        /// </summary>
        /// <param name="fieldName">指定的属性名</param>
        /// <param name="key">指定的值</param>
        /// <returns>存在则返回<c>true</c>，否则为<c>false</c>。</returns>
        public virtual bool IsExistKey(string fieldName, object key)
        {
            return baseBLL.IsExistKey(fieldName, key);
        }

        /// <summary>
        /// 根据condition条件，判断是否存在记录
        /// </summary>
        /// <param name="condition">查询的条件</param>
        /// <returns>如果存在返回True，否则False</returns>
        public bool IsExistRecord(string condition)
        {
            return baseBLL.IsExistRecord(condition);
        }

        /// <summary>
        /// 根据指定对象的ID,从数据库中删除指定对象(用于字符型主键)
        /// </summary>
        /// <param name="key">指定对象的ID</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        public virtual bool DeleteByUser(string key, Int32 userId)
        {
            return baseBLL.DeleteByUser(key, userId);
        }

        /// <summary>
        /// 根据指定对象的ID,从数据库中删除指定对象(用于整型主键)
        /// </summary>
        /// <param name="key">指定对象的ID</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        public virtual bool DeleteByUser2(int key, Int32 userId)
        {
            return baseBLL.DeleteByUser(key, userId);
        }

        /// <summary>
        /// 根据指定条件,从数据库中删除指定对象
        /// </summary>
        /// <param name="condition">删除记录的条件语句</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        public virtual bool DeleteByCondition(string condition)
        {
            return baseBLL.DeleteByCondition(condition);
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
            return baseBLL.GetFieldList(fieldName);
        }

        /// <summary>
        /// 根据条件，获取某字段数据字典列表
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        /// <param name="condition">查询的条件</param>
        /// <returns></returns>
        public List<string> GetFieldListByCondition(string fieldName, string condition)
        {
            return baseBLL.GetFieldListByCondition(fieldName, condition);
        }

        /// <summary>
        /// 获取表的字段名称和数据类型列表。
        /// </summary>
        /// <returns></returns>
        public virtual DataTable GetFieldTypeList()
        {
            return baseBLL.GetFieldTypeList();
        }

        /// <summary>
        /// 获取字段中文别名（用于界面显示）的字典集合
        /// </summary>
        /// <returns></returns>
        public virtual Dictionary<string, string> GetColumnNameAlias()
        {
            return baseBLL.GetColumnNameAlias();
        }

        /// <summary>
        /// 获取指定字段的报表数据
        /// </summary>
        /// <param name="fieldName">表字段</param>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        public virtual DataTable GetReportData(string fieldName, string condition)
        {
            return baseBLL.GetReportData(fieldName, condition);
        }

        #endregion
    }
}
