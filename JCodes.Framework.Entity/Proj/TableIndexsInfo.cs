using DevExpress.XtraEditors.DXErrorProvider;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace JCodes.Framework.Entity
{
    public class TableIndexsInfo : IDXDataErrorInfo
    {
        private string guid;

        [DisplayName("GUID")]
        public string GUID
        {
            get { return guid; }
            set { guid = value; }
        }

        private string indexName;

        [DisplayName("索引名")]
        public string IndexName
        {
            get { return indexName; }
            set { indexName = value; }
        }

        private string indexFieldLst;

        [DisplayName("索引字段列表")]
        public string IndexFieldLst
        {
            get { return indexFieldLst; }
            set { indexFieldLst = value; }
        }

        private Boolean unique;

        [DisplayName("唯一")]
        public Boolean Unique
        {
            get { return unique; }
            set { unique = value; }
        }

        private Boolean index;

        [DisplayName("索引")]
        public Boolean Index
        {
            get { return index; }
            set { index = value; }
        }

        private Boolean primary;

        [DisplayName("主键")]
        public Boolean Primary
        {
            get { return primary; }
            set { primary = value; }
        }

        #region IDXDataErrorInfo Members

        /// <summary>
        /// 用来保存行数据中字段名，错误信息
        /// </summary>
        public Dictionary<string, ErrorInfo> lstInfo
        {
            get;
            set;
        }

        //<gridControl1>
        void IDXDataErrorInfo.GetPropertyError(string propertyName, ErrorInfo info)
        {
            // 添加自定义错误
            if (lstInfo != null && lstInfo.Count > 0 && lstInfo.ContainsKey(propertyName) && !string.IsNullOrEmpty(lstInfo[propertyName].ErrorText))
            {
                info.ErrorText = lstInfo[propertyName].ErrorText;
                info.ErrorType = lstInfo[propertyName].ErrorType;
            }
        }
        void IDXDataErrorInfo.GetError(ErrorInfo info) { }
        //</gridControl1>
        #endregion
    }
}
