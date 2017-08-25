using DevExpress.XtraEditors.DXErrorProvider;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace JCodes.Framework.Entity
{
    public class DataTypeInfo : IDXDataErrorInfo
    {
        public DataTypeInfo()
        { }

        private string guid;

        [DisplayName("GUID")]
        public string GUID
        {
            get { return guid; }
            set { guid = value; }
        }

        /// <summary>
        /// 类型名
        /// </summary>
        private string name;

        [DisplayName("类型名")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string chineseName;

        [DisplayName("名称")]
        public string ChineseName
        {
            get { return chineseName; }
            set { chineseName = value; }
        }
        
        private string oracle;

        [DisplayName("Oracle")]
        public string Oracle
        {
            get { return oracle; }
            set { oracle = value; }
        }

        private string mysql;

        [DisplayName("Mysql")]
        public string Mysql
        {
            get { return mysql; }
            set { mysql = value; }
        }

        private string db2;

        [DisplayName("DB2")]
        public string DB2
        {
            get { return db2; }
            set { db2 = value; }
        }

        private string sqlserver;

        [DisplayName("SqlServer")]
        public string SqlServer
        {
            get { return sqlserver; }
            set { sqlserver = value; }
        }

        private string sqlite;

        [DisplayName("Sqlite")]
        public string Sqlite
        {
            get { return sqlite; }
            set { sqlite = value; }
        }

        private string access;

        [DisplayName("Access")]
        public string Access
        {
            get { return access; }
            set { access = value; }
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
