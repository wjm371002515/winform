using JCodes.Framework.BLL;
using JCodes.Framework.Common;
using JCodes.Framework.CommonControl;
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
    public partial class FrmAnnualSummary : BaseForm
    {
        private BackgroundWorker annualWorker;

        public FrmAnnualSummary()
        {
            InitializeComponent();

            annualWorker = new BackgroundWorker();
            annualWorker.DoWork += new DoWorkEventHandler(annualWorker_DoWork);
            annualWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(annualWorker_RunWorkerCompleted);
            annualWorker.WorkerReportsProgress = true;
            annualWorker.WorkerSupportsCancellation = true;
            annualWorker.ProgressChanged += new ProgressChangedEventHandler(annualWorker_ProgressChanged);
        }

        #region 年度结算操作
        private void annualWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar.EditValue = e.ProgressPercentage;
        }

        private void annualWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageDxUtil.ShowTips("年度汇总操作顺利完成！");
            this.btnOK.Enabled = true;
            this.progressBar.Visible = false;
        }

        private ReportAnnualCostHeaderInfo GetAnnualMainHeader()
        {
            ReportAnnualCostHeaderInfo headerInfo = new ReportAnnualCostHeaderInfo();
            headerInfo.CreateDate = DateTime.Now;
            headerInfo.Creator = LoginUserInfo.FullName;
            headerInfo.ReportYear = DateTime.Now.Year;
            return headerInfo;
        }

        private void ExecuteAnnualCostReport()
        {
            #region 构造全年费用汇总报表
            CListItem[] itemTypeArray = DictItemUtil.GetDictByDictType("备件类别");
            CListItem[] costCenterArray = DictItemUtil.GetDictByDictType("成本中心");
            CListItem[] deptArray = DictItemUtil.GetDictByDictType("部门");
            ReportAnnualCostHeaderInfo annualHeaderInfo = GetAnnualMainHeader();
            annualHeaderInfo.ReportTitle = string.Format("{0}年费用汇总表", DateTime.Now.Year);
            annualHeaderInfo.ReportType = 100;
            int headerID = BLLFactory<ReportAnnualCostHeader>.Instance.InsertOrUpdate(annualHeaderInfo);
            if (headerID > 0)
            {
                annualWorker.ReportProgress(10);

                //先删除当年汇总的数据，防止重复写入
                BLLFactory<ReportAnnualCostDetail>.Instance.DeleteByHeaderID(headerID);
                annualWorker.ReportProgress(20);

                #region 备件类型-成本中心汇总
                //重新写入记录
                int i = 1;
                int j = 1;
                foreach (CListItem itemTypeItem in itemTypeArray)
                {
                    //合计项目
                    ReportAnnualCostDetailInfo totalInfo = new ReportAnnualCostDetailInfo();
                    totalInfo.Header_ID = headerID;
                    totalInfo.ReportYear = DateTime.Now.Year;
                    totalInfo.ItemType = itemTypeItem.Value;
                    totalInfo.CostCenterOrDept = string.Format("{0} 汇总", itemTypeItem.Value);
                    totalInfo.ReportCode = "001";

                    foreach (CListItem costCenterItem in costCenterArray)
                    {
                        #region 十二个月的记录
                        ReportAnnualCostDetailInfo detailInfo = new ReportAnnualCostDetailInfo();
                        detailInfo.Header_ID = headerID;
                        detailInfo.ReportYear = DateTime.Now.Year;
                        detailInfo.ItemType = itemTypeItem.Value;
                        detailInfo.CostCenterOrDept = costCenterItem.Value;
                        detailInfo.One = BLLFactory<ReportAnnualCostDetail>.Instance.GetItemTypeCostCenterSumMoney(itemTypeItem.Value, costCenterItem.Value, DateTime.Now.Year, 1);
                        detailInfo.Two = BLLFactory<ReportAnnualCostDetail>.Instance.GetItemTypeCostCenterSumMoney(itemTypeItem.Value, costCenterItem.Value, DateTime.Now.Year, 2);
                        detailInfo.Three = BLLFactory<ReportAnnualCostDetail>.Instance.GetItemTypeCostCenterSumMoney(itemTypeItem.Value, costCenterItem.Value, DateTime.Now.Year, 3);
                        detailInfo.Four = BLLFactory<ReportAnnualCostDetail>.Instance.GetItemTypeCostCenterSumMoney(itemTypeItem.Value, costCenterItem.Value, DateTime.Now.Year, 4);
                        detailInfo.Five = BLLFactory<ReportAnnualCostDetail>.Instance.GetItemTypeCostCenterSumMoney(itemTypeItem.Value, costCenterItem.Value, DateTime.Now.Year, 5);
                        detailInfo.Six = BLLFactory<ReportAnnualCostDetail>.Instance.GetItemTypeCostCenterSumMoney(itemTypeItem.Value, costCenterItem.Value, DateTime.Now.Year, 6);
                        detailInfo.Seven = BLLFactory<ReportAnnualCostDetail>.Instance.GetItemTypeCostCenterSumMoney(itemTypeItem.Value, costCenterItem.Value, DateTime.Now.Year, 7);
                        detailInfo.Eight = BLLFactory<ReportAnnualCostDetail>.Instance.GetItemTypeCostCenterSumMoney(itemTypeItem.Value, costCenterItem.Value, DateTime.Now.Year, 8);
                        detailInfo.Nine = BLLFactory<ReportAnnualCostDetail>.Instance.GetItemTypeCostCenterSumMoney(itemTypeItem.Value, costCenterItem.Value, DateTime.Now.Year, 9);
                        detailInfo.Ten = BLLFactory<ReportAnnualCostDetail>.Instance.GetItemTypeCostCenterSumMoney(itemTypeItem.Value, costCenterItem.Value, DateTime.Now.Year, 10);
                        detailInfo.Eleven = BLLFactory<ReportAnnualCostDetail>.Instance.GetItemTypeCostCenterSumMoney(itemTypeItem.Value, costCenterItem.Value, DateTime.Now.Year, 11);
                        detailInfo.Twelve = BLLFactory<ReportAnnualCostDetail>.Instance.GetItemTypeCostCenterSumMoney(itemTypeItem.Value, costCenterItem.Value, DateTime.Now.Year, 12);
                        detailInfo.Total = detailInfo.One + detailInfo.Two + detailInfo.Three + detailInfo.Four
                            + detailInfo.Five + detailInfo.Six + detailInfo.Seven + detailInfo.Eight + detailInfo.Nine
                            + detailInfo.Ten + detailInfo.Eleven + detailInfo.Twelve;
                        detailInfo.ReportCode = "001";

                        BLLFactory<ReportAnnualCostDetail>.Instance.Insert(detailInfo);

                        //合计项目的累积
                        totalInfo.One += detailInfo.One;
                        totalInfo.Two += detailInfo.Two;
                        totalInfo.Three += detailInfo.Three;
                        totalInfo.Four += detailInfo.Four;
                        totalInfo.Five += detailInfo.Five;
                        totalInfo.Six += detailInfo.Six;
                        totalInfo.Seven += detailInfo.Seven;
                        totalInfo.Eight += detailInfo.Eight;
                        totalInfo.Nine += detailInfo.Nine;
                        totalInfo.Ten += detailInfo.Ten;
                        totalInfo.Eleven += detailInfo.Eleven;
                        totalInfo.Twelve += detailInfo.Twelve;
                        totalInfo.Total += detailInfo.Total;
                        j++;
                        #endregion
                    }

                    BLLFactory<ReportAnnualCostDetail>.Instance.Insert(totalInfo);
                    i++;
                }
                annualWorker.ReportProgress(50);
                #endregion

                #region 备件类型-部门汇总
                i = 1;
                j = 1;
                foreach (CListItem itemTypeItem in itemTypeArray)
                {
                    //合计项目
                    ReportAnnualCostDetailInfo totalInfo = new ReportAnnualCostDetailInfo();
                    totalInfo.Header_ID = headerID;
                    totalInfo.ReportYear = DateTime.Now.Year;
                    totalInfo.ItemType = itemTypeItem.Value;
                    totalInfo.CostCenterOrDept = string.Format("{0} 汇总", itemTypeItem.Value);
                    totalInfo.ReportCode = "002";

                    foreach (CListItem deptItem in deptArray)
                    {
                        #region 十二个月纪录
                        ReportAnnualCostDetailInfo detailInfo = new ReportAnnualCostDetailInfo();
                        detailInfo.Header_ID = headerID;
                        detailInfo.ReportYear = DateTime.Now.Year;
                        detailInfo.ItemType = itemTypeItem.Value;
                        detailInfo.CostCenterOrDept = deptItem.Value;
                        detailInfo.One = BLLFactory<ReportAnnualCostDetail>.Instance.GetItemTypeDeptSumMoney(itemTypeItem.Value, deptItem.Value, DateTime.Now.Year, 1);
                        detailInfo.Two = BLLFactory<ReportAnnualCostDetail>.Instance.GetItemTypeDeptSumMoney(itemTypeItem.Value, deptItem.Value, DateTime.Now.Year, 2);
                        detailInfo.Three = BLLFactory<ReportAnnualCostDetail>.Instance.GetItemTypeDeptSumMoney(itemTypeItem.Value, deptItem.Value, DateTime.Now.Year, 3);
                        detailInfo.Four = BLLFactory<ReportAnnualCostDetail>.Instance.GetItemTypeDeptSumMoney(itemTypeItem.Value, deptItem.Value, DateTime.Now.Year, 4);
                        detailInfo.Five = BLLFactory<ReportAnnualCostDetail>.Instance.GetItemTypeDeptSumMoney(itemTypeItem.Value, deptItem.Value, DateTime.Now.Year, 5);
                        detailInfo.Six = BLLFactory<ReportAnnualCostDetail>.Instance.GetItemTypeDeptSumMoney(itemTypeItem.Value, deptItem.Value, DateTime.Now.Year, 6);
                        detailInfo.Seven = BLLFactory<ReportAnnualCostDetail>.Instance.GetItemTypeDeptSumMoney(itemTypeItem.Value, deptItem.Value, DateTime.Now.Year, 7);
                        detailInfo.Eight = BLLFactory<ReportAnnualCostDetail>.Instance.GetItemTypeDeptSumMoney(itemTypeItem.Value, deptItem.Value, DateTime.Now.Year, 8);
                        detailInfo.Nine = BLLFactory<ReportAnnualCostDetail>.Instance.GetItemTypeDeptSumMoney(itemTypeItem.Value, deptItem.Value, DateTime.Now.Year, 9);
                        detailInfo.Ten = BLLFactory<ReportAnnualCostDetail>.Instance.GetItemTypeDeptSumMoney(itemTypeItem.Value, deptItem.Value, DateTime.Now.Year, 10);
                        detailInfo.Eleven = BLLFactory<ReportAnnualCostDetail>.Instance.GetItemTypeDeptSumMoney(itemTypeItem.Value, deptItem.Value, DateTime.Now.Year, 11);
                        detailInfo.Twelve = BLLFactory<ReportAnnualCostDetail>.Instance.GetItemTypeDeptSumMoney(itemTypeItem.Value, deptItem.Value, DateTime.Now.Year, 12);
                        detailInfo.Total = detailInfo.One + detailInfo.Two + detailInfo.Three + detailInfo.Four
                            + detailInfo.Five + detailInfo.Six + detailInfo.Seven + detailInfo.Eight + detailInfo.Nine
                            + detailInfo.Ten + detailInfo.Eleven + detailInfo.Twelve;
                        detailInfo.ReportCode = "002";

                        BLLFactory<ReportAnnualCostDetail>.Instance.Insert(detailInfo);

                        //合计项目的累积
                        totalInfo.One += detailInfo.One;
                        totalInfo.Two += detailInfo.Two;
                        totalInfo.Three += detailInfo.Three;
                        totalInfo.Four += detailInfo.Four;
                        totalInfo.Five += detailInfo.Five;
                        totalInfo.Six += detailInfo.Six;
                        totalInfo.Seven += detailInfo.Seven;
                        totalInfo.Eight += detailInfo.Eight;
                        totalInfo.Nine += detailInfo.Nine;
                        totalInfo.Ten += detailInfo.Ten;
                        totalInfo.Eleven += detailInfo.Eleven;
                        totalInfo.Twelve += detailInfo.Twelve;
                        totalInfo.Total += detailInfo.Total;

                        j++;
                        #endregion
                    }

                    BLLFactory<ReportAnnualCostDetail>.Instance.Insert(totalInfo);
                    i++;
                }
                annualWorker.ReportProgress(80);
                #endregion

                #region 特殊部门分月汇总
                i = 1;
                j = 1;

                //合计项目
                ReportAnnualCostDetailInfo totalInfo2 = new ReportAnnualCostDetailInfo();
                totalInfo2.Header_ID = headerID;
                totalInfo2.ReportYear = DateTime.Now.Year;
                totalInfo2.CostCenterOrDept = string.Format("{0} 汇总", "功能性承包");
                totalInfo2.ReportCode = "003";
                foreach (CListItem deptItem in deptArray)
                {
                    #region 十二个月纪录
                    ReportAnnualCostDetailInfo detailInfo = new ReportAnnualCostDetailInfo();
                    detailInfo.Header_ID = headerID;
                    detailInfo.ReportYear = DateTime.Now.Year;
                    detailInfo.ItemType = "";
                    detailInfo.CostCenterOrDept = deptItem.Value;
                    detailInfo.One = BLLFactory<ReportAnnualCostDetail>.Instance.GetDeptSumMoney(deptItem.Value, DateTime.Now.Year, 1);
                    detailInfo.Two = BLLFactory<ReportAnnualCostDetail>.Instance.GetDeptSumMoney(deptItem.Value, DateTime.Now.Year, 2);
                    detailInfo.Three = BLLFactory<ReportAnnualCostDetail>.Instance.GetDeptSumMoney(deptItem.Value, DateTime.Now.Year, 3);
                    detailInfo.Four = BLLFactory<ReportAnnualCostDetail>.Instance.GetDeptSumMoney(deptItem.Value, DateTime.Now.Year, 4);
                    detailInfo.Five = BLLFactory<ReportAnnualCostDetail>.Instance.GetDeptSumMoney(deptItem.Value, DateTime.Now.Year, 5);
                    detailInfo.Six = BLLFactory<ReportAnnualCostDetail>.Instance.GetDeptSumMoney(deptItem.Value, DateTime.Now.Year, 6);
                    detailInfo.Seven = BLLFactory<ReportAnnualCostDetail>.Instance.GetDeptSumMoney(deptItem.Value, DateTime.Now.Year, 7);
                    detailInfo.Eight = BLLFactory<ReportAnnualCostDetail>.Instance.GetDeptSumMoney(deptItem.Value, DateTime.Now.Year, 8);
                    detailInfo.Nine = BLLFactory<ReportAnnualCostDetail>.Instance.GetDeptSumMoney(deptItem.Value, DateTime.Now.Year, 9);
                    detailInfo.Ten = BLLFactory<ReportAnnualCostDetail>.Instance.GetDeptSumMoney(deptItem.Value, DateTime.Now.Year, 10);
                    detailInfo.Eleven = BLLFactory<ReportAnnualCostDetail>.Instance.GetDeptSumMoney(deptItem.Value, DateTime.Now.Year, 11);
                    detailInfo.Twelve = BLLFactory<ReportAnnualCostDetail>.Instance.GetDeptSumMoney(deptItem.Value, DateTime.Now.Year, 12);
                    detailInfo.Total = detailInfo.One + detailInfo.Two + detailInfo.Three + detailInfo.Four
                            + detailInfo.Five + detailInfo.Six + detailInfo.Seven + detailInfo.Eight + detailInfo.Nine
                            + detailInfo.Ten + detailInfo.Eleven + detailInfo.Twelve;
                    detailInfo.ReportCode = "003";

                    BLLFactory<ReportAnnualCostDetail>.Instance.Insert(detailInfo);

                    //合计项目的累积
                    totalInfo2.One += detailInfo.One;
                    totalInfo2.Two += detailInfo.Two;
                    totalInfo2.Three += detailInfo.Three;
                    totalInfo2.Four += detailInfo.Four;
                    totalInfo2.Five += detailInfo.Five;
                    totalInfo2.Six += detailInfo.Six;
                    totalInfo2.Seven += detailInfo.Seven;
                    totalInfo2.Eight += detailInfo.Eight;
                    totalInfo2.Nine += detailInfo.Nine;
                    totalInfo2.Ten += detailInfo.Ten;
                    totalInfo2.Eleven += detailInfo.Eleven;
                    totalInfo2.Twelve += detailInfo.Twelve;
                    totalInfo2.Total += detailInfo.Total;

                    i++;
                    #endregion
                }
                BLLFactory<ReportAnnualCostDetail>.Instance.Insert(totalInfo2);
                #endregion
            }
            #endregion
        }

        private void annualWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            ExecuteAnnualCostReport();
        }

        #endregion

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (MessageDxUtil.ShowYesNoAndTips("您是否需要执行年度汇总操作？\r\n年度汇总可能会比较耗时，任务执行过程中请勿退出。") == DialogResult.Yes)
            {
                if (!annualWorker.IsBusy)
                {
                    this.btnOK.Enabled = false;//不能重复操作
                    this.progressBar.Visible = true;

                    annualWorker.RunWorkerAsync();//开始任务
                }
            }
        }
    }
}
