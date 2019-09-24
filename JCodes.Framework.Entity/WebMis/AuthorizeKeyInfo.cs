using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 定义常用功能的控制ID，方便基类控制器对用户权限的控制(AuthorizeKeyInfo)
	/// 对象号: 100082
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class AuthorizeKeyInfo
	{
		#region Field Members

		/// <summary>
		/// 新增记录的功能控制ID
		/// </summary>
		private string m_InsertKey;

		/// <summary>
		/// 更新记录的功能控制ID
		/// </summary>
		private string m_UpdateKey;

		/// <summary>
		/// 删除记录的功能控制ID
		/// </summary>
		private string m_DeleteKey;

		/// <summary>
		/// 查看列表的功能控制ID
		/// </summary>
		private string m_ListKey;

		/// <summary>
		/// 查看明细的功能控制ID
		/// </summary>
		private string m_ViewKey;

		/// <summary>
		/// 导出记录的功能控制ID
		/// </summary>
		private string m_ExportKey;

		/// <summary>
		/// 判断是否具有插入权限
		/// </summary>
		private bool m_CanInsert;

		/// <summary>
		/// 判断是否具有更新权限
		/// </summary>
		private bool m_CanUpdate;

		/// <summary>
		/// 判断是否具有删除权限
		/// </summary>
		private bool m_CanDelete;

		/// <summary>
		/// 判断是否具有列表权限
		/// </summary>
		private bool m_CanList;

		/// <summary>
		/// 判断是否具有查看权限
		/// </summary>
		private bool m_CanView;

		/// <summary>
		/// 判断是否具有导出权限
		/// </summary>
		private bool m_CanExport;
		#endregion

		#region Property Members

		/// <summary>
		/// 新增记录的功能控制ID
		/// </summary>
		[DataMember]
		public virtual string InsertKey
		{
			get
			{
				return this.m_InsertKey;
			}
			set
			{
				this.m_InsertKey = value;
			}
		}

		/// <summary>
		/// 更新记录的功能控制ID
		/// </summary>
		[DataMember]
		public virtual string UpdateKey
		{
			get
			{
				return this.m_UpdateKey;
			}
			set
			{
				this.m_UpdateKey = value;
			}
		}

		/// <summary>
		/// 删除记录的功能控制ID
		/// </summary>
		[DataMember]
		public virtual string DeleteKey
		{
			get
			{
				return this.m_DeleteKey;
			}
			set
			{
				this.m_DeleteKey = value;
			}
		}

		/// <summary>
		/// 查看列表的功能控制ID
		/// </summary>
		[DataMember]
		public virtual string ListKey
		{
			get
			{
				return this.m_ListKey;
			}
			set
			{
				this.m_ListKey = value;
			}
		}

		/// <summary>
		/// 查看明细的功能控制ID
		/// </summary>
		[DataMember]
		public virtual string ViewKey
		{
			get
			{
				return this.m_ViewKey;
			}
			set
			{
				this.m_ViewKey = value;
			}
		}

		/// <summary>
		/// 导出记录的功能控制ID
		/// </summary>
		[DataMember]
		public virtual string ExportKey
		{
			get
			{
				return this.m_ExportKey;
			}
			set
			{
				this.m_ExportKey = value;
			}
		}

		/// <summary>
		/// 判断是否具有插入权限
		/// </summary>
		[DataMember]
		public virtual bool CanInsert
		{
			get
			{
				return this.m_CanInsert;
			}
			set
			{
				this.m_CanInsert = value;
			}
		}

		/// <summary>
		/// 判断是否具有更新权限
		/// </summary>
		[DataMember]
		public virtual bool CanUpdate
		{
			get
			{
				return this.m_CanUpdate;
			}
			set
			{
				this.m_CanUpdate = value;
			}
		}

		/// <summary>
		/// 判断是否具有删除权限
		/// </summary>
		[DataMember]
		public virtual bool CanDelete
		{
			get
			{
				return this.m_CanDelete;
			}
			set
			{
				this.m_CanDelete = value;
			}
		}

		/// <summary>
		/// 判断是否具有列表权限
		/// </summary>
		[DataMember]
		public virtual bool CanList
		{
			get
			{
				return this.m_CanList;
			}
			set
			{
				this.m_CanList = value;
			}
		}

		/// <summary>
		/// 判断是否具有查看权限
		/// </summary>
		[DataMember]
		public virtual bool CanView
		{
			get
			{
				return this.m_CanView;
			}
			set
			{
				this.m_CanView = value;
			}
		}

		/// <summary>
		/// 判断是否具有导出权限
		/// </summary>
		[DataMember]
		public virtual bool CanExport
		{
			get
			{
				return this.m_CanExport;
			}
			set
			{
				this.m_CanExport = value;
			}
		}
		#endregion
	}
}