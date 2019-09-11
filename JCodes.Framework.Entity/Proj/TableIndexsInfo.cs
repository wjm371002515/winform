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

        private string constrainttype;

        /// <summary>
        /// 0 - 主键
        /// 1 - 索引
        /// 2 - 唯一索引
        /// </summary>
        [DisplayName("约束类型")]
        public string ConstraintType
        {
            get { return constrainttype; }
            set { constrainttype = value; }
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
