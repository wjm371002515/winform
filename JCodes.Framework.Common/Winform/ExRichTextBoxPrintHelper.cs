using System;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Drawing.Printing;
using System.Diagnostics;
using JCodes.Framework.Common;

namespace JCodes.Framework.Common
{
    /// <summary>
    /// 提供一个简单的方法打印RichTextBox的辅助类。
    /// based of work by Martin Müller http://msdn.microsoft.com/en-us/library/ms996492.aspx
    /// </summary>
    public class ExRichTextBoxPrintHelper
    {
        RichTextBox control;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="controlToPrint"></param>
        public ExRichTextBoxPrintHelper(RichTextBox controlToPrint)
        {
            control = controlToPrint;
        }

        /// <summary>
        /// 打印文本
        /// </summary>
        /// <param name="preview">是否预览</param>
        public void PrintRTF(bool preview)
        {
            PrintRTF(new PrintDocument(), preview);
        }

        /// <summary>
        /// 打印文本
        /// </summary>
        /// <param name="printDocument">打印文档</param>
        public void PrintRTF(PrintDocument printDocument)
        {
            try
            {
                printDocument.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.printDocument_BeginPrint);
                printDocument.EndPrint += new System.Drawing.Printing.PrintEventHandler(this.printDocument_EndPrint);
                printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument_PrintPage);
                printDocument.Print();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 打印富文本内容
        /// </summary>
        /// <param name="printDocument">打印文档</param>
        /// <param name="preview">预览对象</param>
        public void PrintRTF(PrintDocument printDocument, bool preview)
        {
            try
            {
                printDocument.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.printDocument_BeginPrint);
                printDocument.EndPrint += new System.Drawing.Printing.PrintEventHandler(this.printDocument_EndPrint);
                printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument_PrintPage);

                CoolPrintPreviewDialog dlg = new CoolPrintPreviewDialog();
                dlg.Document = printDocument;
                if (preview)
                {
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        printDocument.Print();
                    }
                }
                else
                {
                    printDocument.Print();
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }


        private int m_nFirstCharOnPage;

