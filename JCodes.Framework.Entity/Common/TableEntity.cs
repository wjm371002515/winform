using System;
using System.Runtime.Serialization;

namespace JCodes.Framework.Entity.Common
{
    public class TableEntity
    {
        #region Field Members
        private string m_TableName = string.Empty;
        private string m_Remark = string.Empty;
        private string m_FolderName = string.Empty;
        private Int32 m_IsIDXDataError = 0;
        private Int32 m_IsBaseEntity = 0;
        private string m_ToStringContent = string.Empty;
        private string m_ConstructContent = string.Empty;
        private string m_CustomParentClass = string.Empty;
        private string m_CustomContent = string.Empty;
        private string m_CustomNamespace = string.Empty;
        
        #endregion

        #region Property Members
        /// <summary>
        /// 表名
        /// </summary>
        [DataMember]
        public virtual string TableName
        {
            get
            {
                return this.m_TableName;
            }
            set
            {
                this.m_TableName = value;
            }
        }

        /// <summary>
        /// 备注信息
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
        /// 文件夹名字
        /// </summary>
        [DataMember]
        public virtual string FolderName
        {
            get
            {
                return this.m_FolderName;
            }
            set
            {
                this.m_FolderName = value;
            }
        }

        /// <summary>
        /// 是否继承IDXDataError
        /// </summary>
        [DataMember]
        public virtual Int32 IsIDXDataError
        {
            get
            {
                return this.m_IsIDXDataError;
            }
            set
            {
                this.m_IsIDXDataError = value;
            }
        }

        /// <summary>
        /// 是否继承BaseEntity
        /// </summary>
        [DataMember]
        public virtual Int32 IsBaseEntity
        {
            get
            {
                return this.m_IsBaseEntity;
            }
            set
            {
                this.m_IsBaseEntity = value;
            }
        }

        /// <summary>
        /// ToString内容
        /// </summary>
        [DataMember]
        public virtual string ToStringContent
        {
            get
            {
                return this.m_ToStringContent;
            }
            set
            {
                this.m_ToStringContent = value;
            }
        }

        /// <summary>
        /// 构造函数内容
        /// </summary>
        [DataMember]
        public virtual string ConstructContent
        {
            get
            {
                return this.m_ConstructContent;
            }
            set
            {
                this.m_ConstructContent = value;
            }
        }


        /// <summary>
        /// 自定义父类
        /// </summary>
        [DataMember]
        public virtual string CustomParentClass
        {
            get
            {
                return this.m_CustomParentClass;
            }
            set
            {
                this.m_CustomParentClass = value;
            }
        }

        /// <summary>
        /// 自定义文本
        /// </summary>
        [DataMember]
        public virtual string CustomContent
        {
            get
            {
                return this.m_CustomContent;
            }
            set
            {
                this.m_CustomContent = value;
            }
        }

        /// <summary>
        /// 自定义命名空间
        /// </summary>
        [DataMember]
        public virtual string CustomNamespace
        {
            get
            {
                return this.m_CustomNamespace;
            }
            set
            {
                this.m_CustomNamespace = value;
            }
        }
        #endregion
    }

    public class TableFieldEntity
    {
        #region 字段
        private string m_TableName = string.Empty;
        private string m_TableField = string.Empty;
        private string m_Seq = string.Empty;

        #endregion

        #region 属性
        /// <summary>
        /// 表名
        /// </summary>
        [DataMember]
        public virtual string TableName
        {
            get
            {
                return this.m_TableName;
            }
            set
            {
                this.m_TableName = value;
            }
        }

        /// <summary>
        /// 表格字段
        /// </summary>
        [DataMember]
        public virtual string TableField
        {
            get
            {
                return this.m_TableField;
            }
            set
            {
                this.m_TableField = value;
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
        #endregion
    }
}
