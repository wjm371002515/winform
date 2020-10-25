using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using JCodes.Framework.Entity;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.IDAL;
using JCodes.Framework.Common.Office;
using JCodes.Framework.jCodesenum;

namespace JCodes.Framework.BLL
{
    /// <summary>
    /// 客户基本资料
    /// </summary>
	public class Customer : BaseBLL<CustomerInfo>
    {
        public Customer() : base()
        {
            base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
            baseDal.OnOperationLog += new OperationLogEventHandler(OperationLog.OnOperationLog);//如果需要记录操作日志，则实现这个事件
        }

        /// <summary>
        /// 修改客户的所属人员/创建人员
        /// </summary>
        /// <param name="id">客户ID</param>
        /// <param name="userId">所属人员ID</param>
        /// <returns></returns>
        public bool ChangeOwner(string id, string userId)
        {
            ICustomer dal = baseDal as ICustomer;
            return dal.ChangeOwner(id, userId);
        }

        /// <summary>
        /// 根据客户分组的名称，搜索属于该分组的客户列表
        /// </summary>
        /// <param name="ownerUser">客户所属用户</param>
        /// <param name="groupName">客户分组的名称,如果客户分组为空，那么返回未分组客户列表</param>
        /// <param name="pagerInfo">分页条件</param>
        /// <returns></returns>
        public List<CustomerInfo> FindByGroupName(string ownerUser, string groupName, PagerInfo pagerInfo = null)
        {
            ICustomer dal = baseDal as ICustomer;
            return dal.FindByGroupName(ownerUser, groupName, pagerInfo);
        }

        /// <summary>
        /// 设置删除标志
        /// </summary>
        /// <param name="id">客户ID</param>
        /// <param name="deleted">是否删除</param>
        /// <param name="trans">事务对象</param>
        /// <returns></returns>
        public bool SetDeletedFlag(Int32 id, IsDelete isDelete = IsDelete.是, DbTransaction trans = null)
        {
            ICustomer dal = baseDal as ICustomer;
            return dal.SetDeletedFlag(id, isDelete, trans);
        }

        /// <summary>
        /// 获取客户的名称
        /// </summary>
        /// <param name="id">客户ID</param>
        /// <returns></returns>
        public string GetCustomerNameById(Int32 id, DbTransaction trans = null)
        {
            //使用缓存减轻数据库查询压力
            System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
            string key = string.Format("{0}-{1}-{2}", method.DeclaringType.FullName, method.Name, id);
            string name = MemoryCacheHelper.GetCacheItem<string>(key, delegate()
            {
                return GetFieldValue(id, "Name", trans);
            },
            new TimeSpan(0, 30, 0));//30分钟后过期
            return name;
        }
                      
        /// <summary>
        /// 调整客户的组别
        /// </summary>
        /// <param name="customerId">客户ID</param>
        /// <param name="groupIdList">客户分组Id集合</param>
        /// <returns></returns>
        public bool ModifyCustomerGroup(string customerId, List<string> groupIdList)
        {
            ICustomer dal = baseDal as ICustomer;
            return dal.ModifyCustomerGroup(customerId, groupIdList);
        }

        /// <summary>
        /// 获取客户的省份列表
        /// </summary>
        /// <returns></returns>
        public List<string> GetCustomersProvince()
        {
            List<string> list = GetFieldList("Province");
            return list;
        }

        /// <summary>
        /// 获取客户的城市列表
        /// </summary>
        /// <returns></returns>
        public List<string> GetCustomersCity()
        {
            List<string> list = GetFieldList("City");
            return list;
        }

        /// <summary>
        /// 根据客户名称获取客户信息
        /// </summary>
        /// <param name="customerName">客户名称</param>
        /// <returns></returns>
        public CustomerInfo FindByName(string customerName)
        {
            string condition = string.Format("Name='{0}' ", customerName);
            return FindSingle(condition);
        }

        /// <summary>
        /// 给SearchLookup控件提供数据源，返回部分客户字段信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllForLookup()
        {
            string sql = string.Format("Select ID,Name,SimpleName,UserCode from T_CRM_Customer order by UserCode");
            return baseDal.SqlTable(sql);
        }

        /// <summary>
        /// 更新客户的状态信息
        /// </summary>
        /// <param name="id">客户Id</param>
        /// <param name="orderDate">订单日期</param>
        /// <param name="orderCount">交易次数</param>
        /// <param name="orderMoney">交易金额</param>
        /// <returns></returns>
        public bool UpdateTransactionStatus(string id, DateTime orderDate, int orderCount, decimal orderMoney, DbTransaction trans = null)
        {
            ICustomer dal = baseDal as ICustomer;
            return dal.UpdateTransactionStatus(id, orderDate, orderCount, orderMoney, trans);
        }

        /// <summary>
        /// 更新客户的最后联系日期
        /// </summary>
        /// <param name="id">客户ID</param>
        /// <param name="lastContactDate">最后联系日期</param>
        /// <returns></returns>
        public bool UpdateContactDate(string id, DateTime lastContactDate, DbTransaction trans = null)
        {
            ICustomer dal = baseDal as ICustomer;
            return dal.UpdateContactDate(id, lastContactDate, trans);
        }
                        
        /// <summary>
        /// 根据供应商ID，分页获取客户列表（关联客户列表）
        /// </summary>
        /// <param name="supplierID">供应商ID</param>
        /// <param name="pagerInfo">分页条件</param>
        /// <returns></returns>
        public List<CustomerInfo> FindBySupplier(string supplierID, string condition, PagerInfo pagerInfo = null)
        {
            ICustomer dal = baseDal as ICustomer;
            return dal.FindBySupplier(supplierID, condition, pagerInfo);
        }

        /// <summary>
        /// 如果没有建立关系，则创建供应商和客户关系
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <param name="supplierID">供应商ID</param>
        /// <returns></returns>
        public bool AddSupplier(string customerID, string supplierID)
        {
            ICustomer dal = baseDal as ICustomer;
            return dal.AddSupplier(customerID, supplierID);
        }

        /// <summary>
        /// 如果建立关系，则移除供应商和客户关系
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <param name="supplierID">供应商ID</param>
        /// <returns></returns>
        public bool RemoveSupplier(string customerID, string supplierID)
        {
            ICustomer dal = baseDal as ICustomer;
            return dal.RemoveSupplier(customerID, supplierID);
        }
                
        /// <summary>
        /// 如果建立关系，则移除供应商和客户的所有关系
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <returns></returns>
        public bool RemoveSupplier(string customerID)
        {
            ICustomer dal = baseDal as ICustomer;
            return dal.RemoveContact(customerID);
        }     
                   
        /// <summary>
        /// 如果没有建立关系，则创建联系人和客户关系
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <param name="contactID">联系人ID</param>
        /// <returns></returns>
        public bool AddContact(string customerID, string contactID)
        {
            ICustomer dal = baseDal as ICustomer;
            return dal.AddContact(customerID, contactID);
        }

        /// <summary>
        /// 如果建立关系，则移除联系人和客户关系
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <param name="contactID">联系人ID</param>
        /// <returns></returns>
        public bool RemoveContact(string customerID, string contactID)
        {
            ICustomer dal = baseDal as ICustomer;
            return dal.RemoveContact(customerID, contactID);
        }
        /// <summary>
        /// 如果建立关系，则移除联系人和客户的所有关系
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <returns></returns>
        public bool RemoveContact(string customerID)
        {
            ICustomer dal = baseDal as ICustomer;
            return dal.RemoveContact(customerID);
        }
    }
}
