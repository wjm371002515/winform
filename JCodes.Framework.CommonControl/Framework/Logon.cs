using System;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Collections;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;
using JCodes.Framework.BLL;

namespace JCodes.Framework.CommonControl.Framework
{
	/// <summary>
	/// Logon 的摘要说明。
	/// </summary>
	public class Logon : BaseDock
	{
		#region Private Members

		private GroupBox groupBox1;
		private Label label1;
		private Label label2;
        private DevExpress.XtraEditors.SimpleButton btExit;
        private DevExpress.XtraEditors.SimpleButton btLogin;
		private DevExpress.XtraEditors.ComboBoxEdit cmbzhanhao;
		private TextBox tbPass;
		private Label lblTitle;
		private Label lblCalendar;
		private LinkLabel linkHelp;
        private LinkLabel lnkSecurity;

		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private Container components = null;

		#endregion

        public bool bLogin = false; //判断用户是否登录
        private const string Login_Name_Key = "WareHouseMis_LoginName";
        private BackgroundWorker updateWorker;
        private RegisterHotKeyHelper hotKey1 = new RegisterHotKeyHelper();
        private DevExpress.XtraEditors.LabelControl lnkRegister;
        private RegisterHotKeyHelper hotKey2 = new RegisterHotKeyHelper();

		public Logon()
		{
			InitializeComponent();

            //初始化账号列表
            try
            {
                //InitLoginName();
                InitHistoryLoginName();
                SetHotKey();
            }
            catch (Exception ex)
            {
                MessageDxUtil.ShowError(ex.Message);
                LogTextHelper.Error(ex);
            }

            this.btExit.DialogResult = DialogResult.Cancel;
		}

        /// <summary>
        /// 设置F1帮助 F2权限系统 的全局热键
        /// </summary>
        private void SetHotKey()
        {
            hotKey1.Keys = Keys.F1;
            hotKey1.ModKey = 0;
            hotKey1.WindowHandle = this.Handle;
            hotKey1.WParam = 10001;
            hotKey1.HotKey += new RegisterHotKeyHelper.HotKeyPass(hotKey1_HotKey);
            hotKey1.StarHotKey();

            hotKey2.Keys = Keys.F2;
            hotKey2.ModKey = 0;
            hotKey2.WindowHandle = this.Handle;
            hotKey2.WParam = 10002;
            hotKey2.HotKey += new RegisterHotKeyHelper.HotKeyPass(hotKey2_HotKey);
            hotKey2.StarHotKey();
        }

        void hotKey1_HotKey()
        {
            linkHelp_LinkClicked(null, null);
        }

        void hotKey2_HotKey()
        {
            lnkSecurity_LinkClicked(null, null);
        }

        /// <summary>
        /// 从数据库中列出相关用户
        /// </summary>
        private void InitLoginName()
        {
            List<UserInfo> userList = BLLFactory<User>.Instance.GetAll();
            this.cmbzhanhao.Properties.Items.Clear();
            foreach (UserInfo info in userList)
            {
                this.cmbzhanhao.Properties.Items.Add(info.Name);
            }
        }

