using System;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Collections.Generic;
namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 联系人组别节点(ContactGroupNodeInfo)
	/// 对象号: 100013
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class ContactGroupNodeInfo : ContactGroupInfo
	{
		#region Field Members

		/// <summary>
		/// 子分组实体类对象集合
		/// </summary>
		private List<ContactGroupNodeInfo> m_Children = new List<ContactGroupNodeInfo>();
		#endregion

		#region Property Members

		/// <summary>
		/// 子分组实体类对象集合
		/// </summary>
		[DataMember]
		public virtual List<ContactGroupNodeInfo> Children
		{
			get
			{
				return this.m_Children;
			}
			set
			{
				this.m_Children = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public  ContactGroupNodeInfo()
		{
			this.m_Children = new List<ContactGroupNodeInfo>();
		}

		/// <summary>
		/// 
		/// </summary>
		public  ContactGroupNodeInfo(ContactGroupInfo info)
		{
			  base.Id = info.Id;
            base.UserCode = info.UserCode;
            base.Name = info.Name;
            base.Pid = info.Pid;
            base.Remark = info.Remark;
            base.EditorId = info.EditorId;
            base.LastUpdateTime = info.LastUpdateTime;
            base.CreatorId = info.CreatorId;
            base.CreatorTime = info.CreatorTime;
            base.DeptId = info.DeptId;
            base.CompanyId = info.CompanyId;
		}
		#endregion
	}
}