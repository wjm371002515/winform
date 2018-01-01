using System;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace JCodes.Framework.Entity
{
    /// <summary>
    /// 系统功能定义
    /// </summary>
    [Serializable]
    [DataContract]
    public class FunctionInfo : BaseEntity
    {
        #region Field Members

        private string m_ID = System.Guid.NewGuid().ToString(); //          
        private string m_PID = "-1"; //父ID          
        private string m_Name; //功能名称          
        private string m_FunctionId; //功能ID    
        private string m_SystemType_ID; //系统编号          
        private string m_Seq; //排序码          

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
        /// 父ID
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
        /// 功能名称
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
        /// 功能ID
        /// </summary>
        [DataMember]
        public virtual string FunctionId
        {
            get
            {
                return this.m_FunctionId;
            }
            set
            {
                this.m_FunctionId = value;
            }
        }

        /// <summary>
        /// 系统编号
        /// </summary>
        [DataMember]
        public virtual string SystemType_ID
        {
            get
            {
                return this.m_SystemType_ID;
            }
            set
            {
                this.m_SystemType_ID = value;
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

    /// <summary>
    /// 系统功能节点对象
    /// </summary>
    [Serializable]
    [DataContract]
    public class FunctionNodeInfo : FunctionInfo
    {
        private List<FunctionNodeInfo> m_Children = new List<FunctionNodeInfo>();

        /// <summary>
        /// 子菜单实体类对象集合
        /// </summary>
        [DataMember]
        public List<FunctionNodeInfo> Children
        {
            get { return m_Children; }
            set { m_Children = value; }
        }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public FunctionNodeInfo()
        {
            this.m_Children = new List<FunctionNodeInfo>();
        }

        /// <summary>
        /// 参数构造函数
        /// </summary>
        /// <param name="functionInfo">FunctionInfo对象</param>
        public FunctionNodeInfo(FunctionInfo functionInfo)
        {
            base.FunctionId = functionInfo.FunctionId;
            base.ID = functionInfo.ID;
            base.Name = functionInfo.Name;
            base.PID = functionInfo.PID;
            base.SystemType_ID = functionInfo.SystemType_ID;
            base.Seq = functionInfo.Seq;
        }
    }
}