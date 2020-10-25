using System.Collections.Generic;
using JCodes.Framework.IDAL;
using JCodes.Framework.Common.Framework;
using System;
using JCodes.Framework.Entity;

namespace JCodes.Framework.BLL
{
    /// <summary>
    /// 城市业务对象类
    /// </summary>
	public class Consignment : BaseBLL<ConsignmentInfo>
    {
        private IConsignment dal = null;

        public Consignment()
            : base()
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

            dal = baseDal as IConsignment;
        }

       
        /// <summary>
        /// 根据省份名称获取对应的城市列表
        /// </summary>
        /// <param name="provinceName">省份名称</param>
        /// <returns></returns>
        public List<ConsignmentInfo> GetAllConsignmentInfo(string condition)
        {
            return dal.Find(condition);
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool DeleteConsignmentById(Int32 Id)
        {
            return dal.DeleteByCondition(string.Format(" Id = {0}", Id));
        }

        /// <summary>
        /// 修改记录
        /// </summary>
        /// <param name="consignmentInfo"></param>
        /// <returns></returns>
        public Int32 UpdateConsignmentById(ConsignmentInfo consignmentInfo) {
            return dal.UpdateConsignmentById(consignmentInfo);
        }

        public Int32 InsertConsignment(ConsignmentInfo consignmentInfo) {
            return dal.InsertConsignment(consignmentInfo);
        }

        public Int32 GetMaxId() {
            return dal.GetMaxId();
        }
    }
}
