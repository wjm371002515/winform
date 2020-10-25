using System;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Reflection;
using System.Net.Mail;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.jCodesenum;

namespace JCodes.Framework.Common.Network
{
    /// <summary>
    /// 发送邮件的辅助类，可以发送附件、嵌入图片、HTML等内容邮件。使用底层SMTP协议指令进行发送。
    /// </summary>
    public class EmailHelper
    {
        #region public属性字段

        /// <summary>
        /// 设定语言代码，默认设定为GB2312，如不需要可设置为""
        /// </summary>
        public string Charset = "GB2312";

        /// <summary>
        /// 邮箱服务器
        /// </summary>
        public string MailServer
        {
            get { return mailserver; }
            set { mailserver = value; }
        }

        /// <summary>
        /// 邮件服务器端口号,默认端口为25
        /// </summary>	
        public int MailServerPort
        {
            set
            {
                mailserverport = value;
            }
            get
            {
                return mailserverport;
            }
        }

        /// <summary>
        /// SMTP认证时使用的用户名
        /// </summary>
        public string MailServerUsername
        {
            set
            {
                if (value.Trim() != "")
                {
                    username = value.Trim();
                    useSmtpAuth = true;
                }
                else
                {
                    username = "";
                    useSmtpAuth = false;
                }
            }
            get
            {
                return username;
            }
        }

        /// <summary>
        /// SMTP认证时使用的密码
        /// </summary>
        public string MailServerPassword
        {
            set
            {
                password = value;
            }
            get
            {
                return password;
            }
        }

        /// <summary>
        /// 发件人地址
        /// </summary>
        public string From
        {
            get { return from; }
            set
            {
                from = value;
                //如果未设置fromName，则fromName使用发件人邮箱
                if (string.IsNullOrEmpty(fromName)) 
                { 
                    fromName = from; 
                }
            }
        }

        /// <summary>
        /// 发件人姓名
        /// </summary>
        public string FromName
        {
            get { return fromName; }
            set { fromName = value; }
        }

        /// <summary>
        /// 回复邮件地址
        /// </summary>
        public string ReplyTo = "";

        /// <summary>
        /// 邮件主题
        /// </summary>		
        public string Subject = "";

        /// <summary>
        /// 是否Html邮件
        /// </summary>		
        public bool IsHtml = false;

        /// <summary>
        /// 收件人是否发送收条
        /// </summary>
        public bool ReturnReceipt = false;

        /// <summary>
        /// 邮件正文
        /// </summary>		
        public string Body = "";

        /// <summary>
        /// 邮件发送优先级，可设置为"High","Normal","Low"或"1","3","5"
        /// </summary>
        public string Priority
        {
            set
            {
                switch (value.ToLower())
                {
                    case "high":
                        priority = "High";
                        break;

                    case "1":
                        priority = "High";
                        break;

                    case "normal":
                        priority = "Normal";
                        break;

                    case "3":
                        priority = "Normal";
                        break;

                    case "low":
                        priority = "Low";
                        break;

                    case "5":
                        priority = "Low";
                        break;

                    default:
                        priority = "Normal";
                        break;
                }
            }
        }

        /// <summary>
        /// 错误消息反馈
        /// </summary>		
        public string ErrorMessage
        {
            get
            {
                return errmsg;
            }
        }

        /// <summary>
        /// 收件人姓名
        /// </summary>	
        public string RecipientName = "";

        #endregion

        #region private属性字段

        /// <summary>
        /// 邮件服务器域名
        /// </summary>	
        private string mailserver;

        /// <summary>
        /// 邮件服务器端口号
        /// </summary>	
        private int mailserverport = 25;
        /// <summary>
        /// 发件人地址
        /// </summary>
        private string from = "";
        /// <summary>
        /// 发件人姓名
        /// </summary>
        private string fromName = "";

        /// <summary>
        /// 是否需要SMTP验证
        /// </summary>		
        private bool useSmtpAuth = false;

