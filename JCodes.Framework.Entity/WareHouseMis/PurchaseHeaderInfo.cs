using System;
using System.Xml.Serialization;

namespace JCodes.Framework.Entity
{
    [Serializable]
    public class PurchaseHeaderInfo : BaseEntity
    {    
        #region Field Members

        private int m_ID = 0;         
        private string m_HandNo = ""; //进货编号          
        private string m_OperationType = ""; //操作类型（进货还是退货）          
        private string m_Manufacture = ""; //供应商          
        private string m_WareHouse = ""; //库房编号          
        private string m_CostCenter = ""; //成本中心          
        private string m_Note = ""; //备注          
        private DateTime m_CreateDate = System.DateTime.Now; //创建日期          
        private string m_Creator = ""; //操作员        
        private int m_CreateYear = 0; //记录年          
        private int m_CreateMonth = 0; //记录月  
        private string m_PickingPeople = "";//领料人

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
        /// 进货编号
        /// </summary>
        public virtual string HandNo
        {
            get
            {
                return this.m_HandNo;
            }
            set
            {
                this.m_HandNo = value;
            }
        }

        /// <summary>
        /// 操作类型（进货还是退货）
        /// </summary>
        public virtual string OperationType
        {
            get
            {
                return this.m_OperationType;
            }
            set
            {
                this.m_OperationType = value;
            }
        }

        /// <summary>
        /// 供应商
        /// </summary>
        public virtual string Manufacture
        {
            get
            {
                return this.m_Manufacture;
            }
            set
            {
                this.m_Manufacture = value;
            }
        }

        /// <summary>
        /// 库房编号
        /// </summary>
        public virtual string WareHouse
        {
            get
            {
                return this.m_WareHouse;
            }
            set
            {
                this.m_WareHouse = value;
            }
        }

        /// <summary>
        /// 成本中心
        /// </summary>
        public string CostCenter
        {
            get { return m_CostCenter; }
            set { m_CostCenter = value; }
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

        /// <summary>
        /// 创建日期
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
        /// 操作员
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
        /// 记录年
        /// </summary>
        public virtual int CreateYear
        {
            get
            {
                return this.m_CreateYear;
            }
            set
            {
                this.m_CreateYear = value;
            }
        }

        /// <summary>
        /// 记录月
        /// </summary>
        public virtual int CreateMonth
        {
            get
            {
                return this.m_CreateMonth;
            }
            set
            {
                this.m_CreateMonth = value;
            }
        }


        /// <summary>
        /// 领料人
        /// </summary>
        public string PickingPeople
        {
            get { return m_PickingPeople; }
            set { m_PickingPeople = value; }
        }

        #endregion

    }
}