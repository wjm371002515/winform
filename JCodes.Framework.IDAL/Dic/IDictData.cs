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
	/// IDictData 的摘要说明。
	/// </summary>
	public interface IDictData : IBaseDAL<DictDataInfo>
	{             
        /// <summary>
        /// 根据字典类型ID获取所有该类型的字典列表集合
        /// </summary>
        /// <param name="dictTypeId"></param>
        /// <returns></returns>
        List<DictDataInfo> FindByTypeID(Int32 dictTypeId);
               
        /// <summary>
        /// 获取所有的字典列表集合(Key为名称，Value为值）
        /// </summary>
        /// <returns></returns>
        List<DicKeyValueInfo> GetAllDict();
    }
}