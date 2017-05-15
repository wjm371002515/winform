using System;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace JCodes.Framework.Entity
{
    /// <summary>
    /// 登陆系统的黑白名单列表
    /// </summary>
    [DataContract]
    public class BlackIPInfo : BaseEntity
    {    
        #region Field Members

        private string m_ID = System.Guid.NewGuid().ToString(); //          
        private string m_Name; //显示名称                 
        private int m_AuthorizeType = 0; //授权类型          
        private bool m_Forbid = false; //是否禁用          
        private string m_IPStart; //IP起始地址          
        private string m_IPEnd; //IP结束地址          
        private string m_Note; //备注          
        private string m_Creator; //创建人          
        private string m_Creator_ID; //创建人ID          
        private DateTime m_CreateTime = System.DateTime.Now; //创建时间          
        private string m_Editor; //编辑人          
        private string m_Editor_ID; //编辑人ID          
        private DateTime m_EditTime = System.DateTime.Now; //编辑时间          

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
        /// 显示名称
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
        /// 授权类型[0为黑名单，1为白名单 ）
        /// </summary>
		[DataMember]
        public virtual int AuthorizeType
        {
            get
            {
                return this.m_AuthorizeType;
            }
            set
            {
                this.m_AuthorizeType = value;
            }
        }

        /// <summary>
        /// 是否禁用
        /// </summary>
		[DataMember]
        public virtual bool Forbid
        {
            get
            {
                return this.m_Forbid;
            }
            set
            {
                this.m_Forbid = value;
            }
        }

        /// <summary>
        /// IP起始地址
        /// </summary>
		[DataMember]
        public virtual string IPStart
        {
            get
            {
                return this.m_IPStart;
            }
            set
            {
                this.m_IPStart = value;
            }
        }

        /// <summary>
        /// IP结束地址
        /// </summary>
		[DataMember]
        public virtual string IPEnd
        {
            get
            {
                return this.m_IPEnd;
            }
            set
            {
                this.m_IPEnd = value;
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


        #endregion

    }
}