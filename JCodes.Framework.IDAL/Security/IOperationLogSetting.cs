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
    /// 记录操作日志的数据表配置
    /// </summary>
	public interface IOperationLogSetting : IBaseDAL<OperationLogSettingInfo>
	{
    }
}