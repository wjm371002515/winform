namespace JCodes.Framework.AddIn
{
    partial class MenuMgr
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
            this.plbottom = new System.Windows.Forms.Panel();
            this.btnExportxls = new DevExpress.XtraEditors.SimpleButton();
            this.btnexportsql = new DevExpress.XtraEditors.SimpleButton();
            this.btnImport = new DevExpress.XtraEditors.SimpleButton();
            this.pltop = new System.Windows.Forms.Panel();
            this.tlstMenus = new DevExpress.XtraTreeList.TreeList();
            this.tlcolid = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.tlcoltitle = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.tlcolpid = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.tlcolsort = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.tlcolurl = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.tlcolhide = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.tlcoltip = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.tlcolis_show = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.tlcolmenu_group = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.tlcolis_dev = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.tlcolstatus = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.plbottom.SuspendLayout();
            this.pltop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tlstMenus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // plbottom
            // 
            this.plbottom.Controls.Add(this.btnExportxls);
            this.plbottom.Controls.Add(this.btnexportsql);
            this.plbottom.Controls.Add(this.btnImport);
            this.plbottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.plbottom.Location = new System.Drawing.Point(0, 426);
            this.plbottom.Name = "plbottom";
            this.plbottom.Size = new System.Drawing.Size(990, 49);
            this.plbottom.TabIndex = 0;
            // 
            // btnExportxls
            // 
            this.btnExportxls.Location = new System.Drawing.Point(223, 14);
            this.btnExportxls.Name = "btnExportxls";
            this.btnExportxls.Size = new System.Drawing.Size(75, 23);
            this.btnExportxls.TabIndex = 2;
            this.btnExportxls.Text = "导出xls";
            this.btnExportxls.Click += new System.EventHandler(this.btnExportxls_Click);
            // 
            // btnexportsql
            // 
            this.btnexportsql.Location = new System.Drawing.Point(122, 14);
            this.btnexportsql.Name = "btnexportsql";
            this.btnexportsql.Size = new System.Drawing.Size(75, 23);
            this.btnexportsql.TabIndex = 1;
            this.btnexportsql.Text = "导出sql";
            this.btnexportsql.Click += new System.EventHandler(this.btnexportsql_Click);
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(23, 14);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 23);
            this.btnImport.TabIndex = 0;
            this.btnImport.Text = "导入数据";
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // pltop
            // 
            this.pltop.Controls.Add(this.tlstMenus);
            this.pltop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pltop.Location = new System.Drawing.Point(0, 0);
            this.pltop.Name = "pltop";
            this.pltop.Size = new System.Drawing.Size(990, 426);
            this.pltop.TabIndex = 1;
            // 
            // tlstMenus
            // 
            this.tlstMenus.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.tlcolid,
            this.tlcoltitle,
            this.tlcolpid,
            this.tlcolsort,
            this.tlcolurl,
            this.tlcolhide,
            this.tlcoltip,
            this.tlcolis_show,
            this.tlcolmenu_group,
            this.tlcolis_dev,
            this.tlcolstatus});
            this.tlstMenus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlstMenus.Location = new System.Drawing.Point(0, 0);
            this.tlstMenus.Name = "tlstMenus";
            this.tlstMenus.OptionsPrint.UsePrintStyles = true;
            this.tlstMenus.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1});
            this.tlstMenus.Size = new System.Drawing.Size(990, 426);
            this.tlstMenus.TabIndex = 0;
            // 
            // tlcolid
            // 
            this.tlcolid.Caption = "id序号";
            this.tlcolid.FieldName = "id";
            this.tlcolid.Name = "tlcolid";
            this.tlcolid.Visible = true;
            this.tlcolid.VisibleIndex = 0;
            // 
            // tlcoltitle
            // 
            this.tlcoltitle.Caption = "标题";
            this.tlcoltitle.FieldName = "title";
            this.tlcoltitle.Name = "tlcoltitle";
            this.tlcoltitle.Visible = true;
            this.tlcoltitle.VisibleIndex = 1;
            // 
            // tlcolpid
            // 
            this.tlcolpid.Caption = "上级分类ID";
            this.tlcolpid.FieldName = "pid";
            this.tlcolpid.Name = "tlcolpid";
            this.tlcolpid.Visible = true;
            this.tlcolpid.VisibleIndex = 2;
            // 
            // tlcolsort
            // 
            this.tlcolsort.Caption = "排序";
            this.tlcolsort.FieldName = "排序";
            this.tlcolsort.Name = "tlcolsort";
            this.tlcolsort.Visible = true;
            this.tlcolsort.VisibleIndex = 3;
            // 
            // tlcolurl
            // 
            this.tlcolurl.Caption = "频道连接";
            this.tlcolurl.FieldName = "url";
            this.tlcolurl.Name = "tlcolurl";
            this.tlcolurl.Visible = true;
            this.tlcolurl.VisibleIndex = 4;
            // 
            // tlcolhide
            // 
            this.tlcolhide.Caption = "是否隐藏";
            this.tlcolhide.FieldName = "hide";
            this.tlcolhide.Name = "tlcolhide";
            this.tlcolhide.Visible = true;
            this.tlcolhide.VisibleIndex = 5;
            // 
            // tlcoltip
            // 
            this.tlcoltip.Caption = "提示";
            this.tlcoltip.FieldName = "tip";
            this.tlcoltip.Name = "tlcoltip";
            this.tlcoltip.Visible = true;
            this.tlcoltip.VisibleIndex = 6;
            // 
            // tlcolis_show
            // 
            this.tlcolis_show.Caption = "是否显示";
            this.tlcolis_show.FieldName = "is_show";
            this.tlcolis_show.Name = "tlcolis_show";
            this.tlcolis_show.Visible = true;
            this.tlcolis_show.VisibleIndex = 7;
            // 
            // tlcolmenu_group
            // 
            this.tlcolmenu_group.Caption = "菜单分组";
            this.tlcolmenu_group.FieldName = "menu_group";
            this.tlcolmenu_group.Name = "tlcolmenu_group";
            this.tlcolmenu_group.Visible = true;
            this.tlcolmenu_group.VisibleIndex = 8;
            // 
            // tlcolis_dev
            // 
            this.tlcolis_dev.Caption = "是否仅开发者模式可见";
            this.tlcolis_dev.FieldName = "is_dev";
            this.tlcolis_dev.Name = "tlcolis_dev";
            this.tlcolis_dev.Visible = true;
            this.tlcolis_dev.VisibleIndex = 9;
            // 
            // tlcolstatus
            // 
            this.tlcolstatus.Caption = "状态";
            this.tlcolstatus.FieldName = "status";
            this.tlcolstatus.Name = "tlcolstatus";
            this.tlcolstatus.Visible = true;
            this.tlcolstatus.VisibleIndex = 10;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // MenuMgr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(990, 475);
            this.Controls.Add(this.pltop);
            this.Controls.Add(this.plbottom);
            this.Name = "MenuMgr";
            this.Text = "菜单管理界面";
            this.plbottom.ResumeLayout(false);
            this.pltop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tlstMenus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel plbottom;
        private System.Windows.Forms.Panel pltop;
        private DevExpress.XtraEditors.SimpleButton btnImport;
        private DevExpress.XtraEditors.SimpleButton btnexportsql;
        private DevExpress.XtraEditors.SimpleButton btnExportxls;
        private DevExpress.XtraTreeList.TreeList tlstMenus;
        private DevExpress.XtraTreeList.Columns.TreeListColumn tlcolid;
        private DevExpress.XtraTreeList.Columns.TreeListColumn tlcoltitle;
        private DevExpress.XtraTreeList.Columns.TreeListColumn tlcolpid;
        private DevExpress.XtraTreeList.Columns.TreeListColumn tlcolsort;
        private DevExpress.XtraTreeList.Columns.TreeListColumn tlcolurl;
        private DevExpress.XtraTreeList.Columns.TreeListColumn tlcolhide;
        private DevExpress.XtraTreeList.Columns.TreeListColumn tlcoltip;
        private DevExpress.XtraTreeList.Columns.TreeListColumn tlcolis_show;
        private DevExpress.XtraTreeList.Columns.TreeListColumn tlcolmenu_group;
        private DevExpress.XtraTreeList.Columns.TreeListColumn tlcolis_dev;
        private DevExpress.XtraTreeList.Columns.TreeListColumn tlcolstatus;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
    }
}