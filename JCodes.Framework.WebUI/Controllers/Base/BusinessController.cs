using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using JCodes.Framework.WebUI.Controllers;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Common.Office;
using JCodes.Framework.Entity;
using JCodes.Framework.Common;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.Common.Databases;
using JCodes.Framework.Common.Format;
using JCodes.Framework.BLL;
using JCodes.Framework.Common.Extension;
using JCodes.Framework.jCodesenum;

namespace JCodes.Framework.WebUI.Controllers
{
    /// <summary>
    /// 本控制器基类专门为访问数据业务对象而设的基类
    /// </summary>
    /// <typeparam name="B">业务对象类型</typeparam>
    /// <typeparam name="T">实体类类型</typeparam>
    public class BusinessController<B, T> : BaseController
        where B : class
        where T : JCodes.Framework.Entity.BaseEntity, new()
    {
        #region 构造函数及常用

        /// <summary>
        /// 业务对象所在程序集的文件名，不包括其扩展名，
        /// </summary>
        protected string bllAssemblyName = null;

        /// <summary>
        /// 基础业务对象
        /// </summary>
        protected BaseBLL<T> baseBLL = null;

        /// <summary>
        /// 错误号缓存
        /// </summary>
        protected Dictionary<String, ErrornoInfo> dicErrInfo = (new JCodes.Framework.Common.ErrorInfo()).GetAllErrorInfo();

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public BusinessController()
        {
            this.bllAssemblyName = System.Reflection.Assembly.GetAssembly(typeof(B)).GetName().Name;
            this.baseBLL = Reflect<BaseBLL<T>>.Create(typeof(B).FullName, bllAssemblyName);//构造对应的 BaseBLL<T> 业务访问层的对象类

            //调用前检查baseBLL是否为空引用
            if (baseBLL == null)
            {
                throw new ArgumentNullException("baseBLL", "未能成功创建对应的BaseBLL<T> 业务访问层的对象。");
            }
        }

        /// <summary>
        /// 默认的视图控制方法
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult Index()
        {
            return View();
        }

        public ActionResult Second()
        {
            return View("Second");
        }

        /// <summary>
        /// Excel数据导入视图控制方法
        /// </summary>
        /// <returns></returns>
        public ActionResult Import()
        {
            return View("Import");
        }

        #endregion

        #region 对象添加、修改、查询接口

        /// <summary>
        /// 在插入数据前对数据的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected virtual void OnBeforeInsert(T info)
        {
            //留给子类对参数对象进行修改
        }

        /// <summary>
        /// 在更新数据前对数据的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected virtual void OnBeforeUpdate(T info)
        {
            //留给子类对参数对象进行修改
        }

        /// <summary>
        /// 插入指定对象到数据库中
        /// </summary>
        /// <param name="info">指定的对象</param>
        /// <returns>执行操作是否成功。</returns>
        public virtual ActionResult Insert(T info)
        {
            //检查用户是否有权限，否则抛出MyDenyAccessException异常
            base.CheckAuthorized(authorizeKeyInfo.InsertKey);

            ReturnResult rr = new ReturnResult();
            if (info != null)
            {
                try
                {
                    OnBeforeInsert(info);
                    if (baseBLL.Insert(info))
                    {
                        rr.ErrorCode = 0;
                        rr.ErrorMessage = "新增记录成功";
                        rr.ErrorPath = "BusinessController->Insert(T info)";
                        rr.LogLevel = (short)LogLevel.LOG_LEVEL_INFO;
                    }
                    else
                    {
                        rr.ErrorCode = 000014;
                        rr.ErrorMessage = dicErrInfo["E000014"].ChineseName;
                        rr.ErrorPath = "BusinessController->Insert(T info)";
                        rr.LogLevel = dicErrInfo["E000014"].LogLevel;
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(BusinessController<B, T>));

                    rr.ErrorCode = 000015;
                    rr.ErrorMessage = dicErrInfo["E000015"].ChineseName;
                    rr.ErrorPath = "BusinessController->Insert(T info)";
                    rr.LogLevel = dicErrInfo["E000015"].LogLevel;
                }
            }
            return ToJsonContent(rr);
        }

        /// <summary>
        /// 插入指定对象到数据库中
        /// </summary>
        /// <param name="info">指定的对象</param>
        /// <returns>执行成功返回新增记录的自增长ID。</returns>
        public virtual ActionResult Insert2(T info)
        {
            ReturnResult rr = new ReturnResult();
            rr.Data1 = "-1";

            //检查用户是否有权限，否则抛出MyDenyAccessException异常
            base.CheckAuthorized(authorizeKeyInfo.InsertKey);

            if (info != null)
            {
                OnBeforeInsert(info);

                rr.ErrorCode = 0;
                rr.ErrorMessage = "新增记录成功";
                rr.ErrorPath = "BusinessController->Insert2(T info)";
                rr.LogLevel = (short)LogLevel.LOG_LEVEL_INFO;
                rr.Data1 = baseBLL.Insert2(info).ToString();
            }

            return ToJsonContent(rr);
        }

        /// <summary>
        /// 更新对象属性到数据库中
        /// </summary>
        /// <param name="info">指定的对象</param>
        /// <param name="id">主键ID的值</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        public virtual ActionResult Update(string Id, FormCollection formValues)
        {
            //检查用户是否有权限，否则抛出MyDenyAccessException异常
            base.CheckAuthorized(authorizeKeyInfo.UpdateKey);

            T obj = baseBLL.FindById(Id);
            if (obj != null)
            {
                //遍历提交过来的数据（可能是实体类的部分属性更新）
                foreach (string key in formValues.Keys)
                {
                    string value = formValues[key];
                    System.Reflection.PropertyInfo propertyInfo = obj.GetType().GetProperty(key);
                    if (propertyInfo != null)
                    {
                        try
                        {
                            // obj对象有key的属性，把对应的属性值赋值给它(从字符串转换为合适的类型）
                            //如果转换失败，会抛出InvalidCastException异常
                            propertyInfo.SetValue(obj, Convert.ChangeType(value, propertyInfo.PropertyType), null);
                        }
                        catch { }
                    }
                }
            }

            ReturnResult rr = new ReturnResult();
            try
            {
                if (Update(Id, obj))
                {
                    rr.ErrorCode = 0;
                    rr.ErrorMessage = "修改记录成功";
                    rr.ErrorPath = "BusinessController->Update(string Id, FormCollection formValues)";
                    rr.LogLevel = (short)LogLevel.LOG_LEVEL_INFO;
                }
                else
                {
                    rr.ErrorCode = 000016;
                    rr.ErrorMessage = dicErrInfo["E000016"].ChineseName;
                    rr.ErrorPath = "BusinessController->Update(string Id, FormCollection formValues)";
                    rr.LogLevel = dicErrInfo["E000016"].LogLevel;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(BusinessController<B, T>));

                rr.ErrorCode = 000017;
                rr.ErrorMessage = dicErrInfo["E000017"].ChineseName;
                rr.ErrorPath = "BusinessController->Update(string Id, FormCollection formValues)";
                rr.LogLevel = dicErrInfo["E000017"].LogLevel;
            }

            return ToJsonContent(rr);
        }

        /// <summary>
        /// 更新对象属性到数据库中(方便重写的时候操作)
        /// </summary>
        /// <param name="id">主键ID的值</param>
        /// <param name="info">指定的对象</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        protected virtual bool Update(string Id, T info)
        {
            OnBeforeUpdate(info);
            return baseBLL.Update(info, Id);
        }

        /// <summary>
        /// 插入或更新对象属性到数据库中
        /// </summary>
        /// <param name="info">指定的对象</param>
        /// <param name="id">主键的值</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        public virtual ActionResult InsertUpdate(T info, string Id)
        {
            //检查用户是否有权限，否则抛出MyDenyAccessException异常
            base.CheckAuthorized(authorizeKeyInfo.InsertKey);

            ReturnResult rr = new ReturnResult();
            try
            {
                //同时对写入、更新进行处理
                OnBeforeInsert(info);
                OnBeforeUpdate(info);

                if (baseBLL.InsertUpdate(info, Id))
                {
                    rr.ErrorCode = 0;
                    rr.ErrorMessage = "新增修改记录成功";
                    rr.ErrorPath = "BusinessController->InsertUpdate(T info, string Id)";
                    rr.LogLevel = (short)LogLevel.LOG_LEVEL_INFO;
                }
                else
                {
                    rr.ErrorCode = 000018;
                    rr.ErrorMessage = dicErrInfo["E000018"].ChineseName;
                    rr.ErrorPath = "BusinessController->InsertUpdate(T info, string Id)";
                    rr.LogLevel = dicErrInfo["E000018"].LogLevel;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(BusinessController<B, T>));

                rr.ErrorCode = 000019;
                rr.ErrorMessage = dicErrInfo["E000019"].ChineseName;
                rr.ErrorPath = "BusinessController->InsertUpdate(T info, string Id)";
                rr.LogLevel = dicErrInfo["E000019"].LogLevel;
            }

            return ToJsonContent(rr);
        }

        /// <summary>
        /// 如果不存在记录，则插入对象属性到数据库中
        /// </summary>
        /// <param name="info">指定的对象</param>
        /// <param name="id">主键的值</param>
        /// <returns>执行插入成功返回<c>true</c>，否则为<c>false</c>。</returns>
        public virtual ActionResult InsertIfNew(T info, string Id)
        {
            //检查用户是否有权限，否则抛出MyDenyAccessException异常
            base.CheckAuthorized(authorizeKeyInfo.InsertKey);

            ReturnResult rr = new ReturnResult();
            try
            {
                OnBeforeInsert(info);

                if (baseBLL.InsertIfNew(info, Id))
                {
                    rr.ErrorCode = 0;
                    rr.ErrorMessage = "新增记录成功";
                    rr.ErrorPath = "BusinessController->InsertIfNew(T info, string Id)";
                    rr.LogLevel = (short)LogLevel.LOG_LEVEL_INFO;
                }
                else
                {
                    rr.ErrorCode = 000014;
                    rr.ErrorMessage = dicErrInfo["E000014"].ChineseName;
                    rr.ErrorPath = "BusinessController->InsertIfNew(T info, string Id)";
                    rr.LogLevel = dicErrInfo["E000014"].LogLevel;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(BusinessController<B, T>));

                rr.ErrorCode = 000015;
                rr.ErrorMessage = dicErrInfo["E000015"].ChineseName;
                rr.ErrorPath = "BusinessController->InsertIfNew(T info, string Id)";
                rr.LogLevel = dicErrInfo["E000015"].LogLevel;
            }
            return ToJsonContent(rr);
        }

        /// <summary>
        /// 查询数据库,检查是否存在指定ID的对象
        /// </summary>
        /// <param name="id">对象的ID值</param>
        /// <returns>存在则返回指定的对象,否则返回Null</returns>
        public virtual ActionResult FindById(string Id)
        {
            //检查用户是否有权限，否则抛出MyDenyAccessException异常
            base.CheckAuthorized(authorizeKeyInfo.ViewKey);

            ActionResult rr = Content("");
            T info = baseBLL.FindById(Id);
            if (info != null)
            {
                rr = ToJsonContentDate(info);
            }

            return rr;
        }

        /// <summary>
        /// 根据条件查询数据库,如果存在返回第一个对象
        /// </summary>
        /// <param name="condition">查询的条件</param>
        /// <returns>指定的对象</returns>
        public virtual ActionResult FindSingle(string condition)
        {
            //检查用户是否有权限，否则抛出MyDenyAccessException异常
            base.CheckAuthorized(authorizeKeyInfo.ViewKey);

            ActionResult result = Content("");
            T info = baseBLL.FindSingle(condition);
            if (info != null)
            {
                result = ToJsonContentDate(info);
            }
            return result;
        }

        /// <summary>
        /// 根据条件查询数据库,如果存在返回第一个对象
        /// </summary>
        /// <param name="condition">查询的条件</param>
        /// <param name="orderBy">排序条件</param>
        /// <returns>指定的对象</returns>
        public virtual ActionResult FindSingle2(string condition, string orderBy)
        {
            //检查用户是否有权限，否则抛出MyDenyAccessException异常
            base.CheckAuthorized(authorizeKeyInfo.ViewKey);

            ActionResult result = Content("");
            T info = baseBLL.FindSingle(condition, orderBy);
            if (info != null)
            {
                result = ToJsonContentDate(info);
            }
            return result;
        }

        /// <summary>
        /// 查找记录表中最旧的一条记录
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult FindFirst()
        {
            //检查用户是否有权限，否则抛出MyDenyAccessException异常
            base.CheckAuthorized(authorizeKeyInfo.ViewKey);

            ActionResult result = Content("");
            T info = baseBLL.FindFirst();
            if (info != null)
            {
                result = ToJsonContentDate(info);
            }
            return result;
        }

        /// <summary>
        /// 查找记录表中最新的一条记录
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult FindLast()
        {
            //检查用户是否有权限，否则抛出MyDenyAccessException异常
            base.CheckAuthorized(authorizeKeyInfo.ViewKey);

            ActionResult result = Content("");
            T info = baseBLL.FindLast();
            if (info != null)
            {
                result = ToJsonContentDate(info);
            }
            return result;
        }

        #endregion

        #region 返回集合的接口

        /// <summary>
        /// 根据ID字符串(逗号分隔)获取对象列表
        /// </summary>
        /// <param name="ids">ID字符串(逗号分隔)</param>
        /// <returns>符合条件的对象列表</returns>
        public virtual ActionResult FindByIds(string Ids)
        {
            //检查用户是否有权限，否则抛出MyDenyAccessException异常
            base.CheckAuthorized(authorizeKeyInfo.ListKey);

            ActionResult result = Content("");
            if (!string.IsNullOrEmpty(Ids))
            {
                List<T> list = baseBLL.FindByIds(Ids);
                result = ToJsonContentDate(list);
            }
            return result;
        }

        /// <summary>
        /// 根据条件查询数据库,并返回对象集合
        /// </summary>
        /// <param name="condition">查询的条件</param>
        /// <returns>指定对象的集合</returns>
        public virtual ActionResult Find(string condition)
        {
            //检查用户是否有权限，否则抛出MyDenyAccessException异常
            base.CheckAuthorized(authorizeKeyInfo.ListKey);

            ActionResult result = Content("");
            if (!string.IsNullOrEmpty(condition))
            {
                List<T> list = baseBLL.Find(condition);
                result = ToJsonContentDate(list);
            }
            return result;
        }

        /// <summary>
        /// 根据条件查询数据库,并返回对象集合
        /// </summary>
        /// <param name="condition">查询的条件</param>
        /// <param name="orderBy">排序条件</param>
        /// <returns>指定对象的集合</returns>
        public virtual ActionResult Find2(string condition, string orderBy)
        {
            //检查用户是否有权限，否则抛出MyDenyAccessException异常
            base.CheckAuthorized(authorizeKeyInfo.ListKey);

            ActionResult result = Content("");
            if (!string.IsNullOrEmpty(condition) && !string.IsNullOrEmpty(orderBy))
            {
                List<T> list = baseBLL.Find(condition, orderBy);
                result = ToJsonContentDate(list);
            }
            return result;
        }

        /// <summary>
        /// 根据Request参数获取分页对象数据
        /// </summary>
        /// <returns></returns>
        protected virtual PagerInfo GetPagerInfo()
        {
            int pageIndex = Request["page"] == null ? 1 : int.Parse(Request["page"]);
            int pageSize = Request["rows"] == null ? 10 : int.Parse(Request["rows"]);

            PagerInfo pagerInfo = new PagerInfo();
            pagerInfo.CurrenetPageIndex = pageIndex;
            pagerInfo.PageSize = pageSize;

            return pagerInfo;
        }

        /// <summary>
        /// 获取分页操作的查询条件
        /// </summary>
        /// <returns></returns>
        protected virtual string GetPagerCondition()
        {
            string where = "";

            //增加一个CustomedCondition条件，根据客户这个条件进行查询
            string CustomedCondition = Request["CustomedCondition"] ?? "";
            if (!string.IsNullOrWhiteSpace(CustomedCondition))
            {
                where = CustomedCondition;//直接使用条件
            }
            else
            {
                #region 根据数据库字段列，对所有可能的参数进行获值，然后构建查询条件
                SearchCondition condition = new SearchCondition();
                DataTable dt = baseBLL.GetFieldTypeList();
                foreach (DataRow dr in dt.Rows)
                {
                    string columnName = dr["ColumnName"].ToString();
                    string dataType = dr["DataType"].ToString();

                    //字段增加WHC_前缀字符，避免传递如URL这样的Request关键字冲突
                    string columnValue = Request["WHC_" + columnName] ?? "";
                    //对于数值型，如果是显示声明相等的，一般是外键引用，需要特殊处理
                    bool hasEqualValue = columnValue.StartsWith("=");

                    if (IsDateTime(dataType))
                    {
                        condition.AddDateCondition(columnName, columnValue);
                    }
                    else if (IsNumericType(dataType))
                    {
                        //如果数据库是数值类型，而传入的值是true或者false,那么代表数据库的参考值为1,0，需要进行转换
                        bool boolValue = false;
                        bool isBoolenValue = bool.TryParse(columnValue, out boolValue);
                        if (isBoolenValue)
                        {
                            condition.AddCondition(columnName, boolValue ? 1 : 0, SqlOperator.Equal);
                        }
                        else if (hasEqualValue)
                        {
                            columnValue = columnValue.Substring(columnValue.IndexOf("=") + 1);
                            condition.AddCondition(columnName, columnValue, SqlOperator.Equal);
                        }
                        else
                        {
                            condition.AddNumberCondition(columnName, columnValue);
                        }
                    }
                    else
                    {
                        if (ValidateUtil.IsNumeric(columnValue))
                        {
                            condition.AddCondition(columnName, columnValue, SqlOperator.Equal);
                        }
                        else
                        {
                            condition.AddCondition(columnName, columnValue, SqlOperator.Like);
                        }
                    }
                }
                #endregion

                #region MyRegion
                //string SystemType_ID = Request["SystemType_ID"] ?? "";
                //string Name = Request["Name"] ?? "";
                //string LoginName = Request["LoginName"] ?? "";
                //string Note = Request["Note"] ?? "";
                //string IPAddress = Request["IPAddress"] ?? "";
                //string MacAddress = Request["MacAddress"] ?? "";
                //string LastUpdated = Request["LastUpdated"] ?? "";

                //SearchCondition condition = new SearchCondition();
                //condition.AddCondition("SystemType_ID", SystemType_ID, SqlOperator.Like);
                //condition.AddCondition("Name", Name, SqlOperator.Like);
                //condition.AddCondition("LoginName", LoginName, SqlOperator.Like);
                //condition.AddCondition("Note", Note, SqlOperator.Like);
                //condition.AddCondition("IPAddress", IPAddress, SqlOperator.Like);
                //condition.AddCondition("MacAddress", MacAddress, SqlOperator.Like);

                //condition.AddDateCondition("LastUpdated", LastUpdated); 
                #endregion

                where = condition.BuildConditionSql().Replace("Where", "");
            }

            return where;
        }

        /// <summary>
        /// 判断数据类型是否为数值类型
        /// </summary>
        /// <param name="dataType"></param>
        /// <returns></returns>
        protected bool IsNumericType(string dataType)
        {
            bool result = false;
            if (!string.IsNullOrEmpty(dataType))
            {
                dataType = dataType.ToLower();
                if (dataType.Contains("int") || dataType.Contains("decimal") || dataType.Contains("double") ||
                    dataType.Contains("single") || dataType.Contains("byte") || dataType.Contains("short") ||
                    dataType.Contains("float"))
                {
                    result = true;
                }
            }
            return result;
        }

        /// <summary>
        /// 判断数据类型是否为日期类型
        /// </summary>
        /// <param name="dataType"></param>
        /// <returns></returns>
        protected bool IsDateTime(string dataType)
        {
            bool result = false;
            if (!string.IsNullOrEmpty(dataType))
            {
                dataType = dataType.ToLower();
                if (dataType.ToLower().Contains("datetime"))
                {
                    result = true;
                }
            }
            return result;
        }

        /// <summary>
        /// 根据条件查询数据库,并返回对象集合(用于分页数据显示)
        /// </summary>
        /// <returns>指定对象的集合</returns>
        public virtual ActionResult FindWithPager()
        {
            //检查用户是否有权限，否则抛出MyDenyAccessException异常
            base.CheckAuthorized(authorizeKeyInfo.ListKey);

            string where = GetPagerCondition();
            PagerInfo pagerInfo = GetPagerInfo();
            List<T> list = baseBLL.FindWithPager(where, pagerInfo);

            //Json格式的要求{total:22,rows:{}}
            //构造成Json的格式传递
            var result = new { total = pagerInfo.RecordCount, rows = list };
            return ToJsonContentDate(result);
        }

        /// <summary>
        /// 返回数据库所有的对象集合
        /// </summary>
        /// <returns>指定对象的集合</returns>
        public virtual ActionResult GetAll()
        {
            //检查用户是否有权限，否则抛出MyDenyAccessException异常
            base.CheckAuthorized(authorizeKeyInfo.ListKey);

            List<T> list = baseBLL.GetAll();
            return ToJsonContentDate(list);
        }

        /// <summary>
        /// 返回数据库所有的对象集合
        /// </summary>
        /// <param name="orderBy">自定义排序语句，如Order By Name Desc；如不指定，则使用默认排序</param>
        /// <returns>指定对象的集合</returns>
        public virtual ActionResult GetAll2(string orderBy)
        {
            //检查用户是否有权限，否则抛出MyDenyAccessException异常
            base.CheckAuthorized(authorizeKeyInfo.ListKey);

            ActionResult result = Content("");
            if (!string.IsNullOrEmpty(orderBy))
            {
                List<T> list = baseBLL.GetAll(orderBy);
                result = ToJsonContentDate(list);
            }
            return result;
        }

        /// <summary>
        /// 返回数据库所有的对象集合(用于分页数据显示)
        /// </summary>
        /// <param name="info">分页实体信息</param>
        /// <returns>指定对象的集合</returns>
        public virtual ActionResult GetAllWithPager()
        {
            //检查用户是否有权限，否则抛出MyDenyAccessException异常
            base.CheckAuthorized(authorizeKeyInfo.ListKey);

            PagerInfo pagerInfo = GetPagerInfo();
            List<T> list = baseBLL.GetAll(pagerInfo);

            //Json格式的要求{total:22,rows:{}}
            //构造成Json的格式传递
            var result = new { total = pagerInfo.RecordCount, rows = list };
            return ToJsonContentDate(result);
        }

        /// <summary>
        /// 获取字段列表
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        /// <returns></returns>
        public virtual ActionResult GetFieldList(string fieldName)
        {
            //检查用户是否有权限，否则抛出MyDenyAccessException异常
            base.CheckAuthorized(authorizeKeyInfo.ListKey);

            ActionResult result = Content("");
            if (!string.IsNullOrEmpty(fieldName))
            {
                List<string> list = baseBLL.GetFieldList(fieldName);
                result = Json(list, JsonRequestBehavior.AllowGet);
            }
            return result;
        }

        /// <summary>
        /// 获取字段列表
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        /// <returns></returns>
        public virtual ActionResult GetFieldListByCondition(string fieldName, string condition)
        {
            //检查用户是否有权限，否则抛出MyDenyAccessException异常
            base.CheckAuthorized(authorizeKeyInfo.ListKey);

            ActionResult result = Content("");
            if (!string.IsNullOrEmpty(fieldName))
            {
                List<string> list = baseBLL.GetFieldListByCondition(fieldName, condition);
                result = Json(list, JsonRequestBehavior.AllowGet);
            }
            return result;
        }

        #endregion

        #region 基础接口

        /// <summary>
        /// 获取表的所有记录数量
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult GetRecordCount()
        {
            int result = baseBLL.GetRecordCount();
            return Content(result);
        }

        /// <summary>
        /// 获取表的所有记录数量
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult GetRecordCount2(string condition)
        {
            int result = 0;
            if (!string.IsNullOrEmpty(condition))
            {
                result = baseBLL.GetRecordCount(condition);
            }
            return Content(result);
        }

        /// <summary>
        /// 根据condition条件，判断是否存在记录
        /// </summary>
        /// <param name="condition">查询的条件</param>
        /// <returns>如果存在返回True，否则False</returns>
        public virtual ActionResult IsExistRecord(string condition)
        {
            ReturnResult rr = new ReturnResult();
            try
            {
                if (!string.IsNullOrEmpty(condition))
                {
                    if (baseBLL.IsExistRecord(condition))
                    {
                        rr.ErrorCode = 000000;
                        rr.ErrorMessage = "找到记录";
                        rr.ErrorPath = "BusinessController->IsExistRecord(string condition)";
                        rr.LogLevel = (short)LogLevel.LOG_LEVEL_INFO;
                    }
                    else
                    {
                        rr.ErrorCode = 000020;
                        rr.ErrorMessage = dicErrInfo["E000020"].ChineseName;
                        rr.ErrorPath = "BusinessController->IsExistRecord(string condition)";
                        rr.LogLevel = dicErrInfo["E000020"].LogLevel;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(BusinessController<B, T>));

                rr.ErrorCode = 000021;
                rr.ErrorMessage = dicErrInfo["E000021"].ChineseName;
                rr.ErrorPath = "BusinessController->IsExistRecord(string condition)";
                rr.LogLevel = dicErrInfo["E000021"].LogLevel;
            }

            return ToJsonContent(rr);
        }

        /// <summary>
        /// 查询数据库,检查是否存在指定键值的对象
        /// </summary>
        /// <param name="fieldName">指定的属性名</param>
        /// <param name="key">指定的值</param>
        /// <returns>存在则返回<c>true</c>，否则为<c>false</c>。</returns>
        public virtual ActionResult IsExistKey(string fieldName, string key)
        {
            ReturnResult rr = new ReturnResult();
            try
            {
                if (!string.IsNullOrEmpty(fieldName) && !string.IsNullOrEmpty(key))
                {
                    if (baseBLL.IsExistKey(fieldName, key))
                    {
                        rr.ErrorCode = 000000;
                        rr.ErrorMessage = "找到键值";
                        rr.ErrorPath = "BusinessController->IsExistKey(string fieldName, string key)";
                        rr.LogLevel = (short)LogLevel.LOG_LEVEL_INFO;
                    }
                    else
                    {
                        rr.ErrorCode = 000022;
                        rr.ErrorMessage = dicErrInfo["E000022"].ChineseName;
                        rr.ErrorPath = "BusinessController->IsExistKey(string fieldName, string key)";
                        rr.LogLevel = dicErrInfo["E000022"].LogLevel;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(BusinessController<B, T>));

                rr.ErrorCode = 000023;
                rr.ErrorMessage = dicErrInfo["E000023"].ChineseName;
                rr.ErrorPath = "BusinessController->IsExistKey(string fieldName, string key)";
                rr.LogLevel = dicErrInfo["E000023"].LogLevel;
            }

            return ToJsonContent(rr);
        }

        /// <summary>
        /// 根据指定对象的ID,从数据库中删除指定对象
        /// </summary>
        /// <param name="id">指定对象的ID</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        public virtual ActionResult Delete(string Id)
        {
            //检查用户是否有权限，否则抛出MyDenyAccessException异常
            base.CheckAuthorized(authorizeKeyInfo.DeleteKey);

            ReturnResult rr = new ReturnResult();
            try
            {
                if (!string.IsNullOrEmpty(Id))
                {
                    if (baseBLL.DeleteByUser(Id, CurrentUser.Id))
                    {
                        rr.ErrorCode = 000000;
                        rr.ErrorMessage = "根据用户Id删除记录成功";
                        rr.ErrorPath = "BusinessController->Delete(string Id)";
                        rr.LogLevel = (short)LogLevel.LOG_LEVEL_INFO;
                    }
                    else
                    {
                        rr.ErrorCode = 000024;
                        rr.ErrorMessage = dicErrInfo["E000024"].ChineseName;
                        rr.ErrorPath = "BusinessController->Delete(string Id)";
                        rr.LogLevel = dicErrInfo["E000024"].LogLevel;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(BusinessController<B, T>));

                rr.ErrorCode = 000025;
                rr.ErrorMessage = dicErrInfo["E000025"].ChineseName;
                rr.ErrorPath = "BusinessController->Delete(string Id)";
                rr.LogLevel = dicErrInfo["E000025"].LogLevel;
            }

            return ToJsonContent(rr);
        }

        /// <summary>
        /// 删除多个ID的记录
        /// </summary>
        /// <param name="ids">多个id组合，逗号分开（1,2,3,4,5）</param>
        /// <returns></returns>
        public virtual ActionResult DeleteByIds(string Ids)
        {
            //检查用户是否有权限，否则抛出MyDenyAccessException异常
            base.CheckAuthorized(authorizeKeyInfo.DeleteKey);

            ReturnResult rr = new ReturnResult();
            try
            {
                if (!string.IsNullOrEmpty(Ids))
                {
                    List<string> idArray = Ids.ToDelimitedList<string>(",");
                    foreach (string strId in idArray)
                    {
                        if (!string.IsNullOrEmpty(strId))
                        {
                            baseBLL.DeleteByUser(strId, CurrentUser.Id);
                        }
                    }

                    rr.ErrorCode = 000000;
                    rr.ErrorMessage = "批量根据用户Id删除记录成功";
                    rr.ErrorPath = "BusinessController->DeleteByIds(string Ids)";
                    rr.LogLevel = (short)LogLevel.LOG_LEVEL_INFO;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(BusinessController<B, T>));

                rr.ErrorCode = 000026;
                rr.ErrorMessage = dicErrInfo["E000026"].ChineseName;
                rr.ErrorPath = "BusinessController->DeleteByIds(string Ids)";
                rr.LogLevel = dicErrInfo["E000026"].LogLevel;
            }

            return ToJsonContent(rr);
        }

        /// <summary>
        /// 根据指定条件,从数据库中删除指定对象
        /// </summary>
        /// <param name="condition">删除记录的条件语句</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        public virtual ActionResult DeleteByCondition(string condition)
        {
            //检查用户是否有权限，否则抛出MyDenyAccessException异常
            base.CheckAuthorized(authorizeKeyInfo.DeleteKey);

            ReturnResult rr = new ReturnResult();
            try
            {
                if (!string.IsNullOrEmpty(condition))
                {
                    if (baseBLL.DeleteByCondition(condition))
                    {
                        rr.ErrorCode = 000000;
                        rr.ErrorMessage = "根据条件语句删除记录成功";
                        rr.ErrorPath = "BusinessController->DeleteByCondition(string condition)";
                        rr.LogLevel = (short)LogLevel.LOG_LEVEL_INFO;
                    }
                    else
                    {
                        rr.ErrorCode = 000027;
                        rr.ErrorMessage = dicErrInfo["E000027"].ChineseName;
                        rr.ErrorPath = "BusinessController->DeleteByCondition(string condition)";
                        rr.LogLevel = dicErrInfo["E000027"].LogLevel;
                    }

                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(BusinessController<B, T>));

                rr.ErrorCode = 000028;
                rr.ErrorMessage = dicErrInfo["E000028"].ChineseName;
                rr.ErrorPath = "BusinessController->DeleteByCondition(string condition)";
                rr.LogLevel = dicErrInfo["E000028"].LogLevel;
            }
            return ToJsonContent(rr);
        }

        #endregion

        #region 其他接口

        /// <summary>
        /// 初始化数据库表名
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        public virtual void InitTableName(string tableName)
        {
            baseBLL.InitTableName(tableName);
        }

        /// <summary>
        /// 获取表的字段名称和数据类型列表
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult GetFieldTypeList()
        {
            ActionResult result = Content("");
            DataTable dt = baseBLL.GetFieldTypeList();
            if (dt != null && dt.Rows.Count > 0)
            {
                //构造Json
                List<CListItem> list = new List<CListItem>();
                foreach (DataRow dr in dt.Rows)
                {
                    list.Add(new CListItem(dr[0].ToString(), dr[1].ToString()));
                }
                result = Json(list, JsonRequestBehavior.AllowGet);
            }
            return result;
        }

        /// <summary>
        /// 获取字段中文别名（用于界面显示）的字典集合
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult GetColumnNameAlias()
        {
            ActionResult result = Content("");
            Dictionary<string, string> dict = baseBLL.GetColumnNameAlias();
            if (dict != null && dict.Keys.Count > 0)
            {
                List<CListItem> list = new List<CListItem>();
                foreach (string key in dict.Keys)
                {
                    list.Add(new CListItem(key, dict[key]));
                }
                result = Json(list, JsonRequestBehavior.AllowGet);
            }
            return result;
        }

        /// <summary>
        /// 从附件列表中获取第一个Excel文件，并转换Excel数据为对应的DataTable返回
        /// </summary>
        /// <param name="guid">附件的Guid</param>
        /// <returns></returns>
        protected DataTable ConvertExcelFileToTable(string guid)
        {
            DataTable dt = null;
            if (!string.IsNullOrEmpty(guid))
            {
                // TODO 20210106 
                //获取上传附件的路径
                //string serverRealPath = BLLFactory<FileUpload>.Instance.GetFirstFilePath(guid);
                //if (!string.IsNullOrEmpty(serverRealPath))
                //{
                //    //转换Excel文件到DatTable里面
                //    string error = "";
                //    dt = new DataTable();
                //    AsposeExcelTools.ExcelFileToDataTable(serverRealPath, out dt, out error);
                //}
            }
            return dt;
        }

        #endregion

        #region 用户机构角色相关操作

        //自定义图标的基础路径
        protected const string iconBasePath = "/Content/JqueryEasyUI/themes/icons/customed/";

        /// <summary>
        /// 根据当前用户身份，获取对应的顶级机构管理节点。
        /// 是公司管理员，返回其公司节点
        /// </summary>
        /// <returns></returns>
        protected List<OUInfo> GetMyTopGroup(UserInfo userInfo)
        {
            return BLLFactory<OU>.Instance.GetMyTopGroup(userInfo.Id);
        }

        /// <summary>
        /// 根据当前用户身份，获取对应的顶级机构管理节点。
        /// 超级管理员，返回集团节点；
        /// </summary>
        /// <returns></returns>
        protected List<OUInfo> GetSuperAdminTopGroup(UserInfo userInfo)
        {
            return BLLFactory<OU>.Instance.GetSuperAdminTopGroup(userInfo.Id);
        }

        /// <summary>
        /// 根据机构分类获取对应的图形序号
        /// </summary>
        /// <param name="category">机构分类</param>
        /// <returns></returns>
        protected string GetImageIndex(OuType ouType)
        {
            string result = iconBasePath + "house.png";
            if (ouType == OuType.公司)
            {
                result = iconBasePath + "organ.png";
            }
            else if (ouType == OuType.部门)
            {
                result = iconBasePath + "group.png";
            }
            else if (ouType == OuType.工作组)
            {
                result = iconBasePath + "group.png";
            }
            return result;
        }

        /// <summary>
        /// 根据机构分类获取对应的图形序号
        /// </summary>
        /// <param name="category">机构分类</param>
        /// <returns></returns>
        protected string GetIconcls(OuType ouType)
        {
            string result = "icon-house";
            if (ouType == OuType.公司)
            {
                result = "icon-organ";
            }
            else if (ouType == OuType.部门)
            {
                result = "icon-group";
            }
            else if (ouType == OuType.工作组)
            {
                result = "icon-group";
            }
            return result;
        }

        /// <summary>
        /// 根据机构分类获取对应的图形序号
        /// </summary>
        /// <param name="category">机构分类</param>
        /// <returns></returns>
        protected string GetBootstrapIcon(OuType ouType)
        {
            string result = "fa fa-home icon-state-danger icon-lg";
            if (ouType == OuType.公司)
            {
                result = "fa fa-sitemap icon-state-warning icon-lg";
            }
            else if (ouType == OuType.部门)
            {
                result = "fa fa-users icon-state-info icon-lg";
            }
            else if (ouType == OuType.工作组)
            {
                result = "fa fa-user icon-state-success icon-lg";
            }
            return result;
        }
        #endregion
    }
}
