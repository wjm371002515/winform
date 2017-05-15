using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;

namespace JCodes.Framework.IDAL
{
	/// <summary>
	/// IPurchaseHeader 的摘要说明。
	/// </summary>
    public interface IPurchaseHeader : IBaseDAL<PurchaseHeaderInfo>
    {
        /// <summary>
        /// 获取日期字段的年份列表（不重复）
        /// </summary>
        /// <param name="fieldName">日期字段</param>
        /// <returns></returns>
        List<string> GetYearList(string fieldName);
                       
        /// <summary>
        /// 获取指定条件的入库、出库数量
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="isIn">入库还是出库，true为入库，否则为出库</param>
        /// <returns></returns>
        int GetPurchaseQuantity(string condition, bool isIn);
    }
}