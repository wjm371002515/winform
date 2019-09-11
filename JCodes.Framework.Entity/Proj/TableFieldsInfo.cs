using DevExpress.XtraEditors.DXErrorProvider;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace JCodes.Framework.Entity
{
    public class TableFieldsInfo : IDXDataErrorInfo
    {
        private string guid;

        [DisplayName("GUID")]
        public string GUID
        {
            get { return guid; }
            set { guid = value; }
        }

        private string fieldName;

        [DisplayName("字段名")]
        public string FieldName
        {
            get { return fieldName; }
            set { fieldName = value; }
        }

        private string chineseName;

        [DisplayName("中文名")]
        public string ChineseName
        {
            get { return chineseName; }
            set { chineseName = value; }
        }
            
        private string fieldType;

        [DisplayName("字段类型")]
        public string FieldType
        {
            get { return fieldType; }
            set { fieldType = value; }
        }

        private Int32 dictno;

        [DisplayName("数据字典")]
        public Int32 DictNo
        {
            get { return dictno; }
            set { dictno = value; }
        }

        private string fieldInfo;

        [DisplayName("字段说明")]
        public string FieldInfo
        {
            get { return fieldInfo; }
            set { fieldInfo = value; }
        }

        private bool isNull;

        [DisplayName("允许空")]
        public bool IsNull
        {
            get { return isNull; }
            set { isNull = value; }
        }

        private string remark;

        [DisplayName("备注")]
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
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
