using DevExpress.XtraEditors;
using JCodes.Framework.Common;
using JCodes.Framework.Common.Files;
using JCodes.Framework.jCodesenum;
using System;
using System.IO;
using System.Reflection;

namespace JCodes.Framework.TestWinForm.ZsDaixiao
{
    public partial class FrmDealPassword : XtraForm
    {
        public FrmDealPassword()
        {
            InitializeComponent();
        }

        private delegate void InvokeCallback(LogLevel loglevel, String logstr);

        private delegate void InvokeCallbackText(String str);

        private void AddLog(LogLevel loglevel, String logstr) {

            LogHelper.WriteLog(loglevel, logstr, typeof(FrmDealConsignment));

            if (memoEdit1.InvokeRequired)//当前线程不是创建线程
                memoEdit1.Invoke(new InvokeCallback(AddLog), new object[] { loglevel, logstr });//回调
            else//当前线程是创建线程（界面线程）
            {
                memoEdit1.Text = string.Format("日志级别:{0}----日志内容{1}\r\n", loglevel, logstr) + memoEdit1.Text;//直接更新
            }
        }

        private void UpdateSpeekText(String str) {
            if (btnBuild.InvokeRequired)//当前线程不是创建线程
                btnBuild.Invoke(new InvokeCallbackText(UpdateSpeekText), new object[] { str });//回调
            else//当前线程是创建线程（界面线程）
            {
                btnBuild.Text = str;
            }
        }

        private void btnBuild_Click(object sender, EventArgs e)
        {
            string[] filesNames = DirectoryUtil.GetFileNames(txtFilePath.Text.Trim());
            
        }
    }
}
