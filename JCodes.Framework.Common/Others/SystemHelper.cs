using System.Diagnostics;

namespace JCodes.Framework.Common.Others
{
    public class SystemHelper
    {
        /// <summary> 
        /// 执行CMD语句 
        /// </summary> 
        /// <param name="cmd">要执行的CMD命令</param> 
        public static void RunCmd(string cmd)
        {
            Process proc = new Process(); 
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.FileName = "cmd.exe";
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.RedirectStandardInput = true;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.Start();
            proc.StandardInput.WriteLine(cmd);
            proc.StandardInput.WriteLine("exit");//最后打入退出命令   
            proc.WaitForExit(); 
            proc.Close();
            proc.Dispose();
        }

        /// <summary> 
        /// 执行CMD语句 
        /// </summary> 
        /// <param name="cmd">要执行的CMD命令</param> 
        public static void RunCmd(string cmd, out string outstr)
        {
            Process proc = new Process();
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.FileName = "cmd.exe";
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.RedirectStandardInput = true;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.Start();
            proc.StandardInput.WriteLine(cmd);
            proc.StandardInput.WriteLine("exit");//最后打入退出命令
            outstr = proc.StandardOutput.ReadToEnd();   // 把结果内容输出出去
            proc.WaitForExit();
            proc.Close();
            proc.Dispose();
        }

        /// <summary> 
        /// 打开软件并执行命令 
        /// </summary> 
        /// <param name="programName">软件路径加名称（.exe文件）</param> 
        /// <param name="cmd">要执行的命令</param> 
        public static void RunProgram(string programName, string cmd)
        {
            Process proc = new Process();
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.FileName = programName;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.RedirectStandardInput = true;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.Start();
            if (cmd.Length != 0)
            {
                proc.StandardInput.WriteLine(cmd);
            }
            proc.Close();
        } 
    }
}
