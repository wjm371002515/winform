using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using JCodes.Framework.Data.Model;

namespace JCodes.Framework.Data.IDAL
{
    public interface IOrder
    {
        DataTable GetButtonByMenuCodeAndUserId(string menuCode, int userId);

        Order GetOrderByOrderId(string orderid);

        List<Order> GetOrderByOrderId(string type, string in_company, string in_orderid, string id, string aliwangwang);

        string AddOrder(Order order);

        bool EditOrder(Order order);

        bool DelOrder(string id);
    }
}
