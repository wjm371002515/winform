using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 黑白名单列表(BlackIPInfo)
	/// 对象号: 100055
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class BlackIPInfo : BaseEntity
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
		/// 授权类型
		/// </summary>
		private Int16 m_AuthorizeType = 0;

		/// <summary>
		/// 是否禁用
		/// </summary>
		private Int16 m_IsForbid = 0;

		/// <summary>
		/// IP起始地址
		/// </summary>
		private String m_IPStart = string.Empty;

		/// <summary>
		/// IP结束地址
		/// </summary>
		private String m_IPEnd = string.Empty;

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
		/// 授权类型
		/// </summary>
		[DataMember]
		[DisplayName("授权类型")]
		public virtual Int16 AuthorizeType
		{
			get
			{
				return this.m_AuthorizeType;
			}
			set
			{
				this.m_AuthorizeType = value;
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
		/// IP起始地址
		/// </summary>
		[DataMember]
		[DisplayName("IP起始地址")]
		public virtual String IPStart
		{
			get
			{
				return this.m_IPStart;
			}
			set
			{
				this.m_IPStart = value;
			}
		}

		/// <summary>
		/// IP结束地址
		/// </summary>
		[DataMember]
		[DisplayName("IP结束地址")]
		public virtual String IPEnd
		{
			get
			{
				return this.m_IPEnd;
			}
			set
			{
				this.m_IPEnd = value;
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