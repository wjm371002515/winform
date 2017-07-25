using System;
using System.Text;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.Net;
using JCodes.Framework.Entity;
using JCodes.Framework.Common;
using JCodes.Framework.BLL;
using JCodes.Framework.CommonControl;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.AddIn.Other;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.Common.Format;
using JCodes.Framework.Common.Extension;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.AddIn.UI.Security;

namespace JCodes.Framework.AddIn.UI._50Go
{
    public partial class FrmGenerateCoupon : BaseEditForm
    {
        public FrmGenerateCoupon()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 实现控件输入检查的函数
        /// </summary>
        /// <returns></returns>
        public override bool CheckInput()
        {
            bool result = true;//默认是可以通过

            #region MyRegion
            if (this.txtCategory.Text.Length == 0)
            {
                MessageDxUtil.ShowWarning(Const.MsgCheckInput + lblCategory.Text.Replace(Const.MsgCheckSign, string.Empty));
                this.txtCategory.Focus();
                result = false;
            }
            else if (this.txtStartTime.Text.Length == 0)
            {
                MessageDxUtil.ShowWarning(Const.MsgCheckInput + lblStartTime.Text.Replace(Const.MsgCheckSign, string.Empty));
                this.txtStartTime.Focus();
                result = false;
            }
            else if (this.txtEndTime.Text.Length == 0)
            {
                MessageDxUtil.ShowWarning(Const.MsgCheckInput + lblEndTime.Text.Replace(Const.MsgCheckSign, string.Empty));
                this.txtEndTime.Focus();
                result = false;
            }
            else if (this.txtMobilePhone.Text.Length == 0)
            {
                MessageDxUtil.ShowWarning(Const.MsgCheckInput + lblMobilePhone.Text.Replace(Const.MsgCheckSign, string.Empty));
                this.txtMobilePhone.Focus();
                result = false;
            }
            else if (this.txtFullName.Text.Length == 0)
            {
                MessageDxUtil.ShowWarning(Const.MsgCheckInput + lblFullName.Text.Replace(Const.MsgCheckSign, string.Empty));
                this.txtFullName.Focus();
                result = false;
            }
            else if (this.txtEnabled.Text.Length == 0)
            {
                MessageDxUtil.ShowWarning(Const.MsgCheckInput + lblEnabled.Text.Replace(Const.MsgCheckSign, string.Empty));
                this.txtEnabled.Focus();
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
            // 初始化类别 
            this.txtCategory.Properties.Items.Clear();
            List<CouponCategoryInfo> lst = BLLFactory<CouponCategory>.Instance.GetAllCouponCategory();
            foreach (var couponCategory in lst)
            {
                this.txtCategory.Properties.Items.Add(new CListItem(couponCategory.HandNo + "-" + couponCategory.Name, couponCategory.ID));
            }
        }

        /// <summary>
        /// 数据显示的函数
        /// </summary>
        public override void DisplayData()
        {
            InitDictItem();//数据字典加载（公用）

            if (!string.IsNullOrEmpty(ID))
            {
                #region 显示信息
                CouponInfo info = BLLFactory<Coupon>.Instance.FindByID(ID);
                if (info != null)
                {
                    txtID.Text = ID;
                    CouponCategoryInfo info2 = BLLFactory<CouponCategory>.Instance.FindByID(info.CouponCategory_ID);
                    if (info2 != null)
                    {
                        txtCategory.SelectedText = info2.HandNo + "-" + info2.Name;
                    }
                    txtMobilePhone.Text = info.MobilePhone;
                    txtFullName.Text = info.FullName;
                    txtEndTime.DateTime = info.EndTime;
                    txtStartTime.DateTime = info.StartTime;
                    txtEnabled.SelectedIndex = info.DELETED;
                    txtCreator.Text = info.Creator;
                    txtCreateTime.SetDateTime(info.CreateTime);
                }
                #endregion           
            }
            else
            {
                txtCreator.Text = Portal.gc.UserInfo.FullName;  //默认为当前登录用户
                txtCreateTime.DateTime = DateTime.Now;          //默认当前时间
                txtID.Text = Guid.NewGuid().ToString();
            }
        }

        public override void ClearScreen()
        {
            base.ClearScreen();
        }

        /// <summary>
        /// 编辑或者保存状态下取值函数
        /// </summary>
        /// <param name="info"></param>
        private void SetInfo(CouponInfo info)
        {
            info.ID = txtID.Text.Trim();
            if (string.IsNullOrEmpty(ID))
            {
                info.Creator = Portal.gc.UserInfo.FullName;
                info.Creator_ID = Portal.gc.UserInfo.ID.ToString();
                info.CreateTime = DateTime.Now;
            }
            info.CouponCategory_ID = (txtCategory.SelectedItem as CListItem).Value;
            CouponCategoryInfo couponCategoryInfo = BLLFactory<CouponCategory>.Instance.FindByID(info.CouponCategory_ID);
            if (couponCategoryInfo != null)
            {
                info.CouponCategory_Name = couponCategoryInfo.Name;
                info.Company_ID = couponCategoryInfo.BelongCompanys;
                OUInfo ouInfo = BLLFactory<OU>.Instance.FindByID(info.Company_ID);
                info.Company_Name = ouInfo.Name;
            }
           
            info.MobilePhone = txtMobilePhone.Text;
            info.FullName = txtFullName.Text;
            info.StartTime = txtStartTime.DateTime;
            info.EndTime = txtEndTime.DateTime;
            info.DELETED = txtEnabled.SelectedIndex;
        }

        /// <summary>
        /// 新增状态下的数据保存
        /// </summary>
        /// <returns></returns>
        public override bool SaveAddNew()
        {
            CouponInfo info = new CouponInfo();//必须使用存在的局部变量，因为部分信息可能被附件使用
            SetInfo(info);
            try
            {
                #region 新增数据

                bool succeed = BLLFactory<Coupon>.Instance.Insert(info);
                if (succeed)
                {
                    //可添加其他关联操作

                    return true;
                }
                #endregion
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmGenerateCoupon));
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

            CouponInfo info = BLLFactory<Coupon>.Instance.FindByID(ID);
            if (info != null)
            {
                SetInfo(info);

                try
                {
                    #region 更新数据
                    bool succeed = BLLFactory<Coupon>.Instance.Update(info, info.ID);
                    if (succeed)
                    {
                        //可添加其他关联操作

                        return true;
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmGenerateCoupon));
                    MessageDxUtil.ShowError(ex.Message);
                }
            }
            return false;
        }
    }
}
