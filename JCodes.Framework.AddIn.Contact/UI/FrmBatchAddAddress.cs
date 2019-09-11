using System;
using System.Text;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using JCodes.Framework.Entity;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.Common.Office;
using JCodes.Framework.Common;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.BLL;
using JCodes.Framework.Common.Format;
using JCodes.Framework.jCodesenum;

namespace JCodes.Framework.AddIn.Contact
{
    public partial class FrmBatchAddAddress : BaseEditForm
    {
        /// <summary>
        /// 通讯录类型
        /// </summary>
        public AddressType AddressType = AddressType.个人;

        public FrmBatchAddAddress()
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
            if (this.txtContent.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowWarning(Const.MsgCheckInput + txtContent.Text.Replace(Const.MsgCheckSign, string.Empty));
                this.txtContent.Focus();
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

            this.txtAddressType.Text = AddressType.ToString();
            this.btnOK.Enabled = HasFunction("Address/Add") || HasFunction("AddressCompany/Add");
            this.btnAdd.Enabled = this.btnOK.Enabled;

            BindAddressGroup();
        }

        /// <summary>
        /// 编辑或者保存状态下取值函数
        /// </summary>
        /// <param name="info"></param>
        private void SetInfo(AddressInfo info)
        {
            info.Remark = txtNote.Text;
            info.LastUpdateTime = DateTimeHelper.GetServerDateTime2();
            info.EditorId = LoginUserInfo.Id;//当前用户
            info.CurrentLoginUserId = LoginUserInfo.Id; //记录当前登录的用户信息，供操作日志记录使用
        }

        /// <summary>
        /// 获取邮件地址和显示信息到一个字典列表中
        /// </summary>
        /// <param name="emailValues">含邮件地址和说明的字符串（多个用；分开）</param>
        /// <param name="dictList">规范的邮件地址列表</param>
        private void GetEmailList(string emailValues, ref Dictionary<string, string> dictList)
        {
            if (!string.IsNullOrEmpty(emailValues))
            {
                string emailReg = @"([a-z0-9_\.-]+)@([\da-z\.-]+)\.([a-z\.]{2,6})";
                foreach (string item in emailValues.Split(new char[] { ';', ',' }))
                {
                    string email = CRegex.GetText(item, emailReg, 0);
                    if (!string.IsNullOrEmpty(email))
                    {
                        string display = item.Replace(email, "").Replace("(", "").Replace(")", "").Replace("<", "").Replace(">", "");
                        display = !string.IsNullOrEmpty(display) ? display : email;

                        if (!dictList.ContainsKey(email))
                        {
                            dictList.Add(email, display);
                        }
                    }
                }
            }
        }

        private string GetParamValue(string[] paramList, int index)
        {
            string result = "";
            if (paramList.Length >= (index + 1))
            {
                result = paramList[index];
            }
            return result;
        }

        /// <summary>
        /// 新增状态下的数据保存
        /// </summary>
        /// <returns></returns>
        public override bool SaveAddNew()
        {
            try
            {
                //通讯录内容格式：姓名,性别,手机 [,邮箱,QQ,部门,职位]
                //如：张三,男,13800000000
                //李莉,女,13900000000,test@163.com,123456,市场部,部门经理
                foreach (string line in txtContent.Lines)
                {
                    #region 增加数据
                    string lineString = line.Trim();
                    if (!string.IsNullOrEmpty(lineString))
                    {
                        string[] paramList = lineString.Split(new char[] { '，', ',', '、', ';', ' ', '\t' });
                        AddressInfo info = new AddressInfo();
                        if (paramList.Length >= 3)
                        {
                            info.Name = paramList[0];
                            info.Sex = Convert.ToInt32( paramList[1]);
                            info.MobilePhone = paramList[2];

                            info.Email = GetParamValue(paramList, 3);
                            //info.QQ = GetParamValue(paramList, 4);
                            info.DeptName = GetParamValue(paramList, 5);
                            info.Position = GetParamValue(paramList, 6);

                            SetInfo(info);
                            info.CreatorId = LoginUserInfo.Id;
                            info.CreatorTime = DateTimeHelper.GetServerDateTime2();
                            info.DeptId = LoginUserInfo.DeptId;
                            info.CompanyId = LoginUserInfo.CompanyId;
                            info.AddressType = AddressType;

                            bool succeed = BLLFactory<Address>.Instance.Insert(info);
                            if (succeed)
                            {
                                //可添加其他关联操作
                                SaveAddressGroup(info.Id);
                            }
                        }

                    }
                    #endregion
                }

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmBatchAddAddress));
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
            AddressInfo info = BLLFactory<Address>.Instance.FindByID(Id);
            if (info != null)
            {
                SetInfo(info);

                try
                {
                    #region 更新数据
                    bool succeed = BLLFactory<Address>.Instance.Update(info, info.Id);
                    if (succeed)
                    {
                        //可添加其他关联操作
                        SaveAddressGroup(info.Id);

                        return true;
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmBatchAddAddress));
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
            List<AddressGroupNodeInfo> groupList = new List<AddressGroupNodeInfo>();
            if (AddressType == AddressType.个人)
            {
                groupList = BLLFactory<AddressGroup>.Instance.GetTree(AddressType.ToString(), LoginUserInfo.Id);
            }
            else
            {
                groupList = BLLFactory<AddressGroup>.Instance.GetTree(AddressType.ToString());
            }

            this.checkedListContact.BeginUpdate();
            this.checkedListContact.Items.Clear();
            foreach (AddressGroupNodeInfo nodeInfo in groupList)
            {
                // // 20170901 wjm 调整key 和value的顺序
                this.checkedListContact.Items.Add(new CDicKeyValue(nodeInfo.Id, nodeInfo.Name), false);
            }
            this.checkedListContact.EndUpdate();
        }

        private bool SaveAddressGroup(Int32 contactId)
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

            bool result = BLLFactory<Address>.Instance.ModifyAddressGroup(contactId, selectGroupIDList);
            return result;
        }

    }
}
