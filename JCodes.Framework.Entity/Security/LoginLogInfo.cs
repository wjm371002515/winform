using System;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace JCodes.Framework.Entity
{
    /// <summary>
    /// 用户登录日志信息
    /// </summary>
    [Serializable]
    [DataContract]
    public class LoginLogInfo : BaseEntity
    {    
        #region Field Members

        private int m_ID = 0; //          
        private string m_User_ID; //登录用户ID          
        private string m_LoginName; //登录名          
        private string m_FullName; //真实名称          
        private string m_Company_ID; //所属公司ID          
        private string m_CompanyName; //所属公司名称          
        private string m_Note; //日志描述          
        private string m_IPAddress; //IP地址          
        private string m_MacAddress; //Mac地址          
        private DateTime m_LastUpdated; //更新时间          
        private string m_SystemType_ID; //系统编号          

        #endregion

        #region Property Members
        
		[DataMember]
        public virtual int ID
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
        /// 登录用户ID
        /// </summary>
		[DataMember]
        public virtual string User_ID
        {
            get
            {
                return this.m_User_ID;
            }
            set
            {
                this.m_User_ID = value;
            }
        }

        /// <summary>
        /// 登录名
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
        /// 真实名称
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
        /// 日志描述
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
        /// IP地址
        /// </summary>
		[DataMember]
        public virtual string IPAddress
        {
            get
            {
                return this.m_IPAddress;
            }
            set
            {
                this.m_IPAddress = value;
            }
        }

        /// <summary>
        /// Mac地址
        /// </summary>
		[DataMember]
        public virtual string MacAddress
        {
            get
            {
                return this.m_MacAddress;
            }
            set
            {
                this.m_MacAddress = value;
            }
        }

        /// <summary>
        /// 更新时间
        /// </summary>
		[DataMember]
        public virtual DateTime LastUpdated
        {
            get
            {
                return this.m_LastUpdated;
            }
            set
            {
                this.m_LastUpdated = value;
            }
        }

        /// <summary>
        /// 系统编号
        /// </summary>
        [DataMember]
        public virtual string SystemType_ID
        {
            get
            {
                return this.m_SystemType_ID;
            }
            set
            {
                this.m_SystemType_ID = value;
            }
        }

        #endregion

    }
}