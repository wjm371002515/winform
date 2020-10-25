using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 用户对指定内容的操作状态记录(InformationStatusInfo)
	/// 对象号: 100086
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class InformationStatusInfo : BaseEntity
	{
		#region Field Members

		/// <summary>
		/// ID序号
		/// </summary>
		private Int32 m_Id = 0;

		/// <summary>
		/// 信息大类名称
		/// </summary>
		private Int16 m_InformationCategory = 0;

		/// <summary>
		/// 信息Id
		/// </summary>
		private Int32 m_InformationId = 0;

		/// <summary>
		/// 处理状态
		/// </summary>
		private Int16 m_DealStatus = 0;

		/// <summary>
		/// 用户Id
		/// </summary>
		private Int32 m_UserId = 0;
		#endregion

		#region Property Members

		/// <summary>
		/// ID序号
		/// </summary>
		[DataMember]
		[DisplayName("ID序号")]
		public virtual Int32 Id
		{
			get
			{
				return this.m_Id;
			}
			set
			{
				this.m_Id = value;
			}
		}

		/// <summary>
		/// 信息大类名称
		/// 1-通知公告,
		/// 2-行业动态,
		/// 3-政策法规
		/// </summary>
		[DataMember]
		[DisplayName("信息大类名称")]
		public virtual Int16 InformationCategory
		{
			get
			{
				return this.m_InformationCategory;
			}
			set
			{
				this.m_InformationCategory = value;
			}
		}

		/// <summary>
		/// 信息Id
		/// </summary>
		[DataMember]
		[DisplayName("信息Id")]
		public virtual Int32 InformationId
		{
			get
			{
				return this.m_InformationId;
			}
			set
			{
				this.m_InformationId = value;
			}
		}

		/// <summary>
		/// 处理状态
		/// 1-未处理,
		/// 2-待处理,
		/// 3-正在处理,
		/// 4-已处理
		/// </summary>
		[DataMember]
		[DisplayName("处理状态")]
		public virtual Int16 DealStatus
		{
			get
			{
				return this.m_DealStatus;
			}
			set
			{
				this.m_DealStatus = value;
			}
		}

		/// <summary>
		/// 用户Id
		/// </summary>
		[DataMember]
		[DisplayName("用户Id")]
		public virtual Int32 UserId
		{
			get
			{
				return this.m_UserId;
			}
			set
			{
				this.m_UserId = value;
			}
		}
		#endregion
	}
}