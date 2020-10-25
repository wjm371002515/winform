using JCodes.Framework.Common.Framework;
using JCodes.Framework.Entity;
using JCodes.Framework.IDAL;
using JCodes.Framework.jCodesenum;
using System;
using System.Data;


namespace JCodes.Framework.BLL
{
    /// <summary>
    /// 政策法规公告动态
    /// </summary>
	public class Information : BaseBLL<InformationInfo>
    {
        public Information() : base()
        {
            base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
        }

        /// <summary>
        /// 获取我的通知信息
        /// </summary>
        /// <param name="userId">当前用户ID</param>
        /// <param name="infoType">信息类型</param>
        /// <returns></returns>
        public DataTable GetMyInformation(int userId, Int32 infoType)
        {
            IInformation dal = baseDal as IInformation;
            // TODO
            return dal.GetMyInformation(userId, 1);
        }
    }
}
