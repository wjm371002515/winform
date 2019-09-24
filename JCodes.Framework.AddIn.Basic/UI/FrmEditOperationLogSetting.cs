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
using JCodes.Framework.Common.Format;

namespace JCodes.Framework.AddIn.Basic
{
    public partial class FrmEditOperationLogSetting : BaseEditForm
    {
        /// <summary>
        /// 创建一个临时对象，方便在附件管理中获取存在的GUID
        /// </summary>
        private OperationLogSettingInfo tempInfo = new OperationLogSettingInfo();

        public FrmEditOperationLogSetting()
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
            if (this.txtTableName.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowWarning(Const.MsgCheckInput + lblTableName.Text.Replace(Const.MsgCheckSign, string.Empty));
                this.txtTableName.Focus();
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
            this.txtTableName.Properties.BeginUpdate();
            this.txtTableName.Properties.Items.Clear();

            List<string> tableList = BLLFactory<OperationLogSetting>.Instance.GetTableNames();
            this.txtTableName.Properties.Items.AddRange(tableList.ToArray());
            this.txtTableName.Properties.EndUpdate();
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
                OperationLogSettingInfo info = BLLFactory<OperationLogSetting>.Instance.FindByID(Id);
                if (info != null)
                {
                    tempInfo = info;//重新给临时对象赋值，使之指向存在的记录对象

                    txtForbid.Checked = (info.IsForbid == 0);
                    txtTableName.Text = info.TableName;
                    txtInsertLog.Checked = (info.IsInsertLog ==0);
                    txtDeleteLog.Checked = (info.IsDeleteLog == 0);
                    txtUpdateLog.Checked = (info.IsUpdateLog == 0);
                    txtNote.Text = info.Remark;
                    txtCreator.Text = info.CreatorId.ToString();
                    txtCreateTime.SetDateTime(info.CreatorTime);
                    txtEditor.Text = info.EditorId.ToString();
                    txtEditTime.SetDateTime(info.LastUpdateTime);
                }
                #endregion            
            }
            else
            {
                txtCreateTime.DateTime = DateTimeHelper.GetServerDateTime2(); //默认当前时间
                txtCreator.Text = Portal.gc.UserInfo.FullName;//默认为当前登录用户
                txtEditor.Text = Portal.gc.UserInfo.FullName;//默认为当前登录用户
                txtEditTime.DateTime = DateTimeHelper.GetServerDateTime2(); //默认当前时间 
            }
        }


        public override void ClearScreen()
        {
            this.tempInfo = new OperationLogSettingInfo();
            base.ClearScreen();
        }

        /// <summary>
        /// 编辑或者保存状态下取值函数
        /// </summary>
        /// <param name="info"></param>
        private void SetInfo(OperationLogSettingInfo info)
        {
            info.IsForbid = (short)(txtForbid.Checked ? 0 : 1);
            info.TableName = txtTableName.Text;
            info.IsInsertLog = (short)(txtInsertLog.Checked ? 0 : 1);
            info.IsDeleteLog = (short)(txtDeleteLog.Checked ? 0 : 1);
            info.IsUpdateLog = (short)(txtUpdateLog.Checked ? 0 : 1);
            info.Remark = txtNote.Text;
            //info.Editor = Portal.gc.UserInfo.FullName;
            info.EditorId = Portal.gc.UserInfo.Id;
            info.LastUpdateTime = txtCreateTime.DateTime;

            info.CurrentLoginUserId = Portal.gc.UserInfo.Id;
        }

        /// <summary>
        /// 新增状态下的数据保存
        /// </summary>
        /// <returns></returns>
        public override bool SaveAddNew()
        {
            OperationLogSettingInfo info = tempInfo;//必须使用存在的局部变量，因为部分信息可能被附件使用
            SetInfo(info);
            //info.Creator = Portal.gc.UserInfo.FullName;
            info.CreatorId = Portal.gc.UserInfo.Id;
            info.CreatorTime = txtCreateTime.DateTime;

            try
            {
                #region 新增数据
                //检查是否还有其他相同关键字的记录
                bool exist = BLLFactory<OperationLogSetting>.Instance.IsExistKey("TableName", info.TableName);
                if (exist)
                {
                    MessageDxUtil.ShowTips("指定的【数据库表】已经存在，不能重复添加，请修改");
                    return false;
                }

                bool succeed = BLLFactory<OperationLogSetting>.Instance.Insert(info);
                if (succeed)
                {
                    //可添加其他关联操作

                    return true;
                }
                #endregion
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmEditOperationLogSetting));
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
            string condition = string.Format("TableName ='{0}' and ID <> '{1}' ", this.txtTableName.Text, Id);
            bool exist = BLLFactory<OperationLogSetting>.Instance.IsExistRecord(condition);
            if (exist)
            {
                MessageDxUtil.ShowTips("指定的【数据库表】已经存在，不能重复添加，请修改");
                return false;
            }

            OperationLogSettingInfo info = BLLFactory<OperationLogSetting>.Instance.FindByID(Id);
            if (info != null)
            {
                SetInfo(info);

                try
                {
                    #region 更新数据
                    bool succeed = BLLFactory<OperationLogSetting>.Instance.Update(info, info.Id);
                    if (succeed)
                    {
                        //可添加其他关联操作

                        return true;
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmEditOperationLogSetting));
                    MessageDxUtil.ShowError(ex.Message);
                }
            }
            return false;
        }
    }
}
