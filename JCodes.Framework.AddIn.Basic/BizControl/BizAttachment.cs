using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.BLL;
using JCodes.Framework.CommonControl;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.CommonControl.Pager;
using JCodes.Framework.Common.Files;
using JCodes.Framework.Common.Format;
using JCodes.Framework.CommonControl.Other.Images;
using JCodes.Framework.CommonControl.DocViewer;
using JCodes.Framework.Common.Images;

namespace JCodes.Framework.AddIn.Basic.BizControl
{
    public partial class BizAttachment : DevExpress.XtraEditors.XtraUserControl
    {
        #region public属性变量

        /// <summary>
        /// 附件组所属的记录ID，如属于某个主表记录的ID
        /// </summary>
        public Int32 CreatorId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public Int32 UserId { get; set; }

        /// <summary>
        /// 设置附件的存储目录分类
        /// </summary>
        public string AttachmentDirectory
        {
            get { return m_AttachmentDirectory; }
            set { m_AttachmentDirectory = value; }
        }

        [Browsable(true), Description("设置附件组的GUID")]
        public string AttachmentGid
        {
            get { return m_AttachmentGid; }
            set
            {
                m_AttachmentGid = value;
                BindData();
            }
        }

        /// <summary>
        /// 是否显示上传按钮
        /// </summary>
        public bool ShowUpload
        {
            get { return m_ShowUpload; }
            set
            {
                m_ShowUpload = value;
                this.btnUpload.Visible = value;
            }
        }

        /// <summary>
        /// 是否显示删除按钮
        /// </summary>
        public bool ShowDelete
        {
            get { return m_ShowDelete; }
            set
            {
                m_ShowDelete = value;
                this.btnDelete.Visible = value;
            }
        }

        /// <summary>
        /// 是否使用Owner的方式对数据进行过滤，如果为True，则获取属于CreatorId的所有记录
        /// </summary>
        public bool ShowOwnerData { get; set; }

        #endregion

        /// <summary>
        /// 附件列表中文件后缀名图片列表（自动从本地读取加载）
        /// </summary>
        private Dictionary<string, int> imageDict = new Dictionary<string, int>();
        private bool m_ShowUpload = true;// 是否显示上传按钮
        private bool m_ShowDelete = true;// 是否显示删除按钮
        private string m_AttachmentDirectory = "业务附件";// 设置附件的存储目录分类
        private string m_AttachmentGid;// 设置附件组的GUID

        public BizAttachment()
        {
            InitializeComponent();

            //this.pager1.PageSize = 20;
            this.pager1.PageChanged += new PageChangedEventHandler(pager1_PageChanged);
            ClearData();
        }

        /// <summary>
        /// 初始化相关参数
        /// </summary>
        /// <param name="attachmentDir">设置附件的存储目录分类</param>
        /// <param name="creatorId">附件组所属的记录ID，如属于某个主表记录的ID</param>
        /// <param name="userId">操作用户ID，当前登录用户</param>
        public void Init(string attachmentDir, Int32 creatorId, Int32 userId)
        {
            this.AttachmentDirectory = attachmentDir;
            this.CreatorId = creatorId;
            this.UserId = userId;
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            FrmUploadFile dlg = new FrmUploadFile();
            dlg.UserId = this.UserId;
            dlg.CreatorId = this.CreatorId;
            dlg.AttachmentDirectory = this.AttachmentDirectory;
            dlg.AttachmentGid = this.AttachmentGid;
            dlg.OnDataSaved += new EventHandler(dlg_OnDataSaved);
            dlg.Show();
        }

        void dlg_OnDataSaved(object sender, EventArgs e)
        {
            BindData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.listView1.CheckedItems.Count == 0)
            {
                MessageDxUtil.ShowTips("请勾选删除的文件！");
                return;
            }

            string lastError = "";
            bool sucess = false;
            foreach (ListViewItem item in this.listView1.CheckedItems)
            {
                if (item != null && item.Tag != null)
                {
                    string id = item.Tag.ToString();
                    try
                    {
                        sucess = BLLFactory<FileUpload>.Instance.DeleteByUser(id, UserId);
                    }
                    catch (Exception ex)
                    {
                        LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(BizAttachment));
                        lastError += ex.Message + "\r\n";
                    }
                }
            }
            MessageDxUtil.ShowTips(sucess ? "删除操作成功" : "操作失败:" + lastError);

