namespace TestCommons
{
    partial class FrmIconReader
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
            this.txtFilPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGetIcon = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtExtension = new System.Windows.Forms.TextBox();
            this.btnGetExtensionIcon = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnBrowseFile = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnLoadImage = new System.Windows.Forms.Button();
            this.btnPrintImage = new System.Windows.Forms.Button();
            this.btnPrintView = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // txtFilPath
            // 
            this.txtFilPath.Location = new System.Drawing.Point(131, 31);
            this.txtFilPath.Name = "txtFilPath";
            this.txtFilPath.Size = new System.Drawing.Size(232, 21);
            this.txtFilPath.TabIndex = 0;
            this.txtFilPath.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtFilPath_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "文件路径或者目录";
            // 
            // btnGetIcon
            // 
            this.btnGetIcon.Location = new System.Drawing.Point(412, 29);
            this.btnGetIcon.Name = "btnGetIcon";
            this.btnGetIcon.Size = new System.Drawing.Size(75, 23);
            this.btnGetIcon.TabIndex = 2;
            this.btnGetIcon.Text = "获取图标";
            this.btnGetIcon.UseVisualStyleBackColor = true;
            this.btnGetIcon.Click += new System.EventHandler(this.btnGetIcon_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(25, 107);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(236, 140);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // txtExtension
            // 
            this.txtExtension.Location = new System.Drawing.Point(131, 58);
            this.txtExtension.Name = "txtExtension";
            this.txtExtension.Size = new System.Drawing.Size(130, 21);
            this.txtExtension.TabIndex = 0;
            this.txtExtension.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtExtension_KeyUp);
            // 
            // btnGetExtensionIcon
            // 
            this.btnGetExtensionIcon.Location = new System.Drawing.Point(267, 56);
            this.btnGetExtensionIcon.Name = "btnGetExtensionIcon";
            this.btnGetExtensionIcon.Size = new System.Drawing.Size(117, 23);
            this.btnGetExtensionIcon.TabIndex = 2;
            this.btnGetExtensionIcon.Text = "获取扩展名图标";
            this.btnGetExtensionIcon.UseVisualStyleBackColor = true;
            this.btnGetExtensionIcon.Click += new System.EventHandler(this.btnGetExtensionIcon_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(72, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "后缀名";
            // 
            // btnBrowseFile
            // 
            this.btnBrowseFile.Location = new System.Drawing.Point(363, 29);
            this.btnBrowseFile.Name = "btnBrowseFile";
            this.btnBrowseFile.Size = new System.Drawing.Size(34, 23);
            this.btnBrowseFile.TabIndex = 4;
            this.btnBrowseFile.Text = "...";
            this.btnBrowseFile.UseVisualStyleBackColor = true;
            this.btnBrowseFile.Click += new System.EventHandler(this.btnBrowseFile_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(280, 107);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(236, 140);
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // btnLoadImage
            // 
            this.btnLoadImage.Location = new System.Drawing.Point(25, 263);
            this.btnLoadImage.Name = "btnLoadImage";
            this.btnLoadImage.Size = new System.Drawing.Size(75, 23);
            this.btnLoadImage.TabIndex = 5;
            this.btnLoadImage.Text = "加载图片";
            this.btnLoadImage.UseVisualStyleBackColor = true;
            this.btnLoadImage.Click += new System.EventHandler(this.btnLoadImage_Click);
            // 
            // btnPrintImage
            // 
            this.btnPrintImage.Location = new System.Drawing.Point(131, 263);
            this.btnPrintImage.Name = "btnPrintImage";
            this.btnPrintImage.Size = new System.Drawing.Size(75, 23);
            this.btnPrintImage.TabIndex = 6;
            this.btnPrintImage.Text = "打印图片";
            this.btnPrintImage.UseVisualStyleBackColor = true;
            this.btnPrintImage.Click += new System.EventHandler(this.btnPrintImage_Click);
            // 
            // btnPrintView
            // 
            this.btnPrintView.Location = new System.Drawing.Point(234, 263);
            this.btnPrintView.Name = "btnPrintView";
            this.btnPrintView.Size = new System.Drawing.Size(75, 23);
            this.btnPrintView.TabIndex = 7;
            this.btnPrintView.Text = "打印预览";
            this.btnPrintView.UseVisualStyleBackColor = true;
            this.btnPrintView.Click += new System.EventHandler(this.btnPrintView_Click);
            // 
            // FrmIconReader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 324);
            this.Controls.Add(this.btnPrintView);
            this.Controls.Add(this.btnPrintImage);
            this.Controls.Add(this.btnLoadImage);
            this.Controls.Add(this.btnBrowseFile);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnGetExtensionIcon);
            this.Controls.Add(this.btnGetIcon);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtExtension);
            this.Controls.Add(this.txtFilPath);
            this.Name = "FrmIconReader";
            this.Text = "图标操作演示";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFilPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGetIcon;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtExtension;
        private System.Windows.Forms.Button btnGetExtensionIcon;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnBrowseFile;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button btnLoadImage;
        private System.Windows.Forms.Button btnPrintImage;
        private System.Windows.Forms.Button btnPrintView;
    }
}