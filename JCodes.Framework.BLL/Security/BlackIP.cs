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
using JCodes.Framework.jCodesenum;

namespace JCodes.Framework.BLL
{
    /// <summary>
    /// 登陆系统的黑白名单列表(白名单优先于黑名单）
    /// </summary>
	public class BlackIP : BaseBLL<BlackIPInfo>
    {
        private IBlackIP dal;

        public BlackIP() : base()
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

            dal = baseDal as IBlackIP;
        }                     

        /// <summary>
        /// 根据名单ID获取对应的用户列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<SimpleUserInfo> GetSimpleUserByBlackIP(Int32 Id)
        {
            string userIdList = "-1," + dal.GetUserIdList(Id);

            return BLLFactory<User>.Instance.GetSimpleUsers(userIdList.Trim(','));
        }

        public void AddUser(Int32 userId, Int32 blackId)
        {
            dal.AddUser(userId, blackId);
        }

        public void RemoveUser(Int32 userId, Int32 blackId)
        {
            dal.RemoveUser(userId, blackId);
        }

        public void RemoveUserByBlackId(Int32 blackId)
        {
            dal.RemoveUserByBlackId(blackId);
        }
        
        /// <summary>
        /// 根据用户ID和授权类型获取列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="type">授权类型</param>
        /// <returns></returns>
        public List<BlackIPInfo> FindByUser(Int32 userId, AuthorizeType authorizeType)
        {
            return dal.FindByUser(userId, authorizeType);
        }

        /// <summary>
        /// 检验IP的可访问性(白名单优先于黑名单），如果同时白名单、黑名名单都有同一IP，则也允许访问。
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public bool ValidateIPAccess(string ip, Int32 userId)
        {
            bool result = false;

            List<BlackIPInfo> whiteList = dal.FindByUser(userId, AuthorizeType.白名单);    // 白名单

            if (whiteList.Count > 0)
            {
                result = IsInList(whiteList, ip);
                return result; //白名单优先于黑名单，在白名单则通过
            }

            List<BlackIPInfo> blackList = dal.FindByUser(userId, AuthorizeType.黑名单);    // 黑名单
            if (blackList.Count > 0)
            {
                bool flag = IsInList(blackList, ip);
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
