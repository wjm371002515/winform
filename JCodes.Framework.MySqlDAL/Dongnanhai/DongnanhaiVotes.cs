using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using JCodes.Framework.Entity;
using JCodes.Framework.IDAL;
using JCodes.Framework.Common;
using JCodes.Framework.Common.Framework.BaseDAL;
using JCodes.Framework.Common.Databases;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace JCodes.Framework.MySqlDAL
{
	/// <summary>
	/// City 的摘要说明。
	/// </summary>
    public class DongnanhaiVotes : BaseDALMySql<DongnanhaiVotesInfo>, IDongnanhaiVotes
	{
		#region 对象实例及构造函数

        public static DongnanhaiVotes Instance
		{
			get
			{
                return new DongnanhaiVotes();
			}
		}
        public DongnanhaiVotes()
            : base()
		{
            IsDescending = false;
		}

        public List<DongnanhaiVotesInfo> GetVotesBylouzhuang(string louzhuang) {
            string sql = string.Format("select * from {0}dongnanhaiVotes where zhuang='{1}' order by ceng,util,fanghao", MySqlPortal.gc._dongnanhaiTablePre, louzhuang);
            return base.GetList(sql, null);
        }

        public Int32 MaxCengHuShu(string louzhuang) {
            string sql = string.Format("select max(t.row_count) as max_hushu from (SELECT count(1) as row_count FROM `{0}dongnanhaiVotes` WHERE zhuang='{1}' group by ceng) t", MySqlPortal.gc._dongnanhaiTablePre, louzhuang);
            Database db = CreateDatabase();
            DbCommand command = db.GetSqlStringCommand(sql);
            return base.GetExecuteScalarValue(db, command);
        }

        public void UpdateFlag(string fanghao, string util, string zhuang, string yuan, Int32 flag) {
            string sql = string.Format("set autocommit=0;Update {0}dongnanhaiVotes set flag='{1}' where fanghao='{2}' and util='{3}' and zhuang='{4}' and yuan='{5}'", MySqlPortal.gc._dongnanhaiTablePre, flag, fanghao, util, zhuang, yuan);
            base.SqlExecute(sql);
        }

        /// <summary>
        /// 根据条件过滤
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public Int32 GetXianchangAndPhoneShu(string condition) {
            string sql = string.Format("select count(1) from {0}dongnanhaiVotes where {1}", MySqlPortal.gc._dongnanhaiTablePre, condition);
            Database db = CreateDatabase();
            DbCommand command = db.GetSqlStringCommand(sql);
            return base.GetExecuteScalarValue(db, command);
        }

		#endregion
    }
}