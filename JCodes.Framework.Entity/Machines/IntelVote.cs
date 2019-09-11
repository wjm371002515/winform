using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace JCodes.Framework.Entity.Machines
{
    /// <summary>
    /// 投票信息类
    /// </summary>
    [DataContract]
    public class IntelVoteInfo : BaseEntity
    {
        // {"fanghao":"0202","util":"1","zhuang":"1","yuan":"钱塘东南家园1幢"},
         
        #region 字段
        [DataMember]
        public virtual string xiaoquName { get; set; }
        public virtual Int32 intelVote { get; set; }
        public virtual string houseNum { get; set; }
        public virtual string percentage { get; set; }

        public virtual string detailurl { get; set; }
        #endregion
    }

    public class IntelVoteDetailInfo : BaseEntity
    {
        #region 字段
        [DataMember]
        public virtual string houseNum { get; set; }
        public virtual string toupiao { get; set; }
        #endregion
    }

    public class DongnanhaiVotesInfo : BaseEntity { 
        #region 字段
        /// <summary>
        /// 房号
        /// </summary>
        public virtual string Fanghao { get; set; }
        /// <summary>
        /// 单元
        /// </summary>
        public virtual string Util { get; set; }
        /// <summary>
        /// 幢
        /// </summary>
        public virtual string Zhuang { get; set; }
        /// <summary>
        /// 住宅/商铺
        /// </summary>
        public virtual string Yuan { get; set; }
        /// <summary>
        /// 层
        /// </summary>
        public virtual Int32 Ceng { get; set;}

        /// <summary>
        /// 投票情况
        /// 0 未投
        /// 1.已投
        /// </summary>
        public virtual Int32 Flag { get; set;}
        #endregion
    }
}
