using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using JCodes.Framework.jCodesenum.BaseEnum;

namespace JCodes.Framework.Common.Network
{
    /// <summary>
    /// FTP操作辅助类
    /// </summary>
    public class FTPHelper
    {
        #region 公共变量

        /// <summary>
        /// IP地址或者主机名
        /// </summary>
        public string server;

        /// <summary>
        /// 登录用户名
        /// </summary>
        public string user;

        /// <summary>
        /// 登录了密码
        /// </summary>
        public string pass;

        /// <summary>
        /// FTP服务器端口
        /// </summary>
        public int port;

        /// <summary>
        /// 数据等待超时时间（毫秒）
        /// </summary>
        public int timeout;

        #endregion

        #region Private Variables

        private string messages; // server messages
        private string responseStr; // server response if the user wants it.
        private bool passive_mode;		// #######################################
        private long bytes_total; // upload/download info if the user wants it.
        private long file_size; // gets set when an upload or download takes place
        private Socket main_sock;
        private IPEndPoint main_ipEndPoint;
        private Socket listening_sock;
        private Socket data_sock;
        private IPEndPoint data_ipEndPoint;
        private FileStream file;
        private int response;
        private string bucket;

        #endregion

        #region Constructors
        /// <summary>
        /// 构造函数
        /// </summary>
        public FTPHelper()
        {
            server = null;
            user = null;
            pass = null;
            port = 21;
            passive_mode = true;		// #######################################
            main_sock = null;
            main_ipEndPoint = null;
            listening_sock = null;
            data_sock = null;
            data_ipEndPoint = null;
            file = null;
            bucket = "";
            bytes_total = 0;
            timeout = 10000;	// 10 seconds
            messages = "";
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="server">待连接的服务器</param>
        /// <param name="user">登录用户名</param>
        /// <param name="pass">登录密码</param>
        public FTPHelper(string server, string user, string pass)
        {
            this.server = server;
            this.user = user;
            this.pass = pass;
            port = 21;
            passive_mode = true;		// #######################################
            main_sock = null;
            main_ipEndPoint = null;
            listening_sock = null;
            data_sock = null;
            data_ipEndPoint = null;
            file = null;
            bucket = "";
            bytes_total = 0;
            timeout = 10000;	// 10 seconds
            messages = "";
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="server">待连接的FTP服务器</param>
        /// <param name="port">FTP服务器端口</param>
        /// <param name="user">登录用户名</param>
        /// <param name="pass">登录密码</param>
        public FTPHelper(string server, int port, string user, string pass)
        {
            this.server = server;
            this.user = user;
            this.pass = pass;
            this.port = port;
            passive_mode = true;		// #######################################
            main_sock = null;
            main_ipEndPoint = null;
            listening_sock = null;
            data_sock = null;
            data_ipEndPoint = null;
            file = null;
            bucket = "";
            bytes_total = 0;
            timeout = 10000;	// 10 seconds
            messages = "";
        }

        #endregion

        /// <summary>
        /// 服务器连接状态
        /// </summary>
        public bool IsConnected
        {
            get
            {
                if (main_sock != null)
                    return main_sock.Connected;
                return false;
            }
        }
        /// <summary>
        /// 判断消息缓冲是否有数据存在
        /// </summary>
        public bool MessagesAvailable
        {
            get
            {
                if (messages.Length > 0)
                    return true;
                return false;
            }
        }

        /// <summary>
        /// 服务器消息，访问该属性后值被清空
        /// </summary>
        public string Messages
        {
            get
            {
                string tmp = messages;
                messages = "";
                return tmp;
            }
        }

        /// <summary>
        /// 最后命令的应答内容
        /// </summary>
        public string ResponseString
        {
            get
            {
                return responseStr;
            }
        }

        /// <summary>
        /// 发送或者接收的数据总数
        /// </summary>
        public long BytesTotal	
        {
            get
            {
                return bytes_total;
            }
        }

        /// <summary>
        /// 上传或下载的文件大小 (如果没有数据指定，可能为0)
        /// </summary>
        public long FileSize
        {
            get
            {
                return file_size;
            }
        }

        /// <summary>
        /// True:  Passive mode [default]
        /// False: Active Mode
        /// </summary>
        public bool PassiveMode	
        {
            get
            {
                return passive_mode;
            }
            set
            {
                passive_mode = value;
            }
        }

        private void Fail()
        {
            Disconnect();
            throw new Exception(responseStr);
        }

        private void SetBinaryMode(bool mode)
        {
            if (mode)
                SendCommand("TYPE I");
            else
                SendCommand("TYPE A");

            ReadResponse();
            if (response != 200)
                Fail();
        }

        private void SendCommand(string command)
        {
            Byte[] cmd = Encoding.ASCII.GetBytes((command + "\r\n").ToCharArray());

#if (FTP_DEBUG)
			if (command.Length > 3 && command.Substring(0, 4) == "PASS")
				Console.WriteLine("\rPASS xxx");
			else
				Console.WriteLine("\r" + command);
#endif

            main_sock.Send(cmd, cmd.Length, 0);
        }
        
        private void FillBucket()
        {
            Byte[] bytes = new Byte[512];
            long bytesgot;
            int msecs_passed = 0;		// #######################################

            while (main_sock.Available < 1)
            {
                System.Threading.Thread.Sleep(50);
                msecs_passed += 50;
                // this code is just a fail safe option 
                // so the code doesn't hang if there is 
                // no data comming.
                if (msecs_passed > timeout)
                {
                    Disconnect();
                    throw new Exception("Timed out waiting on server to respond.");
                }
            }

            while (main_sock.Available > 0)
            {
                bytesgot = main_sock.Receive(bytes, 512, 0);
                bucket += Encoding.ASCII.GetString(bytes, 0, (int)bytesgot);
                // this may not be needed, gives any more data that hasn't arrived
                // just yet a small chance to get there.
                System.Threading.Thread.Sleep(50);
            }
        }

        private string GetLineFromBucket()
        {
            int i;
            string buf = "";

            if ((i = bucket.IndexOf('\n')) < 0)
            {
                while (i < 0)
                {
                    FillBucket();
                    i = bucket.IndexOf('\n');
                }
            }

            buf = bucket.Substring(0, i);
            bucket = bucket.Substring(i + 1);

            return buf;
        }

        // Any time a command is sent, use ReadResponse() to get the response
        // from the server. The variable responseStr holds the entire string and
        // the variable response holds the response number.
        private void ReadResponse()
        {
            string buf;
            messages = "";

            while (true)
            {
                //buf = GetLineFromBucket();
                buf = GetLineFromBucket();

#if (FTP_DEBUG)
				Console.WriteLine(buf);
#endif
                // the server will respond with "000-Foo bar" on multi line responses
                // "000 Foo bar" would be the last line it sent for that response.
                // Better example:
                // "000-This is a multiline response"
                // "000-Foo bar"
                // "000 This is the end of the response"
                if (Regex.Match(buf, "^[0-9]+ ").Success)
                {
                    responseStr = buf;
                    response = int.Parse(buf.Substring(0, 3));
                    break;
                }
                else
                    messages += Regex.Replace(buf, "^[0-9]+-", "") + "\n";
            }
        }

        // if you add code that needs a data socket, i.e. a PASV or PORT command required,
        // call this function to do the dirty work. It sends the PASV or PORT command,
        // parses out the port and ip info and opens the appropriate data socket
        // for you. The socket variable is private Socket data_socket. Once you
        // are done with it, be sure to call CloseDataSocket()
        private void OpenDataSocket()
        {
            if (passive_mode)		// #######################################
            {
                string[] pasv;
                string server;
                int port;

                Connect();
                SendCommand("PASV");
                ReadResponse();
                if (response != 227)
                    Fail();

                try
                {
                    int i1, i2;

                    i1 = responseStr.IndexOf('(') + 1;
                    i2 = responseStr.IndexOf(')') - i1;
                    pasv = responseStr.Substring(i1, i2).Split(',');
                }
                catch (Exception ex)
                {
                    Disconnect();
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FTPHelper));
                    throw new Exception("Malformed PASV response: " + responseStr);
                }

                if (pasv.Length < 6)
                {
                    Disconnect();
                    throw new Exception("Malformed PASV response: " + responseStr);
                }

                server = String.Format("{0}.{1}.{2}.{3}", pasv[0], pasv[1], pasv[2], pasv[3]);
                port = (int.Parse(pasv[4]) << 8) + int.Parse(pasv[5]);

                try
                {
#if (FTP_DEBUG)
					Console.WriteLine("Data socket: {0}:{1}", server, port);
#endif
                    CloseDataSocket();

#if (FTP_DEBUG)
					Console.WriteLine("Creating socket...");
#endif
                    data_sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

#if (FTP_DEBUG)
					Console.WriteLine("Resolving host");
#endif

                    data_ipEndPoint = new IPEndPoint(Dns.GetHostByName(server).AddressList[0], port);


#if (FTP_DEBUG)
					Console.WriteLine("Connecting..");
#endif
                    data_sock.Connect(data_ipEndPoint);

#if (FTP_DEBUG)
					Console.WriteLine("Connected.");
#endif
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FTPHelper));
                    throw new Exception("Failed to connect for data transfer: " + ex.Message);
                }
            }
            else		// #######################################
            {
                Connect();

                try
                {
#if (FTP_DEBUG)
					Console.WriteLine("Data socket (active mode)");
#endif
                    CloseDataSocket();

#if (FTP_DEBUG)
					Console.WriteLine("Creating listening socket...");
#endif
                    listening_sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

#if (FTP_DEBUG)
					Console.WriteLine("Binding it to local address/port");
#endif
                    // for the PORT command we need to send our IP address; let's extract it
                    // from the LocalEndPoint of the main socket, that's already connected
                    string sLocAddr = main_sock.LocalEndPoint.ToString();
                    int ix = sLocAddr.IndexOf(':');
                    if (ix < 0)
                    {
                        throw new Exception("Failed to parse the local address: " + sLocAddr);
                    }
                    string sIPAddr = sLocAddr.Substring(0, ix);
                    // let the system automatically assign a port number (setting port = 0)
                    System.Net.IPEndPoint localEP = new IPEndPoint(IPAddress.Parse(sIPAddr), 0);

                    listening_sock.Bind(localEP);
                    sLocAddr = listening_sock.LocalEndPoint.ToString();
                    ix = sLocAddr.IndexOf(':');
                    if (ix < 0)
                    {
                        throw new Exception("Failed to parse the local address: " + sLocAddr);
                    }
                    int nPort = int.Parse(sLocAddr.Substring(ix + 1));
#if (FTP_DEBUG)
					Console.WriteLine("Listening on {0}:{1}", sIPAddr, nPort);
#endif
                    // start to listen for a connection request from the host (note that
                    // Listen is not blocking) and send the PORT command
                    listening_sock.Listen(1);
                    string sPortCmd = string.Format("PORT {0},{1},{2}",
                                                    sIPAddr.Replace('.', ','),
                                                    nPort / 256, nPort % 256);
                    SendCommand(sPortCmd);
                    ReadResponse();
                    if (response != 200)
                        Fail();
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FTPHelper));
                    throw new Exception("Failed to connect for data transfer: " + ex.Message);
                }
            }
        }
        
        private void ConnectDataSocket()
        {
            if (data_sock != null)		// already connected (always so if passive mode)
                return;

            try
            {
#if (FTP_DEBUG)
				Console.WriteLine("Accepting the data connection.");
#endif
                data_sock = listening_sock.Accept();	// Accept is blocking
                listening_sock.Close();
                listening_sock = null;

                if (data_sock == null)
                {
                    throw new Exception("Winsock error: " +
                        Convert.ToString(System.Runtime.InteropServices.Marshal.GetLastWin32Error()));
                }
#if (FTP_DEBUG)
				Console.WriteLine("Connected.");
#endif
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FTPHelper));
                throw new Exception("Failed to connect for data transfer: " + ex.Message);
            }
        }
        
        private void CloseDataSocket()
        {
#if (FTP_DEBUG)
			Console.WriteLine("Attempting to close data channel socket...");
#endif
            if (data_sock != null)
            {
                if (data_sock.Connected)
                {
#if (FTP_DEBUG)
						Console.WriteLine("Closing data channel socket!");
#endif
                    data_sock.Close();
#if (FTP_DEBUG)
						Console.WriteLine("Data channel socket closed!");
#endif
                }
                data_sock = null;
            }

            data_ipEndPoint = null;
        }

        /// <summary>
        /// 关闭FTP服务器的所有连接
        /// </summary>
        public void Disconnect()
        {
            CloseDataSocket();

            if (main_sock != null)
            {
                if (main_sock.Connected)
                {
                    SendCommand("QUIT");
                    main_sock.Close();
                }
                main_sock = null;
            }

            if (file != null)
                file.Close();

            main_ipEndPoint = null;
            file = null;
        }

        /// <summary>
        /// 连接到FTP服务器
        /// </summary>
        /// <param name="server">FTP服务器的IP或者主机名</param>
        /// <param name="port">FTP服务器端口</param>
        /// <param name="user">登录用户名</param>
        /// <param name="pass">登录密码</param>
        public void Connect(string server, int port, string user, string pass)
        {
            this.server = server;
            this.user = user;
            this.pass = pass;
            this.port = port;

            Connect();
        }
        /// <summary>
        /// 连接到FTP服务器
        /// </summary>
        /// <param name="server">FTP服务器的IP或者主机名</param>
        /// <param name="user">登录用户名</param>
        /// <param name="pass">登录密码</param>
        public void Connect(string server, string user, string pass)
        {
            this.server = server;
            this.user = user;
            this.pass = pass;

            Connect();
        }

        /// <summary>
        /// 连接到FTP服务器
        /// </summary>
        public void Connect()
        {
            if (server == null)
                throw new Exception("No server has been set.");
            if (user == null)
                throw new Exception("No username has been set.");

            if (main_sock != null)
                if (main_sock.Connected)
                    return;

            main_sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            main_ipEndPoint = new IPEndPoint(Dns.GetHostByName(server).AddressList[0], port);

            try
            {
                main_sock.Connect(main_ipEndPoint);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FTPHelper));
                throw new Exception(ex.Message);
            }

            ReadResponse();
            if (response != 220)
                Fail();

            SendCommand("USER " + user);
            ReadResponse();

            switch (response)
            {
                case 331:
                    if (pass == null)
                    {
                        Disconnect();
                        throw new Exception("No password has been set.");
                    }
                    SendCommand("PASS " + pass);
                    ReadResponse();
                    if (response != 230)
                        Fail();
                    break;
                case 230:
                    break;
            }

            return;
        }

        /// <summary>
        /// 获取FTP服务器的文件及目录列表
        /// </summary>
        /// <returns>文件列表数组集合</returns>
        public ArrayList List()
        {
            Byte[] bytes = new Byte[512];
            string file_list = "";
            long bytesgot = 0;
            int msecs_passed = 0;
            ArrayList list = new ArrayList();

            Connect();
            OpenDataSocket();
            SendCommand("LIST");
            ReadResponse();

            //FILIPE MADUREIRA.
            //Added response 125
            switch (response)
            {
                case 125:
                case 150:
                    break;
                default:
                    CloseDataSocket();
                    throw new Exception(responseStr);
            }
            ConnectDataSocket();		// #######################################

            while (data_sock.Available < 1)
            {
                System.Threading.Thread.Sleep(50);
                msecs_passed += 50;
                // this code is just a fail safe option 
                // so the code doesn't hang if there is 
                // no data comming.
                if (msecs_passed > (timeout / 10))
                {
                    //CloseDataSocket();
                    //throw new Exception("Timed out waiting on server to respond.");

                    //FILIPE MADUREIRA.
                    //If there are no files to list it gives timeout.
                    //So I wait less time and if no data is received, means that there are no files
                    break;//Maybe there are no files
                }
            }

            while (data_sock.Available > 0)
            {
                bytesgot = data_sock.Receive(bytes, bytes.Length, 0);
                file_list += Encoding.ASCII.GetString(bytes, 0, (int)bytesgot);
                System.Threading.Thread.Sleep(50); // *shrug*, sometimes there is data comming but it isn't there yet.
            }

            CloseDataSocket();

            ReadResponse();
            if (response != 226)
                throw new Exception(responseStr);

            foreach (string f in file_list.Split('\n'))
            {
                if (f.Length > 0 && !Regex.Match(f, "^total").Success)
                    list.Add(f.Substring(0, f.Length - 1));
            }

            return list;
        }

        /// <summary>
        /// 仅获取文件列表
        /// </summary>
        /// <returns>获取的文件列表</returns>
        public ArrayList ListFiles()
        {
            ArrayList list = new ArrayList();
            foreach (string f in List())
            {
                //FILIPE MADUREIRA
                //In Windows servers it is identified by <DIR>
                if ((f.Length > 0))
                {
                    if ((f[0] != 'd') && (f.ToUpper().IndexOf("<DIR>") < 0))
                        list.Add(f);
                }
            }

            return list;
        }

        /// <summary>
        /// 仅获取目录列表
        /// </summary>
        /// <returns>获取到的目录列表</returns>
        public ArrayList ListDirectories()
        {
            ArrayList list = new ArrayList();

            foreach (string f in List())
            {
                //FILIPE MADUREIRA
                //In Windows servers it is identified by <DIR>
                if (f.Length > 0)
                {
                    if ((f[0] == 'd') || (f.ToUpper().IndexOf("<DIR>") >= 0))
                        list.Add(f);
                }
            }

            return list;
        }

        /// <summary>
        /// 返回FTP原始（RAW）的数据信息 (YYYYMMDDhhmmss)，
        /// 使用GetFileDate可返回一个较好的日期对象。
        /// </summary>
        /// <param name="fileName">查询的远端文件名</param>
        /// <returns>返回FTP原始（RAW）的数据信息</returns>
        public string GetFileDateRaw(string fileName)
        {
            Connect();

            SendCommand("MDTM " + fileName);
            ReadResponse();
            if (response != 213)
            {
#if (FTP_DEBUG)
				Console.Write("\r" + responseStr);
#endif
                throw new Exception(responseStr);
            }

            return (this.responseStr.Substring(4));
        }

        /// <summary>
        /// 查询FTP服务器中远程文件的日期
        /// </summary>
        /// <param name="fileName">查询的远端文件名</param>
        /// <returns>文件的日期</returns>
        public DateTime GetFileDate(string fileName)
        {
            return ConvertFTPDateToDateTime(GetFileDateRaw(fileName));
        }

        private DateTime ConvertFTPDateToDateTime(string input)
        {
            if (input.Length < 14)
                throw new ArgumentException("Input Value for ConvertFTPDateToDateTime method was too short.");

            //YYYYMMDDhhmmss": 
            int year = Convert.ToInt16(input.Substring(0, 4));
            int month = Convert.ToInt16(input.Substring(4, 2));
            int day = Convert.ToInt16(input.Substring(6, 2));
            int hour = Convert.ToInt16(input.Substring(8, 2));
            int min = Convert.ToInt16(input.Substring(10, 2));
            int sec = Convert.ToInt16(input.Substring(12, 2));

            return new DateTime(year, month, day, hour, min, sec);
        }

        /// <summary>
        /// 获取FTP的当前工作目录
        /// </summary>
        public string GetWorkingDirectory()
        {
            //PWD - print working directory
            Connect();
            SendCommand("PWD");
            ReadResponse();

            if (response != 257)
                throw new Exception(responseStr);

            string pwd;
            try
            {
                pwd = responseStr.Substring(responseStr.IndexOf("\"", 0) + 1);//5);
                pwd = pwd.Substring(0, pwd.LastIndexOf("\""));
                pwd = pwd.Replace("\"\"", "\""); // directories with quotes in the name come out as "" from the server
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FTPHelper));
                throw new Exception("Uhandled PWD response: " + ex.Message);
            }

            return pwd;
        }

        /// <summary>
        /// 在FTP服务器上改变目录
        /// </summary>
        public void ChangeDir(string path)
        {
            Connect();
            SendCommand("CWD " + path);
            ReadResponse();
            if (response != 250)
            {
#if (FTP_DEBUG)
				Console.Write("\r" + responseStr);
#endif
                throw new Exception(responseStr);
            }
        }

        /// <summary>
        /// 在FTP服务器上创建新的目录
        /// </summary>
        public void MakeDir(string dir)
        {
            Connect();
            SendCommand("MKD " + dir);
            ReadResponse();

            switch (response)
            {
                case 257:
                case 250:
                    break;
                default:
#if (FTP_DEBUG)
                    Console.Write("\r" + responseStr);
#endif
                    throw new Exception(responseStr);
            }
        }

        /// <summary>
        /// 在FTP服务器上移除目录
        /// </summary>
        public void RemoveDir(string dir)
        {
            Connect();
            SendCommand("RMD " + dir);
            ReadResponse();
            if (response != 250)
            {
#if (FTP_DEBUG)
				Console.Write("\r" + responseStr);
#endif
                throw new Exception(responseStr);
            }
        }

        /// <summary>
        /// 在FTP上移除一个文件
        /// </summary>
        public void RemoveFile(string filename)
        {
            Connect();
            SendCommand("DELE " + filename);
            ReadResponse();
            if (response != 250)
            {
#if (FTP_DEBUG)
				Console.Write("\r" + responseStr);
#endif
                throw new Exception(responseStr);
            }
        }

        /// <summary>
        /// 在FTP服务器上重命名一个文件
        /// </summary>
        /// <param name="oldfilename">旧文件名</param>
        /// <param name="newfilename">新文件名</param>
        public void RenameFile(string oldfilename, string newfilename)	
        {
            Connect();
            SendCommand("RNFR " + oldfilename);
            ReadResponse();
            if (response != 350)
            {
#if (FTP_DEBUG)
				Console.Write("\r" + responseStr);
#endif
                throw new Exception(responseStr);
            }
            else
            {
                SendCommand("RNTO " + newfilename);
                ReadResponse();
                if (response != 250)
                {
#if (FTP_DEBUG)
					Console.Write("\r" + responseStr);
#endif
                    throw new Exception(responseStr);
                }
            }
        }

        /// <summary>
        /// 获取文件的大小（需要FTP服务器支持）
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns>指定文件的大小</returns>
        public long GetFileSize(string filename)
        {
            Connect();
            SendCommand("SIZE " + filename);
            ReadResponse();
            if (response != 213)
            {
#if (FTP_DEBUG)
				Console.Write("\r" + responseStr);
#endif
                throw new Exception(responseStr);
            }

            return Int64.Parse(responseStr.Substring(4));
        }

        /// <summary>
        /// 打开一个存在的文件（非续传）上传
        /// </summary>
        /// <param name="filename">待上传的文件</param>
        public void OpenUpload(string filename)
        {
            OpenUpload(filename, filename, false);
        }

        /// <summary>
        /// 打开一个存在的文件（非续传）上传
        /// </summary>
        /// <param name="filename">本地上传的文件（包含文件路径）</param>
        /// <param name="remotefilename">存在在FTP上的文件名</param>
        public void OpenUpload(string filename, string remotefilename)
        {
            OpenUpload(filename, remotefilename, false);
        }

        /// <summary>
        /// 打开一个存在的文件（支持续传）上传
        /// </summary>
        /// <param name="filename">本地上传的文件（包含文件路径）</param>
        /// <param name="resume">如果文件存在，指定是否续传</param>
        public void OpenUpload(string filename, bool resume)
        {
            OpenUpload(filename, filename, resume);
        }

        /// <summary>
        /// 打开一个存在的文件（支持续传）上传
        /// </summary>
        /// <param name="filename">本地上传的文件（包含文件路径）</param>
        /// <param name="remote_filename">存储在FTP上的文件名称</param>
        /// <param name="resume">如果存在，指定是否续传</param>
        public void OpenUpload(string filename, string remote_filename, bool resume)
        {
            Connect();
            SetBinaryMode(true);
            OpenDataSocket();

            bytes_total = 0;

            try
            {
                file = new FileStream(filename, FileMode.Open);
            }
            catch (Exception ex)
            {
                file = null;
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FTPHelper));
                throw new Exception(ex.Message);
            }

            file_size = file.Length;

            if (resume)
            {
                long size = GetFileSize(remote_filename);
                SendCommand("REST " + size);
                ReadResponse();
                if (response == 350)
                    file.Seek(size, SeekOrigin.Begin);
            }

            SendCommand("STOR " + remote_filename);
            ReadResponse();

            switch (response)
            {
                case 125:
                case 150:
                    break;
                default:
                    file.Close();
                    file = null;
                    throw new Exception(responseStr);
            }
            ConnectDataSocket();		// #######################################	

            return;
        }

        /// <summary>
        /// 打开下载一个文件（不支持续传）
        /// </summary>
        /// <param name="filename">FTP上的远端文件名</param>
        public void OpenDownload(string filename)
        {
            OpenDownload(filename, filename, false);
        }

        /// <summary>
        /// 打开下载一个文件（可选是否续传）
        /// </summary>
        /// <param name="filename">FTP上的远端文件名</param>
        /// <param name="resume">如果存在，指定是否续传</param>
        public void OpenDownload(string filename, bool resume)
        {
            OpenDownload(filename, filename, resume);
        }

        /// <summary>
        /// 打开下载一个文件（不支持续传）
        /// </summary>
        /// <param name="filename">FTP上的远端文件名</param>
        /// <param name="localfilename">本地文件名（包含文件路径）</param>
        public void OpenDownload(string filename, string localfilename)
        {
            OpenDownload(filename, localfilename, false);
        }

        /// <summary>
        /// 打开下载一个文件
        /// </summary>
        /// <param name="remote_filename">FTP上的远端文件名</param>
        /// <param name="local_filename">另存为的文件名称(包含文件路径)</param>
        /// <param name="resume">如果存在，指定是否续传</param>
        public void OpenDownload(string remote_filename, string local_filename, bool resume)
        {
            Connect();
            SetBinaryMode(true);

            bytes_total = 0;

            try
            {
                file_size = GetFileSize(remote_filename);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FTPHelper));
                file_size = 0;
            }

            if (resume && File.Exists(local_filename))
            {
                try
                {
                    file = new FileStream(local_filename, FileMode.Open);
                }
                catch (Exception ex)
                {
                    file = null;
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FTPHelper));
                    throw new Exception(ex.Message);
                }

                SendCommand("REST " + file.Length);
                ReadResponse();
                if (response != 350)
                    throw new Exception(responseStr);
                file.Seek(file.Length, SeekOrigin.Begin);
                bytes_total = file.Length;
            }
            else
            {
                try
                {
                    file = new FileStream(local_filename, FileMode.Create);
                }
                catch (Exception ex)
                {
                    file = null;
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FTPHelper));
                    throw new Exception(ex.Message);
                }
            }

            OpenDataSocket();
            SendCommand("RETR " + remote_filename);
            ReadResponse();

            switch (response)
            {
                case 125:
                case 150:
                    break;
                default:
                    file.Close();
                    file = null;
                    throw new Exception(responseStr);
            }
            ConnectDataSocket();		// #######################################	

            return;
        }

        /// <summary>
        /// 上传文件，循环操作直到文件全部上传完毕
        /// </summary>
        /// <returns>发送的字节数组大小</returns>
        public long DoUpload()
        {
            Byte[] bytes = new Byte[512];
            long bytes_got;

            try
            {
                bytes_got = file.Read(bytes, 0, bytes.Length);
                bytes_total += bytes_got;
                data_sock.Send(bytes, (int)bytes_got, 0);

                if (bytes_got <= 0)
                {
                    // the upload is complete or an error occured
                    file.Close();
                    file = null;

                    CloseDataSocket();
                    ReadResponse();
                    switch (response)
                    {
                        case 226:
                        case 250:
                            break;
                        default:
                            throw new Exception(responseStr);
                    }

                    SetBinaryMode(false);
                }
            }
            catch (Exception ex)
            {
                file.Close();
                file = null;
                CloseDataSocket();
                ReadResponse();
                SetBinaryMode(false);
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FTPHelper));
                throw ex;
            }

            return bytes_got;
        }

        /// <summary>
        /// 下载文件，循环操作直到文件全部下载完毕
        /// </summary>
        /// <returns>收到的字节数组大小</returns>
        public long DoDownload()
        {
            Byte[] bytes = new Byte[512];
            long bytes_got;

            try
            {
                bytes_got = data_sock.Receive(bytes, bytes.Length, 0);

                if (bytes_got <= 0)
                {
                    // the download is done or an error occured
                    CloseDataSocket();
                    file.Close();
                    file = null;

                    ReadResponse();
                    switch (response)
                    {
                        case 226:
                        case 250:
                            break;
                        default:
                            throw new Exception(responseStr);
                    }

                    SetBinaryMode(false);

                    return bytes_got;
                }

                file.Write(bytes, 0, (int)bytes_got);
                bytes_total += bytes_got;
            }
            catch (Exception ex)
            {
                CloseDataSocket();
                file.Close();
                file = null;
                ReadResponse();
                SetBinaryMode(false);
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FTPHelper));
                throw ex;
            }

            return bytes_got;
        }
    }
}
