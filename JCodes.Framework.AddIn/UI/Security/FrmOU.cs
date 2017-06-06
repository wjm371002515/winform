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
using JCodes.Framework.AddIn.Other;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.Common.Extension;
using JCodes.Framework.Common.Format;

namespace JCodes.Framework.AddIn.UI.Security
{
    public partial class FrmOU : BaseForm
    {
        private string currentID = string.Empty;

        public FrmOU()
        {
            InitializeComponent();
        }

        private void FrmOU_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                RefreshTreeView();
                InitDictItem();
            }
        }

        private void InitDictItem()
        {
            //初始化分类
            string[] enumNames = EnumHelper.GetMemberNames<OUCategoryEnum>();
            this.txtCategory.Properties.Items.Clear();
            foreach (string item in enumNames)
            {
                this.txtCategory.Properties.Items.Add(item);
            }
        }

        private void RefreshTreeView()
        {
            treeView1.Nodes.Clear();
            treeView1.BeginUpdate();
            Cursor.Current = Cursors.WaitCursor;

            List<OUInfo> list = Portal.gc.GetMyTopGroup();
            foreach (OUInfo groupInfo in list)
            {
                //不显示删除的机构
                if (groupInfo != null && !groupInfo.Deleted)
                {
                    TreeNode topnode = new TreeNode();
                    topnode.Text = groupInfo.Name;
                    topnode.Name = groupInfo.ID.ToString();
                    topnode.Tag = groupInfo.ID;
                    topnode.ImageIndex = Portal.gc.GetImageIndex(groupInfo.Category);
                    topnode.SelectedImageIndex = Portal.gc.GetImageIndex(groupInfo.Category);
                    this.treeView1.Nodes.Add(topnode);

                    List<OUNodeInfo> sublist = BLLFactory<OU>.Instance.GetTreeByID(groupInfo.ID);
                    AddOUNode(sublist, topnode);
                }
            }

            Cursor.Current = Cursors.Default;
            treeView1.EndUpdate();
            this.treeView1.ExpandAll();
        }

        private void AddOUNode(List<OUNodeInfo> list, TreeNode parentNode)
        {
            foreach (OUNodeInfo ouInfo in list)
            {
                TreeNode ouNode = new TreeNode();
                ouNode.Text = ouInfo.Name;
                ouNode.Name = ouInfo.ID.ToString();
                ouNode.Tag = ouInfo.ID;
                if (ouInfo.Deleted)
                {
                    ouNode.ForeColor = Color.Red;
                    continue;//跳过不显示
                }
                ouNode.ImageIndex = Portal.gc.GetImageIndex(ouInfo.Category);
                ouNode.SelectedImageIndex = Portal.gc.GetImageIndex(ouInfo.Category);
                parentNode.Nodes.Add(ouNode);

                AddOUNode(ouInfo.Children, ouNode);
            }
        }

        private void RefreshRoles(int id)
        {
            this.lvwRole.BeginUpdate();
            this.lvwRole.Items.Clear();
            List<RoleInfo> list = BLLFactory<Role>.Instance.GetRolesByOU(id);
            foreach (RoleInfo info in list)
            {
                CListItem item = new CListItem(info.Name, info.ID.ToString());
                this.lvwRole.Items.Add(item);
            }
            if (this.lvwRole.Items.Count > 0)
            {
                this.lvwRole.SelectedIndex = 0;
            }
            this.lvwRole.EndUpdate();
        }

        /// <summary>
        /// 记录用户的选择情况
        /// </summary>
        Dictionary<string, string> SelectUserDict = new Dictionary<string, string>();
        private void RefreshUsers(int id)
        {
            SelectUserDict = new Dictionary<string, string>();

            this.lvwUser.BeginUpdate();
            this.lvwUser.Items.Clear();
            List<SimpleUserInfo> list = BLLFactory<User>.Instance.GetSimpleUsersByOU(id);
            foreach (SimpleUserInfo info in list)
            {
                string name = string.Format("{0}（{1}）", info.FullName, info.Name);
                CListItem item = new CListItem(name, info.ID.ToString());
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
                if (MessageDxUtil.ShowYesNoAndTips("您确认删除吗?") == DialogResult.Yes)
                {
                    try
                    {
                        BLLFactory<OU>.Instance.SetDeletedFlag(node.Name);
                        RefreshTreeView();
                    }
                    catch (Exception ex)
                    {
                        LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmOU));
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
                this.cmbUpperOU.Value = node.Tag.ToString();
            }

            this.txtName.Focus();
        }

        private void ClearInput()
        {
            this.txtName.Text = "";
            this.txtNote.Text = "";
            this.txtAddress.Text = "";
            this.txtHandNo.Text = "";
            //this.txtSortCode.Text = "";
            this.txtCreator.Text = Portal.gc.UserInfo.FullName;
            this.txtCreateTime.Text = DateTime.Now.ToString();
            this.txtInnerPhone.Text = "";
            this.txtOuterPhone.Text = "";

            this.lvwRole.Items.Clear();
            this.lvwUser.Items.Clear();

            InitDictItem();
        }

        private OUInfo SetOUInfo(OUInfo info)
        {
            info.Name = this.txtName.Text;
            info.Note = this.txtNote.Text;
            info.Address = this.txtAddress.Text;
            info.InnerPhone = this.txtInnerPhone.Text;
            info.OuterPhone = this.txtOuterPhone.Text;
            info.HandNo = this.txtHandNo.Text;
            info.SortCode = this.txtSortCode.Text;
            info.Category = this.txtCategory.Text;
            info.Editor = Portal.gc.UserInfo.FullName;
            info.Editor_ID = Portal.gc.UserInfo.ID.ToString();
            info.EditTime = DateTime.Now;
            info.PID = this.cmbUpperOU.Value.ToString().ToInt32();

            OUInfo pInfo = BLLFactory<OU>.Instance.FindByID(info.PID);
            if (pInfo != null)
            {   
                //pInfo.Category == "集团" ||
                if ( pInfo.Category == "公司")
                {
                    info.Company_ID = pInfo.ID.ToString();
                    info.CompanyName = pInfo.Name;
                }
                else if (pInfo.Category == "部门" || pInfo.Category == "工作组")
                {
                    info.Company_ID = pInfo.Company_ID;
                    info.CompanyName = pInfo.CompanyName;
                }
            }
            info.CurrentLoginUserId = Portal.gc.UserInfo.ID.ToString();
            return info;
        }

        private void menu_Update_Click(object sender, EventArgs e)
        {
            RefreshTreeView();
        }

        private void menu_ExpandAll_Click(object sender, EventArgs e)
        {
            this.treeView1.ExpandAll();
        }

        private void menu_Collapse_Click(object sender, EventArgs e)
        {
            this.treeView1.CollapseAll();
        }

        public TreeNode GetNodeTopParent(TreeNode n)
        {
            TreeNode returnNode = null;
            if (n.Parent == null)
            {
                returnNode = n;
            }
            else
            {
                returnNode = GetNodeTopParent(n.Parent);
            }
            return returnNode;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null)
            {
                string id = e.Node.Name;
                OUInfo info = BLLFactory<OU>.Instance.FindByID(id);

                if (info != null)
                {
                    currentID = id.ToString();

                    this.txtName.Text = info.Name;
                    this.txtNote.Text = info.Note;
                    this.txtAddress.Text = info.Address;
                    this.txtSortCode.Text = info.SortCode;
                    this.txtHandNo.Text = info.HandNo;
                    this.txtOuterPhone.Text = info.OuterPhone;
                    this.txtInnerPhone.Text = info.InnerPhone;
                    this.txtCategory.Text = info.Category;
                    this.txtCreator.Text = info.Creator;
                    this.txtCreateTime.Text = info.CreateTime.ToString();

                    //由于选择的节点不同，因此根据用户选择的最顶级机构ID进行初始化，才能列出指定机构下的层次关系
                    TreeNode topTreeNode = GetNodeTopParent(e.Node);
                    if(topTreeNode != null && topTreeNode.Tag != null)
                    {
                        string topOuId = topTreeNode.Tag.ToString();
                        this.cmbUpperOU.ParentOuID = topOuId;
                        this.cmbUpperOU.Init();
                    }
                    OUInfo info2 = BLLFactory<OU>.Instance.FindByID(info.PID);
                    if (info2 != null)
                    {
                        this.cmbUpperOU.Value = info.PID.ToString();
                    }
                    else
                    {
                        this.cmbUpperOU.Value = "-1";
                    }

                    //如果是公司管理员，不能编辑公司的信息
                    if (Portal.gc.UserInfo.Company_ID == currentID &&
                        Portal.gc.UserInRole(RoleInfo.CompanyAdminName))
                    {
                        this.btnSave.Enabled = false;
                    }
                    else
                    {
                        this.btnSave.Enabled = true;
                    }

                    int intID = Convert.ToInt32(id);
                    RefreshUsers(intID);
                    RefreshRoles(intID);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            #region 验证用户输入
            if (this.txtName.Text == "")
            {
                MessageDxUtil.ShowTips("机构名称不能为空");
                this.txtName.Focus();
                return;
            }
            else if (this.cmbUpperOU.Text == "")
            {
                MessageDxUtil.ShowTips("上级机构不能为空");
                this.cmbUpperOU.Focus();
                return;
            }

            #endregion

            if (!string.IsNullOrEmpty(currentID))
            {                
                try
                {
                    #region 检查重复
                    int id = Convert.ToInt32(currentID);
                    string filter = string.Format("Name='{0}' AND PID={1} AND ID <> '{2}' ", this.txtName.Text, this.cmbUpperOU.Value, id);
                    bool isExist = BLLFactory<OU>.Instance.IsExistRecord(filter);
                    if (isExist)
                    {
                        MessageDxUtil.ShowTips("组织机构名称已存在！");
                        this.txtName.Focus();
                        return;
                    } 
                    #endregion

                    OUInfo info = BLLFactory<OU>.Instance.FindByID(currentID);
                    if (info != null)
                    {
                        info = SetOUInfo(info);
                        BLLFactory<OU>.Instance.Update(info, info.ID.ToString());

                        RefreshTreeView();
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmOU));
                    MessageDxUtil.ShowError(ex.Message);
                }
            }
            else
            {
                #region 检查重复
                string filter = string.Format("Name='{0}' AND PID={1} ", this.txtName.Text, this.cmbUpperOU.Value);
                bool isExist = BLLFactory<OU>.Instance.IsExistRecord(filter);
                if (isExist)
                {
                    MessageDxUtil.ShowTips("组织机构名称已存在！");
                    this.txtName.Focus();
                    return;
                } 
                #endregion

                OUInfo info = new OUInfo();
                info = SetOUInfo(info);
                info.Creator = Portal.gc.UserInfo.FullName;
                info.Creator_ID = Portal.gc.UserInfo.ID.ToString();
                info.CreateTime = DateTime.Now;

                try
                {
                    BLLFactory<OU>.Instance.Insert(info);
                    RefreshTreeView();
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmOU));
                    MessageDxUtil.ShowError(ex.Message);
                }
            }
        }

        private List<int> addedUserList = new List<int>();
        private List<int> deletedUserList = new List<int>();
        /// <summary>
        /// 获取那些变化了（增加的用户、删除的用户列表）
        /// </summary>
        /// <param name="oldDict">旧的列表</param>
        /// <param name="newDict">新的选择列表</param>
        private void GetUserDictChangs(Dictionary<string, string> oldDict, Dictionary<string, string> newDict)
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
                    GetUserDictChangs(this.SelectUserDict, dlg.SelectUserDict);

                    foreach (int id in deletedUserList)
                    {
                        BLLFactory<OU>.Instance.RemoveUser(id, currentID.ToInt32());
                    }
                    foreach (int id in addedUserList)
                    {
                        BLLFactory<OU>.Instance.AddUser(id, currentID.ToInt32());
                    }

                    this.RefreshUsers(currentID.ToInt32());
                }
            }
            else
            {
                MessageDxUtil.ShowTips("请选择具体的机构");
            }
        }

        private void DeleteUser(int OUID, int UserID)
        {
            BLLFactory<OU>.Instance.RemoveUser(UserID, OUID);
            this.RefreshUsers(OUID);
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
                        int ouID = Convert.ToInt32(currentID);
                        DeleteUser(ouID, userId);
                    }
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

    }
}
