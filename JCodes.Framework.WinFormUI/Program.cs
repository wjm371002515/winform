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
using JCodes.Framework.AddIn.UI.Basic;
using JCodes.Framework.AddIn.Other;
using DevExpress.Utils;
using DevExpress.XtraSplashScreen;
using JCodes.Framework.Common.Files;

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
            // 20150918 wujm09397 捕捉系统框架的异常
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.UserSkins.BonusSkins.Register();
            UserLookAndFeel.Default.SetSkinStyle("DevExpress Style");

            Process[] processes = System.Diagnostics.Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName);

            try {
                if (processes.Length > 1)
                {

                    XtraMessageBox.Show(Const.StartAppText, Const.SystemTipInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Thread.Sleep(Const.SLEEP_TIME);
                    System.Environment.Exit(1);
                }
                else
                {
                    Thread app = new Thread((ThreadStart)delegate
                    {
                        Portal.gc._waitBeforeLogin = new WaitDialogForm(Const.StartAppText, Const.SystemTipInfo);
                        LoginNormal(args);
                    });
                    // 执行线程状态
                    app.ApartmentState = ApartmentState.STA;
                    app.Start();
                }
            }
            catch (Exception ex) {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_EMERG, ex, typeof(Program));
                XtraMessageBox.Show(ex.Message, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
          
            XtraMessageBox.Show(e.Exception.Message, "系统捕捉异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
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
                XtraMessageBox.Show(sb.ToString(), "系统捕捉异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // 捕捉异常图片
                getScreenshot();
            }
            else
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_EMERG, ex, typeof(Program));
                XtraMessageBox.Show(ex.Message, "系统捕捉异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
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