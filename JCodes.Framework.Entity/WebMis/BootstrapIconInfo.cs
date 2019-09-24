using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 基于BootStrap的图标(BootstrapIconInfo)
	/// 对象号: 100083
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class BootstrapIconInfo : BaseEntity
	{
		#region Field Members

		/// <summary>
		/// ID序号
		/// </summary>
		private Int32 m_Id = 0;

		/// <summary>
		/// 显示名称
		/// </summary>
		private String m_DisplayName = string.Empty;

		/// <summary>
		/// 样式名称
		/// </summary>
		private String m_ClassName = string.Empty;

		/// <summary>
		/// Icon来源
		/// </summary>
		private Int16 m_IconSourceType = 0;

		/// <summary>
		/// 创建时间
		/// </summary>
		private DateTime m_CreatorTime = DateTime.Now;
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
		/// 显示名称
		/// </summary>
		[DataMember]
		[DisplayName("显示名称")]
		public virtual String DisplayName
		{
			get
			{
				return this.m_DisplayName;
			}
			set
			{
				this.m_DisplayName = value;
			}
		}

		/// <summary>
		/// 样式名称
		/// </summary>
		[DataMember]
		[DisplayName("样式名称")]
		public virtual String ClassName
		{
			get
			{
				return this.m_ClassName;
			}
			set
			{
				this.m_ClassName = value;
			}
		}

		/// <summary>
		/// Icon来源
		/// </summary>
		[DataMember]
		[DisplayName("Icon来源")]
		public virtual Int16 IconSourceType
		{
			get
			{
				return this.m_IconSourceType;
			}
			set
			{
				this.m_IconSourceType = value;
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
		#endregion
	}
}