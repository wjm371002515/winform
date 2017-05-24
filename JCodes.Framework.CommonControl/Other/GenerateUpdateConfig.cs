using JCodes.Framework.Common;
using JCodes.Framework.Common.Encrypt;
using JCodes.Framework.Common.Files;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JCodes.Framework.CommonControl.Other
{
    public partial class GenerateUpdateConfig : Form
    {
        public GenerateUpdateConfig()
        {
            InitializeComponent();
        }

        private void btnGetMachineCode_Click(object sender, EventArgs e)
        {
            // 不自动更新排除的文件 license.lic 序列号
            // log              日志文件
            // AutoUpdater      自动更新
            string startupPath = Application.StartupPath;

            if (FileUtil.IsExistFile(startupPath + "\\" + txtFileName.Text.Trim()))
            {
                FileUtil.DeleteFile(startupPath + "\\" + txtFileName.Text.Trim());
            }

            // 写入前面的模板
            FileUtil.AppendText(txtFileName.Text.Trim(), "<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n", Encoding.UTF8);
            FileUtil.AppendText(txtFileName.Text.Trim(), "<manifest>\r\n", Encoding.UTF8);
            FileUtil.AppendText(txtFileName.Text.Trim(), "\t<version>"+txtVersion.Text.Trim()+"</version>\r\n", Encoding.UTF8);
            FileUtil.AppendText(txtFileName.Text.Trim(), "\t<description>"+txtUpdateInfo.Text.Trim()+"</description>\r\n", Encoding.UTF8);
            FileUtil.AppendText(txtFileName.Text.Trim(), "\t<!--applicationId运用程序ID，需要与客户端配置一样，否则不会进行更新-->\r\n", Encoding.UTF8);
            FileUtil.AppendText(txtFileName.Text.Trim(), "\t<myapplication applicationId=\""+txtApplicationId.Text.Trim()+"\">\r\n", Encoding.UTF8);
            FileUtil.AppendText(txtFileName.Text.Trim(), "\t\t<!--暂时没有使用，重新启动exe名称，parameters启动时传入的参数-->\r\n", Encoding.UTF8);
            FileUtil.AppendText(txtFileName.Text.Trim(), "\t\t<entryPoint file=\""+txtMain.Text.Trim()+"\" parameters=\""+txtparam.Text.Trim()+"\" />\r\n", Encoding.UTF8);
            FileUtil.AppendText(txtFileName.Text.Trim(), "\t</myapplication>\r\n", Encoding.UTF8);
            FileUtil.AppendText(txtFileName.Text.Trim(), "\t<!--base表示存放该文件的url-->\r\n", Encoding.UTF8);
            FileUtil.AppendText(txtFileName.Text.Trim(), "\t<files base=\"http://www.jcodes.cn/update/Pack/\">\r\n", Encoding.UTF8);
            FileUtil.AppendText(txtFileName.Text.Trim(), "\t\t<!--文件名称-->\r\n", Encoding.UTF8);

            string[] files = DirectoryUtil.GetAllFileNames(startupPath);
            for (Int32 i = 0; i < files.Length; i++)
            {
                // 日志路径
                if (files[i].Contains("log\\"))
                    continue;
                if (files[i].Contains("license.lic"))
                    continue;
                if (files[i].Contains("AutoUpdater"))
                    continue;

                FileStream fsread = new FileStream(files[i], FileMode.Open, FileAccess.Read, FileShare.Read);
                byte[] md5File = new byte[fsread.Length];
                fsread.Read(md5File, 0, (int)fsread.Length);                               // 将文件流读取到Buffer中
                fsread.Close();
                string result = MD5Util.MD5Buffer(md5File, 0, md5File.Length);             // 对Buffer中的字节内容算MD5
                string tmp = files[i].Replace(startupPath + "\\", "");
                FileUtil.AppendText(txtFileName.Text.Trim(), "\t\t<file source=\"" + tmp + "\" md5=\"" + result + "\" checkmd5=\"true\" />\r\n", Encoding.UTF8);
            }
            FileUtil.AppendText(txtFileName.Text.Trim(), "\t</files>\r\n", Encoding.UTF8);
            FileUtil.AppendText(txtFileName.Text.Trim(), "</manifest>\r\n", Encoding.UTF8);

            MessageDxUtil.ShowTips("操作完成");
        }
    }
}
