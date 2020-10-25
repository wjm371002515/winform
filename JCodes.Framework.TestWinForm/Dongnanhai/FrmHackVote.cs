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
using System.Speech.Synthesis;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aspose.Cells;
using JCodes.Framework.Common.Format;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace JCodes.Framework.TestWinForm.Haotian
{
    public partial class FrmHackVote : XtraForm
    {
        public FrmHackVote()
        {
            InitializeComponent();
        }

        private delegate void InvokeCallback(LogLevel loglevel, String logstr);

        private void AddLog(LogLevel loglevel, String logstr) {

            LogHelper.WriteLog(loglevel, logstr, typeof(FrmHackVote));

            if (memoEdit1.InvokeRequired)//当前线程不是创建线程
                memoEdit1.Invoke(new InvokeCallback(AddLog), new object[] { loglevel, logstr });//回调
            else//当前线程是创建线程（界面线程）
            {
                memoEdit1.Text = string.Format("日志级别:{0}----日志内容{1}\r\n", loglevel, logstr) + memoEdit1.Text;//直接更新
            }
        }

        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeal_Click(object sender, EventArgs e)
        {
            // 校验文件是否已经选择了
            if (string.IsNullOrEmpty(txtURL.Text.Trim()))
            {
                MessageDxUtil.ShowError("URL参数未配置");
                txtURL.Focus();
                return;
            }

            string strstartNum = txtStartUserId.Text.Trim();
            string strendNum = txtEndUserId.Text.Trim();

            Int32 intStartNum = ConvertHelper.ToInt32(strstartNum, 0);
            Int32 intEndNum = ConvertHelper.ToInt32(strendNum, 0);

            if (intEndNum < intStartNum) {
                MessageDxUtil.ShowError("配置的开始UserId不可以大于结束的UserId");
                txtStartUserId.Focus();
                return;
            }

            string parammodel = txtParam.Text.Trim();
 
            Task task1 = new Task(() => {
                for(Int32 i = intStartNum; i <= intEndNum; i ++){
                    try
                    {
                        System.Net.ServicePointManager.Expect100Continue = false;

                        string param = parammodel.Replace("{userId}", i.ToString());
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(txtURL.Text.Trim());
                        //request.AllowAutoRedirect = false; //禁止自动重定向
                        request.Method = "POST";
                        request.Host = "ynzx.zgwyzxw.cn";
                        //request.Connection = "keep-alive";
                        //request.Headers.Add("Connection", "keep-alive");
                        request.ContentLength = param.Length;
                        request.Accept = "application/json, text/plain, */*";
                        request.Headers.Add("Origin", "https://ynzx.zgwyzxw.cn");
                        request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36";
                        request.ContentType = "application/x-www-form-urlencoded";
                        request.Referer = "https://ynzx.zgwyzxw.cn/EditData1";
                        request.Headers.Add("Sec-Fetch-Mode", "cors");
                        request.Headers.Add("Sec-Fetch-Site", "same-origin");
                        //request.Headers.Add("Accept-Encoding", "gzip, deflate, br");
                        request.Headers.Add("Accept-Language", "zh-CN,zh;q=0.9");



                        StreamWriter write = new StreamWriter(request.GetRequestStream(), Encoding.ASCII);
                        write.Write(param);
                        write.Flush();
                        HttpWebResponse response = null;
                        try
                        {
                            this.SetCertificatePolicy();
                            response = (HttpWebResponse)request.GetResponse();
                        }
                        catch (System.Exception ex)
                        {
                            LogHelper.WriteLog(LogLevel.LOG_LEVEL_ERR, ex, typeof(FrmHackVote));
                            //MessageDxUtil.ShowError(ex.Message);
                        }

                        // response = (HttpWebResponse)request.GetResponse();
                        string encoding = response.ContentEncoding;
                        if (encoding == null || encoding.Length < 1)
                        {
                            encoding = "UTF-8";
                        }
                        StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
                        string retstring = reader.ReadToEnd();
                        AddLog(LogLevel.LOG_LEVEL_INFO, retstring);

                        if (txtURL.Text.Contains("getUser")) {
                            ToInsertToUser(retstring);
                        }


                    }
                    catch (Exception ex) {
                        LogHelper.WriteLog(LogLevel.LOG_LEVEL_ERR, ex, typeof(FrmHackVote));
                        //MessageDxUtil.ShowError(ex.Message); 
                    }
                   
                }
                MessageDxUtil.ShowTips("执行完成");
            });

            task1.Start();
        }

        //注册证书验证回调事件，在请求之前注册
        private void SetCertificatePolicy()
        {
            ServicePointManager.ServerCertificateValidationCallback
                       += RemoteCertificateValidate;
        }
        /// <summary>  
        /// 远程证书验证，固定返回true 
        /// </summary>  
        private static bool RemoteCertificateValidate(object sender, X509Certificate cert,
            X509Chain chain, SslPolicyErrors error)
        {
            return true;
        }

        /// <summary>
        /// 插入到User表中
        /// </summary>
        /// <param name="str"></param>
        private static void ToInsertToUser(string str) {
            // 
            JObject jo = (JObject)JsonConvert.DeserializeObject(str);

            //string sql = string.Format("insert into T_Dongnanhai_User(Id, phone, real_name, sex) values({0}, '{1}', '{2}', '{2}')", , jo["data"]["phone"], jo["data"]["real_name"], jo["data"]["sex"]);

            DongnanhaiVotesUser dongnanhaiVotesUser = new DongnanhaiVotesUser();
            VoteUserInfo voteUserInfo = new Entity.VoteUserInfo();
            voteUserInfo.Id = ConvertHelper.ToInt32(jo["data"]["id"].ToString(), 0);
            voteUserInfo.MobilePhone = jo["data"]["phone"].ToString();
            voteUserInfo.LoginName = jo["data"]["real_name"].ToString();
            voteUserInfo.Gender = (short)EnumHelper.GetMemberValue<Gender>(jo["data"]["sex"].ToString());
            dongnanhaiVotesUser.InsertVoteUser(voteUserInfo);

        }
    }
}
