using System;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Collections.Generic;
using JCodes.Framework.jCodesenum;

namespace JCodes.Framework.Entity
{
    /// <summary>
    /// 通讯录分组
    /// </summary>
    [DataContract]
    public class AddressGroupInfo : BaseEntity
    {    
        #region Field Members

        private AddressType m_AddressType = AddressType.个人; //通讯录类型[个人,公司]          
        private Int32 m_Id; //          
        private Int32 m_Pid; //父ID          
        private string m_Name; //分组名称          
        private string m_Remark; //备注          
        private string m_Seq; //排序序号          
        private Int32 m_CreatorId; //创建人          
        private DateTime m_CreatorTime; //创建时间          
        private Int32 m_EditorId; //编辑人          
        private DateTime m_LastUpdateTime; //编辑时间          
        private Int32 m_DeptId; //所属部门
        private Int32 m_CompanyId; //所属公司

        #endregion

        #region Property Members

        /// <summary>
        /// 通讯录类型[个人,公司]
        /// </summary>
        [DataMember]
        public virtual AddressType AddressType
        {
            get
            {
                return this.m_AddressType;
            }
            set
            {
                this.m_AddressType = value;
            }
        }

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
        /// 父ID
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
        /// 排序序号
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
    public class AddressGroupNodeInfo : AddressGroupInfo
    {
        private List<AddressGroupNodeInfo> m_Children = new List<AddressGroupNodeInfo>();

        /// <summary>
        /// 子分组实体类对象集合
        /// </summary>
        [DataMember]
        public List<AddressGroupNodeInfo> Children
        {
            get { return m_Children; }
            set { m_Children = value; }
        }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public AddressGroupNodeInfo()
        {
            this.m_Children = new List<AddressGroupNodeInfo>();
        }

        /// <summary>
        /// 参数构造函数
        /// </summary>
        /// <param name="info">AddressGroupInfo对象</param>
        public AddressGroupNodeInfo(AddressGroupInfo info)
        {
            base.AddressType = info.AddressType;
            base.Id = info.Id;
            base.Seq = info.Seq;
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