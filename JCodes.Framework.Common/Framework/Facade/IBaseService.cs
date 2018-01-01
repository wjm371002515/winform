using System;
using System.Data;
using System.Text;
using System.ServiceModel;
using System.Collections.Generic;

using JCodes.Framework.Entity;

namespace JCodes.Framework.Common.Framework.Facade
{
    /// <summary>
    /// 基于Facade业务模式定义的接口，可以使用WCF、传统本地访问等模式进行获取数据的公共接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [ServiceContract]
    public interface IBaseService<T> where T : BaseEntity
    {
        #region 对象添加、修改、查询接口

        /// <summary>
        /// 插入指定对象到数据库中
        /// </summary>
        /// <param name="obj">指定的对象</param>
        /// <returns>执行操作是否成功。</returns>
        [OperationContract]
        bool Insert(T obj);

        /// <summary>
        /// 插入指定对象到数据库中
        /// </summary>
        /// <param name="obj">指定的对象</param>
        /// <returns>执行成功返回新增记录的自增长ID。</returns>
        [OperationContract]
        int Insert2(T obj);
                       
        /// <summary>
        /// 插入或更新对象属性到数据库中
        /// </summary>
        /// <param name="obj">指定的对象</param>
        /// <param name="primaryKeyValue">主键的值</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        [OperationContract]
        bool InsertUpdate(T obj, object primaryKeyValue);
                        
        /// <summary>
        /// 如果不存在记录，则插入对象属性到数据库中
        /// </summary>
        /// <param name="obj">指定的对象</param>
        /// <param name="primaryKeyValue">主键的值</param>
        /// <returns>执行插入成功返回<c>true</c>，否则为<c>false</c>。</returns>
        [OperationContract]
        bool InsertIfNew(T obj, object primaryKeyValue);

        /// <summary>
        /// 更新对象属性到数据库中
        /// </summary>
        /// <param name="obj">指定的对象</param>
        /// <param name="primaryKeyValue">主键的值</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        [OperationContract]
        bool Update(T obj, object primaryKeyValue);

        /// <summary>
        /// 执行SQL查询语句，返回查询结果的所有记录的第一个字段,用逗号分隔。
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns>
        /// 返回查询结果的所有记录的第一个字段,用逗号分隔。
        /// </returns>
        [OperationContract]
        string SqlValueList(string sql);

        /// <summary>
        /// 执行SQL查询语句，返回所有记录的DataTable集合。
        /// </summary>
        /// <param name="sql">SQL查询语句</param>
        /// <returns></returns>
        [OperationContract]
        DataTable SqlTable(string sql);

        /// <summary>
        /// 查询数据库,检查是否存在指定ID的对象(用于字符型主键)
        /// </summary>
        /// <param name="key">对象的ID值</param>
        /// <returns>存在则返回指定的对象,否则返回Null</returns>
        [OperationContract]
        T FindByID(string key);

        /// <summary>
        /// 查询数据库,检查是否存在指定ID的对象(用于整型主键)
        /// </summary>
        /// <param name="key">对象的ID值</param>
        /// <returns>存在则返回指定的对象,否则返回Null</returns>[OperationContract]
        [OperationContract]
        T FindByID2(int key);

        /// <summary>
        /// 根据条件查询数据库,如果存在返回第一个对象
        /// </summary>
        /// <param name="condition">查询的条件</param>
        /// <returns>指定的对象</returns>
        [OperationContract]
        T FindSingle(string condition);

        /// <summary>
        /// 根据条件查询数据库,如果存在返回第一个对象
        /// </summary>
        /// <param name="condition">查询的条件</param>
        /// <param name="orderBy">排序条件</param>
        /// <returns>指定的对象</returns>
        [OperationContract]
        T FindSingle2(string condition, string orderBy);

        /// <summary>
        /// 查找记录表中最旧的一条记录
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        T FindFirst();

        /// <summary>
        /// 查找记录表中最新的一条记录
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        T FindLast();

        #endregion

        #region 返回集合的接口

        /// <summary>
        /// 根据ID字符串(逗号分隔)获取对象列表
        /// </summary>
        /// <param name="idString">ID字符串(逗号分隔)</param>
        /// <returns>符合条件的对象列表</returns>
        [OperationContract]
        List<T> FindByIDs(string idString);

