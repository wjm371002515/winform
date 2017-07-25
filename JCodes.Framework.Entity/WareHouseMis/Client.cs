using System;
using System.Xml.Serialization;

namespace JCodes.Framework.Entity
{
    [Serializable]
    public class ClientInfo : BaseEntity
    {    
        #region Field Members

        private int m_ID = 0;
        private string m_Code = ""; // 客户代码
        private string m_Name = ""; // 客户名称            
        private string m_Phone = ""; //客户电话     
        private string m_Address = ""; //客户地址          
        private string m_Note = ""; //备注  

        #endregion

        #region Property Members
        
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
        /// 仓库名称
        /// </summary>
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
        /// 仓库负责人
        /// </summary>
        public virtual string Code
        {
            get
            {
                return this.m_Code;
            }
            set
            {
                this.m_Code = value;
            }
        }

        /// <summary>
        /// 联系电话
        /// </summary>
        public virtual string Phone
        {
            get
            {
                return this.m_Phone;
            }
            set
            {
                this.m_Phone = value;
            }
        }

        /// <summary>
        /// 仓库地址
        /// </summary>
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
        /// 备注
        /// </summary>
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