using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using JCodes.Framework.Common;
using System.Windows.Forms;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.Common.Winform;

namespace JCodes.Framework.Common.Images
{
    /// <summary>
    /// 位图抓取处理委托
    /// </summary>
    /// <returns></returns>
	public delegate Bitmap CaptureHandleDelegateHandler( IntPtr handle );

	/// <summary>
    /// 实现窗体、完整桌面的屏幕抓取，抓取的图片可以保存为各种不同的图片格式，并可以打印。
	/// </summary>
	public class MyScreenCapture
	{
        #region 属性字段
        /// <summary>
        /// 打印的实际图片
        /// </summary>
        private Bitmap image;

        /// <summary>
        /// 所有截图集合
        /// </summary>
        private Bitmap[] images = null;

        /// <summary>
        /// 打印截图对象的打印文档对象
        /// </summary>
        private PrintDocument doc = new PrintDocument();

        /// <summary>
        /// 不同图片格式的处理器
        /// </summary>
        private ImageFormatHandler formatHandler = null;

        /// <summary>
        /// 定义图片处理器
        /// </summary>
        public ImageFormatHandler FormatHandler
        {
            set { formatHandler = value; }
        } 
        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public MyScreenCapture()
        {
            doc.PrintPage += new PrintPageEventHandler(printPage);
            formatHandler = new ImageFormatHandler();
        }

        /// <summary>
        /// 构造函数，并指定图片格式处理器
        /// </summary>
        /// <param name="formatHandler">并指定图片格式处理器</param>
        public MyScreenCapture(ImageFormatHandler formatHandler)
        {
            doc.PrintPage += new PrintPageEventHandler(printPage);

            this.formatHandler = formatHandler;
        } 
        #endregion

        #region 抓取窗口截图

        /// <summary>
        /// 截取指定的窗体并保存到指定的文件中
        /// </summary>
        /// <param name="window">待截图的窗体</param>
        /// <param name="filename">目标文件名称。此处忽略文件后缀名，后缀名根据图片文件格式而定。</param>
        /// <param name="format">文件格式</param>
        /// <returns>截取的图片对象</returns>
        public virtual Bitmap Capture(Form window, String filename, ImageFormatTypes format)
        {
            return Capture(window, filename, format, false);
        }

        /// <summary>
        /// 截取指定的窗体并保存到指定的文件中
        /// </summary>
        /// <param name="window">待截图的窗体</param>
        /// <param name="filename">目标文件名称。此处忽略文件后缀名，后缀名根据图片文件格式而定。</param>
        /// <param name="format">文件格式</param>
        /// <param name="onlyClient">True截取客户区域，否则截取整个窗体，包括标题栏、边框等。</param>
        /// <returns>截取的图片对象</returns>
        public virtual Bitmap Capture(Form window, String filename, ImageFormatTypes format, bool onlyClient)
        {
            Capture(window, onlyClient);
            Save(filename, format);
            return images[0];
        }

        /// <summary>
        /// 截取指定窗体（通过句柄）并保存到指定的文件中
        /// </summary>
        /// <param name="handle">待截图的窗体句柄</param>
        /// <param name="filename">目标文件名称。此处忽略文件后缀名，后缀名根据图片文件格式而定。</param>
        /// <param name="format">文件格式</param>
        /// <returns>截取的图片对象</returns>
        public virtual Bitmap Capture(IntPtr handle, String filename, ImageFormatTypes format)
        {
            Capture(handle);
            Save(filename, format);
            return images[0];
        }

        /// <summary>
        /// 截取窗体客户区域中指定的控件截图
        /// </summary>
        /// <param name="window">待截图的控件</param>
        /// <param name="filename">The name of the target file. The extension in there is ignored, 
        /// it will replaced by an extension derived from the desired file format.</param>
        /// <param name="format">The format of the file.</param>
        /// <returns>The image which has been captured.</returns>
        public virtual Bitmap CaptureControl(System.Windows.Forms.Control window, String filename, ImageFormatTypes format)
        {
            CaptureControl(window);
            Save(filename, format);
            return images[0];
        }

        /// <summary>
        /// Capture a specific control in the client area of a form.
        /// </summary>
        /// <param name="window">This is a control which should be captured.</param>
        /// <returns>The image which has been captured.</returns>
        public virtual Bitmap CaptureControl(System.Windows.Forms.Control window)
        {
            Rectangle rc = window.RectangleToScreen(window.DisplayRectangle);
            return capture(window, rc);
        }

