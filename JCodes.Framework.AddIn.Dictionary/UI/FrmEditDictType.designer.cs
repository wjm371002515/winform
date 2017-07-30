namespace JCodes.Framework.AddIn.Dictionary
{
    partial class FrmEditDictType
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
            this.chkTopItem = new System.Windows.Forms.CheckBox();
            this.lblSeq = new System.Windows.Forms.Label();
            this.lblParent = new System.Windows.Forms.Label();
            this.lblID = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.lblNote = new System.Windows.Forms.Label();
            this.txtParent = new System.Windows.Forms.TextBox();
            this.txtSeq = new System.Windows.Forms.TextBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.picPrint)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(273, 305);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(356, 305);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(193, 304);
            // 
            // dataNavigator1
            // 
            this.dataNavigator1.Location = new System.Drawing.Point(12, 305);
            // 
            // picPrint
            // 
            this.picPrint.Location = new System.Drawing.Point(202, 387);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.chkTopItem);
            this.groupBox1.Controls.Add(this.lblSeq);
            this.groupBox1.Controls.Add(this.lblParent);
            this.groupBox1.Controls.Add(this.lblID);
            this.groupBox1.Controls.Add(this.lblName);
            this.groupBox1.Controls.Add(this.txtNote);
            this.groupBox1.Controls.Add(this.lblNote);
            this.groupBox1.Controls.Add(this.txtParent);
            this.groupBox1.Controls.Add(this.txtSeq);
            this.groupBox1.Controls.Add(this.txtID);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Location = new System.Drawing.Point(24, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(395, 276);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "字典大类信息";
            // 
            // chkTopItem
            // 
            this.chkTopItem.Location = new System.Drawing.Point(250, 153);
            this.chkTopItem.Name = "chkTopItem";
            this.chkTopItem.Size = new System.Drawing.Size(110, 18);
            this.chkTopItem.TabIndex = 3;
            this.chkTopItem.Text = "作为顶级目录项";
            this.chkTopItem.UseVisualStyleBackColor = true;
            this.chkTopItem.CheckedChanged += new System.EventHandler(this.chkTopItem_CheckedChanged);
            // 
            // lblSeq
            // 
            this.lblSeq.AutoSize = true;
            this.lblSeq.Location = new System.Drawing.Point(22, 114);
            this.lblSeq.Name = "lblSeq";
            this.lblSeq.Size = new System.Drawing.Size(82, 22);
            this.lblSeq.TabIndex = 40;
            this.lblSeq.Text = "类型排序";
            // 
            // lblParent
            // 
            this.lblParent.AutoSize = true;
            this.lblParent.Location = new System.Drawing.Point(21, 152);
            this.lblParent.Name = "lblParent";
            this.lblParent.Size = new System.Drawing.Size(82, 22);
            this.lblParent.TabIndex = 40;
            this.lblParent.Text = "父类名称";
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(21, 41);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(65, 22);
            this.lblID.TabIndex = 40;
            this.lblID.Text = "类别ID";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(21, 78);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(82, 22);
            this.lblName.TabIndex = 40;
            this.lblName.Text = "类型名称";
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(81, 185);
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtNote.Size = new System.Drawing.Size(286, 70);
            this.txtNote.TabIndex = 3;
            // 
            // lblNote
            // 
            this.lblNote.AutoSize = true;
            this.lblNote.Location = new System.Drawing.Point(47, 188);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(46, 22);
            this.lblNote.TabIndex = 35;
            this.lblNote.Text = "备注";
            // 
            // txtParent
            // 
            this.txtParent.Location = new System.Drawing.Point(81, 149);
            this.txtParent.Name = "txtParent";
            this.txtParent.ReadOnly = true;
            this.txtParent.Size = new System.Drawing.Size(163, 29);
            this.txtParent.TabIndex = 2;
            // 
            // txtSeq
            // 
            this.txtSeq.Location = new System.Drawing.Point(81, 111);
            this.txtSeq.Name = "txtSeq";
            this.txtSeq.Size = new System.Drawing.Size(163, 29);
            this.txtSeq.TabIndex = 1;
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(81, 38);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(163, 29);
            this.txtID.TabIndex = 0;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(81, 75);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(163, 29);
            this.txtName.TabIndex = 0;
            // 
            // FrmEditDictType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(442, 351);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmEditDictType";
            this.Text = "字典大类信息";
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.btnAdd, 0);
            this.Controls.SetChildIndex(this.dataNavigator1, 0);
            this.Controls.SetChildIndex(this.picPrint, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            ((System.ComponentModel.ISupportInitialize)(this.picPrint)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkTopItem;
        private System.Windows.Forms.Label lblSeq;
        private System.Windows.Forms.Label lblParent;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.TextBox txtParent;
        private System.Windows.Forms.TextBox txtSeq;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.TextBox txtName;



    }
}