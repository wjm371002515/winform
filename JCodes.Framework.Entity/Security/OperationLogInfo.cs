using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 用户关键操作记录(OperationLogInfo)
	/// 对象号: 100059
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class OperationLogInfo : BaseEntity
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
		/// 登录名
		/// </summary>
		private String m_LoginName = string.Empty;

		/// <summary>
		/// 真实名
		/// </summary>
		private String m_FullName = string.Empty;

		/// <summary>
		/// 公司Id
		/// </summary>
		private Int32 m_CompanyId = 0;

		/// <summary>
		/// 公司名字
		/// </summary>
		private String m_CompanyName = string.Empty;

		/// <summary>
		/// 表名
		/// </summary>
		private String m_TableName = string.Empty;

		/// <summary>
		/// 操作类型
		/// </summary>
		private String m_OperationType = string.Empty;

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
		/// 创建时间
		/// </summary>
		private DateTime m_CreatorTime = DateTime.Now;
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
		/// 真实名
		/// </summary>
		[DataMember]
		[DisplayName("真实名")]
		public virtual String FullName
		{
			get
			{
				return this.m_FullName;
			}
			set
			{
				this.m_FullName = value;
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
		/// 公司名字
		/// </summary>
		[DataMember]
		[DisplayName("公司名字")]
		public virtual String CompanyName
		{
			get
			{
				return this.m_CompanyName;
			}
			set
			{
				this.m_CompanyName = value;
			}
		}

		/// <summary>
		/// 表名
		/// </summary>
		[DataMember]
		[DisplayName("表名")]
		public virtual String TableName
		{
			get
			{
				return this.m_TableName;
			}
			set
			{
				this.m_TableName = value;
			}
		}

		/// <summary>
		/// 操作类型
		/// </summary>
		[DataMember]
		[DisplayName("操作类型")]
		public virtual String OperationType
		{
			get
			{
				return this.m_OperationType;
			}
			set
			{
				this.m_OperationType = value;
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
		/// 创建时间
		/// </summary>
		[DataMember]
		[DisplayName("创建时间")]
		public virtual DateTime CreatorTime
		{
			get
			{
				return this.m_CreatorTime;
			}
			set
			{
				this.m_CreatorTime = value;
			}
		}
		#endregion
	}
}