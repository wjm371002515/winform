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
using JCodes.Framework.CommonControl;
using JCodes.Framework.CommonControl.Framework;
using JCodes.Framework.AddIn.UI.Dictionary;
using JCodes.Framework.Common.Files;
using JCodes.Framework.Common.Winform;
using JCodes.Framework.AddIn.Other;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.Common.Office;
using JCodes.Framework.AddIn.UI.Common;

namespace JCodes.Framework.AddIn.UI.Basic
{
    public partial class MainForm : RibbonForm
    {
        #region 属性变量
        private AppConfig config = new AppConfig();
        //全局热键
        private RegisterHotKeyHelper hotKey2 = new RegisterHotKeyHelper();
        //用来第一次创建动态菜单
        private RibbonPageHelper ribbonHelper = null;
        #endregion 

        #region 托盘菜单操作

        /// <summary>
        /// 热键事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 托盘双击显示主界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            notifyMenu_Show_Click(sender, e);
        }

        /// <summary>
        /// 托盘关于
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyMenu_About_Click(object sender, EventArgs e)
        {
            Portal.gc.About();
        }

        /// <summary>
        /// 托盘退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyMenu_Exit_Click(object sender, EventArgs e)
        {
            Portal.gc.Quit();
        }

        #endregion

        /// <summary>
        /// 根据权限屏蔽功能 TODO
        /// </summary>
        private void InitAuthorizedUI()
        {
            //this.tool_Dict.Enabled = Portal.gc.HasFunction("Dictionary");
            //this.tool_Settings.Enabled = Portal.gc.HasFunction("Parameters");
        }

        #region Window 窗体事件
        /// <summary>
        /// 窗体移动事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                notifyIcon.ShowBalloonTip(3000, "程序最小化提示",
                    "图标已经缩小到托盘，打开窗口请双击图标即可。也可以使用Alt+S键来显示/隐藏窗体。",
                    ToolTipIcon.Info);
            }
        }

        /// <summary>
        /// 窗体控件加载完毕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            Init();
        }

        /// <summary>
        /// 初始化界面信息
        /// </summary>
        private void Init()
        {
            //显示日期信息
            CCalendar cal = new CCalendar();
            // TODO 这里事件调整为取数据库时间
            this.lblCalendar.Caption = cal.GetDateInfo(System.DateTime.Now).Fullinfo;

            //其他初始化工作 TODO
        }

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
                notifyIcon.Visible = false;
                Application.ExitThread();
            }
            else if (dr == DialogResult.No)
            {
                return;
            }
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
                notifyIcon.ShowBalloonTip(3000, "程序最小化提示",
                     "图标已经缩小到托盘，打开窗口请双击图标即可。也可以使用Alt+S键来显示/隐藏窗体。",
                     ToolTipIcon.Info);
            }
        }

        private void MainForm_MaximizedBoundsChanged(object sender, EventArgs e)
        {
            this.Hide();
        }

        /// <summary>
        /// 构造函数初始化界面信息
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            Splasher.Status = "正在展示相关的内容...";
            System.Threading.Thread.Sleep(Const.SLEEP_TIME);
            Application.DoEvents();

            InitUserRelated();

            DevExpress.XtraBars.Helpers.SkinHelper.InitSkinGallery(rgbiSkins, true);
            this.ribbonControl.Toolbar.ItemLinks.Clear();
            this.ribbonControl.Toolbar.ItemLinks.Add(rgbiSkins);
            UserLookAndFeel.Default.SetSkinStyle("Office 2010 Blue");

            Splasher.Status = "初始化完毕...";
            System.Threading.Thread.Sleep(Const.SLEEP_TIME);
            Application.DoEvents();

            Splasher.Close();
            SetHotKey();
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
                // 绑定热键事件
                hotKey2.HotKey += new RegisterHotKeyHelper.HotKeyPass(hotKey2_HotKey);
                hotKey2.StarHotKey();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(MainForm));
                MessageDxUtil.ShowError(ex.Message);
            }
        }

        // 热键事件
        void hotKey2_HotKey()
        {
            notifyMenu_Show_Click(null, null);
        }

        /// <summary>
        /// 初始化用户相关的系统信息
        /// </summary>
        private void InitUserRelated()
        {
            #region 初始化系统名称
            try
            {
                string Manufacturer = config.AppConfigGet("Manufacturer");
                string ApplicationName = config.AppConfigGet("ApplicationName");
                string AppWholeName = string.Format("{0}-【{1}】", Manufacturer, ApplicationName);
                Portal.gc.AppUnit = Manufacturer;
                Portal.gc.AppName = AppWholeName;
                Portal.gc.AppWholeName = AppWholeName;

                this.Text = AppWholeName;
                this.notifyIcon.BalloonTipText = AppWholeName;
                this.notifyIcon.BalloonTipTitle = AppWholeName;
                this.notifyIcon.Text = AppWholeName;
                this.notifyIcon.Tag = AppWholeName;

                lblCurrentUser.Caption = string.Format("当前用户：{0}({1})", Portal.gc.UserInfo.FullName, Portal.gc.UserInfo.Name);
                lblCommandStatus.Caption = string.Format("欢迎使用 {0}", Portal.gc.AppWholeName);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(MainForm));
                MessageDxUtil.ShowError(ex.Message);
                return;
            }

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
        #endregion

        #region Tab顶部右键菜单
        /// <summary>
        /// tab键右击弹出框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xtraTabbedMdiManager1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            DevExpress.XtraTab.ViewInfo.BaseTabHitInfo hi = xtraTabbedMdiManager1.CalcHitInfo(new Point(e.X, e.Y));
            if (hi.HitTest == DevExpress.XtraTab.ViewInfo.XtraTabHitTest.PageHeader)
            {
                popupMenu1.ShowPopup(Cursor.Position);
            }
        }

        /// <summary>
        /// 关闭当前窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void popMenuCloseCurrent_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            XtraMdiTabPage page = xtraTabbedMdiManager1.SelectedPage;
            if (page != null && page.MdiChild != null)
            {
                page.MdiChild.Close();
            }
        }

        /// <summary>
        /// 关闭全部窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void popMenuCloseAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CloseAllDocuments();
        }

        /// <summary>
        /// 关闭除此之外窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 关闭全部窗口
        /// </summary>
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
        /// <summary>
        /// 访问技术支持网站
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuLogo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                Process.Start(Const.SystemWebUrl);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(MainForm));
                MessageDxUtil.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 反馈邮箱
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //使用QQ开放平台的发邮件界面
            string mailUrl = string.Format(Const.Feedback_Mail);
            Process.Start(mailUrl);
        }

        /// <summary>
        /// 当前用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblCurrentUser_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Portal.gc.CurrentUserInfo();
        }
        #endregion

        #region 菜单工具栏事件处理
        /// <summary>
        /// 退出 applicationMenu1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Exit(sender, e);
        }

        /// <summary>
        /// 重新登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRelogin_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageDxUtil.ShowYesNoAndWarning("您确定需要重新登录吗？") != DialogResult.Yes)
                return;

            Portal.gc.MainDialog.Hide();

            Login dlg = new Login();
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

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnModPwd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Portal.gc.ModPwd();
        }

        #endregion
    }
}
