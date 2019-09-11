namespace JCodes.Framework.AddIn.Proj
{
    partial class FrmEditBasicDataGroupName
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
            this.lblGroupName = new System.Windows.Forms.Label();
            this.txtGuid = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbbGroupName = new DevExpress.XtraEditors.ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.picPrint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbbGroupName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(315, 121);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(400, 121);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(234, 121);
            // 
            // dataNavigator1
            // 
            this.dataNavigator1.Location = new System.Drawing.Point(12, 116);
            // 
            // picPrint
            // 
            this.picPrint.Location = new System.Drawing.Point(209, 116);
            // 
            // lblGroupName
            // 
            this.lblGroupName.AutoSize = true;
            this.lblGroupName.Location = new System.Drawing.Point(25, 62);
            this.lblGroupName.Name = "lblGroupName";
            this.lblGroupName.Size = new System.Drawing.Size(82, 22);
            this.lblGroupName.TabIndex = 52;
            this.lblGroupName.Text = "分组名字";
            // 
            // txtGuid
            // 
            this.txtGuid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGuid.Enabled = false;
            this.txtGuid.Location = new System.Drawing.Point(82, 31);
            this.txtGuid.Name = "txtGuid";
            this.txtGuid.ReadOnly = true;
            this.txtGuid.Size = new System.Drawing.Size(368, 29);
            this.txtGuid.TabIndex = 51;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 22);
            this.label1.TabIndex = 52;
            this.label1.Text = "GUID";
            // 
            // cbbGroupName
            // 
            this.cbbGroupName.Location = new System.Drawing.Point(82, 60);
            this.cbbGroupName.Name = "cbbGroupName";
            this.cbbGroupName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbbGroupName.Size = new System.Drawing.Size(368, 28);
            this.cbbGroupName.TabIndex = 53;
            this.cbbGroupName.SelectedValueChanged += new System.EventHandler(this.cbbGroupName_SelectedValueChanged);
            // 
            // FrmEditBasicDataGroupName
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 161);
            this.Controls.Add(this.cbbGroupName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblGroupName);
            this.Controls.Add(this.txtGuid);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmEditBasicDataGroupName";
            this.Text = "基础数据分组";
            this.Controls.SetChildIndex(this.txtGuid, 0);
            this.Controls.SetChildIndex(this.lblGroupName, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.btnAdd, 0);
            this.Controls.SetChildIndex(this.dataNavigator1, 0);
            this.Controls.SetChildIndex(this.picPrint, 0);
            this.Controls.SetChildIndex(this.cbbGroupName, 0);
            ((System.ComponentModel.ISupportInitialize)(this.picPrint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbbGroupName.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblGroupName;
        public System.Windows.Forms.TextBox txtGuid;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.ComboBoxEdit cbbGroupName;
    }
}