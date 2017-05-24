using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCodes.Framework.Entity
{
    /// <summary>
    /// 文件信息类
    /// </summary>
    public class MFileInfo
    {
        private string _fileName; 

        // 文件名
        public string FileName 
        {
            set { _fileName = value; }
            get { return _fileName; }
        }

        private Int32 _dealStatus;
        /// <summary>
        /// 处理状态
        /// 0 - 未处理
        /// 1 - 正在处理
        /// 2 - 处理完成
        /// 3 - 处理错误
        /// </summary>
        public Int32 DealStatus
        {
            set { _dealStatus = value; }
            get { return _dealStatus; }
        }
    }

    /// <summary>
    /// 客户基本信息
    /// </summary>
    public class MClientInfo{
        private Int32 _Id;

        /// <summary>
        /// 序号
        /// </summary>
        public Int32 Id{
            set { _Id = value; }
            get { return _Id; }
        }

        private string _organizename;

        /// <summary>
        /// 机构名或姓名
        /// </summary>
        public string OrganizeName
        {
            set { _organizename = value; }
            get { return _organizename; }
        }

        private string _accountName;

        /// <summary>
        /// 证券账户户名（上海）
        /// </summary>
        public string AccountName{
            set { _accountName = value; }
            get { return _accountName; }
        }

        private string _accountCode;

        /// <summary>
        /// 证券账户代码（上海）
        /// </summary>
        public string AccountCode{
            set { _accountCode = value; }
            get { return _accountCode; }
        }

        private string _seat;

        /// <summary>
        /// 托管席位号
        /// </summary>
        public string Seat{
            set { _seat = value; }
            get { return _seat; }
        }

        private string _cardId;

        /// <summary>
        /// 身份证
        /// </summary>
        public string CardId{
            set { _cardId = value; }
            get { return _cardId; }
        }
    }

    /// <summary>
    /// 开户金额信息
    /// </summary>
    public class MAmountInfo{
        private Int32 _Id;

        /// <summary>
        /// 序号
        /// </summary>
        public Int32 Id{
            set { _Id = value; }
            get { return _Id; }
        }

        private double _rate;

        /// <summary>
        /// 票面利率
        /// </summary>
        public double Rate{
            set { _rate = value; }
            get { return _rate; }
        }

        private double _balance;

         /// <summary>
        /// 申购金额
        /// </summary>
        public double Balance{
            set { _balance = value; }
            get { return _balance; }
        }
    }

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
