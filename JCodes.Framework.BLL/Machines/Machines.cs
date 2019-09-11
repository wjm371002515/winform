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
	public class Machines : BaseBLL<MachinesInfo>
    {
        private IMachines machinesDal;

        public Machines()
            : base()
        {
            base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
            baseDal.OnOperationLog += new OperationLogEventHandler(OperationLog.OnOperationLog);//如果需要记录操作日志，则实现这个事件
        }

        public DataTable GetIntranet(string key)
        {
            // 检查一下key是否含有不合法的字符串
            if (SqlInjection.GetString(key)) return null;

            if (machinesDal == null) machinesDal = baseDal as IMachines;
            return machinesDal.GetIntranet(key);
        }

        public DataTable GetWWIP(string key)
        {
            // 检查一下key是否含有不合法的字符串
            if (SqlInjection.GetString(key)) return null;

            if (machinesDal == null) machinesDal = baseDal as IMachines;
            return machinesDal.GetWWIP(key);
        }

        public DataTable GetJQRT(string key)
        {
            // 检查一下key是否含有不合法的字符串
            if (SqlInjection.GetString(key)) return null;

            if (machinesDal == null) machinesDal = baseDal as IMachines;
            return machinesDal.GetJQRT(key);
        }

        public DataTable GetGLY(string key)
        {
            // 检查一下key是否含有不合法的字符串
            if (SqlInjection.GetString(key)) return null;

            if (machinesDal == null) machinesDal = baseDal as IMachines;
            return machinesDal.GetGLY(key);
        }

        public DataTable GetZG(string key)
        {
            // 检查一下key是否含有不合法的字符串
            if (SqlInjection.GetString(key)) return null;

            if (machinesDal == null) machinesDal = baseDal as IMachines;
            return machinesDal.GetZG(key);
        }


        public List<MachinesInfo> GetMachines(string NWIP, string WWIP, string JQRT, string GLY, string ZG, PagerInfo info)
        {
            if (NWIP != null && SqlInjection.GetString(NWIP)) return null;

            if (WWIP != null && SqlInjection.GetString(WWIP)) return null;

            if (JQRT != null && SqlInjection.GetString(JQRT)) return null;

            if (GLY != null && SqlInjection.GetString(GLY)) return null;

            if (ZG != null && SqlInjection.GetString(ZG)) return null;

            if (machinesDal == null) machinesDal = baseDal as IMachines;

            return machinesDal.GetMachines(NWIP, WWIP, JQRT, GLY, ZG, info);
        }

        public Int32 GetMachinesCount(string NWIP, string WWIP, string JQRT, string GLY, string ZG) {
            if (NWIP != null && SqlInjection.GetString(NWIP)) return -1;

            if (WWIP != null && SqlInjection.GetString(WWIP)) return -1;

            if (JQRT != null && SqlInjection.GetString(JQRT)) return -1;

            if (GLY != null && SqlInjection.GetString(GLY)) return -1;

            if (ZG != null && SqlInjection.GetString(ZG)) return -1;

            if (machinesDal == null) machinesDal = baseDal as IMachines;

            return machinesDal.GetMachinesCount(NWIP, WWIP, JQRT, GLY, ZG);
        }

        public Int32 AddMachine(MachinesInfo machine) {
            if (machinesDal == null) machinesDal = baseDal as IMachines;

            return machinesDal.AddMachine(machine);
        }

        public Int32 ModMachine(MachinesInfo machine){
            if (machinesDal == null) machinesDal = baseDal as IMachines;

            return machinesDal.ModMachine(machine);
        }

    }
}
