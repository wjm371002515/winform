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
using JCodes.Framework.jCodesenum;
using JCodes.Framework.Common.Network;

namespace JCodes.Framework.AddIn.Security
{
    public partial class FrmEditUser : BaseEditForm
    {
        /*
        用户名(Name) 登陆姓名(LoginName)
        所属公司(CompanyId) 所属部门(DeptId)
        用户编码(UserCode) 排序码(Seq)
        qq(QQ)
        邮件地址(Email)	移动电话(MobilePhone)
        身份证(IdCard)		性别(Gender)	出生日期(Birthday)
        办公电话(OfficePhone)	家庭电话(HomePhone)	
        办公地址(WorkAddress)	家庭住址(Address)
        个性签名(Signature)	备注(Remark)
        审核状态(AuditStatus)
        用户过期(IsExpire)	账号删除(IsDelete)
        创建人(CreatorId)		创建时间(CreatorTime)
     
        Id、Password、Portrait、EditorId、LastUpdateTime、Question1、Question2、Question3、Answer1、Answer2、
        Answer3、LastLoginIp、LastLoginTime、LastLoginMac、CurLoginIp、CurLoginTime、CurLoginMac、LastChangePwdTime
         */

        /// <summary>
        /// 创建一个临时对象，方便在附件管理中获取存在的GUID
        /// </summary>
        private UserInfo tempInfo = new UserInfo();

        public FrmEditUser()
        {
            InitializeComponent();

            this.txtCompany.EditValueChanged += new EventHandler(txtCompany_EditValueChanged);
        }

