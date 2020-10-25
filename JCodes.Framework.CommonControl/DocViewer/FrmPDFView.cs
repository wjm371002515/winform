using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using JCodes.Framework.Common;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.Common.Files;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.jCodesenum;

namespace JCodes.Framework.CommonControl.DocViewer
{
    /// <summary>
    /// PDF测试显示窗体
    /// </summary>
    public partial class FrmPDFView : BaseDock
    {
        /// <summary>
        /// 加载流数据
        /// </summary>
        public Stream Stream { get; set; }

        /// <summary>
        /// 文件后缀名，如.pdf
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// 文档文件路径。如果指定了该属性，可以不用设置Stream和Extension属性。
        /// </summary>
        public string FilePath { get; set; }

        //记录窗体的名称
        readonly string mainFormText;

        public FrmPDFView()
        {
            InitializeComponent();

            //记录窗体的名称，并实现文档变化事件的处理，方便显示新的文件名称
            mainFormText = this.Text;
            pdfViewer1.DocumentChanged += new DevExpress.XtraPdfViewer.PdfDocumentChangedEventHandler(pdfViewer1_DocumentChanged);
        }

        /// <summary>
        /// PDF文档变化后，实现对新文件名称的显示
        /// </summary>
        void pdfViewer1_DocumentChanged(object sender, DevExpress.XtraPdfViewer.PdfDocumentChangedEventArgs e)
        {
            string fileName = Path.GetFileName(e.DocumentFilePath);
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
        /// 打开PDF文件
        /// </summary>
        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            string filePath = FileDialogHelper.OpenPdf();
            if (!string.IsNullOrEmpty(filePath))
            {
                this.pdfViewer1.LoadDocument(filePath);
            }
        }

        /// <summary>
        /// 另存为PDF文件
        /// </summary>
        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            string dir = System.Environment.CurrentDirectory;
            string filePath = FileDialogHelper.SavePdf("", dir);
            if (!string.IsNullOrEmpty(filePath))
            {
                try
                {
                    this.pdfViewer1.SaveDocument(filePath);
                    MessageDxUtil.ShowTips("保存成功");
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmPDFView));
                    MessageDxUtil.ShowError(ex.Message);
                }
            }
        }

        /// <summary>
        /// PDF文件打印
        /// </summary>
        private void btnPreview_Click(object sender, EventArgs e)
        {
            this.pdfViewer1.Print();
        }

        private void FrmPDFView_Load(object sender, EventArgs e)
        {
            //如果文件流不为空，首先根据Stream对象加载文档，否则根据文件路径进行加载
            if (!this.DesignMode)
            {
                if (this.Stream != null)
                {
                    #region MyRegion
                    try
                    {
                        this.pdfViewer1.LoadDocument(this.Stream);
                    }
                    catch (Exception ex)
                    {
                        LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmPDFView));
                        MessageDxUtil.ShowError(ex.Message);
                    } 
                    #endregion
                }
                else if (!string.IsNullOrEmpty(FilePath))
                {
                    this.pdfViewer1.LoadDocument(this.FilePath);
                }
            }
        }
    }
}
