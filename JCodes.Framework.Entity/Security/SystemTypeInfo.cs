using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 系统标识信息(SystemTypeInfo)
	/// 对象号: 100065
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class SystemTypeInfo : BaseEntity
	{
		#region Field Members

		/// <summary>
		/// GUID对应的ID序号
		/// </summary>
		private String m_Gid = string.Empty;

		/// <summary>
		/// 名称
		/// </summary>
		private String m_Name = string.Empty;

		/// <summary>
		/// 客户编码
		/// </summary>
		private String m_ConsumerCode = string.Empty;

		/// <summary>
		/// 许可证
		/// </summary>
		private String m_Licence = string.Empty;

		/// <summary>
		/// 备注
		/// </summary>
		private String m_Remark = string.Empty;
		#endregion

		#region Property Members

		/// <summary>
		/// GUID对应的ID序号
		/// </summary>
		[DataMember]
		[DisplayName("GUID对应的ID序号")]
		public virtual String Gid
		{
			get
			{
				return this.m_Gid;
			}
			set
			{
				this.m_Gid = value;
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
		/// 客户编码
		/// </summary>
		[DataMember]
		[DisplayName("客户编码")]
		public virtual String ConsumerCode
		{
			get
			{
				return this.m_ConsumerCode;
			}
			set
			{
				this.m_ConsumerCode = value;
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

		/// <summary>
		/// 备注
		/// </summary>
		[DataMember]
		[DisplayName("备注")]
		public virtual String Remark
		{
			get
			{
				return this.m_Remark;
			}
			set
			{
				this.m_Remark = value;
			}
		}
		#endregion
	}
}