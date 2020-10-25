namespace JCodes.Framework.TestWinForm.ZsDaixiao
{
    partial class FrmDealPassword
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
            this.txtNum = new DevExpress.XtraEditors.TextEdit();
            this.btnBuild = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.ceLower = new DevExpress.XtraEditors.CheckEdit();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dpLog = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.memoEdit1 = new DevExpress.XtraEditors.MemoEdit();
            this.ceUpper = new DevExpress.XtraEditors.CheckEdit();
            this.ceSpecChars = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtFilePath = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtChars = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceLower.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dpLog.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceUpper.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceSpecChars.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFilePath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtChars.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtNum
            // 
            this.txtNum.EditValue = "";
            this.txtNum.Location = new System.Drawing.Point(143, 66);
            this.txtNum.Name = "txtNum";
            this.txtNum.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtNum.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtNum.Size = new System.Drawing.Size(136, 28);
            this.txtNum.TabIndex = 1;
            // 
            // btnBuild
            // 
            this.btnBuild.Location = new System.Drawing.Point(93, 186);
            this.btnBuild.Name = "btnBuild";
            this.btnBuild.Size = new System.Drawing.Size(133, 41);
            this.btnBuild.TabIndex = 2;
            this.btnBuild.Text = "一键生成";
            this.btnBuild.Click += new System.EventHandler(this.btnBuild_Click);
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(41, 21);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(78, 22);
            this.labelControl4.TabIndex = 3;
            this.labelControl4.Text = "所用字符:";
            // 
            // ceLower
            // 
            this.ceLower.Location = new System.Drawing.Point(206, 19);
            this.ceLower.Name = "ceLower";
            this.ceLower.Properties.Caption = "a-z";
            this.ceLower.Size = new System.Drawing.Size(57, 26);
            this.ceLower.TabIndex = 6;
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
            this.dpLog.Location = new System.Drawing.Point(0, 839);
            this.dpLog.Name = "dpLog";
            this.dpLog.OriginalSize = new System.Drawing.Size(200, 208);
            this.dpLog.Size = new System.Drawing.Size(1466, 208);
            this.dpLog.Text = "日志";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.memoEdit1);
            this.dockPanel1_Container.Location = new System.Drawing.Point(4, 30);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(1458, 174);
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
            this.memoEdit1.Size = new System.Drawing.Size(1458, 174);
            this.memoEdit1.TabIndex = 0;
            this.memoEdit1.UseOptimizedRendering = true;
            // 
            // ceUpper
            // 
            this.ceUpper.Location = new System.Drawing.Point(143, 19);
            this.ceUpper.Name = "ceUpper";
            this.ceUpper.Properties.Caption = "A-Z";
            this.ceUpper.Size = new System.Drawing.Size(57, 26);
            this.ceUpper.TabIndex = 6;
            // 
            // ceSpecChars
            // 
            this.ceSpecChars.Location = new System.Drawing.Point(269, 19);
            this.ceSpecChars.Name = "ceSpecChars";
            this.ceSpecChars.Properties.Caption = " ";
            this.ceSpecChars.Size = new System.Drawing.Size(27, 26);
            this.ceSpecChars.TabIndex = 6;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(41, 69);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(78, 22);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "密码长度:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(294, 69);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(18, 22);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "位";
            // 
            // txtFilePath
            // 
            this.txtFilePath.EditValue = "H:\\2019年第一季度\\";
            this.txtFilePath.Location = new System.Drawing.Point(143, 116);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtFilePath.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtFilePath.Size = new System.Drawing.Size(371, 28);
            this.txtFilePath.TabIndex = 1;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(23, 122);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(96, 22);
            this.labelControl3.TabIndex = 3;
            this.labelControl3.Text = "文件夹路径:";
            // 
            // txtChars
            // 
            this.txtChars.EditValue = "!@#$%^&*";
            this.txtChars.Location = new System.Drawing.Point(294, 15);
            this.txtChars.Name = "txtChars";
            this.txtChars.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtChars.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtChars.Size = new System.Drawing.Size(136, 28);
            this.txtChars.TabIndex = 1;
            // 
            // FrmDealPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1466, 1047);
            this.Controls.Add(this.ceUpper);
            this.Controls.Add(this.ceSpecChars);
            this.Controls.Add(this.ceLower);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.btnBuild);
            this.Controls.Add(this.txtFilePath);
            this.Controls.Add(this.txtChars);
            this.Controls.Add(this.txtNum);
            this.Controls.Add(this.dpLog);
            this.Name = "FrmDealPassword";
            this.Text = "密码生成器";
            ((System.ComponentModel.ISupportInitialize)(this.txtNum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceLower.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dpLog.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceUpper.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceSpecChars.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFilePath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtChars.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtNum;
        private DevExpress.XtraEditors.SimpleButton btnBuild;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.CheckEdit ceLower;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Docking.DockPanel dpLog;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraEditors.MemoEdit memoEdit1;
        private DevExpress.XtraEditors.CheckEdit ceUpper;
        private DevExpress.XtraEditors.CheckEdit ceSpecChars;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtFilePath;
        private DevExpress.XtraEditors.TextEdit txtChars;
    }
}