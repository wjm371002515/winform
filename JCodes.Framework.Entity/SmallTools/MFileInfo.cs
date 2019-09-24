using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 文件信息类(MFileInfo)
	/// 对象号: 100090
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class MFileInfo
	{
		#region Field Members

		/// <summary>
		/// 文件名称
		/// </summary>
		private String m_FileName = string.Empty;

		/// <summary>
		/// 处理状态
		/// </summary>
		private Int16 m_DealStatus = 0;
		#endregion

		#region Property Members

		/// <summary>
		/// 文件名称
		/// </summary>
		[DataMember]
		[DisplayName("文件名称")]
		public virtual String FileName
		{
			get
			{
				return this.m_FileName;
			}
			set
			{
				this.m_FileName = value;
			}
		}

		/// <summary>
		/// 处理状态
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
		#endregion
	}
}