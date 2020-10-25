using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using JCodes.Framework.jCodesenum;
using JCodes.Framework.jCodesenum.BaseEnum;

namespace JCodes.Framework.Common.Winform
{
    /// <summary>
    /// 实现系统全局热键注册辅助类
    /// </summary>
    public class RegisterHotKeyHelper
    {
        private IntPtr m_WindowHandle = IntPtr.Zero;
        private MODKEY m_ModKey = 0;
        private Keys m_Keys = Keys.A;
        private int m_WParam = 10000;
        private bool Star = false;
        private HotKeyWndProc m_HotKeyWnd = new HotKeyWndProc();

        /// <summary>
        /// 主窗体句柄
        /// </summary>
        public IntPtr WindowHandle
        {
            get { return m_WindowHandle; }
            set { if (Star)return; m_WindowHandle = value; }
        }

        /// <summary>
        /// 系统控制键
        /// </summary>
        public MODKEY ModKey
        {
            get { return m_ModKey; }
            set { if (Star)return; m_ModKey = value; }
        }

        /// <summary>
        /// 系统支持的键
        /// </summary>
        public Keys Keys
        {
            get { return m_Keys; }
            set { if (Star)return; m_Keys = value; }
        }

        /// <summary>
        /// 定义热键的参数,建议从10000开始
        /// </summary>
        public int WParam
        {
            get { return m_WParam; }
            set { if (Star)return; m_WParam = value; }
        }

        /// <summary>
        /// 开始注册系统全局热键
        /// </summary>
        public void StarHotKey()
        {
            if (m_WindowHandle != IntPtr.Zero)
            {
                if (!RegisterHotKey(m_WindowHandle, m_WParam, m_ModKey, m_Keys))
                {
                    throw new Exception("注册热键失败");
                }
                try
                {
                    m_HotKeyWnd.m_HotKeyPass = new HotKeyPass(KeyPass);
                    m_HotKeyWnd.m_WParam = m_WParam;
                    m_HotKeyWnd.AssignHandle(m_WindowHandle);
                    Star = true;
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(RegisterHotKeyHelper));
                    StopHotKey();
                }
            }
        }

        private void KeyPass()
        {
            if (HotKey != null) HotKey();
        }

        /// <summary>
        /// 取消系统全局热键
        /// </summary>
        public void StopHotKey()
        {
            if (Star)
            {
                if (!UnregisterHotKey(m_WindowHandle, m_WParam))
                {
                    throw new Exception("取消热键失败");
                }
                Star = false;
                m_HotKeyWnd.ReleaseHandle();
            }
        }

        /// <summary>
        /// 热键处理代理定义
        /// </summary>
        public delegate void HotKeyPass();

        /// <summary>
        /// 热键处理事件
        /// </summary>
        public event HotKeyPass HotKey;

        private class HotKeyWndProc : NativeWindow
        {
            public int m_WParam = 10000;
            public HotKeyPass m_HotKeyPass;
            protected override void WndProc(ref Message m)
            {
                //0x0312 热键消息
                if (m.Msg == 0x0312 && m.WParam.ToInt32() == m_WParam)
                {
                    if (m_HotKeyPass != null) m_HotKeyPass.Invoke();
                }

                base.WndProc(ref m);
            }
        }

        /// <summary>
        /// 注册热键
        /// </summary>
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr wnd, int id, MODKEY mode, Keys vk);

        /// <summary>
        /// 取消热键
        /// </summary>
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr wnd, int id);
    }
}
