using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JCodes.Framework.Common.Images
{
    /// <summary>
    /// Web页面预览效果图片抓取辅助类
    /// </summary>
    public class WebPageCapture : IDisposable
    {
        #region 字段属性
        /// <summary>
        /// 图片下载完毕的时间处理
        /// </summary>
        public event ImageEventHandler DownloadCompleted;
        /// <summary>
        /// 图片处理委托定义
        /// </summary>
        /// <param name="image"></param>
        public delegate void ImageEventHandler(Image image);

        /// <summary>
        /// 浏览区域大小
        /// </summary>
        public Size BrowserSize { get; set; }

        /// <summary>
        /// 页面URL地址
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// 下载的图片对象
        /// </summary>
        public Image Image { get; private set; }

        private WebBrowser webBrowser = new WebBrowser(); 
        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数,默认为屏幕大小
        /// </summary>
        public WebPageCapture() : this(Screen.PrimaryScreen.Bounds.Size) { }

        /// <summary>
        /// 构造函数，指定浏览区域大小
        /// </summary>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        public WebPageCapture(int width, int height) : this(new Size(width, height)) { }

        /// <summary>
        /// 构造函数，指定浏览区域大小
        /// </summary>
        /// <param name="browserSize">浏览区域大小</param>
        public WebPageCapture(Size browserSize)
        {
            BrowserSize = browserSize;
            webBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webBrowser_DocumentCompleted);
            webBrowser.ScrollBarsEnabled = false;
        } 
        #endregion

        /// <summary>
        /// 下载页面到图片中
        /// </summary>
        public void DownloadPage()
        {
            if (!string.IsNullOrEmpty(this.URL))
            {
                DownloadPage(this.URL);
            }
        }

        /// <summary>
        /// 下载页面到图片中
        /// </summary>
        /// <param name="url">页面Url地址</param>
        public void DownloadPage(string url)
        {
            this.URL = url;
            webBrowser.Size = BrowserSize;
            webBrowser.Navigate(url);
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            this.Image.Dispose();
            this.webBrowser.Dispose();
        }

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            Rectangle rect = webBrowser.Document.ActiveElement.ScrollRectangle;
            webBrowser.Size = new Size(rect.Width, rect.Height);
            Bitmap bmp = new Bitmap(rect.Width, rect.Height);

            try
            {
                webBrowser.DrawToBitmap(bmp, rect);
                this.Image = bmp;
            }
            finally
            {
                if (DownloadCompleted != null) DownloadCompleted(bmp);
            }
        }
    }
}
