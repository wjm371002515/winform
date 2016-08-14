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
    public class Daily
    {
        private static readonly IDaily dal = Factory.GetDailyDAL();

        public DataTable GetButtonByMenuCodeAndUserId(string menuCode, int userId)
        {
            return dal.GetButtonByMenuCodeAndUserId(menuCode, userId);
        }

        public string AddDaily(Model.Daily daily)
        {
            return dal.AddDaily(daily);
        }

        public bool EditDaily(Model.Daily daily)
        {
            Model.Daily dailyCompare = dal.GetDailyById(daily.Id);
            if (dailyCompare == null)
            {
                throw new Exception("备忘录记录不存在");
            }
            return dal.EditDaily(daily);
        }

        public bool DelDaily(int id)
        {
            
            Model.Daily dailyCompare = dal.GetDailyById(id);
            if (dailyCompare == null)
            {
                throw new Exception("备忘录记录不存在");
            }
            return dal.DelDaily(id);
        }

        public Model.Daily GetDailyById(int id)
        {
            return dal.GetDailyById(id);
        }

        public List<Model.Daily> GetTableDaily(string createdate, string shopname, string aliwangwang, string key,
            string isSolve)
        {
            return dal.GetDailyById(createdate, shopname, aliwangwang, key, isSolve);
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
