using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 已取得批文未发行债券项目更新状态数据(UnissuedBondUpdateDataInfo)
	/// 对象号: 100100
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class UnissuedBondUpdateDataInfo : BaseEntity
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
		/// 年月日
		/// </summary>
		private Int32 m_Date = 0;
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
		/// 年月日
		/// </summary>
		[DataMember]
		[DisplayName("年月日")]
		public virtual Int32 Date
		{
			get
			{
				return this.m_Date;
			}
			set
			{
				this.m_Date = value;
			}
		}
		#endregion
	}
}