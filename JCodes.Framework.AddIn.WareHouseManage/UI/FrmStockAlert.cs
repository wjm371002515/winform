using JCodes.Framework.BLL;
using JCodes.Framework.Common;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.CommonControl;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.Entity;
using JCodes.Framework.jCodesenum.BaseEnum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JCodes.Framework.AddIn.WareHouseManage
{
    public partial class FrmStockAlert : BaseDock
    {
        public string ID = string.Empty;
        public string ItemName = string.Empty;
        public string ItemNo = string.Empty;
        public string WareHouse = string.Empty;

        public FrmStockAlert()
        {
            InitializeComponent();
        }

        private void SetInfo(WareInfo info)
        {
            info.LowAmountWarning = Convert.ToInt32(txtLowWarning.Text);
            info.HighAmountWarning = Convert.ToInt32(txtHighWarning.Text);
        }

        private void FrmStockAlert_Load(object sender, EventArgs e)
        {
            //this.btnOK.Enabled = HasFunction("Stock/Setting");
            if (!string.IsNullOrEmpty(ID))
            {
                WareInfo info = BLLFactory<Stock>.Instance.FindByID(ID);
                if (info != null)
                {
                    txtLowWarning.Text = info.LowAmountWarning.ToString();
                    txtHighWarning.Text = info.HighAmountWarning.ToString();
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ID))
            {
                WareInfo info = BLLFactory<Stock>.Instance.FindByID(ID);
                if (info != null)
                {
                    SetInfo(info);

                    try
                    {
                        bool succeed = BLLFactory<Stock>.Instance.Update(info, info.Id);
                        if (succeed)
                        {
                            MessageDxUtil.ShowTips("保存成功");
                            this.DialogResult = DialogResult.OK;
                        }
                    }
                    catch (Exception ex)
                    {
                        LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmStockAlert));
                        MessageDxUtil.ShowError(ex.Message);
                    }
                }
            }
        }
    }
}
