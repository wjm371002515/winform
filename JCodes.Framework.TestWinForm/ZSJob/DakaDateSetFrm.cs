using JCodes.Framework.Common;
using JCodes.Framework.Common.Encrypt;
using JCodes.Framework.Common.Files;
using JCodes.Framework.jCodesenum.BaseEnum;
using LumiSoft.Net.Mail;
using LumiSoft.Net.MIME;
using LumiSoft.Net.POP3.Client;
using System;
using System.IO;
using System.Windows.Forms;
using ICSharpCode.SharpZipLib;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Checksums;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.Common.Format;
using JCodes.Framework.jCodesenum;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Collections.Specialized;
using JCodes.Framework.Common.Threading;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using System.Timers; 

namespace JCodes.Framework.Test
{
    public partial class DakaDateSetFrm : Form
    {
        Boolean isDo = false;

        System.Timers.Timer myTimer = null;

        AppConfig appconfig = new AppConfig();

        public DakaDateSetFrm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 日期选中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            
        }

        private void DakaDateSetFrm_Load(object sender, EventArgs e)
        {
            //定义定时器  每隔5秒打印日志
            myTimer = new System.Timers.Timer(5000);

            myTimer.Elapsed += new ElapsedEventHandler(myTimer_Elapsed);

            myTimer.Enabled = true;

            myTimer.AutoReset = true;
        }

        void myTimer_Elapsed(object source, ElapsedEventArgs e)
        {
            try
            {
                Int32 intervalConfig = 1000 * ConvertHelper.ToInt32(appconfig.AppConfigGet("IntervalEveryTime"), 5);
                Int32 intervalHourConfig = ConvertHelper.ToInt32(appconfig.AppConfigGet("IntervalHour"), 0);
                Int32 intervalMinuteConfig = ConvertHelper.ToInt32(appconfig.AppConfigGet("IntervalMinute"), 5);
                DateTime dt = DateTime.Now;
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_INFO, dt.ToString("yyyy-MM-dd HH:mm:ss") + ":AutoTask is Working! Now Interval Is " + myTimer.Interval + ", IntervalHour Is: " + intervalHourConfig + ", IntervalMinute Is:" + intervalMinuteConfig + "!", typeof(DakaDateSetFrm));

                // 修改定时时间
                if (myTimer != null && myTimer.Interval != intervalConfig)
                {
                    myTimer.Interval = intervalConfig;
                }

                YourTask(dt, intervalHourConfig, intervalMinuteConfig);
            }

