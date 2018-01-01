namespace JCodes.Framework.AddIn.Proj
{
    partial class FrmEditDictData
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
            this.lblSeq = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblDictType = new System.Windows.Forms.Label();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.lblValue = new System.Windows.Forms.Label();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.lblNote = new System.Windows.Forms.Label();
            this.txtDictType = new System.Windows.Forms.TextBox();
            this.txtSeq = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.picPrint)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(267, 277);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(366, 277);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(180, 277);
            // 
            // dataNavigator1
            // 
            this.dataNavigator1.Location = new System.Drawing.Point(12, 277);
            // 
            // picPrint
            // 
            this.picPrint.Location = new System.Drawing.Point(26, 277);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblSeq);
            this.groupBox1.Controls.Add(this.lblName);
            this.groupBox1.Controls.Add(this.lblDictType);
            this.groupBox1.Controls.Add(this.txtValue);
            this.groupBox1.Controls.Add(this.lblValue);
            this.groupBox1.Controls.Add(this.txtNote);
            this.groupBox1.Controls.Add(this.lblNote);
            this.groupBox1.Controls.Add(this.txtDictType);
            this.groupBox1.Controls.Add(this.txtSeq);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Location = new System.Drawing.Point(16, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(421, 259);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "字典项目信息";
            // 
            // lblSeq
            // 
            this.lblSeq.AutoSize = true;
            this.lblSeq.Location = new System.Drawing.Point(26, 136);
            this.lblSeq.Name = "lblSeq";
            this.lblSeq.Size = new System.Drawing.Size(118, 22);
            this.lblSeq.TabIndex = 40;
            this.lblSeq.Text = "数据字典排序";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(26, 101);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(118, 22);
            this.lblName.TabIndex = 40;
            this.lblName.Text = "数据字典名称";
            // 
            // lblDictType
            // 
            this.lblDictType.AutoSize = true;
            this.lblDictType.Location = new System.Drawing.Point(62, 35);
            this.lblDictType.Name = "lblDictType";
            this.lblDictType.Size = new System.Drawing.Size(82, 22);
            this.lblDictType.TabIndex = 38;
            this.lblDictType.Text = "字典大类";
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(150, 67);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(163, 29);
            this.txtValue.TabIndex = 1;
            // 
            // lblValue
            // 
            this.lblValue.AutoSize = true;
            this.lblValue.Location = new System.Drawing.Point(44, 67);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(100, 22);
            this.lblValue.TabIndex = 39;
            this.lblValue.Text = "数据字典值";
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(151, 171);
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtNote.Size = new System.Drawing.Size(256, 70);
            this.txtNote.TabIndex = 4;
            // 
            // lblNote
            // 
            this.lblNote.AutoSize = true;
            this.lblNote.Location = new System.Drawing.Point(98, 174);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(46, 22);
            this.lblNote.TabIndex = 35;
            this.lblNote.Text = "备注";
            // 
            // txtDictType
            // 
            this.txtDictType.Enabled = false;
            this.txtDictType.Location = new System.Drawing.Point(150, 32);
            this.txtDictType.Name = "txtDictType";
            this.txtDictType.Size = new System.Drawing.Size(163, 29);
            this.txtDictType.TabIndex = 0;
            // 
            // txtSeq
            // 
            this.txtSeq.Location = new System.Drawing.Point(151, 136);
            this.txtSeq.Name = "txtSeq";
            this.txtSeq.Size = new System.Drawing.Size(163, 29);
            this.txtSeq.TabIndex = 3;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(151, 101);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(163, 29);
            this.txtName.TabIndex = 2;
            // 
            // FrmEditDictData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 311);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmEditDictData";
            this.Text = "字典项目";
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.btnAdd, 0);
            this.Controls.SetChildIndex(this.dataNavigator1, 0);
            this.Controls.SetChildIndex(this.picPrint, 0);
            ((System.ComponentModel.ISupportInitialize)(this.picPrint)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblDictType;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblSeq;
        public System.Windows.Forms.TextBox txtDictType;
        private System.Windows.Forms.TextBox txtSeq;
    }
}