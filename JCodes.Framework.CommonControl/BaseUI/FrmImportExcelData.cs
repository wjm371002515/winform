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
using System.Data.Common;
using JCodes.Framework.Common;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.Common.Files;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.Common.Office;

namespace JCodes.Framework.CommonControl.BaseUI
{
    /// <summary>
    /// 通用Excel数据导入操作
    /// </summary>
    public partial class FrmImportExcelData : BaseForm
    {
        private AppConfig config = new AppConfig();
        private DataSet myDs = new DataSet();
        private BackgroundWorker worker = null;

        /// <summary>
        /// 数据保存的代理定义
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public delegate bool SaveDataHandler(DataRow dr);

        /// <summary>
        /// 数据保存的事件
        /// </summary>
        public event SaveDataHandler OnDataSave;
        /// <summary>
        /// 数据刷新的事件
        /// </summary>
        public event EventHandler OnRefreshData;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public FrmImportExcelData()
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
                if (tips == "操作成功")
                {
                    this.gridControl1.DataSource = null;
                }
            }
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            int itemCount = 0;
            int errorCount = 0;
            if (myDs != null && myDs.Tables[0].Rows.Count > 0)
            {
                //定义步长
                double step = 50 * (1.0 / myDs.Tables[0].Rows.Count);
                DataTable dt = myDs.Tables[0];
                int i = 1;
                try
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (OnDataSave != null)
                        {
                            #region 保存操作，如果有错误，则记录并处理
                            try
                            {
                                bool success = OnDataSave(dr);
                                if (success)
                                {
                                    itemCount++;
                                }
                            }
                            catch (Exception ex)
                            {
                                errorCount++;
                                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmImportExcelData));
                                MessageDxUtil.ShowError(ex.Message);
                            }

                            if (errorCount >= 3)
                            {
                                string message = string.Format("记录导入已经连续出错超过[{0}]条，您是否确定退出导入操作？\r\n单击【是】退出导入，单击【否】忽略错误，继续导入下一记录。", errorCount);
                                if (MessageDxUtil.ShowYesNoAndWarning(message) == System.Windows.Forms.DialogResult.Yes)
                                {
                                    break;
                                }
                                else
                                {
                                    errorCount = 0;//置零重新计算
                                }
                            } 
                            #endregion
                        }

                        int currentStep = Convert.ToInt32(step * i);
                        worker.ReportProgress(currentStep);
                        i++;
                    }

                    if (itemCount == dt.Rows.Count)
                    {
                        e.Result = "操作成功";
                    }
                    else
                    {
                        e.Result = "操作完成，有错误可能未导入全部";
                    }
                }
                catch (Exception ex)
                {
                    e.Result = ex.Message;
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmImportExcelData));
                    MessageDxUtil.ShowError(ex.ToString());
                }
            }
            else
            {
                e.Result = "请检查数据记录是否存在";
            }
        }

        /// <summary>
        /// 设置导入模板标题，及文件路径
        /// </summary>
        /// <param name="title">模板标题</param>
        /// <param name="filePath">模板文件路径</param>
        public void SetTemplate(string title, string filePath)
        {
            this.lnkExcel.Text = title;
            this.lnkExcel.Tag = filePath;
        }

        private void lnkExcel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                string templateFile = this.lnkExcel.Tag.ToString();
                if (string.IsNullOrEmpty(templateFile))
                {
                    MessageDxUtil.ShowTips("导入操作未指定模板文件");
                    return;
                }
                Process.Start(templateFile);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmImportExcelData));
                MessageDxUtil.ShowWarning("文件打开失败:"+ex.Message);
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

        /// <summary>
        /// 查看Excel文件并显示在界面上操作
        /// </summary>
        private void ViewData()
        {
            if (this.txtFilePath.Text == "")
            {
                MessageDxUtil.ShowTips("请选择指定的Excel文件");
                return;
            }
            
            try
            { 
                myDs.Tables.Clear();
                myDs.Clear();
                this.gridControl1.DataSource = null;   

                string error = "";
                AsposeExcelTools.ExcelFileToDataSet(this.txtFilePath.Text, out myDs, out error);
                this.gridControl1.DataSource = myDs.Tables[0];
                this.gridView1.PopulateColumns();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmImportExcelData));
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
                    this.progressBar1.Visible = true;
                    worker.RunWorkerAsync();  
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
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmImportExcelData));
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
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmImportExcelData));
                    MessageDxUtil.ShowError(ex.Message);
                }
            }
            return dt;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 判断字符是否为中午
        /// </summary>
        /// <param name="str_chinese"></param>
        /// <returns></returns>
        public bool IsChinese(string str_chinese)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_chinese, @"[\u4e00-\u9fa5]");
        }
    }
}
