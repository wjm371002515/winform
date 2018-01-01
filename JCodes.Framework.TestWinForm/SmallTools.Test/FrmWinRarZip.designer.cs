namespace JCodes.Framework.SmallTools.Test
{
    partial class FrmWinRarZip
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.cbbeUnRarZip = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.txtUnRarPath = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtRarPath = new DevExpress.XtraEditors.TextEdit();
            this.btnUnRar = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnRar = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.richText = new DevExpress.XtraRichEdit.RichEditControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbbeUnRarZip.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUnRarPath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRarPath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.cbbeUnRarZip);
            this.panelControl1.Controls.Add(this.txtUnRarPath);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.txtRarPath);
            this.panelControl1.Controls.Add(this.btnUnRar);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.btnRar);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(830, 106);
            this.panelControl1.TabIndex = 0;
            // 
            // cbbeUnRarZip
            // 
            this.cbbeUnRarZip.Location = new System.Drawing.Point(604, 58);
            this.cbbeUnRarZip.Name = "cbbeUnRarZip";
            this.cbbeUnRarZip.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbbeUnRarZip.Size = new System.Drawing.Size(100, 28);
            this.cbbeUnRarZip.TabIndex = 3;
            // 
            // txtUnRarPath
            // 
            this.txtUnRarPath.Location = new System.Drawing.Point(109, 58);
            this.txtUnRarPath.Name = "txtUnRarPath";
            this.txtUnRarPath.Size = new System.Drawing.Size(469, 28);
            this.txtUnRarPath.TabIndex = 2;
            this.txtUnRarPath.Click += new System.EventHandler(this.textEdit1_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(21, 61);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(78, 22);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "解压路径:";
            // 
            // txtRarPath
            // 
            this.txtRarPath.Location = new System.Drawing.Point(109, 14);
            this.txtRarPath.Name = "txtRarPath";
            this.txtRarPath.Size = new System.Drawing.Size(595, 28);
            this.txtRarPath.TabIndex = 2;
            this.txtRarPath.Click += new System.EventHandler(this.textEdit1_Click);
            // 
            // btnUnRar
            // 
            this.btnUnRar.Location = new System.Drawing.Point(723, 54);
            this.btnUnRar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnUnRar.Name = "btnUnRar";
            this.btnUnRar.Size = new System.Drawing.Size(74, 40);
            this.btnUnRar.TabIndex = 0;
            this.btnUnRar.Text = "解压";
            this.btnUnRar.Click += new System.EventHandler(this.btnUnRar_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(21, 17);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(78, 22);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "压缩路径:";
            // 
            // btnRar
            // 
            this.btnRar.Location = new System.Drawing.Point(723, 8);
            this.btnRar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRar.Name = "btnRar";
            this.btnRar.Size = new System.Drawing.Size(74, 40);
            this.btnRar.TabIndex = 0;
            this.btnRar.Text = "压缩";
            this.btnRar.Click += new System.EventHandler(this.btnRar_Click);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.richText);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 106);
            this.panelControl2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(830, 468);
            this.panelControl2.TabIndex = 1;
            // 
            // richText
            // 
            this.richText.ActiveViewType = DevExpress.XtraRichEdit.RichEditViewType.Simple;
            this.richText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richText.EnableToolTips = true;
            this.richText.Location = new System.Drawing.Point(7, 7);
            this.richText.Margin = new System.Windows.Forms.Padding(0);
            this.richText.Name = "richText";
            this.richText.Options.CopyPaste.MaintainDocumentSectionSettings = false;
            this.richText.Options.Fields.UseCurrentCultureDateTimeFormat = false;
            this.richText.Options.HorizontalRuler.Visibility = DevExpress.XtraRichEdit.RichEditRulerVisibility.Hidden;
            this.richText.Options.HorizontalScrollbar.Visibility = DevExpress.XtraRichEdit.RichEditScrollbarVisibility.Hidden;
            this.richText.Options.MailMerge.KeepLastParagraph = false;
            this.richText.Options.VerticalRuler.Visibility = DevExpress.XtraRichEdit.RichEditRulerVisibility.Hidden;
            this.richText.Size = new System.Drawing.Size(814, 452);
            this.richText.TabIndex = 0;
            this.richText.Views.DraftView.Padding = new System.Windows.Forms.Padding(0);
            this.richText.Views.SimpleView.Padding = new System.Windows.Forms.Padding(0);
            // 
            // FrmWinRarZip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 574);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FrmWinRarZip";
            this.ShowIcon = false;
            this.Text = "压缩&解压小工具(By jCodes)";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbbeUnRarZip.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUnRarPath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRarPath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnRar;
        private DevExpress.XtraRichEdit.RichEditControl richText;
        private DevExpress.XtraEditors.TextEdit txtRarPath;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtUnRarPath;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton btnUnRar;
        private DevExpress.XtraEditors.CheckedComboBoxEdit cbbeUnRarZip;

    }
}

