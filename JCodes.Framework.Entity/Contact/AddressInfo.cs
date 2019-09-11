using System;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using JCodes.Framework.jCodesenum;

namespace JCodes.Framework.Entity
{
    /// <summary>
    /// 通讯录联系人
    /// </summary>
    [DataContract]
    public class AddressInfo : BaseEntity
    {    
        #region Field Members

        private Int32 m_Id; //          
        private AddressType m_AddressType =  AddressType.个人; //通讯录类型[个人,公司]          
        private string m_Name; //姓名          
        private Int32 m_Sex; //性别          
        private DateTime m_Birthday; //出生日期          
        private string m_MobilePhone; //手机          
        private string m_Email; //电子邮箱          
        private Int32 m_QQ; //QQ          
        private string m_HomePhone; //家庭电话          
        private string m_OfficePhone; //办公电话          
        private string m_HomeAddress; //家庭住址          
        private string m_OfficeAddress; //办公地址          
        private string m_Fax; //传真号码          
        private string m_CompanyName; //公司单位          
        private string m_DeptName; //部门    
        private string m_Position;// 职位
        private string m_Other; //其他          
        private string m_Remark; //备注          
        private Int32 m_CreatorId; //创建人          
        private DateTime m_CreatorTime; //创建时间          
        private Int32 m_EditorId; //编辑人          
        private DateTime m_LastUpdateTime; //编辑时间          
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
        /// 通讯录类型[个人,公司]
        /// </summary>
		[DataMember]
        public virtual AddressType AddressType
        {
            get
            {
                return this.m_AddressType;
            }
            set
            {
                this.m_AddressType = value;
            }
        }

        /// <summary>
        /// 姓名
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
        /// 性别
        /// </summary>
		[DataMember]
        public virtual Int32 Sex
        {
            get
            {
                return this.m_Sex;
            }
            set
            {
                this.m_Sex = value;
            }
        }

        /// <summary>
        /// 出生日期
        /// </summary>
		[DataMember]
        public virtual DateTime Birthday
        {
            get
            {
                return this.m_Birthday;
            }
            set
            {
                this.m_Birthday = value;
            }
        }

        /// <summary>
        /// 手机
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
        /// 电子邮箱
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
        /// QQ
        /// </summary>
		[DataMember]
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
        /// 家庭电话
        /// </summary>
		[DataMember]
        public virtual string HomePhone
        {
            get
            {
                return this.m_HomePhone;
            }
            set
            {
                this.m_HomePhone = value;
            }
        }

        /// <summary>
        /// 办公电话
        /// </summary>
		[DataMember]
        public virtual string OfficePhone
        {
            get
            {
                return this.m_OfficePhone;
            }
            set
            {
                this.m_OfficePhone = value;
            }
        }

        /// <summary>
        /// 家庭住址
        /// </summary>
		[DataMember]
        public virtual string HomeAddress
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
        /// 办公地址
        /// </summary>
		[DataMember]
        public virtual string OfficeAddress
        {
            get
            {
                return this.m_OfficeAddress;
            }
            set
            {
                this.m_OfficeAddress = value;
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
        /// 公司单位
        /// </summary>
		[DataMember]
        public virtual string CompanyName
        {
            get
            {
                return this.m_CompanyName;
            }
            set
            {
                this.m_CompanyName = value;
            }
        }

        /// <summary>
        /// 部门
        /// </summary>
		[DataMember]
        public virtual string DeptName
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
        /// 职位
        /// </summary>
        [DataMember]
        public virtual string Position { 
            get { return this.m_Position; }
            set { this.m_Position = value;  } 
        
        }

        /// <summary>
        /// 其他
        /// </summary>
		[DataMember]
        public virtual string Other
        {
            get
            {
                return this.m_Other;
            }
            set
            {
                this.m_Other = value;
            }
        }

        /// <summary>
        /// 备注
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
        /// 创建人
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