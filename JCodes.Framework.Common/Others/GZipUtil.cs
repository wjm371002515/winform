using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;
using Microsoft.Win32;
using System.Collections;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.jCodesenum;

namespace JCodes.Framework.Common.Office
{
    /// <summary>
    /// 压缩文本、字节或者文件的压缩辅助类
    /// </summary>
    public class GZipUtil
    {
        /// <summary>
        /// 压缩字符串
        /// </summary>
        /// <param name="text">待压缩的文本</param>
        /// <returns>压缩后的文本内容</returns>
        public static string Compress(string text)
        {
            // convert text to bytes
            byte[] buffer = Encoding.UTF8.GetBytes(text);
            // get a stream
            MemoryStream ms = new MemoryStream();
            // get ready to zip up our stream
            using (GZipStream zip = new GZipStream(ms, CompressionMode.Compress, true))
            {
                // compress the data into our buffer
                zip.Write(buffer, 0, buffer.Length);
            }
            // reset our position in compressed stream to the start
            ms.Position = 0;
            // get the compressed data
            byte[] compressed = ms.ToArray();
            ms.Read(compressed, 0, compressed.Length);
            // prepare final data with header that indicates length
            byte[] gzBuffer = new byte[compressed.Length + 4];
            //copy compressed data 4 bytes from start of final header
            System.Buffer.BlockCopy(compressed, 0, gzBuffer, 4, compressed.Length);
            // copy header to first 4 bytes
            System.Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gzBuffer, 0, 4);
            // convert back to string and return
            return Convert.ToBase64String(gzBuffer);
        }

        /// <summary>
        /// 解压字符串
        /// </summary>
        /// <param name="compressedText">待解压缩的文本内容</param>
        /// <returns>解压后的原始文本内容</returns>
        public static string Decompress(string compressedText)
        {
            // get string as bytes
            byte[] gzBuffer = Convert.FromBase64String(compressedText);
            // prepare stream to do uncompression
            MemoryStream ms = new MemoryStream();
            // get the length of compressed data
            int msgLength = BitConverter.ToInt32(gzBuffer, 0);
            // uncompress everything besides the header
            ms.Write(gzBuffer, 4, gzBuffer.Length - 4);
            // prepare final buffer for just uncompressed data
            byte[] buffer = new byte[msgLength];
            // reset our position in stream since we're starting over
            ms.Position = 0;
            // unzip the data through stream
            GZipStream zip = new GZipStream(ms, CompressionMode.Decompress);
            // do the unzip
            zip.Read(buffer, 0, buffer.Length);
            // convert back to string and return
            return Encoding.UTF8.GetString(buffer);
        }

        /// <summary>
        /// 压缩流对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="stream">流数据</param>
        /// <param name="mode">压缩类型</param>
        /// <returns></returns>
        public static T GZip<T>(Stream stream, CompressionMode mode) where T : Stream
        {
            byte[] writeData = new byte[4096];
            T ms = default(T);
            using (Stream sg = new GZipStream(stream, mode))
            {
                while (true)
                {
                    Array.Clear(writeData, 0, writeData.Length);
                    int size = sg.Read(writeData, 0, writeData.Length);
                    if (size > 0)
                    {
                        ms.Write(writeData, 0, size);
                    }
                    else
                    {
                        break;
                    }
                }
                return ms;
            }
        }

        /// <summary>
        /// 压缩字节
        /// </summary>
        /// <param name="bytData">待压缩字节</param>
        /// <returns>压缩后的字节数组</returns>
        public static byte[] Compress(byte[] bytData)
        {
            using (MemoryStream stream = GZip<MemoryStream>(new MemoryStream(bytData), CompressionMode.Compress))
            {
                return stream.ToArray();
            }
        }

        /// <summary>
        /// 解压字节
        /// </summary>
        /// <param name="bytData">待解压缩字节</param>
        /// <returns>解压后的原始字节内容</returns>
        public static byte[] Decompress(byte[] bytData)
        {
            using (MemoryStream stream = GZip<MemoryStream>(new MemoryStream(bytData), CompressionMode.Decompress))
            {
                return stream.ToArray();
            }
        }

        /// <summary>
        /// 压缩Object对象到字节数组
        /// </summary>
        /// <param name="obj">待压缩对象</param>
        /// <returns>压缩后的字节数组</returns>
        public static byte[] ObjectToGZip(object obj)
        {
            byte[] byteTempArray = ObjectToByteArray(obj);
            return GZipUtil.Compress(byteTempArray);
        }

