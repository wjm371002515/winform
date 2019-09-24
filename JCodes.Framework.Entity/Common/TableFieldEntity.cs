using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 表格字段实体类(TableFieldEntity)
	/// 对象号: 100032
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class TableFieldEntity
	{
		#region Field Members

		/// <summary>
		/// 表名
		/// </summary>
		private String m_TableName = string.Empty;

		/// <summary>
		/// 表格字段
		/// </summary>
		private String m_TableField = string.Empty;

		/// <summary>
		/// 排序
		/// </summary>
		private String m_Seq = string.Empty;
		#endregion

		#region Property Members

		/// <summary>
		/// 表名
		/// </summary>
		[DataMember]
		[DisplayName("表名")]
		public virtual String TableName
		{
			get
			{
				return this.m_TableName;
			}
			set
			{
				this.m_TableName = value;
			}
		}

		/// <summary>
		/// 表格字段
		/// </summary>
		[DataMember]
		[DisplayName("表格字段")]
		public virtual String TableField
		{
			get
			{
				return this.m_TableField;
			}
			set
			{
				this.m_TableField = value;
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
		#endregion
	}
}