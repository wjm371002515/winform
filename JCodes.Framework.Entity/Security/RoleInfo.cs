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
        private int m_Id = 0; //          
        private int m_Pid = -1; //父ID          
        private string m_RoleCode; //角色编码          
        private string m_Name; //角色名称          
        private string m_Remark; //备注          
        private string m_Seq; //排序码          
        private Int32 m_RoleType; //角色分类          
        private Int32 m_CompanyId; //所属公司ID          
        private string m_CompanyName; //所属公司名称                   
        private Int32 m_CreatorId; //创建人ID          
        private DateTime m_CreatorTime = System.DateTime.Now; //创建时间                
        private Int32 m_EditorId; //编辑人ID          
        private DateTime m_LastUpdateTime = System.DateTime.Now; //编辑时间          
        private Int32 m_IsDelete; //是否已删除          
        private Int32 m_IsForbid; //有效标志          

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
        /// 角色编码
        /// </summary>
		[DataMember]
        public virtual string RoleCode
        {
            get
            {
                return this.m_RoleCode;
            }
            set
            {
                this.m_RoleCode = value;
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
        /// 角色分类
        /// </summary>
		[DataMember]
        public virtual Int32 RoleType
        {
            get
            {
                return this.m_RoleType;
            }
            set
            {
                this.m_RoleType = value;
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
        /// 创建人
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
        /// 有效标志
        /// </summary>
		[DataMember]
        public virtual Int32 IsForbid
        {
            get
            {
                return this.m_IsForbid;
            }
            set
            {
                this.m_IsForbid = value;
            }
        }


        #endregion
    }
}