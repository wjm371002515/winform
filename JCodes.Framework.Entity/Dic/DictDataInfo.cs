using System;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace JCodes.Framework.Entity
{
    // 20180219 wjm 手工调整类对象
    [Serializable]
    [DataContract]
    public class DictDataInfo : BaseEntity
    {    
        #region Field Members
        private string m_Gid = Guid.NewGuid().ToString();
        private Int32 m_DicttypeID = 0; //字典类型名称        
        private string m_Value = string.Empty; //字典值内容  
        private string m_Name = string.Empty; //字典显示名称          
        private string m_Remark = string.Empty; //备注信息          
        private string m_Seq = string.Empty; //排序          
        private Int32 m_EditorId = 0; //编辑者          
        private DateTime m_LastUpdateTime = System.DateTime.Now; //编辑时间          

        #endregion

        #region Property Members
        /// <summary>
        /// GID编码
        /// </summary>
        [DataMember]
        public virtual string Gid
        {
            get
            {
                return this.m_Gid;
            }
            set
            {
                this.m_Gid = value;
            }
        }

        /// <summary>
        /// 字典类型名称
        /// </summary>
        [DataMember]
        public virtual Int32 DicttypeID
        {
            get
            {
                return this.m_DicttypeID;
            }
            set
            {
                this.m_DicttypeID = value;
            }
        }

        /// <summary>
        /// 字典值内容
        /// </summary>
        [DataMember]
        public virtual string Value
        {
            get
            {
                return this.m_Value;
            }
            set
            {
                this.m_Value = value;
            }
        }

        /// <summary>
        /// 字典显示名称
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
        /// 编辑者
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