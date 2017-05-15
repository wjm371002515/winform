using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;

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
        /// <param name="dictTypeId">字典类型ID</param>
        /// <returns></returns>
        Dictionary<string, string> GetAllType();
                
        /// <summary>
        /// 获取所有字典类型的列表集合(Key为名称，Value为ID值）
        /// </summary>
        /// <param name="PID">字典类型ID</param>
        /// <returns></returns>
        Dictionary<string, string> GetAllType(string PID);

        List<DictTypeNodeInfo> GetTree();

        List<DictTypeInfo> GetTopItems();

        List<DictTypeNodeInfo> GetTreeByID(string mainID);
    }
}