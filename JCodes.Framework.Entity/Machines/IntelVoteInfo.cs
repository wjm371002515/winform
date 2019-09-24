using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 投票信息类(IntelVoteInfo)
	/// 对象号: 100033
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class IntelVoteInfo : BaseEntity
	{
		#region Field Members

		/// <summary>
		/// 小区名字
		/// </summary>
		private string m_xiaoquName;

		/// <summary>
		/// 投票数量
		/// </summary>
		private Int32 m_intelVote = 0;

		/// <summary>
		/// 总户数
		/// </summary>
		private string m_houseNum;

		/// <summary>
		/// 投票率
		/// </summary>
		private string m_percentage;

		/// <summary>
		/// 详情连接
		/// </summary>
		private string m_detailurl;
		#endregion

		#region Property Members

		/// <summary>
		/// 小区名字
		/// </summary>
		[DataMember]
		public virtual string xiaoquName
		{
			get
			{
				return this.m_xiaoquName;
			}
			set
			{
				this.m_xiaoquName = value;
			}
		}

		/// <summary>
		/// 投票数量
		/// </summary>
		[DataMember]
		public virtual Int32 intelVote
		{
			get
			{
				return this.m_intelVote;
			}
			set
			{
				this.m_intelVote = value;
			}
		}

		/// <summary>
		/// 总户数
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
		/// 投票率
		/// </summary>
		[DataMember]
		public virtual string percentage
		{
			get
			{
				return this.m_percentage;
			}
			set
			{
				this.m_percentage = value;
			}
		}

		/// <summary>
		/// 详情连接
		/// </summary>
		[DataMember]
		public virtual string detailurl
		{
			get
			{
				return this.m_detailurl;
			}
			set
			{
				this.m_detailurl = value;
			}
		}
		#endregion
	}
}