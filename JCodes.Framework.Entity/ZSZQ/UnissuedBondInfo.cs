using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 已取得批文未发行债券项目信息(UnissuedBondInfo)
	/// 对象号: 100101
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class UnissuedBondInfo : BaseEntity
	{
		#region Field Members

		/// <summary>
		/// ID序号
		/// </summary>
		private Int32 m_Id = 0;

		/// <summary>
		/// 报监管部门在审的项目名称
		/// </summary>
		private String m_ProjectName = string.Empty;

		/// <summary>
		/// 募集资金预计额（亿）
		/// </summary>
		private String m_RaisedAmount = string.Empty;

		/// <summary>
		/// 项目类型
		/// </summary>
		private String m_ProjectType = string.Empty;

		/// <summary>
		/// 项目负责人
		/// </summary>
		private String m_ProjectLeader = string.Empty;

		/// <summary>
		/// 承销商/管理人
		/// </summary>
		private String m_Managers = string.Empty;

		/// <summary>
		/// 交易所确认文件文号
		/// </summary>
		private String m_DocNum = string.Empty;

		/// <summary>
		/// 项目状态跟踪
		/// </summary>
		private String m_ProjectStatusDetail = string.Empty;

		/// <summary>
		/// 部门Id
		/// </summary>
		private Int32 m_DeptId = 0;

		/// <summary>
		/// 部门名字
		/// </summary>
		private String m_DeptName = string.Empty;

		/// <summary>
		/// 项目进度
		/// </summary>
		private String m_ProjectProgress = string.Empty;

		/// <summary>
		/// 最新项目状态
		/// </summary>
		private String m_ProjectStatus = string.Empty;

		/// <summary>
		/// 申报时间
		/// </summary>
		private String m_DeclareTime = string.Empty;

		/// <summary>
		/// 信息来源
		/// </summary>
		private String m_From = string.Empty;

		/// <summary>
		/// 备注
		/// </summary>
		private String m_Remark = string.Empty;
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
		/// 报监管部门在审的项目名称
		/// </summary>
		[DataMember]
		[DisplayName("报监管部门在审的项目名称")]
		public virtual String ProjectName
		{
			get
			{
				return this.m_ProjectName;
			}
			set
			{
				this.m_ProjectName = value;
			}
		}

		/// <summary>
		/// 募集资金预计额（亿）
		/// </summary>
		[DataMember]
		[DisplayName("募集资金预计额（亿）")]
		public virtual String RaisedAmount
		{
			get
			{
				return this.m_RaisedAmount;
			}
			set
			{
				this.m_RaisedAmount = value;
			}
		}

		/// <summary>
		/// 项目类型
		/// </summary>
		[DataMember]
		[DisplayName("项目类型")]
		public virtual String ProjectType
		{
			get
			{
				return this.m_ProjectType;
			}
			set
			{
				this.m_ProjectType = value;
			}
		}

		/// <summary>
		/// 项目负责人
		/// </summary>
		[DataMember]
		[DisplayName("项目负责人")]
		public virtual String ProjectLeader
		{
			get
			{
				return this.m_ProjectLeader;
			}
			set
			{
				this.m_ProjectLeader = value;
			}
		}

		/// <summary>
		/// 承销商/管理人
		/// </summary>
		[DataMember]
		[DisplayName("承销商/管理人")]
		public virtual String Managers
		{
			get
			{
				return this.m_Managers;
			}
			set
			{
				this.m_Managers = value;
			}
		}

		/// <summary>
		/// 交易所确认文件文号
		/// </summary>
		[DataMember]
		[DisplayName("交易所确认文件文号")]
		public virtual String DocNum
		{
			get
			{
				return this.m_DocNum;
			}
			set
			{
				this.m_DocNum = value;
			}
		}

		/// <summary>
		/// 项目状态跟踪
		/// </summary>
		[DataMember]
		[DisplayName("项目状态跟踪")]
		public virtual String ProjectStatusDetail
		{
			get
			{
				return this.m_ProjectStatusDetail;
			}
			set
			{
				this.m_ProjectStatusDetail = value;
			}
		}

		/// <summary>
		/// 部门Id
		/// </summary>
		[DataMember]
		[DisplayName("部门Id")]
		public virtual Int32 DeptId
		{
			get
			{
				return this.m_DeptId;
			}
			set
			{
				this.m_DeptId = value;
			}
		}

		/// <summary>
		/// 部门名字
		/// </summary>
		[DataMember]
		[DisplayName("部门名字")]
		public virtual String DeptName
		{
			get
			{
				return this.m_DeptName;
			}
			set
			{
				this.m_DeptName = value;
			}
		}

		/// <summary>
		/// 项目进度
		/// </summary>
		[DataMember]
		[DisplayName("项目进度")]
		public virtual String ProjectProgress
		{
			get
			{
				return this.m_ProjectProgress;
			}
			set
			{
				this.m_ProjectProgress = value;
			}
		}

		/// <summary>
		/// 最新项目状态
		/// </summary>
		[DataMember]
		[DisplayName("最新项目状态")]
		public virtual String ProjectStatus
		{
			get
			{
				return this.m_ProjectStatus;
			}
			set
			{
				this.m_ProjectStatus = value;
			}
		}

		/// <summary>
		/// 申报时间
		/// </summary>
		[DataMember]
		[DisplayName("申报时间")]
		public virtual String DeclareTime
		{
			get
			{
				return this.m_DeclareTime;
			}
			set
			{
				this.m_DeclareTime = value;
			}
		}

		/// <summary>
		/// 信息来源
		/// </summary>
		[DataMember]
		[DisplayName("信息来源")]
		public virtual String From
		{
			get
			{
				return this.m_From;
			}
			set
			{
				this.m_From = value;
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