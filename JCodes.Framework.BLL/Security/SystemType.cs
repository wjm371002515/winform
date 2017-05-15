using JCodes.Framework.Common;
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
		private ISystemType systemTypeDal;

        /// <summary>
        /// 构造函数
        /// </summary>
		public SystemType() :base()
        {
            base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
			this.systemTypeDal = (ISystemType) baseDal;
		}

        public override bool Delete(object key, System.Data.Common.DbTransaction trans = null)
        {
            int count = BLLFactory<Role>.Instance.GetRecordCount();
            if (count == 1)
            {
                throw new Exception("系统至少需要保留一个记录！");
            }
            return base.Delete(key, trans);
        }

        /// <summary>
        /// 根据系统OID获取系统标识信息
        /// </summary>
        /// <param name="oid">系统OID</param>
        /// <returns></returns>
		public SystemTypeInfo FindByOID(string oid)
		{
			return this.systemTypeDal.FindByOID(oid);
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
		public bool VerifySystem(string serialNumber, string typeID, int authorizeAmount)
		{
			return this.systemTypeDal.VerifySystem(serialNumber, typeID, authorizeAmount);
		}
	}
}