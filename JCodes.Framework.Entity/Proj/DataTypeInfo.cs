using System;
using System.Runtime.Serialization;
using System.ComponentModel;
using DevExpress.XtraEditors.DXErrorProvider;
using System.Collections.Generic;
namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 数据类型(DataTypeInfo)
	/// 对象号: 100039
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class DataTypeInfo : IDXDataErrorInfo
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
		/// 标准类型
		/// </summary>
		private String m_StdType = string.Empty;

		/// <summary>
		/// 长度
		/// </summary>
		private String m_Length = string.Empty;

		/// <summary>
		/// 精度
		/// </summary>
		private String m_Precision = string.Empty;

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
		/// 标准类型
		/// </summary>
		[DataMember]
		[DisplayName("标准类型")]
		public virtual String StdType
		{
			get
			{
				return this.m_StdType;
			}
			set
			{
				this.m_StdType = value;
			}
		}

		/// <summary>
		/// 长度
		/// </summary>
		[DataMember]
		[DisplayName("长度")]
		public virtual String Length
		{
			get
			{
				return this.m_Length;
			}
			set
			{
				this.m_Length = value;
			}
		}

		/// <summary>
		/// 精度
		/// </summary>
		[DataMember]
		[DisplayName("精度")]
		public virtual String Precision
		{
			get
			{
				return this.m_Precision;
			}
			set
			{
				this.m_Precision = value;
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
		/// 添加自定义错误
		/// </summary>
		void IDXDataErrorInfo.GetPropertyError(string propertyName, ErrorInfo info)
		{
			
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