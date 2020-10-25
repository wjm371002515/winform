using DevExpress.Utils;
using JCodes.Framework.BLL;
using JCodes.Framework.Common.Files;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.Entity;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JCodes.Framework.Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnURL_Click(object sender, EventArgs e)
        {
            string url = txtURL.Text.Trim();
            //rtbLog.Text = rtbLog.Text + URL.URLDeConvert(url) + "\r\n";
        }

        private void btnDeURL_Click(object sender, EventArgs e)
        {
            //string url = txtURL.Text.Trim();
            //rtbLog.Text = rtbLog.Text + URL.URLEnConvert(url) + "\r\n";
            this.dictControl1.EditValue = 1;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            /*Database db = DatabaseFactory.CreateDatabase("sqlserver2");
            String sql = "select * from TCard";
            DbCommand command = db.GetSqlStringCommand(sql);

            using (IDataReader dr = db.ExecuteReader(command))
            {
                while (dr.Read())
                {
                    Console.WriteLine("车牌号" + dr["Licence"]);
                }
            }*/

            List<CityInfo> list = BLLFactory<City>.Instance.GetAll();

            return;

            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = @"F:\test";
            watcher.Filter = "*";
            watcher.Created += new FileSystemEventHandler(OnCreated);
            watcher.EnableRaisingEvents = true;
            watcher.NotifyFilter = NotifyFilters.Attributes | NotifyFilters.CreationTime | NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastAccess
                                   | NotifyFilters.LastWrite | NotifyFilters.Security | NotifyFilters.Size;
            watcher.IncludeSubdirectories = true;
        }

        private static void OnCreated(object source, FileSystemEventArgs e)
        {
            SpeechSynthesizer speech = new SpeechSynthesizer();
            String str = "它还提供了一个很棒的特性,可以通过托管代码访问嵌入到操作系统中的计划任务程序的功能。首先,创建一个C#控制台应用程序,然后从System32文件夹中导入";
            speech.Rate = 0;//语速为正常语速
            //speech.SelectVoice("Microsoft Lili");//设置播音员（中文）
            speech.Volume = 100;//音量为最大音量
            speech.SpeakAsync(str);//语音阅读方法
        }

        /// <summary>
        /// 配置config文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfigSet_Click(object sender, EventArgs e)
        {
            AppConfig config = new AppConfig();
            if (config.AppConfigAdd("wujianming", "123") == 0)
            {
                MessageDxUtil.ShowError("添加成功");
            }
            else {
                MessageDxUtil.ShowError("添加失败");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnMultiDatabase_Click(object sender, EventArgs e)
        {
            DatabaseSettings setting = ConfigurationManager.GetSection("dataConfiguration") as DatabaseSettings;


        }


    }
}
