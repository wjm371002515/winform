namespace JCodes.Framework.AddIn.SmallTools
{
    partial class xlsDataDeal
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
            this.btnDeal = new System.Windows.Forms.Button();
            this.btnChoose = new System.Windows.Forms.Button();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.lblPath = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.gcViewData = new DevExpress.XtraGrid.GridControl();
            this.gcDataView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.gcFileName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDealStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcViewData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDataView)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnDeal);
            this.panel1.Controls.Add(this.btnChoose);
            this.panel1.Controls.Add(this.txtPath);
            this.panel1.Controls.Add(this.lblPath);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1282, 49);
            this.panel1.TabIndex = 0;
            // 
            // btnDeal
            // 
            this.btnDeal.Location = new System.Drawing.Point(876, 5);
            this.btnDeal.Name = "btnDeal";
            this.btnDeal.Size = new System.Drawing.Size(106, 38);
            this.btnDeal.TabIndex = 2;
            this.btnDeal.Text = "处理";
            this.btnDeal.UseVisualStyleBackColor = true;
            this.btnDeal.Click += new System.EventHandler(this.btnDeal_Click);
            // 
            // btnChoose
            // 
            this.btnChoose.Location = new System.Drawing.Point(753, 5);
            this.btnChoose.Name = "btnChoose";
            this.btnChoose.Size = new System.Drawing.Size(106, 38);
            this.btnChoose.TabIndex = 2;
            this.btnChoose.Text = "选择路径";
            this.btnChoose.UseVisualStyleBackColor = true;
            this.btnChoose.Click += new System.EventHandler(this.btnChoose_Click);
            // 
            // txtPath
            // 
            this.txtPath.Enabled = false;
            this.txtPath.Location = new System.Drawing.Point(151, 11);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(582, 28);
            this.txtPath.TabIndex = 1;
            // 
            // lblPath
            // 
            this.lblPath.AutoSize = true;
            this.lblPath.Location = new System.Drawing.Point(21, 18);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(125, 18);
            this.lblPath.TabIndex = 0;
            this.lblPath.Text = "xls文件夹路径";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 49);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.gcViewData);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.rtbLog);
            this.splitContainer1.Size = new System.Drawing.Size(1282, 881);
            this.splitContainer1.SplitterDistance = 661;
            this.splitContainer1.TabIndex = 1;
            // 
            // gcViewData
            // 
            this.gcViewData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcViewData.Location = new System.Drawing.Point(0, 0);
            this.gcViewData.MainView = this.gcDataView;
            this.gcViewData.Name = "gcViewData";
            this.gcViewData.Size = new System.Drawing.Size(661, 881);
            this.gcViewData.TabIndex = 0;
            this.gcViewData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gcDataView});
            // 
            // gcDataView
            // 
            this.gcDataView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcFileName,
            this.gcDealStatus});
            this.gcDataView.GridControl = this.gcViewData;
            this.gcDataView.Name = "gcDataView";
            this.gcDataView.OptionsView.ShowGroupPanel = false;
            this.gcDataView.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.gcDataView_CustomColumnDisplayText);
            // 
            // rtbLog
            // 
            this.rtbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbLog.Location = new System.Drawing.Point(0, 0);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.Size = new System.Drawing.Size(617, 881);
            this.rtbLog.TabIndex = 0;
            this.rtbLog.Text = "";
            // 
            // gcFileName
            // 
            this.gcFileName.Caption = "文件名";
            this.gcFileName.FieldName = "FileName";
            this.gcFileName.Name = "gcFileName";
            this.gcFileName.Visible = true;
            this.gcFileName.VisibleIndex = 0;
            // 
            // gcDealStatus
            // 
            this.gcDealStatus.Caption = "处理状态";
            this.gcDealStatus.FieldName = "DealStatus";
            this.gcDealStatus.MaxWidth = 70;
            this.gcDealStatus.MinWidth = 70;
            this.gcDealStatus.Name = "gcDealStatus";
            this.gcDealStatus.Visible = true;
            this.gcDealStatus.VisibleIndex = 1;
            this.gcDealStatus.Width = 70;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1282, 930);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Name = "frmMain";
            this.Text = "处理xls表格数据";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcViewData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDataView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevExpress.XtraGrid.GridControl gcViewData;
        private DevExpress.XtraGrid.Views.Grid.GridView gcDataView;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.Label lblPath;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button btnChoose;
        private System.Windows.Forms.Button btnDeal;
        private DevExpress.XtraGrid.Columns.GridColumn gcFileName;
        private DevExpress.XtraGrid.Columns.GridColumn gcDealStatus;
    }
}

