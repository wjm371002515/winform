using System;
using System.Runtime.Serialization;

namespace JCodes.Framework.Entity
{
    /// <summary>
    /// 可用于传递的用户简单信息
    /// </summary>
    [Serializable]
    [DataContract]
    public class SimpleUserInfo : BaseEntity
    {
        private int m_ID = 0;
        private string m_HandNo; //用户编码   
        private string m_Name = "";//用户名/登录名 
        private string m_Password = "";//用户密码
        private string m_FullName = "";//用户全名
        private string m_MobilePhone; //移动电话          
        private string m_Email; //邮件地址          

        [DataMember]
        public virtual int ID
        {
            get { return m_ID; }
            set { m_ID = value; }
        }

        /// <summary>
        /// 用户编码
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
        /// 用户名/登录名
        /// </summary>
        [DataMember]
        public virtual string Name
        {
            get { return this.m_Name; }
            set { this.m_Name = value; }
        }

        /// <summary>
        /// 用户密码
        /// </summary>
        [DataMember]
        public virtual string Password
        {
            get { return this.m_Password; }
            set { this.m_Password = value; }
        }

        /// <summary>
        /// 用户全名
        /// </summary>
        [DataMember]
        public virtual string FullName
        {
            get { return this.m_FullName; }
            set { this.m_FullName = value; }
        }

        /// <summary>
        /// 移动电话
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
        /// 邮件地址
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
    }
}