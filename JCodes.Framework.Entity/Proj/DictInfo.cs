using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace JCodes.Framework.Entity
{
    public class DictInfo : IComparable<DictInfo>
    {
        public DictInfo()
        { }
        
        /// <summary>
        /// 类型名
        /// </summary>
        private Int32 id;

        [DisplayName("编号")]
        public Int32 Id
        {
            get { return id; }
            set { id = value; }
        }

        private Int32 pid;

        [DisplayName("父节点编号")]
        public Int32 Pid
        {
            get { return pid; }
            set { pid = value; }
        }

        private string name;

        [DisplayName("名字")]
        public string Name
        {
            get { return name; }
            set { name = value; }
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
        public int CompareTo(DictInfo other)
        {
            if (other == null) return -1;
            if (Id > other.Id)
            {
                return 1;
            }
            return -1;
        }
    }

    public class DictDetailInfo : IComparable<DictDetailInfo>
    {

        public DictDetailInfo()
        { }

        private Int32 _value;

        [DisplayName("值")]
        public Int32 Value
        {
            get { return _value; }
            set { _value = value; }
        }

        private string name;

        [DisplayName("名称")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string seq;

        [DisplayName("排序")]
        public string Seq
        {
            get { return seq; }
            set { seq = value; }
        }

        private string remark;

        [DisplayName("名称")]
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
        public int CompareTo(DictDetailInfo other)
        {
            return Seq.CompareTo(other.Seq);
        }
    }
}