        void txtCompany_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtCompany.Value))
            {
                txtDept.ParentOuId = this.txtCompany.Value.ToInt32();
                txtDept.Init();
            }
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
            else if (this.txtLoginName.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowWarning(Const.MsgCheckInput + lblLoginName.Text.Replace(Const.MsgCheckSign, string.Empty));
                this.txtLoginName.Focus();
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
                UserInfo info = BLLFactory<User>.Instance.FindById(Id);
                if (info != null)
                {
                    tempInfo = info;//重新给临时对象赋值，使之指向存在的记录对象

                    RefreshOUs(info.Id);
                    RefreshRoles(info.Id);
                    RefreshFunction(info.Id);

                    txtIsExpire.EditValue = (Int32)info.IsExpire;
                    txtIsDelete.EditValue = (Int32)info.IsDelete;
                    txtName.Text = info.Name;
                    txtLoginName.Text = info.LoginName;
                    txtCompany.Value = info.CompanyId.ToString();
                    txtDept.Value = info.DeptId.ToString();
                    txtUserCode.Text = info.UserCode;
                    txtSeq.Text = info.Seq;
                    txtQq.Text = info.QQ.ToString();
                    txtEmail.Text = info.Email;
                    txtMobilePhone.Text = info.MobilePhone;
                    txtIdCard.Text = info.IdCard;
                    txtGender.EditValue = (Int32)info.Gender;
                    txtBirthday.SetDateTime(info.Birthday);
                    txtOfficePhone.Text = info.OfficePhone;
                    txtHomePhone.Text = info.HomePhone;
                    txtWorkAddr.Text = info.WorkAddress;
                    txtAddress.Text = info.Address;
                    txtSignature.Text = info.Signature;
                    txtRemark.Text = info.Remark;
                    txtAuditStatus.EditValue = (Int32)info.AuditStatus;
                    txtIsExpire.EditValue = (Int32)info.IsExpire;
                    txtIsDelete.EditValue = (Int32)info.IsDelete;
                    txtCreator.Text = Portal.gc.UserInfo.LoginName;
                    txtCreatorTime.SetDateTime(info.CreatorTime);
                    txtMac.Text = NetworkUtil.GetMacAddress();
                    txtIP.Text = NetworkUtil.GetLocalIP();

                    //如果是管理员，不能设置为过期
                    if (Portal.gc.IsSuperAdmin)
                    {
                        this.txtIsExpire.Enabled = false;
                        this.txtIsExpire.EditValue = (short)IsExpand.是;
                        this.txtIsDelete.Enabled = false;
                        this.txtIsDelete.EditValue = (short)IsDelete.是;
                    }
                }
                #endregion            
            }
            else
            {
                txtCreator.Text = Portal.gc.UserInfo.LoginName;//默认为当前登录用户
                txtCreatorTime.DateTime = DateTimeHelper.GetServerDateTime2(); //默认当前时间
                xtraTabControl1.TabPages[1].PageVisible = false;
                txtMac.Text = NetworkUtil.GetMacAddress();
                txtIP.Text = NetworkUtil.GetLocalIP();
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
            info.Name = txtName.Text;
            info.LoginName = txtLoginName.Text;
            info.CompanyId = txtCompany.Value.ToInt32();
            info.DeptId = txtDept.Value.ToInt32();
            info.UserCode = txtUserCode.Text;
            info.Seq = txtSeq.Text;
            info.QQ = txtQq.Text.ToInt32();
            info.Email = txtEmail.Text;
            info.MobilePhone = txtMobilePhone.Text;
            info.IdCard = txtIdCard.Text;
            info.Gender = (short)txtGender.EditValue.ToString().ToInt32();
            info.Birthday = txtBirthday.DateTime;
            info.OfficePhone = txtOfficePhone.Text;
            info.HomePhone = txtHomePhone.Text;
            info.WorkAddress = txtWorkAddr.Text;
            info.Address = txtAddress.Text;
            info.Signature = txtSignature.Text;
            info.Remark = txtRemark.Text;
            info.AuditStatus = (short)txtAuditStatus.EditValue.ToString().ToInt32();
            info.IsExpire = (short)txtIsExpire.EditValue.ToString().ToInt32();
            info.IsDelete = (short)txtIsDelete.EditValue.ToString().ToInt32();
            info.EditorId = Portal.gc.UserInfo.Id;
            info.LastUpdateTime = DateTimeHelper.GetServerDateTime2(); //默认当前时间
            info.LastLoginIp = NetworkUtil.GetLocalIP();
            info.LastLoginTime = DateTimeHelper.GetServerDateTime2(); //默认当前时间
            info.LastLoginMac = NetworkUtil.GetMacAddress();
        }

        /// <summary>
        /// 新增状态下的数据保存
        /// </summary>
        /// <returns></returns>
        public override bool SaveAddNew()
        {
            UserInfo info = tempInfo;//必须使用存在的局部变量，因为部分信息可能被附件使用
            SetInfo(info);
            info.CreatorId = Portal.gc.UserInfo.Id;
            info.CreatorTime = DateTimeHelper.GetServerDateTime2();
            info.Id = BLLFactory<User>.Instance.GetMaxId() + 1;
            info.Password = Const.defaultPwd;
            info.CurLoginIp = NetworkUtil.GetLocalIP();
            info.CurLoginMac = NetworkUtil.GetMacAddress();
            info.LastLoginTime = DateTimeHelper.GetServerDateTime2(); //默认当前时间

            try
            {
                #region 新增数据

                bool succeed = BLLFactory<User>.Instance.Insert(info);
                if (succeed)
                {
                    //可添加其他关联操作
                    MessageDxUtil.ShowTips(string.Format("添加用户{0}({1})成功，账户默认密码为【{2}】", info.Name, info.LoginName, Const.defaultPwd));
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
            UserInfo info = BLLFactory<User>.Instance.FindById(Id);
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
            if (this.txtIdCard.Text.Trim().Length > 0)
            {
                GenerateBirthdays();
            }
            else
            {
                /*this.txtBirthday.Text = "";
                this.txtGender.EditValue = "";*/
            }
        }

        private void GenerateBirthdays()
        {
            string idCardNo = this.txtIdCard.Text.Trim();
            if (!string.IsNullOrEmpty(idCardNo))
            {
                string result = IDCardHelper.Validate(idCardNo);
                if (!string.IsNullOrEmpty(result))
                {
                    MessageDxUtil.ShowTips(result);
                    this.txtIdCard.Focus();
                    return;
                }

                DateTime birthDay = IDCardHelper.GetBirthday(idCardNo);
                //int age = DateTimeHelper.GetServerDateTime2().Year - birthDay.Year;
                string sex = IDCardHelper.GetSexName(idCardNo);
                this.txtBirthday.DateTime = birthDay;
                this.txtGender.EditValue = EnumHelper.GetMemberValue<Gender>(sex);
            }
        }

        private void RefreshOUs(Int32 Id)
        {
            this.lvwOU.BeginUpdate();
            this.lvwOU.Items.Clear();

            List<OUInfo> list = BLLFactory<OU>.Instance.GetOUsByUserId(Id);
            foreach (OUInfo info in list)
            {
                this.lvwOU.Items.Add(info.Name);
            }
            this.lvwOU.EndUpdate();
        }

        private void RefreshRoles(Int32 Id)
        {
            this.lvwRole.BeginUpdate();
            this.lvwRole.Items.Clear();

            List<RoleInfo> list = BLLFactory<Role>.Instance.GetRolesByUser(Id);
            foreach (RoleInfo info in list)
            {
                this.lvwRole.Items.Add(info.Name);
            }
            this.lvwRole.EndUpdate();
        }
        
        public void RefreshFunction(Int32 Id)
        {
            this.treeFunction.BeginUpdate();
            this.treeFunction.Nodes.Clear();

            List<SystemTypeInfo> typeList = BLLFactory<SystemType>.Instance.GetAll();
            foreach (SystemTypeInfo typeInfo in typeList)
            {
                TreeNode parentNode = this.treeFunction.Nodes.Add(typeInfo.Gid, typeInfo.Name, 0, 0);
                List<FunctionNodeInfo> list = BLLFactory<Function>.Instance.GetFunctionNodesByUser(Id, typeInfo.Gid);
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