        /// <summary>
        /// 从压缩的字节数组转换到Object对象
        /// </summary>
        /// <param name="byteArray">待解压缩的字节数据</param>
        /// <returns>对象</returns>
        public static object GZipToObject(byte[] byteArray)
        {
            byte[] byteTempArray = GZipUtil.Decompress(byteArray);
            return ByteArrayToObject(byteTempArray);
        }
        
        private static byte[] ObjectToByteArray(object obj)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream())
            {
                formatter.Serialize(stream, obj);
                return stream.ToArray();
            }
        }

        private static object ByteArrayToObject(byte[] byteArray)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream(byteArray))
            {
                return formatter.Deserialize(stream);
            }
        }

        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="lpSourceFolder">包括在zip文件中的文件夹路径，所有文件，包括子文件夹中的文件将包括在内。</param>
        /// <param name="lpDestFolder">写入到Zip文件的目标文件夹</param>
        /// <param name="zipFileName">zip文件名称</param>
        public static GZipResult Compress(string lpSourceFolder, string lpDestFolder, string zipFileName)
        {
            return Compress(lpSourceFolder, "*.*", SearchOption.AllDirectories, lpDestFolder, zipFileName, true);
        }

        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="lpSourceFolder">包括在zip文件中的源文件夹路径</param>
        /// <param name="searchPattern">搜索模式 (例如 "*.*" or "*.txt" or "*.gif") 以标识那些文件将被包含到Zip文件里面</param>
        /// <param name="searchOption">指定是搜索当前目录，还是搜索当前目录及其所有子目录</param>
        /// <param name="lpDestFolder">写入zip目标文件夹的路径</param>
        /// <param name="zipFileName">Zip文件名</param>
        /// <param name="deleteTempFile">布尔值，如果为true则删除中间临时文件，false则在lpDestFolder保留临时文件(调试用)</param>
        public static GZipResult Compress(string lpSourceFolder, string searchPattern, SearchOption searchOption, string lpDestFolder, string zipFileName, bool deleteTempFile)
        {
            DirectoryInfo di = new DirectoryInfo(lpSourceFolder);
            FileInfo[] files = di.GetFiles("*.*", searchOption);
            return Compress(files, lpSourceFolder, lpDestFolder, zipFileName, deleteTempFile);
        }

        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="files">在zip文件中包含的FileInfo对象数组</param>
        /// <param name="folders">文件夹字符串数组</param>
        /// <param name="lpBaseFolder">
        /// 基础文件夹，在创建的zip文件中存储的文件的相对路径。例如, 如果lpBaseFolder 是 'C:\zipTest\Files\',
        /// 当存在一个文件 'C:\zipTest\Files\folder1\sample.txt' 在数组'files'里面， 则 sample.txt 的相对路径是 'folder1/sample.txt'
        /// </param>
        /// <param name="lpDestFolder">写入Zip文件的文件夹</param>
        /// <param name="zipFileName">Zip文件名</param>
        public static GZipResult Compress(FileInfo[] files, string[] folders, string lpBaseFolder, string lpDestFolder, string zipFileName)
        {
            //support compress folder
            List<FileInfo> list = new List<FileInfo>();
            foreach (FileInfo li in files)
                list.Add(li);

            foreach (string str in folders)
            {
                DirectoryInfo di = new DirectoryInfo(str);
                foreach (FileInfo info in di.GetFiles("*.*", SearchOption.AllDirectories))
                {
                    list.Add(info);
                }
            }

            return Compress(list.ToArray(), lpBaseFolder, lpDestFolder, zipFileName, true);
        }

        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="files">在zip文件中包含的FileInfo对象数组</param>
        /// <param name="lpBaseFolder">
        /// 基础文件夹，在创建的zip文件中存储的文件的相对路径。例如, 如果lpBaseFolder 是 'C:\zipTest\Files\',
        /// 当存在一个文件 'C:\zipTest\Files\folder1\sample.txt' 在数组'files'里面， 则 sample.txt 的相对路径是 'folder1/sample.txt'
        /// </param>
        /// <param name="lpDestFolder">写入Zip文件的文件夹</param>
        /// <param name="zipFileName">Zip文件名</param>
        public static GZipResult Compress(FileInfo[] files, string lpBaseFolder, string lpDestFolder, string zipFileName)
        {
            return Compress(files, lpBaseFolder, lpDestFolder, zipFileName, true);
        }

        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="files">在zip文件中包含的FileInfo对象数组</param>
        /// <param name="lpBaseFolder">
        /// 基础文件夹，在创建的zip文件中存储的文件的相对路径。例如, 如果lpBaseFolder 是 'C:\zipTest\Files\',
        /// 当存在一个文件 'C:\zipTest\Files\folder1\sample.txt' 在数组'files'里面， 则 sample.txt 的相对路径是 'folder1/sample.txt'
        /// </param>
        /// <param name="lpDestFolder">写入Zip文件的文件夹</param>
        /// <param name="zipFileName">Zip文件名</param>
        /// <param name="deleteTempFile">布尔值，如果为true则删除中间临时文件，false则在lpDestFolder保留临时文件(调试用)</param>
        public static GZipResult Compress(FileInfo[] files, string lpBaseFolder, string lpDestFolder, string zipFileName, bool deleteTempFile)
        {
            GZipResult result = new GZipResult();

            try
            {
                if (!lpDestFolder.EndsWith("\\"))
                {
                    lpDestFolder += "\\";
                }

                string lpTempFile = lpDestFolder + zipFileName + ".tmp";
                string lpZipFile = lpDestFolder + zipFileName;

                result.TempFile = lpTempFile;
                result.ZipFile = lpZipFile;

                if (files != null && files.Length > 0)
                {
                    CreateTempFile(files, lpBaseFolder, lpTempFile, result);

                    if (result.FileCount > 0)
                    {
                        CreateZipFile(lpTempFile, lpZipFile, result);
                    }

                    // delete the temp file
                    if (deleteTempFile)
                    {
                        File.Delete(lpTempFile);
                        result.TempFileDeleted = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(GZipUtil));
                result.Errors = true;
            }
            return result;
        }

        private static void CreateZipFile(string lpSourceFile, string lpZipFile, GZipResult result)
        {
            byte[] buffer;
            int count = 0;
            FileStream fsOut = null;
            FileStream fsIn = null;
            GZipStream gzip = null;

            // compress the file into the zip file
            try
            {
                fsOut = new FileStream(lpZipFile, FileMode.Create, FileAccess.Write, FileShare.None);
                gzip = new GZipStream(fsOut, CompressionMode.Compress, true);

                fsIn = new FileStream(lpSourceFile, FileMode.Open, FileAccess.Read, FileShare.Read);
                buffer = new byte[fsIn.Length];
                count = fsIn.Read(buffer, 0, buffer.Length);
                fsIn.Close();
                fsIn = null;

                // compress to the zip file
                gzip.Write(buffer, 0, buffer.Length);

                result.ZipFileSize = fsOut.Length;
                result.CompressionPercent = GetCompressionPercent(result.TempFileSize, result.ZipFileSize);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(GZipUtil));
                result.Errors = true;
            }
            finally
            {
                if (gzip != null)
                {
                    gzip.Close();
                    gzip = null;
                }
                if (fsOut != null)
                {
                    fsOut.Close();
                    fsOut = null;
                }
                if (fsIn != null)
                {
                    fsIn.Close();
                    fsIn = null;
                }
            }
        }

        private static void CreateTempFile(FileInfo[] files, string lpBaseFolder, string lpTempFile, GZipResult result)
        {
            byte[] buffer;
            int count = 0;
            byte[] header;
            string fileHeader = null;
            string fileModDate = null;
            string lpFolder = null;
            int fileIndex = 0;
            string lpSourceFile = null;
            string vpSourceFile = null;
            GZipFileInfo gzf = null;
            FileStream fsOut = null;
            FileStream fsIn = null;

            if (files != null && files.Length > 0)
            {
                try
                {
                    result.Files = new GZipFileInfo[files.Length];

                    // open the temp file for writing
                    fsOut = new FileStream(lpTempFile, FileMode.Create, FileAccess.Write, FileShare.None);

                    foreach (FileInfo fi in files)
                    {
                        lpFolder = fi.DirectoryName + "\\";
                        try
                        {
                            gzf = new GZipFileInfo();
                            gzf.Index = fileIndex;

                            // read the source file, get its virtual path within the source folder
                            lpSourceFile = fi.FullName;
                            gzf.LocalPath = lpSourceFile;
                            vpSourceFile = lpSourceFile.Replace(lpBaseFolder, string.Empty);
                            vpSourceFile = vpSourceFile.Replace("\\", "/");
                            gzf.RelativePath = vpSourceFile;

                            fsIn = new FileStream(lpSourceFile, FileMode.Open, FileAccess.Read, FileShare.Read);
                            buffer = new byte[fsIn.Length];
                            count = fsIn.Read(buffer, 0, buffer.Length);
                            fsIn.Close();
                            fsIn = null;

                            fileModDate = fi.LastWriteTimeUtc.ToString();
                            gzf.ModifiedDate = fi.LastWriteTimeUtc;
                            gzf.Length = buffer.Length;

                            fileHeader = fileIndex.ToString() + "," + vpSourceFile + "," + fileModDate + "," + buffer.Length.ToString() + "\n";
                            header = Encoding.Default.GetBytes(fileHeader);

                            fsOut.Write(header, 0, header.Length);
                            fsOut.Write(buffer, 0, buffer.Length);
                            fsOut.WriteByte(10); // linefeed

                            gzf.AddedToTempFile = true;

                            // update the result object
                            result.Files[fileIndex] = gzf;

                            // increment the fileIndex
                            fileIndex++;
                        }
                        catch (Exception ex)
                        {
                            LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(GZipUtil));
                            result.Errors = true;
                        }
                        finally
                        {
                            if (fsIn != null)
                            {
                                fsIn.Close();
                                fsIn = null;
                            }
                        }
                        if (fsOut != null)
                        {
                            result.TempFileSize = fsOut.Length;
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(GZipUtil));
                    result.Errors = true;
                }
                finally
                {
                    if (fsOut != null)
                    {
                        fsOut.Close();
                        fsOut = null;
                    }
                }
            }

            result.FileCount = fileIndex;
        }

        /// <summary>
        /// 解压文件
        /// </summary>
        /// <param name="lpSourceFolder">zip文件的源目录</param>
        /// <param name="lpDestFolder">解压到的目录</param>
        /// <param name="zipFileName">Zip文件名</param>
        /// <returns></returns>
        public static GZipResult Decompress(string lpSourceFolder, string lpDestFolder, string zipFileName)
        {
            return Decompress(lpSourceFolder, lpDestFolder, zipFileName, true, true, null, null, 4096);
        }

        /// <summary>
        /// 解压文件
        /// </summary>
        /// <param name="lpSourceFolder">zip文件的源目录</param>
        /// <param name="lpDestFolder">解压到的目录</param>
        /// <param name="zipFileName">Zip文件名</param>
        /// <param name="writeFiles"></param>
        /// <param name="addExtension">增加后缀名</param>
        /// <returns></returns>
        public static GZipResult Decompress(string lpSourceFolder, string lpDestFolder, string zipFileName, bool writeFiles, string addExtension)
        {
            return Decompress(lpSourceFolder, lpDestFolder, zipFileName, true, writeFiles, addExtension, null, 4096);
        }

        /// <summary>
        /// 解压文件
        /// </summary>
        /// <param name="lpSrcFolder">zip文件的源目录</param>
        /// <param name="lpDestFolder">解压到的目录</param>
        /// <param name="zipFileName">Zip文件名</param>
        /// <param name="deleteTempFile">布尔值，如果为true则删除中间临时文件，false则在lpDestFolder保留临时文件(调试用)</param>
        /// <param name="writeFiles"></param>
        /// <param name="addExtension">增加后缀名</param>
        /// <param name="htFiles"></param>
        /// <param name="bufferSize"></param>
        /// <returns></returns>
        public static GZipResult Decompress(string lpSrcFolder, string lpDestFolder, string zipFileName,
            bool deleteTempFile, bool writeFiles, string addExtension, Hashtable htFiles, int bufferSize)
        {
            GZipResult result = new GZipResult();

            if (!lpSrcFolder.EndsWith("\\"))
            {
                lpSrcFolder += "\\";
            }

            if (!lpDestFolder.EndsWith("\\"))
            {
                lpDestFolder += "\\";
            }

            string lpTempFile = lpSrcFolder + zipFileName + ".tmp";
            string lpZipFile = lpSrcFolder + zipFileName;

            result.TempFile = lpTempFile;
            result.ZipFile = lpZipFile;

            string line = null;
            string lpFilePath = null;
            string lpFolder = null;
            GZipFileInfo gzf = null;
            FileStream fsTemp = null;
            ArrayList gzfs = new ArrayList();
            bool write = false;

            if (string.IsNullOrEmpty(addExtension))
            {
                addExtension = string.Empty;
            }
            else if (!addExtension.StartsWith("."))
            {
                addExtension = "." + addExtension;
            }

            // extract the files from the temp file
            try
            {
                fsTemp = UnzipToTempFile(lpZipFile, lpTempFile, result);
                if (fsTemp != null)
                {
                    while (fsTemp.Position != fsTemp.Length)
                    {
                        line = null;
                        while (string.IsNullOrEmpty(line) && fsTemp.Position != fsTemp.Length)
                        {
                            line = ReadLine(fsTemp);
                        }

                        if (!string.IsNullOrEmpty(line))
                        {
                            gzf = new GZipFileInfo();
                            if (gzf.ParseFileInfo(line) && gzf.Length > 0)
                            {
                                gzfs.Add(gzf);
                                lpFilePath = lpDestFolder + gzf.RelativePath;
                                lpFolder = GetFolder(lpFilePath);
                                gzf.LocalPath = lpFilePath;

                                write = false;
                                if (htFiles == null || htFiles.ContainsKey(gzf.RelativePath))
                                {
                                    gzf.RestoreRequested = true;
                                    write = writeFiles;
                                }

                                if (write)
                                {
                                    // make sure the folder exists
                                    if (!Directory.Exists(lpFolder))
                                    {
                                        Directory.CreateDirectory(lpFolder);
                                    }

                                    // read from fsTemp and write out the file
                                    gzf.Restored = WriteFile(fsTemp, gzf.Length, lpFilePath + addExtension, bufferSize);
                                }
                                else
                                {
                                    // need to advance fsTemp
                                    fsTemp.Position += gzf.Length;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(GZipUtil));
                result.Errors = true;
            }
            finally
            {
                if (fsTemp != null)
                {
                    fsTemp.Close();
                    fsTemp = null;
                }
            }

            // delete the temp file
            try
            {
                if (deleteTempFile)
                {
                    File.Delete(lpTempFile);
                    result.TempFileDeleted = true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(GZipUtil));
                result.Errors = true;
            }

            result.FileCount = gzfs.Count;
            result.Files = new GZipFileInfo[gzfs.Count];
            gzfs.CopyTo(result.Files);
            return result;
        }

        private static string ReadLine(FileStream fs)
        {
            string line = string.Empty;

            byte[] buffer = new byte[Const.BUFFSIZE];
            byte b = 0;
            byte lf = 10;
            int i = 0;

            while (b != lf)
            {
                b = (byte)fs.ReadByte();
                buffer[i] = b;
                i++;
            }

            line = System.Text.Encoding.Default.GetString(buffer, 0, i - 1);

            return line;
        }

        private static bool WriteFile(FileStream fs, int fileLength, string lpFile, int bufferSize)
        {
            bool success = false;
            FileStream fsFile = null;

            if (bufferSize == 0 || fileLength < bufferSize)
            {
                bufferSize = fileLength;
            }

            int count = 0;
            int remaining = fileLength;
            int readSize = 0;

            try
            {
                byte[] buffer = new byte[bufferSize];
                fsFile = new FileStream(lpFile, FileMode.Create, FileAccess.Write, FileShare.None);

                while (remaining > 0)
                {
                    if (remaining > bufferSize)
                    {
                        readSize = bufferSize;
                    }
                    else
                    {
                        readSize = remaining;
                    }

                    count = fs.Read(buffer, 0, readSize);
                    remaining -= count;

                    if (count == 0)
                    {
                        break;
                    }

                    fsFile.Write(buffer, 0, count);
                    fsFile.Flush();

                }
                fsFile.Flush();
                fsFile.Close();
                fsFile = null;

                success = true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(GZipUtil));
                success = false;
            }
            finally
            {
                if (fsFile != null)
                {
                    fsFile.Flush();
                    fsFile.Close();
                    fsFile = null;
                }
            }
            return success;
        }

        private static string GetFolder(string lpFilePath)
        {
            string lpFolder = lpFilePath;
            int index = lpFolder.LastIndexOf("\\");
            if (index != -1)
            {
                lpFolder = lpFolder.Substring(0, index + 1);
            }
            return lpFolder;
        }

        private static FileStream UnzipToTempFile(string lpZipFile, string lpTempFile, GZipResult result)
        {
            FileStream fsIn = null;
            GZipStream gzip = null;
            FileStream fsOut = null;
            FileStream fsTemp = null;

            byte[] buffer = new byte[Const.BUFFSIZE];
            int count = 0;

            try
            {
                fsIn = new FileStream(lpZipFile, FileMode.Open, FileAccess.Read, FileShare.Read);
                result.ZipFileSize = fsIn.Length;

                fsOut = new FileStream(lpTempFile, FileMode.Create, FileAccess.Write, FileShare.None);
                gzip = new GZipStream(fsIn, CompressionMode.Decompress, true);
                while (true)
                {
                    count = gzip.Read(buffer, 0, Const.BUFFSIZE);
                    if (count != 0)
                    {
                        fsOut.Write(buffer, 0, count);
                    }
                    if (count != Const.BUFFSIZE)
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(GZipUtil));
                result.Errors = true;
            }
            finally
            {
                if (gzip != null)
                {
                    gzip.Close();
                    gzip = null;
                }
                if (fsOut != null)
                {
                    fsOut.Close();
                    fsOut = null;
                }
                if (fsIn != null)
                {
                    fsIn.Close();
                    fsIn = null;
                }
            }

            fsTemp = new FileStream(lpTempFile, FileMode.Open, FileAccess.Read, FileShare.None);
            if (fsTemp != null)
            {
                result.TempFileSize = fsTemp.Length;
            }
            return fsTemp;
        }

        private static int GetCompressionPercent(long tempLen, long zipLen)
        {
            double tmp = (double)tempLen;
            double zip = (double)zipLen;
            double hundred = 100;

            double ratio = (tmp - zip) / tmp;
            double pcnt = ratio * hundred;

            return (int)pcnt;
        }
    }

    /// <summary>
    /// 要压缩的文件信息
    /// </summary>
    public class GZipFileInfo
    {
        /// <summary>
        /// 文件索引
        /// </summary>
        public int Index = 0;
        /// <summary>
        /// 文件相对路径，'/'
        /// </summary>
        public string RelativePath = null;
        public DateTime ModifiedDate;
        /// <summary>
        /// 文件内容长度
        /// </summary>
        public int Length = 0;
        public bool AddedToTempFile = false;
        public bool RestoreRequested = false;
        public bool Restored = false;
        /// <summary>
        /// 文件绝对路径,'\'
        /// </summary>
        public string LocalPath = null;
        public string Folder = null;

        /// <summary>
        /// 转换文件信息
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns></returns>
        public bool ParseFileInfo(string fileInfo)
        {
            bool success = false;
            try
            {
                if (!string.IsNullOrEmpty(fileInfo))
                {
                    // get the file information
                    string[] info = fileInfo.Split(',');
                    if (info != null && info.Length == 4)
                    {
                        this.Index = Convert.ToInt32(info[0]);
                        this.RelativePath = info[1].Replace("/", "\\");
                        this.ModifiedDate = Convert.ToDateTime(info[2]);
                        this.Length = Convert.ToInt32(info[3]);
                        success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(GZipUtil));
                success = false;
            }
            return success;
        }
    }

    /// <summary>
    /// 文件压缩后的压缩包类
    /// </summary>
    public class GZipResult
    {
        /// <summary>
        /// 压缩包中包含的所有文件,包括子目录下的文件
        /// </summary>
        public GZipFileInfo[] Files = null;
        /// <summary>
        /// 要压缩的文件数
        /// </summary>
        public int FileCount = 0;
        /// <summary>
        /// 临时文件大小
        /// </summary>
        public long TempFileSize = 0;
        /// <summary>
        /// 压缩文件大小
        /// </summary>
        public long ZipFileSize = 0;
        /// <summary>
        /// 压缩百分比
        /// </summary>
        public int CompressionPercent = 0;
        /// <summary>
        /// 临时文件
        /// </summary>
        public string TempFile = null;
        /// <summary>
        /// 压缩文件
        /// </summary>
        public string ZipFile = null;
        /// <summary>
        /// 是否删除临时文件
        /// </summary>
        public bool TempFileDeleted = false;
        /// <summary>
        /// 错误信息
        /// </summary>
        public bool Errors = false;

    }
}
