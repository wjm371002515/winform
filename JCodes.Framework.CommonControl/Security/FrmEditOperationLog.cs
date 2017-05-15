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

namespace JCodes.Framework.CommonControl.Security
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

            #region MyRegion
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

            if (!string.IsNullOrEmpty(ID))
            {
                #region 显示信息
                OperationLogInfo info = BLLFactory<OperationLog>.Instance.FindByID(ID);
                if (info != null)
                {
                    tempInfo = info;//重新给临时对象赋值，使之指向存在的记录对象
                    txtUser_ID.Text = info.User_ID;
                    txtLoginName.Text = info.LoginName;
                    txtFullName.Text = info.FullName;
                    txtCompany_ID.Text = info.Company_ID;
                    txtCompanyName.Text = info.CompanyName;
                    txtTableName.Text = info.TableName;
                    txtOperationType.Text = info.OperationType;
                    txtNote.Text = info.Note;
                    txtIPAddress.Text = info.IPAddress;
                    txtMacAddress.Text = info.MacAddress;
                    txtCreateTime.SetDateTime(info.CreateTime);
                }
                #endregion
                //this.btnOK.Enabled = Portal.gc.HasFunction("OperationLog/Edit");             
            }
            else
            {
                //this.btnOK.Enabled = Portal.gc.HasFunction("OperationLog/Add");  
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
            info.LoginName = txtLoginName.Text;
            info.FullName = txtFullName.Text;
            info.CompanyName = txtCompanyName.Text;
            info.TableName = txtTableName.Text;
            info.OperationType = txtOperationType.Text;
            info.Note = txtNote.Text;
            info.MacAddress = this.txtMacAddress.Text;
            info.IPAddress = this.txtIPAddress.Text;
            info.CreateTime = txtCreateTime.DateTime;
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
                LogTextHelper.Error(ex);
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

            OperationLogInfo info = BLLFactory<OperationLog>.Instance.FindByID(ID);
            if (info != null)
            {
                SetInfo(info);

                try
                {
                    #region 更新数据
                    bool succeed = BLLFactory<OperationLog>.Instance.Update(info, info.ID);
                    if (succeed)
                    {
                        //可添加其他关联操作

                        return true;
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    LogTextHelper.Error(ex);
                    MessageDxUtil.ShowError(ex.Message);
                }
            }
            return false;
        }
    }
}
