using System;
using System.Runtime.Serialization;
using System.ComponentModel;
using DevExpress.XtraEditors.DXErrorProvider;
using System.Collections.Generic;
namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 系统功能定义(SysFunctionInfo)
	/// 对象号: 100049
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class SysFunctionInfo : IDXDataErrorInfo
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
		/// 功能gid
		/// </summary>
		private String m_FunctionGid = string.Empty;

		/// <summary>
		/// 系统编号
		/// </summary>
		private String m_SystemtypeId = string.Empty;

		/// <summary>
		/// 排序
		/// </summary>
		private String m_Seq = string.Empty;

		/// <summary>
		/// 保存行数据中字段名，错误信息
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
		/// 功能gid
		/// </summary>
		[DataMember]
		[DisplayName("功能gid")]
		public virtual String FunctionGid
		{
			get
			{
				return this.m_FunctionGid;
			}
			set
			{
				this.m_FunctionGid = value;
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
		/// 保存行数据中字段名，错误信息
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