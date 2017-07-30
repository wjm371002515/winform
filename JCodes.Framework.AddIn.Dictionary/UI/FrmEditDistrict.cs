using JCodes.Framework.BLL;
using JCodes.Framework.Common;
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

namespace JCodes.Framework.AddIn.Dictionary
{
    public partial class FrmEditDistrict : BaseEditForm
    {        
        public FrmEditDistrict()
        {
            InitializeComponent();
        }


        public override bool CheckInput()
        {
            bool result = true;//默认是可以通过
            #region MyRegion
            if (this.txtDistrict.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowWarning(Const.MsgCheckInput + lblDistrict.Text.Replace(Const.MsgCheckSign, string.Empty));
                this.txtDistrict.Focus();
                result = false;
            }
            #endregion

            return result;
        }
        public override void DisplayData()
        {
            if (!string.IsNullOrEmpty(ID))
            {
                this.Text = "编辑 " + this.Text;
                DistrictInfo info = BLLFactory<District>.Instance.FindByID(ID);
                if (info != null)
                {
                    this.txtDistrict.Text = info.DistrictName;
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
            txtDistrict.Text = string.Empty;
            base.ClearScreen();
        }

        public override bool SaveAddNew()
        {
            DistrictInfo info = new DistrictInfo();

            SetInfo(info);

            try
            {
                #region 新增数据

                bool succeed = BLLFactory<District>.Instance.Insert(info);
                if (succeed)
                {
                    //可添加其他关联操作

                    return true;
                }
                #endregion
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmEditDistrict));
                MessageDxUtil.ShowError(ex.Message);
            }
            return false;
        }

        public override bool SaveUpdated()
        {
            DistrictInfo info = new DistrictInfo();
            if (info != null)
            {
                SetInfo(info);
                try
                {
                    #region 更新数据
                    bool succeed = BLLFactory<District>.Instance.Update(info, ID);
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

        private void SetInfo(DistrictInfo info)
        {
            info.DistrictName = this.txtDistrict.Text;
            info.CityID = Convert.ToInt32(this.txtCity.Tag);

            info.CurrentLoginUserId = LoginUserInfo.ID.ToString();
        }

        private void FrmEditCityDistrict_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ID))
            {
                DistrictInfo info = BLLFactory<District>.Instance.FindByID(ID);
                if (info != null)
                {
                    this.txtDistrict.Text = info.DistrictName;
                }
            }
        }
    }
}
