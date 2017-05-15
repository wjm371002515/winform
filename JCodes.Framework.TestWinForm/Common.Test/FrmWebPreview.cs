using JCodes.Framework.Common;
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
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}
