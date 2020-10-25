using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using JCodes.Framework.CommonControl;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.jCodesenum;

namespace JCodes.Framework.AddIn.Basic
{
    public partial class FrmAttachment : BaseDock
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public Int32 UserId = 0;

        public FrmAttachment()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.bizAttachment.UserId = UserId;
            this.bizAttachment.AttachmentDirectory = AttachmentType.业务附件;
            this.bizAttachment.BindData();

            this.MyAttachment.UserId = UserId;
            this.MyAttachment.AttachmentDirectory = AttachmentType.个人附件;
            this.MyAttachment.BindData();
        }

        private void MyAttachment_Load(object sender, EventArgs e)
        {

        }
    }
}