using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using JCodes.Framework.Common;
using JCodes.Framework.Common.Files;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.jCodesenum.BaseEnum;

namespace JCodes.Framework.CommonControl.Device
{
    /// <summary>
    /// 设置打印机名称的窗体
    /// </summary>
    public partial class FrmSetPrinterName : BaseDock
    {
        /// <summary>
        /// 默认的打印机名称
        /// </summary>
        public string DefaultPrinterName { get; set; }

        /// <summary>
        /// 保存的配置名称节点
        /// </summary>
        public string SaveConfigName { get; set; }

        private AppConfig config = new AppConfig();//配置文件操作类

        public FrmSetPrinterName()
        {
            InitializeComponent();
        }

        private void FrmSetPrinterName_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                ListAllPrinterName();
            }
        }

        /// <summary>
        /// 在列表框中列出所有的打印机
        /// </summary>
        private void ListAllPrinterName()
        {
            this.txtPrinter.Properties.BeginUpdate();
            this.txtPrinter.Properties.Items.Clear();
            foreach (String strPrinter in PrinterSettings.InstalledPrinters)
            {
                this.txtPrinter.Properties.Items.Add(strPrinter);
            }
            this.txtPrinter.Properties.EndUpdate();

            //设置选定的默认打印机
            this.txtPrinter.Text = this.DefaultPrinterName;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (this.txtPrinter.Text.Length == 0)
            {
                MessageDxUtil.ShowTips("请选择或输入打印机名称");
                this.txtPrinter.Focus();
                return;
            }

            //保存配置并返回
            try
            {
                config.AppConfigSet(SaveConfigName, this.txtPrinter.Text);

                this.DefaultPrinterName = this.txtPrinter.Text;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmSetPrinterName));
                MessageDxUtil.ShowTips(ex.Message);
            }
        }

    }
}