        private void printDocument_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            // Start at the beginning of the text
            m_nFirstCharOnPage = 0;
        }

        private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            // To print the boundaries of the current page margins
            // uncomment the next line:
            //e.Graphics.DrawRectangle(System.Drawing.Pens.Blue, e.MarginBounds);

            // make the RichTextBoxEx calculate and render as much text as will
            // fit on the page and remember the last character printed for the
            // beginning of the next page
            m_nFirstCharOnPage = FormatRange(false,
                e,
                m_nFirstCharOnPage,
                control.TextLength);

            // check if there are more pages to print
            if (m_nFirstCharOnPage < control.TextLength)
                e.HasMorePages = true;
            else
                e.HasMorePages = false;
        }

        private void printDocument_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            // Clean up cached information
            FormatRangeDone();
        }


        [StructLayout(LayoutKind.Sequential)]
        private struct STRUCT_RECT
        {
            public Int32 left;
            public Int32 top;
            public Int32 right;
            public Int32 bottom;
        }
        [StructLayout(LayoutKind.Sequential)]
        private struct STRUCT_CHARRANGE
        {
            public Int32 cpMin;
            public Int32 cpMax;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct STRUCT_FORMATRANGE
        {
            public IntPtr hdc;
            public IntPtr hdcTarget;
            public STRUCT_RECT rc;
            public STRUCT_RECT rcPage;
            public STRUCT_CHARRANGE chrg;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct CHARFORMATSTRUCT
        {
            public int cbSize;
            public UInt32 dwMask;
            public UInt32 dwEffects;
            public Int32 yHeight;
            public Int32 yOffset;
            public Int32 crTextColor;
            public byte bCharSet;
            public byte bPitchAndFamily;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public char[] szFaceName;
        }

        [DllImport("user32.dll")]
        private static extern Int32 SendMessage(IntPtr hWnd, Int32 msg,
            Int32 wParam, IntPtr lParam);

        /// <summary>
        /// Calculate or render the contents of our RichTextBox for printing
        /// </summary>
        /// <param name="measureOnly">If true, only the calculation is performed,
        /// otherwise the text is rendered as well</param>
        /// <param name="e">The PrintPageEventArgs object from the
        /// PrintPage event</param>
        /// <param name="charFrom">Index of first character to be printed</param>
        /// <param name="charTo">Index of last character to be printed</param>
        /// <returns>(Index of last character that fitted on the
        /// page) + 1</returns>
        public int FormatRange(bool measureOnly, PrintPageEventArgs e,
            int charFrom, int charTo)
        {
            // Specify which characters to print
            STRUCT_CHARRANGE cr;
            cr.cpMin = charFrom;
            cr.cpMax = charTo;

            // Specify the area inside page margins
            STRUCT_RECT rc;
            rc.top = HundredthInchToTwips(e.MarginBounds.Top);
            rc.bottom = HundredthInchToTwips(e.MarginBounds.Bottom);
            rc.left = HundredthInchToTwips(e.MarginBounds.Left);
            rc.right = HundredthInchToTwips(e.MarginBounds.Right);

            // Specify the page area
            STRUCT_RECT rcPage;
            rcPage.top = HundredthInchToTwips(e.PageBounds.Top);
            rcPage.bottom = HundredthInchToTwips(e.PageBounds.Bottom);
            rcPage.left = HundredthInchToTwips(e.PageBounds.Left);
            rcPage.right = HundredthInchToTwips(e.PageBounds.Right);

            // Get device context of output device
            IntPtr hdc = e.Graphics.GetHdc();

            // Fill in the FORMATRANGE struct
            STRUCT_FORMATRANGE fr;
            fr.chrg = cr;
            fr.hdc = hdc;
            fr.hdcTarget = hdc;
            fr.rc = rc;
            fr.rcPage = rcPage;

            // Non-Zero wParam means render, Zero means measure
            Int32 wParam = (measureOnly ? 0 : 1);

            // Allocate memory for the FORMATRANGE struct and
            // copy the contents of our struct to this memory
            IntPtr lParam = Marshal.AllocCoTaskMem(Marshal.SizeOf(fr));
            Marshal.StructureToPtr(fr, lParam, false);

            // Send the actual Win32 message
            int res = SendMessage(control.Handle, Const.EM_FORMATRANGE, wParam, lParam);

            // Free allocated memory
            Marshal.FreeCoTaskMem(lParam);

            // and release the device context
            e.Graphics.ReleaseHdc(hdc);

            return res;
        }
        /// <summary>
        /// Convert between 1/100 inch (unit used by the .NET framework)
        /// and twips (1/1440 inch, used by Win32 API calls)
        /// </summary>
        /// <param name="n">Value in 1/100 inch</param>
        /// <returns>Value in twips</returns>
        private Int32 HundredthInchToTwips(int n)
        {
            return (Int32)(n * 14.4);
        }
        /// <summary>
        /// Free cached data from rich edit control after printing
        /// </summary>
        public void FormatRangeDone()
        {
            IntPtr lParam = new IntPtr(0);
            SendMessage(control.Handle, Const.EM_FORMATRANGE, 0, lParam);
        }

        /// <summary>
        /// Sets the font only for the selected part of the rich text box
        /// without modifying the other properties like size or style
        /// </summary>
        /// <param name="control">control</param>
        /// <param name="face">Name of the font to use</param>
        /// <returns>true on success, false on failure</returns>
        public static bool SetSelectionFont(RichTextBox control, string face)
        {
            CHARFORMATSTRUCT cf = new CHARFORMATSTRUCT();
            cf.cbSize = Marshal.SizeOf(cf);
            cf.szFaceName = new char[32];
            cf.dwMask = Const.CFM_FACE;
            face.CopyTo(0, cf.szFaceName, 0, Math.Min(31, face.Length));

            IntPtr lParam = Marshal.AllocCoTaskMem(Marshal.SizeOf(cf));
            Marshal.StructureToPtr(cf, lParam, false);

            int res = SendMessage(control.Handle, Const.EM_SETCHARFORMAT, Const.SCF_SELECTION, lParam);
            return (res == 0);
        }

        /// <summary>
        /// Sets the font size only for the selected part of the rich text box
        /// without modifying the other properties like font or style
        /// </summary>
        /// <param name="control">RichTextBox</param>
        /// <param name="size">new point size to use</param>
        /// <returns>true on success, false on failure</returns>
        public static bool SetSelectionSize(RichTextBox control, int size)
        {
            CHARFORMATSTRUCT cf = new CHARFORMATSTRUCT();
            cf.cbSize = Marshal.SizeOf(cf);
            cf.dwMask = Const.CFM_SIZE;
            // yHeight is in 1/20 pt
            cf.yHeight = size * 20;

            IntPtr lParam = Marshal.AllocCoTaskMem(Marshal.SizeOf(cf));
            Marshal.StructureToPtr(cf, lParam, false);

            int res = SendMessage(control.Handle, Const.EM_SETCHARFORMAT, Const.SCF_SELECTION, lParam);
            return (res == 0);
        }

        /// <summary>
        /// Sets the bold style only for the selected part of the rich text box
        /// without modifying the other properties like font or size
        /// </summary>
        /// <param name="bold">make selection bold (true) or regular (false)</param>
        /// <returns>true on success, false on failure</returns>
        public bool SetSelectionBold(bool bold)
        {
            return SetSelectionStyle(Const.CFM_BOLD, bold ? Const.CFE_BOLD : 0);
        }

        /// <summary>
        /// Sets the italic style only for the selected part of the rich text box
        /// without modifying the other properties like font or size
        /// </summary>
        /// <param name="italic">make selection italic (true) or regular (false)</param>
        /// <returns>true on success, false on failure</returns>
        public bool SetSelectionItalic(bool italic)
        {
            return SetSelectionStyle(Const.CFM_ITALIC, italic ? Const.CFE_ITALIC : 0);
        }

        /// <summary>
        /// Sets the underlined style only for the selected part of the rich text box
        /// without modifying the other properties like font or size
        /// </summary>
        /// <param name="underlined">make selection underlined (true) or regular (false)</param>
        /// <returns>true on success, false on failure</returns>
        public bool SetSelectionUnderlined(bool underlined)
        {
            return SetSelectionStyle(Const.CFM_UNDERLINE, underlined ? Const.CFE_UNDERLINE : 0);
        }

        /// <summary>
        /// Set the style only for the selected part of the rich text box
        /// with the possibility to mask out some styles that are not to be modified
        /// </summary>
        /// <param name="mask">modify which styles?</param>
        /// <param name="effect">new values for the styles</param>
        /// <returns>true on success, false on failure</returns>
        private bool SetSelectionStyle(UInt32 mask, UInt32 effect)
        {
            CHARFORMATSTRUCT cf = new CHARFORMATSTRUCT();
            cf.cbSize = Marshal.SizeOf(cf);
            cf.dwMask = mask;
            cf.dwEffects = effect;

            IntPtr lParam = Marshal.AllocCoTaskMem(Marshal.SizeOf(cf));
            Marshal.StructureToPtr(cf, lParam, false);

            int res = SendMessage(control.Handle, Const.EM_SETCHARFORMAT, Const.SCF_SELECTION, lParam);
            return (res == 0);
        }
    }
}

