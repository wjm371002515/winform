using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Entity.Machines;

namespace JCodes.Framework.IDAL
{
	/// <summary>
	/// ICity 的摘要说明。
	/// </summary>
    public interface IDongnanhaiVotes : IBaseDAL<DongnanhaiVotesInfo>
	{
        List<DongnanhaiVotesInfo> GetVotesBylouzhuang(string louzhuang);

        Int32 MaxCengHuShu(string louzhuang);

        void UpdateFlag(string fanghao, string util, string zhuang, string yuan, Int32 flag);
    }
}