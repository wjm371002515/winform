namespace JCodes.Framework.AddIn.SmallTools
{
    partial class FrmServerDos
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
            this.txtComand = new DevExpress.XtraEditors.TextEdit();
            this.lblComand = new DevExpress.XtraEditors.LabelControl();
            this.richResultShow = new DevExpress.XtraRichEdit.RichEditControl();
            this.btnStopDosComand = new DevExpress.XtraEditors.SimpleButton();
            this.lblIP = new DevExpress.XtraEditors.LabelControl();
            this.txtIP = new DevExpress.XtraEditors.TextEdit();
            this.txtPort = new DevExpress.XtraEditors.TextEdit();
            this.lblPort = new DevExpress.XtraEditors.LabelControl();
            this.txtServerType = new DevExpress.XtraEditors.LabelControl();
            this.cbbeServerType = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.btnConnServer = new DevExpress.XtraEditors.SimpleButton();
            this.btnDisconnServer = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtComand.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIP.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPort.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbbeServerType.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStartDosComand
            // 
            this.btnStartDosComand.Location = new System.Drawing.Point(845, 46);
            this.btnStartDosComand.Name = "btnStartDosComand";
            this.btnStartDosComand.Size = new System.Drawing.Size(91, 33);
            this.btnStartDosComand.TabIndex = 8;
            this.btnStartDosComand.Text = "执行命令";
            this.btnStartDosComand.Click += new System.EventHandler(this.btnStartDosComand_Click);
            // 
            // txtComand
            // 
            this.txtComand.Location = new System.Drawing.Point(54, 49);
            this.txtComand.Name = "txtComand";
            this.txtComand.Size = new System.Drawing.Size(785, 28);
            this.txtComand.TabIndex = 7;
            // 
            // lblComand
            // 
            this.lblComand.Location = new System.Drawing.Point(12, 52);
            this.lblComand.Name = "lblComand";
            this.lblComand.Size = new System.Drawing.Size(36, 22);
            this.lblComand.TabIndex = 6;
            this.lblComand.Text = "命令";
            // 
            // richResultShow
            // 
            this.richResultShow.ActiveViewType = DevExpress.XtraRichEdit.RichEditViewType.Simple;
            this.richResultShow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richResultShow.EnableToolTips = true;
            this.richResultShow.Location = new System.Drawing.Point(8, 87);
            this.richResultShow.Margin = new System.Windows.Forms.Padding(0);
            this.richResultShow.Name = "richResultShow";
            this.richResultShow.Options.CopyPaste.MaintainDocumentSectionSettings = false;
            this.richResultShow.Options.Fields.UseCurrentCultureDateTimeFormat = false;
            this.richResultShow.Options.HorizontalRuler.Visibility = DevExpress.XtraRichEdit.RichEditRulerVisibility.Hidden;
            this.richResultShow.Options.HorizontalScrollbar.Visibility = DevExpress.XtraRichEdit.RichEditScrollbarVisibility.Hidden;
            this.richResultShow.Options.MailMerge.KeepLastParagraph = false;
            this.richResultShow.Options.VerticalRuler.Visibility = DevExpress.XtraRichEdit.RichEditRulerVisibility.Hidden;
            this.richResultShow.Size = new System.Drawing.Size(1030, 534);
            this.richResultShow.TabIndex = 5;
            this.richResultShow.Views.DraftView.Padding = new System.Windows.Forms.Padding(0);
            this.richResultShow.Views.SimpleView.Padding = new System.Windows.Forms.Padding(0);
            // 
            // btnStopDosComand
            // 
            this.btnStopDosComand.Location = new System.Drawing.Point(942, 46);
            this.btnStopDosComand.Name = "btnStopDosComand";
            this.btnStopDosComand.Size = new System.Drawing.Size(91, 33);
            this.btnStopDosComand.TabIndex = 8;
            this.btnStopDosComand.Text = "停止命令";
            this.btnStopDosComand.Click += new System.EventHandler(this.btnStopDosComand_Click);
            // 
            // lblIP
            // 
            this.lblIP.Location = new System.Drawing.Point(25, 15);
            this.lblIP.Name = "lblIP";
            this.lblIP.Size = new System.Drawing.Size(17, 22);
            this.lblIP.TabIndex = 6;
            this.lblIP.Text = "IP";
            // 
            // txtIP
            // 
            this.txtIP.EditValue = "192.192.192.192";
            this.txtIP.Location = new System.Drawing.Point(54, 12);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(322, 28);
            this.txtIP.TabIndex = 7;
            // 
            // txtPort
            // 
            this.txtPort.EditValue = "";
            this.txtPort.Location = new System.Drawing.Point(442, 12);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(126, 28);
            this.txtPort.TabIndex = 7;
            // 
            // lblPort
            // 
            this.lblPort.Location = new System.Drawing.Point(400, 15);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(36, 22);
            this.lblPort.TabIndex = 6;
            this.lblPort.Text = "端口";
            // 
            // txtServerType
            // 
            this.txtServerType.Location = new System.Drawing.Point(589, 15);
            this.txtServerType.Name = "txtServerType";
            this.txtServerType.Size = new System.Drawing.Size(90, 22);
            this.txtServerType.TabIndex = 6;
            this.txtServerType.Text = "服务器类型";
            // 
            // cbbeServerType
            // 
            this.cbbeServerType.Location = new System.Drawing.Point(685, 12);
            this.cbbeServerType.Name = "cbbeServerType";
            this.cbbeServerType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbbeServerType.Size = new System.Drawing.Size(154, 28);
            this.cbbeServerType.TabIndex = 9;
            // 
            // btnConnServer
            // 
            this.btnConnServer.Location = new System.Drawing.Point(845, 7);
            this.btnConnServer.Name = "btnConnServer";
            this.btnConnServer.Size = new System.Drawing.Size(91, 33);
            this.btnConnServer.TabIndex = 8;
            this.btnConnServer.Text = "链接";
            this.btnConnServer.Click += new System.EventHandler(this.btnConnServer_Click);
            // 
            // btnDisconnServer
            // 
            this.btnDisconnServer.Location = new System.Drawing.Point(942, 7);
            this.btnDisconnServer.Name = "btnDisconnServer";
            this.btnDisconnServer.Size = new System.Drawing.Size(91, 33);
            this.btnDisconnServer.TabIndex = 8;
            this.btnDisconnServer.Text = "断开";
            this.btnDisconnServer.Click += new System.EventHandler(this.btnDisconnServer_Click);
            // 
            // FrmDos
            // 
            this.AcceptButton = this.btnStartDosComand;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1045, 629);
            this.Controls.Add(this.cbbeServerType);
            this.Controls.Add(this.btnStopDosComand);
            this.Controls.Add(this.btnDisconnServer);
            this.Controls.Add(this.btnConnServer);
            this.Controls.Add(this.btnStartDosComand);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.txtServerType);
            this.Controls.Add(this.txtIP);
            this.Controls.Add(this.lblPort);
            this.Controls.Add(this.lblIP);
            this.Controls.Add(this.txtComand);
            this.Controls.Add(this.lblComand);
            this.Controls.Add(this.richResultShow);
            this.Name = "FrmDos";
            this.Text = "远程服务器控制";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmDos_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.txtComand.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIP.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPort.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbbeServerType.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnStartDosComand;
        private DevExpress.XtraEditors.TextEdit txtComand;
        private DevExpress.XtraEditors.LabelControl lblComand;
        private DevExpress.XtraRichEdit.RichEditControl richResultShow;
        private DevExpress.XtraEditors.SimpleButton btnStopDosComand;
        private DevExpress.XtraEditors.LabelControl lblIP;
        private DevExpress.XtraEditors.TextEdit txtIP;
        private DevExpress.XtraEditors.TextEdit txtPort;
        private DevExpress.XtraEditors.LabelControl lblPort;
        private DevExpress.XtraEditors.LabelControl txtServerType;
        private DevExpress.XtraEditors.CheckedComboBoxEdit cbbeServerType;
        private DevExpress.XtraEditors.SimpleButton btnConnServer;
        private DevExpress.XtraEditors.SimpleButton btnDisconnServer;

    }
}

