using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Entity;

namespace JCodes.Framework.IDAL
{
    /// <summary>
    /// 通讯录联系人
    /// </summary>
	public interface IAddress : IBaseDAL<AddressInfo>
	{
        /// <summary>
        /// 根据联系人分组的名称，搜索属于该分组的联系人列表
        /// </summary>
        /// <param name="ownerUser">联系人所属用户</param>
        /// <param name="groupName">联系人分组的名称,如果联系人分组为空，那么返回未分组联系人列表</param>
        /// <param name="pagerInfo">分页条件</param>
        /// <returns></returns>
        List<AddressInfo> FindByGroupName(string ownerUser, string groupName, PagerInfo pagerInfo = null);
                      
        /// <summary>
        /// 获取公共分组的联系人列表。根据联系人分组的名称，搜索属于该分组的联系人列表。
        /// </summary>
        /// <param name="groupName">联系人分组的名称,如果联系人分组为空，那么返回未分组联系人列表</param>
        /// <param name="pagerInfo">分页条件</param>
        /// <returns></returns>
        List<AddressInfo> FindByGroupNamePublic(string groupName, PagerInfo pagerInfo = null);

        /// <summary>
        /// 获取联系人的名称
        /// </summary>
        /// <param name="id">联系人ID</param>
        /// <param name="trans">事务对象</param>
        /// <returns></returns>
        string GetAddressName(string id, DbTransaction trans = null);

        /// <summary>
        /// 调整联系人的组别
        /// </summary>
        /// <param name="contactId">联系人ID</param>
        /// <param name="groupIdList">分组Id集合</param>
        /// <returns></returns>
        bool ModifyAddressGroup(string contactId, List<string> groupIdList);
    }
}