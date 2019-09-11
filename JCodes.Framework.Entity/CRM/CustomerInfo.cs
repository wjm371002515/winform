using System;
using System.Runtime.Serialization;

namespace JCodes.Framework.Entity
{
    /// <summary>
    /// 客户基本资料
    /// </summary>
    [DataContract]
    public class CustomerInfo : BaseEntity
    {    
        #region Field Members

        private Int32 m_Id; //          
        private string m_UserCode; //客户编号          
        private string m_Name; //客户名称          
        private string m_FullName; //客户简称          
        private string m_ProvinceName; //所在省份          
        private string m_CityName; //城市          
        private string m_DistrictName; //所在行政区          
        private string m_Area; //市场分区          
        private string m_Address; //公司地址          
        private string m_ZipCode; //公司邮编          
        private string m_MobilePhone; //办公电话          
        private string m_Fax; //传真号码          
        private string m_Contact; //主联系人          
        private string m_ContactPhone; //联系人电话          
        private string m_ContactMobile; //联系人手机          
        private string m_Email; //电子邮件          
        private string m_QQ; //QQ号码          
        private string m_Industry; //所属行业          
        private string m_BusinessScope; //经营范围          
        private string m_Brand; //经营品牌          
        private string m_PrimaryClient; //主要客户群          
        private string m_PrimaryBusiness; //主营业务          
        private decimal m_RegisterCapital = 0; //注册资金          
        private decimal m_TurnOver = 0; //营业额          
        private string m_LicenseNo; //营业执照          
        private string m_Bank; //开户银行          
        private string m_BankAccount; //银行账号          
        private string m_LocalTaxNo; //地税登记号          
        private string m_NationalTaxNo; //国税登记号          
        private string m_LegalMan; //法人名称          
        private string m_LegalTelephone; //法人电话          
        private string m_LegalMobile; //法人手机          
        private string m_Source; //客户来源          
        private string m_WebSite; //单位网站          
        private string m_CompanyPictureGUID = System.Guid.NewGuid().ToString(); //公司图片信息          
        private string m_AttachGUID = System.Guid.NewGuid().ToString(); //附件组别ID          
        private string m_CustomerType; //客户类别          
        private string m_Grade; //客户级别          
        private string m_CreditStatus; //信用等级          
        private string m_Importance; //重要级别          
        private bool m_IsPublic = false; //公开与否          
        private int m_Satisfaction = 0; //客户满意度          
        private string m_Remark; //备注信息               
        private int m_TransactionCount = 0; //交易次数          
        private decimal m_TransactionTotal = 0; //交易金额          
        private DateTime m_TransactionFirstDay; //首次交易时间          
        private DateTime m_TransactionLastDay; //最近交易时间          
        private DateTime m_LastContactDate; //最近联系日期          
        private string m_Stage; //客户阶段          
        private Int32 m_AuditStatus; //客户状态          
        private Int32 m_CreatorId; //创建人/所属人员     
        private DateTime m_CreatorTime; //创建时间          
        private Int32 m_EditorId; //编辑人          
        private DateTime m_LastUpdateTime; //编辑时间          
        private Int32 m_IsDelete; //是否已删除          
        private Int32 m_DeptId; //所属部门
        private Int32 m_CompanyId; //所属公司
        #endregion

        #region Property Members
        
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
        /// 客户编号
        /// </summary>
		[DataMember]
        public virtual string UserCode
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
        /// 客户名称
        /// </summary>
		[DataMember]
        public virtual string Name
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
        /// 客户简称
        /// </summary>
		[DataMember]
        public virtual string FullName
        {
            get
            {
                return this.m_FullName;
            }
            set
            {
                this.m_FullName = value;
            }
        }

        /// <summary>
        /// 所在省份
        /// </summary>
		[DataMember]
        public virtual string ProvinceName
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
        /// 城市
        /// </summary>
		[DataMember]
        public virtual string CityName
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
        /// 所在行政区
        /// </summary>
		[DataMember]
        public virtual string DistrictName
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
        /// 市场分区
        /// </summary>
		[DataMember]
        public virtual string Area
        {
            get
            {
                return this.m_Area;
            }
            set
            {
                this.m_Area = value;
            }
        }

        /// <summary>
        /// 公司地址
        /// </summary>
		[DataMember]
        public virtual string Address
        {
            get
            {
                return this.m_Address;
            }
            set
            {
                this.m_Address = value;
            }
        }

        /// <summary>
        /// 公司邮编
        /// </summary>
		[DataMember]
        public virtual string ZipCode
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
        /// 办公电话
        /// </summary>
		[DataMember]
        public virtual string MobilePhone
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
        public virtual string Fax
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
        /// 主联系人
        /// </summary>
		[DataMember]
        public virtual string Contact
        {
            get
            {
                return this.m_Contact;
            }
            set
            {
                this.m_Contact = value;
            }
        }

