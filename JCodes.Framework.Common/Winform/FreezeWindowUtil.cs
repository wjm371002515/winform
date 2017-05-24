using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using JCodes.Framework.jCodesenum.BaseEnum;

namespace JCodes.Framework.Common.Winform
{
    /// <summary>
    /// 对进程的窗体进行冻结、解冻操作辅助类
    /// </summary>
    public class FreezeWindowsUtil : IDisposable
    {
        private uint processId;

        /// <summary>
        /// 根据窗体句柄构造类，并冻结所在进程的窗体
        /// </summary>
        /// <param name="windowHandle"></param>
        public FreezeWindowsUtil(IntPtr windowHandle)
        {
            NativeMethods.GetWindowThreadProcessId(windowHandle, out processId);
            FreezeThreads((int)processId);
        }

        /// <summary>
        /// 关闭对象，解除冻结操作
        /// </summary>
        public void Dispose()
        {
            UnfreezeThreads((int)processId);
        }

        /// <summary>
        /// 对进程的窗体进行冻结
        /// </summary>
        /// <param name="intPID"></param>
        public static void FreezeThreads(int intPID)
        {
            if (intPID != 0 && Process.GetCurrentProcess().Id != intPID)
            {
                Process pProc = Process.GetProcessById(intPID);
                if (!string.IsNullOrEmpty(pProc.ProcessName) && pProc.ProcessName != "explorer")
                {
                    foreach (ProcessThread pT in pProc.Threads)
                    {
                        IntPtr ptrOpenThread = NativeMethods.OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)pT.Id);
                        if (ptrOpenThread != null)
                        {
                            NativeMethods.SuspendThread(ptrOpenThread);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 解冻进程的窗体
        /// </summary>
        /// <param name="intPID"></param>
        public static void UnfreezeThreads(int intPID)
        {
            if (intPID != 0 && Process.GetCurrentProcess().Id != intPID)
            {
                Process pProc = Process.GetProcessById(intPID);
                if (!string.IsNullOrEmpty(pProc.ProcessName))
                {
                    foreach (ProcessThread pT in pProc.Threads)
                    {
                        IntPtr ptrOpenThread = NativeMethods.OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)pT.Id);
                        if (ptrOpenThread != null)
                        {
                            NativeMethods.ResumeThread(ptrOpenThread);
                        }
                    }
                }
            }
        }
    }
}
