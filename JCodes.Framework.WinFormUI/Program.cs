using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using System.Text;
using DevExpress.XtraEditors;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Diagnostics;
using JCodes.Framework.Common;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.CommonControl.Framework;
using JCodes.Framework.CommonControl;
using JCodes.Framework.AddIn;
using DevExpress.Utils;
using DevExpress.XtraSplashScreen;
using JCodes.Framework.Common.Files;
using JCodes.Framework.Common.Network;
using JCodes.Framework.CommonControl.Other;
using OAUS.Core;
using JCodes.Framework.AddIn.Basic;

namespace JCodes.Framework.WinFormUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // lst 保存进程参数，后续判断参数只用使用lst.Contains 即可
            List<string> lst = new List<string>();
            foreach (string arg in args)
            {
                lst.Add(arg);
            }

            // 20150918 wujm09397 捕捉系统框架的异常
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.UserSkins.BonusSkins.Register();
            UserLookAndFeel.Default.SetSkinStyle("DevExpress Style");

            Portal.gc._waitBeforeLogin = new WaitDialogForm(Const.StartAppText, Const.SystemTipInfo);

            AppConfig appConfig = new AppConfig(@"AutoUpdater\\AutoUpdater.exe.config");
            string serverIP = appConfig.AppConfigGet("ServerIP");
            Int32 serverPort = Convert.ToInt32(appConfig.AppConfigGet("ServerPort"));
            Boolean _isUpdate = Convert.ToBoolean(appConfig.AppConfigGet("isUpdate"));

            // 检查更新服务器端口是否可用
            if (_isUpdate && (NetworkUtil.CheckIPPortEnabled(serverIP, serverPort) < 0 || NetworkUtil.CheckIPPortEnabled(serverIP, serverPort + 2) < 0))
            {
                _isUpdate = false;
                MessageDxUtil.ShowTips("更新服务器端不可用,服务器更新取消!");
            }

            // 自动升级工具
            if (_isUpdate && VersionHelper.HasNewVersion(serverIP, serverPort) && (MessageDxUtil.ShowYesNoAndTips("服务器有新的版本是否更新") == DialogResult.Yes))
            {
                string updateExePath = AppDomain.CurrentDomain.BaseDirectory + "AutoUpdater\\AutoUpdater.exe";
                System.Diagnostics.Process myProcess = System.Diagnostics.Process.Start(updateExePath);
            }
            else
            {
                // 重启之后需要停止一段时间确保进程已经死了
                if (lst.Contains(Const.Restart)) {
                    Thread.Sleep(Const.SLEEP_TIME*2);
                }

                Process[] processes = System.Diagnostics.Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName);

                try
                {
                    if (processes.Length > 1)
                    {
                        MessageDxUtil.ShowTips(Const.StartAppText);
                        Thread.Sleep(Const.SLEEP_TIME);
                        System.Environment.Exit(1);
                    }
                    else
                    {
                        LoginNormal(args);
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_EMERG, ex, typeof(Program));
                    MessageDxUtil.ShowError(ex.Message);
                }
            }
        }

        private static void LoginNormal(string[] args)
        {
            Login dlg = new Login();
            dlg.StartPosition = FormStartPosition.CenterScreen;
            if (DialogResult.OK == dlg.ShowDialog())
            {
                if (dlg.bLogin)
                {
                    Splasher.Show(typeof(frmSplash));

                    MainForm MainDialog = new MainForm();
                    Portal.gc.MainDialog = MainDialog;
                    Portal.gc.MainDialog.StartPosition = FormStartPosition.CenterScreen;

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

                    Application.Run(Portal.gc.MainDialog);
                }
            }
            dlg.Dispose();
        }

        /// <summary>
        /// 进程异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            if (e.Exception == null)
                return;

            LogHelper.WriteLog(LogLevel.LOG_LEVEL_EMERG, e.Exception, typeof(Program));

            Thread t = new Thread(getScreenshot);
            t.IsBackground = true;
            t.Start();

            FrmException.ShowBug(e.Exception);  
        }

        /// <summary>
        /// 当前域异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;
            if (ex == null)
                return;
            if (ex is AggregateException)
            {
                var sb = new System.Text.StringBuilder();
                foreach (var innerEx in (ex as AggregateException).Flatten().InnerExceptions)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_EMERG, innerEx, typeof(Program));
                    sb.AppendLine(innerEx.Message);
                }
                FrmException.ShowBug(ex); 
                MessageDxUtil.ShowError(sb.ToString());
                // 捕捉异常图片
                getScreenshot();
            }
            else
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_EMERG, ex, typeof(Program));
                FrmException.ShowBug(ex); 
                // 捕捉异常图片
                getScreenshot();
            }
        }

        private static void getScreenshot()
        {
            Thread.Sleep(Const.SLEEP_TIME); 
            int width = Screen.PrimaryScreen.Bounds.Width;
            int height = Screen.PrimaryScreen.Bounds.Height;
            Bitmap bmp = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(0, 0, 0, 0, new Size(width, height));
                bmp.Save("Screen\\" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".png", ImageFormat.Png);
            }
        }
    }
}