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
    /// 用户关键操作记录
    /// </summary>
	public interface IOperationLog : IBaseDAL<OperationLogInfo>
	{
    }
}