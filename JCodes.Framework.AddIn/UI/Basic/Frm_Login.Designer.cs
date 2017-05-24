namespace JCodes.Framework.AddIn.UI.Basic
{
    partial class Frm_Login
    {
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing) {
            if(disposing) {
                if(components != null) {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Login));
            this.lcLogin = new DevExpress.XtraLayout.LayoutControl();
            this.sbCustomization = new DevExpress.XtraEditors.SimpleButton();
            this.sbClose = new DevExpress.XtraEditors.SimpleButton();
            this.sbLogin = new DevExpress.XtraEditors.SimpleButton();
            this.tePwd = new DevExpress.XtraEditors.TextEdit();
            this.teUserName = new DevExpress.XtraEditors.TextEdit();
            this.acBanner = new DevExpress.Utils.Frames.ApplicationCaption8_1();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciUserName = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciPwd = new DevExpress.XtraLayout.LayoutControlItem();
            this.esiLeft = new DevExpress.XtraLayout.EmptySpaceItem();
            this.esiRight = new DevExpress.XtraLayout.EmptySpaceItem();
            this.esiBottom = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.esiBottom2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.esiTop = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.lcLogin)).BeginInit();
            this.lcLogin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tePwd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teUserName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciUserName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciPwd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.esiLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.esiRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.esiBottom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.esiBottom2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.esiTop)).BeginInit();
            this.SuspendLayout();
            // 
            // lcLogin
            // 
            this.lcLogin.Controls.Add(this.sbCustomization);
            this.lcLogin.Controls.Add(this.sbClose);
            this.lcLogin.Controls.Add(this.sbLogin);
            this.lcLogin.Controls.Add(this.tePwd);
            this.lcLogin.Controls.Add(this.teUserName);
            this.lcLogin.Controls.Add(this.acBanner);
            this.lcLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lcLogin.Location = new System.Drawing.Point(0, 0);
            this.lcLogin.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lcLogin.Name = "lcLogin";
            this.lcLogin.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1027, 191, 711, 350);
            this.lcLogin.OptionsView.AllowItemSkinning = false;
            this.lcLogin.Root = this.layoutControlGroup1;
            this.lcLogin.Size = new System.Drawing.Size(656, 347);
            this.lcLogin.TabIndex = 0;
            this.lcLogin.Text = "layoutControl1";
            // 
            // sbCustomization
            // 
            this.sbCustomization.Location = new System.Drawing.Point(5, 313);
            this.sbCustomization.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.sbCustomization.Name = "sbCustomization";
            this.sbCustomization.Size = new System.Drawing.Size(99, 29);
            this.sbCustomization.StyleController = this.lcLogin;
            this.sbCustomization.TabIndex = 9;
            this.sbCustomization.TabStop = false;
            this.sbCustomization.Text = "系统配置";
            this.sbCustomization.ToolTip = "系统配置";
            this.sbCustomization.ToolTipTitle = "Show";
            this.sbCustomization.Click += new System.EventHandler(this.sbCustomization_Click);
            // 
            // sbClose
            // 
            this.sbClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.sbClose.Location = new System.Drawing.Point(529, 313);
            this.sbClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.sbClose.Name = "sbClose";
            this.sbClose.Size = new System.Drawing.Size(122, 29);
            this.sbClose.StyleController = this.lcLogin;
            this.sbClose.TabIndex = 8;
            this.sbClose.TabStop = false;
            this.sbClose.Text = "取    消";
            this.sbClose.Click += new System.EventHandler(this.sbClose_Click);
            // 
            // sbLogin
            // 
            this.sbLogin.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.sbLogin.Location = new System.Drawing.Point(402, 313);
            this.sbLogin.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.sbLogin.Name = "sbLogin";
            this.sbLogin.Size = new System.Drawing.Size(117, 29);
            this.sbLogin.StyleController = this.lcLogin;
            this.sbLogin.TabIndex = 7;
            this.sbLogin.TabStop = false;
            this.sbLogin.Text = "登    陆";
            this.sbLogin.Click += new System.EventHandler(this.sbLogin_Click);
            // 
            // tePwd
            // 
            this.tePwd.EditValue = "123456";
            this.tePwd.Location = new System.Drawing.Point(230, 210);
            this.tePwd.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tePwd.Name = "tePwd";
            this.tePwd.Properties.PasswordChar = '*';
            this.tePwd.Size = new System.Drawing.Size(273, 28);
            this.tePwd.StyleController = this.lcLogin;
            this.tePwd.TabIndex = 5;
            // 
            // teUserName
            // 
            this.teUserName.EditValue = "请输入用户名";
            this.teUserName.Location = new System.Drawing.Point(230, 172);
            this.teUserName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.teUserName.Name = "teUserName";
            this.teUserName.Size = new System.Drawing.Size(273, 28);
            this.teUserName.StyleController = this.lcLogin;
            this.teUserName.TabIndex = 4;
            // 
            // acBanner
            // 
            this.acBanner.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
            this.acBanner.Location = new System.Drawing.Point(5, 5);
            this.acBanner.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.acBanner.Name = "acBanner";
            this.acBanner.Size = new System.Drawing.Size(646, 112);
            this.acBanner.TabIndex = 3;
            this.acBanner.TabStop = false;
            this.acBanner.Text = "Enter your data";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "MainGroup";
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.lciUserName,
            this.lciPwd,
            this.esiLeft,
            this.esiRight,
            this.esiBottom,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.esiBottom2,
            this.esiTop});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(656, 347);
            this.layoutControlGroup1.Text = "MainGroup";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.acBanner;
            this.layoutControlItem1.CustomizationFormText = "Title";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(1, 59);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem1.Size = new System.Drawing.Size(656, 122);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "Title";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // lciUserName
            // 
            this.lciUserName.Control = this.teUserName;
            this.lciUserName.CustomizationFormText = "用户名:   ";
            this.lciUserName.Location = new System.Drawing.Point(144, 167);
            this.lciUserName.Name = "lciUserName";
            this.lciUserName.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lciUserName.Size = new System.Drawing.Size(364, 38);
            this.lciUserName.Text = "用户名:   ";
            this.lciUserName.TextSize = new System.Drawing.Size(78, 22);
            // 
            // lciPwd
            // 
            this.lciPwd.Control = this.tePwd;
            this.lciPwd.CustomizationFormText = "密码:";
            this.lciPwd.Location = new System.Drawing.Point(144, 205);
            this.lciPwd.Name = "lciPwd";
            this.lciPwd.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lciPwd.Size = new System.Drawing.Size(364, 38);
            this.lciPwd.Text = "密   码:";
            this.lciPwd.TextSize = new System.Drawing.Size(78, 22);
            // 
            // esiLeft
            // 
            this.esiLeft.AllowHotTrack = false;
            this.esiLeft.CustomizationFormText = "esiLeft";
            this.esiLeft.Location = new System.Drawing.Point(0, 167);
            this.esiLeft.Name = "esiLeft";
            this.esiLeft.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.esiLeft.Size = new System.Drawing.Size(144, 76);
            this.esiLeft.Text = "esiLeft";
            this.esiLeft.TextSize = new System.Drawing.Size(0, 0);
            // 
            // esiRight
            // 
            this.esiRight.AllowHotTrack = false;
            this.esiRight.CustomizationFormText = "esiRight";
            this.esiRight.Location = new System.Drawing.Point(508, 167);
            this.esiRight.Name = "esiRight";
            this.esiRight.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.esiRight.Size = new System.Drawing.Size(148, 76);
            this.esiRight.Text = "esiRight";
            this.esiRight.TextSize = new System.Drawing.Size(0, 0);
            // 
            // esiBottom
            // 
            this.esiBottom.AllowHotTrack = false;
            this.esiBottom.CustomizationFormText = "esiBottom";
            this.esiBottom.Location = new System.Drawing.Point(109, 308);
            this.esiBottom.Name = "esiBottom";
            this.esiBottom.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.esiBottom.Size = new System.Drawing.Size(288, 39);
            this.esiBottom.Text = "esiBottom";
            this.esiBottom.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.sbLogin;
            this.layoutControlItem5.CustomizationFormText = "OK";
            this.layoutControlItem5.Location = new System.Drawing.Point(397, 308);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem5.Size = new System.Drawing.Size(127, 39);
            this.layoutControlItem5.Text = "OK";
            this.layoutControlItem5.TextLocation = DevExpress.Utils.Locations.Right;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.sbClose;
            this.layoutControlItem6.CustomizationFormText = "Cancel";
            this.layoutControlItem6.Location = new System.Drawing.Point(524, 308);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem6.Size = new System.Drawing.Size(132, 39);
            this.layoutControlItem6.Text = "Cancel";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.sbCustomization;
            this.layoutControlItem7.CustomizationFormText = "Customization";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 308);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem7.Size = new System.Drawing.Size(109, 39);
            this.layoutControlItem7.Text = "Customization";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // esiBottom2
            // 
            this.esiBottom2.AllowHotTrack = false;
            this.esiBottom2.CustomizationFormText = "esiBottom2";
            this.esiBottom2.Location = new System.Drawing.Point(0, 243);
            this.esiBottom2.Name = "esiBottom2";
            this.esiBottom2.Size = new System.Drawing.Size(656, 65);
            this.esiBottom2.Text = "esiBottom2";
            this.esiBottom2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // esiTop
            // 
            this.esiTop.AllowHotTrack = false;
            this.esiTop.CustomizationFormText = "esiTop";
            this.esiTop.Location = new System.Drawing.Point(0, 122);
            this.esiTop.Name = "esiTop";
            this.esiTop.Size = new System.Drawing.Size(656, 45);
            this.esiTop.Text = "esiTop";
            this.esiTop.TextSize = new System.Drawing.Size(0, 0);
            // 
            // Frm_Login
            // 
            this.AcceptButton = this.sbLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.sbClose;
            this.ClientSize = new System.Drawing.Size(656, 347);
            this.Controls.Add(this.lcLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "jCodes 管理系统";
            ((System.ComponentModel.ISupportInitialize)(this.lcLogin)).EndInit();
            this.lcLogin.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tePwd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teUserName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciUserName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciPwd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.esiLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.esiRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.esiBottom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.esiBottom2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.esiTop)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraLayout.LayoutControl lcLogin;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.Utils.Frames.ApplicationCaption8_1 acBanner;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.TextEdit teUserName;
        private DevExpress.XtraLayout.LayoutControlItem lciUserName;
        private DevExpress.XtraEditors.TextEdit tePwd;
        private DevExpress.XtraLayout.LayoutControlItem lciPwd;
        private DevExpress.XtraLayout.EmptySpaceItem esiLeft;
        private DevExpress.XtraLayout.EmptySpaceItem esiRight;
        private DevExpress.XtraLayout.EmptySpaceItem esiBottom;
        private DevExpress.XtraEditors.SimpleButton sbLogin;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.SimpleButton sbClose;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraEditors.SimpleButton sbCustomization;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private System.ComponentModel.IContainer components;
        private DevExpress.XtraLayout.EmptySpaceItem esiBottom2;
        private DevExpress.XtraLayout.EmptySpaceItem esiTop;

    }
}
