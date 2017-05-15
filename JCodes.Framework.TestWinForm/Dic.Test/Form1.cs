using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using JCodes.Framework.CommonControl;
using JCodes.Framework.BLL;
using JCodes.Framework.Common;
using JCodes.Framework.jCodesenum.BaseEnum;

namespace TestDictionary
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //代码设置
            MyConstants.License = "37c6V0hDLkRpY3Rpa25hcnl85LyN5Y2O6IGqfOW5_*W3nueIseWQr*i-qubKgObcr*bciemZkOWFrOWPuHxGYWxzZQvv";
            FrmDictionary dict = new FrmDictionary();
            dict.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //JCodes.Framework.jCodesenum.BaseEnum.
            LogHelper.WriteLog(LogLevel.LOG_LEVEL_ERR, "吴建明测试", typeof(Form1));
            try
            {
                //this.txtItemType.Items.Clear();
                //this.txtItemType.Items.AddRange(DictItemUtil.GetDictByDictType("备件类别"));
            }
            catch (Exception ex)
            {
                //MessageUtil.ShowError(ex.Message);
            }
        }

        private void btnDistrict_Click(object sender, EventArgs e)
        {
            FrmCityDistrict dlg = new FrmCityDistrict();
            dlg.Show();
        }
    }
}
