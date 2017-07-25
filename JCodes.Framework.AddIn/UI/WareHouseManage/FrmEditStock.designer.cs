namespace JCodes.Framework.AddIn.UI.WareHouseManage
{
    partial class FrmEditStock
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtStockQuantity = new DevExpress.XtraEditors.TextEdit();
            this.txtStockMoney = new DevExpress.XtraEditors.TextEdit();
            this.txtLowWarning = new DevExpress.XtraEditors.TextEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.txtHighWarning = new DevExpress.XtraEditors.TextEdit();
            this.label13 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtWareHouse = new System.Windows.Forms.TextBox();
            this.txtMapNo = new System.Windows.Forms.TextBox();
            this.txtSpecification = new System.Windows.Forms.TextBox();
            this.txtManufacturer = new System.Windows.Forms.TextBox();
            this.txtItemType = new System.Windows.Forms.TextBox();
            this.txtBigType = new System.Windows.Forms.TextBox();
            this.txtItemName = new System.Windows.Forms.TextBox();
            this.txtItemNo = new System.Windows.Forms.TextBox();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtStockQuantity);
            this.groupBox1.Controls.Add(this.txtStockMoney);
            this.groupBox1.Controls.Add(this.txtLowWarning);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtHighWarning);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtNote);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtWareHouse);
            this.groupBox1.Controls.Add(this.txtMapNo);
            this.groupBox1.Controls.Add(this.txtSpecification);
            this.groupBox1.Controls.Add(this.txtManufacturer);
            this.groupBox1.Controls.Add(this.txtItemType);
            this.groupBox1.Controls.Add(this.txtBigType);
            this.groupBox1.Controls.Add(this.txtItemName);
            this.groupBox1.Controls.Add(this.txtItemNo);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(568, 282);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "库存信息";
            // 
            // txtStockQuantity
            // 
            this.txtStockQuantity.Properties.Mask.EditMask = "[0-9]*";
            this.txtStockQuantity.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtStockQuantity.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtStockQuantity.Location = new System.Drawing.Point(101, 122);
            this.txtStockQuantity.Name = "txtStockQuantity";
            this.txtStockQuantity.Size = new System.Drawing.Size(163, 22);
            this.txtStockQuantity.TabIndex = 8;
            this.txtStockQuantity.Validated += new System.EventHandler(this.txtStockQuantity_Validated);
            // 
            // txtStockMoney
            // 
            this.txtStockMoney.BackColor = System.Drawing.Color.PeachPuff;
            this.txtStockMoney.Enabled = false;
            this.txtStockMoney.Properties.Mask.EditMask = "[0-9]*";
            this.txtStockMoney.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtStockMoney.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtStockMoney.Location = new System.Drawing.Point(371, 122);
            this.txtStockMoney.Name = "txtStockMoney";
            this.txtStockMoney.Size = new System.Drawing.Size(163, 22);
            this.txtStockMoney.TabIndex = 9;
            // 
            // txtLowWarning
            // 
            this.txtLowWarning.Properties.Mask.EditMask = "[0-9]*";
            this.txtLowWarning.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtLowWarning.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtLowWarning.Location = new System.Drawing.Point(101, 149);
            this.txtLowWarning.Name = "txtLowWarning";
            this.txtLowWarning.Size = new System.Drawing.Size(163, 22);
            this.txtLowWarning.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 154);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 14);
            this.label3.TabIndex = 40;
            this.label3.Text = "低储预警库存数";
            // 
            // txtHighWarning
            // 
            this.txtHighWarning.Properties.Mask.EditMask = "[0-9]*";
            this.txtHighWarning.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtHighWarning.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtHighWarning.Location = new System.Drawing.Point(371, 147);
            this.txtHighWarning.Name = "txtHighWarning";
            this.txtHighWarning.Size = new System.Drawing.Size(163, 22);
            this.txtHighWarning.TabIndex = 11;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(339, 98);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(31, 14);
            this.label13.TabIndex = 40;
            this.label13.Text = "库房";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(339, 75);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(31, 14);
            this.label8.TabIndex = 40;
            this.label8.Text = "图号";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(41, 75);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(55, 14);
            this.label12.TabIndex = 40;
            this.label12.Text = "规格型号";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(41, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 14);
            this.label4.TabIndex = 40;
            this.label4.Text = "供 货 商";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(315, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 14);
            this.label6.TabIndex = 40;
            this.label6.Text = "备件类别";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(41, 51);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(55, 14);
            this.label9.TabIndex = 40;
            this.label9.Text = "备件属类";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(315, 17);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(55, 14);
            this.label11.TabIndex = 40;
            this.label11.Text = "备件名称";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 14);
            this.label1.TabIndex = 40;
            this.label1.Text = "备件编号";
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(101, 187);
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtNote.Size = new System.Drawing.Size(433, 53);
            this.txtNote.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(65, 200);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 14);
            this.label7.TabIndex = 35;
            this.label7.Text = "备注";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(53, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 14);
            this.label2.TabIndex = 37;
            this.label2.Text = "库存量";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(279, 151);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(91, 14);
            this.label10.TabIndex = 37;
            this.label10.Text = "超储预警库存数";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(315, 125);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 14);
            this.label5.TabIndex = 37;
            this.label5.Text = "库存金额";
            // 
            // txtWareHouse
            // 
            this.txtWareHouse.BackColor = System.Drawing.Color.PeachPuff;
            this.txtWareHouse.Enabled = false;
            this.txtWareHouse.Location = new System.Drawing.Point(371, 95);
            this.txtWareHouse.Name = "txtWareHouse";
            this.txtWareHouse.Size = new System.Drawing.Size(163, 22);
            this.txtWareHouse.TabIndex = 7;
            // 
            // txtMapNo
            // 
            this.txtMapNo.BackColor = System.Drawing.Color.PeachPuff;
            this.txtMapNo.Enabled = false;
            this.txtMapNo.Location = new System.Drawing.Point(371, 72);
            this.txtMapNo.Name = "txtMapNo";
            this.txtMapNo.Size = new System.Drawing.Size(163, 22);
            this.txtMapNo.TabIndex = 5;
            // 
            // txtSpecification
            // 
            this.txtSpecification.BackColor = System.Drawing.Color.PeachPuff;
            this.txtSpecification.Enabled = false;
            this.txtSpecification.Location = new System.Drawing.Point(101, 72);
            this.txtSpecification.Name = "txtSpecification";
            this.txtSpecification.Size = new System.Drawing.Size(163, 22);
            this.txtSpecification.TabIndex = 4;
            // 
            // txtManufacturer
            // 
            this.txtManufacturer.BackColor = System.Drawing.Color.PeachPuff;
            this.txtManufacturer.Enabled = false;
            this.txtManufacturer.Location = new System.Drawing.Point(101, 99);
            this.txtManufacturer.Name = "txtManufacturer";
            this.txtManufacturer.Size = new System.Drawing.Size(163, 22);
            this.txtManufacturer.TabIndex = 6;
            // 
            // txtItemType
            // 
            this.txtItemType.BackColor = System.Drawing.Color.PeachPuff;
            this.txtItemType.Enabled = false;
            this.txtItemType.Location = new System.Drawing.Point(371, 45);
            this.txtItemType.Name = "txtItemType";
            this.txtItemType.Size = new System.Drawing.Size(163, 22);
            this.txtItemType.TabIndex = 3;
            // 
            // txtBigType
            // 
            this.txtBigType.BackColor = System.Drawing.Color.PeachPuff;
            this.txtBigType.Enabled = false;
            this.txtBigType.Location = new System.Drawing.Point(101, 47);
            this.txtBigType.Name = "txtBigType";
            this.txtBigType.Size = new System.Drawing.Size(163, 22);
            this.txtBigType.TabIndex = 2;
            // 
            // txtItemName
            // 
            this.txtItemName.BackColor = System.Drawing.Color.PeachPuff;
            this.txtItemName.Enabled = false;
            this.txtItemName.Location = new System.Drawing.Point(371, 14);
            this.txtItemName.Name = "txtItemName";
            this.txtItemName.Size = new System.Drawing.Size(163, 22);
            this.txtItemName.TabIndex = 1;
            // 
            // txtItemNo
            // 
            this.txtItemNo.BackColor = System.Drawing.Color.PeachPuff;
            this.txtItemNo.Enabled = false;
            this.txtItemNo.Location = new System.Drawing.Point(101, 20);
            this.txtItemNo.Name = "txtItemNo";
            this.txtItemNo.Size = new System.Drawing.Size(163, 22);
            this.txtItemNo.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(406, 330);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "保存";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(505, 330);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "关闭";
            // 
            // FrmEditStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 365);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmEditStock";
            this.Text = "调整库存数量";
            this.Load += new System.EventHandler(this.FrmEditProduct_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtItemNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label10;
        private DevExpress.XtraEditors.TextEdit txtHighWarning;
        private DevExpress.XtraEditors.TextEdit txtStockMoney;
        private DevExpress.XtraEditors.TextEdit txtStockQuantity;
        private DevExpress.XtraEditors.TextEdit txtLowWarning;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtItemType;
        private System.Windows.Forms.TextBox txtBigType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtManufacturer;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtSpecification;
        private System.Windows.Forms.TextBox txtItemName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtMapNo;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtWareHouse;
    }
}