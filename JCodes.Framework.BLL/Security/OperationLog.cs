using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Common.Format;
using JCodes.Framework.jCodesenum;
using JCodes.Framework.IDAL;

namespace JCodes.Framework.BLL
{
    /// <summary>
    /// 用户关键操作记录
    /// </summary>
	public class OperationLog : BaseBLL<OperationLogInfo>
    {
        private IOperationLog dal = null;

        public OperationLog() : base()
        {
            if (isMultiDatabase)
            {
                base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, dicmultiDatabase[this.GetType().Name].ToString());
            }
            else
            {
                base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
            }

            dal = baseDal as IOperationLog;
        }

        /// <summary>
        /// 根据相关信息，写入用户的操作日志记录
        /// </summary>
        /// <param name="userId">操作用户</param>
        /// <param name="tableName">操作表名称</param>
        /// <param name="operationType">操作类型 0-增加; 1-修改; 2-删除;</param>
        /// <param name="note">操作详细表述</param>
        /// <param name="trans">事务对象</param>
        /// <returns></returns>
        public static bool OnOperationLog(Int32 userId, string tableName, OperationType operationType, string remark, DbTransaction trans = null)
        {
            //虽然实现了这个事件，但是我们还需要判断该表是否在配置表里面，如果不在，则不记录操作日志。
            OperationLogSettingInfo settingInfo = BLLFactory<OperationLogSetting>.Instance.FindByTableName(tableName, trans);
            if (settingInfo != null)
            {
                bool insert = OperationType.新增 == operationType && (settingInfo.IsInsertLog == (short)IsInsertLog.是);
                bool update = OperationType.修改 == operationType && (settingInfo.IsUpdateLog == (short)IsInsertLog.是);
                bool delete = OperationType.删除 == operationType && (settingInfo.IsDeleteLog == (short)IsInsertLog.是);
                if (insert || update || delete)
                {
                    OperationLogInfo info = new OperationLogInfo();
                    info.TableName = tableName;
                    info.OperationType = (short)operationType;
                    info.Remark = remark;
                    info.CreatorTime = DateTimeHelper.GetServerDateTime2();
                    UserInfo userInfo = BLLFactory<User>.Instance.FindById(userId, trans);
                    if (userInfo != null)
                    {
                        info.UserId = userId;
                        info.Name = userInfo.Name;
                        info.LoginName = userInfo.LoginName;
                        info.CompanyId = userInfo.CompanyId;
                        info.Mac = userInfo.LastLoginMac;
                        info.IP = userInfo.LastLoginIp;
                    }
                    return BLLFactory<OperationLog>.Instance.Insert(info, trans);
                }
            }
            return false;
        }  
    }
}
