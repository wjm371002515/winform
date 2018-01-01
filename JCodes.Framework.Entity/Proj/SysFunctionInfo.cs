using System;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Collections.Generic;
using DevExpress.XtraEditors.DXErrorProvider;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
    /// <summary>
    /// 系统功能定义
    /// </summary>
    [Serializable]
    [DataContract]
    public class SysFunctionInfo : IDXDataErrorInfo
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

        [DisplayName("ID")]
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
        [DisplayName("父ID")]
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
        [DisplayName("功能名称")]
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
        [DisplayName("功能ID")]
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
        [DisplayName("系统编号")]
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
        [DisplayName("排序")]
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

        /// <summary>
        /// 用来保存行数据中字段名，错误信息
        /// </summary>
        public Dictionary<string, ErrorInfo> lstInfo
        {
            get;
            set;
        }

        #region IDXDataErrorInfo Members
        //<gridControl1>
        void IDXDataErrorInfo.GetPropertyError(string propertyName, ErrorInfo info)
        {
            // 添加自定义错误
            if (lstInfo != null && lstInfo.Count > 0 && lstInfo.ContainsKey(propertyName) && !string.IsNullOrEmpty(lstInfo[propertyName].ErrorText))
            {
                info.ErrorText = lstInfo[propertyName].ErrorText;
                info.ErrorType = lstInfo[propertyName].ErrorType;
            }
        }
        void IDXDataErrorInfo.GetError(ErrorInfo info) { }
        //</gridControl1>

        #endregion
    }
}