using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace JCodes.Framework.Entity
{
    /// <summary>
    /// 采购进货退货方式
    /// </summary>
    //[DataEntity]
    //[Flags]
    public enum PuchaseStatus
    {        
        进货,        
        退货
    }

    /// <summary>
    /// 收入支出类型
    /// </summary>
    public enum IncomeType
    {
        收入,
        支出
    }

    public enum MonthlyReportType
    {
        库房部门结存 = 1,
        库房结存 = 2,
        所有库房结存 = 3,
        车间成本月报表 = 4,
        全年费用汇总表 = 100
    }
}
