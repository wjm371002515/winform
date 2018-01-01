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

        private string m_ID = System.Guid.NewGuid().ToString(); //          
        private string m_PID; //上级ID          
        private string m_HandNo; //编号          
        private string m_Name; //分组名称          
        private string m_Note; //备注          
        private string m_Creator; //创建人          
        private DateTime m_CreateTime; //创建时间          
        private string m_Editor; //编辑人          
        private DateTime m_EditTime; //编辑时间          
        private string m_Dept_ID; //所属部门
        private string m_Company_ID; //所属公司
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
        /// 上级ID
        /// </summary>
		[DataMember]
        public virtual string PID
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
        /// 编号
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
        /// 所属部门
        /// </summary>
        [DataMember]
        public virtual string Dept_ID
        {
            get
            {
                return this.m_Dept_ID;
            }
            set
            {
                this.m_Dept_ID = value;
            }
        }

        /// <summary>
        /// 所属公司
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
            base.ID = info.ID;
            base.HandNo = info.HandNo;
            base.Name = info.Name;
            base.PID = info.PID;
            base.Note = info.Note;
            base.Editor = info.Editor;
            base.EditTime = info.EditTime;
            base.Creator = info.Creator;
            base.CreateTime = info.CreateTime;
            base.Dept_ID = info.Dept_ID;
            base.Company_ID = info.Company_ID;
        }
    }
}