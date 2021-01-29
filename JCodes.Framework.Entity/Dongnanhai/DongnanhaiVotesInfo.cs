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
		private String m_FangHao = string.Empty;

		/// <summary>
		/// 单元
		/// </summary>
		private String m_Util = string.Empty;

		/// <summary>
		/// 幢
		/// </summary>
		private String m_Zhuang = string.Empty;

		/// <summary>
		/// 名称
		/// </summary>
		private String m_Name = string.Empty;

		/// <summary>
		/// 层
		/// </summary>
		private String m_Ceng = string.Empty;

		/// <summary>
		/// 启用状态
		/// </summary>
		private Int16 m_EnableStatus = 0;
		#endregion

		#region Property Members

		/// <summary>
		/// 房号
		/// </summary>
		[DataMember]
		[DisplayName("房号")]
		public virtual String FangHao
		{
			get
			{
				return this.m_FangHao;
			}
			set
			{
				this.m_FangHao = value;
			}
		}

		/// <summary>
		/// 单元
		/// </summary>
		[DataMember]
		[DisplayName("单元")]
		public virtual String Util
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
		[DisplayName("幢")]
		public virtual String Zhuang
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
		/// 层
		/// </summary>
		[DataMember]
		[DisplayName("层")]
		public virtual String Ceng
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
		/// 启用状态
		/// 1-启用,
		/// 2-待启用,
		/// 3-作废
		/// </summary>
		[DataMember]
		[DisplayName("启用状态")]
		public virtual Int16 EnableStatus
		{
			get
			{
				return this.m_EnableStatus;
			}
			set
			{
				this.m_EnableStatus = value;
			}
		}
		#endregion
	}
}