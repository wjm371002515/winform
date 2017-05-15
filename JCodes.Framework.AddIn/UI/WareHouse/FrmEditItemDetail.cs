using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using JCodes.Framework.CommonControl;
using JCodes.Framework.Entity;
using JCodes.Framework.Common;
using JCodes.Framework.BLL;

namespace JCodes.Framework.AddIn.UI.WareHouse
{
    public partial class FrmEditItemDetail : BaseEditForm
    {
        public FrmEditItemDetail()
        {
            InitializeComponent();
            SetToolTips();
        }
                
        /// <summary>
        /// 实现控件输入检查的函数
        /// </summary>
        /// <returns></returns>
        public override bool CheckInput()
        {
            bool result = true;//默认是可以通过

            #region MyRegion

            if (this.txtItemNo.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowTips("请输入备件编号");
                this.txtItemNo.Focus();
                result = false;
            }
            else if (this.txtItemName.Text.Length == 0)
            {
                MessageDxUtil.ShowTips("备件名称不能为空，请输入");
                this.txtItemName.Focus();
                result = false;
            }
            else if (this.txtBigType.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowTips("请输入备件属类");
                this.txtBigType.Focus();
                result = false;
            }
            else if (this.txtItemType.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowTips("请输入备件类别");
                this.txtItemType.Focus();
                result = false;
            }
            else if (this.txtBelongWareHouse.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowTips("请选择所属库房");
                this.txtBelongWareHouse.Focus();
                result = false;
            }
            else if (this.txtBelongDept.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowTips("请选择所属部门");
                this.txtBelongDept.Focus();
                result = false;
            }
            #endregion

            return result;
        }

        /// <summary>
        /// 初始化数据字典
        /// </summary>
        private void InitDictItem()
        {
            this.txtManufacture.BindDictItems("供货商");
            this.txtBigType.BindDictItems("备件属类");
            this.txtItemType.BindDictItems("备件类别");
            this.txtUnit.BindDictItems("单位");
            this.txtSource.BindDictItems("来源");
            this.txtUsagePos.BindDictItems("使用位置");
            this.txtUsagePos.SelectedIndex = 0;
            this.txtBelongDept.BindDictItems("部门");

            //其他绑定方式
            this.txtBelongWareHouse.Properties.Items.Clear();
            this.txtBelongWareHouse.Properties.Items.AddRange(WareHouseHelper.GetWareHouse(LoginUserInfo.ID, LoginUserInfo.Name).ToArray());
            this.txtBelongWareHouse.SelectedIndex = 0;
        }                        

        /// <summary>
        /// 数据显示的函数
        /// </summary>
        public override void DisplayData()
        {
            InitDictItem();//数据字典加载（公用）

            if (!string.IsNullOrEmpty(ID))
            {
                #region 显示客户信息
                ItemDetailInfo info = BLLFactory<ItemDetail>.Instance.FindByID(ID);
                if (info != null)
                {
                    this.txtItemNo.Properties.ReadOnly = true;
                    this.txtItemNo.ForeColor = Color.Pink;

                    txtBigType.Text = info.ItemBigType;
                    txtItemName.Text = info.ItemName;
                    txtItemNo.Text = info.ItemNo;
                    txtItemType.Text = info.ItemType;
                    txtManufacture.Text = info.Manufacture;
                    txtMapNo.Text = info.MapNo;
                    txtMaterial.Text = info.Material;
                    txtNote.Text = info.Note;
                    txtPrice.Text = info.Price.ToString("f2");
                    txtSource.Text = info.Source;
                    txtSpecNumber.Text = info.Specification;
                    txtStockPos.Text = info.StoragePos;
                    txtUnit.Text = info.Unit;
                    txtUsagePos.Text = info.UsagePos;
                    txtBelongWareHouse.Text = info.WareHouse;
                    txtBelongWareHouse.Tag = info.WareHouse;//用来识别是否变化
                    txtBelongDept.Text = info.Dept;
                } 
                #endregion
                this.btnOK.Enabled = HasFunction("ItemDetail/Edit");             
            }
            else
            {
                this.btnOK.Enabled = HasFunction("ItemDetail/Add");  
            }
        }

        /// <summary>
        /// 编辑或者保存状态下取值函数
        /// </summary>
        /// <param name="info"></param>
        private void SetInfo(ItemDetailInfo info)
        {
            info.ItemBigType = txtBigType.Text;
            info.ItemName = txtItemName.Text;
            info.ItemNo = txtItemNo.Text;
            info.ItemType = txtItemType.Text;
            info.Manufacture = txtManufacture.Text;
            info.MapNo = txtMapNo.Text;
            info.Material = txtMaterial.Text;
            info.Note = txtNote.Text;
            info.Price = Convert.ToDecimal(txtPrice.Text);
            info.Source = txtSource.Text;
            info.Specification = txtSpecNumber.Text;
            info.StoragePos = txtStockPos.Text;
            info.Unit = txtUnit.Text;
            info.UsagePos = txtUsagePos.Text;
            info.Dept = txtBelongDept.Text;
            info.WareHouse = txtBelongWareHouse.Text;
        }
         
