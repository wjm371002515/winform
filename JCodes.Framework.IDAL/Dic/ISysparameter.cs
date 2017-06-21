using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using JCodes.Framework.Entity;
using JCodes.Framework.Common;
using JCodes.Framework.Common.Framework;

namespace JCodes.Framework.IDAL
{
	/// <summary>
    /// ISysparameter 的摘要说明。
	/// </summary>
    public interface ISysparameter : IBaseDAL<SysparameterInfo>
	{
        /// <summary>
        /// 根据sysId 查询界面元素
        /// </summary>
        /// <param name="sysId"></param>
        /// <returns></returns>
        List<SysparameterInfo> GetSysparameterBysysId(Int32 sysId);

        /// <summary>
        /// 支持批量更新
        /// </summary>
        /// <param name="info"></param>
        /// <returns>成功更新几条</returns>
        Int32 UpdateSysparameter(List<SysparameterInfo> info);
    }
}