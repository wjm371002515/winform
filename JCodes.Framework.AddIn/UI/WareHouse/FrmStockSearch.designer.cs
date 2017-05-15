using JCodes.Framework.CommonControl;
namespace JCodes.Framework.AddIn.UI.WareHouse
{
    partial class FrmStockSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmStockSearch));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("所有记录", 0, 0);
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("自定义分组1", 1, 1);
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("自定义分组2", 1, 1);
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("自定义分组3", 1, 1);
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("个人分组", 1, 1, new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode3,
            treeNode4});
            this.imageList1 = new System.Windows.Forms.ImageList();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip();
            this.menu_SetAlarm = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_ModifyStock = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_DeleteZero = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip();
            this.menu_ClapseAll = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_ExpandAll = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Refresh = new System.Windows.Forms.ToolStripMenuItem();
            this.spinEdit1 = new DevExpress.XtraEditors.SpinEdit();
            this.simpleLabelItem1 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.txtStockQuantity2 = new DevExpress.XtraEditors.TextEdit();
            this.txtUsagePos = new DevExpress.XtraEditors.TextEdit();
            this.txtStockQuantity1 = new DevExpress.XtraEditors.TextEdit();
            this.txtMapNo = new DevExpress.XtraEditors.TextEdit();
            this.txtItemNo = new DevExpress.XtraEditors.TextEdit();
            this.txtItemType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtManufacture = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtPrice2 = new DevExpress.XtraEditors.TextEdit();
            this.cmbSpecNumber = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtPrice1 = new DevExpress.XtraEditors.TextEdit();
            this.txtSource = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.txtUnit = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnReset = new DevExpress.XtraEditors.SimpleButton();
            this.txtBigType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.txtMaterial = new DevExpress.XtraEditors.TextEdit();
            this.txtStoragePos = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtSearchWareHouse = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem16 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem17 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem18 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem19 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem20 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem22 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem23 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem21 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem26 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleSeparator1 = new DevExpress.XtraLayout.SimpleSeparator();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.winGridView1 = new WinGridView();
            this.lvwDetail = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.treeGoods = new System.Windows.Forms.TreeView();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.menuTree = new System.Windows.Forms.ContextMenuStrip();
            this.menuTree_ExpandAll = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTree_Clapase = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTree_Refresh = new System.Windows.Forms.ToolStripMenuItem();
            this.imageListTree = new System.Windows.Forms.ImageList();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.splitContainerControl2 = new DevExpress.XtraEditors.SplitContainerControl();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtUsagePos.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMapNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtItemNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtItemType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtManufacture.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSpecNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSource.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUnit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBigType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaterial.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStoragePos.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearchWareHouse.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem17)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem18)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem19)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem20)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem22)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem23)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem21)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem26)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.menuTree.SuspendLayout();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).BeginInit();
            this.splitContainerControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "16folder.gif");
            this.imageList1.Images.SetKeyName(1, "16Arrow.gif");
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_SetAlarm,
            this.menu_ModifyStock,
            this.menu_DeleteZero});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(191, 70);
            // 
            // menu_SetAlarm
            // 
            this.menu_SetAlarm.Name = "menu_SetAlarm";
            this.menu_SetAlarm.Size = new System.Drawing.Size(190, 22);
            this.menu_SetAlarm.Text = "设置库存警告数量(&Q)";
            this.menu_SetAlarm.Click += new System.EventHandler(this.menu_SetAlarm_Click);
            // 
            // menu_ModifyStock
            // 
            this.menu_ModifyStock.Name = "menu_ModifyStock";
            this.menu_ModifyStock.Size = new System.Drawing.Size(190, 22);
            this.menu_ModifyStock.Text = "调整库存数量(&M)";
            this.menu_ModifyStock.Click += new System.EventHandler(this.menu_ModifyStock_Click);
            // 
            // menu_DeleteZero
            // 
            this.menu_DeleteZero.Name = "menu_DeleteZero";
            this.menu_DeleteZero.Size = new System.Drawing.Size(190, 22);
            this.menu_DeleteZero.Text = "删除冗余库存信息(&D)";
            this.menu_DeleteZero.ToolTipText = "该操作删除库存为0的多余库存记录,谨慎操作!";
            this.menu_DeleteZero.Click += new System.EventHandler(this.menu_DeleteZero_Click);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_ClapseAll,
            this.menu_ExpandAll,
            this.menu_Refresh});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(141, 70);
            // 
            // menu_ClapseAll
            // 
            this.menu_ClapseAll.Name = "menu_ClapseAll";
            this.menu_ClapseAll.Size = new System.Drawing.Size(140, 22);
            this.menu_ClapseAll.Text = "全部折叠(&C)";
            this.menu_ClapseAll.Click += new System.EventHandler(this.menu_ClapseAll_Click);
            // 
            // menu_ExpandAll
            // 
            this.menu_ExpandAll.Name = "menu_ExpandAll";
            this.menu_ExpandAll.Size = new System.Drawing.Size(140, 22);
            this.menu_ExpandAll.Text = "全部展开(&E)";
            this.menu_ExpandAll.Click += new System.EventHandler(this.menu_ExpandAll_Click);
            // 
            // menu_Refresh
            // 
            this.menu_Refresh.Name = "menu_Refresh";
            this.menu_Refresh.Size = new System.Drawing.Size(140, 22);
            this.menu_Refresh.Text = "刷新列表(&R)";
            this.menu_Refresh.Click += new System.EventHandler(this.menu_Refresh_Click);
            // 
            // spinEdit1
            // 
            this.spinEdit1.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinEdit1.Location = new System.Drawing.Point(0, 0);
            this.spinEdit1.Name = "spinEdit1";
            this.spinEdit1.Size = new System.Drawing.Size(100, 20);
            this.spinEdit1.TabIndex = 0;
            // 
            // simpleLabelItem1
            // 
            this.simpleLabelItem1.AllowHotTrack = false;
            this.simpleLabelItem1.CustomizationFormText = "LabelsimpleLabelItem1";
            this.simpleLabelItem1.Location = new System.Drawing.Point(510, 50);
            this.simpleLabelItem1.Name = "simpleLabelItem1";
            this.simpleLabelItem1.Size = new System.Drawing.Size(126, 25);
            this.simpleLabelItem1.Text = "LabelsimpleLabelItem1";
            this.simpleLabelItem1.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.layoutControl1.Controls.Add(this.txtStockQuantity2);
            this.layoutControl1.Controls.Add(this.txtUsagePos);
            this.layoutControl1.Controls.Add(this.txtStockQuantity1);
            this.layoutControl1.Controls.Add(this.txtMapNo);
            this.layoutControl1.Controls.Add(this.txtItemNo);
            this.layoutControl1.Controls.Add(this.txtItemType);
            this.layoutControl1.Controls.Add(this.txtManufacture);
            this.layoutControl1.Controls.Add(this.txtPrice2);
            this.layoutControl1.Controls.Add(this.cmbSpecNumber);
            this.layoutControl1.Controls.Add(this.txtPrice1);
            this.layoutControl1.Controls.Add(this.txtSource);
            this.layoutControl1.Controls.Add(this.btnSearch);
            this.layoutControl1.Controls.Add(this.txtUnit);
            this.layoutControl1.Controls.Add(this.btnReset);
            this.layoutControl1.Controls.Add(this.txtBigType);
            this.layoutControl1.Controls.Add(this.txtName);
            this.layoutControl1.Controls.Add(this.txtMaterial);
            this.layoutControl1.Controls.Add(this.txtStoragePos);
            this.layoutControl1.Controls.Add(this.txtSearchWareHouse);
            this.layoutControl1.Location = new System.Drawing.Point(9, 10);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(525, 406, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(839, 124);
            this.layoutControl1.TabIndex = 14;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // txtStockQuantity2
            // 
            this.txtStockQuantity2.Properties.Mask.EditMask = "[0-9]*";
            this.txtStockQuantity2.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtStockQuantity2.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtStockQuantity2.Location = new System.Drawing.Point(597, 61);
            this.txtStockQuantity2.Name = "txtStockQuantity2";
            this.txtStockQuantity2.Size = new System.Drawing.Size(55, 20);
            this.txtStockQuantity2.TabIndex = 15;
            this.txtStockQuantity2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGoodsType_KeyDown);
            // 
            // txtUsagePos
            // 
            this.txtUsagePos.Location = new System.Drawing.Point(215, 61);
            this.txtUsagePos.Name = "txtUsagePos";
            this.txtUsagePos.Size = new System.Drawing.Size(101, 20);
            this.txtUsagePos.StyleController = this.layoutControl1;
            this.txtUsagePos.TabIndex = 16;
            this.txtUsagePos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGoodsType_KeyDown);
            // 
            // txtStockQuantity1
            // 
            this.txtStockQuantity1.Properties.Mask.EditMask = "[0-9]*";
            this.txtStockQuantity1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtStockQuantity1.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtStockQuantity1.Location = new System.Drawing.Point(537, 61);
            this.txtStockQuantity1.Name = "txtStockQuantity1";
            this.txtStockQuantity1.Size = new System.Drawing.Size(42, 20);
            this.txtStockQuantity1.TabIndex = 14;
            this.txtStockQuantity1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGoodsType_KeyDown);
            // 
            // txtMapNo
            // 
            this.txtMapNo.Location = new System.Drawing.Point(372, 37);
            this.txtMapNo.Name = "txtMapNo";
            this.txtMapNo.Size = new System.Drawing.Size(109, 20);
            this.txtMapNo.StyleController = this.layoutControl1;
            this.txtMapNo.TabIndex = 10;
            this.txtMapNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGoodsType_KeyDown);
            // 
            // txtItemNo
            // 
            this.txtItemNo.Location = new System.Drawing.Point(63, 37);
            this.txtItemNo.Name = "txtItemNo";
            this.txtItemNo.Size = new System.Drawing.Size(97, 20);
            this.txtItemNo.StyleController = this.layoutControl1;
            this.txtItemNo.TabIndex = 9;
            this.txtItemNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGoodsType_KeyDown);
            // 
            // txtItemType
            // 
            this.txtItemType.Location = new System.Drawing.Point(371, 12);
            this.txtItemType.Name = "txtItemType";
            this.txtItemType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtItemType.Size = new System.Drawing.Size(110, 20);
            this.txtItemType.StyleController = this.layoutControl1;
            this.txtItemType.TabIndex = 6;
            this.txtItemType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGoodsType_KeyDown);
            // 
            // txtManufacture
            // 
            this.txtManufacture.Location = new System.Drawing.Point(536, 12);
            this.txtManufacture.Name = "txtManufacture";
            this.txtManufacture.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtManufacture.Size = new System.Drawing.Size(116, 20);
            this.txtManufacture.StyleController = this.layoutControl1;
            this.txtManufacture.TabIndex = 7;
            this.txtManufacture.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGoodsType_KeyDown);
            // 
            // txtPrice2
            // 
            this.txtPrice2.Properties.Mask.EditMask = "[0-9]*";
            this.txtPrice2.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtPrice2.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtPrice2.Location = new System.Drawing.Point(435, 61);
            this.txtPrice2.Name = "txtPrice2";
            this.txtPrice2.Size = new System.Drawing.Size(47, 20);
            this.txtPrice2.TabIndex = 13;
            this.txtPrice2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGoodsType_KeyDown);
            // 
            // cmbSpecNumber
            // 
            this.cmbSpecNumber.Location = new System.Drawing.Point(536, 37);
            this.cmbSpecNumber.Name = "cmbSpecNumber";
            this.cmbSpecNumber.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbSpecNumber.Size = new System.Drawing.Size(116, 20);
            this.cmbSpecNumber.StyleController = this.layoutControl1;
            this.cmbSpecNumber.TabIndex = 13;
            this.cmbSpecNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGoodsType_KeyDown);
            // 
            // txtPrice1
            // 
            this.txtPrice1.Properties.Mask.EditMask = "[0-9]*";
            this.txtPrice1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtPrice1.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtPrice1.Location = new System.Drawing.Point(371, 61);
            this.txtPrice1.Name = "txtPrice1";
            this.txtPrice1.Size = new System.Drawing.Size(44, 20);
            this.txtPrice1.TabIndex = 12;
            this.txtPrice1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGoodsType_KeyDown);
            // 
            // txtSource
            // 
            this.txtSource.Location = new System.Drawing.Point(707, 12);
            this.txtSource.Name = "txtSource";
            this.txtSource.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtSource.Size = new System.Drawing.Size(120, 20);
            this.txtSource.StyleController = this.layoutControl1;
            this.txtSource.TabIndex = 8;
            this.txtSource.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGoodsType_KeyDown);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(705, 85);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(59, 22);
            this.btnSearch.StyleController = this.layoutControl1;
            this.btnSearch.TabIndex = 14;
            this.btnSearch.Text = "查询";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtUnit
            // 
            this.txtUnit.Location = new System.Drawing.Point(63, 61);
            this.txtUnit.Name = "txtUnit";
            this.txtUnit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtUnit.Size = new System.Drawing.Size(97, 20);
            this.txtUnit.StyleController = this.layoutControl1;
            this.txtUnit.TabIndex = 15;
            this.txtUnit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGoodsType_KeyDown);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(768, 85);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(59, 22);
            this.btnReset.StyleController = this.layoutControl1;
            this.btnReset.TabIndex = 15;
            this.btnReset.Text = "清空条件";
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // txtBigType
            // 
            this.txtBigType.Location = new System.Drawing.Point(216, 12);
            this.txtBigType.Name = "txtBigType";
            this.txtBigType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtBigType.Size = new System.Drawing.Size(100, 20);
            this.txtBigType.StyleController = this.layoutControl1;
            this.txtBigType.TabIndex = 5;
            this.txtBigType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGoodsType_KeyDown);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(215, 37);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(102, 20);
            this.txtName.StyleController = this.layoutControl1;
            this.txtName.TabIndex = 11;
            this.txtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGoodsType_KeyDown);
            // 
            // txtMaterial
            // 
            this.txtMaterial.Location = new System.Drawing.Point(707, 37);
            this.txtMaterial.Name = "txtMaterial";
            this.txtMaterial.Size = new System.Drawing.Size(120, 20);
            this.txtMaterial.StyleController = this.layoutControl1;
            this.txtMaterial.TabIndex = 12;
            this.txtMaterial.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGoodsType_KeyDown);
            // 
            // txtStoragePos
            // 
            this.txtStoragePos.Location = new System.Drawing.Point(707, 61);
            this.txtStoragePos.Name = "txtStoragePos";
            this.txtStoragePos.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtStoragePos.Size = new System.Drawing.Size(120, 20);
            this.txtStoragePos.StyleController = this.layoutControl1;
            this.txtStoragePos.TabIndex = 19;
            this.txtStoragePos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGoodsType_KeyDown);
            // 
            // txtSearchWareHouse
            // 
            this.txtSearchWareHouse.AllowDrop = true;
            this.txtSearchWareHouse.Location = new System.Drawing.Point(63, 12);
            this.txtSearchWareHouse.Name = "txtSearchWareHouse";
            this.txtSearchWareHouse.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.DropDown)});
            this.txtSearchWareHouse.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.txtSearchWareHouse.Size = new System.Drawing.Size(98, 20);
            this.txtSearchWareHouse.StyleController = this.layoutControl1;
            this.txtSearchWareHouse.TabIndex = 4;
            this.txtSearchWareHouse.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGoodsType_KeyDown);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem16,
            this.layoutControlItem17,
            this.layoutControlItem18,
            this.layoutControlItem19,
            this.layoutControlItem20,
            this.layoutControlItem22,
            this.layoutControlItem23,
            this.emptySpaceItem1,
            this.layoutControlItem21,
            this.layoutControlItem26,
            this.layoutControlItem6,
            this.layoutControlItem8,
            this.simpleSeparator1,
            this.layoutControlItem7,
            this.layoutControlItem9,
            this.layoutControlItem10});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(839, 124);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.txtSearchWareHouse;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(175, 25);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(50, 25);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(153, 25);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "选择仓库";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.txtBigType;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(153, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(175, 25);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(50, 25);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(155, 25);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "备件属类";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.txtItemType;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(308, 0);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(175, 25);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(50, 25);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(165, 25);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.Text = "备件类别";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.txtManufacture;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(473, 0);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(175, 25);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(50, 25);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(171, 25);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.Text = "供货商";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.txtSource;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(644, 0);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(175, 25);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(50, 25);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(175, 25);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.Text = "来源";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem16
            // 
            this.layoutControlItem16.Control = this.txtItemNo;
            this.layoutControlItem16.CustomizationFormText = "layoutControlItem16";
            this.layoutControlItem16.Location = new System.Drawing.Point(0, 25);
            this.layoutControlItem16.MaxSize = new System.Drawing.Size(175, 24);
            this.layoutControlItem16.MinSize = new System.Drawing.Size(105, 24);
            this.layoutControlItem16.Name = "layoutControlItem16";
            this.layoutControlItem16.Size = new System.Drawing.Size(152, 24);
            this.layoutControlItem16.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem16.Text = "备件编码";
            this.layoutControlItem16.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem17
            // 
            this.layoutControlItem17.Control = this.txtMapNo;
            this.layoutControlItem17.CustomizationFormText = "layoutControlItem17";
            this.layoutControlItem17.Location = new System.Drawing.Point(309, 25);
            this.layoutControlItem17.MaxSize = new System.Drawing.Size(175, 24);
            this.layoutControlItem17.MinSize = new System.Drawing.Size(105, 24);
            this.layoutControlItem17.Name = "layoutControlItem17";
            this.layoutControlItem17.Size = new System.Drawing.Size(164, 24);
            this.layoutControlItem17.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem17.Text = "图号";
            this.layoutControlItem17.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem18
            // 
            this.layoutControlItem18.Control = this.txtName;
            this.layoutControlItem18.CustomizationFormText = "layoutControlItem18";
            this.layoutControlItem18.Location = new System.Drawing.Point(152, 25);
            this.layoutControlItem18.MaxSize = new System.Drawing.Size(175, 24);
            this.layoutControlItem18.MinSize = new System.Drawing.Size(105, 24);
            this.layoutControlItem18.Name = "layoutControlItem18";
            this.layoutControlItem18.Size = new System.Drawing.Size(157, 24);
            this.layoutControlItem18.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem18.Text = "备件名称";
            this.layoutControlItem18.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem19
            // 
            this.layoutControlItem19.Control = this.txtMaterial;
            this.layoutControlItem19.CustomizationFormText = "库位";
            this.layoutControlItem19.Location = new System.Drawing.Point(644, 25);
            this.layoutControlItem19.MaxSize = new System.Drawing.Size(175, 24);
            this.layoutControlItem19.MinSize = new System.Drawing.Size(105, 24);
            this.layoutControlItem19.Name = "layoutControlItem19";
            this.layoutControlItem19.Size = new System.Drawing.Size(175, 24);
            this.layoutControlItem19.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem19.Text = "材质";
            this.layoutControlItem19.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem20
            // 
            this.layoutControlItem20.Control = this.cmbSpecNumber;
            this.layoutControlItem20.CustomizationFormText = "来源";
            this.layoutControlItem20.Location = new System.Drawing.Point(473, 25);
            this.layoutControlItem20.MaxSize = new System.Drawing.Size(175, 24);
            this.layoutControlItem20.MinSize = new System.Drawing.Size(105, 24);
            this.layoutControlItem20.Name = "layoutControlItem20";
            this.layoutControlItem20.Size = new System.Drawing.Size(171, 24);
            this.layoutControlItem20.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem20.Text = "规格型号";
            this.layoutControlItem20.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem22
            // 
            this.layoutControlItem22.Control = this.txtUnit;
            this.layoutControlItem22.CustomizationFormText = "使用位置";
            this.layoutControlItem22.Location = new System.Drawing.Point(0, 49);
            this.layoutControlItem22.MaxSize = new System.Drawing.Size(175, 24);
            this.layoutControlItem22.MinSize = new System.Drawing.Size(105, 24);
            this.layoutControlItem22.Name = "layoutControlItem22";
            this.layoutControlItem22.Size = new System.Drawing.Size(152, 24);
            this.layoutControlItem22.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem22.Text = "单位";
            this.layoutControlItem22.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem23
            // 
            this.layoutControlItem23.Control = this.txtUsagePos;
            this.layoutControlItem23.CustomizationFormText = "备注信息";
            this.layoutControlItem23.Location = new System.Drawing.Point(152, 49);
            this.layoutControlItem23.MaxSize = new System.Drawing.Size(175, 24);
            this.layoutControlItem23.MinSize = new System.Drawing.Size(105, 24);
            this.layoutControlItem23.Name = "layoutControlItem23";
            this.layoutControlItem23.Size = new System.Drawing.Size(156, 24);
            this.layoutControlItem23.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem23.Text = "使用位置";
            this.layoutControlItem23.TextSize = new System.Drawing.Size(48, 14);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 73);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(693, 31);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem21
            // 
            this.layoutControlItem21.Control = this.btnSearch;
            this.layoutControlItem21.CustomizationFormText = "layoutControlItem21";
            this.layoutControlItem21.Location = new System.Drawing.Point(693, 73);
            this.layoutControlItem21.MaxSize = new System.Drawing.Size(83, 26);
            this.layoutControlItem21.MinSize = new System.Drawing.Size(39, 26);
            this.layoutControlItem21.Name = "layoutControlItem21";
            this.layoutControlItem21.Size = new System.Drawing.Size(63, 31);
            this.layoutControlItem21.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem21.Text = "layoutControlItem21";
            this.layoutControlItem21.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem21.TextToControlDistance = 0;
            this.layoutControlItem21.TextVisible = false;
            // 
            // layoutControlItem26
            // 
            this.layoutControlItem26.Control = this.btnReset;
            this.layoutControlItem26.CustomizationFormText = "layoutControlItem26";
            this.layoutControlItem26.Location = new System.Drawing.Point(756, 73);
            this.layoutControlItem26.MaxSize = new System.Drawing.Size(83, 26);
            this.layoutControlItem26.MinSize = new System.Drawing.Size(63, 26);
            this.layoutControlItem26.Name = "layoutControlItem26";
            this.layoutControlItem26.Size = new System.Drawing.Size(63, 31);
            this.layoutControlItem26.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem26.Text = "layoutControlItem26";
            this.layoutControlItem26.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem26.TextToControlDistance = 0;
            this.layoutControlItem26.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.txtStoragePos;
            this.layoutControlItem6.CustomizationFormText = "库位";
            this.layoutControlItem6.Location = new System.Drawing.Point(644, 49);
            this.layoutControlItem6.MaxSize = new System.Drawing.Size(175, 24);
            this.layoutControlItem6.MinSize = new System.Drawing.Size(105, 24);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(175, 24);
            this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem6.Text = "库位";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.txtPrice1;
            this.layoutControlItem8.CustomizationFormText = "layoutControlItem8";
            this.layoutControlItem8.Location = new System.Drawing.Point(308, 49);
            this.layoutControlItem8.MaxSize = new System.Drawing.Size(115, 24);
            this.layoutControlItem8.MinSize = new System.Drawing.Size(75, 24);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(99, 24);
            this.layoutControlItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem8.Text = "单价";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(48, 14);
            // 
            // simpleSeparator1
            // 
            this.simpleSeparator1.AllowHotTrack = false;
            this.simpleSeparator1.CustomizationFormText = "simpleSeparator1";
            this.simpleSeparator1.Location = new System.Drawing.Point(407, 49);
            this.simpleSeparator1.Name = "simpleSeparator1";
            this.simpleSeparator1.Size = new System.Drawing.Size(2, 24);
            this.simpleSeparator1.Text = "simpleSeparator1";
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.txtPrice2;
            this.layoutControlItem7.CustomizationFormText = "~";
            this.layoutControlItem7.Location = new System.Drawing.Point(409, 49);
            this.layoutControlItem7.MaxSize = new System.Drawing.Size(78, 24);
            this.layoutControlItem7.MinSize = new System.Drawing.Size(38, 24);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(65, 24);
            this.layoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem7.Text = "~";
            this.layoutControlItem7.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem7.TextSize = new System.Drawing.Size(9, 14);
            this.layoutControlItem7.TextToControlDistance = 5;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.txtStockQuantity1;
            this.layoutControlItem9.CustomizationFormText = "库存量";
            this.layoutControlItem9.Location = new System.Drawing.Point(474, 49);
            this.layoutControlItem9.MaxSize = new System.Drawing.Size(115, 24);
            this.layoutControlItem9.MinSize = new System.Drawing.Size(75, 24);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(97, 24);
            this.layoutControlItem9.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem9.Text = "库存量";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.txtStockQuantity2;
            this.layoutControlItem10.CustomizationFormText = "~";
            this.layoutControlItem10.Location = new System.Drawing.Point(571, 49);
            this.layoutControlItem10.MaxSize = new System.Drawing.Size(78, 24);
            this.layoutControlItem10.MinSize = new System.Drawing.Size(38, 24);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(73, 24);
            this.layoutControlItem10.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem10.Text = "~";
            this.layoutControlItem10.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem10.TextSize = new System.Drawing.Size(9, 14);
            this.layoutControlItem10.TextToControlDistance = 5;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.winGridView1);
            this.groupBox3.Location = new System.Drawing.Point(9, 140);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(839, 540);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "库存商品信息";
            // 
            // winGridView1
            // 
            this.winGridView1.AppendedMenu = null;
            this.winGridView1.ColumnNameAlias = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("winGridView1.ColumnNameAlias")));
            this.winGridView1.DataSource = null;
            this.winGridView1.DisplayColumns = "";
            this.winGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.winGridView1.Location = new System.Drawing.Point(3, 18);
            this.winGridView1.MinimumSize = new System.Drawing.Size(540, 0);
            this.winGridView1.Name = "winGridView1";
            this.winGridView1.PrintTitle = "";
            this.winGridView1.ShowAddMenu = true;
            this.winGridView1.ShowCheckBox = false;
            this.winGridView1.ShowDeleteMenu = true;
            this.winGridView1.ShowEditMenu = true;
            this.winGridView1.ShowExportButton = true;
            this.winGridView1.Size = new System.Drawing.Size(833, 519);
            this.winGridView1.TabIndex = 2;
            // 
            // lvwDetail
            // 
            this.lvwDetail.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader4,
            this.columnHeader13,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lvwDetail.ContextMenuStrip = this.contextMenuStrip1;
            this.lvwDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwDetail.FullRowSelect = true;
            this.lvwDetail.GridLines = true;
            this.lvwDetail.HideSelection = false;
            this.lvwDetail.Location = new System.Drawing.Point(0, 0);
            this.lvwDetail.MultiSelect = false;
            this.lvwDetail.Name = "lvwDetail";
            this.lvwDetail.Size = new System.Drawing.Size(915, 684);
            this.lvwDetail.TabIndex = 3;
            this.lvwDetail.UseCompatibleStateImageBehavior = false;
            this.lvwDetail.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "项目编号";
            this.columnHeader5.Width = 80;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "项目名称";
            this.columnHeader6.Width = 120;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "备件属类";
            this.columnHeader4.Width = 100;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "备件类别";
            this.columnHeader13.Width = 100;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "单位";
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "单价";
            this.columnHeader8.Width = 70;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "数量";
            this.columnHeader9.Width = 80;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "金额";
            this.columnHeader10.Width = 120;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "使用位置";
            this.columnHeader1.Width = 100;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "所属库房";
            this.columnHeader2.Width = 120;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "所属部门";
            this.columnHeader3.Width = 120;
            // 
            // treeGoods
            // 
            this.treeGoods.ContextMenuStrip = this.contextMenuStrip2;
            this.treeGoods.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeGoods.FullRowSelect = true;
            this.treeGoods.HideSelection = false;
            this.treeGoods.ImageIndex = 0;
            this.treeGoods.ImageList = this.imageList1;
            this.treeGoods.Location = new System.Drawing.Point(0, 0);
            this.treeGoods.Name = "treeGoods";
            this.treeGoods.SelectedImageIndex = 0;
            this.treeGoods.Size = new System.Drawing.Size(212, 684);
            this.treeGoods.TabIndex = 2;
            this.treeGoods.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeGoods_AfterSelect);
            this.treeGoods.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeGoods_NodeMouseDoubleClick);
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(1138, 713);
            this.xtraTabControl1.TabIndex = 16;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.splitContainerControl1);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(1132, 684);
            this.xtraTabPage1.Text = "库存查询";
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel1;
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.groupControl1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.layoutControl1);
            this.splitContainerControl1.Panel2.Controls.Add(this.groupBox3);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1132, 684);
            this.splitContainerControl1.SplitterPosition = 271;
            this.splitContainerControl1.TabIndex = 15;
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
            this.groupControl1.Size = new System.Drawing.Size(271, 684);
            this.groupControl1.TabIndex = 28;
            this.groupControl1.Text = "库存分类";
            // 
            // treeView1
            // 
            this.treeView1.ContextMenuStrip = this.menuTree;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.FullRowSelect = true;
            this.treeView1.HideSelection = false;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageListTree;
            this.treeView1.Location = new System.Drawing.Point(2, 22);
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
            this.treeView1.Size = new System.Drawing.Size(267, 660);
            this.treeView1.TabIndex = 0;
            this.treeView1.TabStop = false;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // menuTree
            // 
            this.menuTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuTree_ExpandAll,
            this.menuTree_Clapase,
            this.menuTree_Refresh});
            this.menuTree.Name = "menuTree";
            this.menuTree.Size = new System.Drawing.Size(141, 70);
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
            // imageListTree
            // 
            this.imageListTree.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTree.ImageStream")));
            this.imageListTree.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListTree.Images.SetKeyName(0, "Tip.png");
            this.imageListTree.Images.SetKeyName(1, "re.ico");
            this.imageListTree.Images.SetKeyName(2, "paint.ico");
            this.imageListTree.Images.SetKeyName(3, "ima.ico");
            this.imageListTree.Images.SetKeyName(4, "sm.ico");
            this.imageListTree.Images.SetKeyName(5, "accept.ico");
            this.imageListTree.Images.SetKeyName(6, "tra.ico");
            this.imageListTree.Images.SetKeyName(7, "configure.ico");
            this.imageListTree.Images.SetKeyName(8, "chart.ico");
            this.imageListTree.Images.SetKeyName(9, "wizard.ico");
            this.imageListTree.Images.SetKeyName(10, "usergroup6.ico");
            this.imageListTree.Images.SetKeyName(11, "user005.ico");
            this.imageListTree.Images.SetKeyName(12, "Setting_3.ico");
            this.imageListTree.Images.SetKeyName(13, "support.ico");
            this.imageListTree.Images.SetKeyName(14, "users.png");
            this.imageListTree.Images.SetKeyName(15, "userTalk.ICO");
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.splitContainerControl2);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(1132, 684);
            this.xtraTabPage2.Text = "库房项目明细";
            // 
            // splitContainerControl2
            // 
            this.splitContainerControl2.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel1;
            this.splitContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl2.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl2.Name = "splitContainerControl2";
            this.splitContainerControl2.Panel1.Controls.Add(this.treeGoods);
            this.splitContainerControl2.Panel1.Text = "Panel1";
            this.splitContainerControl2.Panel2.Controls.Add(this.lvwDetail);
            this.splitContainerControl2.Panel2.Text = "Panel2";
            this.splitContainerControl2.Size = new System.Drawing.Size(1132, 684);
            this.splitContainerControl2.SplitterPosition = 212;
            this.splitContainerControl2.TabIndex = 0;
            this.splitContainerControl2.Text = "splitContainerControl2";
            // 
            // FrmStockSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1138, 713);
            this.Controls.Add(this.xtraTabControl1);
            this.Name = "FrmStockSearch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "库存查询";
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtUsagePos.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMapNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtItemNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtItemType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtManufacture.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSpecNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSource.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUnit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBigType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaterial.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStoragePos.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearchWareHouse.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem17)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem18)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem19)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem20)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem22)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem23)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem21)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem26)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.menuTree.ResumeLayout(false);
            this.xtraTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).EndInit();
            this.splitContainerControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menu_SetAlarm;
        private System.Windows.Forms.ToolStripMenuItem menu_ModifyStock;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.GroupBox groupBox3;
        private WinGridView winGridView1;
        private DevExpress.XtraEditors.TextEdit txtStockQuantity2;
        private DevExpress.XtraEditors.TextEdit txtStockQuantity1;
        private DevExpress.XtraEditors.TextEdit txtPrice2;
        private DevExpress.XtraEditors.TextEdit txtPrice1;
        private System.Windows.Forms.TreeView treeGoods;
        private System.Windows.Forms.ListView lvwDetail;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem menu_ClapseAll;
        private System.Windows.Forms.ToolStripMenuItem menu_ExpandAll;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ToolStripMenuItem menu_Refresh;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ToolStripMenuItem menu_DeleteZero;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.TextEdit txtUsagePos;
        private DevExpress.XtraEditors.TextEdit txtMapNo;
        private DevExpress.XtraEditors.TextEdit txtItemNo;
        private DevExpress.XtraEditors.ComboBoxEdit txtItemType;
        private DevExpress.XtraEditors.ComboBoxEdit txtManufacture;
        private DevExpress.XtraEditors.ComboBoxEdit cmbSpecNumber;
        private DevExpress.XtraEditors.ComboBoxEdit txtSource;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.ComboBoxEdit txtUnit;
        private DevExpress.XtraEditors.SimpleButton btnReset;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem16;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem17;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem18;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem19;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem20;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem22;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem23;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem21;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem26;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraEditors.ComboBoxEdit txtBigType;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.TextEdit txtMaterial;
        private DevExpress.XtraEditors.ComboBoxEdit txtStoragePos;
        private DevExpress.XtraEditors.ComboBoxEdit txtSearchWareHouse;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraEditors.SpinEdit spinEdit1;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem1;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl2;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ContextMenuStrip menuTree;
        private System.Windows.Forms.ToolStripMenuItem menuTree_ExpandAll;
        private System.Windows.Forms.ToolStripMenuItem menuTree_Clapase;
        private System.Windows.Forms.ToolStripMenuItem menuTree_Refresh;
        private System.Windows.Forms.ImageList imageListTree;

    }
}