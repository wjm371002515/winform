using System;
using JCodes.Framework.Enum;

namespace JCodes.Framework.Entity
{
    public class TMenu
    {
        /// <summary>
        /// id序号
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }

        public int pid { get; set; }

        public int sort { get; set; }

        public string url { get; set; }

        public Dic000000 hide { get; set; }

        public string tip { get; set; }

        public Dic000000 is_show { get; set; }

        public string menu_group { get; set; }

        public Dic000000 is_dev { get; set; }

        public Dic000002 status { get; set; }
    }
}
