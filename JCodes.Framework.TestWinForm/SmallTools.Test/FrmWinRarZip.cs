using JCodes.Framework.Common.Format;
using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using JCodes.Framework.CommonControl.Controls;
using JCodes.Framework.Entity;

namespace JCodes.Framework.SmallTools.Test
{
    public partial class FrmWinRarZip : Form
    {
        // 第一步：定义委托类型
        // 将text更新的界面控件的委托类型
        delegate void SetTextCallback(string text);

        public FrmWinRarZip()
        {
            InitializeComponent();

            // 初始化下拉框
            System.Collections.Generic.List<CListItem> lst = new System.Collections.Generic.List<CListItem>();
            lst.Add(new CListItem("*.rar", "RAR解压"));
            lst.Add(new CListItem("*.zip", "ZIP解压"));
            cbbeUnRarZip.BindDictItems(lst);
        }

        //第二步：定义线程的主体方法
        /// <summary>
        /// 线程的主体方法
        /// </summary>
        private void WinRarDeal()
        {
            if(string.IsNullOrEmpty(txtRarPath.Text.Trim()))
            {
                JCodes.Framework.CommonControl.Other.MessageDxUtil.ShowWarning("请选择压缩路径后再操作");
                return;
            }
            
            if (!System.IO.Directory.Exists(txtRarPath.Text.Trim()))
            {
                JCodes.Framework.CommonControl.Other.MessageDxUtil.ShowWarning("压缩路径不存在，请确认后再操作");
                return;
            }

            // 获取rar路径
            string winrarPath = System.Configuration.ConfigurationManager.AppSettings["WinRarPath"];
            if (!System.IO.File.Exists(winrarPath))
            {
                JCodes.Framework.CommonControl.Other.MessageDxUtil.ShowWarning("App.Config 中配置的WinRarPath路径有问题，请确认后再操作");
                return;
            }

            string curPath = System.IO.Directory.GetCurrentDirectory();     // 获取当前工作路径;
            System.IO.Directory.SetCurrentDirectory(txtRarPath.Text.Trim());// 设置当前工作路径

            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(txtRarPath.Text.Trim());
            System.IO.FileSystemInfo[] files = dir.GetFileSystemInfos();

            Int32 successCount = 0;
            foreach (var file in files)
            {
                // "C:\Program Files\WinRAR\WinRAR.exe" a wjm 1   (把1这个文件夹压缩到压缩包wjm中)
                
                string rarfileName = string.Empty;
                if (string.Equals(file.Extension.ToLower(), ".rar") || string.Equals(file.Extension.ToLower(), ".zip"))
                    continue;

                // 该文件是用于备份或删除的候选版本
                if (file.Attributes == System.IO.FileAttributes.Archive)
                {
                    rarfileName = file.FullName.Replace(file.Extension, "");
                }
                if (file.Attributes == System.IO.FileAttributes.Directory)
                {
                    rarfileName = file.FullName;
                }

                if (string.IsNullOrEmpty(rarfileName))
                    continue;

                if (System.IO.File.Exists(rarfileName + ".rar"))
                    continue;

                string comand = string.Format("\"{0}\" a \"{1}\" \"{2}\"", winrarPath, rarfileName, file.Name);
                JCodes.Framework.Common.Others.SystemHelper.RunCmd(comand);
                successCount++;
                this.SetText(String.Format("{0}  {1}\r\n", DateTimeHelper.GetServerDateTime(), comand));   
            }

            System.IO.Directory.SetCurrentDirectory(curPath);

            JCodes.Framework.CommonControl.Other.MessageDxUtil.ShowTips(string.Format("成功压缩文件{0}个", successCount));
        }

