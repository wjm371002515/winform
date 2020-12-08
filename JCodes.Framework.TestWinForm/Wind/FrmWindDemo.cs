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
using WAPIWrapperCSharp;

namespace JCodes.Framework.Wind
{
    public partial class FrmWindDemo : XtraForm
    {
        public FrmWindDemo()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeal_Click(object sender, EventArgs e)
        {
            //接口初始化
            WindAPI w = new WindAPI();

            //在接口操作前，用户首先要登录并启动C#插件，即使用w.start()命令。其中，返回值0表示登陆成功，负数表示登陆失败，可通过w.getErrorMsg()函数获取错误信息。
            w.start(); 

            //当需要判断是否连接c#接口时，可以使用w.isconnected()命令。返回值True表示处于连接状态，反之False表示连接断开。  
            if (w.isconnected()){
                WindData wd = w.wss("000001.SZ,000002.SZ", "open,low,close,volume", "tradeDate=20180809;priceAdj=U;cycle=D");
                //返回的数据转化为便于使用的数据结构,
                object[,] getData = (object[,])wd.getDataByFunc("wss", false);
                MessageBox.Show("连接成功");
            }
            
            //当需要停止使用C#接口时，可以使用w.stop()命令，由于程序退出时会自动执行w.stop()命令，用户一般并不需要执行。
            w.stop(); 
        }
    }
}