        /// <summary>
        /// SMTP认证时使用的用户名
        /// </summary>
        private string username = "";

        /// <summary>
        /// SMTP认证时使用的密码
        /// </summary>
        private string password = "";

        /// <summary>
        /// 收件人最大数量：现在很多SMTP都限制收件人的最大数量，以防止广告邮件泛滥，最大数量一般都限制在10个以下。
        /// </summary>
        private int RecipientMaxNum = 10;

        /// <summary>
        /// 收件人列表
        /// </summary>
        private ArrayList Recipient = new ArrayList();

        /// <summary>
        ///抄送收件人列表
        /// </summary>
        private ArrayList RecipientCC = new ArrayList();

        /// <summary>
        /// 密送收件人列表
        /// </summary>
        private ArrayList RecipientBCC = new ArrayList();

        /// <summary>
        /// 邮件发送优先级，可设置为"High","Normal","Low"或"1","3","5"
        /// </summary>
        private string priority = "Normal";

        /// <summary>
        /// 错误消息反馈
        /// </summary>
        private string errmsg;

        /// <summary>
        /// 回车换行
        /// </summary>
        private string enter = "\r\n";

        /// <summary>
        /// TcpClient对象，用于连接服务器
        /// </summary>	
        private TcpClient tc;

        /// <summary>
        /// NetworkStream对象
        /// </summary>	
        private NetworkStream ns;

        /// <summary>
        /// SMTP错误代码哈希表
        /// </summary>
        private Hashtable ErrCodeHT = new Hashtable();

        /// <summary>
        /// SMTP正确代码哈希表
        /// </summary>
        private Hashtable RightCodeHT = new Hashtable();

        #endregion

        /// <summary>
        /// SMTP回应代码哈希表
        /// </summary>
        private void SMTPCodeAdd()
        {
            ErrCodeHT.Add("500", "邮箱地址错误");
            ErrCodeHT.Add("501", "参数格式错误");
            ErrCodeHT.Add("502", "命令不可实现");
            ErrCodeHT.Add("503", "服务器需要SMTP验证");
            ErrCodeHT.Add("504", "命令参数不可实现");
            ErrCodeHT.Add("421", "服务未就绪，关闭传输信道");
            ErrCodeHT.Add("450", "要求的邮件操作未完成，邮箱不可用（例如，邮箱忙）");
            ErrCodeHT.Add("550", "要求的邮件操作未完成，邮箱不可用（例如，邮箱未找到，或不可访问）");
            ErrCodeHT.Add("451", "放弃要求的操作；处理过程中出错");
            ErrCodeHT.Add("551", "用户非本地，请尝试<forward-path>");
            ErrCodeHT.Add("452", "系统存储不足，要求的操作未执行");
            ErrCodeHT.Add("552", "过量的存储分配，要求的操作未执行");
            ErrCodeHT.Add("553", "邮箱名不可用，要求的操作未执行（例如邮箱格式错误）");
            ErrCodeHT.Add("432", "需要一个密码转换");
            ErrCodeHT.Add("534", "认证机制过于简单");
            ErrCodeHT.Add("538", "当前请求的认证机制需要加密");
            ErrCodeHT.Add("454", "临时认证失败");
            ErrCodeHT.Add("530", "需要认证");

            RightCodeHT.Add("220", "服务就绪");
            RightCodeHT.Add("250", "要求的邮件操作完成");
            RightCodeHT.Add("251", "用户非本地，将转发向<forward-path>");
            RightCodeHT.Add("354", "开始邮件输入，以<CRLF>.<CRLF>结束");
            RightCodeHT.Add("221", "服务关闭传输信道");
            RightCodeHT.Add("334", "服务器响应验证Base64字符串");
            RightCodeHT.Add("235", "验证成功");
        }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public EmailHelper()
        {
            SMTPCodeAdd();
        }

