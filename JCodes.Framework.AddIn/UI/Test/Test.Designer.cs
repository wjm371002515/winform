namespace JCodes.Framework.AddIn
{
    partial class Test
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
            this.sbCustomization = new DevExpress.XtraEditors.SimpleButton();
            this.sbClose = new DevExpress.XtraEditors.SimpleButton();
            this.sbLogin = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new DevExpress.Utils.Frames.ApplicationCaption8_1();
            this.teUserName = new DevExpress.XtraEditors.TextEdit();
            this.tePwd = new DevExpress.XtraEditors.TextEdit();
            this.comboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.teUserName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tePwd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // sbCustomization
            // 
            this.sbCustomization.Location = new System.Drawing.Point(117, 458);
            this.sbCustomization.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.sbCustomization.Name = "sbCustomization";
            this.sbCustomization.Size = new System.Drawing.Size(94, 29);
            this.sbCustomization.TabIndex = 16;
            this.sbCustomization.TabStop = false;
            this.sbCustomization.Text = "系统配置";
            this.sbCustomization.ToolTip = "系统配置";
            this.sbCustomization.ToolTipTitle = "Show";
            // 
            // sbClose
            // 
            this.sbClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.sbClose.Location = new System.Drawing.Point(714, 458);
            this.sbClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.sbClose.Name = "sbClose";
            this.sbClose.Size = new System.Drawing.Size(146, 29);
            this.sbClose.TabIndex = 15;
            this.sbClose.TabStop = false;
            this.sbClose.Text = "取    消";
            // 
            // sbLogin
            // 
            this.sbLogin.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.sbLogin.Location = new System.Drawing.Point(563, 458);
            this.sbLogin.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.sbLogin.Name = "sbLogin";
            this.sbLogin.Size = new System.Drawing.Size(141, 29);
            this.sbLogin.TabIndex = 14;
            this.sbLogin.TabStop = false;
            this.sbLogin.Text = "登    陆";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(117, 128);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(743, 90);
            this.label1.TabIndex = 13;
            this.label1.TabStop = false;
            this.label1.Text = "Enter your data";
            // 
            // teUserName
            // 
            this.teUserName.Location = new System.Drawing.Point(315, 255);
            this.teUserName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.teUserName.Name = "teUserName";
            this.teUserName.Size = new System.Drawing.Size(416, 28);
            this.teUserName.TabIndex = 17;
            // 
            // tePwd
            // 
            this.tePwd.Location = new System.Drawing.Point(315, 293);
            this.tePwd.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tePwd.Name = "tePwd";
            this.tePwd.Size = new System.Drawing.Size(416, 28);
            this.tePwd.TabIndex = 18;
            // 
            // comboBoxEdit1
            // 
            this.comboBoxEdit1.Location = new System.Drawing.Point(315, 331);
            this.comboBoxEdit1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBoxEdit1.Name = "comboBoxEdit1";
            this.comboBoxEdit1.Size = new System.Drawing.Size(416, 28);
            this.comboBoxEdit1.TabIndex = 19;
            // 
            // Test
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(977, 615);
            this.Controls.Add(this.sbCustomization);
            this.Controls.Add(this.sbClose);
            this.Controls.Add(this.sbLogin);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.teUserName);
            this.Controls.Add(this.tePwd);
            this.Controls.Add(this.comboBoxEdit1);
            this.Name = "Test";
            this.Text = "Test";
            ((System.ComponentModel.ISupportInitialize)(this.teUserName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tePwd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton sbCustomization;
        private DevExpress.XtraEditors.SimpleButton sbClose;
        private DevExpress.XtraEditors.SimpleButton sbLogin;
        private DevExpress.Utils.Frames.ApplicationCaption8_1 label1;
        private DevExpress.XtraEditors.TextEdit teUserName;
        private DevExpress.XtraEditors.TextEdit tePwd;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit1;
    }
}