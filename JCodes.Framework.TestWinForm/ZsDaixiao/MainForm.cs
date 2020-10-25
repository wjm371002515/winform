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
using JCodes.Framework.Common.Files;
using JCodes.Framework.Common.Winform;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.Common.Office;
using JCodes.Framework.Entity;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.BLL;
using JCodes.Framework.Common.Format;
using JCodes.Framework.jCodesenum;
using JCodes.Framework.AddIn.Basic;
using DevExpress.XtraBars;
using DevExpress.Utils;
using System.Reflection;
using System.IO;

namespace JCodes.Framework.AddIn.Test
{
    public partial class MainForm : RibbonForm
    {
        #region 属性变量
        //全局热键
        private RegisterHotKeyHelper hotKey2 = new RegisterHotKeyHelper();

        //第一步：定义BackgroundWorker对象，并注册事件（执行线程主体、执行UI更新事件）
        private BackgroundWorker backgroundWorkerShowTime = null;
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
            //ChildWinManagement.PopDialogForm(typeof(AboutBox));
        }

        /// <summary>
        /// 托盘退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyMenu_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion

        /// <summary>
        /// 根据权限屏蔽功能 TODO
        /// </summary>
        private void InitAuthorizedUI()
        {
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
        #endregion

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
            // 这里事件调整为取数据库时间
            this.lblCalendar.Caption = DateTimeHelper.GetServerDateTime();

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

            #region 加载皮肤
            Splasher.Status = "正在展示相关的内容...";
            System.Threading.Thread.Sleep(Const.SLEEP_TIME);
            Application.DoEvents();
            DevExpress.XtraBars.Helpers.SkinHelper.InitSkinGallery(rgbiSkins, true);
            this.ribbonControl.Toolbar.ItemLinks.Clear();
            this.ribbonControl.Toolbar.ItemLinks.Add(rgbiSkins);
            UserLookAndFeel.Default.SetSkinStyle("Office 2010 Blue");
            #endregion

            #region 初始化菜单及界面数据
            Splasher.Status = "初始化菜单及界面数据...";
            System.Threading.Thread.Sleep(Const.SLEEP_TIME);
            Application.DoEvents();
            InitUserRelated();
            #endregion

            #region 注册右下角显示时间
            backgroundWorkerShowTime = new System.ComponentModel.BackgroundWorker();
            //设置报告进度更新
            backgroundWorkerShowTime.WorkerReportsProgress = true;
            //注册线程主体方法
            backgroundWorkerShowTime.DoWork += new DoWorkEventHandler(backgroundWorkerShowTime_DoWork);
            //注册更新UI方法
            backgroundWorkerShowTime.ProgressChanged += new ProgressChangedEventHandler(backgroundWorkerShowTime_ProgressChanged);

            backgroundWorkerShowTime.RunWorkerAsync();
            #endregion

            Splasher.Status = "初始化完毕...";
            System.Threading.Thread.Sleep(Const.SLEEP_TIME);
            Application.DoEvents();

            Splasher.Close();
            // 取消注册热键
            //SetHotKey();
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
        /// 加载图标，如果加载不成功，那么使用默认图标
        /// </summary>
        /// <param name="iconPath"></param>
        /// <returns></returns>
        private Image LoadIcon(string iconPath)
        {
            // 20170512 wjm 临时修改
            Image result = Image.FromFile(@"images\MenuIcon\020.ico");
            try
            {
                if (!string.IsNullOrEmpty(iconPath))
                {
                    string path = Path.Combine(Application.StartupPath, iconPath);
                    if (File.Exists(path))
                    {
                        result = Image.FromFile(path);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(RibbonPageHelper));
                MessageDxUtil.ShowError(ex.Message);
            }

            return result;
        }

        /// <summary>
        /// 加载插件窗体
        /// </summary>
        private void LoadPlugInForm(string typeName)
        {
            try
            {
                string[] itemArray = typeName.Split(new char[] { ',', ';' });

                string type = itemArray[0].Trim();
                string filePath = itemArray[1].Trim();//必须是相对路径

                // 判断是否是打开连接
                // 如果是 则做页面的调整操作
                if (Const.BtnLink == type)
                {
                    Process.Start(filePath);
                    return;
                }

                //判断是否配置了显示模式，默认窗体为Show非模式显示
                string showDialog = (itemArray.Length > 2) ? itemArray[2].ToLower() : "";
                bool isShowDialog = (showDialog == "1") || (showDialog == "dialog");

                if (isShowDialog)
                {
                    if (Portal.gc._waitBeforeLogin != null)
                    {
                        Portal.gc._waitBeforeLogin.Invoke((EventHandler)delegate
                        {
                            if (Portal.gc._waitBeforeLogin != null)
                            {
                                Portal.gc._waitBeforeLogin.Close(); Portal.gc._waitBeforeLogin = null;
                            }
                        });
                    }
                }

                string dllFullPath = Path.Combine(Application.StartupPath, filePath);
                Assembly tempAssembly = System.Reflection.Assembly.LoadFrom(dllFullPath);
                if (tempAssembly != null)
                {
                    Type objType = tempAssembly.GetType(type);
                    if (objType != null)
                    {
                        LoadMdiForm(this, objType, isShowDialog);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(RibbonPageHelper));
                MessageDxUtil.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 唯一加载某个类型的窗体，如果存在则显示，否则创建。
        /// </summary>
        /// <param name="mainDialog">主窗体对象</param>
        /// <param name="formType">待显示的窗体类型</param>
        /// <returns></returns>
        public static Form LoadMdiForm(Form mainDialog, Type formType, bool isShowDialog)
        {
            Form tableForm = null;
            bool bFound = false;
            if (!isShowDialog) //如果是模态窗口，跳过
            {
                foreach (Form form in mainDialog.MdiChildren)
                {
                    if (form.GetType() == formType)
                    {
                        tableForm = form;
                        bFound = true;
                        break;
                    }
                }
            }

            //没有在多文档中找到或者是模态窗口，需要初始化属性
            if (!bFound || isShowDialog)
            {
                tableForm = (Form)Activator.CreateInstance(formType);
            }

            if (isShowDialog)
            {
                tableForm.ShowDialog();
            }
            else
            {
                tableForm.MdiParent = mainDialog;
                tableForm.Show();
            }
            tableForm.BringToFront();
            tableForm.Activate();

            return tableForm;
        }

        private void AddPages()
        {
            // 约定菜单共有3级，第一级为大的类别，第二级为小模块分组，第三级为具体的菜单 
            //List<MenuNodeInfo> menuList = BLLFactory<Menus>.Instance.GetTree(Portal.gc.SYSTEMTYPEID);
            string firguid = "7b5ea040-563b-40aa-a560-170ac9374f56";
            string secguid = "4314381b-fbff-43cf-b727-f72d53e21264";
            string thirdguid1 = "402d58c4-d536-4213-8d35-cbf5164ed8eb";
            string thirdguid2 = "8ff8c148-3675-409d-b80d-22043edcc151";

            List<MenuNodeInfo> menuList = new List<MenuNodeInfo>();
            MenuNodeInfo firstmenuNodeInfo = new MenuNodeInfo();
            firstmenuNodeInfo.Pgid = "-1";
            firstmenuNodeInfo.Gid = firguid;
            firstmenuNodeInfo.CreatorId = 1;
            firstmenuNodeInfo.CreatorTime = DateTime.Now;
            firstmenuNodeInfo.Icon = @"images\MenuIcon\020.ico";
            firstmenuNodeInfo.IsDelete = 0;
            firstmenuNodeInfo.IsVisable = 1;
            firstmenuNodeInfo.LastUpdateTime = DateTime.Now;
            firstmenuNodeInfo.Name = "主菜单";
            firstmenuNodeInfo.Seq = "1";
            firstmenuNodeInfo.WinformClass = "#";
            menuList.Add(firstmenuNodeInfo);

            List<MenuNodeInfo> secondmenuList = new List<MenuNodeInfo>();
            MenuNodeInfo secondmenuNodeInfo = new MenuNodeInfo();
            secondmenuNodeInfo.Pgid = firguid;
            secondmenuNodeInfo.Gid = firguid;
            secondmenuNodeInfo.CreatorId = 1;
            secondmenuNodeInfo.CreatorTime = DateTime.Now;
            secondmenuNodeInfo.Icon = @"images\MenuIcon\020.ico";
            secondmenuNodeInfo.IsDelete = 0;
            secondmenuNodeInfo.IsVisable = 1;
            secondmenuNodeInfo.LastUpdateTime = DateTime.Now;
            secondmenuNodeInfo.Name = "代销数据工具";
            secondmenuNodeInfo.Seq = "1";
            secondmenuNodeInfo.WinformClass = "";
            secondmenuList.Add(secondmenuNodeInfo);
            firstmenuNodeInfo.Children = secondmenuList;

            List<MenuNodeInfo> thirdmenuList = new List<MenuNodeInfo>();
            MenuNodeInfo third1menuNodeInfo = new MenuNodeInfo();
            third1menuNodeInfo.Pgid = firguid;
            third1menuNodeInfo.Gid = firguid;
            third1menuNodeInfo.CreatorId = 1;
            third1menuNodeInfo.CreatorTime = DateTime.Now;
            third1menuNodeInfo.Icon = @"images\MenuIcon\009.ico";
            third1menuNodeInfo.IsDelete = 0;
            third1menuNodeInfo.IsVisable = 1;
            third1menuNodeInfo.LastUpdateTime = DateTime.Now;
            third1menuNodeInfo.Name = "配置代销数据";
            third1menuNodeInfo.Seq = "1";
            third1menuNodeInfo.WinformClass = "JCodes.Framework.TestWinForm.FrmConsignment;JCodes.Framework.TestWinForm.exe";
            thirdmenuList.Add(third1menuNodeInfo);

            MenuNodeInfo third2menuNodeInfo = new MenuNodeInfo();
            third2menuNodeInfo.Pgid = firguid;
            third2menuNodeInfo.Gid = firguid;
            third2menuNodeInfo.CreatorId = 1;
            third2menuNodeInfo.CreatorTime = DateTime.Now;
            third2menuNodeInfo.Icon = @"images\MenuIcon\028.ico";
            third2menuNodeInfo.IsDelete = 0;
            third2menuNodeInfo.IsVisable = 1;
            third2menuNodeInfo.LastUpdateTime = DateTime.Now;
            third2menuNodeInfo.Name = "代销处理";
            third2menuNodeInfo.Seq = "2";
            third2menuNodeInfo.WinformClass = "JCodes.Framework.TestWinForm.ZsDaixiao.FrmDealConsignment;JCodes.Framework.TestWinForm.exe";
            thirdmenuList.Add(third2menuNodeInfo);
            secondmenuNodeInfo.Children = thirdmenuList;

            /*List<MenuNodeInfo> secondmenuList = new List<MenuNodeInfo>();
            MenuNodeInfo secondmenuNodeInfo = new MenuNodeInfo();
            secondmenuNodeInfo.Pgid = firguid;
            secondmenuNodeInfo.Gid = firguid;
            secondmenuNodeInfo.CreatorId = 1;
            secondmenuNodeInfo.CreatorTime = DateTime.Now;
            secondmenuNodeInfo.Icon = @"images\MenuIcon\020.ico";
            secondmenuNodeInfo.IsDelete = 0;
            secondmenuNodeInfo.IsVisable = 1;
            secondmenuNodeInfo.LastUpdateTime = DateTime.Now;
            secondmenuNodeInfo.Name = "浩天网络";
            secondmenuNodeInfo.Seq = "1";
            secondmenuNodeInfo.WinformClass = "";
            secondmenuList.Add(secondmenuNodeInfo);
            firstmenuNodeInfo.Children = secondmenuList;

            List<MenuNodeInfo> thirdmenuList = new List<MenuNodeInfo>();
            MenuNodeInfo third1menuNodeInfo = new MenuNodeInfo();
            third1menuNodeInfo.Pgid = firguid;
            third1menuNodeInfo.Gid = firguid;
            third1menuNodeInfo.CreatorId = 1;
            third1menuNodeInfo.CreatorTime = DateTime.Now;
            third1menuNodeInfo.Icon = @"images\MenuIcon\009.ico";
            third1menuNodeInfo.IsDelete = 0;
            third1menuNodeInfo.IsVisable = 1;
            third1menuNodeInfo.LastUpdateTime = DateTime.Now;
            third1menuNodeInfo.Name = "XLS数据处理";
            third1menuNodeInfo.Seq = "1";
            third1menuNodeInfo.WinformClass = "JCodes.Framework.TestWinForm.Haotian.FrmHackVote;JCodes.Framework.TestWinForm.exe";
            thirdmenuList.Add(third1menuNodeInfo);
            secondmenuNodeInfo.Children = thirdmenuList;*/

            if (menuList.Count == 0) return;

            int i = 0;
            foreach (MenuNodeInfo firstInfo in menuList)
            {
                //添加页面（一级菜单）
                RibbonPage page = new RibbonPage();
                page.Text = firstInfo.Name;
                page.Name = firstInfo.Gid;
                this.ribbonControl.Pages.Insert(i++, page);

                if (firstInfo.Children.Count == 0) continue;
                foreach (MenuNodeInfo secondInfo in firstInfo.Children)
                {
                    //添加RibbonPageGroup（二级菜单）
                    RibbonPageGroup group = new RibbonPageGroup();
                    group.Text = secondInfo.Name;
                    group.Name = secondInfo.Gid;
                    group.Glyph = LoadIcon(secondInfo.Icon);
                    group.ImageIndex = 0;
                    page.Groups.Add(group);

                    if (secondInfo.Children.Count == 0) continue;
                    foreach (MenuNodeInfo thirdInfo in secondInfo.Children)
                    {
                        // 判断 WinformType 如果是 RgbiSkins 则表示皮肤
                        if (thirdInfo.WinformClass == Const.RgbiSkins)
                        {
                            RibbonGalleryBarItem rgbi = new RibbonGalleryBarItem();
                            var galleryItemGroup1 = new GalleryItemGroup();
                            rgbi.Name = thirdInfo.Gid;
                            rgbi.Caption = thirdInfo.Name;
                            rgbi.Gallery.Groups.AddRange(new DevExpress.XtraBars.Ribbon.GalleryItemGroup[] { galleryItemGroup1 });
                            group.ItemLinks.Add(rgbi);
                            DevExpress.XtraBars.Helpers.SkinHelper.InitSkinGallery(rgbi, true);
                        }
                        else
                        {
                            //添加功能按钮（三级菜单）
                            BarButtonItem button = new BarButtonItem();
                            button.PaintStyle = BarItemPaintStyle.CaptionGlyph;
                            button.LargeGlyph = LoadIcon(thirdInfo.Icon);
                            button.Glyph = LoadIcon(thirdInfo.Icon);

                            button.Name = thirdInfo.Gid;
                            button.Caption = thirdInfo.Name;
                            button.Tag = thirdInfo.WinformClass;
                            button.ItemClick += (sender, e) =>
                            {
                                if (button.Tag != null && !string.IsNullOrEmpty(button.Tag.ToString()))
                                {
                                    Portal.gc._waitBeforeLogin = new WaitDialogForm("正则加载 " + button.Caption + " 窗体中...", "加载窗体");
                                    LoadPlugInForm(button.Tag.ToString());
                                    if (Portal.gc._waitBeforeLogin != null)
                                    {
                                        Portal.gc._waitBeforeLogin.Invoke((EventHandler)delegate
                                        {
                                            if (Portal.gc._waitBeforeLogin != null)
                                            {
                                                Portal.gc._waitBeforeLogin.Close(); Portal.gc._waitBeforeLogin = null;
                                            }
                                        });
                                    }
                                }
                                else
                                {
                                    MessageDxUtil.ShowTips(button.Caption);
                                }
                            };
                            if (thirdInfo.WinformClass.Contains(Const.BeginGroup))
                            {
                                group.ItemLinks.Add(button, true);
                            }
                            else
                            {
                                group.ItemLinks.Add(button);
                            }

                        }
                    }
                }
            }
        }

        /// <summary>
        /// 初始化用户相关的系统信息
        /// </summary>
        private void InitUserRelated()
        {
            #region 初始化系统名称
            try
            {
                string Manufacturer = Portal.gc.config.AppConfigGet("Manufacturer");
                string ApplicationName = Portal.gc.config.AppConfigGet("ApplicationName");
                string AppWholeName = string.Format("{0}-【{1}】", Manufacturer, ApplicationName);
                Portal.gc.AppUnit = Manufacturer;
                Portal.gc.AppName = AppWholeName;
                Portal.gc.AppWholeName = AppWholeName;


                if (!RegistryHelper.CheckRegister())
                {
                    AppWholeName += "[未注册]";
                }

                this.Text = AppWholeName;
                this.notifyIcon.BalloonTipText = AppWholeName;
                this.notifyIcon.BalloonTipTitle = AppWholeName;
                this.notifyIcon.Text = AppWholeName;
                this.notifyIcon.Tag = AppWholeName;

                lblCurrentUser.Caption = string.Format("当前用户：{0}({1})", "吴建明", "Jimmy");
                lblCommandStatus.Caption = string.Format("欢迎使用 {0}", Portal.gc.AppWholeName);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(MainForm));
                MessageDxUtil.ShowError(ex.Message);
                return;
            }

            #endregion

            // 手工新增菜单
            AddPages();

            //根据权限屏蔽菜单对象
            InitAuthorizedUI();

            if (this.ribbonControl.Pages.Count > 0)
            {
                ribbonControl.SelectedPage = ribbonControl.Pages[0];
            }
        }

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
            ChildWinManagement.PopDialogForm(typeof(FrmFeeBack));
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
            ChildWinManagement.PopDialogForm(typeof(FrmModifyPassword));
        }

        #endregion

        #region 异步更新时间
        //第二步：定义执行线程主体事件
        //线程主体方法
        public void backgroundWorkerShowTime_DoWork(object sender, DoWorkEventArgs e)
        {
            while(true){
                System.Threading.Thread.Sleep(1000);
                backgroundWorkerShowTime.ReportProgress(0, DateTimeHelper.GetServerDateTime());
            }
        }

        //第三步：定义执行UI更新事件
        //UI更新方法
        public void backgroundWorkerShowTime_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.lblCalendar.Caption = e.UserState.ToString();
        }

        #endregion
        /// <summary>
        /// 手工刷新内存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MessageDxUtil.ShowTips("TODO 刷新内存");
        }
    }
}
