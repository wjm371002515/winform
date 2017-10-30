using DevExpress.Utils;
using JCodes.Framework.Common;
using JCodes.Framework.Common.Files;
using JCodes.Framework.Common.Network;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.Test;
using OAUS.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestCommons;
using WHC.OrderWater.UI;

namespace JCodes.Framework.TestWinForm
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            /*
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-CN");
            DevExpress.UserSkins.BonusSkins.Register();
            DevExpress.Skins.SkinManager.EnableFormSkins();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            AppConfig appConfig = new AppConfig(@"AutoUpdater\AutoUpdater.exe.config");
            string serverIP = appConfig.AppConfigGet("ServerIP");
            Int32 serverPort = Convert.ToInt32( appConfig.AppConfigGet("ServerPort"));
            Boolean _isUpdate = Convert.ToBoolean(appConfig.AppConfigGet("isUpdate"));
            

            // 检查更新服务器端口是否可用
            if (NetworkUtil.CheckIPPortEnabled(serverIP, serverPort) < 0 || NetworkUtil.CheckIPPortEnabled(serverIP, serverPort+2) < 0)
            {
                _isUpdate = false;
                MessageDxUtil.ShowTips("更新服务器端不可用,服务器更新取消!");
            }

            // 自动升级工具
            /*if (_isUpdate && VersionHelper.HasNewVersion(serverIP, serverPort) && (MessageDxUtil.ShowYesNoAndTips("服务器有新的版本是否更新") == DialogResult.Yes))
            {
                string updateExePath = AppDomain.CurrentDomain.BaseDirectory + "AutoUpdater\\AutoUpdater.exe";
                System.Diagnostics.Process myProcess = System.Diagnostics.Process.Start(updateExePath);  
            }
            else
            {
                WaitDialogForm WaitBeforeLogin = null;
                new Thread((ThreadStart)delegate
                {
                    WaitBeforeLogin = new DevExpress.Utils.WaitDialogForm("请稍候...", "正在加载应用系统");
                    Application.Run(new TestCommons.Form1(WaitBeforeLogin));
                }).Start();
            }*/
            //Application.Run(new Form1());

            // 分页控件
            //Application.Run(new TestDictionary.Form1());
            
            // 测试公共类
            //Application.Run(new TestCommons.Form1());

            //Application.Run(new TestControlUtil.Form1());

            // 附件测试
            //Application.Run(new TestAttachmentDx.Form1());

            // 权限管理系统
            //Application.Run(new TestSecurityMix_WCF_WIN.Form1());
            DevExpress.UserSkins.BonusSkins.Register();
            Application.Run(new SyntaxHighlightModule());
        }
    }
}
