using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 投票明细信息(IntelVoteDetailInfo)
	/// 对象号: 100034
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class IntelVoteDetailInfo : BaseEntity
	{
		#region Field Members

		/// <summary>
		/// 房号
		/// </summary>
		private string m_houseNum;

		/// <summary>
		/// 投票
		/// </summary>
		private string m_toupiao;
		#endregion

		#region Property Members

		/// <summary>
		/// 房号
		/// </summary>
		[DataMember]
		public virtual string houseNum
		{
			get
			{
				return this.m_houseNum;
			}
			set
			{
				this.m_houseNum = value;
			}
		}

		/// <summary>
		/// 投票
		/// </summary>
		[DataMember]
		public virtual string toupiao
		{
			get
			{
				return this.m_toupiao;
			}
			set
			{
				this.m_toupiao = value;
			}
		}
		#endregion
	}
}