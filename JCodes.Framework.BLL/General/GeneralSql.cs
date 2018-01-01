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
    /// <summary>
    /// 城市业务对象类
    /// </summary>
    public class GeneralSql : BaseBLL<BaseEntity>
    {
        public GeneralSql()
            : base()
        {
            base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
        }

        /// <summary>
        /// 通用查询
        /// </summary>
        /// <param name="sql">查询Sql</param>
        /// <param name="count">查询数量</param>
        /// <param name="orderBy">排序条件</param>
        /// <returns></returns>
        public DataTable GetSqlTable(string sql)
        {
            IGeneralSql dal = baseDal as IGeneralSql;
            return dal.SqlTable(sql);
        }
    }
}
