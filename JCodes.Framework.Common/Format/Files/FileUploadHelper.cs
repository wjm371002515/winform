using JCodes.Framework.Common.Files;
using JCodes.Framework.Entity;
using JCodes.Framework.jCodesenum.BaseEnum;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;
using JCodes.Framework.Common.Extension;
using JCodes.Framework.Common.Office;
using qiniu;
using System.Threading;
using JCodes.Framework.jCodesenum;
using System.Web;
using System.IO;

namespace JCodes.Framework.Common.Files
{
    /// <summary>
    /// 文件上传辅助类
    /// </summary>
    /// <example>
    /// </example>
    public class FileUploadHelper
    {
        private AppConfig _appConfig = null;

        private Dictionary<String, ErrornoInfo> dicErrInfo = (new JCodes.Framework.Common.ErrorInfo()).GetAllErrorInfo(); 

        public FileUploadHelper() {
            _appConfig = new AppConfig();
        }

        /// <summary>
        /// WEB方式上传附件到指定服务器上或者七牛云上
        /// </summary>
        /// <param name="imgPath"></param>
        /// <param name="file"></param>
        /// <param name="thunk"></param>
        /// <param name="thunks">为1 表示不分段上传</param>
        /// <param name="basePath"></param>
        /// <returns></returns>
        public ReturnResult WebUploadFile(string filePath, HttpPostedFile file, Int32 thunk, Int32 thunks, string basePath = "~/UploadFiles/Files/") {
            ReturnResult rr = new ReturnResult();
            string uploadType = _appConfig.AppConfigGet("UploadType");

            string fileExt = FileUtil.GetExtension(filePath);

            // 配置为七牛上传
            if (uploadType.Equals("Qiniu") && thunks == 1)
            {
                if (fileExt.Equals("gif", StringComparison.OrdinalIgnoreCase) ||
                    fileExt.Equals("jpeg", StringComparison.OrdinalIgnoreCase) ||
                    fileExt.Equals("bmp", StringComparison.OrdinalIgnoreCase) ||
                    fileExt.Equals("png", StringComparison.OrdinalIgnoreCase))
                {
                    rr = UploadImageToQiniu(filePath, file, basePath);
                }
                else {
                    rr = UploadFileToQiniu(filePath, file, basePath);
                }
            }
            else if (uploadType.Equals("Qiniu") && thunks > 1)
            {
                if (fileExt.Equals("gif", StringComparison.OrdinalIgnoreCase) ||
                    fileExt.Equals("jpeg", StringComparison.OrdinalIgnoreCase) ||
                    fileExt.Equals("bmp", StringComparison.OrdinalIgnoreCase) ||
                    fileExt.Equals("png", StringComparison.OrdinalIgnoreCase))
                {
                    // TODO
                    //rr = UploadChunkImageToQiniu(filePath, file, basePath, thunk, thunks);
                }
                else
                {
                    // TODO
                    //rr = UploadChunkFileToQiniu(filePath, file, basePath, thunk, thunks);
                }
            }
            // 配置为保存到本地
            else if (uploadType.Equals("Local") && thunks == 1)
            {
                if (fileExt.Equals("gif", StringComparison.OrdinalIgnoreCase) ||
                   fileExt.Equals("jpeg", StringComparison.OrdinalIgnoreCase) ||
                   fileExt.Equals("bmp", StringComparison.OrdinalIgnoreCase) ||
                   fileExt.Equals("png", StringComparison.OrdinalIgnoreCase))
                {
                    rr = UploadImageToServer(filePath, file, basePath);
                }
                else {
                    rr = UploadFileToServer(filePath, file, basePath);
                }
            }
            else if (uploadType.Equals("Local") && thunks > 1) {
                if (fileExt.Equals("gif", StringComparison.OrdinalIgnoreCase) ||
                   fileExt.Equals("jpeg", StringComparison.OrdinalIgnoreCase) ||
                   fileExt.Equals("bmp", StringComparison.OrdinalIgnoreCase) ||
                   fileExt.Equals("png", StringComparison.OrdinalIgnoreCase))
                {
                    rr = UploadChunkImageToServer(filePath, file, thunk, thunks, basePath);
                }
                else
                {
                    rr = UploadChunkFileToServer(filePath, file, thunk, thunks, basePath);
                }
            }
            // 未配置或者配置类型不支持
            else
            {
                rr.ErrorCode = 000029;
                rr.ErrorMessage = dicErrInfo["E000029"].ChineseName;
                rr.ErrorPath = "FileUploadHelper->WebUploadFile(string imgPath, HttpPostedFile file, string basePath = \"~/UploadFiles/Files/\")";
                rr.LogLevel = dicErrInfo["E000029"].LogLevel;
            }

            return rr;
        }

