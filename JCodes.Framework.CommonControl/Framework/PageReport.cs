using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using JCodes.Framework.CommonControl.Settings;
using System.Configuration;
using JCodes.Framework.Common;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.CommonControl.Other;

namespace JCodes.Framework.CommonControl.Framework
{
    public partial class PageReport : PropertyPage
    {
        private SettingsProvider settings;
        private ISettingsStorage store;

        public PageReport()
        {
            InitializeComponent();

            if(!this.DesignMode)
            {
                // PortableStorage: 在运行程序目录创建一个setting的文件记录参数数据
                // 20170513 wjm 暂时注释掉
                /*store = new PortableStorage();
                settings = new SettingsProvider(store);*/
            }
        }

        public override void OnInit()
        {
            // 20170513 wjm 暂时注释掉
            /*
            ReportParameter parameter = settings.GetSettings<ReportParameter>();
            if (parameter != null)
            {
                EnableOtherReport(false);
                string reportFile = parameter.CarSendReportFile;
                if (reportFile == "WHC.CarDispatch.CarSendBill2.rdlc")
                {
                    this.radReport.SelectedIndex = 0;                    
                }
                else if (reportFile == "WHC.CarDispatch.CarSendBill.rdlc")
                {
                    this.radReport.SelectedIndex = 1;
                }
                else
                {
                    EnableOtherReport(true);
                    this.radReport.SelectedIndex = 2;
                    this.txtOtherReport.Text = reportFile;
                }
            }
             * */
        }

        private void EnableOtherReport(bool enable)
        {
            this.lblOtherReport.Enabled = enable;
            this.txtOtherReport.Enabled = enable;
        }

        public override void OnSetActive()
        {
        }

        public override bool OnApply()
        {
            bool result = false;
            try
            {
                // 20170513 wjm 暂时注释掉
                /*
                ReportParameter parameter = settings.GetSettings<ReportParameter>();
                if (parameter != null)
                {                    
                    int otherType = 2;//2代表其他类型
                    if (this.radReport.SelectedIndex < otherType)
                    {
                        parameter.CarSendReportFile = this.radReport.Properties.Items[this.radReport.SelectedIndex].Value.ToString();
                    }
                    else
                    {
                        parameter.CarSendReportFile = this.txtOtherReport.Text;
                    }
                    settings.SaveSettings<ReportParameter>(parameter);
                }
                 * */
                result = true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(PageReport));
                MessageDxUtil.ShowError(ex.Message);
            }

            return result;
        }

        private void PageSetting_Load(object sender, EventArgs e)
        {

        }

        private void radReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            int otherType = 2;//2代表其他类型
            bool isOtherType = (this.radReport.SelectedIndex == otherType);
            EnableOtherReport(isOtherType);
        }
    }
}
