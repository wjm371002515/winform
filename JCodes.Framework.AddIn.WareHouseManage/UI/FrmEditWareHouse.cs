using System;
using System.Windows.Forms;
using JCodes.Framework.Entity;
using JCodes.Framework.Common;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.BLL;
using JCodes.Framework.Common.Extension;
using JCodes.Framework.jCodesenum;

namespace JCodes.Framework.AddIn.WareHouseManage
{
    public partial class FrmEditWareHouse : BaseEditForm
    {
        public FrmEditWareHouse()
        {
            InitializeComponent();
            InitDictItem();
        }

        private void InitDictItem()
        {
        }

        private void SetInfo(WareHouseInfo info)
        {
            info.Name = txtName.Text;
            info.Address = txtAddress.Text;
            info.UserId = txtManager.Text.ToInt32();
            info.MobilePhone = txtPhone.Text;
            info.Remark = txtNote.Text;
        }

        /// <summary>
        /// 实现控件输入检查的函数
        /// </summary>
        /// <returns></returns>
        public override bool CheckInput()
        {
            bool result = true;//默认是可以通过

            #region MyRegion
            if (this.txtName.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowWarning(Const.MsgCheckInput + lblName.Text.Replace(Const.MsgCheckSign, string.Empty));
                txtName.Focus();
                result = false;
            }
            if (this.txtManager.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowWarning(Const.MsgCheckInput + lblManager.Text.Replace(Const.MsgCheckSign, string.Empty));
                txtManager.Focus();
                result = false;
            }
            #endregion

            return result;
        }

        public override void DisplayData()
        {
            if (Id > 0)
            {
                WareHouseInfo info = BLLFactory<WareHouse>.Instance.FindById(Id);
                if (info != null)
                {
                    txtName.Text = info.Name;
                    txtAddress.Text = info.Address;
                    txtNote.Text = info.Remark;
                    txtManager.Text = info.UserId.ToString();
                    txtManager.Tag = info.UserId;
                    txtPhone.Text = info.MobilePhone;
                }
            }
        }

        /// <summary>
        /// 新增状态下的数据保存
        /// </summary>
        /// <returns></returns>
        public override bool SaveAddNew()
        {
            WareHouseInfo info = new WareHouseInfo();
            SetInfo(info);

            try
            {
                bool succeed = BLLFactory<WareHouse>.Instance.Insert(info);
                if (succeed)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmEditWareHouse));
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
            WareHouseInfo info = BLLFactory<WareHouse>.Instance.FindById(Id);
            if (info != null)
            {
                SetInfo(info);

                try
                {
                    bool succeed = BLLFactory<WareHouse>.Instance.Update(info, info.Id);
                    if (succeed)
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmEditWareHouse));
                    MessageDxUtil.ShowError(ex.Message);
                }
            }
            return false;
        }
    }
}
