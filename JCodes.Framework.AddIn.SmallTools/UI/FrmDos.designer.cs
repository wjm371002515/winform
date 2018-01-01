namespace JCodes.Framework.AddIn.SmallTools
{
    partial class FrmDos
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
            this.txtDosComand = new DevExpress.XtraEditors.TextEdit();
            this.lblDosComand = new DevExpress.XtraEditors.LabelControl();
            this.richResultShow = new DevExpress.XtraRichEdit.RichEditControl();
            this.btnStopDosComand = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtDosComand.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStartDosComand
            // 
            this.btnStartDosComand.Location = new System.Drawing.Point(836, 9);
            this.btnStartDosComand.Name = "btnStartDosComand";
            this.btnStartDosComand.Size = new System.Drawing.Size(91, 33);
            this.btnStartDosComand.TabIndex = 8;
            this.btnStartDosComand.Text = "执行命令";
            this.btnStartDosComand.Click += new System.EventHandler(this.btnStartDosComand_Click);
            // 
            // txtDosComand
            // 
            this.txtDosComand.Location = new System.Drawing.Point(92, 12);
            this.txtDosComand.Name = "txtDosComand";
            this.txtDosComand.Size = new System.Drawing.Size(726, 28);
            this.txtDosComand.TabIndex = 7;
            // 
            // lblDosComand
            // 
            this.lblDosComand.Location = new System.Drawing.Point(9, 15);
            this.lblDosComand.Name = "lblDosComand";
            this.lblDosComand.Size = new System.Drawing.Size(77, 22);
            this.lblDosComand.TabIndex = 6;
            this.lblDosComand.Text = "DOS命令:";
            // 
            // richResultShow
            // 
            this.richResultShow.ActiveViewType = DevExpress.XtraRichEdit.RichEditViewType.Simple;
            this.richResultShow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richResultShow.EnableToolTips = true;
            this.richResultShow.Location = new System.Drawing.Point(6, 47);
            this.richResultShow.Margin = new System.Windows.Forms.Padding(0);
            this.richResultShow.Name = "richResultShow";
            this.richResultShow.Options.CopyPaste.MaintainDocumentSectionSettings = false;
            this.richResultShow.Options.Fields.UseCurrentCultureDateTimeFormat = false;
            this.richResultShow.Options.HorizontalRuler.Visibility = DevExpress.XtraRichEdit.RichEditRulerVisibility.Hidden;
            this.richResultShow.Options.HorizontalScrollbar.Visibility = DevExpress.XtraRichEdit.RichEditScrollbarVisibility.Hidden;
            this.richResultShow.Options.MailMerge.KeepLastParagraph = false;
            this.richResultShow.Options.VerticalRuler.Visibility = DevExpress.XtraRichEdit.RichEditRulerVisibility.Hidden;
            this.richResultShow.Size = new System.Drawing.Size(1030, 502);
            this.richResultShow.TabIndex = 5;
            this.richResultShow.Views.DraftView.Padding = new System.Windows.Forms.Padding(0);
            this.richResultShow.Views.SimpleView.Padding = new System.Windows.Forms.Padding(0);
            // 
            // btnStopDosComand
            // 
            this.btnStopDosComand.Location = new System.Drawing.Point(942, 9);
            this.btnStopDosComand.Name = "btnStopDosComand";
            this.btnStopDosComand.Size = new System.Drawing.Size(91, 33);
            this.btnStopDosComand.TabIndex = 8;
            this.btnStopDosComand.Text = "停止命令";
            this.btnStopDosComand.Click += new System.EventHandler(this.btnStopDosComand_Click);
            // 
            // FrmDos
            // 
            this.AcceptButton = this.btnStartDosComand;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1045, 553);
            this.Controls.Add(this.btnStopDosComand);
            this.Controls.Add(this.btnStartDosComand);
            this.Controls.Add(this.txtDosComand);
            this.Controls.Add(this.lblDosComand);
            this.Controls.Add(this.richResultShow);
            this.Name = "FrmDos";
            this.Text = "Dos命令行";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmDos_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.txtDosComand.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnStartDosComand;
        private DevExpress.XtraEditors.TextEdit txtDosComand;
        private DevExpress.XtraEditors.LabelControl lblDosComand;
        private DevExpress.XtraRichEdit.RichEditControl richResultShow;
        private DevExpress.XtraEditors.SimpleButton btnStopDosComand;

    }
}

