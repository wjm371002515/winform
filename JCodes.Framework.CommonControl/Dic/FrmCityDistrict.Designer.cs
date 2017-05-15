namespace JCodes.Framework.CommonControl
{
    partial class FrmCityDistrict
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("所有记录", 0, 0);
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("自定义分组1", 1, 1);
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("自定义分组2", 1, 1);
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("自定义分组3", 1, 1);
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("个人分组", 1, 1, new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode3,
            treeNode4});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCityDistrict));
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("所有记录", 0, 0);
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("自定义分组1", 1, 1);
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("自定义分组2", 1, 1);
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("自定义分组3", 1, 1);
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("个人分组", 1, 1, new System.Windows.Forms.TreeNode[] {
            treeNode7,
            treeNode8,
            treeNode9});
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.treeProvince = new System.Windows.Forms.TreeView();
            this.menuProviceTree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuTree_ExpandAll = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTree_Clapase = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTree_Refresh = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.splitContainerControl2 = new DevExpress.XtraEditors.SplitContainerControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.treeCity = new System.Windows.Forms.TreeView();
            this.menuCityTree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuCity_ExpandAll = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCity_Clapse = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCity_Refresh = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuCity_AddNew = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCity_Edit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCity_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lblCityName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnBatchAdd = new DevExpress.XtraEditors.SimpleButton();
            this.btnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.label2 = new System.Windows.Forms.Label();
            this.winGridViewPager1 = new WinGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.menuProviceTree.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).BeginInit();
            this.splitContainerControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            this.menuCityTree.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.groupControl1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.splitContainerControl2);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1063, 625);
            this.splitContainerControl1.SplitterPosition = 220;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.AppearanceCaption.Options.UseTextOptions = true;
            this.groupControl1.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.groupControl1.Controls.Add(this.treeProvince);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(220, 625);
            this.groupControl1.TabIndex = 27;
            this.groupControl1.Text = "省份列表";
            // 
            // treeProvince
            // 
            this.treeProvince.ContextMenuStrip = this.menuProviceTree;
            this.treeProvince.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeProvince.FullRowSelect = true;
            this.treeProvince.HideSelection = false;
            this.treeProvince.ImageIndex = 0;
            this.treeProvince.ImageList = this.imageList1;
            this.treeProvince.Location = new System.Drawing.Point(2, 22);
            this.treeProvince.Name = "treeProvince";
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
            this.treeProvince.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode5});
            this.treeProvince.SelectedImageIndex = 0;
            this.treeProvince.Size = new System.Drawing.Size(216, 601);
            this.treeProvince.TabIndex = 0;
            this.treeProvince.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeProvince_AfterSelect);
            // 
            // menuProviceTree
            // 
            this.menuProviceTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuTree_ExpandAll,
            this.menuTree_Clapase,
            this.menuTree_Refresh});
            this.menuProviceTree.Name = "menuTree";
            this.menuProviceTree.Size = new System.Drawing.Size(141, 70);
            // 
            // menuTree_ExpandAll
            // 
            this.menuTree_ExpandAll.Image = ((System.Drawing.Image)(resources.GetObject("menuTree_ExpandAll.Image")));
            this.menuTree_ExpandAll.Name = "menuTree_ExpandAll";
            this.menuTree_ExpandAll.Size = new System.Drawing.Size(140, 22);
            this.menuTree_ExpandAll.Text = "全部展开(&E)";
            this.menuTree_ExpandAll.Click += new System.EventHandler(this.menuTree_ExpandAll_Click);
            // 
            // menuTree_Clapase
            // 
            this.menuTree_Clapase.Image = ((System.Drawing.Image)(resources.GetObject("menuTree_Clapase.Image")));
            this.menuTree_Clapase.Name = "menuTree_Clapase";
            this.menuTree_Clapase.Size = new System.Drawing.Size(140, 22);
            this.menuTree_Clapase.Text = "全部折叠(&C)";
            this.menuTree_Clapase.Click += new System.EventHandler(this.menuTree_Clapase_Click);
            // 
            // menuTree_Refresh
            // 
            this.menuTree_Refresh.Image = ((System.Drawing.Image)(resources.GetObject("menuTree_Refresh.Image")));
            this.menuTree_Refresh.Name = "menuTree_Refresh";
            this.menuTree_Refresh.Size = new System.Drawing.Size(140, 22);
            this.menuTree_Refresh.Text = "刷新列表(&R)";
            this.menuTree_Refresh.Click += new System.EventHandler(this.menuTree_Refresh_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "ima.ico");
            this.imageList1.Images.SetKeyName(1, "star (2).ico");
            this.imageList1.Images.SetKeyName(2, "accept.ico");
            this.imageList1.Images.SetKeyName(3, "re.ico");
            // 
            // splitContainerControl2
            // 
            this.splitContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl2.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl2.Name = "splitContainerControl2";
            this.splitContainerControl2.Panel1.Controls.Add(this.groupControl2);
            this.splitContainerControl2.Panel1.Text = "Panel1";
            this.splitContainerControl2.Panel2.Controls.Add(this.panelControl1);
            this.splitContainerControl2.Panel2.Controls.Add(this.label2);
            this.splitContainerControl2.Panel2.Controls.Add(this.winGridViewPager1);
            this.splitContainerControl2.Panel2.Text = "Panel2";
            this.splitContainerControl2.Size = new System.Drawing.Size(838, 625);
            this.splitContainerControl2.SplitterPosition = 258;
            this.splitContainerControl2.TabIndex = 0;
            this.splitContainerControl2.Text = "splitContainerControl2";
            // 
            // groupControl2
            // 
            this.groupControl2.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.groupControl2.AppearanceCaption.Options.UseFont = true;
            this.groupControl2.AppearanceCaption.Options.UseTextOptions = true;
            this.groupControl2.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.groupControl2.Controls.Add(this.treeCity);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(258, 625);
            this.groupControl2.TabIndex = 28;
            this.groupControl2.Text = "城市列表";
            // 
            // treeCity
            // 
            this.treeCity.ContextMenuStrip = this.menuCityTree;
            this.treeCity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeCity.FullRowSelect = true;
            this.treeCity.HideSelection = false;
            this.treeCity.ImageIndex = 0;
            this.treeCity.ImageList = this.imageList1;
            this.treeCity.Location = new System.Drawing.Point(2, 22);
            this.treeCity.Name = "treeCity";
            treeNode6.ImageIndex = 0;
            treeNode6.Name = "节点73";
            treeNode6.SelectedImageIndex = 0;
            treeNode6.Text = "所有记录";
            treeNode7.ImageIndex = 1;
            treeNode7.Name = "节点11";
            treeNode7.SelectedImageIndex = 1;
            treeNode7.Text = "自定义分组1";
            treeNode8.ImageIndex = 1;
            treeNode8.Name = "节点12";
            treeNode8.SelectedImageIndex = 1;
            treeNode8.Text = "自定义分组2";
            treeNode9.ImageIndex = 1;
            treeNode9.Name = "节点13";
            treeNode9.SelectedImageIndex = 1;
            treeNode9.Text = "自定义分组3";
            treeNode10.ImageIndex = 1;
            treeNode10.Name = "节点2";
            treeNode10.SelectedImageIndex = 1;
            treeNode10.Text = "个人分组";
            this.treeCity.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode6,
            treeNode10});
            this.treeCity.SelectedImageIndex = 0;
            this.treeCity.Size = new System.Drawing.Size(254, 601);
            this.treeCity.TabIndex = 0;
            this.treeCity.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeCity_AfterSelect);
            this.treeCity.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.treeCity_MouseDoubleClick);
            // 
            // menuCityTree
            // 
            this.menuCityTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuCity_ExpandAll,
            this.menuCity_Clapse,
            this.menuCity_Refresh,
            this.toolStripSeparator1,
            this.menuCity_AddNew,
            this.menuCity_Edit,
            this.menuCity_Delete});
            this.menuCityTree.Name = "menuCityTree";
            this.menuCityTree.Size = new System.Drawing.Size(141, 142);
            // 
            // menuCity_ExpandAll
            // 
            this.menuCity_ExpandAll.Image = ((System.Drawing.Image)(resources.GetObject("menuCity_ExpandAll.Image")));
            this.menuCity_ExpandAll.Name = "menuCity_ExpandAll";
            this.menuCity_ExpandAll.Size = new System.Drawing.Size(140, 22);
            this.menuCity_ExpandAll.Text = "全部展开(&E)";
            this.menuCity_ExpandAll.Click += new System.EventHandler(this.menuCity_ExpandAll_Click);
            // 
            // menuCity_Clapse
            // 
            this.menuCity_Clapse.Image = ((System.Drawing.Image)(resources.GetObject("menuCity_Clapse.Image")));
            this.menuCity_Clapse.Name = "menuCity_Clapse";
            this.menuCity_Clapse.Size = new System.Drawing.Size(140, 22);
            this.menuCity_Clapse.Text = "全部折叠(&C)";
            this.menuCity_Clapse.Click += new System.EventHandler(this.menuCity_Clapse_Click);
            // 
            // menuCity_Refresh
            // 
            this.menuCity_Refresh.Image = ((System.Drawing.Image)(resources.GetObject("menuCity_Refresh.Image")));
            this.menuCity_Refresh.Name = "menuCity_Refresh";
            this.menuCity_Refresh.Size = new System.Drawing.Size(140, 22);
            this.menuCity_Refresh.Text = "刷新列表(&R)";
            this.menuCity_Refresh.Click += new System.EventHandler(this.menuCity_Refresh_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(137, 6);
            // 
            // menuCity_AddNew
            // 
            this.menuCity_AddNew.Image = ((System.Drawing.Image)(resources.GetObject("menuCity_AddNew.Image")));
            this.menuCity_AddNew.Name = "menuCity_AddNew";
            this.menuCity_AddNew.Size = new System.Drawing.Size(140, 22);
            this.menuCity_AddNew.Text = "新建(&N)";
            this.menuCity_AddNew.Click += new System.EventHandler(this.menuCity_AddNew_Click);
            // 
            // menuCity_Edit
            // 
            this.menuCity_Edit.Image = ((System.Drawing.Image)(resources.GetObject("menuCity_Edit.Image")));
            this.menuCity_Edit.Name = "menuCity_Edit";
            this.menuCity_Edit.Size = new System.Drawing.Size(140, 22);
            this.menuCity_Edit.Text = "编辑(&M)";
            this.menuCity_Edit.Click += new System.EventHandler(this.menuCity_Edit_Click);
            // 
            // menuCity_Delete
            // 
            this.menuCity_Delete.Image = ((System.Drawing.Image)(resources.GetObject("menuCity_Delete.Image")));
            this.menuCity_Delete.Name = "menuCity_Delete";
            this.menuCity_Delete.Size = new System.Drawing.Size(140, 22);
            this.menuCity_Delete.Text = "删除(&D)";
            this.menuCity_Delete.Click += new System.EventHandler(this.menuCity_Delete_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl1.Controls.Add(this.lblCityName);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Controls.Add(this.btnDelete);
            this.panelControl1.Controls.Add(this.btnBatchAdd);
            this.panelControl1.Controls.Add(this.btnEdit);
            this.panelControl1.Location = new System.Drawing.Point(1, 12);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(571, 48);
            this.panelControl1.TabIndex = 2;
            // 
            // lblCityName
            // 
            this.lblCityName.AutoSize = true;
            this.lblCityName.Location = new System.Drawing.Point(114, 19);
            this.lblCityName.Name = "lblCityName";
            this.lblCityName.Size = new System.Drawing.Size(55, 14);
            this.lblCityName.TabIndex = 6;
            this.lblCityName.Text = "城市名称";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 14);
            this.label1.TabIndex = 7;
            this.label1.Text = "您选择的城市：";
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Location = new System.Drawing.Point(488, 10);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "删除";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnBatchAdd
            // 
            this.btnBatchAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBatchAdd.Location = new System.Drawing.Point(305, 10);
            this.btnBatchAdd.Name = "btnBatchAdd";
            this.btnBatchAdd.Size = new System.Drawing.Size(86, 23);
            this.btnBatchAdd.TabIndex = 2;
            this.btnBatchAdd.Text = "批量添加";
            this.btnBatchAdd.Click += new System.EventHandler(this.btnBatchAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Location = new System.Drawing.Point(407, 10);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 4;
            this.btnEdit.Text = "编辑";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(5, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 14);
            this.label2.TabIndex = 7;
            this.label2.Text = "城市行政区列表";
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
            this.winGridViewPager1.Location = new System.Drawing.Point(1, 95);
            this.winGridViewPager1.MinimumSize = new System.Drawing.Size(540, 0);
            this.winGridViewPager1.Name = "winGridViewPager1";
            this.winGridViewPager1.PrintTitle = "";
            this.winGridViewPager1.ShowCheckBox = false;
            this.winGridViewPager1.ShowExportButton = false;
            this.winGridViewPager1.Size = new System.Drawing.Size(571, 527);
            this.winGridViewPager1.TabIndex = 1;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // FrmCityDistrict
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1063, 625);
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "FrmCityDistrict";
            this.Text = "全国省份行政区划管理";
            this.Load += new System.EventHandler(this.FrmCityDistrict_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.menuProviceTree.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).EndInit();
            this.splitContainerControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.menuCityTree.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl2;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.TreeView treeProvince;
        private System.Windows.Forms.ContextMenuStrip menuProviceTree;
        private System.Windows.Forms.ToolStripMenuItem menuTree_ExpandAll;
        private System.Windows.Forms.ToolStripMenuItem menuTree_Clapase;
        private System.Windows.Forms.ToolStripMenuItem menuTree_Refresh;
        private System.Windows.Forms.ImageList imageList1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private System.Windows.Forms.TreeView treeCity;
        private WinGridView winGridViewPager1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.Label lblCityName;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraEditors.SimpleButton btnBatchAdd;
        private DevExpress.XtraEditors.SimpleButton btnEdit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ContextMenuStrip menuCityTree;
        private System.Windows.Forms.ToolStripMenuItem menuCity_ExpandAll;
        private System.Windows.Forms.ToolStripMenuItem menuCity_Clapse;
        private System.Windows.Forms.ToolStripMenuItem menuCity_Refresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menuCity_AddNew;
        private System.Windows.Forms.ToolStripMenuItem menuCity_Edit;
        private System.Windows.Forms.ToolStripMenuItem menuCity_Delete;
    }
}