namespace TestCommons
{
    partial class FrmTestRTF
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.btnSetBold = new System.Windows.Forms.Button();
            this.btnSetItalic = new System.Windows.Forms.Button();
            this.btnConvertToHTML = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Location = new System.Drawing.Point(12, 46);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(645, 367);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // btnSetBold
            // 
            this.btnSetBold.Location = new System.Drawing.Point(12, 17);
            this.btnSetBold.Name = "btnSetBold";
            this.btnSetBold.Size = new System.Drawing.Size(75, 23);
            this.btnSetBold.TabIndex = 1;
            this.btnSetBold.Text = "设置粗体";
            this.btnSetBold.UseVisualStyleBackColor = true;
            this.btnSetBold.Click += new System.EventHandler(this.btnSetBold_Click);
            // 
            // btnSetItalic
            // 
            this.btnSetItalic.Location = new System.Drawing.Point(106, 17);
            this.btnSetItalic.Name = "btnSetItalic";
            this.btnSetItalic.Size = new System.Drawing.Size(75, 23);
            this.btnSetItalic.TabIndex = 1;
            this.btnSetItalic.Text = "设置斜体";
            this.btnSetItalic.UseVisualStyleBackColor = true;
            this.btnSetItalic.Click += new System.EventHandler(this.btnSetItalic_Click);
            // 
            // btnConvertToHTML
            // 
            this.btnConvertToHTML.Location = new System.Drawing.Point(200, 17);
            this.btnConvertToHTML.Name = "btnConvertToHTML";
            this.btnConvertToHTML.Size = new System.Drawing.Size(75, 23);
            this.btnConvertToHTML.TabIndex = 1;
            this.btnConvertToHTML.Text = "转换HTML";
            this.btnConvertToHTML.UseVisualStyleBackColor = true;
            this.btnConvertToHTML.Click += new System.EventHandler(this.btnConvertToHTML_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(368, 17);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 1;
            this.btnPrint.Text = "打印内容";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // FrmTestRTF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(669, 425);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnConvertToHTML);
            this.Controls.Add(this.btnSetItalic);
            this.Controls.Add(this.btnSetBold);
            this.Controls.Add(this.richTextBox1);
            this.Name = "FrmTestRTF";
            this.Text = "RTF格式测试";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button btnSetBold;
        private System.Windows.Forms.Button btnSetItalic;
        private System.Windows.Forms.Button btnConvertToHTML;
        private System.Windows.Forms.Button btnPrint;
    }
}