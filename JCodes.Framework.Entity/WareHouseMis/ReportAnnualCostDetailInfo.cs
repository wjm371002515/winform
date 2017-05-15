using System;
using System.Xml.Serialization;

namespace JCodes.Framework.Entity
{
    [Serializable]
    public class ReportAnnualCostDetailInfo : BaseEntity
    {    
        #region Field Members

        private int m_ID = 0;         
        private int m_Header_ID = 0; //报表头ID          
        private int m_ReportYear = 0; //报表年份          
        private string m_ItemType = ""; //备件类别          
        private string m_CostCenterOrDept = ""; //成本中心或部门          
        private decimal m_One = 0; //总金额          
        private decimal m_Two = 0;         
        private decimal m_Three = 0;         
        private decimal m_Four = 0;         
        private decimal m_Five = 0;         
        private decimal m_Six = 0;         
        private decimal m_Seven = 0;         
        private decimal m_Eight = 0;         
        private decimal m_Nine = 0;         
        private decimal m_Ten = 0;
        private decimal m_Eleven = 0;
        private decimal m_Twelve = 0;
        private decimal m_Total = 0;     
        private string m_ReportCode = ""; //额外的数据分类码          

        #endregion

        #region Property Members
        
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
        /// 报表头ID
        /// </summary>
        public virtual int Header_ID
        {
            get
            {
                return this.m_Header_ID;
            }
            set
            {
                this.m_Header_ID = value;
            }
        }

        /// <summary>
        /// 报表年份
        /// </summary>
        public virtual int ReportYear
        {
            get
            {
                return this.m_ReportYear;
            }
            set
            {
                this.m_ReportYear = value;
            }
        }

        /// <summary>
        /// 备件类别
        /// </summary>
        public virtual string ItemType
        {
            get
            {
                return this.m_ItemType;
            }
            set
            {
                this.m_ItemType = value;
            }
        }

        /// <summary>
        /// 成本中心或部门
        /// </summary>
        public virtual string CostCenterOrDept
        {
            get
            {
                return this.m_CostCenterOrDept;
            }
            set
            {
                this.m_CostCenterOrDept = value;
            }
        }

        /// <summary>
        /// 总金额
        /// </summary>
        public virtual decimal One
        {
            get
            {
                return this.m_One;
            }
            set
            {
                this.m_One = value;
            }
        }

        public virtual decimal Two
        {
            get
            {
                return this.m_Two;
            }
            set
            {
                this.m_Two = value;
            }
        }

        public virtual decimal Three
        {
            get
            {
                return this.m_Three;
            }
            set
            {
                this.m_Three = value;
            }
        }

        public virtual decimal Four
        {
            get
            {
                return this.m_Four;
            }
            set
            {
                this.m_Four = value;
            }
        }

        public virtual decimal Five
        {
            get
            {
                return this.m_Five;
            }
            set
            {
                this.m_Five = value;
            }
        }

        public virtual decimal Six
        {
            get
            {
                return this.m_Six;
            }
            set
            {
                this.m_Six = value;
            }
        }

        public virtual decimal Seven
        {
            get
            {
                return this.m_Seven;
            }
            set
            {
                this.m_Seven = value;
            }
        }

        public virtual decimal Eight
        {
            get
            {
                return this.m_Eight;
            }
            set
            {
                this.m_Eight = value;
            }
        }

        public virtual decimal Nine
        {
            get
            {
                return this.m_Nine;
            }
            set
            {
                this.m_Nine = value;
            }
        }

        public virtual decimal Ten
        {
            get
            {
                return this.m_Ten;
            }
            set
            {
                this.m_Ten = value;
            }
        }

        public virtual decimal Eleven
        {
            get
            {
                return this.m_Eleven;
            }
            set
            {
                this.m_Eleven = value;
            }
        }

        public virtual decimal Twelve
        {
            get
            {
                return this.m_Twelve;
            }
            set
            {
                this.m_Twelve = value;
            }
        }

        public virtual decimal Total
        {
            get
            {
                return this.m_Total;
            }
            set
            {
                this.m_Total = value;
            }
        }
        /// <summary>
        /// 额外的数据分类码
        /// </summary>
        public virtual string ReportCode
        {
            get
            {
                return this.m_ReportCode;
            }
            set
            {
                this.m_ReportCode = value;
            }
        }


        #endregion

    }
}