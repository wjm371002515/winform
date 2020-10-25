namespace JCodes.Framework.Test
{
    partial class DakaDateSetFrm
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
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.TabPageJiTuan = new DevExpress.XtraTab.XtraTabPage();
            this.txtUrl = new DevExpress.XtraEditors.TextEdit();
            this.lblDakaStatus = new DevExpress.XtraEditors.LabelControl();
            this.lblDaKa = new DevExpress.XtraEditors.LabelControl();
            this.lblCurDayStr = new DevExpress.XtraEditors.LabelControl();
            this.lblUrl = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.TabPageWorkDay = new DevExpress.XtraTab.XtraTabPage();
            this.dateNavigator1 = new DevExpress.XtraScheduler.DateNavigator();
            this.TabPageJituanshezhi = new DevExpress.XtraTab.XtraTabPage();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryItemLookUpEditStdType = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.TabPageJiTuan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtUrl.Properties)).BeginInit();
            this.TabPageWorkDay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateNavigator1)).BeginInit();
            this.TabPageJituanshezhi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEditStdType)).BeginInit();
            this.SuspendLayout();
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.TabPageJiTuan;
            this.xtraTabControl1.Size = new System.Drawing.Size(579, 524);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.TabPageJiTuan,
            this.TabPageWorkDay,
            this.TabPageJituanshezhi});
            // 
            // TabPageJiTuan
            // 
            this.TabPageJiTuan.Controls.Add(this.txtUrl);
            this.TabPageJiTuan.Controls.Add(this.lblDakaStatus);
            this.TabPageJiTuan.Controls.Add(this.lblDaKa);
            this.TabPageJiTuan.Controls.Add(this.lblCurDayStr);
            this.TabPageJiTuan.Controls.Add(this.lblUrl);
            this.TabPageJiTuan.Controls.Add(this.simpleButton3);
            this.TabPageJiTuan.Controls.Add(this.simpleButton2);
            this.TabPageJiTuan.Controls.Add(this.simpleButton1);
            this.TabPageJiTuan.Name = "TabPageJiTuan";
            this.TabPageJiTuan.Size = new System.Drawing.Size(573, 487);
            this.TabPageJiTuan.Text = "集团打卡";
            // 
            // txtUrl
            // 
            this.txtUrl.EditValue = "http://www.cncico.group";
            this.txtUrl.Location = new System.Drawing.Point(185, 28);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(346, 28);
            this.txtUrl.TabIndex = 2;
            // 
            // lblDakaStatus
            // 
            this.lblDakaStatus.Location = new System.Drawing.Point(196, 124);
            this.lblDakaStatus.Name = "lblDakaStatus";
            this.lblDakaStatus.Size = new System.Drawing.Size(0, 22);
            this.lblDakaStatus.TabIndex = 1;
            // 
            // lblDaKa
            // 
            this.lblDaKa.Location = new System.Drawing.Point(196, 170);
            this.lblDaKa.Name = "lblDaKa";
            this.lblDaKa.Size = new System.Drawing.Size(72, 22);
            this.lblDaKa.TabIndex = 1;
            this.lblDaKa.Text = "等待打卡";
            // 
            // lblCurDayStr
            // 
            this.lblCurDayStr.Location = new System.Drawing.Point(196, 84);
            this.lblCurDayStr.Name = "lblCurDayStr";
            this.lblCurDayStr.Size = new System.Drawing.Size(72, 22);
            this.lblCurDayStr.TabIndex = 1;
            this.lblCurDayStr.Text = "等待查询";
            // 
            // lblUrl
            // 
            this.lblUrl.Location = new System.Drawing.Point(81, 31);
            this.lblUrl.Name = "lblUrl";
            this.lblUrl.Size = new System.Drawing.Size(72, 22);
            this.lblUrl.TabIndex = 1;
            this.lblUrl.Text = "集团域名";
            // 
            // simpleButton3
            // 
            this.simpleButton3.Location = new System.Drawing.Point(26, 292);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(148, 30);
            this.simpleButton3.TabIndex = 0;
            this.simpleButton3.Text = "自动打卡";
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(26, 165);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(148, 30);
            this.simpleButton2.TabIndex = 0;
            this.simpleButton2.Text = "当日打卡";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click_1);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(26, 76);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(148, 30);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "当日打卡查询";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // TabPageWorkDay
            // 
            this.TabPageWorkDay.Controls.Add(this.dateNavigator1);
            this.TabPageWorkDay.Name = "TabPageWorkDay";
            this.TabPageWorkDay.Size = new System.Drawing.Size(573, 487);
            this.TabPageWorkDay.Text = "日期维护";
            // 
            // dateNavigator1
            // 
            this.dateNavigator1.DateTime = new System.DateTime(2020, 4, 15, 0, 0, 0, 0);
            this.dateNavigator1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dateNavigator1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dateNavigator1.HotDate = null;
            this.dateNavigator1.Location = new System.Drawing.Point(0, 0);
            this.dateNavigator1.Name = "dateNavigator1";
            this.dateNavigator1.ShowTodayButton = false;
            this.dateNavigator1.Size = new System.Drawing.Size(573, 487);
            this.dateNavigator1.TabIndex = 10;
            // 
            // TabPageJituanshezhi
            // 
            this.TabPageJituanshezhi.Controls.Add(this.gridControl1);
            this.TabPageJituanshezhi.Name = "TabPageJituanshezhi";
            this.TabPageJituanshezhi.Size = new System.Drawing.Size(573, 487);
            this.TabPageJituanshezhi.Text = "打卡维护";
            // 
            // gridControl1
            // 
            this.gridControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemLookUpEditStdType});
            this.gridControl1.Size = new System.Drawing.Size(573, 487);
            this.gridControl1.TabIndex = 14;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.FocusedRow.BackColor = System.Drawing.Color.LightCyan;
            this.gridView1.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.LightCyan;
            this.gridView1.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gridView1.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsCustomization.AllowFilter = false;
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsMenu.EnableColumnMenu = false;
            this.gridView1.OptionsMenu.EnableFooterMenu = false;
            this.gridView1.OptionsMenu.EnableGroupPanelMenu = false;
            this.gridView1.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView1.OptionsView.EnableAppearanceOddRow = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // repositoryItemLookUpEditStdType
            // 
            this.repositoryItemLookUpEditStdType.AutoHeight = false;
            this.repositoryItemLookUpEditStdType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEditStdType.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Text", "字段类型")});
            this.repositoryItemLookUpEditStdType.DisplayMember = "Text";
            this.repositoryItemLookUpEditStdType.DropDownRows = 10;
            this.repositoryItemLookUpEditStdType.Name = "repositoryItemLookUpEditStdType";
            this.repositoryItemLookUpEditStdType.PopupWidth = 220;
            this.repositoryItemLookUpEditStdType.ValueMember = "Value";
            // 
            // DakaDateSetFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 524);
            this.Controls.Add(this.xtraTabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DakaDateSetFrm";
            this.Text = "打卡";
            this.Load += new System.EventHandler(this.DakaDateSetFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.TabPageJiTuan.ResumeLayout(false);
            this.TabPageJiTuan.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtUrl.Properties)).EndInit();
            this.TabPageWorkDay.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dateNavigator1)).EndInit();
            this.TabPageJituanshezhi.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEditStdType)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage TabPageJiTuan;
        private DevExpress.XtraTab.XtraTabPage TabPageWorkDay;
        private DevExpress.XtraTab.XtraTabPage TabPageJituanshezhi;
        private DevExpress.XtraScheduler.DateNavigator dateNavigator1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEditStdType;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraEditors.LabelControl lblUrl;
        private DevExpress.XtraEditors.TextEdit txtUrl;
        private DevExpress.XtraEditors.LabelControl lblCurDayStr;
        private DevExpress.XtraEditors.LabelControl lblDakaStatus;
        private DevExpress.XtraEditors.LabelControl lblDaKa;


    }
}

