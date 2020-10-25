using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.IDAL;

namespace JCodes.Framework.BLL
{
    /// <summary>
    /// 中国省份业务对象类
    /// </summary>
	public class Sysparameter : BaseBLL<SysparameterInfo>
    {
        private ISysparameter dal = null;

        public Sysparameter()
            : base()
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

            dal = baseDal as ISysparameter;
        }

        public List<SysparameterInfo> GetSysparameterBysysId(int sysId)
        {
            return dal.GetSysparameterBysysId(sysId);
        }

        public Int32 UpdateSysparameter(List<SysparameterInfo> info)
        {
            return dal.UpdateSysparameter(info);
        }


        /// <summary>
        /// 判断此用户是否为超级管理员
        /// </summary>
        /// <param name="userName">登陆用户名</param>
        /// <returns>true 为超级管理员，false为普通用户</returns>
        public bool UserIsSuperAdmin(String userName)
        {
            string sql = "Id=1 and SysId = 1";
            SysparameterInfo oneSysparameterInfo = dal.FindSingle(sql);
            if (oneSysparameterInfo == null)
                return false;

            return string.Equals(userName, oneSysparameterInfo.SysValue);
        }
    }
}
