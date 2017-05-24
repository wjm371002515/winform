using DevExpress.Utils;
using JCodes.Framework.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
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
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-CN");

            DevExpress.UserSkins.BonusSkins.Register();
            DevExpress.Skins.SkinManager.EnableFormSkins();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            WaitDialogForm WaitBeforeLogin = null;
            new Thread((ThreadStart)delegate
            {
                WaitBeforeLogin = new DevExpress.Utils.WaitDialogForm("请稍候...", "正在加载应用系统");
                Application.Run(new TestCommons.Form1(WaitBeforeLogin));
            }).Start();

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
        }
    }
}
