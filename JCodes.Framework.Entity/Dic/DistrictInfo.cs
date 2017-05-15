using System;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace JCodes.Framework.Entity
{
    [Serializable]
    [DataContract]
    public class DistrictInfo : BaseEntity
    {    
        #region Field Members

        private int m_ID = 0;         
        private string m_DistrictName;
        private int m_CityID = 0;         

        #endregion

        #region Property Members

        /// <summary>
        /// 行政区ID
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
        /// 行政区名称
        /// </summary>
        [DataMember]
        public virtual string DistrictName
        {
            get
            {
                return this.m_DistrictName;
            }
            set
            {
                this.m_DistrictName = value;
            }
        }

        /// <summary>
        /// 所属城市ID
        /// </summary>
        [DataMember]
        public virtual int CityID
        {
            get
            {
                return this.m_CityID;
            }
            set
            {
                this.m_CityID = value;
            }
        }


        #endregion

    }
}