        /// <summary>
        /// 待邮箱发送配置参数的构造函数
        /// </summary>
        /// <param name="mailServer">邮件服务器</param>
        /// <param name="username">用户名</param>
        /// <param name="password">用户密码</param>
        public EmailHelper(string mailServer, string username, string password) : 
            this(mailServer, username, password, 25)
        {

        }

        /// <summary>
        /// 待邮箱发送配置参数的构造函数
        /// </summary>
        /// <param name="mailServer">邮件服务器</param>
        /// <param name="username">用户名</param>
        /// <param name="password">用户密码</param>
        /// <param name="port">邮箱服务器端口</param>
        public EmailHelper(string mailServer, string username, string password, int port)
        {
            this.MailServer = mailServer;
            this.MailServerUsername = username;
            this.MailServerPassword = password;
            this.MailServerPort = port;

            SMTPCodeAdd();
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        ~EmailHelper()
        {
            if (ns != null)
            {
                ns.Close();
            }
            if (tc != null)
            {
                tc.Close();
            }
        }

        #region 附件

        /// <summary>
        /// 添加一个附件,需使用绝对路径
        /// </summary>	
        public bool AddAttachment(string path)
        {
            if (File.Exists(path))
            {
                Attachments.Add(path);
                return true;
            }
            else
            {
                errmsg += "要附加的文件不存在" + enter;
                return false;
            }
        }

        /// <summary>
        /// 用于分割附件的分割符.
        /// </summary>
        private string boundary  = "=====000_HuolxPubClass113273537350_=====";
        /// <summary>
        /// 分隔符
        /// </summary>
        private string boundary1 = "=====001_HuolxPubClass113273537350_=====";

        /// <summary>
        /// 用于存放附件路径的信息
        /// </summary>		
        private List<string> Attachments = new List<string>();

        /// <summary>
        /// 附件的BASE64编码字符串
        /// </summary>
        /// <param name="path">附件路径</param>
        private string AttachmentB64Str(string path)
        {
            FileStream fs;
            try
            {
                fs = new FileStream(path, System.IO.FileMode.Open,  FileAccess.Read, FileShare.Read);
            }
            catch(Exception ex)
            {
                errmsg += "要附加的文件不存在" + enter;
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(EmailHelper));

                return Base64Encode("要附加的文件:" + path + "不存在");
            }
            int fl = (int)fs.Length;
            byte[] barray = new byte[fl];
            fs.Read(barray, 0, fl);
            fs.Close();
            return B64StrLine(Convert.ToBase64String(barray));
        }

        /// <summary>
        /// 如果文件名中含有非英文字母，则将其编码
        /// </summary>
        private string AttachmentNameStr(string fn)
        {
            if (Encoding.Default.GetByteCount(fn) > fn.Length)
            {
                return "=?" + Charset.ToUpper() + "?B?" + Base64Encode(fn) + "?=";
            }
            else
            {
                return fn;
            }
        }

        private string B64StrLine(string str)
        {
            StringBuilder B64sb = new StringBuilder(str);
            for (int i = 76; i < B64sb.Length; i += 78)
            {
                B64sb.Insert(i, enter);
            }
            return B64sb.ToString();
        }

        /// <summary>
        /// 将字符串编码为Base64字符串
        /// </summary>
        /// <param name="str">要编码的字符串</param>
        private string Base64Encode(string str)
        {
            byte[] barray;
            barray = Encoding.Default.GetBytes(str);
            return Convert.ToBase64String(barray);
        }

        /// <summary>
        /// 将Base64字符串解码为普通字符串
        /// </summary>
        /// <param name="dstr">要解码的字符串</param>
        private string Base64Decode(string dstr)
        {
            byte[] barray;
            barray = Convert.FromBase64String(dstr);
            return Encoding.Default.GetString(barray);
        }

        #endregion

        #region 嵌入图片处理

        private Hashtable EmbedList = new Hashtable(); //widened scope for MatchEvaluator

