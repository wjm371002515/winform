using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using JCodes.Framework.Data.IDAL;
using JCodes.Framework.Data.DALFactory;
using JCodes.Framework.Common;

namespace JCodes.Framework.Data.BLL
{
    public class Order
    {
        private static readonly IOrder dal = Factory.GetOrderDAL();

        public DataTable GetButtonByMenuCodeAndUserId(string menuCode, int userId)
        {
            return dal.GetButtonByMenuCodeAndUserId(menuCode, userId);
        }

        public string AddOrder(Model.Order order)
        {
            Model.Order orderCompare = dal.GetOrderByOrderId(order.in_orderid);
            if (orderCompare != null)
            {
                throw new Exception("订单已存在");
            }
            return dal.AddOrder(order);
        }

        public bool EditOrder(Model.Order order)
        {
            Model.Order orderCompare = dal.GetOrderByOrderId(order.in_orderid);
            if (orderCompare == null)
            {
                throw new Exception("订单不存在");
            }
            return dal.EditOrder(order);
        }

        public bool DelOrder(string orderid)
        {
            Model.Order orderCompare = dal.GetOrderByOrderId(orderid);
            if (orderCompare == null)
            {
                throw new Exception("订单不存在");
            }
            return dal.DelOrder(orderid);
        }

        public Model.Order GetOrderById(string id)
        {
            return dal.GetOrderByOrderId(id);
        }

        public List<Model.Order> GetTableOrder(string type, string in_company, string in_orderid, string Id, string aliwangwang)
        {
            return dal.GetOrderByOrderId(type, in_company, in_orderid, Id, aliwangwang);
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columns">要取的列名（逗号分开）</param>
        /// <param name="order">排序</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="where">查询条件</param>
        /// <param name="totalCount">总记录数</param>
        public string GetPager(string tableName, string columns, string order, int pageSize, int pageIndex, string where, out int totalCount)
        {
            DataTable dt = SqlPagerHelper.GetPager(tableName, columns, order, pageSize, pageIndex, where, out totalCount);
            return JsonHelper.ToJson(dt);
        }
    }
}
