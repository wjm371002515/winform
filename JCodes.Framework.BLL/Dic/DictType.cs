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
        private IDictType dal = null;

        public DictType()
        {
            if (isMultiDatabase)
            {
                base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, dicmultiDatabase[this.GetType().Name].ToString());
            }
            else
            {
                base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
            }

            baseDal.OnOperationLog += new OperationLogEventHandler(OperationLog.OnOperationLog);//如果需要记录操作日志，则实现这个事件

            dal = baseDal as IDictType;
        }

        /// <summary>
        /// 获取所有字典类型的列表集合(Key为名称，Value为ID值）
        /// </summary>
        /// <param name="dictTypeId">字典类型ID</param>
        /// <returns></returns>
        public Dictionary<Int32, string> GetAllType(Int32 dictTypeId)
        {
            return dal.GetAllType(dictTypeId);
        }

        /// <summary>
        /// 判断是否重复，如果重复返回True，否则为False
        /// </summary>
        /// <param name="dictTypeInfo"></param>
        /// <returns></returns>
        public bool CheckDuplicated(Int32 Id)
        {
            string condition = string.Format("(Id == '{0}')", Id);
            DictTypeInfo info = baseDal.FindSingle(condition);
            return (info != null);
        }

        public List<DictTypeNodeInfo> GetTree()
        {
            return dal.GetTree();
        }
    }
}
