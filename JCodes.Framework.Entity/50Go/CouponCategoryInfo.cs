using System;
using System.Runtime.Serialization;
using JCodes.Framework.Entity;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 优惠券分类信息
	/// </summary>
	[Serializable]
	[DataContract]
	public class CouponCategoryInfo : BaseEntity 
	{
		#region Field Members

		/// <summary>
		/// ID序号
		/// </summary>
		private Int32 m_Id = 0;

		/// <summary>
		/// 通用编码
		/// </summary>
		private String m_GeneralCode = string.Empty;

		/// <summary>
		/// 名称
		/// </summary>
		private String m_Name = string.Empty;

		/// <summary>
		/// 公司列表
		/// </summary>
		private String m_CompanyLst = string.Empty;

		/// <summary>
		/// 创建人ID
		/// </summary>
		private Int32 m_CreatorId = 0;

		/// <summary>
		/// 创建时间
		/// </summary>
		private DateTime m_CreatorTime = DateTime.Now;

		/// <summary>
		/// 编辑人ID
		/// </summary>
		private Int32 m_EditorId = 0;

		/// <summary>
		/// 最后更新时间
		/// </summary>
		private DateTime m_LastUpdateTime = DateTime.Now;

		/// <summary>
		/// 是否禁用
		/// </summary>
		private Int16 m_IsForbid = 0;
		#endregion

		#region Property Members

		/// <summary>
		/// ID序号
		/// </summary>
		[DataMember]
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
		/// 通用编码
		/// </summary>
		[DataMember]
		public virtual String GeneralCode
		{
			get
			{
				return this.m_GeneralCode;
			}
			set
			{
				this.m_GeneralCode = value;
			}
		}

		/// <summary>
		/// 名称
		/// </summary>
		[DataMember]
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
		/// 公司列表
		/// </summary>
		[DataMember]
		public virtual String CompanyLst
		{
			get
			{
				return this.m_CompanyLst;
			}
			set
			{
				this.m_CompanyLst = value;
			}
		}

		/// <summary>
		/// 创建人ID
		/// </summary>
		[DataMember]
		public virtual Int32 CreatorId
		{
			get
			{
				return this.m_CreatorId;
			}
			set
			{
				this.m_CreatorId = value;
			}
		}

		/// <summary>
		/// 创建时间
		/// </summary>
		[DataMember]
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
		/// 编辑人ID
		/// </summary>
		[DataMember]
		public virtual Int32 EditorId
		{
			get
			{
				return this.m_EditorId;
			}
			set
			{
				this.m_EditorId = value;
			}
		}

		/// <summary>
		/// 最后更新时间
		/// </summary>
		[DataMember]
		public virtual DateTime LastUpdateTime
		{
			get
			{
				return this.m_LastUpdateTime;
			}
			set
			{
				this.m_LastUpdateTime = value;
			}
		}

		/// <summary>
		/// 是否禁用
		/// </summary>
		[DataMember]
		public virtual Int16 IsForbid
		{
			get
			{
				return this.m_IsForbid;
			}
			set
			{
				this.m_IsForbid = value;
			}
		}
		#endregion
	}
}