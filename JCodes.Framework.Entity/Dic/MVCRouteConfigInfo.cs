using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// MVC路由配置表(MVCRouteConfigInfo)
	/// 对象号: 100103
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class MVCRouteConfigInfo : BaseEntity
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
		/// URL地址
		/// </summary>
		private String m_Url = string.Empty;

		/// <summary>
		/// 模块
		/// </summary>
		private String m_ModuleInfo = string.Empty;

		/// <summary>
		/// 操作
		/// </summary>
		private String m_OperationInfo = string.Empty;
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
		/// URL地址
		/// </summary>
		[DataMember]
		[DisplayName("URL地址")]
		public virtual String Url
		{
			get
			{
				return this.m_Url;
			}
			set
			{
				this.m_Url = value;
			}
		}

		/// <summary>
		/// 模块
		/// </summary>
		[DataMember]
		[DisplayName("模块")]
		public virtual String ModuleInfo
		{
			get
			{
				return this.m_ModuleInfo;
			}
			set
			{
				this.m_ModuleInfo = value;
			}
		}

		/// <summary>
		/// 操作
		/// </summary>
		[DataMember]
		[DisplayName("操作")]
		public virtual String OperationInfo
		{
			get
			{
				return this.m_OperationInfo;
			}
			set
			{
				this.m_OperationInfo = value;
			}
		}
		#endregion
	}
}