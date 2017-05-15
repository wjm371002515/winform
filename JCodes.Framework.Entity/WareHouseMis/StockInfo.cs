using System;
using System.Xml.Serialization;

namespace JCodes.Framework.Entity
{
    [Serializable]
    public class StockInfo : BaseEntity
    {    
        #region Field Members

        private int m_ID = 0;         
        private string m_ItemNo = "";         
        private string m_ItemName = "";         
        private string m_ItemBigType = "";         
        private string m_ItemType = "";         
        private int m_StockQuantity = 0; //库存量          
        private string m_StockMoney = ""; //库存金额          
        private int m_LowWarning = 0; //低储预警          
        private int m_HighWarning = 0; //超储预警          
        private string m_WareHouse = "";         
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
        /// 备件编号
        /// </summary>
        public virtual string ItemNo
        {
            get
            {
                return this.m_ItemNo;
            }
            set
            {
                this.m_ItemNo = value;
            }
        }

        /// <summary>
        /// 备件名称
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
        /// 备件属类
        /// </summary>
        public virtual string ItemBigType
        {
            get
            {
                return this.m_ItemBigType;
            }
            set
            {
                this.m_ItemBigType = value;
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
        /// 库存量
        /// </summary>
        public virtual int StockQuantity
        {
            get
            {
                return this.m_StockQuantity;
            }
            set
            {
                this.m_StockQuantity = value;
            }
        }

        /// <summary>
        /// 库存金额
        /// </summary>
        public virtual string StockMoney
        {
            get
            {
                return this.m_StockMoney;
            }
            set
            {
                this.m_StockMoney = value;
            }
        }

        /// <summary>
        /// 低储预警
        /// </summary>
        public virtual int LowWarning
        {
            get
            {
                return this.m_LowWarning;
            }
            set
            {
                this.m_LowWarning = value;
            }
        }

        /// <summary>
        /// 超储预警
        /// </summary>
        public virtual int HighWarning
        {
            get
            {
                return this.m_HighWarning;
            }
            set
            {
                this.m_HighWarning = value;
            }
        }

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