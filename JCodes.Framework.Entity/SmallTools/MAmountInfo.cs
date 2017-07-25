using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCodes.Framework.Entity
{
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
}
