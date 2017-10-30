using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace JCodes.Framework.Entity
{
    public class ModRecordInfo : IComparable<ModRecordInfo>
    {
        public ModRecordInfo()
        { }

        private string guid;

        [DisplayName("GUID")]
        public string GUID
        {
            get { return guid; }
            set { guid = value; }
        }

        private DateTime modDate;

        [DisplayName("修改日期")]
        public DateTime ModDate
        {
            get { return modDate; }
            set { modDate = value; }
        }

        private string modVersion;

        [DisplayName("修改版本")]
        public string ModVersion
        {
            get { return modVersion; }
            set { modVersion = value; }
        }

        private string modOrderId;

        [DisplayName("修改单号")]
        public string ModOrderId
        {
            get { return modOrderId; }
            set { modOrderId = value; }
        }

        private string proposer;

        [DisplayName("申请人")]
        public string Proposer
        {
            get { return proposer; }
            set { proposer = value; }
        }

        private string programmer;

        [DisplayName("修改人")]
        public string Programmer
        {
            get { return programmer; }
            set { programmer = value; }
        }

        private string modContent;

        [DisplayName("修改内容")]
        public string ModContent
        {
            get { return modContent; }
            set { modContent = value; }
        }

        private string modReason;

        [DisplayName("修改原因")]
        public string ModReason
        {
            get { return modReason; }
            set { modReason = value; }
        }

        private string remark;

        [DisplayName("备注")]
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }

        /// <summary>
        /// Compares to.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns>System.Int32.</returns>
        public int CompareTo(ModRecordInfo other)
        {
            if (other == null) return -1;
            if (ModDate > other.ModDate)
            {
                return -1;
            }
            return 1;
        }
    }
}
