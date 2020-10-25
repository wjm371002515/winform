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
	public class ReportMonthlyCostDetail : BaseBLL<ReportMonthlyCostDetailInfo>
    {
        public ReportMonthlyCostDetail() : base()
        {
            base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
        }

        public void DeleteByHeaderID(int headerID)
        {
            string condition = string.Format("Header_ID={0}", headerID);
            baseDal.DeleteByCondition(condition);
        }
    }
}
