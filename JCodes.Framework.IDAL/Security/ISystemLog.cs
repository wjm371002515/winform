using JCodes.Framework.Entity;
using JCodes.Framework.Common.Framework;

namespace JCodes.Framework.IDAL
{
	/// <summary>
	/// ISystemLog 的摘要说明
	/// </summary>
	public interface ISystemLog : IBaseDAL<SystemLogInfo>
	{
        bool AddSystemLog(SystemLogInfo systemLog);
	}
}