using System;
using System.Text;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.Entity;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.BLL;
using JCodes.Framework.CommonControl.Controls;
using JCodes.Framework.Common;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.Common.Format;

namespace JCodes.Framework.AddIn.Contact
{
    public partial class FrmEditAddress : BaseEditForm
    {
        /// <summary>
        /// 通讯录类型
        /// </summary>
        public AddressType AddressType = AddressType.个人;

        /// <summary>
        /// 创建一个临时对象，方便在附件管理中获取存在的GUID
        /// </summary>
        private AddressInfo tempInfo = new AddressInfo();

        public FrmEditAddress()
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
                AddressInfo info = BLLFactory<Address>.Instance.FindByID(ID);
                if (info != null)
                {
                    tempInfo = info;//重新给临时对象赋值，使之指向存在的记录对象

                    txtName.Text = info.Name;
                    txtSex.Text = info.Sex;
                    txtBirthdate.SetDateTime(info.Birthdate);
                    txtMobile.Text = info.Mobile;
                    txtEmail.Text = info.Email;
                    txtQQ.Text = info.QQ;
                    txtHomeTelephone.Text = info.HomeTelephone;
                    txtOfficeTelephone.Text = info.OfficeTelephone;
                    txtHomeAddress.Text = info.HomeAddress;
                    txtOfficeAddress.Text = info.OfficeAddress;
                    txtFax.Text = info.Fax;
                    txtCompany.Text = info.Company;
                    txtDept.Text = info.Dept;
                    txtPosition.Text = info.Position;
                    txtOther.Text = info.Other;
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
                txtCreator.Text = LoginUserInfo.FullName;//默认为当前登录用户
                txtCreateTime.DateTime = DateTime.Now; //默认当前时间
                txtEditor.Text = LoginUserInfo.FullName;//默认为当前登录用户
                txtEditTime.DateTime = DateTime.Now; //默认当前时间
            }

            BindAddressGroup();

            //tempInfo在对象存在则为指定对象，新建则是全新的对象，但有一些初始化的GUID用于附件上传
            //SetAttachInfo(tempInfo);
        }

        //private void SetAttachInfo(AddressInfo info)
        //{
        //    this.attachmentGUID.AttachmentGUID = info.AttachGUID;
        //    this.attachmentGUID.userId = LoginUserInfo.Name;

        //    string name = txtName.Text;
        //    if (!string.IsNullOrEmpty(name))
        //    {
        //        string dir = string.Format("{0}", name);
        //        this.attachmentGUID.Init(dir, tempInfo.ID, LoginUserInfo.Name);
        //    }
        //}

        public override void ClearScreen()
        {
            this.tempInfo = new AddressInfo();
            base.ClearScreen();
        }

        /// <summary>
        /// 编辑或者保存状态下取值函数
        /// </summary>
        /// <param name="info"></param>
        private void SetInfo(AddressInfo info)
        {
            info.Name = txtName.Text;
            info.Sex = txtSex.Text;
            info.Birthdate = txtBirthdate.DateTime;
            info.Mobile = txtMobile.Text;
            info.Email = txtEmail.Text;
            info.QQ = txtQQ.Text;
            info.HomeTelephone = txtHomeTelephone.Text;
            info.OfficeTelephone = txtOfficeTelephone.Text;
            info.HomeAddress = txtHomeAddress.Text;
            info.OfficeAddress = txtOfficeAddress.Text;
            info.Fax = txtFax.Text;
            info.Company = txtCompany.Text;
            info.Dept = txtDept.Text;
            info.Position = txtPosition.Text;
            info.Other = txtOther.Text;
            info.Note = txtNote.Text;

            info.EditTime = DateTime.Now;
            info.Editor = LoginUserInfo.ID.ToString();//当前用户
            info.CurrentLoginUserId = LoginUserInfo.ID.ToString(); //记录当前登录的用户信息，供操作日志记录使用
        }

