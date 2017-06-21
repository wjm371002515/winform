using System;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace JCodes.Framework.Entity
{
    /// <summary>
    /// 通讯录联系人
    /// </summary>
    [DataContract]
    public class AddressInfo : BaseEntity
    {    
        #region Field Members

        private string m_ID = System.Guid.NewGuid().ToString(); //          
        private AddressType m_AddressType =  AddressType.个人; //通讯录类型[个人,公司]          
        private string m_Name; //姓名          
        private string m_Sex; //性别          
        private DateTime m_Birthdate; //出生日期          
        private string m_Mobile; //手机          
        private string m_Email; //电子邮箱          
        private string m_QQ; //QQ          
        private string m_HomeTelephone; //家庭电话          
        private string m_OfficeTelephone; //办公电话          
        private string m_HomeAddress; //家庭住址          
        private string m_OfficeAddress; //办公地址          
        private string m_Fax; //传真号码          
        private string m_Company; //公司单位          
        private string m_Dept; //部门          
        private string m_Other; //其他          
        private string m_Note; //备注          
        private string m_Creator; //创建人          
        private DateTime m_CreateTime; //创建时间          
        private string m_Editor; //编辑人          
        private DateTime m_EditTime; //编辑时间          
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
        public virtual string Sex
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
        public virtual DateTime Birthdate
        {
            get
            {
                return this.m_Birthdate;
            }
            set
            {
                this.m_Birthdate = value;
            }
        }

        /// <summary>
        /// 手机
        /// </summary>
		[DataMember]
        public virtual string Mobile
        {
            get
            {
                return this.m_Mobile;
            }
            set
            {
                this.m_Mobile = value;
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
        /// 家庭电话
        /// </summary>
		[DataMember]
        public virtual string HomeTelephone
        {
            get
            {
                return this.m_HomeTelephone;
            }
            set
            {
                this.m_HomeTelephone = value;
            }
        }

        /// <summary>
        /// 办公电话
        /// </summary>
		[DataMember]
        public virtual string OfficeTelephone
        {
            get
            {
                return this.m_OfficeTelephone;
            }
            set
            {
                this.m_OfficeTelephone = value;
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
        public virtual string Company
        {
            get
            {
                return this.m_Company;
            }
            set
            {
                this.m_Company = value;
            }
        }

        /// <summary>
        /// 部门
        /// </summary>
		[DataMember]
        public virtual string Dept
        {
            get
            {
                return this.m_Dept;
            }
            set
            {
                this.m_Dept = value;
            }
        }

        /// <summary>
        /// 职位
        /// </summary>
        [DataMember]
        public virtual string Position { get; set; }

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
        /// 创建人
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

    /// <summary>
    /// 通讯录类型
    /// </summary>
    public enum AddressType { 个人, 公共}
}