using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;
using JCodes.Framework.IDAL;
using JCodes.Framework.Common.Framework;

namespace JCodes.Framework.BLL
{
	public class DictType : BaseBLL<DictTypeInfo>
    {
        public DictType()
        {
            base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
        }

        /// <summary>
        /// 获取所有字典类型的列表集合(Key为名称，Value为ID值）
        /// </summary>
        /// <param name="dictTypeId">字典类型ID</param>
        /// <returns></returns>
        public Dictionary<Int32, string> GetAllType(Int32 dictTypeId)
        {
            IDictType typeDal = baseDal as IDictType;
            return typeDal.GetAllType(dictTypeId);
        }

        /// <summary>
        /// 判断是否重复，如果重复返回True，否则为False
        /// </summary>
        /// <param name="dictTypeInfo"></param>
        /// <returns></returns>
        public bool CheckDuplicated(Int32 ID)
        {
            string condition = string.Format("(id == '{0}')", ID);
            DictTypeInfo info = baseDal.FindSingle(condition);
            return (info != null);
        }

        public List<DictTypeNodeInfo> GetTree()
        {
            IDictType typeDal = baseDal as IDictType;
            return typeDal.GetTree();
        }
    }
}
