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
using JCodes.Framework.jCodesenum;

namespace JCodes.Framework.AddIn.Security
{
    /// <summary>
    /// 角色管理模块
    /// </summary>
    public partial class FrmRole : BaseDock
    {
        private Int32 currentRoldId = 0;

        public FrmRole()
        {
            InitializeComponent();
        }

        private void FrmRole_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {               
                InitTreeFunction();
                RefreshTreeView();
                Init_Function();
            }
        }

        void Init_Function()
        {
            if (!HasFunction("Role/Set/RoleDataSearch"))
            {
                xtraTabControl1.TabPages.Remove(xtraTabControl1.TabPages[2]);
            }
            if (!HasFunction("Role/Set/FunctionSearch"))
            {
                xtraTabControl1.TabPages.Remove(xtraTabControl1.TabPages[1]);
            }
            btnAdd.Enabled = HasFunction("Role/Set/RoleAdd");
            btnDelete.Enabled = HasFunction("Role/Set/RoleDel");
            btnEditOU.Enabled = HasFunction("Role/Set/RoleOUEdit");
            btnRemoveOU.Enabled = HasFunction("Role/Set/RoleOUDel");
            btnEditUser.Enabled = HasFunction("Role/Set/RoleUserAdd");
            btnRemoveUser.Enabled = HasFunction("Role/Set/RoleUserDel");
            btnSaveFunction.Enabled = HasFunction("Role/Set/RoleFunctionEdit");
            btnSaveRoleData.Enabled = HasFunction("Role/Set/RoleDataEdit");
        }
        
