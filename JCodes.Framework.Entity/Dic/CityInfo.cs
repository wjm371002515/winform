using System;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace JCodes.Framework.Entity
{
    [Serializable]
    [DataContract]
    public class CityInfo : BaseEntity
    {    
        #region Field Members

        private int m_ID = 0;         
        private string m_CityName;         
        private string m_ZipCode;
        private int m_ProvinceID = 0;         

        #endregion

        #region Property Members

        /// <summary>
        /// 城市ID
        /// </summary>
        [DataMember]
        public virtual int ID
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
        /// 城市名称
        /// </summary>
        [DataMember]
        public virtual string CityName
        {
            get
            {
                return this.m_CityName;
            }
            set
            {
                this.m_CityName = value;
            }
        }

        /// <summary>
        /// 邮政编码
        /// </summary>
        [DataMember]
        public virtual string ZipCode
        {
            get
            {
                return this.m_ZipCode;
            }
            set
            {
                this.m_ZipCode = value;
            }
        }

        /// <summary>
        /// 所属省份ID
        /// </summary>
        [DataMember]
        public virtual int ProvinceID
        {
            get
            {
                return this.m_ProvinceID;
            }
            set
            {
                this.m_ProvinceID = value;
            }
        }

        #endregion

    }
}