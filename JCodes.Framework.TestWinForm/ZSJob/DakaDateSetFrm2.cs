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
    public partial class DakaDateSetFrm2 : Form
    {
        Boolean isDo = false;

        System.Timers.Timer myTimer = null;

        AppConfig appconfig = new AppConfig();

        public DakaDateSetFrm2()
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
    }
}
