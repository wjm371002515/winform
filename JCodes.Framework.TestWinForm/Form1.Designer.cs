namespace JCodes.Framework.Test
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.btnEnURL = new System.Windows.Forms.Button();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.btnDeURL = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtURL);
            this.panel1.Controls.Add(this.btnDeURL);
            this.panel1.Controls.Add(this.btnEnURL);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(836, 43);
            this.panel1.TabIndex = 0;
            // 
            // rtbLog
            // 
            this.rtbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbLog.Location = new System.Drawing.Point(0, 43);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.Size = new System.Drawing.Size(836, 492);
            this.rtbLog.TabIndex = 1;
            this.rtbLog.Text = "";
            // 
            // btnEnURL
            // 
            this.btnEnURL.Location = new System.Drawing.Point(365, 9);
            this.btnEnURL.Name = "btnEnURL";
            this.btnEnURL.Size = new System.Drawing.Size(100, 28);
            this.btnEnURL.TabIndex = 0;
            this.btnEnURL.Text = "URL加密";
            this.btnEnURL.UseVisualStyleBackColor = true;
            this.btnEnURL.Click += new System.EventHandler(this.btnURL_Click);
            // 
            // txtURL
            // 
            this.txtURL.Location = new System.Drawing.Point(13, 9);
            this.txtURL.Name = "txtURL";
            this.txtURL.Size = new System.Drawing.Size(333, 28);
            this.txtURL.TabIndex = 1;
            // 
            // btnDeURL
            // 
            this.btnDeURL.Location = new System.Drawing.Point(482, 9);
            this.btnDeURL.Name = "btnDeURL";
            this.btnDeURL.Size = new System.Drawing.Size(100, 28);
            this.btnDeURL.TabIndex = 0;
            this.btnDeURL.Text = "URL解密";
            this.btnDeURL.UseVisualStyleBackColor = true;
            this.btnDeURL.Click += new System.EventHandler(this.btnDeURL_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 535);
            this.Controls.Add(this.rtbLog);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.Button btnEnURL;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.Button btnDeURL;
    }
}

