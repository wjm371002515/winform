using System;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 框架实体类的基类
	/// </summary>
    [DataContract]
    public class BaseEntity
    {
        /// <summary>
        /// 当前登录用户ID。该字段不保存到数据表中，只用于记录用户的操作日志。
        /// </summary>
        [DataMember]
        public string CurrentLoginUserId { get; set; }

        #region 在实体类存储一些特殊的数据
        /// <summary>
        /// 用来给实体类传递一些额外的数据，如外键的转义等，该字段不保存到数据表中
        /// </summary>
        [DataMember]
        public string Data1 { get; set; }

        /// <summary>
        /// 用来给实体类传递一些额外的数据，如外键的转义等，该字段不保存到数据表中
        /// </summary>
        [DataMember]
        public string Data2 { get; set; }

        /// <summary>
        /// 用来给实体类传递一些额外的数据，如外键的转义等，该字段不保存到数据表中
        /// </summary>
        [DataMember]
        public string Data3 { get; set; } 
        #endregion
    }
}
