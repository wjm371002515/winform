namespace JCodes.Framework.Test
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.btnDeURL = new System.Windows.Forms.Button();
            this.btnEnURL = new System.Windows.Forms.Button();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.searchControl1 = new DevExpress.XtraEditors.SearchControl();
            this.memoExEdit1 = new DevExpress.XtraEditors.MemoExEdit();
            this.spinEdit1 = new DevExpress.XtraEditors.SpinEdit();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.maskedTextBox1 = new System.Windows.Forms.MaskedTextBox();
            this.colorPickEdit1 = new DevExpress.XtraEditors.ColorPickEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.dictControl1 = new JCodes.Framework.AddIn.UI.BizControl.DictControl();
            this.btnConfigSet = new DevExpress.XtraEditors.SimpleButton();
            this.btnMultiDatabase = new DevExpress.XtraEditors.SimpleButton();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoExEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorPickEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textEdit1);
            this.panel1.Controls.Add(this.txtURL);
            this.panel1.Controls.Add(this.btnDeURL);
            this.panel1.Controls.Add(this.btnEnURL);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1115, 57);
            this.panel1.TabIndex = 0;
            // 
            // textEdit1
            // 
            this.textEdit1.EditValue = 0;
            this.textEdit1.Location = new System.Drawing.Point(785, 12);
            this.textEdit1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Properties.Mask.EditMask = "[0-9]*";
            this.textEdit1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.textEdit1.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.textEdit1.Size = new System.Drawing.Size(133, 36);
            this.textEdit1.TabIndex = 2;
            // 
            // txtURL
            // 
            this.txtURL.Location = new System.Drawing.Point(17, 12);
            this.txtURL.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtURL.Name = "txtURL";
            this.txtURL.Size = new System.Drawing.Size(443, 35);
            this.txtURL.TabIndex = 1;
            // 
            // btnDeURL
            // 
            this.btnDeURL.Location = new System.Drawing.Point(643, 12);
            this.btnDeURL.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDeURL.Name = "btnDeURL";
            this.btnDeURL.Size = new System.Drawing.Size(133, 37);
            this.btnDeURL.TabIndex = 0;
            this.btnDeURL.Text = "URL解密";
            this.btnDeURL.UseVisualStyleBackColor = true;
            this.btnDeURL.Click += new System.EventHandler(this.btnDeURL_Click);
            // 
            // btnEnURL
            // 
            this.btnEnURL.Location = new System.Drawing.Point(487, 12);
            this.btnEnURL.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnEnURL.Name = "btnEnURL";
            this.btnEnURL.Size = new System.Drawing.Size(133, 37);
            this.btnEnURL.TabIndex = 0;
            this.btnEnURL.Text = "URL加密";
            this.btnEnURL.UseVisualStyleBackColor = true;
            this.btnEnURL.Click += new System.EventHandler(this.btnURL_Click);
            // 
            // rtbLog
            // 
            this.rtbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbLog.Location = new System.Drawing.Point(0, 57);
            this.rtbLog.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.Size = new System.Drawing.Size(1115, 656);
            this.rtbLog.TabIndex = 1;
            this.rtbLog.Text = "";
            // 
            // searchControl1
            // 
            this.searchControl1.Location = new System.Drawing.Point(17, 92);
            this.searchControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.searchControl1.Name = "searchControl1";
            this.searchControl1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton(),
            new DevExpress.XtraEditors.Repository.SearchButton()});
            this.searchControl1.Size = new System.Drawing.Size(133, 36);
            this.searchControl1.TabIndex = 2;
            // 
            // memoExEdit1
            // 
            this.memoExEdit1.Location = new System.Drawing.Point(17, 273);
            this.memoExEdit1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.memoExEdit1.Name = "memoExEdit1";
            this.memoExEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.memoExEdit1.Size = new System.Drawing.Size(157, 36);
            this.memoExEdit1.TabIndex = 4;
            // 
            // spinEdit1
            // 
            this.spinEdit1.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinEdit1.Location = new System.Drawing.Point(17, 137);
            this.spinEdit1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.spinEdit1.Name = "spinEdit1";
            this.spinEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinEdit1.Size = new System.Drawing.Size(133, 36);
            this.spinEdit1.TabIndex = 5;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(953, 108);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(132, 201);
            this.richTextBox1.TabIndex = 6;
            this.richTextBox1.Text = "";
            // 
            // maskedTextBox1
            // 
            this.maskedTextBox1.Location = new System.Drawing.Point(17, 183);
            this.maskedTextBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.maskedTextBox1.Name = "maskedTextBox1";
            this.maskedTextBox1.Size = new System.Drawing.Size(132, 35);
            this.maskedTextBox1.TabIndex = 7;
            // 
            // colorPickEdit1
            // 
            this.colorPickEdit1.EditValue = System.Drawing.Color.Empty;
            this.colorPickEdit1.Location = new System.Drawing.Point(17, 228);
            this.colorPickEdit1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.colorPickEdit1.Name = "colorPickEdit1";
            this.colorPickEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.colorPickEdit1.Size = new System.Drawing.Size(133, 36);
            this.colorPickEdit1.TabIndex = 9;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(17, 339);
            this.simpleButton1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(165, 45);
            this.simpleButton1.TabIndex = 10;
            this.simpleButton1.Text = "测试";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // dictControl1
            // 
            this.dictControl1.DicNo = 300005;
            this.dictControl1.EditValue = null;
            this.dictControl1.Location = new System.Drawing.Point(168, 520);
            this.dictControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dictControl1.Name = "dictControl1";
            this.dictControl1.Size = new System.Drawing.Size(261, 37);
            this.dictControl1.StyleController = null;
            this.dictControl1.TabIndex = 11;
            // 
            // btnConfigSet
            // 
            this.btnConfigSet.Location = new System.Drawing.Point(213, 339);
            this.btnConfigSet.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnConfigSet.Name = "btnConfigSet";
            this.btnConfigSet.Size = new System.Drawing.Size(216, 45);
            this.btnConfigSet.TabIndex = 10;
            this.btnConfigSet.Text = "Config设置值测试";
            this.btnConfigSet.Click += new System.EventHandler(this.btnConfigSet_Click);
            // 
            // btnMultiDatabase
            // 
            this.btnMultiDatabase.Location = new System.Drawing.Point(452, 339);
            this.btnMultiDatabase.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnMultiDatabase.Name = "btnMultiDatabase";
            this.btnMultiDatabase.Size = new System.Drawing.Size(216, 45);
            this.btnMultiDatabase.TabIndex = 10;
            this.btnMultiDatabase.Text = "多数据库支持";
            this.btnMultiDatabase.Click += new System.EventHandler(this.btnMultiDatabase_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(890, 437);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(133, 37);
            this.button1.TabIndex = 12;
            this.button1.Text = "抓包";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1115, 713);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dictControl1);
            this.Controls.Add(this.btnMultiDatabase);
            this.Controls.Add(this.btnConfigSet);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.colorPickEdit1);
            this.Controls.Add(this.maskedTextBox1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.spinEdit1);
            this.Controls.Add(this.memoExEdit1);
            this.Controls.Add(this.searchControl1);
            this.Controls.Add(this.rtbLog);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoExEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorPickEdit1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.Button btnEnURL;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.Button btnDeURL;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.SearchControl searchControl1;
        private DevExpress.XtraEditors.MemoExEdit memoExEdit1;
        private DevExpress.XtraEditors.SpinEdit spinEdit1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.MaskedTextBox maskedTextBox1;
        private DevExpress.XtraEditors.ColorPickEdit colorPickEdit1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private AddIn.UI.BizControl.DictControl dictControl1;
        private DevExpress.XtraEditors.SimpleButton btnConfigSet;
        private DevExpress.XtraEditors.SimpleButton btnMultiDatabase;
        private System.Windows.Forms.Button button1;
    }
}