        /// <summary>
        /// 上传文件到本地服务器
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="file">文件内容</param>
        /// <param name="basePath">上传到的路径</param>
        /// <returns></returns>
        private ReturnResult UploadFileToServer(string filePath, HttpPostedFile file, string basePath = "~/UploadFiles/Files/")
        {
            ReturnResult rr = new ReturnResult();

            try
            {
                if (!Directory.Exists(basePath))
                    Directory.CreateDirectory(basePath);

                file.SaveAs(filePath);

                rr.ErrorCode = 000000;
                rr.ErrorMessage = dicErrInfo["E000000"].ChineseName;
                rr.ErrorPath = "FileUploadHelper->UploadFileToServer(string filePath, HttpPostedFile file, string basePath = \"~/UploadFiles/Files/\")";
                rr.LogLevel = (short)LogLevel.LOG_LEVEL_INFO;
            }
            catch (Exception ex) {
                rr.ErrorCode = 000030;
                rr.ErrorMessage = dicErrInfo["E000030"].ChineseName;
                rr.ErrorPath = "FileUploadHelper->UploadFileToServer";
                rr.LogLevel = dicErrInfo["E000030"].LogLevel;
                rr.Data1 = ex.Message;
            }

            return rr;
        }

        /// <summary>
        /// 分片段上传文件
        /// </summary>
        /// <param name="filePath">文件路径, 注: 文件名应该传入进来就应该分装找比如 part_5_1_md5_guid_文件名字</param>
        /// <param name="file">文件内容</param>
        /// <param name="thunk">第几块</param>
        /// <param name="thunks">共快数</param>
        /// <param name="basePath">上传文件路径，注: 如果是分片上传，则路径为 basePath+md5值</param>
        /// <returns></returns>
        private ReturnResult UploadChunkFileToServer(string filePath, HttpPostedFile file,  Int32 thunk, Int32 thunks, string basePath = "~/UploadFiles/Files/")
        {
            ReturnResult rr = new ReturnResult();

            try
            {
                if (!Directory.Exists(basePath))
                    Directory.CreateDirectory(basePath);

                file.SaveAs(filePath);

                // 文件数量已经达到了预期的
                if (DirectoryUtil.GetFileNames(basePath).Length == thunks && (thunk + 1) == thunks) {
                    string[] filenames = DirectoryUtil.GetFileNames(basePath);
                    FileUtil.MergeFiles(filenames, "d:\new.rar");
                }

                rr.ErrorCode = 000000;
                rr.ErrorMessage = dicErrInfo["E000000"].ChineseName;
                rr.ErrorPath = "FileUploadHelper->UploadImageToServer(string filePath, HttpPostedFile file, string basePath = \"~/UploadFiles/Files/\")";
                rr.LogLevel = (short)LogLevel.LOG_LEVEL_INFO;
            }
            catch (Exception ex)
            {
                rr.ErrorCode = 000030;
                rr.ErrorMessage = dicErrInfo["E000030"].ChineseName;
                rr.ErrorPath = "FileUploadHelper->UploadImageToServer";
                rr.LogLevel = dicErrInfo["E000030"].LogLevel;
                rr.Data1 = ex.Message;
            }

            return rr;

            return rr;
        }

