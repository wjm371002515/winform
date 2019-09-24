using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 标准字段下拉框(StdFieldComboBox)
	/// 对象号: 100047
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class StdFieldComboBox
	{
		#region Field Members

		/// <summary>
		/// 名称
		/// </summary>
		private String m_Name = string.Empty;

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
		/// 字典条目说明
		/// </summary>
		private String m_DictNameLst = string.Empty;
		#endregion

		#region Property Members

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
		/// 字典条目说明
		/// </summary>
		[DataMember]
		[DisplayName("字典条目说明")]
		public virtual String DictNameLst
		{
			get
			{
				return this.m_DictNameLst;
			}
			set
			{
				this.m_DictNameLst = value;
			}
		}
		#endregion
	}
}