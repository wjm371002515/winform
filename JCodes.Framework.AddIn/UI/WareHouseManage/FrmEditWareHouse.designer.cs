
namespace JCodes.Framework.AddIn.UI.WareHouseManage
{
    partial class FrmEditWareHouse
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
            this.lblName = new DevExpress.XtraEditors.LabelControl();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.label7 = new DevExpress.XtraEditors.LabelControl();
            this.txtNote = new DevExpress.XtraEditors.MemoEdit();
            this.label4 = new DevExpress.XtraEditors.LabelControl();
            this.txtPhone = new DevExpress.XtraEditors.TextEdit();
            this.lblManager = new DevExpress.XtraEditors.LabelControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtManager = new JCodes.Framework.AddIn.UI.BizControl.ManagerSelectControl();
            this.txtAddress = new DevExpress.XtraEditors.MemoEdit();
            this.label6 = new DevExpress.XtraEditors.LabelControl();
            this.label2 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.picPrint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNote.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPhone.Properties)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(407, 287);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(488, 287);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(326, 287);
            // 
            // dataNavigator1
            // 
            this.dataNavigator1.Location = new System.Drawing.Point(12, 283);
            // 
            // picPrint
            // 
            this.picPrint.Location = new System.Drawing.Point(200, 287);
            // 
            // lblName
            // 
            this.lblName.Location = new System.Drawing.Point(30, 25);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(72, 22);
            this.lblName.TabIndex = 15;
            this.lblName.Text = "库房名称";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(96, 22);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(150, 28);
            this.txtName.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(54, 182);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 22);
            this.label7.TabIndex = 16;
            this.label7.Text = "备注";
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(96, 179);
            this.txtNote.Name = "txtNote";
            this.txtNote.Properties.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtNote.Size = new System.Drawing.Size(439, 70);
            this.txtNote.TabIndex = 4;
            this.txtNote.UseOptimizedRendering = true;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(30, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 22);
            this.label4.TabIndex = 22;
            this.label4.Text = "联系电话";
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(96, 76);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(150, 28);
            this.txtPhone.TabIndex = 2;
            // 
            // lblManager
            // 
            this.lblManager.Location = new System.Drawing.Point(44, 52);
            this.lblManager.Name = "lblManager";
            this.lblManager.Size = new System.Drawing.Size(54, 22);
            this.lblManager.TabIndex = 20;
            this.lblManager.Text = "负责人";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtManager);
            this.groupBox1.Controls.Add(this.txtAddress);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblManager);
            this.groupBox1.Controls.Add(this.txtPhone);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtNote);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.lblName);
            this.groupBox1.Location = new System.Drawing.Point(12, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(551, 264);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "库房信息";
            // 
            // txtManager
            // 
            this.txtManager.Location = new System.Drawing.Point(96, 48);
            this.txtManager.Margin = new System.Windows.Forms.Padding(0);
            this.txtManager.MaximumSize = new System.Drawing.Size(150, 20);
            this.txtManager.MinimumSize = new System.Drawing.Size(0, 20);
            this.txtManager.Name = "txtManager";
            this.txtManager.Size = new System.Drawing.Size(150, 20);
            this.txtManager.TabIndex = 28;
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(96, 103);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Properties.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtAddress.Size = new System.Drawing.Size(438, 70);
            this.txtAddress.TabIndex = 3;
            this.txtAddress.UseOptimizedRendering = true;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(30, 121);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 22);
            this.label6.TabIndex = 27;
            this.label6.Text = "库房地址";
            // 
            // label2
            // 
            this.label2.Appearance.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(265, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(338, 22);
            this.label2.TabIndex = 20;
            this.label2.Text = "(注意：库房名称一旦建立，请勿随意修改)";
            // 
            // FrmEditWareHouse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 322);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmEditWareHouse";
            this.Text = "库房信息";
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.btnAdd, 0);
            this.Controls.SetChildIndex(this.dataNavigator1, 0);
            this.Controls.SetChildIndex(this.picPrint, 0);
            ((System.ComponentModel.ISupportInitialize)(this.picPrint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNote.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPhone.Properties)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblName;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.LabelControl label7;
        private DevExpress.XtraEditors.MemoEdit txtNote;
        private DevExpress.XtraEditors.LabelControl label4;
        private DevExpress.XtraEditors.TextEdit txtPhone;
        private DevExpress.XtraEditors.LabelControl lblManager;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.MemoEdit txtAddress;
        private DevExpress.XtraEditors.LabelControl label6;
        private DevExpress.XtraEditors.LabelControl label2;
        private JCodes.Framework.AddIn.UI.BizControl.ManagerSelectControl txtManager;

    }
}