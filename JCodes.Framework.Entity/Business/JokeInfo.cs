using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 段子(JokeInfo)
	/// 对象号: 100108
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class JokeInfo : BaseEntity
	{
		#region Field Members

		/// <summary>
		/// ID序号
		/// </summary>
		private Int32 m_Id = 0;

		/// <summary>
		/// 个人简述
		/// </summary>
		private String m_Introduce = string.Empty;

		/// <summary>
		/// 创建人编号
		/// </summary>
		private Int32 m_CreatorId = 0;

		/// <summary>
		/// 创建时间
		/// </summary>
		private DateTime m_CreatorTime = DateTime.Now;

		/// <summary>
		/// 整形长度
		/// </summary>
		private Int32 m_NumLen = 0;
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
		/// 个人简述
		/// </summary>
		[DataMember]
		[DisplayName("个人简述")]
		public virtual String Introduce
		{
			get
			{
				return this.m_Introduce;
			}
			set
			{
				this.m_Introduce = value;
			}
		}

		/// <summary>
		/// 创建人编号
		/// </summary>
		[DataMember]
		[DisplayName("创建人编号")]
		public virtual Int32 CreatorId
		{
			get
			{
				return this.m_CreatorId;
			}
			set
			{
				this.m_CreatorId = value;
			}
		}

		/// <summary>
		/// 创建时间
		/// </summary>
		[DataMember]
		[DisplayName("创建时间")]
		public virtual DateTime CreatorTime
		{
			get
			{
				return this.m_CreatorTime;
			}
			set
			{
				this.m_CreatorTime = value;
			}
		}

		/// <summary>
		/// 整形长度
		/// </summary>
		[DataMember]
		[DisplayName("整形长度")]
		public virtual Int32 NumLen
		{
			get
			{
				return this.m_NumLen;
			}
			set
			{
				this.m_NumLen = value;
			}
		}
		#endregion
	}
}