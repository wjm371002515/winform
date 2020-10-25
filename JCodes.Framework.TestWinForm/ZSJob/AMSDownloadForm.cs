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

namespace JCodes.Framework.Test
{
    public partial class AMSDownloadForm : Form
    {
        public AMSDownloadForm()
        {
            InitializeComponent();
        }

        private void btnDo_Click(object sender, EventArgs e)
        {
            // 取邮箱地址
            // 取邮箱用户名密码
            // 取要拷贝的路径

            AppConfig config = new AppConfig("ZSJob//App.config");

            string OApop3Server = config.AppConfigGet("OApop3Server");
            Int32 OApop3ServerPort = Convert.ToInt32(config.AppConfigGet("OApop3ServerPort"));
            string OAUsername = config.AppConfigGet("OAUsername");
            string OAUserPwd = config.AppConfigGet("OAUserPwd");
            string EmailSubject = config.AppConfigGet("EmailSubject");
            string EmailAddress = config.AppConfigGet("EmailAddress");

            // 加密
            //string encryptPwd = EncodeHelper.Base64Encrypt(OAUserPwd);
            // 解密
            string decryptPwd = EncodeHelper.Base64Decrypt(OAUserPwd);

            string OutPath = config.AppConfigGet("OutPath");

            try
            {
                ReceiveMail(OApop3Server, OApop3ServerPort, OAUsername, decryptPwd, OutPath, EmailSubject, EmailAddress);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(AMSDownloadForm));
                MessageDxUtil.ShowError(ex.Message);
            }
        }

        private void ReceiveMail(string OApop3Server, Int32 OApop3ServerPort, string OAUsername, string OAUserPwd, string OutPath, string EmailSubject, string EmailAddress)
        {
            using (POP3_Client pop3 = new POP3_Client())
            {
                try {
                    pop3.Connect(OApop3Server, OApop3ServerPort, true);
                }
                catch (Exception ex) { }
                pop3.Connect(OApop3Server, OApop3ServerPort, true);
                pop3.Login(OAUsername, OAUserPwd);//两个参数，前者为Email的账号，后者为Email的密码  

                POP3_ClientMessageCollection messages = pop3.Messages;

                LogHelper.WriteLog(LogLevel.LOG_LEVEL_INFO, string.Format("共{0}封邮件", messages.Count), typeof(AMSDownloadForm));

                for (int i = 0; i < messages.Count; i++)
                {
                    POP3_ClientMessage message = messages[i];//转化为POP3  
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, string.Format("正在检查第{0}封邮件...", i + 1), typeof(AMSDownloadForm));
                    if (message != null)
                    {
                        byte[] messageBytes = null;
                        try {
                            messageBytes = message.MessageToByte();
                        }
                        catch (Exception ex) {
                            LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, string.Format("第{0}封邮件，message.MessageToByte() 失败 错误日志参考下一条信息", i), typeof(AMSDownloadForm));
                            LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(AMSDownloadForm));
                        }
                        Mail_Message mime_message = Mail_Message.ParseFromByte(messageBytes);

                        string sender = mime_message.From == null ? "sender is null" : mime_message.From[0].DisplayName;
                        string senderAddress = mime_message.From == null ? "senderAddress is null" : mime_message.From[0].Address;
                        string subject = mime_message.Subject ?? "subject is null";
                        string recDate = mime_message.Date == DateTime.MinValue ? "date not specified" : mime_message.Date.ToString();
                        string content = mime_message.BodyText ?? "content is null";

                        LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, string.Format("正在检查第{0}封邮件, 邮件发送时间{1}", i + 1, mime_message.Date), typeof(AMSDownloadForm));
                        //Console.WriteLine("内容为{0}", content);

