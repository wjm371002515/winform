namespace JCodes.Framework.TestWinForm.Haotian
{
    partial class FrmXlsDataDeal
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
            this.components = new System.ComponentModel.Container();
            this.txtReferenceFile = new DevExpress.XtraEditors.TextEdit();
            this.txtToDealFile = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.btnDeal = new DevExpress.XtraEditors.SimpleButton();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dpLog = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.memoEdit1 = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReferenceFile.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToDealFile.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dpLog.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtReferenceFile
            // 
            this.txtReferenceFile.EditValue = "";
            this.txtReferenceFile.Location = new System.Drawing.Point(220, 35);
            this.txtReferenceFile.Name = "txtReferenceFile";
            this.txtReferenceFile.Size = new System.Drawing.Size(969, 28);
            this.txtReferenceFile.TabIndex = 2;
            this.txtReferenceFile.Enter += new System.EventHandler(this.txt_Enter);
            // 
            // txtToDealFile
            // 
            this.txtToDealFile.EditValue = "";
            this.txtToDealFile.Location = new System.Drawing.Point(220, 78);
            this.txtToDealFile.Name = "txtToDealFile";
            this.txtToDealFile.Size = new System.Drawing.Size(969, 28);
            this.txtToDealFile.TabIndex = 3;
            this.txtToDealFile.Enter += new System.EventHandler(this.txt_Enter);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(95, 38);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(101, 22);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "参考XLS文件";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(41, 81);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(155, 22);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "需要处理的XLS文件";
            // 
            // btnDeal
            // 
            this.btnDeal.Location = new System.Drawing.Point(1229, 38);
            this.btnDeal.Name = "btnDeal";
            this.btnDeal.Size = new System.Drawing.Size(173, 65);
            this.btnDeal.TabIndex = 1;
            this.btnDeal.Text = "开始处理";
            this.btnDeal.Click += new System.EventHandler(this.btnDeal_Click);
            // 
            // dockManager1
            // 
            this.dockManager1.Form = this;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dpLog});
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "System.Windows.Forms.MenuStrip",
            "System.Windows.Forms.StatusStrip",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl",
            "DevExpress.XtraBars.Navigation.OfficeNavigationBar",
            "DevExpress.XtraBars.Navigation.TileNavPane"});
            // 
            // dpLog
            // 
            this.dpLog.Controls.Add(this.dockPanel1_Container);
            this.dpLog.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom;
            this.dpLog.ID = new System.Guid("bcacf253-c8a4-4d80-a428-7cdc09775351");
            this.dpLog.Location = new System.Drawing.Point(0, 262);
            this.dpLog.Name = "dpLog";
            this.dpLog.OriginalSize = new System.Drawing.Size(200, 785);
            this.dpLog.Size = new System.Drawing.Size(1466, 785);
            this.dpLog.Text = "日志";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.memoEdit1);
            this.dockPanel1_Container.Location = new System.Drawing.Point(4, 30);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(1458, 751);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // memoEdit1
            // 
            this.memoEdit1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memoEdit1.EditValue = "";
            this.memoEdit1.Location = new System.Drawing.Point(0, 0);
            this.memoEdit1.Name = "memoEdit1";
            this.memoEdit1.Properties.Appearance.Font = new System.Drawing.Font("Courier New", 8.25F);
            this.memoEdit1.Properties.Appearance.Options.UseFont = true;
            this.memoEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.memoEdit1.Properties.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.memoEdit1.Properties.WordWrap = false;
            this.memoEdit1.Size = new System.Drawing.Size(1458, 751);
            this.memoEdit1.TabIndex = 0;
            this.memoEdit1.UseOptimizedRendering = true;
            // 
            // FrmXlsDataDeal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1466, 1047);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.btnDeal);
            this.Controls.Add(this.txtToDealFile);
            this.Controls.Add(this.txtReferenceFile);
            this.Controls.Add(this.dpLog);
            this.Name = "FrmXlsDataDeal";
            this.Text = "浩天网络XLS数据处理";
            ((System.ComponentModel.ISupportInitialize)(this.txtReferenceFile.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToDealFile.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dpLog.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtReferenceFile;
        private DevExpress.XtraEditors.TextEdit txtToDealFile;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton btnDeal;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Docking.DockPanel dpLog;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraEditors.MemoEdit memoEdit1;
    }
}