namespace TestControlUtil
{
    partial class FrmMyXls
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
            this.btnTest = new System.Windows.Forms.Button();
            this.btnToolGen = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(55, 35);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 0;
            this.btnTest.Text = "测试生成";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnToolGen
            // 
            this.btnToolGen.Location = new System.Drawing.Point(183, 35);
            this.btnToolGen.Name = "btnToolGen";
            this.btnToolGen.Size = new System.Drawing.Size(130, 23);
            this.btnToolGen.TabIndex = 1;
            this.btnToolGen.Text = "辅助类生成操作";
            this.btnToolGen.UseVisualStyleBackColor = true;
            this.btnToolGen.Click += new System.EventHandler(this.btnToolGen_Click);
            // 
            // FrmMyXls
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 338);
            this.Controls.Add(this.btnToolGen);
            this.Controls.Add(this.btnTest);
            this.Name = "FrmMyXls";
            this.Text = "FrmMyXls";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnToolGen;
    }
}