using System;
using System.Data.Common;
using System.Collections.Generic;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Entity;
using JCodes.Framework.jCodesenum;

namespace JCodes.Framework.IDAL
{
    /// <summary>
    /// 客户基本资料
    /// </summary>
    public interface ICustomer : IBaseDAL<CustomerInfo>
    {                
        /// <summary>
        /// 修改客户的所属人员/创建人员
        /// </summary>
        /// <param name="id">客户ID</param>
        /// <param name="userId">所属人员ID</param>
        /// <returns></returns>
        bool ChangeOwner(string id, string userId);     

        /// <summary>
        /// 设置删除标志
        /// </summary>
        /// <param name="id">客户ID</param>
        /// <param name="deleted">是否删除</param>
        /// <param name="trans">事务对象</param>
        /// <returns></returns>
        bool SetDeletedFlag(Int32 id, IsDelete isDelete = IsDelete.是, DbTransaction trans = null);

        /// <summary>
        /// 根据客户分组的名称，搜索属于该分组的客户列表
        /// </summary>
        /// <param name="ownerUser">客户所属用户</param>
        /// <param name="groupId">客户分组的名称,如果客户分组为空，那么返回未分组客户列表</param>
        /// <param name="pagerInfo">分页条件</param>
        /// <returns></returns>
        List<CustomerInfo> FindByGroupName(string ownerUser, string groupName, PagerInfo pagerInfo = null);

        /// <summary>
        /// 调整客户的组别
        /// </summary>
        /// <param name="customerId">客户ID</param>
        /// <param name="groupIdList">客户分组Id集合</param>
        /// <returns></returns>
        bool ModifyCustomerGroup(string customerId, List<string> groupIdList);

        /// <summary>
        /// 更新客户的状态信息
        /// </summary>
        /// <param name="id">客户Id</param>
        /// <param name="orderDate">订单日期</param>
        /// <param name="orderCount">交易次数</param>
        /// <param name="orderMoney">交易金额</param>
        /// <returns></returns>
        bool UpdateTransactionStatus(string id, DateTime orderDate, int orderCount, decimal orderMoney, DbTransaction trans = null);
                       
        /// <summary>
        /// 更新客户的最后联系日期
        /// </summary>
        /// <param name="id">客户ID</param>
        /// <param name="lastContactDate">最后联系日期</param>
        /// <returns></returns>
        bool UpdateContactDate(string id, DateTime lastContactDate, DbTransaction trans = null);

            
        /// <summary>
        /// 根据供应商ID，分页获取客户列表（关联医院列表）
        /// </summary>
        /// <param name="supplierID">供应商ID</param>
        /// <param name="pagerInfo">分页条件</param>
        /// <returns></returns>
        List<CustomerInfo> FindBySupplier(string supplierID, string condition, PagerInfo pagerInfo = null);

        /// <summary>
        /// 如果没有建立关系，则创建供应商和客户关系
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <param name="supplierID">供应商ID</param>
        /// <returns></returns>
        bool AddSupplier(string customerID, string supplierID);

        /// <summary>
        /// 如果建立关系，则移除供应商和客户关系
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <param name="supplierID">供应商ID</param>
        /// <returns></returns>
        bool RemoveSupplier(string customerID, string supplierID);
               
        /// <summary>
        /// 如果建立关系，则移除供应商和客户的所有关系
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <returns></returns>
        bool RemoveSupplier(string customerID);
                
        /// <summary>
        /// 如果没有建立关系，则创建联系人和客户关系
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <param name="contactID">联系人ID</param>
        /// <returns></returns>
        bool AddContact(string customerID, string contactID);

        /// <summary>
        /// 如果建立关系，则移除联系人和客户关系
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <param name="contactID">联系人ID</param>
        /// <returns></returns>
        bool RemoveContact(string customerID, string contactID);
                        
        /// <summary>
        /// 如果建立关系，则移除联系人和客户的所有关系
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <returns></returns>
        bool RemoveContact(string customerID);
    }
}