using System;
using System.Text;
using System.Data;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.LookAndFeel;
using DevExpress.XtraTabbedMdi;
using JCodes.Framework.Common;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.CommonControl.Security;

namespace JCodes.Framework.CommonControl.Framework
{
    public partial class MainForm : RibbonForm
    {
        #region 属性变量
        private AppConfig config = new AppConfig();
        //月结线程
        private BackgroundWorker worker;
        private BackgroundWorker annualWorker;
        //全局热键
        private RegisterHotKeyHelper hotKey2 = new RegisterHotKeyHelper();
        //用来第一次创建动态菜单
        private RibbonPageHelper ribbonHelper = null;

        /// <summary>
        /// 设置窗体的标题信息
        /// </summary>
        public override sealed string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        /// <summary>
        /// 获取或设置命令状态信息
        /// </summary>
        public string CommandStatus
        {
            get { return lblCommandStatus.Caption; }
            set { lblCommandStatus.Caption = value; }
        }

        /// <summary>
        /// 获取或设置用户信息
        /// </summary>
        public string UserStatus
        {
            get { return lblCurrentUser.Caption; }
            set { lblCurrentUser.Caption = value; }
        } 

        #endregion 

        public MainForm()
        {
            InitializeComponent();

            Splasher.Status = "正在展示相关的内容...";
            System.Threading.Thread.Sleep(1000);
            Application.DoEvents();

            InitUserRelated();

            DevExpress.XtraBars.Helpers.SkinHelper.InitSkinGallery(rgbiSkins, true);
            this.ribbonControl.Toolbar.ItemLinks.Clear();
            this.ribbonControl.Toolbar.ItemLinks.Add(rgbiSkins);
            UserLookAndFeel.Default.SetSkinStyle("Office 2010 Blue");

            Splasher.Status = "初始化完毕...";
            System.Threading.Thread.Sleep(100);
            Application.DoEvents();

            Splasher.Close();
            SetHotKey();
        }
                 
        /// <summary>
        /// 初始化用户相关的系统信息
        /// </summary>
        private void InitUserRelated()
        {            
            #region 根据权限显示对象的初始化窗体

            //if (Portal.gc.HasFunction("StockSearch"))
            //{
            //    ChildWinManagement.LoadMdiForm(this, typeof(FrmStockSearch));
            //}

            #endregion

            #region 初始化系统名称
            try
            {
                string CertificatedCompany = config.AppConfigGet("CertificatedCompany");
                string ApplicationName = config.AppConfigGet("ApplicationName");
                string AppWholeName = string.Format("{0}-{1}", CertificatedCompany, ApplicationName);
                Portal.gc.AppUnit = CertificatedCompany;
                Portal.gc.AppName = AppWholeName;
                Portal.gc.AppWholeName = AppWholeName;

                this.Text = AppWholeName;
                this.notifyIcon1.BalloonTipText = AppWholeName;
                this.notifyIcon1.BalloonTipTitle = AppWholeName;
                this.notifyIcon1.Text = AppWholeName;

                UserStatus = string.Format("当前用户：{0}({1})", Portal.gc.UserInfo.FullName, Portal.gc.UserInfo.Name);
                CommandStatus = string.Format("欢迎使用 {0}", Portal.gc.AppWholeName);
            }
            catch { }

            #endregion

            //动态创建界面菜单对象(防止重复构建）
            if (ribbonHelper == null)
            {
                ribbonHelper = new RibbonPageHelper(this, ref this.ribbonControl);
                ribbonHelper.AddPages();
            }

            //根据权限屏蔽菜单对象
            InitAuthorizedUI();
            if (this.ribbonControl.Pages.Count > 0)
            {
                ribbonControl.SelectedPage = ribbonControl.Pages[0];
            }
        }

