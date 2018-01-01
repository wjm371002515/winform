namespace JCodes.Framework.AddIn.SmallTools
{
    partial class FrmBaiduPan
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
            this.btnStartDosComand = new DevExpress.XtraEditors.SimpleButton();
            this.txtURL = new DevExpress.XtraEditors.TextEdit();
            this.lblDosComand = new DevExpress.XtraEditors.LabelControl();
            this.richResultShow = new DevExpress.XtraRichEdit.RichEditControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtPwd = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.ckPwd = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtURL.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPwd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckPwd.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStartDosComand
            // 
            this.btnStartDosComand.Location = new System.Drawing.Point(937, 11);
            this.btnStartDosComand.Name = "btnStartDosComand";
            this.btnStartDosComand.Size = new System.Drawing.Size(91, 66);
            this.btnStartDosComand.TabIndex = 8;
            this.btnStartDosComand.Text = "解析地址";
            this.btnStartDosComand.Click += new System.EventHandler(this.btnStartDosComand_Click);
            // 
            // txtURL
            // 
            this.txtURL.EditValue = "https://pan.baidu.com/s/1dE3d8tj";
            this.txtURL.Location = new System.Drawing.Point(92, 12);
            this.txtURL.Name = "txtURL";
            this.txtURL.Size = new System.Drawing.Size(828, 28);
            this.txtURL.TabIndex = 7;
            // 
            // lblDosComand
            // 
            this.lblDosComand.Location = new System.Drawing.Point(9, 15);
            this.lblDosComand.Name = "lblDosComand";
            this.lblDosComand.Size = new System.Drawing.Size(78, 22);
            this.lblDosComand.TabIndex = 6;
            this.lblDosComand.Text = "网盘地址:";
            // 
            // richResultShow
            // 
            this.richResultShow.ActiveViewType = DevExpress.XtraRichEdit.RichEditViewType.Simple;
            this.richResultShow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richResultShow.EnableToolTips = true;
            this.richResultShow.Location = new System.Drawing.Point(6, 88);
            this.richResultShow.Margin = new System.Windows.Forms.Padding(0);
            this.richResultShow.Name = "richResultShow";
            this.richResultShow.Options.CopyPaste.MaintainDocumentSectionSettings = false;
            this.richResultShow.Options.Fields.UseCurrentCultureDateTimeFormat = false;
            this.richResultShow.Options.HorizontalRuler.Visibility = DevExpress.XtraRichEdit.RichEditRulerVisibility.Hidden;
            this.richResultShow.Options.HorizontalScrollbar.Visibility = DevExpress.XtraRichEdit.RichEditScrollbarVisibility.Hidden;
            this.richResultShow.Options.MailMerge.KeepLastParagraph = false;
            this.richResultShow.Options.VerticalRuler.Visibility = DevExpress.XtraRichEdit.RichEditRulerVisibility.Hidden;
            this.richResultShow.Size = new System.Drawing.Size(1030, 461);
            this.richResultShow.TabIndex = 5;
            this.richResultShow.Views.DraftView.Padding = new System.Windows.Forms.Padding(0);
            this.richResultShow.Views.SimpleView.Padding = new System.Windows.Forms.Padding(0);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(9, 51);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(114, 22);
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "是否需要密码:";
            // 
            // txtPwd
            // 
            this.txtPwd.Enabled = false;
            this.txtPwd.Location = new System.Drawing.Point(289, 47);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.Size = new System.Drawing.Size(631, 28);
            this.txtPwd.TabIndex = 7;
            // 
            // labelControl2
            // 
            this.labelControl2.Enabled = false;
            this.labelControl2.Location = new System.Drawing.Point(221, 50);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(42, 22);
            this.labelControl2.TabIndex = 6;
            this.labelControl2.Text = "密码:";
            // 
            // ckPwd
            // 
            this.ckPwd.Location = new System.Drawing.Point(134, 53);
            this.ckPwd.Name = "ckPwd";
            this.ckPwd.Properties.Caption = "";
            this.ckPwd.Size = new System.Drawing.Size(23, 19);
            this.ckPwd.TabIndex = 9;
            this.ckPwd.CheckedChanged += new System.EventHandler(this.ckPwd_CheckedChanged);
            // 
            // FrmBaiduPan
            // 
            this.AcceptButton = this.btnStartDosComand;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1045, 553);
            this.Controls.Add(this.ckPwd);
            this.Controls.Add(this.btnStartDosComand);
            this.Controls.Add(this.txtPwd);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txtURL);
            this.Controls.Add(this.lblDosComand);
            this.Controls.Add(this.richResultShow);
            this.Name = "FrmBaiduPan";
            this.Text = "百度云盘资源搜索";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmDos_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.txtURL.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPwd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckPwd.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnStartDosComand;
        private DevExpress.XtraEditors.TextEdit txtURL;
        private DevExpress.XtraEditors.LabelControl lblDosComand;
        private DevExpress.XtraRichEdit.RichEditControl richResultShow;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtPwd;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.CheckEdit ckPwd;

    }
}

