using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace JCodes.Framework.Entity
{
    /// <summary>
    /// 用来传递常规操作的结果内容 TODO 作废
    /// </summary>
    [DataContract]
    [Serializable]
    public class CommonResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        [DataMember]
        public bool Success
        {
            get;
            set;
        }

        /// <summary>
        /// 错误号
        /// </summary>
        [DataMember]
        public string ErrorNo
        {
            get;
            set;
        }

        /// <summary>
        /// 如果不成功，返回的错误信息
        /// </summary>
        [DataMember]
        public string ErrorMessage
        {
            get;
            set;
        }

        /// <summary>
        /// 用来传递的内容
        /// </summary>
        [DataMember]
        public string Data1
        {
            get;
            set;
        }

        /// <summary>
        /// 用来传递的内容
        /// </summary>
        [DataMember]
        public string Data2
        {
            get;
            set;
        }

        /// <summary>
        /// 用来传递的内容
        /// </summary>
        [DataMember]
        public string Data3
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 返回结果对象
    /// </summary>
    [DataContract]
    [Serializable]
    public class ReturnResult {
        /// <summary>
        /// 错误码 0表示成功 1表示错误具体错误信息查看错误信息表
        /// </summary>
        [DataMember]
        public Int32 ErrorCode
        {
            get;
            set;
        }

        /// <summary>
        /// 如果不成功，返回的错误信息
        /// </summary>
        [DataMember]
        public string ErrorMessage
        {
            get;
            set;
        }

        /// <summary>
        /// 路由信息
        /// </summary>
        [DataMember]
        public string ErrorPath
        {
            get;
            set;
        }
    }
}