                        // 判断此邮件是否是需要下载的邮件
                        // 发件人 & 发件主体 && 收件时间
                        DateTime dt = DateTime.Now;
                        if (string.Equals(senderAddress, EmailAddress) && string.Equals(subject, EmailSubject.Replace("yyyyMMdd", dt.ToString("yyyyMMdd"))) && mime_message.Date.Date == dt.Date)
                        {
                            MIME_Entity[] attachments = mime_message.GetAttachments(true, true);

                            foreach (MIME_Entity entity in attachments)
                            {
                                if (entity.ContentDisposition != null)
                                {
                                    string fileName = entity.ContentDisposition.Param_FileName;
                                    if (!string.IsNullOrEmpty(fileName))
                                    {
                                        DirectoryInfo dir = new DirectoryInfo(OutPath);
                                        if (!dir.Exists) dir.Create();
                                        DirectoryInfo dir2 = new DirectoryInfo(OutPath + @"\" + DateTime.Now.ToString("yyyyMMdd") + @"\");
                                        if (!dir2.Exists) dir2.Create();

                                        string path = Path.Combine(dir.FullName, fileName);
                                        MIME_b_SinglepartBase byteObj = (MIME_b_SinglepartBase)entity.Body;
                                        Stream decodedDataStream = byteObj.GetDataStream();
                                        using (FileStream fs = new FileStream(path, FileMode.Create))
                                        {
                                            LumiSoft.Net.Net_Utils.StreamCopy(decodedDataStream, fs, 4000);
                                        }

                                        LogHelper.WriteLog(LogLevel.LOG_LEVEL_INFO, string.Format("{0}已经被下载。", fileName), typeof(AMSDownloadForm));

                                        // 下载完成之后解压
                                        (new FastZip()).ExtractZip(dir.FullName + fileName, dir2.FullName, "");

                                        if (dir2.GetFiles().Length > 0 && dir2.GetDirectories().Length > 0)
                                            MessageDxUtil.ShowTips("下载解压文件成功");
                                    }
                                }
                            }

                            if (MessageDxUtil.ShowTips("下载成功") == DialogResult.OK)
                            {
                                Process.Start("explorer.exe", OutPath);
                            }

                            break;
                        }

                        // 时间超过了也退出
                        if (mime_message.Date.Date < dt.Date)
                        {
                            MessageDxUtil.ShowTips("根网清算文件未到，请稍后处理");
                            break;
                        }
                    }
                }
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_INFO, "执行结束", typeof(AMSDownloadForm));

            }  
        }

        private void ShowInfo()
        {
            AppConfig config = new AppConfig("ZSJob//App.config");
            string OApop3Server = config.AppConfigGet("OApop3Server");
            string OApop3ServerPort = config.AppConfigGet("OApop3ServerPort");
            string OAUsername = config.AppConfigGet("OAUsername");
            string OAUserPwd = config.AppConfigGet("OAUserPwd");
            string EmailSubject = config.AppConfigGet("EmailSubject");
            string EmailAddress = config.AppConfigGet("EmailAddress");
            string OutPath = config.AppConfigGet("OutPath");

            txtpop3Server.Text = OApop3Server;
            txtpop3ServerPort.Text = OApop3ServerPort;
            txtOAuserName.Text = OAUsername;
            txtPwd.Text = OAUserPwd;
            txtSubject.Text = EmailSubject;
            txtAddress.Text = EmailAddress;
            txtPath.Text = OutPath;
        }

        // 变更数据
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            AppConfig config = new AppConfig("ZSJob//App.config");
            // 检查端口是否为整数
            if (!ValidateUtil.IsValidInt(config.AppConfigGet("OApop3ServerPort")))
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_ERR, "POP3服务器端口必须是整数", typeof(AMSDownloadForm));
                MessageDxUtil.ShowError("POP3服务器端口必须是整数");
                return;
            }

            config.AppConfigSet("OApop3Server", txtpop3Server.Text.Trim());
            config.AppConfigSet("OApop3ServerPort", txtpop3ServerPort.Text.Trim());
            config.AppConfigSet("OAUsername", txtOAuserName.Text.Trim());
            config.AppConfigSet("OAUserPwd", EncodeHelper.Base64Encrypt(txtPwd.Text.Trim()));
            config.AppConfigSet("EmailSubject", txtSubject.Text.Trim());
            config.AppConfigSet("EmailAddress", txtAddress.Text.Trim());
            config.AppConfigSet("OutPath", txtPath.Text.Trim());

            ShowInfo();

        }

        private void AMSDownloadForm_Load(object sender, EventArgs e)
        {
            ShowInfo();
        }
    }
}
