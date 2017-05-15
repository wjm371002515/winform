namespace JCodes.Framework.AddIn.UI.Common
{
    partial class FrmClearAll
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmClearAll));
            this.lblTips = new DevExpress.XtraEditors.LabelControl();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
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
            this.lblTips.Location = new System.Drawing.Point(12, 27);
            this.lblTips.Name = "lblTips";
            this.lblTips.Size = new System.Drawing.Size(449, 42);
            this.lblTips.TabIndex = 0;
            this.lblTips.Text = "    本操作是危险操作，仅在系统使用的时候初始化数据库使用，<br><br>请在操作前确保数据库做了备份或不需备份！";
            // 
            // btnOK
            // 
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btnOK.Location = new System.Drawing.Point(80, 100);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(280, 80);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "执行【初始化业务数据】操作";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // FrmClearAll
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 244);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblTips);
            this.Name = "FrmClearAll";
            this.Text = "初始化业务数据";
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblTips;
        private DevExpress.XtraEditors.SimpleButton btnOK;
    }
}