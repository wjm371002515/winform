using JCodes.Framework.Common;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Entity;
using System;

namespace JCodes.Framework.IDAL
{
    public interface ISystemType : IBaseDAL<SystemTypeInfo>
	{
        SystemTypeInfo FindByGid(string gid);
        bool VerifySystem(string licence, string systemtypeId, Int32 authorizeAmount);
	}
}