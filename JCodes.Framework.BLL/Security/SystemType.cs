using JCodes.Framework.Common;
using JCodes.Framework.Common.Device;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Entity;
using JCodes.Framework.IDAL;
using System;

namespace JCodes.Framework.BLL
{
    /// <summary>
    /// 系统标识信息
    /// </summary>
    public class SystemType : BaseBLL<SystemTypeInfo>
	{
        private ISystemType dal = null;

        /// <summary>
        /// 构造函数
        /// </summary>
		public SystemType() :base()
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

            dal = baseDal as ISystemType;
		}

        public override bool DeleteByUser(object key, Int32 userId, System.Data.Common.DbTransaction trans = null)
        {
            int count = BLLFactory<Role>.Instance.GetRecordCount();
            if (count == 1)
            {
                throw new Exception("系统至少需要保留一个记录！");
            }
            return base.DeleteByUser(key, userId, trans);
        }

        /// <summary>
        /// 根据系统OID获取系统标识信息
        /// </summary>
        /// <param name="gid">系统GID</param>
        /// <returns></returns>
        public SystemTypeInfo FindByGid(string gid)
		{
            return dal.FindById(gid);
		}

        /// <summary>
        /// 获取系统CPU序列号
        /// </summary>
        /// <returns></returns>
		public string GetCPUSerialID()
		{
            return HardwareInfoHelper.GetCPUId();
		}

        /// <summary>
        /// 验证系统是否被授权注册
        /// </summary>
        /// <param name="serialNumber">序列号</param>
        /// <param name="typeID">类型ID</param>
        /// <param name="authorizeAmount">授权数量</param>
        /// <returns></returns>
		public bool VerifySystem(string serialNumber, string typeID, Int32 authorizeAmount)
		{
            return dal.VerifySystem(serialNumber, typeID, authorizeAmount);
		}
	}
}