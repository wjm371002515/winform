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
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JCodes.Framework.AddIn.UI.Dictionary
{
    public partial class FrmEditCity : BaseEditForm
    {        
        public FrmEditCity()
        {
            InitializeComponent();
        }

        public override bool CheckInput()
        {
            bool result = true;//默认是可以通过
            #region MyRegion
            if (this.txtCity.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowWarning(Const.MsgCheckInput + lblCity.Text.Replace(Const.MsgCheckSign, string.Empty));
                this.txtCity.Focus();
                result = false;
            }

            if (this.txtZipCode.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowWarning(Const.MsgCheckInput + lblZipCode.Text.Replace(Const.MsgCheckSign, string.Empty));
                this.txtZipCode.Focus();
                result = false;
            }

            if (true)
            { 
                string strZipCode = txtZipCode.Text.Trim();
                if (!ValidateUtil.IsNumber(strZipCode))
                {
                    MessageDxUtil.ShowWarning(lblZipCode.Text.Replace(Const.MsgCheckSign, string.Empty) + Const.MsgErrFormatByNum);
                    this.txtZipCode.Focus();
                    result = false;
                }
            }
            #endregion

            return result;
        }
        public override void DisplayData()
        {
            if (!string.IsNullOrEmpty(ID))
            {
                this.Text = "编辑 " + this.Text;
                CityInfo info = BLLFactory<City>.Instance.FindByID(ID);
                if (info != null)
                {
                    this.txtCity.Text = info.CityName;
                }
            }
            else
            {
                this.Text = "新建 " + this.Text;
            }
            this.txtCity.Focus();
        }

        public override void ClearScreen()
        {
            txtCity.Text = string.Empty;
            txtZipCode.Text = string.Empty;
            base.ClearScreen();
        }

        public override bool SaveAddNew()
        {
            CityInfo info = new CityInfo();

            SetInfo(info);

            try
            {
                #region 新增数据

                bool succeed = BLLFactory<City>.Instance.Insert(info);
                if (succeed)
                {
                    //可添加其他关联操作

                    return true;
                }
                #endregion
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmEditCity));
                MessageDxUtil.ShowError(ex.Message);
            }
            return false;
        }

        public override bool SaveUpdated()
        {
            CityInfo info = new CityInfo();
            if (info != null)
            {
                SetInfo(info);
                try
                {
                    #region 更新数据
                    bool succeed = BLLFactory<City>.Instance.Update(info, ID);
                    if (succeed)
                    {
                        //可添加其他关联操作
                        return true;
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmEditCity));
                    MessageDxUtil.ShowError(ex.Message);
                }
            }
            return false;
        }

        private void SetInfo(CityInfo info)
        {
            info.CityName = this.txtCity.Text;
            info.ProvinceID = Convert.ToInt32(this.txtProvince.Tag.ToString());
            info.ZipCode = txtZipCode.Text;
            info.CurrentLoginUserId = LoginUserInfo.ID.ToString();
        }
    }
}
