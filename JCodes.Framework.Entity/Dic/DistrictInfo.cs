using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 地区信息(DistrictInfo)
	/// 对象号: 100019
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class DistrictInfo : BaseEntity
	{
		#region Field Members

		/// <summary>
		/// ID序号
		/// </summary>
		private Int32 m_Id = 0;

		/// <summary>
		/// 行政区划
		/// </summary>
		private String m_DistrictName = string.Empty;

		/// <summary>
		/// 城市Id
		/// </summary>
		private Int32 m_CityId = 0;
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
		/// 行政区划
		/// </summary>
		[DataMember]
		[DisplayName("行政区划")]
		public virtual String DistrictName
		{
			get
			{
				return this.m_DistrictName;
			}
			set
			{
				this.m_DistrictName = value;
			}
		}

		/// <summary>
		/// 城市Id
		/// </summary>
		[DataMember]
		[DisplayName("城市Id")]
		public virtual Int32 CityId
		{
			get
			{
				return this.m_CityId;
			}
			set
			{
				this.m_CityId = value;
			}
		}
		#endregion
	}
}