        /// <summary>
        /// 修改HTML页面中的图片引用为嵌入式图片邮件内容
        /// </summary>
        /// <param name="rawHtml">原始HTML内容</param>
        /// <param name="extras"></param>
        /// <param name="boundaryString"></param>
        /// <returns></returns>
        private string FixupReferences(string rawHtml, ref StringBuilder extras, string boundaryString)
        {
            //Build a symbol table to avoid redundant embedding.
            Regex imgRE, linkRE, hrefRE;
            MatchCollection imgMatches;

            //图片查找正则表达式
            string imgMatchExpression = @"(?<=img+.+src\=[\x27\x22])(?<Url>[^\x27\x22]*)(?=[\x27\x22])";
            imgRE = new Regex(imgMatchExpression, RegexOptions.IgnoreCase | RegexOptions.Compiled);

            //Link内容查找正则表达式
            string linkMatchExpression = "<\\s*link[^>]+href\\s*=\\s*(?:\"(?<1>[^\"]*)\"|(?<1>\\S+))[^>]*>";
            linkRE = new Regex(linkMatchExpression, RegexOptions.IgnoreCase | RegexOptions.Compiled);

            //修正页面内相对URL地址表达式
            string refMatchExpression = "href\\s*=\\s*(?:['\"](?<1>[^\"]*)['\"]|(?<1>\\S+))";
            hrefRE = new Regex(refMatchExpression, RegexOptions.IgnoreCase | RegexOptions.Compiled);

            imgMatches = imgRE.Matches(rawHtml);
            foreach (Match m in imgMatches)
            {
                if (!EmbedList.ContainsKey(m.Groups[1].Value))
                {
                    EmbedList.Add(m.Groups[1].Value, Guid.NewGuid());
                }
            }

            //准备嵌入数据
            extras.Length = 0;
            string contentType;
            ArrayList embeddees = new ArrayList(EmbedList.Keys);
            foreach (string embeddee in embeddees)
            {
                contentType = embeddee.Substring(embeddee.LastIndexOf(".") + 1).ToLower();
                extras.AppendFormat(boundaryString);
                if (contentType.Equals("jpg")) contentType = "jpeg";
                switch (contentType)
                {
                    case "jpeg":
                    case "gif":
                    case "png":
                    case "bmp":
                        extras.AppendFormat("Content-Type: image/{0}; charset=\"iso-8859-1\"\r\n", contentType);
                        extras.Append("Content-Transfer-Encoding: base64\r\n");
                        extras.Append("Content-Disposition: inline\r\n");
                        extras.AppendFormat("Content-ID: <{0}>\r\n\r\n", EmbedList[embeddee]);
                        extras.Append(GetDataAsBase64(embeddee));
                        extras.Append("\r\n");
                        break;
                }
            }
            //Fixups for references to items now embedded
            rawHtml = imgRE.Replace(rawHtml, new MatchEvaluator(FixupEmbedPath));
            return rawHtml;
        }

        /// <summary>
        /// 修正嵌入图片的地址应用为cid:*** 
        /// </summary>
        private string FixupEmbedPath(Match m)
        {
            string replaceThis = m.Groups[1].Value;
            string withThis = string.Format("cid:{0}", EmbedList[replaceThis]);
            return m.Value.Replace(replaceThis, withThis);
        }

        private string GetDataAsBase64(string sUrl)
        {
            WebClient webClient = new WebClient();
            webClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible;MSIE 6.0)");
            MemoryStream memoryStream = new MemoryStream();
            Stream stream = webClient.OpenRead(sUrl);
            byte[] chunk = new byte[4096];
            int cbChunk;
            while ((cbChunk = stream.Read(chunk, 0, 4096)) > 0)
                memoryStream.Write(chunk, 0, cbChunk);
            stream.Close();
            byte[] buf = new byte[memoryStream.Length];
            memoryStream.Position = 0;
            memoryStream.Read(buf, 0, (int)memoryStream.Length);
            memoryStream.Close();
            string b64 = Convert.ToBase64String(buf);
            StringBuilder base64 = new StringBuilder();
            int i;
            for (i = 0; i + 60 < b64.Length; i += 60)
                base64.AppendFormat("{0}\r\n", b64.Substring(i, 60));
            base64.Append(b64.Substring(i));
            for (i = 0; i < (60 - (b64.Length % 60)); i++) base64.Append('=');
            base64.Append("\r\n");
            return base64.ToString();
        }

