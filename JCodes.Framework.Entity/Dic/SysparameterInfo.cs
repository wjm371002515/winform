using System;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace JCodes.Framework.Entity
{
    /// <summary>
    /// 用户参数配置
    /// </summary>
    [DataContract]
    public class SysparameterInfo : BaseEntity
    {    
        #region Field Members

        private Int32 m_ID = 0;
        private Int32 m_sysId = 0;
        private string m_Name;
        private string m_Value;
        private Int16 m_ControlType = 0;
        private Int32 m_DicNo = 0;
        private Int32 m_Numlen = 0;
        private string m_Remark;        //备注  
        private string m_Seq;
        private string m_Editor;       //创建人          
        private DateTime m_EditorTime = System.DateTime.Now; //创建时间          

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

        [DataMember]
        public virtual Int32 sysId
        {
            get
            {
                return this.m_sysId;
            }
            set
            {
                this.m_sysId = value;
            }
        }

        /// <summary>
        /// 类型名称
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
        /// 类型名称
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

        [DataMember]
        public virtual Int16 ControlType
        {
            get
            {
                return this.m_ControlType;
            }
            set
            {
                this.m_ControlType = value;
            }
        }

        [DataMember]
        public virtual Int32 DicNo
        {
            get
            {
                return this.m_DicNo;
            }
            set
            {
                this.m_DicNo = value;
            }
        }

        [DataMember]
        public virtual Int32 Numlen
        {
            get
            {
                return this.m_Numlen;
            }
            set
            {
                this.m_Numlen = value;
            }
        }

        /// <summary>
        /// 参数文本内容
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
        /// 创建人
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
        /// 创建时间
        /// </summary>
		[DataMember]
        public virtual DateTime EditorTime
        {
            get
            {
                return this.m_EditorTime;
            }
            set
            {
                this.m_EditorTime = value;
            }
        }

        #endregion

    }
}