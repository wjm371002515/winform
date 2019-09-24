using System;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Collections.Generic;
namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 部门机构节点对象(OUNodeInfo)
	/// 对象号: 100069
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class OUNodeInfo : OUInfo
	{
		#region Field Members

		/// <summary>
		/// 子节点
		/// </summary>
		private List<OUNodeInfo> m_Children = new List<OUNodeInfo>();
		#endregion

		#region Property Members

		/// <summary>
		/// 子节点
		/// </summary>
		[DataMember]
		public virtual List<OUNodeInfo> Children
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
		public  OUNodeInfo()
		{
			this.m_Children = new List<OUNodeInfo>();
		}

		/// <summary>
		/// 带参构造函数
		/// </summary>
		public  OUNodeInfo(OUInfo info)
		{
			base.Id = info.Id;
            base.Pid = info.Pid;
            base.OuCode = info.OuCode;
            base.Name = info.Name;
            base.Seq = info.Seq;
            base.OuType = info.OuType;
            base.Address = info.Address;
            base.OutPhone = info.OutPhone;
            base.InnerPhone = info.InnerPhone;
            base.Remark = info.Remark;
            base.CreatorId = info.CreatorId;
            base.CreatorTime = info.CreatorTime;
            base.EditorId = info.EditorId;
            base.LastUpdateTime = info.LastUpdateTime;
            base.IsDelete = info.IsDelete;
            base.IsForbid = info.IsForbid;
            base.CompanyId = info.CompanyId;
		}
		#endregion
	}
}