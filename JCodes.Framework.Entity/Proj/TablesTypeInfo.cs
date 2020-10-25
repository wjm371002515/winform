using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 表分类信息(TablesTypeInfo)
	/// 对象号: 100054
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class TablesTypeInfo
	{
		#region Field Members

		/// <summary>
		/// GUID对应的ID序号
		/// </summary>
		private String m_Gid = string.Empty;

		/// <summary>
		/// 创建时间
		/// </summary>
		private DateTime m_CreatorTime = DateTime.Now;

		/// <summary>
		/// 名称
		/// </summary>
		private String m_Name = string.Empty;

		/// <summary>
		/// 是否基础数据
		/// </summary>
		private Int16 m_IsBasicData = 0;
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
		/// 是否基础数据
		/// 1-是,
		/// 2-否
		/// </summary>
		[DataMember]
		[DisplayName("是否基础数据")]
		public virtual Int16 IsBasicData
		{
			get
			{
				return this.m_IsBasicData;
			}
			set
			{
				this.m_IsBasicData = value;
			}
		}
		#endregion
	}
}