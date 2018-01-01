using System;
using System.Runtime.Serialization;

namespace JCodes.Framework.Entity
{
    /// <summary>
    /// 用户信息
    /// </summary>
    [Serializable]
    [DataContract]
    public class UserInfo : SimpleUserInfo
    {                      
        public const int IdentityLen = 50;

        #region Fields
        private int m_PID = -1; //父ID 
        private string m_Nickname; //用户呢称          
        private bool m_IsExpire = false; //是否过期          
        private string m_Title; //职务头衔          
        private string m_IdentityCard; //身份证号码          
        private string m_OfficePhone; //办公电话          
        private string m_HomePhone; //家庭电话          
        private string m_Address; //住址          
        private string m_WorkAddr; //办公地址          
        private string m_Gender; //性别          
        private DateTime m_Birthday; //出生日期          
        private string m_Qq; //QQ号码          
        private string m_Signature; //个性签名          
        private string m_AuditStatus; //审核状态          
        private byte[] m_Portrait; //个人图片          
        private string m_Note; //备注          
        private string m_CustomField; //自定义字段          
        private string m_Dept_ID; //默认部门ID          
        private string m_DeptName; //默认部门名称          
        private string m_Company_ID; //所属公司ID          
        private string m_CompanyName; //所属公司名称          
        private string m_Seq; //排序          
        private string m_Creator; //创建人          
        private string m_Creator_ID; //创建人ID          
        private DateTime m_CreateTime = System.DateTime.Now; //创建时间          
        private string m_Editor; //编辑人          
        private string m_Editor_ID; //编辑人ID          
        private DateTime m_EditTime = System.DateTime.Now; //编辑时间          
        private bool m_Deleted = false; //是否已删除    
        private string m_CurrentLoginIP; //当前登录IP          
        private DateTime m_CurrentLoginTime; //当前登录时间          
        private string m_CurrentMacAddress; //当前Mac地址    
        #endregion    

        #region Property Members

        /// <summary>
        /// 父ID
        /// </summary>
        [DataMember]
        public virtual int PID
        {
            get
            {
                return this.m_PID;
            }
            set
            {
                this.m_PID = value;
            }
        }

        /// <summary>
        /// 用户呢称
        /// </summary>
        [DataMember]
        public virtual string Nickname
        {
            get
            {
                return this.m_Nickname;
            }
            set
            {
                this.m_Nickname = value;
            }
        }

        /// <summary>
        /// 是否过期
        /// </summary>
        [DataMember]
        public virtual bool IsExpire
        {
            get
            {
                return this.m_IsExpire;
            }
            set
            {
                this.m_IsExpire = value;
            }
        }

        /// <summary>
        /// 职务头衔
        /// </summary>
        [DataMember]
        public virtual string Title
        {
            get
            {
                return this.m_Title;
            }
            set
            {
                this.m_Title = value;
            }
        }

        /// <summary>
        /// 身份证号码
        /// </summary>
        [DataMember]
        public virtual string IdentityCard
        {
            get
            {
                return this.m_IdentityCard;
            }
            set
            {
                this.m_IdentityCard = value;
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
        /// 住址
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
        /// 办公地址
        /// </summary>
        [DataMember]
        public virtual string WorkAddr
        {
            get
            {
                return this.m_WorkAddr;
            }
            set
            {
                this.m_WorkAddr = value;
            }
        }

        /// <summary>
        /// 性别
        /// </summary>
        [DataMember]
        public virtual string Gender
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
        /// QQ号码
        /// </summary>
        [DataMember]
        public virtual string QQ
        {
            get
            {
                return this.m_Qq;
            }
            set
            {
                this.m_Qq = value;
            }
        }

        /// <summary>
        /// 个性签名
        /// </summary>
        [DataMember]
        public virtual string Signature
        {
            get
            {
                return this.m_Signature;
            }
            set
            {
                this.m_Signature = value;
            }
        }

        /// <summary>
        /// 审核状态
        /// </summary>
        [DataMember]
        public virtual string AuditStatus
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
        /// 个人图片
        /// </summary>
        [DataMember]
        public virtual byte[] Portrait
        {
            get
            {
                return this.m_Portrait;
            }
            set
            {
                this.m_Portrait = value;
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
        /// 自定义字段
        /// </summary>
        [DataMember]
        public virtual string CustomField
        {
            get
            {
                return this.m_CustomField;
            }
            set
            {
                this.m_CustomField = value;
            }
        }

        /// <summary>
        /// 默认部门ID
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
        /// 默认部门名称
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
        /// 所属公司ID
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

        /// <summary>
        /// 所属公司名称
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
        /// 排序
        /// </summary>
        [DataMember]
        public virtual string Seq
        {
            get
            {
                return this.m_Seq;
            }
            set
            {
                this.m_Seq = value;
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
        /// 创建人ID
        /// </summary>
        [DataMember]
        public virtual string Creator_ID
        {
            get
            {
                return this.m_Creator_ID;
            }
            set
            {
                this.m_Creator_ID = value;
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
        /// 编辑人ID
        /// </summary>
        [DataMember]
        public virtual string Editor_ID
        {
            get
            {
                return this.m_Editor_ID;
            }
            set
            {
                this.m_Editor_ID = value;
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
        /// 当前登录IP
        /// </summary>
        [DataMember]
        public virtual string CurrentLoginIP
        {
            get
            {
                return this.m_CurrentLoginIP;
            }
            set
            {
                this.m_CurrentLoginIP = value;
            }
        }

        /// <summary>
        /// 当前登录时间
        /// </summary>
        [DataMember]
        public virtual DateTime CurrentLoginTime
        {
            get
            {
                return this.m_CurrentLoginTime;
            }
            set
            {
                this.m_CurrentLoginTime = value;
            }
        }

        /// <summary>
        /// 当前Mac地址
        /// </summary>
        [DataMember]
        public virtual string CurrentMacAddress
        {
            get
            {
                return this.m_CurrentMacAddress;
            }
            set
            {
                this.m_CurrentMacAddress = value;
            }
        }

        #endregion

    }

    /// <summary>
    /// 个人图片分类
    /// </summary>
    [Serializable]
    public enum UserImageType
    {
        个人肖像, 身份证照片1, 身份证照片2, 名片1, 名片2
    }
}