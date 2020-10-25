using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 角色信息(RoleInfo)
	/// 对象号: 100063
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class RoleInfo : BaseEntity
	{
		#region Field Members

		/// <summary>
		/// ID序号
		/// </summary>
		private Int32 m_Id = 0;

		/// <summary>
		/// 角色编码
		/// </summary>
		private String m_RoleCode = string.Empty;

		/// <summary>
		/// 名称
		/// </summary>
		private String m_Name = string.Empty;

		/// <summary>
		/// 备注
		/// </summary>
		private String m_Remark = string.Empty;

		/// <summary>
		/// 排序
		/// </summary>
		private String m_Seq = string.Empty;

		/// <summary>
		/// 公司Id
		/// </summary>
		private Int32 m_CompanyId = 0;

		/// <summary>
		/// 公司名字
		/// </summary>
		private String m_CompanyName = string.Empty;

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

		/// <summary>
		/// 是否删除
		/// </summary>
		private Int16 m_IsDelete = 0;

		/// <summary>
		/// 是否禁用
		/// </summary>
		private Int16 m_IsForbid = 0;
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
		/// 角色编码
		/// </summary>
		[DataMember]
		[DisplayName("角色编码")]
		public virtual String RoleCode
		{
			get
			{
				return this.m_RoleCode;
			}
			set
			{
				this.m_RoleCode = value;
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
		/// 排序
		/// </summary>
		[DataMember]
		[DisplayName("排序")]
		public virtual String Seq
		{
			get
			{
				return this.m_Seq;
			}
			set
			{
				this.m_Seq = value;
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

		/// <summary>
		/// 是否删除
		/// 1-是,
		/// 2-否
		/// </summary>
		[DataMember]
		[DisplayName("是否删除")]
		public virtual Int16 IsDelete
		{
			get
			{
				return this.m_IsDelete;
			}
			set
			{
				this.m_IsDelete = value;
			}
		}

		/// <summary>
		/// 是否禁用
		/// 1-是,
		/// 2-否
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
		#endregion
	}
}