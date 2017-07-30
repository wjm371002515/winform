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
        /// ����һ����ʱ���󣬷����ڸ��������л�ȡ���ڵ�GUID
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
                txtDept.ParentOuID = this.txtCompany.Value;
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
            //��ʼ������
            this.cmbManager.Properties.BeginUpdate();
            this.cmbManager.Properties.Items.Clear();
            this.cmbManager.Properties.Items.Add(new CListItem("��", "-1"));
            List<UserInfo> list = BLLFactory<User>.Instance.FindByDept(DeptID);
            foreach (UserInfo info in list)
            {
                this.cmbManager.Properties.Items.Add(new CListItem(info.FullName, info.ID.ToString()));
            }
            this.cmbManager.Properties.EndUpdate();
        }

        /// <summary>
        /// ʵ�ֿؼ�������ĺ���
        /// </summary>
        /// <returns></returns>
        public override bool CheckInput()
        {
            bool result = true;//Ĭ���ǿ���ͨ��

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
        /// ��ʼ�������ֵ�
        /// </summary>
        private void InitDictItem()
        {
            this.treeFunction.Nodes.Clear();//�����ƽڵ�
        }

        /// <summary>
        /// ������ʾ�ĺ���
        /// </summary>
        public override void DisplayData()
        {
            InitDictItem();//�����ֵ���أ����ã�

            if (!string.IsNullOrEmpty(ID))
            {
                #region ��ʾ��Ϣ
                UserInfo info = BLLFactory<User>.Instance.FindByID(ID);
                if (info != null)
                {
                    tempInfo = info;//���¸���ʱ����ֵ��ʹָ֮����ڵļ�¼����

                    RefreshOUs(info.ID);
                    RefreshRoles(info.ID);
                    RefreshFunction(info.ID);

                    //����ǹ���Ա����������Ϊ����
                    bool isAdmin = BLLFactory<User>.Instance.UserInRole(info.Name, RoleInfo.SuperAdminName);
                    this.txtIsExpire.Enabled = !isAdmin;
                    this.txtDeleted.Enabled = !isAdmin;

                    this.cmbManager.SetComboBoxItem(info.PID.ToString());
                    this.txtCompany.Value = info.Company_ID;
                    this.txtDept.Value = info.Dept_ID;

                    txtHandNo.Text = info.HandNo;
                    txtName.Text = info.Name;
                    txtFullName.Text = info.FullName;
                    txtNickname.Text = info.Nickname;
                    txtTitle.Text = info.Title;
                    txtIdentityCard.Text = info.IdentityCard;
                    txtMobilePhone.Text = info.MobilePhone;
                    txtOfficePhone.Text = info.OfficePhone;
                    txtHomePhone.Text = info.HomePhone;
                    txtEmail.Text = info.Email;
                    txtAddress.Text = info.Address;
                    txtWorkAddr.Text = info.WorkAddr;
                    txtGender.Text = info.Gender;
                    txtBirthday.SetDateTime(info.Birthday);
                    txtQq.Text = info.QQ;
                    txtSignature.Text = info.Signature;
                    txtAuditStatus.Text = info.AuditStatus;
                    txtNote.Text = info.Note;
                    txtCustomField.Text = info.CustomField;                   
                    txtSortCode.Text = info.SortCode;
                    txtCreator.Text = info.Creator;
                    txtCreateTime.SetDateTime(info.CreateTime);
                    txtIsExpire.Checked = info.IsExpire;
                    txtDeleted.Checked = info.Deleted;
                }
                #endregion            
            }
            else
            {
                txtCreator.Text = Portal.gc.UserInfo.FullName;//Ĭ��Ϊ��ǰ��¼�û�
                txtCreateTime.DateTime = DateTime.Now; //Ĭ�ϵ�ǰʱ��
            }
        }

        public override void ClearScreen()
        {
            this.tempInfo = new UserInfo();
            base.ClearScreen();
        }

        /// <summary>
        /// �༭���߱���״̬��ȡֵ����
        /// </summary>
        /// <param name="info"></param>
        private void SetInfo(UserInfo info)
        {
            info.PID = this.cmbManager.GetComboBoxStrValue().ToInt32();
            info.HandNo = txtHandNo.Text;
            info.Name = txtName.Text;
            info.FullName = txtFullName.Text;
            info.Nickname = txtNickname.Text;
            info.IsExpire = txtIsExpire.Checked;
            info.Title = txtTitle.Text;
            info.IdentityCard = txtIdentityCard.Text;
            info.MobilePhone = txtMobilePhone.Text;
            info.OfficePhone = txtOfficePhone.Text;
            info.HomePhone = txtHomePhone.Text;
            info.Email = txtEmail.Text;
            info.Address = txtAddress.Text;
            info.WorkAddr = txtWorkAddr.Text;
            info.Gender = txtGender.Text;
            info.Birthday = txtBirthday.DateTime;
            info.QQ = txtQq.Text;
            info.Signature = txtSignature.Text;
            info.AuditStatus = txtAuditStatus.Text;
            info.Note = txtNote.Text;
            info.CustomField = txtCustomField.Text;
            info.Dept_ID = txtDept.Value;
            info.DeptName = txtDept.Text;
            info.Company_ID = txtCompany.Value;
            info.CompanyName = txtCompany.Text;
            info.SortCode = txtSortCode.Text;
            info.Editor = Portal.gc.UserInfo.FullName;
            info.Editor_ID = Portal.gc.UserInfo.ID.ToString();
            info.EditTime = DateTime.Now;
            info.Deleted = txtDeleted.Checked;

            info.CurrentLoginUserId = Portal.gc.UserInfo.ID.ToString();
        }

        /// <summary>
        /// ����״̬�µ����ݱ���
        /// </summary>
        /// <returns></returns>
        public override bool SaveAddNew()
        {
            UserInfo info = tempInfo;//����ʹ�ô��ڵľֲ���������Ϊ������Ϣ���ܱ�����ʹ��
            SetInfo(info);
            info.Creator = Portal.gc.UserInfo.FullName;
            info.Creator_ID = Portal.gc.UserInfo.ID.ToString();
            info.CreateTime = DateTime.Now;

            try
            {
                #region ��������

                bool succeed = BLLFactory<User>.Instance.Insert(info);
                if (succeed)
                {
                    //������������������

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
        /// �༭״̬�µ����ݱ���
        /// </summary>
        /// <returns></returns>
        public override bool SaveUpdated()
        {
            UserInfo info = BLLFactory<User>.Instance.FindByID(ID);
            if (info != null)
            {
                SetInfo(info);

                try
                {
                    #region ��������
                    bool succeed = BLLFactory<User>.Instance.Update(info, info.ID);
                    if (succeed)
                    {
                        //������������������

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
                int age = DateTime.Now.Year - birthDay.Year;
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
                TreeNode parentNode = this.treeFunction.Nodes.Add(typeInfo.OID, typeInfo.Name, 0, 0);
                List<FunctionNodeInfo> list = BLLFactory<Functions>.Instance.GetFunctionNodesByUser(id, typeInfo.OID);
                AddFunctionNode(parentNode, list);                
            }

            this.treeFunction.ExpandAll();
            this.treeFunction.EndUpdate();            
        }

        private void AddFunctionNode(TreeNode node, List<FunctionNodeInfo> list)
        {
            foreach (FunctionNodeInfo info in list)
            {
               TreeNode subNode =  node.Nodes.Add(info.ID, info.Name, 1, 1);

               AddFunctionNode(subNode, info.Children);
            }
        }
    }
}