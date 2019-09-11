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

namespace JCodes.Framework.AddIn._50Go
{
    public partial class FrmEditCouponCategory : BaseEditForm
    {
        public FrmEditCouponCategory()
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
                CouponCategoryInfo info = BLLFactory<CouponCategory>.Instance.FindByID(Id);
                if (info != null)
                {
                    txtHandNo.Text = info.GeneralCode;
                    txtName.Text = info.Name;
                    txtCompany.Value = info.CompanyLst;
                    txtEnabled.SelectedIndex = info.IsForbid;
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
            CouponCategoryInfo info = BLLFactory<CouponCategory>.Instance.FindByID(Id);
            if (info != null)
            {
                SetInfo(info);

                try
                {
                    #region 更新数据
                    bool succeed = BLLFactory<CouponCategory>.Instance.Update(info, Id);
                    if (succeed)
                    {
                        //可添加其他关联操作

                        return true;
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmEditCouponCategory));
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
            string condition = string.Format("UserCode ='{0}' ", txtHandNo.Text.Trim());
            bool exist = BLLFactory<CouponCategory>.Instance.IsExistRecord(condition);
            if (exist)
            {
                MessageDxUtil.ShowTips("指定的【分类编码】已经存在，请修改");
                return false;
            }

            CouponCategoryInfo couponCategoryInfo = new CouponCategoryInfo();
            SetInfo(couponCategoryInfo);

            try
            {
                #region 新增数据
                bool succeed = BLLFactory<CouponCategory>.Instance.Insert(couponCategoryInfo);
                if (succeed)
                {
                    return true;
                }
                #endregion
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmEditCouponCategory));
                MessageDxUtil.ShowError(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// 编辑或者保存状态下取值函数
        /// </summary>
        /// <param name="info"></param>
        private void SetInfo(CouponCategoryInfo Info)
        {
            // 如果没有ID值则为新增
            if (Info.Id > 0)
            {
                Info.CreatorId = Portal.gc.UserInfo.Id;
                Info.CreatorTime = DateTimeHelper.GetServerDateTime2();
            }
            Info.GeneralCode = txtHandNo.Text.Trim();
            Info.Name = txtName.Text.Trim();
            Info.CompanyLst = txtCompany.Value;
            Info.IsForbid = (short)txtEnabled.SelectedIndex;
            if (Info.Id > 0)
            {
                Info.EditorId = Portal.gc.UserInfo.Id;
                Info.LastUpdateTime = DateTimeHelper.GetServerDateTime2();
            }
        }

        /// <summary>
        /// 实现控件输入检查的函数
        /// </summary>
        /// <returns></returns>
        public override bool CheckInput()
        {
            bool result = true;//默认是可以通过

            #region MyRegion

            if (this.txtHandNo.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowWarning(Const.MsgCheckInput + lblHandNo.Text.Replace(Const.MsgCheckSign, string.Empty));
                ClearRedColor();
                lblHandNo.AppearanceItemCaption.ForeColor = Color.Red;
                this.txtHandNo.Focus();
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
            else if (this.txtCompany.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowWarning(Const.MsgCheckInput + lblCompany.Text.Replace(Const.MsgCheckSign, string.Empty));
                ClearRedColor();
                lblCompany.AppearanceItemCaption.ForeColor = Color.Red;
                this.txtCompany.Focus();
                result = false;
            }
            else if (this.txtEnabled.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowWarning(Const.MsgCheckInput + lblEnabled.Text.Replace(Const.MsgCheckSign, string.Empty));
                ClearRedColor();
                lblEnabled.AppearanceItemCaption.ForeColor = Color.Red;
                this.txtEnabled.Focus();
                result = false;
            }
            else if (this.txtName.Text.Contains(Const.Minus))
            {
                MessageDxUtil.ShowWarning("分类名称中不允许输入字符-");
                ClearRedColor();
                lblName.AppearanceItemCaption.ForeColor = Color.Red;
                this.txtName.Focus();
                result = false;
            }
            #endregion

            return result;
        }

        private void ClearRedColor()
        {
            lblHandNo.AppearanceItemCaption.ForeColor = Color.FromArgb(0, 0, 0, 0);
            lblCompany.AppearanceItemCaption.ForeColor = Color.FromArgb(0, 0, 0, 0);
            lblEnabled.AppearanceItemCaption.ForeColor = Color.FromArgb(0, 0, 0, 0);
            lblName.AppearanceItemCaption.ForeColor = Color.FromArgb(0, 0, 0, 0);
        }
    }
}
