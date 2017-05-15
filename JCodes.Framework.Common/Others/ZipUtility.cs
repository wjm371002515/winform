using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;

namespace JCodes.Framework.Common
{
    /// <summary>
    /// 使用ICSharpCode.SharpZipLib进行文件（包括文件夹）压缩的辅助类
    /// </summary>
    public static class ZipUtility
    {
        #region 文件夹压缩、解压缩

        /// <summary>
        /// 压缩指定文件列表到一个指定的内存流里面，可设置密码
        /// </summary>
        /// <param name="fileList">文件路径列表</param>
        /// <param name="stream">指定的内存流</param>
        /// <param name="password">压缩密码</param>
        public static void ZipFiles(List<string> fileList, Stream stream, string password)
        {
            FileStream ostream;
            byte[] obuffer;
            ZipOutputStream oZipStream = new ZipOutputStream(stream);
            if (!string.IsNullOrEmpty(password))
            {
                oZipStream.Password = password;
            }
            oZipStream.SetLevel(9); // 设置最大压缩率

            ZipEntry oZipEntry;
            foreach (string file in fileList)
            {
                string fileName = Path.GetFileName(file);
                oZipEntry = new ZipEntry(fileName);
                oZipStream.PutNextEntry(oZipEntry);

                if (!file.EndsWith(@"/")) // 如果文件以 '/' 结束，则是目录
                {
                    ostream = File.OpenRead(file);
                    obuffer = new byte[ostream.Length];
                    ostream.Read(obuffer, 0, obuffer.Length);
                    oZipStream.Write(obuffer, 0, obuffer.Length);
                }
            }
            oZipStream.Finish();
            oZipStream.Close();
        }

        /// <summary>
        /// 压缩指定文件列表到一个指定的压缩文件里面，可设置密码
        /// </summary>
        /// <param name="fileList">文件路径列表</param>
        /// <param name="outputPathAndFile">输出的压缩文件全名</param>
        /// <param name="password">压缩密码</param>
        public static void ZipFiles(List<string> fileList, string outputPathAndFile, string password)
        {
            FileStream ostream;
            byte[] obuffer;
            string outPath = outputPathAndFile;//inputFolderPath + @"\" + outputPathAndFile;
            ZipOutputStream oZipStream = new ZipOutputStream(File.Create(outPath));
            if (!string.IsNullOrEmpty(password))
            {
                oZipStream.Password = password;
            }
            oZipStream.SetLevel(9); // 设置最大压缩率

            ZipEntry oZipEntry;
            foreach (string file in fileList)
            {
                string fileName = Path.GetFileName(file);
                oZipEntry = new ZipEntry(fileName);
                oZipStream.PutNextEntry(oZipEntry);

                if (!file.EndsWith(@"/")) // 如果文件以 '/' 结束，则是目录
                {
                    ostream = File.OpenRead(file);
                    obuffer = new byte[ostream.Length];
                    ostream.Read(obuffer, 0, obuffer.Length);
                    oZipStream.Write(obuffer, 0, obuffer.Length);
                }
            }
            oZipStream.Finish();
            oZipStream.Close();
        }

        /// <summary>
        /// 压缩文件中的文件，可设置密码
        /// </summary>
        /// <param name="inputFolderPath">输入的文件夹</param>
        /// <param name="outputPathAndFile">输出的压缩文件全名</param>
        /// <param name="password">压缩密码</param>
        public static void ZipFiles(string inputFolderPath, string outputPathAndFile, string password)
        {
            List<string> fileList = GenerateFileList(inputFolderPath);
            int TrimLength = (Directory.GetParent(inputFolderPath)).ToString().Length;
            // find number of chars to remove   // from orginal file path
            TrimLength += 1; //remove '\'

            FileStream ostream;
            byte[] obuffer;
            string outPath = outputPathAndFile;//inputFolderPath + @"\" + outputPathAndFile;
            ZipOutputStream oZipStream = new ZipOutputStream(File.Create(outPath));
            if (!string.IsNullOrEmpty(password))
            {
                oZipStream.Password = password;
            }
            oZipStream.SetLevel(9); // 设置最大压缩率

            ZipEntry oZipEntry;
            foreach (string file in fileList)
            {
                oZipEntry = new ZipEntry(file.Remove(0, TrimLength));
                oZipStream.PutNextEntry(oZipEntry);

                if (!file.EndsWith(@"/")) // 如果文件以 '/' 结束，则是目录
                {
                    ostream = File.OpenRead(file);
                    obuffer = new byte[ostream.Length];
                    ostream.Read(obuffer, 0, obuffer.Length);
                    oZipStream.Write(obuffer, 0, obuffer.Length);
                }
            }
            oZipStream.Finish();
            oZipStream.Close();
        }

