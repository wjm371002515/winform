using System;
using System.Data.Common;
using System.Collections.Generic;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Entity;
using JCodes.Framework.IDAL;
using JCodes.Framework.Common.Office;
using JCodes.Framework.Common.Extension;
using JCodes.Framework.jCodesenum;

namespace JCodes.Framework.BLL
{
    /// <summary>
    /// 客户联系人
    /// </summary>
	public class Contact : BaseBLL<ContactInfo>
    {
        public Contact() : base()
        {
            base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
            baseDal.OnOperationLog += new OperationLogEventHandler(OperationLog.OnOperationLog);//如果需要记录操作日志，则实现这个事件
        }

        /// <summary>
        /// 根据联系人分组的名称，搜索属于该分组的联系人列表
        /// </summary>
        /// <param name="ownerUser">联系人所属用户</param>
        /// <param name="groupName">联系人分组的名称,如果联系人分组为空，那么返回未分组联系人列表</param>
        /// <param name="pagerInfo">分页条件</param>
        /// <returns></returns>
        public List<ContactInfo> FindByGroupName(string ownerUser, string groupName, PagerInfo pagerInfo = null)
        {
            IContact dal = baseDal as IContact;
            return dal.FindByGroupName(ownerUser, groupName, pagerInfo);
        }

        /// <summary>
        /// 设置删除标志
        /// </summary>
        /// <param name="id">联系人ID</param>
        /// <param name="deleted">是否删除</param>
        /// <param name="trans">事务对象</param>
        /// <returns></returns>
        public bool SetDeletedFlag(Int32 id, IsDelete isDelete = IsDelete.是, DbTransaction trans = null)
        {
            IContact dal = baseDal as IContact;
            return dal.SetDeletedFlag(id, isDelete, trans);
        }

        /// <summary>
        /// 获取联系人的名称
        /// </summary>
        /// <param name="id">联系人ID</param>
        /// <param name="trans">事务对象</param>
        /// <returns></returns>
        public string GetContactName(string id, DbTransaction trans = null)
        {
            //使用缓存减轻数据库查询压力
            System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
            string key = string.Format("{0}-{1}-{2}", method.DeclaringType.FullName, method.Name, id);
            string name = MemoryCacheHelper.GetCacheItem<string>(key, delegate()
            {
                return GetFieldValue(id, "Name", trans);
            },
            new TimeSpan(0, 30, 0));//30分钟后过期
            return name;
        }

        /// <summary>
        /// 调整联系人的组别
        /// </summary>
        /// <param name="contactId">联系人ID</param>
        /// <param name="groupIdList">分组Id集合</param>
        /// <returns></returns>
        public bool ModifyContactGroup(string contactId, List<string> groupIdList)
        {
            IContact dal = baseDal as IContact;
            return dal.ModifyContactGroup(contactId, groupIdList);
        }

        /// <summary>
        /// 根据客户ID获取对应的联系人列表
        /// </summary>
        /// <param name="customerId">客户ID</param>
        /// <returns></returns>
        public List<ContactInfo> FindByCustomer(string customerID)
        {
            IContact dal = baseDal as IContact;
            return dal.FindByCustomer(customerID);
        }

        /// <summary>
        /// 根据客户ID和名称获取实体信息
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <param name="name">名称</param>
        /// <returns></returns>
        public ContactInfo FindByCustomerAndName(string customerID, string name)
        {
            IContact dal = baseDal as IContact;
            return dal.FindByCustomerAndName(customerID, name);
        }

        /// <summary>
        /// 根据供应商ID获取对应的联系人列表
        /// </summary>
        /// <param name="supplierID">供应商ID</param>
        /// <returns></returns>
        public List<ContactInfo> FindBySupplier(string supplierID)
        {
            IContact dal = baseDal as IContact;
            return dal.FindBySupplier(supplierID);
        }

        /// <summary>
        /// 根据供应商ID和名称获取实体信息
        /// </summary>
        /// <param name="supplierID">供应商ID</param>
        /// <param name="name">名称</param>
        /// <returns></returns>
        public ContactInfo FindBySupplierAndName(string supplierID, string name)
        {
            IContact dal = baseDal as IContact;
            return dal.FindBySupplierAndName(supplierID, name);
        }

        /// <summary>
        /// 获取联系人的所属客户ID列表
        /// </summary>
        /// <param name="id">联系人id</param>
        /// <returns></returns>
        public List<string> GetCustomerIDs(string id)
        {
            string sql = string.Format("Select Customer_ID from T_CRM_Customer_Contact where Contact_ID ='{0}' ", id);
            List<string> list = new List<string>();
            string result = SqlValueList(sql);
            if (!string.IsNullOrEmpty(result))
            {
                list = result.ToDelimitedList<string>(",");
            }
            return list;
        }

        /// <summary>
        /// 获取联系人的所属供应商ID列表
        /// </summary>
        /// <param name="id">联系人id</param>
        /// <returns></returns>
        public List<string> GetSupplierIDs(string id)
        {
            string sql = string.Format("Select Supplier_ID from T_CRM_Supplier_Contact where Contact_ID ='{0}' ", id);
            List<string> list = new List<string>();
            string result = SqlValueList(sql);
            if (!string.IsNullOrEmpty(result))
            {
                list = result.ToDelimitedList<string>(",");
            }
            return list;
        }
    }
}
