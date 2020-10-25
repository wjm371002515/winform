using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 可用于传递的用户简单信息(SimpleUserInfo)
	/// 对象号: 100064
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class SimpleUserInfo : BaseEntity
	{
		#region Field Members

		/// <summary>
		/// ID序号
		/// </summary>
		private Int32 m_Id = 0;

		/// <summary>
		/// 用户编码
		/// </summary>
		private String m_UserCode = string.Empty;

		/// <summary>
		/// 名称
		/// </summary>
		private String m_Name = string.Empty;

		/// <summary>
		/// 登录名
		/// </summary>
		private String m_LoginName = string.Empty;

		/// <summary>
		/// 密码
		/// </summary>
		private String m_Password = string.Empty;

		/// <summary>
		/// 手机
		/// </summary>
		private String m_MobilePhone = string.Empty;

		/// <summary>
		/// Email邮箱
		/// </summary>
		private String m_Email = string.Empty;
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
		/// 用户编码
		/// </summary>
		[DataMember]
		[DisplayName("用户编码")]
		public virtual String UserCode
		{
			get
			{
				return this.m_UserCode;
			}
			set
			{
				this.m_UserCode = value;
			}
		}

		/// <summary>
		/// 名称
		/// </summary>
		[DataMember]
		[DisplayName("名称")]
		public virtual String Name
		{
			get
			{
				return this.m_Name;
			}
			set
			{
				this.m_Name = value;
			}
		}

		/// <summary>
		/// 登录名
		/// </summary>
		[DataMember]
		[DisplayName("登录名")]
		public virtual String LoginName
		{
			get
			{
				return this.m_LoginName;
			}
			set
			{
				this.m_LoginName = value;
			}
		}

		/// <summary>
		/// 密码
		/// </summary>
		[DataMember]
		[DisplayName("密码")]
		public virtual String Password
		{
			get
			{
				return this.m_Password;
			}
			set
			{
				this.m_Password = value;
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
		#endregion
	}
}