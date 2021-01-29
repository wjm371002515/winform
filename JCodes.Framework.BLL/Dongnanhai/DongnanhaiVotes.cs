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
	public class DongnanhaiVotes :  BaseBLL<DongnanhaiVotesInfo>
    {
        private IDongnanhaiVotes dal = null;

        public DongnanhaiVotes()
            : base()
        {
            if (isMultiDatabase)
            {
                base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, dicmultiDatabase[this.GetType().Name].ToString());
            }
            else
            {
                base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
            }

            baseDal.OnOperationLog += new OperationLogEventHandler(OperationLog.OnOperationLog);//如果需要记录操作日志，则实现这个事件

            dal = baseDal as IDongnanhaiVotes;
        }

        /// <summary>
        /// 根据省份名称获取对应的城市列表
        /// </summary>
        /// <param name="provinceName">省份名称</param>
        /// <returns></returns>
        public List<DongnanhaiVotesInfo> GetVotesBylouzhuang(string louzhuang)
        {
            IDongnanhaiVotes dal = baseDal as IDongnanhaiVotes;
            return dal.GetVotesBylouzhuang(louzhuang);
        }

        public Int32 MaxCengHuShu(string louzhuang)
        {
            IDongnanhaiVotes dal = baseDal as IDongnanhaiVotes;
            return dal.MaxCengHuShu(louzhuang);
        }
        // fanghao='1004' and util='1' and zhuang='8' and yuan='钱塘东南家园'
        public void UpdateFlag(string fanghao, string util, string zhuang, string yuan, Int32 flag)
        {
            IDongnanhaiVotes dal = baseDal as IDongnanhaiVotes;
            dal.UpdateFlag(fanghao, util, zhuang, yuan, flag);
        }

        public Int32 GetXianchangAndPhoneShu(string condition)
        {
            IDongnanhaiVotes dal = baseDal as IDongnanhaiVotes;
            return dal.GetXianchangAndPhoneShu(condition);
        }
    }
}
