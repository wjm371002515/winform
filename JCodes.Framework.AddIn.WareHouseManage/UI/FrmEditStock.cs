using JCodes.Framework.BLL;
using JCodes.Framework.Common;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.CommonControl;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.Entity;
using JCodes.Framework.jCodesenum;
using JCodes.Framework.jCodesenum.BaseEnum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace JCodes.Framework.AddIn.WareHouseManage
{
    public partial class FrmEditStock : BaseDock
    {
        public string ID = string.Empty;
        public string WareHouse = string.Empty;

        public FrmEditStock()
        {
            InitializeComponent();
        }

        private void FrmEditProduct_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ID))
            {
                WareInfo info = BLLFactory<Stock>.Instance.FindById(ID);
                if (info != null)
                {
                    try
                    {
                        ItemDetailInfo detailInfo = BLLFactory<ItemDetail>.Instance.FindByItemNo(info.ItemNo);
                        if (detailInfo != null)
                        {
                            txtItemNo.Text = info.ItemNo;
                            txtItemName.Text = info.Name;
                            txtStockQuantity.Text = info.Amount.ToString();
                            txtStockMoney.Text = (info.Amount * detailInfo.Price).ToString("f2");
                            txtHighWarning.Text = info.HighAmountWarning.ToString();
                            txtLowWarning.Text = info.LowAmountWarning.ToString();
                            txtNote.Text = info.Remark;
                            txtItemType.Text = info.ItemType.ToString();
                            txtBigType.Text = info.ItemBigtype.ToString();
                            txtManufacturer.Text = detailInfo.Manufacture;
                            txtMapNo.Text = detailInfo.MapNo;
                            txtSpecification.Text = detailInfo.Specification;
                            txtWareHouse.Text = info.WareHouseId.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmEditStock));
                        MessageDxUtil.ShowError(ex.Message);
                        return;
                    }
                }
                this.btnOK.Enabled = HasFunction("Stock/Modify");             
            }
            else
            {
                this.btnOK.Enabled = HasFunction("Stock/Modify");  
            }
        }

        private void SetInfo(WareInfo info)
        {
            //info.ItemNo = txtItemNo.Text;
            info.Amount = Convert.ToInt32(txtStockQuantity.Text);
            info.Balance = Convert.ToDouble(txtStockMoney.Text);
            info.LowAmountWarning = Convert.ToInt32(txtLowWarning.Text);
            info.HighAmountWarning = Convert.ToInt32(txtHighWarning.Text);
            info.Remark = txtNote.Text;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ID))
            {
                WareInfo info = BLLFactory<Stock>.Instance.FindById(ID);
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
                        LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmEditStock));
                        MessageDxUtil.ShowError(ex.Message);
                    }
                }
            }
            else
            {
                WareInfo info = new WareInfo();
                SetInfo(info);

                try
                {
                    bool succeed = BLLFactory<Stock>.Instance.Insert(info);
                    if (succeed)
                    {
                        MessageDxUtil.ShowTips("保存成功");
                        this.DialogResult = DialogResult.OK;
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmEditStock));
                    MessageDxUtil.ShowError(ex.Message);
                }
            }
        }

        private void txtStockQuantity_Validated(object sender, EventArgs e)
        {
            WareInfo info = BLLFactory<Stock>.Instance.FindById(ID);
            if (info != null)
            {
                try
                {
                    ItemDetailInfo detailInfo = BLLFactory<ItemDetail>.Instance.FindByItemNo(info.ItemNo);
                    if (detailInfo != null)
                    {
                        this.txtStockMoney.Text = (Convert.ToInt32(this.txtStockQuantity.Text) * detailInfo.Price).ToString("f2");
                        Application.DoEvents();
                        Thread.Sleep(10);
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmEditStock));
                    MessageDxUtil.ShowError(ex.Message);
                }
            }
        }
    }
}
