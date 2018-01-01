namespace JCodes.Framework.AddIn.Proj
{
    partial class FrmSysFunction
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSysFunction));
            this.btnAddNodes = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.btnImport = new DevExpress.XtraEditors.SimpleButton();
            this.btnExport = new DevExpress.XtraEditors.SimpleButton();
            this.btnDel = new DevExpress.XtraEditors.SimpleButton();
            this.btnAddRoot = new DevExpress.XtraEditors.SimpleButton();
            this.txtSearch = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcitxtSearch = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciadd = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciInsert = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciDel = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciexport = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciimport = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcisearch = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barErrText = new DevExpress.XtraBars.BarStaticItem();
            this.barWarningText = new DevExpress.XtraBars.BarStaticItem();
            this.barInfoText = new DevExpress.XtraBars.BarStaticItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.treelstFunction = new DevExpress.XtraTreeList.TreeList();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcitxtSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciadd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciInsert)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciDel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciexport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciimport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcisearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treelstFunction)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAddNodes
            // 
            this.btnAddNodes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddNodes.Location = new System.Drawing.Point(403, 12);
            this.btnAddNodes.Name = "btnAddNodes";
            this.btnAddNodes.Size = new System.Drawing.Size(102, 29);
            this.btnAddNodes.StyleController = this.layoutControl1;
            this.btnAddNodes.TabIndex = 3;
            this.btnAddNodes.Text = "新增子节点";
            this.btnAddNodes.Click += new System.EventHandler(this.btnAddNodes_Click);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.layoutControl1.Controls.Add(this.btnSearch);
            this.layoutControl1.Controls.Add(this.btnImport);
            this.layoutControl1.Controls.Add(this.btnExport);
            this.layoutControl1.Controls.Add(this.btnDel);
            this.layoutControl1.Controls.Add(this.btnAddRoot);
            this.layoutControl1.Controls.Add(this.btnAddNodes);
            this.layoutControl1.Controls.Add(this.txtSearch);
            this.layoutControl1.Location = new System.Drawing.Point(12, 8);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(588, 392, 810, 624);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(980, 53);
            this.layoutControl1.TabIndex = 12;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(192, 12);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(76, 29);
            this.btnSearch.StyleController = this.layoutControl1;
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "查询";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(661, 12);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(72, 29);
            this.btnImport.StyleController = this.layoutControl1;
            this.btnImport.TabIndex = 8;
            this.btnImport.Text = "导入";
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(585, 12);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(72, 29);
            this.btnExport.StyleController = this.layoutControl1;
            this.btnExport.TabIndex = 7;
            this.btnExport.Text = "导出";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(509, 12);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(72, 29);
            this.btnDel.StyleController = this.layoutControl1;
            this.btnDel.TabIndex = 4;
            this.btnDel.Text = "删除";
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnAddRoot
            // 
            this.btnAddRoot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddRoot.Location = new System.Drawing.Point(323, 12);
            this.btnAddRoot.Name = "btnAddRoot";
            this.btnAddRoot.Size = new System.Drawing.Size(76, 29);
            this.btnAddRoot.StyleController = this.layoutControl1;
            this.btnAddRoot.TabIndex = 2;
            this.btnAddRoot.Text = "新增节点";
            this.btnAddRoot.Click += new System.EventHandler(this.btnAddRoot_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(12, 12);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(176, 28);
            this.txtSearch.StyleController = this.layoutControl1;
            this.txtSearch.TabIndex = 0;
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "Root";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcitxtSearch,
            this.lciadd,
            this.lciInsert,
            this.lciDel,
            this.lciexport,
            this.lciimport,
            this.lcisearch,
            this.emptySpaceItem1,
            this.emptySpaceItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(980, 53);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcitxtSearch
            // 
            this.lcitxtSearch.Control = this.txtSearch;
            this.lcitxtSearch.CustomizationFormText = "显示名称";
            this.lcitxtSearch.Location = new System.Drawing.Point(0, 0);
            this.lcitxtSearch.Name = "lcitxtSearch";
            this.lcitxtSearch.Size = new System.Drawing.Size(180, 33);
            this.lcitxtSearch.Text = "显示名称";
            this.lcitxtSearch.TextSize = new System.Drawing.Size(0, 0);
            this.lcitxtSearch.TextToControlDistance = 0;
            this.lcitxtSearch.TextVisible = false;
            // 
            // lciadd
            // 
            this.lciadd.Control = this.btnAddRoot;
            this.lciadd.CustomizationFormText = "lciadd";
            this.lciadd.Location = new System.Drawing.Point(311, 0);
            this.lciadd.MaxSize = new System.Drawing.Size(0, 33);
            this.lciadd.MinSize = new System.Drawing.Size(51, 33);
            this.lciadd.Name = "lciadd";
            this.lciadd.Size = new System.Drawing.Size(80, 33);
            this.lciadd.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciadd.Text = "lciadd";
            this.lciadd.TextSize = new System.Drawing.Size(0, 0);
            this.lciadd.TextToControlDistance = 0;
            this.lciadd.TextVisible = false;
            // 
            // lciInsert
            // 
            this.lciInsert.Control = this.btnAddNodes;
            this.lciInsert.CustomizationFormText = "lciInsert";
            this.lciInsert.Location = new System.Drawing.Point(391, 0);
            this.lciInsert.MaxSize = new System.Drawing.Size(0, 33);
            this.lciInsert.MinSize = new System.Drawing.Size(51, 33);
            this.lciInsert.Name = "lciInsert";
            this.lciInsert.Size = new System.Drawing.Size(106, 33);
            this.lciInsert.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciInsert.Text = "lciInsert";
            this.lciInsert.TextSize = new System.Drawing.Size(0, 0);
            this.lciInsert.TextToControlDistance = 0;
            this.lciInsert.TextVisible = false;
            // 
            // lciDel
            // 
            this.lciDel.Control = this.btnDel;
            this.lciDel.CustomizationFormText = "lciDel";
            this.lciDel.Location = new System.Drawing.Point(497, 0);
            this.lciDel.MaxSize = new System.Drawing.Size(0, 33);
            this.lciDel.MinSize = new System.Drawing.Size(51, 33);
            this.lciDel.Name = "lciDel";
            this.lciDel.Size = new System.Drawing.Size(76, 33);
            this.lciDel.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciDel.Text = "lciDel";
            this.lciDel.TextSize = new System.Drawing.Size(0, 0);
            this.lciDel.TextToControlDistance = 0;
            this.lciDel.TextVisible = false;
            // 
            // lciexport
            // 
            this.lciexport.Control = this.btnExport;
            this.lciexport.CustomizationFormText = "lciexport";
            this.lciexport.Location = new System.Drawing.Point(573, 0);
            this.lciexport.MaxSize = new System.Drawing.Size(0, 33);
            this.lciexport.MinSize = new System.Drawing.Size(51, 33);
            this.lciexport.Name = "lciexport";
            this.lciexport.Size = new System.Drawing.Size(76, 33);
            this.lciexport.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciexport.Text = "lciexport";
            this.lciexport.TextSize = new System.Drawing.Size(0, 0);
            this.lciexport.TextToControlDistance = 0;
            this.lciexport.TextVisible = false;
            // 
            // lciimport
            // 
            this.lciimport.Control = this.btnImport;
            this.lciimport.CustomizationFormText = "lciimport";
            this.lciimport.Location = new System.Drawing.Point(649, 0);
            this.lciimport.MaxSize = new System.Drawing.Size(0, 33);
            this.lciimport.MinSize = new System.Drawing.Size(51, 33);
            this.lciimport.Name = "lciimport";
            this.lciimport.Size = new System.Drawing.Size(76, 33);
            this.lciimport.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciimport.Text = "lciimport";
            this.lciimport.TextSize = new System.Drawing.Size(0, 0);
            this.lciimport.TextToControlDistance = 0;
            this.lciimport.TextVisible = false;
            // 
            // lcisearch
            // 
            this.lcisearch.Control = this.btnSearch;
            this.lcisearch.CustomizationFormText = "lcisearch";
            this.lcisearch.Location = new System.Drawing.Point(180, 0);
            this.lcisearch.Name = "lcisearch";
            this.lcisearch.Size = new System.Drawing.Size(80, 33);
            this.lcisearch.Text = "lcisearch";
            this.lcisearch.TextSize = new System.Drawing.Size(0, 0);
            this.lcisearch.TextToControlDistance = 0;
            this.lcisearch.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(725, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(235, 33);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(260, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(51, 33);
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar3});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Images = this.imageList1;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barErrText,
            this.barWarningText,
            this.barInfoText});
            this.barManager1.MaxItemId = 5;
            this.barManager1.StatusBar = this.bar3;
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barErrText, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barWarningText, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barInfoText, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // barErrText
            // 
            this.barErrText.Caption = "0 条错误信息";
            this.barErrText.Id = 1;
            this.barErrText.ImageIndex = 0;
            this.barErrText.ItemAppearance.Normal.ForeColor = System.Drawing.Color.Red;
            this.barErrText.ItemAppearance.Normal.Options.UseForeColor = true;
            this.barErrText.Name = "barErrText";
            this.barErrText.TextAlignment = System.Drawing.StringAlignment.Near;
            this.barErrText.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barErrText_ItemClick);
            // 
            // barWarningText
            // 
            this.barWarningText.Caption = "0 条警告信息";
            this.barWarningText.Id = 3;
            this.barWarningText.ImageIndex = 1;
            this.barWarningText.ItemAppearance.Normal.ForeColor = System.Drawing.Color.DarkOrange;
            this.barWarningText.ItemAppearance.Normal.Options.UseForeColor = true;
            this.barWarningText.Name = "barWarningText";
            this.barWarningText.TextAlignment = System.Drawing.StringAlignment.Near;
            this.barWarningText.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barWarningText_ItemClick);
            // 
            // barInfoText
            // 
            this.barInfoText.Caption = "0 条提示信息";
            this.barInfoText.Id = 4;
            this.barInfoText.ImageIndex = 2;
            this.barInfoText.ItemAppearance.Normal.ForeColor = System.Drawing.Color.Blue;
            this.barInfoText.ItemAppearance.Normal.Options.UseForeColor = true;
            this.barInfoText.Name = "barInfoText";
            this.barInfoText.TextAlignment = System.Drawing.StringAlignment.Near;
            this.barInfoText.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barInfoText_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1004, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 645);
            this.barDockControlBottom.Size = new System.Drawing.Size(1004, 35);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 645);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1004, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 645);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "001.png");
            this.imageList1.Images.SetKeyName(1, "002.png");
            this.imageList1.Images.SetKeyName(2, "003.png");
            // 
            // treelstFunction
            // 
            this.treelstFunction.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treelstFunction.Location = new System.Drawing.Point(11, 67);
            this.treelstFunction.Name = "treelstFunction";
            this.treelstFunction.Size = new System.Drawing.Size(981, 572);
            this.treelstFunction.TabIndex = 18;
            this.treelstFunction.ValidateNode += new DevExpress.XtraTreeList.ValidateNodeEventHandler(this.treelstFunction_ValidateNode);
            this.treelstFunction.CellValueChanged += new DevExpress.XtraTreeList.CellValueChangedEventHandler(this.treelstFunction_CellValueChanged);
            this.treelstFunction.DragDrop += new System.Windows.Forms.DragEventHandler(this.treelstFunction_DragDrop);
            // 
            // FrmSysFunction
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 680);
            this.Controls.Add(this.treelstFunction);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FrmSysFunction";
            this.Text = "系统功能";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcitxtSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciadd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciInsert)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciDel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciexport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciimport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcisearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treelstFunction)).EndInit();
            this.ResumeLayout(false);

}

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnAddRoot;
        private DevExpress.XtraEditors.SimpleButton btnAddNodes;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.TextEdit txtSearch;
        private DevExpress.XtraLayout.LayoutControlItem lcitxtSearch;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem lciadd;
        private DevExpress.XtraLayout.LayoutControlItem lciInsert;
        private DevExpress.XtraEditors.SimpleButton btnDel;
        private DevExpress.XtraLayout.LayoutControlItem lciDel;
        private DevExpress.XtraEditors.SimpleButton btnImport;
        private DevExpress.XtraEditors.SimpleButton btnExport;
        private DevExpress.XtraLayout.LayoutControlItem lciexport;
        private DevExpress.XtraLayout.LayoutControlItem lciimport;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraLayout.LayoutControlItem lcisearch;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarStaticItem barErrText;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private System.Windows.Forms.ImageList imageList1;
        private DevExpress.XtraBars.BarStaticItem barWarningText;
        private DevExpress.XtraBars.BarStaticItem barInfoText;
        private DevExpress.XtraTreeList.TreeList treelstFunction;

    }
}