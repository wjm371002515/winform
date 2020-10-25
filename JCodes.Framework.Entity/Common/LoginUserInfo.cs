using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 登陆用户的基础信息(LoginUserInfo)
	/// 对象号: 100094
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class LoginUserInfo
	{
		#region Field Members

		/// <summary>
		/// ID序号
		/// </summary>
		private Int32 m_Id = 0;

		/// <summary>
		/// 部门Id
		/// </summary>
		private Int32 m_DeptId = 0;

		/// <summary>
		/// 公司Id
		/// </summary>
		private Int32 m_CompanyId = 0;

		/// <summary>
		/// 名称
		/// </summary>
		private String m_Name = string.Empty;

		/// <summary>
		/// 登录名
		/// </summary>
		private String m_LoginName = string.Empty;

		/// <summary>
		/// 性别
		/// </summary>
		private Int16 m_Gender = 0;

		/// <summary>
		/// 身份证
		/// </summary>
		private String m_IdCard = string.Empty;

		/// <summary>
		/// 手机
		/// </summary>
		private String m_MobilePhone = string.Empty;

		/// <summary>
		/// Email邮箱
		/// </summary>
		private String m_Email = string.Empty;

		/// <summary>
		/// QQ号
		/// </summary>
		private Int32 m_QQ = 0;
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
		/// 部门Id
		/// </summary>
		[DataMember]
		[DisplayName("部门Id")]
		public virtual Int32 DeptId
		{
			get
			{
				return this.m_DeptId;
			}
			set
			{
				this.m_DeptId = value;
			}
		}

		/// <summary>
		/// 公司Id
		/// </summary>
		[DataMember]
		[DisplayName("公司Id")]
		public virtual Int32 CompanyId
		{
			get
			{
				return this.m_CompanyId;
			}
			set
			{
				this.m_CompanyId = value;
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
		/// 性别
		/// 1-男,
		/// 2-女,
		/// 2-保密
		/// </summary>
		[DataMember]
		[DisplayName("性别")]
		public virtual Int16 Gender
		{
			get
			{
				return this.m_Gender;
			}
			set
			{
				this.m_Gender = value;
			}
		}

		/// <summary>
		/// 身份证
		/// </summary>
		[DataMember]
		[DisplayName("身份证")]
		public virtual String IdCard
		{
			get
			{
				return this.m_IdCard;
			}
			set
			{
				this.m_IdCard = value;
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
		/// QQ号
		/// </summary>
		[DataMember]
		[DisplayName("QQ号")]
		public virtual Int32 QQ
		{
			get
			{
				return this.m_QQ;
			}
			set
			{
				this.m_QQ = value;
			}
		}
		#endregion
	}
}