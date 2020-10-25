using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;
using JCodes.Framework.IDAL;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Common.Databases;
using JCodes.Framework.Common.Format;
using JCodes.Framework.jCodesenum;

namespace JCodes.Framework.BLL
{
    /// <summary>
    /// 用户登录日志信息
    /// </summary>
	public class LoginLog : BaseBLL<LoginLogInfo>
    {
        private ILoginLog dal = null;

        public LoginLog() : base()
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

            dal = baseDal as ILoginLog;
        }

        /// <summary>
        /// 记录用户操作日志
        /// </summary>
        /// <param name="info">用户信息</param>
        /// <param name="systemType">系统类型ID</param>
        /// <param name="ip">IP地址</param>
        /// <param name="macAddr">Mac地址</param>
        /// <param name="note">备注说明</param>
        public void AddLoginLog(UserInfo info, string systemtypeId, string ip, string mac, string remark)
        {
            if (info == null) return;

            #region 记录用户登录操作
            try
            {
                LoginLogInfo logInfo = new LoginLogInfo();
                logInfo.IP = ip;
                logInfo.Mac = mac;
                logInfo.LastUpdateTime = DateTimeHelper.GetServerDateTime2();
                logInfo.Remark = remark;
                logInfo.SystemtypeId = systemtypeId;
                logInfo.Id = info.Id;
                logInfo.Name = info.Name;
                logInfo.LoginName = info.LoginName;
                logInfo.CompanyId = info.CompanyId;
                logInfo.CurrentLoginUserId = info.Id;
                BLLFactory<LoginLog>.Instance.Insert(logInfo);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(LoginLog));
            }
            #endregion
        }

        /// <summary>
        /// 根据最后更新日前的数据获取数据
        /// </summary>
        /// <param name="shopGuid">商店GUID</param>
        /// <param name="LastUpdated">最后更新日前</param>
        /// <returns></returns>
        public List<LoginLogInfo> GetList(DateTime lastUpdateTime)
        {
            SearchCondition search = new SearchCondition();
            search.AddCondition("LastUpdateTime", lastUpdateTime, SqlOperator.MoreThanOrEqual);
            string condition = search.BuildConditionSql().Replace("Where", "");
            return Find(condition);
        }

        /// <summary>
        /// 如果目标不存在则插入，否则判断更新时间，如果目标较旧则更新
        /// </summary>
        /// <param name="infoList"></param>
        public void InsertOrUpdate(List<LoginLogInfo> infoList)
        {
            if (infoList != null && infoList.Count > 0)
            {
                foreach (LoginLogInfo info in infoList)
                {
                    LoginLogInfo tempInfo = baseDal.FindById(info.Id);
                    if (tempInfo != null)
                    {
                        if (tempInfo.LastUpdateTime < info.LastUpdateTime)
                        {
                            baseDal.Update(info, info.Id);
                        }
                    }
                    else
                    {
                        baseDal.Insert(info);
                    }
                }
            }
        }

        /// <summary>
        /// 删除一个月前的数据
        /// </summary>
        public void DeleteMonthLog()
        {
            SearchCondition search = new SearchCondition();
            search.AddCondition("LastUpdateTime", DateTimeHelper.GetServerDateTime2().AddDays(-30), SqlOperator.LessThanOrEqual);
            string condition = search.BuildConditionSql().Replace("Where", "");
            baseDal.DeleteByCondition(condition);
        }

        /// <summary>
        /// 获取上一次（非刚刚登录）的登录日志
        /// </summary>
        /// <param name="userId">登录用户ID</param>
        /// <returns></returns>
        public LoginLogInfo GetLastLoginInfo(Int32 userId)
        {
            return dal.GetLastLoginInfo(userId);
        }
    }
}
