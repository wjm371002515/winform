namespace JCodes.Framework.AddIn.Basic
{
    partial class FrmAnnualSummary
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAnnualSummary));
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.lblTips = new DevExpress.XtraEditors.LabelControl();
            this.progressBar = new DevExpress.XtraEditors.ProgressBarControl();
            ((System.ComponentModel.ISupportInitialize)(this.progressBar.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btnOK.Location = new System.Drawing.Point(110, 91);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(271, 65);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "执行【年度汇总操作】";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblTips
            // 
            this.lblTips.AllowHtmlString = true;
            this.lblTips.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.False;
            this.lblTips.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTips.Appearance.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTips.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblTips.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblTips.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTips.Location = new System.Drawing.Point(46, 30);
            this.lblTips.Name = "lblTips";
            this.lblTips.Size = new System.Drawing.Size(405, 31);
            this.lblTips.TabIndex = 1;
            this.lblTips.Text = "年度汇总操作可能会比较耗时，任务执行过程中请勿退出。";
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(3, 225);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(477, 27);
            this.progressBar.TabIndex = 2;
            this.progressBar.Visible = false;
            // 
            // FrmAnnualSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 255);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.lblTips);
            this.Controls.Add(this.btnOK);
            this.Name = "FrmAnnualSummary";
            this.Text = "年度汇总操作";
            ((System.ComponentModel.ISupportInitialize)(this.progressBar.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.LabelControl lblTips;
        private DevExpress.XtraEditors.ProgressBarControl progressBar;
    }
}