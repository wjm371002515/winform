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

            if (!string.IsNullOrEmpty(ID))
            {
                #region 显示信息
                OperationLogSettingInfo info = BLLFactory<OperationLogSetting>.Instance.FindByID(ID);
                if (info != null)
                {
                    tempInfo = info;//重新给临时对象赋值，使之指向存在的记录对象

                    txtForbid.Checked = info.Forbid;
                    txtTableName.Text = info.TableName;
                    txtInsertLog.Checked = info.InsertLog;
                    txtDeleteLog.Checked = info.DeleteLog;
                    txtUpdateLog.Checked = info.UpdateLog;
                    txtNote.Text = info.Note;
                    txtCreator.Text = info.Creator;
                    txtCreateTime.SetDateTime(info.CreateTime);
                    txtEditor.Text = info.Editor;
                    txtEditTime.SetDateTime(info.EditTime);
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
            info.Forbid = txtForbid.Checked;
            info.TableName = txtTableName.Text;
            info.InsertLog = txtInsertLog.Checked;
            info.DeleteLog = txtDeleteLog.Checked;
            info.UpdateLog = txtUpdateLog.Checked;
            info.Note = txtNote.Text;
            info.Editor = Portal.gc.UserInfo.FullName;
            info.Editor_ID = Portal.gc.UserInfo.ID.ToString();
            info.EditTime = txtCreateTime.DateTime;

            info.CurrentLoginUserId = Portal.gc.UserInfo.ID.ToString();
        }

        /// <summary>
        /// 新增状态下的数据保存
        /// </summary>
        /// <returns></returns>
        public override bool SaveAddNew()
        {
            OperationLogSettingInfo info = tempInfo;//必须使用存在的局部变量，因为部分信息可能被附件使用
            SetInfo(info);
            info.Creator = Portal.gc.UserInfo.FullName;
            info.Creator_ID = Portal.gc.UserInfo.ID.ToString();
            info.CreateTime = txtCreateTime.DateTime;

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
            string condition = string.Format("TableName ='{0}' and ID <> '{1}' ", this.txtTableName.Text, ID);
            bool exist = BLLFactory<OperationLogSetting>.Instance.IsExistRecord(condition);
            if (exist)
            {
                MessageDxUtil.ShowTips("指定的【数据库表】已经存在，不能重复添加，请修改");
                return false;
            }

            OperationLogSettingInfo info = BLLFactory<OperationLogSetting>.Instance.FindByID(ID);
            if (info != null)
            {
                SetInfo(info);

                try
                {
                    #region 更新数据
                    bool succeed = BLLFactory<OperationLogSetting>.Instance.Update(info, info.ID);
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
