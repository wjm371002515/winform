using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 系统开发时间轴(SystemTimeLineInfo)
	/// 对象号: 100107
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class SystemTimeLineInfo : BaseEntity
	{
		#region Field Members

		/// <summary>
		/// ID序号
		/// </summary>
		private Int32 m_Id = 0;

		/// <summary>
		/// 名称
		/// </summary>
		private String m_Name = string.Empty;

		/// <summary>
		/// 自定义文本
		/// </summary>
		private String m_CustomContent = string.Empty;

		/// <summary>
		/// Icon样式名称
		/// </summary>
		private String m_IconCls = string.Empty;

		/// <summary>
		/// 单位网站
		/// </summary>
		private String m_WebSiteUrl = string.Empty;

		/// <summary>
		/// 显示名称
		/// </summary>
		private String m_DisplayName = string.Empty;

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
		/// 自定义文本
		/// </summary>
		[DataMember]
		[DisplayName("自定义文本")]
		public virtual String CustomContent
		{
			get
			{
				return this.m_CustomContent;
			}
			set
			{
				this.m_CustomContent = value;
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
		/// 单位网站
		/// </summary>
		[DataMember]
		[DisplayName("单位网站")]
		public virtual String WebSiteUrl
		{
			get
			{
				return this.m_WebSiteUrl;
			}
			set
			{
				this.m_WebSiteUrl = value;
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