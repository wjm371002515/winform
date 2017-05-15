using System;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace JCodes.Framework.Entity
{
    /// <summary>
    /// 记录操作日志的数据表配置
    /// </summary>
    [DataContract]
    public class OperationLogSettingInfo : BaseEntity
    {    
        #region Field Members

        private string m_ID = System.Guid.NewGuid().ToString(); //          
        private bool m_Forbid = false; //是否禁用          
        private string m_TableName; //数据库表          
        private bool m_InsertLog = false; //记录插入日志          
        private bool m_DeleteLog = false; //记录删除日志          
        private bool m_UpdateLog = false; //记录更新日志          
        private string m_Note; //备注          
        private string m_Creator; //创建人          
        private string m_Creator_ID; //创建人ID          
        private DateTime m_CreateTime = System.DateTime.Now; //创建时间          
        private string m_Editor; //编辑人          
        private string m_Editor_ID; //编辑人ID          
        private DateTime m_EditTime = System.DateTime.Now; //编辑时间          

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
        /// 是否禁用
        /// </summary>
		[DataMember]
        public virtual bool Forbid
        {
            get
            {
                return this.m_Forbid;
            }
            set
            {
                this.m_Forbid = value;
            }
        }

        /// <summary>
        /// 数据库表
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
        /// 记录插入日志
        /// </summary>
		[DataMember]
        public virtual bool InsertLog
        {
            get
            {
                return this.m_InsertLog;
            }
            set
            {
                this.m_InsertLog = value;
            }
        }

        /// <summary>
        /// 记录删除日志
        /// </summary>
		[DataMember]
        public virtual bool DeleteLog
        {
            get
            {
                return this.m_DeleteLog;
            }
            set
            {
                this.m_DeleteLog = value;
            }
        }

        /// <summary>
        /// 记录更新日志
        /// </summary>
		[DataMember]
        public virtual bool UpdateLog
        {
            get
            {
                return this.m_UpdateLog;
            }
            set
            {
                this.m_UpdateLog = value;
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


        #endregion

    }
}