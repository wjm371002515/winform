using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 东南海投票信息(DongnanhaiVotesInfo)
	/// 对象号: 100035
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class DongnanhaiVotesInfo : BaseEntity
	{
		#region Field Members

		/// <summary>
		/// 房号
		/// </summary>
		private string m_Fanghao;

		/// <summary>
		/// 单元
		/// </summary>
		private string m_Util;

		/// <summary>
		/// 幢
		/// </summary>
		private string m_Zhuang;

		/// <summary>
		/// 住宅/商铺
		/// </summary>
		private string m_Yuan;

		/// <summary>
		/// 层
		/// </summary>
		private Int32 m_Ceng = 0;

		/// <summary>
		/// 投票标志
		/// </summary>
		private Int32 m_Flag = 0;
		#endregion

		#region Property Members

		/// <summary>
		/// 房号
		/// </summary>
		[DataMember]
		public virtual string Fanghao
		{
			get
			{
				return this.m_Fanghao;
			}
			set
			{
				this.m_Fanghao = value;
			}
		}

		/// <summary>
		/// 单元
		/// </summary>
		[DataMember]
		public virtual string Util
		{
			get
			{
				return this.m_Util;
			}
			set
			{
				this.m_Util = value;
			}
		}

		/// <summary>
		/// 幢
		/// </summary>
		[DataMember]
		public virtual string Zhuang
		{
			get
			{
				return this.m_Zhuang;
			}
			set
			{
				this.m_Zhuang = value;
			}
		}

		/// <summary>
		/// 住宅/商铺
		/// </summary>
		[DataMember]
		public virtual string Yuan
		{
			get
			{
				return this.m_Yuan;
			}
			set
			{
				this.m_Yuan = value;
			}
		}

		/// <summary>
		/// 层
		/// </summary>
		[DataMember]
		public virtual Int32 Ceng
		{
			get
			{
				return this.m_Ceng;
			}
			set
			{
				this.m_Ceng = value;
			}
		}

		/// <summary>
		/// 投票标志
		/// 0.未投, 1.已投
		/// </summary>
		[DataMember]
		public virtual Int32 Flag
		{
			get
			{
				return this.m_Flag;
			}
			set
			{
				this.m_Flag = value;
			}
		}
		#endregion
	}
}