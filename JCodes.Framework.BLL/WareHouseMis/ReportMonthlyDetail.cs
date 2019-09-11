using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Common.Format;
using JCodes.Framework.jCodesenum;

namespace JCodes.Framework.BLL
{
	public class ReportMonthlyDetail : BaseBLL<ReportMonthlyDetailInfo>
    {
        public ReportMonthlyDetail() : base()
        {
            base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
        }

        /// <summary>
        /// 删除指定表头ID的所有明细项目
        /// </summary>
        /// <param name="headerID"></param>
        public void DeleteByHeaderID(int headerID)
        {
            string condition = string.Format("Header_ID={0}", headerID);
            baseDal.DeleteByCondition(condition);
        }

        public DataTable GetReportDetail(string headerID)
        {
            string sql = string.Format(@"Select Select [LastCount] as LC, [LastMoney] as LM, [CurrentInCount] as CIC, [CurrentInMoney] as CIM, 
                           [CurrentOutCount] as COC, [CurrentOutMoney] as COM, [CurrentCount] as CC, [CurrentMoney] as CM
                           from WM_ReportMonthlyDetail Where Header_ID='{0}' order by ID", headerID);
            return SqlTable(sql);
        }

        /// <summary>
        /// 根据类型获取部门的汇总数量
        /// </summary>
        /// <param name="valueType"></param>
        /// <returns></returns>
        public int GetDeptCount(StatisticValueType valueType, string deptName)
        {
            int result = 0;
            string sql = string.Format("Select Sum(Quantity) from WM_PurchaseDetail d inner join WM_PurchaseHeader h on d.PurchaseHead_ID = h.ID Where d.Dept='{0}' ", deptName);
            if (valueType == StatisticValueType.CurrentCount)
            {
                sql += string.Format(" AND h.CreateYear={0} and h.CreateMonth={1} ", DateTimeHelper.GetServerDateTime2().Year, DateTimeHelper.GetServerDateTime2().Month);
            }
            else if (valueType == StatisticValueType.CurrentInCount)
            {
                sql += string.Format(" AND h.CreateYear={0} and h.CreateMonth={1} and h.OperationType='入库' ", DateTimeHelper.GetServerDateTime2().Year, DateTimeHelper.GetServerDateTime2().Month);
            }
            else if (valueType == StatisticValueType.CurrentOutCount)
            {
                sql += string.Format(" AND h.CreateYear={0} and h.CreateMonth={1} and h.OperationType='出库' ", DateTimeHelper.GetServerDateTime2().Year, DateTimeHelper.GetServerDateTime2().Month);
            }
            else if (valueType == StatisticValueType.LastCount)
            {
                DateTime lastMonth = DateTimeHelper.GetServerDateTime2().AddMonths(-1);
                sql += string.Format(" AND h.CreateYear={0} and h.CreateMonth={1} ", lastMonth.Year, lastMonth.Month);
            }

            string value = baseDal.SqlValueList(sql);
            if (!string.IsNullOrEmpty(value))
            {
                result = Convert.ToInt32(value);
            }

            return result;
        }

        /// <summary>
        /// 根据类型获取部门的汇总金额
        /// </summary>
        /// <param name="valueType"></param>
        /// <returns></returns>
        public decimal GetDeptMoney(StatisticValueType valueType, string deptName)
        {
            decimal result = 0M;
            string sql = string.Format("Select Sum(Amount) from WM_PurchaseDetail d inner join WM_PurchaseHeader h on d.PurchaseHead_ID = h.ID Where d.Dept='{0}' ", deptName);
            if (valueType == StatisticValueType.CurrentMoney)
            {
                sql += string.Format(" AND h.CreateYear={0} and h.CreateMonth={1} ", DateTimeHelper.GetServerDateTime2().Year, DateTimeHelper.GetServerDateTime2().Month);
            }
            else if (valueType == StatisticValueType.CurrentInMoney)
            {
                sql += string.Format(" AND h.CreateYear={0} and h.CreateMonth={1} and h.OperationType='入库' ", DateTimeHelper.GetServerDateTime2().Year, DateTimeHelper.GetServerDateTime2().Month);
            }
            else if (valueType == StatisticValueType.CurrentOutMoney)
            {
                sql += string.Format(" AND h.CreateYear={0} and h.CreateMonth={1} and h.OperationType='出库' ", DateTimeHelper.GetServerDateTime2().Year, DateTimeHelper.GetServerDateTime2().Month);
            }
            else if (valueType == StatisticValueType.LastMoney)
            {
                DateTime lastMonth = DateTimeHelper.GetServerDateTime2().AddMonths(-1);
                sql += string.Format(" AND h.CreateYear={0} and h.CreateMonth={1} ", lastMonth.Year, lastMonth.Month);
            }

            string value = baseDal.SqlValueList(sql);
            if (!string.IsNullOrEmpty(value))
            {
                result = Convert.ToDecimal(value);
            }

            return result;
        }

        /// <summary>
        /// 根据类型获取各库房的汇总数量
        /// </summary>
        /// <param name="valueType"></param>
        /// <returns></returns>
        public int GetEachWareCount(StatisticValueType valueType, string wareHouseName)
        {
            int result = 0;
            string sql = string.Format("Select Sum(Quantity) from WM_PurchaseDetail d inner join WM_PurchaseHeader h on d.PurchaseHead_ID = h.ID Where d.WareHouse='{0}' ", wareHouseName);
            if (valueType == StatisticValueType.CurrentCount)
            {
                sql += string.Format(" AND h.CreateYear={0} and h.CreateMonth={1} ", DateTimeHelper.GetServerDateTime2().Year, DateTimeHelper.GetServerDateTime2().Month);
            }
            else if (valueType == StatisticValueType.CurrentInCount)
            {
                sql += string.Format(" AND h.CreateYear={0} and h.CreateMonth={1} and h.OperationType='入库' ", DateTimeHelper.GetServerDateTime2().Year, DateTimeHelper.GetServerDateTime2().Month);
            }
            else if (valueType == StatisticValueType.CurrentOutCount)
            {
                sql += string.Format(" AND h.CreateYear={0} and h.CreateMonth={1} and h.OperationType='出库' ", DateTimeHelper.GetServerDateTime2().Year, DateTimeHelper.GetServerDateTime2().Month);
            }
            else if (valueType == StatisticValueType.LastCount)
            {
                DateTime lastMonth = DateTimeHelper.GetServerDateTime2().AddMonths(-1);
                sql += string.Format(" AND h.CreateYear={0} and h.CreateMonth={1} ", lastMonth.Year, lastMonth.Month);
            }

            string value = baseDal.SqlValueList(sql);
            if (!string.IsNullOrEmpty(value))
            {
                result = Convert.ToInt32(value);
            }

            return result;
        }

        /// <summary>
        /// 根据类型获取各库房的汇总金额
        /// </summary>
        /// <param name="valueType"></param>
        /// <returns></returns>
        public decimal GetEachWareMoney(StatisticValueType valueType, string wareHouseName)
        {
            decimal result = 0M;
            string sql = string.Format("Select Sum(Amount) from WM_PurchaseDetail d inner join WM_PurchaseHeader h on d.PurchaseHead_ID = h.ID Where d.WareHouse='{0}' ", wareHouseName);
            if (valueType == StatisticValueType.CurrentMoney)
            {
                sql += string.Format(" AND h.CreateYear={0} and h.CreateMonth={1} ", DateTimeHelper.GetServerDateTime2().Year, DateTimeHelper.GetServerDateTime2().Month);
            }
            else if (valueType == StatisticValueType.CurrentInMoney)
            {
                sql += string.Format(" AND h.CreateYear={0} and h.CreateMonth={1} and h.OperationType='入库' ", DateTimeHelper.GetServerDateTime2().Year, DateTimeHelper.GetServerDateTime2().Month);
            }
            else if (valueType == StatisticValueType.CurrentOutMoney)
            {
                sql += string.Format(" AND h.CreateYear={0} and h.CreateMonth={1} and h.OperationType='出库' ", DateTimeHelper.GetServerDateTime2().Year, DateTimeHelper.GetServerDateTime2().Month);
            }
            else if (valueType == StatisticValueType.LastMoney)
            {
                DateTime lastMonth = DateTimeHelper.GetServerDateTime2().AddMonths(-1);
                sql += string.Format(" AND h.CreateYear={0} and h.CreateMonth={1} ", lastMonth.Year, lastMonth.Month);
            }

            string value = baseDal.SqlValueList(sql);
            if (!string.IsNullOrEmpty(value))
            {
                result = Convert.ToDecimal(value);
            }

            return result;
        }

        /// <summary>
        /// 根据类型获取所有库房的备件类别的汇总数量
        /// </summary>
        /// <param name="valueType"></param>
        /// <returns></returns>
        public int GetWareItemTypeCount(StatisticValueType valueType, string itemType)
        {
            int result = 0;
            string sql = string.Format("Select Sum(Quantity) from WM_PurchaseDetail d inner join WM_PurchaseHeader h on d.PurchaseHead_ID = h.ID Where ItemType='{0}' ", itemType);
            if (valueType == StatisticValueType.CurrentCount)
            {
                sql += string.Format(" AND h.CreateYear={0} and h.CreateMonth={1} ", DateTimeHelper.GetServerDateTime2().Year, DateTimeHelper.GetServerDateTime2().Month);
            }
            else if (valueType == StatisticValueType.CurrentInCount)
            {
                sql += string.Format(" AND h.CreateYear={0} and h.CreateMonth={1} and h.OperationType='入库' ", DateTimeHelper.GetServerDateTime2().Year, DateTimeHelper.GetServerDateTime2().Month);
            }
            else if (valueType == StatisticValueType.CurrentOutCount)
            {
                sql += string.Format(" AND h.CreateYear={0} and h.CreateMonth={1} and h.OperationType='出库' ", DateTimeHelper.GetServerDateTime2().Year, DateTimeHelper.GetServerDateTime2().Month);
            }
            else if (valueType == StatisticValueType.LastCount)
            {
                DateTime lastMonth = DateTimeHelper.GetServerDateTime2().AddMonths(-1);
                sql += string.Format(" AND h.CreateYear={0} and h.CreateMonth={1} ", lastMonth.Year, lastMonth.Month);
            }

            string value = baseDal.SqlValueList(sql);
            if (!string.IsNullOrEmpty(value))
            {
                result = Convert.ToInt32(value);
            }

            return result;
        }

        /// <summary>
        /// 根据类型获取所有库房的备件类别的汇总金额
        /// </summary>
        /// <param name="valueType"></param>
        /// <returns></returns>
        public decimal GetWareItemTypeMoney(StatisticValueType valueType, string itemType)
        {
            decimal result = 0M;
            string sql = string.Format("Select Sum(Amount) from WM_PurchaseDetail d inner join WM_PurchaseHeader h on d.PurchaseHead_ID = h.ID Where ItemType='{0}' ", itemType);
            if (valueType == StatisticValueType.CurrentMoney)
            {
                sql += string.Format(" AND h.CreateYear={0} and h.CreateMonth={1} ", DateTimeHelper.GetServerDateTime2().Year, DateTimeHelper.GetServerDateTime2().Month);
            }
            else if (valueType == StatisticValueType.CurrentInMoney)
            {
                sql += string.Format(" AND h.CreateYear={0} and h.CreateMonth={1} and h.OperationType='入库' ", DateTimeHelper.GetServerDateTime2().Year, DateTimeHelper.GetServerDateTime2().Month);
            }
            else if (valueType == StatisticValueType.CurrentOutMoney)
            {
                sql += string.Format(" AND h.CreateYear={0} and h.CreateMonth={1} and h.OperationType='出库' ", DateTimeHelper.GetServerDateTime2().Year, DateTimeHelper.GetServerDateTime2().Month);
            }
            else if (valueType == StatisticValueType.LastMoney)
            {
                DateTime lastMonth = DateTimeHelper.GetServerDateTime2().AddMonths(-1);
                sql += string.Format(" AND h.CreateYear={0} and h.CreateMonth={1} ", lastMonth.Year, lastMonth.Month);
            }

            string value = baseDal.SqlValueList(sql);
            if (!string.IsNullOrEmpty(value))
            {
                result = Convert.ToDecimal(value);
            }

            return result;
        }

        /// <summary>
        /// 根据类型获取所有库房的备件属类的汇总数量
        /// </summary>
        /// <param name="valueType"></param>
        /// <returns></returns>
        public int GetWareItemBigTypeCount(StatisticValueType valueType, string itemBigType)
        {
            int result = 0;
            string sql = string.Format("Select Sum(Quantity) from WM_PurchaseDetail d inner join WM_PurchaseHeader h on d.PurchaseHead_ID = h.ID Where ItemBigType='{0}' ", itemBigType);
            if (valueType == StatisticValueType.CurrentCount)
            {
                sql += string.Format(" AND h.CreateYear={0} and h.CreateMonth={1} ", DateTimeHelper.GetServerDateTime2().Year, DateTimeHelper.GetServerDateTime2().Month);
            }
            else if (valueType == StatisticValueType.CurrentInCount)
            {
                sql += string.Format(" AND h.CreateYear={0} and h.CreateMonth={1} and h.OperationType='入库' ", DateTimeHelper.GetServerDateTime2().Year, DateTimeHelper.GetServerDateTime2().Month);
            }
            else if (valueType == StatisticValueType.CurrentOutCount)
            {
                sql += string.Format(" AND h.CreateYear={0} and h.CreateMonth={1} and h.OperationType='出库' ", DateTimeHelper.GetServerDateTime2().Year, DateTimeHelper.GetServerDateTime2().Month);
            }
            else if (valueType == StatisticValueType.LastCount)
            {
                DateTime lastMonth = DateTimeHelper.GetServerDateTime2().AddMonths(-1);
                sql += string.Format(" AND h.CreateYear={0} and h.CreateMonth={1} ", lastMonth.Year, lastMonth.Month);
            }

            string value = baseDal.SqlValueList(sql);
            if (!string.IsNullOrEmpty(value))
            {
                result = Convert.ToInt32(value);
            }

            return result;
        }

        /// <summary>
        /// 根据类型获取所有库房的备件属类的汇总金额
        /// </summary>
        /// <param name="valueType"></param>
        /// <returns></returns>
        public decimal GetWareItemBigTypeMoney(StatisticValueType valueType, string itemBigType)
        {
            decimal result = 0M;
            string sql = string.Format("Select Sum(Amount) from WM_PurchaseDetail d inner join WM_PurchaseHeader h on d.PurchaseHead_ID = h.ID Where ItemBigType='{0}' ", itemBigType);
            if (valueType == StatisticValueType.CurrentMoney)
            {
                sql += string.Format(" AND h.CreateYear={0} and h.CreateMonth={1} ", DateTimeHelper.GetServerDateTime2().Year, DateTimeHelper.GetServerDateTime2().Month);
            }
            else if (valueType == StatisticValueType.CurrentInMoney)
            {
                sql += string.Format(" AND h.CreateYear={0} and h.CreateMonth={1} and h.OperationType='入库' ", DateTimeHelper.GetServerDateTime2().Year, DateTimeHelper.GetServerDateTime2().Month);
            }
            else if (valueType == StatisticValueType.CurrentOutMoney)
            {
                sql += string.Format(" AND h.CreateYear={0} and h.CreateMonth={1} and h.OperationType='出库' ", DateTimeHelper.GetServerDateTime2().Year, DateTimeHelper.GetServerDateTime2().Month);
            }
            else if (valueType == StatisticValueType.LastMoney)
            {
                DateTime lastMonth = DateTimeHelper.GetServerDateTime2().AddMonths(-1);
                sql += string.Format(" AND h.CreateYear={0} and h.CreateMonth={1} ", lastMonth.Year, lastMonth.Month);
            }

            string value = baseDal.SqlValueList(sql);
            if (!string.IsNullOrEmpty(value))
            {
                result = Convert.ToDecimal(value);
            }

            return result;
        }

        /// <summary>
        /// 根据条件获取成本中心每月的费用
        /// </summary>
        /// <param name="itemType">备件类型</param>
        /// <param name="costCenter">成本中心</param>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <returns></returns>
        public decimal GetPartMonthlyCost(string deptName, string itemType, int year, int month)
        {
            decimal result = 0M;
            string sql = string.Format(@"Select Sum(Amount) from WM_PurchaseDetail d inner join WM_PurchaseHeader h on d.PurchaseHead_ID = h.ID 
                Where h.OperationType='出库' and ItemType='{0}' and d.Dept='{1}' and h.CreateYear={2} and h.CreateMonth={3}", itemType, deptName, year, month);
            string value = baseDal.SqlValueList(sql);
            if (!string.IsNullOrEmpty(value))
            {
                result = Convert.ToDecimal(value);
            }

            return result;
        }
    }
}
