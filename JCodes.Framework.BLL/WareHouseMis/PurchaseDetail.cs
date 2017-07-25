using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.IDAL;

namespace JCodes.Framework.BLL
{
	public class PurchaseDetail : BaseBLL<PurchaseDetailInfo>
    {
        public PurchaseDetail() : base()
        {
            base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
        }

        /// <summary>
        /// 获取货单明细内容
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public DataTable GetPurchaseDetailReportByID(int PurchaseHead_ID)
        {
            IPurchaseDetail dal = baseDal as IPurchaseDetail;
            return dal.GetPurchaseDetailReportByID(PurchaseHead_ID);
        }
    }
}
