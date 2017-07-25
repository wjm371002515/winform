using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;
using JCodes.Framework.Common.Framework;

namespace JCodes.Framework.IDAL
{
	/// <summary>
	/// IStock 的摘要说明。
	/// </summary>
	public interface IStock : IBaseDAL<StockInfo>
	{
        /// <summary>
        /// 初始化库房信息
        /// </summary>
        /// <param name="detailInfo">备件详细信息</param>
        /// <param name="quantity">期初数量</param>
        /// <param name="wareHouse">库房名称</param>
        /// <returns></returns>
        bool InitStockQuantity(ItemDetailInfo detailInfo, int quantity, string wareHouse);

        /// <summary>
        /// 增加库存
        /// </summary>
        /// <param name="ItemNo">备件编号</param>
        /// <param name="itemName">备件名称</param>
        /// <param name="quantity">库存属类</param>
        /// <param name="wareHouse">库房名称</param>
        /// <returns></returns>
        bool AddStockQuantiy(string ItemNo, string itemName, int quantity, string wareHouse);

        /// <summary>
        /// 增加库存
        /// </summary>
        /// <param name="ItemNo">备件编号</param>
        /// <param name="itemName">备件名称</param>
        /// <param name="quantity">库存属类</param>
        /// <param name="wareHouse">库房名称</param>
        /// <returns></returns>
        bool AddStockQuantiy(string ItemNo, string itemName, int quantity, string wareHouse, DbTransaction trans);
                        
        /// <summary>
        /// 获取备件名称的库存数量列表
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        DataTable GetItemStockQuantityReport(string condition, string fieldName);

        DataTable GetCurrentStockReport(string condition);

        int GetCurrentStockReportCount(string condition);
    }
}