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
	/// IDictData 的摘要说明。
	/// </summary>
	public interface IDictData : IBaseDAL<DictDataInfo>
	{             
        /// <summary>
        /// 根据字典类型ID获取所有该类型的字典列表集合
        /// </summary>
        /// <param name="dictTypeId"></param>
        /// <returns></returns>
        List<DictDataInfo> FindByTypeID(string dictTypeId);

        /// <summary>
        /// 根据字典类型名称获取所有该类型的字典列表集合
        /// </summary>
        /// <param name="dictType">字典类型名称</param>
        /// <returns></returns>
        List<DictDataInfo> FindByDictType(string dictTypeName);

               
        /// <summary>
        /// 获取所有的字典列表集合(Key为名称，Value为值）
        /// </summary>
        /// <returns></returns>
        Dictionary<string, string> GetAllDict();

        /// <summary>
        /// 根据字典类型ID获取所有该类型的字典列表集合(Key为名称，Value为值）
        /// </summary>
        /// <param name="dictTypeId">字典类型ID</param>
        /// <returns></returns>
        Dictionary<string, string> GetDictByTypeID(string dictTypeId);

        /// <summary>
        /// 根据字典类型名称获取所有该类型的字典列表集合(Key为名称，Value为值）
        /// </summary>
        /// <param name="dictType">字典类型名称</param>
        /// <returns></returns>
        Dictionary<string, string> GetDictByDictType(string dictTypeName);
        
        /// <summary>
        /// 根据字典类型名称和字典Value值（即字典编码），解析成字典对应的名称
        /// </summary>
        /// <param name="dictTypeName">字典类型名称</param>
        /// <param name="dictValue">字典Value值，即字典编码</param>
        /// <returns>字典对应的名称</returns>
        string GetDictName(string dictTypeName, string dictValue);
    }
}