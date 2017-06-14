using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using JCodes.Framework.Common;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.Common.Files;

namespace JCodes.Framework.CommonControl.DocViewer
{
    /// <summary>
    /// WORD控件的测试例子
    /// </summary>
    public partial class FrmWordView : BaseDock
    {
        /// <summary>
        /// 加载流数据
        /// </summary>
        public Stream Stream { get; set; }

        /// <summary>
        /// 文件后缀名，如.doc
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// 文档文件路径。如果指定了该属性，可以不用设置Stream和Extension属性。
        /// </summary>
        public string FilePath { get; set; }

        //记录窗体的名称
        readonly string mainFormText;

        public FrmWordView()
        {
            InitializeComponent();

            //记录窗体的名称，并实现文档变化事件的处理，方便显示新的文件名称
            mainFormText = this.Text;
            this.richEditControl1.DocumentLoaded += new EventHandler(richEditControl1_DocumentLoaded);
        }

        /// <summary>
        /// WORD文档变化后，实现对新文件名称的显示
        /// </summary>
        void richEditControl1_DocumentLoaded(object sender, EventArgs e)
        {
            string fileName = Path.GetFileName(this.richEditControl1.Options.DocumentSaveOptions.CurrentFileName);
            if (String.IsNullOrEmpty(fileName))
            {
                Text = mainFormText;
            }
            else
            {
                Text = fileName + " - " + mainFormText;
            }

            //修改默认字体
            DocumentRange range = richEditControl1.Document.Range;
            CharacterProperties cp = this.richEditControl1.Document.BeginUpdateCharacters(range);
            cp.FontName = "新宋体";
            //cp.FontSize = 12;
            this.richEditControl1.Document.EndUpdateCharacters(cp);
        }

        /// <summary>
        /// 打开WORD文件
        /// </summary>
        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            string filePath = FileDialogHelper.OpenWord();
            if (!string.IsNullOrEmpty(filePath))
            {
                richEditControl1.LoadDocument(filePath);//, DocumentFormat.Doc);
            }
        }

        /// <summary>
        /// 保存WORD文件
        /// </summary>
        private void btnSaveFile_Click(object sender, EventArgs e)
        {
            this.richEditControl1.SaveDocument();
        }

        /// <summary>
        /// 另存为WORD文件
        /// </summary>
        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            try
            {
                richEditControl1.SaveDocumentAs();
                MessageDxUtil.ShowTips("保存成功");
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmWordView));
                MessageDxUtil.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// WORD文件打印
        /// </summary>
        private void btnPreview_Click(object sender, EventArgs e)
        {
            this.richEditControl1.ShowPrintPreview();
        }

        private void FrmWordView_Load(object sender, EventArgs e)
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
                            if (Extension.Equals(".doc", StringComparison.OrdinalIgnoreCase))
                            {
                                this.richEditControl1.LoadDocument(this.Stream, DocumentFormat.Doc);
                            }
                            else if (Extension.Equals(".docx", StringComparison.OrdinalIgnoreCase))
                            {
                                this.richEditControl1.LoadDocument(this.Stream, DocumentFormat.OpenXml);
                            }
                            else if (Extension.Equals(".rtf", StringComparison.OrdinalIgnoreCase))
                            {
                                this.richEditControl1.LoadDocument(this.Stream, DocumentFormat.Rtf);
                            }
                            else
                            {
                                this.richEditControl1.LoadDocument(this.Stream, DocumentFormat.Doc);
                            }
                        }
                        else
                        {
                            this.richEditControl1.LoadDocument(this.Stream, DocumentFormat.Doc);
                        }
                    }
                    catch (Exception ex)
                    {
                        LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmWordView));
                        MessageDxUtil.ShowError(ex.Message);
                    }
 
	                #endregion         
                }
                else if (!string.IsNullOrEmpty(FilePath))
                {
                    this.richEditControl1.LoadDocument(this.FilePath);
                }
            }
        }
    }
}
