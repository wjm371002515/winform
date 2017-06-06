using System;
using System.Runtime.Serialization;

namespace JCodes.Framework.Entity
{
    [Serializable]
    [DataContract]
    public class CouponInfo : BaseEntity
    {
        #region Field Members
        private string m_ID = "";
        private string m_CouponCategory_ID = "";
        private string m_CouponCategory_Name = "";
        private string m_Company_ID = "";
        private string m_Company_Name = "";
        private string m_Creator = "";
        private string m_Creator_ID = "";
        private DateTime m_CreateTime = System.DateTime.Now;
        private string m_Editor = "";
        private string m_Editor_ID = "";
        private DateTime? m_EditTime = null;
        private string m_MobilePhone = "";
        private string m_FullName = "";
        private DateTime m_StartTime = System.DateTime.Now;
        private DateTime m_EndTime = System.DateTime.Now;
        private Int32 m_DELETED = 0;
        #endregion

        #region Property Members
        /// <summary>
        /// 优惠券序列号
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
        /// 优惠券分类
        /// </summary>
        [DataMember]
        public virtual string CouponCategory_ID
        {
            get
            {
                return this.m_CouponCategory_ID;
            }
            set
            {
                this.m_CouponCategory_ID = value;
            }
        }

        /// <summary>
        /// 优惠券分类名字
        /// </summary>
        [DataMember]
        public virtual string CouponCategory_Name
        {
            get
            {
                return this.m_CouponCategory_Name;
            }
            set
            {
                this.m_CouponCategory_Name = value;
            }
        }

        /// <summary>
        /// 公司编号
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
        /// 公司名字
        /// </summary>
        [DataMember]
        public virtual string Company_Name
        {
            get
            {
                return this.m_Company_Name;
            }
            set
            {
                this.m_Company_Name = value;
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
        /// 联系电话
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
        /// 联系人
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
        /// 有效开始日期
        /// </summary>
        [DataMember]
        public virtual DateTime StartTime
        {
            get
            {
                return this.m_StartTime;
            }
            set
            {
                this.m_StartTime = value;
            }
        }

        public virtual DateTime EndTime
        {
            get
            {
                return this.m_EndTime;
            }
            set
            {
                this.m_EndTime = value;
            }
        }
      
        /// <summary>
        /// 是否使用(0为使用，1已使用)
        /// </summary>
        [DataMember]
        public virtual Int32 DELETED
        {
            get
            {
                return this.m_DELETED;
            }
            set
            {
                this.m_DELETED = value;
            }
        }
        #endregion
    }
}
