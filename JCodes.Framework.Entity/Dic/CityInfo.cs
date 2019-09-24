using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 城市信息(CityInfo)
	/// 对象号: 100014
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class CityInfo : BaseEntity
	{
		#region Field Members

		/// <summary>
		/// ID序号
		/// </summary>
		private Int32 m_Id = 0;

		/// <summary>
		/// 城市名字
		/// </summary>
		private String m_CityName = string.Empty;

		/// <summary>
		/// 邮政编码
		/// </summary>
		private String m_ZipCode = string.Empty;

		/// <summary>
		/// 省份Id
		/// </summary>
		private Int32 m_ProvinceId = 0;
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
		/// 城市名字
		/// </summary>
		[DataMember]
		[DisplayName("城市名字")]
		public virtual String CityName
		{
			get
			{
				return this.m_CityName;
			}
			set
			{
				this.m_CityName = value;
			}
		}

		/// <summary>
		/// 邮政编码
		/// </summary>
		[DataMember]
		[DisplayName("邮政编码")]
		public virtual String ZipCode
		{
			get
			{
				return this.m_ZipCode;
			}
			set
			{
				this.m_ZipCode = value;
			}
		}

		/// <summary>
		/// 省份Id
		/// </summary>
		[DataMember]
		[DisplayName("省份Id")]
		public virtual Int32 ProvinceId
		{
			get
			{
				return this.m_ProvinceId;
			}
			set
			{
				this.m_ProvinceId = value;
			}
		}
		#endregion
	}
}