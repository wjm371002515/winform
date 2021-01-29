using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 前台日志(FrontLogInfo)
	/// 对象号: 100105
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class FrontLogInfo : BaseEntity
	{
		#region Field Members

		/// <summary>
		/// ID序号
		/// </summary>
		private Int32 m_Id = 0;

		/// <summary>
		/// 错误信息
		/// </summary>
		private String m_ErrorMessage = string.Empty;

		/// <summary>
		/// 错误行号
		/// </summary>
		private Int32 m_ErrorLineNo = 0;

		/// <summary>
		/// 错误列号
		/// </summary>
		private Int32 m_ErrorColumNo = 0;

		/// <summary>
		/// 时间戳
		/// </summary>
		private Int32 m_TimeStamp = 0;

		/// <summary>
		/// 错误路由信息
		/// </summary>
		private String m_ErrorPath = string.Empty;
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
		/// 错误信息
		/// </summary>
		[DataMember]
		[DisplayName("错误信息")]
		public virtual String ErrorMessage
		{
			get
			{
				return this.m_ErrorMessage;
			}
			set
			{
				this.m_ErrorMessage = value;
			}
		}

		/// <summary>
		/// 错误行号
		/// </summary>
		[DataMember]
		[DisplayName("错误行号")]
		public virtual Int32 ErrorLineNo
		{
			get
			{
				return this.m_ErrorLineNo;
			}
			set
			{
				this.m_ErrorLineNo = value;
			}
		}

		/// <summary>
		/// 错误列号
		/// </summary>
		[DataMember]
		[DisplayName("错误列号")]
		public virtual Int32 ErrorColumNo
		{
			get
			{
				return this.m_ErrorColumNo;
			}
			set
			{
				this.m_ErrorColumNo = value;
			}
		}

		/// <summary>
		/// 时间戳
		/// </summary>
		[DataMember]
		[DisplayName("时间戳")]
		public virtual Int32 TimeStamp
		{
			get
			{
				return this.m_TimeStamp;
			}
			set
			{
				this.m_TimeStamp = value;
			}
		}

		/// <summary>
		/// 错误路由信息
		/// </summary>
		[DataMember]
		[DisplayName("错误路由信息")]
		public virtual String ErrorPath
		{
			get
			{
				return this.m_ErrorPath;
			}
			set
			{
				this.m_ErrorPath = value;
			}
		}
		#endregion
	}
}