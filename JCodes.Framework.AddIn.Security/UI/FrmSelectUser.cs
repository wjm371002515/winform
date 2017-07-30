using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraTab;
using JCodes.Framework.Entity;
using JCodes.Framework.Common;
using JCodes.Framework.BLL;
using JCodes.Framework.CommonControl;
using JCodes.Framework.AddIn.UI.BizControl;
using JCodes.Framework.CommonControl.BaseUI;
using DevExpress.XtraGrid.Columns;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Common.Extension;
using JCodes.Framework.CommonControl.Pager.Others;
using JCodes.Framework.CommonControl.Controls;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.AddIn.Basic.BizControl;
using JCodes.Framework.AddIn.Basic;

namespace JCodes.Framework.AddIn.Security
{
    public partial class FrmSelectUser : BaseDock
    {
        private Dictionary<string, string> m_SelectUserDict = new Dictionary<string,string>();
        /// <summary>
        /// 选择的角色/人员/部门/业务相关人员的字典数据（实际数据）
        /// </summary>
        public Dictionary<string, string> SelectUserDict
        {
            get
            {
                return m_SelectUserDict;
            }
            set
            {
                m_SelectUserDict = new Dictionary<string, string>(value);
            }
        }

        public FrmSelectUser()
        {
            InitializeComponent();

            this.winGridView1.ShowCheckBox = true;
            this.winGridView1.ShowExportButton = false;
            this.winGridView1.ShowLineNumber = true;
            this.winGridView1.BestFitColumnWith = false;//是否设置为自动调整宽度，false为不设置
            this.winGridView1.OnRefresh += new EventHandler(winGridView1_OnRefresh);
            this.winGridView1.gridView1.DataSourceChanged += new EventHandler(gridView1_DataSourceChanged);

            if (!this.DesignMode)
            {
                InitDeptTree();
                InitRoleTree();
            }
        }

        /// <summary>
        /// 绑定数据后，分配各列的宽度
        /// </summary>
        private void gridView1_DataSourceChanged(object sender, EventArgs e)
        {
            if (this.winGridView1.gridView1.Columns.Count > 0 && this.winGridView1.gridView1.RowCount > 0)
            {
                //统一设置100宽度
                foreach (GridColumn column in this.winGridView1.gridView1.Columns)
                {
                    column.Width = 100;
                }

                //可特殊设置特别的宽度
                winGridView1.gridView1.SetGridColumWidth("Gender", 50);
                winGridView1.gridView1.SetGridColumWidth("Email", 150);
                winGridView1.gridView1.SetGridColumWidth("Note", 150);
            }

        }

        void winGridView1_OnRefresh(object sender, EventArgs e)
        {
            BindGridData();
        }

        private void BindGridData()
        {
            List<UserInfo> list = new List<UserInfo>();
            if (this.treeDept.SelectedNode != null && this.treeDept.SelectedNode.Tag != null)
            {
                int ouId = this.treeDept.SelectedNode.Tag.ToString().ToInt32();
                list = BLLFactory<User>.Instance.FindByDept(ouId);
            }
            else if (this.treeRole.SelectedNode != null && this.treeRole.SelectedNode.Tag != null)
            {
                int roleId = this.treeRole.SelectedNode.Tag.ToString().ToInt32();
                list = BLLFactory<User>.Instance.GetUsersByRole(roleId);
            }

            //entity
            this.winGridView1.DisplayColumns = "HandNo,Name,FullName,Title,MobilePhone,OfficePhone,Email,Gender,QQ,Note";
            this.winGridView1.ColumnNameAlias = BLLFactory<User>.Instance.GetColumnNameAlias();//字段列显示名称转义

            this.winGridView1.DataSource = new SortableBindingList<UserInfo>(list);
        }

        /// <summary>
        /// 刷新选择信息
        /// </summary>
        private void RefreshSelectItems()
        {
            this.flowLayoutPanel1.Controls.Clear();
            foreach (string key in SelectUserDict.Keys)
            {
                string info = SelectUserDict[key];
                if (!string.IsNullOrEmpty(info))
                {
                    UserNameControl control = new UserNameControl();
                    control.BindData(key, info);
                    control.OnDeleteItem += new UserNameControl.DeleteEventHandler(control_OnDeleteItem);
                    this.flowLayoutPanel1.Controls.Add(control);
                }
            }
            this.lblItemCount.Text = string.Format("当前选择【{0}】项目", SelectUserDict.Keys.Count);
        }

        void control_OnDeleteItem(string ID)
        {
            if (SelectUserDict.ContainsKey(ID))
            {
                SelectUserDict.Remove(ID);
                RefreshSelectItems();
            }
        }

        private void FrmSelectFlowUser_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                RefreshSelectItems();
            }
        }

        private void InitDeptTree()
        {
            this.treeDept.BeginUpdate();
            this.treeDept.Nodes.Clear();            

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

                    List<OUNodeInfo> sublist = BLLFactory<OU>.Instance.GetTreeByID(groupInfo.ID);
                    AddDept(sublist, topnode);

                    this.treeDept.Nodes.Add(topnode);
                }
            }
            this.treeDept.ExpandAll();
            this.treeDept.EndUpdate();
        }

        private void AddDept(List<OUNodeInfo> list, TreeNode treeNode)
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
                treeNode.Nodes.Add(deptNode);

                AddDept(ouInfo.Children, deptNode);
            }
        }

        private void InitRoleTree()
        {
            this.treeRole.BeginUpdate();
            this.treeRole.Nodes.Clear();

            List<OUInfo> list = Portal.gc.GetMyTopGroup();
            foreach (OUInfo groupInfo in list)
            {
                if (groupInfo != null)
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
                    this.treeRole.Nodes.Add(topnode);
                }
            }

            this.treeRole.ExpandAll();
            this.treeRole.EndUpdate();
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
                roleNode.Tag = roleInfo.ID;
                roleNode.ImageIndex = 5;
                roleNode.SelectedImageIndex = 5;
                if (ouInfo.Deleted)
                {
                    roleNode.ForeColor = Color.Red;
                    continue;//跳过不显示
                }

                treeNode.Nodes.Add(roleNode);
            }
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            this.SelectUserDict = new Dictionary<string, string>();
            RefreshSelectItems();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.SelectUserDict.Count == 0)
            {
                MessageDxUtil.ShowTips("您还未选择人员");
                return;
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void treeDept_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (!this.DesignMode)
            {
                this.treeRole.SelectedNode = null;//检索部门的时候，去除角色的选择
                BindGridData();
            }
        }

        private void treeRole_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (!this.DesignMode)
            {
                this.treeDept.SelectedNode = null;//检索角色的时候，去掉部门的选择
                BindGridData();
            }
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            List<int> list = this.winGridView1.GetCheckedRows();
            foreach(int rowIndex in list)
            {
                string ID = this.winGridView1.GridView1.GetRowCellDisplayText(rowIndex, "ID");
                string Name= this.winGridView1.GridView1.GetRowCellDisplayText(rowIndex, "Name");
                string FullName = this.winGridView1.GridView1.GetRowCellDisplayText(rowIndex, "FullName");
                string displayname = string.Format("{0}（{1}）", FullName, Name);

                if (!this.SelectUserDict.ContainsKey(ID))
                {
                    this.SelectUserDict.Add(ID, displayname);
                }
            }

            RefreshSelectItems();
        }

    }
}
