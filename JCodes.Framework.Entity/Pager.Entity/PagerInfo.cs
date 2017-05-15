using System;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace JCodes.Framework.Entity
{
    public delegate void PageInfoChanged(PagerInfo info);

    [Serializable]
    [DataContract]
    public class PagerInfo
    {
        public event PageInfoChanged OnPageInfoChanged;

        private int currenetPageIndex; //当前页码
        private int pageSize;//每页显示的记录
        private int recordCount;//记录总数

        #region 属性变量

        /// <summary>
        /// 获取或设置当前页码
        /// </summary>
        [XmlElement(ElementName = "CurrenetPageIndex")]
        [DataMember]
        public int CurrenetPageIndex
        {
            get { return currenetPageIndex; }
            set
            {
                currenetPageIndex = value;

                if (OnPageInfoChanged != null)
                {
                    OnPageInfoChanged(this);
                }
            }
        }

        /// <summary>
        /// 获取或设置每页显示的记录
        /// </summary>
        [XmlElement(ElementName = "PageSize")]
        [DataMember]
        public int PageSize
        {
            get { return pageSize; }
            set
            {
                pageSize = value;
                if (OnPageInfoChanged != null)
                {
                    OnPageInfoChanged(this);
                }
            }
        }

        /// <summary>
        /// 获取或设置记录总数
        /// </summary>
        [XmlElement(ElementName = "RecordCount")]
        [DataMember]
        public int RecordCount
        {
            get { return recordCount; }
            set
            {
                recordCount = value;
                if (OnPageInfoChanged != null)
                {
                    OnPageInfoChanged(this);
                }
            }
        }

        #endregion
    }
}
