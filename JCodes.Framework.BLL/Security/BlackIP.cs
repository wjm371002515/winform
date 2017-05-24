using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;
using JCodes.Framework.IDAL;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Common.Network;

namespace JCodes.Framework.BLL
{
    /// <summary>
    /// 登陆系统的黑白名单列表(白名单优先于黑名单）
    /// </summary>
	public class BlackIP : BaseBLL<BlackIPInfo>
    {
        public BlackIP() : base()
        {
            base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
            baseDal.OnOperationLog += new OperationLogEventHandler(OperationLog.OnOperationLog);//如果需要记录操作日志，则实现这个事件
        }                     

        /// <summary>
        /// 根据名单ID获取对应的用户列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<SimpleUserInfo> GetSimpleUserByBlackIP(string id)
        {
            IBlackIP dal = baseDal as IBlackIP;
            string userIdList = "-1," + dal.GetUserIdList(id);

            return BLLFactory<User>.Instance.GetSimpleUsers(userIdList.Trim(','));
        }
        
        public void AddUser(int userID, string blackID)
        {
            IBlackIP dal = baseDal as IBlackIP;
            dal.AddUser(userID, blackID);
        }

        public void RemoveUser(int userID, string blackID)
        {
            IBlackIP dal = baseDal as IBlackIP;
            dal.RemoveUser(userID, blackID);
        }
        
        /// <summary>
        /// 根据用户ID和授权类型获取列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="type">授权类型</param>
        /// <returns></returns>
        public List<BlackIPInfo> FindByUser(int userId, AuthrizeType type)
        {
            IBlackIP dal = baseDal as IBlackIP;
            return dal.FindByUser(userId, type);
        }

        /// <summary>
        /// 检验IP的可访问性(白名单优先于黑名单），如果同时白名单、黑名名单都有同一IP，则也允许访问。
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public bool ValidateIPAccess(string ipAddress, int userId)
        {
            bool result = false;
            IBlackIP dal = baseDal as IBlackIP;
            List<BlackIPInfo> whiteList = dal.FindByUser(userId, AuthrizeType.白名单);

            if (whiteList.Count > 0)
            {
                result = IsInList(whiteList, ipAddress);
                return result; //白名单优先于黑名单，在白名单则通过
            }

            List<BlackIPInfo> blackList = dal.FindByUser(userId, AuthrizeType.黑名单);
            if (blackList.Count > 0)
            {
                bool flag = IsInList(blackList, ipAddress);
                return !flag;//不在则通过，在就禁止
            }

            //当黑白名单都为空的时候，那么返回true，则默认不禁止
            return true;
        }

        private bool IsInList(List<BlackIPInfo> list, string ip)
        {
            foreach (BlackIPInfo info in list)
            {
                if (NetworkUtil.IsInIp(ip, info.IPStart, info.IPEnd))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