        /// <summary>
        /// 新增状态下的数据保存
        /// </summary>
        /// <returns></returns>
        public override bool SaveAddNew()
        {
            AddressInfo info = tempInfo;//必须使用存在的局部变量，因为部分信息可能被附件使用
            SetInfo(info);
            info.Creator = LoginUserInfo.ID.ToString();
            info.CreateTime = DateTime.Now;
            info.Dept_ID = LoginUserInfo.DeptId;
            info.Company_ID = LoginUserInfo.CompanyId;
            info.AddressType = this.AddressType;//限定类型

            try
            {
                #region 新增数据

                bool succeed = BLLFactory<Address>.Instance.Insert(info);
                if (succeed)
                {
                    //可添加其他关联操作
                    SaveAddressGroup();

                    return true;
                }
                #endregion
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmEditAddress));
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
            AddressInfo info = BLLFactory<Address>.Instance.FindByID(ID);
            if (info != null)
            {
                SetInfo(info);

                try
                {
                    #region 更新数据
                    bool succeed = BLLFactory<Address>.Instance.Update(info, info.ID);
                    if (succeed)
                    {
                        //可添加其他关联操作
                        SaveAddressGroup();

                        return true;
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmEditAddress));
                    MessageDxUtil.ShowError(ex.Message);
                }
            }
            return false;
        }

        private void lblMange_Click(object sender, EventArgs e)
        {
            FrmAddressGroup dlg = new FrmAddressGroup();
            dlg.AddressType = AddressType;
            dlg.InitFunction(LoginUserInfo, FunctionDict);
            dlg.OnDataSaved += new EventHandler(dlg_OnDataSaved);
            dlg.ShowDialog();

            BindAddressGroup();
        }

        void dlg_OnDataSaved(object sender, EventArgs e)
        {
            BindAddressGroup();
        }

        /// <summary>
        /// 初始化并绑定客户个人分组信息
        /// </summary>
        private void BindAddressGroup()
        {
            List<AddressGroupInfo> myGroupList = BLLFactory<AddressGroup>.Instance.GetByContact(tempInfo.ID);
            List<string> groupIdList = new List<string>();
            foreach (AddressGroupInfo info in myGroupList)
            {
                groupIdList.Add(info.ID);
            }

            List<AddressGroupNodeInfo> groupList = new List<AddressGroupNodeInfo>();
            if (AddressType == Entity.AddressType.个人)
            {
                groupList = BLLFactory<AddressGroup>.Instance.GetTree(AddressType.ToString(), LoginUserInfo.ID.ToString());
            }
            else
            {
                groupList = BLLFactory<AddressGroup>.Instance.GetTree(AddressType.ToString());
            }

            this.checkedListContact.BeginUpdate();
            this.checkedListContact.Items.Clear();
            foreach (AddressGroupNodeInfo nodeInfo in groupList)
            {
                bool check = groupIdList.Contains(nodeInfo.ID);
                this.checkedListContact.Items.Add(new CListItem(nodeInfo.ID, nodeInfo.Name), check);
            }
            this.checkedListContact.EndUpdate();
        }

        private bool SaveAddressGroup()
        {
            List<string> selectGroupIDList = new List<string>();
            foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in this.checkedListContact.CheckedItems)
            {
                CListItem listItem = item.Value as CListItem;
                if (listItem != null)
                {
                    selectGroupIDList.Add(listItem.Value);
                }
            }

            bool result = BLLFactory<Address>.Instance.ModifyAddressGroup(tempInfo.ID, selectGroupIDList);
            return result;
        }

        private void btnQQMessage_Click(object sender, EventArgs e)
        {
            string QQ = this.txtQQ.Text.Trim();
            if (!string.IsNullOrEmpty(QQ) && ValidateUtil.IsNumber(QQ))
            {
                Process.Start("tencent://message/?uin=" + QQ);
            }
        }

    }
}
