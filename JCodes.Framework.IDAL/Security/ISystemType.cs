using JCodes.Framework.Common;
using JCodes.Framework.Entity;

namespace JCodes.Framework.IDAL
{
    public interface ISystemType : IBaseDAL<SystemTypeInfo>
	{
		SystemTypeInfo FindByOID(string oid);
		bool VerifySystem(string serialNumber, string typeID, int authorizeAmount);
	}
}