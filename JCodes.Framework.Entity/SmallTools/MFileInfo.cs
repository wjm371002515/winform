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
}
