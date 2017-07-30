namespace TestAttachmentDx
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnAttachment = new DevExpress.XtraEditors.SimpleButton();
            this.attachmentControl1 = new JCodes.Framework.AddIn.Basic.BizControl.AttachmentControl();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnAttachment
            // 
            this.btnAttachment.Location = new System.Drawing.Point(231, 191);
            this.btnAttachment.Name = "btnAttachment";
            this.btnAttachment.Size = new System.Drawing.Size(87, 27);
            this.btnAttachment.TabIndex = 1;
            this.btnAttachment.Text = "个人附件管理";
            this.btnAttachment.Click += new System.EventHandler(this.btnAttachment_Click);
            // 
            // attachmentControl1
            // 
            this.attachmentControl1.AttachmentGUID = null;
            this.attachmentControl1.Location = new System.Drawing.Point(24, 105);
            this.attachmentControl1.Name = "attachmentControl1";
            this.attachmentControl1.Size = new System.Drawing.Size(264, 27);
            this.attachmentControl1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "姓名";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(82, 18);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 22);
            this.textBox1.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "所在单位";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(82, 49);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(236, 22);
            this.textBox2.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 14);
            this.label3.TabIndex = 2;
            this.label3.Text = "家庭住址";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(82, 77);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(236, 22);
            this.textBox3.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 237);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAttachment);
            this.Controls.Add(this.attachmentControl1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "附件上传测试";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JCodes.Framework.AddIn.Basic.BizControl.AttachmentControl attachmentControl1;
        private DevExpress.XtraEditors.SimpleButton btnAttachment;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox3;
    }
}

