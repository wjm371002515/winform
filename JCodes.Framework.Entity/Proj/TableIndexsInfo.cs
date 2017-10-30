using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace JCodes.Framework.Entity
{
    public class TableIndexsInfo
    {
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

        private string unique;

        [DisplayName("唯一")]
        public string Unique
        {
            get { return unique; }
            set { unique = value; }
        }

        private string primary;

        [DisplayName("主键")]
        public string Primary
        {
            get { return primary; }
            set { primary = value; }
        }

        private string cluster;

        [DisplayName("聚合")]
        public string Cluster
        {
            get { return cluster; }
            set { cluster = value; }
        }
    }
}
