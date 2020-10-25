﻿using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using JCodes.Framework.Common.Collections;
using JCodes.Framework.jCodesenum;
using JCodes.Framework.jCodesenum.BaseEnum;

namespace JCodes.Framework.Common.Device
{
    /// <summary>
    ///全局键盘钩子，用来捕捉系统全局的键盘输入。
    /// </summary>
    public static class KeyboardHook
    {
        //钩子句柄，用于安装或者卸载钩子之用
        private static IntPtr hHook = IntPtr.Zero;

        //钩子过滤器处理
        private static Hooks.HookProc hookproc = new Hooks.HookProc(Filter);

        /// <summary>
        /// 判断是否CTRL键按下
        /// </summary>
        public static bool Control = false;

        /// <summary>
        /// 判断Shift键是否按下
        /// </summary>
        public static bool Shift = false;

        /// <summary>
        /// 检查Alt键是否按下
        /// </summary>
        public static bool Alt = false;

        /// <summary>
        /// 检查Windows键是否按下
        /// </summary>
        public static bool Win = false;

        /// <summary>
        /// 键盘敲击事件代理定义
        /// </summary>
        public delegate bool KeyPressed();

        /// <summary>
        /// 键盘处理操作定义
        /// </summary>
        private static CDictionary<Keys, KeyPressed> handledKeysDown = new CDictionary<Keys, KeyPressed>();
        private static CDictionary<Keys, KeyPressed> handledKeysUp = new CDictionary<Keys, KeyPressed>();

        /// <summary>
        /// 处理键盘按下委托函数
        /// </summary>
        /// <param name="key">
        /// 按下的键。需检查CTRL、Shift、Win等键。
        /// </param>
        /// <returns>
        /// 如果想应用程序能捕捉到，设置为True；如果设置为False，则键盘事件被屏蔽。
        /// </returns>
        public delegate bool KeyboardHookHandler(Keys key);

        /// <summary>
        /// 添加一个键盘钩子处理给当前的键
        /// </summary>
        public static KeyboardHookHandler KeyDown;

        /// <summary>
        /// 继续跟踪钩子状态
        /// </summary>
        private static bool Enabled;

        /// <summary>
        /// 启动键盘钩子处理
        /// </summary>
        /// <returns>如无异常返回True</returns>
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
                            hHook = Hooks.SetWindowsHookEx((int)HookType.WH_KEYBOARD_LL, hookproc, Hooks.GetModuleHandle(curModule.ModuleName), 0);
                        }
                    }

                    Enabled = true;
                    return true;
                }
                catch (Exception ex)
                {
                    Enabled = false;
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(KeyboardHook));
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 禁用键盘钩子处理
        /// </summary>
        /// <returns>如果禁用成功，则返回True</returns>
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
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(KeyboardHook));
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private static IntPtr Filter(int nCode, IntPtr wParam, IntPtr lParam)
        {
            bool result = true;

            if (nCode >= 0)
            {
                if (wParam == (IntPtr)Const.WM_KEYDOWN
                    || wParam == (IntPtr)Const.WM_SYSKEYDOWN)
                {
                    int vkCode = Marshal.ReadInt32(lParam);
                    if ((Keys)vkCode == Keys.LControlKey ||
                        (Keys)vkCode == Keys.RControlKey)
                        Control = true;
                    else if ((Keys)vkCode == Keys.LShiftKey ||
                        (Keys)vkCode == Keys.RShiftKey)
                        Shift = true;
                    else if ((Keys)vkCode == Keys.RMenu ||
                        (Keys)vkCode == Keys.LMenu)
                        Alt = true;
                    else if ((Keys)vkCode == Keys.RWin ||
                        (Keys)vkCode == Keys.LWin)
                        Win = true;
                    else
                        result = OnKeyDown((Keys)vkCode);
                }
                else if (wParam == (IntPtr)Const.WM_KEYUP
                    || wParam == (IntPtr)Const.WM_SYSKEYUP)
                {
                    int vkCode = Marshal.ReadInt32(lParam);
                    if ((Keys)vkCode == Keys.LControlKey ||
                        (Keys)vkCode == Keys.RControlKey)
                        Control = false;
                    else if ((Keys)vkCode == Keys.LShiftKey ||
                        (Keys)vkCode == Keys.RShiftKey)
                        Shift = false;
                    else if ((Keys)vkCode == Keys.RMenu ||
                        (Keys)vkCode == Keys.LMenu)
                        Alt = false;
                    else if ((Keys)vkCode == Keys.RWin ||
                        (Keys)vkCode == Keys.LWin)
                        Win = false;
                    else
                        result = OnKeyUp((Keys)vkCode);
                }
            }

            return result ? Hooks.CallNextHookEx(hHook, nCode, wParam, lParam) : new IntPtr(1);
        }

        /// <summary>
        /// 添加一个按下键的钩子处理
        /// </summary>
        /// <param name="key">按下的键</param>
        /// <param name="callback">按键的处理事件函数</param>
        public static bool AddKeyDown(Keys key, KeyPressed callback)
        {
            KeyDown = null;
            if (!handledKeysDown.ContainsKey(key))
            {
                handledKeysDown.Add(key, callback);
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// 添加一个键弹起的钩子处理
        /// </summary>
        /// <param name="key">弹起的键</param>
        /// <param name="callback">按键的处理事件函数</param>
        public static bool AddKeyUp(Keys key, KeyPressed callback)
        {
            if (!handledKeysUp.ContainsKey(key))
            {
                handledKeysUp.Add(key, callback);
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// 添加一个按下键的钩子处理
        /// </summary>
        /// <param name="key">按下的键</param>
        /// <param name="callback">按键的处理事件函数</param>
        public static bool Add(Keys key, KeyPressed callback)
        {
            return AddKeyDown(key, callback);
        }

        /// <summary>
        /// 移除一个按下键的钩子处理
        /// </summary>
        /// <param name="key">移除的按键</param>
        public static bool RemoveDown(Keys key)
        {
            return handledKeysDown.Remove(key);
        }

        /// <summary>
        /// 移除一个弹起键的钩子处理
        /// </summary>
        /// <param name="key">移除的按键</param>
        public static bool RemoveUp(Keys key)
        {
            return handledKeysUp.Remove(key);
        }

        /// <summary>
        /// 移除一个键的钩子处理
        /// </summary>
        /// <param name="key">移除的按键</param>
        public static bool Remove(Keys key)
        {
            return RemoveDown(key);
        }

        private static bool OnKeyDown(Keys key)
        {
            if (KeyDown != null)
                return KeyDown(key);
            if (handledKeysDown.ContainsKey(key))
                return handledKeysDown[key]();
            else
                return true;
        }

        private static bool OnKeyUp(Keys key)
        {
            if (handledKeysUp.ContainsKey(key))
                return handledKeysUp[key]();
            else
                return true;
        }

        /// <summary>
        /// 返回一个给定的键基于当前的控制键的字符串表示形式。
        /// </summary>
        /// <param name="key">当前的键</param>
        /// <returns></returns>
        public static string KeyToString(Keys key)
        {
            return (KeyboardHook.Control ? "Ctrl + " : "") +
                            (KeyboardHook.Alt ? "Alt + " : "") +
                            (KeyboardHook.Shift ? "Shift + " : "") +
                            (KeyboardHook.Win ? "Win + " : "") +
                            key.ToString();
        }
    }
}
