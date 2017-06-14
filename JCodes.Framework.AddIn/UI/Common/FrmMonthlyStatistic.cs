using JCodes.Framework.BLL;
using JCodes.Framework.Common;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.CommonControl;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JCodes.Framework.AddIn.UI.Common
{
    public partial class FrmMonthlyStatistic : BaseDock
    {      
        //月结线程
        private BackgroundWorker worker;

        public FrmMonthlyStatistic()
        {
            InitializeComponent();

            worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
        }

        #region 月结线程操作

        /// <summary>
        /// 构造月结报表表头相同部分
        /// </summary>
        /// <returns></returns>
        private ReportMonthlyHeaderInfo GetMainHeader()
        {
            ReportMonthlyHeaderInfo headerInfo = new ReportMonthlyHeaderInfo();
            headerInfo.CreateDate = DateTime.Now;
            headerInfo.Creator = LoginUserInfo.FullName;
            headerInfo.ReportMonth = DateTime.Now.Month;
            headerInfo.ReportYear = DateTime.Now.Year;
            headerInfo.YearMonth = DateTime.Now.ToString("yyyy年MM月");
            return headerInfo;
        }

        private void ExecuteDeptMonthReport()
        {
            /*#region 构造库房部门结存月结报表
            CListItem[] deptArray = DictItemUtil.GetDictByDictType("部门");
            ReportMonthlyHeaderInfo deptHeaderInfo = GetMainHeader();
            deptHeaderInfo.ReportTitle = string.Format("{0}部门结存月报表", DateTime.Now.ToString("yyyy年MM月"));
            deptHeaderInfo.ReportType = 1;
            int headerID = BLLFactory<ReportMonthlyHeader>.Instance.InsertOrUpdate(deptHeaderInfo);
            if (headerID > 0)
            {
                worker.ReportProgress(10);
                int i = 1;

                //先删除当月月结的数据，防止重复写入
                BLLFactory<ReportMonthlyDetail>.Instance.DeleteByHeaderID(headerID);

                //重新写入记录
                foreach (CListItem deptItem in deptArray)
                {
                    ReportMonthlyDetailInfo detailInfo = new ReportMonthlyDetailInfo();
                    detailInfo.Header_ID = headerID;
                    detailInfo.ReportYear = DateTime.Now.Year;
                    detailInfo.ReportMonth = DateTime.Now.Month;
                    detailInfo.YearMonth = DateTime.Now.ToString("yyyy年MM月");
                    detailInfo.ItemName = deptItem.Value;//项目名称为部门名称
                    detailInfo.CurrentCount = BLLFactory<ReportMonthlyDetail>.Instance.GetDeptCount(StatisticValueType.CurrentCount, deptItem.Value);
                    detailInfo.CurrentInCount = BLLFactory<ReportMonthlyDetail>.Instance.GetDeptCount(StatisticValueType.CurrentInCount, deptItem.Value);
                    detailInfo.CurrentInMoney = BLLFactory<ReportMonthlyDetail>.Instance.GetDeptMoney(StatisticValueType.CurrentInMoney, deptItem.Value);
                    detailInfo.CurrentMoney = BLLFactory<ReportMonthlyDetail>.Instance.GetDeptMoney(StatisticValueType.CurrentMoney, deptItem.Value);
                    detailInfo.CurrentOutCount = BLLFactory<ReportMonthlyDetail>.Instance.GetDeptCount(StatisticValueType.CurrentOutCount, deptItem.Value);
                    detailInfo.CurrentOutMoney = BLLFactory<ReportMonthlyDetail>.Instance.GetDeptMoney(StatisticValueType.CurrentOutMoney, deptItem.Value); ;
                    detailInfo.LastCount = BLLFactory<ReportMonthlyDetail>.Instance.GetDeptCount(StatisticValueType.LastCount, deptItem.Value);
                    detailInfo.LastMoney = BLLFactory<ReportMonthlyDetail>.Instance.GetDeptMoney(StatisticValueType.LastMoney, deptItem.Value); ;
                    BLLFactory<ReportMonthlyDetail>.Instance.Insert(detailInfo);

                    i++;
                }
                worker.ReportProgress(20);
            }
            #endregion*/
        }

        private void ExecuteEachWareMonthlyReport()
        {
            #region 构造库房结存月报表（单库房）
            List<WareHouseInfo> wareList = BLLFactory<WareHouses>.Instance.GetAll();
            ReportMonthlyHeaderInfo eachWareHeaderInfo = GetMainHeader();
            eachWareHeaderInfo.ReportTitle = string.Format("{0}库房结存月报表", DateTime.Now.ToString("yyyy年MM月"));
            eachWareHeaderInfo.ReportType = 2;
            int headerID = BLLFactory<ReportMonthlyHeader>.Instance.InsertOrUpdate(eachWareHeaderInfo);
            if (headerID > 0)
            {
                worker.ReportProgress(30);
                int i = 1;

                //先删除当月月结的数据，防止重复写入
                BLLFactory<ReportMonthlyDetail>.Instance.DeleteByHeaderID(headerID);

                //重新写入记录
                foreach (WareHouseInfo wareInfo in wareList)
                {
                    ReportMonthlyDetailInfo detailInfo = new ReportMonthlyDetailInfo();
                    detailInfo.Header_ID = headerID;
                    detailInfo.ReportYear = DateTime.Now.Year;
                    detailInfo.ReportMonth = DateTime.Now.Month;
                    detailInfo.YearMonth = DateTime.Now.ToString("yyyy年MM月");
                    detailInfo.ItemName = wareInfo.Name;//项目名称为库房名称
                    detailInfo.CurrentCount = BLLFactory<ReportMonthlyDetail>.Instance.GetEachWareCount(StatisticValueType.CurrentCount, wareInfo.Name);
                    detailInfo.CurrentInCount = BLLFactory<ReportMonthlyDetail>.Instance.GetEachWareCount(StatisticValueType.CurrentInCount, wareInfo.Name);
                    detailInfo.CurrentInMoney = BLLFactory<ReportMonthlyDetail>.Instance.GetEachWareMoney(StatisticValueType.CurrentInMoney, wareInfo.Name);
                    detailInfo.CurrentMoney = BLLFactory<ReportMonthlyDetail>.Instance.GetEachWareMoney(StatisticValueType.CurrentMoney, wareInfo.Name);
                    detailInfo.CurrentOutCount = BLLFactory<ReportMonthlyDetail>.Instance.GetEachWareCount(StatisticValueType.CurrentOutCount, wareInfo.Name);
                    detailInfo.CurrentOutMoney = BLLFactory<ReportMonthlyDetail>.Instance.GetEachWareMoney(StatisticValueType.CurrentOutMoney, wareInfo.Name); ;
                    detailInfo.LastCount = BLLFactory<ReportMonthlyDetail>.Instance.GetEachWareCount(StatisticValueType.LastCount, wareInfo.Name);
                    detailInfo.LastMoney = BLLFactory<ReportMonthlyDetail>.Instance.GetEachWareMoney(StatisticValueType.LastMoney, wareInfo.Name); ;

                    BLLFactory<ReportMonthlyDetail>.Instance.Insert(detailInfo);

                    i++;
                }
                worker.ReportProgress(40);
            }
            #endregion
        }

        private void ExecuteAllWareItemTypeMonthlyReport()
        {
            /*#region 构造所有库房结存月结报表(含备件属类、备件类别分类）
            CListItem[] itemBigTypeArray = DictItemUtil.GetDictByDictType("备件属类");
            CListItem[] itemTypeArray = DictItemUtil.GetDictByDictType("备件类别");
            ReportMonthlyHeaderInfo allWareHeaderInfo = GetMainHeader();
            allWareHeaderInfo.ReportTitle = string.Format("{0}库房结存月报表", DateTime.Now.ToString("yyyy年MM月"));
            allWareHeaderInfo.ReportType = 3;
            int headerID = BLLFactory<ReportMonthlyHeader>.Instance.InsertOrUpdate(allWareHeaderInfo);
            if (headerID > 0)
            {
                worker.ReportProgress(50);
                int i = 1;

                //先删除当月月结的数据，防止重复写入
                BLLFactory<ReportMonthlyDetail>.Instance.DeleteByHeaderID(headerID);

                //重新写入记录
                foreach (CListItem bigItem in itemBigTypeArray)
                {
                    ReportMonthlyDetailInfo detailInfo = new ReportMonthlyDetailInfo();
                    detailInfo.Header_ID = headerID;
                    detailInfo.ReportYear = DateTime.Now.Year;
                    detailInfo.ReportMonth = DateTime.Now.Month;
                    detailInfo.YearMonth = DateTime.Now.ToString("yyyy年MM月");
                    detailInfo.ItemName = bigItem.Value;//项目名称为部门名称
                    detailInfo.CurrentCount = BLLFactory<ReportMonthlyDetail>.Instance.GetWareItemBigTypeCount(StatisticValueType.CurrentCount, bigItem.Value);
                    detailInfo.CurrentInCount = BLLFactory<ReportMonthlyDetail>.Instance.GetWareItemBigTypeCount(StatisticValueType.CurrentInCount, bigItem.Value);
                    detailInfo.CurrentInMoney = BLLFactory<ReportMonthlyDetail>.Instance.GetWareItemBigTypeMoney(StatisticValueType.CurrentInMoney, bigItem.Value);
                    detailInfo.CurrentMoney = BLLFactory<ReportMonthlyDetail>.Instance.GetWareItemBigTypeMoney(StatisticValueType.CurrentMoney, bigItem.Value);
                    detailInfo.CurrentOutCount = BLLFactory<ReportMonthlyDetail>.Instance.GetWareItemBigTypeCount(StatisticValueType.CurrentOutCount, bigItem.Value);
                    detailInfo.CurrentOutMoney = BLLFactory<ReportMonthlyDetail>.Instance.GetWareItemBigTypeMoney(StatisticValueType.CurrentOutMoney, bigItem.Value); ;
                    detailInfo.LastCount = BLLFactory<ReportMonthlyDetail>.Instance.GetWareItemBigTypeCount(StatisticValueType.LastCount, bigItem.Value);
                    detailInfo.LastMoney = BLLFactory<ReportMonthlyDetail>.Instance.GetWareItemBigTypeMoney(StatisticValueType.LastMoney, bigItem.Value); ;

                    detailInfo.ReportCode = "ItemBigType";//备件属类数据用001表示
                    BLLFactory<ReportMonthlyDetail>.Instance.Insert(detailInfo);

                    i++;
                }

                i = 0;
                foreach (CListItem itemType in itemTypeArray)
                {
                    ReportMonthlyDetailInfo detailInfo = new ReportMonthlyDetailInfo();
                    detailInfo.Header_ID = headerID;
                    detailInfo.ReportYear = DateTime.Now.Year;
                    detailInfo.ReportMonth = DateTime.Now.Month;
                    detailInfo.YearMonth = DateTime.Now.ToString("yyyy年MM月");
                    detailInfo.ItemName = itemType.Value;//项目名称为部门名称
                    detailInfo.CurrentCount = BLLFactory<ReportMonthlyDetail>.Instance.GetWareItemTypeCount(StatisticValueType.CurrentCount, itemType.Value);
                    detailInfo.CurrentInCount = BLLFactory<ReportMonthlyDetail>.Instance.GetWareItemTypeCount(StatisticValueType.CurrentInCount, itemType.Value);
                    detailInfo.CurrentInMoney = BLLFactory<ReportMonthlyDetail>.Instance.GetWareItemTypeMoney(StatisticValueType.CurrentInMoney, itemType.Value);
                    detailInfo.CurrentMoney = BLLFactory<ReportMonthlyDetail>.Instance.GetWareItemTypeMoney(StatisticValueType.CurrentMoney, itemType.Value);
                    detailInfo.CurrentOutCount = BLLFactory<ReportMonthlyDetail>.Instance.GetWareItemTypeCount(StatisticValueType.CurrentOutCount, itemType.Value);
                    detailInfo.CurrentOutMoney = BLLFactory<ReportMonthlyDetail>.Instance.GetWareItemTypeMoney(StatisticValueType.CurrentOutMoney, itemType.Value); ;
                    detailInfo.LastCount = BLLFactory<ReportMonthlyDetail>.Instance.GetWareItemTypeCount(StatisticValueType.LastCount, itemType.Value);
                    detailInfo.LastMoney = BLLFactory<ReportMonthlyDetail>.Instance.GetWareItemTypeMoney(StatisticValueType.LastMoney, itemType.Value); ;

                    detailInfo.ReportCode = "ItemType";//备件类别数据用002表示
                    BLLFactory<ReportMonthlyDetail>.Instance.Insert(detailInfo);

                    i++;
                }
                worker.ReportProgress(60);
            }
            #endregion*/
        }

        private void ExecuteEachPartCostMonthlyReport()
        {
            /*#region 构造各车间成本月报表
            CListItem[] deptArray = DictItemUtil.GetDictByDictType("部门");
            CListItem[] itemTypeArray = DictItemUtil.GetDictByDictType("备件类别");
            ReportMonthlyHeaderInfo deptCostHeaderInfo = GetMainHeader();
            deptCostHeaderInfo.ReportTitle = string.Format("{0}各车间成本月报表", DateTime.Now.ToString("yyyy年MM月"));
            deptCostHeaderInfo.ReportType = 4;
            int headerID = BLLFactory<ReportMonthlyHeader>.Instance.InsertOrUpdate(deptCostHeaderInfo);
            if (headerID > 0)
            {
                worker.ReportProgress(70);
                int i = 1;
                int j = 1;

                //先删除当月月结的数据，防止重复写入
                BLLFactory<ReportMonthlyCostDetail>.Instance.DeleteByHeaderID(headerID);

                //重新写入记录
                foreach (CListItem deptItem in deptArray)
                {
                    foreach (CListItem itemType in itemTypeArray)
                    {
                        ReportMonthlyCostDetailInfo detailInfo = new ReportMonthlyCostDetailInfo();
                        detailInfo.Header_ID = headerID;
                        detailInfo.ReportYear = DateTime.Now.Year;
                        detailInfo.ReportMonth = DateTime.Now.Month;
                        detailInfo.YearMonth = DateTime.Now.ToString("yyyy年MM月");
                        detailInfo.DeptName = deptItem.Value;
                        detailInfo.ItemType = itemType.Value;
                        detailInfo.TotalMoney = BLLFactory<ReportMonthlyDetail>.Instance.GetPartMonthlyCost(deptItem.Value, itemType.Value, DateTime.Now.Year, DateTime.Now.Month);
                        //detailInfo.ReportCode = "";//
                        BLLFactory<ReportMonthlyCostDetail>.Instance.Insert(detailInfo);

                        j++;
                    }
                    i++;
                }

                worker.ReportProgress(90);
            }
            #endregion*/
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar.EditValue = e.ProgressPercentage;
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            ExecuteDeptMonthReport();

            ExecuteEachWareMonthlyReport();

            ExecuteAllWareItemTypeMonthlyReport();

            ExecuteEachPartCostMonthlyReport();
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageDxUtil.ShowTips("月结操作顺利完成！");
            this.btnOK.Enabled = true;
            this.progressBar.Visible = false;
        }

        #endregion

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (MessageDxUtil.ShowYesNoAndTips("您是否需要执行月结？\r\n月结可能会比较耗时，任务执行过程中请勿退出。") == DialogResult.Yes)
            {
                if (!worker.IsBusy)
                {
                    this.btnOK.Enabled = false;//不能重复月结操作
                    this.progressBar.Visible = true;

                    worker.RunWorkerAsync();//开始任务
                }
            }
        }
    }
}
