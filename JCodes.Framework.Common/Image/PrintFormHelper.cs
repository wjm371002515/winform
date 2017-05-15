using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using JCodes.Framework.Common;

namespace JCodes.Framework.Common
{
	/// <summary>
	/// Winform窗口打印辅助类
	/// </summary>
	public class PrintFormHelper
	{
        /// <summary>
        /// 弹出打印窗体的预览对话框
        /// </summary>
        /// <param name="form">窗体对象</param>
        public static void Print(Form form)
        {
            ScreenCapture capture = new ScreenCapture();
            Image image = capture.CaptureWindow(form.Handle);

            ImagePrintHelper helper = new ImagePrintHelper(image);
            helper.PrintPreview();
        }

        /// <summary>
        /// 打印窗体控件
        /// </summary>
        /// <param name="control">控件对象</param>
        public static void Print(Control control)
        {
            ScreenCapture capture = new ScreenCapture();
            Image image = capture.CaptureWindow(control.Handle);

            ImagePrintHelper helper = new ImagePrintHelper(image);
            helper.PrintPreview();
        }
	}
}
