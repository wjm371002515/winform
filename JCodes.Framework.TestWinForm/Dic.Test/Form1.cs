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
using JCodes.Framework.CommonControl.Pager;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.AddIn.Dictionary;
using JCodes.Framework.jCodesenum;

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
            FrmDictionary dict = new FrmDictionary();
            dict.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //JCodes.Framework.jCodesenum.BaseEnum.
            try
            {
                //this.txtItemType.Items.Clear();
                //this.txtItemType.Items.AddRange(DictItemUtil.GetDictByDictType("备件类别"));
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(Form1));
                MessageDxUtil.ShowError(ex.Message); 
            }
        }

        private void btnDistrict_Click(object sender, EventArgs e)
        {
            FrmCityDistrict dlg = new FrmCityDistrict();
            dlg.Show();
        }
    }
}
