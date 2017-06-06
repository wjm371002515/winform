using System;
using System.Runtime.Serialization;

namespace JCodes.Framework.Entity
{
    [Serializable]
    [DataContract]
    public class CouponCategoryInfo : BaseEntity
    {
        #region Field Members
        private string m_ID = "";
        private string m_HandNo = "";
        private string m_Name = "";
        private string m_BelongCompanys = "";
        private string m_Creator = "";
        private string m_Creator_ID = "";
        private DateTime m_CreateTime = System.DateTime.Now;
        private string m_Editor = "";
        private string m_Editor_ID = "";
        private DateTime? m_EditTime = null;
        private Int32 m_Enabled = 1;
        #endregion

        #region Property Members
        /// <summary>
        /// 分类ID
        /// </summary>
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
        /// 分类编码
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
        /// 分类名称
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
        /// 允许哪些公司操作
        /// </summary>
        [DataMember]
        public virtual string BelongCompanys
        {
            get
            {
                return this.m_BelongCompanys;
            }
            set
            {
                this.m_BelongCompanys = value;
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
        public virtual DateTime? EditTime
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
        /// 是否可用(0不可用，1可用)
        /// </summary>
        [DataMember]
        public virtual Int32 Enabled
        {
            get
            {
                return this.m_Enabled;
            }
            set
            {
                this.m_Enabled = value;
            }
        }
        #endregion

    }
}
