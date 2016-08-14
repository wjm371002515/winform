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
    public class Daily : IDaily
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

        public Model.Daily GetDailyById(int id)
        {
            string sql = "select top 1 * from tbDaily where Id = @Id";
            Model.Daily daily = null;
            DataTable dt = SqlHelper.GetDataTable(SqlHelper.connStr, CommandType.Text, sql, new SqlParameter("@Id", id));
            if (dt.Rows.Count > 0)
            {
                daily = new Model.Daily();
                DataRowToModel(daily, dt.Rows[0]);
                
            }
            return daily;
        }

        public List<Model.Daily> GetDailyById(string createdate, string shopname, string aliwangwang, string key,
            string isSolve)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from tbDaily where 1=1");
            SqlParameter[] param = new SqlParameter[5];
            int count = 0;
            if (!string.IsNullOrEmpty(createdate.Trim()))
            {
                sb.Append(" and createdate=@createdate");
                param[count++] = new SqlParameter("@createdate", createdate);
            }
            if (!string.IsNullOrEmpty(shopname.Trim()))
            {
                sb.Append(" and shopname=@shopname");
                param[count++] = new SqlParameter("@shopname", shopname);
            }
            if (!string.IsNullOrEmpty(aliwangwang.Trim()))
            {
                sb.Append(" and aliwangwang=@aliwangwang");
                param[count++] = new SqlParameter("@aliwangwang", aliwangwang);
            }
            if (!string.IsNullOrEmpty(key.Trim()))
            {
                sb.Append(" and ((shopname like @key) or (aliwangwang like @key) or (dosolve like @key) or (remark like @key))");
                param[count++] = new SqlParameter("@key", key);
            }
            if (!string.IsNullOrEmpty(isSolve.Trim()))
            {
                sb.Append(" and isSolve=@isSolve");
                param[count++] = new SqlParameter("@isSolve", isSolve);
            }


            DataTable dt = SqlHelper.GetDataTable(SqlHelper.connStr, CommandType.Text, sb.Append(";").ToString(), param);

            if (dt.Rows.Count < 0)
            {
                return null;
            }
            List<Model.Daily> dailyList = new List<Model.Daily>();
            Model.Daily daily = null;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                daily = new Model.Daily();
                DataRowToModel(daily, dt.Rows[i]);
                dailyList.Add(daily);
            }
            return dailyList;
        }


        public string AddDaily(Model.Daily daily)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tbDaily(createdate, shopname, aliwangwang, dosolve, username, isSolve, solvename, remark)");
            strSql.Append(" values ");
            strSql.Append("(@createdate,@shopname,@aliwangwang,@dosolve,@username,@isSolve,@solvename,@remark)");
            strSql.Append(";SELECT Max(Id) from tbDaily;");   //返回订单号
            SqlParameter[] paras = { 
                                   new SqlParameter("@createdate", daily.createdate),
                                   new SqlParameter("@shopname", daily.shopname),
                                   new SqlParameter("@aliwangwang", daily.aliwangwang),
                                   new SqlParameter("@dosolve", daily.dosolve),
                                   new SqlParameter("@username", daily.username),
                                   new SqlParameter("@isSolve", daily.isSolve),
                                   new SqlParameter("@solvename", daily.solvename),
                                   new SqlParameter("@remark", daily.remark)
                                   };
            return SqlHelper.ExecuteScalar(SqlHelper.connStr, CommandType.Text, strSql.ToString(), paras).ToString();
        }

        // 暂时先不做
        public bool EditDaily(Model.Daily daily)
        {
            string editstr = @"UPDATE tbDaily " +
                              "SET [shopname]=@shopname, aliwangwang=@aliwangwang, dosolve=@dosolve, isSolve=@isSolve, solvename=@solvename, remark=@remark " +
                              "WHERE id=@id;";

            SqlParameter[] paras = { 
                                   new SqlParameter("@shopname", daily.shopname),
                                   new SqlParameter("@aliwangwang", daily.aliwangwang),
                                   new SqlParameter("@dosolve", daily.dosolve),
                                   new SqlParameter("@username", daily.username),
                                   new SqlParameter("@isSolve", daily.isSolve),
                                   new SqlParameter("@solvename", daily.solvename),
                                   new SqlParameter("@remark", daily.remark),
                                   new SqlParameter("@id",daily.Id)
                                   };
            object obj = SqlHelper.ExecuteNonQuery(SqlHelper.connStr, CommandType.Text, editstr, paras);
            if (Convert.ToInt32(obj) > 0)
                return true;
            else
                return false;
        }

        // 删除功能
         public bool DelDaily(int id)
        {
            string delstr = "delete from tbDaily where id = @id;";
            SqlParameter paras = new SqlParameter("@id", id);
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
         private void DataRowToModel(Model.Daily model, DataRow dr)
        {
            if (!DBNull.Value.Equals(dr["id"]))
                model.Id = Convert.ToInt32(dr["Id"]);
            if (!DBNull.Value.Equals(dr["createdate"]))
                model.createdate = dr["createdate"].ToString();
            if (!DBNull.Value.Equals(dr["shopname"]))
                model.shopname = dr["shopname"].ToString();
            if (!DBNull.Value.Equals(dr["aliwangwang"]))
                model.aliwangwang = dr["aliwangwang"].ToString();
            if (!DBNull.Value.Equals(dr["dosolve"]))
                model.dosolve = dr["dosolve"].ToString();
            if (!DBNull.Value.Equals(dr["username"]))
                model.username = dr["username"].ToString();
            if (!DBNull.Value.Equals(dr["isSolve"]))
                model.isSolve = int.Parse(dr["isSolve"].ToString());
            if (!DBNull.Value.Equals(dr["solvename"]))
                model.solvename = dr["solvename"].ToString();
            if (!DBNull.Value.Equals(dr["remark"]))
                model.remark = dr["remark"].ToString();
        }
    }
}
