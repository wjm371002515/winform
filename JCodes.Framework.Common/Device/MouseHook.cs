using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Diagnostics;
using JCodes.Framework.jCodesenum;
using JCodes.Framework.jCodesenum.BaseEnum;

namespace JCodes.Framework.Common.Device
{
    /// <summary>
    /// 全局的鼠标钩子操作类，用作捕捉全局的鼠标输入操作。
    /// </summary>
    public static class MouseHook
    {
        // 鼠标钩子句柄，用于安装或者卸载钩子使用.
        private static IntPtr hHook = IntPtr.Zero;

        //鼠标钩子过滤器函数处理
        private static Hooks.HookProc hookproc = new Hooks.HookProc(Filter);

        /// <summary>
        /// 鼠标输入处理代理
        /// </summary>
        /// <param name="button">操作的鼠标键</param>
        /// <returns>如果想应用程序能捕捉到，设置为True；如果设置为False，则鼠标事件被屏蔽。</returns>
        public delegate bool MouseButtonHandler(MouseButtons button);

        /// <summary>
        /// 鼠标移动处理委托
        /// </summary>
        /// <param name="point">移动的位置点</param>
        /// <returns></returns>
        public delegate bool MouseMoveHandler(Point point);

        /// <summary>
        /// 鼠标滚动处理委托
        /// </summary>
        /// <param name="delta">滚动的数值</param>
        /// <returns></returns>
        public delegate bool MouseScrollHandler(int delta);

        /// <summary>
        /// 鼠标按键按下处理操作
        /// </summary>
        public static MouseButtonHandler ButtonDown;

        /// <summary>
        /// 鼠标按键弹起处理操作
        /// </summary>
        public static MouseButtonHandler ButtonUp;

        /// <summary>
        /// 鼠标移动处理操作
        /// </summary>
        public static MouseMoveHandler Moved;

        /// <summary>
        /// 鼠标滚动处理操作
        /// </summary>
        public static MouseScrollHandler Scrolled;

        /// <summary>
        /// 继续跟踪钩子状态
        /// </summary>
        private static bool Enabled;

        /// <summary>
        /// 启动鼠标钩子处理
        /// </summary>
        /// <returns>如无异常则返回True</returns>
        public static bool Enable()
        {
            if (Enabled == false)
            {
                try
                {
                    using (Process curProcess = Process.GetCurrentProcess())
                    {
                        using (ProcessModule curModule = curProcess.MainModule)
                        {
                            hHook = Hooks.SetWindowsHookEx((int)HookType.WH_MOUSE_LL, hookproc, Hooks.GetModuleHandle(curModule.ModuleName), 0);
                        }
                    }
                     
                    Enabled = true;
                    return true;
                }
                catch (Exception ex)
                {
                    Enabled = false;
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(MouseHook));
                    return false;
                }
            }
            else
                return false;
        }

        /// <summary>
        /// 禁用鼠标钩子操作
        /// </summary>
        /// <returns>禁用成功则返回True</returns>
        public static bool Disable()
        {
            if (Enabled == true)
            {
                try
                {
                    Hooks.UnhookWindowsHookEx(hHook);
                    Enabled = false;
                    return true;
                }
                catch (Exception ex)
                {
                    Enabled = true;
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(MouseHook));
                    return false;
                }
            }
            else
                return false;
        }

        private static IntPtr Filter(int nCode, IntPtr wParam, IntPtr lParam)
        {
            bool result = true;

            if (nCode >= 0)
            {
                Hooks.MouseHookStruct info = (Hooks.MouseHookStruct)Marshal.PtrToStructure(lParam, typeof(Hooks.MouseHookStruct));
                switch ((int)wParam)
                {
                    case Const.WM_LBUTTONDOWN:
                        result = OnMouseDown(MouseButtons.Left);
                        break;
                    case Const.WM_LBUTTONUP:
                        result = OnMouseUp(MouseButtons.Left);
                        break;
                    case Const.WM_RBUTTONDOWN:
                        result = OnMouseDown(MouseButtons.Right);
                        break;
                    case Const.WM_RBUTTONUP:
                        result = OnMouseUp(MouseButtons.Right);
                        break;
                    case Const.WM_MBUTTONDOWN:
                        result = OnMouseDown(MouseButtons.Middle);
                        break;
                    case Const.WM_MBUTTONUP:
                        result = OnMouseUp(MouseButtons.Middle);
                        break;
                    case Const.WM_XBUTTONDOWN:
                        if (info.Data >> 16 == Const.XBUTTON1)
                            result = OnMouseDown(MouseButtons.XButton1);
                        else if (info.Data >> 16 == Const.XBUTTON2)
                            result = OnMouseDown(MouseButtons.XButton2);
                        break;
                    case Const.WM_XBUTTONUP:
                        if (info.Data >> 16 == Const.XBUTTON1)
                            result = OnMouseUp(MouseButtons.XButton1);
                        else if (info.Data >> 16 == Const.XBUTTON2)
                            result = OnMouseUp(MouseButtons.XButton2);
                        break;
                    case Const.WM_MOUSEMOVE:
                        result = OnMouseMove(new Point(info.Point.X, info.Point.Y));
                        break;
                    case Const.WM_MOUSEWHEEL:
                        result = OnMouseWheel((info.Data >> 16) & 0xffff);
                        break;
                }
            }

            return result ? Hooks.CallNextHookEx(hHook, nCode, wParam, lParam) : new IntPtr(1);
        }

        private static bool OnMouseDown(MouseButtons button)
        {
            if (ButtonDown != null)
                return ButtonDown(button);
            else
                return true;
        }

        private static bool OnMouseUp(MouseButtons button)
        {
            if (ButtonUp != null)
                return ButtonUp(button);
            else
                return true;
        }

        private static bool OnMouseMove(Point point)
        {
            if (Moved != null)
                return Moved(point);
            else
                return true;
        }

        private static bool OnMouseWheel(int delta)
        {
            if (Scrolled != null)
                return Scrolled(delta);
            else
                return true;
        }
    }

    /// <summary>
    /// 鼠标和键盘钩子处理需要用到的类
    /// </summary>
    public static class Hooks
    {
        #region Interop

        //This is the Import for the SetWindowsHookEx function.
        //Use this function to install a thread-specific hook.
        [DllImport("user32.dll", CharSet = CharSet.Auto,
         CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn,
        IntPtr hInstance, int threadId);

        //This is the Import for the UnhookWindowsHookEx function.
        //Call this function to uninstall the hook.
        [DllImport("user32.dll", CharSet = CharSet.Auto,
         CallingConvention = CallingConvention.StdCall)]
        internal static extern bool UnhookWindowsHookEx(IntPtr idHook);

        //This is the Import for the CallNextHookEx function.
        //Use this function to pass the hook information to the next hook procedure in chain.
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr GetModuleHandle(string lpModuleName);

        #endregion

        /// <summary>
        /// 作为一个处理（过滤器）的方法类型
        /// </summary>
        public delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        [StructLayout(LayoutKind.Sequential)]
        internal struct MouseHookStruct
        {
            public Point Point;
            public int Data;
            public int Flags;
            public int Time;
            public IntPtr ExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct Point
        {
            public int X;
            public int Y;
        }
    }
}
