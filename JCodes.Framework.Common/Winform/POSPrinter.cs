using System;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Drawing;
using JCodes.Framework.jCodesenum.BaseEnum;

namespace JCodes.Framework.Common.Winform
{
    /// <summary>
    /// 并口POS打印机操作辅助类
    /// </summary>
    public class POSPrinter
    {
        string prnPort = "LPT1";

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr CreateFile(string lpFileName,
        int dwDesiredAccess,
        int dwShareMode,
        int lpSecurityAttributes,
        uint dwCreationDisposition,
        int dwFlagsAndAttributes,
        int hTemplateFile);

        /// <summary>
        /// 默认构造函数，打印并口名称为LPT1
        /// </summary>
        public POSPrinter()
        {
        }

        /// <summary>
        /// 以指定的并口打印名称构造函数
        /// </summary>
        /// <param name="prnPort">打印并口名称，如LPT1</param>
        public POSPrinter(string prnPort)
        {
            this.prnPort = prnPort;//打印机端口 
        }

        /// <summary>
        /// 打印内容
        /// </summary>
        /// <param name="str">待打印的字符串</param>
        /// <returns></returns>
        public string PrintLine(string str)
        {
            try
            {
                IntPtr iHandle = CreateFile(prnPort, 0x40000000, 0, 0, Const.OPEN_EXISTING, 0, 0);
                if (iHandle.ToInt32() == -1)
                {
                    return "没有连接打印机或者打印机端口不是LPT1";
                }
                else
                {
                    using (FileStream fs = new FileStream(iHandle, FileAccess.ReadWrite))
                    {
                        using (StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default)) //写数据 
                        {
                            sw.WriteLine(str);
                            //开钱箱 
                            //sw.WriteLine(Chr(&H1B) & Chr(70) & Chr(0) & Chr(20) & Chr(&HA0)) 
                        }
                    }
                    return "";
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(POSPrinter));
                return ex.Message;
            }
        }
    }

}