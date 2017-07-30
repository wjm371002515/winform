namespace JCodes.Framework.AddIn.Dictionary
{
    partial class FrmEditCity
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
            this.lblCity = new System.Windows.Forms.Label();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.txtProvince = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtZipCode = new System.Windows.Forms.TextBox();
            this.lblZipCode = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picPrint)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(315, 137);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(400, 137);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(237, 137);
            // 
            // dataNavigator1
            // 
            this.dataNavigator1.Location = new System.Drawing.Point(12, 132);
            // 
            // picPrint
            // 
            this.picPrint.Location = new System.Drawing.Point(209, 132);
            // 
            // lblCity
            // 
            this.lblCity.AutoSize = true;
            this.lblCity.Location = new System.Drawing.Point(25, 62);
            this.lblCity.Name = "lblCity";
            this.lblCity.Size = new System.Drawing.Size(82, 22);
            this.lblCity.TabIndex = 52;
            this.lblCity.Text = "城市名称";
            // 
            // txtCity
            // 
            this.txtCity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCity.Location = new System.Drawing.Point(84, 59);
            this.txtCity.Name = "txtCity";
            this.txtCity.Size = new System.Drawing.Size(391, 29);
            this.txtCity.TabIndex = 51;
            // 
            // txtProvince
            // 
            this.txtProvince.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProvince.Enabled = false;
            this.txtProvince.Location = new System.Drawing.Point(82, 31);
            this.txtProvince.Name = "txtProvince";
            this.txtProvince.ReadOnly = true;
            this.txtProvince.Size = new System.Drawing.Size(186, 29);
            this.txtProvince.TabIndex = 51;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 22);
            this.label1.TabIndex = 52;
            this.label1.Text = "所属省份";
            // 
            // txtZipCode
            // 
            this.txtZipCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtZipCode.Location = new System.Drawing.Point(84, 93);
            this.txtZipCode.Name = "txtZipCode";
            this.txtZipCode.Size = new System.Drawing.Size(391, 29);
            this.txtZipCode.TabIndex = 51;
            // 
            // lblZipCode
            // 
            this.lblZipCode.AutoSize = true;
            this.lblZipCode.Location = new System.Drawing.Point(25, 96);
            this.lblZipCode.Name = "lblZipCode";
            this.lblZipCode.Size = new System.Drawing.Size(82, 22);
            this.lblZipCode.TabIndex = 52;
            this.lblZipCode.Text = "城市编码";
            // 
            // FrmEditCity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 177);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblZipCode);
            this.Controls.Add(this.lblCity);
            this.Controls.Add(this.txtProvince);
            this.Controls.Add(this.txtZipCode);
            this.Controls.Add(this.txtCity);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmEditCity";
            this.Text = "城市信息";
            this.Controls.SetChildIndex(this.txtCity, 0);
            this.Controls.SetChildIndex(this.txtZipCode, 0);
            this.Controls.SetChildIndex(this.txtProvince, 0);
            this.Controls.SetChildIndex(this.lblCity, 0);
            this.Controls.SetChildIndex(this.lblZipCode, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.btnAdd, 0);
            this.Controls.SetChildIndex(this.dataNavigator1, 0);
            this.Controls.SetChildIndex(this.picPrint, 0);
            ((System.ComponentModel.ISupportInitialize)(this.picPrint)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCity;
        public System.Windows.Forms.TextBox txtCity;
        public System.Windows.Forms.TextBox txtProvince;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtZipCode;
        private System.Windows.Forms.Label lblZipCode;
    }
}