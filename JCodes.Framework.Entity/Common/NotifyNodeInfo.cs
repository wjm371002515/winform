using System;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Collections.Generic;
namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 用于各个模块传递的分组联系人信息节点(NotifyNodeInfo)
	/// 对象号: 100029
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class NotifyNodeInfo : NotifyGroupInfo
	{
		#region Field Members

		/// <summary>
		/// 子分组实体类对象集合
		/// </summary>
		private List<NotifyNodeInfo> m_Children = new List<NotifyNodeInfo>();
		#endregion

		#region Property Members

		/// <summary>
		/// 子分组实体类对象集合
		/// </summary>
		[DataMember]
		public virtual List<NotifyNodeInfo> Children
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
		public  NotifyNodeInfo()
		{
			this.m_Children = new List<NotifyNodeInfo>();
		}

		/// <summary>
		/// 带参构造函数
		/// </summary>
		public  NotifyNodeInfo(NotifyGroupInfo info)
		{
			base.NotifyCategoryName = info.NotifyCategoryName;
            base.ContactList = info.ContactList;
		}
		#endregion
	}
}