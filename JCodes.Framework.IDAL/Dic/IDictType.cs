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
	/// IDictType 的摘要说明。
	/// </summary>
	public interface IDictType : IBaseDAL<DictTypeInfo>
	{
        /// <summary>
        /// 获取所有字典类型的列表集合(Key为名称，Value为ID值）
        /// </summary>
        /// <param name="PID">字典类型ID</param>
        /// <returns></returns>
        Dictionary<Int32, string> GetAllType(Int32 PID);

        List<DictTypeNodeInfo> GetTree();
    }
}