using System;
using System.Runtime.Serialization;
using System.ComponentModel;
using DevExpress.XtraEditors.DXErrorProvider;
using System.Collections.Generic;
namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 默认数据类型(DefaultTypeInfo)
	/// 对象号: 100040
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class DefaultTypeInfo : IDXDataErrorInfo
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
		/// Oracle
		/// </summary>
		private String m_Oracle = string.Empty;

		/// <summary>
		/// Mysql
		/// </summary>
		private String m_Mysql = string.Empty;

		/// <summary>
		/// DB2
		/// </summary>
		private String m_DB2 = string.Empty;

		/// <summary>
		/// SqlServer
		/// </summary>
		private String m_SqlServer = string.Empty;

		/// <summary>
		/// Sqlite
		/// </summary>
		private String m_Sqlite = string.Empty;

		/// <summary>
		/// Access
		/// </summary>
		private String m_Access = string.Empty;

		/// <summary>
		/// CShort
		/// </summary>
		private String m_CShort = string.Empty;

		/// <summary>
		/// 用来保存行数据中字段名，错误信息
		/// </summary>
		private Dictionary<string, ErrorInfo> m_lstInfo;
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
		/// Oracle
		/// </summary>
		[DataMember]
		[DisplayName("Oracle")]
		public virtual String Oracle
		{
			get
			{
				return this.m_Oracle;
			}
			set
			{
				this.m_Oracle = value;
			}
		}

		/// <summary>
		/// Mysql
		/// </summary>
		[DataMember]
		[DisplayName("Mysql")]
		public virtual String Mysql
		{
			get
			{
				return this.m_Mysql;
			}
			set
			{
				this.m_Mysql = value;
			}
		}

		/// <summary>
		/// DB2
		/// </summary>
		[DataMember]
		[DisplayName("DB2")]
		public virtual String DB2
		{
			get
			{
				return this.m_DB2;
			}
			set
			{
				this.m_DB2 = value;
			}
		}

		/// <summary>
		/// SqlServer
		/// </summary>
		[DataMember]
		[DisplayName("SqlServer")]
		public virtual String SqlServer
		{
			get
			{
				return this.m_SqlServer;
			}
			set
			{
				this.m_SqlServer = value;
			}
		}

		/// <summary>
		/// Sqlite
		/// </summary>
		[DataMember]
		[DisplayName("Sqlite")]
		public virtual String Sqlite
		{
			get
			{
				return this.m_Sqlite;
			}
			set
			{
				this.m_Sqlite = value;
			}
		}

		/// <summary>
		/// Access
		/// </summary>
		[DataMember]
		[DisplayName("Access")]
		public virtual String Access
		{
			get
			{
				return this.m_Access;
			}
			set
			{
				this.m_Access = value;
			}
		}

		/// <summary>
		/// CShort
		/// </summary>
		[DataMember]
		[DisplayName("CShort")]
		public virtual String CShort
		{
			get
			{
				return this.m_CShort;
			}
			set
			{
				this.m_CShort = value;
			}
		}

		/// <summary>
		/// 用来保存行数据中字段名，错误信息
		/// </summary>
		[DataMember]
		public virtual Dictionary<string, ErrorInfo> lstInfo
		{
			get
			{
				return this.m_lstInfo;
			}
			set
			{
				this.m_lstInfo = value;
			}
		}

		/// <summary>
		/// 添加自定义错误
		/// </summary>
		void IDXDataErrorInfo.GetPropertyError(string propertyName, ErrorInfo info)
		{
			 // 添加自定义错误
            if (lstInfo != null && lstInfo.Count > 0 && lstInfo.ContainsKey(propertyName) && !string.IsNullOrEmpty(lstInfo[propertyName].ErrorText))
            {
                info.ErrorText = lstInfo[propertyName].ErrorText;
                info.ErrorType = lstInfo[propertyName].ErrorType;
            }
		}

		/// <summary>
		/// 
		/// </summary>
		void IDXDataErrorInfo.GetError(ErrorInfo info)
		{
			
		}
		#endregion
	}
}