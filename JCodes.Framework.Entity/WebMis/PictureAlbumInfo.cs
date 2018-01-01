using System;
using System.Runtime.Serialization;

namespace JCodes.Framework.Entity
{
    /// <summary>
    /// 图片相册
    /// </summary>
    [DataContract]
    public class PictureAlbumInfo : BaseEntity
    { 
        /// <summary>
        /// 默认构造函数（需要初始化属性的在此处理）
        /// </summary>
	    public PictureAlbumInfo()
		{
            this.ID= System.Guid.NewGuid().ToString();
         
		}

        #region Property Members
        
		[DataMember]
        public virtual string ID { get; set; }

        /// <summary>
        /// 父ID
        /// </summary>
		[DataMember]
        public virtual string PID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
		[DataMember]
        public virtual string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
		[DataMember]
        public virtual string Note { get; set; }

        /// <summary>
        /// 编辑人
        /// </summary>
		[DataMember]
        public virtual string Editor { get; set; }

        /// <summary>
        /// 编辑时间
        /// </summary>
		[DataMember]
        public virtual DateTime EditTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
		[DataMember]
        public virtual string Creator { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
		[DataMember]
        public virtual DateTime CreateTime { get; set; }


        #endregion

    }
}