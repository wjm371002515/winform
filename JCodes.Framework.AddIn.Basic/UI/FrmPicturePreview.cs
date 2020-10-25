using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using JCodes.Framework.Common;
using JCodes.Framework.CommonControl;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.Common.Files;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.jCodesenum;

namespace JCodes.Framework.AddIn.Basic
{
    internal partial class FrmPicturePreview : BaseDock
    {
        /// <summary>
        /// 图片路径
        /// </summary>
        public string PicturePath { get; set; }
        
        /// <summary>
        /// 图片对象（可选）
        /// </summary>
        public Bitmap ImageObj { get; set; }

        public FrmPicturePreview()
        {
            InitializeComponent();
        }

        private void FrmPicturePreview_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(PicturePath))
            {
                this.kpImageViewer1.ImagePath = PicturePath;
            }

            if (ImageObj != null)
            {
                try
                {
                    this.kpImageViewer1.Image = ImageObj;
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmPicturePreview));
                    MessageDxUtil.ShowError(ex.Message);
                }
            }
        }

        private void menuSaveAs_Click(object sender, EventArgs e)
        {
            string imageFile = FileDialogHelper.SaveImage();
            if (!string.IsNullOrEmpty(imageFile))
            {
                //ImageHelper.
                //FileUtil.StreamToFile(this.ImageObj.
            }
        }

        private void FrmPicturePreview_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                e.Handled = true;
            }
        }

    }
}
