using System;
using System.Runtime.Serialization;

namespace JCodes.Framework.Entity
{
    /// <summary>
    /// 系统标识信息
    /// </summary>
    [Serializable]
    [DataContract]
    public class SystemTypeInfo : BaseEntity
    {
        private string m_OID = System.Guid.NewGuid().ToString(); //系统标识          
        private string m_Name = "";//系统名称 
        private string m_CustomID = "";//客户编码 
        private string m_Authorize = ""; //授权编码 
        private string m_Note = "";//备注

        #region Property Members

        /// <summary>
        /// 系统标识
        /// </summary>
        [DataMember]
        public virtual string OID
        {
            get
            {
                return this.m_OID;
            }
            set
            {
                this.m_OID = value;
            }
        }

        /// <summary>
        /// 系统名称
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
        /// 客户编码
        /// </summary>
        [DataMember]
        public virtual string CustomID
        {
            get
            {
                return this.m_CustomID;
            }
            set
            {
                this.m_CustomID = value;
            }
        }

        /// <summary>
        /// 授权编码
        /// </summary>
        [DataMember]
        public virtual string Authorize
        {
            get
            {
                return this.m_Authorize;
            }
            set
            {
                this.m_Authorize = value;
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


        #endregion

    }
}