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

namespace JCodes.Framework.SQLServerDAL
{
    /// <summary>
    /// TODO
    /// </summary>
    public class DongnanhaiVotesUser : BaseDALSQLServer<VoteUserInfo>, IDongnanhaiVotesUser
    {
        #region 对象实例及构造函数

        public static DongnanhaiVotesUser Instance
        {
            get
            {
                return new DongnanhaiVotesUser();
            }
        }
        public DongnanhaiVotesUser()
            : base()
        {
            IsDescending = false;
        }
        #endregion

        public void InsertVoteUser(VoteUserInfo voteUserInfo)
        {
            if (voteUserInfo.Id == 0)
                return;

            if (HasVoteUser(voteUserInfo.Id) > 0)
                return;

            string sql = string.Format("insert into T_Dongnanhai_User(Id, phone, real_name, sex) values({0}, '{1}', '{2}', '{3}')", voteUserInfo.Id, voteUserInfo.MobilePhone, voteUserInfo.LoginName, voteUserInfo.Gender);
            base.SqlExecute(sql);
        }

        public Int32 HasVoteUser(Int32 userId) {
            string sql = string.Format("select count(1) as Row_Count from T_Dongnanhai_User where id={0}", userId);
            Database db = CreateDatabase();
            DbCommand command = db.GetSqlStringCommand(sql);
            return base.GetExecuteScalarValue(db, command);
        }
    }
}