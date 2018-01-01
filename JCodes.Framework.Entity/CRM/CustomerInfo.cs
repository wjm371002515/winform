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

        private string m_ID = System.Guid.NewGuid().ToString(); //          
        private string m_HandNo; //客户编号          
        private string m_Name; //客户名称          
        private string m_SimpleName; //客户简称          
        private string m_Province; //所在省份          
        private string m_City; //城市          
        private string m_District; //所在行政区          
        private string m_Area; //市场分区          
        private string m_Address; //公司地址          
        private string m_ZipCode; //公司邮编          
        private string m_Telephone; //办公电话          
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
        private string m_Note; //备注信息               
        private int m_TransactionCount = 0; //交易次数          
        private decimal m_TransactionTotal = 0; //交易金额          
        private DateTime m_TransactionFirstDay; //首次交易时间          
        private DateTime m_TransactionLastDay; //最近交易时间          
        private DateTime m_LastContactDate; //最近联系日期          
        private string m_Stage; //客户阶段          
        private string m_Status; //客户状态          
        private string m_Creator; //创建人/所属人员     
        private DateTime m_CreateTime; //创建时间          
        private string m_Editor; //编辑人          
        private DateTime m_EditTime; //编辑时间          
        private bool m_Deleted = false; //是否已删除          
        private string m_Dept_ID; //所属部门
        private string m_Company_ID; //所属公司
        #endregion

        #region Property Members
        
		[DataMember]
        public virtual string ID
        {
            get
            {
                return this.m_ID;
            }
            set
            {
                this.m_ID = value;
            }
        }

        /// <summary>
        /// 客户编号
        /// </summary>
		[DataMember]
        public virtual string HandNo
        {
            get
            {
                return this.m_HandNo;
            }
            set
            {
                this.m_HandNo = value;
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
        public virtual string SimpleName
        {
            get
            {
                return this.m_SimpleName;
            }
            set
            {
                this.m_SimpleName = value;
            }
        }

        /// <summary>
        /// 所在省份
        /// </summary>
		[DataMember]
        public virtual string Province
        {
            get
            {
                return this.m_Province;
            }
            set
            {
                this.m_Province = value;
            }
        }

        /// <summary>
        /// 城市
        /// </summary>
		[DataMember]
        public virtual string City
        {
            get
            {
                return this.m_City;
            }
            set
            {
                this.m_City = value;
            }
        }

        /// <summary>
        /// 所在行政区
        /// </summary>
		[DataMember]
        public virtual string District
        {
            get
            {
                return this.m_District;
            }
            set
            {
                this.m_District = value;
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
        public virtual string Telephone
        {
            get
            {
                return this.m_Telephone;
            }
            set
            {
                this.m_Telephone = value;
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
        public virtual string Note
        {
            get
            {
                return this.m_Note;
            }
            set
            {
                this.m_Note = value;
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
        public virtual string Status
        {
            get
            {
                return this.m_Status;
            }
            set
            {
                this.m_Status = value;
            }
        }

        /// <summary>
        /// 创建人/所属人员
        /// </summary>
		[DataMember]
        public virtual string Creator
        {
            get
            {
                return this.m_Creator;
            }
            set
            {
                this.m_Creator = value;
            }
        }

        /// <summary>
        /// 创建时间
        /// </summary>
		[DataMember]
        public virtual DateTime CreateTime
        {
            get
            {
                return this.m_CreateTime;
            }
            set
            {
                this.m_CreateTime = value;
            }
        }

        /// <summary>
        /// 编辑人
        /// </summary>
		[DataMember]
        public virtual string Editor
        {
            get
            {
                return this.m_Editor;
            }
            set
            {
                this.m_Editor = value;
            }
        }

        /// <summary>
        /// 编辑时间
        /// </summary>
		[DataMember]
        public virtual DateTime EditTime
        {
            get
            {
                return this.m_EditTime;
            }
            set
            {
                this.m_EditTime = value;
            }
        }

        /// <summary>
        /// 是否已删除
        /// </summary>
		[DataMember]
        public virtual bool Deleted
        {
            get
            {
                return this.m_Deleted;
            }
            set
            {
                this.m_Deleted = value;
            }
        }

        /// <summary>
        /// 所属部门
        /// </summary>
        [DataMember]
        public virtual string Dept_ID
        {
            get
            {
                return this.m_Dept_ID;
            }
            set
            {
                this.m_Dept_ID = value;
            }
        }

        /// <summary>
        /// 所属公司
        /// </summary>
        [DataMember]
        public virtual string Company_ID
        {
            get
            {
                return this.m_Company_ID;
            }
            set
            {
                this.m_Company_ID = value;
            }
        }

        #endregion

    }
}