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
using System.Threading;
using System.Windows.Forms;

namespace JCodes.Framework.AddIn.UI.WareHouseManage
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
                this.Text = "编辑 " + this.Text;
                StockInfo info = BLLFactory<Stock>.Instance.FindByID(ID);
                if (info != null)
                {
                    try
                    {
                        ItemDetailInfo detailInfo = BLLFactory<ItemDetail>.Instance.FindByItemNo(info.ItemNo);
                        if (detailInfo != null)
                        {
                            txtItemNo.Text = info.ItemNo;
                            txtItemName.Text = info.ItemName;
                            txtStockQuantity.Text = info.StockQuantity.ToString();
                            txtStockMoney.Text = (info.StockQuantity * detailInfo.Price).ToString("f2");
                            txtHighWarning.Text = info.HighWarning.ToString();
                            txtLowWarning.Text = info.LowWarning.ToString();
                            txtNote.Text = info.Note;
                            txtItemType.Text = info.ItemType;
                            txtBigType.Text = info.ItemBigType;
                            txtManufacturer.Text = detailInfo.Manufacture;
                            txtMapNo.Text = detailInfo.MapNo;
                            txtSpecification.Text = detailInfo.Specification;
                            txtWareHouse.Text = info.WareHouse;
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
                this.Text = "新建 " + this.Text;
                this.btnOK.Enabled = HasFunction("Stock/Modify");  
            }
        }

        private void SetInfo(StockInfo info)
        {
            //info.ItemNo = txtItemNo.Text;
            info.StockQuantity = Convert.ToInt32(txtStockQuantity.Text);
            //info.StockMoney = Convert.ToDecimal(txtStockMoney.Text);
            info.LowWarning = Convert.ToInt32(txtLowWarning.Text);
            info.HighWarning = Convert.ToInt32(txtHighWarning.Text);
            info.Note = txtNote.Text;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ID))
            {
                StockInfo info = BLLFactory<Stock>.Instance.FindByID(ID);
                if (info != null)
                {
                    SetInfo(info);

                    try
                    {
                        bool succeed = BLLFactory<Stock>.Instance.Update(info, info.ID.ToString());
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
                StockInfo info = new StockInfo();
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
            StockInfo info = BLLFactory<Stock>.Instance.FindByID(ID);
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
