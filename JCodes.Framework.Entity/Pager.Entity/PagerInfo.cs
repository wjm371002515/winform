using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 页面信息(PagerInfo)
	/// 对象号: 100037
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class PagerInfo
	{
		#region Field Members

		/// <summary>
		/// 当前页码
		/// </summary>
		private Int32 m_CurrenetPageIndex = 0;

		/// <summary>
		/// 每页显示的记录
		/// </summary>
		private Int32 m_PageSize = 0;

		/// <summary>
		/// 记录总数
		/// </summary>
		private Int32 m_RecordCount = 0;
		#endregion

		#region Property Members

		/// <summary>
		/// 当前页码
		/// </summary>
		[DataMember]
		public virtual Int32 CurrenetPageIndex
		{
			get
			{
				return this.m_CurrenetPageIndex;
			}
			set
			{
				this.m_CurrenetPageIndex = value;
			}
		}

		/// <summary>
		/// 每页显示的记录
		/// </summary>
		[DataMember]
		public virtual Int32 PageSize
		{
			get
			{
				return this.m_PageSize;
			}
			set
			{
				this.m_PageSize = value;
			}
		}

		/// <summary>
		/// 记录总数
		/// </summary>
		[DataMember]
		public virtual Int32 RecordCount
		{
			get
			{
				return this.m_RecordCount;
			}
			set
			{
				this.m_RecordCount = value;
			}
		}
		#endregion
	}
}