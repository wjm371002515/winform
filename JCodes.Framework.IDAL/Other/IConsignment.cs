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
    /// IConsignment 的摘要说明。
	/// </summary>
    public interface IConsignment : IBaseDAL<ConsignmentInfo>
	{
        /// <summary>
        /// 修改记录
        /// </summary>
        /// <param name="consignmentInfo"></param>
        /// <returns></returns>
        Int32 UpdateConsignmentById(ConsignmentInfo consignmentInfo);

        Int32 InsertConsignment(ConsignmentInfo consignmentInfo);

        Int32 GetMaxId();
    }
}