using System;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;
using JCodes.Framework.Common;
using System.IO;

namespace JCodes.Framework.Common
{
    /// <summary>
    /// 抓取整个屏幕或指定窗口，并可保存到文件的操作类
    /// </summary>
    public class ScreenCapture
    {
        private string m_ImageSavePath = "C:\\CaptureImages";
        private string m_ImageExtension = "";
        private ImageFormat m_ImageFormat = ImageFormat.Png;//图片保存的格式

        /// <summary>
        /// 图片保存的路径，默认为C:\\CaptureImages
        /// </summary>
        public string ImageSavePath
        {
            get { return this.m_ImageSavePath; }
            set { this.m_ImageSavePath = value; }
        }

        /// <summary>
        /// 图片的后缀名，如果为空，使用图片格式作为后缀名
        /// </summary>
        public string ImageExtension
        {
            get { return this.m_ImageExtension; }
            set { this.m_ImageExtension = value; }
        }

        /// <summary>
        /// 图片保存的格式
        /// </summary>
        public ImageFormat ImageFormat
        {
            get { return this.m_ImageFormat; }
            set { this.m_ImageFormat = value; }
        }

        /// <summary>
        /// 抓取桌面整个屏幕截图到一个图片对象中
        /// </summary>
        /// <returns></returns>
        public Image CaptureScreen()
        {
            return CaptureWindow(User32.GetDesktopWindow());
        }

        /// <summary>
        /// 抓取桌面整个指定窗口的截图到一个图片对象中
        /// </summary>
        /// <param name="handle">指定窗口的句柄</param>
        /// <returns></returns>
        public Image CaptureWindow(IntPtr handle)
        {
            // get te hDC of the target window
            IntPtr hdcSrc = User32.GetWindowDC(handle);
            // get the size
            User32.RECT windowRect = new User32.RECT();
            User32.GetWindowRect(handle, ref windowRect);
            int width = windowRect.right - windowRect.left;
            int height = windowRect.bottom - windowRect.top;
            // create a device context we can copy to
            IntPtr hdcDest = GDI32.CreateCompatibleDC(hdcSrc);
            // create a bitmap we can copy it to,
            // using GetDeviceCaps to get the width/height
            IntPtr hBitmap = GDI32.CreateCompatibleBitmap(hdcSrc, width, height);
            // select the bitmap object
            IntPtr hOld = GDI32.SelectObject(hdcDest, hBitmap);
            // bitblt over
            GDI32.BitBlt(hdcDest, 0, 0, width, height, hdcSrc, 0, 0, Const.SRCCOPY);
            // restore selection
            GDI32.SelectObject(hdcDest, hOld);
            // clean up
            GDI32.DeleteDC(hdcDest);
            User32.ReleaseDC(handle, hdcSrc);
            // get a .NET image object for it
            Image img = Image.FromHbitmap(hBitmap);
            // free up the Bitmap object
            GDI32.DeleteObject(hBitmap);
            return img;
        }

        /// <summary>
        /// 抓取桌面整个指定窗口的截图，并保存到文件中
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="filename"></param>
        /// <param name="format"></param>
        public void CaptureWindowToFile(IntPtr handle, string filename, ImageFormat format)
        {
            Image img = CaptureWindow(handle);
            img.Save(filename, format);
        }

        /// <summary>
        /// 根据属性配置自动保存图片
        /// </summary>
        public void AutoCaptureScreen()
        {
            DirectoryUtil.AssertDirExist(this.ImageSavePath);
            if (DirectoryUtil.IsExistDirectory(this.ImageSavePath))
            {
                string SubPath = Path.Combine(this.ImageSavePath, DateTime.Now.ToString("yyyy-MM-dd"));
                DirectoryUtil.CreateDirectory(SubPath);

                DateTime snapTime = DateTime.Now;
                string baseFilename = snapTime.ToString("yyyy_MM_dd-HH_mm_ss");
                string fullFilename = Path.Combine(SubPath, baseFilename);
                if (!string.IsNullOrEmpty(this.ImageExtension.Trim('.')))
                {
                    fullFilename += "." + this.ImageExtension.Trim('.');
                }
                else
                {
                    fullFilename += "." + this.ImageFormat.ToString();
                }
                CaptureScreenToFile(fullFilename, ImageFormat);
            }
        }

        /// <summary>
        /// 抓取桌面整个指定窗口的截图，并保存到文件中
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="format"></param>
        public void CaptureScreenToFile(string filename, ImageFormat format)
        {
            Image img = CaptureScreen();
            img.Save(filename, format);
        }

        /// <summary>
        /// Helper class containing Gdi32 API functions
        /// </summary>
        private class GDI32
        {
            [DllImport("gdi32.dll")]
            public static extern bool BitBlt(IntPtr hObject, int nXDest, int nYDest,
               int nWidth, int nHeight, IntPtr hObjectSource,
               int nXSrc, int nYSrc, int dwRop);
            [DllImport("gdi32.dll")]
            public static extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int nWidth,
               int nHeight);
            [DllImport("gdi32.dll")]
            public static extern IntPtr CreateCompatibleDC(IntPtr hDC);
            [DllImport("gdi32.dll")]
            public static extern bool DeleteDC(IntPtr hDC);
            [DllImport("gdi32.dll")]
            public static extern bool DeleteObject(IntPtr hObject);
            [DllImport("gdi32.dll")]
            public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);
        }

        /// <summary>
        /// Helper class containing User32 API functions
        /// </summary>
        private class User32
        {
            [StructLayout(LayoutKind.Sequential)]
            public struct RECT
            {
                public int left;
                public int top;
                public int right;
                public int bottom;
            }
            [DllImport("user32.dll")]
            public static extern IntPtr GetDesktopWindow();
            [DllImport("user32.dll")]
            public static extern IntPtr GetWindowDC(IntPtr hWnd);
            [DllImport("user32.dll")]
            public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);
            [DllImport("user32.dll")]
            public static extern IntPtr GetWindowRect(IntPtr hWnd, ref RECT rect);
        }
    }
}