            catch (Exception ee)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_ALERT, ee, typeof(DakaDateSetFrm));
            }
        }

        void YourTask(DateTime dt, Int32 hour, Int32 minute)
        {
            //在这里写你需要执行的任务

            if (dt.Hour == hour && dt.Minute == minute && !isDo)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_INFO, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":定时器执行的任务!", typeof(DakaDateSetFrm));
                isDo = true;
            }

            // 隔日 不允许配置在0:00 执行任务
            if (dt.Hour == 0 && dt.Minute == 0)
            {
                isDo = false;
            }
        }

        private void dateNavigator1_CustomDrawDayNumberCell(object sender, DevExpress.XtraEditors.Calendar.CustomDrawDayNumberCellEventArgs e)
        {
            if (e.Date == ConvertHelper.ToDateTime("2020-04-16", DateTime.MinValue))
            {
                e.Graphics.FillRectangle(Brushes.Red, e.Bounds);

                e.Graphics.DrawString(e.Date.Day.ToString(), e.Style.Font, Brushes.White, e.Bounds);

                e.Handled = true;

            }
        }

        private void dateNavigator1_DoubleClick(object sender, EventArgs e)
        {

        }

        private void dateNavigator1_Click(object sender, EventArgs e)
        {
            /*txtDate.Text = dateNavigator1.SelectionStart.ToString("yyyy-MM-dd");
            txtWeek.Text = dateNavigator1.SelectionStart.DayOfWeek.ToString();//获得 英文星期几*/
        }

        #region 属性
        /// <summary>
        /// 登陆后返回的Html
        /// </summary>
        public static string ResultHtml
        {
            get;
            set;
        }
        /// <summary>
        /// 下一次请求的Url
        /// </summary>
        public static string NextRequestUrl
        {
            get;
            set;
        }
        /// <summary>
        /// 若要从远程调用中获取COOKIE一定要为request设定一个CookieContainer用来装载返回的cookies
        /// </summary>
        public static CookieContainer CookieContainer
        {
            get;
            set;
        }
        /// <summary>
        /// Cookies 字符创
        /// </summary>
        public static string CookiesString
        {
            get;
            set;
        }
        #endregion

        /// <summary>
        /// 人力打卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnManuDaka_Click(object sender, EventArgs e)
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            try
            {
                string renLiUrl = "http://10.51.190.162/zshris/";

                if (!renLiUrl.EndsWith("/")) renLiUrl = renLiUrl + "/";

                request = (HttpWebRequest)WebRequest.Create(renLiUrl);//实例化web访问类  
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Method = "GET";
                request.Host = renLiUrl.ToLower().Replace("http://", "").Replace("/zshris/", "");
                //request.Headers.Add("Connection", "keep-alive");
                request.Headers.Add("Upgrade-Insecure-Requests", "1");
                request.UserAgent = "Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Mobile Safari/537.36";
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9";
                request.Headers.Add("Accept-Encoding", "gzip, deflate");
                request.Headers.Add("Accept-Language", "zh-CN,zh;q=0.9"); 
                request.AllowAutoRedirect = false;   // 不用需自动跳转
                //必须设置CookieContainer存储请求返回的Cookies
                if (CookieContainer != null)
                {
                    request.CookieContainer = CookieContainer;
                }
                else
                {
                    request.CookieContainer = new CookieContainer();
                    CookieContainer = request.CookieContainer;
                }
                request.KeepAlive = true;

                //接收响应  
                response = (HttpWebResponse)request.GetResponse();
                //保存返回cookie  
                response.Cookies = request.CookieContainer.GetCookies(request.RequestUri);
                CookieCollection cook = response.Cookies;
                string strcrook = request.CookieContainer.GetCookieHeader(request.RequestUri);
                CookiesString = strcrook;
                //取下一次GET跳转地址  
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                string content = sr.ReadToEnd();
                sr.Close();
                request.Abort();
                response.Close();
                //依据登陆成功后返回的Page信息，求出下次请求的url
                //每个网站登陆后加载的Url和顺序不尽相同，以下两步需根据实际情况做特殊处理，从而得到下次请求的URL

                string viewstate = RegInputValue(content, "__VIEWSTATE");
                string viewstategenerator = RegInputValue(content, "__VIEWSTATEGENERATOR");
                string eventvalidation = RegInputValue(content, "__EVENTVALIDATION");
                string userid = "wujianming";
                string pwd = "zs999999";
                string ctl02 = HttpUtility.UrlEncode("登录", Encoding.UTF8).ToUpper(); ;

                // 模拟登陆功能
                string renLiLoginUrl = renLiUrl.ToLower().Replace("/zshris/", "/zshris/loginx.aspx");
                request = (HttpWebRequest)WebRequest.Create(renLiLoginUrl);//实例化web访问类  
                request.Method = "POST";
                SetHeaderValue(request.Headers, "Host", renLiUrl.ToLower().Replace("http://", "").Replace("/zshris/", ""));
                SetHeaderValue(request.Headers, "Connection", "keep-alive");
                //request.Headers.Add("Content-Length", "791");
                request.Headers.Add("Cache-Control", "max-age=0");
                request.Headers.Add("Origin", renLiUrl.ToLower().Replace("/zshris/", ""));
                request.Headers.Add("Upgrade-Insecure-Requests", "1");
                request.ContentType = "application/x-www-form-urlencoded";
                request.UserAgent = "Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Mobile Safari/537.36";
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9";
                request.Referer = renLiUrl + "loginx.aspx";
                request.Headers.Add("Accept-Encoding", "gzip, deflate");
                request.Headers.Add("Accept-Language", "zh-CN,zh;q=0.9");
                request.AllowAutoRedirect = true;   // 不用需自动跳转
                request.ServicePoint.Expect100Continue = false;

                //必须设置CookieContainer存储请求返回的Cookies
                if (CookieContainer != null)
                {
                    request.CookieContainer = CookieContainer;
                }
                else
                {
                    request.CookieContainer = new CookieContainer();
                    CookieContainer = request.CookieContainer;
                }
                //提交请求  
                string postdata = "__VIEWSTATE=" + viewstate + "&__VIEWSTATEGENERATOR=" + viewstategenerator + "&__EVENTVALIDATION=" + eventvalidation + "&userid=" + userid + "&pwd=" + pwd + "&ctl02=" + ctl02;//模拟请求数据，数据样式可以用FireBug插件得到。
                byte[] postdatabytes = Encoding.UTF8.GetBytes(postdata.Replace("+", "%2B"));
                request.ContentLength = postdatabytes.Length;
                Stream stream;
                stream = request.GetRequestStream();
                //设置POST 数据
                stream.Write(postdatabytes, 0, postdatabytes.Length);
                stream.Close();

                //接收响应  
                response = (HttpWebResponse)request.GetResponse();
                //保存返回cookie  
                response.Cookies = request.CookieContainer.GetCookies(request.RequestUri);
                cook = response.Cookies;
                strcrook = request.CookieContainer.GetCookieHeader(request.RequestUri);
                CookiesString = strcrook;
                //取下一次GET跳转地址  
                sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                content = sr.ReadToEnd();
                sr.Close();
                request.Abort();
                response.Close();

                // 停止5秒在打卡
                ThreadHelper.Sleep(5000);

                #region 核心打开程序
              
                request = (HttpWebRequest)WebRequest.Create(renLiUrl + "AjaxJSONProcess.aspx");//实例化web访问类  
                request.Method = "POST";
                SetHeaderValue(request.Headers, "Host", renLiUrl.ToLower().Replace("http://", "").Replace("/zshris/", ""));
                SetHeaderValue(request.Headers, "Connection", "keep-alive");
                request.Accept = "*/*";
                request.Headers.Add("X-Requested-With", "XMLHttpRequest");
                request.UserAgent = "Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Mobile Safari/537.36";
                request.ContentType = "text/html";

                request.Headers.Add("Origin", renLiUrl.ToLower().Replace("/zshris/", ""));
                request.Referer = renLiUrl + "Portal/Home.aspx";
                request.Headers.Add("Accept-Encoding", "gzip, deflate");
                request.Headers.Add("Accept-Language", "zh-CN,zh;q=0.9");
                request.AllowAutoRedirect = true;   // 不用需自动跳转
                request.ServicePoint.Expect100Continue = false;

                //必须设置CookieContainer存储请求返回的Cookies
                if (CookieContainer != null)
                {
                    request.CookieContainer = CookieContainer;
                }
                else
                {
                    request.CookieContainer = new CookieContainer();
                    CookieContainer = request.CookieContainer;
                }
                //提交请求  
                // 20200416 上午抓包内容 {"Toolbar":"601177","RowKeys":"","FuncName":"fm.WindowProcess.RunScript","Index":0,"Step":0}{:ky->w{"Path":"1.1001.413","Parameters":{},"Data":{"A":{"Data":{},"Parameters":{},"FMKey":"A","PKey":"","RowKeys":""}},"OperaParam":{},"Width":1703,"Height":810,"Url":"http://10.51.190.162/zsHRIS/Window/Window.aspx?wind=600050","Init":true,"CurrentForm":"A","ParamHtml":"","Popup":true,"WinType":201}
                // 20200416 下午抓包内容 {"Toolbar":"601178","RowKeys":"","FuncName":"fm.WindowProcess.RunScript","Index":0,"Step":0}{:ky->w{"Path":"1.1001.413","Parameters":{},"Data":{"A":{"Data":{},"Parameters":{},"FMKey":"A","PKey":"","RowKeys":""}},"OperaParam":{},"Width":1703,"Height":810,"Url":"http://10.51.190.162/zsHRIS/Window/Window.aspx?wind=600050","Init":true,"CurrentForm":"A","ParamHtml":"","Popup":true,"WinType":201}
                string dakadata = "{\"Toolbar\":\"601178\",\"RowKeys\":\"\",\"FuncName\":\"fm.WindowProcess.RunScript\",\"Index\":0,\"Step\":0}{:ky->w{\"Path\":\"1.1001.413\",\"Parameters\":{},\"Data\":{\"A\":{\"Data\":{},\"Parameters\":{},\"FMKey\":\"A\",\"PKey\":\"\",\"RowKeys\":\"\",\"AllowChoice\":false}},\"OperaParam\":{},\"Width\":3076,\"Height\":595,\"Url\":\" " + renLiUrl + "/Window/Window.aspx?wind=600050\",\"Init\":true,\"CurrentForm\":\"A\",\"ParamHtml\":\"\",\"Popup\":true,\"WinType\":201}";//模拟请求数据，数据样式可以用FireBug插件得到。
                byte[] dakadatabytes = Encoding.UTF8.GetBytes(dakadata.Replace("+", "%2B"));
                request.ContentLength = dakadatabytes.Length;
                Stream dakastream;
                dakastream = request.GetRequestStream();
                //设置POST 数据
                dakastream.Write(dakadatabytes, 0, dakadatabytes.Length);
                dakastream.Close();

                //接收响应  
                response = (HttpWebResponse)request.GetResponse();
                //保存返回cookie  
                response.Cookies = request.CookieContainer.GetCookies(request.RequestUri);
                cook = response.Cookies;
                strcrook = request.CookieContainer.GetCookieHeader(request.RequestUri);
                CookiesString = strcrook;
               
                
                /*//取下一次GET跳转地址  
                sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                content = sr.ReadToEnd();*/
                content = "";
                if (response.Headers["Content-Encoding"].ToLower() == "gzip")//如果使用了GZip则先解压
                {
                    using (System.IO.Stream streamReceive = response.GetResponseStream())
                    {
                        using (var zipStream =
                            new System.IO.Compression.GZipStream(streamReceive, System.IO.Compression.CompressionMode.Decompress))
                        {
                            using (StreamReader sr2 = new System.IO.StreamReader(zipStream, Encoding.UTF8))
                            {
                                content = sr2.ReadToEnd();
                            }
                        }
                    }
                }

                sr.Close();
                request.Abort();
                response.Close();

                #endregion

                var jObject = JObject.Parse(content);

                MessageDxUtil.ShowTips(jObject["data"]["Msg"].ToString());
            }
            catch (WebException ex)
            {
                MessageBox.Show(string.Format("登陆时出错，详细信息：{0}", ex.Message));
            }

        }

        /// <summary>
        /// OA打卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnOAdaka_Click(object sender, EventArgs e)
        {
            #region OA密码加密
              // "DFEZ0dqe/+2Le7uy2mg/fA=="
            // DFEZ0dqe%2F%2B2Le7uy2mg%2FfA%3D%3D
            // "0c5119d1da9effed8b7bbbb2da683f7c"
            //string abc = EncryptByAES("zs45685222", secretKey.PadRight(32, '0'));

            /*byte[] keyArray = UTF8Encoding.UTF8.GetBytes(secretKey.PadRight(32, '0'));
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes("zs45685222");
            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            string abc = Convert.ToBase64String(resultArray, 0, resultArray.Length);*/
            string secretKey = "1q2w3e4r";
            string userName = "wujianming";
            string password = "zs45685222";
            string aesPwd = symmetry_Encode(password, secretKey);

            //string abc = symmetry_Decode(aesPwd, secretKey);
            #endregion

            #region 访问首页获取参数
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            try
            {
                string OAUrl = "http://10.52.50.238/cas/login?service=http%3A%2F%2F10.52.50.238%2Fzszqsso%2F";

                string p = @"(http|https)://(?<domain>[^(:|/]*)";
                Regex reg = new Regex(p, RegexOptions.IgnoreCase);
                Match m = reg.Match(OAUrl);
                string ip = m.Groups["domain"].Value;

                request = (HttpWebRequest)WebRequest.Create(OAUrl);//实例化web访问类  
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Method = "GET";
                SetHeaderValue(request.Headers, "Host", ip);
                request.Headers.Add("Upgrade-Insecure-Requests", "1");
                request.UserAgent = "Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Mobile Safari/537.36";
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9";
                request.Headers.Add("Accept-Encoding", "gzip, deflate");
                request.Headers.Add("Accept-Language", "zh-CN,zh;q=0.9");
                request.AllowAutoRedirect = false;   // 不用需自动跳转

                //必须设置CookieContainer存储请求返回的Cookies
                if (CookieContainer != null)
                {
                    request.CookieContainer = CookieContainer;
                }
                else
                {
                    request.CookieContainer = new CookieContainer();
                    CookieContainer = request.CookieContainer;
                }
                request.KeepAlive = true;

                //接收响应  
                response = (HttpWebResponse)request.GetResponse();
                //保存返回cookie  
                response.Cookies = request.CookieContainer.GetCookies(request.RequestUri);
                CookieCollection cook = response.Cookies;
                string strcrook = request.CookieContainer.GetCookieHeader(request.RequestUri);
                CookiesString = strcrook;
                //取下一次GET跳转地址  
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                string content = sr.ReadToEnd();
                sr.Close();
                request.Abort();
                response.Close();
                //依据登陆成功后返回的Page信息，求出下次请求的url
                //每个网站登陆后加载的Url和顺序不尽相同，以下两步需根据实际情况做特殊处理，从而得到下次请求的URL

                string execution = RegInputValue(content, "execution");
                string postdata = string.Format("username={0}&password={1}&password_input={2}&execution={3}&_eventId={4}&geolocation=", userName, aesPwd, password, execution, "submit");
                request = (HttpWebRequest)WebRequest.Create(OAUrl);//实例化web访问类  
                request.Method = "POST";
                SetHeaderValue(request.Headers, "Host", ip);
                //SetHeaderValue(request.Headers, "Connection", "keep-alive");
                //request.Headers.Add("Content-Length", "791");
                request.Headers.Add("Cache-Control", "max-age=0");
                request.Headers.Add("Origin", "http://"+ip);
                request.Headers.Add("Upgrade-Insecure-Requests", "1");
                request.ContentType = "application/x-www-form-urlencoded";
                request.UserAgent = "Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Mobile Safari/537.36";
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9";
                request.Referer = OAUrl;
                request.Headers.Add("Accept-Encoding", "gzip, deflate");
                request.Headers.Add("Accept-Language", "zh-CN,zh;q=0.9");
                request.AllowAutoRedirect = false;   // 不用需自动跳转 20200502
                request.ServicePoint.Expect100Continue = false;

                //必须设置CookieContainer存储请求返回的Cookies
                if (CookieContainer != null)
                {
                    request.CookieContainer = CookieContainer;
                }
                else
                {
                    request.CookieContainer = new CookieContainer();
                    CookieContainer = request.CookieContainer;
                }
                //提交请求  
               
                byte[] postdatabytes = Encoding.UTF8.GetBytes(postdata.Replace("+", "%2B"));
                request.ContentLength = postdatabytes.Length;
                Stream stream;
                stream = request.GetRequestStream();
                //设置POST 数据
                stream.Write(postdatabytes, 0, postdatabytes.Length);
                stream.Close();

                //接收响应  
                response = (HttpWebResponse)request.GetResponse();
                string locationStr = response.Headers["Location"];
                //保存返回cookie  
                response.Cookies = request.CookieContainer.GetCookies(request.RequestUri);
                cook = response.Cookies;
                strcrook = request.CookieContainer.GetCookieHeader(request.RequestUri);
                CookiesString = strcrook;
                //取下一次GET跳转地址  
                sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                content = sr.ReadToEnd();
                sr.Close();
                
                request.Abort();
                response.Close();

                #region 跳转到Location位置 20200502 
                if (!string.IsNullOrEmpty(locationStr)) {
                    request = (HttpWebRequest)WebRequest.Create(locationStr);//实例化web访问类  
                    request.Credentials = CredentialCache.DefaultCredentials;
                    request.Method = "GET";
                    SetHeaderValue(request.Headers, "Host", ip);
                    request.Referer = OAUrl;
                    request.Headers.Add("Upgrade-Insecure-Requests", "1");
                    request.UserAgent = "Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Mobile Safari/537.36";
                    request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9";
                    request.Headers.Add("Accept-Encoding", "gzip, deflate");
                    request.Headers.Add("Accept-Language", "zh-CN,zh;q=0.9");
                    request.AllowAutoRedirect = false;   // 不用需自动跳转

                    //必须设置CookieContainer存储请求返回的Cookies
                    if (CookieContainer != null)
                    {
                        request.CookieContainer = CookieContainer;
                    }
                    else
                    {
                        request.CookieContainer = new CookieContainer();
                        CookieContainer = request.CookieContainer;
                    }
                    request.KeepAlive = true;

                    //接收响应  
                    response = (HttpWebResponse)request.GetResponse();
                    //保存返回cookie  
                    response.Cookies = request.CookieContainer.GetCookies(request.RequestUri);
                    cook = response.Cookies;
                    strcrook = request.CookieContainer.GetCookieHeader(request.RequestUri);
                    CookiesString = strcrook;
                    //取下一次GET跳转地址  
                    sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                    content = sr.ReadToEnd();
                    sr.Close();
                    request.Abort();
                    response.Close();
                }
                #endregion

                // 停止5秒在打卡
                ThreadHelper.Sleep(5000);

                #region OA核心打卡
                // http://10.52.50.238/sys/qiantui HTTP/1.1
                //request = (HttpWebRequest)WebRequest.Create("http://" + ip + "/sys/qiantui?_jsonpcallback=jQuery32106857541732276391_" + (new Random()).Next(1000, 9999) + "&_=" + GetTimeStamp());//实例化web访问类  
                request = (HttpWebRequest)WebRequest.Create("http://" + ip + "/sys/qiandao");//实例化web访问类  
                request.Method = "GET";
                SetHeaderValue(request.Headers, "Host", ip);
                SetHeaderValue(request.Headers, "Connection", "keep-alive");
                request.Accept = "text/javascript, application/javascript, application/ecmascript, application/x-ecmascript, */*; q=0.01";
                request.Headers.Add("X-Requested-With", "XMLHttpRequest");
                request.UserAgent = "Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Mobile Safari/537.36";
                request.Referer = "http://" + ip + "/zszqsso/zszq_portal/page/index.html";
                request.Headers.Add("Accept-Encoding", "gzip, deflate");
                request.Headers.Add("Accept-Language", "zh-CN,zh;q=0.9");
                request.AllowAutoRedirect = true;   // 不用需自动跳转
                request.ServicePoint.Expect100Continue = false;

                //必须设置CookieContainer存储请求返回的Cookies
                if (CookieContainer != null)
                {
                    request.CookieContainer = CookieContainer;
                }
                else
                {
                    request.CookieContainer = new CookieContainer();
                    CookieContainer = request.CookieContainer;
                }
  
                //接收响应  
                response = (HttpWebResponse)request.GetResponse();
                //保存返回cookie  
                response.Cookies = request.CookieContainer.GetCookies(request.RequestUri);
                cook = response.Cookies;
                strcrook = request.CookieContainer.GetCookieHeader(request.RequestUri);
                CookiesString = strcrook;

                sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                content = sr.ReadToEnd();

                sr.Close();
                request.Abort();
                response.Close();
                #endregion

            }
            catch (WebException ex)
            {
                MessageBox.Show(string.Format("登陆时出错，详细信息：{0}", ex.Message));
            }

            #endregion
        }

        //默认密钥向量
        private static byte[] Keys = { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00};

        public string symmetry_Encode(string encryptString, string encryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
                dCSP.Mode = CipherMode.ECB;
                dCSP.Padding = PaddingMode.PKCS7;
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch
            {
                return encryptString;
            }
        }

        /// <summary>
        /// 对称加密法解密函数
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public string symmetry_Decode(string decryptString, string decryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey);
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                DCSP.Mode = CipherMode.ECB;
                DCSP.Padding = PaddingMode.PKCS7;
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return decryptString;
            }
        }

        private string RegInputValue(string text,string name)
        {
            string input = Regex.Match(text, "<input[^>]*(?:id|name)=\"" + name + "\"[^>]*>").Value;
            if (input != "")
            {
                return Regex.Match(input, "(?<=value=\")[^\"]*").Value;
            }
            else { return ""; }
        }

        //需要引用 using System.Collections.Specialized;
        public static void SetHeaderValue(WebHeaderCollection header, string name, string value)
        {
            var property = typeof(WebHeaderCollection).GetProperty("InnerCollection",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            if (property != null)
            {
                var collection = property.GetValue(header, null) as NameValueCollection;
                collection[name] = value;
            }
        }

        /// <summary> 
        /// 获取时间戳 
        /// </summary> 
        /// <returns></returns> 
        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            // 自己密码
            string password = "123456";
            // 固定的key
            string secretKey = "oiubtpwx";
            string aesPwd = symmetry_Encode(password, secretKey);
        }


        /// <summary>
        /// 打卡查询记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            CookieContainer = null;
            /*
             Host: www.cncico.group
            Connection: keep-alive
            Cache-Control: max-age=0
            Upgrade-Insecure-Requests: 1
            User-Agent: Mozilla/5.0 (iPhone; CPU iPhone OS 10_3_1 like Mac OS X) AppleWebKit/603.1.30 (KHTML, like Gecko) Version/10.0 Mobile/14E304 Safari/602.1
            Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,* /*;q=0.8,application/signed-exchange;v=b3;q=0.9
            Accept-Encoding: gzip, deflate
            Accept-Language: zh-CN,zh;q=0.9
             */
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            try
            {
                string p = @"(http|https)://(?<domain>[^(:|/]*)";
                Regex reg = new Regex(p, RegexOptions.IgnoreCase);
                Match m = reg.Match(txtUrl.Text);
                string ip = m.Groups["domain"].Value;

                string content = Login(ip);

                var jObject = JObject.Parse(content);
                string sysUserId = jObject["result"]["id"].ToString();
                string sysName = jObject["result"]["name"].ToString();
                string token = jObject["result"]["token"].ToString();
                string mobile = jObject["result"]["mobile"].ToString();
                string description = jObject["description"].ToString();

                /*
                 GET http://www.cncico.group/apis/attendance/checkToday/158606 HTTP/1.1
                Host: www.cncico.group
                Connection: keep-alive
                Accept: application/json, text/javascript, * /*; q=0.01
                X-Requested-With: XMLHttpRequest
                User-Agent: Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.116 Safari/537.36 QBCore/4.0.1301.400 QQBrowser/9.0.2524.400 Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2875.116 Safari/537.36 NetType/WIFI MicroMessenger/7.0.5 WindowsWechat
                Authorization: eyJhbGciOiJIUzUxMiJ9.eyJsb2dpbl91c2VyX2tleSI6IjAwNzRlOTQ3LWNlOTQtNDBiMS1hMWM3LTljNmIzZDRkMDUyZSJ9.Wuj6pekpm6_aaJaZLuyzwaT3PSfe7T2giqDKfzb3nE6BchY7e-xihy7sskXo0vBKtheu1dIGfpHoPnv2cp8UNA
                Referer: http://www.cncico.group/static/appv20200427/home.html
                Accept-Encoding: gzip, deflate
                Accept-Language: zh-CN,zh;q=0.8,en-US;q=0.6,en;q=0.5;q=0.4
                Cookie: acw_tc=707c9f7c15892042116305853e3d74e7d80b3437f09e11ea7fd68037b98752
                 */
                #region 如果登陆成功查询打卡记录
                if (string.Equals(description, "success"))
                {
                    request = (HttpWebRequest)WebRequest.Create(txtUrl.Text + "/apis/attendance/checkToday/"+sysUserId);//实例化web访问类  
                    request.Credentials = CredentialCache.DefaultCredentials;
                    request.Method = "GET";
                    SetHeaderValue(request.Headers, "Host", ip);
                    request.Accept = "application/json, text/javascript, */*; q=0.01";
                    request.Headers.Add("X-Requested-With", "XMLHttpRequest");
                    request.UserAgent = "Mozilla/5.0 (iPhone; CPU iPhone OS 10_3_1 like Mac OS X) AppleWebKit/603.1.30 (KHTML, like Gecko) Version/10.0 Mobile/14E304 Safari/602.1";
                    request.Headers.Add("Authorization", token);
                    request.Referer = txtUrl.Text+ "/static/appv20200427/home.html";    
                    request.Headers.Add("Accept-Encoding", "gzip, deflate");
                    request.Headers.Add("Accept-Language", "zh-CN,zh;q=0.8,en-US;q=0.6,en;q=0.5;q=0.4");
                    request.AllowAutoRedirect = false;   // 不用需自动跳转

                    //必须设置CookieContainer存储请求返回的Cookies
                    if (CookieContainer != null)
                    {
                        request.CookieContainer = CookieContainer;
                    }
                    else
                    {
                        request.CookieContainer = new CookieContainer();
                        CookieContainer = request.CookieContainer;
                    }
                    request.KeepAlive = true;

                    //接收响应  
                    response = (HttpWebResponse)request.GetResponse();
                    //保存返回cookie  
                    response.Cookies = request.CookieContainer.GetCookies(request.RequestUri);
                    CookieCollection cook = response.Cookies;
                    string strcrook = request.CookieContainer.GetCookieHeader(request.RequestUri);
                    CookiesString = strcrook;
                    //取下一次GET跳转地址  
                    StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                    content = sr.ReadToEnd();
                    sr.Close();
                    request.Abort();
                    response.Close();
                }
                #endregion

               /*{"success":true,"msg":"true","data":{"checkToday":true,"date":"2020-05-11"}}*/
                jObject = JObject.Parse(content);
                if (string.Equals(jObject["success"].ToString(), "true", StringComparison.CurrentCultureIgnoreCase))
                {
                    lblCurDayStr.Text = string.Format("姓名:{0} 登录名:{1}", sysName, mobile);
                    lblDakaStatus.Text = string.Format("打卡日期: {0}, 打卡状态: {1}", jObject["data"]["date"], jObject["data"]["checkToday"]);
                }
                else
                { 
                    lblCurDayStr.Text = "查询失败";
                    lblDakaStatus.Text = "";
                }
                    
            }
            catch (WebException ex)
            {
                MessageBox.Show(string.Format("查询失败，详细信息：{0}", ex.Message));
                lblCurDayStr.Text = "查询失败";
                lblDakaStatus.Text = "";
            }
        }

        /// <summary>
        /// 登陆信息
        /// </summary>
        /// <returns>ResponseString</returns>
        private string Login(string ip) {
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            string returnContent = string.Empty;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(txtUrl.Text + "/static/appv20200427/login.html");//实例化web访问类  
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Method = "GET";
                SetHeaderValue(request.Headers, "Host", ip);
                request.Headers.Add("Cache-Control", "max-age=0");
                request.Headers.Add("Upgrade-Insecure-Requests", "1");
                request.UserAgent = "Mozilla/5.0 (iPhone; CPU iPhone OS 10_3_1 like Mac OS X) AppleWebKit/603.1.30 (KHTML, like Gecko) Version/10.0 Mobile/14E304 Safari/602.1";
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9";
                request.Headers.Add("Accept-Encoding", "gzip, deflate");
                request.Headers.Add("Accept-Language", "zh-CN,zh;q=0.9");
                request.AllowAutoRedirect = false;   // 不用需自动跳转

                //必须设置CookieContainer存储请求返回的Cookies
                if (CookieContainer != null)
                {
                    request.CookieContainer = CookieContainer;
                }
                else
                {
                    request.CookieContainer = new CookieContainer();
                    CookieContainer = request.CookieContainer;
                }
                request.KeepAlive = true;

                //接收响应  
                response = (HttpWebResponse)request.GetResponse();
                //保存返回cookie  
                response.Cookies = request.CookieContainer.GetCookies(request.RequestUri);
                CookieCollection cook = response.Cookies;
                string strcrook = request.CookieContainer.GetCookieHeader(request.RequestUri);
                CookiesString = strcrook;
                //取下一次GET跳转地址  
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                string content = sr.ReadToEnd();
                sr.Close();
                request.Abort();
                response.Close();
                //依据登陆成功后返回的Page信息，求出下次请求的url
                //每个网站登陆后加载的Url和顺序不尽相同，以下两步需根据实际情况做特殊处理，从而得到下次请求的URL

                /*POST http://www.cncico.group/loginapi/login HTTP/1.1
                Host: www.cncico.group
                Connection: keep-alive
                Content-Length: 42
                Accept: application/json, text/javascript, * /*; q=0.01
                X-Requested-With: XMLHttpRequest
                User-Agent: Mozilla/5.0 (iPhone; CPU iPhone OS 10_3_1 like Mac OS X) AppleWebKit/603.1.30 (KHTML, like Gecko) Version/10.0 Mobile/14E304 Safari/602.1
                Content-Type: application/x-www-form-urlencoded; charset=UTF-8
                Origin: http://www.cncico.group
                Referer: http://www.cncico.group/static/appv20200427/login.html
                Accept-Encoding: gzip, deflate
                Accept-Language: zh-CN,zh;q=0.9
                Cookie: acw_tc=2f61f26c15882937536273151e3329377c83e0090625961c39e0b669adba78

                mobile=15558066316&password=psJbLc9IZ8Q%3D*/
                string userName = "15558066316";
                // 自己密码
                string password = "123456";
                // 固定的key
                string secretKey = "oiubtpwx";
                string aesPwd = symmetry_Encode(password, secretKey);

                string postdata = string.Format("mobile={0}&password={1}", userName, aesPwd);
                request = (HttpWebRequest)WebRequest.Create(txtUrl.Text + "/loginapi/login");//实例化web访问类  
                request.Method = "POST";
                SetHeaderValue(request.Headers, "Host", ip);
                //SetHeaderValue(request.Headers, "Connection", "keep-alive");
                //request.Headers.Add("Content-Length", "791");
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                request.Headers.Add("X-Requested-With", "XMLHttpRequest");
                request.UserAgent = "Mozilla/5.0 (iPhone; CPU iPhone OS 10_3_1 like Mac OS X) AppleWebKit/603.1.30 (KHTML, like Gecko) Version/10.0 Mobile/14E304 Safari/602.1";
                request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                request.Headers.Add("Origin", "http://" + ip);
                request.Referer = txtUrl.Text + "/static/appv20200427/login.html";
                request.Headers.Add("Accept-Encoding", "gzip, deflate");
                request.Headers.Add("Accept-Language", "zh-CN,zh;q=0.9");
                request.AllowAutoRedirect = false;   // 不用需自动跳转 20200502
                request.ServicePoint.Expect100Continue = false;

                //必须设置CookieContainer存储请求返回的Cookies
                if (CookieContainer != null)
                {
                    request.CookieContainer = CookieContainer;
                }
                else
                {
                    request.CookieContainer = new CookieContainer();
                    CookieContainer = request.CookieContainer;
                }
                //提交请求  

                byte[] postdatabytes = Encoding.UTF8.GetBytes(postdata.Replace("+", "%2B"));
                request.ContentLength = postdatabytes.Length;
                Stream stream;
                stream = request.GetRequestStream();
                //设置POST 数据
                stream.Write(postdatabytes, 0, postdatabytes.Length);
                stream.Close();

                //接收响应  
                response = (HttpWebResponse)request.GetResponse();

                //保存返回cookie  
                response.Cookies = request.CookieContainer.GetCookies(request.RequestUri);
                cook = response.Cookies;
                strcrook = request.CookieContainer.GetCookieHeader(request.RequestUri);
                CookiesString = strcrook;
                //取下一次GET跳转地址  
                sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                returnContent = sr.ReadToEnd();
                sr.Close();

                request.Abort();
                response.Close();

                // 保存token 移到外面处理
                /*var jObject = JObject.Parse(returnContent);
                string sysUserId = jObject["result"]["id"].ToString();
                string sysName = jObject["result"]["name"].ToString();
                string token = jObject["result"]["token"].ToString();
                string mobile = jObject["result"]["mobile"].ToString();
                string description = jObject["description"].ToString();
                */
                var jObject = JObject.Parse(returnContent);
                string token = jObject["result"]["token"].ToString();

                /*
                 POST http://www.cncico.group/apis/loginByToken HTTP/1.1
                Host: www.cncico.group
                Connection: keep-alive
                Content-Length: 190
                Accept: application/json, text/javascript, * /*; q=0.01
                Origin: http://www.cncico.group
                X-Requested-With: XMLHttpRequest
                User-Agent: Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.116 Safari/537.36 QBCore/4.0.1301.400 QQBrowser/9.0.2524.400 Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2875.116 Safari/537.36 NetType/WIFI MicroMessenger/7.0.5 WindowsWechat
                Authorization: eyJhbGciOiJIUzUxMiJ9.eyJsb2dpbl91c2VyX2tleSI6IjAwNzRlOTQ3LWNlOTQtNDBiMS1hMWM3LTljNmIzZDRkMDUyZSJ9.Wuj6pekpm6_aaJaZLuyzwaT3PSfe7T2giqDKfzb3nE6BchY7e-xihy7sskXo0vBKtheu1dIGfpHoPnv2cp8UNA
                Content-Type: application/x-www-form-urlencoded; charset=UTF-8
                Referer: http://www.cncico.group/static/appv20200427/login.html
                Accept-Encoding: gzip, deflate
                Accept-Language: zh-CN,zh;q=0.8,en-US;q=0.6,en;q=0.5;q=0.4
                Cookie: acw_tc=707c9f7c15892042116305853e3d74e7d80b3437f09e11ea7fd68037b98752

                token=eyJhbGciOiJIUzUxMiJ9.eyJsb2dpbl91c2VyX2tleSI6IjAwNzRlOTQ3LWNlOTQtNDBiMS1hMWM3LTljNmIzZDRkMDUyZSJ9.Wuj6pekpm6_aaJaZLuyzwaT3PSfe7T2giqDKfzb3nE6BchY7e-xihy7sskXo0vBKtheu1dIGfpHoPnv2cp8UNA
                 */
                request = (HttpWebRequest)WebRequest.Create(txtUrl.Text + "/apis/loginByToken");//实例化web访问类  
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Method = "POST";
                SetHeaderValue(request.Headers, "Host", ip);
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                request.Headers.Add("Origin", "http://" + ip);
                request.Headers.Add("X-Requested-With", "XMLHttpRequest");
                request.UserAgent = "Mozilla/5.0 (iPhone; CPU iPhone OS 10_3_1 like Mac OS X) AppleWebKit/603.1.30 (KHTML, like Gecko) Version/10.0 Mobile/14E304 Safari/602.1";
                request.Headers.Add("Authorization", token);
                request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                request.Referer = txtUrl.Text + "/static/appv20200427/login.html";
                request.Headers.Add("Accept-Encoding", "gzip, deflate");
                request.Headers.Add("Accept-Language", "zh-CN,zh;q=0.8,en-US;q=0.6,en;q=0.5;q=0.4");
                request.AllowAutoRedirect = false;   // 不用需自动跳转

                //必须设置CookieContainer存储请求返回的Cookies
                if (CookieContainer != null)
                {
                    request.CookieContainer = CookieContainer;
                }
                else
                {
                    request.CookieContainer = new CookieContainer();
                    CookieContainer = request.CookieContainer;
                }
                request.KeepAlive = true;

                postdata = string.Format("token={0}", token);
                postdatabytes = Encoding.UTF8.GetBytes(postdata.Replace("+", "%2B"));
                request.ContentLength = postdatabytes.Length;
                stream = request.GetRequestStream();
                //设置POST 数据
                stream.Write(postdatabytes, 0, postdatabytes.Length);
                stream.Close();

                //接收响应  
                response = (HttpWebResponse)request.GetResponse();
                //保存返回cookie  
                response.Cookies = request.CookieContainer.GetCookies(request.RequestUri);
                cook = response.Cookies;
                strcrook = request.CookieContainer.GetCookieHeader(request.RequestUri);
                CookiesString = strcrook;
                //取下一次GET跳转地址  
                sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                content = sr.ReadToEnd();
                sr.Close();
                request.Abort();
                response.Close();
            }
            catch (Exception ex) {
                MessageBox.Show(string.Format("登陆失败，详细信息：{0}", ex.Message));
                lblCurDayStr.Text = "登陆失败";
                lblDakaStatus.Text = "";
            }

            return returnContent;
        }

        /// <summary>
        /// 打卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton2_Click_1(object sender, EventArgs e)
        {
            CookieContainer = null;
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            try
            {
                string p = @"(http|https)://(?<domain>[^(:|/]*)";
                Regex reg = new Regex(p, RegexOptions.IgnoreCase);
                Match m = reg.Match(txtUrl.Text);
                string ip = m.Groups["domain"].Value;

                string content = Login(ip);

                var jObject = JObject.Parse(content);
                string sysUserId = jObject["result"]["id"].ToString();
                string sysName = jObject["result"]["name"].ToString();
                string token = jObject["result"]["token"].ToString();
                string mobile = jObject["result"]["mobile"].ToString();
                string description = jObject["description"].ToString();

                /*
                    POST http://www.cncico.group/apis/attendance/save HTTP/1.1
                    Host: www.cncico.group
                    Connection: keep-alive
                    Content-Length: 530
                    Accept: application/json, text/javascript, * /*; q=0.01
                    Origin: http://www.cncico.group
                    X-Requested-With: XMLHttpRequest
                    User-Agent: Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.116 Safari/537.36 QBCore/4.0.1301.400 QQBrowser/9.0.2524.400 Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2875.116 Safari/537.36 NetType/WIFI MicroMessenger/7.0.5 WindowsWechat
                    Authorization: eyJhbGciOiJIUzUxMiJ9.eyJsb2dpbl91c2VyX2tleSI6ImRkYTZhNmUwLTVjNDYtNDAyNS1iMGQxLWU5MTBiNzhhMzk4YyJ9.zCgbMHNktSKKrteGiCTtx4qgH-ZMqbZjmIXR4WVQLsEZjj85yEkLV7QqrVb7ucqs8xIo3zE9XMBZup4MhWA8hg
                    Content-Type: application/x-www-form-urlencoded; charset=UTF-8
                    Referer: http://www.cncico.group/static/appv20200427/reportPerson.html
                    Accept-Encoding: gzip, deflate
                    Accept-Language: zh-CN,zh;q=0.8,en-US;q=0.6,en;q=0.5;q=0.4
                    Cookie: acw_tc=2f61f27515892443207613140e34752f479a996d35c8a145d056388a712f24

               date=2020-05-12&attendance=1&health=1&healthdes=&outPlayStatus=1&outPlayStatusDes=&outConcatMan=1&outConcatManDes=&togetherHealth=1&togetherHealthDes=&isFace=2&codeColor=1&codeDes=&hbConcat=2&otherConcat=2&otherConcatDes=&onDutyTime=1&cityId=330100&cityName=%E6%9D%AD%E5%B7%9E%E5%B8%82&proviceId=330000&proviceName=%E6%B5%99%E6%B1%9F%E7%9C%81&address=%E8%9E%8D%E5%88%9B%E4%B8%9C%E5%8D%97%E6%B5%B7&company=05C0501&companyId=%E6%B5%99%E5%95%86%E8%AF%81%E5%88%B8%E6%9C%AC%E9%83%A8&staffId=158606&staffName=%E5%90%B4%E5%BB%BA%E6%98%8E
                 */
                #region 如果登陆成功查询打卡记录
                if (string.Equals(description, "success"))
                {
                    request = (HttpWebRequest)WebRequest.Create(txtUrl.Text + "/apis/attendance/save");//实例化web访问类  
                    request.Credentials = CredentialCache.DefaultCredentials;
                    request.Method = "POST";
                    SetHeaderValue(request.Headers, "Host", ip);
                    request.Accept = "application/json, text/javascript, */*; q=0.01";
                    request.Headers.Add("Origin", "http://" + ip);
                    request.Headers.Add("X-Requested-With", "XMLHttpRequest");
                    request.UserAgent = "Mozilla/5.0 (iPhone; CPU iPhone OS 10_3_1 like Mac OS X) AppleWebKit/603.1.30 (KHTML, like Gecko) Version/10.0 Mobile/14E304 Safari/602.1";
                    request.Headers.Add("Authorization", token);
                    request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                    request.Referer = txtUrl.Text + "/static/appv20200427/reportPerson.html";
                    request.Headers.Add("Accept-Encoding", "gzip, deflate");
                    request.Headers.Add("Accept-Language", "zh-CN,zh;q=0.8,en-US;q=0.6,en;q=0.5;q=0.4");
                    request.AllowAutoRedirect = false;   // 不用需自动跳转

                    //必须设置CookieContainer存储请求返回的Cookies
                    if (CookieContainer != null)
                    {
                        request.CookieContainer = CookieContainer;
                    }
                    else
                    {
                        request.CookieContainer = new CookieContainer();
                        CookieContainer = request.CookieContainer;
                    }
                    request.KeepAlive = true;

                    /*
                      打卡日期 date
                     今日在岗情况 在岗（已到公司/项目部上班）attendance=1, ,不在岗attendance=2
                     在岗时间 onDutyTime
                     目前健康状况 health
                     就诊情况说明 healthdes				
                    目前所在城市 cityName
                                 cityId          readonly 
                                 provinceId      readonly 注递交为:proviceId
                                 provinceName    readonly 注递交为:proviceName
                     详细地址	 address
                    前14天内是否在湖北停留或路过，或接触过来自湖北的人  ispass
                    前14天内是否接触过温州、台州温岭、台州黄岩当地人，或是否有过温州、台州温岭、台州黄岩居住史、旅行史 isContact
                     前14天内是否在湖北等重点疫区停留过或路过，或接触过来自湖北等重点疫区的人 hbConcat
                     前14天内本人或共同生活人员是否从境外回国，或者接触过从境外回国人员（特别是日本、韩国、意大利、伊朗等国家） otherConcat
                     情况说明 otherConcatDes
                     前14天内是否有接触过疑似或确诊的新型冠状病毒感染的肺炎患者或密切接触者 isFace
                     自昨日打卡之后是否有出入公共场所的情况 outPlayStatus
                     具体地点 outPlayStatusDes
                     自昨日打卡之后是否有接触外单位工作人员 outConcatMan
                     人员详情 outConcatManDes
                     共同生活人员目前健康状况 togetherHealth
                     情况说明(请描述患者姓名、与申报人关系及诊治情况) togetherHealthDes
                     健康码颜色是 codeColor
                     情况说明 codeDes 
                     公司代码 company
                     公司名字 companyId
                     员工编号 staffId
                     员工姓名 staffName
                     */
                    string date = DateTime.Now.ToString("yyyy-MM-dd");
                    string attendance = "1";
                    string health = "1";
                    string healthdes = string.Empty;
                    string outPlayStatus = "1";
                    string outPlayStatusDes = "";
                    string outConcatMan = "1";
                    string outConcatManDes = string.Empty;
                    string togetherHealth = "1";
                    string togetherHealthDes = string.Empty;
                    string isFace = "2";
                    string codeColor = "1";
                    string codeDes = string.Empty;
                    string hbConcat = "2";
                    string otherConcat = "2";
                    string otherConcatDes = string.Empty;
                    string onDutyTime = "1";
                    string cityId = "330100";
                    string cityName = HttpUtility.UrlEncode("杭州市").ToUpper();
                    string proviceId = "330000";
                    string proviceName = HttpUtility.UrlEncode("浙江省").ToUpper();
                    string address = HttpUtility.UrlEncode("融创东南海").ToUpper();
                    string company = "05C0501";
                    string companyId = HttpUtility.UrlEncode("浙商证券本部").ToUpper();
                    string staffId = "158606";
                    string staffName = HttpUtility.UrlEncode("吴建明").ToUpper();
 
                    string postdata = string.Format("date={0}&attendance={1}&health={2}&healthdes={3}&outPlayStatus={4}&outPlayStatusDes={5}&outConcatMan={6}&outConcatManDes={7}&togetherHealth={8}&togetherHealthDes={9}&isFace={10}&codeColor={11}&codeDes={12}&hbConcat={13}&otherConcat={14}&otherConcatDes={15}&onDutyTime={16}&cityId={17}&cityName={18}&proviceId={19}&proviceName={20}&address={21}&company={22}&companyId={23}&staffId={24}&staffName={25}",
                        date, attendance, health, healthdes, outPlayStatus,
                        outPlayStatusDes, outConcatMan, outConcatManDes, togetherHealth, togetherHealthDes,
                        isFace, codeColor, codeDes, hbConcat, otherConcat,
                        otherConcatDes, onDutyTime, cityId, cityName, proviceId,
                        proviceName, address, company, companyId, staffId,
                        staffName);
                    byte[] postdatabytes = Encoding.UTF8.GetBytes(postdata.Replace("+", "%2B"));
                    request.ContentLength = postdatabytes.Length;
                    Stream stream = request.GetRequestStream();
                    //设置POST 数据
                    stream.Write(postdatabytes, 0, postdatabytes.Length);
                    stream.Close();

                    //接收响应  
                    response = (HttpWebResponse)request.GetResponse();
                    //保存返回cookie  
                    response.Cookies = request.CookieContainer.GetCookies(request.RequestUri);
                    CookieCollection cook = response.Cookies;
                    string strcrook = request.CookieContainer.GetCookieHeader(request.RequestUri);
                    CookiesString = strcrook;
                    //取下一次GET跳转地址  
                    StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                    content = sr.ReadToEnd();
                    sr.Close();
                    request.Abort();
                    response.Close();
                }
                #endregion

                /* {"success":true,"msg":"true"} */
                jObject = JObject.Parse(content);
                if (string.Equals(jObject["success"].ToString(), "true", StringComparison.CurrentCultureIgnoreCase) && string.Equals(jObject["msg"].ToString(), "true", StringComparison.CurrentCultureIgnoreCase))
                {
                    lblDaKa.Text = "打卡成功";
                }
                else
                {
                    lblDaKa.Text = "打卡失败";
                }

            }
            catch (WebException ex)
            {
                MessageBox.Show(string.Format("打卡失败，详细信息：{0}", ex.Message));
                lblCurDayStr.Text = "打卡失败";
                lblDakaStatus.Text = "";
            }
        }
    }
}
