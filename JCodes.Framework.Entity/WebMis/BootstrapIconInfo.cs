using System;
using System.Runtime.Serialization;

namespace JCodes.Framework.Entity
{
    /// <summary>
    /// 基于BootStrap的图标
    /// </summary>
    [DataContract]
    public class BootstrapIconInfo : BaseEntity
    {
        /// <summary>
        /// 默认构造函数（需要初始化属性的在此处理）
        /// </summary>
        public BootstrapIconInfo()
        {
            this.ID = System.Guid.NewGuid().ToString();
            this.CreateTime = DateTime.Now;
        }

        #region Property Members

        [DataMember]
        public virtual string ID { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        [DataMember]
        public virtual string DisplayName { get; set; }

        /// <summary>
        /// 样式名称
        /// </summary>
        [DataMember]
        public virtual string ClassName { get; set; }

        /// <summary>
        /// 来源
        /// </summary>
        [DataMember]
        public virtual string SourceType { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DataMember]
        public virtual DateTime CreateTime { get; set; }


        #endregion

    }
}