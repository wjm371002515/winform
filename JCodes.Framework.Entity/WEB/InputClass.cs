using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// WEB通用输入参数(InputClass)
	/// 对象号: 100104
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class InputClass : BaseEntity
	{
		#region Field Members

		/// <summary>
		/// 字段类型
		/// </summary>
		private String m_DataType = string.Empty;

		/// <summary>
		/// 输入参数
		/// </summary>
		private String m_InputParam = string.Empty;

		/// <summary>
		/// 时间戳
		/// </summary>
		private Int32 m_TimeStamp = 0;
		#endregion

		#region Property Members

		/// <summary>
		/// 字段类型
		/// </summary>
		[DataMember]
		[DisplayName("字段类型")]
		public virtual String DataType
		{
			get
			{
				return this.m_DataType;
			}
			set
			{
				this.m_DataType = value;
			}
		}

		/// <summary>
		/// 输入参数
		/// </summary>
		[DataMember]
		[DisplayName("输入参数")]
		public virtual String InputParam
		{
			get
			{
				return this.m_InputParam;
			}
			set
			{
				this.m_InputParam = value;
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
		#endregion
	}
}