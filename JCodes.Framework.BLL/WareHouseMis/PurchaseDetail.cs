using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;
using JCodes.Framework.Common.Framework;

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
            string sql = string.Format(@"Select HandNo,ItemNo,ItemName,MapNo,Specification,Material,
            ItemBigType,ItemType,Unit,Price,Quantity,Amount,Source,StoragePos,UsagePos,d.WareHouse,d.Dept
            From WM_PurchaseDetail d inner join WM_PurchaseHeader h on d.PurchaseHead_ID = h.ID 
            Where h.ID={0} order by CreateDate ", PurchaseHead_ID);
            return SqlTable(sql);
        }
    }
}