        /// <summary>
        /// 上传图片到本地服务器
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="file">文件内容</param>
        /// <param name="basePath">上传到的路径</param>
        /// <returns></returns>
        private ReturnResult UploadImageToServer(string filePath, HttpPostedFile file, string basePath = "~/UploadFiles/Files/")
        {
            ReturnResult rr = new ReturnResult();

            try
            {
                if (!Directory.Exists(basePath))
                    Directory.CreateDirectory(basePath);

                file.SaveAs(filePath);

                rr.ErrorCode = 000000;
                rr.ErrorMessage = dicErrInfo["E000000"].ChineseName;
                rr.ErrorPath = "FileUploadHelper->UploadImageToServer(string filePath, HttpPostedFile file, string basePath = \"~/UploadFiles/Files/\")";
                rr.LogLevel = (short)LogLevel.LOG_LEVEL_INFO;
            }
            catch (Exception ex)
            {
                rr.ErrorCode = 000030;
                rr.ErrorMessage = dicErrInfo["E000030"].ChineseName;
                rr.ErrorPath = "FileUploadHelper->UploadImageToServer";
                rr.LogLevel = dicErrInfo["E000030"].LogLevel;
                rr.Data1 = ex.Message;
            }

            return rr;
        }

        private ReturnResult UploadChunkImageToServer(string filePath, HttpPostedFile file, Int32 thunk, Int32 thunks, string basePath = "~/UploadFiles/Files/")
        {
            ReturnResult rr = new ReturnResult();
            return rr;
        }

        private ReturnResult UploadFileToQiniu(string imgPath, HttpPostedFile file, string basePath = "~/UploadFiles/Files/")
        {
            // 初始化qiniu配置，主要是API Keys
            qiniu.Config.ACCESS_KEY = _appConfig.AppConfigGet("ACCESS_KEY");
            qiniu.Config.SECRET_KEY = _appConfig.AppConfigGet("SECRET_KEY");
            qiniu.Config.UP_HOST = _appConfig.AppConfigGet("UP_HOST");
            string bucketName = _appConfig.AppConfigGet("BUCKET_NAME");

            ReturnResult rr = null;

            if (!basePath.EndsWith("/"))
                basePath = basePath + "//";
            string qiniukey = basePath + FileUtil.GetFileName(imgPath);

            /* 断点续传   
             */
            QiniuFile qfile = new QiniuFile(bucketName, qiniukey);
            ManualResetEvent done = new ManualResetEvent(false);
            qfile.UploadCompleted += (sender, e) =>
            {
                rr = new ReturnResult() { ErrorCode = 0, ErrorMessage = string.Format("[UploaderFileByBaidu->UploadFileToQiniu->SaveFile_UploadCompleted] Key:{0} Hash:{1}", e.key, e.Hash), LogLevel = (short)LogLevel.LOG_LEVEL_INFO, Data1 = qiniukey };
                done.Set();
            };
            qfile.UploadFailed += (sender, e) =>
            {
                rr = new ReturnResult() { ErrorCode = 000012, ErrorPath = string.Format("[UploaderFileByBaidu->UploadFileToQiniu->UploadFailed] Error:{0}", e.Error.Error), ErrorMessage = dicErrInfo["E000012"].ChineseName, LogLevel = dicErrInfo["E000012"].LogLevel };
                done.Set();
            };

            // 进度条变更
            qfile.UploadProgressChanged += (sender, e) =>
            {
                int percentage = (int)(100 * e.BytesSent / e.TotalBytes);
                rr = new ReturnResult() { ErrorCode = 0, ErrorMessage = string.Format("[UploaderFileByBaidu->UploadFileToQiniu->SaveFile_UploadProgressChanged] Percentage:{0}", percentage), LogLevel = (short)LogLevel.LOG_LEVEL_INFO, Data1 = qiniukey };
            };

            byte[] bytes = new byte[(int)file.ContentLength];
            file.InputStream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            file.InputStream.Seek(0, SeekOrigin.Begin);
            //上传为异步操作
            //上传本地文件到七牛云存储
            qfile.UploadString(file.ContentLength, file.ContentType, Convert.ToBase64String(bytes));
            done.WaitOne();

            if (rr != null)
            {
                return rr;
            }
            else {
                rr = new ReturnResult();
                rr.ErrorCode = 000013;
                rr.ErrorMessage = dicErrInfo["E000013"].ChineseName;
                rr.ErrorPath = "FileUploadHelper->UploadChunkImageToQiniu";
                rr.LogLevel = dicErrInfo["E000013"].LogLevel;
                return rr;
            }
        }

