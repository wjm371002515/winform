using System;
using System.Text;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using DevExpress.XtraTreeList.Nodes;
using JCodes.Framework.Entity;
using JCodes.Framework.Common;
using JCodes.Framework.BLL;
using JCodes.Framework.CommonControl;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.CommonControl.Controls;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Common.Extension;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.Common.Format;
using JCodes.Framework.AddIn.Basic;

namespace JCodes.Framework.AddIn.Security
{
    public partial class FrmEditUser : BaseEditForm
    {
        /// <summary>
        /// 创建一个临时对象，方便在附件管理中获取存在的GUID
        /// </summary>
        private UserInfo tempInfo = new UserInfo();

        public FrmEditUser()
        {
            InitializeComponent();

            this.txtCompany.EditValueChanged += new EventHandler(txtCompany_EditValueChanged);
            this.txtDept.EditValueChanged += new EventHandler(txtDept_EditValueChanged);
        }

        void txtCompany_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtCompany.Value))
            {
                txtDept.ParentOuID = this.txtCompany.Value.ToInt32();
                txtDept.Init();
            }
        }

        void txtDept_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDept.Value))
            {
                InitManagers(txtDept.Value.ToInt32());
            }
        }

        private void InitManagers(int DeptID)
        {
            //初始化代码
            this.cmbManager.Properties.BeginUpdate();
            this.cmbManager.Properties.Items.Clear();
            this.cmbManager.Properties.Items.Add(new CDicKeyValue(-1, "无"));
            List<UserInfo> list = BLLFactory<User>.Instance.FindByDept(DeptID);
            foreach (UserInfo info in list)
            {
                this.cmbManager.Properties.Items.Add(new CDicKeyValue(info.Id, info.FullName));
            }
            this.cmbManager.Properties.EndUpdate();
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
            else if (this.txtFullName.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowWarning(Const.MsgCheckInput + lblFullName.Text.Replace(Const.MsgCheckSign, string.Empty));
                this.txtFullName.Focus();
                result = false;
            }
            else if (this.txtCompany.Text == "")
            {
                MessageDxUtil.ShowWarning(Const.MsgCheckInput + lblCompany.Text.Replace(Const.MsgCheckSign, string.Empty));
                this.txtCompany.Focus();
                return false;
            }
            else if (this.txtDept.Text == "")
            {
                MessageDxUtil.ShowWarning(Const.MsgCheckInput + lblDept.Text.Replace(Const.MsgCheckSign, string.Empty));
                this.txtDept.Focus();
                return false;
            }
            #endregion

            return result;
        }

        /// <summary>
        /// 初始化数据字典
        /// </summary>
        private void InitDictItem()
        {
            this.treeFunction.Nodes.Clear();//清空设计节点
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
                UserInfo info = BLLFactory<User>.Instance.FindByID(Id);
                if (info != null)
                {
                    tempInfo = info;//重新给临时对象赋值，使之指向存在的记录对象

                    RefreshOUs(info.Id);
                    RefreshRoles(info.Id);
                    RefreshFunction(info.Id);

                    //如果是管理员，不能设置为过期
                    /*bool isAdmin = BLLFactory<User>.Instance.UserInRole(info.Name, RoleInfo.SuperAdminName);
                    this.txtIsExpire.Enabled = !isAdmin;
                    this.txtDeleted.Enabled = !isAdmin;*/

                    this.cmbManager.SetComboBoxItem(info.Pid.ToString());
                    this.txtCompany.Value = info.CompanyId.ToString();
                    this.txtDept.Value = info.DeptId.ToString();

                    txtHandNo.Text = info.UserCode;
                    txtName.Text = info.Name;
                    txtFullName.Text = info.FullName;
                    txtNickname.Text = info.LoginName;
                    //txtTitle.Text = info.Title;
                    txtIdentityCard.Text = info.IdCard;
                    txtMobilePhone.Text = info.MobilePhone;
                    txtOfficePhone.Text = info.OfficePhone;
                    txtHomePhone.Text = info.HomePhone;
                    txtEmail.Text = info.Email;
                    txtAddress.Text = info.Address;
                    txtWorkAddr.Text = info.WorkAddress;
                    txtGender.Text = info.Gender.ToString();
                    txtBirthday.SetDateTime(info.Birthday);
                    txtQq.Text = info.QQ.ToString();
                    txtSignature.Text = info.Signature;
                    txtAuditStatus.Text = info.AuditStatus.ToString();
                    txtNote.Text = info.Remark;
                    //txtCustomField.Text = info.CustomField;                   
                    txtSeq.Text = info.Seq;
                    txtCreator.Text = info.CreatorId.ToString();
                    txtCreateTime.SetDateTime(info.CreatorTime);
                    txtIsExpire.Checked = info.IsExpire == 0 ? true : false;
                    txtDeleted.Checked = info.IsDelete == 0 ? true : false;
                }
                #endregion            
            }
            else
            {
                txtCreator.Text = Portal.gc.UserInfo.FullName;//默认为当前登录用户
                txtCreateTime.DateTime = DateTimeHelper.GetServerDateTime2(); //默认当前时间
            }
        }

        public override void ClearScreen()
        {
            this.tempInfo = new UserInfo();
            base.ClearScreen();
        }

        /// <summary>
        /// 编辑或者保存状态下取值函数
        /// </summary>
        /// <param name="info"></param>
        private void SetInfo(UserInfo info)
        {
            info.Pid = this.cmbManager.GetComboBoxStrValue().ToInt32();
            info.UserCode = txtHandNo.Text;
            info.Name = txtName.Text;
            info.FullName = txtFullName.Text;
            info.LoginName = txtNickname.Text;
            info.IsExpire = (short)(txtIsExpire.Checked ? 0: 1);
            //info.t = txtTitle.Text;
            info.IdCard = txtIdentityCard.Text;
            info.MobilePhone = txtMobilePhone.Text;
            info.OfficePhone = txtOfficePhone.Text;
            info.HomePhone = txtHomePhone.Text;
            info.Email = txtEmail.Text;
            info.Address = txtAddress.Text;
            info.WorkAddress = txtWorkAddr.Text;
            info.Gender = Convert.ToInt16( txtGender.Text);
            info.Birthday = txtBirthday.DateTime;
            info.QQ = txtQq.Text.ToInt32();
            info.Signature = txtSignature.Text;
            info.AuditStatus =Convert.ToInt16(  txtAuditStatus.Text);
            info.Remark = txtNote.Text;
            //info.CustomField = txtCustomField.Text;
            info.DeptId = txtDept.Value.ToInt32();
            //info.DeptName = txtDept.Text;
            info.CompanyId = txtCompany.Value.ToInt32();
            //info.CompanyName = txtCompany.Text;
            info.Seq = txtSeq.Text;
            //info.Editor = Portal.gc.UserInfo.FullName;
            info.EditorId = Portal.gc.UserInfo.Id;
            info.LastLoginTime = DateTimeHelper.GetServerDateTime2();
            info.IsDelete = (short)(txtDeleted.Checked ? 0 : 1);

            info.CurrentLoginUserId = Portal.gc.UserInfo.Id;
        }

        /// <summary>
        /// 新增状态下的数据保存
        /// </summary>
        /// <returns></returns>
        public override bool SaveAddNew()
        {
            UserInfo info = tempInfo;//必须使用存在的局部变量，因为部分信息可能被附件使用
            SetInfo(info);
            //info = Portal.gc.UserInfo.FullName;
            info.CreatorId = Portal.gc.UserInfo.Id;
            info.CreatorTime = DateTimeHelper.GetServerDateTime2();

            try
            {
                #region 新增数据

                bool succeed = BLLFactory<User>.Instance.Insert(info);
                if (succeed)
                {
                    //可添加其他关联操作

                    return true;
                }
                #endregion
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmEditUser));
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
            UserInfo info = BLLFactory<User>.Instance.FindByID(Id);
            if (info != null)
            {
                SetInfo(info);

                try
                {
                    #region 更新数据
                    bool succeed = BLLFactory<User>.Instance.Update(info, info.Id);
                    if (succeed)
                    {
                        //可添加其他关联操作

                        return true;
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmEditUser));
                    MessageDxUtil.ShowError(ex.Message);
                }
            }
            return false;
        }

        private void txtIdentityCard_Validated(object sender, EventArgs e)
        {
            if (this.txtIdentityCard.Text.Trim().Length > 0)
            {
                GenerateBirthdays();
            }
            else
            {
                //this.txtBirthday.Text = "";
                //this.txtSex.Text = "";
            }
        }

        private void GenerateBirthdays()
        {
            string idCardNo = this.txtIdentityCard.Text.Trim();
            if (!string.IsNullOrEmpty(idCardNo))
            {
                string result = IDCardHelper.Validate(idCardNo);
                if (!string.IsNullOrEmpty(result))
                {
                    MessageDxUtil.ShowTips(result);
                    this.txtIdentityCard.Focus();
                    return;
                }

                DateTime birthDay = IDCardHelper.GetBirthday(idCardNo);
                int age = DateTimeHelper.GetServerDateTime2().Year - birthDay.Year;
                string sex = IDCardHelper.GetSexName(idCardNo);

                this.txtBirthday.DateTime = birthDay;
                //this.txtAge.Value = age;
                this.txtGender.Text = sex;
                this.txtMobilePhone.Focus();
            }
        }

        private void RefreshOUs(int id)
        {
            this.lvwOU.BeginUpdate();
            this.lvwOU.Items.Clear();

            List<OUInfo> list = BLLFactory<OU>.Instance.GetOUsByUser(id);
            foreach (OUInfo info in list)
            {
                this.lvwOU.Items.Add(info.Name);
            }
            this.lvwOU.EndUpdate();
        }

        private void RefreshRoles(int id)
        {
            this.lvwRole.BeginUpdate();
            this.lvwRole.Items.Clear();

            List<RoleInfo> list = BLLFactory<Role>.Instance.GetRolesByUser(id);
            foreach (RoleInfo info in list)
            {
                this.lvwRole.Items.Add(info.Name);
            }
            this.lvwRole.EndUpdate();
        }
        
        public void RefreshFunction(int id)
        {
            this.treeFunction.BeginUpdate();
            this.treeFunction.Nodes.Clear();

            List<SystemTypeInfo> typeList = BLLFactory<SystemType>.Instance.GetAll();
            foreach (SystemTypeInfo typeInfo in typeList)
            {
                TreeNode parentNode = this.treeFunction.Nodes.Add(typeInfo.Gid, typeInfo.Name, 0, 0);
                List<FunctionNodeInfo> list = BLLFactory<Functions>.Instance.GetFunctionNodesByUser(id, typeInfo.Gid);
                AddFunctionNode(parentNode, list);                
            }

            this.treeFunction.ExpandAll();
            this.treeFunction.EndUpdate();            
        }

        private void AddFunctionNode(TreeNode node, List<FunctionNodeInfo> list)
        {
            foreach (FunctionNodeInfo info in list)
            {
               TreeNode subNode =  node.Nodes.Add(info.Gid, info.Name, 1, 1);

               AddFunctionNode(subNode, info.Children);
            }
        }
    }
}
