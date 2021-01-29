using System;
using System.Runtime.Serialization;
using System.ComponentModel;
using DevExpress.XtraEditors.DXErrorProvider;
using System.Collections.Generic;
namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 错误信息(ErrornoInfo)
	/// 对象号: 100044
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class ErrornoInfo : IDXDataErrorInfo
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
        private string m_ChineseName = string.Empty;

		/// <summary>
		/// 日志级别
		/// </summary>
		private Int16 m_LogLevel = 0;

		/// <summary>
		/// 备注
		/// </summary>
		private String m_Remark = string.Empty;

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
		public virtual string ChineseName
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
		/// 自定义错误
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