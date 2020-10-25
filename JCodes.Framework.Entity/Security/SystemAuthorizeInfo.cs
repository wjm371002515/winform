using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 系统授权认证信息(SystemAuthorizeInfo)
	/// 对象号: 100097
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class SystemAuthorizeInfo : BaseEntity
	{
		#region Field Members

		/// <summary>
		/// ID序号
		/// </summary>
		private Int32 m_Id = 0;

		/// <summary>
		/// 系统编号
		/// </summary>
		private String m_SystemtypeId = string.Empty;

		/// <summary>
		/// 许可证
		/// </summary>
		private String m_Licence = string.Empty;
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

		/// <summary>
		/// 许可证
		/// </summary>
		[DataMember]
		[DisplayName("许可证")]
		public virtual String Licence
		{
			get
			{
				return this.m_Licence;
			}
			set
			{
				this.m_Licence = value;
			}
		}
		#endregion
	}
}