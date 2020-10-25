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
using JCodes.Framework.Common.Framework;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.CommonControl.Controls;
using JCodes.Framework.jCodesenum;

namespace JCodes.Framework.AddIn.Basic
{
    public partial class FrmEditOperationLog : BaseEditForm
    {
        /// <summary>
        /// 创建一个临时对象，方便在附件管理中获取存在的GUID
        /// </summary>
        private OperationLogInfo tempInfo = new OperationLogInfo();

        public FrmEditOperationLog()
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
                #region 显示信息
                //OperationLogInfo info = BLLFactory<OperationLog>.Instance.FindByID(Id);
                //if (info != null)
                //{
                //    tempInfo = info;//重新给临时对象赋值，使之指向存在的记录对象
                //    txtUser_ID.Text = info.UserId.ToString();
                //    txtLoginName.Text = info.LoginName;
                //    txtLoginName.Text = info.LoginName;
                //    txtCompany_ID.Text = info.CompanyId.ToString();
                //    txtCompanyName.Text = info.CompanyName;
                //    txtTableName.Text = info.TableName;
                //    txtOperationType.Text = info.OperationType.ToString();
                //    txtNote.Text = info.Remark;
                //    txtIPAddress.Text = info.IP;
                //    txtMacAddress.Text = info.Mac;
                //    txtCreateTime.SetDateTime(info.CreatorTime);
                //}
                #endregion          
            }
        }

        public override void ClearScreen()
        {
            this.tempInfo = new OperationLogInfo();
            base.ClearScreen();
        }

        /// <summary>
        /// 编辑或者保存状态下取值函数
        /// </summary>
        /// <param name="info"></param>
        private void SetInfo(OperationLogInfo info)
        {
            info.Name = txtName.Text;
            info.LoginName = txtLoginName.Text;
            info.CompanyName = txtCompanyName.Text;
            info.TableName = txtTableName.Text;
            //info.OperationType = txtOperationType.Text;
            info.Remark = txtNote.Text;
            info.Mac = this.txtMacAddress.Text;
            info.IP = this.txtIPAddress.Text;
            info.CreatorTime = txtCreateTime.DateTime;
        }

        /// <summary>
        /// 新增状态下的数据保存
        /// </summary>
        /// <returns></returns>
        public override bool SaveAddNew()
        {
            OperationLogInfo info = tempInfo;//必须使用存在的局部变量，因为部分信息可能被附件使用
            SetInfo(info);

            try
            {
                #region 新增数据

                bool succeed = BLLFactory<OperationLog>.Instance.Insert(info);
                if (succeed)
                {
                    //可添加其他关联操作

                    return true;
                }
                #endregion
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmEditOperationLog));
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

            //OperationLogInfo info = BLLFactory<OperationLog>.Instance.FindByID(Id);
            //if (info != null)
            //{
            //    SetInfo(info);

            //    try
            //    {
            //        #region 更新数据
            //        bool succeed = BLLFactory<OperationLog>.Instance.Update(info, info.Id);
            //        if (succeed)
            //        {
            //            //可添加其他关联操作

            //            return true;
            //        }
            //        #endregion
            //    }
            //    catch (Exception ex)
            //    {
            //        LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmEditOperationLog));
            //        MessageDxUtil.ShowError(ex.Message);
            //    }
            //}
            return false;
        }
    }
}
