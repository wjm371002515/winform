using System;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace JCodes.Framework.Entity
{
    /// <summary>
    /// 部门机构信息
    /// </summary>
    [Serializable]
    [DataContract]
    public class OUInfo : BaseEntity
    { 
        #region Field Members
        private int m_Id = 0; //          
        private int m_Pid = -1; //父ID          
        private string m_OuCode; //机构编码          
        private string m_Name; //机构名称          
        private string m_Seq; //排序码          
        private Int32 m_OuType; //机构分类          
        private string m_Address; //机构地址          
        private string m_OutPhone; //外线电话          
        private string m_InnerPhone; //内线电话          
        private string m_Remark; //备注                        
        private Int32 m_CreatorId; //创建人ID          
        private DateTime m_CreatorTime = System.DateTime.Now; //创建时间                  
        private Int32 m_EditorId; //编辑人ID          
        private DateTime m_LastUpdateTime = System.DateTime.Now; //编辑时间          
        private Int32 m_IsDelete; //是否已删除          
        private Int32 m_IsForbid; //有效标志   
        private Int32 m_CompanyId; //所属公司ID          

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
        /// 机构编码
        /// </summary>
		[DataMember]
        public virtual string OuCode
        {
            get
            {
                return this.m_OuCode;
            }
            set
            {
                this.m_OuCode = value;
            }
        }

        /// <summary>
        /// 机构名称
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
        /// 机构分类
        /// </summary>
		[DataMember]
        public virtual Int32 OuType
        {
            get
            {
                return this.m_OuType;
            }
            set
            {
                this.m_OuType = value;
            }
        }

        /// <summary>
        /// 机构地址
        /// </summary>
        [DataMember]
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
        /// 外线电话
        /// </summary>
		[DataMember]
        public virtual string OutPhone
        {
            get
            {
                return this.m_OutPhone;
            }
            set
            {
                this.m_OutPhone = value;
            }
        }

        /// <summary>
        /// 内线电话
        /// </summary>
		[DataMember]
        public virtual string InnerPhone
        {
            get
            {
                return this.m_InnerPhone;
            }
            set
            {
                this.m_InnerPhone = value;
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
        /// 创建人ID
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
        #endregion

    }

    /// <summary>
    /// 部门机构节点对象
    /// </summary>
    [Serializable]
    [DataContract]
    public class OUNodeInfo : OUInfo
    {
        private List<OUNodeInfo> m_Children = new List<OUNodeInfo>();

        /// <summary>
        /// 子机构实体类对象集合
        /// </summary>
        [DataMember]
        public List<OUNodeInfo> Children
        {
            get { return m_Children; }
            set { m_Children = value; }
        }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public OUNodeInfo()
        {
            this.m_Children = new List<OUNodeInfo>();
        }

        /// <summary>
        /// 参数构造函数
        /// </summary>
        /// <param name="info">OUInfo对象</param>
        public OUNodeInfo(OUInfo info)
        {
            base.Id = info.Id;
            base.Pid = info.Pid;
            base.OuCode = info.OuCode;
            base.Name = info.Name;
            base.Seq = info.Seq;
            base.OuType = info.OuType;
            base.Address = info.Address;
            base.OutPhone = info.OutPhone;
            base.InnerPhone = info.InnerPhone;
            base.Remark = info.Remark;
            base.CreatorId = info.CreatorId;
            base.CreatorTime = info.CreatorTime;
            base.EditorId = info.EditorId;
            base.LastUpdateTime = info.LastUpdateTime;
            base.IsDelete = info.IsDelete;
            base.IsForbid = info.IsForbid;
            base.CompanyId = info.CompanyId;
        }
    }
}