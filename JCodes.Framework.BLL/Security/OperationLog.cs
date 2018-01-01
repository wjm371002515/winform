using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Common.Format;

namespace JCodes.Framework.BLL
{
    /// <summary>
    /// 用户关键操作记录
    /// </summary>
	public class OperationLog : BaseBLL<OperationLogInfo>
    {
        public OperationLog() : base()
        {
            base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
        }

        /// <summary>
        /// 根据相关信息，写入用户的操作日志记录
        /// </summary>
        /// <param name="userId">操作用户</param>
        /// <param name="tableName">操作表名称</param>
        /// <param name="operationType">操作类型</param>
        /// <param name="note">操作详细表述</param>
        /// <param name="trans">事务对象</param>
        /// <returns></returns>
        public static bool OnOperationLog(Int32 userId, string tableName, string operationType, string note, DbTransaction trans = null)
        {
            //虽然实现了这个事件，但是我们还需要判断该表是否在配置表里面，如果不在，则不记录操作日志。
            OperationLogSettingInfo settingInfo = BLLFactory<OperationLogSetting>.Instance.FindByTableName(tableName, trans);
            if (settingInfo != null)
            {
                bool insert = operationType == "增加" && settingInfo.InsertLog;
                bool update = operationType == "修改" && settingInfo.UpdateLog;
                bool delete = operationType == "删除" && settingInfo.DeleteLog;
                if (insert || update || delete)
                {
                    OperationLogInfo info = new OperationLogInfo();
                    info.TableName = tableName;
                    info.OperationType = operationType;
                    info.Note = note;
                    info.CreateTime = DateTimeHelper.GetServerDateTime2();


                    UserInfo userInfo = BLLFactory<User>.Instance.FindByID(userId, trans);
                    if (userInfo != null)
                    {
                        info.User_ID = userId;
                        info.LoginName = userInfo.Name;
                        info.FullName = userInfo.FullName;
                        info.Company_ID = userInfo.Company_ID;
                        info.CompanyName = userInfo.CompanyName;
                        info.MacAddress = userInfo.CurrentMacAddress;
                        info.IPAddress = userInfo.CurrentLoginIP;
                    }


                    return BLLFactory<OperationLog>.Instance.Insert(info, trans);
                }
            }
            return false;
        }  
    }
}
