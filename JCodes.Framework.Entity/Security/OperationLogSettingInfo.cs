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

        private Int32 m_Id; //          
        private Int32 m_IsForbid; //是否禁用          
        private string m_TableName; //数据库表          
        private Int32 m_IsInsertLog; //记录插入日志          
        private Int32 m_IsDeleteLog; //记录删除日志          
        private Int32 m_IsUpdateLog; //记录更新日志          
        private string m_Remark; //备注                 
        private Int32 m_CreatorId; //创建人ID          
        private DateTime m_CreatorTime = System.DateTime.Now; //创建时间                   
        private Int32 m_EditorId; //编辑人ID          
        private DateTime m_LastUpdateTime = System.DateTime.Now; //编辑时间          

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
        /// 是否禁用
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
        public virtual Int32 IsInsertLog
        {
            get
            {
                return this.m_IsInsertLog;
            }
            set
            {
                this.m_IsInsertLog = value;
            }
        }

        /// <summary>
        /// 记录删除日志
        /// </summary>
		[DataMember]
        public virtual Int32 IsDeleteLog
        {
            get
            {
                return this.m_IsDeleteLog;
            }
            set
            {
                this.m_IsDeleteLog = value;
            }
        }

        /// <summary>
        /// 记录更新日志
        /// </summary>
		[DataMember]
        public virtual Int32 IsUpdateLog
        {
            get
            {
                return this.m_IsUpdateLog;
            }
            set
            {
                this.m_IsUpdateLog = value;
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


        #endregion

    }
}