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
using JCodes.Framework.AddIn.Other;

namespace JCodes.Framework.AddIn.UI.Security
{
    public partial class FrmFunction : BaseForm
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
                this.txtSystemType.Properties.Items.Add(new CListItem(info.Name, info.OID));
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
                pNode.Name = typeInfo.OID;
                pNode.ImageIndex = 0;
                pNode.SelectedImageIndex = 0;
                this.treeView1.Nodes.Add(pNode);

                string systemType = typeInfo.OID;//系统标识ID

                //绑定树控件
                List<FunctionNodeInfo> funList = BLLFactory<Functions>.Instance.GetTree(systemType);
                foreach (FunctionNodeInfo info in funList)
                {
                    TreeNode item = new TreeNode();
                    item.Name = info.ID.ToString();
                    item.Text = info.Name;//一级菜单节点
                    item.Tag = info.ID;//info;//记录其info到Tag中，作为判断依据
                    item.ImageIndex = 1;
                    item.SelectedImageIndex = 1;
                    pNode.Nodes.Add(item);

                    AddChildNode(info.Children, item);
                }
                pNode.Expand();
            }

            Cursor.Current = Cursors.Default;
            treeView1.EndUpdate();
            //this.treeView1.ExpandAll();
        }

        private void AddChildNode(List<FunctionNodeInfo> list, TreeNode fnode)
        {
            foreach (FunctionNodeInfo info in list)
            {
                TreeNode item = new TreeNode();
                item.Name = info.ID.ToString();
                item.Text = info.Name;//二、三级菜单节点
                item.Tag = info.ID;//info;//记录其FunctionNodeInfo到Tag中，作为判断依据
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
                CListItem item = new CListItem(displayName, info.ID.ToString());
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
            TreeNode node = this.treeView1.SelectedNode;
            if (node != null && !string.IsNullOrEmpty(node.Name))
            {
                if (MessageDxUtil.ShowYesNoAndTips("您确认删除指定节点吗？\r\n如果该节点含有子节点，子节点不会被删除，且它们会被提升一级") == DialogResult.Yes)
                {
                    //int id = Convert.ToInt32(node.Name);
                    try
                    {
                        BLLFactory<Functions>.Instance.Delete(node.Name);
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
            TreeNode node = this.treeView1.SelectedNode;
            if (node != null && !string.IsNullOrEmpty(node.Name))
            {
                if (MessageDxUtil.ShowYesNoAndTips("您确认删除指定节点及其子节点吗？\r\n如果该节点含有子节点，子节点也会一并删除！") == DialogResult.Yes)
                {
                    //int id = Convert.ToInt32(node.Name);
                    try
                    {
                        BLLFactory<Functions>.Instance.DeleteWithSubNode(node.Name);
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
                FunctionInfo info = BLLFactory<Functions>.Instance.FindByID(currentID);

                if (info != null)
                {
                    this.txtName.Text = info.Name;
                    this.txtFunctionID.Text = info.ControlID;
                    this.txtSortCode.Text = info.SortCode;

                    FunctionInfo info2 = BLLFactory<Functions>.Instance.FindByID(info.PID);
                    if (info2 != null)
                    {
                        functionControl1.Value = info.PID;
                    }
                    else
                    {
                        functionControl1.Value = "-1";                        
                        txtSystemType.SetComboBoxItem(info.SystemType_ID);//设置系统类型
                    }

                    RefreshRoles(currentID);
                }
            }
        }

        private FunctionInfo SetFunction(FunctionInfo info)
        {
            info.Name = this.txtName.Text;
            info.PID = this.functionControl1.Value;
            info.ControlID = this.txtFunctionID.Text;
            info.SortCode = this.txtSortCode.Text;
            return info;
        }

        private void SetSystemTypeVisible(bool visible)
        {
            this.txtSystemType.Visible = visible;
            this.lblSystemType.Visible = visible;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {            
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

            if (!string.IsNullOrEmpty(currentID))
            {
                try
                {
                    FunctionInfo info = BLLFactory<Functions>.Instance.FindByID(currentID);
                    if (info != null)
                    {
                        info = SetFunction(info);
                        BLLFactory<Functions>.Instance.Update(info, info.ID.ToString());

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
                    string filter = string.Format("ControlID='{0}' and SystemType_ID='{1}'", this.txtFunctionID.Text, functionInfo.SystemType_ID);
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
                    functionInfo.PID = "-1";
                    functionInfo.SystemType_ID = this.txtSystemType.GetComboBoxValue();
                    functionInfo.ControlID = this.txtFunctionID.Text;
                    functionInfo.SortCode = this.txtSortCode.Text;

                    string filter = string.Format("ControlID='{0}' and SystemType_ID='{1}'", this.txtFunctionID.Text, functionInfo.SystemType_ID);
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
                info.SystemType_ID = functionInfo.SystemType_ID;//和父节点的SystemType_ID一样。

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
