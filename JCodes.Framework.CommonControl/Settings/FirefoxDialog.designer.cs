namespace JCodes.Framework.CommonControl.Settings
{
	partial class FirefoxDialog
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.pagePanel = new System.Windows.Forms.Panel();
            this.leftPanel = new System.Windows.Forms.Panel();
            this.mozPane1 = new JCodes.Framework.CommonControl.Settings.MozPane();
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.meChangeContent = new DevExpress.XtraEditors.MemoEdit();
            this.pcbottonright = new DevExpress.XtraEditors.PanelControl();
            this.btnApply = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.leftPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mozPane1)).BeginInit();
            this.bottomPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.meChangeContent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbottonright)).BeginInit();
            this.pcbottonright.SuspendLayout();
            this.SuspendLayout();
            // 
            // pagePanel
            // 
            this.pagePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pagePanel.Location = new System.Drawing.Point(173, 0);
            this.pagePanel.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.pagePanel.Name = "pagePanel";
            this.pagePanel.Size = new System.Drawing.Size(364, 236);
            this.pagePanel.TabIndex = 7;
            // 
            // leftPanel
            // 
            this.leftPanel.Controls.Add(this.mozPane1);
            this.leftPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.leftPanel.Location = new System.Drawing.Point(0, 0);
            this.leftPanel.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.leftPanel.Name = "leftPanel";
            this.leftPanel.Padding = new System.Windows.Forms.Padding(13);
            this.leftPanel.Size = new System.Drawing.Size(173, 236);
            this.leftPanel.TabIndex = 8;
            // 
            // mozPane1
            // 
            this.mozPane1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mozPane1.ImageList = null;
            this.mozPane1.Location = new System.Drawing.Point(13, 13);
            this.mozPane1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.mozPane1.Name = "mozPane1";
            this.mozPane1.Size = new System.Drawing.Size(147, 210);
            this.mozPane1.TabIndex = 0;
            this.mozPane1.ItemClick += new JCodes.Framework.CommonControl.Settings.MozItemClickEventHandler(this.mozPane1_ItemClick);
            // 
            // bottomPanel
            // 
            this.bottomPanel.Controls.Add(this.meChangeContent);
            this.bottomPanel.Controls.Add(this.pcbottonright);
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel.Location = new System.Drawing.Point(0, 236);
            this.bottomPanel.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(537, 81);
            this.bottomPanel.TabIndex = 6;
            // 
            // meChangeContent
            // 
            this.meChangeContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.meChangeContent.Location = new System.Drawing.Point(0, 0);
            this.meChangeContent.Name = "meChangeContent";
            this.meChangeContent.Size = new System.Drawing.Size(139, 81);
            this.meChangeContent.TabIndex = 1;
            this.meChangeContent.UseOptimizedRendering = true;
            // 
            // pcbottonright
            // 
            this.pcbottonright.Controls.Add(this.btnApply);
            this.pcbottonright.Controls.Add(this.btnCancel);
            this.pcbottonright.Controls.Add(this.btnSearch);
            this.pcbottonright.Dock = System.Windows.Forms.DockStyle.Right;
            this.pcbottonright.Location = new System.Drawing.Point(139, 0);
            this.pcbottonright.Name = "pcbottonright";
            this.pcbottonright.Size = new System.Drawing.Size(398, 81);
            this.pcbottonright.TabIndex = 0;
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnApply.Location = new System.Drawing.Point(269, 22);
            this.btnApply.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(125, 42);
            this.btnApply.TabIndex = 7;
            this.btnApply.Text = "应用(&A)";
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(138, 22);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(125, 42);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSearch.Location = new System.Drawing.Point(7, 22);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(125, 42);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "查找";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // FirefoxDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pagePanel);
            this.Controls.Add(this.leftPanel);
            this.Controls.Add(this.bottomPanel);
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "FirefoxDialog";
            this.Size = new System.Drawing.Size(537, 317);
            this.leftPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mozPane1)).EndInit();
            this.bottomPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.meChangeContent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbottonright)).EndInit();
            this.pcbottonright.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pagePanel;
		private System.Windows.Forms.Panel leftPanel;
        private JCodes.Framework.CommonControl.Settings.MozPane mozPane1;
        private System.Windows.Forms.Panel bottomPanel;
        private DevExpress.XtraEditors.PanelControl pcbottonright;
        private DevExpress.XtraEditors.SimpleButton btnApply;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.MemoEdit meChangeContent;

	}
}