        /// <summary>
        /// 联系人电话
        /// </summary>
		[DataMember]
        public virtual string ContactPhone
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
        /// 联系人手机
        /// </summary>
		[DataMember]
        public virtual string ContactMobile
        {
            get
            {
                return this.m_ContactMobile;
            }
            set
            {
                this.m_ContactMobile = value;
            }
        }

        /// <summary>
        /// 电子邮件
        /// </summary>
		[DataMember]
        public virtual string Email
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
        /// QQ号码
        /// </summary>
		[DataMember]
        public virtual string QQ
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
        public virtual string Industry
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
        public virtual string BusinessScope
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
        public virtual string Brand
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
        public virtual string PrimaryClient
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
        public virtual string PrimaryBusiness
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
        public virtual decimal RegisterCapital
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
        public virtual decimal TurnOver
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
        public virtual string LicenseNo
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
        public virtual string Bank
        {
            get
            {
                return this.m_Bank;
            }
            set
            {
                this.m_Bank = value;
            }
        }

        /// <summary>
        /// 银行账号
        /// </summary>
		[DataMember]
        public virtual string BankAccount
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
        public virtual string LocalTaxNo
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
        public virtual string NationalTaxNo
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
        public virtual string LegalMan
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
        public virtual string LegalTelephone
        {
            get
            {
                return this.m_LegalTelephone;
            }
            set
            {
                this.m_LegalTelephone = value;
            }
        }

        /// <summary>
        /// 法人手机
        /// </summary>
		[DataMember]
        public virtual string LegalMobile
        {
            get
            {
                return this.m_LegalMobile;
            }
            set
            {
                this.m_LegalMobile = value;
            }
        }

        /// <summary>
        /// 客户来源
        /// </summary>
		[DataMember]
        public virtual string Source
        {
            get
            {
                return this.m_Source;
            }
            set
            {
                this.m_Source = value;
            }
        }

        /// <summary>
        /// 单位网站
        /// </summary>
		[DataMember]
        public virtual string WebSite
        {
            get
            {
                return this.m_WebSite;
            }
            set
            {
                this.m_WebSite = value;
            }
        }

        /// <summary>
        /// 公司图片信息
        /// </summary>
		[DataMember]
        public virtual string CompanyPictureGUID
        {
            get
            {
                return this.m_CompanyPictureGUID;
            }
            set
            {
                this.m_CompanyPictureGUID = value;
            }
        }

        /// <summary>
        /// 附件组别ID
        /// </summary>
		[DataMember]
        public virtual string AttachGUID
        {
            get
            {
                return this.m_AttachGUID;
            }
            set
            {
                this.m_AttachGUID = value;
            }
        }

        /// <summary>
        /// 客户类别
        /// </summary>
		[DataMember]
        public virtual string CustomerType
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
        /// </summary>
		[DataMember]
        public virtual string Grade
        {
            get
            {
                return this.m_Grade;
            }
            set
            {
                this.m_Grade = value;
            }
        }

        /// <summary>
        /// 信用等级
        /// </summary>
		[DataMember]
        public virtual string CreditStatus
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
        /// </summary>
		[DataMember]
        public virtual string Importance
        {
            get
            {
                return this.m_Importance;
            }
            set
            {
                this.m_Importance = value;
            }
        }

        /// <summary>
        /// 公开与否
        /// </summary>
		[DataMember]
        public virtual bool IsPublic
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
        /// 客户满意度
        /// </summary>
		[DataMember]
        public virtual int Satisfaction
        {
            get
            {
                return this.m_Satisfaction;
            }
            set
            {
                this.m_Satisfaction = value;
            }
        }

        /// <summary>
        /// 备注信息
        /// </summary>
		[DataMember]
        public virtual string Remark
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
        public virtual int TransactionCount
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
        public virtual decimal TransactionTotal
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
        public virtual DateTime TransactionFirstDay
        {
            get
            {
                return this.m_TransactionFirstDay;
            }
            set
            {
                this.m_TransactionFirstDay = value;
            }
        }

        /// <summary>
        /// 最近交易时间
        /// </summary>
		[DataMember]
        public virtual DateTime TransactionLastDay
        {
            get
            {
                return this.m_TransactionLastDay;
            }
            set
            {
                this.m_TransactionLastDay = value;
            }
        }

        /// <summary>
        /// 最近联系日期
        /// </summary>
		[DataMember]
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
        public virtual string Stage
        {
            get
            {
                return this.m_Stage;
            }
            set
            {
                this.m_Stage = value;
            }
        }

        /// <summary>
        /// 客户状态
        /// </summary>
		[DataMember]
        public virtual Int32 AuditStatus
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
        /// 创建人/所属人员
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
        /// 编辑人
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
        /// 编辑时间
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
        /// 是否已删除
        /// </summary>
		[DataMember]
        public virtual Int32 IsDelete
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
        /// 所属部门
        /// </summary>
        [DataMember]
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
        /// 所属公司
        /// </summary>
        [DataMember]
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