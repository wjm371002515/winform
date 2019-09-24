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
using JCodes.Framework.CommonControl.Controls;
using JCodes.Framework.AddIn.Basic;

namespace JCodes.Framework.AddIn.Security
{
    public partial class FrmEditSystemType : BaseEditForm
    {
        public FrmEditSystemType()
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

            if (this.txtOid.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowWarning(Const.MsgCheckInput + lblOid.Text.Replace(Const.MsgCheckSign, string.Empty));
                this.txtOid.Focus();
                result = false;
            }
            else if (this.txtName.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowWarning(Const.MsgCheckInput + lblName.Text.Replace(Const.MsgCheckSign, string.Empty));
                this.txtName.Focus();
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
            //初始化代码
        }

        /// <summary>
        /// 数据显示的函数
        /// </summary>
        public override void DisplayData()
        {
            InitDictItem();//数据字典加载（公用）

            if (Id > 0)
            {
                #region 显示客户信息
                SystemTypeInfo info = BLLFactory<SystemType>.Instance.FindByID(Id);
                if (info != null)
                {
                    this.txtOid.Text = info.Gid;
                    txtName.Text = info.Name;
                    txtCustomID.Text = info.ConsumerCode;
                    txtAuthorize.Text = info.Licence;
                    txtNote.Text = info.Remark;

                    this.txtOid.Enabled = false;
                }
                #endregion          
            }
            else
            {                 
            }
        }

        /// <summary>
        /// 编辑或者保存状态下取值函数
        /// </summary>
        /// <param name="info"></param>
        private void SetInfo(SystemTypeInfo info)
        {
            info.Name = txtName.Text;
            info.ConsumerCode = txtCustomID.Text;
            info.Licence = txtAuthorize.Text;
            info.Remark = txtNote.Text;
            info.CurrentLoginUserId = Portal.gc.UserInfo.Id;
        }

        /// <summary>
        /// 新增状态下的数据保存
        /// </summary>
        /// <returns></returns>
        public override bool SaveAddNew()
        {
            SystemTypeInfo info = new SystemTypeInfo();
            SetInfo(info);

            try
            {
                #region 新增数据
                //检查是否还有其他相同关键字的记录
                bool exist = BLLFactory<SystemType>.Instance.IsExistKey("Gid", info.Gid);
                if (exist)
                {
                    MessageDxUtil.ShowTips("指定的【系统标识】已经存在，不能重复添加，请修改");
                    return false;
                }

                bool succeed = BLLFactory<SystemType>.Instance.Insert(info);
                if (succeed)
                {
                    //可添加其他关联操作

                    return true;
                }
                #endregion
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmEditSystemType));
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
            //检查不同ID是否还有其他相同关键字的记录
            string condition = string.Format("Name ='{0}' and OID <> '{1}' ", this.txtName.Text, Id);
            bool exist = BLLFactory<SystemType>.Instance.IsExistRecord(condition);
            if (exist)
            {
                MessageDxUtil.ShowTips("指定的【系统名称】已经存在，不能重复添加，请修改");
                return false;
            }

            SystemTypeInfo info = BLLFactory<SystemType>.Instance.FindByID(Id);
            if (info != null)
            {
                SetInfo(info);

                try
                {
                    #region 更新数据
                    bool succeed = BLLFactory<SystemType>.Instance.Update(info, info.Gid);
                    if (succeed)
                    {
                        //可添加其他关联操作

                        return true;
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmEditSystemType));
                    MessageDxUtil.ShowError(ex.Message);
                }
            }
            return false;
        }
    }
}