        /// <summary>
        /// 根据文件夹生成文件列表
        /// </summary>
        /// <param name="Dir">指定文件夹</param>
        /// <returns></returns>
        private static List<string> GenerateFileList(string Dir)
        {
            List<string> fileList = new List<string>();
            bool Empty = true;
            foreach (string file in Directory.GetFiles(Dir))
            {
                fileList.Add(file);
                Empty = false;
            }

            if (Empty)
            {
                //加入完全为空的目录
                if (Directory.GetDirectories(Dir).Length == 0)
                {
                    fileList.Add(Dir + @"/");
                }
            }

            foreach (string dirs in Directory.GetDirectories(Dir)) // 递归目录
            {
                foreach (string file in GenerateFileList(dirs))
                {
                    fileList.Add(file);
                }
            }
            return fileList;
        }

        /// <summary>
        /// 解压文件到指定的目录，可设置密码、删除原文件等
        /// </summary>
        /// <param name="zipPathAndFile">压缩文件全名</param>
        /// <param name="outputFolder">解压输出文件目录</param>
        /// <param name="password">解压密码</param>
        /// <param name="deleteZipFile">是否删除原文件（压缩文件）</param>
        public static void UnZipFiles(string zipPathAndFile, string outputFolder, string password, bool deleteZipFile)
        {
            using (ZipInputStream s = new ZipInputStream(File.OpenRead(zipPathAndFile)))
            {
                if (password != null && password != String.Empty)
                {
                    s.Password = password;
                }

                ZipEntry theEntry;
                string tmpEntry = String.Empty;
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    #region 遍历每个Entry对象进行解压处理
                    string directoryName = outputFolder;
                    string fileName = Path.GetFileName(theEntry.Name);
                    if (directoryName != "")
                    {
                        Directory.CreateDirectory(directoryName);
                    }

                    if (fileName != String.Empty)
                    {
                        if (theEntry.Name.IndexOf(".ini") < 0)
                        {
                            string fullPath = directoryName + "\\" + theEntry.Name;
                            fullPath = fullPath.Replace("\\ ", "\\");
                            string fullDirPath = Path.GetDirectoryName(fullPath);
                            if (!Directory.Exists(fullDirPath)) Directory.CreateDirectory(fullDirPath);
                            using (FileStream streamWriter = File.Create(fullPath))
                            {
                                #region 写入文件流
                                int size = 2048;
                                byte[] data = new byte[2048];
                                while (true)
                                {
                                    size = s.Read(data, 0, data.Length);
                                    if (size > 0)
                                    {
                                        streamWriter.Write(data, 0, size);
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                #endregion
                            }
                        }
                    }
                    #endregion
                }
            }

            if (deleteZipFile)
            {
                File.Delete(zipPathAndFile);
            }
        }

        #endregion

        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="fileToZip">待压缩文件路径</param>
        /// <param name="zipedFile">压缩后文件路径</param>
        /// <returns></returns>
        public static bool ZipFile(string fileToZip, string zipedFile)
        {
            try
            {
                FastZip fastZip = new FastZip();
                bool recurse = true;//压缩后的文件名，压缩目录 ，是否递归 
                                
                fastZip.CreateZip(fileToZip, zipedFile, recurse, "");
                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// 解压缩以后的文件名和路径，压缩前的路径
        /// </summary>
        /// <param name="zipFile">压缩的文件路径</param>
        /// <param name="targetDirectory">解压后的文件路径</param>
        /// <returns></returns>
        public static Boolean UnZipFile(string zipFile, string targetDirectory)
        {
            try
            {
                FastZip fastZip = new FastZip();
                fastZip.ExtractZip(zipFile, targetDirectory, "");
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
