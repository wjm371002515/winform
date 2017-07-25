namespace JCodes.Framework.AddIn.UI.Security
{
    partial class FrmEditSystemType
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtOid = new DevExpress.XtraEditors.TextEdit();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.txtCustomID = new DevExpress.XtraEditors.TextEdit();
            this.txtAuthorize = new DevExpress.XtraEditors.TextEdit();
            this.txtNote = new DevExpress.XtraEditors.MemoEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblOid = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblName = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.picPrint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtOid.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAuthorize.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNote.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblOid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(328, 278);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(427, 278);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(241, 278);
            // 
            // dataNavigator1
            // 
            this.dataNavigator1.Location = new System.Drawing.Point(12, 273);
            // 
            // picPrint
            // 
            this.picPrint.Location = new System.Drawing.Point(205, 278);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.layoutControl1.Controls.Add(this.labelControl1);
            this.layoutControl1.Controls.Add(this.txtOid);
            this.layoutControl1.Controls.Add(this.txtName);
            this.layoutControl1.Controls.Add(this.txtCustomID);
            this.layoutControl1.Controls.Add(this.txtAuthorize);
            this.layoutControl1.Controls.Add(this.txtNote);
            this.layoutControl1.Location = new System.Drawing.Point(12, 8);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(495, 249);
            this.layoutControl1.TabIndex = 6;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl1.Location = new System.Drawing.Point(145, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(338, 22);
            this.labelControl1.StyleController = this.layoutControl1;
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "(注意：系统标识一旦建立，请勿随意修改)";
            // 
            // txtOid
            // 
            this.txtOid.Location = new System.Drawing.Point(111, 12);
            this.txtOid.Name = "txtOid";
            this.txtOid.Size = new System.Drawing.Size(30, 28);
            this.txtOid.StyleController = this.layoutControl1;
            this.txtOid.TabIndex = 1;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(111, 38);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(372, 28);
            this.txtName.StyleController = this.layoutControl1;
            this.txtName.TabIndex = 2;
            // 
            // txtCustomID
            // 
            this.txtCustomID.Location = new System.Drawing.Point(111, 70);
            this.txtCustomID.Name = "txtCustomID";
            this.txtCustomID.Size = new System.Drawing.Size(372, 28);
            this.txtCustomID.StyleController = this.layoutControl1;
            this.txtCustomID.TabIndex = 3;
            // 
            // txtAuthorize
            // 
            this.txtAuthorize.Location = new System.Drawing.Point(111, 102);
            this.txtAuthorize.Name = "txtAuthorize";
            this.txtAuthorize.Size = new System.Drawing.Size(372, 28);
            this.txtAuthorize.StyleController = this.layoutControl1;
            this.txtAuthorize.TabIndex = 4;
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(111, 134);
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(372, 103);
            this.txtNote.StyleController = this.layoutControl1;
            this.txtNote.TabIndex = 5;
            this.txtNote.UseOptimizedRendering = true;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblOid,
            this.lblName,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(495, 249);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lblOid
            // 
            this.lblOid.Control = this.txtOid;
            this.lblOid.CustomizationFormText = "系统标识";
            this.lblOid.Location = new System.Drawing.Point(0, 0);
            this.lblOid.MaxSize = new System.Drawing.Size(222, 24);
            this.lblOid.MinSize = new System.Drawing.Size(105, 24);
            this.lblOid.Name = "lblOid";
            this.lblOid.Size = new System.Drawing.Size(133, 26);
            this.lblOid.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblOid.Text = "系统标识(*)";
            this.lblOid.TextSize = new System.Drawing.Size(96, 22);
            // 
            // lblName
            // 
            this.lblName.Control = this.txtName;
            this.lblName.CustomizationFormText = "系统名称";
            this.lblName.Location = new System.Drawing.Point(0, 26);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(475, 32);
            this.lblName.Text = "系统名称(*)";
            this.lblName.TextSize = new System.Drawing.Size(96, 22);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.txtCustomID;
            this.layoutControlItem3.CustomizationFormText = "客户编码";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 58);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(475, 32);
            this.layoutControlItem3.Text = "客户编码";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(96, 22);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.txtAuthorize;
            this.layoutControlItem4.CustomizationFormText = "授权编码";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 90);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(475, 32);
            this.layoutControlItem4.Text = "授权编码";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(96, 22);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.txtNote;
            this.layoutControlItem5.CustomizationFormText = "备注";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 122);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(475, 107);
            this.layoutControlItem5.Text = "备注";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(96, 22);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.labelControl1;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(133, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(342, 26);
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // FrmEditSystemType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 313);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FrmEditSystemType";
            this.Text = "系统类型信息";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.btnAdd, 0);
            this.Controls.SetChildIndex(this.dataNavigator1, 0);
            this.Controls.SetChildIndex(this.picPrint, 0);
            ((System.ComponentModel.ISupportInitialize)(this.picPrint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtOid.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAuthorize.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNote.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblOid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;

        private DevExpress.XtraEditors.TextEdit txtOid;
          private DevExpress.XtraEditors.TextEdit txtName;
          private DevExpress.XtraEditors.TextEdit txtCustomID;
          private DevExpress.XtraEditors.TextEdit txtAuthorize;
  
        private DevExpress.XtraLayout.LayoutControlItem lblOid;    
         private DevExpress.XtraLayout.LayoutControlItem lblName;    
         private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;    
         private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;    
         private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
         private DevExpress.XtraEditors.LabelControl labelControl1;
         private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
         private DevExpress.XtraEditors.MemoEdit txtNote;    
 
    }
}