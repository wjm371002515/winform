using System;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Collections.Generic;
namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 用于各个模块传递的分组联系人信息(NotifyGroupInfo)
	/// 对象号: 100028
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class NotifyGroupInfo
	{
		#region Field Members

		/// <summary>
		/// 通讯录分类名称
		/// </summary>
		private String m_NotifyCategoryName = string.Empty;

		/// <summary>
		/// 联系人列表
		/// </summary>
		private List<NotifyContactInfo> m_ContactList = new List<NotifyContactInfo>();
		#endregion

		#region Property Members

		/// <summary>
		/// 通讯录分类名称
		/// </summary>
		[DataMember]
		[DisplayName("通讯录分类名称")]
		public virtual String NotifyCategoryName
		{
			get
			{
				return this.m_NotifyCategoryName;
			}
			set
			{
				this.m_NotifyCategoryName = value;
			}
		}

		/// <summary>
		/// 联系人列表
		/// </summary>
		[DataMember]
		public virtual List<NotifyContactInfo> ContactList
		{
			get
			{
				return this.m_ContactList;
			}
			set
			{
				this.m_ContactList = value;
			}
		}

		/// <summary>
		/// 无参构造函数
		/// </summary>
		public  NotifyGroupInfo()
		{
			this.ContactList = new List<NotifyContactInfo>();
		}

		#endregion
	}
}