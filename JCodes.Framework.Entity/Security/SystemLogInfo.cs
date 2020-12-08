using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 系统日志信息表(SystemLogInfo)
	/// 对象号: 100102
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class SystemLogInfo : BaseEntity
	{
		#region Field Members

		/// <summary>
		/// ID序号
		/// </summary>
		private Int32 m_Id = 0;

		/// <summary>
		/// 名称
		/// </summary>
		private String m_Name = string.Empty;

		/// <summary>
		/// IP地址
		/// </summary>
		private String m_IP = string.Empty;

		/// <summary>
		/// Mac地址
		/// </summary>
		private String m_Mac = string.Empty;

		/// <summary>
		/// 日志级别
		/// </summary>
		private Int16 m_LogLevel = 0;

		/// <summary>
		/// 创建时间
		/// </summary>
		private DateTime m_CreatorTime = DateTime.Now;

		/// <summary>
		/// 系统编号
		/// </summary>
		private String m_SystemtypeId = string.Empty;

		/// <summary>
		/// 模块
		/// </summary>
		private String m_ModuleInfo = string.Empty;

		/// <summary>
		/// 操作
		/// </summary>
		private String m_OperationInfo = string.Empty;

		/// <summary>
		/// 备注
		/// </summary>
		private String m_Remark = string.Empty;

		/// <summary>
		/// 用户后台Session的值
		/// </summary>
		private String m_SessionId = string.Empty;
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
		/// 日志级别
		/// 1-LOG_LEVEL_EMERG,
		/// 2-LOG_LEVEL_ALERT,
		/// 3-LOG_LEVEL_CRIT,
		/// 4-LOG_LEVEL_ERR,
		/// 5-LOG_LEVEL_WARN,
		/// 6-LOG_LEVEL_NOTICE,
		/// 7-LOG_LEVEL_INFO,
		/// 8-LOG_LEVEL_DEBUG,
		/// 9-LOG_LEVEL_SQL
		/// </summary>
		[DataMember]
		[DisplayName("日志级别")]
		public virtual Int16 LogLevel
		{
			get
			{
				return this.m_LogLevel;
			}
			set
			{
				this.m_LogLevel = value;
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

		/// <summary>
		/// 模块
		/// </summary>
		[DataMember]
		[DisplayName("模块")]
		public virtual String ModuleInfo
		{
			get
			{
				return this.m_ModuleInfo;
			}
			set
			{
				this.m_ModuleInfo = value;
			}
		}

		/// <summary>
		/// 操作
		/// </summary>
		[DataMember]
		[DisplayName("操作")]
		public virtual String OperationInfo
		{
			get
			{
				return this.m_OperationInfo;
			}
			set
			{
				this.m_OperationInfo = value;
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
		/// 用户后台Session的值
		/// </summary>
		[DataMember]
		[DisplayName("用户后台Session的值")]
		public virtual String SessionId
		{
			get
			{
				return this.m_SessionId;
			}
			set
			{
				this.m_SessionId = value;
			}
		}
		#endregion
	}
}