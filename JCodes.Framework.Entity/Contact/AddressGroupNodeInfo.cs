using System;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Collections.Generic;
namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 通讯录分组节点信息(AddressGroupNodeInfo)
	/// 对象号: 100022
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class AddressGroupNodeInfo : AddressGroupInfo
	{
		#region Field Members

		/// <summary>
		/// 子分组实体类对象集合
		/// </summary>
		private List<AddressGroupNodeInfo> m_Children = new List<AddressGroupNodeInfo>();
		#endregion

		#region Property Members

		/// <summary>
		/// 子分组实体类对象集合
		/// </summary>
		[DataMember]
		public virtual List<AddressGroupNodeInfo> Children
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
		/// 无参构造函数
		/// </summary>
		public  AddressGroupNodeInfo()
		{
			this.m_Children = new List<AddressGroupNodeInfo>();
		}

		/// <summary>
		/// 带参构造函数
		/// </summary>
		public  AddressGroupNodeInfo(AddressGroupInfo info)
		{
			base.AddressType = info.AddressType;
            base.Id = info.Id;
            base.Seq = info.Seq;
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