            BindData();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            if (this.listView1.CheckedItems.Count == 0)
            {
                MessageDxUtil.ShowTips("请勾选下载的文件！");
                return;
            }
            StringBuilder sb = new StringBuilder();
            string path = FileDialogHelper.OpenDir();
            if (!string.IsNullOrEmpty(path))
            {
                DirectoryUtil.AssertDirExist(Path.GetDirectoryName(path));

                #region 下载保存图片
                bool hasError = false;

                foreach (ListViewItem item in this.listView1.CheckedItems)
                {
                    if (item != null && item.Tag != null)
                    {
                        string id = item.Tag.ToString();

                        try
                        {
                            FileUploadInfo fileInfo = BLLFactory<FileUpload>.Instance.Download(id);
                            if (fileInfo != null && fileInfo.FileData != null)
                            {
                                string filePath = Path.Combine(path, fileInfo.Name);
                                FileUtil.CreateFile(filePath, fileInfo.FileData);
                            }
                        }
                        catch (Exception ex)
                        {
                            hasError = true;
                            LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(BizAttachment));
                            sb.Append(ex.Message + "\r\n");
                        }
                    }
                }
                #endregion

                if (hasError)
                {
                    MessageDxUtil.ShowError(sb.ToString());
                }
                else
                {
                    System.Diagnostics.Process.Start(path);
                }
            }
        }

        private void BizAttachment_Load(object sender, EventArgs e)
        {
        }

        void pager1_PageChanged(object sender, EventArgs e)
        {
            BindData();
        }

        private void ClearData()
        {
            this.listView1.CheckBoxes = chkSelect.Checked;
            this.listView1.Items.Clear();
            this.imageList1.Images.Clear();
            this.imageList2.Images.Clear(); 
            imageDict.Clear();//刷新需要清除           
        }

        public void BindData()
        {
            ClearData();

            if (!this.DesignMode)
            {
                List<FileUploadInfo> fileList = new List<FileUploadInfo>();
                if (ShowOwnerData)
                {
                    if (!string.IsNullOrEmpty(this.AttachmentGid))
                    {
                        fileList = BLLFactory<FileUpload>.Instance.GetByOwnerAndAttachGUID(this.CreatorId, this.AttachmentGid, this.pager1.PagerInfo);
                    }
                    else
                    {
                        fileList = BLLFactory<FileUpload>.Instance.GetByOwner(this.CreatorId, this.pager1.PagerInfo);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(this.AttachmentGid))
                    {
                        fileList = BLLFactory<FileUpload>.Instance.GetByAttachGUID(this.AttachmentGid, this.pager1.PagerInfo);
                    }
                    else
                    {
                        fileList = BLLFactory<FileUpload>.Instance.GetAllByUser(this.UserId, this.AttachmentDirectory, this.pager1.PagerInfo);
                    }
                }
                
                int k = 0;
                System.Drawing.Icon icon = null;
                foreach (FileUploadInfo fileInfo in fileList)
                {
                    string file = fileInfo.Name;
                    string extension = FileUtil.GetExtension(file);

                    #region 取缩略图存到 imageList1 的操作
                    //如果是图片，取得它的图片数据作为缩略图
                    bool isImage = ValidateUtil.IsImageFile(fileInfo.FileExtend);
                    if (isImage)
                    {
                        try
                        {
                            FileUploadInfo tmpInfo = BLLFactory<FileUpload>.Instance.Download(fileInfo.Gid, 48, 48);
                            if (tmpInfo != null && tmpInfo.FileData != null)
                            {
                                this.imageList1.Images.Add(ImageHelper.BitmapFromBytes(tmpInfo.FileData));
                                this.imageList2.Images.Add(ImageHelper.BitmapFromBytes(tmpInfo.FileData));
                            }
                            else
                            {
                                icon = IconReaderHelper.ExtractIconForExtension(extension, true); //大图标
                                this.imageList1.Images.Add(icon);
                                icon = IconReaderHelper.ExtractIconForExtension(extension, false);//小图标
                                this.imageList2.Images.Add(icon);
                            }
                        }
                        catch (Exception ex)
                        {
                            icon = IconReaderHelper.ExtractIconForExtension(extension, true); //大图标
                            this.imageList1.Images.Add(icon);
                            icon = IconReaderHelper.ExtractIconForExtension(extension, false);//小图标
                            this.imageList2.Images.Add(icon);

                            LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(BizAttachment));
                            MessageDxUtil.ShowError(ex.Message);
                            return;
                        }
                    }
                    else
                    {
                        icon = IconReaderHelper.ExtractIconForExtension(extension, true); //大图标
                        this.imageList1.Images.Add(icon);
                        icon = IconReaderHelper.ExtractIconForExtension(extension, false);//小图标
                        this.imageList2.Images.Add(icon);
                    }
                    #endregion

                    if (!imageDict.ContainsKey(file))
                    {
                        imageDict.Add(file, k++);
                    }
                }

                foreach (FileUploadInfo fileInfo in fileList)
                {
                    ListViewItem item = listView1.Items.Add(fileInfo.Name);

                    double fileSize = ConvertHelper.ToDouble(fileInfo.FileSize / 1024, 1);
                    item.SubItems.Add(fileSize.ToString("#,#KB"));
                    item.SubItems.Add(fileInfo.AddTime.ToShortDateString());
                    item.ImageIndex = GetImageKey(fileInfo.Name);
                    item.Tag = fileInfo.Gid;
                }
                listView1.Refresh();
            }
        }

        private int GetImageKey(string fileName)
        {
            if (imageDict.ContainsKey(fileName))
            {
                return imageDict[fileName];
            }
            else
            {
                return -1;
            }
        }

        private void cmbViewType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbViewType.Text == "大图标")
            {
                this.listView1.View = View.LargeIcon;
            }
            else if (cmbViewType.Text == "小图标")
            {
                this.listView1.View = View.SmallIcon;
            }
            else
            {
                this.listView1.View = View.Details;
            }
        }

        private void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            this.listView1.CheckBoxes = chkSelect.Checked;
        }


        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            bool checkStats = chkSelectAll.Checked;
            this.listView1.CheckBoxes = checkStats;
            foreach (ListViewItem item in this.listView1.Items)
            {
                item.Checked = checkStats;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            BindData();
        }

        private void DownloadOrViewFile(string id, string name)
        {
            try
            {
                FileUploadInfo fileInfo = BLLFactory<FileUpload>.Instance.Download(id);
                if (fileInfo != null && fileInfo.FileData != null)
                {
                    string extension = fileInfo.FileExtend.ToLower();
                    bool isImage = ValidateUtil.IsImageFile(extension);
                    if (isImage)
                    {
                        FrmPicturePreview frm = new FrmPicturePreview();
                        System.Drawing.Bitmap bitmap = ImageHelper.BitmapFromBytes(fileInfo.FileData);
                        frm.ImageObj = bitmap;
                        frm.ShowDialog();
                    }
                    else
                    {
                        if (extension.Contains(".pdf"))
                        {
                            FrmPDFView dlg = new FrmPDFView();
                            dlg.Extension = extension;
                            dlg.Stream = FileUtil.BytesToStream(fileInfo.FileData);
                            dlg.ShowDialog();
                        }
                        else if (extension.Contains(".xls") || extension.Contains(".xlsx") || extension.Contains(".csv"))
                        {
                            FrmExcelView dlg = new FrmExcelView();
                            dlg.Extension = extension;
                            dlg.Stream = FileUtil.BytesToStream(fileInfo.FileData);
                            dlg.ShowDialog();
                        }
                        else if (extension.Contains(".doc") || extension.Contains(".docx") || extension.Contains(".rtf"))
                        {
                            FrmWordView dlg = new FrmWordView();
                            dlg.Extension = extension;
                            dlg.Stream = FileUtil.BytesToStream(fileInfo.FileData);
                            dlg.ShowDialog();
                        }
                        else
                        {
                            #region 非图片文件下载到本地
                            string saveFile = FileDialogHelper.SaveFile(name);
                            if (!string.IsNullOrEmpty(saveFile))
                            {
                                FileUtil.CreateFile(saveFile, fileInfo.FileData);
                                if (File.Exists(saveFile))
                                {
                                    if (MessageDxUtil.ShowYesNoAndTips("文件下载成功，是否打开文件？") == System.Windows.Forms.DialogResult.Yes)
                                    {
                                        System.Diagnostics.Process.Start(saveFile);
                                    }
                                }
                            }
                            #endregion
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(BizAttachment));
                MessageDxUtil.ShowError("保存文件出现错误。具体如下：\r\n" + ex.Message);
            }
        }
        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem item = this.listView1.GetItemAt(e.X, e.Y);
            if (item != null && item.Tag != null)
            {
                string id = item.Tag.ToString();
                DownloadOrViewFile(id, item.Text);
            }
        }


        private void menuDownload_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)
            {
                ListViewItem item = this.listView1.SelectedItems[0];
                if (item != null && item.Tag != null)
                {
                    string id = item.Tag.ToString();
                    DownloadOrViewFile(id, item.Text);
                }
            }
        }

        private void menuDelete_Click(object sender, EventArgs e)
        {
            bool sucess = false;
            string lastError = "";
            try
            {
                foreach (ListViewItem item in this.listView1.CheckedItems)
                {
                    if (item != null && item.Tag != null)
                    {
                        string id = item.Tag.ToString();
                        sucess = BLLFactory<FileUpload>.Instance.DeleteByUser(id, UserId);
                    }
                }
            }
            catch (Exception ex)
            {
                lastError += ex.Message + "\r\n";
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(BizAttachment));
            }

            MessageDxUtil.ShowTips(sucess ? "删除操作成功" : "操作失败:" + lastError);
            //ProcessDataSaved(this.btnDelete, new EventArgs());
            BindData();
        }

        private void menuRefresh_Click(object sender, EventArgs e)
        {
            BindData();
        }

    }
}