        private void RefreshTreeView()
        {
            this.treeView1.BeginUpdate();
            this.treeView1.Nodes.Clear();

            List<OUInfo> list = Portal.gc.GetMyTopGroup();
            foreach (OUInfo groupInfo in list)
            {
                if (groupInfo != null && groupInfo.IsDelete == (short)IsDelete.否)
                {
                    TreeNode topnode = AddOUNode(groupInfo);
                    AddRole(groupInfo, topnode);

                    // 超级管理员可以查看集团
                    if (groupInfo.OuType == (short)OuType.集团 || groupInfo.OuType == (short)OuType.公司)
                    {
                        List<OUInfo> sublist = BLLFactory<OU>.Instance.GetAllCompany(groupInfo.Id);
                        foreach (OUInfo info in sublist)
                        {
                            if (info.IsDelete == (short)IsDelete.否)
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
            ouNode.Name = ouInfo.Id.ToString();
            ouNode.Tag = ouInfo;//机构信息放到Tag里面
            if (ouInfo.IsDelete == (short)IsDelete.是)
            {
                ouNode.ForeColor = Color.Red;
            }
            ouNode.ImageIndex = ouInfo.OuType; //Portal.gc.GetImageIndex(ouInfo.Category);
            ouNode.SelectedImageIndex = ouInfo.OuType; //Portal.gc.GetImageIndex(ouInfo.Category);

            if (parentNode != null)
            {
                parentNode.Nodes.Add(ouNode);
            }

            return ouNode;
        }

        private void AddRole(OUInfo ouInfo, TreeNode treeNode)
        {
            List<RoleInfo> roleList = BLLFactory<Role>.Instance.GetRolesByCompanyId(ouInfo.Id);
            foreach (RoleInfo roleInfo in roleList)
            {
                TreeNode roleNode = new TreeNode();
                roleNode.Text = string.Format("({0}){1}", roleInfo.RoleCode, roleInfo.Name);
                roleNode.Tag = roleInfo;//角色信息放到Tag里面
                roleNode.ImageIndex = Const.Num_Zero;
                roleNode.SelectedImageIndex = Const.Num_Zero;
                if (ouInfo.IsDelete == (short)IsDelete.是)
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
                if (!node.Checked && dictFunction.Contains(id))
                {
                    deletedFunctionList.Add(id);
                }
                if (node.Checked && !dictFunction.Contains(id))
                {
                    addedFunctionList.Add(id);
                }
            }

            foreach (TreeNode subNode in node.Nodes)
            {
                GetFunctionChanges(subNode);
            }
        }

        List<string> dictFunction = new List<string>();//最初的用户列表
        private void RefreshFunctions(Int32 roleId)
        {
            dictFunction = new List<string>();

            List<FunctionInfo> list = BLLFactory<Function>.Instance.GetFunctionsByRoleId(roleId);

            //增加一个字典方便快速选择
            foreach (FunctionInfo info in list)
            {
                if (!dictFunction.Contains(info.Gid))
                {
                    dictFunction.Add(info.Gid);
                }
            }

            TreeNode selectNode = this.treeView1.SelectedNode;

            if (BLLFactory<Sysparameter>.Instance.UserIsSuperAdmin(Portal.gc.UserInfo.Name))
            {
                this.treeFunction.CheckBoxes = false;
            }
            else if (BLLFactory<Role>.Instance.UserHasRole(Portal.gc.UserInfo.Id))
            {
                this.treeFunction.CheckBoxes = true;
            }
            else
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_ERR, string.Format("该用户({0})没有管理员权限", Portal.gc.UserInfo.Name), typeof(Login));
                MessageDxUtil.ShowError(string.Format("该用户({0})没有管理员权限", Portal.gc.UserInfo.Name));
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
            this.treeFunction.BeginUpdate();
            this.treeFunction.Nodes.Clear();

            //初始化全部功能树
            List<SystemTypeInfo> typeList = BLLFactory<SystemType>.Instance.GetAll();
            foreach (SystemTypeInfo typeInfo in typeList)
            {
                TreeNode parentNode = this.treeFunction.Nodes.Add(typeInfo.Gid, typeInfo.Name, 0, 0);

                //如果是超级管理员，不根据角色获取，否则根据角色获取对应的分配权限
                //也就是说，公司管理员只能分配自己被授权的功能，而超级管理员不受限制
                List<FunctionNodeInfo> allNode = new List<FunctionNodeInfo>();

                // 20200528 wjm 调整为不再根据用户所拥有的角色去加载菜单
                // 20191207 wjm 新增判断超级管理员 系统配置参数为1
                // 20171109 wjm 不应该直接去判断这个Name的值，不合理 删除其逻辑判断 超级管理员
                allNode = BLLFactory<Function>.Instance.GetTree(typeInfo.Gid);
                /*if (Portal.gc.IsSuperAdmin)
                {
                    allNode = BLLFactory<Function>.Instance.GetTree(typeInfo.Gid);
                }
                else
                {
                    allNode = BLLFactory<Function>.Instance.GetFunctionNodesByUser(Portal.gc.UserInfo.Id, typeInfo.Gid);
                }*/

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
                subNode.Tag = info.Gid;
                node.Nodes.Add(subNode);

                AddFunctionNode(subNode, info.Children);
            }
        }
        /// <summary>
        /// 根据角色更新功能树勾选
        /// </summary>
        private void RefreshFunctionNode(TreeNode node, List<string> dictFunction)
        {
            foreach (TreeNode subNode in node.Nodes)
            {
                if (subNode.Tag != null && dictFunction.Contains(subNode.Tag.ToString()))
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
        Dictionary<Int32, string> SelectUserDict = new Dictionary<Int32, string>();
        private void RefreshUsers(int roleId)
        {
            this.lvwUser.BeginUpdate();
            this.lvwUser.Items.Clear();//清空列表

            SelectUserDict = new Dictionary<Int32, string>();
            List<UserInfo> list = BLLFactory<User>.Instance.GetUsersByRoleId(roleId);
            foreach (UserInfo info in list)
            {
                string name = string.Format("{0}（{1}）", info.LoginName, info.Name);
                CDicKeyValue item = new CDicKeyValue(info.Id, name);
                this.lvwUser.Items.Add(item);

                if (!SelectUserDict.ContainsKey(info.Id))
                {
                    SelectUserDict.Add(info.Id, name);
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

            List<OUInfo> list = BLLFactory<OU>.Instance.GetOUsByRoleId(roleId);
            foreach (OUInfo info in list)
            {
                CDicKeyValue item = new CDicKeyValue(info.Id, info.Name);
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
            if (!HasFunction("Role/Set/RoleDel"))
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
                    if (MessageDxUtil.ShowYesNoAndTips("您确认删除吗?") == DialogResult.Yes)
                    {
                        try
                        {
                            BLLFactory<Role>.Instance.SetDeletedFlag(roleInfo.Id);//假删除
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
            if (!HasFunction("Role/Set/RoleAdd"))
            {
                MessageDxUtil.ShowError(Const.NoAuthMsg);
                return;
            }

            //跳转到第一个页面
            this.xtraTabControl1.SelectedTabPageIndex = 0;

            ClearInput();
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
                    this.txtCompany.Value = ouInfo.Id.ToString();
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
                        currentRoldId = info.Id;
                        this.txtName.Text = info.Name;
                        this.txtNote.Text = info.Remark;
                        this.txtSeq.Text = info.Seq;
                        this.txtHandNo.Text = info.RoleCode;
                        this.txtCompany.Value = info.CompanyId.ToString();

                        RefreshUsers(info.Id);
                        RefreshFunctions(info.Id);
                        RefreshOUs(info.Id);
                        RefreshTreeRoleData(info.Id);                
        
                        // 20171127 wjm 修复添加后立刻添加成员错误
                        btnEditOU.Enabled = true;
                        btnRemoveOU.Enabled = true;
                        btnEditUser.Enabled = true;
                        btnRemoveUser.Enabled = true;
                    }
                }
            }
        }

        private RoleInfo SetRoleInfo(RoleInfo info)
        {
            info.Name = this.txtName.Text;
            info.Remark = this.txtNote.Text;
            info.CompanyName = this.txtCompany.Text;
            info.CompanyId = this.txtCompany.Value.ToInt32();
            info.RoleCode = this.txtHandNo.Text;
            info.Seq = this.txtSeq.Text;
            info.EditorId = Portal.gc.UserInfo.Id;
            info.LastUpdateTime = DateTimeHelper.GetServerDateTime2();
            info.CurrentLoginUserId = Portal.gc.UserInfo.Id;

            // 新增
            if (currentRoldId == Const.Num_Zero)
            {
                info.CreatorId = Portal.gc.UserInfo.Id;
                info.CreatorTime = DateTimeHelper.GetServerDateTime2();
                info.IsDelete = (short)IsDelete.否;
                info.IsForbid = (short)IsForbid.否;
                info.Id = BLLFactory<Role>.Instance.GetMaxId() + 1;
            }

            return info;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // 检查修改权限
            if (currentRoldId > Const.Num_Zero && !HasFunction("Role/Set/RoleEdit"))
            {
                MessageDxUtil.ShowError(Const.NoAuthMsg);
                return;
            }

            // 检查新增权限
            if (currentRoldId == Const.Num_Zero && !HasFunction("Role/Set/RoleAdd"))
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

            if (currentRoldId > Const.Num_Zero)
            {
                try
                {   
                    #region 排重检查
                    string filter = string.Format("Name='{0}' AND CompanyId='{1}' AND ID <> {2} ", this.txtName.Text, this.txtCompany.Value, currentRoldId);
                    bool isExist = BLLFactory<Role>.Instance.IsExistRecord(filter);
                    if (isExist)
                    {
                        MessageDxUtil.ShowTips("角色名称已存在！");
                        this.txtName.Focus();
                        return;
                    } 
                    #endregion

                    RoleInfo info = BLLFactory<Role>.Instance.FindById(currentRoldId);
                    if (info != null)
                    {
                        info = SetRoleInfo(info);
                        BLLFactory<Role>.Instance.Update(info, info.Id);

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
                #region 排重检查
                string filter = string.Format("Name='{0}' AND CompanyId='{1}' ", this.txtName.Text, this.txtCompany.Value);
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

        private void DeleteFunction(string FunctionId, int RoleId)
        {
            BLLFactory<Role>.Instance.RemoveFunction(FunctionId, RoleId);
            this.RefreshFunctions(RoleId);
        }

        private void DeleteOU(int OUId, int RoleId)
        {
            BLLFactory<Role>.Instance.RemoveOU(OUId, RoleId);
            this.RefreshOUs(RoleId);
        }

        private void DeleteUser(int RoleId, int UserId)
        {
            BLLFactory<Role>.Instance.RemoveUser(UserId, RoleId);
            this.RefreshUsers(RoleId);
        }

        private List<int> addedUserList = new List<int>();
        private List<int> deletedUserList = new List<int>();

        /// <summary>
        /// 获取那些变化了（增加的用户、删除的用户列表）
        /// </summary>
        /// <param name="oldDict">旧的列表</param>
        /// <param name="newDict">新的选择列表</param>
        private void GetUserDictChanges(Dictionary<Int32, string> oldDict, Dictionary<Int32, string> newDict)
        {
            addedUserList = new List<int>();
            deletedUserList = new List<int>();
            foreach (Int32 key in oldDict.Keys)
            {
                if (!newDict.ContainsKey(key))
                {
                    deletedUserList.Add(key);
                }
            }

            foreach (Int32 key in newDict.Keys)
            {
                if (!oldDict.ContainsKey(key))
                {
                    addedUserList.Add(key);
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
                        BLLFactory<Role>.Instance.RemoveUser(id, currentRoldId);
                    }
                    foreach (int id in addedUserList)
                    {
                        BLLFactory<Role>.Instance.AddUser(id, currentRoldId);
                    }

                    this.RefreshUsers(currentRoldId);
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
                CDicKeyValue userItem = this.lvwUser.SelectedItem as CDicKeyValue;
                if (userItem != null)
                {
                    int userId = Convert.ToInt32(userItem.Value);
                    if (currentRoldId > Const.Num_Zero)
                    {
                        int roleID = Convert.ToInt32(currentRoldId);
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
                List<OUInfo> list = BLLFactory<OU>.Instance.GetOUsByRoleId(currentRoldId);
                Dictionary<int, int> ouDict = new Dictionary<int, int>();
                foreach (OUInfo info in list)
                {
                    if (!ouDict.ContainsKey(info.Id))
                    {
                        ouDict.Add(info.Id, info.Id);
                    }
                }

                FrmEditRoleOU dlg = new FrmEditRoleOU();
                dlg.SelectOUDict = ouDict;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    GetOUDictChanges(ouDict, dlg.SelectOUDict);

                    foreach (int id in deletedOUList)
                    {
                        BLLFactory<Role>.Instance.RemoveOU(id, currentRoldId);
                    }
                    foreach (int id in addedOUList)
                    {
                        BLLFactory<Role>.Instance.AddOU(id, currentRoldId);
                    }

                    RefreshOUs(currentRoldId);
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
                CDicKeyValue item = this.lvwOU.SelectedItem as CDicKeyValue;
                if (item != null)
                {
                    int ouId = Convert.ToInt32(item.Value);
                    if (currentRoldId > Const.Num_Zero)
                    {
                        DeleteOU(ouId, currentRoldId);
                    }
                }
            }
        }

        private void btnEditFunction_Click(object sender, EventArgs e)
        {
            if (this.treeView1.SelectedNode != null)
            {
                FrmEditTree dlg = new FrmEditTree();
                dlg.RoleId = currentRoldId;
                dlg.DisplayType = DisplayTreeType.Function;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    RefreshFunctions(currentRoldId);
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

                foreach (string Id in deletedFunctionList)
                {
                    BLLFactory<Role>.Instance.RemoveFunction(Id, currentRoldId);
                }
                foreach (string Id in addedFunctionList)
                {
                    BLLFactory<Role>.Instance.AddFunction(Id, currentRoldId);
                }

                MessageDxUtil.ShowTips("保存成功");
                this.RefreshFunctions(currentRoldId);
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
                this.RefreshFunctions(currentRoldId);
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
                deptNode.Tag = ouInfo.Id;
                deptNode.ImageIndex = ouInfo.OuType; // Portal.gc.GetImageIndex(ouInfo.Category);
                deptNode.SelectedImageIndex = ouInfo.OuType; //Portal.gc.GetImageIndex(ouInfo.Category);
                if (ouInfo.IsDelete == (short)IsDelete.是)
                {
                    deptNode.ForeColor = Color.Red;
                    continue;//跳过不显示
                }
                deptNode.Checked = roleDataDict.ContainsKey(ouInfo.Id);//选中的
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
            companyNode.ImageIndex = (short)OuType.公司;
            companyNode.SelectedImageIndex = (short)OuType.公司;
            companyNode.Checked = roleDataDict.ContainsKey(userCompanyId);

            TreeNode deptNode = new TreeNode();
            deptNode.Text = "所在部门";
            deptNode.Name = userDeptId.ToString();
            deptNode.Tag = userDeptId;
            deptNode.ImageIndex = (short)OuType.部门;
            deptNode.SelectedImageIndex = (short)OuType.部门;
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
                    topnode.Name = groupInfo.Id.ToString();
                    topnode.Tag = groupInfo.Id;
                    topnode.ImageIndex = groupInfo.OuType; //Portal.gc.GetImageIndex(groupInfo.Category);
                    topnode.SelectedImageIndex = groupInfo.OuType; //Portal.gc.GetImageIndex(groupInfo.Category);
                    topnode.Checked = roleDataDict.ContainsKey(groupInfo.Id);//选中的

                    List<OUNodeInfo> sublist = BLLFactory<OU>.Instance.GetTreeById(groupInfo.Id);
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
                if (node.ImageIndex == (short)OuType.集团 || node.ImageIndex == (short)OuType.公司)
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
                bool result = BLLFactory<RoleData>.Instance.UpdateRoleData(currentRoldId, companyString, deptDataString);
                if (result)
                {
                    MessageDxUtil.ShowTips("保存成功");
                    this.RefreshTreeRoleData(currentRoldId);
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
                this.RefreshTreeRoleData(currentRoldId);
            }
            else
            {
                MessageDxUtil.ShowTips("请选择具体的角色");
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
