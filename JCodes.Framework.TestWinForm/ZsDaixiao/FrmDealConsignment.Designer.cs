namespace JCodes.Framework.TestWinForm.ZsDaixiao
{
    partial class FrmDealConsignment
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
            this.btnCheck = new DevExpress.XtraEditors.SimpleButton();
            this.txtcheckPath = new DevExpress.XtraEditors.TextEdit();
            this.btnListen = new DevExpress.XtraEditors.SimpleButton();
            this.txtListen = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtListenTipMsg = new DevExpress.XtraEditors.TextEdit();
            this.btnCancelSpeek = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.checkEdit1 = new DevExpress.XtraEditors.CheckEdit();
            this.txtTDay = new DevExpress.XtraEditors.DateEdit();
            this.txtT1Day = new DevExpress.XtraEditors.DateEdit();
            this.btnFirstStep = new DevExpress.XtraEditors.SimpleButton();
            this.btnSecondStep = new DevExpress.XtraEditors.SimpleButton();
            this.btnThirdStep = new DevExpress.XtraEditors.SimpleButton();
            this.btnForthStep = new DevExpress.XtraEditors.SimpleButton();
            this.btnFifthStep = new DevExpress.XtraEditors.SimpleButton();
            this.btnSixthStep = new DevExpress.XtraEditors.SimpleButton();
            this.btnBuildOk = new DevExpress.XtraEditors.SimpleButton();
            this.txtBuildOk = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtBuild004Ok = new DevExpress.XtraEditors.TextEdit();
            this.txtBuild012Ok = new DevExpress.XtraEditors.TextEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.txtExcludeItems = new DevExpress.XtraEditors.TextEdit();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dpLog = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.memoEdit1 = new DevExpress.XtraEditors.MemoEdit();
            this.checkEdit2 = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.txtFirstStep = new DevExpress.XtraEditors.TextEdit();
            this.txtSecondStep = new DevExpress.XtraEditors.TextEdit();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.txtThirdStep = new DevExpress.XtraEditors.TextEdit();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.txtForthStep = new DevExpress.XtraEditors.TextEdit();
            this.labelControl13 = new DevExpress.XtraEditors.LabelControl();
            this.txtFifthStep = new DevExpress.XtraEditors.TextEdit();
            this.labelControl14 = new DevExpress.XtraEditors.LabelControl();
            this.txtSixthStep = new DevExpress.XtraEditors.TextEdit();
            this.labelControl15 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtcheckPath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtListen.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtListenTipMsg.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTDay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTDay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtT1Day.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtT1Day.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBuildOk.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBuild004Ok.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBuild012Ok.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExcludeItems.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dpLog.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFirstStep.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSecondStep.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtThirdStep.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtForthStep.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFifthStep.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSixthStep.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCheck
            // 
            this.btnCheck.Location = new System.Drawing.Point(23, 68);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(220, 41);
            this.btnCheck.TabIndex = 0;
            this.btnCheck.Text = "0. 检查代销是否齐全";
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // txtcheckPath
            // 
            this.txtcheckPath.EditValue = "H:\\代销\\yyyyMMdd_recv";
            this.txtcheckPath.Location = new System.Drawing.Point(343, 75);
            this.txtcheckPath.Name = "txtcheckPath";
            this.txtcheckPath.Size = new System.Drawing.Size(969, 28);
            this.txtcheckPath.TabIndex = 1;
            this.txtcheckPath.EditValueChanged += new System.EventHandler(this.txtcheckPath_EditValueChanged);
            // 
            // btnListen
            // 
            this.btnListen.Location = new System.Drawing.Point(23, 139);
            this.btnListen.Name = "btnListen";
            this.btnListen.Size = new System.Drawing.Size(220, 41);
            this.btnListen.TabIndex = 2;
            this.btnListen.Text = "0. 监听代销是否齐全";
            this.btnListen.Click += new System.EventHandler(this.btnListen_Click);
            // 
            // txtListen
            // 
            this.txtListen.EditValue = "H:\\代销\\yyyyMMdd_recv";
            this.txtListen.Location = new System.Drawing.Point(343, 136);
            this.txtListen.Name = "txtListen";
            this.txtListen.Size = new System.Drawing.Size(969, 28);
            this.txtListen.TabIndex = 1;
            this.txtListen.EditValueChanged += new System.EventHandler(this.txtListen_EditValueChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(255, 78);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(72, 22);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "检查路径";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(255, 139);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(72, 22);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "监听路径";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(183, 195);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(144, 22);
            this.labelControl3.TabIndex = 3;
            this.labelControl3.Text = "监听完成提示内容";
            // 
            // txtListenTipMsg
            // 
            this.txtListenTipMsg.EditValue = "代销数据已全部收齐";
            this.txtListenTipMsg.Location = new System.Drawing.Point(343, 192);
            this.txtListenTipMsg.Name = "txtListenTipMsg";
            this.txtListenTipMsg.Size = new System.Drawing.Size(668, 28);
            this.txtListenTipMsg.TabIndex = 1;
            this.txtListenTipMsg.EditValueChanged += new System.EventHandler(this.txt_EditValueChanged);
            // 
            // btnCancelSpeek
            // 
            this.btnCancelSpeek.Location = new System.Drawing.Point(1017, 185);
            this.btnCancelSpeek.Name = "btnCancelSpeek";
            this.btnCancelSpeek.Size = new System.Drawing.Size(295, 41);
            this.btnCancelSpeek.TabIndex = 2;
            this.btnCancelSpeek.Text = "停止播放";
            this.btnCancelSpeek.Click += new System.EventHandler(this.btnCancelSpeek_Click);
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(41, 21);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(72, 22);
            this.labelControl4.TabIndex = 3;
            this.labelControl4.Text = "当前日期";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(435, 24);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(70, 22);
            this.labelControl5.TabIndex = 3;
            this.labelControl5.Text = "T+1日期";
            // 
            // checkEdit1
            // 
            this.checkEdit1.Location = new System.Drawing.Point(748, 23);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Caption = "手工修改日期";
            this.checkEdit1.Size = new System.Drawing.Size(138, 26);
            this.checkEdit1.TabIndex = 6;
            this.checkEdit1.CheckedChanged += new System.EventHandler(this.checkEdit1_CheckedChanged);
            // 
            // txtTDay
            // 
            this.txtTDay.EditValue = null;
            this.txtTDay.Enabled = false;
            this.txtTDay.Location = new System.Drawing.Point(119, 18);
            this.txtTDay.Name = "txtTDay";
            this.txtTDay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtTDay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtTDay.Properties.DisplayFormat.FormatString = "yyyyMMdd";
            this.txtTDay.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtTDay.Properties.Mask.EditMask = "yyyyMMdd";
            this.txtTDay.Size = new System.Drawing.Size(187, 28);
            this.txtTDay.TabIndex = 7;
            this.txtTDay.EditValueChanged += new System.EventHandler(this.txtTDay_EditValueChanged);
            // 
            // txtT1Day
            // 
            this.txtT1Day.EditValue = null;
            this.txtT1Day.Enabled = false;
            this.txtT1Day.Location = new System.Drawing.Point(511, 21);
            this.txtT1Day.Name = "txtT1Day";
            this.txtT1Day.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtT1Day.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtT1Day.Properties.DisplayFormat.FormatString = "yyyyMMdd";
            this.txtT1Day.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtT1Day.Properties.Mask.EditMask = "yyyyMMdd";
            this.txtT1Day.Size = new System.Drawing.Size(187, 28);
            this.txtT1Day.TabIndex = 7;
            // 
            // btnFirstStep
            // 
            this.btnFirstStep.Location = new System.Drawing.Point(23, 258);
            this.btnFirstStep.Name = "btnFirstStep";
            this.btnFirstStep.Size = new System.Drawing.Size(220, 41);
            this.btnFirstStep.TabIndex = 2;
            this.btnFirstStep.Text = "1-第一步接收申请文件";
            this.btnFirstStep.Click += new System.EventHandler(this.btnFirstStep_Click);
            // 
            // btnSecondStep
            // 
            this.btnSecondStep.Location = new System.Drawing.Point(23, 316);
            this.btnSecondStep.Name = "btnSecondStep";
            this.btnSecondStep.Size = new System.Drawing.Size(220, 41);
            this.btnSecondStep.TabIndex = 2;
            this.btnSecondStep.Text = "2-第二步发送行情数据";
            this.btnSecondStep.Click += new System.EventHandler(this.btnSecondStep_Click);
            // 
            // btnThirdStep
            // 
            this.btnThirdStep.Location = new System.Drawing.Point(23, 374);
            this.btnThirdStep.Name = "btnThirdStep";
            this.btnThirdStep.Size = new System.Drawing.Size(220, 41);
            this.btnThirdStep.TabIndex = 2;
            this.btnThirdStep.Text = "3-第三步发送确认数据";
            this.btnThirdStep.Click += new System.EventHandler(this.btnThirdStep_Click);
            // 
            // btnForthStep
            // 
            this.btnForthStep.Location = new System.Drawing.Point(23, 432);
            this.btnForthStep.Name = "btnForthStep";
            this.btnForthStep.Size = new System.Drawing.Size(220, 41);
            this.btnForthStep.TabIndex = 2;
            this.btnForthStep.Text = "4-第四步发送托管行数据";
            this.btnForthStep.Click += new System.EventHandler(this.btnForthStep_Click);
            // 
            // btnFifthStep
            // 
            this.btnFifthStep.Location = new System.Drawing.Point(23, 495);
            this.btnFifthStep.Name = "btnFifthStep";
            this.btnFifthStep.Size = new System.Drawing.Size(220, 41);
            this.btnFifthStep.TabIndex = 2;
            this.btnFifthStep.Text = "5-第五步上传自建TA数据";
            this.btnFifthStep.Click += new System.EventHandler(this.btnFifthStep_Click);
            // 
            // btnSixthStep
            // 
            this.btnSixthStep.Location = new System.Drawing.Point(23, 558);
            this.btnSixthStep.Name = "btnSixthStep";
            this.btnSixthStep.Size = new System.Drawing.Size(220, 41);
            this.btnSixthStep.TabIndex = 2;
            this.btnSixthStep.Text = "6-第六步发送OTC转让数据";
            this.btnSixthStep.Click += new System.EventHandler(this.btnSixthStep_Click);
            // 
            // btnBuildOk
            // 
            this.btnBuildOk.Location = new System.Drawing.Point(23, 623);
            this.btnBuildOk.Name = "btnBuildOk";
            this.btnBuildOk.Size = new System.Drawing.Size(220, 41);
            this.btnBuildOk.TabIndex = 2;
            this.btnBuildOk.Text = "7. 生成OK标志";
            this.btnBuildOk.Click += new System.EventHandler(this.btnBuildOk_Click);
            // 
            // txtBuildOk
            // 
            this.txtBuildOk.EditValue = "H:\\代销\\yyyyMMdd";
            this.txtBuildOk.Location = new System.Drawing.Point(343, 663);
            this.txtBuildOk.Name = "txtBuildOk";
            this.txtBuildOk.Size = new System.Drawing.Size(788, 28);
            this.txtBuildOk.TabIndex = 1;
            this.txtBuildOk.EditValueChanged += new System.EventHandler(this.txt_EditValueChanged);
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(255, 669);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(72, 22);
            this.labelControl6.TabIndex = 3;
            this.labelControl6.Text = "生成路径";
            // 
            // txtBuild004Ok
            // 
            this.txtBuild004Ok.EditValue = "H:\\代销\\3T_004_hq";
            this.txtBuild004Ok.Location = new System.Drawing.Point(343, 700);
            this.txtBuild004Ok.Name = "txtBuild004Ok";
            this.txtBuild004Ok.Size = new System.Drawing.Size(788, 28);
            this.txtBuild004Ok.TabIndex = 1;
            this.txtBuild004Ok.EditValueChanged += new System.EventHandler(this.txt_EditValueChanged);
            // 
            // txtBuild012Ok
            // 
            this.txtBuild012Ok.EditValue = "H:\\代销\\3T_012_HQ";
            this.txtBuild012Ok.Location = new System.Drawing.Point(343, 737);
            this.txtBuild012Ok.Name = "txtBuild012Ok";
            this.txtBuild012Ok.Size = new System.Drawing.Size(788, 28);
            this.txtBuild012Ok.TabIndex = 1;
            this.txtBuild012Ok.EditValueChanged += new System.EventHandler(this.txt_EditValueChanged);
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(236, 703);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(91, 22);
            this.labelControl7.TabIndex = 3;
            this.labelControl7.Text = "3T_004_hq";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(231, 737);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(96, 22);
            this.labelControl8.TabIndex = 3;
            this.labelControl8.Text = "3T_012_HQ";
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(273, 775);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(54, 22);
            this.labelControl9.TabIndex = 3;
            this.labelControl9.Text = "排除项";
            // 
            // txtExcludeItems
            // 
            this.txtExcludeItems.EditValue = "zszq_pcf,512190";
            this.txtExcludeItems.Location = new System.Drawing.Point(343, 772);
            this.txtExcludeItems.Name = "txtExcludeItems";
            this.txtExcludeItems.Size = new System.Drawing.Size(788, 28);
            this.txtExcludeItems.TabIndex = 1;
            this.txtExcludeItems.EditValueChanged += new System.EventHandler(this.txt_EditValueChanged);
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
            // checkEdit2
            // 
            this.checkEdit2.EditValue = true;
            this.checkEdit2.Location = new System.Drawing.Point(255, 631);
            this.checkEdit2.Name = "checkEdit2";
            this.checkEdit2.Properties.Caption = "生成详细日志清单";
            this.checkEdit2.Size = new System.Drawing.Size(210, 26);
            this.checkEdit2.TabIndex = 6;
            this.checkEdit2.CheckedChanged += new System.EventHandler(this.checkEdit1_CheckedChanged);
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(255, 268);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(141, 22);
            this.labelControl10.TabIndex = 3;
            this.labelControl10.Text = "接收申请文件BAT";
            // 
            // txtFirstStep
            // 
            this.txtFirstStep.EditValue = "代销处理\\1-第一步接收申请文件.bat";
            this.txtFirstStep.Location = new System.Drawing.Point(435, 265);
            this.txtFirstStep.Name = "txtFirstStep";
            this.txtFirstStep.Size = new System.Drawing.Size(737, 28);
            this.txtFirstStep.TabIndex = 1;
            this.txtFirstStep.EditValueChanged += new System.EventHandler(this.txt_EditValueChanged);
            // 
            // txtSecondStep
            // 
            this.txtSecondStep.EditValue = "代销处理\\2-第二步发送行情数据.bat";
            this.txtSecondStep.Location = new System.Drawing.Point(435, 323);
            this.txtSecondStep.Name = "txtSecondStep";
            this.txtSecondStep.Size = new System.Drawing.Size(737, 28);
            this.txtSecondStep.TabIndex = 1;
            this.txtSecondStep.EditValueChanged += new System.EventHandler(this.txt_EditValueChanged);
            // 
            // labelControl11
            // 
            this.labelControl11.Location = new System.Drawing.Point(255, 326);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(141, 22);
            this.labelControl11.TabIndex = 3;
            this.labelControl11.Text = "发送行情数据BAT";
            // 
            // txtThirdStep
            // 
            this.txtThirdStep.EditValue = "代销处理\\3-第三步发送确认数据.bat";
            this.txtThirdStep.Location = new System.Drawing.Point(435, 381);
            this.txtThirdStep.Name = "txtThirdStep";
            this.txtThirdStep.Size = new System.Drawing.Size(737, 28);
            this.txtThirdStep.TabIndex = 1;
            this.txtThirdStep.EditValueChanged += new System.EventHandler(this.txt_EditValueChanged);
            // 
            // labelControl12
            // 
            this.labelControl12.Location = new System.Drawing.Point(255, 384);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(141, 22);
            this.labelControl12.TabIndex = 3;
            this.labelControl12.Text = "发送确认数据BAT";
            // 
            // txtForthStep
            // 
            this.txtForthStep.EditValue = "代销处理\\4-第四步发送托管行数据.bat";
            this.txtForthStep.Location = new System.Drawing.Point(435, 439);
            this.txtForthStep.Name = "txtForthStep";
            this.txtForthStep.Size = new System.Drawing.Size(737, 28);
            this.txtForthStep.TabIndex = 1;
            this.txtForthStep.EditValueChanged += new System.EventHandler(this.txt_EditValueChanged);
            // 
            // labelControl13
            // 
            this.labelControl13.Location = new System.Drawing.Point(255, 442);
            this.labelControl13.Name = "labelControl13";
            this.labelControl13.Size = new System.Drawing.Size(159, 22);
            this.labelControl13.TabIndex = 3;
            this.labelControl13.Text = "发送托管行数据BAT";
            // 
            // txtFifthStep
            // 
            this.txtFifthStep.EditValue = "代销处理\\5-第五步上传自建TA数据.bat";
            this.txtFifthStep.Location = new System.Drawing.Point(435, 502);
            this.txtFifthStep.Name = "txtFifthStep";
            this.txtFifthStep.Size = new System.Drawing.Size(737, 28);
            this.txtFifthStep.TabIndex = 1;
            this.txtFifthStep.EditValueChanged += new System.EventHandler(this.txt_EditValueChanged);
            // 
            // labelControl14
            // 
            this.labelControl14.Location = new System.Drawing.Point(255, 505);
            this.labelControl14.Name = "labelControl14";
            this.labelControl14.Size = new System.Drawing.Size(163, 22);
            this.labelControl14.TabIndex = 3;
            this.labelControl14.Text = "上传自建TA数据BAT";
            // 
            // txtSixthStep
            // 
            this.txtSixthStep.EditValue = "代销处理\\6-第六步发送OTC转让数据.bat";
            this.txtSixthStep.Location = new System.Drawing.Point(435, 565);
            this.txtSixthStep.Name = "txtSixthStep";
            this.txtSixthStep.Size = new System.Drawing.Size(737, 28);
            this.txtSixthStep.TabIndex = 1;
            this.txtSixthStep.EditValueChanged += new System.EventHandler(this.txt_EditValueChanged);
            // 
            // labelControl15
            // 
            this.labelControl15.Location = new System.Drawing.Point(255, 568);
            this.labelControl15.Name = "labelControl15";
            this.labelControl15.Size = new System.Drawing.Size(176, 22);
            this.labelControl15.TabIndex = 3;
            this.labelControl15.Text = "发送OTC转让数据BAT";
            // 
            // FrmDealConsignment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1466, 1047);
            this.Controls.Add(this.txtT1Day);
            this.Controls.Add(this.txtTDay);
            this.Controls.Add(this.checkEdit2);
            this.Controls.Add(this.checkEdit1);
            this.Controls.Add(this.labelControl9);
            this.Controls.Add(this.labelControl8);
            this.Controls.Add(this.labelControl7);
            this.Controls.Add(this.labelControl15);
            this.Controls.Add(this.labelControl14);
            this.Controls.Add(this.labelControl13);
            this.Controls.Add(this.labelControl12);
            this.Controls.Add(this.labelControl11);
            this.Controls.Add(this.labelControl10);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.btnCancelSpeek);
            this.Controls.Add(this.btnBuildOk);
            this.Controls.Add(this.btnSixthStep);
            this.Controls.Add(this.btnFifthStep);
            this.Controls.Add(this.btnForthStep);
            this.Controls.Add(this.btnThirdStep);
            this.Controls.Add(this.btnSecondStep);
            this.Controls.Add(this.btnFirstStep);
            this.Controls.Add(this.btnListen);
            this.Controls.Add(this.txtExcludeItems);
            this.Controls.Add(this.txtBuild012Ok);
            this.Controls.Add(this.txtBuild004Ok);
            this.Controls.Add(this.txtSixthStep);
            this.Controls.Add(this.txtFifthStep);
            this.Controls.Add(this.txtForthStep);
            this.Controls.Add(this.txtThirdStep);
            this.Controls.Add(this.txtSecondStep);
            this.Controls.Add(this.txtFirstStep);
            this.Controls.Add(this.txtBuildOk);
            this.Controls.Add(this.txtListenTipMsg);
            this.Controls.Add(this.txtListen);
            this.Controls.Add(this.txtcheckPath);
            this.Controls.Add(this.btnCheck);
            this.Controls.Add(this.dpLog);
            this.Name = "FrmDealConsignment";
            this.Text = "处理代销";
            ((System.ComponentModel.ISupportInitialize)(this.txtcheckPath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtListen.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtListenTipMsg.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTDay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTDay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtT1Day.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtT1Day.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBuildOk.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBuild004Ok.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBuild012Ok.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExcludeItems.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dpLog.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFirstStep.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSecondStep.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtThirdStep.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtForthStep.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFifthStep.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSixthStep.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnCheck;
        private DevExpress.XtraEditors.TextEdit txtcheckPath;
        private DevExpress.XtraEditors.SimpleButton btnListen;
        private DevExpress.XtraEditors.TextEdit txtListen;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtListenTipMsg;
        private DevExpress.XtraEditors.SimpleButton btnCancelSpeek;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.CheckEdit checkEdit1;
        private DevExpress.XtraEditors.DateEdit txtTDay;
        private DevExpress.XtraEditors.DateEdit txtT1Day;
        private DevExpress.XtraEditors.SimpleButton btnFirstStep;
        private DevExpress.XtraEditors.SimpleButton btnSecondStep;
        private DevExpress.XtraEditors.SimpleButton btnThirdStep;
        private DevExpress.XtraEditors.SimpleButton btnForthStep;
        private DevExpress.XtraEditors.SimpleButton btnFifthStep;
        private DevExpress.XtraEditors.SimpleButton btnSixthStep;
        private DevExpress.XtraEditors.SimpleButton btnBuildOk;
        private DevExpress.XtraEditors.TextEdit txtBuildOk;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit txtBuild004Ok;
        private DevExpress.XtraEditors.TextEdit txtBuild012Ok;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.TextEdit txtExcludeItems;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Docking.DockPanel dpLog;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraEditors.MemoEdit memoEdit1;
        private DevExpress.XtraEditors.CheckEdit checkEdit2;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.TextEdit txtFirstStep;
        private DevExpress.XtraEditors.LabelControl labelControl15;
        private DevExpress.XtraEditors.LabelControl labelControl14;
        private DevExpress.XtraEditors.LabelControl labelControl13;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraEditors.TextEdit txtSixthStep;
        private DevExpress.XtraEditors.TextEdit txtFifthStep;
        private DevExpress.XtraEditors.TextEdit txtForthStep;
        private DevExpress.XtraEditors.TextEdit txtThirdStep;
        private DevExpress.XtraEditors.TextEdit txtSecondStep;
    }
}