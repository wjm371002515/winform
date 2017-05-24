using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.IO;
using System.Threading;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Net;
using JCodes.Framework.jCodesenum.BaseEnum;

namespace JCodes.Framework.Common.Web
{
    /// <summary>
    /// Web方式文件上传下载操作类
    /// </summary>
    public class FileManage
    {
        /// <summary>
        /// 获取物理地址
        /// </summary>
        private static string MapPathFile(string FileName)
        {
            return HttpContext.Current.Server.MapPath(FileName);
        }

        /// <summary>
        /// 分块下载文件
        /// </summary>
        /// <param name="FileName">文件虚拟路径</param>
        public static void DownLoad(string FileName)
        {
            string filePath = MapPathFile(FileName);
            long chunkSize = 204800;             //指定块大小 
            byte[] buffer = new byte[chunkSize]; //建立一个200K的缓冲区 
            long dataToRead = 0;                 //已读的字节数   
            FileStream stream = null;
            try
            {
                //打开文件   
                stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                dataToRead = stream.Length;

                //添加Http头   
                HttpContext.Current.Response.ContentType = "application/octet-stream";
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachement;filename=" + HttpUtility.UrlEncode(Path.GetFileName(filePath)));
                HttpContext.Current.Response.AddHeader("Content-Length", dataToRead.ToString());

                while (dataToRead > 0)
                {
                    if (HttpContext.Current.Response.IsClientConnected)
                    {
                        int length = stream.Read(buffer, 0, Convert.ToInt32(chunkSize));
                        HttpContext.Current.Response.OutputStream.Write(buffer, 0, length);
                        HttpContext.Current.Response.Flush();
                        HttpContext.Current.Response.Clear();
                        dataToRead -= length;
                    }
                    else
                    {
                        dataToRead = -1; //防止client失去连接 
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FileManage));
            }
            finally
            {
                if (stream != null) stream.Close();
                HttpContext.Current.Response.Close();
            }
        }

        /// <summary>
        ///  输出硬盘文件，提供下载 支持大文件、续传、速度限制、资源占用小
        ///  <code>
        /// string FullPath=Server.MapPath("count.txt");
        /// ResponseFile(this.Request, this.Response, "count.txt", FullPath, 100); 
        /// </code>
        /// </summary>
        /// <param name="request">Page.Request对象</param>
        /// <param name="response">Page.Response对象</param>
        /// <param name="fileName">下载文件名</param>
        /// <param name="fullPath">带文件名下载路径</param>
        /// <param name="speed">每秒允许下载的字节数</param>
        /// <returns>返回是否成功</returns>
        public static bool ResponseFile(HttpRequest request, HttpResponse response, string fileName, string fullPath, long speed)
        {
            try
            {
                FileStream myFile = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(myFile);
                try
                {
                    response.AddHeader("Accept-Ranges", "bytes");
                    response.Buffer = false;

                    long fileLength = myFile.Length;
                    long startBytes = 0;
                    int pack = 10240;  //10K bytes
                    int sleep = (int)Math.Floor((double)(1000 * pack / speed)) + 1;

                    if (request.Headers["Range"] != null)
                    {
                        response.StatusCode = 206;
                        string[] range = request.Headers["Range"].Split(new char[] { '=', '-' });
                        startBytes = Convert.ToInt64(range[1]);
                    }
                    response.AddHeader("Content-Length", (fileLength - startBytes).ToString());
                    if (startBytes != 0)
                    {
                        response.AddHeader("Content-Range", string.Format(" bytes {0}-{1}/{2}", startBytes, fileLength - 1, fileLength));
                    }

                    response.AddHeader("Connection", "Keep-Alive");
                    response.ContentType = "application/octet-stream";
                    response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));

                    br.BaseStream.Seek(startBytes, SeekOrigin.Begin);
                    int maxCount = (int)Math.Floor((double)((fileLength - startBytes) / pack)) + 1;

                    for (int i = 0; i < maxCount; i++)
                    {
                        if (response.IsClientConnected)
                        {
                            response.BinaryWrite(br.ReadBytes(pack));
                            Thread.Sleep(sleep);
                        }
                        else
                        {
                            i = maxCount;
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FileManage));
                    return false;
                }
                finally
                {
                    br.Close();
                    myFile.Close();
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FileManage));
                return false;
            }
            return true;
        }

        /// <summary>
        /// 上传图片文件，限制为gif或者jpg图片，限制大小为10M
        /// </summary>
        /// <param name="posPhotoUpload">文件上传控件</param>
        /// <param name="saveFileName">保存的文件名</param>
        /// <param name="imagePath">保存的文件路径</param>
        public static string UploadImage(FileUpload posPhotoUpload, string saveFileName, string imagePath)
        {
            string state = "";
            if (posPhotoUpload.HasFile)
            {
                if (posPhotoUpload.PostedFile.ContentLength / 1024 < 10240)
                {
                    string MimeType = posPhotoUpload.PostedFile.ContentType;
                    if (String.Equals(MimeType, "image/gif") || String.Equals(MimeType, "image/pjpeg"))
                    {
                        string extFileString = System.IO.Path.GetExtension(posPhotoUpload.PostedFile.FileName);
                        posPhotoUpload.PostedFile.SaveAs(HttpContext.Current.Server.MapPath(imagePath));
                    }
                    else
                    {
                        state = "上传文件类型不正确";
                    }
                }
                else
                {
                    state = "上传文件不能大于10M";
                }
            }
            else
            {
                state = "没有上传文件";
            }
            return state;
        }

        /// <summary>
        /// 从HTML内容中寻找图片文件，并下载图片到本地
        /// </summary>
        /// <param name="strHTML">HTML内容</param>
        /// <param name="path">文件保存路径</param>
        public static string SaveUrlPics(string strHTML, string path)
        {
            string nowym = DateTime.Now.ToString("yyyy-MM");  //当前年月
            string nowdd = DateTime.Now.ToString("dd");       //当天号数
            path = path + nowym + "/" + nowdd;
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            string[] imgurlAry = GetImgTag(strHTML);
            try
            {
                for (int i = 0; i < imgurlAry.Length; i++)
                {
                    string preStr = System.DateTime.Now.ToString() + "_";
                    preStr = preStr.Replace("-", "");
                    preStr = preStr.Replace(":", "");
                    preStr = preStr.Replace(" ", "");
                    WebClient wc = new WebClient();
                    wc.DownloadFile(imgurlAry[i], path + "/" + preStr + imgurlAry[i].Substring(imgurlAry[i].LastIndexOf("/") + 1));
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FileManage));
                return ex.Message;
            }
            return strHTML;
        }

        #region 私有方法
        /// <summary>
        /// 获取图片标志
        /// </summary>
        private static string[] GetImgTag(string htmlStr)
        {
            Regex regObj = new Regex("<img.+?>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            string[] strAry = new string[regObj.Matches(htmlStr).Count];
            int i = 0;
            foreach (Match matchItem in regObj.Matches(htmlStr))
            {
                strAry[i] = GetImgUrl(matchItem.Value);
                i++;
            }
            return strAry;
        }

        /// <summary>
        /// 获取图片URL地址
        /// </summary>
        private static string GetImgUrl(string imgTagStr)
        {
            string str = "";
            Regex regObj = new Regex("http://.+.(?:jpg|gif|bmp|png)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            foreach (Match matchItem in regObj.Matches(imgTagStr))
            {
                str = matchItem.Value;
            }
            return str;
        }
        #endregion
    }
}
