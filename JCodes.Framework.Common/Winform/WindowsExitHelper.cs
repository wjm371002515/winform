using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace JCodes.Framework.Common.Winform
{
    /// <summary>
    /// 计算机重启、关电源、注销、关闭显示器辅助类
    /// </summary>
    public class WindowsExitHelper
    {
        #region 辅助函数
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct TokPriv1Luid
        {
            public int Count;
            public long Luid;
            public int Attr;
        }

        [DllImport("kernel32.dll", ExactSpelling = true)]
        internal static extern IntPtr GetCurrentProcess();

        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern bool OpenProcessToken(IntPtr h, int acc, ref   IntPtr phtok);

        [DllImport("advapi32.dll", SetLastError = true)]
        internal static extern bool LookupPrivilegeValue(string host, string name, ref   long pluid);

        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern bool AdjustTokenPrivileges(IntPtr htok, bool disall,
        ref   TokPriv1Luid newst, int len, IntPtr prev, IntPtr relen);

        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern bool ExitWindowsEx(int flg, int rea);

        #endregion

        private static void DoExitWin(int flg)
        {
            bool ok;
            TokPriv1Luid tp;
            IntPtr hproc = GetCurrentProcess();
            IntPtr htok = IntPtr.Zero;
            ok = OpenProcessToken(hproc, Const.TOKEN_ADJUST_PRIVILEGES | Const.TOKEN_QUERY, ref   htok);
            tp.Count = 1;
            tp.Luid = 0;
            tp.Attr = Const.SE_PRIVILEGE_ENABLED;
            ok = LookupPrivilegeValue(null, Const.SE_SHUTDOWN_NAME, ref   tp.Luid);
            ok = AdjustTokenPrivileges(htok, false, ref   tp, 0, IntPtr.Zero, IntPtr.Zero);
            ok = ExitWindowsEx(flg, 0);
        }

        /// <summary>
        /// 计算机重启
        /// </summary>
        public static void Reboot()
        {
            DoExitWin(Const.EWX_FORCE | Const.EWX_REBOOT);
        }

        /// <summary>
        /// 计算机关电源
        /// </summary>
        public static void PowerOff()
        {
            DoExitWin(Const.EWX_FORCE | Const.EWX_POWEROFF);
        }

        /// <summary>
        /// 计算机注销
        /// </summary>
        public static void LogoOff()
        {
            DoExitWin(Const.EWX_FORCE | Const.EWX_LOGOFF);
        }

        /// <summary>
        /// 锁定Windows
        /// </summary>
        public static void Lock()
        {
            LockWorkStation();
        }

        #region 关闭显示器
        /// <summary>
        /// 关闭显示器
        /// </summary>
        public static void CloseMonitor()
        {
            // 2 为关闭显示器， －1则打开显示器
            SendMessage(HWND_HANDLE, Const.WM_SYSCOMMAND, Const.SC_MONITORPOWER, 2);    
        }

        private static readonly IntPtr HWND_HANDLE = new IntPtr(0xffff);
        [DllImport("user32")]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint wMsg, uint wParam, int lParam);

        [DllImport("user32.dll")]
        private static extern void LockWorkStation();

        #endregion
    }
}
