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
            this.btnGeneralToHtml = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnBindDoc
            // 
            this.btnBindDoc.Location = new System.Drawing.Point(290, 80);
            this.btnBindDoc.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnBindDoc.Name = "btnBindDoc";
            this.btnBindDoc.Size = new System.Drawing.Size(150, 38);
            this.btnBindDoc.TabIndex = 2;
            this.btnBindDoc.Text = "绑定文档数据";
            this.btnBindDoc.UseVisualStyleBackColor = true;
            this.btnBindDoc.Click += new System.EventHandler(this.btnBindDoc_Click);
            // 
            // btnGenerateDoc
            // 
            this.btnGenerateDoc.Location = new System.Drawing.Point(57, 80);
            this.btnGenerateDoc.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnGenerateDoc.Name = "btnGenerateDoc";
            this.btnGenerateDoc.Size = new System.Drawing.Size(150, 38);
            this.btnGenerateDoc.TabIndex = 1;
            this.btnGenerateDoc.Text = "动态生成文档";
            this.btnGenerateDoc.UseVisualStyleBackColor = true;
            this.btnGenerateDoc.Click += new System.EventHandler(this.btnGenerateDoc_Click);
            // 
            // btnReplaceContent
            // 
            this.btnReplaceContent.Location = new System.Drawing.Point(525, 80);
            this.btnReplaceContent.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnReplaceContent.Name = "btnReplaceContent";
            this.btnReplaceContent.Size = new System.Drawing.Size(150, 38);
            this.btnReplaceContent.TabIndex = 2;
            this.btnReplaceContent.Text = "替换文档数据";
            this.btnReplaceContent.UseVisualStyleBackColor = true;
            this.btnReplaceContent.Click += new System.EventHandler(this.btnReplaceContent_Click);
            // 
            // btnGeneralToHtml
            // 
            this.btnGeneralToHtml.Location = new System.Drawing.Point(57, 167);
            this.btnGeneralToHtml.Margin = new System.Windows.Forms.Padding(4);
            this.btnGeneralToHtml.Name = "btnGeneralToHtml";
            this.btnGeneralToHtml.Size = new System.Drawing.Size(150, 38);
            this.btnGeneralToHtml.TabIndex = 1;
            this.btnGeneralToHtml.Text = "word生成HTML";
            this.btnGeneralToHtml.UseVisualStyleBackColor = true;
            this.btnGeneralToHtml.Click += new System.EventHandler(this.btnGeneralToHtml_Click);
            // 
            // FrmAsposeWords
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(915, 621);
            this.Controls.Add(this.btnReplaceContent);
            this.Controls.Add(this.btnBindDoc);
            this.Controls.Add(this.btnGeneralToHtml);
            this.Controls.Add(this.btnGenerateDoc);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FrmAsposeWords";
            this.Text = "FrmAsposeWords";
            this.Load += new System.EventHandler(this.FrmAsposeWords_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnBindDoc;
        private System.Windows.Forms.Button btnGenerateDoc;
        private System.Windows.Forms.Button btnReplaceContent;
        private System.Windows.Forms.Button btnGeneralToHtml;
    }
}