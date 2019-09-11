using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using JCodes.Framework.AddIn.Basic;

namespace TestAttachmentDx
{
    public partial class Form1 : XtraForm
    {
        private string TestGuid = "28150621-a65a-412c-bad7-c61e2bee7951"; 
        public Form1()
        {
            InitializeComponent();

            this.attachmentControl1.AttachmentGid = TestGuid;//Guid.NewGuid().ToString();
        }

        private void btnAttachment_Click(object sender, EventArgs e)
        {
            FrmAttachment dlg = new FrmAttachment();
            dlg.UserId = 1;
            dlg.ShowDialog();
        }
    }
}
