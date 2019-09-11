using DevExpress.XtraEditors.DXErrorProvider;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace JCodes.Framework.Entity
{
    public class DiyFieldInfo : IDXDataErrorInfo
    {
        private string guid;

        [DisplayName("GUID")]
        public string GUID
        {
            get { return guid; }
            set { guid = value; }
        }

        private string name;

        [DisplayName("字段")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string chinesename;

        [DisplayName("中文名")]
        public string ChineseName
        {
            get { return chinesename; }
            set { chinesename = value; }
        }

        private string content;

        [DisplayName("属性内容")]
        public string Content
        {
            get { return content; }
            set { content = value; }
        }

        private string remark;

        [DisplayName("属性内容")]
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
