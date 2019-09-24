using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 项目信息(ProjectInfo)
	/// 对象号: 100046
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class ProjectInfo
	{
		#region Field Members

		/// <summary>
		/// GUID对应的ID序号
		/// </summary>
		private String m_Gid = string.Empty;

		/// <summary>
		/// 名称
		/// </summary>
		private String m_Name = string.Empty;

		/// <summary>
		/// 版本号
		/// </summary>
		private String m_Version = string.Empty;

		/// <summary>
		/// 控件类型
		/// </summary>
		private Int16 m_ControlType = 0;

		/// <summary>
		/// 联系人
		/// </summary>
		private String m_Contacts = string.Empty;

		/// <summary>
		/// 备注
		/// </summary>
		private String m_Remark = string.Empty;

		/// <summary>
		/// 数据库类型
		/// </summary>
		private String m_DbType = string.Empty;

		/// <summary>
		/// 字典大类信息
		/// </summary>
		private String m_DicttypeTable = string.Empty;

		/// <summary>
		/// 字典明细信息
		/// </summary>
		private String m_DictdataTable = string.Empty;

		/// <summary>
		/// 错误号信息表
		/// </summary>
		private String m_ErrTable = string.Empty;

		/// <summary>
		/// 最后更新时间
		/// </summary>
		private DateTime m_LastUpdateTime = DateTime.Now;

		/// <summary>
		/// 脚本生成路径
		/// </summary>
		private String m_OutputPath = string.Empty;
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
		/// 版本号
		/// </summary>
		[DataMember]
		[DisplayName("版本号")]
		public virtual String Version
		{
			get
			{
				return this.m_Version;
			}
			set
			{
				this.m_Version = value;
			}
		}

		/// <summary>
		/// 控件类型
		/// </summary>
		[DataMember]
		[DisplayName("控件类型")]
		public virtual Int16 ControlType
		{
			get
			{
				return this.m_ControlType;
			}
			set
			{
				this.m_ControlType = value;
			}
		}

		/// <summary>
		/// 联系人
		/// </summary>
		[DataMember]
		[DisplayName("联系人")]
		public virtual String Contacts
		{
			get
			{
				return this.m_Contacts;
			}
			set
			{
				this.m_Contacts = value;
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
		/// 数据库类型
		/// </summary>
		[DataMember]
		[DisplayName("数据库类型")]
		public virtual String DbType
		{
			get
			{
				return this.m_DbType;
			}
			set
			{
				this.m_DbType = value;
			}
		}

		/// <summary>
		/// 字典大类信息
		/// </summary>
		[DataMember]
		[DisplayName("字典大类信息")]
		public virtual String DicttypeTable
		{
			get
			{
				return this.m_DicttypeTable;
			}
			set
			{
				this.m_DicttypeTable = value;
			}
		}

		/// <summary>
		/// 字典明细信息
		/// </summary>
		[DataMember]
		[DisplayName("字典明细信息")]
		public virtual String DictdataTable
		{
			get
			{
				return this.m_DictdataTable;
			}
			set
			{
				this.m_DictdataTable = value;
			}
		}

		/// <summary>
		/// 错误号信息表
		/// </summary>
		[DataMember]
		[DisplayName("错误号信息表")]
		public virtual String ErrTable
		{
			get
			{
				return this.m_ErrTable;
			}
			set
			{
				this.m_ErrTable = value;
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
		/// 脚本生成路径
		/// </summary>
		[DataMember]
		[DisplayName("脚本生成路径")]
		public virtual String OutputPath
		{
			get
			{
				return this.m_OutputPath;
			}
			set
			{
				this.m_OutputPath = value;
			}
		}
		#endregion
	}
}