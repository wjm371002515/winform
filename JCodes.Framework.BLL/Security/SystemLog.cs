using JCodes.Framework.IDAL;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Entity;
using JCodes.Framework.Common.Extension;
using System;

namespace JCodes.Framework.BLL
{
	/// <summary>
	/// 系统日志表业务对象类
	/// </summary>
	public class SystemLog : BaseBLL<SystemLogInfo>
	{
		private ISystemLog dal = null;

		public SystemLog() : base()
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

			dal = baseDal as ISystemLog;
		}


        public bool AddSystemLog(SystemLogInfo systemLog) {
            // 过滤掉不再范围内的日志
            Int32 SYSTEM_LOG_LEVEL = config.AppConfigGet("SYSTEM_LOG_LEVEL").ToInt32();
            if (SYSTEM_LOG_LEVEL >= systemLog.LogLevel) {
                return dal.AddSystemLog(systemLog);
            }
            return true;
        }
	}
}