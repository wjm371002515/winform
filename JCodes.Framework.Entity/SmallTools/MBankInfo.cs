using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCodes.Framework.Entity
{
    /// <summary>
    /// 银行信息
    /// </summary>
    public class MBankInfo {
        private Int32 _Id;

        /// <summary>
        /// 序号
        /// </summary>
        public Int32 Id
        {
            set { _Id = value; }
            get { return _Id; }
        }

        private string _bankName;

        /// <summary>
        /// 退款汇入行全称
        /// </summary>
        public string BankName
        {
            set { _bankName = value; }
            get { return _bankName; }
        }

        private string _clientName;

        /// <summary>
        /// 退款收款人全称
        /// </summary>
        public string ClientName
        {
            set { _clientName = value; }
            get { return _clientName; }
        }

        private string _bankaccount;

        /// <summary>
        /// 退款收款人账号
        /// </summary>
        public string BankAccount
        {
            set { _bankaccount = value; }
            get { return _bankaccount; }
        }

        private string _systemId;

        /// <summary>
        /// 大额支付系统号
        /// </summary>
        public string SystemId
        {
            set { _systemId = value; }
            get { return _systemId; }
        }

        private string _bankprovince;

        /// <summary>
        /// 退款汇入行省份
        /// </summary>
        public string BankProvince
        {
            set { _bankprovince = value; }
            get { return _bankprovince; }
        }

        private string _bankcity;

        /// <summary>
        /// 退款汇入行地市
        /// </summary>
        public string BankCity
        {
            set { _bankcity = value; }
            get { return _bankcity; }
        }				
    }


}
