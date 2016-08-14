using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using JCodes.Framework.Common;
using JCodes.Framework.Data.IDAL;
using JCodes.Framework.Data.Model;

namespace JCodes.Framework.Data.SQLServerDAL
{
    public class Order : IOrder
    {

        public DataTable GetButtonByMenuCodeAndUserId(string menuCode, int userId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct(b.Id) id,b.Code code,b.Name name,b.Icon icon,b.Sort sort from tbUser u");
            strSql.Append(" join tbUserRole ur on u.Id=ur.UserId");
            strSql.Append(" join tbRoleMenuButton rmb on ur.RoleId=rmb.RoleId");
            strSql.Append(" join tbMenu m on rmb.MenuId=m.Id");
            strSql.Append(" join tbButton b on rmb.ButtonId=b.Id");
            strSql.Append(" where u.Id=@Id and m.Code=@MenuCode order by b.Sort");
            SqlParameter[] paras = { 
                                   new SqlParameter("@Id",userId),
                                   new SqlParameter("@MenuCode",menuCode)
                                   };

            return SqlHelper.GetDataTable(SqlHelper.connStr, CommandType.Text, strSql.ToString(), paras);
        }

        public Model.Order GetOrderByOrderId(string orderid)
        {
            string sql = "select top 1 * from tbOrder where in_orderid = @in_orderid";
            Model.Order order = null;
            DataTable dt = SqlHelper.GetDataTable(SqlHelper.connStr, CommandType.Text, sql, new SqlParameter("@in_orderid", orderid));
            if (dt.Rows.Count > 0)
            {
                order = new Model.Order();
                DataRowToModel(order, dt.Rows[0]);
                return order;
            }
            else
                return null;
        }

        public List<Model.Order> GetOrderByOrderId(string type, string in_company, string in_orderid, string Id, string aliwangwang)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from tbOrder where 1=1");
            Model.Order order = null;
            SqlParameter[] param = new SqlParameter[5];
            int count = 0;
            if (!string.IsNullOrEmpty(type))
            {
                param[count++] = new SqlParameter("@type", type);
                sb.Append(" and type = @type");
            }
            if (!string.IsNullOrEmpty(in_company))
            {
                param[count++] = new SqlParameter("@in_company", in_company);
                sb.Append(" and in_company = @in_company");
            }
            if (!string.IsNullOrEmpty(in_orderid))
            {
                param[count++] = new SqlParameter("@in_orderid", in_orderid);
                sb.Append(" and in_orderid = @in_orderid");
            }
            if (!string.IsNullOrEmpty(Id))
            {
                param[count++] = new SqlParameter("@id", Id);
                sb.Append(" and id = @id");
            }
            if (!string.IsNullOrEmpty(aliwangwang))
            {
                param[count++] = new SqlParameter("@aliwangwang", aliwangwang);
                sb.Append(" and aliwangwang = @aliwangwang");
            }

            DataTable dt = SqlHelper.GetDataTable(SqlHelper.connStr, CommandType.Text, sb.Append(";").ToString(), param);

            if (dt.Rows.Count < 0)
            {
                return null;
            }
            List<Model.Order> orderList = new List<Model.Order>();
            for (int i = 0; i < dt.Rows.Count; i ++ ) 
            {
                order = new Model.Order();
                DataRowToModel(order, dt.Rows[i]);
                orderList.Add(order);
            }
            return orderList;
        }