        //第二步：定义线程的主体方法
        /// <summary>
        /// 线程的主体方法
        /// </summary>
        private void WinUnRarZipDeal()
        {
            if (string.IsNullOrEmpty(txtUnRarPath.Text.Trim()))
            {
                JCodes.Framework.CommonControl.Other.MessageDxUtil.ShowWarning("请选择解压路径后再操作");
                return;
            }

            if (!System.IO.Directory.Exists(txtUnRarPath.Text.Trim()))
            {
                JCodes.Framework.CommonControl.Other.MessageDxUtil.ShowWarning("解压路径不存在，请确认后再操作");
                return;
            }

            // 获取rar路径
            string winrarPath = System.Configuration.ConfigurationManager.AppSettings["WinRarPath"];
            if (!System.IO.File.Exists(winrarPath))
            {
                JCodes.Framework.CommonControl.Other.MessageDxUtil.ShowWarning("App.Config 中配置的WinRarPath路径有问题，请确认后再操作");
                return;
            }

            string curPath = System.IO.Directory.GetCurrentDirectory();     // 获取当前工作路径;
            System.IO.Directory.SetCurrentDirectory(txtUnRarPath.Text.Trim());// 设置当前工作路径

            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(txtUnRarPath.Text.Trim());
            System.IO.FileSystemInfo[] files = dir.GetFileSystemInfos();

            Int32 successCount = 0;
            foreach (var file in files)
            {
                if (!cbbeUnRarZip.GetCheckedComboBoxValue().Contains(file.Extension.ToLower()))
                    continue;

                if (!System.IO.File.Exists(file.Name))
                    continue;

                string unrarFilePath = file.Name.Replace(file.Extension, "\\");
                string comand = string.Format("\"{0}\" e -y \"{1}\" \"{2}\"", winrarPath, file.Name, unrarFilePath);
                JCodes.Framework.Common.Others.SystemHelper.RunCmd(comand);
                successCount++;
                this.SetText(String.Format("{0}  {1}\r\n", DateTimeHelper.GetServerDateTime(), comand));
            }

            System.IO.Directory.SetCurrentDirectory(curPath);

            JCodes.Framework.CommonControl.Other.MessageDxUtil.ShowTips(string.Format("成功压缩文件{0}个", successCount));
        }

        //第三步：定义更新UI控件的方法
        /// <summary>
        /// 更新文本框内容的方法
        /// </summary>
        /// <param name="text"></param>
        private void SetText(object text)
        {
            // InvokeRequired required compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 
            if (this.richText.InvokeRequired)//如果调用控件的线程和创建创建控件的线程不是同一个则为True
            {
                while (!this.richText.IsHandleCreated)
                {
                    //解决窗体关闭时出现“访问已释放句柄“的异常
                    if (this.richText.Disposing || this.richText.IsDisposed)
                        return;
                }
                SetTextCallback d = new SetTextCallback(SetText);
                this.richText.Invoke(d, new object[] { text });
            }
            else
            {
                this.richText.Text += text;
            }
        }

        /// <summary>
        /// 压缩解压路径选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textEdit1_Click(object sender, EventArgs e)
        {
            // 文件路径选择
            var textEdit = sender as DevExpress.XtraEditors.TextEdit;

            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.Description = "请选择要压缩文件夹";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textEdit.Text = dialog.SelectedPath;
            }
        }

        /// <summary>
        /// 压缩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRar_Click(object sender, EventArgs e)
        {
            // 压缩命令
            // WinRAR绝对路径 a 压缩名 压缩的文件
            // "C:\Program Files\WinRAR\WinRAR.exe" a wjm 1   (把1这个文件夹压缩到压缩包wjm中)
            // 压缩多个文件命令
            // WinRAR绝对路径 a 压缩名 压缩的文件1 压缩的文件2
            // "C:\Program Files\WinRAR\WinRAR.exe" a wjm 1.txt 2.txt   (把1.txt、2.txt这个文件压缩到压缩包wjm中)
            var demoThread = new Thread(new ThreadStart(this.WinRarDeal));
            demoThread.Start();
        }

        /// <summary>
        /// 解压
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUnRar_Click(object sender, EventArgs e)
        {
            // 解压命令
            // WinRAR绝对路径 e y  压缩的文件 压缩名\
            // D:\WinRAR\WinRAR.exe e -y 融创东南海8-业主群_20171014.rar 融创东南海8-业主群_20171014\
            var demoThread = new Thread(new ThreadStart(this.WinUnRarZipDeal));
            demoThread.Start();
        }

    }
}
