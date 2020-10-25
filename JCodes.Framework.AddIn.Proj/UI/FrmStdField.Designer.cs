namespace JCodes.Framework.AddIn.Proj
{
    partial class FrmStdField
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmStdField));
            this.btnInsert = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.btnImport = new DevExpress.XtraEditors.SimpleButton();
            this.btnExport = new DevExpress.XtraEditors.SimpleButton();
            this.btnMoveDown = new DevExpress.XtraEditors.SimpleButton();
            this.btnMoveUp = new DevExpress.XtraEditors.SimpleButton();
            this.btnDel = new DevExpress.XtraEditors.SimpleButton();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.txtSearch = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcitxtSearch = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciadd = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciInsert = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciDel = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcimoveup = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcimovedown = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciexport = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciimport = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcisearch = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryItemLookUpEditStdType = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
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
            this.contextMenuStripFields = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmirealoadcache = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcitxtSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciadd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciInsert)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciDel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcimoveup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcimovedown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciexport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciimport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcisearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEditStdType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.contextMenuStripFields.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnInsert
            // 
            this.btnInsert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInsert.Location = new System.Drawing.Point(403, 12);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(76, 29);
            this.btnInsert.StyleController = this.layoutControl1;
            this.btnInsert.TabIndex = 3;
            this.btnInsert.Text = "插入";
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.layoutControl1.Controls.Add(this.btnSearch);
            this.layoutControl1.Controls.Add(this.btnImport);
            this.layoutControl1.Controls.Add(this.btnExport);
            this.layoutControl1.Controls.Add(this.btnMoveDown);
            this.layoutControl1.Controls.Add(this.btnMoveUp);
            this.layoutControl1.Controls.Add(this.btnDel);
            this.layoutControl1.Controls.Add(this.btnAdd);
            this.layoutControl1.Controls.Add(this.btnInsert);
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
            this.btnImport.Location = new System.Drawing.Point(803, 12);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(76, 29);
            this.btnImport.StyleController = this.layoutControl1;
            this.btnImport.TabIndex = 8;
            this.btnImport.Text = "导入";
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(723, 12);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(76, 29);
            this.btnExport.StyleController = this.layoutControl1;
            this.btnExport.TabIndex = 7;
            this.btnExport.Text = "导出";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.Location = new System.Drawing.Point(643, 12);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(76, 29);
            this.btnMoveDown.StyleController = this.layoutControl1;
            this.btnMoveDown.TabIndex = 6;
            this.btnMoveDown.Text = "下移";
            this.btnMoveDown.Click += new System.EventHandler(this.btnMoveDown_Click);
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.Location = new System.Drawing.Point(563, 12);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(76, 29);
            this.btnMoveUp.StyleController = this.layoutControl1;
            this.btnMoveUp.TabIndex = 5;
            this.btnMoveUp.Text = "上移";
            this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(483, 12);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(76, 29);
            this.btnDel.StyleController = this.layoutControl1;
            this.btnDel.TabIndex = 4;
            this.btnDel.Text = "删除";
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(323, 12);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(76, 29);
            this.btnAdd.StyleController = this.layoutControl1;
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "新增";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
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
            this.lcimoveup,
            this.lcimovedown,
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
            this.lciadd.Control = this.btnAdd;
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
            this.lciInsert.Control = this.btnInsert;
            this.lciInsert.CustomizationFormText = "lciInsert";
            this.lciInsert.Location = new System.Drawing.Point(391, 0);
            this.lciInsert.MaxSize = new System.Drawing.Size(0, 33);
            this.lciInsert.MinSize = new System.Drawing.Size(51, 33);
            this.lciInsert.Name = "lciInsert";
            this.lciInsert.Size = new System.Drawing.Size(80, 33);
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
            this.lciDel.Location = new System.Drawing.Point(471, 0);
            this.lciDel.MaxSize = new System.Drawing.Size(0, 33);
            this.lciDel.MinSize = new System.Drawing.Size(51, 33);
            this.lciDel.Name = "lciDel";
            this.lciDel.Size = new System.Drawing.Size(80, 33);
            this.lciDel.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciDel.Text = "lciDel";
            this.lciDel.TextSize = new System.Drawing.Size(0, 0);
            this.lciDel.TextToControlDistance = 0;
            this.lciDel.TextVisible = false;
            // 
            // lcimoveup
            // 
            this.lcimoveup.Control = this.btnMoveUp;
            this.lcimoveup.CustomizationFormText = "lcimoveup";
            this.lcimoveup.Location = new System.Drawing.Point(551, 0);
            this.lcimoveup.MaxSize = new System.Drawing.Size(0, 33);
            this.lcimoveup.MinSize = new System.Drawing.Size(51, 33);
            this.lcimoveup.Name = "lcimoveup";
            this.lcimoveup.Size = new System.Drawing.Size(80, 33);
            this.lcimoveup.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcimoveup.Text = "lcimoveup";
            this.lcimoveup.TextSize = new System.Drawing.Size(0, 0);
            this.lcimoveup.TextToControlDistance = 0;
            this.lcimoveup.TextVisible = false;
            // 
            // lcimovedown
            // 
            this.lcimovedown.Control = this.btnMoveDown;
            this.lcimovedown.CustomizationFormText = "lcimovedown";
            this.lcimovedown.Location = new System.Drawing.Point(631, 0);
            this.lcimovedown.MaxSize = new System.Drawing.Size(0, 33);
            this.lcimovedown.MinSize = new System.Drawing.Size(51, 33);
            this.lcimovedown.Name = "lcimovedown";
            this.lcimovedown.Size = new System.Drawing.Size(80, 33);
            this.lcimovedown.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcimovedown.Text = "lcimovedown";
            this.lcimovedown.TextSize = new System.Drawing.Size(0, 0);
            this.lcimovedown.TextToControlDistance = 0;
            this.lcimovedown.TextVisible = false;
            // 
            // lciexport
            // 
            this.lciexport.Control = this.btnExport;
            this.lciexport.CustomizationFormText = "lciexport";
            this.lciexport.Location = new System.Drawing.Point(711, 0);
            this.lciexport.MaxSize = new System.Drawing.Size(0, 33);
            this.lciexport.MinSize = new System.Drawing.Size(51, 33);
            this.lciexport.Name = "lciexport";
            this.lciexport.Size = new System.Drawing.Size(80, 33);
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
            this.lciimport.Location = new System.Drawing.Point(791, 0);
            this.lciimport.MaxSize = new System.Drawing.Size(0, 33);
            this.lciimport.MinSize = new System.Drawing.Size(51, 33);
            this.lciimport.Name = "lciimport";
            this.lciimport.Size = new System.Drawing.Size(80, 33);
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
            this.emptySpaceItem1.Location = new System.Drawing.Point(871, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(89, 33);
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
            // gridControl1
            // 
            this.gridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl1.ContextMenuStrip = this.contextMenuStripFields;
            this.gridControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridControl1.Location = new System.Drawing.Point(11, 67);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemLookUpEditStdType});
            this.gridControl1.Size = new System.Drawing.Size(981, 573);
            this.gridControl1.TabIndex = 13;
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
            this.gridView1.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView1_CellValueChanged);
            this.gridView1.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView1_CellValueChanging);
            this.gridView1.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.gridView1_ValidateRow);
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
            // contextMenuStripFields
            // 
            this.contextMenuStripFields.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStripFields.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmirealoadcache});
            this.contextMenuStripFields.Name = "contextMenuStripIndex";
            this.contextMenuStripFields.Size = new System.Drawing.Size(189, 32);
            // 
            // tsmirealoadcache
            // 
            this.tsmirealoadcache.Name = "tsmirealoadcache";
            this.tsmirealoadcache.Size = new System.Drawing.Size(188, 28);
            this.tsmirealoadcache.Text = "更新标准字段";
            this.tsmirealoadcache.Click += new System.EventHandler(this.tsmirealoadcache_Click);
            // 
            // FrmStdField
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 680);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FrmStdField";
            this.Text = "标准字段";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcitxtSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciadd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciInsert)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciDel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcimoveup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcimovedown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciexport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciimport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcisearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEditStdType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.contextMenuStripFields.ResumeLayout(false);
            this.ResumeLayout(false);

}

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraEditors.SimpleButton btnInsert;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.TextEdit txtSearch;
        private DevExpress.XtraLayout.LayoutControlItem lcitxtSearch;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem lciadd;
        private DevExpress.XtraLayout.LayoutControlItem lciInsert;
        private DevExpress.XtraEditors.SimpleButton btnMoveUp;
        private DevExpress.XtraEditors.SimpleButton btnDel;
        private DevExpress.XtraLayout.LayoutControlItem lciDel;
        private DevExpress.XtraLayout.LayoutControlItem lcimoveup;
        private DevExpress.XtraEditors.SimpleButton btnMoveDown;
        private DevExpress.XtraLayout.LayoutControlItem lcimovedown;
        private DevExpress.XtraEditors.SimpleButton btnImport;
        private DevExpress.XtraEditors.SimpleButton btnExport;
        private DevExpress.XtraLayout.LayoutControlItem lciexport;
        private DevExpress.XtraLayout.LayoutControlItem lciimport;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraLayout.LayoutControlItem lcisearch;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
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
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEditStdType;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripFields;
        private System.Windows.Forms.ToolStripMenuItem tsmirealoadcache;

    }
}