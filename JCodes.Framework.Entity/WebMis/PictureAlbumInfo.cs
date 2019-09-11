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
            this.Id=0;
		}

        #region Property Members
        
		[DataMember]
        public virtual Int32 Id { get; set; }

        /// <summary>
        /// 父ID
        /// </summary>
		[DataMember]
        public virtual Int32 Pid { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
		[DataMember]
        public virtual string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
		[DataMember]
        public virtual string Remark { get; set; }

        /// <summary>
        /// 编辑人
        /// </summary>
		[DataMember]
        public virtual Int32 EditorId { get; set; }

        /// <summary>
        /// 编辑时间
        /// </summary>
		[DataMember]
        public virtual DateTime LastUpdateTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
		[DataMember]
        public virtual Int32 CreatorId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
		[DataMember]
        public virtual DateTime CreatorTime { get; set; }


        #endregion

    }
}