using System;
using System.Runtime.Serialization;
using System.ComponentModel;
using DevExpress.XtraEditors.DXErrorProvider;
using System.Collections.Generic;
namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 表格索引信息(TableIndexsInfo)
	/// 对象号: 100052
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class TableIndexsInfo : IDXDataErrorInfo
	{
		#region Field Members

		/// <summary>
		/// GUID对应的ID序号
		/// </summary>
		private String m_Gid = string.Empty;

		/// <summary>
		/// 索引名
		/// </summary>
		private String m_IndexName = string.Empty;

		/// <summary>
		/// 索引字段列表
		/// </summary>
		private String m_IndexFieldLst = string.Empty;

		/// <summary>
		/// 约束类型
		/// </summary>
		private Int16 m_ConstraintType = 0;

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
		/// 索引名
		/// </summary>
		[DataMember]
		[DisplayName("索引名")]
		public virtual String IndexName
		{
			get
			{
				return this.m_IndexName;
			}
			set
			{
				this.m_IndexName = value;
			}
		}

		/// <summary>
		/// 索引字段列表
		/// </summary>
		[DataMember]
		[DisplayName("索引字段列表")]
		public virtual String IndexFieldLst
		{
			get
			{
				return this.m_IndexFieldLst;
			}
			set
			{
				this.m_IndexFieldLst = value;
			}
		}

		/// <summary>
		/// 约束类型
		/// </summary>
		[DataMember]
		[DisplayName("约束类型")]
		public virtual Int16 ConstraintType
		{
			get
			{
				return this.m_ConstraintType;
			}
			set
			{
				this.m_ConstraintType = value;
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