using JCodes.Framework.Common;
using JCodes.Framework.Common.Images;
using JCodes.Framework.CommonControl;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.CommonControl.Other.Images;
using JCodes.Framework.jCodesenum.BaseEnum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestCommons
{
    public partial class FrmWebPreview : Form
    {
        private WebPageCapture capture = new WebPageCapture();
        public FrmWebPreview()
        {
            InitializeComponent();

            capture.DownloadCompleted += new WebPageCapture.ImageEventHandler(capture_DownloadCompleted);
        }

        void capture_DownloadCompleted(Image image)
        {
            this.pictureBox1.Image = image;
        }

        private void btnSnap1_Click(object sender, EventArgs e)
        {
            if (this.txtUrl.Text.Length == 0) return;

            capture.DownloadPage(this.txtUrl.Text);            
        }

        private void btnSnap2_Click(object sender, EventArgs e)
        {
            if (this.txtUrl.Text.Length == 0) return;
            this.pictureBox1.Image = WebPreview.GetWebPreview(new Uri(this.txtUrl.Text), Screen.PrimaryScreen.Bounds.Size);
        }

        private void btnPrintForm_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                PrintFormHelper.Print(this);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmWebPreview));
                MessageDxUtil.ShowError(ex.Message); 
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}
