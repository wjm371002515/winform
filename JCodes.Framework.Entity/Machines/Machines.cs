using System;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace JCodes.Framework.Entity
{
    /// <summary>
    /// 服务器列表
    /// </summary>
    [DataContract]
    public class MachinesInfo : BaseEntity
    {
        #region 字段
        [DataMember]
        public virtual Int32 ID {get;set;}

        /// <summary>
        /// 管理人
        /// </summary>
        public virtual string GLY{ get; set;}

        /// <summary>
        /// 过保日期
        /// </summary>
        public virtual string GBRQ {get;set;}

        /// <summary>
        /// 公网端口
        /// </summary>
        public virtual string GWFWDK { get; set; }
        /// <summary>
        /// 公网IP
        /// </summary>
        public virtual string GWIP { get; set; }
        /// <summary>
        /// 机柜位置
        /// </summary>
        public virtual string JGWZ { get; set; }
        /// <summary>
        /// 机器用途
        /// </summary>
        public virtual string JQRT { get; set; }
        /// <summary>
        /// 机器型号
        /// </summary>
        public virtual string JQXH { get; set; }
        /// <summary>
        /// 内网开放端口
        /// </summary>
        public virtual string NWFWDK { get; set; }
        /// <summary>
        /// 内网IP
        /// </summary>
        public virtual string NWIP { get; set; }
        /// <summary>
        /// 表格类型
        /// </summary>
        public virtual string TABTYPE { get; set; }
        /// <summary>
        /// 数据来源
        /// </summary>
        public virtual string WJLY { get; set; }
        /// <summary>
        /// 外网开放端口
        /// </summary>
        public virtual string WWFWDK { get; set; }
        /// <summary>
        /// 外网IP
        /// </summary>
        public virtual string WWIP { get; set; }
        /// <summary>
        /// 系统版本
        /// </summary>
        public virtual string XTBB { get; set; }
        /// <summary>
        /// 硬件序列号
        /// </summary>
        public virtual string YJXLH { get; set; }
        /// <summary>
        /// 应用版本
        /// </summary>
        public virtual string YYBB { get; set; }
        /// <summary>
        /// 主管
        /// </summary>
        public virtual string ZG { get; set; }
        /// <summary>
        /// 修改备注 
        /// </summary>
        public virtual string MODIFYMARK { get; set; }
     

        #endregion

    }
}