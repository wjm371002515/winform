using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;
using JCodes.Framework.Common.Framework;

namespace JCodes.Framework.IDAL
{
    /// <summary>
    /// 登陆系统的黑白名单列表
    /// </summary>
    public interface IMachines : IBaseDAL<MachinesInfo>
	{
        DataTable GetIntranet(string key);

        DataTable GetWWIP(string key);

        DataTable GetJQRT(string key);

        DataTable GetGLY(string key);

        DataTable GetZG(string key);

        List<MachinesInfo> GetMachines(string NWIP, string WWIP, string JQRT, string GLY, string ZG, PagerInfo pagerinfo);

        Int32 GetMachinesCount(string NWIP, string WWIP, string JQRT, string GLY, string ZG);

        Int32 AddMachine(MachinesInfo machine);

        Int32 ModMachine(MachinesInfo machine);
    }
}