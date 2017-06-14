using System;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace JCodes.Framework.Entity
{
    [Serializable]
    [DataContract]
    public class DictTypeInfo : BaseEntity
    {    
        #region Field Members

        private Int32 m_ID = 0;         
        private string m_Name = ""; //字典类型名称          
        private string m_Remark = ""; //备注说明          
        private string m_Seq = ""; //排序          
        private string m_Editor = ""; //编辑者          
        private DateTime m_LastUpdated = System.DateTime.Now; //编辑时间   
        private Int32 m_PID = 0;//字典大类


        #endregion

        #region Property Members

        [DataMember]
        public virtual Int32 ID
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
        /// 字典类型名称
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
        /// 备注说明
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
        public virtual DateTime LastUpdated
        {
            get
            {
                return this.m_LastUpdated;
            }
            set
            {
                this.m_LastUpdated = value;
            }
        }

        /// <summary>
        /// 字典大类
        /// </summary>
        [DataMember]
        public Int32 PID
        {
            get { return m_PID; }
            set { m_PID = value; }
        }

        #endregion

    }
}