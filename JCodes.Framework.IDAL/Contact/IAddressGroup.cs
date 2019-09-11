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
    /// 通讯录分组
    /// </summary>
	public interface IAddressGroup : IBaseDAL<AddressGroupInfo>
	{
        /// <summary>
        /// 根据用户，获取树形结构的分组列表
        /// </summary>
        List<AddressGroupNodeInfo> GetTree(string addressType, Int32 ?creatorId = null);

        /// <summary>
        /// 根据联系人ID，获取客户对应的分组集合
        /// </summary>
        /// <param name="contactId">联系人ID</param>
        /// <returns></returns>
        List<AddressGroupInfo> GetByContact(Int32 contactId);
    }
}