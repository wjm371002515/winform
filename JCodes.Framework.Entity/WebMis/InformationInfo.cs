using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 政策法规公告动态(InformationInfo)
	/// 对象号: 100085
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class InformationInfo : BaseEntity
	{
		#region Field Members

		/// <summary>
		/// ID序号
		/// </summary>
		private Int32 m_Id = 0;

		/// <summary>
		/// 信息标题
		/// </summary>
		private String m_InformationTitle = string.Empty;

		/// <summary>
		/// 自定义文本
		/// </summary>
		private String m_CustomContent = string.Empty;

		/// <summary>
		/// 附件GUID
		/// </summary>
		private String m_AttachmentGid = string.Empty;

		/// <summary>
		/// 信息大类名称
		/// </summary>
		private Int16 m_InformationCategory = 0;

		/// <summary>
		/// 信息子类
		/// </summary>
		private Int16 m_InformationSubType = 0;

		/// <summary>
		/// 编辑人编号
		/// </summary>
		private Int32 m_EditorId = 0;

		/// <summary>
		/// 最后更新时间
		/// </summary>
		private DateTime m_LastUpdateTime = DateTime.Now;

		/// <summary>
		/// 是否审批通过
		/// </summary>
		private Int16 m_IsApprove = 0;

		/// <summary>
		/// 审批人编号
		/// </summary>
		private Int32 m_ApproveId = 0;

		/// <summary>
		/// 审批时间
		/// </summary>
		private DateTime m_ApproveTime = DateTime.Now;

		/// <summary>
		/// 是否强制过期
		/// </summary>
		private Int16 m_IsForceExpire = 0;

		/// <summary>
		/// 过期截止时间
		/// </summary>
		private DateTime m_TimeOut = DateTime.Now;
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
		/// 信息标题
		/// </summary>
		[DataMember]
		[DisplayName("信息标题")]
		public virtual String InformationTitle
		{
			get
			{
				return this.m_InformationTitle;
			}
			set
			{
				this.m_InformationTitle = value;
			}
		}

		/// <summary>
		/// 自定义文本
		/// </summary>
		[DataMember]
		[DisplayName("自定义文本")]
		public virtual String CustomContent
		{
			get
			{
				return this.m_CustomContent;
			}
			set
			{
				this.m_CustomContent = value;
			}
		}

		/// <summary>
		/// 附件GUID
		/// </summary>
		[DataMember]
		[DisplayName("附件GUID")]
		public virtual String AttachmentGid
		{
			get
			{
				return this.m_AttachmentGid;
			}
			set
			{
				this.m_AttachmentGid = value;
			}
		}

		/// <summary>
		/// 信息大类名称
		/// </summary>
		[DataMember]
		[DisplayName("信息大类名称")]
		public virtual Int16 InformationCategory
		{
			get
			{
				return this.m_InformationCategory;
			}
			set
			{
				this.m_InformationCategory = value;
			}
		}

		/// <summary>
		/// 信息子类
		/// </summary>
		[DataMember]
		[DisplayName("信息子类")]
		public virtual Int16 InformationSubType
		{
			get
			{
				return this.m_InformationSubType;
			}
			set
			{
				this.m_InformationSubType = value;
			}
		}

		/// <summary>
		/// 编辑人编号
		/// </summary>
		[DataMember]
		[DisplayName("编辑人编号")]
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
		[DisplayName("最后更新时间")]
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
		/// 是否审批通过
		/// </summary>
		[DataMember]
		[DisplayName("是否审批通过")]
		public virtual Int16 IsApprove
		{
			get
			{
				return this.m_IsApprove;
			}
			set
			{
				this.m_IsApprove = value;
			}
		}

		/// <summary>
		/// 审批人编号
		/// </summary>
		[DataMember]
		[DisplayName("审批人编号")]
		public virtual Int32 ApproveId
		{
			get
			{
				return this.m_ApproveId;
			}
			set
			{
				this.m_ApproveId = value;
			}
		}

		/// <summary>
		/// 审批时间
		/// </summary>
		[DataMember]
		[DisplayName("审批时间")]
		public virtual DateTime ApproveTime
		{
			get
			{
				return this.m_ApproveTime;
			}
			set
			{
				this.m_ApproveTime = value;
			}
		}

		/// <summary>
		/// 是否强制过期
		/// </summary>
		[DataMember]
		[DisplayName("是否强制过期")]
		public virtual Int16 IsForceExpire
		{
			get
			{
				return this.m_IsForceExpire;
			}
			set
			{
				this.m_IsForceExpire = value;
			}
		}

		/// <summary>
		/// 过期截止时间
		/// </summary>
		[DataMember]
		[DisplayName("过期截止时间")]
		public virtual DateTime TimeOut
		{
			get
			{
				return this.m_TimeOut;
			}
			set
			{
				this.m_TimeOut = value;
			}
		}
		#endregion
	}
}