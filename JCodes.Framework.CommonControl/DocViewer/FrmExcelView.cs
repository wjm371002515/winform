using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DevExpress.Spreadsheet;
using JCodes.Framework.Common;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.Common.Files;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.jCodesenum;

namespace JCodes.Framework.CommonControl.DocViewer
{  
    /// <summary>
    /// Excel控件的测试例子
    /// </summary>
    public partial class FrmExcelView : BaseDock
    {    
        /// <summary>
        /// 加载流数据
        /// </summary>
        public Stream Stream { get; set; }

        /// <summary>
        /// 文件后缀名，如.xls
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// 文档文件路径。如果指定了该属性，可以不用设置Stream和Extension属性。
        /// </summary>
        public string FilePath { get; set; }

        //记录窗体的名称
        readonly string mainFormText;

        public FrmExcelView()
        {
            InitializeComponent();

            //记录窗体的名称，并实现文档变化事件的处理，方便显示新的文件名称
            mainFormText = this.Text;
            this.spreadsheetControl1.DocumentLoaded += new EventHandler(spreadsheetControl1_DocumentLoaded);
        }

        /// <summary>
        /// 文档变化后，实现对新文件名称的显示
        /// </summary>
        void spreadsheetControl1_DocumentLoaded(object sender, EventArgs e)
        {
            string fileName = Path.GetFileName(this.spreadsheetControl1.Document.Path);
            if (String.IsNullOrEmpty(fileName))
            {
                Text = mainFormText;
            }
            else
            {
                Text = fileName + " - " + mainFormText;
            }
        }

        /// <summary>
        /// 打开Excel文件
        /// </summary>
        private void btnOpenFile_Click(object sender, EventArgs e)
        { 
            string filePath = FileDialogHelper.OpenExcel();
            if (!string.IsNullOrEmpty(filePath))
            {
                IWorkbook workbook = spreadsheetControl1.Document;
                workbook.LoadDocument(filePath);
            }
        }

        /// <summary>
        /// 保存Excel文件
        /// </summary>
        private void btnSaveFile_Click(object sender, EventArgs e)
        {
            spreadsheetControl1.SaveDocument();
        }

        /// <summary>
        /// 另存为Excel文件
        /// </summary>
        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            string dir = System.Environment.CurrentDirectory;
            string filePath = FileDialogHelper.SaveExcel("", dir);
            if (!string.IsNullOrEmpty(filePath))
            {
                try
                {
                    IWorkbook workbook = spreadsheetControl1.Document;
                    workbook.SaveDocument(filePath);

                    MessageDxUtil.ShowTips("保存成功");
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmExcelView));
                    MessageDxUtil.ShowError(ex.Message);
                }
            }
        }

        /// <summary>
        /// Excel文件打印
        /// </summary>
        private void btnPreview_Click(object sender, EventArgs e)
        {
            this.Close();
            this.spreadsheetControl1.ShowPrintPreview();
        }

        private void FrmExcelView_Load(object sender, EventArgs e)
        {
            //如果文件流不为空，首先根据Stream对象加载文档，否则根据文件路径进行加载
            if (!this.DesignMode)
            {
                if (this.Stream != null)
                {
                    #region MyRegion
                    try
                    {
                        if (!string.IsNullOrEmpty(Extension))
                        {
                            if (Extension.Equals(".xls", StringComparison.OrdinalIgnoreCase))
                            {
                                this.spreadsheetControl1.LoadDocument(this.Stream, DocumentFormat.Xls);
                            }
                            else if (Extension.Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                            {
                                this.spreadsheetControl1.LoadDocument(this.Stream, DocumentFormat.Xlsx);
                            }
                            else if (Extension.Equals(".csv", StringComparison.OrdinalIgnoreCase))
                            {
                                this.spreadsheetControl1.LoadDocument(this.Stream, DocumentFormat.Csv);
                            }
                            else
                            {
                                this.spreadsheetControl1.LoadDocument(this.Stream, DocumentFormat.Xls);
                            }
                        }
                        else
                        {
                            this.spreadsheetControl1.LoadDocument(this.Stream, DocumentFormat.Xls);
                        }
                    }
                    catch (Exception ex)
                    {
                        LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmExcelView));
                        MessageDxUtil.ShowError(ex.Message);
                    } 
                    #endregion
                }
                else if (!string.IsNullOrEmpty(FilePath))
                {
                    this.spreadsheetControl1.LoadDocument(this.FilePath);
                }
            }
        }
    }
}
