using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 收集用户信息(CollectionUserInfo)
	/// 对象号: 100095
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class CollectionUserInfo : BaseEntity
	{
		#region Field Members

		/// <summary>
		/// ID序号
		/// </summary>
		private Int32 m_Id = 0;

		/// <summary>
		/// 名称
		/// </summary>
		private String m_Name = string.Empty;

		/// <summary>
		/// 关系
		/// </summary>
		private String m_RelationShip = string.Empty;

		/// <summary>
		/// 性别
		/// </summary>
		private Int16 m_Gender = 0;

		/// <summary>
		/// 身份证
		/// </summary>
		private String m_IdCard = string.Empty;

		/// <summary>
		/// 手机
		/// </summary>
		private String m_MobilePhone = string.Empty;

		/// <summary>
		/// 户籍地
		/// </summary>
		private String m_HouseHold = string.Empty;

		/// <summary>
		/// 家庭地址
		/// </summary>
		private String m_HomeAddress = string.Empty;

		/// <summary>
		/// 是否去过湖北
		/// </summary>
		private String m_IsGoHubei = string.Empty;

		/// <summary>
		/// 是否去过温州
		/// </summary>
		private String m_IsGoWenzhou = string.Empty;

		/// <summary>
		/// 是否与疫情感染者密切接触
		/// </summary>
		private String m_IsContactHubeiWenzhou = string.Empty;

		/// <summary>
		/// 是否出租给湖北、温州人员
		/// </summary>
		private String m_IsLeaseHubeiWenzhou = string.Empty;

		/// <summary>
		/// 是否有湖北、温州人员来访
		/// </summary>
		private String m_IsHubeiWenzhouVisit = string.Empty;

		/// <summary>
		/// 是否实施居家观察
		/// </summary>
		private String m_IsHomeObser = string.Empty;
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
		/// 关系
		/// </summary>
		[DataMember]
		[DisplayName("关系")]
		public virtual String RelationShip
		{
			get
			{
				return this.m_RelationShip;
			}
			set
			{
				this.m_RelationShip = value;
			}
		}

		/// <summary>
		/// 性别
		/// 1-男,
		/// 2-女,
		/// 2-保密
		/// </summary>
		[DataMember]
		[DisplayName("性别")]
		public virtual Int16 Gender
		{
			get
			{
				return this.m_Gender;
			}
			set
			{
				this.m_Gender = value;
			}
		}

		/// <summary>
		/// 身份证
		/// </summary>
		[DataMember]
		[DisplayName("身份证")]
		public virtual String IdCard
		{
			get
			{
				return this.m_IdCard;
			}
			set
			{
				this.m_IdCard = value;
			}
		}

		/// <summary>
		/// 手机
		/// </summary>
		[DataMember]
		[DisplayName("手机")]
		public virtual String MobilePhone
		{
			get
			{
				return this.m_MobilePhone;
			}
			set
			{
				this.m_MobilePhone = value;
			}
		}

		/// <summary>
		/// 户籍地
		/// </summary>
		[DataMember]
		[DisplayName("户籍地")]
		public virtual String HouseHold
		{
			get
			{
				return this.m_HouseHold;
			}
			set
			{
				this.m_HouseHold = value;
			}
		}

		/// <summary>
		/// 家庭地址
		/// </summary>
		[DataMember]
		[DisplayName("家庭地址")]
		public virtual String HomeAddress
		{
			get
			{
				return this.m_HomeAddress;
			}
			set
			{
				this.m_HomeAddress = value;
			}
		}

		/// <summary>
		/// 是否去过湖北
		/// </summary>
		[DataMember]
		public virtual String IsGoHubei
		{
			get
			{
				return this.m_IsGoHubei;
			}
			set
			{
				this.m_IsGoHubei = value;
			}
		}

		/// <summary>
		/// 是否去过温州
		/// </summary>
		[DataMember]
		public virtual String IsGoWenzhou
		{
			get
			{
				return this.m_IsGoWenzhou;
			}
			set
			{
				this.m_IsGoWenzhou = value;
			}
		}

		/// <summary>
		/// 是否与疫情感染者密切接触
		/// </summary>
		[DataMember]
		public virtual String IsContactHubeiWenzhou
		{
			get
			{
				return this.m_IsContactHubeiWenzhou;
			}
			set
			{
				this.m_IsContactHubeiWenzhou = value;
			}
		}

		/// <summary>
		/// 是否出租给湖北、温州人员
		/// </summary>
		[DataMember]
		public virtual String IsLeaseHubeiWenzhou
		{
			get
			{
				return this.m_IsLeaseHubeiWenzhou;
			}
			set
			{
				this.m_IsLeaseHubeiWenzhou = value;
			}
		}

		/// <summary>
		/// 是否有湖北、温州人员来访
		/// </summary>
		[DataMember]
		public virtual String IsHubeiWenzhouVisit
		{
			get
			{
				return this.m_IsHubeiWenzhouVisit;
			}
			set
			{
				this.m_IsHubeiWenzhouVisit = value;
			}
		}

		/// <summary>
		/// 是否实施居家观察
		/// </summary>
		[DataMember]
		public virtual String IsHomeObser
		{
			get
			{
				return this.m_IsHomeObser;
			}
			set
			{
				this.m_IsHomeObser = value;
			}
		}
		#endregion
	}
}