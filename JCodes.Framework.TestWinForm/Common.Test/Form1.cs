using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Collections;
using System.Xml;
using System.Collections.Specialized;
using System.Net.Sockets;
using JCodes.Framework.Common;
using JCodes.Framework.Common.Collections;
using JCodes.Framework.Common.Threading;
using JCodes.Framework.Common.Web;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.CommonControl;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.Common.Winform;
using JCodes.Framework.Common.Databases;
using JCodes.Framework.Common.Files;
using JCodes.Framework.CommonControl.Other.Images;
using JCodes.Framework.Common.Encrypt;
using JCodes.Framework.Common.Device;
using JCodes.Framework.Common.Format;
using JCodes.Framework.Common.Office;
using JCodes.Framework.Common.Network;
using JCodes.Framework.Common.Images;
using JCodes.Framework.CommonControl.RSARegistryTool;
using DevExpress.Utils;

namespace TestCommons
{
    public partial class Form1 : Form
    {
        private RegisterHotKeyHelper hotKey1 = new RegisterHotKeyHelper();
        private RegisterHotKeyHelper hotKey2 = new RegisterHotKeyHelper();
        private WaitDialogForm _WaitBeforeLogin = null;

        public Form1(WaitDialogForm WaitBeforeLogin)
        {
            InitializeComponent();

            SetHotKey();

            _WaitBeforeLogin = WaitBeforeLogin;
        }

        private void SetHotKey()
        {
            hotKey1.Keys = Keys.F4;
            hotKey1.ModKey = 0;
            hotKey1.WindowHandle = this.Handle;
            hotKey1.WParam = 10001;
            hotKey1.HotKey += new RegisterHotKeyHelper.HotKeyPass(hotKey1_HotKey);
            hotKey1.StarHotKey();

            hotKey2.Keys = Keys.F5;
            hotKey2.ModKey = MODKEY.MOD_CONTROL;
            hotKey2.WindowHandle = this.Handle;
            hotKey2.WParam = 10002;
            hotKey2.HotKey += new RegisterHotKeyHelper.HotKeyPass(hotKey2_HotKey);
            hotKey2.StarHotKey();
        }

        void hotKey2_HotKey()
        {
            MessageDxUtil.ShowTips("测试热键");
        }

        void hotKey1_HotKey()
        {
            Application.Exit();
        }

        private void btnMessageBox_Click(object sender, EventArgs e)
        {
            MessageDxUtil.ShowTips("提示信息对话框");
            MessageDxUtil.ShowWarning("警告消息提示框");
            MessageDxUtil.ShowError("错误消息提示框");

            MessageDxUtil.ShowYesNoAndTips("提示对话框，有Yes/No按钮");
            MessageDxUtil.ShowYesNoAndWarning("警告对话框，有Yes/No按钮");
            MessageDxUtil.ShowYesNoAndError("错误对话框，有Yes/No按钮");
            MessageDxUtil.ShowYesNoCancelAndTips("提示对话框，有Yes/No/Cancel按钮");

            MessageDxUtil.ConfirmYesNo("确认对话框，有Yes/No对话框");
            MessageDxUtil.ConfirmYesNoCancel("确认对话框，有Yes/No/Cancel对话框");

        }

        private void btnJetAccess_Click(object sender, EventArgs e)
        {
            string fileNoPass = Path.Combine(Path.GetTempPath(), "EmptyNoPass.mdb");
            string filePass = Path.Combine(Path.GetTempPath(), "EmptyWithPass.mdb");

            //创建不带密码的空数据库
            JetAccessUtil.CreateMDB(fileNoPass);
            //创建带密码的空数据库
            JetAccessUtil.CreateMDB(filePass, "wuhuacong@163.com");

            //压缩不带密码的数据库
            JetAccessUtil.CompactMDB(fileNoPass);
            //压缩带密码的数据库
            JetAccessUtil.CompactMDB(filePass, "wuhuacong@163.com");

            // 停顿一秒
            //Thread.Sleep(1000);

            //重新设置数据库的密码
            //JetAccessUtil.SetMDBPassword(filePass, "wuhuacong@163.com", "6966254");
            //列出数据库的表名称
            List<string> tableNameList = JetAccessUtil.ListTables(filePass, "wuhuacong@163.com");
            string strNameList = "";
            foreach (string name in tableNameList)
            {
                strNameList += string.Format(",{0}", name);
            }
            if (!string.IsNullOrEmpty(strNameList))
            {
                MessageDxUtil.ShowTips(strNameList);
            }

            //Process.Start(Path.GetTempPath());
        }

        private void btnOleDbHelper_Click(object sender, EventArgs e)
        {
            string access = @"DB\WinFramework.mdb";
            List<string> tableNameList = JetAccessUtil.ListTables(access, "");
            OleDbHelper helper = new OleDbHelper(access);

            foreach(string tableName in tableNameList)
            {
                string sql = string.Format("Select * from {0} ", tableName);
                DataSet ds = helper.ExecuteDataSet(sql);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    MessageDxUtil.ShowTips(string.Format("tableName:{0} RowCount:{1}", tableName, ds.Tables[0].Rows.Count));
                }
            }

        }

