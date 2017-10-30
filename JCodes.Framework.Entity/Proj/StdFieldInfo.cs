using DevExpress.XtraEditors.DXErrorProvider;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace JCodes.Framework.Entity
{
    public class StdFieldInfo : IDXDataErrorInfo
    {
        public StdFieldInfo()
        { }

        private string guid;

        [DisplayName("GUID")]
        public string GUID
        {
            get { return guid; }
            set { guid = value; }
        }

        /// <summary>
        /// 字段名
        /// </summary>
        private string name;

        [DisplayName("字段名")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string chineseName;

        [DisplayName("字段名称")]
        public string ChineseName
        {
            get { return chineseName; }
            set { chineseName = value; }
        }

        private string datatype;

        [DisplayName("字段类型")]
        public string DataType
        {
            get { return datatype; }
            set { datatype = value; }
        }

        private string dictno;

        [DisplayName("字典条目")]
        public string DictNo
        {
            get { return dictno; }
            set { dictno = value; }
        }

        private string dictnamelst;

        [DisplayName("字典条目说明")]
        public string DictNameLst
        {
            get { return dictnamelst; }
            set { dictnamelst = value; }
        }

        private string remark;

        [DisplayName("说明")]
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }

        /// <summary>
        /// 用来保存行数据中字段名，错误信息
        /// </summary>
        public Dictionary<string, ErrorInfo> lstInfo
        {
            get;
            set;
        }

        #region IDXDataErrorInfo Members
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
