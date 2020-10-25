using System.Data.Common;
using System.Collections.Generic;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Entity;
using JCodes.Framework.jCodesenum;
using System;

namespace JCodes.Framework.IDAL
{
    /// <summary>
    /// 客户联系人
    /// </summary>
	public interface IContact : IBaseDAL<ContactInfo>
	{                    
        /// <summary>
        /// 根据联系人分组的名称，搜索属于该分组的联系人列表
        /// </summary>
        /// <param name="ownerUser">联系人所属用户</param>
        /// <param name="groupName">联系人分组的名称,如果联系人分组为空，那么返回未分组联系人列表</param>
        /// <param name="pagerInfo">分页条件</param>
        /// <returns></returns>
        List<ContactInfo> FindByGroupName(string ownerUser, string groupName, PagerInfo pagerInfo = null);

        /// <summary>
        /// 设置删除标志
        /// </summary>
        /// <param name="id">联系人ID</param>
        /// <param name="deleted">是否删除</param>
        /// <param name="trans">事务对象</param>
        /// <returns></returns>
        bool SetDeletedFlag(Int32 id, IsDelete isDelete = IsDelete.是, DbTransaction trans = null);
                        
        /// <summary>
        /// 调整联系人的组别
        /// </summary>
        /// <param name="contactId">联系人ID</param>
        /// <param name="groupIdList">分组Id集合</param>
        /// <returns></returns>
        bool ModifyContactGroup(string contactId, List<string> groupIdList);
                       
        /// <summary>
        /// 根据客户ID获取对应的联系人列表
        /// </summary>
        /// <param name="customerId">客户ID</param>
        /// <returns></returns>
        List<ContactInfo> FindByCustomer(string customerID);

        /// <summary>
        /// 根据客户ID和名称获取实体信息
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <param name="name">名称</param>
        /// <returns></returns>
        ContactInfo FindByCustomerAndName(string customerID, string name);

        /// <summary>
        /// 根据供应商ID获取对应的联系人列表
        /// </summary>
        /// <param name="supplierID">供应商ID</param>
        /// <returns></returns>
        List<ContactInfo> FindBySupplier(string supplierID);

        /// <summary>
        /// 根据供应商ID和名称获取实体信息
        /// </summary>
        /// <param name="supplierID">供应商ID</param>
        /// <param name="name">名称</param>
        /// <returns></returns>
        ContactInfo FindBySupplierAndName(string supplierID, string name);
    }
}