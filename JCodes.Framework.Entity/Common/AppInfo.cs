using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 应用信息(AppInfo)
	/// 对象号: 100093
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class AppInfo
	{
		#region Field Members

		/// <summary>
		/// 应用单元
		/// </summary>
		private String m_AppUnit = string.Empty;

		/// <summary>
		/// 应用名称
		/// </summary>
		private String m_AppName = string.Empty;

		/// <summary>
		/// 应用程序全部名称
		/// </summary>
		private String m_AppWholeName = string.Empty;

		/// <summary>
		/// 系统编号
		/// </summary>
		private String m_SystemtypeId = string.Empty;
		#endregion

		#region Property Members

		/// <summary>
		/// 应用单元
		/// </summary>
		[DataMember]
		[DisplayName("应用单元")]
		public virtual String AppUnit
		{
			get
			{
				return this.m_AppUnit;
			}
			set
			{
				this.m_AppUnit = value;
			}
		}

		/// <summary>
		/// 应用名称
		/// </summary>
		[DataMember]
		[DisplayName("应用名称")]
		public virtual String AppName
		{
			get
			{
				return this.m_AppName;
			}
			set
			{
				this.m_AppName = value;
			}
		}

		/// <summary>
		/// 应用程序全部名称
		/// </summary>
		[DataMember]
		[DisplayName("应用程序全部名称")]
		public virtual String AppWholeName
		{
			get
			{
				return this.m_AppWholeName;
			}
			set
			{
				this.m_AppWholeName = value;
			}
		}

		/// <summary>
		/// 系统编号
		/// </summary>
		[DataMember]
		[DisplayName("系统编号")]
		public virtual String SystemtypeId
		{
			get
			{
				return this.m_SystemtypeId;
			}
			set
			{
				this.m_SystemtypeId = value;
			}
		}
		#endregion
	}
}