namespace JCodes.Framework.AddIn.UI.Basic
{
    partial class FrmEditOperationLogSetting
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
            this.txtCreator = new DevExpress.XtraEditors.TextEdit();
            this.txtCreateTime = new DevExpress.XtraEditors.DateEdit();
            this.txtEditor = new DevExpress.XtraEditors.TextEdit();
            this.txtEditTime = new DevExpress.XtraEditors.DateEdit();
            this.txtInsertLog = new DevExpress.XtraEditors.CheckEdit();
            this.txtDeleteLog = new DevExpress.XtraEditors.CheckEdit();
            this.txtUpdateLog = new DevExpress.XtraEditors.CheckEdit();
            this.txtNote = new DevExpress.XtraEditors.MemoEdit();
            this.txtTableName = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblTableName = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtForbid = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.picPrint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreator.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreateTime.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreateTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEditor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEditTime.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEditTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInsertLog.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDeleteLog.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUpdateLog.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNote.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTableName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTableName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtForbid.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(438, 392);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(537, 392);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(351, 392);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.layoutControl1.Appearance.ControlReadOnly.BackColor = System.Drawing.Color.SeaShell;
            this.layoutControl1.Appearance.ControlReadOnly.Options.UseBackColor = true;
            this.layoutControl1.Controls.Add(this.txtCreator);
            this.layoutControl1.Controls.Add(this.txtCreateTime);
            this.layoutControl1.Controls.Add(this.txtEditor);
            this.layoutControl1.Controls.Add(this.txtEditTime);
            this.layoutControl1.Controls.Add(this.txtInsertLog);
            this.layoutControl1.Controls.Add(this.txtDeleteLog);
            this.layoutControl1.Controls.Add(this.txtUpdateLog);
            this.layoutControl1.Controls.Add(this.txtNote);
            this.layoutControl1.Controls.Add(this.txtTableName);
            this.layoutControl1.Location = new System.Drawing.Point(12, 8);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(605, 327);
            this.layoutControl1.TabIndex = 6;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // txtCreator
            // 
            this.txtCreator.Location = new System.Drawing.Point(87, 255);
            this.txtCreator.Name = "txtCreator";
            this.txtCreator.Properties.ReadOnly = true;
            this.txtCreator.Size = new System.Drawing.Size(213, 28);
            this.txtCreator.StyleController = this.layoutControl1;
            this.txtCreator.TabIndex = 7;
            // 
            // txtCreateTime
            // 
            this.txtCreateTime.EditValue = null;
            this.txtCreateTime.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtCreateTime.Location = new System.Drawing.Point(379, 255);
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
            this.txtCreateTime.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.txtCreateTime.Size = new System.Drawing.Size(214, 28);
            this.txtCreateTime.StyleController = this.layoutControl1;
            this.txtCreateTime.TabIndex = 9;
            // 
            // txtEditor
            // 
            this.txtEditor.Location = new System.Drawing.Point(87, 287);
            this.txtEditor.Name = "txtEditor";
            this.txtEditor.Properties.ReadOnly = true;
            this.txtEditor.Size = new System.Drawing.Size(213, 28);
            this.txtEditor.StyleController = this.layoutControl1;
            this.txtEditor.TabIndex = 10;
            // 
            // txtEditTime
            // 
            this.txtEditTime.EditValue = null;
            this.txtEditTime.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtEditTime.Location = new System.Drawing.Point(379, 287);
            this.txtEditTime.Name = "txtEditTime";
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
            this.txtEditTime.Size = new System.Drawing.Size(214, 28);
            this.txtEditTime.StyleController = this.layoutControl1;
            this.txtEditTime.TabIndex = 12;
            // 
            // txtInsertLog
            // 
            this.txtInsertLog.EditValue = null;
            this.txtInsertLog.Location = new System.Drawing.Point(12, 37);
            this.txtInsertLog.Name = "txtInsertLog";
            this.txtInsertLog.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.txtInsertLog.Properties.Caption = "记录插入日志";
            this.txtInsertLog.Size = new System.Drawing.Size(131, 26);
            this.txtInsertLog.StyleController = this.layoutControl1;
            this.txtInsertLog.TabIndex = 3;
            // 
            // txtDeleteLog
            // 
            this.txtDeleteLog.EditValue = null;
            this.txtDeleteLog.Location = new System.Drawing.Point(147, 37);
            this.txtDeleteLog.Name = "txtDeleteLog";
            this.txtDeleteLog.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.txtDeleteLog.Properties.Caption = "记录删除日志";
            this.txtDeleteLog.Size = new System.Drawing.Size(135, 26);
            this.txtDeleteLog.StyleController = this.layoutControl1;
            this.txtDeleteLog.TabIndex = 4;
            // 
            // txtUpdateLog
            // 
            this.txtUpdateLog.EditValue = null;
            this.txtUpdateLog.Location = new System.Drawing.Point(286, 37);
            this.txtUpdateLog.Name = "txtUpdateLog";
            this.txtUpdateLog.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.txtUpdateLog.Properties.Caption = "记录更新日志";
            this.txtUpdateLog.Size = new System.Drawing.Size(307, 26);
            this.txtUpdateLog.StyleController = this.layoutControl1;
            this.txtUpdateLog.TabIndex = 5;
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(87, 67);
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(506, 184);
            this.txtNote.StyleController = this.layoutControl1;
            this.txtNote.TabIndex = 6;
            this.txtNote.UseOptimizedRendering = true;
            // 
            // txtTableName
            // 
            this.txtTableName.Location = new System.Drawing.Point(87, 12);
            this.txtTableName.Name = "txtTableName";
            this.txtTableName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtTableName.Properties.DropDownRows = 12;
            this.txtTableName.Size = new System.Drawing.Size(171, 28);
            this.txtTableName.StyleController = this.layoutControl1;
            this.txtTableName.TabIndex = 2;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblTableName,
            this.layoutControlItem3,
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.layoutControlItem10,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem9,
            this.layoutControlItem12});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(605, 327);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lblTableName
            // 
            this.lblTableName.Control = this.txtTableName;
            this.lblTableName.CustomizationFormText = "数据库表";
            this.lblTableName.Location = new System.Drawing.Point(0, 0);
            this.lblTableName.MaxSize = new System.Drawing.Size(250, 0);
            this.lblTableName.MinSize = new System.Drawing.Size(50, 25);
            this.lblTableName.Name = "lblTableName";
            this.lblTableName.Size = new System.Drawing.Size(585, 25);
            this.lblTableName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblTableName.Text = "数据库表";
            this.lblTableName.TextSize = new System.Drawing.Size(72, 22);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.txtInsertLog;
            this.layoutControlItem3.CustomizationFormText = "记录插入日志";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 25);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(135, 30);
            this.layoutControlItem3.Text = "记录插入日志";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.txtNote;
            this.layoutControlItem6.CustomizationFormText = "备注";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 55);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(585, 188);
            this.layoutControlItem6.Text = "备注";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(72, 22);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.txtCreator;
            this.layoutControlItem7.CustomizationFormText = "创建人";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 243);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(292, 32);
            this.layoutControlItem7.Text = "创建人";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(72, 22);
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.txtEditor;
            this.layoutControlItem10.CustomizationFormText = "编辑人";
            this.layoutControlItem10.Location = new System.Drawing.Point(0, 275);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(292, 32);
            this.layoutControlItem10.Text = "编辑人";
            this.layoutControlItem10.TextSize = new System.Drawing.Size(72, 22);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.txtDeleteLog;
            this.layoutControlItem4.CustomizationFormText = "记录删除日志";
            this.layoutControlItem4.Location = new System.Drawing.Point(135, 25);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(139, 30);
            this.layoutControlItem4.Text = "记录删除日志";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.txtUpdateLog;
            this.layoutControlItem5.CustomizationFormText = "记录更新日志";
            this.layoutControlItem5.Location = new System.Drawing.Point(274, 25);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(311, 30);
            this.layoutControlItem5.Text = "记录更新日志";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.txtCreateTime;
            this.layoutControlItem9.CustomizationFormText = "创建时间";
            this.layoutControlItem9.Location = new System.Drawing.Point(292, 243);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(293, 32);
            this.layoutControlItem9.Text = "创建时间";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(72, 22);
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.Control = this.txtEditTime;
            this.layoutControlItem12.CustomizationFormText = "编辑时间";
            this.layoutControlItem12.Location = new System.Drawing.Point(292, 275);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(293, 32);
            this.layoutControlItem12.Text = "编辑时间";
            this.layoutControlItem12.TextSize = new System.Drawing.Size(72, 22);
            // 
            // txtForbid
            // 
            this.txtForbid.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtForbid.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtForbid.Location = new System.Drawing.Point(24, 341);
            this.txtForbid.Name = "txtForbid";
            this.txtForbid.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.txtForbid.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
            this.txtForbid.Properties.Appearance.Options.UseFont = true;
            this.txtForbid.Properties.Appearance.Options.UseForeColor = true;
            this.txtForbid.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.txtForbid.Properties.Caption = "是否禁用";
            this.txtForbid.Size = new System.Drawing.Size(89, 26);
            this.txtForbid.TabIndex = 1;
            // 
            // FrmEditOperationLogSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 427);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.txtForbid);
            this.Name = "FrmEditOperationLogSetting";
            this.Text = "记录操作日志的数据表配置";
            this.Controls.SetChildIndex(this.txtForbid, 0);
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.btnAdd, 0);
            this.Controls.SetChildIndex(this.dataNavigator1, 0);
            this.Controls.SetChildIndex(this.picPrint, 0);
            ((System.ComponentModel.ISupportInitialize)(this.picPrint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtCreator.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreateTime.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreateTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEditor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEditTime.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEditTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInsertLog.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDeleteLog.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUpdateLog.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNote.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTableName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTableName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtForbid.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.TextEdit txtCreator;
          private DevExpress.XtraEditors.DateEdit txtCreateTime;
          private DevExpress.XtraEditors.TextEdit txtEditor;
          private DevExpress.XtraEditors.DateEdit txtEditTime;    
         private DevExpress.XtraLayout.LayoutControlItem lblTableName;    
         private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;    
         private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;    
         private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;    
         private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
         private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;    
         private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
         private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;    
         private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
         private DevExpress.XtraEditors.CheckEdit txtForbid;
         private DevExpress.XtraEditors.CheckEdit txtInsertLog;
         private DevExpress.XtraEditors.CheckEdit txtDeleteLog;
         private DevExpress.XtraEditors.CheckEdit txtUpdateLog;
         private DevExpress.XtraEditors.MemoEdit txtNote;
         private DevExpress.XtraEditors.ComboBoxEdit txtTableName;    
 
    }
}