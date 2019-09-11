using JCodes.Framework.BLL;
using JCodes.Framework.Common;
using JCodes.Framework.Common.Format;
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

namespace JCodes.Framework.AddIn.Dictionary
{
    public partial class FrmEditDictType : BaseEditForm
    {
        public Int32 PID = -1;

        public FrmEditDictType()
        {
            InitializeComponent();
        }

        public override bool CheckInput()
        {
            bool result = true;//默认是可以通过
            #region MyRegion
            if (txtID.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowWarning(Const.MsgCheckInput + lblID.Text.Replace(Const.MsgCheckSign, string.Empty));
                this.txtID.Focus();
                result = false;
            }

            if (txtName.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowWarning(Const.MsgCheckInput + lblName.Text.Replace(Const.MsgCheckSign, string.Empty));
                txtName.Focus();
                result = false;
            }

            if (txtSeq.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowWarning(Const.MsgCheckInput + lblSeq.Text.Replace(Const.MsgCheckSign, string.Empty));
                txtSeq.Focus();
                result = false;
            }

            if (txtParent.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowWarning(Const.MsgCheckInput + lblParent.Text.Replace(Const.MsgCheckSign, string.Empty));
                txtParent.Focus();
                result = false;
            }

            string Id = txtID.Text;
            if (result)
            {
                if (!ValidateUtil.IsNumeric(Id))
                {
                    MessageDxUtil.ShowWarning(txtID.Text.Replace(Const.MsgCheckSign, string.Empty) + Const.MsgErrFormatByNum);
                    txtID.Focus();
                    result = false;
                }
            }


            if (result && !string.IsNullOrEmpty(Id))
            {
                Int32 NumId = Convert.ToInt32(Id);
                DictTypeInfo dictTypeInfo = BLLFactory<DictType>.Instance.FindByID(NumId);
                if (dictTypeInfo != null)
                {
                    MessageDxUtil.ShowTips(string.Format("已存在类别编号[{0}],类别名称[{1}]", dictTypeInfo.Id, dictTypeInfo.Name));
                    txtID.Focus();
                    result = false;
                }
            }

            #endregion

            return result;
        }

        /// <summary>
        /// 数据显示的函数
        /// </summary>
        public override void DisplayData()
        {
            DictTypeInfo parentInfo = BLLFactory<DictType>.Instance.FindByID(PID);
            if (parentInfo != null)
            {
                this.txtParent.Text = parentInfo.Name;
                this.txtParent.Tag = parentInfo.Name;
            }

            if (Id > 0)
            {
                DictTypeInfo info = BLLFactory<DictType>.Instance.FindByID(Id);
                if (info != null)
                {
                    this.txtID.Text = info.Id.ToString();
                    this.txtID.Enabled = false;
                    this.txtName.Text = info.Name;
                    this.txtNote.Text = info.Remark;
                    this.txtSeq.Text = info.Seq;

                    if (info.Pid == -1)
                    {
                        this.chkTopItem.Checked = true;
                    }
                }
            }
            this.txtName.Focus();
        }

        public override void ClearScreen()
        {
            this.txtID.Text = string.Empty;
            this.txtName.Text = string.Empty;
            this.txtNote.Text = string.Empty;
            this.txtSeq.Text = string.Empty;

            base.ClearScreen();
        }

        public override bool SaveAddNew()
        {
            DictTypeInfo info = new DictTypeInfo();

            SetInfo(info);

            try
            {
                #region 新增数据

                bool succeed = BLLFactory<DictType>.Instance.Insert(info);
                if (succeed)
                {
                    //可添加其他关联操作

                    return true;
                }
                #endregion
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmEditDictType));
                MessageDxUtil.ShowError(ex.Message);
            }
            return false;
        }

        public override bool SaveUpdated()
        {
            DictTypeInfo info = BLLFactory<DictType>.Instance.FindByID(Id);
            if (info != null)
            {
                SetInfo(info);
                try
                {
                    #region 更新数据
                    bool succeed = BLLFactory<DictType>.Instance.Update(info, info.Id);
                    if (succeed)
                    {
                        //可添加其他关联操作

                        return true;
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmEditDictType));
                    MessageDxUtil.ShowError(ex.Message);
                }
            }
            return false;
        }


        private void SetInfo(DictTypeInfo info)
        {
            info.Id = Convert.ToInt32(txtID.Text);
            info.EditorId = LoginUserInfo.Id;
            info.LastUpdated = DateTimeHelper.GetServerDateTime2();
            info.Name = this.txtName.Text.Trim();
            info.Remark = this.txtNote.Text.Trim();
            info.Seq = this.txtSeq.Text;
            info.Pid = PID;
            if (this.chkTopItem.Checked)
            {
                info.Pid = -1;
            }

            info.CurrentLoginUserId = LoginUserInfo.Id;
        }

        private void chkTopItem_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkTopItem.Checked || this.txtParent.Tag == null)
            {
                this.txtParent.Text = "无(顶级项目)";
            }
            else
            {
                this.txtParent.Text = this.txtParent.Tag.ToString();
            }
        }

    }
}
