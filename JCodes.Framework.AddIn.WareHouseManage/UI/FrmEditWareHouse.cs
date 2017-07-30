using System;
using System.Windows.Forms;
using JCodes.Framework.Entity;
using JCodes.Framework.Common;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.BLL;

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
            info.Manager = txtManager.Text;
            info.Phone = txtPhone.Text;
            info.Note = txtNote.Text;
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
            if (!string.IsNullOrEmpty(ID))
            {
                WareHouseInfo info = BLLFactory<WareHouse>.Instance.FindByID(ID);
                if (info != null)
                {
                    txtName.Text = info.Name;
                    txtAddress.Text = info.Address;
                    txtNote.Text = info.Note;
                    txtManager.Text = info.Manager;
                    txtManager.Tag = info.Manager;
                    txtPhone.Text = info.Phone;
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
            WareHouseInfo info = BLLFactory<WareHouse>.Instance.FindByID(ID);
            if (info != null)
            {
                SetInfo(info);

                try
                {
                    bool succeed = BLLFactory<WareHouse>.Instance.Update(info, info.ID.ToString());
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
