using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JCodes.Framework.Entity.SmallTools
{
    /// <summary>
    /// 浙商证券EB 实体类（第二版）
    /// </summary>
    public class MEBInfo
    {
        // 机构名或姓名
        private string _organizeName;
        /// <summary>
        /// 机构名或姓名
        /// </summary>
        public string OrganizeName
        {
            set { _organizeName = value; }
            get { return _organizeName; }
        }


        // 证券账户户名（上海）
        private string _accountName;
        /// <summary>
        /// 证券账户户名（上海）
        /// </summary>
        public string AccountName
        {
            set { _accountName = value; }
            get { return _accountName; }
        }

        // 证券账户代码（上海）
        private string _accountCode;
        /// <summary>
        /// 证券账户代码（上海）
        /// </summary>
        public string AccountCode
        {
            set { _accountCode = value; }
            get { return _accountCode; }
        }

        // 托管席位号
        private string _seat;
        /// <summary>
        /// 托管席位号
        /// </summary>
        public string Seat
        {
            set { _seat = value; }
            get { return _seat; }
        }

        // 身份证明号码（如营业执照注册号等）
        private string _cardId;
        /// <summary>
        /// 身份证明号码（如营业执照注册号等）
        /// </summary>
        public string CardId
        {
            set { _cardId = value; }
            get { return _cardId; }
        }
        
        // 到期赎回价格
        private string _rate;
        /// <summary>
        /// 到期赎回价格
        /// </summary>
        public string Rate
        {
            set { _rate = value; }
            get { return _rate; }
        }

        // 申购金额
        private string _balance;
        /// <summary>
        /// 申购金额
        /// </summary>
        public string Balance
        {
            set { _balance = value; }
            get { return _balance; }
        }

        // 退款汇入行全称
        private string _bankName;
        /// <summary>
        /// 退款汇入行全称
        /// </summary>
        public string BankName
        {
            set { _bankName = value; }
            get { return _bankName; }
        }

        // 退款收款人全称
        private string _clientName;
        /// <summary>
        /// 退款收款人全称
        /// </summary>
        public string ClientName
        {
            set { _clientName = value; }
            get { return _clientName; }
        }

        // 退款收款人账号
        private string _bankAccount;
        /// <summary>
        /// 退款收款人账号
        /// </summary>
        public string BankAccount
        {
            set { _bankAccount = value; }
            get { return _bankAccount; }
        }

        // 大额支付系统号
        private string _systemId;
        /// <summary>
        /// 大额支付系统号
        /// </summary>
        public string SystemId
        {
            set { _systemId = value; }
            get { return _systemId; }
        }

        // 退款汇入行省份地市
        private string _bankProvince;
        /// <summary>
        /// 退款汇入行省份地市
        /// </summary>
        public string BankProvince
        {
            set { _bankProvince = value; }
            get { return _bankProvince; }
        }

    }
}
