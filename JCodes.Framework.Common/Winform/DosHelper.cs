using System;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading;
using JCodes.Framework.jCodesenum.BaseEnum;

namespace JCodes.Framework.Common.Winform
{
    /// <summary>
    /// DOS操作封装辅助类
    /// </summary>
    public class DosHelper
    {
        /// <summary>
        /// 后台执行DOS文件
        /// </summary>
        /// <param name="fileName">文件名(包含路径)</param>
        /// <param name="argument">运行参数</param>
        /// <param name="hidden">是否隐藏窗口</param>
        public static void RunDos(string fileName, string argument, bool hidden)
        {
            Process process = new Process();
            process.StartInfo.FileName = string.Format("\"{0}\"", fileName);
            process.StartInfo.Arguments = argument;
            process.EnableRaisingEvents = false;
            if (hidden)
            {
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            }
            else
            {
                process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            }
            process.Start();
            process.WaitForExit(1000);
        }

        /// <summary>   
        /// 运行指定DOS命令行   
        /// </summary>   
        /// <param name="fileName">命令</param>   
        /// <param name="argument">命令行参数</param>   
        /// <param name="hidden">是否隐藏窗口</param> 
        /// <param name="confirm">写入命令行的确认信息</param>   
        /// <returns></returns>   
        public static string ExecuteCMD(string fileName, string argument, bool hidden, string confirm)
        {
            Process process = new Process();
            process.StartInfo.FileName = fileName;
            process.StartInfo.Arguments = argument;
            if (hidden)
            {
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            }
            else
            {
                process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            }

            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = hidden;// 是否显示窗口

            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;            

            process.Start();
            if (confirm != null)
            {
                process.StandardInput.WriteLine(confirm);
            }
            string msg = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            process.Close();

            return msg;
        }

        /// <summary>
        /// 同步方式执行ＤＯＳ命令
        /// </summary>
        /// <param name="command">ＤＯＳ命令</param>
        public static void ExecuteCommandSync(object command)
        {
            try
            {
                // create the ProcessStartInfo using "cmd" as the program to be run,
                // and "/c " as the parameters.
                // Incidentally, /c tells cmd that we want it to execute the command that follows,
                // and then exit.
                System.Diagnostics.ProcessStartInfo procStartInfo =
                    new System.Diagnostics.ProcessStartInfo("cmd", "/c " + command);

                // The following commands are needed to redirect the standard output.
                // This means that it will be redirected to the Process.StandardOutput StreamReader.
                procStartInfo.RedirectStandardOutput = true;
                procStartInfo.UseShellExecute = false;
                // Do not create the black window.
                procStartInfo.CreateNoWindow = true;
                // Now we create a process, assign its ProcessStartInfo and start it
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo = procStartInfo;
                proc.Start();
                // Get the output into a string
                string result = proc.StandardOutput.ReadToEnd();
                // Display the command output.
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(DosHelper));
                // Log the exception
            }
        }

        /// <summary>
        /// 异步方式执行ＤＯＳ命令
        /// </summary>
        /// <param name="command">DOS命令字符串</param>
        public static void ExecuteCommandAsync(string command)
        {
            try
            {
                //Asynchronously start the Thread to process the Execute command request.
                Thread objThread = new Thread(new ParameterizedThreadStart(ExecuteCommandSync));
                //Make the thread as background thread.
                objThread.IsBackground = true;
                //Set the Priority of the thread.
                objThread.Priority = ThreadPriority.AboveNormal;
                //Start the thread.
                objThread.Start(command);
            }
            catch (ThreadStartException ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(DosHelper));
            }
            catch (ThreadAbortException ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(DosHelper));
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(DosHelper));
            }
        }
    }
}
