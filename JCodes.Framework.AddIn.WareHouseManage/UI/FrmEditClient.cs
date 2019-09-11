using System;
using System.Windows.Forms;
using JCodes.Framework.Entity;
using JCodes.Framework.Common;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Common.Databases;
using JCodes.Framework.BLL;

namespace JCodes.Framework.AddIn.WareHouseManage
{
    public partial class FrmEditClient : BaseEditForm
    {
        public FrmEditClient()
        {
            InitializeComponent();
            InitDictItem();
        }

        private void InitDictItem()
        {
        }

        private void SetInfo(ClientInfo info)
        {
            info.Code = txtCode.Text;
            info.Name = txtName.Text;
            info.Phone = txtPhone.Text;
            info.Address = txtAddress.Text;
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
            if (this.txtCode.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowWarning(Const.MsgCheckInput + lblCode.Text.Replace(Const.MsgCheckSign, string.Empty));
                txtCode.Focus();
                result = false;
            }
            if (this.txtName.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowWarning(Const.MsgCheckInput + lblName.Text.Replace(Const.MsgCheckSign, string.Empty));
                txtName.Focus();
                result = false;
            }
            #endregion

            return result;
        }

        public override void DisplayData()
        {
            if (Id > 0)
            {
                ClientInfo info = BLLFactory<Client>.Instance.FindByID(Id);
                if (info != null)
                {
                    txtCode.Text = info.Code;
                    txtName.Text = info.Name;
                    txtPhone.Text = info.Phone;
                    txtAddress.Text = info.Address;
                    txtNote.Text = info.Note;
                }
            }
        }

        /// <summary>
        /// 新增状态下的数据保存
        /// </summary>
        /// <returns></returns>
        public override bool SaveAddNew()
        {
            ClientInfo info = new ClientInfo();
            SetInfo(info);

            string where = GetConditionSql();
            var result = BLLFactory<Client>.Instance.Find(where);
            if (result.Count > 0)
            {
                MessageDxUtil.ShowError("此客户编码已存在，请确认!");
                return false;
            }

            try
            {
                bool succeed = BLLFactory<Client>.Instance.Insert(info);
                if (succeed)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmEditClient));
                MessageDxUtil.ShowError(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// 根据查询条件构造查询语句
        /// </summary> 
        private string GetConditionSql()
        {
            SearchCondition condition = new SearchCondition();

            condition.AddCondition("Code", txtCode.Text, SqlOperator.Equal);
            string where = condition.BuildConditionSql().Replace("Where", "");
            return where;
        }

         /// <summary>
        /// 编辑状态下的数据保存
        /// </summary>
        /// <returns></returns>
        public override bool SaveUpdated()
        {
            ClientInfo info = BLLFactory<Client>.Instance.FindByID(Id);
            if (info != null)
            {
                SetInfo(info);

                try
                {
                    bool succeed = BLLFactory<Client>.Instance.Update(info, info.ID.ToString());
                    if (succeed)
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmEditClient));
                    MessageDxUtil.ShowError(ex.Message);
                }
            }
            return false;
        }
    }
}
