using JCodes.Framework.IDAL;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Entity;

namespace JCodes.Framework.BLL
{
	/// <summary>
	/// 系统开发时间轴业务对象类
	/// </summary>
	public class SystemTimeLine : BaseBLL<SystemTimeLineInfo>
	{
		private ISystemTimeLine dal = null;

		public SystemTimeLine() : base()
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

			dal = baseDal as ISystemTimeLine;
		}
	}
}