using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;
using JCodes.Framework.IDAL;
using JCodes.Framework.Common.Framework;

namespace JCodes.Framework.BLL
{
	public class ItemDetail : BaseBLL<ItemDetailInfo>
    {
        public ItemDetail() : base()
        {
            base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
        }

        /// <summary>
        /// 根据备件属类获取该类型的备件列表
        /// </summary>
        /// <param name="bigType">备件属类</param>
        /// <returns></returns>
        public List<ItemDetailInfo> FindByBigType(string bigType)
        {
            IItemDetail dal = baseDal as IItemDetail;
            return dal.FindByBigType(bigType);
        }

        /// <summary>
        /// 根据备件类型获取该类型的备件列表
        /// </summary>
        /// <param name="itemType">备件类型</param>
        /// <returns></returns>
        public List<ItemDetailInfo> FindByItemType(string itemType, string wareHouse)
        {
            IItemDetail dal = baseDal as IItemDetail;
            return dal.FindByItemType(itemType, wareHouse);
        }

        /// <summary>
        /// 根据备件名称获取列表
        /// </summary>
        /// <param name="itemName">备件类型</param>
        /// <returns></returns>
        public List<ItemDetailInfo> FindByName(string itemName)
        {
            IItemDetail dal = baseDal as IItemDetail;
            return dal.FindByName(itemName);
        }

        /// <summary>
        /// 根据备件编码获取列表
        /// </summary>
        /// <param name="itemNo">备件类型</param>
        /// <returns></returns>
        public ItemDetailInfo FindByItemNo(string itemNo)
        {
            IItemDetail dal = baseDal as IItemDetail;
            return dal.FindByItemNo(itemNo);
        }    
             
        /// <summary>
        /// 根据备件名称和备件编号获取列表
        /// </summary>
        /// <param name="itemName">备件名称</param>
        /// <param name="itemNo">备件编码</param>
        /// <returns></returns>
        public List<ItemDetailInfo> FindByNameAndNo(string itemName, string itemNo, string wareHouse)
        {
            IItemDetail dal = baseDal as IItemDetail;
            return dal.FindByNameAndNo(itemName, itemNo, wareHouse);
        }

        /// <summary>
        /// 检查是否存在项目编码项目（排除ID本身的）
        /// </summary>
        /// <param name="itemNo"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool CheckExist(string itemNo, Int32 Id)
        {
            string condition = string.Format("ItemNo ='{0}' and ID <> {1} ", itemNo, Id);
            return IsExistRecord(condition);
        }

        /// <summary>
        /// 获取指定仓库的备件列表
        /// </summary>
        /// <param name="wareHouse"></param>
        /// <returns></returns>
        public List<ItemDetailInfo> GetAllByWareHouse(string wareHouse)
        {
            string condition = string.Format("WareHouse ='{0}' ", wareHouse);
            return baseDal.Find(condition);
        }

    }
}
