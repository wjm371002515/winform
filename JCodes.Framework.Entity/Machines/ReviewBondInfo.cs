using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
    /// 正在审核债券项目（完成）
	/// </summary>
	[Serializable]
	[DataContract]
    public partial class ReviewBondInfo : BaseEntity
	{
        /*
            过会（或通过发改委、交易所审核）的项目名称	
            核准总额（亿）	
            批文剩余额度（亿）	
            浙商证券预估份额（亿元）	
            批文剩余时间（天）	
            区域	
            项目类型	
            项目负责人	
            投行部门	
            批文日期	
            批文过期时间	
            今天日期	
            申报时间	
            发行进度	
            余额包销/代销
            来源
            备注
         */
        #region Field Members

        /// <summary>
        /// ID序号
        /// </summary>
        public Int32 Id = 0;

        /// <summary>
        /// 过会（或通过发改委、交易所审核）的项目名称	
        /// </summary>
        public String ProjectName = string.Empty;

        /// <summary>
        /// 核准总额（亿）	
        /// </summary>
        public String IssueMoney = string.Empty;

        /// <summary>
        /// 批文剩余额度（亿）
        /// </summary>
        public String LeftMoney = string.Empty;

        /// <summary>
        /// 浙商证券预估份额（亿元）
        /// </summary>
        public String EstimateAmount = string.Empty;

        /// <summary>
        /// 区域
        /// </summary>
        public String Area = string.Empty;

        /// <summary>
        /// 项目类型
        /// </summary>
        public String ProjectType = string.Empty;

        /// <summary>
        /// 项目负责人
        /// </summary>
        public String ProjectLeader = string.Empty;

        /// <summary>
        /// 投行部门
        /// </summary>
        public String DeptName = string.Empty;

        /// <summary>
        /// 批文日期
        /// </summary>
        public String ApprovalDate = string.Empty;

        /// <summary>
        /// 批文过期时间
        /// </summary>
        public String ApprovalPassDate = string.Empty;

        /// <summary>
        /// 今天日期
        /// </summary>
        public String TodayDate = string.Empty;

         /// <summary>
        /// 申报时间
        /// </summary>
        public String DeclareDate = string.Empty;

         /// <summary>
        /// 发行进度
        /// </summary>
        public String PublishType = string.Empty;

         /// <summary>
        /// 余额包销/代销
        /// </summary>
        public String ConsignmentType = string.Empty;

         /// <summary>
        /// 来源
        /// </summary>
        public String From = string.Empty;

         /// <summary>
        /// 备注
        /// </summary>
        public String Remark = string.Empty;
        #endregion
    }
}