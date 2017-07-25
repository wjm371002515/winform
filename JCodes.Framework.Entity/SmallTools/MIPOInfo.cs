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
    public class MIPOInfo{
        /// <summary>
        /// 委托序号
        /// </summary>
        private string _wtxh;

        /// <summary>
        /// 委托序号
        /// </summary>
        public string WTXH
        {
            get { return _wtxh; }
            set { _wtxh = value; }
        }

        /// <summary>
        /// 发行流水号
        /// </summary>
        private string _fxlsh;

        /// <summary>
        /// 发行流水号
        /// </summary>
        public string FXLSH
        {
            get { return _fxlsh; }
            set { _fxlsh = value; }
        }

        /// <summary>
        /// 询价对象名称
        /// </summary>
        private string _xjdxmc;

        /// <summary>
        /// 询价对象名称
        /// </summary>
        public string XJDXMC
        {
            get { return _xjdxmc; }
            set { _xjdxmc = value; }
        }

        /// <summary>
        /// 配售对象ID
        /// </summary>
        private string _psdxID;

        /// <summary>
        /// 配售对象ID
        /// </summary>
        public string PSDXID
        {
            get { return _psdxID; }
            set { _psdxID = value; }
        }

        /// <summary>
        /// 配售对象名称
        /// </summary>
        private string _psdxmc;

        /// <summary>
        /// 配售对象名称
        /// </summary>
        public string PSDXMC
        {
            get { return _psdxmc; }
            set { _psdxmc = value; }
        }

        /// <summary>
        /// 配售对象类型
        /// </summary>
        private string _psdxlx;

        /// <summary>
        /// 配售对象类型
        /// </summary>
        public string PSDXLX
        {
            get { return _psdxlx; }
            set { _psdxlx = value; }
        }

        /// <summary>
        /// 证券代码
        /// </summary>
        private string _zqdm;

        /// <summary>
        /// 证券代码
        /// </summary>
        public string ZQDM
        {
            get { return _zqdm; }
            set { _zqdm = value; }
        }

        /// <summary>
        /// 申购数量
        /// </summary>
        private string _sgsl;

        /// <summary>
        /// 申购数量
        /// </summary>
        public string SGSL
        {
            get { return _sgsl; }
            set { _sgsl = value; }
        }

        /// <summary>
        /// 申购价格
        /// </summary>
        private string _sgjg;

        /// <summary>
        /// 申购价格
        /// </summary>
        public string SGJG
        {
            get { return _sgjg; }
            set { _sgjg = value; }
        }

        /// <summary>
        /// 发行价格
        /// </summary>
        private string _fxjg;

        /// <summary>
        /// 发行价格
        /// </summary>
        public string FXJG
        {
            get { return _fxjg; }
            set { _fxjg = value; }
        }

        /// <summary>
        /// 无内容
        /// </summary>
        private string _remark1;

        /// <summary>
        /// 无内容
        /// </summary>
        public string REMARK1
        {
            get { return _remark1; }
            set { _remark1 = value; }
        }

        /// <summary>
        /// 无内容2
        /// </summary>
        private string _remark2;

        /// <summary>
        /// 无内容2
        /// </summary>
        public string REMARK2
        {
            get { return _remark2; }
            set { _remark2 = value; }
        }
    }
}
