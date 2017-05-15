using System;
using System.Xml.Serialization;

namespace JCodes.Framework.Entity
{
    [Serializable]
    public class ReportMonthlyCostDetailInfo : BaseEntity
    {    
        #region Field Members

        private int m_ID = 0;         
        private int m_Header_ID = 0; //报表头ID          
        private int m_ReportYear = 0; //报表年份          
        private int m_ReportMonth = 0; //报表月份          
        private string m_YearMonth = ""; //报表年月          
        private string m_DeptName = ""; //部门项目          
        private string m_ItemType = ""; //备件类别          
        private decimal m_TotalMoney = 0; //总金额          
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
        /// 部门项目
        /// </summary>
        public virtual string DeptName
        {
            get
            {
                return this.m_DeptName;
            }
            set
            {
                this.m_DeptName = value;
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
        /// 总金额
        /// </summary>
        public virtual decimal TotalMoney
        {
            get
            {
                return this.m_TotalMoney;
            }
            set
            {
                this.m_TotalMoney = value;
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