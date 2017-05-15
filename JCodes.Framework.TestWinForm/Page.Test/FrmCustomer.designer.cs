namespace WHC.OrderWater.UI
{
    partial class FrmCustomer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCustomer));
            this.btnNoOrderCustomer = new DevExpress.XtraEditors.SimpleButton();
            this.chkUseDate = new System.Windows.Forms.CheckBox();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnAddNew = new DevExpress.XtraEditors.SimpleButton();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.cmbArea = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNote = new DevExpress.XtraEditors.TextEdit();
            this.label10 = new System.Windows.Forms.Label();
            this.txtTelephone = new DevExpress.XtraEditors.TextEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCompany = new DevExpress.XtraEditors.TextEdit();
            this.label7 = new System.Windows.Forms.Label();
            this.txtAddress = new DevExpress.XtraEditors.TextEdit();
            this.label6 = new System.Windows.Forms.Label();
            this.txtNumber = new DevExpress.XtraEditors.TextEdit();
            this.label5 = new System.Windows.Forms.Label();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.winGridViewPager1 = new JCodes.Framework.CommonControl.WinGridViewPager();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menu_NewOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_BuyTicket = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_NoOrderCustomer = new System.Windows.Forms.ToolStripMenuItem();
            this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnGetCheckedRows = new DevExpress.XtraEditors.SimpleButton();
            this.btnSecondPager = new DevExpress.XtraEditors.SimpleButton();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.label11 = new System.Windows.Forms.Label();
            this.lblCheckboxSelection = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.txtNote.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTelephone.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompany.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnNoOrderCustomer
            // 
            this.btnNoOrderCustomer.Location = new System.Drawing.Point(830, 75);
            this.btnNoOrderCustomer.Name = "btnNoOrderCustomer";
            this.btnNoOrderCustomer.Size = new System.Drawing.Size(99, 27);
            this.btnNoOrderCustomer.TabIndex = 11;
            this.btnNoOrderCustomer.Text = "显示未订货客户";
            this.btnNoOrderCustomer.Click += new System.EventHandler(this.btnNoOrderCustomer_Click);
            // 
            // chkUseDate
            // 
            this.chkUseDate.AutoSize = true;
            this.chkUseDate.Location = new System.Drawing.Point(391, 79);
            this.chkUseDate.Name = "chkUseDate";
            this.chkUseDate.Size = new System.Drawing.Size(50, 18);
            this.chkUseDate.TabIndex = 8;
            this.chkUseDate.Text = "启用";
            this.chkUseDate.CheckedChanged += new System.EventHandler(this.chkUseDate_CheckedChanged);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(243, 75);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(140, 22);
            this.dateTimePicker2.TabIndex = 14;
            this.dateTimePicker2.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SearchControl_KeyUp);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(78, 75);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(140, 22);
            this.dateTimePicker1.TabIndex = 13;
            this.dateTimePicker1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SearchControl_KeyUp);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(223, 80);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(19, 14);
            this.label8.TabIndex = 11;
            this.label8.Text = "至";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 80);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(55, 14);
            this.label9.TabIndex = 12;
            this.label9.Text = "开户日期";
            // 
            // btnAddNew
            // 
            this.btnAddNew.Location = new System.Drawing.Point(658, 76);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(55, 27);
            this.btnAddNew.TabIndex = 10;
            this.btnAddNew.Text = "新建";
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(588, 76);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(54, 27);
            this.btnSearch.TabIndex = 9;
            this.btnSearch.Text = "查询";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cmbArea
            // 
            this.cmbArea.FormattingEnabled = true;
            this.cmbArea.Location = new System.Drawing.Point(540, 12);
            this.cmbArea.Name = "cmbArea";
            this.cmbArea.Size = new System.Drawing.Size(140, 22);
            this.cmbArea.TabIndex = 2;
            this.cmbArea.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtName_KeyDown);
            this.cmbArea.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SearchControl_KeyUp);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(474, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 14);
            this.label3.TabIndex = 0;
            this.label3.Text = "客户地区";
            // 
            // cmbType
            // 
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Location = new System.Drawing.Point(292, 12);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(140, 22);
            this.cmbType.TabIndex = 1;
            this.cmbType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtName_KeyDown);
            this.cmbType.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SearchControl_KeyUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(232, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 14);
            this.label2.TabIndex = 0;
            this.label2.Text = "客户类型";
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(788, 44);
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(141, 20);
            this.txtNote.TabIndex = 7;
            this.txtNote.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtName_KeyDown);
            this.txtNote.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SearchControl_KeyUp);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(726, 47);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(55, 14);
            this.label10.TabIndex = 0;
            this.label10.Text = "备注信息";
            // 
            // txtTelephone
            // 
            this.txtTelephone.Location = new System.Drawing.Point(788, 12);
            this.txtTelephone.Name = "txtTelephone";
            this.txtTelephone.Size = new System.Drawing.Size(141, 20);
            this.txtTelephone.TabIndex = 3;
            this.txtTelephone.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtName_KeyDown);
            this.txtTelephone.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SearchControl_KeyUp);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(726, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 14);
            this.label4.TabIndex = 0;
            this.label4.Text = "联系电话";
            // 
            // txtCompany
            // 
            this.txtCompany.Location = new System.Drawing.Point(540, 44);
            this.txtCompany.Name = "txtCompany";
            this.txtCompany.Size = new System.Drawing.Size(141, 20);
            this.txtCompany.TabIndex = 6;
            this.txtCompany.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtName_KeyDown);
            this.txtCompany.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SearchControl_KeyUp);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(478, 47);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 14);
            this.label7.TabIndex = 0;
            this.label7.Text = "客户单位";
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(292, 44);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(141, 20);
            this.txtAddress.TabIndex = 5;
            this.txtAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtName_KeyDown);
            this.txtAddress.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SearchControl_KeyUp);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(230, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 14);
            this.label6.TabIndex = 0;
            this.label6.Text = "客户地址";
            // 
            // txtNumber
            // 
            this.txtNumber.Location = new System.Drawing.Point(78, 44);
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.Size = new System.Drawing.Size(141, 20);
            this.txtNumber.TabIndex = 4;
            this.txtNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtName_KeyDown);
            this.txtNumber.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SearchControl_KeyUp);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 14);
            this.label5.TabIndex = 0;
            this.label5.Text = "客户编号";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(78, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(141, 20);
            this.txtName.TabIndex = 0;
            this.txtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtName_KeyDown);
            this.txtName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SearchControl_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "客户名称";
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
            this.winGridViewPager1.Location = new System.Drawing.Point(14, 164);
            this.winGridViewPager1.MinimumSize = new System.Drawing.Size(630, 0);
            this.winGridViewPager1.Name = "winGridViewPager1";
            this.winGridViewPager1.PrintTitle = "";
            this.winGridViewPager1.ShowAddMenu = true;
            this.winGridViewPager1.ShowCheckBox = false;
            this.winGridViewPager1.ShowDeleteMenu = true;
            this.winGridViewPager1.ShowEditMenu = true;
            this.winGridViewPager1.ShowExportButton = true;
            this.winGridViewPager1.Size = new System.Drawing.Size(998, 605);
            this.winGridViewPager1.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_NewOrder,
            this.menu_BuyTicket,
            this.menu_NoOrderCustomer});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(176, 70);
            // 
            // menu_NewOrder
            // 
            this.menu_NewOrder.Name = "menu_NewOrder";
            this.menu_NewOrder.Size = new System.Drawing.Size(175, 22);
            this.menu_NewOrder.Text = "新建订单(&O)";
            this.menu_NewOrder.Click += new System.EventHandler(this.menu_NewOrder_Click);
            // 
            // menu_BuyTicket
            // 
            this.menu_BuyTicket.Name = "menu_BuyTicket";
            this.menu_BuyTicket.Size = new System.Drawing.Size(175, 22);
            this.menu_BuyTicket.Text = "购买水票(&T)";
            this.menu_BuyTicket.Click += new System.EventHandler(this.menu_BuyTicket_Click);
            // 
            // menu_NoOrderCustomer
            // 
            this.menu_NoOrderCustomer.Name = "menu_NoOrderCustomer";
            this.menu_NoOrderCustomer.Size = new System.Drawing.Size(175, 22);
            this.menu_NoOrderCustomer.Text = "显示未订货客户(&Y)";
            this.menu_NoOrderCustomer.Click += new System.EventHandler(this.menu_NoOrderCustomer_Click);
            // 
            // defaultLookAndFeel1
            // 
            this.defaultLookAndFeel1.LookAndFeel.SkinName = "Office 2010 Blue";
            // 
            // panelControl1
            // 
            this.panelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl1.Controls.Add(this.btnGetCheckedRows);
            this.panelControl1.Controls.Add(this.btnNoOrderCustomer);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Controls.Add(this.chkUseDate);
            this.panelControl1.Controls.Add(this.txtName);
            this.panelControl1.Controls.Add(this.dateTimePicker2);
            this.panelControl1.Controls.Add(this.label5);
            this.panelControl1.Controls.Add(this.dateTimePicker1);
            this.panelControl1.Controls.Add(this.txtNumber);
            this.panelControl1.Controls.Add(this.label8);
            this.panelControl1.Controls.Add(this.label6);
            this.panelControl1.Controls.Add(this.label9);
            this.panelControl1.Controls.Add(this.txtAddress);
            this.panelControl1.Controls.Add(this.btnSecondPager);
            this.panelControl1.Controls.Add(this.btnAddNew);
            this.panelControl1.Controls.Add(this.label7);
            this.panelControl1.Controls.Add(this.btnSearch);
            this.panelControl1.Controls.Add(this.txtCompany);
            this.panelControl1.Controls.Add(this.cmbArea);
            this.panelControl1.Controls.Add(this.label4);
            this.panelControl1.Controls.Add(this.label3);
            this.panelControl1.Controls.Add(this.txtTelephone);
            this.panelControl1.Controls.Add(this.cmbType);
            this.panelControl1.Controls.Add(this.label10);
            this.panelControl1.Controls.Add(this.label2);
            this.panelControl1.Controls.Add(this.txtNote);
            this.panelControl1.Location = new System.Drawing.Point(12, 12);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(998, 117);
            this.panelControl1.TabIndex = 6;
            // 
            // btnGetCheckedRows
            // 
            this.btnGetCheckedRows.Location = new System.Drawing.Point(481, 76);
            this.btnGetCheckedRows.Name = "btnGetCheckedRows";
            this.btnGetCheckedRows.Size = new System.Drawing.Size(90, 26);
            this.btnGetCheckedRows.TabIndex = 15;
            this.btnGetCheckedRows.Text = "获取勾选数量";
            this.btnGetCheckedRows.Click += new System.EventHandler(this.btnGetCheckedRows_Click);
            // 
            // btnSecondPager
            // 
            this.btnSecondPager.Location = new System.Drawing.Point(729, 76);
            this.btnSecondPager.Name = "btnSecondPager";
            this.btnSecondPager.Size = new System.Drawing.Size(82, 27);
            this.btnSecondPager.TabIndex = 10;
            this.btnSecondPager.Text = "第二种分页";
            this.btnSecondPager.Click += new System.EventHandler(this.btnSecondPager_Click);
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barStaticItem1});
            this.barManager1.MaxItemId = 2;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1026, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 773);
            this.barDockControlBottom.Size = new System.Drawing.Size(1026, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 773);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1026, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 773);
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.Id = 1;
            this.barStaticItem1.Name = "barStaticItem1";
            this.barStaticItem1.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(28, 147);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(115, 14);
            this.label11.TabIndex = 0;
            this.label11.Text = "选中的复选框记录：";
            // 
            // lblCheckboxSelection
            // 
            this.lblCheckboxSelection.AutoSize = true;
            this.lblCheckboxSelection.Location = new System.Drawing.Point(139, 147);
            this.lblCheckboxSelection.Name = "lblCheckboxSelection";
            this.lblCheckboxSelection.Size = new System.Drawing.Size(19, 14);
            this.lblCheckboxSelection.TabIndex = 0;
            this.lblCheckboxSelection.Text = "无";
            // 
            // FrmCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1026, 773);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.winGridViewPager1);
            this.Controls.Add(this.lblCheckboxSelection);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FrmCustomer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "客户管理";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmCustomer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtNote.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTelephone.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompany.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.TextEdit txtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbArea;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbType;
        private DevExpress.XtraEditors.TextEdit txtTelephone;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.TextEdit txtAddress;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.TextEdit txtNumber;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.SimpleButton btnAddNew;
        private JCodes.Framework.CommonControl.WinGridViewPager winGridViewPager1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private DevExpress.XtraEditors.TextEdit txtCompany;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ToolStripMenuItem menu_NewOrder;
        private System.Windows.Forms.ToolStripMenuItem menu_BuyTicket;
        private System.Windows.Forms.CheckBox chkUseDate;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private DevExpress.XtraEditors.TextEdit txtNote;
        private System.Windows.Forms.Label label10;
        private DevExpress.XtraEditors.SimpleButton btnNoOrderCustomer;
        private System.Windows.Forms.ToolStripMenuItem menu_NoOrderCustomer;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.SimpleButton btnGetCheckedRows;
        private DevExpress.XtraEditors.SimpleButton btnSecondPager;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblCheckboxSelection;
    }
}