using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 记录操作日志的数据表配置(OperationLogSettingInfo)
	/// 对象号: 100060
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class OperationLogSettingInfo : BaseEntity
	{
		#region Field Members

		/// <summary>
		/// ID序号
		/// </summary>
		private Int32 m_Id = 0;

		/// <summary>
		/// 是否禁用
		/// </summary>
		private Int16 m_IsForbid = 0;

		/// <summary>
		/// 表名
		/// </summary>
		private String m_TableName = string.Empty;

		/// <summary>
		/// 是否插入日志
		/// </summary>
		private Int16 m_IsInsertLog = 0;

		/// <summary>
		/// 是否更新日志
		/// </summary>
		private Int16 m_IsUpdateLog = 0;

		/// <summary>
		/// 是否删除日志
		/// </summary>
		private Int16 m_IsDeleteLog = 0;

		/// <summary>
		/// 备注
		/// </summary>
		private String m_Remark = string.Empty;

		/// <summary>
		/// 创建人编号
		/// </summary>
		private Int32 m_CreatorId = 0;

		/// <summary>
		/// 创建时间
		/// </summary>
		private DateTime m_CreatorTime = DateTime.Now;

		/// <summary>
		/// 编辑人编号
		/// </summary>
		private Int32 m_EditorId = 0;

		/// <summary>
		/// 最后更新时间
		/// </summary>
		private DateTime m_LastUpdateTime = DateTime.Now;
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
		/// 是否禁用
		/// </summary>
		[DataMember]
		[DisplayName("是否禁用")]
		public virtual Int16 IsForbid
		{
			get
			{
				return this.m_IsForbid;
			}
			set
			{
				this.m_IsForbid = value;
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
		/// 是否插入日志
		/// </summary>
		[DataMember]
		[DisplayName("是否插入日志")]
		public virtual Int16 IsInsertLog
		{
			get
			{
				return this.m_IsInsertLog;
			}
			set
			{
				this.m_IsInsertLog = value;
			}
		}

		/// <summary>
		/// 是否更新日志
		/// </summary>
		[DataMember]
		[DisplayName("是否更新日志")]
		public virtual Int16 IsUpdateLog
		{
			get
			{
				return this.m_IsUpdateLog;
			}
			set
			{
				this.m_IsUpdateLog = value;
			}
		}

		/// <summary>
		/// 是否删除日志
		/// </summary>
		[DataMember]
		[DisplayName("是否删除日志")]
		public virtual Int16 IsDeleteLog
		{
			get
			{
				return this.m_IsDeleteLog;
			}
			set
			{
				this.m_IsDeleteLog = value;
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
		/// 创建人编号
		/// </summary>
		[DataMember]
		[DisplayName("创建人编号")]
		public virtual Int32 CreatorId
		{
			get
			{
				return this.m_CreatorId;
			}
			set
			{
				this.m_CreatorId = value;
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
		/// 编辑人编号
		/// </summary>
		[DataMember]
		[DisplayName("编辑人编号")]
		public virtual Int32 EditorId
		{
			get
			{
				return this.m_EditorId;
			}
			set
			{
				this.m_EditorId = value;
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
		#endregion
	}
}