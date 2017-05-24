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
	public class ReportAnnualCostDetail : BaseBLL<ReportAnnualCostDetailInfo>
    {
        public ReportAnnualCostDetail() : base()
        {
            base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
        }

        public void DeleteByHeaderID(int headerID)
        {
            string condition = string.Format("Header_ID={0}", headerID);
            baseDal.DeleteByCondition(condition);
        }

        /// <summary>
        /// 根据条件获取出库汇总金额
        /// </summary>
        /// <param name="valueType"></param>
        /// <returns></returns>
        public decimal GetItemTypeCostCenterSumMoney(string itemType, string costCenter, int year, int month)
        {
            decimal result = 0M;
            string sql = string.Format(@"Select Sum(Amount) from WM_PurchaseDetail d 
            inner join WM_PurchaseHeader h on d.PurchaseHead_ID = h.ID Where ItemType='{0}' and CostCenter='{1}' ", itemType, costCenter);
            sql += string.Format("and h.OperationType='出库' AND h.CreateYear={0} and h.CreateMonth={1} ", year, month);

            string value = baseDal.SqlValueList(sql);
            if (!string.IsNullOrEmpty(value))
            {
                result = Convert.ToDecimal(value);
            }

            return result;
        }

        /// <summary>
        /// 根据条件获取出库汇总金额
        /// </summary>
        /// <param name="valueType"></param>
        /// <returns></returns>
        public decimal GetItemTypeDeptSumMoney(string itemType, string dept, int year, int month)
        {
            decimal result = 0M;
            string sql = string.Format(@"Select Sum(Amount) from WM_PurchaseDetail d 
            inner join WM_PurchaseHeader h on d.PurchaseHead_ID = h.ID Where ItemType='{0}' and d.Dept='{1}' ", itemType, dept);
            sql += string.Format("and h.OperationType='出库' AND h.CreateYear={0} and h.CreateMonth={1} ", year, month);

            string value = baseDal.SqlValueList(sql);
            if (!string.IsNullOrEmpty(value))
            {
                result = Convert.ToDecimal(value);
            }

            return result;
        }

        /// <summary>
        /// 根据条件获取出库汇总金额
        /// </summary>
        /// <param name="valueType"></param>
        /// <returns></returns>
        public decimal GetDeptSumMoney(string dept, int year, int month)
        {
            decimal result = 0M;
            string sql = string.Format(@"Select Sum(Amount) from WM_PurchaseDetail d 
            inner join WM_PurchaseHeader h on d.PurchaseHead_ID = h.ID Where d.Dept='{0}' ", dept);
            sql += string.Format("and h.OperationType='出库' AND h.CreateYear={0} and h.CreateMonth={1} ", year, month);

            string value = baseDal.SqlValueList(sql);
            if (!string.IsNullOrEmpty(value))
            {
                result = Convert.ToDecimal(value);
            }

            return result;
        }
    }
}
