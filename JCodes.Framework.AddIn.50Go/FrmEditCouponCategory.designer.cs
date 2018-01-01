namespace JCodes.Framework.AddIn._50Go
{
    partial class FrmEditCouponCategory
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
            this.txtHandNo = new DevExpress.XtraEditors.TextEdit();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.txtEnabled = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblHandNo = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblName = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblCompany = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblEnabled = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtCompany = new JCodes.Framework.AddIn.Basic.BizControl.CompanyControl();
            ((System.ComponentModel.ISupportInitialize)(this.picPrint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtHandNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEnabled.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblHandNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCompany)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblEnabled)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(323, 154);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(422, 154);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(236, 154);
            // 
            // dataNavigator1
            // 
            this.dataNavigator1.Location = new System.Drawing.Point(12, 149);
            // 
            // picPrint
            // 
            this.picPrint.Location = new System.Drawing.Point(205, 154);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.layoutControl1.Controls.Add(this.txtHandNo);
            this.layoutControl1.Controls.Add(this.txtName);
            this.layoutControl1.Controls.Add(this.txtEnabled);
            this.layoutControl1.Controls.Add(this.txtCompany);
            this.layoutControl1.Location = new System.Drawing.Point(12, 8);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(949, 308, 641, 749);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(490, 143);
            this.layoutControl1.TabIndex = 6;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // txtCompany
            // 
            this.txtCompany.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCompany.Location = new System.Drawing.Point(154, 44);
            this.txtCompany.Name = "txtCompany";
            this.txtCompany.Size = new System.Drawing.Size(154, 22);
            this.txtCompany.TabIndex = 10;
            this.txtCompany.Value = "-1";
            // 
            // txtHandNo
            // 
            this.txtHandNo.Location = new System.Drawing.Point(111, 12);
            this.txtHandNo.Name = "txtHandNo";
            this.txtHandNo.Size = new System.Drawing.Size(119, 28);
            this.txtHandNo.StyleController = this.layoutControl1;
            this.txtHandNo.TabIndex = 1;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(111, 36);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(367, 28);
            this.txtName.StyleController = this.layoutControl1;
            this.txtName.TabIndex = 2;
            // 
            // txtEnabled
            // 
            this.txtEnabled.EditValue = "是";
            this.txtEnabled.Location = new System.Drawing.Point(111, 103);
            this.txtEnabled.Name = "txtEnabled";
            this.txtEnabled.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtEnabled.Properties.Items.AddRange(new object[] {
            "否",
            "是"});
            this.txtEnabled.Size = new System.Drawing.Size(367, 28);
            this.txtEnabled.StyleController = this.layoutControl1;
            this.txtEnabled.TabIndex = 16;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "Root";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblHandNo,
            this.lblName,
            this.lblCompany,
            this.lblEnabled});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(490, 143);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lblHandNo
            // 
            this.lblHandNo.Control = this.txtHandNo;
            this.lblHandNo.CustomizationFormText = "分类编码";
            this.lblHandNo.Location = new System.Drawing.Point(0, 0);
            this.lblHandNo.MaxSize = new System.Drawing.Size(222, 24);
            this.lblHandNo.MinSize = new System.Drawing.Size(105, 24);
            this.lblHandNo.Name = "lblHandNo";
            this.lblHandNo.Size = new System.Drawing.Size(470, 24);
            this.lblHandNo.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblHandNo.Text = "分类编码(*)";
            this.lblHandNo.TextSize = new System.Drawing.Size(96, 22);
            // 
            // lblName
            // 
            this.lblName.Control = this.txtName;
            this.lblName.CustomizationFormText = "分类名称";
            this.lblName.Location = new System.Drawing.Point(0, 24);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(470, 32);
            this.lblName.Text = "分类名称(*)";
            this.lblName.TextSize = new System.Drawing.Size(96, 22);
            // 
            // lblCompany
            // 
            this.lblCompany.CustomizationFormText = "操作公司";
            this.lblCompany.Location = new System.Drawing.Point(0, 56);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(470, 35);
            this.lblCompany.Text = "操作公司(*)";
            this.lblCompany.TextSize = new System.Drawing.Size(96, 22);
            // 
            // lblEnabled
            // 
            this.lblEnabled.Control = this.txtEnabled;
            this.lblEnabled.CustomizationFormText = "是否可用";
            this.lblEnabled.Location = new System.Drawing.Point(0, 91);
            this.lblEnabled.Name = "lblEnabled";
            this.lblEnabled.Size = new System.Drawing.Size(470, 32);
            this.lblEnabled.Text = "是否可用(*)";
            this.lblEnabled.TextSize = new System.Drawing.Size(96, 22);
            // 
            // FrmEditCouponCategory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 189);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FrmEditCouponCategory";
            this.Text = "优惠券分类";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.btnAdd, 0);
            this.Controls.SetChildIndex(this.dataNavigator1, 0);
            this.Controls.SetChildIndex(this.picPrint, 0);
            ((System.ComponentModel.ISupportInitialize)(this.picPrint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtHandNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEnabled.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblHandNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCompany)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblEnabled)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;

        private DevExpress.XtraEditors.TextEdit txtHandNo;
        private DevExpress.XtraEditors.TextEdit txtName;
        private JCodes.Framework.AddIn.Basic.BizControl.CompanyControl txtCompany;
        private DevExpress.XtraEditors.ComboBoxEdit txtEnabled;
  
        private DevExpress.XtraLayout.LayoutControlItem lblHandNo;
        private DevExpress.XtraLayout.LayoutControlItem lblName;
        private DevExpress.XtraLayout.LayoutControlItem lblCompany;
        private DevExpress.XtraLayout.LayoutControlItem lblEnabled; 
 
    }
}