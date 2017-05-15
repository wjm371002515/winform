namespace TestControlUtil
{
    partial class FrmZipUtil
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
            this.btnZipFiles = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnZipFiles
            // 
            this.btnZipFiles.Location = new System.Drawing.Point(48, 47);
            this.btnZipFiles.Name = "btnZipFiles";
            this.btnZipFiles.Size = new System.Drawing.Size(162, 45);
            this.btnZipFiles.TabIndex = 0;
            this.btnZipFiles.Text = "压缩多个文件";
            this.btnZipFiles.UseVisualStyleBackColor = true;
            this.btnZipFiles.Click += new System.EventHandler(this.btnZipFiles_Click);
            // 
            // FrmZipUtil
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(756, 425);
            this.Controls.Add(this.btnZipFiles);
            this.Name = "FrmZipUtil";
            this.Text = "压缩解压缩文件";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnZipFiles;
    }
}