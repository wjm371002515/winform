using JCodes.Framework.BLL;
using JCodes.Framework.Common.Files;
using JCodes.Framework.Common.Format;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Entity;
using JCodes.Framework.jCodesenum;
using JCodes.Framework.WebUI.Controllers.Base;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Web;

namespace JCodes.Framework.WebUI.ashx
{
    /// <summary>
    /// UploaderFileByBaidu 的摘要说明
    /// </summary>
    public class UploaderFileByBaidu : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentEncoding = Encoding.UTF8;
            if (context.Request["REQUEST_METHOD"] == "OPTIONS")
            {
                context.Response.End();
            }

            SaveFile(context);
        }

        /// <summary>
        /// 文件保存操作
        /// </summary>
        /// <param name="basePath"></param>
        private void SaveFile(HttpContext context, string basePath = "~/UploadFiles/Photo/")
        {
            basePath = GetUploadPath();
            string Datedir = DateTime.Now.ToString("yy-MM-dd");
            string updir = basePath + "\\" + Datedir;
            string extname = string.Empty;
            string fullname = string.Empty;
            string filename = string.Empty;

            HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
            
            if (files.Count == 0)
            {
                var result = "{\"jsonrpc\" : \"2.0\", \"error\" :  \"保存失败\",\"id\" :  \"id\"}";
                System.Web.HttpContext.Current.Response.Write(result);
            }
            

            var suffix = files[0].ContentType.Split('/');
            var _suffix = suffix[1].Equals("jpeg", StringComparison.CurrentCultureIgnoreCase) ? "" : suffix[1];
            var _temp = System.Web.HttpContext.Current.Request["name"];

            if (!string.IsNullOrEmpty(_temp))
            {
                filename = _temp;
            }
            else
            {
                Random rand = new Random(24 * (int)DateTime.Now.Ticks);
                filename = rand.Next() + "." + _suffix;
            }

            fullname = string.Format("{0}\\{1}", updir, Guid.NewGuid().ToString() + "_" + filename);

            /*
             1. 不需要拆分文件参数内容如下
             * id=WU_FILE_1&
             * name=%u5742%u4e95%u6cc9%u6c34.jpg&
             * type=image%2fjpeg&
             * lastModifiedDate=Sun+Sep+24+2017+00%3a43%3a45+GMT%2b0800+(%u4e2d%u56fd%u6807%u51c6%u65f6%u95f4)&
             * size=164029&
             * md5value=5189eb55135c98fa9ed891d0f8ed1829&
             * fileName_=&
             * isChunked=false
             * 
             2. 需要拆分文件参数内容如下
             * id=WU_FILE_0&
             * name=01%u7248-2018%u6700%u65b0PS%u6269%u5c55%u9762%u677f%u5408%u96c6win%2bmac.rar&
             * type=application%2foctet-stream&
             * lastModifiedDate=Fri+Oct+09+2020+14%3a07%3a00+GMT%2b0800+(%u4e2d%u56fd%u6807%u51c6%u65f6%u95f4)&
             * size=166756964&
             * chunks=16&
             * chunk=5&
             * md5value=5bae634f6e53d6d0c66e2964296a8bc8&
             * fileName_=&
             * isChunked=true&
             */
            ReturnResult rr = null;
            if (context.Request.Params["isChunked"] == null || context.Request.Params["isChunked"].Equals("false"))
                rr = (new FileUploadHelper()).WebUploadFile(fullname, files[0], 0, 1, "test");
            else {
                Int32 chunk = ConvertHelper.ToInt32(context.Request.Params["chunk"], 0);
                Int32 chunks = ConvertHelper.ToInt32(context.Request.Params["chunks"], 1);
                string md5value = context.Request.Params["md5value"];
                if (string.IsNullOrEmpty(md5value)) md5value = "nomd5";
                // 重新改造fullname的名字
                string newchunksFileName = FileUtil.GetFileNameNoExtension(fullname);
                string newchunksFullName = fullname.Replace(newchunksFileName, string.Format("{3}/part_{0}_{1}_{2}", chunks, chunk, newchunksFileName,md5value));

                updir = updir + "/" + md5value + "/";

                rr = (new FileUploadHelper()).WebUploadFile(newchunksFullName, files[0], chunk, chunks, updir);
            }

            System.Web.HttpContext.Current.Response.Write(JsonConvert.SerializeObject(rr));

            /* 普通上传文件
            QiniuFile qfile = new QiniuFile(bucket, qiniukey);
            //				ResumbleUploadEx puttedCtx = new ResumbleUploadEx (localfile);
            ManualResetEvent done = new ManualResetEvent(false);
            qfile.UploadCompleted += (sender, e) =>
            {
                Console.WriteLine(e.key);
                Console.WriteLine(e.Hash);
                done.Set();
            };
            qfile.UploadFailed += (sender, e) =>
            {
                Console.WriteLine(e.Error.ToString());
                //					puttedCtx.Save();
                done.Set();
            };

            qfile.UploadProgressChanged += (sender, e) =>
            {
                int percentage = (int)(100 * e.BytesSent / e.TotalBytes);
                Console.Write(percentage);
            };
            qfile.UploadBlockCompleted += (sender, e) =>
            {
                Console.WriteLine(sender);
                //					puttedCtx.Add(e.Index,e.Ctx);
                //					puttedCtx.Save();
            };
            qfile.UploadBlockFailed += (sender, e) =>
            {
                //
                Console.WriteLine(sender);
            };

            byte[] bytes=new byte[(int)files[0].ContentLength];
            files[0].InputStream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            files[0].InputStream.Seek(0, SeekOrigin.Begin);

            //上传为异步操作
            //上传本地文件到七牛云存储
            //				qfile.Upload (puttedCtx.PuttedCtx);
            qfile.UploadString(files[0].ContentLength, files[0].ContentType, Convert.ToBase64String(bytes));
            done.WaitOne();
             * */

            
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// 获取上传目录
        /// </summary>
        /// <returns></returns>
        private string GetUploadPath()
        {

            string path = HttpContext.Current.Server.MapPath("~/");
            string dirname = GetDirName();
            string uploadDir = path + "\\" + dirname;
            CreateDir(uploadDir);
            return uploadDir;
        }

        private void CreateDir(string path)
        {
            if (!System.IO.Directory.Exists(path))
            {

                System.IO.Directory.CreateDirectory(path);
            }
        }

        private string GetDirName()
        {
            return System.Configuration.ConfigurationManager.AppSettings["uploaddir"];
        }
    }
}