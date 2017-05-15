using System;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace JCodes.Framework.Entity
{
    /// <summary>
    /// 角色的数据权限
    /// </summary>
    [DataContract]
    public class RoleDataInfo : BaseEntity
    {    
        #region Field Members

        private string m_ID = System.Guid.NewGuid().ToString(); //          
        private int m_Role_ID = 0; //角色ID          
        private string m_BelongCompanys; //所属公司          
        private string m_BelongDepts; //所属部门          
        private string m_ExcludeDepts; //排除部门          
        private string m_Note; //备注          

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
        /// 角色ID
        /// </summary>
		[DataMember]
        public virtual int Role_ID
        {
            get
            {
                return this.m_Role_ID;
            }
            set
            {
                this.m_Role_ID = value;
            }
        }

        /// <summary>
        /// 所属公司
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
        /// 所属部门
        /// </summary>
		[DataMember]
        public virtual string BelongDepts
        {
            get
            {
                return this.m_BelongDepts;
            }
            set
            {
                this.m_BelongDepts = value;
            }
        }

        /// <summary>
        /// 排除部门
        /// </summary>
		[DataMember]
        public virtual string ExcludeDepts
        {
            get
            {
                return this.m_ExcludeDepts;
            }
            set
            {
                this.m_ExcludeDepts = value;
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


        #endregion

    }
}