        /// <summary>
        /// Capture a specific form.
        /// </summary>
        /// <param name="window">This is the desired window which should be captured.</param>
        /// <param name="onlyClient">When set to 'true' then only the client area of the form is captured,
        /// otherwise the complete window with title bar, frame etc. is captured.</param>
        /// <returns>The image which has been captured.</returns>
        public virtual Bitmap Capture(Form window, bool onlyClient)
        {
            if (!onlyClient)
                return Capture(window);

            Rectangle rc = window.RectangleToScreen(window.ClientRectangle);
            return capture(window, rc);
        }

        /// <summary>
        /// Capture a specific form.
        /// </summary>
        /// <param name="window">This is the desired window which should be captured.</param>
        /// <returns>The image which has been captured.</returns>
        public virtual Bitmap Capture(Form window)
        {
            Rectangle rc = new Rectangle(window.Location, window.Size);
            return capture(window, rc);
        }

        /// <summary>
        /// Execute the capturing of a specified rectangle in a given window.
        /// </summary>
        /// <param name="window">The window to capture</param>
        /// <param name="rc">The rectangle used for capturing</param>
        /// <returns>The image which has been captured.</returns>
        private Bitmap capture(System.Windows.Forms.Control window, Rectangle rc)
        {
            Bitmap memoryImage = null;
            images = new Bitmap[1];

            try
            {
                // Create new graphics object using handle to window.
                using (Graphics graphics = window.CreateGraphics())
                {
                    memoryImage = new Bitmap(rc.Width, rc.Height, graphics);

                    using (Graphics memoryGrahics = Graphics.FromImage(memoryImage))
                    {
                        memoryGrahics.CopyFromScreen(rc.X, rc.Y, 0, 0, rc.Size, CopyPixelOperation.SourceCopy);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(MyScreenCapture));
                MessageBox.Show(ex.ToString(), "Capture failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            images[0] = memoryImage;
            return memoryImage;
        }

        /// <summary>
        /// Execute the capturing of a window specified by it's windows handle.
        /// The image which has been captured is saved to the 'images[0]' attribute in this class.
        /// This method uses old API calls !!!!!!!
        /// </summary>
        /// <param name="handle">The handle of the window to capture</param>
        /// <returns>The image which has been captured.</returns>
        public virtual Bitmap Capture(IntPtr handle)
        {
            //	Move the window to capture to the top of the Z order.
            NativeMethods.BringWindowToTop(handle);

            CaptureHandleDelegateHandler dlg = new CaptureHandleDelegateHandler(CaptureHandle);

            //	Do an asynchronous call of the capturing method, this is necessary to allow the captured
            //	window to come up in front of the Z-order of the displayed screens.
            IAsyncResult result = dlg.BeginInvoke(handle, null, null);
            return dlg.EndInvoke(result);
        }


        /// <summary>
        /// Execute the capturing of a window specified by it's windows handle.
        /// This method uses old API calls !!!!!!!
        /// </summary>
        /// <param name="handle">The handle of the window to capture</param>
        /// <returns>The image which has been captured.</returns>
        protected virtual Bitmap CaptureHandle(IntPtr handle)
        {
            Bitmap memoryImage = null;
            images = new Bitmap[1];
            try
            {
                // Create new graphics object using handle to window.
                using (Graphics graphics = Graphics.FromHwnd(handle))
                {
                    Rectangle rc = NativeMethods.GetWindowRect(handle);

                    if ((int)graphics.VisibleClipBounds.Width > 0 && (int)graphics.VisibleClipBounds.Height > 0)
                    {
                        memoryImage = new Bitmap(rc.Width, rc.Height, graphics);

                        using (Graphics memoryGrahics = Graphics.FromImage(memoryImage))
                        {
                            memoryGrahics.CopyFromScreen(rc.X, rc.Y, 0, 0, rc.Size, CopyPixelOperation.SourceCopy);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(MyScreenCapture));
                MessageBox.Show(ex.ToString(), "Capture failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            images[0] = memoryImage;
            return memoryImage;
        } 
        #endregion

        #region 抓图各种屏幕截图
        /// <summary>
        /// Capture the screen and save it into a file, which portion of the screen is captured
        /// is defined by <paramref name="typeOfCapture"/>.
        /// </summary>
        /// <param name="typeOfCapture">Selects, what is actually captured, see <see cref="CaptureType"/>.</param>
        /// <param name="filename">The name of the target file. The extension in there is ignored, 
        /// it will replaced by an extension derived from the desired file format.</param>
        /// <param name="format">The format of the file.</param>
        /// <returns>An array of images captured.</returns>
        public virtual Bitmap[] Capture(CaptureType typeOfCapture, String filename, ImageFormatTypes format)
        {
            Capture(typeOfCapture);
            Save(filename, format);
            return images;
        }

        /// <summary>
        /// Capture the screen, which portion of the screen is captured
        /// is defined by <paramref name="typeOfCapture"/>.
        /// </summary>
        /// <param name="typeOfCapture">Selects, what is actually captured, see <see cref="CaptureType"/>.</param>
        /// <returns>An array of images captured.</returns>
        public virtual Bitmap[] Capture(CaptureType typeOfCapture)
        {
            Bitmap memoryImage;
            int count = 1;

            try
            {
                Screen[] screens = Screen.AllScreens;
                Rectangle rc;
                switch (typeOfCapture)
                {
                    case CaptureType.PrimaryScreen:
                        rc = Screen.PrimaryScreen.Bounds;
                        break;
                    case CaptureType.VirtualScreen:
                        rc = SystemInformation.VirtualScreen;
                        break;
                    case CaptureType.WorkingArea:
                        rc = Screen.PrimaryScreen.WorkingArea;
                        break;
                    case CaptureType.AllScreens:
                        count = screens.Length;
                        typeOfCapture = CaptureType.WorkingArea;
                        rc = screens[0].WorkingArea;
                        break;
                    default:
                        rc = SystemInformation.VirtualScreen;
                        break;
                }
                images = new Bitmap[count];

                for (int index = 0; index < count; index++)
                {
                    if (index > 0)
                        rc = screens[index].WorkingArea;

                    memoryImage = new Bitmap(rc.Width, rc.Height, PixelFormat.Format32bppArgb);

                    using (Graphics memoryGrahics = Graphics.FromImage(memoryImage))
                    {
                        memoryGrahics.CopyFromScreen(rc.X, rc.Y, 0, 0, rc.Size, CopyPixelOperation.SourceCopy);
                    }
                    images[index] = memoryImage;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(MyScreenCapture));
            }
            return images;
        } 

        #endregion

        #region 打印
        /// <summary>
        /// Print all captured screens.
        /// </summary>
        public virtual void Print()
        {
            if (images != null)
            {
                try
                {
                    for (int i = 0; i < images.Length; i++)
                    {
                        image = images[i];
                        doc.DefaultPageSettings.Landscape = (image.Width > image.Height);
                        doc.Print();
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(MyScreenCapture));
                    MessageBox.Show(ex.ToString(), "Capture failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Event handler called from printing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printPage(object sender, PrintPageEventArgs e)
        {
            RectangleF rc = doc.DefaultPageSettings.Bounds;
            float ratio = (float)image.Height / (float)(image.Width != 0 ? image.Width : 1);

            rc.Height = rc.Height - doc.DefaultPageSettings.Margins.Top - doc.DefaultPageSettings.Margins.Bottom;
            rc.Y = rc.Y + doc.DefaultPageSettings.Margins.Top;

            rc.Width = rc.Width - doc.DefaultPageSettings.Margins.Left - doc.DefaultPageSettings.Margins.Right;
            rc.X = rc.X + doc.DefaultPageSettings.Margins.Left;

            if (rc.Height / rc.Width > ratio)
                rc.Height = rc.Width * ratio;
            else
                rc.Width = rc.Height / (ratio != 0 ? ratio : 1);

            e.Graphics.DrawImage(image, rc);
        } 
        #endregion

        /// <summary>
        /// 保存所有屏幕截图到文件中.
        /// </summary>
        /// <param name="filename">The name of the target file. The extension in there is ignored, 
        /// it will replaced by an extension derived from the desired file format.</param>
        /// <param name="format">The format of the file.</param>
        /// <returns>An array of images captured.</returns>
        public virtual void Save(String filename, ImageFormatTypes format)
        {
            String directory = Path.GetDirectoryName(filename);
            String name = Path.GetFileNameWithoutExtension(filename);
            String ext = Path.GetExtension(filename);
            ext = formatHandler.GetDefaultFilenameExtension(format);
            if (ext.Length == 0)
            {
                format = ImageFormatTypes.imgPNG;
                ext = "png";
            }

            try
            {
                ImageCodecInfo info;
                EncoderParameters parameters = formatHandler.GetEncoderParameters(format, out info);

                for (int i = 0; i < images.Length; i++)
                {
                    if (images.Length > 1)
                    {
                        filename = String.Format("{0}\\{1}.{2:D2}.{3}", directory, name, i + 1, ext);
                    }
                    else
                    {
                        filename = String.Format("{0}\\{1}.{2}", directory, name, ext);
                    }
                    image = images[i];

                    if (parameters != null)
                    {
                        image.Save(filename, info, parameters);
                    }
                    else
                    {
                        image.Save(filename, ImageFormatHandler.GetImageFormat(format));
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(MyScreenCapture));
            }
        }
	}
}