        private void btnCSV_Click(object sender, EventArgs e)
        {
            string access = @"DB\WinFramework.mdb";
            OleDbHelper helper = new OleDbHelper(access);
            string sql = string.Format("Select * from mps_MailDetail ");
            DataTable dt = helper.ExecuteDataSet(sql).Tables[0];

            //导出到CSV文件
            string fileName = Path.Combine(Application.StartupPath, "mps_MailDetail.csv");
            CSVHelper.DataTableToCSV(dt, fileName);

            //从CSV文件导入到DataTable
            DataTable dtNew = CSVHelper.CSVToDataTableByOledb(fileName);
            this.dataGridView1.DataSource = dtNew.DefaultView;
        }

        private void btnFileUtil_Click(object sender, EventArgs e)
        {
            string filePath = "DB\\Test.txt";
            //创建一个文件并添加文本
            FileUtil.AppendText(filePath, "测试内容", Encoding.Default);

            //获取文件编码
            Encoding encode = FileUtil.GetEncoding(filePath);
            string encodename = encode.EncodingName;

            //读取文件内容
            string content = FileUtil.FileToString(filePath);

            //读取文件到内存流
            Stream stream = FileUtil.FileToStream(filePath);
            stream.Close();

            //获取文件创建时间
            DateTime dtCreate =FileUtil.GetFileCreateTime(filePath);

            //设置文件只读
            FileUtil.SetFileReadonly(filePath, true);

        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            string filePath = @"DB\TestUser.xls";
            //获取第一个表名
            string sheetname = ExcelHelper.GetExcelFirstTableName(filePath, ExcelType.Excel2003);//Sheet1$
            
            //列出所有表名称
            List<string> tableList = ExcelHelper.GetExcelTablesName(filePath, ExcelType.Excel2003);
            
            //从Excel转换为DataSet对象集合
            DataSet ds =  ExcelHelper.ExcelToDataSet(filePath, true, ExcelType.Excel2003);

            //列出指定表的列名称
            List<string> columnList = ExcelHelper.GetColumnsList(filePath, ExcelType.Excel2003, "Sheet1$");

            //绑定数据显示
            this.dataGridView1.DataSource = ds.Tables[0].DefaultView;

            //导出DataSet到Excel文件中
            filePath = FileDialogHelper.SaveExcel();

            ExcelHelper.DataSetToExcel(ds, filePath);
            Process.Start(filePath);
        }

        private void btnAnimator_Click(object sender, EventArgs e)
        {
            FrmAnimator dlg = new FrmAnimator();
            FormAnimator animator = new FormAnimator(dlg, AnimationMethod.Centre, 
                AnimationDirection.Left, 1000);
            dlg.Show();            
        }

        private void btnFreezeWindow_Click(object sender, EventArgs e)
        {
            string title = "";
            title = this.Text;
            
            //FreezeWindowsUtil util = new FreezeWindowsUtil(this.Handle);
            //this.Text = "窗体已经冻结，等待3秒后解除";
            //Thread.Sleep(3000);
            //util.Dispose();
            
            uint processId;
            NativeMethods.GetWindowThreadProcessId(this.Handle, out processId);
            FreezeWindowsUtil.FreezeThreads((int)processId);
            this.Text = "窗体已经冻结，等待3秒后解除";
            Thread.Sleep(3000);
            FreezeWindowsUtil.UnfreezeThreads((int)processId);

            this.Text = title;

        }

        private void btnIcon_Click(object sender, EventArgs e)
        {
            ChildWinManagement.PopDialogForm(typeof(FrmIconReader));
        }

        private void btnSnap_Click(object sender, EventArgs e)
        {
            ChildWinManagement.PopDialogForm(typeof(FrmWebPreview));
        }

        private void btnResouceHelper_Click(object sender, EventArgs e)
        {
            ChildWinManagement.PopDialogForm(typeof(FrmImageHelper));
        }

