using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;

using JCodes.Framework.Common;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.Common.Winform;
using JCodes.Framework.Common.Files;
using JCodes.Framework.jCodesenum.BaseEnum;

namespace JCodes.Framework.CommonControl.Device
{
    /// <summary>
    /// POS小票打印的打印预览管理界面
    /// </summary>
    public partial class FrmPosPrintPreview : BaseDock
    {
        #region 属性变量
        /// <summary>
        /// 设置待打印的内容
        /// </summary>
        public string PrintString { get; set; }

        /// <summary>
        /// 指定默认的小票打印机名称，用作快速POS打印
        /// </summary>
        public string PrinterName
        {
            get { return m_PrinterName; }
            set 
            {
                this.m_PrinterName = value;
                RefreshPrintSetting();
            }
        }

        /// <summary>
        /// POS打印机的边距,默认为2
        /// </summary>
        public int POSPageMargin
        {
            get { return m_POSPageMargin; }
            set
            {
                this.m_POSPageMargin = value; 
                RefreshPrintSetting();
            }
        }

        /// <summary>
        /// POS打印机默认横向还是纵向，默认设置为纵向(false)
        /// </summary>
        public bool Landscape
        {
            get { return m_Landscape; }
            set 
            { 
                this.m_Landscape = value;
                RefreshPrintSetting();
            }
        }

        private string m_PrinterName = "GP-5860III";//默认的小票打印机名称
        private int m_POSPageMargin = 2;//POS打印机的边距,默认为2
        private bool m_Landscape = false;//POS打印机默认横向还是纵向，默认设置为纵向(false)
        private MultipadPrintDocument _printdocument = new MultipadPrintDocument();
        private Font printFont = new Font("宋体", 9f); 

        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmPosPrintPreview()
        {
            InitializeComponent();
        }

        private void FrmPosPrintPreview_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                //默认从配置里面加载
                AppConfig config = new AppConfig();
                string printer = config.AppConfigGet(Const.SaveConfigName);
                if (!string.IsNullOrEmpty(printer))
                {
                    this.PrinterName = printer;
                }

                RefreshPrintSetting();
            }
        }

        /// <summary>
        /// 刷新打印设置
        /// </summary>
        public void RefreshPrintSetting()
        {
            this.txtContent.Text = PrintString;
            this._printdocument.Text = this.txtContent.Text;
            this._printdocument.Font = printFont;
            this._printdocument.DefaultPageSettings.Landscape = Landscape;
            int posMargin = POSPageMargin;
            this._printdocument.DefaultPageSettings.Margins = new Margins(posMargin, posMargin, posMargin, posMargin);
            this._printdocument.PrinterSettings.PrinterName = PrinterName;
        }

        private void btnPrintSetup_Click(object sender, EventArgs e)
        {
            try
            {
                PageSetupDialog psd = new PageSetupDialog();
                psd.Document = _printdocument;
                psd.PageSettings.Margins = PrinterUnitConvert.Convert(psd.PageSettings.Margins,
                    PrinterUnit.ThousandthsOfAnInch, PrinterUnit.HundredthsOfAMillimeter);

                if (psd.ShowDialog() == DialogResult.OK)
                {
                    _printdocument.Print();
                }
                else
                {
                    psd.PageSettings.Margins = PrinterUnitConvert.Convert(psd.PageSettings.Margins,
                        PrinterUnit.HundredthsOfAMillimeter, PrinterUnit.ThousandthsOfAnInch);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmPosPrintPreview));
                MessageDxUtil.ShowError(ex.Message);
            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                PrintPreviewDialog ppd = new PrintPreviewDialog();
                _printdocument.Text = this.txtContent.Text;
                _printdocument.Font = printFont;
                ppd.Document = _printdocument;
                ppd.ShowDialog();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmPosPrintPreview));
                MessageDxUtil.ShowError(ex.Message);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                PrintDialog pd = new PrintDialog();
                _printdocument.Text = this.txtContent.Text;
                _printdocument.Font = printFont;
                pd.Document = _printdocument;
                if (pd.ShowDialog() == DialogResult.OK)
                {
                    _printdocument.Print();
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmPosPrintPreview));
                MessageDxUtil.ShowError(ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSetPrinterName_Click(object sender, EventArgs e)
        {
            FrmSetPrinterName dlg = new FrmSetPrinterName();
            dlg.DefaultPrinterName = this.PrinterName;
            dlg.SaveConfigName = Const.SaveConfigName;
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.PrinterName = dlg.DefaultPrinterName;
            }
        }
    }
}
