namespace JCodes.Framework.AddIn.Dictionary
{
    partial class FrmEditDistrict
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
            this.lblDistrict = new System.Windows.Forms.Label();
            this.txtDistrict = new System.Windows.Forms.TextBox();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picPrint)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(327, 111);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(409, 111);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(249, 111);
            // 
            // dataNavigator1
            // 
            this.dataNavigator1.Location = new System.Drawing.Point(11, 108);
            // 
            // picPrint
            // 
            this.picPrint.Location = new System.Drawing.Point(206, 111);
            // 
            // lblDistrict
            // 
            this.lblDistrict.AutoSize = true;
            this.lblDistrict.Location = new System.Drawing.Point(25, 62);
            this.lblDistrict.Name = "lblDistrict";
            this.lblDistrict.Size = new System.Drawing.Size(82, 22);
            this.lblDistrict.TabIndex = 52;
            this.lblDistrict.Text = "区县名称";
            // 
            // txtDistrict
            // 
            this.txtDistrict.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDistrict.Location = new System.Drawing.Point(84, 59);
            this.txtDistrict.Name = "txtDistrict";
            this.txtDistrict.Size = new System.Drawing.Size(391, 29);
            this.txtDistrict.TabIndex = 51;
            // 
            // txtCity
            // 
            this.txtCity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCity.Enabled = false;
            this.txtCity.Location = new System.Drawing.Point(82, 31);
            this.txtCity.Name = "txtCity";
            this.txtCity.ReadOnly = true;
            this.txtCity.Size = new System.Drawing.Size(186, 29);
            this.txtCity.TabIndex = 51;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 22);
            this.label1.TabIndex = 52;
            this.label1.Text = "所属城市";
            // 
            // FrmEditDistrict
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 150);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblDistrict);
            this.Controls.Add(this.txtCity);
            this.Controls.Add(this.txtDistrict);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmEditDistrict";
            this.Text = "区县信息";
            this.Load += new System.EventHandler(this.FrmEditCityDistrict_Load);
            this.Controls.SetChildIndex(this.txtDistrict, 0);
            this.Controls.SetChildIndex(this.txtCity, 0);
            this.Controls.SetChildIndex(this.lblDistrict, 0);
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

        private System.Windows.Forms.Label lblDistrict;
        public System.Windows.Forms.TextBox txtDistrict;
        public System.Windows.Forms.TextBox txtCity;
        private System.Windows.Forms.Label label1;
    }
}