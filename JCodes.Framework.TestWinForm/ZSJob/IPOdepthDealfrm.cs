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
using System.Collections.Generic;
using SQLiteQueryBrowser;
using System.Data.SQLite;
using System.Text.RegularExpressions;
using JCodes.Framework.TestWinForm.ZSZQ_IPO;
using System.Drawing;
using JCodes.Framework.Common.Office;
using SevenZip;
using System.Diagnostics;
using Microsoft.Win32;
using Aspose.Cells;

namespace JCodes.Framework.Test
{
    public partial class IPOdepthDealfrm : Form
    {
        // 保存UID 判断是否下载过
        private List<string> uidLst = new List<string>();
        private const string dbFile = @"DB\zszq_ipo.db";
        public static readonly string UserNameRegex = @"^[\u4E00-\u9FA5\uf900-\ufa2d·s]{2,128}$";
        public static readonly string ShenfenzhangRegex = @"^\d{15}|\d{18}$";
        private Int32 normalRow = 13;
        private Int32 OffsetRow = 0;

        public IPOdepthDealfrm()
        {
            InitializeComponent();
        }

        private void btnDo_Click(object sender, EventArgs e)
        {
            
            ((Action)DoWork).BeginInvoke(null, null); //调用委托的异步执行方法，回调函数为空 
        }