        /// <summary>
        /// 根据条件查询数据库,并返回对象集合
        /// </summary>
        /// <param name="condition">查询的条件</param>
        /// <returns>指定对象的集合</returns>
        [OperationContract]
        List<T> Find(string condition);

        /// <summary>
        /// 根据条件查询数据库,并返回对象集合
        /// </summary>
        /// <param name="condition">查询的条件</param>
        /// <param name="orderBy">排序条件</param>
        /// <returns>指定对象的集合</returns>
        [OperationContract]
        List<T> Find2(string condition, string orderBy);

        /// <summary>
        /// 根据条件查询数据库,并返回对象集合(用于分页数据显示)
        /// </summary>
        /// <param name="condition">查询的条件</param>
        /// <param name="info">分页实体</param>
        /// <returns>指定对象的集合</returns>
        [OperationContract]
        List<T> FindWithPager(string condition, ref PagerInfo info);

        /// <summary>
        /// 根据条件查询数据库,并返回对象集合(用于分页数据显示)
        /// </summary>
        /// <param name="condition">查询的条件</param>
        /// <param name="info">分页实体</param>
        /// <param name="fieldToSort">排序字段</param>
        /// <returns>指定对象的集合</returns>
        [OperationContract]
        List<T> FindWithPager2(string condition, ref PagerInfo info, string fieldToSort);

        /// <summary>
        /// 根据条件查询数据库,并返回对象集合(用于分页数据显示)
        /// </summary>
        /// <param name="condition">查询的条件</param>
        /// <param name="info">分页实体</param>
        /// <param name="fieldToSort">排序字段</param>
        /// <param name="desc">是否降序</param>
        /// <returns>指定对象的集合</returns>
        [OperationContract]
        List<T> FindWithPager3(string condition, ref PagerInfo info, string fieldToSort, bool desc);

        /// <summary>
        /// 返回数据库所有的对象集合
        /// </summary>
        /// <returns>指定对象的集合</returns>
        [OperationContract]
        List<T> GetAll();

        /// <summary>
        /// 返回数据库所有的对象集合
        /// </summary>
        /// <param name="orderBy">自定义排序语句，如Order By Name Desc；如不指定，则使用默认排序</param>
        /// <returns>指定对象的集合</returns>
        [OperationContract]
        List<T> GetAll2(string orderBy);

        /// <summary>
        /// 返回数据库所有的对象集合(用于分页数据显示)
        /// </summary>
        /// <param name="info">分页实体信息</param>
        /// <returns>指定对象的集合</returns>
        [OperationContract]
        List<T> GetAllWithPager(ref PagerInfo info);

        /// <summary>
        /// 返回数据库所有的对象集合(用于分页数据显示)
        /// </summary>
        /// <param name="info">分页实体信息</param>
        /// <param name="fieldToSort">排序字段</param>
        /// <param name="desc">是否降序</param>
        /// <returns>指定对象的集合</returns>
        [OperationContract]
        List<T> GetAllWithPager2(ref PagerInfo info, string fieldToSort, bool desc);

        /// <summary>
        /// 返回所有记录到DataTable集合中
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        DataTable GetAllToDataTable();

        /// <summary>
        /// 返回所有记录到DataTable集合中
        /// </summary>
        /// <param name="orderBy">自定义排序语句，如Order By Name Desc；如不指定，则使用默认排序</param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetAllToDataTable2(string orderBy);

        /// <summary>
        /// 根据分页条件，返回DataSet对象
        /// </summary>
        /// <param name="info">分页条件</param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetAllToDataTableWithPager(ref PagerInfo info);

        /// <summary>
        /// 根据分页条件，返回DataSet对象
        /// </summary>
        /// <param name="info">分页条件</param>
        /// <param name="fieldToSort">排序字段</param>
        /// <param name="desc">是否降序</param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetAllToDataTableWithPager2(ref PagerInfo info, string fieldToSort, bool desc);

        /// <summary>
        /// 根据查询条件，返回记录到DataTable集合中
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        [OperationContract]
        DataTable FindToDataTable(string condition);

        /// <summary>
        /// 根据查询条件，返回记录到DataTable集合中
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="pagerInfo">分页条件</param>
        /// <returns></returns>
        [OperationContract]
        DataTable FindToDataTableWithPager(string condition, ref PagerInfo pagerInfo);

