using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace JCodes.Framework.Entity
{
    public class TablesInfo
    {
        public TablesInfo()
        { }

        private string guid;

        [DisplayName("GUID")]
        public string GUID
        {
            get { return guid; }
            set { guid = value; }
        }

        private string name;

        [DisplayName("表名")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string chineseName;

        [DisplayName("中文表名")]
        public string ChineseName
        {
            get { return chineseName; }
            set { chineseName = value; }
        }

        private string functionId;

        [DisplayName("功能号")]
        public string FunctionId
        {
            get { return functionId; }
            set { functionId = value; }
        }

        private string typeguid;

        [DisplayName("类型GUID")]
        public string TypeGuid
        {
            get { return typeguid; }
            set { typeguid = value; }
        }

        private string path;

        [DisplayName("路径")]
        public string Path
        {
            get { return path; }
            set { path = value; }
        }
    }
}
