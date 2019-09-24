using System;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Collections.Generic;
namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 修改记录信息(ModRecordInfo)
	/// 对象号: 100045
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class ModRecordInfo : IComparable<ModRecordInfo>
	{
		#region Field Members

		/// <summary>
		/// GUID对应的ID序号
		/// </summary>
		private String m_Gid = string.Empty;

		/// <summary>
		/// 修改日期
		/// </summary>
		private DateTime m_ModDate = DateTime.Now;

		/// <summary>
		/// 修改版本
		/// </summary>
		private String m_ModVersion = string.Empty;

		/// <summary>
		/// 修改单号
		/// </summary>
		private String m_ModOrderId = string.Empty;

		/// <summary>
		/// 申请人
		/// </summary>
		private String m_Proposer = string.Empty;

		/// <summary>
		/// 修改人
		/// </summary>
		private String m_Programmer = string.Empty;

		/// <summary>
		/// 修改内容
		/// </summary>
		private String m_ModContent = string.Empty;

		/// <summary>
		/// 修改原因
		/// </summary>
		private String m_ModReason = string.Empty;

		/// <summary>
		/// 备注
		/// </summary>
		private String m_Remark = string.Empty;
		#endregion

		#region Property Members

		/// <summary>
		/// GUID对应的ID序号
		/// </summary>
		[DataMember]
		[DisplayName("GUID对应的ID序号")]
		public virtual String Gid
		{
			get
			{
				return this.m_Gid;
			}
			set
			{
				this.m_Gid = value;
			}
		}

		/// <summary>
		/// 修改日期
		/// </summary>
		[DataMember]
		[DisplayName("修改日期")]
		public virtual DateTime ModDate
		{
			get
			{
				return this.m_ModDate;
			}
			set
			{
				this.m_ModDate = value;
			}
		}

		/// <summary>
		/// 修改版本
		/// </summary>
		[DataMember]
		[DisplayName("修改版本")]
		public virtual String ModVersion
		{
			get
			{
				return this.m_ModVersion;
			}
			set
			{
				this.m_ModVersion = value;
			}
		}

		/// <summary>
		/// 修改单号
		/// </summary>
		[DataMember]
		[DisplayName("修改单号")]
		public virtual String ModOrderId
		{
			get
			{
				return this.m_ModOrderId;
			}
			set
			{
				this.m_ModOrderId = value;
			}
		}

		/// <summary>
		/// 申请人
		/// </summary>
		[DataMember]
		[DisplayName("申请人")]
		public virtual String Proposer
		{
			get
			{
				return this.m_Proposer;
			}
			set
			{
				this.m_Proposer = value;
			}
		}

		/// <summary>
		/// 修改人
		/// </summary>
		[DataMember]
		[DisplayName("修改人")]
		public virtual String Programmer
		{
			get
			{
				return this.m_Programmer;
			}
			set
			{
				this.m_Programmer = value;
			}
		}

		/// <summary>
		/// 修改内容
		/// </summary>
		[DataMember]
		[DisplayName("修改内容")]
		public virtual String ModContent
		{
			get
			{
				return this.m_ModContent;
			}
			set
			{
				this.m_ModContent = value;
			}
		}

		/// <summary>
		/// 修改原因
		/// </summary>
		[DataMember]
		[DisplayName("修改原因")]
		public virtual String ModReason
		{
			get
			{
				return this.m_ModReason;
			}
			set
			{
				this.m_ModReason = value;
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
		public Int32 CompareTo(ModRecordInfo other)
		{
			if (other == null) return -1;
            if (ModDate > other.ModDate)
            {
                return -1;
            }
            return 1;
		}
		#endregion
	}
}