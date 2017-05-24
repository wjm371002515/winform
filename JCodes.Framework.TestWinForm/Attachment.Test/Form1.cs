using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using JCodes.Framework.AddIn.UI.Attachment;

namespace TestAttachmentDx
{
    public partial class Form1 : XtraForm
    {
        private string TestGuid = "28150621-a65a-412c-bad7-c61e2bee7951"; 
        public Form1()
        {
            InitializeComponent();

            this.attachmentControl1.AttachmentGUID = TestGuid;//Guid.NewGuid().ToString();
        }

        private void btnAttachment_Click(object sender, EventArgs e)
        {
            FrmAttachment dlg = new FrmAttachment();
            dlg.UserId = "0bed145f-e3fd-4b95-8eb1-e67146044f87";
            dlg.ShowDialog();
        }
    }
}
