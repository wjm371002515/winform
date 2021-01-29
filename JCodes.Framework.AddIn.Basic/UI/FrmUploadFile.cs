using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;
using JCodes.Framework.CommonControl;
using JCodes.Framework.BLL;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.Common.Files;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Common.Databases;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.jCodesenum;
using JCodes.Framework.Common.Format;

namespace JCodes.Framework.AddIn.Basic
{
    public partial class FrmUploadFile : BaseDock
    {
        #region 字段属性

        private BackgroundWorker worker = null;

        /// <summary>
        /// 设置附件的存储目录分类
        /// </summary>
        public AttachmentType AttachmentDirectory = AttachmentType.业务附件;

        /// <summary>
        /// 指定附件对应的GUID
        /// </summary>
        public string AttachmentGid = Guid.NewGuid().ToString();

        /// <summary>
        /// 附件的编辑人
        /// </summary>
        public Int32 UserId = 0;

        /// <summary>
        /// 附件组所属的记录ID，如属于某个主表记录的ID
        /// </summary>
        public Int32 CreatorId = 0;

        private List<string> fileList = new List<string>();
        private Dictionary<string, string> fileStatus = new Dictionary<string, string>();

        #endregion

        public FrmUploadFile()
        {
            InitializeComponent();

            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
            worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);

            SetNotCopyToolTips();
        }

        private void SetNotCopyToolTips()
        {
            ToolTip tip = new ToolTip();
            tip.IsBalloon = true;
            tip.ToolTipIcon = ToolTipIcon.Info;
            tip.ToolTipTitle = "提示";
            tip.UseAnimation = true;
            tip.SetToolTip(this.chkNotCopyFile, "对Winform本地程序来说，有时候文件已经是存在的，且路径已经设置好的情况下，不需要复制文件！");
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar1.EditValue = e.ProgressPercentage;

            BindData();
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.progressBar1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.barTips.Caption = (string)e.Result;
            MessageDxUtil.ShowTips(e.Result.ToString());
            if (e.Result != null && e.Result.ToString() == "上传成功")
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();

                ProcessDataSaved(this.btnUpload, new EventArgs());
            }

            this.btnBrowse.Enabled = true;
            this.btnUpload.Enabled = true;
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                //对Winform本地程序来说，有时候文件已经是存在的，且路径已经设置好，
                //因此设置notCopyFile = true, 不需要复制文件从而导致文件路径变动
                bool notCopyFile = Convert.ToBoolean(e.Argument);

                int step = 0;
                int i = 0;
                string state = "";
                if (fileList.Count > 0)
                {
                    bool sucess = true;
                    string lastError = "";
                    foreach (string file in fileList)
                    {
                        FileUploadInfo info = new FileUploadInfo();
                        if (notCopyFile)
                        {
                            //如果不需要复制文件，那么记录文件的相对路径
                            info.BasePath = Path.GetDirectoryName(file);                            
                        }
                        else
                        {
                            // 常规复制文件，需要记录文件的字节
                            info.FileData = FileUtil.FileToBytes(file);
                        }

                        info.Name = FileUtil.GetFileName(file);
                        info.FileUploadType = (short)AttachmentDirectory;
                        info.FileExtend = FileUtil.GetExtension(file);
                        info.FileSize = FileUtil.GetFileSize(file);
                        info.EditorId = UserId;//登录人
                        info.CreatorId = CreatorId;//所属主表记录ID
                        info.AttachmentGid = AttachmentGid;

                        /*ReturnResult result = BLLFactory<FileUpload>.Instance.Upload(info);
                        if (result.ErrorCode != 0)
                        {
                            sucess = false;
                            lastError = result.ErrorMessage;
                            fileStatus[file] = result.ErrorMessage;
                            state = string.Format("{0}|{1}", file, result.ErrorMessage);
                        }
                        else
                        {
                            fileStatus[file] = "成功";
                            state = string.Format("{0}|成功", file);
                        }*/

                        i++;
                        step = Convert.ToInt32((100.0 / (fileList.Count * 1.0)) * i);
                        worker.ReportProgress(step, state);
                    }
                    e.Result = sucess ? "上传成功" : "操作失败:" + lastError;
                }
            }
            catch (Exception ex)
            {
                e.Result = ex.Message;
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmUploadFile));
                MessageDxUtil.ShowError(ex.Message);
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            string fileString = FileDialogHelper.OpenFile(true);
            if (!string.IsNullOrEmpty(fileString))
            {
                this.txtFile.Text = fileString;

                string[] filesArray = this.txtFile.Text.Split(',');
                if (filesArray != null && filesArray.Length > 0)
                {
                    foreach (string filePath in filesArray)
                    {
                        if (!fileList.Contains(filePath))
                        {
                            fileList.Add(filePath);
                            fileStatus.Add(filePath, "");
                        }
                    }
                    BindData();
                }               
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            #region 检查
            if (fileList.Count == 0)
            {
                MessageDxUtil.ShowTips("请先选择文件");
                this.txtFile.Focus();
                return;
            }
            else if ((short)this.AttachmentDirectory == 0)
            {
                MessageDxUtil.ShowTips("请设置目录分类");
                return;
            }
            #endregion

            if (!worker.IsBusy)
            {            
                this.btnBrowse.Enabled = false;
                this.btnUpload.Enabled = false;
                this.txtFile.Text = "";

                this.progressBar1.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                object argument = this.chkNotCopyFile.Checked;
                worker.RunWorkerAsync(argument);
            }
        }

        private void FrmUploadFile_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                BindData();
            }
        }

        private void BindData()
        {
            DataTable dt = DataTableHelper.CreateTable("序号|int,文件名称,状态");
            int i = 1;
            foreach (string key in fileStatus.Keys)
            {
                DataRow dr = dt.NewRow();
                dr[0] = i++;
                dr[1] = key;
                dr[2] = fileStatus[key];
                dt.Rows.Add(dr);
            }
            this.winGridView1.DataSource = dt;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.txtFile.Text = "";
            fileList.Clear();
            fileStatus.Clear();
            BindData();
        }
    }
}
