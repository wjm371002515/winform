namespace JCodes.Framework.TestWinForm.Haotian
{
    partial class FrmHackVote
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
            this.txtURL = new DevExpress.XtraEditors.TextEdit();
            this.txtParam = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.btnDeal = new DevExpress.XtraEditors.SimpleButton();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dpLog = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.memoEdit1 = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtStartUserId = new DevExpress.XtraEditors.TextEdit();
            this.txtEndUserId = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtURL.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtParam.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dpLog.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStartUserId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEndUserId.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtURL
            // 
            this.txtURL.EditValue = "https://ynzx.zgwyzxw.cn/index.php/home/login/getUser";
            this.txtURL.Location = new System.Drawing.Point(220, 35);
            this.txtURL.Name = "txtURL";
            this.txtURL.Size = new System.Drawing.Size(969, 28);
            this.txtURL.TabIndex = 2;
            // 
            // txtParam
            // 
            this.txtParam.EditValue = "openid=false&userId={userId}";
            this.txtParam.Location = new System.Drawing.Point(220, 78);
            this.txtParam.Name = "txtParam";
            this.txtParam.Size = new System.Drawing.Size(969, 28);
            this.txtParam.TabIndex = 3;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(95, 38);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(104, 22);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "请求URL地址";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(119, 81);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(80, 22);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "POST参数";
            // 
            // btnDeal
            // 
            this.btnDeal.Location = new System.Drawing.Point(1229, 38);
            this.btnDeal.Name = "btnDeal";
            this.btnDeal.Size = new System.Drawing.Size(173, 136);
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
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(119, 128);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(88, 22);
            this.labelControl3.TabIndex = 3;
            this.labelControl3.Text = "开始UserId";
            // 
            // txtStartUserId
            // 
            this.txtStartUserId.EditValue = "37614";
            this.txtStartUserId.Location = new System.Drawing.Point(220, 125);
            this.txtStartUserId.Name = "txtStartUserId";
            this.txtStartUserId.Size = new System.Drawing.Size(182, 28);
            this.txtStartUserId.TabIndex = 3;
            // 
            // txtEndUserId
            // 
            this.txtEndUserId.EditValue = "37614";
            this.txtEndUserId.Location = new System.Drawing.Point(533, 125);
            this.txtEndUserId.Name = "txtEndUserId";
            this.txtEndUserId.Size = new System.Drawing.Size(182, 28);
            this.txtEndUserId.TabIndex = 3;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(432, 128);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(88, 22);
            this.labelControl4.TabIndex = 3;
            this.labelControl4.Text = "结束UserId";
            // 
            // FrmHackVote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1466, 1047);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.btnDeal);
            this.Controls.Add(this.txtEndUserId);
            this.Controls.Add(this.txtStartUserId);
            this.Controls.Add(this.txtParam);
            this.Controls.Add(this.txtURL);
            this.Controls.Add(this.dpLog);
            this.Name = "FrmHackVote";
            this.Text = "浩天网络XLS数据处理";
            ((System.ComponentModel.ISupportInitialize)(this.txtURL.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtParam.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dpLog.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStartUserId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEndUserId.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtURL;
        private DevExpress.XtraEditors.TextEdit txtParam;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton btnDeal;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Docking.DockPanel dpLog;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraEditors.MemoEdit memoEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtEndUserId;
        private DevExpress.XtraEditors.TextEdit txtStartUserId;
    }
}