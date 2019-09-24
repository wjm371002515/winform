using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 返回结果对象(ReturnResult)
	/// 对象号: 100007
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class ReturnResult
	{
		#region Field Members

		/// <summary>
		/// 错误码
		/// </summary>
		private Int32 m_ErrorCode = 0;

		/// <summary>
		/// 错误信息
		/// </summary>
		private String m_ErrorMessage = string.Empty;

		/// <summary>
		/// 错误路由信息
		/// </summary>
		private String m_ErrorPath = string.Empty;

		/// <summary>
		/// 额外数据1
		/// </summary>
		private String m_Data1 = string.Empty;

		/// <summary>
		/// 额外数据2
		/// </summary>
		private String m_Data2 = string.Empty;

		/// <summary>
		/// 额外数据3
		/// </summary>
		private String m_Data3 = string.Empty;
		#endregion

		#region Property Members

		/// <summary>
		/// 错误码
		/// </summary>
		[DataMember]
		[DisplayName("错误码")]
		public virtual Int32 ErrorCode
		{
			get
			{
				return this.m_ErrorCode;
			}
			set
			{
				this.m_ErrorCode = value;
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

		/// <summary>
		/// 额外数据1
		/// </summary>
		[DataMember]
		[DisplayName("额外数据1")]
		public virtual String Data1
		{
			get
			{
				return this.m_Data1;
			}
			set
			{
				this.m_Data1 = value;
			}
		}

		/// <summary>
		/// 额外数据2
		/// </summary>
		[DataMember]
		[DisplayName("额外数据2")]
		public virtual String Data2
		{
			get
			{
				return this.m_Data2;
			}
			set
			{
				this.m_Data2 = value;
			}
		}

		/// <summary>
		/// 额外数据3
		/// </summary>
		[DataMember]
		[DisplayName("额外数据3")]
		public virtual String Data3
		{
			get
			{
				return this.m_Data3;
			}
			set
			{
				this.m_Data3 = value;
			}
		}
		#endregion
	}
}