using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace JCodes.Framework.Entity
{
    public class TablesTypeInfo
    {
        public TablesTypeInfo()
        { }

        private string guid;

        [DisplayName("GUID")]
        public string GUID
        {
            get { return guid; }
            set { guid = value; }
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        private string createdate;

        [DisplayName("创建时间")]
        public string CreateDate
        {
            get { return createdate; }
            set { createdate = value; }
        }

        private string name;

        [DisplayName("分类名")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string basicdata;

        [DisplayName("是否基础数据")]
        public string BasicData
        {
            get { return basicdata; }
            set { basicdata = value; }
        }
    }
}
