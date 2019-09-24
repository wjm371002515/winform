using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 字典大类(DicKeyValueInfo)
	/// 对象号: 100015
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class DicKeyValueInfo
	{
		#region Field Members

		/// <summary>
		/// 字典类型对应的ID
		/// </summary>
		private Int32 m_DicttypeId = 0;

		/// <summary>
		/// 字典键
		/// </summary>
		private Int32 m_DicttypeValue = 0;

		/// <summary>
		/// 名称
		/// </summary>
		private String m_Name = string.Empty;
		#endregion

		#region Property Members

		/// <summary>
		/// 字典类型对应的ID
		/// </summary>
		[DataMember]
		[DisplayName("字典类型对应的ID")]
		public virtual Int32 DicttypeId
		{
			get
			{
				return this.m_DicttypeId;
			}
			set
			{
				this.m_DicttypeId = value;
			}
		}

		/// <summary>
		/// 字典键
		/// </summary>
		[DataMember]
		[DisplayName("字典键")]
		public virtual Int32 DicttypeValue
		{
			get
			{
				return this.m_DicttypeValue;
			}
			set
			{
				this.m_DicttypeValue = value;
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
		#endregion
	}
}