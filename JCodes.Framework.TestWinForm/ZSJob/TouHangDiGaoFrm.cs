using JCodes.Framework.Common.Files;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.CommonControl.Other;
using SevenZip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace JCodes.Framework.TestWinForm.ZSJob
{
    public partial class TouHangDiGaoFrm : BaseDock
    {
        public TouHangDiGaoFrm()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, System.EventArgs e)
        {
            string path = FileDialogHelper.OpenDir();

            if (!string.IsNullOrEmpty(path))
            {
                string[] dirs = DirectoryUtil.GetDirectories(path);

                Dictionary<string, bool> a = new Dictionary<string, bool>();
                // 判断是否是子目录
                foreach (var d in dirs)
                {
                    string[] subdirs = DirectoryUtil.GetDirectories(d, "*", true);

                    string[] tmpdsname = d.Split('\\');
                    string dsname = tmpdsname[tmpdsname.Length - 1].Split('$')[0];

                    foreach (var dir in subdirs)
                    {
                        string[] tmpdirsname = dir.Split('\\');
                        string dirname = tmpdirsname[tmpdirsname.Length - 1].Split('$')[0];

                        a[dsname + dirname] = false;

                        if (dirname.Substring(0, 1) == "第" && dirname.Substring(dirname.Length - 1, 1) == "章") continue;

                        Int32 readCount = 0;
                        foreach (var dir2 in subdirs)
                        {
                            string[] tmpdir2sname = dir2.Split('\\');
                            string dir2name = tmpdir2sname[tmpdir2sname.Length - 1].Split('$')[0];

                            if (dir2name != dirname && dirname.Contains(dir2name) && dirname.IndexOf(dir2name) == 0)
                            {
                                a[dsname + dirname] = true;
                                break;
                            }

                            if (dir2name != dirname && dir2name.Contains(dirname) && dir2name.IndexOf(dirname) == 0)
                            {
                                a[dsname + dirname] = false;
                                break;
                            }
                            // 已经遍历玩了，则还没有找到就是没有子目录
                            readCount++;
                            if (subdirs.Length == readCount)
                            {
                                a[dsname + dirname] = true;
                                break;
                            }
                        }
                    }
                }

                foreach (var d in dirs)
                {
                    string[] subdirs = DirectoryUtil.GetDirectories(d, "*", true);

                    string[] tmpdsname = d.Split('\\');
                    string dsname = tmpdsname[tmpdsname.Length - 1].Split('$')[0];

                    foreach (var dir in subdirs)
                    {
                        string[] tmpdirsname = dir.Split('\\');
                        string dirname = tmpdirsname[tmpdirsname.Length - 1].Split('$')[0];

                        if (dirname.Substring(0, 1) == "第" && dirname.Substring(dirname.Length - 1, 1) == "章") continue;
                        // 是子目录
                        if (a[dsname + dirname])
                        {
                            if (dir.Length > 250)
                            {
                                MessageDxUtil.ShowTips("目录超过限制长度[" + dir + "]");
                                continue;
                            }

                            if ((dir + "\\" + tmpdirsname[tmpdirsname.Length - 1].Split('$')[1] + ".txt").Length > 250)
                                FileUtil.WriteText(dir + "\\1.txt", tmpdirsname[tmpdirsname.Length - 1].Split('$')[1], Encoding.UTF8);
                            else
                                FileUtil.WriteText(dir + "\\" + tmpdirsname[tmpdirsname.Length - 1].Split('$')[1] + ".txt", tmpdirsname[tmpdirsname.Length - 1].Split('$')[1], Encoding.UTF8);
                        }
                    }
                }

                MessageDxUtil.ShowTips("操作完成");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            string path = FileDialogHelper.OpenDir();

            if (!string.IsNullOrEmpty(path))
            {
                string[] dirs = DirectoryUtil.GetDirectories(path, "*", true);

                 // 判断是否是子目录
                foreach (var dir in dirs)
                {

                    if (dir.Length > 250)
                    {
                        MessageDxUtil.ShowTips("目录超过限制长度[" + dir + "]");
                        continue;
                    }

                    if (DirectoryUtil.IsEmptyDirectory(dir))
                    {
                        string[] tmp = dir.Split('_');

                        try
                        {
                            if ((dir + "\\" + tmp[tmp.Length - 1] + ".txt").Length > 250)
                                FileUtil.WriteText(dir + "\\1.txt", dir, Encoding.UTF8);
                            else
                                FileUtil.WriteText(dir + "\\" + tmp[tmp.Length - 1] + ".txt", dir, Encoding.UTF8);
                        }
                        catch (Exception ex) {
                            MessageDxUtil.ShowTips(ex.Message);
                        }
                    }
                }
            }

            MessageDxUtil.ShowTips("操作完成");
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            string path = txtPath.Text.Trim();
            if (string.IsNullOrEmpty(path)) { 
                path = FileDialogHelper.OpenDir();
            }

            string fileRecord = "F:\\oldfile.txt";
            string fileNewReocrd = "F:\\newfile.txt";
            if (FileUtil.IsExistFile(fileRecord))
                FileUtil.DeleteFile(fileRecord);
            FileUtil.CreateFile(fileRecord);

            if (FileUtil.IsExistFile(fileNewReocrd))
                FileUtil.DeleteFile(fileNewReocrd);
            FileUtil.CreateFile(fileNewReocrd);

            Int32 copyCount = 0;

            if (!string.IsNullOrEmpty(path))
            {
                string[] files = DirectoryUtil.GetFileNames(path, "*", true);

                foreach (var file in files) {
                    FileUtil.AppendText(fileRecord, file + "\r\n", Encoding.UTF8);

                    string oldfilerelatefile = file.Replace(path + "\\", "");
                    string newfllerelatefile = oldfilerelatefile.Replace("\\", "_");
                    if (FileUtil.IsExistFile(file) && oldfilerelatefile != newfllerelatefile)
                    {
                        File.Move(file, path + "\\" + newfllerelatefile);
                        copyCount++;
                    }   
                }

                files = DirectoryUtil.GetFileNames(path, "*", true);
                foreach (var file in files)
                {
                    FileUtil.AppendText(fileNewReocrd, file + "\r\n", Encoding.UTF8);
                }
            }

            MessageDxUtil.ShowTips("操作完成， 一共成功复制" + copyCount + "个文件");
        }

        private void btnCopyByIndex_Click(object sender, EventArgs e)
        {
            string copyToPath = txtCopytoPath.Text.Trim();
            string sourcePath = txtSourcePath.Text.Trim();

            if (string.IsNullOrEmpty(copyToPath))
            {
                MessageDxUtil.ShowError("复制到对应文件夹未配置");
                return;
            }

            if (string.IsNullOrEmpty(sourcePath))
            {
                MessageDxUtil.ShowError("拷贝源文件夹未配置");
                return;
            }

            // 读取文件夹保存到字典 字段值为 第几部分_索引名字，拷贝全路径
            Dictionary<string, string> dic = new Dictionary<string, string>();

            string[] allDirs = DirectoryUtil.GetDirectories(copyToPath, "*", true);
            foreach (var dir in allDirs)
            {
                // 如果路径超长了 就不往下走了
                if (dir.Length > 245) continue;

                DirectoryInfo dirinfo = new DirectoryInfo(dir);

                // 合法路径
                if (dirinfo.Name.Split('$')[0].Contains("-") && dirinfo.Parent.Name.Split('$')[0].Contains("部分") && dirinfo.Parent.Name.Split('$')[0].Substring(0, 1) == "第") {
                    dic[dirinfo.Parent.Name.Split('$')[0] + "_" + dirinfo.Name.Split('$')[0]] = dir;
                }
            }

            Int32 copyCount = 0;
            string[] allFiles = DirectoryUtil.GetAllFileNames(sourcePath);
            foreach (var file in allFiles) {
                FileInfo fileInfo = new FileInfo(file);
                string indexStr = fileInfo.Name.Split(' ')[0];
                string pattern = @"第\w+部分";
                Regex regex = new Regex(pattern);
                if (regex.IsMatch(fileInfo.DirectoryName))
                {
                    if (dic.ContainsKey(regex.Match(fileInfo.DirectoryName).Value + "_" + fileInfo.Name.Split(' ')[0]))
                    {
                        copyCount++;
                        File.Move(fileInfo.FullName, dic[ regex.Match(fileInfo.DirectoryName).Value + "_" + fileInfo.Name.Split(' ')[0] ] + "\\"+ fileInfo.Name);   
                    }
                }
                else
                    continue;
                    
            }

            MessageDxUtil.ShowTips(string.Format("操作成功, 目录下一共{0}个文件,成功拷贝{1}个文件", allFiles.Length, copyCount));
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            string copyToPath = txtCopytoPath.Text.Trim();
            string sourcePath = txtSourcePath.Text.Trim();

            if (string.IsNullOrEmpty(copyToPath))
            {
                MessageDxUtil.ShowError("复制到对应文件夹未配置");
                return;
            }

            if (string.IsNullOrEmpty(sourcePath))
            {
                MessageDxUtil.ShowError("拷贝源文件夹未配置");
                return;
            }

            // 读取文件夹保存到字典 字段值为 第几部分_索引名字，拷贝全路径
            Dictionary<string, string> dic = new Dictionary<string, string>();

            string[] allDirs = DirectoryUtil.GetDirectories(copyToPath, "*", true);
            foreach (var dir in allDirs)
            {
                // 如果路径超长了 就不往下走了
                if (dir.Length > 245) continue;

                DirectoryInfo dirinfo = new DirectoryInfo(dir);

                // 没找到
                if (dirinfo.Name.Split('$').Length <= 1) continue;

                // 合法路径
                if (dirinfo.Parent.Name.Split('$')[0].Contains("部分") && dirinfo.Parent.Name.Split('$')[0].Substring(0, 1) == "第")
                {
                    dic[dirinfo.Parent.Name.Split('$')[0] + "_" + dirinfo.Name.Split('$')[1]] = dir;
                }
            }

            Int32 copyCount = 0;
            string[] allFiles = DirectoryUtil.GetAllFileNames(sourcePath);
            foreach (var file in allFiles)
            {
                FileInfo fileInfo = new FileInfo(file);

                // 没找到
                if (fileInfo.Name.Split(' ').Length <= 1) continue;

                string indexStr = fileInfo.Name.Split(' ')[1];
                string pattern = @"第\w+部分";
                Regex regex = new Regex(pattern);
                if (regex.IsMatch(fileInfo.DirectoryName))
                {
                    if (dic.ContainsKey(regex.Match(fileInfo.DirectoryName).Value + "_" + fileInfo.Name.Split(' ')[1].Replace(fileInfo.Extension, "")))
                    {
                        copyCount++;
                        File.Move(fileInfo.FullName, dic[regex.Match(fileInfo.DirectoryName).Value + "_" + fileInfo.Name.Split(' ')[1].Replace(fileInfo.Extension, "")] + "\\" + fileInfo.Name);
                    }
                }
                else
                    continue;

            }

            MessageDxUtil.ShowTips(string.Format("操作成功, 目录下一共{0}个文件,成功拷贝{1}个文件", allFiles.Length, copyCount));
        }

        /// <summary>
        /// 查找不适用文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFindNotSetFile_Click(object sender, EventArgs e)
        {
            string path = txtPath.Text.Trim();
            if (string.IsNullOrEmpty(path))
            {
                path = FileDialogHelper.OpenDir();
            }

            if (!DirectoryUtil.IsExistDirectory(path)) {
                MessageDxUtil.ShowTips("文件路径不可用");
                return;
            }

            string recordFileName = "\\" + "不适用文件.txt";
            if (FileUtil.FileIsExist(path + recordFileName))
            {
                FileUtil.DeleteFile(path + recordFileName);
            }
            FileUtil.CreateFile(path + recordFileName);

            string[] allFiles = DirectoryUtil.GetAllFileNames(path);
            foreach (var file in allFiles)
            {
                FileInfo fileInfo = new FileInfo(file);
                if (fileInfo.Name.Contains("不适用")) {
                    FileUtil.AppendText(path + recordFileName, string.Format("{0}\t\t\t{1}\r\n", fileInfo.Name, fileInfo.FullName), Encoding.UTF8);
                }
            }

            MessageDxUtil.ShowTips("操作完成");
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            string path = FileDialogHelper.OpenDir();

            string recordFileName = "\\" + "缺少文件.txt";
            if (FileUtil.FileIsExist(path + recordFileName))
            {
                FileUtil.DeleteFile(path + recordFileName);
            }
            FileUtil.CreateFile(path + recordFileName);


            if (!string.IsNullOrEmpty(path))
            {
                string[] dirs = DirectoryUtil.GetDirectories(path);

                Dictionary<string, bool> a = new Dictionary<string, bool>();
                // 判断是否是子目录
                foreach (var d in dirs)
                {
                    string[] subdirs = DirectoryUtil.GetDirectories(d, "*", true);

                    string[] tmpdsname = d.Split('\\');
                    string dsname = tmpdsname[tmpdsname.Length - 1].Split('$')[0];

                    foreach (var dir in subdirs)
                    {
                        string[] tmpdirsname = dir.Split('\\');
                        string dirname = tmpdirsname[tmpdirsname.Length - 1].Split('$')[0];

                        a[dsname + dirname] = false;

                        if (dirname.Substring(0, 1) == "第" && dirname.Substring(dirname.Length - 1, 1) == "章") continue;

                        Int32 readCount = 0;
                        foreach (var dir2 in subdirs)
                        {
                            string[] tmpdir2sname = dir2.Split('\\');
                            string dir2name = tmpdir2sname[tmpdir2sname.Length - 1].Split('$')[0];

                            if (dir2name != dirname && dirname.Contains(dir2name) && dirname.IndexOf(dir2name) == 0)
                            {
                                a[dsname + dirname] = true;
                                break;
                            }

                            if (dir2name != dirname && dir2name.Contains(dirname) && dir2name.IndexOf(dirname) == 0)
                            {
                                a[dsname + dirname] = false;
                                break;
                            }

                            // 已经遍历玩了，则还没有找到就是没有子目录
                            readCount++;
                            if (subdirs.Length == readCount)
                            {
                                a[dsname + dirname] = true;
                                break;
                            }
                        }
                    }
                }

                foreach (var d in dirs)
                {
                    string[] subdirs = DirectoryUtil.GetDirectories(d, "*", true);

                    string[] tmpdsname = d.Split('\\');
                    string dsname = tmpdsname[tmpdsname.Length - 1].Split('$')[0];

                    foreach (var dir in subdirs)
                    {
                        string[] tmpdirsname = dir.Split('\\');
                        string dirname = tmpdirsname[tmpdirsname.Length - 1].Split('$')[0];

                        if (dirname.Substring(0, 1) == "第" && dirname.Substring(dirname.Length - 1, 1) == "章") continue;
                        // 是子目录
                        if (a[dsname + dirname])
                        {
                            if (dir.Length > 250)
                            {
                                MessageDxUtil.ShowTips("目录超过限制长度[" + dir + "]");
                                continue;
                            }

                            if (DirectoryUtil.IsEmptyDirectory(dir))
                            {
                                FileUtil.AppendText(path + recordFileName, string.Format("{0}\r\n", dir), Encoding.UTF8);
                            }    
                        }
                    }
                }

                MessageDxUtil.ShowTips("操作完成");
            }
        }


    }
}
