using JCodes.Framework.Common.Framework;
using JCodes.Framework.Entity;
using JCodes.Framework.IDAL;
using JCodes.Framework.jCodesenum;

namespace JCodes.Framework.BLL
{
    /// <summary>
    /// 用户对指定内容的操作状态记录
    /// </summary>
	public class InformationStatus : BaseBLL<InformationStatusInfo>
    {
        public InformationStatus() : base()
        {
            base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
        }

        /// <summary>
        /// 设置状态
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="InfoType">信息类型</param>
        /// <param name="InfoID">信息主键ID</param>
        /// <param name="Status">状态</param>
        public void SetStatus(string UserID, InformationCategory InfoType, string InfoID, int Status)
        {
            IInformationStatus dal = baseDal as IInformationStatus;
            dal.SetStatus(UserID, InfoType, InfoID, Status);
        }

        /// <summary>
        /// 匹配状态
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="InfoType">信息类型</param>
        /// <param name="InfoID">信息主键ID</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public bool CheckStatus(string UserID, InformationCategory InfoType, string InfoID, int Status)
        {
            IInformationStatus dal = baseDal as IInformationStatus;
            return dal.CheckStatus(UserID, InfoType, InfoID, Status);
        }
    }
}
