using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JCodes.Framework.Data.Model
{
    public class Order
    {
        public char type { get; set; }
        public string shopname { get; set; }
        public string in_company { get; set; }
        public string in_orderid { get; set; }
        public string id { get; set; }
        public string in_color { get; set; }
        public string in_size { get; set; }
        public int in_num { get; set; }
        public string aliwangwang { get; set; }
        public string out_company { get; set; }
        public string out_orderid { get; set; }
        public string out_id { get; set; }
        public string out_color { get; set; }
        public string out_size { get; set; }
        public int out_num { get; set; }
        public string remark { get; set; }
        public DateTime create_time { get; set; }
        public DateTime update_time { get; set; }
        public string username { get; set; }
    }
}
