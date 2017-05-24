using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Data.OleDb;
using JCodes.Framework.CommonControl;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;
using JCodes.Framework.BLL;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.Common.Files;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.CommonControl.Other;

namespace JCodes.Framework.AddIn.UI.WareHouse
{
    public partial class FrmImportItemStock : BaseForm
    {
        private AppConfig config = new AppConfig();
        private DataSet myDs = new DataSet();
        private string connectionStringFormat = "Provider = Microsoft.Jet.OLEDB.4.0 ; Data Source = '{0}';Extended Properties=Excel 8.0";
        private BackgroundWorker worker = null;
        public event EventHandler OnRefreshData;

        public FrmImportItemStock()
        {
            InitializeComponent();

            this.gridView1.OptionsBehavior.AutoPopulateColumns = true;
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar1.Value = e.ProgressPercentage;
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.progressBar1.Visible = false;
            this.progressBar1.Value = 0;

            if (OnRefreshData != null)
            {
                OnRefreshData(null, null);
            }

            string tips = e.Result as string;
            if (!string.IsNullOrEmpty(tips))
            {
                MessageDxUtil.ShowTips(tips);
            }
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Dictionary<string, string> WareHouseDict = e.Argument as  Dictionary<string, string>;
            if (WareHouseDict != null)
            {
                int itemCount = InsertItemDetailData(WareHouseDict);
                int stockCount = InsertStockData(WareHouseDict);

                e.Result = string.Format("数据导入成功，其中备件入库或更新：{0}个，库存记录修改：{1}个", itemCount, stockCount);
            }
        }

