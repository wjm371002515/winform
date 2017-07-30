namespace JCodes.Framework.AddIn.Contact
{
    partial class FrmEditAddressGroup
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
            this.txtSeq = new DevExpress.XtraEditors.TextEdit();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.txtEditor = new DevExpress.XtraEditors.TextEdit();
            this.txtEditTime = new DevExpress.XtraEditors.DateEdit();
            this.txtNote = new DevExpress.XtraEditors.MemoEdit();
            this.txtPID = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblName = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.picPrint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSeq.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEditor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEditTime.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEditTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNote.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(336, 243);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(435, 243);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(249, 243);
            // 
            // dataNavigator1
            // 
            this.dataNavigator1.Location = new System.Drawing.Point(12, 238);
            // 
            // picPrint
            // 
            this.picPrint.Location = new System.Drawing.Point(202, 240);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.layoutControl1.Controls.Add(this.txtSeq);
            this.layoutControl1.Controls.Add(this.txtName);
            this.layoutControl1.Controls.Add(this.txtEditor);
            this.layoutControl1.Controls.Add(this.txtEditTime);
            this.layoutControl1.Controls.Add(this.txtNote);
            this.layoutControl1.Controls.Add(this.txtPID);
            this.layoutControl1.Location = new System.Drawing.Point(12, 8);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(503, 214);
            this.layoutControl1.TabIndex = 6;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // txtSeq
            // 
            this.txtSeq.Location = new System.Drawing.Point(87, 12);
            this.txtSeq.Name = "txtSeq";
            this.txtSeq.Size = new System.Drawing.Size(126, 28);
            this.txtSeq.StyleController = this.layoutControl1;
            this.txtSeq.TabIndex = 2;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(87, 36);
            this.txtName.Name = "txtName";
            this.txtName.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtName.Properties.Appearance.Options.UseBackColor = true;
            this.txtName.Size = new System.Drawing.Size(404, 28);
            this.txtName.StyleController = this.layoutControl1;
            this.txtName.TabIndex = 3;
            // 
            // txtEditor
            // 
            this.txtEditor.Location = new System.Drawing.Point(87, 174);
            this.txtEditor.Name = "txtEditor";
            this.txtEditor.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.SeaShell;
            this.txtEditor.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.txtEditor.Properties.ReadOnly = true;
            this.txtEditor.Size = new System.Drawing.Size(163, 28);
            this.txtEditor.StyleController = this.layoutControl1;
            this.txtEditor.TabIndex = 5;
            // 
            // txtEditTime
            // 
            this.txtEditTime.EditValue = null;
            this.txtEditTime.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtEditTime.Location = new System.Drawing.Point(329, 174);
            this.txtEditTime.Name = "txtEditTime";
            this.txtEditTime.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.SeaShell;
            this.txtEditTime.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.txtEditTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtEditTime.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtEditTime.Properties.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm";
            this.txtEditTime.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtEditTime.Properties.EditFormat.FormatString = "yyyy-MM-dd HH:mm";
            this.txtEditTime.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtEditTime.Properties.Mask.EditMask = "yyyy-MM-dd HH:mm";
            this.txtEditTime.Properties.ReadOnly = true;
            this.txtEditTime.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.txtEditTime.Size = new System.Drawing.Size(162, 28);
            this.txtEditTime.StyleController = this.layoutControl1;
            this.txtEditTime.TabIndex = 6;
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(87, 68);
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(404, 102);
            this.txtNote.StyleController = this.layoutControl1;
            this.txtNote.TabIndex = 4;
            this.txtNote.UseOptimizedRendering = true;
            // 
            // txtPID
            // 
            this.txtPID.Location = new System.Drawing.Point(292, 12);
            this.txtPID.Name = "txtPID";
            this.txtPID.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtPID.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.txtPID.Size = new System.Drawing.Size(176, 28);
            this.txtPID.StyleController = this.layoutControl1;
            this.txtPID.TabIndex = 1;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.lblName,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(503, 214);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.txtSeq;
            this.layoutControlItem2.CustomizationFormText = "编号";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(205, 24);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(105, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(205, 24);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "排序序号";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(72, 22);
            // 
            // lblName
            // 
            this.lblName.Control = this.txtName;
            this.lblName.CustomizationFormText = "分组名称";
            this.lblName.Location = new System.Drawing.Point(0, 24);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(483, 32);
            this.lblName.Text = "分组名称";
            this.lblName.TextSize = new System.Drawing.Size(72, 22);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.txtNote;
            this.layoutControlItem4.CustomizationFormText = "备注";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 56);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(483, 106);
            this.layoutControlItem4.Text = "备注";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(72, 22);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.txtEditor;
            this.layoutControlItem5.CustomizationFormText = "编辑人";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 162);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(242, 32);
            this.layoutControlItem5.Text = "编辑人";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(72, 22);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.txtEditTime;
            this.layoutControlItem6.CustomizationFormText = "编辑时间";
            this.layoutControlItem6.Location = new System.Drawing.Point(242, 162);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(241, 32);
            this.layoutControlItem6.Text = "编辑时间";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(72, 22);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.txtPID;
            this.layoutControlItem1.CustomizationFormText = "上级ID";
            this.layoutControlItem1.Location = new System.Drawing.Point(205, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(255, 24);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(105, 24);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(278, 24);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "上级分组";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(72, 22);
            // 
            // FrmEditAddressGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 278);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FrmEditAddressGroup";
            this.Text = "通讯录组别";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.btnAdd, 0);
            this.Controls.SetChildIndex(this.dataNavigator1, 0);
            this.Controls.SetChildIndex(this.picPrint, 0);
            ((System.ComponentModel.ISupportInitialize)(this.picPrint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtSeq.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEditor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEditTime.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEditTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNote.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
          private DevExpress.XtraEditors.TextEdit txtSeq;
          private DevExpress.XtraEditors.TextEdit txtName;
          private DevExpress.XtraEditors.TextEdit txtEditor;
          private DevExpress.XtraEditors.DateEdit txtEditTime;
  
         private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;    
         private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;    
         private DevExpress.XtraLayout.LayoutControlItem lblName;    
         private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;    
         private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;    
         private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
         private DevExpress.XtraEditors.MemoEdit txtNote;
         private DevExpress.XtraEditors.ComboBoxEdit txtPID;    
 
    }
}