        /// <summary>
        /// 设置Alt+S的显示/隐藏窗体全局热键
        /// </summary>
        private void SetHotKey()
        {
            try
            {
                hotKey2.Keys = Keys.S;
                hotKey2.ModKey = MODKEY.MOD_ALT;
                hotKey2.WindowHandle = this.Handle;
                hotKey2.WParam = 10003;
                hotKey2.HotKey += new RegisterHotKeyHelper.HotKeyPass(hotKey2_HotKey);
                hotKey2.StarHotKey();
            }
            catch (Exception ex)
            {
                MessageDxUtil.ShowError(ex.Message);
                LogTextHelper.WriteLine(ex.ToString());
            }
        }

        void hotKey2_HotKey()
        {
            notifyMenu_Show_Click(null, null);
        }

        #region 托盘菜单操作

        private void notifyMenu_About_Click(object sender, EventArgs e)
        {
            Portal.gc.About();
        }

        private void notifyMenu_Show_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Maximized;
                this.Show();
                this.BringToFront();
                this.Activate();
                this.Focus();
            }
            else
            {
                this.WindowState = FormWindowState.Minimized;
                this.Hide();
            }
        }

        private void notifyMenu_Exit_Click(object sender, EventArgs e)
        {
            try
            {
                this.ShowInTaskbar = false;
                Portal.gc.Quit();
            }
            catch
            {
                // Nothing to do.
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            notifyMenu_Show_Click(sender, e);
        }

        private void MainForm_MaximizedBoundsChanged(object sender, EventArgs e)
        {
            this.Hide();
        }

        /// <summary>
        /// 缩小到托盘中，不退出
        /// </summary>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //如果我们操作【×】按钮，那么不关闭程序而是缩小化到托盘，并提示用户.
            if (this.WindowState != FormWindowState.Minimized)
            {
                e.Cancel = true;//不关闭程序

                //最小化到托盘的时候显示图标提示信息，提示用户并未关闭程序
                this.WindowState = FormWindowState.Minimized;
                notifyIcon1.ShowBalloonTip(3000, "程序最小化提示",
                     "图标已经缩小到托盘，打开窗口请双击图标即可。也可以使用Alt+S键来显示/隐藏窗体。",
                     ToolTipIcon.Info);
            }
        }

        private void MainForm_Move(object sender, EventArgs e)
        {
            if (this == null)
            {
                return;
            }

            //最小化到托盘的时候显示图标提示信息
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                notifyIcon1.ShowBalloonTip(3000, "程序最小化提示",
                    "图标已经缩小到托盘，打开窗口请双击图标即可。也可以使用Alt+S键来显示/隐藏窗体。",
                    ToolTipIcon.Info);
            }
        }

        #endregion

        /// <summary>
        /// 退出系统
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void Exit(object sender, EventArgs args)
        {
            DialogResult dr = MessageDxUtil.ShowYesNoAndTips("点击“Yes”退出系统，点击“No”返回");

            if (dr == DialogResult.Yes)
            {
                notifyIcon1.Visible = false;
                Application.ExitThread();
            }
            else if (dr == DialogResult.No)
            {
                return;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Init();
        }

        /// <summary>
        /// 根据权限屏蔽功能
        /// </summary>
        private void InitAuthorizedUI()
        {
            this.tool_Dict.Enabled = Portal.gc.HasFunction("Dictionary");
            //this.tool_Settings.Enabled = Portal.gc.HasFunction("Parameters");
        }

        private void Init()
        {
            //显示日期信息
            CCalendar cal = new CCalendar();
            this.lblCalendar.Caption = cal.GetDateInfo(System.DateTime.Now).Fullinfo;

            //其他初始化工作
        }

        #region Tab顶部右键菜单

        private void xtraTabbedMdiManager1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            DevExpress.XtraTab.ViewInfo.BaseTabHitInfo hi = xtraTabbedMdiManager1.CalcHitInfo(new Point(e.X, e.Y));
            if (hi.HitTest == DevExpress.XtraTab.ViewInfo.XtraTabHitTest.PageHeader)
            {
                //Form f = (hi.Page as DevExpress.XtraTabbedMdi.XtraMdiTabPage).MdiChild;
                //do something
                popupMenu1.ShowPopup(Cursor.Position);
            }
        }

        private void popMenuCloseCurrent_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            XtraMdiTabPage page = xtraTabbedMdiManager1.SelectedPage;
            if (page != null && page.MdiChild != null)
            {
                page.MdiChild.Close();
            }
        }

        private void popMenuCloseAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CloseAllDocuments();
        }

        private void popMenuCloseOther_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            XtraMdiTabPage selectedPage = xtraTabbedMdiManager1.SelectedPage;
            Type currentType = selectedPage.MdiChild.GetType();

            for (int i = xtraTabbedMdiManager1.Pages.Count - 1; i >= 0; i--)
            {
                XtraMdiTabPage page = xtraTabbedMdiManager1.Pages[i];
                if (page != null && page.MdiChild != null)
                {
                    Form form = page.MdiChild;
                    if (form.GetType() != currentType)
                    {
                        form.Close();
                        if (form != null && !form.IsDisposed)
                        {
                            form.Dispose();
                        }
                    }
                }
            }
        }

        public void CloseAllDocuments()
        {
            foreach (Form form in this.MdiChildren)
            {
                form.Close();
                if (form != null && !form.IsDisposed)
                {
                    form.Dispose();
                }
            }
        }
        #endregion

        #region 工具条操作
        private void btnRelogin_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageDxUtil.ShowYesNoAndWarning("您确定需要重新登录吗？") != DialogResult.Yes)
                return;


            Portal.gc.MainDialog.Hide();

            Logon dlg = new Logon();
            dlg.StartPosition = FormStartPosition.CenterScreen;
            if (DialogResult.OK == dlg.ShowDialog())
            {
                if (dlg.bLogin)
                {
                    CloseAllDocuments();
                    InitUserRelated();
                }

            }
            dlg.Dispose();
            Portal.gc.MainDialog.Show();
        }

        private void barItemExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Exit(null, null);
        }

        private void btnHelp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Portal.gc.Help();
        }

        private void btnAbout_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Portal.gc.About();
        }

        private void btnRegister_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Portal.gc.ShowRegDlg();
        }

        private void btnBug_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmFeeBack dlg = new FrmFeeBack();
            dlg.ShowDialog();
        }

        private void btnMyWeb_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Process.Start("http://www.iqidi.com");
        }

        private void menuLogo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                Process.Start("http://www.iqidi.com");
            }
            catch (Exception)
            {
                MessageDxUtil.ShowError("打开浏览器失败");
            }
        }


        private void tool_Settings_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmSettings dlg = new FrmSettings();
            dlg.ShowDialog();
        }

        private void tool_Dict_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmDictionary dlg = new FrmDictionary();
            dlg.ShowDialog();
        }

        private void tool_Security_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // 20170513 wjm 暂时注释掉
            //Portal.StartLogin();
        }

        private void tool_ModifyPass_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmModifyPassword dlg = new FrmModifyPassword();
            dlg.InitFunction(Portal.gc.LoginUserInfo, Portal.gc.FunctionDict);//初始化权限控制信息
            dlg.ShowDialog();
        }

        private void tool_CurrentUserInfo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmEditUser dlg = new FrmEditUser();
            dlg.ID = Portal.gc.LoginUserInfo.ID.ToString();
            dlg.ShowDialog();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //使用QQ开放平台的发邮件界面
            string mailUrl = string.Format("http://mail.qq.com/cgi-bin/qm_share?t=qm_mailme&email=S31yfX15fn8LOjplKCQm");
            Process.Start(mailUrl);
        }
        
        private void lblCurrentUser_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            tool_CurrentUserInfo_ItemClick(null, null);
        }


        private void tool_MyAddress_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //ChildWinManagement.LoadMdiForm(this, typeof(FrmAddress));
        }

        private void tool_PublicAddress_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //ChildWinManagement.LoadMdiForm(this, typeof(FrmAddressCompany));
        }
        #endregion



    }
}
