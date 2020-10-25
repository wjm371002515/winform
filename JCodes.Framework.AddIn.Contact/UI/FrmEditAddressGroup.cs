using System;
using System.Text;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.Entity;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.BLL;
using JCodes.Framework.Common.Collections;
using JCodes.Framework.CommonControl.Controls;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.Common;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.Common.Format;
using JCodes.Framework.jCodesenum;

namespace JCodes.Framework.AddIn.Contact
{
    public partial class FrmEditAddressGroup : BaseEditForm
    {                
        /// <summary>
        /// 通讯录类型
        /// </summary>
        public AddressType addressType = AddressType.个人;

        /// <summary>
        /// 上级ID
        /// </summary>
        public string PID = "";

        public FrmEditAddressGroup()
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
            if (this.txtName.Text.Trim().Length == 0)
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
            //绑定下拉列表
            List<AddressGroupInfo> comboList = BLLFactory<AddressGroup>.Instance.GetAllWithAddressType(addressType, LoginUserInfo.Id);

            BLLFactory<AddressGroup>.Instance.GetAll();
            comboList = CollectionHelper<AddressGroupInfo>.Fill("-1", 0, comboList, "PID", "Id", "Name");
            this.txtPID.Properties.Items.Clear();
            foreach (AddressGroupInfo info in comboList)
            {
                this.txtPID.Properties.Items.Add(new CDicKeyValue(info.Id, info.Name));
            }
            this.txtPID.Properties.Items.Insert(0, new CDicKeyValue(-1, "无"));
            if (this.txtPID.Properties.Items.Count == 1)
            {
                this.txtPID.SelectedIndex = 0;
            }
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
                AddressGroupInfo info = BLLFactory<AddressGroup>.Instance.FindById(Id);
                if (info != null)
                {
                    txtPID.SetComboBoxItem(info.Pid);
                    txtSeq.Text = info.Seq;
                    txtName.Text = info.Name;
                    txtNote.Text = info.Remark;
                    txtEditor.Text = info.EditorId.ToString();
                    txtEditTime.SetDateTime(info.LastUpdateTime);
                }
                #endregion          
            }
            else
            {
                if (!string.IsNullOrEmpty(PID))
                {
                    txtPID.SetComboBoxItem(PID);
                }
                else
                {
                    //如果没有父菜单，则设置为无选项（第一个）
                    this.txtPID.SelectedIndex = 0;
                }
                this.txtEditTime.DateTime = DateTimeHelper.GetServerDateTime2(); //默认当前时间
                this.txtEditor.Text = LoginUserInfo.LoginName;//默认为当前登录用户 
            }
        }

        public override void ClearScreen()
        {
            this.txtEditTime.DateTime = DateTimeHelper.GetServerDateTime2(); //默认当前时间
            this.txtEditor.Text = LoginUserInfo.LoginName;//默认为当前登录用户 
            string pid = this.txtPID.GetComboBoxStrValue();

            base.ClearScreen();
            this.txtPID.SetComboBoxItem(pid);
        }

        /// <summary>
        /// 编辑或者保存状态下取值函数
        /// </summary>
        /// <param name="info"></param>
        private void SetInfo(AddressGroupInfo info)
        {
            info.Pid = txtPID.SelectedIndex;//txtPID.GetComboBoxStrValue();
            info.Seq = txtSeq.Text;
            info.Name = txtName.Text;
            info.Remark = txtNote.Text;

            info.LastUpdateTime = DateTimeHelper.GetServerDateTime2();
            info.EditorId = LoginUserInfo.Id;//当前用户
            info.CurrentLoginUserId = LoginUserInfo.Id; //记录当前登录的用户信息，供操作日志记录使用
        }
         
        /// <summary>
        /// 新增状态下的数据保存
        /// </summary>
        /// <returns></returns>
        public override bool SaveAddNew()
        {
            AddressGroupInfo info = new AddressGroupInfo();
            SetInfo(info);
            info.CreatorId = LoginUserInfo.Id;
            info.CreatorTime = DateTimeHelper.GetServerDateTime2();
            info.DeptId = LoginUserInfo.DeptId;
            info.CompanyId = LoginUserInfo.CompanyId;
            info.AddressType = this.addressType;

            try
            {
                #region 新增数据

                bool succeed = BLLFactory<AddressGroup>.Instance.Insert(info);
                if (succeed)
                {
                    //可添加其他关联操作

                    return true;
                }
                #endregion
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmEditAddressGroup));
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
            AddressGroupInfo info = BLLFactory<AddressGroup>.Instance.FindById(Id);
            if (info != null)
            {
                SetInfo(info);

                try
                {
                    #region 更新数据
                    bool succeed = BLLFactory<AddressGroup>.Instance.Update(info, info.Id);
                    if (succeed)
                    {
                        //可添加其他关联操作
                       
                        return true;
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmEditAddressGroup));
                    MessageDxUtil.ShowError(ex.Message);
                }
            }
           return false;
        }
    }
}
