using System;
using System.Text;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using JCodes.Framework.Entity;
using JCodes.Framework.Common;
using JCodes.Framework.BLL;
using JCodes.Framework.CommonControl;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.AddIn.Basic;
using JCodes.Framework.Common.Format;
using JCodes.Framework.jCodesenum;

namespace JCodes.Framework.TestWinForm
{
    public partial class FrmEditConsignment : BaseEditForm
    {
        public FrmEditConsignment()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 数据显示的函数
        /// </summary>
        public override void DisplayData()
        {
            if (Id > 0)
            {
                #region 显示客户信息
                ConsignmentInfo info = BLLFactory<Consignment>.Instance.FindById(Id);
                if (info != null)
                {
                    txtStrValue.Text = info.StrValue;
                    txtSysValue.Text = info.SysValue;
                    txtName.Text = info.Name;
                    ccbEnableStatus.EditValue = (Int32)info.EnableStatus;
                }
                #endregion            
            }
        }

        /// <summary>
        /// 编辑状态下的数据保存
        /// </summary>
        /// <returns></returns>
        public override bool SaveUpdated()
        {
            ConsignmentInfo info = BLLFactory<Consignment>.Instance.FindById(Id);
            if (info != null)
            {
                SetInfo(info);

                try
                {
                    #region 更新数据
                    Int32 succeed = BLLFactory<Consignment>.Instance.UpdateConsignmentById(info);
                    if (succeed > 0)
                    {
                        //可添加其他关联操作
                        MessageDxUtil.ShowTips("操作成功");
                        return true;
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmEditConsignment));
                    MessageDxUtil.ShowError(ex.Message);
                }
            }
            return false;
        }

        /// <summary>
        /// 新增状态下的数据保存
        /// </summary>
        /// <returns></returns>
        public override bool SaveAddNew()
        {
            //检查不同ID是否还有其他相同关键字的记录
            string condition = string.Format("StrValue ='{0}' ", txtStrValue.Text.Trim());
            bool exist = BLLFactory<Consignment>.Instance.IsExistRecord(condition);
            if (exist)
            {
                MessageDxUtil.ShowTips("指定的【小账号】已经存在，请修改");
                return false;
            }

            condition = string.Format("SysValue ='{0}' ", txtSysValue.Text.Trim());
            exist = BLLFactory<Consignment>.Instance.IsExistRecord(condition);
            if (exist && !string.Equals(txtSysValue.Text.Trim(), "无"))
            {
                MessageDxUtil.ShowTips("指定的【代销商号】已经存在，请修改");
                return false;
            }

            ConsignmentInfo consignmentInfo = new ConsignmentInfo();
            SetInfo(consignmentInfo);

            try
            {
                #region 新增数据
                Int32 succeed = BLLFactory<Consignment>.Instance.InsertConsignment(consignmentInfo);
                if (succeed > 0)
                {
                    MessageDxUtil.ShowTips("操作成功");
                    return true;
                }
                #endregion
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmEditConsignment));
                MessageDxUtil.ShowError(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// 编辑或者保存状态下取值函数
        /// </summary>
        /// <param name="info"></param>
        private void SetInfo(ConsignmentInfo info)
        {
            // 如果没有ID值则为新增
            if (info.Id == 0)
            {
                info.Id = BLLFactory<Consignment>.Instance.GetMaxId() + 1;
            }
            // 这里可以写日志 查看变更内容
            info.Name = txtName.Text.Trim();
            info.StrValue = txtStrValue.Text.Trim();
            info.SysValue = txtSysValue.Text.Trim();
            info.EnableStatus = (short)(ccbEnableStatus.SelectedDataRow as DicKeyValueInfo).DicttypeValue;
        }

        /// <summary>
        /// 实现控件输入检查的函数
        /// </summary>
        /// <returns></returns>
        public override bool CheckInput()
        {
            bool result = true;//默认是可以通过

            #region MyRegion

            if (this.txtStrValue.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowWarning(Const.MsgCheckInput + lblStrValue.Text.Replace(Const.MsgCheckSign, string.Empty));
                ClearRedColor();
                lblStrValue.AppearanceItemCaption.ForeColor = Color.Red;
                this.txtStrValue.Focus();
                result = false;
            }
            else if (this.txtSysValue.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowWarning(Const.MsgCheckInput + lblSysValue.Text.Replace(Const.MsgCheckSign, string.Empty));
                ClearRedColor();
                lblSysValue.AppearanceItemCaption.ForeColor = Color.Red;
                this.txtSysValue.Focus();
                result = false;
            }
            else if (this.txtName.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowWarning(Const.MsgCheckInput + lblName.Text.Replace(Const.MsgCheckSign, string.Empty));
                ClearRedColor();
                lblName.AppearanceItemCaption.ForeColor = Color.Red;
                this.txtName.Focus();
                result = false;
            }
            else if ((this.ccbEnableStatus.SelectedDataRow as DicKeyValueInfo).DicttypeValue == 0)
            {
                MessageDxUtil.ShowWarning(Const.MsgCheckInput + lblEnabled.Text.Replace(Const.MsgCheckSign, string.Empty));
                ClearRedColor();
                lblEnabled.AppearanceItemCaption.ForeColor = Color.Red;
                this.ccbEnableStatus.Focus();
                result = false;
            }
            #endregion

            return result;
        }

        private void ClearRedColor()
        {
            lblStrValue.AppearanceItemCaption.ForeColor = Color.FromArgb(0, 0, 0, 0);
            lblName.AppearanceItemCaption.ForeColor = Color.FromArgb(0, 0, 0, 0);
            lblEnabled.AppearanceItemCaption.ForeColor = Color.FromArgb(0, 0, 0, 0);
            lblSysValue.AppearanceItemCaption.ForeColor = Color.FromArgb(0, 0, 0, 0);
        }
    }
}
