namespace JCodes.Framework.AddIn._50Go
{
    partial class FrmCouponCategory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCouponCategory));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("所有记录", 0, 0);
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("自定义分组1", 1, 1);
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("自定义分组2", 1, 1);
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("自定义分组3", 1, 1);
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("个人分组", 1, 1, new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode3,
            treeNode4});
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.txtEnabled = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtFullName = new DevExpress.XtraEditors.TextEdit();
            this.txtMobilePhone = new DevExpress.XtraEditors.TextEdit();
            this.txtCategory = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtID = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.winGridViewPager1 = new JCodes.Framework.CommonControl.Pager.WinGridViewPager();
            this.btnExport = new DevExpress.XtraEditors.SimpleButton();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.menuTree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuTree_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTree_Refresh = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnGenerate = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtEnabled.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFullName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMobilePhone.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCategory.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.menuTree.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(74, 4);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.layoutControl1.Controls.Add(this.txtEnabled);
            this.layoutControl1.Controls.Add(this.txtFullName);
            this.layoutControl1.Controls.Add(this.txtMobilePhone);
            this.layoutControl1.Controls.Add(this.txtCategory);
            this.layoutControl1.Controls.Add(this.txtID);
            this.layoutControl1.Location = new System.Drawing.Point(1, 8);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(767, 70);
            this.layoutControl1.TabIndex = 11;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // txtEnabled
            // 
            this.txtEnabled.Location = new System.Drawing.Point(403, 37);
            this.txtEnabled.Name = "txtEnabled";
            this.txtEnabled.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.DropDown)});
            this.txtEnabled.Properties.Items.AddRange(new object[] {
            "否",
            "是"});
            this.txtEnabled.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.txtEnabled.Size = new System.Drawing.Size(115, 28);
            this.txtEnabled.StyleController = this.layoutControl1;
            this.txtEnabled.TabIndex = 12;
            // 
            // txtFullName
            // 
            this.txtFullName.Location = new System.Drawing.Point(123, 37);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(165, 28);
            this.txtFullName.StyleController = this.layoutControl1;
            this.txtFullName.TabIndex = 10;
            // 
            // txtMobilePhone
            // 
            this.txtMobilePhone.Location = new System.Drawing.Point(633, 12);
            this.txtMobilePhone.Name = "txtMobilePhone";
            this.txtMobilePhone.Size = new System.Drawing.Size(115, 28);
            this.txtMobilePhone.StyleController = this.layoutControl1;
            this.txtMobilePhone.TabIndex = 9;
            this.txtMobilePhone.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SearchControl_KeyUp);
            // 
            // txtCategory
            // 
            this.txtCategory.EditValue = "";
            this.txtCategory.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtCategory.Location = new System.Drawing.Point(403, 12);
            this.txtCategory.Name = "txtCategory";
            this.txtCategory.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.DropDown)});
            this.txtCategory.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.txtCategory.Size = new System.Drawing.Size(115, 28);
            this.txtCategory.StyleController = this.layoutControl1;
            this.txtCategory.TabIndex = 4;
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(123, 12);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(165, 28);
            this.txtID.StyleController = this.layoutControl1;
            this.txtID.TabIndex = 4;
            this.txtID.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SearchControl_KeyUp);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem6,
            this.layoutControlItem1,
            this.layoutControlItem11,
            this.layoutControlItem9});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(767, 70);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.txtCategory;
            this.layoutControlItem2.CustomizationFormText = "真实名称";
            this.layoutControlItem2.Location = new System.Drawing.Point(280, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(230, 25);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(96, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(230, 25);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "分类名称";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(108, 22);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.txtMobilePhone;
            this.layoutControlItem6.CustomizationFormText = "日志信息";
            this.layoutControlItem6.Location = new System.Drawing.Point(510, 0);
            this.layoutControlItem6.MaxSize = new System.Drawing.Size(230, 25);
            this.layoutControlItem6.MinSize = new System.Drawing.Size(105, 24);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(237, 25);
            this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem6.Text = "联系电话";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(108, 22);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.txtID;
            this.layoutControlItem1.CustomizationFormText = "登录名称";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(280, 25);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(105, 24);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(280, 25);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "优惠券序列号";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(108, 22);
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.txtEnabled;
            this.layoutControlItem11.CustomizationFormText = "系统类型编号";
            this.layoutControlItem11.Location = new System.Drawing.Point(280, 25);
            this.layoutControlItem11.MaxSize = new System.Drawing.Size(230, 25);
            this.layoutControlItem11.MinSize = new System.Drawing.Size(129, 24);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(467, 25);
            this.layoutControlItem11.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem11.Text = "是否使用";
            this.layoutControlItem11.TextSize = new System.Drawing.Size(108, 22);
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.txtFullName;
            this.layoutControlItem9.CustomizationFormText = "联系人";
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 25);
            this.layoutControlItem9.MaxSize = new System.Drawing.Size(280, 25);
            this.layoutControlItem9.MinSize = new System.Drawing.Size(105, 24);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(280, 25);
            this.layoutControlItem9.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem9.Text = "联系人";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(108, 22);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(605, 84);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 22);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "查询";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // winGridViewPager1
            // 
            this.winGridViewPager1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.winGridViewPager1.AppendedMenu = null;
            this.winGridViewPager1.ColumnNameAlias = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("winGridViewPager1.ColumnNameAlias")));
            this.winGridViewPager1.DataSource = null;
            this.winGridViewPager1.DisplayColumns = "";
            this.winGridViewPager1.FixedColumns = null;
            this.winGridViewPager1.Location = new System.Drawing.Point(1, 112);
            this.winGridViewPager1.MinimumSize = new System.Drawing.Size(540, 0);
            this.winGridViewPager1.Name = "winGridViewPager1";
            this.winGridViewPager1.PrintTitle = "";
            this.winGridViewPager1.ShowAddMenu = true;
            this.winGridViewPager1.ShowCheckBox = false;
            this.winGridViewPager1.ShowDeleteMenu = true;
            this.winGridViewPager1.ShowEditMenu = true;
            this.winGridViewPager1.ShowExportButton = true;
            this.winGridViewPager1.Size = new System.Drawing.Size(770, 565);
            this.winGridViewPager1.TabIndex = 10;
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Location = new System.Drawing.Point(686, 84);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 22);
            this.btnExport.TabIndex = 5;
            this.btnExport.Text = "导出";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.groupControl1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.btnExport);
            this.splitContainerControl1.Panel2.Controls.Add(this.winGridViewPager1);
            this.splitContainerControl1.Panel2.Controls.Add(this.layoutControl1);
            this.splitContainerControl1.Panel2.Controls.Add(this.btnGenerate);
            this.splitContainerControl1.Panel2.Controls.Add(this.btnSearch);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1008, 680);
            this.splitContainerControl1.SplitterPosition = 229;
            this.splitContainerControl1.TabIndex = 12;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.AppearanceCaption.Options.UseTextOptions = true;
            this.groupControl1.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.groupControl1.Controls.Add(this.treeView1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(229, 680);
            this.groupControl1.TabIndex = 28;
            this.groupControl1.Text = "优惠券分类";
            // 
            // treeView1
            // 
            this.treeView1.ContextMenuStrip = this.menuTree;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.FullRowSelect = true;
            this.treeView1.HideSelection = false;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.ItemHeight = 27;
            this.treeView1.Location = new System.Drawing.Point(2, 30);
            this.treeView1.Name = "treeView1";
            treeNode1.ImageIndex = 0;
            treeNode1.Name = "节点73";
            treeNode1.SelectedImageIndex = 0;
            treeNode1.Text = "所有记录";
            treeNode2.ImageIndex = 1;
            treeNode2.Name = "节点11";
            treeNode2.SelectedImageIndex = 1;
            treeNode2.Text = "自定义分组1";
            treeNode3.ImageIndex = 1;
            treeNode3.Name = "节点12";
            treeNode3.SelectedImageIndex = 1;
            treeNode3.Text = "自定义分组2";
            treeNode4.ImageIndex = 1;
            treeNode4.Name = "节点13";
            treeNode4.SelectedImageIndex = 1;
            treeNode4.Text = "自定义分组3";
            treeNode5.ImageIndex = 1;
            treeNode5.Name = "节点2";
            treeNode5.SelectedImageIndex = 1;
            treeNode5.Text = "个人分组";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode5});
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(225, 648);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseDoubleClick);
            // 
            // menuTree
            // 
            this.menuTree.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuTree_Add,
            this.menuTree_Refresh});
            this.menuTree.Name = "menuTree";
            this.menuTree.Size = new System.Drawing.Size(191, 64);
            // 
            // menuTree_Add
            // 
            this.menuTree_Add.Image = ((System.Drawing.Image)(resources.GetObject("menuTree_Add.Image")));
            this.menuTree_Add.Name = "menuTree_Add";
            this.menuTree_Add.Size = new System.Drawing.Size(190, 30);
            this.menuTree_Add.Text = "新增分类(&A)";
            this.menuTree_Add.Click += new System.EventHandler(this.menuTree_Add_Click);
            // 
            // menuTree_Refresh
            // 
            this.menuTree_Refresh.Image = ((System.Drawing.Image)(resources.GetObject("menuTree_Refresh.Image")));
            this.menuTree_Refresh.Name = "menuTree_Refresh";
            this.menuTree_Refresh.Size = new System.Drawing.Size(190, 30);
            this.menuTree_Refresh.Text = "刷新列表(&R)";
            this.menuTree_Refresh.Click += new System.EventHandler(this.menuTree_Refresh_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Tip.png");
            this.imageList1.Images.SetKeyName(1, "Organ.ico");
            this.imageList1.Images.SetKeyName(2, "Search2.ICO");
            this.imageList1.Images.SetKeyName(3, "tableInfo.ico");
            this.imageList1.Images.SetKeyName(4, "accept.ico");
            this.imageList1.Images.SetKeyName(5, "ima.ico");
            this.imageList1.Images.SetKeyName(6, "sm.ico");
            this.imageList1.Images.SetKeyName(7, "tra.ico");
            // 
            // btnGenerate
            // 
            this.btnGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerate.Location = new System.Drawing.Point(487, 84);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(112, 22);
            this.btnGenerate.TabIndex = 5;
            this.btnGenerate.Text = "生成优惠券";
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // FrmCouponCategory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 680);
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "FrmCouponCategory";
            this.Text = "生成优惠券";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtEnabled.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFullName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMobilePhone.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCategory.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.menuTree.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private JCodes.Framework.CommonControl.Pager.WinGridViewPager winGridViewPager1;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.ComboBoxEdit txtCategory;
        private DevExpress.XtraEditors.TextEdit txtID;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.TextEdit txtMobilePhone;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraEditors.TextEdit txtFullName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraEditors.ComboBoxEdit txtEnabled;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraEditors.SimpleButton btnExport;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ContextMenuStrip menuTree;
        private System.Windows.Forms.ToolStripMenuItem menuTree_Add;
        private System.Windows.Forms.ToolStripMenuItem menuTree_Refresh;
        private System.Windows.Forms.ImageList imageList1;
        private DevExpress.XtraEditors.SimpleButton btnGenerate;
    }
}