        private void DoWork()
        {
            btnUpdate.Invoke((Action<string>)delegate(string a) //在控件对象所在的线程上执行委托
            {
                btnUpdate.Text = a;
            }, "开始下载");

            // 取邮箱地址
            // 取邮箱用户名密码
            // 取要拷贝的路径

            AppConfig config = new AppConfig("ZSJob//App.config");

            config.AppConfigSet("OApop3Server", txtpop3Server.Text);
            config.AppConfigSet("OApop3ServerPort", txtpop3ServerPort.Text);
            config.AppConfigSet("OAUsername", txtOAuserName.Text);
            config.AppConfigSet("OAUserPwd", EncodeHelper.Base64Encrypt(txtPwd.Text));
            config.AppConfigSet("EmailSubject", txtSubject.Text);
            config.AppConfigSet("StartDateTime", ddpStartDateTime.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            config.AppConfigSet("EndDateTime", ddpDateTimeEnd.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            config.AppConfigSet("OutPath", txtPath.Text);


            string OApop3Server = config.AppConfigGet("OApop3Server");
            Int32 OApop3ServerPort = Convert.ToInt32(config.AppConfigGet("OApop3ServerPort"));
            string OAUsername = config.AppConfigGet("OAUsername");
            string OAUserPwd = config.AppConfigGet("OAUserPwd");
            string OutPath = config.AppConfigGet("OutPath");
            string EmailSubject = config.AppConfigGet("EmailSubject");
            DateTime startDt = Convert.ToDateTime(config.AppConfigGet("StartDateTime"));
            DateTime endDt = Convert.ToDateTime(config.AppConfigGet("EndDateTime"));

            // 加密
            //string encryptPwd = EncodeHelper.Base64Encrypt(OAUserPwd);
            // 解密
            string decryptPwd = EncodeHelper.Base64Decrypt(OAUserPwd);

            try
            {
                // 点击按钮清空
                uidLst.Clear();

                ReceiveMail(OApop3Server, OApop3ServerPort, OAUsername, decryptPwd, OutPath, EmailSubject, startDt, endDt);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(AMSDownloadForm));
                MessageDxUtil.ShowError(ex.Message);
            }
        }

        private void ReceiveMail(string OApop3Server, Int32 OApop3ServerPort, string OAUsername, string OAUserPwd, string OutPath, string EmailSubject, DateTime startDt, DateTime endDt)
        {
            Int32 downLoadCount = 0;

            // 清空数据库
            string sql = "delete from T_ReceiveInfos";
            SQLiteDBHelper db = new SQLiteDBHelper(dbFile);
            db.ExecuteNonQuery(sql, null);

            // 查询数据库时间的数据
            sql = string.Format("select uid from t_downloademail where receive_time < '{0}' order by receive_time", startDt.ToString("yyyy-MM-dd HH:mm:ss"));
            db = new SQLiteDBHelper(dbFile);
            using (SQLiteDataReader reader = db.ExecuteReader(sql, null))
            {
                while (reader.Read())
                {
                    uidLst.Add(reader.GetString(0));
                }
            } 

            using (POP3_Client pop3 = new POP3_Client())
            {
                pop3.Connect(OApop3Server, OApop3ServerPort, true);
                pop3.Login(OAUsername, OAUserPwd);//两个参数，前者为Email的账号，后者为Email的密码  

                POP3_ClientMessageCollection messages = pop3.Messages;

                LogHelper.WriteLog(LogLevel.LOG_LEVEL_INFO, string.Format("共{0}封邮件", messages.Count), typeof(AMSDownloadForm));

                // 清空原始文件目录
                if (!DirectoryUtil.IsEmptyDirectory(OutPath))
                {
                    DirectoryUtil.ClearDirectory(OutPath);
                }

                // 处理数据 需要的数据拷贝到DealData目录
                string DealFolder = "DealData";
                if (!DirectoryUtil.IsExistDirectory(OutPath + @"\" + DealFolder))
                {
                    DirectoryUtil.CreateDirectory(OutPath + @"\" + DealFolder);
                }
                if (!DirectoryUtil.IsEmptyDirectory(OutPath + @"\" + DealFolder))
                {
                    DirectoryUtil.ClearDirectory(OutPath + @"\" + DealFolder);
                }

                for (int i = 0; i < messages.Count; i++)
                {
                    btnUpdate.Invoke((Action<string>)delegate(string a) //在控件对象所在的线程上执行委托
                    {
                        btnUpdate.Text = a;
                    }, string.Format("目前处理第" + i + "封邮件，共下载" + downLoadCount + "个附件，一共有" + messages.Count + "封邮件"));

                    try {

                    POP3_ClientMessage message = messages[i];//转化为POP3  

                    // 判断是否下载过 (中途断了连接) start
                    if (uidLst.Contains(message.UID))
                        continue;

                    uidLst.Add(message.UID);
                    // 判断是否下载过 (中途断了连接) end

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

                            // 断开连接重新连接
                            //pop3.Connect(OApop3Server, OApop3ServerPort, true);
                            //pop3.Login(OAUsername, OAUserPwd);//两个参数，前者为Email的账号，后者为Email的密码  
                            //messages = pop3.Messages;

                            continue;
                        }
                        Mail_Message mime_message = Mail_Message.ParseFromByte(messageBytes);

                        // TODO 对象化
                        string sender = mime_message.From == null ? "sender is null" : mime_message.From[0].DisplayName;
                        string senderAddress = mime_message.From == null ? "senderAddress is null" : mime_message.From[0].Address;
                        string subject = mime_message.Subject ?? "subject is null";
                        DateTime recDate = mime_message.Date == DateTime.MinValue ? endDt : mime_message.Date;
                        string content = mime_message.BodyText ?? "content is null";

                        LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, string.Format("正在检查第{0}封邮件, 邮件发送时间{1}", i + 1, mime_message.Date), typeof(AMSDownloadForm));

                        // 这里记录到数据库UID 和 接收时间 方便后期直接跳过 插入数据库中 已经导入的数据 对于不需要的时间直接跳过提高性能
                        sql = "INSERT INTO T_DownloadEmail(receive_time, uid)values(@receive_time, @uid)";
                        db = new SQLiteDBHelper(dbFile);
                        SQLiteParameter[] parameters = new SQLiteParameter[]{ 
                           new SQLiteParameter("@receive_time", recDate), 
                           new SQLiteParameter("@uid", message.UID)
                        };
                        db.ExecuteNonQuery(sql, parameters);
                        // 这里记录到数据库UID  end

                        //Console.WriteLine("内容为{0}", content);
                        // 判断收到的邮件是否在需要循扫范围之内
                        if (startDt > recDate || recDate > endDt)
                        {
                            continue;
                        }

                        // 判断此邮件是否是需要下载的邮件
                        // 发件人 & 发件主体 && 收件时间
                        //if (string.Equals(subject, EmailSubject.Replace("yyyyMMdd", dt.ToString("yyyyMMdd"))) && mime_message.Date.Date == dt.Date)
                        if (startDt <= recDate && recDate <= endDt && subject.Contains(EmailSubject))
                        {
                            MIME_Entity[] attachments = mime_message.GetAttachments(true, true);

                            foreach (MIME_Entity entity in attachments)
                            {
                                if (entity.ContentDisposition != null)
                                {
                                    // 特殊处理如果文件中含有中文 系统截图不到位就会出现？ 
                                    string fileName = entity.ContentDisposition.Param_FileName;
                                    if (!string.IsNullOrEmpty(fileName)) fileName = fileName.Replace("?", ".");

                                    // tmp 临时下载看一下有哪些文件格式
                                    /*if (!string.IsNullOrEmpty(fileName))
                                    {
                                        DirectoryInfo dir = new DirectoryInfo(OutPath+@"\tmp");
                                        if (!dir.Exists) dir.Create();
                                        string path = Path.Combine(dir.FullName, fileName);
                                        MIME_b_SinglepartBase byteObj = (MIME_b_SinglepartBase)entity.Body;
                                        Stream decodedDataStream = byteObj.GetDataStream();
                                        using (FileStream fs = new FileStream(path, FileMode.Create))
                                        {
                                            LumiSoft.Net.Net_Utils.StreamCopy(decodedDataStream, fs, 4000);
                                        }
                                    }*/

                                    // 支持rar zip 7z 后缀
                                    if (!string.IsNullOrEmpty(fileName) && (string.Equals(FileUtil.GetExtension(fileName), ".xls", StringComparison.OrdinalIgnoreCase) || string.Equals(FileUtil.GetExtension(fileName), ".xlsx", StringComparison.OrdinalIgnoreCase) || string.Equals(FileUtil.GetExtension(fileName), ".rar", StringComparison.OrdinalIgnoreCase) || string.Equals(FileUtil.GetExtension(fileName), ".zip", StringComparison.OrdinalIgnoreCase) || string.Equals(FileUtil.GetExtension(fileName), ".7z", StringComparison.OrdinalIgnoreCase)))
                                    {
                                        DirectoryInfo dir = new DirectoryInfo(OutPath);
                                        if (!dir.Exists) dir.Create();
                                        DirectoryInfo dir2 = new DirectoryInfo(OutPath + @"\" + subject.Replace(":", "").Replace("*", "").Replace("?", "").Replace("<", "").Replace(">", "").Replace("|", ""));
                                        if (!dir2.Exists) dir2.Create();

                                        //fastZip.ExtractZip(, @"G:\ipo_files\tmp\111\", "");
                                        //fastZip.ExtractZip(@"G:\ipo_files\tmp\财通证券资产管理有限公司-其他-17巨化EB网下认购申请表.rar", , "");
                                        // 处理标题一样 文件名一样的文件
                                        fileName = string.Format("{0}_{1}", recDate.ToString("yyyyMMddHHmmss"),fileName);
                                        string path = Path.Combine(dir2.FullName, fileName);
                                        MIME_b_SinglepartBase byteObj = (MIME_b_SinglepartBase)entity.Body;
                                        Stream decodedDataStream = byteObj.GetDataStream();
                                        using (FileStream fs = new FileStream(path, FileMode.Create))
                                        {
                                            LumiSoft.Net.Net_Utils.StreamCopy(decodedDataStream, fs, 4000);

                                            downLoadCount++;
                                        }

                                        LogHelper.WriteLog(LogLevel.LOG_LEVEL_INFO, string.Format("{0}已经被下载。", fileName), typeof(AMSDownloadForm));


                                        // 解压缩文件
                                        List<string> files = new List<string>();
                                        // rar 文件
                                        if (string.Equals(FileUtil.GetExtension(fileName), ".rar", StringComparison.OrdinalIgnoreCase))
                                        {
                                            DirectoryInfo dir3 = new DirectoryInfo(dir2.FullName + @"\RAR");
                                            if (!dir3.Exists) dir3.Create();

                                            UnRAR(path, dir3.FullName);

                                            String[] xlsfiles = DirectoryUtil.GetFileNames(dir3.FullName, "*.xls", true);
                                            String[] xlsxfiles = DirectoryUtil.GetFileNames(dir3.FullName, "*.xlsx", true);

                                            for (int j = 0; j < xlsfiles.Length; j++)
                                            {
                                                files.Add(xlsfiles[j]);
                                            }
                                            for (int j = 0; j < xlsxfiles.Length; j++)
                                            {
                                                files.Add(xlsxfiles[j]);
                                            }
                                        }
                                        // zip
                                        if (string.Equals(FileUtil.GetExtension(fileName), ".zip", StringComparison.OrdinalIgnoreCase))
                                        {
                                            DirectoryInfo dir3 = new DirectoryInfo(dir2.FullName + @"\ZIP");
                                            if (!dir3.Exists) dir3.Create();

                                            FastZip fastZip = new FastZip();
                                            fastZip.ExtractZip(path, dir3.FullName, "");

                                            String[] xlsfiles  = DirectoryUtil.GetFileNames(dir3.FullName, "*.xls", true);
                                            String[] xlsxfiles = DirectoryUtil.GetFileNames(dir3.FullName, "*.xlsx", true);
                                            
                                            for (int j = 0; j < xlsfiles.Length; j++)
                                            {
                                                files.Add(xlsfiles[j]);
                                            }
                                            for (int j = 0; j < xlsxfiles.Length; j++)
                                            {
                                                files.Add(xlsxfiles[j]);
                                            }

                                        }

                                        // 7z
                                        if (string.Equals(FileUtil.GetExtension(fileName), ".7z", StringComparison.OrdinalIgnoreCase))
                                        {
                                            DirectoryInfo dir3 = new DirectoryInfo(dir2.FullName + @"\7Z");
                                            if (!dir3.Exists) dir3.Create();

                                            FastZip fastZip = new FastZip();
                                            using (var tmp = new SevenZipExtractor(path))
                                            {
                                                for (var j = 0; i < tmp.ArchiveFileData.Count; j++)
                                                {
                                                    tmp.ExtractFiles(dir3.FullName, tmp.ArchiveFileData[j].Index);
                                                }
                                            }

                                            String[] xlsfiles = DirectoryUtil.GetFileNames(dir3.FullName, "*.xls", true);
                                            String[] xlsxfiles = DirectoryUtil.GetFileNames(dir3.FullName, "*.xlsx", true);

                                            for (int j = 0; j < xlsfiles.Length; j++)
                                            {
                                                files.Add(xlsfiles[j]);
                                            }
                                            for (int j = 0; j < xlsxfiles.Length; j++)
                                            {
                                                files.Add(xlsxfiles[j]);
                                            }
                                        }

                                        if (string.Equals(FileUtil.GetExtension(fileName), ".xls", StringComparison.OrdinalIgnoreCase) || string.Equals(FileUtil.GetExtension(fileName), ".xlsx", StringComparison.OrdinalIgnoreCase))
                                        {
                                            files.Add(path);
                                        }

                                        // 遍历循环数组
                                        foreach (var file in files)
                                        {
                                            Workbook workbook = null;
                                            // 特殊处理 如果遇到打不开的文件则此文件被破坏需要跳过
                                            try
                                            {
                                                // 读取xls数据 判断是否是合法的xls表格
                                                workbook = new Workbook(file);
                                            }
                                            catch (Exception ex)
                                            {
                                                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, string.Format("{0}文件被破坏打不开", file), typeof(IPOdepthDealfrm));
                                                continue;
                                            }

                                            Worksheet sheet = workbook.Worksheets[0];
                                            Cells cells = sheet.Cells;

                                            Console.WriteLine(cells[normalRow, 2 - 1].StringValue.Trim() + ", " + cells[normalRow, 3 - 1].StringValue.Trim() + ", " + cells[normalRow, 6 - 1].StringValue.Trim() + ", " + cells[normalRow, 7 - 1].StringValue.Trim());
                                            // 特殊处理 sheet[13, 3].Value.Trim() 未填信息 取文件名
                                            // && (Regex.IsMatch(sheet[13, 3].Value.Trim(), UserNameRegex) || string.IsNullOrEmpty(sheet[13, 3].Value.Trim()))
                                            if (cells[normalRow, 2 - 1].StringValue.Trim().Contains("经办人姓名") && Regex.IsMatch(cells[normalRow, 3 - 1].StringValue.Trim(), UserNameRegex) && cells[normalRow, 6 - 1].StringValue.Trim().Contains("经办人身份证号") && Regex.IsMatch(cells[normalRow, 7 - 1].StringValue.Trim(), ShenfenzhangRegex) && cells[normalRow - 1, 2 - 1].StringValue.Trim().Contains("机构全称（合格机构投资者填写）"))
                                            {
                                                OffsetRow = 0;
                                                sql = "INSERT INTO T_ReceiveInfos(EmailTitle, EmailReceive, Email, UserName, Shenfenzheng, FilePath, FileName, OrgName)values(@EmailTitle, @EmailReceive, @Email, @UserName, @Shenfenzheng, @FilePath, @FileName, @OrgName)";
                                                db = new SQLiteDBHelper(dbFile);

                                                string orgName = string.IsNullOrEmpty(cells[normalRow - 1, 3 - 1].StringValue.Trim()) ? fileName.Split('-')[0] : cells[normalRow - 1, 3 - 1].StringValue.Trim();

                                                parameters = new SQLiteParameter[]{ 
                                                   new SQLiteParameter("@EmailTitle", subject), 
                                                   new SQLiteParameter("@EmailReceive", recDate),
                                                   new SQLiteParameter("@Email", senderAddress), 
                                                   new SQLiteParameter("@UserName", cells[normalRow, 3- 1].StringValue.Trim()),
                                                   new SQLiteParameter("@Shenfenzheng", cells[normalRow, 7- 1].StringValue.Trim()),
                                                   new SQLiteParameter("@FilePath", file),
                                                   new SQLiteParameter("@FileName", fileName),
                                                   new SQLiteParameter("@OrgName", orgName),
                                                };
                                                db.ExecuteNonQuery(sql, parameters);
                                            }
                                            else
                                            { 
                                                // 特殊处理 兼容不是14行的模式 向下找10行 向上找10行 找不到就找不到
                                                for (Int32 j = -10; j < +10; j ++)
                                                {
                                                    if (cells[normalRow + j, 2 - 1].StringValue.Trim().Contains("经办人姓名") && Regex.IsMatch(cells[normalRow + j, 3 - 1].StringValue.Trim(), UserNameRegex) && cells[normalRow + j, 6 - 1].StringValue.Trim().Contains("经办人身份证号") && Regex.IsMatch(cells[normalRow + j, 7 - 1].StringValue.Trim(), ShenfenzhangRegex) && cells[normalRow + j - 1, 2 - 1].StringValue.Trim().Contains("机构全称（合格机构投资者填写）"))
                                                    {
                                                        OffsetRow = j;
                                                        sql = "INSERT INTO T_ReceiveInfos(EmailTitle, EmailReceive, Email, UserName, Shenfenzheng, FilePath, FileName, OrgName)values(@EmailTitle, @EmailReceive, @Email, @UserName, @Shenfenzheng, @FilePath, @FileName, @OrgName)";
                                                        db = new SQLiteDBHelper(dbFile);

                                                        string orgName = string.IsNullOrEmpty(cells[normalRow + j - 1, 3 - 1].StringValue.Trim()) ? fileName.Split('-')[0] : cells[normalRow + j - 1, 3 - 1].StringValue.Trim();

                                                        parameters = new SQLiteParameter[]{ 
                                                           new SQLiteParameter("@EmailTitle", subject), 
                                                           new SQLiteParameter("@EmailReceive", recDate),
                                                           new SQLiteParameter("@Email", senderAddress), 
                                                           new SQLiteParameter("@UserName", cells[normalRow + j, 3 - 1].StringValue.Trim()),
                                                           new SQLiteParameter("@Shenfenzheng", cells[normalRow + j, 7 - 1].StringValue.Trim()),
                                                           new SQLiteParameter("@FilePath", file),
                                                           new SQLiteParameter("@FileName", fileName),
                                                           new SQLiteParameter("@OrgName", orgName),
                                                        };
                                                        db.ExecuteNonQuery(sql, parameters);
                                                    }
                                                }
                                            }

                                        }
                                        // 这里记录到数据库UID 和 接收时间 方便后期直接跳过 插入数据库中 已经导入的数据 对于不需要的时间直接跳过提高性能
                                        /*sql = "INSERT INTO T_DownloadEmail(receive_time, uid)values(@receive_time, @uid)";
                                        db = new SQLiteDBHelper(dbFile);
                                        parameters = new SQLiteParameter[]{ 
                                           new SQLiteParameter("@receive_time", recDate), 
                                           new SQLiteParameter("@uid", message.UID)
                                        };
                                        db.ExecuteNonQuery(sql, parameters);*/


                                        // 下载完成之后解压 不需要压缩
                                        /*(new FastZip()).ExtractZip(dir.FullName + fileName, dir2.FullName, "");

                                        if (dir2.GetFiles().Length > 0 && dir2.GetDirectories().Length > 0)
                                            MessageDxUtil.ShowTips("下载解压文件成功");
                                         * */ 
                                    }
                                }
                            }
                            
                            continue;
                        }
                    }

                    }
                    catch (Exception ex)
                    {
                        LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(AMSDownloadForm));
                    }
                }

                // 复制文件到指定路径
                sql = string.Format("select shenfenzheng, emailreceive, FilePath, FileName from t_receiveinfos a order by OrgName,shenfenzheng, EmailReceive desc");
                db = new SQLiteDBHelper(dbFile);
                Int32 FileId = 0;
                String tmpShenfenzheng = string.Empty;
                DateTime tmpReceive = DateTime.Now;
                List<String> effectiveFile = new List<string>();
                using (SQLiteDataReader reader = db.ExecuteReader(sql, null))
                {
                    while (reader.Read())
                    {

                        if (reader.GetString(0) == tmpShenfenzheng && DateTime.Equals(reader.GetDateTime(1), tmpReceive))
                            Console.WriteLine("同一封邮件");

                        // 假如身份证不同则换了一个经办人
                        // 假如下一条身份证和接受时间相同那就是说明这2个附件是同一封邮件
                        if (tmpShenfenzheng != reader.GetString(0) || (reader.GetString(0) == tmpShenfenzheng &&  DateTime.Equals(reader.GetDateTime(1), tmpReceive)))
                        {
                            tmpShenfenzheng = reader.GetString(0);
                            tmpReceive = reader.GetDateTime(1);
                            string todealFileName = string.Empty;
                            // 特殊处理
                            if (string.Equals(FileUtil.GetExtension(reader.GetString(3)), ".rar", StringComparison.OrdinalIgnoreCase) || string.Equals(FileUtil.GetExtension(reader.GetString(3)), ".zip", StringComparison.OrdinalIgnoreCase) || string.Equals(FileUtil.GetExtension(reader.GetString(3)), ".7z", StringComparison.OrdinalIgnoreCase))
                            {
                                todealFileName = FileUtil.GetFileName(reader.GetString(2));
                            }
                            else
                                todealFileName  = (++FileId) + "_" + reader.GetString(3);
                             
                            FileUtil.Copy(reader.GetString(2), OutPath + @"\" + DealFolder + @"\" + todealFileName);

                            Console.WriteLine("shenfenzheng: " + reader.GetString(0) + "， emailreceive:" + reader.GetDateTime(1) + ", FilePath:" + reader.GetString(2) + ", FileName:" + reader.GetString(3));

                            effectiveFile.Add(todealFileName);
                        }
                    }
                } 

                // 写入汇总表文件中 最终文件写到如输出路径中
                string xlsfileName = "网下利率询价及申购申请表汇总表.xlsx";
                if (FileUtil.FileIsExist(OutPath + @"\" + xlsfileName))
                {
                    if (FileUtil.FileIsUsing(OutPath + @"\" + xlsfileName))
                    {
                        MessageBox.Show("文件被占用请先关闭对文件的操作之后再重新点击操作");
                        return;
                    }

                    FileUtil.DeleteFile(OutPath + @"\" + xlsfileName);
                }

                // 拷贝一个新的文件到执行路径
                FileUtil.Copy("Files\\" + xlsfileName, OutPath + @"\" + xlsfileName);

                // 把数据写入到汇总表中
                Workbook workbookwrite = new Workbook(OutPath + @"\" + xlsfileName);
                Worksheet sheetwrite = workbookwrite.Worksheets[0];
                Cells cellswrite = sheetwrite.Cells;

                Int32 sheetwriteRow = 1;

                // 不同文件属于同一个结构
                try
                {
                    String tmpOrgName = String.Empty;
                    Int32 tmpOrgNameCount = 0;

                    // 分析每个文件的数据 然后写入到xls中
                    foreach (var fileName in effectiveFile)
                    {
                        // 文件路径OutPath + @"\" + DealFolder + @"\" + todealFileName
                        Workbook workbook = new Workbook(OutPath + @"\" + DealFolder + @"\" + fileName);
                        Worksheet sheet = workbook.Worksheets[0];
                        Cells cells = sheet.Cells;
                        List<PurchaseIPOInfo> purchaseIPOlst = new List<PurchaseIPOInfo>();
                    
                        String tmpUserName = String.Empty;
                        String tmpTelephone = String.Empty;
                        String tmpiPhone = String.Empty;
                        String tmpEMail = String.Empty;
                        Int32 tmpDataRow = 16; // 数据在17行开始

                        try
                        {
                            string orgName = string.IsNullOrEmpty(cells[normalRow - 1 + OffsetRow, 3 - 1].StringValue.Trim()) ? fileName.Split('-')[0] : cells[normalRow - 1 + OffsetRow, 3 - 1].StringValue.Trim();

                            if (tmpOrgName != orgName)
                                tmpOrgNameCount++;

                            tmpOrgName = orgName;
                            tmpUserName = cells[normalRow, 3 - 1].StringValue.Trim();
                            tmpTelephone = cells[normalRow + 1 + OffsetRow, 3 - 1].StringValue.Trim();
                            tmpiPhone = cells[normalRow + 1 + OffsetRow, 7 - 1].StringValue.Trim();
                            tmpEMail = cells[normalRow, 10 - 1].StringValue.Trim();
                        }
                        catch (Exception ex)
                        {
                            LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, "分析表头失败:" + OutPath + @"\" + DealFolder + @"\" + fileName, typeof(IPOdepthDealfrm));
                            continue;
                        }

                        while (!string.IsNullOrWhiteSpace(cells[tmpDataRow + OffsetRow, 2 - 1].StringValue.Trim()) && !string.IsNullOrWhiteSpace(cells[tmpDataRow + OffsetRow, 3 - 1].StringValue.Trim()))
                        {
                            LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, "sheet2:" + cells[tmpDataRow + OffsetRow, 2 - 1].StringValue.Trim() + ", sheet3:" + cells[tmpDataRow + OffsetRow, 3 - 1].StringValue.Trim(), typeof(IPOdepthDealfrm));

                            try
                            {
                                PurchaseIPOInfo onePurchaseIPO = new PurchaseIPOInfo();
                                onePurchaseIPO.Id = tmpOrgNameCount;
                                onePurchaseIPO.OrgName = tmpOrgName;
                                onePurchaseIPO.UserName = tmpUserName;
                                onePurchaseIPO.Telephone = tmpTelephone;
                                onePurchaseIPO.iPhone = tmpiPhone;
                                onePurchaseIPO.EMail = tmpEMail;
                                onePurchaseIPO.StockholderName = cells[tmpDataRow + OffsetRow, 2 - 1].StringValue.Trim();
                                onePurchaseIPO.StockholderCode = cells[tmpDataRow + OffsetRow, 3 - 1].StringValue.Trim();
                                onePurchaseIPO.Seat = cells[tmpDataRow + OffsetRow, 4 - 1].StringValue.Trim();
                                onePurchaseIPO.UserID = cells[tmpDataRow + OffsetRow, 5 - 1].StringValue.Trim();
                                onePurchaseIPO.Rate = cells[tmpDataRow + OffsetRow, 6 - 1].StringValue.Trim();
                                onePurchaseIPO.Money = cells[tmpDataRow + OffsetRow, 7 - 1].StringValue.Trim();
                                onePurchaseIPO.RefundBank = cells[tmpDataRow + OffsetRow, 12 - 1].StringValue.Trim();
                                onePurchaseIPO.RefundName = cells[tmpDataRow + OffsetRow, 14 - 1].StringValue.Trim();
                                onePurchaseIPO.RefundCard = cells[tmpDataRow + OffsetRow, 13 - 1].StringValue.Trim();
                                onePurchaseIPO.RefundSystemNo = cells[tmpDataRow + OffsetRow, 16 - 1].StringValue.Trim();
                                onePurchaseIPO.RefundProvince = cells[tmpDataRow + OffsetRow, 15 - 1].StringValue.Trim();
                                purchaseIPOlst.Add(onePurchaseIPO);

                                if (!string.IsNullOrWhiteSpace(cells[tmpDataRow + OffsetRow, 8 - 1].StringValue.Trim()) && !string.IsNullOrWhiteSpace(cells[tmpDataRow + OffsetRow, 9 - 1].StringValue.Trim()))
                                {
                                    PurchaseIPOInfo onePurchaseIPO1 = new PurchaseIPOInfo();
                                    onePurchaseIPO1.Id = tmpOrgNameCount;
                                    onePurchaseIPO1.OrgName = tmpOrgName;
                                    onePurchaseIPO1.UserName = tmpUserName;
                                    onePurchaseIPO1.Telephone = tmpTelephone;
                                    onePurchaseIPO1.iPhone = tmpiPhone;
                                    onePurchaseIPO1.EMail = tmpEMail;
                                    onePurchaseIPO1.StockholderName = cells[tmpDataRow + OffsetRow, 2 - 1].StringValue.Trim();
                                    onePurchaseIPO1.StockholderCode = cells[tmpDataRow + OffsetRow, 3 - 1].StringValue.Trim();
                                    onePurchaseIPO1.Seat = cells[tmpDataRow + OffsetRow, 4 - 1].StringValue.Trim();
                                    onePurchaseIPO1.UserID = cells[tmpDataRow + OffsetRow, 5 - 1].StringValue.Trim();
                                    onePurchaseIPO1.Rate = cells[tmpDataRow + OffsetRow, 8 - 1].StringValue.Trim();
                                    onePurchaseIPO1.Money = cells[tmpDataRow + OffsetRow, 9 - 1].StringValue.Trim();
                                    onePurchaseIPO1.RefundBank = cells[tmpDataRow + OffsetRow, 12 - 1].StringValue.Trim();
                                    onePurchaseIPO1.RefundName = cells[tmpDataRow + OffsetRow, 14 - 1].StringValue.Trim();
                                    onePurchaseIPO1.RefundCard = cells[tmpDataRow + OffsetRow, 13 - 1].StringValue.Trim();
                                    onePurchaseIPO1.RefundSystemNo = cells[tmpDataRow + OffsetRow, 16 - 1].StringValue.Trim();
                                    onePurchaseIPO1.RefundProvince = cells[tmpDataRow + OffsetRow, 15 - 1].StringValue.Trim();
                                    purchaseIPOlst.Add(onePurchaseIPO1);
                                }
                                if (!string.IsNullOrWhiteSpace(cells[tmpDataRow + OffsetRow, 10 - 1].StringValue.Trim()) && !string.IsNullOrWhiteSpace(cells[tmpDataRow + OffsetRow, 11 - 1].StringValue.Trim()))
                                {
                                    PurchaseIPOInfo onePurchaseIPO2 = new PurchaseIPOInfo();
                                    onePurchaseIPO2.Id = tmpOrgNameCount;
                                    onePurchaseIPO2.OrgName = tmpOrgName;
                                    onePurchaseIPO2.UserName = tmpUserName;
                                    onePurchaseIPO2.Telephone = tmpTelephone;
                                    onePurchaseIPO2.iPhone = tmpiPhone;
                                    onePurchaseIPO2.EMail = tmpEMail;
                                    onePurchaseIPO2.StockholderName = cells[tmpDataRow + OffsetRow, 2 - 1].StringValue.Trim();
                                    onePurchaseIPO2.StockholderCode = cells[tmpDataRow + OffsetRow, 3 - 1].StringValue.Trim();
                                    onePurchaseIPO2.Seat = cells[tmpDataRow + OffsetRow, 4 - 1].StringValue.Trim();
                                    onePurchaseIPO2.UserID = cells[tmpDataRow + OffsetRow, 5 - 1].StringValue.Trim();
                                    onePurchaseIPO2.Rate = cells[tmpDataRow + OffsetRow, 10 - 1].StringValue.Trim();
                                    onePurchaseIPO2.Money = cells[tmpDataRow + OffsetRow, 11 - 1].StringValue.Trim();
                                    onePurchaseIPO2.RefundBank = cells[tmpDataRow + OffsetRow, 12 - 1].StringValue.Trim();
                                    onePurchaseIPO2.RefundName = cells[tmpDataRow + OffsetRow, 14 - 1].StringValue.Trim();
                                    onePurchaseIPO2.RefundCard = cells[tmpDataRow + OffsetRow, 13 - 1].StringValue.Trim();
                                    onePurchaseIPO2.RefundSystemNo = cells[tmpDataRow + OffsetRow, 16 - 1].StringValue.Trim();
                                    onePurchaseIPO2.RefundProvince = cells[tmpDataRow + OffsetRow, 15 - 1].StringValue.Trim();
                                    purchaseIPOlst.Add(onePurchaseIPO2);
                                }
                            }
                            catch (Exception ex)
                            {
                                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, "解析数据失败:" + OutPath + @"\" + DealFolder + @"\" + fileName, typeof(IPOdepthDealfrm));
                                continue;
                            }

                            tmpDataRow++;
                        }// End While

                        for (int j = 0; j < purchaseIPOlst.Count; j++)
                        {
                            try
                            {
                                //sheetwrite.Range[sheetwriteRow, ].Text = lstDeptName[a++];
                                //sheetwrite.Range[sheetwriteRow, iCellcount++].NumberValue = random.Next(1, 100);
                                cellswrite[sheetwriteRow, 1 - 1].PutValue(  "'" + purchaseIPOlst[j].Id);
                                Style style = cells[sheetwriteRow, 1 - 1].GetStyle();
                                style.Borders[Aspose.Cells.BorderType.LeftBorder].LineStyle = Aspose.Cells.CellBorderType.Thin; //应用边界线 左边界线  
                                style.Borders[Aspose.Cells.BorderType.RightBorder].LineStyle = Aspose.Cells.CellBorderType.Thin; //应用边界线 右边界线  
                                style.Borders[Aspose.Cells.BorderType.TopBorder].LineStyle = Aspose.Cells.CellBorderType.Thin; //应用边界线 上边界线  
                                style.Borders[Aspose.Cells.BorderType.BottomBorder].LineStyle = Aspose.Cells.CellBorderType.Thin; //应用边界线 下边界线   
                                style.IsTextWrapped = true; //单元格内容自动换行
                                cells[sheetwriteRow, 1 - 1].SetStyle(style);
                                //cellswrite[sheetwriteRow, 1].PutValue( purchaseIPOlst[j].Id);
                                cellswrite[sheetwriteRow, 2 - 1].PutValue(purchaseIPOlst[j].OrgName);
                                cells[sheetwriteRow, 2 - 1].SetStyle(style);
                                cellswrite[sheetwriteRow, 3 - 1].PutValue(purchaseIPOlst[j].UserName);
                                cells[sheetwriteRow, 3 - 1].SetStyle(style);
                                cellswrite[sheetwriteRow, 4 - 1].PutValue(purchaseIPOlst[j].Telephone);
                                cells[sheetwriteRow, 4 - 1].SetStyle(style);
                                cellswrite[sheetwriteRow, 5 - 1].PutValue(purchaseIPOlst[j].iPhone);
                                cells[sheetwriteRow, 5 - 1].SetStyle(style);
                                cellswrite[sheetwriteRow, 6 - 1].PutValue(purchaseIPOlst[j].EMail);
                                cells[sheetwriteRow, 6 - 1].SetStyle(style);
                                cellswrite[sheetwriteRow, 7 - 1].PutValue(purchaseIPOlst[j].StockholderName);
                                cells[sheetwriteRow, 7 - 1].SetStyle(style);
                                cellswrite[sheetwriteRow, 8- 1].PutValue( purchaseIPOlst[j].StockholderCode);
                                cells[sheetwriteRow, 8 - 1].SetStyle(style);
                                cellswrite[sheetwriteRow, 9 - 1].PutValue(purchaseIPOlst[j].Seat);
                                cells[sheetwriteRow, 9 - 1].SetStyle(style);
                                cellswrite[sheetwriteRow, 10 - 1].PutValue(purchaseIPOlst[j].UserID);
                                cells[sheetwriteRow, 10 - 1].SetStyle(style);
                                cellswrite[sheetwriteRow, 11 - 1].PutValue(purchaseIPOlst[j].Rate);
                                cells[sheetwriteRow, 11 - 1].SetStyle(style);
                                cellswrite[sheetwriteRow, 11 - 1].PutValue(Convert.ToDouble(purchaseIPOlst[j].Rate));
                                cells[sheetwriteRow, 11 - 1].SetStyle(style);
                                cellswrite[sheetwriteRow, 12 - 1].PutValue(purchaseIPOlst[j].Money);
                                cells[sheetwriteRow, 12 - 1].SetStyle(style);
                                cellswrite[sheetwriteRow, 12 - 1].PutValue(Convert.ToDouble(purchaseIPOlst[j].Money));
                                cells[sheetwriteRow, 12 - 1].SetStyle(style);
                                cellswrite[sheetwriteRow, 13 - 1].PutValue(purchaseIPOlst[j].RefundBank);
                                cells[sheetwriteRow, 13 - 1].SetStyle(style);
                                cellswrite[sheetwriteRow, 14 - 1].PutValue(purchaseIPOlst[j].RefundName);
                                cells[sheetwriteRow, 14 - 1].SetStyle(style);
                                cellswrite[sheetwriteRow, 15 - 1].PutValue(purchaseIPOlst[j].RefundCard);
                                cells[sheetwriteRow, 15 - 1].SetStyle(style);
                                cellswrite[sheetwriteRow, 16 - 1].PutValue(purchaseIPOlst[j].RefundSystemNo);
                                cells[sheetwriteRow, 16 - 1].SetStyle(style);
                                cellswrite[sheetwriteRow, 17 - 1].PutValue(purchaseIPOlst[j].RefundProvince);
                                cells[sheetwriteRow, 17 - 1].SetStyle(style);

                                sheetwriteRow++;
                            }
                            catch (Exception ex) {
                                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, "插入数据失败:" + OutPath + @"\" + DealFolder + @"\" + fileName, typeof(IPOdepthDealfrm));
                                continue;
                            }
                           
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(IPOdepthDealfrm));
                }

                LogHelper.WriteLog(LogLevel.LOG_LEVEL_INFO, "执行结束", typeof(IPOdepthDealfrm));

                btnUpdate.Invoke((Action<string>)delegate(string a) //在控件对象所在的线程上执行委托
                {
                    btnUpdate.Text = a;
                }, "IPO一键处理");

                workbookwrite.Save(xlsfileName, SaveFormat.Xlsx);
                System.Diagnostics.Process.Start(xlsfileName);

                MessageBox.Show("执行完成");

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
            string OutPath = config.AppConfigGet("OutPath");
            DateTime startDt = Convert.ToDateTime(config.AppConfigGet("StartDateTime"));
            DateTime endDt = Convert.ToDateTime(config.AppConfigGet("EndDateTime"));

            txtpop3Server.Text = OApop3Server;
            txtpop3ServerPort.Text = OApop3ServerPort;
            txtOAuserName.Text = OAUsername;
            txtPwd.Text = OAUserPwd;
            txtSubject.Text = EmailSubject;
            txtPath.Text = OutPath;
            ddpStartDateTime.Value = startDt;
            ddpDateTimeEnd.Value = endDt;
        }

        private void IPOdepthDealfrm_Load(object sender, EventArgs e)
        {
            //ShowInfo();

            

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                FastZip fastZip = new FastZip();
                fastZip.ExtractZip(@"G:\ipo_files\tmp\华创证券有限责任公司-证券-17巨化EB网下认购申请表.zip", @"G:\ipo_files\tmp\111\", "");
                //fastZip.ExtractZip(, @"G:\ipo_files\tmp\111\", "");
                //fastZip.ExtractZip(@"G:\ipo_files\tmp\财通证券资产管理有限公司-其他-17巨化EB网下认购申请表.rar", , "");

                UnRAR(@"G:\ipo_files\tmp\财通证券资产管理有限公司-其他-17巨化EB网下认购申请表.rar", @"G:\ipo_files\tmp\111\");

                using (var tmp = new SevenZipExtractor(@"G:\ipo_files\tmp\财通证券资产管理有限公司-证券-17巨化EB网下认购申请表.7z"))
                {
                    for (var i = 0; i < tmp.ArchiveFileData.Count; i++)
                    {
                        tmp.ExtractFiles(@"G:\ipo_files\tmp\111\", tmp.ArchiveFileData[i].Index);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(ZipUtility));
            }
        }


        /// <summary>  
        /// 解压RAR文件  
        /// </summary>  
        /// <param name="rarFilePath">要解压的文件路径</param>  
        /// <param name="unrarDestPath">解压路径（绝对路径）</param>  
        public static void UnRAR(string rarFilePath, string unrarDestPath)
        {
            string rarexe = ExistsRAR();
            if (String.IsNullOrEmpty(rarexe))
            {
                throw new Exception("未安装WinRAR程序。");
            }
            try
            {
                 //组合出需要shell的完整格式  
                string shellArguments = string.Format("x -o+ \"{0}\" \"{1}\\\"", rarFilePath, unrarDestPath);
 
                //用Process调用  
                using (Process unrar = new Process())
                {
                    ProcessStartInfo startinfo = new ProcessStartInfo();
                    startinfo.FileName = rarexe;
                    startinfo.Arguments = shellArguments;               //设置命令参数  
                    startinfo.WindowStyle = ProcessWindowStyle.Hidden;  //隐藏 WinRAR 窗口  
 
                    unrar.StartInfo = startinfo;
                    unrar.Start();
                    unrar.WaitForExit();//等待解压完成  
 
                    unrar.Close();
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>  
        /// 获取WinRAR.exe路径  
        /// </summary>  
        /// <returns>为空则表示未安装WinRAR</returns>  
        public static string ExistsRAR()
        {
            RegistryKey regkey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\WinRAR.exe");
            //RegistryKey regkey = Registry.ClassesRoot.OpenSubKey(@"Applications\WinRAR.exe\shell\open\command");  
            string strkey = regkey.GetValue("").ToString();
            regkey.Close();
            //return strkey.Substring(1, strkey.Length - 7);  
            return strkey;
        }

    }
}
