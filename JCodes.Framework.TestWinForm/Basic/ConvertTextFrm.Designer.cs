namespace JCodes.Framework.TestWinForm.Basic
{
    partial class ConvertTextFrm
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
            this.richTextSource = new JCodes.Framework.CommonControl.Controls.ExRichTextBox();
            this.richTextDirection = new JCodes.Framework.CommonControl.Controls.ExRichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // richTextSource
            // 
            this.richTextSource.HiglightColor = JCodes.Framework.jCodesenum.BaseEnum.RtfColor.White;
            this.richTextSource.Location = new System.Drawing.Point(2, 3);
            this.richTextSource.Name = "richTextSource";
            this.richTextSource.Size = new System.Drawing.Size(1043, 377);
            this.richTextSource.TabIndex = 0;
            this.richTextSource.Text = "";
            this.richTextSource.TextColor = JCodes.Framework.jCodesenum.BaseEnum.RtfColor.Black;
            // 
            // richTextDirection
            // 
            this.richTextDirection.HiglightColor = JCodes.Framework.jCodesenum.BaseEnum.RtfColor.White;
            this.richTextDirection.Location = new System.Drawing.Point(2, 444);
            this.richTextDirection.Name = "richTextDirection";
            this.richTextDirection.Size = new System.Drawing.Size(1043, 317);
            this.richTextDirection.TabIndex = 1;
            this.richTextDirection.Text = "";
            this.richTextDirection.TextColor = JCodes.Framework.jCodesenum.BaseEnum.RtfColor.Black;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 392);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(106, 40);
            this.button1.TabIndex = 2;
            this.button1.Text = "转换";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ConvertTextFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1047, 764);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.richTextDirection);
            this.Controls.Add(this.richTextSource);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ConvertTextFrm";
            this.Text = "基础数据";
            this.ResumeLayout(false);

        }

        #endregion

        private CommonControl.Controls.ExRichTextBox richTextSource;
        private CommonControl.Controls.ExRichTextBox richTextDirection;
        private System.Windows.Forms.Button button1;

    }
}