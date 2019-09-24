using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using JCodes.Framework.Entity;
using JCodes.Framework.Common;
using JCodes.Framework.BLL;
using JCodes.Framework.CommonControl;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.CommonControl.Controls;
using JCodes.Framework.AddIn.Basic;
using JCodes.Framework.Common.Extension;

namespace JCodes.Framework.AddIn.Security
{
    public partial class FrmFunction : BaseDock
    {
        private string currentID = string.Empty;

        public FrmFunction()
        {
            InitializeComponent();
        }

        private void FrmFunction_Load(object sender, EventArgs e)
        {
            InitDictItem();
            RefreshTreeView();
            Init_Function();
        }

        void Init_Function()
        {
            btnAdd.Enabled = HasFunction("Function/add");
            btnDelete.Enabled = HasFunction("Function/del");
            btnBatchAdd.Enabled = HasFunction("Function/BatchAdd");
        }

        /// <summary>
        /// 初始化字典列表内容
        /// </summary>
        private void InitDictItem()
        {
            //绑定系统类型
            List<SystemTypeInfo> systemList = BLLFactory<SystemType>.Instance.GetAll();
            foreach (SystemTypeInfo info in systemList)
            {
                this.txtSystemType.Properties.Items.Add(new CListItem(info.Gid, info.Name));
            }
            if (this.txtSystemType.Properties.Items.Count == 1)
            {
                this.txtSystemType.SelectedIndex = 0;
            }
        }

        private void RefreshTreeView()
        {
            treeView1.Nodes.Clear();
            treeView1.BeginUpdate();
            Cursor.Current = Cursors.WaitCursor;                       

            List<SystemTypeInfo> typeList = BLLFactory<SystemType>.Instance.GetAll();
            foreach (SystemTypeInfo typeInfo in typeList)
            {
                TreeNode pNode = new TreeNode();
                pNode.Text = typeInfo.Name;//系统类型节点
                pNode.Name = typeInfo.Gid;
                pNode.ImageIndex = 0;
                pNode.SelectedImageIndex = 0;
                this.treeView1.Nodes.Add(pNode);

                string systemType = typeInfo.Gid;//系统标识ID

                //绑定树控件
                List<FunctionNodeInfo> funList = BLLFactory<Functions>.Instance.GetTree(systemType);
                foreach (FunctionNodeInfo info in funList)
                {
                    TreeNode item = new TreeNode();
                    item.Name = info.Gid;
                    item.Text = info.Name;//一级菜单节点
                    item.Tag = info.Gid;//info;//记录其info到Tag中，作为判断依据
                    item.ImageIndex = 1;
                    item.SelectedImageIndex = 1;
                    pNode.Nodes.Add(item);

                    AddChildNode(info.Children, item);
                }
                pNode.Expand();
            }

            Cursor.Current = Cursors.Default;
            treeView1.EndUpdate();
        }

        private void AddChildNode(List<FunctionNodeInfo> list, TreeNode fnode)
        {
            foreach (FunctionNodeInfo info in list)
            {
                TreeNode item = new TreeNode();
                item.Name = info.Gid;
                item.Text = info.Name;//二、三级菜单节点
                item.Tag = info.Gid;//info;//记录其FunctionNodeInfo到Tag中，作为判断依据
                item.ImageIndex = 1;
                item.SelectedImageIndex = 1;
                fnode.Nodes.Add(item);
                fnode.Collapse();

                AddChildNode(info.Children, item);
            }
        }

        private void ClearInput()
        {
            this.txtFunctionID.Text = "";
            this.txtName.Text = "";
            this.functionControl1.Value = "-1";
            this.lvwRole.Items.Clear();
        }

        private void RefreshRoles(string functionId)
        {
            this.lvwRole.Items.Clear();
            List<RoleInfo> list = BLLFactory<Role>.Instance.GetRolesByFunction(functionId);
            foreach (RoleInfo info in list)
            {
                string displayName = info.Name;
                if (!string.IsNullOrEmpty(info.CompanyName))
                {
                    displayName = string.Format("{0}({1})", info.Name, info.CompanyName);
                }
                CListItem item = new CListItem(info.Id.ToString(), displayName );
                this.lvwRole.Items.Add(item);
            }
            if (this.lvwRole.Items.Count > 0)
            {
                this.lvwRole.SelectedIndex = 0;
            }
        }

        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TreeNode node = treeView1.GetNodeAt(e.X, e.Y);
                if (node != null)
                {
                    treeView1.SelectedNode = node;
                }
            }