        /// <summary>
        /// 根据条件查询数据库,并返回DataTable集合(用于分页数据显示)
        /// </summary>
        /// <param name="condition">查询的条件</param>
        /// <param name="info">分页实体</param>
        /// <param name="fieldToSort">排序字段</param>
        /// <param name="desc">是否降序</param>
        /// <returns>指定DataTable的集合</returns>
        [OperationContract]
        DataTable FindToDataTableWithPager2(string condition, ref PagerInfo info, string fieldToSort, bool desc);

        /// <summary>
        /// 根据条件，从视图里面获取记录
        /// </summary>
        /// <param name="viewName">视图名称</param>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        [OperationContract]
        DataTable FindByView(string viewName, string condition); 
                        
        /// <summary>
        /// 根据条件，从视图里面获取记录
        /// </summary>
        /// <param name="viewName">视图名称</param>
        /// <param name="condition">查询条件</param>
        /// <param name="sortField">排序字段</param>
        /// <param name="isDescending">是否为降序</param>
        /// <returns></returns>
        [OperationContract]
        DataTable FindByView2(string viewName, string condition, string sortField, bool isDescending);
                        
        /// <summary>
        /// 根据条件，从视图里面获取记录
        /// </summary>
        /// <param name="viewName">视图名称</param>
        /// <param name="condition">查询条件</param>
        /// <param name="sortField">排序字段</param>
        /// <param name="isDescending">是否为降序</param>
        /// <param name="info">分页条件</param>
        /// <returns></returns>
        [OperationContract]
        DataTable FindByViewWithPager(string viewName, string condition, string sortField, bool isDescending, PagerInfo info);

        #endregion

        #region 基础接口

        /// <summary>
        /// 获取表的所有记录数量
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        int GetRecordCount();

        /// <summary>
        /// 获取表的所有记录数量
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        int GetRecordCount2(string condition);

        /// <summary>
        /// 根据condition条件，判断是否存在记录
        /// </summary>
        /// <param name="condition">查询的条件</param>
        /// <returns>如果存在返回True，否则False</returns>
        [OperationContract]
        bool IsExistRecord(string condition);
        
        /// <summary>
        /// 查询数据库,检查是否存在指定键值的对象
        /// </summary>
        /// <param name="fieldName">指定的属性名</param>
        /// <param name="key">指定的值</param>
        /// <returns>存在则返回<c>true</c>，否则为<c>false</c>。</returns>
        [OperationContract]
        bool IsExistKey(string fieldName, object key);

        /// <summary>
        /// 根据指定对象的ID,从数据库中删除指定对象(用于字符主键)
        /// </summary>
        /// <param name="key">指定对象的ID</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        [OperationContract]
        bool DeleteByUser(string key, Int32 userId);

        /// <summary>
        /// 根据指定对象的ID,从数据库中删除指定对象(用于整型主键)
        /// </summary>
        /// <param name="key">指定对象的ID</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        [OperationContract]
        bool DeleteByUser2(int key, Int32 userId);

        /// <summary>
        /// 根据指定对象的ID,从数据库中删除指定对象(用于字符主键)
        /// </summary>
        /// <param name="condition">指定的删除条件</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        [OperationContract]
        bool DeleteByCondition(string condition);

        #endregion

        #region 辅助型接口
                        
        /// <summary>
        /// 获取字段列表
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        /// <returns></returns>
        [OperationContract]
        List<string> GetFieldList(string fieldName);

        /// <summary>
        /// 根据条件，获取某字段数据字典列表
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        /// <param name="condition">查询的条件</param>
        /// <returns></returns>
        [OperationContract]
        List<string> GetFieldListByCondition(string fieldName, string condition);

        /// <summary>
        /// 获取表的字段名称和数据类型列表
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        DataTable GetFieldTypeList();

        /// <summary>
        /// 获取字段中文别名（用于界面显示）的字典集合
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        Dictionary<string, string> GetColumnNameAlias();

        /// <summary>
        /// 获取指定字段的报表数据
        /// </summary>
        /// <param name="fieldName">表字段</param>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetReportData(string fieldName, string condition); 
        #endregion
    }
}