        /// <summary>
        /// 初始化账号登录列表
        /// </summary>
        private void InitHistoryLoginName()
        {
            string loginNames = RegistryHelper.GetValue(Login_Name_Key);
            if (!string.IsNullOrEmpty(loginNames))
            {
                if (this.cmbzhanhao.Properties.Items.Count > 0)
                {
                    this.cmbzhanhao.SetComboBoxItem(loginNames);
                }
                else
                {
                    this.cmbzhanhao.Text = loginNames;
                }
            }
        }

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Logon));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbPass = new System.Windows.Forms.TextBox();
            this.cmbzhanhao = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btExit = new DevExpress.XtraEditors.SimpleButton();
            this.btLogin = new DevExpress.XtraEditors.SimpleButton();
            this.lblTitle = new System.Windows.Forms.Label();
            this.linkHelp = new System.Windows.Forms.LinkLabel();
            this.lblCalendar = new System.Windows.Forms.Label();
            this.lnkSecurity = new System.Windows.Forms.LinkLabel();
            this.lnkRegister = new DevExpress.XtraEditors.LabelControl();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbzhanhao.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.tbPass);
            this.groupBox1.Controls.Add(this.cmbzhanhao);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(72, 86);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(352, 150);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "登录信息";
            // 
            // tbPass
            // 
            this.tbPass.Location = new System.Drawing.Point(96, 86);
            this.tbPass.Name = "tbPass";
            this.tbPass.PasswordChar = '*';
            this.tbPass.Size = new System.Drawing.Size(184, 29);
            this.tbPass.TabIndex = 1;
            this.tbPass.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbPass_KeyDown);
            // 
            // cmbzhanhao
            // 
            this.cmbzhanhao.Location = new System.Drawing.Point(96, 43);
            this.cmbzhanhao.Name = "cmbzhanhao";
            this.cmbzhanhao.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbzhanhao.Size = new System.Drawing.Size(184, 28);
            this.cmbzhanhao.TabIndex = 0;
            this.cmbzhanhao.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbzhanhao_KeyDown);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(32, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "登录账号";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(32, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 24);
            this.label2.TabIndex = 0;
            this.label2.Text = "登录密码";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btExit
            // 
            this.btExit.Location = new System.Drawing.Point(349, 268);
            this.btExit.Name = "btExit";
            this.btExit.Size = new System.Drawing.Size(75, 25);
            this.btExit.TabIndex = 1;
            this.btExit.Text = "退出";
            this.btExit.Click += new System.EventHandler(this.btExit_Click);
            // 
            // btLogin
            // 
            this.btLogin.Location = new System.Drawing.Point(248, 268);
            this.btLogin.Name = "btLogin";
            this.btLogin.Size = new System.Drawing.Size(75, 25);
            this.btLogin.TabIndex = 0;
            this.btLogin.Text = "登录";
            this.btLogin.Click += new System.EventHandler(this.btLogin_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblTitle.Location = new System.Drawing.Point(32, 17);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(370, 25);
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "仓库管理系统登录界面";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // linkHelp
            // 
            this.linkHelp.BackColor = System.Drawing.Color.Transparent;
            this.linkHelp.Location = new System.Drawing.Point(440, 34);
            this.linkHelp.Name = "linkHelp";
            this.linkHelp.Size = new System.Drawing.Size(56, 25);
            this.linkHelp.TabIndex = 2;
            this.linkHelp.TabStop = true;
            this.linkHelp.Text = "寻求帮助";
            this.linkHelp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkHelp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkHelp_LinkClicked);
            // 
            // lblCalendar
            // 
            this.lblCalendar.BackColor = System.Drawing.Color.Transparent;
            this.lblCalendar.Location = new System.Drawing.Point(59, 318);
            this.lblCalendar.Name = "lblCalendar";
            this.lblCalendar.Size = new System.Drawing.Size(343, 25);
            this.lblCalendar.TabIndex = 6;
            // 
            // lnkSecurity
            // 
            this.lnkSecurity.AutoSize = true;
            this.lnkSecurity.BackColor = System.Drawing.Color.Transparent;
            this.lnkSecurity.Location = new System.Drawing.Point(398, 327);
            this.lnkSecurity.Name = "lnkSecurity";
            this.lnkSecurity.Size = new System.Drawing.Size(151, 22);
            this.lnkSecurity.TabIndex = 7;
            this.lnkSecurity.TabStop = true;
            this.lnkSecurity.Text = "权限管理系统(F2)";
            this.lnkSecurity.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSecurity_LinkClicked);
            // 
            // lnkRegister
            // 
            this.lnkRegister.Appearance.Font = new System.Drawing.Font("新宋体", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lnkRegister.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lnkRegister.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lnkRegister.Location = new System.Drawing.Point(132, 274);
            this.lnkRegister.Name = "lnkRegister";
            this.lnkRegister.Size = new System.Drawing.Size(124, 29);
            this.lnkRegister.TabIndex = 8;
            this.lnkRegister.Text = "软件注册";
            this.lnkRegister.Click += new System.EventHandler(this.lnkRegister_Click);
            // 
            // Logon
            // 
            this.BackgroundImageLayoutStore = System.Windows.Forms.ImageLayout.Tile;
            this.BackgroundImageStore = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImageStore")));
            this.ClientSize = new System.Drawing.Size(503, 351);
            this.Controls.Add(this.lnkRegister);
            this.Controls.Add(this.lnkSecurity);
            this.Controls.Add(this.lblCalendar);
            this.Controls.Add(this.linkHelp);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btLogin);
            this.Controls.Add(this.btExit);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Logon";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "仓库管理系统登录界面";
            this.Load += new System.EventHandler(this.Logon_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Logon_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbzhanhao.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private void btExit_Click(object sender, EventArgs e)
		{
            this.DialogResult = DialogResult.Cancel;
            Application.ExitThread();
		}

		private void btLogin_Click(object sender, EventArgs e)
		{
            #region 检查验证
            if (Portal.gc.EnableRegister && !Portal.gc.Registed)
            {
                MessageUtil.ShowError("软件未进行授权注册，不能使用。\r\n请单击左边【软件注册】按钮进行注册。");
                return;
            }

            if (this.cmbzhanhao.Text.Length == 0)
            {
                MessageDxUtil.ShowTips("请输入帐号");
                this.cmbzhanhao.Focus();
                return;
            }
            #endregion

            try
            {
                string ip = NetworkUtil.GetLocalIP();
                string macAddr = HardwareInfoHelper.GetMacAddress();
                string loginName = this.cmbzhanhao.Text.Trim();
                string identity = BLLFactory<User>.Instance.VerifyUser(loginName, this.tbPass.Text, Portal.gc.SystemType, ip, macAddr);
                if (!string.IsNullOrEmpty(identity))
                {
                    UserInfo info = BLLFactory<User>.Instance.GetUserByName(loginName);
                    if (info != null)
                    {
                        #region 获取用户的功能列表

                        List<FunctionInfo> list = BLLFactory<BLL.Function>.Instance.GetFunctionsByUser(info.ID, Portal.gc.SystemType);
                        if (list != null && list.Count > 0)
                        {
                            foreach (FunctionInfo functionInfo in list)
                            {
                                if (!Portal.gc.FunctionDict.ContainsKey(functionInfo.ControlID))
                                {
                                    Portal.gc.FunctionDict.Add(functionInfo.ControlID, functionInfo.ControlID);
                                }
                            }
                        }

                        #endregion

                        bLogin = true;
                        Portal.gc.UserInfo = info;
                        Portal.gc.LoginUserInfo = Portal.gc.ConvertToLoginUser(info);
                        Cache.Instance.Add("LoginUserInfo", Portal.gc.LoginUserInfo);//缓存用户信息，方便后续处理
                        Cache.Instance.Add("FunctionDict", Portal.gc.FunctionDict);//缓存权限信息，方便后续使用

                        this.DialogResult = DialogResult.OK;
                    }
                }
                else
                {
                    MessageDxUtil.ShowTips("用户帐号密码不正确");
                    this.tbPass.Text = ""; //设置密码为空
                }
            }
            catch (Exception err)
            {
                MessageDxUtil.ShowError(err.Message);
            }
		}


		/// <summary>
		/// 对字符串加密
		/// </summary>
		/// <returns></returns>
		private string EncodePassword(string passwordText)
		{
            return EncodeHelper.MD5Encrypt(passwordText);
		}

		private void Logon_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				btLogin_Click(sender, null);
			}
			if (e.KeyCode == Keys.F1) //按下F1键将跳出帮助
			{
				linkHelp_LinkClicked(sender, null);
			}
		}

		private void cmbzhanhao_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter) //如果用户回车，跳转到密码的输入框，接受输入
			{
				this.tbPass.Focus();
			}
			if (e.KeyCode == Keys.F1) //按下F1键将跳出帮助
			{
				linkHelp_LinkClicked(sender, null);
			}
		}

		private void tbPass_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter) //密码回车，则检查用户是否符合数据库的用户
			{
                btLogin_Click(sender, null);
			}
			if (e.KeyCode == Keys.F1) //按下F1键将跳出帮助
			{
				linkHelp_LinkClicked(sender, null);
			}
		}

		/// <summary>
		/// 开始的时候提示登录账号和密码
		/// </summary>
		private void Logon_Load(object sender, EventArgs e)
		{
			ToolTip tp = new ToolTip();
			tp.SetToolTip(this.cmbzhanhao, "首次登录的默认账号为:admin");
			tp.SetToolTip(this.tbPass, "首次登录的默认密码为空, \r\n 以后请更改默认密码！");
			
			CCalendar cal = new CCalendar();
			this.lblCalendar.Text = cal.GetDateInfo(DateTime.Now).Fullinfo;
            AppConfig config = new AppConfig();
            this.Text = config.AppName;
            this.lblTitle.Text = config.AppName;

            //ValidateRegisterStatus();//提示是否已经注册

            if (this.cmbzhanhao.Text != "")
            {
                this.tbPass.Focus();
            }
            else
            {
                this.cmbzhanhao.Focus();
            }

            if (Portal.gc.EnableRegister)
            {
                //先处理用户的注册信息
                Portal.gc.CheckRegister();

                if (!Portal.gc.Registed)
                {
                    this.lnkRegister.Visible = true;
                    this.btLogin.ForeColor = Color.Red;
                    ToolTip tip = new ToolTip();
                    tip.SetToolTip(this.btLogin, "软件未进行授权注册，不能使用。\r\n请单击【软件注册】按钮进行注册。");

                    Text += " [未注册]";
                }
                else
                {
                    this.lnkRegister.Visible = false;
                    //Text += " [已注册]";
                }
            }
            else
            {
                this.lnkRegister.Visible = false;
            }

            #region 更新提示/判断是否自动更新
            updateWorker = new BackgroundWorker();
            updateWorker.DoWork += new DoWorkEventHandler(updateWorker_DoWork);
            updateWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(updateWorker_RunWorkerCompleted);

            string strUpdate = config.AppConfigGet("AutoUpdate");
            if (!string.IsNullOrEmpty(strUpdate))
            {
                bool autoUpdate = false;
                bool.TryParse(strUpdate, out autoUpdate);
                if (autoUpdate)
                {
                    updateWorker.RunWorkerAsync();
                }
            }
            #endregion
        }

        #region 更新提示线程处理
        private void updateWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //MessageDxUtil.ShowTips("版本更新完成");
        }

        private void updateWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                /*UpdateClass update = new UpdateClass();
                bool newVersion = update.HasNewVersion;
                if (newVersion)
                {
                    if (MessageDxUtil.ShowYesNoAndTips("有新的版本，是否需要更新") == DialogResult.Yes)
                    {
                        Process.Start(Path.Combine(Application.StartupPath, "Updater.exe"), "121");
                        Application.Exit();
                    }
                }*/
            }
            catch (Exception ex)
            {
                MessageDxUtil.ShowError(ex.Message);
            }
        }

        #endregion

		private void linkHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			try
			{
				Process.Start("Help.chm");
			}
			catch (Exception)
			{
				MessageDxUtil.ShowWarning("不能打开帮助！");
			}
		}

        private void lnkSecurity_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // 20170513 wjm 暂时注释掉
            //Portal.StartLogin();
        }

        private void lnkRegister_Click(object sender, EventArgs e)
        {
            RegDlg myRegdlg = RegDlg.Instance();
            myRegdlg.ShowDialog();
        }
	}
}