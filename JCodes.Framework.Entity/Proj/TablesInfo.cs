using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 表信息(TablesInfo)
	/// 对象号: 100053
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class TablesInfo
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
		/// 中文名称
		/// </summary>
		private String m_ChineseName = string.Empty;

		/// <summary>
		/// 功能号
		/// </summary>
        private String m_FunctionId = string.Empty;

		/// <summary>
		/// 类型GUID
		/// </summary>
		private String m_TypeGuid = string.Empty;

		/// <summary>
		/// 文件保存相对路径
		/// </summary>
		private String m_SavePath = string.Empty;

		/// <summary>
		/// 基础数据文件路径
		/// </summary>
		private String m_BasicdataPath = string.Empty;
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
		/// 中文名称
		/// </summary>
		[DataMember]
		[DisplayName("中文名称")]
		public virtual String ChineseName
		{
			get
			{
				return this.m_ChineseName;
			}
			set
			{
				this.m_ChineseName = value;
			}
		}

		/// <summary>
		/// 功能号
		/// </summary>
		[DataMember]
		[DisplayName("功能号")]
        public virtual String FunctionId
		{
			get
			{
				return this.m_FunctionId;
			}
			set
			{
				this.m_FunctionId = value;
			}
		}

		/// <summary>
		/// 类型GUID
		/// </summary>
		[DataMember]
		[DisplayName("类型GUID")]
		public virtual String TypeGuid
		{
			get
			{
				return this.m_TypeGuid;
			}
			set
			{
				this.m_TypeGuid = value;
			}
		}

		/// <summary>
		/// 文件保存相对路径
		/// </summary>
		[DataMember]
		[DisplayName("文件保存相对路径")]
		public virtual String SavePath
		{
			get
			{
				return this.m_SavePath;
			}
			set
			{
				this.m_SavePath = value;
			}
		}

		/// <summary>
		/// 基础数据文件路径
		/// </summary>
		[DataMember]
		[DisplayName("基础数据文件路径")]
		public virtual String BasicdataPath
		{
			get
			{
				return this.m_BasicdataPath;
			}
			set
			{
				this.m_BasicdataPath = value;
			}
		}
		#endregion
	}
}