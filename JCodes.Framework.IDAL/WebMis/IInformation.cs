using JCodes.Framework.Common.Framework;
using JCodes.Framework.Entity;
using System.Data;

namespace JCodes.Framework.IDAL
{
    /// <summary>
    /// 政策法规公告动态
    /// </summary>
	public interface IInformation : IBaseDAL<InformationInfo>
	{
              
        /// <summary>
        /// 获取我的通知信息
        /// </summary>
        /// <param name="userId">当前用户ID</param>
        /// <param name="infoType">信息类型</param>
        /// <returns></returns>
        DataTable GetMyInformation(int userId, InformationCategory infoType);
    }
}