            base.OnMouseDown(e);
        }

        private void menu_Delete_Click(object sender, EventArgs e)
        {
            if (!HasFunction("Function/del"))
            {
                MessageDxUtil.ShowError(Const.NoAuthMsg);
                return;
            }

            TreeNode node = this.treeView1.SelectedNode;
            if (node != null && !string.IsNullOrEmpty(node.Name))
            {
                if (MessageDxUtil.ShowYesNoAndTips("您确认删除指定节点吗？\r\n如果该节点含有子节点，子节点不会被删除，且它们会被提升一级") == DialogResult.Yes)
                {
                    //int id = Convert.ToInt32(node.Name);
                    try
                    {
                        BLLFactory<Functions>.Instance.DeleteByUser(node.Name, LoginUserInfo.Id);
                        RefreshTreeView();
                    }
                    catch (Exception ex)
                    {
                        LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmFunction));
                        MessageDxUtil.ShowError(ex.Message);
                    }
                }
            }
        }

        private void menu_DeletAll_Click(object sender, EventArgs e)
        {
            if (!HasFunction("Function/BatchDel"))
            {
                MessageDxUtil.ShowError(Const.NoAuthMsg);
                return;
            }

            TreeNode node = this.treeView1.SelectedNode;
            if (node != null && !string.IsNullOrEmpty(node.Name))
            {
                if (MessageDxUtil.ShowYesNoAndTips("您确认删除指定节点及其子节点吗？\r\n如果该节点含有子节点，子节点也会一并删除！") == DialogResult.Yes)
                {
                    try
                    {
                        BLLFactory<Functions>.Instance.DeleteWithSubNode(node.Name, Portal.gc.UserInfo.Id);
                        RefreshTreeView();
                    }
                    catch (Exception ex)
                    {
                        LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmFunction));
                        MessageDxUtil.ShowError(ex.Message);
                    }
                }
            }
        }

        private void menu_Add_Click(object sender, EventArgs e)
        {
            if (!HasFunction("Function/add"))
            {
                MessageDxUtil.ShowError(Const.NoAuthMsg);
                return;
            }

            groupControl1.Text = Const.Add + "功能详细信息";

            ClearInput();
            currentID = "";
            TreeNode node = this.treeView1.SelectedNode;
            if (node != null && node.Tag != null)
            {
                this.functionControl1.Value = node.Name;
            }
            this.txtName.Focus();
        }

        private void menu_Update_Click(object sender, EventArgs e)
        {
            RefreshTreeView();
        }

        private void menu_ExpandAll_Click(object sender, EventArgs e)
        {
            this.treeView1.ExpandAll();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null && e.Node.Tag != null)
            {
                currentID = e.Node.Tag.ToString();
                groupControl1.Text = Const.Edit + "功能详细信息";
                FunctionInfo info = BLLFactory<Functions>.Instance.FindByID(currentID);

                if (info != null)
                {
                    this.txtName.Text = info.Name;
                    this.txtFunctionID.Text = info.DllPath;
                    this.txtSeq.Text = info.Seq;

                    FunctionInfo info2 = BLLFactory<Functions>.Instance.FindByID(info.Pgid);
                    if (info2 != null)
                    {
                        functionControl1.Value = info.Pgid;
                    }
                    else
                    {
                        functionControl1.Value = "-1";                        
                        txtSystemType.SetComboBoxItem(info.SystemtypeId);//设置系统类型
                    }

                    RefreshRoles(currentID);
                }
            }
        }

        private FunctionInfo SetFunction(FunctionInfo info)
        {
            info.Name = this.txtName.Text;
            info.Pgid = this.functionControl1.Value;
            info.DllPath = this.txtFunctionID.Text;
            info.Seq = this.txtSeq.Text;
            info.CurrentLoginUserId = Portal.gc.UserInfo.Id;
            return info;
        }

        private void SetSystemTypeVisible(bool visible)
        {
            this.txtSystemType.Visible = visible;
            this.lblSystemType.Visible = visible;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(currentID) && !HasFunction("Function/edit"))
            {
                MessageDxUtil.ShowError(Const.NoAuthMsg);
                return;
            }

            if (this.functionControl1.Text.Length == 0)
                return;

            #region 验证用户输入
            if (this.txtName.Text == "")
            {
                MessageDxUtil.ShowTips("功能名称不能为空");
                this.txtName.Focus();
                return;
            }
            else if (this.txtFunctionID.Text == "")
            {
                MessageDxUtil.ShowTips("功能ID不能为空");
                this.txtFunctionID.Focus();
                return;
            }
            else if (this.txtSystemType.Visible && this.txtSystemType.Text.Length == 0)
            {
                MessageDxUtil.ShowTips("系统类型编号不能为空");
                this.txtSystemType.Focus();
                return;
            }

            #endregion
            // 更新操作
            if (!string.IsNullOrEmpty(currentID))
            {
                try
                {
                    // 合法操作检查
                    FunctionInfo info = BLLFactory<Functions>.Instance.FindByID(currentID);

                    if (info.Pgid != functionControl1.Value && BLLFactory<Functions>.Instance.GetFunctionByPID(currentID).Count <= 1)
                    {
                        MessageDxUtil.ShowError(Const.ForbidOperMsg);
                        return;
                    }

                    if (info != null)
                    {
                        info = SetFunction(info);
                        BLLFactory<Functions>.Instance.Update(info, info.Gid);

                        RefreshTreeView();
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmFunction));
                    MessageDxUtil.ShowError(ex.Message);
                }
            }
            else
            {
                string pid = this.functionControl1.Value;
                FunctionInfo functionInfo = BLLFactory<Functions>.Instance.FindByID(pid);

                if (functionInfo != null)
                {
                    string filter = string.Format("FunctionId='{0}' and SystemType_ID='{1}'", this.txtFunctionID.Text, functionInfo.SystemtypeId);
                    bool isExist = BLLFactory<Functions>.Instance.IsExistRecord(filter);
                    if (isExist)
                    {
                        MessageDxUtil.ShowTips("指定功能控制ID重复，请重新输入！");
                        this.txtName.Focus();
                        return;
                    }
                }
                else
                {
                    //当新建系统类型的时候
                    functionInfo = new FunctionInfo();
                    functionInfo.Pgid = "-1";
                    functionInfo.SystemtypeId = this.txtSystemType.GetComboBoxStrValue();
                    functionInfo.DllPath = this.txtFunctionID.Text;
                    functionInfo.Seq = this.txtSeq.Text;

                    string filter = string.Format("FunctionId='{0}' and SystemType_ID='{1}'", this.txtFunctionID.Text, functionInfo.SystemtypeId);
                    bool isExist = BLLFactory<Functions>.Instance.IsExistRecord(filter);
                    if (isExist)
                    {
                        MessageDxUtil.ShowTips("指定功能控制ID重复，请重新输入！");
                        this.txtName.Focus();
                        return;
                    }
                }

                FunctionInfo info = new FunctionInfo();
                info = SetFunction(info);
                info.SystemtypeId = functionInfo.SystemtypeId;//和父节点的SystemType_ID一样。

                try
                {
                    BLLFactory<Functions>.Instance.Insert(info);
                    RefreshTreeView();
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmFunction));
                    MessageDxUtil.ShowError(ex.Message);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            groupControl1.Text = Const.Add + "功能详细信息";

            menu_Add_Click(null, null);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            menu_Delete_Click(null, null);
        }

        private void btnBatchAdd_Click(object sender, EventArgs e)
        {
            TreeNode node = this.treeView1.SelectedNode;
            if (node != null && node.Tag != null)
            {
                FrmAddMoreFunction dlg = new FrmAddMoreFunction();
                dlg.OnDataSaved += new EventHandler(dlg_OnDataSaved);
                dlg.SetUpperFunction(node.Name);
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //提高速度，避免重复更新
                    //RefreshTreeView();
                }
            }
            else
            {
                MessageDxUtil.ShowTips("请选择功能节点再执行操作");
            }
        }

        void dlg_OnDataSaved(object sender, EventArgs e)
        {
            //提高速度，避免重复更新
            //RefreshTreeView();
        }

        private void menu_Collapse_Click(object sender, EventArgs e)
        {
            this.treeView1.CollapseAll();
        }

    }
}
