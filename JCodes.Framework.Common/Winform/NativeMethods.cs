using System;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.jCodesenum;

namespace JCodes.Framework.Common.Winform
{
    /// <summary>
    /// Contains InterOp method calls used by the application.
    /// </summary>
    public sealed class NativeMethods
    {
        /// private constructor, not intended for instantiation
        private NativeMethods()
        {
        }

        //PlaySound API can be used to play a wav file. long needs to be fixed according to mapping table.
        [DllImport("winmm.dll")]
        public static extern long PlaySound(String lpszName, long hModule, long dwFlags);

        [DllImport("User32.dll")]
        public static extern Boolean MessageBeep(UInt32 beepType);

        [DllImport("user32.dll")]
        internal static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        internal static extern bool IsIconic(IntPtr hWnd);

        internal static int SW_RESTORE = 9;

        /// <summary>
        /// 把指定的窗口提到最前面
        /// </summary>
        /// <param name="hWnd"></param>
        public static void BringToFront(IntPtr hWnd)
        {
            if (NativeMethods.IsIconic(hWnd))
            {
                NativeMethods.ShowWindowAsync(hWnd, NativeMethods.SW_RESTORE);
            }
            NativeMethods.SetForegroundWindow(hWnd);
        }


        [DllImport("user32.dll")]
        public static extern bool BringWindowToTop(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr CopyIcon(IntPtr hIcon);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DestroyIcon(IntPtr hIcon);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumChildWindows(IntPtr hwndParent, EnumWindowsProc lpEnumFunc, IntPtr lParam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumThreadWindows(uint dwThreadId, EnumWindowsProc lpfn, IntPtr lParam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll")]
        public static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll")]
        public static extern bool GetCursorInfo(out CursorInfo pci);

        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern bool GetIconInfo(IntPtr hIcon, out IconInfo piconinfo);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

        /// <summary>
        /// The GetNextWindow function retrieves a handle to the next or previous window in the Z-Order.
        /// The next window is below the specified window; the previous window is above.
        /// If the specified window is a topmost window, the function retrieves a handle to the next (or previous) topmost window.
        /// If the specified window is a top-level window, the function retrieves a handle to the next (or previous) top-level window.
        /// If the specified window is a child window, the function searches for a handle to the next (or previous) child window.
        /// </summary>
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindow(IntPtr hWnd, GetWindowConstants wCmd);

        [DllImport("user32.dll")]
        public static extern int GetSystemMetrics(int smIndex);

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern ulong GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll")]
        public static extern int GetWindowRgn(IntPtr hWnd, IntPtr hRgn);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowText(IntPtr hWnd, [Out] StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("User32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PrintWindow(IntPtr hwnd, IntPtr hDC, uint nFlags);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr SetActiveWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("kernel32.dll")]
        public static extern int ResumeThread(IntPtr hThread);

        [DllImport("kernel32.dll")]
        public static extern int SuspendThread(IntPtr hThread);

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenThread(ThreadAccess dwDesiredAccess, bool bInheritHandle, uint dwThreadId);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetProcessWorkingSetSize(IntPtr handle, IntPtr min, IntPtr max);

        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst,
           ref POINT pptDst, ref SIZE psize, IntPtr hdcSrc, ref POINT pptSrc, uint crKey,
           [In] ref BLENDFUNCTION pblend, uint dwFlags);

        public struct WINDOWPLACEMENT
        {
            public int length;
            public int flags;
            public int showCmd;
            public System.Drawing.Point ptMinPosition;
            public System.Drawing.Point ptMaxPosition;
            public System.Drawing.Rectangle rcNormalPosition;
        }

        public struct BLENDFUNCTION
        {
            public byte BlendOp;
            public byte BlendFlags;
            public byte SourceConstantAlpha;
            public byte AlphaFormat;
        }

        

        public static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        public static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        public static readonly IntPtr HWND_TOP = new IntPtr(0);

        [StructLayout(LayoutKind.Sequential)]
        public struct IconInfo
        {
            public bool fIcon;         // Specifies whether this structure defines an icon or a cursor. A value of TRUE specifies 
            public Int32 xHotspot;     // Specifies the x-coordinate of a cursor's hot spot. If this structure defines an icon, the hot 
            public Int32 yHotspot;     // Specifies the y-coordinate of the cursor's hot spot. If this structure defines an icon, the hot 
            public IntPtr hbmMask;     // (HBITMAP) Specifies the icon bitmask bitmap. If this structure defines a black and white icon, 
            public IntPtr hbmColor;    // (HBITMAP) Handle to the icon color bitmap. This member can be optional if this 
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct CursorInfo
        {
            public Int32 cbSize;        // Specifies the size, in bytes, of the structure. 
            public Int32 flags;         // Specifies the cursor state. This parameter can be one of the following values:
            public IntPtr hCursor;      // Handle to the cursor. 
            public Point ptScreenPos;   // A POINT structure that receives the screen coordinates of the cursor. 
        }

        [DllImport("shell32.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern uint SHAppBarMessage(int dwMessage, out APPBARDATA pData);

        public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        public static string GetWindowLabel()
        {
            IntPtr handle = GetForegroundWindow();
            StringBuilder sb = new StringBuilder(Const.numOfChars);

            if (GetWindowText(handle, sb, Const.numOfChars) > 0)
            {
                return sb.ToString();
            }

            return string.Empty;
        }

        public static IntPtr GetWindowHandle()
        {
            IntPtr handle = GetForegroundWindow();
            StringBuilder sb = new StringBuilder(Const.numOfChars);

            if (GetWindowText(handle, sb, Const.numOfChars) > 0)
            {
                return handle;
            }

            return IntPtr.Zero;
        }

        public static string GetClassName(IntPtr handle)
        {
            StringBuilder sb = new StringBuilder(Const.numOfChars);

            if (GetClassName(handle, sb, Const.numOfChars) > 0)
            {
                return sb.ToString();
            }

            return string.Empty;
        }

        public static MyCursor CaptureCursor()
        {
            CursorInfo cursorInfo = new CursorInfo();
            cursorInfo.cbSize = Marshal.SizeOf(cursorInfo);

            if (GetCursorInfo(out cursorInfo) && cursorInfo.flags == Const.CURSOR_SHOWING)
            {
                cursorInfo.ptScreenPos = NativeMethods.ConvertPoint(Cursor.Position);

                IntPtr hicon = CopyIcon(cursorInfo.hCursor);
                if (hicon != IntPtr.Zero)
                {
                    IconInfo iconInfo;
                    if (GetIconInfo(hicon, out iconInfo))
                    {
                        Point position = new Point(cursorInfo.ptScreenPos.X - iconInfo.xHotspot, cursorInfo.ptScreenPos.Y - iconInfo.yHotspot);

                        using (Bitmap maskBitmap = Bitmap.FromHbitmap(iconInfo.hbmMask))
                        {
                            Bitmap resultBitmap = null;

                            if (IsCursorMonochrome(maskBitmap))
                            {
                                resultBitmap = new Bitmap(maskBitmap.Width, maskBitmap.Width);

                                Graphics desktopGraphics = Graphics.FromHwnd(GetDesktopWindow());
                                IntPtr desktopHdc = desktopGraphics.GetHdc();

                                IntPtr maskHdc = GDI.CreateCompatibleDC(desktopHdc);
                                IntPtr oldPtr = GDI.SelectObject(maskHdc, maskBitmap.GetHbitmap());

                                using (Graphics resultGraphics = Graphics.FromImage(resultBitmap))
                                {
                                    IntPtr resultHdc = resultGraphics.GetHdc();

                                    // These two operation will result in a black cursor over a white background.
                                    // Later in the code, a call to MakeTransparent() will get rid of the white background.
                                    GDI.BitBlt(resultHdc, 0, 0, 32, 32, maskHdc, 0, 32, CopyPixelOperation.SourceCopy);
                                    GDI.BitBlt(resultHdc, 0, 0, 32, 32, maskHdc, 0, 0, CopyPixelOperation.SourceInvert);

                                    resultGraphics.ReleaseHdc(resultHdc);
                                }

                                IntPtr newPtr = GDI.SelectObject(maskHdc, oldPtr);
                                GDI.DeleteDC(newPtr);
                                GDI.DeleteDC(maskHdc);
                                desktopGraphics.ReleaseHdc(desktopHdc);

                                // Remove the white background from the BitBlt calls,
                                // resulting in a black cursor over a transparent background.
                                resultBitmap.MakeTransparent(Color.White);
                            }
                            else
                            {
                                resultBitmap = Icon.FromHandle(hicon).ToBitmap();
                            }

                            return new MyCursor(new Cursor(cursorInfo.hCursor), position, resultBitmap);
                        }
                    }
                }
            }

            return new MyCursor();
        }

        private static bool IsCursorMonochrome(Bitmap bmp)
        {
            bool isMonochrome = bmp.Height == bmp.Width * 2;

            Color white = Color.FromArgb(255, 255, 255, 255);

            for (int y = 0; y < bmp.Height / 2; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    isMonochrome &= bmp.GetPixel(x, y) == white;
                    if (!isMonochrome) return isMonochrome;
                }
            }

            return isMonochrome;
        }

        public static bool GetWindowRegion(IntPtr hWnd, out Region region)
        {
            IntPtr hRgn = GDI.CreateRectRgn(0, 0, 0, 0);
            RegionType regionType = (RegionType)GetWindowRgn(hWnd, hRgn);
            region = Region.FromHrgn(hRgn);
            return regionType != RegionType.ERROR && regionType != RegionType.NULLREGION;
        }

        public class MyCursor : IDisposable
        {
            public Cursor Cursor;
            public Point Position;
            public Bitmap Bitmap;

            public MyCursor()
            {
                this.Cursor = Cursor.Current;
                this.Position = new Point(Cursor.Position.X - this.Cursor.HotSpot.X, Cursor.Position.Y - this.Cursor.HotSpot.Y);
                this.Bitmap = Icon.FromHandle(this.Cursor.Handle).ToBitmap();
            }

            public MyCursor(Cursor cursor, Point position, Bitmap bitmap)
            {
                this.Cursor = cursor;
                this.Position = position;
                this.Bitmap = bitmap;
            }

            public void Dispose()
            {
                Cursor.Dispose();
                Bitmap.Dispose();
            }
        }

        [DllImport("dwmapi.dll")]
        public static extern int DwmGetWindowAttribute(IntPtr hwnd, int dwAttribute, out RECT pvAttribute, int cbAttribute);

        [DllImport("dwmapi.dll")]
        public static extern int DwmGetWindowAttribute(IntPtr hwnd, int dwAttribute, out bool pvAttribute, int cbAttribute);

        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hwnd, ref MARGINS margins);

        [StructLayout(LayoutKind.Sequential)]
        public struct MARGINS
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }

        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern bool DwmIsCompositionEnabled();

        [DllImport("dwmapi.dll")]
        public static extern void DwmEnableBlurBehindWindow(IntPtr hwnd, ref DWM_BLURBEHIND blurBehind);

        [StructLayout(LayoutKind.Sequential)]
        public struct DWM_BLURBEHIND
        {
            public DWM_BB dwFlags;
            public bool fEnable;
            public IntPtr hRgnBlur;
            public bool fTransitionOnMaximized;
        }

        [DllImport("dwmapi.dll")]
        public static extern int DwmRegisterThumbnail(IntPtr dest, IntPtr src, out IntPtr thumb);

        [DllImport("dwmapi.dll")]
        public static extern int DwmUnregisterThumbnail(IntPtr thumb);

        [DllImport("dwmapi.dll")]
        public static extern int DwmQueryThumbnailSourceSize(IntPtr thumb, out SIZE size);

        [DllImport("dwmapi.dll")]
        public static extern int DwmUpdateThumbnailProperties(IntPtr hThumb, ref DWM_THUMBNAIL_PROPERTIES props);

        public static readonly int DWM_TNP_RECTDESTINATION = 0x1;
        public static readonly int DWM_TNP_OPACITY = 0x4;
        public static readonly int DWM_TNP_VISIBLE = 0x8;

        [DllImport("dwmapi.dll")]
        public static extern int DwmSetDxFrameDuration(IntPtr hwnd, uint cRefreshes);

        [StructLayout(LayoutKind.Sequential)]
        public struct DWM_THUMBNAIL_PROPERTIES
        {
            public int dwFlags;
            public RECT rcDestination;
            public RECT rcSource;
            public byte opacity;
            public bool fVisible;
            public bool fSourceClientAreaOnly;
        }
        [Serializable, StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;

            public RECT(int left_, int top_, int right_, int bottom_)
            {
                Left = left_;
                Top = top_;
                Right = right_;
                Bottom = bottom_;
            }

            public int Height { get { return Bottom - Top; } }
            public int Width { get { return Right - Left; } }

            public Size Size { get { return new Size(Width, Height); } }
            public Point Location { get { return new Point(Left, Top); } }

            public Rectangle ToRectangle()
            {
                return Rectangle.FromLTRB(Left, Top, Right, Bottom);
            }

            public static RECT FromRectangle(Rectangle rectangle)
            {
                return new RECT(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom);
            }

            public override int GetHashCode()
            {
                return Left ^ ((Top << 13) | (Top >> 0x13))
                  ^ ((Width << 0x1a) | (Width >> 6))
                  ^ ((Height << 7) | (Height >> 0x19));
            }

            #region Operator overloads

            public static implicit operator Rectangle(RECT rect)
            {
                return rect.ToRectangle();
            }

            public static implicit operator RECT(Rectangle rect)
            {
                return FromRectangle(rect);
            }

            #endregion
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SIZE
        {
            public int x;
            public int y;

            public SIZE(int width, int height)
            {
                x = width;
                y = height;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public static explicit operator Point(POINT p)
            {
                return new Point(p.X, p.Y);
            }

            public static explicit operator POINT(Point p)
            {
                return new POINT(p.X, p.Y);
            }
        }

        public static bool GetExtendedFrameBounds(IntPtr handle, out Rectangle rectangle)
        {
            RECT rect;
            int result = DwmGetWindowAttribute(handle, (int)DwmWindowAttribute.ExtendedFrameBounds, out rect, Marshal.SizeOf(typeof(RECT)));
            rectangle = rect.ToRectangle();
            return result >= 0;
        }

        public static bool DWMWA_NCRENDERING_ENABLED(IntPtr handle)
        {
            bool enabled;
            int result = DwmGetWindowAttribute(handle, (int)DwmWindowAttribute.NCRenderingEnabled, out enabled, sizeof(bool));
            //if (result < 0) throw new Exception("Error: DWMWA_NCRENDERING_ENABLED");
            return enabled;
        }

        public static void SetDWMWindowAttributeNCRenderingPolicy(IntPtr handle, DwmNCRenderingPolicy renderingPolicy)
        {
            int renderPolicy = (int)renderingPolicy;
            DwmSetWindowAttribute(handle, (int)DwmWindowAttribute.NCRenderingPolicy, ref renderPolicy, sizeof(int));
        }

        public static Rectangle GetWindowRect(IntPtr handle)
        {
            RECT rect;
            GetWindowRect(handle, out rect);
            return rect.ToRectangle();
        }

        public static Rectangle GetWindowRectangle(IntPtr handle)
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                Rectangle rect;
                if (GetExtendedFrameBounds(handle, out rect))
                {
                    return rect;
                }
            }

            return GetWindowRect(handle);
        }

        public static Rectangle MaximizedWindowFix(IntPtr handle, Rectangle windowRect)
        {
            if (NativeMethods.IsWindowMaximized(handle))
            {
                Rectangle screenRect = Screen.FromRectangle(windowRect).Bounds;

                if (windowRect.X < screenRect.X)
                {
                    windowRect.Width -= (screenRect.X - windowRect.X) * 2;
                    windowRect.X = screenRect.X;
                }

                if (windowRect.Y < screenRect.Y)
                {
                    windowRect.Height -= (screenRect.Y - windowRect.Y) * 2;
                    windowRect.Y = screenRect.Y;
                }

                windowRect.Intersect(screenRect);
            }

            return windowRect;
        }

        public static void ActivateWindow(IntPtr handle)
        {
            SetForegroundWindow(handle);
            SetActiveWindow(handle);
        }

        public static void ActivateWindowRepeat(IntPtr handle, int count)
        {
            for (int i = 0; NativeMethods.GetForegroundWindow() != handle && i < count; i++)
            {
                NativeMethods.BringWindowToTop(handle);
                Thread.Sleep(1);
                Application.DoEvents();
            }
        }

        #region Taskbar

        [StructLayout(LayoutKind.Sequential)]
        public struct APPBARDATA
        {
            public int cbSize;
            public IntPtr hWnd;
            public int uCallbackMessage;
            public int uEdge;
            public RECT rc;
            public IntPtr lParam;
        }

        public static Rectangle GetTaskbarRectangle()
        {
            APPBARDATA abd = new APPBARDATA();
            SHAppBarMessage((int)ABMsg.ABM_GETTASKBARPOS, out abd);
            return abd.rc.ToRectangle();
        }

        /// <summary>
        /// Method returns information about the Window's TaskBar.
        /// </summary>
        /// <param name="taskBarEdge">Location of the TaskBar(Top,Bottom,Left,Right).</param>
        /// <param name="height">Height of the TaskBar.</param>
        /// <param name="autoHide">AutoHide property of the TaskBar.</param>
        private static void GetTaskBarInfo(out TaskBarEdge taskBarEdge, out int height, out bool autoHide)
        {
            APPBARDATA abd = new APPBARDATA();

            height = 0;
            taskBarEdge = TaskBarEdge.Bottom;
            autoHide = false;

            uint ret = SHAppBarMessage((int)ABMsg.ABM_GETTASKBARPOS, out abd);
            switch (abd.uEdge)
            {
                case (int)ABEdge.ABE_BOTTOM:
                    taskBarEdge = TaskBarEdge.Bottom;
                    height = abd.rc.Height;
                    break;
                case (int)ABEdge.ABE_TOP:
                    taskBarEdge = TaskBarEdge.Top;
                    height = abd.rc.Bottom;
                    break;
                case (int)ABEdge.ABE_LEFT:
                    taskBarEdge = TaskBarEdge.Left;
                    height = abd.rc.Width;
                    break;
                case (int)ABEdge.ABE_RIGHT:
                    taskBarEdge = TaskBarEdge.Right;
                    height = abd.rc.Width;
                    break;
            }

            abd = new APPBARDATA();
            uint uState = SHAppBarMessage((int)ABMsg.ABM_GETSTATE, out abd);
            switch (uState)
            {
                case (int)ABState.ABS_ALWAYSONTOP:
                    autoHide = false;
                    break;
                case (int)ABState.ABS_AUTOHIDE:
                    autoHide = true;
                    break;
                case (int)ABState.ABS_AUTOHIDEANDONTOP:
                    autoHide = true;
                    break;
                case (int)ABState.ABS_MANUAL:
                    autoHide = false;
                    break;
            }
        }

        #endregion

        public static void TrimMemoryUse()
        {
            System.GC.Collect();
            System.GC.WaitForPendingFinalizers();
            SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, (IntPtr)(-1), (IntPtr)(-1));
        }

        public static bool IsWindowMaximized(IntPtr handle)
        {
            NativeMethods.WINDOWPLACEMENT wp = new NativeMethods.WINDOWPLACEMENT();
            NativeMethods.GetWindowPlacement(handle, ref wp);
            return wp.showCmd == (int)SHOWWINDOW.SW_MAXIMIZE;
        }

        public static Point ConvertPoint(Point p)
        {
            int x = 0, y = 0;

            foreach (Screen screen in Screen.AllScreens)
            {
                x = Math.Min(x, screen.Bounds.X);
                y = Math.Min(y, screen.Bounds.Y);
            }

            return new Point(p.X - x, p.Y - y);
        }
    }
}