        private void btnPrintForm_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                PrintFormHelper.Print(this.dataGridView1);//打印当前窗体
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(Form1));
                MessageDxUtil.ShowError(ex.Message); 
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            string original = "测试加密字符串";
            Console.WriteLine("original:" + original);

            string encrypt = EncodeHelper.SHA256(original);
            Console.WriteLine("EncodeHelper.SHA256:" + encrypt);

            //DES加解密
            encrypt = EncodeHelper.DesEncrypt(original);
            Console.WriteLine("EncodeHelper.DesEncrypt:" + encrypt);

            string decrypt = EncodeHelper.DesDecrypt(encrypt);
            Console.WriteLine("EncodeHelper.DesDecrypt:" + decrypt);

            //MD5加密
            encrypt = EncodeHelper.MD5Encrypt(original);
            Console.WriteLine("EncodeHelper.MD5Encrypt:" + encrypt);
            encrypt = EncodeHelper.MD5EncryptHash(original);
            Console.WriteLine("EncodeHelper.MD5EncryptHash:" + encrypt);
            encrypt = EncodeHelper.MD5EncryptHashHex(original);
            Console.WriteLine("EncodeHelper.MD5EncryptHashHex:" + encrypt);
            encrypt = EncodeHelper.EncyptMD5_3_16(original);
            Console.WriteLine("EncodeHelper.EncyptMD5_3_16:" + encrypt);

            //Base64加解密
            encrypt = EncodeHelper.Base64Encrypt(original);
            Console.WriteLine("EncodeHelper.Base64Encrypt:" + encrypt);
            decrypt = EncodeHelper.Base64Decrypt(encrypt);
            Console.WriteLine("EncodeHelper.Base64Encrypt:" + decrypt);

            encrypt = EncodeHelper.AES_Encrypt(original);
            Console.WriteLine("EncodeHelper.AES_Encrypt:" + encrypt);
            decrypt = EncodeHelper.AES_Decrypt(encrypt);
            Console.WriteLine("EncodeHelper.AES_Decrypt:" + decrypt);

            //MD5加密字符串然后检查是否被篡改
            encrypt = MD5Util.GetMD5_32(original);
            bool flag = MD5Util.ValidateValue(encrypt);
            Console.WriteLine("flag: "+flag);

            //为文件增加MD5编码标签，然后验证是否被修改
            string file = @"DB\test1.xls";
            bool flag2 = MD5Util.AddMD5(file);
            Console.WriteLine("flag2: " + flag2);

            //对给定路径的文件进行验证，如果一致返回True，否则返回False
            bool flag3 = MD5Util.CheckMD5(file);
            Console.WriteLine("flag3: " + flag3);

            //生成加解密私钥、公钥
            string publicKey = "";
            string privateKey = "";
            RSASecurityHelper.GenerateRSAKey(out privateKey, out publicKey);

            string originalString = "testdata";
            string encryptedString = RSASecurityHelper.RSAEncrypt(publicKey, originalString);
            string originalString2 = RSASecurityHelper.RSADecrypt(privateKey, encryptedString); 
            if (originalString == originalString2)
            {
                MessageDxUtil.ShowTips("解密完全正确");
            }
            else
            {
                MessageDxUtil.ShowWarning("解密失败");
            }
          
            string regcode = RSASecurityHelper.RSAEncrypSignature(privateKey, originalString);
            bool validated = RSASecurityHelper.Validate(originalString, regcode, publicKey);
            MessageDxUtil.ShowTips(validated ? "验证成功" : "验证失败");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //WindowsExitHelper.Lock();
            CCalendar cal = new CCalendar();
            this.tsslTips.Text = cal.GetDateInfo(System.DateTime.Now).Fullinfo;

            if (RegistryHelper.CheckRegister())
            {
                this.Text = this.Text + "[已注册]";
            }
            else
            {
                this.Text = this.Text + "[未注册]";
            }

            //关闭登录提示画面  
            _WaitBeforeLogin.Invoke((EventHandler)delegate { _WaitBeforeLogin.Close(); }); 
        }

        private void btnPlaySound_Click(object sender, EventArgs e)
        {
            try
            {
                AppConfig config = Cache.Instance["AppConfig"] as AppConfig;
                if (config == null)
                {
                    config = new AppConfig();
                    Cache.Instance["AppConfig"] = config;
                }	
                string SoundFilePath = config.AppConfigGet("SoundFilePath");
                AudioHelper.Play(SoundFilePath);//AudioHelper.Play("ringin.wav");
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(Form1));
                MessageDxUtil.ShowError(ex.Message); 
            }
        }

        private void btnComputer_Click(object sender, EventArgs e)
        {
            string computerName = HardwareInfoHelper.GetComputerName();
            string userName = HardwareInfoHelper.GetUserName();
            string systemType = HardwareInfoHelper.GetSystemType();
            string cpuid = HardwareInfoHelper.GetCPUId();
            string cpuName = HardwareInfoHelper.GetCPUName();
            int cpuUsage = HardwareInfoHelper.GetCpuUsage();
            string diskId = HardwareInfoHelper.GetDiskID();
            string ip = HardwareInfoHelper.GetIPAddress();
            string macAddress = HardwareInfoHelper.GetMacAddress();
            string memery = HardwareInfoHelper.GetTotalPhysicalMemory();

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("ComputerName:\t {0} \r\n", computerName);
            sb.AppendFormat("UserName:\t {0} \r\n", userName);
            sb.AppendFormat("SystemType:\t {0} \r\n", systemType);
            sb.AppendFormat("CPU ID:\t {0} \r\n", cpuid);
            sb.AppendFormat("CPU Name:\t {0} \r\n", cpuName);
            sb.AppendFormat("CPU Usage:\t {0} \r\n", cpuUsage);
            sb.AppendFormat("Disk Id:\t {0} \r\n", diskId);
            sb.AppendFormat("IP:\t {0} \r\n", ip);
            sb.AppendFormat("MacAddress:\t {0} \r\n", macAddress);
            sb.AppendFormat("TotalPhysicalMemory:\t {0} \r\n", memery);
            MessageDxUtil.ShowTips(sb.ToString());

            string identity = FingerprintHelper.Value();
            MessageDxUtil.ShowTips(identity);
        }

        private void btnKeyboadHook_Click(object sender, EventArgs e)
        {
            KeyboardHook.Enable();
            KeyboardHook.Add(Keys.S, new KeyboardHook.KeyPressed(TestKeyboadHook));

            /*bool capsLock = KeyboardHelper.CapsLock;
            bool numLock = KeyboardHelper.NumLock;
            bool scrollLock = KeyboardHelper.ScrollLock;
            KeyboardHelper.SendKeys("{TAB}"); //发送Tab键
            KeyboardHelper.SendKeys("%{F4}"); //发送Alt + F4退出*/
        }

        private bool TestKeyboadHook()
        {
            //仅处理Alt + S 的钩子事件
            if (KeyboardHook.Alt)
            {
                this.Text = DateTime.Now.ToString();
                NativeMethods.BringToFront(this.Handle);
            }
            return true; //如果要被其他程序捕捉，返回True，否则返回False。
        }

        private void btnRemoveKeyboadHook_Click(object sender, EventArgs e)
        {
            KeyboardHook.Remove(Keys.S);
        }

        private void btnMouseHook_Click(object sender, EventArgs e)
        {
            MouseHook.ButtonDown = new MouseHook.MouseButtonHandler(TestMouseHook);
            MouseHook.Scrolled = new MouseHook.MouseScrollHandler(TestMouseScroll);
            MouseHook.Enable();
        }

        private bool TestMouseHook(MouseButtons sender)
        {
            this.Text = string.Format("你单击了鼠标键：{0}", sender);
            return true;
        }

        private bool TestMouseScroll(int delta)
        {
            this.Text = string.Format("你滚动了鼠标值：{0}", delta);
            return true;
        }

        bool isPlay = false;
        private void btnPlayMp3_Click(object sender, EventArgs e)
        {
            if (!isPlay)
            {
                string mp3File = Path.Combine(Application.StartupPath, "Music\\小酒窝.mp3");
                //string waveFile = Path.Combine(Application.StartupPath, "ringin.wav");
                //string midFile = @"C:\Windows\Media\onestop.mid";

                SoundPlayerHelper.Play(mp3File, false);    //播放MP3格式文件
                //SoundPlayerHelper.Play(waveFile, true); //播放WAV格式文件
                //SoundPlayerHelper.Play(midFile, false);  //播放Midi格式文件
                this.btnPlayMp3.Text = "停止播放";
            }
            else
            {
                SoundPlayerHelper.Stop();//停止播放
                //SoundPlayerHelper.Pause();//暂停播放
                this.btnPlayMp3.Text = "播放声音";
            }

            string statu = SoundPlayerHelper.Status;
            this.Text = statu;
            isPlay = !isPlay;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            //设置音量
            float vol = SoundPlayerHelper.VolumePercent;
            SoundPlayerHelper.VolumePercent = trackBar1.Value;
        }

        private void btnTestEnum_Click(object sender, EventArgs e)
        {
            string desc = EnumHelper.GetDescription(typeof(SqlOperator), SqlOperator.Like);
            MessageDxUtil.ShowTips(string.Format("SqlOperator.Like:{0}", desc));

            SortedList list = EnumHelper.GetStatus(typeof(SqlOperator));
            StringBuilder sb = new StringBuilder();
            foreach (int key in list.Keys)
            {
                sb.AppendFormat("key:{0} Value:{1} \r\n", key, list[key]);
            }
            MessageDxUtil.ShowTips(sb.ToString());
        }

        private void btnThread_Click(object sender, EventArgs e)
        {
        }

        private void btnBase64_Click(object sender, EventArgs e)
        {
            string original = "这是一个测试的Base64加密字符串";
            string encrypt = Base64Util.Encrypt(original);
            Console.WriteLine(encrypt);//输出内容：6L*Z5pi_5LiA5Liq5rWL6K*V55qEQmFzZTY05Yqg5b*G5b2X56ym5Liy

            string decrypt = Base64Util.Decrypt(encrypt);
            Debug.Assert(original == decrypt);//验证相等
        }

        private void btnXML_Click(object sender, EventArgs e)
        {
            SearchInfo searchInfo = new SearchInfo();
            searchInfo.FieldName = "TestFeild";
            searchInfo.FieldValue = "TestValue";

            string file = @"C:\searchInfo.xml";
            XmlHelper.XmlSerialize(file, searchInfo, typeof(SearchInfo));

            SearchInfo info2FromXml = XmlHelper.XmlDeserialize(file, typeof(SearchInfo)) as SearchInfo;
            Console.WriteLine("{0} : {0}", info2FromXml.FieldName, info2FromXml.FieldValue);

            //bookstore.xml文件内容
            /*
            <?xml version="1.0" encoding="gb2312"?>
            <bookstore>
            <book genre="fantasy" ISBN="2-3631-4">
                <title>Oberon's Legacy</title>
                <author>Corets, Eva</author>
                <price>5.95</price>
            </book>
            </bookstore>
            */
            file = @"c:\bookstore.xml";
            XmlHelper helper = new XmlHelper(file);
            string value = helper.Read("bookstore/book", "genre");
            Console.WriteLine(value);//fantasy

            value = helper.Read("bookstore/book", "ISBN");
            Console.WriteLine(value);//2-3631-4

            value = helper.GetElementData("bookstore/book", "title");
            Console.WriteLine(value);//Oberon's Legacy   
    
            XmlElement element = helper.GetElement("bookstore/book", "title");
            element.InnerText = "伍华聪";

            DataSet ds = helper.GetData("bookstore/book");
            ds.WriteXml(@"C:\ds.xml");

        }

        private void btnTestCollection_Click(object sender, EventArgs e)
        {
            OrderedDictionary<string, string> syncDict = new OrderedDictionary<string, string>();
            syncDict.Add("A", "testA");
            syncDict.Add("C", "testC");
            syncDict.Add("B", "TestB");

            //通过键访问
            StringBuilder sb = new StringBuilder();
            foreach (string key in syncDict.Keys)
            {
                sb.AppendFormat("{0}:{1}\r\n", key, syncDict[key]);
            }
            sb.AppendLine();

            //通过索引访问
            for (int i = 0; i < syncDict.Keys.Count; i++)
            {
                sb.AppendFormat("{0}:{1}\r\n", i, syncDict[i]);
            }
            MessageDxUtil.ShowTips(sb.ToString());

            //框架内置的排序字典
            SortedDictionary<string, string> sortDict = new SortedDictionary<string, string>();
            sortDict.Add("A", "testA");
            sortDict.Add("C", "testC");
            sortDict.Add("B", "TestB");

            sb = new StringBuilder();
            foreach (string key in sortDict.Keys)
            {
                sb.AppendFormat("{0}:{1}\r\n", key, sortDict[key]);
            }
            MessageDxUtil.ShowTips(sb.ToString());
        }


        private void btnWebFile_Click(object sender, EventArgs e)
        {
            FileServerManage file = new FileServerManage("ftp://codeany:wjm456852@codeany.w253.cndns5.com:21", "", "");
            try
            {
                //上传文件
                bool first = false;
                using (FileStream fs = new FileStream("DB\\Test.txt", FileMode.Open))
                {
                    first = file.UploadFile(fs, "Test.txt");
                }

                //利用子目录上传，需要服务器手动创建目录
                bool second = file.UploadFile("DB\\Test.txt", "logfiles/Test.txt");

                MessageDxUtil.ShowTips(string.Format("第一次上传:{0} 第二次上传{1}", first, second));

                byte[] fileBytes = file.ReadFileBytes("Test.txt");
                if (fileBytes != null)
                {
                    MessageDxUtil.ShowTips(string.Format("File Bytes:{0}", fileBytes.Length));
                }

                //删除文件
                /*first = file.DeleteFile("Test.txt");
                bool third = file.IsFileExist("Bridge/test.txt");
                second = file.DeleteFile("Bridge/test.txt"); 
                MessageUtil.ShowTips(string.Format("删除文件:{0}、{1} 文件存在：{2}", first, second, third));*/
                
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(Form1));
                MessageDxUtil.ShowError(ex.Message); 
            }
        }

        private void btnSingleton_Click(object sender, EventArgs e)
        {
            Singleton<TestSingletonClass>.Instance.ShowMessage();
        }

        SyncList<string> syncList = new SyncList<string>();
        SyncOrderedDictionary<string, string> syncOrderDict = new SyncOrderedDictionary<string, string>();
        private void btnSyncTest_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                Thread thread = new Thread(new ThreadStart(AddSyncDict));
                thread.Start();
                thread.Join();//等待线程完成才返回主线程
            }

            StringBuilder sb = new StringBuilder();
            foreach (string key in syncOrderDict.Keys)
            {
                sb.AppendFormat("Key:{0}   Value:{1} \r\n", key, syncOrderDict[key]);
            }
            MessageDxUtil.ShowTips(sb.ToString());

            sb = new StringBuilder();
            foreach (string item in syncList)
            {
                sb.AppendFormat("item:{0} \r\n", item);
            }
            MessageDxUtil.ShowTips(sb.ToString());
        }

        private void AddSyncDict()
        {
            string key = new Random().Next().ToString();
            if (!syncOrderDict.ContainsKey(key))
            {
                syncOrderDict.Add(key, DateTime.Now.ToString());
            }

            syncList.Add(key);

            Thread.Sleep(100);
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            //冒泡排序法
            int[] list = new int[10] { 0, 1, 2, 3, 4, 9, 8, 7, 6, 5 };
            SortHelper.BubbleSort(list);
            StringBuilder sb = new StringBuilder();
            foreach (int i in list)
            {
                sb.AppendFormat("{0},", i);
            }
            MessageDxUtil.ShowTips(sb.ToString());

            //插入排序法
            list = new int[10] { 0, 1, 2, 3, 4, 9, 8, 7, 6, 5 };
            SortHelper.InsertionSort(list);
            sb = new StringBuilder();
            foreach (int i in list)
            {
                sb.AppendFormat("{0},", i);
            }
            MessageDxUtil.ShowTips(sb.ToString());
        }
                
        private void btnTimer_Click(object sender, EventArgs e)
        {
            JCodes.Framework.Common.Threading.Timer timer = new JCodes.Framework.Common.Threading.Timer(1000, true);
            timer.Elapsed += new EventHandler(timer_Elapsed);
            timer.Start();
        }

        void timer_Elapsed(object sender, EventArgs e)
        {
            if (!this.InvokeRequired)
                return;

            this.Invoke(new MethodInvoker(delegate()
            {
                this.btnTimer.Text = DateTime.Now.ToLongTimeString();
            }));
        }

        private void btnDelegeteHelper_Click(object sender, EventArgs e)
        {
            //无参数的委托
            DelegateHelper.InvokeDelegate(new UpdateTextDelegate(this.UpdateText)); 

            //有参数的委托
            DelegateHelper.InvokeDelegate(new UpdateTextDelegate2(this.UpdateText), 100);

        }
        private delegate void UpdateTextDelegate();
        private delegate void UpdateTextDelegate2(int count);
        private void UpdateText(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Thread.Sleep(100);
            }
        }  
        
        private void UpdateText()
        {
            for (int i = 0; i < 50; i++)
            {
                Thread.Sleep(100);
            }
        }
        
        private void btnThreadPool_Click(object sender, EventArgs e)
        {
            ThreadPoolHelper.QueueUserWorkItem(new ThreadPoolHelper.WaitCallbackNew(UpdateText));
            ThreadPoolHelper.WaitAny();
        }

        private void btnQueneServer_Click(object sender, EventArgs e)
        {
            QueueServer<PreDataInfo> queueServer = new QueueServer<PreDataInfo>();
            queueServer.IsBackground = true;
            queueServer.ProcessItem += new Action<PreDataInfo>(queueServer_ProcessItem);

            //循环入队
            for (int i = 0; i < 100; i++)
            {
                queueServer.EnqueueItem(new PreDataInfo(i.ToString(), DateTime.Now.ToString()));
                Thread.Sleep(10);
            }
        }

        /// <summary>
        /// 处理每个出队的操作
        /// </summary>
        void queueServer_ProcessItem(PreDataInfo obj)
        {
            Console.WriteLine("{0} : {1}", obj.Key, obj.Data);
        }

        private void btnAbortableThreadPool_Click(object sender, EventArgs e)
        {
            Console.WriteLine(string.Format("QueueCount:{0},WorkingCount:{1}", AbortableThreadPool.QueueCount, AbortableThreadPool.WorkingCount));
            
            WorkItem workItem1 = AbortableThreadPool.QueueUserWorkItem(new WaitCallback(Test));
            Console.WriteLine(string.Format("QueueCount:{0},WorkingCount:{1}", AbortableThreadPool.QueueCount, AbortableThreadPool.WorkingCount));
            
            WorkItem workItem2 = AbortableThreadPool.QueueUserWorkItem(new WaitCallback(Test));
            WorkItem workItem3 = AbortableThreadPool.QueueUserWorkItem(new WaitCallback(Test));
            WorkItem workItem4 = AbortableThreadPool.QueueUserWorkItem(new WaitCallback(Test));
            WorkItem workItem5 = AbortableThreadPool.QueueUserWorkItem(new WaitCallback(Test));
            Console.WriteLine(string.Format("QueueCount:{0},WorkingCount:{1}", AbortableThreadPool.QueueCount, AbortableThreadPool.WorkingCount));
            Thread.Sleep(1000);
            
            Console.WriteLine(AbortableThreadPool.Cancel(workItem1, false));
            Console.WriteLine(string.Format("QueueCount:{0},WorkingCount:{1}", AbortableThreadPool.QueueCount, AbortableThreadPool.WorkingCount));
            Thread.Sleep(1000);
            
            Console.WriteLine(AbortableThreadPool.Cancel(workItem1, true));
            Console.WriteLine(string.Format("QueueCount:{0},WorkingCount:{1}", AbortableThreadPool.QueueCount, AbortableThreadPool.WorkingCount));
            Thread.Sleep(1000);
            
            //AbortableThreadPool.CancelAll(true);//可取消所有任务
            AbortableThreadPool.Join(); //等待所有任务退出
            Console.WriteLine(string.Format("QueueCount:{0},WorkingCount:{1}", AbortableThreadPool.QueueCount, AbortableThreadPool.WorkingCount));
        }

        static void Test(object state)
        {
            int i = 100;
            while (i-- > 0)
            {
                Console.WriteLine(i);
                Thread.Sleep(new Random((int)DateTime.Now.Ticks).Next(100));
            }
        }

        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            EmailHelper email = new EmailHelper("smtp.163.com", "codeany@163.com", "wjm456852");
            email.Subject = "伍华聪的普通测试邮件";
            email.Body = string.Format("测试邮件正文内容");
            email.IsHtml = true;
            email.From = "codeany@163.com";
            email.FromName = "Jimmy";
            email.AddRecipient("371002515@qq.com");
            try
            {
                bool success = email.SendEmail();
                MessageDxUtil.ShowTips(success ? "发送成功" : "发送失败");
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(Form1));
                MessageDxUtil.ShowError(ex.Message); 
            }

            return;
            email = new EmailHelper("smtp.163.com", "wuhuacong2013@163.com", "123abc");
            email.Subject = "伍华聪的图片附件测试邮件";
            string embedFile = Path.Combine(Application.StartupPath, "cityroad.jpg");
            email.Body = string.Format("测试邮件正文内容<img src=\"{0}\" title='测试图片' /> <IMG src=\"C:\\Example.jpg\"> ", embedFile);
            email.IsHtml = true;
            email.From = "wuhuacong2013@163.com";
            email.FromName = "wuhuacong2013";
            email.AddRecipient("6966254@qq.com");
            email.AddAttachment(Path.Combine(Application.StartupPath, "ringin.wav"));//.AddAttachment("C:\\test.txt");

            try
            {
                bool success = email.SendEmail();
                MessageDxUtil.ShowTips(success ? "发送成功" : "发送失败"); 
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(Form1));
                MessageDxUtil.ShowError(ex.Message); 
            }
        }

        private void btnNetWork_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("默认网卡地址:{0} \r\n", NetworkUtil.GetMacAddress2());
            sb.AppendFormat("本机IP:{0} \r\n", NetworkUtil.GetLocalIP());
            sb.AppendFormat("检测本机是否联网:{0} \r\n", NetworkUtil.IsConnectedInternet());
            sb.AppendFormat("www.iqid.com域名IP:{0} \r\n", NetworkUtil.ConvertDnsToIp("www.iqidi.com"));
            sb.AppendFormat("本机LocalHostName:{0}  \r\n", NetworkUtil.LocalHostName);
            sb.AppendFormat("本机局域网IP:{0}  \r\n", NetworkUtil.LANIP);
            sb.AppendFormat("本机广域网IP:{0}  \r\n", NetworkUtil.WANIP);

            sb.AppendLine();
            List<KeyValuePair<string, string>> netCardList = NetworkUtil.GetNetCardList();
            List<KeyValuePair<string, string>>.Enumerator enumNetCard = netCardList.GetEnumerator();
            List<string> macAddrs = new List<string>();
            while (enumNetCard.MoveNext())
            {
                KeyValuePair<string, string> p = enumNetCard.Current;
                string macAddr = NetworkUtil.GetPhysicalAddr(p.Key);
                if (macAddr != string.Empty)
                {
                    sb.AppendFormat("网卡[{0}]的真实地址：{1}",p.Value, macAddr);
                }
            }

            //Socket socket = NetworkUtil.CreateTcpSocket();
            //Socket udpsocket = NetworkUtil.CreateUdpSocket();
            //TcpListener listen = NetworkUtil.CreateTcpListener("127.0.0.1", 9900);
            //listen.Start(100);

            MessageDxUtil.ShowTips(sb.ToString());
        }

        private void btnValidateUtil_Click(object sender, EventArgs e)
        {
            bool isMoble = ValidateUtil.IsValidMobile("18620292076");
            bool isPhoneMoble = ValidateUtil.IsValidPhoneAndMobile("18620292076");
            bool isEmail = ValidateUtil.IsEmail("wuhuacong@163.com");
            bool result = isMoble && isPhoneMoble && isEmail;
            MessageDxUtil.ShowTips(result.ToString());
        }

        private void btnPinyinUtil_Click(object sender, EventArgs e)
        {
            string[] maxims = new string[]{
        "事常与人违，事总在人为",
        "骏马是跑出来的，强兵是打出来的",
        "驾驭命运的舵是奋斗。不抱有一丝幻想，不放弃一点机会，不停止一日努力。 ",
        "如果惧怕前面跌宕的山岩，生命就永远只能是死水一潭", 
        "懦弱的人只会裹足不前，莽撞的人只能引为烧身，只有真正勇敢的人才能所向披靡"
      };

            string[] medicines = new string[] {
        "聚维酮碘溶液",
        "开塞露",
        "炉甘石洗剂",
        "苯扎氯铵贴",
        "鱼石脂软膏",
        "莫匹罗星软膏",
        "红霉素软膏",
        "氢化可的松软膏",
        "曲安奈德软膏",
        "丁苯羟酸乳膏",
        "双氯芬酸二乙胺乳膏",
        "冻疮膏",
        "克霉唑软膏",
        "特比奈芬软膏",
        "酞丁安软膏",
        "咪康唑软膏、栓剂",
        "甲硝唑栓",
        "复方莪术油栓",
        "右桥小脑角岩斜区肿瘤"
      };

            StringBuilder sb = new StringBuilder("UTF8句子拼音：");
            foreach (string s in maxims)
            {
                sb.AppendFormat("汉字：{0}\n拼音：{1}\n简码:{2} \r\n", s, Pinyin.GetPinyin(s), Pinyin.GetFirstPY(s));
            }

            Encoding gb2312 = Encoding.GetEncoding("GB2312");
            sb.AppendFormat("GB2312拼音简码：");
            foreach (string m in medicines)
            {
                string s = Pinyin.ConvertEncoding(m, Encoding.UTF8, gb2312);
                sb.AppendFormat("药品：{0}\n简码：{1}\n", s, Pinyin.GetFirstPY(s, gb2312));
            }

            string testWord = "右桥小脑角岩斜区肿瘤,聚维酮碘溶液,酞丁安软膏";
            //中文拼音(首字母大写，空格分开）
            string letters = PinYinUtil.CHSToPinyin(testWord, " ", true); 

            sb.AppendLine(testWord);
            sb.AppendLine("中文拼音:" + letters);
            sb.AppendLine("首字母:" + PinYinUtil.GetFirstPY(testWord));

            MessageDxUtil.ShowTips(sb.ToString());
        }

        private void btnRMBUtil_Click(object sender, EventArgs e)
        {
            string num1 = "123.45";
            string result1 = RMBUtil.ToRMB(num1);
            string result2 = RMBUtil.ToRMB(123.45M);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(num1);
            sb.AppendLine();
            sb.AppendLine("RMB转换1:" + result1);
            sb.AppendLine("RMB转换2:" + result2);
            MessageDxUtil.ShowTips(sb.ToString());
        }

        private void btnCheckCode_Click(object sender, EventArgs e)
        {
            string filePath = Path.Combine(Application.StartupPath, "DB\\GenerateCheckCode.png");
            Image  image = Bitmap.FromFile(filePath);
            Bitmap bitmap = (Bitmap)image;
            try
            {
                string result = CheckCode.Read(bitmap);
                MessageDxUtil.ShowTips(result); //好像变化了，不可以了。
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(Form1));
                MessageDxUtil.ShowError(ex.Message); 
            }
        }

        private void btnResourceHelper_Click(object sender, EventArgs e)
        {
            //嵌入文件的路径格式为：程序集名称.目录名称.文件名称
            string embedFile = "TestCommons.Resources.book.png";
            Bitmap image = ResourceHelper.LoadBitmap(this.GetType(), embedFile);

            string embedIcon = "TestCommons.Resources.dollar.ico";
            Icon icon = ResourceHelper.LoadIcon(this.GetType(), embedIcon);

            PictureBox pic = new PictureBox();
            pic.Image = image;
            pic.Dock = DockStyle.Fill;

            Form frm = new Form();
            frm.Text = "测试获取嵌入资源";
            frm.Size = new Size(200, 300);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Controls.Add(pic);
            frm.Icon = icon;//动态指定图标
            frm.Show();
        }

        private void btnSmtpMail_Click(object sender, EventArgs e)
        {
            SmtpMail mail = new SmtpMail();
            ArrayList list = mail.ReceiveMail("codeany@163.com", "wjm4568521");

            string content = mail.ReadEmail("1");
            MessageDxUtil.ShowTips(content);
        }

        private void btnRegistryHelper_Click(object sender, EventArgs e)
        {
            string result = string.Empty;
            result += "使用RegistryHelper注册表访问辅助类：" + "\r\n";

            string softwareKey = @"Software\DeepLand\OrderWater";
            bool sucess = RegistryHelper.SaveValue(softwareKey, "Test", DateTime.Now.ToString());
            if (sucess)
            {
                result += RegistryHelper.GetValue(softwareKey, "Test") + "\r\n";
            }

            RegistryHelper.SaveValue(softwareKey, "Test", "测试内容", Microsoft.Win32.Registry.LocalMachine);
            result += RegistryHelper.GetValue(softwareKey, "Test", Microsoft.Win32.Registry.LocalMachine);

            MessageDxUtil.ShowTips(result);
        }

        private void btnGZipUtil_Click(object sender, EventArgs e)
        {
            //压缩解压缩文本内容
            string zippedContent = GZipUtil.Compress("wuhuacong");
            string original = GZipUtil.Decompress(zippedContent);

            GZipUtil.Compress(Application.StartupPath, Application.StartupPath, "cityroad.zip");
            GZipUtil.Decompress(Application.StartupPath, Path.Combine(Application.StartupPath, "cityroad"), "cityroad.zip");

            MessageDxUtil.ShowTips("操作完成");
        }

        private void btnUnicodeHelper_Click(object sender, EventArgs e)
        {
            string str = "\u821e\u7fbd\u6e05\u548c \u5c71\u7f8a\u4e4b\u89d2";
            string test = UnicodeHelper.UnicodeToString(str);
            string result = test + "\r\n";
            result += UnicodeHelper.StringToUnicode(test) + "\r\n";

            MessageDxUtil.ShowTips(result);
        }

        private void btnRTFUtil_Click(object sender, EventArgs e)
        {
            ChildWinManagement.PopDialogForm(typeof(FrmTestRTF));
        }

        /// <summary>
        /// 公钥秘钥
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnpublicsecurt_Click(object sender, EventArgs e)
        {
            ChildWinManagement.PopDialogForm(typeof(FrmKeyPair));
        }

        /// <summary>
        /// 注册码生成器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRegeditTool_Click(object sender, EventArgs e)
        {
            ChildWinManagement.PopDialogForm(typeof(FrmRegeditTool));
        }

        /// <summary>
        /// 自动更新文件生成
        /// 处理逻辑 先根据应用主程序的路径遍历全部的路径 （除Log之外）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAutoUpdateConfig_Click(object sender, EventArgs e)
        {
            ChildWinManagement.PopDialogForm(typeof(GenerateUpdateConfig));
        }

        private void btnWait_Click(object sender, EventArgs e)
        {
            WaitDialogForm WaitBeforeLogin = null; 
            new Thread((ThreadStart)delegate  
            {  
                WaitBeforeLogin = new DevExpress.Utils.WaitDialogForm("请稍候...", "正在加载应用系统");
                Thread.Sleep(3000);
                Console.WriteLine("1:"+DateTime.Now);
            }).Start();
            Console.WriteLine("2:" + DateTime.Now);
            Console.WriteLine("3:" + DateTime.Now);
            ChildWinManagement.PopDialogForm(typeof(FrmRegeditTool));
           
            //关闭登录提示画面  
            WaitBeforeLogin.Invoke((EventHandler)delegate { WaitBeforeLogin.Close(); }); 
            
        }
    }

    public class PreDataInfo
    {
        public string Key;
        public string Data;

        public PreDataInfo()
        {
        }

        public PreDataInfo(string key, string data)
        {
            this.Key = key;
            this.Data = data;
        }
    }

}
