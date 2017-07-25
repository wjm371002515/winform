using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCodes.Framework.Entity
{
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
}
