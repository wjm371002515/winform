using System;
using System.Xml.Serialization;

namespace JCodes.Framework.Entity
{
    [Serializable]
    public class ReportMonthlyDetailInfo : BaseEntity
    {    
        #region Field Members

        private int m_ID = 0;         
        private int m_Header_ID = 0; //报表头ID          
        private int m_ReportYear = 0; //报表年份          
        private int m_ReportMonth = 0; //报表月份          
        private string m_YearMonth = ""; //报表年月          
        private string m_ItemName = ""; //项目名称          
        private int m_LastCount = 0; //上月结存数量          
        private decimal m_LastMoney = 0; //上月结存金额          
        private int m_CurrentInCount = 0; //本月入库数量          
        private decimal m_CurrentInMoney = 0; //本月入库金额          
        private int m_CurrentOutCount = 0; //本月出库数量          
        private decimal m_CurrentOutMoney = 0; //本月出库金额          
        private int m_CurrentCount = 0; //本月结存数量          
        private decimal m_CurrentMoney = 0; //本月结存金额          
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
        /// 报表月份
        /// </summary>
        public virtual int ReportMonth
        {
            get
            {
                return this.m_ReportMonth;
            }
            set
            {
                this.m_ReportMonth = value;
            }
        }

        /// <summary>
        /// 报表年月
        /// </summary>
        public virtual string YearMonth
        {
            get
            {
                return this.m_YearMonth;
            }
            set
            {
                this.m_YearMonth = value;
            }
        }

        /// <summary>
        /// 项目名称
        /// </summary>
        public virtual string ItemName
        {
            get
            {
                return this.m_ItemName;
            }
            set
            {
                this.m_ItemName = value;
            }
        }

        /// <summary>
        /// 上月结存数量
        /// </summary>
        public virtual int LastCount
        {
            get
            {
                return this.m_LastCount;
            }
            set
            {
                this.m_LastCount = value;
            }
        }

        /// <summary>
        /// 上月结存金额
        /// </summary>
        public virtual decimal LastMoney
        {
            get
            {
                return this.m_LastMoney;
            }
            set
            {
                this.m_LastMoney = value;
            }
        }

        /// <summary>
        /// 本月入库数量
        /// </summary>
        public virtual int CurrentInCount
        {
            get
            {
                return this.m_CurrentInCount;
            }
            set
            {
                this.m_CurrentInCount = value;
            }
        }

        /// <summary>
        /// 本月入库金额
        /// </summary>
        public virtual decimal CurrentInMoney
        {
            get
            {
                return this.m_CurrentInMoney;
            }
            set
            {
                this.m_CurrentInMoney = value;
            }
        }

        /// <summary>
        /// 本月出库数量
        /// </summary>
        public virtual int CurrentOutCount
        {
            get
            {
                return this.m_CurrentOutCount;
            }
            set
            {
                this.m_CurrentOutCount = value;
            }
        }

        /// <summary>
        /// 本月出库金额
        /// </summary>
        public virtual decimal CurrentOutMoney
        {
            get
            {
                return this.m_CurrentOutMoney;
            }
            set
            {
                this.m_CurrentOutMoney = value;
            }
        }

        /// <summary>
        /// 本月结存数量
        /// </summary>
        public virtual int CurrentCount
        {
            get
            {
                return this.m_CurrentCount;
            }
            set
            {
                this.m_CurrentCount = value;
            }
        }

        /// <summary>
        /// 本月结存金额
        /// </summary>
        public virtual decimal CurrentMoney
        {
            get
            {
                return this.m_CurrentMoney;
            }
            set
            {
                this.m_CurrentMoney = value;
            }
        }

        /// <summary>
        /// 额外的数据分类码
        /// </summary>
        public string ReportCode
        {
            get { return m_ReportCode; }
            set { m_ReportCode = value; }
        }

        #endregion

    }
}