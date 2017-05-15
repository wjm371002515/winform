using System;
using System.Runtime.Serialization;

namespace JCodes.Framework.Entity
{
    /// <summary>
    /// 角色信息
    /// </summary>
    [Serializable]
    [DataContract]
    public class RoleInfo : BaseEntity
    {
        /// <summary>
        /// 超级管理员名称
        /// </summary>
        public const string SuperAdminName = "超级管理员";

        /// <summary>
        /// 公司级别的系统管理员
        /// </summary>
        public const string CompanyAdminName = "系统管理员";

        #region Field Members
        private int m_ID = 0; //          
        private int m_PID = -1; //父ID          
        private string m_HandNo; //角色编码          
        private string m_Name; //角色名称          
        private string m_Note; //备注          
        private string m_SortCode; //排序码          
        private string m_Category; //角色分类          
        private string m_Company_ID; //所属公司ID          
        private string m_CompanyName; //所属公司名称          
        private string m_Creator; //创建人          
        private string m_Creator_ID; //创建人ID          
        private DateTime m_CreateTime = System.DateTime.Now; //创建时间          
        private string m_Editor; //编辑人          
        private string m_Editor_ID; //编辑人ID          
        private DateTime m_EditTime = System.DateTime.Now; //编辑时间          
        private bool m_Deleted = false; //是否已删除          
        private bool m_Enabled = true; //有效标志          

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
        /// 角色编码
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
        /// 角色名称
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
        /// 排序码
        /// </summary>
		[DataMember]
        public virtual string SortCode
        {
            get
            {
                return this.m_SortCode;
            }
            set
            {
                this.m_SortCode = value;
            }
        }

        /// <summary>
        /// 角色分类
        /// </summary>
		[DataMember]
        public virtual string Category
        {
            get
            {
                return this.m_Category;
            }
            set
            {
                this.m_Category = value;
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
        /// 有效标志
        /// </summary>
		[DataMember]
        public virtual bool Enabled
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