        private string GetDataAsString(string sUrl)
        {
            WebClient webClient = new WebClient();
            webClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible;MSIE 6.0)");
            return (new StreamReader(webClient.OpenRead(sUrl))).ReadToEnd();
        }
        #endregion

        #region 收件人
        /// <summary>
        /// 添加一个收件人
        /// </summary>	
        /// <param name="str">收件人地址</param>
        /// <param name="ra"></param>
        private bool AddRs(string str, ArrayList ra)
        {
            str = str.Trim();

            if (str == null || str == "" || str.IndexOf("@") == -1)
            {
                return true;
                //				上面的语句自动滤除无效的收件人，为了不影响正常运作，未返回错误，如果您需要严格的检查收件人，请替换为下面的语句。
                //				errmsg+="存在无效收件人：" +str;
                //				return false;
            }

            if (ra.Count < RecipientMaxNum)
            {
                ra.Add(str);
                return true;
            }
            else
            {
                errmsg += "收件人过多";
                return false;
            }
        }
        /// <summary>
        /// 添加一组收件人（不超过10个），参数为字符串数组
        /// </summary>
        /// <param name="str">保存有收件人地址的字符串数组（不超过10个）</param>	
        /// <param name="ra">添加的数组列表</param>
        private bool AddRs(string[] str, ArrayList ra)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (!AddRs(str[i], ra))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 添加一个收件人
        /// </summary>	
        /// <param name="str">收件人地址</param>
        public bool AddRecipient(string str)
        {
            return AddRs(str, Recipient);
        }

        /// <summary>
        /// 指定一个收件人
        /// </summary>	
        /// <param name="str">收件人地址</param>
        public void SetRecipient(string str)
        {
            //return AddRs(str, Recipient);
            Recipient.Clear();
            Recipient.Add(str);
        }

        /// <summary>
        /// 添加一组收件人（不超过10个），参数为字符串数组
        /// </summary>
        /// <param name="str">保存有收件人地址的字符串数组（不超过RecipientMaxNum个）</param>	
        public bool AddRecipient(string[] str)
        {
            return AddRs(str, Recipient);
        }

        /// <summary>
        /// 添加一个抄送收件人
        /// </summary>
        /// <param name="str">收件人地址</param>
        public bool AddRecipientCC(string str)
        {
            return AddRs(str, RecipientCC);
        }

        /// <summary>
        /// 添加一组抄送收件人（不超过10个），参数为字符串数组
        /// </summary>	
        /// <param name="str">保存有收件人地址的字符串数组（不超过RecipientMaxNum个）</param>
        public bool AddRecipientCC(string[] str)
        {
            return AddRs(str, RecipientCC);
        }

        /// <summary>
        /// 添加一个密件收件人
        /// </summary>
        /// <param name="str">收件人地址</param>
        public bool AddRecipientBCC(string str)
        {
            return AddRs(str, RecipientBCC);
        }

        /// <summary>
        /// 添加一组密件收件人（不超过10个），参数为字符串数组
        /// </summary>	
        /// <param name="str">保存有收件人地址的字符串数组（不超过RecipientMaxNum个）</param>
        public bool AddRecipientBCC(string[] str)
        {
            return AddRs(str, RecipientBCC);
        }

        /// <summary>
        /// 清空收件人列表
        /// </summary>
        public void ClearRecipient()
        {
            Recipient.Clear();
        }

        #endregion

        #region 连接邮件服务器

        /// <summary>
        /// 发送SMTP命令
        /// </summary>	
        private bool SendCommand(string Command)
        {
            byte[] WriteBuffer;
            if (Command == null || Command.Trim() == "")
            {
                return true;
            }
            //logs+=Command;
            WriteBuffer = Encoding.Default.GetBytes(Command);
            try
            {
                ns.Write(WriteBuffer, 0, WriteBuffer.Length);
            }
            catch (Exception ex)
            {
                errmsg = "网络连接错误";
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(EmailHelper));
                return false;
            }
            return true;
        }

        /// <summary>
        /// 接收SMTP服务器回应
        /// </summary>
        private string RecvResponse()
        {
            int StreamSize;
            string ReturnValue = "false";
            byte[] ReadBuffer = new byte[4096];

            try
            {
                StreamSize = ns.Read(ReadBuffer, 0, ReadBuffer.Length);
            }
            catch (Exception ex)
            {
                errmsg = "网络连接错误";
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(EmailHelper));
                return ReturnValue;
            }

            if (StreamSize == 0)
            {
                return ReturnValue;
            }
            else
            {
                ReturnValue = Encoding.Default.GetString(ReadBuffer).Substring(0, StreamSize).Trim(); ;
                //logs+=ReturnValue;
                return ReturnValue;
            }
        }

        /// <summary>
        /// 与服务器交互，发送一条命令并接收回应。
        /// </summary>
        /// <param name="Command">一个要发送的命令</param>
        /// <param name="errstr">如果错误，要反馈的信息</param>
        private bool Dialog(string Command, string errstr)
        {
            if (Command == null || Command.Trim() == "")
            {
                return true;
            }
            if (SendCommand(Command))
            {
                string RR = RecvResponse();
                if (RR == "false")
                {
                    return false;
                }
                string RRCode = "";

                if (RR.Length >= 3)
                    RRCode = RR.Substring(0, 3);
                else
                    RRCode = RR;

                if (ErrCodeHT[RRCode] != null)
                {
                    errmsg += (RRCode + ErrCodeHT[RRCode].ToString());
                    errmsg += enter;
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// 与服务器交互，发送一组命令并接收回应。
        /// </summary>
        private bool Dialog(ArrayList Command, string errstr)
        {
            foreach (String item in Command)
            {
                if (!Dialog(item, ""))
                {
                    errmsg += enter;
                    errmsg += errstr;
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// SMTP验证过程.
        /// </summary>
        private bool SmtpAuth()
        {
            ArrayList SendBuffer = new ArrayList();
            string SendBufferstr;
            SendBufferstr = "EHLO " + mailserver + enter;
            //			SendBufferstr="HELO " + mailserver + enter;
            //这个地方经常出现命令错位，不得以加入特殊控制代码，才能正常执行。
            //以后最好能有更好的解决办法。
            if (SendCommand(SendBufferstr))
            {
                while (true)
                {
                    int i = 0;
                    if (ns.DataAvailable)
                    {
                        string RR = RecvResponse();
                        if (RR == "false")
                        {
                            return false;
                        }
                        string RRCode = RR.Substring(0, 3);
                        if (RightCodeHT[RRCode] != null)
                        {
                            if (RR.IndexOf("AUTH") != -1)
                            {
                                break;
                            }
                        }
                        else
                        {
                            if (ErrCodeHT[RRCode] != null)
                            {
                                errmsg += (RRCode + ErrCodeHT[RRCode].ToString());
                                errmsg += enter;
                                errmsg += "发送EHLO命令出错，服务器可能不需要验证" + enter;
                            }
                            else
                            {
                                errmsg += RR;
                                errmsg += "发送EHLO命令出错，不明错误,请与作者联系" + enter;
                            }
                            return false;
                        }
                    }
                    else
                    {
                        System.Threading.Thread.Sleep(50);
                        i++;
                        if (i > 6)
                        {
                            errmsg += "收不到AUTH指令，可能是连接超时，或者服务器根本不需要验证" + enter;
                            return false;

                        }
                    }

                }
            }
            else
            {
                errmsg += "发送ehlo命令失败";
                return false;

            }

            SendBuffer.Add("AUTH LOGIN" + enter);
            SendBuffer.Add(Base64Encode(username) + enter);
            SendBuffer.Add(Base64Encode(password) + enter);
            if (!Dialog(SendBuffer, "SMTP服务器验证失败，请核对用户名和密码。"))
                return false;
            return true;
        }

        #endregion

        #region 发送

        /// <summary>
        /// 发送邮件
        /// </summary>
        public bool SendEmail()
        {
            bool checkFlag = Check();
            if (!checkFlag)
                return false;

            #region 连接网络
            try
            {
                tc = new TcpClient(mailserver, mailserverport);
                ns = tc.GetStream();
            }
            catch (Exception ex)
            {
                errmsg = ex.ToString();
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(EmailHelper));
                return false;
            }

            //验证网络连接是否正确
            if (RightCodeHT[RecvResponse().Substring(0, 3)] == null)
            {
                errmsg = "网络连接失败";
                return false;
            } 
            #endregion

            #region 验证发件收件人

            ArrayList SendBuffer = new ArrayList();
            string SendBufferstr;

            //进行SMTP验证
            if (useSmtpAuth)
            {
                if (!SmtpAuth())
                    return false;
            }
            else
            {
                SendBufferstr = "HELO " + mailserver + enter;
                if (!Dialog(SendBufferstr, ""))
                    return false;
            }

            //发件人信息
            SendBufferstr = "MAIL FROM:<" + From + ">" + enter;
            if (!Dialog(SendBufferstr, "发件人地址错误，或不能为空"))
                return false;

            //收件人列表
            SendBuffer.Clear();
            foreach (String item in Recipient)
            {
                SendBuffer.Add("RCPT TO:<" + item + ">" + enter);
                RecipientName = item;//这里其实只能支持一个收件人
            }
            if (!Dialog(SendBuffer, "收件人地址有误"))
                return false; 

            #endregion

            #region 邮件头部

            //开始发送信件内容
            SendBufferstr = "DATA" + enter;
            if (!Dialog(SendBufferstr, ""))
                return false;
		           
            //发件人
            SendBufferstr = "From:\"" + FromName + "\" <" + From + ">" + enter;
            //收件人
            SendBufferstr += "To:\"" + RecipientName + "\" <" + RecipientName + ">" + enter;

            //回复地址
            if (ReplyTo.Trim() != "")
            {
                SendBufferstr += "Reply-To: " + ReplyTo + enter;
            }

            //抄送收件人列表
            if (RecipientCC.Count > 0)
            {
                SendBufferstr += "CC:";
                foreach (String item in RecipientCC)
                {
                    SendBufferstr += item + "<" + item + ">," + enter;
                }
                SendBufferstr = SendBufferstr.Substring(0, SendBufferstr.Length - 3) + enter;
            }

            //密件收件人列表
            if (RecipientBCC.Count > 0)
            {
                SendBufferstr += "BCC:";
                foreach (String item in RecipientBCC)
                {
                    SendBufferstr += item + "<" + item + ">," + enter;
                }
                SendBufferstr = SendBufferstr.Substring(0, SendBufferstr.Length - 3) + enter;
            }

            //邮件主题
            if (Charset == "")
            {
                SendBufferstr += "Subject:" + Subject + enter;
            }
            else
            {
                SendBufferstr += "Subject:" + "=?" + Charset.ToUpper() + "?B?" + Base64Encode(Subject) + "?=" + enter;
            }

            //是否需要收件人发送收条
            if (true == ReturnReceipt)
            {
                SendBufferstr += "Disposition-Notification-To: \"" + FromName + "\" <" + ReplyTo + ">" + enter;
            }

	        #endregion         

            #region 邮件内容

            SendBufferstr += "X-Priority:" + priority + enter;
            SendBufferstr += "X-MSMail-Priority:" + priority + enter;
            SendBufferstr += "Importance:" + priority + enter;
            SendBufferstr += "X-Mailer: Huolx.Pubclass" + enter;
            SendBufferstr += "MIME-Version: 1.0" + enter;

            SendBufferstr += "Content-Type: multipart/mixed;" + enter;
            SendBufferstr += "	boundary=\"" + boundary + "\"" + enter + enter;
            SendBufferstr += "This is a multi-part message in MIME format." + enter + enter;
            SendBufferstr += "--" + boundary + enter;
            SendBufferstr += "Content-Type: multipart/alternative;" + enter;
            SendBufferstr += "	boundary=\"" + boundary1 + "\"" + enter + enter + enter;
            SendBufferstr += "--" + boundary1 + enter;

            //判断信件格式是否html
            if (IsHtml)
            {
                SendBufferstr += "Content-Type: text/html;" + enter;
            }
            else
            {
                SendBufferstr += "Content-Type: text/plain;" + enter;
            }
            //编码信息
            if (Charset == "")
            {
                SendBufferstr += "	charset=\"iso-8859-1\"" + enter;
            }
            else
            {
                SendBufferstr += "	charset=\"" + Charset.ToLower() + "\"" + enter;
            }
            SendBufferstr += "Content-Transfer-Encoding: base64" + enter;

            StringBuilder extras = new StringBuilder();
            string extrasBoundary = "--" + boundary + enter;
            string newBodyHtml = FixupReferences(this.Body, ref extras, extrasBoundary);
            SendBufferstr += enter + enter;
            SendBufferstr += B64StrLine(Base64Encode(newBodyHtml)) + enter;

            SendBufferstr += enter + "--" + boundary1 + "--" + enter + enter;
            SendBufferstr += extras.ToString();

            //如果有附件,开始发送附件.
            if (Attachments.Count > 0)
            {
                SendBufferstr += enter + "--" + boundary1 + "--" + enter + enter;
                foreach (String item in Attachments)
                {
                    SendBufferstr += "--" + boundary + enter;
                    SendBufferstr += "Content-Type: application/octet-stream;" + enter;
                    SendBufferstr += "	name=\"" + AttachmentNameStr(item.Substring(item.LastIndexOf("\\") + 1)) + "\"" + enter;
                    SendBufferstr += "Content-Transfer-Encoding: base64" + enter;
                    SendBufferstr += "Content-Disposition: attachment;" + enter;
                    SendBufferstr += "	filename=\"" + AttachmentNameStr(item.Substring(item.LastIndexOf("\\") + 1)) + "\"" + enter + enter;
                    SendBufferstr += AttachmentB64Str(item) + enter + enter;
                }
                SendBufferstr += "--" + boundary + "--" + enter + enter;
            }
            SendBufferstr += enter + "." + enter; 

            if (!Dialog(SendBufferstr, "错误信件信息"))
                return false;

            #endregion

            SendBufferstr = "QUIT" + enter;
            if (!Dialog(SendBufferstr, "断开连接时错误"))
                return false;
            
            ns.Close();
            tc.Close();
            return true;
        }

        /// <summary>
        /// 发送邮件前对参数进行检查，通过检查返回True，否则为False
        /// </summary>
        /// <returns>如果通过检查返回true，否则为false</returns>
        private bool Check()
        {
            if (Recipient.Count == 0)
            {
                errmsg = "收件人列表不能为空";
                return false;
            }

            if (RecipientName == "")
            {
                RecipientName = Recipient[0].ToString();
            }

            if (mailserver.Trim() == "")
            {
                errmsg = "必须指定SMTP服务器";
                return false;
            }

            return true;
        }

        #endregion
    }

}