        private Int32 UploadChunkFileToQiniu(Int32 FileChunk, string bucket)
        {
            return 0;
        }

        private ReturnResult UploadImageToQiniu(String imgPath, HttpPostedFile file, string basePath = "~/UploadFiles/Files/")
        {
            // 初始化qiniu配置，主要是API Keys
            qiniu.Config.ACCESS_KEY = _appConfig.AppConfigGet("ACCESS_KEY");
            qiniu.Config.SECRET_KEY = _appConfig.AppConfigGet("SECRET_KEY");
            qiniu.Config.UP_HOST = _appConfig.AppConfigGet("UP_HOST");
            string bucketName = _appConfig.AppConfigGet("BUCKET_NAME");

            ReturnResult rr = null;

            string fileExt = FileUtil.GetExtension(imgPath);

            // ,jpg,jpeg,bmp,png
            if (fileExt.Equals("gif", StringComparison.OrdinalIgnoreCase) ||
                fileExt.Equals("jpeg", StringComparison.OrdinalIgnoreCase) ||
                fileExt.Equals("bmp", StringComparison.OrdinalIgnoreCase) ||
                fileExt.Equals("png", StringComparison.OrdinalIgnoreCase))
            {
                string qiniukey = basePath + FileUtil.GetFileName(imgPath);

                /* 断点续传   
                 */
                QiniuFile qfile = new QiniuFile(bucketName, qiniukey);
                ManualResetEvent done = new ManualResetEvent(false);
                qfile.UploadCompleted += (sender, e) =>
                {
                    rr = new ReturnResult() { ErrorCode = 0, ErrorMessage = string.Format("[UploaderFileByBaidu->SaveFile_UploadCompleted] Key:{0} Hash:{1}", e.key, e.Hash), LogLevel = (short)LogLevel.LOG_LEVEL_INFO, Data1 = qiniukey };

                    done.Set();
                };
                qfile.UploadFailed += (sender, e) =>
                {
                    rr = new ReturnResult() { ErrorCode = 000012, ErrorPath = string.Format("[UploaderFileByBaidu->UploadFailed] Error:{0}", e.Error.Error), ErrorMessage = dicErrInfo["E000012"].ChineseName, LogLevel = dicErrInfo["E000012"].LogLevel };
                    done.Set();
                };

                // 进度条变更
                qfile.UploadProgressChanged += (sender, e) =>
                {
                    int percentage = (int)(100 * e.BytesSent / e.TotalBytes);
                    rr = new ReturnResult() { ErrorCode = 0, ErrorMessage = string.Format("[UploaderFileByBaidu->SaveFile_UploadProgressChanged] Percentage:{0}", percentage), LogLevel = (short)LogLevel.LOG_LEVEL_INFO, Data1 = qiniukey };
                };

                byte[] bytes = new byte[(int)file.ContentLength];
                file.InputStream.Read(bytes, 0, bytes.Length);
                // 设置当前流的位置为流的开始
                file.InputStream.Seek(0, SeekOrigin.Begin);
                //上传为异步操作
                //上传本地文件到七牛云存储
                qfile.UploadString(file.ContentLength, file.ContentType, Convert.ToBase64String(bytes));
                done.WaitOne();
            }
            else
            {
                rr = new ReturnResult();
                rr.ErrorCode = 000013;
                rr.ErrorMessage = dicErrInfo["E000013"].ChineseName;
                rr.ErrorPath = "FileUploadHelper->UploadChunkImageToQiniu";
                rr.LogLevel = dicErrInfo["E000013"].LogLevel;
                return rr;
            }

            return rr;
        }

        /// <summary>
        /// 如果是片段文件 则先保存到本地，等全部上传完毕后，进行合并，然后在上传到七牛云服务器端
        /// </summary>
        /// <param name="imgPath"></param>
        /// <param name="imgName"></param>
        /// <param name="bucket"></param>
        /// <returns></returns>
        private Int32 UploadChunkImageToQiniu(String imgPath, HttpPostedFile file, string basePath = "~/UploadFiles/Files/")
        {
            return 0;
        }
    }
}
