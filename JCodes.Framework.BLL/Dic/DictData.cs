using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;
using JCodes.Framework.IDAL;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Common.Office;

namespace JCodes.Framework.BLL
{
	public class DictData : BaseBLL<DictDataInfo>
    {
        public DictData()
        {
            base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
        }
                        
        /// <summary>
        /// 根据字典类型ID获取所有该类型的字典列表集合
        /// </summary>
        /// <param name="dictTypeId"></param>
        /// <returns></returns>
        public List<DictDataInfo> FindByTypeID(Int32 dictTypeId)
        {
            IDictData dal = baseDal as IDictData;
            return dal.FindByTypeID(dictTypeId);
        }
                
        /// <summary>
        /// 获取所有的字典列表集合(Key为名称，Value为值）
        /// </summary>
        /// <returns></returns>
        public List<DicKeyValueInfo> GetAllDict()
        {
            var lst = Cache.Instance["DictData"] as List<DicKeyValueInfo>;
            if (lst != null)
            {
                return lst;
            }
            else 
            {
                IDictData dal = baseDal as IDictData;
                return dal.GetAllDict();
            }
        }

        /// <summary>
        /// 根据字典类型ID获取所有该类型的字典列表集合(Key为名称，Value为值）
        /// </summary>
        /// <param name="dictTypeId">字典类型ID</param>
        /// <returns></returns>
        public List<DicKeyValueInfo> GetDictByTypeID(Int32 dictTypeId)
        {
            List<DicKeyValueInfo> lst = GetAllDict();
            return lst.FindAll(s => s.DicttypeId == dictTypeId);
        }

        public string GetDictName(Int32 dictTypeId, Int32 value)
        {
            List<DicKeyValueInfo> lst = GetAllDict();
            return lst.Find(s => s.DicttypeId == dictTypeId && s.DicttypeValue == value).Name;
        }

    }
}
