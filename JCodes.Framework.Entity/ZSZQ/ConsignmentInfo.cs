using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 代销数据(ConsignmentInfo)
	/// 对象号: 100092
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class ConsignmentInfo : BaseEntity
	{
		#region Field Members

		/// <summary>
		/// ID序号
		/// </summary>
		private Int32 m_Id = 0;

		/// <summary>
		/// 值
		/// </summary>
		private String m_StrValue = string.Empty;

		/// <summary>
		/// 名称
		/// </summary>
		private String m_Name = string.Empty;

		/// <summary>
		/// 启用状态
		/// </summary>
		private Int16 m_EnableStatus = 0;

		/// <summary>
		/// 系统键
		/// </summary>
		private String m_SysValue = string.Empty;
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
		/// 值
		/// 小账号
		/// </summary>
		[DataMember]
		[DisplayName("值")]
		public virtual String StrValue
		{
			get
			{
				return this.m_StrValue;
			}
			set
			{
				this.m_StrValue = value;
			}
		}

		/// <summary>
		/// 名称
		/// 代销名字
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

		/// <summary>
		/// 系统键
		/// 代销商号
		/// </summary>
		[DataMember]
		[DisplayName("系统键")]
		public virtual String SysValue
		{
			get
			{
				return this.m_SysValue;
			}
			set
			{
				this.m_SysValue = value;
			}
		}
		#endregion
	}
}