using System;
using System.Text;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;
using JCodes.Framework.Entity;
using JCodes.Framework.Common;
using JCodes.Framework.BLL;
using JCodes.Framework.CommonControl;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.Common.Files;
using JCodes.Framework.AddIn.Other;

namespace JCodes.Framework.AddIn.UI.Security
{
    public partial class FrmEditMenu : BaseEditForm
    {
        /// <summary>
        /// 系统标识ID
        /// </summary>
        public string SystemType_ID = "";

        /// <summary>
        /// 上级菜单ID
        /// </summary>
        public string PID = "";

        public FrmEditMenu()
        {
            InitializeComponent();

            this.menuControl1.EditValueChanged += new EventHandler(menuControl1_EditValueChanged);
        }

        void menuControl1_EditValueChanged(object sender, EventArgs e)
        {
            DisplaySystemType();
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
                MessageDxUtil.ShowTips("请输入显示名称");
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

            //绑定系统类型
            List<SystemTypeInfo> systemList = BLLFactory<SystemType>.Instance.GetAll();
            foreach (SystemTypeInfo info in systemList)
            {
                this.txtSystemType.Properties.Items.Add(new CListItem(info.Name, info.OID));
            }
            if (this.txtSystemType.Properties.Items.Count == 1)
            {
                this.txtSystemType.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 数据显示的函数
        /// </summary>
        public override void DisplayData()
        {
            InitDictItem();//数据字典加载（公用）

            if (!string.IsNullOrEmpty(ID))
            {
                #region 显示客户信息
                MenuInfo info = BLLFactory<Menus>.Instance.FindByID(ID);
                if (info != null)
                {
                    this.menuControl1.Value = info.PID;
                    txtName.Text = info.Name;
                    txtIcon.Text = info.Icon;
                    txtSeq.Text = info.Seq;
                    txtFunctionId.Text = info.FunctionId;
                    txtVisible.Checked = info.Visible;
                    txtWinformType.Text = info.WinformType;
                    txtUrl.Text = info.Url;
                    txtWebIcon.Text = info.WebIcon;
                    txtSystemType.SetComboBoxItem(info.SystemType_ID);//设置系统类型
                }
                #endregion           
            }
            else
            { 
                if (!string.IsNullOrEmpty(SystemType_ID))
                {
                    txtSystemType.SetComboBoxItem(SystemType_ID); 
                }
            }

            DisplaySystemType();
        }

        /// <summary>
        /// 编辑或者保存状态下取值函数
        /// </summary>
        /// <param name="info"></param>
        private void SetInfo(MenuInfo info)
        {
            info.PID = this.menuControl1.Value;
            info.Name = txtName.Text;
            info.Icon = txtIcon.Text;
            info.Seq = txtSeq.Text;
            info.FunctionId = txtFunctionId.Text;
            info.Visible = txtVisible.Checked;
            info.WinformType = txtWinformType.Text;
            info.Url = txtUrl.Text;
            info.WebIcon = txtWebIcon.Text;
            info.SystemType_ID = this.txtSystemType.GetComboBoxValue();

            info.CurrentLoginUserId = Portal.gc.UserInfo.ID.ToString();
        }

        public override void ClearScreen()
        {
            int intSeq = 0;
            string seqValue = this.txtSeq.Text;
            string pid = this.menuControl1.Value;
            base.ClearScreen();

            this.txtVisible.Checked = true;
            this.txtUrl.Text = "#";
            if (int.TryParse(seqValue, out intSeq))
            {
                this.txtSeq.Text = (intSeq + 1).ToString().PadLeft(seqValue.Trim().Length, '0');
            }
            this.menuControl1.Value = pid;
        }

        /// <summary>
        /// 新增状态下的数据保存
        /// </summary>
        /// <returns></returns>
        public override bool SaveAddNew()
        {
            MenuInfo info = new MenuInfo();
            SetInfo(info);

            try
            {
                #region 新增数据

                bool succeed = BLLFactory<Menus>.Instance.Insert(info);
                if (succeed)
                {
                    if (this.menuControl1.Value == "-1")
                    {
                        string PID = info.ID;//先记录原来的ID，作为PID

                        //如果顶级菜单项目添加，同时添加一个二级菜单项目
                        info.PID = PID;
                        info.ID = Guid.NewGuid().ToString();
                        info.Seq = "001";
                        BLLFactory<Menus>.Instance.Insert(info);
                    }                    

                    return true;
                }
                #endregion
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmEditMenu));
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
            MenuInfo info = BLLFactory<Menus>.Instance.FindByID(ID);
            if (info != null)
            {
                if (info.PID != this.menuControl1.Value && BLLFactory<Menus>.Instance.GetMenuByID(ID).Count <= 1)
                {
                    MessageDxUtil.ShowError(Const.ForbidOperMsg);
                    return false;
                }

                SetInfo(info);

                try
                {
                    #region 更新数据
                    bool succeed = BLLFactory<Menus>.Instance.Update(info, info.ID.ToString());
                    if (succeed)
                    {
                        //可添加其他关联操作

                        return true;
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmEditMenu));
                    MessageDxUtil.ShowError(ex.Message);
                }
            }
            return false;
        }

        private void FrmEditMenu_Load(object sender, EventArgs e)
        {

        }

        private void DisplaySystemType()
        {
            if (menuControl1.Value == "-1")
            {
                this.layoutSystemType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else
            {
                this.layoutSystemType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }

        private void btnSelectIcon_Click(object sender, EventArgs e)
        {
            string file = GetIconPath();
            if (!string.IsNullOrEmpty(file))
            {
                this.txtIcon.Text = file;
            }
        }

        private void btnSelectWebIcon_Click(object sender, EventArgs e)
        {
            string file = GetIconPath();
            if (!string.IsNullOrEmpty(file))
            {
                this.txtWebIcon.Text = file;
            }
        }

        private string GetIconPath()
        {
            string iconFile = "Icon File(*.ico)|*.ico|Image Files(*.BMP;*.bmp;*.JPG;*.jpg;*.GIF;*.gif;*.png;*.PNG)|(*.BMP;*.bmp;*.JPG;*.jpg;*.GIF;*.gif;*.png;*.PNG)|All File(*.*)|*.*";
            string file = FileDialogHelper.Open("选择图标文件", iconFile, "", Application.StartupPath);
            string result = "";
            if (!string.IsNullOrEmpty(file))
            {
                result = file.Replace(Application.StartupPath, "").Trim('\\');
            }

            return result;
        }
    }
}
