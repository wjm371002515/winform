namespace TestCommons
{
    partial class FrmImageHelper
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnLoadBitmap = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnWaterMark = new System.Windows.Forms.Button();
            this.btnZoom = new System.Windows.Forms.Button();
            this.btnRotate = new System.Windows.Forms.Button();
            this.btnTextWatermark = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(21, 41);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(277, 300);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btnLoadBitmap
            // 
            this.btnLoadBitmap.Location = new System.Drawing.Point(21, 13);
            this.btnLoadBitmap.Name = "btnLoadBitmap";
            this.btnLoadBitmap.Size = new System.Drawing.Size(75, 23);
            this.btnLoadBitmap.TabIndex = 1;
            this.btnLoadBitmap.Text = "加载位图";
            this.btnLoadBitmap.UseVisualStyleBackColor = true;
            this.btnLoadBitmap.Click += new System.EventHandler(this.btnLoadBitmap_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.Location = new System.Drawing.Point(304, 41);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(318, 300);
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // btnWaterMark
            // 
            this.btnWaterMark.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnWaterMark.Location = new System.Drawing.Point(21, 357);
            this.btnWaterMark.Name = "btnWaterMark";
            this.btnWaterMark.Size = new System.Drawing.Size(101, 23);
            this.btnWaterMark.TabIndex = 2;
            this.btnWaterMark.Text = "图片水印效果";
            this.btnWaterMark.UseVisualStyleBackColor = true;
            this.btnWaterMark.Click += new System.EventHandler(this.btnWaterMark_Click);
            // 
            // btnZoom
            // 
            this.btnZoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnZoom.Location = new System.Drawing.Point(223, 357);
            this.btnZoom.Name = "btnZoom";
            this.btnZoom.Size = new System.Drawing.Size(75, 23);
            this.btnZoom.TabIndex = 2;
            this.btnZoom.Text = "放大效果";
            this.btnZoom.UseVisualStyleBackColor = true;
            this.btnZoom.Click += new System.EventHandler(this.btnZoom_Click);
            // 
            // btnRotate
            // 
            this.btnRotate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRotate.Location = new System.Drawing.Point(316, 357);
            this.btnRotate.Name = "btnRotate";
            this.btnRotate.Size = new System.Drawing.Size(75, 23);
            this.btnRotate.TabIndex = 3;
            this.btnRotate.Text = "旋转效果";
            this.btnRotate.UseVisualStyleBackColor = true;
            this.btnRotate.Click += new System.EventHandler(this.btnRotate_Click);
            // 
            // btnTextWatermark
            // 
            this.btnTextWatermark.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnTextWatermark.Location = new System.Drawing.Point(128, 357);
            this.btnTextWatermark.Name = "btnTextWatermark";
            this.btnTextWatermark.Size = new System.Drawing.Size(89, 23);
            this.btnTextWatermark.TabIndex = 2;
            this.btnTextWatermark.Text = "文字水印效果";
            this.btnTextWatermark.UseVisualStyleBackColor = true;
            this.btnTextWatermark.Click += new System.EventHandler(this.btnTextWatermark_Click);
            // 
            // FrmImageHelper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(654, 407);
            this.Controls.Add(this.btnRotate);
            this.Controls.Add(this.btnZoom);
            this.Controls.Add(this.btnTextWatermark);
            this.Controls.Add(this.btnWaterMark);
            this.Controls.Add(this.btnLoadBitmap);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Name = "FrmImageHelper";
            this.Text = "FrmResourceHelper";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnLoadBitmap;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button btnWaterMark;
        private System.Windows.Forms.Button btnZoom;
        private System.Windows.Forms.Button btnRotate;
        private System.Windows.Forms.Button btnTextWatermark;
    }
}