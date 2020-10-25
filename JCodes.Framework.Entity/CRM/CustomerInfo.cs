using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 客户基本资料(CustomerInfo)
	/// 对象号: 100012
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class CustomerInfo : BaseEntity
	{
		#region Field Members

		/// <summary>
		/// ID序号
		/// </summary>
		private Int32 m_Id = 0;

		/// <summary>
		/// 用户编码
		/// </summary>
		private String m_UserCode = string.Empty;

		/// <summary>
		/// 名称
		/// </summary>
		private String m_Name = string.Empty;

		/// <summary>
		/// 登录名
		/// </summary>
		private String m_LoginName = string.Empty;

		/// <summary>
		/// 省份名称
		/// </summary>
		private String m_ProvinceName = string.Empty;

		/// <summary>
		/// 城市名字
		/// </summary>
		private String m_CityName = string.Empty;

		/// <summary>
		/// 行政区划
		/// </summary>
		private String m_DistrictName = string.Empty;

		/// <summary>
		/// 市场区域
		/// </summary>
		private String m_MarketArea = string.Empty;

		/// <summary>
		/// 工作地址
		/// </summary>
		private String m_WorkAddress = string.Empty;

		/// <summary>
		/// 邮政编码
		/// </summary>
		private String m_ZipCode = string.Empty;

		/// <summary>
		/// 手机
		/// </summary>
		private String m_MobilePhone = string.Empty;

		/// <summary>
		/// 传真号码
		/// </summary>
		private String m_Fax = string.Empty;

		/// <summary>
		/// 联系人
		/// </summary>
		private String m_Contacts = string.Empty;

		/// <summary>
		/// 联系人电话
		/// </summary>
		private String m_ContactPhone = string.Empty;

		/// <summary>
		/// Email邮箱
		/// </summary>
		private String m_Email = string.Empty;

		/// <summary>
		/// QQ号
		/// </summary>
		private Int32 m_QQ = 0;

		/// <summary>
		/// 所属行业
		/// </summary>
		private String m_Industry = string.Empty;

		/// <summary>
		/// 经营范围
		/// </summary>
		private String m_BusinessScope = string.Empty;

		/// <summary>
		/// 经营品牌
		/// </summary>
		private String m_Brand = string.Empty;

		/// <summary>
		/// 主要客户群
		/// </summary>
		private String m_PrimaryClient = string.Empty;

		/// <summary>
		/// 主营业务
		/// </summary>
		private String m_PrimaryBusiness = string.Empty;

		/// <summary>
		/// 注册资金 
		/// </summary>
		private Double m_RegisterCapital = 0.0;

		/// <summary>
		/// 营业额
		/// </summary>
		private Double m_TurnOver = 0.0;

		/// <summary>
		/// 营业执照
		/// </summary>
		private String m_LicenseNo = string.Empty;

		/// <summary>
		/// 开户银行
		/// </summary>
		private String m_OpenBank = string.Empty;

		/// <summary>
		/// 银行账号
		/// </summary>
		private String m_BankAccount = string.Empty;

		/// <summary>
		/// 地税登记号
		/// </summary>
		private String m_LocalTaxNo = string.Empty;

		/// <summary>
		/// 国税登记号
		/// </summary>
		private String m_NationalTaxNo = string.Empty;

		/// <summary>
		/// 法人名称
		/// </summary>
		private String m_LegalMan = string.Empty;

		/// <summary>
		/// 法人电话
		/// </summary>
		private String m_LegalPhone = string.Empty;

		/// <summary>
		/// 客户来源
		/// </summary>
		private String m_CustomerSource = string.Empty;

		/// <summary>
		/// 单位网站
		/// </summary>
		private String m_WebSiteUrl = string.Empty;

		/// <summary>
		/// 客户类别
		/// </summary>
		private Int16 m_CustomerType = 0;

		/// <summary>
		/// 客户级别
		/// </summary>
		private Int16 m_CustomerLevel = 0;

		/// <summary>
		/// 信用等级
		/// </summary>
		private Int16 m_CreditStatus = 0;

		/// <summary>
		/// 重要级别
		/// </summary>
		private Int16 m_ImportanceLevel = 0;

		/// <summary>
		/// 是否公开
		/// </summary>
		private Int16 m_IsPublic = 0;

		/// <summary>
		/// 客户满意度级别
		/// </summary>
		private Int16 m_SatisfactionLevel = 0;

		/// <summary>
		/// 备注
		/// </summary>
		private String m_Remark = string.Empty;

		/// <summary>
		/// 交易次数
		/// </summary>
		private Int32 m_TransactionCount = 0;

		/// <summary>
		/// 交易金额
		/// </summary>
		private Double m_TransactionTotal = 0.0;

		/// <summary>
		/// 首次交易时间
		/// </summary>
		private DateTime m_TransactionFirstDate = DateTime.Now;

		/// <summary>
		/// 最近交易时间
		/// </summary>
		private DateTime m_TransactionLastDate = DateTime.Now;

		/// <summary>
		/// 最近联系日期
		/// </summary>
		private DateTime m_LastContactDate = DateTime.Now;

		/// <summary>
		/// 客户阶段
		/// </summary>
		private String m_CustomerStage = string.Empty;

		/// <summary>
		/// 审核状态
		/// </summary>
		private Int16 m_AuditStatus = 0;

		/// <summary>
		/// 创建人编号
		/// </summary>
		private Int32 m_CreatorId = 0;

		/// <summary>
		/// 创建时间
		/// </summary>
		private DateTime m_CreatorTime = DateTime.Now;

		/// <summary>
		/// 编辑人编号
		/// </summary>
		private Int32 m_EditorId = 0;

		/// <summary>
		/// 最后更新时间
		/// </summary>
		private DateTime m_LastUpdateTime = DateTime.Now;

		/// <summary>
		/// 是否删除
		/// </summary>
		private Int16 m_IsDelete = 0;

		/// <summary>
		/// 部门Id
		/// </summary>
		private Int32 m_DeptId = 0;

		/// <summary>
		/// 公司Id
		/// </summary>
		private Int32 m_CompanyId = 0;
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
		/// 用户编码
		/// </summary>
		[DataMember]
		[DisplayName("用户编码")]
		public virtual String UserCode
		{
			get
			{
				return this.m_UserCode;
			}
			set
			{
				this.m_UserCode = value;
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
		/// 登录名
		/// </summary>
		[DataMember]
		[DisplayName("登录名")]
		public virtual String LoginName
		{
			get
			{
				return this.m_LoginName;
			}
			set
			{
				this.m_LoginName = value;
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
		/// 市场区域
		/// </summary>
		[DataMember]
		[DisplayName("市场区域")]
		public virtual String MarketArea
		{
			get
			{
				return this.m_MarketArea;
			}
			set
			{
				this.m_MarketArea = value;
			}
		}

		/// <summary>
		/// 工作地址
		/// </summary>
		[DataMember]
		[DisplayName("工作地址")]
		public virtual String WorkAddress
		{
			get
			{
				return this.m_WorkAddress;
			}
			set
			{
				this.m_WorkAddress = value;
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
		/// 传真号码
		/// </summary>
		[DataMember]
		[DisplayName("传真号码")]
		public virtual String Fax
		{
			get
			{
				return this.m_Fax;
			}
			set
			{
				this.m_Fax = value;
			}
		}

		/// <summary>
		/// 联系人
		/// </summary>
		[DataMember]
		[DisplayName("联系人")]
		public virtual String Contacts
		{
			get
			{
				return this.m_Contacts;
			}
			set
			{
				this.m_Contacts = value;
			}
		}

		/// <summary>
		/// 联系人电话
		/// </summary>
		[DataMember]
		[DisplayName("联系人电话")]
		public virtual String ContactPhone
		{
			get
			{
				return this.m_ContactPhone;
			}
			set
			{
				this.m_ContactPhone = value;
			}
		}

		/// <summary>
		/// Email邮箱
		/// </summary>
		[DataMember]
		[DisplayName("Email邮箱")]
		public virtual String Email
		{
			get
			{
				return this.m_Email;
			}
			set
			{
				this.m_Email = value;
			}
		}

		/// <summary>
		/// QQ号
		/// </summary>
		[DataMember]
		[DisplayName("QQ号")]
		public virtual Int32 QQ
		{
			get
			{
				return this.m_QQ;
			}
			set
			{
				this.m_QQ = value;
			}
		}

		/// <summary>
		/// 所属行业
		/// </summary>
		[DataMember]
		[DisplayName("所属行业")]
		public virtual String Industry
		{
			get
			{
				return this.m_Industry;
			}
			set
			{
				this.m_Industry = value;
			}
		}

		/// <summary>
		/// 经营范围
		/// </summary>
		[DataMember]
		[DisplayName("经营范围")]
		public virtual String BusinessScope
		{
			get
			{
				return this.m_BusinessScope;
			}
			set
			{
				this.m_BusinessScope = value;
			}
		}

		/// <summary>
		/// 经营品牌
		/// </summary>
		[DataMember]
		[DisplayName("经营品牌")]
		public virtual String Brand
		{
			get
			{
				return this.m_Brand;
			}
			set
			{
				this.m_Brand = value;
			}
		}

		/// <summary>
		/// 主要客户群
		/// </summary>
		[DataMember]
		[DisplayName("主要客户群")]
		public virtual String PrimaryClient
		{
			get
			{
				return this.m_PrimaryClient;
			}
			set
			{
				this.m_PrimaryClient = value;
			}
		}

		/// <summary>
		/// 主营业务
		/// </summary>
		[DataMember]
		[DisplayName("主营业务")]
		public virtual String PrimaryBusiness
		{
			get
			{
				return this.m_PrimaryBusiness;
			}
			set
			{
				this.m_PrimaryBusiness = value;
			}
		}

		/// <summary>
		/// 注册资金 
		/// </summary>
		[DataMember]
		[DisplayName("注册资金 ")]
		public virtual Double RegisterCapital
		{
			get
			{
				return this.m_RegisterCapital;
			}
			set
			{
				this.m_RegisterCapital = value;
			}
		}

		/// <summary>
		/// 营业额
		/// </summary>
		[DataMember]
		[DisplayName("营业额")]
		public virtual Double TurnOver
		{
			get
			{
				return this.m_TurnOver;
			}
			set
			{
				this.m_TurnOver = value;
			}
		}

		/// <summary>
		/// 营业执照
		/// </summary>
		[DataMember]
		[DisplayName("营业执照")]
		public virtual String LicenseNo
		{
			get
			{
				return this.m_LicenseNo;
			}
			set
			{
				this.m_LicenseNo = value;
			}
		}

		/// <summary>
		/// 开户银行
		/// </summary>
		[DataMember]
		[DisplayName("开户银行")]
		public virtual String OpenBank
		{
			get
			{
				return this.m_OpenBank;
			}
			set
			{
				this.m_OpenBank = value;
			}
		}

		/// <summary>
		/// 银行账号
		/// </summary>
		[DataMember]
		[DisplayName("银行账号")]
		public virtual String BankAccount
		{
			get
			{
				return this.m_BankAccount;
			}
			set
			{
				this.m_BankAccount = value;
			}
		}

		/// <summary>
		/// 地税登记号
		/// </summary>
		[DataMember]
		[DisplayName("地税登记号")]
		public virtual String LocalTaxNo
		{
			get
			{
				return this.m_LocalTaxNo;
			}
			set
			{
				this.m_LocalTaxNo = value;
			}
		}

		/// <summary>
		/// 国税登记号
		/// </summary>
		[DataMember]
		[DisplayName("国税登记号")]
		public virtual String NationalTaxNo
		{
			get
			{
				return this.m_NationalTaxNo;
			}
			set
			{
				this.m_NationalTaxNo = value;
			}
		}

		/// <summary>
		/// 法人名称
		/// </summary>
		[DataMember]
		[DisplayName("法人名称")]
		public virtual String LegalMan
		{
			get
			{
				return this.m_LegalMan;
			}
			set
			{
				this.m_LegalMan = value;
			}
		}

		/// <summary>
		/// 法人电话
		/// </summary>
		[DataMember]
		[DisplayName("法人电话")]
		public virtual String LegalPhone
		{
			get
			{
				return this.m_LegalPhone;
			}
			set
			{
				this.m_LegalPhone = value;
			}
		}

		/// <summary>
		/// 客户来源
		/// </summary>
		[DataMember]
		[DisplayName("客户来源")]
		public virtual String CustomerSource
		{
			get
			{
				return this.m_CustomerSource;
			}
			set
			{
				this.m_CustomerSource = value;
			}
		}

		/// <summary>
		/// 单位网站
		/// </summary>
		[DataMember]
		[DisplayName("单位网站")]
		public virtual String WebSiteUrl
		{
			get
			{
				return this.m_WebSiteUrl;
			}
			set
			{
				this.m_WebSiteUrl = value;
			}
		}

		/// <summary>
		/// 客户类别
		/// 1-潜在客户,
		/// 2-正式客户,
		/// 3-合作伙伴,
		/// 4-流动商
		/// </summary>
		[DataMember]
		[DisplayName("客户类别")]
		public virtual Int16 CustomerType
		{
			get
			{
				return this.m_CustomerType;
			}
			set
			{
				this.m_CustomerType = value;
			}
		}

		/// <summary>
		/// 客户级别
		/// 1-普通客户,
		/// 2-VIP客户,
		/// 3-高级VIP会员
		/// </summary>
		[DataMember]
		[DisplayName("客户级别")]
		public virtual Int16 CustomerLevel
		{
			get
			{
				return this.m_CustomerLevel;
			}
			set
			{
				this.m_CustomerLevel = value;
			}
		}

		/// <summary>
		/// 信用等级
		/// 1-优秀,
		/// 2-良好,
		/// 3-一般,
		/// 4-差
		/// </summary>
		[DataMember]
		[DisplayName("信用等级")]
		public virtual Int16 CreditStatus
		{
			get
			{
				return this.m_CreditStatus;
			}
			set
			{
				this.m_CreditStatus = value;
			}
		}

		/// <summary>
		/// 重要级别
		/// 1-重要紧急,
		/// 2-重要不紧急,
		/// 3-不重要紧急,
		/// 4-不重要不紧急
		/// </summary>
		[DataMember]
		[DisplayName("重要级别")]
		public virtual Int16 ImportanceLevel
		{
			get
			{
				return this.m_ImportanceLevel;
			}
			set
			{
				this.m_ImportanceLevel = value;
			}
		}

		/// <summary>
		/// 是否公开
		/// 1-是,
		/// 2-否
		/// </summary>
		[DataMember]
		[DisplayName("是否公开")]
		public virtual Int16 IsPublic
		{
			get
			{
				return this.m_IsPublic;
			}
			set
			{
				this.m_IsPublic = value;
			}
		}

		/// <summary>
		/// 客户满意度级别
		/// 1-非常重要,
		/// 2-较为重要,
		/// 3-一般,
		/// 4-不重要
		/// </summary>
		[DataMember]
		[DisplayName("客户满意度级别")]
		public virtual Int16 SatisfactionLevel
		{
			get
			{
				return this.m_SatisfactionLevel;
			}
			set
			{
				this.m_SatisfactionLevel = value;
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

		/// <summary>
		/// 交易次数
		/// </summary>
		[DataMember]
		[DisplayName("交易次数")]
		public virtual Int32 TransactionCount
		{
			get
			{
				return this.m_TransactionCount;
			}
			set
			{
				this.m_TransactionCount = value;
			}
		}

		/// <summary>
		/// 交易金额
		/// </summary>
		[DataMember]
		[DisplayName("交易金额")]
		public virtual Double TransactionTotal
		{
			get
			{
				return this.m_TransactionTotal;
			}
			set
			{
				this.m_TransactionTotal = value;
			}
		}

		/// <summary>
		/// 首次交易时间
		/// </summary>
		[DataMember]
		[DisplayName("首次交易时间")]
		public virtual DateTime TransactionFirstDate
		{
			get
			{
				return this.m_TransactionFirstDate;
			}
			set
			{
				this.m_TransactionFirstDate = value;
			}
		}

		/// <summary>
		/// 最近交易时间
		/// </summary>
		[DataMember]
		[DisplayName("最近交易时间")]
		public virtual DateTime TransactionLastDate
		{
			get
			{
				return this.m_TransactionLastDate;
			}
			set
			{
				this.m_TransactionLastDate = value;
			}
		}

		/// <summary>
		/// 最近联系日期
		/// </summary>
		[DataMember]
		[DisplayName("最近联系日期")]
		public virtual DateTime LastContactDate
		{
			get
			{
				return this.m_LastContactDate;
			}
			set
			{
				this.m_LastContactDate = value;
			}
		}

		/// <summary>
		/// 客户阶段
		/// </summary>
		[DataMember]
		[DisplayName("客户阶段")]
		public virtual String CustomerStage
		{
			get
			{
				return this.m_CustomerStage;
			}
			set
			{
				this.m_CustomerStage = value;
			}
		}

		/// <summary>
		/// 审核状态
		/// 1-未审核,
		/// 2-已审核,
		/// 3-审核中
		/// </summary>
		[DataMember]
		[DisplayName("审核状态")]
		public virtual Int16 AuditStatus
		{
			get
			{
				return this.m_AuditStatus;
			}
			set
			{
				this.m_AuditStatus = value;
			}
		}

		/// <summary>
		/// 创建人编号
		/// </summary>
		[DataMember]
		[DisplayName("创建人编号")]
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
		/// 是否删除
		/// 1-是,
		/// 2-否
		/// </summary>
		[DataMember]
		[DisplayName("是否删除")]
		public virtual Int16 IsDelete
		{
			get
			{
				return this.m_IsDelete;
			}
			set
			{
				this.m_IsDelete = value;
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
		/// 公司Id
		/// </summary>
		[DataMember]
		[DisplayName("公司Id")]
		public virtual Int32 CompanyId
		{
			get
			{
				return this.m_CompanyId;
			}
			set
			{
				this.m_CompanyId = value;
			}
		}
		#endregion
	}
}