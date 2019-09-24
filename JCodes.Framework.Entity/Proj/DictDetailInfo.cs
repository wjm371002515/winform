using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 数据字典明细信息(DictDetailInfo)
	/// 对象号: 100042
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class DictDetailInfo : IComparable<DictDetailInfo>
	{
		#region Field Members

		/// <summary>
		/// 整型值
		/// </summary>
		private Int32 m_IntValue = 0;

		/// <summary>
		/// 名称
		/// </summary>
		private String m_Name = string.Empty;

		/// <summary>
		/// 排序
		/// </summary>
		private String m_Seq = string.Empty;

		/// <summary>
		/// 备注
		/// </summary>
		private String m_Remark = string.Empty;
		#endregion

		#region Property Members

		/// <summary>
		/// 整型值
		/// </summary>
		[DataMember]
		[DisplayName("整型值")]
		public virtual Int32 IntValue
		{
			get
			{
				return this.m_IntValue;
			}
			set
			{
				this.m_IntValue = value;
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
		/// Compares to.
		/// </summary>
		public Int32 CompareTo(DictDetailInfo other)
		{
			return Seq.CompareTo(other.Seq);
		}
		#endregion
	}
}