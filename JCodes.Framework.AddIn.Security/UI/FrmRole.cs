using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;
using JCodes.Framework.BLL;
using JCodes.Framework.CommonControl;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.CommonControl.Controls;
using JCodes.Framework.Common.Format;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.Common.Extension;
using JCodes.Framework.AddIn.Basic;

namespace JCodes.Framework.AddIn.Security
{
    /// <summary>
    /// 角色管理模块
    /// </summary>
    public partial class FrmRole : BaseDock
    {
        private string currentID = string.Empty;

        public FrmRole()
        {
            InitializeComponent();
        }

        private void FrmRole_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                InitDictItem();                
                InitTreeFunction();
                RefreshTreeView();
                Init_Function();
            }
        }

        void Init_Function()
        {
            if (!HasFunction("Role/RoleDataSearch"))
            {
                xtraTabControl1.TabPages.Remove(xtraTabControl1.TabPages[2]);
            }
            if (!HasFunction("Role/FunctionSearch"))
            {
                xtraTabControl1.TabPages.Remove(xtraTabControl1.TabPages[1]);
            }
            btnAdd.Enabled = HasFunction("Role/add");
            btnDelete.Enabled = HasFunction("Role/del");
            btnEditOU.Enabled = HasFunction("Role/OUedit");
            btnRemoveOU.Enabled = HasFunction("Role/OUdel");
            btnEditUser.Enabled = HasFunction("Role/UserAdd");
            btnRemoveUser.Enabled = HasFunction("Role/UserDel");
            btnSaveFunction.Enabled = HasFunction("Role/FunctionSave");
            btnSaveRoleData.Enabled = HasFunction("Role/RoleDataSave");
        }

        private void InitDictItem()
        {
            //初始化分类
            string[] enumNames = EnumHelper.GetMemberNames<RoleCategoryEnum>();
            this.txtCategory.Properties.Items.Clear();
            foreach (string item in enumNames)
            {
                this.txtCategory.Properties.Items.Add(item);
            }
        }
        
        private void RefreshTreeView()
        {
            this.treeView1.BeginUpdate();
            this.treeView1.Nodes.Clear();

            List<OUInfo> list = Portal.gc.GetMyTopGroup();
            foreach (OUInfo groupInfo in list)
            {
                if (groupInfo != null && !groupInfo.Deleted)
                {
                    TreeNode topnode = AddOUNode(groupInfo);
                    AddRole(groupInfo, topnode);

                    if (groupInfo.Category == "集团")
                    {
                        List<OUInfo> sublist = BLLFactory<OU>.Instance.GetAllCompany(groupInfo.ID);
                        foreach (OUInfo info in sublist)
                        {
                            if (!info.Deleted)
                            {
                                TreeNode ouNode = AddOUNode(info, topnode);
                                AddRole(info, ouNode);
                            }
                        }
                    }
                    this.treeView1.Nodes.Add(topnode);
                }
            }
           
            this.treeView1.ExpandAll();
            this.treeView1.EndUpdate();
        }

        private TreeNode AddOUNode(OUInfo ouInfo, TreeNode parentNode = null)
        {
            TreeNode ouNode = new TreeNode();
            ouNode.Text = ouInfo.Name;
            ouNode.Name = ouInfo.ID.ToString();
            ouNode.Tag = ouInfo;//机构信息放到Tag里面
            if (ouInfo.Deleted)
            {
                ouNode.ForeColor = Color.Red;
            }
            ouNode.ImageIndex = Portal.gc.GetImageIndex(ouInfo.Category);
            ouNode.SelectedImageIndex = Portal.gc.GetImageIndex(ouInfo.Category);

            if (parentNode != null)
            {
                parentNode.Nodes.Add(ouNode);
            }

            return ouNode;
        }

        private void AddRole(OUInfo ouInfo, TreeNode treeNode)
        {
            List<RoleInfo> roleList = BLLFactory<Role>.Instance.GetRolesByCompany(ouInfo.ID.ToString());
            foreach (RoleInfo roleInfo in roleList)
            {
                TreeNode roleNode = new TreeNode();
                roleNode.Text = roleInfo.Name;
                roleNode.Tag = roleInfo;//角色信息放到Tag里面
                roleNode.ImageIndex = 3;
                roleNode.SelectedImageIndex = 3;
                if (ouInfo.Deleted)
                {
                    roleNode.ForeColor = Color.Red;
                    continue;//跳过不显示
                }

                treeNode.Nodes.Add(roleNode);
            }
        }

        private List<string> addedFunctionList = new List<string>();//增加的功能列表
        private List<string> deletedFunctionList = new List<string>();//删除的功能列表
        private void GetFunctionChanges(TreeNode node)
        {
            if (node.Tag != null)
            {
                string id = node.Tag.ToString();
                if (!node.Checked && dictFunction.ContainsKey(id))
                {
                    deletedFunctionList.Add(id);
                }
                if (node.Checked && !dictFunction.ContainsKey(id))
                {
                    addedFunctionList.Add(id);
                }
            }

            foreach (TreeNode subNode in node.Nodes)
            {
                GetFunctionChanges(subNode);
            }
        }

        Dictionary<string,string> dictFunction = new Dictionary<string,string>();//最初的用户列表
        private void RefreshFunctions(int roleId)
        {
            dictFunction = new Dictionary<string, string>();

            List<FunctionInfo> list = BLLFactory<Functions>.Instance.GetFunctionsByRole(roleId);

            //增加一个字典方便快速选择
            foreach (FunctionInfo info in list)
            {
                if (!dictFunction.ContainsKey(info.ID))
                {
                    dictFunction.Add(info.ID, info.ID);
                }
            }
                
            //如果是公司管理员一级，不能修改自己角色的权限（避免误操作，不再显示）
            bool isSuperAdmin = BLLFactory<User>.Instance.UserInRole(Portal.gc.UserInfo.Name, RoleInfo.SuperAdminName);
            TreeNode selectNode = this.treeView1.SelectedNode;
            if (selectNode != null && selectNode.Text.Equals(RoleInfo.CompanyAdminName) && !isSuperAdmin)
            {
                this.treeFunction.CheckBoxes = false;
            }
            else
            {
                this.treeFunction.CheckBoxes = true;
            }

            //判断角色具有哪些功能，更新勾选项
            foreach (TreeNode node in this.treeFunction.Nodes)
            {
                RefreshFunctionNode(node, dictFunction);
            }
        }

        /// <summary>
        /// 为了提高速度，第一次需要构建功能树节点
        /// </summary>
        private void InitTreeFunction()
        {
            bool isSuperAdmin = BLLFactory<User>.Instance.UserInRole(Portal.gc.UserInfo.Name, RoleInfo.SuperAdminName);
            this.treeFunction.BeginUpdate();
            this.treeFunction.Nodes.Clear();

            //初始化全部功能树
            List<SystemTypeInfo> typeList = BLLFactory<SystemType>.Instance.GetAll();
            foreach (SystemTypeInfo typeInfo in typeList)
            {
                TreeNode parentNode = this.treeFunction.Nodes.Add(typeInfo.OID, typeInfo.Name, 0, 0);

                //如果是超级管理员，不根据角色获取，否则根据角色获取对应的分配权限
                //也就是说，公司管理员只能分配自己被授权的功能，而超级管理员不受限制
                List<FunctionNodeInfo> allNode = new List<FunctionNodeInfo>();
                if (isSuperAdmin)
                {
                    allNode = BLLFactory<Functions>.Instance.GetTree(typeInfo.OID);
                }
                else
                {
                    allNode = BLLFactory<Functions>.Instance.GetFunctionNodesByUser(Portal.gc.UserInfo.ID, typeInfo.OID);
                }
                AddFunctionNode(parentNode, allNode);
            }
            this.treeFunction.ExpandAll();
            this.treeFunction.EndUpdate();
        }

        /// <summary>
        /// 初始化功能树
        /// </summary>
        private void AddFunctionNode(TreeNode node, List<FunctionNodeInfo> list)
        {
            foreach (FunctionNodeInfo info in list)
            {
                TreeNode subNode = new TreeNode(info.Name, 1, 1);
                subNode.Tag = info.ID;
                node.Nodes.Add(subNode);

                AddFunctionNode(subNode, info.Children);
            }
        }
        /// <summary>
        /// 根据角色更新功能树勾选
        /// </summary>
        private void RefreshFunctionNode(TreeNode node, Dictionary<string, string> dictFunction)
        {
            foreach (TreeNode subNode in node.Nodes)
            {
                if (subNode.Tag != null && dictFunction.ContainsKey(subNode.Tag.ToString()))
                {
                    subNode.Checked = true;
                }
                else
                {
                    subNode.Checked = false;
                }
                RefreshFunctionNode(subNode, dictFunction);
            }
        }

        /// <summary>
        /// 记录用户的选择情况
        /// </summary>
        Dictionary<string, string> SelectUserDict = new Dictionary<string, string>();
        private void RefreshUsers(int roleId)
        {
            this.lvwUser.BeginUpdate();
            this.lvwUser.Items.Clear();//清空列表

            SelectUserDict = new Dictionary<string, string>();
            List<UserInfo> list = BLLFactory<User>.Instance.GetUsersByRole(roleId);
            foreach (UserInfo info in list)
            {
                string name = string.Format("{0}（{1}）", info.FullName, info.Name);
                CListItem item = new CListItem(info.ID.ToString(), name);
                this.lvwUser.Items.Add(item);

                if (!SelectUserDict.ContainsKey(info.ID.ToString()))
                {
                    SelectUserDict.Add(info.ID.ToString(), name);
                }
            }
            if (this.lvwUser.Items.Count > 0)
            {
                this.lvwUser.SelectedIndex = 0;
            }
            this.lvwUser.EndUpdate();
        }

        private void RefreshOUs(int roleId)
        {
            this.lvwOU.BeginUpdate();
            this.lvwOU.Items.Clear();

            List<OUInfo> list = BLLFactory<OU>.Instance.GetOUsByRole(roleId);
            foreach (OUInfo info in list)
            {
                CListItem item = new CListItem( info.ID.ToString(), info.Name);
                this.lvwOU.Items.Add(item);
            }
            if (this.lvwOU.Items.Count > 0)
            {
                this.lvwOU.SelectedIndex = 0;
            }
            this.lvwOU.EndUpdate();
        }

        /// <summary>
        /// 右键的时候，设置当前节点为选中节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            if (!HasFunction("Role/del"))
            {
                MessageDxUtil.ShowError(Const.NoAuthMsg);
                return;
            }

            TreeNode node = this.treeView1.SelectedNode;
            if (node != null && node.Tag != null)
            {
                RoleInfo roleInfo = node.Tag as RoleInfo;
                if (roleInfo != null)
                {
                    if (RoleInfo.SuperAdminName.Equals(node.Text, StringComparison.OrdinalIgnoreCase))
                    {
                        MessageDxUtil.ShowWarning("保留角色不能删除");
                        return;
                    }

                    if (MessageDxUtil.ShowYesNoAndTips("您确认删除吗?") == DialogResult.Yes)
                    {
                        try
                        {
                            BLLFactory<Role>.Instance.SetDeletedFlag(roleInfo.ID);//假删除
                            RefreshTreeView();
                        }
                        catch (Exception ex)
                        {
                            LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmRole));
                            MessageDxUtil.ShowError(ex.Message);
                        }
                    }
                }
            }
        }

        private void menu_Add_Click(object sender, EventArgs e)
        {
            if (!HasFunction("Role/add"))
            {
                MessageDxUtil.ShowError(Const.NoAuthMsg);
                return;
            }

            //跳转到第一个页面
            this.xtraTabControl1.SelectedTabPageIndex = 0;

            ClearInput();
            currentID = "";
            groupControl2.Text = Const.Add + "角色详细信息";

            // 20171127 wjm 修复添加后立刻添加成员错误
            btnEditOU.Enabled = false;
            btnRemoveOU.Enabled = false;
            btnEditUser.Enabled = false;
            btnRemoveUser.Enabled = false;

            TreeNode node = this.treeView1.SelectedNode;
            if (node != null && node.Tag != null)
            {
                OUInfo ouInfo = node.Tag as OUInfo;//转换为机构对象
                if (ouInfo != null)
                {
                    this.txtCompany.Value = ouInfo.ID.ToString();
                }
            }
            this.txtName.Focus();
        }

        private void ClearInput()
        {
            this.txtName.Text = "";
            this.txtNote.Text = "";
            this.treeFunction.Nodes.Clear();
            this.lvwOU.Items.Clear();
            this.lvwUser.Items.Clear();
            this.txtCompany.Text = "";
            this.txtCategory.Text = "";
            this.txtHandNo.Text = "";
            this.txtSeq.Text = "";
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
            if (e.Node != null)
            {
                if (e.Node.Tag != null)
                {
                    RoleInfo info = e.Node.Tag as RoleInfo;
                    if (info != null)
                    {
                        groupControl2.Text = Const.Edit + "角色详细信息";
                        currentID = info.ID.ToString();
                        this.txtName.Text = info.Name;
                        this.txtNote.Text = info.Note;
                        this.txtSeq.Text = info.Seq;
                        this.txtHandNo.Text = info.HandNo;
                        this.txtCategory.Text = info.Category;
                        this.txtCompany.Value = info.Company_ID;

                        RefreshUsers(info.ID);
                        RefreshFunctions(info.ID);
                        RefreshOUs(info.ID);
                        RefreshTreeRoleData(info.ID);                
        
                        // 20171127 wjm 修复添加后立刻添加成员错误
                        btnEditOU.Enabled = true;
                        btnRemoveOU.Enabled = true;
                        btnEditUser.Enabled = true;
                        btnRemoveUser.Enabled = true;
                    }
                }
                else if (e.Node.Text == "全部角色")
                {
                }
            }
        }

        private RoleInfo SetRoleInfo(RoleInfo info)
        {
            info.Name = this.txtName.Text;
            info.Note = this.txtNote.Text;
            info.CompanyName = this.txtCompany.Text;
            info.Company_ID = this.txtCompany.Value;
            info.HandNo = this.txtHandNo.Text;
            info.Seq = this.txtSeq.Text;
            info.Category = this.txtCategory.Text;
            info.Editor = Portal.gc.UserInfo.FullName;
            info.Editor_ID = Portal.gc.UserInfo.ID.ToString();
            info.EditTime = DateTimeHelper.GetServerDateTime2();

            info.CurrentLoginUserId = Portal.gc.UserInfo.ID;
            return info;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(currentID) && !HasFunction("Role/edit"))
            {
                MessageDxUtil.ShowError(Const.NoAuthMsg);
                return;
            }

            #region 验证用户输入
            if (this.txtName.Text == "")
            {
                MessageDxUtil.ShowTips("角色名称不能为空");
                this.txtName.Focus();
                return;
            }            
            else if (this.txtCompany.Text == "")
            {
                MessageDxUtil.ShowTips("所属公司不能为空");
                this.txtCompany.Focus();
                return;
            }

            #endregion

            if (!string.IsNullOrEmpty(currentID))
            {
                TreeNode node = this.treeView1.SelectedNode;
                if (node != null && node.Tag != null)
                {
                    RoleInfo roleInfo = node.Tag as RoleInfo;
                    if (roleInfo != null && RoleInfo.SuperAdminName.Equals(roleInfo.Name, StringComparison.OrdinalIgnoreCase))
                    {
                        MessageDxUtil.ShowWarning("保留角色不能修改");
                        return;
                    }
                }

                try
                {   
                    #region 排重检查
                    string filter = string.Format("Name='{0}' AND Company_ID='{1}' AND ID <> {2} ", this.txtName.Text, this.txtCompany.Value, currentID);
                    bool isExist = BLLFactory<Role>.Instance.IsExistRecord(filter);
                    if (isExist)
                    {
                        MessageDxUtil.ShowTips("角色名称已存在！");
                        this.txtName.Focus();
                        return;
                    } 
                    #endregion

                    RoleInfo info = BLLFactory<Role>.Instance.FindByID(currentID);
                    if (info != null)
                    {
                        info = SetRoleInfo(info);
                        BLLFactory<Role>.Instance.Update(info, info.ID.ToString());

                        // 20171127 wjm 修复添加后立刻添加成员错误
                        btnEditOU.Enabled = true;
                        btnRemoveOU.Enabled = true;
                        btnEditUser.Enabled = true;
                        btnRemoveUser.Enabled = true;

                        RefreshTreeView();

                        MessageDxUtil.ShowTips("添加角色成功,请选择角色后继续操作！");
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmRole));
                    MessageDxUtil.ShowError(ex.Message);
                }
            }
            else
            {
                if (this.txtName.Text.Trim() == RoleInfo.SuperAdminName)
                {
                    MessageDxUtil.ShowTips("超级管理员为保留名称，不能新增使用");
                    this.txtName.Focus();
                    return;
                }

                #region 排重检查
                string filter = string.Format("Name='{0}' AND Company_ID='{1}' ", this.txtName.Text, this.txtCompany.Value);
                bool isExist = BLLFactory<Role>.Instance.IsExistRecord(filter);
                if (isExist)
                {
                    MessageDxUtil.ShowTips("角色名称已存在！");
                    this.txtName.Focus();
                    return;
                } 
                #endregion

                RoleInfo info = new RoleInfo();
                info = SetRoleInfo(info);
                info.Creator = Portal.gc.UserInfo.FullName;
                info.Creator_ID = Portal.gc.UserInfo.ID.ToString();
                info.CreateTime = DateTimeHelper.GetServerDateTime2();

                try
                {
                    BLLFactory<Role>.Instance.Insert(info);

                    // 20171127 wjm 修复添加后立刻添加成员错误
                    btnEditOU.Enabled = true;
                    btnRemoveOU.Enabled = true;
                    btnEditUser.Enabled = true;
                    btnRemoveUser.Enabled = true;

                    RefreshTreeView();

                    MessageDxUtil.ShowTips("添加角色成功,请选择角色后继续操作！");
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmRole));
                    MessageDxUtil.ShowError(ex.Message);
                }
            }
        }

        private void DeleteFunction(string FunctionID, int RoleID)
        {
            BLLFactory<Role>.Instance.RemoveFunction(FunctionID, RoleID);
            this.RefreshFunctions(RoleID);
        }

        private void DeleteOU(int OUID, int RoleID)
        {
            BLLFactory<Role>.Instance.RemoveOU(OUID, RoleID);
            this.RefreshOUs(RoleID);
        }

        private void DeleteUser(int RoleID, int UserID)
        {
            BLLFactory<Role>.Instance.RemoveUser(UserID, RoleID);
            this.RefreshUsers(RoleID);
        }

        private List<int> addedUserList = new List<int>();
        private List<int> deletedUserList = new List<int>();
        /// <summary>
        /// 获取那些变化了（增加的用户、删除的用户列表）
        /// </summary>
        /// <param name="oldDict">旧的列表</param>
        /// <param name="newDict">新的选择列表</param>
        private void GetUserDictChanges(Dictionary<string, string> oldDict, Dictionary<string, string> newDict)
        {
            addedUserList = new List<int>();
            deletedUserList = new List<int>();
            foreach (string key in oldDict.Keys)
            {
                if (!newDict.ContainsKey(key))
                {
                    deletedUserList.Add(key.ToInt32());
                }
            }

            foreach (string key in newDict.Keys)
            {
                if (!oldDict.ContainsKey(key))
                {
                    addedUserList.Add(key.ToInt32());
                }
            }
        }

        private void btnEditUser_Click(object sender, EventArgs e)
        {
            if (this.treeView1.SelectedNode != null)
            {
                FrmSelectUser dlg = new FrmSelectUser();
                dlg.SelectUserDict = this.SelectUserDict;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    GetUserDictChanges(this.SelectUserDict, dlg.SelectUserDict);

                    foreach (int id in deletedUserList)
                    {
                        BLLFactory<Role>.Instance.RemoveUser(id, currentID.ToInt32());
                    }
                    foreach (int id in addedUserList)
                    {
                        BLLFactory<Role>.Instance.AddUser(id, currentID.ToInt32());
                    }

                    this.RefreshUsers(currentID.ToInt32());
                }
            }
            else
            {
                MessageDxUtil.ShowTips("请选择具体的角色");
            }
        }

        private void btnRemoveUser_Click(object sender, EventArgs e)
        {
            if (this.lvwUser.SelectedItem != null)
            {
                CListItem userItem = this.lvwUser.SelectedItem as CListItem;
                if (userItem != null)
                {
                    int userId = Convert.ToInt32(userItem.Value);
                    if (!string.IsNullOrEmpty(currentID))
                    {
                        int roleID = Convert.ToInt32(currentID);
                        DeleteUser(roleID, userId);
                    }
                }
            }
        }


        private List<int> addedOUList = new List<int>();
        private List<int> deletedOUList = new List<int>();
        /// <summary>
        /// 获取那些变化了（增加的机构、删除的机构列表）
        /// </summary>
        /// <param name="oldDict">旧的列表</param>
        /// <param name="newDict">新的选择列表</param>
        private void GetOUDictChanges(Dictionary<int, int> oldDict, Dictionary<int, int> newDict)
        {
            addedOUList = new List<int>();
            deletedOUList = new List<int>();
            foreach (int key in oldDict.Keys)
            {
                if (!newDict.ContainsKey(key))
                {
                    deletedOUList.Add(key);
                }
            }

            foreach (int key in newDict.Keys)
            {
                if (!oldDict.ContainsKey(key))
                {
                    addedOUList.Add(key);
                }
            }
        }
        private void btnEditOU_Click(object sender, EventArgs e)
        {
            if (this.treeView1.SelectedNode != null)
            {
                List<OUInfo> list = BLLFactory<OU>.Instance.GetOUsByRole(currentID.ToInt32());
                Dictionary<int, int> ouDict = new Dictionary<int, int>();
                foreach (OUInfo info in list)
                {
                    if (!ouDict.ContainsKey(info.ID))
                    {
                        ouDict.Add(info.ID, info.ID);
                    }
                }

                FrmEditRoleOU dlg = new FrmEditRoleOU();
                dlg.SelectOUDict = ouDict;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    GetOUDictChanges(ouDict, dlg.SelectOUDict);

                    foreach (int id in deletedOUList)
                    {
                        BLLFactory<Role>.Instance.RemoveOU(id, currentID.ToInt32());
                    }
                    foreach (int id in addedOUList)
                    {
                        BLLFactory<Role>.Instance.AddOU(id, currentID.ToInt32());
                    }

                    RefreshOUs(currentID.ToInt32());
                }
            }
            else
            {
                MessageDxUtil.ShowTips("请选择具体的角色");
            }
        }

        private void btnRemoveOU_Click(object sender, EventArgs e)
        {
            if (this.lvwOU.SelectedItem != null)
            {
                CListItem item = this.lvwOU.SelectedItem as CListItem;
                if (item != null)
                {
                    int ouID = Convert.ToInt32(item.Value);
                    if (!string.IsNullOrEmpty(currentID))
                    {
                        int roleID = Convert.ToInt32(currentID);
                        DeleteOU(ouID, roleID);
                    }
                }
            }
        }

        private void btnEditFunction_Click(object sender, EventArgs e)
        {
            if (this.treeView1.SelectedNode != null)
            {
                FrmEditTree dlg = new FrmEditTree();
                dlg.RoleID = currentID;
                dlg.DisplayType = FrmEditTree.DisplayTreeType.Function;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    RefreshFunctions(Convert.ToInt32(currentID));
                }
            }
            else
            {
                MessageDxUtil.ShowTips("请选择具体的角色");
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

        private void menu_Collapse_Click(object sender, EventArgs e)
        {
            this.treeView1.CollapseAll();
        }

        private void btnSaveFunction_Click(object sender, EventArgs e)
        {
            if (this.treeView1.SelectedNode != null)
            {
                addedFunctionList = new List<string>();
                deletedFunctionList = new List<string>();

                //获取相关的变化
                foreach (TreeNode node in this.treeFunction.Nodes)
                {
                    GetFunctionChanges(node);
                }

                foreach (string id in deletedFunctionList)
                {
                    BLLFactory<Role>.Instance.RemoveFunction(id, currentID.ToInt32());
                }
                foreach (string id in addedFunctionList)
                {
                    BLLFactory<Role>.Instance.AddFunction(id, currentID.ToInt32());
                }

                MessageDxUtil.ShowTips("保存成功");
                this.RefreshFunctions(currentID.ToInt32());
            }
            else
            {
                MessageDxUtil.ShowTips("请选择具体的角色");
            }
        }

        private void btnRefreshFunction_Click(object sender, EventArgs e)
        {
            if (this.treeView1.SelectedNode != null)
            {
                InitTreeFunction();
                this.RefreshFunctions(currentID.ToInt32());
            }
            else
            {
                MessageDxUtil.ShowTips("请选择具体的角色");
            }
        }

        private void treeFunction_AfterCheck(object sender, TreeViewEventArgs e)
        {
            CheckSelect(e.Node, e.Node.Checked);
        }

        private void CheckSelect(TreeNode node, bool selectAll)
        {
            foreach (TreeNode subNode in node.Nodes)
            {
                subNode.Checked = selectAll;

                CheckSelect(subNode, selectAll);
            }
        }


        /// <summary>
        /// 用户角色的公司、部门数据集合
        /// </summary>
        private Dictionary<int, int> roleDataDict = new Dictionary<int, int>();

        private void AddRoleDataDept(List<OUNodeInfo> list, TreeNode treeNode)
        {
            foreach (OUNodeInfo ouInfo in list)
            {
                TreeNode deptNode = new TreeNode();
                deptNode.Text = ouInfo.Name;
                deptNode.Tag = ouInfo.ID;
                deptNode.ImageIndex = Portal.gc.GetImageIndex(ouInfo.Category);
                deptNode.SelectedImageIndex = Portal.gc.GetImageIndex(ouInfo.Category);
                if (ouInfo.Deleted)
                {
                    deptNode.ForeColor = Color.Red;
                    continue;//跳过不显示
                }
                deptNode.Checked = roleDataDict.ContainsKey(ouInfo.ID);//选中的
                treeNode.Nodes.Add(deptNode);

                AddRoleDataDept(ouInfo.Children, deptNode);
            }
        }

        /// <summary>
        /// 刷新可访问数据的角色数据权限列表
        /// </summary>
        private void RefreshTreeRoleData(int roleID)
        {
            this.roleDataDict = BLLFactory<RoleData>.Instance.GetRoleDataDict(roleID);

            this.treeRoleData.BeginUpdate();
            Cursor.Current = Cursors.WaitCursor;
            this.treeRoleData.Nodes.Clear();

            #region 增加一个所在公司、所在部门的节点
            int userCompanyId = -1;//使用-1替代用户所在公司，获取的时候替换为对应公司ID
            int userDeptId = -11;//使用-11替代用户所在部门, 获取的时候替换为对应部门ID
            TreeNode companyNode = new TreeNode();
            companyNode.Text = "所在公司";
            companyNode.Name = userCompanyId.ToString();
            companyNode.Tag = userCompanyId;
            companyNode.ImageIndex = 1;
            companyNode.SelectedImageIndex = 1;
            companyNode.Checked = roleDataDict.ContainsKey(userCompanyId);

            TreeNode deptNode = new TreeNode();
            deptNode.Text = "所在部门";
            deptNode.Name = userDeptId.ToString();
            deptNode.Tag = userDeptId;
            deptNode.ImageIndex = 2;
            deptNode.SelectedImageIndex = 2;
            deptNode.Checked = roleDataDict.ContainsKey(userDeptId);

            companyNode.Nodes.Add(deptNode);
            this.treeRoleData.Nodes.Add(companyNode); 
            #endregion

            List<OUInfo> list = Portal.gc.GetMyTopGroup();
            foreach (OUInfo groupInfo in list)
            {
                if (groupInfo != null)
                {
                    TreeNode topnode = new TreeNode();
                    topnode.Text = groupInfo.Name;
                    topnode.Name = groupInfo.ID.ToString();
                    topnode.Tag = groupInfo.ID;
                    topnode.ImageIndex = Portal.gc.GetImageIndex(groupInfo.Category);
                    topnode.SelectedImageIndex = Portal.gc.GetImageIndex(groupInfo.Category);
                    topnode.Checked = roleDataDict.ContainsKey(groupInfo.ID);//选中的

                    List<OUNodeInfo> sublist = BLLFactory<OU>.Instance.GetTreeByID(groupInfo.ID);
                    AddRoleDataDept(sublist, topnode);

                    this.treeRoleData.Nodes.Add(topnode);
                }
            }

            this.treeRoleData.ExpandAll();
            this.treeRoleData.EndUpdate();
            Cursor.Current = Cursors.Default;
        }

        //用来记录用户选择的公司，部门列表
        List<string> companyDataList = new List<string>();
        List<string> deptDataList = new List<string>();
        private void GetRoleDataSelected(TreeNode node)
        {
            if (node.Checked && node.Tag != null)
            {
                //group 0, company 1, other dept 2,3...
                if (node.ImageIndex <= 1)
                {
                    companyDataList.Add(node.Tag.ToString());
                }
                else
                {
                    deptDataList.Add(node.Tag.ToString());
                }
            }

            foreach (TreeNode subNode in node.Nodes)
            {        
                //继续递归遍历
                GetRoleDataSelected(subNode);
            }
        }
        
        private void btnSaveRoleData_Click(object sender, EventArgs e)
        {
            if (this.treeView1.SelectedNode != null)
            {
                //初始化，并使用递归获取数据的列表
                companyDataList = new List<string>();
                deptDataList = new List<string>();
                foreach (TreeNode node in this.treeRoleData.Nodes)
                {
                    GetRoleDataSelected(node);
                }

                string companyString = string.Join(",", companyDataList.ToArray());
                string deptDataString = string.Join(",", deptDataList);
                bool result = BLLFactory<RoleData>.Instance.UpdateRoleData(currentID.ToInt32(), companyString, deptDataString);
                if (result)
                {
                    MessageDxUtil.ShowTips("保存成功");
                    this.RefreshTreeRoleData(currentID.ToInt32());
                }
                else
                {
                    MessageDxUtil.ShowTips("保存失败");
                }
            }
            else
            {
                MessageDxUtil.ShowTips("请选择具体的角色");
            }
        }

        private void btnRefreshRoleData_Click(object sender, EventArgs e)
        {
            if (this.treeView1.SelectedNode != null)
            {
                this.RefreshTreeRoleData(currentID.ToInt32());
            }
            else
            {
                //MessageDxUtil.ShowTips("请选择具体的角色");
            }
        }

        private void chkAllRoleData_CheckedChanged(object sender, EventArgs e)
        {
            foreach (TreeNode node in this.treeRoleData.Nodes)
            {
                CheckRoleDataSelect(node, this.chkAllRoleData.Checked);
            }
        }

        private void CheckRoleDataSelect(TreeNode node, bool selectAll)
        {
            node.Checked = selectAll;
            foreach (TreeNode subNode in node.Nodes)
            {
                CheckRoleDataSelect(subNode, selectAll);
            }
        }

        private void chkFunctionSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (TreeNode node in this.treeFunction.Nodes)
            {
                CheckSelect(node, this.chkFunctionSelectAll.Checked);
            }
        }

        private void function_Refresh_Click(object sender, EventArgs e)
        {
            InitTreeFunction();
        }

        private void function_Collapse_Click(object sender, EventArgs e)
        {
            this.treeFunction.CollapseAll();
        }

        private void function_ExpandAll_Click(object sender, EventArgs e)
        {
            this.treeFunction.ExpandAll();
        }
    }
}
