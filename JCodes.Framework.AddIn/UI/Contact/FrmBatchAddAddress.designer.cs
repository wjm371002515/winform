namespace JCodes.Framework.AddIn.UI.Contact
{
    partial class FrmBatchAddAddress
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
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBatchAddAddress));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.txtContent = new DevExpress.XtraEditors.MemoEdit();
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.lblMange = new DevExpress.XtraEditors.LabelControl();
            this.checkedListContact = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem21 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem22 = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtNote = new DevExpress.XtraEditors.MemoEdit();
            this.txtAddressType = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem16 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnCommonTip = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.picPrint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtContent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkedListContact)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem21)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem22)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNote.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddressType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(667, 627);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(766, 627);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(580, 627);
            // 
            // dataNavigator1
            // 
            this.dataNavigator1.Location = new System.Drawing.Point(12, 622);
            // 
            // picPrint
            // 
            this.picPrint.Location = new System.Drawing.Point(202, 624);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.layoutControl1.Appearance.ControlReadOnly.BackColor = System.Drawing.Color.SeaShell;
            this.layoutControl1.Appearance.ControlReadOnly.Options.UseBackColor = true;
            this.layoutControl1.Controls.Add(this.txtContent);
            this.layoutControl1.Controls.Add(this.layoutControl2);
            this.layoutControl1.Controls.Add(this.txtNote);
            this.layoutControl1.Controls.Add(this.txtAddressType);
            this.layoutControl1.Location = new System.Drawing.Point(12, 63);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(834, 543);
            this.layoutControl1.TabIndex = 6;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // txtContent
            // 
            this.txtContent.Location = new System.Drawing.Point(75, 41);
            this.txtContent.Name = "txtContent";
            this.txtContent.Size = new System.Drawing.Size(540, 446);
            this.txtContent.StyleController = this.layoutControl1;
            toolTipTitleItem1.Text = "通讯录格式";
            toolTipItem1.LeftIndent = 6;
            toolTipItem1.Text = "格式可以是：\r\n伍华聪(wuhuacong@163.com);其他(test@163.com);\r\n\r\n格式也可以是：\r\n广州爱奇迪软件<6966254@qq.c" +
    "om>;其他(12345678@qq.com);";
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.txtContent.SuperTip = superToolTip1;
            this.txtContent.TabIndex = 22;
            this.txtContent.UseOptimizedRendering = true;
            // 
            // layoutControl2
            // 
            this.layoutControl2.Controls.Add(this.lblMange);
            this.layoutControl2.Controls.Add(this.checkedListContact);
            this.layoutControl2.Location = new System.Drawing.Point(619, 12);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.Root = this.Root;
            this.layoutControl2.Size = new System.Drawing.Size(203, 519);
            this.layoutControl2.TabIndex = 21;
            this.layoutControl2.Text = "layoutControl2";
            // 
            // lblMange
            // 
            this.lblMange.Appearance.Image = ((System.Drawing.Image)(resources.GetObject("lblMange.Appearance.Image")));
            this.lblMange.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftTop;
            this.lblMange.Location = new System.Drawing.Point(179, 3);
            this.lblMange.Name = "lblMange";
            this.lblMange.Size = new System.Drawing.Size(21, 20);
            this.lblMange.StyleController = this.layoutControl2;
            this.lblMange.TabIndex = 63;
            this.lblMange.ToolTip = "管理客户的分组";
            this.lblMange.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.lblMange.Click += new System.EventHandler(this.lblMange_Click);
            // 
            // checkedListContact
            // 
            this.checkedListContact.CheckOnClick = true;
            this.checkedListContact.Location = new System.Drawing.Point(3, 27);
            this.checkedListContact.Name = "checkedListContact";
            this.checkedListContact.Size = new System.Drawing.Size(197, 489);
            this.checkedListContact.StyleController = this.layoutControl2;
            this.checkedListContact.TabIndex = 58;
            // 
            // Root
            // 
            this.Root.CustomizationFormText = "Root";
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem1,
            this.layoutControlItem21,
            this.layoutControlItem22});
            this.Root.Location = new System.Drawing.Point(0, 0);
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.Root.Size = new System.Drawing.Size(203, 519);
            this.Root.Text = "Root";
            this.Root.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.AppearanceItemCaption.BackColor = System.Drawing.Color.SkyBlue;
            this.emptySpaceItem1.AppearanceItemCaption.BackColor2 = System.Drawing.Color.White;
            this.emptySpaceItem1.AppearanceItemCaption.BorderColor = System.Drawing.Color.Blue;
            this.emptySpaceItem1.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.emptySpaceItem1.AppearanceItemCaption.Options.UseBackColor = true;
            this.emptySpaceItem1.AppearanceItemCaption.Options.UseBorderColor = true;
            this.emptySpaceItem1.AppearanceItemCaption.Options.UseFont = true;
            this.emptySpaceItem1.AppearanceItemCaption.Options.UseTextOptions = true;
            this.emptySpaceItem1.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.emptySpaceItem1.CustomizationFormText = "通讯录分组";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(176, 24);
            this.emptySpaceItem1.Text = "通讯录分组";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            this.emptySpaceItem1.TextVisible = true;
            // 
            // layoutControlItem21
            // 
            this.layoutControlItem21.Control = this.checkedListContact;
            this.layoutControlItem21.CustomizationFormText = "layoutControlItem21";
            this.layoutControlItem21.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem21.Name = "layoutControlItem21";
            this.layoutControlItem21.Size = new System.Drawing.Size(201, 493);
            this.layoutControlItem21.Text = "layoutControlItem21";
            this.layoutControlItem21.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem21.TextToControlDistance = 0;
            this.layoutControlItem21.TextVisible = false;
            // 
            // layoutControlItem22
            // 
            this.layoutControlItem22.Control = this.lblMange;
            this.layoutControlItem22.CustomizationFormText = "layoutControlItem22";
            this.layoutControlItem22.Location = new System.Drawing.Point(176, 0);
            this.layoutControlItem22.Name = "layoutControlItem22";
            this.layoutControlItem22.Size = new System.Drawing.Size(25, 24);
            this.layoutControlItem22.Text = "layoutControlItem22";
            this.layoutControlItem22.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem22.TextToControlDistance = 0;
            this.layoutControlItem22.TextVisible = false;
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(75, 491);
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(540, 40);
            this.txtNote.StyleController = this.layoutControl1;
            this.txtNote.TabIndex = 16;
            this.txtNote.UseOptimizedRendering = true;
            // 
            // txtAddressType
            // 
            this.txtAddressType.EditValue = "";
            this.txtAddressType.Location = new System.Drawing.Point(75, 12);
            this.txtAddressType.Name = "txtAddressType";
            this.txtAddressType.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
            this.txtAddressType.Properties.Appearance.Options.UseForeColor = true;
            this.txtAddressType.Properties.AutoHeight = false;
            this.txtAddressType.Properties.ReadOnly = true;
            this.txtAddressType.Size = new System.Drawing.Size(540, 25);
            this.txtAddressType.StyleController = this.layoutControl1;
            this.txtAddressType.TabIndex = 2;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem16,
            this.layoutControlItem1,
            this.layoutControlItem3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(834, 543);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.txtAddressType;
            this.layoutControlItem2.CustomizationFormText = "姓名";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(50, 25);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(607, 29);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "通讯录类型";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem16
            // 
            this.layoutControlItem16.Control = this.txtNote;
            this.layoutControlItem16.CustomizationFormText = "备注";
            this.layoutControlItem16.Location = new System.Drawing.Point(0, 479);
            this.layoutControlItem16.MaxSize = new System.Drawing.Size(0, 44);
            this.layoutControlItem16.MinSize = new System.Drawing.Size(65, 44);
            this.layoutControlItem16.Name = "layoutControlItem16";
            this.layoutControlItem16.Size = new System.Drawing.Size(607, 44);
            this.layoutControlItem16.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem16.Text = "备注";
            this.layoutControlItem16.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.layoutControl2;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(607, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(207, 523);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.txtContent;
            this.layoutControlItem3.CustomizationFormText = "通讯录内容";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 29);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(607, 450);
            this.layoutControlItem3.Text = "通讯录内容";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(60, 14);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.labelControl1.Location = new System.Drawing.Point(120, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(368, 42);
            this.labelControl1.TabIndex = 7;
            this.labelControl1.Text = "通讯录内容格式：姓名,性别,手机 [,邮箱,QQ,部门,职位]\r\n如：张三,男,13800000000\r\n      李莉,女,13900000000,test@" +
    "163.com,123456,市场部,部门经理";
            // 
            // btnCommonTip
            // 
            this.btnCommonTip.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnCommonTip.Appearance.ForeColor = System.Drawing.Color.Red;
            this.btnCommonTip.Appearance.Image = ((System.Drawing.Image)(resources.GetObject("btnCommonTip.Appearance.Image")));
            this.btnCommonTip.AutoEllipsis = true;
            this.btnCommonTip.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCommonTip.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnCommonTip.Location = new System.Drawing.Point(12, 12);
            this.btnCommonTip.Name = "btnCommonTip";
            this.btnCommonTip.Size = new System.Drawing.Size(102, 36);
            this.btnCommonTip.TabIndex = 8;
            this.btnCommonTip.Text = "通讯录格式";
            this.btnCommonTip.ToolTip = "常见邮箱账号配置信息";
            this.btnCommonTip.ToolTipTitle = "提示";
            // 
            // FrmBatchAddAddress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 662);
            this.Controls.Add(this.btnCommonTip);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FrmBatchAddAddress";
            this.Text = "通讯录联系人";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.btnAdd, 0);
            this.Controls.SetChildIndex(this.dataNavigator1, 0);
            this.Controls.SetChildIndex(this.picPrint, 0);
            this.Controls.SetChildIndex(this.labelControl1, 0);
            this.Controls.SetChildIndex(this.btnCommonTip, 0);
            ((System.ComponentModel.ISupportInitialize)(this.picPrint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtContent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checkedListContact)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem21)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem22)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNote.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddressType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
          private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
          private DevExpress.XtraLayout.LayoutControlItem layoutControlItem16;
         private DevExpress.XtraEditors.MemoEdit txtNote;
         private DevExpress.XtraLayout.LayoutControl layoutControl2;
         private DevExpress.XtraLayout.LayoutControlGroup Root;
         private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
         private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
         private DevExpress.XtraEditors.CheckedListBoxControl checkedListContact;
         private DevExpress.XtraLayout.LayoutControlItem layoutControlItem21;
         private DevExpress.XtraEditors.LabelControl lblMange;
         private DevExpress.XtraLayout.LayoutControlItem layoutControlItem22;
         private DevExpress.XtraEditors.MemoEdit txtContent;
         private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
         private DevExpress.XtraEditors.LabelControl labelControl1;
         private DevExpress.XtraEditors.LabelControl btnCommonTip;
         private DevExpress.XtraEditors.TextEdit txtAddressType;    
 
    }
}