        public string AddOrder(Model.Order order)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tbOrder(type, in_company, in_orderid, id, in_color, in_size, in_num, aliwangwang, out_company, out_orderid, out_color, out_size, out_num, remark, create_time, update_time, username, shopname, out_id)");
            strSql.Append(" values ");
            strSql.Append("(@type,@in_company,@in_orderid,@id,@in_color,@in_size,@in_num,@aliwangwang,@out_company,@out_orderid,@out_color,@out_size,@out_num,@remark,@create_time,@update_time,@username,@shopname,@out_id)");
            strSql.Append(";SELECT '" + order.in_orderid+"';");   //返回订单号
            SqlParameter[] paras = { 
                                   new SqlParameter("@type",order.type),
                                   new SqlParameter("@in_company",order.in_company),
                                   new SqlParameter("@in_orderid",order.in_orderid),
                                   new SqlParameter("@id",order.id),
                                   new SqlParameter("@in_color",order.in_color),
                                   new SqlParameter("@in_size",order.in_size),
                                   new SqlParameter("@in_num",order.in_num),
                                   new SqlParameter("@aliwangwang",order.aliwangwang),
                                   new SqlParameter("@out_company",order.out_company),
                                   new SqlParameter("@out_orderid",order.out_orderid),
                                   new SqlParameter("@out_color",order.out_color),
                                   new SqlParameter("@out_size",order.out_size),
                                   new SqlParameter("@out_num",order.out_num),
                                   new SqlParameter("@remark",order.remark),
                                   new SqlParameter("@create_time",DateTime.Now),
                                   new SqlParameter("@update_time",DateTime.Now),
                                   new SqlParameter("@username",order.username),
                                   new SqlParameter("@shopname",order.shopname),
                                   new SqlParameter("@out_id",order.out_id),
                                   };
            return SqlHelper.ExecuteScalar(SqlHelper.connStr, CommandType.Text, strSql.ToString(), paras).ToString();
        }

        // 暂时先不做
        public bool EditOrder(Model.Order order)
        {
            string editstr = @"UPDATE tbOrder "+
                              "SET [type]=@type, shopname=@shopname, in_company=@in_company, id=@id, in_color=@in_color, in_size=@in_size, in_num=@in_num, aliwangwang=@aliwangwang, " +
                              "out_company=@out_company, out_orderid=@out_orderid, out_id=@out_id, out_color=@out_color,out_size=@out_size, out_num=@out_num, remark=@remark,update_time=GETDATE(),username=@username " +
                              "WHERE in_orderid=@in_orderid;";

            SqlParameter[] paras = { 
                                   new SqlParameter("@type",order.type),
                                   new SqlParameter("@shopname",order.shopname),
                                   new SqlParameter("@in_company",order.in_company),
                                   new SqlParameter("@id",order.id),
                                   new SqlParameter("@in_color",order.in_color),
                                   new SqlParameter("@in_size",order.in_size),
                                   new SqlParameter("@in_num",order.in_num),
                                   new SqlParameter("@aliwangwang",order.aliwangwang),
                                   new SqlParameter("@out_company",order.out_company),
                                   new SqlParameter("@out_orderid",order.out_orderid),
                                   new SqlParameter("@out_id",order.out_id),
                                   new SqlParameter("@out_color",order.out_color),
                                   new SqlParameter("@out_size",order.out_size),
                                   new SqlParameter("@out_num",order.out_num),
                                   new SqlParameter("@remark",order.remark),
                                   new SqlParameter("@username",order.username),
                                   new SqlParameter("@in_orderid",order.in_orderid)
                                   };
            object obj = SqlHelper.ExecuteNonQuery(SqlHelper.connStr, CommandType.Text, editstr, paras);
            if (Convert.ToInt32(obj) > 0)
                return true;
            else
                return false;
        }

        // 删除功能
        public bool DelOrder(string orderid)
        {
            string delstr = "delete from tbOrder where in_orderid = @in_orderid;";
            SqlParameter paras = new SqlParameter("@in_orderid", orderid);
            try
            {
                SqlHelper.ExecuteNonQuery(SqlHelper.connStr, CommandType.Text, delstr, paras);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 把DataRow行转成实体类对象
        /// </summary>
        private void DataRowToModel(Model.Order model, DataRow dr)
        {
            if (!DBNull.Value.Equals(dr["type"]))
                model.type = Convert.ToChar(dr["type"]);
            if (!DBNull.Value.Equals(dr["in_company"]))
                model.in_company = dr["in_company"].ToString();
            if (!DBNull.Value.Equals(dr["in_orderid"]))
                model.in_orderid = dr["in_orderid"].ToString();
            if (!DBNull.Value.Equals(dr["id"]))
                model.id = dr["id"].ToString();
            if (!DBNull.Value.Equals(dr["in_color"]))
                model.in_color = dr["in_color"].ToString();
            if (!DBNull.Value.Equals(dr["in_size"]))
                model.in_size = dr["in_size"].ToString();
            if (!DBNull.Value.Equals(dr["in_num"]))
                model.in_num = int.Parse(dr["in_num"].ToString());
            if (!DBNull.Value.Equals(dr["aliwangwang"]))
                model.aliwangwang = dr["aliwangwang"].ToString();
            if (!DBNull.Value.Equals(dr["out_company"]))
                model.out_company = dr["out_company"].ToString();
            if (!DBNull.Value.Equals(dr["out_orderid"]))
                model.out_orderid = dr["out_orderid"].ToString();
            if (!DBNull.Value.Equals(dr["out_color"]))
                model.out_color = dr["out_color"].ToString();
            if (!DBNull.Value.Equals(dr["out_size"]))
                model.out_size = dr["out_size"].ToString();
            if (!DBNull.Value.Equals(dr["out_num"]))
                model.out_num = int.Parse(dr["out_num"].ToString());
            if (!DBNull.Value.Equals(dr["remark"]))
                model.remark = dr["remark"].ToString();
            if (!DBNull.Value.Equals(dr["create_time"]))
                model.create_time = Convert.ToDateTime(dr["create_time"]);
            if (!DBNull.Value.Equals(dr["update_time"]))
                model.update_time = Convert.ToDateTime(dr["update_time"]);
            if (!DBNull.Value.Equals(dr["username"]))
                model.username = dr["username"].ToString();
            if (!DBNull.Value.Equals(dr["shopname"]))
                model.username = dr["shopname"].ToString();
            if (!DBNull.Value.Equals(dr["out_id"]))
                model.username = dr["out_id"].ToString();
        }
    }
}
