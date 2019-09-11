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

        private int m_Id = 0; //          
        private Int32 m_UserId; //登录用户ID          
        private string m_LoginName; //登录名          
        private string m_FullName; //真实名称          
        private Int32 m_CompanyId; //所属公司ID          
        private string m_CompanyName; //所属公司名称          
        private string m_Remark; //日志描述          
        private string m_IP; //IP地址          
        private string m_Mac; //Mac地址          
        private DateTime m_LastUpdateTime; //更新时间          
        private string m_SystemtypeId; //系统编号          

        #endregion

        #region Property Members
        
		[DataMember]
        public virtual int Id
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
        /// 登录用户ID
        /// </summary>
		[DataMember]
        public virtual Int32 UserId
        {
            get
            {
                return this.m_UserId;
            }
            set
            {
                this.m_UserId = value;
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
        /// IP地址
        /// </summary>
		[DataMember]
        public virtual string IP
        {
            get
            {
                return this.m_IP;
            }
            set
            {
                this.m_IP = value;
            }
        }

        /// <summary>
        /// Mac地址
        /// </summary>
		[DataMember]
        public virtual string Mac
        {
            get
            {
                return this.m_Mac;
            }
            set
            {
                this.m_Mac = value;
            }
        }

        /// <summary>
        /// 更新时间
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
        /// 系统编号
        /// </summary>
        [DataMember]
        public virtual string SystemtypeId
        {
            get
            {
                return this.m_SystemtypeId;
            }
            set
            {
                this.m_SystemtypeId = value;
            }
        }

        #endregion

    }
}