
using JCodes.Framework.AddIn.Basic;
using JCodes.Framework.BLL;
using JCodes.Framework.Common.Files;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Common.Office;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.Entity;
using JCodes.Framework.jCodesenum;
using System;
using System.Collections.Generic;
using System.Text;
namespace JCodes.Framework.TestWinForm.Basic
{
    public partial class FrmTestBizControl : DevExpress.XtraEditors.XtraForm
    {
        public FrmTestBizControl()
        {
            InitializeComponent();

            UserInfo info = BLLFactory<User>.Instance.GetUserByName("dev");

            Portal.gc.UserInfo = info;

            Cache.Instance["LoginUserInfo"] = ConvertToLoginUser(info);
           
            attachmentControl1.Init(AttachmentType.个人附件, 1, 1);
            attachmentControl1.AttachmentGid = "7cd4b652-562c-4827-b483-5ad22635caea";

            bizAttachment1.Init(AttachmentType.个人附件, 1, 1);
            bizAttachment1.AttachmentGid = "7cd4b652-562c-4827-b483-5ad22635caea";
        }

        private void FrmTestBizControl_Load(object sender, System.EventArgs e)
        {
            userNameControl1.BindData(1, "吴建明");

            
        }

        /// <summary>
        /// 转换框架通用的用户基础信息，方便框架使用
        /// </summary>
        /// <param name="info">权限系统定义的用户信息</param>
        /// <returns></returns>
        public LoginUserInfo ConvertToLoginUser(UserInfo info)
        {
            LoginUserInfo loginInfo = new LoginUserInfo();
            loginInfo.Id = info.Id;
            loginInfo.Name = info.Name;
            loginInfo.LoginName = info.LoginName;
            loginInfo.IdCard = info.IdCard;
            loginInfo.MobilePhone = info.MobilePhone;
            loginInfo.QQ = info.QQ;
            loginInfo.Email = info.Email;
            loginInfo.Gender = info.Gender;
            loginInfo.DeptId = info.DeptId;
            loginInfo.CompanyId = info.CompanyId;
            return loginInfo;
        }
    }
}
