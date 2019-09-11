using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCodes.Framework.TestWinForm.ZSZQ_IPO
{
    public class DownloadEmail
    {
        public DateTime ReceiveTime{ get; set; }
        public String UID { get; set; }
    }

    public class ReceiveInfos {
        public string EmailTitle { get; set; }
        public DateTime EmailReceive { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Shenfenzheng { get; set; }
    }

    public class PurchaseIPOInfo
    {
        // 1 序号（一个机构一个编码）
        public Int32 Id { get; set; }
        // 2 机构全称	
        public string OrgName { get; set; }
        // 3 经办人姓名	
        public string UserName { get; set; }
        // 4 办公电话	
        public string Telephone { get; set; }
        // 5 手机	
        public string iPhone { get; set; }
        // 6 邮箱	
        public string EMail { get; set; }
        // 7 证券账户户名（上海）	
        public string StockholderName { get; set; }
        // 8 证券账户代码（上海）	
        public string StockholderCode { get; set; }
        // 9 托管席位号	
        public string Seat { get; set; }
        // 10 身份证明号码（如营业执照注册号等）
        public string UserID { get; set; }
        // 11 票面利率	
        public string Rate { get; set; }
        // 12 申购金额	
        public string Money { get; set; }
        // 13 退款汇入行全称	
        public string RefundBank { get; set; }
        // 14 退款收款人全称	
        public string RefundName { get; set; }
        // 15 退款收款人账号	
        public string RefundCard { get; set; }
        // 16 大额支付系统号	
        public string RefundSystemNo { get; set; }
        // 17 退款汇入行省份地市
        public string RefundProvince { get; set; }
    
    }
}
