namespace TestControlUtil
{
    partial class FrmAsposeWords
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
            this.btnBindDoc = new System.Windows.Forms.Button();
            this.btnGenerateDoc = new System.Windows.Forms.Button();
            this.btnReplaceContent = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnBindDoc
            // 
            this.btnBindDoc.Location = new System.Drawing.Point(193, 53);
            this.btnBindDoc.Name = "btnBindDoc";
            this.btnBindDoc.Size = new System.Drawing.Size(100, 25);
            this.btnBindDoc.TabIndex = 2;
            this.btnBindDoc.Text = "绑定文档数据";
            this.btnBindDoc.UseVisualStyleBackColor = true;
            this.btnBindDoc.Click += new System.EventHandler(this.btnBindDoc_Click);
            // 
            // btnGenerateDoc
            // 
            this.btnGenerateDoc.Location = new System.Drawing.Point(38, 53);
            this.btnGenerateDoc.Name = "btnGenerateDoc";
            this.btnGenerateDoc.Size = new System.Drawing.Size(100, 25);
            this.btnGenerateDoc.TabIndex = 1;
            this.btnGenerateDoc.Text = "动态生成文档";
            this.btnGenerateDoc.UseVisualStyleBackColor = true;
            this.btnGenerateDoc.Click += new System.EventHandler(this.btnGenerateDoc_Click);
            // 
            // btnReplaceContent
            // 
            this.btnReplaceContent.Location = new System.Drawing.Point(350, 53);
            this.btnReplaceContent.Name = "btnReplaceContent";
            this.btnReplaceContent.Size = new System.Drawing.Size(100, 25);
            this.btnReplaceContent.TabIndex = 2;
            this.btnReplaceContent.Text = "替换文档数据";
            this.btnReplaceContent.UseVisualStyleBackColor = true;
            this.btnReplaceContent.Click += new System.EventHandler(this.btnReplaceContent_Click);
            // 
            // FrmAsposeWords
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 414);
            this.Controls.Add(this.btnReplaceContent);
            this.Controls.Add(this.btnBindDoc);
            this.Controls.Add(this.btnGenerateDoc);
            this.Name = "FrmAsposeWords";
            this.Text = "FrmAsposeWords";
            this.Load += new System.EventHandler(this.FrmAsposeWords_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnBindDoc;
        private System.Windows.Forms.Button btnGenerateDoc;
        private System.Windows.Forms.Button btnReplaceContent;
    }
}