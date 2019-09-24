using System;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Collections.Generic;
namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 用于各个模块传递的联系信息(NotifyContactInfo)
	/// 对象号: 100027
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class NotifyContactInfo
	{
		#region Field Members

		/// <summary>
		/// ID序号
		/// </summary>
		private Int32 m_Id = 0;

		/// <summary>
		/// 联系人
		/// </summary>
		private String m_Contacts = string.Empty;

		/// <summary>
		/// 手机
		/// </summary>
		private String m_MobilePhone = string.Empty;

		/// <summary>
		/// Email邮箱
		/// </summary>
		private String m_Email = string.Empty;

		/// <summary>
		/// 备注
		/// </summary>
		private String m_Remark = string.Empty;
		#endregion

		#region Property Members

		/// <summary>
		/// ID序号
		/// </summary>
		[DataMember]
		[DisplayName("ID序号")]
		public virtual Int32 Id
		{
			get
			{
				return this.m_Id;
			}
			set
			{
				this.m_Id = value;
			}
		}

		/// <summary>
		/// 联系人
		/// </summary>
		[DataMember]
		[DisplayName("联系人")]
		public virtual String Contacts
		{
			get
			{
				return this.m_Contacts;
			}
			set
			{
				this.m_Contacts = value;
			}
		}

		/// <summary>
		/// 手机
		/// </summary>
		[DataMember]
		[DisplayName("手机")]
		public virtual String MobilePhone
		{
			get
			{
				return this.m_MobilePhone;
			}
			set
			{
				this.m_MobilePhone = value;
			}
		}

		/// <summary>
		/// Email邮箱
		/// </summary>
		[DataMember]
		[DisplayName("Email邮箱")]
		public virtual String Email
		{
			get
			{
				return this.m_Email;
			}
			set
			{
				this.m_Email = value;
			}
		}

		/// <summary>
		/// 备注
		/// </summary>
		[DataMember]
		[DisplayName("备注")]
		public virtual String Remark
		{
			get
			{
				return this.m_Remark;
			}
			set
			{
				this.m_Remark = value;
			}
		}

		/// <summary>
		/// 带参构造函数
		/// </summary>
		public  NotifyContactInfo(Int32 id, string contactName, string mobile, string email = "", string remark = "")
		{
			this.Id = id;
            this.Contacts = contactName;
            this.MobilePhone = mobile;
            this.Email = email;
            this.Remark = remark;
		}
		#endregion
	}
}