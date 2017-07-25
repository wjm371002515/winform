using System;
using System.Runtime.Serialization;

namespace JCodes.Framework.Entity
{
    [Serializable]
    [DataContract]
    public class DicKeyValueInfo
    {
        #region Field Members
        private Int32 m_DictType_ID = 0; //字典类型名称        
        private Int32 m_Value = 0; //字典值内容  
        private string m_Name = ""; //字典显示名称          
        #endregion

        #region Property Members
        /// <summary>
        /// 字典类型名称
        /// </summary>
        [DataMember]
        public virtual Int32 DictType_ID
        {
            get
            {
                return this.m_DictType_ID;
            }
            set
            {
                this.m_DictType_ID = value;
            }
        }

        /// <summary>
        /// 字典值内容
        /// </summary>
        [DataMember]
        public virtual Int32 Value
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

        public override string ToString()
        {
            return string.Format("{0}-{1}", this.m_Value, this.m_Name);
        }
        #endregion
    }
}
