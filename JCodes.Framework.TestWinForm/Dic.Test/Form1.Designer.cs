namespace TestDictionary
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new DevExpress.XtraEditors.SimpleButton();
            this.txtItemType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDistrict = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(124, 49);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(166, 33);
            this.button1.TabIndex = 0;
            this.button1.Text = "直接调用";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtItemType
            // 
            this.txtItemType.FormattingEnabled = true;
            this.txtItemType.Location = new System.Drawing.Point(124, 106);
            this.txtItemType.Name = "txtItemType";
            this.txtItemType.Size = new System.Drawing.Size(165, 22);
            this.txtItemType.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "项目列表";
            // 
            // btnDistrict
            // 
            this.btnDistrict.Location = new System.Drawing.Point(124, 161);
            this.btnDistrict.Name = "btnDistrict";
            this.btnDistrict.Size = new System.Drawing.Size(166, 33);
            this.btnDistrict.TabIndex = 0;
            this.btnDistrict.Text = "全国行政区划管理";
            this.btnDistrict.Click += new System.EventHandler(this.btnDistrict_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 271);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtItemType);
            this.Controls.Add(this.btnDistrict);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "字典模块调用测试程序";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton button1;
        private System.Windows.Forms.ComboBox txtItemType;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SimpleButton btnDistrict;
    }
}

