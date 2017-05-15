using JCodes.Framework.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WHC.OrderWater.UI
{
    public partial class FrmEditCustomer : DevExpress.XtraEditors.XtraForm
    {
        public string ID = string.Empty;

        public FrmEditCustomer()
        {
            InitializeComponent();
        }

        //private void SetInfo(CustomerInfo info)
        //{
        //    info.Address = txtAddress.Text;
        //    info.Company = txtCompany.Text;
        //    info.Name = txtName.Text;
        //    info.Note = txtNote.Text;
        //    info.Number = txtNumber.Text;
        //    info.Telephone1 = txtTelephone1.Text;
        //    info.Telephone2 = txtTelephone2.Text;
        //    info.Telephone3 = txtTelephone3.Text;
        //    info.Telephone4 = txtTelephone4.Text;
        //    info.Telephone5 = txtTelephone5.Text;
        //    info.Area = cmbArea.Text;
        //    info.Type = cmbType.Text;
        //    info.LastUpdated = DateTime.Now;
        //    info.Shop_ID = "";
        //}


        private void btnOK_Click(object sender, EventArgs e)
        {
            #region 输入验证
            if (this.txtNumber.Text.Length == 0 && !string.IsNullOrEmpty(ID))
            {
                MessageUtil.ShowTips("客户编号不能为空");
                this.txtNumber.Focus();
                return;
            }
            else if (this.txtName.Text.Length == 0)
            {
                MessageUtil.ShowTips("客户名称不能为空");
                this.txtName.Focus();
                return;
            } 
            #endregion

            if (!string.IsNullOrEmpty(ID))
            {
                //CustomerInfo info = BLLFactory<Customer>.Instance.FindByID(ID);
                //if (info != null)
                //{
                //    SetInfo(info);

                //    try
                //    {
                //        bool succeed = BLLFactory<Customer>.Instance.Update(info, info.ID);
                //        MessageUtil.ShowTips("保存成功");
                //        this.DialogResult = DialogResult.OK;
                //    }
                //    catch (Exception ex)
                //    {
                //        LogHelper.Error(ex);
                //        MessageUtil.ShowError(ex.Message);
                //    }
                //}
            }
            else
            {
                //CustomerInfo info = new CustomerInfo();
                //SetInfo(info);
                //info.ID = Guid.NewGuid().ToString();

                //try
                //{
                //    bool succeed = BLLFactory<Customer>.Instance.Insert(info);
                //    MessageUtil.ShowTips("保存成功");
                //    this.DialogResult = DialogResult.OK;

                //}
                //catch (Exception ex)
                //{
                //    LogHelper.Error(ex);
                //    MessageUtil.ShowError(ex.Message);
                //}
            }
        }

        private void InitArea()
        {
            this.cmbArea.Items.Clear();
            //List<CustomerAreaInfo> areaList = BLLFactory<CustomerArea>.Instance.GetAll();
            //foreach (CustomerAreaInfo info in areaList)
            //{
            //    this.cmbArea.Items.Add(info.Area);
            //}
        }

        private void InitType()
        {
            this.cmbType.Items.Clear();
            //List<CustomerTypeInfo> typeList = BLLFactory<CustomerType>.Instance.GetAll();
            //foreach (CustomerTypeInfo info in typeList)
            //{
            //    this.cmbType.Items.Add(info.CustomerType);
            //}
        }

        /// <summary>
        /// 初始化客户财务相关信息
        /// </summary>
        private void InitTradeData(string customerNumber)
        {
            if (!string.IsNullOrEmpty(customerNumber))
            {
                //CustomerTradeInfo tradeInfo =
                //    BLLFactory<CustomerTrade>.Instance.GetCustomerTrade(customerNumber, Portal.gc.LoginInfo.Shop_ID);
                //if (tradeInfo != null)
                //{
                //    this.txtLeftMoney.Tag = tradeInfo.PayMoney.ToString("f2");//保存未变化的值，用来标识是否用户修改了
                //    this.txtLeftMoney.Text = tradeInfo.PayMoney.ToString("f2");
                //    this.txtDebtMoney.Text = tradeInfo.DebtMoney.ToString("f2");
                //    this.txtDebtTicket.Text = tradeInfo.DebtTicket.ToString();
                //    this.txtDebtTong.Text = tradeInfo.DebtGood.ToString();
                //    this.txtLeftTickets.Text = tradeInfo.LeftTickets.ToString();
                //    this.txtStayMoney.Text = tradeInfo.DepositMoney.ToString("f2");
                //}

                ////获取用户定购的数量和金额
                //this.txtTotalMoney.Text = BLLFactory<Order>.Instance.GetOrderMoney(customerNumber, Portal.gc.LoginInfo.Shop_ID).ToString("f2");
                //this.txtTotalQuantity.Text = BLLFactory<Order>.Instance.GetOrderQuantiy(customerNumber, Portal.gc.LoginInfo.Shop_ID).ToString();
            }
        }

        private void FrmEditCustomer_Load(object sender, EventArgs e)
        {
            InitArea();
            InitType();

            if (!string.IsNullOrEmpty(ID))
            {
                this.Text = "编辑 " + this.Text;
                //CustomerInfo info = BLLFactory<Customer>.Instance.FindByID(ID);
                //if (info != null)
                //{
                //    #region 显示客户信息
                //    txtAddress.Text = info.Address;
                //    txtCompany.Text = info.Company;
                //    txtName.Text = info.Name;
                //    txtNote.Text = info.Note;
                //    txtNumber.Text = info.Number;
                //    txtTelephone1.Text = info.Telephone1;
                //    txtTelephone2.Text = info.Telephone2;
                //    txtTelephone3.Text = info.Telephone3;
                //    txtTelephone4.Text = info.Telephone4;
                //    txtTelephone5.Text = info.Telephone5;
                //    cmbArea.Text = info.Area;
                //    cmbType.Text = info.Type;
                //    lblCreateDate.Text = info.CreateDate.ToString();

                //    InitTradeData(info.Number); 
                //    #endregion

                //    this.txtNumber.Visible = true;
                //}
            }
            else
            {
                this.Text = "新建 " + this.Text;
                lblCreateDate.Text = DateTime.Now.ToString();
                //根据分店代码和客户信息生成客户代码
                this.txtNumber.Text = "";//BLLFactory<Customer>.Instance.GenerateCustomerNumber(Portal.gc.LoginInfo.Shop_ID);
                //this.txtNumber.Visible = false;
            }
        }
    }
}
