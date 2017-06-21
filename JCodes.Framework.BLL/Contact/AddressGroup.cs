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
    /// 通讯录分组
    /// </summary>
	public class AddressGroup : BaseBLL<AddressGroupInfo>
    {
        public AddressGroup() : base()
        {
            base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
        }

        /// <summary>
        /// 根据用户，获取树形结构的分组列表
        /// </summary>
        public List<AddressGroupNodeInfo> GetTree(string addressType, string creator = null)
        {
            IAddressGroup dal = baseDal as IAddressGroup;
            return dal.GetTree(addressType, creator);
        }

        /// <summary>
        /// 根据联系人ID，获取客户对应的分组集合
        /// </summary>
        /// <param name="contactId">联系人ID</param>
        /// <returns></returns>
        public List<AddressGroupInfo> GetByContact(string contactId)
        {
            IAddressGroup dal = baseDal as IAddressGroup;
            return dal.GetByContact(contactId);
        }

        /// <summary>
        /// 根据通讯录类型和所属用户，获取对应用户的分组集合
        /// </summary>
        /// <param name="addressType">通讯录类型，个人还是公共</param>
        /// <param name="creator">用户标识（用户登陆名称），如果是公共通讯录，可以为空</param>
        /// <returns></returns>
        public List<AddressGroupInfo> GetAllWithAddressType(AddressType addressType, string creator = null)
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
