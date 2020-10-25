using System;
using System.Runtime.Serialization;
using System.ComponentModel;
using DevExpress.XtraEditors.DXErrorProvider;
using System.Collections.Generic;
namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 表格字段信息(TableFieldsInfo)
	/// 对象号: 100051
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class TableFieldsInfo : IDXDataErrorInfo
	{
		#region Field Members

		/// <summary>
		/// GUID对应的ID序号
		/// </summary>
		private String m_Gid = string.Empty;

		/// <summary>
		/// 字段名字
		/// </summary>
		private String m_FieldName = string.Empty;

		/// <summary>
		/// 中文名称
		/// </summary>
		private String m_ChineseName = string.Empty;

		/// <summary>
		/// 字段类型
		/// </summary>
		private String m_DataType = string.Empty;

		/// <summary>
		/// 字典条目
		/// </summary>
		private Int32 m_DictNo = 0;

		/// <summary>
		/// 字段说明
		/// </summary>
		private String m_FieldInfo = string.Empty;

		/// <summary>
		/// 允许空
		/// </summary>
		private Int16 m_IsNull = 0;

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
		/// 字段名字
		/// </summary>
		[DataMember]
		[DisplayName("字段名字")]
		public virtual String FieldName
		{
			get
			{
				return this.m_FieldName;
			}
			set
			{
				this.m_FieldName = value;
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
		/// 字段类型
		/// </summary>
		[DataMember]
		[DisplayName("字段类型")]
		public virtual String DataType
		{
			get
			{
				return this.m_DataType;
			}
			set
			{
				this.m_DataType = value;
			}
		}

		/// <summary>
		/// 字典条目
		/// </summary>
		[DataMember]
		[DisplayName("字典条目")]
		public virtual Int32 DictNo
		{
			get
			{
				return this.m_DictNo;
			}
			set
			{
				this.m_DictNo = value;
			}
		}

		/// <summary>
		/// 字段说明
		/// </summary>
		[DataMember]
		[DisplayName("字段说明")]
		public virtual String FieldInfo
		{
			get
			{
				return this.m_FieldInfo;
			}
			set
			{
				this.m_FieldInfo = value;
			}
		}

		/// <summary>
		/// 允许空
		/// 1-是,
		/// 2-否
		/// </summary>
		[DataMember]
		[DisplayName("允许空")]
		public virtual Int16 IsNull
		{
			get
			{
				return this.m_IsNull;
			}
			set
			{
				this.m_IsNull = value;
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