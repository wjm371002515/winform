using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace JCodes.Framework.Common
{
    /// <summary>
    /// 提供一些不在.NET框架中的Window功能操作辅助类
    /// 注意: 该类是非线程安全的
    /// </summary>
    public class Win32Window
    {
        #region Static Members
        private static ArrayList topLevelWindows = null;
        private static ArrayList applicationWindows = null;
        private static Image myImage = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
            Screen.PrimaryScreen.Bounds.Height);

        #endregion Static Members

        #region Static Public

        /// <summary>
        /// 获取所有最顶层窗体
        /// </summary>
        public static ArrayList TopLevelWindows
        {
            get
            {
                topLevelWindows = new ArrayList();
                EnumWindows(new EnumWindowsProc(EnumerateTopLevelProc), 0);
                ArrayList top = topLevelWindows;
                topLevelWindows = null;
                return top;
            }
        }

        /// <summary>
        /// 获取所有应用程序窗体
        /// </summary>
        public static ArrayList ApplicationWindows
        {
            get
            {
                applicationWindows = new ArrayList();
                EnumWindows(new EnumWindowsProc(EnumerateApplicationCallback), 0);
                ArrayList apps = applicationWindows;
                applicationWindows = null;
                return apps;
            }
        }

        /// <summary>
        /// 返回给定线程的所有窗体
        /// </summary>
        /// <param name="threadId">线程ID</param>
        public static ArrayList GetThreadWindows(int threadId)
        {
            topLevelWindows = new ArrayList();
            EnumThreadWindows(threadId, new EnumWindowsProc(EnumerateThreadProc), 0);
            ArrayList windows = topLevelWindows;
            topLevelWindows = null;
            return windows;
        }

        /// <summary>
        /// 获取桌面窗体对象
        /// </summary>
        public static Win32Window DesktopWindow
        {
            get
            {
                Win32Window window = new Win32Window(GetDesktopWindow());
                window.Name = "Desktop";
                return window;
            }
        }

        /// <summary>
        /// 获取当前前置窗体对象
        /// </summary>
        public static Win32Window ForegroundWindow
        {
            get
            {
                return new Win32Window(GetForegroundWindow());
            }
        }


        /// <summary>
        /// 通过窗体类名或者窗体名称查找窗体对象
        /// </summary>
        /// <param name="className">类名，可为null</param>
        /// <param name="windowName">窗体名称，可为null</param>
        /// <returns></returns>
        public static Win32Window FindWindow(string className, string windowName)
        {
            return new Win32Window(FindWindowWin32(className, windowName));
        }

        /// <summary>
        /// 判断某窗体是否是给定窗体的子窗体
        /// </summary>
        /// <param name="parent">父窗体对象</param>
        /// <param name="window">待检查的窗体对象</param>
        /// <returns></returns>
        public static bool IsChild(Win32Window parent, Win32Window window)
        {
            return IsChild(parent.window, window.window);
        }

        /// <summary>
        /// 桌面截图
        /// </summary>
        public static Image DesktopAsBitmap
        {
            get
            {
                Graphics gr1 = Graphics.FromImage(myImage);
                IntPtr dc1 = gr1.GetHdc();
                IntPtr dc2 = GetWindowDC(GetDesktopWindow());
                BitBlt(dc1, 0, 0, Screen.PrimaryScreen.Bounds.Width,
                    Screen.PrimaryScreen.Bounds.Height, dc2, 0, 0, Const.SRCCOPY);
                gr1.ReleaseHdc(dc1);
                gr1.Dispose();
                return myImage;
            }
        }

        #endregion Static Public

        #region Static Private

        private delegate bool EnumWindowsProc(IntPtr window, int i);

        private static bool EnumerateThreadProc(IntPtr window, int i)
        {
            topLevelWindows.Add(new Win32Window(window));
            return (true);
        }

        private static bool EnumerateApplicationCallback(IntPtr pWindow, int i)
        {
            Win32Window window = new Win32Window(pWindow);

            if (window.Parent.window != IntPtr.Zero) return (true);
            if (window.Visible != true) return (true);
            if (window.Text == string.Empty) return (true);
            if (window.ClassName.Substring(0, 8) == "IDEOwner") return (true); // Skip invalid VS.Net 2003 windows

            applicationWindows.Add(window);
            return (true);
        }

        private static bool EnumerateTopLevelProc(IntPtr window, int i)
        {
            topLevelWindows.Add(new Win32Window(window));
            return (true);
        }


        #endregion Static Private

        #region Static Externs Private

        [DllImport("user32.dll")]
        private static extern bool BringWindowToTop(IntPtr window);

        [DllImport("user32.dll")]
        private static extern IntPtr FindWindowEx(IntPtr parent, IntPtr childAfter, string className, string windowName);

        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        private static extern IntPtr FindWindowWin32(string className, string windowName);

        [DllImport("user32.dll ")]
        public static extern int GetClassName(IntPtr hWnd, [Out] StringBuilder className, int maxCount);

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr window, int message, int wparam, int lparam);

        [DllImport("user32", CharSet = CharSet.Auto)]
        private extern static int SendMessage(IntPtr hWnd, int wMsg, int wParam, string lpstring);

        [DllImport("user32.dll")]
        private static extern int PostMessage(IntPtr window, int message, int wparam, int lparam);

        [DllImport("User32.dll", EntryPoint = "PostMessage")]
        private static extern int PostMessage(IntPtr hWnd, int Msg, IntPtr wParam, string lParam);

        [DllImport("user32.dll")]
        private static extern IntPtr GetParent(IntPtr window);

        [DllImport("user32.dll")]
        private static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern IntPtr GetLastActivePopup(IntPtr window);

        [DllImport("user32.dll")]
        private static extern int GetWindowText(IntPtr window, [In][Out] StringBuilder text, int copyCount);

        [DllImport("user32.dll")]
        public static extern bool SetWindowText(IntPtr window, [MarshalAs(UnmanagedType.LPTStr)] string text);

        [DllImport("user32.dll")]
        private static extern int GetWindowTextLength(IntPtr window);

        [DllImport("user32.dll")]
        private static extern int GetWindowClassNameLength(IntPtr window);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr window, int index, int value);

        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr window, int index);

        [DllImport("user32.dll")]
        private static extern int IsWindowVisible(IntPtr hwnd);

        [DllImport("user32.dll")]
        private static extern bool EnumChildWindows(IntPtr window, EnumWindowsProc callback, int i);

        [DllImport("user32.dll")]
        private static extern bool EnumThreadWindows(int threadId, EnumWindowsProc callback, int i);

        [DllImport("user32.dll")]
        private static extern bool EnumWindows(EnumWindowsProc callback, int i);

        [DllImport("user32.dll")]
        private static extern int GetWindowThreadProcessId(IntPtr window, ref int processId);

        [DllImport("user32.dll")]
        private static extern int GetWindowThreadProcessId(IntPtr window, IntPtr ptr);

        [DllImport("user32.dll")]
        private static extern bool GetWindowPlacement(IntPtr window, ref WindowPlacement position);

        [DllImport("user32.dll")]
        private static extern bool IsChild(IntPtr parent, IntPtr window);

        [DllImport("user32.dll")]
        private static extern bool IsIconic(IntPtr window);

        [DllImport("user32.dll")]
        private static extern bool IsZoomed(IntPtr window);

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hwnd, ref Rect rectangle);

        [DllImport("user32.dll")]
        private static extern bool GetClientRect(IntPtr hwnd, ref Rect rectangle);

        [DllImport("gdi32.dll")]
        private static extern UInt64 BitBlt(IntPtr hDestDC, int x, int y, int nWidth, int nHeight,
            IntPtr hSrcDC, int xSrc, int ySrc, System.Int32 dwRop);

        [DllImport("user32.dll")]
        private static extern bool GetWindowInfo(IntPtr hwnd, ref WindowInfo info);

        [DllImport("User32.dll")]
        private static extern bool PrintWindow(IntPtr windowHandle, IntPtr deviceContextHandle, UInt32 flags);

        #endregion Externs

        #region Members

        private IntPtr window;
        private ArrayList windowList = null;

        private string name = null;

        #endregion Members

        #region Public

        /// <summary>
        /// 窗体名称
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// ToString实现
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string name = this.name;

            if (name == null)
            {
                name = Text;
            }

            return name;
        }

        /// <summary>
        /// 给定句柄构造一个窗体对象
        /// </summary>
        /// <param name="window">窗口句柄</param>
        public Win32Window(IntPtr window)
        {
            this.window = window;
        }

        /// <summary>
        /// 获取窗体对象的句柄
        /// </summary>
        public IntPtr Window
        {
            get
            {
                return window;
            }
        }

        /// <summary>
        /// 如果窗体为空，返回True
        /// </summary>
        public bool IsNull
        {
            get
            {
                return window == IntPtr.Zero;
            }
        }

        /// <summary>
        /// 窗体对象的子窗体ArrayList集合
        /// </summary>
        public ArrayList Children
        {
            get
            {
                windowList = new ArrayList();
                EnumChildWindows(window, new EnumWindowsProc(EnumerateChildProc), 0);
                ArrayList children = windowList;
                windowList = null;
                return children;
            }
        }

        /// <summary>
        /// 设置窗体到顶端位置
        /// </summary>
        public void BringWindowToTop()
        {
            BringWindowToTop(window);
            System.Threading.Thread.Sleep(500);
        }

        /// <summary>
        /// 根据类名或窗体名称，查找其下面的子窗体对象
        /// </summary>
        /// <param name="className">类名，可为null</param>
        /// <param name="windowName">窗体名称，可为null</param>
        /// <returns></returns>
        public Win32Window FindChild(string className, string windowName)
        {
            return new Win32Window(
                FindWindowEx(window, IntPtr.Zero, className, windowName));
        }
        /// <summary>
        /// 发送窗体消息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="wparam"></param>
        /// <param name="lparam"></param>
        /// <returns></returns>
        public int SendMessage(int message, int wparam, int lparam)
        {
            return SendMessage(window, message, wparam, lparam);
        }

        /// <summary>
        /// Post消息到窗体对象
        /// </summary>
        /// <param name="message"></param>
        /// <param name="wparam"></param>
        /// <param name="lparam"></param>
        /// <returns></returns>
        public int PostMessage(int message, int wparam, int lparam)
        {
            return PostMessage(window, message, wparam, lparam);
        }


        /// <summary>
        /// 发送窗体消息
        /// </summary>
        /// <param name="wMsg"></param>
        /// <param name="wParam"></param>
        /// <param name="lpstring"></param>
        /// <returns></returns>
        public int SendMessage(int wMsg, int wParam, string lpstring)
        {
            return SendMessage(window, wMsg, wParam, lpstring);
        }

        /// <summary>
        /// 模拟按钮单击/双击指定的窗口位置，以便触发相关事件（使用SendMessage方式）
        /// </summary>
        /// <param name="button">left,right左键或者右键</param>
        /// <param name="x">X坐标位置</param>
        /// <param name="y">Y坐标位置</param>
        /// <param name="doubleklick">是否为双击</param>
        public void ClickWindow(string button, int x, int y, bool doubleklick)
        {
            int LParam = MakeLParam(x, y);

            int btnDown = 0;
            int btnUp = 0;

            if (button == "left")
            {
                btnDown = (int)Const.WM_LBUTTONDOWN;
                btnUp = (int)Const.WM_LBUTTONUP;
            }

            if (button == "right")
            {
                btnDown = (int)Const.WM_RBUTTONDOWN;
                btnUp = (int)Const.WM_RBUTTONUP;
            }

            if (doubleklick == true)
            {
                SendMessage(window, btnDown, 0, LParam);
                SendMessage(window, btnUp, 0, LParam);
                SendMessage(window, btnDown, 0, LParam);
                SendMessage(window, btnUp, 0, LParam);
            }
            else
            {
                SendMessage(window, btnDown, 0, LParam);
                SendMessage(window, btnUp, 0, LParam);
            }
        }

        /// <summary>
        /// 模拟按钮单击/双击指定的窗口位置，以便触发相关事件（使用PostMessage方式）
        /// </summary>
        /// <param name="button">left,right左键或者右键</param>
        /// <param name="x">X坐标位置</param>
        /// <param name="y">Y坐标位置</param>
        /// <param name="doubleklick">是否为双击</param>
        public void ClickWindow_Post(string button, int x, int y, bool doubleklick)
        {
            int LParam = MakeLParam(x, y);

            int btnDown = 0;
            int btnUp = 0;

            if (button == "left")
            {
                btnDown = (int)Const.WM_LBUTTONDOWN;
                btnUp = (int)Const.WM_LBUTTONUP;
            }

            if (button == "right")
            {
                btnDown = (int)Const.WM_RBUTTONDOWN;
                btnUp = (int)Const.WM_RBUTTONUP;
            }

            if (doubleklick == true)
            {
                PostMessage(window, btnDown, 0, LParam);
                PostMessage(window, btnUp, 0, LParam);
                PostMessage(window, btnDown, 0, LParam);
                PostMessage(window, btnUp, 0, LParam);
            }
            else
            {
                PostMessage(window, btnDown, 0, LParam);
                PostMessage(window, btnUp, 0, LParam);
            }
        }

        /// <summary>
        /// 构造消息参数
        /// </summary>
        private int MakeLParam(int LoWord, int HiWord)
        {
            return ((HiWord << 16) | (LoWord & 0xffff));
        }

        /// <summary>
        /// 获取窗体的父窗体对象，如果为null则表示是最顶层窗体
        /// </summary>
        public Win32Window Parent
        {
            get
            {
                return new Win32Window(GetParent(window));
            }
        }

        /// <summary>
        /// 获取最顶端的弹出窗体对象
        /// </summary>
        public Win32Window LastActivePopup
        {
            get
            {
                IntPtr popup = GetLastActivePopup(window);
                if (popup == window)
                    return new Win32Window(IntPtr.Zero);
                else
                    return new Win32Window(popup);
            }
        }

        /// <summary>
        /// 窗体的标题信息
        /// </summary>
        public string Text
        {
            get
            {
                int length = GetWindowTextLength(window);
                StringBuilder sb = new StringBuilder(length + 1);
                GetWindowText(window, sb, sb.Capacity);
                return sb.ToString();
            }
            set
            {
                SetWindowText(window, value);
            }
        }

        /// <summary>
        /// 窗体对象的类名
        /// </summary>
        public string ClassName
        {
            get
            {
                StringBuilder builder1 = new StringBuilder(0x100);
                int num1 = GetClassName(window, builder1, 0x100);
                return builder1.ToString(0, num1);
            }
        }

        /// <summary>
        /// 获取Window窗体的Long Value
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public int GetWindowLong(int index)
        {
            return GetWindowLong(window, index);
        }

        /// <summary>
        /// 设置Window窗体的Long Value
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public int SetWindowLong(int index, int value)
        {
            return SetWindowLong(window, index, value);
        }

        /// <summary>
        /// 该窗体对象的线程ID
        /// </summary>
        public int ThreadId
        {
            get
            {
                return GetWindowThreadProcessId(window, IntPtr.Zero);
            }
        }

        /// <summary>
        /// 该窗体对象的进程ID
        /// </summary>
        public int ProcessId
        {
            get
            {
                int processId = 0;
                GetWindowThreadProcessId(window, ref processId);
                return processId;
            }
        }

        /// <summary>
        /// 窗体的布局
        /// </summary>
        public WindowPlacement WindowPlacement
        {
            get
            {
                WindowPlacement placement = new WindowPlacement();
                GetWindowPlacement(window, ref placement);
                return placement;
            }
        }

        /// <summary>
        /// 判断窗体是否是最小化状态
        /// </summary>
        public bool Minimized
        {
            get
            {
                return IsIconic(window);
            }
        }

        /// <summary>
        /// 判断窗体是否可见
        /// </summary>
        public bool Visible
        {
            get
            {
                return IsWindowVisible(window) != 0 ? true : false;
            }
        }

        /// <summary>
        /// 判断窗体是否是最大化状态
        /// </summary>
        public bool Maximized
        {
            get
            {
                return IsZoomed(window);
            }
        }

        public void MakeToolWindow()
        {
            int windowStyle = GetWindowLong(Const.GWL_EXSTYLE);
            SetWindowLong(Const.GWL_EXSTYLE, windowStyle | Const.WS_EX_TOOLWINDOW);
        }

        /// <summary>
        /// Windows窗体转换为BitMap对象
        /// </summary>
        public Image WindowAsBitmap
        {
            get
            {
                if (IsNull)
                    return null;

                this.BringWindowToTop();

                Rect rect = new Rect();
                if (!GetWindowRect(window, ref rect))
                    return null;

                WindowInfo windowInfo = new WindowInfo();
                windowInfo.size = Marshal.SizeOf(typeof(WindowInfo));
                if (!GetWindowInfo(window, ref windowInfo))
                    return null;

                Image myImage = new Bitmap(rect.Width, rect.Height);
                Graphics gr1 = Graphics.FromImage(myImage);
                IntPtr dc1 = gr1.GetHdc();
                IntPtr dc2 = GetWindowDC(window);
                BitBlt(dc1, 0, 0, rect.Width, rect.Height, dc2, 0, 0, Const.SRCCOPY);
                gr1.ReleaseHdc(dc1);
                return myImage;
            }
        }

        /// <summary>
        /// Windows的客户区域转换为Bitmap对象
        /// </summary>
        public Image WindowClientAsBitmap
        {
            get
            {
                if (IsNull)
                    return null;

                this.BringWindowToTop();

                Rect rect = new Rect();
                if (!GetClientRect(window, ref rect))
                    return null;

                WindowInfo windowInfo = new WindowInfo();
                windowInfo.size = Marshal.SizeOf(typeof(WindowInfo));
                if (!GetWindowInfo(window, ref windowInfo))
                    return null;

                int xOffset = windowInfo.client.X - windowInfo.window.X;
                int yOffset = windowInfo.client.Y - windowInfo.window.Y;

                Image myImage = new Bitmap(rect.Width, rect.Height);
                Graphics gr1 = Graphics.FromImage(myImage);
                IntPtr dc1 = gr1.GetHdc();
                IntPtr dc2 = GetWindowDC(window);
                BitBlt(dc1, 0, 0, rect.Width, rect.Height, dc2, xOffset, yOffset, Const.SRCCOPY);
                gr1.ReleaseHdc(dc1);
                return myImage;
            }
        }


        #endregion Public

        private bool EnumerateChildProc(IntPtr window, int i)
        {
            windowList.Add(new Win32Window(window));
            return (true);
        }
        
        struct WindowInfo
        {
            public int size;
            public Rectangle window;
            public Rectangle client;
            public int style;
            public int exStyle;
            public int windowStatus;
            public uint xWindowBorders;
            public uint yWindowBorders;
            public short atomWindowtype;
            public short creatorVersion;
        }
    }
}
// like the Win32 rect type. Can't use Rectangle because it uses a different format...
public struct Rect
{
    public int left;
    public int top;
    public int right;
    public int bottom;

    public int Width
    {
        get { return right - left; }
    }

    public int Height
    {
        get { return bottom - top; }
    }
}

/// <summary>
/// Windows布局结构
/// </summary>
public struct WindowPlacement
{
    public int length;
    public int flags;
    public int showCmd;
    public Point minPosition;
    public Point maxPosition;
    public Rectangle normalPosition;
}