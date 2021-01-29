using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 系统图表库(IconInfo)
	/// 对象号: 100084
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class IconInfo : BaseEntity
	{
		#region Field Members

		/// <summary>
		/// ID序号
		/// </summary>
		private Int32 m_Id = 0;

		/// <summary>
		/// Icon样式名称
		/// </summary>
		private String m_IconCls = string.Empty;

		/// <summary>
		/// Icon中URL地址
		/// </summary>
		private String m_IconUrl = string.Empty;

		/// <summary>
		/// Icon尺寸
		/// </summary>
		private Int32 m_IconSize = 0;

		/// <summary>
		/// 创建时间
		/// </summary>
		private DateTime m_CreatorTime = DateTime.Now;

		/// <summary>
		/// 名称
		/// </summary>
		private String m_Name = string.Empty;

		/// <summary>
		/// 中文名称
		/// </summary>
		private String m_ChineseName = string.Empty;

		/// <summary>
		/// 值
		/// </summary>
		private String m_StrValue = string.Empty;
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
		/// Icon样式名称
		/// </summary>
		[DataMember]
		[DisplayName("Icon样式名称")]
		public virtual String IconCls
		{
			get
			{
				return this.m_IconCls;
			}
			set
			{
				this.m_IconCls = value;
			}
		}

		/// <summary>
		/// Icon中URL地址
		/// </summary>
		[DataMember]
		[DisplayName("Icon中URL地址")]
		public virtual String IconUrl
		{
			get
			{
				return this.m_IconUrl;
			}
			set
			{
				this.m_IconUrl = value;
			}
		}

		/// <summary>
		/// Icon尺寸
		/// </summary>
		[DataMember]
		[DisplayName("Icon尺寸")]
		public virtual Int32 IconSize
		{
			get
			{
				return this.m_IconSize;
			}
			set
			{
				this.m_IconSize = value;
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
		/// 中文名称
		/// </summary>
		[DataMember]
		[DisplayName("中文名称")]
		public virtual String ChineseName
		{
			get
			{
				return this.m_ChineseName;
			}
			set
			{
				this.m_ChineseName = value;
			}
		}

		/// <summary>
		/// 值
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
		#endregion
	}
}