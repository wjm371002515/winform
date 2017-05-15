using System;
using System.Xml.Serialization;

namespace JCodes.Framework.Entity
{
    [Serializable]
    public class ReportAnnualCostHeaderInfo : BaseEntity
    {    
        #region Field Members

        private int m_ID = 0;         
        private int m_ReportType = 0; //报表类型：1为全年费用汇总报表          
        private string m_ReportTitle = ""; //报表标题          
        private int m_ReportYear = 0; //报表年份          
        private DateTime m_CreateDate = System.DateTime.Now; //报表创建日期          
        private string m_Creator = ""; //报表创建人员          
        private string m_Note = ""; //备注          

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
        /// 报表类型：1为全年费用汇总报表
        /// </summary>
        public virtual int ReportType
        {
            get
            {
                return this.m_ReportType;
            }
            set
            {
                this.m_ReportType = value;
            }
        }

        /// <summary>
        /// 报表标题
        /// </summary>
        public virtual string ReportTitle
        {
            get
            {
                return this.m_ReportTitle;
            }
            set
            {
                this.m_ReportTitle = value;
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
        /// 报表创建日期
        /// </summary>
        public virtual DateTime CreateDate
        {
            get
            {
                return this.m_CreateDate;
            }
            set
            {
                this.m_CreateDate = value;
            }
        }

        /// <summary>
        /// 报表创建人员
        /// </summary>
        public virtual string Creator
        {
            get
            {
                return this.m_Creator;
            }
            set
            {
                this.m_Creator = value;
            }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Note
        {
            get
            {
                return this.m_Note;
            }
            set
            {
                this.m_Note = value;
            }
        }


        #endregion

    }
}