namespace JCodes.Framework.TestWinForm
{
    partial class FrmEditConsignment
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
            this.txtStrValue = new DevExpress.XtraEditors.TextEdit();
            this.txtSysValue = new DevExpress.XtraEditors.TextEdit();
            this.ccbEnableStatus = new JCodes.Framework.AddIn.UI.BizControl.DictControl();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblStrValue = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblSysValue = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblName = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblEnabled = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.picPrint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtStrValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSysValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblStrValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblSysValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblEnabled)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(164, 127);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(263, 127);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(77, 127);
            // 
            // dataNavigator1
            // 
            this.dataNavigator1.Location = new System.Drawing.Point(12, 122);
            // 
            // picPrint
            // 
            this.picPrint.Enabled = false;
            this.picPrint.Location = new System.Drawing.Point(205, 127);
            this.picPrint.Visible = false;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.layoutControl1.Controls.Add(this.txtStrValue);
            this.layoutControl1.Controls.Add(this.txtSysValue);
            this.layoutControl1.Controls.Add(this.ccbEnableStatus);
            this.layoutControl1.Controls.Add(this.txtName);
            this.layoutControl1.Location = new System.Drawing.Point(12, 8);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(949, 308, 641, 749);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(331, 116);
            this.layoutControl1.TabIndex = 6;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // txtStrValue
            // 
            this.txtStrValue.Location = new System.Drawing.Point(111, 12);
            this.txtStrValue.Name = "txtStrValue";
            this.txtStrValue.Size = new System.Drawing.Size(182, 28);
            this.txtStrValue.StyleController = this.layoutControl1;
            this.txtStrValue.TabIndex = 1;
            // 
            // txtSysValue
            // 
            this.txtSysValue.Location = new System.Drawing.Point(111, 44);
            this.txtSysValue.Name = "txtSysValue";
            this.txtSysValue.Size = new System.Drawing.Size(182, 28);
            this.txtSysValue.StyleController = this.layoutControl1;
            this.txtSysValue.TabIndex = 2;
            // 
            // ccbEnableStatus
            // 
            this.ccbEnableStatus.DicNo = 300005;
            this.ccbEnableStatus.EditValue = null;
            this.ccbEnableStatus.Location = new System.Drawing.Point(111, 108);
            this.ccbEnableStatus.Name = "ccbEnableStatus";
            this.ccbEnableStatus.Size = new System.Drawing.Size(182, 22);
            this.ccbEnableStatus.TabIndex = 16;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(111, 76);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(182, 28);
            this.txtName.StyleController = this.layoutControl1;
            this.txtName.TabIndex = 3;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "Root";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblStrValue,
            this.lblSysValue,
            this.lblName,
            this.lblEnabled});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(305, 142);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lblStrValue
            // 
            this.lblStrValue.Control = this.txtStrValue;
            this.lblStrValue.CustomizationFormText = "小账号";
            this.lblStrValue.Location = new System.Drawing.Point(0, 0);
            this.lblStrValue.Name = "lblStrValue";
            this.lblStrValue.Size = new System.Drawing.Size(285, 32);
            this.lblStrValue.Text = "小账号(*)";
            this.lblStrValue.TextSize = new System.Drawing.Size(96, 22);
            // 
            // lblSysValue
            // 
            this.lblSysValue.Control = this.txtSysValue;
            this.lblSysValue.CustomizationFormText = "代销商号";
            this.lblSysValue.Location = new System.Drawing.Point(0, 32);
            this.lblSysValue.Name = "lblSysValue";
            this.lblSysValue.Size = new System.Drawing.Size(285, 32);
            this.lblSysValue.Text = "代销商号(*)";
            this.lblSysValue.TextSize = new System.Drawing.Size(96, 22);
            // 
            // lblName
            // 
            this.lblName.Control = this.txtName;
            this.lblName.CustomizationFormText = "代销名字";
            this.lblName.Location = new System.Drawing.Point(0, 64);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(285, 32);
            this.lblName.Text = "代销名字(*)";
            this.lblName.TextSize = new System.Drawing.Size(96, 22);
            // 
            // lblEnabled
            // 
            this.lblEnabled.Control = this.ccbEnableStatus;
            this.lblEnabled.CustomizationFormText = "代销状态";
            this.lblEnabled.Location = new System.Drawing.Point(0, 96);
            this.lblEnabled.Name = "lblEnabled";
            this.lblEnabled.Size = new System.Drawing.Size(285, 26);
            this.lblEnabled.Text = "代销状态(*)";
            this.lblEnabled.TextSize = new System.Drawing.Size(96, 22);
            // 
            // FrmEditConsignment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 162);
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmEditConsignment";
            this.Text = "编辑代销数据";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.btnAdd, 0);
            this.Controls.SetChildIndex(this.dataNavigator1, 0);
            this.Controls.SetChildIndex(this.picPrint, 0);
            ((System.ComponentModel.ISupportInitialize)(this.picPrint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtStrValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSysValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblStrValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblSysValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblEnabled)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;

        private DevExpress.XtraEditors.TextEdit txtStrValue;
        private DevExpress.XtraEditors.TextEdit txtSysValue;
        private DevExpress.XtraEditors.TextEdit txtName;
        private JCodes.Framework.AddIn.UI.BizControl.DictControl ccbEnableStatus;
  
        private DevExpress.XtraLayout.LayoutControlItem lblStrValue;
        private DevExpress.XtraLayout.LayoutControlItem lblSysValue;
        private DevExpress.XtraLayout.LayoutControlItem lblName;
        private DevExpress.XtraLayout.LayoutControlItem lblEnabled; 
 
    }
}