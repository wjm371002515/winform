using JCodes.Framework.jCodesenum;
namespace JCodes.Framework.AddIn.Basic
{
    partial class FrmAttachment
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.MyAttachment = new JCodes.Framework.AddIn.Basic.BizControl.BizAttachment();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.bizAttachment = new JCodes.Framework.AddIn.Basic.BizControl.BizAttachment();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.xtraTabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(913, 635);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.MyAttachment);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(907, 606);
            this.xtraTabPage1.Text = "个人附件";
            // 
            // MyAttachment
            // 
            this.MyAttachment.AttachmentDirectory = AttachmentType.业务附件;
            this.MyAttachment.AttachmentGid = null;
            this.MyAttachment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MyAttachment.Location = new System.Drawing.Point(0, 0);
            this.MyAttachment.Name = "MyAttachment";
            this.MyAttachment.CreatorId = 0;
            this.MyAttachment.ShowDelete = true;
            this.MyAttachment.ShowUpload = true;
            this.MyAttachment.Size = new System.Drawing.Size(907, 606);
            this.MyAttachment.TabIndex = 0;
            this.MyAttachment.UserId = 0;
            this.MyAttachment.Load += new System.EventHandler(this.MyAttachment_Load);
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.bizAttachment);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(907, 606);
            this.xtraTabPage2.Text = "业务附件";
            // 
            // bizAttachment
            // 
            this.bizAttachment.AttachmentDirectory = AttachmentType.业务附件;
            this.bizAttachment.AttachmentGid = null;
            this.bizAttachment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bizAttachment.Location = new System.Drawing.Point(0, 0);
            this.bizAttachment.Name = "bizAttachment";
            this.bizAttachment.CreatorId = 0;
            this.bizAttachment.ShowDelete = false;
            this.bizAttachment.ShowUpload = false;
            this.bizAttachment.Size = new System.Drawing.Size(907, 606);
            this.bizAttachment.TabIndex = 0;
            this.bizAttachment.UserId = 0;
            // 
            // FrmAttachment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(913, 635);
            this.Controls.Add(this.xtraTabControl1);
            this.Name = "FrmAttachment";
            this.Text = "附件管理";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.xtraTabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private JCodes.Framework.AddIn.Basic.BizControl.BizAttachment MyAttachment;
        private JCodes.Framework.AddIn.Basic.BizControl.BizAttachment bizAttachment;
    }
}