using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 省份信息(ProvinceInfo)
	/// 对象号: 100020
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class ProvinceInfo : BaseEntity
	{
		#region Field Members

		/// <summary>
		/// ID序号
		/// </summary>
		private Int32 m_Id = 0;

		/// <summary>
		/// 省份名称
		/// </summary>
		private String m_ProvinceName = string.Empty;
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
		/// 省份名称
		/// </summary>
		[DataMember]
		[DisplayName("省份名称")]
		public virtual String ProvinceName
		{
			get
			{
				return this.m_ProvinceName;
			}
			set
			{
				this.m_ProvinceName = value;
			}
		}
		#endregion
	}
}