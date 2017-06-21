using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Entity;
using JCodes.Framework.IDAL;

namespace JCodes.Framework.BLL
{
    /// <summary>
    /// 通讯录联系人
    /// </summary>
	public class Address : BaseBLL<AddressInfo>
    {
        public Address() : base()
        {
            base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
        }

        /// <summary>
        /// 根据联系人分组的名称，搜索属于该分组的联系人列表
        /// </summary>
        /// <param name="ownerUser">联系人所属用户</param>
        /// <param name="groupName">联系人分组的名称,如果联系人分组为空，那么返回未分组联系人列表</param>
        /// <param name="pagerInfo">分页条件</param>
        /// <returns></returns>
        public List<AddressInfo> FindByGroupName(string ownerUser, string groupName, PagerInfo pagerInfo = null)
        {
            IAddress dal = baseDal as IAddress;
            return dal.FindByGroupName(ownerUser, groupName, pagerInfo);
        }
                        
        /// <summary>
        /// 获取公共分组的联系人列表。根据联系人分组的名称，搜索属于该分组的联系人列表。
        /// </summary>
        /// <param name="groupName">联系人分组的名称,如果联系人分组为空，那么返回未分组联系人列表</param>
        /// <param name="pagerInfo">分页条件</param>
        /// <returns></returns>
        public List<AddressInfo> FindByGroupNamePublic(string groupName, PagerInfo pagerInfo = null)
        {
            IAddress dal = baseDal as IAddress;
            return dal.FindByGroupNamePublic(groupName, pagerInfo);
        }

        /// <summary>
        /// 获取联系人的名称
        /// </summary>
        /// <param name="id">联系人ID</param>
        /// <param name="trans">事务对象</param>
        /// <returns></returns>
        public string GetAddressName(string id, DbTransaction trans = null)
        {
            IAddress dal = baseDal as IAddress;
            return dal.GetAddressName(id, trans);
        }

        /// <summary>
        /// 调整联系人的组别
        /// </summary>
        /// <param name="contactId">联系人ID</param>
        /// <param name="groupIdList">分组Id集合</param>
        /// <returns></returns>
        public bool ModifyAddressGroup(string contactId, List<string> groupIdList)
        {
            IAddress dal = baseDal as IAddress;
            return dal.ModifyAddressGroup(contactId, groupIdList);
        }

        /// <summary>
        /// 根据通讯录类型和所属用户，获取通讯录联系人信息
        /// </summary>
        /// <param name="addressType">通讯录类型，个人还是公共</param>
        /// <param name="creator">用户标识（用户登陆名称），如果是公共通讯录，可以为空</param>
        public List<AddressInfo> GetAllByAddressType(AddressType addressType, string creator = null)
        {
            string condition = "";
            if (addressType == AddressType.个人)
            {
                condition = string.Format("AddressType = '{0}' and creator='{1}'", addressType.ToString(), creator);
            }
            else
            {
                condition = string.Format("AddressType = '{0}'", addressType.ToString());
            }

            return Find(condition);
        }

    }
}