        /// <summary>
        /// 写入备件明细信息
        /// </summary>
        private int InsertItemDetailData(Dictionary<string, string> WareHouseDict)
        {
            int intOk = 0;
            int intFail = 0;
            bool isFirstError = true;


            if (myDs != null && myDs.Tables[0].Rows.Count > 0)
            {
                double step = 50 * (1.0 / myDs.Tables[0].Rows.Count);    
                DataTable dt = myDs.Tables[0];
                try
                {
                    int i = 1;
                    foreach (DataRow dr in dt.Rows)
                    {
                        #region 单条记录操作代码
                        if (dr[0].ToString() == "")
                        {
                            continue;
                        }

                        string itemNo = dr["备件编号（mm码）"].ToString();
                        string wareHouseKey = dr["库房编号"].ToString();
                        if (string.IsNullOrEmpty(itemNo) || string.IsNullOrEmpty(wareHouseKey))
                            continue;

                        try
                        {
                            string wareHouse = WareHouseDict[wareHouseKey];
                            ItemDetailInfo itemInfo = BLLFactory<ItemDetail>.Instance.FindByItemNo(itemNo);
                            if (itemInfo != null)
                            {
                                #region 更新
                                itemInfo.ItemBigType = dr["备件属类"].ToString();
                                itemInfo.ItemName = dr["备件名称"].ToString();
                                itemInfo.ItemNo = itemNo;
                                itemInfo.ItemType = dr["备件类别"].ToString();
                                itemInfo.MapNo = dr["图号"].ToString();
                                itemInfo.Material = dr["材质"].ToString();
                                try
                                {
                                    itemInfo.Price = Convert.ToDecimal(dr["单价（元）"].ToString());
                                }
                                catch (Exception ex){
                                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmImportItemStock));
                                    MessageDxUtil.ShowError(ex.Message);
                                }
                                itemInfo.Source = dr["来源"].ToString();
                                itemInfo.Specification = dr["规格型号"].ToString();
                                itemInfo.StoragePos = dr["库位"].ToString();
                                itemInfo.Unit = dr["单位"].ToString();
                                itemInfo.UsagePos = dr["使用位置"].ToString();
                                itemInfo.WareHouse = wareHouse;
                                itemInfo.Dept = dr["部门"].ToString();

                                bool success = BLLFactory<ItemDetail>.Instance.Update(itemInfo, itemInfo.ID.ToString());
                                if (success)
                                {
                                    intOk++;
                                }
                                #endregion
                            }
                            else
                            {
                                #region 添加
                                itemInfo = new ItemDetailInfo();
                                itemInfo.ItemBigType = dr["备件属类"].ToString();
                                itemInfo.ItemName = dr["备件名称"].ToString();
                                itemInfo.ItemNo = itemNo;
                                itemInfo.ItemType = dr["备件类别"].ToString();
                                itemInfo.MapNo = dr["图号"].ToString();
                                itemInfo.Material = dr["材质"].ToString();
                                try
                                {
                                    itemInfo.Price = Convert.ToDecimal(dr["单价（元）"].ToString());
                                }
                                catch (Exception ex){
                                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmImportItemStock));
                                    MessageDxUtil.ShowError(ex.Message);
                                }
                                itemInfo.Source = dr["来源"].ToString();
                                itemInfo.Specification = dr["规格型号"].ToString();
                                itemInfo.StoragePos = dr["库位"].ToString();
                                itemInfo.Unit = dr["单位"].ToString();
                                itemInfo.UsagePos = dr["使用位置"].ToString();
                                itemInfo.WareHouse = wareHouse;
                                itemInfo.Dept = dr["部门"].ToString();

                                bool success = BLLFactory<ItemDetail>.Instance.Insert(itemInfo);
                                if (success)
                                {
                                    intOk++;
                                }
                                #endregion
                            }
                        }
                        catch (Exception ex)
                        {
                            intFail++;
                            LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmImportItemStock));
                            if (isFirstError)
                            {
                                MessageDxUtil.ShowError(ex.Message);
                                isFirstError = false;
                            }
                        }
                        #endregion

                        int currentStep = Convert.ToInt32(step * i);
                        worker.ReportProgress(currentStep);
                        i++;
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmImportItemStock));
                    MessageDxUtil.ShowError(ex.Message);
                }
            }
            return intOk;
        }

        /// <summary>
        /// 写入库存信息
        /// </summary>
        private int InsertStockData(Dictionary<string, string> WareHouseDict)
        {
            int intOk = 0;
            int intFail = 0;
            bool isFirstError = true;

            if (myDs != null && myDs.Tables[0].Rows.Count > 0)
            {
                double step = 50 * (1.0 / myDs.Tables[0].Rows.Count);   
                DataTable dt = myDs.Tables[0];
                try
                {
                    int i = 1;
                    foreach (DataRow dr in dt.Rows)
                    {
                        #region 单条记录操作代码
                        if (dr[0].ToString() == "")
                        {
                            continue;
                        }

                        string itemNo = dr["备件编号（mm码）"].ToString();
                        string wareHouseKey = dr["库房编号"].ToString();
                        if (string.IsNullOrEmpty(itemNo) || string.IsNullOrEmpty(wareHouseKey))
                            continue;

                        try
                        {
                            string wareHouse = WareHouseDict[wareHouseKey];
                            StockInfo stockInfo = BLLFactory<Stock>.Instance.FindByItemNo(itemNo, wareHouse);
                            if (stockInfo != null)
                            {
                                #region 更新
                                stockInfo.ItemNo = itemNo;
                                stockInfo.WareHouse = wareHouse;
                                stockInfo.ItemName = dr["备件名称"].ToString();
                                stockInfo.ItemBigType = dr["备件属类"].ToString();
                                stockInfo.ItemType = dr["备件类别"].ToString();
                                int quantity = 0;
                                try
                                {
                                    quantity = Convert.ToInt32(dr["库存量"].ToString());
                                }
                                catch (Exception ex){
                                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmImportItemStock));
                                    MessageDxUtil.ShowError(ex.Message);
                                }
                                stockInfo.StockQuantity = quantity;

                                decimal price = 0M;
                                try
                                {
                                    price = Convert.ToDecimal(dr["单价（元）"].ToString());
                                }
                                catch (Exception ex){
                                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmImportItemStock));
                                    MessageDxUtil.ShowError(ex.Message);
                                }

                                bool success = BLLFactory<Stock>.Instance.Update(stockInfo, stockInfo.ID.ToString());
                                if (success)
                                {
                                    intOk++;
                                }
                                #endregion
                            }
                            else
                            {
                                #region 添加
                                stockInfo = new StockInfo();
                                stockInfo.ItemNo = itemNo;
                                stockInfo.WareHouse = wareHouse;
                                stockInfo.ItemName = dr["备件名称"].ToString();
                                stockInfo.ItemBigType = dr["备件属类"].ToString();
                                stockInfo.ItemType = dr["备件类别"].ToString();

                                int quantity = 0;
                                try
                                {
                                    quantity = Convert.ToInt32(dr["库存量"].ToString());
                                }
                                catch (Exception ex){
                                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmImportItemStock));
                                    MessageDxUtil.ShowError(ex.Message);
                                }
                                stockInfo.StockQuantity = quantity;

                                decimal price = 0M;
                                try
                                {
                                    price = Convert.ToDecimal(dr["单价（元）"].ToString());
                                }
                                catch (Exception ex){
                                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmImportItemStock));
                                    MessageDxUtil.ShowError(ex.Message);
                                }

                                bool success = BLLFactory<Stock>.Instance.Insert(stockInfo);
                                if (success)
                                {
                                    intOk++;
                                }

                                #endregion
                            }
                        }
                        catch (Exception ex)
                        {
                            intFail++;
                            LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmImportItemStock));
                            if (isFirstError)
                            {
                                MessageDxUtil.ShowError(ex.Message);
                                isFirstError = false;
                            }
                        }

                        #endregion

                        int currentStep = Convert.ToInt32(step * i);
                        worker.ReportProgress(50 + currentStep);//50为前面部分进度
                        i++;
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmImportItemStock));
                    MessageDxUtil.ShowError(ex.ToString());
                }
            }
            return intOk;
        }

        private void lnkExcel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                string templateFile = Path.Combine(Application.StartupPath, "备件及库存导入模板.xls");
                Process.Start(templateFile);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmImportItemStock));
                MessageDxUtil.ShowError("文件打开失败:"+ex.Message);
            }
        }

        private void FrmImportExcelData_Load(object sender, EventArgs e)
        {
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            string file = FileDialogHelper.OpenExcel();
            if (!string.IsNullOrEmpty(file))
            {
                this.txtFilePath.Text = file;

                ViewData();
            }
        }

        private void btnViewData_Click(object sender, EventArgs e)
        {
            ViewData();
        }

        private void ViewData()
        {
            if (this.txtFilePath.Text == "")
            {
                MessageDxUtil.ShowTips("请选择指定的Excel文件");
                return;
            }

            string connectString = string.Format(connectionStringFormat, this.txtFilePath.Text);
            string firstSheet = "数据库主表" + "$";// ExcelHelper.GetExcelFirstTableName(connectString);
            try
            {
                myDs.Tables.Clear();
                myDs.Clear();
                OleDbConnection cnnxls = new OleDbConnection(connectString);
                OleDbDataAdapter myDa = new OleDbDataAdapter(string.Format("select * from [{0}]", firstSheet), cnnxls);
                myDa.Fill(myDs, "c");

                gridControl1.DataSource = myDs.Tables[0];
                this.gridView1.PopulateColumns();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmImportItemStock));
                MessageDxUtil.ShowError(ex.Message);
            }
        }

        private void btnSaveData_Click(object sender, EventArgs e)
        {
            if (worker.IsBusy)
                return;

            if (this.txtFilePath.Text == "")
            {
                MessageDxUtil.ShowTips("请选择指定的Excel文件");
                return;
            }

            if (MessageDxUtil.ShowYesNoAndWarning("该操作将把数据导入到系统数据库中，您确定是否继续？") == DialogResult.Yes)
            {
                if (myDs != null && myDs.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = myDs.Tables[0];
                    Dictionary<string, string> WareHouseDict = new Dictionary<string, string>();
                    foreach (DataRow row in dt.Rows)
                    {
                        string wareHouse = row["库房编号"].ToString();
                        if (!WareHouseDict.ContainsKey(wareHouse))
                        {
                            WareHouseDict.Add(wareHouse, "");
                        }
                    }

                    SetWareHouseAlias dlg = new SetWareHouseAlias();
                    dlg.WareHouseDict = WareHouseDict;
                    dlg.InitFunction(this.LoginUserInfo, this.FunctionDict);//初始化权限控制信息

                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        this.progressBar1.Visible = true;
                        WareHouseDict = dlg.WareHouseDict;
                        worker.RunWorkerAsync(WareHouseDict);  
                    }
                }     
            }
        }

        private DateTime? GetDateTime(TextBox tb)
        {
            DateTime? dt = null;
            if (tb.Text.Length > 0)
            {
                try
                {
                    dt = Convert.ToDateTime(tb.Text);
                }
                catch (Exception ex){
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmImportItemStock));
                    MessageDxUtil.ShowError(ex.Message);
                }
            }
            return dt;
        }

        private DateTime? GetDateTime(string text)
        {
            DateTime? dt = null;
            if (!string.IsNullOrEmpty(text))
            {
                try
                {
                    dt = Convert.ToDateTime(text);
                }
                catch (Exception ex){
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmImportItemStock));
                    MessageDxUtil.ShowError(ex.Message);
                }
            }
            return dt;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public bool IsChinese(string str_chinese)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_chinese, @"[\u4e00-\u9fa5]");
        }
    }
}
