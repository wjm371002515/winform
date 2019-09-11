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
        private int m_Pid = -1; //父ID 
        private string m_LoginName; //用户呢称          
        private Int32 m_IsExpire; //是否过期   
        private string m_Remark; //备注    
        private string m_IdCard; //身份证号码          
        private string m_OfficePhone; //办公电话          
        private string m_HomePhone; //家庭电话          
        private string m_Address; //住址          
        private string m_WorkAddress; //办公地址          
        private Int32 m_Gender; //性别          
        private DateTime m_Birthday; //出生日期          
        private Int32 m_Qq; //QQ号码          
        private string m_Signature; //个性签名          
        private Int32 m_AuditStatus; //审核状态          
        private string m_Portrait; //个人图片                
        private Int32 m_DeptId; //默认部门ID        
        private Int32 m_CompanyId; //所属公司ID     
        private string m_Seq; //排序                  
        private Int32 m_CreatorId; //创建人ID          
        private DateTime m_CreatorTime = System.DateTime.Now; //创建时间                
        private Int32 m_EditorId; //编辑人ID          
        private DateTime m_LastUpdateTime = System.DateTime.Now; //编辑时间          
        private Int32 m_IsDelete; //是否已删除   
        private string m_Question1;
        private string m_Question2;
        private string m_Question3;
        private string m_Answer1;
        private string m_Answer2;
        private string m_Answer3;
        private string m_LastLoginIp; //当前登录IP       
        private string m_LastLoginMac; //
        private DateTime m_LastLoginTime; //当前登录时间          
        private DateTime m_LastChangePwdTime;// 最后修改密码时间 
        #endregion    

        #region Property Members

        /// <summary>
        /// 父ID
        /// </summary>
        [DataMember]
        public virtual int Pid
        {
            get
            {
                return this.m_Pid;
            }
            set
            {
                this.m_Pid = value;
            }
        }

        /// <summary>
        /// 用户呢称
        /// </summary>
        [DataMember]
        public virtual string LoginName
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
        /// 是否过期
        /// </summary>
        [DataMember]
        public virtual Int32 IsExpire
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
        /// 身份证号码
        /// </summary>
        [DataMember]
        public virtual string IdCard
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
        public virtual string WorkAddress
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
        /// 性别
        /// </summary>
        [DataMember]
        public virtual Int32 Gender
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
        public virtual Int32 QQ
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
        /// 个人图片
        /// </summary>
        [DataMember]
        public virtual string Portrait
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
        /// 默认部门ID
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
        /// 所属公司ID
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
        /// 问题1
        /// </summary>
        [DataMember]
        public virtual string Question1
        {
            get
            {
                return this.m_Question1;
            }
            set
            {
                this.m_Question1 = value;
            }
        }

        /// <summary>
        /// 问题2
        /// </summary>
        [DataMember]
        public virtual string Question2
        {
            get
            {
                return this.m_Question2;
            }
            set
            {
                this.m_Question2 = value;
            }
        }

        /// <summary>
        /// 问题3
        /// </summary>
        [DataMember]
        public virtual string Question3
        {
            get
            {
                return this.m_Question3;
            }
            set
            {
                this.m_Question3 = value;
            }
        }

        /// <summary>
        /// 回答1
        /// </summary>
        [DataMember]
        public virtual string Answer1
        {
            get
            {
                return this.m_Answer1;
            }
            set
            {
                this.m_Answer1 = value;
            }
        }

        /// <summary>
        /// 回答2
        /// </summary>
        [DataMember]
        public virtual string Answer2
        {
            get
            {
                return this.m_Answer2;
            }
            set
            {
                this.m_Answer2 = value;
            }
        }

        /// <summary>
        /// 回答3
        /// </summary>
        [DataMember]
        public virtual string Answer3
        {
            get
            {
                return this.m_Answer3;
            }
            set
            {
                this.m_Answer3 = value;
            }
        }

        /// <summary>
        /// 最后登录IP
        /// </summary>
        [DataMember]
        public virtual string LastLoginIp
        {
            get
            {
                return this.m_LastLoginIp;
            }
            set
            {
                this.m_LastLoginIp = value;
            }
        }

        /// <summary>
        /// 最后登录Mac
        /// </summary>
        [DataMember]
        public virtual string LastLoginMac
        {
            get
            {
                return this.m_LastLoginMac;
            }
            set
            {
                this.m_LastLoginMac = value;
            }
        }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        [DataMember]
        public virtual DateTime LastLoginTime
        {
            get
            {
                return this.m_LastLoginTime;
            }
            set
            {
                this.m_LastLoginTime = value;
            }
        }

        /// <summary>
        /// 最后修改密码时间
        /// </summary>
        [DataMember]
        public virtual DateTime LastChangePwdTime
        {
            get
            {
                return this.m_LastChangePwdTime;
            }
            set
            {
                this.m_LastChangePwdTime = value;
            }
        }

        #endregion

    }
}