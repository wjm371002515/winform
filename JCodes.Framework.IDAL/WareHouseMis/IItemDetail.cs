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
	/// IItemDetail 的摘要说明。
	/// </summary>
    public interface IItemDetail : IBaseDAL<ItemDetailInfo>
    {
        /// <summary>
        /// 根据备件属类获取该类型的备件列表
        /// </summary>
        /// <param name="bigType">备件属类</param>
        /// <returns></returns>
        List<ItemDetailInfo> FindByBigType(string bigType);

        /// <summary>
        /// 根据备件类型获取该类型的备件列表
        /// </summary>
        /// <param name="itemType">备件类型</param>
        /// <returns></returns>
        List<ItemDetailInfo> FindByItemType(string itemType, string wareHouse);

        /// <summary>
        /// 根据备件名称获取列表
        /// </summary>
        /// <param name="goodsType">备件类型</param>
        /// <returns></returns>
        List<ItemDetailInfo> FindByName(string itemName);
                        
        /// <summary>
        /// 根据备件编码获取列表
        /// </summary>
        /// <param name="itemNo">备件类型</param>
        /// <returns></returns>
        ItemDetailInfo FindByItemNo(string itemNo);

        /// <summary>
        /// 根据备件名称和备件编号获取列表
        /// </summary>
        /// <param name="itemName">备件名称</param>
        /// <param name="itemNo">备件编码</param>
        /// <returns></returns>
        List<ItemDetailInfo> FindByNameAndNo(string itemName, string itemNo, string wareHouse);

    }
}