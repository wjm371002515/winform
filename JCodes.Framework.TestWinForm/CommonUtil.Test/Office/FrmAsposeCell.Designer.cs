namespace TestControlUtil
{
    partial class FrmAsposeCell
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAsposeCell));
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnReplaceContent = new System.Windows.Forms.Button();
            this.btnBindExcel = new System.Windows.Forms.Button();
            this.btnGeneralToXLS = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(50, 39);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(168, 42);
            this.button1.TabIndex = 0;
            this.button1.Text = "测试生成报表";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(50, 112);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1002, 564);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // btnReplaceContent
            // 
            this.btnReplaceContent.Location = new System.Drawing.Point(538, 43);
            this.btnReplaceContent.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnReplaceContent.Name = "btnReplaceContent";
            this.btnReplaceContent.Size = new System.Drawing.Size(150, 38);
            this.btnReplaceContent.TabIndex = 4;
            this.btnReplaceContent.Text = "替换文档数据";
            this.btnReplaceContent.UseVisualStyleBackColor = true;
            this.btnReplaceContent.Click += new System.EventHandler(this.btnReplaceContent_Click);
            // 
            // btnBindExcel
            // 
            this.btnBindExcel.Location = new System.Drawing.Point(297, 41);
            this.btnBindExcel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnBindExcel.Name = "btnBindExcel";
            this.btnBindExcel.Size = new System.Drawing.Size(150, 38);
            this.btnBindExcel.TabIndex = 3;
            this.btnBindExcel.Text = "绑定文档数据";
            this.btnBindExcel.UseVisualStyleBackColor = true;
            this.btnBindExcel.Click += new System.EventHandler(this.btnBindExcel_Click);
            // 
            // btnGeneralToXLS
            // 
            this.btnGeneralToXLS.Location = new System.Drawing.Point(761, 43);
            this.btnGeneralToXLS.Margin = new System.Windows.Forms.Padding(4);
            this.btnGeneralToXLS.Name = "btnGeneralToXLS";
            this.btnGeneralToXLS.Size = new System.Drawing.Size(150, 38);
            this.btnGeneralToXLS.TabIndex = 4;
            this.btnGeneralToXLS.Text = "XLS生成HTML";
            this.btnGeneralToXLS.UseVisualStyleBackColor = true;
            this.btnGeneralToXLS.Click += new System.EventHandler(this.btnGeneralToXLS_Click);
            // 
            // FrmAsposeCell
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 694);
            this.Controls.Add(this.btnGeneralToXLS);
            this.Controls.Add(this.btnReplaceContent);
            this.Controls.Add(this.btnBindExcel);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FrmAsposeCell";
            this.Text = "FrmAsposeCell";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnReplaceContent;
        private System.Windows.Forms.Button btnBindExcel;
        private System.Windows.Forms.Button btnGeneralToXLS;
    }
}