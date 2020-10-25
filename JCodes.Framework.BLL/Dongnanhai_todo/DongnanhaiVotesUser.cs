using System.Collections.Generic;
using JCodes.Framework.IDAL;
using JCodes.Framework.Common.Framework;
using System;
using JCodes.Framework.Entity;

namespace JCodes.Framework.BLL
{
    /// <summary>
    /// 城市业务对象类
    /// </summary>
	public class DongnanhaiVotesUser : BaseBLL<VoteUserInfo>
    {
        public DongnanhaiVotesUser()
            : base()
        {
            base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
        }

       
        /// <summary>
        /// 根据省份名称获取对应的城市列表
        /// </summary>
        /// <param name="provinceName">省份名称</param>
        /// <returns></returns>
        public void InsertVoteUser(VoteUserInfo voteUserInfo)
        {
            IDongnanhaiVotesUser dal = baseDal as IDongnanhaiVotesUser;
            dal.InsertVoteUser(voteUserInfo);
        }
    }
}
