using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 用户登录日志信息(LoginLogInfo)
	/// 对象号: 100057
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class LoginLogInfo : BaseEntity
	{
		#region Field Members

		/// <summary>
		/// ID序号
		/// </summary>
		private Int32 m_Id = 0;

		/// <summary>
		/// 用户Id
		/// </summary>
		private Int32 m_UserId = 0;

		/// <summary>
		/// 名称
		/// </summary>
		private String m_Name = string.Empty;

		/// <summary>
		/// 登录名
		/// </summary>
		private String m_LoginName = string.Empty;

		/// <summary>
		/// 公司Id
		/// </summary>
		private Int32 m_CompanyId = 0;

		/// <summary>
		/// 备注
		/// </summary>
		private String m_Remark = string.Empty;

		/// <summary>
		/// IP地址
		/// </summary>
		private String m_IP = string.Empty;

		/// <summary>
		/// Mac地址
		/// </summary>
		private String m_Mac = string.Empty;

		/// <summary>
		/// 最后更新时间
		/// </summary>
		private DateTime m_LastUpdateTime = DateTime.Now;

		/// <summary>
		/// 系统编号
		/// </summary>
		private String m_SystemtypeId = string.Empty;
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
		/// 用户Id
		/// </summary>
		[DataMember]
		[DisplayName("用户Id")]
		public virtual Int32 UserId
		{
			get
			{
				return this.m_UserId;
			}
			set
			{
				this.m_UserId = value;
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
		/// IP地址
		/// </summary>
		[DataMember]
		[DisplayName("IP地址")]
		public virtual String IP
		{
			get
			{
				return this.m_IP;
			}
			set
			{
				this.m_IP = value;
			}
		}

		/// <summary>
		/// Mac地址
		/// </summary>
		[DataMember]
		[DisplayName("Mac地址")]
		public virtual String Mac
		{
			get
			{
				return this.m_Mac;
			}
			set
			{
				this.m_Mac = value;
			}
		}

		/// <summary>
		/// 最后更新时间
		/// </summary>
		[DataMember]
		[DisplayName("最后更新时间")]
		public virtual DateTime LastUpdateTime
		{
			get
			{
				return this.m_LastUpdateTime;
			}
			set
			{
				this.m_LastUpdateTime = value;
			}
		}

		/// <summary>
		/// 系统编号
		/// </summary>
		[DataMember]
		[DisplayName("系统编号")]
		public virtual String SystemtypeId
		{
			get
			{
				return this.m_SystemtypeId;
			}
			set
			{
				this.m_SystemtypeId = value;
			}
		}
		#endregion
	}
}