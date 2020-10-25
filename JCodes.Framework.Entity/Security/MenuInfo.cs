using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 菜单信息(MenuInfo)
	/// 对象号: 100058
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class MenuInfo : BaseEntity
	{
		#region Field Members

		/// <summary>
		/// GUID对应的ID序号
		/// </summary>
		private String m_Gid = string.Empty;

		/// <summary>
		/// 父节点GUID对应的ID序号
		/// </summary>
		private String m_Pgid = string.Empty;

		/// <summary>
		/// 名称
		/// </summary>
		private String m_Name = string.Empty;

		/// <summary>
		/// icon图标路径
		/// </summary>
		private String m_Icon = string.Empty;

		/// <summary>
		/// 排序
		/// </summary>
		private String m_Seq = string.Empty;

		/// <summary>
		/// 控制标识
		/// </summary>
		private String m_AuthGid = string.Empty;

		/// <summary>
		/// 是否可见
		/// </summary>
		private Int16 m_IsVisable = 0;

		/// <summary>
		/// 窗体类名
		/// </summary>
		private String m_WinformClass = string.Empty;

		/// <summary>
		/// URL地址
		/// </summary>
		private String m_Url = string.Empty;

		/// <summary>
		/// Web对应的icon图标路径
		/// </summary>
		private String m_WebIcon = string.Empty;

		/// <summary>
		/// 系统编号
		/// </summary>
		private String m_SystemtypeId = string.Empty;

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
		/// 映射路径
		/// </summary>
		private String m_DllPath = string.Empty;
		#endregion

		#region Property Members

		/// <summary>
		/// GUID对应的ID序号
		/// </summary>
		[DataMember]
		[DisplayName("GUID对应的ID序号")]
		public virtual String Gid
		{
			get
			{
				return this.m_Gid;
			}
			set
			{
				this.m_Gid = value;
			}
		}

		/// <summary>
		/// 父节点GUID对应的ID序号
		/// </summary>
		[DataMember]
		[DisplayName("父节点GUID对应的ID序号")]
		public virtual String Pgid
		{
			get
			{
				return this.m_Pgid;
			}
			set
			{
				this.m_Pgid = value;
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
		/// icon图标路径
		/// </summary>
		[DataMember]
		[DisplayName("icon图标路径")]
		public virtual String Icon
		{
			get
			{
				return this.m_Icon;
			}
			set
			{
				this.m_Icon = value;
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
		/// 控制标识
		/// </summary>
		[DataMember]
		[DisplayName("控制标识")]
		public virtual String AuthGid
		{
			get
			{
				return this.m_AuthGid;
			}
			set
			{
				this.m_AuthGid = value;
			}
		}

		/// <summary>
		/// 是否可见
		/// 1-是,
		/// 2-否
		/// </summary>
		[DataMember]
		[DisplayName("是否可见")]
		public virtual Int16 IsVisable
		{
			get
			{
				return this.m_IsVisable;
			}
			set
			{
				this.m_IsVisable = value;
			}
		}

		/// <summary>
		/// 窗体类名
		/// </summary>
		[DataMember]
		[DisplayName("窗体类名")]
		public virtual String WinformClass
		{
			get
			{
				return this.m_WinformClass;
			}
			set
			{
				this.m_WinformClass = value;
			}
		}

		/// <summary>
		/// URL地址
		/// </summary>
		[DataMember]
		[DisplayName("URL地址")]
		public virtual String Url
		{
			get
			{
				return this.m_Url;
			}
			set
			{
				this.m_Url = value;
			}
		}

		/// <summary>
		/// Web对应的icon图标路径
		/// </summary>
		[DataMember]
		[DisplayName("Web对应的icon图标路径")]
		public virtual String WebIcon
		{
			get
			{
				return this.m_WebIcon;
			}
			set
			{
				this.m_WebIcon = value;
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
		/// 映射路径
		/// </summary>
		[DataMember]
		[DisplayName("映射路径")]
		public virtual String DllPath
		{
			get
			{
				return this.m_DllPath;
			}
			set
			{
				this.m_DllPath = value;
			}
		}
		#endregion
	}
}