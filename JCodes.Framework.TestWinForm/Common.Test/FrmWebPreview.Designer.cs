namespace TestCommons
{
    partial class FrmWebPreview
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.btnSnap1 = new System.Windows.Forms.Button();
            this.btnSnap2 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnPrintForm = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 33);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "网页地址";
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(108, 28);
            this.txtUrl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(697, 28);
            this.txtUrl.TabIndex = 1;
            this.txtUrl.Text = "http://www.jcodes.cn";
            // 
            // btnSnap1
            // 
            this.btnSnap1.Location = new System.Drawing.Point(816, 28);
            this.btnSnap1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSnap1.Name = "btnSnap1";
            this.btnSnap1.Size = new System.Drawing.Size(112, 34);
            this.btnSnap1.TabIndex = 2;
            this.btnSnap1.Text = "截图方式一";
            this.btnSnap1.UseVisualStyleBackColor = true;
            this.btnSnap1.Click += new System.EventHandler(this.btnSnap1_Click);
            // 
            // btnSnap2
            // 
            this.btnSnap2.Location = new System.Drawing.Point(952, 26);
            this.btnSnap2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSnap2.Name = "btnSnap2";
            this.btnSnap2.Size = new System.Drawing.Size(112, 34);
            this.btnSnap2.TabIndex = 3;
            this.btnSnap2.Text = "截图方式二";
            this.btnSnap2.UseVisualStyleBackColor = true;
            this.btnSnap2.Click += new System.EventHandler(this.btnSnap2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(22, 99);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1260, 804);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // btnPrintForm
            // 
            this.btnPrintForm.Location = new System.Drawing.Point(1096, 24);
            this.btnPrintForm.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnPrintForm.Name = "btnPrintForm";
            this.btnPrintForm.Size = new System.Drawing.Size(112, 34);
            this.btnPrintForm.TabIndex = 5;
            this.btnPrintForm.Text = "窗体打印";
            this.btnPrintForm.UseVisualStyleBackColor = true;
            this.btnPrintForm.Click += new System.EventHandler(this.btnPrintForm_Click);
            // 
            // FrmWebPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1296, 921);
            this.Controls.Add(this.btnPrintForm);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnSnap2);
            this.Controls.Add(this.btnSnap1);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FrmWebPreview";
            this.Text = "FrmWebPreview";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Button btnSnap1;
        private System.Windows.Forms.Button btnSnap2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnPrintForm;
    }
}