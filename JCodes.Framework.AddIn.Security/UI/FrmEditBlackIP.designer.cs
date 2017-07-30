namespace JCodes.Framework.AddIn.Security
{
    partial class FrmEditBlackIP
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.txtIPStart = new DevExpress.XtraEditors.TextEdit();
            this.txtIPEnd = new DevExpress.XtraEditors.TextEdit();
            this.txtCreator = new DevExpress.XtraEditors.TextEdit();
            this.txtCreateTime = new DevExpress.XtraEditors.DateEdit();
            this.txtAuthorizeType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtNote = new DevExpress.XtraEditors.MemoEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblName = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblIPStart = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblIPEnd = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblNote = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblAuthorizeType = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtForbid = new DevExpress.XtraEditors.CheckEdit();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.btnRemoveUser = new DevExpress.XtraEditors.SimpleButton();
            this.lvwUser = new System.Windows.Forms.ListBox();
            this.btnEditUser = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.picPrint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIPStart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIPEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreator.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreateTime.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreateTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAuthorizeType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNote.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblIPStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblIPEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNote)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAuthorizeType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtForbid.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(585, 478);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(684, 478);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(498, 478);
            // 
            // dataNavigator1
            // 
            this.dataNavigator1.Location = new System.Drawing.Point(12, 473);
            // 
            // picPrint
            // 
            this.picPrint.Location = new System.Drawing.Point(205, 478);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.layoutControl1.Appearance.ControlReadOnly.BackColor = System.Drawing.Color.SeaShell;
            this.layoutControl1.Appearance.ControlReadOnly.Options.UseBackColor = true;
            this.layoutControl1.Controls.Add(this.txtName);
            this.layoutControl1.Controls.Add(this.txtIPStart);
            this.layoutControl1.Controls.Add(this.txtIPEnd);
            this.layoutControl1.Controls.Add(this.txtCreator);
            this.layoutControl1.Controls.Add(this.txtCreateTime);
            this.layoutControl1.Controls.Add(this.txtAuthorizeType);
            this.layoutControl1.Controls.Add(this.txtNote);
            this.layoutControl1.Location = new System.Drawing.Point(12, 8);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(459, 419);
            this.layoutControl1.TabIndex = 6;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(128, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(319, 28);
            this.txtName.StyleController = this.layoutControl1;
            this.txtName.TabIndex = 1;
            // 
            // txtIPStart
            // 
            this.txtIPStart.Location = new System.Drawing.Point(128, 76);
            this.txtIPStart.Name = "txtIPStart";
            this.txtIPStart.Size = new System.Drawing.Size(319, 28);
            this.txtIPStart.StyleController = this.layoutControl1;
            this.txtIPStart.TabIndex = 6;
            // 
            // txtIPEnd
            // 
            this.txtIPEnd.Location = new System.Drawing.Point(128, 108);
            this.txtIPEnd.Name = "txtIPEnd";
            this.txtIPEnd.Size = new System.Drawing.Size(319, 28);
            this.txtIPEnd.StyleController = this.layoutControl1;
            this.txtIPEnd.TabIndex = 7;
            // 
            // txtCreator
            // 
            this.txtCreator.Location = new System.Drawing.Point(128, 379);
            this.txtCreator.Name = "txtCreator";
            this.txtCreator.Properties.ReadOnly = true;
            this.txtCreator.Size = new System.Drawing.Size(100, 28);
            this.txtCreator.StyleController = this.layoutControl1;
            this.txtCreator.TabIndex = 9;
            // 
            // txtCreateTime
            // 
            this.txtCreateTime.EditValue = null;
            this.txtCreateTime.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtCreateTime.Location = new System.Drawing.Point(348, 379);
            this.txtCreateTime.Name = "txtCreateTime";
            this.txtCreateTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtCreateTime.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtCreateTime.Properties.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm";
            this.txtCreateTime.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtCreateTime.Properties.EditFormat.FormatString = "yyyy-MM-dd HH:mm";
            this.txtCreateTime.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtCreateTime.Properties.Mask.EditMask = "yyyy-MM-dd HH:mm";
            this.txtCreateTime.Properties.ReadOnly = true;
            this.txtCreateTime.Size = new System.Drawing.Size(99, 28);
            this.txtCreateTime.StyleController = this.layoutControl1;
            this.txtCreateTime.TabIndex = 11;
            // 
            // txtAuthorizeType
            // 
            this.txtAuthorizeType.EditValue = "";
            this.txtAuthorizeType.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtAuthorizeType.Location = new System.Drawing.Point(128, 44);
            this.txtAuthorizeType.Name = "txtAuthorizeType";
            this.txtAuthorizeType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.DropDown)});
            this.txtAuthorizeType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.txtAuthorizeType.Size = new System.Drawing.Size(319, 28);
            this.txtAuthorizeType.StyleController = this.layoutControl1;
            this.txtAuthorizeType.TabIndex = 4;
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(128, 140);
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(319, 235);
            this.txtNote.StyleController = this.layoutControl1;
            this.txtNote.TabIndex = 8;
            this.txtNote.UseOptimizedRendering = true;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblName,
            this.lblIPStart,
            this.lblIPEnd,
            this.lblNote,
            this.layoutControlItem9,
            this.layoutControlItem11,
            this.lblAuthorizeType});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(459, 419);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lblName
            // 
            this.lblName.Control = this.txtName;
            this.lblName.CustomizationFormText = "显示名称";
            this.lblName.Location = new System.Drawing.Point(0, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(439, 32);
            this.lblName.Text = "显示名称(*)";
            this.lblName.TextSize = new System.Drawing.Size(113, 22);
            // 
            // lblIPStart
            // 
            this.lblIPStart.Control = this.txtIPStart;
            this.lblIPStart.CustomizationFormText = "IP起始地址";
            this.lblIPStart.Location = new System.Drawing.Point(0, 64);
            this.lblIPStart.Name = "lblIPStart";
            this.lblIPStart.Size = new System.Drawing.Size(439, 32);
            this.lblIPStart.Text = "IP起始地址(*)";
            this.lblIPStart.TextSize = new System.Drawing.Size(113, 22);
            // 
            // lblIPEnd
            // 
            this.lblIPEnd.Control = this.txtIPEnd;
            this.lblIPEnd.CustomizationFormText = "IP结束地址";
            this.lblIPEnd.Location = new System.Drawing.Point(0, 96);
            this.lblIPEnd.Name = "lblIPEnd";
            this.lblIPEnd.Size = new System.Drawing.Size(439, 32);
            this.lblIPEnd.Text = "IP结束地址(*)";
            this.lblIPEnd.TextSize = new System.Drawing.Size(113, 22);
            // 
            // lblNote
            // 
            this.lblNote.Control = this.txtNote;
            this.lblNote.CustomizationFormText = "备注";
            this.lblNote.Location = new System.Drawing.Point(0, 128);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(439, 239);
            this.lblNote.Text = "备注说明";
            this.lblNote.TextSize = new System.Drawing.Size(113, 22);
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.txtCreator;
            this.layoutControlItem9.CustomizationFormText = "创建人";
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 367);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(220, 32);
            this.layoutControlItem9.Text = "创建人";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(113, 22);
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.txtCreateTime;
            this.layoutControlItem11.CustomizationFormText = "创建时间";
            this.layoutControlItem11.Location = new System.Drawing.Point(220, 367);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(219, 32);
            this.layoutControlItem11.Text = "创建时间";
            this.layoutControlItem11.TextSize = new System.Drawing.Size(113, 22);
            // 
            // lblAuthorizeType
            // 
            this.lblAuthorizeType.Control = this.txtAuthorizeType;
            this.lblAuthorizeType.CustomizationFormText = "授权类型";
            this.lblAuthorizeType.Location = new System.Drawing.Point(0, 32);
            this.lblAuthorizeType.Name = "lblAuthorizeType";
            this.lblAuthorizeType.Size = new System.Drawing.Size(439, 32);
            this.lblAuthorizeType.Text = "授权类型(*)";
            this.lblAuthorizeType.TextSize = new System.Drawing.Size(113, 22);
            // 
            // txtForbid
            // 
            this.txtForbid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtForbid.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtForbid.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtForbid.Location = new System.Drawing.Point(12, 435);
            this.txtForbid.Name = "txtForbid";
            this.txtForbid.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.txtForbid.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
            this.txtForbid.Properties.Appearance.Options.UseFont = true;
            this.txtForbid.Properties.Appearance.Options.UseForeColor = true;
            this.txtForbid.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.txtForbid.Properties.Caption = "是否禁用";
            this.txtForbid.Size = new System.Drawing.Size(93, 26);
            this.txtForbid.TabIndex = 5;
            // 
            // groupControl3
            // 
            this.groupControl3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl3.Controls.Add(this.btnRemoveUser);
            this.groupControl3.Controls.Add(this.lvwUser);
            this.groupControl3.Controls.Add(this.btnEditUser);
            this.groupControl3.Location = new System.Drawing.Point(494, 8);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(265, 419);
            this.groupControl3.TabIndex = 7;
            this.groupControl3.Text = "包含用户";
            // 
            // btnRemoveUser
            // 
            this.btnRemoveUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveUser.Location = new System.Drawing.Point(172, 384);
            this.btnRemoveUser.Name = "btnRemoveUser";
            this.btnRemoveUser.Size = new System.Drawing.Size(75, 23);
            this.btnRemoveUser.TabIndex = 1;
            this.btnRemoveUser.Text = "移除用户";
            this.btnRemoveUser.Click += new System.EventHandler(this.btnRemoveUser_Click);
            // 
            // lvwUser
            // 
            this.lvwUser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwUser.FormattingEnabled = true;
            this.lvwUser.ItemHeight = 22;
            this.lvwUser.Location = new System.Drawing.Point(3, 25);
            this.lvwUser.Name = "lvwUser";
            this.lvwUser.Size = new System.Drawing.Size(259, 334);
            this.lvwUser.TabIndex = 3;
            // 
            // btnEditUser
            // 
            this.btnEditUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditUser.Location = new System.Drawing.Point(75, 384);
            this.btnEditUser.Name = "btnEditUser";
            this.btnEditUser.Size = new System.Drawing.Size(75, 23);
            this.btnEditUser.TabIndex = 0;
            this.btnEditUser.Text = "添加用户";
            this.btnEditUser.Click += new System.EventHandler(this.btnEditUser_Click);
            // 
            // FrmEditBlackIP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(771, 513);
            this.Controls.Add(this.groupControl3);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.txtForbid);
            this.Name = "FrmEditBlackIP";
            this.Text = "登陆系统的黑白名单列表";
            this.Load += new System.EventHandler(this.FrmEditBlackIP_Load);
            this.Controls.SetChildIndex(this.txtForbid, 0);
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.btnAdd, 0);
            this.Controls.SetChildIndex(this.dataNavigator1, 0);
            this.Controls.SetChildIndex(this.picPrint, 0);
            this.Controls.SetChildIndex(this.groupControl3, 0);
            ((System.ComponentModel.ISupportInitialize)(this.picPrint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIPStart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIPEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreator.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreateTime.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreateTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAuthorizeType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNote.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblIPStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblIPEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNote)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAuthorizeType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtForbid.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;

        private DevExpress.XtraEditors.TextEdit txtName;
          private DevExpress.XtraEditors.TextEdit txtIPStart;
          private DevExpress.XtraEditors.TextEdit txtIPEnd;
          private DevExpress.XtraEditors.TextEdit txtCreator;

          private DevExpress.XtraLayout.LayoutControlItem lblName;
         private DevExpress.XtraLayout.LayoutControlItem lblAuthorizeType;    
         private DevExpress.XtraLayout.LayoutControlItem lblIPStart;    
         private DevExpress.XtraLayout.LayoutControlItem lblIPEnd;    
         private DevExpress.XtraLayout.LayoutControlItem lblNote;
         private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
         private DevExpress.XtraEditors.ComboBoxEdit txtAuthorizeType;
         private DevExpress.XtraEditors.DateEdit txtCreateTime;
         private DevExpress.XtraEditors.MemoEdit txtNote;
         private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
         private DevExpress.XtraEditors.CheckEdit txtForbid;
         private DevExpress.XtraEditors.GroupControl groupControl3;
         private DevExpress.XtraEditors.SimpleButton btnRemoveUser;
         private System.Windows.Forms.ListBox lvwUser;
         private DevExpress.XtraEditors.SimpleButton btnEditUser;    
 
    }
}