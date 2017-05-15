namespace TestControlUtil
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnAsposeCell = new System.Windows.Forms.Button();
            this.btnAsposeWords = new System.Windows.Forms.Button();
            this.btnFrmMyXls = new System.Windows.Forms.Button();
            this.btnNPOI = new System.Windows.Forms.Button();
            this.btnADSLDialer = new System.Windows.Forms.Button();
            this.btnZipUtil = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAsposeCell
            // 
            this.btnAsposeCell.Location = new System.Drawing.Point(13, 13);
            this.btnAsposeCell.Name = "btnAsposeCell";
            this.btnAsposeCell.Size = new System.Drawing.Size(103, 27);
            this.btnAsposeCell.TabIndex = 0;
            this.btnAsposeCell.Text = "AsposeCell测试";
            this.btnAsposeCell.UseVisualStyleBackColor = true;
            this.btnAsposeCell.Click += new System.EventHandler(this.btnAsposeCell_Click);
            // 
            // btnAsposeWords
            // 
            this.btnAsposeWords.Location = new System.Drawing.Point(122, 12);
            this.btnAsposeWords.Name = "btnAsposeWords";
            this.btnAsposeWords.Size = new System.Drawing.Size(103, 27);
            this.btnAsposeWords.TabIndex = 0;
            this.btnAsposeWords.Text = "AsposeWords测试";
            this.btnAsposeWords.UseVisualStyleBackColor = true;
            this.btnAsposeWords.Click += new System.EventHandler(this.btnAsposeWords_Click);
            // 
            // btnFrmMyXls
            // 
            this.btnFrmMyXls.Location = new System.Drawing.Point(231, 12);
            this.btnFrmMyXls.Name = "btnFrmMyXls";
            this.btnFrmMyXls.Size = new System.Drawing.Size(103, 27);
            this.btnFrmMyXls.TabIndex = 0;
            this.btnFrmMyXls.Text = "MyXls测试";
            this.btnFrmMyXls.UseVisualStyleBackColor = true;
            this.btnFrmMyXls.Click += new System.EventHandler(this.btnFrmMyXls_Click);
            // 
            // btnNPOI
            // 
            this.btnNPOI.Location = new System.Drawing.Point(340, 12);
            this.btnNPOI.Name = "btnNPOI";
            this.btnNPOI.Size = new System.Drawing.Size(103, 27);
            this.btnNPOI.TabIndex = 0;
            this.btnNPOI.Text = "NPOI测试";
            this.btnNPOI.UseVisualStyleBackColor = true;
            this.btnNPOI.Click += new System.EventHandler(this.btnNPOI_Click);
            // 
            // btnADSLDialer
            // 
            this.btnADSLDialer.Location = new System.Drawing.Point(13, 62);
            this.btnADSLDialer.Name = "btnADSLDialer";
            this.btnADSLDialer.Size = new System.Drawing.Size(103, 27);
            this.btnADSLDialer.TabIndex = 0;
            this.btnADSLDialer.Text = "ADSL拨号测试";
            this.btnADSLDialer.UseVisualStyleBackColor = true;
            this.btnADSLDialer.Click += new System.EventHandler(this.btnADSLDialer_Click);
            // 
            // btnZipUtil
            // 
            this.btnZipUtil.Location = new System.Drawing.Point(122, 62);
            this.btnZipUtil.Name = "btnZipUtil";
            this.btnZipUtil.Size = new System.Drawing.Size(103, 27);
            this.btnZipUtil.TabIndex = 0;
            this.btnZipUtil.Text = "压缩文件";
            this.btnZipUtil.UseVisualStyleBackColor = true;
            this.btnZipUtil.Click += new System.EventHandler(this.btnZipUtil_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 345);
            this.Controls.Add(this.btnZipUtil);
            this.Controls.Add(this.btnADSLDialer);
            this.Controls.Add(this.btnNPOI);
            this.Controls.Add(this.btnFrmMyXls);
            this.Controls.Add(this.btnAsposeWords);
            this.Controls.Add(this.btnAsposeCell);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "辅助类测试主界面";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAsposeCell;
        private System.Windows.Forms.Button btnAsposeWords;
        private System.Windows.Forms.Button btnFrmMyXls;
        private System.Windows.Forms.Button btnNPOI;
        private System.Windows.Forms.Button btnADSLDialer;
        private System.Windows.Forms.Button btnZipUtil;

    }
}