        /// <summary>
        /// 新增状态下的数据保存
        /// </summary>
        /// <returns></returns>
        public override bool SaveAddNew()
        {
            ItemDetailInfo info = new ItemDetailInfo();
            SetInfo(info);

            try
            {
                #region 新增数据
                bool exist = BLLFactory<ItemDetail>.Instance.IsExistKey("ItemNo", info.ItemNo);
                if (exist)
                {
                    MessageDxUtil.ShowTips("指定的备件编号已经存在，不能重复添加，请修改");
                    return false;
                }

                bool succeed = BLLFactory<ItemDetail>.Instance.Insert(info);
                if (succeed)
                {
                    try
                    {
                        //不管是更新还是新增，如果对应的备件编码在库房没有初始化，初始化之
                        bool isInit = BLLFactory<Stock>.Instance.CheckIsInitedWareHouse(this.txtBelongWareHouse.Text, this.txtItemNo.Text);
                        if (!isInit)
                        {
                            BLLFactory<Stock>.Instance.InitStockQuantity(info, 0, this.txtBelongWareHouse.Text);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageDxUtil.ShowTips(string.Format("初始化库存为0失败：", ex.Message));
                    }

                    return true;
                    //MessageDxUtil.ShowTips("备件数据保存成功");
                    //this.DialogResult = DialogResult.OK;
                }
                #endregion
            }
            catch (Exception ex)
            {
                LogTextHelper.Error(ex);
                MessageDxUtil.ShowError(ex.Message);
            }
            return false;
        }                 

        /// <summary>
        /// 编辑状态下的数据保存
        /// </summary>
        /// <returns></returns>
        public override bool SaveUpdated()
        {
            bool exist = BLLFactory<ItemDetail>.Instance.CheckExist(this.txtItemNo.Text, ID);
            if (exist)
            {
                MessageDxUtil.ShowTips("指定的备件编号已经存在，不能重复添加，请修改");
                return false;
            }

            ItemDetailInfo info = BLLFactory<ItemDetail>.Instance.FindByID(ID);
            if (info != null)
            {
                if (txtBelongWareHouse.Text != txtBelongWareHouse.Tag.ToString())
                {
                    if (MessageDxUtil.ShowYesNoAndWarning("您的备件所属库存发生了修改，是否继续更改库房数据") == DialogResult.No)
                    {
                        return false;
                    }
                }

                SetInfo(info);

                try
                {
                    #region 更新数据
                    bool succeed = BLLFactory<ItemDetail>.Instance.Update(info, info.ID.ToString());
                    if (succeed)
                    {
                        try
                        {
                            StockInfo stockInfo = BLLFactory<Stock>.Instance.FindByItemNo(this.txtItemNo.Text, this.txtBelongWareHouse.Tag.ToString());
                            if (stockInfo != null)
                            {
                                stockInfo.WareHouse = txtBelongWareHouse.Text;
                                BLLFactory<Stock>.Instance.Update(stockInfo, stockInfo.ID.ToString());
                            }

                            //不管是更新还是新增，如果对应的备件编码在库房没有初始化，初始化之
                            bool isInit = BLLFactory<Stock>.Instance.CheckIsInitedWareHouse(this.txtBelongWareHouse.Text, this.txtItemNo.Text);
                            if (!isInit)
                            {
                                BLLFactory<Stock>.Instance.InitStockQuantity(info, 0, this.txtBelongWareHouse.Text);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageDxUtil.ShowTips(string.Format("初始化库存为0失败：", ex.Message));
                        }
                        //MessageDxUtil.ShowTips("备件数据保存成功");
                        //this.DialogResult = DialogResult.OK;
                        return true;
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    LogTextHelper.Error(ex);
                    MessageDxUtil.ShowError(ex.Message);
                }
            }
           return false;
        }

        private void SetToolTips()
        {
            ToolTip tip = new ToolTip();
            tip.ToolTipTitle = "备件编码说明";
            tip.UseAnimation = true;
            tip.IsBalloon = true;
            string tipsContent = "备件以备件编码作为唯一的依据。备件编码是以公司原来的PM系统中的备件编码为蓝本的，如果出现库房a和库房b有相同的备件名称的备件的话，则输入备件编码的时候在备件编码后面+a、b、c之类的字母加以区分。";
            tip.ShowAlways = true;
            tip.SetToolTip(this.pictureBox1, tipsContent);
        }
    }
}
