using DevExpress.XtraBars.Alerter;
using DevExpress.XtraEditors;
using JCodes.Framework.BLL;
using JCodes.Framework.Common;
using JCodes.Framework.Common.Databases;
using JCodes.Framework.Common.Files;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Common.Others;
using JCodes.Framework.Common.Threading;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.Entity;
using JCodes.Framework.jCodesenum;
using JCodes.Framework.jCodesenum.BaseEnum;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Speech.Synthesis;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace JCodes.Framework.TestWinForm.ZsDaixiao
{
    public partial class FrmDealConsignment : XtraForm
    {
        private Prompt speekerPrompt = null;

        MyFileSystemWatcher mfsw = null;

        private List<ConsignmentInfo> list = null;

        private SpeechSynthesizer speech = new SpeechSynthesizer();

        private AppConfig app = new AppConfig();

        /// <summary>
        /// 判断是否全部到齐，0未到齐，1-全部到齐，2-到齐已处理，
        /// </summary>
        Int32 isAllArr = 1;

        public FrmDealConsignment()
        {
            InitializeComponent();

            InitData();
        }

        private void InitData() {
            txtTDay.Text = DateTime.Now.ToString("yyyyMMdd");
            txtTDay.Text = DateTime.Now.ToString("yyyyMMdd");
            DayOfWeek dow = DateTime.Now.DayOfWeek;
            if (DayOfWeek.Friday == dow || DayOfWeek.Saturday == dow || DayOfWeek.Sunday == dow)
            {
                // 做时间差
                Int32 diffDay =(Int32)(DayOfWeek.Monday - dow);
                // 如果时间差小于0 则加一周
                if (diffDay  < 0)
                {
                    diffDay += 7;
                }
                txtT1Day.Text = DateTime.Now.AddDays(diffDay).ToString("yyyyMMdd");
            }
            else
            {
                txtT1Day.Text = DateTime.Now.AddDays(1).ToString("yyyyMMdd");
            }

            txtListen.Text = app.AppConfigGet("checkPath");
            txtcheckPath.Text = app.AppConfigGet("Listen");
            txtBuildOk.Text = app.AppConfigGet("BuildOk");
            txtBuild004Ok.Text = app.AppConfigGet("Build004Ok");
            txtBuild012Ok.Text = app.AppConfigGet("Build012Ok");
            txtExcludeItems.Text = app.AppConfigGet("ExcludeItems");
            txtListenTipMsg.Text = app.AppConfigGet("ListenTipMsg");

            btnCancelSpeek.Text = "开始播放";
        }

        private void txtcheckPath_EditValueChanged(object sender, System.EventArgs e)
        {
            txtListen.Text = txtcheckPath.Text;

            if (string.IsNullOrEmpty(app.AppConfigGet("checkPath"))){
                Int32 addKey = app.AppConfigAdd("checkPath", "X:");
                if (addKey == 0 || addKey == 1) { }
                else
                {
                    MessageDxUtil.ShowError("添加配置checkPath失败");
                    return;
                }
            }

            if (string.IsNullOrEmpty(app.AppConfigGet("Listen")))
            {
                Int32 addKey = app.AppConfigAdd("Listen", "X:");
                if (addKey == 0 || addKey == 1) { }
                else
                {
                    MessageDxUtil.ShowError("添加配置Listen失败");
                    return;
                }
            }

            app.AppConfigSet("checkPath", txtcheckPath.Text.Trim());
            app.AppConfigSet("Listen", txtListen.Text.Trim());
        }

        /// <summary>
        /// 通用文本变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_EditValueChanged(object sender, System.EventArgs e)
        {
            string configKey = (sender as DevExpress.XtraEditors.TextEdit).Name;
            configKey = configKey.Substring(3, configKey.Length - 3);

            if (string.IsNullOrEmpty(app.AppConfigGet(configKey)))
            {
                Int32 addKey = app.AppConfigAdd(configKey, "X:");
                if (addKey == 0 || addKey == 1) { }
                else
                {
                    MessageDxUtil.ShowError(string.Format("添加配置{0}失败", configKey));
                    return;
                }
            }

            app.AppConfigSet(configKey, (sender as DevExpress.XtraEditors.TextEdit).Text.Trim());
        }

        private void txtListen_EditValueChanged(object sender, System.EventArgs e)
        {
            txtcheckPath.Text = txtListen.Text;

            if (string.IsNullOrEmpty(app.AppConfigGet("checkPath")))
            {
                Int32 addKey = app.AppConfigAdd("checkPath", "X:");
                if (addKey == 0 || addKey == 1) { }
                else
                {
                    MessageDxUtil.ShowError("添加配置checkPath失败");
                    return;
                }
            }

            if (string.IsNullOrEmpty(app.AppConfigGet("Listen")))
            {
                Int32 addKey = app.AppConfigAdd("Listen", "X:");
                if (addKey == 0 || addKey == 1) { }
                else
                {
                    MessageDxUtil.ShowError("添加配置Listen失败");
                    return;
                }
            }

            app.AppConfigSet("checkPath", txtcheckPath.Text.Trim());
            app.AppConfigSet("Listen", txtListen.Text.Trim());
        }

        /// <summary>
        /// 监听文件是否来了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnListen_Click(object sender, System.EventArgs e)
        {
            string revPath = txtListen.Text.Replace("yyyyMMdd", txtTDay.Text.Trim()).Trim();

            if (string.IsNullOrEmpty(revPath))
            {
                txtListen.Focus();
                MessageDxUtil.ShowError("监听路径 未配置");
                return;
            }

            if (string.IsNullOrEmpty(txtListenTipMsg.Text.Trim())){
                txtListenTipMsg.Focus();
                MessageDxUtil.ShowError("监听完成提示内容 未配置");
                return;
            }

            if (!DirectoryUtil.IsExistDirectory(revPath))
            {
                txtListen.Focus();
                MessageDxUtil.ShowError(string.Format("配置监听路径不存在 ListenPath: {0}", revPath));
                return;
            }

            Task task1 = new Task(() => {
                // 获取初始化数据
                list = InitRevData(revPath);
                if (list.Count == 0)
                {
                    MessageDxUtil.ShowError("启用代销数据项为0, 请在代销配置中配置启用的代销数据项");
                    return;
                }

                // 检查是否已经全部到齐
                Boolean isRev = true;
                foreach (ConsignmentInfo consignmentInfo in list)
                {
                    if (string.Equals("0", consignmentInfo.Data1))
                    {
                        isRev = false;
                    }
                }

                if (isRev)
                {
                    MessageDxUtil.ShowTips(txtListenTipMsg.Text.Trim());
                }
                else
                {
                    UpdateSpeekText("停止播放");
                    
                    //btnCancelSpeek.Text = "停止播放";
                    isAllArr = 1;

                    mfsw = new MyFileSystemWatcher(revPath);
                    mfsw.Filter = "*";
                    mfsw.Created += new FileSystemEventHandler(mfsw_Created);
                    mfsw.EnableRaisingEvents = true;
                    mfsw.NotifyFilter = NotifyFilters.Attributes | NotifyFilters.CreationTime | NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastAccess
                                           | NotifyFilters.LastWrite | NotifyFilters.Security | NotifyFilters.Size;
                    mfsw.IncludeSubdirectories = true;
                }
            });

            task1.Start();
        }

        private void mfsw_Created(object sender, FileSystemEventArgs e)
        {
            if (string.IsNullOrEmpty(txtListenTipMsg.Text.Trim()))
            {
                txtListenTipMsg.Focus();
                MessageDxUtil.ShowError("监听完成提示内容");
                return;
            }

            // 20191206 新增UI界面输出 
            //LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, string.Format("代销文件 {0} 已到", e.Name), typeof(FrmDealConsignment));
            AddLog(LogLevel.LOG_LEVEL_DEBUG, string.Format("代销文件 {0} 已到", e.Name));

            string tmpSysValue = e.Name.Split('\\')[0];
            // 更新状态表
            foreach (ConsignmentInfo consignmentInfo in list) {
                if (string.Equals(tmpSysValue, consignmentInfo.SysValue))
                {
                    consignmentInfo.Data1 = "1";
                    break;
                }
            }

            // 新增日志 补充差的数据
            StringBuilder notRevMsg = new StringBuilder();
            notRevMsg.Append("代销数据还未全部收齐，差");
            foreach (ConsignmentInfo consignmentInfo in list)
            {
                if (string.Equals("0", consignmentInfo.Data1))
                {
                    notRevMsg.Append(string.Format("({0}){1},", consignmentInfo.SysValue, consignmentInfo.Name));
                }
            }
            LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, notRevMsg.ToString(), typeof(FrmDealConsignment));

            if (isAllArr != 2)
                isAllArr = 1;

            // 检查状态表如果全部都不为 0
            foreach (ConsignmentInfo consignmentInfo in list)
            {
                if (string.Equals("0", consignmentInfo.Data1))
                {
                    if (2 != isAllArr)
                        isAllArr = 0;
                    break;
                }
            }

            // 已到期;
            if (isAllArr == 1)
            {
                // 状态置为已到期并且处理过
                isAllArr = 2;
                String str = txtListenTipMsg.Text.Trim();

                LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, str, typeof(FrmDealConsignment));

                speech.Rate = 0;//语速为正常语速
                //speech.SelectVoice("Microsoft Lili");//设置播音员（中文）
                speech.Volume = 100;//音量为最大音量
                speekerPrompt = speech.SpeakAsync(str);//语音阅读方法

                // 20200408 wujianming 新增自动化执行 start
                leftTime = 60;
                t = new System.Timers.Timer();
                t.Elapsed += new ElapsedEventHandler(t_Elapsed);
                t.Interval = 1000;
                t.Enabled = true;
                t.AutoReset = true;
                // 20200408 wujianming 新增自动化执行 end

                //Thread创建的线程是前台线程
                Thread th = new Thread(delegate()
                {
                    if (DialogResult.OK == MessageDxUtil.ShowTips(str)) {
                        if (speekerPrompt != null)
                        {
                            speech.SpeakAsyncCancel(speekerPrompt);
                            speech.SpeakAsyncCancelAll();
                        }

                        if (mfsw != null)
                        {
                            mfsw.EnableRaisingEvents = false;
                            mfsw.Created -= new FileSystemEventHandler(mfsw_Created);
                        }
                    }
                   
                });
                th.Start();

                speech.SpeakCompleted += (sender1, e1) =>
                {
                    if (!mfsw.EnableRaisingEvents)
                        return;

                    ThreadHelper.Sleep(5000);

                    speekerPrompt = speech.SpeakAsync(str);//语音阅读方法
                };
            }
        }

        System.Timers.Timer t = null;
        Int32 leftTime = 60;

        /// <summary>
        /// 定时器执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void t_Elapsed(object sender, EventArgs e)
        {
            if (leftTime == 0)
            {
                // 定时器关闭
                t.Enabled = false;

                // 放声音
                speech.Rate = 0;//语速为正常语速
                //speech.SelectVoice("Microsoft Lili");//设置播音员（中文）
                speech.Volume = 100;//音量为最大音量
                speech.Speak("系统自动执行代销数据拷贝");//语音阅读方法

                // 拷贝代销数据
                btnFirstStep_Click(null, null);

                SetTextCallback d2 = new SetTextCallback(ThreadProcSafe);
                this.btnCancelSpeek.Invoke(d2, new object[] { "开始播放" });

                // 取消speekerPrompt播放
                if (speekerPrompt != null)
                {
                    speech.SpeakAsyncCancel(speekerPrompt);
                    speech.SpeakAsyncCancelAll();
                }

                // 取消文件监控
                if (mfsw != null)
                {
                    mfsw.EnableRaisingEvents = false;
                    mfsw.Created -= new FileSystemEventHandler(mfsw_Created);
                }
            }
            else {
                //这个里面是要执行的代码
                SetTextCallback d = new SetTextCallback(ThreadProcSafe);
                this.btnCancelSpeek.Invoke(d, new object[] { string.Format("距离自动执行拷贝代销数据还剩{0}秒", leftTime) });

                leftTime--;
            }
        }

        // 第一步：定义委托类型
        // 将text更新的界面控件的委托类型
        delegate void SetTextCallback(string text);

        //第二步：定义线程的主体方法
        /// <summary>
        /// 线程的主体方法
        /// </summary>
        private void ThreadProcSafe(string text)
        {
            // InvokeRequired required compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 
            if (this.btnCancelSpeek.InvokeRequired)//如果调用控件的线程和创建创建控件的线程不是同一个则为True
            {
                while (!this.btnCancelSpeek.IsHandleCreated)
                {
                    //解决窗体关闭时出现“访问已释放句柄“的异常
                    if (this.btnCancelSpeek.Disposing || this.btnCancelSpeek.IsDisposed)
                        return;
                }
                SetTextCallback d = new SetTextCallback(ThreadProcSafe);
                this.btnCancelSpeek.Invoke(d, new object[] { text });
            }
            else
            {
                this.btnCancelSpeek.Text = text;
            }
        }

        /// <summary>
        /// 停止播放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelSpeek_Click(object sender, EventArgs e)
        {
            if ((sender as SimpleButton).Text == "停止播放")
            {
                if (speekerPrompt != null)
                {
                    speech.SpeakAsyncCancel(speekerPrompt);
                    speech.SpeakAsyncCancelAll();
                }

                if (mfsw != null)
                {
                    mfsw.EnableRaisingEvents = false;
                    mfsw.Created -= new FileSystemEventHandler(mfsw_Created);
                }

                btnCancelSpeek.Text = "开始播放";
            }
            else
            {
                speech.Rate = 0;//语速为正常语速
                //speech.SelectVoice("Microsoft Lili");//设置播音员（中文）
                speech.Volume = 100;//音量为最大音量
                speekerPrompt = speech.SpeakAsync(txtListenTipMsg.Text.Trim());//语音阅读方法
                btnCancelSpeek.Text = "开始播放";
            }
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            txtTDay.Enabled = checkEdit1.Checked;
            txtT1Day.Enabled = checkEdit1.Checked;
        }

        /// <summary>
        /// 检查代销数据是否已经收齐
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCheck_Click(object sender, EventArgs e)
        {
            string revPath = txtcheckPath.Text.Replace("yyyyMMdd", txtTDay.Text.Trim()).Trim();

            if (string.IsNullOrEmpty(revPath))
            {
                txtcheckPath.Focus();
                MessageDxUtil.ShowError("检查路径 未配置");
                return;
            }

            if (!DirectoryUtil.IsExistDirectory(revPath))
            {
                txtcheckPath.Focus();
                MessageDxUtil.ShowError(string.Format("配置检查路径不存在 CheckPath: {0}", revPath));
                return;
            }
            Task task1 = new Task(() => {
                list = InitRevData(revPath);
                if (list.Count == 0)
                {
                    MessageDxUtil.ShowError("启用代销数据项为0, 请在代销配置中配置启用的代销数据项");
                    return;
                }

                StringBuilder notRevMsg = new StringBuilder();
                Boolean isRev = true;
                notRevMsg.Append("代销数据还未全部收齐，差");
                foreach (ConsignmentInfo consignmentInfo in list)
                {
                    if (string.Equals("0", consignmentInfo.Data1))
                    {
                        isRev = false;
                        notRevMsg.Append(string.Format("({0}){1},", consignmentInfo.SysValue, consignmentInfo.Name));
                    }
                }

                if (isRev)
                {
                    MessageDxUtil.ShowTips(txtListenTipMsg.Text.Trim());
                }
                else
                {
                    MessageDxUtil.ShowTips(notRevMsg.ToString());
                }
            });

            task1.Start();
        }

        private List<ConsignmentInfo> InitRevData(string revPath)
        {
            SearchCondition condition = new SearchCondition();
            condition.AddCondition("EnableStatus", (Int32)EnableStatus.启用, SqlOperator.Equal);
            string where = condition.BuildConditionSql().Replace("Where", "");
            List<ConsignmentInfo> list = BLLFactory<Consignment>.Instance.GetAllConsignmentInfo(where);

            // 更新consignmentInfo.Data1 确认文件是否都到期了
            foreach (ConsignmentInfo consignmentInfo in list)
            {
                string checkPath = revPath + "\\" + consignmentInfo.SysValue;
                if (!DirectoryUtil.IsExistDirectory(checkPath))
                {
                    DirectoryUtil.CreateDirectory(checkPath);
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_WARN, string.Format("系统创建了 {0} 文件夹", checkPath), typeof(FrmDealConsignment));
                }

                if (DirectoryUtil.ContainFile(checkPath, "*.*"))
                {
                    consignmentInfo.Data1 = "1";
                }
                else
                {
                    consignmentInfo.Data1 = "0";
                }
            }

            return list;
        }

        private void btnBuildOk_Click(object sender, EventArgs e)
        {
             Task task1 = new Task(() => {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_INFO, "START [开始生成OK标志]", typeof(FrmDealConsignment));

                AddLog(LogLevel.LOG_LEVEL_INFO, "START [开始生成OK标志]");

                string buildPath = txtBuildOk.Text.Replace("yyyyMMdd", txtTDay.Text.Trim()).Trim();
                string build004Path = txtBuild004Ok.Text.Replace("yyyyMMdd", txtTDay.Text.Trim()).Trim();
                string build012Path = txtBuild012Ok.Text.Replace("yyyyMMdd", txtTDay.Text.Trim()).Trim();
                string TDay = txtTDay.Text.Trim();
                string T1Day = txtT1Day.Text.Trim();
                string[] ExcludeItems = txtExcludeItems.Text.Trim().Split(',');

                if (string.IsNullOrEmpty(buildPath))
                {
                    txtcheckPath.Focus();
                    MessageDxUtil.ShowError("生成路径 未配置");
                    return;
                }

                if (!DirectoryUtil.IsExistDirectory(buildPath))
                {
                    txtcheckPath.Focus();
                    MessageDxUtil.ShowError(string.Format("配置生成路径不存在 BuildPath: {0}", buildPath));
                    return;
                }

                if (string.IsNullOrEmpty(build004Path))
                {
                    txtcheckPath.Focus();
                    MessageDxUtil.ShowError("3T_004_hq 未配置");
                    return;
                }

                if (!DirectoryUtil.IsExistDirectory(build004Path))
                {
                    txtcheckPath.Focus();
                    MessageDxUtil.ShowError(string.Format("配置 3T_004_hq 不存在 Build004Path: {0}", build004Path));
                    return;
                }

                if (string.IsNullOrEmpty(build012Path))
                {
                    txtcheckPath.Focus();
                    MessageDxUtil.ShowError("3T_012_HQ 未配置");
                    return;
                }

                if (!DirectoryUtil.IsExistDirectory(build012Path))
                {
                    txtcheckPath.Focus();
                    MessageDxUtil.ShowError(string.Format("配置 3T_012_HQ 不存在 Build012Path: {0}", build012Path));
                    return;
                }

                // 遍历全部的文件夹并删除已经存在的OK标志
                String[] files = DirectoryUtil.GetFileNames(buildPath, "*.ok", true);
                foreach (String file in files)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, string.Format("删除文件OK文件 {0}", file), typeof(FrmDealConsignment));
                    FileUtil.DeleteFile(file);
                }

                Int32 buildOkCount = 0;
                Int32 build004OkCount = 0;
                Int32 build012OkCount = 0;
                // 遍历全部的文件夹并生成OK标志
                files = DirectoryUtil.GetFileNames(buildPath, "*.*", true);
                foreach (String file in files)
                {
                    // 对于排除的项目不生成ok标志
                    bool isSkip = false;

                    foreach (string item in ExcludeItems)
                    {
                        if (file.Contains(item))
                        {
                            isSkip = true;
                            continue;
                        }
                    }

                    if (isSkip) continue;

                    string fileOk = string.Format("{0}.ok", file);
                    AddLog(LogLevel.LOG_LEVEL_DEBUG, string.Format("生成文件OK文件 {0}", fileOk));
                    FileUtil.CreateFile(fileOk);

                    buildOkCount++;
                }

                // 遍历 3T_004_hq 下 T+1对应的文件生成OK标志
                files = DirectoryUtil.GetFileNames(build004Path, string.Format("*{0}*.ok", T1Day), true);
                foreach (String file in files)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, string.Format("删除004文件OK文件 {0}", file), typeof(FrmDealConsignment));
                    FileUtil.DeleteFile(file);
                }
              
                // 遍历全部的文件夹并生成OK标志
                files = DirectoryUtil.GetFileNames(build004Path, string.Format("*{0}*.*", T1Day), true);
                foreach (String file in files)
                {
                    string fileOk = string.Format("{0}.ok", file);
                    AddLog(LogLevel.LOG_LEVEL_DEBUG, string.Format("生成004文件OK文件 {0}", fileOk));
                    //LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, string.Format("生成004文件OK文件 {0}", fileOk), typeof(FrmDealConsignment));
                    FileUtil.CreateFile(fileOk);
                    build004OkCount++;
                }

                // 遍历 3T_012_HQ 下 T+1对应的文件生成OK标志
                files = DirectoryUtil.GetFileNames(build012Path, string.Format("*{0}*.ok", T1Day), true);
                foreach (String file in files)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, string.Format("删除012文件OK文件 {0}", file), typeof(FrmDealConsignment));
                    FileUtil.DeleteFile(file);
                }

                // 遍历全部的文件夹并生成OK标志
                files = DirectoryUtil.GetFileNames(build012Path, string.Format("*{0}*.*", T1Day), true);
                foreach (String file in files)
                {
                    string fileOk = string.Format("{0}.ok", file);
                    AddLog(LogLevel.LOG_LEVEL_DEBUG, string.Format("生成012文件OK文件 {0}", fileOk));
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, string.Format("生成012文件OK文件 {0}", fileOk), typeof(FrmDealConsignment));
                    FileUtil.CreateFile(fileOk);
                    build012OkCount++;
                }
                string str = string.Format("代销处理完成! 生成路径生成OK共计{0}条记录 3T_004_hq生成OK共计{1}条记录 3T_012_HQ生成OK共计{2}条记录", buildOkCount, build004OkCount, build012OkCount);
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_INFO, "END [" + str + "]", typeof(FrmDealConsignment));

                AddLog(LogLevel.LOG_LEVEL_INFO, "END [" + str + "]");

                MessageDxUtil.ShowTips(str);
            });

             task1.Start();
        }

        private void txtTDay_EditValueChanged(object sender, EventArgs e)
        {
            DayOfWeek dow = txtTDay.DateTime.DayOfWeek;
            if (DayOfWeek.Friday == dow || DayOfWeek.Saturday == dow || DayOfWeek.Sunday == dow)
            {
                // 做时间差
                Int32 diffDay = (Int32)(DayOfWeek.Monday - dow);
                // 如果时间差小于0 则加一周
                if (diffDay < 0)
                {
                    diffDay += 7;
                }
                txtT1Day.Text = txtTDay.DateTime.AddDays(diffDay).ToString("yyyyMMdd");
            }
            else
            {
                txtT1Day.Text = txtTDay.DateTime.AddDays(1).ToString("yyyyMMdd");
            }
        }

        private delegate void InvokeCallback(LogLevel loglevel, String logstr);

        private delegate void InvokeCallbackText(String str);

        private void AddLog(LogLevel loglevel, String logstr) {

            LogHelper.WriteLog(loglevel, logstr, typeof(FrmDealConsignment));

            if (!checkEdit2.Checked)
                return;

            if (memoEdit1.InvokeRequired)//当前线程不是创建线程
                memoEdit1.Invoke(new InvokeCallback(AddLog), new object[] { loglevel, logstr });//回调
            else//当前线程是创建线程（界面线程）
            {
                memoEdit1.Text = string.Format("日志级别:{0}----日志内容{1}\r\n", loglevel, logstr) + memoEdit1.Text;//直接更新
            }
        }

        private void UpdateSpeekText(String str) {
            if (btnCancelSpeek.InvokeRequired)//当前线程不是创建线程
                btnCancelSpeek.Invoke(new InvokeCallbackText(UpdateSpeekText), new object[] { str });//回调
            else//当前线程是创建线程（界面线程）
            {
                btnCancelSpeek.Text = str;
            }
        }

        /// <summary>
        /// 第一步
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFirstStep_Click(object sender, EventArgs e)
        {
            string fileName = txtFirstStep.Text.Trim();
            if (string.IsNullOrEmpty(fileName))
            {
                MessageDxUtil.ShowError("接收申请文件BAT未配置");
                return;
            }

            if (!FileUtil.IsExistFile(fileName))
            {
                MessageDxUtil.ShowError("接收申请文件BAT文件不存在");
                return;
            }
            Task task1 = new Task(() => {
                String outStr = string.Empty;
                // 参数 第一个参数为当前日期，第二个参数为T+1 第三个参数为是否启用pause 用程序调用默认不启用pause
                string arguments = string.Format("{0} 0", txtTDay.Text.Trim());
                SystemHelper.RunCmd(fileName, arguments, out outStr);
                AddLog(LogLevel.LOG_LEVEL_INFO, outStr);

                MessageDxUtil.ShowTips(fileName+"操作成功");
            });
            task1.Start();
        }

        /// <summary>
        /// 第二步
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSecondStep_Click(object sender, EventArgs e)
        {
            string fileName = txtSecondStep.Text.Trim();
            if (string.IsNullOrEmpty(fileName))
            {
                MessageDxUtil.ShowError("发送行情数据BAT未配置");
                return;
            }

            if (!FileUtil.IsExistFile(fileName))
            {
                MessageDxUtil.ShowError("发送行情数据BAT文件不存在");
                return;
            }
            Task task1 = new Task(() =>
            {
                String outStr = string.Empty;
                // 参数 第一个参数为当前日期，第二个参数为T+1 第三个参数为是否启用pause 用程序调用默认不启用pause
                string arguments = string.Format("{0} {1} 0", txtTDay.Text.Trim(), txtT1Day.Text.Trim());
                SystemHelper.RunCmd(fileName, arguments, out outStr);
                AddLog(LogLevel.LOG_LEVEL_INFO, outStr);

                MessageDxUtil.ShowTips(fileName + "操作成功");
            });
            task1.Start();
        }

        /// <summary>
        /// 第三步
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnThirdStep_Click(object sender, EventArgs e)
        {
            string fileName = txtThirdStep.Text.Trim();
            if (string.IsNullOrEmpty(fileName))
            {
                MessageDxUtil.ShowError("发送确认数据BAT未配置");
                return;
            }

            if (!FileUtil.IsExistFile(fileName))
            {
                MessageDxUtil.ShowError("发送确认数据BAT文件不存在");
                return;
            }
            Task task1 = new Task(() =>
            {
                String outStr = string.Empty;
                // 参数 第一个参数为当前日期，第二个参数为T+1 第三个参数为是否启用pause 用程序调用默认不启用pause
                string arguments = string.Format("{0} {1} 0", txtTDay.Text.Trim(), txtT1Day.Text.Trim());
                SystemHelper.RunCmd(fileName, arguments, out outStr);
                AddLog(LogLevel.LOG_LEVEL_INFO, outStr);

                MessageDxUtil.ShowTips(fileName + "操作成功");
            });
            task1.Start();
        }

        /// <summary>
        /// 第四步
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnForthStep_Click(object sender, EventArgs e)
        {
            string fileName = txtForthStep.Text.Trim();
            if (string.IsNullOrEmpty(fileName))
            {
                MessageDxUtil.ShowError("发送托管行数据BAT未配置");
                return;
            }

            if (!FileUtil.IsExistFile(fileName))
            {
                MessageDxUtil.ShowError("发送托管行数据BAT文件不存在");
                return;
            }
            Task task1 = new Task(() =>
            {
                String outStr = string.Empty;
                // 参数 第一个参数为当前日期，第二个参数为T+1 第三个参数为是否启用pause 用程序调用默认不启用pause
                string arguments = string.Format("{0} {1} 0", txtTDay.Text.Trim(), txtT1Day.Text.Trim());
                SystemHelper.RunCmd(fileName, arguments, out outStr);
                AddLog(LogLevel.LOG_LEVEL_INFO, outStr);

                MessageDxUtil.ShowTips(fileName + "操作成功");
            });
            task1.Start();
        }

        /// <summary>
        /// 第五步
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFifthStep_Click(object sender, EventArgs e)
        {
            string fileName = txtFifthStep.Text.Trim();
            if (string.IsNullOrEmpty(fileName))
            {
                MessageDxUtil.ShowError("上传自建TA数据BAT未配置");
                return;
            }

            if (!FileUtil.IsExistFile(fileName))
            {
                MessageDxUtil.ShowError("上传自建TA数据BAT文件不存在");
                return;
            }
            Task task1 = new Task(() =>
            {
                String outStr = string.Empty;
                // 参数 第一个参数为当前日期，第二个参数为T+1 第三个参数为是否启用pause 用程序调用默认不启用pause
                string arguments = string.Format("{0} {1} 0", txtTDay.Text.Trim(), txtT1Day.Text.Trim());
                SystemHelper.RunCmd(fileName, arguments, out outStr);
                AddLog(LogLevel.LOG_LEVEL_INFO, outStr);

                MessageDxUtil.ShowTips(fileName + "操作成功");
            });
            task1.Start();
        }

        /// <summary>
        /// 第六步
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSixthStep_Click(object sender, EventArgs e)
        {
            string fileName = txtSixthStep.Text.Trim();
            if (string.IsNullOrEmpty(fileName))
            {
                MessageDxUtil.ShowError("发送OTC转让数据BAT未配置");
                return;
            }

            if (!FileUtil.IsExistFile(fileName))
            {
                MessageDxUtil.ShowError("发送OTC转让数据BAT文件不存在");
                return;
            }
            Task task1 = new Task(() =>
            {
                String outStr = string.Empty;
                // 参数 第一个参数为当前日期，第二个参数为T+1 第三个参数为是否启用pause 用程序调用默认不启用pause
                string arguments = string.Format("{0} 0", txtTDay.Text.Trim());
                SystemHelper.RunCmd(fileName, arguments, out outStr);
                AddLog(LogLevel.LOG_LEVEL_INFO, outStr);

                MessageDxUtil.ShowTips(fileName + "操作成功");
            });
            task1.Start();
        }
    }
}
