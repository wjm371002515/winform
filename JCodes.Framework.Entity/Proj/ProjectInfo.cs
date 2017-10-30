using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace JCodes.Framework.Entity
{
    public class ProjectInfo
    {
        public ProjectInfo()
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

        [DisplayName("名称")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string version;

        [DisplayName("版本号")]
        public string Version
        {
            get { return version; }
            set { version = value; }
        }

        private string contract;

        [DisplayName("联系方式")]
        public string Contract
        {
            get { return contract; }
            set { contract = value; }
        }

        private string remark;

        [DisplayName("说明")]
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }

        private string dbtype;

        [DisplayName("数据库类型")]
        public string DbType
        {
            get { return dbtype; }
            set { dbtype = value; }
        }

        private string dicttype_table;

        [DisplayName("字典大类信息")]
        public string DicttypeTable
        {
            get { return dicttype_table; }
            set { dicttype_table = value; }
        }

        private string dictdata_table;

        [DisplayName("字典明细信息")]
        public string DictdataTable
        {
            get { return dictdata_table; }
            set { dictdata_table = value; }
        }

        private string err_table;

        [DisplayName("错误号信息表")]
        public string ErrTable
        {
            get { return err_table; }
            set { err_table = value; }
        }

        private string lasttime;

        [DisplayName("最后更新日期")]
        public string LastTime
        {
            get { return lasttime; }
            set { lasttime = value; }
        }

        private string outputpath;

        [DisplayName("脚本生成路径")]
        public string OutputPath
        {
            get { return outputpath; }
            set { outputpath = value; }
        }
    }
}
