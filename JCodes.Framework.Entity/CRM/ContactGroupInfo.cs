using System;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace JCodes.Framework.Entity
{
    /// <summary>
    /// 联系人组别
    /// </summary>
    [DataContract]
    public class ContactGroupInfo : BaseEntity
    {    
        #region Field Members

        private Int32 m_Id; //          
        private Int32 m_Pid; //上级ID          
        private string m_UserCode; //编号          
        private string m_Name; //分组名称          
        private string m_Remark; //备注          
        private Int32 m_CreatorId; //创建人          
        private DateTime m_CreatorTime; //创建时间          
        private Int32 m_EditorId; //编辑人          
        private DateTime m_LastUpdateTime; //编辑时间          
        private Int32 m_DeptId; //所属部门
        private Int32 m_CompanyId; //所属公司
        #endregion

        #region Property Members
        
		[DataMember]
        public virtual Int32 Id
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
        /// 上级ID
        /// </summary>
		[DataMember]
        public virtual Int32 Pid
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
        /// 编号
        /// </summary>
		[DataMember]
        public virtual string UserCode
        {
            get
            {
                return this.m_UserCode;
            }
            set
            {
                this.m_UserCode = value;
            }
        }

        /// <summary>
        /// 分组名称
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
        /// 编辑人
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
        /// 所属部门
        /// </summary>
        [DataMember]
        public virtual Int32 DeptId
        {
            get
            {
                return this.m_DeptId;
            }
            set
            {
                this.m_DeptId = value;
            }
        }

        /// <summary>
        /// 所属公司
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

        #endregion

    }

    [Serializable]
    [DataContract]
    public class ContactGroupNodeInfo : ContactGroupInfo
    {
        private List<ContactGroupNodeInfo> m_Children = new List<ContactGroupNodeInfo>();

        /// <summary>
        /// 子分组实体类对象集合
        /// </summary>
        [DataMember]
        public List<ContactGroupNodeInfo> Children
        {
            get { return m_Children; }
            set { m_Children = value; }
        }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public ContactGroupNodeInfo()
        {
            this.m_Children = new List<ContactGroupNodeInfo>();
        }

        /// <summary>
        /// 参数构造函数
        /// </summary>
        /// <param name="info">ContactGroupInfo对象</param>
        public ContactGroupNodeInfo(ContactGroupInfo info)
        {
            base.Id = info.Id;
            base.UserCode = info.UserCode;
            base.Name = info.Name;
            base.Pid = info.Pid;
            base.Remark = info.Remark;
            base.EditorId = info.EditorId;
            base.LastUpdateTime = info.LastUpdateTime;
            base.CreatorId = info.CreatorId;
            base.CreatorTime = info.CreatorTime;
            base.DeptId = info.DeptId;
            base.CompanyId = info.CompanyId;
        }
    }
}