namespace JCodes.Framework.TestWinForm.Basic
{
    partial class FrmTestBizControl
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.managerSelectControl1 = new JCodes.Framework.AddIn.Basic.BizControl.ManagerSelectControl();
            this.bizAttachment1 = new JCodes.Framework.AddIn.Basic.BizControl.BizAttachment();
            this.attachmentControl1 = new JCodes.Framework.AddIn.Basic.BizControl.AttachmentControl();
            this.userNameControl1 = new JCodes.Framework.AddIn.Basic.BizControl.UserNameControl();
            this.dictControl1 = new JCodes.Framework.AddIn.UI.BizControl.DictControl();
            this.deptControl1 = new JCodes.Framework.AddIn.UI.BizControl.DeptControl();
            this.companyControl1 = new JCodes.Framework.AddIn.Basic.BizControl.CompanyControl();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 22);
            this.label1.TabIndex = 3;
            this.label1.Text = "公司";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 22);
            this.label2.TabIndex = 3;
            this.label2.Text = "部门";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 22);
            this.label3.TabIndex = 3;
            this.label3.Text = "数据字典";
            // 
            // managerSelectControl1
            // 
            this.managerSelectControl1.Location = new System.Drawing.Point(92, 219);
            this.managerSelectControl1.Margin = new System.Windows.Forms.Padding(0);
            this.managerSelectControl1.MaximumSize = new System.Drawing.Size(150, 20);
            this.managerSelectControl1.MinimumSize = new System.Drawing.Size(0, 20);
            this.managerSelectControl1.Name = "managerSelectControl1";
            this.managerSelectControl1.Size = new System.Drawing.Size(150, 20);
            this.managerSelectControl1.TabIndex = 8;
            // 
            // bizAttachment1
            // 
            this.bizAttachment1.AttachmentDirectory = JCodes.Framework.jCodesenum.AttachmentType.业务附件;
            this.bizAttachment1.AttachmentGid = null;
            this.bizAttachment1.CreatorId = 0;
            this.bizAttachment1.Location = new System.Drawing.Point(379, 12);
            this.bizAttachment1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bizAttachment1.Name = "bizAttachment1";
            this.bizAttachment1.ShowDelete = true;
            this.bizAttachment1.ShowOwnerData = false;
            this.bizAttachment1.ShowUpload = true;
            this.bizAttachment1.Size = new System.Drawing.Size(964, 548);
            this.bizAttachment1.TabIndex = 7;
            this.bizAttachment1.UserId = 0;
            // 
            // attachmentControl1
            // 
            this.attachmentControl1.AttachmentGid = "75c9bd34-d335-4668-8043-9a9521a4f0da";
            this.attachmentControl1.Location = new System.Drawing.Point(58, 186);
            this.attachmentControl1.MaximumSize = new System.Drawing.Size(210, 30);
            this.attachmentControl1.MinimumSize = new System.Drawing.Size(210, 30);
            this.attachmentControl1.Name = "attachmentControl1";
            this.attachmentControl1.Size = new System.Drawing.Size(210, 30);
            this.attachmentControl1.TabIndex = 6;
            this.attachmentControl1.TipsContent = "共有【{0}】个附件";
            // 
            // userNameControl1
            // 
            this.userNameControl1.AutoSize = true;
            this.userNameControl1.Location = new System.Drawing.Point(92, 140);
            this.userNameControl1.Name = "userNameControl1";
            this.userNameControl1.Size = new System.Drawing.Size(204, 31);
            this.userNameControl1.TabIndex = 4;
            // 
            // dictControl1
            // 
            this.dictControl1.DicNo = 100035;
            this.dictControl1.EditValue = null;
            this.dictControl1.Location = new System.Drawing.Point(92, 94);
            this.dictControl1.Name = "dictControl1";
            this.dictControl1.Size = new System.Drawing.Size(196, 28);
            this.dictControl1.TabIndex = 2;
            // 
            // deptControl1
            // 
            this.deptControl1.Location = new System.Drawing.Point(92, 55);
            this.deptControl1.Name = "deptControl1";
            this.deptControl1.Size = new System.Drawing.Size(196, 20);
            this.deptControl1.TabIndex = 1;
            this.deptControl1.Value = "-1";
            // 
            // companyControl1
            // 
            this.companyControl1.Location = new System.Drawing.Point(92, 12);
            this.companyControl1.Name = "companyControl1";
            this.companyControl1.Size = new System.Drawing.Size(224, 20);
            this.companyControl1.TabIndex = 0;
            this.companyControl1.Value = "-1";
            // 
            // FrmTestBizControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1356, 664);
            this.Controls.Add(this.managerSelectControl1);
            this.Controls.Add(this.bizAttachment1);
            this.Controls.Add(this.attachmentControl1);
            this.Controls.Add(this.userNameControl1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dictControl1);
            this.Controls.Add(this.deptControl1);
            this.Controls.Add(this.companyControl1);
            this.Name = "FrmTestBizControl";
            this.Text = "测试业务控件";
            this.Load += new System.EventHandler(this.FrmTestBizControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AddIn.Basic.BizControl.CompanyControl companyControl1;
        private AddIn.UI.BizControl.DeptControl deptControl1;
        private AddIn.UI.BizControl.DictControl dictControl1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private AddIn.Basic.BizControl.UserNameControl userNameControl1;
        private AddIn.Basic.BizControl.AttachmentControl attachmentControl1;
        private AddIn.Basic.BizControl.BizAttachment bizAttachment1;
        private AddIn.Basic.BizControl.ManagerSelectControl managerSelectControl1;
    }
}